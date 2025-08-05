<%@ Page Language="C#" AutoEventWireup="true" CodeFile="seriesMeta.aspx.cs" Inherits="_Estrategias_wizard_seriesMeta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 5px;
        }
        .style2
        {
            width: 100%;
        }
        .style6
        {
            height: 10px;
        }
        .style7
        {
            width: 90px;
        }
        .style8
        {
            width: 115px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%" 
        ClientInstanceName="pnCallback" oncallback="pnCallback_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	onEnd_pnCallbackLocal(s, e);
}" />
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabelAno0" runat="server" 
                                    Text="Unidade:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxComboBox ID="ddlUnidades" runat="server" ClientInstanceName="ddlUnidades"
                                     ValueType="System.Int32" Width="100%">
                                    <ClientSideEvents EndCallback="function(s, e) {
	hfGeral.Set(&quot;CodigoUnidade&quot;, s.cp_CodigoUnidade);
    gvMetas.PerformCallback('Atualizar');
}" SelectedIndexChanged="function(s, e) {
	hfGeral.Set(&quot;CodigoUnidade&quot;, s.GetValue());
    gvMetas.PerformCallback('Atualizar');
}" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                                    Text="Indicador:">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px">
                                                <dxe:ASPxTextBox ID="txtIndicadorDado" runat="server" ClientEnabled="False" ClientInstanceName="txtIndicadorDado"
                                                     Width="100%">
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                                     Width="100%" KeyFieldName="ano;meta">
                                    <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" 
                                        AllowFocusedRow="True" />
                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="165" />
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s, true);
}" CustomButtonClick="function(s, e) {
    s.SetFocusedRowIndex(e.visibleIndex);	
	e.processOnServer = false;	
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		TipoOperacao = &quot;Editar&quot;;
		hfGeral.Set(&quot;TipoOperacao&quot;, TipoOperacao);
		OnGridFocusedRowChanged(gvDados);
		onClickBarraNavegacao(TipoOperacao, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
}" />
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="Ano" FieldName="ano" VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Meta" FieldName="meta" VisibleIndex="2" 
                                            Width="180px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <HeaderTemplate>
                                                <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir Novo Registro"" onclick=""TipoOperacao = 'Incluir'; onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Incluir Novo Registro"" style=""cursor: default;""/>")%>
                                            </HeaderTemplate>
                                        </dxwgv:GridViewCommandColumn>
                                    </Columns>
                                    <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False"></SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                    <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" VerticalScrollableHeight="165">
                                    </Settings>
                                    <SettingsText EmptyHeaders="Resultados" EmptyDataRow="Nenhum Resultado para Atualizar no Indicador Selecionado.">
                                    </SettingsText>
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" CloseAction="CloseButton"
                     HeaderText="Detalhes"
                    Width="350px" AllowDragging="True" AllowResize="True" 
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server"
                            SupportsDisabledAttribute="True">
                            <table cellpadding="0" cellspacing="0" class="style2">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="style2">
                                            <tr>
                                                <td class="style8">
                                                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" 
                                                        Text="Ano:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server" 
                                                        Text="Valor da Meta:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style8">
                                                    <dxe:ASPxSpinEdit ID="spAno" runat="server" ClientInstanceName="spAno" 
                                                         Height="21px" MaxLength="4" 
                                                        MaxValue="9999" Number="0" NumberType="Integer" Width="100%">
                                                        <SpinButtons ShowIncrementButtons="False">
                                                        </SpinButtons>
                                                        <ValidationSettings CausesValidation="True" ErrorDisplayMode="ImageWithTooltip" 
                                                            ValidationGroup="MKE">
                                                        </ValidationSettings>
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                <td>
                                                    <dxe:ASPxSpinEdit ID="spValorMeta" runat="server" 
                                                        ClientInstanceName="spValorMeta"  
                                                        Height="21px" Number="0" Width="100%">
                                                        <SpinButtons ShowIncrementButtons="False">
                                                        </SpinButtons>
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style6">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td style="padding-right: 10px">
                                                    <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" 
                                                        ClientInstanceName="btnSalvar"  
                                                        Text="Salvar" ValidationGroup="MKE" Width="100px">
                                                        <ClientSideEvents Click="function(s, e) {	 
 if (window.onClick_btnSalvar &amp;&amp; spAno.isValid)
		onClick_btnSalvar();
}" />
                                                        <Paddings Padding="0px" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td>
                                                    <dxe:ASPxButton ID="btnCancelar" runat="server" AutoPostBack="False" 
                                                        ClientInstanceName="btnCancelar"  
                                                        Text="Cancelar" Width="100px">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
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
                <dxpc:ASPxPopupControl ID="pcUsuarioIncluido" runat="server" 
                                            ClientInstanceName="pcUsuarioIncluido"  
                                            HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" 
                                            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" 
                                            Width="270px">
                                            <ContentCollection>
                                                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td align="center" style="">
                                                                </td>
                                                                <td align="center" rowspan="3" style="width: 70px">
                                                                    <dxe:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar" 
                                                                        ImageAlign="TextTop" ImageUrl="~/imagens/Workflow/salvarBanco.png">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <dxe:ASPxLabel ID="lblAcaoGravacao" runat="server" 
                                                                        ClientInstanceName="lblAcaoGravacao" >
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
    </form>
</body>
</html>
