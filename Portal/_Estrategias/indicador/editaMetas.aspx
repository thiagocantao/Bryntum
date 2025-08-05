<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editaMetas.aspx.cs" Inherits="_Estrategias_wizard_editaMetas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <style type="text/css">

        .titulo-atualizacao-resultados {
            color: #575757;
            display: inline-block;
            font-size: 16px !important;
            padding-left: 8px;
            padding-top: 10px;
        }

        .marginLeft10 {
            margin-left: 10px;
        }

        .style2 {
            width: 75px;
        }

        .style3 {
            width: 150px;
        }

        .style4 {
            width: 100%;
        }

        .style5 {
            width: 100%;
        }

    </style>
    <script language="javascript" type="text/javascript">
        var codigoUnidadeSelecionado;
        var validaFechamento = 'S';

        function fechaEdicao() {
            validaFechamento = 'N';

            gvMetas.CancelEdit();

            if (window.parent.gvDados)
                window.parent.gvDados.PerformCallback();

            if (window.parent.pcDados) {
                window.parent.pcDados.Hide();
                window.open(window.top.pcModal.cp_Path + "branco.htm", '_self');
            }
        }
    </script>
</head>
<body style="margin: 0px; min-height : 400px; overflow: auto;" onunload="/*fechaEdicao();*/" onload="inicializaVariaveis();">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="<%=exibeTitulo%>width: 100%">
            <tr style="height: 26px">
                <td valign="middle">
                    <table>
                        <tr>
                            <td><dxe:ASPxLabel CssClass="titulo-atualizacao-resultados" ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloSelecao"
                                Font-Bold="True" Text="Atualização de Metas">
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
                                            <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                                Text="Indicador:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="formulario-label" style="width: 75px;">
                                            <dxe:ASPxLabel ID="ASPxLabelAno" runat="server"
                                                Text="Ano:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="formulario-label" style="width: 150px;">
                                            <dxe:ASPxLabel runat="server" Text="Valor Atual:"
                                                ID="lblTituloMeta">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtIndicadorDado" runat="server" ClientEnabled="False" ClientInstanceName="txtIndicadorDado"
                                                Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td style="width: 75px;">
                                            <dxe:ASPxComboBox ID="ddlAnos" runat="server" ClientInstanceName="ddlAnos"
                                                Width="100%" AutoPostBack="True">
                                                <ClientSideEvents EndCallback="function(s, e) {
	hfGeral.Set(&quot;Ano&quot;, s.cp_Ano);
    gvMetas.PerformCallback('Atualizar');
}"
                                                    SelectedIndexChanged="function(s, e) {	
	
	if(gvMetas.batchEditApi.HasChanges())
	{
        var funcObj = { funcaoClickOK: function(s, e){ hfGeral.Set(&quot;Ano&quot;, s.GetValue());	
		                                               gvMetas.CancelEdit();
		                                               gvMetas.PerformCallback('Atualizar'); },
			            funcaoClickCancelar: function(s, e){ s.SetValue(codigoUnidadeSelecionado); } }
	    //window.top.mostraConfirmacao('Existem dados não salvos que serão perdidos. Deseja continuar?', function(){funcObj['funcaoClickOK'](s, e)}, function(){funcObj['funcaoClickCancelar'](s, e)});
		gvMetas.CancelEdit();
		gvMetas.PerformCallback('Atualizar');
    }
    else
    {
        hfGeral.Set(&quot;Ano&quot;, s.GetValue());	
		gvMetas.PerformCallback('Atualizar');
    }
}"
                                                    GotFocus="function(s, e) {
	codigoUnidadeSelecionado = s.GetValue();
}" />
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td style="width: 150px;">
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
                    <tr class="formulario-linha">
                        <td>
                            <dxe:ASPxLabel ID="lblPeriodoVigencia" runat="server" Font-Bold="True"
                                Font-Italic="True">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr class="formulario-linha">
                        <td>
                            <dxwgv:ASPxGridView ID="gvMetas" runat="server" ClientInstanceName="gvMetas"
                                KeyFieldName="CodigoUnidade" OnCellEditorInitialize="gvMetas_CellEditorInitialize"
                                Width="100%" OnBatchUpdate="gvMetas_BatchUpdate"
                                OnHtmlDataCellPrepared="gvMetas_HtmlDataCellPrepared"
                                AutoGenerateColumns="False">

                                <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False"></SettingsBehavior>



                                <SettingsEditing Mode="Batch"></SettingsEditing>

                                <SettingsText EmptyHeaders="Resultados"
                                    EmptyDataRow="Nenhum Resultado para Atualizar no Indicador Selecionado."></SettingsText>
                              <%--  <Templates>
                                    <StatusBar>
                                        <table align="right" class="formulario-botoes">
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxHyperLink ID="hlSave" runat="server" Cursor="pointer"
                                                            CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" CssPostfix="MaterialCompact"
                                                        Font-Underline="True" Text="Salvar">
                                                        <ClientSideEvents Click="function(s, e){ gvMetas.UpdateEdit(); }" />
                                                    </dxtv:ASPxHyperLink>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxHyperLink ID="hlCancel" runat="server" Cursor="pointer"
                                                            CssFilePath="~/App_Themes/MaterialCompact/{0}/styles.css" CssPostfix="MaterialCompact"
                                                        Font-Underline="True" Text="Cancelar">
                                                        <ClientSideEvents Click="function(s, e){inicializaVariaveis(); gvMetas.CancelEdit(); gvMetas.Refresh(); }" />
                                                    </dxtv:ASPxHyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </StatusBar>
                                </Templates>--%>
                                <ClientSideEvents BatchEditConfirmShowing="function(s, e) {
	if(window.parent.pnCarregarClick)
		window.parent.pnCarregarClick.Hide();

	if(validaFechamento == 'N')
		e.cancel = true;
}"
                                    FocusedRowChanged="function(s, e) {
	inicializaVariaveis();
}"
                                    Init="function(s, e) {
	//gvMetas.SetHeight(window.innerHeight - 95);

}"
                                    BatchEditStartEditing="function(s, e) {
	if(hfGeral.Get(e.focusedColumn.fieldName + '_' + e.visibleIndex + '_' + e.focusedColumn.index) == 'N')
                                {
                                e.cancel = true;
                                }
		

}" BeginCallback="function(s, e) {
	hfGeral.Set('command', e.command);
}" />

                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <SettingsEditing Mode="Batch">
                                </SettingsEditing>

                                <Settings
                                    VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />

                                <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False"
                                    AllowFocusedRow="True" />

                                <SettingsText EmptyDataRow="Nenhum Resultado para Atualizar no Indicador Selecionado."
                                    EmptyHeaders="Resultados"
                                    ConfirmOnLosingBatchChanges="Existem dados que foram alterados e não salvos. Deseja sair da atualização de metas?" />
                                <Columns>
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
                                    <dxwgv:GridViewDataTextColumn FieldName="_Mes" Visible="False" VisibleIndex="4">
                                        <EditFormSettings Visible="True" />
                                        <EditFormSettings Visible="True"></EditFormSettings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="_Ano" Visible="False" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <Styles>
                                    <StatusBar HorizontalAlign="Right">
                                    </StatusBar>
                                    <BatchEditModifiedCell BackColor="#CCFFCC" ForeColor="#666666">
                                    </BatchEditModifiedCell>
                                </Styles>
                            </dxwgv:ASPxGridView>
                            <script language="javascript" type="text/javascript">                            
                                //gvMetas.SetHeight(window.innerHeight - 95);
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
