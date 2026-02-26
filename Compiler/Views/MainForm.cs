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
            get => richTextBox1.Text;
            set => richTextBox1.Text = value;
        }

        public bool IsEditorVisible
        {
            get => richTextBox1.Visible;
            set
            {
                richTextBox1.Visible = value;
                lblPlaceholder.Visible = !value;
            }
        }

        public string WindowTitle
        {
            set => this.Text = value;
        }

        public event EventHandler NewFileClicked;
        public event EventHandler OpenFileClicked;
        public event EventHandler SaveFileClicked;
        public event EventHandler UndoClicked;
        public event EventHandler RedoClicked;
        public event EventHandler CopyClicked;
        public event EventHandler CutClicked;
        public event EventHandler PasteClicked;
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
            NewFileSB.Click += (s, e) => NewFileClicked?.Invoke(this, EventArgs.Empty);
            OpenSB.Click += (s, e) => OpenFileClicked?.Invoke(this, EventArgs.Empty);
            SaveSB.Click += (s, e) => SaveFileClicked?.Invoke(this, EventArgs.Empty);

            CancelSB.Click += (s, e) => UndoClicked?.Invoke(this, EventArgs.Empty);
            ReturnSB.Click += (s, e) => RedoClicked?.Invoke(this, EventArgs.Empty);
            CopySB.Click += (s, e) => CopyClicked?.Invoke(this, EventArgs.Empty);
            CutSB.Click += (s, e) => CutClicked?.Invoke(this, EventArgs.Empty);
            PasteSB.Click += (s, e) => PasteClicked?.Invoke(this, EventArgs.Empty);

            HelpSB.Click += (s, e) => HelpClicked?.Invoke(this, EventArgs.Empty);
            AboutSB.Click += (s, e) => AboutClicked?.Invoke(this, EventArgs.Empty);

            richTextBox1.TextChanged += (s, e) => ContentChanged?.Invoke(this, EventArgs.Empty);
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

        public void PerformUndo() { if (richTextBox1.CanUndo) richTextBox1.Undo(); }
        public void PerformRedo() { if (richTextBox1.CanRedo) richTextBox1.Redo(); }
        public void PerformCopy() { richTextBox1.Copy(); }
        public void PerformCut() { richTextBox1.Cut(); }
        public void PerformPaste() { richTextBox1.Paste(); }

        public DialogResult ConfirmSaveBeforeAction()
        {
            return MessageBox.Show("В файле есть несохраненные изменения. Сохранить их?",
                "Внимание", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
        }

        public void CloseView() => Application.Exit();

    }
}
