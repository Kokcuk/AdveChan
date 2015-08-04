$(document).ready(function() {
    $("#scroll").click(function() {
        var elementClick = $(this).attr("href");
        var destination = $(elementClick).offset().top;
        if ($.browser.safari) {
            $('body').animate({ scrollTop: destination }, 1100); //1100 - скорость
        } else {
            $('html').animate({ scrollTop: destination }, 1100);
        }
        return false;
    });
    $('a').hover(function () {
        var elementId = $(this).attr("href");
        var elementToClone = $(this).attr("href") + "s";
        $(elementId).clone().prependTo(elementToClone);
        $(elementToClone).show();
    },
function () {
    var elementToClone = $(this).attr("href") + "s";
    $(elementToClone).hide();
    $(elementToClone).empty();
});
});
