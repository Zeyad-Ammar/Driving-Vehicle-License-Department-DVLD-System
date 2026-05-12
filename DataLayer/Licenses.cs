using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DataLayer
{
    public class Licenses
    {
        public static DataTable GetAllLicenseOfDriver(int DriverID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @" SELECT [LicenseID]
      ,[ApplicationID]  
      ,[ClassName]
      ,[IssueDate]
      ,[ExpirationDate]
      ,[IsActive]
  FROM [dbo].[Licenses]
  inner join
  LicenseClasses
  on LicenseClasses.LicenseClassID=Licenses.LicenseClass
  where DriverID=@DriverID
   order by IsActive desc , ExpirationDate, LicenseID desc ;";

            SqlCommand command = new SqlCommand(Query, connection);



            command.Parameters.AddWithValue("DriverID", DriverID);
            DataTable dt=new DataTable();
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

                MessageBox.Show($"Error When Trying to Get All License of Driver with ID: {DriverID}");

            }
            finally
            {

                connection.Close();

            }



           return dt;
           
        }
        public static int AddLicense(int AppID,int DriverID,int LicenseClassID ,int ValidLength  ,string Notes,decimal PaidFees,int IssueReason,int CreatedByUserID)
        {
            


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"INSERT INTO [dbo].[Licenses]
           ([ApplicationID]
           ,[DriverID]
           ,[LicenseClass]
           ,[IssueDate]
           ,[ExpirationDate]
           ,[Notes]
           ,[PaidFees]
           ,[IsActive]
           ,[IssueReason]
           ,[CreatedByUserID])
     VALUES
           (@ApplicationID
           ,@DriverID
           ,@LicenseClass
           ,@IssueDate
           ,@ExpirationDate
           ,@Notes
           ,@PaidFees
           ,@IsActive
           ,@IssueReason
           ,@CreatedByUserID);
Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("ApplicationID", AppID);
            command.Parameters.AddWithValue("DriverID", DriverID);
            command.Parameters.AddWithValue("LicenseClass", LicenseClassID);
            command.Parameters.AddWithValue("IssueDate", DateTime.Now);
            command.Parameters.AddWithValue("ExpirationDate", DateTime.Now.AddYears(ValidLength));
            if(string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("Notes", Notes);

            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("IsActive", 1);
            command.Parameters.AddWithValue("IssueReason", IssueReason);

            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            int ID = -1;
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

                MessageBox.Show("Error When Trying to Add New License");

            }
            finally
            {

                connection.Close();

            }

            return ID;

        }

        public static int AddLicense(int AppID, int DriverID, int LicenseClassID, DateTime ExpirationDate, string Notes, decimal PaidFees, int IssueReason, int CreatedByUserID)
        {



            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"INSERT INTO [dbo].[Licenses]
           ([ApplicationID]
           ,[DriverID]
           ,[LicenseClass]
           ,[IssueDate]
           ,[ExpirationDate]
           ,[Notes]
           ,[PaidFees]
           ,[IsActive]
           ,[IssueReason]
           ,[CreatedByUserID])
     VALUES
           (@ApplicationID
           ,@DriverID
           ,@LicenseClass
           ,@IssueDate
           ,@ExpirationDate
           ,@Notes
           ,@PaidFees
           ,@IsActive
           ,@IssueReason
           ,@CreatedByUserID);
Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("ApplicationID", AppID);
            command.Parameters.AddWithValue("DriverID", DriverID);
            command.Parameters.AddWithValue("LicenseClass", LicenseClassID);
            command.Parameters.AddWithValue("IssueDate", DateTime.Now);
            command.Parameters.AddWithValue("ExpirationDate", ExpirationDate);
            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("Notes", Notes);

            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("IsActive", 1);
            command.Parameters.AddWithValue("IssueReason", IssueReason);

            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            int ID = -1;
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

                MessageBox.Show("Error When Trying to Add New License");

            }
            finally
            {

                connection.Close();

            }

            return ID;

        }
        public static bool DeactivateLicense(int LicenseID )
        {



            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"UPDATE [dbo].[Licenses]
   SET 
      [IsActive] = 0
     
 WHERE LicenseID=@LicenseID;";

            SqlCommand command = new SqlCommand(Query, connection);

            

            command.Parameters.AddWithValue("LicenseID", LicenseID);
           bool isDone = false;
            try
            {

                connection.Open();
                var AffectedRecords = command.ExecuteNonQuery();
                if (AffectedRecords>0)
                {
                  isDone = true;
                }

            }
            catch (Exception ex)
            {
                clsUtilityDataLayer.LogError(ex);

                MessageBox.Show($"Error When Trying to Deactivate License with ID: {LicenseID}");

            }
            finally
            {

                connection.Close();

            }

            return isDone;

        }
        public static bool GetLicenseByLicenseID(int LicenseID,ref int AppID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {

            bool isExist=false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [LicenseID]
      ,[ApplicationID]
      ,[DriverID]
      ,[LicenseClass]
      ,[IssueDate]
      ,[ExpirationDate]
      ,[Notes]
      ,[PaidFees]
      ,[IsActive]
      ,[IssueReason]
      ,[CreatedByUserID]
  FROM [dbo].[Licenses]
Where LicenseID=@LicenseID
";

            SqlCommand command = new SqlCommand(Query, connection);

          

            command.Parameters.AddWithValue("LicenseID", LicenseID);
            
            try
            {

                connection.Open();
                var Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isExist = true;
                    AppID = (int)Reader["ApplicationID"];
                    DriverID = (int)Reader["DriverID"];
                    LicenseClassID = (int)Reader["LicenseClass"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    Notes = Reader["Notes"].ToString();
                    PaidFees = (decimal)Reader["PaidFees"];
                    IsActive = (bool)Reader["IsActive"];
                    IssueReason = Convert.ToInt32(Reader["IssueReason"]);
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                }

            }
            catch (Exception ex)
            {
                clsUtilityDataLayer.LogError(ex);

                MessageBox.Show("Error When Trying to Get License");

            }
            finally
            {

                connection.Close();

            }

           






            return isExist;
        }

        public static bool GetLicenseByAppID(int AppID, ref int LicenseID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {

            bool isExist = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [LicenseID]
      ,[ApplicationID]
      ,[DriverID]
      ,[LicenseClass]
      ,[IssueDate]
      ,[ExpirationDate]
      ,[Notes]
      ,[PaidFees]
      ,[IsActive]
      ,[IssueReason]
      ,[CreatedByUserID]
  FROM [dbo].[Licenses]
Where ApplicationID=@ApplicationID
";

            SqlCommand command = new SqlCommand(Query, connection);



            command.Parameters.AddWithValue("ApplicationID", AppID);

            try
            {

                connection.Open();
                var Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isExist = true;
                    LicenseID = (int)Reader["LicenseID"];
                    DriverID = (int)Reader["DriverID"];
                    LicenseClassID = (int)Reader["LicenseClass"];
                    IssueDate = (DateTime)Reader["IssueDate"];
                    ExpirationDate = (DateTime)Reader["ExpirationDate"];
                    Notes = Reader["Notes"].ToString();
                    PaidFees = (decimal)Reader["PaidFees"];
                    IsActive = (bool)Reader["IsActive"];
                    IssueReason = Convert.ToInt32(Reader["IssueReason"]);
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                }

            }
            catch (Exception ex)
            {
                clsUtilityDataLayer.LogError(ex);

                MessageBox.Show("Error When Trying to Get License");

            }
            finally
            {

                connection.Close();

            }








            return isExist;
        }
     
        public static bool isDriverHaveActiveLicenseWithClass(int DriverID, int LicenseClassID)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT
Found=1
  FROM [dbo].[Licenses]
Where [DriverID]=@DriverID and [LicenseClass]=@LicenseClassID and IsActive=1
";

            SqlCommand command = new SqlCommand(Query, connection);



            command.Parameters.AddWithValue("DriverID", DriverID);
            command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);


            try
            {

                connection.Open();
                var obj = command.ExecuteScalar();
                if (obj!=null)
                {
                    isExist = true;
                   
                }

            }
            catch (Exception ex)
            {
                clsUtilityDataLayer.LogError(ex);

                MessageBox.Show("Error When Trying to Check is there an active License of this class for this driver");

            }
            finally
            {

                connection.Close();

            }


            return isExist;
        }

    }
}
