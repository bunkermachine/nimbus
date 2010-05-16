/**
 * Website Interactions
 * --------------------
 * Controls most of the major page changes
 */
CloudLab = new function() {
  this.redisplay = function() {
    $('#Content').width($(window).width() - 24).height($(window).height() - 67);
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

  this.init = function() {
  };

  this.addProject = function(project) {
  };

  this.setProject = function(projectName) {
    userBar.show();
    projectTitle.text(projectName);
  };
};

/**
 * Sidebar
 * ---------
 * Bundles the sidebar
 */

CloudLab.Sidebar = new function() {
  var sidebar = $('#Sidebar');
  var sidebarTemplates = sidebar.children('.templates');
  var sidebarContent = sidebar.children('.content');
  var sidebarHandle = sidebar.children('.handle');
  var sidebarWidth = 0;

  this.init = function() {
    this.clearList();
    sidebarWidth = sidebar.width();
    sidebar.resizable({ handles: 'w', maxWidth: 300, stop: function(event, ui) {
      sidebarWidth = sidebar.width();
    } });
    sidebarHandle.click(CloudLab.Sidebar.collapse);

    $('#NewProjectBtn').click(function() {
      CloudLab.Workspace.set('ViewProjects');
    });

    $('#NewTaskBtn').click(function() {
      CloudLab.Workspace.set('NewTask');
    });
  }

  this.refresh = function() {
    __doPostBack('SidebarPanel','');
  }

  this.addTasks = function(task) {
    var element = this.createElement(task, 'Basic');
    $('#SidebarTasks').append(sidebarListElement);
    this.preview();
  }

  this.addElement = function(element, style) {
    var template = sidebarTemplates.children('#SidebarTemplate' + style);
    var sidebarListElement = template.clone();
    sidebarListElement.removeAttr('id');
    sidebarListElement.children('.title').text(element['title']);
    sidebarListElement.children('.description').text('Status: ' + element['description'] + ' (' + element['progress'] + '%)');
    sidebarListElement.click(element['click']);
    return sidebarListElement;
  }

  this.initList = function(list, style) {
    this.clearList();
    sidebar.show();
    // Add a list entry for each of the items
    for (var idx in list) {
      this.addElement(list[idx], style);
    }
    sidebarHandle.height(sidebar.height());
    this.preview();
  };

  this.expand = function() {
    sidebar.width(sidebarHandle.width());
    sidebar.animate({ width: sidebarWidth }, 'fast');
    $('#Workspace').animate({ marginRight: sidebarWidth }, 'fast');
    sidebarHandle.unbind('click');
    sidebarHandle.click(CloudLab.Sidebar.collapse);
  };

  this.collapse = function() {
    sidebar.animate({ width: sidebarHandle.width() }, 'fast');
    $('#Workspace').animate({ marginRight: sidebarHandle.width() }, 'fast');
    sidebarHandle.unbind('click');
    sidebarHandle.click(CloudLab.Sidebar.expand);
  };

  this.preview = function() {
    this.expand();
    setTimeout(this.collapse, 2000);
  };

  this.clearList = function() {
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
  var workspace = $('[name=Workspace]');

  this.init = function() {
    workspace.bind('load', function() {
      CloudLab.Sidebar.refresh();
    });
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