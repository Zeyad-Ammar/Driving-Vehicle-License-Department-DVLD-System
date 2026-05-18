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
    public partial class frmChangePassword : Form
    {
        private clsUser _User=new clsUser();
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _User=clsUser.GetUser(UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _CheckCurrentPassword(string password)
        {

            return _User.Password == clsUtil.GetHash(password);
        }
        private bool _CheckValidation()
        {
            if (string.IsNullOrEmpty(tbCurrentPassword.Text))
            {
                errorProvider1.SetError(tbCurrentPassword, "The Current Password is Required");
                return false;
            }
            else
            {
                errorProvider1.SetError(tbCurrentPassword, "");
            }

            if (string.IsNullOrEmpty(tbNewPassword.Text))
            {
                errorProvider1.SetError(tbNewPassword, "The Current Password is Required");
                return false;
            }
            else {
                errorProvider1.SetError(tbNewPassword, "");
            }

            if (string.IsNullOrEmpty(tbConfirmPassword.Text))
            {
                errorProvider1.SetError(tbConfirmPassword, "The Current Password is Required");
                return false;
            }
            else
            {
                errorProvider1.SetError(tbConfirmPassword, "");
            }

            if (!_CheckCurrentPassword(tbCurrentPassword.Text))
            {
                errorProvider1.SetError(tbCurrentPassword, "The Current Password is Wrong");
                return false;
            }
            else
            {
                errorProvider1.SetError(tbCurrentPassword, "");
            }

            if (tbNewPassword.Text != tbConfirmPassword.Text)
            {
                errorProvider1.SetError(tbConfirmPassword, "The New Password and Confirm Password Not matched");
                return false;
            }
            else
            {
                errorProvider1.SetError(tbConfirmPassword, "");
            }


            return true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_CheckValidation()) {
                return;
            }
            else
            {
                _User.Password = clsUtil.GetHash(tbNewPassword.Text) ;
            }



            if (_User.Save())
            {
                MessageBox.Show("The Data Updated Successfully");
            }
            else
            {
                MessageBox.Show("Error While Updating Data");
            }

            tbConfirmPassword.Text = tbNewPassword.Text = tbCurrentPassword.Text = "";

        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.SetUserInfo(_User.UserID);
        }
    }
}
