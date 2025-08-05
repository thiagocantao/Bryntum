using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Drawing;
using DevExpress.XtraPrinting;
using System.Text.RegularExpressions;

public partial class _VisaoMaster_ListaIndicadores : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    bool podeConsultarAnalises = true;
    bool podeEditarAnalise = true;
    bool podeEditarPlanoAcao = true;
    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);

        if (!IsPostBack && !IsCallback)
        {
            hfDadosSessao.Set("CodigoEntidade", cDados.getInfoSistema("CodigoEntidade").ToString());
            hfDadosSessao.Set("Resolucao", cDados.getInfoSistema("ResolucaoCliente").ToString());
            hfDadosSessao.Set("IDUsuarioLogado", cDados.getInfoSistema("IDUsuarioLogado").ToString());
            hfDadosSessao.Set("IDEstiloVisual", cDados.getInfoSistema("IDEstiloVisual").ToString());
            hfDadosSessao.Set("NomeUsuarioLogado", cDados.getInfoSistema("NomeUsuarioLogado").ToString());
            hfDadosSessao.Set("CodigoCarteira", cDados.getInfoSistema("CodigoCarteira").ToString());
        }

        cDados.setInfoSistema("CodigoEntidade", hfDadosSessao.Get("CodigoEntidade").ToString());
        cDados.setInfoSistema("ResolucaoCliente", hfDadosSessao.Get("Resolucao").ToString());
        cDados.setInfoSistema("IDUsuarioLogado", hfDadosSessao.Get("IDUsuarioLogado").ToString());
        cDados.setInfoSistema("IDEstiloVisual", hfDadosSessao.Get("IDEstiloVisual").ToString());
        cDados.setInfoSistema("NomeUsuarioLogado", hfDadosSessao.Get("NomeUsuarioLogado").ToString());
        cDados.setInfoSistema("CodigoCarteira", hfDadosSessao.Get("CodigoCarteira").ToString());

        codigoUsuarioResponsavel = int.Parse(hfDadosSessao.Get("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(hfDadosSessao.Get("CodigoEntidade").ToString());

        cDados.aplicaEstiloVisual(this);

        podeEditarAnalise = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_CnsAnlInd");

        podeConsultarAnalises = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_AltAnlInd");

        podeEditarPlanoAcao = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_AltPlaInd");

        carregaGrid();
        defineLarguraTela();

        int diasTolerancia = cDados.getDiasToleranciaUHE();

        DateTime data = DateTime.Now.Day < diasTolerancia ? DateTime.Now.AddMonths(-2) : DateTime.Now.AddMonths(-1);

        lblDataReferencia.Text = string.Format("*Dados referentes a {0:MM/yyyy}", data);
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        gvDados.Columns["NomeIndicador"].Width = largura - 760;

        gvDados.Settings.VerticalScrollableHeight = (int)(altura - 170);
    }

    private void carregaGrid()
    {
        DataSet ds = cDados.getMapaIndicadoresPainelPresidencia(codigoEntidadeUsuarioResponsavel, "");

        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.ToString().Contains("SalvarMarcacao"))
        {
            string codigoIndicador = e.Parameters.Split(';')[1];
            string codigoProjeto = e.Parameters.Split(';')[2];
            string valor = e.Parameters.Split(';')[3];

            string comandoSQL = string.Format(@"UPDATE {0}.{1}.MetaOperacional SET IndicadorMetaOperacionalPrioritaria = '{2}' WHERE CodigoIndicador = {3} AND CodigoProjeto = {4}"
                , cDados.getDbName()
                , cDados.getDbOwner()
                , valor
                , codigoIndicador
                , codigoProjeto);

            int regAf = 0;

            cDados.execSQL(comandoSQL, ref regAf);

            carregaGrid();

        }
        else
            if (e.Parameters != string.Empty)
            {
                (sender as ASPxGridView).Columns[e.Parameters].Visible = false;
                (sender as ASPxGridView).Columns[e.Parameters].ShowInCustomizationForm = true;
            }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string dataHora = "Mapa_Indicadores_Estrategicos_" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
        
        XlsExportOptionsEx op = new XlsExportOptionsEx();
        op.TextExportMode = TextExportMode.Value;
        ASPxGridViewExporter1.WriteXlsToResponse(dataHora, new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
    }


    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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

        Font fonte = new Font("Verdana", 9);
        e.BrickStyle.Font = fonte;

        if (e.Column != null)
        {
            if (e.Column.Name == "StatusIndicador" && e.TextValue != null)
            {

                if (e.TextValue.ToString().Trim() == "Vermelho")
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.Red;
                }
                else if (e.TextValue.ToString().Trim() == "Amarelo")
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.Yellow;
                }
                else if (e.TextValue.ToString().Trim() == "Verde")
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.Green;
                }
                else if (e.TextValue.ToString().Trim() == "Azul")
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.Blue;
                }
                else if (e.TextValue.ToString().Trim() == "Branco")
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.WhiteSmoke;
                }
                else if (e.TextValue.ToString().Trim() == "Laranja")
                {
                    e.Text = "l";
                    e.TextValue = "l";
                    e.BrickStyle.ForeColor = Color.Orange;
                }
                else
                {
                    e.Text = " ";
                    e.TextValue = " ";
                }

                fonte = new Font("Wingdings", 18, FontStyle.Bold);
                e.BrickStyle.Font = fonte;
                e.BrickStyle.SetAlignment(DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center);
            }
            else if (e.Column.Name == "CodigoIndicador")
            {
                e.Text = " ";
                e.TextValue = " ";
                e.Column.Visible = false;
            }
            else if (e.Column.Name == "Metrica")
            {
                string strValue = gvDados.GetRowValuesByKeyValue(e.KeyValue, "Metrica") + "";
                strValue = Regex.Replace(strValue, @"<[^>]*>", "", RegexOptions.IgnoreCase).Replace(Environment.NewLine, "");
                strValue = Server.HtmlDecode(strValue); 
                e.Column.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.False;
                if (strValue.Replace("&nbsp;", "").Trim() != "")
                {
                    e.Text = strValue.Replace("&nbsp;", "").TrimStart();
                    e.TextValue = strValue.Replace("&nbsp;", "").TrimStart();
                }
            }
        }
    }

    public string getBotoes()
    {
        string htmlBotoes = "";
        string codigoIndicador = Eval("CodigoIndicador").ToString();
        string codigoProjeto = Eval("CodigoProjeto").ToString();
        string codigoMeta = Eval("CodigoMeta").ToString();
        string ano = Eval("AnoResultado").ToString();
        string mes = Eval("MesResultado").ToString();

        string tdGraficoResultados = podeConsultarAnalises ? string.Format(@"<td title='Gráfico de Resultados' style='padding-right:2px'><img alt='Gráfico de Resultados' src='../imagens/botoes/btnCurvaS.png' style='cursor:pointer' onclick='abreGraficoResultados({0}, {1}, {2})' /></td>", codigoIndicador, codigoProjeto, codigoMeta)
            : string.Format(@"<td style='padding-right:2px'><img src='../imagens/botoes/btnCurvaSDes.png' /></td>");
        string tdHistoricoAnalises = podeConsultarAnalises ? string.Format(@"<td title='Histórico de Análises' style='padding-right:2px'><img alt='Histórico de Análises' src='../imagens/botoes/btnAnalises.png' style='cursor:pointer' onclick='abreHistoricoAnalises({0}, {1}, {2})' /></td>", codigoIndicador, codigoProjeto, codigoMeta)
            : string.Format(@"<td style='padding-right:2px'><img src='../imagens/botoes/btnAnalisesDes.png' /></td>");
        string tdEdicaoAnalise = podeEditarAnalise && ano != "" && mes != "" ? string.Format(@"<td title='Análise' style='padding-right:2px'><img alt='Análise' src='../imagens/botoes/btnEditarAnalises.png' style='cursor:pointer' onclick='abreEdicaoAnalises({0}, {1}, {2}, {3}, {4})' /></td></td>", codigoIndicador, codigoProjeto, codigoMeta, ano, mes)
            : string.Format(@"<td style='padding-right:2px'><img src='../imagens/botoes/btnEditarAnalisesDes.png' /></td>");
        string tdPlanoAcao = podeEditarPlanoAcao ? string.Format(@"<td title='Plano de Ação' style='padding-right:2px'><img alt='Plano de Ação' src='../imagens/botoes/btnPlanoAcao.png' style='cursor:pointer' onclick='abrePlanoAcao({0}, {1}, {2})' /></td></td>", codigoIndicador, codigoProjeto, codigoMeta)
            : string.Format(@"<td style='padding-right:2px'><img src='../imagens/botoes/btnPlanoAcaoDes.png' /></td>");

        htmlBotoes = string.Format(@"<table cellpadding=""0"" cellspacing=""0"">
                                        <tr>
                                            {0}
                                            {1}
                                            {2}
                                        </tr>
                                      </table>", tdGraficoResultados, tdEdicaoAnalise, tdPlanoAcao);

        return htmlBotoes;
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }

    public static string ConvertHtmlToText(string source)
    {

        string result;

        // Remove HTML Development formatting
        // Replace line breaks with space
        // because browsers inserts space
        result = source.Replace("\r", " ");
        // Replace line breaks with space
        // because browsers inserts space
        result = result.Replace("\n", " ");
        // Remove step-formatting
        result = result.Replace("\t", string.Empty);
        // Remove repeating speces becuase browsers ignore them
        result = System.Text.RegularExpressions.Regex.Replace(result,
                                                              @"( )+", " ");

        // Remove the header (prepare first by clearing attributes)
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*head([^>])*>", "<head>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"(<( )*(/)( )*head( )*>)", "</head>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(<head>).*(</head>)", string.Empty,
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // remove all scripts (prepare first by clearing attributes)
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*script([^>])*>", "<script>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"(<( )*(/)( )*script( )*>)", "</script>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //result = System.Text.RegularExpressions.Regex.Replace(result, 
        //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
        //         string.Empty, 
        //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"(<script>).*(</script>)", string.Empty,
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // remove all styles (prepare first by clearing attributes)
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*style([^>])*>", "<style>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"(<( )*(/)( )*style( )*>)", "</style>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(<style>).*(</style>)", string.Empty,
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // insert tabs in spaces of <td> tags
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*td([^>])*>", "\t",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // insert line breaks in places of <BR> and <LI> tags
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*br( )*>", "\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*li( )*>", "\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // insert line paragraphs (double line breaks) in place
        // if <P>, <DIV> and <TR> tags
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*div([^>])*>", "\r\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*tr([^>])*>", "\r\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<( )*p([^>])*>", "\r\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // Remove remaining tags like <a>, links, images,
        // comments etc - anything thats enclosed inside < >
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<[^>]*>", string.Empty,
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        // replace special characters:
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&nbsp;", " ",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&bull;", " * ",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&lsaquo;", "<",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&rsaquo;", ">",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&trade;", "(tm)",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&frasl;", "/",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"<", "<",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @">", ">",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&copy;", "(c)",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&reg;", "(r)",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        // Remove all others. More can be added, see
        // http://hotwired.lycos.com/webmonkey/reference/special_characters/
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 @"&(.{2,6});", string.Empty,
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);


        // make line breaking consistent
        result = result.Replace("\n", "\r");

        // Remove extra line breaks and tabs:
        // replace over 2 breaks with 2 and over 4 tabs with 4. 
        // Prepare first to remove any whitespaces inbetween
        // the escaped characters and remove redundant tabs inbetween linebreaks
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\r)( )+(\r)", "\r\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\t)( )+(\t)", "\t\t",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\t)( )+(\r)", "\t\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\r)( )+(\t)", "\r\t",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        // Remove redundant tabs
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\r)(\t)+(\r)", "\r\r",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        // Remove multible tabs followind a linebreak with just one tab
        result = System.Text.RegularExpressions.Regex.Replace(result,
                 "(\r)(\t)+", "\r\t",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        // Initial replacement target string for linebreaks
        string breaks = "\r\r\r";
        // Initial replacement target string for tabs
        string tabs = "\t\t\t\t\t";
        for (int index = 0; index < result.Length; index++)
        {
            result = result.Replace(breaks, "\r\r");
            result = result.Replace(tabs, "\t\t\t\t");
            breaks = breaks + "\r";
            tabs = tabs + "\t";
        }

        // Thats it.
        return result;

    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "IndicadorMetaOperacionalPrioritaria")
        {
            string codigoIndicador = gvDados.GetRowValues(e.VisibleIndex, "CodigoIndicador").ToString();
            string codigoProjeto = gvDados.GetRowValues(e.VisibleIndex, "CodigoProjeto").ToString();

            ASPxCheckBox ck = (ASPxCheckBox)gvDados.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "ck_Marcado");

            string comandoJS = string.Format(@" var codigoIndicadorLinha = {0};
                                                var codigoProjetoLinha = {1};
	                                            var valor = s.GetValue();
	                                            gvDados.PerformCallback('SalvarMarcacao;' + codigoIndicadorLinha + ';' + codigoProjetoLinha + ';' + valor);
                                                ", codigoIndicador
                                                 , codigoProjeto);

            ck.ClientSideEvents.CheckedChanged = @"function(s, e) { " + comandoJS + " }";

            ck.Checked = e.CellValue + "" == "S";
        }
    }
}