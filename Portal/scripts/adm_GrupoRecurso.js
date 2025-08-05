/*
OBSERVAÇÕES
  
MUDANÇA
04/02/2011 - Alejandro: 
a.- "Não se pode alterar o tipo de um grupo se o grupo tiver 'filhos'"
função [function MontaCamposFormulario(values)] adiciono a linha
ddlTipoRecurso.PerformCallback(codigoTipoRecurso + ";" + codigoGrupoRecurso);

*/

function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    if (txtNome.GetText() == "") {
        mensagemErro_ValidaCamposFormulario = traducao.adm_GrupoRecurso_nome_do_grupo_de_recurso_deve_ser_informado_;
    }
    else if (ddlTipoRecurso.GetSelectedIndex() == -1) {
        mensagemErro_ValidaCamposFormulario = traducao.adm_GrupoRecurso_tipo_de_recurso_deve_ser_informado_;
    }
    return mensagemErro_ValidaCamposFormulario;
}
function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
    // o método será chamado por meio do objeto pnCallBack
    hfGeral.Set("StatusSalvar", "0");
    pnCallback.PerformCallback(TipoOperacao);
    return false;
}

// **************************************************************************************
// - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
// **************************************************************************************

function LimpaCamposFormulario() {
    txtNome.SetText("");
    txtNome.cpCodigoGrupoRecurso = "-1";
    txtDetalhe.SetText("");
    txtDetalhe.Validate();
    desabilitaHabilitaComponentes();

    ddlGrupoRecursoSuperior.SetSelectedIndex(-1);

    ddlTipoRecurso.SetSelectedIndex(-1);

    txtUnidadeMedida.SetText('');
    txtValorHora.SetText('');
    txtValorUso.SetText('');

    ddlGrupoRecursoSuperior.PerformCallback('-1;-1;');
}

// Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
    if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoGrupoRecurso;DescricaoGrupo;DetalheGrupo;GrupoRecursoSuperior;NivelGrupo;CodigoEntidade;CodigoTipoRecurso;CustoHora;CustoUso;CustoHoraExtra;UnidadeMedida;DescricaoGrupoSuperior', MontaCamposFormulario);
    }
}

function MontaCamposFormulario(values) {
    
    desabilitaHabilitaComponentes();

    var codigoGrupoRecurso = (values[0] != null ? values[0] : "");
    var descricaoGrupo = (values[1] != null ? values[1] : "");
    var detalheGrupo = (values[2] != null ? values[2] : "");
    var CodigoGrupoSuperior = (values[3] != null ? values[3] : "");
    var nivelGrupo = (values[4] != null ? values[4] : "");
    var codigoEntidade = (values[5] != null ? values[5] : "");
    var codigoTipoRecurso = (values[6] != null ? values[6] : "");
    var custoHora = (values[7] != null ? values[7] : "");
    var custoUso = (values[8] != null ? values[8] : "");
    var custoHoraExtra = (values[9] != null ? values[9] : "");
    var unidadeMedida = (values[10] != null ? values[10] : "");
    var descricaoGrupoSuperior = (values[11] != null ? values[11] : "");
    
    
   // var codigoGrupoRecurso = (values[0] != null ? values[0] : "");
   // var CodigoGrupoSuperior = (values[3] != null ? values[3] : "");
  //  var codigoTipoRecurso = (values[6] != null ? values[6] : "");

    var parametro = codigoTipoRecurso + ";" + codigoGrupoRecurso + ";" + CodigoGrupoSuperior;

    // cpCodigoGrupoRecurso será usado no SelectedIndexChanged() do ddlTipoRecurso;
    txtNome.cpCodigoGrupoRecurso = codigoGrupoRecurso;
    txtNome.SetText(descricaoGrupo);
    txtDetalhe.SetText(detalheGrupo);
    txtDetalhe.Validate();
    ddlTipoRecurso.SetValue(codigoTipoRecurso);
    if (TipoOperacao != "Consultar") {
        if ("Financeiro" == ddlTipoRecurso.GetText()) {
            txtValorHora.SetEnabled(false);
            txtValorUso.SetEnabled(false);
            txtUnidadeMedida.SetEnabled(false);
        }
        else if ("Equipamento" == ddlTipoRecurso.GetText()) {
            txtValorHora.SetEnabled(true);
            txtValorUso.SetEnabled(true);
            txtUnidadeMedida.SetEnabled(true);
        }
        else {
            txtValorHora.SetEnabled(true);
            txtValorUso.SetEnabled(true);
            txtUnidadeMedida.SetEnabled(false);
        }
    }

    txtUnidadeMedida.SetText(unidadeMedida);
    txtValorHora.SetText(custoHora);
    txtValorUso.SetText(custoUso);
    ddlGrupoRecursoSuperior.PerformCallback(parametro);
}

// ---------------------------------------------------------------------------------
// Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
// ---------------------------------------------------------------------------------

function desabilitaHabilitaComponentes() {
    var habilitado = window.TipoOperacao && TipoOperacao != "Consultar";
    txtNome.SetEnabled(habilitado);
    ddlGrupoRecursoSuperior.SetEnabled(habilitado);
    ddlTipoRecurso.SetEnabled(habilitado);
    txtDetalhe.SetEnabled(habilitado);
    txtUnidadeMedida.SetEnabled(habilitado);
    txtValorHora.SetEnabled(habilitado);
    txtValorUso.SetEnabled(habilitado);
}