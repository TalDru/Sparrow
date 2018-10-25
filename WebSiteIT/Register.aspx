<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AddMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <form id="form1" runat="server">
        <asp:Table ID="Register_Tbl" CssClass="Tbl" runat="server" Height="82px">

            <asp:TableRow>
            <asp:TableCell>
            <asp:Label ID="Mail" runat="server" Text="E-Mail:"></asp:Label>
            
            </asp:TableCell>
            <asp:TableCell>
            <asp:TextBox ID="MailText" runat="server"></asp:TextBox>
            
            </asp:TableCell>
            <asp:TableCell>
            <asp:RequiredFieldValidator ID="RFV_Mail" runat="server" ErrorMessage="Required Field" ControlToValidate="MailText" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="REGXV_Mail" runat="server" ErrorMessage="Invalid E-Mail" ControlToValidate="MailText" ForeColor="Red" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            <asp:Label ID="MailError" Visible="false" ForeColor="Red" runat="server" Text="E-Mail was not authorized. Please contact your employer." ></asp:Label>
            
            </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
            <asp:TableCell>
            <asp:Label ID="Pass" runat="server" Text="Password:"></asp:Label>
            
            </asp:TableCell>
            <asp:TableCell>
            <asp:TextBox ID="PasswordText" TextMode="Password" runat="server"></asp:TextBox>
            
            </asp:TableCell>
            <asp:TableCell>
            <asp:RequiredFieldValidator ID="RFV_Pass" runat="server" ErrorMessage="Required Field" ControlToValidate="PasswordText" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                
            <asp:Label ID="Label2" Visible="false" ForeColor="Red" runat="server" Text="Invalid Password" ></asp:Label>
            
            </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
            <asp:TableCell>
            <asp:Label ID="ConfPass" runat="server" Text="Confirm Password:"></asp:Label>
            
            </asp:TableCell>
            <asp:TableCell>
            <asp:TextBox ID="ConfPassText" TextMode="Password" runat="server"></asp:TextBox>
            
            </asp:TableCell>
            <asp:TableCell>
            <asp:CompareValidator ID="CV_ConfPass" runat="server" ErrorMessage="Passwords do not match" ControlToCompare="PasswordText" ForeColor="Red" Display="Dynamic" ControlToValidate="ConfPassText"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RFV_ConfPass" runat="server" ErrorMessage="Required Field" ForeColor="Red" ControlToValidate="ConfPassText"></asp:RequiredFieldValidator>
            </asp:TableCell>
            </asp:TableRow>

            



            <asp:TableRow>
                <asp:TableCell>
                
            
                </asp:TableCell>
                <asp:TableCell>
                <asp:Button ID="Register_Btn" CssClass="SmallButton" runat="server" Text="Register" OnClick="Register_Btn_Click"/>
                &nbsp
                <asp:Button ID="Clear_Btn" CssClass="SmallButton" runat="server" Text="Clear" OnClick="Clear_Btn_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

