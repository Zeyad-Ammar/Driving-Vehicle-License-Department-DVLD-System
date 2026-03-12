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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Project
{
    public partial class frmReadPersonCard : Form
    {

        private clsPerson _Person=new clsPerson();
        private int _PersonID = -1;

        
        public frmReadPersonCard(int personID)
        {
            InitializeComponent();
            this._PersonID= personID;
            ucPersonReadCard1.setCardData(personID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void UpdatePersonData(int personID)
        {

            ucPersonReadCard1.setCardData(personID);
            

        }
        private void lbEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        
            frmAddEditPerson PersonCard = new frmAddEditPerson(_Person.PersonID);
            PersonCard.PersonData += UpdatePersonData;
            PersonCard.ShowDialog();


        }
    }
}
