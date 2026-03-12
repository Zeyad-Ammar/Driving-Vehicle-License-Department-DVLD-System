using BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace DVLD_Project.General
{
    static class clsCurrentUser
    {
        public static clsUser User=new clsUser();

        

        private static string _path = @"C:\RememberUser\User.txt";

        public static bool checkAuthentication(string UserName,string Password)
        {
         
            User=clsUser.FindAccount(UserName,Password);

            return User.UserID != -1;
        }


        public static bool StoreCurrentUserCredentials()
        {


            try
            {
                
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


               
                string filePath = currentDirectory + "\\LoginData.txt";

                
                if (User.UserName == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

               
                string dataToSave = User.UserName + "#//#" + User.Password;

                
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    
                    writer.WriteLine(dataToSave);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }





        }

        public static bool LoadStoredCredentials(ref string UserName,ref string Password)
        {
            
            try
            {
               
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                string filePath = currentDirectory + "\\LoginData.txt";

               
                if (File.Exists(filePath))
                {
                   
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                       
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); 
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            UserName = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }


        }

         

    }
}
