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
    public partial class Store : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
                Response.Redirect("Login.aspx");
            if (!IsPostBack)
            {
                runCartQuery();
                if ((bool)Session["isFirstBook"])
                    addAllBooksToDropDownList();
                else
                {
                    AddRemainingBooksToDropDownList();
                    checkoutButton.Visible = true;
                }
            }
            
        }

        private void addAllBooksToDropDownList()
        {
            string databaseQuery = "SELECT BookId, Title FROM Books";
            OleDbConnection databaseConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            OleDbCommand databaseComman = new OleDbCommand(databaseQuery, databaseConnection);
            booksDropDownList.Items.Add("");
            databaseConnection.Open();
            OleDbDataReader booksDataReader = databaseComman.ExecuteReader();
            while (booksDataReader.Read())
            {
                ListItem bookListItem = new ListItem(booksDataReader["Title"].ToString(), booksDataReader["BookId"].ToString());
                booksDropDownList.Items.Add(bookListItem);
            }
            booksDataReader.Close();
            databaseConnection.Close();
        }

        private void AddRemainingBooksToDropDownList()
        {
            string booksQuery = "SELECT BookId, Title FROM Books";
            OleDbConnection databaseConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            OleDbCommand databaseCommand = new OleDbCommand(booksQuery, databaseConnection);
            booksDropDownList.Items.Add("");
            string[] bookIdsInCart = ViewState["bookIds"].ToString().Split(';');
            bookIdsInCart = bookIdsInCart.Take(bookIdsInCart.Count() - 1).ToArray();
            string[] bookQuantitiesInCart = ViewState["bookQuantities"].ToString().Split(';');
            bookQuantitiesInCart = bookQuantitiesInCart.Take(bookQuantitiesInCart.Count() - 1).ToArray();
            databaseConnection.Open();
            OleDbDataReader booksDataReader = databaseCommand.ExecuteReader();
            while (booksDataReader.Read())
            {
                if (!bookIdsInCart.Contains(booksDataReader["BookId"].ToString()))
                {
                    ListItem bookListItem = new ListItem(booksDataReader["Title"].ToString(), booksDataReader["BookId"].ToString());
                    booksDropDownList.Items.Add(bookListItem);
                }
            }
            booksDataReader.Close();
            databaseConnection.Close();
        }

        private void runCartQuery()
        {
            ViewState["bookIds"] = "";
            ViewState["bookQuantities"] = "";
            Session["isFirstBook"] = true;
            Session["userHasCart"] = false;
            OleDbConnection databaseConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string databaseQuery = "SELECT CartId, BookIds, BookQuantities FROM Carts WHERE UserId=@id";
            OleDbCommand databaseCommand = new OleDbCommand(databaseQuery, databaseConnection);
            databaseCommand.Parameters.AddWithValue("@id", Session["userId"].ToString());
            databaseConnection.Open();
            OleDbDataReader dataReader = databaseCommand.ExecuteReader();
            while (dataReader.Read())
            {
                Session["userHasCart"] = true;
                if (!dataReader["BookIds"].ToString().Trim().Equals(""))
                {
                    Session["isFirstBook"] = false;
                }
                Session["cartId"] = dataReader["CartId"].ToString();
                ViewState["itemIds"] = dataReader["BookIds"].ToString().Trim();
                ViewState["quantities"] = dataReader["BookQuantities"].ToString().Trim();
            }
            dataReader.Close();
            databaseConnection.Close();
        }

        protected void booksDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            quantityDropDownList.Items.Clear();
            if (!booksDropDownList.SelectedValue.ToString().Equals(""))
            {
                reloadQuantitiesDropDownList();
            }
            else
            {
                quantityDropDownList.Items.Clear();
            }
        }

        private void reloadQuantitiesDropDownList()
        {
            OleDbConnection databaseConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["bookstore"].ConnectionString);
            string databaseQuery = "SELECT NumberInStock FROM Books WHERE BookId=@id";
            OleDbCommand databaseCommand = new OleDbCommand(databaseQuery, databaseConnection);
            databaseCommand.Parameters.AddWithValue("@id", booksDropDownList.SelectedValue.ToString());
            databaseConnection.Open();
            OleDbDataReader dataReader = databaseCommand.ExecuteReader();
            while (dataReader.Read())
            {
                int stock = Int32.Parse(dataReader["NumberInStock"].ToString());
                for (int i = 1; i <= stock; i++)
                {
                    ListItem quantityListItem = new ListItem(i.ToString(), i.ToString());
                    quantityDropDownList.Items.Add(quantityListItem);
                }
            }
            dataReader.Close();
            databaseConnection.Close();
        }

        protected void addToCartButton_Click(object sender, EventArgs e)
        {

        }

        protected void checkoutButton_Click(object sender, EventArgs e)
        {

        }
    }
}