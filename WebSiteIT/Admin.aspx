<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AddMenu" Runat="Server">
    <%=RenderMenu("Admin")%>
    <%=RenderMenu("Log")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <form runat="server">
    <asp:Table ID="Admin_Tbl" CssClass="Tbl" runat="server">
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand=""></asp:SqlDataSource>
                <asp:GridView ID="GridView" CssClass="Table" runat="server" DataSourceID="SqlDataSource" EmptyDataText="Table is empty."
                    Caption="<table width='90%' style='font-size:30px; text-align:center;'><tr><td>Users</td></tr></table>"
                    CaptionAlign="Top">

                </asp:GridView>
            </asp:TableCell>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="SwitchButton" CssClass="SmallButton" runat="server" Text="View Employees" OnClick="SwitchButton_Click" />
                &nbsp<asp:Label ID="LabelLog1" runat="server" Text="     Last Log Entry:"></asp:Label><asp:Label ID="LabelLog2" runat="server" Text=""></asp:Label>
            </asp:TableCell>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="LabelCost" CssClass="AspBold" runat="server" Text="Search Cost:"></asp:Label>
                    &nbsp
                    <asp:TextBox ID="CostTextBox" runat="server"></asp:TextBox>&nbsp$
                    <asp:Button ID="UpdateButton" CssClass="SmallButton" runat="server" Text="Update Cost" OnClick="UpdateButton_Click"/>
                </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
    
</asp:Content>

