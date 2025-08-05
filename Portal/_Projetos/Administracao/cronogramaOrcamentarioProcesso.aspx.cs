using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_Administracao_cronogramaOrcamentarioProcesso : System.Web.UI.Page
{
    dados cDados;
    ASPxGridView gvCampos_;
    DataSet dsValoresAcoes = new DataSet();

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string nomeProjeto;

    private string resolucaoCliente = "";

    int codigoProjeto = -1;
    public string somenteLeitura = "";

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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            cDados.aplicaEstiloVisual(Page);
        }

        if (Request.QueryString["CP"] != null)
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

        if (Request.QueryString["RO"] != null)
            somenteLeitura = Request.QueryString["RO"].ToString();

        if (Request.QueryString["ALT"] != null && Request.QueryString["ALT"].ToString() != "")
        {
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["ALT"].ToString());
        }        

        carregaGrid();
    }

    private void carregaGrid()
    {
        gvDados.JSProperties["cp_Msg"] = "";
        dsValoresAcoes = cDados.getValoresAcoesProjetoProcesso(codigoProjeto, "");
        nomeProjeto = cDados.getNomeProjeto(codigoProjeto, "").ToString();

        DataSet ds = cDados.getCronogramaOrcamentarioProcesso(codigoProjeto, "");

        gvDados.DataSource = ds;
        gvDados.DataBind();
        
    }

    public string getBotoesAtividade()
    {
        string descricao = "";

        bool podeIncluir = Eval("FonteRecurso").ToString() != "SR" && Eval("IndicaSemRecurso").ToString() != "S" && somenteLeitura != "S";

        descricao = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir Conta"" onclick=""incluiLinha({0}, {1})"" style=""cursor: pointer;""/>", Eval("CodigoAtividade"), codigoProjeto) : @"<img src=""../../imagens/botoes/incluirRegDes.png"" style=""cursor: default;""/>");

        return descricao;
    }

    public string getDescricaoAcao()
    {
        string descricao = "";

        string fonteRecursos = "";

        string valorAcao = "";

        if (cDados != null && cDados.DataSetOk(dsValoresAcoes))
        {
            DataRow[] dr = dsValoresAcoes.Tables[0].Select("CodigoAcao = " + Eval("CodigoAcao"));

            if (dr.Length > 0)
                valorAcao = string.Format("{0:n2}", dr[0]["Valor"]);
        }

        switch (Eval("FonteRecurso").ToString())
        {
            case "SR": fonteRecursos = "Sem Recursos";
                break;
            case "FU": fonteRecursos = "Fonte de Recursos: FUNDECOP";
                break;
            case "RP": fonteRecursos = "Fonte de Recursos: Unidade Nacional";
                break;
            default: fonteRecursos = "";
                break;
        }

        descricao = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" class=""headerGrid"" >
                                        <tr>
                                            <td>Ação: {0} - {1}</td>
                                            <td style='width:180px'>{2}</td>
                                            <td align='right' style='width:130px'>{3}</td>
                                        </tr>
                                    </table>", Eval("NumeroAcao"), Eval("NomeAcao"), fonteRecursos, valorAcao);

        return descricao;
    }

    public string getDescricaoAtividade()
    {
        string descricao = "";

        descricao = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" class=""headerGrid"">
                                        <tr>
                                            <td>Atividade: {0}.{1} - {2}</td>
                                        </tr>
                                    </table>", Eval("NumeroAcao"), Eval("NumeroAtividade"), Eval("NomeAtividade"));

        return descricao;
    }

    public string getTotalProjeto()
    {
        string descricao = "";
        
        object totalUN = null;
        object totalFdc = null;
        object totalProjeto = null;

        if (cDados != null && cDados.DataSetOk(dsValoresAcoes))
        {
            totalUN = dsValoresAcoes.Tables[0].Compute("Sum(Valor)", "FonteRecurso = 'RP'");
            totalFdc = dsValoresAcoes.Tables[0].Compute("Sum(Valor)", "FonteRecurso = 'FU'");
            totalProjeto = dsValoresAcoes.Tables[0].Compute("Sum(Valor)", "");
        }

        descricao = string.Format(@"<table cellpadding=""0"" cellspacing=""0"" class=""headerGrid""  >
                                        <tr>
                                            <td>Projeto: {0}</td>
                                            <td align='right' style='width:200px'>Total UN: {1:n2}</td>
                                            <td align='right' style='width:200px'>Total FDC: {2:n2}</td>
                                            <td align='right' style='width:220px'>Total Projeto: {3:n2}</td>
                                        </tr>
                                    </table>", nomeProjeto, totalUN, totalFdc, totalProjeto);

        return descricao;
    }

    private void carregaGvContas(int codigoAtividade)
    {
        DataSet ds = cDados.getContasAcoesProjetoProcesso(codigoAtividade, codigoProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gvCampos_.DataSource = ds;
            gvCampos_.DataBind();
        }
    }

    protected void gvContas_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "MemoriaCalculo" && e.CellValue != null && e.CellValue.ToString().Length > 60)
        {
            //string texto = e.CellValue.ToString().Replace("\"", "&quot;").Replace(Environment.NewLine, "<br>");

            //texto = texto.Replace("\n", "<BR>");

            //e.Cell.Attributes["onmouseover"] = "getToolTip(\"" + texto + "\")";// e.CellValue.ToString();
            ////e.Cell. e.CellValue.ToString();
            //e.Cell.Text = e.CellValue.ToString().Substring(0, 59) + "...";

            //e.Cell.ToolTip = e.CellValue.ToString();
            //e.Cell.Text = e.CellValue.ToString().Substring(0, 59) + "...";
            string texto = e.CellValue.ToString().Replace("\"", "&quot;").Replace(Environment.NewLine, "<br>");

            texto = texto.Replace("\n", "<BR>");

            e.Cell.Attributes["onmouseover"] = "getToolTip(\"" + texto + "\")";// e.CellValue.ToString();
            e.Cell.Attributes["onmouseout"] = "escondeToolTip()";// e.CellValue.ToString();
            e.Cell.Text = e.CellValue.ToString().Substring(0, 59) + "...";

        }
    }

    protected void gvContas_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {

    }

    protected void gvDados_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        if (e.Expanded)
        {
            preparaGvContas(e.VisibleIndex);
        }
    }

    protected void gvDados_DetailRowGetButtonVisibility(object sender, ASPxGridViewDetailRowButtonEventArgs e)
    {
        string possuiContas = gvDados.GetRowValues(e.VisibleIndex, "PossuiContas").ToString();
        string codigoAtividade = gvDados.GetRowValues(e.VisibleIndex, "CodigoAtividade").ToString();

        if (possuiContas == "S")
            e.ButtonState = GridViewDetailRowButtonState.Visible;
        else
        {
            gvDados.DetailRows.CollapseRow(e.VisibleIndex);
            e.ButtonState = GridViewDetailRowButtonState.Hidden;
        }
    }

    public string getBotoesContas()
    {
        string imgEdicao = "", imgExclusao = "";

        if (somenteLeitura != "S")
            imgEdicao = string.Format(@"<img alt='Editar Conta' onclick='editaConta({0}, {1}, {2})' src='../../imagens/botoes/editarReg02.PNG' style='cursor:pointer' />", Eval("SeqPlanoContas"), Eval("CodigoAcao"), codigoProjeto);
        else
            imgEdicao = string.Format(@"<img src='../../imagens/botoes/editarRegDes.png' />");

        if (somenteLeitura != "S")
            imgExclusao = string.Format(@"<img alt='Excluir Conta' onclick='excluiConta({0}, {1})' src='../../imagens/botoes/excluirReg02.PNG' style='cursor:pointer' />", Eval("SeqPlanoContas"), Eval("CodigoAcao"));
        else
            imgExclusao = string.Format(@"<img src='../../imagens/botoes/excluirRegDes.png' />");

        return string.Format("<table><tr><td>{0}</td><td>{1}</td></tr></table>", imgEdicao, imgExclusao);
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.ToString() == "ATL")
        {
            gvDados.DetailRows.ExpandRowByKey(int.Parse(hfGeral.Get("CodigoAtividade").ToString()));   
            preparaGvContas(gvDados.FindVisibleIndexByKeyValue(int.Parse(hfGeral.Get("CodigoAtividade").ToString())));
        }
        else
            if (e.Parameters.ToString() != "")
            {
                string codigoConta = e.Parameters.ToString().Split(';')[0];
                string codigoAtividade = e.Parameters.ToString().Split(';')[1];

                bool resultado = cDados.excluiContaAtividadeProcesso(int.Parse(codigoConta), int.Parse(codigoAtividade), codigoProjeto);

                if (resultado)
                {
                    
                    carregaGrid(); 
                    preparaGvContas(gvDados.FindVisibleIndexByKeyValue(int.Parse(hfGeral.Get("CodigoAtividade").ToString())));

                    if (gvCampos_ != null && gvCampos_.VisibleRowCount > 0)
                        gvDados.DetailRows.ExpandRowByKey(int.Parse(hfGeral.Get("CodigoAtividade").ToString()));
                    else
                    {
                        gvDados.DetailRows.ExpandRowByKey(-1);
                    }

                    gvDados.JSProperties["cp_Msg"] = "Conta removida do cronograma orçamentário com sucesso!";
                }
                else
                {
                    gvDados.JSProperties["cp_Msg"] = "Erro ao remover a conta do cronograma orçamentário!";
                }
            }
    }

    private void preparaGvContas(int indexMaster)
    {
        gvCampos_ = gvDados.FindDetailRowTemplateControl(indexMaster, "gvContas") as ASPxGridView;

        if (gvCampos_ != null)
        {
            cDados.aplicaEstiloVisual(gvCampos_);

            object objCodigo = gvCampos_.GetMasterRowKeyValue();

            if (objCodigo != null)
            {
                carregaGvContas(int.Parse(objCodigo.ToString()));
                gvCampos_.DataBind();
            }
        }
    }
}