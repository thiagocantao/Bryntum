<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mt_001.aspx.cs" Inherits="_VisaoObjetivos_VisaoCorporativa_mt_001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../scripts/CDIS.js" language="javascript"></script>
    <script type="text/javascript" src="../../scripts/FusionCharts.js?v=1" language="javascript"></script>
    <script type="text/javascript" language="javascript">                
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
                            <dxwgv:GridViewDataTextColumn Caption="Iniciativa" FieldName="NomeProjeto" VisibleIndex="1"
                                Width="46%">
                                <PropertiesTextEdit DisplayFormatString="{0}">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                                <DataItemTemplate>
                                    <%# "<a href='../../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=" + Eval("CodigoProjeto") + "&NomeProjeto=" + Eval("NomeProjeto") + "' target='_top'>" + Eval("NomeProjeto") + "</a>"%>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Respons&#225;vel" FieldName="Responsavel"
                                VisibleIndex="2" Width="28%">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="%" FieldName="Concluido" VisibleIndex="3"
                                Width="11%">
                                <PropertiesTextEdit DisplayFormatString="{0:p0}" EncodeHtml="False">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" VisibleIndex="4"
                                Width="10%">
                                <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.gif' style='border-width:0px;' /&gt;">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="170" />
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </table>            
        </div>
    </form>
</body>
</html>
