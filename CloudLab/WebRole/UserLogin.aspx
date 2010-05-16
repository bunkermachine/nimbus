<%@ Page Title="Login" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="WebRole.UserLogin" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">
  <div id="UserLogin">
    <div id="LoginForm">
      <h1>Sign into your account</h1>
      <asp:Login ID="Login" runat="server" TitleText="" />
    </div>
    <div id="CreateUserForm">
      <h1>Create a new account</h1>
      <asp:CreateUserWizard ID="CreateUserWizard" runat="server" TitleText="" />
    </div>
  </div>
</asp:Content>