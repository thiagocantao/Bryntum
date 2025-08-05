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

public partial class _Projetos_DadosProjeto_TarefasToDoList : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string resolucaoCliente = "";

    public bool podeIncluir = true;
    bool podeEditar = true;
    bool podeExcluir = true;

    public string alturaDivPopup = "200px";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        

        headerOnTela();

        if (Request.QueryString["CP"] != null)
            idProjeto = int.Parse(Request.QueryString["CP"].ToString());

        gvDados.JSProperties["cp_CodigoProjeto"] = idProjeto.ToString();

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTela(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_AleCrn");
        }

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
           
        }
        cDados.aplicaEstiloVisual(Page);
        carregaGvDados();
    }

    #region GRID

    private void carregaGvDados()
    {
        DataSet ds = cDados.getAlertasCronograma(idProjeto, "");

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
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/AlertasCronograma.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "AlertasCronograma"));
        Header.Controls.Add(cDados.getLiteral(@"<title>TO DO List</title>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x'))) - 200;
        int altura = (alturaPrincipal - 190);

        //gvDados.Settings.VerticalScrollableHeight = altura - 150;
        gvDados.Width = new Unit("100%");
        float fator = 1;
        if (alturaPrincipal <= 768)
        {
            fator = 1;
        }
        else if (alturaPrincipal <= 800)
        {
            fator = 1.15f;
        }
        else if (alturaPrincipal <= 960)
        {
            fator = 1.25f;
        }
        else
        {
            fator = 1.35f;
        }
        alturaDivPopup = string.Format("{0}px", (int)((alturaPrincipal / 100.00) * fator * 30.00));

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

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_MSG"] = "";
        pnCallback.JSProperties["cp_Erro"] = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                pnCallback.JSProperties["cp_MSG"] = Resources.traducao.AlertasCronograma_alerta_inclu_do_com_sucesso_;
            }
            else
            {
                pnCallback.JSProperties["cp_Erro"] = mensagemErro_Persistencia;
            }

        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                pnCallback.JSProperties["cp_MSG"] = Resources.traducao.AlertasCronograma_alerta_alterado_com_sucesso_;
            }
            else
            {
                pnCallback.JSProperties["cp_Erro"] = mensagemErro_Persistencia;
            }

        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                pnCallback.JSProperties["cp_MSG"] = Resources.traducao.AlertasCronograma_alerta_exclu_do_com_sucesso_;

            }
            else
            {
                pnCallback.JSProperties["cp_Erro"] = mensagemErro_Persistencia;
            }
        }

        
    }

    private string persisteInclusaoRegistro() // Método responsável pela Inclusao do registro
    {
        string msgErro = "";

        string nomeRegra = txtNomeRegra.Text;
        string diasAntecedenciaInicio1 = txt001.Text == "" ? "NULL" : txt001.Text;
        string diasIntervaloRecorrenciaInicio2 = txt002.Text == "" ? "NULL" : txt002.Text;
        string diasAntecedenciaInicio2 = txt003.Text == "" ? "NULL" : txt003.Text;
        string diasIntervaloRecorrenciaInicio3 = txt004.Text == "" ? "NULL" : txt004.Text;
        string diasIntervaloRecorrenciaTermino = txt005.Text == "" ? "NULL" : txt005.Text;
        string diasIntervaloRecorrenciaAtraso = txt006.Text == "" ? "NULL" : txt006.Text;
        string mensagemAlertaInicio1 = txtDescricao001.Text;
        string mensagemAlertaInicio2 = txtDescricao002.Text;
        string mensagemAlertaInicio3 = txtDescricao003.Text;
        string mensagemAlertaTermino = txtDescricao004.Text;
        string mensagemAlertaAtraso = txtDescricao005.Text;
        string indicaAlertaInicio1 = ck001.Checked ? "S" : "N";
        string indicaAlertaInicio2 = ck002.Checked ? "S" : "N";
        string indicaAlertaInicio3 = ck003.Checked ? "S" : "N";
        string indicaAlertaTermino = ck004.Checked ? "S" : "N";
        string indicaAlertaAtraso = ck005.Checked ? "S" : "N";

        cDados.incluiAlertaCronograma(idProjeto, codigoUsuarioResponsavel, nomeRegra, diasAntecedenciaInicio1, diasIntervaloRecorrenciaInicio2
            ,diasAntecedenciaInicio2, diasIntervaloRecorrenciaInicio3, diasIntervaloRecorrenciaTermino, diasIntervaloRecorrenciaAtraso
            ,mensagemAlertaInicio1, mensagemAlertaInicio2, mensagemAlertaInicio3, mensagemAlertaTermino, mensagemAlertaAtraso
            ,indicaAlertaInicio1, indicaAlertaInicio2, indicaAlertaInicio3, indicaAlertaTermino, indicaAlertaAtraso, ref msgErro);
        
        carregaGvDados();
        return msgErro;
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgErro = "";

        string nomeRegra = txtNomeRegra.Text;
        string diasAntecedenciaInicio1 = txt001.Text == "" ? "NULL" : txt001.Text;
        string diasIntervaloRecorrenciaInicio2 = txt002.Text == "" ? "NULL" : txt002.Text;
        string diasAntecedenciaInicio2 = txt003.Text == "" ? "NULL" : txt003.Text;
        string diasIntervaloRecorrenciaInicio3 = txt004.Text == "" ? "NULL" : txt004.Text;
        string diasIntervaloRecorrenciaTermino = txt005.Text == "" ? "NULL" : txt005.Text;
        string diasIntervaloRecorrenciaAtraso = txt006.Text == "" ? "NULL" : txt006.Text;
        string mensagemAlertaInicio1 = txtDescricao001.Text;
        string mensagemAlertaInicio2 = txtDescricao002.Text;
        string mensagemAlertaInicio3 = txtDescricao003.Text;
        string mensagemAlertaTermino = txtDescricao004.Text;
        string mensagemAlertaAtraso = txtDescricao005.Text;
        string indicaAlertaInicio1 = ck001.Checked ? "S" : "N";
        string indicaAlertaInicio2 = ck002.Checked ? "S" : "N";
        string indicaAlertaInicio3 = ck003.Checked ? "S" : "N";
        string indicaAlertaTermino = ck004.Checked ? "S" : "N";
        string indicaAlertaAtraso = ck005.Checked ? "S" : "N";

        cDados.atualizaAlertaCronograma(int.Parse(chave), codigoUsuarioResponsavel, nomeRegra, diasAntecedenciaInicio1, diasIntervaloRecorrenciaInicio2
            , diasAntecedenciaInicio2, diasIntervaloRecorrenciaInicio3, diasIntervaloRecorrenciaTermino, diasIntervaloRecorrenciaAtraso
            , mensagemAlertaInicio1, mensagemAlertaInicio2, mensagemAlertaInicio3, mensagemAlertaTermino, mensagemAlertaAtraso
            , indicaAlertaInicio1, indicaAlertaInicio2, indicaAlertaInicio3, indicaAlertaTermino, indicaAlertaAtraso, ref msgErro);

        carregaGvDados();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
        gvDados.ClientVisible = false;
        return msgErro;
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

}

