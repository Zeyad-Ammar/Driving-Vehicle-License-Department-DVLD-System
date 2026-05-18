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
    public partial class frmAddEditUser : Form
    {
        public frmAddEditUser()
        {
            InitializeComponent();
        }

        private clsUser _User=new clsUser();

        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            _User=clsUser.GetUser(UserID);
            _LoadDataToTheForm();
            lbFormLableName.Text = "Update User Info";
            tbPassword.Enabled = tbConfirmPassword.Enabled = false;
            ctrlPersonCardWithFilter1.isFilterEnable = false;
            lbUserIDValue.Text = _User.UserID.ToString();


        }

        private void _LoadDataToTheForm()
        {
            
            tbUserName.Text = _User.UserName;
            chkIsActive.Checked = _User.isActive;
            tbPassword.Text = tbConfirmPassword.Text = _User.Password;
            ctrlPersonCardWithFilter1.SetPersonData(_User.PersonID);

            
        }

        
       

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private  bool CheckValidation()
        {
            if (string.IsNullOrEmpty(tbUserName.Text))
            {
                errorProvider1.SetError(tbUserName, "User Name Is Required");
                return false;
            }

            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                errorProvider1.SetError(tbPassword, "Password Is Required");
                return false;
            }

            if (string.IsNullOrEmpty(tbConfirmPassword.Text))
            {
                errorProvider1.SetError(tbConfirmPassword, "Confirm Password Is Required");
                return false;
            }

            if (tbPassword.Text != tbConfirmPassword.Text)
            {
                errorProvider1.SetError(tbConfirmPassword, "Password and Confirm Password Not Matched");
                return false;
            }

            errorProvider1.SetError(tabControl, "");



                return true;
        }

        private void LoadDataToUserObj()
        {
            _User.PersonID=ctrlPersonCardWithFilter1.getSelectedPersonID();
            _User.UserName=tbUserName.Text.Trim(); 
            _User.Password=clsUtil.GetHash(tbPassword.Text.Trim());
            _User.isActive=chkIsActive.Checked;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckValidation())
            {
                return;
            }

            LoadDataToUserObj();


            if (_User.Save())
            {
                MessageBox.Show("The Data is Saved Successfully");
            }

            lbFormLableName.Text = "Update User Info";

            lbUserIDValue.Text=_User.UserID.ToString();






        }

        

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (!clsPerson.isPersonExist(ctrlPersonCardWithFilter1.getSelectedPersonID()))
            {
                MessageBox.Show("U should Choose a Person First", "Not Person Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            if (_User.UserID==-1&&clsUser.isPersonUser(ctrlPersonCardWithFilter1.getSelectedPersonID()))
            {
                MessageBox.Show("This Person is Already a User", "Select Another Person", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            tabControl.SelectedIndex = 1;
  
            btnSave.Enabled = true;
        }

        

        private void frmAddUser_Load(object sender, EventArgs e)
        {
            
            btnSave.Enabled = false;
        }

       
    }
}
