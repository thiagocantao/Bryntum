<%@ Page Language="C#" AutoEventWireup="true" CodeFile="relResumoProjeto.aspx.cs"
    Inherits="_Projetos_Relatorios_relResumoProjeto" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="DivPrincipal" runat="server">
        <table style="width: 100%;">
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                            KeyFieldName="codigoSequencialAnexo" Width="100%" ClientInstanceName="gvDados"
                             OnCustomUnboundColumnData="gvDados_CustomUnboundColumnData"
                            EnableRowsCache="False" EnableViewState="False">
                            <Columns>
                                <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="40px"
                                    ShowInCustomizationForm="True" Caption=" ">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="Nome" VisibleIndex="1"
                                    ShowInCustomizationForm="True">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataBinaryImageColumn Caption="Foto" FieldName="Miniatura" ShowInCustomizationForm="True"
                                    UnboundType="Object" VisibleIndex="2" Width="120px">
                                </dxwgv:GridViewDataBinaryImageColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Codigo" FieldName="codigoSequencialAnexo"
                                    Visible="False" VisibleIndex="3" ShowInCustomizationForm="True">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings VerticalScrollBarMode="Visible" ShowTitlePanel="True" />
                            <SettingsText  
                                GroupPanel="Arraste aqui as colunas que deseja agrupar" 
                                Title="Selecione até 4 fotos entre as últimas apresentadas abaixo:" />
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-top: 10px">
                        <table>
                            <tr>
                                <td style="padding-right: 10px">
                                    <dxe:ASPxButton ID="btnPdf" runat="server" Text="PDF" ClientInstanceName="btnPdf"
                                         OnClick="btnExecutar_Click" 
                                        Width="75px" ClientVisible="False">
                                        <ClientSideEvents Click="function(s, e) {
	//document.body.style.cursor = 'wait';
	var qtdeFotosSelecionadas = gvDados.GetSelectedRowCount();
	if(qtdeFotosSelecionadas &lt;= 4)
		processOnServer = true;
	else
	{
		window.top.mostraMensagem('No máximo 4 fotos devem ser selecionadas.', 'atencao', true, false, null);
		processOnServer = true;
	}
}" />
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btnRtf" runat="server" Text="Gerar Relatório" ClientInstanceName="btnRtf"
                                         OnClick="btnExecutar_Click" 
                                        Width="120px">
                                        <ClientSideEvents Click="function(s, e) {
	//document.body.style.cursor = 'wait';
	var qtdeFotosSelecionadas = gvDados.GetSelectedRowCount();
	if(qtdeFotosSelecionadas &lt;= 4)
		processOnServer = true;
	else
	{
		window.top.mostraMensagem('No máximo 4 fotos devem ser selecionadas.', 'atencao', true, false, null);
		processOnServer = true;
	}
}" />
                                        <Paddings Padding="0px" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    </div>
    <dxpc:ASPxPopupControl ID="pcRelatorio" runat="server" AllowDragging="True" ClientInstanceName="pcRelatorio"
        CloseAction="CloseButton" HeaderText="Relatório" Height="750px" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" ShowPageScrollbarWhenModal="True"
        Width="775px">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <div style="padding-top: 20px">
                    <dxcp:ASPxCallbackPanel ID="pnlCallback" runat="server" 
                        ClientInstanceName="pnlCallback" Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxxr:ReportToolbar ID="ReportToolbar1" runat="server" ReportViewer="<%# ReportViewer1 %>"
                                    ShowDefaultButtons="False">
                                    <Items>
                                        <dxxr:ReportToolbarButton ItemKind="Search" />
                                        <dxxr:ReportToolbarSeparator />
                                        <dxxr:ReportToolbarButton ItemKind="PrintReport" />
                                        <dxxr:ReportToolbarButton ItemKind="PrintPage" />
                                        <dxxr:ReportToolbarSeparator />
                                        <dxxr:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                                        <dxxr:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                                        <dxxr:ReportToolbarLabel ItemKind="PageLabel" Text="Página" />
                                        <dxxr:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                                        </dxxr:ReportToolbarComboBox>
                                        <dxxr:ReportToolbarLabel ItemKind="OfLabel" Text="de" />
                                        <dxxr:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                                        <dxxr:ReportToolbarButton ItemKind="NextPage" />
                                        <dxxr:ReportToolbarButton ItemKind="LastPage" />
                                        <dxxr:ReportToolbarSeparator />
                                        <dxxr:ReportToolbarButton ItemKind="SaveToDisk" />
                                        <dxxr:ReportToolbarButton ItemKind="SaveToWindow" />
                                        <dxxr:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                            <Elements>
                                                <dxxr:ListElement Value="pdf" />
                                                <dxxr:ListElement Value="rtf" />
                                            </Elements>
                                        </dxxr:ReportToolbarComboBox>
                                    </Items>
                                    <Styles>
                                        <LabelStyle>
                                            <Margins MarginLeft="3px" MarginRight="3px" />
                                        </LabelStyle>
                                    </Styles>
                                </dxxr:ReportToolbar>
                                <dxxr:ReportViewer ID="ReportViewer1" runat="server" AutoSize="False" Width="750px">
                                </dxxr:ReportViewer>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                </div>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="     SELECT TOP 10 
            a.Nome,
            ca.codigoSequencialAnexo,
            ca.Anexo
       FROM Anexo AS a INNER JOIN
            AnexoAssociacao AS aa ON (aa.CodigoAnexo = a.CodigoAnexo AND 
                                              aa.CodigoObjetoAssociado = @CodigoProjeto AND
                                              aa.CodigoTipoAssociacao = 4) INNER JOIN
            AnexoVersao AS av ON (av.codigoAnexo = a.CodigoAnexo) INNER JOIN
            ConteudoAnexo AS ca ON (ca.codigoSequencialAnexo = av.codigoSequencialAnexo)
      WHERE a.DataExclusao IS NULL
        AND (av.nomeArquivo LIKE '%.JPG%' OR av.NomeArquivo LIKE '%.BMP%' OR av.NomeArquivo LIKE '%.PNG%')
      ORDER BY 
            av.dataVersao DESC">
        <SelectParameters>
            <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="CP" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
