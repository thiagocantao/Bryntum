// JScript File

var keyGvDados;

function onClick_CustomButtomGvDado(s, e)
{
	e.processOnServer = false;
	
	if ( 'btnCustomCompartilhar' == e.buttonID)
	{
	    OnGridFocusedRowChangedPopup(gvDados);
    }
}

function OnGridFocusedRowChangedPopup(grid) 
{
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoObjeto;NomeObjeto;NomeObjetoPai;DoMapaEstrategico', getDadosPopup); //getKeyGvDados);
}

function getDadosPopup(valor)
{
    var idObjeto =  (valor[0] != null ? valor[0] : "-1");
    var tituloObjeto = (valor[1] != null ? valor[1] : "");
    var tituloMapa = (valor[2] != null ? valor[2] : "");
    var doMapaEstrategico = (valor[3] != null ? valor[3] : "");
    
    var auxiliar = tituloMapa.split(":")[0];
    auxiliar = auxiliar.substr(0, auxiliar.length - 1);
    //sStr.substr(0, sStr.length - 1);
    
    if(auxiliar != "Mapa" & auxiliar !="")
        var conteudoTME = tituloMapa + " ]-[ " + traducao.PermissaoObjetoEstrategia_mapa_estrat_gico__ + doMapaEstrategico;
    else
        var conteudoTME = traducao.PermissaoObjetoEstrategia_mapa_estrat_gico__ + doMapaEstrategico;
    
    //Preparação dos parâmetros para fixar o popup no centro da tela.
    var window_width = 800;
    var window_height = 490;
    var newfeatures= 'scrollbars=no,resizable=yes';
    var window_top = (screen.height-window_height)/2;
    var window_left = (screen.width-window_width)/2;
    window.top.showModal("../InteressadoObjetoEstrategia.aspx?ITO=" + hfGeral.Get("iniciaisTO") + "&COE=" + idObjeto + "&TOE=" + tituloObjeto + "&TME=" + conteudoTME, 'Permissão', 800, 490, '', null); 
}

function OnGridFocusedRowChanged(grid) 
{
    grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoObjeto', getKeyGvDados); //getKeyGvDados);
}

function getKeyGvDados(valor)
{
    hfGeral.Set("idObjeto" , (valor != null ? valor : "-1"));
}

function onSelected_IndexDllListarPor(s, e)
{
    var opcao = s.GetValue();
    if(window.hfGeral && window.hfGeral.Contains('iniciaisTO'))
		window.hfGeral.Set('iniciaisTO', opcao);
    
    pnCallback.PerformCallback(opcao);
    //ddlFiltarPor.PerformCallback(opcao);
}

function onSelected_dllFiltarPor(s, e)
{
    var opcao = s.GetValue();

    gvDados.PerformCallback(opcao);
    //pnCallbackGvDados.PerformCallback(opcao);
}

