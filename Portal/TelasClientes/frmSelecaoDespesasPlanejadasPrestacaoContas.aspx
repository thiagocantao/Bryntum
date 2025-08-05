<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSelecaoDespesasPlanejadasPrestacaoContas.aspx.cs" Inherits="TelasClientes_frmSelecaoDespesasPlanejadasPrestacaoContas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        function btnSalvar_Click(s, e) {
            var keys = [];
            keys = gvDados.GetSelectedKeysOnPage();
            if (keys.length > 0)
                callback.PerformCallback(keys.join(','));
            else
                window.top.mostraMensagem('Nenhuma despesa selecionada', 'erro', true, false, null);
        }

        function btnCancelar_Click(s, e) {
            FecharJanela();
        }

        function FecharJanela() {
            window.top.fechaModal();
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxcp:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados" DataSourceID="dataSource"  KeyFieldName="CodigoOutraDespesa" Width="100%">
                            <ClientSideEvents Init="function(s, e) {
		var height = Math.max(0, document.documentElement.clientHeight);
	height = height - 75;
	s.SetHeight(height);
}" />
                            <Settings VerticalScrollBarMode="Visible" />
                            <SettingsText EmptyDataRow="Não há outras despesas pendentes" />
                            <Columns>
                                <dxtv:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0" Width="30px">
                                </dxtv:GridViewCommandColumn>
                                <dxtv:GridViewDataTextColumn FieldName="CodigoOutraDespesa" Visible="False" VisibleIndex="1">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Despesa" FieldName="DescricaoItem" VisibleIndex="2">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Quantidade" FieldName="Quantidade" VisibleIndex="3" Width="125px">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Valor Unitário" FieldName="ValorUnitario" VisibleIndex="4" Width="125px">
                                    <PropertiesTextEdit DisplayFormatString="N2">
                                    </PropertiesTextEdit>
                                </dxtv:GridViewDataTextColumn>
                            </Columns>

                        </dxcp:ASPxGridView>
                        <asp:SqlDataSource ID="dataSource" runat="server" SelectCommand=" SELECT CodigoOutraDespesa,
		DescricaoItem,
		Quantidade,
		ValorUnitario
   FROM f_SENAR_GetItensOutrasDespesasABC(@in_CodigoWorkflow, @in_CodigoInstanciaWF)">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="in_CodigoWorkflow" QueryStringField="cw" />
                                <asp:QueryStringParameter Name="in_CodigoInstanciaWF" QueryStringField="ci" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="margin-left: auto;">
                            <tr>
                                <td style="padding-left: 10px">
                                    <dxcp:ASPxButton runat="server" Text="Salvar" ID="btnSalvar" AutoPostBack="False" ClientInstanceName="btnSalvar"  Width="75px">
                                        <ClientSideEvents Click="btnSalvar_Click" />
                                    </dxcp:ASPxButton>
                                </td>
                                <td style="padding-left: 10px">
                                    <dxcp:ASPxButton runat="server" Text="Cancelar" ID="btnCancelar" AutoPostBack="False" ClientInstanceName="btnCancelar"  Width="75px">
                                        <ClientSideEvents Click="btnCancelar_Click" />
                                    </dxcp:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                <ClientSideEvents CallbackComplete="FecharJanela" />
            </dxcp:ASPxCallback>
        </div>
    </form>
</body>
</html>
