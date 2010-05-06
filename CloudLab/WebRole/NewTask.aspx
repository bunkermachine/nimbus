<%@ Page Title="New Task" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="NewTask.aspx.cs" Inherits="WebRole.NewTask" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">

  <h1>Launch a new task</h1>
  <table class="inputForm">
    <tr>
      <td><label>Task Name</label></td>
      <td><asp:Textbox ID="TaskNameText" runat="server" /></td>
    </tr>
    <tr>
      <td><label>Dataset</label></td>
      <td><asp:ListBox ID="DatasetList" runat="server" SelectionMode="Multiple" /></td>
    </tr>
    <tr>
      <td><label>Files</label></td>
      <asp:UpdatePanel runat="server" ID="Files">
        <ContentTemplate>
          <td><asp:ListBox ID="FileList" runat="server" SelectionMode="Multiple" /></td>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="DatasetList" />
        </Triggers>
      </asp:UpdatePanel>
    </tr>
    <tr>
      <td><label>Executable</label></td>
      <td><asp:FileUpload runat="server" id="exeFile" /></td>
    </tr>
  </table>
  <asp:Button runat="server" ID="LaunchTaskBtn" Text="Test" Onclick="LaunchTaskBtn_Click" />

</asp:Content>