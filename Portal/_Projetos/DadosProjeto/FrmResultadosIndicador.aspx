<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmResultadosIndicador.aspx.cs" Inherits="_Projetos_DadosProjeto_FrmResultadosIndicador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 5px;
        }
        .style3
        {
            width: 85px;
        }
.dxgvControl,
.dxgvDisabled
{
	border: 1px Solid #9F9F9F;
	font: 12px Tahoma, Geneva, sans-serif;
	background-color: #F2F2F2;
	color: Black;
	cursor: default;
}
.dxgvTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxgvTable
{
	background-color: White;
	border-width: 0;
	border-collapse: separate!important;
	overflow: hidden;
	color: Black;
}
.dxgvHeader
{
	cursor: pointer;
	white-space: nowrap;
	padding: 4px 6px 5px;
	border: 1px Solid #9F9F9F;
	background-color: #DCDCDC;
	overflow: hidden;
	font-weight: normal;
	text-align: left;
}
.dxgvCommandColumn
{
	padding: 2px;
}
        .style4
        {
            height: 10px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                    <table style="WIDTH: 100%" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td class="style3">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                        Text="Ano:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Indicador:"  ID="ASPxLabel8">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <dxe:ASPxComboBox ID="ddlAno" runat="server" ClientInstanceName="ddlAno" 
                                         Width="75px">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvResultados.PerformCallback();
}" />
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="100%" 
                                        ClientInstanceName="txtIndicadorDado" ClientEnabled="False" 
                                         ID="txtIndicadorDado">
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
                <td class="style2">
                </td>
            </tr>
            <tr>
                <td>
                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvResultados" 
                        KeyFieldName="_CodigoMeta" AutoGenerateColumns="False" Width="100%" 
                         ID="gvResultados" 
                        OnCellEditorInitialize="gvResultados_CellEditorInitialize" 
                        OnHtmlDataCellPrepared="gvResultados_HtmlDataCellPrepared">
                        <Columns>
                            <dxwgv:GridViewDataTextColumn FieldName="Periodo" ReadOnly="True" ShowInCustomizationForm="True" Caption="Per&#237;odo" VisibleIndex="1">
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle BackColor="Transparent">
                                    </ReadOnlyStyle>
                                </PropertiesTextEdit>
                                <EditFormSettings Visible="False">
                                </EditFormSettings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Valor" ShowInCustomizationForm="True" Width="210px" Caption="Resultado" VisibleIndex="2">
                                <PropertiesTextEdit Width="150px">
                                </PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Editavel" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="_Mes" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                <EditFormSettings Visible="True">
                                </EditFormSettings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="_Ano" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False">
                        </SettingsBehavior>
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsEditing Mode="Inline">
                        </SettingsEditing>
                        <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" 
                            VerticalScrollableHeight="250">
                        </Settings>
                        <SettingsText EmptyHeaders="Resultados" EmptyDataRow="Nenhum Resultado para Atualizar no Indicador Selecionado.">
                        </SettingsText>
                    </dxwgv:ASPxGridView>
                    <dxcb:ASPxCallback ID="callbackSalvar" runat="server" 
                        ClientInstanceName="callbackSalvar" oncallback="callbackSalvar_Callback">
                    </dxcb:ASPxCallback>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Font-Bold="True" 
                        Font-Italic="True"  ForeColor="Red" 
                        Text="*Os dados serão salvos automaticamente ao serem digitados.">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" 
                         Text="Fechar" Width="90px">
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>
                            </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
