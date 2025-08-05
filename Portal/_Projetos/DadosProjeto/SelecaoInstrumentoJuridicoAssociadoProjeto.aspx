<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelecaoInstrumentoJuridicoAssociadoProjeto.aspx.cs" Inherits="_Projetos_DadosProjeto_SelecaoInstrumentoJuridicoAssociadoProjeto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function DefinirInstrumentosJuridicosSelecionados() {
            gvDados.GetSelectedFieldValues("CodigoIJ", executarSelecaoDeIJs);            
        }

        function executarSelecaoDeIJs(valores) {
            var keys = "";
            if (valores.length == 0) {
                window.top.mostraMensagem('Nenhum instrumento jurídico foi selecionado', 'atencao', true, false, null);
                return;
            }
            for (i = 0; i < valores.length; i++) {

                keys += valores[i] + ",";
            }

            if (valores.length > 0)
                callback.PerformCallback(keys);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxcp:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados" DataSourceID="dataSource"  KeyFieldName="CodigoIJ" Width="100%">
                <ClientSideEvents Init="function(s, e) {
	var height = Math.max(0, document.documentElement.clientHeight);
	height = height - 60;
	s.SetHeight(height);
}" />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsEditing Mode="Inline">
                </SettingsEditing>
                <Settings VerticalScrollBarMode="Visible" />
                <Columns>
                    <dxtv:GridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0" Width="30px">
                    </dxtv:GridViewCommandColumn>
                    <dxtv:GridViewDataTextColumn FieldName="CodigoIJ" Visible="False" VisibleIndex="1">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="IJ" FieldName="IJ" VisibleIndex="2">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="Fornecedor" FieldName="Fornecedor" VisibleIndex="3">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="Valor Contrato" FieldName="ValorContrato" VisibleIndex="4" Width="125px">
                        <PropertiesTextEdit DisplayFormatString="N2">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Right" />
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="Disponível p/ Projeto" FieldName="ValorDisponivel" VisibleIndex="5" Width="125px">
                        <PropertiesTextEdit DisplayFormatString="N2">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Right" />
                    </dxtv:GridViewDataTextColumn>
                </Columns>
            </dxcp:ASPxGridView>

            <table style="padding-top: 5px; margin-left: auto;">
                <tr>
                    <td>
                        <dxcp:ASPxButton ID="ASPxButton1" runat="server" Text="Selecionar" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
	DefinirInstrumentosJuridicosSelecionados();
}" />
                        </dxcp:ASPxButton>
                    </td>
                    <td>
                        <dxcp:ASPxButton ID="ASPxButton2" runat="server" Text="Cancelar" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                        </dxcp:ASPxButton>
                    </td>
                </tr>
            </table>
            <asp:SqlDataSource ID="dataSource" runat="server" InsertCommand="INSERT INTO [PBH_IJ_ProjetoPortal] ([ID_IJ_INSTRUMENTO_JURIDICO], [CodigoProjeto], [ValorContratoProjeto], [CodigoUsuarioInclusao], [DataInclusao]) VALUES (@CodigoIJ, @CodigoProjeto, @ValorDisponivel, @CodigoUsuarioInclusao, GETDATE())" SelectCommand=" SELECT * FROM f_pbh_IJ_GetContratosAssociarProjeto(@CodigoProjeto)">
                <InsertParameters>
                    <asp:Parameter Name="CodigoIJ" />
                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                    <asp:SessionParameter Name="CodigoUsuarioInclusao" SessionField="cul" />
                    <asp:Parameter Name="ValorDisponivel" />
                </InsertParameters>
                <SelectParameters>
                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                </SelectParameters>
            </asp:SqlDataSource>

            <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                <ClientSideEvents CallbackComplete="function(s, e) {
	window.top.fechaModal();
}" />
            </dxcp:ASPxCallback>

        </div>
    </form>
</body>
</html>
