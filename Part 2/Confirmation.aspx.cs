using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Part_2
{
    public partial class Confirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            clearUserCart();
        }

        private void clearUserCart()
        {
            OleDbConnection databaseConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Store"].ConnectionString);
            string databaseQuery = "UPDATE Carts SET BookIds='', BookQuantities='' WHERE CartId=@cartId";
            OleDbCommand databaseCommand = new OleDbCommand(databaseQuery, databaseConnection);
            databaseCommand.Parameters.AddWithValue("@cartId", Session["cartId"].ToString());
            databaseConnection.Open();
            databaseCommand.ExecuteNonQuery();
            databaseConnection.Close();
        }
    }
}