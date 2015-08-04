function autoclick() {
    var nickName = $("#txtUserName").val();
    var href = "/Chat?member=" + encodeURIComponent(nickName);
    href = href + "&logOn=true";
    $("#LoginButton").attr("href", href).click();
    $("#Username").text(nickName);
}

$(document).ready(autoclick());
function LoginOnSuccess(result) {

    Scroll();
    ShowLastRefresh();

    setTimeout("Refresh();", 5000);

    $('#txtMessage').keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#btnMessage").click();
        }
    });

    $("#btnMessage").click(function () {
        var text = $("#txtMessage").val();
        if (text) {

            var href = "/Chat?member=" + encodeURIComponent($("#Username").text());
            href = href + "&chatMessage=" + encodeURIComponent(text);
            $("#ActionLink").attr("href", href).click();
        }
    });

    $("#btnLogOff").click(function () {

        var href = "/Chat?member=" + encodeURIComponent($("#Username").text());
        href = href + "&logOff=true";
        $("#ActionLink").attr("href", href).click();

        document.location.href = "Index";
    });
}

function LoginOnFailure(result) {
    $("#Username").val("");
    $("#Error").text(result.responseText);
    setTimeout("$('#Error').empty();", 2000);
}

function Refresh() {
    var href = "/Chat?member=" + encodeURIComponent($("#Username").text());

    $("#ActionLink").attr("href", href).click();
    setTimeout("Refresh();", 5000);
}

function ChatOnFailure(result) {
    $("#Error").text(result.responseText);
    setTimeout("$('#Error').empty();", 2000);
}

function ChatOnSuccess(result) {
    Scroll();
    ShowLastRefresh();
}

function Scroll() {
    var win = $('#Messages');
    var height = win[0].scrollHeight;
    win.scrollTop(height);
}

function ShowLastRefresh() {
    var dt = new Date();
    var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
    $("#LastRefresh").text("Last refresh was in " + time);
}