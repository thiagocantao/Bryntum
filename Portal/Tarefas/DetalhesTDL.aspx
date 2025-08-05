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
            mensagemError += ++numError + ") A data de início real não pode ser maior a data atual!\n";
            retorno = false;
        }
    }

    if((dataTermino != null))
    {        
        if(dataInicio != null)
        {
            if(dataInicio > dataTermino)
            {
                mensagemError += ++numError + ") A data de término real não pode ser menor a data início real!\n";
                retorno = false;        
            }
        }
        if(dataInicio == null)
        {
            mensagemError += ++numError + ") A data de início real deve ser informada se o término real foi preenchido\n";
            retorno = false;
        }
        if(dataTermino > dataHoje)
        {
            mensagemError += ++numError + ") A data de término real não pode ser maior que a data atual!\n";
            retorno = false;
        }

    }
    else
	{        
        if(dataInicio == null)
        {
            if(txtEsforcoReal.GetText() != null && txtEsforcoReal.GetValue() != 0 && txtEsforcoReal.GetText() != "")
            {
                mensagemError += ++numError + ") Só indicar esforço real se o início real estiver preenchido\n";
                retorno = false;
            }
        }
	}
	
	if (!retorno)
	    window.top.mostraMensagem(mensagemError, 'atencao', true, false, null);
	    
	return retorno;
}

function mostraDivSalvoPublicado(acao)
{
    if (acao.toUpperCase().indexOf('SUCESSO'))
        window.top.mostraMensagem(acao, 'sucesso', false, false, null);
    else
        window.top.mostraMensagem(acao, 'erro', true, false, null);
    window.top.retornoModal = 'S';
    window.top.fechaModal();
}

   

    </script>
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
                                                                <td style="width: 450px">
                                                                    <dxe:ASPxLabel ID="lblDescricaoTarefa" runat="server" ClientInstanceName="lblDescricaoTarefa"
                                                                         Text="Descrição Tarefa:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td >
                                                                    <dxe:ASPxLabel ID="lblCodigoUsuarioResponsavel" runat="server" ClientInstanceName="lblCodigoUsuarioResponsavel"
                                                                         Text="Usuário Responsável:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr >
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtDescricaoTarefaBanco" runat="server" ClientEnabled="False"
                                                                        ClientInstanceName="txtDescricaoTarefaBanco" 
                                                                        Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                                            ForeColor="Black">
                                                                            <Border BorderColor="Silver" />
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
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
                                                <td style="width: 90px">
                                                    <dxe:ASPxLabel ID="lblInicioPrevisto" runat="server" ClientInstanceName="lblInicioPrevisto"
                                                         Text="Início Prev.:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 10px; height: 17px">
                                                </td>
                                                <td style="width: 90px">
                                                    <dxe:ASPxLabel ID="lblTerminoPrevisto" runat="server" ClientInstanceName="lblTerminoPrevisto"
                                                          Text="<%$ Resources:traducao, DetalhesTDL_t_rmino_prev__ %>">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 113px">
                                                    <dxe:ASPxLabel ID="lblCustoPrevisto" runat="server" ClientInstanceName="lblCustoPrevisto"
                                                         Text="Despesa Prev.(R$):">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 100px">
                                                    <dxe:ASPxLabel ID="lblEsforcoPrevisto" runat="server" ClientInstanceName="lblEsforcoPrevisto"
                                                         Text="Esforço Prev.(h):">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="lblEstagio" runat="server" ClientInstanceName="lblEstagio"
                                                        Text="<%$ Resources:traducao, DetalhesTDL_est_gio_ %>">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 115px">
                                                    <dxe:ASPxLabel ID="lblCustoReal" runat="server" ClientInstanceName="lblCustoReal"
                                                         Text="Despesa Real (R$):">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtInicioPrevistoBanco" runat="server" ClientEnabled="False"
                                                        ClientInstanceName="txtInicioPrevistoBanco" DisplayFormatString="dd/MM/yyyy"
                                                         Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                            ForeColor="Black">
                                                            <Border BorderColor="Silver" />
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtTerminoPrevistoBanco" runat="server" ClientEnabled="False"
                                                        ClientInstanceName="txtTerminoPrevistoBanco" DisplayFormatString="dd/MM/yyyy"
                                                         Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                            ForeColor="Black">
                                                            <Border BorderColor="Silver" />
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtCustoPrevistoBanco" runat="server" ClientEnabled="False"
                                                        ClientInstanceName="txtCustoPrevistoBanco" 
                                                        HorizontalAlign="Right" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                            ForeColor="Black">
                                                            <Border BorderColor="Silver" />
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtEsforcoPrevistoBanco" runat="server" ClientEnabled="False"
                                                        ClientInstanceName="txtEsforcoPrevistoBanco" 
                                                        HorizontalAlign="Right" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                            ForeColor="Black">
                                                            <Border BorderColor="Silver" />
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtEstagioBanco" runat="server" ClientEnabled="False" ClientInstanceName="txtEstagioBanco"
                                                         Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB"  
                                                            ForeColor="Black">
                                                            <Border BorderColor="Silver" />
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
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
                                <td>
                                    <dxe:ASPxLabel ID="lblOrigemTarefa" runat="server" ClientInstanceName="lblOrigemTarefa"
                                         Text="Origem Tarefa:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr >
                                <td>
                                    <dxe:ASPxMemo ID="txtOrigemTarefaBanco" runat="server" ClientEnabled="False" ClientInstanceName="txtOrigemTarefaBanco"
                                         Rows="8" Width="100%">
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
                                                                    <td style="width: 120px">
                                                                        <dxe:ASPxLabel ID="lblInicioReal" runat="server" ClientInstanceName="lblInicioReal"
                                                                             Text="In&#237;cio Real:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 120px">
                                                                        <dxe:ASPxLabel ID="lblTerminoReal" runat="server" ClientInstanceName="lblTerminoReal"
                                                                             Text="T&#233;rmino Real:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <dxe:ASPxLabel ID="lblEsforcoReal" runat="server" ClientInstanceName="lblEsforcoReal"
                                                                             Text="Esfor&#231;o Real (h):">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="ddlInicioReal" runat="server" ClientInstanceName="ddlInicioReal"
                                                                             Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
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
                                                                    <td>
                                                                    </td>
                                                                    <td id="Td1">
                                                                        <dxe:ASPxDateEdit ID="ddlTerminoReal" runat="server" ClientInstanceName="ddlTerminoReal"
                                                                             Width="100%" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
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
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txtEsforcoReal" runat="server" ClientInstanceName="txtEsforcoReal"
                                                                             HorizontalAlign="Right" Width="100%">
                                                                            <DisabledStyle BackColor="PapayaWhip" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>
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
                                                                    <td style="height: 16px">
                                                                        <dxe:ASPxLabel ID="lblAnotacoes" runat="server" ClientInstanceName="lblAnotacoes"
                                                                             Text="Anota&#231;&#245;es:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="mmAnotacoesBanco" runat="server" ClientInstanceName="mmAnotacoesBanco"
                                                                             Rows="14" Width="100%">
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
            <tr >
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr >
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <dxe:aspxbutton id="btnSalvar" runat="server" autopostback="False" clientinstancename="btnSalvar"
                                     text="Salvar" width="90px">
<ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
	                    if(verificarDadosPreenchidos())
						{
							callBack.PerformCallback();
                        	//window.top.fechaModal();
						}
                    }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:aspxbutton>
                            </td>
                            <td>
                            </td>
                            <td>
                    <dxe:aspxbutton id="btnFechar" runat="server" autopostback="False" clientinstancename="btnFechar"
                         text="Fechar" width="90px">
<Paddings Padding="0px"></Paddings>

<ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
						window.top.retornoModal = 'N';
                        window.top.fechaModal();
                    }"></ClientSideEvents>
</dxe:aspxbutton>
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
        </table>
        <dxcb:aspxcallback id="callBack" runat="server" clientinstancename="callBack" oncallback="callBack_Callback">
<ClientSideEvents EndCallback="function(s, e) {
	mostraDivSalvoPublicado(&quot;Dados salvos com sucesso!&quot;);
}"></ClientSideEvents>
</dxcb:aspxcallback>
        <dxpc:ASPxPopupControl ID="pcUsuarioIncluido" runat="server" ClientInstanceName="pcUsuarioIncluido"
             HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
            Width="270px">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td align="center" style="">
                                </td>
                                <td align="center" rowspan="3" style="width: 70px">
                                    <dxe:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar" ImageAlign="TextTop"
                                        ImageUrl="~/imagens/Workflow/salvarBanco.png">
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <dxe:ASPxLabel ID="lblAcaoGravacao" runat="server" ClientInstanceName="lblAcaoGravacao"
                                        >
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
