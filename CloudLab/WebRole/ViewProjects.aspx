<%@ Page Title="View Projects" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="ViewProjects.aspx.cs" Inherits="WebRole.ViewProjects" %>
<asp:Content ID="ViewProjects" ContentPlaceHolderID="Content" runat="server">
<ul id="ProjectList">
  <asp:ListView runat="server">
    <LayoutTemplate>
      <li runat="server" id="itemPlaceholder"></li>
    </LayoutTemplate>
    <ItemTemplate>
      <li runat="server" class="projectListElement"><%#Container.DataItem%></li>
    </ItemTemplate>
  </asp:ListView>
  <li><asp:LinkButton runat="server" id="NewProjectBtn">New Project</asp:LinkButton></li>
</ul>
<asp:Panel runat="server" id="CreateProjectDialog" class="dialog">
  <table class="inputForm">
    <tr>
      <td><label>Project Name</label></td>
      <td>
        <asp:textbox id="NewProjectName" runat="server" />
        <asp:RequiredFieldValidator id="npRequired" runat="server" ControlToValidate="newProjectName" ErrorMessage="* Project must be given a name" Display="static">*</asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender runat="server" TargetControlID="npRequired" HighlightCssClass="validatorCalloutHighlight" />
      </td>
    </tr>
  </table>
  <asp:Button runat="server" ID="CreateProjectBtn" Text="Create" /> 
  <asp:Button runat="server" ID="CloseDialogBtn" Text="Cancel" CausesValidation="false" />
</asp:Panel>
<ajaxToolkit:ModalPopupExtender runat="server" BackgroundCssClass="dialog" TargetControlID="NewProjectBtn" PopupControlID="CreateProjectDialog" OkControlID="CreateProjectBtn" OnOkScript="CreateProject()" CancelControlID="CancelCreateBtn" />
</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
$(function() {
  $('.projectListElement').click(function() {
    CloudLab.UserBar.setProject($(this).text());
  });
});

function CreateProject() {
}
</asp:Content>