<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editaResultados.aspx.cs" Inherits="_Estrategias_wizard_editaResultados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tbody>
                <tr>
                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabelAno0" runat="server" 
                                            Text="Unidade:">
                                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                                        <dxe:ASPxComboBox ID="ddlUnidades" runat="server" 
                                            ClientInstanceName="ddlUnidades"
                                            ValueType="System.Int32" Width="100%">
                                            <ClientSideEvents EndCallback="function(s, e) {
	hfGeral.Set(&quot;CodigoUnidade&quot;, s.cp_CodigoUnidade);
    gvResultados.PerformCallback('Atualizar');
}" SelectedIndexChanged="function(s, e) {
	hfGeral.Set(&quot;CodigoUnidade&quot;, s.GetValue());
    gvResultados.PerformCallback('Atualizar');
}" />
                                        </dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 66px">
                                        <dxe:ASPxLabel ID="ASPxLabelAno" runat="server" 
                                            Text="Ano:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                            Text="Indicador:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 66px">
                                        <dxe:ASPxComboBox ID="ddlAnos" runat="server" ClientInstanceName="ddlAnos"
                                            Width="100%">
                                            <ClientSideEvents EndCallback="function(s, e) {	
	gvResultados.PerformCallback();
}" SelectedIndexChanged="function(s, e) {
	gvResultados.PerformCallback();	
}" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txtIndicadorDado" runat="server" ClientEnabled="False" ClientInstanceName="txtIndicadorDado"
                                             Width="100%">
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="gvResultados" runat="server" AutoGenerateColumns="False"
                            ClientInstanceName="gvResultados"  KeyFieldName="_CodigoDado"
                            OnCellEditorInitialize="gvResultados_CellEditorInitialize"
                            OnCommandButtonInitialize="gvResultados_CommandButtonInitialize" OnHtmlDataCellPrepared="gvResultados_HtmlDataCellPrepared"
                            OnRowUpdating="gvResultados_RowUpdating" OnStartRowEditing="gvResultados_StartRowEditing"
                            Width="100%">
                            <ClientSideEvents BeginCallback="function(s, e) {
	if(gvResultados.IsEditing())
		ddlAnos.SetEnabled(false);
	else
		ddlAnos.SetEnabled(true);
}" EndCallback="function(s, e) {
	if(gvResultados.IsEditing())
		ddlAnos.SetEnabled(false);
	else
		ddlAnos.SetEnabled(true);
}" />
                            <Columns>
                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="35px" ShowEditButton="true">
                                </dxwgv:GridViewCommandColumn>
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
                                <dxwgv:GridViewDataTextColumn FieldName="_Mes" Visible="False" VisibleIndex="3">
                                    <EditFormSettings Visible="True" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="_Ano" Visible="False" VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings 
                                VerticalScrollableHeight="165" VerticalScrollBarMode="Auto" 
                                HorizontalScrollBarMode="Auto" />
                            <SettingsText EmptyDataRow="Nenhum Resultado para Atualizar no Indicador Selecionado."
                                EmptyHeaders="Resultados" />
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 3px">
                        <dxe:ASPxLabel ID="lblPeriodoVigencia" runat="server" Font-Bold="True" 
                            Font-Italic="True" >
                        </dxe:ASPxLabel>
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
