using BusinessLayer;
using DVLD_Project.General;
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
    public partial class frmDetainLicense : Form
    {
        private clsLicense _License=new clsLicense();
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void tbFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
           llbShowLicenseHistory.Enabled=false;
            btnDetain.Enabled = false;
            tbFineFees.Enabled = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLicenseCardWithFilter1_onLicenseSelected(int obj)
        {
            int LicenseID = obj;
            _License = clsLicense.GetLicenseByLicenseID(LicenseID);
            if(_License.LicenseID==-1)
            {
                MessageBox.Show("Wrong License ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_License.isDetained())
            {
                MessageBox.Show("The License is Already Detained!", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            llbShowLicenseHistory.Enabled = true;
            btnDetain.Enabled = true;
            tbFineFees.Enabled = true;
            lbDetainDate.Text = DateTime.Now.ToShortDateString();
            lbCreatedBy.Text = clsCurrentUser.User.UserName;
            tbFineFees.Focus();
        }

        private void llbShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicensesHistory licensesHistoryForm= new frmLicensesHistory(_License.DriverInfo.PersonID);
            licensesHistoryForm.ShowDialog();
        }

        private bool _CheckValidations()
        {
            if (string.IsNullOrEmpty(tbFineFees.Text))
            {
                errorProvider1.SetError(tbFineFees, "Please Enter the Fine Fees!");
                return false;
            }
            else
            {
                errorProvider1.SetError(tbFineFees, "");
            }


            return true;
        }
        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!_CheckValidations())
            {
                MessageBox.Show("The Data Not Completed Put Mouse on Red Circle to Understand!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            int DetainID = _License.Detain(Convert.ToInt32(tbFineFees.Text), clsCurrentUser.User.UserID);
            if (DetainID != -1)
            {
                MessageBox.Show("License Detained Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ctrlLicenseCardWithFilter1.RefreshCard();
                btnDetain.Enabled = false;
            }
            else
            {
                MessageBox.Show("Error When Trying To Detain The License!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
