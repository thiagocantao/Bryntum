
    // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
function OnGridFocusedRowChanged(grid) 
{
    grid.GetRowValues(grid.GetFocusedRowIndex(),'ResourceUID;Recurso;Email;ContaWindows;Sincronizado;TipoProblema;DescricaoMotivo', MontaCamposFormulario);    
}

function MontaCamposFormulario(values)
{    
    var codigoRecurso = (values[0] != null ? values[0]  : "");
    var nomeRecurso = (values[1] != null ? values[1]  : "");
    var emailRecurso = (values[2] != null ? values[2]  : "");
    var contaWindows = (values[3] != null ? values[3]  : "");
    var sincronizado = (values[4] != null ? values[4]  : "");
    var tipoProblema = (values[5] != null ? values[5]  : "");
    var descricaoMotivo = (values[6] != null ? values[6]  : "");
    
    txtNome.SetText(nomeRecurso);
    txtContaWindows.SetText(contaWindows);
    txtEmail.SetText(emailRecurso);
    
    if(tipoProblema == "0")
    {
        document.getElementById('tdProblemas').style.display = 'none';
        btnSalvar.SetVisible(false);  
    }
    else
    {    
        document.getElementById('frmVinculo').src = './sincronismoMSTipo' + tipoProblema + '.aspx'
        document.getElementById('tdProblemas').style.display = 'block';
        btnSalvar.SetVisible(true);
    }
    
    lblMotivo.SetText(descricaoMotivo);
    
    if(pcDados.IsVisible() == false)
    {
        pcDados.Show();
    }
}

function mostraInformacao(msgInformacao)
{
    lblInformacao.SetText(msgInformacao);
    pcInformacao.Show();
}

function atualizaEmailSistema()
{
    pcNovoEmail.Show();
}

//----------- Mensagem modificação con sucesso..!!!
function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
}

function validateEmail(elementValue)
{      
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return emailPattern.test(elementValue); 
}

function validaFormulario()
{
    var eMail  = txtNovoEmail.GetText();
    if(eMail != "")
    {
	    if (!validateEmail(eMail))
	    {
	        mostraDivSalvoPublicado(traducao.sincronismoMSProject_por_favor__forne_a_um_endere_o_de_e_mail_v_lido_); 
	        return false;
	    }
    }
    else
    {
    	mostraDivSalvoPublicado(traducao.sincronismoMSProject_por_favor__forne_a_um_endere_o_de_e_mail_); 
    	return false;
    }
    return true;
}