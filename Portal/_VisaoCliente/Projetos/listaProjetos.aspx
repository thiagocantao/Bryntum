<%@ Page Language="C#"  MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="listaProjetos.aspx.cs" Inherits="_VisaoCliente_Projetos_listaProjetos" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="
            width: 100%">
            <tr style="height:26px">
                <td align="center" valign="middle">
                    </td>
                <td valign="middle">
                    <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                        Font-Overline="False" Font-Strikeout="False"
                        Text="Meu Painel de Bordo"></asp:Label></td>               
                <td align="right" style="display: block; width: 90px; font-family: Arial">
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                        Text="Projeto:">
                    </dxe:ASPxLabel>
                </td>
                <td align="left" style="display: block; width: 225px; font-family: Arial">
                    <dxe:ASPxTextBox ID="txtNomeProjeto" runat="server" 
                        Width="220px">
                    </dxe:ASPxTextBox>
                </td>
               
                <td id="tdFiltroMetas" align="right" style="display: block; width: 230px; font-family: Arial">
                    <table id="tbFiltro" border="0" cellpadding="0" cellspacing="0">
                        <tr>
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
                <td align="right" style="width: 8px; font-family: Arial">
                </td>                
                <td id="tdBotao" align="left" style="width: 80px; font-family: Arial">
                    <dxe:ASPxButton ID="btnSelecionar" runat="server" 
                        Text="Selecionar">
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>
                </td>
                <td style="width: 5px; font-family: Arial">
                </td>
            </tr>
        </table>
       <iframe frameborder="0" name="graficos" scrolling="auto" src="<%=paginaIncialMetas %>"
                    width="100%" style="height: <%=alturaTela %>" id="frmVC"></iframe>
        
    </div>      
    
</asp:Content>
