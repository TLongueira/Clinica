$(document).ready(function () {

    let atras_click = function () {
        window.history.go(-1);
    }

    $('.atras').click(atras_click);
    $('[data-toggle="tooltip"]').tooltip();
});