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

namespace DVLD_Project.Users
{
    public partial class frmUserCard : Form
    {
        private clsUser _User=new clsUser();
      
        public frmUserCard(int UserID)
        {
            InitializeComponent();
            ctrlUserCard1.SetUserInfo(UserID);

        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
