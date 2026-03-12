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

namespace DVLD_Project.Applications.ReplaceLicenseForDamagOrLost
{
    public partial class frmReplaceLicense : Form
    {
       
        int _ReplaceReason = -1;
        int _ApplicationTypeID = -1;
        clsLicense _License = new clsLicense();
        int _NewLicenseID = -1;
        public frmReplaceLicense()
        {
            InitializeComponent();
        }

        private void frmReplaceLicense_Load(object sender, EventArgs e)
        {
            llbLicenseHistroy.Enabled = false;
            llbLicenseInfo.Enabled = false;
            btnIssue.Enabled = false;
            rbDamage.Checked = true;
            _ReplaceReason = (int)clsLicense.enIssueReason.Replacement4Damaged;
            _ApplicationTypeID=(int)clsApplicationType.enAppType.ReplaceDamagedDL;

        }

        private void ctrlLicenseCardWithFilter1_onLicenseSelected(int obj)
        {
            int OldLicenseID = obj;
            _License= clsLicense.GetLicenseByLicenseID(OldLicenseID);
            if (!_License.IsActive)
            {
                                MessageBox.Show($"Can't Replace This License Cuz, It's Not Active.", "Wait", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if(_License.isLicenseExpired())
            {
                MessageBox.Show($"Can't Replace This License Cuz, It's Expired.", "Wait", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            lbApplicationDate.Text = DateTime.Now.ToShortDateString();     
            lbApplicationFees.Text=clsApplicationType.GetApplicationType(_ApplicationTypeID).ApplicationFees.ToString();
            lbOldLicenseID.Text = _License.LicenseID.ToString();
            lbCreatedBy.Text = clsCurrentUser.User.UserName;
            btnIssue.Enabled = true;
            llbLicenseHistroy.Enabled = true;
            llbLicenseInfo.Enabled = false;

        }

        private void rbDamage_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamage.Checked)
            {
                _ReplaceReason = (int)clsLicense.enIssueReason.Replacement4Damaged;
                _ApplicationTypeID = (int)clsApplicationType.enAppType.ReplaceDamagedDL;
                lbApplicationFees.Text = clsApplicationType.GetApplicationType(_ApplicationTypeID).ApplicationFees.ToString();
            }
        }

        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLost.Checked)
            {
                _ReplaceReason = (int)clsLicense.enIssueReason.Replacement4Lost;
                _ApplicationTypeID = (int)clsApplicationType.enAppType.ReplaceLostDL;
                lbApplicationFees.Text = clsApplicationType.GetApplicationType(_ApplicationTypeID).ApplicationFees.ToString();
            }

        }

        private void llbLicenseHistroy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicensesHistory licensesHistoryForm = new frmLicensesHistory(_License.DriverInfo.PersonID);
            licensesHistoryForm.ShowDialog();
        }

        private void llbLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseCard licenseCardForm = new frmLicenseCard(_NewLicenseID);
            licenseCardForm.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _NewLicenseID = _License.ReplaceLicense(_ReplaceReason,clsCurrentUser.User.UserID);
            clsLicense NewLicense= clsLicense.GetLicenseByLicenseID(_NewLicenseID);
            if (_NewLicenseID != -1)
            {
                MessageBox.Show($"License Replaced Successfully, New License ID is {_NewLicenseID}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lbRenewedLicenseID.Text = _NewLicenseID.ToString();
                lbRLApplicationID.Text = NewLicense.ApplicationID.ToString();
                ctrlLicenseCardWithFilter1.RefreshCard();
                llbLicenseInfo.Enabled = true;
                btnIssue.Enabled = false;
            }
            else
            {
                               MessageBox.Show($"Failed To Replace License, Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
