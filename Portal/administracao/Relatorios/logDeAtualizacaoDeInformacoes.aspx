<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="logDeAtualizacaoDeInformacoes.aspx.cs" Inherits="administracao_Relatorios_logDeAtualizacaoDeInformacoes"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr>
                <td valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 100%">
                        <tr>
                            <td align="right" style="height: 26px;">
                                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                                    width: 100%; height: 28px">
                                    <tr>
                                        <td align="left" style="padding-left: 10px">
                                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                                Font-Overline="False" Font-Strikeout="False"
                                                Text="Log de Atualização de Informações"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Desempenho:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                                <dxe:ASPxLabel ID="ASPxLabel40" runat="server" 
                                    Text="Ano:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td>
                <div>
                    &nbsp;<table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <dxcp:ASPxCallbackPanel ID="pnCallbackDado" runat="server" ClientInstanceName="pnCallbackDado"
                                    Width="100%">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 5px; padding-top: 10px">
                                                            <table id="tabelaFiltros" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <table style="height: 28px" cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 51px" valign="middle">
                                                                                            <dxe:ASPxLabel runat="server" Text="Per&#237;odo:" Width="50px"
                                                                                                ID="ASPxLabel1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="width: 115px" valign="middle">
                                                                                            <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                Width="110px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtInicio"
                                                                                                 ID="txtInicio">
                                                                                                <CalendarProperties>
                                                                                                    <DayHeaderStyle ></DayHeaderStyle>
                                                                                                    <DayStyle ></DayStyle>
                                                                                                    <DayWeekendStyle >
                                                                                                    </DayWeekendStyle>
                                                                                                    <ButtonStyle >
                                                                                                    </ButtonStyle>
                                                                                                </CalendarProperties>
                                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                                </Paddings>
                                                                                            </dxe:ASPxDateEdit>
                                                                                        </td>
                                                                                        <td style="width: 15px" valign="middle" align="left">
                                                                                            <dxe:ASPxLabel runat="server" Text="a" Width="5px" 
                                                                                                ID="ASPxLabel2">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="width: 115px" valign="middle">
                                                                                            <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                Width="110px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtTermino"
                                                                                                 ID="txtTermino">
                                                                                                <CalendarProperties>
                                                                                                    <DayHeaderStyle ></DayHeaderStyle>
                                                                                                    <DayStyle ></DayStyle>
                                                                                                    <DayWeekendStyle >
                                                                                                    </DayWeekendStyle>
                                                                                                    <ButtonStyle >
                                                                                                    </ButtonStyle>
                                                                                                    <HeaderStyle ></HeaderStyle>
                                                                                                </CalendarProperties>
                                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                                </Paddings>
                                                                                            </dxe:ASPxDateEdit>
                                                                                        </td>
                                                                                        <td valign="middle">
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Selecionar" Width="75px"
                                                                                                 ID="btnSelecionar">
                                                                                                <ClientSideEvents Click="function(s, e) {
	var msg = '';

	if(txtInicio.GetValue()!= null &amp;&amp; txtTermino.GetValue()  != null)
    {
        var dataInicio 	  = new Date(txtInicio.GetValue());
		var meuDataInicio = (dataInicio.getMonth() +1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
		dataInicio  	  = Date.parse(meuDataInicio);
				
		var dataTermino 	= new Date(txtTermino.GetValue());
		var meuDataTermino 	= (dataTermino.getMonth() +1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
		dataTermino		    = Date.parse(meuDataTermino);

	    if(dataInicio &gt; dataTermino)
        {
            msg = 'A Data de In&#237;cio n&#227;o pode ser maior que a Data de T&#233;rmino!\n';
            window.top.mostraMensagem(msg, 'atencao', true, false, null);
			e.processOnServer = false;
	    }
	}
	else
	{
        if(txtInicio.GetValue() == null)
		{
			msg += 'Data de in&#237;cio deve ser informada.\n';
        }
        if(txtTermino.GetValue()== null)
		{
			msg += 'Data de t&#233;rmino deve ser informada.\n';
        }
		if(msg != '')
		{
			window.top.mostraMensagem(msg, 'atencao', true, false, null);
			e.processOnServer = false;
		}	
	}

	if(msg == '')
		pnCallbackDado.PerformCallback()
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                                </Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                        <td style="padding-right: 3px; padding-left: 5px" valign="middle">
                                                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ClientInstanceName="ImgHelp"
                                                                                                Cursor="pointer" ID="ImgHelp">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcAjuda" CloseAction="CloseButton"
                                                                                HeaderText="Ajuda" PopupElementID="imgHelp" PopupHorizontalAlign="RightSides"
                                                                                PopupVerticalAlign="BottomSides" PopupVerticalOffset="120" Width="323px"
                                                                                ID="pcAjuda">
                                                                                <ContentCollection>
                                                                                    <dxpc:PopupControlContentControl runat="server">
                                                                                        <dxe:ASPxLabel runat="server" Text="Intervalo de datas a serem analizadas as altera&#231;&#245;es nas tabelas do sistema."
                                                                                            Width="100%" Font-Bold="True"  ID="ASPxLabel4">
                                                                                        </dxe:ASPxLabel>
                                                                                    </dxpc:PopupControlContentControl>
                                                                                </ContentCollection>
                                                                            </dxpc:ASPxPopupControl>
                                                                        </td>
                                                                        <td align="right">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 205px">
                                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" ClientInstanceName="ddlExporta"
                                                                                                                 ID="ddlExporta">
                                                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set('tipoArquivo', s.GetValue());
}"></ClientSideEvents>
                                                                                                            </dxe:ASPxComboBox>
                                                                                                        </td>
                                                                                                        <td style="padding-right: 3px; padding-left: 3px">
                                                                                                            <dxcp:ASPxCallbackPanel runat="server"  
                                                                                                                ClientInstanceName="pnImage" Width="23px" Height="22px" ID="pnImage" OnCallback="pnImage_Callback">
<SettingsLoadingPanel Enabled="False" ShowImage="False"></SettingsLoadingPanel>
                                                                                                                <PanelCollection>
                                                                                                                    <dxp:PanelContent runat="server">
                                                                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png"
                                                                                                                            Width="20px" Height="20px" ClientInstanceName="imgExportacao" ID="imgExportacao">
                                                                                                                        </dxe:ASPxImage>
                                                                                                                    </dxp:PanelContent>
                                                                                                                </PanelCollection>
                                                                                                            </dxcp:ASPxCallbackPanel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxButton runat="server" Text="Exportar" 
                                                                                                ID="Aspxbutton1" OnClick="btnExcel_Click">
                                                                                                <ClientSideEvents Click="function(s, e) 
{
	if(gvDados.pageRowCount == 0)
	{
		window.top.mostraMensagem(&quot;N&#227;o h&#225; Nenhuma informa&#231;&#227;o para exportar.&quot;, 'Atencao', true, false, null);
		e.processOnServer = false;	
	}
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                                                                            </dxhf:ASPxHiddenField>
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
                                                    <tr>
                                                        <td style="padding-right: 10px; padding-left: 10px">
                                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="Data"
                                                                AutoGenerateColumns="False" Width="100%" 
                                                                ID="logAtualizacaoDeInformacoes" 
                                                                OnAutoFilterCellEditorInitialize="logAtualizacaoDeInformacoes_AutoFilterCellEditorInitialize">
                                                                <Columns>
                                                                    <dxwgv:GridViewDataTextColumn Caption="Registro" FieldName="IdentificadorRegistro"
                                                                        VisibleIndex="0" Width="190px">
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="Campo" Caption="Campo" VisibleIndex="0"
                                                                        Width="220px">
                                                                        <Settings FilterMode="DisplayText" AutoFilterCondition="Contains"></Settings>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="InformacaoAnterior" Caption="Valor Anterior"
                                                                        VisibleIndex="1" Width="170px">
                                                                        <Settings FilterMode="DisplayText" AutoFilterCondition="Contains"></Settings>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="NovaInformacao" Caption="Novo Valor" VisibleIndex="2"
                                                                        Width="170px">
                                                                        <Settings FilterMode="DisplayText" AutoFilterCondition="Contains"></Settings>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Caption="Nome Usu&#225;rio"
                                                                        VisibleIndex="3" Width="190px">
                                                                        <Settings AllowAutoFilter="False" AllowGroup="True"></Settings>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="TipoOperacao" Width="75px" Caption="Opera&#231;&#227;o"
                                                                        VisibleIndex="4">
                                                                        <Settings AllowAutoFilter="False" AllowGroup="True"></Settings>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="Data" Width="85px" Caption="Data" VisibleIndex="5">
                                                                        <Settings AllowAutoFilter="False"></Settings>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="Tabela" Caption="Tabela" VisibleIndex="6"
                                                                        Width="160px">
                                                                        <Settings AllowAutoFilter="False" AllowGroup="True"></Settings>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                </Columns>
                                                                <SettingsBehavior AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>
                                                                <SettingsPager Mode="ShowAllRecords">
                                                                </SettingsPager>
                                                                <Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible" 
                                                                    HorizontalScrollBarMode="Visible"></Settings>
                                                                <SettingsText CommandClearFilter="Limpar filtro"></SettingsText>
                                                            </dxwgv:ASPxGridView>
                                                            <dxwgv:ASPxGridViewExporter runat="server" GridViewID="logAtualizacaoDeInformacoes"
                                                                LeftMargin="50" RightMargin="50" Landscape="True" 
                                                                ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                                                                <Styles>
                                                                    <Header Font-Bold="True" >
                                                                    </Header>
                                                                    <Cell >
                                                                    </Cell>
                                                                </Styles>
                                                            </dxwgv:ASPxGridViewExporter>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </td>
                        </tr>
                    </table>
                    &nbsp;</div>
            </td>
        </tr>
    </table>
</asp:Content>
