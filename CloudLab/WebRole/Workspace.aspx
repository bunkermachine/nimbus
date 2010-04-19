<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Workspace.aspx.cs" Inherits="WebRole.WebForm1" %>

<div id="projects">
  <ul></ul>
</div>
<asp:UpdatePanel id="project" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
    <table>
      <tr>
        <td><asp:label runat="server" Width="112px">Project Name</asp:label></td>
        <td><asp:textbox id="newProjectName" runat="server" width="300px" /></td>
      </tr>
      <tr>
        <td><asp:label runat="server" Width="112px">Dataset</asp:label></td>
        <td><asp:textbox id="newDataset" runat="server" Width="300px" /></td>
      </tr>
    </table>
    <asp:Button id="startProject" UseSubmitBehavior="false" Text="Start Project" runat="server" OnClientClick="return CloudLab.startProject()" OnClick="CreateProject" />
  </ContentTemplate>
</asp:UpdatePanel>
<div id="map"></div>
<asp:UpdatePanel id="task" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
    <table>
      <tr>
        <td><asp:label runat="server" Width="112px">Task Name</asp:label></td>
        <td><asp:textbox id="newTaskName" runat="server" /></td>
      </tr>
      <tr>
        <td><asp:label runat="server" Width="112px">Dataset</asp:label></td>
        <td>
          <asp:ListBox ID="datasetList" runat="server" Width="300px" />
        </td>
      </tr>
      <tr>
        <td><asp:label runat="server" Width="112px">Files</asp:label></td>              
        <td>
          <asp:ListBox ID="fileList" runat="server" Width="300px" SelectionMode="Multiple" OnSelectedIndexChanged="DatasetSelected" />
        </td>
      </tr>
      <tr>
        <td><asp:label runat="server" Width="112px">Executable</asp:label></td>
        <td><asp:FileUpload runat="server" id="exeFile" /></td>
      </tr>
    </table>
    <asp:Button id="startTask" UseSubmitBehavior="false" Text="Start Task" runat="server" OnClientClick="return CloudLab.startTask()" OnClick="CreateTask" />
  </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel id="output" runat="server">
  <ContentTemplate>
    <asp:Label ID="programOutput" runat="server" Text='<%# Eval("Url") %>' />        
  </ContentTemplate>
</asp:UpdatePanel>
</div>