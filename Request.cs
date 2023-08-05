using gatepassgenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace gatepass
{
    public partial class Request : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        DataSet ds;
        public Request()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String name = txtName.Text;
                String branch = txtBranch.Text;
                String rollno = txtRollno.Text;
                String gender = txtGender.Text;
                String reason = txtReason.Text;
                String contact= txtContact.Text;
                

                if (!String.IsNullOrEmpty(name) &&
                    !String.IsNullOrEmpty(branch) &&
                    !String.IsNullOrEmpty(rollno) &&
                    !String.IsNullOrEmpty(gender) &&
                    !String.IsNullOrEmpty(reason) &&
                    !String.IsNullOrEmpty(contact))
                   
                {
                    Int64 contactNum = Int64.Parse(contact);
                    query = "Insert into student2 (ename,contact,gender,reason,branch,rollno) values ('" + name + "'," + contact + ",'" + gender + "','" + reason + "','" + branch + "','"+rollno+"')";
                    databaseOperations.setData(query, "Pass Requested Successfully.");
                    clearAllFields();
                }
                else
                {
                    MessageBox.Show("Fields empty. Fill and try again", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
             }
            catch(Exception ex)
            {
                Console.WriteLine("Exception in Request btnSave Click : " + ex);
                MessageBox.Show("Something went wrong : " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.onlyNumber(e);
        }
        private void clearAllFields()
        {
            txtName.Clear();
            txtBranch.Clear();
            txtContact.Clear();
            txtGender.SelectedIndex = -1;
            txtReason.Clear();
            txtRollno.Clear();
        }
    }
}
