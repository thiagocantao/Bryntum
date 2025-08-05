<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cadastroCronogramaOrcamentario.aspx.cs" Inherits="_Projetos_Administracao_cadastroAcoes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script language="javascript" type="text/javascript">
        //----------- Mensagem modificação con sucesso..!!!
        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

        }

        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";

            if (ddlConta.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") A conta deve ser informada.";
            }

            if (txtQuantidade.GetValue() == null || txtQuantidade.GetText() == "") {
                numAux++;
                mensagem += "\n" + numAux + ") A quantidade deve ser informada.";
            }

            if (txtQuantidade.GetValue() <= 0) {
                numAux++;
                mensagem += "\n" + numAux + ") A quantidade deve ser informada com um valor maior que zero.";
            }

            if (txtValorUnitario.GetValue() == null || txtValorUnitario.GetText() == "") {
                numAux++;
                mensagem += "\n" + numAux + ") O valor unitário deve ser informado.";
            }


            if (txtValorUnitario.GetValue() <= 0) {
                numAux++;
                mensagem += "\n" + numAux + ") O valor unitário deve ser informado com um valor maior que zero.";
            }

            if ((mmObjeto.GetValue() == null || mmObjeto.GetText() == "")) {
                numAux++;
                mensagem += "\n" + numAux + ") Memória de cálculo deve ser informado.";
            }

            if ((mmObjeto.GetValue() != null || mmObjeto.GetText() != "") && mmObjeto.GetText().length < 10) {
                numAux++;
                mensagem += "\n" + numAux + ") Memória de cálculo deve ser informado com no mínimo 10 caracteres.";
            }

            if (txtValorTotal.GetValue() != null && txtValorTotal.GetText() != "" && verificaValorTotalPreenchidoMeses() == false) {
                numAux++;
                mensagem += "\n" + numAux + ") O valor total deve ser igual ao informado no planejamento anual.";
            }

            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = mensagem;
            }

            return mensagemErro_ValidaCamposFormulario;
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

        function verificaValorTotal() {
            var quantidade = txtQuantidade.GetValue();
            var valorUnitario = txtValorUnitario.GetValue();

            if (quantidade != null && valorUnitario != null) {
                txtValorTotal.SetValue(quantidade * valorUnitario);
            }
            else {
                txtValorTotal.SetValue(null);
            }
        }

        function verificaValorTotalPreenchidoMeses() {
            var valorTotal = txtValorTotal.GetValue();
            var valorPreenchido1 = 0;
            var valorPreenchido2 = 0;
            var valorPreenchido3 = 0;
            var valorPreenchido4 = 0;
            var valorPreenchido5 = 0;
            var valorPreenchido6 = 0;
            var valorPreenchido7 = 0;
            var valorPreenchido8 = 0;
            var valorPreenchido9 = 0;
            var valorPreenchido10 = 0;
            var valorPreenchido11 = 0;
            var valorPreenchido12 = 0;

            if (txtPlan01.GetValue() != null && txtPlan01.GetText() != "")
                valorPreenchido1 = txtPlan01.GetValue();

            if (txtPlan02.GetValue() != null && txtPlan02.GetText() != "")
                valorPreenchido2 = txtPlan02.GetValue();

            if (txtPlan03.GetValue() != null && txtPlan03.GetText() != "")
                valorPreenchido3 = txtPlan03.GetValue();

            if (txtPlan04.GetValue() != null && txtPlan04.GetText() != "")
                valorPreenchido4 = txtPlan04.GetValue();

            if (txtPlan05.GetValue() != null && txtPlan05.GetText() != "")
                valorPreenchido5 = txtPlan05.GetValue();

            if (txtPlan06.GetValue() != null && txtPlan06.GetText() != "")
                valorPreenchido6 = txtPlan06.GetValue();

            if (txtPlan07.GetValue() != null && txtPlan07.GetText() != "")
                valorPreenchido7 = txtPlan07.GetValue();

            if (txtPlan08.GetValue() != null && txtPlan08.GetText() != "")
                valorPreenchido8 = txtPlan08.GetValue();

            if (txtPlan09.GetValue() != null && txtPlan09.GetText() != "")
                valorPreenchido9 = txtPlan09.GetValue();

            if (txtPlan10.GetValue() != null && txtPlan10.GetText() != "")
                valorPreenchido10 = txtPlan10.GetValue();

            if (txtPlan11.GetValue() != null && txtPlan11.GetText() != "")
                valorPreenchido11 = txtPlan11.GetValue();

            if (txtPlan12.GetValue() != null && txtPlan12.GetText() != "")
                valorPreenchido12 = txtPlan12.GetValue();

            var valorPreenchido = parseInt(valorPreenchido1 * 100) + parseInt(valorPreenchido2 * 100) + parseInt(valorPreenchido3 * 100)
                                + parseInt(valorPreenchido4 * 100) + parseInt(valorPreenchido5 * 100) + parseInt(valorPreenchido6 * 100) 
                                + parseInt(valorPreenchido7 * 100) + parseInt(valorPreenchido8 * 100) + parseInt(valorPreenchido9 * 100)
                                + parseInt(valorPreenchido10 * 100) + parseInt(valorPreenchido11 * 100) + parseInt(valorPreenchido12 * 100);

            if ((valorPreenchido / 100) != valorTotal)
                return false;
            else
                return true;
        }
    </script>
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style7
        {
            width: 150px;
        }
        .style10
        {
            height: 10px;
        }
        .style12
        {
            height: 10px;
        }
        .style13
        {
            width: 130px;
        }
        .style14
        {
            width: 115px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" class="style1">
        <tr>
            <td>
                            <dxe:ASPxLabel runat="server" Text="Ação:" 
                                 ID="ASPxLabel2"></dxe:ASPxLabel>

            </td>
        </tr>
        <tr>
            <td>
                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="255" 
                                ClientInstanceName="txtNomeAcao" 
                    ID="txtNomeAcao" ClientEnabled="False">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                </dxe:ASPxTextBox>

            </td>
        </tr>
        <tr>
            <td scope="height:5px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                            <dxe:ASPxLabel runat="server" Text="Atividade:" 
                                 ID="ASPxLabel7"></dxe:ASPxLabel>

                        </td>
        </tr>
        <tr>
            <td>
                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="255" 
                                ClientInstanceName="txtNomeAtividade" 
                    ID="txtNomeAtividade" ClientEnabled="False">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                </dxe:ASPxTextBox>

                        </td>
        </tr>
        <tr>
            <td scope="height:5px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td>
                        <table>
                        <td>
                            <dxe:ASPxLabel runat="server" Text="Conta Orçamentária:" 
                                ID="ASPxLabel5"></dxe:ASPxLabel>

                        </td>
                        <td style="padding-left: 10px">
                            <dxe:ASPxImage ID="ASPxImage10" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                ToolTip="Selecione a conta orçamentária." Width="18px"
                                meta:resourcekey="ASPxImage10Resource1">
                            </dxe:ASPxImage>
                        </td>
                        </table>
                        </td>
                        <td class="style14">
                        <table>
                        <td>
                            <dxe:ASPxLabel runat="server" Text="Quantidade:" 
                                ID="ASPxLabel8"></dxe:ASPxLabel>

                        </td>
                        <td style="padding-left: 10px">
                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                ToolTip="Informe a quantidade." Width="18px"
                                meta:resourcekey="ASPxImage10Resource1">
                            </dxe:ASPxImage>
                        </td>
                        </table>
                        </td>
                        <td class="style13">
                        <table>
                        <td>
                            <dxe:ASPxLabel runat="server" Text="Valor Unitário:" 
                                ID="ASPxLabel9"></dxe:ASPxLabel>

                        </td>
                        <td style="padding-left: 10px">
                            <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                ToolTip="Informe valor unitário." Width="18px"
                                meta:resourcekey="ASPxImage10Resource1">
                            </dxe:ASPxImage>
                        </td>
                        </table>
                        </td>
                        <td>
                        <table>
                        <td>
                            <dxe:ASPxLabel runat="server" Text="Valor Total:" 
                                ID="ASPxLabel6"></dxe:ASPxLabel>

                        </td>
                        <td style="padding-left: 10px">
                            <dxe:ASPxImage ID="ASPxImage4" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                ToolTip="Valor Total é igual Valor unitário multiplicado por quantidade" Width="18px"
                                meta:resourcekey="ASPxImage10Resource1">
                            </dxe:ASPxImage>
                        </td>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                                Width="100%" ClientInstanceName="ddlConta" 
                                ID="ddlConta" TextFormatString="{0} - {1}">

                                <Columns>
                                    <dxe:ListBoxColumn Caption="Tipo" FieldName="DespesaReceita" Width="90px" 
                                        Visible="False" />
                                    <dxe:ListBoxColumn Caption="Código" FieldName="CONTA_COD" Width="110px" />
                                    <dxe:ListBoxColumn Caption="Descrição" FieldName="CONTA_DES" Width="500px" />
                                </Columns>

<ItemStyle ></ItemStyle>
</dxe:ASPxComboBox>

                        </td>
                        <td class="style14" >
                            <dxe:ASPxSpinEdit runat="server" Width="100%" 
                                ClientInstanceName="txtQuantidade"  
                                ID="txtQuantidade" DecimalPlaces="2" DisplayFormatString="{0:n2}">
<SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                <ClientSideEvents LostFocus="function(s, e) {
	verificaValorTotal();
}" />
</dxe:ASPxSpinEdit>

                        </td>
                        <td class="style13" >
                            <dxe:ASPxSpinEdit runat="server" Width="100%" 
                                ClientInstanceName="txtValorUnitario"  
                                ID="txtValorUnitario" DecimalPlaces="2" DisplayFormatString="{0:n2}">
<SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                <ClientSideEvents LostFocus="function(s, e) {
	verificaValorTotal();
}" />
</dxe:ASPxSpinEdit>

                        </td>
                        <td class="style7">
                            <dxe:ASPxSpinEdit runat="server" Width="100%" 
                                ClientInstanceName="txtValorTotal"  
                                ID="txtValorTotal" ClientEnabled="False" DecimalPlaces="2" 
                                DisplayFormatString="{0:n2}">
<SpinButtons ShowIncrementButtons="False"></SpinButtons>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
</dxe:ASPxSpinEdit>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td scope="height:5px" class="style12">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            <table>
            <td>
                <dxe:ASPxLabel runat="server" Text="Memória de Cálculo:" 
                    ClientInstanceName="lblNumeroContrato"  
                    ID="ASPxLabel4"></dxe:ASPxLabel>





 <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCarater" 
                     ForeColor="Silver" ID="lblCantCarater"></dxe:ASPxLabel>





 <dxe:ASPxLabel runat="server" Text="&amp;nbsp;de 4000" EncodeHtml="False" 
                    ClientInstanceName="lblDe250"  
                    ForeColor="Silver" ID="lblDe250"></dxe:ASPxLabel>





            </td>
                        <td style="padding-left: 10px">
                            <dxe:ASPxImage ID="ASPxImage3" runat="server" ClientInstanceName="imgAjuda" Cursor="pointer"
                                Height="18px" ImageUrl="~/imagens/ajuda.png" 
                                ToolTip="Informe memória de cálculo." Width="18px"
                                meta:resourcekey="ASPxImage10Resource1">
                            </dxe:ASPxImage>
                        </td>
                        </table>
                        </td>
        </tr>
        <tr>
            <td>
            <dxe:ASPxMemo runat="server" Rows="9" Width="100%" ClientInstanceName="mmObjeto" 
                     ID="mmObjeto">
<ClientSideEvents ValueChanged="function(s, e) {
	
}" Init="function(s, e) {
	onInit_mmObjeto(s, e);
}"></ClientSideEvents>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>





            </td>
        </tr>
        <tr>
            <td class="style12">
                </td>
        </tr>
        <tr>
            <td>
                    <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" 
                        
                        HeaderText="Planejamento Anual (Informe os valores mês a mês, a soma dos valores mensais tem que coincidir com o campo valor total)" 
                        Width="100%">
                        <ContentPaddings Padding="5px" />
                        <PanelCollection>
<dxp:PanelContent ID="PanelContent1" runat="server" >
                            <table cellpadding="0" cellspacing="0" class="style1">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server" 
                                            Text="Janeiro:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" 
                                            Text="Fevereiro:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" 
                                            Text="Março:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel13" runat="server" 
                                            Text="Abril:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel14" runat="server" 
                                            Text="Maio:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel15" runat="server" 
                                            Text="Junho:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan01" runat="server" 
                                            ClientInstanceName="txtPlan01"  
                                            Width="100%" DecimalPlaces="2" DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan02" runat="server" 
                                            ClientInstanceName="txtPlan02"  
                                            Width="100%" DecimalPlaces="2" DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan03" runat="server" 
                                            ClientInstanceName="txtPlan03"  
                                            Width="100%" DecimalPlaces="2" DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan04" runat="server" 
                                            ClientInstanceName="txtPlan04"  
                                            Width="100%" DecimalPlaces="2" DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan05" runat="server" 
                                            ClientInstanceName="txtPlan05"  
                                            Width="100%" DecimalPlaces="2" DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan06" runat="server" 
                                            ClientInstanceName="txtPlan06"  
                                            Width="100%" DecimalPlaces="2" DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style10" >
                                    </td>
                                    <td class="style10" >
                                    </td>
                                    <td class="style10" >
                                    </td>
                                    <td class="style10" >
                                    </td>
                                    <td class="style10" >
                                        &nbsp;</td>
                                    <td class="style10" >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel16" runat="server" 
                                            Text="Julho:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel17" runat="server" 
                                            Text="Agosto:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel18" runat="server" 
                                            Text="Setembro:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel19" runat="server" 
                                            Text="Outubro:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel20" runat="server" 
                                            Text="Novembro:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel21" runat="server" 
                                            Text="Dezembro:" Width="80px">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan07" runat="server" 
                                            ClientInstanceName="txtPlan07"  
                                            style="margin-bottom: 0px" Width="100%" DecimalPlaces="2" 
                                            DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan08" runat="server" 
                                            ClientInstanceName="txtPlan08"  
                                            style="margin-bottom: 0px" Width="100%" DecimalPlaces="2" 
                                            DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan09" runat="server" 
                                            ClientInstanceName="txtPlan09"  
                                            Width="100%" DecimalPlaces="2" DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan10" runat="server" 
                                            ClientInstanceName="txtPlan10"  
                                            Width="100%" DecimalPlaces="2" DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan11" runat="server" 
                                            ClientInstanceName="txtPlan11"  
                                            Width="100%" DecimalPlaces="2" DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="txtPlan12" runat="server" 
                                            ClientInstanceName="txtPlan12"  
                                            Width="100%" DecimalPlaces="2" DisplayFormatString="{0:n2}">
                                            <SpinButtons ShowIncrementButtons="False">
                                            </SpinButtons>
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                </tr>
                            </table>
                            </dxp:PanelContent>
</PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
        </tr>
        <tr>
            <td scope="height:5px">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <table>
                    <tr>
                        <td>
                <dxe:ASPxButton runat="server" AutoPostBack="False" Text="Salvar" Width="100px" 
                                 ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	verificaValorTotal();
	var valida = validaCamposFormulario();
	if(valida == '') 	
		callbackSalvar.PerformCallback();
    else
      window.top.mostraMensagem(valida, 'atencao', true, false, null);
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

                        </td>
                        <td align="right">
                    <dxe:ASPxButton ID="btnFechar" runat="server" 
                        Text="Fechar" Width="100px" ClientInstanceName="btnFechar" 
                        AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
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
    <dxhf:ASPxHiddenField ID="hfCodigoAcao" runat="server" 
        ClientInstanceName="hfCodigoAcao">
    </dxhf:ASPxHiddenField>

        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" 
            HeaderText="Incluir a Entidade Atual" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" 
            Width="420px"  ID="pcUsuarioIncluido">
            <ContentCollection>
<dxpc:PopupControlContentControl runat="server">
    <table cellSpacing="0" cellPadding="0" 
        width="100%" border="0"><tbody><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center">
            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" 
                 ID="lblAcaoGravacao" EncodeHtml="False"></dxe:ASPxLabel></td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

    <dxcb:ASPxCallback ID="callbackSalvar" runat="server" 
        ClientInstanceName="callbackSalvar" oncallback="callbackSalvar_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Retorno == 'S')
		window.top.retornoModal = s.cp_Retorno;

	if(s.cp_CodigoCronograma != null &amp;&amp; s.cp_CodigoCronograma != '')
		hfCodigoAcao.Set(&quot;CodigoCronograma&quot;, s.cp_CodigoCronograma);

	if(s.cp_Msg != null &amp;&amp; s.cp_Msg != '')
	 	mostraDivSalvoPublicado(s.cp_Msg);
}" />
    </dxcb:ASPxCallback>

    </form>
</body>
</html>
