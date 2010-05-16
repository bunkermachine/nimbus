CloudLab = parent.CloudLab;

CloudLab.Overlay = new function() {
  var overlay = $('#ModalOverlay');
  var backdrop = overlay.children('.backdrop');
  var content = overlay.children('.content');

  this.init = function() {
  }

  this.show = function() {
    overlay.show('fast');
  }

  this.hide = function() {
    overlay.hide('fast');
  }
}

$(function() {
  CloudLab.Overlay.init();
  $('#ShowMapBtn').click(CloudLab.Overlay.show);
  // $("select, input:checkbox, input:radio, input:file").uniform();
});