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
    prod.find('span').eq(1).html(produto.Formatado);

    home.hide();
    prod.show();

    setTimeout(function () {
        prod.hide();
        home.show();
    }, 2000);
};

var updateScreen = function (carrinho) {
    $('.total-produtos').find('span').eq(1).html(carrinho.Quantidade);
    $('.valor-total').find('span').eq(1).html(carrinho.Total);
};

$(function () {

    $('#iniciar-carrinho').click(function (event) {
        event.preventDefault();

        var botao1 = $(this);
        var botao2 = $('#finalizar-carrinho');
        var botao3 = $('#add-produto, #remove-produto');

        botao1.html('AGUARDE');
        botao1.addClass('disabled');

        $.ajax({
            type: 'POST',
            url: '/Carrinho/Abrir',
            success: function (response) {
                if (response.Status == 1) {
                    botao1.hide();
                    botao1.html("INICIAR");
                    botao1.removeClass("disabled");
                    botao2.show();
                    botao3.show();
                }
            }
        });

    });

    $('#finalizar-carrinho').click(function (event) {
        event.preventDefault();

        var botao1 = $(this);
        var botao2 = $('#iniciar-carrinho');
        var botao3 = $('#add-produto, #remove-produto');

        botao1.html('AGUARDE');
        botao1.addClass('disabled');

        $.ajax({
            type: 'POST',
            url: '/Carrinho/Finalizar',
            success: function (response) {
                if (response.Status == 1) {
                    botao1.hide();
                    botao1.html("FINALIZAR");
                    botao1.removeClass("disabled");
                    botao2.show();
                    botao3.hide();
                }
            }
        });
    });

    $('#add-produto').click(function (event) {
        event.preventDefault();
        getBarCode(function (barcode) {

            $.ajax({
                type: 'POST',
                url: '/Carrinho/Adicionar',
                data: {
                    barcode: barcode
                },
                success: function (response) {
                    console.log(response);
                    if (response.Status == 1) {
                        showProduto(response.Produto);
                        updateScreen(response.Carrinho);
                    } else {
                        showMessage("Produto não encontrado");
                    }
                }
            });

        });
    });

    $('#remove-produto').click(function () {
        event.preventDefault();
        getBarCode(function (barcode) {

            $.ajax({
                type: 'POST',
                url: '/Carrinho/Remover',
                data: {
                    barcode: barcode
                },
                success: function (response) {
                    console.log(response);
                    if (response.Status == 1) {
                        showProduto(response.Produto);
                        updateScreen(response.Carrinho);
                    } else {
                        showMessage("Produto não encontrado");
                    }
                }
            });

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
                    console.log(response);
                    if (response.Status == 1) {
                        showProduto(response.Produto);
                    } else {
                        showMessage("Produto não encontrado");
                    }
                }
            });

        });
    });

});