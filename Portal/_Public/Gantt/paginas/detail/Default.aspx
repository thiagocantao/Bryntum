<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Gantt_Default" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <title>Cronograma Bryntum </title>
    <link rel="stylesheet" href="../../build/gantt.stockholm.css" id="bryntum-theme">
    <link rel="stylesheet" href="../_shared/shared.css">
    <link rel="stylesheet" href="resources/app.css">
    <style>
        .width100 {
            width: 100%;
        }
    </style>
</head>

<body style="height: 95%">
    <div id="container" class="no-tools">
    </div>
    <form runat="server">
        <div style="display: none;">
            <dxe:ASPxButton ID="btnConfirmaEdicaoCronograma" AutoPostBack="False" ClientInstanceName="btnConfirmaEdicaoCronograma" runat="server" Text="btnConfirmaEdicaoCronograma">
                <ClientSideEvents Click="function(s, e) {                    
	                pcInformacao.Show();
                    e.processOnServer = false;
                }" />
                <Paddings Padding="0px" />
            </dxe:ASPxButton>
        </div>
        <dxpc:ASPxPopupControl ID="pcInformacao" runat="server" ClientInstanceName="pcInformacao"
            HeaderText="Informação"
            Width="500px" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <table cellspacing="1" class="style4">
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="lblInformacao" runat="server"
                                    ClientInstanceName="lblInformacao">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style10"></td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="1" class="style4">
                                    <tr>
                                        <td align="right">
                                            <dxe:ASPxButton ID="btnAbrirCronoBloqueado" runat="server"
                                                Text="Sim" Width="80px" AutoPostBack="False">
                                                <Paddings Padding="0px" />
                                                <ClientSideEvents Click="function(s, e) {
                                                        DesbloquearCronograma();
	                                                    pcInformacao.Hide();
                                                    }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td class="style11">&nbsp;</td>
                                        <td>
                                            <dxe:ASPxButton ID="ASPxButton3" runat="server"
                                                Text="Não" Width="80px" AutoPostBack="False">
                                                <ClientSideEvents Click="function(s, e) {
	                                                    pcInformacao.Hide();
                                                    }" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>


        <dxpc:ASPxPopupControl ID="pcDownload" runat="server" ClientInstanceName="pcDownload"
            HeaderText="Informação"
            Width="500px" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" Height="112px">
            <ClientSideEvents Shown="function(s, e) {
	setTimeout(&quot;pcDownload.Hide()&quot;,10000);
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <dxe:ASPxLabel ID="lblDownload" runat="server" ClientInstanceName="lblDownload"
                        EncodeHtml="False"
                        Text="Download &lt;a href='#'&gt;Aqui...&lt;/a&gt;">
                    </dxe:ASPxLabel>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>

        <dxpc:ASPxPopupControl ID="pcLinhaBase" runat="server" ClientInstanceName="pcLinhaBase"
            HeaderText="Informações da Linha de Base Selecionada"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
            Width="500px">
            <SettingsLoadingPanel Enabled="False" />
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <div id="divInfoLb">
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right"><b style="">Versão:</b> </td>
                                    <td align="left"><span id="descVersao"></span></td>
                                </tr>
                                <tr>
                                    <td align="right"><b style="">Data de solicitação:</b></td>
                                    <td align="left"><span id="DataSolicitacao"></span></td>
                                </tr>
                                <tr>
                                    <td align="right"><b style="">Solicitante:</b></td>
                                    <td align="left"><span id="NomeSolicitante"></span></td>
                                </tr>
                                <tr>
                                    <td align="right"><b style="">Data Aprov/Reprov:</b></td>
                                    <td align="left"><span id="DataAprovacao"></span></td>
                                </tr>
                                <tr>
                                    <td align="right"><b style="">Aprov/Repro por:</b></td>
                                    <td align="left"><span id="NomeAprovador"></span></td>
                                </tr>
                                <tr>
                                    <td align="right"><b style="">Descrição:</b></td>
                                    <td align="left"><span id="Anotacao"></span></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>

        <script src="../../../../scripts/jquery-3.1.1.min.js"></script>
        <script src="../../../../scripts/custom/util/jquery.injectCSS.js"></script>
        <script src="../../../../scripts/custom/util/linqtoo.min.js"></script>
        <script>        

            var idProjeto = '<%=idProjeto%>';
            var langCode = '<%=langCode%>';
            var baseUrlEAP = '<%=baseUrlEAP%>'
            var baseUrl = '<%=Session["baseUrl"]%>'
            var infoCronograma = JSON.parse('<%=jsonInfoCronograma%>');
            var jsonTraducao = JSON.parse('<%=jsonTraducao%>');

            var jsonLinhaBase = JSON.parse('<%=jsonLinhaBase%>');
            var jsonComboLinhaBase = JSON.parse('<%=jsonComboLinhaBase%>');
            var numLinhaBase = '<%=numLinhaBase%>';
            var idUser = '<%=UsuarioLogado.Id%>';
            var recursosCorporativos = JSON.parse('<%=jsonRecursosCorporativos%>');

            function getTraducao(nameKey) {
                if (jsonTraducao == null) {
                    return "No translation";
                } else {
                    var itemTraducao = jsonTraducao.Where(function (item) { return item.Key == nameKey; }).First();
                    return itemTraducao == null ? nameKey : itemTraducao.Text;
                }
            }

            function getDimension() {
                return {
                    width: screen.availWidth - 60,
                    height: screen.availHeight - 220
                }
            }

            //Funções da Cronograma_gantt para os modais de EAP e Cronograma
            function funcaoPosModalEdicao(retorno) {
                var funcFechaModal = function () {
                    window.top.pcModal.GetContentIFrameWindow().parent.existeConteudoCampoAlterado = false;
                    window.top.cancelaFechamentoPopUp = 'N';
                    window.top.fechaModal();
                };
                var existeInformacoesPendentes = VerificaExistenciaInformacoesPendentes();
                if (existeInformacoesPendentes) {
                    var textoMsg = traducao.Cronograma_gantt_existem_altera__es_ainda_n_o_salvas_ + "</br></br>" + traducao.Cronograma_gantt_ao_pressionar__ok___as_altera__es_n_o_salvas_ser_o_perdidas_ + "</br></br>" + traducao.Cronograma_gantt_deseja_continuar_;
                    window.top.mostraMensagem(textoMsg, 'confirmacao', true, true, funcFechaModal);
                    window.top.cancelaFechamentoPopUp = 'S';
                }
                else {
                    recarregar();
                }
            }

            function abreDetalhes(idTarefa, idProjeto, data) {
                var tarefaParam = 'CT=' + idTarefa;
                var dataParam = '&Data=' + data;
                var idProjetoParam = '&IDProjeto=' + idProjeto;
                var titulo = getTraducao("Cronograma_gantt_detalhes_da_tarefa");
                window.top.showModal("PopUpCronograma.aspx?" + tarefaParam + idProjetoParam + dataParam, titulo, 820, 400, "", null);

            }

            function recarregar() {
                location.reload();
            }

            function VerificaExistenciaInformacoesPendentes() {
                var frame = window.top.pcModal.GetContentIFrameWindow().parent;
                return frame.existeConteudoCampoAlterado;
            }

            function AbrirCronograma() {
                window.open(infoCronograma.LinkTasques, 'framePrincipal');
                //window.top.mostraMensagem("erokdfjg", 'erro', true, false, null);
            }

            var isAbrirCronograma;
            function DesbloquearCronograma() {
                var configAjax = {
                    url: baseUrl + '/ApiHandler/GanttHandler.ashx',
                    method: 'POST',
                    headers: { typeResult: "desbloquearCronograma", idprojeto: idProjeto, idUser: idUser }
                };
                configAjax.success = function (ret) {
                    if (ret.IsSuccess) {
                        window.top.mostraMensagem(ret.Message, 'sucesso', false, false, recarregar(), 3000);
                    } else {
                        console.log('Msg:', ret.Message);
                    }
                };

                $.ajax(configAjax);
            }

            function BloquearCronograma() {
                var configAjax = {
                    url: baseUrl + '/ApiHandler/GanttHandler.ashx',
                    method: 'POST',
                    headers: { typeResult: "bloquearCronograma", idprojeto: idProjeto, identity: '<%=UsuarioLogado.Id%>' }
                };
                configAjax.success = function (ret) {
                    if (ret.IsSuccess) {
                        AbrirCronograma()
                    } else {
                        window.top.mostraMensagem(ret.Message, 'erro', true, false, recarregar(), 3000);
                    }
                };

                $.ajax(configAjax);
            }

            function atualizarInfoLb() {
                var linhaDeBase = jsonLinhaBase.Where(function (item) { return item.NumVersao == numLinhaBase }).First();
                var descVersao = jsonComboLinhaBase.Where(function (item) { return item.value == numLinhaBase }).First().text;

                $('#descVersao').text(descVersao);
                $('#DataSolicitacao').text(linhaDeBase.DataSolicitacao);
                $('#NomeSolicitante').text(linhaDeBase.NomeSolicitante);
                $('#DataAprovacao').text(linhaDeBase.DataAprovacao);
                $('#NomeAprovador').text(linhaDeBase.NomeAprovador);
                $('#Anotacao').text(linhaDeBase.Anotacao);

                pcLinhaBase.Show();
            }

            function getDivInfoLb(linhaDeBase, descVersao) {
                var lbVersao = getTraducao("versao");
                var lbDataSolicitacao = getTraducao("data_solicita__o_");
                var lbSolicitante = getTraducao("solicitante");
                var lbDataAprovReprov = getTraducao("data_aprov__reprov__");
                var lbAprovReprovPor = getTraducao("aprov__reprov__por_");
                var lbDescricao = getTraducao("descri__o");
                var lbInformaLinhaBaseSelecionada = getTraducao("informa__es_da_linha_de_base_selecionada");

                return '<div id="divInfoLb">' +
                    '                    <div class="b-gantt-task-title"><u><b>' + lbInformaLinhaBaseSelecionada + '</b> </u></div>                                   ' +
                    '                    <table border="0" cellspacing="0" cellpadding="0">                        ' +
                    '        <tbody>                                                                               ' +
                    '            <tr><td ><b style="">' + lbVersao + ':</b> </td><td align="right">' + descVersao + '</td></tr>                   ' +
                    '            <tr><td ><b style="">' + lbDataSolicitacao + ':</b></td> <td align="right">' + linhaDeBase.DataSolicitacao + '</td></tr>       ' +
                    '            <tr><td ><b style="">' + lbSolicitante + ':</b></td> <td align="right">' + linhaDeBase.NomeSolicitante + '</td></tr>              ' +
                    '            <tr><td ><b style="">' + lbDataAprovReprov + ':</b></td> <td align="right">' + linhaDeBase.DataAprovacao + '</td></tr>            ' +
                    '            <tr><td ><b style="">' + lbAprovReprovPor + ':</b></td> <td align="right">' + linhaDeBase.NomeAprovador + '</td></tr>                ' +
                    '            <tr><td ><b style="">' + lbDescricao + ':</b></td> <td align="right">' + linhaDeBase.Anotacao + '</td></tr>                        ' +
                    '        </tbody>                                                                              ' +
                    '                    </table>                                                                                              ' +
                    '		</div>                                                                                                                     ';
            }

            function getTipLinhaBase() {

                var linhaDeBase = jsonLinhaBase.Where(function (item) { return item.NumVersao == numLinhaBase }).First();
                var descVersao = jsonComboLinhaBase.Where(function (item) { return item.value == numLinhaBase }).First().text;

                return getDivInfoLb(linhaDeBase, descVersao);

            }

            var isShowCaminhoCritico = false;
            function removerClassCaminhoCritico() {
                if (isShowCaminhoCritico) {
                    document.head.removeChild(document.head.children.namedItem("injectCSSContainer"));
                }
            }

            function incluirClassCaminhoCritico() {
                if (!isShowCaminhoCritico) {
                    $.injectCSS({
                        ".isCaminhoCritico": {
                            'color': '#c70f0f',
                            'font-weight': 'bold',
                            'font-size': 'medium'
                        }
                    });
                    isShowCaminhoCritico = true;
                } else {
                    $.injectCSS({
                        ".isCaminhoCritico": {

                        }
                    });
                    isShowCaminhoCritico = false;
                }
            }
    </script>


        <!-- Using Ecma Modules bundle -->

        <script src="../../build/locales/gantt.locale.Nl.js"></script>
        <script src="../../build/locales/gantt.locale.examples.Nl.js"></script>
        <script src="../../build/locales/gantt.locale.Ru.js"></script>
        <script src="../../build/locales/gantt.locale.examples.Ru.js"></script>
        <script src="../../build/locales/gantt.locale.SvSE.js"></script>
        <script src="../../build/locales/gantt.locale.examples.SvSE.js"></script>
        <script src="../../build/locales/gantt.locale.Pt_BR.js"></script>
        <script src="../../../../scripts/custom/util/browser.js"></script>
        <script src="columns/col<%=langCode%>.js"></script>
        <script data-default-locale="<%=langCode%>" src="../../build/gantt.umd.min.js"></script>

        <script src="../_shared/shared.umd.js"></script>
        <script src="app.umd.js"></script>
        <script>
            gantt.project.on('load', function () {
                const recursos = (window.recursosCorporativos || []).map(r => ({
                    id: r.CodigoRecursoCorporativo,
                    name: r.NomeRecursoCorporativo
                }));
                gantt.project.resourceStore.data = recursos;
            });
        </script>
        <dxcp:ASPxCallback ID="callbackAtualizaTela" runat="server" ClientInstanceName="callbackAtualizaTela" OnCallback="callbackAtualizaTela_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
                     window.location.reload();
            }" />
        </dxcp:ASPxCallback>
    </form>
</body>

</html>
