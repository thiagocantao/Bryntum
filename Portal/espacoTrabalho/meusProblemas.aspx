<%@ Page Language="C#" AutoEventWireup="true" CodeFile="meusProblemas.aspx.cs" Inherits="espacoTrabalho_meusProblemas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Meus Problemas</title>
    <link href="../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript">
        var pastaImagens = "../imagens";
        var mostrarRelatorioRiscoSelecionado = false;
        var retornoPopUp = null;

        function onEndLocal_pnCallback() {
            onEnd_pnCallback();
        }

        function mostraRelatorioRiscoSelecionado() {
            cbExportacao.PerformCallback();
            //window.top.showModal('../_Projetos/Relatorios/RiscoQuestaoSelecionada.aspx?CRQ='+hfGeral.Get("CodigoRiscoQuestao"), 'Relatório', screen.width - 20, screen.height - 270, '', null);

        }

        function callbackPopupComentarios(comentario) {
            btnAcao_pcComentarioAcao.SetVisible(false);
            btnCancelar_pcComentarioAcao.SetVisible(true);
            btnCancelar.SetText("Fechar");
            mmComentarioAcao.SetText(comentario);
            mmComentarioAcao.SetEnabled(false);
            pcComentarioAcao.Show();
        }

    </script>
    <script type="text/javascript" language="javascript" src="../scripts/barraNavegacao.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/_Strings.js"></script>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style3 {
            height: 10px;
        }

        .style5 {
            width: 25px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td style="width: 10px; height: 10px;"></td>
                    <td style="height: 10px">
                        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                            OnRenderBrick="ASPxGridViewExporter1_RenderBrick1">
                        </dxwgv:ASPxGridViewExporter>
                        <dxcb:ASPxCallback ID="cbExportacao" runat="server" ClientInstanceName="cbExportacao"
                            OnCallback="cbExportacao_Callback">
                            <ClientSideEvents EndCallback="function(s, e) {
	window.location = '../_Processos/Visualizacao/ExportacaoDados.aspx?exportType=pdf&amp;bInline=false';
}" />
                        </dxcb:ASPxCallback>
                    </td>
                    <td style="width: 10px; height: 10px;"></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            OnCallback="pnCallback_Callback" Width="100%" HideContentOnCallback="False" TabIndex="1">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                        CloseAction="CloseButton" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" PopupVerticalOffset="-40" ShowCloseButton="False"
                                        Width="850px" ID="pcDados">
                                        <ClientSideEvents CloseButtonClick="function(s, e) {
                        if (window.onClick_btnCancelar)
                            onClick_btnCancelar();
                        }"
                                            PopUp="function(s, e) {
                        }"
                                            Shown="function(s, e) {
                            //desabilitaHabilitaComponentes();
                            btnSalvar.Focus();
                            verificaVisibilidadeBotoes();
                        }"></ClientSideEvents>
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                                </dxhf:ASPxHiddenField>
                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/btnPDF.png" ToolTip="Imprimir Relatório"
                                                    ClientInstanceName="btnRelatorioRisco" EnableClientSideAPI="True" ID="btnRelatorioRisco"
                                                    Cursor="pointer">
                                                    <ClientSideEvents Click="function(s, e) {
                    mostraRelatorioRiscoSelecionado();
                }"></ClientSideEvents>
                                                </dxe:ASPxImage>
                                                <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="pcAbas"
                                                    Width="100%" ID="pcAbas">
                                                    <TabPages>
                                                        <dxtc:TabPage Name="tabPageRisco" Text="Questão">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <div runat="server" id="divTab1" style="overflow-y: auto;">
                                                                        <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="lblRiscoQuestao" runat="server"
                                                                                            Text="Risco:">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="txtRisco" runat="server" ClientInstanceName="txtRisco"
                                                                                            MaxLength="250" ReadOnly="True" Width="100%">
                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </ReadOnlyStyle>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style3"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblAnotacoes"
                                                                                            Text="Descrição:">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="txtDescricao" runat="server" ClientInstanceName="txtDescricao"
                                                                                            ReadOnly="True" Width="100%" Height="90px">
                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </ReadOnlyStyle>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="formulario-colunas">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxLabel ID="lblTipo" runat="server" ClientInstanceName="lblInicioReal"
                                                                                                            Text="Tipo:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td style="width: 120px">
                                                                                                        <dxe:ASPxLabel ID="lblProbabilidadeUrgencia" runat="server"
                                                                                                            Text="Probabilidade:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td style="width: 120px">
                                                                                                        <dxe:ASPxLabel ID="lblImpactoPrioridade" runat="server"
                                                                                                            Text="Impacto:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td class="style5">&nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxComboBox ID="ddlTipo" runat="server" ClientInstanceName="ddlTipo"
                                                                                                            Width="100%">
                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
                    atualizaImagemSeveridade();
                    }" />
                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
                    atualizaImagemSeveridade();
                    }"></ClientSideEvents>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxComboBox ID="ddlProbabilidade" runat="server" ClientInstanceName="ddlProbabilidade"
                                                                                                            Width="100%">
                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
    atualizaImagemSeveridade();
    }" />
                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
    atualizaImagemSeveridade();
    }"></ClientSideEvents>
                                                                                                            <Items>
                                                                                                                <dxe:ListEditItem Text="Alta" Value="A" />
                                                                                                                <dxe:ListEditItem Text="Média" Value="M" />
                                                                                                                <dxe:ListEditItem Text="Baixa" Value="B" />
                                                                                                            </Items>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxComboBox ID="ddlImpacto" runat="server" ClientInstanceName="ddlImpacto"
                                                                                                            Width="100%">
                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
    atualizaImagemSeveridade();
    }" />
                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
    atualizaImagemSeveridade();
    }"></ClientSideEvents>
                                                                                                            <Items>
                                                                                                                <dxe:ListEditItem Text="Alta" Value="A" />
                                                                                                                <dxe:ListEditItem Text="Média" Value="M" />
                                                                                                                <dxe:ListEditItem Text="Baixa" Value="B" />
                                                                                                            </Items>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td align="center" class="style5">
                                                                                                        <dxe:ASPxImage ID="imgSeveridade" runat="server" ClientInstanceName="imgSeveridade"
                                                                                                            ImageUrl="~/imagens/Branco.gif">
                                                                                                        </dxe:ASPxImage>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style3"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel14" runat="server" ClientInstanceName="lblCodigoUsuarioResponsavel"
                                                                                            Text="Responsável:">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxButtonEdit ID="txtResponsavel" runat="server" ClientInstanceName="txtResponsavel"
                                                                                            ReadOnly="True" Width="100%">
                                                                                            <ClientSideEvents ButtonClick="function(s, e) {
    e.processOnServer = false;
    buscaNomeBD(s);
    }"
                                                                                                TextChanged="function(s, e) {	
    buscaNomeBD(s);
    }" />
                                                                                            <ClientSideEvents ButtonClick="function(s, e) {
    e.processOnServer = false;
    buscaNomeBD(s);
    }"
                                                                                                TextChanged="function(s, e) {	
    buscaNomeBD(s);
    }"></ClientSideEvents>
                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </ReadOnlyStyle>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxButtonEdit>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="formulario-colunas">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="width: 355px">
                                                                                                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" ClientInstanceName="lblOrigemTarefa"
                                                                                                            Text="Incluído em/por:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxLabel ID="ASPxLabel13" runat="server" ClientInstanceName="lblDescricaoTarefa"
                                                                                                            Text="Publicado em/por:">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxTextBox ID="txtIncluidoEmPor" runat="server" BackColor="WhiteSmoke" ClientInstanceName="txtIncluidoEmPor"
                                                                                                            ReadOnly="True" Width="100%">
                                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </ReadOnlyStyle>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxTextBox ID="txtPublicadoEmPor" runat="server" BackColor="WhiteSmoke" ClientInstanceName="txtPublicadoEmPor"
                                                                                                            ReadOnly="True" Width="100%">
                                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </ReadOnlyStyle>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px"></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" EnableClientSideAPI="True"
                                                                                                        TabIndex="1" Text="Salvar" ValidationGroup="MKE"
                                                                                                        Width="90px">
                                                                                                        <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    if (window.onClick_btnSalvar)
    onClick_btnSalvar();    
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
                                                                                                <td></td>
                                                                                                <td>
                                                                                                    <dxe:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar"
                                                                                                        Text="Fechar" Width="90px">
                                                                                                        <ClientSideEvents Click="function(s, e) {	
    e.processOnServer = false;
    if (window.onClick_btnCancelar)
    onClick_btnCancelar();
    }" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                        <ClientSideEvents Click="function(s, e) {	
    e.processOnServer = false;
    if (window.onClick_btnCancelar)
    onClick_btnCancelar();
    }"></ClientSideEvents>
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtc:TabPage Name="tabPagetratamento" Text="Tratamento">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <div runat="server" id="divTab2" style="overflow-y: auto;">
                                                                        <asp:Panel runat="server" Width="100%" ID="pnMemosTratamento">
                                                                            <table id="tblMemostratamento" cellspacing="0" cellpadding="0"
                                                                                border="0" style="width: 100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="Consequências:" ClientInstanceName="lblOrigemTarefa"
                                                                                                ID="ASPxLabel6">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxMemo runat="server" Width="100%" ReadOnly="True" Enabled="False" ClientInstanceName="txtConsequencia"
                                                                                                ID="txtConsequencia" Height="90px">
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </DisabledStyle>
                                                                                            </dxe:ASPxMemo>
                                                                                            <dxe:ASPxLabel ID="lblContadorMemoConsequencias" runat="server" ClientInstanceName="lblContadorMemoConsequencias"
                                                                                                Font-Bold="True" ForeColor="#999999">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="style3"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="Tratamento:" ClientInstanceName="lblDescricaoTarefa"
                                                                                                ID="ASPxLabel15">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxMemo runat="server" Width="100%" ClientInstanceName="txtEstrategiaTratamento"
                                                                                                ID="txtEstrategiaTratamento" OnTextChanged="txtEstrategiaTratamento_TextChanged" ReadOnly="True" Height="90px">
                                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </ReadOnlyStyle>
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </DisabledStyle>
                                                                                            </dxe:ASPxMemo>
                                                                                            <dxe:ASPxLabel ID="lblContadorMemoTratamento" runat="server" ClientInstanceName="lblContadorMemoTratamento"
                                                                                                Font-Bold="True" ForeColor="#999999">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </asp:Panel>
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="height: 10px"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px">
                                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="formulario-colunas">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxLabel runat="server" Text="Limite Elimina&#231;&#227;o:" ClientInstanceName="lblTerminoReal"
                                                                                                            ID="lblLimiteEliminacaoResolucao">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxLabel runat="server" Text="Status:" ClientInstanceName="lblEsforcoReal"
                                                                                                            ID="ASPxLabel10">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxLabel runat="server" Text="Elimina&#231;&#227;o/Canc.:" ClientInstanceName="lblStatusTarefa"
                                                                                                            ID="lblEliminacaoResolucaoCancelamento">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxe:ASPxDateEdit runat="server" Width="100%" ReadOnly="True" ClientInstanceName="ddeLimiteEliminacao"
                                                                                                            ID="ddeLimiteEliminacao">
                                                                                                            <ValidationSettings ValidationGroup="MKE">
                                                                                                            </ValidationSettings>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxDateEdit>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtStatus"
                                                                                                            BackColor="#EBEBEB" ID="txtStatus">
                                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </ReadOnlyStyle>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtDataElimincaoCancelado"
                                                                                                            BackColor="#EBEBEB" ID="txtDataElimincaoCancelado">
                                                                                                            <MaskSettings IncludeLiterals="None"></MaskSettings>
                                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </ReadOnlyStyle>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxe:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px"></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table cellspacing="0" cellpadding="0" border="0" class="formulario-botoes">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td align="right" class="formulario-botao">
                                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnEliminar"
                                                                                                        EnableClientSideAPI="True" Wrap="False" Text="Eliminar" Width="90px"
                                                                                                        ToolTip="Eliminar risco" ID="btnEliminar">
                                                                                                        <ClientSideEvents Click="function(s, e) {
    onBtnAcaoEliminar_Click();
    e.processOnServer = false;
	btnAcao_pcComentarioAcao.SetText(s.GetText());
    }"></ClientSideEvents>
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                                <td align="right"></td>
                                                                                                <td align="right" class="formulario-botao">
                                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar"
                                                                                                        EnableClientSideAPI="True" Wrap="False" Text="Cancelar" Width="90px"
                                                                                                        ToolTip="Cancelar risco" ID="btnCancelar">
                                                                                                        <ClientSideEvents Click="function(s, e) {
    onBtnAcaoCancelar_Click();
    e.processOnServer = false;
	btnAcao_pcComentarioAcao.SetText(s.GetText());
    }"></ClientSideEvents>
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                                <td align="right"></td>
                                                                                                <td align="right" class="formulario-botao">
                                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar2" EnableClientSideAPI="True"
                                                                                                        Text="Salvar" ValidationGroup="MKE" Width="90px"
                                                                                                        TabIndex="1" ID="btnSalvar2">
                                                                                                        <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    if (window.onClick_btnSalvar)
    onClick_btnSalvar();    
    }"></ClientSideEvents>
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                                <td align="right"></td>
                                                                                                <td align="right" class="formulario-botao">
                                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar2" Text="Fechar" Width="90px"
                                                                                                        ID="btnFechar2">
                                                                                                        <ClientSideEvents Click="function(s, e) {	
    e.processOnServer = false;
    if (window.onClick_btnCancelar)
    onClick_btnCancelar();
    }"></ClientSideEvents>
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtc:TabPage Name="tabPageComentario" Text="Coment&#225;rios">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvComentarios" KeyFieldName="CodigoComentario"
                                                                                        AutoGenerateColumns="False" Width="100%"
                                                                                        ID="gvComentarios" OnRowUpdating="gvComentarios_RowUpdating" OnRowDeleting="gvComentarios_RowDeleting"
                                                                                        OnRowInserting="gvComentarios_RowInserting" OnCustomCallback="gvComentarios_CustomCallback">
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewCommandColumn Name="colunaControlesOriginal" ButtonRenderMode="Image" Width="75px" VisibleIndex="0" ShowEditButton="true"
                                                                                                ShowDeleteButton="true">
                                                                                                <HeaderTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <dxm:ASPxMenu ID="menu1" runat="server" BackColor="Transparent" ClientInstanceName="menu1"
                                                                                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init1">
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
                                                                                                                                <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                                                                            <dxtv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " ShowInCustomizationForm="False" VisibleIndex="1" Width="90px" Name="colunaControlesComentario" Visible="False">
                                                                                                <CustomButtons>
                                                                                                    <dxtv:GridViewCommandColumnCustomButton ID="btnFormularioCustomComentario">
                                                                                                        <Image Url="~/imagens/botoes/pFormulario.png">
                                                                                                        </Image>
                                                                                                    </dxtv:GridViewCommandColumnCustomButton>
                                                                                                </CustomButtons>
                                                                                            </dxtv:GridViewCommandColumn>
                                                                                            <dxwgv:GridViewDataMemoColumn FieldName="DescricaoComentario" Name="DescricaoComentario"
                                                                                                Caption="Coment&#225;rio" VisibleIndex="2">
                                                                                                <PropertiesMemoEdit Rows="6" ClientInstanceName="txtComentario">
                                                                                                    <ClientSideEvents Init="function(s, e) {
	
	document.getElementById('spC').innerText = s.GetText().length;
	return setMaxLength(s.GetInputElement(), 2000);
}"></ClientSideEvents>
                                                                                                    <Style></Style>
                                                                                                </PropertiesMemoEdit>
                                                                                                <EditFormSettings CaptionLocation="Top" Caption="Comentários: (&lt;span id='spC'&gt;0&lt;/span&gt; de 2000) "></EditFormSettings>
                                                                                            </dxwgv:GridViewDataMemoColumn>
                                                                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" ReadOnly="True" Name="NomeUsuario"
                                                                                                Caption="Respons&#225;vel" VisibleIndex="3" Width="200px">
                                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataDateColumn Caption="Data" FieldName="DataComentario" Name="DataComentario"
                                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Width="140px">
                                                                                                <PropertiesDateEdit DisplayFormatString="">
                                                                                                </PropertiesDateEdit>
                                                                                                <Settings ShowFilterRowMenu="True"></Settings>
                                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <CellStyle HorizontalAlign="Center">
                                                                                                </CellStyle>
                                                                                            </dxwgv:GridViewDataDateColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                                                        <ClientSideEvents CustomButtonClick="function(s, e) {
    if(e.buttonID == 'btnFormularioCustomComentario')
    {	
          s.GetRowValues(s.GetFocusedRowIndex(), 'DescricaoComentario', callbackPopupComentarios);
    }
}" />
                                                                                        <SettingsPager Mode="ShowAllRecords">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
                                                                                        </SettingsEditing>
                                                                                        <SettingsPopup>
                                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                AllowResize="True" Width="600px" Height="165px" />
                                                                                        </SettingsPopup>
                                                                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="237"></Settings>
                                                                                        <SettingsText PopupEditFormCaption="Coment&#225;rio"></SettingsText>
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar3" Text="Fechar" Width="90px"
                                                                                        ID="btnFechar3">
                                                                                        <ClientSideEvents Click="function(s, e) {	
    e.processOnServer = false;
    if (window.onClick_btnCancelar)
    onClick_btnCancelar();
    }"></ClientSideEvents>
                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtc:TabPage Name="tabPageToDoList" Text="Plano de A&#231;&#227;o">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <div runat="server" id="Content4Div"></div>
                                                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar4" Text="Fechar" Width="90px"
                                                                                        ID="btnFechar4">
                                                                                        <ClientSideEvents Click="function(s, e) {	
    e.processOnServer = false;
    if (window.onClick_btnCancelar)
    onClick_btnCancelar();
    }"></ClientSideEvents>
                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                        <dxtc:TabPage Name="tabPageAnexo" Text="Anexos">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <div runat="server" id="divTab5" style="overflow-y: auto;">
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <iframe id="frmAnexos" frameborder="0" height="450px" scrolling="no" src="" width="100%"></iframe>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px"></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <dxe:ASPxButton ID="btnFechar5" runat="server" ClientInstanceName="btnFechar5"
                                                                                        Text="Fechar" Width="90px">
                                                                                        <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                                        <Paddings Padding="0px" />
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dxtc:TabPage>
                                                    </TabPages>
                                                    <ClientSideEvents ActiveTabChanging="function(s, e) {
    e.cancel = podeMudarAba(s, e)
    }"></ClientSideEvents>
                                                </dxtc:ASPxPageControl>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoRiscoQuestao"
                                        AutoGenerateColumns="False" Width="100%"
                                        ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
                            OnGridFocusedRowChanged(s, true);
                            }"
                                            CustomButtonClick="function(s, e) 
{
	gvDados.SetFocusedRowIndex(e.visibleIndex);	
	e.processOnServer = false;                            
	if(e.buttonID == 'btnEditarCustom')
    {
          hfGeral.Set('TipoOperacao', 'Editar');	
		  onClickBarraNavegacao('Editar', gvDados, pcDados);
          pcAbas.SetActiveTab(pcAbas.GetTab(0)); 
          desabilitaHabilitaComponentes();
    }
    else if(e.buttonID == 'btnExcluirCustom')
    {
          onClickBarraNavegacao('Excluir', gvDados, pcDados);
    }
    else if(e.buttonID == 'btnFormularioCustom')
    {	
          OnGridFocusedRowChanged(gvDados, true);
          pcAbas.SetActiveTab(pcAbas.GetTab(0)); 
          hfGeral.Set('TipoOperacao', 'Consultar');
          desabilitaHabilitaComponentes();
          pcDados.Show();
    }
}"></ClientSideEvents>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="acao" Width="100px" Caption="A&#231;&#227;o"
                                                VisibleIndex="0">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
                                                        <Image Url="~/imagens/botoes/pFormulario.png">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                                <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <td align="center">
                                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                                                <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                            <dxwgv:GridViewDataTextColumn FieldName="CorRiscoQuestao" Name="CorRiscoQuestao"
                                                Width="50px" Caption="Grau" VisibleIndex="1">
                                                <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/{0}.gif'/&gt;" EncodeHtml="False">
                                                </PropertiesTextEdit>
                                                <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                    AllowSort="False" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoRiscoQuestao" Name="DescricaoRiscoQuestao"
                                                Caption="Questão" VisibleIndex="2">
                                                <Settings ShowFilterRowMenu="False" AllowHeaderFilter="False"></Settings>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoStatusRiscoQuestao" Name="colStatus"
                                                Width="120px" Caption="Status" VisibleIndex="3">
                                                <FilterCellStyle>
                                                </FilterCellStyle>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavel" Name="Responsavel"
                                                Width="200px" Caption="Respons&#225;vel" Visible="False" VisibleIndex="4">
                                                <FilterCellStyle>
                                                </FilterCellStyle>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" Name="col_NomeProjeto" Caption="Nome Projeto"
                                                VisibleIndex="4">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="DataLimiteResolucao" Name="DataLimiteResolucao"
                                                Width="130px" Caption="Limite Elimina&#231;&#227;o" VisibleIndex="5">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                                                </PropertiesDateEdit>
                                                <Settings AllowHeaderFilter="False" ShowFilterRowMenu="True"></Settings>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="DataEliminacaoCancelamento" Name="DataEliminacaoCancelamento"
                                                Width="130px" Caption="Data Elimina&#231;&#227;o" VisibleIndex="6">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                                                </PropertiesDateEdit>
                                                <Settings AllowHeaderFilter="False" ShowFilterRowMenu="True"></Settings>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Publicado" FieldName="Publicado" Name="DataPublicacao"
                                                ShowInCustomizationForm="True" VisibleIndex="7" Width="90px">
                                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                    <MaskSettings Mask="dd/MM/yyyy" />
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioResponsavel" Name="CodigoUsuarioResponsavel"
                                                Caption="CodigoUsuarioResponsavel" Visible="False" VisibleIndex="8">
                                                <Settings AllowHeaderFilter="False"></Settings>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DetalheRiscoQuestao" Name="col_DetalheRisco"
                                                Caption="Detalhe" Visible="False" VisibleIndex="9">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ProbabilidadePrioridade" Name="col_ProbabilidadePrioridade"
                                                Caption="Probabilidade Prioridade" Visible="False" VisibleIndex="10">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ImpactoUrgencia" Name="col_ImpactoUrgencia"
                                                Caption="Impacto Urgencia" Visible="False" VisibleIndex="11">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataStatusRiscoQuestao" Name="col_DataStatusRiscoQuestao"
                                                Caption="Data Status Risco Questao" Visible="False" VisibleIndex="12">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataPublicacao" Name="col_DataPublicacao"
                                                Caption="Data Publicacao" Visible="False" VisibleIndex="13">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoTipoRiscoQuestao" Name="col_DescricaoTipoRiscoQuestao"
                                                Caption="Descricao Tipo Risco Questao" Visible="False" VisibleIndex="14">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Severidade" Name="col_Severidade" Caption="Severidade"
                                                Visible="False" VisibleIndex="15">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="IncluidoEmPor" Name="col_IncluidoEmPor"
                                                Caption="Incluido Em Por" Visible="False" VisibleIndex="16">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="PublicaoEmPor" Name="col_PublicaoEmPor"
                                                Caption="Publicao Em Por" Visible="False" VisibleIndex="17">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ConsequenciaRiscoQuestao" Name="col_ConsequenciaRiscoQuestao"
                                                Caption="Consequencia Risco Questao" Visible="False" VisibleIndex="19">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="TratamentoRiscoQuestao" Name="col_tratamentoRiscoQuestao"
                                                Caption="tratamento Risco Questao" Visible="False" VisibleIndex="20">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" Name="CodigoProjeto" Visible="False"
                                                VisibleIndex="8">
                                                <EditFormSettings Visible="False"></EditFormSettings>
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Settings ShowHeaderFilterBlankItems="false" ShowGroupPanel="True" VerticalScrollBarMode="Visible"></Settings>
                                        <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                        <Styles>
                                            <HeaderPanel CssClass="janelaFiltro">
                                            </HeaderPanel>
                                            <FilterCell>
                                            </FilterCell>
                                            <FilterBar>
                                            </FilterBar>
                                        </Styles>
                                    </dxwgv:ASPxGridView>
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfAcao" ID="hfAcao">
                                    </dxhf:ASPxHiddenField>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents EndCallback="function(s, e) {
    onEndLocal_pnCallback();
    atualizaImagemSeveridade();
    // se o callback foi em virtude de uma a&#231;&#227;o de elimina&#231;&#227;o ou cancelamento do risco
    var acao = hfAcao.Get(&quot;Acao&quot;);
    if ( (null != acao) &amp;&amp; ('' != acao) )
    onPnCallBack_EndCallback_Acao();
    }"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                        <dxpc:ASPxPopupControl ID="pcComentarioAcao" runat="server" ClientInstanceName="pcComentarioAcao"
                            Width="700px" HeaderText="Comentário para a ação" PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" ShowCloseButton="False">
                            <ClientSideEvents Shown="function(s, e) {
                            // seta o foco para o campo de coment&#225;rio;
                            mmComentarioAcao.Focus();
                            }"></ClientSideEvents>
                            <ContentCollection>
                                <dxpc:PopupControlContentControl runat="server">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblComentarioAcao" EnableClientSideAPI="True"
                                                        ID="lblComentarioAcao">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxMemo ID="mmComentarioAcao" runat="server" ClientInstanceName="mmComentarioAcao"
                                                        EnableClientSideAPI="True" Height="100px"
                                                        Width="100%">
                                                        <ClientSideEvents KeyUp="function(s, e) {
                validaTamanhoTexto(s, e, 2000);
                }" />
                                                    </dxe:ASPxMemo>
                                                    <dxe:ASPxLabel ID="lblContadorMemoComentarioAcao" runat="server" ClientInstanceName="lblContadorMemoComentarioAcao"
                                                        Font-Bold="True" ForeColor="#999999">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px"></td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td align="right">
                                                                    <table>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <dxe:ASPxButton ID="btnAcao_pcComentarioAcao" runat="server" AutoPostBack="False"
                                                                                        ClientInstanceName="btnAcao_pcComentarioAcao" EnableClientSideAPI="True"
                                                                                        Text="xxx" Width="90px">
                                                                                        <ClientSideEvents Click="function(s, e) {
                    e.processOnServer = false;
                    onBtnAcao_pcComentarioAcao_Click();
                    }" />
                                                                                        <Paddings Padding="0px" />
                                                                                        <ClientSideEvents Click="function(s, e) {
                    e.processOnServer = false;
                    onBtnAcao_pcComentarioAcao_Click();
                    }"></ClientSideEvents>
                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td></td>
                                                                                <td align="right">
                                                                                    <dxe:ASPxButton ID="btnCancelar_pcComentarioAcao" runat="server" AutoPostBack="False"
                                                                                        ClientInstanceName="btnCancelar_pcComentarioAcao" EnableClientSideAPI="True"
                                                                                        Text="Fechar" Width="90px">
                                                                                        <ClientSideEvents Click="function(s, e) {
                    e.processOnServer = false;
                    pcComentarioAcao.Hide();	
                    }" />
                                                                                        <Paddings Padding="0px" />
                                                                                        <ClientSideEvents Click="function(s, e) {
                    e.processOnServer = false;
                    pcComentarioAcao.Hide();	
                    }"></ClientSideEvents>
                                                                                        <Paddings Padding="0px"></Paddings>
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
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxpc:PopupControlContentControl>
                            </ContentCollection>
                            <HeaderStyle Font-Bold="True"></HeaderStyle>
                        </dxpc:ASPxPopupControl>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
