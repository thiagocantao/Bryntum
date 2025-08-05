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
using DevExpress.Web;
using System.Drawing;

public partial class _Projetos_DadosProjeto_ListaTarefasAlertas : System.Web.UI.Page
{
    dados cDados;

    private int codigoAlerta = -1, codigoProjeto = -1;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;


    public bool podeIncluir = true;
    bool podeEditar = true;
    bool podeExcluir = true;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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

        headerOnTela();
        
        if (Request.QueryString["CA"] != null)
            codigoAlerta = int.Parse(Request.QueryString["CA"].ToString());

        if (Request.QueryString["CP"] != null)
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

        gvDados.JSProperties["cp_CodigoProjeto"] = codigoProjeto;
        gvDados.JSProperties["cp_CodigoAlerta"] = codigoAlerta;

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            //cDados.VerificaAcessoTela(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_CnsTdl");
        }

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(this);
        }
        gvDados.Settings.VerticalScrollableHeight = 340;

        carregaGvDados();
    }

    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getTarefasAlertasCronograma(codigoAlerta, codigoProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        try
        {
            if (e.ButtonID == "btnExcluirCustom")
            {
                if (podeExcluir)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }

            if (e.ButtonID == "btnEditarCustom")
            {
                if (podeEditar)
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }

            }
        }
        catch (Exception)
        {
        }
    }

    #endregion

    #region VARIOS

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ListaTarefasAlerta.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ListaTarefasAlerta"));
        Header.Controls.Add(cDados.getLiteral(@"<title>TO DO List</title>"));
    }   

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        return codigoDado;
    }        
   
    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgErro = "";

        cDados.excluiAlertaCronograma(int.Parse(chave), codigoUsuarioResponsavel, ref msgErro);
        carregaGvDados();
        return msgErro;
    }

    #endregion

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        gvDados.JSProperties["cp_MSG"] = "";

        if (e.Parameters.ToString() == "X")
        {
            string codigoAlertaTarefa = getChavePrimaria();

            string msgErro = "";

            bool result = cDados.excluiTarefaAlerta(int.Parse(codigoAlertaTarefa), ref msgErro);

            if (result)
                msgErro = "Associação excluída com sucesso!";

            gvDados.JSProperties["cp_MSG"] = msgErro;

            carregaGvDados();
        }
    }
}

