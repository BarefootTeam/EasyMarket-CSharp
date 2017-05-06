var getBarCode = function (callback) {

    var janela = $('.codigo-de-barras');
    var input = $('#codigo-de-barras');

    var close = function () {
        janela.hide();
        input.val(null);
        input.off();
    };

    janela.show();
    input.focus();

    input.on('keypress', function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            var value = input.val();
            close();
            callback.call(this, value);
        }
    });

    input.focusout(close);

};

$(function () {

    $('#add-produto').click(function (event) {
        event.preventDefault();
        getBarCode(function (barcode) {
            alert("Add: " + barcode);
        });
    });

    $('#remove-produto').click(function () {
        event.preventDefault();
        getBarCode(function (barcode) {
            alert("Remove: " + barcode);
        });
    });

    $('#busca-produto').click(function () {
        event.preventDefault();
        getBarCode(function (barcode) {
            alert("Buscar: " + barcode);
        });
    });

});