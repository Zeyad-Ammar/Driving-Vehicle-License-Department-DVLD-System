using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.LocalApplications
{
    public partial class frmApplicationDetails : Form
    {
        private int _LocalAppID = -1;
        public frmApplicationDetails(int LocalAppID)
        {
            InitializeComponent();
            _LocalAppID = LocalAppID;
        }
        
        private void frmApplicationDetails_Load(object sender, EventArgs e)
        {
            ctrlLocalApplicationCard1.SetLocalDrivingLicenseAppData( _LocalAppID);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
