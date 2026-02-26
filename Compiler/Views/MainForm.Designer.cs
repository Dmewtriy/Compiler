namespace Compiler
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip = new MenuStrip();
            menu = new ToolStripMenuItem();
            menuCreate = new ToolStripMenuItem();
            menuOpen = new ToolStripMenuItem();
            menuSave = new ToolStripMenuItem();
            menuSaveAs = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            menuExit = new ToolStripMenuItem();
            editing = new ToolStripMenuItem();
            menuCancel = new ToolStripMenuItem();
            menuReturn = new ToolStripMenuItem();
            menuCut = new ToolStripMenuItem();
            menuCopy = new ToolStripMenuItem();
            menuPaste = new ToolStripMenuItem();
            menuDelete = new ToolStripMenuItem();
            menuSelectAll = new ToolStripMenuItem();
            text = new ToolStripMenuItem();
            menuTaskDescription = new ToolStripMenuItem();
            menuGrammar = new ToolStripMenuItem();
            menuGrammarClassification = new ToolStripMenuItem();
            menuAnalysisMethod = new ToolStripMenuItem();
            menuTestExample = new ToolStripMenuItem();
            menuReferences = new ToolStripMenuItem();
            menuSourceCode = new ToolStripMenuItem();
            run = new ToolStripMenuItem();
            reference = new ToolStripMenuItem();
            menuHelp = new ToolStripMenuItem();
            menuAbout = new ToolStripMenuItem();
            tools = new ToolStrip();
            toolCreate = new ToolStripButton();
            toolOpen = new ToolStripButton();
            toolSave = new ToolStripButton();
            toolCancel = new ToolStripButton();
            toolReturn = new ToolStripButton();
            toolCopy = new ToolStripButton();
            toolCut = new ToolStripButton();
            toolPaste = new ToolStripButton();
            toolRun = new ToolStripButton();
            toolAbout = new ToolStripButton();
            toolHelp = new ToolStripButton();
            statusStrip = new StatusStrip();
            FilePathStatusLabel = new ToolStripStatusLabel();
            mainPanel = new SplitContainer();
            lblPlaceholder = new Label();
            richTextBoxEdit = new RichTextBox();
            richTextBoxResult = new RichTextBox();
            menuStrip.SuspendLayout();
            tools.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainPanel).BeginInit();
            mainPanel.Panel1.SuspendLayout();
            mainPanel.Panel2.SuspendLayout();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.BackColor = SystemColors.Control;
            menuStrip.Items.AddRange(new ToolStripItem[] { menu, editing, text, run, reference });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(784, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // menu
            // 
            menu.DropDownItems.AddRange(new ToolStripItem[] { menuCreate, menuOpen, menuSave, menuSaveAs, toolStripSeparator1, menuExit });
            menu.Name = "menu";
            menu.Size = new Size(53, 20);
            menu.Text = "Меню";
            // 
            // menuCreate
            // 
            menuCreate.Name = "menuCreate";
            menuCreate.Size = new Size(153, 22);
            menuCreate.Text = "Создать";
            // 
            // menuOpen
            // 
            menuOpen.Name = "menuOpen";
            menuOpen.Size = new Size(153, 22);
            menuOpen.Text = "Открыть";
            // 
            // menuSave
            // 
            menuSave.Name = "menuSave";
            menuSave.Size = new Size(153, 22);
            menuSave.Text = "Сохранить";
            // 
            // menuSaveAs
            // 
            menuSaveAs.Name = "menuSaveAs";
            menuSaveAs.Size = new Size(153, 22);
            menuSaveAs.Text = "Сохранить как";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(150, 6);
            // 
            // menuExit
            // 
            menuExit.Name = "menuExit";
            menuExit.Size = new Size(153, 22);
            menuExit.Text = "Выход";
            // 
            // editing
            // 
            editing.DropDownItems.AddRange(new ToolStripItem[] { menuCancel, menuReturn, menuCut, menuCopy, menuPaste, menuDelete, menuSelectAll });
            editing.Name = "editing";
            editing.Size = new Size(59, 20);
            editing.Text = "Правка";
            // 
            // menuCancel
            // 
            menuCancel.Name = "menuCancel";
            menuCancel.Size = new Size(148, 22);
            menuCancel.Text = "Отменить";
            // 
            // menuReturn
            // 
            menuReturn.Name = "menuReturn";
            menuReturn.Size = new Size(148, 22);
            menuReturn.Text = "Вернуть";
            // 
            // menuCut
            // 
            menuCut.Name = "menuCut";
            menuCut.Size = new Size(148, 22);
            menuCut.Text = "Вырезать";
            // 
            // menuCopy
            // 
            menuCopy.Name = "menuCopy";
            menuCopy.Size = new Size(148, 22);
            menuCopy.Text = "Копировать";
            // 
            // menuPaste
            // 
            menuPaste.Name = "menuPaste";
            menuPaste.Size = new Size(148, 22);
            menuPaste.Text = "Вставить";
            // 
            // menuDelete
            // 
            menuDelete.Name = "menuDelete";
            menuDelete.Size = new Size(148, 22);
            menuDelete.Text = "Удалить";
            // 
            // menuSelectAll
            // 
            menuSelectAll.Name = "menuSelectAll";
            menuSelectAll.Size = new Size(148, 22);
            menuSelectAll.Text = "Выделить все";
            // 
            // text
            // 
            text.DropDownItems.AddRange(new ToolStripItem[] { menuTaskDescription, menuGrammar, menuGrammarClassification, menuAnalysisMethod, menuTestExample, menuReferences, menuSourceCode });
            text.Name = "text";
            text.Size = new Size(49, 20);
            text.Text = "Текст";
            // 
            // menuTaskDescription
            // 
            menuTaskDescription.Name = "menuTaskDescription";
            menuTaskDescription.Size = new Size(231, 22);
            menuTaskDescription.Text = "Постановка задачи";
            // 
            // menuGrammar
            // 
            menuGrammar.Name = "menuGrammar";
            menuGrammar.Size = new Size(231, 22);
            menuGrammar.Text = "Грамматика";
            // 
            // menuGrammarClassification
            // 
            menuGrammarClassification.Name = "menuGrammarClassification";
            menuGrammarClassification.Size = new Size(231, 22);
            menuGrammarClassification.Text = "Классификация грамматики";
            // 
            // menuAnalysisMethod
            // 
            menuAnalysisMethod.Name = "menuAnalysisMethod";
            menuAnalysisMethod.Size = new Size(231, 22);
            menuAnalysisMethod.Text = "Метод анализа";
            // 
            // menuTestExample
            // 
            menuTestExample.Name = "menuTestExample";
            menuTestExample.Size = new Size(231, 22);
            menuTestExample.Text = "Тестовый пример";
            // 
            // menuReferences
            // 
            menuReferences.Name = "menuReferences";
            menuReferences.Size = new Size(231, 22);
            menuReferences.Text = "Список литературы";
            // 
            // menuSourceCode
            // 
            menuSourceCode.Name = "menuSourceCode";
            menuSourceCode.Size = new Size(231, 22);
            menuSourceCode.Text = "Исходный код программы";
            // 
            // run
            // 
            run.Name = "run";
            run.Size = new Size(46, 20);
            run.Text = "Пуск";
            // 
            // reference
            // 
            reference.DropDownItems.AddRange(new ToolStripItem[] { menuHelp, menuAbout });
            reference.Name = "reference";
            reference.Size = new Size(65, 20);
            reference.Text = "Справка";
            // 
            // menuHelp
            // 
            menuHelp.Name = "menuHelp";
            menuHelp.Size = new Size(180, 22);
            menuHelp.Text = "Вызов справки";
            // 
            // menuAbout
            // 
            menuAbout.Name = "menuAbout";
            menuAbout.Size = new Size(180, 22);
            menuAbout.Text = "О программе";
            // 
            // tools
            // 
            tools.ImageScalingSize = new Size(32, 32);
            tools.Items.AddRange(new ToolStripItem[] { toolCreate, toolOpen, toolSave, toolCancel, toolReturn, toolCopy, toolCut, toolPaste, toolRun, toolAbout, toolHelp });
            tools.Location = new Point(0, 24);
            tools.Name = "tools";
            tools.Size = new Size(784, 39);
            tools.TabIndex = 1;
            tools.Text = "toolStrip1";
            // 
            // toolCreate
            // 
            toolCreate.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolCreate.Image = (Image)resources.GetObject("toolCreate.Image");
            toolCreate.ImageTransparentColor = Color.Magenta;
            toolCreate.Margin = new Padding(0, 1, 5, 2);
            toolCreate.Name = "toolCreate";
            toolCreate.Size = new Size(36, 36);
            toolCreate.Text = "Создать";
            toolCreate.ToolTipText = "Создать";
            // 
            // toolOpen
            // 
            toolOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolOpen.Image = (Image)resources.GetObject("toolOpen.Image");
            toolOpen.ImageTransparentColor = Color.Magenta;
            toolOpen.Margin = new Padding(0, 1, 5, 2);
            toolOpen.Name = "toolOpen";
            toolOpen.Size = new Size(36, 36);
            toolOpen.Text = "Открыть";
            // 
            // toolSave
            // 
            toolSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolSave.Image = (Image)resources.GetObject("toolSave.Image");
            toolSave.ImageTransparentColor = Color.Magenta;
            toolSave.Margin = new Padding(0, 1, 40, 2);
            toolSave.Name = "toolSave";
            toolSave.Size = new Size(36, 36);
            toolSave.Text = "Сохранить";
            // 
            // toolCancel
            // 
            toolCancel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolCancel.Image = (Image)resources.GetObject("toolCancel.Image");
            toolCancel.ImageTransparentColor = Color.Magenta;
            toolCancel.Margin = new Padding(0, 1, 5, 2);
            toolCancel.Name = "toolCancel";
            toolCancel.Size = new Size(36, 36);
            toolCancel.Text = "Отменить";
            // 
            // toolReturn
            // 
            toolReturn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolReturn.Image = (Image)resources.GetObject("toolReturn.Image");
            toolReturn.ImageTransparentColor = Color.Magenta;
            toolReturn.Margin = new Padding(0, 1, 40, 2);
            toolReturn.Name = "toolReturn";
            toolReturn.Size = new Size(36, 36);
            toolReturn.Text = "Вернуть";
            // 
            // toolCopy
            // 
            toolCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolCopy.Image = (Image)resources.GetObject("toolCopy.Image");
            toolCopy.ImageTransparentColor = Color.Magenta;
            toolCopy.Margin = new Padding(0, 1, 5, 2);
            toolCopy.Name = "toolCopy";
            toolCopy.Size = new Size(36, 36);
            toolCopy.Text = "Копировать";
            // 
            // toolCut
            // 
            toolCut.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolCut.Image = (Image)resources.GetObject("toolCut.Image");
            toolCut.ImageTransparentColor = Color.Magenta;
            toolCut.Margin = new Padding(0, 1, 5, 2);
            toolCut.Name = "toolCut";
            toolCut.Size = new Size(36, 36);
            toolCut.Text = "Вырезать";
            // 
            // toolPaste
            // 
            toolPaste.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolPaste.Image = (Image)resources.GetObject("toolPaste.Image");
            toolPaste.ImageTransparentColor = Color.Magenta;
            toolPaste.Margin = new Padding(0, 1, 80, 2);
            toolPaste.Name = "toolPaste";
            toolPaste.Size = new Size(36, 36);
            toolPaste.Text = "Вставить";
            // 
            // toolRun
            // 
            toolRun.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolRun.Image = (Image)resources.GetObject("toolRun.Image");
            toolRun.ImageTransparentColor = Color.Magenta;
            toolRun.Name = "toolRun";
            toolRun.Size = new Size(36, 36);
            toolRun.Text = "Пуск";
            // 
            // toolAbout
            // 
            toolAbout.Alignment = ToolStripItemAlignment.Right;
            toolAbout.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolAbout.Image = (Image)resources.GetObject("toolAbout.Image");
            toolAbout.ImageTransparentColor = Color.Magenta;
            toolAbout.Name = "toolAbout";
            toolAbout.Size = new Size(36, 36);
            toolAbout.Text = "О программе";
            // 
            // toolHelp
            // 
            toolHelp.Alignment = ToolStripItemAlignment.Right;
            toolHelp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolHelp.Image = (Image)resources.GetObject("toolHelp.Image");
            toolHelp.ImageTransparentColor = Color.Magenta;
            toolHelp.Name = "toolHelp";
            toolHelp.Size = new Size(36, 36);
            toolHelp.Text = "Вызов справки";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { FilePathStatusLabel });
            statusStrip.Location = new Point(0, 439);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(784, 22);
            statusStrip.TabIndex = 3;
            statusStrip.Text = "statusStrip1";
            // 
            // FilePathStatusLabel
            // 
            FilePathStatusLabel.Name = "FilePathStatusLabel";
            FilePathStatusLabel.Size = new Size(118, 17);
            FilePathStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // mainPanel
            // 
            mainPanel.BorderStyle = BorderStyle.Fixed3D;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FixedPanel = FixedPanel.Panel2;
            mainPanel.Location = new Point(0, 63);
            mainPanel.Name = "mainPanel";
            mainPanel.Orientation = Orientation.Horizontal;
            // 
            // mainPanel.Panel1
            // 
            mainPanel.Panel1.Controls.Add(lblPlaceholder);
            mainPanel.Panel1.Controls.Add(richTextBoxEdit);
            // 
            // mainPanel.Panel2
            // 
            mainPanel.Panel2.Controls.Add(richTextBoxResult);
            mainPanel.Size = new Size(784, 376);
            mainPanel.SplitterDistance = 238;
            mainPanel.TabIndex = 2;
            // 
            // lblPlaceholder
            // 
            lblPlaceholder.AutoSize = true;
            lblPlaceholder.Dock = DockStyle.Fill;
            lblPlaceholder.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblPlaceholder.Location = new Point(0, 0);
            lblPlaceholder.Name = "lblPlaceholder";
            lblPlaceholder.Size = new Size(464, 25);
            lblPlaceholder.TabIndex = 2;
            lblPlaceholder.Text = "Создайте или откройте файл, чтобы начать работу";
            lblPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // richTextBoxEdit
            // 
            richTextBoxEdit.AllowDrop = true;
            richTextBoxEdit.BorderStyle = BorderStyle.None;
            richTextBoxEdit.Dock = DockStyle.Fill;
            richTextBoxEdit.Font = new Font("Segoe UI", 12F);
            richTextBoxEdit.Location = new Point(0, 0);
            richTextBoxEdit.Margin = new Padding(0);
            richTextBoxEdit.Name = "richTextBoxEdit";
            richTextBoxEdit.Size = new Size(780, 234);
            richTextBoxEdit.TabIndex = 1;
            richTextBoxEdit.Text = "";
            richTextBoxEdit.WordWrap = false;
            // 
            // richTextBoxResult
            // 
            richTextBoxResult.BackColor = Color.White;
            richTextBoxResult.BorderStyle = BorderStyle.None;
            richTextBoxResult.Dock = DockStyle.Fill;
            richTextBoxResult.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            richTextBoxResult.ForeColor = Color.Black;
            richTextBoxResult.Location = new Point(0, 0);
            richTextBoxResult.Margin = new Padding(0);
            richTextBoxResult.Name = "richTextBoxResult";
            richTextBoxResult.ReadOnly = true;
            richTextBoxResult.Size = new Size(780, 130);
            richTextBoxResult.TabIndex = 1;
            richTextBoxResult.Text = ">";
            richTextBoxResult.WordWrap = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Controls.Add(mainPanel);
            Controls.Add(statusStrip);
            Controls.Add(tools);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            MinimumSize = new Size(800, 500);
            Name = "MainForm";
            Text = "Компилятор";
            Load += MainForm_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            tools.ResumeLayout(false);
            tools.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            mainPanel.Panel1.ResumeLayout(false);
            mainPanel.Panel1.PerformLayout();
            mainPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainPanel).EndInit();
            mainPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem menu;
        private ToolStripMenuItem menuCreate;
        private ToolStripMenuItem menuOpen;
        private ToolStripMenuItem menuSave;
        private ToolStripMenuItem menuSaveAs;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuExit;
        private ToolStripMenuItem editing;
        private ToolStripMenuItem menuCancel;
        private ToolStripMenuItem menuReturn;
        private ToolStripMenuItem menuCut;
        private ToolStripMenuItem menuCopy;
        private ToolStripMenuItem menuPaste;
        private ToolStripMenuItem menuDelete;
        private ToolStripMenuItem menuSelectAll;
        private ToolStripMenuItem text;
        private ToolStripMenuItem menuTaskDescription;
        private ToolStripMenuItem menuGrammar;
        private ToolStripMenuItem menuGrammarClassification;
        private ToolStripMenuItem menuAnalysisMethod;
        private ToolStripMenuItem menuTestExample;
        private ToolStripMenuItem menuReferences;
        private ToolStripMenuItem menuSourceCode;
        private ToolStripMenuItem run;
        private ToolStripMenuItem reference;
        private ToolStripMenuItem menuHelp;
        private ToolStripMenuItem menuAbout;
        private ToolStrip tools;
        private ToolStripButton toolCreate;
        private ToolStripButton toolOpen;
        private ToolStripButton toolSave;
        private ToolStripButton toolCancel;
        private ToolStripButton toolReturn;
        private ToolStripButton toolCopy;
        private ToolStripButton toolCut;
        private ToolStripButton toolPaste;
        private ToolStripButton toolRun;
        private ToolStripButton toolAbout;
        private ToolStripButton toolHelp;
        private SplitContainer mainPanel;
        private RichTextBox richTextBoxEdit;
        private RichTextBox richTextBoxResult;
        private Label lblPlaceholder;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel FilePathStatusLabel;
    }
}
