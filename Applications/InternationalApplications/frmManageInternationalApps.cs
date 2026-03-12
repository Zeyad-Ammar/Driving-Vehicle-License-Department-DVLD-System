using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Licenses.InternationalLicenses
{
    public partial class frmManageInternationalApps : Form
    {
        private DataTable _InternationalLicenseList=new DataTable();
        private enum enFilterBase { None=0, InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IsActive };
        private enFilterBase enFilter;
        public frmManageInternationalApps()
        {
            InitializeComponent();
        }

        private void frmManageInternationalApps_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            tbFilterByValue.Visible=false;
            cbActiveValue.Visible = false;
            _InternationalLicenseList =clsInternationalLicense.GetAllInternationalLicense();
            dgvInternationalLicenses.DataSource= _InternationalLicenseList;
            lbRecordsValue.Text= _InternationalLicenseList.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            enFilter=(enFilterBase)cbFilterBy.SelectedIndex;
            if(enFilter == enFilterBase.None)
            {
                
                tbFilterByValue.Visible = false;
                cbActiveValue.Visible = false;
                
            }
            else if (enFilter != enFilterBase.IsActive)
            {
                tbFilterByValue.Visible=true;
                cbActiveValue.Visible = false;
            }
            else
            {
                tbFilterByValue.Visible = false;
                cbActiveValue.Visible = true;
            }
            _InternationalLicenseList.DefaultView.RowFilter = "";
            tbFilterByValue.Text = "";
            cbActiveValue.SelectedIndex = 0;
        }

        private void tbFilterByValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilterByValue.Text.Trim()))
            {
                _InternationalLicenseList.DefaultView.RowFilter = "";
                return;
            }

           
            
              _InternationalLicenseList.DefaultView.RowFilter = $"{enFilter} = {tbFilterByValue.Text}";
            
          


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbActiveValue.SelectedIndex == 0) {
                _InternationalLicenseList.DefaultView.RowFilter = "";
                return;
            }
            bool ActiveFilterValue = true;
            if (cbActiveValue.SelectedIndex == 2) {
                ActiveFilterValue = false;
            }
            _InternationalLicenseList.DefaultView.RowFilter = $"{enFilter} = {ActiveFilterValue}";
        }

        private void btnAddInternationalLicense_Click(object sender, EventArgs e)
        {
            frmInternationalLicense internationalLicenseForm = new frmInternationalLicense();
            internationalLicenseForm.ShowDialog();
            frmManageInternationalApps_Load(null, null);
        }

        private void personInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppID=(int)dgvInternationalLicenses.CurrentRow.Cells[1].Value;
            
            frmReadPersonCard personCardForm=new frmReadPersonCard(clsApplication.GetApplication(AppID).ApplicationPersonID);
            personCardForm.ShowDialog();
        }

        private void licenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID= (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;
            frmInternationalLicenseCard internationalLicenseCardForm = new frmInternationalLicenseCard(InternationalLicenseID);
            internationalLicenseCardForm.ShowDialog();
        }

        private void personLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppID = (int)dgvInternationalLicenses.CurrentRow.Cells[1].Value;

            frmLicensesHistory licenseHistoryForm = new frmLicensesHistory(clsApplication.GetApplication(AppID).ApplicationPersonID);
            licenseHistoryForm.ShowDialog();
        }
    }
}
