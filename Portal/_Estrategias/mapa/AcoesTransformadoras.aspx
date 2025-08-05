<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AcoesTransformadoras.aspx.cs" Inherits="_Estrategias_mapa_Macrometa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body style="overflow-x:hidden; margin:0px">
    <form id="form1" runat="server">    
        <dxwgv:ASPxGridView ID="gvProjetos" runat="server" AutoGenerateColumns="False" 
                                     Width="100%">
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Caption="Ação Transformadora" 
                                            FieldName="AcaoTransformadora" GroupIndex="0" SortIndex="0" 
                                            SortOrder="Ascending">
                                        </dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="IndicaProjetoCarteiraPrioritaria" ShowInCustomizationForm="True" Caption=" " 
                                            VisibleIndex="1" Width="35px">
    <DataItemTemplate>
                                                <%# Eval("IndicaProjetoCarteiraPrioritaria").ToString() == "S" ? "<img src='../../imagens/estrela.png' />" : "&nbsp;" %>
                                            
</DataItemTemplate>
    <CellStyle HorizontalAlign="Center">
    </CellStyle>
</dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" 
                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                            <DataItemTemplate>
                                                <table>
                                                <tr>
                                                    <td title='<%# string.Format("Concluído: {0:n0}%", Eval("PercentualConcluido")) %>'><%#Eval("CorDesempenho").ToString() != "" ? (string.Format("<img src='../../imagens/{0}.gif' alt='Concluído: {1:n0}%' />", Eval("CorDesempenho").ToString().Trim()
                                                                                                                    , Eval("PercentualConcluido"))) : "&nbsp;"%></td>
                                                    <td><%# Eval("PodeAcessarProjeto").ToString() == "S" ? ("<a class='LinkGrid' target='_top' href='../../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=" + Eval("CodigoProjeto") + "&NomeProjeto=" + Eval("NomeProjeto") + "'>" + Eval("NomeProjeto") + "</a>") : Eval("NomeProjeto").ToString()%></td>
                                                </tr></table>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>                                                                             
                                        <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="NomeResponsavel" 
                                            VisibleIndex="3" Width="200px">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AutoExpandAllGroups="True" />
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <Settings 
                                        VerticalScrollBarMode="Auto" ShowFooter="True" 
                                        VerticalScrollableHeight="250" ShowTitlePanel="True" />
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
                                    <SettingsText Title="Projetos em Execução" />
                                </dxwgv:ASPxGridView>
    </form>
</body>
</html>
