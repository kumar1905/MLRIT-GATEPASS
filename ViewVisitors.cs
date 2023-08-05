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
    public partial class ViewVisitors : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        DataSet ds;
        public ViewVisitors()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewVisitors_Load(object sender, EventArgs e)
        {
            try
            {
                query = "select * from visitors";
                ds = databaseOperations.getData(query);
                dataGridViewVisitor.DataSource = ds.Tables[0];

            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void txtSearch2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                query = "select * from visitors where vname like '" + txtSearch2.Text + "%' or visitorId like '" + txtSearch2.Text + "%'";
                ds = databaseOperations.getData(query);
                dataGridViewVisitor.DataSource = ds.Tables[0];

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
