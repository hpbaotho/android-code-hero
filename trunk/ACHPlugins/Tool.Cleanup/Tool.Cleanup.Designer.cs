namespace ACHPlugin
{
    partial class Tool_Cleanup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tool_Cleanup));
            this.tabcScanResult = new System.Windows.Forms.TabControl();
            this.tabpUnusedResources = new System.Windows.Forms.TabPage();
            this.dgvUnusedResources = new System.Windows.Forms.DataGridView();
            this.tabpUsedResources = new System.Windows.Forms.TabPage();
            this.dgvUsedResources = new System.Windows.Forms.DataGridView();
            this.tabpAllResources = new System.Windows.Forms.TabPage();
            this.dgvAllResources = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbBeginScanProject = new System.Windows.Forms.ToolStripButton();
            this.tsbCheck = new System.Windows.Forms.ToolStripButton();
            this.tsbUncheck = new System.Windows.Forms.ToolStripButton();
            this.tabcScanResult.SuspendLayout();
            this.tabpUnusedResources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnusedResources)).BeginInit();
            this.tabpUsedResources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedResources)).BeginInit();
            this.tabpAllResources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllResources)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabcScanResult
            // 
            this.tabcScanResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabcScanResult.Controls.Add(this.tabpUnusedResources);
            this.tabcScanResult.Controls.Add(this.tabpUsedResources);
            this.tabcScanResult.Controls.Add(this.tabpAllResources);
            this.tabcScanResult.Location = new System.Drawing.Point(3, 28);
            this.tabcScanResult.Name = "tabcScanResult";
            this.tabcScanResult.SelectedIndex = 0;
            this.tabcScanResult.Size = new System.Drawing.Size(541, 461);
            this.tabcScanResult.TabIndex = 1;
            // 
            // tabpUnusedResources
            // 
            this.tabpUnusedResources.Controls.Add(this.dgvUnusedResources);
            this.tabpUnusedResources.Location = new System.Drawing.Point(4, 22);
            this.tabpUnusedResources.Name = "tabpUnusedResources";
            this.tabpUnusedResources.Padding = new System.Windows.Forms.Padding(3);
            this.tabpUnusedResources.Size = new System.Drawing.Size(533, 435);
            this.tabpUnusedResources.TabIndex = 0;
            this.tabpUnusedResources.Text = "Unused Resources";
            this.tabpUnusedResources.UseVisualStyleBackColor = true;
            // 
            // dgvUnusedResources
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvUnusedResources.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUnusedResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnusedResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnusedResources.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvUnusedResources.Location = new System.Drawing.Point(3, 3);
            this.dgvUnusedResources.Name = "dgvUnusedResources";
            this.dgvUnusedResources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUnusedResources.Size = new System.Drawing.Size(527, 429);
            this.dgvUnusedResources.TabIndex = 2;
            this.dgvUnusedResources.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUnusedResources_CellClick);
            this.dgvUnusedResources.SelectionChanged += new System.EventHandler(this.dgvUnusedResources_SelectionChanged);
            // 
            // tabpUsedResources
            // 
            this.tabpUsedResources.Controls.Add(this.dgvUsedResources);
            this.tabpUsedResources.Location = new System.Drawing.Point(4, 22);
            this.tabpUsedResources.Name = "tabpUsedResources";
            this.tabpUsedResources.Padding = new System.Windows.Forms.Padding(3);
            this.tabpUsedResources.Size = new System.Drawing.Size(533, 422);
            this.tabpUsedResources.TabIndex = 1;
            this.tabpUsedResources.Text = "Used Resources";
            this.tabpUsedResources.UseVisualStyleBackColor = true;
            // 
            // dgvUsedResources
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvUsedResources.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsedResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsedResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsedResources.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvUsedResources.Location = new System.Drawing.Point(3, 3);
            this.dgvUsedResources.Name = "dgvUsedResources";
            this.dgvUsedResources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsedResources.Size = new System.Drawing.Size(527, 416);
            this.dgvUsedResources.TabIndex = 1;
            // 
            // tabpAllResources
            // 
            this.tabpAllResources.Controls.Add(this.dgvAllResources);
            this.tabpAllResources.Location = new System.Drawing.Point(4, 22);
            this.tabpAllResources.Name = "tabpAllResources";
            this.tabpAllResources.Size = new System.Drawing.Size(533, 435);
            this.tabpAllResources.TabIndex = 2;
            this.tabpAllResources.Text = "All Resources";
            this.tabpAllResources.UseVisualStyleBackColor = true;
            // 
            // dgvAllResources
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvAllResources.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAllResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllResources.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAllResources.Location = new System.Drawing.Point(0, 0);
            this.dgvAllResources.Name = "dgvAllResources";
            this.dgvAllResources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllResources.Size = new System.Drawing.Size(533, 435);
            this.dgvAllResources.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBeginScanProject,
            this.tsbCheck,
            this.tsbUncheck});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(546, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbBeginScanProject
            // 
            this.tsbBeginScanProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBeginScanProject.Image = ((System.Drawing.Image)(resources.GetObject("tsbBeginScanProject.Image")));
            this.tsbBeginScanProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBeginScanProject.Name = "tsbBeginScanProject";
            this.tsbBeginScanProject.Size = new System.Drawing.Size(23, 22);
            this.tsbBeginScanProject.Text = "toolStripButton1";
            this.tsbBeginScanProject.Click += new System.EventHandler(this.tsbBeginScanProject_Click);
            // 
            // tsbCheck
            // 
            this.tsbCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCheck.Enabled = false;
            this.tsbCheck.Image = ((System.Drawing.Image)(resources.GetObject("tsbCheck.Image")));
            this.tsbCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheck.Name = "tsbCheck";
            this.tsbCheck.Size = new System.Drawing.Size(23, 22);
            this.tsbCheck.Text = "toolStripButton1";
            this.tsbCheck.Click += new System.EventHandler(this.tsbCheck_Click);
            // 
            // tsbUncheck
            // 
            this.tsbUncheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUncheck.Enabled = false;
            this.tsbUncheck.Image = ((System.Drawing.Image)(resources.GetObject("tsbUncheck.Image")));
            this.tsbUncheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUncheck.Name = "tsbUncheck";
            this.tsbUncheck.Size = new System.Drawing.Size(23, 22);
            this.tsbUncheck.Text = "toolStripButton2";
            this.tsbUncheck.Click += new System.EventHandler(this.tsbUncheck_Click);
            // 
            // Tool_Cleanup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabcScanResult);
            this.Name = "Tool_Cleanup";
            this.Size = new System.Drawing.Size(546, 492);
            this.tabcScanResult.ResumeLayout(false);
            this.tabpUnusedResources.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnusedResources)).EndInit();
            this.tabpUsedResources.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedResources)).EndInit();
            this.tabpAllResources.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllResources)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabcScanResult;
        private System.Windows.Forms.TabPage tabpUnusedResources;
        private System.Windows.Forms.TabPage tabpUsedResources;
        private System.Windows.Forms.TabPage tabpAllResources;
        private System.Windows.Forms.DataGridView dgvAllResources;
        private System.Windows.Forms.DataGridView dgvUsedResources;
        private System.Windows.Forms.DataGridView dgvUnusedResources;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbBeginScanProject;
        private System.Windows.Forms.ToolStripButton tsbCheck;
        private System.Windows.Forms.ToolStripButton tsbUncheck;
    }
}
