<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TarefasItemBacklog_antigo.aspx.cs" Inherits="_Projetos_DadosProjeto_TarefasItemBacklog_antigo" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            width: 125px;
        }

        .style3 {
            height: 10px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            Width="100%" OnCallback="pnCallback_Callback">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                        KeyFieldName="CodigoItem" AutoGenerateColumns="False" Width="100%"
                                        ID="gvDados"
                                        OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                        OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                            CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
     }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
        hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
        var funcObj = { funcaoClickOK: function () { pnCallback.PerformCallback('Excluir'); } }

        window.top.mostraMensagem('Deseja realmente excluir a tarefa e o histórico de movimentações?', 'confirmacao', true, true, function () { funcObj['funcaoClickOK']() });

		
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }
}
"></ClientSideEvents>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                        <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                        <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                        <Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <HeaderTemplate>
                                                    <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
                                                </HeaderTemplate>
                                            </dxwgv:GridViewCommandColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="TituloItem" Name="TituloItem"
                                                Caption="Tarefa" VisibleIndex="1">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="Estimativa" FieldName="EsforcoPrevisto"
                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="100px">
                                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                </PropertiesTextEdit>
                                                <Settings AllowAutoFilter="False" />
                                            </dxtv:GridViewDataTextColumn>
                                            <dxtv:GridViewDataTextColumn Caption="Descrição" FieldName="DetalheItem"
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                            </dxtv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                        <SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>
                                        <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"
                                            VerticalScrollableHeight="310"></Settings>
                                        <SettingsText></SettingsText>
                                    </dxwgv:ASPxGridView>
                                    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados"
                                        CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
                                        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="800px"
                                        ID="pcDados">
                                        <ContentStyle>
                                            <Paddings Padding="5px"></Paddings>
                                        </ContentStyle>
                                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td>
                                                                        <dxtv:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                            Text="Tarefa:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                    <td class="style2">
                                                                        <dxtv:ASPxLabel ID="ASPxLabel1" runat="server"
                                                                            Text="Estimativa:">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 10px">
                                                                        <dxtv:ASPxTextBox ID="txtTituloTarefa" runat="server"
                                                                            ClientInstanceName="txtTituloTarefa"
                                                                            MaxLength="150" Width="100%">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxTextBox>
                                                                    </td>
                                                                    <td class="style2">
                                                                        <dxtv:ASPxSpinEdit ID="txtEsforcoPrevisto" runat="server"
                                                                            AllowMouseWheel="False" ClientInstanceName="txtEsforcoPrevisto"
                                                                            DisplayFormatString="{0:n0}"
                                                                            Number="0" Width="100%" NumberType="Integer">
                                                                            <SpinButtons ClientVisible="False" ShowIncrementButtons="False">
                                                                            </SpinButtons>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                Text="Descrição:">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxMemo ID="mmDetalhes" runat="server" ClientInstanceName="mmDetalhes"
                                                                Rows="10" Width="100%">
                                                                <ValidationSettings CausesValidation="True" Display="Dynamic"
                                                                    ErrorDisplayMode="ImageWithTooltip" ErrorText="Formato Inválido"
                                                                    ValidationGroup="MKE">
                                                                    <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                </ValidationSettings>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxtv:ASPxMemo>
                                                            <dxtv:ASPxLabel ID="lbl_mmDetalhes" runat="server"
                                                                ClientInstanceName="lbl_mmDetalhes" Font-Bold="True"
                                                                Font-Size="7pt" ForeColor="#999999">
                                                            </dxtv:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px" ID="btnSalvar">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td style="width: 10px"></td>
                                                                        <td>
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
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
                                    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="" align="center"></td>
                                                            <td style="width: 70px" align="center" rowspan="3">
                                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                    </dxpc:ASPxPopupControl>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Tarefa incluída com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Tarefa alterada com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
        mostraDivSalvoPublicado(&quot;Tarefa excluída com sucesso!&quot;);
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                </tr>
                <tr>
                    <td class="style3"></td>
                </tr>
                <tr>
                    <td align="right">
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False"
                            Text="Fechar" Width="90px">
                            <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                            <Paddings Padding="0px" />
                        </dxe:ASPxButton>
                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
 