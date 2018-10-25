<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AddMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
<form id="form1" runat="server">
    <asp:Table ID="Login_Tbl" CssClass="Tbl" runat="server" Height="82px">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Email" runat="server" Text="Your E-Mail:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="MailText" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="RFV_Mail" runat="server" ErrorMessage="Email Required" ControlToValidate="MailText" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:Label ID="ErrorLable" Visible="false" ForeColor="Red" runat="server" Text="Email not found" ></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
       
        <asp:TableRow>
            <asp:TableCell>
                <asp:Button ID="Recover_Btn" CssClass="SmallButton" runat="server" Text="Send Recovery Mail" OnClick="Recover_Btn_Click"/>
            </asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="ConfirmLabel" Text="Recovery Email was sent to the provided adress" Visible="False" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

