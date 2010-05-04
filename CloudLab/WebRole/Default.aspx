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

<div id="CloudLab">
  <div id="ApplicationFrame">
    <!-- Begin header -->
    <div id="Header"><div class="tl"><div class="tr">
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
        <div id="SidebarHandle"></div><h1 id="SidebarTitle">Tasks</h1><div id="SidebarContent"></div>
      </div>
      
      <div id="HUD">
        <div class="hud" id="DataSources">
          <div id="DataSourceSearch"><label>Enter Keyword:</label> <input type="text" /></div>
        </div>
        <div class="hud" id="Data" runat="server"><table id="dataTable"></table></div>
        <div class="hud" id="Parameters">
        </div>
      </div>

<<<<<<< HEAD
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
=======
      <iframe src="ViewProjects.aspx" name="Workspace" id="Workspace" frameborder="0" scrolling="no"></iframe>
>>>>>>> newui
    </div>  
    <!-- End content -->
    
    <!-- Begin footer -->
    <div id="Footer"><div class="br"><div class="bl"></div></div></div>
    <!-- End footer -->  
  </div>
</div>

<script language="javascript" type="text/javascript" src="js/jquery.ui.js"></script>
<script language="javascript" type="text/javascript" src="js/jquery.dataTables.js"></script>
<script language="javascript" type="text/javascript" src="js/master.js"></script>

</body>
</html>