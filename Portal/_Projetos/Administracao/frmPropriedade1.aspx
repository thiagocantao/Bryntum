<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPropriedade1.aspx.cs"
    Inherits="frmPropriedade1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script language="javascript" type="text/javascript">
</script>
    <title></title>
    <style type="text/css">
        .Tabela
        {
            width: 100%;
        }
        .style1
        {
            width: 100%;
        }
        .style20
        {
            width: 294px;
        }
        </style>
    <script type="text/javascript" language="javascript">

        var existeConteudoCampoAlterado = false;

        function btnSalvar_Click(s, e) {
            var msg = ValidaCampos();
            if (msg == "") {
                callback.PerformCallback("");
                existeConteudoCampoAlterado = false;
            }
            else {
                window.top.mostraMensagem(msg, 'atencao', true, false, null);
            }
        }

        function ValidaCampos() {
            var msg = "";
            //if (txtLocalizacao.GetText() == "") {
            //      msg += "O campo 'Localização' deve ser informado.\n";
            //}
            //if (ddlUF.GetText() == "") {
            //      msg += "O campo 'UF' deve ser informado.\n";
            //}
            //if (ddlMunicipio.GetText() == "") {
            //      msg += "O campo 'Município' deve ser informado.\n";
            //}
            //if (ddlSpolio.GetText() == "") {
            //      msg += "O campo 'Espólio' deve ser informado.\n";
            //}
            return msg;
        }

        function ProcessaResultadoCallback(s, e) {
            var result = e.result;
            if (result && result.length && result.length > 0) {
                if (result.substring(0, 1) == "I") {
                    var activeTabIndex = pageControl.GetActiveTabIndex();
                    window.location = "./frmPropriedade1.aspx?" + result.substring(1) + "&tab=" + activeTabIndex;
                }
                window.top.mostraMensagem('Alterações salvas com sucesso!', 'sucesso', false, false, null);
            }
        }

        function setMaxLength(textAreaElement, length, lblCount) {
            textAreaElement.maxlength = length;
            textAreaElement.LabelCount = lblCount;
            ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
            ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
        }

        function onKeyUpOrChange(evt) {
            processTextAreaText(ASPxClientUtils.GetEventSource(evt));
        }

        function processTextAreaText(textAreaElement) {
            var maxLength = textAreaElement.maxlength;
            var labelCount = textAreaElement.LabelCount;
            var text = textAreaElement.value;
            var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
            if (maxLength != 0 && text.length > maxLength)
                textAreaElement.value = text.substr(0, maxLength);
            else {
                labelCount.SetText(text.length + ' de ' + maxLength);
            }
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" sroll="yes">
    <div style="overflow: auto">
        <table border="0" cellpadding="0" cellspacing="0" class="Tabela">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="margin-left: 20px">
                    <dxtc:ASPxPageControl ID="pageControl" runat="server" ActiveTabIndex="0" ClientInstanceName="pageControl"
                         Width="100%">
                        <TabPages>
                            <dxtc:TabPage Name="tabImovel" Text="Imovel">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <div id="divRolagem" runat="server">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 98%">
                                                <tr>
                                                    <td style="padding-right: 5px;">
                                                        <table border="0" cellpadding="0" cellspacing="0" class="style1">
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel607" runat="server" Font-Bold="False" 
                                                                         Text="Cód. Antigo:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel630" runat="server" Font-Bold="False" 
                                                                        
                                                                        Text="Cód. Novo:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel608" runat="server" Font-Bold="False" 
                                                                         Text="Localização:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel609" runat="server" Font-Bold="False" 
                                                                         Text="Distrito:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; padding-right: 3px">
                                                                    <dxe:ASPxTextBox ID="txtSeq" runat="server" ClientInstanceName="txtSeq" 
                                                                         MaxLength="25" Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 100px; padding-right: 3px">
                                                                    <dxe:ASPxTextBox ID="txtSeq1" runat="server" ClientInstanceName="txtSeq1" 
                                                                         MaxLength="25" Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 400px; padding-right: 3px">
                                                                    <dxe:ASPxTextBox ID="txtLocalizacao" runat="server" 
                                                                        ClientInstanceName="txtLocalizacao"  
                                                                        MaxLength="255" Width="100%">
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
}" />
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtDistrito" runat="server" 
                                                                        ClientInstanceName="txtDistrito"  
                                                                        MaxLength="250" Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 5px;">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel610" runat="server" Font-Bold="False" 
                                                                         Text="UF:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td class="style20">
                                                                    <dxe:ASPxLabel ID="ASPxLabel611" runat="server" Font-Bold="False" 
                                                                         Text="Município:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel612" runat="server" Font-Bold="False" 
                                                                         
                                                                        Text="Comarca:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%; padding-right: 3px">
                                                                    <dxe:ASPxComboBox ID="ddlUF" runat="server" ClientInstanceName="ddlUF" 
                                                                        DataSourceID="sdsUF"  
                                                                        IncrementalFilteringMode="Contains"  
                                                                        ShowShadow="False" 
                                                                        TextField="SiglaUF" ValueField="SiglaUF" Width="99%">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
    ddlMunicipio.PerformCallback();
}" />
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 45%;; padding-right: 3px">
                                                                    <dxe:ASPxComboBox ID="ddlMunicipio" runat="server" 
                                                                        ClientInstanceName="ddlMunicipio"  
                                                                        OnCallback="ddlMunicipio_Callback" Width="100%">
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtComarca" runat="server" ClientInstanceName="txtComarca" 
                                                                         MaxLength="250" 
                                                                        Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 5px;">
                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                            <tr>
                                                                <td style="width: 505px; padding-right: 3px">
                                                                    <dxe:ASPxLabel ID="ASPxLabel613" runat="server" Font-Bold="False" 
                                                                         Text="Denominação/Região:">
                                                                    </dxe:ASPxLabel>
                                                                    <dxe:ASPxTextBox ID="txtRegiao" runat="server" ClientInstanceName="txtRegiao" 
                                                                         MaxLength="250" 
                                                                        Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel614" runat="server" Font-Bold="False" 
                                                                         
                                                                        Text="Identificação Fundiária:">
                                                                    </dxe:ASPxLabel>
                                                                    <dxe:ASPxTextBox ID="txtIdFundiaria" runat="server" 
                                                                        ClientInstanceName="txtIdFundiaria"  
                                                                        MaxLength="250" Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 10px; height: 5px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 5px;">
                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                            <tr>
                                                                <td style="width: 505px; padding-right: 3px">
                                                                    <dxe:ASPxLabel ID="ASPxLabel615" runat="server" Font-Bold="False" 
                                                                         
                                                                        Text="Coordenadas:">
                                                                    </dxe:ASPxLabel>
                                                                    <dxe:ASPxTextBox ID="txtCoordenadas" runat="server" 
                                                                        ClientInstanceName="txtCoordenadas"  
                                                                        MaxLength="50" Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="; padding-right: 3px">
                                                                    <dxe:ASPxLabel ID="ASPxLabel616" runat="server" Font-Bold="False" 
                                                                        
                                                                        Text="Área Total (ha):">
                                                                    </dxe:ASPxLabel>
                                                                    <dxe:ASPxSpinEdit ID="speAreaTotal" runat="server" 
                                                                        ClientInstanceName="speAreaTotal" DecimalPlaces="4" DisplayFormatString="n4" 
                                                                         HorizontalAlign="Right" Increment="0.1" 
                                                                        Number="0" Width="100%">
                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                        </SpinButtons>
                                                                        <Paddings Padding="0px" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>
                                                                    &nbsp;<dxe:ASPxLabel ID="ASPxLabel617" runat="server" Font-Bold="False" 
                                                                         
                                                                        Text="Área Atingida (ha):">
                                                                    </dxe:ASPxLabel>
                                                                    <dxe:ASPxSpinEdit ID="speAreaAtingida" runat="server" 
                                                                        ClientInstanceName="speAreaAtingida" DecimalPlaces="4" DisplayFormatString="n4" 
                                                                         Height="21px" HorizontalAlign="Right" 
                                                                        Increment="0.1" Number="0" Width="100%">
                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                        </SpinButtons>
                                                                        <Paddings Padding="0px" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 10px">
                                                        <dxe:ASPxLabel ID="ASPxLabel618" runat="server" Font-Bold="False" 
                                                             
                                                            Text="Documentos Registrados:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxwgv:ASPxGridView ID="gvDocRegistradoImovel" runat="server" 
                                                            AutoGenerateColumns="False" ClientInstanceName="gvDocRegistradoImovel" 
                                                            DataSourceID="sdsDocumentosRegistrados"  
                                                            KeyFieldName="CodigoDocumentoRegistradoImovel" 
                                                            OnCommandButtonInitialize="grid_CommandButtonInitialize" 
                                                            OnRowInserting="gvDocRegistradoImovel_RowInserting" Width="100%">
                                                            <TotalSummary>
                                                                <dxwgv:ASPxSummaryItem DisplayFormat="Total: {0:n4}" FieldName="Area" 
                                                                    ShowInGroupFooterColumn="Área" SummaryType="Sum" 
                                                                    ValueDisplayFormat="Total: {0:n4}" />
                                                            </TotalSummary>
                                                            <Columns>
                                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="0" Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                                    <HeaderTemplate>
                                                                        <%=ObtemBotaoInclusaoRegistro("gvDocRegistradoImovel", "Documento registrado")%>
                                                                    </HeaderTemplate>
                                                                </dxwgv:GridViewCommandColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="CodigoDocumentoRegistradoImovel" 
                                                                    FieldName="CodigoDocumentoRegistradoImovel" ShowInCustomizationForm="True" 
                                                                    Visible="False" VisibleIndex="7">
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Ofício" FieldName="Oficio" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                                    <PropertiesTextEdit EnableClientSideAPI="True">
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings Caption="Ofício:" CaptionLocation="Top" Visible="True" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Matrícula/Transcrição" 
                                                                    FieldName="NumeroMatriculaTranscricao" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="5">
                                                                    <PropertiesTextEdit EnableClientSideAPI="True">
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings Caption="Matricula/Transcrição:" CaptionLocation="Top" 
                                                                        Visible="True" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Livro" FieldName="NumeroLivro" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                                                    <PropertiesTextEdit>
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings Caption="Livro:" CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Folha" FieldName="NumeroFolha" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                                                    <PropertiesTextEdit EnableClientSideAPI="True">
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings Caption="Folha:" CaptionLocation="Top" Visible="True" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataSpinEditColumn Caption="Área (ha)" FieldName="Area" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="6">
                                                                    <PropertiesSpinEdit DisplayFormatString="n4" NumberFormat="Custom">
                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                        </SpinButtons>
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesSpinEdit>
                                                                    <EditFormSettings Caption="Área:" CaptionLocation="Top" />
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <CellStyle HorizontalAlign="Right">
                                                                    </CellStyle>
                                                                </dxwgv:GridViewDataSpinEditColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Comarca" FieldName="Comarca" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                                                    <PropertiesTextEdit>
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings Caption="Comarca:" CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                            </Columns>
                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="PopupEditForm" />
                                                                 <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                AllowResize="True" Width="600px" />
            </SettingsPopup> 
                                                                
                                                            <Settings ShowFooter="True" />
                                                            <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                        </dxwgv:ASPxGridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 5px; padding-top: 3px;">
                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                            <tr>
                                                                <td width="123px">
                                                                    <dxe:ASPxLabel ID="ASPxLabel619" runat="server" Font-Bold="False" 
                                                                        
                                                                        Text="Imóvel adquirido de:" Width="120px">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtProcedenciaAquisicao" runat="server" 
                                                                        ClientInstanceName="txtProcedenciaAquisicao" 
                                                                        MaxLength="250"
                                                                        Width="100%">
                                                                        <Paddings Padding="0px" />
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 10px; height: 5px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 10px">
                                                        <dxe:ASPxLabel ID="ASPxLabel620" runat="server" Font-Bold="False" 
                                                            
                                                            Text="Área de reserva legal averbada:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxwgv:ASPxGridView ID="gvAreaAverbada" runat="server" 
                                                            AutoGenerateColumns="False" ClientInstanceName="gvAreaAverbada" 
                                                            DataSourceID="sdsAreaAverbada"  
                                                            KeyFieldName="CodigoAreaAverbada" 
                                                            OnCommandButtonInitialize="gvAreaAverbada_CommandButtonInitialize" 
                                                            OnRowInserting="gvAreaAverbada_RowInserting" 
                                                            OnRowUpdating="gvAreaAverbada_RowUpdating" Width="100%">
                                                            <TotalSummary>
                                                                <dxwgv:ASPxSummaryItem DisplayFormat="Total: {0:n4}" FieldName="Area" 
                                                                    ShowInColumn="Área (ha)" ShowInGroupFooterColumn="Área (ha)" SummaryType="Sum" 
                                                                    ValueDisplayFormat="Total: {0:n4}" />
                                                            </TotalSummary>
                                                            <Columns>
                                                             <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="0" Width="60px" ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true">
                                                                    <HeaderTemplate>
                                                                        <%=ObtemBotaoInclusaoRegistro("gvAreaAverbada", "Área Averbada")%>
                                                                    </HeaderTemplate>
                                                                </dxwgv:GridViewCommandColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="CodigoAreaAverbada" 
                                                                    FieldName="CodigoAreaAverbada" ShowInCustomizationForm="True" Visible="False" 
                                                                    VisibleIndex="8">
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="CodigoImovel" FieldName="CodigoImovel" 
                                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="NIRF" FieldName="NIRF" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                                                    <PropertiesTextEdit>
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings Caption="NIRF:" CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="FMP" FieldName="FMP" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                                    <PropertiesTextEdit>
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings Caption="FMP:" CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="CCIR" FieldName="CCIR" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                                                    <PropertiesTextEdit>
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings Caption="CCIR:" CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Nº Módulo" FieldName="NumeroModulo" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                                                    <PropertiesTextEdit>
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings Caption="Nº Módulo" CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataTextColumn Caption="Módulo" FieldName="Modulo" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="5">
                                                                    <PropertiesTextEdit>
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesTextEdit>
                                                                    <EditFormSettings Caption="Módulo:" CaptionLocation="Top" />
                                                                </dxwgv:GridViewDataTextColumn>
                                                                <dxwgv:GridViewDataSpinEditColumn Caption="Área (ha)" FieldName="Area" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="7">
                                                                    <PropertiesSpinEdit DisplayFormatString="n4" NumberFormat="Custom">
                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                        </SpinButtons>
                                                                        <Style >
                                                                        </Style>
                                                                    </PropertiesSpinEdit>
                                                                    <EditFormSettings Caption="Área" CaptionLocation="Top" />
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <CellStyle HorizontalAlign="Right">
                                                                    </CellStyle>
                                                                </dxwgv:GridViewDataSpinEditColumn>
                                            
                                                            </Columns>
                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="PopupEditForm"/>
                                                             <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="600px"
                AllowResize="True" />
            </SettingsPopup>
                                                            <Settings ShowFooter="True" />
                                                            <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                        </dxwgv:ASPxGridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 5px; padding-top: 3px;">
                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                            <tr>
                                                                <td style="width: 215px">
                                                                    <dxe:ASPxLabel ID="ASPxLabel621" runat="server" Font-Bold="False" 
                                                                        
                                                                        Text="Dados da Última Declaração de ITR:" Width="210px">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtDadosUltimaDeclaracaoITR" runat="server" 
                                                                        ClientInstanceName="txtDadosUltimaDeclaracaoITR" 
                                                                        MaxLength="250" 
                                                                        style="margin-left: 0px" Width="100%">
                                                                        <Paddings Padding="0px" />
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 10px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel622" runat="server" Font-Bold="False" 
                                                                        
                                                                        Text="Espólio:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel ID="ASPxLabel623" runat="server" Font-Bold="False" 
                                                                         
                                                                        Text="Inventariante:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 90px; padding-right: 3px">
                                                                    <dxe:ASPxComboBox ID="ddlSpolio" runat="server" ClientInstanceName="ddlSpolio" 
                                                                          
                                                                        SelectedIndex="1" Width="100%">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	existeConteudoCampoAlterado = true;
	if(s.GetValue() == 'S')
	{
		txtNomeInventariante.SetEnabled(true);
		txtJuizo.SetEnabled(true);
		txtCartorio.SetEnabled(true);
		txtAdvogado.SetEnabled(true);
		txtEndereco.SetEnabled(true);
		txtFone.SetEnabled(true);
	}
	else
	{
        txtNomeInventariante.SetText('');
		txtJuizo.SetText('');
		txtCartorio.SetText('');
		txtAdvogado.SetText('');
		txtEndereco.SetText('');
		txtFone.SetText('');
        
        txtNomeInventariante.SetEnabled(false);
		txtJuizo.SetEnabled(false);
		txtCartorio.SetEnabled(false);
		txtAdvogado.SetEnabled(false);
		txtEndereco.SetEnabled(false);
		txtFone.SetEnabled(false);
		

	}
}" />
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="Sim" Value="S" />
                                                                            <dxe:ListEditItem Selected="True" Text="Não" Value="N" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td align="left">
                                                                    <dxe:ASPxTextBox ID="txtNomeInventariante" runat="server" ClientEnabled="False" 
                                                                        ClientInstanceName="txtNomeInventariante"  
                                                                        MaxLength="150" Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 10px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel624" runat="server" Font-Bold="False" 
                                                                        
                                                                        Text="Juízo:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel625" runat="server" Font-Bold="False" 
                                                                        
                                                                        Text="Cartório:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 380px; padding-right: 3px">
                                                                    <dxe:ASPxTextBox ID="txtJuizo" runat="server" ClientEnabled="False" 
                                                                        ClientInstanceName="txtJuizo"  
                                                                        MaxLength="250" Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtCartorio" runat="server" ClientEnabled="False" 
                                                                        ClientInstanceName="txtCartorio"  
                                                                        MaxLength="250" Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel626" runat="server" Font-Bold="False" 
                                                                        
                                                                        Text="Advogado:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel627" runat="server" Font-Bold="False" 
                                                                         Text="Endereço:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel628" runat="server" Font-Bold="False" 
                                                                          
                                                                        Text="Telefone:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 230px; padding-right: 3px">
                                                                    <dxe:ASPxTextBox ID="txtAdvogado" runat="server" ClientEnabled="False" 
                                                                        ClientInstanceName="txtAdvogado"  
                                                                        MaxLength="150" Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="padding-right: 3px">
                                                                    <dxe:ASPxTextBox ID="txtEndereco" runat="server" ClientEnabled="False" 
                                                                        ClientInstanceName="txtEndereco"  
                                                                        MaxLength="500" Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 130px">
                                                                    <dxe:ASPxTextBox ID="txtFone" runat="server" ClientEnabled="False" 
                                                                        ClientInstanceName="txtFone"  
                                                                        MaxLength="50" Width="100%">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-right: 10px">
                                                        <dxe:ASPxLabel ID="ASPxLabel629" runat="server" Font-Bold="False" 
                                                             
                                                            Text="Observações:">
                                                        </dxe:ASPxLabel>
                                                        &nbsp;&nbsp;
                                                        <dxe:ASPxLabel ID="lblCantCarater0" runat="server" 
                                                            ClientInstanceName="lblCantCarater"  
                                                            ForeColor="Silver" Text="0">
                                                        </dxe:ASPxLabel>
                                                        &nbsp;
                                                        <dxe:ASPxLabel ID="lblDe501" runat="server" ClientInstanceName="lblDe500" 
                                                             ForeColor="Silver" Text=" de 500">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo ID="memoObservacoes" runat="server" 
                                                            ClientInstanceName="memoObservacoes"  
                                                            Rows="5" Width="100%">
                                                            <ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 500);
}" ValueChanged="function(s, e) {
	//existeConteudoCampoAlterado = true;
}" />
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
&nbsp;<table border="0" cellpadding="0" cellspacing="0" 
                                            style="width: 100%;">
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="tabProprietariosOcupantes" Text="Proprietários e ocupantes">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxp:ASPxPanel ID="pnlElementosResultado" runat="server" Width="100%">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <dxwgv:ASPxGridView ID="gvProprietarioOcupante" runat="server" 
                                                        AutoGenerateColumns="False" ClientInstanceName="gvProprietarioOcupante" 
                                                        DataSourceID="sdsPessoaImovel"  
                                                        KeyFieldName="CodigoPessoaImovel" Width="100%" 
                                                        OnCustomCallback="gvProprietarioOcupante_CustomCallback">
                                                        <ClientSideEvents CustomButtonClick="function(s, e) {
	if(e.buttonID == 'btnEditar')
    {
		gvProprietarioOcupante.GetRowValues(gvProprietarioOcupante.GetFocusedRowIndex(), 'CodigoProjeto;CodigoPessoaImovel;', abrejanelaPessoaImovel);
    } 
}
" />
                                                        <Columns>
                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" 
                                                                VisibleIndex="0" Width="80px" ShowDeleteButton="true">
                                                                <CustomButtons>
                                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar">
                                                                        <Image ToolTip="Alterar informações de ocupantes e proprietários" 
                                                                            Url="~/imagens/botoes/editarReg02.PNG">
                                                                        </Image>
                                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                                </CustomButtons>
                                                                <HeaderTemplate>
                                                                    <%=ObtemBotaoInclusaoRegistro("gvProprietarioOcupante", "Proprietários e ocupantes")%>
                                                                </HeaderTemplate>
                                                            </dxwgv:GridViewCommandColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Nascimento" 
                                                                FieldName="DataNascimentoPessoa" ShowInCustomizationForm="True" 
                                                                VisibleIndex="2" Width="95px">
                                                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                                    <Style >
                                                                    </Style>
                                                                </PropertiesTextEdit>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Proprietário?" 
                                                                FieldName="IndicaProprietario" ShowInCustomizationForm="True" VisibleIndex="3" 
                                                                Width="65px">
                                                                <DataItemTemplate>
                                                                    <%# Eval("IndicaProprietario").ToString() == "S" ? "Sim" : "Não" %>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Ocupante?" FieldName="IndicaOcupante" 
                                                                ShowInCustomizationForm="True" VisibleIndex="4" Width="65px">
                                                                <DataItemTemplate>
                                                                    <%# Eval("IndicaOcupante").ToString() == "S" ? "Sim" : "Não"%>
                                                                </DataItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Nome" FieldName="NomePessoa" 
                                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="CodigoProjeto" FieldName="CodigoProjeto" 
                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="CodigoPessoaImovel" 
                                                                FieldName="CodigoPessoaImovel" ShowInCustomizationForm="True" Visible="False" 
                                                                VisibleIndex="5">
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                        </SettingsPager>
                                                        <SettingsEditing Mode="Inline" />
                                                             <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                AllowResize="True" Width="600px"/>
            </SettingsPopup>
                                                        <Settings VerticalScrollBarMode="Visible" />
                                                        <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                                    </dxwgv:ASPxGridView>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                        </TabPages>
                        <ContentStyle>
                            <Paddings Padding="0px" PaddingLeft="5px" PaddingTop="5px" />
                        </ContentStyle>
                    </dxtc:ASPxPageControl>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-top: 20px">
                    &nbsp;
                </td>
                <td align="right" style="padding-top: 5px; padding-right: 5px;">
                    <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                        Text="Salvar" AutoPostBack="False" 
                        Width="110px">
                        <ClientSideEvents Click="function(s, e) {
	btnSalvar_Click(s, e);
}" />
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            </table>
    </div>
    <asp:SqlDataSource ID="sdsDadosFomulario" runat="server" SelectCommand="SELECT pim.CodigoImovel
            ,pim.SequencialImovel
            ,pim.Localizacao
            ,mun.NomeMunicipio as NomeMunicipio
            ,mun.CodigoMunicipio
            ,mun.SiglaUF
            ,pim.Distrito
            ,pim.Comarca
            ,pim.NomeRegiao
            ,pim.IdentificacaoFundiaria
            ,pim.Coordenadas
            ,pim.AreaTotal
            ,pim.AreaAtingida
            ,pim.ProcedenciaAquisicao
            ,pim.Observacao
            ,pim.IndicaEspolio
            ,pim.NomeInventariante
            ,pim.JuizoEspolio
            ,pim.CartorioEspolio
            ,pim.NomeAdvogadoEspolio
            ,pim.EnderecoAdvogadoEspolio
            ,pim.TelefoneAdvogadoEspolio
            ,pim.CodigoProjeto
            ,pim.DadosUltimaDeclaracaoITR
            ,pim.CodigoAntigo
            ,pim.CodigoNovo
  FROM dbo.Prop_Imovel pim
  left JOIN dbo.Municipio mun on (mun.CodigoMunicipio = pim.CodigoMunicipio)
INNER JOIN Projeto p on (pim.CodigoProjeto = p.CodigoProjeto)
WHERE pim.CodigoProjeto = @CodigoProjeto" 

InsertCommand="INSERT INTO Prop_Imovel (SequencialImovel ,Localizacao,CodigoMunicipio,Distrito
           ,Comarca ,NomeRegiao ,IdentificacaoFundiaria,Coordenadas ,AreaTotal ,AreaAtingida
           ,ProcedenciaAquisicao,Observacao ,IndicaEspolio,NomeInventariante,JuizoEspolio
           ,CartorioEspolio,NomeAdvogadoEspolio,EnderecoAdvogadoEspolio ,TelefoneAdvogadoEspolio
           ,CodigoProjeto ,DadosUltimaDeclaracaoITR)
     VALUES(@SequencialImovel,@Localizacao ,@CodigoMunicipio ,@Distrito,@Comarca,@NomeRegiao
           ,@IdentificacaoFundiaria,@Coordenadas  ,@AreaTotal,@AreaAtingida,@ProcedenciaAquisicao
           ,@Observacao,@IndicaEspolio,@NomeInventariante ,@JuizoEspolio  ,@CartorioEspolio
           ,@NomeAdvogadoEspolio,@EnderecoAdvogadoEspolio,@TelefoneAdvogadoEspolio,
           ,@CodigoProjeto,@DadosUltimaDeclaracaoITR)"> 
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" />
        </SelectParameters>
        <InsertParameters>
        
            <asp:FormParameter FormField="txt" Name="SequencialImovel" />
            <asp:Parameter Name="Localizacao" />
            <asp:Parameter Name="CodigoMunicipio" />
            <asp:Parameter Name="Distrito" />
            <asp:Parameter Name="Comarca" />
            <asp:Parameter Name="NomeRegiao" />
            <asp:Parameter Name="IdentificacaoFundiaria" />
            <asp:Parameter Name="Coordenadas" />
            <asp:Parameter Name="AreaTotal" />
            <asp:Parameter Name="AreaAtingida" />
            <asp:Parameter Name="ProcedenciaAquisicao" />
            <asp:Parameter Name="Observacao" />
            <asp:Parameter Name="IndicaEspolio" />
            <asp:Parameter Name="NomeInventariante" />
            <asp:Parameter Name="JuizoEspolio" />
            <asp:Parameter Name="CartorioEspolio" />
            <asp:Parameter Name="NomeAdvogadoEspolio" />
            <asp:Parameter Name="EnderecoAdvogadoEspolio" />
            <asp:Parameter Name="TelefoneAdvogadoEspolio" />
            <asp:Parameter Name="CodigoProjeto" />
            <asp:Parameter Name="DadosUltimaDeclaracaoITR" />
        
        </InsertParameters>
       

    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsDocumentosRegistrados" runat="server" SelectCommand="SELECT Comarca, Oficio, NumeroMatriculaTranscricao, 
                                                                                          NumeroLivro, NumeroFolha, Area, 
                                                                                          CodigoDocumentoRegistradoImovel 
                                                                                     FROM dbo.Prop_DocumentoRegistradoImovel 
                                                                                    WHERE CodigoImovel = @CodigoImovel"
        DeleteCommand="DELETE FROM dbo.Prop_DocumentoRegistradoImovel
      WHERE [CodigoDocumentoRegistradoImovel] = @CodigoDocumentoRegistradoImovel" 
        InsertCommand="INSERT INTO dbo.Prop_DocumentoRegistradoImovel
           (Comarca,Oficio,NumeroMatriculaTranscricao
           ,NumeroLivro,NumeroFolha,Area,CodigoImovel)
     VALUES(@Comarca,@Oficio,@NumeroMatriculaTranscricao,
           @NumeroLivro,@NumeroFolha,@Area,@CodigoImovel)"
 
        UpdateCommand="UPDATE Prop_DocumentoRegistradoImovel
                   SET Comarca = @Comarca
                            ,Oficio = @Oficio
        ,NumeroMatriculaTranscricao = @NumeroMatriculaTranscricao
                       ,NumeroLivro = @NumeroLivro
                       ,NumeroFolha = @NumeroFolha
                              ,Area = @Area WHERE CodigoDocumentoRegistradoImovel = @CodigoDocumentoRegistradoImovel">
        <DeleteParameters>
            <asp:Parameter Name="CodigoDocumentoRegistradoImovel" />
            <asp:Parameter Name="SequenciaRegistro" />

        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoImovel" SessionField="codigoImovel" 
                DefaultValue="-1" />
            <asp:Parameter Name="Comarca" Type="String" />
            <asp:Parameter Name="Oficio" Type="String" />
            <asp:Parameter Name="NumeroMatriculaTranscricao" Type="String" />
            <asp:Parameter Name="NumeroLivro" Type="String" />
            <asp:Parameter Name="NumeroFolha" Type="String" />
            <asp:Parameter Name="Area" Type="Decimal" />
            
            
            
           
            
            

        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoImovel" SessionField="codigoImovel" DefaultValue="-1" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CodigoImovel" />
            <asp:Parameter Name="Comarca" />
            <asp:Parameter Name="Oficio" />
            <asp:Parameter Name="NumeroMatriculaTranscricao" />
            <asp:Parameter Name="NumeroLivro" />
            <asp:Parameter Name="NumeroFolha" />
            <asp:Parameter Name="CodigoDocumentoRegistradoImovel" />
            <asp:Parameter Name="Area" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <asp:SqlDataSource ID="sdsAreaAverbada" runat="server" 
    SelectCommand="SELECT CodigoAreaAverbada
      ,CodigoImovel
      ,FMP
      ,NIRF
      ,CCIR
      ,NumeroModulo
      ,Modulo
      ,Area
  FROM Prop_AreaAverbada WHERE CodigoImovel = @CodigoImovel"
        DeleteCommand="DELETE FROM Prop_AreaAverbada
       WHERE CodigoAreaAverbada = @CodigoAreaAverbada" 
        InsertCommand="INSERT INTO Prop_AreaAverbada
           (CodigoImovel,FMP
           ,NIRF,CCIR,NumeroModulo ,Modulo ,Area)
     VALUES(@CodigoImovel  ,@FMP
           ,@NIRF  ,@CCIR ,@NumeroModulo,@Modulo,@Area)"
         
        UpdateCommand="UPDATE Prop_AreaAverbada
        SET CodigoImovel = @CodigoImovel
            ,FMP = @FMP
            ,NIRF = @NIRF
            ,CCIR = @CCIR
            ,NumeroModulo = @NumeroModulo
            ,Modulo = @Modulo
            ,Area = @Area
      WHERE CodigoAreaAverbada = @CodigoAreaAverbada">
        <DeleteParameters>
            <asp:Parameter Name="@CodigoAreaAverbada" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="CodigoImovel" SessionField="codigoImovel" DefaultValue="1"/>
            <asp:Parameter Name="FMP" />
            <asp:Parameter Name="NIRF" />
            <asp:Parameter Name="CCIR" />
            <asp:Parameter Name="NumeroModulo" />
            <asp:Parameter Name="Modulo" />
            <asp:Parameter Name="Area" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoImovel" SessionField="codigoImovel" DefaultValue="1"/>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CodigoImovel" />
            <asp:Parameter Name="FMP" />
            <asp:Parameter Name="NIRF" />
            <asp:Parameter Name="CCIR" />
            <asp:Parameter Name="NumeroModulo" />
            <asp:Parameter Name="Modulo" />
            <asp:Parameter Name="Area" />
            <asp:Parameter Name="CodigoAreaAverbada" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsUF" runat="server" 
    SelectCommand="SELECT SiglaUF
                         ,NomeUF                         
                     FROM UF">
        </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsPessoaImovel" runat="server" SelectCommand="SELECT * FROM dbo.Prop_PessoaImovel WHERE CodigoProjeto = @CodigoProjeto"
        DeleteCommand="DELETE FROM dbo.Prop_PessoaImovel
      WHERE [CodigoPessoaImovel] = @CodigoPessoaImovel"> 
        <DeleteParameters>
            <asp:Parameter Name="CodigoPessoaImovel" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter Name="CodigoProjeto" SessionField="codigoProjeto" DefaultValue="-1"/>
        </SelectParameters>
     </asp:SqlDataSource>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" 
        oncallback="callback_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {
	ProcessaResultadoCallback(s, e);
}" />
    </dxcb:ASPxCallback>
    </form>
</body>
</html>
