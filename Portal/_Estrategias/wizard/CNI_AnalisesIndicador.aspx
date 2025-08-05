<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CNI_AnalisesIndicador.aspx.cs"
    Inherits="_Estrategias_objetivoEstrategico_ObjetivoEstrategico_Analises" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style3
        {
            height: 9px;
        }
        .style4
        {
            height: 5px;
        }
        .style5
        {
            height: 7px;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                </td>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%" OnCallback="pnCallback_Callback">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoAnalisePerformance"
                                    AutoGenerateColumns="False" Width="100%" 
                                    ID="grid" OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s, true);
}" CustomButtonClick="function(s, e) {
	if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
        hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
}"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="70px" Caption=" " VisibleIndex="0">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton Text="Excluir" ID="btnExcluir">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <HeaderTemplate>
                                                <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>",@"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>")%>
                                            </HeaderTemplate>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataDateColumn FieldName="DataAnalise" Width="80px" Caption="Data"
                                            VisibleIndex="1">
                                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" Width="100px">
                                            </PropertiesDateEdit>
                                            <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataMemoColumn FieldName="Analise" Name="txtAnaliseForm" Caption="Tendências"
                                            VisibleIndex="3">
                                            <PropertiesMemoEdit Rows="10" Height="100px">
                                                <ClientSideEvents KeyUp="function(s, e) {
	limitaASPxMemo(s, 2000);
}"></ClientSideEvents>
                                                <ValidationSettings CausesValidation="True">
                                                    <RequiredField IsRequired="True" ErrorText="Campo Obrigat&#243;rio!"></RequiredField>
                                                </ValidationSettings>
                                            </PropertiesMemoEdit>
                                            <EditFormSettings ColumnSpan="2" RowSpan="3" Visible="True" VisibleIndex="0" CaptionLocation="Top"
                                                Caption="An&#225;lise:"></EditFormSettings>
                                        </dxwgv:GridViewDataMemoColumn>
                                        <dxwgv:GridViewDataMemoColumn FieldName="Recomendacoes" Name="txtRecomendacoesForm"
                                            Caption="Recomenda&#231;&#245;es" VisibleIndex="6" Visible="False">
                                            <PropertiesMemoEdit Rows="10" Height="100px">
                                                <ClientSideEvents KeyUp="function(s, e) {
	limitaASPxMemo(s, 2000);
}"></ClientSideEvents>
                                                <ValidationSettings CausesValidation="True" ErrorText="*">
                                                </ValidationSettings>
                                            </PropertiesMemoEdit>
                                            <EditFormSettings ColumnSpan="2" RowSpan="2" Visible="True" VisibleIndex="1" CaptionLocation="Top"
                                                Caption="Recomenda&#231;&#245;es:"></EditFormSettings>
                                        </dxwgv:GridViewDataMemoColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Responsavel" VisibleIndex="2" Caption="Responsável"
                                            ReadOnly="True">
                                            <PropertiesTextEdit EncodeHtml="False">
                                                <ReadOnlyStyle BackColor="#EBEBEB">
                                                </ReadOnlyStyle>
                                            </PropertiesTextEdit>
                                            <EditFormSettings Caption="Incluído Por:" CaptionLocation="Top" Visible="False">
                                            </EditFormSettings>
                                            <CellStyle >
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption=" " FieldName="CorApresentacao" ShowInCustomizationForm="True"
                                            VisibleIndex="4" Width="35px">
                                            <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.gif' /&gt;">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoCorStatusObjetoAssociado" ShowInCustomizationForm="True"
                                            Visible="False" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                    </SettingsPager>
                                    <SettingsEditing Mode="PopupEditForm">
                                    </SettingsEditing>
                                    <SettingsPopup>
                                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                            AllowResize="True" Width="600px" />
                                    </SettingsPopup>
                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="180"></Settings>
                                </dxwgv:ASPxGridView>
                                <dxpc:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                    CloseAction="None"  HeaderText="Análises"
                                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                    Width="800px">
                                    <ContentStyle>
                                        <Paddings PaddingBottom="8px" />
                                    </ContentStyle>
                                    <HeaderStyle Font-Bold="True" />
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                                            <table cellpadding="0" cellspacing="0" class="style2">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                            Text="Tendências:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txtAnalise" runat="server" ClientInstanceName="txtAnalise"
                                                            Rows="10" Width="100%">
                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </ReadOnlyStyle>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style3">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                            Text="Agenda:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo ID="txtRecomendacoes" runat="server" ClientInstanceName="txtRecomendacoes"
                                                             Rows="10" Width="100%">
                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </ReadOnlyStyle>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                            Text="Status:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxComboBox ID="ddlStatus" runat="server" ClientInstanceName="ddlStatus"
                                                            Width="100%">
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="lblInclusao" runat="server" ClientInstanceName="lblInclusao"
                                                           >
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                                             Text="Salvar" Width="100px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                            <Paddings Padding="0px" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                             Text="Fechar" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                            <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                                                PaddingTop="0px" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxpc:PopupControlContentControl>
                                    </ContentCollection>
                                </dxpc:ASPxPopupControl>
                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <dxpc:ASPxPopupControl ID="pcUsuarioIncluido" runat="server" ClientInstanceName="pcUsuarioIncluido"
                                     HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
                                    Width="270px">
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td align="center" style="">
                                                        </td>
                                                        <td align="center" rowspan="3" style="width: 70px">
                                                            <dxe:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar" ImageAlign="TextTop"
                                                                ImageUrl="~/imagens/Workflow/salvarBanco.png">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <dxe:ASPxLabel ID="lblAcaoGravacao" runat="server" ClientInstanceName="lblAcaoGravacao"
                                                                >
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
                        <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();	

	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Estrat&#233;gia inclu&#237;da com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Dados gravados com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Estrat&#233;gia exclu&#237;da com sucesso!&quot;);
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
