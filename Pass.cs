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
    public partial class Pass : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        DataSet ds;
        public Pass()
        {
            InitializeComponent();
        }
        String path,passId,name,contact,gender,validFrom,validTo;


        Bitmap bmp;
        private long studentPk;
        private long days;

        private void Print()
        {
            bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp,new Rectangle(0,0,this.Width,this.Height));
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle r = e.PageBounds;
            e.Graphics.DrawImage(bmp, r);
        }

        public Pass(String path,String passId,String name,String contact,String gender,String validFrom,String validTo,Int64 visitorPk,Int64 days)
        {
            InitializeComponent();
            try
            {
                pictureBoxProfile.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxProfile.Image = Image.FromFile(path);

            }
            catch
            {

            }
            labelPassId.Text = passId;
            labelName.Text = name;
            labelContact.Text = contact;
            labelGender.Text = gender;
            labelValidFrom.Text = validFrom;
            labelValidTo.Text = validTo;

            setPassColor(days);
            savePassDetails(passId,validFrom,validTo,visitorPk);


            this.path = path;
            this.passId = passId;
            this.name = name;
            this.contact = contact;
            this.gender = gender;
            this.validFrom = validFrom;
            this.validTo = validTo;
        }

        public Pass(string passId, string name, string contact, string gender, string validFrom, string validTo, long studentPk, long days)
        {
            this.passId = passId;
            this.name = name;
            this.contact = contact;
            this.gender = gender;
            this.validFrom = validFrom;
            this.validTo = validTo;
            this.studentPk = studentPk;
            this.days = days;
        }

        private void setPassColor(Int64 days)
        {
            if (days == 0)
            {
                this.BackColor = Color.Gray;
            }
            else if (days <= 6)
            {
                this.BackColor = Color.Yellow;
            }
            else
            {
                this.BackColor = Color.SkyBlue;
            }
        }

        private void savePassDetails(string passId,String validFrom,String validTo,Int64 visitorPk)
        {
            try
            {
                DateTime validFromDate = DateTime.ParseExact(validFrom,"dd.MM.yyyy",CultureInfo.InvariantCulture);
                DateTime validToDate = DateTime.ParseExact(validTo,"dd.MM.yyyy",CultureInfo.InvariantCulture) ;
                query = "insert into pass (passId,validFrom,validTo,visitors_fk) values('" + passId + "','" + validFromDate.ToString("yyyy-MM-dd") + "','" + validToDate.ToString("yyyy-MM-dd") + "'," + visitorPk + ")";
                databaseOperations.setData(query, null);

            }catch(Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }


        private void Pass_Load(object sender, EventArgs e)
        {
            Print();
            printPreviewDialog1.ShowDialog();
            this.Close();

        }
    }
}
