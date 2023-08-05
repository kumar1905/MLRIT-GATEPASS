using gatepassgenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gatepass
{
    public partial class ValidateStudentPass : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        DataSet ds;
        public ValidateStudentPass()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void ValidateStudentPass_Load(object sender, EventArgs e)
        {
            query = "select e.*,a.passId,a.validFrom,a.ValidTo from student as e inner join pass1 as a on e.student_pk=a.student_fk";
            ds = databaseOperations.getData(query);
            dataGridViewStudent.DataSource = ds.Tables[0];

            pictureBox1.BackColor = Color.LightGreen;
            pictureBox2.BackColor = Color.IndianRed;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public static bool IsDateAfterTodayorToday(string input)
        {
            DateTime pDate;
            if (!DateTime.TryParseExact(input, "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out pDate))
            {
                return false;
            }
            return DateTime.Today <= pDate;


        }


        String path;
        Int64 studentPk;
        private void dataGridViewStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                studentPk = Int64.Parse(dataGridViewStudent.Rows[e.RowIndex].Cells[0].Value.ToString());
                labelPassId.Text = dataGridViewStudent.Rows[e.RowIndex].Cells[7].Value.ToString();
                labelName.Text = dataGridViewStudent.Rows[e.RowIndex].Cells[1].Value.ToString();
                labelContact.Text = dataGridViewStudent.Rows[e.RowIndex].Cells[3].Value.ToString();
                labelGender.Text = dataGridViewStudent.Rows[e.RowIndex].Cells[6].Value.ToString();


                labelValidFrom.Text = dataGridViewStudent.Rows[e.RowIndex].Cells[11].Value.ToString();
                labelValidTo.Text = dataGridViewStudent.Rows[e.RowIndex].Cells[12].Value.ToString();

                if (IsDateAfterTodayorToday(dataGridViewStudent.Rows[e.RowIndex].Cells[12].Value.ToString()))
                {
                    panel1.BackColor = Color.LightGreen;
                }
                else
                {
                    panel1.BackColor = Color.IndianRed;
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }

        private void txtSearch2_TextChanged(object sender, EventArgs e)
        {
            query = "select e.*,a.passId,a.validFrom,a.validTo from student as e inner join pass1 as a on e.student_pk=a.student_fk where a.passId like '"
                + txtSearch2.Text + "%' or e.rollno like '" + txtSearch2.Text + "%' or e.ename like'" + txtSearch2.Text + "%'";
            ds = databaseOperations.getData(query);
            dataGridViewStudent.DataSource = ds.Tables[0];
           

        }
    }
}
