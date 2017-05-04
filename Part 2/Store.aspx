<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="Part_2.Store" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Store</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Store</h1>
        <p>Add books to your cart below. Click "Checkout" once you're ready.</p>
        <p>
            Book:&nbsp;
            <asp:DropDownList ID="booksDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="booksDropDownList_SelectedIndexChanged"></asp:DropDownList>
        </p>
        <p>
            Quantity:&nbsp;
            <asp:DropDownList ID="quantityDropDownList" runat="server">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Button ID="addToCartButton" runat="server" Text="Add to Cart" OnClick="addToCartButton_Click" />
        </p>
        <p>
            <asp:Button ID="checkoutButton" runat="server" Text="Checkout"  Visible="false" OnClick="checkoutButton_Click" />
        </p>
    </div>
    </form>
</body>
</html>
