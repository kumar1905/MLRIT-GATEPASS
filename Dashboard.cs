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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        String role;
        public Dashboard(String role)
        {
            InitializeComponent();
            this.role = role;

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AddStudent>().Count() == 1)
            {
                Application.OpenForms.OfType<AddStudent>().First().BringToFront();
            }
            else
            {
                AddStudent addStudent = new AddStudent();
                addStudent.Show();
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            String backgroundName;
            if ("ADMIN".Equals(role))
            {
                StudentToolStripMenuItem1.Visible = true;
                requestPassToolStripMenuItem.Visible = false;
                backgroundName = "gatePassBg1";
                labelWelcomeText3.Text = "Admin Dashboard";

            }
            else
            {
                StudentToolStripMenuItem1.Visible = false;
                toolStripMenuItem17.Visible = false;
                generatePasstoolStripMenuItem18.Visible = false;
                toolStripMenuItem19.Visible = false;
                toolStripMenuItem20.Visible = false;
                toolStripMenuItem18.Visible = false;
                backgroundName = "gatePassBg2";
                labelWelcomeText3.Text = "Student Dashboard";
            }
            Image image = Image.FromFile(Utility.getImageStorePath(backgroundName));
            this.BackgroundImage = image;
            this.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to logout.?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit..?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }


        }

        private void UpdatetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<UpdateStudent2>().Count() == 1)
            {
                Application.OpenForms.OfType<UpdateStudent2>().First().BringToFront();


            }
            else
            {
                UpdateStudent2 updatestudent = new UpdateStudent2();
                updatestudent.Show();

            }
        }

        private void viewStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ViewStudent>().Count() == 1)
            {
                Application.OpenForms.OfType<ViewStudent>().First().BringToFront();
            }
            else
            {
                ViewStudent viewstudent = new ViewStudent();
                viewstudent.Show();
            }

        }

        private void deleteStudenttoolStripMenuItem26_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<DeleteStudent>().Count() == 1)
            {
                Application.OpenForms.OfType<DeleteStudent>().First().BringToFront();
            }
            else
            {
                DeleteStudent deletestudent = new DeleteStudent();
                deletestudent.Show();
            }
        }

        private void AddVisitortoolStripMenuItem27_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AddVisitor>().Count() == 1)
            {
                Application.OpenForms.OfType<AddVisitor>().First().BringToFront();
            }
            else
            {
                AddVisitor addVisitor = new AddVisitor();
                addVisitor.Show();
            }
        }

        private void UpdateVisitortoolStripMenuItem29_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<UpdateVisitor>().Count() == 1)
            {
                Application.OpenForms.OfType<UpdateVisitor>().First().BringToFront();
            }
            else
            {
                UpdateVisitor updateVisitor = new UpdateVisitor();
                updateVisitor.Show();
            }
        }

        private void viewVisitortoolStripMenuItem28_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ViewVisitors>().Count() == 1)
            {
                Application.OpenForms.OfType<ViewVisitors>().First().BringToFront();
            }
            else
            {
                ViewVisitors viewVisitors = new ViewVisitors();
                viewVisitors.Show();
            }
        }

        private void generatePasstoolStripMenuItem18_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<GeneratePass>().Count() == 1)
            {
                Application.OpenForms.OfType<GeneratePass>().First().BringToFront();
            }
            else
            {
                GeneratePass generatePass = new GeneratePass();
                generatePass.Show();
            }
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ValidatePass>().Count() == 1)
            {
                Application.OpenForms.OfType<ValidatePass>().First().BringToFront();
            }
            else
            {
                ValidatePass validatePass = new ValidatePass();
                validatePass.Show();
            }
        }

        private void validPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ValidateStudentPass>().Count() == 1)
            {
                Application.OpenForms.OfType<ValidateStudentPass>().First().BringToFront();
            }
            else
            {
                ValidateStudentPass validateStudentPass = new ValidateStudentPass();
                validateStudentPass.Show();
            }
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FilterPass>().Count() == 1)
            {
                Application.OpenForms.OfType<FilterPass>().First().BringToFront();
            }
            else
            {
                FilterPass filterPass = new FilterPass();
                filterPass.Show();
            }
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<GeneratePass1>().Count() == 1)
            {
                Application.OpenForms.OfType<GeneratePass1>().First().BringToFront();
            }
            else
            {
                GeneratePass1 generatePass1 = new GeneratePass1();
                generatePass1.Show();
            }
        }

        private void requestPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Application.OpenForms.OfType<Request>().Count() == 1)
            {
                Application.OpenForms.OfType<Request>().First().BringToFront();
            }
            else
            {
                Request request = new Request();    
                request.Show();
            }
        }

        private void requestedPassesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Application.OpenForms.OfType<RequestedPasses>().Count() == 1)
            {
                Application.OpenForms.OfType<RequestedPasses>().First().BringToFront();
            }
            else
            {
                RequestedPasses requestedPasses = new RequestedPasses();
                requestedPasses.Show();
            }
        }
    }
}

