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
    public partial class frmManageApplicationTypes : Form
    {
        private DataTable _ApplicationTypes=clsApplicationType.GetAllApplicationTypes();

        public frmManageApplicationTypes()
        {
            InitializeComponent();

        }

        private void _RefreshDataGridView()
        {
            _ApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dataGridView1.DataSource = _ApplicationTypes;
            if(dataGridView1.Columns.Count>0)
            {
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 350;
                dataGridView1.Columns[2].Width = 100;
            }
           

            lbRecordsValue.Text=dataGridView1.RowCount.ToString();


        }
        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshDataGridView();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmsiEditApplicationType_Click(object sender, EventArgs e)
        {
            int ApplicationTypeID = -1;
            ApplicationTypeID=Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            frmEditApplicationType editApplicationTypeForm=new frmEditApplicationType(ApplicationTypeID);
            editApplicationTypeForm.ShowDialog();
            _RefreshDataGridView();

        }
    }
}
