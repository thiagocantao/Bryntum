<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalhesTDL.aspx.cs" Inherits="espacoTrabalho_DetalhesTS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<base target="_self" />
    <title>Detalhes</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
     <script type="text/javascript" language="javascript">
        function verificarDadosPreenchidos()
{
    var mensagemError = "";
    var retorno = true;
    var numError = 0;
    var status = ddlStatusTarefa.GetText();
    
    //----------obtendo a data atual, início real e término real
    var dataAtual 	 = new Date();
	var meuDataAtual = (dataAtual.getMonth() +1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
	var dataHoje 	 = Date.parse(meuDataAtual);
	
    var dataTermino;
	var dataInicio;

	if(ddlInicioReal.GetValue() != null){
		var dataInicio 	  = new Date(ddlInicioReal.GetValue());
		var meuDataInicio = (dataInicio.getMonth() +1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
		dataInicio  	  = Date.parse(meuDataInicio);
	}else{
		dataInicio = null;
	}

	if(ddlTerminoReal.GetValue() != null){
		var dataTermino 	= new Date(ddlTerminoReal.GetValue());
		var meuDataTermino 	= (dataTermino.getMonth() +1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
		dataTermino		    = Date.parse(meuDataTermino);
	}else{
		dataTermino = null;
	}
    
    
    if((dataInicio != null))
    {
        if(dataInicio > dataHoje)
        {
            mensagemError += ++numError + ") " + traducao.DetalhesTDL_a_data_de_in_cio_real_n_o_pode_ser_maior_a_data_atual + "!\n";
            retorno = false;
        }
    }

    if((dataTermino != null))
    {
        if(status != traducao.kanban_StatusTarefaConcluida)
        {
            mensagemError += ++numError + ") " + traducao.DetalhesTDL_o_status_da_tarefa_deve_ser_igual_a__conclu_do__caso_o_t_rmino_real_estiver_informado + "!\n";
            retorno = false;
        }
        if(dataInicio != null)
        {
            if(dataInicio > dataTermino)
            {
                mensagemError += ++numError + ") " + traducao.DetalhesTDL_a_data_de_t_rmino_real_n_o_pode_ser_menor_a_data_in_cio_real + "!\n";
                retorno = false;        
            }
        }
        if(dataInicio == null)
        {
            mensagemError += ++numError + ") " + traducao.DetalhesTDL_a_data_de_in_cio_real_deve_ser_informada_se_o_t_rmino_real_foi_preenchido + "!\n";
            retorno = false;
        }
        if(dataTermino > dataHoje)
        {
            mensagemError += ++numError + ") " + traducao.DetalhesTDL_a_data_de_t_rmino_real_n_o_pode_ser_maior_que_a_data_atual + "!\n";
            retorno = false;
        }

    }
    else
	{
        if(status == traducao.kanban_StatusTarefaConcluida)
        {
            mensagemError += ++numError + ") " + traducao.DetalhesTDL_a_tarefa_s__poder__ter_status_igual_a___conclu_da___se_o_t_rmino_real_for_informado + "!\n";
            retorno = false;
        }
        if(dataInicio == null)
        {
            if(txtEsforcoReal.GetText() != null && parseFloat(txtEsforcoReal.GetValue().toString()) != 0 && txtEsforcoReal.GetText() != "")
            {
                mensagemError += ++numError + ") " + traducao.DetalhesTDL_s__indicar_esfor_o_real_se_o_in_cio_real_estiver_preenchido + "!\n";
                retorno = false;
            }
        }
	}
	
    if (status == traducao.kanban_StatusTarefaNaoIniciada) {
        if (ddlInicioReal.GetValue() != null) {
            mensagemError += ++numError + ") " + traducao.DetalhesTDL_in_cio_real_n_o_pode_estar_preenchido_se_o_status_est__como_n_o_iniciado + "!\n";
            retorno = false;
        }
    }
    if (!retorno)
        window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);
	    
	return retorno;
}

         function mostraDivSalvoPublicado(acao) {
             if (acao.toUpperCase().indexOf('SUCESSO'))
                 window.top.mostraMensagem(acao, 'sucesso', false, false, null);
             else
                 window.top.mostraMensagem(acao, 'erro', true, false, null);

             window.top.retornoModal = 'S';
             window.top.fechaModal();
         }
         
         function funcaoCallbackSalvar() {
             if (verificarDadosPreenchidos()) {
                 callBack.PerformCallback();
                 //window.top.fechaModal();
             }
         }

         function funcaoCallbackFechar() {
             window.top.retornoModal = 'N';
             window.top.fechaModalComFooter();
         }

     </script>
    <style>
        .btn{
            text-transform:capitalize !important;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="99%">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 450px; padding-right: 5px; padding-left: 5px;">
                                                                    <dxe:ASPxLabel ID="lblDescricaoTarefa" runat="server" ClientInstanceName="lblDescricaoTarefa"
                                                                         Text="Descrição Tarefa:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="padding-right: 5px; padding-left: 5px;" >
                                                                    <dxe:ASPxLabel ID="lblCodigoUsuarioResponsavel" runat="server" ClientInstanceName="lblCodigoUsuarioResponsavel"
                                                                         Text="Usuário Responsável:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr >
                                                                <td style="padding-right: 5px; padding-left: 5px;">
                                                                    <dxe:ASPxTextBox ID="txtDescricaoTarefaBanco" runat="server" ClientEnabled="False"
                                                                        ClientInstanceName="txtDescricaoTarefaBanco" 
                                                                        Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                                            ForeColor="Black">
                                                                            <Border BorderColor="Silver" />
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="padding-right: 5px; padding-left: 5px;">
                                                                    <dxe:ASPxTextBox ID="txtCodigoUsuarioResponsavelBanco" runat="server" ClientEnabled="False"
                                                                        ClientInstanceName="txtCodigoUsuarioResponsavelBanco" 
                                                                        Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                                            ForeColor="Black">
                                                                            <Border BorderColor="Silver" />
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr >
                                <td>
                                </td>
                            </tr>
                            <tr >
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="width: 110px; padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxLabel ID="lblInicioPrevisto" runat="server" ClientInstanceName="lblInicioPrevisto"
                                                         Text="Início Prev.:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 110px; padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxLabel ID="lblTerminoPrevisto" runat="server" ClientInstanceName="lblTerminoPrevisto"
                                                         Text="<%$ Resources:traducao, DetalhesTDL_t_rmino_prev__ %>">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 125px; padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxLabel ID="lblCustoPrevisto" runat="server" ClientInstanceName="lblCustoPrevisto"
                                                         Text="Despesa Prev.(R$):">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 110px; padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxLabel ID="lblEsforcoPrevisto" runat="server" ClientInstanceName="lblEsforcoPrevisto"
                                                         Text="Esforço Prev.(h):">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxLabel ID="lblEstagio" runat="server" ClientInstanceName="lblEstagio"
                                                        Text="<%$ Resources:traducao, DetalhesTDL_est_gio_ %>">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 130px; padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxLabel ID="lblCustoReal" runat="server" ClientInstanceName="lblCustoReal"
                                                         Text="Despesa Real (R$):">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxTextBox ID="txtInicioPrevistoBanco" runat="server" ClientEnabled="False"
                                                        ClientInstanceName="txtInicioPrevistoBanco" DisplayFormatString="dd/MM/yyyy"
                                                         Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                            ForeColor="Black">
                                                            <Border BorderColor="Silver" />
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td style="padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxTextBox ID="txtTerminoPrevistoBanco" runat="server" ClientEnabled="False"
                                                        ClientInstanceName="txtTerminoPrevistoBanco" DisplayFormatString="dd/MM/yyyy"
                                                         Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                            ForeColor="Black">
                                                            <Border BorderColor="Silver" />
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td style="padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxTextBox ID="txtCustoPrevistoBanco" runat="server" ClientEnabled="False"
                                                        ClientInstanceName="txtCustoPrevistoBanco" 
                                                        HorizontalAlign="Right" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                            ForeColor="Black">
                                                            <Border BorderColor="Silver" />
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td style="padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxTextBox ID="txtEsforcoPrevistoBanco" runat="server" ClientEnabled="False"
                                                        ClientInstanceName="txtEsforcoPrevistoBanco" 
                                                        HorizontalAlign="Right" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                            ForeColor="Black">
                                                            <Border BorderColor="Silver" />
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td style="padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxTextBox ID="txtEstagioBanco" runat="server" ClientEnabled="False" ClientInstanceName="txtEstagioBanco"
                                                         Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                            ForeColor="Black">
                                                            <Border BorderColor="Silver" />
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td style="padding-right: 5px; padding-left: 5px;">
                                                    <dxe:ASPxTextBox ID="txtCustoRealBanco" runat="server" ClientEnabled="False" ClientInstanceName="txtCustoRealBanco"
                                                         HorizontalAlign="Right" Width="100%">
                                                        <DisabledStyle BackColor="#E0E0E0"  ForeColor="Black">
                                                            <Border BorderColor="Silver" />
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr >
                                <td>
                                </td>
                            </tr>
                            <tr >
                                <td style="padding-right: 5px; padding-left: 5px;">
                                    <dxe:ASPxLabel ID="lblOrigemTarefa" runat="server" ClientInstanceName="lblOrigemTarefa"
                                         Text="Origem Tarefa:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr >
                                <td style="padding-right: 5px; padding-left: 5px;">
                                    <dxe:ASPxMemo ID="txtOrigemTarefaBanco" runat="server" ClientEnabled="False" ClientInstanceName="txtOrigemTarefaBanco"
                                         Rows="5" Width="100%">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            <Border BorderColor="Silver" />
                                        </DisabledStyle>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr >
                                <td style="height: 10px">
                                </td>
                            </tr>
                            <tr >
                                <td>
                                    <dxp:ASPxPanel ID="paEditar" runat="server" ClientInstanceName="paEditar" Width="100%">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <table id="Table2" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 120px; padding-right: 5px; padding-left: 5px;">
                                                                        <dxe:ASPxLabel ID="lblInicioReal" runat="server" ClientInstanceName="lblInicioReal"
                                                                             Text="In&#237;cio Real:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 120px; padding-right: 5px; padding-left: 5px;">
                                                                        <dxe:ASPxLabel ID="lblTerminoReal" runat="server" ClientInstanceName="lblTerminoReal"
                                                                             Text="T&#233;rmino Real:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 120px; padding-right: 5px; padding-left: 5px;">
                                                                        <dxe:ASPxLabel ID="lblEsforcoReal" runat="server" ClientInstanceName="lblEsforcoReal"
                                                                             Text="Esfor&#231;o Real (h):">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 140px; padding-right: 5px; padding-left: 5px;">
                                                                        <dxe:ASPxLabel ID="lblStatusTarefa" runat="server" ClientInstanceName="lblStatusTarefa"
                                                                             Text="Status:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 5px; padding-left: 5px;">

                                                                        <dxe:ASPxDateEdit ID="ddlInicioReal" runat="server" PopupHorizontalAlign="OutsideRight" PopupVerticalAlign="Middle" ClientInstanceName="ddlInicioReal"
                                                                             Width="100%" EditFormat="Custom" DisplayFormatString="<%# Resources.traducao.geral_formato_data_csharp %>" EditFormatString="<%# Resources.traducao.geral_formato_data_csharp %>">
                                                                            <CalendarProperties>
                                                                                <DayHeaderStyle  />
                                                                                <WeekNumberStyle >
                                                                                </WeekNumberStyle>
                                                                                <DayStyle  />
                                                                                <DaySelectedStyle >
                                                                                </DaySelectedStyle>
                                                                                <DayOtherMonthStyle >
                                                                                </DayOtherMonthStyle>
                                                                                <DayWeekendStyle >
                                                                                </DayWeekendStyle>
                                                                                <DayOutOfRangeStyle >
                                                                                </DayOutOfRangeStyle>
                                                                                <TodayStyle >
                                                                                </TodayStyle>
                                                                                <ButtonStyle >
                                                                                </ButtonStyle>
                                                                                <Style ></Style>
                                                                            </CalendarProperties>
                                                                            <ValidationSettings ValidationGroup="MKE">
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="PapayaWhip" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td id="Td1" style="padding-right: 5px; padding-left: 5px;">
                                                                        <dxe:ASPxDateEdit ID="ddlTerminoReal" runat="server" PopupHorizontalAlign="OutsideRight" PopupVerticalAlign="Middle" ClientInstanceName="ddlTerminoReal"
                                                                             Width="100%" EditFormat="Custom" DisplayFormatString="<%# Resources.traducao.geral_formato_data_csharp %>" EditFormatString="<%# Resources.traducao.geral_formato_data_csharp %>">
                                                                            <CalendarProperties>
                                                                                <DayHeaderStyle  />
                                                                                <WeekNumberStyle >
                                                                                </WeekNumberStyle>
                                                                                <DayStyle  />
                                                                                <DaySelectedStyle >
                                                                                </DaySelectedStyle>
                                                                                <DayOtherMonthStyle >
                                                                                </DayOtherMonthStyle>
                                                                                <DayWeekendStyle >
                                                                                </DayWeekendStyle>
                                                                                <DayOutOfRangeStyle >
                                                                                </DayOutOfRangeStyle>
                                                                                <TodayStyle >
                                                                                </TodayStyle>
                                                                                <ButtonStyle >
                                                                                </ButtonStyle>
                                                                                <Style ></Style>
                                                                            </CalendarProperties>
                                                                            <ValidationSettings ValidationGroup="MKE">
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="PapayaWhip" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td style="padding-right: 5px; padding-left: 5px;">
                                                                        <dxe:ASPxTextBox ID="txtEsforcoReal" runat="server" ClientInstanceName="txtEsforcoReal"
                                                                             HorizontalAlign="Right" Width="100%">
                                                                            <DisabledStyle BackColor="PapayaWhip" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                                <MaskSettings Mask="&lt;0..999999&gt;.&lt;00..99&gt;"></MaskSettings>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 5px; padding-left: 5px;">
                                                                        <dxe:ASPxComboBox ID="ddlStatusTarefa" runat="server" ClientInstanceName="ddlStatusTarefa"
                                                                             ValueType="System.Int32" Width="100%">
                                                                            <DisabledStyle BackColor="PapayaWhip" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="height: 16px; padding-right: 5px; padding-left: 5px;">
                                                                        <dxe:ASPxLabel ID="lblAnotacoes" runat="server" ClientInstanceName="lblAnotacoes"
                                                                             Text="Anota&#231;&#245;es:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 5px; padding-left: 5px;">
                                                                        <dxe:ASPxMemo ID="mmAnotacoesBanco" runat="server" MaxLength="4000" ClientInstanceName="mmAnotacoesBanco"
                                                                             Rows="9" Width="100%">
                                                                            <DisabledStyle BackColor="PapayaWhip" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td >
                </td>
            </tr>
        </table>
        <dxcb:aspxcallback id="callBack" runat="server" clientinstancename="callBack" oncallback="callBack_Callback">
<ClientSideEvents EndCallback="function(s, e) {
	mostraDivSalvoPublicado(traducao.DetalhesTDL_dados_salvos_com_sucesso_);
}"></ClientSideEvents>
</dxcb:aspxcallback>
    </form>
</body>
</html>
