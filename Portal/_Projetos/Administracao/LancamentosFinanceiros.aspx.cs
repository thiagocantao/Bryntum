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
using System.IO;
using DevExpress.XtraPrinting;

public partial class LancamentosFinanceiros : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    public string where_Atrasados = "";

    private string resolucaoCliente = "";

    public bool podeEditar = true;

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

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            //Master.geraRastroSite();
            //Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "LACFIN", "ENT", -1, "Adicionar aos Favoritos");

            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
        }

        podeEditar = true; //só virão registros sob a responsabilidade do usuário, podeEditar = true


        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
        {
            podeEditar = false;
        }

        if (Request.QueryString["Atrasados"] != null && Request.QueryString["Atrasados"].ToString() == "S")
        {
            where_Atrasados = "S";
        }
        cDados.aplicaEstiloVisual(Page);

        carregaGvDados();
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/LancamentosFinanceiros.js""></script>"));
        //Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));

    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 305;
    }



    #endregion

    #region GRID

    private void carregaGvDados()
    {
        string orderby = " order by lf.DataPagamentoRecebimento desc";
        DataSet ds = getEmpenhosFinanceirosProjetoAlternativo(orderby);

        if ((cDados.DataSetOk(ds)))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    public DataSet getEmpenhosFinanceirosProjetoAlternativo(string orderby)
    {
        //venceu dia 3 e  hoje é dia 20 e a data de pagamento ainda esta nula
        //atrasado significa que a data de vencimento é menor que a data atual
        //se a DataVencimento < dataatual && DataPagamentoRecebimento == null
        string where = "";
        if (where_Atrasados == "S")
        {
            where = " AND DATEADD(DD, 1, [DataVencimento]) < GETDATE() ";
        }
        string comandoSQL = string.Format(@"            
        DECLARE @CodigoEntidade int, 
                @CodigoProjeto int, 
                @CodigoUsuario int, 
                @IndicaEmpenhoPagamento char(1),
                @IndicaResponsabilidadeUsuario char(1)

                SET @CodigoEntidade = {0}
                SET @CodigoProjeto = NULL
                SET @CodigoUsuario = {2}
                SET @IndicaEmpenhoPagamento = 'P'
                SET @IndicaResponsabilidadeUsuario = 'S'

                SELECT 
                    CodigoLancamentoFinanceiro,
                    IniciaisControleLancamento,
                    DataVencimento,
                    DataEmpenho,
                    DataPrevistaPagamentoRecebimento,
                    IndicaDespesaReceita,
                    PessoaEmitente,
                    DescricaoConta,
                    NomeParticipe,
                    ValorEmpenhado,
                    NumeroDocFiscal,
                    DataEmissaoDocFiscal,
                    NomeProjeto
               FROM [dbo].[f_gestconv_GetLancamentosFinanceiros] (@CodigoEntidade, @CodigoProjeto, @CodigoUsuario, @IndicaEmpenhoPagamento, @IndicaResponsabilidadeUsuario)
               WHERE 1=1
                     {3}
                     ORDER BY DataPagamentoRecebimento DESC, DataPrevistaPagamentoRecebimento ASC
            ", codigoEntidadeUsuarioResponsavel, 0, codigoUsuarioResponsavel, where);

        return cDados.getDataSet(comandoSQL);
    }

    #endregion

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "atualizaGrid")
        {
            carregaGvDados();
        }
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    #endregion

    public string getDescricaoStatus()
    {
        switch (Eval("IndicaAprovacaoReprovacao").ToString())
        {
            case "A": return "Aprovado";
            case "R": return "Reprovado";
            default: return "Pendente";
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
                nomeArquivo = "Lancamentos_Financeiros_" + dataHora + ".xls";
                XlsExportOptionsEx x = new XlsExportOptionsEx();

                gvExporter.WriteXls(stream, x);
                //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                app = "application/ms-excel";
            }
            catch
            {
                erro = "S";
            }
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

        e.Text = e.Text.Replace("<br>", "");

        if (e.RowType == DevExpress.Web.GridViewRowType.Group)
        {
            e.BrickStyle.Font = new System.Drawing.Font("Verdana", 8.0f, System.Drawing.FontStyle.Bold);

            if (e.Text.IndexOf(':') != -1)
            {
                string DescricaoColuna = e.Text.Substring(0, e.Text.IndexOf(':'));
                string strValue = System.Text.RegularExpressions.Regex.Replace(DescricaoColuna + ": " + e.Value, @"<[^>]*>", " ");
                e.TextValue = strValue;
                e.Text = strValue;
            }

        }

        if (e.RowType == DevExpress.Web.GridViewRowType.Header)
        {
            if (e.Column.Name == "colValorEmpenhado")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleRight;
            }
            if (e.Column.Name == "colValorPagamentoRecebimento")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleRight;
            }
            if (e.Column.Name == "colNumeroDocFiscal")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleLeft;
            }
            if (e.Column.Name == "colEmitente")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleLeft;
            }
            if (e.Column.Name == "colConta")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleLeft;
            }
        }

        if (e.RowType == DevExpress.Web.GridViewRowType.Data)
        {
            if (e.Column.Name == "colDataPagamentoRecebimento")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            }

            if (e.Column.Name == "colDataPrevistaPagamentoRecebimento")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            }

            if (e.Column.Name == "colTipo")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
                if (e.Value != null)
                {
                    if (e.Value.ToString() == "D")
                    {
                        e.Text = "Despesa";
                        e.TextValue = "Despesa";
                    }
                    else
                    {
                        e.Text = "Receita";
                        e.TextValue = "Receita";
                    }
                }
            }

            if (e.Column.Name == "colStatus")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
                if (e.Value != null)
                {
                    if (e.Value.ToString() == "R")
                    {
                        e.Text = "Reprovado";
                        e.TextValue = "Reprovado";
                    }
                    else
                    {
                        e.Text = "Aprovado";
                        e.TextValue = "Aprovado";
                    }
                }
            }

            if (e.Column.Name == "colDataEmpenho")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            }

            if (e.Column.Name == "colPendente")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleCenter;
            }
        }
    }

    protected void gvDados_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
    {
        if (e.Column.FieldName == "IndicaAprovacaoReprovacao")
        {
            string displayText;
            switch (e.Value as string)
            {
                case "A":
                    displayText = "Aprovado";
                    break;
                case "R":
                    displayText = "Reprovado";
                    break;
                default:
                    displayText = "Pendente";
                    break;
            }
            e.DisplayText = displayText;
        }

        if (e.Column.FieldName == "IndicaDespesaReceita")
        {

            string displayText;
            switch (e.Value as string)
            {
                case "R":
                    displayText = "Receita";
                    break;
                case "D":
                    displayText = "Despesa";
                    break;
                default:
                    displayText = "";
                    break;
            }
            e.DisplayText = displayText;
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporter, "LncFinUsr");
    }

    #endregion

}
