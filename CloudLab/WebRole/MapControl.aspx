<%@ Page Title="Create Project" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="MapControl.aspx.cs" Inherits="WebRole.MapControl" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
<script language="javascript" type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2"></script>
<div id="map"></div>
</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
var tasks = [
  {
     title: "Initial",
     description: "Processing",
     progress: 50,
     click: function() {
       parent.Workspace.set('NewTask');
     }
  }
];

$(function() {
  parent.Sidebar.initList(tasks, 'Basic');

  var map = new VEMap('map');
  map.LoadMap();
  map.SetMapStyle(VEMapStyle.Aerial);
});
</asp:Content>