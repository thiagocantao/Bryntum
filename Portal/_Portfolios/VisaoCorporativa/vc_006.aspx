<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vc_006.aspx.cs" Inherits="_Portfolios_VisaoCorporativa_vc_006" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: left">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="White"
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" HeaderText="Números da Instituição"
            Height="415px" Width="210px" ImageFolder="~/App_Themes/PlasticBlue/{0}/">
            <ContentPaddings Padding="1px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
            <HeaderStyle Font-Bold="False" Font-Italic="False"  BackColor="#EBEBEB">
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
            </HeaderStyle>
            <HeaderContent>
                <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/260627823/HeaderContent.png" Repeat="RepeatX"
                    VerticalPosition="bottom" HorizontalPosition="left" />
            </HeaderContent>
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <table>
                        <tr>
                            <td>
                                <table>
                                </table>
                                <table>
                                    <tr>
                                        <td align="left" style="height: 15px" valign="bottom">
                                            <span>
                                                <asp:Label ID="lblTituloProjetos" runat="server" Font-Bold="False"
                                                   >Projetos:</asp:Label>
                                            </span>
                                            <asp:Label ID="lblQtdProjetos" runat="server" Font-Bold="False"
                                               ></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 3px">
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: #c6c6c6 1px solid; color: white; border-bottom: #c6c6c6 1px solid;
                                height: 16px; background-color: #ebebeb" align="center">
                                <strong>
                                    <asp:Label ID="lblData" runat="server" Font-Bold="False" 
                                        ForeColor="Black"></asp:Label>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr  <%=styleDisplayDespesa %>>
                                        <td align="left" style="width: 114px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloCustoOrcadoData" runat="server" Font-Bold="False"
                                                    Text="Despesa Or&#231;ada:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblCustoOrcadoData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr  <%=styleDisplayDespesa %>>
                                        <td align="left" style="width: 114px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloCustoRealData" runat="server" Font-Bold="False"
                                                    Text="Despesa Realizada:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblCustoRealData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr  <%=styleDisplayDespesa %>>
                                        <td align="left" style="width: 114px; height: 15px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloPercentualCustoData" runat="server" Font-Bold="False"
                                                    Text="% Realizado:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right" style="height: 15px">
                                            <asp:Label ID="lblPercentualCustoData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 114px; height: 3px">
                                        </td>
                                        <td align="right" style="height: 3px">
                                        </td>
                                    </tr>
                                    <tr id="rec1"  <%=styleDisplayReceita %>>
                                        <td align="left" style="width: 114px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloReceitaOrcadaData" runat="server" Font-Bold="False"
                                                    Text="Receita Or&#231;ada:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblReceitaOrcadaData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="rec2"  <%=styleDisplayReceita %>>
                                        <td align="left" style="width: 114px; height: 14px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloReceitaRealData" runat="server" Font-Bold="False"
                                                    Text="Receita Realizada:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right" style="height: 14px">
                                            <asp:Label ID="lblReceitaRealData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="rec3"  <%=styleDisplayReceita %>>
                                        <td align="left" style="width: 114px; height: 15px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloPercentualRealReceitaData" runat="server" Font-Bold="False"
                                                     Text="% Realizado:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right" style="height: 15px">
                                            <asp:Label ID="lblPercentualReceitaData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 114px; height: 3px">
                                        </td>
                                        <td align="right" style="height: 3px">
                                        </td>
                                    </tr>
                                    <tr id="esforco01" <%=styleDisplayEsforco %>>
                                        <td align="left" style="width: 114px; height: 16px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloEsforcoPrevistoData" runat="server" Font-Bold="False"
                                                    Text="Esfor&#231;o Previsto:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right" style="height: 16px">
                                            <asp:Label ID="lblEsforcoPrevData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr4" <%=styleDisplayEsforco %>>
                                        <td align="left" style="width: 114px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloEsforcoRealData" runat="server" Font-Bold="False"
                                                    Text="Esfor&#231;o Realizado:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblEsforcoRealData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr5" <%=styleDisplayEsforco %>>
                                        <td align="left" style="width: 114px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloPercentualRealEsforcoData" runat="server" Font-Bold="False"
                                                     Text="% Realizado:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblPercentualEsforcoData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 3px">
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: #c6c6c6 1px solid; color: white; border-bottom: #c6c6c6 1px solid;
                                height: 16px; background-color: #ebebeb" align="center">
                                <strong><span>
                                    <asp:Label ID="lblTituloTotal" runat="server" Font-Bold="False"
                                        ForeColor="Black" Text="Total"></asp:Label>
                                </span></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr  <%=styleDisplayDespesa %>>
                                        <td align="left" style="width: 115px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloCustoOrcado" runat="server" Font-Bold="False"
                                                    Text="Despesa Or&#231;ada:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblCustoOrcadoTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr  <%=styleDisplayDespesa %>>
                                        <td align="left" style="width: 115px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloCustoReal" runat="server" Font-Bold="False"
                                                    Text="Despesa Realizada:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblCustoRealTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr  <%=styleDisplayDespesa %>>
                                        <td align="left" style="width: 115px; height: 15px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloPercentualRealCusto" runat="server" Font-Bold="False"
                                                    Text="% Realizado:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right" style="height: 15px">
                                            <asp:Label ID="lblPercentualCustoTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 115px; height: 3px">
                                        </td>
                                        <td align="right" style="height: 3px">
                                        </td>
                                    </tr>
                                    <tr id="rec4"  <%=styleDisplayReceita %>>
                                        <td align="left" style="width: 115px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloReceitaOrcada" runat="server" Font-Bold="False"
                                                    Text="Receita Or&#231;ada:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblReceitaOrcadaTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="rec5"  <%=styleDisplayReceita %>>
                                        <td align="left" style="width: 115px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloReceitaReal" runat="server" Font-Bold="False"
                                                    Text="Receita Realizada:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblReceitaRealTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="rec6"  <%=styleDisplayReceita %>>
                                        <td align="left" style="width: 115px; height: 15px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloPercentualRealReceita" runat="server" Font-Bold="False"
                                                    Text="% Realizado:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right" style="height: 15px">
                                            <asp:Label ID="lblPercentualReceitaTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 115px; height: 3px">
                                        </td>
                                        <td align="right" style="height: 3px">
                                        </td>
                                    </tr>
                                    <tr id="Tr1" <%=styleDisplayEsforco %>>
                                        <td align="left" style="width: 115px; height: 16px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloEsforcoPrevisto" runat="server" Font-Bold="False"
                                                    Text="Esfor&#231;o Previsto:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right" style="height: 16px">
                                            <asp:Label ID="lblEsforcoPrevTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr2" <%=styleDisplayEsforco %>>
                                        <td align="left" style="width: 115px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloEsforcoReal" runat="server" Font-Bold="False"
                                                    Text="Esfor&#231;o Realizado:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblEsforcoRealTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr3" <%=styleDisplayEsforco %>>
                                        <td align="left" style="width: 115px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloPercentualRealEsforco" runat="server" Font-Bold="False"
                                                    Text="% Realizado:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblPercentualEsforcoTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
            <HeaderTemplate>
                <table>
                    <tr>
                        <td>
                            Números do Portfólio</td>
                        <td align="right" style="width: 20px; cursor: pointer">
                            <dxe:ASPxImage ID="ASPxImage1" runat="server" CssClass="mao" ImageUrl="~/imagens/fecharNumeros.gif"
                                ToolTip="Fechar Números do Portfólio">
                                <ClientSideEvents Click="function(s, e) {
	parent.mostrarNumeros(false);
}" />
                            </dxe:ASPxImage>
                        </td>
                    </tr>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        &nbsp;</div>
    </form>
</body>
</html>
