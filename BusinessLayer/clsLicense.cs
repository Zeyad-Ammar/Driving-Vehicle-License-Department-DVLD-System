using DataLayer;
using DVLD_Project.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsLicense
    {

        public enum enMode { New, Update };

        public enum enIssueReason { FirstTime = 1, Renew, Replacement4Damaged, Replacement4Lost }
        public enMode Mode { get; set; }
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }

        public clsDriver DriverInfo { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public int CreatedByUserID { get; set; }


        public clsLicense()
        {

            Mode = enMode.New;
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.DriverInfo = new clsDriver();
            this.LicenseClassID = -1;
            this.LicenseClassInfo = new clsLicenseClass();
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = string.Empty;
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;
        }

        public clsLicense(int ApplicationID, int DriverID, int LicenseClassID, string Notes, int CreatedByUserID)
        {

            Mode = enMode.New;
            this.LicenseID = -1;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.GetDriverByDriverID(DriverID);
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.GetLicenseClass(LicenseClassID);
            this.IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.GetLicenseClass(LicenseClassID).DefaultValidityLength);
            this.Notes = Notes;
            this.PaidFees = clsLicenseClass.GetLicenseClass(LicenseClassID).ClassFees; ;
            this.IsActive = false;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = CreatedByUserID;
        }

        private clsLicense(int LicenseID, int AppID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = AppID;
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.GetDriverByDriverID(DriverID);
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.GetLicenseClass(LicenseClassID);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = (enIssueReason)IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        public string GetIssueReason()
        {
            switch (this.IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.Replacement4Damaged:
                    return "Replacement For Damaged";
                case enIssueReason.Replacement4Lost:
                    return "Replacement For Lost";
            }
            return "Wrong Issue Reason";
        }
        private bool _Add()
        {


            this.LicenseID = Licenses.AddLicense(this.ApplicationID, this.DriverID, this.LicenseClassID, clsLicenseClass.GetLicenseClass(LicenseClassID).DefaultValidityLength, this.Notes, this.PaidFees, (int)enIssueReason.FirstTime, this.CreatedByUserID);

            IsActive = this.LicenseID != -1;
            return this.LicenseID != -1;
        }

        public bool DeactivateLicense()
        {
            return Licenses.DeactivateLicense(this.LicenseID);
        }

        public bool Save()
        {
            bool isDone = false;

            switch (Mode)
            {
                case enMode.New:
                    isDone = _Add();
                    Mode = enMode.Update;
                    break;
            }

            return isDone;
        }

        public static clsLicense GetLicenseByLicenseID(int LicenseID)
        {
            int AppID = -1, DriverID = -1, LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            int IssueReason = -1, CreatedByUserID = -1;

            if (!Licenses.GetLicenseByLicenseID(LicenseID, ref AppID, ref DriverID, ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense();
            }

            return new clsLicense(LicenseID, AppID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
        }

        public bool isDetained()
        {
            return clsDetainedLicense.isLicenseDetained(this.LicenseID);
        }

       
        public int Detain(decimal FineFees,int CreatedByUserID)
        {
            clsDetainedLicense detainedLicense = new clsDetainedLicense(this.LicenseID, FineFees, CreatedByUserID);
            if(!detainedLicense.Save())
            {
                return -1;
            }
            return detainedLicense.DetainedID;
        }
        public static clsLicense GetLicenseByAppID(int AppID)
        {
            int LicenseID = -1, DriverID = -1, LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            int IssueReason = -1, CreatedByUserID = -1;

            if (!Licenses.GetLicenseByAppID(AppID, ref LicenseID, ref DriverID, ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                new clsLicense();
            }

            return new clsLicense(LicenseID, AppID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
        }

        public static bool isLicenseExist(int LicenseID)
        {

            return GetLicenseByLicenseID(LicenseID).LicenseID != -1;
        }
        public static DataTable GetAllLicensesOfDriver(int DriverID)
        {
            return Licenses.GetAllLicenseOfDriver(DriverID);
        }

        public static bool isDriverHaveActiveLicenseOfClass(int DriverID, int LicenseClassID)
        {
            return Licenses.isDriverHaveActiveLicenseWithClass(DriverID, LicenseClassID);
        }

        public int RenewLicense(string Notes, int CreatedByUserID)
        {
            int NewLicenseID = -1;

            clsApplication NewApplication = new clsApplication(this.DriverInfo.PersonID, (int)clsApplicationType.enAppType.RenewDL, CreatedByUserID);
            if (NewApplication.Save())
            {

                NewLicenseID = Licenses.AddLicense(NewApplication.ApplicationID, this.DriverID, this.LicenseClassID, this.LicenseClassInfo.DefaultValidityLength, Notes, this.LicenseClassInfo.ClassFees, (int)enIssueReason.Renew, CreatedByUserID);
                NewApplication.Complete();
                this.DeactivateLicense();
            }

            return NewLicenseID;
        }
        public bool isLicenseExpired()
        {
            return DateTime.Now > this.ExpirationDate;
        }

        public int ReplaceLicense(int ReplaceReason, int CreatedByUserID)
        {
            int NewLicenseID = -1;
            int AppType = -1;
            if (ReplaceReason == (int)enIssueReason.Replacement4Damaged)
            {
                AppType = (int)clsApplicationType.enAppType.ReplaceDamagedDL;
            }
            else if (ReplaceReason == (int)enIssueReason.Replacement4Lost)
            {
                AppType = (int)clsApplicationType.enAppType.ReplaceLostDL;
            }
            else
            {
                return -1;
            }

            clsApplication NewApplication = new clsApplication(DriverInfo.PersonID, AppType, CreatedByUserID);
            if (NewApplication.Save())
            {

                NewLicenseID = Licenses.AddLicense(NewApplication.ApplicationID, this.DriverID, this.LicenseClassID, this.ExpirationDate, this.Notes, 0, ReplaceReason, CreatedByUserID);


                if (NewLicenseID != -1)
                {
                    NewApplication.Complete();
                    this.DeactivateLicense();
                }
               
            }
            return NewLicenseID;
        }

        
        
    }
}
