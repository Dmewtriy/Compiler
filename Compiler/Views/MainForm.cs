using Compiler.Models;
using Compiler.Views.Interfaces;
using ParserANTLR;

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

        public string? ShowOpenFileDialog()
        {
            using var ofd = new OpenFileDialog { Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*" };
            return ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : null;
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
            richTextBoxEdit.SelectedText = "";
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
            CreateColumnsLexer();
            CreateColumnsParser();
            CreateColumnsFlexBison();
            CreateColumnsAntlr();
        }

        private void CreateColumnsLexer()
        {
            dgvLexer.Columns.Clear();

            dgvLexer.Columns.Add("colCode", "Код");
            dgvLexer.Columns["colCode"].FillWeight = 30;

            dgvLexer.Columns.Add("colType", "Тип лексемы");
            dgvLexer.Columns["colType"].FillWeight = 100;

            dgvLexer.Columns.Add("colLexeme", "Лексема");
            dgvLexer.Columns["colLexeme"].FillWeight = 100;

            dgvLexer.Columns.Add("colPos", "Местоположение");
            dgvLexer.Columns["colPos"].FillWeight = 80;

            dgvLexer.Columns.Add("colAbsIndex", "Index");
            dgvLexer.Columns["colAbsIndex"].Visible = false;

            foreach (DataGridViewColumn column in dgvLexer.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void CreateColumnsParser()
        {
            dgvParser.Columns.Clear();
            dgvParser.Columns.Add("Fragment", "Неверный фрагмент");
            dgvParser.Columns["Fragment"].FillWeight = 50;

            dgvParser.Columns.Add("Location", "Местоположение");
            dgvParser.Columns["Location"].FillWeight = 80;

            dgvParser.Columns.Add("Description", "Описание ошибки");
            dgvParser.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvParser.Columns.Add("colAbsIndex", "Index");
            dgvParser.Columns["colAbsIndex"].Visible = false;
            dgvParser.Columns.Add("colLen", "Len");
            dgvParser.Columns["colLen"].Visible = false;

            foreach (DataGridViewColumn column in dgvParser.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void CreateColumnsFlexBison()
        {
            dgvFlexBison.Columns.Clear();
            dgvFlexBison.Columns.Add("Fragment", "Неверный фрагмент");
            dgvFlexBison.Columns["Fragment"].FillWeight = 50;

            dgvFlexBison.Columns.Add("Location", "Местоположение");
            dgvFlexBison.Columns["Location"].FillWeight = 80;

            dgvFlexBison.Columns.Add("Description", "Описание ошибки");
            dgvFlexBison.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvFlexBison.Columns.Add("colAbsIndex", "Index");
            dgvFlexBison.Columns["colAbsIndex"].Visible = false;
            dgvFlexBison.Columns.Add("colLen", "Len");
            dgvFlexBison.Columns["colLen"].Visible = false;

            foreach (DataGridViewColumn column in dgvFlexBison.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void CreateColumnsAntlr()
        {
            dgvAntlr.Columns.Clear();
            dgvAntlr.Columns.Add("Fragment", "Неверный фрагмент");
            dgvAntlr.Columns["Fragment"].FillWeight = 50;

            dgvAntlr.Columns.Add("Location", "Местоположение");
            dgvAntlr.Columns["Location"].FillWeight = 80;

            dgvAntlr.Columns.Add("Description", "Описание ошибки");
            dgvAntlr.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvAntlr.Columns.Add("colAbsIndex", "Index");
            dgvAntlr.Columns["colAbsIndex"].Visible = false;
            dgvAntlr.Columns.Add("colLen", "Len");
            dgvAntlr.Columns["colLen"].Visible = false;

            foreach (DataGridViewColumn column in dgvAntlr.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void ClearResults()
        {
            dgvLexer.Rows.Clear();
            dgvParser.Rows.Clear();
            dgvFlexBison.Rows.Clear();
            dgvAntlr.Rows.Clear();
        }

        public void ShowTokens(List<Token> tokens)
        {
            dgvLexer.Rows.Clear();
            foreach (var token in tokens)
            {
                string pos = $"строка {token.Line}, {token.StartPos}-{token.EndPos}";
                dgvLexer.Rows.Add(token.NumericCode, token.TypeName, token.Lexeme, pos, token.AbsoluteIndex);

                if (token.Type == TokenType.INVALID_TOKEN)
                {
                    dgvLexer.Rows[^1].DefaultCellStyle.BackColor = Color.LightPink;
                }
            }
        }


        public void SelectTextInEditor(int start, int length)
        {
            richTextBoxEdit.Focus();
            richTextBoxEdit.Select(start, length);
        }

        public void SetParserResult(string message, bool isSuccess)
        {
            if (!isSuccess)
            {
                CenteredMessageBox.Show(this, message, "Ошибка синтаксиса", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                CenteredMessageBox.Show(this, message, "Ошибок не обнаружено", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        public void DisplayErrorsParser(List<SyntaxError> errors)
        {
            dgvParser.Rows.Clear();
            foreach (var error in errors)
            {
                dgvParser.Rows.Add(error.Fragment, error.Location, error.Description, error.AbsoluteIndex, error.Length);
            }
            if (errors.Count > 0) dgvParser.Rows.Add("Общее количество ошибок:", dgvParser.Rows.Count);
        }

        public void DisplayErrorsFlexBison(List<SyntaxError> errors)
        {
            dgvFlexBison.Rows.Clear();
            foreach (var error in errors)
            {
                dgvFlexBison.Rows.Add(error.Fragment, error.Location, error.Description, error.AbsoluteIndex, error.Length);
            }
            if (errors.Count > 0) dgvFlexBison.Rows.Add("Общее количество ошибок:", dgvFlexBison.Rows.Count);
        }

        public void DisplayErrorsAntlr(List<SyntaxError> errors)
        {
            dgvAntlr.Rows.Clear();
            foreach (var error in errors)
            {
                dgvAntlr.Rows.Add(error.Fragment, error.Location, error.Description, error.AbsoluteIndex, error.Length);
            }
            if (errors.Count > 0) dgvAntlr.Rows.Add("Общее количество ошибок:", dgvAntlr.Rows.Count);
        }

        private void dgvLexer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = dgvLexer.Rows[e.RowIndex];

            if ((int)row.Cells[0].Value != (int)TokenCodes.ERROR)
            {
                return;
            }

            var absIndex = (int)row.Cells[4].Value;
            var length = row.Cells[2].Value?.ToString()?.Length ?? 0;

            NavigateToErrorRequested?.Invoke(absIndex, length);
        }

        private void dgvParser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == dgvParser.Rows.Count - 1)
            {
                return;
            }

            var row = dgvParser.Rows[e.RowIndex];

            var absIndex = (int)row.Cells[3].Value;
            var length = (int)row.Cells[4].Value;

            NavigateToErrorRequested?.Invoke(absIndex, length);
        }

        private void dgvFlexBison_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == dgvFlexBison.Rows.Count - 1)
            {
                return;
            }

            var row = dgvFlexBison.Rows[e.RowIndex];

            var absIndex = (int)row.Cells[3].Value;
            var length = (int)row.Cells[4].Value;

            NavigateToErrorRequested?.Invoke(absIndex, length);
        }

        private void dgvAntlr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == dgvAntlr.Rows.Count - 1)
            {
                return;
            }

            var row = dgvAntlr.Rows[e.RowIndex];

            var absIndex = (int)row.Cells[3].Value;
            var length = (int)row.Cells[4].Value;

            NavigateToErrorRequested?.Invoke(absIndex, length);
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
