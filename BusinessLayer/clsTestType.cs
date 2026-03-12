using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsTestType
    {
        public enum enTestType { Vision=1,Writting,Street}
        public enTestType TestTypeID  { get; set; }
        public string TestTypeTitle { get; set; }

        public string TestTypeDescription { get; set; }

        public decimal TestTypeFees { get; set; }

        public clsTestType()
        {
            this.TestTypeTitle = string.Empty;
            this.TestTypeDescription = string.Empty;
            this.TestTypeFees = 0;
            this.TestTypeID = enTestType.Vision;
        }

        private clsTestType(int TestTypeID,string TestTypeTitle,string TestTypeDescription,decimal TestTypeFees)
        {
            this.TestTypeID= (enTestType)TestTypeID;
            this.TestTypeTitle= TestTypeTitle;
            this.TestTypeDescription= TestTypeDescription;
            this.TestTypeFees= TestTypeFees;

        }

        public static DataTable GetAllTestTypes()
        {
            return TestTypes.GetAllTestTypes();
        }

        public static clsTestType GetTestType(int testTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            decimal TestTypeFees = 0;

            if(!TestTypes.GetTestType(testTypeID,ref TestTypeTitle,ref TestTypeDescription,ref TestTypeFees))
            {
                return new clsTestType();
            }

            return new clsTestType(testTypeID,TestTypeTitle,TestTypeDescription,TestTypeFees);
        }

        public bool Save()
        {
            return TestTypes.UpdateTestTypeDescriptionAndFees( (int)TestTypeID, TestTypeDescription, TestTypeFees);
        }

    }
}
