<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoricoAnalises.aspx.cs"
    Inherits="_VisaoMaster_HistoricoAnalises" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function gvPeriodo_FocusedRowChanged(s, e) {
            var rowIndex = s.GetFocusedRowIndex();
            if (-1 < rowIndex)
                s.GetRowValues(rowIndex, ';', preencheDetalhePeriodo);
        }

        function preencheDetalhePeriodo(valores) {
            if (null != valores) {
                var CodigoProjeto = valores[0];
                var CodigoIndicador = valores[1];
                var Ano = valores[2];
                var Mes = valores[3];
                var periodo = valores[4];
                var MetaMes = valores[5];
                var ResultadoMes = valores[6];
                var Desempenho = valores[7];

                hfGeral.Set('Ano', Ano);
                hfGeral.Set('Mes', Mes);
            }
        }

        function salvaAnalise() {
            callbackSalvar.PerformCallback();
        }

        function setMaxLength(textAreaElement, length) {
            textAreaElement.maxlength = length;
            ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
            ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
        }

        function onKeyUpOrChange(evt) {
            processTextAreaText(ASPxClientUtils.GetEventSource(evt));
        }

        function processTextAreaText(textAreaElement) {
            var maxLength = textAreaElement.maxlength;
            var text = textAreaElement.value;
            var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
            if (maxLength != 0 && text.length > maxLength)
                textAreaElement.value = text.substr(0, maxLength);
            else {
                if (textAreaElement.name.indexOf("mmAnalise") >= 0)
                    lblContador1.SetText(text.length);
                else
                    lblContador2.SetText(text.length);
            }
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function onInit_memo(s, e) {
            try { return setMaxLength(s.GetInputElement(), 2000); }
            catch (e) { }
        }

        //----------- Mensagem modificação con sucesso..!!!
        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 5px;
        }
        .style2
        {
            width: 10px;
        }
        .style3
        {
            height: 10px;
            width: 10px;
        }
        .style4
        {
            height: 10px;
        }
        .style5
        {
            width: 50%;
        }
        .style6
        {
            height: 10px;
            width: 50%;
        }
        .style7
        {
            width: 20px;
        }
        .style8
        {
            width: 230px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                            <tr>
                                <td class="style7">
                                    &nbsp;
                                </td>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                        Text="Indicador:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td class="style8">
                                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                        Text="Sítio:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    <dxe:ASPxImage ID="imgStatusIndicador" runat="server">
                                    </dxe:ASPxImage>
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txtIndicador" runat="server" ClientEnabled="False"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td class="style8">
                                    <dxe:ASPxTextBox ID="txtSitio" runat="server" ClientEnabled="False"
                                        Width="100%">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvPeriodo" KeyFieldName="CodigoIndicador"
                            AutoGenerateColumns="False" Width="100%" 
                            ID="gvPeriodo">
                            <ClientSideEvents FocusedRowChanged="function(s, e) {
	e.processOnServer = false;
	pnCallbackDetalhe.PerformCallback();
}"></ClientSideEvents>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="Periodo" Name="Periodo" Caption="Per&#237;odo"
                                    VisibleIndex="0">
                                    <HeaderStyle Wrap="True"></HeaderStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="ResultadoMes" Name="ResultadoMes" Caption="Resultado"
                                    VisibleIndex="1">
                                    <PropertiesTextEdit DisplayFormatString="{0:n2}" EncodeHtml="False" NullDisplayText="-"
                                        NullText="-">
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Desempenho" Name="Desempenho" Width="100px"
                                    Caption="Status" VisibleIndex="2">
                                    <DataItemTemplate>
                                        <img alt='' src="../imagens/<%# Eval("Desempenho") %>.gif" />
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True">
                            </SettingsBehavior>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings ShowFooter="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="130">
                            </Settings>
                            <SettingsText Title="An&#225;lises da Meta Selecionada"></SettingsText>
                            
                            <Styles>
                                <Header SortingImageSpacing="5px" ImageSpacing="5px">
                                </Header>
                            </Styles>
                            <StylesEditors>
                                <ProgressBar Height="25px">
                                </ProgressBar>
                            </StylesEditors>
                            <Templates>
                                <FooterRow>
                                    <table cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <img id="IMG2" onclick="return IMG1_onclick()" src="../imagens/verdeMenor.gif" alt=""/>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblVerde0" runat="server" EnableViewState="False"
                                                        Font-Size="7pt" Text="Satisfatório"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <img src="../imagens/amareloMenor.gif" alt="" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAmarelo0" runat="server" EnableViewState="False"
                                                        Font-Size="7pt" Text="Atenção"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <img src="../imagens/AzulMenor.gif" alt=""/>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" EnableViewState="False"
                                                        Font-Size="7pt" Text="Acima da Meta"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 25px">
                                                    <img src="../imagens/vermelhoMenor.gif" alt="" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblVermelho0" runat="server" EnableViewState="False"
                                                        Font-Size="7pt" Text="Crítico"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 25px">
                                                    <img src="../imagens/brancoMenor.gif" alt=""/>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" EnableViewState="False"
                                                        Font-Size="7pt" Text="Sem Informação"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </FooterRow>
                            </Templates>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style1">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="height:250px">
                        <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackDetalhe" Width="100%" ID="pnCallbackDetalhe">
                            <Styles>
                            <LoadingPanel HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></LoadingPanel>
                            </Styles>
                            <PanelCollection>
                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" style="height:250px">
                                        <tbody>
                                            <tr>
                                                <td align="left" class="style5">
                                                    <dxe:ASPxLabel runat="server" Text="Análises do Período Selecionado:"
                                                        ID="ASPxLabel1">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="left" class="style2">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                                        Text="Análises do Período Atual:">
                                                    </dxe:ASPxLabel>
                                                    <dxe:ASPxLabel ID="lblContador1" runat="server" ClientInstanceName="lblContador1"
                                                         ForeColor="Silver" Text="0">
                                                    </dxe:ASPxLabel>
                                                    <dxe:ASPxLabel ID="lblDe250Ob" runat="server" ClientInstanceName="lblDe250Ob"
                                                        ForeColor="Silver" Text=" de 2000">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style5">
                                                    <dxe:ASPxMemo runat="server" Width="100%" ClientInstanceName="mmAnalise"
                                                        ID="mmAnalise" Rows="3" ClientEnabled="False">
                                                        <ClientSideEvents KeyUp="function(s, e) {
	//limitaASPxMemo(s, 2000);
}"></ClientSideEvents>
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="MKE">
                                                            <RequiredField ErrorText="Campo Obrigat&#243;rio!"></RequiredField>
                                                        </ValidationSettings>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                </td>
                                                <td class="style2">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <dxe:ASPxMemo ID="mmAnaliseAtual" runat="server" ClientInstanceName="mmAnaliseAtual"
                                                         Rows="3" Width="100%">
                                                        <ClientSideEvents Init="function(s, e) {
	lblContador1.SetText(s.GetText().length);
}" KeyUp="function(s, e) {
	onInit_memo(s, e);
}" Validation="function(s, e) {
	if(e.value == null)
	{
		e.isValid = false;
		e.errorText = 'Campo Obrigatório!';
	}
	else
		e.isValid = true;
}" />
                                                        <ClientSideEvents KeyUp="function(s, e) {
	onInit_memo(s, e);
}" Validation="function(s, e) {
	if(e.value == null)
	{
		e.isValid = false;
		e.errorText = &#39;Campo Obrigat&#243;rio!&#39;;
	}
	else
		e.isValid = true;
}" Init="function(s, e) {
	lblContador1.SetText(s.GetText().length);
}"></ClientSideEvents>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                                                            <RequiredField ErrorText="Campo Obrigatório!" />
                                                            <RequiredField ErrorText="Campo Obrigat&#243;rio!"></RequiredField>
                                                        </ValidationSettings>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style5">
                                                    <dxe:ASPxLabel runat="server" Text="Recomendações do Período Selecionado:"
                                                        ID="ASPxLabel3">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="left" class="style2">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                                        Text="Recomendações do Período Atual:">
                                                    </dxe:ASPxLabel>
                                                    <dxe:ASPxLabel ID="lblContador2" runat="server" ClientInstanceName="lblContador2"
                                                         ForeColor="Silver" Text="0">
                                                    </dxe:ASPxLabel>
                                                    <dxe:ASPxLabel ID="lblDe2" runat="server" ClientInstanceName="lblDe250Ob"
                                                        ForeColor="Silver" Text=" de 2000">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style5">
                                                    <dxe:ASPxMemo runat="server" Width="100%" ClientInstanceName="mmRecomendacoes"
                                                        ID="mmRecomendacoes" Rows="3" ClientEnabled="False">
                                                        <ClientSideEvents KeyUp="function(s, e) {
	//limitaASPxMemo(s, 2000);
}"></ClientSideEvents>
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                        </ValidationSettings>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                </td>
                                                <td class="style2">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <dxe:ASPxMemo ID="mmRecomendacoesAtuais" runat="server" ClientInstanceName="mmRecomendacoesAtuais"
                                                         Rows="3" Width="100%">
                                                        <ClientSideEvents Init="function(s, e) {
	lblContador2.SetText(s.GetText().length);
}" KeyUp="function(s, e) {
	onInit_memo(s, e);
}" />
                                                        <ClientSideEvents KeyUp="function(s, e) {
	onInit_memo(s, e);
}" Init="function(s, e) {
	lblContador2.SetText(s.GetText().length);
}"></ClientSideEvents>
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                        </ValidationSettings>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="style5">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left">
                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="padding-right: 2px; padding-bottom: 2px;" align="right">
                                                                                    <dxe:ASPxLabel runat="server" Text="Inclus&#227;o:" ClientInstanceName="lblInclusao"
                                                                                        Font-Bold="True"  ForeColor="DimGray" ID="lblCaptionInclusao">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td align="left" style="padding-bottom: 2px">
                                                                                    <dxe:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblDataInclusao"
                                                                                         ForeColor="DimGray" ID="lblDataInclusao">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px;">
                                                                                </td>
                                                                                <td style="padding-right: 2px; padding-bottom: 2px;" align="right">
                                                                                    <dxe:ASPxLabel runat="server" Text="Incluido por:" ClientInstanceName="lblIncluidoPor"
                                                                                        Font-Bold="True"  ForeColor="DimGray" ID="lblCaptionIncluidoPor">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td align="left" style="padding-bottom: 2px">
                                                                                    <dxe:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblUsuarioInclusao"
                                                                                         ForeColor="DimGray" ID="lblUsuarioInclusao">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-right: 2px">
                                                                                    <dxe:ASPxLabel runat="server" Text="Última Alteração:" ClientInstanceName="lblUltimaAlteracao"
                                                                                        Font-Bold="True"  ForeColor="DimGray" ID="lblCaptionUltimaAlteracao">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <dxe:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblDataAlteracao"
                                                                                         ForeColor="DimGray" ID="lblDataAlteracao">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px;">
                                                                                </td>
                                                                                <td style="padding-right: 2px">
                                                                                    <dxe:ASPxLabel runat="server" Text="Alterado por:" ClientInstanceName="lblAlteradoPor"
                                                                                        Font-Bold="True"  ForeColor="DimGray" ID="lblCaptionAlteradoPor">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <dxe:ASPxLabel runat="server" Text="ASPxLabel" ClientInstanceName="lblUsuarioAlteracao"
                                                                                         ForeColor="DimGray" ID="lblUsuarioAlteracao">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td align="right" class="style2">
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                    <table>
                                                        <tbody>
                                                            <tr style="padding-bottom: 2px">
                                                                <td style="padding-right: 2px; padding-bottom: 2px;" align="right">
                                                                    <dxe:ASPxLabel ID="lblCaptionInclusaoAtual" runat="server" ClientInstanceName="lblCaptionInclusaoAtual"
                                                                        Font-Bold="True"  ForeColor="DimGray" Text="Inclusão:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="left" style="padding-bottom: 2px">
                                                                    <dxe:ASPxLabel ID="lblDataInclusaoAtual" runat="server" ClientInstanceName="lblDataInclusaoAtual"
                                                                         ForeColor="DimGray" Text=" ">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 10px;">
                                                                </td>
                                                                <td style="padding-right: 2px; padding-bottom: 2px;" align="right">
                                                                    <dxe:ASPxLabel ID="lblCaptionIncluidoPorAtual" runat="server" ClientInstanceName="lblCaptionIncluidoPorAtual"
                                                                        Font-Bold="True"  ForeColor="DimGray" Text="Incluido por:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="left" style="padding-bottom: 2px">
                                                                    <dxe:ASPxLabel ID="lblUsuarioInclusaoAtual" runat="server" ClientInstanceName="lblUsuarioInclusaoAtual"
                                                                         ForeColor="DimGray" Text=" ">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-right: 2px">
                                                                    <dxe:ASPxLabel ID="lblCaptionUltimaAlteracaoAtual" runat="server" ClientInstanceName="lblCaptionUltimaAlteracaoAtual"
                                                                        Font-Bold="True"  ForeColor="DimGray" Text="Ultima Alteração:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel ID="lblDataAlteracaoAtual" runat="server" ClientInstanceName="lblDataAlteracaoAtual"
                                                                         ForeColor="DimGray" Text=" ">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 10px;">
                                                                </td>
                                                                <td style="padding-right: 2px">
                                                                    <dxe:ASPxLabel ID="lblCaptionAlteradoPorAtual" runat="server" ClientInstanceName="lblCaptionAlteradoPorAtual"
                                                                        Font-Bold="True"  ForeColor="DimGray" Text="Alterado por:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel ID="lblUsuarioAlteracaoAtual" runat="server" ClientInstanceName="lblUsuarioAlteracaoAtual"
                                                                         ForeColor="DimGray" Text=" ">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style4">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td style="width: 100px;padding-right:5px" valign="top">
                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE"
                                            Width="100%"  ID="btnSalvar" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
	if(ASPxClientEdit.ValidateGroup('MKE', true))
		salvaAnalise();
}"></ClientSideEvents>
                                        </dxe:ASPxButton>
                                    </td>
                                    
                                    <td style="width: 100px" valign="top">
                                        <dxe:ASPxButton runat="server" CommandArgument="btnCancelar" Text="Fechar" Width="100%"
                                             ID="btnCancelar">
                                            <ClientSideEvents Click="function(s, e) {
	window.parent.fechaModal();
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
    </div>
    <dxhf:ASPxHiddenField ID="hfDadosSessao" runat="server" ClientInstanceName="hfDadosSessao">
    </dxhf:ASPxHiddenField>
    <dxcb:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar"
        OnCallback="callbackSalvar_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
		if (s.cp_Status == '1') {
            mostraDivSalvoPublicado(traducao.HistoricoAnalises_an_lise_salva_com_sucesso_);     
			pnCallbackDetalhe.PerformCallback();
			//gvPeriodo.PerformCallback();       
        }
        else
            mostraDivSalvoPublicado(traducao.HistoricoAnalises_erro_ao_salvar_an_lise_);		
}" />
    </dxcb:ASPxCallback>
    <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" ShowCloseButton="False"
        ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
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
                                    ID="lblAcaoGravacao">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
