jQuery.validator.setDefaults({
    errorClass: 'is-invalid',
    validClass: 'is-valid',
    errorPlacement: function(error, element) {
        let parent = element.parent();
        let current =  parent.find("#" + error.attr('id'));

        if(current.length > 0){
            current.html(error.html());
        } else {
            let div = '<div id="' + error.attr('id') + '" class="invalid-feedback">' + error.html() + '</div>';
            parent.append(div);
        }
    },
    highlight: function(element, errorClass, validClass) {
        $(element).addClass(errorClass).removeClass(validClass);
        $(element).parent().find('label:last-child').addClass('invalid-feedback').removeClass('valid-feedback');
    },
    unhighlight: function(element, errorClass, validClass) {
        $(element).removeClass(errorClass).addClass(validClass);
        $(element).parent().find('label:last-child').removeClass('invalid-feedback').addClass('valid-feedback');
    }
});
