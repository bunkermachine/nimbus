<%@ Page language="c#" Codebehind="Default.aspx.cs" Inherits="WebRole.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html>
<head runat="server">
  <title>CloudLab</title>
  <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.0r4/build/reset/reset-min.css" />
  <link rel="stylesheet" type="text/css" href="css/master.css" />
</head>
<body>

<form id="CloudLab" runat="server">

  <ajaxToolkit:ToolkitScriptManager ID="Manager" EnablePartialRendering="true" runat="server" />
  
  <div id="ApplicationFrame">
    <!-- Begin header -->
    <div id="Header"><div class="tl"><div class="tr">
      <div id="UserBar">
        <asp:UpdateProgress ID="StatusBar" runat="server">
          <ProgressTemplate>Loading...</ProgressTemplate>
        </asp:UpdateProgress>
        <a id="ProjectTitle"></a>
        <a id="CurrentUser"></a>
      </div>
    </div></div></div>
    <!-- End header -->
    
    <!-- Begin content -->
    <div id="Content">
      <div id="Sidebar">
        <ul id="SidebarTemplates">
          <li id="SidebarTemplateBasic">
            <h1 class="title"></h1>
            <span class="description"></span>
          </li>
          <li id="SidebarTemplateSimple">
            <h1 class="title"></h1>
          </li>
        </ul>
        <a id="SidebarTitle">Tasks</a>
        <div id="sidebarContent"></div>
      </div>
      
      <div id="hud">
        <div class="hud" id="DataSources">
          <div id="DataSourceSearch"><label>Enter Keyword:</label> <input type="text" /></div>
        </div>
        <div class="hud" id="Data" runat="server"><table id="dataTable"></table></div>
        <div class="hud" id="Parameters">
        </div>
      </div>

      <iframe src="ViewProjects.aspx" id="Workspace" frameborder="0" scrolling="no"></iframe>
    </div>  
    <!-- End content -->
    
    <!-- Begin footer -->
    <div id="Footer"><div class="br"><div class="bl"></div></div></div>
    <!-- End footer -->  
  </div>
</form>

<script language="javascript" type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" />
<script language="javascript" type="text/javascript" src="js/jquery.js" />
<script language="javascript" type="text/javascript" src="js/jquery.jup.js" />
<script language="javascript" type="text/javascript" src="js/jquery.dataTables.js" />
<script language="javascript" type="text/javascript" src="js/master.js" />

</body>
</html>