$(function() {
    //variables id con # y clase con .
    let form = $("#form_profesional");
    //funciones
    let document_ready = function () {
        form.validate({
            rules: {
                'Persona.Nombre': {
                    'required': true,
                    'solonumerosletras': true,
                    'vaciohtml': true
                },
                'Persona.Apellido': {
                    'required': true,
                    'solonumerosletras': true,
                    'vaciohtml': true
                },
                'Persona.Documento': {
                    'required': true,
                    'solonumerosletras': true,
                    'vaciohtml': true,
                    'digits': true
                },
                'MatriculaNacional': {
                    'required': true,
                    'solonumerosletras': true,
                    'vaciohtml': true
                },
                'MatriculaProvincial': {
                    'required': true,
                    'solonumeroletras': true,
                    'vaciohtml': true
                }
            }
        });
    };
    let form_submit = function () {
        return form.valid();
    }
    $(document).ready(document_ready);
    form.submit(form_submit);
});