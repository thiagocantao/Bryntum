<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="ComentariosTS.aspx.cs" Inherits="administracao_CadastroRamosAtividades" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
            height: 10px;
        }
        .style4
        {
            width: 120px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>
                <dxcp:aspxcallbackpanel id="pnCallback" runat="server" clientinstancename="pnCallback"
                    width="100%"><PanelCollection>
<dxp:PanelContent runat="server"><dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
        KeyFieldName="CodigoMovimento" AutoGenerateColumns="False" Width="100%" 
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
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="50px" VisibleIndex="0" 
        Caption=" ">
<CustomButtons>
<dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar" 
        Visibility="Invisible">
<Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir" 
        Visibility="Invisible">
<Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
<Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
</CustomButtons>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn 
        Caption="Usuário" VisibleIndex="2" Width="200px" FieldName="Responsavel">
</dxwgv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Ação" 
        ShowInCustomizationForm="True" VisibleIndex="3" Width="150px" 
        FieldName="Acao">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Comentários" 
        ShowInCustomizationForm="True" VisibleIndex="4" FieldName="Comentarios">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataDateColumn Caption="Data" ShowInCustomizationForm="True" 
        VisibleIndex="1" Width="100px" FieldName="Data">
        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
        </PropertiesDateEdit>
    </dxtv:GridViewDataDateColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" 
         VerticalScrollableHeight="410"></Settings>

<SettingsText ></SettingsText>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" 
        CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="650px" 
         ID="pcDados">
<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td class="style4">
                            <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" 
                                Text="Data:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td>
                            <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" 
                                Text="Usuário:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right: 10px" class="style4">
                            <dxtv:ASPxTextBox ID="txtData" runat="server" ClientInstanceName="txtData" 
                                DisplayFormatString="{0:dd/MM/yyyy}"  
                                Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxTextBox>
                        </td>
                        <td>
                            <dxtv:ASPxTextBox ID="txtUsuario" runat="server" 
                                ClientInstanceName="txtUsuario"  
                                Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3">
            </td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" 
                    Text="Comentários:">
                </dxtv:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxMemo ID="mmComentarios" runat="server" ClientInstanceName="mmComentarios" 
                     Rows="15" Width="100%">
                    <ValidationSettings CausesValidation="True" Display="Dynamic" 
                        ErrorDisplayMode="ImageWithTooltip" ErrorText="Formato Inválido" 
                        ValidationGroup="MKE">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxtv:ASPxMemo>
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

 </td></tr></TBODY></table>
            </td>
        </tr>
    </table>
</dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido"><ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>

























 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>

























 </td></tr></TBODY></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Ramo de atividade incluído com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Ramo de atividade alterado com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Ramo de atividade excluído com sucesso!&quot;);
}"></ClientSideEvents>
</dxcp:aspxcallbackpanel>
            </td>
        </tr>

        </table>
</div>
    </form>
</body>
</html>
