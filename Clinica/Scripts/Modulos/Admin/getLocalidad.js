$(function () {
    let sucursal_localidad;

    let document_ready = function () {
        sucursal_localidad = $("#LocalidadId");
        sucursal_localidad.chosen();
        $("#ProvinciaId").chosen();
    };

    let provincia_change = function () {
        let value = $(this).val();
        sucursal_localidad.html("<option value>Seleccione una Localidad</option>");
        $.getJSON("/Admin/Ajax/getLocalidad/" + value, function (data) {
            let html = "";

            $.each(data, function (index, value) {
                html += '<option value="' + value.LocalidadId + '"> ' + value.Descripcion + "</option>";
            });

            sucursal_localidad.html(html);
            sucursal_localidad.trigger("chosen:updated");
        });
    };
    
    //Declaremos eventos
    $(document).ready(document_ready);
    $("#ProvinciaId").change(provincia_change);
});