<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaTarefasAlertas.aspx.cs" Inherits="_Projetos_DadosProjeto_ListaTarefasAlertas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title>Alertas</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script language="javascript" type="text/javascript">
// <!CDATA[

// ]]>
    </script>
    <style type="text/css">
        .style1 {
            height: 10px;
        }

        .style2 {
            width: 100%;
        }

        .style7 {
            height: 5px;
        }

        .style8 {
            height: 21px;
        }

        .style9 {
            height: 11px;
        }
    </style>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td style="width: 10px; height: 10px"></td>
                    <td style="height: 10px"></td>
                    <td style="width: 10px; height: 10px;"></td>
                </tr>
                <tr>
                    <td style="width: 5px"></td>
                    <td>
                        <!-- PANELCALLBACK: pnCallback -->
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%"
                            ClientInstanceName="pnCallback">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <!-- ASPxGRIDVIEW: gvDados -->
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                        KeyFieldName="CodigoAlertaTarefa" AutoGenerateColumns="False" Width="850px"
                                        ID="gvDados"
                                        OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                                        OnCustomCallback="gvDados_CustomCallback">
                                        <ClientSideEvents CustomButtonClick="function(s, e) {
     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
		editaTarefa();
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
		excluiTarefa();
     }
     else if(e.buttonID == &quot;btnFormularioCustom&quot;)
     {	
		consultaTarefa();
     }		
}"></ClientSideEvents>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="95px"
                                                Caption=" " VisibleIndex="0">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                        <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                        <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
                                                        <Image Url="~/imagens/botoes/pFormulario.png"></Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                                <HeaderTemplate>
                                                    <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir"" onclick=""chamaPopUp('I', -1)"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Incluir"" style=""cursor: default;""/>")%>
                                                </HeaderTemplate>
                                            </dxwgv:GridViewCommandColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Tarefa" FieldName="NomeTarefa"
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="390" />
                                        <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                        <Templates>
                                            <FooterRow>
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel runat="server" Text="Tarefa Concluída" ClientInstanceName="lblDescricaoConcluido" ID="lblDescricaoConcluido"></dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 10px"></td>
                                                            <td style="width: 10px; background-color: green"></td>
                                                            <td style="width: 10px" align="center">|</td>
                                                            <td>
                                                                <dxe:ASPxLabel runat="server" Text="Tarefa Atrasada" ClientInstanceName="lblDescricaoAtrasada" ID="lblDescricaoAtrasada"></dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 10px"></td>
                                                            <td style="width: 10px; background-color: red"></td>
                                                            <td style="width: 10px" align="center">|</td>
                                                            <td>
                                                                <dxe:ASPxLabel runat="server" Text="Tem Anotações" ClientInstanceName="lblDescricaoAnotacoes" ID="lblDescricaoAnotacoes"></dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 10px"></td>
                                                            <td>
                                                                <img style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; border-right-width: 0px" src="../../imagens/anotacao.gif" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </FooterRow>
                                        </Templates>
                                    </dxwgv:ASPxGridView>
                                    <!-- PANEL CONTROL : pcDados -->
                                </dxp:PanelContent>
                            </PanelCollection>

                        </dxcp:ASPxCallbackPanel>
                    </td>
                    <td style="width: 5px"></td>
                </tr>
            </table>
        </div>
        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido"
            HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" PopupVerticalOffset="10" ShowHeader="False"
            Width="270px" ID="pcUsuarioIncluido">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td style="" align="center"></td>
                                <td style="width: 70px" align="center" rowspan="3">
                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxe:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>

        <dxpc:ASPxPopupControl ID="pcModal" runat="server"
            ClientInstanceName="pcModal"
            HeaderText="Associação de Tarefas ao Alerta"
            Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" AllowDragging="True"
            AllowResize="True" CloseAction="CloseButton" Width="720px">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                    <iframe id="frmModal" name="frmModal" frameborder="0"
                        style="overflow: auto; padding: 0px; margin: 0px;" height="400" width="100%"></iframe>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Closing="function(s, e) {
	document.getElementById('frmModal').src = &quot;&quot;;
	gvDados.PerformCallback();
}" />
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
        </dxpc:ASPxPopupControl>

    </form>
</body>
</html>
