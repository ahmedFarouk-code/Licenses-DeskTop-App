namespace licensesApp
{
    partial class frmManagePeople
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSortingBy = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 118);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(797, 270);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(677, 394);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 44);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(713, 44);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 58);
            this.btnAddNew.TabIndex = 2;
            this.btnAddNew.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 410);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "# Records:";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblRecords.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(96, 410);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(28, 16);
            this.lblRecords.TabIndex = 4;
            this.lblRecords.Text = "???";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sorting by:";
            // 
            // cbSortingBy
            // 
            this.cbSortingBy.FormattingEnabled = true;
            this.cbSortingBy.Location = new System.Drawing.Point(97, 85);
            this.cbSortingBy.Name = "cbSortingBy";
            this.cbSortingBy.Size = new System.Drawing.Size(168, 21);
            this.cbSortingBy.TabIndex = 6;
            // 
            // frmManagePeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbSortingBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmManagePeople";
            this.Text = "Manage People";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSortingBy;
    }
}