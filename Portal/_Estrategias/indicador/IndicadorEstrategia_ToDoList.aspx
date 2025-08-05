<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndicadorEstrategia_ToDoList.aspx.cs" Inherits="_Estrategias_indicador_IndicadorEstrategia_ToDoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
   
    <script type="text/javascript" language="javascript">   
        
        function abrirGantt(urlParam) {
            var altura = Math.max(0, document.documentElement.clientHeight - 20);
            window.top.showModal(urlParam, traducao.IndicadorEstrategia_ToDoList_gantt_do_plano_de_a__o_do_indicador_selecionado, screen.width - 40, altura, "", null);
        }

        function abreGanttOEIndicador(codigoIndicador) {            
            var urlGanttIndicador = hfGeral.Get('urlGantt') + "&COE=" + codigoIndicador;
            abrirGantt(urlGanttIndicador);            
        }

        function abreGanttOE(codigoIndicador, codigoPA) {            
            var urlGanttIndicador = hfGeral.Get('urlGantt') + "&COE=" + codigoIndicador + "&CPA="+codigoPA;
            abrirGantt(urlGanttIndicador);
        }
       
    </script>
    <style type="text/css">
        .headerGrid {
            width: 100%;
        }

        .titulo-tela,
        .titulo-tela > span {
            display: inline-block;
            font-size: 18px !important;
            padding-bottom: 10px;
            padding-left: 7px;
            padding-top: 5px;
        }
    </style>
</head>
<body class="body">
    <form id="form1" runat="server">
        <div style="padding: 5px;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr style="height: 26px">
                    <td valign="middle" style="padding-right: 10px;">
                        <table class="formulario" cellpadding="0" cellspacing="0" style="width: 100%;">
                           <tr>
                                <td>
                                    <dxe:ASPxLabel ID="lblTitulo" CssClass="titulo-tela" runat="server" ClientInstanceName="lblTituloSelecao"
                                        Font-Bold="True" Text="Plano de Ação">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="formulario-linha" style="display: none;">
                                <td id="tdObjetivoMapa" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td id="tdObjetivoMapa0" runat="server">
                                                <table border="0" cellpadding="0" cellspacing="0" class="formulario-colunas" width="100%">
                                                    <tr>
                                                        <td width="25%">
                                                            <table cellspacing="0" cellpadding="0" class="headerGrid formulario-colunas" width="100%">
                                                                <tr class="formulario-linha">
                                                                    <td class="formulario-label">
                                                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                            Text="Mapa Estratégico:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr class="formulario-linha">
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientEnabled="False"
                                                                            Width="100%">
                                                                            <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="75%">
                                                            <table cellspacing="0" class="headerGrid" width="100%">
                                                                <tr class="formulario-linha">
                                                                    <td class="formulario-label">
                                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                                            Text="Objetivo Estratégico:">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr class="formulario-linha">
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server" ClientEnabled="False"
                                                                            Width="100%">
                                                                            <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td class="formulario-label">
                                    <dxe:ASPxLabel ID="lblIndicador" runat="server"
                                        Text="Indicador:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td>
                                    <dxe:ASPxTextBox ID="txtIndicador" runat="server" ClientInstanceName="txtIndicador"
                                        Width="100%" ClientEnabled="False">
                                        <DisabledStyle BackColor="#EAEAEA" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td>
                                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                                        OnCallback="pnCallback_Callback" Width="100%" HideContentOnCallback="False">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <!-- ASPxGRIDVIEW: gvDados -->
                                                <!-- PANEL CONTROL : pcDados -->
                                                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                                                    ClientInstanceName="gvDados"
                                                    KeyFieldName="CodigoToDoList"
                                                    OnAfterPerformCallback="gvDados_AfterPerformCallback"
                                                    OnCustomButtonInitialize="gvDados_CustomButtonInitialize" Width="100%">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
var larguraPopup = Math.max(0, document.documentElement.clientWidth - 50);  
pcDados.SetWidth(larguraPopup);
                 if(e.buttonID == 'btnEditarCustom')
                 {
                        TipoOperacao = 'Editar';
	       hfGeral.Set('TipoOperacao', 'Editar');                
                       s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
                       s.GetSelectedFieldValues('CodigoToDoList;CodigoUsuarioResponsavelToDoList;NomeToDoList;Porcentagem;Status;', MontaCamposFormulario);                 
                }
                 else if(e.buttonID == 'btnExcluirCustom')
                 {
		            onClickBarraNavegacao('Excluir', gvDados, pcDados);
                 }
                 else if(e.buttonID == 'btnFormularioCustom')
                 {	
                                    TipoOperacao = 'Consultar';
	                   hfGeral.Set('TipoOperacao', 'Consultar');
	                   s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
                                  s.GetSelectedFieldValues('CodigoToDoList;CodigoUsuarioResponsavelToDoList;NomeToDoList;Porcentagem;Status;', MontaCamposFormulario);
                 }
	             else if(e.buttonID == 'btnGanttCustom')
                 {	
		            hfGeral.Set('TipoOperacao', 'Consultar');
                    var codigoPA = s.GetRowKey(e.visibleIndex);
                    var codigoIndic = hfGeral.Get('codigoObjetoAssociado');
		            abreGanttOE(codigoIndic, codigoPA);
	             }
            }" />

                                                    <SettingsCommandButton>
                                                        <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                        <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                                    </SettingsCommandButton>
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="Ação" Name="acao"
                                                            ShowInCustomizationForm="True" VisibleIndex="0" Width="130px">
                                                            <CustomButtons>
                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnIncluirCustom" Text="Incluir"
                                                                    Visibility="Invisible">
                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                    </Image>
                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnGanttCustom" Text="Gantt">
                                                                    <Image Url="~/imagens/ganttBotao.png">
                                                                    </Image>
                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                                    </Image>
                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                                    </Image>
                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnFormularioCustom"
                                                                    Text="Detalhe">
                                                                    <Image Url="~/imagens/botoes/pFormulario.png">
                                                                    </Image>
                                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <HeaderTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <dxm:ASPxMenu ID="menu2" runat="server" BackColor="Transparent"
                                                                                ClientInstanceName="menu2" ItemSpacing="5px" OnInit="menu_Init2"
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
                                                                                    <dxm:MenuItem Name="btnGantt" Text="" ToolTip="Visualizar gantt">
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
                                                        <dxwgv:GridViewDataTextColumn Caption="CodigoToDoList"
                                                            FieldName="CodigoToDoList" Name="CodigoToDoList" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="NomeToDoList"
                                                            Name="NomeToDoList" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="NomeUsuario"
                                                            Name="NomeUsuario" ShowInCustomizationForm="True" VisibleIndex="3"
                                                            Width="200px">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Responsavel"
                                                            FieldName="CodigoUsuarioResponsavelToDoList"
                                                            Name="CodigoUsuarioResponsavelToDoList" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="%" FieldName="Porcentagem"
                                                            Name="Porcentagem" ShowInCustomizationForm="True" VisibleIndex="5" Width="70px">
                                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" Name="Status"
                                                            ShowInCustomizationForm="True" VisibleIndex="6" Width="70px">
                                                            <PropertiesTextEdit DisplayFormatString="&lt;img style='border:0px' alt='' src='../../imagens/{0}.gif'/&gt;">
                                                            </PropertiesTextEdit>
                                                            <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False"
                                                                AllowSort="False" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized" />
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Settings ShowFooter="True" ShowGroupPanel="True"
                                                        VerticalScrollBarMode="Visible" />
                                                    <Templates>
                                                        <FooterRow>
                                                            <table class="grid-legendas">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="grid-legendas-icone">
                                                                            <dxe:ASPxImage ID="ASPxImage2" runat="server"
                                                                                ImageUrl="~/imagens/verdeMenor.gif">
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td class="grid-legendas-label">
                                                                            <dxe:ASPxLabel ID="lblDescricaoConcluido" runat="server"
                                                                                ClientInstanceName="lblDescricaoConcluido"
                                                                                Text="<%# Resources.traducao.IndicadorEstrategia_ToDoList_satisfat_rio %>">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td class="grid-legendas-icone">
                                                                            <dxe:ASPxImage ID="ASPxImage3" runat="server"
                                                                                ImageUrl="~/imagens/amareloMenor.gif">
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td class="grid-legendas-label">
                                                                            <dxe:ASPxLabel ID="lblDescricaoPendiente" runat="server"
                                                                                ClientInstanceName="lblDescricaoPendiente"
                                                                                Text="<%# Resources.traducao.IndicadorEstrategia_ToDoList_aten__o %>">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td class="grid-legendas-icone">
                                                                            <dxe:ASPxImage ID="ASPxImage4" runat="server"
                                                                                ImageUrl="~/imagens/vermelhoMenor.gif">
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td class="grid-legendas-label">
                                                                            <dxe:ASPxLabel ID="lblDescricaoAtrazadas" runat="server"
                                                                                ClientInstanceName="lblDescricaoAtrazadas"
                                                                                Text="<%# Resources.traducao.IndicadorEstrategia_ToDoList_cr_tico %>">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </FooterRow>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                                <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                                    GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
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
                                                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="774px" Height="145px" ID="pcDados" PopupVerticalOffset="-110">
                                                    <ClientSideEvents Shown="function(s, e) {
	            //desabilitaHabilitaComponentes();
            }"></ClientSideEvents>

                                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                                    <ContentCollection>
                                                        <dxpc:PopupControlContentControl runat="server">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="Plano de A&#231;&#227;o:" ClientInstanceName="lblCodigoUsuarioResponsavel" ID="lblCodigoUsuarioResponsavel"></dxe:ASPxLabel>

                                                                                        </td>
                                                                                        <td style="width: 10px"></td>
                                                                                        <td style="width: 50px" valign="middle" align="center">
                                                                                            <dxe:ASPxLabel runat="server" Text="Status:" ClientInstanceName="lblStatus" ID="lblStatus"></dxe:ASPxLabel>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <dxe:ASPxTextBox runat="server" Width="100%"
                                                                                                ClientInstanceName="txtDescricaoPlanoAcao" ClientEnabled="False"
                                                                                                ID="txtDescricaoPlanoAcao" MaxLength="250">
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    <border bordercolor="Silver"></border>
                                                                                                </DisabledStyle>
                                                                                            </dxe:ASPxTextBox>

                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td valign="middle" align="center">
                                                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Branco.gif" ClientInstanceName="imgStatus" ID="imgStatus"></dxe:ASPxImage>

                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 5px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="Respons&#225;vel:" ClientInstanceName="lblStatusTarefa" ID="lblStatusTarefa"></dxe:ASPxLabel>

                                                                                        </td>
                                                                                        <td style="width: 10px"></td>
                                                                                        <td style="width: 80px">
                                                                                            <dxe:ASPxLabel runat="server" Text="% Concluido:" ClientInstanceName="lblPorcentajeConcluido" ID="lblPorcentajeConcluido"></dxe:ASPxLabel>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.Int32" TextFormatString="{0}" Width="100%" ClientInstanceName="ddlResponsavel" ID="ddlResponsavel">
                                                                                                <Columns>
                                                                                                    <dxe:ListBoxColumn FieldName="NomeUsuario" Width="300px" Caption="Nome"></dxe:ListBoxColumn>
                                                                                                    <dxe:ListBoxColumn FieldName="EMail" Width="200px" Caption="Email"></dxe:ListBoxColumn>
                                                                                                </Columns>

                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                            </dxe:ASPxComboBox>

                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td valign="top">
                                                                                            <dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right" ClientInstanceName="txtPorcentajeConcluido" ClientEnabled="False" ID="txtPorcentajeConcluido">
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                            </dxe:ASPxTextBox>

                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="A&#231;&#245;es:" ClientInstanceName="lblAnotacoes" ID="lblAnotacoes"></dxe:ASPxLabel>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td id="Td1">
                                                                            <dxp:ASPxPanel runat="server" Width="100%" ID="pAcoes">
                                                                                <PanelCollection>
                                                                                    <dxp:PanelContent runat="server"></dxp:PanelContent>
                                                                                </PanelCollection>
                                                                            </dxp:ASPxPanel>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar"
                                                                                                ValidationGroup="MKE" Width="90px"
                                                                                                ID="btnSalvar">
                                                                                                <ClientSideEvents Click="function(s, e) {
	            Click_Salvar(s,e);
            }"></ClientSideEvents>
                                                                                            </dxe:ASPxButton>

                                                                                        </td>
                                                                                        <td style="width: 10px"></td>
                                                                                        <td>
                                                                                            <dxe:ASPxButton runat="server"
                                                                                                ClientInstanceName="btnFechar" Text="Fechar" Width="90px"
                                                                                                ID="btnFechar">
                                                                                                <ClientSideEvents Click="function(s, e) {	
	            e.processOnServer = false;
                if (window.onClick_btnCancelar)
                   onClick_btnCancelar();

	            pnCallback.PerformCallback('FecharPopup'); // somente para remover Session[&quot;_CodigoToDoList_&quot;]
            }"></ClientSideEvents>
                                                                                            </dxe:ASPxButton>

                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>

                                                        </dxpc:PopupControlContentControl>
                                                    </ContentCollection>
                                                </dxpc:ASPxPopupControl>
                                            </dxp:PanelContent>
                                        </PanelCollection>

                                        <ClientSideEvents EndCallback="function(s, e) {
                if( (s.cp_OperacaoOk) &amp;&amp; (&quot;ShowToDoList&quot; == s.cp_OperacaoOk) )
		            pcDados.Show();
	            else
		            onEnd_pnCallback();
            }"></ClientSideEvents>
                                    </dxcp:ASPxCallbackPanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
