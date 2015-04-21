var timeout = 500;
var closetimer = 500;
var ddmenuitem = 0;

function menubar_open() {
    menubar_canceltimer();
    menubar_close();
    ddmenuitem = $(this).find('ul').eq(0).css('visibility', 'visible');
}

function menubar_close()
{ if (ddmenuitem) ddmenuitem.css('visibility', 'hidden'); }

function menubar_timer()
{ closetimer = window.setTimeout(menubar_close, timeout); }

function menubar_canceltimer() {
    if (closetimer) {
        window.clearTimeout(closetimer);
        closetimer = null;
    }
}
$(document).ready(function() {
    $('#menubar > li').bind('mouseover', menubar_open);
    $('#menubar > li').bind('mouseout', menubar_timer);
});
document.onclick = menubar_close;