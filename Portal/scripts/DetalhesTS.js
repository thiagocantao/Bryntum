// JScript File
var keypressPorcentual = "";
var keypressTrabalhoReal = "";
var keypressTerminoReal = "";
var keypressTrabalhoRestante = "";
var trabalhoRestanteReal = "";
var mudouRestante = false;
var valorTrabalhoReal;

/*
txtInicioPrevisto
txtTerminoPrevisto
txtTabalhoPrevisto
txtInicio
txtTermino
txtTrabalho

txtPorcentaje
ddlInicioReal
ddlTerminoReal
txtTrabalhoReal
txtTrabalhoRestante
*/

function verificarPorcentual() //Porcentual concluido digitado
{
    var percentual = txtPorcentaje.GetValue();
    var inicioReal = ddlInicioReal.GetDate();
    var terminoReal = ddlTerminoReal.GetDate();
    var trabalhoReal = txtTrabalhoReal.GetValue();
    var trabalhoRestante = txtTrabalhoRestante.GetValue();

    var inicioPrevisto = txtInicioPrevisto.GetValue();
    var terminoPrevisto = txtTerminoPrevisto.GetValue();
    var trabalhoPrevisto = txtTabalhoPrevisto.GetValue();
    var inicio = txtInicio.GetValue();
    var termino = txtTermino.GetValue();
    var trabalho = txtTrabalho.GetValue();

    var trabalhoTotal = trabalhoReal + trabalhoRestante;

    var novoPercentual;
    var novoInicioReal;
    var novoTerminoReal;
    var novoTrabalhoReal;
    var novoTrabalhoRestante;

    if (inicioReal == null)
        novoInicioReal = inicioPrevisto != null ? inicioPrevisto : inicio;
    else
        novoInicioReal = inicioReal;

    if (percentual >= 100) {

        if (terminoReal == null)
            novoTerminoReal = terminoPrevisto != null ? terminoPrevisto : termino;
        else
            novoTerminoReal = terminoReal;

        novoTrabalhoReal = trabalhoTotal;

        novoTrabalhoRestante = 0;
    }
    else {
        novoTerminoReal = null;

        novoTrabalhoReal = trabalhoTotal != 0 ? (trabalhoTotal * percentual / 100) : 0;

        novoTrabalhoRestante = trabalhoTotal - novoTrabalhoReal;
    }

    ddlInicioReal.SetDate(novoInicioReal);
    ddlTerminoReal.SetDate(novoTerminoReal);
    txtTrabalhoReal.SetValue(novoTrabalhoReal);
    txtTrabalhoRestante.SetValue(novoTrabalhoRestante < 0 ? 0 : novoTrabalhoRestante);
}

function verificarTerminoReal() //Termino Real digitado
{
    var percentual = txtPorcentaje.GetValue();
    var inicioReal = ddlInicioReal.GetDate();
    var terminoReal = ddlTerminoReal.GetDate();
    var trabalhoReal = txtTrabalhoReal.GetValue();
    var trabalhoRestante = txtTrabalhoRestante.GetValue();

    var inicioPrevisto = txtInicioPrevisto.GetValue();
    var terminoPrevisto = txtTerminoPrevisto.GetValue();
    var trabalhoPrevisto = txtTabalhoPrevisto.GetValue();
    var inicio = txtInicio.GetValue();
    var termino = txtTermino.GetValue();
    var trabalho = txtTrabalho.GetValue();

    var trabalhoTotal = trabalhoReal + trabalhoRestante;

    var novoPercentual = 100;
    var novoInicioReal;
    var novoTerminoReal;
    var novoTrabalhoReal;
    var novoTrabalhoRestante = 0;

    if (inicioReal == null)
        novoInicioReal = inicioPrevisto != null ? inicioPrevisto : inicio;
    else
        novoInicioReal = inicioReal;

    if (trabalhoReal == null)
        novoTrabalhoReal = trabalhoPrevisto != null ? trabalhoPrevisto : trabalho;
    else
        novoTrabalhoReal = trabalhoReal;

    txtPorcentaje.SetValue(novoPercentual);
    ddlInicioReal.SetDate(novoInicioReal);
    txtTrabalhoReal.SetValue(novoTrabalhoReal);
    txtTrabalhoRestante.SetValue(novoTrabalhoRestante < 0 ? 0 : novoTrabalhoRestante);
}

function verificarTrabalhoReal() //Trablaho real digitado
{
    var percentual = txtPorcentaje.GetValue();
    var inicioReal = ddlInicioReal.GetDate();
    var terminoReal = ddlTerminoReal.GetDate();
    var trabalhoReal = txtTrabalhoReal.GetValue();
    var trabalhoRestante = txtTrabalhoRestante.GetValue();

    var inicioPrevisto = txtInicioPrevisto.GetValue();
    var terminoPrevisto = txtTerminoPrevisto.GetValue();
    var trabalhoPrevisto = txtTabalhoPrevisto.GetValue();
    var inicio = txtInicio.GetValue();
    var termino = txtTermino.GetValue();
    var trabalho = txtTrabalho.GetValue();    

    var novoPercentual;
    var novoInicioReal;
    var novoTerminoReal;
    var novoTrabalhoReal;
    var novoTrabalhoRestante;

    if (inicioReal == null)
        novoInicioReal = inicioPrevisto != null ? inicioPrevisto : inicio;
    else
        novoInicioReal = inicioReal;

    if (mudouRestante == false) {        
        novoTrabalhoRestante = trabalhoRestante - (trabalhoReal - valorTrabalhoReal);
    }
    else {
        novoTrabalhoRestante = trabalhoRestante;
    }

    var trabalhoTotal = trabalhoReal + novoTrabalhoRestante;

    if (novoTrabalhoRestante == 0) {

        if (trabalhoReal == null)
            novoTrabalhoReal = trabalhoPrevisto != null ? trabalhoPrevisto : trabalho;
        else
            novoTrabalhoReal = trabalhoReal;
    }
    else
        novoTerminoReal = null;

    novoPercentual = trabalhoTotal == 0 ? 0 : (trabalhoReal / trabalhoTotal);

    txtPorcentaje.SetValue(novoPercentual * 100);
    ddlInicioReal.SetDate(novoInicioReal);
    ddlTerminoReal.SetDate(novoTerminoReal);
    txtTrabalhoRestante.SetValue(novoTrabalhoRestante < 0 ? 0 : novoTrabalhoRestante);
}

function verificarTrabalhoRestante() //Trabalho restante digitado
{
    var percentual = txtPorcentaje.GetValue();
    var inicioReal = ddlInicioReal.GetDate();
    var terminoReal = ddlTerminoReal.GetDate();
    var trabalhoReal = txtTrabalhoReal.GetValue();
    var trabalhoRestante = txtTrabalhoRestante.GetValue();

    var inicioPrevisto = txtInicioPrevisto.GetValue();
    var terminoPrevisto = txtTerminoPrevisto.GetValue();
    var trabalhoPrevisto = txtTabalhoPrevisto.GetValue();
    var inicio = txtInicio.GetValue();
    var termino = txtTermino.GetValue();
    var trabalho = txtTrabalho.GetValue();

    var trabalhoTotal = trabalhoReal + trabalhoRestante;

    var novoPercentual;
    var novoInicioReal;
    var novoTerminoReal;
    var novoTrabalhoReal;
    var novoTrabalhoRestante;

    if (trabalhoRestante == 0) {
        if (inicioReal == null)
            novoInicioReal = inicioPrevisto != null ? inicioPrevisto : inicio;
        else
            novoInicioReal = inicioReal;

        if (terminoReal == null)
            novoTerminoReal = terminoPrevisto != null ? terminoPrevisto : termino;
        else
            novoTerminoReal = terminoReal;

        if (trabalhoReal == null)
            novoTrabalhoReal = trabalhoPrevisto != null ? trabalhoPrevisto : trabalho;
        else
            novoTrabalhoReal = trabalhoReal;

        novoPercentual = 1;

        ddlInicioReal.SetDate(novoInicioReal);
        ddlTerminoReal.SetDate(novoTerminoReal);
        txtTrabalhoReal.SetValue(novoTrabalhoReal);

    }
    else {
        ddlTerminoReal.SetDate(null);
    }

    novoPercentual = trabalhoTotal == 0 ? 0 : (trabalhoReal / trabalhoTotal);

    txtPorcentaje.SetValue(novoPercentual * 100);
}

function verificarDadosPreenchidos() {
    var estado = true;
    var msgErro = traducao.DetalhesTS_lista_de_pend_ncias_para_cadastrar + "\n\n";
    var dataAtual = new Date();
    var dataInicio = ddlInicioReal.GetDate();
    var dataTermino = ddlTerminoReal.GetDate();
    var trabajoReal = parseFloat(txtTrabalhoReal.GetText().replace('.', '').replace(',', '.'));
    var trabajoRestante = parseFloat(txtTrabalhoRestante.GetText().replace('.', '').replace(',', '.'));
    var percentual = parseFloat(txtPorcentaje.GetText().replace('.', '').replace(',', '.'));
    var indicaMarco = txtIndicaMarco.GetText().toUpperCase();

    if ("S" == indicaMarco) {
        if ((percentual != 0) && (percentual != 100)) {
            msgErro += traducao.DetalhesTS_para_uma_tarefa_marco__o_percentual_conclu_do_deve_0_ou_100__ + "\n";
            estado = false;
        }
        else if ( ( (dataInicio == null) && (dataTermino !=null) )
              ||  ( (dataInicio != null) && (dataTermino ==null) )
              || ((dataInicio != null) && (dataInicio.valueOf() !== dataTermino.valueOf()))) {
            msgErro += traducao.DetalhesTS_as_datas_de_in_cio_e_t_rmino_n_o_podem_ser_diferentes_quando_a_tarefa___um_marco_ + "\n";
            estado = false;
        }
        else {
            if ((percentual != 0) && ( (dataInicio == null) || (dataTermino == null) ) ){
                msgErro += traducao.DetalhesTS_data_de__nicio_e_t_rmino_dever_o_ser_informadas_ + "\n";
                estado = false;
            }
            if ((percentual == 0) && ( (dataInicio != null) || (dataTermino != null) ) ){
                msgErro += traducao.DetalhesTS_o_percentual_conclu_do_deve_ser_informado_ + "\n";
                estado = false;
            }
        }
    }
    else {

        if (trabajoReal == 0 && dataInicio != null) {
            msgErro += traducao.DetalhesTS_trabalho_real_dever__ser_informado_ + "\n";
            estado = false;
        }
        if (trabajoReal != 0 && dataInicio == null) {
            msgErro += traducao.DetalhesTS_data_de_in_cio_dever__ser_informada_ + "\n";
            estado = false;
        }
        if (trabajoRestante == 0 && dataTermino == null) {
            msgErro += traducao.DetalhesTS_data_de_t_rmino_dever__ser_informada_ + "\n";
            estado = false;
        }
    }
    if (dataInicio != null && dataTermino != null && dataInicio > dataTermino) {
        msgErro += traducao.DetalhesTS_data_de_in_cio_n_o_pode_ser_maior_que_a_data_de_t_rmino_ + "\n";
        estado = false;
    }
    if (dataInicio != null && dataInicio > dataAtual) {
        msgErro += traducao.DetalhesTS_data_de_in_cio_n_o_pode_ser_maior_que_a_data_atual_ + "\n";
        estado = false;
    }
    if (dataTermino != null && dataTermino > dataAtual) {
        msgErro += traducao.DetalhesTS_data_de_t_rmino_n_o_pode_ser_maior_que_a_data_atual_;
        estado = false;
    }
    if (dataTermino != null && percentual != 100) {
        msgErro += traducao.DetalhesTS_o_percentual_conclu_do_deve_ser_100__quando_existir_t_rmino_real_;
        estado = false;
    }

    if (estado == false)
        window.top.mostraMensagem(msgErro, 'erro', true, false, null);

    return estado;
}
