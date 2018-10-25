<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Personal.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AddMenu" Runat="Server">
    <%=RenderMenu("Personal")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <form runat="server">
        <div style="display:table;width:75%;padding-left:25%;">
        <div style="display: table-row;margin:0 auto;">
            <div style="display: table-cell;padding:10px;">
        <asp:Table ID="Table1" runat="server" >
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Name" runat="server" Text="Full Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="NameText" runat="server"></asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Gender" runat="server" Text="Gender:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID ="GenderText" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Dob" runat="server" Text="Date of Birth:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="DobText" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Doh" runat="server" Text="Hiring Date:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="DohText" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Position" runat="server" Text="Job Description:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="PosText" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Phone" runat="server" Text="Phone Number:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="PhoneText" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Mail" runat="server" Text="E-Mail:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="MailText" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Pic" runat="server" Text="Picture:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Image ID="Picture" CssClass="AspImage" runat="server" Width="100px"/>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="Update_Btn" CssClass="SmallButton" runat="server" Text="Update" OnClick="Update_Btn_Click" />
                </asp:TableCell>
            </asp:TableRow>



        </asp:Table>

            </div>
            <div style="display: table-cell;padding:10px;">
                        <asp:XmlDataSource ID="TaskXmlDataSource" runat="server"  EnableCaching="false"></asp:XmlDataSource>
                        <asp:GridView ID="TaskGridView" runat="server" DataSourceID="TaskXmlDataSource"
                            Caption="<table width='90%' style='font-size:20px; text-align:center;'><tr><td>Tasks</td></tr></table>"
                            CaptionAlign="Top">
                            <Columns>
                                <asp:TemplateField >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Delete_Link"  runat="server" OnClick="Delete_Link_Click">Complete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
            
            <br />
            <asp:Button ID="In_Btn" CssClass="SmallButton" runat="server" Text="Start Shift" OnClick="Shift_Btn_Click" CommandName="I"/>
            &nbsp
            <asp:Button ID="Out_Btn" CssClass="SmallButton" runat="server" Text="End Shift" OnClick="Shift_Btn_Click" CommandName="O"/>
        </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

