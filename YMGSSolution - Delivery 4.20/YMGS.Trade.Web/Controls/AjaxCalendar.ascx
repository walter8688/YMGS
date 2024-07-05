<%@ Control Language="C#" AutoEventWireup="true" Codebehind="AjaxCalendar.ascx.cs"
    Inherits="YMGS.Manage.Web.Controls.AjaxCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<ajaxToolkit:FilteredTextBoxExtender FilterType="Custom, Numbers" ValidChars="-"
 runat="server" TargetControlID = "txtDateHolder"   FilterMode="ValidChars"  />
<asp:TextBox CssClass="TextBox" ID="txtDateHolder" runat=server MaxLength="10"></asp:TextBox><asp:ImageButton ID="imgDatePopup" ImageUrl="~/Images/AjaxCalendar.png" runat="server" CausesValidation="false" OnClientClick="return false;" />
<ajaxToolkit:CalendarExtender ID="calendarExtender" runat="server" TargetControlID="txtDateHolder" Format="yyyy-mm-dd" 
    FirstDayOfWeek="Monday"
    PopupButtonID="imgDatePopup" />
<script type="text/javascript" language="javascript">
  function OnClientDateSelectionChangedHandler(calendar) {
    calendar._onValueChange();
  }
</script>

