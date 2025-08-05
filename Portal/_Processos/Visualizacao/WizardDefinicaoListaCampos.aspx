<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WizardDefinicaoListaCampos.aspx.cs" Inherits="_Processos_Visualizacao_WizardDefinicaoListaCampos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
    </style>
    <script type="text/javascript">

        var gridCommand = '';
        var currentEditableVisibleIndex = -1;

        function cmbIndicaCampoHierarquia_ValueChanged(s, e) {
            var value = s.GetValue();
            if (value != 'P') {
                gvDados.batchEditApi.SetCellValue(currentEditableVisibleIndex, "unbound_CampoSubLista", null)
            }
        }

        function OnBatchEditStarEditing(s, e) {
            currentEditableVisibleIndex = e.visibleIndex;
            var fieldName = e.focusedColumn.fieldName;
            if (fieldName == "unbound_CampoSubLista") {
                var value = e.rowValues[gvDados.GetColumnByField("IndicaCampoHierarquia").index].value;
                if (value != "P") {
                    e.cancel = true;
                }
            }
            else if (fieldName == 'IndicaLink') {
                var permiteLink = false;
                if (s.GetColumnByField("IndicaCampoControle").visible) {
                    for (var i = 0; i < s.cp_rowCount; i++) {
                        var indicaCampoControlado = s.batchEditApi.GetCellValue(i, 'IndicaCampoControle');
                        var iniciaisCampoControle = s.batchEditApi.GetCellValue(i, 'IniciaisCampoControlado');
                        if (iniciaisCampoControle == 'CP' && indicaCampoControlado == 'S') {
                            permiteLink = true;
                            break;
                        }
                    }
                }
                else {
                    permiteLink = s.cp_existeColunaCodigoProjeto;
                }
                e.cancel = !permiteLink;
            }
        }

    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:100%">
                <tr>
                    <td style="padding-bottom: 10px">
                        <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Configurando campos do relatório" Font-Bold="True" Font-Size="10pt"></dxcp:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxcp:ASPxGridView ID="gvDados" ClientInstanceName="gvDados" runat="server"  AutoGenerateColumns="False" DataSourceID="dataSource" KeyFieldName="CodigoCampo" Width="100%" OnBatchUpdate="gvDados_BatchUpdate" OnCustomJSProperties="gvDados_CustomJSProperties">
                            <ClientSideEvents BatchEditStartEditing="OnBatchEditStarEditing" Init="function(s, e) {
    var height = Math.max(0, document.documentElement.clientHeight);
    height = height - 55;
 	s.SetHeight(height);
    window.top.retornoModal2 = {grid: gvDados};
}"
                                BeginCallback="function(s, e) {
	gridCommand = e.command;
}"
                                EndCallback="function(s, e) {
	if(gridCommand == 'UPDATEEDIT' || gridCommand == 'CANCELEDIT'){
		window.top.fechaModal2();
	}
}" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsEditing Mode="Batch">
                            </SettingsEditing>
                            <Settings HorizontalScrollBarMode="Visible" ColumnMinWidth="25" VerticalScrollBarMode="Visible" />
                            <SettingsBehavior AllowFocusedRow="True" />
                            <SettingsResizing ColumnResizeMode="Control"/>
                            <SettingsText ConfirmOnLosingBatchChanges="Tem certeza que deseja realizar essa ação? Todos os dados não salvos serão perdidos." CommandBatchEditCancel="Desfazer alterações" CommandBatchEditUpdate="Finalizar configuração de campos" />
                            <Columns>
                                <dxtv:GridViewDataTextColumn FieldName="CodigoCampo" ReadOnly="True" Visible="False" VisibleIndex="0">
                                    <EditFormSettings Visible="False" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn FieldName="CodigoLista" Visible="False" VisibleIndex="1">
                                    <EditFormSettings Visible="True" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Campo" FieldName="NomeCampo" VisibleIndex="2" FixedStyle="Left" Width="150px">
                                    <PropertiesTextEdit MaxLength="255">
                                        <ValidationSettings Display="Dynamic">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="False" />
                                    <CellStyle BackColor="#EBEBEB" Font-Bold="True" ForeColor="Black">
                                    </CellStyle>
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Título" FieldName="TituloCampo" VisibleIndex="4" Width="175px">
                                    <PropertiesTextEdit MaxLength="255">
                                        <ValidationSettings Display="Dynamic">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Formato" FieldName="Formato" VisibleIndex="9" Width="75px">
                                    <PropertiesTextEdit MaxLength="256">
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Iniciais controle" FieldName="IniciaisCampoControlado" VisibleIndex="19" Width="75px">
                                    <PropertiesTextEdit MaxLength="2">
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Título coluna agrupadora" FieldName="TituloColunaAgrupadora" Visible="False" VisibleIndex="25" Width="100px">
                                    <PropertiesTextEdit MaxLength="255">
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataSpinEditColumn Caption="Ordem" FieldName="OrdemCampo" VisibleIndex="5" Width="50px">
                                    <PropertiesSpinEdit DisplayFormatString="g" MaxValue="32767" MinValue="1" NumberType="Integer">
                                    </PropertiesSpinEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataSpinEditColumn>
                                <dxtv:GridViewDataSpinEditColumn Caption="Ordem de agrupamento" FieldName="OrdemAgrupamentoCampo" Visible="False" VisibleIndex="7" Width="100px">
                                    <PropertiesSpinEdit DisplayFormatString="g" MaxValue="32767" MinValue="-1" NumberType="Integer">
                                    </PropertiesSpinEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataSpinEditColumn>
                                <dxtv:GridViewDataComboBoxColumn Caption="Tipo de campo" FieldName="TipoCampo" VisibleIndex="8" Width="75px">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxtv:ListEditItem Text="Numérico" Value="NUM" />
                                            <dxtv:ListEditItem Text="Texto" Value="TXT" />
                                            <dxtv:ListEditItem Text="Data" Value="DAT" />
                                            <dxtv:ListEditItem Text="VAR" Value="VAR" />
                                            <dxtv:ListEditItem Text="Monetário" Value="MON" />
                                            <dxtv:ListEditItem Text="Percentual" Value="PER" />
                                            <dxtv:ListEditItem Text="Bullet" Value="BLT" />
                                        </Items>
                                    </PropertiesComboBox>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataComboBoxColumn>
                                <dxtv:GridViewDataCheckColumn Caption="Área de filtro?" FieldName="IndicaAreaFiltro" Visible="False" VisibleIndex="10" Width="75px">
                                    <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N" AllowGrayedByClick="False">
                                    </PropertiesCheckEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataCheckColumn>
                                <dxcp:GridViewDataComboBoxColumn FieldName="TipoFiltro" ShowInCustomizationForm="True" Width="75px" Caption="Tipo de filtro" Visible="False" VisibleIndex="11">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxcp:ListEditItem Text="Nenhum" Value="N"></dxcp:ListEditItem>
                                            <dxcp:ListEditItem Text="Edit&#225;vel" Value="E"></dxcp:ListEditItem>
                                            <dxcp:ListEditItem Text="Combo" Value="C"></dxcp:ListEditItem>
                                        </Items>
                                    </PropertiesComboBox>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxcp:GridViewDataComboBoxColumn>
                                <dxtv:GridViewDataCheckColumn Caption="Permite Agrupamento?" FieldName="IndicaAgrupamento" Visible="False" VisibleIndex="6" Width="100px">
                                    <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N" AllowGrayedByClick="False">
                                    </PropertiesCheckEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataCheckColumn>
                                <dxtv:GridViewDataComboBoxColumn Caption="Tipo de totalizador" FieldName="TipoTotalizador" VisibleIndex="12" Visible="False" Width="75px">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxtv:ListEditItem Text="Nenhum" Value="NENHUM" />
                                            <dxtv:ListEditItem Text="Contar" Value="CONTAR" />
                                            <dxtv:ListEditItem Text="Soma" Value="SOMA" />
                                            <dxtv:ListEditItem Text="Média" Value="MEDIA" />
                                        </Items>
                                    </PropertiesComboBox>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataComboBoxColumn>
                                <dxtv:GridViewDataCheckColumn Caption="Área de dados?" FieldName="IndicaAreaDado" Visible="False" VisibleIndex="13" Width="75px">
                                    <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N" AllowGrayedByClick="False">
                                    </PropertiesCheckEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataCheckColumn>
                                <dxtv:GridViewDataCheckColumn Caption="Área de colunas?" FieldName="IndicaAreaColuna" Visible="False" VisibleIndex="14" Width="75px">
                                    <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N" AllowGrayedByClick="False">
                                    </PropertiesCheckEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataCheckColumn>
                                <dxtv:GridViewDataCheckColumn Caption="Área de linhas?" FieldName="IndicaAreaLinha" Visible="False" VisibleIndex="15" Width="75px">
                                    <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N" AllowGrayedByClick="False">
                                    </PropertiesCheckEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataCheckColumn>
                                <dxtv:GridViewDataComboBoxColumn Caption="Área default" FieldName="AreaDefault" Visible="False" VisibleIndex="16" Width="75px">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxtv:ListEditItem Text="Linha" Value="L" />
                                            <dxtv:ListEditItem Text="Coluna" Value="C" />
                                            <dxtv:ListEditItem Text="Dados" Value="D" />
                                            <dxtv:ListEditItem Text="Filtro" Value="F" />
                                        </Items>
                                    </PropertiesComboBox>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataComboBoxColumn>
                                <dxtv:GridViewDataCheckColumn Caption="Campo visível?" FieldName="IndicaCampoVisivel" VisibleIndex="17" Width="75px">
                                    <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N" AllowGrayedByClick="False">
                                    </PropertiesCheckEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataCheckColumn>
                                <dxtv:GridViewDataCheckColumn Caption="Campo de controle" FieldName="IndicaCampoControle" VisibleIndex="18" Width="80px">
                                    <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N" AllowGrayedByClick="False">
                                    </PropertiesCheckEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataCheckColumn>
                                <dxtv:GridViewDataCheckColumn Caption="Link para o projeto?" FieldName="IndicaLink" VisibleIndex="20" Width="100px">
                                    <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N" AllowGrayedByClick="False">
                                    </PropertiesCheckEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataCheckColumn>
                                <dxtv:GridViewDataComboBoxColumn Caption="Alinhamento" FieldName="AlinhamentoCampo" VisibleIndex="21" Visible="False" Width="80px">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxtv:ListEditItem Text="Esquerda" Value="E" />
                                            <dxtv:ListEditItem Text="Direita" Value="D" />
                                            <dxtv:ListEditItem Text="Centro" Value="C" />
                                        </Items>
                                    </PropertiesComboBox>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataComboBoxColumn>
                                <dxtv:GridViewDataComboBoxColumn Caption="Hierarquia" FieldName="IndicaCampoHierarquia" Visible="False" VisibleIndex="22" Width="100px">
                                    <PropertiesComboBox>
                                        <ClientSideEvents ValueChanged="cmbIndicaCampoHierarquia_ValueChanged" />
                                        <Items>
                                            <dxtv:ListEditItem Text="Nenhum" Value="N" />
                                            <dxtv:ListEditItem Text="Chave primária" Value="P" />
                                            <dxtv:ListEditItem Text="Codigo superior" Value="S" />
                                        </Items>
                                    </PropertiesComboBox>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataComboBoxColumn>
                                <dxtv:GridViewDataSpinEditColumn Caption="Largura da coluna" FieldName="LarguraColuna" Visible="False" VisibleIndex="24" Width="75px">
                                    <PropertiesSpinEdit DisplayFormatString="g" MaxValue="32767" MinValue="1" NumberType="Integer">
                                    </PropertiesSpinEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataSpinEditColumn>
                                <dxtv:GridViewDataCheckColumn Caption="Coluna fixa?" FieldName="IndicaColunaFixa" Visible="False" VisibleIndex="26" Width="75px">
                                    <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N" AllowGrayedByClick="False">
                                    </PropertiesCheckEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxtv:GridViewDataCheckColumn>
                                <dxtv:GridViewDataComboBoxColumn Caption="Campo Relacionado Sub-Lista" FieldName="unbound_CampoSubLista" Visible="False" VisibleIndex="23" Width="200px" UnboundType="Integer">
                                    <PropertiesComboBox DataSourceID="sdsCamposSubLista" TextField="NomeCampo" ValueField="CodigoCampo" ValueType="System.Int32">
                                    </PropertiesComboBox>
                                </dxtv:GridViewDataComboBoxColumn>
                            </Columns>
                            <Styles>
                                <Header Wrap="True">
                                </Header>
                            </Styles>
                            <Templates>
                                <StatusBar>
                                    <table style="margin-left: auto">
                                        <tr>
                                            <td>
                                                <dxtv:ASPxHyperLink ID="hlSave" runat="server" Cursor="pointer"
                                                     Font-Underline="True" Text="Finalizar configuração de campos">
                                                    <ClientSideEvents Click="function(s, e){ gvDados.UpdateEdit(); }" />
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                            <td style="padding-left: 10px">
                                                <dxtv:ASPxHyperLink ID="hlCancel" runat="server" Cursor="pointer"
                                                     Font-Underline="True" Text="Cancelar">
                                                    <ClientSideEvents Click="function(s, e){ if(confirm('Tem certeza que deseja realizar essa ação? Todos os dados não salvos serão perdidos.')){gvDados.CancelEdit(); window.top.fechaModal2(); }}" />
                                                </dxtv:ASPxHyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </StatusBar>
                            </Templates>
                        </dxcp:ASPxGridView>
                        <asp:SqlDataSource ID="dataSource" runat="server" DeleteCommand="DELETE FROM [ListaCampo] WHERE [CodigoCampo] = @CodigoCampo" InsertCommand="INSERT INTO [ListaCampo] ([CodigoLista], [NomeCampo], [TituloCampo], [OrdemCampo], [OrdemAgrupamentoCampo], [TipoCampo], [Formato], [IndicaAreaFiltro], [TipoFiltro], [IndicaAgrupamento], [TipoTotalizador], [IndicaAreaDado], [IndicaAreaColuna], [IndicaAreaLinha], [AreaDefault], [IndicaCampoVisivel], [IndicaCampoControle], [IniciaisCampoControlado], [IndicaLink], [AlinhamentoCampo], [IndicaCampoHierarquia], [LarguraColuna], [TituloColunaAgrupadora], [IndicaColunaFixa]) VALUES (@CodigoLista, @NomeCampo, @TituloCampo, @OrdemCampo, @OrdemAgrupamentoCampo, @TipoCampo, @Formato, @IndicaAreaFiltro, @TipoFiltro, @IndicaAgrupamento, @TipoTotalizador, @IndicaAreaDado, @IndicaAreaColuna, @IndicaAreaLinha, @AreaDefault, @IndicaCampoVisivel, @IndicaCampoControle, @IniciaisCampoControlado, @IndicaLink, @AlinhamentoCampo, @IndicaCampoHierarquia, @LarguraColuna, @TituloColunaAgrupadora, @IndicaColunaFixa)" SelectCommand="SELECT * FROM [ListaCampo] WHERE ([CodigoLista] = @CodigoLista) ORDER BY [OrdemCampo], [NomeCampo]" UpdateCommand="UPDATE [ListaCampo] SET [CodigoLista] = @CodigoLista, [NomeCampo] = @NomeCampo, [TituloCampo] = @TituloCampo, [OrdemCampo] = @OrdemCampo, [OrdemAgrupamentoCampo] = @OrdemAgrupamentoCampo, [TipoCampo] = @TipoCampo, [Formato] = @Formato, [IndicaAreaFiltro] = @IndicaAreaFiltro, [TipoFiltro] = @TipoFiltro, [IndicaAgrupamento] = @IndicaAgrupamento, [TipoTotalizador] = @TipoTotalizador, [IndicaAreaDado] = @IndicaAreaDado, [IndicaAreaColuna] = @IndicaAreaColuna, [IndicaAreaLinha] = @IndicaAreaLinha, [AreaDefault] = @AreaDefault, [IndicaCampoVisivel] = @IndicaCampoVisivel, [IndicaCampoControle] = @IndicaCampoControle, [IniciaisCampoControlado] = @IniciaisCampoControlado, [IndicaLink] = @IndicaLink, [AlinhamentoCampo] = @AlinhamentoCampo, [IndicaCampoHierarquia] = @IndicaCampoHierarquia, [LarguraColuna] = @LarguraColuna, [TituloColunaAgrupadora] = @TituloColunaAgrupadora, [IndicaColunaFixa] = @IndicaColunaFixa WHERE [CodigoCampo] = @CodigoCampo">
                            <DeleteParameters>
                                <asp:Parameter Name="CodigoCampo" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="CodigoLista" Type="Int32" />
                                <asp:Parameter Name="NomeCampo" Type="String" />
                                <asp:Parameter Name="TituloCampo" Type="String" />
                                <asp:Parameter Name="OrdemCampo" Type="Int16" />
                                <asp:Parameter Name="OrdemAgrupamentoCampo" Type="Int16" />
                                <asp:Parameter Name="TipoCampo" Type="String" />
                                <asp:Parameter Name="Formato" Type="String" />
                                <asp:Parameter Name="IndicaAreaFiltro" Type="String" />
                                <asp:Parameter Name="TipoFiltro" Type="String" />
                                <asp:Parameter Name="IndicaAgrupamento" Type="String" />
                                <asp:Parameter Name="TipoTotalizador" Type="String" />
                                <asp:Parameter Name="IndicaAreaDado" Type="String" />
                                <asp:Parameter Name="IndicaAreaColuna" Type="String" />
                                <asp:Parameter Name="IndicaAreaLinha" Type="String" />
                                <asp:Parameter Name="AreaDefault" Type="String" />
                                <asp:Parameter Name="IndicaCampoVisivel" Type="String" />
                                <asp:Parameter Name="IndicaCampoControle" Type="String" />
                                <asp:Parameter Name="IniciaisCampoControlado" Type="String" />
                                <asp:Parameter Name="IndicaLink" Type="String" />
                                <asp:Parameter Name="AlinhamentoCampo" Type="String" />
                                <asp:Parameter Name="IndicaCampoHierarquia" Type="String" />
                                <asp:Parameter Name="LarguraColuna" Type="Int16" />
                                <asp:Parameter Name="TituloColunaAgrupadora" Type="String" />
                                <asp:Parameter Name="IndicaColunaFixa" Type="String" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:SessionParameter Name="CodigoLista" SessionField="cl" Type="Int32" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="CodigoLista" Type="Int32" />
                                <asp:Parameter Name="NomeCampo" Type="String" />
                                <asp:Parameter Name="TituloCampo" Type="String" />
                                <asp:Parameter Name="OrdemCampo" Type="Int16" />
                                <asp:Parameter Name="OrdemAgrupamentoCampo" Type="Int16" />
                                <asp:Parameter Name="TipoCampo" Type="String" />
                                <asp:Parameter Name="Formato" Type="String" />
                                <asp:Parameter Name="IndicaAreaFiltro" Type="String" />
                                <asp:Parameter Name="TipoFiltro" Type="String" />
                                <asp:Parameter Name="IndicaAgrupamento" Type="String" />
                                <asp:Parameter Name="TipoTotalizador" Type="String" />
                                <asp:Parameter Name="IndicaAreaDado" Type="String" />
                                <asp:Parameter Name="IndicaAreaColuna" Type="String" />
                                <asp:Parameter Name="IndicaAreaLinha" Type="String" />
                                <asp:Parameter Name="AreaDefault" Type="String" />
                                <asp:Parameter Name="IndicaCampoVisivel" Type="String" />
                                <asp:Parameter Name="IndicaCampoControle" Type="String" />
                                <asp:Parameter Name="IniciaisCampoControlado" Type="String" />
                                <asp:Parameter Name="IndicaLink" Type="String" />
                                <asp:Parameter Name="AlinhamentoCampo" Type="String" />
                                <asp:Parameter Name="IndicaCampoHierarquia" Type="String" />
                                <asp:Parameter Name="LarguraColuna" Type="Int16" />
                                <asp:Parameter Name="TituloColunaAgrupadora" Type="String" />
                                <asp:Parameter Name="IndicaColunaFixa" Type="String" />
                                <asp:Parameter Name="CodigoCampo" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="sdsCamposSubLista" runat="server" SelectCommand=" SELECT *
   FROM(
         SELECT CodigoCampo, 
                NomeCampo
           FROM ListaCampo 
          WHERE (CodigoLista = @CodigoSubLista) 
        UNION
         SELECT NULL,
                NULL) AS rs
  ORDER BY 
        rs.NomeCampo">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="CodigoSubLista" QueryStringField="csl" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
