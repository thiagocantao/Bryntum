<%@ Page Language="C#" AutoEventWireup="true" CodeFile="analises.aspx.cs" Inherits="_Estrategias_indicador_analises" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript">
        function atualizaTela() {
            window.location.reload();
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            height: 5px;
        }

        .Resize textarea {
            resize: both;
        }

        .titulo-tela,
        .titulo-tela > span {
            display: inline-block;
            font-size: 18px !important;
            padding-bottom: 10px;
            padding-left: 7px;
            padding-top: 5px;
        }
    </style>
</head>
<body class="body">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr style="height: 26px">
                    <td valign="middle" style="padding-right: 10px;">
                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="lblTituloTela" CssClass="titulo-tela" runat="server" ClientInstanceName="lblTituloSelecao"
                                        Font-Bold="True" Text="Análises">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" style="width: 59px">
                                    <dxe:ASPxLabel ID="ASPxLabel11" runat="server"
                                        Text="Unidade:" Visible="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 110px">
                                    <dxe:ASPxComboBox ID="ddlUnidade" runat="server" ClientInstanceName="ddlUnidade"
                                        Width="100%" Visible="False">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td align="center" style="width: 100px">
                                    <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False"
                                        Text="Selecionar" Visible="False">
                                        <Paddings Padding="0px"></Paddings>

                                        <ClientSideEvents Click="function(s, e) {
	grid.PerformCallback('A');
	e.processOnServer = false;
}"></ClientSideEvents>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-right: 5px; padding-left: 5px">
                <tr>
                    <td id="tdObjetivoMapa" runat="server">
                        <table cellspacing="0" class="headerGrid formulario">
                            <tr class="formulario-linha" style="display: none;">
                                <td class="formulario-label">
                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                        Text="Mapa Estratégico:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="formulario-linha" style="display: none;">
                                <td>
                                    <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientEnabled="False"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr class="formulario-linha" style="display: none;">
                                <td class="formulario-label">
                                    <dxe:ASPxLabel ID="ASPxLabel12" runat="server"
                                        Text="Objetivo Estratégico:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="formulario-linha" style="display: none;">
                                <td>
                                    <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server" ClientEnabled="False"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td class="formulario-label">
                                    <dxe:ASPxLabel ID="lblIndicador" runat="server"
                                        Text="Indicador:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td>
                                    <dxe:ASPxTextBox ID="txtIndicador" runat="server" ClientInstanceName="txtIndicador"
                                        Width="100%" ClientEnabled="False">
                                        <DisabledStyle BackColor="#EAEAEA" ForeColor="Black"></DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="padding-top: 10px;">
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            OnCallback="pnCallback_Callback" Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="DetalhamentoPeriodo" AutoGenerateColumns="False" Width="100%" ID="gvDados" OnCustomButtonInitialize="grid_CustomButtonInitialize" OnHtmlRowPrepared="gvDados_HtmlRowPrepared">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                            CustomButtonClick="function(s, e) {
	onClick_CustomButomGrid(s, e);
}"></ClientSideEvents>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0"
                                                Width="70px">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnCustomInserir" Text="Incluir"
                                                        Visibility="EditableRow">
                                                        <Image Url="~/imagens/botoes/incluirReg02.png">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnCustomEditar" Text="Alterar"
                                                        Visibility="EditableRow">
                                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
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
                                            <dxwgv:GridViewDataTextColumn Caption="Per&#237;odo" FieldName="DetalhamentoPeriodo"
                                                Name="DetalhamentoPeriodo" VisibleIndex="1">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Per&#237;odo" FieldName="Periodo" Name="Periodo"
                                                Visible="False" VisibleIndex="0">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Meta" FieldName="ValorPrevisto" Name="ValorPrevisto"
                                                VisibleIndex="1">
                                                <PropertiesTextEdit DisplayFormatString="{0:n2}" EncodeHtml="False">
                                                </PropertiesTextEdit>
                                                <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Realizado" FieldName="ValorRealizado" Name="ValorRealizado"
                                                VisibleIndex="2">
                                                <PropertiesTextEdit DisplayFormatString="{0:n2}" EncodeHtml="False" NullDisplayText="-"
                                                    NullText="-">
                                                </PropertiesTextEdit>
                                                <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Desempenho" Visible="False" VisibleIndex="3">
                                                <HeaderStyle HorizontalAlign="Right" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption=" " FieldName="CorIndicador" VisibleIndex="3"
                                                Width="45px" Name="CorIndicador" ExportWidth="45">
                                                <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False"
                                                    AllowSort="False" />
                                                <DataItemTemplate>
                                                    <img alt='' src='../../imagens/<%# Eval("CorIndicador") %>.gif' />
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Tend&#234;ncia" Visible="False" VisibleIndex="5">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Ano" Name="Ano" Visible="False" VisibleIndex="4">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="mes" Name="mes" Visible="False" VisibleIndex="6">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Analise" Name="Analise" Visible="False"
                                                VisibleIndex="7">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Recomendacoes" Name="Recomendacoes" Visible="False"
                                                VisibleIndex="8">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoAnalisePerformance" Name="CodigoAnalisePerformance"
                                                Visible="False" VisibleIndex="9">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataAnalisePerformance" Name="DataAnalisePerformance"
                                                Visible="False" VisibleIndex="10">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataInclusao" Name="DataInclusao" Visible="False"
                                                VisibleIndex="11">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao" Name="CodigoUsuarioInclusao"
                                                Visible="False" VisibleIndex="12">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Name="NomeUsuario" Visible="False"
                                                VisibleIndex="13">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioUltimaAlteracao" Name="NomeUsuarioUltimaAlteracao"
                                                Visible="False" VisibleIndex="14">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataUltimaAlteracao" Name="DataUltimaAlteracao"
                                                Visible="False" VisibleIndex="15">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>

                                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>

                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="145" ShowFooter="True"></Settings>

                                        <SettingsText></SettingsText>

                                        <Templates>
                                            <FooterRow>
                                                <table class="grid-legendas" cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="grid-legendas-cor grid-legendas-cor-em-andamento"><span></span></td>
                                                            <td class="grid-legendas-label grid-legendas-label-em-andamento">
                                                                <dxe:ASPxLabel runat="server" Text="<%# Resources.traducao.analises_per_odo_em_andamento %>" ClientInstanceName="lblDescricaoNaoAtiva" Font-Bold="False" ID="lblDescricaoNaoAtiva"></dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </FooterRow>
                                        </Templates>
                                    </dxwgv:ASPxGridView>

                                </dxp:PanelContent>
                            </PanelCollection>

                            <ClientSideEvents EndCallback="function(s, e) {
      if(s.cpErro != '')
      {
                window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
      }
      else
      {
            if(s.cpSucesso != '')
           {
                 window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null);
                 onClick_btnCancelar();
           }
      }	
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                </tr>
            </table>
        </div>
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="600px" ID="pcDados" AllowResize="True">
            <ClientSideEvents Closing="function(s, e) {
	LimpaCamposFormulario();
}"></ClientSideEvents>

            <ContentStyle>
                <Paddings Padding="5px"></Paddings>
            </ContentStyle>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table style="width: 100%; border-collapse: collapse" id="pnCallback_grid_DXPEForm_DXEFT" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td rowspan="1">
                                    <dxe:ASPxLabel runat="server" Text="Análise:" ID="ASPxLabel1"></dxe:ASPxLabel>



                                </td>
                            </tr>
                            <tr>
                                <td rowspan="1">
                                    <dxe:ASPxMemo runat="server" Height="71px" Width="100%" ClientInstanceName="txtAnalise" ID="txtAnalise" CssClass="Resize">

                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black"></ReadOnlyStyle>
                                    </dxe:ASPxMemo>
                                    <dxe:ASPxLabel ID="lblContadorMemoAnalise" runat="server"
                                        ClientInstanceName="lblContadorMemoAnalise" Font-Bold="True"
                                        ForeColor="#999999">
                                    </dxe:ASPxLabel>



                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" rowspan="1"></td>
                            </tr>
                            <tr>
                                <td rowspan="1">
                                    <dxe:ASPxLabel runat="server" Text="Recomendações:" ID="ASPxLabel2"></dxe:ASPxLabel>



                                </td>
                            </tr>
                            <tr>
                                <td rowspan="1">
                                    <dxe:ASPxMemo runat="server" Height="71px" Width="100%" ClientInstanceName="txtRecomendacoes" ID="txtRecomendacoes" CssClass="Resize">

                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black"></ReadOnlyStyle>
                                    </dxe:ASPxMemo>
                                    <dxe:ASPxLabel ID="lblContadorMemoRecomendacoes" runat="server"
                                        ClientInstanceName="lblContadorMemoRecomendacoes" Font-Bold="True"
                                        ForeColor="#999999">
                                    </dxe:ASPxLabel>



                                </td>
                            </tr>
                            <tr>
                                <td rowspan="1"></td>
                            </tr>
                            <tr>
                                <td rowspan="1">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td style="width: 287px">
                                                    <dxe:ASPxLabel runat="server" Text="Incluído Em:" ID="ASPxLabel5"></dxe:ASPxLabel>



                                                </td>
                                                <td style="width: 5px"></td>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Incluído Por:" ID="ASPxLabel3"></dxe:ASPxLabel>



                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 287px">
                                                    <dxe:ASPxDateEdit runat="server" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm" Width="100%" DisplayFormatString="dd/MM/yyyy HH:mm" ReadOnly="True" ClientInstanceName="dteDataInclusao" ID="dteDataInclusao">
                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black"></ReadOnlyStyle>

                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                    </dxe:ASPxDateEdit>



                                                </td>
                                                <td style="width: 5px"></td>
                                                <td style="width: 50%;">
                                                    <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtIncluidoPor" ID="txtIncluidoPor">
                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black"></ReadOnlyStyle>

                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                    </dxe:ASPxTextBox>



                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 287px; height: 5px"></td>
                                                <td style="width: 5px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 287px">
                                                    <dxe:ASPxLabel runat="server" Text="Última Alteração:" ID="ASPxLabel6"></dxe:ASPxLabel>



                                                </td>
                                                <td style="width: 5px"></td>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Alterado Por:" ID="ASPxLabel4"></dxe:ASPxLabel>



                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 287px">
                                                    <dxe:ASPxDateEdit runat="server" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm" Width="100%" DisplayFormatString="dd/MM/yyyy HH:mm" ReadOnly="True" ClientInstanceName="dteUltimaAlteracao" ID="dteUltimaAlteracao">
                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black"></ReadOnlyStyle>

                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                    </dxe:ASPxDateEdit>



                                                </td>
                                                <td style="width: 5px"></td>
                                                <td style="width: 50%;">
                                                    <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtAlteradoPor" ID="txtAlteradoPor">
                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black"></ReadOnlyStyle>

                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                    </dxe:ASPxTextBox>



                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" rowspan="1"></td>
                            </tr>
                            <tr>
                                <td align="right" rowspan="2">
                                    <table class="formulario-botoes" id="Table2" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False"
                                                        ClientInstanceName="btnExcluir" CausesValidation="False" Text="Excluir"
                                                        Width="90px" ID="btnExcluir">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnExcluirAnalise)
		onClick_btnExcluirAnalise(s, e);
}"></ClientSideEvents>
                                                    </dxe:ASPxButton>



                                                </td>
                                                <td>
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False"
                                                        ClientInstanceName="btnSalvar" CausesValidation="False" Text="Salvar"
                                                        Width="90px" ID="btnSalvar">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
		onClick_btnSalvar();
}"></ClientSideEvents>
                                                    </dxe:ASPxButton>


                                                </td>
                                                <td>
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                        Text="Fechar" Width="90px" ID="btnFechar">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if (window.onClick_btnCancelar)
	    onClick_btnCancelar();
}"></ClientSideEvents>
                                                    </dxe:ASPxButton>


                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>


        <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxcp:ASPxHiddenField>

        <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Default></Default>

                <Header></Header>

                <Cell></Cell>

                <GroupFooter Font-Bold="True"></GroupFooter>

                <Title Font-Bold="True"></Title>
            </Styles>
        </dxcp:ASPxGridViewExporter>

    </form>
</body>
</html>
