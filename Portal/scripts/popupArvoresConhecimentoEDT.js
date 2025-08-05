var comando;
var modo = '';
function editar(chave) {
    tlArvore.GetNodeValues(chave, "CodigoElementoArvore;IndicaElementoFolha;DescricaoElementoArvore;NivelCriticidadeConhecimento;DescricaoNIvelCriticidade;IndicaPodeExcluir;CodigoEstruturaAnalitica", function (valores) {
        hfGeral.Set('modo', 'E');
        var CodigoElementoArvore = (valores[0] == null) ? "" : valores[0].toString();
        var IndicaElementoFolha = (valores[1] == null) ? "" : valores[1].toString();
        var DescricaoElementoArvore = (valores[2] == null) ? "" : valores[2].toString();
        var NivelCriticidadeConhecimento = (valores[3] == null) ? "" : valores[3].toString();
        var DescricaoNIvelCriticidade = (valores[4] == null) ? "" : valores[4].toString();
        var IndicaPodeExcluir = (valores[5] == null) ? "" : valores[5].toString();
        var CodigoEstruturaAnalitica = (valores[6] == null) ? "" : valores[6].toString();

        txtDescricaoElementoArvore.SetText(DescricaoElementoArvore);
        radioIndicaElementoFolha.SetValue(IndicaElementoFolha);
        comboCriticidade.SetValue(NivelCriticidadeConhecimento);
        comboCriticidade.SetText(DescricaoNIvelCriticidade);

        //Se a coluna IndicaPodeExcluir = "N"  OU a coluna CodigoEstruturaAnalitica = 1 então impedir alteração.
        if (IndicaPodeExcluir === 'N' || CodigoEstruturaAnalitica === '1') {
            txtDescricaoElementoArvore.SetEnabled(false);
            radioIndicaElementoFolha.SetEnabled(false);
            comboCriticidade.SetEnabled(false);
            btnSalvar.SetEnabled(false);
        }
        else {
            txtDescricaoElementoArvore.SetEnabled(true);
            radioIndicaElementoFolha.SetEnabled(true);
            comboCriticidade.SetEnabled(true);
            btnSalvar.SetEnabled(true);
        }
        pcDados.Show();
    });
}

function processaInclusao() {
    hfGeral.Set('modo', 'I');
    txtDescricaoElementoArvore.SetText('');
    radioIndicaElementoFolha.SetSelectedIndex(-1);

    comboCriticidade.SetValue(null);

    txtDescricaoElementoArvore.SetEnabled(true);
    radioIndicaElementoFolha.SetEnabled(true);
    comboCriticidade.SetEnabled(true);
    btnSalvar.SetEnabled(true);

    divLabelCriticidade.style.display = 'none';
    divComboCriticidade.style.display = 'none';


    pcDados.Show();
}

function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaTela() {
    debugger
    var mensagemError = "";
    var numError = 0;
    if (Trim(txtDescricaoElementoArvore.GetText()) == "") {
        mensagemError += ++numError + ") Elemento deve ser informado\n";
    }    
    if (radioIndicaElementoF3olha.GetValue() == 'S')
    {
        if (comboCriticidade.GetValue() == null) {
            mensagemError += ++numError + ") Criticidade deve ser informada\n";
        }
    }
    return mensagemError;
}

function excluir(chave) {
    tlArvore.DeleteNode(chave);
}