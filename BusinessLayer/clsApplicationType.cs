using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class clsApplicationType
    {
        public enum enMode { New=0,Update};

        public enMode Mode=enMode.New;

       public enum enAppType { NewLDL=1,RenewDL,ReplaceDamagedDL, ReplaceLostDL, ReleaseDetainedDL,NewIL,RetakeTest }
        public enAppType ApplicationTypeID {  get; set; }
        public string ApplicationName { get; set; }

        public decimal ApplicationFees {  get; set; }

        public clsApplicationType() {
            this.Mode=enMode.New;
            this.ApplicationTypeID = enAppType.NewLDL;
            this.ApplicationName = "";
            this.ApplicationFees = 0;
        }

        private clsApplicationType(int applicationTypeID, string applicationName, decimal applicationFees) {
            this.Mode = enMode.Update;
            this.ApplicationTypeID=(enAppType)applicationTypeID;
            this.ApplicationName=applicationName;
            this.ApplicationFees=applicationFees;

        }

        public static DataTable GetAllApplicationTypes()
        {
            return ApplicationTypes.GetAllApplicationTypes();
        }

        public static clsApplicationType GetApplicationType(int applicationTypeID) {
            string ApplicationTypeName = "";
            decimal ApplicationTypeFees = 0;
            if(!ApplicationTypes.GetApplicationType(applicationTypeID,ref ApplicationTypeName, ref ApplicationTypeFees))
            {
                return new clsApplicationType();
            }

            return new clsApplicationType(applicationTypeID, ApplicationTypeName, ApplicationTypeFees);
           

        }

        public bool Save()
        {
            if (this.Mode == enMode.New) {
                MessageBox.Show("The Application Types Are Fixed. Can't Add New Type");
                return false;
            }
           return ApplicationTypes.UpdateApplicationType((int)this.ApplicationTypeID,this.ApplicationFees);
        }

    }
}
