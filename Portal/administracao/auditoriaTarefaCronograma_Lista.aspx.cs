using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.Web;

public partial class administracao_auditoriaTarefaCronograma_Lista : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string resolucaoCliente = "";
    private DataTable dtGrid = new DataTable();
    private DataSet ds = new DataSet();

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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");
        
        carregaGvAuditoriaTarefaCronograma();

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
        }

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        this.Title = cDados.getNomeSistema();
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        gvDados.Settings.VerticalScrollableHeight = altura - 290;
    }

    private void carregaGvAuditoriaTarefaCronograma()
    {
        dtGrid.Columns.Clear();
        dtGrid.Columns.Add("Descricao");
        dtGrid.Columns.Add("Valor");
        dtGrid.Columns.Add("ValorOLD");
        dtGrid.Columns.Add("IndicaAlterado");

        ds = cDados.getListaAuditoriaTarefaCronograma("");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    public string getBotaoVisualizar()
    {
        
        string retorno = "";

        retorno = string.Format(@"<img alt='Visualizar' style='cursor:pointer' src='../imagens/botoes/pFormulario.png' onclick=""gvDetalhes.PerformCallback();"" />");

        return retorno;
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    private void carregaGvDetalhes(int index)
    {
        if (gvDados.FocusedRowIndex != -1)
        {
            string id = gvDados.GetRowValues(index, gvDados.KeyFieldName).ToString();

            DataRow[] dr = ds.Tables[0].Select("ID = " + id);

            insereLinhaTable("Projeto", dr[0]["NomeProjeto"].ToString(), dr[0]["NomeProjeto"].ToString(), (dr[0]["NomeProjeto"].ToString() != dr[0]["NomeProjeto"].ToString() ) ? "S" : "N");
            insereLinhaTable("Nome da Tarefa", dr[0]["NomeTarefa"].ToString(), dr[0]["oldNomeTarefa"].ToString(), (dr[0]["NomeTarefa"].ToString() != dr[0]["oldNomeTarefa"].ToString()) ? "S" : "N");
            insereLinhaTable("Duração", string.Format("{0:n2}", dr[0]["Duracao"]), string.Format("{0:n2}", dr[0]["oldDuracao"]), (dr[0]["Duracao"].ToString() != dr[0]["oldDuracao"].ToString()) ? "S" : "N");
            insereLinhaTable("Início", dr[0]["Inicio"].ToString(), dr[0]["oldInicio"].ToString(), (dr[0]["Inicio"].ToString() != dr[0]["oldInicio"].ToString()) ? "S" : "N");
            insereLinhaTable("Término", dr[0]["Termino"].ToString(), dr[0]["oldTermino"].ToString(), (dr[0]["NomeProjeto"].ToString() != dr[0]["NomeProjeto"].ToString()) ? "S" : "N");
            insereLinhaTable("Predecessoras", dr[0]["Predecessoras"].ToString(), dr[0]["oldPredecessoras"].ToString(), (dr[0]["Termino"].ToString() != dr[0]["oldTermino"].ToString()) ? "S" : "N");
            insereLinhaTable("% Concluído", dr[0]["PercentualFisicoConcluido"].ToString(), dr[0]["oldPercentualFisicoConcluido"].ToString(), (dr[0]["PercentualFisicoConcluido"].ToString() != dr[0]["oldPercentualFisicoConcluido"].ToString()) ? "S" : "N");
            insereLinhaTable("Duração Real", dr[0]["DuracaoReal"].ToString(), dr[0]["oldDuracaoReal"].ToString(), (dr[0]["DuracaoReal"].ToString() != dr[0]["oldDuracaoReal"].ToString()) ? "S" : "N");
            insereLinhaTable("Início Real", dr[0]["InicioReal"].ToString(), dr[0]["oldInicioReal"].ToString(), (dr[0]["InicioReal"].ToString() != dr[0]["oldInicioReal"].ToString()) ? "S" : "N");
            insereLinhaTable("Término Real", dr[0]["TerminoReal"].ToString(), dr[0]["oldTerminoReal"].ToString(), (dr[0]["TerminoReal"].ToString() != dr[0]["oldTerminoReal"].ToString()) ? "S" : "N");
            insereLinhaTable("Trabalho Real (hrs)", dr[0]["TrabalhoReal"].ToString(), dr[0]["oldTrabalhoReal"].ToString(), (dr[0]["TrabalhoReal"].ToString() != dr[0]["oldTrabalhoReal"].ToString()) ? "S" : "N");
            insereLinhaTable("Custo Real", dr[0]["CustoReal"].ToString(), dr[0]["oldCustoReal"].ToString(), (dr[0]["CustoReal"].ToString() != dr[0]["oldCustoReal"].ToString()) ? "S" : "N");
            insereLinhaTable("Início LB", dr[0]["InicioLB"].ToString(), dr[0]["oldInicioLB"].ToString(), (dr[0]["InicioLB"].ToString() != dr[0]["oldInicioLB"].ToString()) ? "S" : "N");
            insereLinhaTable("Término LB", dr[0]["TerminoLB"].ToString(), dr[0]["oldTerminoLB"].ToString(), (dr[0]["TerminoLB"].ToString() != dr[0]["oldTerminoLB"].ToString()) ? "S" : "N");
            insereLinhaTable("Duração LB", dr[0]["DuracaoLB"].ToString(), dr[0]["oldDuracaoLB"].ToString(), (dr[0]["DuracaoLB"].ToString() != dr[0]["oldDuracaoLB"].ToString()) ? "S" : "N");
            insereLinhaTable("Trabalho LB", dr[0]["TrabalhoLB"].ToString(), dr[0]["oldTrabalhoLB"].ToString(), (dr[0]["TrabalhoLB"].ToString() != dr[0]["oldTrabalhoLB"].ToString()) ? "S" : "N");

            gvDetalhes.DataSource = dtGrid;
            gvDetalhes.DataBind();

            gvDetalhes.JSProperties["cp_lblUsuario"] = dr[0]["UsuarioOperacao"].ToString();
            gvDetalhes.JSProperties["cp_lblData"] = dr[0]["DataOperacao"].ToString();
            gvDetalhes.JSProperties["cp_lblOperacao"] = dr[0]["Operacao"].ToString();
        }
    }

    private void insereLinhaTable(string descricao, string valor, string valorOld, string indicaAlterado)
    {
        DataRow dr = dtGrid.NewRow();

        dr["Descricao"] = descricao;
        dr["Valor"] = valor;
        dr["ValorOLD"] = valorOld;
        dr["IndicaAlterado"] = indicaAlterado;

        dtGrid.Rows.Add(dr);
    }

    protected void gvDetalhes_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters != "")
            carregaGvDetalhes(int.Parse(e.Parameters));
    }

    protected void gvDetalhes_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.GridViewRowType.Data)
        {
            string indicaAlterado = gvDetalhes.GetRowValues(e.VisibleIndex, "IndicaAlterado").ToString();

            if (indicaAlterado == "S")
            {
                e.Row.ForeColor = Color.Red;
            }
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //carregaGvAuditoriaTarefaCronograma();

        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
            string nomeArquivo = "", app = "", erro = "";

            try
            {
                nomeArquivo = "auditoriaTarefaCronograma_" + dataHora + ".xls";
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
                                    window.top.mostraMensagem('Erro ao exportar os dados. Verifique se não foi ultrapassado o número máximo de 256 colunas!', 'erro', true, false, null);                                   
                                 </script>";

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }
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
    }
}