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

public partial class espacoTrabalho_DetalhesTS : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private string idAtribucao = "";
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;


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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CA"] != null)
            idAtribucao = Request.QueryString["CA"].ToString();

        cDados.aplicaEstiloVisual(Page);

        if (!IsPostBack && !IsCallback)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);

            if (!IsCallback)
            {
                pcDados.ActiveTabIndex = 0;
                carregaDados(idAtribucao);
            }
        }

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/espacoTrabalho_DetalhesTS.js""></script>"));
        this.TH(this.TS("espacoTrabalho_DetalhesTS"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 148);
        //mmComentariosAprovador.Height = altura;
    }

    private void carregaDados(string codigoAtribucao)
    {
        if (codigoAtribucao != "")
        {
            DataSet ds = cDados.getDetalhesTSMSProject(codigoAtribucao);

            if (cDados.DataSetOk(ds))
            {
                if (cDados.DataTableOk(ds.Tables[0]))
                {
                    //string.Format("{0:dd/MM/yyyy}", txtDe.Date);
                    //Cargar Datos Aba Detalhes
                    txtProjeto.Text = ds.Tables[0].Rows[0]["NomeProjeto"].ToString();
                    txtTarefa.Text = ds.Tables[0].Rows[0]["NomeTarefa"].ToString();
                    txtTarefaSuperior.Text = ds.Tables[0].Rows[0]["TarefaSuperior"].ToString();
                    txtInicioPrevisto.Text = ds.Tables[0].Rows[0]["InicioPrevisto"] + "" == "" ? "" : string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ds.Tables[0].Rows[0]["InicioPrevisto"].ToString()));
                    txtTerminoPrevisto.Text = ds.Tables[0].Rows[0]["TerminoPrevisto"] + "" == "" ? "" : string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ds.Tables[0].Rows[0]["TerminoPrevisto"].ToString()));
                    txtTabalhoPrevisto.Text = ds.Tables[0].Rows[0]["TrabalhoPrevisto"].ToString();

                    txtInicio.Text = ds.Tables[0].Rows[0]["Inicio"] + "" == "" ? "" : string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ds.Tables[0].Rows[0]["Inicio"].ToString()));
                    txtTermino.Text = ds.Tables[0].Rows[0]["Termino"] + "" == "" ? "" : string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ds.Tables[0].Rows[0]["Termino"].ToString()));
                    txtTrabalho.Text = ds.Tables[0].Rows[0]["Trabalho"].ToString();
                    mmAnotacoes.Html = ds.Tables[0].Rows[0]["Anotacoes"].ToString(); //Anotacoes"].ToString();

                    if (ds.Tables[0].Rows[0]["InicioReal"].ToString() != "")
                        ddlInicioReal.Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["InicioReal"].ToString());
                    if (ds.Tables[0].Rows[0]["TerminoReal"].ToString() != "")
                        ddlTerminoReal.Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["TerminoReal"].ToString());

                    txtTrabalhoReal.Text = ds.Tables[0].Rows[0]["TrabalhoReal"].ToString() == "" ? "0" : string.Format("{0:n0}", float.Parse(ds.Tables[0].Rows[0]["TrabalhoReal"].ToString()));
                    txtTrabalhoRestante.Text = ds.Tables[0].Rows[0]["TrabalhoRestante"].ToString() == "" ? "0" : string.Format("{0:n0}", float.Parse(ds.Tables[0].Rows[0]["TrabalhoRestante"].ToString()));
                    txtPorcentaje.Text = ds.Tables[0].Rows[0]["PercentualConcluido"].ToString();
                }
            }
        }
    }      
}
