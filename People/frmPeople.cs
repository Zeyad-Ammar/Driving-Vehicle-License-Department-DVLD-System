using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmPeople : Form
    {
        enum enFilterBase { None=0, PersonID, NationalNo, FirstName, LastName, Gendor};

        private static  DataTable _PeopleList = clsPerson.GetAllPeople();

        private DataTable _People =_PeopleList.DefaultView.ToTable(false,"PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName", "DateOfBirth", "Gendor", "Phone", "Email", "CountryName");

        
        
        enFilterBase enFilter = enFilterBase.None;


        public frmPeople()
        {
            InitializeComponent();
        }


        private void frmPeople_Load(object sender, EventArgs e)
        {
            tbFilterValue.Visible= false;
            cbGender.Visible = false;
            cbFilterBy.SelectedIndex = 0;
            _PeopleList = clsPerson.GetAllPeople();
            _People = _PeopleList.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName", "DateOfBirth", "Gendor", "Phone", "Email", "CountryName");

            dgvPeopleList.DataSource = _People;
            if(dgvPeopleList.Columns.Count>0)
            {
                dgvPeopleList.Columns[0].Width = 100;
                dgvPeopleList.Columns[1].Width = 100;
                dgvPeopleList.Columns[3].Width = 100;
                dgvPeopleList.Columns[2].Width = 100;
                dgvPeopleList.Columns[4].Width = 100;
                dgvPeopleList.Columns[5].Width = 100;
                dgvPeopleList.Columns[6].Width = 100;
                dgvPeopleList.Columns[7].Width = 100;
                dgvPeopleList.Columns[8].Width = 100;
                dgvPeopleList.Columns[9].Width = 200;
                dgvPeopleList.Columns[10].Width = 100;
            }

            lbNumOfRecordsValue.Text = dgvPeopleList.Rows.Count.ToString();
        }

      

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson addNewPerson = new frmAddEditPerson();
             addNewPerson.ShowDialog();
            frmPeople_Load(null,null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            
            enFilter = (enFilterBase)cbFilterBy.SelectedIndex;

            if (enFilter == enFilterBase.None)
            {
                tbFilterValue.Visible = false;
               
                
               
            }
            else if(enFilter==enFilterBase.Gendor)
            {
                tbFilterValue.Visible = false;
                cbGender.Visible = true;


            }
            else
            {
                tbFilterValue.Visible = true;
                cbGender.Visible = false;
                
            }
            _People.DefaultView.RowFilter = "";
            tbFilterValue.Text = "";
            cbGender.SelectedIndex = 0;
            lbNumOfRecordsValue.Text = dgvPeopleList.Rows.Count.ToString();


        }

      
        private void tbFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            

            if (enFilter == enFilterBase.PersonID)
            {
                if (!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;//block it.
                }
                
            }
            else if (enFilter == enFilterBase.Gendor)
            {
                if (!char.IsControl(e.KeyChar)&&!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
               
            }

            
            

        }

        private void tbFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilterValue.Text))
            {
                _People.DefaultView.RowFilter="";
            }
            else if (enFilter == enFilterBase.PersonID)
            {
                _People.DefaultView.RowFilter = string.Format(@"[{0}] = {1}", enFilter.ToString().Trim(), tbFilterValue.Text.Trim());
            } 
            else
            {
                _People.DefaultView.RowFilter = string.Format(@"[{0}] LIKE '%{1}%'", enFilter.ToString().Trim(), tbFilterValue.Text.Trim());
            }



            lbNumOfRecordsValue.Text = dgvPeopleList.Rows.Count.ToString();
        }

        private void smiEdit_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(dgvPeopleList.CurrentRow.Cells[0].Value);

            frmAddEditPerson addNewPerson = new frmAddEditPerson(id);
            addNewPerson.ShowDialog();
            frmPeople_Load(null, null);



        }

        private void smiShowDetail_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeopleList.CurrentRow.Cells[0].Value);
            
            
            
            frmReadPersonCard ReadPersonCardForm = new frmReadPersonCard(PersonID);
            ReadPersonCardForm.ShowDialog();
               
            
            frmPeople_Load(null, null);
        }

        private void smiAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson addNewPerson = new frmAddEditPerson();
            addNewPerson.ShowDialog();
            frmPeople_Load(null, null);
        }

        private void smiDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvPeopleList.CurrentRow.Cells[0].Value);

            if (MessageBox.Show($"Are U Sure to Delete Person With ID: {id}", "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK){
                    clsPerson.DeletePerson(id);
                   
            }

            frmPeople_Load(null, null);




        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGender.SelectedIndex == 0)
            {
                _People.DefaultView.RowFilter = "";
            }
            else
            {
                bool isMale = cbGender.SelectedIndex == 1;
                _People.DefaultView.RowFilter = string.Format(@"[{0}] = {1}", enFilter.ToString().Trim(), isMale);
            }
            lbNumOfRecordsValue.Text = dgvPeopleList.Rows.Count.ToString();
        }
    }
}
