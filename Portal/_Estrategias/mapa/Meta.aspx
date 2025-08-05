<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Meta.aspx.cs" Inherits="_Estrategias_mapa_Macrometa" %>

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
<body style="overflow-x:hidden; margin:0px; background-color: #FFFFFF;">
    <form id="form1" runat="server">    
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td valign="top" width="50%">
                                <table cellpadding="0" cellspacing="0" class="style1">
                                    <tr>
                                        <td style="background-color: #F2F2F2; padding: 10px">
                                            <span id="spanDetalhes" runat="server"></span></td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #F2F2F2; padding-right: 5px">
                                            <div id="chartdiv" align="center">
                    </div>

                    <script type="text/javascript">
                        getGrafico('<%=grafico_swf %>', "grafico001", '100%', '<%=alturaGrafico %>', '<%=grafico_xml %>', 'chartdiv');
                    </script></td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #F2F2F2; padding-right: 5px;" 
                                            align="right">
                                            <dxe:ASPxLabel ID="lblFonte" runat="server" Font-Italic="True">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #F2F2F2; padding-right: 5px;<%=escondeTdComentario %>" 
                                            align="left">
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Comentários:">
                    </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="<%=escondeTdComentario %>" 
                                            align="left">
                   <div style="padding:2px; border: 1px solid #999999; overflow:hidden; width:100%; min-height:130px;" 
                        id="spanComentarios" runat="server"></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="padding-left: 5px">                            
                               <dxwgv:ASPxGridView ID="gvProjetos" runat="server" AutoGenerateColumns="False" 
                                     Width="100%">
                                    <Columns>
<dxwgv:GridViewDataTextColumn FieldName="IndicaProjetoCarteiraPrioritaria" Caption=" " 
                                            VisibleIndex="1" Width="35px">
    <DataItemTemplate>
                                                <%# Eval("IndicaProjetoCarteiraPrioritaria").ToString() == "S" ? "<img src='../../imagens/estrela.png' />" : "&nbsp;" %>
                                            
</DataItemTemplate>
    <CellStyle HorizontalAlign="Center">
    </CellStyle>
</dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" 
                                            VisibleIndex="2">
                                            <DataItemTemplate>
                                               <%# getDescricao() %>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>                                                                             
                                    </Columns>
                                    <SettingsBehavior AutoExpandAllGroups="True" />
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <Settings 
                                        VerticalScrollBarMode="Auto" ShowFooter="True" 
                                        VerticalScrollableHeight="404" />
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
                                    <Border BorderStyle="None" />
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                                                        <span id="spanStatus" runat="server"></span>
                </td>
            </tr>            
            <tr>
                <td align="center">
                    <span id="spanAnos" style="width:100%" runat="server"></span></td>
            </tr>
        </table>
    </form>
</body>
</html>
