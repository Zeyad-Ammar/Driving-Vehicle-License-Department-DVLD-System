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

namespace DVLD_Project.Licenses.LocalLicenses
{
    public partial class frmRenewLicense : Form
    {
        clsLicense _License = new clsLicense();
        int _NewLicenseID = -1;
        public frmRenewLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLicenseCardWithFilter1_onLicenseSelected(int obj)
        {
            int LicenseID = obj;
            _License =clsLicense.GetLicenseByLicenseID(LicenseID);
            if (!_License.isLicenseExpired())
            {
                MessageBox.Show($"Can't Renew This License Cuz, It's Not Expired Yet.\n it's Expiration Date is {_License.ExpirationDate.ToShortDateString()}","Wait",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }

            if (!_License.IsActive)
            {
                MessageBox.Show($"Can't Renew This License Cuz, It's Not Active.", "Wait", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            lbApplicationDate.Text = DateTime.Now.ToShortDateString();
            lbIssueDate.Text = DateTime.Now.ToShortDateString();
            lbApplicationFees.Text = clsApplicationType.GetApplicationType((int)clsApplicationType.enAppType.RenewDL).ApplicationFees.ToString();
            lbLicenseFees.Text = _License.LicenseClassInfo.ClassFees.ToString();
            lbOldLicenseID.Text = _License.LicenseID.ToString();
            lbExpirationDate.Text = DateTime.Now.AddYears(_License.LicenseClassInfo.DefaultValidityLength).ToShortDateString();
            lbCreatedBy.Text = clsCurrentUser.User.UserName;
            lbTotalFees.Text = (clsApplicationType.GetApplicationType((int)clsApplicationType.enAppType.RenewDL).ApplicationFees + _License.LicenseClassInfo.ClassFees).ToString();
            llbLicenseHistroy.Enabled=true;
            btnIssue.Enabled = true;

        }

        private void frmRenewLicense_Load(object sender, EventArgs e)
        {
            ctrlLicenseCardWithFilter1.FocusOnSearchBox = true;
            btnIssue.Enabled = false;
            llbLicenseHistroy.Enabled=llbLicenseInfo.Enabled=false;

        }

        private void llbLicenseHistroy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicensesHistory licenseHistoryForm= new frmLicensesHistory(_License.DriverInfo.PersonID);
            licenseHistoryForm.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int NewLicenseID=_License.RenewLicense( rtbNotes.Text,clsCurrentUser.User.UserID);

               if(NewLicenseID != -1)
                {
                    MessageBox.Show($"License Renewed Successfully, New License ID is {NewLicenseID}","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    btnIssue.Enabled = false;
                    llbLicenseHistroy.Enabled = llbLicenseInfo.Enabled = true;
                    _NewLicenseID = NewLicenseID;
                     lbRenewedLicenseID.Text = NewLicenseID.ToString();
                     ctrlLicenseCardWithFilter1.RefreshCard();
                     clsLicense NewLicense= clsLicense.GetLicenseByLicenseID(NewLicenseID);
                      lbRLApplicationID.Text = NewLicense.ApplicationID.ToString();


            }
                else
                {
                    MessageBox.Show("Failed To Renew License","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
        }

        private void llbLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseCard licenseCardForm = new frmLicenseCard(_NewLicenseID);
            licenseCardForm.ShowDialog();

        }

 

        
        
       

        

       

        

       

       
        
    }
}
