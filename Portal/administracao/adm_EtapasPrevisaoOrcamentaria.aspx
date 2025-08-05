<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="adm_EtapasPrevisaoOrcamentaria.aspx.cs" Inherits="administracao_adm_EtapasPrevisaoOrcamentaria" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .auto-style2 {
            height: 14px;
        }
    </style>
    <script type="text/javascript" language="javascript" src="../scripts/adm_EtapasPrevisaoOrcamentaria.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/_Strings.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <p>
        <dxcp:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" Width="750px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Detalhes">
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">
                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="headerGrid">
                                    <tr>
                                        <td>
                                            <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" Text="Previsão: *">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td style="width: 80px; padding-left: 5px;">
                                            <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="Ano: *">
                                            </dxtv:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxTextBox ID="txtPrevisao" runat="server" ClientInstanceName="txtPrevisao" MaxLength="100" Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxTextBox>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <dxtv:ASPxSpinEdit ID="spnAno" runat="server" ClientInstanceName="spnAno" Number="0" Width="100%" MaxValue="2099" NumberType="Integer">
                                                <SpinButtons ClientVisible="False" Enabled="False">
                                                </SpinButtons>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxSpinEdit>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2" style="padding-top: 5px">
                                <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" Text="Observações:">
                                </dxtv:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 5px">
                                <dxtv:ASPxMemo ID="memoObservacoes" runat="server" ClientInstanceName="memoObservacoes" Height="71px" Width="100%">
                                </dxtv:ASPxMemo>
                                &nbsp;<dxtv:ASPxLabel ID="lblContadorMemo" runat="server" ClientInstanceName="lblContadorMemo" Font-Bold="True" ForeColor="#999999">
                                </dxtv:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="headerGrid">
                                    <tr>
                                        <td style="width: 20%">
                                            <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" Text="Mês início bloqueio:">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td style="width: 20%; padding-left: 5px;">
                                            <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" Text="Mês término bloqueio:">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td style="width: 20%; padding-left: 5px;">
                                            <dxtv:ASPxLabel ID="ASPxLabel7" runat="server" Text="Início Elaboração: *">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td style="width: 20%; padding-left: 5px;">
                                            <dxtv:ASPxLabel ID="ASPxLabel8" runat="server" Text="Término Elaboração: *">
                                            </dxtv:ASPxLabel>
                                        </td>
                                        <td style="width: 20%; padding-left: 5px;">
                                            <dxtv:ASPxLabel ID="ASPxLabel9" runat="server" Text="Status: *">
                                            </dxtv:ASPxLabel>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtv:ASPxComboBox ID="ddlMesInicioBloqueio" runat="server" ClientInstanceName="ddlMesInicioBloqueio" Width="100%">
                                                <Items>
                                                    <dxtv:ListEditItem Text="Janeiro" Value="1" />
                                                    <dxtv:ListEditItem Text="Fevereiro" Value="2" />
                                                    <dxtv:ListEditItem Text="Março" Value="3" />
                                                    <dxtv:ListEditItem Text="Abril" Value="4" />
                                                    <dxtv:ListEditItem Text="Maio" Value="5" />
                                                    <dxtv:ListEditItem Text="Junho" Value="6" />
                                                    <dxtv:ListEditItem Text="Julho" Value="7" />
                                                    <dxtv:ListEditItem Text="Agosto" Value="8" />
                                                    <dxtv:ListEditItem Text="Setembro" Value="9" />
                                                    <dxtv:ListEditItem Text="Outubro" Value="10" />
                                                    <dxtv:ListEditItem Text="Novembro" Value="11" />
                                                    <dxtv:ListEditItem Text="Dezembro" Value="12" />
                                                </Items>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxComboBox>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <dxtv:ASPxComboBox ID="ddlMesTerminoBloqueio" runat="server" ClientInstanceName="ddlMesTerminoBloqueio" TextField="DescricaoGrupo" Width="100%">
                                                <Items>
                                                    <dxtv:ListEditItem Text="Janeiro" Value="1" />
                                                    <dxtv:ListEditItem Text="Fevereiro" Value="2" />
                                                    <dxtv:ListEditItem Text="Março" Value="3" />
                                                    <dxtv:ListEditItem Text="Abril" Value="4" />
                                                    <dxtv:ListEditItem Text="Maio" Value="5" />
                                                    <dxtv:ListEditItem Text="Junho" Value="6" />
                                                    <dxtv:ListEditItem Text="Julho" Value="7" />
                                                    <dxtv:ListEditItem Text="Agosto" Value="8" />
                                                    <dxtv:ListEditItem Text="Setembro" Value="9" />
                                                    <dxtv:ListEditItem Text="Outubro" Value="10" />
                                                    <dxtv:ListEditItem Text="Novembro" Value="11" />
                                                    <dxtv:ListEditItem Text="Dezembro" Value="12" />
                                                </Items>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxComboBox>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <dxtv:ASPxDateEdit ID="ddlDataInicioElaboracao" runat="server" ClientInstanceName="ddlDataInicioElaboracao" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxDateEdit>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <dxtv:ASPxDateEdit ID="ddlDataTerminoElaboracao" runat="server" ClientInstanceName="ddlDataTerminoElaboracao" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="100%">
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </DisabledStyle>
                                            </dxtv:ASPxDateEdit>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <dxtv:ASPxCallbackPanel ID="pnCallbackStatus" ClientInstanceName="pnCallbackStatus" runat="server" Width="100%" OnCallback="pnCallbackStatus_Callback">
                                                <PanelCollection>
                                                    <dxtv:PanelContent runat="server">
                                                        <dxtv:ASPxComboBox ID="ddlStatus" runat="server" ClientInstanceName="ddlStatus" TextField="DescricaoGrupo" ValueField="CodigoGrupoRecurso" ValueType="System.Int32" Width="100%">
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxtv:ASPxComboBox>
                                                    </dxtv:PanelContent>
                                                </PanelCollection>
                                            </dxtv:ASPxCallbackPanel>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table id="Table1" class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar1"
                                                    Text="Salvar" Width="90px"
                                                    ID="btnSalvar1">
                                                    <ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
	                    if(verificarDadosPreenchidos())
                        {
                             callbackSalvar.PerformCallback(TipoOperacao);                           
                        }
	                    else
		                    return false;
                    }"></ClientSideEvents>
                                                    <Paddings Padding="0px"></Paddings>
                                                </dxe:ASPxButton>
                                            </td>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                    Text="Fechar" Width="90px"
                                                    ID="btnFechar">
                                                    <ClientSideEvents Click="function(s, e) {
pcDados.Hide();
                    }"></ClientSideEvents>
                                                    <Paddings Padding="0px"></Paddings>
                                                </dxe:ASPxButton>
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



        <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="CodigoPrevisao" ClientInstanceName="gvDados" Width="100%" ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
            <Paddings Padding="10px" />

            <ClientSideEvents CustomButtonClick="function(s, e) {
         s.SetFocusedRowIndex(e.visibleIndex);        
 if(e.buttonID == 'btnEditar')
         {
	btnSalvar1.SetVisible(true);  
                TipoOperacao = 'Editar';
	   hfGeral.Set('TipoOperacao', TipoOperacao);
s.GetRowValues(e.visibleIndex,'CodigoPrevisao;DescricaoPrevisao;AnoOrcamento;Observacao;DescricaoStatusPrevisaoFluxoCaixa;MesInicioBloqueio;MesTerminoBloqueio;InicioPeriodoElaboracao;TerminoPeriodoElaboracao;CodigoStatusPrevisaoFluxoCaixa;PodeExcluir;PodeAlterarStatus;PodeIgualarPrevistoRealizado;CodigoNovoStatusPermitido;CodigoStatusAnteriorPermitido',mostraPopup);
           }
           else if(e.buttonID == 'btnExcluir')
          {
	   TipoOperacao = 'Excluir';
                    hfGeral.Set(&quot;TipoOperacao&quot;, TipoOperacao);
                    window.top.mostraMensagem('Deseja realmente excluir o registro?', 'confirmacao', true, true, ExcluirRegistroSelecionado);
           }
           else if(e.buttonID == 'btnDetalhesCustom')
           {	
	      TipoOperacao = 'Consultar';
	      hfGeral.Set('TipoOperacao', TipoOperacao);
                       btnSalvar1.SetVisible(false); s.GetRowValues(e.visibleIndex,'CodigoPrevisao;DescricaoPrevisao;AnoOrcamento;Observacao;DescricaoStatusPrevisaoFluxoCaixa;MesInicioBloqueio;MesTerminoBloqueio;InicioPeriodoElaboracao;TerminoPeriodoElaboracao;CodigoStatusPrevisaoFluxoCaixa;PodeExcluir;PodeAlterarStatus;PodeIgualarPrevistoRealizado;CodigoNovoStatusPermitido;CodigoStatusAnteriorPermitido',mostraPopup);
           }	 
           else if(e.buttonID == 'btnIgualar')
           {
	       TipoOperacao = 'Igualar';
window.top.mostraMensagem('Deseja realmente Igualar o orçamento?', 'confirmacao', true, true, igualaPrevistoRealizado);
	       hfGeral.Set('TipoOperacao', TipoOperacao);
	}	
}" />


            <SettingsPager PageSize="100"></SettingsPager>

            <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFooter="True" ShowGroupedColumns="True" VerticalScrollBarMode="Visible"></Settings>

            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>

            <SettingsDataSecurity AllowInsert="False" />

            <SettingsText GroupPanel="Para agrupar, arraste uma coluna aqui"></SettingsText>
            <Columns>
                <dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="180px" VisibleIndex="0">
                    <CustomButtons>
                        <dxcp:GridViewCommandColumnCustomButton ID="btnIgualar" Text="Igualar Orçamento">
                            <Image Url="~/imagens/igualar.png"></Image>
                        </dxcp:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                            </Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                            </Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                            <Image Url="~/imagens/botoes/pFormulario.PNG">
                            </Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td align="center">
                                    <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                        ClientInstanceName="menu" ItemSpacing="5px"  OnInit="menu_Init" OnItemClick="menu_ItemClick">
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
                </dxcp:GridViewCommandColumn>
                <dxcp:GridViewDataTextColumn FieldName="CodigoPrevisao" Caption="CodigoPrevisao" VisibleIndex="1" Visible="False">
                </dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="DescricaoPrevisao" Caption="Descrição" VisibleIndex="2">
                </dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="Observacao" Caption="Observação" Visible="False" VisibleIndex="4">
                </dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="CodigoStatusPrevisaoFluxoCaixa" Visible="False" VisibleIndex="10" Caption="CodigoStatusPrevisaoFluxoCaixa"></dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="DescricaoStatusPrevisaoFluxoCaixa" VisibleIndex="5" Caption="Status"></dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="PodeExcluir" Visible="False" VisibleIndex="11"></dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="PodeAlterarStatus" Visible="False" VisibleIndex="12"></dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="PodeIgualarPrevistoRealizado" Visible="False" VisibleIndex="13"></dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="CodigoNovoStatusPermitido" Visible="False" VisibleIndex="14"></dxcp:GridViewDataTextColumn>
                <dxcp:GridViewDataTextColumn FieldName="CodigoStatusAnteriorPermitido" Visible="False" VisibleIndex="15"></dxcp:GridViewDataTextColumn>
                <dxtv:GridViewDataSpinEditColumn Caption="Ano" FieldName="AnoOrcamento" VisibleIndex="3">
                    <PropertiesSpinEdit DisplayFormatString="g">
                    </PropertiesSpinEdit>
                </dxtv:GridViewDataSpinEditColumn>
                <dxtv:GridViewDataSpinEditColumn FieldName="MesInicioBloqueio" Visible="False" VisibleIndex="6">
                    <PropertiesSpinEdit DisplayFormatString="g">
                    </PropertiesSpinEdit>
                </dxtv:GridViewDataSpinEditColumn>
                <dxtv:GridViewDataSpinEditColumn FieldName="MesTerminoBloqueio" Visible="False" VisibleIndex="7">
                    <PropertiesSpinEdit DisplayFormatString="g">
                    </PropertiesSpinEdit>
                </dxtv:GridViewDataSpinEditColumn>
                <dxtv:GridViewDataDateColumn FieldName="InicioPeriodoElaboracao" Visible="False" VisibleIndex="8">
                    <PropertiesDateEdit DisplayFormatString="">
                    </PropertiesDateEdit>
                </dxtv:GridViewDataDateColumn>
                <dxtv:GridViewDataDateColumn FieldName="TerminoPeriodoElaboracao" Visible="False" VisibleIndex="9">
                    <PropertiesDateEdit DisplayFormatString="">
                    </PropertiesDateEdit>
                </dxtv:GridViewDataDateColumn>
            </Columns>
        </dxcp:ASPxGridView>
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
        <dxcp:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
           if(s.cp_Erro != '')
           {
                        window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
            }
           else
           {
                       if(s.cp_Sucesso != '')
                      {
                                  window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
                       }
           }
            gvDados.Refresh();
            pcDados.Hide();
            s.cp_Erro = '';
            s.cp_Sucesso = '';
}" />
        </dxcp:ASPxCallback>
        <dxcp:ASPxGridViewExporter ID="gridviewExporter" runat="server" GridViewID="gvDados">
        </dxcp:ASPxGridViewExporter>
    </p>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" runat="Server">
</asp:Content>

