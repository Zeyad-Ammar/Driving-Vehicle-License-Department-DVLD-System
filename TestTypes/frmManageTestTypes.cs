using BusinessLayer;
using DVLD_Project.TestTypes;
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
    public partial class frmManageTestTypes : Form
    {
        private DataTable _TestTypes=clsTestType.GetAllTestTypes();
        public frmManageTestTypes()
        {
            InitializeComponent();
        }


        private void _RefreshDataGridView()
        {
            _TestTypes = clsTestType.GetAllTestTypes();
            dataGridView1.DataSource = _TestTypes;
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[0].Width = 80;
                dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].Width = 350;
                dataGridView1.Columns[3].Width = 80;
            }


            lbRecordsValue.Text = dataGridView1.RowCount.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshDataGridView();

        }

        private void cmsiEditTestType_Click(object sender, EventArgs e)
        {
            int TestTypeID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            frmEditTestType EditTestTypeForm = new frmEditTestType(TestTypeID);
            EditTestTypeForm.ShowDialog();
            _RefreshDataGridView();
        }
    }
}
