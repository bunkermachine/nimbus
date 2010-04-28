<%@ Page Title="View Projects" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="ViewProjects.aspx.cs" Inherits="WebRole.ViewProjects" %>
<asp:Content ID="ViewProjects" ContentPlaceHolderID="Content" runat="server">
    <asp:ListView runat="server" ID="ProjectList">
      <LayoutTemplate>
        <ul runat="server">
          <li runat="server" id="itemPlaceholder"></li>
          <li><a href="CreateProject.aspx">New Project</a></li>
        </ul>
      </LayoutTemplate>
      <ItemTemplate>
        <li runat="server"><%#Container.DataItem%></li>
      </ItemTemplate>
    </asp:ListView>
</asp:Content>
