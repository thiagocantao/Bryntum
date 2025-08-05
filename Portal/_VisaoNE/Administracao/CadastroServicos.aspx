<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CadastroServicos.aspx.cs" Inherits="_VisaoNE_Administracao_CadastroServicos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
<link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">


    function validaCamposFormulario() {
        // Esta função tem que retornar uma string.
        // "" se todas as validações estiverem OK
        // "<erro>" indicando o que deve ser corrigido
        mensagemErro_ValidaCamposFormulario = "";
        var numAux = 0;
        var mensagem = "";

        //------------Obtendo data e hora actual
        var dataInicio = new Date(ddlInicio.GetValue());
        var dataInicioP = (dataInicio.getMonth() + 1) + "/" + dataInicio.getDate() + "/" + dataInicio.getFullYear();
        var dataInicioC = Date.parse(dataInicioP);

        var dataTermino = new Date(ddlTermino.GetValue());
        var dataTerminoP = (dataTermino.getMonth() + 1) + "/" + dataTermino.getDate() + "/" + dataTermino.getFullYear();
        var dataTerminoC = Date.parse(dataTerminoP);
        
        if (txtNomeObra.GetText() == "") {
            numAux++;
            mensagem += "\n" + numAux + ") O nome da obra deve ser informado.";
        }

        if (ddlMunicipio.GetValue() == null) {
            numAux++;
            mensagem += "\n" + numAux + ") O município deve ser informado.";
        }        

        if (ddlInicio.GetValue() == null || ddlInicio.GetText() == "") {
            numAux++;
            mensagem += "\n" + numAux + ") A data de início previsto deve ser informada.";
        }

        if (ddlTermino.GetValue() == null || ddlTermino.GetText() == "") {
            numAux++;
            mensagem += "\n" + numAux + ") A data de término previsto deve ser informada.";
        }

        if ((ddlInicio.GetValue() != null) && (ddlTermino.GetValue() != null)) {
            if (dataInicioC > dataTerminoC) {
                mensagem += "\n" + numAux + ") A data de início previsto não pode ser maior que a data de término previsto!\n";
                retorno = false;
            }
        }

        if (mensagem != "") {
            mensagemErro_ValidaCamposFormulario = "Alguns dados são de preenchimento obrigatório:\n\n" + mensagem;
        }

        return mensagemErro_ValidaCamposFormulario;
    }

    //----------- Mensagem modificação con sucesso..!!!
    function mostraDivSalvoPublicado(acao) {
        if (acao.toUpperCase().indexOf('SUCESSO'))
            window.top.mostraMensagem(acao, 'sucesso', false, false, null);
        else
            window.top.mostraMensagem(acao, 'erro', true, false, null);
        
        if (callbackSalvar.cp_Status == "1")
            setTimeout('finalizaEdicao();', 1500);
    }

    function finalizaEdicao() {

        gravaInstanciaWf();
        pcUsuarioIncluido.Hide();
        window.top.retornoModal = 'S';
        try {
            window.top.fechaModal();
        }
        catch (e)
		    { }

    }

    function gravaInstanciaWf() {
        try {
            window.parent.executaCallbackWF();
        } catch (e) { }
    }

    function eventoPosSalvar(codigoInstancia) {
        try {
            window.parent.parent.hfGeralWorkflow.Set('CodigoInstanciaWf', codigoInstancia);
        } catch (e) {
        }
    }

</script>
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 5px;
        }
        .style2
        {
            width: 5px;
            height: 10px;
        }
        .style3
        {
            height: 10px;
        }
        .style17
        {
            height: 4px;
        }
        
        .style20
        {
            width: 5px;
            height: 5px;
        }
        .style23
        {
            width: 5px;
            height: 10px;
        }

        .style24
        {
            width: 105px;
            margin-left: 40px;
        }
        
        .style30
        {
            width: 128px;
        }
        .style34
        {
            width: 110px;
        }
        .style35
        {
            width: 115px;
        }

    </style>
</head>
<body style='margin:0px'>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="style20">
                    </td>
                <td class="style17">
                    </td>
                <td class="style20">
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                        Text="Serviço:">
                    </dxe:ASPxLabel>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <dxe:ASPxTextBox ID="txtNomeObra" runat="server"  
                        ClientInstanceName="txtNomeObra"  
                        Width="100%" MaxLength="250">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr class="PrevistoPBA">
                <td class="style2">
                    </td>
                <td class="style3">
                    </td>
                <td class="style2">
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                                <table cellspacing="1" class="headerGrid">
                                    <tr>
                                        <td class="style34">
                                <dxe:ASPxLabel ID="ASPxLabel15" runat="server" 
                                    Text="Número OS:">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="style35">
                                <dxe:ASPxLabel ID="ASPxLabel16" runat="server" 
                                    Text="Emissão:">
                                </dxe:ASPxLabel>
                                        </td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td>
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                    Text="Município:">
                                </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style34">
                    <dxe:ASPxTextBox ID="txtNumeroOS" runat="server" 
                        ClientInstanceName="txtNumeroOS"  
                        Width="100%" MaxLength="25">
                        <ValidationSettings ErrorDisplayMode="None">
                        </ValidationSettings>
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                                        </td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="style35">
                                <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" 
                                    EditFormatString="dd/MM/yyyy" EncodeHtml="False" Width="100%" 
                                    DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlEmissao" 
                                     
                                    ID="ddlEmissao" PopupHorizontalAlign="OutsideLeft" 
                                     PopupVerticalAlign="WindowCenter">
<CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje"></CalendarProperties>

<ClientSideEvents DateChanged="function(s, e) {
	//ddlTerminoDeVigencia.SetDate(s.GetValue());
	//calendar = ddlTerminoDeVigencia.GetCalendar();
  	//if ( calendar )
    //	calendar.minDate = new Date(s.GetValue());
}" ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxDateEdit>





                                        </td>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td>
                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                                    ValueType="System.String" TextFormatString="{1} - {0}" Width="100%" 
                                    ClientInstanceName="ddlMunicipio"  
                                    ID="ddlMunicipio">
<ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>
<Columns>
<dxe:ListBoxColumn FieldName="SiglaUF" Width="50px" Caption="UF"></dxe:ListBoxColumn>
<dxe:ListBoxColumn FieldName="NomeMunicipio" Width="650px" Caption="Munic&#237;pio"></dxe:ListBoxColumn>
</Columns>

<ItemStyle Wrap="True"></ItemStyle>

<ListBoxStyle Wrap="True"></ListBoxStyle>

                                    <ValidationSettings ErrorDisplayMode="None">
                                    </ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>





                                        </td>
                                    </tr>
                                </table>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style23">
                    </td>
                <td class="style3">
                    </td>
                <td class="style23">
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                                <dxe:ASPxLabel ID="ASPxLabel17" runat="server" 
                                    Text="Projeto Vinculado:">
                                </dxe:ASPxLabel>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <dxe:ASPxComboBox ID="ddlProjetos" runat="server" 
                        ClientInstanceName="ddlProjetos"  
                        IncrementalFilteringMode="Contains" TextField="NomeProjeto" 
                        ValueField="CodigoProjeto" Width="100%">
                        <ItemStyle Wrap="True" />
                        <ListBoxStyle Wrap="True">
                        </ListBoxStyle>
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxComboBox>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                                <dxe:ASPxLabel ID="ASPxLabel18" runat="server" 
                                    Text="Endereço Completo:">
                                </dxe:ASPxLabel>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <dxe:ASPxMemo ID="txtEndereco" runat="server" 
                        ClientInstanceName="txtEndereco"  
                        Rows="2" Width="100%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                    <dxe:ASPxLabel ID="lblContadorEnderecoCompleto" runat="server" 
                        ClientInstanceName="lblContadorEnderecoCompleto" Font-Bold="True" 
                        Font-Size="7pt" ForeColor="#999999">
                    </dxe:ASPxLabel>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
                <td class="style3">
                    </td>
                <td class="style2">
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabel14" runat="server" 
                                    Text="Coordenadas GPS:">
                                </dxe:ASPxLabel>
                            </td>
                             <td class="style1">
                                 &nbsp;</td>
                             <td class="style24">
                                <dxe:ASPxLabel ID="ASPxLabel11" runat="server" 
                                    Text="Início Previsto:">
                                </dxe:ASPxLabel>
                            </td>
                            <td class="style1">
                                &nbsp;</td>
                            <td class="style30">
                                <dxe:ASPxLabel ID="ASPxLabel12" runat="server" 
                                    Text="Término Previsto:">
                                </dxe:ASPxLabel>
                            </td>
                            <td class="style1">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                    <dxe:ASPxTextBox ID="txtCoordenadas" runat="server" 
                        ClientInstanceName="txtCoordenadas"  
                        Width="100%" MaxLength="100">
                        <ValidationSettings ErrorDisplayMode="None">
                        </ValidationSettings>
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>





                            </td>
                             <td class="style1">
                                 &nbsp;</td>
                             <td class="style24">
                                <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" 
                                    EditFormatString="dd/MM/yyyy" EncodeHtml="False" Width="100%" 
                                    DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlInicio" 
                                     
                                    ID="ddlInicio" PopupHorizontalAlign="OutsideLeft" 
                                     PopupVerticalAlign="WindowCenter">
<CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje"></CalendarProperties>

<ClientSideEvents DateChanged="function(s, e) {
	//ddlTerminoDeVigencia.SetDate(s.GetValue());
	//calendar = ddlTerminoDeVigencia.GetCalendar();
  	//if ( calendar )
    //	calendar.minDate = new Date(s.GetValue());
}" ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxDateEdit>





                            </td>
                            <td class="style1">
                                &nbsp;</td>
                            <td class="style30">
                                <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" 
                                    EditFormatString="dd/MM/yyyy" EncodeHtml="False" Width="100%" 
                                    DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlTermino" 
                                      
                                    ID="ddlTermino" PopupHorizontalAlign="OutsideLeft" 
                                    PopupVerticalAlign="WindowCenter">
<CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje"></CalendarProperties>

<ClientSideEvents DateChanged="function(s, e) {
	//ddlTerminoDeVigencia.SetDate(s.GetValue());
	//calendar = ddlTerminoDeVigencia.GetCalendar();
  	//if ( calendar )
    //	calendar.minDate = new Date(s.GetValue());
}" ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxDateEdit>





                            </td>

                            </tr>
                            </table>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
                <td class="style3">
                </td>
                <td class="style2">
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                                <dxe:ASPxLabel ID="ASPxLabel19" runat="server" 
                                    Text="Detalhamento:">
                                </dxe:ASPxLabel>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <dxe:ASPxMemo ID="txtDetalhamento" runat="server" 
                        ClientInstanceName="txtDetalhamento"  
                        Rows="5" Width="100%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                    <dxe:ASPxLabel ID="lblContadorDetalhamento" runat="server" 
                        ClientInstanceName="lblContadorDetalhamento" Font-Bold="True" 
                        Font-Size="7pt" ForeColor="#999999">
                    </dxe:ASPxLabel>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
                <td class="style3">
                </td>
                <td class="style2">
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                                <dxe:ASPxLabel ID="ASPxLabel20" runat="server" 
                                    Text="Licenças Exigidas:">
                                </dxe:ASPxLabel>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <dxe:ASPxTextBox ID="txtLicencas" runat="server" 
                        ClientInstanceName="txtLicencas"  
                        Width="100%" MaxLength="250">
                        <ValidationSettings ErrorDisplayMode="None">
                        </ValidationSettings>
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
                <td class="style3">
                    </td>
                <td class="style2">
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                                <dxe:ASPxLabel ID="ASPxLabel13" runat="server" 
                                    Text="Observações:">
                                </dxe:ASPxLabel>
                            </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <dxe:ASPxMemo ID="txtObservacoes" runat="server" 
                        ClientInstanceName="txtObservacoes"  
                        Rows="4" Width="100%">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                    <dxe:ASPxLabel ID="lblContadorObservacoes" runat="server" 
                        ClientInstanceName="lblContadorObservacoes" Font-Bold="True" 
                        Font-Size="7pt" ForeColor="#999999">
                    </dxe:ASPxLabel>
                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
                <td class="style3">
                    </td>
                <td class="style2">
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>



 <table border="0" cellpadding="0" cellspacing="0" width="100%"><tr>
         <td align="right">
             &nbsp;</td>
         <td align="right" style="width: 100px;">
             <dxe:ASPxButton runat="server" ClientInstanceName="btnExcluir" Text="Excluir" 
                 Width="95px"  
                 ID="btnExcluir">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(confirm('Deseja excluir a obra?'))
		callbackSalvar.PerformCallback('X');
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>



 </td><td align="right" style="width: 100px; ">
             <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" 
                 Width="95px"  
                 ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var msgErro = validaCamposFormulario();

	if(msgErro != &quot;&quot;)
		window.top.mostraMensagem(msgErro, 'atencao', true, false, null);
	else
		callbackSalvar.PerformCallback();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>



 </td><td align="right" style="width: 95px; padding-left: 5px; <%=mostrarBotaoCancelar %>">
             <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" 
                 CommandArgument="btnCancelar" Text="Fechar" Width="95px" 
                 ID="btnCancelar" AutoPostBack="False">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false; 
    window.top.fechaModal();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>



 </td></tr>
 </table></td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            </table>
    
        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" 
            HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" 
            Width="420px"  ID="pcUsuarioIncluido">
            <ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table cellSpacing="0" cellPadding="0" 
        width="100%" border="0"><tbody><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center">
            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" 
                 ID="lblAcaoGravacao" EncodeHtml="False"></dxe:ASPxLabel></td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

    </div>

 <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackSalvar" 
        ID="callbackSalvar" OnCallback="callbackSalvar_Callback">
     <ClientSideEvents EndCallback="function(s, e) {
	mostraDivSalvoPublicado(s.cp_MsgStatus);	
}" />
    </dxcb:ASPxCallback>

    </form>
</body>
</html>
