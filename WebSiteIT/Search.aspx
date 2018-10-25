<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AddMenu" Runat="Server">
    <%=RenderMenu("Search")%>
    <%=RenderMenu("Company")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <form runat="server">
        <div>
        <asp:GridView ID="WorkerGridView" CssClass="Table" runat="server" OnRowCommand="Invite" EmptyDataText="No workers found. Try a different query."
                    CaptionAlign="Top"> 
            <Columns >
                <asp:ButtonField ControlStyle-CssClass="SmallButton"  ButtonType="Button" CommandName="Invite_btn" Text="Invite" />
                <asp:TemplateField HeaderText="Picture">
                    <ItemTemplate>
                        <asp:Image CssClass="AspImage" ID="ImageControl" runat="server"  Width="50px" ImageUrl='<%#"data:Image/png;base64," 
                                + Convert.ToBase64String((Byte[])((Eval("Pic")==DBNull.Value)?Encoding.ASCII.GetBytes(String.Empty):Eval("Pic")) )%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CV">
                    <ItemTemplate>
                        <asp:LinkButton CssClass="SmallButton" ID="DownloadCVLink" runat="server" CommandName='<%#Convert.ToBase64String((Byte[])((Eval("CV")==DBNull.Value)?Encoding.ASCII.GetBytes(String.Empty):Eval("CV"))) %>' OnClick="DownloadCVLink_Click">Download CV...</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <asp:Label ID="LabelSuccess" runat="server" Text="Invitation sent successfully" Visible="false"></asp:Label>
        <br />
        <asp:Button id="Back" CssClass="SmallButton" runat="server" OnClick="Back_Click" Text="Back"/>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

