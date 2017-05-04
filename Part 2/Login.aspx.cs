using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Part_2
{
    public partial class Login : System.Web.UI.Page
    {
        int userId;
        string hashedPasswordFromDatabase;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
                Response.Redirect("Store.aspx");
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            if (anyFieldsAreEmpty())
                showError("Username and password are required to login.");
            else
                attemptLogin();
        }

        protected void signUpLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("Signup.aspx");
        }

        private bool anyFieldsAreEmpty()
        {
            return usernameTextBox.Text.Equals("") || passwordTextBox.Text.Equals("");
        }

        private void attemptLogin()
        {
            executeLoginDatabaseQuery();
            string hashedPasswordFromUser = getHashedPassword();
            if (hashedPasswordFromUser.Equals(hashedPasswordFromDatabase))
            {
                Session["userId"] = userId;
                Response.Redirect("Store.aspx");
            }
            else
            {
                loginErrorLabel.Text = "You've entered an invalid username or password.";
            }
        }

        private void executeLoginDatabaseQuery()
        {
            string databaseQuery = "Select [UserId], [Password] from Users where [Username]=@u";
            OleDbConnection databaseConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            OleDbCommand databaseCommand = new OleDbCommand(databaseQuery, databaseConnection);
            databaseCommand.Parameters.AddWithValue("@u", usernameTextBox.Text);
            databaseConnection.Open();
            OleDbDataReader dataReader = databaseCommand.ExecuteReader();
            readDataWith(dataReader);
            dataReader.Close();
            databaseConnection.Close();
        }

        private void readDataWith(OleDbDataReader dataReader)
        {
            while (dataReader.Read())
            {
                userId = Int32.Parse(dataReader["UserId"].ToString());
                hashedPasswordFromDatabase = dataReader["Password"].ToString();
            }
        }

        private string getHashedPassword()
        {
            SHA1CryptoServiceProvider sha1Provider = new SHA1CryptoServiceProvider();
            string passwordFromUser = passwordTextBox.Text;
            sha1Provider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(passwordFromUser));
            byte[] result = sha1Provider.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        private void showError(string errorMessage)
        {
            loginErrorLabel.Text = errorMessage;
        }
    }
}