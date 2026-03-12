using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsTest
    {
        public enum enMode { New,Update};

        public enMode Mode { get; set; }
        public int TestID {  get; set; }
        public int TestAppointmentID {  get; set; }
        public bool TestResult {  get; set; }

        public string Notes {  get; set; }
        public int CreatedByUserID {  get; set; }

        public clsTest()
        {
            this.Notes = string.Empty;
            this.CreatedByUserID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.TestID = -1;
            this.Mode = enMode.New;
        }

        private clsTest(int TestID,int TestAppointmentID,bool TestResult,string Notes,int CreatedByUserID)
        {
            this.TestID=TestID;
            this.TestAppointmentID=TestAppointmentID;
            this.TestResult=TestResult;
            this.Notes=Notes;
            this.CreatedByUserID=CreatedByUserID;
            this.Mode = enMode.Update;

        }

        private  bool _AddNewTest()
        {
             this.TestID=Tests.AddTest(this.TestAppointmentID,this.TestResult,this.Notes,this.CreatedByUserID);

            return this.TestID != -1;
        }

        public static bool isAppPassedTest(int LocalAppID,int testTypeID)
        {
            return Tests.isAppPassedTest(LocalAppID,testTypeID);
        }
        public bool Save()
        {
            return _AddNewTest();
        }

        public static clsTest FindByTestAppointmentID(int TestAppointemntID)
        {
            int TestID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;
            if (!Tests.GetTestByTestAppointmentID(TestAppointemntID,ref TestID,ref TestResult,ref Notes,ref CreatedByUserID))
            {
                return new clsTest();
            }

            return new clsTest(TestID,TestAppointemntID,TestResult,Notes,CreatedByUserID);

        }
    }
}
