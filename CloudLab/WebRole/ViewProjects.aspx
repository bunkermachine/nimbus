<%@ Page Title="View Projects" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="ViewProjects.aspx.cs" Inherits="WebRole.ViewProjects" %>
<asp:Content ID="ViewProjects" ContentPlaceHolderID="Content" runat="server">
<ul id="ProjectList">
  <asp:ListView id="ProjectListView" runat="server" OnItemCommand="OpenProject">
    <LayoutTemplate>
      <li runat="server" id="itemPlaceholder"></li>
    </LayoutTemplate>
    <ItemTemplate>
      <li runat="server" class="projectListElement">
        <asp:LinkButton runat="server" id="ProjectButton" CausesValidation="false" CommandName="SelectProject" CommandArgument="<%#Container.DataItem%>" Text="<%#Container.DataItem%>" />
      </li>
    </ItemTemplate>
  </asp:ListView>
  <li id="CreateProjectItem">
    <asp:Textbox runat="server" ID="NewProjectName"  />
    <asp:LinkButton runat="server" ID="CreateProjectBtn" Text="+" OnClick="CreateProject" />
    <asp:RequiredFieldValidator runat="server" ID="NewProjectValidator" ControlToValidate="NewProjectName" ErrorMessage="* Project must be given a name" Display="static">*</asp:RequiredFieldValidator>
    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="NewProjectValidatorCallout" TargetControlID="NewProjectValidator" HighlightCssClass="validatorCalloutHighlight" />
  </li>
</ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
var projects = [
  {
    title: "Evapotranspiration",
    description: "Processing",
    progress: 50,
    click: function() {
      CloudLab.Workspace.set("MapControl");
    }
  }
];

$(function() {
  $('.projectListElement').click(function() {
    CloudLab.UserBar.setProject($(this).text());
  });
});
</asp:Content>