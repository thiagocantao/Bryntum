using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using CDIS;

public partial class espacoTrabalho_PendenciasWorkflow : System.Web.UI.Page
{
    dados cDados;

    public int codigoUsuarioResponsavel;
    public int codigoEntidadeUsuarioResponsavel;
    public string stringConexao = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
       
        // cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // cDados.setInfoSistema("IDUsuarioLogado", DecodificaHashToUserCode(hashCode));

        cDados = CdadosUtil.GetCdados(null);

        if (Request.QueryString["cm"] == null)
        {

            if ((Request.QueryString["msg"] != null) && (Request.QueryString["msg"].ToString() != ""))
            {
                string stringErro = Request.QueryString["msg"].ToString();

                gvDados.SettingsText.EmptyCard = stringErro;
            }
            else if (Request.QueryString["sc"] != null)
            {
                string stringCriptografada = Request.QueryString["sc"].ToString();
                stringConexao = stringCriptografada;

                decriptaStringConexao(stringCriptografada);
            }
        }


        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        // How to get rid of this???
        cDados.setInfoSistema("ResolucaoCliente", "320x640");
        cDados.setInfoSistema("IDEstiloVisual", "iOS");
      
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if(gvDados.SettingsText.EmptyCard == "")
            populaGrid();
         Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/PendenciasWorkflow.js""></script>"));
        this.TH(this.TS("PendenciasWorkflow"));
        // Header.Controls.Add(cDados.getLiteral(@"<script> var cp_Path = '" + cDados.getPathSistema() + "'; console.log(cp_Path); </script>"));

        // gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
    }

    private void decriptaStringConexao(string stringCriptografada)
    {
        string chavePrivada = "", stringPlana;
        DataSet dsChaveWfMobile = cDados.getParametrosSistema(-1, "chaveAutenticacaoWsMobile");
        DateTime datConexao;
        int codigoEntidade = 0, codigoUsuario = 0;

        if (cDados.DataSetOk(dsChaveWfMobile) && cDados.DataTableOk(dsChaveWfMobile.Tables[0]))
        {
            chavePrivada = dsChaveWfMobile.Tables[0].Rows[0]["chaveAutenticacaoWsMobile"] + "";
            stringPlana = Cripto.descriptografar(stringCriptografada, chavePrivada);
            if (string.IsNullOrEmpty(stringPlana) == false)
            {
                string[] valores = stringPlana.Split(';');

                if (valores.Length > 2)
                {
                    if (DateTime.TryParse(valores[1].TrimEnd(' '), out datConexao))
                    {
                        if (datConexao.AddHours(1).CompareTo(DateTime.Now) >= 0)
                        {

                            if (int.TryParse(valores[2], out codigoEntidade))
                            {
                                string strWhere = string.Format(" AND us.[Email] = '{0}' ", valores[0].TrimEnd(' '));
                                DataSet dsUsr = cDados.getDadosResumidosUsuario(strWhere);
                                if (cDados.DataSetOk(dsUsr) && cDados.DataTableOk(dsUsr.Tables[0]))
                                {
                                    codigoUsuario = int.Parse(dsUsr.Tables[0].Rows[0]["CodigoUsuario"].ToString());
                                }
                            }
                        }
                    }

                }
            }
        }

        cDados.setInfoSistema("CodigoEntidade", codigoEntidade);
        cDados.setInfoSistema("IDUsuarioLogado", codigoUsuario);
        return;
    }

    private void populaGrid()
    {

        string comandoSQL = string.Format(@"EXEC [dbo].[p_wf_obtemListaInstanciasUsuario] 
                  @in_identificadorUsuario	= '{0}'
                , @in_codigoEntidade		= {1}
                , @in_codigoFluxo			= NULL
                , @in_codigoProjeto 		= NULL                
                , @in_somenteInteracao		= 1
                , @in_somentePendencia 		= 1
                ", codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.SettingsSearchPanel.Visible = false;
            gvDados.DataBind();
            }

    }


  /*  public string getRowCount()
    {
        string retorno = "";
        int quantidadeLinhas = 0;
        for (int i = 0; i < gvDados.VisibleRowCount; i++)
        {
            if (!gvDados.IsGroupRow(i))
                quantidadeLinhas++;
        }

        retorno = quantidadeLinhas + " pendências";

        return retorno;
    }*/
}