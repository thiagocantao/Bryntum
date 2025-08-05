<%@ Page Language="C#" AutoEventWireup="true" CodeFile="numeros_001.aspx.cs" Inherits="_Projetos_VisaoCorporativa_numeros_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <link href="../../estilos/custom_frame.css" rel="stylesheet" />
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <style>
      html div#ASPxRoundPanel1_CRC > table td, div#ASPxRoundPanel1_CRC > table td a, div #ASPxRoundPanel1_CRC > table td span{
              font-size: 10px !important;
      }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: left">
        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" BackColor="White"
            HeaderText="Números da Instituição"
            Height="400px" Width="100%">
            <ContentPaddings Padding="1px" />
            <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
 <%--           <HeaderStyle Font-Bold="False" Font-Italic="False"  BackColor="#EBEBEB">
                <BorderLeft BorderStyle="None" />
                <BorderRight BorderStyle="None" />
                <BorderBottom BorderStyle="None" />
                <Paddings Padding="1px" />
            </HeaderStyle>--%>
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
                                    <tr>
                                        <td align="left" style="height: 0px" valign="bottom">
                                            <span>
                                                <asp:Label ID="lblTituloProjetos" runat="server" Font-Bold="False"
                                                    Font-Underline="True" ForeColor="#5D7B9D">Projetos:</asp:Label>
                                            </span>
                                            <asp:Label ID="lblQtdProjetos" runat="server" Font-Bold="False"
                                                Font-Underline="True" ForeColor="#5D7B9D"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td style="border-top: #c6c6c6 1px solid; color: white; width: 460px; border-bottom: #c6c6c6 1px solid;
                                height: 0px; background-color: #ebebeb" align="center">
                                <strong>
                                    <asp:Label ID="lblData" runat="server" Font-Bold="False" 
                                        ForeColor="Black"></asp:Label>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td align="left" style="width: 150px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloCustoOrcadoData" runat="server" Font-Bold="False"
                                                    Text="Despesa Or&#231;ada:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblCustoOrcadoData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
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
                                    <tr>
                                        <td align="left" style="width: 114px; height: 0px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloPercentualCustoData" runat="server" Font-Bold="False"
                                                    Text="% Realizado:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right" style="height: 0px">
                                            <asp:Label ID="lblPercentualCustoData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>

                                    <tr id="rec1" <%=styleDisplayReceita %>>
                                        <td align="left" style="width: 114px" >
                                            <strong><span>
                                                <asp:Label ID="lblTituloReceitaOrcadaData" runat="server" Font-Bold="False"
                                                    Text="Receita Or&#231;ada:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblReceitaOrcadaData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="rec2" <%=styleDisplayReceita %>>
                                        <td align="left" style="width: 114px; height: 0px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloReceitaRealData" runat="server" Font-Bold="False"
                                                    Text="Receita Realizada:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right" style="height: 0px">
                                            <asp:Label ID="lblReceitaRealData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="rec3" <%=styleDisplayReceita %>>
                                        <td align="left" style="width: 114px; height: 0px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloPercentualRealReceitaData" runat="server" Font-Bold="False"
                                                     Text="% Realizado:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right" style="height: 0px">
                                            <asp:Label ID="lblPercentualReceitaData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>

                                    <tr id="esforco01">
                                        <td align="left" style="width: 114px; height: 0px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloEsforcoPrevistoData" runat="server" Font-Bold="False"
                                                    Text="Esfor&#231;o Previsto:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right" style="height: 0px">
                                            <asp:Label ID="lblEsforcoPrevData" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="esforco02">
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
                                    <tr id="esforco03">
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
                            <td style="border-top: #c6c6c6 1px solid; color: white; border-bottom: #c6c6c6 1px solid;
                                height: 0px; background-color: #ebebeb" align="center">
                                <strong><span>
                                    <asp:Label ID="lblTituloTotal" runat="server" Font-Bold="False"
                                        ForeColor="Black" Text="Total"></asp:Label>
                                </span></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td align="left" style="width: 150px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloCustoOrcado" runat="server" Font-Bold="False"
                                                    Text="Despesa Or&#231;ada:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblCustoOrcadoTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
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
                                    <tr>
                                        <td align="left" style="width: 115px; height: 0px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloPercentualRealCusto" runat="server" Font-Bold="False"
                                                    Text="% Realizado:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right" style="height: 0px">
                                            <asp:Label ID="lblPercentualCustoTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>

                                    <tr id="rec4" <%=styleDisplayReceita %>>
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
                                    <tr id="rec5" <%=styleDisplayReceita %>>
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
                                    <tr id="rec6" <%=styleDisplayReceita %>>
                                        <td align="left" style="width: 115px; height: 10px">
                                            <strong><span>
                                                <asp:Label ID="lblTituloPercentualRealReceita" runat="server" Font-Bold="False"
                                                    Text="% Realizado:"></asp:Label>
                                            </span></strong>
                                        </td>
                                        <td align="right" style="height: 0px">
                                            <asp:Label ID="lblPercentualReceitaTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>

                                    <tr id="esforco04">
                                        <td align="left" style="width: 115px; height: 0px">
                                            <span><strong>
                                                <asp:Label ID="lblTituloEsforcoPrevisto" runat="server" Font-Bold="False"
                                                    Text="Esfor&#231;o Previsto:"></asp:Label>
                                            </strong></span>
                                        </td>
                                        <td align="right" style="height: 0px">
                                            <asp:Label ID="lblEsforcoPrevTotal" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="esforco05">
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
                                    <tr id="esforco06">
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
                            Números da Instituição</td>
                        <td align="right" style="width: 20px; cursor: pointer">
                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/fecharNumeros.gif" CssClass="mao" ToolTip="Fechar Números da Instituição">
                                <ClientSideEvents Click="function(s, e) {
	parent.mostrarNumeros(false);
}" />
                            </dxe:ASPxImage>
                        </td>
                    </tr>
                </table>
            </HeaderTemplate>
        </dxrp:ASPxRoundPanel>
        </div>
    </form>
</body>
</html>
