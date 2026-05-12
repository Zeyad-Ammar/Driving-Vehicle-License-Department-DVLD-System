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
    public class LicenseClasses
    {
        public static DataTable GetAllLicenseClasses()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [LicenseClassID]
      ,[ClassName]
      ,[ClassDescription]
      ,[MinimumAllowedAge]
      ,[DefaultValidityLength]
      ,[ClassFees]
  FROM [dbo].[LicenseClasses]";

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
                clsUtilityDataLayer.LogError(ex);

                MessageBox.Show("Error When Trying To Get License Classes Data");
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool GetLicenseClass(int LicenseClassID,ref string ClassName,ref string ClassDescription,ref int MinimumAllowedAge,ref int DefaultValidityLength,ref decimal ClassFees)
        {
            bool isDone=false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"SELECT [LicenseClassID]
      ,[ClassName]
      ,[ClassDescription]
      ,[MinimumAllowedAge]
      ,[DefaultValidityLength]
      ,[ClassFees]
  FROM [dbo].[LicenseClasses]
    Where LicenseClassID=@LicenseClassID";

            SqlCommand command= new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("LicenseClassID", LicenseClassID);

            try
            {
                connection.Open ();
                var Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isDone = true;
                    ClassName = Reader["ClassName"].ToString();
                    ClassDescription = Reader["ClassDescription"].ToString();
                    MinimumAllowedAge = Convert.ToInt32(Reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt32(Reader["DefaultValidityLength"]);
                    ClassFees = (decimal)Reader["ClassFees"];

                }
                Reader.Close();

            } 
            catch (Exception ex) 
            {
                clsUtilityDataLayer.LogError(ex);

                MessageBox.Show("Error When Trying To Get License Class Data");
            }
            finally
            {
                connection.Close();
            }

            return isDone;

        }

    }
}
