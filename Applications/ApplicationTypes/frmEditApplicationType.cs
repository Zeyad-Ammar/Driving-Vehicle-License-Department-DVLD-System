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

namespace DVLD_Project
{
    public partial class frmEditApplicationType : Form
    {
        private clsApplicationType _ApplicationType=new clsApplicationType();
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationType=clsApplicationType.GetApplicationType(ApplicationTypeID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadDataFromObjToForm()
        {
            lbIDValue.Text=_ApplicationType.ApplicationTypeID.ToString();
            lbApplicationTypeName.Text=_ApplicationType.ApplicationName;
            tbFees.Text=_ApplicationType.ApplicationFees.ToString();

        }

        private bool _CheckValidation()
        {
            if (string.IsNullOrEmpty(tbFees.Text)||!clsValidation.IsNumber(tbFees.Text))
            {
                errorProvider1.SetError(tbFees, "The Fees is Required And Should Be Number");
                return false;
            }
            errorProvider1.SetError(tbFees, "");



            return true;
        }

        private void _LoadDataFromFormToObj()
        {
            _ApplicationType.ApplicationFees = Convert.ToDecimal(tbFees.Text);

        }
        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _LoadDataFromObjToForm();

            

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_CheckValidation())
            {
                MessageBox.Show("Error in Inputs put the mouse on the red icon And Read it!");
                return;
            }

            _LoadDataFromFormToObj();

            if (!_ApplicationType.Save())
            {
                MessageBox.Show("Problem While Trying To Save Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            MessageBox.Show("The Data Updated Successfully");
        }
    }
}
