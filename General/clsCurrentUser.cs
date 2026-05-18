using BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Microsoft.Win32;

namespace DVLD_Project.General
{
    static class clsCurrentUser
    {
        public static clsUser User=new clsUser();

        

        private static string _path = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

        private static string _DataName = @"SAVEDUSERNAME";

       
        public static bool checkAuthentication(string UserName,string Password)
        {
         
            User=clsUser.FindAccount(UserName,Password);

            return User.UserID != -1;
        }


        public static bool StoreCurrentUserCredentials(string UserName,string Password)
        {


            try
            {
                
              
                
               
                string dataToSave = UserName + "#//#" + Password;

                Registry.SetValue(_path,_DataName, dataToSave,RegistryValueKind.String);

                return true;
               
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
               
                var StoredCredentials = Registry.GetValue(_path,_DataName, null) as string;

                if (StoredCredentials != null)
                {
                    string[] LoginCredintals = StoredCredentials.Split(new string[] { "#//#" }, StringSplitOptions.None);


                    UserName = LoginCredintals[0];
                    Password = LoginCredintals[1];
                    return true;
                }
                

               
           
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                
            }

            return false;

        }

         

    }
}
