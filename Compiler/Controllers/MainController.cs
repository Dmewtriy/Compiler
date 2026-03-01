using Compiler.Models;
using Compiler.Views;
using Compiler.Views.Interfaces;
using Markdig;

namespace Compiler.Controllers
{
    public class MainController
    {
        private readonly IMainView _view;
        private readonly FileService _model;
        private bool _isModified = false;
        private const string AppName = "Compiler";
        private readonly InfoService _infoService;
        private readonly Scanner _scanner;

        public MainController(IMainView view, FileService model, InfoService infoModel, Scanner scanner)
        {
            _view = view;
            _model = model;
            _infoService = infoModel;
            _scanner = scanner;

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

            // Добавить проверку на открытый файл
            _view.TaskDescriptionClicked += (s, e) => { };
            _view.GrammarClicked += (s, e) => { };
            _view.GrammarClassificationClicked += (s, e) => { };
            _view.AnalysisMethodClicked += (s, e) => { };
            _view.TestExampleClicked += (s, e) => { };
            _view.ReferencesClicked += (s, e) => { };
            _view.SourceCodeClicked += (s, e) => { };

            _view.RunClicked += OnRunClicked;

            _view.HelpClicked += (s, e) => ShowInfoWindow("Справка", _infoService.GetHelpText());
            _view.AboutClicked += (s, e) => ShowInfoWindow("О программе", _infoService.GetAboutText());

            _view.ContentChanged += OnContentChanged;

            _view.NavigateToErrorRequested += (start, len) => _view.SelectTextInEditor(start, len);

            _view.ViewClosing += OnViewClosing;

            UpdateTitle();

        }

        private void UpdateTitle()
        {
            string fileName = string.IsNullOrEmpty(_model.CurrentFilePath)
                ? "Новый файл"
                : Path.GetFileName(_model.CurrentFilePath);

            string modifiedMark = _isModified ? "*" : "";

            _view.WindowTitle = $"{AppName} - [{fileName}{modifiedMark}]";
            _view.StatusText = _model.CurrentFilePath ?? "Путь не определен";
        }

        private void OnContentChanged(object sender, EventArgs e)
        {
            if (!_isModified)
            {
                _isModified = true;
                UpdateTitle();
            }
        }

        private void OnNewFile(object sender, EventArgs e)
        {
            if (EnsureChangesSaved())
            {
                _model.ClearCurrentFile();
                _view.EditorContent = string.Empty;
                _view.IsEditorVisible = true;
                _isModified = false;
                UpdateTitle();
            }
        }

        private void OnOpenFile(object sender, EventArgs e)
        {
            if (!EnsureChangesSaved())
            {
                return;
            }

            try
            {
                string filePath = _view.ShowOpenFileDialog();
                if (!string.IsNullOrEmpty(filePath))
                {
                    string content = _model.OpenFile(filePath);
                    _view.EditorContent = content;

                    _view.IsEditorVisible = true;
                    _isModified = false;
                    UpdateTitle();
                }
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
            else
            {
                return PerformSave();
            }
        }

        private bool PerformSave()
        {
            return ExecuteFileWrite(_model.CurrentFilePath);
        }

        private bool PerformSaveAs()
        {
            string newPath = _view.ShowSaveFileDialog();

            if (!string.IsNullOrEmpty(newPath))
            {
                return ExecuteFileWrite(newPath);
            }

            return false;
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

        private void OnHelp(object sender, EventArgs e)
        {
            _view.ShowMessage("Справка", "Справка по компилятору.\n\nИспользуйте редактор для написания кода.");
        }

        private void OnAbout(object sender, EventArgs e)
        {
            _view.ShowMessage("О программе", "Compiler GUI v1.0\nРазработано с использованием паттерна MVC на WinForms.");
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
            if (_isModified)
            {
                DialogResult result = _view.ConfirmSaveBeforeAction();

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
                }
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

        private void ShowInfoWindow(string title, string markdownContent)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

            string htmlBody = Markdown.ToHtml(markdownContent, pipeline);

            string finalHtml = $@"
            <html>
            <head>
                <meta http-equiv='X-UA-Compatible' content='IE=edge' />
                <style>
                    body {{ font-family: 'Segoe UI', sans-serif; padding: 20px; line-height: 1.6; }}
                    code {{ background-color: #f4f4f4; padding: 2px 4px; border-radius: 4px; }}
                    pre {{ background-color: #f4f4f4; padding: 10px; border: 1px solid #ddd; overflow: auto; }}
                    h1, h2 {{ color: #2c3e50; border-bottom: 1px solid #eee; }}
                </style>
            </head>
            <body>
                {htmlBody}
            </body>
            </html>";

            using (var infoForm = new InfoForm(title, finalHtml))
            {
                infoForm.ShowDialog();
            }
        }

        private void OnRunClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_view.EditorContent))
            {
                _view.ShowMessage("Внимание", "Редактор пуст. Нечего анализировать.");
                return;
            }

            var tokens = _scanner.Analyze(_view.EditorContent);

            _view.ShowTokens(tokens);
        }
    }

}
