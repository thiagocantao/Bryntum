<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento.aspx.cs" Inherits="_Portfolios_frameSelecaoBalanceamento" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
       .fonte12px span{
            font-size:12px
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
                <script type="text/javascript" language="javascript">
                function mostrarRelatorioPortfolio()
                {
                    lpLoading.Show();
                    callBackVC.PerformCallback('export');
                    
                }
                </script>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 26px;">
                    <tr style="height:26px">
                        <td valign="middle">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td valign="middle" style="padding-left: 10px">
                                        <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloSelecao"
                                Font-Bold="True"  Text="Seleção e Balanceamento">
                            </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="middle" style="padding-right: 10px;">
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                            Text="Portfólio:">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 325px" valign="middle">
                                        <dxe:ASPxComboBox ID="ddlPortfolio" runat="server" ClientInstanceName="ddlPortfolio" Width="317px" DropDownStyle="DropDown"  >
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	callBackVC.PerformCallback('AtualizarVC');
}" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td style="width: 38px" valign="middle">
                                        <dxe:ASPxImage ID="btnAtualizar" runat="server" ImageUrl="~/imagens/botoes/ativar.PNG"
                                            ToolTip="Atualizar Cenários" Cursor="pointer">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td style="width: 50px" valign="middle">
                                        <dxe:ASPxImage ID="btnRelatorioRisco" runat="server" ClientInstanceName="btnRelatorioRisco"
                                            ImageUrl="~/imagens/botoes/btnPDF.png" ToolTip="Gerar Relatório em PDF" 
                                            Cursor="pointer" Height="18px">
                                            <ClientSideEvents Click="function(s, e) {
	mostrarRelatorioPortfolio();
}" />
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <dxcp:ASPxCallbackPanel ID="callBackVC" runat="server" ClientInstanceName="callBackVC" OnCallback="callBackVC_Callback1" Width="100%">
                    <ClientSideEvents EndCallback="function(s, e) {
                        
	if(s.cp_Funcao == &quot;AtualizarVC&quot;)
	{
		//document.getElementById('framePrincipal').src = tcOpcoes.GetActiveTab().GetNavigateUrl();
    		framePrincipal.frameElement.src = tcOpcoes.GetActiveTab().GetNavigateUrl();
	}
	else if(s.cp_Funcao == &quot;export&quot;)
	{
        lpLoading.Hide();
        //window.top.showModal('../_Portfolios/frameSelecaoBalanceamento_Relatorio.aspx', '', screen.width - 60, screen.height - 260, '', null);
        window.location = &quot;../_Processos/Visualizacao/ExportacaoDados.aspx?exportType=pdf&amp;bInline=false&quot;;
        
	}	

}
" />
                    <PanelCollection>
<dxcp:PanelContent runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">

        <tr>
            <td></td>
            <td>
                <dxtv:ASPxTabControl ID="tcOpcoes" runat="server" ActiveTabIndex="6" ClientInstanceName="tcOpcoes"  Width="100%">
                    <Tabs>
                        <dxtv:Tab Name="ListaProjetos" Target="framePrincipal" Text="Projetos" >
                            <TabStyle Width="100" />
                        </dxtv:Tab>
                        <dxtv:Tab ClientVisible="False" Name="AnaliseGeral" Target="framePrincipal" Text="Análise Geral">
                            <TabStyle Width="100" />
                        </dxtv:Tab>
                        <dxtv:Tab Name="AnaliseGrafica" Target="framePrincipal" Text="Cenários">
                            <TabStyle Width="120" />
                        </dxtv:Tab>
                        <dxtv:Tab Name="AnaliseOlapFC" Target="framePrincipal" Text="Fluxo de Caixa">
                            <TabStyle Width="120">
                                <Paddings Padding="10px" />
                            </TabStyle>
                        </dxtv:Tab>
                        <dxtv:Tab Name="AnaliseOlapC" Target="framePrincipal" Text="Importância">
                            <TabStyle Width="125" />
                        </dxtv:Tab>
                        <dxtv:Tab Name="AnaliseOlapCpx" Target="framePrincipal" Text="Complexidade">
                            <TabStyle Width="135" />
                        </dxtv:Tab>
                        <dxtv:Tab Name="AnaliseOlapR" Target="framePrincipal" Text="Recursos">
                            <TabStyle Width="130" />
                        </dxtv:Tab>
                        <dxtv:Tab Name="Simulacao" Target="framePrincipal" Text="Simulação">
                            <TabStyle Width="115" />
                        </dxtv:Tab>
                        <dxtv:Tab Name="Publicacao" Target="framePrincipal" Text="Publicação">
                            <TabStyle Width="135" />
                        </dxtv:Tab>
                    </Tabs>
                    <ClientSideEvents TabClick="function(s, e) {
                        if(e.tab.name === tcOpcoes.GetActiveTab().name){
                            e.htmlEvent.preventDefault();
                        }
}" />
                    <TabStyle CssClass="fonte12px">
                        <Paddings PaddingLeft="30px" PaddingRight="30px" />
                        
                    </TabStyle>
                </dxtv:ASPxTabControl>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <dxtv:ASPxPanel ID="ASPxPanel1" runat="server">
                    <PanelCollection>
                        <dxtv:PanelContent runat="server">
                            <iframe frameborder="0" name="framePrincipal" scrolling="yes" src="frameSelecaoBalanceamento_Resumo.aspx" style="height: <%=alturaTabela %>" width="100%"></iframe>
                        </dxtv:PanelContent>
                    </PanelCollection>
                    <BorderLeft BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                    <BorderTop BorderStyle="None" />
                    <BorderRight BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                    <BorderBottom BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                </dxtv:ASPxPanel>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <dxtv:ASPxPopupControl ID="pcAguarde" runat="server" ClientInstanceName="pcAguarde" CloseAction="None" HeaderText="Download" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                    <ContentCollection>
                        <dxtv:PopupControlContentControl runat="server">
                            <asp:Label ID="Label1" runat="server" Text="<%# Resources.traducao.frameSelecaoBalanceamento_aguarde___ %>"></asp:Label>
                        </dxtv:PopupControlContentControl>
                    </ContentCollection>
                </dxtv:ASPxPopupControl>
                <dxtv:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading" HorizontalAlign="Center" Text="" VerticalAlign="Middle">
                </dxtv:ASPxLoadingPanel>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <dxtv:ASPxPopupControl ID="pcAtualizacao" runat="server" ClientInstanceName="pcAtualizacao"  HeaderText="Atualização de Cenários" Modal="True" PopupElementID="btnAtualizar" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="900px" AllowDragging="True" PopupVerticalOffset="15">
                    <ClientSideEvents Closing="function(s, e) {
	framePrincipal.atualizaDados();
}" PopUp="function(s, e) {
	 var sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 100;        
       s.SetWidth(sWidth);
       s.SetHeight(sHeight); 
       s.UpdatePosition();

}" />
                    <HeaderStyle Font-Bold="True" />
                    <ContentCollection>
                        <dxtv:PopupControlContentControl runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td>
                                        <dxtv:ASPxGridView ID="gvProjetos" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvProjetos"  OnAfterPerformCallback="gvProjetos_AfterPerformCallback" OnCustomCallback="gvProjetos_CustomCallback" Width="100%">
                                            <Templates>
                                                <FooterRow>
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td style="background-color: #619340;border-radius: 50%; display: inline-block;height: 16px; width: 16px;">&nbsp;</td>
                                                                <td></td>
                                                                <td style="font-size:12px; color:#575757;"><asp:Literal runat="server" Text="<%$ Resources:traducao, frameSelecaoBalanceamento_projetos_n_o_associados_ao_portf_lio %>" /></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </FooterRow>
                                            </Templates>
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <Settings ShowFilterRow="True" ShowFooter="True" VerticalScrollableHeight="280" VerticalScrollBarMode="Visible" />
                                            <SettingsCommandButton>
                                                <ShowAdaptiveDetailButton RenderMode="Image">
                                                </ShowAdaptiveDetailButton>
                                                <HideAdaptiveDetailButton RenderMode="Image">
                                                </HideAdaptiveDetailButton>
                                            </SettingsCommandButton>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                                            <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar" />
                                            <Columns>
                                                <dxtv:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" Width="110px">
                                                </dxtv:GridViewCommandColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" ShowInCustomizationForm="True" VisibleIndex="1">
                                                    <DataItemTemplate>
                                                        <span style='color: <%# Eval("IndicaItalico").ToString() == "S" ? "#339900" : "#000000" %>'><%# Eval("NomeProjeto") + "" != "" ? Eval("NomeProjeto") : "&nbsp;"%></span>
                                                    </DataItemTemplate>
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Cen. 1" ShowInCustomizationForm="True" VisibleIndex="10" Width="55px">
                                                    <Settings AllowAutoFilter="False" />
                                                    <DataItemTemplate>
                                                        <%# getCenarioMarcado("check1", Eval("IndicaCenario1").ToString(), "1;", Eval("_CodigoProjeto").ToString())%>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Cen. 2" ShowInCustomizationForm="True" VisibleIndex="11" Width="55px">
                                                    <Settings AllowAutoFilter="False" />
                                                    <DataItemTemplate>
                                                        <%# getCenarioMarcado("check2", Eval("IndicaCenario2").ToString(), "2;", Eval("_CodigoProjeto").ToString())%>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Cen. 3" ShowInCustomizationForm="True" VisibleIndex="12" Width="55px">
                                                    <Settings AllowAutoFilter="False" />
                                                    <DataItemTemplate>
                                                        <%# getCenarioMarcado("check3", Eval("IndicaCenario3").ToString(), "3;", Eval("_CodigoProjeto").ToString())%>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Cen. 4" ShowInCustomizationForm="True" VisibleIndex="13" Width="55px">
                                                    <Settings AllowAutoFilter="False" />
                                                    <DataItemTemplate>
                                                        <%# getCenarioMarcado("check4", Eval("IndicaCenario4").ToString(), "4;", Eval("_CodigoProjeto").ToString())%>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Cen. 5" ShowInCustomizationForm="True" VisibleIndex="14" Width="55px">
                                                    <Settings AllowAutoFilter="False" />
                                                    <DataItemTemplate>
                                                        <%# getCenarioMarcado("check5", Eval("IndicaCenario5").ToString(), "5;", Eval("_CodigoProjeto").ToString())%>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Cen. 6" ShowInCustomizationForm="True" VisibleIndex="15" Width="55px">
                                                    <Settings AllowAutoFilter="False" />
                                                    <DataItemTemplate>
                                                        <%# getCenarioMarcado("check6", Eval("IndicaCenario6").ToString(), "6;", Eval("_CodigoProjeto").ToString())%>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Cen. 7" ShowInCustomizationForm="True" VisibleIndex="16" Width="55px">
                                                    <Settings AllowAutoFilter="False" />
                                                    <DataItemTemplate>
                                                        <%# getCenarioMarcado("check7", Eval("IndicaCenario7").ToString(), "7;", Eval("_CodigoProjeto").ToString())%>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Cen. 8" ShowInCustomizationForm="True" VisibleIndex="17" Width="55px">
                                                    <Settings AllowAutoFilter="False" />
                                                    <DataItemTemplate>
                                                        <%# getCenarioMarcado("check8", Eval("IndicaCenario8").ToString(), "8;", Eval("_CodigoProjeto").ToString())%>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Cen. 9" ShowInCustomizationForm="True" VisibleIndex="18" Width="55px">
                                                    <Settings AllowAutoFilter="False" />
                                                    <DataItemTemplate>
                                                        <%# getCenarioMarcado("check9", Eval("IndicaCenario9").ToString(), "9;", Eval("_CodigoProjeto").ToString())%>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxtv:GridViewDataTextColumn>
                                            </Columns>
                                        </dxtv:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <dxtv:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar"  Text="Fechar" Width="90px">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    pcAtualizacao.Hide();
}" />
                                        </dxtv:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxtv:PopupControlContentControl>
                    </ContentCollection>
                </dxtv:ASPxPopupControl>
            </td>
            <td></td>
        </tr>
    </table>
                        </dxcp:PanelContent>
</PanelCollection>
                </dxcp:ASPxCallbackPanel>

</asp:Content>

