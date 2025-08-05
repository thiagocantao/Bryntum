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
using CDIS;

public partial class wfRenderizaFormulario_Edicao : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;
    string resolucaoCliente;
    private string larguraTabelaWf;
    int? codigoFormulario = null;
    string versaoFormulario = "Atual";
    bool possuiVersoes = false;
    bool indicaAssinado = false;

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

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        

        int codigoModeloFormulario = int.Parse(Request.QueryString["CMF"].ToString());

        
        if (Request.QueryString["CF"] != null)
        {
            codigoFormulario = int.Parse(Request.QueryString["CF"].ToString());
        }

        

        int? codigoProjeto = null;
        if (Request.QueryString["CP"] != null)
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());


        larguraTabelaWf = (getLarguraTela() - 60).ToString();
        
        string alturaFormulario = "800px";
        if (Request.QueryString["AT"] != null)
            alturaFormulario = Request.QueryString["AT"] + "px";

        if (Request.QueryString["LT"] != null)
            larguraTabelaWf = Request.QueryString["LT"] + "px";

        if (Request.QueryString["Largura"] != null)
            larguraTabelaWf = Request.QueryString["Largura"].ToString();

        configuraVersoesFormulario();
        configuraAssinaturasFormulario();

        bool readOnly = false;

        if (Request.QueryString["RO"] != null)
            readOnly = Request.QueryString["RO"].ToString() == "S";
        
        if (Request.QueryString["CFV"] != null && Request.QueryString["CFV"].ToString() != "")
        {
            readOnly = true;
            codigoFormulario = int.Parse(Request.QueryString["CFV"].ToString());
        }

        if (indicaAssinado)
            readOnly = true;

        if (codigoFormulario != -1)
            hfSessao.Set("_CodigoFormularioMaster_", codigoFormulario);
        
        Hashtable parametros = new Hashtable();
        if (Request.QueryString["ppp"] != null)
        {
            parametros.Add("FormMaster", Request.QueryString["FormMaster"]);
            parametros.Add("ppp", true);
        }
        parametros.Add("LarguraTela", int.Parse(larguraTabelaWf.Replace("px", "")));
        parametros.Add("formularioImpressao", cDados.getPathSistema() + "wfRenderizaFormulario_Impressao.aspx");
        this.TH(this.TS("geral"));
        Formulario myForm = new Formulario(cDados.classeDados, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoModeloFormulario, new Unit(larguraTabelaWf + "px"), new Unit(alturaFormulario), readOnly, this, parametros, ref hfSessao,indicaAssinado);
        Control ctrl = myForm.constroiInterfaceFormulario(true, IsPostBack, codigoFormulario, codigoProjeto, "", "");
        form1.Controls.Add(ctrl);

        if (possuiVersoes)
        {
            string colunaCertificacao = "";

            if (indicaAssinado)
                colunaCertificacao = "<td id='tdAssinatura' style='padding-right:15px;' title='Visualizar assinaturas digitais'><img onclick='abreAssinaturas();' src='imagens/certificado.png' style='cursor:pointer;' /></td>";

            form1.Controls.Add(cDados.getLiteral(string.Format(@"
        <table><tr>{1}<td id='tbVersao' title='" + Resources.traducao.wfRenderizaFormulario_Edicao_lista_de_vers_es_do_formul_rio + "'><img src='imagens/anexo/version.png' onclick='abreVersoes();' alt='" + Resources.traducao.wfRenderizaFormulario_Edicao_lista_de_vers_es_do_formul_rio + "' style='cursor:pointer;'/></td><td id='tdVersao' style='padding-left:5px; font-size:13px;'>" + Resources.traducao.wfRenderizaFormulario_Edicao_vers_o + "{0}</td></tr></table>", versaoFormulario, colunaCertificacao)));
        }

        // popup onde será editado os subformulários
        popupCampo.Width = new Unit("970px");
        popupCampo.Height = new Unit("550px");

        cDados.aplicaEstiloVisual(this);
    }

    private void configuraVersoesFormulario()
    {
        //fazer novo pull request
        pcVersoes.Controls.Clear();
        if (codigoFormulario.HasValue)
        {
            string comandoSQL = string.Format(@"  declare @CodigoFormulario as bigint
              declare @CodigoWorkflow as int
              declare @CodigoInstanciaWf as bigint
              declare @CodigoEtapa as int
              declare @SequenciaOcorrenciaEtapa as int
              declare @CodigoModeloFormulario as int

              SET @CodigoFormulario = {0}
              SET @CodigoWorkflow = NULL
              SET @CodigoInstanciaWf = NULL
              SET @CodigoEtapa = NULL
              SET @SequenciaOcorrenciaEtapa = NULL
              SET @CodigoModeloFormulario = NULL

            SELECT [CodigoFormulario]
		            , [Titulo]
		            , [Versao]
		            , [DataVersao]
		            , [CodigoUsuarioResponsavel]
		            , [NomeUsuarioResponsavel]
		            , [PossuiCertificacao]	FROM [dbo].[f_getVersoesFormulario] (
               @CodigoFormulario
              ,@CodigoWorkflow
              ,@CodigoInstanciaWf
              ,@CodigoEtapa
              ,@SequenciaOcorrenciaEtapa
              ,@CodigoModeloFormulario)
", codigoFormulario.Value);

            DataSet ds = cDados.getDataSet(comandoSQL);
            string linhas = "";
            int codigoFormularioSelecionado = codigoFormulario.Value;

            if (Request.QueryString["CFV"] != null && Request.QueryString["CFV"].ToString() != "")
            {
                codigoFormularioSelecionado = int.Parse(Request.QueryString["CFV"].ToString());                
            }

            if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    int indexCFV = Request.QueryString.ToString().IndexOf("&CFV");
                    bool indicaFormOriginal = codigoFormulario == int.Parse(dr["CodigoFormulario"].ToString());
                    bool indicaFormSelecionado = codigoFormularioSelecionado == int.Parse(dr["CodigoFormulario"].ToString());
                    string queryString = indexCFV == -1 ? Request.QueryString.ToString() : Request.QueryString.ToString().Substring(0, indexCFV);
                    string urlRedirect = "wfRenderizaFormulario_Edicao.aspx?" + queryString + ((indicaFormOriginal) ? "" : ("&CFV=" + dr["CodigoFormulario"]));

                    linhas += string.Format(@"
                      <tr>
                        <td style='padding-right:10px; border: solid 1px #CCCCCC; border-right:0px; border-top:0px;'><img onclick=""mudaVersao('{3}');"" style='cursor:pointer' alt='" + Resources.traducao.wfRenderizaFormulario_Edicao_visualizar_vers_o + @"' src='imagens/searchBox.png' /></td>
                        <td align='right' style='padding-right:10px; border: solid 1px #CCCCCC; border-right:0px; border-top:0px;'>{0}</td>
                        <td align='center' style='padding-right:10px; border: solid 1px #CCCCCC; border-right:0px; border-top:0px;'>{1:dd/MM/yyyy HH:mm}</td>
                        <td style='border: solid 1px #CCCCCC; border-top:0px;'>{2}</td>
                     </tr>", i == (ds.Tables[0].Rows.Count - 1) ? Resources.traducao.wfRenderizaFormulario_Edicao_atual : dr["Versao"].ToString()
                           , dr["DataVersao"]
                           , dr["NomeUsuarioResponsavel"]
                           , urlRedirect);

                    if (codigoFormularioSelecionado == int.Parse(dr["CodigoFormulario"].ToString()))
                    {
                        versaoFormulario = i == (ds.Tables[0].Rows.Count - 1) ? Resources.traducao.wfRenderizaFormulario_Edicao_atual : dr["Versao"].ToString();

                        if (dr["Titulo"].ToString().Trim() != "")
                            versaoFormulario += " - " + dr["Titulo"].ToString();

                        indicaAssinado = dr["PossuiCertificacao"].ToString() == "S";
                    }
                }

                string table = string.Format(@"
                <table style='width:100%'>
                    <tr style='background-color:#CCCCCC'>
                        <td style='padding-right:10px; border: solid 1px #CCCCCC; border-right:0px; border-top:0px; width:30px'>&nbsp;</td>
                        <td align='right' style='padding-right:10px;border: solid 1px #CCCCCC; border-right:0px;width:60;'>" + Resources.traducao.wfRenderizaFormulario_Edicao_vers_o + @"</td>
                        <td align='center' style='padding-right:10px;border: solid 1px #CCCCCC; border-right:0px;width:130px;'>" + Resources.traducao.wfRenderizaFormulario_Edicao_data + @"</td>
                        <td style='border: solid 1px #CCCCCC;'>" + Resources.traducao.wfRenderizaFormulario_Edicao_respons_vel + @"</td>
                    </tr>
                    {0}
                </table>", linhas);

                pcVersoes.Controls.Add(cDados.getLiteral(table));
                possuiVersoes = true;
            }
        }
    }

    private void configuraAssinaturasFormulario()
    {
        pcAssinaturas.Controls.Clear();
        if (codigoFormulario.HasValue)
        {

            string linhas = "";
            int codigoFormularioSelecionado = codigoFormulario.Value;

            if (Request.QueryString["CFV"] != null && Request.QueryString["CFV"].ToString() != "")
            {
                codigoFormularioSelecionado = int.Parse(Request.QueryString["CFV"].ToString());
            }

            string comandoSQL = string.Format(@"SELECT * FROM dbo.f_getAssinaturasFormulario({0})", codigoFormularioSelecionado);

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    linhas += string.Format(@"
                      <tr>                        
                        <td align='center' style='padding-right:10px; border: solid 1px #FFFF66; border-right:0px; border-top:0px;'>{0:dd/MM/yyyy HH:mm}</td>
                        <td align='left' style='padding-right:10px; border: solid 1px #FFFF66; border-right:0px; border-top:0px;'>{1}</td>
                        <td style='border: solid 1px #FFFF66; border-top:0px;'>{2}</td>
                     </tr>", dr["DataAssinatura"]
                           , dr["NomeUsuario"]
                           , dr["ChaveAssinatura"]);
                }

                string table = string.Format(@"
                <table style='width:100%'>
                    <tr style='background-color:#FFFF66'>
                        <td align='center' style='padding-right:10px;border: solid 1px #FFFF66; border-right:0px;width:130px;'>" + Resources.traducao.wfRenderizaFormulario_Edicao_data + @"</td>
                        <td align='left' style='padding-right:10px;border: solid 1px #FFFF66; border-right:0px;'>" + Resources.traducao.wfRenderizaFormulario_Edicao_respons_vel + @"</td>
                        <td style='border: solid 1px #FFFF66;width:180px;'>" + Resources.traducao.wfRenderizaFormulario_Edicao_chave + @"</td>
                    </tr>
                    {0}
                </table>", linhas);

                pcAssinaturas.Controls.Add(cDados.getLiteral(table));
            }
        }
    }


    private int getLarguraTela()
    {
        return int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
    }

    private ASPxGridView gridToDoList()
    {
        if (form1.FindControl("pnExterno") == null)
            return null;

        if (form1.FindControl("pnExterno").FindControl("pnFormulario") == null)
            return null;

        if (form1.FindControl("pnExterno").FindControl("pnFormulario").FindControl("pcFormulario") == null)
            return null;

        ASPxPageControl pcFormulario = (ASPxPageControl)form1.FindControl("pnExterno").FindControl("pnFormulario").FindControl("pcFormulario");

        TabPage pgToDoList = pcFormulario.TabPages.FindByName("tabPageToDoList");

        if( pgToDoList != null)
            return (ASPxGridView)pgToDoList.FindControl("gvToDoList");
        else
            return null;

    }
}
