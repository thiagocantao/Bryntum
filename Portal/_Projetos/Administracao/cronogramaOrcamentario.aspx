<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cronogramaOrcamentario.aspx.cs" Inherits="_Projetos_Administracao_cronogramaOrcamentario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js">
    </script>   
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function ConfirmaGerarArquivo(s, e) {
            if (confirm("ATENÇÃO!!!\n\nConfirma geração do arquivo ? \n")) {
                hfGeral.Set("podeGerarArquivo", "S");
                pnLoading.Show();
            }
            else
                hfGeral.Set("podeGerarArquivo", "N");
        }    

        function incluiLinha(codigoAtividade, codigoProjeto) {
            hfGeral.Set("CodigoAtividade", codigoAtividade);
            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroCronogramaOrcamentario.aspx?CO=-1&CA=' + codigoAtividade + "&CP=" + codigoProjeto, 'Nova Conta', 900, 500, funcaoPosModal, null);
        }

        function funcaoPosModal(valor) {
            if (valor == 'S')
                gvDados.PerformCallback('ATL');
        }

        function editaConta(codigoConta, codigoAtividade, codigoProjeto) {
            hfGeral.Set("CodigoAtividade", codigoAtividade);
            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroCronogramaOrcamentario.aspx?CO=' + codigoConta + '&CA=' + codigoAtividade + "&CP=" + codigoProjeto, 'Edição da Conta', 900, 500, funcaoPosModal, null);
        }

        function excluiConta(codigoConta, codigoAtividade) {
            hfGeral.Set("CodigoAtividade", codigoAtividade);

            if (confirm("Deseja excluir a conta selecionada do cronograma orçamentário?")) {
                gvDados.PerformCallback(codigoConta + ";" + codigoAtividade);
            }
        }

        //----------- Mensagem modificação con sucesso..!!!
        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);
        }

        function getToolTip(valor) {
            pcToolTip.SetContentHTML(valor);
            pcToolTip.Show();
        }

        function escondeToolTip() {
            pcToolTip.Hide();
        }
        </script>
    <style type="text/css">
        .style1
        {
            width: 789px;
        }
        .style2
        {
            height: 244px;
        }
    </style>
</head>
<body class="body">
    <form id="form1" runat="server">
    <div>
    
    </div>
    
        <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
        <tr style="text-align: right">

                                    <td class="style1">
                                        &nbsp;</td>
                                    <td style="width: 150px;">
                                        <dxe:ASPxButton ID="AspxbuttonGerarArquivoCSV" runat="server" 
                                            
                                            OnClick="btnGerarArquivocsv_Click" Text="Gerar Arquivo .CSV" 
                                            ClientInstanceName="AspxbuttonGerarArquivoCSV" Width="150px">
                                            <ClientSideEvents Click="function(s, e) {
	ConfirmaGerarArquivo(s,e);
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton></td>     
                                    <td style="width: 150px;">
                                        <dxe:ASPxButton ID="AspxbuttonGerarArquivoXLS" runat="server" 
                                            
                                            OnClick="btnGerarArquivoxls_Click" Text="Gerar Arquivo .XLS" 
                                            ClientInstanceName="AspxbuttonGerarArquivoXLS" Width="151px">
                                            <ClientSideEvents Click="function(s, e) {
	ConfirmaGerarArquivo(s,e);
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton></td>
     
        </tr>
        <tr><td>
                                         &nbsp;</td>
                                    </td></tr>
        </table>
 <table cellpadding="0" cellspacing="0" class="headerGrid">
            <tr>
                <td class="style2">
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" 
                        AutoGenerateColumns="False"  
                        KeyFieldName="CodigoAtividade" Width="100%" ClientInstanceName="gvDados" 
                        ondetailrowexpandedchanged="gvDados_DetailRowExpandedChanged" 
                        ondetailrowgetbuttonvisibility="gvDados_DetailRowGetButtonVisibility" 
                        oncustomcallback="gvDados_CustomCallback">
                        <ClientSideEvents EndCallback="function(s, e) {	
	if(s.cp_Msg != null &amp;&amp; s.cp_Msg != '')
	 	mostraDivSalvoPublicado(s.cp_Msg);

	if(window.parent.callbackCronogramaOrcamentario)
		window.parent.callbackCronogramaOrcamentario.PerformCallback();

}" />
<ClientSideEvents EndCallback="function(s, e) {
	if(window.parent.callbackCronogramaOrcamentario)
		window.parent.callbackCronogramaOrcamentario.PerformCallback();

	if(s.cp_Msg != null &amp;&amp; s.cp_Msg != '')
	 	mostraDivSalvoPublicado(s.cp_Msg);
}"></ClientSideEvents>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption=" " FieldName="CodigoAcao" 
                                VisibleIndex="0" Width="55px">
                                <DataItemTemplate>
                                    <%# getBotoesAtividade() %>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Ação" FieldName="NumeroAcao" 
                                GroupIndex="0" SortIndex="0" SortOrder="Ascending" VisibleIndex="1" 
                                Width="280px">
                                <GroupRowTemplate>
                                    <%# getDescricaoAcao() %>
                                </GroupRowTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption=" Atividade" FieldName="NomeAtividade" 
                                VisibleIndex="2">
                                <DataItemTemplate>
                                     <%# getDescricaoAtividade() %>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Valor" FieldName="Valor" 
                                VisibleIndex="3" Width="130px">
                                <PropertiesTextEdit DisplayFormatString="n2">
                                </PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="PossuiContas" Visible="False" 
                                VisibleIndex="5">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AutoExpandAllGroups="True" />

<SettingsBehavior AutoExpandAllGroups="True"></SettingsBehavior>

                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" ShowColumnHeaders="False" 
                            ShowTitlePanel="True" VerticalScrollableHeight="170" />
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />

<Settings ShowTitlePanel="True" ShowColumnHeaders="False" VerticalScrollBarMode="Visible"></Settings>

<SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>

                        <Styles>
                            <GroupRow BackColor="#EBEBEB">
                            </GroupRow>
                            <DetailRow HorizontalAlign="Left">
                            </DetailRow>
                            <DetailCell HorizontalAlign="Left">
                                <Paddings Padding="0px" PaddingBottom="5px" />
<Paddings Padding="0px" PaddingBottom="5px"></Paddings>
                            </DetailCell>
                            <TitlePanel HorizontalAlign="Left" Font-Bold="True" 
                               >
                            </TitlePanel>
                        </Styles>
                        <Templates>
                            <DetailRow>
                                <dxwgv:ASPxGridView ID="gvContas" runat="server" AutoGenerateColumns="False" 
                                    ClientInstanceName="gvContas"  
                                    KeyFieldName="SeqPlanoContas" 
                                    onhtmldatacellprepared="gvContas_HtmlDataCellPrepared" Width="100%">
                                    <ClientSideEvents EndCallback="function(s, e) {

}" />
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption=" " FieldName="SeqPlanoContas" 
                                            FixedStyle="Left" VisibleIndex="0" Width="70px">
                                            <DataItemTemplate>
                                                <%# getBotoesContas() %>
                                            </DataItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Conta Orçamentária" 
                                            FieldName="CONTA_DES" FixedStyle="Left" VisibleIndex="1" Width="280px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Quantidade" FieldName="Quantidade" 
                                            VisibleIndex="2" Width="90px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Valor Unitário" 
                                            FieldName="ValorUnitario" VisibleIndex="3" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Memória de Cálculo" 
                                            FieldName="MemoriaCalculo" VisibleIndex="4" Width="200px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Janeiro" FieldName="Plan01" 
                                            VisibleIndex="5" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Fevereiro" FieldName="Plan02" 
                                            VisibleIndex="6" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Março" FieldName="Plan03" 
                                            VisibleIndex="7" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Abril" FieldName="Plan04" 
                                            VisibleIndex="8" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Maio" FieldName="Plan05" 
                                            VisibleIndex="9" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Junho" FieldName="Plan06" 
                                            VisibleIndex="10" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Julho" FieldName="Plan07" 
                                            VisibleIndex="11" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Agosto" FieldName="Plan08" 
                                            VisibleIndex="12" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Setembro" FieldName="Plan09" 
                                            VisibleIndex="13" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Outubro" FieldName="Plan10" 
                                            VisibleIndex="14" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Novembro" FieldName="Plan11" 
                                            VisibleIndex="15" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Dezembro" FieldName="Plan12" 
                                            VisibleIndex="16" Width="110px">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                            </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowSort="False" />
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" 
                                        VerticalScrollableHeight="150" />
                                </dxwgv:ASPxGridView>
                            </DetailRow>
                            <TitlePanel>
                                <%# getTotalProjeto() %>
                            </TitlePanel>
                        </Templates>
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>

    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDadosGeraArquivo" AutoGenerateColumns="False" Width="99%"  ID="gvDadosGeraArquivo" Visible="False"><Columns>
<dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" Caption="Codigo Projeto" 
            VisibleIndex="0"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" Caption="Nome Projeto" 
            VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoAcao" Caption="Codigo A&#231;&#227;o" 
            VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoPai" Caption="Codigo Acao Superior" 
            VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Numero" Caption="Numero " VisibleIndex="4"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeAcao" Caption="Nome acao" VisibleIndex="5"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="FonteRecurso" Caption="Fonte Recurso" 
            VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="SeqPlanoContas" Caption="SeqPlanoContas" 
            VisibleIndex="8"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeAtividade" Caption="Nome Atividade" 
            VisibleIndex="6"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Quantidade" Caption="Quantidade" 
            VisibleIndex="9"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="ValorUnitario" Caption="ValorUnitario" 
            VisibleIndex="10"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="ValorTotal" Caption="ValorTotal" 
            VisibleIndex="11"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="MemoriaCalculo" Caption="Memoria Calculo" 
            VisibleIndex="12"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="ContaOrcamentaria" 
            Caption="Conta Orcamentaria" VisibleIndex="26"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Janeiro" Caption="Janeiro" VisibleIndex="13"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Fevereiro" Caption="Fevereiro" 
            VisibleIndex="14"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Marco" Caption="Marco" VisibleIndex="15"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Abril" Caption="Abril" VisibleIndex="16"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Maio" Caption="Maio" VisibleIndex="17"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Junho" Caption="Junho" VisibleIndex="18"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Julho" Caption="Julho" VisibleIndex="19"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Agosto" Caption="Agosto" VisibleIndex="20"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Setembro" Caption="Setembro" VisibleIndex="21"></dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Caption="Outubro" FieldName="Outubro" 
            VisibleIndex="22">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Caption="Novembro" FieldName="Novembro" 
            VisibleIndex="23">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Caption="Dezembro" FieldName="Dezembro" 
            VisibleIndex="24">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Caption="CONTA_DES" FieldName="CONTA_DES" 
            VisibleIndex="25">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Caption="CONTA_COD" FieldName="CONTA_COD" 
            VisibleIndex="27">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Caption="acaoSup" FieldName="acaoSup" 
            VisibleIndex="28">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Caption="NumeroAcao" FieldName="NumeroAcao" 
            VisibleIndex="29">
        </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings ShowGroupPanel="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"></Settings>
</dxwgv:ASPxGridView>

                </td>
            </tr>
        </table>
    
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    
                <dxlp:ASPxLoadingPanel ID="pnLoading" runat="server" 
                    ClientInstanceName="pnLoading" 
                    Font-Size="10pt" Height="93px" Modal="True" 
                    Text="Processando os arquivos, aguarde..." Width="326px" 
                    HorizontalAlign="Center" VerticalAlign="Middle">
                </dxlp:ASPxLoadingPanel>
    
        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" 
            HeaderText="" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" 
            Width="420px"  
        ID="pcUsuarioIncluido">
            <ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table cellSpacing="0" cellPadding="0" 
        width="100%" border="0"><tbody><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
 </td></tr><tr><td  style="HEIGHT: 10px"></td></tr><tr><td align="center">
            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" 
                 ID="lblAcaoGravacao" EncodeHtml="False"></dxe:ASPxLabel></td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcToolTip" 
            HeaderText="Memória de Cálculo" PopupHorizontalAlign="Center" 
            PopupVerticalAlign="Below"  ID="pcToolTip" 
        BackColor="#FFFFCC" CloseAction="None" MaxHeight="300px" MaxWidth="700px" 
        ScrollBars="Auto" Width="650px" PopupAction="None" 
        PopupHorizontalOffset="5" PopupVerticalOffset="5" 
        RenderIFrameForPopupElements="False" ShowCloseButton="False" 
        Height="200px">
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
            <ContentCollection>
<dxpc:PopupControlContentControl runat="server">
                </dxpc:PopupControlContentControl>
</ContentCollection>
            <Border BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
</dxpc:ASPxPopupControl>

    </form>
</body>
</html>
