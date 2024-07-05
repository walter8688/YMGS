Type.registerNamespace('YMGS.Manage.Web.Controls');

YMGS.Manage.Web.Controls.AjaxCalendar = function(element){
    YMGS.Manage.Web.Controls.AjaxCalendar.initializeBase(this, [element]);
    
    this._changeHandler = null;
}

YMGS.Manage.Web.Controls.AjaxCalendar.prototype = {
  initialize: function() {
    YMGS.Manage.Web.Controls.AjaxCalendar.callBaseMethod(this, 'initialize');

    this._changeHandler = Function.createDelegate(this, this._onValueChange);
    $addHandler(this._txtDateHolder, 'change', this._changeHandler);
  },

  dispose: function() {
    if (this._changeHandler != null) {
      $removeHandler(this._txtDateHolder, 'change', this._changeHandler);
    }
    YMGS.Manage.Web.Controls.AjaxCalendar.callBaseMethod(this, 'dispose');
  },

  set_value: function(value) { this._txtDateHolder.value = value; },
  get_value: function() { return this._txtDateHolder.value; },

  _onValueChange: function(event) {
    var h = this.get_events().getHandler('change');
    if (h) h(this, Sys.EventArgs.Empty);
  },

  // [Obsolete("Use set_disabled() instead.")]
  disable: function(value) {
    this.set_disabled(true);
  },

  // [Obsolete("Use set_disabled() instead.")]
  enable: function() {
    this.set_disabled(false);
  },

  get_disabled: function() {
    return this._txtDateHolder.disabled;
  },
  set_disabled: function(value) {
    this._txtDateHolder.disabled = value;
    this._imgDatePopup.disabled = value;
  },

  add_change: function(handler) {
    this.get_events().addHandler('change', handler);
  },
  remove_change: function(handler) {
    this.get_events().removeHandler('change', handler);
  }
}

YMGS.Manage.Web.Controls.AjaxCalendar.registerClass('YMGS.Manage.Web.Controls.AjaxCalendar', Sys.UI.Control);

GenerateProps(YMGS.Manage.Web.Controls.AjaxCalendar, [
    'txtDateHolder',
    'imgDatePopup'
]);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
