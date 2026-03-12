using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLayer
{
    public class clsPerson
    {
        public enum enMode { New=1,Update=2};

        public enMode Mode= enMode.New;
        public int PersonID { get; set; }

        public string NationalNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }

        public string ProfilePhotoURL { get; set; }
        
        public clsCountry Country { get; set; }

        public string FullName { get {  return FirstName+' '+SecondName+' '+ThirdName+' '+LastName; } }
        public clsPerson()
        {
            PersonID = -1;
            NationalNumber = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Gender = false;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            Country = new clsCountry();
            ProfilePhotoURL = "";
            Mode= enMode.New;
        }

        private clsPerson(int personID, string nationalNumber, string firstName, string secondName, string thirdName, string lastName,
           DateTime dateOfBirth, bool gender, string address, string phone, string email, int nationalityCountryID,string prfilePhotoURL)
        {
            
            PersonID = personID;
            NationalNumber = nationalNumber;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            Phone = phone;
            Email = email;
            NationalityCountryID = nationalityCountryID;
            Country= clsCountry.getCountry(nationalityCountryID);
            ProfilePhotoURL = prfilePhotoURL;
            Mode = enMode.Update;

        }

        public static clsPerson GetPerson(int PersonID)
        {
            string nationalNumber="", firstName="", secondName= "", thirdName = "", lastName = "";
            DateTime dateOfBirth=DateTime.Now; 
            bool gender=false; 
            string address = "",  phone = "",  email = "";
            int nationalityCountryID=0;
            string ImageUrl="";

            if(!People.GetPerson(PersonID,ref nationalNumber,ref firstName,ref secondName,ref thirdName,ref lastName,
                ref dateOfBirth,ref gender,ref address, ref phone,ref email, ref nationalityCountryID,ref ImageUrl))
            {
                return new clsPerson();
            }

            return new clsPerson(PersonID, nationalNumber, firstName, secondName, thirdName, lastName,
            dateOfBirth, gender, address, phone, email, nationalityCountryID,ImageUrl);
        }

        public static bool isPersonExist(int PersonID)
        {
            return GetPerson(PersonID).PersonID!=-1;
        }

        public static clsPerson GetPerson(string NationalNo)
        {
            int PersonID = -1;
            string  firstName = "", secondName = "", thirdName = "", lastName = "";
            DateTime dateOfBirth = DateTime.Now;
            bool gender = false;
            string address = "", phone = "", email = "";
            int nationalityCountryID = 0;
            string ImageUrl = "";

            if (!People.GetPerson(NationalNo, ref PersonID, ref firstName, ref secondName, ref thirdName, ref lastName,
                ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityCountryID, ref ImageUrl))
            {
                return null;
            }

            return new clsPerson(PersonID, NationalNo, firstName, secondName, thirdName, lastName,
            dateOfBirth, gender, address, phone, email, nationalityCountryID, ImageUrl);
        }
        public static DataTable GetAllPeople()
        {
            return People.GetAllPeople();
        }

        private bool SaveAsNew()
        {
            PersonID= People.AddPerson(NationalNumber, FirstName, SecondName, ThirdName, LastName,
                DateOfBirth, Gender, Address, Phone, Email,NationalityCountryID,ProfilePhotoURL);
            return PersonID != -1;
        }

        private bool SaveAsUpdate()
        {
            return People.UpdatePerson(PersonID, NationalNumber, FirstName, SecondName, ThirdName, LastName,
                DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ProfilePhotoURL);
        }
        public bool Save()
        {
            bool Done = false;
            switch (Mode)
            {
                case enMode.New:
                    Done= SaveAsNew();
                    Mode = enMode.Update;
                    break;
                case enMode.Update:
                   Done= SaveAsUpdate();
                    break;
            }

            return Done;
        }

        public static bool isPersonWithNationalNoExist(string NationalNo)
        {
            return People.FindPersonWithNationalNo(NationalNo);
        }

        public static bool isPersonWithEmailExist(string Email) { 
            return People.FindPersonWithEmail(Email);
        }

        public static bool isPersonWithPhoneExist(string Phone)
        {
            return People.FindPersonWithPhone(Phone);
        }

        public static bool DeletePerson(int ID)
        {
            return People.DeletePersonID(ID);
        }

    }
}
