<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelecaoItensPlanejamentoSprint_antigo.aspx.cs"
    Inherits="_Projetos_DadosProjeto_SelecaoItensPlanejamentoSprint_antigo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <div id="divConteudo">
            <table>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="gvDados" runat="server" Width="100%" AutoGenerateColumns="False"
                            ClientInstanceName="gvDados"
                            KeyFieldName="CodigoItem" 
                            onbatchupdate="gvDados_BatchUpdate" 
                            oncustomcallback="gvDados_CustomCallback" 
                            onhtmldatacellprepared="gvDados_HtmlDataCellPrepared">
                           
                            <Columns>
                                <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" 
                                    Width="30px" Caption=" ">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="TituloItem" VisibleIndex="1" 
                                    Caption="Item" Width="240px">
                                    <EditFormSettings Visible="False" />
<EditFormSettings Visible="False"></EditFormSettings>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Importância" FieldName="Importancia" 
                                    VisibleIndex="2" Width="90px">
                                    <EditFormSettings Visible="False" />
                                    <Settings AllowAutoFilter="False" />
<EditFormSettings Visible="False"></EditFormSettings>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="DescricaoComplexidade" 
                                    VisibleIndex="4" Caption="Complexidade" Width="120px">
                                    <Settings AllowAutoFilter="False" />
                                    <EditFormSettings Visible="False" />
<EditFormSettings Visible="False"></EditFormSettings>
                                </dxwgv:GridViewDataTextColumn>
                                <dxtv:GridViewDataTextColumn Caption="Descrição" VisibleIndex="5" 
                                    FieldName="DetalheItem">
                                    <EditFormSettings Visible="False" />
<EditFormSettings Visible="False"></EditFormSettings>
                                </dxtv:GridViewDataTextColumn>
                                <dxtv:GridViewDataSpinEditColumn Caption="Estimativa" 
                                    FieldName="EsforcoPrevisto" VisibleIndex="3" Width="90px">
                                    <PropertiesSpinEdit AllowMouseWheel="False" DisplayFormatString="n0" 
                                        NumberFormat="Custom" NumberType="Integer">
                                        <SpinButtons ShowIncrementButtons="False">
                                        </SpinButtons>
                                    <ValidationSettings Display="Dynamic"><RequiredField ErrorText="Campo Obrigatório" 
                                            IsRequired="True" /></ValidationSettings></PropertiesSpinEdit>
                                    <Settings AllowAutoFilter="False" />
                                </dxtv:GridViewDataSpinEditColumn>
                            </Columns>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsEditing Mode="Batch">
                            </SettingsEditing>
                            <Settings VerticalScrollableHeight="280" VerticalScrollBarMode="Auto" />

<Settings VerticalScrollableHeight="280" VerticalScrollBarMode="Auto"></Settings>

                            <Styles>
                                <TitlePanel Font-Bold="True" Font-Size="10pt">
                                </TitlePanel>
                            </Styles>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-top: 10px">
                        <table>
                            <tr>
                                <td style="padding-right: 10px">
                        <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" 
                             Text="Selecionar" Width="90px">
                            <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                            <Paddings Padding="0px" />
<ClientSideEvents Click="function(s, e) {
	gvDados.PerformCallback();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
                        </dxe:ASPxButton>
                                </td>
                                <td>
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
