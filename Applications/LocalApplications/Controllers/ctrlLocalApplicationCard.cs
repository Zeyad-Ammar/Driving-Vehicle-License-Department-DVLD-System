using BusinessLayer;
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

namespace DVLD_Project.Applications.LocalApplications.Controllers
{
    public partial class ctrlLocalApplicationCard : UserControl
    {
        private clsLocalDrivingLicenseApp _LocalDrivingLicenseApp=new clsLocalDrivingLicenseApp();
        public ctrlLocalApplicationCard()
        {
            InitializeComponent();
        }

        public bool EnableShowLicense
        {
            set
            {
                llbShowLicenseInfo.Enabled = value;
            }
        }
        private void _LoadDataFromObjToControl()
        {
            lbClassValue.Text=_LocalDrivingLicenseApp.LicenseClassInfo.ClassName;
            lbLDLAppIDValue.Text=_LocalDrivingLicenseApp.LDLicenseAppID.ToString();
            lbPassedTestsValue.Text=_LocalDrivingLicenseApp.PassedTests.ToString()+"/3";
            ctrlApplicationCard1.ApplicationID = _LocalDrivingLicenseApp.ApplicationID;


        }
       

        public void SetLocalDrivingLicenseAppData(int LocalDrivingLicenseAppID)
        {
            _LocalDrivingLicenseApp = clsLocalDrivingLicenseApp.GetLocalLicenseApp(LocalDrivingLicenseAppID);
            llbShowLicenseInfo.Enabled = _LocalDrivingLicenseApp.ApplicationStatus == clsApplication.enApplicationStatus.Completed;
            _LoadDataFromObjToControl();
        }

        private void llbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmLicenseCard licenseCardForm = new frmLicenseCard(clsLicense.GetLicenseByAppID(_LocalDrivingLicenseApp.ApplicationID).LicenseID);
            licenseCardForm.ShowDialog();
        }

        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
