using DataLayer;
using DVLD_Project.General;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsApplication
    {
        public enum enMode { New,Update};
        public enMode Mode;
        public enum enApplicationTypes { LocalDL=1,RenewDL,Replacement4LostDL,Replacement4DamagedDL,ReleaseDetainedDL,NewIL,RetakeTest  };

        public enum enApplicationStatus { New=1,Cancelled,Completed };
         public int ApplicationID {  get; set; }

        public int ApplicationPersonID {  get; set; }
        public clsPerson PersonInfo { get;set; }
        public DateTime ApplicationDate {  get; }

        public enApplicationTypes ApplicationTypeID { get; set; }

        public enApplicationStatus ApplicationStatus { get; set; }

        public DateTime LastStatusDate { get;  }

        public decimal PaidFees {  get;  }

        public int CreatedByUserID { get; set; }

        public clsApplication(int AppPersonID, int AppType, int CreatedByUserID)
        {
            this.ApplicationID = -1;
            this.ApplicationPersonID = AppPersonID;
            this.PersonInfo = clsPerson.GetPerson(AppPersonID);
            this.ApplicationTypeID = (enApplicationTypes)AppType;
            this.ApplicationStatus = enApplicationStatus.New;
            this.ApplicationDate = DateTime.Now;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = CreatedByUserID;
            this.Mode = enMode.New;

        }
        public clsApplication() {
            this.ApplicationID = -1;
            this.ApplicationPersonID = -1;
            this.PersonInfo = new clsPerson();
            this.ApplicationTypeID = enApplicationTypes.LocalDL;
            this.ApplicationStatus = enApplicationStatus.New;
            this.ApplicationDate = DateTime.Now;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees =0;
            this.CreatedByUserID = -1;
            this.Mode = enMode.New;

        }

        protected clsApplication(int ApplicationID,int ApplicationPersonID,DateTime ApplicationDate,int ApplicationTypeID,int ApplicationStatus,
            DateTime LastStatusDate,decimal PaidFees,int CreatedByUserID)
        {
            this.ApplicationID=ApplicationID;
            this.ApplicationPersonID=ApplicationPersonID;
            this.PersonInfo=clsPerson.GetPerson(ApplicationPersonID);
            this.ApplicationDate=ApplicationDate;
            this.ApplicationTypeID=(enApplicationTypes)ApplicationTypeID;
            this.ApplicationStatus=(enApplicationStatus)ApplicationStatus;
            this.LastStatusDate=LastStatusDate;
            this.PaidFees=PaidFees;
            this.CreatedByUserID=CreatedByUserID;
            Mode = enMode.Update;

        }

        private bool _Add()
        {
            decimal fees = clsApplicationType.GetApplicationType((int)ApplicationTypeID).ApplicationFees;

            this.ApplicationID= Applications.AddApplication(ApplicationPersonID, DateTime.Now, (int)ApplicationTypeID,(int)enApplicationStatus.New
                , DateTime.Now, fees , this.CreatedByUserID);

            return this.ApplicationID != -1;
        }

        private bool _Update()
        {
            return Applications.UpdateApplicationStatus(ApplicationID, (int)ApplicationStatus);
        }

        public static bool Delete(int ApplicationID)
        {
            return Applications.DeleteApplication(ApplicationID);
        }

        public bool Complete()
        {
            ApplicationStatus = enApplicationStatus.Completed;
            return this.Save();
        }
        public bool Save()
        {

            bool isDone = false;
            switch (Mode)
            {

                case enMode.New:
                    isDone= _Add();
                    Mode = enMode.Update;
                    break;


                case enMode.Update:

                    isDone=_Update();


                    break;

            }




            return isDone;
        }


        public static clsApplication GetApplication(int ApplicationID)
        {
            int ApplicationPersonID = -1; DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = 0;
            int ApplicationStatus = -1;
            DateTime LastStatusDate=DateTime.Now;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;

            if(!Applications.GetApplication(ApplicationID,ref ApplicationPersonID,ref ApplicationDate,ref ApplicationTypeID,ref ApplicationStatus,ref LastStatusDate, ref PaidFees,ref CreatedByUserID))
            {
                return new clsApplication();
            }

            return new clsApplication(ApplicationID, ApplicationPersonID, ApplicationDate,ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
        }
       

    }
}
