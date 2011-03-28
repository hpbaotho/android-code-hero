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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tool_Cleanup));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbBeginScanProject = new System.Windows.Forms.ToolStripButton();
            this.tsbCheck = new System.Windows.Forms.ToolStripButton();
            this.tsbUncheck = new System.Windows.Forms.ToolStripButton();
            this.tsbFilter = new System.Windows.Forms.ToolStripButton();
            this.dgvAllResources = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllResources)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBeginScanProject,
            this.tsbCheck,
            this.tsbUncheck,
            this.tsbFilter});
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
            // tsbFilter
            // 
            this.tsbFilter.CheckOnClick = true;
            this.tsbFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsbFilter.Image")));
            this.tsbFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFilter.Name = "tsbFilter";
            this.tsbFilter.Size = new System.Drawing.Size(23, 22);
            this.tsbFilter.Text = "toolStripButton1";
            this.tsbFilter.Click += new System.EventHandler(this.tsbFilter_Click);
            // 
            // dgvAllResources
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvAllResources.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAllResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllResources.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAllResources.Location = new System.Drawing.Point(0, 25);
            this.dgvAllResources.Name = "dgvAllResources";
            this.dgvAllResources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllResources.Size = new System.Drawing.Size(546, 467);
            this.dgvAllResources.TabIndex = 3;
            // 
            // Tool_Cleanup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvAllResources);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Tool_Cleanup";
            this.Size = new System.Drawing.Size(546, 492);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllResources)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbBeginScanProject;
        private System.Windows.Forms.ToolStripButton tsbCheck;
        private System.Windows.Forms.ToolStripButton tsbUncheck;
        private System.Windows.Forms.ToolStripButton tsbFilter;
        private System.Windows.Forms.DataGridView dgvAllResources;
    }
}
