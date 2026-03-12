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
    public class TestTypes
    {


        public static DataTable GetAllTestTypes()
        {
            SqlConnection connection=new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [TestTypeID]
      ,[TestTypeTitle]
      ,[TestTypeDescription]
      ,[TestTypeFees]
  FROM [dbo].[TestTypes]";

            SqlCommand command = new SqlCommand(Query, connection);
            DataTable dt=new DataTable();
            try
            {
                connection.Open();
                var Reader=command.ExecuteReader();
                if (Reader.HasRows) { 
                    dt.Load(Reader);
                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error When Trying To Get Test Types");
            }
            finally { 
            connection.Close();
            }

            return dt;
        }

        public static bool GetTestType(int TestTypeID,ref string TestTypeTitle,ref string TestTypeDescription,ref decimal TestTypeFees) {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [TestTypeID]
      ,[TestTypeTitle]
      ,[TestTypeDescription]
      ,[TestTypeFees]
  FROM [dbo].[TestTypes]
Where TestTypeID=@TestTypeID";



            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);

            bool isExist = false;
            try
            {
                connection.Open();
                var Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isExist = true;
                    TestTypeTitle = Reader["TestTypeTitle"].ToString();
                    TestTypeDescription = Reader["TestTypeDescription"].ToString() ;
                    TestTypeFees = (decimal)Reader["TestTypeFees"];

                }

                Reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error When Trying To Get The Test Type");
            }
            finally
            {
                connection.Close();
            }

            return isExist;
        }

        public static bool UpdateTestTypeDescriptionAndFees(int TestTypeID, string TestTypeDescription, decimal TestFees)
        {

            SqlConnection connection=new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"UPDATE [dbo].[TestTypes]
   SET 
      [TestTypeDescription] = @TestTypeDescription
      ,[TestTypeFees] = @TestTypeFees
 WHERE TestTypeID=@TestTypeID";


            SqlCommand command= new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("TestTypeFees", TestFees);



            bool isDone =false;

            try
            {
                connection.Open();
                var AffectedRows=command.ExecuteNonQuery();
                if (AffectedRows > 0)
                {
                    isDone = true;

                }

            }
            catch (Exception ex) {
                MessageBox.Show("Error When Trying To UpdateApplicationStatus The Test Type Data");
            }
            finally
            {
                connection.Close();
            }


            return isDone;

        }
    }
}
