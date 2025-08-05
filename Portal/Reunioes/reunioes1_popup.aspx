<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reunioes1_popup.aspx.cs"
    Inherits="Reunioes_reunioes1_popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        // JScript File
        var frmAnexos = '';
        var atualizarURLAnexos = '';
        var TipoOperacao = '';

        function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
            // o método será chamado por meio do objeto pnCallBack
            alert('passou em SalvarCamposFormulario()');
            hfGeral.Set("StatusSalvar", "0");
            callbackTelaReuniao.PerformCallback(TipoOperacao);
            return false;
        }

        // **************************************************************************************
        // - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
        // **************************************************************************************


        // ---------------------------------------------------------------------------------
        // Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
        // ---------------------------------------------------------------------------------
        function posSalvarComSucesso() {
            // se já incluiu alguma opção, feche a tela após salvar
            if (gvDados.GetVisibleRowsOnPage() > 0)
                onClick_btnCancelar();
        }




        function desabilitaHabilitaComponentes() {
            //var tipoOperacaoSel = TipoOperacao; hfGeral.Get("TipoOperacao");
            var OperacaoSel = hfGeral.Get("TipoOperacao").toString();
            var BoolEnabled = (OperacaoSel == "Editar" || OperacaoSel == "Incluir") ? true : false;

            if ("Incluir" == OperacaoSel || "Consultar" == OperacaoSel)
                btnEnviarPauta.SetVisible(false);
            else
                btnEnviarPauta.SetVisible(true);

            /*if("Incluir" == OperacaoSel)
                btnNovaReuniao.SetVisible(false);
            else
                btnNovaReuniao.SetVisible(true);*/

            txtAssunto.SetEnabled(BoolEnabled);
            txtHoraInicio.SetEnabled(BoolEnabled);
            txtHoraTermino.SetEnabled(BoolEnabled);
            txtHoraInicioAta.SetEnabled(BoolEnabled);
            txtHoraTerminoAta.SetEnabled(BoolEnabled);

            memoLocal.clientEnabled = BoolEnabled;
            memoPauta.clientEnabled = BoolEnabled;
            memoAta.clientEnabled = BoolEnabled;

            memoLocal.SetEnabled(BoolEnabled);
            memoPauta.SetEnabled(BoolEnabled);
            memoAta.SetEnabled(BoolEnabled);


            ddlResponsavelEvento.SetEnabled(BoolEnabled);
            ddlTipoEvento.SetEnabled(BoolEnabled);
            ddlInicioPrevisto.SetEnabled(BoolEnabled);
            ddlTerminoPrevisto.SetEnabled(BoolEnabled);
            ddlInicioReal.SetEnabled(BoolEnabled);
            ddlTerminoReal.SetEnabled(BoolEnabled);

            lbDisponiveis.SetEnabled(BoolEnabled);
            lbSelecionados.SetEnabled(BoolEnabled);
            lbGrupos.SetEnabled(BoolEnabled);


            if ("Consultar" == OperacaoSel) {
                btnADD.SetEnabled(false);
                btnADDTodos.SetEnabled(false);
                btnRMV.SetEnabled(false);
                btnRMVTodos.SetEnabled(false);
            }
        }

        function podeMudarAba(s, e) {

            if (e.tab.index <= 1)
                return false;

            if (hfGeral.Get("codigoEvento") == "-1" || hfGeral.Get("codigoEvento") == "") {
                window.top.mostraMensagem(traducao.reunioes_para_ter_acesso_a_op__o + " \"" + e.tab.GetText() + "\" " + traducao.reunioes___obrigat_rio_salvar_as_informa__es_da_op__o + " \"" + tabControl.GetTab(0).GetText() + "\"", traducao.reunioes_atencao, true, false, null);
                return true;
            }

            if (!s.cpTipoOperacao)
                TipoOperacao = "Consultar";

            else if (e.tab.name == "TabD")
                getValoresPlanoAcao();

            return false;
        }

        function getValoresPlanoAcao() {
            var codigoEvento = hfGeral.Get("codigoEvento");
            if (window.hfGeralToDoList) {
                hfGeral.Set("TipoOperacao", TipoOperacao);
                hfGeral.Set("codigoObjetoAssociado", codigoEvento);
                hfGeralToDoList.Set("codigoObjetoAssociado", codigoEvento);
                gvToDoList.PerformCallback("Popular");
            }
            else
                window.top.mostraMensagem(traducao.reunioes_n_o_foi_poss_vel_encontrar_o_componente_todolist, 'Atencao', true, false, null);
        }

        //------------------------------------------------------------funções relacionadas com a ListBox
        var delimitador = ";";

        function UpdateButtons() {
            try {
                btnADD.SetEnabled(callbackTelaReuniao.cpTipoOperacao != "" && callbackTelaReuniao.cpTipoOperacao != "Consultar" && lbDisponiveis.GetSelectedItem() != null);
                btnADDTodos.SetEnabled(callbackTelaReuniao.cpTipoOperacao != "" && callbackTelaReuniao.cpTipoOperacao != "Consultar" && lbDisponiveis.GetItemCount() > 0);
                btnRMV.SetEnabled(callbackTelaReuniao.cpTipoOperacao != "" && callbackTelaReuniao.cpTipoOperacao != "Consultar" && lbSelecionados.GetSelectedItem() != null);
                btnRMVTodos.SetEnabled(callbackTelaReuniao.cpTipoOperacao != "" && callbackTelaReuniao.cpTipoOperacao != "Consultar" && lbSelecionados.GetItemCount() > 0);
                capturaCodigosInteressados();
            } catch (e) { }
        }

        function UpdateButtonsPopup() {
            var somenteLeitura = hfGeral.Get("SomenteLeitura").toString();

            btnADD.SetEnabled(somenteLeitura != "S" && lbDisponiveis.GetSelectedItem() != null);
            btnADDTodos.SetEnabled(somenteLeitura != "S" && lbDisponiveis.GetItemCount() > 0);
            btnRMV.SetEnabled(somenteLeitura != "S" && lbSelecionados.GetSelectedItem() != null);
            btnRMVTodos.SetEnabled(somenteLeitura != "S" && lbSelecionados.GetItemCount() > 0);
            capturaCodigosInteressados();
        }



        function capturaCodigosInteressados() {
            var CodigosProjetosSelecionados = "";
            for (var i = 0; i < lbSelecionados.GetItemCount(); i++) {
                CodigosProjetosSelecionados += lbSelecionados.GetItem(i).value + delimitador;
            }
            hfGeral.Set("CodigosSelecionados", CodigosProjetosSelecionados);
        }
        //--------------------***********

        function verificarDadosAta() {
            var mensagemError = "";
            var retorno = true;
            var numError = 0;

            //------------Obtendo data e hora actual
            var momentoActual = new Date();
            var horaActual = momentoActual.getHours() + ":" + momentoActual.getMinutes();
            var arrayHoraAgora = horaActual.split(':');
            var meuDataAtual = (momentoActual.getMonth() + 1) + "/" + momentoActual.getDate() + "/" + momentoActual.getFullYear();
            var dataHoje = Date.parse(meuDataAtual);

            var dataInicioReal = new Date(ddlInicioReal.GetValue());
            var dataInicioRealP = (dataInicioReal.getMonth() + 1) + "/" + dataInicioReal.getDate() + "/" + dataInicioReal.getFullYear();
            var dataInicioRealC = Date.parse(dataInicioRealP);

            var dataTerminoReal = new Date(ddlTerminoReal.GetValue());
            var dataTerminoRealP = (dataTerminoReal.getMonth() + 1) + "/" + dataTerminoReal.getDate() + "/" + dataTerminoReal.getFullYear();
            var dataTerminoRealC = Date.parse(dataTerminoRealP);

            var arrayMomentoInicio = txtHoraInicioAta.GetText().split(':');
            var arrayMomentoFinal = txtHoraTerminoAta.GetText().split(':');
            //------------- ***
            //---- Datas para comparação ****
            var dataInicioComparaP = (momentoActual.getMonth() + 1) + "/" + momentoActual.getDate() + "/" + (momentoActual.getFullYear() - 5);
            var dataInicioComparaMostra = momentoActual.getDate() + "/" + (momentoActual.getMonth()) + "/" + (momentoActual.getFullYear() - 5);
            var dataInicioComparaC = Date.parse(dataInicioComparaP);

            var dataTerminoCompara = new Date(ddlTerminoReal.GetValue());
            var dataTerminoComparaP = (dataTerminoCompara.getMonth() + 1) + "/" + dataTerminoCompara.getDate() + "/" + dataTerminoCompara.getFullYear();
            var dataTerminoComparaMostra = (dataTerminoCompara.getMonth() + 1) + "/" + dataTerminoCompara.getDate() + "/" + dataTerminoCompara.getFullYear();
            var dataTerminoComparaC = Date.parse(dataTerminoComparaP);

            //------***********

            if (ddlInicioReal.GetValue() == null) {
                mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_in_cio_real_da_reuni_o_deve_ser_informada_ + "\n";
                retorno = false;
            }

            if (ddlTerminoReal.GetValue() == null) {
                mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_t_rmino_real_da_reuni_o_deve_ser_informada_ + "\n";
                retorno = false;
            }

            if (dataHoje < dataInicioRealC) {
                mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_in_cio_real_da_reuni_o_n_o_pode_ser_maior_que_a_data_atual_ + "\n";
                retorno = false;
            }

            if (dataHoje < dataTerminoRealC) {
                mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_t_rmino_real_da_reuni_o_n_o_pode_ser_maior_que_a_data_atual_ + "\n";
                retorno = false;
            }

            if (dataInicioRealC > dataTerminoRealC) {
                mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_in_cio_real_da_reuni_o_n_o_pode_ser_maior_que_a_data_de_t_rmino_real_ + "\n";
                retorno = false;
            }




            if (dataInicioRealP == dataTerminoRealP) {
                if (arrayMomentoFinal[0] < arrayMomentoInicio[0]) {
                    mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_real_indicada_n_o_pode_ser_superior___hora_de_t_rmino_real__horas__ + "\n";
                    retorno = false;
                }
                if (arrayMomentoFinal[0] == arrayMomentoInicio[0]) {
                    if (arrayMomentoFinal[1] < arrayMomentoInicio[1]) {
                        mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_real_indicada_n_o_pode_ser_superior___hora_atual__minutos__ + "\n";
                        retorno = false;
                    }
                }


            }

            if (dataTerminoRealP == dataHoje) {
                if (arrayHoraAgora[0] < arrayMomentoInicio[0]) {
                    mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_real_indicada_n_o_pode_ser_superior___hora_atual__horas__ + "\n";
                    retorno = false;

                }
                if (arrayHoraAgora[0] == arrayMomentoInicio[0]) {
                    if (arrayHoraAgora[1] < arrayMomentoInicio[1]) {
                        mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_real_indicada_n_o_pode_ser_superior___hora_atual__minutos__ + "\n";
                        retorno = false;
                    }
                }

                if (arrayHoraAgora[0] < arrayMomentoFinal[0]) {
                    mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_t_rmino_real_indicada_n_o_pode_ser_superior___hora_atual__horas__ + "\n";
                    retorno = false;

                }
                if (arrayHoraAgora[0] == arrayMomentoFinal[0]) {
                    if (arrayHoraAgora[1] < arrayMomentoFinal[1]) {
                        mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_t_rmino_real_indicada_n_o_pode_ser_superior___hora_atual__minutos__ + "\n";
                        retorno = false;
                    }
                }
            }

            if (window.memoAta) {
                if (memoAta.GetHtml() == "") {
                    mensagemError += ++numError + ") " + traducao.reunioes_a_ata_da_reuni_o_deve_ser_informada_ + "\n";
                    retorno = false;
                }
            }

            if (!retorno)
                window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);

            return retorno;
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

            var data


            var dataInicioComparaP = (momentoActual.getMonth() + 1) + "/" + momentoActual.getDate() + "/" + (momentoActual.getFullYear() - 5);
            var dataInicioComparaMostra = momentoActual.getDate() + "/" + (momentoActual.getMonth()) + "/" + (momentoActual.getFullYear() - 5);
            var dataInicioComparaC = Date.parse(dataInicioComparaP);

            var dataTerminoCompara = new Date(ddlInicioPrevisto.GetValue());
            var dataTerminoComparaP = (dataTerminoCompara.getMonth() + 1) + "/" + dataTerminoCompara.getDate() + "/" + dataTerminoCompara.getFullYear();
            var dataTerminoComparaMostra = (dataTerminoCompara.getMonth() + 1) + "/" + dataTerminoCompara.getDate() + "/" + dataTerminoCompara.getFullYear();
            var dataTerminoComparaC = Date.parse(dataTerminoComparaP);

            //------------- ***

            if (txtAssunto.GetText() == "") {
                mensagemError += ++numError + ") " + traducao.reunioes_o_assunto_deve_ser_informado_ + "\n";
                retorno = false;
            }

            if (ddlResponsavelEvento.GetText() == "" || ddlResponsavelEvento.GetText() === ddlResponsavelEvento.GetValue()) {
                mensagemError += ++numError + ") " + traducao.reunioes_o_respons_vel_pela_reuni_o_deve_ser_informado_ + "\n";
                ddlResponsavelEvento.SetValue(null);
                retorno = false;
            }

            if (ddlTipoEvento.GetText() == "") {
                mensagemError += ++numError + ") " + traducao.reunioes_o_tipo_de_reuni_o_deve_ser_informado_ + "\n";
                retorno = false;
            }

            if (ddlInicioPrevisto.GetText() == "" || ddlInicioPrevisto.GetDate() == null) {
                mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_in_cio_da_reuni_o_deve_ser_informada_ + "\n";
                retorno = false;
            } else {

                if (ddlInicioPrevisto.GetDate() < dataInicioComparaC) {
                    mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_in_cio_da_reuni_o_n_o_pode_ser_inferior_a + " [" + dataInicioComparaMostra + "]!\n";
                    retorno = false;
                }
            }


            if (ddlTerminoPrevisto.GetText() == "" || ddlTerminoPrevisto.GetDate() == null) {
                mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_t_rmino_da_reuni_o_deve_ser_informada_ + "\n";
                retorno = false;
            } else {

                if (ddlTerminoPrevisto.GetDate() < dataInicioComparaC && ddlInicioPrevisto.GetDate() >= dataInicioComparaC) {
                    mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_t_rmino_da_reuni_o_n_o_pode_ser_inferior_a + " [" + dataTerminoComparaMostra + "]!\n";
                    retorno = false;
                }
            }

            if (memoLocal.GetText() == "") {
                mensagemError += ++numError + ") " + traducao.reunioes_a_descri__o_do_local_da_reuni_o_deve_ser_informada_ + "\n";
                retorno = false;
            }

            if (window.memoPauta) {
                if (memoPauta.GetHtml() == "") {
                    mensagemError += ++numError + ") " + traducao.reunioes_a_pauta_da_reuni_o_deve_ser_informada_ + "\n";
                    retorno = false;
                }
            }

            //----------- PAUTA
            //---coletando dados da reunion

            var dataInicioPrevisto = new Date(ddlInicioPrevisto.GetValue());
            var dataInicioPrevistoP = (dataInicioPrevisto.getMonth() + 1) + "/" + dataInicioPrevisto.getDate() + "/" + dataInicioPrevisto.getFullYear();
            var dataInicioPrevistoC = Date.parse(dataInicioPrevistoP);

            var dataTerminoPrevisto = new Date(ddlTerminoPrevisto.GetValue());
            var dataTerminoPrevistoP = (dataTerminoPrevisto.getMonth() + 1) + "/" + dataTerminoPrevisto.getDate() + "/" + dataTerminoPrevisto.getFullYear();
            var dataTerminoPrevistoC = Date.parse(dataTerminoPrevistoP);

            var arrayHoraPrevistoInicio = txtHoraInicio.GetText().split(':');
            var arrayHoraPrevistoFinal = txtHoraTermino.GetText().split(':');




            if (dataInicioPrevistoP == dataTerminoPrevistoP) {
                if (arrayHoraPrevistoInicio[0] > arrayHoraPrevistoFinal[0]) {
                    mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_indicada_na_reuni_o_n_o_pode_ser_superior___hora_de_t_rmino__horas__ + "\n";
                    retorno = false;
                }

                if (arrayHoraPrevistoInicio[0] == arrayHoraPrevistoFinal[0]) {
                    if (arrayHoraPrevistoInicio[1] > arrayHoraPrevistoFinal[1]) {
                        mensagemError += ++numError + ") " + traducao.reunioes_a_hora_de_in_cio_indicada_na_reuni_o_n_o_pode_ser_superior___hora_de_t_rmino__minutos__ + "\n";
                        retorno = false;
                    }
                }
            }
            else if (dataInicioPrevistoC > dataTerminoPrevistoC) {
                mensagemError += ++numError + ") " + traducao.reunioes_a_data_de_inicio_indicada_na_reuni_o_n_o_pode_ser_maior___data_de_t_rmino_ + "\n";
                retorno = false;
            }

            if (!retorno) {
                window.top.mostraMensagem(mensagemError, 'Atencao', true, false, null);
                return false;
            }

            //----------- ATA
            //---coletando dados da ATA
            if (ddlInicioReal.GetValue() != null || ddlTerminoReal.GetValue() != null || memoAta.GetHtml() != "") {
                retorno = verificarDadosAta();
            }
            //----------- ***

            return retorno;
        }

        function verificaEnvioPauta() {
            //var tipoOperacaoSel = TipoOperacao; hfGeral.Get("TipoOperacao");
            var OperacaoSel = hfGeral.Get("TipoOperacao").toString();

            if (OperacaoSel == "Editar") {
                if (tabControl.GetActiveTab().index == 0 || tabControl.GetActiveTab().index == 1) {
                    var dataInicioPrevisto = new Date(ddlInicioPrevisto.GetValue());
                    var dataHoje = new Date();

                    if (dataHoje > dataInicioPrevisto) {
                        btnEnviarPauta.SetEnabled(false);
                        var buttonElement = btnEnviarPauta.GetMainElement();
                        buttonElement.title = 'O envio da pauta só é permitido para reuniões com data futura.';
                    }
                    else {
                        btnEnviarPauta.SetEnabled(true);
                        var buttonElement1 = btnEnviarPauta.GetMainElement();
                        buttonElement1.title = '';
                    }
                }
                else {
                    btnEnviarPauta.SetEnabled(true);
                    var buttonElement1 = btnEnviarPauta.GetMainElement();
                    buttonElement1.title = '';
                }
            }
        }

        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);
        }

        function mostraDivAtualizando(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

            //setTimeout('fechaTelaEdicao();', 10);
        }

        function abrejanelaImpressaoReuniao(codigoProjeto, codigoEvento, moduloSistema) {
            var url = '../Reunioes/imprimeAtaDeReuniao.aspx?';

            url += 'CP=' + codigoProjeto;
            url += '&CE=' + codigoEvento;
            //url += '&MOD=' + moduloSistema;
            var w = screen.width;
            var h = screen.height;
            var left = (w - (w - 300)) / 2;
            var top = (h - (h - 300)) / 2;
            window.open(url, 'frmRelReuniao', 'menubar=no,titlebar=no,status=yes,scrollbars=yes,resizable=yes,width=' + (w - 300) + ',height=' + (h - 300) + ',left=' + left + ',top=' + top + '', false);
            //window.open(url, 'Ocupantes', 'menubar=no;titlebar=no;scrollbars=yes', true);
        }

        function buscaNoArray(arr, obj) {
            var retorno = false;
            var i = 0;
            for (i = 0; i < arr.length; i++) {
                if (arr[i] == obj) {
                    retorno = true;
                }
            }
            return retorno;
        }



        function confirmaClickEnviarPauta() {
            capturaCodigosInteressados();
            if (btnEnviarPauta.cp_EditaMensagem == 'S') {
                if (tabControl.GetActiveTab().index == 0 || tabControl.GetActiveTab().index == 1) {
                    if (verificarDadosPreenchidos()) {
                        pcMensagemPauta.SetHeaderText(traducao.reunioes_envio_de_pauta)
                        heEncabecadoAta.SetHtml(btnEnviarPauta.cp_MensagemPauta);
                        pcMensagemPauta.Show();
                    }
                }
                else {
                    if (verificarDadosAta()) {
                        pcMensagemPauta.SetHeaderText(traducao.reunioes_envio_de_ata)
                        heEncabecadoAta.SetHtml(btnEnviarPauta.cp_MensagemAta);
                        pcMensagemPauta.Show();
                    }
                }
            }
            else {
                if (tabControl.GetActiveTab().index == 0 || tabControl.GetActiveTab().index == 1) {
                    if (verificarDadosPreenchidos()) {
                        tipoEnvio = "EnviarPauta";
                        callbackTelaReuniao.PerformCallback(tipoEnvio);
                    }
                }
                else {
                    if (verificarDadosAta()) {
                        tipoEnvio = "EnviarAta";
                        callbackTelaReuniao.PerformCallback(tipoEnvio);
                    }
                }
            }
        }

        function AdjustSize() {
            var height = (Math.max(0, document.documentElement.clientHeight) - 110) + 'px';
            document.getElementById('divTabReuniao').style.height = height;
            document.getElementById('divTabParticipantes').style.height = height;
            document.getElementById('divTabAta').style.height = height;
            document.getElementById('divTabPendenciasReuniaoAnterior').style.height = height;
            document.getElementById('divTabPlanoAcao').style.height = height;
            document.getElementById('frmAnexosReuniao').height = height;
            memoPauta.SetHeight(height.replace('px', ''));
            memoAta.SetHeight(height.replace('px', ''));

            var alturaListBoxes = (Math.max(0, document.documentElement.clientHeight) - 130);// + 'px';
            var alturaListBoxesEsquerda = (alturaListBoxes / 2) - 10;
            lbSelecionados.SetHeight(alturaListBoxes);
            lbDisponiveis.SetHeight(alturaListBoxesEsquerda);
            lbGrupos.SetHeight(alturaListBoxesEsquerda);

            var alturaGridPendencias = (Math.max(0, document.documentElement.clientHeight) - 180);// + 'px';
            gvPendencias.SetHeight(alturaGridPendencias);
        }

        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }
    </script>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../scripts/barraNavegacao.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/reunioes_ASPxListbox.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/_Strings.js"></script>

    <title>Reuniones</title>
    <style type="text/css">
        .style2 {
            width: 210px;
        }

        .style3 {
            width: 274px;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <dxtv:ASPxPopupControl ID="pcComentarios" runat="server" AllowDragging="True" ClientInstanceName="pcComentarios"
            CloseAction="None" HeaderText="Comentários"
            PopupAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="False" Width="695px" PopupVerticalOffset="-15">
            <HeaderStyle Font-Bold="True" />
            <ContentCollection>
                <dxtv:PopupControlContentControl runat="server">
                    <dxtv:ASPxCallbackPanel ID="pnGeral" runat="server" ClientInstanceName="pnGeral"
                        OnCallback="pnGeral_Callback" Width="100%">
                        <ClientSideEvents EndCallback="function(s, e) {
	pcComentarios.Show();
}" />
                        <Paddings Padding="0px" />
                        <PanelCollection>
                            <dxtv:PanelContent runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <dxtv:ASPxMemo ID="mmComentario" runat="server" ClientInstanceName="mmComentario"
                                                Rows="10" Width="100%">
                                            </dxtv:ASPxMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-top: 10px">
                                            <dxtv:ASPxButton ID="btFechar" runat="server" AutoPostBack="False" ClientInstanceName="btFechar"
                                                Text="Fechar" Width="90px">
                                                <ClientSideEvents Click="function(s, e) {pcComentarios.Hide();}" />
                                                <Paddings Padding="0px" />
                                            </dxtv:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </dxtv:PanelContent>
                        </PanelCollection>
                    </dxtv:ASPxCallbackPanel>
                </dxtv:PopupControlContentControl>
            </ContentCollection>
        </dxtv:ASPxPopupControl>
        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
        </dxhf:ASPxHiddenField>
        
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td>
                        <dxcp:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tabControl" Width="99%" ID="tabControl">
                            <TabPages>
                                <dxcp:TabPage Name="TabA" Text="<%$ Resources:traducao, reunioes_reuni_o %>">
                                    <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>

                                    <TabStyle Wrap="True"></TabStyle>
                                    <ContentCollection>
                                        <dxcp:ContentControl runat="server">
                                            <div id="divTabReuniao" style="overflow-y: auto">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0" style="width: 100%" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 33%">
                                                                                <dxcp:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_assunto_ %>" Width="100%" Font-Size="11px" ID="lblAssunto"></dxcp:ASPxLabel>

                                                                            </td>
                                                                            <td style="width: 33%">
                                                                                <dxcp:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_respons_vel_ %>" Width="100%" Font-Size="11px" ID="lblResponsavel"></dxcp:ASPxLabel>

                                                                            </td>
                                                                            <td style="width: 33%">
                                                                                <dxcp:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_tipo_de_reuni_o_ %>" Width="100%" Font-Size="11px" ID="lblTipoEventos"></dxcp:ASPxLabel>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 5px">
                                                                                <dxcp:ASPxTextBox runat="server" Width="100%" MaxLength="100" ClientInstanceName="txtAssunto" ID="txtAssunto">
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE">
                                                                                        <RequiredField ErrorText="<%$ Resources:traducao, reunioes_campo_obrigat_rio %>"></RequiredField>
                                                                                    </ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                            <td style="padding-right: 5px">
                                                                                <dxcp:ASPxComboBox runat="server" DropDownStyle="DropDown" ValueType="System.Int32" NullValueItemDisplayText="{0}" CallbackPageSize="80" EnableCallbackMode="True" DropDownRows="10" TextField="<%$ Resources:traducao, reunioes_nomeusuario %>" ValueField="CodigoUsuario" TextFormatString="{0}" Width="100%" ClientInstanceName="ddlResponsavelEvento" ID="ddlResponsavelEvento" OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition">
                                                                                    <Columns>
                                                                                        <dxcp:ListBoxColumn FieldName="NomeUsuario" Width="200px" Caption="Nome"></dxcp:ListBoxColumn>
                                                                                        <dxcp:ListBoxColumn FieldName="EMail" Width="320px" Caption="Email"></dxcp:ListBoxColumn>
                                                                                    </Columns>

                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" SetFocusOnError="True" ValidationGroup="MKE">
                                                                                        <RequiredField ErrorText="<%$ Resources:traducao, reunioes_campo_obrigat_rio %>"></RequiredField>
                                                                                    </ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxComboBox>

                                                                            </td>
                                                                            <td>
                                                                                <dxcp:ASPxComboBox runat="server" Width="100%" ClientInstanceName="ddlTipoEvento" ID="ddlTipoEvento">
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxComboBox>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0" style="width: 100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 20%">
                                                                                <dxcp:ASPxLabel runat="server" Text="In&#237;cio:" Width="100%" Font-Size="11px" ID="lblInicio"></dxcp:ASPxLabel>

                                                                            </td>
                                                                            <td style="width: 10%"></td>
                                                                            <td style="width: 20%">
                                                                                <dxcp:ASPxLabel runat="server" Text="T&#233;rmino:" Width="100%" Font-Size="11px" ID="lblTermino"></dxcp:ASPxLabel>

                                                                            </td>
                                                                            <td style="width: 10%"></td>
                                                                            <td style="width: 40%">
                                                                                <dxcp:ASPxLabel runat="server" Text="Local:" Width="100%" Font-Size="11px" ID="lblLocal"></dxcp:ASPxLabel>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>

                                                                            <td style="padding-right: 5px;">
                                                                                <dxcp:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" PopupVerticalAlign="TopSides" EncodeHtml="False" Width="100%" DisplayFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" ClientInstanceName="ddlInicioPrevisto" ID="ddlInicioPrevisto">
                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje"></CalendarProperties>

                                                                                    <ClientSideEvents DateChanged="function(s, e) {
	ddlTerminoPrevisto.SetDate(s.GetValue());
	calendar = ddlTerminoPrevisto.GetCalendar();
  	if ( calendar )
    	calendar.minDate = new Date(s.GetValue());
}"></ClientSideEvents>

                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxDateEdit>

                                                                            </td>
                                                                            <td style="padding-right: 10px;">
                                                                                <dxcp:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtHoraInicio" CssClass="dx_Hora" ID="txtHoraInicio">
                                                                                    <MaskSettings Mask="HH:mm"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                            <td style="padding-right: 5px;">
                                                                                <dxcp:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" PopupVerticalAlign="TopSides" EncodeHtml="False" Width="100%" DisplayFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" ClientInstanceName="ddlTerminoPrevisto" ID="ddlTerminoPrevisto">
                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje"></CalendarProperties>

                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxDateEdit>

                                                                            </td>
                                                                            <td style="padding-right: 10px;">
                                                                                <dxcp:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtHoraTermino" CssClass="dx_Hora" ID="txtHoraTermino">
                                                                                    <MaskSettings Mask="HH:mm"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                            <td>
                                                                                <dxcp:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="memoLocal" ID="memoLocal">
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" ValidationGroup="MKE">
                                                                                        <RequiredField ErrorText="Campo Obrigat&#243;rio"></RequiredField>
                                                                                    </ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxLabel runat="server" Text="Pauta:" Font-Size="11px" ID="lblPauta"></dxcp:ASPxLabel>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="memoPauta" Width="100%" Height="460px" EnableTheming="True" ID="memoPauta">
                                                                    <Toolbars>
                                                                        <dxhe:HtmlEditorToolbar>
                                                                            <Items>
                                                                                <dxhe:ToolbarCutButton></dxhe:ToolbarCutButton>
                                                                                <dxhe:ToolbarCopyButton></dxhe:ToolbarCopyButton>
                                                                                <dxhe:ToolbarPasteButton></dxhe:ToolbarPasteButton>
                                                                                <dxhe:ToolbarPasteFromWordButton></dxhe:ToolbarPasteFromWordButton>
                                                                                <dxhe:ToolbarUndoButton BeginGroup="True"></dxhe:ToolbarUndoButton>
                                                                                <dxhe:ToolbarRedoButton></dxhe:ToolbarRedoButton>
                                                                                <dxhe:ToolbarRemoveFormatButton BeginGroup="True"></dxhe:ToolbarRemoveFormatButton>
                                                                                <dxhe:ToolbarSuperscriptButton BeginGroup="True"></dxhe:ToolbarSuperscriptButton>
                                                                                <dxhe:ToolbarSubscriptButton></dxhe:ToolbarSubscriptButton>
                                                                                <dxhe:ToolbarInsertOrderedListButton BeginGroup="True"></dxhe:ToolbarInsertOrderedListButton>
                                                                                <dxhe:ToolbarInsertUnorderedListButton></dxhe:ToolbarInsertUnorderedListButton>
                                                                                <dxhe:ToolbarIndentButton BeginGroup="True"></dxhe:ToolbarIndentButton>
                                                                                <dxhe:ToolbarOutdentButton></dxhe:ToolbarOutdentButton>
                                                                                <dxhe:ToolbarInsertLinkDialogButton BeginGroup="True"></dxhe:ToolbarInsertLinkDialogButton>
                                                                                <dxhe:ToolbarUnlinkButton></dxhe:ToolbarUnlinkButton>
                                                                                <dxhe:ToolbarInsertImageDialogButton Visible="False"></dxhe:ToolbarInsertImageDialogButton>
                                                                                <dxhe:ToolbarCheckSpellingButton BeginGroup="True" Visible="False"></dxhe:ToolbarCheckSpellingButton>
                                                                                <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                                    <Items>
                                                                                        <dxhe:ToolbarInsertTableDialogButton Text="Insert Table..." ToolTip="Insert Table..." BeginGroup="True"></dxhe:ToolbarInsertTableDialogButton>
                                                                                        <dxhe:ToolbarTablePropertiesDialogButton BeginGroup="True"></dxhe:ToolbarTablePropertiesDialogButton>
                                                                                        <dxhe:ToolbarTableRowPropertiesDialogButton></dxhe:ToolbarTableRowPropertiesDialogButton>
                                                                                        <dxhe:ToolbarTableColumnPropertiesDialogButton></dxhe:ToolbarTableColumnPropertiesDialogButton>
                                                                                        <dxhe:ToolbarTableCellPropertiesDialogButton></dxhe:ToolbarTableCellPropertiesDialogButton>
                                                                                        <dxhe:ToolbarInsertTableRowAboveButton BeginGroup="True"></dxhe:ToolbarInsertTableRowAboveButton>
                                                                                        <dxhe:ToolbarInsertTableRowBelowButton></dxhe:ToolbarInsertTableRowBelowButton>
                                                                                        <dxhe:ToolbarInsertTableColumnToLeftButton></dxhe:ToolbarInsertTableColumnToLeftButton>
                                                                                        <dxhe:ToolbarInsertTableColumnToRightButton></dxhe:ToolbarInsertTableColumnToRightButton>
                                                                                        <dxhe:ToolbarSplitTableCellHorizontallyButton BeginGroup="True"></dxhe:ToolbarSplitTableCellHorizontallyButton>
                                                                                        <dxhe:ToolbarSplitTableCellVerticallyButton></dxhe:ToolbarSplitTableCellVerticallyButton>
                                                                                        <dxhe:ToolbarMergeTableCellRightButton></dxhe:ToolbarMergeTableCellRightButton>
                                                                                        <dxhe:ToolbarMergeTableCellDownButton></dxhe:ToolbarMergeTableCellDownButton>
                                                                                        <dxhe:ToolbarDeleteTableButton BeginGroup="True"></dxhe:ToolbarDeleteTableButton>
                                                                                        <dxhe:ToolbarDeleteTableRowButton></dxhe:ToolbarDeleteTableRowButton>
                                                                                        <dxhe:ToolbarDeleteTableColumnButton></dxhe:ToolbarDeleteTableColumnButton>
                                                                                    </Items>
                                                                                </dxhe:ToolbarTableOperationsDropDownButton>
                                                                                <dxhe:ToolbarFullscreenButton></dxhe:ToolbarFullscreenButton>
                                                                            </Items>
                                                                        </dxhe:HtmlEditorToolbar>
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

                                                                    <Settings AllowHtmlView="False" AllowPreview="False"></Settings>

                                                                    <SettingsHtmlEditing>
                                                                        <PasteFiltering Attributes="class"></PasteFiltering>
                                                                    </SettingsHtmlEditing>

                                                                    <StylesToolbars>
                                                                        <ToolbarItem>
                                                                            <Paddings Padding="1px"></Paddings>
                                                                        </ToolbarItem>
                                                                    </StylesToolbars>
                                                                </dxhe:ASPxHtmlEditor>

                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </dxcp:ContentControl>
                                    </ContentCollection>
                                </dxcp:TabPage>
                                <dxcp:TabPage Name="TabB" Text="Participantes">
                                    <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>

                                    <TabStyle Width="170px" Wrap="True"></TabStyle>
                                    <ContentCollection>
                                        <dxcp:ContentControl runat="server">
                                            <div id="divTabParticipantes" style="overflow-y: auto">
                                                <table cellspacing="0" cellpadding="0" width="98%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 355px">
                                                                <dxcp:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_dispon_veis_ %>" ClientInstanceName="lblDisponiveis" Font-Size="11px" ID="lblDisponiveis"></dxcp:ASPxLabel>

                                                            </td>
                                                            <td align="center" style="width: 60px"></td>
                                                            <td align="left" style="width: 355px">
                                                                <dxcp:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_selecionados_ %>" ClientInstanceName="lblSelecionado" Font-Size="11px" ID="lblSelecionado"></dxcp:ASPxLabel>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <dxcp:ASPxListBox runat="server" CallbackPageSize="10" EnableCallbackMode="True" EncodeHtml="False" Rows="3" SelectionMode="CheckColumn" ClientInstanceName="lbDisponiveis" Width="100%" Height="100px" ID="lbDisponiveis" OnCallback="lbDisponiveis_Callback">
                                                                                <ItemStyle>
                                                                                    <SelectedStyle BackColor="#FFE4AC"></SelectedStyle>
                                                                                </ItemStyle>

                                                                                <SettingsLoadingPanel Text=""></SettingsLoadingPanel>

                                                                                <FilteringSettings ShowSearchUI="True"></FilteringSettings>

                                                                                <ClientSideEvents SelectedIndexChanged="UpdateButtons" Init="UpdateButtons
"></ClientSideEvents>

                                                                                <ValidationSettings>
                                                                                    <ErrorImage Height="14px" Width="14px"></ErrorImage>

                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>

                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxcp:ASPxListBox>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 5px">
                                                                            <dxcp:ASPxLabel runat="server" Text="Grupos:" Font-Size="11px" ID="lblGrupos0"></dxcp:ASPxLabel>

                                                                            <dxcp:ASPxCallback runat="server" ClientInstanceName="callback" ID="callback" OnCallback="callback_Callback">
                                                                                <ClientSideEvents BeginCallback="function(s, e) {
	mostraDivAtualizando(&#39;Atualizando...&#39;); 
}"
                                                                                    EndCallback="function(s, e) {
    var delimitador = &quot;&#165;&quot;;
  	var listaCodigos = s.cp_ListaCodigos;
  	var arrayItens = listaCodigos.split(&#39;;&#39;);

    //arrayItens.sort();
    var array3 = new Array();

    for (i = 0; i &lt; arrayItens.length; i++)
    {
        temp = arrayItens[i].split(delimitador);
        if((temp[0] != null) &amp;&amp; (temp[1] != null))
        {
           array3.push(temp[1]);
        }
    }
    //lbDisponiveis.BeginUpdate(); 
    lbDisponiveis.UnselectAll();
    lbDisponiveis.SelectValues(array3);
    //lbDisponiveis.EndUpdate();    

    UpdateButtons();    
    setTimeout(&#39;fechaTelaEdicao();&#39;, 10);
}"></ClientSideEvents>
                                                                            </dxcp:ASPxCallback>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxcp:ASPxListBox runat="server" EnableCallbackMode="True" EncodeHtml="False" Rows="3" SelectionMode="CheckColumn" ClientInstanceName="lbGrupos" EnableClientSideAPI="True" Width="100%" ID="lbGrupos">
                                                                                <ItemStyle>
                                                                                    <SelectedStyle BackColor="#FFE4AC"></SelectedStyle>
                                                                                </ItemStyle>

                                                                                <SettingsLoadingPanel Text=""></SettingsLoadingPanel>

                                                                                <FilteringSettings ShowSearchUI="True"></FilteringSettings>

                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	callback.PerformCallback(s.GetValue());
}"></ClientSideEvents>

                                                                                <ValidationSettings>
                                                                                    <ErrorImage Height="14px" Width="14px"></ErrorImage>

                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>

                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxcp:ASPxListBox>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td align="center">
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="height: 28px" valign="middle">
                                                                                <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADDTodos" ClientEnabled="False" Text="&gt;&gt;" EncodeHtml="False" Width="45px" Height="25px" Font-Bold="True" ID="btnADDTodos">
                                                                                    <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
    lb_moveTodosItens(lbDisponiveis,lbSelecionados);
	UpdateButtons();    
	capturaCodigosInteressados();



}"></ClientSideEvents>

                                                                                    <Paddings Padding="0px"></Paddings>
                                                                                </dxcp:ASPxButton>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 28px">
                                                                                <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnADD" ClientEnabled="False" Text="&gt;" EncodeHtml="False" Width="45px" Height="25px" Font-Bold="True" ID="btnADD">
                                                                                    <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	lb_moveItem(lbDisponiveis, lbSelecionados);
	UpdateButtons();	
	capturaCodigosInteressados();
}"></ClientSideEvents>

                                                                                    <Paddings Padding="0px"></Paddings>
                                                                                </dxcp:ASPxButton>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 28px">
                                                                                <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMV" ClientEnabled="False" Text="&lt;" EncodeHtml="False" Width="45px" Height="25px" Font-Bold="True" ID="btnRMV">
                                                                                    <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	lb_moveItem(lbSelecionados, lbDisponiveis);
	UpdateButtons();
	capturaCodigosInteressados();	
}"></ClientSideEvents>

                                                                                    <Paddings Padding="0px"></Paddings>
                                                                                </dxcp:ASPxButton>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 28px">
                                                                                <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRMVTodos" ClientEnabled="False" Text="&lt;&lt;" EncodeHtml="False" Width="45px" Height="25px" Font-Bold="True" ID="btnRMVTodos">
                                                                                    <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	lb_moveTodosItens(lbSelecionados, lbDisponiveis);
    UpdateButtons();
	capturaCodigosInteressados();
}"></ClientSideEvents>

                                                                                    <Paddings Padding="0px"></Paddings>
                                                                                </dxcp:ASPxButton>

                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td align="left">
                                                                <dxcp:ASPxListBox runat="server" EnableSynchronization="True" EncodeHtml="False" Rows="4" SelectionMode="CheckColumn" ClientInstanceName="lbSelecionados" Width="100%" Height="100px" ID="lbSelecionados" OnCallback="lbSelecionados_Callback">
                                                                    <ItemStyle>
                                                                        <SelectedStyle BackColor="#FFE4AC"></SelectedStyle>
                                                                    </ItemStyle>

                                                                    <SettingsLoadingPanel Text=""></SettingsLoadingPanel>

                                                                    <FilteringSettings ShowSearchUI="True"></FilteringSettings>

                                                                    <ClientSideEvents EndCallback="function(s, e) {	
    btnSalvar.SetEnabled(true);
	verificaEnvioPauta();	

    capturaCodigosInteressados();	        
    
	lpCarregando.Hide();
}"
                                                                        SelectedIndexChanged="UpdateButtons" Init="UpdateButtons"></ClientSideEvents>

                                                                    <ValidationSettings>
                                                                        <ErrorImage Height="14px" Width="14px"></ErrorImage>

                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>

                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                </dxcp:ASPxListBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top"></td>
                                                            <td style="width: 60px" align="center"></td>
                                                            <td align="center">
                                                                <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfRiscosSelecionados" ID="hfRiscosSelecionados"></dxcp:ASPxHiddenField>

                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </dxcp:ContentControl>
                                    </ContentCollection>
                                </dxcp:TabPage>
                                <dxcp:TabPage Name="TabC" Text="Ata">
                                    <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>

                                    <TabStyle Width="95px" Wrap="True"></TabStyle>
                                    <ContentCollection>
                                        <dxcp:ContentControl runat="server">
                                            <div id="divTabAta" style="overflow-y: auto">
                                                <table cellspacing="0" cellpadding="0" width="98%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="position: inherit">
                                                                <table cellspacing="0" cellpadding="0" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 110px">
                                                                                <dxcp:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_in_cio_real_ %>" Font-Size="11px" ID="lblInicioReal"></dxcp:ASPxLabel>

                                                                            </td>
                                                                            <td style="padding-right: 10px"></td>
                                                                            <td style="width: 110px">
                                                                                <dxcp:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_t_rmino_real_ %>" Font-Size="11px" ID="lblTerminoReal"></dxcp:ASPxLabel>

                                                                            </td>
                                                                            <td style="width: 55px"></td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 140px">
                                                                                <dxcp:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" PopupVerticalAlign="TopSides" EncodeHtml="False" Width="100%" DisplayFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" ClientInstanceName="ddlInicioReal" ID="ddlInicioReal">
                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje"></CalendarProperties>

                                                                                    <ClientSideEvents DateChanged="function(s, e) {
                ddlTerminoReal.SetDate(s.GetValue());
  	calendar = ddlTerminoReal.GetCalendar();
  	if ( calendar )
    	calendar.minDate = new Date(s.GetValue());
}"></ClientSideEvents>

                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxDateEdit>

                                                                            </td>
                                                                            <td style="padding-right: 10px; padding-left: 10px;">
                                                                                <dxcp:ASPxTextBox runat="server" ClientInstanceName="txtHoraInicioAta" CssClass="dx_Hora" ID="txtHoraInicioAta">
                                                                                    <MaskSettings Mask="HH:mm"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                            <td style="width: 140px">
                                                                                <dxcp:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" PopupVerticalAlign="TopSides" EncodeHtml="False" Width="100%" DisplayFormatString="<%$ Resources:traducao, reunioes_dd_mm_yyyy %>" ClientInstanceName="ddlTerminoReal" ID="ddlTerminoReal">
                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje"></CalendarProperties>

                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxDateEdit>

                                                                            </td>
                                                                            <td style="width: 55px; padding-right: 10px; padding-left: 10px;">
                                                                                <dxcp:ASPxTextBox runat="server" ClientInstanceName="txtHoraTerminoAta" CssClass="dx_Hora" ID="txtHoraTerminoAta">
                                                                                    <MaskSettings Mask="HH:mm"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxcp:ASPxTextBox>

                                                                            </td>
                                                                            <td>
                                                                                <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/botoes/btnPDF.png" ToolTip="<%$ Resources:traducao, reunioes_relat_rio_de_ata_de_reuni_o %>" ClientInstanceName="btnImprimir" Cursor="pointer" ID="btnImprimir">
                                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var tipoEnvio = &quot;&quot;;
	if(verificarDadosAta())
	{
       var codigoEvento = hfGeral.Get(&quot;codigoEvento&quot;);
	   var codigoProjeto = hfGeral.Get(&quot;CodigoProjetoAtual&quot;);
       var moduloSistema = hfGeral.Get(&quot;moduloSistema&quot;);			
       abrejanelaImpressaoReuniao(codigoProjeto, codigoEvento, moduloSistema);
    }
}"></ClientSideEvents>
                                                                                </dxcp:ASPxImage>

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
                                                                <dxcp:ASPxLabel runat="server" Text="<%$ Resources:traducao, reunioes_resumo_da_reuni_o_ %>" Font-Size="11px" ID="lblAta"></dxcp:ASPxLabel>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="memoAta" Width="100%" ID="memoAta">
                                                                    <Toolbars>
                                                                        <dxhe:HtmlEditorToolbar>
                                                                            <Items>
                                                                                <dxhe:ToolbarCutButton></dxhe:ToolbarCutButton>
                                                                                <dxhe:ToolbarCopyButton></dxhe:ToolbarCopyButton>
                                                                                <dxhe:ToolbarPasteButton></dxhe:ToolbarPasteButton>
                                                                                <dxhe:ToolbarPasteFromWordButton></dxhe:ToolbarPasteFromWordButton>
                                                                                <dxhe:ToolbarUndoButton BeginGroup="True"></dxhe:ToolbarUndoButton>
                                                                                <dxhe:ToolbarRedoButton></dxhe:ToolbarRedoButton>
                                                                                <dxhe:ToolbarRemoveFormatButton BeginGroup="True"></dxhe:ToolbarRemoveFormatButton>
                                                                                <dxhe:ToolbarSuperscriptButton BeginGroup="True"></dxhe:ToolbarSuperscriptButton>
                                                                                <dxhe:ToolbarSubscriptButton></dxhe:ToolbarSubscriptButton>
                                                                                <dxhe:ToolbarInsertOrderedListButton BeginGroup="True"></dxhe:ToolbarInsertOrderedListButton>
                                                                                <dxhe:ToolbarInsertUnorderedListButton></dxhe:ToolbarInsertUnorderedListButton>
                                                                                <dxhe:ToolbarIndentButton BeginGroup="True"></dxhe:ToolbarIndentButton>
                                                                                <dxhe:ToolbarOutdentButton></dxhe:ToolbarOutdentButton>
                                                                                <dxhe:ToolbarInsertLinkDialogButton BeginGroup="True"></dxhe:ToolbarInsertLinkDialogButton>
                                                                                <dxhe:ToolbarUnlinkButton></dxhe:ToolbarUnlinkButton>
                                                                                <dxhe:ToolbarInsertImageDialogButton Visible="False"></dxhe:ToolbarInsertImageDialogButton>
                                                                                <dxhe:ToolbarCheckSpellingButton BeginGroup="True" Visible="False"></dxhe:ToolbarCheckSpellingButton>
                                                                                <dxhe:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                                    <Items>
                                                                                        <dxhe:ToolbarInsertTableDialogButton Text="Insert Table..." ToolTip="Insert Table..." BeginGroup="True"></dxhe:ToolbarInsertTableDialogButton>
                                                                                        <dxhe:ToolbarTablePropertiesDialogButton BeginGroup="True"></dxhe:ToolbarTablePropertiesDialogButton>
                                                                                        <dxhe:ToolbarTableRowPropertiesDialogButton></dxhe:ToolbarTableRowPropertiesDialogButton>
                                                                                        <dxhe:ToolbarTableColumnPropertiesDialogButton></dxhe:ToolbarTableColumnPropertiesDialogButton>
                                                                                        <dxhe:ToolbarTableCellPropertiesDialogButton></dxhe:ToolbarTableCellPropertiesDialogButton>
                                                                                        <dxhe:ToolbarInsertTableRowAboveButton BeginGroup="True"></dxhe:ToolbarInsertTableRowAboveButton>
                                                                                        <dxhe:ToolbarInsertTableRowBelowButton></dxhe:ToolbarInsertTableRowBelowButton>
                                                                                        <dxhe:ToolbarInsertTableColumnToLeftButton></dxhe:ToolbarInsertTableColumnToLeftButton>
                                                                                        <dxhe:ToolbarInsertTableColumnToRightButton></dxhe:ToolbarInsertTableColumnToRightButton>
                                                                                        <dxhe:ToolbarSplitTableCellHorizontallyButton BeginGroup="True"></dxhe:ToolbarSplitTableCellHorizontallyButton>
                                                                                        <dxhe:ToolbarSplitTableCellVerticallyButton></dxhe:ToolbarSplitTableCellVerticallyButton>
                                                                                        <dxhe:ToolbarMergeTableCellRightButton></dxhe:ToolbarMergeTableCellRightButton>
                                                                                        <dxhe:ToolbarMergeTableCellDownButton></dxhe:ToolbarMergeTableCellDownButton>
                                                                                        <dxhe:ToolbarDeleteTableButton BeginGroup="True"></dxhe:ToolbarDeleteTableButton>
                                                                                        <dxhe:ToolbarDeleteTableRowButton></dxhe:ToolbarDeleteTableRowButton>
                                                                                        <dxhe:ToolbarDeleteTableColumnButton></dxhe:ToolbarDeleteTableColumnButton>
                                                                                    </Items>
                                                                                </dxhe:ToolbarTableOperationsDropDownButton>
                                                                                <dxhe:ToolbarFullscreenButton></dxhe:ToolbarFullscreenButton>
                                                                            </Items>
                                                                        </dxhe:HtmlEditorToolbar>
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

                                                                    <Settings AllowHtmlView="False" AllowPreview="False"></Settings>

                                                                    <SettingsHtmlEditing>
                                                                        <PasteFiltering Attributes="class"></PasteFiltering>
                                                                    </SettingsHtmlEditing>

                                                                    <StylesToolbars>
                                                                        <ToolbarItem>
                                                                            <Paddings Padding="1px"></Paddings>
                                                                        </ToolbarItem>
                                                                    </StylesToolbars>
                                                                </dxhe:ASPxHtmlEditor>

                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </dxcp:ContentControl>
                                    </ContentCollection>
                                </dxcp:TabPage>
                                <dxcp:TabPage Name="tabPendencias" Text="Pendências da reunião anterior" ToolTip="Pendencias da reunião anterior">
                                    <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>

                                    <TabStyle Width="300px" Wrap="True"></TabStyle>
                                    <ContentCollection>
                                        <dxcp:ContentControl runat="server">
                                            <div id="divTabPendenciasReuniaoAnterior" style="overflow-y: auto">
                                                <table style="width: 98%" cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxLabel runat="server" Text="Status:" Font-Size="11px" ID="ASPxLabel2"></dxcp:ASPxLabel>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxComboBox runat="server" SelectedIndex="0" Width="332px" ID="ddlStatusPendencia">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvPendencias.PerformCallback();
}"></ClientSideEvents>
                                                                    <Items>
                                                                        <dxcp:ListEditItem Selected="True" Text="Todos" Value="-1"></dxcp:ListEditItem>
                                                                        <dxcp:ListEditItem Text="Atrasada" Value="Atrasada"></dxcp:ListEditItem>
                                                                        <dxcp:ListEditItem Text="Em Execu&#231;&#227;o" Value="Em_Execu&#231;&#227;o"></dxcp:ListEditItem>
                                                                        <dxcp:ListEditItem Text="Futura" Value="Futura"></dxcp:ListEditItem>
                                                                        <dxcp:ListEditItem Text="In&#237;cio atrasado" Value="In&#237;cio atrasado"></dxcp:ListEditItem>
                                                                    </Items>
                                                                </dxcp:ASPxComboBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 10px">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxcp:ASPxGridView runat="server" KeyboardSupport="True" AutoGenerateColumns="False" KeyFieldName="CodigoTarefa" ClientInstanceName="gvPendencias" Width="100%" ID="gvPendencias" OnCustomButtonInitialize="gvPendencias_CustomButtonInitialize" OnCustomColumnDisplayText="gvPendencias_CustomColumnDisplayText" OnCustomCallback="gvPendencias_CustomCallback" OnHtmlDataCellPrepared="gvPendencias_HtmlDataCellPrepared" OnAfterPerformCallback="gvPendencias_AfterPerformCallback">
                                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	gvPendencias.SetFocusedRowIndex(e.visibleIndex);
	e.processOnServer = false;
    if (e.buttonID == &quot;btnComentarios&quot;)
    {
//       pcComentarios.Show();
       pnGeral.PerformCallback(e.visibleIndex);       
    }	
}"></ClientSideEvents>
                                                                    <Templates>
                                                                        <FooterRow>
                                                                            <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellpadding="0" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <dxe:ASPxImage ID="ASPxImage4" runat="server" Height="16px" ImageUrl="~/imagens/Verde.gif">
                                                                                                    </dxe:ASPxImage>
                                                                                                </td>
                                                                                                <td style="padding-right: 10px; padding-left: 3px;">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                                                        Text="Em execução">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxImage ID="ASPxImage8" runat="server" Height="16px" ImageUrl="~/imagens/amarelo.gif">
                                                                                                    </dxe:ASPxImage>
                                                                                                </td>
                                                                                                <td style="padding-right: 10px; padding-left: 3px;">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                                                        Text="Início atrasado">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxImage ID="ASPxImage5" runat="server" Height="16px" ImageUrl="~/imagens/Vermelho.gif">
                                                                                                    </dxe:ASPxImage>
                                                                                                </td>
                                                                                                <td style="padding-right: 10px; padding-left: 3px;">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                                                                        Text="Atrasada">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxImage ID="ASPxImage6" runat="server" Height="16px" ImageUrl="~/imagens/Relogio.png">
                                                                                                    </dxe:ASPxImage>
                                                                                                </td>
                                                                                                <td style="padding-right: 10px; padding-left: 3px;">
                                                                                                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                                                                                        Text="Futura">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td align="right">
                                                                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                                            Text="<%$ Resources:traducao, reunioes__pend_ncias_da_reuni_o_anterior %>" Font-Italic="True">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>

                                                                        </FooterRow>
                                                                    </Templates>

                                                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                                                    <Settings ShowGroupPanel="True" ShowFooter="True" VerticalScrollableHeight="120" VerticalScrollBarMode="Visible"></Settings>

                                                                    <SettingsResizing ColumnResizeMode="Control"></SettingsResizing>

                                                                    <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                                                    <Columns>
                                                                        <dxcp:GridViewDataTextColumn FieldName="DescricaoTarefa" ShowInCustomizationForm="True" Caption="<%$ Resources:traducao, reunioes_tarefa %>" VisibleIndex="3"></dxcp:GridViewDataTextColumn>
                                                                        <dxcp:GridViewDataTextColumn FieldName="NomeUsuarioResponsavel" ShowInCustomizationForm="True" Caption="<%$ Resources:traducao, reunioes_respons_vel %>" VisibleIndex="4"></dxcp:GridViewDataTextColumn>
                                                                        <dxcp:GridViewDataDateColumn FieldName="TerminoPrevisto" ShowInCustomizationForm="True" Width="170px" Caption="<%$ Resources:traducao, reunioes_t_rmino_previsto %>" VisibleIndex="6">
                                                                            <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, reunioes__0_dd_mm_yyyy_hh_mm_ %>"></PropertiesDateEdit>

                                                                            <Settings ShowFilterRowMenu="True"></Settings>
                                                                        </dxcp:GridViewDataDateColumn>
                                                                        <dxcp:GridViewDataDateColumn FieldName="TerminoReal" ShowInCustomizationForm="True" Width="170px" Caption="<%$ Resources:traducao, reunioes_t_rmino_real %>" VisibleIndex="7">
                                                                            <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, reunioes__0_dd_mm_yyyy_hh_mm_ %>"></PropertiesDateEdit>

                                                                            <Settings ShowFilterRowMenu="True"></Settings>
                                                                        </dxcp:GridViewDataDateColumn>
                                                                        <dxcp:GridViewDataTextColumn FieldName="CodigoTarefa" ShowInCustomizationForm="True" Caption="CodigoTarefa" Visible="False" VisibleIndex="9"></dxcp:GridViewDataTextColumn>
                                                                        <dxcp:GridViewDataTextColumn FieldName="Anotacoes" ShowInCustomizationForm="True" Caption="<%$ Resources:traducao, reunioes_anotacoes %>" Visible="False" VisibleIndex="8"></dxcp:GridViewDataTextColumn>
                                                                        <dxcp:GridViewDataTextColumn FieldName="DescricaoReuniao" ShowInCustomizationForm="True" Caption="<%$ Resources:traducao, reunioes_reuni_o %>" VisibleIndex="5">
                                                                            <CellStyle Wrap="True"></CellStyle>
                                                                        </dxcp:GridViewDataTextColumn>
                                                                        <dxcp:GridViewDataImageColumn FieldName="Estagio" ShowInCustomizationForm="True" Name="Estagio" Width="50px" Caption=" " VisibleIndex="0">
                                                                            <PropertiesImage DisplayFormatString="&lt;img {0}&gt;"></PropertiesImage>
                                                                            <HeaderTemplate>
                                                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                    <tr>
                                                                                        <td align="center">
                                                                                            <dxm:ASPxMenu ID="menu2" runat="server" BackColor="Transparent" ClientInstanceName="menu2"
                                                                                                ItemSpacing="5px" OnItemClick="menu2_ItemClick" OnInit="menu2_Init">
                                                                                                <Paddings Padding="0px" />
                                                                                                <Items>
                                                                                                    <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="<%$ Resources:traducao, reunioes_incluir %>">
                                                                                                        <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                                        </Image>
                                                                                                    </dxm:MenuItem>
                                                                                                    <dxm:MenuItem Name="btnExportar" Text="" ToolTip="<%$ Resources:traducao, reunioes_exportar %>">
                                                                                                        <Items>
                                                                                                            <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="<%$ Resources:traducao, reunioes_exportar_para_xls %>">
                                                                                                                <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                                                </Image>
                                                                                                            </dxm:MenuItem>
                                                                                                            <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="<%$ Resources:traducao, reunioes_exportar_para_pdf %>">
                                                                                                                <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                                                </Image>
                                                                                                            </dxm:MenuItem>
                                                                                                            <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="<%$ Resources:traducao, reunioes_exportar_para_rtf %>">
                                                                                                                <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                                                </Image>
                                                                                                            </dxm:MenuItem>
                                                                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="<%$ Resources:traducao, reunioes_exportar_para_html %>" ClientVisible="False">
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
                                                                                                            <dxm:MenuItem Text="Salvar" ToolTip="<%$ Resources:traducao, reunioes_salvar_layout %>">
                                                                                                                <Image IconID="save_save_16x16">
                                                                                                                </Image>
                                                                                                            </dxm:MenuItem>
                                                                                                            <dxm:MenuItem Text="Restaurar" ToolTip="<%$ Resources:traducao, reunioes_restaurar_layout %>">
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
                                                                        </dxcp:GridViewDataImageColumn>
                                                                    </Columns>

                                                                    <Styles>
                                                                        <Footer>
                                                                            <Paddings Padding="0px"></Paddings>
                                                                        </Footer>
                                                                    </Styles>
                                                                </dxcp:ASPxGridView>

                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </dxcp:ContentControl>
                                    </ContentCollection>
                                </dxcp:TabPage>
                                <dxcp:TabPage Name="TabD" Text="Plano de A&#231;&#227;o">
                                    <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>

                                    <TabStyle Width="200px" Wrap="True"></TabStyle>
                                    <ContentCollection>
                                        <dxcp:ContentControl runat="server">
                                            <div id="divTabPlanoAcao" style="overflow-y: auto">
                                                <div runat="server" id="Content4Div">
                                                </div>
                                            </div>
                                        </dxcp:ContentControl>
                                    </ContentCollection>
                                </dxcp:TabPage>
                                <dxcp:TabPage Name="tabAnexos" Text="Anexos">
                                    <ActiveTabStyle BackColor="#C7DFB9"></ActiveTabStyle>

                                    <TabStyle Width="150px" Wrap="True"></TabStyle>
                                    <ContentCollection>
                                        <dxcp:ContentControl runat="server">

                                            <iframe id="frmAnexosReuniao" frameborder="0" scrolling="yes"
                                                height="350px" width="98%"></iframe>

                                        </dxcp:ContentControl>
                                    </ContentCollection>
                                </dxcp:TabPage>
                            </TabPages>

                            <ClientSideEvents ActiveTabChanged="function(s, e) {

                                AdjustSize();
                                if(s.GetActiveTab().index == 0 || s.GetActiveTab().index == 1)
	{
		btnEnviarPauta.SetText(traducao.reunioes_enviar_pauta);
		if(s.GetActiveTab().index == 1)
			UpdateButtons();
		
	}
                else if(e.tab.index == 3)
{
                             btnEnviarPauta.SetText(traducao.reunioes_enviar_ata);
                             gvPendencias.PerformCallback();
}
	else if(e.tab.index == 5)
	{
        btnEnviarPauta.SetText(traducao.reunioes_enviar_ata);
		if(atualizarURLAnexos != 'N')
	    {
	        atualizarURLAnexos = 'N';
			document.getElementById('frmAnexosReuniao').src = s.cpUrlAnexos;
		}
	}
	else
		btnEnviarPauta.SetText(traducao.reunioes_enviar_ata);

	verificaEnvioPauta();
}"
                                ActiveTabChanging="function(s, e) {
AdjustSize();

	e.cancel = podeMudarAba(s, e);
}"
                                Init="function(s, e) {       
                                AdjustSize();
                                }"></ClientSideEvents>

                            <TabStyle Width="150px"></TabStyle>
                        </dxcp:ASPxPageControl>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <table cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td style="padding-right: 5px; width: 115px">
                                        <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnEnviarPauta" Text="Enviar Pauta" ValidationGroup="MKE" Width="100%" ID="btnEnviarPauta">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	var tipoEnvio = &quot;&quot;;
                window.top.mostraConfirmacao(traducao.reunioes_a_reuni_o_ser__salva__deseja_continuar_, confirmaClickEnviarPauta, null);
}"></ClientSideEvents>

                                            <Paddings Padding="0px"></Paddings>
                                        </dxcp:ASPxButton>
                                    </td>
                                    <td style="width: 115px; padding-right: 5px">
                                        <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE" Width="100%" ID="btnSalvar">
                                            <ClientSideEvents Click="function(s, e) {
                                                e.processOnServer = false;
	capturaCodigosInteressados();
	if(verificarDadosPreenchidos())
                {
                         hfGeral.Set(&quot;StatusSalvar&quot;,&quot;0&quot;);
                                                var op = '';
                                                if(hfGeral.Get('TipoOperacao') == '')
                                                {
                                                op = callbackTelaReuniao.cpTipoOperacao;
                                                }
                                                else
                                                {
                                                op = hfGeral.Get('TipoOperacao');
                                                }
	                     callbackTelaReuniao.PerformCallback(op);                        
                }
}"></ClientSideEvents>
                                            <Paddings Padding="0px"></Paddings>
                                        </dxcp:ASPxButton>
                                    </td>
                                    <td style="width: 115px">
                                        <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar2" Text="Fechar" Width="100%" ID="btnFechar3">
                                            <ClientSideEvents Click="function(s, e) {	
	                                            e.processOnServer = false;
                                                callbackTelaReuniao.PerformCallback('Fechar'); 
}"></ClientSideEvents>

                                            <Paddings Padding="0px"></Paddings>
                                        </dxcp:ASPxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ShowCloseButton="False" ShowHeader="False" Target="_top" Width="270px"
            ID="pcUsuarioIncluido">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td style="" align="center"></td>
                                <td style="width: 70px" align="center" rowspan="3">
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
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcMensagemPauta"
            CloseAction="None" HeaderText="Envio de pauta" PopupAction="None" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="900px" ID="pcMensagemPauta"
            EnableViewState="False" PopupVerticalOffset="-15">
            <ClientSideEvents Closing="function(s, e) {
	tabControl.SetActiveTab(tabControl.GetTabByName('TabA')); 
}"></ClientSideEvents>
            <ContentStyle>
                <Paddings Padding="3px" PaddingLeft="3px" PaddingTop="3px" PaddingRight="3px" PaddingBottom="3px"></Paddings>
            </ContentStyle>
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Texto de apresenta&#231;&#227;o:" ID="Label1"></dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxhe:ASPxHtmlEditor runat="server" ClientInstanceName="heEncabecadoAta" Width="100%"
                                        Height="240px" ID="heEncabecadoAta">
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
                                                    <dxhe:ToolbarBoldButton BeginGroup="True">
                                                    </dxhe:ToolbarBoldButton>
                                                    <dxhe:ToolbarItalicButton>
                                                    </dxhe:ToolbarItalicButton>
                                                    <dxhe:ToolbarUnderlineButton>
                                                    </dxhe:ToolbarUnderlineButton>
                                                    <dxhe:ToolbarStrikethroughButton>
                                                    </dxhe:ToolbarStrikethroughButton>
                                                    <dxhe:ToolbarJustifyLeftButton BeginGroup="True">
                                                    </dxhe:ToolbarJustifyLeftButton>
                                                    <dxhe:ToolbarJustifyCenterButton>
                                                    </dxhe:ToolbarJustifyCenterButton>
                                                    <dxhe:ToolbarJustifyRightButton>
                                                    </dxhe:ToolbarJustifyRightButton>
                                                    <dxhe:ToolbarJustifyFullButton>
                                                    </dxhe:ToolbarJustifyFullButton>
                                                    <dxhe:ToolbarBackColorButton BeginGroup="True">
                                                    </dxhe:ToolbarBackColorButton>
                                                    <dxhe:ToolbarFontColorButton>
                                                    </dxhe:ToolbarFontColorButton>
                                                </Items>
                                            </dxhe:HtmlEditorToolbar>
                                        </Toolbars>
                                        <Settings AllowHtmlView="False" AllowPreview="False"></Settings>
                                        <SettingsDialogs>
                                            <InsertImageDialog>
                                                <SettingsImageSelector>
                                                    <CommonSettings AllowedFileExtensions=".jpe, .jpeg, .jpg, .gif, .png" />
                                                </SettingsImageSelector>
                                            </InsertImageDialog>
                                            <InsertLinkDialog>
                                                <SettingsDocumentSelector>
                                                    <CommonSettings AllowedFileExtensions=".rtf, .pdf, .doc, .docx, .odt, .txt, .xls, .xlsx, .ods, .ppt, .pptx, .odp" />
                                                </SettingsDocumentSelector>
                                            </InsertLinkDialog>
                                        </SettingsDialogs>
                                        <SettingsHtmlEditing>
                                            <PasteFiltering Attributes="class"></PasteFiltering>
                                        </SettingsHtmlEditing>
                                    </dxhe:ASPxHtmlEditor>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px" align="right">
                                    <table cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnEnviarEncabecadoPauta"
                                                        Text="Enviar" ValidationGroup="MKE" Width="100px"
                                                        ID="btnEnviarEncabecadoPauta" EnableViewState="False">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;

	var tipoEnvio = '';

	if(tabControl.GetActiveTab().index == 0 || tabControl.GetActiveTab().index == 1){
		if(verificarDadosPreenchidos())
		{
			tipoEnvio = 'EnviarPauta';
			callbackTelaReuniao.PerformCallback(tipoEnvio);
		}
	}
	else{
		if(verificarDadosAta())
		{
			tipoEnvio = 'EnviarAta';
			callbackTelaReuniao.PerformCallback(tipoEnvio);
		}
	}
}"></ClientSideEvents>
                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnCancelarEncabecadoPauta"
                                                        Text="Fechar" Width="100px" ID="btnCancelarEncabecadoPauta"
                                                        EnableViewState="False">
                                                        <ClientSideEvents Click="function(s, e) {	
	pcMensagemPauta.Hide();
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
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
        <dxcp:ASPxCallback ID="callbackTelaReuniao" runat="server" ClientInstanceName="callbackTelaReuniao" OnCallback="callbackTelaReuniao_Callback">
            <ClientSideEvents EndCallback="function(s, e) 
{
        if(s.cpErro != '')
        {
                    window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
        }
        else if(s.cpSucesso != '')
        {
                    window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3400);
                    
                    if(hfGeral.Get('TipoOperacao') == 'Incluir')
                   {
                             hfGeral.Set('codigoEvento', s.cpCodigoEvento);
                             hfGeral.Set('TipoOperacao', 'Editar');
                             tabControl.cpUrlAnexos = '../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=RE&ID=' + s.cpCodigoEvento + '&RO=N&TO=Editar';
                   }   
        }
        if(s.cp_OperacaoOk == 'Fechar')
        {
        window.top.fechaModal();
        }
}" />
        </dxcp:ASPxCallback>
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
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
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter_pendencia" runat="server" GridViewID="gvPendencias"
            OnRenderBrick="ASPxGridViewExporter_pendencia_RenderBrick" PaperKind="A4">
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
    </form>
</body>
</html>
