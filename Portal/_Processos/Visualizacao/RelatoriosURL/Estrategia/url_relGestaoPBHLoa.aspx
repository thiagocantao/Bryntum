<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_relGestaoPBHLoa.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Estrategia_url_relGestaoPBHLoa"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url('../../../../imagens/titulo/back_Titulo_Desktop.gif');">
            <tr style="height: 26px;">
                <td align="left">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="padding-left: 5px">
                                &nbsp;</td>
                            <td style="width: 120px">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="padding-right: 5px;">
                                                <span>Ano</span>:
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox ID="ddlAno" runat="server" ClientInstanceName="ddlAno"
                                                    ValueType="System.String" Width="80px" AutoPostBack="True">
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td style="width: 20px; padding-right: 5px;">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png"
                                    OnClick="ImageButton1_Click" ToolTip="Exportar para arquivo Excel" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallbackDados" runat="server" Width="100%" ClientInstanceName="pnCallbackDados"
                    OnCallback="pnCallbackDados_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td style="padding-left: 5px">
                                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                                Text="Descrição">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 5px; padding-bottom: 5px">
                                            <dxe:ASPxTextBox ID="txtDescricao" runat="server" ClientInstanceName="txtDescricao"
                                                 Width="500px">
                                                <ClientSideEvents KeyDown="function(s, e) {
	novaDescricao();
}" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;">
                                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                            </dxhf:ASPxHiddenField>
                                            <dxwtle:ASPxTreeListExporter ID="ASPxTreeListExporter1" runat="server">
                                                <Styles>
                                                    <Header >
                                                    </Header>
                                                    <Cell >
                                                    </Cell>
                                                </Styles>
                                            </dxwtle:ASPxTreeListExporter>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxwtl:ASPxTreeList ID="tlDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlDados"
                                                             KeyFieldName="Codigo" OnCustomCallback="tlDados_CustomCallback"
                                                            ParentFieldName="CodigoPai" Width="100%">
                                                            <Columns>
                                                                <dxwtl:TreeListTextColumn Caption="CodigoPai" FieldName="CodigoPai" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="0">
                                                                </dxwtl:TreeListTextColumn>
                                                                <dxwtl:TreeListTextColumn Caption="Codigo" FieldName="Codigo" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="1">
                                                                </dxwtl:TreeListTextColumn>
                                                                <dxwtl:TreeListTextColumn Caption="Descrição" FieldName="Descricao" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2">
                                                                </dxwtl:TreeListTextColumn>
                                                                <dxwtl:TreeListSpinEditColumn Caption="Valor Previsto" FieldName="ValorPrevisto"
                                                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="120px">
                                                                    <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="c" NumberFormat="Currency">
                                                                    </PropertiesSpinEdit>
                                                                </dxwtl:TreeListSpinEditColumn>
                                                                <dxwtl:TreeListSpinEditColumn Caption="Valor Realizado" FieldName="ValorReal" ShowInCustomizationForm="True"
                                                                    VisibleIndex="4" Width="120px">
                                                                    <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="c" NumberFormat="Currency">
                                                                    </PropertiesSpinEdit>
                                                                </dxwtl:TreeListSpinEditColumn>
                                                                <dxwtl:TreeListSpinEditColumn Caption="Percentual Realizado" FieldName="PercentualReal"
                                                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="120px">
                                                                    <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="{0}%" NumberFormat="Percent">
                                                                    </PropertiesSpinEdit>
                                                                </dxwtl:TreeListSpinEditColumn>
                                                                <dxwtl:TreeListSpinEditColumn Caption="Saldo" FieldName="Saldo" ShowInCustomizationForm="True"
                                                                    VisibleIndex="6" Width="120px">
                                                                    <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="c" NumberFormat="Custom">
                                                                    </PropertiesSpinEdit>
                                                                </dxwtl:TreeListSpinEditColumn>
                                                            </Columns>
                                                            <Settings VerticalScrollBarMode="Visible" />
                                                            <Styles>
                                                                <AlternatingNode BackColor="#EBEBEB" Enabled="True">
                                                                </AlternatingNode>
                                                            </Styles>
                                                        </dxwtl:ASPxTreeList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
