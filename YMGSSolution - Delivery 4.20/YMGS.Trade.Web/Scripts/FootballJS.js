var accordion = function (obj) {
    $(obj).click(function () {
        var btn = $(this).find('input');
        if (typeof (btn) !== 'undefined') {
            $(btn).toggleClass('marker-selected');
        }

        var arrowEle = $(this).children().children().children().first();
        if (arrowEle.prop('class') == 'close-arrow') {
            arrowEle.removeClass('close-arrow').addClass('open-arrow');
        }
        else {
            arrowEle.removeClass('open-arrow').addClass('close-arrow');
        }
        //$(this).next().toggleClass('marker-selected');
        if ($(this).next().prop('style')['display'] === 'none') {
            $(this).next().slideDown('slow');
        }
        else {
            $(this).next().slideUp('slow');
        }
    })
};
var accordionTop1 = function (obj, ctl) {
    $(obj).click(function () {
        //alert(ctl._autoRefresh);
        var div = $(this).find('div');
        //        if (typeof (div) !== 'undefined') {
        //            $(div).toggleClass('marker-selected');
        //        }
        if (typeof (div) !== 'undefined') {
            if ($(div).prop('class') == 'infoContainer') {
                $(div).removeClass('infoContainer').addClass('marker-selected');
            }
            else {
                $(div).removeClass('marker-selected').addClass('infoContainer');
            }
        }
        var arrowEle = $(this).children().children().children().first();
        if (arrowEle.prop('class') == 'arrow-close') {
            arrowEle.removeClass('arrow-close').addClass('arrow-open');
            ctl._autoRefresh = true;
        }
        else {
            arrowEle.removeClass('arrow-open').addClass('arrow-close');
            ctl._autoRefresh = false;
        }
        if ($(this).next().prop('style')['display'] === 'none') {
            $(this).next().show();
        }
        else {
            $(this).next().hide();
        }
        //如果打开折叠，更新内部数据
        if (ctl._autoRefresh == true) {
            ctl.refresh();
        }
    })
};

var alternatelyDisplay = function (obj) {
    $(obj).mouseover(function () {
        $(this).addClass('betContainermouseover');
    }).mouseout(function () {
        $(this).removeClass('betContainermouseover');
    })
}
