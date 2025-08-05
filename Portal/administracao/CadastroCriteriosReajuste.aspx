<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CadastroCriteriosReajuste.aspx.cs" Inherits="administracao_CadastroCriterioMedicao" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content id="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <!-- TABLA CONTEUDO -->
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel id="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Cadastro de Critérios de Reajuste de Contratos"></dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
            </td>
            <td style="height: 10px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxcp:aspxcallbackpanel id="pnCallback" runat="server" clientinstancename="pnCallback"
                    width="100%" oncallback="pnCallback_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
        KeyFieldName="CodigoCriterioReajusteContrato" AutoGenerateColumns="False" Width="100%" 
         ID="gvDados" 
        OnCustomButtonInitialize="gvDados_CustomButtonInitialize" 
        OnAfterPerformCallback="gvDados_AfterPerformCallback1" 
        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
<ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
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
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
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
            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
        
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoCriterioReajusteContrato" Name="DescricaoCriterioReajusteContrato" 
        Caption="Critério de Reajuste" VisibleIndex="2">
    <Settings AutoFilterCondition="Contains" />
</dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"></Settings>

<SettingsText ></SettingsText>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="620px"  ID="pcDados">
<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table>
        <tr>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                    Text="Critério de Reajuste:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxTextBox ID="txtCriterioReajuste" runat="server" ClientInstanceName="txtCriterioReajuste"
                     MaxLength="50" Width="100%">
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxe:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
        <tr>
            <td align="right">
                <table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

 </td><td style="WIDTH: 10px"></td><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxButton>

 </td></tr></tbody></table>
            </td>
        </tr>
    </table>
</dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido"><ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>

























 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>

























 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Critério de reajuste incluído com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Critério de reajuste alterado com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Critério de reajuste excluído com sucesso!&quot;);
}"></ClientSideEvents>
</dxcp:aspxcallbackpanel>
            </td>
            <td>
            </td>
        </tr>

    </table>
</asp:Content>
