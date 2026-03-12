using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsDriver
    {
        public int DriverID {  get; set; }
        public int PersonID {  get; set; }
        public int CreatedByUserID {  get; set; }
        public DateTime CreatedDate {  get; set; }

        public clsPerson PersonInfo {  get; set; }

        public clsDriver() {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            this.PersonInfo = new clsPerson();
        
        }

        public clsDriver(int PersonID,int CreatedByUserID)
        {
            this.DriverID = -1;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = DateTime.Now;
            this.PersonInfo = clsPerson.GetPerson(PersonID);

        }

        private clsDriver(int DriverID,int PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID=CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo= clsPerson.GetPerson(PersonID);
        }
        public static DataTable GetAllDrivers()
        {
            return Drivers.GetAllDrivers();
        }

        private bool _Add()
        {
            if (Drivers.isThisPersonDriver(this.PersonID))
            {
                return true;
            }
            this.DriverID = Drivers.AddDriver(this.PersonID, this.CreatedByUserID);
            return this.DriverID!=-1;
        }

        public bool Save()
        {

            return _Add();
        }

        public static clsDriver GetDriverByDriverID(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if(!Drivers.GetDriverByDriverID(DriverID,ref PersonID,ref CreatedByUserID,ref CreatedDate))
            {
                return new clsDriver();
            }

            return new clsDriver(DriverID,PersonID,CreatedByUserID,CreatedDate);
        }

        public static clsDriver GetDriverByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (!Drivers.GetDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver();
            }

            return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
        }
    }
}
