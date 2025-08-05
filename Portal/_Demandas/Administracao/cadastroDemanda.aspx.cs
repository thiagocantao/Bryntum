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
using System.IO;

public partial class _Demandas_Administracao_cadastroDemanda : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuario;
    private int codigoEntidade;
    private bool permissaoAbrirDemandaOutroUsuario = false;
    private int codigoDemandaAberta = -1;

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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(Page);
                    
        carregaComboAssuntos();
        carregaComboTiposDemandas();
        carregaComboCanal();
        carregaComboDemandante();

        ddlDemandante.ClientEnabled = permissaoAbrirDemandaOutroUsuario;

        if (Request.QueryString["CD"] != null && Request.QueryString["CD"].ToString() != "")
            codigoDemandaAberta = int.Parse(Request.QueryString["CD"].ToString());

        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
            desabilitaComponentes();   

        if (Request.QueryString["MB"] != null && Request.QueryString["MB"].ToString() == "N")
        {
            btnSalvar.ClientVisible = false;
            btnCancelar.ClientVisible = false;
        }

        if (codigoDemandaAberta != -1)
            montaCamposDemanda();
    }

    private void montaCamposDemanda()
    {
        DataSet ds = cDados.getDemandasEntidade(codigoEntidade, " AND d.CodigoDemanda = " + codigoDemandaAberta);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow dr = ds.Tables[0].Rows[0];
            ddlAssunto.Value = dr["CodigoAssuntoDemanda"].ToString();
            txtTitulo.Text = dr["Titulo"].ToString();
            ddlDemandante.Value = int.Parse(dr["CodigoUsuarioDemandante"].ToString());
            ddlTipo.Value = dr["CodigoTipoDemanda"].ToString();
            ddlUrgencia.Value = dr["UrgenciaDemanda"].ToString();
            ddlCanal.Value = dr["CodigoCanalAberturaDemanda"].ToString();
            txtDetalhes.Text = dr["DescricaoDetalhadaDemanda"].ToString();
        }
    }

    private void desabilitaComponentes()
    {
        ddlAssunto.ClientEnabled = false;
        txtTitulo.ClientEnabled = false;
        ddlDemandante.ClientEnabled = false;
        ddlTipo.ClientEnabled = false;
        ddlUrgencia.ClientEnabled = false;
        ddlCanal.ClientEnabled = false;
        txtDetalhes.ClientEnabled = false;
        fuAnexo.ClientVisible = false;
        lblAnexo.ClientVisible = false;
        lblMsgAnexo.ClientVisible = false;
        btnSalvar.ClientVisible = false;
    }

    private void carregaComboAssuntos()
    {
        DataSet ds = cDados.getAssuntosDemandas(codigoEntidade, "");

        ddlAssunto.ValueField = "CodigoAssuntoDemanda";
        ddlAssunto.TextField = "DescricaoAssuntoDemanda";
        ddlAssunto.DataSource = ds;
        ddlAssunto.DataBind();
    }

    private void carregaComboTiposDemandas()
    {
        DataSet ds = cDados.getTiposDemandas(codigoEntidade, "");

        ddlTipo.ValueField = "CodigoTipoDemanda";
        ddlTipo.TextField = "DescricaoTipoDemanda";
        ddlTipo.DataSource = ds;
        ddlTipo.DataBind();
    }

    private void carregaComboCanal()
    {
        DataSet ds = cDados.getTiposCanaisAberturaDemandas("");

        ddlCanal.ValueField = "CodigoCanalAberturaDemanda";
        ddlCanal.TextField = "DescricaoCanalAberturaDemanda";
        ddlCanal.DataSource = ds;
        ddlCanal.DataBind();
    }

    private void carregaComboDemandante()
    {
        DataSet ds = cDados.getUsuariosAtivosEntidade(codigoEntidade, "");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlDemandante.ValueField = "CodigoUsuario";
            ddlDemandante.TextField = "NomeUsuario";

            ddlDemandante.DataSource = ds.Tables[0];
            ddlDemandante.DataBind();

            if (!IsPostBack)
                ddlDemandante.Value = codigoUsuario;
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        int codigoDemanda = 0;
        string msgErro = "";

        bool retorno = cDados.incluiDemandaEntidade(codigoEntidade, codigoUsuario, int.Parse(ddlDemandante.Value.ToString()), txtTitulo.Text
                                                    , txtDetalhes.Text, int.Parse(ddlAssunto.Value.ToString()), ddlUrgencia.Value.ToString()
                                                    , int.Parse(ddlTipo.Value.ToString()), int.Parse(ddlCanal.Value.ToString()), ref codigoDemanda, ref msgErro);

        if (retorno)
        {
            incluiNovoAnexo(codigoDemanda);

            string script = string.Format(@"<script type='text/javascript' language='Javascript'>
                                                if(window.parent.executaFuncaoFluxo)
                                                    window.parent.executaFuncaoFluxo('{0}', '{1}');
                                                else
                                                    window.top.fechaModal();
                                            </script>", codigoDemanda, hfGeral.Get("CodigoAcaoWf").ToString());

            ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
        }
    }

    private string incluiNovoAnexo(int codigoDemanda)
    {
        try
        {
            int codigoTipoAssociacao = cDados.getCodigoTipoAssociacao("DM");
            
            // VERIFICA SE O USUÁRIO SELECIONOU ALGUM ARQUIVO
            if (fuAnexo.UploadedFiles.Length > 0)
            {
                string nomeNovoAnexo = Path.GetFileName(fuAnexo.UploadedFiles[fuAnexo.UploadedFiles.Length - 1].FileName);
                string descricaoNovoAnexo = nomeNovoAnexo;
                long tamanhoImagem = fuAnexo.UploadedFiles[fuAnexo.UploadedFiles.Length - 1].ContentLength;

                // VERIFICA A EXTENSÃO DO ARQUIVO.
                string extensao = Path.GetExtension(nomeNovoAnexo).ToLower();
                if (extensao == ".exe" || extensao == ".com" || extensao == ".dll")
                    return "O tipo do arquivo não pode ser anexado.";

                // BLOQUEIA A TRANSFERÊNCIA DE ARQUIVOS MAIOR QUE O LIMITE PERMITIDO - VER PARAMETRO
                if (tamanhoImagem > 5 * (1024 * 1024))
                    return string.Format("Limite máximo para arquivo é de {0} MB", 5);

                //RECEBE O ARQUIVO COLOCANDO-O NA MEMÓRIA
                Stream imagem = fuAnexo.UploadedFiles[fuAnexo.UploadedFiles.Length - 1].FileContent;
                
                byte[] imagemBinario = new byte[tamanhoImagem];
                int n = imagem.Read(imagemBinario, 0, (int)tamanhoImagem);

                // insere o arquivo no banco de dados
                string mensagem = cDados.incluirAnexo(descricaoNovoAnexo, codigoUsuario.ToString(), nomeNovoAnexo, codigoEntidade.ToString(), null, 'N', 'N', codigoTipoAssociacao,"NULL", "", codigoDemanda.ToString(), imagemBinario, "N");
                
                if (mensagem != "")
                    return mensagem;
            }
            
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
