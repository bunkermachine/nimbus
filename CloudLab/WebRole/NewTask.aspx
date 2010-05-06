 <%@ Page Title="New Task" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="NewTask.aspx.cs" Inherits="WebRole.NewTask" %>
<asp:Content id="NewTaskControl" ContentPlaceHolderID="Content" runat="server">
  <div id="NewTask">
    <h1>Launch a new task</h1>
    <table class="inputForm">
      <tr>
        <td><label>Task Name</label></td>
        <td><asp:Textbox runat="server" ID="TaskNameText" /></td>
      </tr>
      <tr>
        <td><label>Dataset</label></td>
        <td><asp:ListBox runat="server" ID="DatasetList" SelectionMode="Multiple" OnSelectedIndexChanged="PopulateFileList" AutoPostBack="true" /></td>
      </tr>
      <tr>
        <td><label>Year</label></td>
        <td><asp:Textbox runat="server" ID="YearText" OnTextChanged="PopulateFileList" AutoPostBack="true" /></td>
      </tr>
      <tr>
        <td><label>Day</label></td>
        <td><asp:TextBox runat="server" ID="DayText" OnTextChanged="PopulateFileList" AutoPostBack="true" /></td>
      </tr>
      <tr>
        <td><label>Available Files</label></td>
        <td>
          <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <asp:ListBox runat="server" Rows="10" ID="FileList" SelectionMode="Multiple" OnSelectedIndexChanged="AddFile" AutoPostBack="true" />
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="DatasetList" />
              <asp:AsyncPostBackTrigger ControlID="YearText" />
              <asp:AsyncPostBackTrigger ControlID="DayText" />
              <asp:AsyncPostBackTrigger ControlID="SelectedFileList" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td><label>Selected Files</label></td>
        <td>
          <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <asp:ListBox runat="server" Rows="10" ID="SelectedFileList" SelectionMode="Multiple" OnSelectedIndexChanged="RemoveFile" AutoPostBack="true" />
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="DatasetList" />
              <asp:AsyncPostBackTrigger ControlID="YearText" />
              <asp:AsyncPostBackTrigger ControlID="DayText" />
              <asp:AsyncPostBackTrigger ControlID="FileList" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td><label>Executable</label></td>
        <td><asp:FileUpload runat="server" id="exeFile" /></td>
      </tr>
    </table>
    <asp:Button runat="server" ID="LaunchTaskBtn" Text="Test" Onclick="LaunchTask" />
  </div>
</asp:Content>