<%@ Page Language="C#" AutoEventWireup="true" CodeFile="relResumoMonitoramentoSMD.aspx.cs" Inherits="_Estrategias_Relatorios_relResumoMonitoramentoSMD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
                        <tr>
            <td align="right">

                    <table cellpadding="0" cellspacing="0" >
                        <tr>
                            <td align="right" style="width: 30px; padding-right: 5px;">
                                <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Ano:">
                                </dxcp:ASPxLabel>
                            </td>
                            <td style="width: 80px; padding-right: 5px">
                                <dxcp:ASPxSpinEdit ID="spAno" runat="server" ClientInstanceName="spAno" 
                                     MaxLength="4" Number="0" 
                                    NumberType="Integer" Width="100%">
                                    <SpinButtons ClientVisible="False" Width="100%">
                                    </SpinButtons>
                                    <Paddings Padding="0px" />
                                </dxcp:ASPxSpinEdit>
                            </td>
                            <td style="width: 70px">
                                <dxcp:ASPxButton ID="btnOK" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnOK"  Text="Selecionar" 
                                    Width="100%">
                                    <ClientSideEvents Click="function(s, e) {
               if(spAno.GetValue() == null)
              {
                        window.top.mostraMensagem('Ano inválido', 'atencao', true, false, null);
               }
               else
               {	 
                       pgDados.PerformCallback(spAno.GetValue());
               }
}" />
                                    <Paddings Padding="0px" />
                                </dxcp:ASPxButton>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>

            </td>
            </tr>
            <tr>
            <td>

                                <table runat="server" ID="tbBotoes" cellpadding="0" 
                    cellspacing="0" style="width:100%"><tr id="Tr1" runat="server"><td id="Td1" runat="server" style="padding: 3px" 
                                            valign="top">
                                            <table cellpadding="0" cellspacing="0" style="height: 22px">
                                                <tr>
                                                    <td>
                                                        <dxcp:ASPxMenu runat="server" ClientInstanceName="menu" ItemSpacing="5px" 
                                                            BackColor="Transparent" ID="menu" OnInit="menu_Init" 
                                                            OnItemClick="menu_ItemClick">
<Paddings Padding="0px"></Paddings>
<Items>
<dxcp:MenuItem Name="btnExportar" Text="" ToolTip="Exportar"><Items>
<dxcp:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
<Image Url="~/imagens/menuExportacao/xls.png"></Image>
</dxcp:MenuItem>
<dxcp:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
<Image Url="~/imagens/menuExportacao/pdf.png"></Image>
</dxcp:MenuItem>
<dxcp:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
<Image Url="~/imagens/menuExportacao/rtf.png"></Image>
</dxcp:MenuItem>
<dxcp:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
<Image Url="~/imagens/menuExportacao/html.png"></Image>
</dxcp:MenuItem>
</Items>

<Image Url="~/imagens/botoes/btnDownload.png"></Image>
</dxcp:MenuItem>
<dxcp:MenuItem ClientVisible="False" Name="btnLayout" Text="" ToolTip="Layout">
    <Items>
<dxcp:MenuItem Text="Salvar" ToolTip="Salvar Layout">
<Image IconID="save_save_16x16"></Image>
</dxcp:MenuItem>
<dxcp:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
<Image IconID="actions_reset_16x16"></Image>
</dxcp:MenuItem>
</Items>

<Image Url="~/imagens/botoes/layout.png"></Image>
</dxcp:MenuItem>
</Items>

<ItemStyle Cursor="pointer">
<HoverStyle>
<Border BorderStyle="None"></Border>
</HoverStyle>

<Paddings Padding="0px"></Paddings>
</ItemStyle>

<SubMenuItemStyle Cursor="pointer" BackColor="White">
<SelectedStyle>
<Border BorderStyle="None"></Border>
</SelectedStyle>
</SubMenuItemStyle>

<Border BorderStyle="None"></Border>
</dxcp:ASPxMenu>

                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
</tr>
</table>

            </td>
            </tr>
            <tr>
                <td>
<dxwpg:ASPxPivotGrid ID="pgDados" runat="server" ClientIDMode="AutoID" 
                        ClientInstanceName="pgDados"  
                        oncustomcallback="pgDados_CustomCallback" Width="100%" 
                        oncustomcelldisplaytext="pgDados_CustomCellDisplayText" Height="200px" 
                        oncustomcellstyle="pgDados_CustomCellStyle" 
                        oncustomcellvalue="pgDados_CustomCellValue" 
                        onhtmlfieldvalueprepared="pgDados_HtmlFieldValuePrepared">
                        <Fields>
   <dxpgwx:PivotGridField ID="fieldNomeIndicador" Area="RowArea" AreaIndex="0" 
                                FieldName="NomeIndicador" AllowedAreas="RowArea, FilterArea" 
                                Caption="Indicador">
                            </dxpgwx:PivotGridField>
                            <dxpgwx:PivotGridField ID="fieldGrupoProduto" Area="RowArea" AreaIndex="1" 
                                Caption="Grupo de Produto" FieldName="GrupoProduto" 
                                AllowedAreas="RowArea, FilterArea">
                            </dxpgwx:PivotGridField>
                            <dxpgwx:PivotGridField ID="fieldqtdPrevistoAno0" Area="DataArea" AreaIndex="3" 
                                FieldName="qtdPrevistoAno0" AllowedAreas="DataArea" 
                                CellFormat-FormatString="{0:n0}" CellFormat-FormatType="Custom">
                                <HeaderStyle BackColor="#FFFF99" />
                            </dxpgwx:PivotGridField>
                            <dxpgwx:PivotGridField ID="fieldqtdRealAno0" Area="DataArea" AreaIndex="5" 
                                FieldName="qtdRealAno0" AllowedAreas="DataArea" 
                                CellFormat-FormatString="{0:n0}" CellFormat-FormatType="Custom">
                            </dxpgwx:PivotGridField>
                            <dxpgwx:PivotGridField ID="fieldqtdRealAno1" Area="DataArea" AreaIndex="2" 
                                FieldName="qtdRealAno1" AllowedAreas="DataArea" 
                                CellFormat-FormatString="{0:n0}" CellFormat-FormatType="Custom">
                            </dxpgwx:PivotGridField>

                            <dxpgwx:PivotGridField ID="fieldqtdPrevistoPeriodoAno0" Area="DataArea" 
                                AreaIndex="4" FieldName="qtdPrevistoPeriodoAno0" AllowedAreas="DataArea" 
                                CellFormat-FormatString="{0:n0}" CellFormat-FormatType="Custom">
                            </dxpgwx:PivotGridField>
                            <dxpgwx:PivotGridField ID="fieldRealPlanoAnual" Area="DataArea" 
                                AreaIndex="6" AllowedAreas="DataArea" 
                                CellFormat-FormatString="{0:n0}" CellFormat-FormatType="Custom" 
                                Caption="% Execução">
                            </dxpgwx:PivotGridField>
                            <dxpgwx:PivotGridField ID="fieldRealPlano0" Area="DataArea" 
                                AreaIndex="7" 
                                Caption="fieldRealPlano0" AllowedAreas="DataArea">
                            </dxpgwx:PivotGridField>
                            <dxpgwx:PivotGridField ID="fieldRealPlano1" Area="DataArea" AreaIndex="8" 
                                Caption="fieldRealPlano1" AllowedAreas="DataArea">
                            </dxpgwx:PivotGridField> 
                            <dxpgwx:PivotGridField ID="fieldvalorRealOrcAno" AllowedAreas="DataArea" 
                                Area="DataArea"  Caption="Orc. Realizado" 
                                CellFormat-FormatString="n2" CellFormat-FormatType="Numeric" 
                                FieldName="valorRealOrcAno" AreaIndex="1">
                            </dxpgwx:PivotGridField>
                            <dxpgwx:PivotGridField ID="fieldValorPrevOrcAno" AllowedAreas="DataArea" 
                                Area="DataArea" AreaIndex="0" Caption="Orc. Previsto" 
                                CellFormat-FormatString="n2" CellFormat-FormatType="Numeric" 
                                FieldName="ValorPrevOrcAno">
                            </dxpgwx:PivotGridField>
                            <dxpgwx:PivotGridField ID="fieldNomeUnidadeNegocio" Area="RowArea" 
                                AreaIndex="2" Caption="Unidade" FieldName="NomeUnidadeNegocio">
                            </dxpgwx:PivotGridField>
                        </Fields>
                        <OptionsView ShowColumnTotals="False" ShowColumnGrandTotalHeader="False" 
                            ShowDataHeaders="False" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" />
                        <OptionsPager RowsPerPage="100">
                        </OptionsPager>
                        <Styles>
                            <FieldValueStyle CssClass="rowFieldValues">
                            </FieldValueStyle>
                        </Styles>
                    </dxwpg:ASPxPivotGrid>
    <dxpgwx:ASPxPivotGridExporter ID="exporter" runat="server"
        ASPxPivotGridID="pgDados" oncustomexportcell="exporter_CustomExportCell" 
                        oncustomexportfieldvalue="exporter_CustomExportFieldValue" 
                        oncustomexportheader="exporter_CustomExportHeader">
        <OptionsPrint>
            <PageSettings Landscape="True" PaperKind="A4" />
        </OptionsPrint>
    </dxpgwx:ASPxPivotGridExporter>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback">
        <ClientSideEvents CallbackComplete="function(s, e) {
               if(e.parameter.indexOf('salvar_como') != -1){
		CarregarConsulta(e.result);
	}
	else if(e.parameter.indexOf('salvar') != -1){
		//Se o retorno indicar que a consulta não foi salva ou por a consulta
		 //carregada ter sido excluída ou por ter sido carregada as configurações
		//originais da consulta, é exibida o popup de salvar como
		if(e.result.toLowerCase() == String(true)){
			 parent.ExibirJanelaSalvarComo();
}
}
}
" />
    </dxcb:ASPxCallback>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
