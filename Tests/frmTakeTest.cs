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

namespace DVLD_Project.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID = -1;
        private clsTestAppointment clsTestAppointment = new clsTestAppointment();
        private clsTest _test=new clsTest();
        public frmTakeTest(int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            clsTestAppointment=clsTestAppointment.GetTestAppointment(TestAppointmentID);

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlTestCard1.testAppointmentID = _TestAppointmentID;
            _test=clsTest.FindByTestAppointmentID(_TestAppointmentID);
            
            rbPass.Checked = true;

            if (_test.TestID != -1)
            {
                rbPass.Checked = _test.TestResult;
                rbFail.Checked = !_test.TestResult;
            }
           
            rbPass.Enabled = rbFail.Enabled= btnSave.Enabled = !clsTestAppointment.isLocked;
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadDataToTestObj()
        {
            _test.Notes=rtbNotes.Text;
            _test.TestResult=rbPass.Checked;
            _test.TestAppointmentID= _TestAppointmentID;
            _test.CreatedByUserID=clsCurrentUser.User.UserID;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            _LoadDataToTestObj();


            if (_test.Save())
            {
                clsTestAppointment testAppointment=clsTestAppointment.GetTestAppointment(_TestAppointmentID);
                if(testAppointment._LockTestAppointment())
                { 
                    MessageBox.Show("Data Saved Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ctrlTestCard1.TestIDValue = _test.TestID;
                   
                }
                btnSave.Enabled = false;
            }
        }
    }
}
