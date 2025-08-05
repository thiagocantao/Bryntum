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

public partial class grafico_016 : System.Web.UI.Page
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

        cDados.defineAlturaFrames(this, 55);
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

            int cronograma = 0, toDoList = 0, entregas = 0, produtosAtrasados = 0;

            cronograma = int.Parse(dt.Rows[0]["AtrasosCronograma"].ToString());
            toDoList = int.Parse(dt.Rows[0]["AtrasosToDoList"].ToString());
            entregas = int.Parse(dt.Rows[0]["EntregasAtrasadas"].ToString());
            produtosAtrasados = int.Parse(dt.Rows[0]["ProdutosAtrasados"].ToString());


            hlProdutosAtrasados.Text = dt.Rows[0]["ProdutosAtrasados"] + "" == "" ? "Produtos Atrasados (0)" : String.Format(@"Produtos Atrasados ({0}) ", produtosAtrasados);

            //if (dt.Rows[0]["ProdutosAtrasados"] + "" != "" && diasAtualizacao != -1)
            //{
            //    DateTime dataUltimaAtualizacao = DateTime.Parse(dt.Rows[0]["ProdutosAtrasados"] + "");

            //    hlProdutosAtrasados.ForeColor = (dataUltimaAtualizacao.AddDays(diasAtualizacao) < DateTime.Now) ? Color.Red : Color.Black;
            //}

            hlCronograma.Text = "Atrasos no Cronograma (" + cronograma + ")";

            hlToDoList.Text = "Atrasos em " + definicaoToDoList + " (" + toDoList + ")";

           
            DataSet dsParametro = cDados.getParametrosSistema("lblEntregasAtrasadas");
            if(cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
            {
                hlEntregas.Text = dsParametro.Tables[0].Rows[0]["lblEntregasAtrasadas"].ToString() + " (" + entregas + ")";
            }
            else
            {
                hlEntregas.Text =  "Entregas atrasadas (" + entregas + ")";
            }

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
            {
                DataSet ds = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "linkCronograma");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    hlCronograma.NavigateUrl = VirtualPathUtility.ToAbsolute("~/") + ds.Tables[0].Rows[0]["linkCronograma"].ToString() + "?ApenasMarcos=S&Atrasadas=S&IDProjeto=" + codigoProjeto;
                }
            }
            
            //if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsMsg"))
            hlProdutosAtrasados.NavigateUrl = "../produtosTAI.aspx?Atrasados=S&IDProjeto=" + codigoProjeto;

            if (cronograma > 0)
                hlCronograma.ForeColor = Color.Red;
            if (toDoList > 0)
                hlToDoList.ForeColor = Color.Red;
            if (entregas > 0)
                hlEntregas.ForeColor = Color.Red;
            if (produtosAtrasados > 0)
                hlProdutosAtrasados.ForeColor = Color.Red;

        }
        else
        {
            hlCronograma.Text = "Atrasos no Cronograma (0)";

            hlToDoList.Text = "Atrasos em " + definicaoToDoList + " (0)";

            DataSet dsParametro = cDados.getParametrosSistema("lblEntregasAtrasadas");
            if(cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
            {
                hlEntregas.Text = dsParametro.Tables[0].Rows[0]["lblEntregasAtrasadas"].ToString() + " (0)";
            }
            else
            {
                hlEntregas.Text = "Entregas Atrasadas (0)";
            }
            
        }
    }
}
