$(function () {
    //variables id con # y clase con .
    let form = $("#Login-form");
    //funciones
    let document_ready = function () {
        form.validate({
            rules: {
                '#dato': {
                    'required': true,
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