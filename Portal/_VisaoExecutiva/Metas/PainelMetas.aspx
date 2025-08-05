<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="PainelMetas.aspx.cs" Inherits="_VisaoExecutiva_Metas_listaMetas" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr style="height: 26px">
                <td align="center" valign="middle"></td>
                <td valign="middle">
                    <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                        Font-Overline="False" Font-Strikeout="False"
                        Text="<%# Resources.traducao.PainelMetas_painel_de_metas_estrat_gicas %>"></asp:Label></td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="padding-top: 3px; padding-bottom: 3px">
                    <table>
                        <tr>
                            <td align="left">
                                <table>
                                    <tr>
                                        <td align="right">
                                            <dxe:ASPxLabel runat="server" Text="Unidade de Negócio:"
                                                ClientInstanceName="lblUnidade"
                                                ID="lblUnidade">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" style="width: 150px">
                                            <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains"
                                                TextFormatString="{0}" Width="150px"
                                                ClientInstanceName="ddlUnidadeNegocio"
                                                ID="ddlUnidadeNegocio">
                                                <Columns>
                                                    <dxe:ListBoxColumn FieldName="SiglaUnidadeNegocio" Width="140px" Caption="Sigla"></dxe:ListBoxColumn>
                                                    <dxe:ListBoxColumn FieldName="NomeUnidadeNegocio" Width="300px" Caption="Nome"></dxe:ListBoxColumn>
                                                </Columns>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" style="width: 43px;">
                                <dxe:ASPxLabel runat="server" Text="Mapa:" ClientInstanceName="lblUnidade"
                                    ID="lblUnidade0">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 210px;">
                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains"
                                    Width="100%" ClientInstanceName="ddlMapa"
                                    ID="ddlMapa">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="right" style="width: 70px;">
                                <dxe:ASPxLabel runat="server" Text="Indicador:" ClientInstanceName="lblUnidade"
                                    ID="lblUnidade1">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left">
                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains"
                                    Width="100%" ClientInstanceName="ddlIndicador"
                                    ID="ddlIndicador">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="right" style="width: 85px;">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                    Text="Responsável:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 180px; padding-right: 10px;">
                                <dxe:ASPxComboBox ID="ddlResponsavel" runat="server" ClientInstanceName="ddlResponsavel"
                                    Width="100%"
                                    IncrementalFilteringMode="Contains" TextFormatString="{0}"
                                    ValueType="System.Int32">
                                    <Columns>
                                        <dxe:ListBoxColumn FieldName="NomeUsuario" Width="200px" Caption="Nome"></dxe:ListBoxColumn>
                                        <dxe:ListBoxColumn FieldName="EMail" Width="300px" Caption="Email"></dxe:ListBoxColumn>
                                    </Columns>
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="padding-right: 10px">
                                <dxe:ASPxButtonEdit ID="txtPesquisa" runat="server"
                                    ClientInstanceName="txtPesquisa"
                                    NullText="Pesquisar pela descrição da meta..." Width="100%" Height="20px">
                                    <ClientSideEvents ButtonClick="function(s, e) {
	gvDados.PerformCallback();
}" />
                                    <Buttons>
                                        <dxe:EditButton Enabled="False">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Search"
                                                    HottrackedCssClass="Sprite_SearchHover" PressedCssClass="Sprite_SearchHover" />
                                            </Image>
                                        </dxe:EditButton>
                                    </Buttons>
                                    <ButtonStyle CssClass="MailMenuSearchBoxButton" />
                                </dxe:ASPxButtonEdit>
                            </td>
                            <td style="width: 286px">
                                <table id="tbFiltro" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center" style="width: 55px" valign="bottom">
                                            <table cellpadding="0" cellspacing="0" style="width: 30px">
                                                <tr>
                                                    <td align="right">
                                                        <dxe:ASPxCheckBox ID="ckbAzul" runat="server" ClientInstanceName="ckbAzul"
                                                            Text=" " Checked="True">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td align="left">
                                                        <img alt="" src="../../imagens/azul.gif" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="width: 55px" valign="bottom">
                                            <table cellpadding="0" cellspacing="0" style="width: 30px">
                                                <tr>
                                                    <td align="right">
                                                        <dxe:ASPxCheckBox ID="ckbVerde" runat="server" ClientInstanceName="ckbVerde"
                                                            Text=" " Checked="True">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td align="left">
                                                        <img alt="" src="../../imagens/verde.gif" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="width: 55px" valign="bottom">
                                            <table cellpadding="0" cellspacing="0" style="width: 30px">
                                                <tr>
                                                    <td align="right" style="width: 20px">
                                                        <dxe:ASPxCheckBox ID="ckbAmarelo" runat="server" ClientInstanceName="ckbAmarelo"
                                                            Text=" " Checked="True">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td align="left">
                                                        <img alt="" src="../../imagens/amarelo.gif" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="width: 55px" valign="bottom">
                                            <table cellpadding="0" cellspacing="0" style="width: 30px">
                                                <tr>
                                                    <td align="right" style="width: 20px">
                                                        <dxe:ASPxCheckBox ID="ckbVermelho" runat="server" ClientInstanceName="ckbVermelho"
                                                            Text=" " Checked="True">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td align="left">
                                                        <img alt="" src="../../imagens/vermelho.gif" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="width: 55px" valign="bottom">
                                            <table cellpadding="0" cellspacing="0" style="width: 30px">
                                                <tr>
                                                    <td align="right">
                                                        <dxe:ASPxCheckBox ID="ckbBranco" runat="server" ClientInstanceName="ckbBranco"
                                                            Text=" " Checked="True">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td align="left">
                                                        <img alt="" src="../../imagens/branco.gif" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 100px;" align="right">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server"
                                    Text="Selecionar" Width="100px">
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <iframe frameborder="0" name="I1" scrolling="auto" src="<%=paginaIncialMetas %>"
                        width="100%" style="height: <%=alturaTela %>" id="I1"></iframe>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
