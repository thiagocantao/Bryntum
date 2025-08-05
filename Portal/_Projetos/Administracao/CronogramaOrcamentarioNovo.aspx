<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CronogramaOrcamentarioNovo.aspx.cs" Inherits="_Projetos_Administracao_CronogramaOrcamentarioNovo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js">
    </script>   
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        var valorAtual = 0;
        var codigoContaMemoria;
        var codigoAcaoMemoria;
        var codigoAcaoConta;
        var salvouConta = "N";

        function novaConta(codigoAcao) {
            codigoAcaoConta = codigoAcao;
            ddlConta.PerformCallback(codigoAcao);
        }

        function navegaSetas(tipo) {
            ASPxClientControl.AdjustControls(gvDados);
            var collection = ASPxClientControl.GetControlCollection();
            var indexControle = 0;

            var novoIndex;
            var varAux = gvDados.cp_IndexNavegacao;

            if (tipo == 'C')
                varAux = -3;
            else if (tipo == 'B')
                varAux = 3;

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

                    if (novoIndex == indexControle) {                                           
                        if (control.name.indexOf("txt") != -1) {
                            control.Focus();
                            break;
                        }
                    }

                    indexControle++;
                }

            } catch (e) { }
        }

        function executaCalculoCampo(valorQuantidade, textBox, txtSoma, txtSomaTotal, txtDisponibilidade, txtTotalGrupoDesponibilidade, txtSomaTotalDisponibilidade, valorDisponibilidadeAtual) {

            valorSoma = parseFloat(txtSoma.GetValue().toString().replace(',', '.')) - valorAtual;
            valorSomaTotal = parseFloat(txtSomaTotal.GetValue().toString().replace(',', '.')) - valorAtual;
            valorDisp = parseFloat(txtDisponibilidade.GetValue().toString().replace(',', '.')) - valorAtual;
            valorDispGrupo = parseFloat(txtTotalGrupoDesponibilidade.GetValue().toString().replace(',', '.')) - valorAtual;
            valorDispTotal = parseFloat(txtSomaTotalDisponibilidade.GetValue().toString().replace(',', '.')) - valorAtual;
                        
            valorSoma = valorSoma + (textBox.GetText() == '' ? 0 : parseFloat(textBox.GetValue().toString().replace(',', '.')));
            valorSomaTotal = valorSomaTotal + (textBox.GetText() == '' ? 0 : parseFloat(textBox.GetValue().toString().replace(',', '.')));

            valorDisp = valorDisp + (textBox.GetText() == '' ? 0 : parseFloat(textBox.GetValue().toString().replace(',', '.')));
            valorDispGrupo = valorDispGrupo + (textBox.GetText() == '' ? 0 : parseFloat(textBox.GetValue().toString().replace(',', '.')));
            valorDispTotal = valorDispTotal + (textBox.GetText() == '' ? 0 : parseFloat(textBox.GetValue().toString().replace(',', '.')));

            txtSoma.SetValue(valorSoma);
            txtSomaTotal.SetValue(valorSomaTotal);

            txtDisponibilidade.SetValue(valorDisp);
            txtTotalGrupoDesponibilidade.SetValue(valorDispGrupo);
            txtSomaTotalDisponibilidade.SetValue(valorDispTotal);
        }

        function executaCalculoCampoFormulacao(textBoxUni, textBoxQtde, txtLinha, txtSoma, txtSomaTotal) {

            //valorReal = valorAtual == 0 || valorAtual == null ? 0 : parseFloat(txtLinha.GetValue().toString().replace(',', '.')) / valorAtual;
            
            valorSoma = parseFloat(txtSoma.GetValue() == null ? "0" : txtSoma.GetValue().toString().replace(',', '.')) - txtLinha.GetValue().toString().replace(',', '.');
            valorSomaTotal = parseFloat(txtSomaTotal.GetValue().toString().replace(',', '.')) - txtLinha.GetValue().toString().replace(',', '.');

            valorCalculado = parseFloat(textBoxUni.GetValue() == null ? "0" : textBoxUni.GetValue().toString().replace(',', '.')) * parseFloat(textBoxQtde.GetValue() == null ? "0" : textBoxQtde.GetValue().toString().replace(',', '.'));
            valorSoma = valorSoma + valorCalculado;
            valorSomaTotal = valorSomaTotal + valorCalculado;

            txtLinha.SetValue(valorCalculado);
            txtSoma.SetValue(valorSoma);
            txtSomaTotal.SetValue(valorSomaTotal);
        }

        function abreMemoria(codigoAcao, codigoConta) {
            codigoContaMemoria = codigoConta;
            codigoAcaoMemoria = codigoAcao;
            callbackGetMemoria.PerformCallback(codigoAcao + ';' + codigoConta);
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
                if (textAreaElement.name.indexOf("mmObservacoes") >= 0)
                    lblCantCaraterOb.SetText(text.length);
                else
                    lblCantCarater.SetText(text.length);
            }
        }

        function createEventHandler(funcName) {
            return new Function("event", funcName + "(event);");
        }

        function onInit_mmObjeto(s, e) {
            try { return setMaxLength(s.GetInputElement(), 4000); }
            catch (e) { }
        }

    </script>
</head>
<body class="body">
    <form id="form1" runat="server">
    <div>
        <div style="overflow:auto;">
            <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" OnHtmlRowPrepared="gvDados_HtmlRowPrepared"
                OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" KeyFieldName="CodigoAcao;CodigoConta"
                 Width="100%" OnRowDeleting="gvDados_RowDeleting"
                OnCommandButtonInitialize="gvDados_CommandButtonInitialize" OnCustomCallback="gvDados_CustomCallback"
                EnableRowsCache="False" EnableViewState="False">
                <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_AtualizaComboContas == 'S')
		ddlConta.PerformCallback(codigoAcaoConta);
	
	if(window.parent.callbackCronogramaOrcamentario)
		window.parent.callbackCronogramaOrcamentario.PerformCallback();
}" />
                <Columns>
                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="40px" ShowDeleteButton="true">
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ação" FieldName="NumeroAcao" GroupIndex="0"
                        SortIndex="0" SortOrder="Ascending" VisibleIndex="1" Width="350px">
                        <GroupRowTemplate>
                            <%# string.Format(@"<table style=""width:100%""><tr><td style=""width:40px"" align=""Center"">{0}</td><td>{1}</td></tr></table>", (somenteLeitura != "S") ? string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""novaConta({0})"" style=""cursor: pointer;""/>", Eval("CodigoAcao")) : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>"
                                            , Eval("NomeAcao"))%>
                        </GroupRowTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Memória de Cálculo" FieldName="MemoriaCalculo"
                        VisibleIndex="3" Width="150px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Conta" FieldName="CONTA_DES" VisibleIndex="2"
                        Width="250px" FixedStyle="Left">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Suplemento Anterior" FieldName="ValorSuplementacao_Old"
                        VisibleIndex="12" Width="125px" 
                        ToolTip="É o valor de suplementações anteriores obtido no Zeus">
                        <FooterTemplate>
                            <%# string.Format("{0:n2}", getSomaColuna("ValorSuplementacao_Old"))%>
                        </FooterTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Transposto Anterior" FieldName="ValorTransposicao_Old"
                        VisibleIndex="11" Width="125px" 
                        ToolTip="É o valor de transposições anteriores obtido no Zeus">
                        <FooterTemplate>
                            <%# string.Format("{0:n2}", getSomaColuna("ValorTransposicao_Old"))%>
                        </FooterTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Realizado" FieldName="ValorRealizado" VisibleIndex="4"
                        Width="120px" ToolTip="É o valor das realizações obtido no Zeus ">
                        <PropertiesTextEdit DisplayFormatString="N2">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <%# string.Format("{0:n2}", getSomaColuna("ValorRealizado")) %>
                        </FooterTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Disponib. Atual" FieldName="DisponibilidadeAtual"
                        VisibleIndex="5" Width="120px" 
                        ToolTip="É o resultado do cálculo (Valor Proposto - valor Realizado) + (Suplemento Anterior+Transposto Anterior)">
                        <PropertiesTextEdit DisplayFormatString="N2">
                        </PropertiesTextEdit>
                        <HeaderStyle Wrap="False" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <%# string.Format("{0:n2}", getSomaColuna("DisponibilidadeAtual")) %>
                        </FooterTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Suplementado" FieldName="ValorSuplemento"
                        VisibleIndex="6" Name="SUP" Width="120px" 
                        ToolTip="É o valor informado na reformulação">
                        <PropertiesTextEdit DisplayFormatString="N2">
                        </PropertiesTextEdit>
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit ID="txtSUP" runat="server" BackColor="#E1EAFF" DecimalPlaces="2"
                                DisplayFormatString="{0:n2}"  ForeColor="Black"
                                HorizontalAlign="Right" Increment="0" LargeIncrement="0" MaxValue="9999999999999"
                                MinValue="-9999999999999" NullText="0" Style="margin-bottom: 0px" text='<%# Eval("ValorSuplemento") %>'
                                Width="100%">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                                <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvar.PerformCallback('SUP');
}" />
                                <NullTextStyle ForeColor="Black">
                                </NullTextStyle>
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Left"
                                    ValidationGroup="MKE">
                                </ValidationSettings>
                                <Border BorderStyle="None" />
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle BackColor="#E1EAFF">
                        </CellStyle>
                        <FooterTemplate>
                            <dxe:ASPxSpinEdit ID="txtSomaTotalSUP" runat="server" BackColor="Transparent" ClientEnabled="False"
                                ClientInstanceName="txtSomaTotalSUP" DisplayFormatString="{0:n2}"
                                HorizontalAlign="Right" Style="margin-bottom: 0px" Text='<%# getSomaColuna("ValorSuplemento") %>'
                                Width="100%">
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
                    <dxwgv:GridViewDataTextColumn Caption="Transposto" FieldName="ValorTransposto" VisibleIndex="10"
                        Name="TRANS" Width="120px" ToolTip="É o valor informado na reformulação">
                        <PropertiesTextEdit DisplayFormatString="N2">
                        </PropertiesTextEdit>
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit ID="txtTRANS" runat="server" BackColor="#E1EAFF" DecimalPlaces="2"
                                DisplayFormatString="{0:n2}"  ForeColor="Black"
                                HorizontalAlign="Right" Increment="0" LargeIncrement="0" MaxValue="9999999999999"
                                MinValue="-9999999999999" NullText="0" Style="margin-bottom: 0px" text='<%# Eval("ValorTransposto") %>'
                                Width="100%">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                                <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvar.PerformCallback('TRANS');
}" />
                                <NullTextStyle ForeColor="Black">
                                </NullTextStyle>
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Left"
                                    ValidationGroup="MKE">
                                </ValidationSettings>
                                <Border BorderStyle="None" />
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle BackColor="#E1EAFF">
                        </CellStyle>
                        <FooterTemplate>
                            <dxe:ASPxSpinEdit ID="txtSomaTotalTRANS" runat="server" BackColor="Transparent" ClientEnabled="False"
                                ClientInstanceName="txtSomaTotalTRANS" DisplayFormatString="{0:n2}"
                                HorizontalAlign="Right" Style="margin-bottom: 0px" Text='<%# getSomaColuna("ValorTransposto") %>'
                                Width="100%">
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
                    <dxwgv:GridViewDataTextColumn Caption="Disponib. Reformulada" FieldName="DisponibilidadeReformulada"
                        VisibleIndex="13" Name="REF" Width="150px" 
                        ToolTip="É o resultado do cálculo (Valor Proposto - valor Realizado) + (Suplemento Anterior+Transposto Anterior + Suplementado + Transposto)">
                        <PropertiesTextEdit DisplayFormatString="N2">
                        </PropertiesTextEdit>
                        <HeaderStyle Wrap="False" HorizontalAlign="Right" />
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit ID="txtREF" runat="server" BackColor="Transparent" DecimalPlaces="2"
                                DisplayFormatString="{0:n2}"  ClientEnabled="false"
                                ForeColor="Black" HorizontalAlign="Right" Increment="0" LargeIncrement="0" MaxValue="9999999999999"
                                MinValue="-9999999999999" NullText="0" Style="margin-bottom: 0px" text='<%# Eval("DisponibilidadeReformulada") %>'
                                Width="100%">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                                <NullTextStyle ForeColor="Black">
                                </NullTextStyle>
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Left"
                                    ValidationGroup="MKE">
                                </ValidationSettings>
                                <Border BorderStyle="None" />
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle BackColor="#E1EAFF">
                        </CellStyle>
                        <FooterTemplate>
                            <dxe:ASPxSpinEdit ID="txtSomaTotalREF" runat="server" ReadOnly="true" BackColor="Transparent"
                                ClientEnabled="False" ClientInstanceName="txtSomaTotalREF" DisplayFormatString="{0:n2}"
                                 HorizontalAlign="Right" Style="margin-bottom: 0px"
                                Text='<%# getSomaColuna("DisponibilidadeReformulada") %>' Width="100%">
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
                    <dxwgv:GridViewDataTextColumn Caption="Quantidade" FieldName="Quantidade" VisibleIndex="8"
                        Name="QTDE" Width="120px" ToolTip="Quantidade proposta">
                        <PropertiesTextEdit DisplayFormatString="N0">
                        </PropertiesTextEdit>
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit ID="txtQTDE" runat="server" BackColor="#E1EAFF" DecimalPlaces="0"
                                DisplayFormatString="{0:n0}"  AllowMouseWheel="false"
                                NumberType="Integer" ForeColor="Black" HorizontalAlign="Right" Increment="0"
                                LargeIncrement="0" MaxValue="9999999999999" NullText="0" Style="margin-bottom: 0px"
                                Value='<%# (Eval("Quantidade") + "") == "" ? 0 : (int)double.Parse(Eval("Quantidade").ToString()) %>'
                                Width="100%">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                                <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvar.PerformCallback('QTDE');
}" />
                                <NullTextStyle ForeColor="Black">
                                </NullTextStyle>
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Left"
                                    ValidationGroup="MKE">
                                </ValidationSettings>
                                <Border BorderStyle="None" />
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle BackColor="#E1EAFF">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Valor Unitário" FieldName="ValorUnitario"
                        VisibleIndex="9" Name="UNI" Width="120px" 
                        ToolTip="Valor unitário proposto">
                        <PropertiesTextEdit DisplayFormatString="N2">
                        </PropertiesTextEdit>
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit ID="txtUNI" runat="server" BackColor="#E1EAFF" DecimalPlaces="2"
                                DisplayFormatString="{0:n2}"  ForeColor="Black"
                                HorizontalAlign="Right" Increment="0" LargeIncrement="0" MaxValue="9999999999999"
                                NullText="0" Style="margin-bottom: 0px" text='<%# Eval("ValorUnitario") %>' Width="100%">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                                <ClientSideEvents ValueChanged="function(s, e) {
	callbackSalvar.PerformCallback('UNI');
}" />
                                <NullTextStyle ForeColor="Black">
                                </NullTextStyle>
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Left"
                                    ValidationGroup="MKE">
                                </ValidationSettings>
                                <Border BorderStyle="None" />
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle BackColor="#E1EAFF">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Proposto" FieldName="ValorProposto" VisibleIndex="7"
                        Name="PROP" Width="120px" ToolTip="É o valor Inicial proposto">
                        <PropertiesTextEdit DisplayFormatString="N2">
                        </PropertiesTextEdit>
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit ID="txtPROP" runat="server" BackColor="Transparent" DecimalPlaces="2"
                                ReadOnly="true" DisplayFormatString="{0:n2}" 
                                ClientEnabled="false" ForeColor="Black" HorizontalAlign="Right" Increment="0"
                                LargeIncrement="0" MaxValue="9999999999999" NullText="0" Style="margin-bottom: 0px"
                                text='<%# Eval("ValorProposto") %>' Width="100%">
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                                <NullTextStyle ForeColor="Black">
                                </NullTextStyle>
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Left"
                                    ValidationGroup="MKE">
                                </ValidationSettings>
                                <Border BorderStyle="None" />
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle BackColor="#E1EAFF">
                        </CellStyle>
                        <FooterTemplate>
                            <dxe:ASPxSpinEdit ID="txtSomaTotalPROP" runat="server" BackColor="Transparent" ClientEnabled="False"
                                ClientInstanceName="txtSomaTotalPROP" DisplayFormatString="{0:n2}"
                                HorizontalAlign="Right" Style="margin-bottom: 0px" Text='<%# getSomaColuna("ValorProposto") %>'
                                Width="100%">
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
                <SettingsBehavior AllowSort="False" AllowDragDrop="False" ConfirmDelete="True" />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Settings VerticalScrollBarMode="Visible" ShowFooter="True" 
                    HorizontalScrollBarMode="Visible" />
                <SettingsText CommandDelete="Deseja excluir a conta do cronograma orçamentário?" />
                <Paddings Padding="5px" />
                <Styles>
                    <Header Wrap="False">
                    </Header>
                </Styles>
                <Border BorderColor="#000066" BorderStyle="Solid" />
            </dxwgv:ASPxGridView>
        </div>
        <dxcb:ASPxCallback ID="callbackSalvar" runat="server" 
            ClientInstanceName="callbackSalvar" oncallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	if(window.parent.callbackCronogramaOrcamentario)
		window.parent.callbackCronogramaOrcamentario.PerformCallback();
}" />
        </dxcb:ASPxCallback>
        <dxcb:ASPxCallback ID="callbackSalvarMemoria" runat="server" 
            ClientInstanceName="callbackSalvarMemoria" 
            oncallback="callbackSalvarMemoria_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	pcMemoria.Hide();
    gvDados.PerformCallback();
	if(window.parent.callbackCronogramaOrcamentario)
		window.parent.callbackCronogramaOrcamentario.PerformCallback();
}" />
        </dxcb:ASPxCallback>
        <dxcb:ASPxCallback ID="callbackGetMemoria" runat="server" 
            ClientInstanceName="callbackGetMemoria" 
            oncallback="callbackGetMemoria_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	txtComentario.SetText(s.cp_Memoria);
    pcMemoria.Show();
}" />
        </dxcb:ASPxCallback>
        <dxpc:ASPxPopupControl ID="pcMemoria" runat="server" 
            ClientInstanceName="pcMemoria"  
            HeaderText="Memória de Cálculo" Width="800px" Modal="True" 
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
            CloseAction="None" ShowCloseButton="False">
            <ClientSideEvents CloseUp="function(s, e) {	
    txtComentario.SetText(&quot;&quot;);
	lblCantCarater.SetText(&quot;0&quot;);
}" />
            <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table>        
        <tr>
            <td>
                <dxe:ASPxLabel ID="lblComentarios" runat="server" 
                    ClientInstanceName="lblComentarios"  
                    Text="Comentários:">
                </dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lblCantCarater0" runat="server" 
                    ClientInstanceName="lblCantCarater"  
                    ForeColor="Silver" Text="0">
                </dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lblDe251" runat="server" ClientInstanceName="lblDe250" 
                    EncodeHtml="False"  ForeColor="Silver" 
                    Text="&amp;nbsp;de 4000">
                </dxe:ASPxLabel>
                <dxe:ASPxMemo ID="txtComentario" runat="server" 
                    ClientInstanceName="txtComentario" 
                    Rows="10" Width="100%">
                    <ClientSideEvents Init="function(s, e) {
	onInit_mmObjeto(s, e);
}" />
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxe:ASPxMemo>
            </td>
        </tr>
        <tr>
            <td height="10px">
                &nbsp;<table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr>
                            <td align="right" style="padding-right: 10px; padding-top: 5px">
                                <dxe:ASPxButton ID="btnSalvarMemoria" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnSalvarMemoria"  
                                    Text="Salvar" Width="95px">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	callbackSalvarMemoria.PerformCallback(codigoAcaoMemoria + ';' + codigoContaMemoria);
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                            <td align="right" style="width: 100px; padding-top: 5px">
                                <dxe:ASPxButton ID="btnFecharMemoria" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnFecharMemoria"  
                                    Text="Fechar" Width="95px">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcMemoria.Hide();
}" />
                                    <Paddings Padding="0px" />
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
        <dxpc:ASPxPopupControl ID="pcNovaConta" runat="server" 
            ClientInstanceName="pcNovaConta"  
            HeaderText="Associar Nova Conta" Width="800px" Modal="True" 
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
            CloseAction="None" ShowCloseButton="False">
            <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table>        
        <tr>
            <td>
                <dxe:ASPxLabel ID="lblComentarios0" runat="server" 
                    ClientInstanceName="lblComentarios"  
                    Text="Conta:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td height="10px">
                <dxe:ASPxComboBox ID="ddlConta" runat="server" ClientInstanceName="ddlConta" 
                     OnCallback="ddlConta_Callback" 
                    Width="100%" TextFormatString="{0} - {1}" 
                    IncrementalFilteringMode="Contains">
                    <ClientSideEvents EndCallback="function(s, e) {
	pcNovaConta.Show();
}" />
                    <Columns>
                        <dxe:ListBoxColumn Caption="Código" FieldName="CONTA_COD" Width="120px" />
                        <dxe:ListBoxColumn Caption="Conta" FieldName="CONTA_DES" Width="300px" />
                    </Columns>
                </dxe:ASPxComboBox>
                &nbsp;<table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr>
                            <td align="right" style="padding-right: 10px; padding-top: 5px">
                                <dxe:ASPxButton ID="btnSalvar0" runat="server" AutoPostBack="False" 
                                    ClientInstanceName="btnSalvar"  
                                    Text="Salvar" Width="95px">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(ddlConta.GetValue() == null)
		window.top.mostraMensagem(&quot;A conta deve ser informada!&quot;, 'atencao', true, false, null);
	else
	{
		gvDados.PerformCallback(codigoAcaoConta + ';' + ddlConta.GetValue());		
	}
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                            <td align="right" style="width: 100px; padding-top: 5px">
                                <dxe:ASPxButton ID="btnFechar0" runat="server" 
                                    ClientInstanceName="btnFechar"  
                                    Text="Fechar" Width="95px" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	pcNovaConta.Hide();
}" />
                                    <Paddings Padding="0px" />
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
    </div>
    </form>
</body>
</html>
