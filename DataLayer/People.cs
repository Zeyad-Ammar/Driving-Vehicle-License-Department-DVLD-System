using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataLayer
{
    public class People
    {
        
        public static bool FindPersonWithNationalNo(string NationalNo)
        {

            bool isExist = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = "Select A=1 from People where NationalNo=@NationalNo";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("NationalNo", NationalNo);

                    
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

                    }
                    

                }
            }
            return isExist;
        }

        public static bool FindPersonWithPhone(string Phone)
        {

            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = "Select A=1 from People where Phone=@Phone";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("Phone", Phone);

                   
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

                    }
                    
                }

            }

            return isExist;
        }

        public static bool FindPersonWithEmail(string Email)
        {
            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = "Select A=1 from People where Email=@Email";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("Email", Email);

                    
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

                    }
                    
                }
            }

            return isExist;
        }

        public static int AddPerson(string NationalNo,string FirstName,string SecondName,string ThirdName,string LastName,DateTime DateOfBirth,
            bool Gendor,string Address,string phone,string Email,int NationalityCountryID,string ImagePath)
        {
            int PersonID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"INSERT INTO [dbo].[People]
           ([NationalNo]
           ,[FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
           ,[DateOfBirth]
           ,[Gendor]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityCountryID]
           ,[ImagePath])
     VALUES
           (@NationalNo
           ,@FirstName
           ,@SecondName
           ,@ThirdName
           ,@LastName 
           ,@DateOfBirth 
           ,@Gendor
           ,@Address
           ,@Phone 
           ,@Email 
           ,@NationalityCountryID
           ,@ImagePath);
            Select Scope_Identity()
            ";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("NationalNo", NationalNo);
                    command.Parameters.AddWithValue("FirstName", FirstName);
                    command.Parameters.AddWithValue("SecondName", SecondName);

                    if (!string.IsNullOrEmpty(ThirdName))
                        command.Parameters.AddWithValue("ThirdName", ThirdName);
                    else
                        command.Parameters.AddWithValue("ThirdName", DBNull.Value);


                    command.Parameters.AddWithValue("LastName", LastName);
                    command.Parameters.AddWithValue("DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("Gendor", Gendor);
                    command.Parameters.AddWithValue("Address", Address);
                    command.Parameters.AddWithValue("Phone", phone);


                    if (!string.IsNullOrEmpty(Email))
                        command.Parameters.AddWithValue("Email", Email);
                    else
                        command.Parameters.AddWithValue("Email", DBNull.Value);

                    command.Parameters.AddWithValue("NationalityCountryID", NationalityCountryID);

                    if (!string.IsNullOrEmpty(ImagePath))
                        command.Parameters.AddWithValue("ImagePath", ImagePath);
                    else
                        command.Parameters.AddWithValue("ImagePath", DBNull.Value);



                    
                    try
                    {
                        connection.Open();
                        object ER = command.ExecuteScalar();
                        if (ER != null)
                        {
                            PersonID = int.Parse(ER.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show("The Program Has Error While Adding New Person.");
                    }
                   

                }

            }


            return PersonID;
        }

        public static bool UpdatePerson(int PersonID,string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            bool Gendor, string Address, string phone, string Email, int NationalityCountryID, string ImagePath)
        {
            bool Done = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"UPDATE [dbo].[People]
   SET [NationalNo] = @NationalNo
      ,[FirstName] = @FirstName
      ,[SecondName] = @SecondName
      ,[ThirdName] = @ThirdName
      ,[LastName] = @LastName
      ,[DateOfBirth] = @DateOfBirth
      ,[Gendor] = @Gendor
      ,[Address] = @Address
      ,[Phone] = @Phone
      ,[Email] = @Email
      ,[NationalityCountryID] = @NationalityCountryID
      ,[ImagePath] = @ImagePath
 WHERE PersonID=@PersonID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("PersonID", PersonID);
                    command.Parameters.AddWithValue("NationalNo", NationalNo);
                    command.Parameters.AddWithValue("FirstName", FirstName);
                    command.Parameters.AddWithValue("SecondName", SecondName);
                    command.Parameters.AddWithValue("ThirdName", ThirdName);
                    command.Parameters.AddWithValue("LastName", LastName);
                    command.Parameters.AddWithValue("DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("Gendor", Gendor);
                    command.Parameters.AddWithValue("Address", Address);
                    command.Parameters.AddWithValue("Phone", phone);
                    command.Parameters.AddWithValue("Email", Email);
                    command.Parameters.AddWithValue("NationalityCountryID", NationalityCountryID);
                    command.Parameters.AddWithValue("ImagePath", ImagePath);

                   
                    try
                    {
                        connection.Open();
                        var ER = command.ExecuteNonQuery();
                        if (ER > 0)
                        {
                            Done = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                    }
                   
                }

            }

            return Done;
        }

        public static bool GetPerson(int personID,ref string nationalNumber, ref string firstName, ref string secondName, ref string thirdName, ref string lastName,
                ref DateTime dateOfBirth, ref bool gender, ref string address, ref string phone, ref string email, ref int nationalityCountryID,ref string  ImageUrl )
        {
            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [PersonID]
      ,[NationalNo]
      ,[FirstName]
      ,[SecondName]
      ,[ThirdName]
      ,[LastName]
      ,[DateOfBirth]
      ,[Gendor]
      ,[Address]
      ,[Phone]
      ,[Email]
      ,[NationalityCountryID]
      ,[ImagePath]
  FROM [dbo].[People]
  Where PersonID=@PersonID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("PersonID", personID);

                 
                    try
                    {

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isExist = true;
                                nationalNumber = reader["NationalNo"].ToString();

                                firstName = reader["FirstName"].ToString();
                                secondName = reader["SecondName"].ToString();
                                thirdName = reader["ThirdName"].ToString();
                                lastName = reader["LastName"].ToString();
                                dateOfBirth = (DateTime)reader["DateOfBirth"];
                                gender = reader["Gendor"].ToString() == "1";
                                address = reader["Address"].ToString();
                                phone = reader["Phone"].ToString();
                                email = reader["Email"].ToString();
                                nationalityCountryID = (int)reader["NationalityCountryID"];
                                ImageUrl = reader["ImagePath"].ToString();


                            }
                        }
                        

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                    }
                }

            }

            return isExist;
            
        }

        public static bool GetPerson(  string nationalNumber,ref int personID, ref string firstName, ref string secondName, ref string thirdName, ref string lastName,
                ref DateTime dateOfBirth, ref bool gender, ref string address, ref string phone, ref string email, ref int nationalityCountryID, ref string ImageUrl)
        {
            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [PersonID]
      ,[NationalNo]
      ,[FirstName]
      ,[SecondName]
      ,[ThirdName]
      ,[LastName]
      ,[DateOfBirth]
      ,[Gendor]
      ,[Address]
      ,[Phone]
      ,[Email]
      ,[NationalityCountryID]
      ,[ImagePath]
  FROM [dbo].[People]
  Where NationalNo=@NationalNo";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("NationalNo", nationalNumber);

                    
                    try
                    {

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isExist = true;
                                personID = (int)reader["PersonID"];
                                firstName = reader["FirstName"].ToString();
                                secondName = reader["SecondName"].ToString();
                                thirdName = reader["ThirdName"].ToString();
                                lastName = reader["LastName"].ToString();
                                dateOfBirth = (DateTime)reader["DateOfBirth"];
                                gender = reader["Gendor"].ToString() == "1";
                                address = reader["Address"].ToString();
                                phone = reader["Phone"].ToString();
                                email = reader["Email"].ToString();
                                nationalityCountryID = (int)reader["NationalityCountryID"];
                                ImageUrl = reader["ImagePath"].ToString();


                            }
                        }
                       

                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                    }

                }
                
            }

            return isExist;

        }

        public static DataTable GetAllPeople()
        {

            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"SELECT [PersonID]
      ,[NationalNo]
      ,[FirstName]
      ,[SecondName]
      ,[ThirdName]
      ,[LastName]
      ,[DateOfBirth]
      ,[Gendor]
	  ,case
	  when Gendor=1 then 'Male'
	  else 'Female'
	  end
	  as Gendar
      ,[Address]
      ,[Phone]
      ,[Email]
      ,[NationalityCountryID]
      ,[ImagePath]
	  ,[CountryName]
  FROM [dbo].[People]
  inner join
  Countries
  on CountryID=NationalityCountryID;";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                   

                    try
                    {
                        connection.Open();
                       using( var reader = command.ExecuteReader())
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

                    }
                   

                }

            }

            return dt;
        }

        public static bool DeletePersonID(int personID)
        {
            bool done = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {

                string Query = @"DELETE FROM [dbo].[People]
      WHERE PersonID=@personID ";

                using (SqlCommand cmd = new SqlCommand(Query, connection))
                {

                    cmd.Parameters.AddWithValue("personID", personID);
                   
                    try
                    {
                        connection.Open();
                        var effectedRecords = cmd.ExecuteNonQuery();
                        if (effectedRecords > 0)
                        {
                            done = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        clsUtilityDataLayer.LogError(ex);

                        MessageBox.Show(ex.Message);
                    }
                  
                }
            }
            return done;
        }

    }
}
