$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('#scrollToTopBtn').fadeIn();
        } else {
            $('#scrollToTopBtn').fadeOut();
        }
    });
    $('#scrollToTopBtn').click(function () {
        $('body,html').animate({
            scrollTop: 0
        }, 400);
        return false;
    });
});