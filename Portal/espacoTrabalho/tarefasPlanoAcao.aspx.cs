using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using CDIS;
using System.Data;

public partial class espacoTrabalho_tarefasPlanoAcao : System.Web.UI.Page
{
    dados cDados;
    private int codigoUnidadeLogada = 0, codigoUnidadeSelecionada = 0;
    private int idUsuarioLogado = 0;
    private int codigoObjetoAssociado = 0;
    private int codigoTipoAssociacao = 0;
    private int codigoToDoList = 0;
    private ASPxGridView gvToDoList;
    private int codigoProjeto = -1;
    private bool somenteLeitura = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        try
        {
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }
        if (Request.QueryString["CTA"] != null && Request.QueryString["CTA"].ToString() != "")
        {
            codigoTipoAssociacao = int.Parse(Request.QueryString["CTA"].ToString());
        }
        else if (Request.QueryString["ITA"] != null && Request.QueryString["ITA"].ToString() != "")
        {
            codigoTipoAssociacao = cDados.getCodigoTipoAssociacao(Request.QueryString["ITA"].ToString());
        }

        if (Request.QueryString["COA"] != null && Request.QueryString["COA"].ToString() != "")
        {
            codigoObjetoAssociado = int.Parse(Request.QueryString["COA"].ToString());
        }
        if (Request.QueryString["CTDL"] != null && Request.QueryString["CTDL"].ToString() != "")
        {
            codigoToDoList = int.Parse(Request.QueryString["CTDL"].ToString());
        }
        if (Request.QueryString["UN"] != null && Request.QueryString["UN"].ToString() != "")
        {
            codigoUnidadeSelecionada = int.Parse(Request.QueryString["UN"].ToString());
        }
        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() != "")
        {
            somenteLeitura = Request.QueryString["RO"].ToString() == "S";
        }

        int alturaTela = 500;

        if (Request.QueryString["AT"] != null && Request.QueryString["AT"].ToString() != "")
        {
            alturaTela = int.Parse(Request.QueryString["AT"].ToString()) - 120;
        }

        int[] convidados = null;

        if (Session["TDL_" + codigoTipoAssociacao + "_" + codigoObjetoAssociado] != null)
        {
            convidados = (int[])(Session["TDL_" + codigoTipoAssociacao + "_" + codigoObjetoAssociado]);
        }
        else
        {
            DataSet dsConvidados = cDados.getUsuarioDaEntidadeAtiva(codigoUnidadeLogada.ToString(), "");

            convidados = new int[dsConvidados.Tables[0].Rows.Count];

            if (cDados.DataSetOk(dsConvidados))
            {
                int i = 0;
                foreach (DataRow dr in dsConvidados.Tables[0].Rows)
                {
                    convidados[i] = int.Parse(dr["CodigoUsuario"].ToString());
                    i++;
                }
            }
        }

        hfGeral.Set("codigoToDoList", codigoToDoList);
        hfGeral.Set("codigoObjetoAssociado", codigoObjetoAssociado);

        Unit tamanho = new Unit("100%");

        txtDescricaoPlanoAcao.Text = getNomeToDoList();

        PlanoDeAcao myPlanoDeAcao = null;
        myPlanoDeAcao = new PlanoDeAcao(cDados.classeDados, codigoUnidadeSelecionada, idUsuarioLogado, null, codigoTipoAssociacao, codigoObjetoAssociado, tamanho, alturaTela, somenteLeitura, convidados.Length == 0 ? null : convidados, false, txtDescricaoPlanoAcao.Text);
        pAcoes.Controls.AddAt(0, myPlanoDeAcao.constroiInterfaceFormulario(codigoToDoList));
        gvToDoList = myPlanoDeAcao.gvToDoList;

        //gvToDoList.Font.Name = "Verdana";
        //gvToDoList.Font.Size = new FontUnit("8pt");
        gvToDoList.ClientInstanceName = "gvToDoList";
        gvToDoList.ClientSideEvents.BeginCallback = "function(s, e) { hfGeralToDoList.Set('NomeToDoList',  txtDescricaoPlanoAcao.GetText());}";
        gvToDoList.ClientSideEvents.Init = @"function(s, e) {
            window.top.lpAguardeMasterPage.Hide(); 
            var height = Math.max(0, document.documentElement.clientHeight - 150);
            s.SetHeight(height);
}";
        gvToDoList.DataBind();


        cDados.aplicaEstiloVisual(Page);

        gvToDoList.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private string getNomeToDoList()
    {
        string nomeToDo = "";

        string comandoSQL = string.Format(
                @"SELECT NomeToDoList 
                        FROM ToDoList 
                       WHERE CodigoToDoList = {0}
               ", codigoToDoList);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (ds != null && ds.Tables[0] != null & ds.Tables[0].Rows.Count > 0)
            nomeToDo = ds.Tables[0].Rows[0]["NomeToDoList"].ToString();

        return nomeToDo;
    }

    private int[] getParticipantesEvento(string listaConvidados)
    {
        string[] strConvidados = listaConvidados.Split(',');

        int[] convidados = new int[strConvidados.Length];

        int i = 0;
        foreach (string usuario in strConvidados)
        {
            if (usuario != "")
                convidados[i] = int.Parse(usuario);
            i++;
        }

        return convidados;
    }

}