<%@ Page Title="View Projects" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="ViewProjects.aspx.cs" Inherits="WebRole.ViewProjects" %>
<asp:Content ID="ViewProjects" ContentPlaceHolderID="Content" runat="server">
    <asp:ListView runat="server" ID="ProjectList">
      <LayoutTemplate>
        <ul id="ProjectList">
          <li runat="server" id="itemPlaceholder"></li>
          <li><a href="CreateProject.aspx">New Project</a></li>
        </ul>
      </LayoutTemplate>
      <ItemTemplate>
        <li runat="server" class="projectListElement" Onclick><%#Container.DataItem%></li>
      </ItemTemplate>
    </asp:ListView>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server">
$(function() {
  $('.projectListElement').click(function() {
    parent.UserBar.setProject($(this).text());
  });
});
</asp:Content>