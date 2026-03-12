using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class clsCountry
    {
        public int CountryID{get;set;}
        public string CountryName{get;set;}

       public clsCountry()
       {
            CountryID = -1;
            this.CountryName = CountryName;
       }

        
        private clsCountry(int CountryID,string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;

        }


        public static DataTable getAllCountries()
        {
            return DataLayer.Countries.GetAllCountries();
        }

        public static int getCountryID(string CountryName) {
            return DataLayer.Countries.getCountryID(CountryName);
        }

        public static string getCountryName(int CountryID)
        {
            return DataLayer.Countries.getCountryName(CountryID);
        }

        static public clsCountry getCountry(int countryID)
        {
            string countryName = "";

            if(!Countries.getCountry(countryID, ref countryName))
            {
                return new clsCountry();
            }

            return new clsCountry(countryID,countryName);
        }

    }
}
