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
            using (SqlCommand sqlCommand = new SqlCommand("Sales.uspNewCustomer",connection))
            {

            }
        }

        private void btnPlaceOrder_Click(object sender, System.EventArgs e)
        {

        }

        private void btnAddFinish_Click(object sender, System.EventArgs e)
        {

        }

        private void btnAddAnotherAccount_Click(object sender, System.EventArgs e)
        {

        }
    }
}
