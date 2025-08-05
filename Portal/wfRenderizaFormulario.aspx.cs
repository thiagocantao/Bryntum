using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using CDIS;
using System.Collections.Specialized;
using System.Drawing;
using DevExpress.Web.Rendering;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Cdis.IntegraCertillion;
using Cdis.IntegraCertillion.com.certillion.labs;

public partial class wfRenderizaFormulario : System.Web.UI.Page
{
    dados cDados;
    string resolucaoCliente;

    //Variável exclusiva para identificar se o fluxo é o de "inclusão de uma nova proposta"
    int codigoModeloFormulario;
    int? codigoProjeto;
    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;
    string tituloFormulario = "";
    string tituloInstanciaWf = "";
    private int larguraTela;
    public string alturaFormulario;
    string versaoFormulario = "Atual";
    bool possuiVersoes = false;
    bool indicaAssinado = false;

    bool readOnly = false;
    Hashtable parametros = new Hashtable();

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

        if (Request.QueryString["CMF"] != null && Request.QueryString["CMF"].ToString() != "")
        {
            codigoModeloFormulario = int.Parse(Request.QueryString["CMF"].ToString());
        }
        else
        {
            codigoModeloFormulario = getCodigoModeloFormulario(Request.QueryString["INIMF"].ToString());
        }

        callbackWF.JSProperties["cp_FechaModal"] = Request.QueryString["FechaModalPosSalvar"] + "";

        // se foi passado o código projeto, seta na variável de sessão
        if (Request.QueryString["CPWF"] != null && Request.QueryString["CPWF"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CPWF"].ToString());
            // a linha abaixo é necessária para os formulários do tipo "Tela"
            if (codigoProjeto.Value != -1)
                cDados.setInfoSistema("CodigoProjeto", codigoProjeto);
            else
                cDados.setInfoSistema("CodigoProjeto", null);
        }
        else if(int.Parse(Request.QueryString["CIWF"]) != -1)
        {
            var codigoProjetoInfoSistema = cDados.getInfoSistema("CodigoProjeto");
            if(codigoProjetoInfoSistema != null && codigoProjetoInfoSistema is int && ((int)codigoProjetoInfoSistema != -1))
            {
                codigoProjeto = (int)codigoProjetoInfoSistema;
            }
        }

        // //caso não tenha sido passado o código do projeto pela URL, ou é um código inválido tenta obtê-lo da variável do sistema
        //if ((!codigoProjeto.HasValue) || (codigoProjeto.Value == -1))
        //{
        //    if (cDados.getInfoSistema("CodigoProjeto") != null)
        //        codigoProjeto = int.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());
        //}


        if (Request.QueryString["INIPERM"] != null)
        {
            readOnly = readOnly == true ? true : !cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto.Value, "null", "PR", 0, "null", Request.QueryString["INIPERM"].ToString());
        }

        renderizaFormulario();

        cDados.aplicaEstiloVisual(this);

        callbackWF.JSProperties["cp_QS"] = Request.QueryString.ToString();

        // se a chamada veio de dentro de um Workflow
        if (Request.QueryString["CWF"] != null)
        {
            // no workflow não tem exportacao para xls
            pnExportarGrid.Visible = false;
        }
        else
        {
            string estiloFooter = "dxgvControl dxgvGroupPanel";

            string cssPostfix = "", cssPath = "";

            cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

            if (cssPostfix != "")
                estiloFooter = "dxgvControl_" + cssPostfix + " dxgvGroupPanel_" + cssPostfix;

            tbBotoes.Attributes.Add("class", estiloFooter);

            tbBotoes.Style.Add("padding", "3px");

            tbBotoes.Style.Add("border-collapse", "collapse");

            tbBotoes.Style.Add("border-bottom", "none");
        }

        string podeEditarCronograma = codigoProjeto.HasValue && cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoProjeto.Value, "null", "PR", 0, "null", "PR_CadCrn") ? "S" : "N";

        string linkOpcao = cDados.getLinkPortalDesktop(Request.Url, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, codigoProjeto, "./");

        hfSessao.Set("UrlCrono", linkOpcao);
        hfSessao.Set("AcessaCrono", podeEditarCronograma);
    }

    private void configuraVersoesFormulario(int codigoFormularioParametro)
    {
        pcVersoes.Controls.Clear();
        int codigoWorkflow = -1;
        int codigoInstanciaWf = -1;
        int codigoEtapa = -1;
        int sequenciaOcorrenciaEtapa = -1;
        int codigoModeloFormulario = -1;
        bool retorno = false;

        retorno = int.TryParse(Request.QueryString["CWF"] + "", out codigoWorkflow);
        retorno = int.TryParse(Request.QueryString["CIWF"] + "", out codigoInstanciaWf);
        retorno = int.TryParse(Request.QueryString["CEWF"] + "", out codigoEtapa);
        retorno = int.TryParse(Request.QueryString["COWF"] + "", out sequenciaOcorrenciaEtapa);
        retorno = int.TryParse(Request.QueryString["CMF"] + "", out codigoModeloFormulario);

        int codigoFormulario = codigoFormularioParametro;

        if (codigoFormulario <= 0)
        {
            if (hfSessao.Contains("_CodigoFormularioMaster_"))
                codigoFormulario = int.Parse(hfSessao.Get("_CodigoFormularioMaster_").ToString());
        }

        bool existeFormularioRecemIncluido = (codigoFormularioParametro <= 0) && (codigoFormulario > 0);

        string comandoSQL = string.Format(@"
            DECLARE @CodigoFormulario as bigint
            DECLARE @CodigoWorkflow as int
            DECLARE @CodigoInstanciaWf as bigint
            DECLARE @CodigoEtapa as int
            DECLARE @SequenciaOcorrenciaEtapa as int
            DECLARE @CodigoModeloFormulario as int

            SET @CodigoFormulario = {0} 
            SET @CodigoWorkflow = {1}
            SET @CodigoInstanciaWf = {2}
            SET @CodigoEtapa = {3}
            SET @SequenciaOcorrenciaEtapa = {4}
            SET @CodigoModeloFormulario = {5}

            SELECT    [CodigoFormulario]    
              , [Titulo]            
              , [Versao]            
              , [DataVersao]          
              , [CodigoUsuarioResponsavel]   
              , [NomeUsuarioResponsavel]    
              , [PossuiCertificacao]      
               FROM [dbo].[f_getVersoesFormulario] (
                @CodigoFormulario
              , @CodigoWorkflow
              , @CodigoInstanciaWf
              , @CodigoEtapa
              , @SequenciaOcorrenciaEtapa
              , @CodigoModeloFormulario) ORDER BY Versao ", codigoFormulario,
          (codigoWorkflow == -1) ? "NULL" : codigoWorkflow.ToString(),
          (codigoInstanciaWf == -1) ? "NULL" : codigoInstanciaWf.ToString(),
          (codigoEtapa == -1) ? "NULL" : codigoEtapa.ToString(),
          (sequenciaOcorrenciaEtapa == -1) ? "NULL" : sequenciaOcorrenciaEtapa.ToString(),
          (codigoModeloFormulario == -1) ? "NULL" : codigoModeloFormulario.ToString());

        DataSet ds = cDados.getDataSet(comandoSQL);
        string linhas = "";
        int codigoFormularioSelecionado = codigoFormulario;

        if (Request.QueryString["CFV"] != null && Request.QueryString["CFV"].ToString() != "")
        {
            codigoFormularioSelecionado = int.Parse(Request.QueryString["CFV"].ToString());
        }

        //NameValueCollection auxQueryString = new NameValueCollection(Request.QueryString);
        NameValueCollection auxQueryString = HttpUtility.ParseQueryString(String.Empty);
        auxQueryString.Add(Request.QueryString);

        if (auxQueryString["CFV"] != null)
        {
            auxQueryString.Remove("CFV");
        }

        // se existeFormularioRecemIncluido, inserimos "&CF=xxx" na URL dos formulários a serem apresentados. 
        // isso evitará que o formulário que acabou de ser inserido fique somente leitura quando o usuário visualizar uma versão anterior e voltar à atual
        // ( a visualização de versões anteriores após ou durante a inclusão de um formulário só é possível se o formulário estiver configurado como "Novo em cada Ocorrência" )
        if (true == existeFormularioRecemIncluido)
        {
            if (auxQueryString["CF"] == null)
            {
                auxQueryString.Add("CF", codigoFormulario.ToString());
            }
            else
            {
                auxQueryString["CF"] = codigoFormulario.ToString();
            }
        }

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                bool indicaFormOriginal = ( (ds.Tables[0].Rows.Count == 1) || (codigoFormulario == int.Parse(dr["CodigoFormulario"].ToString())) );

                // na versão atual, NÃO se adiciona o parâmetro de url CFV
                if (i == (ds.Tables[0].Rows.Count - 1))
                {
                    if (auxQueryString["CFV"] != null)
                    {
                        auxQueryString.Remove("CFV");
                    }
                }
                else
                {
                    if (auxQueryString["CFV"] == null)
                    {
                        auxQueryString.Add("CFV", dr["CodigoFormulario"].ToString());
                    }
                    else
                    {
                        auxQueryString["CFV"] = dr["CodigoFormulario"].ToString();
                    }
                }
                string urlRedirect = "wfRenderizaFormulario.aspx?" + auxQueryString.ToString();

                linhas += string.Format(@"
                      <tr>
                        <td align='center' style='padding-right:10px; border: solid 1px #CCCCCC; border-right:0px; border-top:0px;'><img onclick=""mudaVersao('{3}');"" style='cursor:pointer' alt='" + Resources.traducao.wfRenderizaFormulario_visualizar_vers_o_ + @"src='imagens/searchBox.png' /></td>
                        <td align='right' style='padding-right:10px; border: solid 1px #CCCCCC; border-right:0px; border-top:0px;'>{0}</td>
                        <td align='center' style='padding-right:10px; border: solid 1px #CCCCCC; border-right:0px; border-top:0px;'>{1}</td>
                        <td style='border: solid 1px #CCCCCC; border-top:0px;'>{2}</td>
                     </tr>", i == (ds.Tables[0].Rows.Count - 1) ? (Resources.traducao.wfRenderizaFormulario_atual + ": " + dr["Versao"].ToString()) : dr["Versao"].ToString()
                       , dr["DataVersao"]
                       , dr["NomeUsuarioResponsavel"]
                       , urlRedirect);

                if (codigoFormularioSelecionado == int.Parse(dr["CodigoFormulario"].ToString()))
                {
                    versaoFormulario = dr["Versao"].ToString() + ":";

                    if (dr["Titulo"].ToString().Trim() != "")
                        versaoFormulario += " " + dr["Titulo"].ToString();

                    if (i == (ds.Tables[0].Rows.Count - 1))
                    {
                        versaoFormulario += " (" + Resources.traducao.wfRenderizaFormulario__ltima_vers_o + ")";
                    }

                    indicaAssinado = dr["PossuiCertificacao"].ToString() == "S";
                }
            }

            string table = string.Format(@"
                <table style='width:100%'>
                    <tr style='background-color:#CCCCCC'>
                        <td align='center' style='padding-right:10px; border: solid 1px #CCCCCC; border-right:0px; border-top:0px; width:30px'>&nbsp;</td>
                        <td align='right' style='padding-right:10px;border: solid 1px #CCCCCC; border-right:0px;width:60px;'>" + Resources.traducao.wfRenderizaFormulario_vers_o + @"</td>
                        <td align='center' style='padding-right:10px;border: solid 1px #CCCCCC; border-right:0px;width:150px;'>" + Resources.traducao.wfRenderizaFormulario_data + @"</td>
                        <td style='border: solid 1px #CCCCCC;'>" + Resources.traducao.wfRenderizaFormulario_respons_vel + @"</td>
                    </tr>
                    {0}
                </table>", linhas);

            pcVersoes.Controls.Add(cDados.getLiteral(table));
            possuiVersoes = true;
        }
    }

    private void configuraAssinaturasFormulario(int? codigoFormulario)
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
                        <td align='center' style='padding-right:10px; border: solid 1px #CCCCCC; border-right:0px; border-top:0px;'>{0:dd/MM/yyyy HH:mm}</td>
                        <td align='left' style='padding-right:10px; border: solid 1px #CCCCCC; border-right:0px; border-top:0px;'>{1}</td>
                        <td style='border: solid 1px #CCCCCC; border-top:0px;'>{2}</td>
                     </tr>", dr["DataAssinatura"]
                           , dr["NomeUsuario"]
                           , dr["ChaveAssinatura"]);
                }

                string table = string.Format(@"
                <table style='width:100%'>
                    <tr style='background-color:#CCCCCC'>
                        <td align='center' style='padding-right:10px;border: solid 1px #CCCCCC; border-right:0px;width:130px;'>Data</td>
                        <td align='left' style='padding-right:10px;border: solid 1px #CCCCCC; border-right:0px;'>Responsável</td>
                        <td style='border: solid 1px #CCCCCC;width:180px;'>Chave</td>
                    </tr>
                    {0}
                </table>", linhas);

                pcAssinaturas.Controls.Add(cDados.getLiteral(table));
            }
        }
    }

    private int getCodigoModeloFormulario(string iniciaisModeloFormulario)
    {
        int codigoModeloFormulario = 0;
        // busca o modelo do formulário de propostas
        string comandoSQL = string.Format("Select codigoModeloFormulario from modeloFormulario where IniciaisFormularioControladoSistema = '{0}'", iniciaisModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            DataTable dt = ds.Tables[0];
            if (cDados.DataTableOk(dt))
            {
                codigoModeloFormulario = int.Parse(dt.Rows[0]["codigoModeloFormulario"].ToString());
            }
        }
        return codigoModeloFormulario;
    }

    private int getLarguraTela()
    {
        return int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
    }

    private void renderizaFormulario()
    {
        int codigoWorkflow = -1;
        int codigoInstanciaWf = -1;

        if (codigoModeloFormulario > 0)
        {
            // se tem filtro por formularios
            string filtroCodigosFormularios = "";
            if (Request.QueryString["FF"] != null)
            {
                filtroCodigosFormularios = Request.QueryString["FF"].ToString().Trim();
            }

            int? codigoFormulario = null;
            int codigoFormularioParam = -1;

            if (Request.QueryString["CF"] != null)
            {
                codigoFormulario = int.Parse(Request.QueryString["CF"].ToString());
                codigoFormularioParam = codigoFormulario.Value;

                if (codigoFormulario.HasValue)
                    hfSessao.Set("_CodigoFormularioMaster_", codigoFormulario);
            }
            else if (codigoProjeto.HasValue && (Request.QueryString["CWF"] == null))
            {
                string comandoSQL = string.Format(@"
                SELECT Max(f.CodigoFormulario) AS CodigoFormulario
                  FROM Formulario as F INNER JOIN 
			           FormularioProjeto AS fp ON (fp.CodigoProject = {0} 
											    AND f.CodigoFormulario = fp.CodigoFormulario 
												AND f.CodigoModeloFormulario = {1} 
												AND f.DataExclusao IS NULL)"
                    , codigoProjeto
                    , codigoModeloFormulario);

                DataSet dsForm = cDados.getDataSet(comandoSQL);

                if (cDados.DataTableOk(dsForm.Tables[0]) && dsForm.Tables[0].Rows[0]["CodigoFormulario"].ToString() != "")
                    codigoFormularioParam = int.Parse(dsForm.Tables[0].Rows[0]["CodigoFormulario"].ToString());

            }

            configuraVersoesFormulario(codigoFormularioParam);

            configuraAssinaturasFormulario(codigoFormularioParam);

            bool readOnly = false;

            if (Request.QueryString["RO"] != null)
                readOnly = Request.QueryString["RO"].ToString().ToUpper().Contains("S");

            if (Request.QueryString["CFV"] != null && Request.QueryString["CFV"].ToString() != "")
            {
                readOnly = true;
                codigoFormulario = int.Parse(Request.QueryString["CFV"].ToString());
            }

            if (indicaAssinado)
                readOnly = true;

            if (codigoFormulario.HasValue)
                hfSessao.Set("_CodigoFormularioMaster_", codigoFormulario);

            //if (Request.QueryString["RO"] != null)
            //    readOnly = Request.QueryString["RO"].ToString() == "S";

            /*------*/
            alturaFormulario = getAlturaTela();//"800";
            if (Request.QueryString["AT"] != null && Request.QueryString["AT"].ToString().Trim() != "")
                alturaFormulario = Request.QueryString["AT"];
            ///*------*/


            if (Request.QueryString["CWF"] != null)
            {
                codigoWorkflow = int.Parse(Request.QueryString["CWF"].ToString());
                codigoInstanciaWf = int.Parse(Request.QueryString["CIWF"].ToString());

                parametros.Add("CodigoWorkflow", codigoWorkflow.ToString());
                parametros.Add("CodigoInstanciaWorkflow", codigoInstanciaWf.ToString());
                parametros.Add("CodigoEtapaAtual", Request.QueryString["CEWF"].ToString());
                parametros.Add("CodigoOcorrenciaAtual", Request.QueryString["COWF"].ToString());
            }
            parametros.Add("formularioEdicao", cDados.getPathSistema() + "wfRenderizaFormulario_Edicao.aspx");
            parametros.Add("formularioImpressao", cDados.getPathSistema() + "wfRenderizaFormulario_Impressao.aspx");
            

            parametros.Add("nomeSessao", "Form");

            larguraTela = getLarguraTela();

            if (Request.QueryString["WSCR"] != null)
                larguraTela = int.Parse(Request.QueryString["WSCR"]);

            larguraTela -= 150;

            // se existe o parametro para reduzir a largura...
            if (Request.QueryString["RedLarg1024"] != null && getLarguraTela() <= 1024)
            {
                larguraTela -= int.Parse(Request.QueryString["RedLarg1024"].ToString());
            }

            if (Request.QueryString["PND"] != null)
            {
                larguraTela = larguraTela - 20;
                alturaFormulario = (int.Parse(alturaFormulario) - 20).ToString();
            }

            /*-----*/
            string alturaMyForm = (int.Parse(alturaFormulario) - 100) + "px";

            alturaFormulario = (alturaFormulario + 120) + "px";
            /*-----*/

            larguraTela = larguraTela <= 400 ? 900 : larguraTela;
            parametros.Add("LarguraTela", larguraTela);
            Formulario myForm = new Formulario(cDados.classeDados, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, codigoModeloFormulario, new Unit("100%"), new Unit(alturaMyForm), readOnly, this, parametros, ref hfSessao, indicaAssinado);

            if (myForm.TipoFormularioPreDefinido == FormulariosPreDefinidos.NovaProposta)
            {
                if ((!codigoProjeto.HasValue) || (codigoProjeto.Value == -1))
                {
                    hfSessao.Set("_TipoOperacao_", "I");
                }
                myForm.AntesSalvar += new Formulario.AntesSalvarEventHandler(myForm_AntesSalvarFormularioProposta);
            }

            if (hfSessao.Contains("_CodigoFormularioMaster_"))
                codigoFormulario = int.Parse(hfSessao.Get("_CodigoFormularioMaster_").ToString());

            // os eventos abaixo só podem ser executados quando a chamada desta tela está relacionada a Worflow
            if (codigoWorkflow > 0 || codigoInstanciaWf > 0)
                myForm.AposSalvar += new Formulario.AposSalvarEventHandler(myForm_AposSalvarFormularioWorkflow);

            string cssFile = "";
            string cssPostFix = "";

            cDados.getVisual("MaterialCompact", ref cssFile, ref cssPostFix);

            Control ctrl = myForm.constroiInterfaceFormulario(false, IsPostBack, codigoFormulario, codigoProjeto, cssFile, cssPostFix);

            form1.Controls.Add(ctrl);

            DataSet ds = cDados.getDataSet("SELECT Valor FROM ParametroConfiguracaoSistema WHERE Parametro = 'ControlaBloqueioEdicaoFormulario' AND CodigoEntidade = " + codigoEntidadeUsuarioResponsavel);

            if (readOnly == false && codigoFormularioParam > 0 && ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Valor"].ToString() == "S")
            {
                DataSet dsFormBloqueio = cDados.getDataSet(@"
                SELECT 1 
                  FROM Formulario f
                 WHERE ISNULL(CodigoUsuarioCheckOut, -1) = " + codigoUsuarioResponsavel + " AND CodigoFormulario = " + codigoFormularioParam);

                if (dsFormBloqueio.Tables[0] != null && dsFormBloqueio.Tables[0].Rows.Count > 0)
                {
                    callbackVerificacaoAssinatura.JSProperties["cp_Desbloqueio"] = 'S';
                    callbackVerificacaoAssinatura.JSProperties["cp_CodigoFormulario"] = codigoFormularioParam;
                }
            }

            if (possuiVersoes)
            {
                string colunaCertificacao = "";

                if (indicaAssinado)
                    colunaCertificacao = "<td id='tdAssinatura' style='padding-right:5px;' title='Visualizar assinaturas digitais'><img onclick='abreAssinaturas();' src='imagens/certificado.png' style='cursor:pointer;' /></td>";

                form1.Controls.Add(cDados.getLiteral(string.Format(@"
        <table><tr>{1}<td id='tbVersao' title='" + Resources.traducao.wfRenderizaFormulario_lista_de_vers_es_do_formul_rio + @"'><img src='imagens/anexo/version.png' onclick='abreVersoes();' alt='" + Resources.traducao.wfRenderizaFormulario_lista_de_vers_es_do_formul_rio + @"' style='cursor:pointer;'/></td><td id='tdVersao' style='padding-left:5px; font-size:13px; font-family: 'Roboto Regular', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif !important;'>" + Resources.traducao.wfRenderizaFormulario_vers_o + " {0}</td></tr></table>", versaoFormulario, colunaCertificacao)));
            }

            // se o painel de exportação estiver visivel e o tipo do formulário não for lista (tipo 2), deve ser escondido
            if (pnExportarGrid.Visible && parametros["modeloCodigoTipoFormulario"].ToString() != "2")
                pnExportarGrid.Visible = false;
        }
        callbackWF.JSProperties["cp_CIWF"] = codigoInstanciaWf;
        callbackWF.JSProperties["cp_CWF"] = codigoWorkflow;
        callbackWF.JSProperties["cp_CI"] = codigoInstanciaWf;
    }

    private string getAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int alturaTela = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        return (alturaTela - 185).ToString();
    }

    void myForm_AntesSalvarFormularioProposta(object sender, EventFormsWF e, ref string mensagemErroEvento)
    {
        // apenas por garantia, já que o evento só é adicionado caso seja o formulário de nova proposta;
        if (e.TipoFormularioPreDefinido != FormulariosPreDefinidos.NovaProposta)
            return;

        string detalheProjeto = null; ; // mantendo null para tratamento diferenciado
        string categoriaProjeto = "";
        string unidadeProjeto = "";
        string descricaoProposta = "";

        // se vai salvar o formulário de proposta de iniciativa, cria ou atualiza o projeto associado.
        if (e.TipoFormularioPreDefinido == FormulariosPreDefinidos.NovaProposta)
        {
            for (int i = 0; i < e.camposControladoSistema.Count; i++)
            {
                object[] Controles = e.camposControladoSistema[i];
                if (Controles[0].ToString() == "DESC")
                {
                    Control temp = (Controles[2] as ASPxTextBox);
                    if (temp != null)
                        tituloFormulario = (Controles[2] as ASPxTextBox).Text.Replace("'", "´");
                }
                if (Controles[0].ToString() == "DETA")
                {
                    detalheProjeto = (Controles[2] as ASPxMemo).Text.Replace("'", "´");
                }
                if (Controles[0].ToString() == "CATE")
                    categoriaProjeto = (Controles[2] as ASPxComboBox).Value == null ? "" : (Controles[2] as ASPxComboBox).Value.ToString();
                if (Controles[0].ToString() == "UNID")
                    unidadeProjeto = (Controles[2] as ASPxComboBox).Value.ToString();
            }


            if (detalheProjeto == null)
                descricaoProposta = "NULL";
            else if (detalheProjeto == "")
                descricaoProposta = "NULL";
            else
                descricaoProposta = string.Format("'{0}'", detalheProjeto);

            if (categoriaProjeto == "")
                categoriaProjeto = "NULL";

            if (unidadeProjeto == "")
                unidadeProjeto = "NULL";

            if (tituloFormulario != "")
            {
                string comandoSQL;
                int afetados = 0;
                // se ainda não tem o projeto, inclui.
                if ((!codigoProjeto.HasValue) || (codigoProjeto.Value == -1))
                {
                    // chama a procedure para incluir o projeto;
                    comandoSQL = string.Format(
                        @"BEGIN
                            DECLARE @CodigoProjeto int

                            EXEC [dbo].[p_InsereProposta] '{0}', {1}, {2}, {3}, {4}, {5}, @CodigoProjeto out

                            SELECT @CodigoProjeto

                          END", tituloFormulario, codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, categoriaProjeto, unidadeProjeto, descricaoProposta);
                    DataSet ds = cDados.getDataSet(comandoSQL);
                    e.codigoProjeto = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                } // if (e.codigoProjeto <= 0)
                else
                {
                    comandoSQL = string.Format(
                        @"  UPDATE projeto
                               SET NomeProjeto = '{0}' ", tituloFormulario);

                    if (detalheProjeto != null)
                        comandoSQL = comandoSQL + string.Format(@" 
                            , DescricaoProposta = {0} ", descricaoProposta);

                    if (categoriaProjeto != "NULL")
                        comandoSQL = comandoSQL + string.Format(@" 
                            , CodigoCategoria = {0} ", categoriaProjeto);

                    if (unidadeProjeto != "NULL")
                        comandoSQL = comandoSQL + string.Format(@" 
                            , CodigoUnidadeNegocio = {0} ", unidadeProjeto);

                    comandoSQL = comandoSQL + string.Format(@"
                            WHERE codigoProjeto = {0}", e.codigoProjeto);
                    cDados.execSQL(comandoSQL, ref afetados);
                }
                // chama a procedure para atualizar a lista de objetivos relacionados à proposta (projeto)
                comandoSQL = string.Format(
                    @" EXEC [dbo].[p_AtualizaInfoNovaProposta] {0}, {1}", e.codigoProjeto, e.codigoFormulario);
                cDados.execSQL(comandoSQL, ref afetados);

            }
        }

        return;
    }

    void myForm_AposSalvarFormularioWorkflow(object sender, EventFormsWF e, ref string mensagemErroEvento)
    {
        gravaInstanciaWf(e);

        int auxCodigoInstanciaWf = int.Parse(e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString());

        // se já auxCodigoInstanciaWf> 0 -> significa que quando o formulário foi chamado, já havia uma instância;
        if (auxCodigoInstanciaWf > 0)
        {
            string comandoSQL = string.Format(
                @"BEGIN
                if (not exists(SELECT 1 FROM FormulariosInstanciasWorkflows
                                WHERE [CodigoWorkflow] = {0}
                                  AND [CodigoInstanciaWf] = {1}
                                  AND [SequenciaOcorrenciaEtapa] = {2}
                                  AND [CodigoEtapaWf] = {3}
                                  AND [CodigoFormulario] = {4} ) )

                        INSERT INTO [FormulariosInstanciasWorkflows]
                            (
                                [CodigoWorkflow]
                                , [CodigoInstanciaWf]
                                , [SequenciaOcorrenciaEtapa]
                                , [CodigoEtapaWf]
                                , [CodigoFormulario]
                            )
                            VALUES
                          (
                                {0}
                                , {1}
                                , {2}
                                , {3}
                                , {4}
                          )
                END
            ", e.parametrosEntrada["CodigoWorkflow"].ToString(),
                   e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString(),
                   e.parametrosEntrada["CodigoOcorrenciaAtual"].ToString(),
                   e.parametrosEntrada["CodigoEtapaAtual"].ToString(),
                   e.codigoFormulario);
            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);
        }
    }

    private bool gravaInstanciaWf(EventFormsWF e)
    {
        int auxCodigoInstanciaWf = e.parametrosEntrada["CodigoInstanciaWorkflow"] != null && e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString() != "" ? int.Parse(e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString()) : -1;

        // se estiver incluindo um formulário, verifica se precisa criar a instância
        if (auxCodigoInstanciaWf <= 0)
            verificaNecessidadeCriacaoInstanciaWf(e);
        else
            verificaNecessidadeAtualizacaoInstanciaWf(e);

        return true;
    }

    private void verificaNecessidadeCriacaoInstanciaWf(EventFormsWF e)
    {
        int auxCodigoInstanciaWf = int.Parse(e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString());

        // se já auxCodigoInstanciaWf<= 0 -> significa que quando o formulário foi chamado, NÃO havia instância;
        if (auxCodigoInstanciaWf <= 0)
        {
            int auxCodigoWorkflow = int.Parse(e.parametrosEntrada["CodigoWorkflow"].ToString());

            tituloInstanciaWf = montaTituloInstancia(e.codigoProjeto, auxCodigoWorkflow);

            string comandoSQL = string.Format(
                            @"BEGIN 
                    BEGIN TRAN
                    BEGIN TRY
                    INSERT INTO [InstanciasWorkflows]
                               ([CodigoWorkflow]
                               ,[NomeInstancia]
                               ,[DataInicioInstancia]
                               ,[DataTerminoInstancia]
                               ,[OcorrenciaAtual]
                               ,[EtapaAtual]
                               ,[IdentificadorUsuarioCriadorInstancia]
                               ,[IdentificadorProjetoRelacionado])
                         VALUES
                               ({0}
                               ,LEFT('{1}',250)
                               , GETDATE()
                               , NULL
                               , NULL
                               , NULL
                               , '{2}'
                               , {3} )

                    DECLARE @CodigoInstanciaWf int 
                    DECLARE @CodigoEtapaInicial int
                    SELECT @CodigoInstanciaWf = scope_identity(), 
                           @CodigoEtapaInicial = [CodigoEtapaInicial]  
	                  FROM Workflows 
                     WHERE CodigoWorkflow = {0}

                    INSERT INTO [EtapasInstanciasWf]
                               ([CodigoWorkflow]
                               ,[CodigoInstanciaWf]
                               ,[SequenciaOcorrenciaEtapaWf]
                               ,[CodigoEtapaWf]
                               ,[DataInicioEtapa]
                               ,[IdentificadorUsuarioIniciador]
                               ,[DataTerminoEtapa]
                               ,IdentificadorUsuarioFinalizador)
                         VALUES
                               ({0}
                               ,@CodigoInstanciaWf
                               ,1
                               ,@CodigoEtapaInicial
                               , GETDATE()
                               , '{2}'
                               , NULL
                               , NULL)

                        UPDATE InstanciasWorkflows
                           SET [OcorrenciaAtual] = 1
                             , [EtapaAtual] = @CodigoEtapaInicial
                        WHERE [CodigoWorkflow] = {0}
                          AND [CodigoInstanciaWf] = @CodigoInstanciaWf

                        INSERT INTO [FormulariosInstanciasWorkflows]
	                        (
		                        [CodigoWorkflow]
		                        , [CodigoInstanciaWf]
		                        , [SequenciaOcorrenciaEtapa]
		                        , [CodigoEtapaWf]
		                        , [CodigoFormulario]
	                        )
                            VALUES
                          (
		                        {0}
		                        , @CodigoInstanciaWf
		                        , 1
		                        , @CodigoEtapaInicial
		                        , {4}
                          )

                        SELECT @CodigoInstanciaWf as CodigoInstanciaWf, @CodigoEtapaInicial as CodigoEtapaInicial
                        COMMIT
                        END TRY
                        BEGIN CATCH
		                    DECLARE 
			                      @ErrorMessage		Nvarchar(4000)
			                    , @ErrorSeverity	Int
			                    , @ErrorState		Int
			                    , @ErrorNumber		Int;

		                    SET @ErrorMessage		= ERROR_MESSAGE();
		                    SET @ErrorSeverity	    = ERROR_SEVERITY();
		                    SET @ErrorState			= ERROR_STATE();
		                    SET @ErrorNumber		= ERROR_NUMBER();
                            ROLLBACK TRANSACTION
			                RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
                        END CATCH
                END
            ", auxCodigoWorkflow, tituloInstanciaWf.Replace("'", "''"), codigoUsuarioResponsavel, e.codigoProjeto, e.codigoFormulario);

            DataSet ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataTable dt = ds.Tables[0];
                auxCodigoInstanciaWf = int.Parse(dt.Rows[0]["CodigoInstanciaWf"].ToString());

                if (e.parametros.Contains("CodigoInstanciaWf"))
                    e.parametros.Remove("CodigoInstanciaWf");

                if (e.parametros.Contains("CodigoEtapaWf"))
                    e.parametros.Remove("CodigoEtapaWf");

                if (e.parametros.Contains("SequenciaOcorrenciaEtapaWf"))
                    e.parametros.Remove("SequenciaOcorrenciaEtapaWf");

                e.parametros.Add("CodigoInstanciaWf", auxCodigoInstanciaWf);
                e.parametros.Add("CodigoEtapaWf", dt.Rows[0]["CodigoEtapaInicial"].ToString());
                e.parametros.Add("SequenciaOcorrenciaEtapaWf", "1");

                // todo: verificar os nomes dos parametros incluídos e seus usos;

            } // if (cDados.DataSetOk(ds) && ...
        } // if (codigoInstanciaWf <= 0)
    }

    private void verificaNecessidadeAtualizacaoInstanciaWf(EventFormsWF e)
    {
        // por enquanto, só temos atualização do nome da instância quando se atualiza o nome do projeto no formulário NovaProposta
        if (e.TipoFormularioPreDefinido == FormulariosPreDefinidos.NovaProposta)
        {
            int auxCodigoInstanciaWf = int.Parse(e.parametrosEntrada["CodigoInstanciaWorkflow"].ToString());
            int auxCodigoWorkflow = int.Parse(e.parametrosEntrada["CodigoWorkflow"].ToString());
            tituloInstanciaWf = montaTituloInstancia(codigoProjeto, auxCodigoWorkflow);

            string comandoSQL = string.Format(
                @"  UPDATE InstanciasWorkflows
                        SET NomeInstancia = '{2}'
                        WHERE [CodigoWorkflow] = {0}
                        AND [CodigoInstanciaWf] = {1}
                            ", auxCodigoWorkflow, auxCodigoInstanciaWf, tituloInstanciaWf.Replace("'", "''"));
            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);
        }
        return;
    }

    private string montaTituloInstancia(int? codigoProjeto, int codigoWorkflow)
    {
        DataSet ds;
        DataRow dr;
        string titulo = "";
        string comandoSQL = string.Format(@"
            SELECT f.[NomeFluxo]
	            FROM
		            [dbo].[Workflows]				AS [wf]
				        INNER JOIN [dbo].[Fluxos]	AS [f]	ON 
					        ( f.[CodigoFluxo] = wf.[CodigoFluxo] )
	            WHERE
				        wf.[CodigoWorkflow]	= {0} ", codigoWorkflow);
        try
        {
            ds = cDados.getDataSet(comandoSQL);
            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                dr = ds.Tables[0].Rows[0];
                titulo = dr["NomeFluxo"].ToString();
            }
        }
        catch
        {
            titulo = "";
        }
        if (codigoProjeto != null)
        {
            comandoSQL = string.Format(@"
            SELECT 
		            p.[NomeProjeto]
	            FROM
		            [dbo].[Projeto]					AS [p]
	            WHERE
				        p.CodigoProjeto		= {0}", codigoProjeto);
            try
            {
                ds = cDados.getDataSet(comandoSQL);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    dr = ds.Tables[0].Rows[0];
                    titulo = titulo + " - " + dr["NomeProjeto"].ToString();
                }
            }
            catch
            {
            }
        }
        return titulo;
    }

    protected void btnExportarXLS_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }

    private void criaInstanciaWF_ASPX()
    {
        if (!codigoProjeto.HasValue && cDados.getInfoSistema("CodigoProjeto") != null)
            codigoProjeto = int.Parse(cDados.getInfoSistema("CodigoProjeto").ToString());

        string comandoSQL = string.Format(
                        @"BEGIN
                            DECLARE @CodigoFormularioMaster int

                            INSERT INTO Formulario (CodigoModeloFormulario, DescricaoFormulario, DataInclusao, IncluidoPor, DataExclusao, DataPublicacao)
                            VALUES ({0}, '{1}', getdate(), {2}, getdate(), getdate())

                            SELECT @CodigoFormularioMaster = scope_identity()

			                INSERT INTO FormularioProjeto (CodigoFormulario, CodigoProject)
                            VALUES (@CodigoFormularioMaster, {3})
                            
                            SELECT @CodigoFormularioMaster AS CodigoFormulario

                          END", codigoModeloFormulario, "Formulario ASPX", codigoUsuarioResponsavel, codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            int codigoFormulario = -1;

            codigoFormulario = int.Parse(ds.Tables[0].Rows[0]["CodigoFormulario"].ToString());

            int auxCodigoInstanciaWf = -1;

            // se já auxCodigoInstanciaWf<= 0 -> significa que quando o formulário foi chamado, NÃO havia instância;
            if (auxCodigoInstanciaWf <= 0)
            {
                int auxCodigoWorkflow = int.Parse(parametros["CodigoWorkflow"].ToString());

                tituloInstanciaWf = montaTituloInstancia(codigoProjeto, auxCodigoWorkflow);
                comandoSQL = string.Format(
                                @"BEGIN
                    INSERT INTO [InstanciasWorkflows]
                               ([CodigoWorkflow]
                               ,[NomeInstancia]
                               ,[DataInicioInstancia]
                               ,[DataTerminoInstancia]
                               ,[OcorrenciaAtual]
                               ,[EtapaAtual]
                               ,[IdentificadorUsuarioCriadorInstancia]
                               ,[IdentificadorProjetoRelacionado])
                         VALUES
                               ({0}
                               ,LEFT('{1}',250)
                               , GETDATE()
                               , NULL
                               , NULL
                               , NULL
                               , '{2}'
                               , {3} )

                    DECLARE @CodigoInstanciaWf int 
                    DECLARE @CodigoEtapaInicial int
                    SELECT @CodigoInstanciaWf = scope_identity(), 
                           @CodigoEtapaInicial = [CodigoEtapaInicial]  
	                  FROM Workflows 
                     WHERE CodigoWorkflow = {0}

                    INSERT INTO [EtapasInstanciasWf]
                               ([CodigoWorkflow]
                               ,[CodigoInstanciaWf]
                               ,[SequenciaOcorrenciaEtapaWf]
                               ,[CodigoEtapaWf]
                               ,[DataInicioEtapa]
                               ,[IdentificadorUsuarioIniciador]
                               ,[DataTerminoEtapa]
                               ,IdentificadorUsuarioFinalizador)
                         VALUES
                               ({0}
                               ,@CodigoInstanciaWf
                               ,1
                               ,@CodigoEtapaInicial
                               , GETDATE()
                               , '{2}'
                               , NULL
                               , NULL)

                        UPDATE InstanciasWorkflows
                           SET [OcorrenciaAtual] = 1
                             , [EtapaAtual] = @CodigoEtapaInicial
                        WHERE [CodigoWorkflow] = {0}
                          AND [CodigoInstanciaWf] = @CodigoInstanciaWf

                        INSERT INTO [FormulariosInstanciasWorkflows]
	                        (
		                        [CodigoWorkflow]
		                        , [CodigoInstanciaWf]
		                        , [SequenciaOcorrenciaEtapa]
		                        , [CodigoEtapaWf]
		                        , [CodigoFormulario]
	                        )
                            VALUES
                          (
		                        {0}
		                        , @CodigoInstanciaWf
		                        , 1
		                        , @CodigoEtapaInicial
		                        , {4}
                          )

                        SELECT @CodigoInstanciaWf as CodigoInstanciaWf, @CodigoEtapaInicial as CodigoEtapaInicial

                END
            ", auxCodigoWorkflow, tituloInstanciaWf.Replace("'", "''"), codigoUsuarioResponsavel, codigoProjeto, codigoFormulario);

                ds = cDados.getDataSet(comandoSQL);
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    DataTable dt = ds.Tables[0];
                    auxCodigoInstanciaWf = int.Parse(dt.Rows[0]["CodigoInstanciaWf"].ToString());
                    string codigoEtapaWf = dt.Rows[0]["CodigoEtapaInicial"].ToString();

                    callbackWF.JSProperties["cp_CodigoInstanciaWf"] = auxCodigoInstanciaWf;

                    // se já auxCodigoInstanciaWf> 0 -> significa que quando o formulário foi chamado, já havia uma instância;
                    if (auxCodigoInstanciaWf > 0)
                    {
                        comandoSQL = string.Format(
                            @"BEGIN
                if (not exists(SELECT 1 FROM FormulariosInstanciasWorkflows
                                WHERE [CodigoWorkflow] = {0}
                                  AND [CodigoInstanciaWf] = {1}
                                  AND [SequenciaOcorrenciaEtapa] = {2}
                                  AND [CodigoEtapaWf] = {3}
                                  AND [CodigoFormulario] = {4} ) )

                        INSERT INTO [FormulariosInstanciasWorkflows]
                            (
                                [CodigoWorkflow]
                                , [CodigoInstanciaWf]
                                , [SequenciaOcorrenciaEtapa]
                                , [CodigoEtapaWf]
                                , [CodigoFormulario]
                            )
                            VALUES
                          (
                                {0}
                                , {1}
                                , {2}
                                , {3}
                                , {4}
                          )
                END
            ", parametros["CodigoWorkflow"].ToString(),
                               auxCodigoInstanciaWf,
                               1,
                               codigoEtapaWf,
                               codigoFormulario);
                        int afetados = 0;
                        cDados.execSQL(comandoSQL, ref afetados);

                    } // if (cDados.DataSetOk(ds) &&
                }
            }
        }
    }

    protected void callbackWF_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        criaInstanciaWF_ASPX();
    }

    protected void callbackReload_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callbackReload.JSProperties["cp_URL"] = "";
        if (e.Parameter != "" && Request.QueryString["Engine"] != null && Request.QueryString["Engine"].ToString() == "S")
        {
            string queryString = "";

            //if (Request.QueryString["CPWF"] != null)
            //    queryString += "&CP=" + Request.QueryString["CPWF"];

            if (Request.QueryString["CFX"] != null)
                queryString += "&CF=" + Request.QueryString["CFX"];

            if (Request.QueryString["AEI"] != null)
                queryString += "&AEI=" + Request.QueryString["AEI"];

            if (Request.QueryString["Demandas"] != null)
                queryString += "&Demandas=" + Request.QueryString["Demandas"];

            if (Request.QueryString["ModuloMenuLDP"] != null)
                queryString += "&ModuloMenuLDP=" + Request.QueryString["ModuloMenuLDP"];

            if (Request.QueryString["PlanosInvestimento"] != null)
                queryString += "&PlanosInvestimento=" + Request.QueryString["PlanosInvestimento"];

            if (Request.QueryString["Popup"] != null)
                queryString += "&Popup=" + Request.QueryString["Popup"];

            if (Request.QueryString["AlturaWf"] != null)
                queryString += "&Altura=" + Request.QueryString["AlturaWf"];

            callbackReload.JSProperties["cp_URL"] = cDados.getPathSistema() + "wfEngineInterno.aspx?CW=" + Request.QueryString["CWF"] + e.Parameter + queryString;
        }

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "FormDinam");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "FormDinam", "Formulário", this);
    }

    #endregion    
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group || e.RowType == GridViewRowType.Data)
        {
            if (e.RowType == GridViewRowType.Group && e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
            else
            {
                string strValue = System.Text.RegularExpressions.Regex.Replace(e.Text, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
    }

    protected void callbackVerificacaoAssinatura_Callback(object source, CallbackEventArgs e)
    {
        string mensagemErro;
        int codigoFormulario;
        if (!int.TryParse(e.Parameter, out codigoFormulario))
            codigoFormulario = int.Parse(Request.QueryString["CF"]);
        if (!VerificaAssinaturaFormularioValida(codigoFormulario, out mensagemErro))
            e.Result = mensagemErro;
    }

    private bool VerificaAssinaturaFormularioValida(int codigoFormulario, out string mensagemErro)
    {
        mensagemErro = string.Empty;
        string comandoSql;

        #region Comando SQL

        comandoSql = string.Format(@"
BEGIN
DECLARE @CodigoFormulario INT 
    SET @CodigoFormulario = {0}
			
 SELECT fa.CodigoFormularioAssinatura,
        fa.ImagemFormulario, 
		fa.BinarioAssinatura, 
		fa.DataAssinatura, 
		u.CPF
   FROM FormularioAssinatura AS fa INNER JOIN 
		Usuario AS u on fa.CodigoUsuario = u.CodigoUsuario
  WHERE fa.CodigoFormulario IN (SELECT CodigoFormulario FROM dbo.f_getAssinaturasFormulario(@CodigoFormulario))
  ORDER BY 
		fa.DataAssinatura DESC;
END", codigoFormulario);

        #endregion

        DataSet ds = cDados.getDataSet(comandoSql);
        long codigoOperacao = cDados.RegistraOperacaoCritica("VERIFICASS", codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel);
        RegistraLog(codigoOperacao, Resources.traducao.wfRenderizaFormulario_escopo_do_processo, "CodigoFormulario:" + codigoFormulario);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            int codigoFormularioAssinatura = (int)dr["CodigoFormularioAssinatura"];
            byte[] binarioAssinatura = (byte[])dr["BinarioAssinatura"];
            byte[] conteudoArquivo = (byte[])dr["ImagemFormulario"];
            DateTime data = (DateTime)dr["DataAssinatura"];
            string cpf = (string)dr["CPF"];
            try
            {
                RegistraLog(codigoOperacao, Resources.traducao.wfRenderizaFormulario_inicia_processo_de_requisi__o_de_verifica__o_de_assinatura, "CFA:" + codigoFormularioAssinatura);
                mensagemErro = ObtemMensagemVerificacaoValidadeAssinaturaArquivo(binarioAssinatura, conteudoArquivo);
                if (!string.IsNullOrEmpty(mensagemErro))
                {
                    RegistraLog(codigoOperacao, Resources.traducao.wfRenderizaFormulario_assinatura_inv_lida, "CFA:" + codigoFormularioAssinatura);
                    cDados.RegistraFalhaOperacaoCritica(codigoOperacao, "AssinaturaInvalida", mensagemErro);
                    return false;
                }
            }
            catch (Exception ex)
            {
                mensagemErro = Resources.traducao.wfRenderizaFormulario_n_o_foi_poss_vel_verificar_a_autenticidade_da_assinatura_digital_do_documento_devido_a_um_erro__n;
                mensagemErro += ex.Message;
                RegistraLog(codigoOperacao, Resources.traducao.wfRenderizaFormulario_exce__o_gerada_ao_assinar, "CFA:" + codigoFormularioAssinatura);
                cDados.RegistraFalhaOperacaoCritica(codigoOperacao, "ExcessaoVerifAss", mensagemErro);
                return false;
            }
        }
        cDados.FinalizaOperacaoCritica(codigoOperacao);
        return true;
    }

    private string ObtemMensagemVerificacaoValidadeAssinaturaArquivo(byte[] binarioAssinatura, byte[] conteudoArquivo)
    {
        var mensagem = string.Empty;
        Integrador integrador = ObtemInstanciaIntegradorCertillion();
        ValidateRespType validateResponse = integrador.ValidarAssinatura(conteudoArquivo, binarioAssinatura);
        if (validateResponse.ErrorSpecified)
        {
            string motivo = validateResponse.Error.ToString();
            mensagem = ObtemMensagemFalhaValidacao(motivo);
        }
        else
        {
            SignatureInfoType signarute = validateResponse.Signatures.Single();
            if (!signarute.Valid)
            {
                string motivo = string.Join(Environment.NewLine, signarute.InvalidReason);
                mensagem = ObtemMensagemFalhaValidacao(motivo);
            }
        }

        return mensagem;
    }

    private static string ObtemMensagemFalhaValidacao(string motivo)
    {
        return string.Format(Resources.traducao.wfRenderizaFormulario_n_o_foi_poss_vel_verificar_a_autenticidade_da_assinatura_digital_do_documento__1__motivo___0_, motivo, Environment.NewLine);
    }

    private Integrador ObtemInstanciaIntegradorCertillion()
    {
        var dr = cDados.getParametrosSistema(
            "SUCC_URLWebServiceUploadArquivosAssinados",
            "SUCC_URLWebServiceCertillion").Tables[0].Rows[0];
        var fileUploadServiceUrl = dr.Field<string>("SUCC_URLWebServiceUploadArquivosAssinados");
        var fileSigningServiceUrl = dr.Field<string>("SUCC_URLWebServiceCertillion");
        var integrador = new Integrador(fileUploadServiceUrl, fileSigningServiceUrl);
        return integrador;
    }

    public static bool AcceptAllCertifictions(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    private HttpWebRequest GetRequest()
    {
        ServicePointManager.ServerCertificateValidationCallback =
            new RemoteCertificateValidationCallback(AcceptAllCertifictions);
        string nomeParametro = "enderecoWebServiceAssinaturaDigital";
        DataSet dsParam = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, nomeParametro);
        object valorParametro = dsParam.Tables[0].Rows[0][nomeParametro];
        string endrecoWebServiceAssinaturaDigital = valorParametro + "verify";
        //"https://179.124.38.222:8543/cdis/ws-sign/signature/verify";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endrecoWebServiceAssinaturaDigital);
        request.ContentType = "application/json; charset=UTF-8";
        request.Accept = "application/json";
        request.Method = "POST";
        return request;
    }

    private string ObtemStringRequisicao(byte[] binarioAssinatura, byte[] conteudoArquivo, DateTime data, string cpf)
    {
        string dataSigned = Convert.ToBase64String(conteudoArquivo);
        string signature = Convert.ToBase64String(binarioAssinatura);
        double unixTime = (data.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;

        var requestBody = new
        {
            dataSigned = dataSigned,
            signature = signature,
            signatureReceptionTimes = new[] {
                new {
                    cpf = cpf,
                    receptionTime = unixTime
                }
            }
        };
        return new JavaScriptSerializer().Serialize(requestBody);
    }

    private void RegistraLog(long codigoOperacao, string descricao, string contexto = null)
    {
        cDados.RegistraPassoOperacaoCritica(codigoOperacao, descricao, contexto);
    }

    protected void pcVersoes_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
    {
        pcVersoes.JSProperties["cp_TextoVersao"] = string.Format("{0} {1}", Resources.traducao.wfRenderizaFormulario_vers_o, versaoFormulario);
    }
}