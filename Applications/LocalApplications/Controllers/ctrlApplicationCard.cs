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

namespace DVLD_Project.Applications.LocalApplications.Controllers
{
    public partial class ctrlApplicationCard : UserControl
    {
        private clsApplication _Application=new clsApplication();
        public ctrlApplicationCard()
        {
            InitializeComponent();
        }

        private void _LoadDataFromObjToControl()
        {
            lbIDValue.Text = _Application.ApplicationID.ToString();
            lbStatusDateValue.Text= _Application.LastStatusDate.ToShortDateString();
            lbStatusValue.Text=_Application.ApplicationStatus.ToString();
            lbTypeValue.Text = clsApplicationType.GetApplicationType((int)_Application.ApplicationTypeID).ApplicationName;
            lbFeesValue.Text=_Application.PaidFees.ToString();
            lbDateValue.Text=_Application.ApplicationDate.ToShortDateString();
            lbCreatedByValue.Text=clsUser.GetUser(_Application.CreatedByUserID).UserName;
            lbApplicantValue.Text = clsPerson.GetPerson(_Application.ApplicationPersonID).FullName;
        }
       public int ApplicationID
        {
            set {
                _Application = clsApplication.GetApplication(value);
                _LoadDataFromObjToControl();
            }
        }
        

        private void llbViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmReadPersonCard readPersonCardForm = new frmReadPersonCard(_Application.ApplicationPersonID);
            //to update if any updates happened on the person name
            readPersonCardForm.ShowDialog();
            ApplicationID=_Application.ApplicationID;

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
