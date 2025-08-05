using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Projetos_DadosProjeto_agrupamentoContratoMaster : System.Web.UI.Page
{
    dados cDados;
    private int idUsuarioLogado;
    private int CodigoEntidade;
    
    string dbName = "";
    string dbOwner = "";

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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); //usuario logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());   //entidad logada.

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        if (!Page.IsPostBack)
        {
            carregaDadosFormulario();
        }
        cDados.aplicaEstiloVisual(Page);
        Master.geraRastroSite();
    }
    
    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/agrupamentoContratoMaster.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "agrupamentoContratoMaster"));
    }
    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        bool retorno = false;
        string mensagemErro = "";
        decimal EPBM = decimal.Parse((txtEPBM.Value != null) ? txtEPBM.Value.ToString() : "0");
        decimal ELM = decimal.Parse((txtELM.Value != null) ? txtELM.Value.ToString() : "0");
        decimal CCBM = decimal.Parse((txtCCBM.Value != null) ? txtCCBM.Value.ToString() : "0");
        decimal IMPSA = decimal.Parse((txtIMPSA.Value != null) ? txtIMPSA.Value.ToString() : "0");
        
        retorno = atualizaAgrupamentoContrato(EPBM, ELM, CCBM, IMPSA, ref mensagemErro);

        if (mensagemErro != "")
        {
            callback.JSProperties["cp_MensagemErro"] = "Erro ao salvar: " + mensagemErro;
        }
        else
        {
            callback.JSProperties["cp_MensagemErro"] = "";
        }
    }

    public bool atualizaAgrupamentoContrato(decimal txtEPBM, decimal txtELM, decimal txtCCBM,decimal txtIMPSA, ref string mensagemErro)
    {
        bool retorno = false;
        int regAfetados = 0;
        string comandoSQL = string.Format(@"
        UPDATE AgrupamentoContratoMaster
           SET ValorRealizadoI0 = {0}
         WHERE DescricaoContratoAgrupado = 'EPBM'
        UPDATE AgrupamentoContratoMaster
           SET ValorRealizadoI0 = {1}
         WHERE DescricaoContratoAgrupado = 'ELM'
       UPDATE AgrupamentoContratoMaster
           SET ValorRealizadoI0 = {2}
         WHERE DescricaoContratoAgrupado = 'CCBM'
       UPDATE AgrupamentoContratoMaster
           SET ValorRealizadoI0 = {3}
         WHERE DescricaoContratoAgrupado = 'IMPSA'", txtEPBM.ToString().Replace(",", "."), txtELM.ToString().Replace(",", "."), txtCCBM.ToString().Replace(",", "."), txtIMPSA.ToString().Replace(",", "."));
        try
        {
           retorno = cDados.execSQL(comandoSQL, ref regAfetados);
           if (regAfetados == 0)
           {
               mensagemErro = "Nenhum registro foi afetado, consulte o administrador do sistema ou realize os cadastros dependentes necessários.";
           }
        }
        catch (Exception ex)
        {
            retorno = false;
            mensagemErro = ex.Message;
        }
        return retorno;
    }

    public void carregaDadosFormulario()
    {
        string comandoSQL = string.Format(@"
        SELECT CodigoContrato
              ,CodigoContratoMaster
              ,DescricaoContratoAgrupado
              ,ValorRealizadoI0
         FROM AgrupamentoContratoMaster
        where codigoContratoMaster = {0}", 1);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            DataRow[] dr = ds.Tables[0].Select("[DescricaoContratoAgrupado] = 'EPBM'");
            txtEPBM.Text = dr[0]["ValorRealizadoI0"].ToString();

            dr = ds.Tables[0].Select("[DescricaoContratoAgrupado] = 'ELM'");
            txtELM.Text = dr[0]["ValorRealizadoI0"].ToString();

            dr = ds.Tables[0].Select("[DescricaoContratoAgrupado] = 'CCBM'");
            txtCCBM.Text = dr[0]["ValorRealizadoI0"].ToString();

            dr = ds.Tables[0].Select("[DescricaoContratoAgrupado] = 'IMPSA'");
            txtIMPSA.Text = dr[0]["ValorRealizadoI0"].ToString();
        }
    }
}