<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="ElementosEstrategia.aspx.cs" Inherits="ElementosEstrategia" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function abreNovo() {
            hfGeral.Set("TipoOperacao", "Incluir");
            TipoOperacao = "Incluir";
            ddlChave.SetValue(null);
            pcDados.Show();
            ddlTema.PerformCallback();
        }


        function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
            // o método será chamado por meio do objeto pnCallBack
            hfGeral.Set("StatusSalvar", "0");
            pnCallback.PerformCallback(TipoOperacao);
            return false;
        }
               

        function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
            // o método será chamado por meio do objeto pnCallBack
            hfGeral.Set("StatusSalvar", "0");
            pnCallback.PerformCallback(TipoOperacao);
            return false;
        }

        //----------- Mensagem modificação con sucesso..!!!
        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

            fechaTelaEdicao();
        }

        function fechaTelaEdicao() {
            onClick_btnCancelar();
        }

        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";

            if (ddlAcao.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") Todos os campos devem ser preenchidos!";
            }

            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = mensagem;
            }

            return mensagemErro_ValidaCamposFormulario;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" sroll="yes">
    <table>
        <tr>
            <td>
                <dxcp:aspxcallbackpanel id="pnCallback" runat="server" clientinstancename="pnCallback"
                    width="100%" oncallback="pnCallback_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
        KeyFieldName="CodigoObjetivoEstrategico;CodigoAcaoTransformadora" 
        AutoGenerateColumns="False" Width="100%" 
         ID="gvDados" 
        OnCustomButtonInitialize="gvDados_CustomButtonInitialize" 
        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" 
        OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
<ClientSideEvents CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnEditar&quot;)
     {
        hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;		
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
        if(confirm('Deseja excluir o registro selecionado?'))
		pnCallback.PerformCallback('Excluir');
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {			
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		ddlChave.SetValue(null);
		pcDados.Show();
		ddlTema.PerformCallback();
     }	
}
"></ClientSideEvents>
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="50px" VisibleIndex="0">
<CustomButtons>
<dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar" 
        Visibility="Invisible">
<Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
<Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes" 
        Visibility="Invisible">
<Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
</CustomButtons>
<HeaderTemplate>
            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""abreNovo();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
        
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="FatorChave" 
        Caption="Fator Chave" VisibleIndex="1" Width="200px">
    <Settings AutoFilterCondition="Contains" AllowAutoFilter="True" />
</dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Tema Prioritário" 
        FieldName="TemaPrioritario" ShowInCustomizationForm="True" VisibleIndex="2" 
        Width="200px">
        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Objetivo" 
        FieldName="ObjetivoEstrategico" ShowInCustomizationForm="True" VisibleIndex="3" 
        Width="200px">
        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Ação Transformadora" 
        FieldName="AcaoTransformadora" ShowInCustomizationForm="True" 
        VisibleIndex="4">
        <Settings AllowAutoFilter="True" AllowGroup="True" 
            AutoFilterCondition="Contains" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="CodigoFatorChave" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="CodigoTemaPrioritario" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" ShowGroupPanel="True"></Settings>

<SettingsText ></SettingsText>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" 
        CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="780px" 
         ID="pcDados" Modal="True">
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
                    Text="Fator Chave:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxComboBox ID="ddlChave" runat="server" ClientInstanceName="ddlChave" 
                     Width="100%">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlTema.PerformCallback();
}" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                    Text="Tema Prioritário:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxComboBox ID="ddlTema" runat="server" ClientInstanceName="ddlTema" 
                     Width="100%">
                    <ClientSideEvents EndCallback="function(s, e) {
	ddlObjetivo.PerformCallback();
}" SelectedIndexChanged="function(s, e) {
	ddlObjetivo.PerformCallback();
}" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                    Text="Objetivo:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxComboBox ID="ddlObjetivo" runat="server" 
                    ClientInstanceName="ddlObjetivo"  
                    Width="100%">
                    <ClientSideEvents EndCallback="function(s, e) {
	ddlAcao.PerformCallback();
}" SelectedIndexChanged="function(s, e) {
	ddlAcao.PerformCallback();
}" />
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                    Text="Ação Transformadora:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxComboBox ID="ddlAcao" runat="server" ClientInstanceName="ddlAcao" 
                     Width="100%">
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnSalvar"  
                                    Text="Salvar" Width="100px">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                            <td style="WIDTH: 10px">
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnFechar"  
                                    Text="Fechar" Width="90px">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                    <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                                        PaddingRight="0px" PaddingTop="0px" />
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
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido"><ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>

























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
		mostraDivSalvoPublicado(&quot;Ação incluída com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Ação excluída com sucesso!&quot;);
}"></ClientSideEvents>
</dxcp:aspxcallbackpanel>
            </td>
        </tr>

    </table>
    </form>
</body>
</html>
