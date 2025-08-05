<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reapresentacaoDemanda.aspx.cs" Inherits="_Projetos_DadosProjeto_reapresentacaoDemanda" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
    <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="Protocolo" ClientInstanceName="gvDados" Width="100%"  ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnCustomCallback="gvDados_CustomCallback">

<Templates><FooterRow>
            <table cellspacing="0" cellpadding="0" border="0" ><tbody><TR ><td ><dxe:ASPxLabel runat="server" Text="Tarefa Concluída" ClientInstanceName="lblDescricaoConcluido"  ID="lblDescricaoConcluido"  ></dxe:ASPxLabel>
 </td><td style="WIDTH: 10px" ></td><td style="WIDTH: 10px; BACKGROUND-COLOR: green" ></td><td style="WIDTH: 10px" align="center" >|</td><td ><dxe:ASPxLabel runat="server" Text="Tarefa Atrasada" ClientInstanceName="lblDescricaoAtrasada"  ID="lblDescricaoAtrasada"  ></dxe:ASPxLabel>
 </td><td style="WIDTH: 10px" ></td><td style="WIDTH: 10px; BACKGROUND-COLOR: red" ></td><td style="WIDTH: 10px" align="center" >|</td><td ><dxe:ASPxLabel runat="server" Text="Tem Anotações" ClientInstanceName="lblDescricaoAnotacoes"  ID="lblDescricaoAnotacoes"  ></dxe:ASPxLabel>
 </td><td style="WIDTH: 10px" ></td><td ><IMG style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" src="../../imagens/anotacao.gif"  /> </td></tr></tbody></table>
        
</FooterRow>
</Templates>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings VerticalScrollBarMode="Visible"></Settings>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
<Columns>
<dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="60px" Caption=" " VisibleIndex="0"><CustomButtons>
<dxcp:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe" Visibility="Invisible">
<Image Url="~/imagens/botoes/pFormulario.png"></Image>
</dxcp:GridViewCommandColumnCustomButton>
</CustomButtons>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Right"></CellStyle>
    <HeaderTemplate>
        <table>
            <tr>
                <td align="center">
                    <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu" ItemSpacing="5px" oninit="menu_Init" onitemclick="menu_ItemClick">
                        <Paddings Padding="0px" />
                        <Items>
                            <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                <Image Url="~/imagens/botoes/incluirReg02.png">
                                </Image>
                            </dxtv:MenuItem>
                            <dxtv:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                <Items>
                                    <dxtv:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                        <Image Url="~/imagens/menuExportacao/xls.png">
                                        </Image>
                                    </dxtv:MenuItem>
                                    <dxtv:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                        <Image Url="~/imagens/menuExportacao/pdf.png">
                                        </Image>
                                    </dxtv:MenuItem>
                                    <dxtv:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                        <Image Url="~/imagens/menuExportacao/rtf.png">
                                        </Image>
                                    </dxtv:MenuItem>
                                    <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                        <Image Url="~/imagens/menuExportacao/html.png">
                                        </Image>
                                    </dxtv:MenuItem>
                                </Items>
                                <Image Url="~/imagens/botoes/btnDownload.png">
                                </Image>
                            </dxtv:MenuItem>
                            <dxtv:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                <Items>
                                    <dxtv:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                        <Image IconID="save_save_16x16">
                                        </Image>
                                    </dxtv:MenuItem>
                                    <dxtv:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                        <Image IconID="actions_reset_16x16">
                                        </Image>
                                    </dxtv:MenuItem>
                                </Items>
                                <Image Url="~/imagens/botoes/layout.png">
                                </Image>
                            </dxtv:MenuItem>
                        </Items>
                        <ItemStyle Cursor="pointer">
                        <HoverStyle>
                            <Border BorderStyle="None" />
                        </HoverStyle>
                        <Paddings Padding="0px" />
                        </ItemStyle>
                        <SubMenuItemStyle BackColor="White" Cursor="pointer">
                            <SelectedStyle>
                                <Border BorderStyle="None" />
                            </SelectedStyle>
                        </SubMenuItemStyle>
                        <Border BorderStyle="None" />
                    </dxtv:ASPxMenu>
                </td>
            </tr>
        </table>
    </HeaderTemplate>
</dxcp:GridViewCommandColumn>
<dxcp:GridViewDataTextColumn FieldName="Protocolo" VisibleIndex="1" Width="100px" Caption="Protocolo"></dxcp:GridViewDataTextColumn>
<dxcp:GridViewDataTextColumn FieldName="Descricao" VisibleIndex="2" Caption="Descrição"></dxcp:GridViewDataTextColumn>
<dxcp:GridViewDataTextColumn FieldName="Solicitante" VisibleIndex="4" Caption="Solicitante"></dxcp:GridViewDataTextColumn>
    <dxtv:GridViewDataDateColumn FieldName="DataAbertura" VisibleIndex="3" Width="120px" Caption="Data de Abertura">
        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
        </PropertiesDateEdit>
    </dxtv:GridViewDataDateColumn>
</Columns>
</dxcp:ASPxGridView>

        </div>
        <dxcp:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
        </dxcp:ASPxGridViewExporter>
        <dxcp:ASPxCallback ID="callbackAberturaFluxo" runat="server" ClientInstanceName="callbackAberturaFluxo" OnCallback="callbackAberturaFluxo_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
window.top.mostraMensagem(s.cp_mensagemRetorno, s.cp_codigoRetorno == '0' ? 'sucesso' : 'erro', true, false, atualizaGrid, 5000);
}" />
        </dxcp:ASPxCallback>

                            <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" Width="730px"  ID="pcDados">
<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxcp:PopupControlContentControl runat="server">
                                        <table cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-top: 10px" align="right">
                                                        <table id="Table1" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="padding-right: 10px" align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
                        if (window.onClick_btnCancelar)
                           onClick_btnCancelar();
                    }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxcp:ASPxButton>

                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxcp:PopupControlContentControl>
</ContentCollection>
</dxcp:ASPxPopupControl>

    </form>
</body>
</html>
