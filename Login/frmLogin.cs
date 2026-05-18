using DVLD_Project.General;
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

       

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!clsCurrentUser.checkAuthentication(tbUserName.Text.Trim(), clsUtil.GetHash(tbPassword.Text.Trim()))) {
                MessageBox.Show("The Username Or Password is  Wrong","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (!clsCurrentUser.User.isActive)
            {
                MessageBox.Show("Your Account is Not Active Please Contact The Admin","Stop",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                return;
            }

            if (chkRememberMe.Checked) {
                clsCurrentUser.StoreCurrentUserCredentials(tbUserName.Text.Trim(),tbPassword.Text.Trim());
            }
           

            clsUtil.LogInformation($"User {clsCurrentUser.User.UserName} logged in. at {DateTime.Now}");
            MainForm mainForm = new MainForm(this);
            this.Hide();
            mainForm.ShowDialog();
            



        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName="",Password="";
            if (clsCurrentUser.LoadStoredCredentials(ref UserName,ref Password))
            {
                tbUserName.Text = UserName;
                tbPassword.Text = Password;
                chkRememberMe.Checked = true;
                btnLogin.Focus();
            }
            else
            {
                tbUserName.Text = "";
                tbPassword.Text = "";
                chkRememberMe.Checked = false;
                tbUserName.Focus();
            }
            

        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
