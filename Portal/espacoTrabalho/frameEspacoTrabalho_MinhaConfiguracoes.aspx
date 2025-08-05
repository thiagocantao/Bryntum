<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="frameEspacoTrabalho_MinhaConfiguracoes.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_MinhaConfiguracoes" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="center" style="width: 1px">
                            <span id="spanBotoes" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False"
                                Text="Configurações do Sistema"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <table>
        <tr>
            <td>
            </td>
            <td>
                <CDIS:BarraNavegacao ID="BarraNavegacao1" runat="server" NomeASPxGridView="gvDados" NomeASPxPopupControl="pcDados" MostrarExclusao="False" MostrarInclusao="False" />
 
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server"><dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados" KeyFieldName="CodigoParametro" Width="100%" >
<Columns>
    <dxwgv:GridViewDataTextColumn Caption="Par&#226;metro" FieldName="DescricaoParametro_PT"
        VisibleIndex="0" Width="60%">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Valor" FieldName="DescricaoValor" VisibleIndex="1"
        Width="30%">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Tipo" FieldName="TipoDadoParametro" Name="TipoDadoParametro"
        VisibleIndex="2" Width="5%" Visible="False">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="Valor" VisibleIndex="2" Width="5%" Visible="False">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" />
<SettingsBehavior AllowFocusedRow="True" />

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" />
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl ID="pcDados" runat="server" HeaderText="Dados do Risco Padr&#227;o" ClientInstanceName="pcDados" CloseAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" AllowDragging="True"  Height="100px"><ContentCollection>
<dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server"><table><tr><td align="left">
    <dxp:ASPxPanel ID="pnFormulario2" runat="server" style="overflow: auto" Width="100%">
        <panelcollection>
<dxp:PanelContent runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0"><TBODY><tr><td><SPAN style=""><asp:Label runat="server" Text="Valor:" ID="lblValor"></asp:Label>


 </SPAN></td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="700px"  ClientInstanceName="txtValorTXT"  ID="txtValorTXT">
<ValidationSettings>
<ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png"></ErrorImage>

<ErrorFrameStyle ImageSpacing="4px">
<ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
</ErrorFrameStyle>
</ValidationSettings>

<DisabledStyle ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="60px"  ClientInstanceName="txtValorINT"  ID="txtValorINT">
<MaskSettings Mask="&lt;0..100000000&gt;"></MaskSettings>

<ValidationSettings>
<ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png"></ErrorImage>

<ErrorFrameStyle ImageSpacing="4px">
<ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
</ErrorFrameStyle>
</ValidationSettings>

<DisabledStyle ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


 </td></tr><tr><td><dxe:ASPxComboBox runat="server" ValueType="System.String"  ShowShadow="False" ImageFolder="~/App_Themes/Aqua/{0}/" AutoResizeWithContainer="True"  ClientInstanceName="ddlValorMES"  ID="ddlValorMES">
<DropDownButton>
<Image UrlDisabled="~/App_Themes/Aqua/Editors/edtdropDownDisabled.png" UrlHottracked="~/App_Themes/Aqua/Editors/edtdropDownHottracked.png" UrlPressed="~/App_Themes/Aqua/Editors/edtdropDownPressed.png" Height="7px" Width="9px" Url="~/App_Themes/Aqua/Editors/edtdropDown.png"></Image>
</DropDownButton>

<ButtonEditEllipsisImage UrlDisabled="~/App_Themes/Aqua/Editors/edtEllipsisDisabled.png" UrlHottracked="~/App_Themes/Aqua/Editors/edtEllipsisHottracked.png" UrlPressed="~/App_Themes/Aqua/Editors/edtEllipsisPressed.png" Height="3px" Width="12px" Url="~/App_Themes/Aqua/Editors/edtEllipsis.png"></ButtonEditEllipsisImage>

<ValidationSettings>
<ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png"></ErrorImage>

<ErrorFrameStyle ImageSpacing="4px">
<ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
</ErrorFrameStyle>
</ValidationSettings>
</dxe:ASPxComboBox>


 </td></tr><tr><td><dxe:ASPxRadioButtonList runat="server" ItemSpacing="15px" RepeatDirection="Horizontal" ClientInstanceName="rbValorBOL" Width="129px"  ID="rbValorBOL">
<Paddings Padding="0px"></Paddings>
<Items>
<dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
<dxe:ListEditItem Text="N&#227;o" Value="N"></dxe:ListEditItem>
</Items>
</dxe:ASPxRadioButtonList>


 </td></tr><tr><td><dxe:ASPxColorEdit runat="server" ShowShadow="False" ImageFolder="~/App_Themes/Aqua/{0}/"  ClientInstanceName="ddlCOR"  ID="ddlCOR">
<DropDownButton>
<Image UrlDisabled="~/App_Themes/Aqua/Editors/edtdropDownDisabled.png" UrlHottracked="~/App_Themes/Aqua/Editors/edtdropDownHottracked.png" UrlPressed="~/App_Themes/Aqua/Editors/edtdropDownPressed.png" Height="7px" Width="9px" Url="~/App_Themes/Aqua/Editors/edtdropDown.png"></Image>
</DropDownButton>

<ValidationSettings>
<ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Aqua/Editors/edtError.png"></ErrorImage>

<ErrorFrameStyle ImageSpacing="4px">
<ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
</ErrorFrameStyle>
</ValidationSettings>
</dxe:ASPxColorEdit>


 </td></tr></TBODY></table></dxp:PanelContent>
</panelcollection>
    </dxp:ASPxPanel>
 </td></tr><tr><td align="left" style="height: 10px"></td></tr><tr><td align="left"><dxe:ASPxMemo ID="memoDescricao" runat="server" ClientEnabled="False" ClientInstanceName="memoDescricao"
                 Rows="5" Width="700px" >
<ValidationSettings>
<ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" Width="14px" />

<ErrorFrameStyle ImageSpacing="4px">
<ErrorTextPaddings PaddingLeft="4px" />
</ErrorFrameStyle>
</ValidationSettings>

<DisabledStyle ForeColor="#404040"></DisabledStyle>
</dxe:ASPxMemo>

 </td></tr><tr><td style="height: 10px"></td></tr><tr><td align="right"><table id="tblSalvarFechar" border="0" cellpadding="0" cellspacing="0"><tr style="height: 35px"><td><dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" Text="Salvar" AutoPostBack="False"  Width="70px">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />

<Paddings Padding="0px" />
</dxe:ASPxButton>

 </td><td style="width: 80px" align="right"><dxe:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar" Text="Fechar" AutoPostBack="False"  Width="70px">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />

<Paddings Padding="0px" />
</dxe:ASPxButton>

 </td></tr></table></td></tr></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral"></dxhf:ASPxHiddenField>
 </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
	    onEnd_pnCallback();
}" />
                </dxcp:ASPxCallbackPanel>
 
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>  
