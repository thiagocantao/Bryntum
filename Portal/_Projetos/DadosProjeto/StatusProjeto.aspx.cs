using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_StatusProjeto : System.Web.UI.Page
{
    dados cDados;
    public int codigoProjeto = 0;

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

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/FusionCharts.js?v=1""></script>"));
        this.TH(this.TS("FusionCharts"));
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        preencheCampos();
        getPendencias();
        getRiscosProblemas();
        carregaGraficoPainel1();
        carregaGraficoPainel2();
        carregaGraficoPainel3();
        carregaGraficoPainel4();
    }

    #region Painel 1 - Resumo do Projeto

    private void preencheCampos()
    {
        DataSet dsDados = cDados.getDadosGeraisProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataTable dt = dsDados.Tables[0];

            DataSet ds = cDados.getParametrosSistema("labelGerente", "diasAtualizacaoProjeto");

            string labelResponsavel = "Responsável: ";
            double diasAtualizacao = -1;

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                labelResponsavel = ds.Tables[0].Rows[0]["labelGerente"] + "" != "" ? ds.Tables[0].Rows[0]["labelGerente"] + ": " : "Responsável: ";

                if (ds.Tables[0].Rows[0]["diasAtualizacaoProjeto"] + "" != "")
                {
                    diasAtualizacao = double.Parse(ds.Tables[0].Rows[0]["diasAtualizacaoProjeto"] + "");
                }
            }

            //Preenche os campos de texto de Dados do Projeto
            lblUnidade.Text = "UN: " + (dt.Rows[0]["Unidade"] + "" == "" ? "---" : dt.Rows[0]["Unidade"].ToString());
            lblInicio.Text = "Início: " + (dt.Rows[0]["InicioReprogramado"] + "" == "" ? "---" : dt.Rows[0]["InicioReprogramado"].ToString());
            lblTermino.Text = "Término: " + (dt.Rows[0]["TerminoReprogramado"] + "" == "" ? "---" : dt.Rows[0]["TerminoReprogramado"].ToString());
            lblInicioLB.Text = "Início LB: " + (dt.Rows[0]["DataInicio"] + "" == "" ? "---" : dt.Rows[0]["DataInicio"].ToString());
            lblTerminoLB.Text = "Término LB: " + (dt.Rows[0]["DataTermino"] + "" == "" ? "---" : dt.Rows[0]["DataTermino"].ToString());
            lblResponsavel.Text = labelResponsavel + (dt.Rows[0]["Gerente"] + "" == "" ? "---" : dt.Rows[0]["Gerente"].ToString());
            lblAtualizacao.Text = dt.Rows[0]["UltimaAtualizacao"] + "" == "" ? "Atualização: --/--/----" : "Atualização: " + dt.Rows[0]["UltimaAtualizacao"].ToString();

            if (dt.Rows[0]["DataUltimaAtualizacao"] != null && dt.Rows[0]["DataUltimaAtualizacao"].ToString() != "" && ((DateTime)dt.Rows[0]["DataUltimaAtualizacao"]).AddDays(diasAtualizacao) < DateTime.Now)
                lblAtualizacao.ForeColor = Color.Red;

            formataLinkDataAtualizacao();
        }
    }

    private void formataLinkDataAtualizacao()
    {
        DataSet dsDados = cDados.getItensProjetoAtualizacaoMonitorada(codigoProjeto, "");
        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            if (dsDados.Tables[0].Rows.Count > 1)
            {
                lblAtualizacao.Style.Add("cursor", "pointer");
                lblAtualizacao.Font.Underline = true;
                lblAtualizacao.ClientSideEvents.Click = "function(s,e) {pcEventosAtlPrj.Show();}";
                gvEventosAtlPrj.Settings.ShowFilterRow = false;
                gvEventosAtlPrj.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
                gvEventosAtlPrj.SettingsLoadingPanel.Mode = DevExpress.Web.GridViewLoadingPanelMode.Disabled;
                gvEventosAtlPrj.DataSource = dsDados;
                gvEventosAtlPrj.DataBind();
            }
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "FormAtlPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "FormAtlPrj", "Atualização Projeto", this);
    }

    #endregion

    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
    }

    #endregion

    #region Painel 2 - Informações de Cronograma, Riscos e Problemas

    private void getPendencias()
    {
        string definicaoToDoList = "To Do List";

        DataSet dsParametros = cDados.getParametrosSistema("labelToDoList");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelToDoList"] + "" != "")
            definicaoToDoList = dsParametros.Tables[0].Rows[0]["labelToDoList"] + "";

        DataSet dsDados = cDados.getPendenciasProjeto(codigoProjeto, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataTable dt = dsDados.Tables[0];

            int cronograma = 0, toDoList = 0, entregas = 0;

            cronograma = int.Parse(dt.Rows[0]["AtrasosCronograma"].ToString());
            toDoList = int.Parse(dt.Rows[0]["AtrasosToDoList"].ToString());
            entregas = int.Parse(dt.Rows[0]["EntregasAtrasadas"].ToString());

            hlCronograma.Text = "Atrasos Cronograma(" + cronograma + ")";

            hlToDoList.Text = "Atrasos " + definicaoToDoList + "(" + toDoList + ")";

            DataSet dsParametro = cDados.getParametrosSistema("lblEntregasAtrasadas");
            if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
            {
                hlEntregas.Text = dsParametro.Tables[0].Rows[0]["lblEntregasAtrasadas"].ToString() + "(" + entregas + ")";
            }
            else
            {
                hlEntregas.Text = "Entregas Atrasadas(" + entregas + ")";
            }


            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsCrn"))
            {
                hlCronograma.NavigateUrl = "./Cronograma_gantt.aspx?Atrasadas=S&IDProjeto=" + codigoProjeto;
                DataSet ds = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "linkCronograma");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    hlCronograma.NavigateUrl = VirtualPathUtility.ToAbsolute("~/") + ds.Tables[0].Rows[0]["linkCronograma"].ToString() + "?Atrasadas=S&IDProjeto=" + codigoProjeto;
                }
            }

            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsTdl"))
                hlToDoList.NavigateUrl = "./TarefasToDoList.aspx?Estagio=Atrasada&IDProjeto=" + codigoProjeto;

            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsMsg"))
            {
                hlEntregas.NavigateUrl = "./Cronograma_gantt.aspx?ApenasMarcos=S&Atrasadas=S&IDProjeto=" + codigoProjeto;
                
                DataSet ds = cDados.getParametrosSistema(int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), "linkCronograma");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    hlEntregas.NavigateUrl = VirtualPathUtility.ToAbsolute("~/") + ds.Tables[0].Rows[0]["linkCronograma"].ToString() + "?ApenasMarcos=S&Atrasadas=S&IDProjeto=" + codigoProjeto;
                }
            }

            if (cronograma > 0)
                hlCronograma.ForeColor = Color.Red;
            if (toDoList > 0)
                hlToDoList.ForeColor = Color.Red;
            if (entregas > 0)
                hlEntregas.ForeColor = Color.Red;
        }
        else
        {
            hlCronograma.Text = "Atrasos Cronograma(0)";

            hlToDoList.Text = "Atrasos " + definicaoToDoList + "(0)";

            DataSet dsParametro = cDados.getParametrosSistema("lblEntregasAtrasadas");
            if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
            {
                hlEntregas.Text = dsParametro.Tables[0].Rows[0]["lblEntregasAtrasadas"].ToString() + "(0)";
            }
            else
            {
                hlEntregas.Text = "Entregas Atrasadas(0)";
            }
        }
    }

    private void getRiscosProblemas()
    {
        string labelQuestao = "Questão", labelQuestoesAtivas = " Questões Ativas", labelQuestaoAtiva = " Questão Ativa";
        string labelQuestoesEliminadas = " Questões Eliminadas", labelQuestaoEliminada = " Questão Eliminada";
        string labelNenhuma = "Nenhuma";
        string labelQuestoes = "Questões";
        string generoQuestao = "F";
        DataSet dsDados = cDados.getQuantidadeRiscosProblemasProjeto(codigoProjeto, "");
        DataSet dsQuestoes = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");

        if (cDados.DataSetOk(dsQuestoes) && cDados.DataTableOk(dsQuestoes.Tables[0]))
        {
            labelQuestao = dsQuestoes.Tables[0].Rows[0]["labelQuestao"].ToString();
            labelQuestoes = dsQuestoes.Tables[0].Rows[0]["labelQuestoes"].ToString();
            generoQuestao = dsQuestoes.Tables[0].Rows[0]["lblGeneroLabelQuestao"].ToString();


            labelQuestoesAtivas = string.Format(@" {0} Ativ{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a");
            labelQuestaoAtiva = string.Format(@" {0} Ativ{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a"); //" Questão Ativa";
            labelQuestoesEliminadas = string.Format(@" {0} Eliminad{1}s", labelQuestoes, generoQuestao == "M" ? "o" : "a"); // Questões Eliminadas";
            labelQuestaoEliminada = string.Format(@" {0} Eliminad{1}", labelQuestao, generoQuestao == "M" ? "o" : "a");// " Questão Eliminada";
            labelNenhuma = generoQuestao == "M" ? "Nenhum" : "Nenhuma";
        }


        DataSet dsParametros = cDados.getParametrosSistema("labelQuestao");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataTable dt = dsDados.Tables[0];

            int riscosAtivos, riscosEliminados, problemasAtivos, problemasEliminados;

            riscosAtivos = int.Parse(dt.Rows[0]["RiscosAtivos"].ToString());
            riscosEliminados = int.Parse(dt.Rows[0]["RiscosEliminados"].ToString());
            problemasAtivos = int.Parse(dt.Rows[0]["ProblemasAtivos"].ToString());
            problemasEliminados = int.Parse(dt.Rows[0]["ProblemasEliminados"].ToString());

            hlRiscosAtivos.Text = (riscosAtivos > 0) ? "Riscos Ativos(" + riscosAtivos + ")" : "Riscos Ativos(0)";

            hlRiscosEliminados.Text = "Riscos Eliminados(" + riscosEliminados + ")";

            hlProblemasAtivos.Text = labelQuestoesAtivas + "(" + problemasAtivos + ")";

            hlProblemasEliminados.Text = labelQuestoesEliminadas + "(" + problemasEliminados + ")";

            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ1"))
            {
                if (riscosAtivos > 0)
                    hlRiscosAtivos.NavigateUrl = "./riscos.aspx?Publicado=SIM&TT=R&Status=Ativo&IDProjeto=" + codigoProjeto;

                if (riscosEliminados > 0)
                    hlRiscosEliminados.NavigateUrl = "./riscos.aspx?Publicado=SIM&TT=R&Status=Eliminado&IDProjeto=" + codigoProjeto;
            }
            if (cDados.VerificaPermissaoUsuario(int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()), int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString()), codigoProjeto, "null", "PR", 0, "null", "PR_CnsRQ2"))
            {
                if (problemasAtivos > 0)
                    hlProblemasAtivos.NavigateUrl = "./riscos.aspx?Publicado=SIM&TT=Q&Status=Ativo&IDProjeto=" + codigoProjeto;

                if (problemasEliminados > 0)
                    hlProblemasEliminados.NavigateUrl = "./riscos.aspx?Publicado=SIM&TT=Q&Status=Eliminado&IDProjeto=" + codigoProjeto;
            }
        }
        else
        {
            hlRiscosAtivos.Text = "Riscos Ativos(0)";
            hlRiscosEliminados.Text = "Riscos Eliminados(0)";
            hlProblemasAtivos.Text = labelQuestoesAtivas + "(0)";
            hlProblemasEliminados.Text = labelQuestoesEliminadas + "(0)";
        }
    }

    #endregion

    #region Gráficos

    private void carregaGraficoPainel1()
    {
        pn.Panes["pnGrupo2"].Panes["pnGrafico01"].ContentUrl = "./graficos/grafico_028.aspx?IDProjeto=" + codigoProjeto;
    }

    private void carregaGraficoPainel2()
    {
        pn.Panes["pnGrupo2"].Panes["pnGrafico02"].ContentUrl = "./graficos/grafico_029.aspx?IDProjeto=" + codigoProjeto;
    }

    private void carregaGraficoPainel3()
    {
        pn.Panes["pnGrupo3"].Panes["pnGrafico04"].ContentUrl = "./graficos/grafico_027.aspx?IDProjeto=" + codigoProjeto;
    }

    private void carregaGraficoPainel4()
    {
        pn.Panes["pnGrupo3"].Panes["pnGrafico05"].ContentUrl = "./graficos/grafico_030.aspx?IDProjeto=" + codigoProjeto;
    }

    #endregion
}