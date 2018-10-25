<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Log.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AddMenu" Runat="Server">
    <%=RenderMenu("Admin")%>
    <%=RenderMenu("Log")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <form runat="server">
        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [Log]"></asp:SqlDataSource>
        <asp:GridView ID="GridView" CssClass="Table" runat="server" DataSourceID="SqlDataSource" EmptyDataText="Log is Clear."
                    Caption="<table width='90%' style='font-size:30px; text-align:center;'><tr><td>Log</td></tr></table>"
                    CaptionAlign="Top">

        </asp:GridView>
        <asp:Button ID="Clear_Btn" CssClass="SmallButton" runat="server" Text="Clear Log (Cannot be undone!)" OnClick="Clear_Btn_Click"/>
    </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

