<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AddMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <form runat="server">
        <asp:Table ID="AddPersonTable" runat="server" CssClass="Tbl">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="NameLabel" runat="server" Text="Name:"></asp:Label>
                    <br />
                    <asp:TextBox ID="NameText" runat="server"></asp:TextBox>
                    
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="GenderLabel" runat="server" Text="Gender:"></asp:Label>
                    <br />
                    <asp:DropDownList ID="GenderDropDownList" runat="server">
                        <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>  
                <asp:TableCell>
                    <asp:Label ID="PicLabel" runat="server" Text="Picture:"></asp:Label>
                    <br />
                    <asp:FileUpload ID="PictureFileUpload" runat="server" />
                </asp:TableCell>
                            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="SkillLabel" runat="server" Text="Skills:"></asp:Label>
                    <br />
                    <asp:TextBox ID="SkillsText" runat="server" placeholder="Skill:Years,Skill:Years,..."></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="MailLabel" runat="server" Text="E-mail:"></asp:Label>
                    <br />
                    <asp:TextBox ID="MailText" runat="server"></asp:TextBox>
                    
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="CVLabel" runat="server" Text="CV File (.pdf Only!):"></asp:Label>
                    <br />
                    <asp:FileUpload ID="CVFileUpload" runat="server" />
                </asp:TableCell>
                <asp:TableCell>
                    <br />
                    <asp:Button ID="AddButton" CssClass="SmallButton" runat="server" Text="Add" OnClick="AddButton_Click"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:SqlDataSource ID="WorkersSqlDataSource" EnableCaching="false" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [Workers]"></asp:SqlDataSource>
        <asp:GridView ID="WorkersGridView" CssClass="Table" runat="server" OnRowCommand="Delete" DataSourceID="WorkersSqlDataSource"
                    Caption="<table width='90%' style='font-size:30px; text-align:center;'><tr><td>Workforce</td></tr></table>"
                    CaptionAlign="Top">
            <Columns>
                <asp:ButtonField ControlStyle-CssClass="SmallButton" ItemStyle-Width="50px"  ButtonType="Button" CommandName="Delete_btn" Text="Delete" />
            </Columns>
        </asp:GridView>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

