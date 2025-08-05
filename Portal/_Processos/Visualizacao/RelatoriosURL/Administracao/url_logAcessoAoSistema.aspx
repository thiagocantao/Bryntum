<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_logAcessoAoSistema.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Administracao_url_logAcessoAoSistema" Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>

    </div>
    <table>
        <tr>
            <td>
                <div>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <dxcp:ASPxCallbackPanel ID="pnCallbackDados" runat="server" ClientInstanceName="pnCallbackDados"
                                    OnCallback="pnCallbackDados_Callback" Width="100%">
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
                                                                                        <td style="width: 50px" valign="middle">
                                                                                            <dxe:ASPxLabel runat="server" Text="Per&#237;odo:" Width="49px"
                                                                                                ID="ASPxLabel1">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="width: 112px" valign="middle">
                                                                                            <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                Width="110px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtInicio"
                                                                                                 ID="txtInicio">
                                                                                                <CalendarProperties>
                                                                                                    <Style  />
 

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
                                                                                                    <HeaderStyle  />
                                                                                                    <FooterStyle  />
                                                                                                    <FastNavStyle >
                                                                                                    </FastNavStyle>
                                                                                                    <FastNavMonthAreaStyle >
                                                                                                    </FastNavMonthAreaStyle>
                                                                                                    <FastNavYearAreaStyle >
                                                                                                    </FastNavYearAreaStyle>
                                                                                                    <FastNavMonthStyle >
                                                                                                    </FastNavMonthStyle>
                                                                                                    <FastNavYearStyle >
                                                                                                    </FastNavYearStyle>
                                                                                                    <FastNavFooterStyle >
                                                                                                    </FastNavFooterStyle>
                                                                                                    <Style >
                                                                                                        
                                                                                                    </Style>
                                                                                                </CalendarProperties>
                                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                                </Paddings>
                                                                                            </dxe:ASPxDateEdit>
                                                                                        </td>
                                                                                        <td style="width: 12px" valign="middle">
                                                                                            <dxe:ASPxLabel runat="server" Text="a" Width="10px" 
                                                                                                ID="ASPxLabel2">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="width: 115px" valign="middle">
                                                                                            <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                                Width="110px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtTermino"
                                                                                                 ID="txtTermino">
                                                                                                <CalendarProperties>
                                                                                                    <Style  />
 

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
                                                                                                    <HeaderStyle  />
                                                                                                    <FooterStyle  />
                                                                                                    <FastNavStyle >
                                                                                                    </FastNavStyle>
                                                                                                    <FastNavMonthAreaStyle >
                                                                                                    </FastNavMonthAreaStyle>
                                                                                                    <FastNavYearAreaStyle >
                                                                                                    </FastNavYearAreaStyle>
                                                                                                    <FastNavMonthStyle >
                                                                                                    </FastNavMonthStyle>
                                                                                                    <FastNavYearStyle >
                                                                                                    </FastNavYearStyle>
                                                                                                    <FastNavFooterStyle >
                                                                                                    </FastNavFooterStyle>
                                                                                                    <Style >
                                                                                                        
                                                                                                    </Style>
                                                                                                </CalendarProperties>
                                                                                                <ButtonStyle >
                                                                                                </ButtonStyle>
                                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                                </Paddings>
                                                                                            </dxe:ASPxDateEdit>
                                                                                        </td>
                                                                                        <td style="width: 85px" valign="middle">
                                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Selecionar" Width="80px"
                                                                                                 ID="btnSelecionar">
                                                                                                <ClientSideEvents Click="function(s, e) 
{
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
            msg = 'A Data de Início não pode ser maior que a Data de Término!\n';
            window.top.mostraMensagem(msg, 'Atencao', true, false, null);
			e.processOnServer = false;
	    }
	}
	else
	{
        if(txtInicio.GetValue() == null)
		{
			msg += 'Data de início deve ser informada.\n';
        }
        if(txtTermino.GetValue()== null)
		{
			msg += 'Data de término deve ser informada.\n';
        }
		if(msg != '')
		{
			window.top.mostraMensagem(msg, 'Atencao', true, false, null);
			e.processOnServer = false;
		}	
	}
	
	if(msg == '')
		pnCallbackDados.PerformCallback();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                                </Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                        <td style="width: 35px" valign="middle" id="tdHelp">
                                                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ClientInstanceName="ImgHelp"
                                                                                                Cursor="pointer" ID="ImgHelp">
                                                                                            </dxe:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                        <td align="right">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 116px">
                                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="70px" ClientInstanceName="ddlExporta"
                                                                                                                 ID="ddlExporta">
                                                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
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
                                                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcAjuda" CloseAction="CloseButton"
                                                                HeaderText="Ajuda" PopupElementID="tdHelp" Width="323px"
                                                                ID="pcAjuda">
                                                                <ContentCollection>
                                                                    <dxpc:PopupControlContentControl runat="server">
                                                                        <dxe:ASPxLabel runat="server" Text="Mostra o log de acessos para o per&#237;odo informado."
                                                                            Width="100%" Font-Bold="True"  ID="ASPxLabel4">
                                                                        </dxe:ASPxLabel>
                                                                    </dxpc:PopupControlContentControl>
                                                                </ContentCollection>
                                                            </dxpc:ASPxPopupControl>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-right: 10px; padding-left: 10px" id="tdDado">
                                                            <dxpgwx:ASPxPivotGridExporter runat="server" ASPxPivotGridID="pvgLogAcessoSistema"
                                                                ID="ASPxPivotGridExporter1">
                                                            </dxpgwx:ASPxPivotGridExporter>
                                                            <div id="divGrid" runat="server">
                                                                <dxwpg:ASPxPivotGrid runat="server" ClientInstanceName="pvgLogAcessoSistema"
                                                                    Width="100%" ID="pvgLogAcessoSistema" OnCustomFieldSort="pvgLogAcessoSistema_CustomFieldSort">
                                                                    <Fields>
                                                                        <dxwpg:PivotGridField FieldName="NomeUsuario" ID="field" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                                            Area="RowArea" AreaIndex="0" Caption="Nome do Usu&#225;rio">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField FieldName="Data" ID="field1" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                                            Area="RowArea" AreaIndex="1" Visible="False" UnboundFieldName="field1" Caption="Data de Acesso"
                                                                            SortMode="Custom" CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime"
                                                                            TotalCellFormat-FormatString="hh:mm" TotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy"
                                                                            ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="hh:mm" TotalValueFormat-FormatType="DateTime">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField FieldName="Horario" ID="field2" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                                            Area="RowArea" AreaIndex="1" Visible="False" Caption="Hor&#225;rio de Acesso"
                                                                            SortMode="Custom" CellFormat-FormatString="hh:mm" CellFormat-FormatType="DateTime"
                                                                            TotalCellFormat-FormatString="hh:mm" TotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="hh:mm"
                                                                            ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="hh:mm" TotalValueFormat-FormatType="DateTime">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField FieldName="QuantidadeAcesso" ID="field3" AllowedAreas="DataArea"
                                                                            Area="DataArea" AreaIndex="0" Caption="Quantidade de Acessos" SortBySummaryInfo-SummaryType="Count"
                                                                            CellFormat-FormatString="#.###" CellFormat-FormatType="Numeric" TotalCellFormat-FormatString="#.###"
                                                                            TotalCellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatString="#.###"
                                                                            GrandTotalCellFormat-FormatType="Numeric" ValueFormat-FormatString="#.###" ValueFormat-FormatType="Numeric">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField FieldName="Ano" ID="field4" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                                            Area="ColumnArea" AreaIndex="0" Caption="Ano" CellFormat-FormatType="Numeric"
                                                                            ValueFormat-FormatType="Numeric">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField FieldName="Mes" ID="fldMes" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                                            Area="ColumnArea" AreaIndex="1" Caption="M&#234;s" SortMode="Custom" CellFormat-FormatType="Numeric"
                                                                            ValueFormat-FormatType="Numeric">
                                                                        </dxwpg:PivotGridField>
                                                                    </Fields>
                                                                    <OptionsPager Visible="False">
                                                                    </OptionsPager>
                                                                    <OptionsFilter ShowOnlyAvailableItems="True" />
                                                                    <Styles>
                                                                        <HeaderStyle ></HeaderStyle>
                                                                        <AreaStyle >
                                                                        </AreaStyle>
                                                                        <DataAreaStyle >
                                                                        </DataAreaStyle>
                                                                        <ColumnAreaStyle >
                                                                        </ColumnAreaStyle>
                                                                        <FieldValueStyle >
                                                                        </FieldValueStyle>
                                                                        <CellStyle >
                                                                        </CellStyle>
                                                                        <MenuItemStyle ></MenuItemStyle>
                                                                        <MenuStyle ></MenuStyle>
                                                                    </Styles>
                                                                </dxwpg:ASPxPivotGrid>
                                                            </div>
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
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
