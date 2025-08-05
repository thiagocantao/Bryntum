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

public partial class _VisaoAnalista2_Graficos_an_003 : System.Web.UI.Page
{
    dados cDados;

    public string alturaTabela = "";
    public string larguraTabela = "";
    int codigoUsuarioLogado, codigoEntidade;

    int diasParamCronograma = 10, diasParamFotos = 30, diasParamComentarios = 30;

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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
                
        //Função que gera o gráfico
        geraTabela();
    }

    private void defineLarguraTela()
    {
        int largura, altura;


        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        larguraTabela = ((largura - 30) / 3).ToString();
        ASPxRoundPanel1.Width = ((largura - 20) / 3);
        ASPxRoundPanel1.ContentHeight = (altura - 240) / 2;
        alturaTabela = ((altura - 250) / 2).ToString(); 

    }

    private void geraTabela()
    {
        //Data Set contendo a tabela com os dados a serem carregados no gráfico de PIZZA
        DataTable dt = new DataTable();

        if (cDados.getInfoSistema("DsOperacional") != null)
            dt = ((DataSet)cDados.getInfoSistema("DsOperacional")).Tables[2];

        if (cDados.DataTableOk(dt))
        {
            int cronogramas, fotos, comentarios;

            cronogramas = int.Parse(dt.Rows[0]["Total"].ToString());
            fotos = int.Parse(dt.Rows[1]["Total"].ToString());
            comentarios = int.Parse(dt.Rows[2]["Total"].ToString());

            constroiMensagemCronograma(cronogramas);
            constroiMensagemFotos(fotos);
            constroiMensagemComentarios(comentarios);
        }
    }

    private void constroiMensagemCronograma(int cronogramas)
    {
        switch (cronogramas)
        {
            case 0: lkCronogramas.Text = string.Format("Todos os cronogramas atualizado nos últimos {0} dias.", diasParamCronograma);
                break;
            case 1: lkCronogramas.Text = string.Format("<a href='#' onclick='abreListaObras(\"CR\");'>1</a> <a class='SF'>cronograma não atualizado nos últimos {0} dias.</a>", diasParamCronograma);
                break;
            default: lkCronogramas.Text = string.Format("<a href='#' onclick='abreListaObras(\"CR\");'>{0}</a> <a class='SF'>cronogramas não atualizados nos últimos {1} dias.</a>", cronogramas, diasParamCronograma);
                break;
        }
    }

    private void constroiMensagemFotos(int fotos)
    {
        switch (fotos)
        {
            case 0: lkFotos.Text = string.Format("Todos os cronogramas com fotos incluídas nos últimos {0} dias.", diasParamFotos);
                break;
            case 1: lkFotos.Text = string.Format("<a href='#' onclick='abreListaObras(\"FT\");'>1</a> <a class='SF'>obra sem fotos incluída nos últimos {0} dias.</a>", diasParamFotos);
                break;
            default: lkFotos.Text = string.Format("<a href='#' onclick='abreListaObras(\"FT\");'>{0}</a> <a class='SF'>obras sem fotos incluídas nos últimos {1} dias.</a>", fotos, diasParamFotos);
                break;
        }
    }

    private void constroiMensagemComentarios(int comentarios)
    {
        switch (comentarios)
        {
            case 0: lkComentarios.Text = string.Format("Todos os projetos comentados pela fiscalização nos últimos {0} dias.", diasParamComentarios);
                break;
            case 1: lkComentarios.Text = string.Format("<a href='#' onclick='abreListaObras(\"CM\");'>1</a> <a class='SF'>projeto sem comentários da fiscalização nos últimos {0} dias.</a>", diasParamComentarios);
                break;
            default: lkComentarios.Text = string.Format("<a href='#' onclick='abreListaObras(\"CM\");'>{0}</a> <a class='SF'>projetos sem comentários da fiscalização nos últimos {1} dias.</a>", comentarios, diasParamComentarios);
                break;
        }
    }
}
