//// JScript File
//// ---- Provavelmente não será necessário alterar as duas próximas funções
//function SalvarCamposFormulario()
//{
//    return false;
//}

//function ExcluirRegistroSelecionado()
//{
//    return false;
//}


//// **************************************************************************************
//// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
//// **************************************************************************************

//function validaCamposFormulario()
//{
//    // Esta função tem que retornar uma string.
//    // "" se todas as validações estiverem OK
//    // "<erro>" indicando o que deve ser corrigido
//    mensagemErro_ValidaCamposFormulario = "";
//    
//    return mensagemErro_ValidaCamposFormulario;
//}

function LimpaCamposFormulario() {
    
    txtIndicador.SetText('');
    try {
        txtIndicadorDado.SetText('');

        txtAgrupamento.SetText('');

        txtPolaridade.SetText('');
        
        txtResponsavel.SetText('');
        txtUnidadeMedida.SetText('');
        txtPeriodicidade.SetText('');
        txtMeta.SetText('');
        txtValorMeta.SetText('');
        txtFonte.SetText('');
        
    } catch (e) { }
}


function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
        grid.GetRowValues(grid.GetFocusedRowIndex(),
                'CodigoIndicador;NomeIndicador;Agrupamento;Polaridade;NomeUsuario;SiglaUnidadeMedida;DescricaoPeriodicidade_PT;Meta;MetaNumerica;FonteIndicador', MontaCamposFormulario);

}

function MontaCamposFormulario(valores) {
    var indicador = valores[1];
    var agrupamento = valores[2];
    var polaridade = valores[3];
    var nomeResponsavel = valores[4];
    var unidadeMedida = valores[5];
    var periodicidade = valores[6];
    var meta = (valores[7] == null ? "" : valores[7]);
    var valorMeta = (valores[8] == null ? "" : valores[8].toString().replace('.', ','));
    var FonteIndicador = valores[9];

    txtIndicador.SetText(indicador);
    try {
        txtIndicadorDado.SetText(indicador);

        txtAgrupamento.SetText(agrupamento);

        //generar o mensagem para polaridade.
        if (polaridade == "POS") {
            txtPolaridade.SetText(traducao.AtualizacaoResultadosProjeto_quanto_maior__melhor);
        }
        if (polaridade == "NEG") {
            txtPolaridade.SetText(traducao.AtualizacaoResultadosProjeto_quanto_maior__pior);
        }
        txtResponsavel.SetText(nomeResponsavel);
        txtUnidadeMedida.SetText(unidadeMedida);
        txtPeriodicidade.SetText(periodicidade);
        txtMeta.SetText(meta);
        txtValorMeta.SetText(valorMeta);
        txtFonte.SetText(FonteIndicador);

        if ((periodicidade == "Semanal") || (periodicidade == "Diária")) {
            document.getElementById('tdPeriodo').style.display = "block";
            pnCallbackPeriodos.PerformCallback();
        }
        else {
            document.getElementById('tdPeriodo').style.display = "none";
            try {
                gvResultados.PerformCallback();
            } catch (e) { }
        }
    } catch (e) { }
}


// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
//function posSalvarComSucesso()
//{   
//}


// --------------------------------------------------------------------------------
// Escreva aqui as novas funções que forem necessárias para o funcionamento da tela
// --------------------------------------------------------------------------------

/*-------------------------------------------------------------------
    Función: OnClick_CustomEditarGvDado(s, e)
    Parámetro: .
    retorno: void.
    Descripción: Iniciar ações de edição da linha da grig gvDados.
-------------------------------------------------------------------*/
function OnClick_CustomEditarGvDado(s, e) {
    OnGridFocusedRowChanged(s, true);
    pcDados.Show();


}
/*-------------------------------------------------------------------
    Función: lancarMensagemSistema(mensagem)
    Parámetro: cadena de texto.
    retorno: void.
    Descripción: Exibe panelControl com o texto como parâmetro.
                 O parâmetro e usado para exibir mensagens 
                 informativas ao usuário.
-------------------------------------------------------------------*/
function lancarMensagemSistema(mensagem) {
    lblMensagemVarios.SetText(mensagem);
    pcMensagemVarios.Show();
}


function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 60;
    gvDados.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}