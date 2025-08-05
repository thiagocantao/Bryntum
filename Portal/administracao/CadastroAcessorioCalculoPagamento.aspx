<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="CadastroAcessorioCalculoPagamento.aspx.cs" Inherits="administracao_CadastroAcessorioCalculoPagamento" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <!-- table principal -->
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
            width: 100%">
            <tr>
                <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                    height: 26px" valign="middle">
                    <table>
                        <tr>
                            <td align="center" style="width: 1px; height: 19px">
                                <span id="Span2" runat="server"></span>
                            </td>
                            <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                                <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                    Text="Cadastro de Acessórios" ClientInstanceName="lblTitulo">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <!-- ASPxGRidVIEW: gvDados -->
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                        DataSourceID="sdsAcessorio"  KeyFieldName="CodigoAcessorio"
                        Width="99%" OnRowInserting="gvDados_RowInserting" OnRowUpdating="gvDados_RowUpdating">
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="75px" ShowEditButton="true"
                                ShowDeleteButton="true">
                                <HeaderTemplate>
                                    <%# ObtemBtnIncluir() %>
                                </HeaderTemplate>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoAcessorio" ReadOnly="True" VisibleIndex="1"
                                Visible="False">
                                <EditFormSettings Visible="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoAcessorio" VisibleIndex="2" Caption="Acessório"
                                Width="100%">
                                <PropertiesTextEdit MaxLength="64">
                                    <ValidationSettings Display="Dynamic">
                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                    </ValidationSettings>
                                    <Style >
                                        
                                    </Style>
                                </PropertiesTextEdit>
                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataSpinEditColumn Caption="Alíquota" FieldName="Aliquota" VisibleIndex="3"
                                Width="90px">
                                <PropertiesSpinEdit ClientInstanceName="txtAliquota" DisplayFormatString="{0:n3}"
                                    NumberFormat="Custom">
                                    <ClientSideEvents GotFocus="function(s, e) {
	txtValor.SetValue(null);
}" />
                                    <ValidationSettings Display="Dynamic">
                                    </ValidationSettings>
                                    <Style >
                                        
                                    </Style>
                                </PropertiesSpinEdit>
                                <EditFormSettings CaptionLocation="Top" />
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn Caption="Valor" FieldName="Valor" VisibleIndex="4"
                                Width="90px">
                                <PropertiesSpinEdit ClientInstanceName="txtValor" DisplayFormatString="{0:n2}" NumberFormat="Custom">
                                    <ClientSideEvents GotFocus="function(s, e) {
	txtAliquota.SetValue(null);
}" />
                                    <ValidationSettings Display="Dynamic">
                                    </ValidationSettings>
                                    <Style >
                                        
                                    </Style>
                                </PropertiesSpinEdit>
                                <EditFormSettings CaptionLocation="Top" />
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="Tipo" VisibleIndex="5" Caption="Tipo"
                                Width="110px">
                                <PropertiesComboBox>
                                    <Items>
                                        <dxe:ListEditItem Text="Desconto" Value="Desconto" />
                                        <dxe:ListEditItem Text="Acréscimo" Value="Acréscimo" />
                                    </Items>
                                    <ValidationSettings Display="Dynamic">
                                        <RequiredField ErrorText="Campo Obrigatório" />
                                    </ValidationSettings>
                                    <Style >
                                        
                                    </Style>
                                </PropertiesComboBox>
                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataComboBoxColumn Caption="Incide Sobre ?" FieldName="IncideSobreQualValor"
                                VisibleIndex="6" Width="90px">
                                <PropertiesComboBox>
                                    <Items>
                                        <dxe:ListEditItem Text="Valor da Medição" Value="VM" />
                                        <dxe:ListEditItem Text="Valor da Nota Fiscal" Value="VNF" />
                                    </Items>
                                    <Style >
                                        
                                    </Style>
                                </PropertiesComboBox>
                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                            </dxwgv:GridViewDataComboBoxColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsEditing Mode="PopupEditForm"/>
                        <SettingsPopup>
                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                AllowResize="True" Width="400px"/>
                        </SettingsPopup>
                        <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" />
                        <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                    </dxwgv:ASPxGridView>
                    <asp:SqlDataSource ID="sdsAcessorio" runat="server" SelectCommand="SELECT [CodigoAcessorio], [DescricaoAcessorio], [Aliquota], [Valor], [Tipo], RTRIM( [IncideSobreQualValor]) AS IncideSobreQualValor FROM [AcessorioCalculoPagamento] ORDER BY [DescricaoAcessorio]"
                        DeleteCommand="DELETE FROM [AcessorioCalculoPagamento] WHERE [CodigoAcessorio] = @CodigoAcessorio"
                        InsertCommand="INSERT INTO [AcessorioCalculoPagamento]
              ([DescricaoAcessorio],
                [Aliquota],
                [Valor],
                [Tipo],
                [IncideSobreQualValor] )
VALUES(
               @DescricaoAcessorio, 
               @Aliquota, 
               @Valor, 
               @Tipo,
               @IncideSobreQualValor)" UpdateCommand="UPDATE [AcessorioCalculoPagamento]
        SET  [DescricaoAcessorio] = @DescricaoAcessorio, 
                [Aliquota] = @Aliquota, 
                [Valor]  = @Valor, 
                [Tipo] = @Tipo, 
                [IncideSobreQualValor] = @IncideSobreQualValor
WHERE  [CodigoAcessorio] = @CodigoAcessorio">
                        <DeleteParameters>
                            <asp:Parameter Name="CodigoAcessorio" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="DescricaoAcessorio" />
                            <asp:Parameter Name="Aliquota" />
                            <asp:Parameter Name="Valor" />
                            <asp:Parameter Name="Tipo" />
                            <asp:Parameter Name="IncideSobreQualValor" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="DescricaoAcessorio" />
                            <asp:Parameter Name="Aliquota" />
                            <asp:Parameter Name="Valor" />
                            <asp:Parameter Name="Tipo" />
                            <asp:Parameter Name="CodigoAcessorio" />
                            <asp:Parameter Name="IncideSobreQualValor" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
