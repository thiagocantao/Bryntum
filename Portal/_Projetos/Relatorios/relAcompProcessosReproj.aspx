<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relAcompProcessosReproj.aspx.cs" Inherits="_Projetos_Relatorios_relAcompProcessosReproj" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px" valign="middle">
                <table>
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" style="height: 19px" valign="middle">
                            &nbsp; &nbsp;
                            <dxe:ASPxLabel ID="lblTitulo" runat="server" Font-Bold="True"
                                Text="Relatório de Acompanhamento dos Processos Reprojetados">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="padding-left: 5px; padding-bottom: 5px" bgcolor="White">
                <table>
                    <tr>
                        <td align="left" style="width: 320px; padding-right: 5px; padding-top: 5px;" valign="bottom"
                            bgcolor="White">
                            <table align="left" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                            Text="Unidade:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxComboBox ID="ddlUnidade" runat="server" ClientInstanceName="ddlUnidade"
                                             Width="100%" AutoPostBack="True">
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 100px; padding-right: 5px; padding-top: 5px;" valign="bottom" bgcolor="White">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                            Text="Ano:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxComboBox ID="ddlAno" runat="server" ClientInstanceName="ddlAno"
                                            Width="100%">
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td bgcolor="White" valign="bottom">
                            <dxxr:ReportToolbar ID="ReportToolbar1" runat="server" ShowDefaultButtons="False"
                                 ReportViewerID="ReportViewer1" ItemSpacing="0px"
                                ClientInstanceName="rtb">
                                <Paddings Padding="0px" />
                                <Items>
                                    <dxxr:ReportToolbarButton ItemKind="Search" ToolTip="Busca no documento" />
                                    <dxxr:ReportToolbarSeparator />
                                    <dxxr:ReportToolbarButton ItemKind="PrintReport" Text="Imprimir" ToolTip="Imprimir" />
                                    <dxxr:ReportToolbarButton ItemKind="PrintPage" ToolTip="Imprimir página" />
                                    <dxxr:ReportToolbarSeparator />
                                    <dxxr:ReportToolbarButton ItemKind="FirstPage" Enabled="False" ToolTip="Primeira página" />
                                    <dxxr:ReportToolbarButton ItemKind="PreviousPage" Enabled="False" ToolTip="Página Anterior" />
                                    <dxxr:ReportToolbarLabel ItemKind="PageLabel" Text="Página" />
                                    <dxxr:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                                    </dxxr:ReportToolbarComboBox>
                                    <dxxr:ReportToolbarLabel ItemKind="OfLabel" Text="de" />
                                    <dxxr:ReportToolbarTextBox ItemKind="PageCount" />
                                    <dxxr:ReportToolbarButton ItemKind="NextPage" ToolTip="Próxima Página" />
                                    <dxxr:ReportToolbarButton ItemKind="LastPage" ToolTip="Última Página" />
                                    <dxxr:ReportToolbarSeparator />
                                    <dxxr:ReportToolbarButton ItemKind="SaveToDisk" ToolTip="Salvar no disco" />
                                    <dxxr:ReportToolbarButton ItemKind="SaveToWindow" ToolTip="Salvar em Nova Janela" />
                                    <dxxr:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                        <Elements>
                                            <dxxr:ListElement Text="RTF" Value="rtf" />
                                            <dxxr:ListElement Value="pdf" Text="PDF" />
                                        </Elements>
                                    </dxxr:ReportToolbarComboBox>
                                </Items>
                                <Styles>
                                    <LabelStyle>
                                        <Margins MarginLeft="3px" MarginRight="3px" />
                                    </LabelStyle>
                                </Styles>
                            </dxxr:ReportToolbar>
                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                <ClientSideEvents BeginCallback="function(s, e) {
	
pcDados.Show();
}" EndCallback="function(s, e) {
	
pcDados.Hide();
}" />
                            </dxhf:ASPxHiddenField>
                            <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" CloseAction="CloseButton"
                                Modal="True" PopupAnimationType="Fade" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowHeader="False" Width="120px">
                                <ModalBackgroundStyle BackColor="Transparent" Opacity="6">
                                </ModalBackgroundStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                                        <table class="headerGrid">
                                            <tr>
                                                <td align="center" valign="bottom">
                                                    &nbsp;<table cellpadding="0" cellspacing="0" class="headerGrid">
                                                        <tr>
                                                            <td align="right">
                                                                <img alt="carregando" src="../../imagens/iconeCarregando.png" />
                                                            </td>
                                                            <td style="width: 80px; padding-left: 0px">
                                                                <dxe:ASPxLabel ID="lblCarregando0" runat="server" Text="Carregando...">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="border-style: solid; border-width: thin;
                    border-color: #000000; width: 100%; height: 100%; background-color: #DDDAD5;">
                    <tr>
                        <td style="vertical-align: middle; text-align: center">
                        </td>
                        <td style="vertical-align: middle; text-align: center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td id="tdRelatorio" runat="server" style="vertical-align: middle; text-align: center">
                            &nbsp;
                        </td>
                        <td style="vertical-align: middle; text-align: center">
                            <dxxr:ReportViewer ID="ReportViewer1" runat="server" AutoSize="False" ClientInstanceName="ReportViewer1"
                                Width="860px">
                                <Paddings PaddingBottom="0px" PaddingLeft="5px" PaddingRight="0px" PaddingTop="0px" />
                                <Border BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                            </dxxr:ReportViewer>
                        </td>
                        
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
