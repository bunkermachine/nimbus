<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewProject.ascx.cs" Inherits="WebRole.NewProject" %>
<div id="project">
  <table>
    <tr>
      <td><label>Project Name</label></td>
      <td>
        <asp:textbox id="newProjectName" runat="server" width="300px" />
        <asp:RequiredFieldValidator id="npRequired" runat="server" ControlToValidate="newProjectName" ErrorMessage="* Project must be given a name" Display="static">*</asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender runat="server" id="npValidation" targetControlID="npRequired" /> 
      </td>
    </tr>
    <tr>
      <td><label>Dataset</label></td>
      <td><asp:textbox id="newDataset" runat="server" Width="300px" /></td>
    </tr>
  </table>
  <asp:Button id="startProject" UseSubmitBehavior="false" Text="Start Project" runat="server" OnClientClick="return CloudLab.startProject()" OnClick="CreateProject" />
</div>