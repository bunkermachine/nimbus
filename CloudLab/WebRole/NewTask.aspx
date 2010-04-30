<%@ Page Title="New Task" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="NewTask.aspx.cs" Inherits="WebRole.NewTask" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">

  <h1>Launch a new task</h1>
  <table class="inputForm">
    <tr>
      <td><label>Task Name</label></td>
      <td><asp:ListBox ID="datasetList" runat="server" Width="300px" /></td>
    </tr>
    <tr>
      <td><label>Dataset</label></td>
      <td><asp:ListBox ID="fileList" runat="server" Width="300px" SelectionMode="Multiple" /></td>
    </tr>
    <tr>
      <td><asp:label ID="Label6" runat="server" Width="112px">Executable</asp:label></td>
      <td><asp:FileUpload runat="server" id="exeFile" /></td>
    </tr>
  </table>
  <asp:Button runat="server" ID="LaunchTaskBtn" Text="Test" PostBackUrl="~/MapControl.aspx" Onclick="LaunchTaskBtn_Click" />

</asp:Content>