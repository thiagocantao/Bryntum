<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="adm_ImportaExportaZeus.aspx.cs" Inherits="administracao_ImportaExportaZeus" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr style="height:26px">
                <td valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 100%">
                        <tr style="height:26px">
                            <td valign="middle">
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Integração com ZEUS"></asp:Label></td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Desempenho:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                                <dxe:ASPxLabel ID="ASPxLabel40" runat="server" 
                                    Text="Ano:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="right" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                            <td align="left" style="display: none; width: 8px; height: 26px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
                    width: 100%">
                    <tr>
                        <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                            height: 26px" valign="middle">
<table>
                                <tr>
                                    <td>
                                         &nbsp;</td>
                                    </td>
                                    <td style="width: 100px">
                                        <dxe:ASPxButton ID="AspxbuttonGerarArquivo" runat="server" 
                                            OnClick="btnGerarArquivo_Click" Text="Gerar Arquivo" 
                                            ClientInstanceName="AspxbuttonExportar" Visible="False">
                                            <ClientSideEvents Click="function(s, e) {
	ConfirmaGerarArquivo(s,e);
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton></td>
 <td style="width: 6px">
                                         &nbsp;</td>
                                    </td>
                                    <td style="width: 100px">
                                        <dxe:ASPxButton ID="AspxbuttonExportar" runat="server" 
                                            OnClick="btnExportar_Click" Text="Exportar" 
                                            ClientInstanceName="AspxbuttonExportar">
                                            <ClientSideEvents Click="function(s, e) {
	ConfirmaExportacao(s,e);
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton></td>
                                     <td width="10px">
                                         &nbsp;</td>
                                    <td style="width: 100px">
                                        <dxe:ASPxButton ID="AspxbuttonImportar" runat="server" 
                                            OnClick="btnImportar_Click" Text="Importar">
                                            <ClientSideEvents Click="function(s, e) {
	ConfirmaImportacao(s,e);
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                        <td style="width: 6px">
                                         &nbsp;</td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 107px" align="center">
                <br />
                <dxe:ASPxLabel ID="ASPxLabel41" runat="server" Font-Bold="True" 
                    Font-Size="12pt">
                </dxe:ASPxLabel>
                <br />
                <dxe:ASPxMemo ID="ASPxMemo1" runat="server" ClientInstanceName="ASPxMemo1" 
                     Height="404px" 
                    style="margin-left: 0px; margin-top: 0px" Width="1000px">
                    <Border BorderStyle="Dotted" />
                </dxe:ASPxMemo>
                <dxlp:ASPxLoadingPanel ID="pnLoading" runat="server" 
                    ClientInstanceName="pnLoading" 
                    Font-Size="10pt" Height="93px" Modal="True" 
                    Text="Processando os arquivos, aguarde..." Width="326px" 
                    HorizontalAlign="Center" VerticalAlign="Middle">
                </dxlp:ASPxLoadingPanel>
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" 
                    ClientInstanceName="pnCallback" Width="100%"><PanelCollection>
<dxp:PanelContent runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td style="PADDING-LEFT: 10px; WIDTH: 100%">
    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
        AutoGenerateColumns="False" Width="99%"  
        ID="gvDados" Visible="False">
<Columns>
<dxwgv:GridViewDataTextColumn FieldName="CodigoUnidade" Caption="CodigoUnidade" 
        VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="SiglaUnidade" Caption="SiglaUnidade" 
        Visible="False" VisibleIndex="8"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeUnidade" 
        ShowInCustomizationForm="True" Caption="Nome Unidade" Visible="False" 
        VisibleIndex="8"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" Caption="Nome Projeto" 
        VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Nomeacao" Caption="Nome acao" 
        VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Conta Orcamentaria" 
        FieldName="ContaOrcamentaria" ShowInCustomizationForm="True" VisibleIndex="4">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Mes" FieldName="Mes" 
        ShowInCustomizationForm="True" VisibleIndex="5">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Valororcado" FieldName="Valororcado" 
        ShowInCustomizationForm="True" VisibleIndex="6">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Codigoacaoportal" 
        FieldName="Codigoacaoportal" ShowInCustomizationForm="True" VisibleIndex="7">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" showgrouppanel="True"></Settings>
</dxwgv:ASPxGridView>
    <dxwgv:ASPxGridView ID="gvDadosGeraArquivo" runat="server" 
        AutoGenerateColumns="False" ClientInstanceName="gvDadosGeraArquivo" 
         Width="99%" Visible="False">
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="Codigo Projeto" 
                FieldName="CodigoProjeto" ShowInCustomizationForm="True" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Nome Projeto" FieldName="NomeProjeto" 
                ShowInCustomizationForm="True" VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Codigo Ação" FieldName="CodigoAcao" 
                ShowInCustomizationForm="True" VisibleIndex="3">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Nome acao" FieldName="NomeAcao" 
                ShowInCustomizationForm="True" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Fonte Recurso" FieldName="FonteRecurso" 
                ShowInCustomizationForm="True" VisibleIndex="4">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Nome Atividade" 
                FieldName="NomeAtividade" ShowInCustomizationForm="True" VisibleIndex="5">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Quantidade" FieldName="Quantidade" 
                ShowInCustomizationForm="True" VisibleIndex="6">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ValorUnitario" FieldName="ValorUnitario" 
                ShowInCustomizationForm="True" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ValorTotal" FieldName="ValorTotal" 
                ShowInCustomizationForm="True" VisibleIndex="8">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Memoria Calculo" 
                FieldName="MemoriaCalculo" ShowInCustomizationForm="True" VisibleIndex="9">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Conta Orcamentaria" 
                FieldName="ContaOrcamentaria" ShowInCustomizationForm="True" VisibleIndex="10">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Janeiro" FieldName="Janeiro" 
                ShowInCustomizationForm="True" VisibleIndex="11">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Fevereiro" FieldName="Fevereiro" 
                ShowInCustomizationForm="True" VisibleIndex="12">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Marco" FieldName="Marco" 
                ShowInCustomizationForm="True" VisibleIndex="13">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Abril" FieldName="Abril" 
                ShowInCustomizationForm="True" VisibleIndex="14">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Maio" FieldName="Maio" 
                ShowInCustomizationForm="True" VisibleIndex="15">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Junho" FieldName="Junho" 
                ShowInCustomizationForm="True" VisibleIndex="16">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Julho" FieldName="Julho" 
                ShowInCustomizationForm="True" VisibleIndex="17">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Agosto" FieldName="Agosto" 
                ShowInCustomizationForm="True" VisibleIndex="18">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Setembro" FieldName="Setembro" 
                ShowInCustomizationForm="True" VisibleIndex="19">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Outubro" FieldName="Outubro" 
                ShowInCustomizationForm="True" VisibleIndex="20">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Novembro" FieldName="Novembro" 
                ShowInCustomizationForm="True" VisibleIndex="21">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Dezembro" FieldName="Dezembro" 
                ShowInCustomizationForm="True" VisibleIndex="22">
            </dxwgv:GridViewDataTextColumn>
        </Columns>
        <settingspager mode="ShowAllRecords">
        </settingspager>
        <settings showgrouppanel="True" HorizontalScrollBarMode="Visible" 
            VerticalScrollBarMode="Visible" />

<Settings ShowGroupPanel="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"></Settings>
    </dxwgv:ASPxGridView>
 </td></tr></tbody></table>
 <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral" ></dxhf:ASPxHiddenField>
    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcMensagemGravacao" ><ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar" ></dxe:ASPxImage>



 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao" ></dxe:ASPxLabel>



 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 </dxp:PanelContent>
</PanelCollection>

</dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</asp:Content>


