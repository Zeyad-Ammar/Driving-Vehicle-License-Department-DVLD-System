namespace DVLD_Project
{
    partial class frmLocalDrivingLicenseApplications
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
            this.lbFormTitle = new System.Windows.Forms.Label();
            this.lbFilterBy = new System.Windows.Forms.Label();
            this.lbRecords = new System.Windows.Forms.Label();
            this.lbRecordsValue = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsiShowAppDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsiEditApp = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiDeleteApp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsiCancelApp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsiSchduelTests = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiWrittingTest = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsiIssueDrivingLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsiShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsiShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.tbFilterByValue = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.cms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbFormTitle
            // 
            this.lbFormTitle.AutoSize = true;
            this.lbFormTitle.BackColor = System.Drawing.Color.OrangeRed;
            this.lbFormTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFormTitle.ForeColor = System.Drawing.Color.Blue;
            this.lbFormTitle.Location = new System.Drawing.Point(378, 228);
            this.lbFormTitle.Name = "lbFormTitle";
            this.lbFormTitle.Size = new System.Drawing.Size(628, 46);
            this.lbFormTitle.TabIndex = 1;
            this.lbFormTitle.Text = "Local Driving License Applications";
            // 
            // lbFilterBy
            // 
            this.lbFilterBy.AutoSize = true;
            this.lbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFilterBy.Location = new System.Drawing.Point(14, 300);
            this.lbFilterBy.Name = "lbFilterBy";
            this.lbFilterBy.Size = new System.Drawing.Size(99, 25);
            this.lbFilterBy.TabIndex = 2;
            this.lbFilterBy.Text = "_Filter By:";
            // 
            // lbRecords
            // 
            this.lbRecords.AutoSize = true;
            this.lbRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRecords.Location = new System.Drawing.Point(14, 633);
            this.lbRecords.Name = "lbRecords";
            this.lbRecords.Size = new System.Drawing.Size(106, 25);
            this.lbRecords.TabIndex = 3;
            this.lbRecords.Text = "# Records:";
            // 
            // lbRecordsValue
            // 
            this.lbRecordsValue.AutoSize = true;
            this.lbRecordsValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRecordsValue.Location = new System.Drawing.Point(126, 637);
            this.lbRecordsValue.Name = "lbRecordsValue";
            this.lbRecordsValue.Size = new System.Drawing.Size(45, 25);
            this.lbRecordsValue.TabIndex = 4;
            this.lbRecordsValue.Text = "???";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.cms;
            this.dataGridView1.Location = new System.Drawing.Point(12, 327);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1347, 290);
            this.dataGridView1.TabIndex = 5;
            // 
            // cms
            // 
            this.cms.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cms.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsiShowAppDetails,
            this.toolStripMenuItem1,
            this.cmsiEditApp,
            this.cmsiDeleteApp,
            this.toolStripMenuItem2,
            this.cmsiCancelApp,
            this.toolStripMenuItem3,
            this.cmsiSchduelTests,
            this.toolStripMenuItem4,
            this.cmsiIssueDrivingLicense,
            this.toolStripMenuItem5,
            this.cmsiShowLicense,
            this.toolStripMenuItem6,
            this.cmsiShowPersonLicenseHistory});
            this.cms.Name = "contextMenuStrip1";
            this.cms.Size = new System.Drawing.Size(376, 408);
            this.cms.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // cmsiShowAppDetails
            // 
            this.cmsiShowAppDetails.Image = global::DVLD_Project.Properties.Resources.PersonDetails_32;
            this.cmsiShowAppDetails.Name = "cmsiShowAppDetails";
            this.cmsiShowAppDetails.Size = new System.Drawing.Size(375, 46);
            this.cmsiShowAppDetails.Text = "Show Application Details";
            this.cmsiShowAppDetails.Click += new System.EventHandler(this.cmsiShowAppDetails_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(372, 6);
            // 
            // cmsiEditApp
            // 
            this.cmsiEditApp.Image = global::DVLD_Project.Properties.Resources.edit_32;
            this.cmsiEditApp.Name = "cmsiEditApp";
            this.cmsiEditApp.Size = new System.Drawing.Size(375, 46);
            this.cmsiEditApp.Text = "Edit Application";
            this.cmsiEditApp.Click += new System.EventHandler(this.cmsiEditApp_Click);
            // 
            // cmsiDeleteApp
            // 
            this.cmsiDeleteApp.Image = global::DVLD_Project.Properties.Resources.Delete_32_2;
            this.cmsiDeleteApp.Name = "cmsiDeleteApp";
            this.cmsiDeleteApp.Size = new System.Drawing.Size(375, 46);
            this.cmsiDeleteApp.Text = "Delete Application";
            this.cmsiDeleteApp.Click += new System.EventHandler(this.cmsiDeleteApp_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(372, 6);
            // 
            // cmsiCancelApp
            // 
            this.cmsiCancelApp.Image = global::DVLD_Project.Properties.Resources.Delete_32;
            this.cmsiCancelApp.Name = "cmsiCancelApp";
            this.cmsiCancelApp.Size = new System.Drawing.Size(375, 46);
            this.cmsiCancelApp.Text = "Cancel Application";
            this.cmsiCancelApp.Click += new System.EventHandler(this.cmsiCancelApp_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(372, 6);
            // 
            // cmsiSchduelTests
            // 
            this.cmsiSchduelTests.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsiVisionTest,
            this.cmsiWrittingTest,
            this.cmsiStreetTest});
            this.cmsiSchduelTests.Image = global::DVLD_Project.Properties.Resources.Schedule_Test_32;
            this.cmsiSchduelTests.Name = "cmsiSchduelTests";
            this.cmsiSchduelTests.Size = new System.Drawing.Size(375, 46);
            this.cmsiSchduelTests.Text = "Schedule Tests";
            // 
            // cmsiVisionTest
            // 
            this.cmsiVisionTest.Image = global::DVLD_Project.Properties.Resources.Vision_Test_32;
            this.cmsiVisionTest.Name = "cmsiVisionTest";
            this.cmsiVisionTest.Size = new System.Drawing.Size(304, 46);
            this.cmsiVisionTest.Text = "schedule Vision Test";
            this.cmsiVisionTest.Click += new System.EventHandler(this.cmsiVisionTest_Click);
            // 
            // cmsiWrittingTest
            // 
            this.cmsiWrittingTest.Image = global::DVLD_Project.Properties.Resources.Written_Test_32;
            this.cmsiWrittingTest.Name = "cmsiWrittingTest";
            this.cmsiWrittingTest.Size = new System.Drawing.Size(304, 46);
            this.cmsiWrittingTest.Text = "schedule writting test";
            this.cmsiWrittingTest.Click += new System.EventHandler(this.cmsiWrittingTest_Click);
            // 
            // cmsiStreetTest
            // 
            this.cmsiStreetTest.Image = global::DVLD_Project.Properties.Resources.Cars_48;
            this.cmsiStreetTest.Name = "cmsiStreetTest";
            this.cmsiStreetTest.Size = new System.Drawing.Size(304, 46);
            this.cmsiStreetTest.Text = "schedule street test";
            this.cmsiStreetTest.Click += new System.EventHandler(this.cmsiStreetTest_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(372, 6);
            // 
            // cmsiIssueDrivingLicense
            // 
            this.cmsiIssueDrivingLicense.Image = global::DVLD_Project.Properties.Resources.IssueDrivingLicense_321;
            this.cmsiIssueDrivingLicense.Name = "cmsiIssueDrivingLicense";
            this.cmsiIssueDrivingLicense.Size = new System.Drawing.Size(375, 46);
            this.cmsiIssueDrivingLicense.Text = "Issue Driving License First Time";
            this.cmsiIssueDrivingLicense.Click += new System.EventHandler(this.cmsiIssueDrivingLicense_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(372, 6);
            // 
            // cmsiShowLicense
            // 
            this.cmsiShowLicense.Image = global::DVLD_Project.Properties.Resources.License_View_32;
            this.cmsiShowLicense.Name = "cmsiShowLicense";
            this.cmsiShowLicense.Size = new System.Drawing.Size(375, 46);
            this.cmsiShowLicense.Text = "Show License";
            this.cmsiShowLicense.Click += new System.EventHandler(this.cmsiShowLicense_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(372, 6);
            // 
            // cmsiShowPersonLicenseHistory
            // 
            this.cmsiShowPersonLicenseHistory.Image = global::DVLD_Project.Properties.Resources.PersonLicenseHistory_321;
            this.cmsiShowPersonLicenseHistory.Name = "cmsiShowPersonLicenseHistory";
            this.cmsiShowPersonLicenseHistory.Size = new System.Drawing.Size(375, 46);
            this.cmsiShowPersonLicenseHistory.Text = "Show Person License History";
            this.cmsiShowPersonLicenseHistory.Click += new System.EventHandler(this.cmsiShowPersonLicenseHistory_Click);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "L.D.L AppID",
            "National No",
            "FullName",
            "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(108, 292);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(207, 33);
            this.cbFilterBy.TabIndex = 6;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // tbFilterByValue
            // 
            this.tbFilterByValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFilterByValue.Location = new System.Drawing.Point(321, 293);
            this.tbFilterByValue.Name = "tbFilterByValue";
            this.tbFilterByValue.Size = new System.Drawing.Size(363, 30);
            this.tbFilterByValue.TabIndex = 7;
            this.tbFilterByValue.TextChanged += new System.EventHandler(this.tbFilterByValue_TextChanged);
            this.tbFilterByValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFilterByValue_KeyPress);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::DVLD_Project.Properties.Resources.New_Application_641;
            this.button1.Location = new System.Drawing.Point(1250, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 73);
            this.button1.TabIndex = 9;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1198, 633);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(161, 47);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(541, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(315, 225);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmLocalDrivingLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1371, 687);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tbFilterByValue);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbRecordsValue);
            this.Controls.Add(this.lbRecords);
            this.Controls.Add(this.lbFilterBy);
            this.Controls.Add(this.lbFormTitle);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLocalDrivingLicenseApplications";
            this.Text = "frmLocalDrivingLicenseApplications";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.cms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbFormTitle;
        private System.Windows.Forms.Label lbFilterBy;
        private System.Windows.Forms.Label lbRecords;
        private System.Windows.Forms.Label lbRecordsValue;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.TextBox tbFilterByValue;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem cmsiShowAppDetails;
        private System.Windows.Forms.ToolStripMenuItem cmsiEditApp;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cmsiDeleteApp;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem cmsiCancelApp;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem cmsiSchduelTests;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem cmsiIssueDrivingLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem cmsiShowLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem cmsiShowPersonLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem cmsiVisionTest;
        private System.Windows.Forms.ToolStripMenuItem cmsiWrittingTest;
        private System.Windows.Forms.ToolStripMenuItem cmsiStreetTest;
    }
}