<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfRenderizaFormulario_Impressao.aspx.cs"
    Inherits="wfRenderizaFormulario_Impressao" %>

<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
    function init(s) {
            var isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);
            var createFrameElement = s.printHelper.createFrameElement;

            s.printHelper.createFrameElement = function (name) {
                var frameElement = createFrameElement.call(this, name);
                if(isChrome) {
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
<body>
    <form id="form1" runat="server">
        <div id="divToolBox" runat="server">
            <dx:reporttoolbar id="ReportToolbar1" runat="server" showdefaultbuttons="False"
                reportviewer="<%# ReportViewer1 %>"><Items>
                    <dx:ReportToolbarButton Name="btnOrientacao" ToolTip="Alternar a orientação do documento entre retrato e paisagem" ImageUrl="~/imagens/icone_retrato_paisagem_transparente.png"></dx:ReportToolbarButton>
<dx:ReportToolbarButton ItemKind="PrintReport" ToolTip="Imprimir"></dx:ReportToolbarButton>
<dx:ReportToolbarButton ItemKind="PrintPage" ToolTip="Imprimir p&#225;gina atual"></dx:ReportToolbarButton>
<dx:ReportToolbarSeparator></dx:ReportToolbarSeparator>
<dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" ToolTip="Primeira p&#225;gina"></dx:ReportToolbarButton>
<dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" ToolTip="P&#225;gina anterior"></dx:ReportToolbarButton>
<dx:ReportToolbarLabel ItemKind="PageLabel" Text="P&#225;gina"></dx:ReportToolbarLabel>
<dx:ReportToolbarComboBox Width="65px" ItemKind="PageNumber"></dx:ReportToolbarComboBox>
<dx:ReportToolbarLabel ItemKind="OfLabel" Text="de"></dx:ReportToolbarLabel>
<dx:ReportToolbarTextBox ItemKind="PageCount"></dx:ReportToolbarTextBox>
<dx:ReportToolbarButton ItemKind="NextPage" ToolTip="Pr&#243;xima p&#225;gina"></dx:ReportToolbarButton>
<dx:ReportToolbarButton ItemKind="LastPage" ToolTip="&#218;ltima p&#225;gina"></dx:ReportToolbarButton>
<dx:ReportToolbarSeparator></dx:ReportToolbarSeparator>
<dx:ReportToolbarButton ItemKind="SaveToDisk" ToolTip="Salvar"></dx:ReportToolbarButton>
<dx:ReportToolbarComboBox Width="100px" ItemKind="SaveFormat"><Elements>
<dx:ListElement Value="pdf"></dx:ListElement>
<dx:ListElement Value="xls"></dx:ListElement>
<dx:ListElement Value="xlsx"></dx:ListElement>
<dx:ListElement Value="mht"></dx:ListElement>
<dx:ListElement Value="html"></dx:ListElement>
<dx:ListElement Text="Texto" Value="txt"></dx:ListElement>
<dx:ListElement Value="csv"></dx:ListElement>
<dx:ListElement Text="Imagem" Value="png"></dx:ListElement>
<dx:ListElement Text="Xml" Value="xml"></dx:ListElement>
    <dx:ListElement Text="Rtf" Value="rtf" />
</Elements>
</dx:ReportToolbarComboBox>
</Items>

<Styles>
<LabelStyle>
<Margins MarginLeft="3px" MarginRight="3px"></Margins>
</LabelStyle>
</Styles>

<ClientSideEvents ItemClick="function(s, e) {
    if (e.item.name == 'SaveToDisk' || e.item.name == 'SaveToWindow')
    {
        // ACG: se incluir/excluir objetos na barra do relatorio, o componente abaixo deve ser renomeado
        if (ReportToolbar1_Menu_ITCNT11_SaveFormat_DDD_L.GetSelectedValues()[0]=='xml')
            __doPostBack('', 'xml')
	}
     if (e.item.name == 'btnOrientacao')
     {
        setTimeout(function(){ var variavel; }, 2000); 
         if(hfSessao.Get('orientacao') == 'paisagem')
          {
              hfSessao.Set('orientacao', 'retrato');
          }
          else
          {
              hfSessao.Set('orientacao', 'paisagem');
          }          
          ReportViewer1.Refresh();
     } 
}"></ClientSideEvents>
</dx:reporttoolbar>

                        <dxhf:ASPxHiddenField ID="hfSessao" runat="server" 
                            ClientInstanceName="hfSessao">
                        </dxhf:ASPxHiddenField>
         </div>
         <div id="divRelatorio" runat="server">
             <dx:reportviewer id="ReportViewer1" ClientInstanceName="ReportViewer1" runat="server">
                 <ClientSideEvents PageLoad="init" />
             </dx:reportviewer>
         </div>
        <script type="text/javascript">
document.getElementById('divRelatorio').style.height = (Math.max(0, document.documentElement.clientHeight) - 75) + 'px';
        </script>
    </form>
</body>
</html>
