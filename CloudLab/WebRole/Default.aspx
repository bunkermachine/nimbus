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
<div id="CloudLab">
  <div id="ApplicationFrame">
    <!-- Begin header -->
    <div id="Header"><div class="tl"><div class="tr">
      <div id="ProfileTag">David (<asp:LinkButton id="Signout" text="Logout" onclick="SubmitBtn_Click" runat="server" />)</div>
      <div id="UserBar">
        <div id="ProjectTitle">Test</div>
        <ul id="ProjectDropdown">
          <li>Global Evapotranspiration</li>
          <li>Cloud Nimbus</li>
        </ul>
      </div>
    </div></div></div>
    <!-- End header -->
    
    <!-- Begin content -->
    <div id="Content">
      <div id="Sidebar">
        <ul id="SidebarTemplates">
          <li id="SidebarTemplateBasic"><h1 class="title"></h1><span class="description"></span></li>
          <li id="SidebarTemplateSimple"><h1 class="title"></h1></li>
        </ul>
        <div id="SidebarHandle"></div>
        <h1 id="SidebarTitle">Tasks</h1>
        <div id="SidebarContent"><ul id="SidebarList"></ul></div>
      </div>
      
      <div id="HUD">
        <div class="hud" id="DataSources">
          <div id="DataSourceSearch"><label>Enter Keyword:</label> <input type="text" /></div>
        </div>
        <div class="hud" id="Data" runat="server"><table id="dataTable"></table></div>
        <div class="hud" id="Parameters">
        </div>
      </div>

      <iframe src="ViewProjects.aspx" name="Workspace" id="Workspace" frameborder="0" scrolling="no"></iframe>
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