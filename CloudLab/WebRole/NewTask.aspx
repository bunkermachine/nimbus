<%@ Page Title="New Task" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="NewTask.aspx.cs" Inherits="WebRole.NewTask" %>
<asp:Content ID="NewTask" ContentPlaceHolderID="Content" runat="server">

<div id="task">
  <table>
    <tr>
      <td><asp:label ID="Label3" runat="server" Width="112px">Task Name</asp:label></td>
      <td><asp:textbox id="newTaskName" runat="server" /></td>
    </tr>
    <tr>
      <td><asp:label ID="Label4" runat="server" Width="112px">Dataset</asp:label></td>
      <td>
        <asp:ListBox ID="datasetList" runat="server" Width="300px" />
      </td>
    </tr>
    <tr>
      <td><asp:label ID="Label5" runat="server" Width="112px">Files</asp:label></td>              
      <td>
        <asp:ListBox ID="fileList" runat="server" Width="300px" SelectionMode="Multiple" OnSelectedIndexChanged="DatasetSelected" />
      </td>
    </tr>
    <tr>
      <td><asp:label ID="Label6" runat="server" Width="112px">Executable</asp:label></td>
      <td><asp:FileUpload runat="server" id="exeFile" /></td>
    </tr>
  </table>
  <asp:Button id="startTask" UseSubmitBehavior="false" Text="Start Task" runat="server" OnClientClick="return CloudLab.startTask()" OnClick="CreateTask" />
</div>

</asp:Content>