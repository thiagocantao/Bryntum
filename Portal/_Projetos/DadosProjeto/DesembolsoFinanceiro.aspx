<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesembolsoFinanceiro.aspx.cs" Inherits="_Projetos_DadosProjeto_DesembolsoFinanceiro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 5px;
        }
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 70px;
        }
        .style6
        {
            width: 10px;
        }
        .style7
        {
            height: 5px;
            width: 10px;
        }
        .style8
        {
            width: 435px;
        }
        .style9
        {
            width: 540px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var valorAtual = 0;

        function navegaSetas(tipo) {
            var collection = ASPxClientControl.GetControlCollection();
            var indexControle = 0;

            var novoIndex;
            var varAux = 0;

            if (tipo == 'C')
                varAux = -3;
            else if (tipo == 'B')
                varAux = 3;
            else if (tipo == 'E')
                varAux = -1
            else if (tipo == 'D')
                varAux = 1;

            try {
                for (var key in collection.elements) {

                    var control = collection.elements[key];
                    if (control.focused == true) {
                        var novoIndex = indexControle + varAux;
                        break;
                    }

                    indexControle++;
                }

                indexControle = 0;

                for (var key in collection.elements) {

                    var control = collection.elements[key];

                    if (novoIndex == indexControle && control.name.indexOf("txt") != -1) {
                        control.Focus();
                        break;
                    }

                    indexControle++;
                }

            } catch (e) { }
        }

        function executaCalculoCampo(valorQuantidade, textBox, txtSoma) {

            valorSoma = parseFloat(txtSoma.GetValue().toString().replace(',', '.')) - valorAtual;

            valorSoma = valorSoma + parseFloat(textBox.GetValue().toString().replace(',', '.'));

            txtSoma.SetValue(valorSoma);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" class="headerGrid" width="100%">
        <tr>
            <td class="style7">
                    </td>
            <td class="style1">
                </td>
        </tr>
        <tr>
            <td class="style6">
                    &nbsp;</td>
            <td>
                    <table cellpadding="0" cellspacing="0" class="style2">
                        <tr>
                            <td class="style9" style="padding-right: 10px">
                            <dxrp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" 
                                HeaderText="Período" View="GroupBox" Width="100%" BackColor="White" 
                                    EnableViewState="False">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table cellpadding="0" cellspacing="0" class="style2">
                                            <tr>
                                                <td class="style3">
                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                        Text="Ano:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td class="style8">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style3" style="padding-right: 10px">
                                                    <dxe:ASPxComboBox ID="ddlAno" runat="server" ClientInstanceName="ddlEixoX" 
                                                         Width="100%">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback('A');
}" />
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td class="style8" style="padding-right: 10px">
                                                    <dxe:ASPxRadioButtonList ID="rbTrimestres" runat="server" 
                                                        ClientInstanceName="rbTrimestres"  
                                                        ItemSpacing="10px" RepeatDirection="Horizontal" SelectedIndex="0" Width="100%">
                                                        <Paddings Padding="0px" />
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback('A');
}" />
                                                        <Items>
                                                            <dxe:ListEditItem Selected="True" Text="1º Trimestre" Value="1" />
                                                            <dxe:ListEditItem Text="2º Trimestre" Value="2" />
                                                            <dxe:ListEditItem Text="3º Trimestre" Value="3" />
                                                            <dxe:ListEditItem Text="4º Trimestre" Value="4" />
                                                        </Items>
                                                    </dxe:ASPxRadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <Border BorderWidth="1px" BorderColor="#8B8B8B" BorderStyle="Solid" />
                                <HeaderStyle BackColor="White" />
                            </dxrp:ASPxRoundPanel>
                            </td>
                            <td>
                            <dxrp:ASPxRoundPanel ID="ASPxRoundPanel3" runat="server" 
                                HeaderText="Editar Valores" View="GroupBox" Width="200px" BackColor="White" 
                                    EnableViewState="False">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table cellpadding="0" cellspacing="0" class="style2">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxRadioButtonList ID="rbValores" runat="server" 
                                                        ClientInstanceName="rbValores"  
                                                        ItemSpacing="10px" RepeatDirection="Horizontal" SelectedIndex="0" Width="100%">
                                                        <Paddings Padding="0px" />
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback('A');
}" />
                                                        <Items>
                                                            <dxe:ListEditItem Selected="True" Text="Previstos" Value="P" />
                                                            <dxe:ListEditItem Text="Realizados" Value="R" />
                                                        </Items>
                                                    </dxe:ASPxRadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <Border BorderWidth="1px" BorderColor="#8B8B8B" BorderStyle="Solid" />
                                <HeaderStyle BackColor="White" />
                            </dxrp:ASPxRoundPanel>
                            </td>
                        </tr>
                    </table>
                </td>
        </tr>
        <tr>
            <td class="style7">
                &nbsp;</td>
            <td class="style1">
                </td>
        </tr>
        <tr>
            <td class="style6">
                    &nbsp;</td>
            <td>
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" 
                         Width="100%" 
                        onautofiltercelleditorinitialize="gvDados_AutoFilterCellEditorInitialize" 
                        onhtmldatacellprepared="gvDados_HtmlDataCellPrepared" 
                        oncustomcallback="gvDados_CustomCallback" EnableViewState="False" 
                        onhtmlrowprepared="gvDados_HtmlRowPrepared" KeyFieldName="CodigoConta">
                        <TotalSummary>
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Valor1" 
                                ShowInColumn="1" SummaryType="Sum" 
                                ShowInGroupFooterColumn="1" ValueDisplayFormat="n2" Tag="Valor1" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Valor2" 
                                ShowInColumn="2" SummaryType="Sum" 
                                ShowInGroupFooterColumn="2" ValueDisplayFormat="n2" Tag="Valor2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Valor3" 
                                ShowInColumn="3" ShowInGroupFooterColumn="3" 
                                SummaryType="Sum" ValueDisplayFormat="n2" Tag="Valor3" /> 
                        </TotalSummary>
                        <GroupSummary>
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Valor1" 
                                ShowInGroupFooterColumn="1" SummaryType="Sum" ValueDisplayFormat="n2" 
                                Tag="Valor1" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Valor2" 
                                ShowInGroupFooterColumn="2" SummaryType="Sum" ValueDisplayFormat="n2" 
                                Tag="Valor2" />
                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="Valor3" 
                                ShowInGroupFooterColumn="3" SummaryType="Sum" ValueDisplayFormat="n2" 
                                Tag="Valor3" />
                        </GroupSummary>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="CodigoConta" VisibleIndex="0" 
                                FieldName="CodigoConta" Visible="False">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tipo" VisibleIndex="1" FieldName="Tipo" 
                                GroupIndex="0" SortIndex="0" SortOrder="Ascending">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Grupo" FieldName="GrupoConta" 
                                VisibleIndex="2" GroupIndex="1" SortIndex="1" SortOrder="Ascending">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Item" FieldName="DescricaoConta" VisibleIndex="3" 
                                ExportWidth="320" FixedStyle="Left">
                                <Settings AutoFilterCondition="Contains" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataSpinEditColumn VisibleIndex="4" 
                                ExportWidth="170" Width="140px" Caption="Janeiro" FieldName="Valor1" 
                                Name="1">
                                <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" 
                                    ClientInstanceName="spin1">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                </PropertiesSpinEdit>
                                <Settings AllowAutoFilter="False" />
                                <DataItemTemplate>
                                    <dxe:ASPxSpinEdit ID="txt1" runat="server" BackColor="#E1EAFF"  DecimalPlaces="2"
                                        DisplayFormatString="{0:n2}"  
                                        ForeColor="Black" HorizontalAlign="Right" Increment="0" LargeIncrement="0" 
                                        MaxValue="9999999999999" MinValue="-9999999999999" NullText="0" style="margin-bottom: 0px" 
                                        Text='<%# Eval("Valor1") %>' Width="100%">
                                        <SpinButtons ShowIncrementButtons="False">
                                        </SpinButtons>
                                        <ClientSideEvents ValueChanged="function(s, e) {	  
    callbackSalvar.PerformCallback(1);
}" />
                                        <NullTextStyle ForeColor="Black">
                                        </NullTextStyle>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                            ErrorTextPosition="Left" ValidationGroup="MKE">
                                        </ValidationSettings>
                                        <Border BorderStyle="None" />
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxSpinEdit>
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" >
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                                <FooterTemplate>
                                    <dxe:ASPxSpinEdit ID="spinTotal1" runat="server" BackColor="Transparent"
                                        ClientInstanceName="spinTotal1" DisplayFormatString="{0:n2}" ClientEnabled="False"
                                         ForeColor="Black" HorizontalAlign="Right" 
                                        Increment="0" LargeIncrement="0" MinValue="-9999999999999" MaxValue="9999999999999" NullText="0"  
                                        style="margin-bottom: 0px" Width="100%">
                                        <SpinButtons ShowIncrementButtons="False">
                                        </SpinButtons>                                       
                                        <ClientSideEvents Init="function(s, e) {
	s.SetValue(gvDados.cp_spinTotal1);
}" />
                                        <NullTextStyle ForeColor="Black">
                                        </NullTextStyle>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                            ErrorTextPosition="Left" ValidationGroup="MKE">
                                        </ValidationSettings>
                                        <Border BorderStyle="None" />
                                        <DisabledStyle BackColor="Transparent" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxSpinEdit>
                                </FooterTemplate>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn VisibleIndex="5" 
                                ExportWidth="170" Width="140px" Caption="Fevereiro" FieldName="Valor2" 
                                Name="2">
                                <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                </PropertiesSpinEdit>
                                <Settings AllowAutoFilter="False" />
                                <DataItemTemplate>
                                    <dxe:ASPxSpinEdit ID="txt2" runat="server" BackColor="#E1EAFF"  DecimalPlaces="2"
                                        DisplayFormatString="{0:n2}"  
                                        ForeColor="Black" HorizontalAlign="Right" Increment="0" LargeIncrement="0" 
                                        MaxValue="9999999999999" MinValue="-9999999999999" NullText="0" style="margin-bottom: 0px" 
                                        Text='<%# Eval("Valor2") %>' Width="100%">
                                        <SpinButtons ShowIncrementButtons="False">
                                        </SpinButtons>
                                        <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvar.PerformCallback(2);
}" />
                                        <NullTextStyle ForeColor="Black">
                                        </NullTextStyle>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                            ErrorTextPosition="Left" ValidationGroup="MKE">
                                        </ValidationSettings>
                                        <Border BorderStyle="None" />
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxSpinEdit>
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" >
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                                <FooterTemplate>
                                    <dxe:ASPxSpinEdit ID="spinTotal2" runat="server" BackColor="Transparent" 
                                        ClientInstanceName="spinTotal2" DisplayFormatString="{0:n2}" ClientEnabled="false"
                                         ForeColor="Black" HorizontalAlign="Right" 
                                        Increment="0" LargeIncrement="0" MinValue="-9999999999999" MaxValue="9999999999999" NullText="0"   
                                        style="margin-bottom: 0px" Width="100%">
                                        <SpinButtons ShowIncrementButtons="False">
                                        </SpinButtons>                                        
                                        <ClientSideEvents Init="function(s, e) {
	s.SetValue(gvDados.cp_spinTotal2);
}" />                                     
                                        <NullTextStyle ForeColor="Black">
                                        </NullTextStyle>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                            ErrorTextPosition="Left" ValidationGroup="MKE">
                                        </ValidationSettings>
                                        <Border BorderStyle="None" />
                                        <DisabledStyle BackColor="Transparent" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxSpinEdit>
                                </FooterTemplate>
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn VisibleIndex="6" 
                                ExportWidth="170" Width="140px" Caption="Março" FieldName="Valor3" 
                                Name="3">
                                <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                    <SpinButtons ShowIncrementButtons="False">
                                    </SpinButtons>
                                </PropertiesSpinEdit>
                                <Settings AllowAutoFilter="False" />
                                <DataItemTemplate>
                                    <dxe:ASPxSpinEdit ID="txt3" runat="server" BackColor="#E1EAFF" DecimalPlaces="2"
                                        DisplayFormatString="{0:n2}"  
                                        ForeColor="Black" HorizontalAlign="Right" Increment="0" LargeIncrement="0" 
                                        MaxValue="9999999999999" MinValue="-9999999999999" NullText="0" style="margin-bottom: 0px" 
                                        Text='<%# Eval("Valor3") %>' Width="100%">
                                        <SpinButtons ShowIncrementButtons="False">
                                        </SpinButtons>
                                        <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvar.PerformCallback(3);
}" />
                                        <NullTextStyle ForeColor="Black">
                                        </NullTextStyle>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                            ErrorTextPosition="Left" ValidationGroup="MKE">
                                        </ValidationSettings>
                                        <Border BorderStyle="None" />
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxSpinEdit>
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" Wrap="True" >
                                <Paddings Padding="3px" />
                                </HeaderStyle>
                                 
                                <FooterTemplate>
                                    <dxe:ASPxSpinEdit ID="spinTotal3" runat="server" BackColor="Transparent" 
                                        ClientInstanceName="spinTotal3" DisplayFormatString="{0:n2}" ClientEnabled="false"
                                         ForeColor="Black" HorizontalAlign="Right" 
                                        Increment="0" LargeIncrement="0" MinValue="-9999999999999" MaxValue="9999999999999" NullText="0"  
                                        style="margin-bottom: 0px"  Width="100%">
                                        <SpinButtons ShowIncrementButtons="False">
                                        </SpinButtons>                                       
                                        <ClientSideEvents Init="function(s, e) {
	s.SetValue(gvDados.cp_spinTotal3);
}" />                                      
                                        <NullTextStyle ForeColor="Black">
                                        </NullTextStyle>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                            ErrorTextPosition="Left" ValidationGroup="MKE">
                                        </ValidationSettings>
                                        <Border BorderStyle="None" />
                                        <DisabledStyle BackColor="Transparent" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxSpinEdit>
                                </FooterTemplate>
                            </dxwgv:GridViewDataSpinEditColumn>
                        </Columns>
                        <SettingsBehavior AutoExpandAllGroups="True" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" 
                            VerticalScrollBarMode="Visible" VerticalScrollableHeight="300" 
                            ShowFooter="True" ShowTitlePanel="True" />
                        <Styles>
                            <Header>
                                <Paddings PaddingLeft="2px" PaddingRight="2px" />
                            </Header>
                            <TitlePanel HorizontalAlign="Left">
                            </TitlePanel>
                        </Styles>
                        <Templates>
                            <TitlePanel>
                                <HeaderTemplate>
                                <table><tr><td style="cursor:pointer" title="Exportar para excel">
                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                        ImageUrl="~/imagens/botoes/btnExcel.png" 
                                        ToolTip="Exportar para excel" onclick="ImageButton1_Click" />
                                        </td></tr></table>
                                </HeaderTemplate>
                            </TitlePanel>
                        </Templates>
                    </dxwgv:ASPxGridView>
        <dxwgv:aspxgridviewexporter id="ASPxGridViewExporter1" runat="server" 
            gridviewid="gvDados" onrenderbrick="ASPxGridViewExporter1_RenderBrick"></dxwgv:aspxgridviewexporter>
    <dxcb:ASPxCallback ID="callbackSalvar" runat="server" 
        ClientInstanceName="callbackSalvar" oncallback="callbackSalvar_Callback">
    </dxcb:ASPxCallback>
                </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
