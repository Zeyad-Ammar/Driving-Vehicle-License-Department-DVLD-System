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

namespace DVLD_Project.Licenses.InternationalLicenses
{
    public partial class frmInternationalLicense : Form
    {
        private int _LicenseID = -1;
        clsLicense _License=new clsLicense();
        private int _InternationalLicenseID = -1;
        public frmInternationalLicense()
        {
            InitializeComponent();
        }

        private bool _CheckValidation()
        {
            if (clsInternationalLicense.isDriverHaveActiveInternationalLicense(_License.DriverID))
            {
                MessageBox.Show("Driver Already Have An Active International License");
                return false;
            }

            if (_License.LicenseClassID!=(int)clsLicenseClass.enLicenseClass.OrdinaryDrivingLicense)
            {
                MessageBox.Show("The License should be Ordinary Driving License Class");
                return false;
            }

            return true;
        }

        
        private void btnIssue_Click(object sender, EventArgs e)
        {

           if(!_CheckValidation())
            {
                return;
            }

          
               clsInternationalLicense internationalLicense=new clsInternationalLicense(_License.DriverID,_LicenseID,clsCurrentUser.User.UserID);
                if (internationalLicense.Save())
                {
                    MessageBox.Show($"International License Issued Successfully With ID: {internationalLicense.InternationalLicenseID}");
                    _InternationalLicenseID = internationalLicense.InternationalLicenseID;
                    llbShowLicense.Enabled = true;
                    btnIssue.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Error When Trying To Issue International License");
                }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLicenseCardWithFilter1_onLicenseSelected(int obj)
        {
             _LicenseID = obj;
            _License=clsLicense.GetLicenseByLicenseID(_LicenseID);
            if(_License.LicenseID==-1)
            {
                MessageBox.Show("Error No License With This ID Exist");

                return;
            }

            btnIssue.Enabled=true;
            llbShowLicensesHistory.Enabled=true;
            lbLocalLicenseID.Text=_LicenseID.ToString();

        }

        private void frmInternationalLicense_Load(object sender, EventArgs e)
        {
            clsApplicationType InternationalLicenseApp = clsApplicationType.GetApplicationType((int)clsApplicationType.enAppType.NewIL);
            lbFees.Text = InternationalLicenseApp.ApplicationFees.ToString("0.00");
            lbIssueDate.Text=DateTime.Now.ToShortDateString();
            lbExpirationDate.Text=DateTime.Now.AddYears(1).ToShortDateString();
            lbCreatedBy.Text=clsCurrentUser.User.UserName;
            llbShowLicense.Enabled=false;
            llbShowLicensesHistory.Enabled = false;
            btnIssue.Enabled=false; 
        }

        private void llbShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_License.LicenseID == -1)
            {
                MessageBox.Show("Select a License First");
                return;
            }
            frmLicensesHistory licenseHistoryForm= new frmLicensesHistory(_License.DriverInfo.PersonID);
            licenseHistoryForm.ShowDialog();

        }

        private void llbShowLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseCard internationalLicenseCardForm = new frmInternationalLicenseCard(_InternationalLicenseID);
            internationalLicenseCardForm.ShowDialog();
        }
    }
}
