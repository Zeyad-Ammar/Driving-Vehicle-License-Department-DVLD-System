using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class clsUser
    {
        public enum enMode { New,Update};

        public enMode Mode= enMode.New;
        public int PersonID { get; set; }

        public clsPerson PersonInfo { get; set; }
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool isActive { get; set; }


        public clsUser()
        {
            Mode=enMode.New;
            PersonID = -1;
            UserID = -1;
            UserName = "";
            Password = "";
            PersonInfo = new clsPerson();
            isActive = false;

        }

        private clsUser(int PersonID,int UserID,string UserName,string Password,bool isActive)
        {
            Mode = enMode.Update;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.GetPerson(PersonID);
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.isActive = isActive;

        }

        private bool _AddUser()
        {
            UserID= Users.AddUser(PersonID, UserName, Password, isActive);
            return UserID != -1;
        }

        private bool _UpdateUser()
        {
            return Users.UpdateUser(UserID, UserName, Password, isActive);
        }

        
        public bool Save()
        {
            bool isDone= false; 
            switch (Mode) { 
                case enMode.New:
                    isDone= _AddUser();
                    Mode = enMode.Update;

                    break;
                case enMode.Update:
                    isDone = _UpdateUser();
                break;
            }

            return isDone;
        }

        public static bool DeleteUser(int UserID)
        {
            return Users.DeleteUser(UserID);
        }
        public static DataTable GetAllUsers()
        {
            return Users.GetAllUsers();
        }

        public static clsUser GetUser(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool isActive=false;
             if(!Users.GetUser(UserID, ref PersonID, ref UserName,ref Password, ref isActive))
            {
                return new clsUser();
            }

             return new clsUser(PersonID,UserID, UserName,Password,isActive); 
        }

        public static bool isPersonUser(int PersonID)
        {
           
           
            if (!Users.isPersonUser( PersonID))
            {
                return false;
            }

            return true;
        }

        public static clsUser FindAccount(string UserName,string Password)
        {
            int PersonID = -1,UserID=-1;
            
           
            bool isActive = false;
            if (!Users.GetUser(UserName,ref UserID,ref PersonID,Password,ref isActive))
            {
                return new clsUser();
            }

            return new clsUser(PersonID, UserID, UserName, Password, isActive);
        }

        

    }
}
