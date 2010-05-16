 <%@ Page Title="New Task" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="NewTask.aspx.cs" Inherits="WebRole.NewTask" %>

<asp:Content ContentPlaceHolderID="Overlay" runat="server">
  <script language="javascript" type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2"></script>
  <div id="Map" class="map"></div>
  <div id="MapParams">
    <h1>Location</h1>
    <div class="fieldGroup"><label>Top</label> <asp:Textbox runat="server" ID="TopBound" /></div>
    <div class="fieldGroup"><label>Bottom</label> <asp:Textbox runat="server" ID="Bottom" /></div>
    <div class="fieldGroup"><label>Left</label> <asp:Textbox runat="server" ID="LeftBound" /></div>
    <div class="fieldGroup"><label>Right</label> <asp:Textbox runat="server" ID="RightBound" /></div>
  </div>
</asp:Content>

<asp:Content id="NewTaskControl" ContentPlaceHolderID="Content" runat="server">
  <div id="NewTask">
    <h1>Launch a new task</h1>
    <div id="TaskParam" class="fieldGroup">
      <label>Task Name</label>
      <asp:Textbox runat="server" ID="TaskNameText" />
      <asp:RequiredFieldValidator runat="server" ID="TaskNameValidator" ControlToValidate="TaskNameText" ErrorMessage="Task must be given a name" />
      <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="TaskNameValidatorCallout" TargetControlID="TaskNameValidator" CssClass="validator" HighlightCssClass="validatorCalloutHighlight" />
    </div>
    <div id="FromDateParam" class="fieldGroup">
      <label>From</label>
      <asp:Textbox runat="server" ID="FromDateText" />
      <ajaxToolkit:CalendarExtender runat="server" ID="FromDate" TargetControlID="FromDateText" />
    </div>
    <div id="ToDateParam" class="fieldGroup">
      <label>To</label>
      <asp:Textbox runat="server" ID="ToDateText" />
      <ajaxToolkit:CalendarExtender runat="server" ID="ToDate" TargetControlID="ToDateText" />
    </div>
    <br class="clear" />
    <div class="fieldGroup">
      <label>Latitude</label> <asp:Textbox runat="server" ID="TopLatText" /><asp:Textbox runat="server" ID="BottomLatText" />
    </div>
    <div id="LeftParam" class="fieldGroup">
      <label>Longitude</label> <asp:Textbox runat="server" ID="LeftLongText" /><asp:Textbox runat="server" ID="RightLongText" />
    </div>
    <div id="ShowMap" class="fieldGroup">
      <a id="ShowMapBtn">Show Map</a>
    </div>
    <br class="clear" />
    <div id="DatasetParam" class="fieldGroup">
      <label>Dataset</label>
      <asp:ListBox runat="server" Rows="10" ID="DatasetList" SelectionMode="Multiple" OnSelectedIndexChanged="PopulateFileList" AutoPostBack="true" />
    </div>
    <div id="AvailableFilesParam" class="fieldGroup">
      <label>Available Files</label>
      <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="AvailableFilePanel">
        <ProgressTemplate>
          <div>Searching for files...</div>
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel id="AvailableFilePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <asp:ListBox runat="server" Rows="10" ID="AvailableFileList" SelectionMode="Multiple" OnSelectedIndexChanged="AddFile" AutoPostBack="true" />
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="DatasetList" />
          <asp:AsyncPostBackTrigger ControlID="FromDateText" />
          <asp:AsyncPostBackTrigger ControlID="ToDateText" />
          <asp:AsyncPostBackTrigger ControlID="SelectedFileList" />
        </Triggers>
      </asp:UpdatePanel>
    </div>
    <div id="SelectedFilesParam" class="fieldGroup">
      <label>Selected Files</label>
      <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <asp:ListBox runat="server" Rows="10" ID="SelectedFileList" SelectionMode="Multiple" OnSelectedIndexChanged="RemoveFile" AutoPostBack="true" />
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="AvailableFileList" />
        </Triggers>
      </asp:UpdatePanel>
    </div>
    <table class="inputForm">
    <tr>
        <td><label>Upload HDF(s)</label>
        </td>
        <td><asp:FileUpload runat="server" ID="FileUpload1" /></td>
    </tr>
      <tr>
        <td><label>Executable</label></td>
        <td>
          <asp:FileUpload runat="server" id="ExeFile" />
        </td>
      </tr>
    </table>
    <asp:Button runat="server" ID="LaunchTaskBtn" Text="Test" CausesValidation="true" OnClientClick="NewTask()" Onclick="LaunchTask" />
  </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
<script type="text/javascript">
var tasks = [
  {
    title: "Evapotranspiration",
    description: "Processing",
    progress: 50,
    click: function() {
      CloudLab.Workspace.set("ViewTask.aspx");
    }
  }
];

$(function() {
  CloudLab.Sidebar.initList(tasks, 'Basic');
 
  // Setup our map
  var map = new VEMap('Map');
  map.LoadMap();
  map.SetMapStyle(VEMapStyle.Aerial);

	map.AttachEvent("onmousedown", function(e) {
		var point = map.PixelToLatLong(new VEPixel(e.mapX, e.mapY));
		if (point.Longitude >= -180 && point.Longitude <= 180) {
			if (dragging) {
				region.end = point;
				updateShape(region);
				dragging = false;
			} else {
				region.start = region.end = point;
				drawShape(region);
				dragging = true;
				return true;
			}
		}
	});
	map.AttachEvent("onmousemove", function(e) {
		if (dragging) {
			region.end = map.PixelToLatLong(new VEPixel(e.mapX, e.mapY));
			updateShape(region);
		}
	});
	map.AttachEvent("onmouseup", function(e) { dragging = false; });  
});

function NewTask() {
  if (Page_ClientValidate()) {
    CloudLab.Sidebar.addTask({
      title: $('<%=TaskNameText.ClientID %>').text(),
      description: 'Processing',
      progress: '0',
      click: function() {
        CloudLab.Workspace.set('ViewTask')
      }
    });
  }
}
</script>
</asp:Content>