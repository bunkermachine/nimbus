﻿<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="WebRole.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html>
<head>
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

      <div id="workspace">
        <div id="projects">
          <ul></ul>
        </div>
        <asp:UpdatePanel id="project" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
            <table>
              <tr>
                <td><asp:label runat="server" Width="112px">Project Name</asp:label></td>
                <td><asp:textbox id="newProjectName" runat="server" width="300px" /></td>
              </tr>
              <tr>
                <td><asp:label runat="server" Width="112px">Dataset</asp:label></td>
                <td><asp:textbox id="newDataset" runat="server" Width="300px" /></td>
              </tr>
            </table>
            <asp:Button id="startProject" UseSubmitBehavior="false" Text="Start Project" runat="server" OnClientClick="return CloudLab.startProject()" OnClick="CreateProject" />
          </ContentTemplate>
        </asp:UpdatePanel>
        <div id="map"></div>
        <asp:UpdatePanel id="task" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
            <table>
              <tr>
                <td><asp:label runat="server" Width="112px">Task Name</asp:label></td>
                <td><asp:textbox id="newTaskName" runat="server" /></td>
              </tr>
              <tr>
                <td><asp:label runat="server" Width="112px">Dataset</asp:label></td>
                <td>
                  <asp:ListBox ID="datasetList" runat="server" Width="300px" />
                </td>
              </tr>
              <tr>
                <td><asp:label runat="server" Width="112px">Files</asp:label></td>              
                <td>
                  <asp:ListBox ID="fileList" runat="server" Width="300px" SelectionMode="Multiple" OnSelectedIndexChanged="DatasetSelected" />
                </td>
              </tr>
              <tr>
                <td><asp:label runat="server" Width="112px">Executable</asp:label></td>
                <td><asp:FileUpload runat="server" id="exeFile" /></td>
              </tr>
            </table>
            <asp:Button id="startTask" Text="Start Task" runat="server" OnClientClick="return CloudLab.startTask()" OnClick="CreateTask" />
          </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel id="output" runat="server">
          <ContentTemplate>
            <asp:Label ID="programOutput" runat="server" Text='<%# Eval("Url") %>' />        
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
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