using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Views.Interfaces
{
    public interface IMainView
    {
        string EditorContent { get; set; }
        bool IsEditorVisible { get; set; }
        string WindowTitle { set; }

        event EventHandler NewFileClicked;
        event EventHandler OpenFileClicked;
        event EventHandler SaveFileClicked;

        event EventHandler UndoClicked;
        event EventHandler RedoClicked;
        event EventHandler CopyClicked;
        event EventHandler CutClicked;
        event EventHandler PasteClicked;

        event EventHandler HelpClicked;
        event EventHandler AboutClicked;

        event EventHandler ContentChanged;

        event FormClosingEventHandler ViewClosing;

        void ShowMessage(string title, string message);
        string ShowOpenFileDialog();
        string ShowSaveFileDialog();

        void PerformUndo();
        void PerformRedo();
        void PerformCopy();
        void PerformCut();
        void PerformPaste();

        DialogResult ConfirmSaveBeforeAction();

        void CloseView();
    }
}
