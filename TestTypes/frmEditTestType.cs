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

namespace DVLD_Project.TestTypes
{
    public partial class frmEditTestType : Form
    {
        private clsTestType _TestType=new clsTestType();
        public frmEditTestType(int TestTypeID)
        {
            InitializeComponent();
            _TestType=clsTestType.GetTestType(TestTypeID);
        }

        private void _LoadDataFromObjToForm()
        {
            lbIDValue.Text=_TestType.TestTypeID.ToString();
            lbTitleValue.Text=_TestType.TestTypeTitle.ToString();
            tbFees.Text=_TestType.TestTypeFees.ToString();  
            rtbDescription.Text=_TestType.TestTypeDescription.ToString();

        }

        private void _LoadDataFromFormToObj()
        {
            _TestType.TestTypeFees=Convert.ToDecimal(tbFees.Text);
            _TestType.TestTypeDescription=rtbDescription.Text;

        }

        private bool _CheckValidation()
        {
            if (string.IsNullOrEmpty(tbFees.Text) || !clsValidation.IsNumber(tbFees.Text))
            {
                errorProvider1.SetError(tbFees, "The fees is required and should be Number");
                return false;
            }
            else
            {
                errorProvider1.SetError(tbFees, "");

            }

            


            return true;
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _LoadDataFromObjToForm();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_CheckValidation())
            {
                MessageBox.Show("There is Wrong in Inputs Put the mouse on red icon to know the error");
                return;
            }

            _LoadDataFromFormToObj();

           if(_TestType.Save())
           {
                MessageBox.Show("The Data Updated Successfully");
            }

           


        }
    }
}
