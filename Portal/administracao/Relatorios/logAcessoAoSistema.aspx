<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="logAcessoAoSistema.aspx.cs" Inherits="administracao_Relatorios_logAcessoAoSistema"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr style="height: 26px">
                <td valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr style="height: 26px">
                            <td align="right" style="height: 26px;">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 28px">
                                    <tr>
                                        <td align="left" style="padding-left: 10px">
                                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                                Font-Overline="False" Font-Strikeout="False"
                                                Text="Log de Acesso ao Sistema"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                <table cellspacing="0"
                                    cellpadding="0" border="0"
                                    __designer:mapid="47e">
                                    <tbody __designer:mapid="47f">
                                        <tr __designer:mapid="480">
                                            <td style="width: 50px" valign="middle" __designer:mapid="481">
                                                <dxe:ASPxLabel runat="server" Text="Per&#237;odo:" Width="49px"
                                                    ID="ASPxLabel1">
                                                </dxe:ASPxLabel>

                                            </td>
                                            <td style="width: 112px" valign="middle" __designer:mapid="483">
                                                <dxe:ASPxDateEdit runat="server" EditFormat="Custom"
                                                    EditFormatString="dd/MM/yyyy" Width="110px" Height="20px"
                                                    DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtInicio"
                                                    ID="txtInicio">
                                                    <CalendarProperties>

                                                        <DayHeaderStyle></DayHeaderStyle>

                                                        <WeekNumberStyle></WeekNumberStyle>

                                                        <DayStyle></DayStyle>

                                                        <DaySelectedStyle></DaySelectedStyle>

                                                        <DayOtherMonthStyle></DayOtherMonthStyle>

                                                        <DayWeekendStyle></DayWeekendStyle>

                                                        <DayOutOfRangeStyle></DayOutOfRangeStyle>

                                                        <TodayStyle></TodayStyle>

                                                        <ButtonStyle></ButtonStyle>

                                                        <HeaderStyle></HeaderStyle>

                                                        <FooterStyle></FooterStyle>

                                                        <FastNavStyle></FastNavStyle>

                                                        <FastNavMonthAreaStyle></FastNavMonthAreaStyle>

                                                        <FastNavYearAreaStyle></FastNavYearAreaStyle>

                                                        <FastNavMonthStyle></FastNavMonthStyle>

                                                        <FastNavYearStyle></FastNavYearStyle>

                                                        <FastNavFooterStyle></FastNavFooterStyle>

                                                        <Style></Style>
                                                    </CalendarProperties>

                                                    <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                </dxe:ASPxDateEdit>

                                            </td>
                                            <td style="width: 12px" valign="middle" align="center" __designer:mapid="49a">
                                                <dxe:ASPxLabel runat="server" Text="a" Width="10px"
                                                    ID="ASPxLabel2">
                                                </dxe:ASPxLabel>

                                            </td>
                                            <td style="width: 115px" valign="middle" __designer:mapid="49c">
                                                <dxe:ASPxDateEdit runat="server" EditFormat="Custom"
                                                    EditFormatString="dd/MM/yyyy" Width="110px" Height="20px"
                                                    DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtTermino"
                                                    ID="txtTermino">
                                                    <CalendarProperties>

                                                        <DayHeaderStyle></DayHeaderStyle>

                                                        <WeekNumberStyle></WeekNumberStyle>

                                                        <DayStyle></DayStyle>

                                                        <DaySelectedStyle></DaySelectedStyle>

                                                        <DayOtherMonthStyle></DayOtherMonthStyle>

                                                        <DayWeekendStyle></DayWeekendStyle>

                                                        <DayOutOfRangeStyle></DayOutOfRangeStyle>

                                                        <TodayStyle></TodayStyle>

                                                        <ButtonStyle></ButtonStyle>

                                                        <HeaderStyle></HeaderStyle>

                                                        <FooterStyle></FooterStyle>

                                                        <FastNavStyle></FastNavStyle>

                                                        <FastNavMonthAreaStyle></FastNavMonthAreaStyle>

                                                        <FastNavYearAreaStyle></FastNavYearAreaStyle>

                                                        <FastNavMonthStyle></FastNavMonthStyle>

                                                        <FastNavYearStyle></FastNavYearStyle>

                                                        <FastNavFooterStyle></FastNavFooterStyle>

                                                        <Style></Style>
                                                    </CalendarProperties>

                                                    <ButtonStyle></ButtonStyle>

                                                    <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                </dxe:ASPxDateEdit>

                                            </td>
                                            <td style="width: 95px" valign="middle" __designer:mapid="4b4">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Selecionar"
                                                    Width="90px" ID="btnSelecionar">
                                                    <ClientSideEvents Click="function(s, e) 
{
	var msg = &#39;&#39;;

	if(txtInicio.GetValue()!= null &amp;&amp; txtTermino.GetValue()  != null)
    {
        var dataInicio 	  = new Date(txtInicio.GetValue());
		var meuDataInicio = (dataInicio.getMonth() +1) + &#39;/&#39; + dataInicio.getDate() + &#39;/&#39; + dataInicio.getFullYear();
		dataInicio  	  = Date.parse(meuDataInicio);
				
		var dataTermino 	= new Date(txtTermino.GetValue());
		var meuDataTermino 	= (dataTermino.getMonth() +1) + &#39;/&#39; + dataTermino.getDate() + &#39;/&#39; + dataTermino.getFullYear();
		dataTermino		    = Date.parse(meuDataTermino);

	    if(dataInicio &gt; dataTermino)
        {
            msg = &#39;A Data de In&#237;cio n&#227;o pode ser maior que a Data de T&#233;rmino!\n&#39;;
            window.top.mostraMensagem(msg, 'Atencao', true, false, null);
			e.processOnServer = false;
	    }
	}
	else
	{
        if(txtInicio.GetValue() == null)
		{
			msg += &#39;Data de in&#237;cio deve ser informada.\n&#39;;
        }
        if(txtTermino.GetValue()== null)
		{
			msg += &#39;Data de t&#233;rmino deve ser informada.\n&#39;;
        }
		if(msg != &#39;&#39;)
		{
			window.top.mostraMensagem(msg, 'Atencao', true, false, null);
			e.processOnServer = false;
		}	
	}
	
	if(msg == &#39;&#39;)
		pnCallbackDados.PerformCallback();
}"></ClientSideEvents>

                                                    <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                </dxe:ASPxButton>

                                            </td>
                                            <td style="width: 20px" valign="middle" id="tdHelp" __designer:mapid="4b8">
                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png"
                                                    ClientInstanceName="ImgHelp" Cursor="pointer" ID="ImgHelp">
                                                </dxe:ASPxImage>

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
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
                                                        <td style="padding-right: 10px; padding-left: 10px;" id="tdDado">
                                                            <div id="divGrid" runat="server">
                                                                <table id="tbBotoes" runat="server" cellpadding="0" cellspacing="0"
                                                                    style="width: 100%; padding-top: 5px;">
                                                                    <tr runat="server">
                                                                        <td runat="server" style="padding-top: 5px; padding-bottom: 5px;" valign="top">
                                                                            <table cellpadding="0" cellspacing="0" style="height: 22px">
                                                                                <tr>
                                                                                    <td style="padding-right: 10px; padding-left: 5px;">
                                                                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                                                            ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init"
                                                                                            OnItemClick="menu_ItemClick">
                                                                                            <Paddings Padding="0px" />
                                                                                            <Items>
                                                                                                <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                                                    <Items>
                                                                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                        <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                                            <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                    </Items>
                                                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                                                    </Image>
                                                                                                </dxm:MenuItem>
                                                                                                <dxm:MenuItem ClientVisible="False" Name="btnLayout" Text="" ToolTip="Layout">
                                                                                                    <Items>
                                                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                                            <Image IconID="save_save_16x16">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                                            <Image IconID="actions_reset_16x16">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                    </Items>
                                                                                                    <Image Url="~/imagens/botoes/layout.png">
                                                                                                    </Image>
                                                                                                </dxm:MenuItem>
                                                                                            </Items>
                                                                                            <ItemStyle Cursor="pointer">
                                                                                                <HoverStyle>
                                                                                                    <border borderstyle="None" />
                                                                                                </HoverStyle>
                                                                                                <Paddings Padding="0px" />
                                                                                            </ItemStyle>
                                                                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                                                <SelectedStyle>
                                                                                                    <border borderstyle="None" />
                                                                                                </SelectedStyle>
                                                                                            </SubMenuItemStyle>
                                                                                            <Border BorderStyle="None" />
                                                                                        </dxm:ASPxMenu>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <dxwpg:ASPxPivotGrid ID="pvgLogAcessoSistema" runat="server"
                                                                    ClientIDMode="AutoID"
                                                                    ClientInstanceName="pvgLogAcessoSistema"
                                                                    OnCustomFieldSort="pvgLogAcessoSistema_CustomFieldSort" Width="100%">
                                                                    <Fields>
                                                                        <dxwpg:PivotGridField ID="field" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                                                            Area="RowArea" AreaIndex="0" Caption="Nome do Usuário" FieldName="NomeUsuario">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="field1"
                                                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1"
                                                                            Caption="Data de Acesso" CellFormat-FormatString="dd/MM/yyyy"
                                                                            CellFormat-FormatType="DateTime" FieldName="Data" SortMode="Custom"
                                                                            TotalCellFormat-FormatString="hh:mm" TotalCellFormat-FormatType="DateTime"
                                                                            TotalValueFormat-FormatString="hh:mm" TotalValueFormat-FormatType="DateTime"
                                                                            UnboundFieldName="field1" ValueFormat-FormatString="dd/MM/yyyy"
                                                                            ValueFormat-FormatType="DateTime" Visible="False">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="field2"
                                                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="RowArea" AreaIndex="1"
                                                                            Caption="Horário de Acesso" CellFormat-FormatString="hh:mm"
                                                                            CellFormat-FormatType="DateTime" FieldName="Horario" SortMode="Custom"
                                                                            TotalCellFormat-FormatString="hh:mm" TotalCellFormat-FormatType="DateTime"
                                                                            TotalValueFormat-FormatString="hh:mm" TotalValueFormat-FormatType="DateTime"
                                                                            ValueFormat-FormatString="hh:mm" ValueFormat-FormatType="DateTime"
                                                                            Visible="False">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="field3" AllowedAreas="DataArea" Area="DataArea"
                                                                            AreaIndex="0" Caption="Quantidade de Acessos" CellFormat-FormatString="#.###"
                                                                            CellFormat-FormatType="Numeric" FieldName="QuantidadeAcesso"
                                                                            GrandTotalCellFormat-FormatString="#.###"
                                                                            GrandTotalCellFormat-FormatType="Numeric" SortBySummaryInfo-SummaryType="Count"
                                                                            TotalCellFormat-FormatString="#.###" TotalCellFormat-FormatType="Numeric"
                                                                            ValueFormat-FormatString="#.###" ValueFormat-FormatType="Numeric">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="field4"
                                                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="0"
                                                                            Caption="Ano" CellFormat-FormatType="Numeric" FieldName="Ano"
                                                                            ValueFormat-FormatType="Numeric">
                                                                        </dxwpg:PivotGridField>
                                                                        <dxwpg:PivotGridField ID="fldMes"
                                                                            AllowedAreas="RowArea, ColumnArea, FilterArea" Area="ColumnArea" AreaIndex="1"
                                                                            Caption="Mês" CellFormat-FormatType="Numeric" FieldName="Mes" SortMode="Custom"
                                                                            ValueFormat-FormatType="Numeric">
                                                                        </dxwpg:PivotGridField>
                                                                    </Fields>
                                                                    <OptionsPager Visible="False">
                                                                    </OptionsPager>
                                                                    <Styles>
                                                                        <HeaderStyle />
                                                                        <AreaStyle>
                                                                        </AreaStyle>
                                                                        <DataAreaStyle>
                                                                        </DataAreaStyle>
                                                                        <ColumnAreaStyle>
                                                                        </ColumnAreaStyle>
                                                                        <FieldValueStyle>
                                                                        </FieldValueStyle>
                                                                        <CellStyle>
                                                                        </CellStyle>
                                                                        <MenuItemStyle />
                                                                        <MenuStyle />
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
    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcAjuda" CloseAction="CloseButton"
        HeaderText="Ajuda" PopupElementID="tdHelp" Width="323px"
        ID="pcAjuda">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dxe:ASPxLabel runat="server" Text="Mostra o log de acessos para o per&#237;odo informado."
                    Width="100%" Font-Bold="False"
                    ID="ASPxLabel4">
                </dxe:ASPxLabel>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpgwx:ASPxPivotGridExporter runat="server" ASPxPivotGridID="pvgLogAcessoSistema"
        ID="exporter" OnCustomExportCell="exporter_CustomExportCell">
    </dxpgwx:ASPxPivotGridExporter>
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
    </dxhf:ASPxHiddenField>
</asp:Content>
