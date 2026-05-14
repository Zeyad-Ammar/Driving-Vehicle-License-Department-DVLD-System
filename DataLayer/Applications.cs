using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DataLayer
{
    public class Applications
    {

        public static int AddApplication(int personID, DateTime ApplicationDate, int ApplicationTypeID,
            int ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int ID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"INSERT INTO [dbo].[Applications]
           ([ApplicantPersonID]
           ,[ApplicationDate]
           ,[ApplicationTypeID]
           ,[ApplicationStatus]
           ,[LastStatusDate]
           ,[PaidFees]
           ,[CreatedByUserID])
     VALUES
           (@ApplicantPersonID
           ,@ApplicationDate
           ,@ApplicationTypeID
           ,@ApplicationStatus
           ,@LastStatusDate
           ,@PaidFees
           ,@CreatedByUserID);
Select SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("ApplicantPersonID", personID);
                    command.Parameters.AddWithValue("ApplicationDate", ApplicationDate);
                    command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("ApplicationStatus", ApplicationStatus);
                    command.Parameters.AddWithValue("LastStatusDate", LastStatusDate);
                    command.Parameters.AddWithValue("PaidFees", PaidFees);
                    command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);

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

                        MessageBox.Show("Error When Trying to Add New Application");

                    }

                }

            }

            return ID;
        }

        public static bool UpdateApplicationStatus(int ApplicationID, int ApplicationStatus)
        {
            bool isDone = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"UPDATE [dbo].[Applications]
   SET 
      [ApplicationStatus] = @ApplicationStatus
      ,[LastStatusDate]=@LastStatusDate
 WHERE [ApplicationID]=@ApplicationID
";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("ApplicationStatus", ApplicationStatus);
                    command.Parameters.AddWithValue("LastStatusDate", DateTime.Now);

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
                        MessageBox.Show("Error When Trying to update The Application");
                    }

                }

            }
            return isDone;
        }

        public static bool GetApplication(int ApplicationID,ref int AppPersonID,ref DateTime  AppDate,ref int AppTypeID,ref int AppStatus,ref DateTime LastStatusDate,ref decimal PaidFees,ref int CreatedByUserID)
        {
            bool isDone = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [ApplicationID]
      ,[ApplicantPersonID]
      ,[ApplicationDate]
      ,[ApplicationTypeID]
      ,[ApplicationStatus]
      ,[LastStatusDate]
      ,[PaidFees]
      ,[CreatedByUserID]
  FROM [dbo].[Applications]
    Where ApplicationID=@ApplicationID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("ApplicationID", ApplicationID);

                    try
                    {
                        connection.Open();
                        using (var Reader = command.ExecuteReader())
                        {


                            if (Reader.Read())
                            {
                                isDone = true;
                                AppPersonID = (int)Reader["ApplicantPersonID"];
                                AppDate = (DateTime)Reader["ApplicationDate"];
                                AppTypeID = (int)Reader["ApplicationTypeID"];
                                AppStatus = Convert.ToInt32(Reader["ApplicationStatus"]);
                                LastStatusDate = (DateTime)Reader["LastStatusDate"];
                                PaidFees = (decimal)Reader["PaidFees"];
                                CreatedByUserID = (int)Reader["CreatedByUserID"];
                            }

                        }
                       
                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show("Error When Trying to Get The Application");
                    }
                   
                }

            }
            return isDone;
        }

        public static bool DeleteApplication(int AppID)
        {
           bool isDone = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"DELETE FROM [dbo].[Applications]
      WHERE [ApplicationID]=@ApplicationID
";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("ApplicationID", AppID);

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

                        MessageBox.Show($"Error when trying to Delete App Data");
                    }
                   

                }

            }
            return isDone;
        
        }
    }


   
}
