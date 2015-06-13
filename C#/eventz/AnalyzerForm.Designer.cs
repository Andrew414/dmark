namespace eventz
{
    partial class AnalyzerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalyzerForm));
            this.highGroup = new ComponentOwl.BetterListView.BetterListViewGroup();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.captureMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.openButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.filterBox = new System.Windows.Forms.ToolStripTextBox();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.eventMorePanel = new System.Windows.Forms.Panel();
            this.eventMoreBox = new System.Windows.Forms.GroupBox();
            this.eventMore = new System.Windows.Forms.ListView();
            this.eventSplitter = new System.Windows.Forms.Splitter();
            this.eventPanel = new System.Windows.Forms.Panel();
            this.eventsBox = new System.Windows.Forms.GroupBox();
            this.eventView = new ComponentOwl.BetterListView.BetterListView();
            this.timeHeader = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.processNameHeader = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.pidHeader = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.ppidHeader = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.operationHeader = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.pathHeader = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.tidHeader = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.imgPathHeader = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.flagsHeader = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.usernameHeader = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.accidentsBox = new System.Windows.Forms.GroupBox();
            this.accidentsView = new ComponentOwl.BetterListView.BetterListView();
            this.mediumSeverity = new ComponentOwl.BetterListView.BetterListViewGroup();
            this.lowGroup = new ComponentOwl.BetterListView.BetterListViewGroup();
            this.otherGroup = new ComponentOwl.BetterListView.BetterListViewGroup();
            this.verticalSplitter = new System.Windows.Forms.Splitter();
            this.openLog = new System.Windows.Forms.OpenFileDialog();
            this.mainMenu.SuspendLayout();
            this.toolMenu.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.eventMorePanel.SuspendLayout();
            this.eventMoreBox.SuspendLayout();
            this.eventPanel.SuspendLayout();
            this.eventsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventView)).BeginInit();
            this.rightPanel.SuspendLayout();
            this.accidentsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accidentsView)).BeginInit();
            this.SuspendLayout();
            // 
            // highGroup
            // 
            this.highGroup.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.highGroup.Header = "High Severity";
            this.highGroup.Name = "highGroup";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.captureMenu,
            this.helpMenu});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(803, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "File";
            this.fileMenu.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // captureMenu
            // 
            this.captureMenu.Name = "captureMenu";
            this.captureMenu.Size = new System.Drawing.Size(61, 20);
            this.captureMenu.Text = "Capture";
            this.captureMenu.Click += new System.EventHandler(this.captureToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "Help";
            this.helpMenu.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolMenu
            // 
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.saveButton,
            this.filterBox});
            this.toolMenu.Location = new System.Drawing.Point(0, 24);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Size = new System.Drawing.Size(803, 25);
            this.toolMenu.TabIndex = 1;
            this.toolMenu.Text = "toolStrip1";
            this.toolMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // openButton
            // 
            this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openButton.Image = ((System.Drawing.Image)(resources.GetObject("openButton.Image")));
            this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(23, 22);
            this.openButton.Text = "toolStripButton1";
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(23, 22);
            this.saveButton.Text = "toolStripButton2";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // filterBox
            // 
            this.filterBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filterBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterBox.Name = "filterBox";
            this.filterBox.Size = new System.Drawing.Size(100, 25);
            this.filterBox.Text = "filter...";
            this.filterBox.TextChanged += new System.EventHandler(this.filterBox_TextChanged);
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.leftPanel.Controls.Add(this.eventMorePanel);
            this.leftPanel.Controls.Add(this.eventSplitter);
            this.leftPanel.Controls.Add(this.eventPanel);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 49);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(600, 338);
            this.leftPanel.TabIndex = 2;
            // 
            // eventMorePanel
            // 
            this.eventMorePanel.Controls.Add(this.eventMoreBox);
            this.eventMorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventMorePanel.Location = new System.Drawing.Point(3, 338);
            this.eventMorePanel.Name = "eventMorePanel";
            this.eventMorePanel.Size = new System.Drawing.Size(597, 0);
            this.eventMorePanel.TabIndex = 0;
            // 
            // eventMoreBox
            // 
            this.eventMoreBox.Controls.Add(this.eventMore);
            this.eventMoreBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventMoreBox.Location = new System.Drawing.Point(0, 0);
            this.eventMoreBox.Name = "eventMoreBox";
            this.eventMoreBox.Size = new System.Drawing.Size(597, 0);
            this.eventMoreBox.TabIndex = 0;
            this.eventMoreBox.TabStop = false;
            this.eventMoreBox.Text = "Event properties";
            // 
            // eventMore
            // 
            this.eventMore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventMore.Location = new System.Drawing.Point(3, 16);
            this.eventMore.Name = "eventMore";
            this.eventMore.Size = new System.Drawing.Size(591, 0);
            this.eventMore.TabIndex = 0;
            this.eventMore.UseCompatibleStateImageBehavior = false;
            // 
            // eventSplitter
            // 
            this.eventSplitter.Location = new System.Drawing.Point(0, 338);
            this.eventSplitter.Name = "eventSplitter";
            this.eventSplitter.Size = new System.Drawing.Size(3, 0);
            this.eventSplitter.TabIndex = 1;
            this.eventSplitter.TabStop = false;
            // 
            // eventPanel
            // 
            this.eventPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.eventPanel.Controls.Add(this.eventsBox);
            this.eventPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.eventPanel.Location = new System.Drawing.Point(0, 0);
            this.eventPanel.Name = "eventPanel";
            this.eventPanel.Size = new System.Drawing.Size(600, 338);
            this.eventPanel.TabIndex = 0;
            // 
            // eventsBox
            // 
            this.eventsBox.Controls.Add(this.eventView);
            this.eventsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventsBox.Location = new System.Drawing.Point(0, 0);
            this.eventsBox.Name = "eventsBox";
            this.eventsBox.Size = new System.Drawing.Size(600, 338);
            this.eventsBox.TabIndex = 0;
            this.eventsBox.TabStop = false;
            this.eventsBox.Text = "Events";
            // 
            // eventView
            // 
            this.eventView.Columns.Add(this.timeHeader);
            this.eventView.Columns.Add(this.processNameHeader);
            this.eventView.Columns.Add(this.pidHeader);
            this.eventView.Columns.Add(this.ppidHeader);
            this.eventView.Columns.Add(this.operationHeader);
            this.eventView.Columns.Add(this.pathHeader);
            this.eventView.Columns.Add(this.tidHeader);
            this.eventView.Columns.Add(this.imgPathHeader);
            this.eventView.Columns.Add(this.flagsHeader);
            this.eventView.Columns.Add(this.usernameHeader);
            this.eventView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventView.Location = new System.Drawing.Point(3, 16);
            this.eventView.Name = "eventView";
            this.eventView.Size = new System.Drawing.Size(594, 319);
            this.eventView.TabIndex = 0;
            this.eventView.SelectedIndexChanged += new System.EventHandler(this.eventView_SelectedIndexChanged);
            // 
            // timeHeader
            // 
            this.timeHeader.Name = "timeHeader";
            this.timeHeader.Text = "Time";
            // 
            // processNameHeader
            // 
            this.processNameHeader.Name = "processNameHeader";
            this.processNameHeader.Text = "Process Name";
            // 
            // pidHeader
            // 
            this.pidHeader.Name = "pidHeader";
            this.pidHeader.Text = "Process ID";
            // 
            // ppidHeader
            // 
            this.ppidHeader.Name = "ppidHeader";
            this.ppidHeader.Text = "Parend PID";
            // 
            // operationHeader
            // 
            this.operationHeader.Name = "operationHeader";
            this.operationHeader.Text = "Operation";
            // 
            // pathHeader
            // 
            this.pathHeader.Name = "pathHeader";
            this.pathHeader.Text = "Path";
            // 
            // tidHeader
            // 
            this.tidHeader.Name = "tidHeader";
            this.tidHeader.Text = "Thread ID";
            // 
            // imgPathHeader
            // 
            this.imgPathHeader.Name = "imgPathHeader";
            this.imgPathHeader.Text = "Process path";
            // 
            // flagsHeader
            // 
            this.flagsHeader.Name = "flagsHeader";
            this.flagsHeader.Text = "Flags";
            // 
            // usernameHeader
            // 
            this.usernameHeader.Name = "usernameHeader";
            this.usernameHeader.Text = "Username";
            // 
            // rightPanel
            // 
            this.rightPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rightPanel.Controls.Add(this.accidentsBox);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(600, 49);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(203, 338);
            this.rightPanel.TabIndex = 4;
            // 
            // accidentsBox
            // 
            this.accidentsBox.Controls.Add(this.accidentsView);
            this.accidentsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accidentsBox.Location = new System.Drawing.Point(0, 0);
            this.accidentsBox.Name = "accidentsBox";
            this.accidentsBox.Size = new System.Drawing.Size(203, 338);
            this.accidentsBox.TabIndex = 0;
            this.accidentsBox.TabStop = false;
            this.accidentsBox.Text = "Accidents";
            // 
            // accidentsView
            // 
            this.accidentsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accidentsView.FontItems = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.accidentsView.GridLines = ComponentOwl.BetterListView.BetterListViewGridLines.None;
            this.accidentsView.GroupHeaderBehavior = ((ComponentOwl.BetterListView.BetterListViewGroupHeaderBehavior)((ComponentOwl.BetterListView.BetterListViewGroupHeaderBehavior.KeyboardFocus | ComponentOwl.BetterListView.BetterListViewGroupHeaderBehavior.MouseHighlight)));
            this.accidentsView.Groups.Add(this.highGroup);
            this.accidentsView.Groups.Add(this.mediumSeverity);
            this.accidentsView.Groups.Add(this.lowGroup);
            this.accidentsView.Groups.Add(this.otherGroup);
            this.accidentsView.HScrollBarDisplayMode = ComponentOwl.BetterListView.BetterListViewScrollBarDisplayMode.Hide;
            this.accidentsView.Location = new System.Drawing.Point(3, 16);
            this.accidentsView.MaximumSize = new System.Drawing.Size(500, 500);
            this.accidentsView.Name = "accidentsView";
            this.accidentsView.ShowEmptyGroups = true;
            this.accidentsView.ShowGroups = true;
            //this.accidentsView.Size = new System.Drawing.Size(197, 319);
            this.accidentsView.TabIndex = 0;
            // 
            // mediumSeverity
            // 
            this.mediumSeverity.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.mediumSeverity.Header = "Medium Severity";
            this.mediumSeverity.Name = "mediumSeverity";
            // 
            // lowGroup
            // 
            this.lowGroup.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lowGroup.Header = "Low Severity";
            this.lowGroup.Name = "lowGroup";
            // 
            // otherGroup
            // 
            this.otherGroup.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.otherGroup.Header = "Other";
            this.otherGroup.Name = "otherGroup";
            // 
            // verticalSplitter
            // 
            this.verticalSplitter.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.verticalSplitter.Location = new System.Drawing.Point(600, 49);
            this.verticalSplitter.Name = "verticalSplitter";
            this.verticalSplitter.Size = new System.Drawing.Size(3, 338);
            this.verticalSplitter.TabIndex = 5;
            this.verticalSplitter.TabStop = false;
            this.verticalSplitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.verticalSplitter_SplitterMoved);
            // 
            // openLog
            // 
            this.openLog.FileName = "LogFile.CSV";
            this.openLog.Filter = "Process Monitor CSV logs (*.csv)|*.csv";
            // 
            // AnalyzerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 387);
            this.Controls.Add(this.verticalSplitter);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.toolMenu);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "AnalyzerForm";
            this.Text = "Events";
            this.Load += new System.EventHandler(this.AnalyzerForm_Load);
            this.Resize += new System.EventHandler(this.AnalyzerForm_Resize);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            this.leftPanel.ResumeLayout(false);
            this.eventMorePanel.ResumeLayout(false);
            this.eventMoreBox.ResumeLayout(false);
            this.eventPanel.ResumeLayout(false);
            this.eventsBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventView)).EndInit();
            this.rightPanel.ResumeLayout(false);
            this.accidentsBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accidentsView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem captureMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.ToolStripButton openButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Splitter verticalSplitter;
        private System.Windows.Forms.Panel eventPanel;
        private System.Windows.Forms.Splitter eventSplitter;
        private System.Windows.Forms.Panel eventMorePanel;
        private System.Windows.Forms.GroupBox eventMoreBox;
        private System.Windows.Forms.GroupBox eventsBox;
        private System.Windows.Forms.GroupBox accidentsBox;
        private System.Windows.Forms.ListView eventMore;
        private ComponentOwl.BetterListView.BetterListView eventView;
        private ComponentOwl.BetterListView.BetterListView accidentsView;
        private ComponentOwl.BetterListView.BetterListViewGroup highGroup;
        private ComponentOwl.BetterListView.BetterListViewGroup mediumSeverity;
        private ComponentOwl.BetterListView.BetterListViewGroup lowGroup;
        private ComponentOwl.BetterListView.BetterListViewGroup otherGroup;
        private ComponentOwl.BetterListView.BetterListViewColumnHeader timeHeader;
        private ComponentOwl.BetterListView.BetterListViewColumnHeader processNameHeader;
        private ComponentOwl.BetterListView.BetterListViewColumnHeader pidHeader;
        private ComponentOwl.BetterListView.BetterListViewColumnHeader operationHeader;
        private ComponentOwl.BetterListView.BetterListViewColumnHeader pathHeader;
        private System.Windows.Forms.OpenFileDialog openLog;
        private ComponentOwl.BetterListView.BetterListViewColumnHeader ppidHeader;
        private ComponentOwl.BetterListView.BetterListViewColumnHeader tidHeader;
        private ComponentOwl.BetterListView.BetterListViewColumnHeader imgPathHeader;
        private ComponentOwl.BetterListView.BetterListViewColumnHeader flagsHeader;
        private ComponentOwl.BetterListView.BetterListViewColumnHeader usernameHeader;
        private System.Windows.Forms.ToolStripTextBox filterBox;
    }
}

