<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="frm_relAcompProcessosReproj.aspx.cs" Inherits="_Projetos_Relatorios_frm_relAcompProcessosReproj" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <dxxr:ReportViewer runat="server"  AutoSize="False" Width="745px" 
                    ID="ReportViewer1" BackColor="#DDDAD5">
        <Paddings Padding="3px" />
        <Border BorderStyle="Solid" BorderColor="Black" BorderWidth="1px">
        </Border>
    </dxxr:ReportViewer>
</asp:Content>

