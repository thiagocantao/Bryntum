<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="espacoTrabalho.aspx.cs" Inherits="espacoTrabalho_espacoTrabalho" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <script type="text/javascript" src="../scripts/jquery.js"></script>
    <script type="text/javascript" src="../scripts/interface.js"></script>
    <div style="height: <%=alturaTabela %>">
        <table>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 126px; height: 5px">
                            </td>
                            <td style="width: 10px; height: 5px">  
                            </td>
                            <td valign="top">
                            </td>
                            <td style="width: 10px; height: 5px" valign="top">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 126px" valign="top">
                                <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="#E0E0E0" CssFilePath="~/App_Themes/GreenLeaf/{0}/styles.css"
                                    CssPostfix="GreenLeaf" HorizontalAlign="Center" ShowHeader="False" View="GroupBox"
                                    Width="100px">
                                    <ContentPaddings Padding="0px" />
                                    <Border BorderStyle="Solid" BorderWidth="2px" />
                                    <HeaderStyle>
                                        <Border BorderColor="#94C43A" BorderStyle="Solid" BorderWidth="1px" />
                                    </HeaderStyle>
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                <table>
                                    <tr>
                                        <td style="width: 100px; height: 61px; text-align: center">
                                            <a href="avisos.aspx"><img height="48" src="../imagens/principal/advertisment-48.png" style="cursor: pointer"
                                                width="48" /></a>&nbsp;<br />
                                            <span style=" color: dimgray;">Avisos</span></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px; text-align: center; height: 63px;">
                                            <img height="48" src="../imagens/principal/Biblioteca2.png" style="cursor: pointer"
                                                width="48" />&nbsp;<br />
                                            <span style=" color: dimgray;">Bibliotecas</span></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px; text-align: center">
                                            <img height="48" src="../imagens/principal/96px-Nuvola_apps_edu_miscellaneous_svg.png" style="cursor: pointer" width="48" />&nbsp;<br />
                                            <span style=" color: dimgray;">Lições aprendidas</span></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px; text-align: center">
                                            <img height="48" src="../imagens/principal/Configuração.png" style="cursor: pointer"
                                                width="48" />&nbsp;<br />
                                            <span style=" color: dimgray;">Configuracoes</span></td>
                                    </tr>
                                </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxrp:ASPxRoundPanel>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True"
                                                Font-Size="12pt" ForeColor="Gray" Text="Agenda">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Size="10pt"
                                                ForeColor="Gray" Text="Minhas Próximas Reuniões (30 dias)">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False"
                                                Width="100%">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="Reuni&#227;o" VisibleIndex="0" Width="70%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Data" VisibleIndex="1" Width="30%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                            </dxwgv:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Size="10pt"
                                                ForeColor="Gray" Text="Entrada (preciso me posicionar)">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxwgv:ASPxGridView ID="gridEntrada" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridEntrada"
                                                Width="100%">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="Assunto" VisibleIndex="0" Width="20%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Descri&#231;&#227;o" VisibleIndex="1" Width="50%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Desde" VisibleIndex="2" Width="15%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Prazo" VisibleIndex="3" Width="15%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <Border BorderColor="#E0E0E0" />
                                            </dxwgv:ASPxGridView>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Font-Size="10pt"
                                                ForeColor="Gray" Text="Entrada (preciso me posicionar)">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxwgv:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False"
                                                Width="100%">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="Assunto" VisibleIndex="0" Width="20%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Descri&#231;&#227;o" VisibleIndex="1" Width="50%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Desde" VisibleIndex="2" Width="15%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Prazo" VisibleIndex="3" Width="15%">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                            </dxwgv:ASPxGridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" valign="bottom">
                <!--bottom dock -->
<div class="dock" id="dock2"><hr />
  <div class="dock-container2">
  <a class="dock-item2" href="../selecionaOpcao.aspx?op=po"><span>Portfólios</span><img src="../imagens/principal/portfolio.png" alt="portfolios" /></a> 
  <a class="dock-item2" href="../selecionaOpcao.aspx?op=pr"><span>Projetos</span><img src="../imagens/principal/projetos2.png" alt="projectos" /></a> 
  <a class="dock-item2" href="../selecionaOpcao.aspx?op=es"><span>Estratégias</span><img src="../imagens/principal/estrategia.png" alt="estrategias" /></a> 
  <a class="dock-item2" href="../selecionaOpcao.aspx?op=or"><span>Orçamentos</span><img src="../imagens/principal/orcamento2.png" alt="orçamentos" /></a> 
  <a class="dock-item2" href="../selecionaOpcao.aspx?op=ad"><span>Administração</span><img src="../imagens/principal/administracao.png" alt="administração" /></a> 
  <a class="dock-item2" href="../selecionaOpcao.aspx?op=aj"><span>Ajudas</span><img src="../imagens/principal/ajuda.png" alt="Ajudas" /></a> 
  </div>
</div>
 
<!--dock menu JS options -->
<script type="text/javascript"> 
	
	$(document).ready(
		function()
		{
			$('#dock2').Fisheye(
				{
					maxWidth: 60,
					items: 'a',
					itemsText: 'span',
					container: '.dock-container2',
					itemWidth: 40,
					proximity: 80,
					alignment : 'left',
					valign: 'bottom',
					halign : 'center'
				}
			)
		}
	);
 
</script>
                </td>
            </tr>
        </table>
    
    </div>
</asp:Content>