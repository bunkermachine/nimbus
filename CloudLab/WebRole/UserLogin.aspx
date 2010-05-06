<%@ Page Title="Login" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="WebRole.UserLogin" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">
  <div>
      <asp:Login ID="Login" runat="server" />
      <asp:CreateUserWizard ID="CreateUserWizard" runat="server"></asp:CreateUserWizard>
  </div>
</asp:Content>