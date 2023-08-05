using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gatepassgenerator
{
    internal class DatabaseOperations
    {
        protected SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source =KUMARVENKAT\\SQLEXPRESS02;database=gatepass;integrated security=True";
            return con;
        }

        public DataSet getData(String query)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = getConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in getData : " + ex);
            }
            return ds;
        }

        public void setData(String query, String msg)
        {
            try
            {
                SqlConnection con = getConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                con.Close();
                if (msg != null)
                    MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        internal static DataSet setData(string query)
        {
            throw new NotImplementedException();
        }
    }
}
