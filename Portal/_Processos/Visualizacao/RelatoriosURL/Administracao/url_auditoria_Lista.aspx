<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_auditoria_Lista.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Administracao_url_auditoria_Lista"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style2
            {
                width: 10px;
            }
            .style3
            {
                width: 10px;
                height: 10px;
            }
            .style4
            {
                height: 10px;
            }
        </style>

        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
            width: 100%">
 
        </table>
        <table cellspacing="1" class="style1">
            <tr>
                <td class="style3">
                </td>
                <td class="style4">
                </td>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                        Width="100%" KeyFieldName="ID" OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                        <Columns>
                            <dxwgv:GridViewDataTextColumn FieldName="ID" VisibleIndex="0" Width="40px" Caption=" ">
                                <Settings AllowAutoFilter="False" />
                                <DataItemTemplate>
                                    <%# getBotaoVisualizar()%>
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataDateColumn Caption="Data" FieldName="DATA_OPERACAO" VisibleIndex="1"
                                Width="110px">
                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                </PropertiesDateEdit>
                                <Settings AllowHeaderFilter="False" AutoFilterCondition="Equals" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TABELA" VisibleIndex="2" Caption="Nome da Tabela"
                                Width="200px">
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TipoOperacao" VisibleIndex="3" Caption="Operação Realizada"
                                Width="140px">
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="USUARIO" VisibleIndex="4">
                                <Settings AutoFilterCondition="Contains" />
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager PageSize="200">
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" />
                    </dxwgv:ASPxGridView>
                </td>
                <td class="style2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
