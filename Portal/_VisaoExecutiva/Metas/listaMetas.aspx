<%@ Page Language="C#"  MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="listaMetas.aspx.cs" Inherits="_VisaoExecutiva_Metas_listaMetas" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr style="height:26px">
                <td align="center" valign="middle">
                    </td>
                <td valign="middle">
                    <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                        Font-Overline="False" Font-Strikeout="False"
                        Text="Painel de Metas Estratégicas"></asp:Label></td>               
                <td align="right" style="width: 90px;">
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                        Text="Responsável:">
                    </dxe:ASPxLabel>
                </td>
                <td align="left" style="width: 260px;">
                    <dxe:ASPxComboBox ID="ddlResponsavel" runat="server" ClientInstanceName="ddlResponsavel"
                         Width="250px" IncrementalFilteringMode="Contains" TextFormatString="{0}" ValueType="System.Int32"><Columns>
<dxe:ListBoxColumn FieldName="NomeUsuario" Width="200px" Caption="Nome"></dxe:ListBoxColumn>
<dxe:ListBoxColumn FieldName="EMail" Width="300px" Caption="Email"></dxe:ListBoxColumn>
</Columns>
</dxe:ASPxComboBox>
                </td>
               
                <td id="tdFiltroMetas" align="right" style="width: 285px;">
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
                <td align="right" style="width: 8px;">
                </td>                
                <td id="tdBotao" align="left" style="width: 80px;">
                    <dxe:ASPxButton ID="btnSelecionar" runat="server" 
                        Text="Selecionar">
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>
                </td>
                <td style="width: 5px;">
                </td>
            </tr>
        </table>
       <iframe frameborder="0" name="graficos" scrolling="auto" src="<%=paginaIncialMetas %>"
                    width="100%" style="height: <%=alturaTela %>" id="frmVC"></iframe>
        
    </div>      
    
</asp:Content>
