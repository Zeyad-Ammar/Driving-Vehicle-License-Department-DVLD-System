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
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"select * from Countries";

            SqlCommand command=new SqlCommand(Query, connection);

            DataTable Countries = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) {
                    Countries.Load(reader);
                }
            }
            catch (Exception ex) {
                clsUtilityDataLayer.LogError(ex);

                Countries.Rows.Add(ex.Message);
            }
            finally {
                connection.Close();
            }

            return Countries;
        }

        static public int getCountryID(string CountryName)
        {
            SqlConnection connection=new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [CountryID]
                FROM [dbo].[Countries] Where CountryName=@CountryName";

            SqlCommand command= new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("CountryName", CountryName);

            int ID = -1;
            try
            {
                connection.Open();
                object CountryID=command.ExecuteScalar();
                if (CountryID != null) { 
                    ID=(int)CountryID;
                }
            }
            catch (Exception ex) {


                clsUtilityDataLayer.LogError(ex);

            }
            finally { 

                connection.Close ();
                
            }

            return ID;


        }

        static public string getCountryName(int CountryID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [CountryName]
                FROM [dbo].[Countries] Where CountryID=@CountryID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("CountryID", CountryID);

            string CountryName = "";
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
            finally
            {

                connection.Close();

            }

            return CountryName;


        }

        static public bool getCountry(int CountryID, ref string CountryName) {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [CountryID]
      ,[CountryName]
  FROM [dbo].[Countries]
where CountryID=@CountryID";

            SqlCommand command= new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("CountryID", CountryID);

            bool isExist=false;
            try
            {
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isExist = true;
                    CountryName = reader["CountryName"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex) {
                clsUtilityDataLayer.LogError(ex);

                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                
            }



           

            return isExist; 
        }
        

    }

}
