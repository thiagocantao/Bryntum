<%@ Page Language="C#" AutoEventWireup="true" CodeFile="meusRiscos.aspx.cs" Inherits="espacotrabalho_meusRiscos" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Meus Riscos</title>
    <link href="../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript">
        var pastaImagens = "../imagens";
        var mostrarRelatorioRiscoSelecionado = false;
        var retornoPopUp = null;

        function onEndLocal_pnCallback() {
            gvDados.Refresh();
            onEnd_pnCallback();
        }

        function mostraRelatorioRiscoSelecionado() {
            cbExportacao.PerformCallback();
            //window.top.showModal('../_Projetos/Relatorios/RiscoQuestaoSelecionada.aspx?CRQ=' + hfGeral.Get("CodigoRiscoQuestao"), 'Relatório', screen.width - 20, screen.height - 275, '', null);

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
        .style4 {
            height: 10px;
        }

        .style5 {
            width: 25px;
        }

        .style6 {
            height: 13px;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">

        <div id="ConteudoPrincipal">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" id="tabela">
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
                    <td style="width: 10px"></td>
                    <td>
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            OnCallback="pnCallback_Callback" Width="100%" HideContentOnCallback="False" TabIndex="1">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                        CloseAction="CloseButton" PopupVerticalOffset="-100" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                        PopupHorizontalOffset="-40" ShowCloseButton="True"
                                        Width="950px" Height="135px" Font-Names="Verdana" Font-Size="8pt" ID="pcDados">
                                        <ClientSideEvents CloseButtonClick="function(s, e) {
                        if (window.onClick_btnCancelar)
                            onClick_btnCancelar();
                        }"
                                            PopUp="function(s, e) {
                        }"
                                            Shown="function(s, e) {
                            btnSalvar.Focus();
                            verificaVisibilidadeBotoes();
                        }"></ClientSideEvents>
                                        <ContentStyle>
                                            <Paddings PaddingBottom="5px"></Paddings>
                                        </ContentStyle>
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                                </dxhf:ASPxHiddenField>
                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/btnPDF.png" ToolTip="Imprimir Relatório"
                                                    ClientInstanceName="btnRelatorioRisco" EnableClientSideAPI="True" Cursor="pointer"
                                                    ID="btnRelatorioRisco">
                                                    <ClientSideEvents Click="function(s, e) {
                    mostraRelatorioRiscoSelecionado();
                }"></ClientSideEvents>
                                                </dxe:ASPxImage>
                                                <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="pcAbas"
                                                    Width="100%" ID="pcAbas">
                                                    <TabPages>
                                                        <dxtc:TabPage Name="tabPageRisco" Text="Risco">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">
                                                                    <div runat="server" id="divTab1" style="overflow-y: auto;">
                                                                        <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel runat="server" Text="Risco:" Font-Names="Verdana" Font-Size="8pt"
                                                                                            ID="lblRiscoQuestao">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="250" ReadOnly="True" ClientInstanceName="txtRisco"
                                                                                            Font-Names="Verdana" Font-Size="8pt" ID="txtRisco">
                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </ReadOnlyStyle>
                                                                                            <DisabledStyle BackColor="#EBEBEB" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                                                                                                <border bordercolor="Silver"></border>
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-top: 5px;">
                                                                                        <dxtv:ASPxLabel ID="lblRiscoAssociado" runat="server" ClientInstanceName="lblRiscoAssociado" Font-Names="Verdana" Font-Size="8pt" Text="Risco (superior):">
                                                                                        </dxtv:ASPxLabel>

                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 10px">
                                                                                        <dxtv:ASPxTextBox ID="txtRiscoAssociado" runat="server" ClientInstanceName="txtRiscoAssociado" Font-Names="Verdana" Font-Size="8pt" MaxLength="250" ReadOnly="True" Width="100%">
                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </ReadOnlyStyle>
                                                                                            <DisabledStyle BackColor="#EBEBEB" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                                                                                                <border bordercolor="Silver" />
                                                                                            </DisabledStyle>
                                                                                        </dxtv:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                            <tr>
                                                                                                <td style="width: 175px; padding-top: 5px;">
                                                                                                    <dxtv:ASPxLabel ID="lblCustoRisco" runat="server" ClientInstanceName="lblCustoRisco" Font-Names="Verdana" Font-Size="8pt" Text="Custo:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                                <td style="padding-top: 5px">
                                                                                                    <dxtv:ASPxLabel ID="lblTipoRespostaRisco" runat="server" ClientInstanceName="lblTipoRespostaRisco" Font-Names="Verdana" Font-Size="8pt" Text="Tipo de Resposta:">
                                                                                                    </dxtv:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxtv:ASPxSpinEdit ID="spnCusto" runat="server" ClientInstanceName="spnCusto" DecimalPlaces="2" DisplayFormatString="c" EnableClientSideAPI="True" Font-Names="Verdana" Font-Size="8pt" Number="0" ReadOnly="True">
                                                                                                        <SpinButtons ClientVisible="False">
                                                                                                        </SpinButtons>
                                                                                                        <ReadOnlyStyle BackColor="#EBEBEB">
                                                                                                        </ReadOnlyStyle>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxSpinEdit>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxtv:ASPxTextBox ID="txtTipoResposta" runat="server" ClientInstanceName="txtTipoResposta" Font-Names="Verdana" Font-Size="8pt" MaxLength="250" ReadOnly="True" Width="100%">
                                                                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </ReadOnlyStyle>
                                                                                                        <DisabledStyle BackColor="#EBEBEB" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black">
                                                                                                            <border bordercolor="Silver" />
                                                                                                        </DisabledStyle>
                                                                                                    </dxtv:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-top: 5px">
                                                                                        <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblAnotacoes" Font-Names="Verdana" Font-Size="8pt" Text="Descrição:">
                                                                                        </dxtv:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtDescricao"
                                                                                            Font-Names="Verdana" Font-Size="8pt" ID="txtDescricao" Rows="6">
                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </ReadOnlyStyle>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-top: 5px;">
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxLabel ID="lblTipo" runat="server" ClientInstanceName="lblInicioReal" Font-Names="Verdana" Font-Size="8pt" Text="Tipo:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 10px"></td>
                                                                                                    <td style="width: 120px">
                                                                                                        <dxtv:ASPxLabel ID="lblProbabilidadeUrgencia" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Probabilidade:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 10px"></td>
                                                                                                    <td style="width: 120px">
                                                                                                        <dxtv:ASPxLabel ID="lblImpactoPrioridade" runat="server" Font-Names="Verdana" Font-Size="8pt" Text="Impacto:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 10px"></td>
                                                                                                    <td class="style5">&nbsp; </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxComboBox ID="ddlTipo" runat="server" ClientInstanceName="ddlTipo" Font-Names="Verdana" Font-Size="8pt" Width="100%">
                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
                    atualizaImagemSeveridade();
                    }" />
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxComboBox ID="ddlProbabilidade" runat="server" ClientInstanceName="ddlProbabilidade" Font-Names="Verdana" Font-Size="8pt" Width="100%">
                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
    atualizaImagemSeveridade();
    }" />
                                                                                                            <Items>
                                                                                                                <dxtv:ListEditItem Text="Alta" Value="A" />
                                                                                                                <dxtv:ListEditItem Text="Média" Value="M" />
                                                                                                                <dxtv:ListEditItem Text="Baixa" Value="B" />
                                                                                                            </Items>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxComboBox ID="ddlImpacto" runat="server" ClientInstanceName="ddlImpacto" Font-Names="Verdana" Font-Size="8pt" Width="100%">
                                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
    atualizaImagemSeveridade();
    }" />
                                                                                                            <Items>
                                                                                                                <dxtv:ListEditItem Text="Alta" Value="A" />
                                                                                                                <dxtv:ListEditItem Text="Média" Value="M" />
                                                                                                                <dxtv:ListEditItem Text="Baixa" Value="B" />
                                                                                                            </Items>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td align="center" class="style5">
                                                                                                        <dxtv:ASPxImage ID="imgSeveridade" runat="server" ClientInstanceName="imgSeveridade" ImageUrl="~/imagens/Branco.gif">
                                                                                                        </dxtv:ASPxImage>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-top: 5px">
                                                                                        <dxe:ASPxLabel runat="server" Text="Respons&#225;vel:" ClientInstanceName="lblCodigoUsuarioResponsavel"
                                                                                            Font-Names="Verdana" Font-Size="8pt" ID="ASPxLabel14">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxButtonEdit runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtResponsavel"
                                                                                            Font-Names="Verdana" Font-Size="8pt" ID="txtResponsavel">
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
                                                                                    <td style="padding-top: 5px;">
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="width: 355px">
                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel12" runat="server" ClientInstanceName="lblOrigemTarefa" Font-Names="Verdana" Font-Size="8pt" Text="Incluído em/por:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td style="width: 10px"></td>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxLabel ID="ASPxLabel13" runat="server" ClientInstanceName="lblDescricaoTarefa" Font-Names="Verdana" Font-Size="8pt" Text="Publicado em/por:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxTextBox ID="txtIncluidoEmPor" runat="server" ClientInstanceName="txtIncluidoEmPor" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" Width="100%">
                                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </ReadOnlyStyle>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxTextBox>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxTextBox ID="txtPublicadoEmPor" runat="server" ClientInstanceName="txtPublicadoEmPor" Font-Names="Verdana" Font-Size="8pt" ReadOnly="True" Width="100%">
                                                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </ReadOnlyStyle>
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>

                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="formulario-botao">
                                                                                                    <dxtv:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" EnableClientSideAPI="True" TabIndex="1" Text="Salvar" ValidationGroup="MKE" Width="90px">
                                                                                                        <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();    
    }" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </dxtv:ASPxButton>
                                                                                                </td>
                                                                                                <td class="formulario-botao">
                                                                                                    <dxtv:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="90px">
                                                                                                        <ClientSideEvents Click="function(s, e) {	
    e.processOnServer = false;
    if (window.onClick_btnCancelar)
        onClick_btnCancelar();
    }" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </dxtv:ASPxButton>
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
                                                                            <table style="width: 100%" id="tblMemostratamento" cellspacing="0" cellpadding="0"
                                                                                border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="Consequências:" ClientInstanceName="lblOrigemTarefa"
                                                                                                Font-Names="Verdana" Font-Size="8pt" ID="ASPxLabel6">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxMemo runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtConsequencia"
                                                                                                Font-Names="Verdana" Font-Size="8pt" ID="txtConsequencia" Rows="8">
                                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </ReadOnlyStyle>
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </DisabledStyle>
                                                                                            </dxe:ASPxMemo>
                                                                                            <dxe:ASPxLabel ID="lblContadorMemoConsequencias" runat="server" ClientInstanceName="lblContadorMemoConsequencias"
                                                                                                Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="#999999">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="style4"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="Estratégia Para Tratamento:" ClientInstanceName="lblDescricaoTarefa"
                                                                                                Font-Names="Verdana" Font-Size="8pt" ID="ASPxLabel15">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxMemo runat="server" Width="100%" ClientInstanceName="txtEstrategiaTratamento"
                                                                                                Font-Names="Verdana" Font-Size="8pt" ID="txtEstrategiaTratamento" Rows="8">
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                </DisabledStyle>
                                                                                            </dxe:ASPxMemo>
                                                                                            <dxe:ASPxLabel ID="lblContadorMemoTratamento" runat="server" ClientInstanceName="lblContadorMemoTratamento"
                                                                                                Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="#999999">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </asp:Panel>
                                                                        <table class="formulario formulario-colunas" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tbody>
                                                                                <tr class="formulario-linha">
                                                                                    <td class="formulario-label">
                                                                                        <dxe:ASPxLabel runat="server" Text="Limite Elimina&#231;&#227;o:" ClientInstanceName="lblTerminoReal"
                                                                                            ID="lblLimiteEliminacaoResolucao">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td class="formulario-label">
                                                                                        <dxe:ASPxLabel runat="server" Text="Status:" ClientInstanceName="lblEsforcoReal"
                                                                                            ID="ASPxLabel10">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td class="formulario-label">
                                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, riscos_eliminar_risco %>" ClientInstanceName="lblStatusTarefa"
                                                                                            ID="lblEliminacaoResolucaoCancelamento">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="formulario-linha">
                                                                                    <td>
                                                                                        <dxe:ASPxDateEdit runat="server" Width="100%" ClientInstanceName="ddeLimiteEliminacao"
                                                                                            ID="ddeLimiteEliminacao">
                                                                                            <ValidationSettings ValidationGroup="MKE">
                                                                                            </ValidationSettings>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                            <CalendarProperties>
                                                                                                <DayHeaderStyle />
                                                                                                <DayStyle />
                                                                                                <DayWeekendStyle>
                                                                                                </DayWeekendStyle>
                                                                                                <ButtonStyle>
                                                                                                </ButtonStyle>
                                                                                                <HeaderStyle />
                                                                                            </CalendarProperties>
                                                                                        </dxe:ASPxDateEdit>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtStatus"
                                                                                            ID="txtStatus" ClientEnabled="False">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtDataElimincaoCancelado"
                                                                                            ID="txtDataElimincaoCancelado" ClientEnabled="False">
                                                                                            <MaskSettings IncludeLiterals="None"></MaskSettings>
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                            </DisabledStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="formulario-botao">
                                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnEliminar"
                                                                                                        EnableClientSideAPI="True" Wrap="False" Text="<%$ Resources:traducao, riscos_eliminar_risco %>" Width="90px" Font-Names="Verdana"
                                                                                                        Font-Size="8pt" ToolTip="<%$ Resources:traducao, riscos_eliminar_risco %>" ID="btnEliminar0">
                                                                                                        <ClientSideEvents Click="function(s, e) {
    onBtnAcaoEliminar_Click();
    e.processOnServer = false;
	btnAcao_pcComentarioAcao.SetText(s.GetText());
    }"></ClientSideEvents>
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                                <td class="formulario-botao">
                                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar"
                                                                                                        EnableClientSideAPI="True" Wrap="False" Text="Cancelar" Width="90px" Font-Names="Verdana"
                                                                                                        Font-Size="8pt" ToolTip="Cancelar risco" ID="btnCancelar0">
                                                                                                        <ClientSideEvents Click="function(s, e) {
    onBtnAcaoCancelar_Click();
    e.processOnServer = false;
	btnAcao_pcComentarioAcao.SetText(s.GetText());
    }"></ClientSideEvents>
                                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                                    </dxe:ASPxButton>
                                                                                                </td>
                                                                                                <td class="formulario-botao">
                                                                                                    <dxe:ASPxButton ID="btnSalvar3" runat="server" ClientInstanceName="btnSalvar2" EnableClientSideAPI="True"
                                                                                                        Font-Names="Verdana" Font-Size="8pt" TabIndex="1" Text="Salvar" ValidationGroup="MKE"
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
                                                                                                <td class="formulario-botao">
                                                                                                    <dxe:ASPxButton ID="btnFechar6" runat="server" ClientInstanceName="btnFechar2" Font-Names="Verdana"
                                                                                                        Font-Size="8pt" Text="Fechar" Width="90px">
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
                                                        <dxtc:TabPage Name="tabPageComentario" Text="Coment&#225;rios">
                                                            <ContentCollection>
                                                                <dxw:ContentControl runat="server">

                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxwgv:ASPxGridView ID="gvComentarios" runat="server" AutoGenerateColumns="False"
                                                                                        ClientInstanceName="gvComentarios" KeyFieldName="CodigoComentario"
                                                                                        OnCustomCallback="gvComentarios_CustomCallback" OnRowDeleting="gvComentarios_RowDeleting"
                                                                                        OnRowInserting="gvComentarios_RowInserting" OnRowUpdating="gvComentarios_RowUpdating"
                                                                                        Width="100%" OnAfterPerformCallback="gvComentarios_AfterPerformCallback">
                                                                                        <SettingsBehavior AllowSort="False"></SettingsBehavior>
                                                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
                                                                                        </SettingsEditing>
                                                                                        <Settings VerticalScrollBarMode="Visible"></Settings>
                                                                                        <SettingsText PopupEditFormCaption="Coment&#225;rio"></SettingsText>
                                                                                        <ClientSideEvents CustomButtonClick="function(s, e) {
    if(e.buttonID == 'btnFormularioCustomComentario')
    {	
          s.GetRowValues(s.GetFocusedRowIndex(), 'DescricaoComentario', callbackPopupComentarios);
    }
}" />
                                                                                        <SettingsPager Mode="ShowAllRecords">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
                                                                                        <Settings VerticalScrollBarMode="Visible" />
                                                                                        <SettingsBehavior AllowSort="False" />
                                                                                        <SettingsPopup>
                                                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                AllowResize="True" Width="600px" />
                                                                                        </SettingsPopup>
                                                                                        <SettingsText PopupEditFormCaption="Comentário" />
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewCommandColumn Name="colunaControlesOriginal" ButtonRenderMode="Image" ShowInCustomizationForm="True" ShowEditButton="true"
                                                                                                ShowDeleteButton="true" VisibleIndex="0" Width="75px">
                                                                                                <HeaderTemplate>
                                                                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <dxm:ASPxMenu ID="menu1" runat="server" BackColor="Transparent" ClientInstanceName="menu1"
                                                                                                                    ItemSpacing="5px" OnInit="menu_Init1" OnItemClick="menu_ItemClick">
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
                                                                                                                                <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
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
                                                                                                                        <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
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
                                                                                            <dxwgv:GridViewDataMemoColumn Caption="Comentário" FieldName="DescricaoComentario"
                                                                                                Name="DescricaoComentario" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                                <PropertiesMemoEdit ClientInstanceName="txtComentario" Rows="6">
                                                                                                    <ClientSideEvents Init="function(s, e) {
	
	document.getElementById('spC').innerText = s.GetText().length;
	return setMaxLength(s.GetInputElement(), 2000);
}"></ClientSideEvents>
                                                                                                    <Style Font-Names="Verdana" Font-Size="8pt"></Style>
                                                                                                </PropertiesMemoEdit>
                                                                                                <EditFormSettings Caption="Comentários: (&lt;span id='spC'&gt;0&lt;/span&gt; de 2000) "
                                                                                                    CaptionLocation="Top" />
                                                                                                <EditFormSettings CaptionLocation="Top" Caption="Coment&#225;rios: (&lt;span id=&#39;spC&#39;&gt;0&lt;/span&gt; de 2000) "></EditFormSettings>
                                                                                            </dxwgv:GridViewDataMemoColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="NomeUsuario" Name="NomeUsuario"
                                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3" Width="200px">
                                                                                                <EditFormSettings Visible="False" />
                                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataDateColumn Caption="Data" FieldName="DataComentario" Name="DataComentario"
                                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Width="140px">
                                                                                                <PropertiesDateEdit DisplayFormatString="">
                                                                                                </PropertiesDateEdit>
                                                                                                <Settings ShowFilterRowMenu="True" />
                                                                                                <EditFormSettings Visible="False" />
                                                                                                <Settings ShowFilterRowMenu="True"></Settings>
                                                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <CellStyle HorizontalAlign="Center">
                                                                                                </CellStyle>
                                                                                            </dxwgv:GridViewDataDateColumn>
                                                                                        </Columns>
                                                                                    </dxwgv:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="formulario-botao">
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
                                                                    <table id="htmltablePlanoAcao" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="formulario-botao">
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
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td></td>
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
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <iframe id="frmAnexos" frameborder="0" height="450px" name="I1" scrolling="no" src=""
                                                                                            width="100%"></iframe>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <table class="formulario-botoes" border="0" cellpadding="0" cellspacing="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="formulario-botao">
                                                                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar5" Text="Fechar" Width="90px"
                                                                                                        ID="btnFechar5">
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
                                                    </TabPages>
                                                    <ClientSideEvents ActiveTabChanging="function(s, e) {
    e.cancel = podeMudarAba(s, e)
    }"></ClientSideEvents>
                                                </dxtc:ASPxPageControl>
                                                &nbsp;
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoRiscoQuestao"
                                        AutoGenerateColumns="False" Width="100%" Font-Names="Verdana" Font-Size="8pt"
                                        ID="gvDados" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
                            OnGridFocusedRowChanged(s, true);
                            }"
                                            CustomButtonClick="function(s, e) {
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
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td align="center">
                                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                                    ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
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
                                                                                <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
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
                                                                        <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
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
                                                Caption="Risco" VisibleIndex="2">
                                                <Settings ShowFilterRowMenu="False" AllowHeaderFilter="False"></Settings>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoStatusRiscoQuestao" Name="colStatus"
                                                Width="120px" Caption="Status" VisibleIndex="3">
                                                <FilterCellStyle Font-Names="Verdana" Font-Size="8pt">
                                                </FilterCellStyle>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavel" Name="Responsavel"
                                                Width="200px" Caption="Respons&#225;vel" Visible="False" VisibleIndex="4">
                                                <FilterCellStyle Font-Names="Verdana" Font-Size="8pt">
                                                </FilterCellStyle>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" Name="col_NomeProjeto" Caption="Nome Projeto"
                                                VisibleIndex="5">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="DataLimiteResolucao" Name="DataLimiteResolucao"
                                                Width="130px" Caption="Limite Elimina&#231;&#227;o" VisibleIndex="6">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                                                </PropertiesDateEdit>
                                                <Settings AllowHeaderFilter="False" ShowFilterRowMenu="True"></Settings>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="DataEliminacaoCancelamento" Name="DataEliminacaoCancelamento"
                                                Width="130px" Caption="Data Elimina&#231;&#227;o" VisibleIndex="7">
                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                                                </PropertiesDateEdit>
                                                <Settings AllowHeaderFilter="False" ShowFilterRowMenu="True"></Settings>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Publicado" Name="DataPublicacao" Caption="Publicado"
                                                VisibleIndex="8" Width="90px">
                                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                    <MaskSettings Mask="dd/MM/yyyy" />
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioResponsavel" Name="CodigoUsuarioResponsavel"
                                                Caption="CodigoUsuarioResponsavel" Visible="False" VisibleIndex="9">
                                                <Settings AllowHeaderFilter="False" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DetalheRiscoQuestao" Name="col_DetalheRisco"
                                                Caption="Detalhe" Visible="False" VisibleIndex="11">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ProbabilidadePrioridade" Name="col_ProbabilidadePrioridade"
                                                Caption="Probabilidade Prioridade" Visible="False" VisibleIndex="12">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ImpactoUrgencia" Name="col_ImpactoUrgencia"
                                                Caption="Impacto Urgencia" Visible="False" VisibleIndex="13">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataStatusRiscoQuestao" Name="col_DataStatusRiscoQuestao"
                                                Caption="Data Status Risco Questao" Visible="False" VisibleIndex="14">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DataPublicacao" Name="col_DataPublicacao"
                                                Caption="Data Publicacao" Visible="False" VisibleIndex="15">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoTipoRiscoQuestao" Name="col_DescricaoTipoRiscoQuestao"
                                                Caption="Descricao Tipo Risco Questao" Visible="False" VisibleIndex="16">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="Severidade" Name="col_Severidade" Caption="Severidade"
                                                Visible="False" VisibleIndex="17">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="IncluidoEmPor" Name="col_IncluidoEmPor"
                                                Caption="Incluido Em Por" Visible="False" VisibleIndex="18">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="PublicaoEmPor" Name="col_PublicaoEmPor"
                                                Caption="Publicao Em Por" Visible="False" VisibleIndex="19">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="ConsequenciaRiscoQuestao" Name="col_ConsequenciaRiscoQuestao"
                                                Caption="Consequencia Risco Questao" Visible="False" VisibleIndex="20">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="TratamentoRiscoQuestao" Name="col_tratamentoRiscoQuestao"
                                                Visible="False" VisibleIndex="21" Caption="tratamento Risco Questao">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" Name="CodigoProjeto" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="10">
                                                <EditFormSettings Visible="False" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="DescricaoRespostaRisco" FieldName="DescricaoRespostaRisco" ShowInCustomizationForm="True" Visible="False" VisibleIndex="22">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="CustoRiscoQuestao" FieldName="CustoRiscoQuestao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="23">
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="DescricaoRiscoQuestaoSuperior" FieldName="DescricaoRiscoQuestaoSuperior" ShowInCustomizationForm="True" Visible="False" VisibleIndex="24">
                                            </dxtv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Settings ShowHeaderFilterBlankItems="false" ShowGroupPanel="True" VerticalScrollBarMode="Visible"></Settings>
                                        <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                        <Styles>
                                            <HeaderPanel CssClass="janelaFiltro">
                                            </HeaderPanel>
                                            <FilterCell Font-Names="Verdana" Font-Size="8pt">
                                            </FilterCell>
                                            <FilterBar Font-Names="Verdana" Font-Size="8pt">
                                            </FilterBar>
                                        </Styles>
                                    </dxwgv:ASPxGridView>
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfAcao" ID="hfAcao">
                                    </dxhf:ASPxHiddenField>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents EndCallback="function(s, e) {
                                gvDados.Refresh();
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
                            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Font-Names="Verdana"
                            Font-Size="8pt">
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
                                                        Font-Names="Verdana" Font-Size="8pt" ID="lblComentarioAcao">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxMemo ID="mmComentarioAcao" runat="server" ClientInstanceName="mmComentarioAcao"
                                                        EnableClientSideAPI="True" Font-Names="Verdana" Font-Size="8pt" Height="100px"
                                                        Width="100%">
                                                        <ClientSideEvents KeyUp="function(s, e) {
                validaTamanhoTexto(s, e, 2000);
                }" />
                                                    </dxe:ASPxMemo>
                                                    <dxe:ASPxLabel ID="lblContadorMemoComentarioAcao" runat="server" ClientInstanceName="lblContadorMemoComentarioAcao"
                                                        Font-Bold="True" Font-Names="Verdana" Font-Size="7pt" ForeColor="#999999">
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
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <dxe:ASPxButton ID="btnAcao_pcComentarioAcao" runat="server" AutoPostBack="False"
                                                                                        ClientInstanceName="btnAcao_pcComentarioAcao" EnableClientSideAPI="True" Font-Names="Verdana"
                                                                                        Font-Size="8pt" Text="xxx" Width="90px">
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
                                                                                <td style="width: 10px"></td>
                                                                                <td align="right">
                                                                                    <dxe:ASPxButton ID="btnCancelar_pcComentarioAcao" runat="server" AutoPostBack="False"
                                                                                        ClientInstanceName="btnCancelar_pcComentarioAcao" EnableClientSideAPI="True"
                                                                                        Font-Names="Verdana" Font-Size="8pt" Text="Fechar" Width="90px">
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
                        <asp:SqlDataSource ID="sdsTipoRespostaRisco" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [CodigoTipoRespostaRisco]
      ,[DescricaoRespostaRisco]
  FROM [TipoRespostaRisco]

"></asp:SqlDataSource>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
