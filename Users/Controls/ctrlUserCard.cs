using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Users
{
    public partial class ctrlUserCard : UserControl
    {
        private clsUser _User=new clsUser();

       
        public ctrlUserCard()
        {
            InitializeComponent();
           
        }

        public void SetUserInfo(int UserID)
        {
            _User = clsUser.GetUser(UserID);
            

            ucPersonReadCard1.setCardData(_User.PersonInfo.PersonID);
            _SetUserInfoData();
        }

        private void _SetUserInfoData()
        {
            lbIsActiveValue.Text = "Yes";
            if (!_User.isActive)
            {
                lbIsActiveValue.Text = "No";

            }

            lbUserNameValue.Text = _User.UserName;
            lbUserIDValue.Text = _User.UserID.ToString();

        }
        private void ctrlUserCard_Load(object sender, EventArgs e)
        {

         

        }
    }
}
