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
        if (Request.QueryString["CTDL"] != null && Request.QueryString["CTDL"].ToString() != "")
        {
            codigoTDL = int.Parse(Request.QueryString["CTDL"].ToString());
            codigoTDL = codigoTDL < 0 ? (codigoTDL * -1) : codigoTDL;
        }

        cDados.aplicaEstiloVisual(Page);
        
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
                txtInicioPrevistoBanco.Text = dr["InicioPrevisto"].ToString() != "" ? string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["InicioPrevisto"].ToString())) : "";
                txtTerminoPrevistoBanco.Text = dr["TerminoPrevisto"].ToString() != "" ? string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["TerminoPrevisto"].ToString())) : "";
                ddlInicioReal.Text = dr["InicioRealKanban"].ToString() != "" ? string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["InicioRealKanban"].ToString())) : "";
                ddlTerminoReal.Text = dr["TerminoRealKanban"].ToString() != "" ? string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["TerminoRealKanban"].ToString())) : ""; 
                txtCodigoUsuarioResponsavelBanco.Text = dr["NomeUsuarioResponsavel"].ToString();
                mmAnotacoesBanco.Text = dr["Anotacoes"].ToString();
                txtEsforcoPrevistoBanco.Text = dr["EsforcoPrevisto"].ToString();
                txtEsforcoReal.Text = dr["EsforcoRealKanban"].ToString();
                txtCustoPrevistoBanco.Text = dr["CustoPrevisto"].ToString() != "" ? string.Format("{0:n2}", double.Parse(dr["CustoPrevisto"].ToString())) : "";
                txtCustoRealBanco.Text = dr["CustoReal"].ToString() != "" ? string.Format("{0:n2}", double.Parse(dr["CustoReal"].ToString())) : ""; 
                txtEstagioBanco.Text = dr["Estagio"].ToString();
                txtOrigemTarefaBanco.Text = dr["DescricaoOrigem"].ToString();

                if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
                {
                    ddlInicioReal.ClientEnabled = false;
                    ddlTerminoReal.ClientEnabled = false;
                    mmAnotacoesBanco.ClientEnabled = false;
                    txtEsforcoReal.ClientEnabled = false;
                    btnSalvar.ClientVisible = false;
                }
            }
        }
    }

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string msgErro = "";

        msgErro = persisteEdicaoRegistro();

        if (msgErro == "")
        {
            callBack.JSProperties["cp_OK"] = "Atribuição Atualizada com Sucesso!";
            carregaDados();
        }
        else
        {
            callBack.JSProperties["cp_OK"] = "Erro ao Atualizar a Atribuição";
        }
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        string msgErro = "";
        string esforcoReal = (txtEsforcoReal.Text == "" ? "0" : txtEsforcoReal.Text);
        string inicioReal = ddlInicioReal.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlInicioReal.Text + "', 103)");
        string terminoReal = ddlTerminoReal.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlTerminoReal.Text + "', 103)");

        cDados.atualizaTarefaToDoListKanban(codigoTDL.ToString(), inicioReal, terminoReal, esforcoReal,
                                      mmAnotacoesBanco.Text.Replace("'", "''"), ref msgErro);        
        return msgErro;
    }
}
