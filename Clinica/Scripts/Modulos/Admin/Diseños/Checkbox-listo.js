        $(function () {
            $('.button-checkbox').each(function () {

                // Seteo
                var $widget = $(this),
                    $button = $widget.find('button'),
                    $checkbox = $widget.find('input:checkbox'),
                    color = $button.data('color'),
                    settings = {
                        on: {
                            icon: 'glyphicon glyphicon-check'
                        },
                        off: {
                            icon: 'glyphicon glyphicon-unchecked'
                        }
                    };

                // manejador de eventos
                $button.on('click', function () {
                    $checkbox.prop('checked', !$checkbox.is(':checked'));
                    $checkbox.triggerHandler('change');
                    updateDisplay();
                });
                $checkbox.on('change', function () {
                    updateDisplay();
                });

                // acciones
                function updateDisplay() {
                    var isChecked = $checkbox.is(':checked');

                    // Setear el boton de estado
                    $button.data('state', (isChecked) ? "on" : "off");

                    // setear el icono
                    $button.find('.state-icon')
                        .removeClass()
                        .addClass('state-icon ' + settings[$button.data('state')].icon);

                    // cambiar el color
                    if (isChecked) {
                        $button
                            .removeClass('btn-default')
                            .html(txt + 'Activo') 
                            .addClass('btn-' + color + ' active');
                    }
                    else {
                        $button
                            .removeClass('btn-' + color + ' active')
                            .html(txt + 'Inativo') 
                            .addClass('btn-default');
                    }
                }

                // inicializacion 
                function init() {

                    updateDisplay();

                    // meter el icono
                    if ($button.find('.state-icon').length == 0) {
                        $button.prepend('<i class="state-icon ' + settings[$button.data('state')].icon + '"></i> ');
                    }
                }
                init();
            });
        });