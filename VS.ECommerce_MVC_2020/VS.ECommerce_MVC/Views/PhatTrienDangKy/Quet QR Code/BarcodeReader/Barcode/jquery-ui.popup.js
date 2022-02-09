(function ($) {
    $.fn.extend({
        center: function () {
            return this.each(function () {
                var top = ($('.over-popup').height() - $(this).outerHeight(false)) / 2;
                var left = ($('.over-popup').width() - $(this).outerWidth(false)) / 2;
                $(this).css({
                    position: ($(window).width() < 768) ? 'sticky' : 'fixed',
                    top: (top > 0 ? top : 0) + 'px',
                    left: (left > 0 ? left : 0) + 'px',
                    margin: 0
                });
            });
        }
    });
})(jQuery);
$(document).ready(function () {
    $(window).on('resize', function () {
        $('.auto-resize').center();
    });
});
function js_ShowPopup(id) {
    //$('.main-popup').draggable({ handle: '.title-popup' });
    $('.over-popup').fadeIn('slow');
    $('#' + id).fadeIn('slow');
    $('#' + id).center();
    $(document).on('keydown', function (e) {
        e = window.event || e;
        var keyCode = (e.which) ? e.which : e.keyCode;
        if (keyCode === 27) js_ClosePopup(id);
    });
}
function js_ClosePopup(id) {
    $('.over-popup').fadeOut('slow');
    $('.main-popup').fadeOut('slow');
    $('#' + id).fadeOut('slow');
    $('.print').fadeOut('slow');
    $(document).off('keydown');
}