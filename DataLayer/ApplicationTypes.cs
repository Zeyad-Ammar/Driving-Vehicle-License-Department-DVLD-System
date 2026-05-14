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
    public class ApplicationTypes
    {

        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string Query = @"SELECT [ApplicationTypeID]
      ,[ApplicationTypeTitle]
      ,[ApplicationFees]
  FROM [dbo].[ApplicationTypes]";

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

                        MessageBox.Show("Error While Try Back Application Types");
                    }
                    
                }
            }
            return dt;
        }

        public static bool UpdateApplicationType(int ApplicationTypeID, decimal NewFees)
        {
            bool isDone = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"UPDATE [dbo].[ApplicationTypes]
   SET 
      [ApplicationFees] = @ApplicationFees
 WHERE ApplicationTypeID=@ApplicationTypeID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {



                    command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("ApplicationFees", NewFees);
                  
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

                        MessageBox.Show("Error While Trying UpdateApplicationStatus The Price");
                    }
                  
                }
            }



            return isDone;
        }

        public static bool GetApplicationType(int ApplicationTypeID,ref string ApplicationTypeName,ref decimal ApplicationtypeFees) {

            bool isDone = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {



                string Query = @"SELECT [ApplicationTypeID]
      ,[ApplicationTypeTitle]
      ,[ApplicationFees]
  FROM [dbo].[ApplicationTypes]
Where ApplicationTypeID=@ApplicationTypeID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);

                    
                    try
                    {
                        connection.Open();
                        using (var Reader = command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                isDone = true;
                                ApplicationTypeName = Reader["ApplicationTypeTitle"].ToString();
                                ApplicationtypeFees = (decimal)Reader["ApplicationFees"];

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                    }
                   
                }
            }
            return isDone;
        }


    }
}
