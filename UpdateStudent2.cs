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
    public partial class UpdateStudent2 : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        DataSet ds;
        Boolean studentAvailable = false;
        public UpdateStudent2()
        {
            InitializeComponent();
        }

        private void UpdateStudent2_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                String username = txtUsername.Text;
                query = "select e. *,a.* from student as e inner join appUser as a on e.appuser_fk = a.appuser_pk where a. username='" + username+ "'";
                ds = databaseOperations.getData(query);
                if(ds!=null && ds.Tables[0].Rows.Count!=0)
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
                    MessageBox.Show("Studen Not Found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                String name = txtName.Text;
                String contact = txtContact.Text;
                String gender = txtGender.Text;
                String address = txtAddress.Text;
                String branch = txtBranch.Text;
                String rollno = txtRollno.Text;
                String username = txtUsername.Text;

                if(studentAvailable)
                {
                    if(!string.IsNullOrEmpty(name) &&
                        !string.IsNullOrEmpty(contact) &&
                        !string.IsNullOrEmpty(gender) &&
                        !string.IsNullOrEmpty(address) &&
                        !string.IsNullOrEmpty(branch) &&
                        !string.IsNullOrEmpty(rollno) &&
                        !string.IsNullOrEmpty(username))
                    {
                        Int64 number = Int64.Parse(contact);
                        query = "Update e set e.ename='" + name + "', e.contact=" + number + ",e.gender='" + gender + "',e.eaddress='" + address + "',e.branch='" + branch + "',e.rollno='" + rollno + "'from student as e inner join appUser as a on e.appuser_fk=a.appuser_pk where a.username='" + username + "'";
                        databaseOperations.setData(query, "Student updated");
                        clearAllFields();

                    }
                    else
                    {
                        MessageBox.Show("Fields Empty.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Student not found!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
             
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAllFields();

        }


        private void clearAllFields()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtBranch.Clear();
            txtContact.Clear();
            txtGender.SelectedIndex = -1;
            txtJoin.ResetText();
            txtRollno.Clear();
            studentAvailable = false;
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.onlyNumber(e);
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            clearAllFields();
        }
    }
}
