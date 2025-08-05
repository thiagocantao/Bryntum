<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="relGestaoPBH.aspx.cs" Inherits="_Estrategias_Relatorios_relGestaoPBH" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url('../../imagens/titulo/back_Titulo_Desktop.gif');">
            <tr style="height: 26px;">
                <td align="left">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="padding-left: 10px">
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Gestão Financeira do PDTI"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 205px">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxComboBox runat="server" Height="22px" ClientInstanceName="ddlExporta"
                                                                    ID="ddlExporta" ClientVisible="False" Visible="False">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                pnImage.PerformCallback(s.GetValue());
	                                                                hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
                                                            }"></ClientSideEvents>
                                                                    <Paddings Padding="0px"></Paddings>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                            <td style="padding-left: 2px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="padding-right: 5px;">
                                                <dxcp:ASPxCallbackPanel runat="server"  
                                                    ClientInstanceName="pnImage" Width="23px" Height="22px" ID="pnImage0" OnCallback="pnImage_Callback">
                                                    <PanelCollection>
                                                        <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png"
                                                                OnClick="ImageButton1_Click" ToolTip="Exportar para arquivo Excel" />
                                                        </dxp:PanelContent>
                                                    </PanelCollection>
                                                </dxcp:ASPxCallbackPanel>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton runat="server" Text="Exportar" Height="22px"
                                                    ID="Aspxbutton2" OnClick="btnExcel_Click" ClientVisible="False">
                                                    <Paddings Padding="0px"></Paddings>
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td style="width: 20px">
                                &nbsp;
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
                                        <td style="padding-left: 5px; ">
                                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                Text="Descrição:">
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
                                                <dxwtl:ASPxTreeList ID="tlDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlDados"
                                                     KeyFieldName="Codigo" OnCustomCallback="tlDados_CustomCallback"
                                                    ParentFieldName="CodigoPai" Width="100%">
                                                    <Columns>
                                                        <dxwtl:TreeListTextColumn Caption="Descrição" FieldName="Descricao" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="220px">
                                                            <PropertiesTextEdit>
                                                                <Style >
                                                                    
                                                                </Style>
                                                            </PropertiesTextEdit>
                                                        </dxwtl:TreeListTextColumn>
                                                        <dxwtl:TreeListSpinEditColumn Caption="Valor PDTI" FieldName="ValorPrevistoPDTI"
                                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="c" NumberFormat="Custom">
                                                                <Style >
                                                                    
                                                                </Style>
                                                            </PropertiesSpinEdit>
                                                        </dxwtl:TreeListSpinEditColumn>
                                                        <dxwtl:TreeListSpinEditColumn Caption="Valor Autorizado CGTIC" FieldName="ValorAutorizado"
                                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="c" NumberFormat="Custom">
                                                                <Style >
                                                                    
                                                                </Style>
                                                            </PropertiesSpinEdit>
                                                        </dxwtl:TreeListSpinEditColumn>
                                                        <dxwtl:TreeListSpinEditColumn Caption="Valor Realizado" FieldName="ValorReal" ShowInCustomizationForm="True"
                                                            VisibleIndex="3">
                                                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="c" NumberFormat="Custom">
                                                                <Style >
                                                                    
                                                                </Style>
                                                            </PropertiesSpinEdit>
                                                        </dxwtl:TreeListSpinEditColumn>
                                                        <dxwtl:TreeListSpinEditColumn Caption="Diferença (CGTIC - PDTI)" FieldName="Diferenca"
                                                            ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="c" NumberFormat="Custom">
                                                                <Style >
                                                                    
                                                                </Style>
                                                            </PropertiesSpinEdit>
                                                        </dxwtl:TreeListSpinEditColumn>
                                                        <dxwtl:TreeListSpinEditColumn Caption="% Diferença (CGTIC - PDTI)" FieldName="PercDiferenca"
                                                            ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="p" NumberFormat="Custom">
                                                                <Style >
                                                                    
                                                                </Style>
                                                            </PropertiesSpinEdit>
                                                        </dxwtl:TreeListSpinEditColumn>
                                                        <dxwtl:TreeListSpinEditColumn Caption="% Realizado PDTI" FieldName="PercRealizadoPDTI"
                                                            ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="p" NumberFormat="Custom">
                                                                <Style >
                                                                    
                                                                </Style>
                                                            </PropertiesSpinEdit>
                                                        </dxwtl:TreeListSpinEditColumn>
                                                        <dxwtl:TreeListSpinEditColumn Caption="% Realizado CGTIC" FieldName="PercRealizadoCGTIC"
                                                            ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="p" NumberFormat="Custom">
                                                                <Style >
                                                                    
                                                                </Style>
                                                            </PropertiesSpinEdit>
                                                        </dxwtl:TreeListSpinEditColumn>
                                                        <dxwtl:TreeListSpinEditColumn Caption="Valor Restante PDTI" FieldName="ValorRestantePDTI"
                                                            ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="c" NumberFormat="Custom">
                                                                <Style >
                                                                    
                                                                </Style>
                                                            </PropertiesSpinEdit>
                                                        </dxwtl:TreeListSpinEditColumn>
                                                        <dxwtl:TreeListTextColumn Caption="CodigoPai" FieldName="CodigoPai" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="9">
                                                        </dxwtl:TreeListTextColumn>
                                                        <dxwtl:TreeListTextColumn Caption="Codigo" FieldName="Codigo" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="10">
                                                        </dxwtl:TreeListTextColumn>
                                                        <dxwtl:TreeListTextColumn Caption="Tipo" FieldName="Tipo" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="11">
                                                        </dxwtl:TreeListTextColumn>
                                                    </Columns>
                                                    <Settings VerticalScrollBarMode="Visible" />
                                                    <Styles>
                                                        <Cell >
                                                        </Cell>
                                                        <AlternatingNode Enabled="True">
                                                        </AlternatingNode>
                                                    </Styles>
                                                </dxwtl:ASPxTreeList>                                            
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
</asp:Content>
