$(function () {
    let sucursal_localidad;

    let document_ready = function () {
        sucursal_localidad = $("#SucursalLocalidad");
    };

    let provincia_change = function () {
        let value = $(this).val();
        sucursal_localidad.html("");
        $.getJSON("/Admin/Clinicas/getLocalidades/" + value, function (data) {
            let html = "";

            $.each(data, function (index, value) {
                html += '<option value="' + value.LocalidadId + '"> ' + value.Descripcion + "</option>";
            });

            sucursal_localidad.html(html);
        });
    };

    //Declaremos eventos
    $(document).ready(document_ready);
    $("#Provincia").change(provincia_change);
});