<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="CloudLab.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd"><html><head>  <title>CloudLab</title>  <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/2.8.0r4/build/reset/reset-min.css" />  <link rel="stylesheet" type="text/css" href="css/master.css" /></head><body><div id="application">  <div id="applicationFrame">    <!-- Begin header -->    <div id="header"><div class="tl"><div class="tr"><span id="projectTitle"></span><span id="currentUser"></span></div></div></div>    <!-- End header -->        <!-- Begin content -->    <div id="content">      <div id="sidebar">        <ul id="sidebarTemplates">          <li id="sidebarTemplateBasic">            <h1 class="title"></h1>            <span class="description"></span>          </li>          <li id="sidebarTemplateSimple">            <h1 class="title"></h1>          </li>        </ul>        <a id="sidebarTitle">Tasks</a>        <div id="sidebarContent"></div>      </div>            <div id="hud">
        <div class="hud" id="dataSources">
          <div id="dataSourceSearch"><label>Enter Keyword:</label> <input type="text" /></div>

        </div>
  
        <div class="hud" id="data" runat="server"><table id="dataTable"></table></div>
        
        <div class="hud" id="parameters">
        </div>
      </div>      <div id="workspace">        <div id="projects">          <ul></ul>        </div>        <div id="map">        </div>        <div id="task">          <form id="upload" runat="server">            <ajaxToolkit:ToolkitScriptManager ID="scriptManager" EnablePartialRendering="true" runat="server" />            <h1>Task Name</h1>            <input type="text" id="taskName" />            <h1>Select Dataset</h1>            <select multiple="true">
              <option>MODIS/Aqua Raw Radiances in Counts 5-Min L1A Swath V005</option>
              <option>MODIS/Aqua Temperature and Water Vapor Profiles 5-Min L2 Swath 5m V005</option>
              <option>MODIS/Aqua Total Precipitable Water Vapor 5-Min L2 Swath 1km and 5km V005</option>
              <option>NAMMA MODIS/AQUA AND MODIS/TERRA DEEP BLUE PRODUCTS V1</option>
              <option>CLPX-Satellite: MODIS Radiances, Reflectances, Snow Cover and Related Grids</option>
              <option>MODIS/Terra 8-Day Clear Sky Radiance Bias Daily L3 Global 1Deg Zonal Bands V005</option>
              <option>MODIS/Terra Aerosol 5-Min L2 Swath 10km V005</option>
              <option>MODIS/Terra Aerosol Cloud Water Vapor Ozone 8-Day L3 Global 1Deg CMG V005</option>
            </select>            <h1>Select Files</h1>            <select multiple="true">
              <option>MODIS/Aqua Raw Radiances in Counts 5-Min L1A Swath V005</option>
              <option>MODIS/Aqua Temperature and Water Vapor Profiles 5-Min L2 Swath 5m V005</option>
              <option>MODIS/Aqua Total Precipitable Water Vapor 5-Min L2 Swath 1km and 5km V005</option>
              <option>NAMMA MODIS/AQUA AND MODIS/TERRA DEEP BLUE PRODUCTS V1</option>
              <option>CLPX-Satellite: MODIS Radiances, Reflectances, Snow Cover and Related Grids</option>
              <option>MODIS/Terra 8-Day Clear Sky Radiance Bias Daily L3 Global 1Deg Zonal Bands V005</option>
              <option>MODIS/Terra Aerosol 5-Min L2 Swath 10km V005</option>
              <option>MODIS/Terra Aerosol Cloud Water Vapor Ozone 8-Day L3 Global 1Deg CMG V005</option>
              </select>            <h1>Upload EXE</h1>            <label>File</label> <ajaxToolkit:AsyncFileUpload runat="server" id="exeFile" />            <input type="submit" id="startTask" value="Start Task" runat="server" name="Submit" onserverclick="Submit_ServerClick" />          </form>      
        </div>      </div>    </div>      <!-- End content -->        <!-- Begin footer -->    <div id="footer"><div class="br"><div class="bl"></div></div></div>    <!-- End footer -->    </div></div><script src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" type="text/javascript"></script><script src="js/jquery-1.4.1.js" type="text/javascript"></script><script src="js/jquery.jup.js" type="text/javascript"></script><script src="js/jquery.dataTables.js" type="text/javascript"></script><script src="js/master.js" type="text/javascript"></script></body></html>