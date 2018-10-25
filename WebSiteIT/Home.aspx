<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AddMenu" Runat="Server">
    <%=RenderMenu()%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <h1>Welcome to  <span style="font-family:'Pacifico'">Sparrow™</span> - A Powerful Manpower management tool</h1><br /> Both for Employers and employees.
    <br />
    Here employers can give and manage thier employee's tasks, and employees can keep track of their tasks.
    <br />
    New feature - as an employer you can now contact our sister-company,
    <br />
    <b style="font-size:larger;"> <span style="font-family:'Pacifico'">Falcon™</span> Human resources</b>, to inive new personnel to your company instantly via our website!
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

