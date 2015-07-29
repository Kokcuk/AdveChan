$(document).ready(function () {
    $("#btnUpload").click(function () {
        console.log("upl");
        var files = $("#pic").get(0).files;
        console.log(files);
        var data = new FormData();
        data.append("image", files[0]);
        var ajaxRequest = $.ajax({
            type: "POST",
            url: "/Pictures/LoadingPicture",
            contentType: false,
            processData: false,
            data: data,
            beforeSend: function (file, ext) {
                console.log("bedore");
            }
        });
        ajaxRequest.done(function (responseData, textStatus) {
            var currentVal = $("#imgsrc").val();
            $("#imgsrc").val(currentVal + ';' + responseData);
        });
    });
});