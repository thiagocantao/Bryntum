<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reuniaoTecnicaRealizacao.aspx.cs"
    Inherits="Reunioes_reuniaoTecnicaRealizacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../estilos/custom.css" rel="stylesheet" />
    <title>Realização da Reunião</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../scripts/barraNavegacao.js"></script>
    <script type="text/javascript">

        function replaceAll(origem, antigo, novo) {
            var teste = 0;
            while (teste == 0) {
                if (origem.indexOf(antigo) >= 0) {
                    origem = origem.replace(antigo, novo);
                }
                else
                    teste = 1;
            }
            return origem;
        }

        var eventoOKMsg = null;

        function mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK) {

            if (nomeImagem != null && nomeImagem != '')
                imgApresentacaoAcao.SetImageUrl(pcApresentacaoAcao.cp_Path + 'imagens/' + nomeImagem + '.png');
            else
                imgApresentacaoAcao.SetVisible(false);

            textoMsg = replaceAll(textoMsg, '\n', '<br/>');

            lblMensagemApresentacaoAcao.SetText(textoMsg);
            btnOkApresentacaoAcao.SetVisible(mostraBtnOK);
            btnCancelarApresentacaoAcao.SetVisible(mostraBtnCancelar);
            pcApresentacaoAcao.Show();
            eventoOKMsg = eventoOK;

            if (!mostraBtnOK && !mostraBtnCancelar) {
                setTimeout('fechaMensagem();', 3500);
            }
        }

        function fechaMensagem() {
            lblMensagemApresentacaoAcao.SetText('');

            pcApresentacaoAcao.AdjustSize();
            pcApresentacaoAcao.Hide();
        }
        var frmAnexos = '';
        var atualizarURLAnexos = '';
        window.moveTo(0, 0);
        if (document.all) {
            top.window.resizeTo(screen.availWidth, screen.availHeight);



        }
        else {
            if (document.layers || document.getElementById) {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {


                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;
                }

                try {
                    top.window.resizeTo(screen.availWidth, screen.availHeight);
                } catch (e) {
                }
            }
        }


        function podeMudarAba(s, e) {
            if (e.tab.name == "tabPlanoAcao")
                getValoresPlanoAcao();
            else if (e.tab.name == "tabAnexos")
                getValoresAnexo();
            return false;
        }

        function getValoresAnexo() {
            var codigoEvento = hfGeral.Get("codigoEvento");
            var altura = hfGeral.Get("alturaTela") - 120;
            var readOnly = hfGeral.Get("readOnly");
            hfGeral.Set("TipoOperacao", "Editar");
            atualizarURLAnexos = 'S';
            frmAnexos = '../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&TA=RE&ID=' + codigoEvento + '&RO=' + readOnly + '&ALT=' + altura + '&TO=' + hfGeral.Get("TipoOperacao").toString() + '&Popup=S';
            document.getElementById('frmAnexosReuniao').src = frmAnexos;
            document.getElementById('frmAnexosReuniao').height = altura + 20;

        }


        function getValoresPlanoAcao() {
            var codigoEvento = hfGeral.Get("codigoEvento");

            if (window.hfGeralToDoList) {
                hfGeral.Set("TipoOperacao", "Editar");
                hfGeral.Set("codigoObjetoAssociado", codigoEvento);
                hfGeralToDoList.Set("codigoObjetoAssociado", codigoEvento);
                gvToDoList.PerformCallback("Popular");
            }
            else
                mostraMensagem("Não foi possível encontrar o componente Plano de Ação", 'Atencao', true, false, null);
        }

        function SalvarCamposFormulario(TipoOperacao) {   // esta função chama o método no servidor responsável por persistir as informações no banco
            // o método será chamado por meio do objeto pnCallBack
            hfGeral.Set("StatusSalvar", "0");
            pnCallback.PerformCallback(TipoOperacao);
            return false;
        }

        function verificarDadosPreenchidos() {
            var mensagemError = "";
            var retorno = true;
            var numError = 0;
            //------------Obtendo data e hora actual
            var momentoActual = new Date();
            var horaActual = momentoActual.getHours() + ':' + momentoActual.getMinutes();
            var arrayHoraAgora = horaActual.split(':');

            var meuDataAtual = (momentoActual.getMonth() + 1) + '/' + momentoActual.getDate() + '/' + momentoActual.getFullYear();
            var dataHoje = Date.parse(meuDataAtual);

            var arrayHoraRealInicio = txtHoraInicioAta.GetText().split(':');
            var arrayHoraRealFinal = txtHoraTerminoAta.GetText().split(':');

            var dataInicioReal = new Date(ddlInicioReal.GetValue());
            var dataInicioRealP = (dataInicioReal.getMonth() + 1) + '/' + dataInicioReal.getDate() + '/' + dataInicioReal.getFullYear();
            var dataInicioRealC = Date.parse(dataInicioRealP);

            var dataTerminoReal = new Date(ddlTerminoReal.GetValue());
            var dataTerminoRealP = (dataTerminoReal.getMonth() + 1) + '/' + dataTerminoReal.getDate() + '/' + dataTerminoReal.getFullYear();
            var dataTerminoRealC = Date.parse(dataTerminoRealP);

            //------------- ***    

            if (ddlInicioReal.GetValue() == null) {
                mensagemError += ++numError + ") A Data de Inicio Real do Evento deve ser informada!\n";
                retorno = false;
            }

            if (ddlTerminoReal.GetValue() == null) {
                mensagemError += ++numError + ") A Data de Término Real do Evento deve ser informada!\n";
                retorno = false;
            }

            if ((dataHoje < dataInicioRealC) && (ddlInicioReal.GetValue() != null)) {
                mensagemError += ++numError + ") A Data de Inicio Real do Evento não pode ser maior que a data atual!\n";
                retorno = false;
            }

            if ((dataHoje < dataTerminoRealC) && (ddlTerminoReal.GetValue() != null)) {
                mensagemError += ++numError + ") A Data de Termino Real do Evento não pode ser maior que a data atual!\n";
                retorno = false;
            }

            if (dataInicioRealC > dataTerminoRealC) {
                mensagemError += ++numError + ") A Data de Inicio Real do Evento não pode ser maior que a Data de Termino Real!\n";
                retorno = false;
            }

            //----------- ***

            if (!retorno)
                mostraMensagem(mensagemError, 'erro', true, false, null);

            return retorno;
        }

        function verificarDadosAta() {

            var mensagemError = "";
            var retorno = true;
            var numError = 0;
            //------------Obtendo data e hora actual
            var momentoActual = new Date();
            var horaActual = momentoActual.getHours() + ':' + momentoActual.getMinutes();
            var arrayHoraAgora = horaActual.split(':');
            var meuDataAtual = (momentoActual.getMonth() + 1) + '/' + momentoActual.getDate() + '/' + momentoActual.getFullYear();
            var dataHoje = Date.parse(meuDataAtual);

            var dataInicioReal = new Date(ddlInicioReal.GetValue());
            var dataInicioRealP = (dataInicioReal.getMonth() + 1) + '/' + dataInicioReal.getDate() + '/' + dataInicioReal.getFullYear();
            var dataInicioRealC = Date.parse(dataInicioRealP);

            var dataTerminoReal = new Date(ddlTerminoReal.GetValue());
            var dataTerminoRealP = (dataTerminoReal.getMonth() + 1) + '/' + dataTerminoReal.getDate() + '/' + dataTerminoReal.getFullYear();
            var dataTerminoRealC = Date.parse(dataTerminoRealP);

            var arrayMomentoInicio = txtHoraInicioAta.GetText().split(':');
            var arrayMomentoFinal = txtHoraTerminoAta.GetText().split(':');
            //------------- ***

            if (ddlInicioReal.GetValue() == null) {
                mensagemError += ++numError + ") A Data de Início Real da Reunião deve ser informada!\n";
                retorno = false;
            }

            if (ddlTerminoReal.GetValue() == null) {
                mensagemError += ++numError + ") A Data de Término Real da Reunião deve ser informada!\n";
                retorno = false;
            }

            if (dataHoje < dataInicioRealC) {
                mensagemError += ++numError + ") A Data de Início Real da Reunião não pode ser maior que a data atual!\n";
                retorno = false;
            }

            if (dataHoje < dataTerminoRealC) {
                mensagemError += ++numError + ") A Data de Término Real da Reunião não pode ser maior que a data atual!\n";
                retorno = false;
            }

            if (dataInicioRealC > dataTerminoRealC) {
                mensagemError += ++numError + ") A Data de Início Real da Reunião não pode ser maior que a Data de Término Real!\n";
                retorno = false;
            }

            if (dataHoje == dataInicioRealC && arrayHoraAgora[0] < arrayMomentoInicio[0]) {
                mensagemError += ++numError + ") A Hora de Início Real indicada não pode ser superior à Hora Atual!\n";
                retorno = false;

            }
            if (dataHoje == dataInicioRealC && arrayHoraAgora[0] == arrayMomentoInicio[0]) {
                if (arrayHoraAgora[1] < arrayMomentoInicio[1]) {
                    mensagemError += ++numError + ") A Hora de Início Real indicada não pode ser superior à Hora Atual!\n";
                    retorno = false;
                }
            }

            if (dataHoje == dataTerminoRealC && arrayHoraAgora[0] < arrayMomentoFinal[0]) {
                mensagemError += ++numError + ") A Hora de Término Real indicada não pode ser superior à Hora Atual!\n";
                retorno = false;

            }

            if (dataHoje == dataTerminoRealC && arrayHoraAgora[0] == arrayMomentoFinal[0]) {
                if (arrayHoraAgora[1] < arrayMomentoFinal[1]) {
                    mensagemError += ++numError + ") A Hora de Término Real indicada não pode ser superior à Hora Atual!\n";
                    retorno = false;
                }
            }

            if (window.memoAta) {
                if (memoAta.GetHtml() == "") {
                    mensagemError += ++numError + ") A Ata da Reunião deve ser informada!\n";
                    retorno = false;
                }
            }
            if (!retorno)
                mostraMensagem(mensagemError, 'erro', true, false, null);

            return retorno;
        }



        function MaximizaTela() {


            var e = document.getElementById("corpo");



            if (RunPrefixMethod(document, "FullScreen") || RunPrefixMethod(document, "IsFullScreen")) {
                RunPrefixMethod(document, "CancelFullScreen");
            }
            else {
                RunPrefixMethod(e, "RequestFullScreen");
            }

        }

        function RunPrefixMethod(obj, method) {

            var pfx = ["webkit", "moz", "ms", "o", ""];


            var p = 0, m, t;
            while (p < pfx.length && !obj[m]) {
                m = method;
                if (pfx[p] == "") {
                    m = m.substr(0, 1).toLowerCase() + m.substr(1);
                }
                m = pfx[p] + m;
                t = typeof obj[m];
                if (t != "undefined") {
                    pfx = [pfx[p]];
                    return (t == "function" ? obj[m]() : obj[m]);
                }
                p++;
            }

        }



    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body style="margin: 0px; overflow: auto" id="bodyDoc" onload="MaximizaTela();">
    <section id="corpo">
        <form id="form1" runat="server">
            <div>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr style="height: 26px">
                        <td valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" CssClass="TituloTela" ClientInstanceName="lblTituloTela"
                                Font-Bold="True">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
                <table width="100%">

                    <tr>

                        <td>
                            <dxe:ASPxLabel ID="lblUnidade" runat="server"
                                Text="Unidade:">
                            </dxe:ASPxLabel>
                        </td>

                    </tr>
                    <tr>

                        <td>
                            <dxe:ASPxTextBox ID="txtUnidade" runat="server" ClientEnabled="False"
                                Width="100%">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxTextBox>
                        </td>

                    </tr>
                    <tr>

                        <td>
                            <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                                Width="100%" OnCallback="pnCallback_Callback">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <dxtc:ASPxPageControl runat="server"
                                            ActiveTabIndex="0" Width="100%"
                                            ID="ASPxPageControl1" ClientInstanceName="ASPxPageControl1">
                                            <TabPages>
                                                <dxtc:TabPage Name="tabReuniao" Text="Reuni&#227;o">
                                                    <ContentCollection>
                                                        <dxw:ContentControl runat="server">
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label runat="server" Text="Assunto:" ID="lblAssunto"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="100"
                                                                                ClientInstanceName="txtAssunto" ClientEnabled="False"
                                                                                ID="txtAssunto">
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE">
                                                                                    <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxTextBox>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label runat="server" Text="Pauta:" ID="lblPauta"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="memoPauta"
                                                                                ActiveView="Preview" Width="100%" Height="100px"
                                                                                ID="memoPauta">
                                                                                <Toolbars>
                                                                                    <dxhe:HtmlEditorToolbar>
                                                                                        <Items>
                                                                                            <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                                                                                <Items>
                                                                                                    <dxhe:ToolbarListEditItem Text="Normal" Value="p"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Address" Value="address"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div"></dxhe:ToolbarListEditItem>
                                                                                                </Items>
                                                                                            </dxhe:ToolbarParagraphFormattingEdit>
                                                                                            <dxhe:ToolbarFontNameEdit>
                                                                                                <Items>
                                                                                                    <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Arial" Value="Arial"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Courier" Value="Courier"></dxhe:ToolbarListEditItem>
                                                                                                </Items>
                                                                                            </dxhe:ToolbarFontNameEdit>
                                                                                            <dxhe:ToolbarFontSizeEdit>
                                                                                                <Items>
                                                                                                    <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7"></dxhe:ToolbarListEditItem>
                                                                                                </Items>
                                                                                            </dxhe:ToolbarFontSizeEdit>
                                                                                            <dxhe:ToolbarBoldButton BeginGroup="True"></dxhe:ToolbarBoldButton>
                                                                                            <dxhe:ToolbarItalicButton></dxhe:ToolbarItalicButton>
                                                                                            <dxhe:ToolbarUnderlineButton></dxhe:ToolbarUnderlineButton>
                                                                                            <dxhe:ToolbarStrikethroughButton></dxhe:ToolbarStrikethroughButton>
                                                                                            <dxhe:ToolbarJustifyLeftButton BeginGroup="True"></dxhe:ToolbarJustifyLeftButton>
                                                                                            <dxhe:ToolbarJustifyCenterButton></dxhe:ToolbarJustifyCenterButton>
                                                                                            <dxhe:ToolbarJustifyRightButton></dxhe:ToolbarJustifyRightButton>
                                                                                            <dxhe:ToolbarJustifyFullButton></dxhe:ToolbarJustifyFullButton>
                                                                                            <dxhe:ToolbarBackColorButton BeginGroup="True"></dxhe:ToolbarBackColorButton>
                                                                                            <dxhe:ToolbarFontColorButton></dxhe:ToolbarFontColorButton>
                                                                                        </Items>
                                                                                    </dxhe:HtmlEditorToolbar>
                                                                                </Toolbars>
                                                                                <Settings AllowDesignView="False" AllowHtmlView="False"></Settings>
                                                                                <SettingsSpellChecker Culture="Portuguese (Brazil)">
                                                                                    <Dictionaries>
                                                                                        <dxwsc:ASPxSpellCheckerDictionary AlphabetPath="~/DevExpress_Trad/ASPxSpellCheckerForms/pt_BR.aff" DictionaryPath="~/DevExpress_Trad/ASPxSpellCheckerForms/pt_BR.dic" Culture="Portuguese (Brazil)" EncodingName="Unicode (UTF-8)"></dxwsc:ASPxSpellCheckerDictionary>
                                                                                        <dxwsc:ASPxSpellCheckerDictionary AlphabetPath="~/DevExpress_Trad/ASPxSpellCheckerForms/es_ES.aff" DictionaryPath="~/DevExpress_Trad/ASPxSpellCheckerForms/es_ES.dic" Culture="Spanish"></dxwsc:ASPxSpellCheckerDictionary>
                                                                                        <dxwsc:ASPxSpellCheckerDictionary AlphabetPath="~/DevExpress_Trad/ASPxSpellCheckerForms/en_US.aff" DictionaryPath="~/DevExpress_Trad/ASPxSpellCheckerForms/en_US.dic" Culture="English"></dxwsc:ASPxSpellCheckerDictionary>
                                                                                    </Dictionaries>
                                                                                </SettingsSpellChecker>
                                                                            </dxhe:ASPxHtmlEditor>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 100px">
                                                                                            <asp:Label runat="server" Text="In&#237;cio Real:" ID="lblInicioReal"></asp:Label>
                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td style="width: 10px"></td>
                                                                                        <td style="width: 100px">
                                                                                            <asp:Label runat="server" Text="T&#233;rmino Real:" ID="lblTerminoReal"></asp:Label>
                                                                                        </td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="115px">
                                                                                            <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlInicioReal" ID="ddlInicioReal">
                                                                                                <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje"></CalendarProperties>
                                                                                                <ClientSideEvents DateChanged="function(s, e) {
	ddlTerminoReal.SetDate(s.GetValue());
  	calendar = ddlTerminoReal.GetCalendar();
  	if ( calendar )
    	calendar.minDate = new Date(s.GetValue());
}"></ClientSideEvents>
                                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                            </dxe:ASPxDateEdit>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxTextBox runat="server" Width="55px" ClientInstanceName="txtHoraInicioAta" ID="txtHoraInicioAta">
                                                                                                <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                                <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                            </dxe:ASPxTextBox>
                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td width="115px">
                                                                                            <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlTerminoReal" ID="ddlTerminoReal">
                                                                                                <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje"></CalendarProperties>
                                                                                                <ClientSideEvents DateChanged="function(s, e) {
	ddlTerminoReal.SetDate(s.GetValue());
  	calendar = ddlTerminoReal.GetCalendar();
  	if ( calendar )
    	calendar.minDate = new Date(s.GetValue());
}"></ClientSideEvents>
                                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                            </dxe:ASPxDateEdit>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxTextBox runat="server" Width="55px" ClientInstanceName="txtHoraTerminoAta" ID="txtHoraTerminoAta">
                                                                                                <MaskSettings Mask="HH:mm"></MaskSettings>
                                                                                                <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>
                                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                            </dxe:ASPxTextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label runat="server" Text="Resumo da Reuni&#227;o:" ID="lblAta"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="memoAta"
                                                                                Width="100%" Height="100px" ID="memoAta">
                                                                                <Toolbars>
                                                                                    <dxhe:HtmlEditorToolbar>
                                                                                        <Items>
                                                                                            <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                                                                                <Items>
                                                                                                    <dxhe:ToolbarListEditItem Text="Normal" Value="p"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Address" Value="address"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div"></dxhe:ToolbarListEditItem>
                                                                                                </Items>
                                                                                            </dxhe:ToolbarParagraphFormattingEdit>
                                                                                            <dxhe:ToolbarFontNameEdit>
                                                                                                <Items>
                                                                                                    <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Arial" Value="Arial"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="Courier" Value="Courier"></dxhe:ToolbarListEditItem>
                                                                                                </Items>
                                                                                            </dxhe:ToolbarFontNameEdit>
                                                                                            <dxhe:ToolbarFontSizeEdit Width="170px">
                                                                                                <Items>
                                                                                                    <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6"></dxhe:ToolbarListEditItem>
                                                                                                    <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7"></dxhe:ToolbarListEditItem>
                                                                                                </Items>
                                                                                            </dxhe:ToolbarFontSizeEdit>
                                                                                            <dxhe:ToolbarBoldButton BeginGroup="True"></dxhe:ToolbarBoldButton>
                                                                                            <dxhe:ToolbarItalicButton></dxhe:ToolbarItalicButton>
                                                                                            <dxhe:ToolbarUnderlineButton></dxhe:ToolbarUnderlineButton>
                                                                                            <dxhe:ToolbarStrikethroughButton></dxhe:ToolbarStrikethroughButton>
                                                                                            <dxhe:ToolbarJustifyLeftButton BeginGroup="True"></dxhe:ToolbarJustifyLeftButton>
                                                                                            <dxhe:ToolbarJustifyCenterButton></dxhe:ToolbarJustifyCenterButton>
                                                                                            <dxhe:ToolbarJustifyRightButton></dxhe:ToolbarJustifyRightButton>
                                                                                            <dxhe:ToolbarJustifyFullButton></dxhe:ToolbarJustifyFullButton>
                                                                                            <dxhe:ToolbarBackColorButton BeginGroup="True"></dxhe:ToolbarBackColorButton>
                                                                                            <dxhe:ToolbarFontColorButton></dxhe:ToolbarFontColorButton>
                                                                                            <dxhe:ToolbarFullscreenButton>
                                                                                            </dxhe:ToolbarFullscreenButton>
                                                                                        </Items>
                                                                                    </dxhe:HtmlEditorToolbar>
                                                                                </Toolbars>
                                                                                <Settings AllowHtmlView="False" AllowPreview="False"></Settings>
                                                                            </dxhe:ASPxHtmlEditor>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </dxw:ContentControl>
                                                    </ContentCollection>
                                                </dxtc:TabPage>
                                                <dxtc:TabPage Name="tabProjetos" Text="Projetos">
                                                    <ContentCollection>
                                                        <dxw:ContentControl runat="server">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxwgv:ASPxGridView ID="gvProjetos" runat="server" AutoGenerateColumns="False"
                                                                                ClientInstanceName="gvProjetos"
                                                                                KeyFieldName="CodigoProjeto" Width="100%">
                                                                                <Columns>
                                                                                    <dxwgv:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True"
                                                                                        VisibleIndex="0" Width="50px">
                                                                                        <HeaderTemplate>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td align="center">
                                                                                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                                                                            ClientInstanceName="menu"
                                                                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                                                                            OnInit="menu_Init">
                                                                                                            <Paddings Padding="0px" />
                                                                                                            <Items>
                                                                                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                                                    </Image>
                                                                                                                </dxm:MenuItem>
                                                                                                                <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                                                                    <Items>
                                                                                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                                                                            ClientVisible="False">
                                                                                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                        <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                                                            <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                    </Items>
                                                                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                                                                    </Image>
                                                                                                                </dxm:MenuItem>
                                                                                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                                                                    <Items>
                                                                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                                                            <Image IconID="save_save_16x16">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                                                            <Image IconID="actions_reset_16x16">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                    </Items>
                                                                                                                    <Image Url="~/imagens/botoes/layout.png">
                                                                                                                    </Image>
                                                                                                                </dxm:MenuItem>
                                                                                                            </Items>
                                                                                                            <ItemStyle Cursor="pointer">
                                                                                                                <HoverStyle>
                                                                                                                    <border borderstyle="None" />
                                                                                                                </HoverStyle>
                                                                                                                <Paddings Padding="0px" />
                                                                                                            </ItemStyle>
                                                                                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                                                                <SelectedStyle>
                                                                                                                    <border borderstyle="None" />
                                                                                                                </SelectedStyle>
                                                                                                            </SubMenuItemStyle>
                                                                                                            <Border BorderStyle="None" />
                                                                                                        </dxm:ASPxMenu>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </HeaderTemplate>
                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                    <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto"
                                                                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewBandColumn Caption="Desempenho" ShowInCustomizationForm="True"
                                                                                        VisibleIndex="2">
                                                                                        <Columns>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Atual" FieldName="DesempenhoAtual"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="0" Width="75px"
                                                                                                Name="DesempenhoAtual">
                                                                                                <PropertiesTextEdit DisplayFormatString="&lt;img border=0 src='../imagens/{0}.gif' /&gt;">
                                                                                                </PropertiesTextEdit>
                                                                                                <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False"
                                                                                                    AllowSort="False" />
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <CellStyle HorizontalAlign="Center">
                                                                                                </CellStyle>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                            <dxwgv:GridViewDataTextColumn Caption="Anterior" FieldName="DesempenhoAnterior"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="75px"
                                                                                                Name="DesempenhoAnterior">
                                                                                                <PropertiesTextEdit DisplayFormatString="&lt;img border=0 src='../imagens/{0}.gif' /&gt;">
                                                                                                </PropertiesTextEdit>
                                                                                                <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False"
                                                                                                    AllowSort="False" />
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <CellStyle HorizontalAlign="Center">
                                                                                                </CellStyle>
                                                                                            </dxwgv:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dxwgv:GridViewBandColumn>
                                                                                </Columns>
                                                                                <SettingsPager Mode="ShowAllRecords">
                                                                                </SettingsPager>
                                                                                <Settings VerticalScrollBarMode="Visible" />
                                                                                <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar" />
                                                                            </dxwgv:ASPxGridView>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </dxw:ContentControl>
                                                    </ContentCollection>
                                                </dxtc:TabPage>
                                                <dxtc:TabPage Name="tabMapa" Text="Mapa Estrat&#233;gico" ClientVisible="False"
                                                    Visible="False">
                                                    <ContentCollection>
                                                        <dxw:ContentControl runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <iframe frameborder="0" name="frameMapa" scrolling="auto" src=""
                                                                            style="height: " width="100%" marginheight="0"></iframe>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 3px"></td>
                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td style="width: 20px">
                                                                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/selecionado.PNG" ID="ASPxImage1"></dxe:ASPxImage>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxLabel runat="server" Text="Objetivos Selecionados" Font-Bold="True" ID="ASPxLabel1"></dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dxw:ContentControl>
                                                    </ContentCollection>
                                                </dxtc:TabPage>
                                                <dxtc:TabPage Name="tabObjetivos" Text="Objetivos Estrat&#233;gicos">
                                                    <ContentCollection>
                                                        <dxw:ContentControl runat="server">
                                                            <dxwgv:ASPxGridView runat="server"
                                                                ClientInstanceName="gvObjetivos" KeyFieldName="Codigo"
                                                                AutoGenerateColumns="False" Width="100%"
                                                                ID="gvObjetivos">
                                                                <Columns>
                                                                    <dxwgv:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True"
                                                                        VisibleIndex="0" Width="50px"
                                                                        Visible="False">
                                                                        <HeaderTemplate>
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <dxm:ASPxMenu ID="menu1" runat="server" BackColor="Transparent"
                                                                                            ClientInstanceName="menu1"
                                                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                                                            OnInit="menu_Init">
                                                                                            <Paddings Padding="0px" />
                                                                                            <Items>
                                                                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                                    </Image>
                                                                                                </dxm:MenuItem>
                                                                                                <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                                                    <Items>
                                                                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                                                            ClientVisible="False">
                                                                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                    </Items>
                                                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                                                    </Image>
                                                                                                </dxm:MenuItem>
                                                                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                                                    <Items>
                                                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                                            <Image IconID="save_save_16x16">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                                            <Image IconID="actions_reset_16x16">
                                                                                                            </Image>
                                                                                                        </dxm:MenuItem>
                                                                                                    </Items>
                                                                                                    <Image Url="~/imagens/botoes/layout.png">
                                                                                                    </Image>
                                                                                                </dxm:MenuItem>
                                                                                            </Items>
                                                                                            <ItemStyle Cursor="pointer">
                                                                                                <HoverStyle>
                                                                                                    <border borderstyle="None" />
                                                                                                </HoverStyle>
                                                                                                <Paddings Padding="0px" />
                                                                                            </ItemStyle>
                                                                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                                                <SelectedStyle>
                                                                                                    <border borderstyle="None" />
                                                                                                </SelectedStyle>
                                                                                            </SubMenuItemStyle>
                                                                                            <Border BorderStyle="None" />
                                                                                        </dxm:ASPxMenu>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>

                                                                        </HeaderTemplate>
                                                                    </dxwgv:GridViewCommandColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="Desempenho" Width="50px" Caption=" " VisibleIndex="1">
                                                                        <PropertiesTextEdit DisplayFormatString="&lt;img border=0 src='../imagens/{0}.gif' /&gt;">
                                                                        </PropertiesTextEdit>
                                                                        <Settings AllowAutoFilter="False" AllowGroup="False" AllowSort="False" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn FieldName="Descricao" Caption="Objetivo" VisibleIndex="2">
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                </Columns>
                                                                <SettingsPager Mode="ShowAllRecords">
                                                                </SettingsPager>
                                                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="50" />
                                                                <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar" />
                                                            </dxwgv:ASPxGridView>
                                                        </dxw:ContentControl>
                                                    </ContentCollection>
                                                </dxtc:TabPage>
                                                <dxtc:TabPage Name="tabPendencias" Text="Pend&#234;ncias">
                                                    <ContentCollection>
                                                        <dxw:ContentControl runat="server">
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="Status:" ID="ASPxLabel2"></dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-bottom: 5px">
                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String"
                                                                                Width="270px" ID="ddlStatusPendencia"
                                                                                SelectedIndex="0">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvPendencias.PerformCallback();
}"></ClientSideEvents>
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Todos" Value="-1" Selected="True"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="Atrasada" Value="Atrasada"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="Cancelada" Value="Cancelada"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="Concluída com Atraso" Value="Concluída_com_Atraso"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="Concluída no Prazo" Value="Concluída_no_Prazo" />
                                                                                    <dxe:ListEditItem Text="Em Execução" Value="Em_Execução" />
                                                                                    <dxe:ListEditItem Text="Futura" Value="Futura" />
                                                                                    <dxe:ListEditItem Text="Início atrasado" Value="Início atrasado" />
                                                                                </Items>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvPendencias"
                                                                                KeyFieldName="CodigoTarefa" AutoGenerateColumns="False" Width="100%"
                                                                                ID="gvPendencias"
                                                                                OnCustomButtonInitialize="gvPendencias_CustomButtonInitialize"
                                                                                KeyboardSupport="True"
                                                                                OnHtmlDataCellPrepared="gvPendencias_HtmlDataCellPrepared">
                                                                                <ClientSideEvents CustomButtonClick="function(s, e) {
	gvPendencias.SetFocusedRowIndex(e.visibleIndex);
	e.processOnServer = false;
    if (e.buttonID == &quot;btnComentarios&quot;)
    {
       gvMedicao.GetRowValues(gvPendencias.GetFocusedRowIndex(), 'CodigoMedicao;Contrato;Mes_Ano;Fornecedor;ObjetoContrato;ValorContrato;DataInicio;DataTermino;DataBaseReajuste;CodigoContrato;ValorTotalMedicao;ValorMedidoAteMes;', MontaCampos );
       gvHistorico.PerformCallback(e.visibleIndex);       
    }	
}" />
                                                                                <ClientSideEvents CustomButtonClick="function(s, e) {
	gvPendencias.SetFocusedRowIndex(e.visibleIndex);
	e.processOnServer = false;
    if (e.buttonID == &quot;btnComentarios&quot;)
    {
//       pcComentarios.Show();
       pnGeral.PerformCallback(e.visibleIndex);       
    }	
}"></ClientSideEvents>
                                                                                <Columns>
                                                                                    <dxwgv:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True"
                                                                                        VisibleIndex="0" Width="50px">
                                                                                        <HeaderTemplate>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td align="center">
                                                                                                        <dxm:ASPxMenu ID="menu2" runat="server" BackColor="Transparent"
                                                                                                            ClientInstanceName="menu2"
                                                                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                                                                            OnInit="menu_Init">
                                                                                                            <Paddings Padding="0px" />
                                                                                                            <Items>
                                                                                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                                                    </Image>
                                                                                                                </dxm:MenuItem>
                                                                                                                <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                                                                    <Items>
                                                                                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                                                                            ClientVisible="False">
                                                                                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                    </Items>
                                                                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                                                                    </Image>
                                                                                                                </dxm:MenuItem>
                                                                                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                                                                    <Items>
                                                                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                                                            <Image IconID="save_save_16x16">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                                                            <Image IconID="actions_reset_16x16">
                                                                                                                            </Image>
                                                                                                                        </dxm:MenuItem>
                                                                                                                    </Items>
                                                                                                                    <Image Url="~/imagens/botoes/layout.png">
                                                                                                                    </Image>
                                                                                                                </dxm:MenuItem>
                                                                                                            </Items>
                                                                                                            <ItemStyle Cursor="pointer">
                                                                                                                <HoverStyle>
                                                                                                                    <border borderstyle="None" />
                                                                                                                </HoverStyle>
                                                                                                                <Paddings Padding="0px" />
                                                                                                            </ItemStyle>
                                                                                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                                                                <SelectedStyle>
                                                                                                                    <border borderstyle="None" />
                                                                                                                </SelectedStyle>
                                                                                                            </SubMenuItemStyle>
                                                                                                            <Border BorderStyle="None" />
                                                                                                        </dxm:ASPxMenu>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </HeaderTemplate>
                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoTarefa" Caption="Tarefa"
                                                                                        VisibleIndex="2" Width="300px">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavel" Width="300px"
                                                                                        Caption="Respons&#225;vel" VisibleIndex="3">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataDateColumn Caption="Término Previsto"
                                                                                        FieldName="TerminoPrevisto" ShowInCustomizationForm="True" VisibleIndex="5"
                                                                                        Width="200px">
                                                                                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm}"></PropertiesDateEdit>
                                                                                        <Settings ShowFilterRowMenu="True" />
                                                                                        <Settings ShowFilterRowMenu="True"></Settings>
                                                                                    </dxwgv:GridViewDataDateColumn>
                                                                                    <dxwgv:GridViewDataDateColumn Caption="Término Real" FieldName="TerminoReal"
                                                                                        ShowInCustomizationForm="True" VisibleIndex="6" Width="200px">
                                                                                        <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm}"></PropertiesDateEdit>
                                                                                        <Settings ShowFilterRowMenu="True" />
                                                                                        <Settings ShowFilterRowMenu="True"></Settings>
                                                                                    </dxwgv:GridViewDataDateColumn>
                                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True"
                                                                                        VisibleIndex="1" Width="40px" Caption=" ">
                                                                                        <CustomButtons>
                                                                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnComentarios" Text="Comentários">
                                                                                                <Image Url="~/imagens/botoes/pFormulario.png">
                                                                                                </Image>
                                                                                            </dxwgv:GridViewCommandColumnCustomButton>
                                                                                        </CustomButtons>
                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                    <dxwgv:GridViewDataTextColumn Caption="CodigoTarefa" FieldName="CodigoTarefa"
                                                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn Caption="Anotacoes" FieldName="Anotacoes"
                                                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn Caption="Reunião" FieldName="DescricaoReuniao"
                                                                                        ShowInCustomizationForm="True" VisibleIndex="4" Width="300px">
                                                                                        <CellStyle Wrap="True">
                                                                                        </CellStyle>
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxtv:GridViewDataImageColumn Caption=" " FieldName="Estagio" Name="Estagio"
                                                                                        ShowInCustomizationForm="True" VisibleIndex="7" Width="50px">
                                                                                        <PropertiesImage DisplayFormatString="&lt;img {0}  /&gt;"></PropertiesImage>
                                                                                        <Settings AllowAutoFilter="False" AllowDragDrop="False" AllowGroup="False" />
                                                                                    </dxtv:GridViewDataImageColumn>
                                                                                </Columns>
                                                                                <SettingsResizing ColumnResizeMode="Control" />
                                                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                                                <Settings VerticalScrollBarMode="Visible" ShowFooter="True" ShowGroupPanel="True" HorizontalScrollBarMode="Auto"></Settings>
                                                                                <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                                                                <Styles>
                                                                                    <Footer>
                                                                                        <Paddings Padding="0px" />
                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </Footer>
                                                                                </Styles>
                                                                                <Templates>
                                                                                    <FooterRow>
                                                                                        <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table class="grid-legendas">
                                                                                                        <tr>
                                                                                                            <td class="grid-legendas-icone">
                                                                                                                <dxe:ASPxImage ID="ASPxImage2" runat="server" Height="16px"
                                                                                                                    ImageUrl="~/imagens/VerdeOK.gif">
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label-icone">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                                                                                                    Text="<%# Resources.traducao.reuniaoTecnicaRealizacao_conclu_da_no_prazo %>">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-icone">
                                                                                                                <dxe:ASPxImage ID="ASPxImage3" runat="server" Height="16px"
                                                                                                                    ImageUrl="~/imagens/VermelhoOK.gif">
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label-icone">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                                                                                    Text="<%# Resources.traducao.reuniaoTecnicaRealizacao_conclu_da_com_atraso %>">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-icone">
                                                                                                                <dxe:ASPxImage ID="ASPxImage4" runat="server" Height="16px"
                                                                                                                    ImageUrl="~/imagens/Verde.gif">
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label-icone">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                                                                    Text="<%# Resources.traducao.reuniaoTecnicaRealizacao_em_execu__o %>">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-icone">
                                                                                                                <dxe:ASPxImage ID="ASPxImage8" runat="server" Height="16px"
                                                                                                                    ImageUrl="~/imagens/amarelo.gif">
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label-icone">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                                                                    Text="<%# Resources.traducao.reuniaoTecnicaRealizacao_in_cio_atrasado %>">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-icone">
                                                                                                                <dxe:ASPxImage ID="ASPxImage5" runat="server" Height="16px"
                                                                                                                    ImageUrl="~/imagens/Vermelho.gif">
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label-icone">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                                                                                    Text="<%# Resources.traducao.reuniaoTecnicaRealizacao_atrasada %>">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-icone">
                                                                                                                <dxe:ASPxImage ID="ASPxImage6" runat="server" Height="16px"
                                                                                                                    ImageUrl="~/imagens/Relogio.png">
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label-icone">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                                                                                                    Text="<%# Resources.traducao.reuniaoTecnicaRealizacao_futura %>">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-icone">
                                                                                                                <dxe:ASPxImage ID="ASPxImage7" runat="server" Height="16px"
                                                                                                                    ImageUrl="~/imagens/TarefaCancelada.png">
                                                                                                                </dxe:ASPxImage>
                                                                                                            </td>
                                                                                                            <td class="grid-legendas-label-icone">
                                                                                                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server"
                                                                                                                    Text="<%# Resources.traducao.reuniaoTecnicaRealizacao_cancelada %>">
                                                                                                                </dxe:ASPxLabel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                                <td align="right">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                                                        Text="*Pendências da Reunião Anterior" Font-Italic="True">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </FooterRow>
                                                                                </Templates>
                                                                            </dxwgv:ASPxGridView>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </dxw:ContentControl>
                                                    </ContentCollection>
                                                </dxtc:TabPage>
                                                <dxtc:TabPage Name="tabPlanoAcao" Text="Plano de A&#231;&#227;o">
                                                    <ContentCollection>
                                                        <dxw:ContentControl runat="server">
                                                            <div runat="server" id="Content4Div"></div>
                                                        </dxw:ContentControl>
                                                    </ContentCollection>
                                                </dxtc:TabPage>
                                                <dxtc:TabPage Name="tabAnexos" Text="Anexos">
                                                    <TabStyle Width="90px">
                                                    </TabStyle>
                                                    <ContentCollection>
                                                        <dxw:ContentControl ID="ContentControl5" runat="server" SupportsDisabledAttribute="True">
                                                            <iframe id="frmAnexosReuniao" frameborder="0" scrolling="yes" allowtransparency="False"
                                                                height="350px" width="100%"></iframe>
                                                        </dxw:ContentControl>
                                                    </ContentCollection>
                                                </dxtc:TabPage>
                                            </TabPages>
                                            <ClientSideEvents ActiveTabChanging="function(s, e) {
	e.cancel = podeMudarAba(s, e)
}"
                                                ActiveTabChanged="function(s, e) {
}"></ClientSideEvents>
                                        </dxtc:ASPxPageControl>
                                        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                                        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"
                                            GridViewID="gvDados" OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                                            <Styles>
                                                <Default>
                                                </Default>
                                                <Header>
                                                </Header>
                                                <Cell>
                                                </Cell>
                                                <GroupFooter Font-Bold="True">
                                                </GroupFooter>
                                                <Title Font-Bold="True"></Title>
                                            </Styles>
                                        </dxwgv:ASPxGridViewExporter>
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <ClientSideEvents EndCallback="function(s, e) {		
        if(s.cp_MSG != '')
        {
                 window.top.mostraMensagem(s.cp_MSG,'sucesso', false, false, null);
        }
        else if(s.cp_Erro != '')
       {
                  window.top.mostraMensagem(s.cp_Erro,'erro', true, false, null);
        }
}"></ClientSideEvents>
                            </dxcp:ASPxCallbackPanel>
                        </td>

                    </tr>
                    <tr>

                        <td align="right">
                            <table class="formulario-botoes">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="btnEnviarAta" runat="server" AutoPostBack="False" ClientInstanceName="btnEnviarAta"
                                                Text="Enviar Ata" ValidationGroup="MKE"
                                                Width="110px">
                                                <Paddings Padding="0px"></Paddings>

                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var tipoEnvio = &quot;&quot;;
	var podeEditar = btnEnviarAta.cp_EditaMensagem;

	if(podeEditar == 'N')
	{
		if(verificarDadosAta())
		{
			if (confirm('A reuni&#227;o ser&#225; Salva, Deseja continuar?'))
			{
				tipoEnvio = &quot;EnviarAta&quot;;
				pnCallback.PerformCallback(tipoEnvio);
			}		
		}
	}
	else
	{
		pcMensagemAta.Show();
	}
}"></ClientSideEvents>
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                                Text="Salvar" ValidationGroup="MKE"
                                                Width="110px">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	
	if(verificarDadosPreenchidos())
		if (window.SalvarCamposFormulario)
	    	SalvarCamposFormulario(&quot;Editar&quot;);
	else
		return false;
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btnFechar2" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar2"
                                                Text="Fechar" Width="110px">
                                                <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    window.close();
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
                <dxhf:ASPxHiddenField ID="hfDadosSessao" runat="server" ClientInstanceName="hfDadosSessao">
                </dxhf:ASPxHiddenField>
                <dxpc:ASPxPopupControl ID="pcMensagemAta" runat="server" AllowDragging="True" ClientInstanceName="pcMensagemAta"
                    CloseAction="None" EnableViewState="False" HeaderText="Envio de ata" PopupAction="None"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                    Width="700px">
                    <ContentStyle>
                        <Paddings Padding="3px" PaddingLeft="3px" PaddingTop="3px" PaddingRight="3px" PaddingBottom="3px"></Paddings>
                    </ContentStyle>
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" Text="Texto de apresenta&#231;&#227;o:" ID="Label1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="heEncabecadoAta" Width="98%" Height="120px" ID="heEncabecadoAta">
                                                                <Toolbars>
                                                                    <dxhe:HtmlEditorToolbar>
                                                                        <Items>
                                                                            <dxhe:ToolbarFullscreenButton>
                                                                            </dxhe:ToolbarFullscreenButton>
                                                                            <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                                                                <Items>
                                                                                    <dxhe:ToolbarListEditItem Text="Normal" Value="p"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Address" Value="address"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div"></dxhe:ToolbarListEditItem>
                                                                                </Items>
                                                                            </dxhe:ToolbarParagraphFormattingEdit>
                                                                            <dxhe:ToolbarFontNameEdit>
                                                                                <Items>
                                                                                    <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Arial" Value="Arial"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="Courier" Value="Courier"></dxhe:ToolbarListEditItem>
                                                                                </Items>
                                                                            </dxhe:ToolbarFontNameEdit>
                                                                            <dxhe:ToolbarFontSizeEdit>
                                                                                <Items>
                                                                                    <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6"></dxhe:ToolbarListEditItem>
                                                                                    <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7"></dxhe:ToolbarListEditItem>
                                                                                </Items>
                                                                            </dxhe:ToolbarFontSizeEdit>
                                                                            <dxhe:ToolbarBoldButton BeginGroup="True"></dxhe:ToolbarBoldButton>
                                                                            <dxhe:ToolbarItalicButton></dxhe:ToolbarItalicButton>
                                                                            <dxhe:ToolbarUnderlineButton></dxhe:ToolbarUnderlineButton>
                                                                            <dxhe:ToolbarStrikethroughButton></dxhe:ToolbarStrikethroughButton>
                                                                            <dxhe:ToolbarJustifyLeftButton BeginGroup="True"></dxhe:ToolbarJustifyLeftButton>
                                                                            <dxhe:ToolbarJustifyCenterButton></dxhe:ToolbarJustifyCenterButton>
                                                                            <dxhe:ToolbarJustifyRightButton></dxhe:ToolbarJustifyRightButton>
                                                                            <dxhe:ToolbarJustifyFullButton></dxhe:ToolbarJustifyFullButton>
                                                                            <dxhe:ToolbarBackColorButton BeginGroup="True"></dxhe:ToolbarBackColorButton>
                                                                            <dxhe:ToolbarFontColorButton></dxhe:ToolbarFontColorButton>
                                                                        </Items>
                                                                    </dxhe:HtmlEditorToolbar>
                                                                </Toolbars>
                                                                <Settings AllowHtmlView="False" AllowPreview="False"></Settings>
                                                            </dxhe:ASPxHtmlEditor>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 10px" align="right">
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                                ClientInstanceName="btnEnviarEncabecadoAta" Text="Enviar" ValidationGroup="MKE"
                                                                                Width="90px" ID="btnEnviarEncabecadoAta"
                                                                                EnableViewState="False">
                                                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var tipoEnvio = &quot;&quot;;

	if(verificarDadosAta())
	{
		tipoEnvio = 'EnviarAta';
		pnCallback.PerformCallback(tipoEnvio);
	}
}"></ClientSideEvents>
                                                                                <Paddings Padding="0px"></Paddings>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td style="width: 10px"></td>
                                                                        <td style="width: 90px">
                                                                            <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                                ClientInstanceName="btnCancelarEncabecadoAta" Text="Fechar" Width="90px"
                                                                                ID="btnCancelarEncabecadoAta"
                                                                                EnableViewState="False">
                                                                                <ClientSideEvents Click="function(s, e) {	
	pcMensagemAta.Hide();
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
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                </dxpc:ASPxPopupControl>
                <dxpc:ASPxPopupControl ID="pcComentarios" runat="server" ClientInstanceName="pcComentarios"
                    HeaderText="Comentários" Width="695px"
                    AllowDragging="True" CloseAction="None" PopupAction="None"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowCloseButton="False">
                    <HeaderStyle Font-Bold="True" />
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
                            <dxcp:ASPxCallbackPanel ID="pnGeral" runat="server" Width="100%"
                                ClientInstanceName="pnGeral" OnCallback="pnGeral_Callback">
                                <ClientSideEvents EndCallback="function(s, e) {
	pcComentarios.Show();
}" />
                                <Paddings Padding="0px" />
                                <PanelCollection>
                                    <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <dxe:ASPxMemo ID="mmComentario" runat="server" Width="100%"
                                                        ClientInstanceName="mmComentario" Rows="10">
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="padding-top: 10px">
                                                    <dxe:ASPxButton ID="btFechar" runat="server" AutoPostBack="False"
                                                        ClientInstanceName="btFechar"
                                                        Text="Fechar" Width="90px">
                                                        <ClientSideEvents Click="function(s, e) {pcComentarios.Hide();}" />
                                                        <Paddings Padding="0px" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxcp:ASPxCallbackPanel>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
            </div>
            <dxtv:ASPxPopupControl ID="pcApresentacaoAcao" runat="server" ClientInstanceName="pcApresentacaoAcao" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" MaxWidth="650px" MinWidth="260px" Modal="True" CloseAction="None">
                <ClientSideEvents Closing="function(s, e) {
	lblMensagemApresentacaoAcao.SetText('');
}" />
                <ContentCollection>
                    <dxtv:PopupControlContentControl runat="server">
                        <table cellspacing="0" class="auto-style1">
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="width: 70px" valign="middle">
                                                    <dxtv:ASPxImage ID="imgApresentacaoAcao" runat="server" ClientInstanceName="imgApresentacaoAcao" ImageAlign="TextTop" ImageUrl="~/imagens/Workflow/salvarBanco.png" Height="40px">
                                                    </dxtv:ASPxImage>
                                                </td>
                                                <td align="left" style="padding: 10px" valign="middle">
                                                    <dxtv:ASPxLabel ID="lblMensagemApresentacaoAcao" runat="server" ClientInstanceName="lblMensagemApresentacaoAcao" EncodeHtml="False" Wrap="False">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding-top: 2px">
                                    <table cellspacing="0">
                                        <tr>
                                            <td style="padding-right: 3px">
                                                <dxtv:ASPxButton ID="btnOkApresentacaoAcao" runat="server" AutoPostBack="False" ClientInstanceName="btnOkApresentacaoAcao" Text="OK" Width="70px">
                                                    <ClientSideEvents Click="function(s, e) {
	if(eventoOKMsg != null &amp;&amp; eventoOKMsg != '')
		eventoOKMsg();
	fechaMensagem();
}" />
                                                    <Paddings Padding="0px" />
                                                </dxtv:ASPxButton>
                                            </td>
                                            <td style="padding-left: 3px">
                                                <dxtv:ASPxButton ID="btnCancelarApresentacaoAcao" runat="server" AutoPostBack="False" Text="Cancelar" Width="70px" ClientInstanceName="btnCancelarApresentacaoAcao">
                                                    <ClientSideEvents Click="function(s, e) {
	fechaMensagem();
}" />
                                                    <Paddings Padding="0px" />
                                                </dxtv:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxtv:PopupControlContentControl>
                </ContentCollection>
            </dxtv:ASPxPopupControl>
        </form>
    </section>
</body>
</html>
