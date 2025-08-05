// JScript File
var keypressPorcentual = "";
var keypressTrabalhoReal = "";
var keypressTerminoReal = "";
var keypressTrabalhoRestante = "";
var trabalhoRestanteReal = "";
/*-------------------------------------------------
<summary>
Apos de prencher o porcentagem e sair do foco do componente, analiza seu valor.
100 (porcentual)    : prenche automaticamente as datas de inicio e termino.
< 100 (porcentual)  : no realiza nihuma mudança.
</summary>
<return></return>
<Parameters></Parameters>
-------------------------------------------------*/

if (String.prototype.ano == null) {

    String.prototype.ano = function (idiomaPadrao) {

        var separador = this.replace(/[0-9]/gi, '').substr(0, 1);
        var data = this.split(separador);
        return data[2];
    }

}

if (String.prototype.mes == null) {

    String.prototype.mes = function (idiomaPadrao) {
        var separador = this.replace(/[0-9]/gi, '').substr(0, 1);
        var data = this.split(separador);
        var i = 1;
        if (idiomaPadrao !== true) {
            if (traducao.geral_formato_data_js == 'MM/DD/YYYY') // Idioma "en" English
            {
                i = 0;
            }
        }
        return (data[i]);
    }

}

if (String.prototype.dia == null) {

    String.prototype.dia = function (idiomaPadrao) {
        var separador = this.replace(/[0-9]/gi, '').substr(0, 1);
        var data = this.split(separador);
        var i = 0;
        if (idiomaPadrao !== true) {
            if (traducao.geral_formato_data_js == 'MM/DD/YYYY') // Idioma "en" English
            {
                i = 1;
            }
        }
        return (data[i]);
    }

}

function verificarPorcentual() //Porcentual concluido digitado
{
    var trabalhoPrevisto = parseFloat(txtTabalhoPrevisto.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));

    var terminoPrevisto = txtTerminoPrevisto.GetText();
    var termino = txtTermino.GetText();

    var dateTermino = null;

    if (terminoPrevisto != "")// Si Data de 'termino previsto' estivese prenchido.
    {
        var MD_Y = terminoPrevisto.ano();
        var MD_M = terminoPrevisto.mes();
        var MD_D = terminoPrevisto.dia();
        dateTermino = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));
    }
    else if (termino != "")//Si data 'termino previsto' não estivese preenchido, utilizo ao data de 'termino'.
    {
        var MD_Y = termino.ano();
        var MD_M = termino.mes();
        var MD_D = termino.dia();
        dateTermino = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));
    }


    var inicioPrevisto = txtInicioPrevisto.GetText();
    var inicio = txtInicio.GetText();
    var dateInicio = null;

    if (inicioPrevisto != "") // Si Data de 'inicio previsto' estivese prenchido.
    {
        MD_Y = inicioPrevisto.ano();
        MD_M = inicioPrevisto.mes();
        MD_D = inicioPrevisto.dia();
        dateInicio = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));
    }
    else if (inicio != "")//Si data 'inicio previsto' não estivese preenchido, utilizo ao data de 'inicio'.
    {
        MD_Y = inicio.ano();
        MD_M = inicio.mes();
        MD_D = inicio.dia();
        dateInicio = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));
    }

    var porcentaje = parseFloat(txtPorcentaje.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));
    var trabalhoReal = parseFloat(txtTrabalhoReal.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));
    var trabalhoRestante = parseFloat(txtTrabalhoRestante.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));
    var trabalhoTotal = trabalhoReal + trabalhoRestante;
    var trabalhoPorcentaje = porcentaje * trabalhoTotal / 100;
    var trabalhoRestante = trabalhoTotal - trabalhoPorcentaje;
    var trabalhoReal = trabalhoPorcentaje;

    txtTrabalhoReal.SetText(trabalhoReal.toString().replace('.', traducao.geral_separador_decimal));
    txtTrabalhoRestante.SetText(trabalhoRestante.toString().replace('.', traducao.geral_separador_decimal));

    //Controle segundo Porcentaje.
    if (porcentaje == 100) {
        if (ddlTerminoReal.GetDate() == null) {
            ddlTerminoReal.SetDate(dateTermino);
        }

        if (parseFloat(txtTrabalhoReal.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.')) == 0)
            txtTrabalhoReal.SetText(trabalhoPrevisto.toString().replace('.', traducao.geral_separador_decimal));

        txtTrabalhoRestante.SetText("0");

    }
    else {
        ddlTerminoReal.SetDate(null);
    }

    if (porcentaje <= 0) {
        ddlInicioReal.SetDate(null);
        txtTrabalhoRestante.SetText((trabalhoRestante + trabalhoReal).toString().replace('.', traducao.geral_separador_decimal));
        txtTrabalhoReal.SetText("");
    } else if (ddlInicioReal.GetDate() == null) {
        if (dateInicio != null) {
            ddlInicioReal.SetDate(dateInicio);
        }
    }
}

/*-------------------------------------------------
<summary>
Apos de preencher Trabalho Real, prenche outros campos segundo certas condições.
Trabalho Restante   : si não tivese prenchido.
Procentagem         : si trabalho total e maior ao cero.
Inicio Real         : si inicio real nao tivese prenchido.
Termino Real        : si no tivese prenchido, e porcentagem seja 100.
</summary>
<return></return>
<Parameters></Parameters>
-------------------------------------------------*/
function verificarTrabalhoReal() //Trablaho real digitado
{
    var trabalhoPrevisto = parseFloat(txtTabalhoPrevisto.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));

    var terminoPrevisto = txtTerminoPrevisto.GetText();
    var termino = txtTermino.GetText();
    var dateTermino = null;

    if (terminoPrevisto != "")// Si Data de 'termino previsto' estivese prenchido.
    {
        var MD_Y = terminoPrevisto.ano();
        var MD_M = terminoPrevisto.mes();
        var MD_D = terminoPrevisto.dia();
        dateTermino = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));
    }
    else if (termino != "") //Si data 'termino previsto' não estivese preenchido, utilizo ao data de 'termino'.
    {
        var MD_Y = termino.ano();
        var MD_M = termino.mes();
        var MD_D = termino.dia();
        dateTermino = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));
    }

    var inicioPrevisto = txtInicioPrevisto.GetText();
    var inicio = txtInicio.GetText();
    var dateInicio = null;

    if (inicioPrevisto != "") // Si Data de 'inicio previsto' estivese prenchido.
    {
        MD_Y = inicioPrevisto.ano();
        MD_M = inicioPrevisto.mes();
        MD_D = inicioPrevisto.dia();
        dateInicio = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));
    }
    else if (inicio != "")//Si data 'inicio previsto' não estivese preenchido, utilizo ao data de 'inicio'.
    {
        MD_Y = inicio.ano();
        MD_M = inicio.mes();
        MD_D = inicio.dia();
        dateInicio = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));
    }


    var trabalhoReal = parseFloat(txtTrabalhoReal.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));
    var trabalhoRestante = parseFloat(txtTrabalhoRestante.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));
    var porcentaje = parseFloat(txtPorcentaje.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));

    if (trabalhoRestante == trabalhoRestanteReal) {
        trabalhoRestante = trabalhoPrevisto - trabalhoReal;

        if (trabalhoRestante < 0)
            txtTrabalhoRestante.SetText("0");
        else
            txtTrabalhoRestante.SetText(trabalhoRestante.toString().replace('.', traducao.geral_separador_decimal));
    }

    var trabalhoTotal = trabalhoReal + trabalhoRestante;
    var porcentaje = 0;
    if (trabalhoTotal > 0)
        porcentaje = Math.round((trabalhoReal * 100 / trabalhoTotal), 2);
    txtPorcentaje.SetText(porcentaje.toString().replace('.', traducao.geral_separador_decimal));

    if (ddlInicioReal.GetDate() == null)
        ddlInicioReal.SetDate(dateInicio);

    if (ddlTerminoReal.GetDate() == null) {
        if (porcentaje == 100)
            ddlTerminoReal.SetDate(dateTermino);
    }
    else if (porcentaje != 100)
        ddlTerminoReal.SetDate(null);


}

/*-------------------------------------------------
<summary>
Apos de prencher Termino Real, prenche otros campos segundo condições.
Trabalho Real   : si fose 0.
Porcetagem      : si fose 0.
Trabalho Restante : si fose 0.
Inicio Real     : si fose null.
</summary>
<return></return>
<Parameters></Parameters>
-------------------------------------------------*/
function verificarTerminoReal() //Termino Real digitado
{
    if (ddlTerminoReal.GetDate() != null) {
        var trabalhoPrevisto = parseFloat(txtTabalhoPrevisto.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));

        var inicioPrevisto = txtInicioPrevisto.GetText();
        MD_Y = inicioPrevisto.ano();
        MD_M = inicioPrevisto.mes();
        MD_D = inicioPrevisto.dia();
        var dateInicio = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));

        if (parseFloat(txtTrabalhoReal.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.')) == 0)
            txtTrabalhoReal.SetText(trabalhoPrevisto + "");

        txtPorcentaje.SetText("100");
        txtTrabalhoRestante.SetText("0");

        if (ddlInicioReal.GetDate() == null)
            ddlInicioReal.SetDate(dateInicio);
    }
}

/*-------------------------------------------------
<summary>
Função que retorna a listagem seleccionada.
</summary>
<return></return>
<Parameters>
s: Componente RadioButtonList.
e: Propiedade do RadioBurronList.
</Parameters>
-------------------------------------------------*/
function verificarTrabalhoRestante() //Trabalho restante digitado
{
    var trabalhoPrevisto = parseFloat(txtTabalhoPrevisto.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));

    var terminoPrevisto = txtTerminoPrevisto.GetText();
    var termino = txtTermino.GetText();
    var dateTermino = null;

    if (terminoPrevisto != "")// Si Data de 'termino previsto' estivese prenchido.
    {
        var MD_Y = terminoPrevisto.ano();
        var MD_M = terminoPrevisto.mes();
        var MD_D = terminoPrevisto.dia();
        dateTermino = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));
    }
    else if (termino != "")//Si data 'termino previsto' não estivese preenchido, utilizo ao data de 'termino'.
    {
        var MD_Y = termino.ano();
        var MD_M = termino.mes();
        var MD_D = termino.dia();
        dateTermino = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));
    }

    var inicioPrevisto = txtInicioPrevisto.GetText();
    var inicio = txtInicio.GetText();
    if (inicioPrevisto != "") // Si Data de 'inicio previsto' estivese prenchido.
    {
        MD_Y = inicioPrevisto.ano();
        MD_M = inicioPrevisto.mes();
        MD_D = inicioPrevisto.dia();
    }
    else //Si data 'inicio previsto' não estivese preenchido, utilizo ao data de 'inicio'.
    {
        MD_Y = inicio.ano();
        MD_M = inicio.mes();
        MD_D = inicio.dia();
    }
    var dateInicio = new Date(parseInt(MD_Y), MD_M - 1, parseInt(MD_D));

    var trabalhoRestante = parseFloat(txtTrabalhoRestante.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));
    var trabalhoReal = parseFloat(txtTrabalhoReal.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));
    //var porcentaje = parseFloat(txtPorcentaje.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));
    var trabalhoTotal = 0;

    if (trabalhoReal == 0 && txtTrabalhoReal.GetEnabled() == true) {
        var trabalhoReal = trabalhoPrevisto - trabalhoRestante;
        if (trabalhoReal > 0)
            txtTrabalhoReal.SetText(trabalhoReal.toString().replace('.', traducao.geral_separador_decimal));
        trabalhoTotal = trabalhoPrevisto + trabalhoRestante;
    }
    else
        trabalhoTotal = trabalhoReal + trabalhoRestante;

    var porcentaje = 0;
    if (trabalhoTotal > 0)
        porcentaje = Math.round((trabalhoReal * 100 / trabalhoTotal), 2);

    txtPorcentaje.SetText(porcentaje.toString().replace('.', traducao.geral_separador_decimal));

    if (trabalhoRestante == 0) {
        if (ddlInicioReal.GetDate() == null && ddlInicioReal.GetEnabled() == true)
            ddlInicioReal.SetDate(dateInicio);
        if (ddlTerminoReal.GetDate() == null && ddlTerminoReal.GetEnabled() == true)
            ddlTerminoReal.SetDate(dateTermino);
    }

    if (porcentaje != 100)
        ddlTerminoReal.SetDate(null);
}

/*-------------------------------------------------
<summary>
Função que retorna um menssgem falando si falta algúm dado a preencher pelo usuario.
</summary>
<return></return>
<Parameters></Parameters>
-------------------------------------------------*/
function verificarDadosPreenchidos() {
    var estado = true;
    var msgErro = traducao.kanban_ListaPendenciasCadastrar + "\n\n";
    var dataAtual = new Date();
    var dataAtualSemHora = new Date(dataAtual.getFullYear() + "/" + dataAtual.getMonth() + "/" + dataAtual.getDate());
    var dataInicio = ddlInicioReal.GetDate();
    if (dataInicio == null) {
        var dataInicioSemHora = null;
    }
    else {
        var dataInicioSemHora = new Date(dataInicio.getFullYear() + "/" + dataInicio.getMonth() + "/" + dataInicio.getDate());
    }
    var dataTermino = ddlTerminoReal.GetDate();
    if (dataTermino == null) {
        var dataTerminoSemHora = null;
    }
    else {
        var dataTerminoSemHora = new Date(dataTermino.getFullYear() + "/" + dataTermino.getMonth() + "/" + dataTermino.getDate());
    }
    var trabajoReal = parseFloat(txtTrabalhoReal.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));
    var trabajoRestante = parseFloat(txtTrabalhoRestante.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));
    var percentual = parseFloat(txtPorcentaje.GetText().replace(traducao.geral_separador_milhar, '').replace(traducao.geral_separador_decimal, '.'));
    var indicaMarco = txtIndicaMarco.GetText().toUpperCase();

    if ("S" == indicaMarco) {
        if ((percentual != 0) && (percentual != 100)) {
            msgErro += traducao.espacoTrabalho_DetalhesTS_para_uma_tarefa_marco__o_percentual_conclu_do_deve_0_ou_100_ + "!\n";
            estado = false;
        }
        else if (((dataInicio == null) && (dataTermino != null))
            || ((dataInicio != null) && (dataTermino == null))
            || ((dataInicio != null) && (dataInicio.valueOf() !== dataTermino.valueOf()))) {
            msgErro += traducao.espacoTrabalho_DetalhesTS_as_datas_de_in_cio_e_t_rmino_n_o_podem_ser_diferentes_quando_a_tarefa___um_marco + "!\n";
            estado = false;
        }
        else {
            if ((percentual != 0) && ((dataInicio == null) || (dataTermino == null))) {
                msgErro += traducao.espacoTrabalho_DetalhesTS_data_de__nicio_e_t_rmino_dever_o_ser_informadas + "!\n";
                estado = false;
            }
            if ((percentual == 0) && ((dataInicio != null) || (dataTermino != null))) {
                msgErro += traducao.espacoTrabalho_DetalhesTS_o_percentual_conclu_do_deve_ser_informado + "!\n";
                estado = false;
            }
        }
    }
    else {

        if (trabajoReal == 0 && dataInicio != null) {
            msgErro += "" + traducao.espacoTrabalho_DetalhesTS_trabalho_real_dever__ser_informado + "!\n";
            estado = false;
        }
        if (trabajoReal != 0 && dataInicio == null) {
            msgErro += traducao.espacoTrabalho_DetalhesTS_data_de_in_cio_dever__ser_informada + "!\n";
            estado = false;
        }
        if (trabajoRestante == 0 && dataTermino == null) {
            msgErro += traducao.espacoTrabalho_DetalhesTS_data_de_t_rmino_dever__ser_informada + "!\n";
            estado = false;
        }


        if (trabajoReal != 0 && trabajoReal > 99999.99) {
            msgErro += traducao.espacoTrabalho_DetalhesTS_o_trabalho_real_n_o_pode_ser_maior_que_99_999_99 + "!\n";
            estado = false;
        }
        if (trabajoRestante != 0 && trabajoRestante > 99999.99) {
            msgErro += traducao.espacoTrabalho_DetalhesTS_o_trabalho_restante_n_o_pode_ser_maior_que_99_999_99 + "!\n";
            estado = false;
        }



    }
    //if (dataInicio != null && dataTermino != null && dataInicio > dataTermino) { // Desabilitado porque as horas entre servidores e estações de trabalho nem sempre estão sincronizadas, então não é apropriado comparar hora do lado cliente com hora do lado servidor.
    if (dataInicio != null && dataTermino != null && dataInicioSemHora > dataTerminoSemHora) {
        msgErro += traducao.espacoTrabalho_DetalhesTS_data_de_in_cio_n_o_pode_ser_maior_que_a_data_de_t_rmino + "!\n";
        estado = false;
    }
    //if (dataInicio != null && dataInicio > dataAtual) { // Desabilitado porque as horas entre servidores e estações de trabalho nem sempre estão sincronizadas, então não é apropriado comparar hora do lado cliente com hora do lado servidor.
    if (dataInicio != null && dataInicioSemHora > dataAtualSemHora) {
        msgErro += traducao.espacoTrabalho_DetalhesTS_data_de_in_cio_n_o_pode_ser_maior_que_a_data_atual + "!\n";
        estado = false;
    }
    //if (dataTermino != null && dataTermino > dataAtual) { // Desabilitado porque as horas entre servidores e estações de trabalho nem sempre estão sincronizadas, então não é apropriado comparar hora do lado cliente com hora do lado servidor.
    if (dataTermino != null && dataTerminoSemHora > dataAtualSemHora) {
        msgErro += traducao.espacoTrabalho_DetalhesTS_data_de_t_rmino_n_o_pode_ser_maior_que_a_data_atual + "!";
        estado = false;
    }
    if (dataTermino != null && percentual != 100) {
        msgErro += traducao.espacoTrabalho_DetalhesTS_o_percentual_conclu_do_deve_ser_100__quando_existir_t_rmino_real + "!";
        estado = false;
    }

    if (estado == false)
        window.top.mostraMensagem(msgErro, 'erro', true, false, null);

    return estado;
}

function funcaoCallbackSalvar() {
    if (verificarDadosPreenchidos()) {
        callBack.PerformCallback();
        //window.top.fechaModal();
    }
}

function funcaoCallbackFechar() {

    window.top.retornoModal = 'N';
    window.top.fechaModalComFooter();
}


function processaAbrePopup(valores) {
    var sUrl = '';
    var sHeaderTitulo = '';
    var sHeight = 0;
    var sWidth = 0;
    var CodigoTipoRecurso = (valores[16] == null) ? "" : valores[16].toString();
    var CodigoAtribuicao = (valores[0] == null) ? "" : valores[0].toString();

    hfGeral.Set("CodigoAtribuicaoSelecionado", CodigoAtribuicao);

    if (CodigoTipoRecurso == '2') {
        showPopupEquipamento(valores);

    }
    else if (CodigoTipoRecurso == '3') {
        showPopupFinanceiro(valores);
    }
}

function showPopupEquipamento(valores) {

    var NomeRecurso = (valores[2] == null) ? "" : valores[2].toString();
    var UnidadeMedidaRecurso = (valores[3] == null) ? "" : valores[3].toString();
    var UnidadeAtribuicaoLB = (valores[4] == null) ? "" : valores[4].toString();
    var CustoUnitarioLB = (valores[5] == null) ? "" : valores[5].toString();
    var CustoLB = (valores[6] == null) ? "" : valores[6].toString();
    var UnidadeAtribuicao = (valores[7] == null) ? "" : valores[7].toString();
    var CustoUnitario = (valores[8] == null) ? "" : valores[8].toString();
    var Custo = (valores[9] == null) ? "" : valores[9].toString();
    var UnidadeAtribuicaoRealInformado = (valores[10] == null) ? "" : valores[10].toString();
    var CustoUnitarioRealInformado = (valores[11] == null) ? "" : valores[11].toString();
    var CustoRealInformado = (valores[12] == null) ? "" : valores[12].toString();
    var UnidadeAtribuicaoRestanteInformado = (valores[13] == null) ? "" : valores[13].toString();
    var CustoUnitarioRestanteInformado = (valores[14] == null) ? "" : valores[14].toString();
    var CustoRestanteInformado = (valores[15] == null) ? "" : valores[15].toString();

    txtRecurso.SetText(NomeRecurso);
    txtUnidadeMedida.SetText(UnidadeMedidaRecurso);

    labelQuantidadeLinhaBase.SetText("Quantidade (" + UnidadeMedidaRecurso + "):");
    txtQuantidadeLinhaBase.SetText(UnidadeAtribuicaoLB);
    spnValorUnitarioLinhaBase.SetText(CustoUnitarioLB);
    spnValorTotalLinhaBase.SetText(CustoLB);

    labelQuantidadeUltimoPlanejamento.SetText("Quantidade (" + UnidadeMedidaRecurso + "):")
    txtQuantidadeUltimoPlanejamento.SetText(UnidadeAtribuicao);
    spnValorUnitarioUltimoPlanejamento.SetText(CustoUnitario);
    spnValorTotalUltimoPlanejamento.SetText(Custo);

    labelQuantidadeRealizado.SetText("Quantidade (" + UnidadeMedidaRecurso + "):")
    txtQuantidadeRealizado.SetText(UnidadeAtribuicaoRealInformado);
    spnValorUnitarioRealizado.SetText(CustoUnitarioRealInformado);
    spnValorTotalRealizado.SetText(CustoRealInformado);

    labelQuantidadeRestante.SetText("Quantidade (" + UnidadeMedidaRecurso + "):");
    txtQuantidadeRestante.SetText(UnidadeAtribuicaoRestanteInformado);
    spnValorUnitarioRestante.SetText(CustoUnitarioRestanteInformado);
    spnValorTotalRestante.SetText(CustoRestanteInformado);

    var popupTituloApontamentoOutrosCustos = hfGeral.Get("popupTituloApontamentoOutrosCustos");

    pcTipoEquipamento.SetHeaderText(popupTituloApontamentoOutrosCustos + ": " + txtTarefa.GetText());
    pcTipoEquipamento.Show();


    
}

function showPopupFinanceiro(valores) {
    var NomeRecurso = (valores[2] == null) ? "" : valores[2].toString();
    var UnidadeMedidaRecurso = (valores[3] == null) ? "" : valores[3].toString();
    var CustoLB = (valores[6] == null) ? "" : valores[6].toString();
    var Custo = (valores[9] == null) ? "" : valores[9].toString();
    var CustoRealInformado = (valores[12] == null) ? "" : valores[12].toString();
    var CustoRestanteInformado = (valores[15] == null) ? "" : valores[15].toString();

    txtRecursoFinanceiro.SetText(NomeRecurso);
    txtUnidadeMedidaFinanceiro.SetText(UnidadeMedidaRecurso);
    spnLinhaBaseFinanceiro.SetText(CustoLB);
    spnUltimoPlanejamentoFinanceiro.SetText(Custo);
    spnRealizadoFinanceiro.SetText(CustoRealInformado);
    spnRestanteFinanceiro.SetText(CustoRestanteInformado);

    pcTipoFinanceiro.SetHeaderText("Apontamento outros custos tarefa: " + txtTarefa.GetText());
    pcTipoFinanceiro.Show();
}


function calculaValorTotalRealizado(variavel1, variavel2) {
    var resultado = variavel1 * variavel2;
    spnValorTotalRealizado.SetValue(resultado);
}

function calculaValorTotalRestante(variavel1, variavel2) {
    var resultado = variavel1 * variavel2;
    spnValorTotalRestante.SetValue(resultado);

}