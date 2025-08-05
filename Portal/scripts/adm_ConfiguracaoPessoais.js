// JScript File
function validaCamposFormulario() {
    // Esta função tem que retornar uma string.
    // "" se todas as validações estiverem OK
    // "<erro>" indicando o que deve ser corrigido
    mensagemErro_ValidaCamposFormulario = "";
    var countMsg = 0;
    //Se o usuário não tem permissão de acesso em nenhuma entidade, retorna essa mensagem senão, segue o procedimento de validação
    //tradicionalmente adotado.
    if (callbackGeralConfig.cp_IndicaPermissaoAcessoEntidade != null && callbackGeralConfig.cp_IndicaPermissaoAcessoEntidade == "N") {
        mensagemErro_ValidaCamposFormulario = traducao.adm_ConfiguracaoPessoais_n_o___poss_vel_salvar_pois_este_usu_rio_n_o_possui_permiss_o_de_acesso_a_nenhuma_entidade;
    }
    else {

        if (ddlEntidade.GetValue() == null || ddlEntidade.GetText() == "")
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.adm_ConfiguracaoPessoais_a_entidade_deve_ser_informada_ + "\n";

        if (ddlTelaInicial.GetValue() == null || ddlTelaInicial.GetText() == "")
            mensagemErro_ValidaCamposFormulario += countMsg++ + ") " + traducao.adm_ConfiguracaoPessoais_a_tela_inicial_deve_ser_informada_ + "\n";
    }



    return mensagemErro_ValidaCamposFormulario;
}
