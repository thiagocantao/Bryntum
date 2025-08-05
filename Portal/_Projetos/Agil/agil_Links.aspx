<%@ Page Language="C#" AutoEventWireup="true" CodeFile="agil_Links.aspx.cs" Inherits="_Projetos_Agil_agil_Links" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .conteudo {
            padding: 5px 10px
        }

        .grid-links {
            padding-top: 10px;
        }
    </style>
    <script>
        function LimparFormularioVinculacaoItem() {
            cmbProjetosTfs.SetValue(null);
            seWorkItemId.SetValue(null);
        }

        function RemoverVinculoItem() {
            window.top.mostraConfirmacao('Deseja remover o vínculo do item?', function () { callback.PerformCallback('remover'); }, null);
        }

        function VincularItem() {
            popupVinculacaoItem.Show();
        }

        function OnCallbackComplete(s, e) {
            var result = eval("(" + e.result + ")");
            if (result.Sucesso) {
                window.top.mostraMensagem((result.Mensagem) ? result.Mensagem : 'Operação realizada com sucesso', 'sucesso', false, false, null);
                if ((e.parameter) && e.parameter.indexOf('vincular') > -1)
                    popupVinculacaoItem.Hide();
                gvLinks.Refresh();
            }
            else
                window.top.mostraMensagem((result.Mensagem) ? result.Mensagem : 'Ocorreu uma falha ao realizar a operação', 'erro', true, false, null);
        }

        function ConfirmarVinculo(s, e) {

            if (ASPxClientEdit.ValidateGroup("form-vinculo")) {
                var value = seWorkItemId.GetValue();
                var acao = (value) ? 'vincular' : 'criar-e-vincular';
                callback.PerformCallback(acao + ';' + value);
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="conteudo">
            <table style="width: 100%">
                <tr>
                    <td>
                        <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Título"></dxcp:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxcp:ASPxTextBox ID="txtTituloItem" runat="server" Width="100%" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#EBEBEB">
                            </ReadOnlyStyle>
                        </dxcp:ASPxTextBox>
                    </td>
                </tr>
            </table>
            <div id="grid-links">
                <dxcp:ASPxGridView ID="gvLinks" Width="100%" runat="server" EnableTheming="True" Theme="MaterialCompact" AutoGenerateColumns="False" OnLoad="gvLinks_Load">
                    <ClientSideEvents Init="function(s, e) {
	s.SetHeight(Math.max(0, document.documentElement.clientHeight) - 100);
}"
                        ToolbarItemClick="function(s, e) {
	var item = e.item.name;
	switch(item){
		case 'vincular-item':
            VincularItem();
		break;
		case 'remover-item':
            RemoverVinculoItem();
		break;
	}
}" />
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Visible" ShowColumnHeaders="False" />
                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                    <SettingsPopup>
                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                    </SettingsPopup>
                    <Columns>
                        <dxtv:GridViewDataTextColumn FieldName="Tipo" ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataHyperLinkColumn FieldName="Url" ShowInCustomizationForm="True" VisibleIndex="1">
                            <PropertiesHyperLinkEdit TextField="Titulo" Target="_blank">
                            </PropertiesHyperLinkEdit>
                        </dxtv:GridViewDataHyperLinkColumn>
                    </Columns>
                    <Toolbars>
                        <dxtv:GridViewToolbar>
                            <Items>
                                <dxtv:GridViewToolbarItem DisplayMode="Image" Text="Vincular" ToolTip="Vincular Work Item" Name="vincular-item">
                                    <Image IconID="iconbuilder_actions_hyperlink_svg_gray_16x16">
                                    </Image>
                                </dxtv:GridViewToolbarItem>
                                <dxtv:GridViewToolbarItem DisplayMode="Image" Text="Remover vínculo" ToolTip="Remover vínculo com Work Item" Name="remover-item">
                                    <Image IconID="iconbuilder_actions_trash_svg_gray_16x16">
                                    </Image>
                                </dxtv:GridViewToolbarItem>
                                <dxtv:GridViewToolbarItem DisplayMode="Image" Text="Atualizar" ToolTip="Atualizar" Command="Refresh">
                                    <Image IconID="iconbuilder_actions_refresh_svg_gray_16x16">
                                    </Image>
                                </dxtv:GridViewToolbarItem>
                            </Items>
                        </dxtv:GridViewToolbar>
                    </Toolbars>
                    <StylesToolbar>
                        <Style>
                            <Paddings Padding="0px" PaddingBottom="3px" />
                        </Style>
                    </StylesToolbar>
                </dxcp:ASPxGridView>
            </div>
        </div>
        <dxcp:ASPxPopupControl ID="popupVinculacaoItem" runat="server" ClientInstanceName="popupVinculacaoItem" HeaderText="Vinculação do Item" Theme="MaterialCompact" Width="400px" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ClientSideEvents CloseUp="function(s, e) {
	LimparFormularioVinculacaoItem();
}" />
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">
                    <dxtv:ASPxFormLayout ID="formLayout" runat="server" EnableTheming="True" NestedControlWidth="100%" Theme="MaterialCompact" Width="100%">
                        <Items>
                            <dxtv:LayoutItem Caption="Projeto" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dxtv:LayoutItemNestedControlContainer runat="server">
                                        <dxtv:ASPxComboBox ID="cmbProjetosTfs" runat="server" Width="100%" ClientInstanceName="cmbProjetosTfs" TextField="Name" ValueField="Id">
                                            <ValidationSettings ValidationGroup="form-vinculo">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxComboBox>
                                    </dxtv:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dxtv:LayoutItem>
                            <dxtv:LayoutItem Caption="Informe o número do Workitem no Azure/TFS ou deixe em branco para criar um" ColSpan="1">
                                <LayoutItemNestedControlCollection>
                                    <dxtv:LayoutItemNestedControlContainer runat="server">
                                        <dxtv:ASPxSpinEdit ID="formLayout_E4" runat="server" Width="100%" ClientInstanceName="seWorkItemId" NumberType="Integer">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                            <ValidationSettings ValidationGroup="form-vinculo">
                                            </ValidationSettings>
                                        </dxtv:ASPxSpinEdit>
                                    </dxtv:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dxtv:LayoutItem>
                            <dxtv:LayoutGroup AlignItemCaptions="False" ColCount="3" ColSpan="1" ColumnCount="3" ShowCaption="False" VerticalAlign="Bottom">
                                <GroupBoxStyle Border-BorderStyle="None">
                                </GroupBoxStyle>
                                <Items>
                                    <dxtv:EmptyLayoutItem ColSpan="1" Width="90%">
                                    </dxtv:EmptyLayoutItem>
                                    <dxtv:LayoutItem ColSpan="1" HorizontalAlign="Right">
                                        <LayoutItemNestedControlCollection>
                                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                                <dxtv:ASPxButton ID="ASPxFormLayout1_E3" runat="server" AutoPostBack="False" Text="Confirmar" Width="90px">
                                                    <ClientSideEvents Click="ConfirmarVinculo" />
                                                </dxtv:ASPxButton>
                                            </dxtv:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dxtv:LayoutItem>
                                    <dxtv:LayoutItem ColSpan="1" HorizontalAlign="Right">
                                        <LayoutItemNestedControlCollection>
                                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                                <dxtv:ASPxButton ID="ASPxFormLayout1_E4" runat="server" AutoPostBack="False" Text="Cancelar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	popupVinculacaoItem.Hide();
}" />
                                                </dxtv:ASPxButton>
                                            </dxtv:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dxtv:LayoutItem>
                                </Items>
                                <SettingsItems HorizontalAlign="Right" ShowCaption="False" />
                                <ParentContainerStyle>
                                    <Paddings Padding="0px" />
                                </ParentContainerStyle>
                            </dxtv:LayoutGroup>
                        </Items>
                        <SettingsItemCaptions Location="Top" />
                    </dxtv:ASPxFormLayout>
                </dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>
        <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents CallbackComplete="OnCallbackComplete" />
        </dxcp:ASPxCallback>
    </form>
</body>
</html>
