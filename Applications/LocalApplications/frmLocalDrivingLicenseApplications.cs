using BusinessLayer;
using DVLD_Project.Applications.LocalApplications;
using DVLD_Project.Licenses;
using DVLD_Project.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace DVLD_Project
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        private DataTable _LocalDrivingLicenseApps=new DataTable();

        
        enum enFilterBase { None=0, LocalDrivingLicenseApplicationID, NationalNo, FullName, ApplicationStatus };

        private enFilterBase enFilter;
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApps = clsLocalDrivingLicenseApp.GetAllLocalLicenseApps();
            dataGridView1.DataSource= _LocalDrivingLicenseApps;
            cbFilterBy.SelectedIndex = 0;
            tbFilterByValue.Visible= false;
            enFilter = (enFilterBase)cbFilterBy.SelectedIndex;

            lbRecordsValue.Text=dataGridView1.RowCount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddEditLocalLicense localLicenseForm= new frmAddEditLocalLicense();
            localLicenseForm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null,null);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            enFilter = (enFilterBase)cbFilterBy.SelectedIndex;

            if (enFilter == enFilterBase.None)
            {
                tbFilterByValue.Visible=false;
                tbFilterByValue.Text = "";
                _LocalDrivingLicenseApps.DefaultView.RowFilter = "";
                return;
            }

            tbFilterByValue.Visible = true;
            
        }

        private void tbFilterByValue_TextChanged(object sender, EventArgs e)
        {



            if (string.IsNullOrEmpty(tbFilterByValue.Text))
            {
                _LocalDrivingLicenseApps.DefaultView.RowFilter = "";

            }
            else if (enFilter == enFilterBase.LocalDrivingLicenseApplicationID) {

                
                _LocalDrivingLicenseApps.DefaultView.RowFilter = string.Format($"{enFilter.ToString()} = {tbFilterByValue.Text.Trim()}");
            }
            else
            {
                _LocalDrivingLicenseApps.DefaultView.RowFilter = string.Format($"{enFilter.ToString()} like '{tbFilterByValue.Text.Trim()}%'");

            }

            lbRecordsValue.Text = dataGridView1.RowCount.ToString();
        }

        private void tbFilterByValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (enFilter == enFilterBase.LocalDrivingLicenseApplicationID && !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) { 
                e.Handled = true;
            }
        }

       

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int PassedTests = (int)dataGridView1.CurrentRow.Cells["PassedTests"].Value;

              cmsiVisionTest.Enabled = PassedTests==0;             
              cmsiWrittingTest.Enabled = PassedTests == 1;
              cmsiStreetTest.Enabled = PassedTests == 2;
              cmsiSchduelTests.Enabled = dataGridView1.CurrentRow.Cells["ApplicationStatus"].Value.ToString() == "New"&& PassedTests != 3;


            cmsiEditApp.Enabled = dataGridView1.CurrentRow.Cells["ApplicationStatus"].Value.ToString() == "New";
            cmsiDeleteApp.Enabled = PassedTests==0 && dataGridView1.CurrentRow.Cells["ApplicationStatus"].Value.ToString() == "New";
            cmsiCancelApp.Enabled = dataGridView1.CurrentRow.Cells["ApplicationStatus"].Value.ToString() == "New";

            cmsiShowLicense.Enabled= dataGridView1.CurrentRow.Cells["ApplicationStatus"].Value.ToString() == "Completed";
            cmsiIssueDrivingLicense.Enabled = PassedTests == 3 && dataGridView1.CurrentRow.Cells["ApplicationStatus"].Value.ToString() == "New";

        }

        private void cmsiCancelApp_Click(object sender, EventArgs e)
        {
            int localAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApp LDLicenseApp = clsLocalDrivingLicenseApp.GetLocalLicenseApp(localAppID);
            
            if (!LDLicenseApp.Cancel())
            {
                MessageBox.Show("Error");
                return;
            }
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void cmsiDeleteApp_Click(object sender, EventArgs e)
        {
            int localAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            if (!clsLocalDrivingLicenseApp.Delete(localAppID))
            {
                MessageBox.Show("Can't Delete This Local License Application");
            }
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void cmsiVisionTest_Click(object sender, EventArgs e)
        {
            int localAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            
            frmTestAppointmentsList visionTestAppointmentForm =new frmTestAppointmentsList(localAppID,clsTestType.enTestType.Vision);
            visionTestAppointmentForm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);

        }

        private void cmsiWrittingTest_Click(object sender, EventArgs e)
        {
            int localAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            frmTestAppointmentsList visionTestAppointmentForm = new frmTestAppointmentsList(localAppID, clsTestType.enTestType.Writting);
            visionTestAppointmentForm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void cmsiStreetTest_Click(object sender, EventArgs e)
        {
            int localAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            frmTestAppointmentsList visionTestAppointmentForm = new frmTestAppointmentsList(localAppID, clsTestType.enTestType.Street);
            visionTestAppointmentForm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void cmsiShowAppDetails_Click(object sender, EventArgs e)
        {
            int localAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmApplicationDetails applicationDetailsForm=new frmApplicationDetails(localAppID);
            applicationDetailsForm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void cmsiEditApp_Click(object sender, EventArgs e)
        {
            int LocalAppID =(int) dataGridView1.CurrentRow.Cells[0].Value;
            frmAddEditLocalLicense localLicenseForm = new frmAddEditLocalLicense(LocalAppID);
            localLicenseForm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void cmsiIssueDrivingLicense_Click(object sender, EventArgs e)
        {
            int LocalAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmIssueLicensce issueLicensceForm = new frmIssueLicensce(LocalAppID);
            issueLicensceForm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);

        }

        private void cmsiShowLicense_Click(object sender, EventArgs e)
        {
            int AppID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            frmLicenseCard licenseCardForm = new frmLicenseCard(clsLicense.GetLicenseByAppID(AppID).LicenseID);
            licenseCardForm.ShowDialog();

        }

        private void cmsiShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int localAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            
            frmLicensesHistory licenseHistoryForm = new frmLicensesHistory(clsLocalDrivingLicenseApp.GetLocalLicenseApp(localAppID).ApplicationPersonID);
            licenseHistoryForm.ShowDialog();
        }
    }
}
