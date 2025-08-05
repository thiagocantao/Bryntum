var arrayCamposDisponiveis = [];
var codigoCampoSelecionado = -1;
var expressaoAvaliada = "";

function mostraEditorExpressao(CampoSelecionado)
{
    codigoCampoSelecionado = CampoSelecionado;
    arrayCamposDisponiveis = hfCamposDisponiveis.Get('arrayCamposDisponiveis');
    ppDvEditorExpressaoValidacao.Show();
}

function selecionaCampoClicado(sequencia)
{
    // obtem o nome do campo
    var codigoCampo = arrayCamposDisponiveis[sequencia][1];
    var nomeCampo = arrayCamposDisponiveis[sequencia][2];
    ConteudoExtenso = "[" + (sequencia + 1) + "!" + nomeCampo + "]";
    ConteudoCodificado = "[" + codigoCampo + "]";

    // obtem a posição do cursor e insere o título do campo
    var el = txtEdidorExpressao.GetInputElement();
    var start = el.selectionStart;
    var end = el.selectionEnd;
    var text = el.value;
    var before = text.substring(0, start);
    var after = text.substring(end, text.length);
    el.value = before + ConteudoExtenso + after;

    //  reposiciona o cursor dentro do campo "memo"
    el.selectionStart = el.selectionEnd = start + ConteudoExtenso.length;
    //txtEdidorExpressao.Focus();
}

function salvaExpressaoCampo()
{
    if (validaExpressaoCampo())
    {
        if (txtMensagemValidacao.GetText().trim() == "")
        {
            txtMensagemValidacao.Focus();
            window.top.mostraMensagem(traducao.uc_cfg_validacaoCampo_informe_a_mensagem_que_ser__apresentada_quando_a_express_o_n_o_for_verdadeira_, 'Atencao', true, false, null);
            return false;
        }
        loadPanel.Show();
        // envia comando para salvar a expressão no banco de dados
        hfCamposDisponiveis.Set("expressaoAvaliada", expressaoAvaliada);
        hfCamposDisponiveis.Set("expressaoExtenso", txtEdidorExpressao.GetText().toUpperCase());
        hfCamposDisponiveis.Set("mensagem", txtMensagemValidacao.GetText().trim());
        hfCamposDisponiveis.PerformCallback("Salva");
        return true;
    }
    return false;
}

function validaExpressaoCampo() {
    var expressao = txtEdidorExpressao.GetText().toUpperCase();

    // as quantidade de "[", "]" e "!" devem ser iguais e maior que zero para que exista campos na expressão
    var qtde_AbreCampo = expressao.split("[").length - 1;
    var qtde_FechaCampo = expressao.split("]").length - 1;
    var qtde_SeparaCampo = expressao.split("!").length - 1;

    // se a quantidade de caracteres é diferente, inválido
    if (qtde_AbreCampo != qtde_FechaCampo || qtde_FechaCampo != qtde_SeparaCampo)
        return false;

    // se existe campo
    if (qtde_AbreCampo > 0) {
        var campos = [];
        var expTemp = expressao;
        for (campo = 0; campo < qtde_AbreCampo; campo++) {
            campos[campo] = expTemp.substring(expTemp.indexOf('[') + 1, expTemp.indexOf(']'));
            // verifica se o campo existe na lista
            try{
                seqCampo = parseInt(campos[campo].replace("B", "").trim());
                // o seqCampo não pode ser maior que a quantidade disponível
                if (seqCampo > arrayCamposDisponiveis.length + 1)
                    return;
            }
            catch (err) {
                return false;
            }
            expTemp = expTemp.replace("[" + campos[campo] + "]", "1");
        }

        // se após a troca dos campos, sobrou algum dos caracteres "[.]", tem algo errado.
        if (expTemp.indexOf("[") >= 0 || expTemp.indexOf("!") >= 0 || expTemp.indexOf("]") >= 0)
            return false;

        // Avalia a expressão
        var result = false;
        try {
            result = eval(expTemp);
            result = result.toString() == "true" || result.toString() == "false";
        }
        catch (err) { }

        //// reescreve a expressão, trocando os nomes pelos códigos dos campos
        for (campo = 0; campo < campos.length; campo++) {
            var seqCampo = campos[campo].substring(0, campos[campo].indexOf("!"));
            codigoCampo = arrayCamposDisponiveis[seqCampo - 1][1];
            expressao = expressao.replace(campos[campo], codigoCampo);
        }

        // guarda a exoressao avaliada para ser utilizada pelos metodos "insereNovoRegistro()" e "atualizaRegistro()" 
        expressaoAvaliada = expressao;

        return result;
    }
}

