using BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DVLD_Project.General
{
    static class clsCurrentUser
    {
        public static clsUser User { get; private set; }

        

        private static string _path = @"C:\RememberUser\User.txt";

        public static bool checkAuthentication(string UserName,string Password)
        {
         
            User=clsUser.FindAccount(UserName,Password);

            return User != null;
        }

         

    }
}
