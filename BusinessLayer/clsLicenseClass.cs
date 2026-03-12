using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsLicenseClass
    {
        public enum enMode { New,Update};

        public enMode Mode;
        public enum enLicenseClass { SmallMotorcycle=1,HeavyMotorcycleLicense,OrdinaryDrivingLicense,Commercial,Agricultural,SmallAndMediumBus,TruckAndHeavyVehicle}
        public enLicenseClass LicenseClassID {  get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }   
        public int MinimumAllowedAge {  get; set; }
        public int DefaultValidityLength {  get; set; }
        public decimal ClassFees {  get; set; }
        public clsLicenseClass()
        {
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;
            this.MinimumAllowedAge = 0;
            this.ClassName ="";
            this.ClassDescription = "";
            this.LicenseClassID =enLicenseClass.OrdinaryDrivingLicense;
            Mode = enMode.New;

        }

        private clsLicenseClass(int LicenseClassID,string ClassName,string ClassDescription,int MinimumAllowedAge,int DefaultValidityLength,decimal ClassFees)
        {
            this.LicenseClassID= (enLicenseClass)LicenseClassID;
            this.ClassName= ClassName;
            this.ClassDescription= ClassDescription;
            this.MinimumAllowedAge= MinimumAllowedAge;
            this.DefaultValidityLength= DefaultValidityLength;
            this.ClassFees= ClassFees;
            Mode=enMode.Update;

        }
        public static DataTable GetAllLicenseClasses()
        {
            return LicenseClasses.GetAllLicenseClasses();
        }

        public static clsLicenseClass GetLicenseClass(int LicenseClassID)
        {
            string ClassName = "", ClassDescription = "";
            int MinimumAllowedAge = 0, DefaultValidityLength = 0;
            decimal ClassFees = 0;

            if (!LicenseClasses.GetLicenseClass(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass();
            }
            return new clsLicenseClass(LicenseClassID,ClassName,ClassDescription,MinimumAllowedAge,DefaultValidityLength,ClassFees);

        }


    }
}
