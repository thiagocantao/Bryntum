<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupRelProgramas.aspx.cs"
    Inherits="_Projetos_DadosProjeto_popupRelProgramas" %>

<%@ Register Assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dxxr" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        body {
            overflow: hidden;
        }

            body > form {
                overflow: hidden;
            }

        .WaterMark {
            background-image: url('../../espacoCliente/imagem_fundo_capa.png');
            background-position: center center;
            background-repeat: no-repeat;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function sairSemPublicar() {
            try {
                window.top.fechaModal();
            }
            catch (e) {
                window.close();
            }
        }
        function publicarRelatorio() {
            pnCallback.PerformCallback('');
        }

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
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="divToolBox" runat="server">
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" OnCallback="pnCallback_Callback"
                    Width="100%">
                    <ClientSideEvents EndCallback="function(s, e) {
        if(s.cp_erro != '')
       {
               window.top.mostraMensagem(s.cp_erro, 'erro', true, false, null);
       }
       else
      {
               if(s.cp_msg != '')
               {
                        window.top.mostraMensagem(s.cp_msg, 'sucesso', false, false, null);
               }
     }
    var varOpener = window.parent.myObject;
    window.top.retornoModal = 'S';
    window.top.fechaModal();
}" />
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                            <dxxr:ReportToolbar ID="ReportToolbar1" runat="server" ReportViewer="<%# ReportViewer1 %>"
                                ShowDefaultButtons="False">
                                <Items>
                                    <dxxr:ReportToolbarButton ItemKind="Search" />
                                    <dxxr:ReportToolbarButton ItemKind="PrintReport" />
                                    <dxxr:ReportToolbarSeparator />
                                    <dxxr:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                                    <dxxr:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                                    <dxxr:ReportToolbarLabel ItemKind="PageLabel" Text="Página" />
                                    <dxxr:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                                    </dxxr:ReportToolbarComboBox>
                                    <dxxr:ReportToolbarLabel ItemKind="OfLabel" Text="de" />
                                    <dxxr:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                                    <dxxr:ReportToolbarButton ItemKind="NextPage" />
                                    <dxxr:ReportToolbarButton ItemKind="LastPage" />
                                    <dxxr:ReportToolbarSeparator />
                                    <dxxr:ReportToolbarButton ImageUrl="~/imagens/publicar.PNG" Name="btnPublicar" Text="Publicar"
                                        ToolTip="Publicar" />
                                    <dxxr:ReportToolbarButton Name="btnCancelar" Text="Cancelar" ToolTip="Cancelar" />
                                </Items>
                                <ClientSideEvents ItemClick="function(s, e) {
	if (e.item.name == 'btnPublicar')
	{
                              window.top.mostraMensagem(traducao.popupRelProgramas_deseja_realmente_publicar_o_relat_rio_, 'confirmacao', true, true,publicarRelatorio);
	}
                else if (e.item.name == 'btnCancelar')
	{
		window.top.mostraMensagem('Sair sem publicar', 'confirmacao', true, true,sairSemPublicar);
	}
}" />
                                <Styles>
                                    <LabelStyle>
                                        <Margins MarginLeft="3px" MarginRight="3px" />
                                    </LabelStyle>
                                </Styles>
                            </dxxr:ReportToolbar>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
            </div>
            <div id="divRelatorio" runat="server" style="height: 100%;">
                <dxxr:ReportViewer ID="ReportViewer1" runat="server">
                    <ClientSideEvents PageLoad="init" />
                </dxxr:ReportViewer>
            </div>
        </div>
    </form>
</body>
</html>
