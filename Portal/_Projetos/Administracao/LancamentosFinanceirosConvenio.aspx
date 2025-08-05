<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LancamentosFinanceirosConvenio.aspx.cs" Inherits="_Projetos_Administracao_LancamentosFinanceirosConvenio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function btnSalvar_Click(s, e) {
            var container = formLayout.GetMainElement();
            var valido = ASPxClientEdit.ValidateEditorsInContainer(container);
            if (valido) {
                callback.PerformCallback('');
            } else {
                window.top.mostraMensagem('Todos os campos obrigatórios devem ser preenchidos', 'atencao', true, false, null);
            }
        }

        function callback_CallbackComplete(s, e) {
            if (e.result) {
                window.top.mostraMensagem(e.result, 'atencao', true, false, null);
            }
            else {
                window.top.mostraMensagem('Alterações salvas com sucesso', 'sucesso', false, false, null, 2000);
                if (window.top.pcModal2.GetVisible() == true) {
                    window.top.fechaModal2();
                }
                else {
                    window.top.fechaModal();
                }
            }
        }
    </script>
    <style type="text/css">
        .btn_inicialMaiuscula {
            text-transform: capitalize !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

             <dxcp:ASPxPageControl ID="pageControl" runat="server" ActiveTabIndex="0" Width="100%" ClientInstanceName="pageControl">
            <TabPages>
                <dxtv:TabPage Text="Pagamentos">
                    <ContentCollection>
                        <dxtv:ContentControl runat="server">
                            <dxcp:ASPxFormLayout ID="formLayout" runat="server" ColCount="10" OptionalMark="(opicional)" Width="100%" ClientInstanceName="formLayout" DataSourceID="dataSource" Font-Names="Verdana" Font-Size="8pt" AlignItemCaptionsInAllGroups="True">
                <Items>
                    <dxtv:LayoutItem Caption="Contrato:" ColSpan="2" FieldName="NumeroContrato">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxTextBox ID="txtContrato" runat="server" Width="100%" ClientInstanceName="txtContrato" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <CaptionSettings Location="Top" />
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Término" ColSpan="2" FieldName="DataTermino">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxDateEdit ID="deTermino" runat="server" Width="100%" ClientInstanceName="deTermino" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxDateEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Valor" ColSpan="2" FieldName="ValorContrato">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxSpinEdit ID="seValor" runat="server" Width="100%" ClientInstanceName="seValor" DecimalPlaces="2" DisplayFormatString="{0:n2}" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" HorizontalAlign="Right">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxSpinEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Saldo" ColSpan="2" FieldName="Saldo">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxSpinEdit ID="seSaldo" runat="server" Width="100%" ClientInstanceName="seSaldo" DecimalPlaces="2" DisplayFormatString="{0:n2}" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" HorizontalAlign="Right">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxSpinEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Nº Aditivo" Width="10%" FieldName="NumeroAditivo">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxSpinEdit ID="seNumeroAditivo" runat="server" Width="100%" ClientInstanceName="seNumeroAditivo" NumberType="Integer" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt">
                                    <ReadOnlyStyle BackColor="LightGray" ForeColor="Black">
                                    </ReadOnlyStyle>
                                </dxtv:ASPxSpinEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Nº Parcela" Width="10%" FieldName="NumeroParcela">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxSpinEdit ID="seNumeroParcela" runat="server" Width="100%" ClientInstanceName="seNumeroParcela" NumberType="Integer" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt">
                                    <ReadOnlyStyle BackColor="LightGray" ForeColor="Black">
                                    </ReadOnlyStyle>
                                </dxtv:ASPxSpinEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Tipo" ColSpan="4" ShowCaption="False" VerticalAlign="Bottom" FieldName="IndicaDespesaReceita">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxRadioButtonList ID="rblTipo" runat="server" RepeatDirection="Horizontal" Width="100%" ClientInstanceName="rblTipo" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" SelectedIndex="0">
                                    <Items>
                                        <dxtv:ListEditItem Text="Custo" Value="D" Selected="True" />
                                        <dxtv:ListEditItem Text="Receita" Value="R" />
                                    </Items>
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxRadioButtonList>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Emitente" ColSpan="6" FieldName="CodigoPessoaEmitente">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxComboBox ID="cmbEmitente" runat="server" Width="100%" ClientInstanceName="cmbEmitente" DataSourceID="dataSourceEmitente" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" TextField="NomePessoa" ValueField="CodigoPessoa" ValueType="System.Int32">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxComboBox>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Partícipe" ColSpan="4" FieldName="CodigoParticipe">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxComboBox ID="cmbParticipe" runat="server" Width="100%" ClientInstanceName="cmbParticipe" DataSourceID="dataSourceParticipe" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" TextField="NomePessoa" ValueField="CodigoPessoa" ValueType="System.Int32">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxComboBox>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Valor Previsto" ColSpan="2" FieldName="ValorPrevisto">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxSpinEdit ID="seValorPrevisto" runat="server" Width="100%" ClientInstanceName="seValorPrevisto" DecimalPlaces="2" DisplayFormatString="{0:n2}" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" HorizontalAlign="Right">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxSpinEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Data Vencimento" ColSpan="2" FieldName="DataVencimento">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxDateEdit ID="deDataVencimento" runat="server" Width="100%" ClientInstanceName="deDataVencimento" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" OnDataBound="dateEdit_DataBound">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxDateEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Previsão de Pagamento" ColSpan="2" FieldName="PrevisaoPagamento">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxDateEdit ID="dePrevisaoPagamento" runat="server" Width="100%" ClientInstanceName="dePrevisaoPagamento" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" OnDataBound="dateEdit_DataBound">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxDateEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Conta Contábil" ColSpan="6" FieldName="ContaContabil">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxComboBox ID="cmbContaContabil" runat="server" Width="100%" ClientInstanceName="cmbContaContabil" DataSourceID="dataSourceContaContabil" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" TextField="DescricaoConta" ValueField="CodigoConta" ValueType="System.Int32">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxComboBox>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Projeto" ColSpan="4" FieldName="NomeProjeto">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxTextBox ID="txtProjeto" runat="server" ClientEnabled="False" ClientInstanceName="txtProjeto" Font-Names="Verdana" Font-Size="8pt" Width="100%">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Observações Empenho" ColSpan="10" FieldName="ObservacaoEmpenho">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxMemo ID="memoObservacoesEmprenho" runat="server" Rows="5" Width="100%" ClientInstanceName="memoObservacoesEmprenho" ClientEnabled="False" MaxLength="4000" Font-Names="Verdana" Font-Size="8pt">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxMemo>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Emissão Doc. Fiscal" ColSpan="2" FieldName="EmissaoDocFiscal">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxDateEdit ID="deEmissaoDocumentoFiscal" runat="server" Width="100%" ClientInstanceName="deEmissaoDocumentoFiscal" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxDateEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Nº Doc. Fiscal" ColSpan="2" FieldName="NumeroDocFiscal">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxTextBox ID="txtNumeroDocumentoFiscal" runat="server" Width="100%" ClientInstanceName="txtNumeroDocumentoFiscal" ClientEnabled="False" MaxLength="25" Font-Names="Verdana" Font-Size="8pt">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Data do Pagamento" ColSpan="2" FieldName="DataPagamento">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxDateEdit ID="deDataPagamento" runat="server" Width="100%" ClientInstanceName="deDataPagamento" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxDateEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Valor Pago" ColSpan="2" FieldName="ValorPago">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxSpinEdit ID="seValorPago" runat="server" Width="100%" ClientInstanceName="seValorPago" DecimalPlaces="2" DisplayFormatString="{0:n2}" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" HorizontalAlign="Right">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxSpinEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Valor Retenções" ColSpan="2" FieldName="ValorRetencoes">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxSpinEdit ID="seValorRetencoes" runat="server" Width="100%" ClientInstanceName="seValorRetencoes" DecimalPlaces="2" DisplayFormatString="{0:n2}" ClientEnabled="False" Font-Names="Verdana" Font-Size="8pt" HorizontalAlign="Right">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxSpinEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="Observações Pagamento" ColSpan="10" FieldName="ObservacaoPagamento">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxMemo ID="memoOpcoesPagamento" runat="server" Rows="5" Width="100%" ClientInstanceName="memoOpcoesPagamento" ClientEnabled="False" MaxLength="4000" Font-Names="Verdana" Font-Size="8pt">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxMemo>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="DataInicio" ClientVisible="False" ColSpan="2" FieldName="DataInicio">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxDateEdit ID="deInicio" runat="server" ClientEnabled="False" ClientInstanceName="deInicio">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxDateEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="CodigoLancamentoFinanceiro" ClientVisible="False" ColSpan="2" FieldName="CodigoLancamentoFinanceiro">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxSpinEdit ID="seCodigoLancamentoFinanceiro" runat="server" ClientInstanceName="seCodigoLancamentoFinanceiro" NumberType="Integer">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxSpinEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="CodigoContrato" ClientVisible="False" ColSpan="3" FieldName="CodigoContrato">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxSpinEdit ID="seCodigoContrato" runat="server" ClientInstanceName="seCodigoContrato">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxSpinEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                    <dxtv:LayoutItem Caption="SequencialParcela" ClientVisible="False" ColSpan="3" FieldName="SequencialParcela">
                        <LayoutItemNestedControlCollection>
                            <dxtv:LayoutItemNestedControlContainer runat="server">
                                <dxtv:ASPxSpinEdit ID="seSequencialParcela" runat="server" ClientInstanceName="seSequencialParcela" NumberType="Integer">
                                    <DisabledStyle BackColor="LightGray" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxSpinEdit>
                            </dxtv:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dxtv:LayoutItem>
                </Items>
                <SettingsItemCaptions Location="Top" />
                <Styles>
                    <Disabled BackColor="LightGray" ForeColor="Black">
                    </Disabled>
                </Styles>
            </dxcp:ASPxFormLayout>
                        </dxtv:ContentControl>
                    </ContentCollection>
                </dxtv:TabPage>
                <dxtv:TabPage Text="Anexos">
                    <ContentCollection>
                        <dxtv:ContentControl runat="server">
                             <iframe id="frmAnexos" frameborder="0" height="276" scrolling="no" src=""></iframe>
                        </dxtv:ContentControl>
                    </ContentCollection>
                </dxtv:TabPage>
            </TabPages>
                 <ClientSideEvents ActiveTabChanged="function(s, e) {
	document.getElementById('frmAnexos').src = '../../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=LF&amp;ID=' + s.cp_CodigoObjeto ;

}" Init="function(s, e) {
document.getElementById('frmAnexos').height = s.cp_Altura;
document.getElementById('frmAnexos').width = s.cp_Largura; 


}" />
        </dxcp:ASPxPageControl>
            <table style="margin-left: auto; padding: 10px;width:100%">
                <tr>
                    <td></td>
                    <td style="width:100px">
                        <dxtv:ASPxButton ID="btnSalvar" runat="server" Text="Salvar" Width="100px" AutoPostBack="False" ClientInstanceName="btnSalvar" CssClass="btn_inicialMaiuscula">
                            <ClientSideEvents Click="btnSalvar_Click" />
                        </dxtv:ASPxButton>
                    </td>
                    <td style="width:100px;padding-left:5px">
                        <dxtv:ASPxButton ID="btnFechar" runat="server" Text="Fechar" Width="100px" AutoPostBack="False" ClientInstanceName="btnFechar" CssClass="btn_inicialMaiuscula">
                            <ClientSideEvents Click="function btnFechar_Click(s, e) 
{
        if(window.top.pcModal2.GetVisible() == true)
        {
              window.top.fechaModal2();
        }
        else
       {
              window.top.fechaModal();
       }
}" />
                        </dxtv:ASPxButton>
                    </td>
                </tr>
            </table>
           
        </div>

        <asp:SqlDataSource ID="dataSource" runat="server" SelectCommand="IF @CodigoLancamentoFinanceiro = -1
BEGIN
 SELECT NULL AS CodigoLancamentoFinanceiro,
        c.NumeroContrato,
        c.DataInicio,
        c.DataTermino,
        c.ValorContrato,
        (c.ValorContrato - 
          (SELECT ISNULL(SUM(ISNULL(ValorPago,0)),0) FROM ParcelaContrato WHERE CodigoContrato = c.CodigoContrato AND DataPagamento IS NOT NULL) -
          (SELECT ISNULL(SUM(ISNULL(ValorPrevisto,0)),0) FROM ParcelaContrato WHERE CodigoContrato = c.CodigoContrato AND DataPagamento is NULL)
        ) as Saldo,
        NULL AS NumeroAditivo,
        NULL AS NumeroParcela,
        (CASE WHEN c.TipoPessoa = 'F' THEN 'D' ELSE 'R' END) AS IndicaDespesaReceita,
        c.CodigoPessoaContratada AS CodigoPessoaEmitente,
        NULL AS CodigoParticipe,
        NULL AS ValorPrevisto,
        NULL AS DataVencimento,
        NULL AS PrevisaoPagamento,
        NULL AS ContaContabil,
        NULL AS ObservacaoEmpenho,
        NULL AS EmissaoDocFiscal,
        NULL AS NumeroDocFiscal,
        NULL AS DataPagamento,
        NULL AS ValorPago,
        NULL AS ValorRetencoes,
        NULL AS ObservacaoPagamento,
        c.CodigoContrato,
        NULL AS SequencialParcela,
        p.NomeProjeto
   FROM Contrato AS c LEFT JOIN
        Projeto AS p ON p.CodigoProjeto = c.CodigoProjeto
  WHERE c.CodigoContrato = @CodigoContrato
END
ELSE
BEGIN
 SELECT lf.CodigoLancamentoFinanceiro,
        c.NumeroContrato,
        c.DataInicio,
        c.DataTermino,
        c.ValorContrato,
        (c.ValorContrato - 
          (SELECT ISNULL(SUM(ISNULL(ValorPago,0)),0) FROM ParcelaContrato WHERE CodigoContrato = c.CodigoContrato AND DataPagamento IS NOT NULL) -
          (SELECT ISNULL(SUM(ISNULL(ValorPrevisto,0)),0) FROM ParcelaContrato WHERE CodigoContrato = c.CodigoContrato AND DataPagamento is NULL)
        ) as Saldo,
        pc.NumeroAditivoContrato AS NumeroAditivo,
        pc.NumeroParcela,
        ISNULL(lf.IndicaDespesaReceita, (CASE WHEN c.TipoPessoa = 'F' THEN 'D' ELSE 'R' END)) AS IndicaDespesaReceita,
        ISNULL(lf.CodigoPessoaEmitente, c.CodigoPessoaContratada) AS CodigoPessoaEmitente,
        lf.CodigoParticipe,
        lf.ValorEmpenhado AS ValorPrevisto,
        lf.DataVencimento AS DataVencimento,
        lf.DataPrevistaPagamentoRecebimento AS PrevisaoPagamento,
        lf.CodigoConta AS ContaContabil,
        lf.HistoricoEmpenho AS ObservacaoEmpenho,
        lf.DataEmissaoDocFiscal AS EmissaoDocFiscal,
        lf.NumeroDocFiscal,
        lf.DataPagamentoRecebimento AS DataPagamento,
        lf.ValorPagamentoRecebimento AS ValorPago,
        lf.ValorRetencao AS ValorRetencoes,
        lf.HistoricoPagamento AS ObservacaoPagamento,
        c.CodigoContrato,
        pc.SequencialParcela,
        p.NomeProjeto
   FROM LancamentoFinanceiro AS lf LEFT JOIN
        Projeto AS p ON p.CodigoProjeto = lf.CodigoProjeto LEFT JOIN
        ParcelaContrato AS pc ON lf.CodigoObjetoAssociado = pc.Sequencialparcela
                             AND dbo.f_GetIniciaisTipoAssociacao(lf.CodigoTipoAssociacao) = 'PA' LEFT JOIN
        Contrato AS c ON pc.CodigoContrato = c.CodigoContrato
  WHERE CodigoLancamentoFinanceiro = @CodigoLancamentoFinanceiro 
END">
            <SelectParameters>
                <asp:QueryStringParameter Name="CodigoLancamentoFinanceiro" QueryStringField="clf" DefaultValue="-1" />
                <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="cc" DefaultValue="-1" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource runat="server" ID="dataSourceEmitente" SelectCommand=" SELECT p.CodigoPessoa, p.NomePessoa, p.NumeroCNPJCPF
   FROM Pessoa p INNER JOIN 
        PessoaEntidade pe ON pe.CodigoPessoa = p.CodigoPessoa
  WHERE pe.CodigoEntidade = @CodigoEntidade
    AND pe.IndicaPessoaAtivaEntidade = 'S'              
  ORDER BY p.NomePessoa">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" />
            </SelectParameters>
        </asp:SqlDataSource>
        
        <asp:SqlDataSource ID="dataSourceParticipe" runat="server" SelectCommand="SELECT CodigoPessoa, NomePessoa, NumeroCNPJCPF FROM dbo.f_gestconv_getParticipesProjeto(@CodigoProjeto) ORDER BY NomePessoa">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="cp" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dataSourceContaContabil" runat="server" UpdateCommand="SELECT CodigoConta, CodigoReservadoGrupoConta FROM dbo.f_gestconv_getPlanoContas(@CodigoProjeto, @CodigoEntidade)" SelectCommand="IF(@CodigoProjeto = -1)
--BEGIN
 SELECT [CodigoConta] = pcfp.[CodigoConta], 
        [DescricaoConta] = CAST(pcfp.[DescricaoConta] + ISNULL(' (' + pcfp.[CodigoReservadoGrupoConta] + ')','') AS varchar(70))
   FROM [dbo].[PlanoContasFluxoCaixa] pcfp 
  WHERE pcfp.[CodigoEntidade] = @CodigoEntidade AND pcfp.[IndicaContaAnalitica] = 'S' 
  ORDER BY 2
--END
ELSE
 SELECT [CodigoConta] = CodigoConta, 
        [DescricaoConta] = (DescricaoConta + ' (' + CodigoReservadoGrupoConta + ')')
   FROM [dbo].[f_gestconv_getContasAnaliticasProjeto](@CodigoProjeto) 
  ORDER BY 2">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoProjeto" SessionField="cp" />
                <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" />
            </SelectParameters>
            <UpdateParameters>
                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                <asp:SessionParameter Name="CodigoEntidade" SessionField="ce" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <dxcp:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents CallbackComplete="callback_CallbackComplete" />
        </dxcp:ASPxCallback>
    </form>
</body>
</html>
