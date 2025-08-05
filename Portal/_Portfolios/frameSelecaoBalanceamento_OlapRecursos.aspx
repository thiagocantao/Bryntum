<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameSelecaoBalanceamento_OlapRecursos.aspx.cs" Inherits="_Portfolios_frameSelecaoBalanceamento_OlapRecursos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        function atualizaDados()
        {            
            callBackVC.PerformCallback();
        }
        
        function recarregaTela()
        {
            if(document.getElementById('tdFiltroRecurso2').style.display == 'block')
            {
                document.getElementById('frameOlap').src = 'frameProposta_GraficoRH.aspx?DefineAltura=S&CRH=' + ddlRecurso.GetValue() + '&Cenario=' + ddlCenario.GetValue();   
            }
            else
            {
                document.getElementById('frameOlap').src = 'frameSelecaoBalanceamento_OlapRecursosTabela.aspx';               
            }           
        }

        function exportarParaExcel() {
            document.getElementById('frameOlap').src = 'frameSelecaoBalanceamento_OlapRecursosTabela.aspx?Excel=S';  
        }

        function alteraVisao()
        {
            if(document.getElementById('tdFiltroRecurso2').style.display == 'block')
            {
                document.getElementById('tdFiltroRecurso2').style.display = "none";
                document.getElementById('tdFiltroRecurso1').style.display = "none";                
                imgModoVisao.SetImageUrl('../imagens/graficos.PNG');
                imgModoVisao.mainElement.title = 'Mostrar Gráfico de Disponibilidade dos Recursos';	
                document.getElementById('frameOlap').src = 'frameSelecaoBalanceamento_OlapRecursosTabela.aspx';
                lblGantt.SetText('Mostrar Gráfico');
            }
            else
            {
                imgModoVisao.SetImageUrl('../imagens/olap.PNG');   
                imgModoVisao.mainElement.title = 'Mostrar Olap de Análise de Recursos';
                document.getElementById('frameOlap').src = 'frameProposta_GraficoRH.aspx?DefineAltura=S&CRH=' + ddlRecurso.GetValue() + '&Cenario=' + ddlCenario.GetValue();
                document.getElementById('tdFiltroRecurso1').style.display = "block";                
                document.getElementById('tdFiltroRecurso2').style.display = "block";
                lblGantt.SetText('Mostrar Tabela');
            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }

        .btn_inicialMaiuscula {
            text-transform: capitalize !important;
        }
    </style>
</head>
<body style="margin-top:0; overflow:hidden">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td>
                    <dxcb:ASPxCallback ID="callBackVC" runat="server" ClientInstanceName="callBackVC"
                        OnCallback="callBackVC_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
                        if (document.getElementById('frameOlap')!=null)
	                        recarregaTela();
}" />
                    </dxcb:ASPxCallback>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td align="left" valign="bottom">
                                <table cellpadding="0" cellspacing="0" style="border-right: gainsboro 1px solid;
                                    border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid; padding: 8px; display: none;">
                                    <tr>
                                        <td style="width: 35px">
                                            <dxe:ASPxImage ID="imgModoVisao" runat="server" ClientInstanceName="imgModoVisao" ImageUrl="~/imagens/graficos.PNG" ToolTip="Mostrar Gráfico de Disponibilidade dos Recursos">
                                    <ClientSideEvents Click="function(s, e) {
	alteraVisao();
}" />
                                </dxe:ASPxImage>
                                        </td>
                                        <td style="width: 110px">
                                            <dxe:ASPxLabel ID="lblGantt" runat="server" 
                                                Text="Mostrar Gráfico" ClientInstanceName="lblGantt">
                                                <ClientSideEvents Click="function(s, e) {
	alteraVisao();
}" />
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                    <table>
                        <tr>
                            
                            <td align="left" id="tdFiltroRecurso2" style="display: none;">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    >
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 90px" align="left">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <td style="width:65px" align="left">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                        Text="Categoria:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 175px" align="left">
                                    <dxe:ASPxComboBox ID="ddlCategoria" runat="server" ClientInstanceName="ddlCategoria"
                                         Width="170px" IncrementalFilteringMode="Contains" TextFormatString="{1}" ValueType="System.Int32">
                                        <Columns>
                                            <dxe:ListBoxColumn Caption="Sigla" FieldName="SiglaCategoria" Width="100px" />
                                            <dxe:ListBoxColumn Caption="Categoria" FieldName="DescricaoCategoria" Width="300px" />
                                        </Columns>
                                    </dxe:ASPxComboBox>
                                </td> 
                            </td>
                            <td>
                              <td align="left" style="width: 40px; ">
                                <dxe:ASPxLabel ID="lblCenario" runat="server" 
                                    Text="Cenário:">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 100px; ">
                                <dxe:ASPxComboBox ID="ddlCenario" runat="server" ClientInstanceName="ddlCenario"
                                     SelectedIndex="0" ValueType="System.String"
                                    Width="105px">
                                    <Items>
                                        <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, cen_rio_1 %>" Value="1" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_2 %>" Value="2" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_3 %>" Value="3" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_4 %>" Value="4" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_5 %>" Value="5" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_6 %>" Value="6" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_7 %>" Value="7" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_8 %>" Value="8" />
                                        <dxe:ListEditItem Text="<%$ Resources:traducao, cen_rio_9 %>" Value="9" />
                                    </Items>
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlRecurso.PerformCallback('A');
}" />
                                </dxe:ASPxComboBox>
                            </td>
                            </td>
                            <td align="left" style="padding-right: 5px; display: none;" 
                                id="tdFiltroRecurso1">
                                <dxe:ASPxComboBox ID="ddlRecurso" runat="server" 
                                    ClientInstanceName="ddlRecurso"  
                                    OnCallback="ddlRecurso_Callback" ValueType="System.String" Width="200px">
                                </dxe:ASPxComboBox>
                            </td>
                            <td style="width: 90px" align="left">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" AutoPostBack="False" CssClass="btn_inicialMaiuscula"
                                    Text="Selecionar" Width="90px">
                                    <Paddings Padding="0px" />
                                    <ClientSideEvents Click="function(s, e) 
{
var mensagemErro = &quot;&quot;;
var contador = 0;
if(ddlCategoria.GetValue() == null)
{
mensagemErro += contador++ +  &quot;) A categoria precisa ser preenchida\n&quot;; 
}
if(ddlCenario.GetValue() == null)
{
mensagemErro += contador++ +  &quot;) O cenário precisa ser escolhido\n&quot;;
}
if(ddlRecurso.GetValue() == null)
{
mensagemErro += contador++ +  &quot;) O recurso precisa ser escolhido\n&quot;;
}	
if(mensagemErro != &quot;&quot;)
{
window.top.mostraMensagem(mensagemErro, 'atencao', true, false, null);
}
else
{
callBackVC.PerformCallback('AtualizarVC');
}
}" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td id='tdGrafico'>
                    <iframe id="frameOlap" name="frameOlap" src="frameSelecaoBalanceamento_OlapRecursosTabela.aspx" width="100%" scrolling="no"
                                frameborder="0" style="height: <%=alturaTela %>"></iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
