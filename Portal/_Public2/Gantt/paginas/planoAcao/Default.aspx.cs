using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Presentation.Portal.Gantt;
using Cdis.Brisk.Service.Services.Estrategia;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Public_Gantt_paginas_planoAcao_Default : BriskGanttPageBase
{   
    protected int idEntidade;
    protected int idObjeto;    
    protected string langCode;
    protected bool isIniciativas;
    protected int idPlanoAcao;
    protected string iniciaisObjeto;
    public string jsonTraducao;
    public string strNomeTitulo;
    public string strDescricaoTitulo;

    protected void Page_Load(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);

        VerificarAuth();

        iniciaisObjeto = Request.QueryString["IniciaisObjeto"] + "";
        
        idObjeto = (Request.QueryString["COE"] != null && Request.QueryString["COE"].ToString() != "") ? int.Parse(Request.QueryString["COE"].ToString()) : 0;
        idPlanoAcao = (Request.QueryString["CPA"] != null && Request.QueryString["CPA"].ToString() != "") ? int.Parse(Request.QueryString["CPA"].ToString()) : 0;       
        isIniciativas = Request.QueryString["TrazerIniciativas"] + "" != "N";
        idEntidade = UsuarioLogado.CodigoEntidade;

        //idObjeto = 1051;
        //idPlanoAcao = 0;
        //isIniciativas = true;
        //idEntidade = 111;
        //iniciaisObjeto = "OB";

        List<string> listTraducaoItem = new List<string>()
        {
            "RecursosHumanos_expandir_todos",
            "RecursosHumanos_contrair_todos",
            "aumentar_zoom",
            "diminuir_zoom",
            "tela_cheia",
            "fechar"
        };

        if (iniciaisObjeto == "IN")
        {
            strNomeTitulo = Resources.traducao.indicador;
            strDescricaoTitulo = UowApplication.UowService.GetUowService<IndicadorService>().GetIndicadorPorCodigo(idObjeto).NomeIndicador;
        }
        else
        {
            strNomeTitulo = Resources.traducao.objetivo_estrategico;
            strDescricaoTitulo = UowApplication.UowService.GetUowService<ObjetoEstrategiaService>().GetObjetoEstrategiaPorCodigo(idObjeto).DescricaoObjetoEstrategia;
        }            

        jsonTraducao = Cdis.Brisk.Infra.Core.Util.ResourceUtil.GetListResourceItem(typeof(Resources.traducao), listTraducaoItem).ToJson();
        langCode = GetLangPage();        
    }
}