<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="AnalisesDePerformance.aspx.cs"
    Inherits="_Estrategias_Relatorios_AnalisesDePerformance" Title="Portal da Estratégia" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>    
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
<script type="text/javascript" language="javascript">
    function validaData()
    {
        var retorno = false;
        if(dteDe.GetValue()!= null && dteAte.GetValue()  != null)
        {
        
            var dataInicio 	  = new Date(dteDe.GetValue());
		    var meuDataInicio = (dataInicio.getMonth() +1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
		    dataInicio  	  = Date.parse(meuDataInicio);
				
		    var dataTermino 	= new Date(dteAte.GetValue());
		    var meuDataTermino 	= (dataTermino.getMonth() +1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
		    dataTermino		    = Date.parse(meuDataTermino);
		
	        if(dataInicio > dataTermino)
            {
	            window.top.mostraMensagem("A Data de Início não pode ser maior que a Data de Término.", 'atencao', true, false, null);
            }    
		    else
		    {
		        retorno = true;
		    }
        }
        else
        {
            
            if(dteDe.GetValue() == null && dteAte.GetValue() == null)
            {
                window.top.mostraMensagem("A Data de Início e a Data de Término devem ser informadas.", 'atencao', true, false, null);
            }
            if(dteDe.GetValue() == null)
            {
                window.top.mostraMensagem("A Data de Início deve ser informada.", 'atencao', true, false, null);
            }
            else if(dteAte.GetValue() == null)
            {
                window.top.mostraMensagem("A Data de Término deve ser informada.", 'atencao', true, false, null);
            }
        }
        return retorno;
    }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="left">
                <dxcp:ASPxCallbackPanel ID="pnRelatorio" runat="server" ClientInstanceName="pnRelatorio"
                    OnCallback="pnRelatorio_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" id="tabelaDentroCallback">
                                <tr>
                                    <td style="width: 5px; height: 10px">
                                    </td>
                                    <td style="height: 10px">
                                        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
                                            width: 100%">
                                            <tr>
                                                <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                                                    height: 26px">
                                                    <table>
                                                        <tr>
                                                            <td align="left" style="height: 19px" valign="middle">
                                                                &nbsp; &nbsp;
                                                                <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloTela"
                                                                    Font-Bold="True"  
                                                                    Text="Relat&#243;rio de An&#225;lise de Performance">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px; height: 10px">
                                    </td>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td valign="middle">
                <dxe:ASPxRadioButtonList ID="rblTipoRelatorio" runat="server" ClientInstanceName="rblTipoRelatorio"
                     RepeatDirection="Horizontal" SelectedIndex="1" Width="150px">
                    <Items>
                        <dxe:ListEditItem Text="Objetivo" Value="OB" />
                        <dxe:ListEditItem Selected="True" Text="Indicador" Value="IN" />
                    </Items>
                    <Paddings Padding="0px" />
                </dxe:ASPxRadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                    <td style="height: 15px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                    <td>
                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                    Text="Unidade:" Width="50px">
                </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                    <td>
                                        <dxe:ASPxComboBox ID="ddlUnidade" runat="server" ClientInstanceName="ddlUnidade"
                                             IncrementalFilteringMode="Contains" ValueType="System.String"
                                            Width="700px">
                                            <Columns>
                                                <dxe:ListBoxColumn Caption="Nome" />
                                                <dxe:ListBoxColumn Caption="Sigla" />
                                            </Columns>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                    <td style="height: 15px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                    <td>
                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                    Text="Mapa:" Width="36px">
                </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                    <td>
                <dxe:ASPxComboBox ID="ddlMapa" runat="server" ClientInstanceName="ddlMapa" 
                    ValueType="System.String" Width="700px">
                </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="height: 15px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
    <table cellpadding="0" cellspacing="0" style="width: 233px" border="0">
        <tr>
            <td align="left" valign="middle">
                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                    Text="De:">
                </dxe:ASPxLabel>
            </td>
            <td align="left" style="width: 7px" valign="middle">
            </td>
            <td align="left" valign="middle">
                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                    Text="At&#233;:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <dxe:ASPxDateEdit ID="dteDe" runat="server" ClientInstanceName="dteDe" 
                    Width="95px" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom"
                    EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
            </td>
            <td align="left" style="width: 7px" valign="middle">
                &nbsp;</td>
            <td align="left" valign="middle">
                <dxe:ASPxDateEdit ID="dteAte" runat="server" ClientInstanceName="dteAte" 
                    Width="95px" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom"
                    EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
            </td>
        </tr>
    </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                    <td style="height: 15px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 115px">
                                                    <dxe:ASPxButton ID="ASPxButton1" runat="server" 
                                                        Text="Gerar PDF" AutoPostBack="False" ImageSpacing="2px" Width="125px">
                                                        <ClientSideEvents Click="function(s, e) 
{   
    var option = '';
	var nome = '';
	    
   if(ddlMapa.GetSelectedItem() != null)
   {
        e.processOnServer = false;
	    var option = ddlMapa.GetSelectedItem().value;
	    var nome = ddlMapa.GetSelectedItem().text;

	    var parametro = option + &quot;|&quot; + nome;
		    
	    if(validaData())
	        pnRelatorio.PerformCallback(parametro);
        else
            return;
   }
   else
   {
        window.top.mostraMensagem('Selecione um mapa estratégico', 'atencao', true, false, null);
        return;
   }
   var nomeMapa = nome;
   var codMapa = option != '' ? option : -1;
   
   //debugger
   var codUnidadeSelecionada = ddlUnidade.GetValue();
   var codTipoAssociacao = hfGeral.Get(&quot;CodigoTipoAssociacao&quot;);
   
   var dataImpressao = hfGeral.Get(&quot;dataImpressao&quot;);
   var dataInicial = dteDe.GetText();
   var dataFinal = dteAte.GetText();
   var tipoAnalise = hfGeral.Get(&quot;tipoAnalise&quot;);
   var iniciaisTipoAssociacao = rblTipoRelatorio.GetValue();

   var url = &quot;popupRelAnaliseDePerformance.aspx&quot;;
   url += &quot;?CUN=&quot; + codUnidadeSelecionada;
   url += &quot;&amp;CTA=&quot; + codTipoAssociacao;
   url += &quot;&amp;CM=&quot; + codMapa;
   url += &quot;&amp;DE=&quot; + dataInicial;
   url += &quot;&amp;AT=&quot; + dataFinal;
   url += &quot;&amp;DI=&quot; + dataImpressao;
   url += &quot;&amp;TA=&quot; + tipoAnalise;
   url += &quot;&amp;INI=&quot; + iniciaisTipoAssociacao;
   window.top.showModal(url, 'Análise de Performance', screen.width - 60, screen.height - 260, '', null);
}" />
                                                        <Image Url="~/imagens/botoes/btnPDF.png">
                                                        </Image>
                                                        <Paddings Padding="0px" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td style="width: 12px">
                                                </td>
                                                <td>
                                                    <dx:ReportToolbar ID="ReportToolbar1" runat="server" ReportViewer="<%# ReportViewer1 %>"
                                                        ShowDefaultButtons="False" Width="362px"  Visible="False">
                                                        <Items>
                                                            <dx:ReportToolbarButton ItemKind="Search" />
                                                            <dx:ReportToolbarSeparator />
                                                            <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                                                            <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                                                            <dx:ReportToolbarLabel ItemKind="PageLabel" Text="P&#225;gina" />
                                                            <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                                                            </dx:ReportToolbarComboBox>
                                                            <dx:ReportToolbarLabel ItemKind="OfLabel" Text="de" />
                                                            <dx:ReportToolbarTextBox ItemKind="PageCount" />
                                                            <dx:ReportToolbarButton ItemKind="NextPage" />
                                                            <dx:ReportToolbarButton ItemKind="LastPage" />
                                                        </Items>
                                                        <Styles>
                                                            <LabelStyle>
                                                                <Margins MarginLeft="3px" MarginRight="3px" />
                                                            </LabelStyle>
                                                        </Styles>
                                                    </dx:ReportToolbar>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <dx:ReportViewer runat="server" ClientInstanceName="ReportViewer1" AutoSize="False"
                                            Width="98%" ID="ReportViewer1" Style="overflow: scroll" Visible="False">
                                            <Paddings PaddingLeft="5px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                        </dx:ReportViewer>
                                    </td>
                                </tr>
                            </table>
                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                            </dxhf:ASPxHiddenField>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</asp:Content>
