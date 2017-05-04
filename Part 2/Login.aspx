<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Part_2.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Login</h1>
        <p>
            Username:&nbsp;
            <asp:TextBox ID="usernameTextBox" runat="server"></asp:TextBox>
        </p>
        <p>
            Password:&nbsp;
            <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="loginButton_Click" />
        </p>
        <p>
            New here?&nbsp;
            <asp:LinkButton ID="signUpLink" runat="server" Text="Sign up!" OnClick="signUpLink_Click" />
        </p>
        <p>
            <asp:Label ID="loginErrorLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
        </p>
    </div>
    </form>
</body>
</html>
