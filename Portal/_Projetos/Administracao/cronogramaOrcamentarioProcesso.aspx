<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cronogramaOrcamentarioProcesso.aspx.cs"
    Inherits="_Projetos_Administracao_cronogramaOrcamentarioProcesso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function incluiLinha(codigoAtividade, codigoProjeto) {
            hfGeral.Set("CodigoAtividade", codigoAtividade);
            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroCronogramaOrcamentarioProcesso.aspx?CO=-1&CA=' + codigoAtividade + "&CP=" + codigoProjeto, 'Nova Conta', 800, 455, funcaoPosModal, null);
        }

        function funcaoPosModal(valor) {
            if (valor == 'S')
                gvDados.PerformCallback('ATL');
        }

        function editaConta(codigoConta, codigoAtividade, codigoProjeto) {
            hfGeral.Set("CodigoAtividade", codigoAtividade);
            window.top.showModal(window.top.pcModal.cp_Path + '_Projetos/Administracao/cadastroCronogramaOrcamentarioProcesso.aspx?CO=' + codigoConta + '&CA=' + codigoAtividade + "&CP=" + codigoProjeto, 'Edição da Conta', 800, 455, funcaoPosModal, null);
        }

        function excluiConta(codigoConta, codigoAtividade) {
            hfGeral.Set("CodigoAtividade", codigoAtividade);

            if (confirm("Deseja excluir a conta selecionada do cronograma orçamentário?"))
                gvDados.PerformCallback(codigoConta + ";" + codigoAtividade);
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
</head>
<body class="body">
    <form id="form1" runat="server">
    <div>
    </div>
    <table cellpadding="0" cellspacing="0" class="headerGrid">
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                    KeyFieldName="CodigoAtividade" Width="100%" ClientInstanceName="gvDados"
                    OnDetailRowExpandedChanged="gvDados_DetailRowExpandedChanged" OnDetailRowGetButtonVisibility="gvDados_DetailRowGetButtonVisibility"
                    OnCustomCallback="gvDados_CustomCallback">
                    <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Msg != null &amp;&amp; s.cp_Msg != '')
	 	mostraDivSalvoPublicado(s.cp_Msg);
}" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption=" " FieldName="CodigoAcao" VisibleIndex="0"
                            Width="45px">
                            <DataItemTemplate>
                                <%# getBotoesAtividade() %>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Ação" FieldName="NumeroAcao" GroupIndex="0"
                            SortIndex="0" SortOrder="Ascending" VisibleIndex="1">
                            <GroupRowTemplate>
                                <%# getDescricaoAcao() %>
                            </GroupRowTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption=" Atividade" FieldName="NomeAtividade" VisibleIndex="2">
                            <DataItemTemplate>
                                <%# getDescricaoAtividade() %>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Valor" FieldName="Valor" VisibleIndex="3"
                            Width="130px">
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="PossuiContas" Visible="False" VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AutoExpandAllGroups="True" />
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings VerticalScrollBarMode="Visible" ShowColumnHeaders="False" ShowTitlePanel="True" />
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <Styles>
                        <GroupRow BackColor="#EBEBEB">
                        </GroupRow>
                        <DetailRow HorizontalAlign="Left">
                        </DetailRow>
                        <DetailCell HorizontalAlign="Left">
                            <Paddings Padding="0px" PaddingBottom="5px" />
                        </DetailCell>
                        <TitlePanel HorizontalAlign="Left" Font-Bold="True" >
                        </TitlePanel>
                    </Styles>
                    <Templates>
                        <DetailRow>
                            <dxwgv:ASPxGridView ID="gvContas" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvContas"
                                 KeyFieldName="SeqPlanoContas" OnCustomCallback="gvContas_CustomCallback"
                                OnHtmlDataCellPrepared="gvContas_HtmlDataCellPrepared" Width="100%">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption=" " FieldName="SeqPlanoContas" FixedStyle="Left"
                                        VisibleIndex="0" Width="70px">
                                        <DataItemTemplate>
                                            <%# getBotoesContas() %>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Conta Orçamentária" FieldName="CONTA_DES"
                                        FixedStyle="Left" VisibleIndex="1" Width="280px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Quantidade" FieldName="Quantidade" VisibleIndex="2"
                                        Width="90px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Valor Unitário" FieldName="ValorUnitario"
                                        VisibleIndex="3" Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Memória de Cálculo" FieldName="MemoriaCalculo"
                                        VisibleIndex="4" Width="200px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Janeiro" FieldName="Plan01" VisibleIndex="5"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Fevereiro" FieldName="Plan02" VisibleIndex="6"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Março" FieldName="Plan03" VisibleIndex="7"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Abril" FieldName="Plan04" VisibleIndex="8"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Maio" FieldName="Plan05" VisibleIndex="9"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Junho" FieldName="Plan06" VisibleIndex="10"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Julho" FieldName="Plan07" VisibleIndex="11"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Agosto" FieldName="Plan08" VisibleIndex="12"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Setembro" FieldName="Plan09" VisibleIndex="13"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Outubro" FieldName="Plan10" VisibleIndex="14"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Novembro" FieldName="Plan11" VisibleIndex="15"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Dezembro" FieldName="Plan12" VisibleIndex="16"
                                        Width="110px">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="150" />
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
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText=""
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
        ShowHeader="False" Width="420px"  ID="pcUsuarioIncluido">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="" align="center">
                            </td>
                            <td style="width: 70px" align="center" rowspan="3">
                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                    ClientInstanceName="imgSalvar" ID="imgSalvar">
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                    ID="lblAcaoGravacao" EncodeHtml="False">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcToolTip" HeaderText="Memória de Cálculo"
        PopupHorizontalAlign="Center" PopupVerticalAlign="Below"
        ID="pcToolTip" BackColor="#FFFFCC" CloseAction="None" MaxHeight="300px"
        MaxWidth="700px" ScrollBars="Auto" Width="650px" PopupAction="None" PopupHorizontalOffset="5"
        PopupVerticalOffset="5" RenderIFrameForPopupElements="False" ShowCloseButton="False">
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
