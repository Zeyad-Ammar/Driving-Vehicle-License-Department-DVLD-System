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
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [TestTypeID]
      ,[TestTypeTitle]
      ,[TestTypeDescription]
      ,[TestTypeFees]
  FROM [dbo].[TestTypes]";

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

                        MessageBox.Show("Error When Trying To Get Test Types");
                    }

                }

            }

            return dt;
        }

        public static bool GetTestType(int TestTypeID,ref string TestTypeTitle,ref string TestTypeDescription,ref decimal TestTypeFees) {

            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [TestTypeID]
      ,[TestTypeTitle]
      ,[TestTypeDescription]
      ,[TestTypeFees]
  FROM [dbo].[TestTypes]
Where TestTypeID=@TestTypeID";



                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("TestTypeID", TestTypeID);

                   
                    try
                    {
                        connection.Open();
                        using (var Reader = command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                isExist = true;
                                TestTypeTitle = Reader["TestTypeTitle"].ToString();
                                TestTypeDescription = Reader["TestTypeDescription"].ToString();
                                TestTypeFees = (decimal)Reader["TestTypeFees"];

                            }
                        }

                        
                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);
                        MessageBox.Show("Error When Trying To Get The Test Type");
                    }
                }
               
            }
            return isExist;
        }

        public static bool UpdateTestTypeDescriptionAndFees(int TestTypeID, string TestTypeDescription, decimal TestFees)
        {

            bool isDone = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"UPDATE [dbo].[TestTypes]
   SET 
      [TestTypeDescription] = @TestTypeDescription
      ,[TestTypeFees] = @TestTypeFees
 WHERE TestTypeID=@TestTypeID";


                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("TestTypeDescription", TestTypeDescription);
                    command.Parameters.AddWithValue("TestTypeFees", TestFees);



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
                        MessageBox.Show("Error When Trying To UpdateApplicationStatus The Test Type Data");
                    }
                    
                }
            }


            return isDone;

        }
    }
}
