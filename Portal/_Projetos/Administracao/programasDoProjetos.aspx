<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="programasDoProjetos.aspx.cs" Inherits="_Projetos_Administracao_programasDoProjetos" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 10px; height: 10px;"></td>
                <td style="height: 10px"></td>
                <td style="width: 10px; height: 10px;"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <!-- ASPxCALLBACKPANEL principal: pnCallback -->
                     <div id="divGrid" style="visibility:visible;">
                         <dxcp:ASPxCallback ID="callbackTela" runat="server" ClientInstanceName="callbackTela" OnCallback="callbackTela_Callback">
                             <ClientSideEvents EndCallback="function(s, e) {
     if(s.cpErro != '')
    {
         window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
    }
    else
    {
        if(s.cpSucesso != '')
        {
              window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 2700);
              gvDados.Refresh();
        }
         }
}" />
                         </dxcp:ASPxCallback>
                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoProjeto"
                        AutoGenerateColumns="False" ID="gvDados" Width="100%"
                        OnAfterPerformCallback="gvDados_AfterPerformCallback">
                        <ClientSideEvents CustomButtonClick="function(s, e) {
    //gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnIncluirCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
     }
     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
                 s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoProjeto', editarPrograma);           
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
                    s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoProjeto', excluirPrograma);      
     }
     else if(e.buttonID == &quot;btnDetalheCustom&quot;)
     {	
                  s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoProjeto', visualizarPrograma);           
     }
      else if(e.buttonID == &quot;btnPesos&quot;)
       {
          lpLoading.Show();
         s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoProjeto', MontaCallbackPesos);
          
       }	
}"
                            FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s, true);
}"  Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>

                        <SettingsCommandButton>
                            <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                            <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                        </SettingsCommandButton>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="A&#231;&#245;es" VisibleIndex="0"
                                Width="120px">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnIncluirCustom" Text="Inserir" Visibility="Invisible">
                                        <Image AlternateText="Inserir" Url="~/imagens/botoes/incluirReg02.png">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                        <Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                        <Image AlternateText="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalheCustom" Text="Detalhe">
                                        <Image AlternateText="Detalhe" Url="~/imagens/botoes/pFormulario.png">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                    <dxtv:GridViewCommandColumnCustomButton ID="btnPesos" Text="Editar Pesos dos Projetos Vinculados">
                                        <Image AlternateText="Editar" Url="~/imagens/botoes/NovoMenu.png">
                                        </Image>
                                    </dxtv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                    ClientInstanceName="menu"
                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                    OnInit="menu_Init">
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
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Programa" FieldName="NomeProjeto" Name="NomeProjeto"
                                VisibleIndex="1">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Unidade" FieldName="NomeUnidadeNegocio" Name="SiglaUnidadeNegocio"
                                VisibleIndex="3" Width="300px">
                                <HeaderStyle HorizontalAlign="Left" />
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Gerente" FieldName="NomeGerente" Name="NomeGerente"
                                VisibleIndex="5" Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" Name="CodigoProjeto" Visible="False"
                                VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoMSProject" Name="CodigoMSProject"
                                Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoGerenteProjeto" Name="CodigoGerenteProjeto"
                                Visible="False" VisibleIndex="6">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeNegocio" Name="CodigoUnidadeNegocio"
                                Visible="False" VisibleIndex="7">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" ShowGroupPanel="True"></Settings>
                        <Styles>
                            <EmptyDataRow BackColor="#EEEEDD" ForeColor="Black">
                            </EmptyDataRow>
                        </Styles>
                    </dxwgv:ASPxGridView>
                         </div>
                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>
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

                                <dxtv:ASPxPopupControl ID="pcPesos" runat="server" AllowDragging="True" ClientInstanceName="pcPesos" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="900px">
                                    <ContentStyle>
                                        <Paddings Padding="8px" />
                                    </ContentStyle>
                                    <ContentCollection>
                                        <dxtv:PopupControlContentControl runat="server">
                                            <!-- table -->
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <dxtv:ASPxGridView ID="gvPesos" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvPesos" KeyFieldName="CodigoProjeto" OnCustomCallback="gvPesos_CustomCallback" Width="100%" OnRowUpdating="gvPesos_RowUpdating">

                                                                <ClientSideEvents BeginCallback="function(s, e) {
	comando = e.command;
}"
                                                                    EndCallback="function(s, e) {
        //alert(comando);
        if(comando == &quot;CUSTOMCALLBACK&quot;)
        {
               pcPesos.Show();
               lpLoading.Hide();
        }
         if(comando == &quot;UPDATEEDIT&quot;)
        {
                if(s.cp_erro != &quot;&quot;)
                {
            window.top.mostraMensagem(s.cp_erro, 'erro', true, false, null);           
                 }
                 else
                 {
                           if(s.cp_sucesso != &quot;&quot;)
                          {
                                  window.top.mostraMensagem(s.cp_sucesso, 'sucesso', false, false, null);   
                          }
                 }
                 s.cp_erro = &quot;&quot;;
                 s.cp_sucesso = &quot;&quot;;       
           }
}" />

                                                                <SettingsPager Mode="ShowAllRecords">
                                                                </SettingsPager>
                                                                <SettingsEditing Mode="PopupEditForm">
                                                                </SettingsEditing>
                                                                <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                                                <SettingsCommandButton>
                                                                    <ShowAdaptiveDetailButton RenderMode="Image">
                                                                    </ShowAdaptiveDetailButton>
                                                                    <HideAdaptiveDetailButton RenderMode="Image">
                                                                    </HideAdaptiveDetailButton>
                                                                </SettingsCommandButton>
                                                                <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>
                                                                <Columns>
                                                                    <dxtv:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True" VisibleIndex="0" Width="80px" ButtonRenderMode="Image" ShowEditButton="True">
                                                                    </dxtv:GridViewCommandColumn>
                                                                    <dxtv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="True">
                                                                        <PropertiesTextEdit EncodeHtml="False">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </PropertiesTextEdit>
                                                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                                                        <EditFormSettings CaptionLocation="None" Visible="False" />
                                                                    </dxtv:GridViewDataTextColumn>
                                                                    <dxtv:GridViewDataSpinEditColumn Caption="Peso" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px" FieldName="PesoProjetoFilho">
                                                                        <PropertiesSpinEdit DisplayFormatString="g" MaxLength="3" MaxValue="999" NumberType="Integer">
                                                                            <SpinButtons ClientVisible="False">
                                                                            </SpinButtons>
                                                                        </PropertiesSpinEdit>
                                                                    </dxtv:GridViewDataSpinEditColumn>
                                                                    <dxtv:GridViewDataTextColumn FieldName="CodigoProjeto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                    </dxtv:GridViewDataTextColumn>
                                                                </Columns>
                                                            </dxtv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td align="right">
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td style="width: 20px">
                                                                                    <img border='0' src='../../imagens/projeto.PNG' title='<%# Resources.traducao.programasDoProjetos_projeto %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, programasDoProjetos_projeto %>" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                    <img border='0' src='../../imagens/processo.PNG' style='width: 21px; height: 18px;' title='<%# Resources.traducao.programasDoProjetos_processo %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, programasDoProjetos_processo %>" />
                                                                                </td>
                                                                                <td style="width: 20px">
                                                                                    <img border='0' src='../../imagens/agile.PNG' style='width: 21px; height: 18px;' title='<%# Resources.traducao.programasDoProjetos_projeto__gil %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, programasDoProjetos_projeto__gil %>" /></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td align="right">
                                                                        <dxtv:ASPxButton ID="btnCancelar0" runat="server" ClientInstanceName="btnCancelar" Style="margin: 10px" Text="Fechar" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
                pcPesos.Hide();
}" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxtv:PopupControlContentControl>
                                    </ContentCollection>
                                </dxtv:ASPxPopupControl>

                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <!-- ASPxGRidVIEW: gvDados -->
                                
                                <!-- ASPxHidDENFIELD hfGeral-->
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) {
    if(s.cpErro != '')
    {
         window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
    }
    else
    {
        if(s.cpSucesso != '')
        {
              window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 2700);
gvDados.Refresh();
        }
    }
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                    <dxlp:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading" Font-Size="10pt"
                        Height="73px" HorizontalAlign="Center" Text="Carregando&amp;hellip;" VerticalAlign="Middle" Width="180px">
                    </dxlp:ASPxLoadingPanel>
                </td>
                <td></td>
            </tr>
        </table>
    </div>
</asp:Content>
