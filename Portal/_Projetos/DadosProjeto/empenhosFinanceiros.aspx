<%@ Page Language="C#" AutoEventWireup="true" CodeFile="empenhosFinanceiros.aspx.cs"
    Inherits="empenhosFinanceiros" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style3 {
            height: 10px;
        }

        .style4 {
            width: 230px;
        }

        .style5 {
            width: 160px;
        }

        .style6 {
            width: 130px;
        }

        .style7 {
            width: 115px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%">
                <tr>
                    <td></td>
                    <td style="height: 10px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            Width="100%" OnCallback="pnCallback_Callback">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoLancamentoFinanceiro"
                                        AutoGenerateColumns="False" Width="100%"
                                        ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnCustomColumnDisplayText="gvDados_CustomColumnDisplayText" OnCustomJSProperties="gvDados_CustomJSProperties">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                            CustomButtonClick="function(s, e){

    var utilizaConvenio = gvDados.cp_UtilizaConvenio;

     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        if(utilizaConvenio === true)
        {
              var valores = [-1, 'LEN', gvDados.cp_CodigoProjeto];
              mostraPopupLancamentoFinanceiro(valores);
        }
        else
        {
             onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		     hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		     TipoOperacao = &quot;Incluir&quot;
        }
     }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		 if(utilizaConvenio === true){
            gvDados.GetRowValues(gvDados.GetFocusedRowIndex(), 'CodigoLancamentoFinanceiro;IniciaisControleLancamento;CodigoProjeto;', mostraPopupLancamentoFinanceiro);
        }
        else
        {
            onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
            hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		    TipoOperacao = &quot;Editar&quot;
         }
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		if(utilizaConvenio === true)
        {
            var codigoLancamentoFinanceiro = gvDados.GetRowKey(e.visibleIndex);
            var valores = [codigoLancamentoFinanceiro, 'RO', gvDados.cp_CodigoProjeto];
            mostraPopupLancamentoFinanceiro(valores);
        }
        else
        {
             OnGridFocusedRowChanged(gvDados, true);
		     btnSalvar.SetVisible(false);
		     hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		     TipoOperacao = &quot;Consultar&quot;;
		     pcDados.Show();
        }
     }	
	 if(e.buttonID == &quot;btnAprovar&quot;)
     {
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Aprovar&quot;);
		TipoOperacao = &quot;Aprovar&quot;;
		pcAprovacao.Show();
		OnGridFocusedRowChanged(gvDados, true);		
     }
}
"></ClientSideEvents>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="135px" VisibleIndex="0">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnAprovar" Text="Aprovar / Reprovar">
                                                        <Image ToolTip="Aprovar / Reprovar" Url="~/imagens/botoes/aprovarReprovar.png">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                        <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <td align="center">
                                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                                    ClientInstanceName="menu"
                                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                                    OnInit="menu_Init">
                                                                    <Paddings Padding="0px" />
                                                                    <Items>
                                                                        <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                            <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
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
                                                                                <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                                    ClientVisible="False">
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
                                                                        <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
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

                                                </HeaderTemplate>
                                            </dxwgv:GridViewCommandColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Tipo" VisibleIndex="4" Width="65px" FieldName="IndicaDespesaReceita"
                                                Name="colTipo">
                                                <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataDateColumn Caption="Data Empenho" FieldName="DataEmpenho" Name="colDataEmpenho"
                                                ShowInCustomizationForm="True" VisibleIndex="6" Width="120px">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesDateEdit>
                                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"
                                                    ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Emitente" FieldName="PessoaEmitente" ShowInCustomizationForm="True"
                                                VisibleIndex="1" Name="colEmitente" Width="200px">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="NomeProjeto" FieldName="NomeProjeto" Name="NomeProjeto"
                                                ShowInCustomizationForm="True" VisibleIndex="4">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Nota Fiscal" ShowInCustomizationForm="True"
                                                VisibleIndex="7" Width="95px" FieldName="NumeroDocFiscal" Name="colNumeroDocFiscal">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Valor(R$)" ShowInCustomizationForm="True"
                                                VisibleIndex="8" Width="125px" FieldName="ValorEmpenhado" Name="colValorEmpenhado">
                                                <PropertiesTextEdit DisplayFormatString="N2">
                                                </PropertiesTextEdit>
                                                <Settings AllowAutoFilter="False" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <CellStyle HorizontalAlign="Right">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Status" ShowInCustomizationForm="True" VisibleIndex="9"
                                                Width="90px" FieldName="IndicaAprovacaoReprovacao" Name="colStatus">
                                                <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataDateColumn Caption="Data Pagamento" FieldName="DataPagamentoRecebimento"
                                                Name="colDataPagamentoRecebimento" ShowInCustomizationForm="True" Visible="False"
                                                VisibleIndex="10" Width="85px">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesDateEdit>
                                                <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="16" Name="colCodigoProjeto">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ValorPagamentoRecebimento" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="17" Name="colValorPagamentoRecebimento">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoPessoaEmitente" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="18" Name="colCodigoPessoaEmitente">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataEmissaoDocFiscal" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="19" Name="colDataEmissaoDocFiscal">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoConta" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="20" Name="colCodigoConta">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoFonteRecursosFinanceiros" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="26" Name="colCodigoFontePagadora">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataDateColumn Caption="Previsão Pagamento" FieldName="DataPrevistaPagamentoRecebimento"
                                                ShowInCustomizationForm="True" VisibleIndex="21" Width="140px" Name="colDataPrevistaPagamentoRecebimento">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesDateEdit>
                                                <Settings AllowAutoFilter="True" ShowFilterRowMenu="True" />
                                                <CellStyle Wrap="True">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="HistoricoEmpenho" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="23" Name="colHistoricoEmpenho">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="HistoricoPagamento" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="24" Name="colHistoricoPagamento">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoTarefa" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="25" Name="colCodigoTarefa">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoRecursoProjeto" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="22" Name="colCodigoRecursoProjeto">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataInclusao" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="12" Name="colDataInclusao">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="UsuarioInclusao" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="13" Name="colUsuarioInclusao">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataAprovacaoReprovacao" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="14" Name="colDataAprovacaoReprovacao">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="UsuarioAprovacao" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="15" Name="colUsuarioAprovacao">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="HistoricoAprovacaoReprovacao" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="11" Name="colHistoricoAprovacaoReprovacao">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Conta" FieldName="DescricaoConta" ShowInCustomizationForm="True"
                                                VisibleIndex="2" Name="colConta" Width="200px">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Partícipe" FieldName="NomeParticipe" ShowInCustomizationForm="True"
                                                VisibleIndex="4" Name="colFontePagadora" Width="200px">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                        <SettingsPager AlwaysShowPager="True" PageSize="50">
                                        </SettingsPager>
                                        <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" ShowGroupPanel="True"
                                            HorizontalScrollBarMode="Visible"></Settings>
                                        <SettingsText></SettingsText>
                                        <StylesPager>
                                            <PageNumber>
                                            </PageNumber>
                                            <Pager>
                                            </Pager>
                                        </StylesPager>
                                        <SettingsCommandButton>
                                            <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                            <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                        </SettingsCommandButton>
                                        <StylesEditors>
                                            <Calendar>
                                            </Calendar>
                                            <CalendarDayHeader>
                                            </CalendarDayHeader>
                                            <CalendarWeekNumber>
                                            </CalendarWeekNumber>
                                            <CalendarDay>
                                            </CalendarDay>
                                            <CalendarDayOtherMonth>
                                            </CalendarDayOtherMonth>
                                            <CalendarDaySelected>
                                            </CalendarDaySelected>
                                            <CalendarDayWeekEnd>
                                            </CalendarDayWeekEnd>
                                            <CalendarDayOutOfRange>
                                            </CalendarDayOutOfRange>
                                            <CalendarToday>
                                            </CalendarToday>
                                            <CalendarHeader>
                                            </CalendarHeader>
                                            <CalendarFooter>
                                            </CalendarFooter>
                                            <CalendarButton>
                                            </CalendarButton>
                                        </StylesEditors>
                                    </dxwgv:ASPxGridView>
                                    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
                                        HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                        ShowCloseButton="False" Width="750px"
                                        ID="pcDados">
                                        <ContentStyle>
                                            <Paddings Padding="5px"></Paddings>
                                        </ContentStyle>
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <table style="width: 100%">
                                                    <tr id="trProjeto">
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                                            Text="Projeto:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-bottom: 10px">
                                                                        <dxe:ASPxComboBox ID="ddlProjeto" runat="server" ClientInstanceName="ddlProjeto"
                                                                            IncrementalFilteringMode="Contains" TextField="NomeProjeto"
                                                                            ValueField="CodigoProjeto" Width="100%">
                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	codigoRecursoParam = null;
	ddlTarefa.PerformCallback();
}" />
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                            Text="Tarefa:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td class="style4">
                                                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                            Text="Recurso:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxe:ASPxComboBox ID="ddlTarefa" runat="server" ClientInstanceName="ddlTarefa"
                                                                            IncrementalFilteringMode="Contains" TextField="NomeProjeto" ValueField="CodigoProjeto"
                                                                            Width="100%" OnCallback="ddlTarefa_Callback">
                                                                            <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Codigo);
	ddlRecurso.PerformCallback(codigoRecursoParam);
}"
                                                                                SelectedIndexChanged="function(s, e) {
	ddlRecurso.PerformCallback();
}" />
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td class="style4">
                                                                        <dxe:ASPxComboBox ID="ddlRecurso" runat="server" ClientInstanceName="ddlRecurso"
                                                                            IncrementalFilteringMode="Contains" TextField="NomeProjeto"
                                                                            ValueField="CodigoProjeto" Width="100%" OnCallback="ddlRecurso_Callback">
                                                                            <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Codigo);
}" />
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td class="style5">&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                                                            Text="Conta:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td class="style4" id="tdLBLFontePagadora" runat="server">
                                                                        <dxe:ASPxLabel ID="ASPxLabel25" runat="server"
                                                                            Text="Fonte Pagadora:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style5" style="padding-right: 10px">
                                                                        <dxe:ASPxRadioButtonList ID="rbDespesaReceita" runat="server"
                                                                            ItemSpacing="15px" RepeatDirection="Horizontal" SelectedIndex="0"
                                                                            ClientInstanceName="rbDespesaReceita">
                                                                            <Paddings Padding="0px" />
                                                                            <ClientSideEvents Init="function(s, e) {
	verificaDespesaReceita();
}"
                                                                                SelectedIndexChanged="function(s, e) {
	verificaDespesaReceita();
	ddlConta.PerformCallback();
}" />
                                                                            <Items>
                                                                                <dxe:ListEditItem Selected="True" Text="Despesa" Value="D" />
                                                                                <dxe:ListEditItem Text="Receita" Value="R" />
                                                                            </Items>
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                            <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxRadioButtonList>
                                                                    </td>
                                                                    <td id="tdDDLConta" style="padding-right: 10px" runat="server">
                                                                        <dxe:ASPxComboBox ID="ddlConta" runat="server" ClientInstanceName="ddlConta"
                                                                            IncrementalFilteringMode="Contains" TextField="NomeProjeto" ValueField="CodigoProjeto"
                                                                            Width="100%" OnCallback="ddlConta_Callback">
                                                                            <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Codigo);
}" />
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td class="style4" id="tdDDLFontePagadora" runat="server">
                                                                        <dxe:ASPxComboBox ID="ddlFontePagadora" runat="server"
                                                                            ClientInstanceName="ddlFontePagadora"
                                                                            IncrementalFilteringMode="Contains"
                                                                            TextField="NomeProjeto" ValueField="CodigoProjeto" Width="100%"
                                                                            OnCallback="ddlFontePagadora_Callback">
                                                                            <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_Codigo);
}" />
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel ID="lblRazaoSocial" runat="server"
                                                                            Text="Fornecedor:" ClientInstanceName="lblRazaoSocial">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td class="style6">
                                                                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                                            Text="Emissão Doc Fiscal:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td class="style5">
                                                                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                                                            Text="Nº Doc Fiscal:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxe:ASPxComboBox ID="ddlRazaoSocial" runat="server" ClientInstanceName="ddlRazaoSocial"
                                                                            IncrementalFilteringMode="Contains" TextFormatString="{0}"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <dxe:ListBoxColumn Caption="Razão Social" FieldName="NomePessoa" Name="NomePessoa"
                                                                                    Width="300px" />
                                                                                <dxe:ListBoxColumn Caption="Nome Fantasia" FieldName="NomeFantasia" Name="NomeFantasia"
                                                                                    Width="260px" />
                                                                            </Columns>
                                                                            <ItemStyle Wrap="True" />
                                                                            <ListBoxStyle Wrap="True">
                                                                            </ListBoxStyle>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td class="style6" style="padding-right: 10px">
                                                                        <dxe:ASPxDateEdit ID="ddlEmissaoDoc" runat="server" ClientInstanceName="ddlEmissaoDoc"
                                                                            DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                            EncodeHtml="False" UseMaskBehavior="True"
                                                                            Width="100%">
                                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                <Style>
                                                                            </Style>
                                                                            </CalendarProperties>
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="style5">
                                                                        <dxe:ASPxTextBox ID="txtNumeroDoc" runat="server" ClientInstanceName="txtNumeroDoc"
                                                                            MaxLength="25" Width="100%">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td class="style7" style="padding-right: 10px" valign="top">
                                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server"
                                                                                        Text="Empenhado em:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                                        Text="Previsto para:">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>

                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel14" runat="server"
                                                                                        Text="Valor (R$):">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>

                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="lblPagoEmRecebidoEm" runat="server"
                                                                                        Text="Pago em:" ClientInstanceName="lblPagoEmRecebidoEm">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 5px;">
                                                                                    <dxe:ASPxDateEdit ID="ddlEmpenhadoEm" runat="server" ClientInstanceName="ddlEmpenhadoEm"
                                                                                        DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                        EncodeHtml="False" UseMaskBehavior="True"
                                                                                        Width="100%">
                                                                                        <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                            <Style>
                                                                                        </Style>
                                                                                        </CalendarProperties>
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td style="padding-right: 5px;">
                                                                                    <dxe:ASPxDateEdit ID="ddlPrevistoPara" runat="server" ClientInstanceName="ddlPrevistoPara"
                                                                                        DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                        EncodeHtml="False" UseMaskBehavior="True"
                                                                                        Width="100%">
                                                                                        <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                            <Style>
                                                                                        </Style>
                                                                                        </CalendarProperties>
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>

                                                                                <td style="padding-right: 5px;">
                                                                                    <dxe:ASPxSpinEdit ID="txtValor" runat="server" ClientInstanceName="txtValor" DecimalPlaces="2"
                                                                                        DisplayFormatString="{0:n2}" HorizontalAlign="Right"
                                                                                        NullText=" " Number="0" Width="100%">
                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                        </SpinButtons>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxSpinEdit>
                                                                                </td>

                                                                                <td>
                                                                                    <dxe:ASPxDateEdit ID="dtPagoEm" runat="server" ClientEnabled="False" ClientInstanceName="dtPagoEm"
                                                                                        DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                        EncodeHtml="False" UseMaskBehavior="True"
                                                                                        Width="100%">
                                                                                        <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                            <Style>
                                                                                        </Style>
                                                                                        </CalendarProperties>
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                            </tr>


                                                                        </table>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>

                                                                    <td valign="top">
                                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel15" runat="server" EncodeHtml="False"
                                                                                        Text="Comentários Pagamento:">
                                                                                    </dxe:ASPxLabel>
                                                                                    &nbsp;
                                                                                </td>

                                                                                <td>
                                                                                    <dxe:ASPxLabel ID="ASPxLabel12" runat="server" EncodeHtml="False"
                                                                                        Text="Comentários Empenho: &amp;nbsp;">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 5px;">
                                                                                    <dxe:ASPxMemo ID="mmPagamento" runat="server" ClientEnabled="False" ClientInstanceName="mmPagamento"
                                                                                        Rows="2" Width="100%">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxMemo>
                                                                                    <dxe:ASPxLabel ID="lblContadorMemoComentariosPagamento" runat="server"
                                                                                        ClientInstanceName="lblContadorMemoComentariosPagamento" Font-Bold="True"
                                                                                        ForeColor="#999999">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>

                                                                                <td>
                                                                                    <dxe:ASPxMemo ID="txtComentariosEmpenho" runat="server" ClientInstanceName="txtComentariosEmpenho"
                                                                                        Rows="2" Width="100%">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxMemo>
                                                                                    <dxe:ASPxLabel ID="lblContadorMemoComentariosEmpenho" runat="server"
                                                                                        ClientInstanceName="lblContadorMemoComentariosEmpenho" Font-Bold="True"
                                                                                        ForeColor="#999999">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>

                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="lblInclusao" runat="server" ClientInstanceName="lblInclusao" Font-Italic="True">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td>&nbsp;&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="lblAprovacao" runat="server" ClientInstanceName="lblAprovacao"
                                                                                            Font-Italic="True">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td>&nbsp;&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td width="100px" style="padding-right: 5px;">
                                                                            <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                                Text="Salvar" Width="100%">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                                <Paddings Padding="0px" />
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    hfGeral.Set(&quot;Critica&quot;,&quot;S&quot;);
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td></td>
                                                                        <td width="100px">
                                                                            <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                                Text="Fechar" Width="100%">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                                <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                                                    PaddingTop="0px" />
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                    <dxpc:ASPxPopupControl ID="pcAprovacao" runat="server" ClientInstanceName="pcAprovacao"
                                        CloseAction="None" HeaderText="Detalhes"
                                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                        Width="850px">
                                        <ContentStyle>
                                            <Paddings Padding="5px" />
                                        </ContentStyle>
                                        <HeaderStyle Font-Bold="True" />
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxRadioButtonList ID="rbStatusEmpenho" runat="server" ClientInstanceName="rbStatusEmpenho"
                                                                ItemSpacing="15px" RepeatDirection="Horizontal">
                                                                <Paddings Padding="0px" />
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Aprovado" Value="A" />
                                                                    <dxe:ListEditItem Text="Reprovado" Value="R" />
                                                                    <dxe:ListEditItem Text="Pendente de Aprovação" Value="P" />
                                                                </Items>
                                                                <DisabledStyle ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxRadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel24" runat="server" EncodeHtml="False"
                                                                Text="Comentários: &amp;nbsp;">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxMemo ID="mmAprovacao" runat="server" ClientInstanceName="mmAprovacao"
                                                                Rows="8" Width="100%">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxMemo>
                                                            <dxe:ASPxLabel ID="lblContadorMemoComentarios" runat="server"
                                                                ClientInstanceName="lblContadorMemoComentarios" Font-Bold="True"
                                                                ForeColor="#999999">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 15px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                                <tr>
                                                                                    <td>&nbsp;
                                                                                    </td>
                                                                                    <td>&nbsp;&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="lblAprovacao2" runat="server" ClientInstanceName="lblAprovacao2"
                                                                                            Font-Italic="True">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td>&nbsp;&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td width="100px" style="padding-right: 5px;">
                                                                            <dxe:ASPxButton ID="btnSalvar0" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarAprovacao"
                                                                                Text="Salvar" Width="100%">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Aprovar&quot;);
	TipoOperacao = &quot;Aprovar&quot;;
   hfGeral.Set(&quot;Critica&quot;,&quot;N&quot;);
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                                <Paddings Padding="0px" />
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td></td>
                                                                        <td width="100px">
                                                                            <dxe:ASPxButton ID="btnFechar0" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                                Text="Fechar" Width="100%">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    pcAprovacao.Hide();
}" />
                                                                                <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                                                    PaddingTop="0px" />
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                        <td></td>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
                                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                        ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="" align="center"></td>
                                                            <td style="width: 70px" align="center" rowspan="3">
                                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                                    ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                                                    ID="lblAcaoGravacao">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                        ExportEmptyDetailGrid="True" GridViewID="gvDados" Landscape="True"
                                        LeftMargin="50" RightMargin="50" OnRenderBrick="gvExporter_RenderBrick">
                                        <Styles>
                                            <Default>
                                            </Default>
                                            <Header>
                                            </Header>
                                            <Cell>
                                            </Cell>
                                            <Footer>
                                            </Footer>
                                            <GroupFooter>
                                            </GroupFooter>
                                            <GroupRow>
                                            </GroupRow>
                                            <Title></Title>
                                        </Styles>
                                    </dxwgv:ASPxGridViewExporter>
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                    </dxhf:ASPxHiddenField>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents EndCallback="function(s, e) 
{
	debugger
                                if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.empenhosFinanceiros_empenho_inclu_do_com_sucesso);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.empenhosFinanceiros_empenho_alterado_com_sucesso);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(traducao.empenhosFinanceiros_empenho_exclu_do_com_sucesso);
	else if(&quot;Aprovar&quot; == s.cp_OperacaoOk)
	{
		pcAprovacao.Hide();
		mostraDivSalvoPublicado(traducao.empenhosFinanceiros_status_alterado_com_sucesso);
	}
	
	if (ddlProjeto.cp_RO == 'S')
           document.getElementById('trProjeto').style.display = 'none';
}"></ClientSideEvents>

                        </dxcp:ASPxCallbackPanel>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
        <script language="javascript" type="text/javascript">
            if (ddlProjeto.cp_RO == "S")
                document.getElementById("trProjeto").style.display = "none";
        </script>

    </form>
</body>
</html>
