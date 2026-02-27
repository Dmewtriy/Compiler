using Compiler.Views.Interfaces;

namespace Compiler
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
            BindEvents();
        }

        public string EditorContent
        {
            get => richTextBoxEdit.Text;
            set => richTextBoxEdit.Text = value;
        }

        public bool IsEditorVisible
        {
            get => richTextBoxEdit.Visible;
            set
            {
                richTextBoxEdit.Visible = value;
                lblPlaceholder.Visible = !value;
            }
        }

        public string WindowTitle
        {
            set => this.Text = value;
        }

        public string StatusText
        {
            set => FilePathStatusLabel.Text = value;
        }

        public event EventHandler NewFileClicked;
        public event EventHandler OpenFileClicked;
        public event EventHandler SaveFileClicked;
        public event EventHandler SaveAsClicked;
        public event EventHandler ExitClicked;

        public event EventHandler UndoClicked;
        public event EventHandler RedoClicked;
        public event EventHandler CopyClicked;
        public event EventHandler CutClicked;
        public event EventHandler PasteClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler SelectAllClicked;

        public event EventHandler TaskDescriptionClicked;
        public event EventHandler GrammarClicked;
        public event EventHandler GrammarClassificationClicked;
        public event EventHandler AnalysisMethodClicked;
        public event EventHandler TestExampleClicked;
        public event EventHandler ReferencesClicked;
        public event EventHandler SourceCodeClicked;

        public event EventHandler RunClicked;

        public event EventHandler HelpClicked;
        public event EventHandler AboutClicked;

        public event EventHandler ContentChanged;

        public event FormClosingEventHandler ViewClosing;

        private void MainForm_Load(object sender, EventArgs e)
        {
            IsEditorVisible = false;
        }

        private void BindEvents()
        {
            menuCreate.Click += (s, e) => NewFileClicked?.Invoke(this, EventArgs.Empty);
            toolCreate.Click += (s, e) => NewFileClicked?.Invoke(this, EventArgs.Empty);

            menuOpen.Click += (s, e) => OpenFileClicked?.Invoke(this, EventArgs.Empty);
            toolOpen.Click += (s, e) => OpenFileClicked?.Invoke(this, EventArgs.Empty);

            menuSave.Click += (s, e) => SaveFileClicked?.Invoke(this, EventArgs.Empty);
            toolSave.Click += (s, e) => SaveFileClicked?.Invoke(this, EventArgs.Empty);

            menuSaveAs.Click += (s, e) => SaveAsClicked?.Invoke(this, EventArgs.Empty);
            menuExit.Click += (s, e) => ExitClicked?.Invoke(this, EventArgs.Empty);

            menuCancel.Click += (s, e) => UndoClicked?.Invoke(this, EventArgs.Empty);
            toolCancel.Click += (s, e) => UndoClicked?.Invoke(this, EventArgs.Empty);

            menuReturn.Click += (s, e) => RedoClicked?.Invoke(this, EventArgs.Empty);
            toolReturn.Click += (s, e) => RedoClicked?.Invoke(this, EventArgs.Empty);

            menuCopy.Click += (s, e) => CopyClicked?.Invoke(this, EventArgs.Empty);
            toolCopy.Click += (s, e) => CopyClicked?.Invoke(this, EventArgs.Empty);

            menuCut.Click += (s, e) => CutClicked?.Invoke(this, EventArgs.Empty);
            toolCut.Click += (s, e) => CutClicked?.Invoke(this, EventArgs.Empty);

            menuPaste.Click += (s, e) => PasteClicked?.Invoke(this, EventArgs.Empty);
            toolPaste.Click += (s, e) => PasteClicked?.Invoke(this, EventArgs.Empty);

            menuDelete.Click += (s, e) => DeleteClicked?.Invoke(this, EventArgs.Empty);
            menuSelectAll.Click += (s, e) => SelectAllClicked?.Invoke(this, EventArgs.Empty);

            menuTaskDescription.Click += (s, e) => TaskDescriptionClicked?.Invoke(this, EventArgs.Empty);
            menuGrammar.Click += (s, e) => GrammarClicked?.Invoke(this, EventArgs.Empty);
            menuGrammarClassification.Click += (s, e) => GrammarClassificationClicked?.Invoke(this, EventArgs.Empty);
            menuAnalysisMethod.Click += (s, e) => AnalysisMethodClicked?.Invoke(this, EventArgs.Empty);
            menuTestExample.Click += (s, e) => TestExampleClicked?.Invoke(this, EventArgs.Empty);
            menuReferences.Click += (s, e) => ReferencesClicked?.Invoke(this, EventArgs.Empty);
            menuSourceCode.Click += (s, e) => SourceCodeClicked?.Invoke(this, EventArgs.Empty);

            run.Click += (s, e) => RunClicked?.Invoke(this, EventArgs.Empty);

            menuHelp.Click += (s, e) => HelpClicked?.Invoke(this, EventArgs.Empty);
            toolHelp.Click += (s, e) => HelpClicked?.Invoke(this, EventArgs.Empty);

            menuAbout.Click += (s, e) => AboutClicked?.Invoke(this, EventArgs.Empty);
            toolAbout.Click += (s, e) => AboutClicked?.Invoke(this, EventArgs.Empty);

            richTextBoxEdit.TextChanged += (s, e) => ContentChanged?.Invoke(this, EventArgs.Empty);
            this.FormClosing += (s, e) => ViewClosing?.Invoke(this, e);

        }

        public string ShowOpenFileDialog()
        {
            using (var ofd = new OpenFileDialog { Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*" })
            {
                return ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : null;
            }
        }

        public string ShowSaveFileDialog()
        {
            using (var sfd = new SaveFileDialog { Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*" })
            {
                return sfd.ShowDialog() == DialogResult.OK ? sfd.FileName : null;
            }
        }

        public void ShowMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void PerformUndo()
        {
            if (richTextBoxEdit.CanUndo)
                richTextBoxEdit.Undo();
        }
        public void PerformRedo()
        {
            if (richTextBoxEdit.CanRedo)
                richTextBoxEdit.Redo();
        }
        public void PerformCopy()
        {
            richTextBoxEdit.Copy();
        }
        public void PerformCut()
        {
            richTextBoxEdit.Cut();
        }
        public void PerformPaste()
        {
            richTextBoxEdit.Paste();
        }
        public void PerformDelete()
        {
            richTextBoxEdit.SelectedText = "";
        }
        public void PerformSelectAll()
        {
            richTextBoxEdit.SelectAll();
        }

        public DialogResult ConfirmSaveBeforeAction()
        {
            return MessageBox.Show("В файле есть несохраненные изменения. Сохранить их?",
                "Внимание", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
        }

        public void CloseView() => Application.Exit();

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter) 
            { 
                richTextBoxEdit.SelectionFont = richTextBoxEdit.SelectionFont; 
            }
        }
    }
}
