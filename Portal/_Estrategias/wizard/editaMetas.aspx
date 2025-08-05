<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editaMetas.aspx.cs" Inherits="_Estrategias_wizard_editaMetas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            height: 5px;
        }
        .style2
        {
            width: 75px;
        }
        .style3
        {
            width: 150px;
        }
    </style>
</head>
<body style="margin:0px" onload="inicializaVariaveis();">
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
    gvMetas.PerformCallback('Atualizar');
}" SelectedIndexChanged="function(s, e) {
	hfGeral.Set(&quot;CodigoUnidade&quot;, s.GetValue());
    gvMetas.PerformCallback('Atualizar');
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
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                            Text="Indicador:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td class="style2">
                                        <dxe:ASPxLabel ID="ASPxLabelAno" runat="server" 
                                            Text="Ano:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td class="style3">
                                                                                                                                    <dxe:ASPxLabel runat="server" Text="Valor Atual:" 
                                                            ID="lblTituloMeta"></dxe:ASPxLabel>




                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 10px">
                                        <dxe:ASPxTextBox ID="txtIndicadorDado" runat="server" ClientEnabled="False" ClientInstanceName="txtIndicadorDado"
                                             Width="100%">
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td class="style2" style="padding-right: 10px">
                                        <dxe:ASPxComboBox ID="ddlAnos" runat="server" ClientInstanceName="ddlAnos"
                                            ValueType="System.String" Width="100%">
                                            <ClientSideEvents EndCallback="function(s, e) {
	hfGeral.Set(&quot;Ano&quot;, s.cp_Ano);
    gvMetas.PerformCallback('Atualizar');
}" SelectedIndexChanged="function(s, e) {	
	hfGeral.Set(&quot;Ano&quot;, s.GetValue());
	gvMetas.PerformCallback('Atualizar');
}" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td class="style3">
                                                                                                                                                <dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right" 
                                                            ClientInstanceName="txtMetaInformada"  
                                                            ID="txtMetaInformada" ClientEnabled="False">

<ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
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
                        <dxwgv:ASPxGridView ID="gvMetas" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvMetas"
                             KeyFieldName="_CodigoIndicador" OnCellEditorInitialize="gvMetas_CellEditorInitialize"
                            OnCommandButtonInitialize="gvMetas_CommandButtonInitialize" OnRowUpdating="gvMetas_RowUpdating"
                            Width="100%">
                            <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />

<SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False"></SettingsBehavior>

                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <SettingsText EmptyDataRow="Nenhum Resultado para Atualizar no Indicador Selecionado."
                                EmptyHeaders="Resultados" />
                            <Columns>
                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="35px" ShowEditButton="true">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Per&#237;odo" FieldName="Periodo" ReadOnly="True"
                                    VisibleIndex="1" Width="345px">
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle BackColor="Transparent">
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="False" />

<EditFormSettings Visible="False"></EditFormSettings>
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
<EditFormSettings Visible="True"></EditFormSettings>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="_Ano" Visible="False" VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <Settings
                                VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" 
                                VerticalScrollableHeight="175" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<SettingsText EmptyHeaders="Resultados" 
                                EmptyDataRow="Nenhum Resultado para Atualizar no Indicador Selecionado."></SettingsText>
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
