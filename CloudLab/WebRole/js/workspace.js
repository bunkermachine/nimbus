CloudLab = parent.CloudLab;

CloudLab.Overlay = new function() {
  var overlay = $('#ModalOverlay');
  var backdrop = $('#ModalOverlayBackdrop');

  this.init() = function() {
  }

  this.show() = function() {
    overlay.animate({ opacity: 1 }, 'fast');
  }

  this.hide() = function() {
    overlay.animate({ opacity: 0 }, 'fast');
  }
}

$(function() {
  CloudLab.Overlay.init();
  // $("select, input:checkbox, input:radio, input:file").uniform();
});