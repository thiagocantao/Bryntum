<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="chaveAcessoExternoAPI.aspx.cs" Inherits="administracao_chaveAcessoExternoAPI" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <div id="divGrid" style="padding: 10px 10px 10px 10px">
        <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados" Width="100%" Font-Names="Verdana" Font-Size="8pt" ID="gvDados" KeyFieldName="CodigoUsuario" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
            <ClientSideEvents CustomButtonClick="function(s, e) 
{
                s.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnChaveAcesso&quot;)
     {  
            s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoUsuario;Client_ID', abrePopupChaveAcesso);
     }
}
"
                BeginCallback="function(s, e) {
	comando = e.command;
}"
                Init="function(s, e) {
	s.SetHeight(Math.max(0, document.documentElement.clientHeight) - 100);
}"></ClientSideEvents>
            <SettingsPager Mode="ShowAllRecords" Visible="False">
            </SettingsPager>
            <SettingsEditing Mode="Inline">
            </SettingsEditing>
            <Settings ShowGroupButtons="False" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible"></Settings>
            <SettingsBehavior AllowFocusedRow="True" AllowGroup="False"></SettingsBehavior>
            <SettingsCommandButton>
                <CancelButton>
                    <Image Url="~/imagens/botoes/cancelar.PNG">
                    </Image>
                </CancelButton>
            </SettingsCommandButton>
            <SettingsPopup>
                <HeaderFilter MinHeight="140px">
                </HeaderFilter>
            </SettingsPopup>
            <Columns>
                <dxtv:GridViewCommandColumn VisibleIndex="0">
                    <CustomButtons>
                        <dxtv:GridViewCommandColumnCustomButton ID="btnChaveAcesso" Text="Chave de acesso">
                            <Image Url="~/imagens/botoes/imgChaveAcesso.PNG" ToolTip="Chave de acesso" IconID="imgChaveAcesso">
                            </Image>
                        </dxtv:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dxtv:GridViewCommandColumn>
                <dxcp:GridViewDataTextColumn FieldName="NomeUsuario" ShowInCustomizationForm="True" Width="50%" Caption="Nome do Usuário" VisibleIndex="2">
                    <Settings AllowGroup="False" ShowFilterRowMenu="False" ShowFilterRowMenuLikeItem="False" AllowHeaderFilter="False" ShowInFilterControl="False" AllowSort="True"></Settings>
                    <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Tarefa"></EditFormSettings>
                </dxcp:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Email do Usuário" FieldName="EMail" VisibleIndex="3" Width="50%">
                    <EditFormSettings Visible="True" />
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn FieldName="CodigoUsuario" ShowInCustomizationForm="False" Visible="False" VisibleIndex="4">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Client_ID" FieldName="Client_ID" Visible="False" VisibleIndex="5">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataCheckColumn Caption="Credenciado" FieldName="IndicaUsuarioCredenciado" VisibleIndex="1" Width="120px">
                    <PropertiesCheckEdit AllowGrayed="True" DisplayTextChecked="Sim" DisplayTextUnchecked="Não" ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                    </PropertiesCheckEdit>
                    <DataItemTemplate>
                      <%# getCheckBox()%>
                     </DataItemTemplate>
                </dxtv:GridViewDataCheckColumn>
            </Columns>
            <Border BorderColor="#009900" BorderWidth="1px"></Border>
        </dxcp:ASPxGridView>
        
        <dxcp:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
        </dxcp:ASPxLoadingPanel>
        <dxcp:ASPxCallback ID="callbackCredencia" runat="server" ClientInstanceName="callbackCredencia" OnCallback="callbackCredencia_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
          var sHeight = Math.max(0, document.documentElement.clientHeight) - 255;
          var sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
          lpLoading.Hide();
          var url1 =  window.top.pcModal.cp_Path + &quot;administracao/popupChaveAcesso.aspx?C=&quot; + s.cpCodigoUsuarioSelecionado;
          url1 +=  '&amp;CI=' + s.cpRetornoClientID;
          url1 += '&amp;CS=' +s.cpRetornoClientSecret;
          window.top.showModal(url1 , 'Chave', 800, 330, executaPosPopUp, null);
}" />
        </dxcp:ASPxCallback>
        <dxcp:ASPxCallback ID="callbackDescredencia" runat="server" ClientInstanceName="callbackDescredencia" OnCallback="callbackDescredencia_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
var textoMsg = 'a ação de ' + s.cpAcao + ' foi executada no usuário de codigo: ' + s.cpCodigo;
lpLoading.Hide();
//alert(textoMsg);
gvDados.Refresh();
//window.top.mostraMensagem(textoMsg, 'sucesso', false, false, null, 3000);
}" />
        </dxcp:ASPxCallback>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" runat="Server">
</asp:Content>

