<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detalhesObjetivoEstrategico.aspx.cs"
    Inherits="_Estrategias_detalhesObjetivoEstrategico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript">
        var tipoEdicao = 'I';
        var tipoOperacaoAssociaIndicador = '';
        function getCodigoProjeto(indexLinha) {
            gridProjetos.GetRowValues(indexLinha, 'Codigo;Prioridade;CodigoIndicador;Descricao;Tipo;PesoObjetoLink', abreEdicaoProjeto);
        }

        function abreEdicaoProjeto(valores) {
            var codigo = valores[0];
            var tipo = valores[4];
            if (tipo == 'projeto') {
                var prioridade = valores[1];
                var codigoIndicador = valores[2] != null ? valores[2] : "-1";
                var descricao = valores[3];
                var PesoObjetoLink = (valores[5] != null) ? valores[5] : "";
                if (window.PesoObjetoLink2) {
                    PesoObjetoLink2.SetValue(PesoObjetoLink);
                }
                ddlProjeto.SetValue(codigo);
                //ddlProjeto.SetText(descricao);

                txtNomeProjeto.SetVisible(true);
                ddlProjeto.SetVisible(false);
                txtNomeProjeto.SetText(descricao);
                txtNomeProjeto.SetEnabled(false);

                tipoEdicao = 'E';

                //mostraTabelas("E");
                pcAssociarProjeto.Show();

                if (prioridade != null && prioridade != "")
                    ddlPrioridade.SetValue(prioridade.toString());

                
                gvAcoes.PerformCallback(codigo);
            }
            else {
                var url = window.top.pcModal.cp_Path + 'espacoTrabalho/tarefasPlanoAcao.aspx?RO=N&ITA=OB&COA=' + gridProjetos.cp_COE + '&CTDL=' + codigo + '&UN=' + gridProjetos.cp_UN;
                window.top.showModal2(url + '&AT=' + (screen.height - 380), traducao.detalhesObjetivoEstrategico_plano_de_trabalho, screen.width - 40, screen.height - 240, function (e) { gridProjetos.PerformCallback('Atualizar') });
            }
        }

        function mostraTabelas(tipo) {
            if (tipo == "I") {
                ddlProjeto.SetEnabled(true);
                ddlProjeto.SetValue(null);
                ddlProjeto.SetText('');
                tipoEdicao = 'I';
            }
            else {
                ddlProjeto.SetEnabled(false);
                tipoEdicao = 'E';
            }
        }
        function abreGanttOE(urlGantt) {
            window.top.showModal(urlGantt, traducao.detalhesObjetivoEstrategico_gantt_dos_projetos_associados_ao_objetivo_estrat_gico_selecionado, screen.width - 40, screen.height - 240, "", null);
        }

        function abrePopupEdicaoIndicador(valores) {
            tipoOperacaoAssociaIndicador = 'E';
            var CodigoIndicador = (valores[0] == null) ? "" : valores[0].toString();
            var PesoObjetoLink = (valores[1] == null) ? "" : valores[1].toString();
            var NomeIndicador = (valores[2] == null) ? "" : valores[2].toString();
            ddlIndicadorAssociado.SetValue(CodigoIndicador);
            ddlIndicadorAssociado.SetText(NomeIndicador);
            ddlIndicadorAssociado.SetEnabled(false);
            spnPeso.SetValue(PesoObjetoLink);
            pcAssociaIndicadorPeso.Show();
        }

        function abrePopupAssociaIndicadorModoInclusao() {
            tipoOperacaoAssociaIndicador = 'I';
            ddlIndicadorAssociado.SetValue(null);
            ddlIndicadorAssociado.SetText('');
            ddlIndicadorAssociado.SetEnabled(true);
            spnPeso.SetValue(null);
            pcAssociaIndicadorPeso.Show();
        }

        function validaCamposFormularioAssociaIndicador() {
            var utilizaPeso = btnSalvarAssociacaoIndicador.cp_utilizaPesoDesempenhoObjetivo;

            var mensagemErro_ValidaCamposFormulario = "";
            var countMsg = 0;
            if (tipoOperacaoAssociaIndicador == 'I') {
                if (ddlIndicadorAssociado.GetValue() == null) {
                    mensagemErro_ValidaCamposFormulario += countMsg++ + ") O indicador associado deverá ser informado. \n";
                }
            }
            if (utilizaPeso == 'S') {
                if (spnPeso.GetValue() == null) {
                    mensagemErro_ValidaCamposFormulario += countMsg++ + ") O peso do indicador associado deverá ser informado. \n";
                }
            }
            return mensagemErro_ValidaCamposFormulario;

        }

        function excluiIndicadorAssociado(codigoIndicador)
        {
            
            var funcObj = { funcaoClickOK: function (codigoIndicadorParam) { gridIndicadores.PerformCallback("Excluir|" + codigoIndicadorParam); } };
            window.top.mostraMensagem('Deseja realmente excluir o indicador associado?', 'confirmacao', true, true, function(){funcObj['funcaoClickOK'](codigoIndicador)});
        }

        function excluiProjetoAssociado(codigoProjeto)
        {
            var funcObj = { funcaoClickOK: function (codigoProjetoParam) { gridProjetos.PerformCallback("Excluir|" + codigoProjetoParam); } };
            window.top.mostraMensagem('Deseja realmente excluir o projeto associado?', 'confirmacao', true, true, function(){funcObj['funcaoClickOK'](codigoProjeto)});
        }
    </script>
    <style>
        .gridBottom {
            margin-bottom: 10px;
        }

        #gridIndicadores_DXPEForm_efnew_DXEFL_1 {
            width: 9% !important;
        }

        #pcAssociarProjeto_PesoObjetoLink2_I {
            width: 40px;
        }

        #pcAssociarProjeto_tdPeso2 {
            padding-left: 10px;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <dxhf:ASPxHiddenField ID="hfGeral" ClientInstanceName="hfGeral" runat="server">
        </dxhf:ASPxHiddenField>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" CssClass="campo-label" runat="server"
                                                Text="<%$ Resources:traducao, objetivo_estrat_gico %>"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td style="width: 280px">
                                            <asp:Label ID="Label8" runat="server" CssClass="campo-label" Text="<%$ Resources:traducao, detalhesObjetivoEstrategico_respons_vel_ %>"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>

                                        <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientInstanceName="txtMapa"
                                            ReadOnly="True" Width="100%" Visible="False">
                                            <ReadOnlyStyle BackColor="#E0E0E0">
                                            </ReadOnlyStyle>
                                        </dxe:ASPxTextBox>



                                        <dxe:ASPxTextBox ID="txtPerspectiva" runat="server" ClientInstanceName="txtPerspectiva"
                                            ReadOnly="True" Width="100%" Visible="False">
                                            <ReadOnlyStyle BackColor="#E0E0E0">
                                            </ReadOnlyStyle>
                                        </dxe:ASPxTextBox>



                                        <dxe:ASPxTextBox ID="txtTema" runat="server" ClientInstanceName="txtTema"
                                            ReadOnly="True" Width="100%" Visible="False">
                                            <ReadOnlyStyle BackColor="#E0E0E0">
                                            </ReadOnlyStyle>
                                        </dxe:ASPxTextBox>


                                        <td style="width: 50%">
                                            <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server" ClientInstanceName="txtObjetivoEstrategico"
                                                ReadOnly="True" Width="100%">
                                                <ReadOnlyStyle BackColor="#E0E0E0">
                                                </ReadOnlyStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td></td>
                                        <td style="width: 50%">
                                            <dxe:ASPxTextBox ID="txtResponsavel" runat="server" ClientInstanceName="txtResponsavel"
                                                ReadOnly="True" Width="100%">
                                                <ReadOnlyStyle BackColor="#E0E0E0">
                                                </ReadOnlyStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px;"></td>
                        </tr>

                        <tr>
                            <td>
                                <!--GRID INDICADORES -->
                                <dxwgv:ASPxGridView ID="gridIndicadores" runat="server" AutoGenerateColumns="False" CssClass="gridBottom"
                                    ClientInstanceName="gridIndicadores" OnBeforeColumnSortingGrouping="gridIndicadores_BeforeColumnSortingGrouping"
                                    Width="100%" OnHtmlDataCellPrepared="gridIndicadores_HtmlDataCellPrepared" KeyFieldName="CodigoIndicador"
                                    OnCommandButtonInitialize="gridIndicadores_CommandButtonInitialize" OnLoad="gridIndicadores_Load" OnCustomButtonInitialize="gridIndicadores_CustomButtonInitialize" OnCustomCallback="gridIndicadores_CustomCallback">
                                    <Templates>
                                        <FooterRow>
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td valign="middle" align="left" width="20px">
                                                            <dxe:ASPxImage ID="imgSimResultado" runat="server" ImageUrl="~/imagens/IndicadorResultante.png"
                                                                ClientInstanceName="imgSimResultado" Height="16px">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                        <td style="padding-right: 5px; width: 180px;" valign="middle">
                                                            <asp:Label ID="Label2" runat="server" Text="<%# Resources.traducao.detalhesObjetivoEstrategico_indicador_resultante %>"></asp:Label>
                                                        </td>
                                                        <td valign="middle" align="left" width="20px">
                                                            <dxe:ASPxImage ID="imgPolaridadNeg" runat="server" ImageUrl="~/imagens/botoes/PolaridadeN.png"
                                                                ClientInstanceName="imgPolaridadNeg" Height="16px">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                        <td style="padding-right: 5px; width: 180px" valign="middle">
                                                            <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%# Resources.traducao.detalhesObjetivoEstrategico_polaridade_negativa %>"></dxcp:ASPxLabel>
                                                        </td>
                                                        <td valign="middle" align="left" width="20px">
                                                            <dxe:ASPxImage ID="imgPolaridadPos" runat="server" ImageUrl="~/imagens/botoes/PolaridadeP.png"
                                                                ClientInstanceName="imgPolaridadPos" ToolTip="<%# Resources.traducao.detalhesObjetivoEstrategico_associar_projeto_ao_objetivo_estrat_gico_selecionado %>"
                                                                Height="16px">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                        <td style="padding-right: 5px;" valign="middle">
                                                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:traducao, polaridade_positiva %>"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </FooterRow>
                                    </Templates>
                                    <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>
                                    <StylesPopup>
                                        <EditForm>
                                            <Header Font-Bold="True">
                                            </Header>
                                        </EditForm>
                                    </StylesPopup>
                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                    </SettingsPager>
                                    <SettingsEditing Mode="PopupEditForm">
                                    </SettingsEditing>
                                    <SettingsPopup>
                                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                            AllowResize="True" Width="800px" />
                                    </SettingsPopup>
                                    <SettingsText ConfirmDelete="Deseja realmente desvincular esse indicador do objetivo estratégico?"
                                        PopupEditFormCaption="Associar Indicador"></SettingsText>
                                    <ClientSideEvents BeginCallback="function(s, e) {
	hfGeral.Set('command',e.command);
}"
                                        CustomButtonClick="function(s, e) 
{
	if(e.buttonID == 'btnEditarPeso')
                {
                             s.GetRowValues(e.visibleIndex, 'CodigoIndicador;PesoObjetoLink;NomeIndicador', abrePopupEdicaoIndicador);                             
                }
                else if(e.buttonID == 'btnExcluirIndicadorAssociado')
                 {
                               s.GetRowValues(e.visibleIndex, 'CodigoIndicador', excluiIndicadorAssociado);
                 }
}" EndCallback="function(s, e) {
          if(s.cp_Sucesso !=  '')
          {
                       window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
                       s.Refresh();
                       pcAssociarProjeto.Hide();
          }
          else
          {
                   if(s.cp_Erro != '')
                   {
                               window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
                    }
           }
           s.cp_Sucesso = '';
           s.cp_Erro = '';
          pn_ddlIndicadorAssociado.PerformCallback();          
}"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="70px" VisibleIndex="0" ExportWidth="65">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                            <HeaderTemplate>
                                                <!-- BOTÃO DA GRID -->
                                                <table>
                                                    <tr>
                                                        <td align="center">
                                                            <dxm:ASPxMenu ID="menu" runat="server" ClientInstanceName="menu"
                                                                ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                                                    <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
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
                                                                <SubMenuItemStyle Cursor="pointer">
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
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarPeso" Text="Editar Peso">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxtv:GridViewCommandColumnCustomButton ID="btnExcluirIndicadorAssociado" Text="Excluir">
                                                    <Image AlternateText="Excluir" ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                    </Image>
                                                </dxtv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Indicador" FieldName="NomeIndicador" VisibleIndex="2">
                                            <EditFormSettings Visible="False" />
                                            <DataItemTemplate>
                                                <%# getLinkIndicador(int.Parse(Eval("CodigoIndicador").ToString()), Eval("NomeIndicador").ToString(), (bool)(Eval("Permissao"))) %>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoIndicador" Name="CodigoIndicador"
                                            Caption="Indicador" VisibleIndex="3" Visible="False">
                                            <PropertiesComboBox ValueType="System.String" DisplayFormatString="{0}">
                                                <ItemStyle Wrap="True" />
                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesComboBox>
                                            <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Indicador:"></EditFormSettings>
                                            <CellStyle HorizontalAlign="Left">
                                            </CellStyle>
                                        </dxwgv:GridViewDataComboBoxColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Polaridade" Name="Polaridade" Width="60px"
                                            Caption="Polaridade" Visible="False" VisibleIndex="1" ExportWidth="60">
                                            <EditFormSettings Visible="False"></EditFormSettings>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Name="PO" Width="30px" Caption=" " VisibleIndex="5">
                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                                            <EditFormSettings Visible="False"></EditFormSettings>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Name="SR" Width="30px" Caption=" " VisibleIndex="6"
                                            ExportWidth="30">
                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" />
                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" />
                                            <EditFormSettings Visible="False"></EditFormSettings>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="StatusDesempenho" Width="50px" Caption=" "
                                            VisibleIndex="8" ExportWidth="50" Name="StatusDesempenho">
                                            <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.gif' style='border-width:0px;' /&gt;">
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                                            <EditFormSettings Visible="False"></EditFormSettings>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="IndicadorResultante" Name="IndicadorResultante"
                                            Visible="False" VisibleIndex="7">
                                            <EditFormSettings Visible="False"></EditFormSettings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataSpinEditColumn AllowTextTruncationInAdaptiveMode="True" Caption="Peso" FieldName="PesoObjetoLink" Name="col_PesoObjetoLink" ShowInCustomizationForm="True" VisibleIndex="4" Width="60px" UnboundExpression="1">
                                            <PropertiesSpinEdit ClientInstanceName="PesoObjetoLink" DisplayFormatString="N2" MaxLength="100" NullText="1" MaxValue="100" NullDisplayText="1" ValueChangedDelay="1" NumberFormat="Custom">
                                                <SpinButtons ToolTip="Peso" Width="40px" ClientVisible="false">
                                                </SpinButtons>
                                            </PropertiesSpinEdit>
                                            <Settings FilterMode="DisplayText" GroupInterval="DisplayText" SortMode="DisplayText" />
                                            <EditFormSettings CaptionLocation="Top" ColumnSpan="1" Visible="True" VisibleIndex="22" />
                                            <BatchEditModifiedCellStyle BackColor="Red">
                                            </BatchEditModifiedCellStyle>
                                        </dxtv:GridViewDataSpinEditColumn>
                                    </Columns>
                                    <Settings ShowFooter="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="125"></Settings>
                                    <StylesEditors>
                                        <Style></Style>
                                    </StylesEditors>
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxwgv:ASPxGridView ID="gridProjetos" runat="server" AutoGenerateColumns="False"
                                    ClientInstanceName="gridProjetos" KeyFieldName="Codigo"
                                    OnAfterPerformCallback="gridProjetos_AfterPerformCallback" OnBeforeColumnSortingGrouping="gridIndicadores_BeforeColumnSortingGrouping"
                                    OnCommandButtonInitialize="gridProjetos_CommandButtonInitialize" OnCustomButtonInitialize="gridProjetos_CustomButtonInitialize"
                                    OnCustomCallback="gridProjetos_CustomCallback" OnRowDeleting="gridProjetos_RowDeleting"
                                    Width="100%" OnLoad="gridProjetos_Load">
                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                    </SettingsPager>
                                    <SettingsText ConfirmDelete="Deseja realmente excluir o registro?"></SettingsText>
                                    <ClientSideEvents CustomButtonClick="function(s, e) {
          e.processOnServer = false;
          if(e.buttonID == 'btnEditar')
          {
                   getCodigoProjeto(e.visibleIndex);
          }
          else if(e.buttonID == 'btnExcluirProjetoAssociado')
          {
                    s.GetRowValues(e.visibleIndex, 'Codigo', excluiProjetoAssociado);
          }
}"
                                        EndCallback="function(s, e) {
          if(s.cp_Sucesso !=  '')
          {
                       window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
                       pcAssociarProjeto.Hide();
          }
          else
          {
                   if(s.cp_Erro != '')
                   {
                               window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
                    }
           }
           s.cp_Sucesso = '';
           s.cp_Erro = '';
           gvAcoes.UnselectAllRowsOnPage(); 
          pn_ddlProjeto.PerformCallback();
}"
                                        BeginCallback="function(s, e) {	hfGeral.Set('command',e.command);
}"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="105px" Caption=" " VisibleIndex="0">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar Associa&#231;&#227;o">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxtv:GridViewCommandColumnCustomButton ID="btnExcluirProjetoAssociado" Text="Excluir">
                                                    <Image AlternateText="Excluir" ToolTip="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                                    </Image>
                                                </dxtv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                            <HeaderTemplate>
                                                <!-- BOTÃO DA GRID -->
                                                <table>
                                                    <tr>
                                                        <td align="center">
                                                            <dxm:ASPxMenu ID="menu2" runat="server" ClientInstanceName="menu2"
                                                                ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init2">
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
                                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
                                                                                <Image Url="~/imagens/menuExportacao/html.png">
                                                                                </Image>
                                                                            </dxm:MenuItem>
                                                                        </Items>
                                                                        <Image Url="~/imagens/botoes/btnDownload.png">
                                                                        </Image>
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                        <Items>
                                                                            <dxm:MenuItem Text="Salvar" ToolTip="<%$ Resources:traducao, detalhesObjetivoEstrategico_salvar_layout %>">
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
                                                                    <dxm:MenuItem Name="btnGantt" Text="" ToolTip="<%$ Resources:traducao, visualizar_gantt %>">
                                                                        <Image Url="~/imagens/ganttBotao.png">
                                                                        </Image>
                                                                    </dxm:MenuItem>
                                                                </Items>
                                                                <ItemStyle Cursor="pointer">
                                                                    <HoverStyle>
                                                                        <border borderstyle="None" />
                                                                    </HoverStyle>
                                                                    <Paddings Padding="0px" />
                                                                </ItemStyle>
                                                                <SubMenuItemStyle Cursor="pointer">
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
                                        <dxwgv:GridViewDataTextColumn FieldName="Descricao"
                                            Caption="Projeto / Plano de Ação" VisibleIndex="2">
                                            <PropertiesTextEdit DisplayFormatString="{0}">
                                            </PropertiesTextEdit>
                                            <DataItemTemplate>
                                                <%# getLinkProjeto()%>
                                            </DataItemTemplate>
                                            <CellStyle HorizontalAlign="Left">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Responsavel" Width="220px" Caption="Respons&#225;vel"
                                            VisibleIndex="3" ExportWidth="220">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Concluido" Width="80px" Caption="Conclu&#237;do"
                                            VisibleIndex="5" ExportWidth="80">
                                            <PropertiesTextEdit DisplayFormatString="{0:p0}" EncodeHtml="False">
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" />
                                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Status" Width="70px" Caption="Status" VisibleIndex="6"
                                            ExportWidth="70" Name="Status">
                                            <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.gif' style='border-width:0px;' /&gt;">
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn FieldName="CodigoIndicador" Visible="False"
                                            VisibleIndex="7">
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn FieldName="Prioridade" Visible="False"
                                            VisibleIndex="8">
                                        </dxtv:GridViewDataTextColumn>
                                        <dxtv:GridViewDataTextColumn Caption=" " FieldName="Tipo" VisibleIndex="1"
                                            Width="40px">
                                            <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.png' /&gt;">
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="False" FilterMode="DisplayText" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxtv:GridViewDataTextColumn>

                                        <dxtv:GridViewDataSpinEditColumn AllowTextTruncationInAdaptiveMode="True" Caption="Peso" FieldName="PesoObjetoLink" Name="col_PesoObjetoLink" VisibleIndex="4" Width="60px">
                                            <PropertiesSpinEdit ClientInstanceName="PesoObjetoLink" DisplayFormatString="N2" MaxLength="100" MaxValue="100" NumberFormat="Custom">
                                                <SpinButtons ToolTip="Peso" Width="40px" ClientVisible="false">
                                                </SpinButtons>
                                            </PropertiesSpinEdit>
                                        </dxtv:GridViewDataSpinEditColumn>

                                        <dxtv:GridViewDataTextColumn Caption="CodigoProjeto" FieldName="Codigo" Visible="False" VisibleIndex="9">
                                        </dxtv:GridViewDataTextColumn>

                                    </Columns>
                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="140" ShowFooter="True"></Settings>
                                    <Templates>
                                        <FooterRow>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <img src="../../imagens/verdeMenor.gif" width="16" />
                                                    </td>
                                                    <td>
                                                        <span style="">
                                                            <asp:Label ID="lblVerde" runat="server" EnableViewState="False"
                                                                Text="<%$ Resources:traducao, satisfat_rio %>"></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td>
                                                        <img src="../../imagens/amareloMenor.gif" width="16" />
                                                    </td>
                                                    <td>
                                                        <span style="">
                                                            <asp:Label ID="lblAmarelo" runat="server" EnableViewState="False"
                                                                Text="<%$ Resources:traducao, aten__o %>"></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td>
                                                        <img src="../../imagens/vermelhoMenor.gif" width="16" />
                                                    </td>
                                                    <td>
                                                        <span style="">
                                                            <asp:Label ID="lblVermelho" runat="server" EnableViewState="False"
                                                                Text="<%$ Resources:traducao, cr_tico %>"></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td>
                                                        <img src="../../imagens/brancoMenor.gif" width="16" />
                                                    </td>
                                                    <td>
                                                        <span style="">
                                                            <asp:Label ID="lblBranco" runat="server" EnableViewState="False"
                                                                Text="<%$ Resources:traducao, sem_acompanhamento %>"></asp:Label>
                                                        </span>
                                                    </td>
                                                    <td style="<%=displayLaranja %>;">
                                                        <img src="../../imagens/laranjaMenor.gif" width="16" />
                                                    </td>
                                                    <td style="<%=displayLaranja %>; padding-right: 10px; padding-left: 3px;">
                                                        <span>
                                                            <asp:Label ID="lblLaranja" runat="server" EnableViewState="False"
                                                                Text="<%$ Resources:traducao, finalizando %>"></asp:Label>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterRow>
                                    </Templates>
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                    </table>
                    <dxpc:ASPxPopupControl ID="pcAssociarProjeto" runat="server" ClientInstanceName="pcAssociarProjeto"
                        HeaderText="Associação" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" Width="700px" CloseAction="None" ShowCloseButton="False"
                        AllowDragging="True">
                        <ContentStyle>
                            <Paddings Padding="5px"></Paddings>
                        </ContentStyle>
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <table cellspacing="0" cellpadding="0" style="width: 100%">
                                    <tbody>

                                        <tr>
                                            <td style="width: 25%">
                                                <dxe:ASPxLabel runat="server" Text="Meta:" ClientInstanceName="lblMeta" Width="100%"
                                                    ID="lblMeta">
                                                </dxe:ASPxLabel>



                                                <dxe:ASPxComboBox runat="server" ValueType="System.Int32" Width="100%" Height="17px"
                                                    ClientInstanceName="ddlMeta"
                                                    ID="ddlMeta">
                                                    <ItemStyle Wrap="True"></ItemStyle>
                                                    <ListBoxStyle Wrap="True">
                                                    </ListBoxStyle>
                                                </dxe:ASPxComboBox>
                                            </td>

                                            <td style="padding: 5px 10px; width: 40%">
                                                <table id="tbInsercao" cellspacing="0" cellpadding="0" style="width: 100%">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel runat="server" Text="Projeto:"
                                                                    ID="ASPxLabel2">
                                                                </dxe:ASPxLabel>
                                                                <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" ForeColor="Green" Text="* ">
                                                                </dxtv:ASPxLabel>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxtv:ASPxCallbackPanel ID="pn_ddlProjeto" runat="server" ClientInstanceName="pn_ddlProjeto" OnCallback="pn_ddlProjeto_Callback" Width="100%">
                                                                    <PanelCollection>
                                                                        <dxtv:PanelContent runat="server">
                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.Int32" Width="100%" ClientInstanceName="ddlProjeto"
                                                                                ID="ddlProjeto" TextField="NomeProjeto" ValueField="CodigoProjeto" ClientVisible="False">
                                                                                <Columns>
                                                                                    <dxtv:ListBoxColumn Caption="Projeto" FieldName="NomeProjeto">
                                                                                    </dxtv:ListBoxColumn>
                                                                                </Columns>
                                                                            </dxe:ASPxComboBox>
                                                                        </dxtv:PanelContent>
                                                                    </PanelCollection>
                                                                </dxtv:ASPxCallbackPanel>
                                                                <dxtv:ASPxTextBox ID="txtNomeProjeto" runat="server" ClientInstanceName="txtNomeProjeto" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>

                                            <td style="width: 20%">
                                                <dxe:ASPxLabel runat="server" Text="Prioridade:"
                                                    ID="ASPxLabel4">
                                                </dxe:ASPxLabel>

                                                <dxe:ASPxComboBox runat="server" SelectedIndex="0" ValueType="System.String" Width="100%"
                                                    ClientInstanceName="ddlPrioridade" ID="ddlPrioridade">
                                                    <Items>
                                                        <dxe:ListEditItem Text="Alta" Value="A" Selected="True"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Text="Média" Value="M"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Text="Baixa" Value="B"></dxe:ListEditItem>
                                                    </Items>
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <%--Spin Peso--%>
                                            <td runat="server" id="tdPeso2" style="width: 15%">
                                                <dxe:ASPxLabel runat="server" Text="Peso:"
                                                    ID="ASPxLabelPeso">
                                                </dxe:ASPxLabel>
                                                <dxcp:ASPxSpinEdit ID="PesoObjetoLink2" ClientInstanceName="PesoObjetoLink2" Width="100%" MaxValue="100" NullText="1" NullTextDisplayMode="Unfocused" runat="server" Number="0">
                                                    <SpinButtons ClientVisible="False">
                                                    </SpinButtons>
                                                </dxcp:ASPxSpinEdit>
                                            </td>

                                        </tr>
                                    </tbody>
                                </table>

                                <table cellspacing="0" cellpadding="4" style="width: 100%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvAcoes" KeyFieldName="CodigoAcaoSugerida"
                                                    AutoGenerateColumns="False" Width="100%"
                                                    ID="gvAcoes" OnCustomCallback="gvAcoes_CustomCallback">
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" Width="8%" Caption=" " VisibleIndex="0">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <HeaderTemplate>
                                                                <input onclick="gvAcoes.SelectAllRowsOnPage(this.checked);" title="Marcar/Desmarcar Todos"
                                                                    type="checkbox" />
                                                            </HeaderTemplate>
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoAcao" Width="90%" Caption="A&#231;&#245;es Sugeridas"
                                                            VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False"></SettingsBehavior>
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="140"></Settings>
                                                    <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                                </dxwgv:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" Text="<%$ Resources:traducao, salvar %>" Width="100px"
                                                                    ID="btnSalvar" ClientInstanceName="btnSalvar">
                                                                    <ClientSideEvents Click="function(s, e) 
{
       if(tipoEdicao == 'I')
       {
                   hfGeral.Set('operacao', 'salvar');
	   gridProjetos.PerformCallback(ddlProjeto.GetValue());
        }
         else
         {
                     gridProjetos.PerformCallback('Atualizar');
         }
}"></ClientSideEvents>
                                                                    <Paddings Padding="0px"></Paddings>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td style="padding-left: 5px">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" Text="<%$ Resources:traducao, fechar %>" Width="100px"
                                                                    ID="btnFechar">
                                                                    <ClientSideEvents Click="function(s, e) {
	pcAssociarProjeto.Hide();
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
                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                    </dxpc:ASPxPopupControl>
                    <dxpc:ASPxPopupControl ID="pcAssociaIndicadorPeso" runat="server" ClientInstanceName="pcAssociaIndicadorPeso"
                        HeaderText="Associação" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" Width="700px" CloseAction="None" ShowCloseButton="False"
                        AllowDragging="True">
                        <ContentStyle>
                            <Paddings Padding="5px"></Paddings>
                        </ContentStyle>
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <table cellspacing="0" cellpadding="0" style="width: 100%">
                                    <tbody>

                                        <tr>
                                            <td style="width: 60%">

                                                <dxe:ASPxLabel runat="server" Text="Indicador:" ClientInstanceName="lblNomeIndicador" Width="100%"
                                                    ID="lblNomeIndicador">
                                                </dxe:ASPxLabel>
                                                <dxtv:ASPxCallbackPanel ID="pn_ddlIndicadorAssociado" ClientInstanceName="pn_ddlIndicadorAssociado" OnCallback="pn_ddlIndicadorAssociado_Callback" runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            <dxe:ASPxComboBox runat="server" ValueType="System.Int32" Width="100%" Height="17px"
                                                                ClientInstanceName="ddlIndicadorAssociado"
                                                                ID="ddlIndicadorAssociado">
                                                                <ItemStyle Wrap="True"></ItemStyle>
                                                                <ListBoxStyle Wrap="True">
                                                                </ListBoxStyle>
                                                            </dxe:ASPxComboBox>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxCallbackPanel>

                                            </td>
                                            <td runat="server" id="tdPeso3" style="width: 40%; padding-left: 5px">
                                                <dxe:ASPxLabel runat="server" Text="Peso:"
                                                    ID="ASPxLabelPeso0">
                                                </dxe:ASPxLabel>
                                                <dxcp:ASPxSpinEdit ID="spnPeso" ClientInstanceName="spnPeso" Width="100%" MaxValue="100" runat="server" Number="1">
                                                    <SpinButtons ClientVisible="False">
                                                    </SpinButtons>
                                                    <ClientSideEvents KeyPress="function(s, e) {
	if(e.htmlEvent.keyCode == 13)
	{
		e.processOnServer = false; 
                                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
	}
}" />
                                                </dxcp:ASPxSpinEdit>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                                <table cellspacing="0" cellpadding="4" style="width: 100%">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" Text="<%$ Resources:traducao, salvar %>" Width="100px"
                                                                    ID="btnSalvarAssociacaoIndicador" ClientInstanceName="btnSalvarAssociacaoIndicador">
                                                                    <ClientSideEvents Click="function(s, e) 
{
        var mensagem = validaCamposFormularioAssociaIndicador(); 
                                                                       
        if(mensagem == '')
        {
               callbackAssociaIndicador.PerformCallback(tipoOperacaoAssociaIndicador);
        }
        else
       {
                    window.top.mostraMensagem(mensagem, 'atencao', true, false, null, 4000);
       }
}"></ClientSideEvents>
                                                                    <Paddings Padding="0px"></Paddings>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td style="padding-left: 5px">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" Text="<%$ Resources:traducao, fechar %>" Width="100px"
                                                                    ID="btnFecharAssociacaoIndicador" ClientInstanceName="btnFecharAssociacaoIndicador">
                                                                    <ClientSideEvents Click="function(s, e) {
	pcAssociaIndicadorPeso.Hide();
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
                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                    </dxpc:ASPxPopupControl>
                    <dxcb:ASPxCallback ID="callbackAssociaIndicador" runat="server" ClientInstanceName="callbackAssociaIndicador" OnCallback="callbackAssociaIndicador_Callback">
                        <ClientSideEvents EndCallback="function(s, e) 
{
       if(s.cp_Sucesso != '')
       {

             window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null, 3000);  
             gridIndicadores.Refresh();
             pn_ddlIndicadorAssociado.PerformCallback();
             pcAssociaIndicadorPeso.Hide();
       }
        else
       {
             if(s.cp_Erro != '' )
             {
                   window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
             }
       }
}" />
                    </dxcb:ASPxCallback>
                    <asp:SqlDataSource ID="sdsProjetos" runat="server" SelectCommand="BEGIN
                    SELECT      p.CodigoProjeto
                            ,   p.NomeProjeto

                    FROM       Projeto AS p 
                    INNER JOIN  Status  AS st ON (st.CodigoStatus = p.CodigoStatusProjeto AND st.IndicaSelecaoPortfolio = 'S')

                    WHERE   p.DataExclusao          IS NULL
                        AND P.CodigoTipoProjeto     NOT IN(4,5) -- não e (demanda complexa, demanda simples).
                        AND p.CodigoEntidade        = @CodigoEntidade
                        AND p.CodigoProjeto         NOT IN(SELECT poe.CodigoProjeto 
                                                             FROM .ProjetoObjetivoEstrategico AS poe
                                                             WHERE poe.CodigoObjetivoEstrategico = @CodigoObjetivo
                                                           )
                        AND  p.NomeProjeto != ''
                    ORDER BY p.NomeProjeto       
                END">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="CodigoObjetivo" QueryStringField="COE" />
                            <asp:QueryStringParameter Name="CodigoEntidade" QueryStringField="UN" />
                            <asp:QueryStringParameter Name="CodigoUnidade" QueryStringField="UNM" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1"
                        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                        <Styles>
                            <Default>
                            </Default>
                            <Header>
                            </Header>
                            <Cell>
                            </Cell>
                            <GroupFooter Font-Bold="True">
                            </GroupFooter>
                            <Title Font-Bold="True"></Title>
                        </Styles>
                    </dxwgv:ASPxGridViewExporter>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
