<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="adm_IntegracaoZeus.aspx.cs" Inherits="administracao_adm_IntegracaoZeus" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <div>
<table>
<tr>
<td>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr style="height:26px">
                <td valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 100%">
                        <tr style="height:26px">
                            <td valign="middle" style="padding-left: 10px">
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" 
                                    Font-Strikeout="False"></asp:Label>
                                    </td>
                                    <td align="right">
                <table>
                    <tr>
                        <td style="padding-right: 10px">
                            <dxe:ASPxButton ID="AspxbuttonGeraDotação" runat="server" 
                                Text="Gera Dotação" Width="150px" AutoPostBack="False" 
                                ClientInstanceName="btnGeraDotacao">
                                <ClientSideEvents Click="function(s, e) {
   
   var conf = ConfirmaGeraArquivos();
   e.processOnServer = false;
   hfGeral.Set(&quot;TipoConsulta&quot;, &quot;D&quot;);
   if (conf)
     CallbackCarga.PerformCallback(&quot;D&quot;);  
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                        <td style="padding-right: 10px">
                            <dxe:ASPxButton ID="AspxbuttonGeraTransposicao" runat="server" 
                                
                                Text="Gera Transposição" ClientInstanceName="btnGeraTransposicao" Width="150px"
                                AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
   
   var conf = ConfirmaGeraArquivos();
   e.processOnServer = false;
   hfGeral.Set(&quot;TipoConsulta&quot;, &quot;T&quot;);
   if (conf)
     CallbackCarga.PerformCallback(&quot;T&quot;);  
    
}" />
                                <Paddings Padding="0px" />
                            </dxe:ASPxButton>
                        </td>
                        <td style="padding-right: 10px;" align="left">
                            <dxe:ASPxButton ID="AspxbuttonGeraSuplementacao" runat="server"
                                Text="Gera Suplementação" ClientInstanceName="btnGeraSuplementacao"
                                Style="margin-left: 0px" Width="150px" AutoPostBack="False">

<Paddings Padding="0px"></Paddings>
                                <ClientSideEvents Click="function(s, e) {
   var conf = ConfirmaGeraArquivos();
   e.processOnServer = false;
   hfGeral.Set(&quot;TipoConsulta&quot;, &quot;S&quot;);
   if (conf)
     CallbackCarga.PerformCallback(&quot;S&quot;);  

}" />
                                <Paddings Padding="0px" />

                            </dxe:ASPxButton>
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
</table>
 <table>
        <tr>
            <td style="padding-bottom: 3px"><dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDadosGeraArquivo" LeftMargin="50" 
                    RightMargin="50" Landscape="True" ID="ASPxGridViewExporter1" 
                    ExportEmptyDetailGrid="True" PreserveGroupRowStates="False">
     <Styles>
         <Default >
         </Default>
         <Header >
         </Header>
         <Cell >
         </Cell>
         <Footer >
         </Footer>
         <GroupFooter >
         </GroupFooter>
         <GroupRow >
         </GroupRow>
         <Title ></Title>
     </Styles>
                </dxwgv:ASPxGridViewExporter>
                            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" 
                    ID="hfGeral"></dxhf:ASPxHiddenField>
                <dxlp:ASPxLoadingPanel ID="pnLoading" runat="server" 
                    ClientInstanceName="pnLoading" 
                    Font-Size="10pt" Height="43px" Modal="True" 
                    Text="Gerando o arquivo, aguarde..." Width="314px" 
                    HorizontalAlign="Center" VerticalAlign="Middle">
                </dxlp:ASPxLoadingPanel>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px; padding-right: 10px">
    <dxcp:ASPxCallbackPanel ID="CallbackCarga" runat="server" Width="100%" 
        ClientInstanceName="CallbackCarga" 
        oncallback="CallbackCarga_Callback" >
        <ClientSideEvents EndCallback="function(s, e) {
	pnLoading.Hide();
}" />
        <PanelCollection>
<dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="right">
                <dxp:ASPxPanel ID="pnExportacao" runat="server" ClientInstanceName="pnExportacao"
                    ClientVisible="False" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                            <table border="0" cellpadding="0" cellspacing="0" align="right" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <dxe:ASPxComboBox runat="server" ClientInstanceName="ddlExporta" 
                                                 ID="ddlExporta" Width="120px">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&#39;tipoArquivo&#39;, s.GetValue());
}"></ClientSideEvents>
</dxe:ASPxComboBox>

                                        </td>
                                        <td style="padding-left: 3px; width: 15px;">
                                            <dxcp:ASPxCallbackPanel runat="server"  
                                                 ClientInstanceName="pnImage" Width="23px" 
                                                Height="22px" ID="pnImage" OnCallback="pnImage_Callback"><PanelCollection>
<dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoCSV.png" 
                                                            Width="20px" Height="20px" ClientInstanceName="imgExportacao" 
                                                            ID="imgExportacao" ImageAlign="AbsMiddle"></dxe:ASPxImage>

                                                    </dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>

                                        </td>
                                        <td style="width: 90px">
                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnExcel" Text="Exportar" 
                                                 ID="AspxbtnExcel" 
                                                OnClick="btnExcel_Click" Width="100%">
<ClientSideEvents Click="function(s, e) 
{
	if(gvDadosGeraArquivo.pageRowCount == 0)
	{
		window.top.mostraMensagem(&quot;N&#227;o h&#225; Nenhuma informa&#231;&#227;o para exportar.&quot;, 'atencao', true, false, null);
		e.processOnServer = false;	
	}
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxp:ASPxPanel>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px; padding-right: 10px; padding-top: 10px; padding-bottom: 10px;">
                <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ClientVisible="False" 
                    ShowHeader="False" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                            <dxe:ASPxLabel ID="lblMostraMensagem" runat="server" 
                                ClientInstanceName="lblMostraMensagem" Font-Size="Small">
                                <Border BorderStyle="None" />
                            </dxe:ASPxLabel>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxrp:ASPxRoundPanel>
            </td>
        </tr>
    </table>
    <dxwgv:ASPxGridView ID="gvDadosGeraArquivo" runat="server" 
        AutoGenerateColumns="False" ClientInstanceName="gvDadosGeraArquivo" 
        ClientVisible="False"  
        KeyFieldName="CodigoProjeto" Width="100%" 
        OnHtmlRowPrepared="gvDadosGeraArquivo_HtmlRowPrepared">
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="Sistema" FieldName="col01" 
                ShowInCustomizationForm="True" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Tipo Movimento" FieldName="col02" 
                ShowInCustomizationForm="True" VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Ano Orçamento" FieldName="col03" 
                ShowInCustomizationForm="True" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Mês Orçamento" FieldName="col04" 
                ShowInCustomizationForm="True" VisibleIndex="3">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Cod. Empresa" FieldName="col05" 
                ShowInCustomizationForm="True" VisibleIndex="4">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Código Unidade" FieldName="col06" 
                ShowInCustomizationForm="True" VisibleIndex="5">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Código CR" FieldName="col07" 
                ShowInCustomizationForm="True" VisibleIndex="6">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Grupo Conta" FieldName="col08" 
                ShowInCustomizationForm="True" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="qtd_real_mes" FieldName="col09" 
                ShowInCustomizationForm="True" VisibleIndex="8" Visible="False">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Valor" ShowInCustomizationForm="True" 
                VisibleIndex="9" FieldName="col10" Width="150px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="status_registro" FieldName="col11" 
                ShowInCustomizationForm="True" VisibleIndex="10" Visible="False">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Nome Arquivo" FieldName="col12" 
                ShowInCustomizationForm="True" VisibleIndex="11">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ano_plano" FieldName="col13" 
                ShowInCustomizationForm="True" VisibleIndex="12" Visible="False">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="cod_conta_contabil" FieldName="col14" 
                ShowInCustomizationForm="True" VisibleIndex="13" Visible="False">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="cod_entrada" FieldName="col15" 
                ShowInCustomizationForm="True" VisibleIndex="14" Visible="False">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Data Atualização" FieldName="col16" 
                ShowInCustomizationForm="True" VisibleIndex="15" Width="200px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="cod_pln_uni" FieldName="col17" 
                ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="cod_pln_pro" FieldName="col18" 
                ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="cod_pln_cta" FieldName="col19" 
                ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
            </dxwgv:GridViewDataTextColumn>
        </Columns>
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <Settings HorizontalScrollBarMode="Visible" ShowTitlePanel="True" 
            VerticalScrollBarMode="Visible" ShowFooter="True" />
        <Paddings PaddingTop="5px" />
        <Templates>
            <FooterRow>
                <table cellpadding="0" cellspacing="0" style="WIDTH: 100%">
                    <tbody>
                        <tr>
                            <td style="BORDER-RIGHT: green 1px solid; BORDER-TOP: green 1px solid; BORDER-LEFT: green 1px solid; WIDTH: 10px; BORDER-BOTTOM: green 1px solid; BACKGROUND-COLOR: #FFFF00; border-color: #FFFF00;">
                                &nbsp;</td>
                            <td style="WIDTH: 10px">
                            </td>
                            <td>
                                <dxe:ASPxLabel ID="lblErros" runat="server" 
                                    ClientInstanceName="lblErros" Font-Bold="False" 
                                     
                                    Text="Atenção! Registros inconsistentes. Não foi possível obter o Código da Unidade.">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </FooterRow>
        </Templates>
    </dxwgv:ASPxGridView>
            </dxp:PanelContent>
</PanelCollection>
    </dxcp:ASPxCallbackPanel>
                        </td>       
        </tr>
</table>
</div>
</asp:Content>

