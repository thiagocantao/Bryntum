<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editaMetas.aspx.cs" Inherits="_Estrategias_wizard_editaMetas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style3 {
            width: 110px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        var valores;
        var anos;
        var meses;

        function atualizaDadosExternos() {
            try {
                window.parent.txtValorMeta.SetText(txtMetaNumerica.GetText());
            } catch (e) { }

            try {
                window.parent.gvDados.PerformCallback();
            } catch (e) { }

            try {
                window.parent.verificaEdicaoPeriodicidade('N');
            } catch (e) { }
        }

        function verificaMetasMesesAnteriores() {
            var anoAtual = -1;
            var possuiPendencias = false;
            var possuiValor = false;
            var ultimoMesValor = '';

            for (var i = (anos.length - 1); i >= 0; i--) {
                if (anoAtual != anos[i]) {
                    anoAtual = anos[i];
                    possuiPendencias = false;
                    possuiValor = false;
                    ultimoMesValor = "";
                }

                if (valores[i] != null && valores[i] + '' != '') {
                    possuiValor = true;
                    ultimoMesValor = ultimoMesValor != "" ? ultimoMesValor : meses[i];
                }
                else if (possuiValor)
                    return "Todos os períodos do ano " + anoAtual + " anteriores a '" + ultimoMesValor + "' devem ser preenchidos!";


            }
            return "";
        }

        function getUltima(s, e, indexColuna) {
            var valorAtual = s.GetValue() == null ? '' : s.GetValue().toString();

            if (valorAtual == '') {
                txtMetaInformada.SetText('');
            } else {

                valores[indexColuna] = valorAtual;

                var valor = getLstArray(valores);

                if (isNaN(valor) || valor == null)
                    txtMetaInformada.SetText('');
                else
                    txtMetaInformada.SetText(valor.toString().replace('.', ','));
            }
        }

        function getSoma(s, e, indexColuna) {
            var valorAtual = s.GetValue() == null ? '0' : s.GetValue().toString();

            if (valorAtual == '') {
                txtMetaInformada.SetText('');
            } else {

                valores[indexColuna] = valorAtual;

                var valor = getSumArray(valores);
                //valor = valor.toFixed(2);

                if (isNaN(valor) || valor == null)
                    txtMetaInformada.SetText('');
                else
                    txtMetaInformada.SetText(valor.toString().replace('.', ','));
            }
        }

        function getMinimo(s, e, indexColuna) {
            var valorAtual = s.GetValue() == null ? '' : s.GetValue().toString();

            if (valorAtual == '') {
                txtMetaInformada.SetText('');
            } else {

                valores[indexColuna] = valorAtual;

                var valor = getMinArray(valores);
                if (isNaN(valor) || valor == null)
                    txtMetaInformada.SetText('');
                else
                    txtMetaInformada.SetText(valor.toString().replace('.', ','));
            }
        }

        function getMaximo(s, e, indexColuna) {
            var valorAtual = s.GetValue() == null ? '' : s.GetValue().toString();

            if (valorAtual == '') {
                txtMetaInformada.SetText('');
            } else {

                valores[indexColuna] = valorAtual;

                var valor = getMaxArray(valores);
                if (isNaN(valor) || valor == null)
                    txtMetaInformada.SetText('');
                else
                    txtMetaInformada.SetText(valor.toString().replace('.', ','));
            }
        }

        function getMedia(s, e, indexColuna) {
            var valorAtual = s.GetValue() == null ? '0' : s.GetValue().toString();

            if (valorAtual == '') {
                txtMetaInformada.SetText('');
            } else {

                valores[indexColuna] = valorAtual;

                var valor = getAvgArray(valores);
                if (isNaN(valor) || valor == null)
                    txtMetaInformada.SetText('');
                else
                    txtMetaInformada.SetText(valor.toString().replace('.', ','));
            }
        }

        function inicializaVariaveis() {
            valores = gvMetas.cp_Valores.toString().split(';');
            anos = gvMetas.cp_Anos.toString().split(';');
            meses = gvMetas.cp_Meses.toString().split(';');
        }

        function getLstArray(array) {
            var ultimo;

            for (i = array.length; i >= 0; i--) {
                if (array[i] != null && array[i] != "") {
                    var valorArray = parseFloat(parseFloat(array[i].toString().replace(',', '.')).toFixed(2));
                    ultimo = valorArray;
                    break;
                }
            }

            return ultimo;
        };

        function getMaxArray(array) {
            var maximo;

            for (i = 0; i < array.length; i++) {
                if (array[i] != null && array[i] != "") {
                    var valorArray = parseFloat(parseFloat(array[i].toString().replace(',', '.')).toFixed(2));
                    if (maximo == null || valorArray > maximo)
                        maximo = valorArray;
                }
            }

            return maximo;
        }

        function getMinArray(array) {
            var minimo;

            for (i = 0; i < array.length; i++) {
                if (array[i] != null && array[i] != "") {
                    var valorArray = parseFloat(parseFloat(array[i].toString().replace(',', '.')).toFixed(2));
                    if (minimo == null || valorArray < minimo)
                        minimo = valorArray;
                }
            }

            return minimo;
        };

        function getAvgArray(array) {
            var count = 0;
            var soma = 0;

            for (i = 0; i < array.length; i++) {
                if (array[i] != null && array[i] != "") {
                    soma += parseFloat(parseFloat(array[i].toString().replace(',', '.')).toFixed(2));
                    count++;
                }
            }

            return count == 0 ? null : soma / count;
        };

        function getSumArray(array) {
            var count = 0;
            var soma = 0;

            for (i = 0; i < array.length; i++) {
                if (array[i] != null && array[i] != "") {
                    soma += parseFloat(parseFloat(array[i].toString().replace(',', '.')).toFixed(2));

                    count++;
                }
            }

            return count == 0 ? null : soma;
        };
    </script>
</head>
<body style="margin: 0px" onload="inicializaVariaveis();">
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%">
                <tbody>
                    <tr>
                        <td class="auto-style1">
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%; <%=mostraTDPeriodo %>" id="tdPeriodo">
                                            <dxrp:ASPxRoundPanel runat="server" HeaderText="Per&#237;odo" View="GroupBox" Width="225px" ID="ASPxRoundPanel1">
                                                <ContentPaddings Padding="1px"></ContentPaddings>
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server">
                                                        <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackPeriodos" Width="211px" ID="pnCallbackPeriodos">
                                                            <ClientSideEvents EndCallback="function(s, e) {
	gvMetas.PerformCallback();
}"></ClientSideEvents>
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 110px">
                                                                                    <dxe:ASPxLabel runat="server" Text="In&#237;cio:" ID="ASPxLabel1">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="T&#233;rmino:" ID="ASPxLabel9">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 110px">
                                                                                    <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" AllowUserInput="False" Width="100px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtDe" ID="txtDe">
                                                                                        <CalendarProperties ShowClearButton="False" ShowTodayButton="False" HighlightToday="False"></CalendarProperties>
                                                                                        <ClientSideEvents DateChanged="function(s, e) {
	if(gvDados.cp_Inicio != s.GetText().toString())
	{				
		var dataAtual = new Date();
		var dataDe = new Date(s.GetValue());
		var dataAte = new Date(txtFim.GetValue());
		var qtdDias = parseInt(gvDados.cp_dias);
		txtFim.SetMinDate(s.GetValue());

		if(dataDe &gt; dataAte)
			txtFim.SetValue(dataDe.getDate() + qtdDias);

		dataDe.setDate(dataDe.getDate() + qtdDias);		
	
		if(dataDe &lt; dataAte)
			txtFim.SetValue(dataDe);		

		gvDados.cp_Inicio = s.GetValue();
		
		

		gvMetas.PerformCallback();
	}
}"></ClientSideEvents>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" AllowUserInput="False" Width="100px" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtFim" ID="txtFim">
                                                                                        <CalendarProperties ShowClearButton="False" ShowTodayButton="False" HighlightToday="False"></CalendarProperties>
                                                                                        <ClientSideEvents DateChanged="function(s, e) {
	if(gvDados.cp_Termino != s.GetText().toString())
	{
		gvDados.cp_Termino = s.GetValue();
		gvMetas.PerformCallback();
	}
}"></ClientSideEvents>
                                                                                    </dxe:ASPxDateEdit>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxcp:ASPxCallbackPanel>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxrp:ASPxRoundPanel>
                                        </td>
                                        <td>
                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel runat="server" Text="Indicador:" ID="ASPxLabel8">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td class="style3">
                                                            <dxe:ASPxLabel runat="server" Text="Meta Numérica:"
                                                                ID="ASPxLabel14">
                                                            </dxe:ASPxLabel>




                                                        </td>
                                                        <td class="style3">
                                                            <dxe:ASPxLabel runat="server" Text="Valor Atual:"
                                                                ID="lblTituloMeta">
                                                            </dxe:ASPxLabel>




                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="99%" ClientInstanceName="txtIndicadorDado" ClientEnabled="False" ID="txtIndicadorDado">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td class="style3">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right"
                                                                ClientInstanceName="txtMetaNumerica"
                                                                ID="txtMetaNumerica" ClientEnabled="False" DisplayFormatString="{0:n2}">

                                                                <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                            </dxe:ASPxTextBox>





                                                        </td>
                                                        <td class="style3">
                                                            <dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right"
                                                                ClientInstanceName="txtMetaInformada"
                                                                ID="txtMetaInformada" ClientEnabled="False" DisplayFormatString="{0:n2}">

                                                                <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                            </dxe:ASPxTextBox>





                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <dxwgv:ASPxGridView ID="gvMetas" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvMetas"
                                KeyFieldName="_CodigoMeta" OnCellEditorInitialize="gvMetas_CellEditorInitialize"
                                OnCommandButtonInitialize="gvMetas_CommandButtonInitialize" OnRowUpdating="gvMetas_RowUpdating"
                                Width="100%" OnCancelRowEditing="gvMetas_CancelRowEditing">
                                <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <SettingsEditing Mode="Inline" />

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                                <SettingsText EmptyDataRow="Nenhum Resultado para Atualizar no Indicador Selecionado."
                                    EmptyHeaders="Resultados" />
                                <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Atualiza == 'S')
		atualizaDadosExternos();
	
	if(s.cp_MetaBanco != &quot;NULL&quot;)
		txtMetaInformada.SetText(s.cp_MetaBanco);
}"
                                    Init="function(s, e) {
	var telaHeight = Math.max(768, window.top.innerHeight);
    var height = 0;
    if (telaHeight&lt;769)
    {
        height = 143;
    }
	else
    {
        height = 203;
    }
 	s.SetHeight(height);
}" />
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="35px" ShowEditButton="true">
                                        <HeaderTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td align="center">
                                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                            ClientInstanceName="menu"
                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                            OnInit="menu_Init">
                                                            <Paddings Padding="0px" />
                                                            <Items>
                                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                    <Items>
                                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                            ClientVisible="False">
                                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                            <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                    </Items>
                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                    <Items>
                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                            <Image IconID="save_save_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                            <Image IconID="actions_reset_16x16">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                    </Items>
                                                                    <Image Url="~/imagens/botoes/layout.png">
                                                                    </Image>
                                                                </dxm:MenuItem>
                                                            </Items>
                                                            <ItemStyle Cursor="pointer">
                                                                <HoverStyle>
                                                                    <border borderstyle="None" />
                                                                </HoverStyle>
                                                                <Paddings Padding="0px" />
                                                            </ItemStyle>
                                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                <SelectedStyle>
                                                                    <border borderstyle="None" />
                                                                </SelectedStyle>
                                                            </SubMenuItemStyle>
                                                            <Border BorderStyle="None" />
                                                        </dxm:ASPxMenu>
                                                    </td>
                                                </tr>
                                            </table>

                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Per&#237;odo" FieldName="Periodo" ReadOnly="True"
                                        VisibleIndex="1" Width="345px">
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
                                <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible"
                                    VerticalScrollableHeight="204" />
                            </dxwgv:ASPxGridView>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxwgv:ASPxGridViewExporter runat="server"
            GridViewID="gvDados" LeftMargin="50" RightMargin="50"
            Landscape="True" ID="ASPxGridViewExporter1"
            ExportEmptyDetailGrid="True"
            PreserveGroupRowStates="False"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Default>
                </Default>
                <Header>
                </Header>
                <Cell>
                </Cell>
                <Footer>
                </Footer>
                <GroupFooter>
                </GroupFooter>
                <GroupRow>
                </GroupRow>
                <Title></Title>
            </Styles>
        </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
