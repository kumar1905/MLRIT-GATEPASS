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
using System.Xml.Linq;

namespace gatepass
{
    public partial class FilterPass : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        DataSet ds;
        public FilterPass()
        {
            InitializeComponent();
        }

        private void dataGridViewVisitor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePickerValidFrom_ValueChanged(object sender, EventArgs e)
        {
            DateTime pickedDate = DateTime.ParseExact(dateTimePicker.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            String selectedDate = pickedDate.ToString("yyyy-MM-dd");

            if (String.IsNullOrEmpty(filterType))
                MessageBox.Show("Please select the filter type first.", "Information !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if ("Valid From".Equals(filterType))
                query = "select v.*,p.passId,p.validFrom,p.validTo from visitors as v inner join pass as p on v.visitors_pk=p.visitors_fk where p.validFrom like '%" + selectedDate + "%'";
            else if ("Valid To".Equals(filterType))
                query = "select v.*,p.passId,p.validFrom,p.validTo from visitors as v inner join pass as p on v.visitors_pk=p.visitors_fk where p.validFrom like '%" + selectedDate + "%'";
            else
            {

                FilterPass_Load(this, null);

            }
            ds = databaseOperations.getData(query);
            dataGridViewVisitor.DataSource = ds.Tables[0];



        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FilterPass_Load(object sender, EventArgs e)
        {
            query = "select v.*,p.passId,p.validFrom,p.validTo from visitors as v inner join pass as p on v.visitors_pk=p.visitors_fk";
            ds = databaseOperations.getData(query);
            dataGridViewVisitor.DataSource = ds.Tables[0];
        }

        String filterType = null;
        private void txtFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterType = txtFilterType.Text;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            FilterPass_Load(this, null );
        }

        private void txtSearch2_TextChanged(object sender, EventArgs e)
        {
            query = "select v.*,p.passId,p.validFrom,p.validTo from visitors as v inner join pass as p on v.visitors_pk=p.visitors_fk where p.passId like '"
                + txtSearch2.Text + "%' or v.vname like '"+txtSearch2.Text+"%'";
            /*ds = databaseOperations.getData(query);
            dataGridViewVisitor.DataSource = ds.Tables[0];*/

            dataGridViewVisitor.DataSource = databaseOperations.getData(query).Tables[0];
        }

        private void dataGridViewVisitor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsDateAfterTodayOrToday(dataGridViewVisitor.Rows[e.RowIndex].Cells[9].Value.ToString()))
            {
                panel1.BackColor = Color.LightGreen;
                labelStatus.Text = "Valid Pass";
            }
            else
            {
                panel1.BackColor = Color.IndianRed;
                labelStatus.Text = "Pass Expired";

            }
        }

        private bool IsDateAfterTodayOrToday(string input)
        {
            DateTime pDate;
            if(!DateTime.TryParseExact(input,"dd-MM_yyyy hh:mm:ss",CultureInfo.InvariantCulture,DateTimeStyles.None,out pDate))
            {
                return false;
            }
            return DateTime.Today <= pDate;
        }
    }
}
