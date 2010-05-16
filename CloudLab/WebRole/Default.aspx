<%@ Page language="c#" AutoEventWireup="true" Codebehind="Default.aspx.cs" Inherits="WebRole.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html>
<head runat="server">
  <title>CloudLab</title>
  <link rel="stylesheet" type="text/css" href="css/reset.css" />
  <link rel="stylesheet" type="text/css" href="css/master.css" />
  <script language="javascript" type="text/javascript" src="js/jquery.js"></script>
</head>
<body>
<form id="form" runat="server">
<ajaxToolkit:ToolkitScriptManager ID="manager" EnablePartialRendering="true" runat="server" />

<div id="CloudLab">
  <div id="ApplicationFrame">
    <!-- Begin header -->
    <div id="Header"><div class="tl"><div class="tr">
      <div id="UserBar">
        <div id="ProjectTitle">Test</div>
      </div>
    </div></div></div>
    <!-- End header -->
    
    <!-- Begin content -->
    <div id="Content">
      <div id="Sidebar">
        <ul class="templates">
          <li id="SidebarTemplateBasic"><h1 class="title"></h1><span class="description"></span></li>
          <li id="SidebarTemplateSimple"><h1 class="title"></h1></li>
        </ul>
        <div class="handle"></div>
        <div class="content">
          <asp:UpdatePanel runat="server" ID="SidebarPanel" OnLoad="UpdateSidebar">
            <ContentTemplate>
              <div id="SidebarLogin">
                <asp:LoginView runat="server">
                  <AnonymousTemplate><asp:Login ID="Login" runat="server" /></AnonymousTemplate>
                  <LoggedInTemplate><asp:LoginName runat="server" /></LoggedInTemplate>
                </asp:LoginView>
              </div>
              <div id="SidebarProjects">
                <h1 class="sectionTitle">Projects</h1>
                <asp:ListView id="ProjectListView" runat="server" OnItemCommand="OpenProject">
                  <LayoutTemplate>
                    <ul>
                      <li runat="server" id="itemPlaceholder"></li>
                    </ul>
                  </LayoutTemplate>
                  <ItemTemplate>
                    <li runat="server" class="sidebarElement">
                      <h1 class="title">
                        <asp:LinkButton runat="server" id="ProjectButton" CommandName="SelectProject" CommandArgument="<%#Container.DataItem%>" Text="<%#Container.DataItem%>" />
                      </h1>
                    </li>
                  </ItemTemplate>
                </asp:ListView>
                <div id="NewProjectBtn" class="sidebarElement">New Project</div>
              </div>
              <div id="SidebarTasks">
                <h1 class="sectionTitle">Tasks</h1>
                <ul></ul>
                <div id="NewTaskBtn" class="sidebarElement">New Task</div>
              </div>
            </ContentTemplate>
          </asp:UpdatePanel>
        </div>
      </div>
      
      <div id="HUD">
        <div class="hud" id="DataSources">
          <div id="DataSourceSearch"><label>Enter Keyword:</label> <input type="text" /></div>
        </div>
        <div class="hud" id="Data" runat="server"><table id="dataTable"></table></div>
        <div class="hud" id="Parameters">
        </div>
      </div>

      <div id="Workspace">
        <iframe src="ViewProjects.aspx" name="Workspace" frameborder="0"></iframe>
      </div>
    </div>  
    <!-- End content -->
    
    <!-- Begin footer -->
    <div id="Footer"><div class="br"><div class="bl"></div></div></div>
    <!-- End footer -->  
  </div>
</div>
</form>

<script language="javascript" type="text/javascript" src="js/jquery.ui.js"></script>
<script language="javascript" type="text/javascript" src="js/jquery.dataTables.js"></script>
<script language="javascript" type="text/javascript" src="js/master.js"></script>

</body>
</html>