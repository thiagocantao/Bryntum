<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalculoPesoObjetosCategoria.aspx.cs" Inherits="_Portfolios_Administracao_CalculoPesoObjetosCategoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript">
        function mostraPopupMensagemGravacao(acao) {
            lblAcaoGravacao.SetText(acao);
            pcMensagemGravacao.Show();
            setTimeout('fechaTelaEdicao();', 1500);
        }

        function fechaTelaEdicao() {
            pcMensagemGravacao.Hide();
            if (callbackSalvar.cp_status != null && callbackSalvar.cp_status == "1")
                window.parent.fechaModal();
        }

        function calculaPeso() {
            var novoValor = 1;
            var numeroLinhas = gridMatriz.GetVisibleRowsOnPage();
            var valorTotal = 0;

            for (i = 0; i < numeroLinhas; i++) {
                for (j = 0; j < numeroLinhas; j++) {
                    var txt2 = document.getElementById('gridMatriz_txt_' + i + '_' + (j + 1) + "_I");
                    if (txt2.value != "") {
                        var valor;
                        if (txt2.value.indexOf("/") == -1)
                            valor = parseFloat(txt2.value);
                        else {
                            valor = 1 / parseFloat(txt2.value.substring((txt2.value.indexOf("/") + 1)));
                        }


                        valorTotal = valorTotal + valor;
                    }

                }
            }

            for (i = 0; i < numeroLinhas; i++) {
                var valorLinha = 0;
                for (j = 0; j < numeroLinhas; j++) {
                    var txt2 = document.getElementById('gridMatriz_txt_' + i + '_' + (j + 1) + "_I");
                    if (txt2.value != "") {
                        var valor;
                        if (txt2.value.indexOf("/") == -1)
                            valor = parseFloat(txt2.value);
                        else {
                            valor = 1 / parseFloat(txt2.value.substring((txt2.value.indexOf("/") + 1)));
                        }

                        hfValores.Set('Criterio_' + i + '_' + (j + 1), txt2.value);

                        valorLinha = valorLinha + valor;
                    }
                }
                var dxo = new ASPxClientTextBox('gridMatriz_peso_' + i + '_' + (j + 1));
                window['peso'] = dxo;
                dxo.uniqueID = 'gridMatriz$peso_' + i + '_' + (j + 1);
                //aspxAddDisabledItems('gridMatriz_peso_' + i + '_' + (j + 1),[[['dxeDisabled'],['color:Black;background-color:#EBEBEB;'],['','I']]]);
                dxo.readOnly = true;
                dxo.RequireStyleDecoration();
                dxo.styleDecoration.AddStyle('F', 'dxeFocused', '');
                dxo.displayFormat = '{0:p2}';
                dxo.SetEnabled(false);
                dxo.SetValue(parseFloat((valorLinha / valorTotal)));
                dxo.InlineInitialize();
            }

        }

        function alteraValor(s, e, numeroLinha, numeroColuna) {
            try {
                if (s.GetText() == "") {
                    // var txt = new ASPxClientTextBox('gridMatriz_txt_' + (parseInt(numeroColuna) - 1) + '_' + (parseInt(numeroLinha) + 1));
                    var txt = document.getElementById('gridMatriz_txt_' + (parseInt(numeroColuna) - 1) + '_' + (parseInt(numeroLinha) + 1) + "_I");
                    txt.value = "";
                    hfValores.Set('Criterio_' + (parseInt(numeroColuna) - 1) + '_' + (parseInt(numeroLinha) + 1), "");
                    hfValores.Set('Criterio_' + numeroLinha + '_' + numeroColuna, "");
                }
                else {
                    if (verificaExpressao(s.GetText()) == false) {
                        window.top.mostraMensagem("Valor inválido! Verifique a legenda abaixo com os possíveis valores.", 'atencao', true, false, null);
                        s.SetFocus();
                        return;
                    }
                    var nome = s.name.substring(60);
                    //var numeroLinha = nome.substring(0, nome.indexOf('_'));
                    //var numeroColuna = nome.substring(nome.indexOf('_') + 1, nome.lastIndexOf('_'));
                    var novoValor = 1;
                    var numeroLinhas = gridMatriz.GetVisibleRowsOnPage();
                    var valorTotal = ((numeroLinhas * numeroLinhas) / 2) + (numeroLinhas / 2);

                    hfValores.Set('Criterio_' + numeroLinha + '_' + numeroColuna, s.GetText());

                    if (s.GetText() == 1)
                        novoValor = 1;
                    else {

                        if (s.GetText().indexOf("/") == -1) {
                            novoValor = "1/" + s.GetText();
                        }
                        else {
                            novoValor = s.GetText().substring((s.GetText().indexOf("/") + 1));
                        }
                    }
                    var txt = document.getElementById('gridMatriz_txt_' + (parseInt(numeroColuna) - 1) + '_' + (parseInt(numeroLinha) + 1) + "_I");

                    txt.value = novoValor;
                    hfValores.Set('Criterio_' + (parseInt(numeroColuna) - 1) + '_' + (parseInt(numeroLinha) + 1), novoValor);

                    calculaPeso();
                }
            } catch (e) { }
        }

        function verificaExpressao(expressao) {
            var regExp = new RegExp("^[1-9]{1}$");
            var valido = false;

            if (expressao.length == 1)
                valido = regExp.test(expressao);
            else if (expressao.length == 3) {
                if (expressao.indexOf("/") != -1) {
                    var valor1 = expressao.substring(0, 1);
                    var valor2 = expressao.substring((expressao.indexOf("/") + 1));

                    valido = (valor1 == 1 && regExp.test(valor2));
                }
            }

            return valido;
        }

        function funcaoCallbackSalvar() {
            callbackSalvar.PerformCallback();
        }

        function funcaoCallbackFechar() {
            window.parent.fechaModal();
        }

    </script>
    <style type="text/css">
        .style1 {
            width: 100%;
            border: 1px solid #464646;
        }

        .style4 {
            width: 50px;
            height: 20px;
        }

        .style5 {
            width: 155px;
            height: 20px;
        }

        .style9 {
            height: 20px;
        }

        .style6 {
            width: 50px;
            height: 25px;
        }

        .style7 {
            width: 155px;
            height: 25px;
        }

        .style8 {
            height: 25px;
        }

        .btn {
            text-transform: capitalize !important;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
               <td>
                <table style="width:100%">
                    <tr>
                        <td style="width:40px">
                            <dxe:ASPxLabel ID="lblCategoria" runat="server" 
                                Text="Categoria:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txtCategoria" runat="server" ClientEnabled="False"
                                Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td style="width:55px">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                Text="Fator:">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txtFator" runat="server" ClientEnabled="False"
                                Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>
                        <td>
                             <dxe:ASPxImage ID="imgHelp" runat="server" ClientInstanceName="imgHelp" 
                                    Cursor="Pointer" ImageUrl="~/imagens/ajuda.png" ToolTip="Ajuda">
                                    <ClientSideEvents Click="function(s, e) {
	popUpAjuda.Show();
}" />
                                </dxe:ASPxImage>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td>
                    <dxwgv:ASPxGridView ID="gridMatriz" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridMatriz"
                         KeyFieldName="Grupo" Width="100%" OnHtmlDataCellPrepared="gridMatriz_HtmlDataCellPrepared">
                        <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" AllowSort="False" />
                        <Styles>
                            <Header Wrap="True" HorizontalAlign="Left">
                                <Paddings PaddingBottom="3px" PaddingTop="3px" />
                            </Header>
                            <FocusedRow BackColor="White" ForeColor="Black">
                            </FocusedRow>
                            <Cell CssClass="linhas" HorizontalAlign="Center">
                                <Paddings PaddingBottom="0px" PaddingTop="0px" />
                            </Cell>
                        </Styles>
                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                        </SettingsPager>
                        <SettingsEditing Mode="Inline" />

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                        <SettingsText EmptyDataRow="Nenhum Crit&#233;rio foi Selecionado!" />
                        <ClientSideEvents Init="function(s, e) {
	                    calculaPeso();
                    }" />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Grupos" FieldName="Criterio" VisibleIndex="0">
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Grupo A" Visible="False" VisibleIndex="1">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="325" ShowFooter="False" />
                    </dxwgv:ASPxGridView>
                    <dxhf:ASPxHiddenField ID="hfValores" runat="server" ClientInstanceName="hfValores">
                    </dxhf:ASPxHiddenField>
                </td>
            </tr>
        </table>
    
    </div>
        <dxpc:ASPxPopupControl ID="pcMensagemGravacao" runat="server" ClientInstanceName="pcMensagemGravacao"
             HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
            Width="270px">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td align="center" style="">
                                </td>
                                <td align="center" rowspan="3" style="width: 70px">
                                    <dxe:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar" ImageAlign="TextTop"
                                        ImageUrl="~/imagens/Workflow/salvarBanco.png">
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <dxe:ASPxLabel ID="lblAcaoGravacao" runat="server" ClientInstanceName="lblAcaoGravacao"
                                        >
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxcb:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar"
            OnCallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_msg != null &amp;&amp; s.cp_msg != &quot;&quot;)
		mostraPopupMensagemGravacao(s.cp_msg);
}" />
        </dxcb:ASPxCallback>
    <dxpc:ASPxPopupControl ID="popUpAjuda" runat="server" 
        ClientInstanceName="popUpAjuda"  
        HeaderText="Ajuda" Modal="True" PopupElementID="imgHelp" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        Width="770px" AllowDragging="True">
        <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table cellpadding="0" cellspacing="0" class="style1">
        <tr>
            <td align="center" class="style4" 
                style="border: 1px solid #808080; background-color: #CCCCCC; font-weight: bold;">
                Valor</td>
            <td class="style5" 
                style="border: 1px solid #808080; background-color: #CCCCCC; font-weight: bold; padding-left: 2px">
                Significado</td>
            <td class="style9" 
                style="border: 1px solid #808080; background-color: #CCCCCC; font-weight: bold; padding-left: 2px">
                Descrição</td>
        </tr>
        <tr>
            <td align="center" class="style6" style="border: 1px solid #808080">
                1</td>
            <td class="style7" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                Relevância equivalente</span></td>
            <td class="style8" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                Os elementos da linha e coluna têm a mesma relevância.</span></td>
        </tr>
        <tr>
            <td align="center" class="style6" style="border: 1px solid #808080">
                3</td>
            <td class="style7" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                Relevância moderada</span></td>
            <td class="style8" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                Julga-se que o elemento da linha tem uma relevância moderadamente maior que o 
                elemento da coluna.</span></td>
        </tr>
        <tr>
            <td align="center" class="style6" style="border: 1px solid #808080">
                5</td>
            <td class="style7" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                Forte relevância</span></td>
            <td class="style8" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                Julga-se que o elemento da linha tem uma relevância bem maior que o elemento da 
                coluna.</span></td>
        </tr>
        <tr>
            <td align="center" class="style6" style="border: 1px solid #808080">
                7</td>
            <td class="style7" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                Relevância Demonstrada</span></td>
            <td class="style8" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                Há histórico de que o elemento da linha tem relevância bem maior que o elemento 
                da coluna.</span></td>
        </tr>
        <tr>
            <td align="center" class="style6" style="border: 1px solid #808080">
                9</td>
            <td class="style7" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                Absoluta relevância</span></td>
            <td class="style8" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                A relevância do elemento da linha em relação ao da coluna é inquestionável.</span></td>
        </tr>
        <tr>
            <td align="center" class="style6" style="border: 1px solid #808080">
                2,4,6,8</td>
            <td class="style7" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                Valores intermediários</span></td>
            <td class="style8" style="border: 1px solid #808080; padding-left: 2px">
                <span style="line-height: 115%; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: PT-BR; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                Valores utilizados para indicar um nível intermediário entre os valores acima.</span></td>
        </tr>
    </table>
            </dxpc:PopupControlContentControl>
</ContentCollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
