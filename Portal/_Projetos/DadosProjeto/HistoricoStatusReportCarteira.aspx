<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoricoStatusReportCarteira.aspx.cs"
    Inherits="_Projetos_DadosProjeto_HistoricoStatusReportCarteira" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">        function atualizaTela() {  } </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="width: 12px">
            </td>
        </tr>
        <tr>
            <td style="width: 10px;">
            </td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback" HideContentOnCallback="False">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
                                            ClientInstanceName="gvDados"  
                                            KeyFieldName="CodigoModeloStatusReport" 
                                            OnCustomCallback="gvDados_CustomCallback" Width="100%">
                                            <ClientSideEvents CustomButtonClick="function(s, e) 
{
	onClick_CustomButtomGrid(s, e);
}
" />
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="Ação" Name="Acao" 
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="1" Width="130px">
                                                    <CustomButtons>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnVisualizar" 
                                                            Text="Visualizar Relatório">
                                                            <Image Url="~/imagens/menuExportacao/iconoPDF.png">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Relatórios" FieldName="NomeRelatorio" 
                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Gerado em" FieldName="DataGeracao" 
                                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                    </PropertiesTextEdit>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoStatusReport" 
                                                    Name="CodigoStatusReport" ShowInCustomizationForm="True" Visible="False" 
                                                    VisibleIndex="18">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoModeloStatusReport" 
                                                    Name="CodigoModeloStatusReport" ShowInCustomizationForm="True" Visible="False" 
                                                    VisibleIndex="19">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Modelo de Relatório" 
                                                    FieldName="DescricaoModeloStatusReport" GroupIndex="0" 
                                                    ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending" 
                                                    VisibleIndex="20">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" 
                                                    Width="60px">
                                                    <DataItemTemplate>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <dxe:ASPxButton ID="btnDownLoad" runat="server"  
                                                                            BackColor="Transparent" Height="16px" ImageSpacing="0px" 
                                                                            OnClick="ASPxButton1_Click" ToolTip="Visualizar o arquivo" Width="16px" 
                                                                            Wrap="False">
                                                                            <Image Url="~/imagens/menuExportacao/iconoPDF.png">
                                                                            </Image>
                                                                            <FocusRectPaddings Padding="0px" />
                                                                            <FocusRectBorder BorderColor="Transparent" BorderStyle="None" />
                                                                            <Border BorderStyle="None" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td align="center" valign="middle">
                                                                        <dxe:ASPxButton ID="btnExcluir" runat="server" AutoPostBack="False" 
                                                                            BackColor="Transparent" ClientInstanceName="btnExcluir" Height="16px" 
                                                                            ImageSpacing="0px" onload="Button_Load" ToolTip="Excluir" Width="16px" 
                                                                            Wrap="False">
                                                                            <image url="~/imagens/botoes/excluirReg02.PNG">
                                                                            </image>
                                                                            <FocusRectPaddings Padding="0px" />
                                                                            <FocusRectBorder BorderColor="Transparent" BorderStyle="None" />
                                                                            <Border BorderStyle="None" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </DataItemTemplate>
                                                    <HeaderTemplate>
                                                        <%# ObtemHtmlBtnAdicionarStatusReport() %>
                                                    </HeaderTemplate>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="IniciaisModeloControladoSistema" 
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="23">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" AllowSort="False" 
                                                AutoExpandAllGroups="True" />
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" />
                                            <SettingsText  />
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-top: 10px;">
                                        <dxe:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar" 
                                            Text="Fechar" Width="120px">
                                            <ClientSideEvents Click="function(s, e) {
	try{
		window.top.fechaModal();
	}
	catch(e){
		window.close();
	}
}" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            &nbsp;
                            <dxpc:ASPxPopupControl ID="pcSelecaoModeloStatusReport" runat="server" AllowDragging="True"
                                ClientInstanceName="pcSelecaoModeloStatusReport" CloseAction="None"
                                HeaderText="Gerar Relatório de Status" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="500px">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                        <table cellpadding="0" cellspacing="0" class="style1" width="100%">
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                        Text="Modelo do Relatório de Status:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxComboBox ID="ddlModeloStatusReport" runat="server" ClientInstanceName="ddlModeloStatusReport"
                                                         ValueType="System.Int32" Width="100%">
                                                    </dxe:ASPxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="padding-top: 10px">
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 10px">
                                                                <dxe:ASPxButton ID="btnSalvar_MSR" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                     Text="Gerar Relatório" Width="120px">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(onClick_btnSalvar_MSR)
		onClick_btnSalvar_MSR();
}" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxButton ID="btnCancelar_MSR" runat="server" AutoPostBack="False"
                                                                    Text="Cancelar" Width="120px">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(onClick_btnCancelar_MSR)
		onClick_btnCancelar_MSR();
}" />
                                                                    <Paddings Padding="0px" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                            <br />
                            <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackMensagem" Width="640px"
                                ID="pnCallbackMensagem" OnCallback="pnCallbackMensagem_Callback">
                                <ClientSideEvents EndCallback="function(s, e) {
	//local_onEnd_pnCallback(s,e);
}"></ClientSideEvents>
                                <PanelCollection>
                                    <dxp:PanelContent ID="PanelContent2" runat="server">
                                        &nbsp;<dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                        </dxhf:ASPxHiddenField>
                                        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcOperMsg" HeaderText="Incluir a Entidade Atual"
                                            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                            ShowCloseButton="False" ShowHeader="False" Width="270px"
                                            ID="pcOperMsg">
                                            <ContentCollection>
                                                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="" align="center">
                                                                </td>
                                                                <td style="width: 70px" align="center" rowspan="3">
                                                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                                        ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                                                        ID="lblAcaoGravacao">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxpc:PopupControlContentControl>
                                            </ContentCollection>
                                        </dxpc:ASPxPopupControl>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxcp:ASPxCallbackPanel>
                            <!-- ASPxPOPUPCONTROL : Dados salvos -->
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {
	local_onEnd_pnCallback(s,e);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td style="width: 12px">
            </td>
        </tr>
    </table>
    <%# ObtemHtmlBtnAdicionarStatusReport() %>
    </form>
</body>
</html>
