<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditaIniciaisFormularios_popup.aspx.cs"
    Inherits="formularios_EditaIniciaisFormularios_popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title></title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        <dxwgv:ASPxGridView ID="gvDados" ClientInstanceName="gvDados" runat="server" AutoGenerateColumns="False"
            DataSourceID="sdsListaCamposFormularios" KeyFieldName="CodigoCampo" Width="100%"
             OnRowUpdating="gvDados_RowUpdating">
            <ClientSideEvents CustomButtonClick="function(s, e) {
if(e.buttonID == &quot;btnEditar&quot;){
//s.UpdateEdit();
s.StartEditRow(e.visibleIndex);
}
}" BeginCallback="function(s, e) {
	comando = e.command;
}" EndCallback="function(s, e) {
	if(comando == &quot;UPDATEEDIT&quot;)
{
mostraDivSalvoPublicado(&quot;&quot;); 

}

}" />
            <ClientSideEvents CustomButtonClick="function(s, e) {
if(e.buttonID == &quot;btnEditar&quot;){
//s.UpdateEdit();
s.StartEditRow(e.visibleIndex);
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
}"></ClientSideEvents>
            <Columns>
                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="90px" ShowCancelButton="True"
                    ShowUpdateButton="True">
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
                    <HeaderTemplate>
                        &nbsp;
                    </HeaderTemplate>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn FieldName="CodigoCampo" VisibleIndex="1" Visible="False">
                    <EditFormSettings Visible="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NomeCampo" VisibleIndex="2" Caption="Campo">
                    <EditFormSettings Visible="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="DescricaoCampo" VisibleIndex="3" Caption="Descrição">
                    <EditFormSettings Visible="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Iniciais" FieldName="IniciaisCampoControladoSistema"
                    VisibleIndex="5" Width="100px">
                    <PropertiesTextEdit MaxLength="24">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" Caption="Iniciais" CaptionLocation="Top" />
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataCheckColumn Caption="Controlado" FieldName="IndicaControladoSistema"
                    VisibleIndex="4" Width="100px">
                    <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                    </PropertiesCheckEdit>
                    <EditFormSettings Visible="True" Caption="Indica Controlado Sistema" CaptionLocation="Top" />
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
            <Templates>
                <DetailRow>
                </DetailRow>
            </Templates>
        </dxwgv:ASPxGridView>
        <asp:SqlDataSource ID="sdsListaCamposFormularios" runat="server" SelectCommand="
SELECT CodigoModeloFormulario, CodigoCampo, NomeCampo, DescricaoCampo, IndicaControladoSistema, IniciaisCampoControladoSistema
FROM CampoModeloFormulario
WHERE DataExclusao IS NULL
AND CodigoModeloFormulario =@CodigoModeloFormulario
ORDER BY NomeCampo">
            <SelectParameters>
                <asp:QueryStringParameter Name="CodigoModeloFormulario" QueryStringField="CMF" />
            </SelectParameters>
        </asp:SqlDataSource>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </div>
    </form>
</body>
</html>
