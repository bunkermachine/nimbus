 <%@ Page Title="New Task" Language="C#" MasterPageFile="~/Workspace.Master" AutoEventWireup="true" CodeBehind="NewTask.aspx.cs" Inherits="WebRole.NewTask" %>
<asp:Content id="NewTaskControl" ContentPlaceHolderID="Content" runat="server">
  <div id="NewTask">
    <h1>Launch a new task</h1>
    <table class="inputForm">
      <tr>
        <td><label>Task Name</label></td>
        <td>
          <asp:Textbox runat="server" ID="TaskNameText" /><br />
          <asp:RequiredFieldValidator runat="server" ID="TaskNameValidator" ControlToValidate="TaskNameText" ErrorMessage="Task must be given a name" />
          <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="TaskNameValidatorCallout" TargetControlID="TaskNameValidator" CssClass="validator" HighlightCssClass="validatorCalloutHighlight" />
        </td>
      </tr>
      <tr>
        <td><label>Dataset</label></td>
        <td><asp:ListBox runat="server" ID="DatasetList" SelectionMode="Multiple" OnSelectedIndexChanged="PopulateFileList" AutoPostBack="true" /></td>
      </tr>
      <tr>
        <td><label>Year</label></td>
        <td><asp:Textbox runat="server" ID="YearText" OnTextChanged="PopulateFileList" AutoPostBack="true" /></td>
      </tr>
      <tr>
        <td><label>Day</label></td>
        <td><asp:TextBox runat="server" ID="DayText" OnTextChanged="PopulateFileList" AutoPostBack="true" /></td>
      </tr>
        <td><label>Available Files</label></td>
        <td>
          <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <asp:ListBox runat="server" Rows="10" ID="FileList" SelectionMode="Multiple" OnSelectedIndexChanged="AddFile" AutoPostBack="true" />
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="DatasetList" />
              <asp:AsyncPostBackTrigger ControlID="YearText" />
              <asp:AsyncPostBackTrigger ControlID="DayText" />
              <asp:AsyncPostBackTrigger ControlID="SelectedFileList" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td><label>Selected Files</label></td>
        <td>
          <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <asp:ListBox runat="server" Rows="10" ID="SelectedFileList" SelectionMode="Multiple" OnSelectedIndexChanged="RemoveFile" AutoPostBack="true" />
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="DatasetList" />
              <asp:AsyncPostBackTrigger ControlID="YearText" />
              <asp:AsyncPostBackTrigger ControlID="DayText" />
              <asp:AsyncPostBackTrigger ControlID="FileList" />
            </Triggers>
          </asp:UpdatePanel>
          
        </td>
      </tr>
          OR
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
  <script language="javascript" type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2"></script>
  <div id="ParameterControls">
    <div id="Map"></div>
    <h1>Location</h1>
    <div id="LocationFormLeft">
      <label>Top</label> <asp:Textbox runat="server" ID="TopBound" /><br />
      <label>Bottom</label> <asp:Textbox runat="server" ID="Bottom" /><br />
    </div>
    <div id="LocationFormRight">
      <label>Left</label> <asp:Textbox runat="server" ID="LeftBound" /><br />
      <label>Right</label> <asp:Textbox runat="server" ID="RightBound" /><br />
    </div>
  </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server">
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
});

function NewTask() {
  if (Page_ClientValidate()) {
    CloudLab.Sidebar.addElement({
      title: $('<%=TaskNameText.ClientID %>').text(),
      description: 'Processing',
      progress: '0',
      click: function() {
        CloudLab.Workspace.set('ViewTask')
      }
    });
  }
}
</asp:Content>