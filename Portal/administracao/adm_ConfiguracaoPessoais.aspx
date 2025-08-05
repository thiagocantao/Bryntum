<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="adm_ConfiguracaoPessoais.aspx.cs" Inherits="administracao_adm_ConfiguracaoPessoais"
    Title="Configurações Pessoais" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1"  ClientIDMode="AutoID" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table style="width: 100%">
        <tr>
            <td id="ConteudoPrincipal">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <dxrp:ASPxRoundPanel ID="rpEntradaSistema" runat="server" ClientInstanceName="rpEntradaSistema"
                                HeaderText="Entrada no Sistema" Width="100%">
                                <HeaderStyle Font-Bold="True">
                                    <BorderBottom BorderStyle="None" />
                                </HeaderStyle>
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" style="width: 450px">
                                                        <dxrp:ASPxRoundPanel runat="server" HeaderText="<%$ Resources:traducao, adm_ConfiguracaoPessoais_entidade %>" View="GroupBox" Width="100%"
                                                            ID="rpEntidade">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
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
                                                                    <br />
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxrp:ASPxRoundPanel>
                                                    </td>
                                                    <td valign="top">
                                                        <dxrp:ASPxRoundPanel runat="server" HeaderText="<%$ Resources:traducao, adm_ConfiguracaoPessoais_tela_inicial %>" View="GroupBox" Width="100%"
                                                            ID="rpPainelDeBordo">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlTelaInicial"
                                                                        ID="ddlTelaInicial">
                                                                    </dxe:ASPxComboBox>
                                                                    <br />
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxrp:ASPxRoundPanel>
                                                    </td>
                                                    <td style="width: 260px; display: none;">
                                                        <dxrp:ASPxRoundPanel runat="server" HeaderText="Tipo Portf&#243;lio Padr&#227;o"
                                                            View="GroupBox" ID="rpProtfolioPadrao" Width="100%">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
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
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                                                                <tr>
                                                    <td valign="top" style="width: 450px">
                                                        <dxtv:ASPxRoundPanel ID="rpVisaoInicial" runat="server" HeaderText="Carteira inicial *" Style="text-align: left; margin-left: 0px;" View="GroupBox" Width="100%">
                                                            <PanelCollection>
                                                                <dxtv:PanelContent runat="server">
                                                                    <dxtv:ASPxComboBox ID="ddlVisaoInicial" runat="server" ClientInstanceName="ddlVisaoInicial" Width="100%">
                                                                    </dxtv:ASPxComboBox>
                                                                    <br />
                                                                </dxtv:PanelContent>
                                                            </PanelCollection>
                                                        </dxtv:ASPxRoundPanel>
                                                    </td>
                                                    <td valign="top">
                                                        <dxrp:ASPxRoundPanel runat="server" HeaderText="Tela Inicial Mobile*" View="GroupBox" Width="100%" ClientVisible="false"
                                                            ID="ASPxRoundPanel3">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <dxtv:ASPxTextBox ID="txtTelaInicialMobile" runat="server" ClientInstanceName="txtTelaInicialMobile" Width="100%">
                                                                    </dxtv:ASPxTextBox>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxrp:ASPxRoundPanel>
                                                    </td>
                                                    <td style="width: 260px; display: none;">
                                                        <dxrp:ASPxRoundPanel runat="server" HeaderText="Tipo Portf&#243;lio Padr&#227;o"
                                                            View="GroupBox" ID="ASPxRoundPanel4" Width="100%">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <dxe:ASPxRadioButtonList runat="server" RepeatDirection="Horizontal" ClientInstanceName="rblPorfolioPadrao"
                                                                        Width="100%" ID="ASPxRadioButtonList1" ItemSpacing="15px">
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
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <Border BorderWidth="1px" />

                            </dxrp:ASPxRoundPanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxrp:ASPxRoundPanel ID="rpOpcoesPessoais" runat="server" ClientInstanceName="rpOpcoesPessoais"
                                HeaderText="Opções Pessoais" Width="100%">
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 300px; display: none;">
                                                                        <dxe:ASPxLabel runat="server" Text="Tema Visual:" ClientInstanceName="lblTemaVisual"
                                                                            ID="lblTemaVisual" Visible="False">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Mapa Estrat&#233;gico:" ClientInstanceName="lblMapaEstrtegico"
                                                                            ID="lblMapaEstrtegico">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="display: none;">
                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" AutoPostBack="True"
                                                                            ClientInstanceName="cmbModeloVisual" ID="cmbModeloVisual"
                                                                            OnSelectedIndexChanged="cmbModeloVisual_SelectedIndexChanged" Visible="False">
                                                                            <Items>
<%--                                                                                <dxe:ListEditItem Text="Padr&#227;o" Value="Default"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="Aqua" Value="Aqua"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="Black Glass" Value="BlackGlass"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="Express" Value="DevEx"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="Glass" Value="Glass"></dxe:ListEditItem>--%>
                                                                                <dxe:ListEditItem Text="Material Compact" Value="MaterialCompact"></dxe:ListEditItem>
<%--                                                                                <dxe:ListEditItem Text="Metropolis" Value="Metropolis" />
                                                                                <dxe:ListEditItem Text="Moderno" Value="Moderno" />
                                                                                <dxe:ListEditItem Text="Office2003 Blue" Value="Office2003Blue"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="Office2003 Olive" Value="Office2003Olive"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="Office2003 Silver" Value="Office2003Silver"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="Office2010 Black" Value="Office2010Black"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="Office2010 Blue" Value="Office2010Blue"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="Office2010 Silver" Value="Office2010Silver" />
                                                                                <dxe:ListEditItem Text="Plastic Blue" Value="PlasticBlue"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Text="Red Wine" Value="RedWine" />
                                                                                <dxe:ListEditItem Text="Soft Orange" Value="SoftOrange" />
                                                                                <dxe:ListEditItem Text="Youthful" Value="Youthful" />--%>
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="ddlMapaEstrategico"
                                                                            ID="ddlMapaEstrategico">
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                                    <td style="height: 10px">&nbsp;
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td width="width: 260px">
                                                                        <dxrp:ASPxRoundPanel runat="server" HeaderText="<%$ Resources:traducao, adm_ConfiguracaoPessoais_verificar_atraso_nas_tarefas_do_relat_rio_de_atividades_utilizando_ %>"
                                                                            View="GroupBox" ID="ASPxRoundPanel1" Width="450px">
                                                                            <PanelCollection>
                                                                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                                                                    <dxe:ASPxRadioButtonList runat="server" RepeatDirection="Horizontal" ClientInstanceName="rblAtrasoAtividades"
                                                                                        Width="400px" ID="rblAtrasoAtividades" ItemSpacing="15px">
                                                                                        <Paddings PaddingLeft="0px" PaddingRight="0px" />
                                                                                        <Items>
                                                                                            <dxe:ListEditItem Text="Linha de Base" Value="LB"></dxe:ListEditItem>
                                                                                            <dxe:ListEditItem Text="Reprogramação" Value="RP"></dxe:ListEditItem>
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
                                                        </table>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td style="height: 10px"></td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="ckbEmailSemanal" runat="server" CheckState="Unchecked"
                                                            ClientInstanceName="ckbEmailSemanal">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <dxrp:ASPxRoundPanel ID="rpGraficoTabelas" runat="server" ClientInstanceName="rpGraficoTabelas"
                                                            HeaderText="Gráficos e Tabelas" Visible="False"
                                                            Width="100%">
                                                            <HeaderStyle Font-Bold="True" />
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxCheckBox ID="ckBordasArredondadas" runat="server" CheckState="Unchecked"
                                                                                        ClientInstanceName="ckBordasArredondadas"
                                                                                        Text="Bordas Arredondadas nos Gráficos.">
                                                                                    </dxe:ASPxCheckBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxCheckBox ID="ckAplicarGradiente" runat="server" CheckState="Unchecked" ClientInstanceName="ckAplicarGradiente"
                                                                                        Text="Aplicar Gradiente nos Gráficos.">
                                                                                    </dxe:ASPxCheckBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxCheckBox ID="ckTabelasComPaginacao" runat="server" CheckState="Unchecked"
                                                                                        ClientInstanceName="ckTabelasComPaginacao" ClientVisible="False"
                                                                                        Text="Tabelas com Paginação.">
                                                                                    </dxe:ASPxCheckBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                            <Border BorderWidth="1px"></Border>
                                                        </dxrp:ASPxRoundPanel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <Border BorderWidth="1px" />
                            </dxrp:ASPxRoundPanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td class="formulario-botao">
                                            <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                                                Text="Salvar" Width="100px" AutoPostBack="False">
                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;

	if( validaCamposFormulario() != '')
    	{
    		window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'Atencao', true, false, null);       
	}	
	else
	{
		callbackGeralConfig.PerformCallback();
	}          
}"></ClientSideEvents>
                                            </dxe:ASPxButton>
                                            <dxcb:ASPxCallback ID="callbackGeralConfig" runat="server" ClientInstanceName="callbackGeralConfig"
                                                OnCallback="callbackGeral_Callback">
                                                <ClientSideEvents EndCallback="function(s, e) {

if(s.cp_msg != null &amp;&amp; s.cp_msg != '')
		window.top.mostraMensagem(s.cp_msg, 'sucesso', false, false, null);
}" />
                                            </dxcb:ASPxCallback>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
