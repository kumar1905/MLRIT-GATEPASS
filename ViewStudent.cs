using gatepassgenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gatepass
{
    public partial class ViewStudent : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        DataSet ds;
        public ViewStudent()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ViewStudent_Load(object sender, EventArgs e)
        {
            try
            {
                query = "select * from student";
                ds = databaseOperations.getData(query);
                dataGridView1.DataSource = ds.Tables[0];

            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                query = "Select * from student where ename like '"+txtSearch.Text+"%'";
                ds = databaseOperations.getData(query);
                dataGridView1.DataSource = ds.Tables[0];
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
