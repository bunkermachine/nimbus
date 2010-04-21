<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="WebRole.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html>
<head runat="server">
  <title>CloudLab</title>
  <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.0r4/build/reset/reset-min.css" />
  <link rel="stylesheet" type="text/css" href="css/master.css" />
</head>
<body>

<form id="application" runat="server">

  <ajaxToolkit:ToolkitScriptManager ID="scriptManager" EnablePartialRendering="true" runat="server" />
  
  <div id="applicationFrame">
    <!-- Begin header -->
    <div id="header"><div class="tl"><div class="tr">
      <div id="userBar">
        <asp:UpdateProgress ID="statusBar" runat="server">
          <ProgressTemplate>Loading...</ProgressTemplate>
        </asp:UpdateProgress>
        <a id="projectTitle"></a>
        <a id="currentUser"></a>
      </div>
    </div></div></div>
    <!-- End header -->
    
    <!-- Begin content -->
    <div id="content">
      <div id="sidebar">
        <ul id="sidebarTemplates">
          <li id="sidebarTemplateBasic">
            <h1 class="title"></h1>
            <span class="description"></span>
          </li>
          <li id="sidebarTemplateSimple">
            <h1 class="title"></h1>
          </li>
        </ul>
        <a id="sidebarTitle">Tasks</a>
        <div id="sidebarContent"></div>
      </div>
      
      <div id="hud">
        <div class="hud" id="dataSources">
          <div id="dataSourceSearch"><label>Enter Keyword:</label> <input type="text" /></div>

        </div>
  
        <div class="hud" id="data" runat="server"><table id="dataTable"></table></div>

        <div class="hud" id="parameters">
        </div>
      </div>

      <asp:UpdatePanel id="workspace" runat="server">
        <ContentTemplate>
          <asp:Button runat="server" ID="testButton" Text="Test" OnClick="CreateProject" />
          <div id="projects">
            <ul></ul>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>  
    <!-- End content -->
    
    <!-- Begin footer -->
    <div id="footer"><div class="br"><div class="bl"></div></div></div>
    <!-- End footer -->  
  </div>
</form>

<script src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" type="text/javascript"></script>
<script src="js/jquery-1.4.1.js" type="text/javascript"></script>
<script src="js/jquery.jup.js" type="text/javascript"></script>
<script src="js/jquery.dataTables.js" type="text/javascript"></script>
<script src="js/master.js" type="text/javascript"></script>

</body>
</html>