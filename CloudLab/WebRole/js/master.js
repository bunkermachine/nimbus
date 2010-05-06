/**
 * Hardcoded Test Data
 * -------------------
 * Some sample data before we have the full fledged table setup
 */
// User ID, Username, real name
var users = [[0, 'davidli@cs.stanford.edu', 'David Li']];
// User ID, Project Name, Project Shortname
var projects = [];
// Project ID, Task Name, Task Type, Progress
var tasks = [];
// Task ID, Queued Item
var queue = [[0, 'MODISAOE_1']];

/**
 * Website Interactions
 * --------------------
 * Controls most of the major page changes
 */
CloudLab = new function() {
  this.redisplay = function() {
    $('#Workspace').width($(window).width() - 24).height($(window).height() - 67);
  }
}

/**
 * UserBar
 * -----------
 * Bundles the userbar functionality
 */

CloudLab.UserBar = new function() {
  var userBar = $('#UserBar');
  var projectTitle = $('#ProjectTitle');
  var projectDropdown = $('#ProjectDropdown');

  this.init = function() {
    projectTitle.click(CloudLab.UserBar.showProjects);
  };

  this.addProject = function(project) {
  };

  this.setProject = function(projectName) {
    userBar.show();
    projectTitle.text(projectName);
  };

  this.showProjects = function() {
    projectDropdown.show();
    projectTitle.unbind('click');
    projectTitle.click(CloudLab.UserBar.hideProjects);
  };

  this.hideProjects = function() {
    projectDropdown.hide();
    projectTitle.unbind('click');
    projectTitle.click(CloudLab.UserBar.showProjects);
  };
};

/**
 * Sidebar
 * ---------
 * Bundles the sidebar
 */

CloudLab.Sidebar = new function() {
  var sidebar = $('#Sidebar');
  var sidebarTemplates = sidebar.children('#SidebarTemplates');
  var sidebarTitle = sidebar.children('#SidebarTitle');
  var sidebarContent = sidebar.children('#SidebarContent');
  var sidebarHandle = sidebar.children('#SidebarHandle');
  var sidebarList = sidebarContent.children('#SidebarList');
  var sidebarWidth = 0;
  var currentStyle = "";

  this.init = function() {
    this.clearList();
    sidebarWidth = sidebar.width();
    sidebar.resizable({ handles: 'w', maxWidth: 300, stop: function(event, ui) {
      sidebarWidth = sidebar.width();
    } });
    sidebarHandle.click(CloudLab.Sidebar.collapse);

    var newTask = $('#SidebarGeneric');
    newTask.click(function() {
      CloudLab.Workspace.set('NewTask');
    });
  }

  this.setTitle = function(title) {
    sidebarTitle.text(title);
  }

  this.addElement = function(element) {
    var template = sidebarTemplates.children('#SidebarTemplate' + currentStyle);
    var sidebarListElement = template.clone();
    sidebarListElement.removeAttr('id');
    sidebarListElement.children('.title').text(element['title']);
    sidebarListElement.children('.description').text('Status: ' + element['description'] + ' (' + element['progress'] + '%)');
    sidebarListElement.click(element['click']);
    sidebarList.append(sidebarListElement);
  }

  this.initList = function(list, style) {
    currentStyle = style;
    this.clearList();
    sidebar.show();
    // Add a list entry for each of the items
    for (var idx in list) {
      this.addElement(list[idx]);
    }
    sidebarHandle.height(sidebar.height());
    setTimeout("CloudLab.Sidebar.collapse()", 2000);
  };

  this.expand = function() {
    sidebar.animate({ width: sidebarWidth }, 'fast');
    sidebarHandle.unbind('click');
    sidebarHandle.click(CloudLab.Sidebar.collapse);
  };

  this.collapse = function() {
    sidebar.animate({ width: sidebarHandle.width() }, 'fast');
    sidebarHandle.unbind('click');
    sidebarHandle.click(CloudLab.Sidebar.expand);
  };

  this.clearList = function() {
    sidebar.hide();
    sidebarList.children().remove();
    sidebarHandle.height(sidebar.height());
  };
};

/**
 * HUD Boxes
 * ---------
 * Draggable, movable, hidable boxes for showing different task modules
 */

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

/**
 * Workspace
 * ---------
 * Houses the main work area of CloudLab
 */

CloudLab.Workspace = new function() {
  var workspace = $('#Workspace');

  this.init = function() {
  };

  this.set = function(mode) {
    workspace.attr('src', mode + '.aspx');
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

/**
 * Main Initialization Functions
 */
$(function() {
  // Initialize the components
  CloudLab.UserBar.init();
  CloudLab.Sidebar.init();
  CloudLab.Workspace.init();

  $(window).bind('load resize', CloudLab.redisplay);
});