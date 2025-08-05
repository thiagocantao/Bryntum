<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupRelStatusReport.aspx.cs"
    Inherits="_Projetos_DadosProjeto_popupRelStatusReport" %>

<%@ Register Assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dxxr" %>
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
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" OnCallback="pnCallback_Callback"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxxr:ReportToolbar runat="server" ShowDefaultButtons="False" ReportViewer="<%# rptViewer %>"
                                    ID="ReportToolbar1">
                                    <Items>
                                        <dxxr:ReportToolbarButton ItemKind="Search"></dxxr:ReportToolbarButton>
                                        <dxxr:ReportToolbarButton ItemKind="PrintReport" />
                                        <dxxr:ReportToolbarSeparator></dxxr:ReportToolbarSeparator>
                                        <dxxr:ReportToolbarSeparator></dxxr:ReportToolbarSeparator>
                                        <dxxr:ReportToolbarButton Enabled="False" ItemKind="FirstPage"></dxxr:ReportToolbarButton>
                                        <dxxr:ReportToolbarButton Enabled="False" ItemKind="PreviousPage"></dxxr:ReportToolbarButton>
                                        <dxxr:ReportToolbarLabel ItemKind="PageLabel" Text="P&#225;gina"></dxxr:ReportToolbarLabel>
                                        <dxxr:ReportToolbarComboBox Width="65px" ItemKind="PageNumber">
                                        </dxxr:ReportToolbarComboBox>
                                        <dxxr:ReportToolbarLabel ItemKind="OfLabel" Text="de"></dxxr:ReportToolbarLabel>
                                        <dxxr:ReportToolbarTextBox ItemKind="PageCount"></dxxr:ReportToolbarTextBox>
                                        <dxxr:ReportToolbarButton ItemKind="NextPage"></dxxr:ReportToolbarButton>
                                        <dxxr:ReportToolbarButton ItemKind="LastPage"></dxxr:ReportToolbarButton>
                                        <dxxr:ReportToolbarSeparator></dxxr:ReportToolbarSeparator>
                                        <dxxr:ReportToolbarButton Text="Publicar" ToolTip="Publicar Relat&#243;rio de Status"
                                            ImageUrl="~/imagens/publicar.PNG" Name="btnPublicar"></dxxr:ReportToolbarButton>
                                        <dxxr:ReportToolbarButton Name="btnCancelar" Text="Cancelar" ToolTip="Cancelar" />
                                    </Items>
                                    <ClientSideEvents ItemClick="function(s, e) {
	if (e.item.name == 'btnPublicar')
	{
		if (confirm('Deseja realmente publicar o relat&#243;rio?'))
	    {
			pnCallback.PerformCallback(&quot;&quot;);
		}
	}
    else if (e.item.name == 'btnCancelar')
	{
		if (confirm('Deseja realmente sair sem publicar?'))
	    {
			try{
                window.top.fechaModal();
            }
            catch(e){
                window.close();
            }
		}
	}
}"></ClientSideEvents>
                                    <Styles>
                                        <LabelStyle>
                                            <Margins MarginLeft="3px" MarginRight="3px"></Margins>
                                        </LabelStyle>
                                    </Styles>
                                </dxxr:ReportToolbar>
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_status == 'ok')
            window.top.mostraMensagem(s.cp_msg, 'sucesso', false, false, null);
        else
            window.top.mostraMensagem(s.cp_msg, 'erro', true, false, null);

    var varOpener = window.parent.myObject;
	window.top.retornoModal = 'S';
	window.top.fechaModal();
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
        <div>
            <table>
                <tr>
                    <td>
                        <div id="divRelatorio" runat="server">
                            <dxxr:ReportViewer ID="rptViewer" runat="server">
                                <ClientSideEvents PageLoad="init" />
                            </dxxr:ReportViewer>
                        </div>

                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
