using BusinessLayer;
using DVLD_Project.Properties;
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
    public partial class ctrlPersonReadCard : UserControl
    {
        private clsPerson _Person=new clsPerson();
        private int _PersonID=-1;
        public ctrlPersonReadCard()
        {
            InitializeComponent();
        }

        public int GetPersonID()
        {
            int ID = -1;
            int.TryParse(lbPersonIDValue.Text, out ID);
            return ID;
        }
        public void setCardData(int PersonID)
        {
            _PersonID = PersonID;
            _Person=clsPerson.GetPerson(_PersonID);
            lbPersonIDValue.Text = _Person.PersonID.ToString();
            lbNameValue.Text = _Person.FullName;
            lbNationalNoValue.Text = _Person.NationalNumber;
            lbDateOfBirthValue.Text = _Person.DateOfBirth.ToShortDateString();
            lbGenderValue.Text = "Male";
            pbProfilePhoto.Image = Resources.Male_512;
            pbGenderImage.Image = Resources.Man_32;
            if (_Person.Gender == false)
            {
                lbGenderValue.Text = "Female";
                pbProfilePhoto.Image = Resources.Female_512;
                pbGenderImage.Image = Resources.Woman_32;
            }
            lbPhoneValue.Text = _Person.Phone;
            if (!string.IsNullOrEmpty(_Person.Email))
            {
                lbEmailValue.Text = _Person.Email;
            }
            else
            {
                lbEmailValue.Text = "No Email";
            }

            lbCountryValue.Text = clsCountry.getCountryName(_Person.NationalityCountryID);
            lbAddressValue.Text = _Person.Address;
            if(!string.IsNullOrEmpty(_Person.ProfilePhotoURL)) 
             {
                
                pbProfilePhoto.ImageLocation=(_Person.ProfilePhotoURL);
             }

        }

        private void ucPersonReadCard_Load(object sender, EventArgs e)
        {

        }

        private void UpdatePersonData(int PersonID)
        {

           setCardData(PersonID);


        }
        private void lbEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = -1;
            int.TryParse(lbPersonIDValue.Text.Trim(),out PersonID);

            if (PersonID == 0)
            {
                MessageBox.Show("No Person Selected!");
                return;
            }
            frmAddEditPerson PersonCard = new frmAddEditPerson(PersonID);
            PersonCard.PersonData += UpdatePersonData;
            PersonCard.ShowDialog();
        }
    }
}
