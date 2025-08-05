<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="SincronismoProject.aspx.cs" Inherits="administracao_adm_GrupoRecurso" %>
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
                                Text="Sincronismo MS-EPM"></dxe:ASPxLabel>
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
 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="ResourceUID" AutoGenerateColumns="False" Width="100%"  ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnCustomCallback="gvDados_CustomCallback">
<ClientSideEvents FocusedRowChanged="function(s, e) {
	if (window.pcDados &amp;&amp; pcDados.IsVisible())
    {
        OnGridFocusedRowChanged(s);
	}
}" CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
	 OnGridFocusedRowChanged(s);
     
	 //if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     //{			
	 //	pcDados.Show();
     //}	
}
" EndCallback="function(s, e) {
	if(s.cp_Status == &quot;0&quot;)
	{
		pcNovoEmail.Hide();
		pcDados.Hide();
	}
		

    if(s.cp_msg != null &amp;&amp; s.cp_msg != &quot;&quot;)
        mostraDivSalvoPublicado(s.cp_msg);
}"></ClientSideEvents>
<Columns>
    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="50px">
        <CustomButtons>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Solucionar">
                <Image Url="~/imagens/botoes/vincularMSProject.PNG">
                </Image>
            </dxwgv:GridViewCommandColumnCustomButton>
            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                <Image Url="~/imagens/botoes/pFormulario.PNG">
                </Image>
            </dxwgv:GridViewCommandColumnCustomButton>
        </CustomButtons>
        <HeaderTemplate>
           &nbsp;
        </HeaderTemplate>
    </dxwgv:GridViewCommandColumn>
    <dxwgv:GridViewDataTextColumn Caption="Recurso" FieldName="Recurso" VisibleIndex="1" Width="180px">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Email" FieldName="Email" VisibleIndex="2">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Conta Windows" FieldName="ContaWindows" VisibleIndex="3"
        Width="150px">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Sincronizado" FieldName="Sincronizado" VisibleIndex="4"
        Width="85px">
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="TipoProblema" Visible="False" VisibleIndex="5">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="DescricaoMotivo" Visible="False" VisibleIndex="6">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible"></Settings>

<SettingsText ></SettingsText>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Recurso MS-Project" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="900px"  ID="pcDados">
<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 545px">
                            <dxe:ASPxLabel runat="server"  ID="ASPxLabel1" Text="Nome do Recurso:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxLabel runat="server"  ID="ASPxLabel2" Text="Conta Windows:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 545px">
                            <dxe:ASPxTextBox ID="txtNome" runat="server" ClientEnabled="False"
                                Width="535px" ClientInstanceName="txtNome">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txtContaWindows" runat="server" ClientEnabled="False"
                                Width="100%" ClientInstanceName="txtContaWindows">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxLabel runat="server"  ID="ASPxLabel3" Text="Email:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxTextBox ID="txtEmail" runat="server" ClientEnabled="False"
                    Width="100%" ClientInstanceName="txtEmail">
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxe:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td id="tdProblemas">
                <dxrp:ASPxRoundPanel ID="pcSolucionar" runat="server" BackColor="White" CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css"
                    CssPostfix="PlasticBlue" HeaderText="Solu&#231;&#227;o de problemas – V&#237;nculos de Recursos MS-Project" ImageFolder="~/App_Themes/PlasticBlue/{0}/"
                    Width="100%" ClientInstanceName="pcSolucionar">
                    <ContentPaddings Padding="1px" PaddingTop="5px" />
                    <HeaderStyle BackColor="#EBEBEB" Font-Bold="False" 
                        Height="23px">
                        <BorderLeft BorderStyle="None" />
                        <BorderRight BorderStyle="None" />
                        <BorderBottom BorderStyle="None" />
                    </HeaderStyle>
                    
                    <HeaderContent>
                        <BackgroundImage HorizontalPosition="left" ImageUrl="~/Images/ASPxRoundPanel/574514387/HeaderContent.png"
                            Repeat="RepeatX" VerticalPosition="bottom" />
                    </HeaderContent>
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="lblMsgVinculo" runat="server" Font-Bold="True"
                                            ForeColor="Red" ClientInstanceName="lblMsgVinculo">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 50px">
                                                    <dxe:ASPxLabel runat="server"  ID="ASPxLabel4" Text="Motivo:" Font-Bold="True">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="lblMotivo" runat="server" ClientInstanceName="lblMotivo" Font-Bold="True"
                                                         ForeColor="Red">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                <IFRAME id="frmVinculo" src="" frameBorder="0" width="100%" scrolling=no height="70"></IFRAME>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                </dxrp:ASPxRoundPanel>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td align="right">
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                     Text="Avan&#231;ar" Width="100px">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	window.document.getElementById('frmVinculo').executaAcao();
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
	pcDados.Hide();
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
                <dxpc:ASPxPopupControl ID="pcInformacao" runat="server" ClientInstanceName="pcInformacao"
                     HeaderText="Incluir a Entidad Atual" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                    ShowHeader="False" Width="543px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 65px; height: 15px" valign="top">
                                                </td>
                                                <td style="height: 15px" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 65px" valign="top">
                                                    <dxe:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar" ImageAlign="TextTop"
                                                        ImageUrl="~/imagens/informacao.PNG">
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td valign="top">
                                                    <dxe:ASPxLabel ID="lblInformacao" runat="server" ClientInstanceName="lblInformacao"
                                                        EncodeHtml="False" >
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False"
                                            Text="OK" Width="45px">
                                            <ClientSideEvents Click="function(s, e) {
	pcInformacao.Hide();
	pcDados.Hide();
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxpc:ASPxPopupControl ID="pcUsuarioIncluido" runat="server" ClientInstanceName="pcUsuarioIncluido"
                     HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
                    Width="270px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="center" style="">
                                        </td>
                                        <td align="center" rowspan="3" style="width: 70px">
                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgSalvar" ImageAlign="TextTop"
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
                <dxpc:ASPxPopupControl ID="pcNovoEmail" runat="server" ClientInstanceName="pcNovoEmail"
                     HeaderText="Incluir a Entidad Atual" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                    ShowHeader="False" Width="543px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" ClientInstanceName="lblEmail"
                                                        EncodeHtml="False"  Text="Email:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxTextBox ID="txtNovoEmail" runat="server"
                    Width="100%" ClientInstanceName="txtNovoEmail">
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                     Text="Salvar" Width="100px">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(validaFormulario())
		gvDados.PerformCallback('S');
}" />
                                                            <Paddings Padding="0px" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                     Text="Fechar" Width="90px">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcNovoEmail.Hide();
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
            </td>
            <td>
            </td>
        </tr>

    </table>
</asp:Content>
