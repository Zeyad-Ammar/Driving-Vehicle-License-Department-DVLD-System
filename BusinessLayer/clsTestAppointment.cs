using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode { New,Update};

        public enMode Mode { get; set; }
        public int TestAppointmentID {  get; set; }
        public int TestTypeID {  get; set; }

        public int LDLAppID {  get; set; }
        public DateTime AppointmentDate {  get; set; }
        public decimal PaidFees {  get; set; }
        public int CreatedByUserID {  get; set; }
        public bool isLocked {  get; set; }
        public int RetakeTestAppID {  get; set; }

        public clsTestType TestTypeInfo {  get; set; }


        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LDLAppID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.isLocked = false;
            this.RetakeTestAppID = -1;
            Mode= enMode.New;
        }

        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LDLAppID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool isLocked, int RetakeTestAppID)
        {
            this.TestAppointmentID= TestAppointmentID;
            this.TestTypeID= TestTypeID;
            this.LDLAppID= LDLAppID;
           this.AppointmentDate= AppointmentDate;
            this.PaidFees= PaidFees;
            this.CreatedByUserID= CreatedByUserID;
            this.isLocked = isLocked;
            this.RetakeTestAppID= RetakeTestAppID;
            this.TestTypeInfo=clsTestType.GetTestType(TestTypeID);

            this.Mode= enMode.Update;

        }


        public static DataTable GetAllTestAppointmentsOfTypeForLDLApp(int LDLAppID,int TestTypeID)
        {
            return TestAppointments.GetAllTestAppointmentsForLocalAppFromType(LDLAppID, TestTypeID);
        }

        public static int GetNumberOfTestAppointmentsOfTypeForLDLApp(int LDLAppID,int TestTypeID)
        {
            return TestAppointments.GetNumberOfTestAppointmentsForLocalAppFromType(LDLAppID,TestTypeID);
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID= TestAppointments.AddTestAppointment(this.TestTypeID, this.LDLAppID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID,this.RetakeTestAppID);

            return this.TestAppointmentID != -1;
        }

        private bool _UpdateTestAppointment()
        {
            return TestAppointments.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.LDLAppID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.isLocked, this.RetakeTestAppID);

            
        }

        public bool _LockTestAppointment()
        {
            this.isLocked = true;
            return _UpdateTestAppointment();
        }
        public bool Save()
        {
            bool isDone = false;
            switch (this.Mode)
            {
                case enMode.New:

                    isDone = _AddNewTestAppointment();
                    Mode = enMode.Update;

                    break;

                case enMode.Update:

                    isDone= _UpdateTestAppointment();

                    break;
            }

            return isDone;
        }

        public static bool isTestAppointmentOpenExist(int LDLAppID, int TestTypeID) { 
            return TestAppointments.isOpenTestAppointmentExist(LDLAppID, TestTypeID);
        }

        public static clsTestAppointment GetTestAppointment(int TestAppointmentID)
        {
            int TestTypeID=-1, LDLAppID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0; int CreatedByUserID = -1; bool isLocked = false; int RetakeTestAppID = -1;
            if(!TestAppointments.GetTestAppointment(TestAppointmentID,ref TestTypeID,ref LDLAppID,ref AppointmentDate,ref PaidFees, ref CreatedByUserID,ref isLocked,ref RetakeTestAppID))
            {
                return new clsTestAppointment();
            }

            return new clsTestAppointment( TestAppointmentID,  TestTypeID,  LDLAppID,  AppointmentDate,  PaidFees,  CreatedByUserID,  isLocked,  RetakeTestAppID);
        }
    }
}
