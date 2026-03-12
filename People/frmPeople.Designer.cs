namespace DVLD_Project
{
    partial class frmPeople
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
            this.cmsForListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.smiShowDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.smiAddNewPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.smiEdite = new System.Windows.Forms.ToolStripMenuItem();
            this.smiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.smiSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.smiPhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.lbManagePeople = new System.Windows.Forms.Label();
            this.lbRecords = new System.Windows.Forms.Label();
            this.lbNumOfRecordsValue = new System.Windows.Forms.Label();
            this.lbFilterBy = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.tbFilterValue = new System.Windows.Forms.TextBox();
            this.dgvPeopleList = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddNewPerson = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbGender = new System.Windows.Forms.ComboBox();
            this.cmsForListView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeopleList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmsForListView
            // 
            this.cmsForListView.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsForListView.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.cmsForListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiShowDetail,
            this.toolStripSeparator1,
            this.smiAddNewPerson,
            this.smiEdite,
            this.smiDelete,
            this.toolStripSeparator2,
            this.smiSendEmail,
            this.smiPhoneCall});
            this.cmsForListView.Name = "cmsForListView";
            this.cmsForListView.Size = new System.Drawing.Size(253, 292);
            // 
            // smiShowDetail
            // 
            this.smiShowDetail.Image = global::DVLD_Project.Properties.Resources.PersonDetails_32;
            this.smiShowDetail.Name = "smiShowDetail";
            this.smiShowDetail.Size = new System.Drawing.Size(252, 46);
            this.smiShowDetail.Text = "Show Details";
            this.smiShowDetail.Click += new System.EventHandler(this.smiShowDetail_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(249, 6);
            // 
            // smiAddNewPerson
            // 
            this.smiAddNewPerson.Image = global::DVLD_Project.Properties.Resources.Add_Person_40;
            this.smiAddNewPerson.Name = "smiAddNewPerson";
            this.smiAddNewPerson.Size = new System.Drawing.Size(252, 46);
            this.smiAddNewPerson.Text = "Add New Person";
            this.smiAddNewPerson.Click += new System.EventHandler(this.smiAddNewPerson_Click);
            // 
            // smiEdite
            // 
            this.smiEdite.Image = global::DVLD_Project.Properties.Resources.edit_32;
            this.smiEdite.Name = "smiEdite";
            this.smiEdite.Size = new System.Drawing.Size(252, 46);
            this.smiEdite.Text = "Edit";
            this.smiEdite.Click += new System.EventHandler(this.smiEdit_Click);
            // 
            // smiDelete
            // 
            this.smiDelete.Image = global::DVLD_Project.Properties.Resources.Delete_32;
            this.smiDelete.Name = "smiDelete";
            this.smiDelete.Size = new System.Drawing.Size(252, 46);
            this.smiDelete.Text = "Delete";
            this.smiDelete.Click += new System.EventHandler(this.smiDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(249, 6);
            // 
            // smiSendEmail
            // 
            this.smiSendEmail.Image = global::DVLD_Project.Properties.Resources.send_email_32;
            this.smiSendEmail.Name = "smiSendEmail";
            this.smiSendEmail.Size = new System.Drawing.Size(252, 46);
            this.smiSendEmail.Text = "Send Email";
            // 
            // smiPhoneCall
            // 
            this.smiPhoneCall.Image = global::DVLD_Project.Properties.Resources.call_32;
            this.smiPhoneCall.Name = "smiPhoneCall";
            this.smiPhoneCall.Size = new System.Drawing.Size(252, 46);
            this.smiPhoneCall.Text = "Phone Call";
            // 
            // lbManagePeople
            // 
            this.lbManagePeople.AutoSize = true;
            this.lbManagePeople.BackColor = System.Drawing.Color.DarkOrange;
            this.lbManagePeople.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbManagePeople.ForeColor = System.Drawing.Color.Blue;
            this.lbManagePeople.Location = new System.Drawing.Point(688, 180);
            this.lbManagePeople.Name = "lbManagePeople";
            this.lbManagePeople.Size = new System.Drawing.Size(314, 38);
            this.lbManagePeople.TabIndex = 1;
            this.lbManagePeople.Text = "MANAGE PEOPLE";
            // 
            // lbRecords
            // 
            this.lbRecords.AutoSize = true;
            this.lbRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRecords.Location = new System.Drawing.Point(23, 528);
            this.lbRecords.Name = "lbRecords";
            this.lbRecords.Size = new System.Drawing.Size(116, 25);
            this.lbRecords.TabIndex = 5;
            this.lbRecords.Text = "# Records:";
            // 
            // lbNumOfRecordsValue
            // 
            this.lbNumOfRecordsValue.AutoSize = true;
            this.lbNumOfRecordsValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumOfRecordsValue.Location = new System.Drawing.Point(145, 532);
            this.lbNumOfRecordsValue.Name = "lbNumOfRecordsValue";
            this.lbNumOfRecordsValue.Size = new System.Drawing.Size(39, 20);
            this.lbNumOfRecordsValue.TabIndex = 6;
            this.lbNumOfRecordsValue.Text = "???";
            // 
            // lbFilterBy
            // 
            this.lbFilterBy.AutoSize = true;
            this.lbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFilterBy.Location = new System.Drawing.Point(12, 184);
            this.lbFilterBy.Name = "lbFilterBy";
            this.lbFilterBy.Size = new System.Drawing.Size(110, 25);
            this.lbFilterBy.TabIndex = 7;
            this.lbFilterBy.Text = "_Filter By:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "PersonID",
            "NationalNo",
            "FirstName",
            "LastName",
            "Gender"});
            this.cbFilterBy.Location = new System.Drawing.Point(115, 182);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(214, 33);
            this.cbFilterBy.TabIndex = 8;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tbFilterValue
            // 
            this.tbFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFilterValue.Location = new System.Drawing.Point(347, 184);
            this.tbFilterValue.Name = "tbFilterValue";
            this.tbFilterValue.Size = new System.Drawing.Size(299, 30);
            this.tbFilterValue.TabIndex = 9;
            this.tbFilterValue.TextChanged += new System.EventHandler(this.tbFilterValue_TextChanged);
            this.tbFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFilterValue_KeyPress);
            // 
            // dgvPeopleList
            // 
            this.dgvPeopleList.AllowUserToAddRows = false;
            this.dgvPeopleList.AllowUserToDeleteRows = false;
            this.dgvPeopleList.AllowUserToOrderColumns = true;
            this.dgvPeopleList.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPeopleList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeopleList.ContextMenuStrip = this.cmsForListView;
            this.dgvPeopleList.Location = new System.Drawing.Point(6, 222);
            this.dgvPeopleList.Name = "dgvPeopleList";
            this.dgvPeopleList.ReadOnly = true;
            this.dgvPeopleList.RowHeadersWidth = 51;
            this.dgvPeopleList.RowTemplate.Height = 24;
            this.dgvPeopleList.Size = new System.Drawing.Size(1661, 295);
            this.dgvPeopleList.TabIndex = 10;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1510, 523);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(157, 43);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewPerson.Image = global::DVLD_Project.Properties.Resources.Add_Person_40;
            this.btnAddNewPerson.Location = new System.Drawing.Point(1549, 145);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(116, 64);
            this.btnAddNewPerson.TabIndex = 3;
            this.btnAddNewPerson.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddNewPerson.UseVisualStyleBackColor = true;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.People_400;
            this.pictureBox1.Location = new System.Drawing.Point(692, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(317, 168);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // cbGender
            // 
            this.cbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGender.FormattingEnabled = true;
            this.cbGender.Items.AddRange(new object[] {
            "All",
            "Male",
            "Female"});
            this.cbGender.Location = new System.Drawing.Point(347, 183);
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(139, 33);
            this.cbGender.TabIndex = 11;
            this.cbGender.SelectedIndexChanged += new System.EventHandler(this.cbGender_SelectedIndexChanged);
            // 
            // frmPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1677, 571);
            this.Controls.Add(this.cbGender);
            this.Controls.Add(this.dgvPeopleList);
            this.Controls.Add(this.tbFilterValue);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lbFilterBy);
            this.Controls.Add(this.lbNumOfRecordsValue);
            this.Controls.Add(this.lbRecords);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddNewPerson);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbManagePeople);
            this.Name = "frmPeople";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPeople";
            this.Load += new System.EventHandler(this.frmPeople_Load);
            this.cmsForListView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeopleList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbManagePeople;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnAddNewPerson;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbRecords;
        private System.Windows.Forms.Label lbNumOfRecordsValue;
        private System.Windows.Forms.Label lbFilterBy;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.TextBox tbFilterValue;
        private System.Windows.Forms.ContextMenuStrip cmsForListView;
        private System.Windows.Forms.ToolStripMenuItem smiShowDetail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem smiAddNewPerson;
        private System.Windows.Forms.ToolStripMenuItem smiEdite;
        private System.Windows.Forms.ToolStripMenuItem smiDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem smiSendEmail;
        private System.Windows.Forms.ToolStripMenuItem smiPhoneCall;
        private System.Windows.Forms.DataGridView dgvPeopleList;
        private System.Windows.Forms.ComboBox cbGender;
    }
}