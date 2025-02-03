namespace licensesApp
{
    partial class frmScheduleTest
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
            this.urScheduleTestAndRetake1 = new licensesApp.UrScheduleTestAndRetake();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // urScheduleTestAndRetake1
            // 
            this.urScheduleTestAndRetake1.Location = new System.Drawing.Point(12, 24);
            this.urScheduleTestAndRetake1.Name = "urScheduleTestAndRetake1";
            this.urScheduleTestAndRetake1.Size = new System.Drawing.Size(635, 678);
            this.urScheduleTestAndRetake1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(292, 686);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmScheduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 733);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.urScheduleTestAndRetake1);
            this.Name = "frmScheduleTest";
            this.Text = "frmScheduleTest";
            this.Load += new System.EventHandler(this.frmScheduleTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UrScheduleTestAndRetake urScheduleTestAndRetake1;
        private System.Windows.Forms.Button btnClose;
    }
}