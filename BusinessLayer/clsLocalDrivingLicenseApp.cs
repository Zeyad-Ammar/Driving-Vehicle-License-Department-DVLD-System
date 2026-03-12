using DataLayer;
using DVLD_Project.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsLocalDrivingLicenseApp : clsApplication
    {
        public enum enMode { New, Update };

        public enMode Mode;
        public int LDLicenseAppID { get; set; }


        public int PassedTests { get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClassInfo { get; set; }


        public clsLocalDrivingLicenseApp()
        {
            this.LicenseClassInfo = new clsLicenseClass();
            this.LicenseClassID = -1;
            this.LDLicenseAppID = -1;
            this.PassedTests = 0;
            this.Mode = enMode.New;


        }

        private clsLocalDrivingLicenseApp(int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus,
            DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID, int LDLicenseAppID, int LicenseClassID, int PassedTests) : base(ApplicationID, ApplicationPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
        {
            this.LDLicenseAppID = LDLicenseAppID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.GetLicenseClass(LicenseClassID);
            this.Mode = enMode.Update;
            this.PassedTests = PassedTests;

        }

        public bool Cancel()
        {
            this.ApplicationStatus = enApplicationStatus.Cancelled;
            return this.Save();
        }

        public bool Complete()
        {
            this.ApplicationStatus = enApplicationStatus.Completed;
            return this.Save();
        }
        public static DataTable GetAllLocalLicenseApps()
        {
            return LocalDrivingLicenseApplications.GetAllLocalDrivingLicenseApplications();
        }

        public static bool isPersonHaveNotCanceledLocalAppWithThisClass(int PersonID, int LicenseClassID)
        {
            return LocalDrivingLicenseApplications.isPersonHaveNotCanceledLicenseWithThisClass(PersonID, LicenseClassID);
        }

        public static clsLocalDrivingLicenseApp GetLocalLicenseApp(int LocalAppID)
        {
            int LicenseClassID = -1;
            int PassedTests = 0;
            int ApplicationID = -1;


            if (!LocalDrivingLicenseApplications.GetLocalApplication(LocalAppID, ref ApplicationID, ref LicenseClassID, ref PassedTests))
            {
                return new clsLocalDrivingLicenseApp();
            }
            else
            {
                clsApplication App = clsApplication.GetApplication(ApplicationID);
                return new clsLocalDrivingLicenseApp(ApplicationID, App.ApplicationPersonID, App.ApplicationDate, (int)App.ApplicationTypeID, (int)App.ApplicationStatus, App.LastStatusDate, App.PaidFees, App.CreatedByUserID, LocalAppID, LicenseClassID, PassedTests);
            }


        }





        private bool _AddLocalLicenseApp()
        {

            this.LDLicenseAppID = LocalDrivingLicenseApplications.AddLocalApplication(base.ApplicationID, LicenseClassID);

            return this.LDLicenseAppID != -1;
        }

        private bool _UpdateLocalLicense()
        {
            return LocalDrivingLicenseApplications.UpdateLocalApplication(this.LDLicenseAppID, LicenseClassID);
        }
        public bool Save()
        {

            base.Mode = (clsApplication.enMode)this.Mode;
            if (!base.Save())
            {
                return false;
            }

            bool isDone = false;

            switch (Mode)
            {

                case enMode.New:
                    isDone = _AddLocalLicenseApp();
                    Mode = enMode.Update;
                    base.Mode = clsApplication.enMode.Update;
                    break;
                case enMode.Update:
                    isDone = _UpdateLocalLicense();
                    break;
            }

            return isDone;
        }

        public static bool Delete(int LocalAppID)
        {
            clsLocalDrivingLicenseApp LDLApp = clsLocalDrivingLicenseApp.GetLocalLicenseApp(LocalAppID);

            if (!LocalDrivingLicenseApplications.DeleteLocalApplication(LocalAppID))
                return false;


            return clsApplication.Delete(LDLApp.ApplicationID);
        }

        private int AddAsDriverIfNotExist()
        {
            clsDriver driver = clsDriver.GetDriverByPersonID(this.ApplicationPersonID);

            if (driver.DriverID == -1)
            {
                driver = new clsDriver(this.ApplicationPersonID, clsCurrentUser.User.UserID);
                if (!driver.Save())
                {
                    return -1;
                }
               
            }
            return driver.DriverID;

        }
        public int IssueLicense(string Notes, int CreatedByUserID)
        {

            int DriverID = AddAsDriverIfNotExist();
            if (DriverID==-1)
            {
                return -1;
            }

            clsLicense license = new clsLicense(this.ApplicationID,DriverID, this.LicenseClassID, Notes, CreatedByUserID);
            if (!license.Save())
            {
                return -1;
            }

            if (license.LicenseID != -1)
            {
                this.Complete();
            }

            return license.LicenseID;
        }
    }
}
