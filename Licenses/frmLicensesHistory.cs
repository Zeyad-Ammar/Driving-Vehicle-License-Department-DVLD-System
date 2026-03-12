using BusinessLayer;
using DVLD_Project.Licenses.InternationalLicenses;
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
    public partial class frmLicensesHistory : Form
    {
        private int _PersonID = -1;
        private clsDriver _Driver=new clsDriver();
        private DataTable _LocalLicensesList=new DataTable();
        private DataTable _InternationalLicensesList = new DataTable();
        public frmLicensesHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _Driver=clsDriver.GetDriverByPersonID(PersonID);
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            ucPersonReadCard1.setCardData(_PersonID);
            _LocalLicensesList=clsLicense.GetAllLicensesOfDriver(_Driver.DriverID);
            dgvLocalLicense.DataSource = _LocalLicensesList;
            if (dgvLocalLicense.Rows.Count > 0)
            {
                dgvLocalLicense.Columns[0].Width = 100;
                dgvLocalLicense.Columns[1].Width = 100;
                dgvLocalLicense.Columns[2].Width = 250;
                dgvLocalLicense.Columns[3].Width = 100;
                dgvLocalLicense.Columns[4].Width = 100;
                dgvLocalLicense.Columns[5].Width = 100;

            }
            _InternationalLicensesList = clsInternationalLicense.GetAllInternationalLicenseOfDriver(_Driver.DriverID);
            dgvInternationalLicenses.DataSource = _InternationalLicensesList;

            lbRecords.Text=dgvLocalLicense.RowCount.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                lbRecords.Text = dgvLocalLicense.RowCount.ToString();
            }
            else
            {
                lbRecords.Text = dgvInternationalLicenses.RowCount.ToString();
            }
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalLicenseID=(int)dgvLocalLicense.CurrentRow.Cells[0].Value;
            frmLicenseCard licenseCardForm = new frmLicenseCard(LocalLicenseID);
            licenseCardForm.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID=(int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;
            frmInternationalLicenseCard internationalLicenseCardForm = new frmInternationalLicenseCard(InternationalLicenseID);
            internationalLicenseCardForm.ShowDialog();
        }
    }
}
