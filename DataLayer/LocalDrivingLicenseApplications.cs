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
    public class LocalDrivingLicenseApplications
    {

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"  
select * from [LDLicenseApplicationView]
Order by ApplicationDate;
";

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

                        MessageBox.Show("Error When Trying To get Local License Applications");
                    }
                    

                }

            }

            return dt;

        }

        public static bool isPersonHaveNotCanceledLicenseWithThisClass(int PersonID,int LicenseClassID)
        {
            bool isExist=false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT Found=1
  FROM [DVLD].[dbo].[LocalDrivingLicenseApplications]
  inner join Applications
  on Applications.ApplicationID=LocalDrivingLicenseApplications.ApplicationID
Where Applications.ApplicantPersonID=@PersonID and LicenseClassID=@LicenseClassID and Applications.ApplicationStatus  <> 2;";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("PersonID", PersonID);
                    command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);

                    try
                    {
                        connection.Open();
                        var found = command.ExecuteScalar();
                        if (found != null)
                        {
                            isExist = true;
                        }


                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show($"Error when try to check if the LDLApp for The Person W/ ID {PersonID} w/ Class {LicenseClassID} is Exist");
                    }
                    

                }
            }




            return isExist;
        }


        public static int AddLocalApplication(int ApplicationID,int LicenseClassID)
        {
            int ID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"INSERT INTO [dbo].[LocalDrivingLicenseApplications]
           ([ApplicationID]
           ,[LicenseClassID])
     VALUES
           (@ApplicationID
           ,@LicenseClassID);
            Select Scope_Identity()";


                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);

                   
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

                        MessageBox.Show("Error When Trying to add new Local Driving Licence App");
                    }
                    
                }
            }

            return ID;
        }

        public static bool UpdateLocalApplication(int LocalAppID,int LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"UPDATE [dbo].[LocalDrivingLicenseApplications]
   SET 
      [LicenseClassID] = @LicenseClassID
 WHERE [LocalDrivingLicenseApplicationID]=@LocalDrivingLicenseApplicationID";


            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalAppID);
            command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);

           bool isDone=false;
            try
            {
                connection.Open();
                var AffectedRows = command.ExecuteNonQuery();
                if (AffectedRows>0)
                {
                    isDone=true;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error When Trying to add new Local Driving Licence App");
            }
            finally
            {
                connection.Close();
            }

            return isDone;
        }
        public static bool GetLocalApplication(int LocalAppID, ref int AppID,ref int LicenseClassID,ref int PassedTests)
        {
            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"  select LocalDrivingLicenseApplicationID
      ,ApplicationID
      ,LicenseClassID
	  , PassedTests = case
	  when PassedTests=1 then 1
	  when PassedTests=2 then 2
	  when PassedTests=3 then 3 
	  else 0
	  end
	  
	  from (SELECT LocalDrivingLicenseApplications.[LocalDrivingLicenseApplicationID]
      ,LocalDrivingLicenseApplications.[ApplicationID]
      ,LocalDrivingLicenseApplications.[LicenseClassID]
	  , PassedTests=sum(cast(Tests.TestResult  as int))
	  from
	  [LocalDrivingLicenseApplications]
	  left join
	  TestAppointments
	inner join Tests
  on Tests.TestAppointmentID=TestAppointments.TestAppointmentID
  on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=TestAppointments.LocalDrivingLicenseApplicationID
  where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID
  group by LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,LocalDrivingLicenseApplications.[ApplicationID]
      ,LocalDrivingLicenseApplications.[LicenseClassID]) as t1
";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalAppID);


                    try
                    {
                        connection.Open();
                        using (var Reader = command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                isExist = true;
                                AppID = (int)Reader["ApplicationID"];
                                LicenseClassID = (int)Reader["LicenseClassID"];
                                PassedTests = (int)Reader["PassedTests"];
                            }

                        }

                        

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show($"Error when trying to Get Local App Data");
                    }
                  
                }

            }


            return isExist;
        }

        public static bool DeleteLocalApplication(int LocalAppID)
        {
            bool isDone = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"DELETE FROM [dbo].[LocalDrivingLicenseApplications]
      WHERE [LocalDrivingLicenseApplicationID]=@LocalDrivingLicenseApplicationID
";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LocalAppID);


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

                        MessageBox.Show($"Error when trying to Delete Local App Data");
                    }

                }

            }


            return isDone;
        }
    }
}
