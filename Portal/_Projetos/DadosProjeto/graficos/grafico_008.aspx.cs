using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class grafico_008 : System.Web.UI.Page
{
    dados cDados;

    int codigoProjeto = 0;
    double diasAtualizacao = -1;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.aplicaEstiloVisual(this);
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        DataSet dsParametro = cDados.getParametrosSistema("diasAtualizacaoProjeto");

        if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
        {
            if (dsParametro.Tables[0].Rows[0]["diasAtualizacaoProjeto"] + "" != "")
            {
                diasAtualizacao = double.Parse(dsParametro.Tables[0].Rows[0]["diasAtualizacaoProjeto"] + "");
            }
        }

        defineTamanhoObjetos();

        getPendencias();

        cDados.defineAlturaFrames(this, 65);
        cDados.aplicaEstiloVisual(this);
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));

        int larguraPaineis = ((largura) / 2 - 105);
    }

    private void getPendencias()
    {
        string definicaoToDoList = "To Do List";

        DataSet dsParametros = cDados.getParametrosSistema("labelToDoList");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelToDoList"] + "" != "")
            definicaoToDoList = dsParametros.Tables[0].Rows[0]["labelToDoList"] + "";

        DataSet dsDados = cDados.getPendenciasProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataTable dt = dsDados.Tables[0];

            int cronograma = 0, toDoList = 0, mensagens = 0;

            cronograma = int.Parse(dt.Rows[0]["AtrasosCronograma"].ToString());
            toDoList = int.Parse(dt.Rows[0]["AtrasosToDoList"].ToString());
            mensagens = int.Parse(dt.Rows[0]["MensagensNaoRespondidas"].ToString());


            lblAtualizacao.Text = dt.Rows[0]["TerminoPrimeiraLB"] + "" == "" ? "Término 1ª LB: --/--/----" : "Término 1ª LB: " + dt.Rows[0]["TerminoPrimeiraLB"].ToString();

            if (dt.Rows[0]["DataUltimaAtualizacao"] + "" != "" && diasAtualizacao != -1)
            {
                DateTime dataUltimaAtualizacao = DateTime.Parse(dt.Rows[0]["DataUltimaAtualizacao"] + "");

                //lblAtualizacao.ForeColor = (dataUltimaAtualizacao.AddDays(diasAtualizacao) < DateTime.Now) ? Color.Red : Color.Black;
            }

            hlCronograma.Text = "Atrasos no Cronograma (" + cronograma + ")";

            hlToDoList.Text = "Atrasos em " + definicaoToDoList + " (" + toDoList + ")";

            hlMensagens.Text = "";//"Mensagens Não Respondidas (" + mensagens + ")";


            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsCrn"))
            {
                hlCronograma.NavigateUrl = "../Cronograma_gantt.aspx?Atrasadas=S&IDProjeto=" + codigoProjeto;
                DataSet ds = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "linkCronograma");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    hlCronograma.NavigateUrl = VirtualPathUtility.ToAbsolute("~/") + ds.Tables[0].Rows[0]["linkCronograma"].ToString() + "?Atrasadas=S&IDProjeto=" + codigoProjeto;
                }
            }

            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsTdl"))
                hlToDoList.NavigateUrl = "../TarefasToDoList.aspx?Estagio=Atrasada&IDProjeto=" + codigoProjeto;

            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsMsg"))
                hlMensagens.NavigateUrl = "../editaMensagens.aspx?RESP=S&Status=NR&IDProjeto=" + codigoProjeto;


            if (cronograma > 0)
                hlCronograma.ForeColor = Color.Red;
            if (toDoList > 0)
                hlToDoList.ForeColor = Color.Red;
            if (mensagens > 0)
                hlMensagens.ForeColor = Color.Red;
        }
        else
        {
            hlCronograma.Text = "Atrasos no Cronograma (0)";

            hlToDoList.Text = "Atrasos em " + definicaoToDoList + " (0)";

            hlMensagens.Text = "Mensagens Não Respondidas (0)";
        }
    }
}
