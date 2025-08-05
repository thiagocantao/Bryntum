<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento_Simulacao.aspx.cs"
    Inherits="_Portfolios_frameSelecaoBalanceamento_Simulacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <title>Gráfico gantt</title>

    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        var statusVC = 1;       
        function atualizaDados() {
            btnSelecionar.DoClick()
        }
    </script>
</head>
<body style="margin-top: 0px">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td>
                            </td>
                            <td align="right" style="width: 70px; height: 5px">
                            </td>
                            <td style="width: 95px; height: 5px">
                            </td>
                            <td style="width: 85px; height: 5px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table cellpadding="0" cellspacing="0" style="">
                                    <tr>
                                        <td>
                                            <dxe:ASPxImage ID="imgVisao" runat="server" ImageUrl="~/imagens/ganttBotao.png"
                                                ToolTip="Visualizar Gantt">     
                                                <ClientSideEvents Click="function(s, e) 
{
	window.location.href = './frameSelecaoBalanceamento_SimulacaoGantt.aspx';
}" />
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" style="width: 70px; padding-right: 8px;">
                                <dxe:ASPxLabel ID="lblCenario" runat="server"
                                    Text="Cenário:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dxe:ASPxComboBox ID="ddlCenario" runat="server" 
                                    Width="100px" ClientInstanceName="ddlCenario" SelectedIndex="0" ValueType="System.String">
                                    <Items>
                                        <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, cen_rio_1 %>" Value="1" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_2 %>" Value="2" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_3 %>" Value="3" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_4 %>" Value="4" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_5 %>" Value="5" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_6 %>" Value="6" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_7 %>" Value="7" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_8 %>" Value="8" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_9 %>" Value="9" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                            <td style="width: 85px">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False"  Text="Selecionar">
                                    <Paddings Padding="0px" />
                                    <ClientSideEvents Click="function(s, e) 
{
	pnCallback.PerformCallback('AtualizarVC');
}" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 6px">
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td valign="top"">
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <iframe id="frm01" frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no"
                                                            src="./SelecaoBalanceamento/sb_001.aspx" width="100%"></iframe>
                                                    </td>
                                                    <td style="width: 5px">
                                                    </td>
                                                    <td>
                                                        <iframe id="frm02" frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no"
                                                            src="./SelecaoBalanceamento/sb_002.aspx" width="100%"></iframe>
                                                    </td>
                                                    <td style="width: 5px">
                                                    </td>
                                                    <td>
                                                        <iframe id="frm03" frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no"
                                                            src="./SelecaoBalanceamento/sb_003.aspx" width="100%"></iframe>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <iframe id="frm04" frameborder="0" height="<%=metadeAlturaTela %>" scrolling="no"
                                                src="./SelecaoBalanceamento/sb_004.aspx" width="100%"></iframe>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 5px">
                            </td>
                            <td style="width: 230px;">
                                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                                    OnCallback="pnCallback_Callback">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <dxrp:ASPxRoundPanel ID="pNumeros" runat="server"
                                                HeaderText="Projetos" Width="220px">
                                                <ContentPaddings Padding="0px" />
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server">
                                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                            <tr>
                                                                <td align="left" style="height: 8px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 5px">
                                                                            </td>
                                                                            <td style="width: 85px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                                    Text="Em Execu&#231;&#227;o:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right">
                                                                                <dxe:ASPxLabel ID="lblProjetosExecucao" runat="server"
                                                                                    Text="0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right" style="width: 2px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px; height: 8px">
                                                                            </td>
                                                                            <td style="width: 85px; height: 8px;">
                                                                            </td>
                                                                            <td align="right" style="height: 8px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="right" style="width: 2px; height: 8px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px">
                                                                            </td>
                                                                            <td style="width: 85px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                                    Text="Novos:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right">
                                                                                <dxe:ASPxLabel ID="lblNovosProjetos" runat="server"
                                                                                    Text="0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right" style="width: 2px">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 10px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid;
                                                                    border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; height: 23px;
                                                                    background-color: #ebebeb">
                                                                    &nbsp;<dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                                        Text="N&#250;meros">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 8px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 5px">
                                                                            </td>
                                                                            <td style="width: 80px">
                                                                                <dxe:ASPxLabel ID="lblDespesaLabel" runat="server" 
                                                                                    Text="Despesa(R$):" ClientInstanceName="lblDespesaLabel">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right">
                                                                                <dxe:ASPxLabel ID="lblDespesa" runat="server" 
                                                                                    Text="0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right" style="width: 2px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px; height: 8px">
                                                                            </td>
                                                                            <td style="width: 80px; height: 8px;">
                                                                            </td>
                                                                            <td align="right" style="height: 8px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="right" style="width: 2px; height: 8px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px">
                                                                            </td>
                                                                            <td style="width: 80px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                                                                    Text="Receita(R$):">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right">
                                                                                <dxe:ASPxLabel ID="lblReceita" runat="server" 
                                                                                    Text="0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right" style="width: 2px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px; height: 8px">
                                                                            </td>
                                                                            <td style="width: 80px; height: 8px">
                                                                            </td>
                                                                            <td align="right" style="height: 8px">
                                                                            </td>
                                                                            <td align="right" style="width: 2px; height: 8px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px">
                                                                            </td>
                                                                            <td style="width: 80px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                                                                    Text="Recurso(h):">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right">
                                                                                <dxe:ASPxLabel ID="lblTrabalho" runat="server" 
                                                                                    Text="0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right" style="width: 2px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px; height: 8px">
                                                                            </td>
                                                                            <td style="width: 80px; height: 8px">
                                                                            </td>
                                                                            <td align="right" style="height: 8px">
                                                                            </td>
                                                                            <td align="right" style="width: 2px; height: 8px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px">
                                                                            </td>
                                                                            <td style="width: 80px">
                                                                                <dxe:ASPxLabel ID="lblTituloScore" runat="server" 
                                                                                    Text="Import&#226;ncia:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right">
                                                                                <dxe:ASPxLabel ID="lblCriterios" runat="server" 
                                                                                    Text="0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right" style="width: 2px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px; height: 8px">
                                                                            </td>
                                                                            <td style="width: 80px; height: 8px">
                                                                            </td>
                                                                            <td align="right" style="height: 8px">
                                                                            </td>
                                                                            <td align="right" style="width: 2px; height: 8px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px">
                                                                            </td>
                                                                            <td style="width: 80px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                                                                    Text="Complexidade:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right">
                                                                                <dxe:ASPxLabel ID="lblRiscos" runat="server" 
                                                                                    Text="0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right" style="width: 2px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px; height: 8px">
                                                                            </td>
                                                                            <td style="width: 80px; height: 8px">
                                                                            </td>
                                                                            <td align="right" style="height: 8px">
                                                                            </td>
                                                                            <td align="right" style="width: 2px; height: 8px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px">
                                                                            </td>
                                                                            <td style="width: 80px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" 
                                                                                    Text="TIR:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right">
                                                                                <dxe:ASPxLabel ID="lblTIR" runat="server"  Text="0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right" style="width: 2px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px; height: 8px">
                                                                            </td>
                                                                            <td style="width: 80px; height: 8px">
                                                                            </td>
                                                                            <td align="right" style="height: 8px">
                                                                            </td>
                                                                            <td align="right" style="width: 2px; height: 8px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px">
                                                                            </td>
                                                                            <td style="width: 80px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel11" runat="server" 
                                                                                    Text="VPL(R$):">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right">
                                                                                <dxe:ASPxLabel ID="lblVPL" runat="server"  Text="0">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="right" style="width: 2px">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5px">
                                                                            </td>
                                                                            <td style="width: 80px">
                                                                            </td>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="right" style="width: 2px">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 10px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                        <tr>
                                                                            <td align="left" style="border-right: #c6c6c6 1px solid; border-top: #c6c6c6 1px solid;
                                                                                border-left: #c6c6c6 1px solid; border-bottom: #c6c6c6 1px solid; height: 23px;
                                                                                background-color: #ebebeb">
                                                                                &nbsp;<dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                                    Text="Legenda dos Gr&#225;ficos">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 8px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel ID="ASPxLabel12" runat="server"
                                                                        Text="1K = 1.000">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 8px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel ID="ASPxLabel13" runat="server"
                                                                        Text="1M = 1.000.000">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxrp:ASPxRoundPanel>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                    <ClientSideEvents EndCallback="function(s, e) {
	document.getElementById('frm01').src = './SelecaoBalanceamento/sb_001.aspx';
	document.getElementById('frm02').src = './SelecaoBalanceamento/sb_002.aspx';
	document.getElementById('frm03').src = './SelecaoBalanceamento/sb_003.aspx';
	document.getElementById('frm04').src = './SelecaoBalanceamento/sb_004.aspx';
}" />
                                    <Styles>
                                        <LoadingPanel>
                                        </LoadingPanel>
                                    </Styles>
                                </dxcp:ASPxCallbackPanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
