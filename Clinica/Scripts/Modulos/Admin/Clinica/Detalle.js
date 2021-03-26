$(function () {
   
    let document_ready = function ()
    {

    }
    let agregar_sucursal_click = function (event)
    {
        event.preventDefault();

        $("#modal_sucursal").modal();
    }

    //declaremos eventos
    $(document).ready(document_ready);
    $("#agregar_sucursal").click(agregar_sucursal_click);
});