namespace AndroidCodeHero
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Project");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.tvAndroidProject = new System.Windows.Forms.TreeView();
            this.cmtTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCreateSocketEngine = new System.Windows.Forms.ToolStripMenuItem();
            this.fbdAndroidProject = new System.Windows.Forms.FolderBrowserDialog();
            this.tslAssistingAndroidProject = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsbAssistAndroidProject = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbGenerateCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.cmtTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(958, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAssistAndroidProject,
            this.toolStripSeparator1,
            this.tsbSave,
            this.toolStripSeparator2,
            this.tsbGenerateCode});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(958, 31);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslAssistingAndroidProject});
            this.statusStrip1.Location = new System.Drawing.Point(0, 504);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(958, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 55);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.tvAndroidProject);
            this.splitMain.Size = new System.Drawing.Size(958, 449);
            this.splitMain.SplitterDistance = 202;
            this.splitMain.TabIndex = 3;
            // 
            // tvAndroidProject
            // 
            this.tvAndroidProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvAndroidProject.LabelEdit = true;
            this.tvAndroidProject.Location = new System.Drawing.Point(0, 0);
            this.tvAndroidProject.Name = "tvAndroidProject";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Project";
            this.tvAndroidProject.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvAndroidProject.ShowLines = false;
            this.tvAndroidProject.ShowPlusMinus = false;
            this.tvAndroidProject.Size = new System.Drawing.Size(202, 449);
            this.tvAndroidProject.TabIndex = 0;
            // 
            // cmtTreeView
            // 
            this.cmtTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCreateSocketEngine});
            this.cmtTreeView.Name = "cmtTreeView";
            this.cmtTreeView.Size = new System.Drawing.Size(148, 26);
            // 
            // tsmiCreateSocketEngine
            // 
            this.tsmiCreateSocketEngine.Name = "tsmiCreateSocketEngine";
            this.tsmiCreateSocketEngine.Size = new System.Drawing.Size(147, 22);
            this.tsmiCreateSocketEngine.Text = "Create Engine";
            // 
            // fbdAndroidProject
            // 
            this.fbdAndroidProject.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // tslAssistingAndroidProject
            // 
            this.tslAssistingAndroidProject.Image = global::AndroidCodeHero.Properties.Resources.assistandroidproject;
            this.tslAssistingAndroidProject.Name = "tslAssistingAndroidProject";
            this.tslAssistingAndroidProject.Size = new System.Drawing.Size(177, 16);
            this.tslAssistingAndroidProject.Text = "Assisting Android Project : ....";
            // 
            // tsbAssistAndroidProject
            // 
            this.tsbAssistAndroidProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAssistAndroidProject.Image = global::AndroidCodeHero.Properties.Resources.assistandroidproject;
            this.tsbAssistAndroidProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAssistAndroidProject.Name = "tsbAssistAndroidProject";
            this.tsbAssistAndroidProject.Size = new System.Drawing.Size(28, 28);
            this.tsbAssistAndroidProject.Text = "Assist Android Project";
            this.tsbAssistAndroidProject.Click += new System.EventHandler(this.tsbAssistAndroidProject_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::AndroidCodeHero.Properties.Resources.Save;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(28, 28);
            this.tsbSave.Text = "Save";
            // 
            // tsbGenerateCode
            // 
            this.tsbGenerateCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGenerateCode.Image = global::AndroidCodeHero.Properties.Resources.cog;
            this.tsbGenerateCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGenerateCode.Name = "tsbGenerateCode";
            this.tsbGenerateCode.Size = new System.Drawing.Size(28, 28);
            this.tsbGenerateCode.Text = "Generate Code";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 526);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Android Code Hero";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.ResumeLayout(false);
            this.cmtTreeView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslAssistingAndroidProject;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TreeView tvAndroidProject;
        private System.Windows.Forms.ContextMenuStrip cmtTreeView;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateSocketEngine;
        private System.Windows.Forms.ToolStripButton tsbAssistAndroidProject;
        private System.Windows.Forms.FolderBrowserDialog fbdAndroidProject;
        private System.Windows.Forms.ToolStripButton tsbGenerateCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

