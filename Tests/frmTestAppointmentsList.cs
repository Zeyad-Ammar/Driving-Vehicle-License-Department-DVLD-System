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

namespace DVLD_Project.Tests
{
    public partial class frmTestAppointmentsList : Form
    {
        private int _LocalAppID=-1;

        private clsTestType.enTestType _TestType=clsTestType.enTestType.Vision;

        private DataTable _TestAppointmentsList=new DataTable();
        public frmTestAppointmentsList(int LocalAppID, clsTestType.enTestType testType)
        {
            InitializeComponent();
            _LocalAppID = LocalAppID;
            _TestType = testType;
        }

        private void _SetPictureAndTitle()
        {
            switch (_TestType)
            {

                case clsTestType.enTestType.Vision:
                    lbFormTitle.Text = "Vision Test Appointments";
                    pictureBox1.Image = Resources.Vision_512;

                    break;

                       case clsTestType.enTestType.Writting:
                    lbFormTitle.Text = "Writting Test Appointments";
                    pictureBox1.Image = Resources.Written_Test_512;
                    break;

                case clsTestType.enTestType.Street:
                    lbFormTitle.Text = "Street Test Appointments";
                    pictureBox1.Image = Resources.driving_test_512;
                    break;
            }
        }
        private void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
            
            _SetPictureAndTitle();
            _TestAppointmentsList = clsTestAppointment.GetAllTestAppointmentsOfTypeForLDLApp(_LocalAppID, (int)_TestType);
            dataGridView1.DataSource = _TestAppointmentsList;
            ctrlLocalApplicationCard1.SetLocalDrivingLicenseAppData(_LocalAppID);
            lbRecordsValue.Text=dataGridView1.RowCount.ToString();

        }

        private void btnScheduleTest_Click(object sender, EventArgs e)
        {
            if (clsTestAppointment.isTestAppointmentOpenExist(_LocalAppID, (int)_TestType))
            {
                MessageBox.Show("This Person has Already Open Appointment", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (clsTest.isAppPassedTest(_LocalAppID, (int)_TestType))
            {
                MessageBox.Show("This Person is Already Passed This Test", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmScheduleTest scheduleTestForm=new frmScheduleTest(_LocalAppID,(int)_TestType);
            scheduleTestForm.ShowDialog();
            frmVisionTestAppointment_Load(null, null);

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int testAppointmentID=(int)dataGridView1.CurrentRow.Cells[0].Value;
            frmScheduleTest scheduleTestForm = new frmScheduleTest(testAppointmentID);
            scheduleTestForm.ShowDialog();
            frmVisionTestAppointment_Load(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int testAppointmentID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmTakeTest takeTestForm = new frmTakeTest(testAppointmentID);
            takeTestForm.ShowDialog();
            frmVisionTestAppointment_Load(null,null);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool isLocked = (bool)dataGridView1.CurrentRow.Cells[3].Value;
           
                takeTestToolStripMenuItem.Enabled = !isLocked;
                editToolStripMenuItem.Enabled = !isLocked;
            
        }
    }
}
