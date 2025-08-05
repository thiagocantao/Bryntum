// ---- Provavelmente não será necessário alterar as duas próximas funções
var teste;
function SalvarCamposFormulario()
{
    // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado()
{
    // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar","0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function validaCamposFormulario()
{
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
        
    return mensagemErro_ValidaCamposFormulario;
}

function LimpaCamposFormulario()
{
  
    // Função responsável por preparar os campos do formulário para receber um novo registro
    dteData.SetValue(null);
    txtIncluidaPor.SetText("");
    txtTipo.SetText("");
    txtAssunto.SetText("");
    txtProjeto.SetText("");
    txtLicao.SetText("");
}

function OnGridFocusedRowChanged(grid, forcarMontaCampos) 
{
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible()))
       grid.GetRowValues(grid.GetFocusedRowIndex(), 'cla;data;tipo;assunto;projeto;licao;IncluidoPor;', MontaCamposFormulario);
}

function MontaCamposFormulario(values)
{
//    if (window.TipoOperacao &&  TipoOperacao == "Incluir")
//        return;
        
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
    
    LimpaCamposFormulario();
    
    
    
    if(values[1] == null)
        dteData.SetText("");    
   else
        dteData.SetValue(values[1]);
   
    txtIncluidaPor.SetText(values[6] != null ? values[6].toString() : "");
    
    txtTipo.SetText(values[2] != null ? values[2].toString() : "");
    
    txtAssunto.SetText(values[3] != null ? values[3].toString() : "");
    
    txtProjeto.SetText(values[4] != null ? values[4].toString() : "");
    
    txtLicao.SetText(values[5] != null ? values[5].toString() : "");
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------
function posSalvarComSucesso()
{
    /*
    if (TipoOperacao == "Editar")
    {
        btnRelatorioRisco.SetClientVisible(true);
        window.top.mostraMensagem("As informações foram salvas com sucesso.", 'Atencao', false, false, null);
        
    }
   */
}

function mostrarRelatorioLicoesAprendidas()
{   
    var strUrl = "../Relatorios/relatorioLicoesAprendidas.aspx?";
    
    strUrl += "CLA=" + gvDados.GetRowKey(gvDados.GetFocusedRowIndex());
    window.top.showModal(strUrl, traducao.licoesAprendidas_li__es_aprendidas, screen.width - 60, screen.height - 250, '', null);
}

function relLicoesAprendidasMenuProjRelatorios(urlDaImagem) {
    var strUrl = "../Relatorios/popupRelLicoesAprendidas.aspx";
    var now = new Date();
    var dataFormatada = now.getDate() + "/" + now.getMonth() + "/" + now.getFullYear() + " " + now.getHours() + ":" + now.getMinutes();
    strUrl += "?DT=" + dataFormatada;
    window.top.showModal(strUrl, traducao.licoesAprendidas_li__es_aprendidas, screen.width - 60, screen.height - 250, '', null);
}
