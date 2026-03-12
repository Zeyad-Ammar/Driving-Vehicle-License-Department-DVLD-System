using BusinessLayer;
using DVLD_Project.General;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Tests
{
    public partial class frmScheduleTest : Form
    {

        private clsTestType _TestType;
        private int _LDLAppID;
        private clsTestAppointment _testAppointment=new clsTestAppointment();
        public frmScheduleTest(int lDLAppID,int TestType)
        {
            InitializeComponent();
            _LDLAppID = lDLAppID;
            _TestType=clsTestType.GetTestType(TestType);

        }

        private void _SetFormPicture()
        {
            switch (_TestType.TestTypeID)
            {
                case clsTestType.enTestType.Vision:
                    pictureBox1.Image = Resources.Vision_512;
                    break;
                case clsTestType.enTestType.Writting:
                    pictureBox1.Image=Resources.Written_Test_512;
                    break;
                case clsTestType.enTestType.Street:
                    pictureBox1.Image = Resources.Street_Test_32;
                    break;
            }
        }
        public frmScheduleTest(int testAppointmentID)
        {
            InitializeComponent();
           
            _testAppointment=clsTestAppointment.GetTestAppointment(testAppointmentID);
            _TestType = clsTestType.GetTestType(_testAppointment.TestTypeID);
            _LDLAppID = _testAppointment.LDLAppID;
            if (_testAppointment.RetakeTestAppID != -1)
            { 
                lbRTestAppIDValue.Text = _testAppointment.RetakeTestAppID.ToString();
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApp localDrivingLicenseApp = clsLocalDrivingLicenseApp.GetLocalLicenseApp(_LDLAppID);
            lbLDLAppIDValue.Text = localDrivingLicenseApp.LDLicenseAppID.ToString();
            lbNameValue.Text = localDrivingLicenseApp.PersonInfo.FullName;
            lbDLClassValue.Text = localDrivingLicenseApp.LicenseClassInfo.ClassName;
            int numberOfTrial = clsTestAppointment.GetNumberOfTestAppointmentsOfTypeForLDLApp(_LDLAppID, (int)_TestType.TestTypeID);
            lbTrialValue.Text = numberOfTrial.ToString();
            lbFeesValue.Text = _TestType.TestTypeFees.ToString();
            dtpDateOfTest.MinDate = DateTime.Now;
            dtpDateOfTest.Enabled = !_testAppointment.isLocked;
            btnSave.Enabled= !_testAppointment.isLocked;
            _SetFormPicture();



            if (numberOfTrial == 0)
            {
                gbRetakeTest.Enabled = false;
            }
            else
            {
                lbRAppFeesValue.Text = clsApplicationType.GetApplicationType((int)clsApplicationType.enAppType.NewLDL).ApplicationFees.ToString();
                lbTotalFeesValue.Text = (_TestType.TestTypeFees + clsApplicationType.GetApplicationType((int)clsApplicationType.enAppType.NewLDL).ApplicationFees).ToString();
            }

        }

        private void _LoadDataToTestAppointmentObj()
        {
            _testAppointment.TestTypeID =(int) _TestType.TestTypeID;
            _testAppointment.LDLAppID =(int) _LDLAppID;
            _testAppointment.CreatedByUserID=clsCurrentUser.User.UserID;
            _testAppointment.AppointmentDate = dtpDateOfTest.Value;
            _testAppointment.PaidFees= _TestType.TestTypeFees;
            _testAppointment.CreatedByUserID= clsCurrentUser.User.UserID;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsTestAppointment.GetNumberOfTestAppointmentsOfTypeForLDLApp(_LDLAppID, (int)_TestType.TestTypeID) > 0)
            {
                clsLocalDrivingLicenseApp LApp= clsLocalDrivingLicenseApp.GetLocalLicenseApp(_LDLAppID);
                clsApplication application = new clsApplication(LApp.ApplicationPersonID,(int)clsApplication.enApplicationTypes.RetakeTest,clsCurrentUser.User.UserID);

                if (!application.Save())
                {
                    return;
                }
                _testAppointment.RetakeTestAppID=application.ApplicationID;
                lbRTestAppIDValue.Text = _testAppointment.RetakeTestAppID.ToString();
            }
            _LoadDataToTestAppointmentObj();
            if (!_testAppointment.Save())
            {
                MessageBox.Show("Error Can't Save the Test Appointment");
                return;

            }
           
            MessageBox.Show("The Data Saved Successfully");
            btnSave.Enabled=false;

        }
    }
}
