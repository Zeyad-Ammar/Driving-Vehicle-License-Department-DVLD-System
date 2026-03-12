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
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string Query = @"SELECT [ApplicationTypeID]
      ,[ApplicationTypeTitle]
      ,[ApplicationFees]
  FROM [dbo].[ApplicationTypes]";

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
                MessageBox.Show("Error While Try Back Application Types");
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool UpdateApplicationType(int ApplicationTypeID,decimal NewFees)
        {

            SqlConnection connection=new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"UPDATE [dbo].[ApplicationTypes]
   SET 
      [ApplicationFees] = @ApplicationFees
 WHERE ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command= new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("ApplicationFees", NewFees);
            bool isDone = false;
            try
            {
                connection.Open();
                var AffectedRows = command.ExecuteNonQuery();
                if (AffectedRows>0)
                {
                   isDone = true;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error While Trying UpdateApplicationStatus The Price");
            }
            finally { 
                connection.Close();
            }

            



            return isDone;
        }

        public static bool GetApplicationType(int ApplicationTypeID,ref string ApplicationTypeName,ref decimal ApplicationtypeFees) {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [ApplicationTypeID]
      ,[ApplicationTypeTitle]
      ,[ApplicationFees]
  FROM [dbo].[ApplicationTypes]
Where ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command=new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);

            bool isDone=false;
            try
            {
                connection.Open();
                var Reader=command.ExecuteReader();
                if (Reader.Read())
                {
                    isDone=true;
                    ApplicationTypeName = Reader["ApplicationTypeTitle"].ToString();
                    ApplicationtypeFees = (decimal)Reader["ApplicationFees"];

                }

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close(); 
            }

            return isDone;
        }


    }
}
