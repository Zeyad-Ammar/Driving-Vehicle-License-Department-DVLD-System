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

namespace DVLD_Project.Licenses.InternationalLicenses.Controls
{
    public partial class ctrlInternationalLicenseCard : UserControl
    {
        public ctrlInternationalLicenseCard()
        {
            InitializeComponent();
        }

        public void SetInternationalLicenseData(int InternationalLicenseID)
        {
            clsInternationalLicense internationalLicense = clsInternationalLicense.Find(InternationalLicenseID);
            lbNameValue.Text = internationalLicense.DriverInfo.PersonInfo.FullName;
            lbInterLicneseIDValue.Text = internationalLicense.InternationalLicenseID.ToString();
            lbLicenseID.Text = internationalLicense.IssuedUsingLocalLicenseID.ToString();
            lbNationalNoValue.Text = internationalLicense.DriverInfo.PersonInfo.NationalNumber.ToString();
            lbGenderValue.Text = "Male";
            if (internationalLicense.DriverInfo.PersonInfo.Gender == false)
            {
                pbProfilePhoto.Image = Resources.Female_512;
                lbGenderValue.Text = "Female";
            }
            lbIssueDateValue.Text = internationalLicense.IssuedDate.ToShortDateString();
            lbApplicationID.Text = internationalLicense.ApplicationID.ToString();
            lbIsActiveValue.Text = "Yes";
            if (internationalLicense.IsActive == false)
            {
                lbIsActiveValue.Text = "No";
            }
            lbDateValue.Text = internationalLicense.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lbDriverIDValue.Text = internationalLicense.DriverID.ToString();
            lbExpirationDateValue.Text = internationalLicense.ExpirationDate.ToShortDateString();
            if (!string.IsNullOrEmpty(internationalLicense.DriverInfo.PersonInfo.ProfilePhotoURL))
            {
                pbProfilePhoto.ImageLocation = internationalLicense.DriverInfo.PersonInfo.ProfilePhotoURL;

            }
        }
    }
}
