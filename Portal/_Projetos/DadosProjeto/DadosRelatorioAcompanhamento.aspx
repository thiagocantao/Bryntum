<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DadosRelatorioAcompanhamento.aspx.cs"
    Inherits="_Projetos_DadosProjeto_DadosRelatorioAcompanhamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .tabela
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript" language="javascript">
        var popUp = 'S';
        var returnValue;

        function OnLoad() {
            var situacao = hfGeral.Get("Situacao");
            if (situacao)
                returnValue = situacao;
        }

        function OnCallbackComplete(s, e) {
             if (e.result === '') {
                 returnValue = "ok";
                 window.top.mostraMensagem('Dados atualizados com sucesso!', 'sucesso', false, false, null);
                window.top.fechaModal();
            }
            else {
                window.top.mostraMensagem(e.result, 'erro', true, false, null);
            }
        }

        function Valida() {
            return true;
        }

        //para funcionar o label mostrador de quantos caracteres faltam para terminar o texto
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
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

    </script>
</head>
<body onload="OnLoad()">
    <form id="form1" runat="server">
    <dxtc:ASPxPageControl ID="pageControl" runat="server" ClientInstanceName="pageControl"
        ActiveTabIndex="0" Width="100%" >
        <TabPages>
            <dxtc:TabPage Text="Principal">
                <ContentCollection>
                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxp:ASPxPanel ID="pnlConteudo" runat="server" Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <table class="tabela">
                                        <tr>
                                            <td>
                                                <div id="divPrincipal" runat="server" style="overflow: auto; width: 100%;">
                                                    <table class="tabela">
                                                        <tr>
                                                            <td style="height: 26px; width: 10px;" valign="middle">
                                                                &nbsp;
                                                            </td>
                                                            <td style="height: 26px; padding-left: 20px; background-image: url('../imagens/titulo/back_Titulo_Desktop.gif');"
                                                                valign="middle">
                                                                <dxe:ASPxLabel ID="lblTitulo" runat="server" 
                                                                    Text="Relatório de Acompanhamento do Projeto">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="height: 26px; width: 10px;" valign="middle">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <table class="tabela">
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                                                            Text="Projeto">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="txtProjeto" runat="server" ClientInstanceName="txtProjeto"
                                                                                            ReadOnly="True" Width="100%">
                                                                                            <ReadOnlyStyle BackColor="LightGray">
                                                                                            </ReadOnlyStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                                                            Text="Gerente do Projeto">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="txtGerente" runat="server" 
                                                                                            ReadOnly="True" Width="100%">
                                                                                            <ReadOnlyStyle BackColor="LightGray">
                                                                                            </ReadOnlyStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td width="150">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                                                            Text="Data da Elaboração">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                                                                            Text="Responsável pela Elaboração">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxTextBox ID="txtDataElaboracao" runat="server" DisplayFormatString="{0:dd/MM/yyyy}"
                                                                                             ReadOnly="True" Width="100%">
                                                                                            <ReadOnlyStyle BackColor="LightGray">
                                                                                            </ReadOnlyStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="txtResponsavelElaboracao" runat="server"
                                                                                            ReadOnly="True" Width="100%">
                                                                                            <ReadOnlyStyle BackColor="LightGray">
                                                                                            </ReadOnlyStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                                                                            Text="Unidade Funcional">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="150">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                                                                            Text="Início Planejado">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="150">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                                                                            Text="Início Real">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxTextBox ID="txtUnidade" runat="server" 
                                                                                            ReadOnly="True" Width="100%">
                                                                                            <ReadOnlyStyle BackColor="LightGray">
                                                                                            </ReadOnlyStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxDateEdit ID="txtInicioPlanejado" runat="server" DisplayFormatString="{0:dd/MM/yyyy}"
                                                                                             ReadOnly="True" Width="100%">
                                                                                            <ReadOnlyStyle BackColor="LightGray">
                                                                                            </ReadOnlyStyle>
                                                                                        </dxe:ASPxDateEdit>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxDateEdit ID="txtInicioReal" runat="server" DisplayFormatString="{0:dd/MM/yyyy}"
                                                                                             ReadOnly="True" Width="100%">
                                                                                            <ReadOnlyStyle BackColor="LightGray">
                                                                                            </ReadOnlyStyle>
                                                                                        </dxe:ASPxDateEdit>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                                                                            Text="Término">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="margin-left: 120px" width="20%" valign="bottom">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" 
                                                                                            Text="Planejado">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%" valign="bottom">
                                                                                        <table class="tabela">
                                                                                            <tr>
                                                                                                <td align="center" colspan="2">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server" 
                                                                                                        Text="Progresso">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center" width="50%">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel59" runat="server" Font-Bold="False"
                                                                                                        Text="Anterior">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td align="center" width="50%">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel60" runat="server" 
                                                                                                        Text="Atual">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td width="20%" valign="bottom">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" 
                                                                                            Text="Tendência">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%" valign="bottom">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" 
                                                                                            Text="Status">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%" valign="bottom">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel13" runat="server" 
                                                                                            Text="Variação">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxTextBox ID="txtTerminoPlanejado" runat="server" 
                                                                                            MaxLength="250" Width="100%">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <table class="tabela">
                                                                                            <tr>
                                                                                                <td style="padding-right: 5px; padding-left: 5px;" width="50%">
                                                                                                    <dxe:ASPxTextBox ID="txtProgressoFisico" runat="server" 
                                                                                                        MaxLength="250" ReadOnly="True" Width="100%" DisplayFormatString="{0}%" HorizontalAlign="Center">
                                                                                                        <ReadOnlyStyle BackColor="LightGray">
                                                                                                        </ReadOnlyStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td style="padding-right: 5px; padding-left: 5px;" width="50%">
                                                                                                    <dxe:ASPxTextBox ID="txtTerminoProgresso" runat="server" 
                                                                                                        MaxLength="250" ReadOnly="True" Width="100%" DisplayFormatString="{0}%" HorizontalAlign="Center">
                                                                                                        <ReadOnlyStyle BackColor="LightGray">
                                                                                                        </ReadOnlyStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxTextBox ID="txtTerminoTendencia" runat="server" DisplayFormatString="{0:dd/MM/yyyy}"
                                                                                             ReadOnly="True" Width="100%">
                                                                                            <ReadOnlyStyle BackColor="LightGray">
                                                                                            </ReadOnlyStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxTextBox ID="txtTerminoStatus" runat="server" 
                                                                                            MaxLength="250" ReadOnly="True" Width="100%">
                                                                                            <ReadOnlyStyle BackColor="LightGray">
                                                                                            </ReadOnlyStyle>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="txtTerminoVariacao" runat="server" 
                                                                                            MaxLength="250" Width="100%">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel14" runat="server" 
                                                                                            Text="Ponto de Função">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="margin-left: 120px" width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel15" runat="server" 
                                                                                            Text="Planejado">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel16" runat="server" 
                                                                                            Text="Progresso">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel17" runat="server" 
                                                                                            Text="Tendência">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel18" runat="server" 
                                                                                            Text="Status">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel19" runat="server" 
                                                                                            Text="Variação">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxTextBox ID="txtPFPlanejado" runat="server" 
                                                                                            MaxLength="250" Width="100%">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px; margin-left: 40px;">
                                                                                        <dxe:ASPxTextBox ID="txtPFProgresso" runat="server" 
                                                                                            MaxLength="250" Width="100%">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxTextBox ID="txtPFTendencia" runat="server" 
                                                                                            MaxLength="250" Width="100%">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxTextBox ID="txtPFStatus" runat="server" 
                                                                                            MaxLength="250" Width="100%">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxTextBox ID="txtPFVariacao" runat="server" 
                                                                                            MaxLength="255" Width="100%">
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel20" runat="server" 
                                                                                            Text="Investimento (R$)">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="margin-left: 120px" width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel21" runat="server" 
                                                                                            Text="Planejado">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterInvestimentoPlanejado" runat="server" ClientInstanceName="lblCantCaraterInvestimentoPlanejado"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe250" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 250">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel22" runat="server" 
                                                                                            Text="Progresso">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterInvestimentoProgresso" runat="server" ClientInstanceName="lblCantCaraterInvestimentoProgresso"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe251" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 250">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel23" runat="server" 
                                                                                            Text="Tendência">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterInvestimentoTendencia" runat="server" ClientInstanceName="lblCantCaraterInvestimentoTendencia"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe252" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 250">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel24" runat="server" 
                                                                                            Text="Status">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterInvestimentoStatus" runat="server" ClientInstanceName="lblCantCaraterInvestimentoStatus"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe253" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 250">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel25" runat="server" 
                                                                                            Text="Variação">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterInvestimentoVariacao" runat="server" ClientInstanceName="lblCantCaraterInvestimentoVariacao"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe254" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 255">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxMemo ID="txtInvestimentoPlanejado" runat="server" 
                                                                                            MaxLength="250" Width="100%" Rows="5">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterInvestimentoPlanejado.SetText(s.GetText().length);
return setMaxLength(s.GetInputElement(), 250);
}" KeyDown="function(s, e) {
    lblCantCaraterInvestimentoPlanejado.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxMemo ID="txtInvestimentoProgresso" runat="server" 
                                                                                            MaxLength="250" Width="100%" Rows="5">
                                                                                            <ClientSideEvents Init="function(s, e) {
    lblCantCaraterInvestimentoProgresso.SetText(s.GetText().length);
    return setMaxLength(s.GetInputElement(), 250);
}" KeyDown="function(s, e) {
	lblCantCaraterInvestimentoProgresso.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxMemo ID="txtInvestimentoTendencia" runat="server" 
                                                                                            MaxLength="250" Width="100%" Rows="5">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterInvestimentoTendencia.SetText(s.GetText().length);
return setMaxLength(s.GetInputElement(), 250);
}" KeyDown="function(s, e) {
		lblCantCaraterInvestimentoTendencia.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxMemo ID="txtInvestimentoStatus" runat="server" 
                                                                                            MaxLength="250" Width="100%" Rows="5">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterInvestimentoStatus.SetText(s.GetText().length);
return setMaxLength(s.GetInputElement(), 250);
}" KeyDown="function(s, e) {
		lblCantCaraterInvestimentoStatus.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="txtInvestimentoVariacao" runat="server" 
                                                                                            MaxLength="250" Width="100%" Rows="5">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterInvestimentoVariacao.SetText(s.GetText().length);
return setMaxLength(s.GetInputElement(), 255);
}" KeyDown="function(s, e) {
		lblCantCaraterInvestimentoVariacao.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel26" runat="server" 
                                                                                            Text="Custeio (R$)">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="margin-left: 120px" width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel27" runat="server" 
                                                                                            Text="Planejado">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterCusteioPlanejado" runat="server" ClientInstanceName="lblCantCaraterCusteioPlanejado"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe255" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 250">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel28" runat="server" 
                                                                                            Text="Progresso">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterCusteioProgresso" runat="server" ClientInstanceName="lblCantCaraterCusteioProgresso"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe256" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 250">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel29" runat="server" 
                                                                                            Text="Tendência">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterCusteioTendencia" runat="server" ClientInstanceName="lblCantCaraterCusteioTendencia"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe257" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 250">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel30" runat="server" 
                                                                                            Text="Status">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterCusteioStatus" runat="server" ClientInstanceName="lblCantCaraterCusteioStatus"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe258" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 250">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td width="20%">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel31" runat="server" 
                                                                                            Text="Variação">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterCusteioVariacao" runat="server" ClientInstanceName="lblCantCaraterCusteioVariacao"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe259" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 255">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxMemo ID="txtCusteioPlanejado" runat="server" 
                                                                                            MaxLength="250" Width="100%" Rows="5">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterCusteioPlanejado.SetText(s.GetText().length);
return setMaxLength(s.GetInputElement(), 250);
}" KeyDown="function(s, e) {
		lblCantCaraterCusteioPlanejado.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxMemo ID="txtCusteioProgresso" runat="server" 
                                                                                            MaxLength="250" Width="100%" Rows="5">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterCusteioProgresso.SetText(s.GetText().length);
return setMaxLength(s.GetInputElement(), 250);
}" KeyDown="function(s, e) {
		lblCantCaraterCusteioProgresso.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxMemo ID="txtCusteioTendencia" runat="server" 
                                                                                            MaxLength="250" Width="100%" Rows="5">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterCusteioTendencia.SetText(s.GetText().length);
return setMaxLength(s.GetInputElement(), 250);
}" KeyDown="function(s, e) {
		lblCantCaraterCusteioTendencia.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                    <td style="padding-right: 10px">
                                                                                        <dxe:ASPxMemo ID="txtCusteioStatus" runat="server" 
                                                                                            MaxLength="250" Width="100%" Rows="5">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterCusteioStatus.SetText(s.GetText().length);
return setMaxLength(s.GetInputElement(), 250);
}" KeyDown="function(s, e) {
		lblCantCaraterCusteioStatus.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="txtCusteioVariacao" runat="server" 
                                                                                            MaxLength="250" Width="100%" Rows="5">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterCusteioVariacao.SetText(s.GetText().length);
return setMaxLength(s.GetInputElement(), 255);
}" KeyDown="function(s, e) {
		lblCantCaraterCusteioVariacao.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel32" runat="server" 
                                                                                            Text="ANÁLISE DE INDICADORES">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxwgv:ASPxGridView ID="gvAnaliseIndicadores" runat="server" AutoGenerateColumns="False"
                                                                                            ClientInstanceName="gvAnaliseIndicadores" DataSourceID="sdsIndicadores"
                                                                                            KeyFieldName="CodigoIndicadorRelatorio" Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize">
                                                                                            <Columns>
                                                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                    Width="60px" ShowEditButton="true" ShowDeleteButton="true" ShowNewButton="false"
                                                                                                    ShowSelectButton="false" ShowCancelButton="false" ShowUpdateButton="false">
                                                                                                    <HeaderTemplate>
                                                                                                        <%# ObtemBotaoInclusaoRegistro("gvAnaliseIndicadores", "Indicadores")%>
                                                                                                        <%--<img src="../../imagens/botoes/incluirReg02.png" alt="Novo" onclick="gvAnaliseIndicadores.AddNewRow ();"
                                                                                                            style="cursor: pointer;" />--%>
                                                                                                    </HeaderTemplate>
                                                                                                </dxwgv:GridViewCommandColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Indicador" FieldName="NomeIndicador" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="3">
                                                                                                    <PropertiesTextEdit MaxLength="250">
                                                                                                        <ValidationSettings Display="Dynamic">
                                                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                        </ValidationSettings>
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="% planejado" FieldName="Previsto" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="4" Width="90px">
                                                                                                    <PropertiesTextEdit MaxLength="50">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="1" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="% realizado" FieldName="Realizado" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="5" Width="90px">
                                                                                                    <PropertiesTextEdit MaxLength="50">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="1" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Justificativa (caso o valor planejado não tenha sido alcançado)"
                                                                                                    FieldName="Comentarios" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                                    <PropertiesTextEdit MaxLength="250">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                            </Columns>
                                                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                            </SettingsPager>
                                                                                            <SettingsEditing Mode="PopupEditForm" />
                                                                                            <SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>

                                                                                                <UpdateButton>
                                                                                                    <Image ToolTip="Salvar" Url="~/imagens/botoes/salvar.PNG">
                                                                                                    </Image>
                                                                                                </UpdateButton>
                                                                                                <CancelButton>
                                                                                                    <Image ToolTip="Cancelar" Url="~/imagens/botoes/cancelar.PNG">
                                                                                                    </Image>
                                                                                                </CancelButton>
                                                                                                <EditButton>
                                                                                                    <Image ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                                                                    </Image>
                                                                                                </EditButton>
                                                                                                <DeleteButton>
                                                                                                    <Image ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                                                                    </Image>
                                                                                                </DeleteButton>
                                                                                            </SettingsCommandButton>
                                                                                            <SettingsPopup>
                                                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                    AllowResize="True" />
                                                                                            </SettingsPopup>
                                                                                        </dxwgv:ASPxGridView>
                                                                                        <asp:SqlDataSource ID="sdsIndicadores" runat="server" DeleteCommand="DELETE FROM [pbh_RelatorioAcompanhamentoIndicadores] WHERE [CodigoIndicadorRelatorio] = @CodigoIndicadorRelatorio"
                                                                                            InsertCommand="INSERT INTO [pbh_RelatorioAcompanhamentoIndicadores] ([CodigoRelatorio], [NomeIndicador], [Previsto], [Realizado], [Comentarios], [CodigoTarefa]) VALUES (@CodigoRelatorio, @NomeIndicador, @Previsto, @Realizado, @Comentarios, @CodigoTarefa)"
                                                                                            SelectCommand="SELECT * FROM [pbh_RelatorioAcompanhamentoIndicadores] WHERE ([CodigoRelatorio] = @CodigoRelatorio) ORDER BY [NomeIndicador]"
                                                                                            UpdateCommand="UPDATE [pbh_RelatorioAcompanhamentoIndicadores] 
SET [NomeIndicador] = @NomeIndicador, 
       [Previsto] = @Previsto, 
       [Realizado] = @Realizado, 
       [Comentarios] = @Comentarios
WHERE [CodigoIndicadorRelatorio] = @CodigoIndicadorRelatorio">
                                                                                            <DeleteParameters>
                                                                                                <asp:Parameter Name="CodigoIndicadorRelatorio" Type="Int32" />
                                                                                            </DeleteParameters>
                                                                                            <InsertParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                                <asp:Parameter Name="NomeIndicador" Type="String" />
                                                                                                <asp:Parameter Name="Previsto" Type="String" />
                                                                                                <asp:Parameter Name="Realizado" Type="String" />
                                                                                                <asp:Parameter Name="Comentarios" Type="String" />
                                                                                                <asp:Parameter Name="CodigoTarefa" Type="Int32" />
                                                                                            </InsertParameters>
                                                                                            <SelectParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                            </SelectParameters>
                                                                                            <UpdateParameters>
                                                                                                <asp:Parameter Name="NomeIndicador" Type="String" />
                                                                                                <asp:Parameter Name="Previsto" Type="String" />
                                                                                                <asp:Parameter Name="Realizado" Type="String" />
                                                                                                <asp:Parameter Name="Comentarios" Type="String" />
                                                                                                <asp:Parameter Name="CodigoIndicadorRelatorio" Type="Int32" />
                                                                                            </UpdateParameters>
                                                                                        </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel33" runat="server" 
                                                                                            Text="MONITORAMENTO">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel34" runat="server" 
                                                                                            Text="Prazo">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterComentarioPrazo" runat="server" ClientInstanceName="lblCantCaraterComentarioPrazo"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe260" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 2000">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="txtComentarioPrazo" runat="server" 
                                                                                            Rows="5" Width="100%">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterComentarioPrazo.SetText(s.GetText().length);
	return setMaxLength(s.GetInputElement(), 2000);
}" KeyDown="function(s, e) {
		lblCantCaraterComentarioPrazo.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel35" runat="server" 
                                                                                            Text="Motivo do Atraso">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterComentarioMotivoAtraso" runat="server" ClientInstanceName="lblCantCaraterComentarioMotivoAtraso"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe261" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 2000">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="txtComentarioMotivoAtraso" runat="server"
                                                                                            Rows="5" Width="100%">
                                                                                            <ClientSideEvents Init="function(s, e) {
     lblCantCaraterComentarioMotivoAtraso.SetText(s.GetText().length);
	return setMaxLength(s.GetInputElement(), 2000);
}" KeyDown="function(s, e) {
		lblCantCaraterComentarioMotivoAtraso.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel37" runat="server" 
                                                                                            Text="Escopo">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterComentarioEscopo" runat="server" ClientInstanceName="lblCantCaraterComentarioEscopo"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe262" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 2000">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="txtComentarioEscopo" runat="server" 
                                                                                            Rows="5" Width="100%">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterComentarioEscopo.SetText(s.GetText().length);
	return setMaxLength(s.GetInputElement(), 2000);
}" KeyDown="function(s, e) {
	lblCantCaraterComentarioEscopo.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel36" runat="server" 
                                                                                            Text="Custos">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterComentarioCustos" runat="server" ClientInstanceName="lblCantCaraterComentarioCustos"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe263" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 2000">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="txtComentarioCustos" runat="server" 
                                                                                            Rows="5" Width="100%">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterComentarioCustos.SetText(s.GetText().length);
	return setMaxLength(s.GetInputElement(), 2000);
}" KeyDown="function(s, e) {
		lblCantCaraterComentarioCustos.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel38" runat="server" 
                                                                                            Text="Recursos Humanos">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterComentarioRH" runat="server" ClientInstanceName="lblCantCaraterComentarioRH"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe264" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 2000">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="txtComentarioRH" runat="server" 
                                                                                            Rows="5" Width="100%">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterComentarioRH.SetText(s.GetText().length);	
return setMaxLength(s.GetInputElement(), 2000);
}" KeyDown="function(s, e) {
		lblCantCaraterComentarioRH.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel39" runat="server" 
                                                                                            Text="Comunicações">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterComentarioComunicacoes" runat="server" ClientInstanceName="lblCantCaraterComentarioComunicacoes"
                                                                                             ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe265" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 2000">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="txtComentarioComunicacoes" runat="server"
                                                                                            Rows="5" Width="100%">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterComentarioComunicacoes.SetText(s.GetText().length);	
return setMaxLength(s.GetInputElement(), 2000);
}" KeyDown="function(s, e) {
		lblCantCaraterComentarioComunicacoes.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel40" runat="server" 
                                                                                            Text="Aquisições Realizadas">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxwgv:ASPxGridView ID="gvAquisicoes" runat="server" AutoGenerateColumns="False"
                                                                                            ClientInstanceName="gvAquisicoes" DataSourceID="sdsAquisicoes"
                                                                                            KeyFieldName="CodigoAquisicaoRelatorio" Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize">
                                                                                            <Columns>
                                                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                    Width="60px" ShowEditButton="true" ShowNewButton="false" ShowDeleteButton="true"
                                                                                                    ShowCancelButton="false" ShowUpdateButton="false">
                                                                                                    <HeaderTemplate>
                                                                                                        <%# ObtemBotaoInclusaoRegistro("gvAquisicoes", "Arquisições")%>
                                                                                                        <%--<img src="../../imagens/botoes/incluirReg02.png" alt="Novo" onclick="gvAquisicoes.AddNewRow ();"
                                                                                                            style="cursor: pointer;" />--%>
                                                                                                    </HeaderTemplate>
                                                                                                </dxwgv:GridViewCommandColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Aquisição" FieldName="Aquisicao" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="3">
                                                                                                    <PropertiesTextEdit MaxLength="250">
                                                                                                        <ValidationSettings Display="Dynamic">
                                                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                        </ValidationSettings>
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataSpinEditColumn Caption="Valor (R$)" FieldName="ValorAquisicao"
                                                                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="150px">
                                                                                                    <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                    </PropertiesSpinEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataSpinEditColumn>
                                                                                            </Columns>
                                                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                            </SettingsPager>
                                                                                            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
                                                                                            <SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>

                                                                                                <UpdateButton>
                                                                                                    <Image ToolTip="Salvar" Url="~/imagens/botoes/salvar.PNG">
                                                                                                    </Image>
                                                                                                </UpdateButton>
                                                                                                <CancelButton>
                                                                                                    <Image ToolTip="Cancelar" Url="~/imagens/botoes/cancelar.PNG">
                                                                                                    </Image>
                                                                                                </CancelButton>
                                                                                                <EditButton>
                                                                                                    <Image ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                                                                    </Image>
                                                                                                </EditButton>
                                                                                                <DeleteButton>
                                                                                                    <Image ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                                                                    </Image>
                                                                                                </DeleteButton>
                                                                                            </SettingsCommandButton>
                                                                                            <SettingsPopup>
                                                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                    AllowResize="True" />
                                                                                            </SettingsPopup>
                                                                                        </dxwgv:ASPxGridView>
                                                                                        <asp:SqlDataSource ID="sdsAquisicoes" runat="server" DeleteCommand="DELETE FROM [pbh_RelatorioAcompanhamentoAquisicoes] WHERE [CodigoAquisicaoRelatorio] = @CodigoAquisicaoRelatorio"
                                                                                            InsertCommand="INSERT INTO [pbh_RelatorioAcompanhamentoAquisicoes] ([CodigoRelatorio], [Aquisicao], [ValorAquisicao],[IndicaAquisicaoPendente]) VALUES (@CodigoRelatorio, @Aquisicao, @ValorAquisicao, 'N')"
                                                                                            SelectCommand="SELECT * FROM [pbh_RelatorioAcompanhamentoAquisicoes] WHERE ([CodigoRelatorio] = @CodigoRelatorio AND [IndicaAquisicaoPendente] = 'N') ORDER BY [Aquisicao]"
                                                                                            UpdateCommand="UPDATE [pbh_RelatorioAcompanhamentoAquisicoes] 
SET [Aquisicao] = @Aquisicao, 
       [ValorAquisicao] = @ValorAquisicao,
       [IndicaAquisicaoPendente] = 'N'
WHERE [CodigoAquisicaoRelatorio] = @CodigoAquisicaoRelatorio">
                                                                                            <DeleteParameters>
                                                                                                <asp:Parameter Name="CodigoAquisicaoRelatorio" Type="Int32" />
                                                                                            </DeleteParameters>
                                                                                            <InsertParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                                <asp:Parameter Name="Aquisicao" Type="String" />
                                                                                                <asp:Parameter Name="ValorAquisicao" Type="Decimal" />
                                                                                            </InsertParameters>
                                                                                            <SelectParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                            </SelectParameters>
                                                                                            <UpdateParameters>
                                                                                                <asp:Parameter Name="Aquisicao" Type="String" />
                                                                                                <asp:Parameter Name="ValorAquisicao" Type="Decimal" />
                                                                                                <asp:Parameter Name="CodigoAquisicaoRelatorio" Type="Int32" />
                                                                                            </UpdateParameters>
                                                                                        </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel41" runat="server" 
                                                                                            Text="Aquisições a Realizar">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxwgv:ASPxGridView ID="gvAquisicoesPendentes" runat="server" AutoGenerateColumns="False"
                                                                                            ClientInstanceName="gvAquisicoesPendentes" DataSourceID="sdsAquisicoesPendentes"
                                                                                             KeyFieldName="CodigoAquisicaoRelatorio"
                                                                                            OnCommandButtonInitialize="grid_CommandButtonInitialize" Width="100%">
                                                                                            <Columns>
                                                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                    Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                                                                    <HeaderTemplate>
                                                                                                        <%# ObtemBotaoInclusaoRegistro("gvAquisicoesPendentes", "Arquisições a realizar")%>
                                                                                                        <%--<img src="../../imagens/botoes/incluirReg02.png" alt="Novo" onclick="gvAquisicoes.AddNewRow ();"
                                                                                                            style="cursor: pointer;" />--%>
                                                                                                    </HeaderTemplate>
                                                                                                </dxwgv:GridViewCommandColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Aquisição" FieldName="Aquisicao" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="3">
                                                                                                    <PropertiesTextEdit MaxLength="250">
                                                                                                        <ValidationSettings Display="Dynamic">
                                                                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                                                        </ValidationSettings>
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataSpinEditColumn Caption="Valor (R$)" FieldName="ValorAquisicao"
                                                                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="150px">
                                                                                                    <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                    </PropertiesSpinEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataSpinEditColumn>
                                                                                            </Columns>
                                                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                            </SettingsPager>
                                                                                            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
                                                                                            <SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>

                                                                                                <UpdateButton>
                                                                                                    <Image ToolTip="Salvar" Url="~/imagens/botoes/salvar.PNG">
                                                                                                    </Image>
                                                                                                </UpdateButton>
                                                                                                <CancelButton>
                                                                                                    <Image ToolTip="Cancelar" Url="~/imagens/botoes/cancelar.PNG">
                                                                                                    </Image>
                                                                                                </CancelButton>
                                                                                                <EditButton>
                                                                                                    <Image ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                                                                    </Image>
                                                                                                </EditButton>
                                                                                                <DeleteButton>
                                                                                                    <Image ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                                                                    </Image>
                                                                                                </DeleteButton>
                                                                                            </SettingsCommandButton>
                                                                                            <SettingsPopup>
                                                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                    AllowResize="True" />
                                                                                            </SettingsPopup>
                                                                                        </dxwgv:ASPxGridView>
                                                                                        <asp:SqlDataSource ID="sdsAquisicoesPendentes" runat="server" DeleteCommand="DELETE FROM [pbh_RelatorioAcompanhamentoAquisicoes] WHERE [CodigoAquisicaoRelatorio] = @CodigoAquisicaoRelatorio"
                                                                                            InsertCommand="INSERT INTO [pbh_RelatorioAcompanhamentoAquisicoes] ([CodigoRelatorio], [Aquisicao], [ValorAquisicao],[IndicaAquisicaoPendente]) VALUES (@CodigoRelatorio, @Aquisicao, @ValorAquisicao, 'S')"
                                                                                            SelectCommand="SELECT * FROM [pbh_RelatorioAcompanhamentoAquisicoes] WHERE ([CodigoRelatorio] = @CodigoRelatorio AND [IndicaAquisicaoPendente] = 'S') ORDER BY [Aquisicao]"
                                                                                            UpdateCommand="UPDATE [pbh_RelatorioAcompanhamentoAquisicoes] 
SET [Aquisicao] = @Aquisicao, 
       [ValorAquisicao] = @ValorAquisicao,
       [IndicaAquisicaoPendente] = 'S'
WHERE [CodigoAquisicaoRelatorio] = @CodigoAquisicaoRelatorio">
                                                                                            <DeleteParameters>
                                                                                                <asp:Parameter Name="CodigoAquisicaoRelatorio" Type="Int32" />
                                                                                            </DeleteParameters>
                                                                                            <InsertParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                                <asp:Parameter Name="Aquisicao" Type="String" />
                                                                                                <asp:Parameter Name="ValorAquisicao" Type="Decimal" />
                                                                                            </InsertParameters>
                                                                                            <SelectParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                            </SelectParameters>
                                                                                            <UpdateParameters>
                                                                                                <asp:Parameter Name="Aquisicao" Type="String" />
                                                                                                <asp:Parameter Name="ValorAquisicao" Type="Decimal" />
                                                                                                <asp:Parameter Name="CodigoAquisicaoRelatorio" Type="Int32" />
                                                                                            </UpdateParameters>
                                                                                        </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel42" runat="server" 
                                                                                            Text="Garantia da Qualidade">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterComentarioGarantiaQualidade" runat="server"
                                                                                            ClientInstanceName="lblCantCaraterComentarioGarantiaQualidade"
                                                                                            ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe266" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 2000">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="txtComentarioGarantiaQualidade" runat="server"
                                                                                            Rows="5" Width="100%">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterComentarioGarantiaQualidade.SetText(s.GetText().length);	
return setMaxLength(s.GetInputElement(), 2000);
}" KeyDown="function(s, e) {
		lblCantCaraterComentarioGarantiaQualidade.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel43" runat="server" 
                                                                                            Text="Gerência de Configurações">
                                                                                        </dxe:ASPxLabel>
                                                                                        &nbsp;<dxe:ASPxLabel ID="lblCantCaraterComentarioGerenciaConfiguracoes" runat="server"
                                                                                            ClientInstanceName="lblCantCaraterComentarioGerenciaConfiguracoes"
                                                                                            ForeColor="Silver" Text="0">
                                                                                        </dxe:ASPxLabel>
                                                                                        <dxe:ASPxLabel ID="lblDe267" runat="server" ClientInstanceName="lblDe250"
                                                                                            ForeColor="Silver" Text=" de 2000">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo ID="txtComentarioGerenciaConfiguracoes" runat="server"
                                                                                            Rows="5" Width="100%">
                                                                                            <ClientSideEvents Init="function(s, e) {
lblCantCaraterComentarioGerenciaConfiguracoes.SetText(s.GetText().length);	
return setMaxLength(s.GetInputElement(), 2000);
}" KeyDown="function(s, e) {
		lblCantCaraterComentarioGerenciaConfiguracoes.SetText(s.GetText().length);
}" />
                                                                                        </dxe:ASPxMemo>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel44" runat="server" 
                                                                                            Text="Riscos Altos">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxwgv:ASPxGridView ID="gvRiscosAltos" runat="server" AutoGenerateColumns="False"
                                                                                            ClientInstanceName="gvRiscosAltos" DataSourceID="sdsRiscos"
                                                                                            KeyFieldName="CodigoRiscoRelatorio" Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize">
                                                                                            <Columns>
                                                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                    Width="60px" ShowEditButton="true" ShowNewButton="false" ShowDeleteButton="true"
                                                                                                    ShowSelectButton="false" ShowCancelButton="false" ShowUpdateButton="false">
                                                                                                    <HeaderTemplate>
                                                                                                        <%# ObtemBotaoInclusaoRegistro("gvRiscosAltos", "Riscos")%>
                                                                                                        <%--<img src="../../imagens/botoes/incluirReg02.png" alt="Novo" onclick="gvRiscosAltos.AddNewRow ();"
                                                                                                            style="cursor: pointer;" />--%>
                                                                                                    </HeaderTemplate>
                                                                                                </dxwgv:GridViewCommandColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Risco" FieldName="Risco" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="1">
                                                                                                    <PropertiesTextEdit MaxLength="250">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Consequências" FieldName="Impacto" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="2">
                                                                                                    <PropertiesTextEdit MaxLength="2000">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Tratamento" FieldName="Acao" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="3">
                                                                                                    <PropertiesTextEdit MaxLength="2000">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="Responsavel" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="4">
                                                                                                    <PropertiesTextEdit MaxLength="250">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                            </Columns>
                                                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                            </SettingsPager>
                                                                                            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
                                                                                            <SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>

                                                                                                <UpdateButton>
                                                                                                    <Image ToolTip="Salvar" Url="~/imagens/botoes/salvar.PNG">
                                                                                                    </Image>
                                                                                                </UpdateButton>
                                                                                                <CancelButton>
                                                                                                    <Image ToolTip="Cancelar" Url="~/imagens/botoes/cancelar.PNG">
                                                                                                    </Image>
                                                                                                </CancelButton>
                                                                                                <EditButton>
                                                                                                    <Image ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                                                                    </Image>
                                                                                                </EditButton>
                                                                                                <DeleteButton>
                                                                                                    <Image ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                                                                    </Image>
                                                                                                </DeleteButton>
                                                                                            </SettingsCommandButton>
                                                                                            <SettingsPopup>
                                                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                    AllowResize="True" />
                                                                                            </SettingsPopup>
                                                                                        </dxwgv:ASPxGridView>
                                                                                        <asp:SqlDataSource ID="sdsRiscos" runat="server" DeleteCommand="DELETE FROM [pbh_RelatorioAcompanhamentoRiscos] WHERE [CodigoRiscoRelatorio] = @CodigoRiscoRelatorio"
                                                                                            InsertCommand="INSERT INTO [pbh_RelatorioAcompanhamentoRiscos] ([CodigoRelatorio], [Risco], [Impacto], [Acao], [Responsavel], [CodigoRisco]) VALUES (@CodigoRelatorio, @Risco, @Impacto, @Acao, @Responsavel, @CodigoRisco)"
                                                                                            SelectCommand="SELECT * FROM [pbh_RelatorioAcompanhamentoRiscos] WHERE ([CodigoRelatorio] = @CodigoRelatorio) ORDER BY [Risco]"
                                                                                            UpdateCommand="UPDATE [pbh_RelatorioAcompanhamentoRiscos] 
SET [Risco] = @Risco, 
       [Impacto] = @Impacto, 
       [Acao] = @Acao, 
       [Responsavel] = @Responsavel 
WHERE [CodigoRiscoRelatorio] = @CodigoRiscoRelatorio">
                                                                                            <DeleteParameters>
                                                                                                <asp:Parameter Name="CodigoRiscoRelatorio" Type="Int32" />
                                                                                            </DeleteParameters>
                                                                                            <InsertParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                                <asp:Parameter Name="Risco" Type="String" />
                                                                                                <asp:Parameter Name="Impacto" Type="String" />
                                                                                                <asp:Parameter Name="Acao" Type="String" />
                                                                                                <asp:Parameter Name="Responsavel" Type="String" />
                                                                                                <asp:Parameter Name="CodigoRisco" Type="Int32" />
                                                                                            </InsertParameters>
                                                                                            <SelectParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                            </SelectParameters>
                                                                                            <UpdateParameters>
                                                                                                <asp:Parameter Name="Risco" Type="String" />
                                                                                                <asp:Parameter Name="Impacto" Type="String" />
                                                                                                <asp:Parameter Name="Acao" Type="String" />
                                                                                                <asp:Parameter Name="Responsavel" Type="String" />
                                                                                                <asp:Parameter Name="CodigoRiscoRelatorio" Type="Int32" />
                                                                                            </UpdateParameters>
                                                                                        </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel45" runat="server" 
                                                                                            Text="SUSTENTAÇÃO">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel46" runat="server" 
                                                                                            Text="Foi definida a área responsável pela sustentação?">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 120px">
                                                                                        <dxe:ASPxRadioButtonList ID="rblAreaDefinida" runat="server"
                                                                                            SelectedIndex="2" Width="120px">
                                                                                            <Items>
                                                                                                <dxe:ListEditItem Text="Sim" Value="S" />
                                                                                                <dxe:ListEditItem Text="Não" Value="N" />
                                                                                                <dxe:ListEditItem Selected="True" Text="Não se aplica" Value="X" />
                                                                                            </Items>
                                                                                            <Border BorderStyle="None" />
                                                                                        </dxe:ASPxRadioButtonList>
                                                                                    </td>
                                                                                    <td valign="top">
                                                                                        <table class="tabela">
                                                                                            <tr>
                                                                                                <td width="60">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel47" runat="server" 
                                                                                                        Text="Área:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxComboBox ID="cmbAreaSustentacao" runat="server" DataSourceID="sdsUnidade"
                                                                                                        IncrementalFilteringMode="Contains" TextField="SiglaUnidadeNegocio" ValueField="CodigoUnidadeNegocio"
                                                                                                        ValueType="System.Int32">
                                                                                                    </dxe:ASPxComboBox>
                                                                                                    <asp:SqlDataSource ID="sdsUnidade" runat="server" SelectCommand="SELECT [CodigoUnidadeNegocio], [NomeUnidadeNegocio], [SiglaUnidadeNegocio] FROM [UnidadeNegocio] WHERE (([CodigoEntidade] = @CodigoEntidade) AND ([DataExclusao] IS NULL)) UNION SELECT -1, '', '' ORDER BY 2,3">
                                                                                                        <SelectParameters>
                                                                                                            <asp:SessionParameter Name="CodigoEntidade" SessionField="CodigoEntidade" Type="Int32" />
                                                                                                        </SelectParameters>
                                                                                                    </asp:SqlDataSource>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel48" runat="server" 
                                                                                                        Text="Justificar:">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox ID="txtJustificativaSustentacao" runat="server"
                                                                                                        MaxLength="500" Width="100%">
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel49" runat="server" 
                                                                                            Text="Foi emitido o documento para repasse da solução/sistema para área de Sustentação?">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <dxe:ASPxRadioButtonList ID="rblDocumentoEmitido" runat="server"
                                                                                            SelectedIndex="2" Width="120px">
                                                                                            <Items>
                                                                                                <dxe:ListEditItem Text="Sim" Value="S" />
                                                                                                <dxe:ListEditItem Text="Não" Value="N" />
                                                                                                <dxe:ListEditItem Selected="True" Text="Não se aplica" Value="X" />
                                                                                            </Items>
                                                                                            <Border BorderStyle="None" />
                                                                                        </dxe:ASPxRadioButtonList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel50" runat="server" 
                                                                                            Text="DESVIOS/PROBLEMAS/PENDÊNCIAS  ">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxwgv:ASPxGridView ID="gvDesvios" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDesvios"
                                                                                            DataSourceID="sdsDesvios"  KeyFieldName="CodigoProblemaRelatorio"
                                                                                            Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize">
                                                                                            <Columns>
                                                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                    Width="60px" ShowEditButton="true" ShowNewButton="false" ShowDeleteButton="true"
                                                                                                    ShowSelectButton="true" ShowCancelButton="false" ShowUpdateButton="false">
                                                                                                    <HeaderTemplate>
                                                                                                        <%# ObtemBotaoInclusaoRegistro("gvDesvios", "Desvios/Problemas/Pendências")%>
                                                                                                        <%--<img src="../../imagens/botoes/incluirReg02.png" alt="Novo" onclick="gvDesvios.AddNewRow();"
                                                                                                            style="cursor: pointer;" />--%>
                                                                                                    </HeaderTemplate>
                                                                                                </dxwgv:GridViewCommandColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="Problema" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="3">
                                                                                                    <PropertiesTextEdit MaxLength="250">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Tratamento" FieldName="Acao" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="4">
                                                                                                    <PropertiesTextEdit MaxLength="2000">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="Responsavel" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="5">
                                                                                                    <PropertiesTextEdit MaxLength="250">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataDateColumn Caption="Prazo" FieldName="Prazo" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="6">
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataDateColumn>
                                                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Status" FieldName="Status" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="7">
                                                                                                    <PropertiesComboBox>
                                                                                                        <Items>
                                                                                                            <dxe:ListEditItem Text="Não iniciado" Value="Não iniciado" />
                                                                                                            <dxe:ListEditItem Text="Em execução" Value="Em execução" />
                                                                                                            <dxe:ListEditItem Text="Resolvido" Value="Resolvido" />
                                                                                                        </Items>
                                                                                                    </PropertiesComboBox>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                                            </Columns>
                                                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                            </SettingsPager>
                                                                                            <SettingsEditing Mode="PopupEditForm" />
                                                                                            <SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>

                                                                                                <UpdateButton>
                                                                                                    <Image ToolTip="Salvar" Url="~/imagens/botoes/salvar.PNG">
                                                                                                    </Image>
                                                                                                </UpdateButton>
                                                                                                <CancelButton>
                                                                                                    <Image ToolTip="Cancelar" Url="~/imagens/botoes/cancelar.PNG">
                                                                                                    </Image>
                                                                                                </CancelButton>
                                                                                                <EditButton>
                                                                                                    <Image ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                                                                    </Image>
                                                                                                </EditButton>
                                                                                                <DeleteButton>
                                                                                                    <Image ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                                                                    </Image>
                                                                                                </DeleteButton>
                                                                                            </SettingsCommandButton>
                                                                                            <SettingsPopup>
                                                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                    AllowResize="True" />
                                                                                            </SettingsPopup>
                                                                                        </dxwgv:ASPxGridView>
                                                                                        <asp:SqlDataSource ID="sdsDesvios" runat="server" DeleteCommand="DELETE FROM [pbh_RelatorioAcompanhamentoProblemas] WHERE [CodigoProblemaRelatorio] = @CodigoProblemaRelatorio"
                                                                                            InsertCommand="INSERT INTO [pbh_RelatorioAcompanhamentoProblemas] ([CodigoRelatorio], [Problema], [Acao], [Responsavel], [Prazo], [Status], [CodigoProblema]) VALUES (@CodigoRelatorio, @Problema, @Acao, @Responsavel, @Prazo, @Status, @CodigoProblema)"
                                                                                            SelectCommand="SELECT * FROM [pbh_RelatorioAcompanhamentoProblemas] WHERE ([CodigoRelatorio] = @CodigoRelatorio) ORDER BY [Problema]"
                                                                                            UpdateCommand="UPDATE [pbh_RelatorioAcompanhamentoProblemas] 
SET [Problema] = @Problema,
       [Acao] = @Acao, 
       [Responsavel] = @Responsavel, 
       [Prazo] = @Prazo, 
       [Status] = @Status
WHERE [CodigoProblemaRelatorio] = @CodigoProblemaRelatorio">
                                                                                            <DeleteParameters>
                                                                                                <asp:Parameter Name="CodigoProblemaRelatorio" Type="Int32" />
                                                                                            </DeleteParameters>
                                                                                            <InsertParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                                <asp:Parameter Name="Problema" Type="String" />
                                                                                                <asp:Parameter Name="Acao" Type="String" />
                                                                                                <asp:Parameter Name="Responsavel" Type="String" />
                                                                                                <asp:Parameter Name="Prazo" Type="DateTime" />
                                                                                                <asp:Parameter Name="Status" Type="String" />
                                                                                                <asp:Parameter Name="CodigoProblema" Type="Int32" />
                                                                                            </InsertParameters>
                                                                                            <SelectParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                            </SelectParameters>
                                                                                            <UpdateParameters>
                                                                                                <asp:Parameter Name="Problema" Type="String" />
                                                                                                <asp:Parameter Name="Acao" Type="String" />
                                                                                                <asp:Parameter Name="Responsavel" Type="String" />
                                                                                                <asp:Parameter Name="Prazo" Type="DateTime" />
                                                                                                <asp:Parameter Name="Status" Type="String" />
                                                                                                <asp:Parameter Name="CodigoProblemaRelatorio" Type="Int32" />
                                                                                            </UpdateParameters>
                                                                                        </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel51" runat="server" 
                                                                                            Text="CONTROLE DE MUDANÇAS">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table class="tabela">
                                                                                            <tr>
                                                                                                <td width="150px">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel52" runat="server" Font-Bold="True"
                                                                                                        Text="Escopo" Width="100px">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel53" runat="server" Font-Bold="True"
                                                                                                        Text="Cronograma" Width="100px">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel54" runat="server" Font-Bold="True"
                                                                                                        Text="Custos" Width="100px">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel55" runat="server" Font-Bold="True"
                                                                                                        Text="Paralisação" Width="100px">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel61" runat="server" Font-Bold="True"
                                                                                                        Text="Reativação" Width="100px">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel56" runat="server" Font-Bold="True"
                                                                                                        Text="Volume de Mudanças">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td style="padding-right: 10px">
                                                                                                    <dxe:ASPxSpinEdit ID="seEscopo" runat="server" 
                                                                                                        Height="21px" Number="0" NumberType="Integer" ReadOnly="True" Width="100%">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ReadOnlyStyle BackColor="LightGray">
                                                                                                        </ReadOnlyStyle>
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                                <td style="padding-right: 10px">
                                                                                                    <dxe:ASPxSpinEdit ID="seCronograma" runat="server" 
                                                                                                        Height="21px" Number="0" NumberType="Integer" ReadOnly="True" Width="100%">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ReadOnlyStyle BackColor="LightGray">
                                                                                                        </ReadOnlyStyle>
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                                <td style="padding-right: 10px">
                                                                                                    <dxe:ASPxSpinEdit ID="seCustos" runat="server" 
                                                                                                        Height="21px" Number="0" NumberType="Integer" ReadOnly="True" Width="100%">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ReadOnlyStyle BackColor="LightGray">
                                                                                                        </ReadOnlyStyle>
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                                <td style="padding-right: 10px">
                                                                                                    <dxe:ASPxSpinEdit ID="seParalizacao" runat="server" 
                                                                                                        Height="21px" Number="0" NumberType="Integer" ReadOnly="True" Width="100%">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ReadOnlyStyle BackColor="LightGray">
                                                                                                        </ReadOnlyStyle>
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                                <td style="padding-right: 10px">
                                                                                                    <dxe:ASPxSpinEdit ID="seQuantReativacao" runat="server" 
                                                                                                        Height="21px" Number="0" NumberType="Integer" ReadOnly="True" Width="100%">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ReadOnlyStyle BackColor="LightGray">
                                                                                                        </ReadOnlyStyle>
                                                                                                    </dxe:ASPxSpinEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel57" runat="server" 
                                                                                            Text="ENTREGAS  ">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxwgv:ASPxGridView ID="gvEntregas" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvEntregas"
                                                                                            DataSourceID="sdsEntregas"  KeyFieldName="CodigoEntregaRelatorio"
                                                                                            Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize">
                                                                                            <Columns>
                                                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                    Width="60px" ShowEditButton="true" ShowNewButton="false" ShowDeleteButton="true"
                                                                                                    ShowSelectButton="false" ShowCancelButton="false" ShowUpdateButton="false">
                                                                                                    <HeaderTemplate>
                                                                                                        <%# ObtemBotaoInclusaoRegistro("gvEntregas", "Entregas")%>
                                                                                                        <%--<img src="../../imagens/botoes/incluirReg02.png" alt="Novo" onclick="gvEntregas.AddNewRow();"
                                                                                                            style="cursor: pointer;" />--%>
                                                                                                    </HeaderTemplate>
                                                                                                </dxwgv:GridViewCommandColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Produto" FieldName="Produto" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="3">
                                                                                                    <PropertiesTextEdit MaxLength="250">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataComboBoxColumn Caption="Tipo de Entrega (Parcial/Total)" FieldName="TipoEntrega"
                                                                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                                                                    <PropertiesComboBox MaxLength="1">
                                                                                                        <Items>
                                                                                                            <dxe:ListEditItem Text="Parcial" Value="P" />
                                                                                                            <dxe:ListEditItem Text="Total" Value="T" />
                                                                                                        </Items>
                                                                                                    </PropertiesComboBox>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                    <HeaderStyle Wrap="True" />
                                                                                                </dxwgv:GridViewDataComboBoxColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="DescricaoEntrega" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="5">
                                                                                                    <PropertiesTextEdit MaxLength="2000">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                            </Columns>
                                                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                            </SettingsPager>
                                                                                            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
                                                                                            <SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>

                                                                                                <UpdateButton>
                                                                                                    <Image ToolTip="Salvar" Url="~/imagens/botoes/salvar.PNG">
                                                                                                    </Image>
                                                                                                </UpdateButton>
                                                                                                <CancelButton>
                                                                                                    <Image ToolTip="Cancelar" Url="~/imagens/botoes/cancelar.PNG">
                                                                                                    </Image>
                                                                                                </CancelButton>
                                                                                                <EditButton>
                                                                                                    <Image ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                                                                    </Image>
                                                                                                </EditButton>
                                                                                                <DeleteButton>
                                                                                                    <Image ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                                                                    </Image>
                                                                                                </DeleteButton>
                                                                                            </SettingsCommandButton>
                                                                                            <SettingsPopup>
                                                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                    AllowResize="True" />
                                                                                            </SettingsPopup>
                                                                                        </dxwgv:ASPxGridView>
                                                                                        <asp:SqlDataSource ID="sdsEntregas" runat="server" DeleteCommand="DELETE FROM [pbh_RelatorioAcompanhamentoEntregas] WHERE [CodigoEntregaRelatorio] = @CodigoEntregaRelatorio"
                                                                                            InsertCommand="INSERT INTO [pbh_RelatorioAcompanhamentoEntregas] ([CodigoRelatorio], [Produto], [TipoEntrega], [DescricaoEntrega]) VALUES (@CodigoRelatorio, @Produto, @TipoEntrega, @DescricaoEntrega)"
                                                                                            SelectCommand="SELECT * FROM [pbh_RelatorioAcompanhamentoEntregas] WHERE ([CodigoRelatorio] = @CodigoRelatorio) ORDER BY [DescricaoEntrega]"
                                                                                            UpdateCommand="UPDATE [pbh_RelatorioAcompanhamentoEntregas] 
SET [Produto] = @Produto, 
       [TipoEntrega] = @TipoEntrega, 
       [DescricaoEntrega] = @DescricaoEntrega
WHERE [CodigoEntregaRelatorio] = @CodigoEntregaRelatorio">
                                                                                            <DeleteParameters>
                                                                                                <asp:Parameter Name="CodigoEntregaRelatorio" Type="Int32" />
                                                                                            </DeleteParameters>
                                                                                            <InsertParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                                <asp:Parameter Name="Produto" Type="String" />
                                                                                                <asp:Parameter Name="TipoEntrega" Type="String" />
                                                                                                <asp:Parameter Name="DescricaoEntrega" Type="String" />
                                                                                            </InsertParameters>
                                                                                            <SelectParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                            </SelectParameters>
                                                                                            <UpdateParameters>
                                                                                                <asp:Parameter Name="Produto" Type="String" />
                                                                                                <asp:Parameter Name="TipoEntrega" Type="String" />
                                                                                                <asp:Parameter Name="DescricaoEntrega" Type="String" />
                                                                                                <asp:Parameter Name="CodigoEntregaRelatorio" Type="Int32" />
                                                                                            </UpdateParameters>
                                                                                        </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="tabela">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel58" runat="server" 
                                                                                            Text="DESTINATÁRIOS">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxwgv:ASPxGridView ID="gvDestinatarios" runat="server" AutoGenerateColumns="False"
                                                                                            ClientInstanceName="gvDestinatarios" DataSourceID="sdsDestinatarios"
                                                                                            KeyFieldName="CodigoDestinatarioRelatorio" Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize">
                                                                                            <Columns>
                                                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                    Width="60px" ShowEditButton="true" ShowNewButton="false" ShowDeleteButton="true"
                                                                                                    ShowSelectButton="false" ShowCancelButton="false" ShowUpdateButton="false">
                                                                                                    <HeaderTemplate>
                                                                                                        <%# ObtemBotaoInclusaoRegistro("gvDestinatarios", "Destinatários")%>
                                                                                                        <%--<img src="../../imagens/botoes/incluirReg02.png" alt="Novo" onclick="gvDestinatarios.AddNewRow();"
                                                                                                            style="cursor: pointer;" />--%>
                                                                                                    </HeaderTemplate>
                                                                                                </dxwgv:GridViewCommandColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Destinatário" FieldName="Destinatario" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="4">
                                                                                                    <PropertiesTextEdit MaxLength="250">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="Unidade" FieldName="Unidade" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="3">
                                                                                                    <PropertiesTextEdit MaxLength="50">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                                <dxwgv:GridViewDataTextColumn Caption="E-Mail" FieldName="EmailDestinatario" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="5">
                                                                                                    <PropertiesTextEdit MaxLength="250">
                                                                                                    </PropertiesTextEdit>
                                                                                                    <EditFormSettings CaptionLocation="Top" />
                                                                                                </dxwgv:GridViewDataTextColumn>
                                                                                            </Columns>
                                                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                            </SettingsPager>
                                                                                            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm"/>
                                                                                            <SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>

                                                                                                <UpdateButton>
                                                                                                    <Image ToolTip="Salvar" Url="~/imagens/botoes/salvar.PNG">
                                                                                                    </Image>
                                                                                                </UpdateButton>
                                                                                                <CancelButton>
                                                                                                    <Image ToolTip="Cancelar" Url="~/imagens/botoes/cancelar.PNG">
                                                                                                    </Image>
                                                                                                </CancelButton>
                                                                                                <EditButton>
                                                                                                    <Image ToolTip="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                                                                    </Image>
                                                                                                </EditButton>
                                                                                                <DeleteButton>
                                                                                                    <Image ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                                                                    </Image>
                                                                                                </DeleteButton>
                                                                                            </SettingsCommandButton>
                                                                                            <SettingsPopup>
                                                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                    AllowResize="True" />
                                                                                            </SettingsPopup>
                                                                                        </dxwgv:ASPxGridView>
                                                                                        <asp:SqlDataSource ID="sdsDestinatarios" runat="server" DeleteCommand="DELETE FROM [pbh_RelatorioAcompanhamentoDestinatarios] WHERE [CodigoDestinatarioRelatorio] = @CodigoDestinatarioRelatorio"
                                                                                            InsertCommand="INSERT INTO [pbh_RelatorioAcompanhamentoDestinatarios] ([CodigoRelatorio], [Destinatario], [Unidade], [EmailDestinatario]) VALUES (@CodigoRelatorio, @Destinatario, @Unidade, @EmailDestinatario)"
                                                                                            SelectCommand="SELECT * FROM [pbh_RelatorioAcompanhamentoDestinatarios] WHERE ([CodigoRelatorio] = @CodigoRelatorio) ORDER BY [Destinatario]"
                                                                                            UpdateCommand="UPDATE [pbh_RelatorioAcompanhamentoDestinatarios] 
SET [Destinatario] = @Destinatario, 
       [Unidade] = @Unidade, 
       [EmailDestinatario] = @EmailDestinatario 
WHERE [CodigoDestinatarioRelatorio] = @CodigoDestinatarioRelatorio">
                                                                                            <DeleteParameters>
                                                                                                <asp:Parameter Name="CodigoDestinatarioRelatorio" Type="Int32" />
                                                                                            </DeleteParameters>
                                                                                            <InsertParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                                <asp:Parameter Name="Destinatario" Type="String" />
                                                                                                <asp:Parameter Name="Unidade" Type="String" />
                                                                                                <asp:Parameter Name="EmailDestinatario" Type="String" />
                                                                                            </InsertParameters>
                                                                                            <SelectParameters>
                                                                                                <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
                                                                                            </SelectParameters>
                                                                                            <UpdateParameters>
                                                                                                <asp:Parameter Name="Destinatario" Type="String" />
                                                                                                <asp:Parameter Name="Unidade" Type="String" />
                                                                                                <asp:Parameter Name="EmailDestinatario" Type="String" />
                                                                                                <asp:Parameter Name="CodigoDestinatarioRelatorio" Type="Int32" />
                                                                                            </UpdateParameters>
                                                                                        </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                              </PanelCollection>
                        </dxp:ASPxPanel>
                         <table>
                                                    <tr>
                                                        <td style="width: 100%;">
                                                            &nbsp;</td>
                                                        <td style="padding-right: 10px">
                                                            <dxtv:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="90px">
                                                                <ClientSideEvents Click="function(s, e) {
	if(Valida()){
		callback.PerformCallback();
	}
}" />
                                                                <Paddings Padding="0px" />
                                                            </dxtv:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btnCancelar" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar"
                                                                Text="Fechar" Width="90px" >
                                                                <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                                                                <Paddings Padding="0px" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
     
                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
            <dxtc:TabPage Text="Anexos">
                <ContentCollection>
                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div id="divAnexos" runat="server">
                            <iframe runat="server" id="frmAnexos" width="100%" frameborder="0" style="width: 100%;"></iframe>
                        </div>
                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
        </TabPages>
        <Paddings PaddingTop="0px" />
    </dxtc:ASPxPageControl>
    <asp:SqlDataSource ID="sdsRelatorio" runat="server" SelectCommand=" SELECT 
        p.CodigoProjeto,
        p.NomeProjeto,
        g.NomeUsuario AS NomeGerente,
        ra.DataElaboracao,
        e.NomeUsuario AS NomeElaborador,
        un.SiglaUnidadeNegocio,
        ra.DataInicioPlanejada,
        ra.DataInicioReal,
        ra.TerminoPlanejado,
        ra.ProgressoFisico,
        ra.TendenciaTermino,
        ra.StatusTermino,
        ra.PontoFuncaoPlanejado,
        ra.PontoFuncaoProgresso,
        ra.PontoFuncaoTendencia,
        ra.PontoFuncaoStatus,
        ra.InvestimentoPlanejado,
        ra.InvestimentoProgresso,
        ra.InvestimentoTendencia,
        ra.InvestimentoStatus,
        ra.CusteioPlanejado,
        ra.CusteioProgresso,
        ra.CusteioTendencia,
        ra.CusteioStatus,
        ra.ComentarioPrazo,
        ra.ComentarioAtraso,
        ra.ComentarioEscopo,
        ra.ComentarioCusto,
        ra.ComentarioRH,
        ra.ComentarioComunicacoes,
        ra.ComentarioAquisicoesRealizar,
        ra.ComentarioGarantiaQualidade,
        ra.ComentarioGerenciaConfiguracao,
        ra.IndicaDefinicaoAreaSustentacao,
        ra.JustificativaAreaSustentacao,
        ra.IndicaEmissaoDocumentoRepasse,
        ra.QuantMudancaEscopo,
        ra.QuantMudancaCronograma,
        ra.QuantMudancaCusto,
        ra.QuantParalisacao, 
        ra.VariacaoTermino, 
        ra.VariacaoPontoFuncao, 
        ra.VaricaoInvestimento, 
        ra.VariacaoCusteio, 
        us.CodigoUnidadeNegocio AS CodigoUnidadeSustentacao,
        us.SiglaUnidadeNegocio AS SiglaUnidadeSustentacao,
        ra.ProgressoFisicoAnterior,
        ra.QuantReativacao
FROM            pbh_RelatorioAcompanhamento AS ra INNER JOIN
                         Projeto AS p ON ra.CodigoProjeto = p.CodigoProjeto INNER JOIN
                         UnidadeNegocio AS un ON un.CodigoUnidadeNegocio = p.CodigoUnidadeNegocio LEFT OUTER JOIN
                         UnidadeNegocio AS us ON us.CodigoUnidadeNegocio = ra.CodigoUnidadeSustentacao LEFT OUTER JOIN
                         Usuario AS g ON g.CodigoUsuario = p.CodigoGerenteProjeto LEFT OUTER JOIN
                         Usuario AS e ON e.CodigoUsuario = ra.CodigoUsuarioInclusao
  WHERE ra.CodigoRelatorio = @CodigoRelatorio" DeleteCommand="DELETE FROM [pbh_RelatorioAcompanhamento] WHERE [CodigoRelatorio] = @CodigoRelatorio"
        InsertCommand="INSERT INTO [pbh_RelatorioAcompanhamento] ([CodigoProjeto], [DataElaboracao], [DataInicioPlanejada], [DataInicioReal], [TerminoPlanejado], [ProgressoFisico], [TendenciaTermino], [StatusTermino], [PontoFuncaoPlanejado], [PontoFuncaoProgresso], [PontoFuncaoTendencia], [PontoFuncaoStatus], [InvestimentoPlanejado], [InvestimentoProgresso], [InvestimentoTendencia], [InvestimentoStatus], [CusteioPlanejado], [CusteioProgresso], [CusteioTendencia], [CusteioStatus], [ComentarioPrazo], [ComentarioAtraso], [ComentarioEscopo], [ComentarioCusto], [ComentarioRH], [ComentarioComunicacoes], [ComentarioGarantiaQualidade], [ComentarioGerenciaConfiguracao], [IndicaDefinicaoAreaSustentacao], [JustificativaAreaSustentacao], [IndicaEmissaoDocumentoRepasse], [QuantMudancaEscopo], [QuantMudancaCronograma], [QuantMudancaCusto], [QuantParalisacao], [QuantReativacao], [CodigoUsuarioInclusao], [DataUltimaAlteracao], [CodigoUsuarioUltimaAlteracao], [DataExclusao], [CodigoUsuarioExclusao], [DataPublicacao], [CodigoUsuarioPublicacao]) VALUES (@CodigoProjeto, @DataElaboracao, @DataInicioPlanejada, @DataInicioReal, @TerminoPlanejado, @ProgressoFisico, @TendenciaTermino, @StatusTermino, @PontoFuncaoPlanejado, @PontoFuncaoProgresso, @PontoFuncaoTendencia, @PontoFuncaoStatus, @InvestimentoPlanejado, @InvestimentoProgresso, @InvestimentoTendencia, @InvestimentoStatus, @CusteioPlanejado, @CusteioProgresso, @CusteioTendencia, @CusteioStatus, @ComentarioPrazo, @ComentarioAtraso, @ComentarioEscopo, @ComentarioCusto, @ComentarioRH, @ComentarioComunicacoes, @ComentarioGarantiaQualidade, @ComentarioGerenciaConfiguracao, @IndicaDefinicaoAreaSustentacao, @JustificativaAreaSustentacao, @IndicaEmissaoDocumentoRepasse, @QuantMudancaEscopo, @QuantMudancaCronograma, @QuantMudancaCusto, @QuantParalisacao, @QuantReativacao, @CodigoUsuarioInclusao, @DataUltimaAlteracao, @CodigoUsuarioUltimaAlteracao, @DataExclusao, @CodigoUsuarioExclusao, @DataPublicacao, @CodigoUsuarioPublicacao)"
        UpdateCommand=" UPDATE [pbh_RelatorioAcompanhamento] 
    SET [CodigoProjeto] = @CodigoProjeto, 
        [TerminoPlanejado] = @TerminoPlanejado, 
        [ProgressoFisico] = @ProgressoFisico, 
        [TendenciaTermino] = @TendenciaTermino, 
        [StatusTermino] = @StatusTermino, 
        [PontoFuncaoPlanejado] = @PontoFuncaoPlanejado, 
        [PontoFuncaoProgresso] = @PontoFuncaoProgresso, 
        [PontoFuncaoTendencia] = @PontoFuncaoTendencia, 
        [PontoFuncaoStatus] = @PontoFuncaoStatus, 
        [InvestimentoPlanejado] = @InvestimentoPlanejado, 
        [InvestimentoProgresso] = @InvestimentoProgresso, 
        [InvestimentoTendencia] = @InvestimentoTendencia, 
        [InvestimentoStatus] = @InvestimentoStatus, 
        [CusteioPlanejado] = @CusteioPlanejado, 
        [CusteioProgresso] = @CusteioProgresso, 
        [CusteioTendencia] = @CusteioTendencia, 
        [CusteioStatus] = @CusteioStatus, 
        [ComentarioPrazo] = @ComentarioPrazo, 
        [ComentarioAtraso] = @ComentarioAtraso, 
        [ComentarioEscopo] = @ComentarioEscopo, 
        [ComentarioCusto] = @ComentarioCusto, 
        [ComentarioRH] = @ComentarioRH, 
        [ComentarioComunicacoes] = @ComentarioComunicacoes, 
        [ComentarioGarantiaQualidade] = @ComentarioGarantiaQualidade, 
        [ComentarioGerenciaConfiguracao] = @ComentarioGerenciaConfiguracao, 
        [IndicaDefinicaoAreaSustentacao] = @IndicaDefinicaoAreaSustentacao, 
        [JustificativaAreaSustentacao] = @JustificativaAreaSustentacao, 
        [IndicaEmissaoDocumentoRepasse] = @IndicaEmissaoDocumentoRepasse, 
        [DataUltimaAlteracao] = GETDATE(), 
        [CodigoUsuarioUltimaAlteracao] = @CodigoUsuario,
        [QuantMudancaEscopo] = @QuantMudancaEscopo,
        [QuantMudancaCronograma] = @QuantMudancaCronograma,
        [QuantMudancaCusto] = @QuantMudancaCusto,
        [QuantParalisacao] = @QuantParalisacao,
        [QuantReativacao] = @QuantReativacao,
        [CodigoUnidadeSustentacao] = @CodigoUnidadeSustentacao,
        [VariacaoTermino] = @VariacaoTermino, 
        [VariacaoPontoFuncao] = @VariacaoPontoFuncao, 
        [VaricaoInvestimento] = @VaricaoInvestimento, 
        [VariacaoCusteio] = @VariacaoCusteio
  WHERE [CodigoRelatorio] = @CodigoRelatorio">
        <DeleteParameters>
            <asp:Parameter Name="CodigoRelatorio" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="CodigoProjeto" Type="Int32" />
            <asp:Parameter Name="DataElaboracao" Type="DateTime" />
            <asp:Parameter Name="DataInicioPlanejada" Type="DateTime" />
            <asp:Parameter Name="DataInicioReal" Type="DateTime" />
            <asp:Parameter Name="TerminoPlanejado" Type="String" />
            <asp:Parameter Name="ProgressoFisico" Type="String" />
            <asp:Parameter Name="TendenciaTermino" Type="String" />
            <asp:Parameter Name="StatusTermino" Type="String" />
            <asp:Parameter Name="PontoFuncaoPlanejado" Type="String" />
            <asp:Parameter Name="PontoFuncaoProgresso" Type="String" />
            <asp:Parameter Name="PontoFuncaoTendencia" Type="String" />
            <asp:Parameter Name="PontoFuncaoStatus" Type="String" />
            <asp:Parameter Name="InvestimentoPlanejado" Type="String" />
            <asp:Parameter Name="InvestimentoProgresso" Type="String" />
            <asp:Parameter Name="InvestimentoTendencia" Type="String" />
            <asp:Parameter Name="InvestimentoStatus" Type="String" />
            <asp:Parameter Name="CusteioPlanejado" Type="String" />
            <asp:Parameter Name="CusteioProgresso" Type="String" />
            <asp:Parameter Name="CusteioTendencia" Type="String" />
            <asp:Parameter Name="CusteioStatus" Type="String" />
            <asp:Parameter Name="ComentarioPrazo" Type="String" />
            <asp:Parameter Name="ComentarioAtraso" Type="String" />
            <asp:Parameter Name="ComentarioEscopo" Type="String" />
            <asp:Parameter Name="ComentarioCusto" Type="String" />
            <asp:Parameter Name="ComentarioRH" Type="String" />
            <asp:Parameter Name="ComentarioComunicacoes" Type="String" />
            <asp:Parameter Name="ComentarioGarantiaQualidade" Type="String" />
            <asp:Parameter Name="ComentarioGerenciaConfiguracao" Type="String" />
            <asp:Parameter Name="IndicaDefinicaoAreaSustentacao" Type="String" />
            <asp:Parameter Name="JustificativaAreaSustentacao" Type="String" />
            <asp:Parameter Name="IndicaEmissaoDocumentoRepasse" Type="String" />
            <asp:Parameter Name="QuantMudancaEscopo" Type="Int16" />
            <asp:Parameter Name="QuantMudancaCronograma" Type="Int16" />
            <asp:Parameter Name="QuantMudancaCusto" Type="Int16" />
            <asp:Parameter Name="QuantParalisacao" Type="Int16" />
            <asp:Parameter Name="QuantReativacao" />
            <asp:Parameter Name="CodigoUsuarioInclusao" Type="Int32" />
            <asp:Parameter Name="DataUltimaAlteracao" Type="DateTime" />
            <asp:Parameter Name="CodigoUsuarioUltimaAlteracao" Type="Int32" />
            <asp:Parameter Name="DataExclusao" Type="DateTime" />
            <asp:Parameter Name="CodigoUsuarioExclusao" Type="Int32" />
            <asp:Parameter Name="DataPublicacao" Type="DateTime" />
            <asp:Parameter Name="CodigoUsuarioPublicacao" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:QueryStringParameter Name="CodigoRelatorio" QueryStringField="CR" />
            <asp:SessionParameter Name="CodigoUsuario" SessionField="CodigoUsuario" />
            <asp:Parameter Name="CodigoProjeto" />
            <asp:Parameter Name="TerminoPlanejado" />
            <asp:Parameter Name="ProgressoFisico" />
            <asp:Parameter Name="TendenciaTermino" />
            <asp:Parameter Name="StatusTermino" />
            <asp:Parameter Name="PontoFuncaoPlanejado" />
            <asp:Parameter Name="PontoFuncaoProgresso" />
            <asp:Parameter Name="PontoFuncaoTendencia" />
            <asp:Parameter Name="PontoFuncaoStatus" />
            <asp:Parameter Name="InvestimentoPlanejado" />
            <asp:Parameter Name="InvestimentoProgresso" />
            <asp:Parameter Name="InvestimentoTendencia" />
            <asp:Parameter Name="InvestimentoStatus" />
            <asp:Parameter Name="CusteioPlanejado" />
            <asp:Parameter Name="CusteioProgresso" />
            <asp:Parameter Name="CusteioTendencia" />
            <asp:Parameter Name="CusteioStatus" />
            <asp:Parameter Name="ComentarioPrazo" />
            <asp:Parameter Name="ComentarioAtraso" />
            <asp:Parameter Name="ComentarioEscopo" />
            <asp:Parameter Name="ComentarioCusto" />
            <asp:Parameter Name="ComentarioRH" />
            <asp:Parameter Name="ComentarioComunicacoes" />
            <asp:Parameter Name="ComentarioGarantiaQualidade" />
            <asp:Parameter Name="ComentarioGerenciaConfiguracao" />
            <asp:Parameter Name="IndicaDefinicaoAreaSustentacao" />
            <asp:Parameter Name="JustificativaAreaSustentacao" />
            <asp:Parameter Name="IndicaEmissaoDocumentoRepasse" />
            <asp:Parameter Name="QuantMudancaEscopo" />
            <asp:Parameter Name="QuantMudancaCronograma" />
            <asp:Parameter Name="QuantMudancaCusto" />
            <asp:Parameter Name="QuantParalisacao" />
            <asp:Parameter Name="CodigoUnidadeSustentacao" />
            <asp:Parameter Name="VariacaoTermino" />
            <asp:Parameter Name="VariacaoPontoFuncao" />
            <asp:Parameter Name="VaricaoInvestimento" />
            <asp:Parameter Name="VariacaoCusteio" />
            <asp:Parameter Name="QuantReativacao" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {
	OnCallbackComplete(s, e);
}" />
    </dxcb:ASPxCallback>
    </form>
</body>
</html>
