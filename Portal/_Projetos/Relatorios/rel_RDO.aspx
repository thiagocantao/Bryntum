<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="rel_RDO.aspx.cs" Inherits="_Projetos_Relatorios_rel_RDO" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
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
                                Text="Relatório Diário de Obras">
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
                            &nbsp;</td>
                        <td style="width: 100px; padding-right: 5px; padding-top: 5px;" valign="bottom" 
                            bgcolor="White">
                            &nbsp;</td>
                        <td bgcolor="White" valign="bottom">
                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                <ClientSideEvents BeginCallback="function(s, e) {
	
pcDados.Show();
}" EndCallback="function(s, e) {
	
pcDados.Hide();
}" />
                            </dxhf:ASPxHiddenField>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                                                 <table cellpadding="0" cellspacing="0" 
                                                     style="border-style: solid; border-width: thin; border-color: #000000; width: 100%; height: 100%; background-color: #DDDAD5;">
                                                     <tr>
                                                         <td style="vertical-align: middle; text-align: center">
                                                         </td>
                                                         <td style="vertical-align: middle; text-align: center">
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td ID="tdRelatorio" runat="server" 
                                                             style="vertical-align: middle; text-align: center">
                                                             &nbsp;</td>
                                                         <td style="vertical-align: middle; text-align: center">
                                                             <dxxr:ReportViewer ID="ReportViewer1" runat="server" AutoSize="False" 
                                                                 ClientInstanceName="ReportViewer1" Width="860px">
                                                                 <Paddings PaddingBottom="0px" PaddingLeft="5px" PaddingRight="0px" 
                                                                     PaddingTop="0px" />
                                                                 <Border BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                             </dxxr:ReportViewer>
                                                         </td>
                                                     </tr>
                                                 </table>
                                             </td>
        </tr>
    </table>
</asp:Content>

