<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPrevisaoFinanceira.aspx.cs"
    Inherits="administracao_frmPrevisaoFinanceira" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function Trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";

            if (ddlDataPrevisao.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") A data de previsão deve ser informada.";
            }
            if (parseFloat(txtValorPrevisao.GetValue()) == 0) {
                numAux++;
                mensagem += "\n" + numAux + ") O campo valor da previsão deve ser informado.";
            }
            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = "Alguns dados são de preenchimento obrigatório:\n\n" + mensagem;
            }
            return mensagemErro_ValidaCamposFormulario;
        }

        function setMaxLength(textAreaElement, length) {
            textAreaElement.maxlength = length;
            ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
            ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
        }

        function onKeyUpOrChange(evt) {
            processTextAreaText(ASPxClientUtils.GetEventSource(evt));
        }

        function processTextAreaText(textAreaElement) {
            var maxLength = textAreaElement.maxlength;
            var text = textAreaElement.value;
            var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
            if (maxLength != 0 && text.length > maxLength)
                textAreaElement.value = text.substr(0, maxLength);
            else {
                lblCantCaraterOb.SetText(text.length);
            }
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function onInit_mmObjeto(s, e) {
            try { return setMaxLength(s.GetInputElement(), 2000); }
            catch (e) { }
        }

        function onInit_mmObservacoes(s, e) {
            try { return setMaxLength(s.GetInputElement(), 2000); }
            catch (e) { }
        }

        function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
            // o método será chamado por meio do objeto pnCallBack
            hfGeral.Set("StatusSalvar", "0");
            pnCallback.PerformCallback(TipoOperacao);
            return false;
        }

        function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
            // o método será chamado por meio do objeto pnCallBack
            hfGeral.Set("StatusSalvar", "0");
            pnCallback.PerformCallback(TipoOperacao);
            return false;
        }

        // **************************************************************************************
        // - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
        // **************************************************************************************

        function LimpaCamposFormulario() {

            ddlDataPrevisao.SetValue(null);
            txtValorPrevisao.SetText("");
            mmObservacao.SetText("");
            txtDataInclusao.SetText("");
            txtUsuarioInclusao.SetText("");
            desabilitaHabilitaComponentes();
        }

        // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
        function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
            if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoPrevisaoFinanceira;CodigoContrato;DataInclusao;ValorPrevisto;ObservacoesPrevisaoFinanceira;UsuarioInclusao;DataPrevisao', MontaCamposFormulario);
            }
        }

        function MontaCamposFormulario(values) {


            var CodigoPrevisaoFinanceira = (values[0] != null ? values[0] : "");
            var CodigoContrato = (values[1] != null ? values[1] : null);
            var DataInclusao = (values[2] != null ? values[2] : "");
            var ValorPrevisto = (values[3] != null ? values[3] : null);
            var ObservacoesPrevisaoFinanceira = (values[4] != null ? values[4] : "");
            var UsuarioInclusao = (values[5] != null ? values[5] : null);
            var DataPrevisao = (values[6] != null ? values[6] : "");

            ddlDataPrevisao.SetValue(DataPrevisao)
            txtValorPrevisao.SetText(ValorPrevisto != null ? ValorPrevisto.toString().replace('.', ',') : "");
            mmObservacao.SetText(ObservacoesPrevisaoFinanceira);
            txtDataInclusao.SetText(DataInclusao);
            txtUsuarioInclusao.SetText(UsuarioInclusao);


            desabilitaHabilitaComponentes()
        }


        // ---------------------------------------------------------------------------------
        // Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
        // ---------------------------------------------------------------------------------

        function desabilitaHabilitaComponentes() {
            ddlDataPrevisao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            txtValorPrevisao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            mmObservacao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
        }

        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

            fechaTelaEdicao();
        }

        function fechaTelaEdicao() {
           
            onClick_btnCancelar();
        }   
       
    </script>
    <style type="text/css">
        .style5
        {
            width: 5px;
        }
        .style7
        {
            width: 120px;
        }
        .style1
        {
            height: 10px;
        }
        .style13
        {
            width: 110px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%" OnCallback="pnCallback_Callback">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                </dxhf:ASPxHiddenField>
                                        <table class="headerGrid">
                        <tr>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel25" runat="server" Text="Nº Contrato:">
                                </dxtv:ASPxLabel>
                            </td>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel29" runat="server" Text="Tipo de Contrato:">
                                </dxtv:ASPxLabel>
                            </td>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel28" runat="server" Text="Status:">
                                </dxtv:ASPxLabel>
                            </td>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel27" runat="server" Text="Início da Vigência:">
                                </dxtv:ASPxLabel>
                            </td>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel26" runat="server" Text="Término da Vigência:">
                                </dxtv:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxtv:ASPxTextBox ID="txtNumeroContrato" runat="server" ClientInstanceName="txtNumeroContrato" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </td>
                            <td>
                                <dxtv:ASPxTextBox ID="txtTipoContrato" runat="server" ClientInstanceName="txtTipoContrato" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </td>
                            <td>
                                <dxtv:ASPxTextBox ID="txtStatusContrato" runat="server" ClientInstanceName="txtStatusContrato" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </td>
                            <td>
                                <dxtv:ASPxTextBox ID="txtInicioVigencia" runat="server" ClientInstanceName="txtInicioVigencia" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </td>
                            <td>
                                <dxtv:ASPxTextBox ID="txtTerminoVigencia" runat="server" ClientInstanceName="txtTerminoVigencia" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoPrevisaoFinanceira"
                                    AutoGenerateColumns="False"  ID="gvDados"
                                    OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                    OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" Width="100%"
                                    OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	//OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
     }

     if(e.buttonID == &quot;btnXLS&quot;)
     {
        onClickBarraNavegacao(&quot;btnXLS&quot;, gvDados, pcDados);
     }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
        hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
" Init="function(s, e) {
	 var sHeight = Math.max(0, document.documentElement.clientHeight) - 70;
        s.SetHeight(sHeight);
}"></ClientSideEvents>
                                    <TotalSummary>
                                        <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorPrevisto" ShowInColumn="Valor Previsão"
                                            ShowInGroupFooterColumn="Valor Previsão" SummaryType="Sum" />
                                    </TotalSummary>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="95px" VisibleIndex="0">
                                            <CustomButtons>
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
                                                            <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick1">
                                                                <Paddings Padding="0px" />
                                                                <Items>
                                                                    <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                        <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                        </Image>
                                                                    </dxtv:MenuItem>
                                                                    <dxtv:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                        <Items>
                                                                            <dxtv:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                </Image>
                                                                            </dxtv:MenuItem>
                                                                            <dxtv:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                </Image>
                                                                            </dxtv:MenuItem>
                                                                            <dxtv:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                </Image>
                                                                            </dxtv:MenuItem>
                                                                            <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                                                <Image Url="~/imagens/menuExportacao/html.png">
                                                                                </Image>
                                                                            </dxtv:MenuItem>
                                                                            <dxtv:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                                </Image>
                                                                            </dxtv:MenuItem>
                                                                        </Items>
                                                                        <Image Url="~/imagens/botoes/btnDownload.png">
                                                                        </Image>
                                                                    </dxtv:MenuItem>
                                                                    <dxtv:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                                        <Items>
                                                                            <dxtv:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                <Image IconID="save_save_16x16">
                                                                                </Image>
                                                                            </dxtv:MenuItem>
                                                                            <dxtv:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                <Image IconID="actions_reset_16x16">
                                                                                </Image>
                                                                            </dxtv:MenuItem>
                                                                        </Items>
                                                                        <Image Url="~/imagens/botoes/layout.png">
                                                                        </Image>
                                                                    </dxtv:MenuItem>
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
                                                            </dxtv:ASPxMenu>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <FooterTemplate>
                                                TOTAL:
                                            </FooterTemplate>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ValorPrevisto" Caption="Valor Previsão"
                                            VisibleIndex="3" Width="120px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="False" />
                                            <Settings AutoFilterCondition="Contains"></Settings>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="1" Caption="Data Inclusão"
                                            FieldName="DataInclusao" Width="100px">
                                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="False" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ObservacoesPrevisaoFinanceira" ShowInCustomizationForm="True"
                                            VisibleIndex="4" Caption="Observações">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataDateColumn Caption="Data Previsão" FieldName="DataPrevisao" ShowInCustomizationForm="True"
                                            VisibleIndex="2" Width="125px">
                                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                            </PropertiesDateEdit>
                                            <Settings AllowAutoFilter="False" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="UsuarioInclusao" 
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoContrato" 
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                    </SettingsPager>
                                    <Settings ShowHeaderFilterBlankItems="False" ShowStatusBar="Visible" VerticalScrollBarMode="Visible"
                                        ShowFooter="True"></Settings>
                                    <SettingsText></SettingsText>
                                    <Styles>
                                        <StatusBar Font-Bold="True" Font-Size="Small" ForeColor="Red">
                                            <Paddings Padding="0px" />
                                        </StatusBar>
                                    </Styles>
                                    <Templates>
                                        <StatusBar>
                                            <%=textoStatus %>
                                        </StatusBar>
                                    </Templates>
                                </dxwgv:ASPxGridView>
<dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="gvExporter" OnRenderBrick="gvExporter_RenderBrick">
    <Styles>
        <Header Font-Bold="True">
        </Header>
        <Cell>
        </Cell>
        <Footer Font-Bold="True">
        </Footer>
        <GroupFooter Font-Bold="True">
        </GroupFooter>
        <GroupRow Font-Bold="True">
        </GroupRow>
        <Title Font-Bold="True"></Title>
    </Styles>
</dxwgv:ASPxGridViewExporter>
                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
                                    HeaderText="Previsão Financeira" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                    ShowCloseButton="False" Width="600px"  ID="pcDados">
                                    <ContentStyle>
                                        <Paddings Padding="5px"></Paddings>
                                    </ContentStyle>
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td class="style5" style="padding-top: 10px">
                                                            &nbsp;
                                                        </td>
                                                        <td >
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="lblDataPrevisão" runat="server" 
                                                                                Text="Data Previsão:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td width="15">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="padding-right: 5px;" align="left">
                                                                            <dxe:ASPxLabel ID="ASPxLabel24" runat="server" 
                                                                                Text="Valor Previsão (R$):">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 5px;" class="style13">
                                                                            <dxe:ASPxDateEdit ID="ddlDataPrevisao" runat="server" ClientInstanceName="ddlDataPrevisao"
                                                                                DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                 Width="100%">
                                                                                <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                </CalendarProperties>
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td width="15">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left" style="padding-right: 5px;">
                                                                            <dxe:ASPxTextBox ID="txtValorPrevisao" runat="server" ClientEnabled="False" ClientInstanceName="txtValorPrevisao"
                                                                                DisplayFormatString="{0:n2}"  HorizontalAlign="Right"
                                                                                Width="150px">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <ValidationSettings ErrorDisplayMode="None">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td class="style5" style="padding-top: 10px">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td >
                                                                            <dxe:ASPxLabel ID="lblObservacoes" runat="server" ClientInstanceName="lblObservacoes"
                                                                                EncodeHtml="False"  Text="Observações: &amp;nbsp;">
                                                                            </dxe:ASPxLabel>
                                                                            <dxe:ASPxLabel ID="lblCantCaraterOb" runat="server" ClientInstanceName="lblCantCaraterOb"
                                                                                 ForeColor="Silver" Text="0">
                                                                            </dxe:ASPxLabel>
                                                                            <dxe:ASPxLabel ID="lblDe250Ob" runat="server" ClientInstanceName="lblDe250Ob" EncodeHtml="False"
                                                                                 ForeColor="Silver" Text="&amp;nbsp;de 2000">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxMemo ID="mmObservacao" runat="server" ClientInstanceName="mmObservacao"
                                                                                 Rows="6" Width="100%" Height="130px">
                                                                                <ClientSideEvents Init="function(s, e) {
	onInit_mmObservacoes(s, e);
}" ValueChanged="function(s, e) {
	
}" />
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" Init="function(s, e) {
	onInit_mmObservacoes(s, e);
}"></ClientSideEvents>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style1">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
                                                                                <tr>
                                                                                    <td class="style7">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel15" runat="server" 
                                                                                            Text="Data Inclusão:">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel16" runat="server" 
                                                                                            Text="Incluído Por:">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style7" style="padding-right: 10px;">
                                                                                        <dxe:ASPxTextBox ID="txtDataInclusao" runat="server" ClientEnabled="False" ClientInstanceName="txtDataInclusao"
                                                                                             Width="100%" DisplayFormatString="{0:dd/MM/yyyy}">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="txtUsuarioInclusao" runat="server" ClientEnabled="False" ClientInstanceName="txtUsuarioInclusao"
                                                                                             Width="100%">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <table>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                                                 Text="Salvar" Width="100px">
                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	{
		    onClick_btnSalvar();
	}
}" />
                                                                                                <Paddings Padding="0px" />
                                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                                                <Paddings Padding="0px"></Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                                                 Text="Fechar" Width="90px">
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
                                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                                                                </Paddings>
                                                                                            </dxe:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td class="style5">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagem" HeaderText=""
                                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                    ShowHeader="False" Width="270px"  ID="pcMensagem"
                                    CloseAction="None" Modal="True">
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="" align="center">
                                                        </td>
                                                        <td style="width: 70px" align="center" rowspan="3">
                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                                ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
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
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) 
{
    try
	{		
		window.parent.parent.gvDados.PerformCallback();
		}catch(e)
	{}
	try
	{		
		window.parent.gvDados.PerformCallback();
		}catch(e)
	{}
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Previsao Financeira incluída com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Previsao Financeira alterada com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Previsao Financeira excluída com sucesso!&quot;);	
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
