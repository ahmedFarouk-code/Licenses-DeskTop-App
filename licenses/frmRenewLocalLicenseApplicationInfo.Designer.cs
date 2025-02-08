namespace licensesApp
{
    partial class frmRenewLocalLicenseApplicationInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRenewLocalLicenseApplicationInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFindLicense = new System.Windows.Forms.Button();
            this.txtLicenseID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.llblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.llblShowNewLicense = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRenew = new System.Windows.Forms.Button();
            this.urDriverLIcenseInfo1 = new licensesApp.urDriverLIcenseInfo();
            this.ucRenewLocalApplicationInfo1 = new licensesApp.ucRenewLocalApplicationInfo();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(334, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(331, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "Renew License Application";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFindLicense);
            this.groupBox2.Controls.Add(this.txtLicenseID);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(10, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(476, 69);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter";
            // 
            // btnFindLicense
            // 
            this.btnFindLicense.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFindLicense.BackgroundImage")));
            this.btnFindLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFindLicense.Location = new System.Drawing.Point(409, 18);
            this.btnFindLicense.Name = "btnFindLicense";
            this.btnFindLicense.Size = new System.Drawing.Size(46, 45);
            this.btnFindLicense.TabIndex = 2;
            this.btnFindLicense.UseVisualStyleBackColor = true;
            this.btnFindLicense.Click += new System.EventHandler(this.btnFindLicense_Click);
            // 
            // txtLicenseID
            // 
            this.txtLicenseID.Location = new System.Drawing.Point(69, 26);
            this.txtLicenseID.Name = "txtLicenseID";
            this.txtLicenseID.Size = new System.Drawing.Size(301, 20);
            this.txtLicenseID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "_OldLicenseID:";
            // 
            // llblShowLicenseHistory
            // 
            this.llblShowLicenseHistory.AutoSize = true;
            this.llblShowLicenseHistory.Enabled = false;
            this.llblShowLicenseHistory.Location = new System.Drawing.Point(30, 711);
            this.llblShowLicenseHistory.Name = "llblShowLicenseHistory";
            this.llblShowLicenseHistory.Size = new System.Drawing.Size(108, 13);
            this.llblShowLicenseHistory.TabIndex = 12;
            this.llblShowLicenseHistory.TabStop = true;
            this.llblShowLicenseHistory.Text = "Show License History";
            this.llblShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblShowLicenseHistory_LinkClicked);
            // 
            // llblShowNewLicense
            // 
            this.llblShowNewLicense.AutoSize = true;
            this.llblShowNewLicense.Enabled = false;
            this.llblShowNewLicense.Location = new System.Drawing.Point(171, 711);
            this.llblShowNewLicense.Name = "llblShowNewLicense";
            this.llblShowNewLicense.Size = new System.Drawing.Size(118, 13);
            this.llblShowNewLicense.TabIndex = 11;
            this.llblShowNewLicense.TabStop = true;
            this.llblShowNewLicense.Text = "Show New License Info";
            this.llblShowNewLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblShowNewLicense_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(686, 699);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 36);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRenew
            // 
            this.btnRenew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRenew.Location = new System.Drawing.Point(831, 699);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(91, 36);
            this.btnRenew.TabIndex = 9;
            this.btnRenew.Text = "Renew";
            this.btnRenew.UseVisualStyleBackColor = true;
            this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // urDriverLIcenseInfo1
            // 
            this.urDriverLIcenseInfo1.Location = new System.Drawing.Point(-3, 112);
            this.urDriverLIcenseInfo1.Name = "urDriverLIcenseInfo1";
            this.urDriverLIcenseInfo1.Size = new System.Drawing.Size(933, 332);
            this.urDriverLIcenseInfo1.TabIndex = 13;
            // 
            // ucRenewLocalApplicationInfo1
            // 
            this.ucRenewLocalApplicationInfo1.Location = new System.Drawing.Point(12, 442);
            this.ucRenewLocalApplicationInfo1.Name = "ucRenewLocalApplicationInfo1";
            this.ucRenewLocalApplicationInfo1.Size = new System.Drawing.Size(918, 251);
            this.ucRenewLocalApplicationInfo1.TabIndex = 14;
            // 
            // frmRenewLocalLicenseApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 739);
            this.Controls.Add(this.ucRenewLocalApplicationInfo1);
            this.Controls.Add(this.urDriverLIcenseInfo1);
            this.Controls.Add(this.llblShowLicenseHistory);
            this.Controls.Add(this.llblShowNewLicense);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRenew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmRenewLocalLicenseApplicationInfo";
            this.Text = "frmRenewLocalLicenseApplicationInfo";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnFindLicense;
        private System.Windows.Forms.TextBox txtLicenseID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel llblShowLicenseHistory;
        private System.Windows.Forms.LinkLabel llblShowNewLicense;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRenew;
        private urDriverLIcenseInfo urDriverLIcenseInfo1;
        private ucRenewLocalApplicationInfo ucRenewLocalApplicationInfo1;
    }
}