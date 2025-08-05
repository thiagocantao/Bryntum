var myObject = new Object();
var winGerenciarConsultas = null;
var winSalvarComo = null;
function abreTelaNovaMensagem()
{
    var codProj = hfGeral.Get('hfCodigoProjeto');
	var nomeProj = hfGeral.Get('hfNomeProjeto');
	 myObject = new Object();
     myObject.nomeProjeto = nomeProj; 

    window.top.showModal("../../Mensagens/EnvioMensagens.aspx?CO=" + codProj + "&TA=PR", traducao.indexResumoProjeto_nova_mensagem___ + nomeProj, 950, 580, "", myObject);
}

function funcaoPosModal(retorno)
{
	cbkGeral.PerformCallback(retorno);
}


function atualizaProjeto()
{
    callback.PerformCallback('A');
}

function redirecionaProjeto() {
    callback.PerformCallback('R');
}


function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 130;
    sp_Tela.SetHeight(height);
    nvbMenuProjeto.SetHeight(height - 25);
}


function ExibeConsultaSalvas(codigoLista, codigoUsuario) {
    var parametro = codigoUsuario + ';' + codigoLista;
    popup.ShowWindow(winGerenciarConsultas);
    popup.PerformWindowCallback(winGerenciarConsultas, parametro);
}

function ExibirJanelaSalvarComo() {
    txtNomeConsulta.SetText('');
    popup.ShowWindow(winSalvarComo);
}

function SalvarConsultaComo(nomeConsulta) {
    var framePrincipal = window.frames['framePrincipal'];
    if (framePrincipal.SalvarConsultaComo)
        framePrincipal.SalvarConsultaComo(nomeConsulta);
}

function CarregarConsulta(codigoListaUsuario) {
    var framePrincipal = window.frames['framePrincipal'];
    if (framePrincipal.CarregarConsulta)
        framePrincipal.CarregarConsulta(codigoListaUsuario);
}

if (window.top.lpAguardeMasterPage)
    window.top.lpAguardeMasterPage.Hide();