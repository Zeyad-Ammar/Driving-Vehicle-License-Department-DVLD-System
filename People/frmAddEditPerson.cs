using BusinessLayer;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmAddEditPerson : Form
    {
       
        private clsPerson Person=new clsPerson();
        public delegate void  PersonInfo(int person);
        public event PersonInfo PersonData;
        public frmAddEditPerson()
        {
            InitializeComponent();
            llbRemoveImage.Visible = false;
        }

        public frmAddEditPerson(int PersonID)
        {
            
            InitializeComponent();
           
          
            Person = clsPerson.GetPerson(PersonID);
            
           
        }

        private int getCountryIndexInCompoBox(string countryName)
        {
            for (int i = 0; i < cbCountry.Items.Count; i++)
            {
                if (cbCountry.Items[i].ToString() == countryName)
                {
                    return i;
                }
            }

            return -1;
        }
        private void LoadDataPersonDataToControls()
        {
            lbPersonIDValue.Text =Person.PersonID.ToString();
            lbNAValue.Text = Person.NationalNumber;
           tbFirstName.Text=(Person.FirstName);  
            tbSecondName.Text = (Person.SecondName);
            tbThirdName.Text=(Person.ThirdName);
            tbLastName.Text = (Person.LastName);
            tbNationalNumber.Text = (Person.NationalNumber);
            dtpDateOfBirth.Value=(Person.DateOfBirth);
            
            rbMale.Checked = (Person.Gender == true);
            rbFemale.Checked = (Person.Gender == false);
            tbPhone.Text=(Person.Phone);
            tbEmail.Text=(Person.Email);
            cbCountry.SelectedIndex=getCountryIndexInCompoBox(Person.Country.CountryName);
            rtbAddress.Text=(Person.Address);
            pbProfilePhoto.ImageLocation=(Person.ProfilePhotoURL);

        }

        private void LoadCountriesToTheComboBox(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }

            cbCountry.SelectedIndex = 50;
        }
        private void LoadDataToThePersonObj()
        {
            Person.FirstName= tbFirstName.Text;
            Person.SecondName=tbSecondName.Text;
            Person.ThirdName=tbThirdName.Text;
            Person.LastName=tbLastName.Text;
            Person.NationalNumber=tbNationalNumber.Text;
            Person.DateOfBirth=dtpDateOfBirth.Value;
            Person.Gender=(rbMale.Checked==true);
            Person.Phone=tbPhone.Text;
            Person.Email=tbEmail.Text;
            Person.NationalityCountryID=clsCountry.getCountryID(cbCountry.Text);
            Person.Country = clsCountry.getCountry(Person.NationalityCountryID);
            Person.Address=rtbAddress.Text;
            Person.ProfilePhotoURL=pbProfilePhoto.ImageLocation;



        }
        
        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        private bool CheckValidation()
        {

            if (string.IsNullOrEmpty(tbFirstName.Text))
            {
                errorProvider1.SetError(tbFirstName, "The FirstName  is Required");
                return false;
            }

            if (string.IsNullOrEmpty(tbSecondName.Text))
            {
                errorProvider1.SetError(tbSecondName, "The SecondName  is Required");
                return false;
            }
            if (string.IsNullOrEmpty(tbLastName.Text))
            {
                errorProvider1.SetError(tbLastName, "The LastName is Required");
                return false;
            }

            if (string.IsNullOrEmpty(tbNationalNumber.Text))
            {
                errorProvider1.SetError(tbNationalNumber, "The National Number  is Required");
                return false;
            }

            if ( tbNationalNumber.Text!=Person.NationalNumber && clsPerson.isPersonWithNationalNoExist(tbNationalNumber.Text))
            {
                errorProvider1.SetError(tbNationalNumber, "The National No is Already Exist");
                return false;
            }

            if ( !string.IsNullOrEmpty(tbEmail.Text)&&tbEmail.Text!=Person.Email&&clsPerson.isPersonWithEmailExist(tbEmail.Text)) {
                
                errorProvider1.SetError(tbEmail, "The Email No is Already Exist");
                return false;
            }

            if (!string.IsNullOrEmpty(tbEmail.Text)&&!clsValidation.ValidateEmail(tbEmail.Text))
            {
                errorProvider1.SetError(tbEmail, "The Email pattern wrong");
                return false;
            }

            if (string.IsNullOrEmpty(tbPhone.Text))
            {
                errorProvider1.SetError(tbPhone, "The Phone  is Required");
                return false;
            }
            if ( tbPhone.Text!=Person.Phone && clsPerson.isPersonWithPhoneExist(tbPhone.Text))
            {
                errorProvider1.SetError(tbPhone, "The Phone No is Already Exist");
                return false;
            }


            if (!clsValidation.ValidateInteger(tbPhone.Text))
            {
                errorProvider1.SetError(tbPhone, "The Phone Number should contain Numbers Only");
                return false;
            }

            if (string.IsNullOrEmpty(rtbAddress.Text))
            {
                errorProvider1.SetError(rtbAddress, "The FirstName  is Required");
                return false;
            }

            errorProvider1.SetError(gbNewPersonData, "");

            return true;
        }

        private bool HandlePersonImage()
        {

            if (Person.ProfilePhotoURL != pbProfilePhoto.ImageLocation) {
                if (!string.IsNullOrEmpty(Person.ProfilePhotoURL))
                {
                    try
                    {
                        File.Delete(Person.ProfilePhotoURL);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error To Delete the Old Image");
                    }
                    
                }

                if (!string.IsNullOrEmpty(pbProfilePhoto.ImageLocation))
                {
                    string sourcePath=pbProfilePhoto.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref sourcePath)) {
                        
                        pbProfilePhoto.ImageLocation = sourcePath;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                   
                }

            }

            return true;
        }

        private void btnSavePerson_Click(object sender, EventArgs e)
        {
           
             if (!CheckValidation())
             {
                 return;
             }

            if (!HandlePersonImage())
            {
                return;
            }
            LoadDataToThePersonObj();



           
           
            Person.Save();

            lbFrmName.Text = "Update Person Info.";
            lbPersonIDValue.Text=Person.PersonID.ToString();
            lbNAValue.Text=Person.NationalNumber.ToString();

            //sent the person data back to the caller form.
            if (PersonData != null)
            {
                  PersonData?.Invoke(Person.PersonID);
            }

            MessageBox.Show("Person Added Successfully","Done",MessageBoxButtons.OK);
        }

        private void frmAddNewPerson_Load(object sender, EventArgs e)
        {
            DateTime mxDate=  DateTime.Now;
            mxDate=mxDate.AddYears(-19);
            rbMale.Checked = true;
            dtpDateOfBirth.MaxDate=mxDate;
            LoadCountriesToTheComboBox(clsCountry.getAllCountries());

            if (Person.PersonID != -1)
            {
                LoadDataPersonDataToControls();
                llbRemoveImage.Visible = !string.IsNullOrEmpty(Person.ProfilePhotoURL);
                lbFrmName.Text = "Update Person Data";
            }
        }

       
        private void rbMale_CheckedChanged_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pbProfilePhoto.ImageLocation))
            {
                pbProfilePhoto.Image = Resources.Male_512;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pbProfilePhoto.ImageLocation))
            {
                pbProfilePhoto.Image = Resources.Female_512;
            }
        }

        private void llbRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            pbProfilePhoto.ImageLocation = "";
            if (rbMale.Checked)
            {

                pbProfilePhoto.Image = Resources.Male_512;

            }
            else
            {
                pbProfilePhoto.Image = Resources.Female_512;

            }
        
        }

        private void llbSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            openFileDialog1.Title = "Choose Ur Profile Image";
            openFileDialog1.InitialDirectory = @"D:\";
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string imageLocation=openFileDialog1.FileName;
                pbProfilePhoto.ImageLocation = imageLocation;
            }

            llbRemoveImage.Visible = true;

        
        }

    }
}
