<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="EditaIniciaisFormularios.aspx.cs" Inherits="formularios_EditaIniciaisFormularios" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%; padding-bottom: 5px;">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>
                    <tr>
                        <td align="left" style="height: 19px; padding-left: 10px;" valign="middle">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Gestão" ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="padding-left: 5px">
        <dxwgv:ASPxGridView ID="gvDados" ClientInstanceName="gvDados" runat="server" AutoGenerateColumns="False"
            DataSourceID="sdsListaFormularios" KeyFieldName="CodigoModeloFormulario" Width="100%"
             OnCustomErrorText="gvDados_CustomErrorText"
            OnRowUpdating="gvDados_RowUpdating">
            <ClientSideEvents CustomButtonClick="function(s, e) {
if(e.buttonID == &quot;btnEditar&quot;){
s.StartEditRow(e.visibleIndex);        
//s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoModeloFormulario',MontaCamposFormulario);

}
}" BeginCallback="function(s, e) {
	comando = e.command;
}" EndCallback="function(s, e) {
     if(comando == &quot;UPDATEEDIT&quot;)
     {
          if(s.cp_Erro != &quot;&quot; &amp;&amp; s.cp_Erro != &quot;OK&quot;)
          {
               window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
          }
          else
          {
                 mostraDivSalvoPublicado(s.cp_Sucesso);
          }
     }
}" />
            <Columns>
                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="90px">
                    <CustomButtons>
                        <dxtv:GridViewCommandColumnCustomButton ID="btnEditar">
                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                            </Image>
                        </dxtv:GridViewCommandColumnCustomButton>
                        <dxtv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Visibility="Invisible">
                            <Image Url="~/imagens/botoes/pFormulario.png">
                            </Image>
                        </dxtv:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NomeFormulario" VisibleIndex="1" Caption="Formulário">
                    <EditFormSettings Visible="False" />
                    <DataItemTemplate>
                        <%# ("<a class='LinkGrid' target='_self' href='#' onclick='MontaCamposFormulario(" + Eval("CodigoModeloFormulario") + ")'>" + Eval("NomeFormulario") + "</a>")%>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="IniciaisFormularioControladoSistema" VisibleIndex="4"
                    Caption="Iniciais" Width="95px">
                    <PropertiesTextEdit MaxLength="24">
                    </PropertiesTextEdit>
                    <EditFormSettings Caption="Iniciais" CaptionLocation="Top" Visible="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxtv:GridViewDataCheckColumn Caption="Controlado" FieldName="IndicaControladoSistema"
                    VisibleIndex="3" Width="90px">
                    <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                    </PropertiesCheckEdit>
                    <EditFormSettings Caption="Indica Controlado Sistema" CaptionLocation="Top" Visible="True" />
                </dxtv:GridViewDataCheckColumn>
            </Columns>
            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
            <Settings VerticalScrollBarMode="Visible" />
            <SettingsPopup>
                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                    Width="350px" AllowResize="True" />
            </SettingsPopup>
            <StylesEditors>
                <Style >                    
                </Style>
            </StylesEditors>
        </dxwgv:ASPxGridView>
    </div>
    <dxpc:ASPxPopupControl ID="popup" runat="server" HeaderText="Informe a senha de acesso"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="300px"
        ClientInstanceName="popup" CloseAction="None" Modal="True" ShowCloseButton="False"
        >
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table style="width: 100%;">
                    <tr>
                        <td width="100%">
                            <dxe:ASPxTextBox ID="txtSenha" runat="server" ClientInstanceName="txtSenha" Password="True"
                                Width="100%" >
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="75px">
                            <dxe:ASPxButton ID="btnValidarSenha" runat="server" Text="Validar" ClientInstanceName="btnValidarSenha"
                                Width="75px" >
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <dxe:ASPxLabel ID="lblMensagem" runat="server" ClientVisible="False"
                                ForeColor="Red" Text="* A senha informada não é válida">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <asp:SqlDataSource ID="sdsListaFormularios" runat="server" SelectCommand="SELECT CodigoModeloFormulario, NomeFormulario, IndicaControladoSistema, IniciaisFormularioControladoSistema FROM ModeloFormulario  WHERE DataExclusao is null  AND CodigoTipoFormulario <> 3 ORDER BY NomeFormulario">
    </asp:SqlDataSource>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="pcMensagemGravacao" HeaderText="Incluir a Entidad Atual"
        ShowCloseButton="False" ShowHeader="False" Width="270px"
        ID="pcMensagemGravacao">
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="" align="center">
                            </td>
                            <td style="width: 70px" align="center" rowspan="3">
                                <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                    ClientInstanceName="imgSalvar" ID="imgSalvar">
                                </dxcp:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxcp:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                    ID="lblAcaoGravacao">
                                </dxcp:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>
</asp:Content>
