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
    public partial class frmInternationalLicenseCard : Form
    {
        private int _InternationalLicenseID = -1;
        public frmInternationalLicenseCard(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID=InternationalLicenseID;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInternationalLicenseCard_Load(object sender, EventArgs e)
        {
            ctrlInternationalLicenseCard1.SetInternationalLicenseData(_InternationalLicenseID);
        }
    }
}
