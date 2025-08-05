<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssociacaoProjetoInstrumentoJuridico.aspx.cs" Inherits="_Projetos_DadosProjeto_AssociacaoProjetoInstrumentoJuridico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function SelecionarInstrumentosJuridicos() {
            window.top.showModal('SelecaoInstrumentoJuridicoAssociadoProjeto.aspx?cp=' + gvDados.cpCodigoProjeto, 'Selecionar instrumentos jurídicos associados ao projeto', screen.width - 30, window.top.innerHeight - 60, executarAposFecharPopup);
        }

        function executarAposFecharPopup(retorno) {
            gvDados.Refresh();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxcp:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados" DataSourceID="dataSource" Width="100%" EditFormLayoutProperties-SettingsItems-Height="100%" OnCustomJSProperties="gvDados_CustomJSProperties" KeyFieldName="CodigoIJ" OnCellEditorInitialize="gvDados_CellEditorInitialize" OnRowValidating="gvDados_RowValidating">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsEditing Mode="Inline">
                </SettingsEditing>
                <Settings VerticalScrollBarMode="Visible" />
                <SettingsBehavior ConfirmDelete="True" />
                <SettingsCommandButton>
                    <EditButton RenderMode="Image" Text="Alterar">
                        <Image AlternateText="Alterar" ToolTip="Alterar" Url="~/imagens/botoes/editarReg02.PNG">
                        </Image>
                    </EditButton>
                    <DeleteButton  RenderMode="Image" Text="Excluir">
                        <Image AlternateText="Excluir" ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                        </Image>
                    </DeleteButton>
                </SettingsCommandButton>
                <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                <Columns>
                    <dxtv:GridViewDataTextColumn FieldName="CodigoIJ" Visible="False" VisibleIndex="1">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="Instrumento Jurídico" FieldName="IJ" VisibleIndex="2" ReadOnly="True">
                        <EditFormSettings Visible="False" />
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewCommandColumn VisibleIndex="0" Width="75px" ShowDeleteButton="True" ShowEditButton="True">

                        <HeaderCaptionTemplate>
                            <dxtv:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" ImageUrl="~/imagens/botoes/novoReg.PNG" ShowLoadingImage="True" ToolTip="Incluir">
                                <ClientSideEvents Click="function(s, e) {
	SelecionarInstrumentosJuridicos();
}" />
                            </dxtv:ASPxImage>
                        </HeaderCaptionTemplate>
                    </dxtv:GridViewCommandColumn>
                    <dxtv:GridViewDataSpinEditColumn Caption="Valor Projeto" FieldName="ValorProjeto" VisibleIndex="3" Width="125px">
                        <PropertiesSpinEdit DisplayFormatString="N2" DecimalPlaces="2" AllowNull="False" DisplayFormatInEditMode="True">
                            <SpinButtons ShowIncrementButtons="False">
                            </SpinButtons>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesSpinEdit>
                        <HeaderStyle HorizontalAlign="Right" />
                    </dxtv:GridViewDataSpinEditColumn>
                    <dxtv:GridViewDataTextColumn FieldName="ValorMaximo" Visible="False" VisibleIndex="4">
                    </dxtv:GridViewDataTextColumn>
                </Columns>
            </dxcp:ASPxGridView>
            <asp:SqlDataSource ID="dataSource" runat="server" DeleteCommand="DELETE FROM [PBH_IJ_ProjetoPortal] WHERE [ID_IJ_INSTRUMENTO_JURIDICO] = @CodigoIJ AND [CodigoProjeto] = @CodigoProjeto" InsertCommand="INSERT INTO [PBH_IJ_ProjetoPortal] ([ID_IJ_INSTRUMENTO_JURIDICO], [CodigoProjeto], [ValorContratoProjeto], [CodigoUsuarioInclusao], [DataInclusao]) VALUES (@CodigoIJ, @CodigoProjeto, @ValorProjeto, @CodigoUsuarioInclusao, @DataInclusao)" SelectCommand="SELECT CodigoIJ,
		IJ,
		ValorProjeto,
		(ValorDisponivel + ValorProjeto) ValorMaximo
   FROM f_pbh_IJ_GetContratosProjeto(@CodigoProjeto)"
                UpdateCommand="UPDATE [PBH_IJ_ProjetoPortal] SET [ValorContratoProjeto] = @ValorProjeto, [CodigoUsuarioUltimaAlteracao] = @CodigoUsuarioUltimaAlteracao, [DataUltimaAlteracao] =GETDATE() WHERE [ID_IJ_INSTRUMENTO_JURIDICO] = @CodigoIJ AND [CodigoProjeto] = @CodigoProjeto">
                <DeleteParameters>
                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" Type="Int32" />
                    <asp:Parameter Name="CodigoIJ" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" Type="Int32" />
                    <asp:SessionParameter Name="CodigoUsuarioInclusao" SessionField="cul" Type="Int32" />
                    <asp:Parameter Name="DataInclusao" Type="DateTime" />
                    <asp:Parameter Name="CodigoIJ" />
                    <asp:Parameter Name="ValorProjeto" />
                </InsertParameters>
                <SelectParameters>
                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:SessionParameter Name="CodigoUsuarioUltimaAlteracao" SessionField="cul" Type="Int32" />
                    <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                    <asp:Parameter Name="ValorProjeto" />
                    <asp:Parameter Name="CodigoIJ" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
