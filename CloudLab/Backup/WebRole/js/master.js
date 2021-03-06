/** * Hardcoded Test Data * ------------------- * Some sample data before we have the full fledged table setup */
// User ID, Username, real name
var users = [[0, 'davidli@cs.stanford.edu', 'David Li']];
// User ID, Project Name, Project Shortname
var projects = [];
// Project ID, Task Name, Task Type, Progress
var tasks = [];
// Task ID, Queued Item
var queue = [[0, 'MODISAOE_1']];
/** * Website Interactions * -------------------- * Controls most of the major page changes */
var CloudLab = new function() {
  this.redisplay = function() {
    $('#workspace').width($(window).width()).height($(window).height() - 67);
    //$('#main').css('display','none').css('display','block');
    Workspace.refresh();
  }
  this.selectProject = function(projectID) {
    for (var idx in projects) {
      if (projects[idx][0] == projectID) {
        Sidebar.initList(tasks, 'Basic');
        $('#projectTitle').text(projects[idx][1]);
        Workspace.setMode('map');
        break;
      }
    }
  }
  this.startTask = function(taskName) {
    Workspace.setMode('map');
    tasks.push([0, $('#newTaskName').val(), 'Downloading', 0]);
    Sidebar.initList(tasks, 'Basic');
    return false;
  };
  this.startProject = function(projectName) {
    projects.push([projects.length, $('#newProjectName').val(), 'test']);
    CloudLab.selectProject(projects.length - 1);
    return false;
  };
}
/** * Sidebar * --------- * Bundles the sidebar */
var Sidebar = new function() {
  var sidebar = $('#sidebar');
  var sidebarTemplates = sidebar.children('#sidebarTemplates');
  var sidebarTitle = sidebar.children('#sidebarTitle');
  var sidebarContent = sidebar.children('#sidebarContent');
  var sidebarHeight = 0;
  this.initList = function(list, style) {
    this.clearList();
    sidebar.show();
    var template = sidebarTemplates.children('#sidebarTemplate' + style);
    var sidebarList = $(document.createElement('ul'));
    sidebarList.addClass('sidebarMode');
    // Add a list entry for each of the items
    for (var idx in list) {
      var sidebarListElement = template.clone();
      sidebarListElement.removeAttr('id');
      sidebarListElement.children('.title').text(list[idx][1]);
      sidebarListElement.children('.description').text('Status: ' + list[idx][2] + ' (' + list[idx][3] + '%)');
      sidebarListElement.click(function() {
        Workspace.setMode('output');
      });
      sidebarList.append(sidebarListElement);
    }
    var newTask = $(document.createElement('li'));
    newTask.text('Add Task');
    newTask.click(function() {
      Workspace.setMode('task');
    });
    sidebarList.append(newTask);
    sidebarContent.append(sidebarList);
    sidebarHeight = sidebarList.height();
    this.expand();
  };
  this.expand = function() {
    sidebarContent.animate({ height: sidebarHeight }, 'fast');
    sidebarTitle.unbind('click');
    sidebarTitle.click(Sidebar.collapse);
  };
  this.collapse = function() {
    sidebarContent.animate({ height: 0 }, 'fast');
    sidebarTitle.unbind('click');
    sidebarTitle.click(Sidebar.expand);
  };
  this.clearList = function() {
    sidebar.hide();
    sidebarContent.children().remove();
    sidebarHeight = 0;
  };
};
/** * HUD Boxes * --------- * Draggable, movable, hidable boxes for showing different task modules */
$.fn.hud = function(options) {
  var settings = jQuery.extend({
    title: 'Datasets',
    position: 'bottom',
    width: 300
  }, options);
  // Wrap our content in an modal box
  var container = $(document.createElement('div'));
  container.addClass('hudContainer');
  // Add a title bar to our modal box
  var titleBar = $(document.createElement('div'));
  titleBar.addClass('hudTitle');
  // Position and size our modal box
  container.width(settings.width);
  if ('bottom' != settings.position) {
    container.css('top', '0px');
  }
  container.css(settings.position, '0px');
  this.css('border-' + settings.position + '-width', '0px');
  this.show();
  this.wrap(container);
  container.append(titleBar);
  return this;
};
/** * Workspace * --------- * Houses the main work area of CloudLab */
var Workspace = new function() {
  var workspace = $('#workspace');
  /* Virtual Earth Map */
  var map = null;
  this.mode = 'projects';
  this.refresh = function() {
    this.setMode(this.mode);
  };
  this.setMode = function(mode) {
    this.mode = mode;
    workspace.children().hide();
    switch (mode) {
      case 'task': this.taskMode(); break;
      case 'projects': this.projectsMode(); break;
      case 'project': this.projectMode(); break;
      case 'map': this.mapMode(); break;
    }
    $('#' + mode).show();
  };
  this.taskMode = function() {
    $('form')[0].reset();
  };
  this.projectsMode = function() {
    Sidebar.clearList();
    $('#projects ul').children().remove();
    // Add behavior to the project buttons
    for (var idx in projects) {
      var project = $(document.createElement('li'));
      project.text(projects[idx][1]);
      project.click(function() {
        CloudLab.selectProject(projects[idx][0]);
      });
      $('#projects ul').append(project);
    }
    // New project button
    var newProjectButton = $(document.createElement('li'));
    $('#projects ul').append(newProjectButton);
    newProjectButton.click(function() {
      Workspace.setMode('project');
    });
    newProjectButton.text('New Project');
  };
  this.projectMode = function() {
    $('form')[0].reset();
  };
  this.mapMode = function() {
    if (!map) {
      map = new VEMap('map');
      map.LoadMap();
      map.SetMapStyle(VEMapStyle.Aerial);
    }
    map.Resize($('#workspace').width(), $('#workspace').height());
  };
};

$.fn.clearForm = function() {
 return this.each(function() {
   var type = this.type, tag = this.tagName.toLowerCase();
   if (tag == 'form')
     return $(':input',this).clearForm();
   if (type == 'text' || type == 'password' || tag == 'textarea')
     this.value = '';
   else if (type == 'checkbox' || type == 'radio')
     this.checked = false;
    else if (tag == 'select')
      this.selectedIndex = -1;
  });
};
// Data Table 
//var dataTable = null;
//
//function initDataTable()
//{
//  dataTable = $('#DataTable').dataTable({
//    'aoColumns': [
//      { 'sTitle': 'Date' },
//      { 'sTitle': 'Surf. Pressure' },
//      { 'sTitle': 'Air Temp.' },
//      { 'sTitle': 'Rel. Hum.' },
//      { 'sTitle': 'Cloud Cov.' },
//      { 'sTitle': 'Precipitation' },
//      { 'sTitle': 'Down. Short. Rad.' },
//      { 'sTitle': 'Down. Long. Rad. '}
//    ],
//   	'bSortClasses': false
//  });
//  
//  $('#data-table tbody td').hover(function() {
//      var iCol = $('td').index(this) % 5;
//    	var nTrs = dataTable.fnGetNodes();
//    	$('td:nth-child('+(iCol+1)+')', nTrs).addClass('highlighted');
//    },
//    function() {
//    	var nTrs = dataTable.fnGetNodes();
//    	$('td.highlighted', nTrs).removeClass('highlighted');
//  });
//}
//
///* Parameter List */
//function displayHDFData(response, formId)
//{
//  dataTable.fnAddData([
//    [ 2010021818 , 1014 , 23 , 27 , 'NaN' , 'NaN' , 'NaN' , 'NaN' ],
//    [ 2010021821 , 1015.7 , 22.4 , 30.5 , 23 , 0 , 0 , 336 ],
//    [ 2010021900 , 1016.7 , 30.2 , 19.5 , 12 , 0 , 130 , 338 ],
//    [ 2010021903 , 1014.8 , 35.1 , 14 , 10 , 0 , 880 , 368 ],
//    [ 2010021906 , 1011.5 , 37.2 , 11.4 , 5 , 0 , 945 , 375 ],  
//    [ 2010021909 , 1010 , 35.8 , 11.8 , 1 , 0 , 560 , 381 ],
//    [ 2010021912 , 1012.1 , 29.8 , 16.7 , 0 , 0 , 292 , 371 ],
//    [ 2010021915 , 1012.4 , 26.9 , 19.4 , 0 , 0 , 0 , 345 ],
//    [ 2010021918 , 1011.3 , 25.6 , 23.5 , 4 , 0 , 0 , 343 ]
//  ]);
//}
$(function() {
  //GetMap();
  CloudLab.redisplay();
  Sidebar.clearList();
  $('#addButton').click(function(e) {
    e.preventDefault();
    addParameter();
  });
  $('#projectTitle').click(function() {
    Workspace.setMode('projects');
  });
  $('#upload').submit(function() {
    tasks.push([0, $('#taskName').val(), 'Processing', 0]);
  });
  Workspace.setMode('projects');

  // Add HUD functionalities to our modal boxes
  //$('#DataSources').hud({ position: 'bottom' });
  //$('#Data').hud({ position: 'bottom' });
  //$('#parameters').hud({ position: 'right' });
  // Setup the HDF uploader
  //$('#UploadHDF').jup({ json: true, onComplete: displayHDFData });
  // Resize the map whenever the window is resized
  $(window).bind('load resize', CloudLab.redisplay);
  //initDataTable();
});