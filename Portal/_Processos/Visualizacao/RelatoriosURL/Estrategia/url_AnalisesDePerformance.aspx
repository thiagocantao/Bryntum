<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_AnalisesDePerformance.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Estrategia_url_AnalisesDePerformance"
    Title="Portal da Estratégia" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../../estilos/custom.css" rel="stylesheet" />
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 100px;
        }
        </style>
        <script type="text/javascript">
            function init(s) {
                var isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);
                var createFrameElement = s.printHelper.createFrameElement;

                s.printHelper.createFrameElement = function (name) {
                    var frameElement = createFrameElement.call(this, name);
                    if (isChrome) {
                        frameElement.addEventListener("load", function (e) {
                            if (frameElement.contentDocument.contentType !== "text/html")
                                frameElement.contentWindow.print();
                        });
                    }
                    return frameElement;
                }
            }
            </script>
    </head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td>
                                <table id="Table1" border="0" cellpadding="0" cellspacing="0" 
                                    style="width: 100%">
                                    <tr>
                                        <td valign="bottom">
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td valign="bottom">
                                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                            Text="Unidade:" Width="60px">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="bottom">
                                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                            Text="Mapa:" Width="60px">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="style2" valign="bottom">
                                                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                                            Text="De:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="style2" valign="bottom">
                                                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server"
                                                            Text="Até:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="style2">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 5px; width: 180px; padding-left: 5px;" 
                                                        valign="bottom">
                                            <dxe:ASPxRadioButtonList ID="rblTipoRelatorio" runat="server" ClientInstanceName="rblTipoRelatorio"
                                                RepeatDirection="Horizontal" SelectedIndex="1"
                                                Width="100%">
                                                <Paddings Padding="0px" />
<Paddings Padding="0px"></Paddings>
                                                <Items>
                                                    <dxe:ListEditItem Text="Objetivo" Value="OB" />
                                                    <dxe:ListEditItem Selected="True" Text="Indicador" Value="IN" />
                                                </Items>
                                            </dxe:ASPxRadioButtonList>
                                                    </td>
                                                    <td style="padding-right: 5px" valign="bottom">
                                                        <dxe:ASPxComboBox ID="ddlUnidade" runat="server" ClientInstanceName="ddlUnidade"
                                                            IncrementalFilteringMode="Contains" 
                                                            Width="100%">
                                                            <Columns>
                                                                <dxe:ListBoxColumn Caption="Nome" />
                                                                <dxe:ListBoxColumn Caption="Sigla" />
                                                            </Columns>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td style="padding-right: 5px" valign="bottom">
                                                        <dxe:ASPxComboBox ID="ddlMapa" runat="server" ClientInstanceName="ddlMapa" Width="100%">
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td class="style2" style="padding-right: 5px" valign="bottom">
                                                        <dxe:ASPxDateEdit ID="dteDe" runat="server" ClientInstanceName="dteDe" DisplayFormatString="dd/MM/yyyy"
                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="120px">
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                    <td class="style2" style="padding-right: 5px" valign="bottom">
                                                        <dxe:ASPxDateEdit ID="dteAte" runat="server" ClientInstanceName="dteAte" DisplayFormatString="dd/MM/yyyy"
                                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" Width="120px">
                                                        </dxe:ASPxDateEdit>
                                                    </td>
                                                    <td class="style2" style="padding-right: 5px" valign="bottom">
                                            <dxe:ASPxButton ID="ASPxButton11" runat="server"
                                                ImageSpacing="2px" Text="Selecionar" AutoPostBack="False" 
                                                onclick="ASPxButton11_Click1" Width="100%">
                                                <ClientSideEvents Click="function(s, e) 
{   
    var option = '';
	var nome = '';
	//debugger    
   if(ddlMapa.GetSelectedItem() != null)
   {
        e.processOnServer = false;
	    var option = ddlMapa.GetSelectedItem().value;
	    var nome = ddlMapa.GetSelectedItem().text;

	    var parametro = option + &quot;|&quot; + nome;
		    
	    if(validaData())
        {	        
			e.processOnServer = true;          
			return true;
        }
        else
        {
			e.processOnServer = false;
			return false;
        }            
   }
   else
   {
         window.top.mostraMensagem('Selecione um mapa estratégico', 'atencao', true, false, null);
        e.processOnServer = false;
        return false;
   }
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
                        <tr>
                            <td style="padding-left: 5px; padding-top: 5px; padding-bottom: 5px">
                                <dxxr:ReportToolbar ID="ReportToolbar1" runat="server"
                                    ReportViewer="<%# ReportViewer1 %>" ShowDefaultButtons="False" Width="153px"
                                    ClientInstanceName="ReportToolbar1" ReportViewerID="ReportViewer1" 
                                    ItemSpacing="0px">
                                    <Paddings Padding="0px" />
                                    <Items>
                                        <dxxr:ReportToolbarLabel ItemKind="PageLabel" Text="Página"></dxxr:ReportToolbarLabel>
                                        <dxxr:ReportToolbarComboBox Width="65px" ItemKind="PageNumber">
                                        </dxxr:ReportToolbarComboBox>
                                        <dxxr:ReportToolbarLabel ItemKind="OfLabel"></dxxr:ReportToolbarLabel>
                                        <dxxr:ReportToolbarTextBox ItemKind="PageCount"></dxxr:ReportToolbarTextBox>
                                        <dxxr:ReportToolbarButton ItemKind="NextPage"></dxxr:ReportToolbarButton>
                                        <dxxr:ReportToolbarButton ItemKind="LastPage"></dxxr:ReportToolbarButton>
                                        <dxxr:ReportToolbarSeparator />
                                        <dxxr:ReportToolbarButton ItemKind="SaveToDisk" />
                                        <dxxr:ReportToolbarButton ItemKind="PrintReport" />
                                        <dxxr:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                            <Elements>
                                                <dxxr:ListElement Value="pdf" />
                                            </Elements>
                                        </dxxr:ReportToolbarComboBox>
                                    </Items>
                                    <Styles>
                                        <LabelStyle>
                                            <Margins MarginLeft="3px" MarginRight="3px" />
                                        </LabelStyle>
                                    </Styles>
                                </dxxr:ReportToolbar>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px">
                                            <table cellpadding="0" cellspacing="0" 
                                                style="width: 100%; padding-left: 5px; padding-right: 5px;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" 
                                                            style="border-style: none; width: 100%; height: 100%; " width="98%">
                                                            <tr>
                                                                <td style="vertical-align: middle; text-align: center; padding-top: 5px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="vertical-align: middle; ">
                                                                    <dxxr:ReportViewer ID="ReportViewer1" runat="server" AutoSize="False" 
                                                                        ClientInstanceName="ReportViewer1" Width="100%">
                                                                        <ClientSideEvents PageLoad="init"/>
                                                                        <Paddings PaddingBottom="0px" PaddingLeft="5px" PaddingRight="0px" 
                                                                            PaddingTop="0px" />
                                                                        <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                    </dxxr:ReportViewer>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                            </td>
                        </tr>
                    </table>
                    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                    </dxhf:ASPxHiddenField>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
