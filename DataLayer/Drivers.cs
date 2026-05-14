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
    public class Drivers
    {

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [DriverID]
      ,Drivers.[PersonID]
	  ,NationalNo
      ,FullName= FirstName+' '+SecondName+' '+ThirdName+' '+LastName
      ,[CreatedDate]
	  ,ActiveLicenses=(Select count(DriverID) from Licenses where Licenses.DriverID=Drivers.DriverID)
  FROM [dbo].[Drivers]
  inner join People
  on Drivers.PersonID=People.PersonID;";


                using (SqlCommand command = new SqlCommand(Query, connection))
                {



                    try
                    {
                        connection.Open();
                        using (var Reader = command.ExecuteReader())
                        {


                            if (Reader.HasRows)
                            {
                                dt.Load(Reader);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show("Error When Trying To Get The Drivers Data");
                    }
                   
                }
            }

            return dt;
        }

        public static bool isThisPersonDriver(int PersonID)
        {
            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"select top(1) found=1
  from Drivers
  where PersonID=@PersonID;";


                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("PersonID", PersonID);

                   
                    try
                    {
                        connection.Open();
                        var obj = command.ExecuteScalar();
                        if (obj != null)
                        {
                            isExist = true;
                        }


                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show("Error When Trying To Check Person exist in Drivers Data");
                    }
                   
                }

            }

            return isExist;
        }

        public static int AddDriver(int PersonID,int CreatedByUserID)
        {
            int ID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"INSERT INTO [dbo].[Drivers]
           ([PersonID]
           ,[CreatedByUserID]
           ,[CreatedDate])
     VALUES
           (@PersonID
           ,@CreatedByUserID
           ,@CreatedDate);
Select SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("PersonID", PersonID);
                    command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("CreatedDate", DateTime.Now);


                    
                    try
                    {

                        connection.Open();
                        var newID = command.ExecuteScalar();
                        if (newID != null)
                        {
                            ID = Convert.ToInt32(newID);
                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show("Error When Trying to Add New Driver");

                    }
                   
                }
            }

            return ID;
        }

        public static bool GetDriverByDriverID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isExist = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [DriverID]
      ,[PersonID]
      ,[CreatedByUserID]
      ,[CreatedDate]
  FROM [dbo].[Drivers]
    Where DriverID=@DriverID";


                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("DriverID", DriverID);


                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isExist = true;
                                PersonID = (int)reader["PersonID"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CreatedDate = (DateTime)reader["CreatedDate"];
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show("Error When Trying To Get Driver Data");
                    }
                    
                }

            }
            return isExist; 
        }

        public static bool GetDriverByPersonID(int PersonID, ref int DriverID , ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isExist = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [DriverID]
      ,[PersonID]
      ,[CreatedByUserID]
      ,[CreatedDate]
  FROM [dbo].[Drivers]
    Where PersonID=@PersonID";


                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("PersonID", PersonID);


                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                isExist = true;
                                DriverID = (int)reader["DriverID"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CreatedDate = (DateTime)reader["CreatedDate"];
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show("Error When Trying To Get Driver Data");
                    }
                  
                }

            }

            return isExist;
        }
    }
}
