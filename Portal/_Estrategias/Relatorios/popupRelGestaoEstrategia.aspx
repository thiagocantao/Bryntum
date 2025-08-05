<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupRelGestaoEstrategia.aspx.cs" Inherits="_Estrategias_Relatorios_popupRelGestaoEstrategia" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Gestão da Estrategia</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0"
            width="100%">
            <tr style="height:26px">
                <td valign="top" align="right">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
                        width: 100%">
                        <tr>
                            <td align="right" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); height: 26px">
                                <table>
                                    <tr>
                                        <td align="left" valign="middle" style="padding-left: 10px">
                                           <dxe:ASPxLabel ID="lblTitulo" runat="server" ClientInstanceName="lblTitulo" Font-Bold="True"
                         Text="Relatório de Gestão da Estratégia">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="middle">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblTitulo" Font-Bold="True"
                         Text="Mapa:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" style="width: 450px" valign="middle">
                                            <dxe:ASPxComboBox ID="ddlMapa" runat="server" ClientInstanceName="ddlMapa" 
                                                Width="445px" ValueType="System.String" AutoPostBack="True">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	//hfGeral.Set(&quot;CodigoMapa&quot;,s.GetSelectedItem().value);
 
}" />
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td align="right" style="width: 85px; padding-right: 5px;" valign="middle">
                                            <dxe:ASPxButton ID="btnSelecionar" runat="server" 
                                                Text="Selecionar" Width="76px" ClientInstanceName="btnSelecionar" ClientEnabled="False">
                                                <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                    PaddingTop="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height:26px">
                <td valign="middle" align="left" style="padding-left: 10px">
                    <div style="height: <%=alturaDivGrid %>">
                        &nbsp;<table>
                            <tr>
                                <td style="width: 354px">
                        <dx:ReportToolbar ID="ReportToolbar1" runat="server" ShowDefaultButtons="False" ReportViewer="<%# ReportViewer1 %>" Width="62%">
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
                                <td>
                                            <dxe:ASPxImage ID="imgRelatorio" runat="server" ImageUrl="~/imagens/menuExportacao/iconoPDF.png" Cursor="pointer" ClientInstanceName="imgRelatorio">
                                                <ClientSideEvents Click="function(s, e) {
	window.top.showModal(&quot;relGestaoEstrategia.aspx?M=N&quot;, 'Gestão Estratégica', screen.width - 60, screen.height - 260, '', null);
}" />
                                            </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                        <dx:ReportViewer ID="ReportViewer1" runat="server" ClientInstanceName="ReportViewer1" AutoSize="False" Height="99%" Width="97%">
                        <Paddings PaddingBottom="0px" PaddingLeft="5px" PaddingRight="0px" PaddingTop="0px" />
                    </dx:ReportViewer>
                    </div>
                </td>
            </tr>
        </table>
        </div>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    </div>
    </form>
</body>
</html>
