<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="cni_resumoProjeto.aspx.cs" Inherits="_Projetos_DadosProjeto_cni_resumoProjeto" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="left" style="height: 19px; padding-left:10px" valign="middle">
                            <table>
                                <tr>
                                    <td style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Resumo" ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" style="padding-right: 10px">
                    <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divGeral" runat="server">
        <table>
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                        Text="Nome do Projeto:">
                    </dxe:ASPxLabel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td >
                    <dxe:ASPxTextBox ID="txtNomeProjeto" runat="server" ClientInstanceName="txtNomeProjeto"
                        Width="100%" Enabled="False" >
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="padding-bottom: 5px;">
                    <table>
                        <tr>
                            <td>
                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                        Text="Unidade Responsável:">
                    </dxe:ASPxLabel>
                            </td>
                            <td style="width: 275px">
                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                        Text="Gerente da Unidade:">
                    </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px">
                    <dxe:ASPxTextBox ID="txtUnidadeResponsavel" runat="server" ClientInstanceName="txtUnidadeResponsavel"
                        Width="100%" Enabled="False" >
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                            </td>
                            <td style="width: 275px">
                    <dxe:ASPxTextBox ID="txtGerenteUnidade" runat="server" ClientInstanceName="txtGerenteUnidade"
                        Width="100%" Enabled="False" >
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                        Text="Objetivo do Projeto:">
                    </dxe:ASPxLabel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td >
                    <dxe:ASPxMemo ID="memObjetivoProjeto" runat="server" ClientInstanceName="memObjetivoProjeto"
                        Height="71px" Width="100%" Enabled="False" >
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="padding-bottom: 3px;">
                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvAcao" KeyFieldName="CodigoMetaOperacional"
                        AutoGenerateColumns="False" EnableRowsCache="False" Width="100%"
                        ID="gvAcao" EnableViewState="False">
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	if(window.pcDados &amp;&amp; pcDados.GetVisible())
		OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
{
	OnClick_CustomButtons(s, e);
}"></ClientSideEvents>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoAcao"
                                Caption="Ação Transformadora" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowGroup="False" AllowSort="False" AllowDragDrop="False"></SettingsBehavior>
                        <SettingsPager PageSize="100" Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="65"></Settings>
                        <SettingsText CommandClearFilter="Limpar filtro"></SettingsText>
                    </dxwgv:ASPxGridView>
                </td>
                <td>
                </td>
            </tr>            
            <tr>
                <td>
                </td>
                <td style="padding-top: 3px">
                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server" 
                        Text="Produto Final / Resultados Esperados">
                    </dxe:ASPxLabel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td >
                    <dxe:ASPxMemo ID="memProdFinalResEsperados" runat="server" ClientInstanceName="memProdFinalResEsperados"
                        Height="71px" Width="100%" Enabled="False" >
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                        Text="Cronograma Básico:">
                    </dxe:ASPxLabel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td >
                    <dxe:ASPxMemo ID="memCronograma" runat="server" ClientInstanceName="memCronograma"
                        Height="71px" Width="100%" Enabled="False" 
                       >
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
                <td>
                </td>
            </tr>
            </table>
    </div>
</asp:Content>
