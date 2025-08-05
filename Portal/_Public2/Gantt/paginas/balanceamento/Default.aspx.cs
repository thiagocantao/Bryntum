using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Presentation.Portal.Gantt;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Public_Gantt_paginas_balanceamento_Default : BriskGanttPageBase
{
    protected int idProjeto;

    protected int idEntidade;
    protected int idPortfolio;
    protected int numCenario;
    protected string langCode;
    public string jsonTraducao;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);

        VerificarAuth();
        idEntidade = UsuarioLogado.CodigoEntidade;
        idPortfolio = UsuarioLogado.CodigoPortfolio;
        var strNumCenario = CDados.getInfoSistema("Cenario");        

        List<string> listTraducaoItem = new List<string>()
        {            
            "aumentar_zoom",
            "diminuir_zoom",
            "tela_cheia",            
            "mostrar_gr_ficos",
            "selecionar",
            "cenario"
        };

        jsonTraducao = Cdis.Brisk.Infra.Core.Util.ResourceUtil.GetListResourceItem(typeof(Resources.traducao), listTraducaoItem).ToJson();
        numCenario = strNumCenario == null ? 1 : Convert.ToInt32(strNumCenario.ToString());             

        langCode = GetLangPage();        
    }
}