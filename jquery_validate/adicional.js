$.validator.addMethod('solonumerosletras', function (value, element, params) {
    return this.optional(element) || /^[a-zA-Z0-9ÁÉÍÓÚáéíóúñÑ .]+$/i.test(value);
}, "Por favor, Solo ingrese números y letras");

$.validator.addMethod('alfanumerico', function (value, element, params) {
    return this.optional(element) || /^[a-zA-Z0-9ÁÉÍÓÚáéíóúñÑ_.]+$/i.test(value);
}, "Por favor, Solo ingrese números y letras");

$.validator.addMethod('vaciohtml', function (value, element, params) {
    if(value === null)
        return false;
    value = value.replace(new RegExp('<p><br></p>', 'g'), '');
    value = value.trim();
    return this.optional(element) || value !== '';
}, "Este campo es obligatorio.");
