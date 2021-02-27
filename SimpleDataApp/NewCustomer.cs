using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SimpleDataApp
{
    public partial class NewCustomer : Form
    {
        private int _parsedCustomerID;
        private int _orderID;
        public NewCustomer()
        {
            InitializeComponent();
        }

        private void btnCreateAccount_Click(object sender, System.EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            using (SqlCommand sqlCommand = new SqlCommand("Sales.uspNewCustomer", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@CustomerName",SqlDbType.VarChar,40));
                sqlCommand.Parameters["@CustomerName"].Value = txtCustomerName.Text;

                sqlCommand.Parameters.Add(new SqlParameter("@CustomerID",SqlDbType.Int));
                sqlCommand.Parameters["@CustomerID"].Direction = ParameterDirection.Output;

                try
                {
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    this._parsedCustomerID = (int)sqlCommand.Parameters["@CustomerID"].Value;
                    this.txtCustomerID.Text = Convert.ToString(_parsedCustomerID);
                }
                catch
                {
                    MessageBox.Show("Customer ID has not been returned. Account could not have been created.");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnPlaceOrder_Click(object sender, System.EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString))
            using (SqlCommand sqlCommand = new SqlCommand("Sales.uspPlaceNewOrder", connection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
                sqlCommand.Parameters["@CustomerID"].Value = this._parsedCustomerID;

                sqlCommand.Parameters.Add(new SqlParameter("@OrderDate", SqlDbType.DateTime,8));
                sqlCommand.Parameters["@OrderDate"].Value = dtpOrderDate.Value;

                sqlCommand.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int));
                sqlCommand.Parameters["@Amount"].Value = numOrderAmount.Value;

                sqlCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.Char,1));
                sqlCommand.Parameters["@Status"].Value = "0";

                sqlCommand.Parameters.Add(new SqlParameter("@RC", SqlDbType.Int));
                sqlCommand.Parameters["@RC"].Direction = ParameterDirection.ReturnValue;

                try
                {
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    this._orderID = (int)sqlCommand.Parameters["@RC"].Value;
                    MessageBox.Show("Order number " + this._orderID + "has been submitted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Order cannot be placed. " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void btnAddFinish_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnAddAnotherAccount_Click(object sender, System.EventArgs e)
        {
            
        }
    }
}
