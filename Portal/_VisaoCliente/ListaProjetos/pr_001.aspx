<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pr_001.aspx.cs" Inherits="_VisaoCliente_ListaProjetos_pr_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" language="javascript">
        function renderizaGrafico()
        {
            vchart.render(document.getElementById('chartdiv'));
        }
        
        function abreAnalise(ano, mes, codigoIndicador)
        {
            parent.callback.PerformCallback(ano + ';' + mes + ';' + codigoIndicador);
        }
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        <table>            
            <tr>
                <td>
                    <dxwgv:ASPxGridView ID="gridProjetos" runat="server" AutoGenerateColumns="False"
                        ClientInstanceName="gridProjetos"  KeyFieldName="CodigoProjeto" Width="100%">
                        <Styles>
                            <FocusedRow BackColor="Transparent">
                            </FocusedRow>
                        </Styles>
                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                        </SettingsPager>
                        <SettingsText ConfirmDelete="Deseja Realmente Excluir a Associa&#231;&#227;o?"  />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="5%" Caption=" " FieldName="Status">
                                <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.gif' /&gt;">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Marco" FieldName="NomeTarefa" VisibleIndex="1"
                                Width="50%">
                                <PropertiesTextEdit DisplayFormatString="{0}">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Previsto" FieldName="TerminoPrevisto" VisibleIndex="2"
                                Width="11%">
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tend&#234;ncia" FieldName="Termino" VisibleIndex="3"
                                Width="11%">
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy" EncodeHtml="False">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Realizado" FieldName="TerminoReal" VisibleIndex="4"
                                Width="11%">
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="210" ShowFooter="True" />
                        <Templates>
                            <FooterRow>
                                <table>
                                    <tr>
                                        <td align="left" style="width: 25px">
                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/VerdeOK.gif">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td align="left" style="width: 125px">
                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                Text="Concluído no Prazo">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" style="width: 25px">
                                            <dxe:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/VermelhoOK.gif">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td align="left" style="width: 140px">
                                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                Text="Concluído com Atraso">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" style="width: 25px">
                                            <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/Verde.gif">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td align="left" style="width: 127px">
                                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                                Text="No Prazo/Adiantado">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" style="width: 25px">
                                            <dxe:ASPxImage ID="ASPxImage4" runat="server" ImageUrl="~/imagens/Amarelo.gif">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td align="left" style="width: 131px">
                                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                                Text="Tendência de Atraso">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" style="width: 25px">
                                            <dxe:ASPxImage ID="ASPxImage5" runat="server" ImageUrl="~/imagens/Vermelho.gif">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td align="left">
                                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                                Text="Atrasado">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </FooterRow>
                        </Templates>
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </table>      
        </div>
    </form>
</body>
</html>
