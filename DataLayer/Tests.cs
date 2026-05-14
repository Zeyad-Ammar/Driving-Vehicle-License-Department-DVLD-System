using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataLayer
{
    public class Tests
    {
     
        public static int AddTest(int TestAppointmentID,bool testResult, string Notes,int CreatedByUserID)
        {
            int newID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"INSERT INTO [dbo].[Tests]
           ([TestAppointmentID]
           ,[TestResult]
           ,[Notes]
           ,[CreatedByUserID])
     VALUES
           (@TestAppointmentID
           ,@TestResult
           ,@Notes
           ,@CreatedByUserID);
Select Scope_Identity()";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("TestAppointmentID", TestAppointmentID);
                    command.Parameters.AddWithValue("TestResult", testResult);
                    if (string.IsNullOrEmpty(Notes))
                        command.Parameters.AddWithValue("Notes", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("Notes", Notes);

                    command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);

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
                        MessageBox.Show("Error When Trying to Add Test"); ;
                    }
                }

            }

            return newID;
        }

        public static bool isAppPassedTest(int LocalAppID,int TestTypeID) {
           
            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"
SELECT top(1) found=1
  FROM [dbo].[Tests]
  inner join 
  TestAppointments
  on tests.TestAppointmentID=TestAppointments.TestAppointmentID
  where TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID and TestTypeID=@TestTypeID and IsLocked=1 and TestResult=1;;";

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
                            isExist = true;

                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show("Error When Trying to check Passed The Test");
                    }
                    

                }

            }
            return isExist;
        }

        public static bool GetTestByTestAppointmentID(int TestAppointmentID, ref int TestID,ref bool TestResult,ref string Notes,ref int CreatedByUserID) { 
        
            bool isExist=false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"
SELECT [TestID]
      ,[TestAppointmentID]
      ,[TestResult]
      ,[Notes]
      ,[CreatedByUserID]
  FROM [dbo].[Tests]
    Where TestAppointmentID=@TestAppointmentID";

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
                                TestID = (int)Reader["TestID"];
                                TestResult = (bool)Reader["TestResult"];
                                Notes = Reader["Notes"].ToString();
                                TestID = (int)Reader["CreatedByUserID"];


                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show("Error When Trying to Get Test");
                    }

                }
            }
            return isExist;
        }
    }
}
