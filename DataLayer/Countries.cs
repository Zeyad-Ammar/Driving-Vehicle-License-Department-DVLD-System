using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataLayer
{
    public class Countries
    {

        public static DataTable GetAllCountries()
        {
            DataTable Countries = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"select * from Countries";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                Countries.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        Countries.Rows.Add(ex.Message);
                    }
                   
                }
            }

            return Countries;
        }

        static public int getCountryID(string CountryName)
        {
            int ID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [CountryID]
                FROM [dbo].[Countries] Where CountryName=@CountryName";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("CountryName", CountryName);

                    
                    try
                    {
                        connection.Open();
                        object CountryID = command.ExecuteScalar();
                        if (CountryID != null)
                        {
                            ID = (int)CountryID;
                        }
                    }
                    catch (Exception ex)
                    {


                        clsUtilityDataLayer.LogError(ex);

                    }
                    
                }
            }
            return ID;


        }

        static public string getCountryName(int CountryID)
        {
            string CountryName = "";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [CountryName]
                FROM [dbo].[Countries] Where CountryID=@CountryID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("CountryID", CountryID);

                    
                    try
                    {
                        connection.Open();
                        object Country = command.ExecuteScalar();
                        if (Country != null)
                        {
                            CountryName = Country.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                    }
                    
                }
            }
            return CountryName;


        }

        static public bool getCountry(int CountryID, ref string CountryName) {

            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [CountryID]
      ,[CountryName]
  FROM [dbo].[Countries]
where CountryID=@CountryID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("CountryID", CountryID);

                    
                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isExist = true;
                                CountryName = reader["CountryName"].ToString();
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        connection.Close();

                    }
                }

            }

            return isExist; 
        }
        

    }

}
