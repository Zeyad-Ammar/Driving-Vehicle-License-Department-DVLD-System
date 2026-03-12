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

namespace DVLD_Project
{
    public partial class frmDrivers : Form
    {
        
        enum enFilterBase { None=0, DriverID , PersonID , NationalNo, FullName }
        private enFilterBase enFilter;
        private DataTable _Drivers=new DataTable();
        public frmDrivers()
        {
            InitializeComponent();
        }

        private void frmDrivers_Load(object sender, EventArgs e)
        {
            _Drivers = clsDriver.GetAllDrivers();
            dataGridView1.DataSource= _Drivers;
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 300;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 100;
            }


            cbFilterBase.SelectedIndex = 0;
            tbFilterValue.Visible = false;
            enFilter=enFilterBase.None;
            lbRecordsValue.Text=dataGridView1.RowCount.ToString();
        }

        private void cbFilterBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            enFilter=(enFilterBase)cbFilterBase.SelectedIndex;

            if (enFilter == enFilterBase.None)
            {
                tbFilterValue.Text = "";
                tbFilterValue.Visible=false;
                return;
            }

            tbFilterValue.Visible=true;

        }

        private void tbFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilterValue.Text.Trim())||enFilter==enFilterBase.None) {
                _Drivers.DefaultView.RowFilter = "";
            }

            else if (enFilter == enFilterBase.DriverID || enFilter == enFilterBase.PersonID) { 
                _Drivers.DefaultView.RowFilter = string.Format($"{enFilter.ToString()} = {tbFilterValue.Text.Trim()}");

            }
            else
            {
                _Drivers.DefaultView.RowFilter = string.Format($"{enFilter.ToString()} like '{tbFilterValue.Text.Trim()}%'");

            }

            lbRecordsValue.Text = dataGridView1.RowCount.ToString();

        }

        private void tbFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (enFilter == enFilterBase.DriverID || enFilter == enFilterBase.PersonID)
            {
                if (!char.IsControl(e.KeyChar)&& !char.IsDigit(e.KeyChar)) { 
                    e.Handled = true;
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void personInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dataGridView1.CurrentRow.Cells[1].Value;

            frmReadPersonCard personCardForm = new frmReadPersonCard(PersonID);
            personCardForm.ShowDialog();
            frmDrivers_Load(null,null);
        }

        private void licensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            frmLicensesHistory licenseHistoryForm = new frmLicensesHistory(PersonID);
            licenseHistoryForm.ShowDialog();

        }
    }
}
