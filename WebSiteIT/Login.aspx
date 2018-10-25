<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AddMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <form id="form1" runat="server">
    <asp:Table ID="Login_Tbl" CssClass="Tbl" runat="server" Height="82px">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="ID" runat="server" Text="ID:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="IDText" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="RFV_ID" runat="server" ErrorMessage="ID Required" ControlToValidate="IDText" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:Label ID="ErrorLable" Visible="false" ForeColor="Red" runat="server" Text="Invalid ID or Password" ></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow> 
            <asp:TableCell>
                <asp:Label ID="Password" runat="server" Text="Password:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="PasswordText" TextMode="Password" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="RFV_Password" runat="server" ErrorMessage="Password Required" ControlToValidate="PasswordText" ForeColor="Red"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button CssClass="SmallButton" ID="Login_Btn" runat="server" Text="Log In" OnClick="Login_Btn_Click"/>
                <asp:Button CssClass="SmallButton" ID="Clear_Btn" runat="server" Text="Clear" OnClick="Clear_Btn_Click" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:HyperLink ID="ForgotPassLink" runat="server" NavigateUrl="~/PasswordRecovery.aspx">Forgot Your ID or Password? </asp:HyperLink>
            </asp:TableCell>
            <asp:TableCell>
                <asp:HyperLink ID="RegisterLink" runat="server" NavigateUrl="~/Register.aspx">Don't have an account? </asp:HyperLink>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

