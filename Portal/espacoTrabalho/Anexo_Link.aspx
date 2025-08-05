<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Anexo_Link.aspx.cs" Inherits="espacoTrabalho_Anexo_Link" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 10px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="auto-style1">
            <tr>
                <td>
                    <dxcp:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0"  Width="100%">
                        <TabPages>
                            <dxtv:TabPage Name="tabProjetos" Text="Projetos">
                                <ContentCollection>
                                    <dxtv:ContentControl runat="server">
                                        <dxtv:ASPxGridView ID="gvProjetos" runat="server" AutoGenerateColumns="False"  KeyFieldName="CodigoObjeto" Width="100%">
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <Settings VerticalScrollableHeight="400" VerticalScrollBarMode="Auto" />
                                            <SettingsCommandButton>
                                                <ShowAdaptiveDetailButton RenderMode="Image">
                                                </ShowAdaptiveDetailButton>
                                                <HideAdaptiveDetailButton RenderMode="Image">
                                                </HideAdaptiveDetailButton>
                                            </SettingsCommandButton>
                                            <Columns>
                                                <dxtv:GridViewDataTextColumn Caption="Nome Projeto" FieldName="NomeObjeto" ShowInCustomizationForm="True" VisibleIndex="1">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewCommandColumn SelectAllCheckboxMode="AllPages" ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="50px">
                                                </dxtv:GridViewCommandColumn>
                                            </Columns>
                                        </dxtv:ASPxGridView>
                                    </dxtv:ContentControl>
                                </ContentCollection>
                            </dxtv:TabPage>
                            <dxtv:TabPage Name="tabConsultores" Text="Consultores">
                                <ContentCollection>
                                    <dxtv:ContentControl runat="server">
                                        <dxtv:ASPxGridView ID="gvConsultores" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvConsultores"  KeyFieldName="CodigoObjeto" Width="100%">
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <Settings VerticalScrollableHeight="400" VerticalScrollBarMode="Auto" />
                                            <SettingsCommandButton>
                                                <ShowAdaptiveDetailButton RenderMode="Image">
                                                </ShowAdaptiveDetailButton>
                                                <HideAdaptiveDetailButton RenderMode="Image">
                                                </HideAdaptiveDetailButton>
                                            </SettingsCommandButton>
                                            <Columns>
                                                <dxtv:GridViewDataTextColumn Caption="Nome Consultor" FieldName="NomeObjeto" ShowInCustomizationForm="True" VisibleIndex="1">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewCommandColumn SelectAllCheckboxMode="AllPages" ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" Width="50px">
                                                </dxtv:GridViewCommandColumn>
                                            </Columns>
                                        </dxtv:ASPxGridView>
                                    </dxtv:ContentControl>
                                </ContentCollection>
                            </dxtv:TabPage>
                        </TabPages>
                        <ContentStyle>
                            <Paddings Padding="5px" />
                        </ContentStyle>
                    </dxcp:ASPxPageControl>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
	window.top.fechaModal3();
}
" />
                    </dxcp:ASPxCallback>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table>
                        <tr>
                            <td style="padding-right: 10px">
                                <dxcp:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False"  Text="Salvar" Width="100px">
                                    <ClientSideEvents Click="function(s, e) {
	callback.PerformCallback();
}" />
                                </dxcp:ASPxButton>
                            </td>
                            <td>
                                <dxcp:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False"  Text="Fechar" Width="100px">
                                    <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal3();
}
" />
                                </dxcp:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
