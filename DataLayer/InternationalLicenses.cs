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
    public class InternationalLicenses
    {
        public static DataTable GetAllInternationalLicenseOfDriver(int DriverID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [InternationalLicenseID]
                  ,[ApplicationID]
                  ,[DriverID]
                  ,[IssueDate]
                  ,[ExpirationDate]
                  ,[IsActive]
              FROM [dbo].[InternationalLicenses]
              where DriverID=@DriverID
order by IsActive desc , ExpirationDate
;";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {



                    command.Parameters.AddWithValue("DriverID", DriverID);
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

                        MessageBox.Show($"Error When Trying to Get All international License of Driver with ID: {DriverID}");

                    }
                   

                }

            } 



            return dt;

        }

        public static DataTable GetAllInternationalLicensesApplications()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [InternationalLicenseID]
                  ,[ApplicationID]
                  ,[DriverID]
                  ,[IssuedUsingLocalLicenseID]
                  ,[IssueDate]
                  ,[ExpirationDate]
                  ,[IsActive]
              FROM [dbo].[InternationalLicenses]
            ";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    try
                    {

                        connection.Open();
                        var Reader = command.ExecuteReader();
                        if (Reader.HasRows)
                        {
                            dt.Load(Reader);
                        }
                        Reader.Close();

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show($"Error When Trying to Get All international License");

                    }
                    
                }
            }


            return dt;
        }

        public static int AddInternationalLicense(int ApplicationID,int DriverID,int IssuedUsingLocalLicenseID,int CreatedByUserID)
        {

            int NewID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"UPDATE [dbo].[InternationalLicenses]
                 SET 
                    [IsActive] = 0 
               WHERE DriverID=@DriverID

INSERT INTO [dbo].[InternationalLicenses]
           ([ApplicationID]
           ,[DriverID]
           ,[IssuedUsingLocalLicenseID]
           ,[IssueDate]
           ,[ExpirationDate]
           ,[IsActive]
           ,[CreatedByUserID])
     VALUES
           (@ApplicationID
           ,@DriverID
           ,@IssuedUsingLocalLicenseID
           ,@IssueDate
           ,@ExpirationDate
           ,@IsActive
           ,@CreatedByUserID)
Select SCOPE_IDENTITY();
";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("DriverID", DriverID);
                    command.Parameters.AddWithValue("IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                    command.Parameters.AddWithValue("IssueDate", DateTime.Now);
                    command.Parameters.AddWithValue("ExpirationDate", DateTime.Now.AddYears(1));
                    command.Parameters.AddWithValue("IsActive", true);
                    command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);

                    try
                    {
                        connection.Open();
                        var obj = command.ExecuteScalar();
                        if (obj != null)
                        {
                            NewID = Convert.ToInt32(obj);

                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show($"Error When Trying to Add International License for Driver with ID: {DriverID}");
                    }
                    
                }
            }

            return NewID;
        }

        public static bool GetInternationalLicenseByID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isExist = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string Query = @"SELECT [InternationalLicenseID]
                  ,[ApplicationID]
                  ,[DriverID]
                  ,[IssuedUsingLocalLicenseID]
                  ,[IssueDate]
                  ,[ExpirationDate]
                  ,[IsActive]
                  ,[CreatedByUserID]
              FROM [dbo].[InternationalLicenses]
              where InternationalLicenseID=@InternationalLicenseID;";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("InternationalLicenseID", InternationalLicenseID);

                    try
                    {
                        connection.Open();
                        using (var Reader = command.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                Reader.Read();
                                ApplicationID = Convert.ToInt32(Reader["ApplicationID"]);
                                DriverID = Convert.ToInt32(Reader["DriverID"]);
                                IssuedUsingLocalLicenseID = Convert.ToInt32(Reader["IssuedUsingLocalLicenseID"]);
                                IssueDate = Convert.ToDateTime(Reader["IssueDate"]);
                                ExpirationDate = Convert.ToDateTime(Reader["ExpirationDate"]);
                                IsActive = Convert.ToBoolean(Reader["IsActive"]);
                                CreatedByUserID = Convert.ToInt32(Reader["CreatedByUserID"]);
                                isExist = true;
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show($"Error When Trying to Get International License with ID: {InternationalLicenseID}");
                    }
                   
                }
            }

            return isExist;
        }
        public static int GetActiveInternationalLicenseIDForDriver(int DriverID)
        {
            int ID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string Query = @"SELECT Top(1) [InternationalLicenseID]
      
  FROM [dbo].[InternationalLicenses]
              where DriverID=@DriverID and IsActive=1 and GETDATE() between IssueDate and ExpirationDate
             ";
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("DriverID", DriverID);

                    try
                    {
                        connection.Open();
                        var id = command.ExecuteScalar();
                        if (id != null)
                        {

                            ID = Convert.ToInt32(id);
                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show($"Error When Trying to Get Last International License ID of Driver with ID: {DriverID}");
                    }
                    
                }
            }
            return ID;
        }

        }
}
