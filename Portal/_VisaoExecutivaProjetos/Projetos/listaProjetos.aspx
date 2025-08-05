<%@ Page Language="C#"  MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="listaProjetos.aspx.cs" Inherits="_VisaoExecutivaProjetos_Projetos_listaProjetos" %>
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
                        Text="Meu Painel de Bordo"></asp:Label></td>               
                <td align="right" style="width: 90px;">
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                        Text="Projeto:">
                    </dxe:ASPxLabel>
                </td>
                <td align="left" style="width: 225px;">
                    <dxe:ASPxTextBox ID="txtNomeProjeto" runat="server" 
                        Width="220px">
                    </dxe:ASPxTextBox>
                </td>
               
                <td id="tdFiltroMetas" align="right" style="width: 230px;">
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
                            <td align="center" style="width: 55px; <%=displayLaranja %>" valign="bottom">
                                <table cellpadding="0" cellspacing="0" style="width: 30px">
                                    <tr>
                                        <td align="right" style="width: 20px">
                                            <dxe:ASPxCheckBox ID="checkLaranja" runat="server" ClientInstanceName="checkLaranja"
                                                 Text=" " Checked="True">
                                            </dxe:ASPxCheckBox>
                                        </td>
                                        <td align="left">
                                            <img alt="" src="../../imagens/laranja.gif" /></td>
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
                    &nbsp;</td>                
                <td id="tdBotao" align="left" style="width: 80px;">
                    <dxe:ASPxButton ID="btnSelecionar" runat="server" 
                        Text="Selecionar">
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>
                </td>
                <td style="width: 5px; font-family: Arial">
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
       <iframe frameborder="0" name="graficos" scrolling="auto" src="<%=paginaIncialMetas %>"
                    width="100%" style="height: <%=alturaTela %>" id="frmVC"></iframe>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px">
    <table runat="server" ID="tbBotoes" cellpadding="0" cellspacing="0" style="width:100%" __designer:mapid="3e13"><tr runat="server" __designer:mapid="3e14"><td runat="server" style="padding: 3px" valign="top" __designer:mapid="3e15">
                    <table>
                        <tr>
                            <td align="center" style="width: 20px">
                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/FisicoLegenda.png">
                                </dxe:ASPxImage>
                            </td>
                            <td style="width: 43px">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                    Text="Físico">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="center" style="width: 21px">
                                <dxe:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/FinanceiroLegenda.png">
                                </dxe:ASPxImage>
                            </td>
                            <td style="width: 72px">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                    Text="Financeiro">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 25px">
                                <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/RiscoLegenda.png">
                                </dxe:ASPxImage>
                            </td>
                            <td style="width: 45px">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Risco">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 25px" align="center"><dxe:ASPxImage ID="ASPxImage4" runat="server" ImageUrl="~/imagens/QuestaoLegenda.png">
                            </dxe:ASPxImage>
                            </td>
                            <td style="width: 100px"><dxe:ASPxLabel ID="lblQuestao" runat="server" 
                                    
                                    Text="Questão">
                            </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </table>
            </td>
</tr>
</table>

                </td>
            </tr>
        </table>
        
    </div>      
    
</asp:Content>
