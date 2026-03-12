using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsDetainedLicense
    {
        public enum enMode { New,Update};
        public enMode Mode { get; set; }
        public int DetainedID {  get; set; }
        public int LicenseID { get; set; } 
        public clsLicense LicenseInfo { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees {  get; set; }
        public int CreatedByUserID {  get; set; }

        public bool isReleased {  get; set; }
        public DateTime ReleaseDate {  get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID {  get; set; }

        public clsDetainedLicense() {
            this.DetainedID = -1;
            this.LicenseID = -1;
            this.LicenseInfo = new clsLicense();
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.isReleased = false;
            this.ReleaseDate = DateTime.Now;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
            this.Mode=enMode.New;

        }

        public clsDetainedLicense(int LicenseID, decimal FineFees, int CreatedByUserID)
        {
            this.DetainedID = -1;
            this.LicenseID = LicenseID;
            this.LicenseInfo = clsLicense.GetLicenseByLicenseID(LicenseID);
            this.DetainDate = DateTime.Now;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.isReleased = false;
            this.ReleaseDate = DateTime.Now;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
            this.Mode = enMode.New;

        }

        private clsDetainedLicense(int LicenseID,int DetainedID,DateTime DetainDate,decimal FineFees,int CreatedByUserID,bool IsReleased,DateTime ReleasedDate,int ReleasedByUseID,int ReleasedApplicationID)
        {
            this.DetainedID = DetainedID;
            this.LicenseID = LicenseID;
            this.LicenseInfo = clsLicense.GetLicenseByLicenseID(LicenseID);
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.isReleased = isReleased;
            this.ReleaseDate = ReleasedDate;
            this.ReleasedByUserID = ReleasedByUseID;
            this.ReleaseApplicationID = ReleasedApplicationID;
            this.Mode = enMode.Update;
        }

        public static bool isLicenseDetained(int LicenseID)
        {
            return DetainedLicenses.isLicenseDetained(LicenseID);
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return DetainedLicenses.GetAllDetainedLicenses();
        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainedID = -1, CreatedByUserID = -1, ReleasedByUserID=-1, ReleaseApplicationID=-1;
            DateTime DetainDate = DateTime.Now, ReleaseDate=DateTime.Now;
            decimal FineFees = 0;
            bool isReleased = false;
             
            if(! DetainedLicenses.GetDetainedLicenseByLicenseID(LicenseID, ref  DetainedID, ref  DetainDate, ref  FineFees
                , ref  CreatedByUserID, ref  isReleased, ref ReleaseDate, ref  ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense();
            }

            return new clsDetainedLicense(LicenseID,  DetainedID,  DetainDate,  FineFees
                ,  CreatedByUserID,  isReleased,  ReleaseDate,  ReleasedByUserID,  ReleaseApplicationID);
        }

        public static clsDetainedLicense FindByDetainID(int DetainedID)
        {
            int LicenseID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.Now;
            decimal FineFees = 0;
            bool isReleased = false;

            if (!DetainedLicenses.GetDetainedLicenseByDetainID(DetainedID, ref LicenseID, ref DetainDate, ref FineFees
                , ref CreatedByUserID, ref isReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense();
            }

            return new clsDetainedLicense(LicenseID, DetainedID, DetainDate, FineFees
                , CreatedByUserID, isReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
        }
        private bool _Add()
        {
            this.DetainedID = DetainedLicenses.AddDetainLicense(this.LicenseID, this.FineFees, this.CreatedByUserID);
            return this.DetainedID != -1;
        }

        

        public  bool Release( int ReleasedUserID)
        {
            clsApplication App = new clsApplication(this.LicenseInfo.DriverInfo.PersonID, (int)clsApplicationType.enAppType.ReleaseDetainedDL, ReleasedUserID);

            if (App.Save())
            {
                if (DetainedLicenses.ReleaseDetainedLicense(this.DetainedID, ReleasedUserID, App.ApplicationID))
                {
                    this.ReleaseApplicationID=App.ApplicationID;
                    this.isReleased = true;
                    this.ReleasedByUserID = ReleasedUserID;
                    this.ReleaseDate= DateTime.Now;
                    App.Complete();
                    return true;
                }
            }
            
            return false;
        }
        public bool Save()
        {

            bool isDone = false;
            switch (Mode)
            {
                case enMode.New:
                    isDone = _Add();
                    this.Mode = enMode.Update;
                    break;

                case enMode.Update:

                    break;
            }

            return isDone;
        }

        
        
    }
}
