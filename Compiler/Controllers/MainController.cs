using Compiler.Models;
using Compiler.Views;
using Compiler.Views.Interfaces;
using System.Security.AccessControl;

namespace Compiler.Controllers
{
    public class MainController
    {
        private readonly IMainView _view;
        private readonly FileService _model;
        private bool _isModified;
        private const string AppName = "Compiler";
        private readonly Scanner _scanner;
        private readonly Parser _parser;
        private readonly SemanticAnalyzer _semanticAnalyzer;
        private readonly AstVisualizer _visualizer;

        public MainController(IMainView view, FileService model, InfoService infoServ, Scanner scanner)
        {
            _view = view;
            _model = model;
            var infoService = infoServ;
            _scanner = scanner;
            _parser = new Parser();
            _semanticAnalyzer = new SemanticAnalyzer();
            _visualizer = new AstVisualizer();

            _view.NewFileClicked += OnNewFile;
            _view.OpenFileClicked += OnOpenFile;
            _view.SaveFileClicked += (s, e) => TrySave(false);
            _view.SaveAsClicked += (s, e) => TrySave(true);
            _view.ExitClicked += OnExitRequest;

            _view.UndoClicked += (s, e) => ExecuteIfEditorActive(_view.PerformUndo);
            _view.RedoClicked += (s, e) => ExecuteIfEditorActive(_view.PerformRedo);
            _view.CopyClicked += (s, e) => ExecuteIfEditorActive(_view.PerformCopy);
            _view.CutClicked += (s, e) => ExecuteIfEditorActive(_view.PerformCut);
            _view.PasteClicked += (s, e) => ExecuteIfEditorActive(_view.PerformPaste);
            _view.DeleteClicked += (s, e) => ExecuteIfEditorActive(_view.PerformDelete);
            _view.SelectAllClicked += (s, e) => ExecuteIfEditorActive(_view.PerformSelectAll);

            _view.TaskDescriptionClicked += (s, e) => ShowInfoWindow("Постановка задачи", infoService.GetTaskDescriptionContent());
            _view.GrammarClicked += (s, e) => ShowInfoWindow("Грамматика", infoService.GetGrammarContent());
            _view.GrammarClassificationClicked += (s, e) => ShowInfoWindow("Классификация грамматики", infoService.GetGrammarClassificationContent());
            _view.AnalysisMethodClicked += (s, e) => ShowInfoWindow("Метод анализа", infoService.GetAnalysisMethodContent());
            _view.TestExampleClicked += (s, e) => ShowInfoWindow("Тестовый пример", infoService.GetTestExampleContent());
            _view.ReferencesClicked += (s, e) => ShowInfoWindow("Список литературы", infoService.GetReferencesContent());
            _view.SourceCodeClicked += (s, e) => ShowSourceCode(infoService.GetSourceCodeContent());

            _view.RunClicked += OnRunClicked;

            _view.HelpClicked += (s, e) => ShowInfoWindow("Справка", infoService.GetHelpText());
            _view.AboutClicked += (s, e) => ShowInfoWindow("О программе", infoService.GetAboutText());

            _view.ContentChanged += OnContentChanged;

            _view.NavigateToErrorRequested += (start, len) => _view.SelectTextInEditor(start, len);

            _view.ViewClosing += OnViewClosing;

            UpdateTitle();
        }

        private void UpdateTitle()
        {
            var fileName = string.IsNullOrEmpty(_model.CurrentFilePath)
                ? "Новый файл"
                : Path.GetFileName(_model.CurrentFilePath);

            var modifiedMark = _isModified ? "*" : "";

            _view.WindowTitle = $"{AppName} - [{fileName}{modifiedMark}]";
            _view.StatusText = _model.CurrentFilePath ?? "Путь не определен";
        }

        private void OnContentChanged(object sender, EventArgs e)
        {
            if (_isModified)
            {
                return;
            }

            _isModified = true;
            UpdateTitle();
        }

        private void OnNewFile(object sender, EventArgs e)
        {
            if (!EnsureChangesSaved())
            {
                return;
            }

            _model.ClearCurrentFile();
            _view.EditorContent = string.Empty;
            _view.IsEditorVisible = true;
            _isModified = false;
            UpdateTitle();
            _view.SelectTextInEditor(0, 0);
        }

        private void OnOpenFile(object sender, EventArgs e)
        {
            if (!EnsureChangesSaved())
            {
                return;
            }

            try
            {
                var filePath = _view.ShowOpenFileDialog();

                if (string.IsNullOrEmpty(filePath))
                {
                    return;
                }

                var content = _model.OpenFile(filePath);
                _view.EditorContent = content;

                _view.IsEditorVisible = true;
                _isModified = false;
                UpdateTitle();
                _view.SelectTextInEditor(_view.EditorContent.Length, 0);
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Ошибка", $"Не удалось открыть файл: {ex.Message}");
            }
        }

        private bool TrySave(bool isSaveAs)
        {
            if (isSaveAs || string.IsNullOrEmpty(_model.CurrentFilePath))
            {
                return PerformSaveAs();
            } 
            return PerformSave();

        }

        private bool PerformSave()
        {
            return ExecuteFileWrite(_model.CurrentFilePath);
        }

        private bool PerformSaveAs()
        {
            var newPath = _view.ShowSaveFileDialog();

            return !string.IsNullOrEmpty(newPath) && ExecuteFileWrite(newPath);
        }

        private bool ExecuteFileWrite(string path)
        {
            try
            {
                _model.SaveFile(path, _view.EditorContent);
                _isModified = false;
                UpdateTitle();

                return true;
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Ошибка сохранения", ex.Message);
                return false;
            }
        }

        private bool EnsureChangesSaved()
        {
            if (!_isModified) return true;

            var result = _view.ConfirmSaveBeforeAction();

            if (result == DialogResult.Yes)
            {
                return TrySave(false);
            }

            return result == DialogResult.No;
        }

        private void OnViewClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isModified)
            {
                return;
            }
            var result = _view.ConfirmSaveBeforeAction();

            switch (result)
            {
                case DialogResult.Yes:
                    if (!TrySave(false))
                    {
                        e.Cancel = true;
                    }
                    break;

                case DialogResult.No:
                    break;

                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.None:
                case DialogResult.Ignore:
                case DialogResult.Continue:
                case DialogResult.OK:
                case DialogResult.TryAgain:
                default:
                    break;
            }
        }

        private void OnExitRequest(object sender, EventArgs e)
        {
            _view.CloseView();
        }

        private void ExecuteIfEditorActive(Action editorAction)
        {
            if (_view.IsEditorVisible)
            {
                editorAction.Invoke();
            }
        }

        private static void ShowInfoWindow(string title, string content)
        {
            using var infoForm = new InfoForm(title, content);
            infoForm.ShowDialog();
        }

        private void OnRunClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_view.EditorContent))
            {
                _view.ClearResults();
                _view.ShowMessage("Внимание", "Редактор пуст. Нечего анализировать.");
                return;
            }

            var tokens = _scanner.Analyze(_view.EditorContent);

            _view.ShowTokens(tokens);

            OnParseRequested(tokens);
        }

        private void OnParseRequested(List<Token> tokens)
        {

            var defaultParserRes = ParseDefaultParser(tokens);

            _view.DisplayErrorsParser(defaultParserRes);
        }

        private List<SyntaxError> ParseDefaultParser(List<Token> tokens)
        {

            SemanticAnaliz(_parser.Parse(tokens));

            return _parser.Errors;
        }

        private void SemanticAnaliz(List<EnumDeclNode> enumDeclNodes)
        {
            var cleanAst = _semanticAnalyzer.Analyze(enumDeclNodes);

            _view.ShowSemanticErrors(_semanticAnalyzer.Errors);

            _view.AstContent = _visualizer.GenerateTreeText(cleanAst);
        }

        private void ShowSourceCode(string url)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }

}
