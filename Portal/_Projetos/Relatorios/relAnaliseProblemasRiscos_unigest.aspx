<%@ Page Title="" Language="C#" AutoEventWireup="true"
    CodeFile="relAnaliseProblemasRiscos_unigest.aspx.cs" Inherits="_Projetos_Relatorios_relAnaliseProblemasRiscos_unigest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <style type="text/css">
        .style1 {
            width: 100%;
        }
    </style>
</head>
<body scroll="no" style="margin: 0px; text-align: center;" text="#00a">
    <form id="form1" runat="server" enableviewstate="false">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td align="left" style="padding-top: 10px; padding-left: 5px; width: 500px">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 245px; padding-right: 3px">
                                <dxe:ASPxRadioButtonList ID="rblTipoRelatorio" runat="server" Font-Names="Verdana"
                                    Font-Size="8pt" ItemSpacing="15px" RepeatDirection="Horizontal"
                                    Width="100%" SelectedIndex="0">
                                    <Paddings Padding="0px" />
                                    <ClientSideEvents Init="function(s, e) {
	ddlTipo.PerformCallback(s.GetValue());
}"
                                        ValueChanged="function(s, e) {
	ddlTipo.PerformCallback(s.GetValue());
}" />
                                    <Items>
                                        <dxe:ListEditItem Text="Riscos" Value="R" Selected="True" />
                                        <dxe:ListEditItem Text="Problemas" Value="Q" />
                                        <dxe:ListEditItem Text="Ambos" Value="A" />
                                    </Items>
                                </dxe:ASPxRadioButtonList>
                            </td>
                            <td>
                                <dxe:ASPxCheckBox ID="ckbPlanosAcao" runat="server" ClientInstanceName="ckbPlanosAcao"
                                    Font-Names="Verdana" Font-Size="8pt" Text="Apresentar Planos de Ação"
                                    Width="100%" ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                </dxe:ASPxCheckBox>
                            </td>
                            <td>
                                <dxe:ASPxCheckBox ID="ckbAtivo" runat="server" ClientInstanceName="ckbAtivo"
                                    Font-Names="Verdana" Font-Size="8pt" Text="Ativo" Width="100%"
                                    ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                </dxe:ASPxCheckBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td align="left" style="padding-top: 10px">

                    <table class="style1">
                        <tr>
                            <td style="width: 120px">
                                <dxe:ASPxCheckBox ID="checkAfetaAMeta" runat="server" ClientInstanceName="checkAfetaAMeta"
                                    Font-Names="Verdana" Font-Size="8pt" Text="Afeta a meta?" Width="100%"
                                    ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                </dxe:ASPxCheckBox>
                            </td>
                            <td id="4" style="width: 150px">
                                <dxe:ASPxCheckBox ID="checkAfetaOOrcamento" runat="server" ClientInstanceName="checkAfetaOOrcamento"
                                    Font-Names="Verdana" Font-Size="8pt" Text="Afeta o orçamento?" Width="100%"
                                    ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                </dxe:ASPxCheckBox>
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="ddlTipoProjeto" runat="server"
                                    ClientInstanceName="ddlTipoProjeto" Font-Names="Verdana"
                                    Font-Size="8pt" ValueType="System.String" Width="100%" NullText="Tipo de Projeto">
                                    <ItemStyle Wrap="True" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-top: 5px; padding-left: 5px">
                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Names="Verdana"
                        Font-Size="8pt" Text="Tipo:">
                    </dxe:ASPxLabel>
                </td>
                <td align="left" style="padding-left: 5px; padding-top: 5px;">
                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Font-Names="Verdana"
                        Font-Size="8pt" Text="Unidade de Negócio:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 5px">
                    <dxe:ASPxComboBox ID="ddlTipo" runat="server" ClientInstanceName="ddlTipo" Font-Names="Verdana"
                        Font-Size="8pt" ValueType="System.String" Width="100%"
                        OnCallback="ddlTipo_Callback">
                    </dxe:ASPxComboBox>
                </td>
                <td style="padding-left: 5px; padding-right: 5px;">
                    <dxe:ASPxComboBox ID="ddlUnidade" runat="server"
                        ClientInstanceName="ddlUnidade" Font-Names="Verdana"
                        Font-Size="8pt" ValueType="System.String" Width="100%">
                        <ItemStyle Wrap="True" />
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-top: 5px; padding-left: 5px">
                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Font-Names="Verdana"
                        Font-Size="8pt" Text="Projeto/Programa:">
                    </dxe:ASPxLabel>
                </td>
                <td align="left" style="padding-left: 5px; padding-top: 5px">
                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Font-Names="Verdana"
                        Font-Size="8pt" Text="Status do Plano de Ação:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 5px">
                    <dxe:ASPxComboBox ID="ddlProjetoPrograma" runat="server"
                        ClientInstanceName="ddlProjetoPrograma" Font-Names="Verdana"
                        Font-Size="8pt" ValueType="System.String" Width="100%">
                        <ItemStyle Wrap="True" />
                    </dxe:ASPxComboBox>
                </td>
                <td align="left"
                    style="padding-left: 5px; padding-right: 5px;">
                    <dxe:ASPxComboBox ID="ddStatusPlanoAcao" runat="server"
                        ClientInstanceName="ddStatusPlanoAcao" Font-Names="Verdana"
                        Font-Size="8pt" ValueType="System.String" Width="100%">
                    </dxe:ASPxComboBox>
                </td>
            </tr>
        </table>

        <table border="0" cellpadding="0" cellspacing="0"
            style="width: 100%;">
            <tr>
                <td align="left" style="padding-left: 5px; padding-top: 5px;">
                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Font-Names="Verdana"
                        Font-Size="8pt" Text="Foco:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 5px; padding-right: 5px;">
                    <dxe:ASPxComboBox ID="ddlFoco" runat="server"
                        ClientInstanceName="ddlFoco" Font-Names="Verdana"
                        Font-Size="8pt" Width="100%"
                        TextField="Descricao" ValueField="Codigo" ValueType="System.Int32">
                        <ItemStyle Wrap="True" />
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 5px; padding-top: 5px;">
                    <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Font-Names="Verdana"
                        Font-Size="8pt" Text="Direcionador:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 5px; padding-right: 5px;">
                    <dxe:ASPxComboBox ID="ddlDirecionador" runat="server"
                        ClientInstanceName="ddlDirecionador" Font-Names="Verdana"
                        Font-Size="8pt" ValueType="System.Int32" Width="100%"
                        TextField="Descricao" ValueField="Codigo">
                        <ItemStyle Wrap="True" />
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 5px; padding-top: 5px;">
                    <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Font-Names="Verdana"
                        Font-Size="8pt" Text="Grande Desafio:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td align="left" style="padding-left: 5px; padding-right: 5px;">
                    <dxe:ASPxComboBox ID="ddlGrandeDesafio" runat="server"
                        ClientInstanceName="ddlGrandeDesafio" Font-Names="Verdana"
                        Font-Size="8pt" Width="100%">
                        <ItemStyle Wrap="True" />
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 20px; padding-top: 10px;">
                    <dxe:ASPxButton ID="btnImprimir" runat="server"
                        ClientInstanceName="btnImprimir" Font-Names="Verdana" Font-Size="8pt"
                        OnClick="btnImprimir_Click" Text="Imprimir">
                        <ClientSideEvents Click="function(s, e) {
	if(ddlTipo.GetValue() == null)
	{
		window.top.mostraMensagem(&quot;Tipo deve ser escolhido!&quot;, 'atencao', true, false, null);
                               e.processOnServer = false;
}
}" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>



<script>
    var mensagem = '<%=mensagemAusenciaDados%>';
    if (mensagem) {
        window.top.mostraMensagem(mensagem, 'atencao', true, false, null);
    }
</script>