<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanilhaCTC.aspx.cs" Inherits="_Projetos_DadosProjeto_PlanilhaCTC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Alertas</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    
    
    
<script language="javascript" type="text/javascript">
    function abreAlteracoes(codigoMestre) {
        pcAlteracoes.SetHeaderText('LOG de alterações para o C. Mestre "' + codigoMestre + '"');
        hfCodigoMestre.Set("Valor", codigoMestre);
        gvAlteracoes.PerformCallback(codigoMestre);
    }
</script>
    <style type="text/css">
        .style1
        {
            width: 5px;
            height: 10px;
        }
        .style2
        {
            height: 10px;
        }
        .style3
        {
            width: 100%;
        }
        .style6
        {
            width: 110px;
        }
        .style7
        {
            width: 100px;
        }
        .style8
        {
            width: 125px;
        }
        .style10
        {
            width: 120px;
        }
    </style>
    </head>
<body style="margin:0">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 10px; height: 10px">
                </td>
                <td style="height: 10px;">

 <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="gvExporter" 
                        OnRenderBrick="gvExporter_RenderBrick"></dxwgv:ASPxGridViewExporter>

                    </td>
                <td style="width: 10px; height: 10px;">
                </td>
            </tr>
            <tr>
                <td style="width: 5px">
                    &nbsp;</td>
                <td>
                <dxe:ASPxLabel runat="server" Text="Previsão Orçamentária:" 
                        ID="ASPxLabel1"></dxe:ASPxLabel>


                </td>
                <td style="width: 5px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 5px">
                    &nbsp;</td>
                <td>
                    <dxe:ASPxComboBox ID="ddlPrevisao" runat="server" 
                        ClientInstanceName="ddlPrevisao"  
                        Width="500px">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback();
}" />
                    </dxe:ASPxComboBox>
                </td>
                <td style="width: 5px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                </td>
                <td class="style2">
                </td>
                <td class="style1">
                </td>
            </tr>
            <tr>
                <td style="width: 5px">
                </td>
                <td>
                <!-- PANELCALLBACK: pnCallback -->
                    <dxcp:aspxcallbackpanel id="pnCallback" runat="server" width="100%" 
                        clientinstancename="pnCallback"><PanelCollection>
<dxp:PanelContent runat="server"><!-- ASPxGRIDVIEW: gvDados -->
    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
        KeyFieldName="CodigoMestre" AutoGenerateColumns="False" Width="100%" 
         ID="gvDados" 
        OnCommandButtonInitialize="gvDados_CommandButtonInitialize" 
        OnCellEditorInitialize="gvDados_CellEditorInitialize" 
        OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" 
        OnRowUpdating="gvDados_RowUpdating" 
        OnSummaryDisplayText="gvDados_SummaryDisplayText">
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="60px" 
        Caption=" " VisibleIndex="0" ShowEditButton="true">

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
    <HeaderTemplate>
        &nbsp;
        <asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="~/imagens/botoes/btnExcel.png" onclick="ImageButton1_Click" 
            ToolTip="Exportar para excel"  />
    </HeaderTemplate>
    <FooterTemplate>
        Total:
    </FooterTemplate>
</dxwgv:GridViewCommandColumn>
    <dxwgv:GridViewDataTextColumn Caption="C. Mestre" FieldName="CodigoMestre" 
        ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="True" 
        Width="80px">
        <PropertiesTextEdit>
            <Style HorizontalAlign="Center">
            </Style>
        </PropertiesTextEdit>
        <EditFormSettings Visible="False" />        
        <EditCellStyle HorizontalAlign="Center">
        </EditCellStyle>
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Descrição" 
        ShowInCustomizationForm="True" VisibleIndex="2" FieldName="Descricao">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataSpinEditColumn Caption="Budget Projetado" 
        FieldName="BudgetProjetado" ReadOnly="True" ShowInCustomizationForm="True" 
        VisibleIndex="3" Width="110px">
        <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="{0:n2}" 
            NumberFormat="Custom">
            <SpinButtons ShowIncrementButtons="False">
            </SpinButtons>
        </PropertiesSpinEdit>
        <EditFormSettings Visible="False" />
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxwgv:GridViewDataSpinEditColumn>
    <dxwgv:GridViewDataSpinEditColumn Caption="Actual &lt;br&gt;(Despesa Incorrida)" 
        FieldName="CustoIncorrido" ReadOnly="True" ShowInCustomizationForm="True" 
        VisibleIndex="4" Width="110px">
        <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="{0:n2}" 
            NumberFormat="Custom">
            <SpinButtons ShowIncrementButtons="False">
            </SpinButtons>
        </PropertiesSpinEdit>
        <EditFormSettings Visible="False" />
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxwgv:GridViewDataSpinEditColumn>
    <dxwgv:GridViewDataSpinEditColumn Caption="Cost To &lt;br&gt;Complete" 
        FieldName="ValorCTC" ShowInCustomizationForm="True" VisibleIndex="5" 
        Width="100px">
        <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatInEditMode="True" 
            DisplayFormatString="{0:n2}" NumberFormat="Custom" Width="100%">
            <SpinButtons ShowIncrementButtons="False">
            </SpinButtons>
            <Style  HorizontalAlign="Right">
            </Style>
        </PropertiesSpinEdit>
        <EditFormSettings Visible="False" />
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxwgv:GridViewDataSpinEditColumn>
    <dxwgv:GridViewDataSpinEditColumn Caption="Forecast &lt;br&gt;(Previsão Revisada)" 
        FieldName="PrevisaoRevisada" ReadOnly="True" ShowInCustomizationForm="True" 
        VisibleIndex="6" Width="125px">
        <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="{0:n2}" 
            NumberFormat="Custom">
            <SpinButtons ShowIncrementButtons="False">
            </SpinButtons>
        </PropertiesSpinEdit>
        <EditFormSettings Visible="False" />
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxwgv:GridViewDataSpinEditColumn>
    <dxwgv:GridViewDataSpinEditColumn Caption="Variação &lt;br&gt;(Resultado)" 
        FieldName="Variacao" ReadOnly="True" ShowInCustomizationForm="True" 
        VisibleIndex="7" Width="100px">
        <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="{0:n2}" 
            NumberFormat="Custom">
            <SpinButtons ShowIncrementButtons="False">
            </SpinButtons>
        </PropertiesSpinEdit>
        <EditFormSettings Visible="False" />
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxwgv:GridViewDataSpinEditColumn>
    <dxwgv:GridViewDataSpinEditColumn Caption="Saldo a &lt;br&gt;Contratar" 
        FieldName="SaldoaContratar" ShowInCustomizationForm="True" VisibleIndex="8" 
        Width="100px">
        <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatInEditMode="True" 
            DisplayFormatString="{0:n2}" NumberFormat="Custom" Width="100%">
            <SpinButtons ShowIncrementButtons="False">
            </SpinButtons>
            <Style  HorizontalAlign="Right">
            </Style>
        </PropertiesSpinEdit>
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxwgv:GridViewDataSpinEditColumn>
    <dxwgv:GridViewDataSpinEditColumn Caption="Avanço &lt;br&gt;Financeiro &lt;br&gt;(Base no Forecast)" 
        FieldName="AvancoFinanceiro" ReadOnly="True" ShowInCustomizationForm="True" 
        VisibleIndex="9" Width="120px">
        <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="{0:p2}" 
            NumberFormat="Custom">
            <SpinButtons ShowIncrementButtons="False">
            </SpinButtons>
        </PropertiesSpinEdit>
        <EditFormSettings Visible="False" />
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxwgv:GridViewDataSpinEditColumn>
    <dxwgv:GridViewDataTextColumn FieldName="ValorCTCAlterado" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

        <SettingsEditing Mode="Inline" />

        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="420" 
            ShowFooter="True" ShowTitlePanel="True" />

<SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar" ></SettingsText>
        <Styles>
            <Header Wrap="True">
            </Header>
            <Footer Font-Bold="True">
            </Footer>
        </Styles>
        <Templates>
            <TitlePanel>
                <table cellpadding="0" cellspacing="0" class="style3">
                    <tr>
                        <td align="left">
                            R$ x 1.000</td>
                        <td align="center" class="style6">
                            A</td>
                        <td align="center" class="style6">
                            B</td>
                        <td align="center" class="style7">
                            C</td>
                        <td align="center" class="style8">
                            D = (B + C)</td>
                        <td align="center" class="style7">
                            E = (A - D)</td>
                        <td align="center" class="style7">
                            &nbsp;</td>
                        <td align="center" class="style10" style="padding-right: 16px">
                            B / D</td>
                    </tr>
                </table>
            </TitlePanel>
        </Templates>
</dxwgv:ASPxGridView>
 <!-- PANEL CONTROL : pcDados -->
 </dxp:PanelContent>
</PanelCollection>

</dxcp:aspxcallbackpanel>
                </td>
                <td style="width: 5px">
                </td>
            </tr>
        </table>
    
    
    
    
    </div>

 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" 
        HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" 
        Width="270px"  ID="pcUsuarioIncluido">
     <ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>


























 </td></tr><tr><td style="HEIGHT: 10px">&nbsp;</td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>


























 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

    <dxpc:ASPxPopupControl ID="pcAlteracoes" runat="server" 
        ClientInstanceName="pcAlteracoes"  
        HeaderText="" Width="950px" Modal="True" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table cellpadding="0" cellspacing="0" class="style3">
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="gvAlteracoes" runat="server" 
                    AutoGenerateColumns="False" ClientInstanceName="gvAlteracoes" 
                     KeyFieldName="idLogAlteracao" 
                    Width="100%">
                    <ClientSideEvents EndCallback="function(s, e) {
	pcAlteracoes.Show();
}" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="Data " FieldName="DataAlteracao" 
                            ShowInCustomizationForm="True" VisibleIndex="3" Width="85px">
                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Usuário Alteração" 
                            FieldName="NomeUsuario" ShowInCustomizationForm="True" VisibleIndex="4">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataSpinEditColumn Caption="Cost To &lt;br&gt;Complete (Antes)" 
                            FieldName="ValorCTCAntes" ShowInCustomizationForm="True" VisibleIndex="5" 
                            Width="125px">
                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatInEditMode="True" 
                                DisplayFormatString="{0:n2}" NumberFormat="Custom" Width="100%">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                                <Style  HorizontalAlign="Right">
                                </Style>
                            </PropertiesSpinEdit>
                            <EditFormSettings Visible="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataSpinEditColumn>
                        <dxwgv:GridViewDataSpinEditColumn Caption="Cost To &lt;br&gt;Complete (Depois)" 
                            FieldName="ValorCTCDepois" ReadOnly="True" ShowInCustomizationForm="True" 
                            VisibleIndex="6" Width="125px">
                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="{0:n2}" 
                                NumberFormat="Custom">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                                <Style  HorizontalAlign="Right">
                                </Style>
                            </PropertiesSpinEdit>
                            <EditFormSettings Visible="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataSpinEditColumn>
                        <dxwgv:GridViewDataSpinEditColumn Caption="Saldo a &lt;br&gt;Contratar (Antes)" 
                            FieldName="SaldoContratualAntes" ShowInCustomizationForm="True" 
                            VisibleIndex="7" Width="125px">
                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="{0:n2}" 
                                NumberFormat="Custom">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                                <Style  HorizontalAlign="Right">
                                </Style>
                            </PropertiesSpinEdit>
                            <EditFormSettings Visible="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataSpinEditColumn>
                        <dxwgv:GridViewDataSpinEditColumn Caption="Saldo a &lt;br&gt;Contratar (Depois)" 
                            FieldName="SaldoContratualDepois" ShowInCustomizationForm="True" 
                            VisibleIndex="8" Width="125px">
                            <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatInEditMode="True" 
                                DisplayFormatString="{0:n2}" NumberFormat="Custom" Width="100%">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                                <Style  HorizontalAlign="Right">
                                </Style>
                            </PropertiesSpinEdit>
                            <EditFormSettings Visible="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataSpinEditColumn>
                    </Columns>
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings ShowGroupPanel="True" ShowTitlePanel="True" 
                        VerticalScrollBarMode="Visible" />
                    <SettingsText  />
                    <Styles>
                        <Header Wrap="True">
                        </Header>
                        <Footer Font-Bold="True">
                        </Footer>
                    </Styles>
                    <Templates>
                        <TitlePanel>
                            <table cellpadding="0" cellspacing="0" class="style3">
                                <tr>
                                    <td align="left">
                                        R$ x 1.000</td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td class="style2">
            </td>
        </tr>
        <tr>
            <td align="right">
                <dxe:ASPxButton ID="btnFechar5" runat="server" ClientInstanceName="btnFechar5" 
                     Text="Fechar" Width="90px">
                    <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    pcAlteracoes.Hide();
}" />
                    <Paddings Padding="0px" />
                </dxe:ASPxButton>
            </td>
        </tr>
    </table>
            </dxpc:PopupControlContentControl>
</ContentCollection>
    </dxpc:ASPxPopupControl>

    <dxhf:ASPxHiddenField ID="hfCodigoMestre" runat="server" 
        ClientInstanceName="hfCodigoMestre">
    </dxhf:ASPxHiddenField>

    </form>
</body>
</html>
