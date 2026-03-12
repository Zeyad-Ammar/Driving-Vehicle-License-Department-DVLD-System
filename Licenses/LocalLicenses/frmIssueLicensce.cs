using BusinessLayer;
using DVLD_Project.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmIssueLicensce : Form
    {
        private int _LocalAppID=-1;
        clsLocalDrivingLicenseApp _LocalApp=new clsLocalDrivingLicenseApp();
      
        public frmIssueLicensce(int LocalAppID)
        {
            InitializeComponent();
            _LocalAppID = LocalAppID;
            _LocalApp=clsLocalDrivingLicenseApp.GetLocalLicenseApp(LocalAppID);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssueLicensce_Load(object sender, EventArgs e)
        {
            ctrlLocalApplicationCard1.SetLocalDrivingLicenseAppData(_LocalAppID);

        }

       
        private void btnIssue_Click(object sender, EventArgs e)
        {





            int NewLicenseID = _LocalApp.IssueLicense(rtbNotes.Text, clsCurrentUser.User.UserID);
            if (NewLicenseID!=-1)
            {
               
                MessageBox.Show($"The Data Saved Successfully Ur License ID: {NewLicenseID}","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
               ctrlLocalApplicationCard1.EnableShowLicense=true;
                btnIssue.Enabled=false;

            }



        }
    }
}
