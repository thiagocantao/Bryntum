<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detalhesMedicao.aspx.cs"
    Inherits="detalhesMedicao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
            height: 5px;
        }
        .style4
        {
            height: 10px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var valorAtual = 0;
        var podeSalvar;
        var codigoItem;

        function executaCalculoCampo(quantidadeMes, valorUnitario, quantidadeMedidaAteMes, valorMedidoMes, valorTotalAteMes) {

            
            paramQuantidadeMes = quantidadeMes.GetValue() == null ? 0 : quantidadeMes.GetValue();
            paramQuantidadeMedidaAteMes = quantidadeMedidaAteMes.GetValue() == null ? 0 : quantidadeMedidaAteMes.GetValue();
            paramValorMedidoMes = valorMedidoMes.GetValue() == null ? 0 : valorMedidoMes.GetValue();
            paramValorTotalAteMes = valorTotalAteMes.GetValue() == null ? 0 : valorTotalAteMes.GetValue();
            paramSomaValorMedidoMes = txtSomaValorMedidoMes.GetValue() == null ? 0 : txtSomaValorMedidoMes.GetValue();
            paramSomaValorTotalAteMes = txtSomaValorTotalAteMes.GetValue() == null ? 0 : txtSomaValorTotalAteMes.GetValue();

            quantidadeMedidaAteMes.SetValue(paramQuantidadeMes + paramQuantidadeMedidaAteMes - valorAtual);

            valorMedidoMes.SetValue(paramQuantidadeMes * valorUnitario);
            valorTotalAteMes.SetValue(quantidadeMedidaAteMes.GetValue() * valorUnitario);

            txtSomaValorMedidoMes.SetValue(paramSomaValorMedidoMes + valorMedidoMes.GetValue() - paramValorMedidoMes);
            txtSomaValorTotalAteMes.SetValue(paramSomaValorTotalAteMes + valorTotalAteMes.GetValue() - paramValorTotalAteMes);

            quantidadeMedidaAteMes.Validate();

            quantidadeMedidaAteMes.SetValue(paramQuantidadeMes + paramQuantidadeMedidaAteMes - valorAtual);
        }

        //----------- Mensagem modificação con sucesso..!!!
        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);
        }

        function ValidaValores() {
            return ASPxClientEdit.ValidateGroup('MKR', true);
        }

        function navegaSetas(tipo) {
            var collection = ASPxClientControl.GetControlCollection();
            var indexControle = 0;

            var novoIndex;

            try {
                for (var key in collection.elements) {

                    var control = collection.elements[key];
                    if (control.focused == true) {
                        var novoIndex = tipo == 'C' ? (indexControle - 4) : indexControle + 4;
                        break;
                    }

                    indexControle++;
                }

                indexControle = 0;

                for (var key in collection.elements) {

                    var control = collection.elements[key];

                    if (novoIndex == indexControle) {
                        control.Focus();
                        break;
                    }

                    indexControle++;
                }

            } catch (e) { }
        }

        function montaCampos(values) {
            var descricaoItem = (values[0] != null ? values[0] : "");
            var comentarioItemMedicao = (values[1] != null ? values[1] : "");

            txtDescricao.SetText(descricaoItem);
            mmObservacoes.SetText(comentarioItemMedicao);
            codigoItem = values[2];

            lblCantCaraterOb.SetText(mmObservacoes.GetText().length);
            pcDados.Show();
        }

        function setMaxLength(textAreaElement, length) {
            textAreaElement.maxlength = length;
            ASPxClientUtils.AttachEventToElement(textAreaElement, "keypress", createEventHandler("onKeyUpOrChange"));
            ASPxClientUtils.AttachEventToElement(textAreaElement, "change", createEventHandler("onKeyUpOrChange"));
        }

        function onKeyUpOrChange(evt) {
            processTextAreaText(ASPxClientUtils.GetEventSource(evt));
        }

        function processTextAreaText(textAreaElement) {
            var maxLength = textAreaElement.maxlength;
            var text = textAreaElement.value;
            var isAcceptable = (maxLength == 0) || (text.length <= maxLength);
            if (maxLength != 0 && text.length > maxLength)
                textAreaElement.value = text.substr(0, maxLength);
            else {
                lblCantCaraterOb.SetText(text.length);
            }
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function onInit_mmObservacoes(s, e) {
            try { return setMaxLength(s.GetInputElement(), 4000); }
            catch (e) { }
        }
    </script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td  width="50%" valign="top">
                                <dxrp:ASPxRoundPanel runat="server" HeaderText="" Width="100%" ClientInstanceName="rdValores"
                                     ID="rdValores">
                                    <ClientSideEvents Init="function(s, e) {
	window.top.pcModal.SetHeaderText(s.cp_Titulo);
}" />
                                    <ContentPaddings Padding="0px" />
                                    <HeaderStyle Font-Bold="False" >
                                        <Paddings Padding="0px" />
                                    </HeaderStyle>
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                        Text="Desconto de Adiantamento:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="padding-right: 15px">
                                                    <dxe:ASPxSpinEdit ID="txtAdiantamento" runat="server" BackColor="#E1EAFF" ClientInstanceName="txtAdiantamento"
                                                        DisplayFormatString="{0:n2}"  ForeColor="Black"
                                                        HorizontalAlign="Right" Increment="0" LargeIncrement="0" MaxValue="999999999"
                                                        NullText="0" Text='<%# valorAdiantamento %>' Style="margin-bottom: 0px" Width="90px">
                                                        <SpinButtons ShowIncrementButtons="False">
                                                        </SpinButtons>
                                                        <ClientSideEvents ValueChanged="function(s, e) {
	gvValores.PerformCallback('AD');
}" GotFocus="function(s, e) {
	s.SetText(s.GetValue() == 0 ? null : s.GetValue());
}" LostFocus="function(s, e) {
	s.SetText(s.GetValue() == null ? 0 : s.GetValue());
}" />
                                                        <NullTextStyle ForeColor="Black">
                                                        </NullTextStyle>
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Left"
                                                            ValidationGroup="MKR">
                                                        </ValidationSettings>
                                                        <FocusedStyle BackColor="#CCFFCC" Cursor="Pointer">
                                                        </FocusedStyle>
                                                        <Border BorderStyle="None" />
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                        Text="Faturamento Direto:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="padding-right: 15px">
                                                    <dxe:ASPxSpinEdit ID="txtFaturamento" runat="server" BackColor="#E1EAFF" ClientInstanceName="txtFaturamento"
                                                        DisplayFormatString="{0:n2}"  ForeColor="Black"
                                                        HorizontalAlign="Right" Increment="0" LargeIncrement="0" MaxValue="999999999"
                                                        NullText="0" Text='<%# valorFaturamento %>' Style="margin-bottom: 0px" Width="90px">
                                                        <SpinButtons ShowIncrementButtons="False">
                                                        </SpinButtons>
                                                        <ClientSideEvents ValueChanged="function(s, e) {
	gvValores.PerformCallback('FA');
}" GotFocus="function(s, e) {
	s.SetText(s.GetValue() == 0 ? null : s.GetValue());
}" LostFocus="function(s, e) {
	s.SetText(s.GetValue() == null ? 0 : s.GetValue());
}" />
                                                        <NullTextStyle ForeColor="Black">
                                                        </NullTextStyle>
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Left"
                                                            ValidationGroup="MKR">
                                                        </ValidationSettings>
                                                        <FocusedStyle BackColor="#CCFFCC" Cursor="Pointer">
                                                        </FocusedStyle>
                                                        <Border BorderStyle="None" />
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                                        Text="Glosa:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxSpinEdit ID="txtGlosa" runat="server" BackColor="#E1EAFF" ClientInstanceName="txtGlosa"
                                                        DisplayFormatString="{0:n2}"  ForeColor="Black"
                                                        HorizontalAlign="Right" Increment="0" Text='<%# valorGlosa %>' LargeIncrement="0"
                                                        MaxValue="999999999" NullText="0" Style="margin-bottom: 0px" Width="90px">
                                                        <SpinButtons ShowIncrementButtons="False">
                                                        </SpinButtons>
                                                        <ClientSideEvents ValueChanged="function(s, e) {
	gvValores.PerformCallback('GL');
}" GotFocus="function(s, e) {
	s.SetText(s.GetValue() == 0 ? null : s.GetValue());
}" LostFocus="function(s, e) {
	s.SetText(s.GetValue() == null ? 0 : s.GetValue());
}" />
                                                        <NullTextStyle ForeColor="Black">
                                                        </NullTextStyle>
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Left"
                                                            ValidationGroup="MKR">
                                                        </ValidationSettings>
                                                        <FocusedStyle BackColor="#CCFFCC" Cursor="Pointer">
                                                        </FocusedStyle>
                                                        <Border BorderStyle="None" />
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                            <dxwgv:ASPxGridView ID="gvValores" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvValores"
                                                 Width="100%" OnCustomCallback="gvValores_CustomCallback">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="Descrição do Valor Adicional" ShowInCustomizationForm="True"
                                                        VisibleIndex="0" FieldName="Descricao">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Aliquota (%)" ShowInCustomizationForm="True"
                                                        VisibleIndex="1" Width="90px" FieldName="Aliquota">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Valor (R$)" ShowInCustomizationForm="True"
                                                        VisibleIndex="2" Width="150px" FieldName="Valor">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Valores Resultantes (R$)" ShowInCustomizationForm="True"
                                                        VisibleIndex="3" Width="150px" FieldName="ValorResultante">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <Settings VerticalScrollableHeight="140" VerticalScrollBarMode="Auto" />
                                                <SettingsLoadingPanel Mode="Disabled" />
                                                <Styles>
                                                    <Cell>
                                                        <Paddings PaddingBottom="2px" PaddingTop="2px" />
                                                    </Cell>
                                                </Styles>
                                            </dxwgv:ASPxGridView>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxrp:ASPxRoundPanel>
                            </td>
                            <td style="padding-left: 5px">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                Text="Comentários Gerais da Medição:">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxMemo ID="txtComentarioGeral" runat="server" Rows="13" Width="100%" ClientInstanceName="txtComentarioGeral">
                                                <ClientSideEvents TextChanged="function(s, e) {
	callbackSalvar.PerformCallback('MG');
}" />
                                            </dxe:ASPxMemo>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td>
                    <dxwgv:ASPxGridView ID="gvMedicao" runat="server" AutoGenerateColumns="False"
                        Width="100%" ClientInstanceName="gvMedicao" OnHtmlDataCellPrepared="gvMedicao_HtmlDataCellPrepared"
                        KeyFieldName="CodigoItemMedicaoContrato" OnCustomButtonInitialize="gvMedicao_CustomButtonInitialize"
                        Style="margin-bottom: 27px">
                        <ClientSideEvents CustomButtonClick="function(s, e) {
	gvMedicao.GetRowValues(e.visibleIndex, 'DescricaoItem;ComentarioItemMedicao;CodigoItemMedicaoContrato', montaCampos);
}" />
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="40px">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnComentarios" Text="Comentários">
                                        <Image ToolTip="Comentários" Url="~/imagens/botoes/comentarios.PNG">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Descrição" VisibleIndex="6" FieldName="DescricaoItem">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Unidade" VisibleIndex="7" Width="65px" FieldName="UnidadeMedidaItem">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Valor Unitário(R$)" VisibleIndex="8" Width="120px"
                                FieldName="ValorUnitarioItem" Name="ValorUnitarioItem">
                                <PropertiesTextEdit DisplayFormatString="N2">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Right" />
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewBandColumn Caption="Quantidades" VisibleIndex="9">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Prevista" VisibleIndex="0" Width="85px" FieldName="QuantidadePrevistaTotal"
                                        Name="QuantidadePrevistaTotal">
                                        <PropertiesTextEdit DisplayFormatString="N2">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="No Mês" FieldName="QuantidadeMedidaMes" VisibleIndex="1"
                                        Width="100px" Name="QuantidadeMedidaMes">
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <DataItemTemplate>
                                            <dxe:ASPxSpinEdit ID="txtQuantidadeMedidaMes" runat="server" BackColor="#E1EAFF"
                                                DisplayFormatString="{0:n2}"  ForeColor="Black"
                                                HorizontalAlign="Right" Increment="0" LargeIncrement="0" MaxValue="999999999"
                                                NullText=" " Style="margin-bottom: 0px" Text='<%# Eval("QuantidadeMedidaMes") != null && Eval("QuantidadeMedidaMes").ToString() != "" ? Eval("QuantidadeMedidaMes").ToString() : "0" %>'
                                                Width="65px">
                                                <SpinButtons ShowIncrementButtons="False">
                                                </SpinButtons>
                                                <ClientSideEvents GotFocus="function(s, e) {
	//valorTotal = valorTotal - s.GetValue();
}" Init="function(s, e) {
//	 s.Validate();
     s.SetValue(s.GetValue());
}" LostFocus="function(s, e) {
	//valorTotal = valorTotal + s.GetValue();
	//atribuiValorTotal();
}" ValueChanged="function(s, e) {
    gvValores.PerformCallback();
}" />
                                                <NullTextStyle ForeColor="Black">
                                                </NullTextStyle>
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Left"
                                                    ValidationGroup="MKR">
                                                </ValidationSettings>
                                                <FocusedStyle BackColor="#CCFFCC" Cursor="Pointer">
                                                </FocusedStyle>
                                                <Border BorderStyle="None" />
                                            </dxe:ASPxSpinEdit>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle BackColor="#E1EAFF" HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Acumulada" VisibleIndex="2" Width="85px" FieldName="QuantidadeMedidaAteMes"
                                        Name="QuantidadeMedidaAteMes">
                                        <DataItemTemplate>
                                            <dxe:ASPxSpinEdit ID="txtQuantidadeMedidaAteMes" runat="server" ClientEnabled="False"
                                                ClientInstanceName="txtQuantidadeMedidaAteMes" DisplayFormatString="{0:n2}"
                                                HorizontalAlign="Right" Style="margin-bottom: 0px" Text='<%# Eval("QuantidadeMedidaAteMes") %>'
                                                Width="100%">
                                                <SpinButtons ShowIncrementButtons="False">
                                                </SpinButtons>
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Left"
                                                    ValidationGroup="MKR">
                                                </ValidationSettings>
                                                <Border BorderStyle="None" />
                                                <DisabledStyle ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxSpinEdit>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewBandColumn Caption="Valores(R$)" VisibleIndex="10">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="Previsto" VisibleIndex="0" Width="110px" FieldName="ValorPrevistoTotal"
                                        Name="ValorPrevistoTotal">
                                        <FooterTemplate>
                                            <dxe:ASPxSpinEdit ID="txtSomaValorPrevistoTotal" runat="server" BackColor="Transparent"
                                                ClientEnabled="False" ClientInstanceName="txtSomaValorPrevistoTotal" DisplayFormatString="{0:n2}"
                                                 HorizontalAlign="Right" Style="margin-bottom: 0px"
                                                Text='<%# getSomaColuna("ValorPrevistoTotal") %>' Width="100%">
                                                <SpinButtons ShowIncrementButtons="False">
                                                </SpinButtons>
                                                <ValidationSettings ErrorDisplayMode="None" ErrorTextPosition="Left">
                                                </ValidationSettings>
                                                <Border BorderStyle="None" />
                                                <DisabledStyle ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxSpinEdit>
                                        </FooterTemplate>
                                        <PropertiesTextEdit DisplayFormatString="N2">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="No Mês" VisibleIndex="1" Width="110px" FieldName="ValorMedidoMes"
                                        Name="ValorMedidoMes">
                                        <DataItemTemplate>
                                            <dxe:ASPxSpinEdit ID="txtValorMedidoMes" runat="server" ClientEnabled="False" ClientInstanceName="txtValorMedidoMes"
                                                DisplayFormatString="{0:n2}"  HorizontalAlign="Right"
                                                Style="margin-bottom: 0px" Text='<%# Eval("ValorMedidoMes") %>' Width="100%">
                                                <SpinButtons ShowIncrementButtons="False">
                                                </SpinButtons>
                                                <ValidationSettings ErrorDisplayMode="None" ErrorTextPosition="Left">
                                                </ValidationSettings>
                                                <Border BorderStyle="None" />
                                                <DisabledStyle ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxSpinEdit>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                        <FooterTemplate>
                                            <dxe:ASPxSpinEdit ID="txtSomaValorMedidoMes" runat="server" BackColor="Transparent"
                                                ClientEnabled="False" ClientInstanceName="txtSomaValorMedidoMes" DisplayFormatString="{0:n2}"
                                                 HorizontalAlign="Right" Style="margin-bottom: 0px"
                                                Text='<%# getSomaColuna("ValorMedidoMes") %>' Width="100%">
                                                <SpinButtons ShowIncrementButtons="False">
                                                </SpinButtons>
                                                <ValidationSettings ErrorDisplayMode="None" ErrorTextPosition="Left">
                                                </ValidationSettings>
                                                <Border BorderStyle="None" />
                                                <DisabledStyle ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxSpinEdit>
                                        </FooterTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Acumulado" VisibleIndex="2" Width="110px"
                                        FieldName="ValorTotalAteMes" Name="ValorTotalAteMes">
                                        <DataItemTemplate>
                                            <dxe:ASPxSpinEdit ID="txtValorTotalAteMes" runat="server" ClientEnabled="False" ClientInstanceName="txtValorTotalAteMes"
                                                DisplayFormatString="{0:n2}"  HorizontalAlign="Right"
                                                Style="margin-bottom: 0px" Text='<%# Eval("ValorTotalAteMes") %>' Width="100%">
                                                <SpinButtons ShowIncrementButtons="False">
                                                </SpinButtons>
                                                <ValidationSettings ErrorDisplayMode="None" ErrorTextPosition="Left">
                                                </ValidationSettings>
                                                <Border BorderStyle="None" />
                                                <DisabledStyle ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxSpinEdit>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                        <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                        </PropertiesTextEdit>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                        <FooterTemplate>
                                            <dxe:ASPxSpinEdit ID="txtSomaValorTotalAteMes" runat="server" BackColor="Transparent"
                                                ClientEnabled="False" ClientInstanceName="txtSomaValorTotalAteMes" DisplayFormatString="{0:n2}"
                                                 HorizontalAlign="Right" Style="margin-bottom: 0px"
                                                Text='<%# getSomaColuna("ValorTotalAteMes") %>' Width="100%">
                                                <SpinButtons ShowIncrementButtons="False">
                                                </SpinButtons>
                                                <ValidationSettings ErrorDisplayMode="None" ErrorTextPosition="Left">
                                                </ValidationSettings>
                                                <Border BorderStyle="None" />
                                                <DisabledStyle ForeColor="Black">
                                                </DisabledStyle>
                                            </dxe:ASPxSpinEdit>
                                        </FooterTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dxwgv:GridViewBandColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="ComentarioItemMedicao" Visible="False" VisibleIndex="12">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="RegistroEditavel" Visible="False" VisibleIndex="14">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" AutoExpandAllGroups="True" />
                        <SettingsPager Mode="ShowAllRecords">
                            <PageSizeItemSettings ShowAllItem="True" Visible="True">
                            </PageSizeItemSettings>
                        </SettingsPager>
                        <Settings ShowFooter="True" VerticalScrollBarMode="Visible" />
                        <Styles>
                            <Header Wrap="True">
                            </Header>
                        </Styles>
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
            <tr>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btnImprimir" runat="server" AutoPostBack="False" Text="Imprimir"
                                    Width="130px"  ClientInstanceName="btnImprimir"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	var url = window.top.pcModal.cp_Path + &quot;administracao/PrevisualizacaoImpressaoMedicao.aspx?cc=&quot; + s.cp_CodigoContrato + &quot;&amp;cm=&quot; + s.cp_CodigoMedicao;
	document.getElementById('frameConteudo').src = url;
	popupPrevisualizacaoImpressao.Show();
	//window.top.showModal(url, &quot;Visualizar Impressão&quot;, 1200, screen.height - 240, &quot;&quot;, null);
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnRemeter" runat="server" AutoPostBack="False" Text="Remeter para Aprovação"
                                    Width="185px"  ClientInstanceName="btnRemeter"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	if(confirm('Deseja remeter para aprovação?'))
		callbackSalvar.PerformCallback('AP');
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnFecharTela" runat="server" AutoPostBack="False" Text="Fechar"
                                    Width="130px"  ClientInstanceName="btnFecharTela"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	lpAguarde.Show();
	setTimeout('window.top.fechaModal();', 3000);
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <dxcb:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar"
        OnCallback="callbackSalvar_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Fechar == 'S')
	{
		window.top.retornoModal = 'S';
		window.top.fechaModal();
	}else
	{
		pcDados.Hide();
	}
}" />
    </dxcb:ASPxCallback>
    <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" ShowCloseButton="False"
        ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="" align="center">
                            </td>
                            <td style="width: 70px" align="center" rowspan="3">
                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                    ClientInstanceName="imgSalvar" ID="imgSalvar">
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                    ID="lblAcaoGravacao">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxlp:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="lpAguarde"
         Height="72px" Modal="True" Text="Aguarde&amp;hellip;"
        Width="124px">
    </dxlp:ASPxLoadingPanel>
    <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        CloseAction="None" ClientInstanceName="pcDados" HeaderText="Comentários" ShowCloseButton="False"
        Width="850px"  ID="pcDados" Modal="True">
        <ClientSideEvents CloseUp="function(s, e) {	
    txtDescricao.SetText('');
    mmObservacoes.SetText('');
	lblCantCaraterOb.SetText('0');
}" />
        <ContentStyle>
            <Paddings Padding="5px"></Paddings>
        </ContentStyle>
        <HeaderStyle Font-Bold="False"></HeaderStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table>
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" EncodeHtml="False"
                                Text="Descrição:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxTextBox ID="txtDescricao" runat="server" ClientEnabled="False" ClientInstanceName="txtDescricao"
                                 Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" EncodeHtml="False"
                                Text="Comentários: &amp;nbsp;">
                            </dxe:ASPxLabel>
                            <dxe:ASPxLabel ID="lblCantCaraterOb" runat="server" ClientInstanceName="lblCantCaraterOb"
                                 ForeColor="Silver" Text="0">
                            </dxe:ASPxLabel>
                            <dxe:ASPxLabel ID="lblDe250" runat="server" ClientInstanceName="lblDe250" EncodeHtml="False"
                                 ForeColor="Silver" Text=" &amp;nbsp;de 4000">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxMemo runat="server" Rows="13" Width="100%" ClientInstanceName="mmObservacoes"
                                 ID="mmObservacoes">
                                <ClientSideEvents Init="function(s, e) {
	onInit_mmObservacoes(s, e);
}"></ClientSideEvents>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAprovar"
                                                Text="Salvar" Width="100px"  ID="btnSalvar">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    callbackSalvar.PerformCallback(codigoItem);
}" Init="function(s, e) {
	s.SetVisible(gvMedicao.cp_RO != 'S');
}"></ClientSideEvents>
                                                <Paddings Padding="0px"></Paddings>
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                Text="Fechar" Width="90px"  ID="btnFechar">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    pcDados.Hide();
}"></ClientSideEvents>
                                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px">
                                                </Paddings>
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="popupPrevisualizacaoImpressao" runat="server" ClientInstanceName="popupPrevisualizacaoImpressao"
        HeaderText="Pré-visualização" Width="1200px" CloseAction="CloseButton"
        Height="550px" Modal="True" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" AllowDragging="True">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <div>
                    <iframe frameborder="0" id="frameConteudo" style="border: none;" scrolling="auto"
                        width="100%" height="500"></iframe>
                </div>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
