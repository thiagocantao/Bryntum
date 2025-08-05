<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CenariosRecursos.aspx.cs"
    Inherits="_Estrategias_wizard_editaResultados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            height: 5px;
        }
    </style>
</head>
<body onunload="/*fechaEdicao();*/" style="margin: 0px">
    <form id="form1" runat="server">
    <div style="padding: 5px; padding-bottom: 0px">
        <table>
            <tbody>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="gvResultados" runat="server" AutoGenerateColumns="False"
                            ClientInstanceName="gvResultados"  KeyFieldName="CodigoConta" Width="100%" OnBatchUpdate="gvResultados_BatchUpdate" OnHtmlDataCellPrepared="gvResultados_HtmlDataCellPrepared">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="Per&#237;odo" FieldName="Periodo" ReadOnly="True"
                                    VisibleIndex="1">
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle BackColor="Transparent">
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="False" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Resultado" FieldName="Valor" VisibleIndex="2"
                                    Width="210px">
                                    <PropertiesTextEdit Width="150px">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Editavel" Visible="False" VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="_Mes" Visible="False" VisibleIndex="4">
                                    <EditFormSettings Visible="True" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="_Ano" Visible="False" VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                            <ClientSideEvents BatchEditStartEditing="function(s, e) {
	if(hfGeral.Get(e.focusedColumn.fieldName + '_' + e.visibleIndex) == 'N')
		e.cancel = true;
}
" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsEditing Mode="Batch">
                            </SettingsEditing>
                            <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                            <SettingsText EmptyDataRow="Nenhum Resultado para Atualizar no Indicador Selecionado."
                                EmptyHeaders="Resultados" ConfirmOnLosingBatchChanges="Existem dados que foram alterados e não salvos. Deseja sair da atualização de resultados?" />
                            <Styles>
                                <BatchEditModifiedCell BackColor="#CCFFCC" ForeColor="#666666">
                                </BatchEditModifiedCell>
                            </Styles>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 3px">
                        <script language="javascript" type="text/javascript">
                            gvResultados.SetHeight(window.innerHeight - 20);
                        </script>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
