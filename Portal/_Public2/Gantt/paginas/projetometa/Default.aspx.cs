using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Presentation.Portal.Gantt;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Public_Gantt_paginas_projetometa_Default : BriskGanttPageBase
{    
    protected int idEntidade;
    protected int idUsuario;
    protected int idCarteira;
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
        idUsuario = UsuarioLogado.Id;        
        idCarteira = UsuarioLogado.CodigoCarteira;

        List<string> listTraducaoItem = new List<string>()
        {
            "RecursosHumanos_expandir_todos",
            "RecursosHumanos_contrair_todos",
            "aumentar_zoom",
            "diminuir_zoom",
            "tela_cheia",
            "fechar"
        };

        jsonTraducao = Cdis.Brisk.Infra.Core.Util.ResourceUtil.GetListResourceItem(typeof(Resources.traducao), listTraducaoItem).ToJson();
        langCode = GetLangPage();
        
    }
}