using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _VisaoMaster_NovaAnalise : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoIndicador;
    private int codigoProjeto;
    private int codigoMeta;
    private int anoResultado = 0;
    private int mesResultado = 0;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        if (!IsPostBack && !IsCallback)
        {
            hfDadosSessao.Set("CodigoEntidade", cDados.getInfoSistema("CodigoEntidade").ToString());
            hfDadosSessao.Set("Resolucao", cDados.getInfoSistema("ResolucaoCliente").ToString());
            hfDadosSessao.Set("IDUsuarioLogado", cDados.getInfoSistema("IDUsuarioLogado").ToString());
            hfDadosSessao.Set("IDEstiloVisual", cDados.getInfoSistema("IDEstiloVisual").ToString());
            hfDadosSessao.Set("NomeUsuarioLogado", cDados.getInfoSistema("NomeUsuarioLogado").ToString());
            hfDadosSessao.Set("CodigoCarteira", cDados.getInfoSistema("CodigoCarteira").ToString());
            cDados.setInfoSistema("TipoEdicao", "I");
        }

        cDados.setInfoSistema("CodigoEntidade", hfDadosSessao.Get("CodigoEntidade").ToString());
        cDados.setInfoSistema("ResolucaoCliente", hfDadosSessao.Get("Resolucao").ToString());
        cDados.setInfoSistema("IDUsuarioLogado", hfDadosSessao.Get("IDUsuarioLogado").ToString());
        cDados.setInfoSistema("IDEstiloVisual", hfDadosSessao.Get("IDEstiloVisual").ToString());
        cDados.setInfoSistema("NomeUsuarioLogado", hfDadosSessao.Get("NomeUsuarioLogado").ToString());
        cDados.setInfoSistema("CodigoCarteira", hfDadosSessao.Get("CodigoCarteira").ToString());

        codigoUsuarioResponsavel = int.Parse(hfDadosSessao.Get("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(hfDadosSessao.Get("CodigoEntidade").ToString());

        codigoIndicador = int.Parse(Request.QueryString["CI"].ToString());
        codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        codigoMeta = int.Parse(Request.QueryString["CM"].ToString());
        mesResultado = int.Parse(Request.QueryString["Mes"].ToString());
        anoResultado = int.Parse(Request.QueryString["Ano"].ToString());
        this.TH(this.TS("NovaAnalise"));
        cDados.aplicaEstiloVisual(this);

        if (!IsPostBack)
        {
            carregaAnalise();
        }
    }

    private void carregaAnalise()
    {
        DataSet ds = cDados.getAnalisePerformanceObjeto(codigoMeta.ToString(), anoResultado.ToString(), mesResultado.ToString(), "MT");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];
            cDados.setInfoSistema("TipoEdicao", "E");
            mmAnalise.Text = dr["Analise"].ToString();
            mmRecomendacoes.Text = dr["Recomendacoes"].ToString();
            lblDataInclusao.Text = string.Format(@"Incluído por {0} em {1:dd/MM/yyyy HH:mm:ss}", dr["UsuarioInclusao"], dr["DataInclusao"]);

            if (dr["UsuarioAlteracao"].ToString() != "" && dr["DataUltimaAlteracao"].ToString() != "")
                lblDataAlteracao.Text = string.Format(@"Alterado por {0} em {1:dd/MM/yyyy HH:mm:ss}", dr["UsuarioAlteracao"], dr["DataUltimaAlteracao"]);
        }
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        bool retorno = true;

        if (cDados.getInfoSistema("TipoEdicao").ToString() == "I")
        {
            retorno = cDados.incluirAnalisePerformanceMeta(codigoIndicador.ToString(), codigoMeta.ToString()
                , anoResultado.ToString(), mesResultado.ToString(), mmAnalise.Text, mmRecomendacoes.Text, codigoUsuarioResponsavel.ToString(), "MT", "O");
        }
        else if (cDados.getInfoSistema("TipoEdicao").ToString() == "E")
        {
            retorno = cDados.atualizaAnalisePerformanceMeta(codigoIndicador.ToString(), codigoMeta.ToString()
                , anoResultado.ToString(), mesResultado.ToString(), mmAnalise.Text, mmRecomendacoes.Text, codigoUsuarioResponsavel.ToString(), "MT");
        }

        if (retorno)
        {
            callbackSalvar.JSProperties["cp_Status"] = "1";
        }
        else
        {
            callbackSalvar.JSProperties["cp_Status"] = "0";
        }
    }
}