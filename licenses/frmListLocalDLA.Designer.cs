namespace licensesApp
{
    partial class frmListLocalDLA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListLocalDLA));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilterby = new System.Windows.Forms.ComboBox();
            this.btnAddNewApplication = new System.Windows.Forms.Button();
            this.dgvLocalApplicationList = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalApplicationList)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(297, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 123);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(210, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(365, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local Driving License Applications";
            // 
            // cbFilterby
            // 
            this.cbFilterby.FormattingEnabled = true;
            this.cbFilterby.Location = new System.Drawing.Point(81, 206);
            this.cbFilterby.Name = "cbFilterby";
            this.cbFilterby.Size = new System.Drawing.Size(121, 21);
            this.cbFilterby.TabIndex = 2;
            // 
            // btnAddNewApplication
            // 
            this.btnAddNewApplication.Location = new System.Drawing.Point(725, 174);
            this.btnAddNewApplication.Name = "btnAddNewApplication";
            this.btnAddNewApplication.Size = new System.Drawing.Size(63, 53);
            this.btnAddNewApplication.TabIndex = 3;
            this.btnAddNewApplication.UseVisualStyleBackColor = true;
            // 
            // dgvLocalApplicationList
            // 
            this.dgvLocalApplicationList.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalApplicationList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalApplicationList.Location = new System.Drawing.Point(2, 233);
            this.dgvLocalApplicationList.Name = "dgvLocalApplicationList";
            this.dgvLocalApplicationList.Size = new System.Drawing.Size(882, 224);
            this.dgvLocalApplicationList.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Filter By:";
            // 
            // frmListLocalDLA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(896, 541);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvLocalApplicationList);
            this.Controls.Add(this.btnAddNewApplication);
            this.Controls.Add(this.cbFilterby);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmListLocalDLA";
            this.Text = "frmListLocalDLA";
            this.Load += new System.EventHandler(this.frmListLocalDLA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalApplicationList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilterby;
        private System.Windows.Forms.Button btnAddNewApplication;
        private System.Windows.Forms.DataGridView dgvLocalApplicationList;
        private System.Windows.Forms.Label label2;
    }
}