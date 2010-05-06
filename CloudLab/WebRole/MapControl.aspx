<%@ Page Title="Create Project" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="MapControl.aspx.cs" Inherits="WebRole.MapControl" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
<script language="javascript" type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2"></script>
<div id="Map"></div>
<div id="ParameterControls">

  <fieldset>
    <legend>Location</legend>
    <label>From (Northwest)</label> <asp:Textbox runat="server" ID="topLeftLong" />
    <label>To (Southeast)</label> <asp:Textbox ClientID="topLeftLat" runat="server" ID="topLeftLat" />
  </fieldset>

  <asp:Textbox runat="server" ID="TopLat" />
  <asp:Textbox runat="server" ID="BottomLat" />

  <asp:Textbox runat="server" ID="LeftLong" />
  <asp:Textbox runat="server" ID="RightLong" />

  <asp:TextBox runat="server" ID="FromDateText" />
  <ajaxToolkit:Calendar runat="server" TargetControlID="FromDateText" Format="MMMM d, yyyy" />
  <asp:Textbox runat="server" ID="ToDateText" />
  <ajaxToolkit:Calendar runat="server" TargetControlID="ToDateText" Format="MMMM d, yyyy" />

</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
var tasks = [
  {
     title: "Initial",
     description: "Processing",
     progress: 50,
     click: function() {
       CloudLab.Workspace.set('NewTask');
     }
  }
];

$(function() {
  CloudLab.UserBar.setProject("Test");
  CloudLab.Sidebar.initList(tasks, 'Basic');
 
  // Setup our map
  var map = new VEMap('Map');
  map.LoadMap();
  map.SetMapStyle(VEMapStyle.Aerial);

  // Setup the paramter controls
  $("#fromDate, #toDate").datepicker();
});
</asp:Content>