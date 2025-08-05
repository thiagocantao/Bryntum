<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adm_ConfiguracaoPessoais_Popup.aspx.cs"
    Inherits="administracao_adm_ConfiguracaoPessoais_Popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .Tabela {
            width: 100%;
        }
        .auto-style1 {
            width: 300px;
        }
    </style>
    <link href="../estilos/custom.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" class="Tabela">
                <tr>
                    <td>
                        <dxrp:ASPxRoundPanel ID="rpEntradaSistema" runat="server" ClientInstanceName="rpEntradaSistema"
                            HeaderText="Entrada no Sistema" Width="100%">
                            <HeaderStyle Font-Bold="True">
                                <BorderBottom BorderStyle="None" />
                            </HeaderStyle>
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td valign="top" style="width: 250px">
                                                    <dxrp:ASPxRoundPanel runat="server" HeaderText="Entidade" View="GroupBox" Width="100%"
                                                        ID="rpEntidade">
                                                        <PanelCollection>
                                                            <dxp:PanelContent ID="PanelContent2" runat="server">
                                                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.Int32"
                                                                    TextFormatString="{1}" Width="100%" ClientInstanceName="ddlEntidade"
                                                                    ID="ddlEntidade">
                                                                    <Columns>
                                                                        <dxe:ListBoxColumn FieldName="SiglaUnidadeNegocio" Width="100px" Caption="Sigla">
                                                                        </dxe:ListBoxColumn>
                                                                        <dxe:ListBoxColumn FieldName="NomeUnidadeNegocio" Width="300px" Caption="Entidade">
                                                                        </dxe:ListBoxColumn>
                                                                    </Columns>
                                                                </dxe:ASPxComboBox>
                                                            </dxp:PanelContent>
                                                        </PanelCollection>
                                                    </dxrp:ASPxRoundPanel>
                                                </td>
                                                <td></td>
                                                <td valign="top">
                                                    <dxrp:ASPxRoundPanel runat="server" HeaderText="Tela Inicial" View="GroupBox" Width="100%"
                                                        ID="rpPainelDeBordo">
                                                        <PanelCollection>
                                                            <dxp:PanelContent ID="PanelContent3" runat="server">
                                                                <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlTelaInicial"
                                                                    ID="ddlTelaInicial">
                                                                </dxe:ASPxComboBox>
                                                            </dxp:PanelContent>
                                                        </PanelCollection>
                                                    </dxrp:ASPxRoundPanel>
                                                </td>
                                                <td></td>
                                                <td style="width: 200px; display: none;">
                                                    <dxrp:ASPxRoundPanel runat="server" HeaderText="Tipo Portf&#243;lio Padr&#227;o"
                                                        View="GroupBox" ID="rpProtfolioPadrao" Width="100%">
                                                        <PanelCollection>
                                                            <dxp:PanelContent ID="PanelContent4" runat="server">
                                                                <dxe:ASPxRadioButtonList runat="server" RepeatDirection="Horizontal" ClientInstanceName="rblPorfolioPadrao"
                                                                    Width="100%" ID="rblPorfolioPadrao" ItemSpacing="15px">
                                                                    <Paddings PaddingLeft="0px" PaddingRight="0px" />
                                                                    <Paddings PaddingLeft="0px" PaddingRight="0px"></Paddings>
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Geral" Value="G"></dxe:ListEditItem>
                                                                        <dxe:ListEditItem Text="Unidade" Value="U"></dxe:ListEditItem>
                                                                    </Items>
                                                                    <Border BorderColor="Silver" BorderStyle="None"></Border>
                                                                    <DisabledStyle ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxRadioButtonList>
                                                            </dxp:PanelContent>
                                                        </PanelCollection>
                                                    </dxrp:ASPxRoundPanel>
                                                </td>

                                            </tr>
                                        </tbody>

                                    </table>
                                    <dxrp:ASPxRoundPanel ID="rpVisaoInicial" runat="server"
                                        HeaderText="Tela Inicial" Style="text-align: left; margin-left: 0px" View="GroupBox"
                                        Width="100%">
                                        <PanelCollection>
                                            <dxp:PanelContent ID="PanelContent5" runat="server">
                                                <dxe:ASPxComboBox ID="ddlVisaoInicial" runat="server" ClientInstanceName="ddlVisaoInicial"
                                                    ValueType="System.String" Width="100%" IncrementalFilteringMode="Contains">
                                                </dxe:ASPxComboBox>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxrp:ASPxRoundPanel>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <Border BorderWidth="1px" />
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 5px">
                        <dxrp:ASPxRoundPanel ID="rpOpcoesPessoais" runat="server" ClientInstanceName="rpOpcoesPessoais"
                            HeaderText="Opções Pessoais" Width="100%">
                            <HeaderStyle Font-Bold="True"></HeaderStyle>
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent6" runat="server">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td class="auto-style1" style="display:none;">
                                                                    <dxe:ASPxLabel runat="server" Text="Tema Visual:" ClientInstanceName="lblTemaVisual"
                                                                        ID="lblTemaVisual" Visible="False">
                                                                    </dxe:ASPxLabel>
                                                                </td>

                                                                <td style="padding-left:5px;">
                                                                    <dxe:ASPxLabel runat="server" Text="Mapa Estrat&#233;gico:" ClientInstanceName="lblMapaEstrtegico"
                                                                        ID="lblMapaEstrtegico">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="auto-style1" style="display:none;">
                                                                    <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="cmbModeloVisual"
                                                                        ID="cmbModeloVisual" Visible="False">
                                                                        <Items>
<%--                                                                            <dxe:ListEditItem Text="Padr&#227;o" Value="Default"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Text="Aqua" Value="Aqua"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Text="Black Glass" Value="BlackGlass"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Text="Glass" Value="Glass"></dxe:ListEditItem>--%>
                                                                            <dxe:ListEditItem Text="Material Compact" Value="MaterialCompact"></dxe:ListEditItem>
<%--                                                                            <dxe:ListEditItem Text="Office2003 Blue" Value="Office2003Blue"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Text="Office2003 Olive" Value="Office2003Olive"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Text="Office2003 Silver" Value="Office2003Silver"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Text="Office2010 Black" Value="Office2010Black"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Text="Office2010 Blue" Value="Office2010Blue"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Text="Office2010 Silver" Value="Office2010Silver" />
                                                                            <dxe:ListEditItem Text="Plastic Blue" Value="PlasticBlue"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Text="Red Wine" Value="RedWine" />
                                                                            <dxe:ListEditItem Text="Soft Orange" Value="SoftOrange" />
                                                                            <dxe:ListEditItem Text="Youthful" Value="Youthful" />
                                                                            <dxe:ListEditItem Text="Express" Value="DevEx"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Text="Metropolis" Value="Metropolis" />--%>
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>

                                                                <td style="padding-left:5px;">
                                                                    <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlMapaEstrategico"
                                                                        ID="ddlMapaEstrategico">
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top: 5px;">
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td width="width: 260px">
                                                                    <dxtv:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server"
                                                                        HeaderText="Verificar atraso nas tarefas do relatório de atividades utilizando"
                                                                        View="GroupBox" Width="450px">
                                                                        <PanelCollection>
                                                                            <dxtv:PanelContent runat="server">
                                                                                <dxtv:ASPxRadioButtonList ID="rblAtrasoAtividades" runat="server" ClientInstanceName="rblAtrasoAtividades"
                                                                                    ItemSpacing="15px" RepeatDirection="Horizontal"
                                                                                    Width="400px">
                                                                                    <Paddings PaddingLeft="0px" PaddingRight="0px" />
                                                                                    <Items>
                                                                                        <dxtv:ListEditItem Text="Linha de Base" Value="LB" />
                                                                                        <dxtv:ListEditItem Text="Reprogramação" Value="RP" />
                                                                                    </Items>
                                                                                    <Border BorderColor="Silver" BorderStyle="None" />
                                                                                    <DisabledStyle ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxtv:ASPxRadioButtonList>
                                                                            </dxtv:PanelContent>
                                                                        </PanelCollection>
                                                                    </dxtv:ASPxRoundPanel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxCheckBox ID="ckbEmailSemanal" runat="server" CheckState="Unchecked" ClientInstanceName="ckbEmailSemanal">
                                                                    </dxe:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <table class="formulario-botoes-direita">
                            <tr>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="7">
                                        <tr>
                                            <td>
                                                <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                                                    Text="Salvar" Width="100px" AutoPostBack="False">
                                                    <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                    <ClientSideEvents Click="function(s, e) 
{
	if( validaCamposFormulario() != &quot;&quot;)
    {
    	window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'atencao', true, false, null);
        e.processOnServer = false;
	}	
	else
	{
		callbackGeralConfig.PerformCallback();
	}          
}"></ClientSideEvents>
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar"
                                                    Text="Fechar" Width="100px" AutoPostBack="False">
                                                    <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                    <ClientSideEvents Click="function(s, e) 
{
window.top.fechaModal();      
}"></ClientSideEvents>
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            &nbsp;
        </div>
        <dxcb:ASPxCallback ID="callbackGeralConfig" runat="server" ClientInstanceName="callbackGeralConfig"
            OnCallback="callbackGeral_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
//debugger
if(s.cp_msg != null &amp;&amp; s.cp_msg != &quot;&quot;)
		window.top.mostraMensagem(s.cp_msg, 'sucesso', false, false, null);
}" />
        </dxcb:ASPxCallback>
    </form>
</body>
</html>
