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

namespace DVLD_Project.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
       public enum enFilterBase { PersonID,NationalNo};

        private clsPerson _Person=new clsPerson();

        public enFilterBase enFilter=enFilterBase.PersonID;
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public bool isFilterEnable { get { 
            return gbFilter.Enabled;
            }
            
            set {
                gbFilter.Enabled = value;
            }
        }
        private void gbFilter_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            enFilter = (enFilterBase)comboBox1.SelectedIndex;

        }

        public void SetPersonData(int PersonID)
        {
            _Person=clsPerson.GetPerson(PersonID);
            ucPersonReadCard1.setCardData(PersonID);
            comboBox1.SelectedIndex = 0;
            tbFilterValue.Text = _Person.PersonID.ToString();

        }
        private void NewPersonBackData(int PersonID)
        {
            _Person = clsPerson.GetPerson(PersonID);
            ucPersonReadCard1.setCardData(_Person.PersonID);
            comboBox1.SelectedIndex = 0;
            tbFilterValue.Text=_Person.PersonID.ToString();
        }
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson addEditPersonForm=new frmAddEditPerson();
            addEditPersonForm.PersonData += NewPersonBackData;
            addEditPersonForm.ShowDialog();

        }

        public int getSelectedPersonID()
        {
            return ucPersonReadCard1.GetPersonID();
        }
        private bool LoadPersonData(int PersonID)
        {
            _Person=clsPerson.GetPerson(PersonID);

           return  _Person.PersonID != -1;
        }

        private bool LoadPersonData(string NationalNo)
        {
            _Person = clsPerson.GetPerson(NationalNo);

            return _Person != null;
        }

        private void btnSearchPerson_Click(object sender, EventArgs e)
        {
            if (enFilter == enFilterBase.PersonID)
            {
                int ID = -1;
                int.TryParse(tbFilterValue.Text, out ID);
                if (LoadPersonData(ID))
                {
                    ucPersonReadCard1.setCardData(_Person.PersonID);
                }
                else
                {
                    MessageBox.Show($"The Person With ID {tbFilterValue.Text} Not Exist", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (LoadPersonData(tbFilterValue.Text))
                {
                    ucPersonReadCard1.setCardData(_Person.PersonID);
                }
                else
                {
                    MessageBox.Show($"The Person With NationalNo {tbFilterValue.Text} Not Exist", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tbFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(enFilter == enFilterBase.PersonID)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearchPerson.PerformClick();
            }
        }
    }
}
