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

namespace DVLD_Project.Applications.DetainAndReleaseLicense
{
    
    public partial class frmManageDetainedLicenses : Form
    {

        private DataTable _DetainedLicensesList = new DataTable();
        private enum enFilterBase { None=0, DetainID, NationalNo, FullName, ReleaseApplicationID, IsReleased   }
        private enFilterBase _Filter= enFilterBase.None;
        
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManagementDetainedLicenses_Load(object sender, EventArgs e)
        {
            _DetainedLicensesList= clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _DetainedLicensesList;
            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].Width = 80;
                dgvDetainedLicenses.Columns[1].Width = 80;
                dgvDetainedLicenses.Columns[2].Width = 80;
                dgvDetainedLicenses.Columns[3].Width = 80;
                dgvDetainedLicenses.Columns[4].Width = 80;
                dgvDetainedLicenses.Columns[5].Width = 80;
                dgvDetainedLicenses.Columns[6].Width = 80;
                dgvDetainedLicenses.Columns[7].Width = 300;
                dgvDetainedLicenses.Columns[8].Width = 130;



            }
            cbFilterBy.SelectedIndex = 0;
            tbFilterBy.Visible = false;
            cbisReleased.Visible = false;
            _Filter = enFilterBase.None;
            lbRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Filter = (enFilterBase)cbFilterBy.SelectedIndex;
            if (_Filter == enFilterBase.None)
            {     
                tbFilterBy.Visible = false;  
                cbisReleased.Visible = false;
               
            }
            else if (_Filter == enFilterBase.IsReleased)
            {
                
              
                tbFilterBy.Visible = false;      
                cbisReleased.Visible = true;
              
            }
            else
            {                       
                tbFilterBy.Visible = true;      
                cbisReleased.Visible = false;
            }
            tbFilterBy.Text = "";
            cbisReleased.SelectedIndex = 0;
            _DetainedLicensesList.DefaultView.RowFilter = "";
            lbRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbFilterBy.Text)||_Filter==enFilterBase.None)
            {
                _DetainedLicensesList.DefaultView.RowFilter = "";
               
            }
            else if (_Filter==enFilterBase.DetainID|| _Filter == enFilterBase.ReleaseApplicationID)
            {
                _DetainedLicensesList.DefaultView.RowFilter = $"{_Filter} = {tbFilterBy.Text.Trim()}";
            }
            else
            {
                _DetainedLicensesList.DefaultView.RowFilter = $"{_Filter} LIKE '%{tbFilterBy.Text}%'";
            }

            lbRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(_Filter==enFilterBase.DetainID|| _Filter == enFilterBase.ReleaseApplicationID)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void cbisReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
             if (_Filter == enFilterBase.IsReleased)
            {
                if (cbisReleased.SelectedIndex == 0)
                {
                    _DetainedLicensesList.DefaultView.RowFilter = "";
                }
                else
                {
                    bool isReleased = cbisReleased.SelectedIndex == 1 ? true : false;
                    _DetainedLicensesList.DefaultView.RowFilter = $"IsReleased = {isReleased}";
                }

            }
            lbRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void cbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense detainLicenseForm = new frmDetainLicense();
            detainLicenseForm.ShowDialog();
            ManagementDetainedLicenses_Load(null, null);
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense releaseDetainedLicenseForm = new frmReleaseDetainedLicense();
            releaseDetainedLicenseForm.ShowDialog();
            ManagementDetainedLicenses_Load(null, null);
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
             
            frmReadPersonCard readPersonCardForm = new frmReadPersonCard(clsLicense.GetLicenseByLicenseID(LicenseID).DriverInfo.PersonID);
            readPersonCardForm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmLicenseCard licenseCardForm = new frmLicenseCard(LicenseID);
            licenseCardForm.ShowDialog();
        }

        private void shToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicensesHistory licensesHistoryForm = new frmLicensesHistory(clsLicense.GetLicenseByLicenseID((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value).DriverInfo.PersonID);
            licensesHistoryForm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool isReleased = (bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value;
            releaseLicenseToolStripMenuItem.Enabled = !isReleased;

        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DetainID = (int)dgvDetainedLicenses.CurrentRow.Cells[0].Value;
            frmReleaseDetainedLicense releaseDetainedLicenseForm = new frmReleaseDetainedLicense(DetainID);
            releaseDetainedLicenseForm.ShowDialog();
            ManagementDetainedLicenses_Load(null, null);
        }
    }
}
