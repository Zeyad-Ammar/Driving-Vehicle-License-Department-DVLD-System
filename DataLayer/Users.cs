using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace DataLayer
{
    public class Users
    {
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {


                string Query = @"SELECT [UserID]
      ,Users.PersonID
	  ,(FirstName+' '+SecondName+' '+ThirdName+' '+LastName) as FullName
      ,[UserName]
      ,[Password]
      ,[IsActive]
  FROM [dbo].[Users]
  inner join
  People
  on People.PersonID=Users.PersonID;";


                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {

                        clsUtilityDataLayer.LogError(ex);
                        MessageBox.Show("Error While Try Get Users Data");
                    }
                   
                }
            }

            return dt;
        }

        public static int AddUser(int PersonID,string UserName,string Password,bool isActive)
        {
            int UserID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string Query = @"INSERT INTO [dbo].[Users]
           ([PersonID]
           ,[UserName]
           ,[Password]
           ,[IsActive])
     VALUES
           (@PersonID
           ,@UserName
           ,@Password
           ,@IsActive);
Select Scope_Identity()
    ";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("PersonID", PersonID);
                    command.Parameters.AddWithValue("UserName", UserName);
                    command.Parameters.AddWithValue("Password", Password);
                    command.Parameters.AddWithValue("IsActive", isActive);

                   
                    try
                    {
                        connection.Open();
                        object ID = command.ExecuteScalar();
                        if (ID != null)
                        {
                            UserID = Convert.ToInt32(ID);
                        }

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);
                        MessageBox.Show("Error When Try Add New User");
                    }
                    
                }
            }

            return UserID;

        }

        public static bool UpdateUser( int UserID,string UserName, string Password, bool isActive)
        {
            bool isDone = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string Query = @"UPDATE [dbo].[Users]
   SET 
       [UserName] = @UserName
      ,[Password] = @Password
      ,[IsActive] = @IsActive
 WHERE 
        UserID=@UserID;
    ";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("UserID", UserID);
                    command.Parameters.AddWithValue("UserName", UserName);
                    command.Parameters.AddWithValue("Password", Password);
                    command.Parameters.AddWithValue("IsActive", isActive);

                   

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

                        MessageBox.Show("Error When Try Update New User");
                    }
                }
                
            }

            return isDone;

        }

        public static bool DeleteUser(int UserID) 
        {
            bool isDone = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"DELETE FROM [dbo].[Users]
      WHERE UserID=@UserID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("UserID", UserID);

                   
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

                        MessageBox.Show("Error While Tring Delete User");

                    }

                }
                

            }

            return isDone;

        }

        public static bool GetUser(int UserID,ref int PersonID,ref string UserName,ref string Password, ref bool IsActive) {

            bool isDone = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [UserID]
      ,[PersonID]
      ,[UserName]
      ,[Password]
      ,[IsActive]
  FROM [dbo].[Users]
Where UserID=@UserID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("UserID", UserID);

                    
                    try
                    {
                        connection.Open();

                        var Reader = command.ExecuteReader();
                        if (Reader.Read())
                        {
                            isDone = true;
                            PersonID = (int)Reader["PersonID"];
                            UserName = Reader["UserName"].ToString();
                            Password = Reader["Password"].ToString();
                            IsActive = (bool)Reader["IsActive"];


                        }
                        Reader.Close();

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);
                        MessageBox.Show("Error While Tring Get User");
                    }
                  
                }
            }

            return isDone;
        }

        public static bool GetUser(string UserName,ref int UserID, ref int PersonID,  string Password, ref bool IsActive)
        {
            bool isDone = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [UserID]
      ,[PersonID]
      ,[UserName]
      ,[Password]
      ,[IsActive]
  FROM [dbo].[Users]
Where UserName=@UserName and Password=@Password";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("UserName", UserName);
                    command.Parameters.AddWithValue("Password", Password);


                   
                    try
                    {
                        connection.Open();

                        using (var Reader = command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                isDone = true;
                                PersonID = (int)Reader["PersonID"];
                                UserID = (int)Reader["UserID"];
                                IsActive = (bool)Reader["IsActive"];


                            }
                        }
                       

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);
                        MessageBox.Show("Error While Tring Get User");
                    }
                    
                }
            }

            return isDone;
        }

        public static bool isAccountExist(string UserName,string Password)
        {
            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT Found=1
  FROM [dbo].[Users]
Where [UserName]=@UserName and [Password]=@Password and [IsActive]=1";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("UserName", UserName);
                    command.Parameters.AddWithValue("Password", Password);

                   
                    try
                    {
                        connection.Open();

                        var obj = command.ExecuteScalar();
                        if (obj != null)
                        {
                            isExist = true;
                        }


                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);
                        MessageBox.Show("Error While Tring Check Account Existing");
                    }
                   

                }

            }

            return isExist;


        }
        public static bool isPersonUser(int PersonID)
        {
            bool isDone = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT Found=1
  FROM [dbo].[Users]
Where PersonID=@PersonID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("PersonID", PersonID);

                    
                    try
                    {
                        connection.Open();

                        var obj = command.ExecuteScalar();
                        if (obj != null)
                        {
                            isDone = true;
                        }


                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);
                        MessageBox.Show("Error While Tring Get User");
                    }
                    
                }

            }

            return isDone;
        }

    }
}
