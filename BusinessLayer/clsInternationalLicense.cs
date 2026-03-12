using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsInternationalLicense:clsApplication
    {
        public enum enMode { New,Update};

        public enMode Mode { get; set; }
        public int InternationalLicenseID{get; set;}
      
        public int DriverID {  get; set;}
        public clsDriver DriverInfo { get; set; }
        public int IssuedUsingLocalLicenseID {  get; set;}

        public DateTime IssuedDate {  get; set;}
        public DateTime ExpirationDate {  get; set;}
        public bool IsActive {  get; set;}

       

        public clsLicense LocalLicenseInfo { get; set;}

        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1; 
            this.DriverID = -1;
            this.DriverInfo=new clsDriver();
            this.IssuedUsingLocalLicenseID = -1;
            this.LocalLicenseInfo = new clsLicense();
            this.IssuedDate=DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.Mode=enMode.New;

        }

        public clsInternationalLicense(int DriverID,int IssuedUsingLocalLicenseID,int CreatedByUserID):base(clsDriver.GetDriverByDriverID(DriverID).PersonID,(int)clsApplicationType.enAppType.NewIL,CreatedByUserID)
        {
            this.InternationalLicenseID = -1;
            this.DriverID = DriverID;
            this.DriverInfo =  clsDriver.GetDriverByDriverID(DriverID);
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.LocalLicenseInfo= clsLicense.GetLicenseByLicenseID(IssuedUsingLocalLicenseID);
            this.IssuedDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now.AddYears(1);
            this.IsActive = true;
            this.Mode = enMode.New;


        }

        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus,
            DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID, int DriverID, int IssuedUsingLocalLicenseID,DateTime IssuedDate,DateTime ExpierationDate,bool IsActive)
            :base( ApplicationID,  ApplicationPersonID,  ApplicationDate,  ApplicationTypeID,  ApplicationStatus,
             LastStatusDate,  PaidFees,  CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.DriverInfo= clsDriver.GetDriverByDriverID(DriverID);
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.LocalLicenseInfo = clsLicense.GetLicenseByLicenseID(IssuedUsingLocalLicenseID);
            this.IssuedDate = IssuedDate;
            this.ExpirationDate = ExpierationDate;
            this.IsActive = IsActive;
            this.Mode = enMode.Update;
        }

        public static int GetLastInternationalLicenseIDOfDriver(int DriverID)
        {
            return InternationalLicenses.GetActiveInternationalLicenseIDForDriver(DriverID);
        }

        public static bool isDriverHaveActiveInternationalLicense(int DriverID)
        {
            int LastInternationalLicenseID = GetLastInternationalLicenseIDOfDriver(DriverID);
            return Find(LastInternationalLicenseID).IsActive;
        }
        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID=-1, DriverID=-1, IssuedUsingLocalLicenseID=-1, CreatedByUserID=-1;
            DateTime IssuedDate=DateTime.Now, ExpirationDate=DateTime.Now;
            bool IsActive=false;
            if(!InternationalLicenses.GetInternationalLicenseByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssuedDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense();
            }

            clsApplication App=clsApplication.GetApplication(ApplicationID);

            return new clsInternationalLicense(InternationalLicenseID, ApplicationID,App.ApplicationPersonID,App.ApplicationDate,(int)App.ApplicationTypeID,(int)App.ApplicationStatus,App.LastStatusDate,App.PaidFees,App.CreatedByUserID, DriverID, IssuedUsingLocalLicenseID, IssuedDate, ExpirationDate, IsActive);
        }
        
        private bool Add()
        {
            
            this.InternationalLicenseID=InternationalLicenses.AddInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.CreatedByUserID);
            return this.InternationalLicenseID != -1;
        }
        public static DataTable GetAllInternationalLicenseOfDriver(int DriverID)
        {
            return InternationalLicenses.GetAllInternationalLicenseOfDriver(DriverID);
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)this.Mode;
            if (!base.Save()) {
                return false;
            }
            bool isDone = false;
            switch (this.Mode)
            {
                case enMode.New:
                    isDone = this.Add();
                    base.Complete();
                    Mode = enMode.Update;
                    base.Mode = clsApplication.enMode.Update;

                    break;
                case enMode.Update:

                    break;
            }
             return isDone;
        }

        public static DataTable GetAllInternationalLicense()
        {
            return InternationalLicenses.GetAllInternationalLicensesApplications();
        }
    }
}
