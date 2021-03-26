$(function () {
    //variables id con # y clase con .
    let modal = $("#modal_profesional");
    let modal_titulo;
    let modal_cuerpo;
    //funciones
    let document_ready = function () {
        modal_titulo = modal.find("#titulo_modal");
        modal_cuerpo = modal.find("#cuerpo_modal");
    };

    let bonton_detalle_click = function () {

        let id = $(this).data('id');
        let nombre = $(this).data('nombre')

        $.ajax({
            url: "/admin/profesional/detalles/" + id,
            method: "GET",
            dataType: "html",
            success: function (data, status, xhr) {
                modal_cuerpo.html(data);
                modal_titulo.html('Detalle del Profesional ' + nombre);

                event.preventDefault();
                modal.modal();
            }
        });
    }

    $(".boton_detalle").click(bonton_detalle_click);

    $(document).ready(document_ready);
});