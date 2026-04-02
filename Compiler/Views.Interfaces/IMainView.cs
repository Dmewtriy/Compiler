using Compiler.Models;

namespace Compiler.Views.Interfaces
{
    public interface IMainView
    {
        string EditorContent { get; set; }
        string SelectedPattern { get; }
        bool IsEditorVisible { get; set; }
        string WindowTitle { set; }
        public string StatusText { set; }

        event EventHandler NewFileClicked;
        event EventHandler OpenFileClicked;
        event EventHandler SaveFileClicked;
        event EventHandler SaveAsClicked;
        event EventHandler ExitClicked;

        event EventHandler UndoClicked;
        event EventHandler RedoClicked;
        event EventHandler CopyClicked;
        event EventHandler CutClicked;
        event EventHandler PasteClicked;
        event EventHandler DeleteClicked;
        event EventHandler SelectAllClicked;

        event EventHandler TaskDescriptionClicked;
        event EventHandler GrammarClicked;
        event EventHandler GrammarClassificationClicked;
        event EventHandler AnalysisMethodClicked;
        event EventHandler TestExampleClicked;
        event EventHandler ReferencesClicked;
        event EventHandler SourceCodeClicked;

        event EventHandler RunClicked;

        event EventHandler HelpClicked;
        event EventHandler AboutClicked;

        event EventHandler ContentChanged;
        event Action<int, int> NavigateToErrorRequested;

        event FormClosingEventHandler ViewClosing;

        event EventHandler<RegexSearchResult> ResultSelected;

        void ShowMessage(string title, string message);
        string? ShowOpenFileDialog();
        string? ShowSaveFileDialog();

        void PerformUndo();
        void PerformRedo();
        void PerformCopy();
        void PerformCut();
        void PerformPaste();
        void PerformDelete();
        void PerformSelectAll();

        DialogResult ConfirmSaveBeforeAction();

        void DisplayResults(List<RegexSearchResult> results);
        void SelectTextInEditor(int start, int length);

        void ClearResults();

        void CloseView();
    }
}
