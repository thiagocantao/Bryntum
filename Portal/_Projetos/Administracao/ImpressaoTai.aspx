<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImpressaoTai.aspx.cs" Inherits="_Projetos_Administracao_ImpressaoTai" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dxxr" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divToolBox" runat="server">
        <dxxr:ReportToolbar ID="ReportToolbar1" runat="server" ShowDefaultButtons="False"
            ReportViewer="<%# ReportViewer1 %>">
            <Items>
                <dxxr:ReportToolbarButton ItemKind="PrintReport" ToolTip="Imprimir"></dxxr:ReportToolbarButton>
                <dxxr:ReportToolbarButton ItemKind="PrintPage" ToolTip="Imprimir p&#225;gina atual">
                </dxxr:ReportToolbarButton>
                <dxxr:ReportToolbarSeparator></dxxr:ReportToolbarSeparator>
                <dxxr:ReportToolbarButton Enabled="False" ItemKind="FirstPage" ToolTip="Primeira p&#225;gina">
                </dxxr:ReportToolbarButton>
                <dxxr:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" ToolTip="P&#225;gina anterior">
                </dxxr:ReportToolbarButton>
                <dxxr:ReportToolbarLabel ItemKind="PageLabel" Text="P&#225;gina"></dxxr:ReportToolbarLabel>
                <dxxr:ReportToolbarComboBox Width="65px" ItemKind="PageNumber">
                </dxxr:ReportToolbarComboBox>
                <dxxr:ReportToolbarLabel ItemKind="OfLabel" Text="de"></dxxr:ReportToolbarLabel>
                <dxxr:ReportToolbarTextBox ItemKind="PageCount"></dxxr:ReportToolbarTextBox>
                <dxxr:ReportToolbarButton ItemKind="NextPage" ToolTip="Pr&#243;xima p&#225;gina">
                </dxxr:ReportToolbarButton>
                <dxxr:ReportToolbarButton ItemKind="LastPage" ToolTip="&#218;ltima p&#225;gina">
                </dxxr:ReportToolbarButton>
                <dxxr:ReportToolbarSeparator></dxxr:ReportToolbarSeparator>
                <dxxr:ReportToolbarButton ItemKind="SaveToDisk" ToolTip="Salvar"></dxxr:ReportToolbarButton>
                <dxxr:ReportToolbarComboBox Width="100px" ItemKind="SaveFormat">
                    <Elements>
                        <dxxr:ListElement Value="pdf"></dxxr:ListElement>
                        <dxxr:ListElement Value="xls"></dxxr:ListElement>
                        <dxxr:ListElement Value="xlsx"></dxxr:ListElement>
                        <dxxr:ListElement Value="mht"></dxxr:ListElement>
                        <dxxr:ListElement Value="html"></dxxr:ListElement>
                        <dxxr:ListElement Text="Texto" Value="txt"></dxxr:ListElement>
                        <dxxr:ListElement Value="csv"></dxxr:ListElement>
                        <dxxr:ListElement Text="Imagem" Value="png"></dxxr:ListElement>
                        <dxxr:ListElement Text="Xml" Value="xml"></dxxr:ListElement>
                    </Elements>
                </dxxr:ReportToolbarComboBox>
            </Items>
            <Styles>
                <LabelStyle>
                    <Margins MarginLeft="3px" MarginRight="3px"></Margins>
                </LabelStyle>
            </Styles>
        </dxxr:ReportToolbar>
    </div>
    <div id="divRelatorio" runat="server">
        <dxxr:ReportViewer ID="ReportViewer1" runat="server">
        </dxxr:ReportViewer>
    </div>
    </form>
</body>
</html>
