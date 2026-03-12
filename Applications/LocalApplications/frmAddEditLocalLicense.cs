using BusinessLayer;
using DVLD_Project.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmAddEditLocalLicense : Form
    {
        private enum enMode { Add,Edit};

        private enMode Mode = enMode.Add;
       
        clsLocalDrivingLicenseApp _LocalDrivingLicenseApp=new clsLocalDrivingLicenseApp();
        public frmAddEditLocalLicense()
        {
            InitializeComponent();
        }

        public frmAddEditLocalLicense(int LocalAppID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApp=clsLocalDrivingLicenseApp.GetLocalLicenseApp(LocalAppID);
            Mode = enMode.Edit;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.getSelectedPersonID() < 1) {
                MessageBox.Show("Select Right Person First or Add it");
                return;
            }

            tabControler.SelectedIndex = 1;
            btnSave.Enabled = true;

        }

        private void _FillCompoBoxWithClassesName()
        {
            DataTable Classes = clsLicenseClass.GetAllLicenseClasses();
            foreach (DataRow cl in Classes.Rows)
            {
                comboBox1.Items.Add(cl["ClassName"]);
            }

        }
        private void frmLocalLicense_Load(object sender, EventArgs e)
        {
            if (Mode == enMode.Edit)
            {
                ctrlPersonCardWithFilter1.SetPersonData(_LocalDrivingLicenseApp.ApplicationPersonID);
                ctrlPersonCardWithFilter1.isFilterEnable = false;
                lbDLApplicationIDValue.Text=_LocalDrivingLicenseApp.LDLicenseAppID.ToString();
            }
            btnSave.Enabled = false;
            _FillCompoBoxWithClassesName();
            comboBox1.SelectedIndex = 2;
            lbApplicationDateValue.Text =DateTime.Now.ToShortDateString();
            lbApplicationFeesValue.Text = clsApplicationType.GetApplicationType(1).ApplicationFees.ToString();

            lbCreatedByValue.Text=clsCurrentUser.User.UserName;


        }

        private void _LoadDataToLocalDrivingLicenseAppObj()
        {
            _LocalDrivingLicenseApp.LicenseClassID = comboBox1.SelectedIndex + 1;
            _LocalDrivingLicenseApp.ApplicationPersonID=ctrlPersonCardWithFilter1.getSelectedPersonID();
            _LocalDrivingLicenseApp.CreatedByUserID=clsCurrentUser.User.UserID;
           
        }
        
        private bool _CheckValidation()
        {
           

            if(clsLocalDrivingLicenseApp.isPersonHaveNotCanceledLocalAppWithThisClass(ctrlPersonCardWithFilter1.getSelectedPersonID(),comboBox1.SelectedIndex+1))
            {
                MessageBox.Show("This Person Is Already Have Local License of This Class choose another one");
                return false;
            }



            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_CheckValidation()) { 
                return;
            }


            _LoadDataToLocalDrivingLicenseAppObj();


            if (!_LocalDrivingLicenseApp.Save())
            {
                MessageBox.Show("Error could not Add This Local Driving License");
                return;

            }

            MessageBox.Show("The Data Saved Successfully");

            lbDLApplicationIDValue.Text=_LocalDrivingLicenseApp.LDLicenseAppID.ToString();
            
        }
    }
}
