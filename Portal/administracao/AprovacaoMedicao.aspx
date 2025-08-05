<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="AprovacaoMedicao.aspx.cs" Inherits="administracao_CadastroRamosAtividades" %>
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
                                Text="Aprovação/Reprovação de Medições"></dxe:ASPxLabel>
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
        KeyFieldName="CodigoMedicao" AutoGenerateColumns="False" Width="100%" 
         ID="gvDados" 
        OnCustomButtonInitialize="gvDados_CustomButtonInitialize" 
        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
<ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
     hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
	 TipoOperacao = &quot;Editar&quot;;
     
}
"></ClientSideEvents>
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="45px" VisibleIndex="0" 
        Caption=" ">
<CustomButtons>
<dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Aprovar / Reprovar">
<Image Url="~/imagens/botoes/aprovarReprovar.png" ToolTip="Aprovar / Reprovar"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
</CustomButtons>

</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="AnoMedicao" 
        Caption="Ano" VisibleIndex="1" Width="50px">
    <Settings AutoFilterCondition="Equals" />
    <HeaderStyle HorizontalAlign="Center" />
    <CellStyle HorizontalAlign="Center">
    </CellStyle>
</dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Mês" FieldName="MesMedicao" 
        ShowInCustomizationForm="True" VisibleIndex="2" Width="50px">
        <Settings AutoFilterCondition="Equals" />
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Número Contrato" 
        FieldName="NumeroContrato" ShowInCustomizationForm="True" VisibleIndex="3" 
        Width="150px">
        <Settings AutoFilterCondition="Contains" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Descrição do Objeto Contrato" 
        FieldName="DescricaoObjetoContrato" ShowInCustomizationForm="True" 
        VisibleIndex="4">
        <Settings AutoFilterCondition="Contains" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" 
        Name="NomeProjeto" ShowInCustomizationForm="True" VisibleIndex="5">
        <Settings AutoFilterCondition="Contains" />
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"></Settings>

<SettingsText ></SettingsText>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" 
        CloseAction="None" HeaderText="Aprovar / Reprovar" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        ShowCloseButton="False" Width="850px"  
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
                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                    Text="Comentários da Medição:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxMemo ID="txtComentariosMedicao" runat="server" ClientEnabled="False" 
                    ClientInstanceName="txtComentariosMedicao"  
                    Rows="8" Width="100%">
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxe:ASPxMemo>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" EncodeHtml="False" 
                     Text="Comentários: &amp;nbsp;">
                </dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lblCantCarater" runat="server" 
                    ClientInstanceName="lblCantCarater"  
                    ForeColor="Silver" Text="0">
                </dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lblDe250" runat="server" ClientInstanceName="lblDe250" 
                    EncodeHtml="False"  ForeColor="Silver" 
                    Text=" &amp;nbsp;de 4000">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxMemo ID="mmObservacoes" runat="server" 
                    ClientInstanceName="mmObservacoes"  
                    Rows="8" Width="100%">
                    <ClientSideEvents Init="function(s, e) {
	onInit_mmObservacoes(s, e);
}" ValueChanged="function(s, e) {
	
}" />
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxe:ASPxMemo>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td align="right">
                <table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td>
                    <dxe:ASPxButton ID="btnAprovar" runat="server" AutoPostBack="False" 
                        ClientInstanceName="btnAprovar"  
                        Text="Aprovar" Width="100px">
                        <clientsideevents click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                        <paddings padding="0px" />
                    </dxe:ASPxButton>

 </td>
                    <td style="WIDTH: 10px">
                        &nbsp;</td>
                    <td>
                        <dxe:ASPxButton ID="btnReprovar" runat="server" AutoPostBack="False" 
                            ClientInstanceName="btnReprovar"  
                            Text="Reprovar" Width="100px">
                            <clientsideevents click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                            <paddings padding="0px" />
                        </dxe:ASPxButton>
                    </td>
                    <td style="WIDTH: 10px"></td><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"  ID="btnFechar">
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
		mostraDivSalvoPublicado(&quot;Ramo de atividade incluído com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Ramo de atividade alterado com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Ramo de atividade excluído com sucesso!&quot;);
}"></ClientSideEvents>
</dxcp:aspxcallbackpanel>
            </td>
            <td>
            </td>
        </tr>

    </table>
</asp:Content>
