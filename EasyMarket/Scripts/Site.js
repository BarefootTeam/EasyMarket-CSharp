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

var showMessage = function (message) {
    var actual = $('.screen > div:visible');
    var messagebox = $('.screen .mensagem');

    messagebox.find('span').html(message);

    if (actual.length > 0) {
        actual.hide();
    }

    messagebox.css('display', 'flex');

    setTimeout(function () {
        messagebox.hide();

        if (actual.length > 0) {
            actual.show();
        }

    }, 3000);
};

var showProduto = function (produto) {
    var home = $('.home');
    var prod = $('.produto');

    prod.find('span').eq(0).html(produto.Nome);
    prod.find('span').eq(1).html(produto.PrecoCusto);

    home.hide();
    prod.show();

    setTimeout(function () {
        prod.hide();
        home.show();
    }, 3000);
};

$(function () {

    $('#iniciar-carrinho').click(function (event) {
        event.preventDefault();

        var botao1 = $(this);
        var botao2 = $('#finalizar-carrinho');

        botao1.html('AGUARDE');
        botao1.addClass('disabled');

        $.ajax({
            type: 'POST',
            url: '/Carrinho/Abrir',
            success: function (response) {
                if (response.Status == 1) {
                    botao1.hide();
                    botao2.show();
                }
            }
        });

    });

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

            $.ajax({
                type: 'POST',
                url: '/Carrinho/Buscar',
                data: {
                    barcode: barcode
                },
                success: function (response) {
                    showProduto(response);
                }
            });

        });
    });

});