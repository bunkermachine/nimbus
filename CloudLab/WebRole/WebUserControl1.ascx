<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="WebRole.WebUserControl1" %>
<div id="projects">
  <ul></ul>
</div>
<div id="project">
  <table>
    <tr>
      <td><asp:label ID="Label1" runat="server" Width="112px">Project Name</asp:label></td>
      <td>
        <asp:textbox id="newProjectName" runat="server" width="300px" />
        <asp:RequiredFieldValidator id="npRequired" runat="server" ControlToValidate="newProjectName" ErrorMessage="* Project must be given a name" Display="static">*</asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender runat="server" id="npValidation" targetControlID="npRequired" /> 
      </td>
    </tr>
    <tr>
      <td><asp:label ID="Label2" runat="server" Width="112px">Dataset</asp:label></td>
      <td><asp:textbox id="newDataset" runat="server" Width="300px" /></td>
    </tr>
  </table>
  <asp:Button id="startProject" UseSubmitBehavior="false" Text="Start Project" runat="server" OnClientClick="return CloudLab.startProject()" OnClick="CreateProject" />
</div>
<div id="map"></div>
<asp:UpdatePanel id="task" runat="server" UpdateMode="Conditional">
  <ContentTemplate>
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
  </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel id="output" runat="server">
  <ContentTemplate>
    <asp:Label ID="programOutput" runat="server" Text='<%# Eval("Url") %>' />        
  </ContentTemplate>
</asp:UpdatePanel>