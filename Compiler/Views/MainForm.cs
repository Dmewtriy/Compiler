using Compiler.Models;
using Compiler.Views.Interfaces;
using System.IO;

namespace Compiler
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
            CreateColumns();
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

        public event Action<int, int> NavigateToErrorRequested;

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
            toolRun.Click += (s, e) => RunClicked?.Invoke(this, EventArgs.Empty);

            menuHelp.Click += (s, e) => HelpClicked?.Invoke(this, EventArgs.Empty);
            toolHelp.Click += (s, e) => HelpClicked?.Invoke(this, EventArgs.Empty);

            menuAbout.Click += (s, e) => AboutClicked?.Invoke(this, EventArgs.Empty);
            toolAbout.Click += (s, e) => AboutClicked?.Invoke(this, EventArgs.Empty);

            richTextBoxEdit.TextChanged += (s, e) => ContentChanged?.Invoke(this, EventArgs.Empty);

            tabControlOutput.DrawItem += new DrawItemEventHandler(tabControlOutput_DrawItem);

            this.FormClosing += (s, e) => ViewClosing?.Invoke(this, e);

        }

        public string PolizContent
        {
            set => txtTerminal.Text = value;
        }

        public void ClearAll()
        {
            dgvLexer.Rows.Clear();
            txtTerminal.Clear();
            dgvParser.Rows.Clear();
        }

        public void ShowErrors(List<string> errors)
        {
            foreach (var error in errors)
            {
                dgvParser.Rows.Add(error);
            }
        }

        public void ShowTetrads(List<Tetrad> tetrads)
        {
            dgvLexer.Rows.Clear();
            foreach (var t in tetrads)
            {
                dgvLexer.Rows.Add(t.Op, t.Arg1, t.Arg2, t.Result);
            }
        }

        public string ShowOpenFileDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string exeDirectory = Path.GetDirectoryName(exePath);

                string testsPath = Path.Combine(exeDirectory, "Tests");

                if (Directory.Exists(testsPath))
                {
                    openFileDialog.InitialDirectory = testsPath;
                }
                else
                {
                    openFileDialog.InitialDirectory = exeDirectory;
                }

                openFileDialog.RestoreDirectory = true;
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return null;
        }

        public string? ShowSaveFileDialog()
        {
            using var sfd = new SaveFileDialog { Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*" };
            return sfd.ShowDialog() == DialogResult.OK ? sfd.FileName : null;
        }

        public void ShowMessage(string title, string message)
        {
            CenteredMessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (richTextBoxEdit.Focused)
            {
                if (richTextBoxEdit.SelectionLength > 0)
                {
                    richTextBoxEdit.SelectedText = "";
                }
                else if (richTextBoxEdit.SelectionStart < richTextBoxEdit.TextLength)
                {
                    richTextBoxEdit.Select(richTextBoxEdit.SelectionStart, 1);
                    richTextBoxEdit.SelectedText = "";
                }
            }
        }
        public void PerformSelectAll()
        {
            richTextBoxEdit.SelectAll();
        }

        public DialogResult ConfirmSaveBeforeAction()
        {
            return CenteredMessageBox.Show(this, "В файле есть несохраненные изменения. Сохранить их?",
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

        private void CreateColumns()
        {
            CreateColumnsTetrads();
            CreateColumnsErrors();
        }

        private void CreateColumnsTetrads()
        {
            dgvLexer.Columns.Clear();

            dgvLexer.Columns.Add("Op", "Op");
            dgvLexer.Columns["Op"].FillWeight = 40;

            dgvLexer.Columns.Add("ColArg1", "Arg 1");
            dgvLexer.Columns["ColArg1"].FillWeight = 40;

            dgvLexer.Columns.Add("ColArg2", "Arg 2");
            dgvLexer.Columns["ColArg2"].FillWeight = 40;

            dgvLexer.Columns.Add("ColRes", "Result");
            dgvLexer.Columns["ColRes"].FillWeight = 80;


            foreach (DataGridViewColumn column in dgvLexer.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void CreateColumnsErrors()
        {
            dgvParser.Columns.Clear();
            dgvParser.Columns.Add("Error", "Неверный фрагмент");
            dgvParser.Columns["Error"].FillWeight = 100;

            foreach (DataGridViewColumn column in dgvParser.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void ClearResults()
        {
            dgvLexer.Rows.Clear();
            dgvParser.Rows.Clear();
        }

        private void tabControlOutput_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            TabPage page = tabControlOutput.TabPages[e.Index];
            Rectangle rect = tabControlOutput.GetTabRect(e.Index);

            Color activeBorderColor = Color.FromArgb(0, 122, 204);
            Color inactiveTextColor = Color.FromArgb(150, 150, 150);
            Color backgroundColor = Color.White;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.FillRectangle(new SolidBrush(backgroundColor), rect);

            if (e.State == DrawItemState.Selected)
            {
                using (Pen p = new Pen(activeBorderColor, 2))
                {
                    g.DrawLine(p, rect.Left, rect.Bottom, rect.Left, rect.Top);
                    g.DrawLine(p, rect.Left, rect.Top, rect.Right, rect.Top);
                    g.DrawLine(p, rect.Right, rect.Top, rect.Right, rect.Bottom);
                }

                TextRenderer.DrawText(g, page.Text, new Font(e.Font, FontStyle.Bold),
                    rect, Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
            }
            else
            {
                TextRenderer.DrawText(g, page.Text, e.Font,
                    rect, inactiveTextColor, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

                g.DrawLine(Pens.LightGray, rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
            }
        }

        private void tabControlOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
    }
}
