using BusinessLayer;
using DVLD_Project.General;
using DVLD_Project.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.DetainAndReleaseLicense
{
    public partial class frmReleaseDetainedLicense : Form
    {
        private clsDetainedLicense _DetainedLicense=new clsDetainedLicense();
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int DetainedID)
        {
            InitializeComponent();
            _DetainedLicense = clsDetainedLicense.FindByDetainID(DetainedID);
            ctrlLicenseCardWithFilter1.EnableSearch = false;
            ctrlLicenseCardWithFilter1.SetLicenseID(_DetainedLicense.LicenseID);

            lbDetainID.Text = _DetainedLicense.DetainedID.ToString();
            lbDetainDate.Text = _DetainedLicense.DetainDate.ToShortDateString();
            lbApplicationFees.Text = clsApplicationType.GetApplicationType((int)clsApplicationType.enAppType.ReleaseDetainedDL).ApplicationFees.ToString();
            lbFineFees.Text = _DetainedLicense.FineFees.ToString();
            lbTotalFees.Text = (clsApplicationType.GetApplicationType((int)clsApplicationType.enAppType.ReleaseDetainedDL).ApplicationFees + _DetainedLicense.FineFees).ToString();
            lbLicenseID.Text = _DetainedLicense.LicenseID.ToString();
            lbCreatedBy.Text = _DetainedLicense.CreatedByUserID.ToString();
            btnRelease.Enabled = true;
            llbShowLicensesHistory.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llbShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicensesHistory licensesHistoryForm=new frmLicensesHistory(_DetainedLicense.LicenseInfo.DriverInfo.PersonID);
            licensesHistoryForm.ShowDialog();
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            if (_DetainedLicense.DetainedID == -1)
            {
                llbShowLicensesHistory.Enabled = false;
                btnRelease.Enabled = false;
            }

        }

        private void ctrlLicenseCardWithFilter1_onLicenseSelected(int obj)
        {
            int LicenseID = obj;
            _DetainedLicense = clsDetainedLicense.FindByLicenseID(LicenseID);
            if (_DetainedLicense.LicenseID==-1)
            {
                MessageBox.Show("Can't Release Not Detained License!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            lbDetainID.Text=_DetainedLicense.DetainedID.ToString();
            lbDetainDate.Text=_DetainedLicense.DetainDate.ToShortDateString();
            lbApplicationFees.Text=clsApplicationType.GetApplicationType((int)clsApplicationType.enAppType.ReleaseDetainedDL).ApplicationFees.ToString();
            lbFineFees.Text=_DetainedLicense.FineFees.ToString();
            lbTotalFees.Text=(clsApplicationType.GetApplicationType((int)clsApplicationType.enAppType.ReleaseDetainedDL).ApplicationFees+ _DetainedLicense.FineFees).ToString();
            lbLicenseID.Text=_DetainedLicense.LicenseID.ToString();
            lbCreatedBy.Text=_DetainedLicense.CreatedByUserID.ToString();
            btnRelease.Enabled= true;
            llbShowLicensesHistory.Enabled = true;



        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (_DetainedLicense.Release(clsCurrentUser.User.UserID))
            {
                MessageBox.Show("The License Released Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lbApplicationID.Text=_DetainedLicense.ReleaseApplicationID.ToString();
                ctrlLicenseCardWithFilter1.RefreshCard();
                btnRelease.Enabled = false;

            }
            else
            {
                MessageBox.Show("Error Can't Release The License Try Again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
