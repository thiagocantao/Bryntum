<%@ Page Language="C#" Title="Orçamentação Projeto" AutoEventWireup="true" CodeFile="frameGridOrcamentacaoProjeto.aspx.cs" Inherits="_Projetos_frameGridOrcamentacaoProjeto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Untitled Page</title>
    <style type="text/css">
        .marginLeft10 {
            margin-left: 10px;
        }

        .invisibleCol {
            display: none;
        }

        .dxgvControl_MaterialCompact .dxgvTable_MaterialCompact
        .dxgvFocusedRow_MaterialCompact,
        .dxgvControl_MaterialCompact
        .dxgvTable_MaterialCompact
        .dxgvFocusedRow_MaterialCompact.dxgvDataRowHover_MaterialCompact {
            background-color: #35B86B;
            color: #000000;
        }

        #tabOrcamentacao_gridReceita_DXFooterRow,
        #tabOrcamentacao_gridDespesa_DXFooterRow {
            text-align: right
        }
        .iniciaisMaiusculas{
            text-transform:capitalize !important;
        }
    </style>
    <script type="text/javascript">

        var nomeGridMemoriaCalculoClicada;
        var comandoExecutadoNaGrid;

        function atualizaGrid() {
            gridReceita.Refresh();
            gridDespesa.Refresh();
        }

        function abrePopupMemoriaCalculo(valor) {

            var codigoConta = (valor[0] != null ? valor[0] : "");
            var codigoMemoriaCalculo = (valor[1] != null ? valor[1] : "");
            var descricaoMemoriaCalculo = (valor[2] != null ? valor[2] : "");

            hfGeral.Set("CodigoConta", codigoConta);
            hfGeral.Set("CodigoMemoriaCalculo", codigoMemoriaCalculo);
            memoMemoriaCalculo.SetText(descricaoMemoriaCalculo);

            popupMemoriaCalculo.Show();
        }


        function gridReceita_saveChangesBtn_Click(s, e) {
            comandoExecutadoNaGrid = 'UPDATEEDIT';
            gridReceita.UpdateEdit();
        }

        function gridReceita_cancelChangesBtn_Click(s, e) {
            gridReceita.CancelEdit();
        }


        function gridDespesa_saveChangesBtn_Click(s, e) {
            comandoExecutadoNaGrid = 'UPDATEEDIT';
            gridDespesa.UpdateEdit();
        }

        function gridDespesa_cancelChangesBtn_Click(s, e) {
            gridDespesa.CancelEdit();
        }

    </script>

</head>

<body style="margin: 0px; font-size: 8pt; font-family: Verdana">

    <div id="divOrcamentacao">

        <form id="form1" runat="server">
            <dx:ASPxGridView ID="gridFakeDespesa"
                runat="server" Visible="False" AutoGenerateColumns="False">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="<%$ Resources:traducao, c_digo %>" Width="200" FieldName="GrupoConta" ReadOnly="true" />
                    <dx:GridViewDataTextColumn Caption="<%$ Resources:traducao, conta %>" Width="400" FieldName="DescConta" ReadOnly="true" />

                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="1">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="2">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="3">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="4">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="5">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="6">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="7">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="8">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="9">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="10">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="11">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="12">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" Caption="<%$ Resources:traducao, total_conta %>" FieldName="TotalConta" ReadOnly="true">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" Caption="<%$ Resources:traducao, or_amento_anterior %>" FieldName="ValorOrcamentoAnterior" ReadOnly="true">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" Caption="<%$ Resources:traducao, varia__o %>" FieldName="VariacaoOrcamentoAnterior" ReadOnly="true">
                        <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>

                    <dx:GridViewDataTextColumn Caption="Descrição Memória Cálculo" FieldName="DescricaoMemoriaCalculo" ShowInCustomizationForm="True" VisibleIndex="2" Width="300px" Visible="True">
                    </dx:GridViewDataTextColumn>

                </Columns>
            </dx:ASPxGridView>

            <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gridFakeDespesa" ID="ASPxGridViewExporter1"
                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                <Styles>
                    <Default Font-Names="Verdana" Font-Size="8pt"></Default>

                    <Header Font-Names="Verdana" Font-Size="9pt"></Header>

                    <Cell Font-Names="Verdana" Font-Size="8pt"></Cell>

                    <GroupFooter Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></GroupFooter>

                    <Title Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"></Title>
                </Styles>
            </dxwgv:ASPxGridViewExporter>

            <dx:ASPxGridView ID="gridFakeReceita"
                runat="server" Visible="false">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="<%$ Resources:traducao, c_digo %>" Width="200" FieldName="GrupoConta" ReadOnly="true" />
                    <dx:GridViewDataTextColumn Caption="<%$ Resources:traducao, conta %>" Width="400" FieldName="DescConta" ReadOnly="true" />

                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="1"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="2"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="3"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="4"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="5"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="6"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="7"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="8"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="9"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="10"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="11"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" FieldName="12"></dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Width="100" Caption="<%$ Resources:traducao, total_conta %>" FieldName="TotalConta" ReadOnly="true" />
                    <dx:GridViewDataSpinEditColumn Width="100" Caption="<%$ Resources:traducao, or_amento_anterior %>" FieldName="ValorOrcamentoAnterior" ReadOnly="true" />
                    <dx:GridViewDataSpinEditColumn Width="100" Caption="<%$ Resources:traducao, varia__o %>" FieldName="VariacaoOrcamentoAnterior" ReadOnly="true" />

                    <dx:GridViewDataTextColumn Caption="Descrição Memória Cálculo" FieldName="DescricaoMemoriaCalculo" ShowInCustomizationForm="True" VisibleIndex="2" Width="300px" Visible="True">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>

            <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gridFakeReceita" ID="ASPxGridViewExporter2"
                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                <Styles>
                    <Default Font-Names="Verdana" Font-Size="8pt"></Default>

                    <Header Font-Names="Verdana" Font-Size="9pt"></Header>

                    <Cell Font-Names="Verdana" Font-Size="8pt"></Cell>

                    <GroupFooter Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"></GroupFooter>

                    <Title Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"></Title>
                </Styles>
            </dxwgv:ASPxGridViewExporter>

            <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
            </dxcp:ASPxHiddenField>

            <dxcp:ASPxCallback ID="callbackMemoriaCalculo" runat="server" ClientInstanceName="callbackMemoriaCalculo" OnCallback="callbackMemoriaCalculo_Callback">
                <ClientSideEvents EndCallback="function(s, e) {
        if(s.cp_erro == '')
        {
                   if(s.cp_sucesso != '')
                   {   
                    window.top.mostraMensagem(s.cp_sucesso, 'sucesso', false, false, null, 3000);
         popupMemoriaCalculo.Hide();
                   }
         }
         else
        {
                  if(s.cp_erro != '')
                  {
                          window.top.mostraMensagem(s.cp_erro, 'erro', true, false, null);
                  }
        }                    
} " />
            </dxcp:ASPxCallback>

            <dxcp:ASPxCallback ID="callbackIncluirConta" runat="server" ClientInstanceName="callbackIncluirConta" OnCallback="callbackIncluirConta_Callback">
                <ClientSideEvents EndCallback="function(s, e) {
        if(s.cp_erro != '')
        {
                   window.top.mostraMensagem(s.cp_erro, 'erro', true, false, null);         
        }
         else
        {
                    gridReceita.PerformCallback();
                   gridDespesa.PerformCallback();
        }                    
}" />
            </dxcp:ASPxCallback>
            <dxcp:ASPxPopupControl ID="popupMemoriaCalculo" runat="server" ClientInstanceName="popupMemoriaCalculo" Height="250px" PopupAction="None" Width="584px" Modal="True" PopupAnimationType="Fade" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Memória de Cálculo" CloseAction="CloseButton" CloseAnimationType="Fade">
                <ContentCollection>
                    <dxcp:PopupControlContentControl runat="server">
                        <dxtv:ASPxMemo ID="memoMemoriaCalculo" runat="server" ClientInstanceName="memoMemoriaCalculo" Height="160px" Width="100%">
                        </dxtv:ASPxMemo>

                        <table style="width: 100%">
                            <tr>
                                <td align="right">
                                    <dxtv:ASPxLabel ID="lblContadorMemo" runat="server" ClientInstanceName="lblContadorMemo" Text="ASPxLabel">
                                        <DisabledStyle ForeColor="#999999">
                                        </DisabledStyle>
                                    </dxtv:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 200px">
                                        <tbody>
                                            <tr>
                                                <td class="formulario-botao">
                                                    <dxtv:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar1" Text="Salvar" Width="90px">
                                                        <ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
	                    callbackMemoriaCalculo.PerformCallback(hfGeral.Get(&quot;CodigoConta&quot;) + '|' + hfGeral.Get(&quot;CodigoMemoriaCalculo&quot;));
                    }" />
                                                        <Paddings Padding="0px" />
                                                    </dxtv:ASPxButton>
                                                </td>
                                                <td class="formulario-botao">
                                                    <dxtv:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px">
                                                        <ClientSideEvents Click="function(s, e) {
	                                                        e.processOnServer = false;
                                                            popupMemoriaCalculo.Hide();
                                                            noEdit();
                    }" />
                                                        <Paddings Padding="0px" />
                                                    </dxtv:ASPxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>



                    </dxcp:PopupControlContentControl>
                </ContentCollection>
            </dxcp:ASPxPopupControl>

            <dx:ASPxPageControl ID="tabOrcamentacao" Width="100%" runat="server" CssClass="dxtcFixed" ActiveTabIndex="0" EnableHierarchyRecreation="True" TabPosition="Left">
                <TabPages>
                    <dx:TabPage Text="<%$ Resources:traducao, despesas %>">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControlDespesa" runat="server">

                                <div>
                                    <div style="float: left; vertical-align: bottom; padding-top: 10px">
                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                            ItemSpacing="5px" OnItemClick="menu_ItemClickDespesa" OnInit="menu_InitDespesa">
                                            <Paddings Padding="0px" />
                                            <Items>
                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="<%$ Resources:traducao, incluir %>">
                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                    </Image>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem Name="btnExportar" Text="<%$ Resources:traducao, exportar %>" ToolTip="<%$ Resources:traducao, exportar %>">
                                                    <Items>
                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="<%$ Resources:traducao, exportar_para_xls %>">
                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="<%$ Resources:traducao, exportar_para_pdf %>" ClientVisible="False">
                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="<%$ Resources:traducao, exportar_para_rtf %>" ClientVisible="False">
                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="<%$ Resources:traducao, exportar_para_html %>"
                                                            ClientVisible="False">
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
                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="<%$ Resources:traducao, layout %>">
                                                    <Items>
                                                        <dxm:MenuItem Text="<%$ Resources:traducao, salvar %>" ToolTip="<%$ Resources:traducao, salvar_layout %>">
                                                            <Image IconID="save_save_16x16">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem Text="<%$ Resources:traducao, restaurar %>" ToolTip="<%$ Resources:traducao, restaurar_layout %>">
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
                                    </div>
                                    <div style="float: left; vertical-align: bottom; margin-left: 10px; padding-top: 10px">
                                        <dxtv:ASPxImage ID="imagemFiltroProjetoDespesa" runat="server" Cursor="pointer" ImageUrl="~/imagens/filtroOrcamentacaoProjeto.png" ToolTip="Escolher contas" ClientInstanceName="imagemFiltroProjetoDespesa">
                                            <ClientSideEvents Click="function(s, e) {
     const urlParams = new URLSearchParams(window.location.search);
     const codigoProjeto = urlParams.get('CP');
     //showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam)
       var sUrl = window.top.pcModal.cp_Path + '_Projetos/DadosProjeto/AssociacaoContas.aspx?CP=' + codigoProjeto + '&amp;ISPOPUP=S';
     window.top.showModal(sUrl, 'Associação', null, null, atualizaGrid, null);
}" />
                                        </dxtv:ASPxImage>
                                    </div>
                                    <div style="float: left; vertical-align: bottom; margin-left: 5px; padding-top: 10px">
                                        <dxtv:ASPxLabel ID="lblEscolherContas1" runat="server" Text="Escolher contas" ForeColor="Black">
                                        </dxtv:ASPxLabel>
                                    </div>
                                    <div style="float: left; vertical-align: top; margin-left: 5px">
                                        <dxtv:ASPxComboBox ID="comboEscolheContaDespesa" Width="270px" ClientInstanceName="comboEscolheContaDespesa" runat="server">
                                        </dxtv:ASPxComboBox>
                                    </div>
                                    <div style="float: left; vertical-align: top; margin-left: 5px">
                                        <dxtv:ASPxButton ID="btnIncluiContaDespesa" Text="Incluir Nova Conta" ClientInstanceName="btnIncluiContaDespesa" CssClass="iniciaisMaiusculas" runat="server" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
            if(comboEscolheContaDespesa.GetSelectedIndex() != -1)
           {
                     callbackIncluirConta.PerformCallback();
           }
}" />
                                        </dxtv:ASPxButton>
                                    </div>
                                    <div style="float: right; padding-bottom: 10px;">
                                        <dx:ASPxButton ID="gridDespesa_saveChangesBtn" runat="server" Text="<%$ Resources:traducao, salvar %>" AutoPostBack="false"
                                            ClientInstanceName="gridDespesa_saveChangesBtn" ClientEnabled="false">
                                            <ClientSideEvents Click="gridDespesa_saveChangesBtn_Click" />
                                        </dx:ASPxButton>
                                        <dx:ASPxButton ID="gridDespesa_cancelChangesBtn" runat="server" Text="<%$ Resources:traducao, cancelar %>" AutoPostBack="false"
                                            ClientInstanceName="gridDespesa_cancelChangesBtn" ClientEnabled="false">
                                            <ClientSideEvents Click="gridDespesa_cancelChangesBtn_Click" />
                                        </dx:ASPxButton>
                                    </div>

                                </div>

                                <dx:ASPxGridView ID="gridDespesa"
                                    ClientInstanceName="gridDespesa"
                                    runat="server"
                                    OnHtmlDataCellPrepared="gridHtmlDataCellPrepared"
                                    OnBatchUpdate="grid_BatchUpdate"
                                    OnHtmlRowCreated="grid_HtmlRowCreated"
                                    KeyFieldName="GrupoConta" Width="100%" CssClass="gridWidth" OnCustomButtonInitialize="gridDespesa_CustomButtonInitialize" OnCustomCallback="gridDespesa_CustomCallback1">
                                    <ClientSideEvents
                                        BatchEditStartEditing="
                                        function(s, e) {
                                            gridDespesa_saveChangesBtn.SetEnabled(true);
                                            gridDespesa_cancelChangesBtn.SetEnabled(true);
                                        }"
                                        BatchEditChangesCanceling="
                                        function(s, e) {
                                            gridDespesa_saveChangesBtn.SetEnabled(false);
                                            gridDespesa_cancelChangesBtn.SetEnabled(false);
                                        }"
                                        BatchEditChangesSaving="
                                        function(s, e) {
                                            gridDespesa_saveChangesBtn.SetEnabled(false);
                                            gridDespesa_cancelChangesBtn.SetEnabled(false);
                                        }"
                                        CustomButtonClick="function(s, e) 
{
          if(e.buttonID == 'btnMemoriaCalculoDespesa')
         {
                                        nomeGridMemoriaCalculoClicada = s.name;
                                        s.GetRowValues(e.visibleIndex, 'CodConta;CodigoMemoriaCalculo;DescricaoMemoriaCalculo', abrePopupMemoriaCalculo);
          }
}"
                                        EndCallback="function(s, e) {
if(comandoExecutadoNaGrid === 'UPDATEEDIT')
                                        {
                                         comandoExecutadoNaGrid = '';
                                        s.Refresh();
                                        }
	
}" Init="function(s, e) {
                                        
                                        var sHeight = Math.max(0, document.documentElement.clientHeight) - 75;
                                        s.SetHeight(sHeight);
                        }"/>

                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                    <Settings VerticalScrollableHeight="594" ShowStatusBar="Hidden" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"
                                        ShowFooter="True" ShowGroupButtons="False" ShowGroupPanel="False"
                                        ShowGroupFooter="VisibleAlways" />
                                    <SettingsEditing Mode="Batch" />
                                    <SettingsBehavior AllowDragDrop="False" AllowSort="False" />

                                    <SettingsCommandButton>
                                        <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>
                                        <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                    </SettingsCommandButton>

                                    <Styles>
                                        <Footer Font-Bold="True"></Footer>
                                    </Styles>

                                    <SettingsPopup>
                                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                                    </SettingsPopup>
                                    <Columns>

                                        <dx:GridViewDataTextColumn Caption="<%$ Resources:traducao, c_digo %>" Width="200" FieldName="GrupoConta" ReadOnly="true" VisibleIndex="3" />
                                        <dx:GridViewDataTextColumn Caption="<%$ Resources:traducao, conta %>" Width="400" FieldName="DescConta" ReadOnly="true" VisibleIndex="4" />

                                        <dx:GridViewDataSpinEditColumn FieldName="1" HeaderStyle-CssClass="clickPopUp" VisibleIndex="5">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>

                                            <HeaderStyle CssClass="clickPopUp"></HeaderStyle>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="2" VisibleIndex="6">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="3" VisibleIndex="7">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn FieldName="4" VisibleIndex="8">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="5" VisibleIndex="9">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="6" VisibleIndex="10">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn FieldName="7" VisibleIndex="11">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="8" VisibleIndex="12">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="9" VisibleIndex="13">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn FieldName="10" VisibleIndex="14">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="11" VisibleIndex="15">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="12" VisibleIndex="16">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="<%$ Resources:traducao, total_conta %>" FieldName="TotalConta" ReadOnly="true" VisibleIndex="17">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Width="100" Caption="<%$ Resources:traducao, or_amento_anterior %>" FieldName="ValorOrcamentoAnterior" ReadOnly="true" VisibleIndex="18">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Width="100" Caption="<%$ Resources:traducao, varia__o %>" FieldName="VariacaoOrcamentoAnterior" ReadOnly="true" VisibleIndex="19">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataTextColumn Width="0" Caption="CodConta" FieldName="CodConta" HeaderStyle-CssClass="invisibleCol" CellStyle-CssClass="invisibleCol" VisibleIndex="20">
                                            <HeaderStyle CssClass="invisibleCol"></HeaderStyle>

                                            <CellStyle CssClass="invisibleCol"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Width="0" Caption="ListValor" FieldName="ListValor" HeaderStyle-CssClass="invisibleCol" CellStyle-CssClass="invisibleCol" VisibleIndex="21">

                                            <HeaderStyle CssClass="invisibleCol"></HeaderStyle>

                                            <CellStyle CssClass="invisibleCol"></CellStyle>
                                        </dx:GridViewDataTextColumn>

                                        <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0" Caption=" Memória" Width="70px" ToolTip="Memória de cálculo">
                                            <CustomButtons>
                                                <dxtv:GridViewCommandColumnCustomButton ID="btnMemoriaCalculoDespesa">
                                                    <Image Height="16px" Url="~/imagens/botoes/calculadora.png" Width="16px" AlternateText="Memória de cálculo" ToolTip="Memória de cálculo">
                                                    </Image>
                                                </dxtv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dxtv:GridViewCommandColumn>
                                        <dxtv:GridViewDataTextColumn Caption="Descrição Memória Cálculo" FieldName="DescricaoMemoriaCalculo" ShowInCustomizationForm="True" VisibleIndex="2" Width="300px" Visible="False">
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="Código Memória Cálculo" FieldName="CodigoMemoriaCalculo" Name="colMemoriaCalculo" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            <PropertiesTextEdit DisplayFormatString="g">
                                            </PropertiesTextEdit>
                                        </dxtv:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="<%$ Resources:traducao, receitas %>">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControlReceita" runat="server">

                                <div>
                                    <div style="float: left; padding-top: 10px">

                                        <dxm:ASPxMenu ID="ASPxMenu1" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                            ItemSpacing="5px" OnItemClick="menu_ItemClickReceita" OnInit="menu_InitReceita">
                                            <Paddings Padding="0px" />
                                            <Items>
                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="<%$ Resources:traducao, incluir %>">
                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                    </Image>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem Name="btnExportar" Text="<%$ Resources:traducao, exportar %>" ToolTip="<%$ Resources:traducao, exportar %>">
                                                    <Items>
                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="<%$ Resources:traducao, exportar_para_xls %>">
                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="<%$ Resources:traducao, exportar_para_pdf %>" ClientVisible="False">
                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="<%$ Resources:traducao, exportar_para_rtf %>" ClientVisible="False">
                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="<%$ Resources:traducao, exportar_para_html %>"
                                                            ClientVisible="False">
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
                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="<%$ Resources:traducao, layout %>">
                                                    <Items>
                                                        <dxm:MenuItem Text="<%$ Resources:traducao, salvar %>" ToolTip="<%$ Resources:traducao, salvar_layout %>">
                                                            <Image IconID="save_save_16x16">
                                                            </Image>
                                                        </dxm:MenuItem>
                                                        <dxm:MenuItem Text="<%$ Resources:traducao, restaurar %>" ToolTip="<%$ Resources:traducao, restaurar_layout %>">
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

                                    </div>
                                    <div style="float: left; vertical-align: bottom; margin-left: 10px; padding-top: 10px">
                                        <dxtv:ASPxImage ID="imagemFiltroProjetoReceita" runat="server" ImageUrl="~/imagens/filtroOrcamentacaoProjeto.png" Cursor="pointer" ToolTip="Escolher contas" ClientInstanceName="imagemFiltroProjetoReceita">
                                            <ClientSideEvents Click="function(s, e) {
     const urlParams = new URLSearchParams(window.location.search);
     const codigoProjeto = urlParams.get('CP');
     //showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam)
     var sUrl = window.top.pcModal.cp_Path + '_Projetos/DadosProjeto/AssociacaoContas.aspx?CP=' + codigoProjeto + '&ISPOPUP=S';
     window.top.showModal(sUrl, 'Associação', null, null, atualizaGrid, null);
}" />
                                        </dxtv:ASPxImage>
                                    </div>
                                    <div style="float: left; vertical-align: bottom; margin-left: 5px; padding-top: 10px">
                                        <dxtv:ASPxLabel ID="lblEscolherContas2" runat="server" Text="Escolher contas" ForeColor="Black">
                                        </dxtv:ASPxLabel>
                                    </div>
                                    <div style="float: left; vertical-align: top; margin-left: 5px">
                                        <dxtv:ASPxComboBox ID="comboEscolheContaReceita" Width="270px" ClientInstanceName="comboEscolheContaReceita" runat="server">
                                        </dxtv:ASPxComboBox>
                                    </div>
                                    <div style="float: left; vertical-align: top; margin-left: 5px">
                                        <dxtv:ASPxButton ID="btnIncluiContaReceita" Text="Incluir Nova Conta" ClientInstanceName="btnIncluiContaReceita" CssClass="iniciaisMaiusculas" runat="server" AutoPostBack="False" CausesValidation="False">
                                            <ClientSideEvents Click="function(s, e) {
            if(comboEscolheContaReceita.GetSelectedIndex() != -1)
           {
                     callbackIncluirConta.PerformCallback();
           }
          
}" />
                                        </dxtv:ASPxButton>
                                    </div>
                                    <div style="float: right; padding-bottom: 10px;">

                                        <dx:ASPxButton ID="gridReceita_saveChangesBtn" runat="server" Text="<%$ Resources:traducao, salvar %>" AutoPostBack="false"
                                            ClientInstanceName="gridReceita_saveChangesBtn" ClientEnabled="false">
                                            <ClientSideEvents Click="gridReceita_saveChangesBtn_Click" />
                                        </dx:ASPxButton>
                                        <dx:ASPxButton ID="gridReceita_cancelChangesBtn" runat="server" Text="<%$ Resources:traducao, cancelar %>" AutoPostBack="false"
                                            ClientInstanceName="gridReceita_cancelChangesBtn" ClientEnabled="false">
                                            <ClientSideEvents Click="gridReceita_cancelChangesBtn_Click" />
                                        </dx:ASPxButton>

                                    </div>
                                </div>

                                <dx:ASPxGridView ID="gridReceita" ClientInstanceName="gridReceita"
                                    runat="server"
                                    OnBatchUpdate="grid_BatchUpdate"
                                    OnHtmlDataCellPrepared="gridHtmlDataCellPrepared"
                                    OnHtmlRowCreated="grid_HtmlRowCreated"
                                    OnCustomButtonInitialize="gridReceita_CustomButtonInitialize"
                                    KeyFieldName="GrupoConta" Width="100%" OnCustomCallback="gridReceita_CustomCallback1">
                                    <ClientSideEvents
                                        BatchEditStartEditing="
                                        function(s, e) {
                                            gridReceita_saveChangesBtn.SetEnabled(true);
                                            gridReceita_cancelChangesBtn.SetEnabled(true);
                                        }"
                                        BatchEditChangesCanceling="
                                        function(s, e) {
                                            gridReceita_saveChangesBtn.SetEnabled(false);
                                            gridReceita_cancelChangesBtn.SetEnabled(false);
                                        }"
                                        BatchEditChangesSaving="
                                        function(s, e) {
                                            gridReceita_saveChangesBtn.SetEnabled(false);
                                            gridReceita_cancelChangesBtn.SetEnabled(false);
                                        }"
                                        CustomButtonClick="function(s, e) {
          if(e.buttonID == 'btnMemoriaCalculoReceita')
         {
                   nomeGridMemoriaCalculoClicada = s.name;
                  s.GetRowValues(e.visibleIndex, 'CodConta;CodigoMemoriaCalculo;DescricaoMemoriaCalculo', abrePopupMemoriaCalculo);
          }
}"
                                        EndCallback="function(s, e) {
	if(comandoExecutadoNaGrid === 'UPDATEEDIT')
                                        {
                                        s.Refresh();
                                         comandoExecutadoNaGrid = '';
                                        }
}" Init="function(s, e) {
	                                        var sHeight = Math.max(0, document.documentElement.clientHeight) - 75;
                                        s.SetHeight(sHeight);

}" />

                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                    <Settings VerticalScrollableHeight="594" ShowStatusBar="Hidden" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"
                                        ShowFooter="True" ShowGroupButtons="False" ShowGroupPanel="False"
                                        ShowGroupFooter="VisibleAlways" />
                                    <SettingsEditing Mode="Batch" />
                                    <SettingsBehavior AllowDragDrop="False" AllowSort="False" />

                                    <SettingsCommandButton>
                                        <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>
                                        <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                    </SettingsCommandButton>

                                    <Styles>
                                        <Footer Font-Bold="True"></Footer>
                                    </Styles>

                                    <SettingsPopup>
                                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                                    </SettingsPopup>
                                    <Columns>

                                        <dx:GridViewDataTextColumn Caption="<%$ Resources:traducao, c_digo %>" Width="200" FieldName="GrupoConta" ReadOnly="true" VisibleIndex="3" />
                                        <dx:GridViewDataTextColumn Caption="<%$ Resources:traducao, conta %>" Width="400" FieldName="DescConta" ReadOnly="true" VisibleIndex="4" />

                                        <dx:GridViewDataSpinEditColumn FieldName="1" VisibleIndex="5">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="2" VisibleIndex="6">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="3" VisibleIndex="7">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn FieldName="4" VisibleIndex="8">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="5" VisibleIndex="9">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="6" VisibleIndex="10">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn FieldName="7" VisibleIndex="11">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="8" VisibleIndex="12">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="9" VisibleIndex="13">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn FieldName="10" VisibleIndex="14">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="11" VisibleIndex="15">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="12" VisibleIndex="16">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="<%$ Resources:traducao, total_conta %>" FieldName="TotalConta" ReadOnly="true" VisibleIndex="17">

                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn Width="100" Caption="<%$ Resources:traducao, or_amento_anterior %>" FieldName="ValorOrcamentoAnterior" ReadOnly="true" VisibleIndex="18">
                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Width="100" Caption="<%$ Resources:traducao, varia__o %>" FieldName="VariacaoOrcamentoAnterior" ReadOnly="true" VisibleIndex="19">

                                            <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataTextColumn Width="0" FieldName="CodConta" HeaderStyle-CssClass="invisibleCol" CellStyle-CssClass="invisibleCol" VisibleIndex="20">
                                            <HeaderStyle CssClass="invisibleCol"></HeaderStyle>

                                            <CellStyle CssClass="invisibleCol"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Width="0" FieldName="ListValor" HeaderStyle-CssClass="invisibleCol" CellStyle-CssClass="invisibleCol" VisibleIndex="21">

                                            <HeaderStyle CssClass="invisibleCol"></HeaderStyle>

                                            <CellStyle CssClass="invisibleCol"></CellStyle>
                                        </dx:GridViewDataTextColumn>

                                        <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0" Caption=" Memória" Width="70px" ToolTip="Memória de cálculo">
                                            <CustomButtons>
                                                <dxtv:GridViewCommandColumnCustomButton ID="btnMemoriaCalculoReceita">
                                                    <Image Height="16px" Url="~/imagens/botoes/calculadora.png" Width="16px" AlternateText="Memória de cálculo" ToolTip="Memória de cálculo">
                                                    </Image>
                                                </dxtv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dxtv:GridViewCommandColumn>
                                        <dxtv:GridViewDataTextColumn Caption="Descrição Memória Cálculo" FieldName="DescricaoMemoriaCalculo" ShowInCustomizationForm="True" VisibleIndex="2" Visible="False" Width="300px">
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption="Código Memória Calculo" FieldName="CodigoMemoriaCalculo" Name="colMemoriaCalculoReceita" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            <PropertiesTextEdit DisplayFormatString="g">
                                            </PropertiesTextEdit>
                                        </dxtv:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </form>
    </div>
    <script type="text/javascript" src="../../Bootstrap/vendor/jquery/v3.3.1/jquery-3.3.1.min.js"></script>
    <script type="text/javascript">
        var isSemProjeto ='<%=isSemProjeto%>';
        console.log('isSemProjeto:', isSemProjeto);
        if (isSemProjeto == 'True') {
            $('#divOrcamentacao').hide();
            var msg = '<%=msgSemProjeto%>';
            window.top.mostraMensagem(msg, 'atencao', true, false, null);
        } else {
            $('#divOrcamentacao').show();
        }

        var noEdit = function () {
            $('td[noeditfieldvalor]').click(function () { return false; });
            console.log('Teste No Edit 2');
        }
        $(function () {
            noEdit();
        });
    </script>
</body>
</html>




