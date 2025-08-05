<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalhesAnoMacrometa.aspx.cs" Inherits="_Estrategias_mapa_DetalhesAnoMacrometa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                                <dxwgv:ASPxGridView ID="gvProjetos" runat="server" AutoGenerateColumns="False" 
                                     Width="100%">
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Caption=" " Width="35px" 
                                            FieldName="IndicaProjetoCarteiraPrioritaria">
                                            <DataItemTemplate>
                                                <%# Eval("IndicaProjetoCarteiraPrioritaria").ToString() == "S" ? "<img src='../../imagens/estrela.png' />" : "&nbsp;" %>
                                            </DataItemTemplate>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" 
                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                            <DataItemTemplate>
                                                <%# getDescricao() %>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Concluído" VisibleIndex="2" 
                                            Width="75px" FieldName="PercentualConcluido" Visible="False">
                                            <PropertiesTextEdit DisplayFormatString="{0:n0}%">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Responsável" VisibleIndex="3" 
                                            Width="190px" FieldName="NomeResponsavel">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption=" " VisibleIndex="4" Width="45px" 
                                            FieldName="CorDesempenho" Visible="False">
                                            <DataItemTemplate>
                                                <%# Eval("CorDesempenho").ToString() != "" ? ("<img src='../../imagens/" + Eval("CorDesempenho").ToString().Trim() + ".gif' />") : "&nbsp;"%>
                                            </DataItemTemplate>                                            
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <Settings 
                                        VerticalScrollBarMode="Visible" ShowFooter="True" 
                                        VerticalScrollableHeight="135" />
                                    <SettingsText Title="Projetos em Execução" />
                                    <Templates>
                                        <FooterRow>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/estrela.png">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td style="padding-left: 4px">
                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" 
                                                            Text="Projetos da Carteira Prioritária">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterRow>
                                    </Templates>
                                </dxwgv:ASPxGridView>
                            </td>
            </tr>
            <tr>
                <td >
                     <span id="spanAnalises" runat="server"></span>
                </td>
            </tr>            
        </table>
    
    </div>
    </form>
</body>
</html>
