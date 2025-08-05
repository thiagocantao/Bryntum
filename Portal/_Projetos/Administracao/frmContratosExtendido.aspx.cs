/*OBSERVAÇÕES
 * 
 * MODIFICAÇÕES
 * 
 * 01/03/2011 :: Alejandro : -Alteração no método 'carregaComboFontePagadora()', aonde agora indicara o código da
 *                          entidade logada para filtrar as fontes pagadoras correspondientes.
 *                          -Alteração do desenho da grid para obter o padron.
 * 
 * 17/03/2011 :: Alejandro : adiciono el botão de Permissãos para os contratos.
 * 21/03/2011 :: Alejandro : adiciono el control de acesso para o botão de permissões [CT_AdmPrs].
 */
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
using DevExpress.XtraPrinting;
using System.IO;
using System.Drawing;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using System.Collections.Generic;

public partial class _Projetos_DadosProjeto_frmContratos : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";

    public bool podeEditar = false;
    public bool podeIncluir = false;
    public bool podeExcluir = false;
    public bool podePermissao = false;
    public bool podeEncerrar = false;
    public bool podeAlterarNumero = false;
    public string mostraStatus = "";
    public string alturaFrames = "";

    bool permissaoProjeto = true;

    private int codigoProjeto = -1;

    string utilizaCalculoSaldoPorValorPago = "N";

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

        defineParametros();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (Request.QueryString["ID"] != null)
        {
            codigoProjeto = int.Parse(Request.QueryString["ID"].ToString());
            cDados.verificaPermissaoProjetoInativo(codigoProjeto, ref permissaoProjeto, ref permissaoProjeto, ref permissaoProjeto);
        }

        podeIncluir = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "UN", "UN_IncCtt") && permissaoProjeto;

        gvDados.JSProperties["cp_Msg"] = "";

        gvDados.JSProperties["cp_CodigoProjeto"] = codigoProjeto;

        carregaGvDados();

        if (!IsPostBack)
        {

            definePermissoes();
            inicializaFiltros();

        }

        cDados.aplicaEstiloVisual(Page);

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/ListaContratosExtendido.js""></script>"));
        this.TH(this.TS("ListaContratosExtendido"));
        this.Title = cDados.getNomeSistema();

        gvDados.JSProperties["cp_PodeAlterarProjeto"] = (Request.QueryString["PodeAlterarProjeto"] + "") == "" ? "S" : (Request.QueryString["PodeAlterarProjeto"] + "");
    }

    #region INICIALIZAÇÃO

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 100);

        alturaFrames = (alturaPrincipal - 100).ToString();

        gvDados.JSProperties["cp_Altura"] = (alturaPrincipal - 180).ToString(); ;
        gvDados.Width = new Unit("100%");
    }

    private void definePermissoes()
    {
        bool bPodeAcessarTela;
        /// se houver algum contrato que o usuário pode consultar
        bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "CT", "CT_Cns");

        /// se ainda não foi determinado que pode acessar, 
        /// verifica se há alguma unidade em que o usuário possa incluir contratos
        if (bPodeAcessarTela == false)
            bPodeAcessarTela = podeIncluir;

        /// se ainda não foi determinado que pode acessar, 
        /// verifica se há alguma unidade em que o usuário possa consultar contratos, mesmo que não exista nenhum
        if (bPodeAcessarTela == false)
            bPodeAcessarTela = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "UN", "CT_Cns");

        // se não puder, redireciona para a página sem acesso
        if (bPodeAcessarTela == false)
            cDados.RedirecionaParaTelaSemAcesso(this);
    }

    private void inicializaFiltros()
    {
        if (Request.QueryString["DiasVencimento"] != null && Request.QueryString["DiasVencimento"].ToString() != "")
        {
            int diasVencimento = int.Parse(Request.QueryString["DiasVencimento"].ToString());
            DateTime dataVencimento = DateTime.Now.AddDays(diasVencimento);

            gvDados.FilterExpression = string.Format("[SituacaoContrato] = 'Ativo' AND [DataTermino] <= #{0:yyyy-MM-dd}# AND [DataTermino] >= #{1:yyyy-MM-dd}# AND [DataTermino] <> ''", dataVencimento, DateTime.Now);
            //gvDados.Columns[""].
        }

        if (Request.QueryString["Vencidos"] != null && Request.QueryString["Vencidos"].ToString() == "S")
        {
            DateTime dataVencimento = DateTime.Now;

            gvDados.FilterExpression = string.Format("[SituacaoContrato] = 'Ativo' AND [DataTermino] < #{0:yyyy-MM-dd}# AND [DataTermino] <> ''", dataVencimento);
            //gvDados.Columns[""].
        }

        if (Request.QueryString["ApenasMeusContratos"] != null && Request.QueryString["ApenasMeusContratos"].ToString() != "")
        {
            DataSet dsFiltro = cDados.getUsuarios(" AND u.CodigoUsuario = " + codigoUsuarioResponsavel);

            if (cDados.DataSetOk(dsFiltro) && cDados.DataTableOk(dsFiltro.Tables[0]))
            {
                string nomeUsuario = dsFiltro.Tables[0].Rows[0]["NomeUsuario"].ToString();

                if (gvDados.FilterExpression != "")
                    gvDados.FilterExpression += " AND [SituacaoContrato] = 'Ativo' AND  [GestorContrato] = '" + nomeUsuario + "'";
                else
                    gvDados.FilterExpression = " [SituacaoContrato] = 'Ativo' AND  [GestorContrato] = '" + nomeUsuario + "'";
            }
        }

        int codigoMunicipio = -1;

        if (Request.QueryString["CodigoMunicipio"] != null && Request.QueryString["CodigoMunicipio"].ToString() != "")
        {
            codigoMunicipio = int.Parse(Request.QueryString["CodigoMunicipio"].ToString());
        }

        string status = "";

        if (Request.QueryString["Situacao"] != null && Request.QueryString["Situacao"].ToString() != "")
        {
            status = Request.QueryString["Situacao"].ToString();
        }

        if (codigoMunicipio != -1)
        {
            string nomeMunicipio = "";

            DataSet dsMunicipio = cDados.getMunicipios(" AND CodigoMunicipio = " + codigoMunicipio);

            if (cDados.DataSetOk(dsMunicipio) && cDados.DataTableOk(dsMunicipio.Tables[0]))
            {
                nomeMunicipio = dsMunicipio.Tables[0].Rows[0]["NomeMunicipio"].ToString();

                if (gvDados.FilterExpression != "")
                    gvDados.FilterExpression += " AND [NomeMunicipio] = '" + nomeMunicipio + "'";
                else
                    gvDados.FilterExpression = " [NomeMunicipio] = '" + nomeMunicipio + "'";
            }
        }

        if (status != "")
        {
            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += " AND [SituacaoContrato] = '" + status + "'";
            else
                gvDados.FilterExpression = " [SituacaoContrato] = '" + status + "'";
        }

        if (Request.QueryString["IndicaVigentesAno"] != null && Request.QueryString["IndicaVigentesAno"].ToString() == "S")
        {
            int anoParam = cDados.getInfoSistema("AnoPainelContrato") == null || cDados.getInfoSistema("AnoPainelContrato").ToString() == "-1" ? DateTime.Now.Year : int.Parse(cDados.getInfoSistema("AnoPainelContrato").ToString());

            if (gvDados.FilterExpression != "")
                gvDados.FilterExpression += string.Format(" AND [AnoTermino] = {0}", anoParam);
            else
                gvDados.FilterExpression += string.Format("[AnoTermino] = {0}", anoParam);
        }
    }

    private void defineParametros()
    {
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "calculaSaldoContratualPorValorContrato");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["calculaSaldoContratualPorValorContrato"].ToString() != "")
            {
                utilizaCalculoSaldoPorValorPago = dsParametros.Tables[0].Rows[0]["calculaSaldoContratualPorValorContrato"].ToString();
            }
        }
    }

    #endregion

    #region GRID

    private void carregaGvDados()
    {
        string strWhere = "";

        if (codigoProjeto != -1)
            strWhere = " AND CodigoProjeto = " + codigoProjeto;

        DataSet ds = cDados.getListaContratosEstendidos(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, strWhere);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.ToString() == "C")
        {
            int codigoContrato = int.Parse(getChavePrimaria());

            bool resultado = cDados.atualizaStatusContratoExtendido(codigoContrato, "I", txtComentarioEncerramento.Text, codigoUsuarioResponsavel);

            if (resultado)
            {
                gvDados.JSProperties["cp_Msg"] = "Contrato encerrado com sucesso!";
                carregaGvDados();
            }
            else
                gvDados.JSProperties["cp_Msg"] = "Erro ao encerrar o contrato!";
        }
        else if (e.Parameters.ToString() == "A")
        {
            if (gvDados.FocusedRowIndex >= 0)
            {
                string codigoProjeto = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoProjeto") + "";
                if (codigoProjeto != "")
                {
                    try
                    {
                        string msgErro = "";
                        cDados.atualizaStatusProjeto(int.Parse(codigoProjeto), codigoUsuarioResponsavel, ref msgErro);
                    }
                    catch { }
                }
            }
        }
        else
        {
            try
            {
                int cod = int.Parse(e.Parameters.ToString());
                gvDados.JSProperties["cp_Msg"] = persisteExclusaoRegistro(cod);
            }
            catch { }
        }
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

        if (e.Column.FieldName == "AnoTermino")
            ((ASPxTextBox)e.Editor).HorizontalAlign = HorizontalAlign.Center;
    }

    public string getBotoes()
    {
        string tabelaBotoes = "";
        string botaoEditar = "";
        string botaoPermissao = "";
        string botaoDetalhes = "";
        string botaoExcluir = "";

        if (Eval("Permissoes") != null)
        {
            int permissoes = int.Parse(Eval("Permissoes").ToString());

            podeEditar = (permissoes & 2) > 0 && permissaoProjeto;
            podePermissao = (permissoes & 4) > 0 && permissaoProjeto;
            podeExcluir = (bool)Eval("PodeExcluirContrato") && permissaoProjeto;

            bool indicaContratoPossuiVinculo =
                Eval("ContratoPossuiVinculo").ToString() == "S";

            botaoEditar = !podeEditar ? @"<img src='../../imagens/botoes/editarRegDes.png' />" :
                string.Format(@"<img src='../../imagens/botoes/editarReg02.png' style='cursor:pointer' alt='Editar' onclick=""abreDetalhes({0}, {1}, 'N')"" />"
                , Eval("CodigoContrato")
                , Eval("CodigoProjeto") != null && Eval("CodigoProjeto").ToString() != "" ? Eval("CodigoProjeto") : "-1");

            botaoExcluir = !podeExcluir || indicaContratoPossuiVinculo ? @"<img src='../../imagens/botoes/excluirRegDes.png' />" :
                string.Format(@"<img src='../../imagens/botoes/excluirReg02.png' style='cursor:pointer' alt='Excluir' onclick=""excluiContrato({0})"" />", Eval("CodigoContrato"));

            botaoDetalhes = string.Format(@"<img src='../../imagens/botoes/pFormulario.png' style='cursor:pointer' alt='Detalhes' onclick=""abreDetalhes({0}, {1}, 'S')"" />"
                , Eval("CodigoContrato")
                , Eval("CodigoProjeto") != null && Eval("CodigoProjeto").ToString() != "" ? Eval("CodigoProjeto") : "-1");

            botaoPermissao = !podePermissao ? @"<img src='../../imagens/Perfis/Perfil_PermissoesDes.png' />" :
                string.Format(@"<img src='../../imagens/Perfis/Perfil_Permissoes.png' style='cursor:pointer' alt='Permissões' onclick=""abrePermissoes({0}, '{1}')"" />"
                , Eval("CodigoContrato")
                , Eval("NumeroContrato"));
        }

        tabelaBotoes = string.Format(@"<table cellSpacing=0 cellPadding=0 width=""100%"" border=0>
                                        <tr>
                                            <td>{0}</td>
                                            <td>{1}</td>
                                            <td>{2}</td>
                                            <td>{3}</td>
                                        </tr>
                                     </table>", botaoEditar, botaoExcluir, botaoDetalhes, botaoPermissao);

        return tabelaBotoes;
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        GridViewDataColumn coluna = e.DataColumn;
        if (coluna.FieldName == "DescricaoObjetoContrato")
        {
            if (e.CellValue.ToString().Length > 285)
            {
                e.Cell.ToolTip = e.CellValue.ToString();
                e.Cell.Text = e.CellValue.ToString().Substring(0, 285) + "...";
            }
        }
    }

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    public string getValorAditadoGrid(string valorCAditivos, string valorOriginal)
    {
        string novoValorCAditivo = "&nbsp;";

        if (valorCAditivos != "" && (valorCAditivos != valorOriginal || Eval("TemAditivoTEC").ToString() == "S"))
        {
            novoValorCAditivo = string.Format("{0:c2}", double.Parse(valorCAditivos));
        }

        return novoValorCAditivo;
    }

    public string getSaldoContratual()
    {
        string valorCAditivos = Eval("ValorContrato").ToString();
        string ValorMedido = utilizaCalculoSaldoPorValorPago == "S" ? Eval("ValorPago").ToString() : Eval("ValorMedido").ToString();

        if (valorCAditivos == "" || ValorMedido == "")
            return "";
        else
            return string.Format("{0:c2}", (double.Parse(valorCAditivos) - double.Parse(ValorMedido)));
    }

    #endregion

    #region Exclusão

    private string persisteExclusaoRegistro(int cod) // Método responsável pela Exclusão do registro
    {
        string msg = "";
        string chave = cod.ToString();
        bool retorno = false;
        try
        {
            retorno = cDados.excluiContratoAquisicoes(chave, cDados.getCodigoTipoAssociacao("CT"), ref msg);
            carregaGvDados();
        }
        catch
        {
            msg = Resources.traducao.frmContratosExtendido_houve_um_erro_ao_excluir_o_registro__entre_em_contato_com_o_administrador_do_sistema_;
        }

        if (retorno)
            msg = Resources.traducao.frmContratosExtendido_contrato_exclu_do_com_sucesso_;

        return msg;
    }

    #endregion

    protected void gvExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == GridViewRowType.Group)
        {
            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }
        }
        if (e.Column.Name.ToString() == "SaldoContratual")
        {
            e.BrickStyle.TextAlignment = TextAlignment.MiddleRight;

            if (e.KeyValue != null && e.KeyValue.ToString() != "")
            {
                string keyValue = e.KeyValue.ToString();
                string valorMedido = utilizaCalculoSaldoPorValorPago == "S" ? gvDados.GetRowValuesByKeyValue(int.Parse(keyValue), "ValorPago") + "" : gvDados.GetRowValuesByKeyValue(int.Parse(keyValue), "ValorMedido") + "";
                string valorContrato = gvDados.GetRowValuesByKeyValue(int.Parse(keyValue), "ValorContrato") + "";

                if (valorMedido != "" && valorContrato != "")
                    e.TextValue = double.Parse(valorContrato) - double.Parse(valorMedido);

                //e.Text = getSaldoContratual(valorContrato, valorMedido);
            }
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        carregaGvDados();

        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
            string nomeArquivo = "", app = "", erro = "";

            try
            {
                nomeArquivo = "Contratos_" + dataHora + ".xls";
                XlsExportOptionsEx x = new XlsExportOptionsEx();

                gvExporter.WriteXls(stream, x);
                //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                app = "application/ms-excel";
            }
            catch
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", app);
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
                Response.BinaryWrite(stream.GetBuffer());
                Response.End();
            }
            else
            {
                string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem(traducao.frmContratosExtendido_erro_ao_exportar_os_dados__verifique_se_n_o_foi_ultrapassado_o_n_mero_m_ximo_de_256_colunas_, 'erro', true, false, null);                                   
                                 </script>";

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }
}