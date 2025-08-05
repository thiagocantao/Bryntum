<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="relProjetosObjetivos.aspx.cs" Inherits="_Portfolios_Relatorios_relProjetosObjetivos" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>
<asp:Content id="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0"
            width="100%">
            <tr style="height:26px">
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
                        width: 100%">
                        <tr>
                            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); height: 26px" valign="middle">
                                &nbsp;<dxe:ASPxLabel ID="lblTitulo" runat="server" ClientInstanceName="lblTitulo" Font-Bold="True"
                         Text="Propostas e Projetos">
                                            </dxe:ASPxLabel>
                            </td>
                            <td align="right" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); height: 26px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblTitulo" Font-Bold="False"
                         Text="Unidade:" Height="16px">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                                width: 156px; height: 26px" valign="middle">
                                <dxe:ASPxComboBox ID="ddlUnidade" runat="server" ClientInstanceName="ddlUnidade"
                                                Width="160px" ValueType="System.String">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	hfGeral.Set(&quot;CodigoMapa&quot;,s.GetSelectedItem().value);
}" />
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                                width: 10px; height: 26px">
                            </td>
                            <td align="right" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                                width: 40px; height: 26px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel123" runat="server" ClientInstanceName="lblTitulo" Font-Bold="False"
                         Text="Status:" Height="16px">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                                width: 195px; height: 26px" valign="middle">
                                <dxe:ASPxComboBox ID="ddlStatus" runat="server" ClientInstanceName="ddlStatus"
                                                Width="285px" ValueType="System.String">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	hfGeral.Set(&quot;CodigoMapa&quot;,s.GetSelectedItem().value);
}" />
                                </dxe:ASPxComboBox>
                            </td>
                            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                                width: 10px; height: 26px">
                            </td>
                            <td align="left" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                                width: 85px; height: 26px" valign="middle">
                                            <dxe:ASPxButton ID="ASPxButton1" runat="server" 
                                                Text="Selecionar">
                                                <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                    PaddingTop="0px" />
                                            </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height:26px">
                <td valign="top">
                </td>
            </tr>
            <tr style="height:26px">
                <td valign="middle" align="left" style="padding-left: 10px">
                   
                        <table>
                            <tr>
                                <td style="width: 354px">
                        <dxxr:ReportToolbar ID="ReportToolbar1" runat="server" ShowDefaultButtons="False" ReportViewer="<%# ReportViewer1 %>" Width="62%" >
                            <Items>
                                <dxxr:ReportToolbarButton ItemKind="Search" />
                                <dxxr:ReportToolbarSeparator />
                                <dxxr:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                                <dxxr:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                                <dxxr:ReportToolbarLabel ItemKind="PageLabel" Text="P&#225;gina" />
                                <dxxr:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                                </dxxr:ReportToolbarComboBox>
                                <dxxr:ReportToolbarLabel ItemKind="OfLabel" Text="de" />
                                <dxxr:ReportToolbarTextBox ItemKind="PageCount" />
                                <dxxr:ReportToolbarButton ItemKind="NextPage" />
                                <dxxr:ReportToolbarButton ItemKind="LastPage" />
                            </Items>
                            <Styles>
                                <LabelStyle>
                                    <Margins MarginLeft="3px" MarginRight="3px" />
                                </LabelStyle>
                            </Styles>
                        </dxxr:ReportToolbar>
                                </td>
                                <td>
                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/menuExportacao/iconoPDF.png" Cursor="pointer">
                                                <ClientSideEvents Click="function(s, e) {
var cun=hfGeral.Get(&quot;CodigoUnidade&quot;);
var sta=hfGeral.Get(&quot;CodigoStatus&quot;);
window.top.showModal(&quot;popupRelObjetivosProjetos.aspx?CUN=&quot; + cun + &quot;&amp;STA=&quot; + sta, '', screen.width - 60, screen.height - 260, '', null);
}" />
                                            </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                         <div style="height: <%=alturaDivGrid %>;overflow:auto">
                        <dxxr:ReportViewer ID="ReportViewer1" runat="server" ClientInstanceName="ReportViewer1" AutoSize="False" Height="90%" Width="97%">
                        <Paddings PaddingBottom="0px" PaddingLeft="5px" PaddingRight="0px" PaddingTop="0px" />
                    </dxxr:ReportViewer>
                    </div>
                </td>
            </tr>
        </table>
        </div>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>


</asp:Content>