<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAditivosContratos.aspx.cs" Inherits="administracao_frmAditivosContratos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">var pastaImagens = "../../ imagens";</script>
    <script type="text/javascript" language="javascript" src="../../scripts/_Strings.js"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/barraNavegacao.js"></script>
    <link href="../../estilos/frmContratosNovoPopup.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function Trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function incluiAditivoContrato() {
            var sUrl = '../../administracao/CadastroAditivoContrato.aspx';

            window.top.showModal2(sUrl, 'Aditivo', null, null, null, null);
        }

        function verificaTipoContrato(mudaValor) {
            if (ddlDataPrazo.cp_RO.toString() != "S") {

                var tipoContrato = ddlAditivar.GetValue();
                ddlTipoInstrumento.SetEnabled(TipoOperacao == "Incluir");

                var contratacao = ddlTipoInstrumento.GetText();

                if (contratacao == 'Termo de Encerramento de Contrato' || contratacao == 'TEC') {
                    tipoContrato = 'TC';
                    ddlAditivar.SetEnabled(false);
                }
                else
                    ddlAditivar.SetEnabled(TipoOperacao == "Incluir");

                if (tipoContrato == 'PR') {
                    ddlDataPrazo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtValorAditivo.SetEnabled(false);
                    if (mudaValor) {
                        txtValorAditivo.SetText("");
                    }
                    txtNovoValor.SetEnabled(false);
                    atualizaNovoValor();
                } else if (tipoContrato == 'VL') {
                    ddlDataPrazo.SetEnabled(false);
                    txtValorAditivo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtNovoValor.SetEnabled(false);

                    if (mudaValor) {
                        ddlDataPrazo.SetValue(null);
                    }

                    atualizaNovoValor();

                } else if (tipoContrato == 'PV') {
                    ddlDataPrazo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtValorAditivo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtNovoValor.SetEnabled(false);
                    atualizaNovoValor();

                } else if (tipoContrato == 'TM') {
                    txtValorAditivo.SetEnabled(false);

                    if (mudaValor) {
                        txtValorAditivo.SetText("");
                        ddlDataPrazo.SetValue(null);
                        txtNovoValor.SetText("");
                    }
                    ddlDataPrazo.SetEnabled(false);
                    txtNovoValor.SetEnabled(false);


                } else if (tipoContrato == 'TC') {
                    ddlDataPrazo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtNovoValor.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
                    txtValorAditivo.SetEnabled(false);

                    if (mudaValor) {
                        ddlAditivar.SetValue(null);
                        txtValorAditivo.SetText("");
                        txtNovoValor.SetText("");
                    }
                } else {
                    txtValorAditivo.SetEnabled(false);

                    if (mudaValor) {
                        txtValorAditivo.SetText("");
                        ddlDataPrazo.SetValue(null);
                        //txtNovoValor.SetText("");
                    }
                    ddlDataPrazo.SetEnabled(false);
                    txtNovoValor.SetEnabled(false);
                }
            }
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
            ddlTipoInstrumento.SetValue(null);
            txtNumeroInstrumento.SetText(pnCallback.cp_NumeroNovoInstrumento);
            ddlAditivar.SetValue(null);
            verificaTipoContrato(true);
            txtValorAditivo.SetText("");
            //            txtNovoValor.SetText("");
            txtValorDoContrato.SetText(pnCallback.cp_ValorContrato.toString().replace('.', ','));
            txtNovoValor.SetText(pnCallback.cp_ValorContrato.toString().replace('.', ','));
            ddlDataPrazo.SetValue(null);
            mmMotivo.SetText("");
            txtDataInclusao.SetText("");
            txtUsuarioInclusao.SetText("");
            txtDataAprovacao.SetText("");
            txtUsuarioAprovacao.SetText("");
            atualizaNovoValor();
            desabilitaHabilitaComponentes();
        }

        // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
        function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
            if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoAditivoContrato;CodigoTipoContratoAditivo;NumeroContratoAditivo;TipoAditivo;NovoValorContrato;NovaDataVigencia;DescricaoMotivoAditivo;DataInclusao;UsuarioInclusao;DataAprovacaoAditivo;UsuarioAprovacao;ValorContrato;ValorAditivo', MontaCamposFormulario);
            }
        }

        function MontaCamposFormulario(values) {


            var codigoAditivoContrato = (values[0] != null ? values[0] : "");
            var tipoInstrumento = (values[1] != null ? values[1] : null);
            var numeroInstrumento = (values[2] != null ? values[2] : "");
            var aditivar = (values[3] != null ? values[3] : null);
            var novoValor = (values[4] != null ? values[4] : "");
            var dataPrazo = (values[5] != null ? values[5] : null);
            var motivo = (values[6] != null ? values[6] : "");
            var dataInclusao = (values[7] != null ? values[7] : "");
            var usuarioInclusao = (values[8] != null ? values[8] : "");
            var dataAprovacao = (values[9] != null ? values[9] : "");
            var usuarioAprovacao = (values[10] != null ? values[10] : "");
            var valorContrato = (values[11] != null ? values[11] : "");
            var valorAditivo = (values[12] != null ? values[12] : "");

            ddlTipoInstrumento.SetValue(tipoInstrumento);
            txtNumeroInstrumento.SetText(numeroInstrumento);

            if ((tipoInstrumento == 'Termo de Encerramento de Contrato' || tipoInstrumento == 'TEC'))
                ddlAditivar.SetValue(null);
            else
                ddlAditivar.SetValue(aditivar);

            verificaTipoContrato(true);
            txtValorAditivo.SetText(valorAditivo != null ? valorAditivo.toString().replace('.', ',') : "");
            txtValorDoContrato.SetText(valorContrato != null ? valorContrato.toString().replace('.', ',') : "");
            txtNovoValor.SetText(novoValor != null ? novoValor.toString().replace('.', ',') : "");
            ddlDataPrazo.SetValue(dataPrazo);
            mmMotivo.SetText(motivo);
            txtDataInclusao.SetText(dataInclusao);
            txtUsuarioInclusao.SetText(usuarioInclusao);
            txtDataAprovacao.SetText(dataAprovacao);
            txtUsuarioAprovacao.SetText(usuarioAprovacao);
            desabilitaHabilitaComponentes();
        }

        function atualizaNovoValor() {
            var novoValor;

            if (txtValorDoContrato.GetText() != "") {
                try {
                    var valorAditivo = parseFloat(txtValorAditivo.GetValue().toString().replace(',', '.'));
                    var valorContrato = parseFloat(txtValorDoContrato.GetValue().toString().replace(',', '.'));
                    if (valorAditivo + valorContrato < 0) {
                        window.top.mostraMensagem('Não é possível colocar um valor de aditivo negativo maior que o valor do contrato', 'atencao', true, false, null);
                        txtValorAditivo.SetFocus();
                        return;
                    }
                    else
                        novoValor = valorAditivo + valorContrato;

                } catch (e) { }
            }

            txtNovoValor.SetText(novoValor != null ? novoValor.toString().replace('.', ',') : "");
        }

        // ---------------------------------------------------------------------------------
        // Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
        // ---------------------------------------------------------------------------------

        function desabilitaHabilitaComponentes() {
            ddlTipoInstrumento.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            txtNumeroInstrumento.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            ddlAditivar.SetEnabled(window.TipoOperacao && TipoOperacao == "Incluir");
            verificaTipoContrato(false);
            mmMotivo.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
        }
        //----------- Mensagem modificação con sucesso..!!!
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
        .style5 {
            width: 5px;
        }

        .style7 {
            width: 120px;
        }

        .style1 {
            height: 10px;
        }

        .style12 {
            width: 140px;
        }

        .style13 {
            width: 110px;
        }
    
        .dxmLite {
            color: Black;
        }
    
        .dxmLite {
            font: 12px Tahoma, Geneva, sans-serif;
        }

        .dxmLite {
            color: Black;
        }

        .dxmLite {
            font: 12px Tahoma, Geneva, sans-serif;
        }

        </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <div id="divLinha1" style="display: flex; flex-direction: row; padding-bottom: 10px">
                <div id="divCampoNumeroContrato" class="label-mais-campo" style="flex-grow: 1">
                    <div>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblNumeroContrato"
                            Text="Número do Contrato:">
                        </dxe:ASPxLabel>
                    </div>
                    <div>
                        <dxe:ASPxTextBox ID="txtNumeroContrato_tabItens" runat="server" ClientInstanceName="txtNumeroContrato_tabItens"
                            MaxLength="50" TabIndex="1" Width="100%" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxTextBox>
                    </div>
                </div>
                <div id="divCampoTipoContrato" class="label-mais-campo" style="flex-grow: 1">
                    <div>
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" ClientInstanceName="lblTipoContrato"
                            Text="Tipo de Contrato:">
                        </dxe:ASPxLabel>
                    </div>
                    <div>
                        <dxe:ASPxComboBox ID="ddlTipoContrato_tabItens" runat="server" ClientInstanceName="ddlTipoContrato_tabItens"
                            TabIndex="2" TextField="DescricaoTipoContrato"
                            ValueField="CodigoTipoContrato" ValueType="System.Int32" Width="100%" ReadOnly="True">
                            <Items>
                                <dxe:ListEditItem Text="Ativo" Value="0" />
                                <dxe:ListEditItem Text="N&#227;o Ativo" Value="0" />
                            </Items>
                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxComboBox>
                    </div>
                </div>
                <div id="divCampoStatus" class="label-mais-campo" style="flex-grow: 1">
                    <div>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" ClientInstanceName="lblTipoContrato"
                            Text="Status">
                        </dxe:ASPxLabel>
                    </div>
                    <div>
                        <dxtv:ASPxComboBox ID="ddlStatusComplementarContrato_tabItens" runat="server" ClientInstanceName="ddlStatusComplementarContrato_tabItens" TabIndex="3" Width="100%" ReadOnly="True">
                            <Items>
                                <dxe:ListEditItem Text="Ativo" Value="A" />
                                <dxe:ListEditItem Text="Não Ativo" Value="I" />
                            </Items>
                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxtv:ASPxComboBox>
                    </div>
                </div>
                <div id="divCampoInicioVigencia" class="label-mais-campo" style="flex-grow: 1">
                    <div>
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                            Text="Início de Vigência">
                        </dxe:ASPxLabel>
                    </div>
                    <div>
                        <dxe:ASPxDateEdit ID="ddlInicioDeVigencia_tabItens" PopupVerticalAlign="TopSides" runat="server" ClientInstanceName="ddlInicioDeVigencia_tabItens"
                            DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>"
                            EncodeHtml="False" TabIndex="10" UseMaskBehavior="True"
                            Width="100%" ReadOnly="True">
                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                            </CalendarProperties>
                            <ClientSideEvents DateChanged="function(s, e) {
	//ddlTerminoDeVigencia.SetDate(s.GetValue());
	//calendar = ddlTerminoDeVigencia.GetCalendar();
  	//if ( calendar )
    //	calendar.minDate = new Date(s.GetValue());
}"
                                ValueChanged="function(s, e) {
	
}" />
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxDateEdit>
                    </div>
                </div>
                <div id="divCampoTerminoVigencia" class="label-mais-campo" style="flex-grow: 1">
                    <div>
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                            Text="Término de Vigência:">
                        </dxe:ASPxLabel>
                    </div>
                    <div>
                        <dxe:ASPxDateEdit ID="ddlTerminoDeVigencia_tabItens" PopupVerticalAlign="TopSides" runat="server" ClientInstanceName="ddlTerminoDeVigencia_tabItens"
                            DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>"
                            EncodeHtml="False" TabIndex="11" UseMaskBehavior="True"
                            Width="100%" ReadOnly="True">
                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                            </CalendarProperties>
                            <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxDateEdit>
                    </div>
                </div>
            </div>
            <div id="divLinha2" style="display: flex; flex-direction: column">

                <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="CodigoAditivoContrato" ClientInstanceName="gvDados" Width="100%" ID="gvDados">
                    <ClientSideEvents CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
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
"
                        FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                        Init="function(s, e) {
	var sHeight = Math.max(0, document.documentElement.clientHeight) - 65;
       s.SetHeight(sHeight);
}"></ClientSideEvents>

                    <Templates>
                        <StatusBar>
                            <dxe:ASPxLabel ID="lblStatus0" runat="server"
                                Text="Não é possível incluir, editar ou excluir aditivos. Este contrato possui um aditivo pendente de aprovação no fluxo. ">
                            </dxe:ASPxLabel>

                        </StatusBar>
                    </Templates>

                    <SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

                    <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" VerticalScrollBarMode="Visible"></Settings>

                    <SettingsBehavior AllowFocusedRow="True" AllowSort="False"></SettingsBehavior>

                    <SettingsPopup>
                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                    </SettingsPopup>
                    <Columns>
                        <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ButtonType="Image" ShowInCustomizationForm="True" Width="95px" VisibleIndex="0">
                            <CustomButtons>
                                <dxcp:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                    <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                </dxcp:GridViewCommandColumnCustomButton>
                                <dxcp:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                    <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                </dxcp:GridViewCommandColumnCustomButton>
                                <dxcp:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                    <Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
                                </dxcp:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td align="center">
                                            <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
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
                        </dxcp:GridViewCommandColumn>
                        <dxcp:GridViewDataTextColumn FieldName="NumeroContratoAditivo" ShowInCustomizationForm="True" Name="NumeroContratoAditivo" Width="140px" Caption="N&#250;mero Instrumento" VisibleIndex="1">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="DescricaoTipoContrato" ShowInCustomizationForm="True" Caption="Tipo Instrumento" VisibleIndex="2"></dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="DescricaoTipoAditivo" ShowInCustomizationForm="True" Width="110px" Caption="Aditivo de " VisibleIndex="3">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="ValorAditivo" ShowInCustomizationForm="True" Width="120px" Caption="Valor Aditivo" VisibleIndex="4">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}"></PropertiesTextEdit>

                            <Settings AllowAutoFilter="False"></Settings>

                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>

                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="NovoValorContrato" ShowInCustomizationForm="True" Width="135px" Caption="Novo Valor Contrato" VisibleIndex="5">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}"></PropertiesTextEdit>

                            <Settings AllowAutoFilter="False"></Settings>

                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>

                            <CellStyle HorizontalAlign="Right"></CellStyle>
                        </dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="NovaDataVigencia" ShowInCustomizationForm="True" Width="100px" Caption="Nova Vig&#234;ncia" VisibleIndex="6">
                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesTextEdit>

                            <Settings AllowAutoFilter="False" AutoFilterCondition="Contains"></Settings>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="DataInclusao" ShowInCustomizationForm="True" Width="100px" Caption="Data Inclus&#227;o" VisibleIndex="7">
                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesTextEdit>

                            <Settings AllowAutoFilter="False"></Settings>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="UsuarioInclusao" ShowInCustomizationForm="True" Width="235px" Caption="Usu&#225;rio Inclus&#227;o" Visible="False" VisibleIndex="9"></dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="CodigoTipoContratoAditivo" ShowInCustomizationForm="True" Visible="False" VisibleIndex="10"></dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="DescricaoMotivoAditivo" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11"></dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="UsuarioAprovacao" ShowInCustomizationForm="True" Width="280px" Caption="Aprovador" Visible="False" VisibleIndex="12">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataDateColumn FieldName="DataAprovacaoAditivo" ShowInCustomizationForm="True" Width="125px" Caption="Data Aprova&#231;&#227;o" Visible="False" VisibleIndex="13">
                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}"></PropertiesDateEdit>

                            <Settings AllowAutoFilter="False"></Settings>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dxcp:GridViewDataDateColumn>
                        <dxcp:GridViewDataTextColumn FieldName="ValorContrato" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14"></dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="Editavel" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15"></dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="TipoAditivo" ShowInCustomizationForm="True" Caption="Aditivo de" Visible="False" VisibleIndex="16"></dxcp:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="CodigoWorkflow" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8"></dxcp:GridViewDataTextColumn>
                    </Columns>

                    <Styles>
                        <StatusBar ForeColor="Red"></StatusBar>
                    </Styles>
                </dxcp:ASPxGridView>

            </div>
        </div>

                <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" ExportedRowType="All" ID="gvExporter" OnRenderBrick="gvExporter_RenderBrick">
                    <Styles>
                        <Header Font-Bold="True"></Header>

                        <GroupFooter Font-Bold="True"></GroupFooter>

                        <GroupRow Font-Bold="True"></GroupRow>

                        <Title Font-Bold="True"></Title>
                    </Styles>
                </dxcp:ASPxGridViewExporter>

    </form>
</body>
</html>

