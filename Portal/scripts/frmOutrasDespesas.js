function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function validaCamposFormulario()
{    
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
   mensagemErro_ValidaCamposFormulario = "";
   var numAux = 0;
    var mensagem = "";

    if (ddlAcao.GetValue() == null)
    {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.frmOutrasDespesas_a_despesa_deve_ser_informada_;
    }

    if (txtQuantidade.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.frmOutrasDespesas_a_quantidade_deve_ser_informada_;
    }

    if (txtQuantidadeReal.GetVisible() && txtQuantidadeReal.GetEnabled() && txtQuantidadeReal.GetValue() == null) {
        numAux++;
        mensagem += "\n" + numAux + ") " + traducao.frmOutrasDespesas_a_quantidade_real_deve_ser_informada_;
    }
    
    if(mensagem != "")
    {
        mensagemErro_ValidaCamposFormulario = mensagem;
    }
    
    return mensagemErro_ValidaCamposFormulario;
}
function SalvarCamposFormulario()
{   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario()
{
    ddlAcao.SetValue(null);
    txtQuantidade.SetValue(null);
    txtQuantidadeReal.SetValue(null);
    txtValor.SetValue(null);
    
    desabilitaHabilitaComponentes();
}
    
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    grid.SelectRowOnPage(grid.GetFocusedRowIndex(), true);

    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
    {
        grid.GetSelectedFieldValues('CodigoItemOutraDespesa;Quantidade;QuantidadeReal;Valor;AcaoPlanejada', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(valores)
{
    var values = valores[0];
    desabilitaHabilitaComponentes();
    
    var acao = values[0];
    var quantidade = values[1];
    var quantidadeReal = values[2];
    var valor = values[3];
    var acaoPlanejada = (values[4] == 'S' || values[4] == 's');

    ddlAcao.SetValue(acao);
    txtQuantidade.SetValue(quantidade);
    txtQuantidadeReal.SetValue(quantidadeReal);
    txtValor.SetValue(valor);

    if (gvDados.cpExibirColunas)
        controlaCamposHabilitadorPrestacaoContas(acaoPlanejada);
}

function controlaCamposHabilitadorPrestacaoContas(acaoPlanejada) {
    ddlAcao.SetEnabled(!acaoPlanejada);
    txtQuantidade.SetEnabled(!acaoPlanejada);
    txtQuantidadeReal.SetEnabled(acaoPlanejada);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes()
{
    var habilitado = window.TipoOperacao && TipoOperacao != "Consultar";

    ddlAcao.SetEnabled(habilitado);
    txtQuantidade.SetEnabled(habilitado);
    txtQuantidadeReal.SetEnabled(habilitado);
    txtValor.SetEnabled(false);

    if (habilitado && gvDados.cpExibirColunas)
        controlaCamposHabilitadorPrestacaoContas(false);
}
//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    onClick_btnCancelar();
}

function IncluirOutrasDespesasPrestacaoContas() {
    popupMenu.Show();
}

function popupMenu_ItemClick(s, e) {
    switch (e.item.name) {
        case 'itemOutraDespesaNaoPlanejada':
            onClickBarraNavegacao('Incluir', gvDados, pcDados);
            break;
        case 'itemRegistrarOutrasDespesasPlanejadas':
            var cw = gvDados.cpCodigoWorkflow;
            var ci = gvDados.cpCodigoInstancia;
            var callbackFunc = function () {
                gvDados.Refresh();
                window.parent.atualizaValor();
            }
            window.top.showModal('../../TelasClientes/frmSelecaoDespesasPlanejadasPrestacaoContas.aspx?cw=' + cw + '&ci=' + ci, 'Selecionar ações planejadas', screen.width - 100, window.top.innerHeight - 150, callbackFunc);
            break;
    }
}