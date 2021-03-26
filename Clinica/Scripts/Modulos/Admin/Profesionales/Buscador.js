$(function () {
    //Declarar variables
    let filtro = $("#filtro");

    //Declarar Funciones 
    let document_ready = function () {

    };

    let btnBuscar_click = function () {
        var buscar = filtro.val();

        if (buscar !== '') {
            window.location = '/Admin/Profesional?filtro=' + buscar;
        }
    };

    let btnReload_click = function () {
        window.location = '/Admin/Profesional';
    };

    filtro_keypress = function (event) {
        if (event.which === 13) {
            btnBuscar_click();
        }
    };

    //Eventos
    $(document).ready(document_ready);
    $("#btnBuscar").click(btnBuscar_click);
    $("#btnReload").click(btnReload_click);
    filtro.keypress(filtro_keypress);
});