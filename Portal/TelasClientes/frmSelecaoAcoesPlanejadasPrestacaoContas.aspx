<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSelecaoAcoesPlanejadasPrestacaoContas.aspx.cs" Inherits="TelasClientes_frmSelecaoAcoesPlanejadasPrestacaoContas" %>

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
    <div><table>
                <tr>
                    <td>
                        <dxcp:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados" DataSourceID="dataSource"  KeyFieldName="CodigoAcao" Width="100%">
                            <ClientSideEvents Init="function(s, e) {
	var height = Math.max(0, document.documentElement.clientHeight);
	height = height - 75;
	s.SetHeight(height);
}" />
                            <Settings VerticalScrollBarMode="Visible" />
                            <SettingsText EmptyDataRow="Não há ações pendentes" />
                            <Columns>
                                <dxtv:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0" Width="30px">
                                </dxtv:GridViewCommandColumn>
                                <dxtv:GridViewDataTextColumn FieldName="CodigoAcao" Visible="False" VisibleIndex="1">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Ação" FieldName="DescricaoItem" VisibleIndex="2">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Valor" FieldName="ValorUnitario" VisibleIndex="3" Width="100px">
                                    <PropertiesTextEdit DisplayFormatString="n2">
                                    </PropertiesTextEdit>
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataDateColumn Caption="Início Previsto" FieldName="DataInicioPrevisto" VisibleIndex="4" Width="110px">
                                </dxtv:GridViewDataDateColumn>
                                <dxtv:GridViewDataDateColumn Caption="Término Previsto" FieldName="DataTerminoPrevisto" VisibleIndex="5" Width="110px">
                                </dxtv:GridViewDataDateColumn>
                                <dxtv:GridViewDataTextColumn Caption="Consultor" FieldName="NomeConsultor" VisibleIndex="6">
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Município" FieldName="NomeMunicipio" VisibleIndex="7">
                                </dxtv:GridViewDataTextColumn>
                            </Columns>

                        </dxcp:ASPxGridView>
                        <asp:SqlDataSource ID="dataSource" runat="server" SelectCommand=" SELECT f.CodigoAcao, 
		f.DescricaoItem, 
		f.ValorUnitario, 
		f.DataInicioPrevisto, 
		f.DataTerminoPrevisto,
		f.NomeConsultor,
		m.NomeMunicipio
   FROM f_SENAR_GetItensPrestacaoContasABC(@in_CodigoWorkflow, @in_CodigoInstanciaWF) AS f INNER JOIN
		Municipio AS m ON m.CodigoMunicipio = f.CodigoMunicipio">
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
