using BusinessLayer;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Licenses
{
    public partial class ctrlLicenseCard : UserControl
    {
        private clsLicense _License=new clsLicense();
      
        public ctrlLicenseCard()
        {
            InitializeComponent();
        }

        public void SetLicenseDataByLicenseID(int LicenseID)
        {
            _License=clsLicense.GetLicenseByLicenseID(LicenseID);
            
            lbClassValue.Text = _License.LicenseClassInfo.ClassName;
            lbNameValue.Text= _License.DriverInfo.PersonInfo.FullName;
            lbLicneseIDValue.Text= _License.LicenseID.ToString();
            lbNationalNoValue.Text= _License.DriverInfo.PersonInfo.NationalNumber.ToString();
            lbGenderValue.Text = "Male";
            pbProfilePhoto.Image = Resources.Male_512;
            if (_License.DriverInfo.PersonInfo.Gender == false)
            {
                pbProfilePhoto.Image = Resources.Female_512;
                lbGenderValue.Text = "Female";
            }
            lbIssueDateValue.Text=_License.IssueDate.ToShortDateString();
            lbReasonValue.Text=_License.GetIssueReason();
            if (string.IsNullOrEmpty(_License.Notes))
            {
                lbNotesValue.Text = "No Notes";
            }
            else
            {
                lbNotesValue.Text = _License.Notes;
            }
            lbIsActiveValue.Text = "Yes";
            if (_License.IsActive == false) {
                lbIsActiveValue.Text = "No";
            }
            lbDateValue.Text=_License.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lbDriverIDValue.Text=_License.DriverID.ToString();
            lbExpirationDateValue.Text=_License.ExpirationDate.ToShortDateString();
            lbIsDetainedValue.Text = "Yes";
            if (clsDetainedLicense.isLicenseDetained(LicenseID) == false)
            {
                lbIsDetainedValue.Text = "No";
            }

            if (!string.IsNullOrEmpty(_License.DriverInfo.PersonInfo.ProfilePhotoURL))
            {
                pbProfilePhoto.ImageLocation = _License.DriverInfo.PersonInfo.ProfilePhotoURL; 
            }
        }

        public void SetLicenseDataByAppID(int AppID)
        {

            _License = clsLicense.GetLicenseByAppID(AppID);
            if (_License.LicenseID == -1)
            {
                MessageBox.Show("Can't Find The License Of This Application");
                return;
            }
           
            lbClassValue.Text = _License.LicenseClassInfo.ClassName;
            lbNameValue.Text = _License.DriverInfo.PersonInfo.FullName;
            lbLicneseIDValue.Text = _License.LicenseID.ToString();
            lbNationalNoValue.Text = _License.DriverInfo.PersonInfo.NationalNumber.ToString();
            lbGenderValue.Text = "Male";
            if (_License.DriverInfo.PersonInfo.Gender == false)
            {
                lbGenderValue.Text = "Female";
                pictureBox1.Image = Resources.Female_512;
            }
            lbIssueDateValue.Text = _License.IssueDate.ToShortDateString();
            lbReasonValue.Text = _License.GetIssueReason();
            lbNotesValue.Text = _License.Notes;
            lbIsActiveValue.Text = _License.IsActive? "Yes":"No";
           
            lbDateValue.Text = _License.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lbDriverIDValue.Text = _License.DriverID.ToString();
            lbExpirationDateValue.Text = _License.ExpirationDate.ToShortDateString();
            lbIsDetainedValue.Text = _License.isDetained()? "Yes":"No";
           

            if (!string.IsNullOrEmpty(_License.DriverInfo.PersonInfo.ProfilePhotoURL)) { 
                 pbProfilePhoto.ImageLocation = (_License.DriverInfo.PersonInfo.ProfilePhotoURL);
            }

        }


    }
}
