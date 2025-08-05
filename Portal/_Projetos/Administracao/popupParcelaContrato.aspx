<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupParcelaContrato.aspx.cs" Inherits="_Projetos_Administracao_popupParcelaContrato" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .text-btn {
            text-transform: capitalize !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxcp:ASPxPageControl ID="tabControl" runat="server" ActiveTabIndex="0" ClientInstanceName="tabControl" Width="100%">
                <TabPages>
                    <dxtv:TabPage Text="Dados Parcela">
                        <ContentCollection>
                            <dxtv:ContentControl runat="server">
                                <div id="divDadosParcela">
                                    <table cellpadding="0" cellspacing="0" id="tabelaContainerTabParcela" class="auto-style1">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" class="auto-style1">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="auto-style1">
                                                                <tr>
                                                                    <td style="width: 300px;">
                                                                        <dxcp:ASPxLabel ID="ASPxLabel12" runat="server" Text="Fornecedor:">
                                                                        </dxcp:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 190px">
                                                                        <dxcp:ASPxLabel ID="ASPxLabel11" runat="server" Text="Contrato:">
                                                                        </dxcp:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 120px">
                                                                        <dxcp:ASPxLabel ID="ASPxLabel13" runat="server" Text="Término:">
                                                                        </dxcp:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxcp:ASPxLabel ID="ASPxLabel14" runat="server" Text="Valor do Contrato:">
                                                                        </dxcp:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxcp:ASPxLabel ID="ASPxLabel15" runat="server" Text="Saldo:">
                                                                        </dxcp:ASPxLabel>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-right: 5px;">
                                                                        <dxcp:ASPxTextBox ID="txtFornecedor" runat="server" Width="100%" ClientInstanceName="txtFornecedor" MaxLength="20" ReadOnly="True">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxcp:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dxcp:ASPxTextBox ID="txtNumeroContrato" runat="server" Width="100%" ClientInstanceName="txtNumeroContrato" MaxLength="20" ReadOnly="True">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxcp:ASPxTextBox>
                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dxcp:ASPxDateEdit ID="dtTerminoContrato" runat="server" ClientInstanceName="dtTerminoContrato" Width="100%" ReadOnly="True">
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                        </dxcp:ASPxDateEdit>
                                                                    </td>
                                                                    <td style="padding-right: 5px;">
                                                                        <dxcp:ASPxSpinEdit ID="spValorContrato" runat="server" ClientInstanceName="spValorContrato" Number="0" Width="100%" DisplayFormatString="{0:c2}" DecimalPlaces="2" MaxLength="18" ReadOnly="True">
                                                                            <SpinButtons ClientVisible="False">
                                                                            </SpinButtons>
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                            <DisabledStyle BackColor="Gainsboro" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxcp:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxtv:ASPxSpinEdit ID="spSaldoContrato" runat="server" ClientInstanceName="spSaldoContrato" DecimalPlaces="2" DisplayFormatString="{0:c2}" MaxLength="18" Number="0" ReadOnly="True" Width="100%">
                                                                            <SpinButtons ClientVisible="False">
                                                                            </SpinButtons>
                                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </ReadOnlyStyle>
                                                                            <DisabledStyle BackColor="Gainsboro" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxtv:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" class="auto-style1">
                                                    <tr>
                                                        <td style="width: 33%">
                                                            <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Nº Aditivo:">
                                                            </dxcp:ASPxLabel>

                                                        </td>
                                                        <td style="width: 33%">
                                                            <dxcp:ASPxLabel ID="ASPxLabel2" runat="server" Text="Nº Parcela:">
                                                            </dxcp:ASPxLabel>
                                                        </td>
                                                        <td style="width: 33%">
                                                            <dxcp:ASPxLabel ID="labelPrevistoParcelaContrato" runat="server" Text="Valor Previsto:" ClientInstanceName="labelPrevistoParcelaContrato">
                                                            </dxcp:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" class="auto-style1">
                                                    <tr>
                                                        <td style="padding-right: 5px;">
                                                            <dxcp:ASPxSpinEdit ID="spNumeroAditivo" runat="server" ClientInstanceName="spNumeroAditivo" Number="0" Width="100%" MaxLength="5">
                                                                <SpinButtons ClientVisible="False">
                                                                </SpinButtons>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxcp:ASPxSpinEdit>
                                                        </td>
                                                        <td style="padding-right: 5px;">
                                                            <dxcp:ASPxSpinEdit ID="spNumeroParcela" runat="server" ClientInstanceName="spNumeroParcela" Number="0" Width="100%" MaxLength="5" NumberType="Integer">
                                                                <SpinButtons ClientVisible="False">
                                                                </SpinButtons>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxcp:ASPxSpinEdit>
                                                        </td>
                                                        <td>
                                                            <dxcp:ASPxSpinEdit ID="spValorPrevisto" runat="server" ClientInstanceName="spValorPrevisto" Number="0" Width="100%" DisplayFormatString="{0:c2}" DecimalPlaces="2" MaxLength="18">
                                                                <SpinButtons ClientVisible="False">
                                                                </SpinButtons>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
linkIniciativasAssociadasParcela.SetEnabled(false);
cbAvisos.PerformCallback();
}" />
                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </ReadOnlyStyle>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxcp:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" style="width: 960px; padding-top: 5px">
                                                    <tr>
                                                        <td style="width: 15%;">
                                                            <dxcp:ASPxLabel ID="labelVencimentoParcelaContrato" runat="server" Text="Data de Vencimento:" ClientInstanceName="labelVencimentoParcelaContrato">
                                                            </dxcp:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%;">
                                                            <dxcp:ASPxLabel ID="ASPxLabel5" runat="server" Text="Valor Pago:">
                                                            </dxcp:ASPxLabel>
                                                        </td>
                                                        <td style="width: 37%">
                                                            <dxcp:ASPxLabel ID="ASPxLabel6" runat="server" Text="Data de Pagamento:">
                                                            </dxcp:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td style="width: 33%; padding-right: 5px">
                                                            <dxcp:ASPxDateEdit ID="dtVencimento" runat="server" ClientInstanceName="dtVencimento" Width="100%">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
     if(s.cp_atualizaRealizadoConformePrevisto == &quot;S&quot;)
     {
               dtPagamento.SetValue(dtVencimento.GetValue());
               spValorPago.SetValue(spValorPrevisto.GetValue());
      }
      cbAvisos.PerformCallback();
}" />
                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </ReadOnlyStyle>
                                                            </dxcp:ASPxDateEdit>
                                                        </td>
                                                        <td style="width: 33%; padding-right: 5px">
                                                            <dxcp:ASPxSpinEdit ID="spValorPago" runat="server" ClientInstanceName="spValorPago" Number="0" Width="100%" DisplayFormatString="{0:c2}" DecimalPlaces="2">
                                                                <SpinButtons ClientVisible="False">
                                                                </SpinButtons>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
	cbAvisos.PerformCallback();
}" />
                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </ReadOnlyStyle>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxcp:ASPxSpinEdit>
                                                        </td>
                                                        <td style="width: 33%">
                                                            <dxcp:ASPxDateEdit ID="dtPagamento" runat="server" ClientInstanceName="dtPagamento" Width="100%">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
	cbAvisos.PerformCallback();
}" />
                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </ReadOnlyStyle>
                                                            </dxcp:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" class="auto-style1">
                                                    <tr>
                                                        <td>
                                                            <dxcp:ASPxLabel ID="ASPxLabel7" runat="server" Text="Nº Nota Fiscal:">
                                                            </dxcp:ASPxLabel>
                                                        </td>

                                                        <td>
                                                            <dxcp:ASPxLabel runat="server" Text="Emissão Nota Fiscal:">
                                                            </dxcp:ASPxLabel>
                                                        </td>

                                                        <td>
                                                            <dxcp:ASPxLabel ID="ASPxLabel8" runat="server" Text="Valor da Retenção:">
                                                            </dxcp:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxcp:ASPxLabel ID="ASPxLabel9" runat="server" Text="Valor de Multas:">
                                                            </dxcp:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-right: 5px;">
                                                            <dxcp:ASPxTextBox ID="txtNotaFiscal" runat="server" Width="100%" ClientInstanceName="txtNotaFiscal" MaxLength="20">
                                                            </dxcp:ASPxTextBox>
                                                        </td>
                                                        <td style="padding-right: 5px;">
                                                            <dxcp:ASPxDateEdit ID="dtEmissao" runat="server" ClientInstanceName="dtEmissao" Width="100%">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
     if(s.cp_atualizaRealizadoConformePrevisto == &quot;S&quot;)
     {
               dtPagamento.SetValue(dtVencimento.GetValue());
               spValorPago.SetValue(spValorPrevisto.GetValue());
      }
      cbAvisos.PerformCallback();
}" />
                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </ReadOnlyStyle>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxcp:ASPxDateEdit>
                                                        </td>
                                                        <td style="padding-right: 5px;">
                                                            <dxcp:ASPxSpinEdit ID="spRetencao" runat="server" ClientInstanceName="spRetencao" Number="0" Width="100%" DisplayFormatString="{0:c2}" DecimalPlaces="2" MaxLength="18">
                                                                <SpinButtons ClientVisible="False">
                                                                </SpinButtons>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxcp:ASPxSpinEdit>
                                                        </td>
                                                        <td style="padding-right: 5px;">
                                                            <dxcp:ASPxSpinEdit ID="spMultas" runat="server" ClientInstanceName="spMultas" Number="0" Width="100%" DisplayFormatString="{0:c2}" DecimalPlaces="2" MaxLength="18">
                                                                <SpinButtons ClientVisible="False">
                                                                </SpinButtons>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxcp:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divContaContabil" runat="server">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxLabel ID="lblTituloContaContabil" runat="server" Text="Conta Contábil:" ClientInstanceName="lblTituloContaContabil">
                                                                </dxcp:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxComboBox ID="ddlContaContabil" runat="server" ClientInstanceName="ddlContaContabil" ValueType="System.Int32" Width="100%">
                                                                </dxcp:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div id="divProjeto" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxLabel ID="lblTituloProjeto" runat="server" Text="Projeto:" ClientInstanceName="lblTituloProjeto">
                                                                </dxcp:ASPxLabel>
                                                            </td>
                                                            <td style="width: 250px">
                                                                <dxcp:ASPxLabel ID="ASPxLabel3" runat="server" Text="Versão:">
                                                                </dxcp:ASPxLabel>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxComboBox runat="server" Width="100%" ClientInstanceName="ddlProjetos" ID="ddlProjetos">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                   pnVersaoLB.PerformCallback(s.GetValue());}" />
                                                                </dxcp:ASPxComboBox>
                                                            </td>
                                                            <td style="width: 250px">
                                                                <dxcp:ASPxCallbackPanel ID="pnVersaoLB" ClientInstanceName="pnVersaoLB" runat="server" Width="100%" OnCallback="pnVersaoLB_Callback">
                                                                    <PanelCollection>
                                                                        <dxcp:PanelContent runat="server">
                                                                            <dxcp:ASPxComboBox runat="server" Width="100%" ClientInstanceName="ddlVersaoLB" ID="ddlVersaoLB">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                       var aux = (s.GetValue() == 'null') ? -1 : s.GetValue();
                                                       var strParametros = ddlProjetos.GetValue() + '|' + aux;
	                                                   pnPacote.PerformCallback(strParametros);
                                                       }" />
                                                                            </dxcp:ASPxComboBox>
                                                                        </dxcp:PanelContent>
                                                                    </PanelCollection>
                                                                </dxcp:ASPxCallbackPanel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div runat="server" id="divPacoteEAP">
                                                    <dxcp:ASPxCallbackPanel ID="pnPacote" runat="server" ClientInstanceName="pnPacote" OnCallback="pnPacote_Callback" Width="100%">
                                                        <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" Text="" />
                                                        <Paddings Padding="0px" />
                                                        <PanelCollection>
                                                            <dxcp:PanelContent runat="server">
                                                                <table cellpadding="0" cellspacing="0" class="auto-style1">
                                                                    <tr>
                                                                        <td>
                                                                            <dxcp:ASPxLabel ID="lblTituloPacoteEAP" runat="server" Text="Pacote da EAP:" ClientInstanceName="lblTituloPacoteEAP">
                                                                            </dxcp:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxcp:ASPxComboBox runat="server" Width="100%" ClientInstanceName="ddlPacoteDaEAP" ID="ddlPacoteDaEAP">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnRecurso.PerformCallback(s.GetValue());
}"></ClientSideEvents>
                                                                            </dxcp:ASPxComboBox>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </dxcp:PanelContent>
                                                        </PanelCollection>
                                                    </dxcp:ASPxCallbackPanel>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div runat="server" id="divRecurso">
                                                    <table cellpadding="0" cellspacing="0" class="auto-style1">
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxLabel ID="lblTituloRecurso" runat="server" Text="Recurso:" ClientInstanceName="lblTituloRecurso">
                                                                </dxcp:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxCallbackPanel ID="pnRecurso" runat="server" ClientInstanceName="pnRecurso" Width="100%" OnCallback="pnRecurso_Callback">
                                                                    <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" Text="" />
                                                                    <Paddings Padding="0px" />
                                                                    <PanelCollection>
                                                                        <dxcp:PanelContent runat="server">

                                                                            <dxtv:ASPxComboBox ID="ddlRecurso" runat="server" ClientInstanceName="ddlRecurso" Width="100%">
                                                                            </dxtv:ASPxComboBox>

                                                                        </dxcp:PanelContent>
                                                                    </PanelCollection>
                                                                </dxcp:ASPxCallbackPanel>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxcp:ASPxLabel ID="ASPxLabel10" runat="server" Text="Histórico:">
                                                </dxcp:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxcp:ASPxMemo ID="memoHistorico" runat="server" ClientInstanceName="memoHistorico" Width="100%" Rows="4">
                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </ReadOnlyStyle>
                                                </dxcp:ASPxMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <dxcp:ASPxLabel ID="lblContadorMemo" runat="server" ClientInstanceName="lblContadorMemo" ForeColor="Gray">
                                                </dxcp:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" dir="ltr">
                                                <dxcp:ASPxCallbackPanel ID="cbAvisos" runat="server" ClientInstanceName="cbAvisos" OnCallback="cbAvisos_Callback" Width="100%">
                                                    <PanelCollection>
                                                        <dxcp:PanelContent runat="server">

                                                            <dxtv:ASPxLabel ID="lblAvisos" runat="server" ClientInstanceName="lblAvisos" Font-Bold="True" ForeColor="Red" Width="100%" Wrap="True">
                                                            </dxtv:ASPxLabel>

                                                        </dxcp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </dxtv:ContentControl>
                        </ContentCollection>
                    </dxtv:TabPage>
                    <dxtv:TabPage Text="Documentos">
                        <ContentCollection>
                            <dxtv:ContentControl runat="server">
                                    <iframe id="frmLinkContrato">
                                    </iframe>
                            </dxtv:ContentControl>
                        </ContentCollection>
                    </dxtv:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanging="function(s, e) {
       if (e.tab.index &gt; 0)
       {
                    const urlParams = new URLSearchParams(window.location.search);

                    const codigoContrato = urlParams.get('cc');
                    const numeroAditivoContrato = urlParams.get('na');
                    const numeroParcelaContrato = urlParams.get('np');

                    var sHeightLinkContrato = Math.max(0, document.documentElement.clientHeight) - 146;

                    if (document.getElementById(&quot;frmLinkContrato&quot;) != undefined &amp;&amp; document.getElementById(&quot;frmLinkContrato&quot;) != null) 
                   {
                             document.getElementById(&quot;frmLinkContrato&quot;).style.height = sHeightLinkContrato + &quot;px&quot;;
                             document.getElementById(&quot;frmLinkContrato&quot;).style.width = &quot;100%&quot;;
                             document.getElementById(&quot;frmLinkContrato&quot;).style.padding = &quot;0px&quot;;
                             document.getElementById(&quot;frmLinkContrato&quot;).style.overflow = &quot;auto&quot;;
                            document.getElementById(&quot;frmLinkContrato&quot;).src = './frmLinksContrato1.aspx?CC=' + codigoContrato + '&amp;RO=N&amp;NAC=' + numeroAditivoContrato + '&amp;NPC=' + numeroParcelaContrato;
                   }
                   else 
                  {
                  }
        }
}" />
            </dxcp:ASPxPageControl>

        </div>
        <table style="width: 100%">
            <tr>
                <td>
                    <dxcp:ASPxHyperLink ID="linkIniciativasAssociadasParcela" runat="server" Text="0 Iniciativa(s) Associada(s) à Parcela" ClientInstanceName="linkIniciativasAssociadasParcela" NavigateUrl="javascript:void(0)" ToolTip="É necessário salvar as alterações para acessar o link">
                    </dxcp:ASPxHyperLink>
                </td>
                <td>
                    <table cellpadding="0" cellspacing="5" style="width: 200px; margin-left: auto;">
                        <tr>
                            <td>
                                <dxcp:ASPxButton ID="btnSalvar" runat="server" Text="Salvar" AutoPostBack="False" ClientInstanceName="btnSalvar" Width="100%" CssClass="text-btn">
                                    <ClientSideEvents Click="function(s, e) {
          var msgValidacao = validaCamposFormulario();
          if(msgValidacao == &quot;&quot;)
          {
linkIniciativasAssociadasParcela.SetEnabled(false);

                        callbackSalvar.PerformCallback();
          }
          else
         {
                  window.top.mostraMensagem(msgValidacao, 'atencao', true, false,null);
         }
}" />
                                </dxcp:ASPxButton>
                            </td>
                            <td>
                                <dxcp:ASPxButton ID="btnFechar" runat="server" Text="Fechar" AutoPostBack="False" ClientInstanceName="btnFechar" Width="100%" CssClass="text-btn">
                                    <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal2();
}" />
                                </dxcp:ASPxButton>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>
        <dxcp:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function (s,e) {
    if(s.cp_sucesso != '')
    {
        window.top.mostraMensagem(s.cp_sucesso, 'sucesso', false, false, null);
        window.top.fechaModal2(); 
    }
    else
    {
        if(s.cp_erro != '')
        {
            window.top.mostraMensagem(s.cp_erro, 'erro', true, false, null);
        }
        else
        {
            if(s.cp_aviso != '')
            {
                window.top.mostraMensagem(s.cp_aviso, 'atencao', true, false, null);
            }
        }
    }
    s.cp_erro = '';
    s.cp_sucesso = ''; 
    s.cp_aviso = '';   
}" />
        </dxcp:ASPxCallback>
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
    </form>
    <script type="text/javascript">
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 140;
        var sWidth = Math.max(0, document.documentElement.clientWidth) - 100;

        document.getElementById("divDadosParcela").style.height = sHeight + "px";
        document.getElementById("divDadosParcela").style.overflow = "auto";        
    </script>
</body>
</html>
