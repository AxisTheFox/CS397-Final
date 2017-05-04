<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Part_2.Signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Signup</h1>
        <p>
            *Username:&nbsp;
            <asp:TextBox ID="usernameTextBox" runat="server"></asp:TextBox>
        </p>
        <p>
            *Password:&nbsp;
            <asp:TextBox ID="passwordTextBox" runat="server"></asp:TextBox>
        </p>
        <p>
            *Confirm Password:&nbsp;
            <asp:TextBox ID="confirmPasswordTextBox" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="signupButton" runat="server" Text="Signup" OnClick="signupButton_Click" />
        </p>
        <i>*Indicates a required field</i>
        <p>
            <asp:Label ID="signupErrorLabel" runat="server" ForeColor="Red"></asp:Label>
        </p>
    </div>
    </form>
</body>
</html>
