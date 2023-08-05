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
    public partial class AddStudent : Form
    {
        DatabaseOperations DatabaseOperations = new DatabaseOperations();
        string query;
        DataSet ds;
        public AddStudent()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AddStudent_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String name = txtName.Text;
                String joining = txtJoin.Text;
                String Contact = txtContact.Text;
                String gender = txtGender.Text;
                String address = txtAddress.Text;
                String branch = txtBranch.Text;
                String rollno = txtRollno.Text;
                String userName = txtUsername.Text;
                String password = txtPassword.Text;
                

                if(!String.IsNullOrEmpty(name) &&
                    !String.IsNullOrEmpty(joining) && 
                    !String.IsNullOrEmpty(Contact) &&
                    !String.IsNullOrEmpty(gender) &&
                    !String.IsNullOrEmpty(address) &&
                    !String.IsNullOrEmpty(branch) &&
                    !String.IsNullOrEmpty(rollno) && 
                    !String.IsNullOrEmpty(userName) &&
                    
                    !String.IsNullOrEmpty(password))
                {
                    Int64 ContactInt = Int64.Parse(Contact);
                    query = "select * from appUser where username='" + userName + "' and uenabled=1";
                    ds = DatabaseOperations.getData(query);
                    if(ds!= null && ds.Tables[0].Rows.Count == 0)
                    {
                        query = "insert into appUser(username,upass, urole) values ('"+userName+"','"+password+"','STUDENT')";
                        DatabaseOperations.setData(query, null);

                        query = "select * from appUser where username='" + userName + "' and upass = '" + password + "' and uenabled=1";
                        ds = DatabaseOperations.getData(query);

                        query = "insert into student(ename,joindate,contact,gender,eaddress,branch,rollno,appuser_fk) values ('" + name + "','" + joining + "','" + ContactInt + "','" + gender + "','" + address + "','" + branch + "','" + rollno + "'," + ds.Tables[0].Rows[0][0] + ")";
                        DatabaseOperations.setData(query, "Student added successsfully.");
                        clearAllFields();

                    }
                    else
                    {
                        MessageBox.Show("Username already linked with another account.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }



                }
                else
                {
                    MessageBox.Show("Fields empty. Fill and try again","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception in Add Student btnSave Click : " + ex);
                MessageBox.Show("Something went wrong : "+ex,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.onlyNumber(e);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void clearAllFields()
        {
            txtName.Clear();
            txtJoin.ResetText();
            txtContact.Clear();
            txtGender.SelectedIndex = -1;
            txtAddress.Clear();
            txtBranch.Clear();
            txtRollno.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            
        }
    }
}
