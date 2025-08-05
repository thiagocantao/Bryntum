<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editaResultados.aspx.cs"
    Inherits="_Estrategias_wizard_editaResultados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <style type="text/css">
        .style1 {
            height: 5px;
        }

        .titulo-atualizacao-resultados {
            color: #575757;
            display: inline-block;
            font-size: 16px !important;
            padding-left: 8px;
            padding-top: 10px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var codigoUnidadeSelecionado;
        var validaFechamento = 'S';
        var indexAtual = -1;

        function fechaEdicao() {
            validaFechamento = 'N';

            gvResultados.CancelEdit();

            if (window.parent.pcDados) {
                window.parent.pcDados.Hide();
                window.open(window.top.pcModal.cp_Path + "branco.htm", '_self');
            }
        }
    </script>
</head>
<body onunload="/*fechaEdicao();*/" style="margin: 0px">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="<%=exibeTitulo%>width: 100%">
            <tr style="height: 26px">
                <td valign="middle">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td><dxe:ASPxLabel CssClass="titulo-atualizacao-resultados" ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloSelecao"
                                Font-Bold="True" Text="Atualização de Resultados">
                            </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
            <div id="ConteudoPrincipal<%=sufixoConteudo%>">
                <table class="formulario" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr class="formulario-linha">
                            <td>
                                <table class="formulario-colunas" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td class="formulario-label">
                                                <dxe:ASPxLabel ID="ASPxLabelAno0" runat="server"
                                                    Text="Unidade:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td class="formulario-label">
                                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                                    Text="Indicador:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td class="formulario-label" style="width: 66px">
                                                <dxe:ASPxLabel ID="ASPxLabelAno" runat="server"
                                                    Text="Ano:">
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
}"
                                                        SelectedIndexChanged="function(s, e) {
	hfGeral.Set(&quot;CodigoUnidade&quot;, s.GetValue());
    gvResultados.PerformCallback('Atualizar');
}" />
                                                    <DisabledStyle ForeColor="#666666">
                                                    </DisabledStyle>
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtIndicadorDado" runat="server" ClientEnabled="False" ClientInstanceName="txtIndicadorDado"
                                                    Width="100%">
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td style="width: 66px">
                                                <dxe:ASPxComboBox ID="ddlAnos" runat="server" HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ClientInstanceName="ddlAnos"
                                                    Width="100px" AutoPostBack="True">
                                                    <ClientSideEvents EndCallback="function(s, e) {	
	gvResultados.PerformCallback();
}"
                                                        SelectedIndexChanged="function(s, e) {
	var continuaFuncao = true;
	if(gvResultados.batchEditApi.HasChanges())
	{
		e.cancel = true;
	}
    else
    {
        gvResultados.PerformCallback();
    }
}
"
                                                        GotFocus="function(s, e) {
	codigoUnidadeSelecionado = s.GetValue();
}
" />
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr class="formulario-linha">
                            <td>
                                <dxe:ASPxLabel ID="lblPeriodoVigencia" ClientInstanceName="lblTextoPeriodoVigencia" runat="server" Font-Bold="True" Font-Italic="True">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxwgv:ASPxGridView ID="gvResultados" runat="server" AutoGenerateColumns="False"
                                    ClientInstanceName="gvResultados" KeyFieldName="_CodigoDado" OnHtmlDataCellPrepared="gvResultados_HtmlDataCellPrepared" Width="100%" OnBatchUpdate="gvResultados_BatchUpdate" OnHtmlRowPrepared="gvResultados_HtmlRowPrepared">
                                    <ClientSideEvents BeginCallback="function(s, e) {
	if(gvResultados.IsEditing())
		ddlAnos.SetEnabled(false);
	else
		ddlAnos.SetEnabled(true);
}"
                                        EndCallback="function(s, e) {
	if(gvResultados.IsEditing())
		ddlAnos.SetEnabled(false);
	else
		ddlAnos.SetEnabled(true);
}"
                                        BatchEditConfirmShowing="function(s, e) {
	if(window.parent.pnCarregarClick)
		window.parent.pnCarregarClick.Hide();
	if(validaFechamento == 'N')
		e.cancel = true;

}
"
                                        BatchEditStartEditing="function(s, e) {
    indexAtual = e.visibleIndex;
	if(hfGeral.Get(e.focusedColumn.fieldName + '_' + e.visibleIndex) == 'N')
		e.cancel = true;
}" />
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
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <SettingsEditing Mode="Batch">
                                    </SettingsEditing>
                                    <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                                    <SettingsText EmptyDataRow="<%$ Resources:traducao,nenhuma_informa__o_a_ser_apresentada_ %>"
                                        EmptyHeaders="<%$ Resources:traducao, resultados %>" ConfirmOnLosingBatchChanges="Existem dados que foram alterados e não salvos. Deseja sair da atualização de resultados?" />
                                    <Styles>
                                        <BatchEditModifiedCell BackColor="#CCFFCC" ForeColor="#666666">
                                        </BatchEditModifiedCell>
                                    </Styles>
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="display: none; padding-top: 3px">
                                <script language="javascript" type="text/javascript">
                            /*
                            if (lblTextoPeriodoVigencia.GetText() == '')
                                gvResultados.SetHeight(window.innerHeight - 115);
                            else
                                gvResultados.SetHeight(window.innerHeight - 130);
                            */
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
