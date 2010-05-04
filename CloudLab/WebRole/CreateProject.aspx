<%@ Page Title="Create Project" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="CreateProject.aspx.cs" Inherits="WebRole.CreateProject" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">
  <h1>Create a new project</h1>
  <table class="inputForm">
    <tr>
      <td><label>Project Name</label></td>
      <td>
        <asp:textbox id="newProjectName" runat="server" />
        <asp:RequiredFieldValidator id="npRequired" runat="server" ControlToValidate="newProjectName" ErrorMessage="* Project must be given a name" Display="static">*</asp:RequiredFieldValidator>
      </td>
    </tr>
    <tr>
      <td><label>Dataset</label></td>
      <td><asp:textbox id="newDataset" runat="server" /></td>
    </tr>
  </table>
  <asp:Button runat="server" ID="CreateProjectBtn" Text="Test" PostBackUrl="~/MapControl.aspx" />
</asp:Content>