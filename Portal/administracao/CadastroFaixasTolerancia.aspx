<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="CadastroFaixasTolerancia.aspx.cs" Inherits="administracao_CadastroFaixasTolerancia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="height: 26px; padding-left: 20px;"
                valign="middle">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" Text="Cadastro de faixas de tolerância">
                </dxe:ASPxLabel>
            </td>
            <td style="height: 26px; width: 10px;" valign="middle">&nbsp;
            </td>
        </tr>
    </table>
    <div id="ConteudoPrincipal">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                        Width="100%" DataSourceID="dsDados"
                        KeyFieldName="CodigoParametro">

                        <Columns>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoParametro" Visible="False" VisibleIndex="0">
                                <EditFormSettings Visible="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="TipoIndicador" Visible="False" VisibleIndex="1">
                                <EditFormSettings Visible="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="2" Width="50px"
                                ShowEditButton="true">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                                <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                            <dxwgv:GridViewDataTextColumn Caption="Indicador" FieldName="DescricaoIndicador"
                                GroupIndex="0" SortIndex="0" SortOrder="Ascending" VisibleIndex="3" ExportWidth="150">
                                <EditFormSettings Visible="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="De" FieldName="ValorInicial" VisibleIndex="4"
                                ExportWidth="150">
                                <PropertiesTextEdit ClientInstanceName="txtValorInicial" Width="100%">
                                    <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..99999&gt;.&lt;00..99&gt;" />
                                    <ValidationSettings CausesValidation="True" ErrorText="Valor inválido" Display="Dynamic">
                                        <RegularExpression ErrorText="Valor em formato inválido" ValidationExpression="\d+(\,\d{1,2})?" />
                                        <RequiredField ErrorText="Informe um valor válido" IsRequired="True" />
                                    </ValidationSettings>
                                    <Style>
                                    
                                </Style>
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False" AllowGroup="False" />
                                <EditFormSettings Caption="De" Visible="True" VisibleIndex="0" CaptionLocation="Top" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Até" FieldName="ValorFinal" VisibleIndex="5"
                                ExportWidth="150">
                                <PropertiesTextEdit ClientInstanceName="txtValorFinal" Width="100%">
                                    <ClientSideEvents Validation="function(s, e) { Valida(s,e);}" />
                                    <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..99999&gt;.&lt;00..99&gt;" />
                                    <ValidationSettings CausesValidation="True" ErrorText="Valor inválido" Display="Dynamic">
                                        <RegularExpression ErrorText="Valor em formato inválido" ValidationExpression="\d+(\,\d{1,2})?" />
                                        <RequiredField ErrorText="Informe um valor válido" IsRequired="True" />
                                    </ValidationSettings>
                                    <Style>
                                    
                                </Style>
                                </PropertiesTextEdit>
                                <Settings AllowAutoFilter="False" AllowGroup="False" />
                                <EditFormSettings Caption="Até" Visible="True" VisibleIndex="1" CaptionLocation="Top" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataComboBoxColumn Caption="Status" FieldName="CodigoTipoStatus" VisibleIndex="6"
                                Visible="False">
                                <PropertiesComboBox TextField="TipoStatus" ValueField="CodigoTipoStatus" ValueType="System.Int32"
                                    DataSourceID="dsStatus" Width="100%">
                                    <ValidationSettings CausesValidation="True" ErrorTextPosition="Bottom" Display="Dynamic">
                                    </ValidationSettings>
                                </PropertiesComboBox>
                                <EditFormSettings Caption="Status" Visible="True" VisibleIndex="2" CaptionLocation="Top" />
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="TipoStatus" VisibleIndex="7"
                                ExportWidth="150">
                                <PropertiesTextEdit Width="100%">
                                    <ValidationSettings Display="Dynamic">
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                                <EditFormSettings Visible="False" />
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" AutoExpandAllGroups="True" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm" />
                        <SettingsPopup>
                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                AllowResize="True" Width="450px" />
                        </SettingsPopup>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" />
                        <SettingsText />
                    </dxwgv:ASPxGridView>
                    <asp:SqlDataSource ID="dsDados" runat="server" ProviderName="System.Data.SqlClient"
                        SelectCommand=" SELECT p.CodigoParametro,
		p.TipoIndicador,
		CASE p.TipoIndicador
			WHEN 'FIS' THEN 'Físico'
			WHEN 'FIN' THEN 'Financeiro'
		END AS DescricaoIndicador,
		t.CodigoTipoStatus,
		t.TipoStatus,
		p.ValorInicial,
		p.ValorFinal
   FROM ParametroIndicadores p INNER JOIN
		TipoStatusAnalise t ON t.CodigoTipoStatus = p.CodigoTipoStatus
  WHERE p.TipoIndicador IN ('FIS', 'FIN')
  ORDER BY
        p.TipoIndicador,
        p.ValorInicial"
                        UpdateCommand="UPDATE ParametroIndicadores
   SET CodigoTipoStatus = @CodigoTipoStatus
      ,ValorInicial = @ValorInicial
      ,ValorFinal = @ValorFinal
 WHERE CodigoParametro = @CodigoParametro">
                        <UpdateParameters>
                            <asp:Parameter Name="CodigoTipoStatus" />
                            <asp:Parameter Name="ValorInicial" />
                            <asp:Parameter Name="ValorFinal" />
                            <asp:Parameter Name="CodigoParametro" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsStatus" runat="server"
                        ProviderName="System.Data.SqlClient"
                        SelectCommand="SELECT CodigoTipoStatus, TipoStatus FROM TipoStatusAnalise WHERE CodigoTipoStatus &lt;&gt; 0"></asp:SqlDataSource>
                    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1"
                        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                        <Styles>
                            <Default>
                            </Default>
                            <Header>
                            </Header>
                            <Cell>
                            </Cell>
                            <GroupFooter Font-Bold="True">
                            </GroupFooter>
                            <Title Font-Bold="True"></Title>
                        </Styles>
                    </dxwgv:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
