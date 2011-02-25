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
            this.btnScanAndroidProject = new System.Windows.Forms.Button();
            this.tabcScanResult = new System.Windows.Forms.TabControl();
            this.tabpUnusedResources = new System.Windows.Forms.TabPage();
            this.tabpUsedResources = new System.Windows.Forms.TabPage();
            this.tabpAllResources = new System.Windows.Forms.TabPage();
            this.dgvAllResources = new System.Windows.Forms.DataGridView();
            this.dgvUsedResources = new System.Windows.Forms.DataGridView();
            this.dgvUnusedResources = new System.Windows.Forms.DataGridView();
            this.tabcScanResult.SuspendLayout();
            this.tabpUnusedResources.SuspendLayout();
            this.tabpUsedResources.SuspendLayout();
            this.tabpAllResources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllResources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedResources)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnusedResources)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScanAndroidProject
            // 
            this.btnScanAndroidProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScanAndroidProject.Location = new System.Drawing.Point(3, 3);
            this.btnScanAndroidProject.Name = "btnScanAndroidProject";
            this.btnScanAndroidProject.Size = new System.Drawing.Size(537, 32);
            this.btnScanAndroidProject.TabIndex = 0;
            this.btnScanAndroidProject.Text = "Scan Android Project";
            this.btnScanAndroidProject.UseVisualStyleBackColor = true;
            this.btnScanAndroidProject.Click += new System.EventHandler(this.btnScanAndroidProject_Click);
            // 
            // tabcScanResult
            // 
            this.tabcScanResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabcScanResult.Controls.Add(this.tabpUnusedResources);
            this.tabcScanResult.Controls.Add(this.tabpUsedResources);
            this.tabcScanResult.Controls.Add(this.tabpAllResources);
            this.tabcScanResult.Location = new System.Drawing.Point(3, 41);
            this.tabcScanResult.Name = "tabcScanResult";
            this.tabcScanResult.SelectedIndex = 0;
            this.tabcScanResult.Size = new System.Drawing.Size(541, 448);
            this.tabcScanResult.TabIndex = 1;
            // 
            // tabpUnusedResources
            // 
            this.tabpUnusedResources.Controls.Add(this.dgvUnusedResources);
            this.tabpUnusedResources.Location = new System.Drawing.Point(4, 22);
            this.tabpUnusedResources.Name = "tabpUnusedResources";
            this.tabpUnusedResources.Padding = new System.Windows.Forms.Padding(3);
            this.tabpUnusedResources.Size = new System.Drawing.Size(533, 422);
            this.tabpUnusedResources.TabIndex = 0;
            this.tabpUnusedResources.Text = "Unused Resources";
            this.tabpUnusedResources.UseVisualStyleBackColor = true;
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
            // tabpAllResources
            // 
            this.tabpAllResources.Controls.Add(this.dgvAllResources);
            this.tabpAllResources.Location = new System.Drawing.Point(4, 22);
            this.tabpAllResources.Name = "tabpAllResources";
            this.tabpAllResources.Size = new System.Drawing.Size(533, 422);
            this.tabpAllResources.TabIndex = 2;
            this.tabpAllResources.Text = "All Resources";
            this.tabpAllResources.UseVisualStyleBackColor = true;
            // 
            // dgvAllResources
            // 
            this.dgvAllResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllResources.Location = new System.Drawing.Point(0, 0);
            this.dgvAllResources.Name = "dgvAllResources";
            this.dgvAllResources.Size = new System.Drawing.Size(533, 422);
            this.dgvAllResources.TabIndex = 0;
            // 
            // dgvUsedResources
            // 
            this.dgvUsedResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsedResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsedResources.Location = new System.Drawing.Point(3, 3);
            this.dgvUsedResources.Name = "dgvUsedResources";
            this.dgvUsedResources.Size = new System.Drawing.Size(527, 416);
            this.dgvUsedResources.TabIndex = 1;
            // 
            // dgvUnusedResources
            // 
            this.dgvUnusedResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnusedResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnusedResources.Location = new System.Drawing.Point(3, 3);
            this.dgvUnusedResources.Name = "dgvUnusedResources";
            this.dgvUnusedResources.Size = new System.Drawing.Size(527, 416);
            this.dgvUnusedResources.TabIndex = 2;
            // 
            // Tool_Cleanup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabcScanResult);
            this.Controls.Add(this.btnScanAndroidProject);
            this.Name = "Tool_Cleanup";
            this.Size = new System.Drawing.Size(546, 492);
            this.tabcScanResult.ResumeLayout(false);
            this.tabpUnusedResources.ResumeLayout(false);
            this.tabpUsedResources.ResumeLayout(false);
            this.tabpAllResources.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllResources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedResources)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnusedResources)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScanAndroidProject;
        private System.Windows.Forms.TabControl tabcScanResult;
        private System.Windows.Forms.TabPage tabpUnusedResources;
        private System.Windows.Forms.TabPage tabpUsedResources;
        private System.Windows.Forms.TabPage tabpAllResources;
        private System.Windows.Forms.DataGridView dgvAllResources;
        private System.Windows.Forms.DataGridView dgvUsedResources;
        private System.Windows.Forms.DataGridView dgvUnusedResources;
    }
}
