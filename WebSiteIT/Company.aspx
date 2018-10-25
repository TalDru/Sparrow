<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Company.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AddMenu" Runat="Server">
    <%=RenderMenu("Search")%>
    <%=RenderMenu("Company")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <form runat="server">
        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand=""></asp:SqlDataSource>
        <asp:GridView ID="GridView" CssClass="Table" runat="server" DataSourceID="SqlDataSource" EmptyDataText="Table is empty."
                    Caption="<table width='90%' style='font-size:30px; text-align:center;'><tr><td>Employees</td></tr></table>"
                    CaptionAlign="Top">
            <Columns>
                <asp:TemplateField HeaderText="Tasks For Employee">
                    <ItemTemplate>
                        <asp:LinkButton ID="Tasks_Link"  runat="server" CommandName='<%#Eval("Id")%>' OnClick="Tasks_Link_Click">Tasks</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div style="text-align:right;margin:10px;"><asp:Label ID="Bill" runat="server" Text="Current Bill: "></asp:Label></div>
        <asp:Table ID="Company_Tbl" CssClass="Tbl" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="AspBold">
                    <asp:Label ID="Label" runat="server" Text="Invite Employee"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Mail" runat="server" Text="E-Mail:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="MailText" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="Invite_Btn" CssClass="SmallButton" runat="server" Text="Invite" OnClick="ButtonInvite_Click"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="ErrorLabel" runat="server" Text="E-Mail Alredy Invited" Visible="false" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="SentLable" runat="server" Text="Inventation sent." Visible="false"></asp:Label>
                </asp:TableCell>
            </asp:Tablerow>
            <asp:TableRow>
                <asp:TableCell CssClass="AspBold">
                    <asp:Label ID="Label1" runat="server" Text="Search Human Resources"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label2" runat="server" Text="Skill needed:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="QTextBox" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="SearchButton" CssClass="SmallButton" runat="server" Text="Search" OnClick="SearchButton_Click"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="LabelAlert" runat="server" ForeColor="Red" Text="Remember: Every Search is automatically billed "/>
                    <asp:Label ID="LabelSum" runat="server" Text=""/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label3" runat="server" Text="E-Mail Alredy Invited" Visible="false" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

