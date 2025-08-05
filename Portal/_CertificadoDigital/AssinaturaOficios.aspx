<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="AssinaturaOficios.aspx.cs" Inherits="_CertificadoDigital_AssinaturaOficios" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
                        
        .NoStyle
        {
            background-repeat: no-repeat;
            background-color: transparent;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    
    <table style="width: 100%">
        <tr>
            <td style="width: 10px; height: 10px">
            </td>
            <td style="height: 10px">
            </td>
            <td style="width: 10px; height: 10px;">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>

    <dxcp:ASPxGridView runat="server" ClientInstanceName="gvDados" 
                    KeyFieldName="CodigoOficio" 
                    AutoGenerateColumns="False" Width="100%" 
                     ID="gvDados" 
                    oncustomjsproperties="gvDados_CustomJSProperties" OnAfterPerformCallback="gvDados_AfterPerformCallback" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
<ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Arquivo != '')
		window.open(window.top.pcModal.cp_Path + s.cp_Arquivo, '_blank');
}" ContextMenu="grid_ContextMenu
" SelectionChanged="OnSelectionChanged"></ClientSideEvents>
<Columns>
    
    <dxtv:GridViewCommandColumn Caption=" " SelectAllCheckboxMode="AllPages" 
        ShowSelectCheckbox="True" VisibleIndex="0" Width="30px">
    </dxtv:GridViewCommandColumn>
    <dxtv:GridViewDataTextColumn Caption=" " Name="Ações" VisibleIndex="1"  FieldName="CodigoWorkflow"
        Width="100px">
        <HeaderStyle  HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
        <HeaderCaptionTemplate>
            <table>
                <tr>
                    <td align="center">
                        <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" 
                            ClientInstanceName="menu" ItemSpacing="5px" oninit="menu_Init" 
                            onitemclick="menu_ItemClick">
                            <Paddings Padding="0px" />
                            <Items>
                                <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir" Visible="True" ClientVisible="False">
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
                                        <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" 
                                            ToolTip="Exportar para HTML">
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
        </HeaderCaptionTemplate>
        <DataItemTemplate>
        <table>
            <tr>
                <%if (!string.IsNullOrEmpty(Request.QueryString["CD"]))
                    { %>
                <td>
                    <img alt="Visualizar Graficamente" onclick='<%# string.Format(@"processaClickBotao(""{0}"", ""{1}"", ""{2}"", ""{3}"", ""{4}"", ""{5}"");"
            , "G"
            , Eval("CodigoWorkflow")
            , Eval("CodigoInstanciaWf")
            , Eval("CodigoEtapaAtual")
            , Eval("OcorrenciaAtual")
            , Eval("CodigoFluxo")) %>'  style="cursor:pointer" src="../imagens/botoes/fluxos.PNG" />
                </td>
                <td>
                    <img alt="Interagir" onclick='<%# string.Format(@"processaClickBotao(""{0}"", ""{1}"", ""{2}"", ""{3}"", ""{4}"", ""{5}"");"
            , "I"
            , Eval("CodigoWorkflow")
            , Eval("CodigoInstanciaWf")
            , Eval("CodigoEtapaAtual")
            , Eval("OcorrenciaAtual")
            , Eval("CodigoFluxo")) %>'  style="cursor:pointer" src="../imagens/botoes/interagir.png" />
                </td>
                <%} %>
                <td>
                    <dxtv:ASPxButton ID="btnPDF" runat="server" CssClass="NoStyle" Cursor="pointer" 
                        ImageSpacing="0px" onclick="btnPDF_Click" RenderMode="Link">
                        <Image Url="~/imagens/botoes/btnPDF.png">
                        </Image>
                        <Paddings Padding="0px" />
                        <PressedStyle CssClass="NoStyle">
                        </PressedStyle>
                    </dxtv:ASPxButton>
                </td>
            </tr>
        </table>
    </DataItemTemplate>
    </dxtv:GridViewDataTextColumn>
    <dxcp:GridViewDataTextColumn FieldName="NumeroDemanda" 
        ShowInCustomizationForm="True" Name="colNumeroDemanda" 
        Caption="Número Demanda" VisibleIndex="2" Width="240px">

<Settings AllowAutoFilter="True" 
        AllowHeaderFilter="False" AutoFilterCondition="Contains" ></Settings>   

</dxcp:GridViewDataTextColumn>
<dxcp:GridViewDataTextColumn FieldName="Deliberacao" 
        ShowInCustomizationForm="True" Width="200px" Caption="Deliberação" 
        VisibleIndex="14" Visible="False">
<Settings AllowAutoFilter="True" AllowHeaderFilter="False" 
        AutoFilterCondition="Contains"></Settings>
</dxcp:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Assinado?" FieldName="OficioAssinado" 
        VisibleIndex="4" Width="70px" Visible="False">
        <Settings AllowAutoFilter="True" AllowHeaderFilter="False" />
    </dxtv:GridViewDataTextColumn>
<dxcp:GridViewDataTextColumn FieldName="SiglaOrgao" 
        ShowInCustomizationForm="True" Width="300px" Caption="Órgão" 
        VisibleIndex="5">
<Settings AllowAutoFilter="True" AllowHeaderFilter="False" 
        AutoFilterCondition="Contains"></Settings>

<HeaderStyle ></HeaderStyle>
</dxcp:GridViewDataTextColumn>
<dxcp:GridViewDataTextColumn FieldName="Decisao" 
        ShowInCustomizationForm="True" Width="300px" Caption="Status" 
        VisibleIndex="6">
<Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" 
        AllowAutoFilter="True" ShowFilterRowMenu="False"></Settings>
</dxcp:GridViewDataTextColumn>
<dxcp:GridViewDataTextColumn FieldName="NumeroOficio" 
        ShowInCustomizationForm="True" VisibleIndex="7" 
        Caption="Número Ofício" Width="300px">
    <Settings AllowAutoFilter="True" AllowHeaderFilter="False" />
    </dxcp:GridViewDataTextColumn>
<dxcp:GridViewDataTextColumn FieldName="TituloDemanda" VisibleIndex="3" 
        Caption="Título Demanda" Width="680px">

    <Settings AllowAutoFilter="True" AllowHeaderFilter="False" />

</dxcp:GridViewDataTextColumn>
<dxcp:GridViewDataTextColumn FieldName="TipoDeliberacao" 
        ShowInCustomizationForm="True" Width="300px" Caption="Tipo Deliberação" 
        VisibleIndex="8">
    <Settings AllowHeaderFilter="False"  
        AllowAutoFilter="True" AutoFilterCondition="Contains" />
<HeaderStyle ></HeaderStyle>
</dxcp:GridViewDataTextColumn>
<dxcp:GridViewDataTextColumn FieldName="ValorSolicitado" 
        VisibleIndex="10" Caption="Valor Solicitado" Width="300px" 
        ShowInCustomizationForm="True">
    <PropertiesTextEdit DisplayFormatString="n2">
    </PropertiesTextEdit>
    <HeaderStyle HorizontalAlign="Right" />
    <CellStyle HorizontalAlign="Right">
    </CellStyle>
    </dxcp:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Valor Solicitado Exercício" 
        VisibleIndex="11" FieldName="ValorSolicitadoExercicio" 
        ShowInCustomizationForm="True" Width="200px">
        <PropertiesTextEdit DisplayFormatString="n2">
        </PropertiesTextEdit>
        <HeaderStyle HorizontalAlign="Right" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxtv:GridViewDataTextColumn>
<dxcp:GridViewDataTextColumn FieldName="ValorAprovado" 
        ShowInCustomizationForm="True" Caption="Valor Aprovado" VisibleIndex="12" Width="200px">
    <PropertiesTextEdit DisplayFormatString="n2">
    </PropertiesTextEdit>
<Settings AllowAutoFilter="False" AllowHeaderFilter="False"></Settings>
    <HeaderStyle HorizontalAlign="Right" />
    <CellStyle HorizontalAlign="Right">
    </CellStyle>
</dxcp:GridViewDataTextColumn>
    <dxtv:GridViewDataDateColumn Caption="Data Deliberação" 
        FieldName="DataDeliberacao" ShowInCustomizationForm="True" VisibleIndex="9" 
        Width="300px">
        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
        </PropertiesDateEdit>
        <Settings AllowAutoFilter="True" AllowHeaderFilter="False" 
            AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
    </dxtv:GridViewDataDateColumn>
<dxcp:GridViewDataTextColumn FieldName="ValorAprovadoExercicio" 
        ShowInCustomizationForm="True" 
        VisibleIndex="13" Caption="Valor Aprovado Exercício" Width="200px">
    <Settings AllowAutoFilter="False" AllowHeaderFilter="False" 
        AutoFilterCondition="Contains" />
    <PropertiesTextEdit DisplayFormatString="n2">
    </PropertiesTextEdit>
    <Settings AllowAutoFilter="True" 
        AllowHeaderFilter="False" AutoFilterCondition="Contains" 
        ShowFilterRowMenu="False" />
    <HeaderStyle HorizontalAlign="Right" />
    <CellStyle HorizontalAlign="Right">
    </CellStyle>
    </dxcp:GridViewDataTextColumn>
    <dxtv:GridViewDataDateColumn Caption="Data Ofício" FieldName="DataOficio" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="15" 
        Width="110px">
        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
        </PropertiesDateEdit>
        <Settings AllowAutoFilter="True" AllowHeaderFilter="False" 
            AutoFilterCondition="Contains" ShowFilterRowMenu="True" />
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
    </dxtv:GridViewDataDateColumn>
    <dxtv:GridViewDataTextColumn FieldName="CodigoFormularioAssinar" 
        ShowInCustomizationForm="False" Visible="False" VisibleIndex="16">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="CodigoEtapaInicial" 
        ShowInCustomizationForm="False" Visible="False" VisibleIndex="17">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="CodigoProjeto" 
        ShowInCustomizationForm="False" Visible="False" VisibleIndex="18">
    </dxtv:GridViewDataTextColumn>    
    <dxtv:GridViewDataTextColumn FieldName="CodigoInstanciaWf" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="CodigoWorkflow" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
    </dxtv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" 
            EnableCustomizationWindow="True"></SettingsBehavior>
<SettingsResizing  ColumnResizeMode="Control"/>
<SettingsPager PageSize="100" AlwaysShowPager="True"></SettingsPager>

<Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" ShowGroupPanel="True" 
            VerticalScrollBarMode="Visible" ShowStatusBar="Hidden" 
            HorizontalScrollBarMode="Auto"></Settings>

<SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>

<SettingsPopup>
<HeaderFilter Width="200px" Height="350px"></HeaderFilter>
</SettingsPopup>

<Styles>
<Header ></Header>

<EmptyDataRow ></EmptyDataRow>

<GroupPanel ></GroupPanel>

<HeaderPanel ></HeaderPanel>

<CommandColumn HorizontalAlign="Center"></CommandColumn>

<CommandColumnItem>
<Paddings Padding="1px"></Paddings>
</CommandColumnItem>

    <StatusBar Font-Bold="True"  
        Font-Italic="True">
    </StatusBar>

<HeaderFilterItem ></HeaderFilterItem>
</Styles>

<StylesEditors>
<ButtonEdit ></ButtonEdit>
</StylesEditors>
        <Templates>
            <StatusBar>
                <%# getRowCount() %> 
            </StatusBar>
        </Templates>
</dxcp:ASPxGridView>

            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 10px; height: 10px;">
                </td>
            <td style="height: 10px">

    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" 
            ID="ASPxGridViewExporter1" onrenderbrick="ASPxGridViewExporter1_RenderBrick">
<Styles>
<Default ></Default>

<Header ></Header>

<Cell ></Cell>

<GroupFooter Font-Bold="True" ></GroupFooter>

<Title Font-Bold="True" ></Title>
</Styles>
</dxwgv:ASPxGridViewExporter>

    <dxm:ASPxPopupMenu ID="popupMenu" runat="server" ClientInstanceName="popupMenu">
    </dxm:ASPxPopupMenu>
    <dxm:ASPxPopupMenu ID="pmColumnMenu" runat="server" ClientInstanceName="pmColumnMenu">
        <Items>
            <dxm:MenuItem Name="cmdShowCustomization" Text="Selecionar campos">
            </dxm:MenuItem>
        </Items>
        <ClientSideEvents ItemClick="function(s, e) {
	if(e.item.name == 'cmdShowCustomization')
        gvDados.ShowCustomizationWindow();
}" />
    </dxm:ASPxPopupMenu>

                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" 
                    ClientInstanceName="callbackSalvar" ClientVisible="False" 
                    oncallback="pnCallback_Callback">
                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Status == 'OK')
	{
		window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null);
		gvDados.PerformCallback();
	}
	else
		window.top.mostraMensagem(s.cp_Msg, 'erro', true, false, null);
}
" />
                    <PanelCollection>
<dxcp:PanelContent runat="server">
    <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxtv:ASPxHiddenField>
                        </dxcp:PanelContent>
</PanelCollection>
                </dxcp:ASPxCallbackPanel>

            </td>
            <td style="width: 10px; height: 10px;">
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="right">
                <dxcp:ASPxButton ID="ASPxButton3" runat="server" Text="Assinar Ofícios Selecionados" Width="190px" AutoPostBack="False" 
                    ValidationGroup="MKE" ClientInstanceName="btnAssinar" 
                    OnCustomJSProperties="ASPxButton3_CustomJSProperties" onload="ASPxButton3_Load">
                    <ClientSideEvents Click="function(s, e) {
	onClick(s, e);
}" />
                    <Paddings Padding="0px" />
                </dxcp:ASPxButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
        <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents CallbackComplete="function(s, e) {
	if(e.parameter == 'prosseguirSemAssinaturaDigital'){
        if(e.result == 'sucesso'){
		    gvDados.Refresh();
}
        else
            window.top.mostraMensagem('Falha ao realizar a operação', 'erro', true, false, null);
}
}" />
        </dxcp:ASPxCallback>
</form>
</body>
</html>

