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
            this.SuspendLayout();
            // 
            // urScheduleTestAndRetake1
            // 
            this.urScheduleTestAndRetake1.Location = new System.Drawing.Point(12, 24);
            this.urScheduleTestAndRetake1.Name = "urScheduleTestAndRetake1";
            this.urScheduleTestAndRetake1.Size = new System.Drawing.Size(635, 678);
            this.urScheduleTestAndRetake1.TabIndex = 0;
            // 
            // frmScheduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 733);
            this.Controls.Add(this.urScheduleTestAndRetake1);
            this.Name = "frmScheduleTest";
            this.Text = "frmScheduleTest";
            this.Load += new System.EventHandler(this.frmScheduleTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UrScheduleTestAndRetake urScheduleTestAndRetake1;
    }
}