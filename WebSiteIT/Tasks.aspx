<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Tasks.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AddMenu" Runat="Server">
    <%=RenderMenu("Search")%>
    <%=RenderMenu("Company")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <form runat="server">
        <asp:XmlDataSource ID="TaskXmlDataSource" runat="server"  EnableCaching="false"></asp:XmlDataSource>
        <asp:GridView ID="TaskGridView" CssClass="Table" runat="server" DataSourceID="TaskXmlDataSource"
                    Caption="<table width='90%' style='font-size:30px; text-align:center;'><tr><td>Tasks</td></tr></table>"
                    CaptionAlign="Top">
            <Columns>
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="Delete_Link"  runat="server" OnClick="Delete_Link_Click">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Table runat="server" CssClass="Tbl">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="AddTaskLabel" runat="server" Text="Add Task: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="TaskText" runat="server" placeholder="Task Description"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="AddTask_Button" CssClass="SmallButton" runat="server" Text="Add" OnClick="AddTask_Button_Click"/>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Visible="false" Text="Task Already Exists"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

