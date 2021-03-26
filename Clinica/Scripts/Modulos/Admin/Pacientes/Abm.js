$(function () {
    //variables id con # y clase con .
    let form = $("#Form-Paciente");
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
                },
                'Email': {
                    'required': true,
                    'vaciohtml': true
                },
                'Numero_credencial': {
                    'required': true,
                    'solonumeroletras': true,
                    'vaciohtml': true,
                    'digits': true
                },
                
            }
        });
    };
    let form_submit = function () {
        return form.valid();
    }
    $(document).ready(document_ready);
    form.submit(form_submit);
});