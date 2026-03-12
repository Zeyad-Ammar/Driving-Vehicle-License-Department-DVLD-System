using BusinessLayer;
using DVLD_Project.Users;
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
    public partial class frmUsers : Form
    {
       
        public enum enFilterBase { None, PersonID, UserID, FullName, UserName, IsActive };
        public enFilterBase enFilter = enFilterBase.None;
        private static DataTable _UsersList = clsUser.GetAllUsers();
        private DataTable _Users = _UsersList.DefaultView.ToTable(false, "UserID", "PersonID", "FullName", "UserName", "IsActive");



        public frmUsers()
        {
            InitializeComponent();
        }

        private void RefreshDataGridView()
        {
            _UsersList = clsUser.GetAllUsers();
            _Users = _UsersList.DefaultView.ToTable(false, "UserID", "PersonID", "FullName", "UserName", "IsActive");

            dgvUsersList.DataSource = _Users;

            if(dgvUsersList.Rows.Count > 0) {

                dgvUsersList.Columns[0].Width = 100;
                dgvUsersList.Columns[1].Width = 100;
                dgvUsersList.Columns[2].Width = 300;
                dgvUsersList.Columns[3].Width = 100;
                dgvUsersList.Columns[4].Width = 90;

            }
           



            lbRecordsValue.Text = dgvUsersList.RowCount.ToString();


        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex= 0;
            cbIsActive.Visible = false;
            tbFilterValue.Visible = false;
            RefreshDataGridView();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            enFilter = (enFilterBase)comboBox1.SelectedIndex;

            if (enFilter == enFilterBase.None)
            {
                _Users.DefaultView.RowFilter = "";
            }
            else if (enFilter == enFilterBase.IsActive)
            {
                cbIsActive.Visible = true;
                tbFilterValue.Visible = false;

            }
            else
            {
                cbIsActive.Visible = false;
                tbFilterValue.Visible = true;

            }

            tbFilterValue.Text = "";
            cbIsActive.SelectedIndex = 0;
            lbRecordsValue.Text = dgvUsersList.RowCount.ToString();
        }

        private void tbFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (enFilter == enFilterBase.None || string.IsNullOrEmpty(tbFilterValue.Text))
            {
                _Users.DefaultView.RowFilter = "";

            }
            else if (enFilter == enFilterBase.PersonID || enFilter == enFilterBase.UserID)
            {
                _Users.DefaultView.RowFilter = string.Format("[{0}] = {1}", enFilter, tbFilterValue.Text.Trim());
            }
            else
            {
                _Users.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", enFilter, tbFilterValue.Text.Trim());

            }

            lbRecordsValue.Text = dgvUsersList.RowCount.ToString();
        }

        private void tbFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (enFilter == enFilterBase.PersonID || enFilter == enFilterBase.UserID) {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) { 
                    e.Handled = true;//not accept it
                }
            }

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser AddUserForm=new frmAddEditUser();
            AddUserForm.ShowDialog();
            RefreshDataGridView();
        }

        private void cmsiDeleteUser_Click(object sender, EventArgs e)
        {
            int UserID = -1;
            int.TryParse(dgvUsersList.CurrentRow.Cells[0].Value.ToString(),out UserID);
            if (UserID != -1) {
                if (MessageBox.Show("Are U sure To Delete User With ID {UserID}!", "Careful", MessageBoxButtons.OKCancel) == DialogResult.OK) {

                    clsUser.DeleteUser(UserID);
                    
                }
                
            }

            RefreshDataGridView();


        }

        private void cmsiEditUser_Click(object sender, EventArgs e)
        {
            int UserID = -1;
            int.TryParse(dgvUsersList.CurrentRow.Cells[0].Value.ToString(), out UserID);
            if (UserID != -1)
            {

                frmAddEditUser AddEditUserForm=new frmAddEditUser(UserID);
                AddEditUserForm.ShowDialog();


            }

            RefreshDataGridView();
        }

        private void cmsiAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser AddUserForm = new frmAddEditUser();
            AddUserForm.ShowDialog();
            RefreshDataGridView();


        }

        private void cmsiShowDetails_Click(object sender, EventArgs e)
        {
           
           
            int UserID = -1;
            int.TryParse(dgvUsersList.CurrentRow.Cells[0].Value.ToString(), out UserID);
            if (UserID != -1)
            {

                frmUserCard UserCardForm = new frmUserCard(UserID);
                UserCardForm.ShowDialog();


            }

            RefreshDataGridView();
        }

        private void cmsiChangePassword_Click(object sender, EventArgs e)
        {
            int UserID = -1;
            int.TryParse(dgvUsersList.CurrentRow.Cells[0].Value.ToString(), out UserID);
            if (UserID != -1)
            {

                frmChangePassword changePasswordForm = new frmChangePassword(UserID);
                changePasswordForm.ShowDialog();


            }

            RefreshDataGridView();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsActive.SelectedIndex == 0) {
                _Users.DefaultView.RowFilter = "";
            }
            else
            {
                bool isActive = cbIsActive.SelectedIndex == 1;
                _Users.DefaultView.RowFilter = string.Format("[{0}] = {1}", enFilter, isActive);
            }
             lbRecordsValue.Text = dgvUsersList.RowCount.ToString();
        }
    }
}
