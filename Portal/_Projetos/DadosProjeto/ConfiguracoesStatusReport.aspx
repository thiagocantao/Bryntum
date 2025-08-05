<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfiguracoesStatusReport.aspx.cs"
    Inherits="_Projetos_DadosProjeto_ConfiguracoesStatusReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <style type="text/css">
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
         <div id="divGrid" style="visibility:hidden">
        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoModeloStatusReport" AutoGenerateColumns="False" Width="100%" ID="gvDados" OnCustomCallback="gvDados_CustomCallback" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
            <ClientSideEvents CustomButtonClick="function(s, e) 
{
	onClick_CustomButtomGrid(s, e);
}
"   Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"
></ClientSideEvents>
            <Columns>
                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="Acao" Width="70px"
                    Caption=" " VisibleIndex="0" ShowClearFilterButton="true">
                    <CustomButtons>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnCompartilhar" Text="Identificar os destinat&#225;rios do Status Report">
                            <Image Url="~/imagens/compartilhar.PNG"></Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td align="center">
                                    <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                        ClientInstanceName="menu" ItemSpacing="5px" OnInit="menu_Init"
                                        OnItemClick="menu_ItemClick">
                                        <Paddings Padding="0px" />
                                        <Items>
                                            <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                <Image Url="~/imagens/botoes/incluirReg02.png">
                                                </Image>
                                            </dxm:MenuItem>
                                            <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                <Items>
                                                    <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                        <Image Url="~/imagens/menuExportacao/xls.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                        <Image Url="~/imagens/menuExportacao/pdf.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                        <Image Url="~/imagens/menuExportacao/rtf.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML"
                                                        ToolTip="Exportar para HTML">
                                                        <Image Url="~/imagens/menuExportacao/html.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                        <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                </Items>
                                                <Image Url="~/imagens/botoes/btnDownload.png">
                                                </Image>
                                            </dxm:MenuItem>
                                            <dxm:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                <Items>
                                                    <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                        <Image IconID="save_save_16x16">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                        <Image IconID="actions_reset_16x16">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                </Items>
                                                <Image Url="~/imagens/botoes/layout.png">
                                                </Image>
                                            </dxm:MenuItem>
                                        </Items>
                                        <ItemStyle Cursor="pointer">
                                            <HoverStyle>
                                                <border borderstyle="None" />
                                            </HoverStyle>
                                            <Paddings Padding="0px" />
                                        </ItemStyle>
                                        <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                            <SelectedStyle>
                                                <border borderstyle="None" />
                                            </SelectedStyle>
                                        </SubMenuItemStyle>
                                        <Border BorderStyle="None" />
                                    </dxm:ASPxMenu>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn FieldName="DescricaoModeloStatusReport" Caption="Descri&#231;&#227;o" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="DescricaoPeriodicidade_PT"
                    Caption="Periodicidade" VisibleIndex="2" Width="180px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="CodigoModeloStatusReport"
                    Name="CodigoModeloStatusReport" Visible="False" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="CodigoPeriodicidade"
                    Name="CodigoPeriodicidade" Visible="False" VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaTarefasAtrasadas"
                    Name="ListaTarefasAtrasadas" Visible="False" VisibleIndex="6">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaTarefasConcluidas"
                    Name="ListaTarefasConcluidas" Visible="False" VisibleIndex="7">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaTarefasFuturas"
                    Name="ListaTarefasFuturas" Visible="False" VisibleIndex="8">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaMarcosConcluidos"
                    Name="ListaMarcosConcluidos" Visible="False" VisibleIndex="9">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaMarcosAtrasados"
                    Name="ListaMarcosAtrasados" Visible="False" VisibleIndex="10">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaMarcosFuturos"
                    Name="ListaMarcosFuturos" Visible="False" VisibleIndex="11">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="GraficoDesempenhoFisico"
                    Name="GraficoDesempenhoFisico" Visible="False" VisibleIndex="12">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaRH" Name="ListaRH" Visible="False"
                    VisibleIndex="13">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="GraficoDesempenhoCusto"
                    Name="GraficoDesempenhoCusto" Visible="False" VisibleIndex="14">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaContasCusto" Name="ListaContasCusto"
                    Visible="False" VisibleIndex="15">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="GraficoDesempenhoReceita"
                    Name="GraficoDesempenhoReceita" Visible="False" VisibleIndex="16">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaContasReceita"
                    Name="ListaContasReceita" Visible="False" VisibleIndex="17">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="AnaliseValorAgregado"
                    Name="AnaliseValorAgregado" Visible="False" VisibleIndex="18">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaRiscosAtivos"
                    Name="ListaRiscosAtivos" Visible="False" VisibleIndex="19">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaRiscosEliminados"
                    Name="ListaRiscosEliminados" Visible="False" VisibleIndex="20">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaQuestoesAtivas"
                    Name="ListaQuestoesAtivas" Visible="False" VisibleIndex="21">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaQuestoesResolvidas"
                    Name="ListaQuestoesResolvidas" Visible="False" VisibleIndex="22">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaMetasResultados"
                    Name="ListaMetasResultados" Visible="False" VisibleIndex="23">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ListaPendenciasToDoList"
                    Name="ListaPendenciasToDoList" Visible="False" VisibleIndex="24">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ComentarioGeral" Name="ComentarioGeral"
                    Visible="False" VisibleIndex="25">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ComentarioFisico" Name="ComentarioFisico"
                    Visible="False" VisibleIndex="26">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ComentarioFinanceiro"
                    Name="ComentarioFinanceiro" Visible="False" VisibleIndex="27">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ComentarioRisco" Name="ComentarioRisco"
                    Visible="False" VisibleIndex="28">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ComentarioQuestao"
                    Name="ComentarioQuestao" Visible="False" VisibleIndex="29">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ComentarioMeta" Name="ComentarioMeta"
                    Visible="False" VisibleIndex="30">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="IndicaIncluiGraficoReceita"
                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True"></Settings>
            <SettingsText></SettingsText>
        </dxwgv:ASPxGridView>
        </div>
             <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcCompartilharIndicador" CloseAction="None" HeaderText="Destinat&#225;rios do Relat&#243;rio de Status" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="630px" ID="pcCompartilharIndicador">
            <ContentStyle>
                <Paddings Padding="5px"></Paddings>
            </ContentStyle>
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table style="width: 626px; height: 100px" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Nome do Relat&#243;rio:" ID="lblNomeRelatorio"></dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="30" ReadOnly="True" ClientInstanceName="txtNomeModelo" ID="txtNomeModelo">
                                        <ValidationSettings>
                                            <ErrorImage Height="14px" Url="~/App_Themes/MaterialCompact/Editors/edtError.png"></ErrorImage>

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black"></ReadOnlyStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td style="width: 280px" align="left">
                                                    <dxe:ASPxLabel runat="server" Text="Usu&#225;rios Dispon&#237;veis:" ID="ASPxLabel106"></dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 5px"></td>
                                                <td></td>
                                                <td style="width: 5px"></td>
                                                <td style="width: 280px" align="left">
                                                    <dxe:ASPxLabel runat="server" Text="Usu&#225;rios Selecionados:" ID="ASPxLabel107"></dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <dxtv:ASPxCallbackPanel ID="callback_lbItensDisponiveis" ClientInstanceName="callback_lbItensDisponiveis" runat="server" Width="100%" OnCallback="callback_lbItensDisponiveis_Callback">
                                                        <PanelCollection>
                                                            <dxtv:PanelContent runat="server">
                                                                <dxe:ASPxListBox FilteringSettings-ShowSearchUI="true" runat="server" EncodeHtml="False" Rows="3" SelectionMode="Multiple" ClientInstanceName="lbItensDisponiveis" EnableClientSideAPI="True" Width="100%" Height="<%# alturaListBox %>" ID="lbItensDisponiveis">
                                                                    <FilteringSettings ShowSearchUI="True"></FilteringSettings>

                                                                    <ClientSideEvents EndCallback="function(s, e) {
	UpdateButtons();
}"
                                                                        SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>
                                                                    <ValidationSettings>
                                                                        <ErrorImage Width="14px"></ErrorImage>
                                                                    </ValidationSettings>
                                                                </dxe:ASPxListBox>
                                                            </dxtv:PanelContent>
                                                        </PanelCollection>
                                                    </dxtv:ASPxCallbackPanel>

                                                </td>
                                                <td></td>
                                                <td valign="middle" align="center">
                                                    <table class="formulario-lista-botoes" cellspacing="0" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddAll" ClientEnabled="False" Text="&gt;&gt;" Width="40px" ToolTip="Selecionar todas as unidades" ID="btnAddAll">
                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensDisponiveis,lbItensSelecionados);
	UpdateButtons();
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddSel" ClientEnabled="False" Text="&gt;" Width="40px" ToolTip="Selecionar as unidades marcadas" ID="btnAddSel">
                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensDisponiveis, lbItensSelecionados);
	UpdateButtons();
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveSel" ClientEnabled="False" Text="&lt;" Width="40px" ToolTip="Retirar da sele&#231;&#227;o as unidades marcadas" ID="btnRemoveSel">
                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensSelecionados, lbItensDisponiveis);
	UpdateButtons();
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveAll" ClientEnabled="False" Text="&lt;&lt;" Width="40px" ToolTip="Retirar da sele&#231;&#227;o todas as unidades" ID="btnRemoveAll">
                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensSelecionados, lbItensDisponiveis);
	UpdateButtons();
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td></td>
                                                <td valign="top">
                                                    <dxtv:ASPxCallbackPanel ID="callback_lblItensSelecionados" ClientInstanceName="callback_lblItensSelecionados" runat="server" Width="100%" OnCallback="callback_lblItensSelecionados_Callback">
                                                        <PanelCollection>
                                                            <dxtv:PanelContent runat="server">
                                                                <dxe:ASPxListBox FilteringSettings-ShowSearchUI="true" runat="server" EncodeHtml="False" Rows="4" SelectionMode="Multiple" ClientInstanceName="lbItensSelecionados" EnableClientSideAPI="True" Width="100%" Height="<%# alturaListBox %>" ID="lbItensSelecionados">
                                                                    <FilteringSettings ShowSearchUI="True"></FilteringSettings>

                                                                    <ClientSideEvents EndCallback="function(s, e) {
UpdateButtons();
}"
                                                                        SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}"></ClientSideEvents>
                                                                    <ValidationSettings>
                                                                        <ErrorImage Width="14px"></ErrorImage>
                                                                    </ValidationSettings>
                                                                </dxe:ASPxListBox>
                                                            </dxtv:PanelContent>
                                                        </PanelCollection>
                                                    </dxtv:ASPxCallbackPanel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxCheckBox ID="cbIncluirGrafico" runat="server" CheckState="Unchecked"
                                        ClientInstanceName="cbIncluirGrafico" RootStyle-Paddings-PaddingTop="5px" RootStyle-Paddings-PaddingBottom="5px"
                                        Text="Incluir gráfico de receita no RAP da Unidade">
                                        <RootStyle>
                                            <Paddings PaddingTop="5px" PaddingBottom="5px"></Paddings>
                                        </RootStyle>
                                    </dxe:ASPxCheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <table class="formulario-botoes" id="tblSalvarFechar" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td class="formulario-botao">
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvarCompartilhar" CausesValidation="False" Text="Salvar" Width="100px" ID="btnSalvarCompartilhar">
                                                        <ClientSideEvents Click="function(s, e) {
                    UpdateButtons();
	                onClick_btnSalvarCompartilhar();
                }"></ClientSideEvents>
                                                    </dxe:ASPxButton>

                                                </td>
                                                <td class="formulario-botao">
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="100px" ID="ASPxButton2">
                                                        <ClientSideEvents Click="function(s, e) {
	                e.processOnServer = false;
                    pcCompartilharIndicador.Hide();
                }"></ClientSideEvents>
                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxcp:ASPxHiddenField>

        <dxcp:ASPxCallback runat="server" ClientInstanceName="callback" ID="callback" OnCallback="callback_Callback"></dxcp:ASPxCallback>

        <dxcp:ASPxCallback runat="server" ClientInstanceName="callback1" ID="callback1" OnCallback="callback1_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
     if(s.cp_Erro != '')
     {
             window.top.mostraMensagem(s.cp_Erro, 'erro',true, false, null);
     }
     else if(s.cp_Sucesso != '')
     {
           window.top.mostraMensagem(s.cp_Sucesso, 'sucesso',false, false, null);    
   }

}" />
        </dxcp:ASPxCallback>

        <dxcp:ASPxGridViewExporter runat="server" GridViewID="gvDados" ExportSelectedRowsOnly="true" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <GroupFooter Font-Bold="True"></GroupFooter>

                <Title Font-Bold="True"></Title>
            </Styles>
        </dxcp:ASPxGridViewExporter>

    </form>
</body>
</html>
