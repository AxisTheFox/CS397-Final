using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Part_2
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
                Response.Redirect("Store.aspx");
        }

        protected void signupButton_Click(object sender, EventArgs e)
        {
            if (anyFieldsAreEmpty())
                showError("Fill in all the required fields to sign up.");
            else if (passwordFieldsDoNotMatch())
                showError("The two passwords you entered do not match.");
            else if (usernameAlreadyExists())
                showError("That username is already taken.");
            else
                createAccount();
        }

        private bool anyFieldsAreEmpty()
        {
            return usernameTextBox.Text.Equals("") || passwordTextBox.Text.Equals("") || confirmPasswordTextBox.Text.Equals("");
        }

        private bool passwordFieldsDoNotMatch()
        {
            return !passwordTextBox.Text.Equals(confirmPasswordTextBox.Text);
        }

        private bool usernameAlreadyExists()
        {
            DataTable usernameTable = getUsernameTable();
            return usernameExistsIn(usernameTable);
        }

        private DataTable getUsernameTable()
        {
            string databaseQuery = "SELECT [Username] FROM Users";
            OleDbConnection databaseConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            OleDbCommand databaseCommand = new OleDbCommand(databaseQuery, databaseConnection);
            OleDbDataAdapter databaseAdapter = new OleDbDataAdapter(databaseCommand);
            DataTable databaseTable = new DataTable();
            databaseConnection.Open();
            databaseAdapter.Fill(databaseTable);
            databaseConnection.Close();
            return databaseTable;
        }

        private bool usernameExistsIn(DataTable usernameTable)
        {
            foreach (DataRow user in usernameTable.Rows)
            {
                if (user["Username"].Equals(usernameTextBox.Text))
                    return true;
            }
            return false;
        }

        private void createAccount()
        {
            string hashedPassword = hashUserPassword();
            string enteredUsername = usernameTextBox.Text;
            createNewUserWith(enteredUsername, hashedPassword);
            Response.Redirect("Login.aspx");
        }

        private static void createNewUserWith(string enteredUsername, string hashedPassword)
        {
            string databaseQuery = "INSERT INTO Users([Username], [Password]) VALUES(@u, @p)";
            OleDbConnection databaseConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            OleDbCommand databaseCommand = new OleDbCommand(databaseQuery, databaseConnection);
            databaseCommand.Parameters.AddWithValue("@u", enteredUsername);
            databaseCommand.Parameters.AddWithValue("@p", hashedPassword);
            databaseConnection.Open();
            databaseCommand.ExecuteNonQuery();
            databaseConnection.Close();
        }

        private string hashUserPassword()
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            sha1.ComputeHash(Encoding.ASCII.GetBytes(passwordTextBox.Text));
            byte[] hashResult = sha1.Hash;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashResult.Length; i++)
            {
                stringBuilder.Append(hashResult[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        private void showError(string errorMessage)
        {
            signupErrorLabel.Text = errorMessage;
        }
    }
}