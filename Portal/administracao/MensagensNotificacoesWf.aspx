<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MensagensNotificacoesWf.aspx.cs" Inherits="_Projetos_DadosProjeto_editaMensagens" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/ASPxListbox.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/barraNavegacao.js"></script>
<script type="text/javascript" language="javascript">

    function OnGridFocusedRowChanged(grid) {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoNotificacaoWf;Assunto;TextoNotificacao', MontaCamposFormulario);
    }

    function LimpaCamposFormulario() {
        var tOperacao = ""
        
        hfGeral.Set("CodigoNotificacaoWf", "");

        lblCantCarater.SetText("0");
        txtAssunto.SetText("");
        txtMensagem.SetText("");
    }

    function MontaCamposFormulario(values) {
        try {
           
            LimpaCamposFormulario();

            if (values) {
                var CodigoNotificacaoWf = (values[0] != null ? values[0] : "");
                hfGeral.Set("CodigoNotificacaoWf", CodigoNotificacaoWf);
                                
                txtAssunto.SetText((values[1] != null ? values[1] : ""));
                txtMensagem.SetText((values[2] != null ? values[2] : ""));

                lblCantCarater.SetText(txtMensagem.GetText().length);

                if (CodigoNotificacaoWf != "") {
                    lbDisponiveis.PerformCallback(CodigoNotificacaoWf);
                    lbSelecionados.PerformCallback(CodigoNotificacaoWf);
                }
            }
        } catch (e) { }
    }

    function novaMensagem() {
        var parametro = "-1";
        pcMensagem.SetActiveTabIndex(0);
        hfGeral.Set("CodigosSelecionados", "");
        lbDisponiveis.PerformCallback(parametro);
        lbSelecionados.PerformCallback(parametro);

        onClickBarraNavegacao('Incluir', gvDados, pcDados);
    }

    //------------------------------------------------------------funções relacionadas com a ListBox
    var delimitador = ";";

    function UpdateButtons() {
        try {
            btnADD.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbDisponiveis.GetSelectedItem() != null);
            btnADDTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbDisponiveis.GetItemCount() > 0);
            btnRMV.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbSelecionados.GetSelectedItem() != null);
            btnRMVTodos.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar" && lbSelecionados.GetItemCount() > 0);
        } catch (e) { }
    }

    function capturaCodigosInteressados() {
        var CodigosProjetosSelecionados = "";
        for (var i = 0; i < lbSelecionados.GetItemCount(); i++) {
            CodigosProjetosSelecionados += lbSelecionados.GetItem(i).value + ";";
        }
        hfGeral.Set("CodigosSelecionados", CodigosProjetosSelecionados);
    }
    //--------------------***********

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

        if (Trim(txtAssunto.GetText()) == '') {
            numAux++;
            mensagem += "\n" + numAux + ") O assunto deve ser informado.";
        }

        if (Trim(txtMensagem.GetText()) == '') {
            numAux++;
            mensagem += "\n" + numAux + ") A data prevista deve ser informada.";
        }

        if (lbSelecionados.GetItemCount() == 0) {
            numAux++;
            mensagem += "\n" + numAux + ") Pelo menos 1 destinatário deve ser selecionado.";
        }

        if (mensagem != "") {
            mensagemErro_ValidaCamposFormulario = mensagem;
        }

        return mensagemErro_ValidaCamposFormulario;
    }

    function desabilitaHabilitaComponentes() {
        txtAssunto.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
        txtMensagem.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
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
            lblCantCarater.SetText(text.length);
        }
    }

    function createEventHandler(funcName) {
        return new Function("event", funcName + "(event);");
    }

    function onInit_mmObjeto(s, e, length) {
        try {
            return setMaxLength(s.GetInputElement(), length);
        }
        catch (e) { }
    }

    function verificaAvancoWorkflow() {
        if (gvDados.GetVisibleRowsOnPage() == 0) {
            window.top.mostraMensagem("Para prosseguir com o fluxo, é necessário enviar pelo menos uma notificação aos envolvidos!", 'Atencao', true, false, null);
            return false;
        }

        return true;
    }

    function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
        // o método será chamado por meio do objeto pnCallBack
        hfGeral.Set("StatusSalvar", "0");
        pnCallback.PerformCallback(TipoOperacao);
        return false;
    }
</script>
<link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style1
        {
            height: 10px;
        }
        .style3
        {
            height: 5px;
        }
    </style>
</head>
<body  class="body">
    <form id="form1" runat="server">
        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
            OnCallback="pnCallback_Callback" Width="100%"><PanelCollection>
<dxp:PanelContent runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="WIDTH: 10px"></td><td>
    &nbsp;</td></tr><tr><td style="WIDTH: 10px"></td><td>
    <dxwgv:ASPxGridView runat="server" 
            ClientInstanceName="gvDados" KeyFieldName="CodigoNotificacaoWf" 
            AutoGenerateColumns="False" Width="100%"  
            ID="gvDados">
<ClientSideEvents FocusedRowChanged="function(s, e) 
{
	if(window.pcDados &amp;&amp; pcDados.IsVisible())
		OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
 {	
    if(e.buttonID == &quot;btnDetalhesCustom&quot;)
    {
        pcMensagem.SetActiveTabIndex(0);
		OnGridFocusedRowChanged(gvDados);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();    
    }
 }"></ClientSideEvents>
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="col_CustomButtons" 
        Width="50px" Caption="A&#231;&#227;o" VisibleIndex="0">
<CustomButtons>
<dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
<Image Url="~/imagens/botoes/pFormulario.png"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
</CustomButtons>

    <HeaderStyle HorizontalAlign="Center" />

<CellStyle HorizontalAlign="Center"></CellStyle>
    <HeaderTemplate>
        <%# (IncluiMsg) ? "<img src='../imagens/botoes/incluirReg02.png' style='cursor: pointer' onclick='novaMensagem();' />" : "<img style='cursor:default' src='../imagens/botoes/incluirRegDes.png' />"%>
    </HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="Assunto" Name="Assunto" Caption="Assunto" 
        VisibleIndex="1">    
    </dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DataNotificacao" Name="DataNotificacao" 
        Caption="Enviada Em" VisibleIndex="2" Width="160px">
    <propertiestextedit displayformatstring="{0: dd/MM/yyyy HH:mm}"></propertiestextedit>
    <HeaderStyle HorizontalAlign="Center" />
    <cellstyle horizontalalign="Center">
    </cellstyle>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="TextoNotificacao" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible"></Settings>

<SettingsText GroupPanel="Arraste um cabe&#231;alho da coluna aqui para agrupar por essa coluna" ></SettingsText>

<Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxwgv:ASPxGridView>
 </td></tr><tr><td style="WIDTH: 10px"></td><td>
        <dxpc:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True" 
            ClientInstanceName="pcDados"  
            HeaderText="Detalhes" Modal="True" PopupAction="None" 
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
            ShowCloseButton="False" Width="750px" PopupVerticalOffset="-15">
<ClientSideEvents PopUp="function(s, e) 
{
	
}"></ClientSideEvents>
            <contentstyle>
                <paddings padding="5px" />
<Paddings Padding="5px"></Paddings>
            </contentstyle>
            <contentcollection>
                <dxpc:PopupControlContentControl runat="server" 
                    SupportsDisabledAttribute="True">
                    <dxp:ASPxPanel ID="pnFormulario" runat="server" 
                        ClientInstanceName="pnFormulario" Width="100%">
                        <panelcollection>
                            <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <table cellpadding="0" cellspacing="0" class="style2">
                                    <tr>
                                        <td>
                                            <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="pcMensagem" Width="100%"  ID="pcMensagem"><TabPages>
<dxtc:TabPage Name="tbMensagem" Text="Mensagem"><ContentCollection>
<dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel runat="server" Text="Assunto:" ClientInstanceName="lblMsg0"  ID="lblMsg0"></dxe:ASPxLabel>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="150" 
                                                                                    ClientInstanceName="txtAssunto" ID="txtAssunto" 
                                                                                   >
                                                                                    <disabledstyle backcolor="#EBEBEB" forecolor="Black">
                                                                                    </disabledstyle>
                                                                                </dxe:ASPxTextBox>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style1">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel runat="server" Text="Mensagem:" ClientInstanceName="lblMsg"  ID="lblMsg"></dxe:ASPxLabel>

                                                                                <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCarater"  ForeColor="Silver" ID="lblCantCarater"></dxe:ASPxLabel>

                                                                                <dxe:ASPxLabel runat="server" Text="&amp;nbsp;de 4000" EncodeHtml="False" ClientInstanceName="lblDe250"  ForeColor="Silver" ID="lblDe250"></dxe:ASPxLabel>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxMemo runat="server" Rows="11" Width="100%" 
                                                                                    ClientInstanceName="txtMensagem" EnableClientSideAPI="True" 
                                                                                     ID="txtMensagem">
<ClientSideEvents Init="function(s, e) {
	onInit_mmObjeto(s, e, 4000, lblCantCarater);
}"></ClientSideEvents>

<ReadOnlyStyle BackColor="WhiteSmoke"></ReadOnlyStyle>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dxw:ContentControl>
</ContentCollection>
</dxtc:TabPage>
<dxtc:TabPage Name="tbDestinatarios" Text="Destinat&#225;rios"><ContentCollection>
<dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="widht: 340px">
                                                                                <dxe:ASPxLabel runat="server" Text="Dispon&#237;veis:" ClientInstanceName="lblSelecionado"  ID="ASPxLabel1"></dxe:ASPxLabel>

                                                                            </td>
                                                                            <td align="center">
                                                                            </td>
                                                                            <td style="WIDTH: 340px">
                                                                                <dxe:ASPxLabel runat="server" Text="Selecionados:" ClientInstanceName="lblSelecionado"  ID="lblSelecionado"></dxe:ASPxLabel>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="20" 
                                                                                     SelectionMode="Multiple" 
                                                                                    ImageFolder="~/App_Themes/Aqua/{0}/" ClientInstanceName="lbDisponiveis" 
                                                                                    EnableClientSideAPI="True" Width="100%" Height="200px" 
                                                                                    ID="lbDisponiveis" OnCallback="lbDisponiveis_Callback">
<ItemStyle BackColor="White">
<SelectedStyle BackColor="#FFE4AC"></SelectedStyle>
</ItemStyle>

<ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>

<ValidationSettings>
<ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png"></ErrorImage>

<ErrorFrameStyle ImageSpacing="4px">
<ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
</ErrorFrameStyle>
</ValidationSettings>

<DisabledStyle ForeColor="Black"></DisabledStyle>
</dxe:ASPxListBox>

                                                                            </td>
                                                                            <td align="center" style="WIDTH: 60px">
                                                                                <table>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="WIDTH: 41px; HEIGHT: 5px" valign="middle">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="WIDTH: 41px; HEIGHT: 28px" valign="middle">
                                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADDTodos" ClientEnabled="False" Text="&gt;&gt;" EncodeHtml="False" Width="50px" Height="25px" Font-Bold="True"  ID="btnADDTodos">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveTodosItens(lbDisponiveis,lbSelecionados);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="WIDTH: 41px; HEIGHT: 28px">
                                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADD" ClientEnabled="False" Text="&gt;" EncodeHtml="False" Width="50px" Height="25px" Font-Bold="True"  ID="btnADD">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbDisponiveis, lbSelecionados);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="WIDTH: 41px; HEIGHT: 28px">
                                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMV" ClientEnabled="False" Text="&lt;" EncodeHtml="False" Width="50px" Height="25px" Font-Bold="True"  ID="btnRMV">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	lb_moveItem(lbSelecionados, lbDisponiveis);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="WIDTH: 41px; HEIGHT: 28px">
                                                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMVTodos" ClientEnabled="False" Text="&lt;&lt;" EncodeHtml="False" Width="50px" Height="25px" Font-Bold="True"  ID="btnRMVTodos">
<ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
	lb_moveTodosItens(lbSelecionados, lbDisponiveis);
	UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxListBox runat="server" EnableSynchronization="True" EncodeHtml="False" 
                                                                                    Rows="20"  SelectionMode="Multiple" 
                                                                                    ImageFolder="~/App_Themes/Aqua/{0}/" ClientInstanceName="lbSelecionados" 
                                                                                    EnableClientSideAPI="True" Width="100%" Height="200px" 
                                                                                    ID="lbSelecionados" OnCallback="lbSelecionados_Callback">
<ItemStyle BackColor="White">
<SelectedStyle BackColor="#FFE4AC"></SelectedStyle>
</ItemStyle>

<ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>

<ValidationSettings>
<ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png"></ErrorImage>

<ErrorFrameStyle ImageSpacing="4px">
<ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
</ErrorFrameStyle>
</ValidationSettings>

<DisabledStyle ForeColor="Black"></DisabledStyle>
</dxe:ASPxListBox>

                                                                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfRiscosSelecionados" ID="hfRiscosSelecionados"></dxhf:ASPxHiddenField>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dxw:ContentControl>
</ContentCollection>
</dxtc:TabPage>
</TabPages>
                                                <contentstyle>
                                                    <paddings padding="5px" />
<Paddings Padding="5px"></Paddings>
                                                </contentstyle>
</dxtc:ASPxPageControl>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="90px" Height="5px"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	var textoConfirmacao = 'Ao salvar não será mais possível alterar ou excluir a mensagem.\nDeseja continuar?';
    if (window.onClick_btnSalvar &amp;&amp; confirm(textoConfirmacao))
	    onClick_btnSalvar();
}"></ClientSideEvents>

<Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxButton>

                                                        </td>
                                                        <td style="WIDTH: 10px">
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar" 
                                                                 Height="1px" Text="Fechar" Width="90px">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

<Paddings PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </panelcollection>
                    </dxp:ASPxPanel>
                </dxpc:PopupControlContentControl>
            </contentcollection>
        </dxpc:ASPxPopupControl>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxhf:ASPxHiddenField ID="hiddenField" runat="server" 
            ClientInstanceName="hiddenField">
        </dxhf:ASPxHiddenField>
        </td></tr></TBODY></table>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();

	if(s.cp_Msg != '')
		mostraDivSalvoPublicado(s.cp_Msg);
}"></ClientSideEvents>
</dxcp:ASPxCallbackPanel>
        &nbsp;<dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUsuarioIncluido" 
        HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False" 
        Width="270px"  ID="pcUsuarioIncluido">
     <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>


























 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>


























 </td></tr></TBODY></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

    </form>
</body>
</html>
