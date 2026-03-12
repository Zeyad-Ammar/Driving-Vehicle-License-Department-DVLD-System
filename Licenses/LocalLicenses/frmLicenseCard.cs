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

namespace DVLD_Project.Licenses
{
    public partial class frmLicenseCard : Form
    {
    
        private int _LicensesID=-1;
        public frmLicenseCard( int licensesID)
        {
            InitializeComponent();
           
            _LicensesID = licensesID;
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLicenseCard_Load(object sender, EventArgs e)
        {
            
                ctrlLicenseCard1.SetLicenseDataByLicenseID(_LicensesID);
          
        }
    }
}
