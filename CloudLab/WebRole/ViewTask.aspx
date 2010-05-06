<%@ Page Title="View Task" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="ViewTask.aspx.cs" Inherits="WebRole.ViewTask" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">
  <asp:GridView runat="server" id="TaskProgress" DataSourceID="TaskData" 
    EnableModelValidation="True" CssClass="taskProgress" AllowPaging="True">
    <Columns>
      <asp:CommandField ShowSelectButton="True" />
    </Columns>
  </asp:GridView>
  <asp:XmlDataSource ID="TaskData" runat="server"></asp:XmlDataSource>
</asp:Content>