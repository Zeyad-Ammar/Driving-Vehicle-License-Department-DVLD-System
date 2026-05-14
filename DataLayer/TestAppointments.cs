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
    public class TestAppointments
    {

        public static DataTable GetAllTestAppointmentsForLocalAppFromType(int LocalAppID,int TestTypeID)
        {

            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [TestAppointmentID]
      ,[AppointmentDate]
      ,[PaidFees] 
      ,[IsLocked]
  FROM [dbo].[TestAppointments]
Where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalAppID);
                    command.Parameters.AddWithValue("TestTypeID", TestTypeID);

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

                        MessageBox.Show("Error When Trying to get All Test Appointments for local App"); ;
                    }
                    
                }
            }
            return dt;
        }

        public static int GetNumberOfTestAppointmentsForLocalAppFromType(int LocalAppID, int TestTypeID) {


            int numberOfAppointments = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"select count(TestAppointments.LocalDrivingLicenseApplicationID) from TestAppointments
  where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID and [IsLocked]=1;";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalAppID);
                    command.Parameters.AddWithValue("TestTypeID", TestTypeID);

                    
                    try
                    {
                        connection.Open();
                        var numOfAppointments = command.ExecuteScalar();
                        if (numOfAppointments != null)
                        {
                            numberOfAppointments = Convert.ToInt32(numOfAppointments);
                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show("Error When Trying to get number of Test Appointments for local App"); ;
                    }
                  
                }

            }

            return numberOfAppointments;

        }

        public static int AddTestAppointment(int TestTypeID,int LocalDrivingLicenseApplicationID,DateTime AppointmentDate,decimal PaidFees,int CreatedByUserID,int RetakeTestAppID)
        {
            int newID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"INSERT INTO [dbo].[TestAppointments]
           ([TestTypeID]
           ,[LocalDrivingLicenseApplicationID]
           ,[AppointmentDate]
           ,[PaidFees]
           ,[CreatedByUserID]
           ,[IsLocked]
           ,[RetakeTestApplicationID])
     VALUES
           (@TestTypeID
           ,@LocalDrivingLicenseApplicationID
           ,@AppointmentDate
           ,@PaidFees
           ,@CreatedByUserID
           ,@IsLocked
           ,@RetakeTestApplicationID);
Select Scope_Identity()";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("AppointmentDate", AppointmentDate);
                    command.Parameters.AddWithValue("PaidFees", PaidFees);
                    command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("IsLocked", false);
                    if (RetakeTestAppID > 0)
                        command.Parameters.AddWithValue("RetakeTestApplicationID", RetakeTestAppID);
                    else
                        command.Parameters.AddWithValue("RetakeTestApplicationID", DBNull.Value);

                    try
                    {
                        connection.Open();
                        var ID = command.ExecuteScalar();
                        if (ID != null)
                        {
                            newID = Convert.ToInt32(ID);
                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show("Error When Trying to Add New Test Appointments for local App"); ;
                    }
                   
                }
            }

            return newID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID,int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID,bool isLocked,int RetakeTestAppID)
        {
            bool isDone =false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"UPDATE [dbo].[TestAppointments]
   SET
       [TestTypeID] = @TestTypeID
      ,[LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID
      ,[AppointmentDate] = @AppointmentDate
      ,[PaidFees] = @PaidFees
      ,[CreatedByUserID] = @CreatedByUserID
      ,[IsLocked] = @IsLocked
      ,[RetakeTestApplicationID] = @RetakeTestApplicationID
 WHERE TestAppointmentID=@TestAppointmentID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);
                    command.Parameters.AddWithValue("TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("AppointmentDate", AppointmentDate);
                    command.Parameters.AddWithValue("PaidFees", PaidFees);
                    command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("IsLocked", isLocked);
                    if (RetakeTestAppID < 1)
                    {
                        command.Parameters.AddWithValue("RetakeTestApplicationID", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("RetakeTestApplicationID", RetakeTestAppID);
                    }

                    try
                    {
                        connection.Open();
                        var AffectedRows = command.ExecuteNonQuery();
                        if (AffectedRows > 0)
                        {
                            isDone = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);
                        MessageBox.Show("Error When Trying to Add New Test Appointments for local App"); ;
                    }
                }
                

            }

            return isDone;
        }

        public static bool isOpenTestAppointmentExist(int LDLAppID,int TestTypeID)
        {
            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"
select found=1 from TestAppointments
where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID and IsLocked=0;;";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LDLAppID);
                    command.Parameters.AddWithValue("TestTypeID", TestTypeID);

                   
                    try
                    {
                        connection.Open();
                        var numOfAppointments = command.ExecuteScalar();
                        if (numOfAppointments != null)
                        {
                            isExist = true;

                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);
                        MessageBox.Show("Error When Trying to check Existing of open Test Appointments for local App"); ;
                    }
                }

            }

            return isExist;
        }
        public static bool GetTestAppointment(int TestAppointmentID,ref int TestTypeID,ref int LDLAppID,ref DateTime AppointmentDate,ref decimal PaidFees
            ,ref int CreatedByUserID,ref bool isLocked,ref int RetakeTestAppID)
        {
            bool isExist = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [TestAppointmentID]
      ,[TestTypeID]
      ,[LocalDrivingLicenseApplicationID]
      ,[AppointmentDate]
      ,[PaidFees]
      ,[CreatedByUserID]
      ,[IsLocked]
      ,[RetakeTestApplicationID]
  FROM [dbo].[TestAppointments]
  where TestAppointmentID=@TestAppointmentID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);

                    
                    try
                    {
                        connection.Open();
                        using (var Reader = command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                isExist = true;
                                TestTypeID = (int)Reader["TestTypeID"];
                                LDLAppID = (int)Reader["LocalDrivingLicenseApplicationID"];
                                AppointmentDate = (DateTime)Reader["AppointmentDate"];
                                PaidFees = (decimal)Reader["PaidFees"];
                                CreatedByUserID = (int)Reader["CreatedByUserID"];
                                isLocked = (bool)Reader["IsLocked"];
                                if (Reader["RetakeTestApplicationID"] != DBNull.Value)
                                    RetakeTestAppID = (int)Reader["RetakeTestApplicationID"];

                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);
                        MessageBox.Show("Error When Trying to get  Test Appointment for local App"); ;
                    }
                    
                }
            }

            return isExist;
        }
    }
}
