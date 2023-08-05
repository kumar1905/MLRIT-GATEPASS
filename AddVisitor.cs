using gatepassgenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gatepass
{
    public partial class AddVisitor : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        DataSet ds;
        public AddVisitor()
        {
            InitializeComponent();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "(*.jpg;*.jpeg;*.bmp;)| *.jpg; *.jpeg; *.bmp;";
            open.FilterIndex = 1;

            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(open.CheckFileExists)
                {
                    if(!File.Exists(path))
                    {
                        System.IO.File.Copy(open.FileName, path);
                    }
                    else
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                        System.IO.File.Delete(path);
                        System.IO.File.Copy (open.FileName, path);
                    }
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = Image.FromFile(path);
                    imageUploaded = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(imageUploaded)
            {
                if (MessageBox.Show("Image wil be removed.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    pictureBox1.Image.Dispose ();
                    pictureBox1.Image = null;
                    System.IO.File.Delete(path);
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
        String visitorId;
        Boolean imageUploaded = false;
        String path; 
        private void AddVisitor_Load(object sender, EventArgs e)
        {
            visitorId = Utility.getUniqueId("MLR-");
            txtVisitor.Text = visitorId;
            path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + "\\Images\\" + visitorId+".jpg";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String name = txtName.Text;
                String contact = txtContact.Text;
                String gender = txtGender.Text;
                String address  = txtAddress.Text;
                String uniqueId = txtUnique1.Text;
                
                if(imageUploaded)
                {
                    if (!String.IsNullOrEmpty(name) &&
                        !String.IsNullOrEmpty(contact) &&
                        !String.IsNullOrEmpty(gender) &&
                        !String.IsNullOrEmpty(address) &&
                        !String.IsNullOrEmpty(uniqueId))
                    {


                        Int64 contactNum = Int64.Parse(contact);
                        query = "Insert into visitors (vname,contact,gender,vaddress,uniqueId,visitorId) values ('" + name + "'," + contact + ",'" + gender + "','" + address + "','" + uniqueId + "','" + visitorId + "')";
                        databaseOperations.setData(query, "Visitor added.");
                        clearAllFields();
                    }
                    else
                    {
                        MessageBox.Show("Fill Mandatory fields and try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("Picture not selected.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

            }catch(Exception ex)
            {
                Console.WriteLine("Exception in Add Visotor save Click" + ex);
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
            txtGender.SelectedIndex = -1;
            txtAddress.Clear();
            txtContact.Clear();
            txtUnique1.Clear();

            imageUploaded = false;
            if(pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            AddVisitor_Load(this, null);





        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAllFields();
        }

        private void txtUnique1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
