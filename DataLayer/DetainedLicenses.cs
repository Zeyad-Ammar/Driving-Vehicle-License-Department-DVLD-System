using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataLayer
{
    public class DetainedLicenses
    {
        public static bool isLicenseDetained(int LicenseID)
        {
            bool isDetained=false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"
  select top(1) found=1
  from DetainedLicenses
  where LicenseID=@LicenseID and [IsReleased]=0;";


            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("LicenseID", LicenseID);


            try
            {
                connection.Open();
                var obj = command.ExecuteScalar();
                if (obj!=null)
                {
                    isDetained = true;
                   
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error When Trying To check License is Detained?");
            }
            finally
            {
                connection.Close();
            }

            return isDetained;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT *  FROM [dbo].[DetainedLicenses_View] ";


            SqlCommand command = new SqlCommand(Query, connection);

            DataTable dt = new DataTable();
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
                MessageBox.Show("Error When Trying To Get The Detained Licenses Data");
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static int AddDetainLicense(int LicenseID,decimal FineFees,int CreatedByUserID )
        {
            

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"
  INSERT INTO [dbo].[DetainedLicenses]
           ([LicenseID]
           ,[DetainDate]
           ,[FineFees]
           ,[CreatedByUserID]
           ,[IsReleased]
           ,[ReleaseDate]
           ,[ReleasedByUserID]
           ,[ReleaseApplicationID])
     VALUES
           (@LicenseID
           ,@DetainDate
           ,@FineFees
           ,@CreatedByUserID
           ,@IsReleased
           ,@ReleaseDate
           ,@ReleasedByUserID
           ,@ReleaseApplicationID);
        Select SCOPE_IDENTITY();";


            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("LicenseID", LicenseID);
            command.Parameters.AddWithValue("DetainDate", DateTime.Now);
            command.Parameters.AddWithValue("FineFees", FineFees);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("IsReleased", 0);
            command.Parameters.AddWithValue("ReleaseDate", DBNull.Value);
            command.Parameters.AddWithValue("ReleasedByUserID", DBNull.Value);
            command.Parameters.AddWithValue("ReleaseApplicationID", DBNull.Value);


            int newID = -1;
            try
            {
                connection.Open();
                var obj = command.ExecuteScalar();
                if (obj != null)
                {
                    newID = Convert.ToInt32(obj);


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error When Trying To Detain License");
            }
            finally
            {
                connection.Close();
            }



            return newID;
        }

        public static bool GetDetainedLicenseByLicenseID(int LicenseID, ref int DetainedID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool isReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [DetainID]
      ,[LicenseID]
      ,[DetainDate]
      ,[FineFees]
      ,[CreatedByUserID]
      ,[IsReleased]
      ,[ReleaseDate]
      ,[ReleasedByUserID]
      ,[ReleaseApplicationID]
  FROM [dbo].[DetainedLicenses]
Where LicenseID=@LicenseID and IsReleased=0";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("LicenseID", LicenseID);

            
            try
            {
                connection.Open();
                var Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    DetainedID = (int)Reader["DetainID"];
                    DetainDate = (DateTime)Reader["DetainDate"];
                    FineFees = (decimal)Reader["FineFees"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    isReleased = (bool)Reader["IsReleased"];
                    if (Reader["ReleaseDate"] != DBNull.Value)
                    {
                        ReleaseDate = (DateTime)Reader["ReleaseDate"];
                    }
                    if (Reader["ReleasedByUserID"] != DBNull.Value)
                    {
                        ReleasedByUserID = (int)Reader["ReleasedByUserID"];
                    }
                    if (Reader["ReleaseApplicationID"] != DBNull.Value)
                    {
                        ReleaseApplicationID = (int)Reader["ReleaseApplicationID"];
                    }
                    isFound = true;
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error When Trying To Get The Detained License Data");
            }
            finally
            {
                connection.Close();
            }


            return isFound;
        }

        public static bool GetDetainedLicenseByDetainID(int DetainedID , ref int LicenseID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool isReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [DetainID]
      ,[LicenseID]
      ,[DetainDate]
      ,[FineFees]
      ,[CreatedByUserID]
      ,[IsReleased]
      ,[ReleaseDate]
      ,[ReleasedByUserID]
      ,[ReleaseApplicationID]
  FROM [dbo].[DetainedLicenses]
Where DetainID=@DetainID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("DetainID", DetainedID);


            try
            {
                connection.Open();
                var Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    LicenseID = (int)Reader["LicenseID"];
                    DetainDate = (DateTime)Reader["DetainDate"];
                    FineFees = (decimal)Reader["FineFees"];
                    CreatedByUserID = (int)Reader["CreatedByUserID"];
                    isReleased = (bool)Reader["IsReleased"];
                    if (Reader["ReleaseDate"] != DBNull.Value)
                    {
                        ReleaseDate = (DateTime)Reader["ReleaseDate"];
                    }
                    if (Reader["ReleasedByUserID"] != DBNull.Value)
                    {
                        ReleasedByUserID = (int)Reader["ReleasedByUserID"];
                    }
                    if (Reader["ReleaseApplicationID"] != DBNull.Value)
                    {
                        ReleaseApplicationID = (int)Reader["ReleaseApplicationID"];
                    }
                    isFound = true;
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error When Trying To Get The Detained License Data");
            }
            finally
            {
                connection.Close();
            }


            return isFound;
        }

        public static bool ReleaseDetainedLicense(int DetainID,int ReleasedByUserID, int ReleaseApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            bool isDone = false;
            string Query = @"
            UPDATE [dbo].[DetainedLicenses]
   SET 
       [IsReleased] = @IsReleased
      ,[ReleaseDate] = @ReleaseDate
      ,[ReleasedByUserID] = @ReleasedByUserID
      ,[ReleaseApplicationID] = @ReleaseApplicationID
 WHERE 
    [DetainID]=@DetainID and IsReleased=0;
            ";


            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("DetainID", DetainID);
            command.Parameters.AddWithValue("IsReleased", 1);
            command.Parameters.AddWithValue("ReleaseDate", DateTime.Now);
            command.Parameters.AddWithValue("ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("ReleaseApplicationID", ReleaseApplicationID);


          
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
                MessageBox.Show("Error When Trying To Release Detained License");
            }
            finally
            {
                connection.Close();
            }



            return isDone;
        }

        
    }
}
