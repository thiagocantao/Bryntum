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
    private int codigoTDL = -1;
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
        this.TH(this.TS("DetalhesTDL", "CDIS", "kanban"));

        if (Request.QueryString["CTDL"] != null && Request.QueryString["CTDL"].ToString() != "")
        {
            codigoTDL = int.Parse(Request.QueryString["CTDL"].ToString());
            codigoTDL = codigoTDL < 0 ? (codigoTDL * -1) : codigoTDL;
        }

        cDados.aplicaEstiloVisual(Page);

        populaCombo();

        if (!IsPostBack && !IsCallback)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);

            if (!IsCallback)
            {
                carregaDados();
            }
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 148);
        //mmComentariosAprovador.Height = altura;
    }

    private void carregaDados()
    {
        if (codigoTDL != -1)
        {
            DataSet ds = cDados.getMinhasTarefasLogado(-1, codigoUsuarioLogado, codigoEntidadeLogada, "AND CodigoTarefa = " + codigoTDL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow dr = ds.Tables[0].Rows[0];

                txtDescricaoTarefaBanco.Text = dr["DescricaoTarefa"].ToString();
                txtInicioPrevistoBanco.Text = dr["InicioPrevisto"].ToString() != "" ? string.Format("{0:" + Resources.traducao.geral_formato_data_csharp + "}", DateTime.Parse(dr["InicioPrevisto"].ToString())) : "";
                txtTerminoPrevistoBanco.Text = dr["TerminoPrevisto"].ToString() != "" ? string.Format("{0:" + Resources.traducao.geral_formato_data_csharp + "}", DateTime.Parse(dr["TerminoPrevisto"].ToString())) : "";
                ddlInicioReal.Text = dr["InicioReal"].ToString() != "" ? string.Format("{0:" + Resources.traducao.geral_formato_data_csharp + "}", DateTime.Parse(dr["InicioReal"].ToString())) : "";
                ddlTerminoReal.Text = dr["TerminoReal"].ToString() != "" ? string.Format("{0:" + Resources.traducao.geral_formato_data_csharp + "}", DateTime.Parse(dr["TerminoReal"].ToString())) : "";
                txtCodigoUsuarioResponsavelBanco.Text = dr["NomeUsuarioResponsavel"].ToString();
                mmAnotacoesBanco.Text = dr["Anotacoes"].ToString();
                ddlStatusTarefa.Value = int.Parse(dr["CodigoStatusTarefa"].ToString());
                txtEsforcoPrevistoBanco.Text = dr["EsforcoPrevisto"].ToString();
                txtEsforcoReal.Text = dr["EsforcoReal"].ToString();
                txtCustoPrevistoBanco.Text = dr["CustoPrevisto"].ToString() != "" ? string.Format("{0:n2}", double.Parse(dr["CustoPrevisto"].ToString())) : "";
                txtCustoRealBanco.Text = dr["CustoReal"].ToString() != "" ? string.Format("{0:n2}", double.Parse(dr["CustoReal"].ToString())) : "";
                txtEstagioBanco.Text = dr["Estagio"].ToString();
                txtOrigemTarefaBanco.Text = dr["DescricaoOrigem"].ToString();

                //if (dr["CodigoStatusTarefa"].ToString() != "1")//DIFERENTE DE ABERTA
                //{
                //ddlInicioReal.ClientEnabled = false;
                //ddlTerminoReal.ClientEnabled = false;
                //mmAnotacoesBanco.ClientEnabled = false;
                //txtEsforcoReal.ClientEnabled = false;
                //}
            }
        }
    }

    private void populaCombo()
    {
        DataSet ds = cDados.getStatusTarefas("");

        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {
            ddlStatusTarefa.TextField = "DescricaoStatusTarefa";
            ddlStatusTarefa.ValueField = "CodigoStatusTarefa";
            ddlStatusTarefa.DataSource = ds.Tables[0];
            ddlStatusTarefa.DataBind();
        }
    }

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string msgErro = "";

        msgErro = persisteEdicaoRegistro();

        if (msgErro == "")
        {
            callBack.JSProperties["cp_OK"] = Resources.traducao.DetalhesTDL_atribui__o_atualizada_com_sucesso_;
            carregaDados();
        }
        else
        {
            callBack.JSProperties["cp_OK"] = Resources.traducao.DetalhesTDL_erro_ao_atualizar_a_atribui__o;
        }
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        string msgErro = "";
        string esforcoReal = (txtEsforcoReal.Text == "" ? "0" : txtEsforcoReal.Text);
        string statusTarefa = (ddlStatusTarefa.Value != null) ? ddlStatusTarefa.Value.ToString() : "NULL";
        string inicioReal = ddlInicioReal.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlInicioReal.Text + "', " + Resources.traducao.geral_formato_data_sql_server + ")");
        string terminoReal = ddlTerminoReal.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlTerminoReal.Text + "', " + Resources.traducao.geral_formato_data_sql_server + ")");

        cDados.atualizaTarefaToDoList(codigoTDL.ToString(), inicioReal, terminoReal, esforcoReal,
                                      mmAnotacoesBanco.Text.Replace("'", "''"),
                                      statusTarefa, codigoUsuarioLogado, ref msgErro);        
        return msgErro;
    }
}
