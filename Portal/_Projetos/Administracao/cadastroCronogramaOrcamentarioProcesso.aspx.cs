using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Projetos_Administracao_cadastroCronogramaOrcamentarioProcesso : System.Web.UI.Page
{
    dados cDados;
    int codigoProjeto = -1;
    int codigoAtividade = -1;
    int codigoCronograma = -1;
    int codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel;
    public bool podeIncluir = false;

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

        cDados.aplicaEstiloVisual(Page);

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "")
        {
            codigoAtividade = int.Parse(Request.QueryString["CA"].ToString());
        }

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["CO"] != null && Request.QueryString["CO"].ToString() != "")
            {
                hfCodigoAcao.Set("CodigoCronograma", Request.QueryString["CO"].ToString());
            }
            else
            {
                hfCodigoAcao.Set("CodigoCronograma", "-1");
            }
        }

        codigoCronograma = int.Parse(hfCodigoAcao.Get("CodigoCronograma").ToString());

        carregaComboContas();

        callbackSalvar.JSProperties["cp_Msg"] = "";
        
        carregaCampos();
    }

    private void carregaCampos()
    {
         DataSet dsAcao = cDados.getAtividadeAcaoIniciativaProcesso(codigoAtividade, "");

         if (cDados.DataSetOk(dsAcao) && cDados.DataTableOk(dsAcao.Tables[0]))
         {
             DataRow dr = dsAcao.Tables[0].Rows[0];

             txtNomeAcao.Text = dr["NomeAcao"].ToString();
             txtNomeAtividade.Text = dr["Descricao"].ToString();
         }

         if (!IsPostBack)
         {
             callbackSalvar.JSProperties["cp_Retorno"] = "N";

             if (codigoCronograma != -1)
             {
                 DataSet ds = cDados.getContaAtividadeProcesso(codigoCronograma, " AND co.CodigoAcao = " + codigoAtividade);

                 if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                 {
                     DataRow dr = ds.Tables[0].Rows[0];

                     ddlConta.Value = dr["SeqPlanoContas"].ToString();
                     txtQuantidade.Text = dr["Quantidade"].ToString();
                     txtValorUnitario.Text = dr["ValorUnitario"].ToString();
                     txtValorTotal.Text = dr["ValorTotal"].ToString();
                     mmObjeto.Text = dr["MemoriaCalculo"].ToString();
                     txtPlan01.Text = dr["Plan01"].ToString();
                     txtPlan02.Text = dr["Plan02"].ToString();
                     txtPlan03.Text = dr["Plan03"].ToString();
                     txtPlan04.Text = dr["Plan04"].ToString();
                     txtPlan05.Text = dr["Plan05"].ToString();
                     txtPlan06.Text = dr["Plan06"].ToString();
                     txtPlan07.Text = dr["Plan07"].ToString();
                     txtPlan08.Text = dr["Plan08"].ToString();
                     txtPlan09.Text = dr["Plan09"].ToString();
                     txtPlan10.Text = dr["Plan10"].ToString();
                     txtPlan11.Text = dr["Plan11"].ToString();
                     txtPlan12.Text = dr["Plan12"].ToString();
                 }
             }
         }
    }

    private void carregaComboContas()
    {
        DataSet ds = cDados.getPlanoContasAnoAtualProcesso(codigoCronograma, codigoAtividade, "");

        if (cDados.DataSetOk(ds))
        {
            ddlConta.DataSource = ds;
            //ddlConta.TextField = "CONTA_DES";
            ddlConta.ValueField = "SeqPlanoContas";
            ddlConta.DataBind();
        }
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        int codigoConta = int.Parse(ddlConta.Value.ToString());
        string quantidade = txtQuantidade.Value.ToString().Replace(",", ".");
        string valorUnitario = txtValorUnitario.Value.ToString().Replace(",", ".");
        string valorTotal = txtValorTotal.Value.ToString().Replace(",", ".");
        string memoriaCalculo = mmObjeto.Text;
        string plan01 = txtPlan01.Value != null && txtPlan01.Text != "" ? txtPlan01.Value.ToString().Replace(",", ".") : "0";
        string plan02 = txtPlan02.Value != null && txtPlan02.Text != "" ? txtPlan02.Value.ToString().Replace(",", ".") : "0";
        string plan03 = txtPlan03.Value != null && txtPlan03.Text != "" ? txtPlan03.Value.ToString().Replace(",", ".") : "0";
        string plan04 = txtPlan04.Value != null && txtPlan04.Text != "" ? txtPlan04.Value.ToString().Replace(",", ".") : "0";
        string plan05 = txtPlan05.Value != null && txtPlan05.Text != "" ? txtPlan05.Value.ToString().Replace(",", ".") : "0";
        string plan06 = txtPlan06.Value != null && txtPlan06.Text != "" ? txtPlan06.Value.ToString().Replace(",", ".") : "0";
        string plan07 = txtPlan07.Value != null && txtPlan07.Text != "" ? txtPlan07.Value.ToString().Replace(",", ".") : "0";
        string plan08 = txtPlan08.Value != null && txtPlan08.Text != "" ? txtPlan08.Value.ToString().Replace(",", ".") : "0";
        string plan09 = txtPlan09.Value != null && txtPlan09.Text != "" ? txtPlan09.Value.ToString().Replace(",", ".") : "0";
        string plan10 = txtPlan10.Value != null && txtPlan10.Text != "" ? txtPlan10.Value.ToString().Replace(",", ".") : "0";
        string plan11 = txtPlan11.Value != null && txtPlan11.Text != "" ? txtPlan11.Value.ToString().Replace(",", ".") : "0";
        string plan12 = txtPlan12.Value != null && txtPlan12.Text != "" ? txtPlan12.Value.ToString().Replace(",", ".") : "0";
        bool retorno = false;

        if (codigoCronograma == -1)
        {
            retorno = cDados.incluiContaAtividadeProcesso(codigoProjeto, codigoAtividade, codigoConta, quantidade, valorUnitario, valorTotal, memoriaCalculo, plan01, plan02, plan03, plan04, plan05, plan06, plan07, plan08, plan09, plan10, plan11, plan12);
            
            if (retorno)
            {
                callbackSalvar.JSProperties["cp_Msg"] = "Conta Incluída com Sucesso!";
                callbackSalvar.JSProperties["cp_Retorno"] = "S";
            }
            else
            {
                callbackSalvar.JSProperties["cp_Msg"] = "Erro ao Incluir a Conta!";
            }
        }
        else
        {
            retorno = cDados.atualizaContaAtividadeProcesso(codigoCronograma, codigoAtividade, codigoConta, quantidade, valorUnitario, valorTotal, memoriaCalculo, plan01, plan02, plan03, plan04, plan05, plan06, plan07, plan08, plan09, plan10, plan11, plan12);

            if (retorno)
            {
                callbackSalvar.JSProperties["cp_Msg"] = "Conta Alterada com Sucesso!";
                callbackSalvar.JSProperties["cp_Retorno"] = "S";
            }
            else
            {
                callbackSalvar.JSProperties["cp_Msg"] = "Erro ao Alterar a Conta!";
            }
        }


        callbackSalvar.JSProperties["cp_CodigoCronograma"] = codigoConta;
    }
}