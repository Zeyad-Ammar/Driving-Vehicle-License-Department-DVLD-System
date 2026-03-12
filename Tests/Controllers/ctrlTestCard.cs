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

namespace DVLD_Project.Tests.Controllers
{
    public partial class ctrlTestCard : UserControl
    {
        private int _testAppointmentID = -1;
        
        public ctrlTestCard()
        {
            InitializeComponent();
        }

        private void _SetPictureAndTitle()
        {
            clsTestAppointment testAppointment = clsTestAppointment.GetTestAppointment(_testAppointmentID);

            switch ((clsTestType.enTestType)testAppointment.TestTypeID)
            {
                case clsTestType.enTestType.Vision:
                    groupBox1.Text = "Vision Test";
                    pictureBox3.Image = Resources.Vision_512;
                    lbTestTitle.Text = "Vison Test";

                    break;
                case clsTestType.enTestType.Writting:
                    groupBox1.Text = "Writting Test";
                    pictureBox3.Image = Resources.Written_Test_512;
                    lbTestTitle.Text = "Writting Test";
                    break;

                case clsTestType.enTestType.Street:
                    groupBox1.Text = "Street Test";
                    pictureBox3.Image = Resources.Street_Test_32;
                    lbTestTitle.Text = "Street Test";

                    break;
            }
        }
        private void _LoadDataToController()
        {

            clsTestAppointment testAppointment = clsTestAppointment.GetTestAppointment(_testAppointmentID);
            lbDateValue.Text=testAppointment.AppointmentDate.ToShortDateString();
            lbDLAppIDValue.Text=testAppointment.LDLAppID.ToString();
            lbFeesValue.Text=testAppointment.PaidFees.ToString();
            lbTestIDValue.Text = "Not Taken Yet";
            clsLocalDrivingLicenseApp lDlAppInfo = clsLocalDrivingLicenseApp.GetLocalLicenseApp(testAppointment.LDLAppID);
            lbNameValue.Text=lDlAppInfo.PersonInfo.FullName;
            lbDClassValue.Text = lDlAppInfo.LicenseClassInfo.ClassName;
            lbTrialValue.Text = clsTestAppointment.GetNumberOfTestAppointmentsOfTypeForLDLApp(testAppointment.LDLAppID, testAppointment.TestTypeID).ToString();
            _SetPictureAndTitle();
        }
        public int testAppointmentID
        {
            set
            {
                _testAppointmentID=value;
                _LoadDataToController();
            }
        }

       public int TestIDValue
        {
            set
            {
                lbTestIDValue.Text=value.ToString();
            }
        }

       
    }
}
