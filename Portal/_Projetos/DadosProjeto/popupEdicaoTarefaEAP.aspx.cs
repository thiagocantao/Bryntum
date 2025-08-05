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

public partial class _Projetos_DadosProjeto_popupEdicaoTarefaEAP : System.Web.UI.Page
{
    dados cDados;

    private string IDEdicaoEAP;
    private string modoVisualizacao;
    private string code;
    private string elementName;
    private string IdTarefa;
    private int codigoEntidade;

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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        try
        {
            codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
            
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/popupEdicaoTarefaEAP.js""></script>"));
        this.TH(this.TS("popupEdicaoTarefaEAP"));
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (Request.QueryString["projectID"] != null && (Request.QueryString["projectID"] + "") != "")
            IDEdicaoEAP = Request.QueryString["projectID"].ToString();

        if (Request.QueryString["code"] != null && (Request.QueryString["code"] + "") != "")
            code = Request.QueryString["code"];

        if (Request.QueryString["elementName"] != null && (Request.QueryString["elementName"] + "") != "")
            elementName = Server.UrlDecode(Request.QueryString["elementName"].ToString());

        if (Request.QueryString["GUID"] != null && (Request.QueryString["GUID"] + "") != "")
            IdTarefa = Request.QueryString["GUID"];

        if (Request.QueryString["MO"] != null && (Request.QueryString["MO"] + "") != "")
            modoVisualizacao = Request.QueryString["MO"];

        if (!IsPostBack)
        {
            PreencheCamposTela();
            cDados.aplicaEstiloVisual(Page);


            string where = string.Format(@"and CodigoUsuario in(SELECT us.CodigoUsuario FROM {0}.{1}.Usuario AS us 
											                 INNER JOIN {0}.{1}.UsuarioUnidadeNegocio AS uun 
														             ON (uun.CodigoUsuario = us.CodigoUsuario)
								                                  WHERE Uun.CodigoUnidadeNegocio = {2}  AND us.DataExclusao IS NULL)", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade);
            hfGeral.Set("hfWheregetLov_NomeValor", where);
        }
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);
    }

    private void PreencheCamposTela()
    {
        string modo = modoVisualizacao == "V" ? "Visualização" : "Edição";
        habilitaComponentes(modoVisualizacao != "V");
        lblPacote.Text = elementName + " " + code + "  -  " + modo;
        
        // tenta primeiramente, encontrar a tarefa na tabela de edição de tarefas EAP, 
        // caso não encontre, pega os dados na tabela [TarefaCronogramaProjeto], caso ela exista nesta tabela.
        DataSet ds = cDados.getTarefaEdicaoEAP(IDEdicaoEAP, IdTarefa);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            dteInicio.Value = ds.Tables[0].Rows[0]["Inicio"];
            dteTermino.Value = ds.Tables[0].Rows[0]["Termino"];
            txtDuracao.Text = ds.Tables[0].Rows[0]["Duracao"].ToString();
            txtTrabalho.Text = ds.Tables[0].Rows[0]["Trabalho"].ToString();
            txtCusto.Text = ds.Tables[0].Rows[0]["Custo"].ToString();
            txtResponsavel.Text = ds.Tables[0].Rows[0]["NomeUsuario"].ToString();
            hfGeral.Set("lovCodigoResponsavel", ds.Tables[0].Rows[0]["CodigoUsuario"].ToString());
            txtDicionario.Text = ds.Tables[0].Rows[0]["Anotacoes"].ToString();
        }
        else
        {
            DataSet ds1 = cDados.getTarefaCronogramaProjetoEdicaoEAP(IDEdicaoEAP, IdTarefa);
            if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
            {
                dteInicio.Value = ds1.Tables[0].Rows[0]["Inicio"];
                dteTermino.Value = ds1.Tables[0].Rows[0]["Termino"];
                txtDuracao.Text = ds1.Tables[0].Rows[0]["Duracao"].ToString();
                txtTrabalho.Text = ds1.Tables[0].Rows[0]["Trabalho"].ToString();
                txtCusto.Text = ds1.Tables[0].Rows[0]["Custo"].ToString();
                txtResponsavel.Text = ds1.Tables[0].Rows[0]["NomeUsuario"].ToString();
                hfGeral.Set("lovCodigoResponsavel", ds1.Tables[0].Rows[0]["CodigoUsuario"].ToString());
                txtDicionario.Text = ds1.Tables[0].Rows[0]["Anotacoes"].ToString();
            }
        }
    }

    private void habilitaComponentes(bool sim)
    {
        dteInicio.ClientEnabled = sim;
        dteTermino.ClientEnabled = sim;
        txtDuracao.ClientEnabled = sim;
        txtTrabalho.ClientEnabled = sim;
        txtCusto.ClientEnabled = sim;
        txtResponsavel.ClientEnabled = sim;
        txtDicionario.ClientEnabled = sim;
        btnSalvar.ClientVisible = sim;
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "PesquisarResp")
        {
            Session["Pesquisa"] = txtResponsavel.Text;
            lblResponsavel.Text = "";
            pnCallback.JSProperties["cp_OperacaoOk"] = "NAO";
            hfGeral.Set("lovMostrarPopPup", "0"); // 0 indica que não precisa abrir popup de pesquisa

            string nomeUsuario = "";
            string codigoUsuario = "";

            btnSalvar.Enabled = true;

            DataSet ds = cDados.getLov_NomeValor("usuario", "CodigoUsuario", "NomeUsuario", txtResponsavel.Text, false,  hfGeral.Get("hfWheregetLov_NomeValor").ToString(), "NomeUsuario", out codigoUsuario, out nomeUsuario);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                // se encontrou um único registro
                if (nomeUsuario != "" && ds.Tables[0].Rows.Count == 1)
                {
                    txtResponsavel.Text = nomeUsuario;
                    hfGeral.Set("lovCodigoResponsavel", codigoUsuario);
                }
                else if (ds.Tables[0].Rows.Count > 1)//se encontrou varios registros, mostra popup
                {
                    //se tiver vazio o campo texto não é preciso abrir o popup e o usuário pode salvar
                    hfGeral.Set("lovMostrarPopPup", "1"); // 1 indica que precisa abrir popup de pesquisa
                    hfGeral.Set("lovCodigoResponsavel", "");
                }
            }
            else
            {
                if (txtResponsavel.Text != "")
                {
                    btnSalvar.Enabled = false;
                    //txtResponsavel.Text = "";
                    lblResponsavel.Text = "Usuário digitado não existe.";
                }
                else
                {
                    btnSalvar.Enabled = true;
                }
            }
        }

        if (e.Parameter == "salvar")
        {
            string msgErro = "";
            DataSet ds = cDados.getTarefaEdicaoEAP(IDEdicaoEAP, IdTarefa);
            bool retorno = false;

            string inicio = (dteInicio.Value == null) ? "NULL" : "convert(datetime,'" + dteInicio.Value.ToString() + "',103)";
            string termino = (dteTermino.Value == null) ? "NULL" : "convert(datetime,'" + dteTermino.Value.ToString() + "',103)";
            string codResponsavel = (hfGeral.Contains("lovCodigoResponsavel") && hfGeral.Get("lovCodigoResponsavel").ToString() != "") ? hfGeral.Get("lovCodigoResponsavel").ToString() : "NULL";
            pnCallback.JSProperties["cp_OperacaoOk"] = "NAO";
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                try
                {
                    retorno = cDados.atualizaTarefaEAP(IDEdicaoEAP, IdTarefa, inicio, termino, decimal.Parse(txtDuracao.Text), decimal.Parse(txtTrabalho.Text), decimal.Parse(txtCusto.Text), codResponsavel, txtDicionario.Text, ref msgErro);
                    pnCallback.JSProperties["cp_OperacaoOk"] = "SIM";
                }
                catch (Exception ex)
                {
                    cDados.alerta(Page, ex.Message);
                }
            }
            else
            {
                try
                {
                    retorno = cDados.incluiTarefaEAP(IDEdicaoEAP, IdTarefa, inicio, termino, decimal.Parse(txtDuracao.Text), decimal.Parse(txtTrabalho.Text), decimal.Parse(txtCusto.Text), codResponsavel, txtDicionario.Text, ref msgErro);
                    pnCallback.JSProperties["cp_OperacaoOk"] = "SIM";
                }
                catch (Exception ex)
                {
                    cDados.alerta(Page, ex.Message);
                }
            }
        }
        if (e.Parameter == "habilitaSalvar")
        {
            pnCallback.JSProperties["cp_OperacaoOk"] = "NAO";
            btnSalvar.Enabled = true;
        }
    }
}

