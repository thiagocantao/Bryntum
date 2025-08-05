function atualizaGrid()
{
    tlProjetos.PerformCallback();
}

function novaDescricao()
{
    timedCount(0);
}

function timedCount(tempo)
{  
   if(tempo == 3)
   {
    tempo = 0;
    tlProjetos.PerformCallback();
   }
   else	
   {      
      tempo++;
      t=setTimeout("timedCount(" + tempo + ")",1000);      
   }
}


function mudaCorCheck(checkNome, lblNome, valor)
{    
          
}

function mudaCorComboDev(ddlNome, lblNome, valor)
{
            
}

function abreReuniao(codigoUnidade)
{
    window.top.gotoURL('Reunioes/reuniaoTecnicaPlanejamento.aspx?TipoReuniao=T&MOD=PRJ&IOB=UN&COB=' + codigoUnidade, '_self');

}

function abreBoletins(codigoUnidade, nomeUnidade) {
    window.top.gotoURL('_Projetos/Boletim/index.aspx?CodigoUnidade=' + codigoUnidade + '&NomeUnidade=' + nomeUnidade, '_self');
}

function abreResumoDemanda(codigoProjeto, nomeProjeto,tipoDemanda)
{
    var url = "";

    if (tipoDemanda == "S") {
        url = "./cadastroDemandasSimples.aspx?CodigoDemanda=" + codigoProjeto;

        window.top.showModal(url, traducao.ListaProjetos_demanda, 890, 550, atualizaGrid, null);
    }
    else {
        url = window.top.pcModal.cp_Path + "_Projetos/DadosProjeto/indexResumoDemanda.aspx?Tipo=D&IDProjeto=" + codigoProjeto + "&NomeProjeto=" + nomeProjeto;
        window.open(url, '_top');
    }
}

function abreResumoProcesso(codigoProjeto, nomeProjeto)
{
    var url = "";

    url = window.top.pcModal.cp_Path + "_Projetos/DadosProjeto/indexResumoProcesso.aspx?Tipo=PC&IDProjeto=" + codigoProjeto + "&NomeProjeto=" + nomeProjeto;
    window.open(url, '_top');
}

function treeList_ContextMenu(s, e) {
    if (e.objectType == "Header")
        pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
}

function SalvarConfiguracoesLayout() {
    
    callback.PerformCallback("save_layout");
}

function RestaurarConfiguracoesLayout() {
    var funcObj = { funcaoClickOK: function () { callback.PerformCallback("restore_layout"); } }
    window.top.mostraConfirmacao(traducao.ListaProjetos_deseja_restaurar_as_configura__es_originais_do_layout_da_consulta_, function () { funcObj['funcaoClickOK']() }, null);
}


function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}
function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 240;
    tlProjetos.SetHeight(height);
}