using gatepassgenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gatepass
{
    public partial class GeneratePass : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        DataSet ds;
        public GeneratePass()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            

            String passId = labelPassId.Text;
            String name = labelName.Text;
            String contact = labelContact.Text;
            String gender = labelGender.Text;
            String validFrom = labelValidFrom.Text;
            String validTo = labelValidTo.Text;

            if(!String.IsNullOrEmpty(passId) &&
                !String.IsNullOrEmpty(name) &&
                !String.IsNullOrEmpty(contact) &&
                !String.IsNullOrEmpty(gender) &&
                !String.IsNullOrEmpty(validFrom) &&
                !String.IsNullOrEmpty(validTo))
            {
                Pass p = new Pass(path,passId,name,contact,gender,validFrom,validTo,visitorPk,days);
                p.Show();
                reset();
            }
            else
            {
                MessageBox.Show("Invalid pass data. Complete selection to generate pass.","Information",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void GeneratePass_Load(object sender, EventArgs e)
        {
            query = "select * from visitors";
            ds = databaseOperations.getData(query);
            dataGridViewVisitor.DataSource = ds.Tables[0];

            pictureBox1.BackColor = Color.Gray;
            pictureBox2.BackColor = Color.Yellow;
            pictureBox3.BackColor = Color.SkyBlue;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch2_TextChanged(object sender, EventArgs e)
        {
            String searchText = txtSearch2.Text;
            query = "select * from visitors where vname like '%" + searchText + "' or uniqueId like '" + searchText + "%' or visitorId like '%"+searchText+"%'";
            ds = databaseOperations.getData(query);
            dataGridViewVisitor.DataSource= ds.Tables[0];

        }
        String passId;
        String path;
        Int64 visitorPk;
        private void dataGridViewVisitor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + "\\Images\\" +
                    dataGridViewVisitor.Rows[e.RowIndex].Cells[6].Value.ToString() + ".jpg";
                passId = Utility.getUniqueId("PID-");
                visitorPk = Int64.Parse(dataGridViewVisitor.Rows[e.RowIndex].Cells[0].Value.ToString());
                labelPassId.Text = passId;
                labelName.Text = dataGridViewVisitor.Rows[e.RowIndex].Cells[1].Value.ToString();
                labelContact.Text = dataGridViewVisitor.Rows[e.RowIndex].Cells[2].Value.ToString();
                labelGender.Text = dataGridViewVisitor.Rows[e.RowIndex].Cells[3].Value.ToString();

                if(pictureBoxProfile.Image != null)
                {
                    pictureBoxProfile.Image.Dispose();
                    pictureBoxProfile.Image = null;
                }
                pictureBoxProfile.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxProfile.Image = Image.FromFile(path);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void setPassColor(Int64 days)
        {
            if(days == 0)
            {
                panel1.BackColor = Color.Gray;
            }else if(days <= 6)
            {
                panel1.BackColor = Color.Yellow;
            }
            else
            {
                panel1.BackColor = Color.SkyBlue;
            }
        }


        private void compareDate(String input)
        {
            DateTime dtcurrent = DateTime.Now;
            DateTime inputDate = DateTime.ParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture);

            int result = DateTime.Compare(dtcurrent, inputDate);
            Console.WriteLine(result);
        }

        public static bool IsDateBeforeOrToday(string input)
        {
            DateTime pDate;
            if(!DateTime.TryParseExact(input,"dd.MM.yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None,out pDate))
            {
                return false;
            }
            return DateTime.Today <= pDate;

        }

        public static bool IsDateAfterValidFrom(string date,String dateFrom)
        {
            DateTime validTo, ValidFrom;
            if(!DateTime.TryParseExact(date,"dd.MM.yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None,out validTo))
            {
                return false;
            }
            if (!DateTime.TryParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ValidFrom))
            {
                return false;
            }
            return ValidFrom <= validTo;

        }

        private void dateTimePickerValidFrom_ValueChanged(object sender, EventArgs e)
        {
            if (IsDateBeforeOrToday(dateTimePickerValidFrom.Text))
            {
                labelValidFrom.Text = dateTimePickerValidFrom.Text;
            }
            else
            {
                MessageBox.Show("Select today date or date after today.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        
      
        private void reset()
        {
            labelPassId.ResetText();
            labelName.ResetText();
            labelContact.ResetText();
            labelGender.ResetText();
            labelValidFrom.ResetText();
            labelValidTo.ResetText();
            if(pictureBoxProfile.Image != null)
            {
                pictureBoxProfile.Image.Dispose();
                pictureBoxProfile.Image = null;
            }
        }
        Int64 days;
        private void dateTimePickerValidTo_ValueChanged_1(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(dateTimePickerValidFrom.Text))
            {
                if (IsDateAfterValidFrom(dateTimePickerValidTo.Text, labelValidFrom.Text))
                {
                    labelValidTo.Text = dateTimePickerValidTo.Text;
                    DateTime StartDate = DateTime.ParseExact(labelValidFrom.Text,"dd.MM.yyyy",CultureInfo.InvariantCulture);
                    DateTime EndDate = DateTime.ParseExact(labelValidTo.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    days = (EndDate.Date - StartDate).Days;
                    setPassColor(days);
                }
                else
                {
                    MessageBox.Show("Select date after valid from date.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("select valid from date first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
