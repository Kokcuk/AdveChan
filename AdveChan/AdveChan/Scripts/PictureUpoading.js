$("#btnUpload").click(function () {
    function shipOff(event) {
        var result = event.target.result;
        $.post('/Pictures/LoadingPicture', { image: result }, function () { });
       // $.get('/StartPage/ShowStartPage/', function() {}); //- функция для теста кнопки, тоже нихуя не работает.
    }

    var file = document.getElementById('picture').files[0];
    var reader = new FileReader();
    reader.readAsText(file, 'UTF-8');
    reader.onload = shipOff;
});