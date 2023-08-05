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
    public partial class DeleteStudent : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        DataSet ds;
        Boolean studentAvailable = false;
        public DeleteStudent()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                String username = txtUsername.Text;
                query = "select e.*,a.* from student as e inner join appUser as a on e.appuser_fk=a.appuser_pk where a.username='" + username + "'";
                ds = databaseOperations.getData(query);
                if(ds!= null && ds.Tables[0].Rows.Count != 0) 
                {
                    studentAvailable = true;
                    txtName.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtJoin.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtContact.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtGender.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0][5].ToString();
                    txtBranch.Text = ds.Tables[0].Rows[0][6].ToString();
                    txtRollno.Text = ds.Tables[0].Rows[0][7].ToString();

                }
                else
                {
                    studentAvailable = false;
                    MessageBox.Show("Student not found.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }

            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure will u close?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void clearAllFields()
        {
            txtAddress.Clear();
            txtBranch.Clear();
            txtContact.Clear();
            txtGender.SelectedIndex = -1;
            txtJoin.ResetText();
            txtRollno.Clear();
            txtName.Clear();
            studentAvailable = false;

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAllFields();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
               if(studentAvailable)
                {
                    query = "delete from student from appUser where appuser_fk=appuser_pk and username='" + txtUsername.Text + "'";
                    databaseOperations.setData(query, "Student deleted");
                    query = "delete from appUser where username = '" + txtUsername.Text + "'";
                    databaseOperations.setData(query,null);
                    clearAllFields();

                }
               else
                {
                    studentAvailable = false;
                    MessageBox.Show("Student not found.","Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            clearAllFields();
        }
    }
}
