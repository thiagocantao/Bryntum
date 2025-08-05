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
using DevExpress.Web.ASPxTreeList;
using System.IO;
using DevExpress.Web;

public partial class administracao_CadastroDocumentos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoTipoAssociacao;
    int IDObjetoAssociado;
    private string resolucaoCliente = "";
    private Int64 tamanhoMaximoArquivoUpload;

    protected void Page_Init(object sender, EventArgs e)
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        string IniciaisTipoAssociacao = (Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() != "") ? Request.QueryString["TA"].ToString() : "";
        IDObjetoAssociado = (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "") ? int.Parse(Request.QueryString["ID"].ToString()) : -1;

        // se os objetos da tela estiverem associados com outros objetos...
        if (IniciaisTipoAssociacao != "")
            codigoTipoAssociacao = cDados.getCodigoTipoAssociacao(IniciaisTipoAssociacao);

        hfGeral.Set("IniciaisTipoAssociacao", IniciaisTipoAssociacao);
        hfGeral.Set("IDObjetoAssociado", IDObjetoAssociado);

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));

        // esconde o cabecçalho das colunas
        tlAnexos.Settings.ShowColumnHeaders = false;

        // busca o tamanho máximo permitido para upload no parametro
        tamanhoMaximoArquivoUpload = 2; // inicialmente é 2 megas
        DataSet ds = cDados.getParametrosSistema("tamanhoMaximoArquivoAnexoEmMegaBytes");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["tamanhoMaximoArquivoAnexoEmMegaBytes"].ToString() != "")
            tamanhoMaximoArquivoUpload = int.Parse(ds.Tables[0].Rows[0]["tamanhoMaximoArquivoAnexoEmMegaBytes"].ToString());

        populaTreeListArquivos();
        defineAlturaTela(resolucaoCliente);

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        if (resolucaoCliente != "")
        {
            int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
            pnAnexos.Height = new Unit((alturaPrincipal - 185) + "px");
            pnCallback.Height = pnAnexos.Height;
        }
    }

    #region TREELIST

    private void populaTreeListArquivos()
    {
        DataSet ds = cDados.getAnexos(IDObjetoAssociado, codigoTipoAssociacao, codigoEntidadeUsuarioResponsavel);
        if ((cDados.DataSetOk(ds)))
        {
            tlAnexos.DataSource = ds.Tables[0];
            tlAnexos.DataBind();
        }
    }

    protected string GetIconUrl(TreeListDataCellTemplateContainer container)
    {
        //ASPxImage imgDownLoad = tlAnexos.FindDataCellTemplateControl(container.NodeKey, null, "imgDownload") as ASPxImage;
        ASPxButton btnDownLoad = tlAnexos.FindDataCellTemplateControl(container.NodeKey, null, "btnDownLoad") as ASPxButton;
        string nomeIcone = "pasta.gif";
        string IndicaPasta = container.GetValue("IndicaPasta").ToString();
        if (IndicaPasta == "N")
        {
            btnDownLoad.ClientInstanceName = "Btn_CodigoAnexo_" + container.GetValue("CodigoAnexo").ToString();
            if (btnDownLoad != null)
                btnDownLoad.Visible = true;
            nomeIcone = "arquivo.gif";
            string NomeArquivo = container.GetValue("Nome").ToString();
        }
        else
            if (btnDownLoad != null)
                btnDownLoad.Visible = false;

        return string.Format("~/imagens/anexo/{0}", nomeIcone);
    }

    protected string GetToolTip(TreeListDataCellTemplateContainer container)
    {
        string toolTip = "Incluído em: " + container.GetValue("DataInclusao").ToString() + Environment.NewLine + "por: " + container.GetValue("NomeUsuario").ToString() + Environment.NewLine + "==================================" + Environment.NewLine + container.GetValue("DescricaoAnexo").ToString();
        return toolTip;
    }


    #endregion

    
    protected void btnDownLoad_Click(object sender, EventArgs e)
    {
        string CodigoAnexo = (sender as ASPxButton).ClientInstanceName.Substring(16);
        cDados.download(int.Parse(CodigoAnexo),null, Page, Response, Request, true);

    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        hfGeral.Set("hfErro", "");
        if (e.Parameter == "excluir")
        {
            int codigoAnexo = int.Parse(hfGeral.Contains("CodigoAnexo") == true ? hfGeral.Get("CodigoAnexo").ToString() : "-1");

            int codigoPastaSup = int.Parse(hfGeral.Contains("CodigoPastaSuperior") == true ? hfGeral.Get("CodigoPastaSuperior").ToString() : "-1");

            string indicaPasta = hfGeral.Contains("IndicaPasta") == true ? hfGeral.Get("IndicaPasta").ToString() : "N";

            string erro = "";
            if (indicaPasta != "")
            {
                bool retorno = cDados.excluiAnexoProjeto(char.Parse(indicaPasta), codigoAnexo, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, IDObjetoAssociado, codigoTipoAssociacao, ref erro);
                if (!retorno)
                {
                    hfGeral.Set("hfErro", erro);
                }
                else
                {
                    hfGeral.Set("CodigoAnexo", -1);
                    hfGeral.Set("CodigoPastaSuperior", -1);
                    hfGeral.Set("IndicaPasta", "");
                }
            }
            populaTreeListArquivos();
        }
        if (e.Parameter == "Listar")
        {
            populaTreeListArquivos();
            defineAlturaTela(resolucaoCliente);
            
        }
    }
}
