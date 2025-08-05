<%@ Page Language="C#" AutoEventWireup="true" CodeFile="produtosTAI.aspx.cs" Inherits="_Projetos_DadosProjeto_produtosTAI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" class="style1">
        <tr>
            <td style="width: 5px">
                &nbsp;
            </td>
            <td align="right">
                <table>
                    <tr>
                        <td>
                            <dxe:ASPxButton ID="btnSalvar" runat="server" 
                                Text="Salvar" AutoPostBack="False" Width="90px" ClientEnabled="False" ClientInstanceName="btnSalvar">
                                <ClientSideEvents Click="function(s, e) {
	//btnSalvar_Click(s, e);
	//btnImprimir.SetEnabled(true);
	//btnImprimir0.SetEnabled(true);
}" />
                            </dxe:ASPxButton>
                        </td>
                        <td style="padding-left: 10px">
                            <dxe:ASPxButton ID="btnImprimir" runat="server" ClientInstanceName="btnImprimir"
                                 Text="Imprimir" AutoPostBack="False" ClientEnabled="False"
                                Width="90px">
                                <ClientSideEvents Click="function(s, e) {
	//btnImprimir_Click(s, e);
}" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <dxtc:ASPxPageControl ID="pageControl" runat="server" ActiveTabIndex="0" ClientInstanceName="pageControl"
                     Width="100%">
                    <TabPages>
                        <dxtc:TabPage Name="tabProdutos" Text="Produtos">
                            <ContentCollection>
                                <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <div>
                                        <div id="dv01" runat="server">
                                            <dxwgv:ASPxGridView ID="gvProdutosAcao" runat="server" AutoGenerateColumns="False"
                                                ClientInstanceName="gvProdutosAcao"  KeyFieldName="SequenciaRegistro"
                                                Width="100%" OnAutoFilterCellEditorInitialize="gvProdutosAcao_AutoFilterCellEditorInitialize"
                                                OnRowUpdating="gvProdutosAcao_RowUpdating">
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                        Width="50px" ShowEditButton="true">
                                                    </dxwgv:GridViewCommandColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Descrição do Produto" FieldName="DescricaoProduto"
                                                        ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        <PropertiesTextEdit MaxLength="500">
                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </ReadOnlyStyle>
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                            </ValidationSettings>
                                                            <Style >
                                                                
                                                            </Style>
                                                        </PropertiesTextEdit>
                                                        <EditFormSettings CaptionLocation="Top" ColumnSpan="2" VisibleIndex="1" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataSpinEditColumn Caption="Qtde" FieldName="Quantidade" ReadOnly="True"
                                                        ShowInCustomizationForm="True" VisibleIndex="2" Width="50px">
                                                        <PropertiesSpinEdit DecimalPlaces="4" DisplayFormatString="g" MaxValue="9999999999999.9999"
                                                            MinValue="-9999999999999.9999" NumberFormat="Custom">
                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </ReadOnlyStyle>
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                            </ValidationSettings>
                                                            <Style >
                                                                
                                                            </Style>
                                                        </PropertiesSpinEdit>
                                                        <EditFormSettings Caption="Quantidade" CaptionLocation="Top" VisibleIndex="2" />
                                                    </dxwgv:GridViewDataSpinEditColumn>
                                                    <dxwgv:GridViewDataDateColumn Caption="Data de Término" FieldName="DataLimitePrevista"
                                                        ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" PopupVerticalAlign="WindowCenter">
                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </ReadOnlyStyle>
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                            </ValidationSettings>
                                                            <Style >
                                                                
                                                            </Style>
                                                        </PropertiesDateEdit>
                                                        <EditFormSettings Caption="Data Limite" CaptionLocation="Top" VisibleIndex="3" />
                                                    </dxwgv:GridViewDataDateColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Ação Vinculada" FieldName="SequenciaAcao"
                                                        GroupIndex="0" ReadOnly="True" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending"
                                                        VisibleIndex="5" Width="200px">
                                                        <PropertiesComboBox>
                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </ReadOnlyStyle>
                                                            <Style >
                                                                
                                                            </Style>
                                                        </PropertiesComboBox>
                                                        <EditFormSettings CaptionLocation="Top" ColumnSpan="2" VisibleIndex="0" />
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Sequencia" FieldName="SequenciaRegistro" ReadOnly="True"
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                        <PropertiesTextEdit>
                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </ReadOnlyStyle>
                                                            <Style >
                                                                
                                                            </Style>
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataDateColumn Caption="Data Real" FieldName="DataReal" ShowInCustomizationForm="True"
                                                        VisibleIndex="6">
                                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" DisplayFormatInEditMode="True">
                                                            <Style >
                                                                
                                                            </Style>
                                                        </PropertiesDateEdit>
                                                    </dxwgv:GridViewDataDateColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn Caption="Situação" FieldName="CodigoSituacaoProduto"
                                                        ShowInCustomizationForm="True" VisibleIndex="7">
                                                        <PropertiesComboBox DataSourceID="sdsTipoSituacaoProduto" TextField="DescricaoSituacaoProduto"
                                                            ValueField="CodigoSituacaoProduto">
                                                            <ItemStyle >
                                                                <SelectedStyle >
                                                                </SelectedStyle>
                                                            </ItemStyle>
                                                            <Style >
                                                                
                                                            </Style>
                                                        </PropertiesComboBox>
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" ConfirmDelete="True" />
                                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                </SettingsPager>
                                                <SettingsEditing Mode="Inline" />
                                                <SettingsPopup>
                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                        AllowResize="True" Width="600px"/>
                                                </SettingsPopup>
                                                <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                <Paddings Padding="0px" />
                                                <Styles>
                                                    <Cell >
                                                    </Cell>
                                                </Styles>
                                            </dxwgv:ASPxGridView>
                                        </div>
                                    </div>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                </dxtc:ASPxPageControl>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
    </dxhf:ASPxHiddenField>
    <asp:SqlDataSource ID="sdsTipoSituacaoProduto" runat="server" SelectCommand="SELECT CodigoSituacaoProduto
                         ,DescricaoSituacaoProduto
                    FROM TipoSitucaoProduto"></asp:SqlDataSource>
    </form>
</body>
</html>
