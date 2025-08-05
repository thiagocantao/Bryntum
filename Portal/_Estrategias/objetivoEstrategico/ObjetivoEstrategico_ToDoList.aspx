<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjetivoEstrategico_ToDoList.aspx.cs" Inherits="_Estrategias_objetivoEstrategico_ObjetivoEstrategico_ToDoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title></title>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <%--                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="campo-label" Text="Mapa Estratégico:"></asp:Label></td>
                                        <td></td>
                                        <td style="width: 209px">
                                            <asp:Label ID="lblPerspectiva" CssClass="campo-label" runat="server"
                                                Text="Perspectiva:"></asp:Label></td>
                                        <td></td>
                                        <td style="width: 220px">
                                            <asp:Label ID="Label6" runat="server" CssClass="campo-label" Text="Tema:"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientInstanceName="txtMapa"
                                                ReadOnly="True" Width="100%">
                                                <ReadOnlyStyle BackColor="#E0E0E0">
                                                </ReadOnlyStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td></td>
                                        <td style="width: 220px">
                                            <dxe:ASPxTextBox ID="txtPerspectiva" runat="server" ClientInstanceName="txtPerspectiva"
                                                ReadOnly="True" Width="100%">
                                                <ReadOnlyStyle BackColor="#E0E0E0">
                                                </ReadOnlyStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td></td>
                                        <td style="width: 220px">
                                            <dxe:ASPxTextBox ID="txtTema" runat="server" ClientInstanceName="txtTema"
                                                ReadOnly="True" Width="100%">
                                                <ReadOnlyStyle BackColor="#E0E0E0">
                                                </ReadOnlyStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>--%>

                        <tr>
                            <td>
                                <table style="width: 100%" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblObjetivoEstrategico" CssClass="campo-label" runat="server" Text="<%$ Resources:traducao, objetivo_estrat_gico_ %>"></asp:Label></td>
                                            <td
                                                style="width: 10px"></td>
                                            <td style="width: 280px">
                                                <asp:Label ID="Label3" runat="server" CssClass="campo-label" Text="<%$ Resources:traducao, respons_vel_ %>"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtObjetivoEstrategico">
                                                    <ReadOnlyStyle BackColor="#E0E0E0">
                                                    </ReadOnlyStyle>
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td
                                                style="width: 280px">
                                                <dxe:ASPxTextBox ID="txtResponsavel" runat="server" Width="100%" ReadOnly="True" ClientInstanceName="txtResponsavel">
                                                    <ReadOnlyStyle BackColor="#E0E0E0">
                                                    </ReadOnlyStyle>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                                    OnCallback="pnCallback_Callback" Width="100%">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <!-- ASPxGRIDVIEW: gvDados -->
                                            <!-- PANEL CONTROL : pcDados -->
                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoToDoList" AutoGenerateColumns="False" Width="100%" ID="gvDados" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                                <ClientSideEvents
                                                    CustomButtonClick="function(s, e) {
var larguraPopup = Math.max(0, document.documentElement.clientWidth - 50);  
pcDados.SetWidth(larguraPopup);
 if(e.buttonID == 'btnEditarCustom')
     {
                TipoOperacao = 'Editar';
	hfGeral.Set('TipoOperacao', 'Editar');                
       s.SelectRowOnPage(s.GetFocusedRowIndex(), true);
       s.GetSelectedFieldValues('CodigoToDoList;CodigoUsuarioResponsavelToDoList;NomeToDoList;Porcentagem;Status;PesoObjetoLink', MontaCamposFormulario);
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
                s.GetSelectedFieldValues('CodigoToDoList;CodigoUsuarioResponsavelToDoList;NomeToDoList;Porcentagem;Status;PesoObjetoLink', MontaCamposFormulario);
     }
     else if(e.buttonID == 'btnGanttCustom')
     {	
                 hfGeral.Set('TipoOperacao', 'Consultar');
                 var codigoPA = s.GetRowKey(e.visibleIndex);
                 var codigoOE = hfGeral.Get('codigoObjetoAssociado');
                  var urlGanttObjetivo = hfGeral.Get('urlGantt') + &quot;&amp;COE=&quot; + codigoOE  + &quot;&amp;CPA=&quot; + codigoPA;
              var altura = Math.max(0, document.documentElement.clientHeight - 20);
   window.top.showModal( urlGanttObjetivo, traducao.ObjetivoEstrategico_ToDoList_gantt_do_plano_de_ac_o_do_objetivo_estrat_gico_selecionado, screen.width - 40, altura, &quot;&quot;, null);
     }
}"></ClientSideEvents>

                                                <SettingsCommandButton>
                                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                                </SettingsCommandButton>
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Name="acao" Width="130px" Caption="A&#231;&#227;o" VisibleIndex="0">
                                                        <CustomButtons>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnIncluirCustom" Visibility="Invisible" Text="Incluir">
                                                                <Image Url="~/imagens/botoes/incluirReg02.png"></Image>
                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnGanttCustom" Text="Visualizar Gantt">
                                                                <Image Url="~/imagens/ganttBotao.png"></Image>
                                                            </dxwgv:GridViewCommandColumnCustomButton>
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

                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                        <HeaderTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        <dxm:ASPxMenu ID="menu2" runat="server" BackColor="Transparent"
                                                                            ClientInstanceName="menu2"
                                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                                            OnInit="menu_Init2">
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
                                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
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
                                                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
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
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoToDoList" Name="CodigoToDoList" Caption="CodigoToDoList" Visible="False" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="NomeToDoList" Name="NomeToDoList" Caption="Descri&#231;&#227;o" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="NomeUsuario" Name="NomeUsuario" Width="210px" Caption="Respons&#225;vel" VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioResponsavelToDoList" Name="CodigoUsuarioResponsavelToDoList" Caption="Responsavel" Visible="False" VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="Porcentagem" Name="Porcentagem" Width="60px" Caption="%" VisibleIndex="5">
                                                        <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="Status" Name="Status" Width="70px" Caption="Status" VisibleIndex="7">
                                                        <PropertiesTextEdit DisplayFormatString="&lt;img style='border:0px' alt='' src='../../imagens/{0}.gif'/&gt;"></PropertiesTextEdit>

                                                        <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False"
                                                            AllowSort="False" />

                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxtv:GridViewDataTextColumn FieldName="PesoObjetoLink" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                        <PropertiesTextEdit DisplayFormatString="g">
                                                        </PropertiesTextEdit>
                                                    </dxtv:GridViewDataTextColumn>
                                                </Columns>

                                                <SettingsBehavior AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>

                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                                <Settings ShowFooter="True" VerticalScrollBarMode="Visible" ShowGroupPanel="True"></Settings>

                                                <Templates>
                                                    <FooterRow>
                                                        <table class="grid-legendas" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="grid-legendas-icone">
                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/verdeMenor.gif" ID="ASPxImage2"></dxe:ASPxImage>
                                                                    </td>
                                                                    <td class="grid-legendas-label grid-legendas-label-icone">
                                                                        <dxe:ASPxLabel runat="server" Text="<%# Resources.traducao.ObjetivoEstrategico_ToDoList_satisfat_rio %>" ClientInstanceName="lblDescricaoConcluido" ID="lblDescricaoConcluido"></dxe:ASPxLabel>
                                                                    </td>
                                                                    <td class="grid-legendas-icone">
                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/amareloMenor.gif" ID="ASPxImage3"></dxe:ASPxImage>
                                                                    </td>
                                                                    <td class="grid-legendas-label grid-legendas-label-icone">
                                                                        <dxe:ASPxLabel runat="server" Text="<%# Resources.traducao.ObjetivoEstrategico_ToDoList_aten__o %>" ClientInstanceName="lblDescricaoPendiente" ID="lblDescricaoPendiente"></dxe:ASPxLabel>
                                                                    </td>
                                                                    <td class="grid-legendas-icone">
                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/vermelhoMenor.gif" ID="ASPxImage4"></dxe:ASPxImage>
                                                                    </td>
                                                                    <td class="grid-legendas-label grid-legendas-label-icone">
                                                                        <dxe:ASPxLabel runat="server" Text="<%# Resources.traducao.ObjetivoEstrategico_ToDoList_cr_tico %>" ClientInstanceName="lblDescricaoAtrazadas" ID="lblDescricaoAtrazadas"></dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>

                                                    </FooterRow>
                                                </Templates>
                                            </dxwgv:ASPxGridView>

                                        </dxp:PanelContent>
                                    </PanelCollection>
                                    <ClientSideEvents EndCallback="function(s, e) {
         if(s.cpErro != '')
        {
         window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
        }
        else if (s.cpSucesso != '')
        {
                      window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3500);
                       pcDados.Hide();
        }
       //pnCallback.PerformCallback('FecharPopup'); // somente para remover Session[&quot;_CodigoToDoList_&quot;]
}"></ClientSideEvents>
                                </dxcp:ASPxCallbackPanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
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
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True"
            ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="False"   ID="pcDados" PopupVerticalOffset="-110">
            <ClientSideEvents Shown="function(s, e) {
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>

            <ContentStyle>
                <Paddings Padding="5px"></Paddings>
            </ContentStyle>

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
                                                    <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" ForeColor="Green" Text="* ">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Respons&#225;vel:" ClientInstanceName="lblStatusTarefa" ID="lblStatusTarefa"></dxe:ASPxLabel>
                                                    <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" ForeColor="Green" Text="* ">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td runat="server" id="tdPeso3Label" style="width: 90px">
                                                    <dxe:ASPxLabel runat="server" Text="Peso:"
                                                        ID="ASPxLabelPeso">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 80px">
                                                    <dxe:ASPxLabel runat="server" Text="% Conclu&#237;do:" ClientInstanceName="lblPorcentajeConcluido" ID="lblPorcentajeConcluido"></dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 50px" valign="middle" align="center">
                                                    <dxe:ASPxLabel runat="server" Text="Status:" ClientInstanceName="lblStatus" ID="lblStatus"></dxe:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 10px" valign="top">
                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtDescricaoPlanoAcao" ClientEnabled="False" ID="txtDescricaoPlanoAcao" MaxLength="250">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            <border bordercolor="Silver"></border>
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>

                                                </td>
                                                <td style="padding-right: 10px" valign="top">
                                                    <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" ValueType="System.Int32" TextFormatString="{0}" Width="100%" ClientInstanceName="ddlResponsavel" ID="ddlResponsavel">
                                                        <Columns>
                                                            <dxe:ListBoxColumn FieldName="NomeUsuario" Width="300px" Caption="Nome"></dxe:ListBoxColumn>
                                                            <dxe:ListBoxColumn FieldName="EMail" Width="200px" Caption="Email"></dxe:ListBoxColumn>
                                                        </Columns>

                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td runat="server" style="width: 90px;padding-right:5px" id="tdPeso3" valign="top">
                                                    <dxcp:ASPxSpinEdit ID="PesoObjetoLink3" ClientInstanceName="PesoObjetoLink3" Width="100%" MaxValue="100" NullText="1" runat="server" Number="1">
                                                        
                                                    </dxcp:ASPxSpinEdit>
                                                </td>
                                                <td valign="top">
                                                    <dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right" ClientInstanceName="txtPorcentajeConcluido" ClientEnabled="False" ID="txtPorcentajeConcluido">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                    </dxe:ASPxTextBox>

                                                </td>

                                                <td valign="middle" align="center">
                                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Branco.gif" ClientInstanceName="imgStatus" ID="imgStatus"></dxe:ASPxImage>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px">
                                    <dxe:ASPxLabel runat="server" Text="A&#231;&#245;es:" ClientInstanceName="lblAnotacoes" ID="lblAnotacoes"></dxe:ASPxLabel>

                                </td>
                            </tr>
                            <tr>
                                <td id="Td1">
                                    <dxp:ASPxPanel runat="server" Width="100%" ID="pAcoes" ScrollBars="Vertical">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server"></dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>

                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px" align="right">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar"
                                                        ValidationGroup="MKE" Width="90px"
                                                        ID="btnSalvar">
                                                        <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
                 if(verificarDadosPreenchidos())
	{
		if (window.onClick_btnSalvar)
	                 onClick_btnSalvar();
	}
	else 
       {
            return false;
       }	
}"></ClientSideEvents>
                                                    </dxe:ASPxButton>

                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxe:ASPxButton runat="server"
                                                        ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
                                                        <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
pcDados.Hide();
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
    </form>
</body>
</html>
