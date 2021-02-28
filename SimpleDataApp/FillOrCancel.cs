using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SimpleDataApp.Properties;

namespace SimpleDataApp
{
    public partial class FillOrCancel : Form
    {
        private int _parsedOrderID;
        public FillOrCancel()
        {
            InitializeComponent();
        }

        private void btnFindByOrderID_Click(object sender, System.EventArgs e)
        {
            const string sql = "SELECT * FROM Sales.Orders WHERE orderID = @orderID";

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
            {
                sqlCommand.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
                sqlCommand.Parameters["@orderID"].Value = _parsedOrderID;

                try
                {
                    connection.Open();
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(dataReader);
                        this.dgvCustomerOrders.DataSource = dataTable;
                        dataReader.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("The requested order could not be loaded into the form.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnCancelOrder_Click(object sender, System.EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            using(SqlCommand sqlCommand = new SqlCommand("Sales.uspCancelOrder",connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@orderID", SqlDbType.Int));
                sqlCommand.Parameters["@orderID"].Value = _parsedOrderID;

                try
                {
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("The cancel operation was not completed.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
