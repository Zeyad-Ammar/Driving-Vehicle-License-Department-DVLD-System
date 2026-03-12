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

namespace DVLD_Project.Licenses.Controls
{
    public partial class ctrlLicenseCardWithFilter : UserControl
    {
        private int _LicenseID = -1;
        public ctrlLicenseCardWithFilter()
        {
            InitializeComponent();
        }

        public event Action<int> onLicenseSelected;

        public bool EnableSearch
        {
            set
            {
                gbFilter.Enabled = value;
            }
        }

        public void SetLicenseID(int LicenseID)
        {
            _LicenseID= LicenseID;
            ctrlLicenseCard1.SetLicenseDataByLicenseID(LicenseID);
            tbLicenseID.Text = LicenseID.ToString();

        }
        public bool FocusOnSearchBox
        {
           
            set
            {
                if (value)
                {
                    tbLicenseID.Focus();
                }
            }
        }

        public void RefreshCard()
        {
            ctrlLicenseCard1.SetLicenseDataByLicenseID(_LicenseID);
        }
        protected virtual void LicenseSelected(int licenseID)
        {
            Action<int> handler = onLicenseSelected;
            if(handler != null)
            {
                handler(licenseID);
            }
        }
        private void tbLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) &&! char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if(e.KeyChar == (char)Keys.Enter)
                {
                    btnSearch.PerformClick();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int LicenseID = -1;
            int.TryParse(tbLicenseID.Text,out LicenseID);

            if(clsLicense.isLicenseExist(LicenseID))
            {
                ctrlLicenseCard1.SetLicenseDataByLicenseID(LicenseID);
                _LicenseID = LicenseID;
                //trigger the event to notify the parent form that a license has been selected
                if (onLicenseSelected != null)
                {
                    onLicenseSelected(LicenseID);
                }
            }
            else
            {
                MessageBox.Show("No License Found With This ID");
            }
        }
    }
}
