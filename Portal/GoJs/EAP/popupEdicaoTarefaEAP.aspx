<%@ Page Language="C#" ResponseEncoding="utf-8" AutoEventWireup="true" CodeFile="popupEdicaoTarefaEAP.aspx.cs"
    Inherits="_Projetos_DadosProjeto_popupEdicaoTarefaEAP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pacote de Trabalho - Detalhes</title>
    <base target="_self" />
    <script type="text/javascript" language="javascript">

        var indicaPesoAlterado = false;

        function buscaNomeBD(objeto) {
            hfGeral.Set("lovCodigoResponsavel", "");
            nome = objeto.GetText();
            if (nome != "") {
                pnCallback.PerformCallback("PesquisarResp");
            }
            else {
                btnSalvar.SetEnabled(true);
            }
        }

        function preparaWhereGetLovNomeValor() {
            var where = new String();
            where = hfGeral.Contains("hfWheregetLov_NomeValor") ? hfGeral.Get("hfWheregetLov_NomeValor") : "dataExclusao is null";
            if (hfGeral.Contains("hfWheregetLov_NomeValor")) {
                if (where.substring(0, 3) == "and") {
                    where = where.substring(4, where.length);
                }
            }
            return where;
        }

        function mostraLov() {
            var where = preparaWhereGetLovNomeValor();
            var retorno = window.showModalDialog('../../lov.aspx?tab=usuario&val=codigoUsuario&nom=nomeUsuario&whe=' + where + '&ord=nomeUsuario&Pes=' + txtResponsavel.GetText(), '', 'resizable:0; dialogWidth:520px; dialogHeight:465px; status:no; menubar=no;');
            hfGeral.Set("lovMostrarPopPup", "0");
            if (retorno && retorno != "") {
                var aRetorno = retorno.split(';');
                hfGeral.Set("lovCodigoResponsavel", aRetorno[0]);
                txtResponsavel.SetText(aRetorno[1]);
            }
            else {
                txtResponsavel.SetText("");
                hfGeral.Set("lovCodigoResponsavel", "");
            }
            pnCallback.PerformCallback("habilitaSalvar");
        }

        function getDuracao() {
            if (dteInicio.GetText() != '' && dteTermino.GetText() != '' && dteTermino.GetDate() >= dteInicio.GetDate()) {
                var diferenca = calculaDiasUteis(dteInicio.GetDate(), dteTermino.GetDate()); //diferença em milésimos e positivo

                txtDuracao.SetValue(diferenca);
            }
            else
                txtDuracao.SetValue(null);
        }

        function calculaDiasUteis(dDate1, dDate2) { // input given as Date objects
            var iWeeks, iDateDiff, iAdjust = 0;
            if (dDate2 < dDate1) return -1; // error code if dates transposed
            var iWeekday1 = dDate1.getDay(); // day of week
            var iWeekday2 = dDate2.getDay();
            iWeekday1 = (iWeekday1 == 0) ? 7 : iWeekday1; // change Sunday from 0 to 7
            iWeekday2 = (iWeekday2 == 0) ? 7 : iWeekday2;
            if ((iWeekday1 > 5) && (iWeekday2 > 5)) iAdjust = 1; // adjustment if both days on weekend
            iWeekday1 = (iWeekday1 > 5) ? 5 : iWeekday1; // only count weekdays
            iWeekday2 = (iWeekday2 > 5) ? 5 : iWeekday2;

            // calculate differnece in weeks (1000mS * 60sec * 60min * 24hrs * 7 days = 604800000)
            iWeeks = Math.floor((dDate2.getTime() - dDate1.getTime()) / 604800000)

            if (iWeekday1 <= iWeekday2) {
                iDateDiff = (iWeeks * 5) + (iWeekday2 - iWeekday1)
            } else {
                iDateDiff = ((iWeeks + 1) * 5) - (iWeekday1 - iWeekday2)
            }

            iDateDiff -= iAdjust // take into account both days on weekend

            return (iDateDiff + 1); // add 1 because dates are inclusive
        }

        function atualizarDadosPopup() {
            var documento = obtemDocumentoGraficoEap();
            if (documento != null) {
                documento.atualizaDadosPopup(txtTarefa, dteInicio, dteTermino, txtDuracao, txtTrabalho, txtCusto, txtReceita, txtPeso, ddlResponsavel, mmCriterios, txtDicionario);
                lbl_mmDicionario.SetText(txtDicionario.GetInputElement().value.length + ' de 4000');
                lbl_mmCriterios.SetText(mmCriterios.GetInputElement().value.length + ' de 4000');
                //atualizarValorUnitario();
            }
        }

        function atualizarInformacoes() {
            var documento = obtemDocumentoGraficoEap();
            if (documento != null)
                documento.atualizaInformacoesEAP(txtTarefa.GetText(), dteInicio.GetText(), dteTermino.GetText(), txtDuracao.GetText(), txtTrabalho.GetText(), txtCusto.GetText(), txtReceita.GetText(), txtPeso.GetText(), ddlResponsavel.GetValue(), mmCriterios.GetText(), txtDicionario.GetText(), indicaPesoAlterado);
        }

        function obtemDocumentoGraficoEap(documento) {
            if (documento == undefined) {
                var popup = {};
                if (window.top.painelAtual.name == 'pcModal')
                    popup = window.top.pcModal2;
                else
                    popup = window.top.pcModal;

                var contentFrame = popup.GetContentIFrameWindow();

                return obtemDocumentoGraficoEap(contentFrame);
            }
            if (documento.location.pathname.indexOf('/GoJs/EAP/graficoEAP.aspx') > -1)
                return documento;

            for (var i = 0; i < documento.frames.length; i++) {
                var frame = obtemDocumentoGraficoEap(documento.frames[i]);
                if (frame != null)
                    return frame;
            }

            return null;
        }

        function onValueChanged(s, e) {
            //atualizarValorTotal();
        }

        function atualizarValorTotal() {
            var funcParse = function (strValue) {
                return parseFloat(strValue.split(".").join("").replace(",", "."));
            };
            var valorUnitario = funcParse(txtCustoUnitario.GetText());
            var quatidadeHoras = funcParse(txtTrabalho.GetText());
            var valorTotal = valorUnitario * quatidadeHoras;

            txtCusto.SetText(valorTotal.toLocaleString('pt-BR', { style: 'decimal', minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        }

        function atualizarValorUnitario() {
            var funcParse = function (strValue) {
                return parseFloat(strValue.split(".").join("").replace(",", "."));
            };
            var valorTotal = funcParse(txtCusto.GetText());
            var quatidadeHoras = funcParse(txtTrabalho.GetText());
            var valorUnitario = quatidadeHoras == 0 ? 0 : valorTotal / quatidadeHoras;;

            txtCustoUnitario.SetText(valorUnitario.toLocaleString('pt-BR', { style: 'decimal', minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        }

    </script>
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <style type="text/css">
        .btn_inicialMaiuscula {
            text-transform: capitalize !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
            <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
            </dxhf:ASPxHiddenField>
        <div>  
           <div id="divConteudo" style="overflow:auto;">
                <table cellspacing="0" cellpadding="0" border="0" style="width: 100%;">
                <tbody>
                    <tr>
                        <td align="left">
                                                        <dxe:ASPxLabel runat="server" Text="* "
                                                            ID="ASPxLabel15" ForeColor="Red">
                                                        </dxe:ASPxLabel>
                            <dxtv:ASPxLabel ID="ASPxLabel9" runat="server" Text="Item:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <dxcp:ASPxMemo ID="txtTarefa" runat="server" ClientEnabled="False" ClientInstanceName="txtTarefa" Width="100%" Rows="2" MaxLength="255">
                                <ValidationSettings ErrorDisplayMode="None" ErrorText="">
                                    <RequiredField ErrorText="" />
                                </ValidationSettings>
                                <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                </DisabledStyle>
                            </dxcp:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tbody>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0" class="formulario-colunas" style="width: 100%">
                                                <tbody>
                                                    <tr>
                                                        <td valign="bottom" style="width: 33%">
                                                            <dxe:ASPxLabel runat="server" Text="In&#237;cio:"
                                                                ID="ASPxLabel4">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="bottom" style="width: 33%">
                                                            <dxe:ASPxLabel runat="server" Text="T&#233;rmino:"
                                                                ID="ASPxLabel2">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="bottom">
                                                            <dxe:ASPxLabel runat="server" Text="Dura&#231;&#227;o (dias):"
                                                                ID="ASPxLabel3">
                                                                <DisabledStyle>
                                                                </DisabledStyle>
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxDateEdit runat="server"
                                                                Width="100%" ClientInstanceName="dteInicio"
                                                                ID="dteInicio">
                                                                <CalendarProperties ClearButtonText="Limpar">
                                                                </CalendarProperties>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
	getDuracao();
}" />
                                                                <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                                </DisabledStyle>
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxDateEdit runat="server"
                                                                Width="100%" ClientInstanceName="dteTermino"
                                                                ID="dteTermino">
                                                                <CalendarProperties ClearButtonText="Limpar">
                                                                </CalendarProperties>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
	getDuracao();
}" />
                                                                <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                                </DisabledStyle>
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>
                                                            <dxcp:ASPxSpinEdit ID="txtDuracao" runat="server" AllowMouseWheel="False" ClientEnabled="False" ClientInstanceName="txtDuracao" HorizontalAlign="Right" Width="100%">
                                                                <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxcp:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" class="formulario-colunas" style="width: 100%">
                                                <tr>
                                                    <td valign="bottom" style="width: 33%">
                                                        <dxe:ASPxLabel runat="server" Text="Trabalho (horas):"
                                                            ID="ASPxLabel5">
                                                            <DisabledStyle>
                                                            </DisabledStyle>
                                                        </dxe:ASPxLabel>
                                                        
                                                    </td>
                                                    <td style="width: 33%">
                                                        <dxe:ASPxLabel runat="server" Text="Custo (R$):"
                                                            ID="ASPxLabel13">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Receita (R$):"
                                                            ID="ASPxLabel10">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtTrabalho"
                                                            ID="txtTrabalho" HorizontalAlign="Right">
                                                            <ClientSideEvents ValueChanged="onValueChanged" />
                                                            <MaskSettings Mask="&lt;0..99999999999g&gt;.&lt;00..99&gt;"></MaskSettings>
                                                            <ValidationSettings ErrorDisplayMode="None" ValidateOnLeave="False" ErrorText="">
                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                <RequiredField ErrorText=""></RequiredField>
                                                            </ValidationSettings>
                                                            <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                            </DisabledStyle>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtCusto"
                                                            ID="txtCusto" HorizontalAlign="Right">
                                                            <MaskSettings Mask="&lt;0..99999999999g&gt;.&lt;00..99&gt;" />
                                                            <ValidationSettings ErrorDisplayMode="None" ValidateOnLeave="False" ErrorText="">
                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                <RequiredField ErrorText=""></RequiredField>
                                                            </ValidationSettings>
                                                            <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                            </DisabledStyle>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtReceita"
                                                            ID="txtReceita" HorizontalAlign="Right">
                                                            <MaskSettings Mask="&lt;0..99999999999g&gt;.&lt;00..99&gt;"></MaskSettings>
                                                            <ValidationSettings ErrorDisplayMode="None" ValidateOnLeave="False" ErrorText="">
                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                <RequiredField ErrorText=""></RequiredField>
                                                            </ValidationSettings>
                                                            <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                            </DisabledStyle>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Respons&#225;vel:"
                                                            ID="ASPxLabel7">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 34%">
                                                        <dxe:ASPxLabel runat="server" Text="Peso:"
                                                            ID="ASPxLabel11">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxComboBox ID="ddlResponsavel" runat="server" ClientInstanceName="ddlResponsavel" EnableCallbackMode="True" Width="100%" ValueField="CodigoUsuario" TextField="NomeRecursoCorporativo">
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxtv:ASPxComboBox>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtPeso"
                                                            ID="txtPeso">
                                                            <ClientSideEvents ValueChanged="function(s, e) {
	indicaPesoAlterado = true;
}" />
                                                            <MaskSettings Mask="&lt;0..99999999999g&gt;.&lt;00..99&gt;"></MaskSettings>
                                                            <Paddings PaddingRight="0px" />
                                                            <ValidationSettings ErrorDisplayMode="None" ValidateOnLeave="False" ErrorText="">
                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                <RequiredField ErrorText=""></RequiredField>
                                                            </ValidationSettings>
                                                            <DisabledStyle ForeColor="Black" BackColor="#EBEBEB">
                                                            </DisabledStyle>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" class="formulario-colunas" style="width: 100%">
                                                <tr>
                                                    <td style="width: 50%">
                                                        <dxe:ASPxLabel runat="server" Text="Descrição:"
                                                            ID="ASPxLabel8">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <dxe:ASPxLabel runat="server" Text="Critérios de Aceitação:"
                                                            ID="ASPxLabel12">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxcp:ASPxMemo runat="server" Width="100%" ClientInstanceName="txtDicionario" TabIndex="6" ID="txtDicionario" Height="225px">
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxcp:ASPxMemo>
                                                        <dxcp:ASPxLabel runat="server" ClientInstanceName="lbl_mmDicionario" Font-Bold="True" ForeColor="#999999" ID="lbl_mmDicionario">
                                                        </dxcp:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxcp:ASPxMemo runat="server" Width="100%" ClientInstanceName="mmCriterios" TabIndex="6" ID="mmCriterios" Height="225px">
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxcp:ASPxMemo>
                                                        <dxcp:ASPxLabel runat="server" ClientInstanceName="lbl_mmCriterios" Font-Bold="True" ForeColor="#999999" ID="lbl_mmCriterios">
                                                        </dxcp:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="formulario-nao-botao">
                                                            <dxe:ASPxLabel ID="lblResponsavel" runat="server" ClientInstanceName="lblResponsavel">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Aplicar" Width="90px"
                                                                ID="btnSalvar" CssClass="btn_inicialMaiuscula">
                                                                <ClientSideEvents Click="function(s, e) 
{
    hfGeral.Set(&quot;lovMostrarPopPup&quot;, &quot;0&quot;);
	e.processOnServer = false;	
	if (window.validaCamposFormulario)
	{
    	if(validaCamposFormulario() != &quot;&quot;)
		{
			window.top.mostraMensagem(validaCamposFormulario(), 'atencao', true, false, null);
		}
		else
		{
            atualizarInformacoes();                                                              
			//window.top.painelAtual.GetContentIFrameWindow().atualizaInformacoesEAP(txtTarefa.GetText(), dteInicio.GetText(), dteTermino.GetText(), txtDuracao.GetText(), txtTrabalho.GetText(), txtCusto.GetText(), txtReceita.GetText(), txtPeso.GetText(), ddlResponsavel.GetValue(), mmCriterios.GetText(), txtDicionario.GetText());
window.top.fechaModal3();	     
	    }


        alert(traducao.popupEdicaoTarefaEAP_item_da_eap_salvo_com_sucesso_);
	}
}"></ClientSideEvents>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="90px"
                                                                ID="btnCancelar" CssClass="btn_inicialMaiuscula">
                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	window.top.fechaModal3();
}"></ClientSideEvents>
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
            </div>
        
            </div>
            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" HeaderText=""
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                ShowHeader="False" Width="270px" ID="pcMensagemGravacao">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tbody>
                                <tr>
                                    <td align="center" style=""></td>
                                    <td align="center" rowspan="3" style="width: 70px">
                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                            ClientInstanceName="imgSalvar" ID="imgSalvar">
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px"></td>
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

        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
        <dxcp:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents ControlsInitialized="function(s, e) {
	if(!e.isCallback){
                
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 20;
        document.getElementById('divConteudo').style.height = sHeight + 'px';
        atualizarDadosPopup();
    }
}" />
        </dxcp:ASPxGlobalEvents>
    </form>
</body>
</html>
