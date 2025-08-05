using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Diagnostics;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using System.Text.RegularExpressions;

public partial class _Projetos_Administracao_cronogramaPendenteAprovacao : System.Web.UI.Page
{
    private dados cDados;

    private int codigoProjeto;
    private int codigoWF = -1;
    private int codigoInstancia = -1;
    private int codigoUsuarioResponsavel;
    private bool exportaOLAPTodosFormatos = false;

    protected void Page_Init(object sender, EventArgs e)
    {

        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        try
        {
            codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            if (cDados.getInfoSistema("IDUsuarioLogado") == null)
                Response.Redirect("~/erros/erroInatividade.aspx");
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

        if (Request.QueryString["CWF"] != null && Request.QueryString["CWF"].ToString() != "")
            codigoWF = int.Parse(Request.QueryString["CWF"].ToString());

        if (Request.QueryString["CIWF"] != null && Request.QueryString["CIWF"].ToString() != "")
            codigoInstancia = int.Parse(Request.QueryString["CIWF"].ToString());


        HeaderOnTela();
        cDados.aplicaEstiloVisual(Page);

        if (!hfGeral.Contains("tipoArquivo"))
        {
            hfGeral.Set("tipoArquivo", "XLS");
        }

        #region

        DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
        if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
            exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");

        populaOpcoesExportacao();

        #endregion

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            this.Title = cDados.getNomeSistema();
           
        }
        carregaGrid();
        defineAlturaTela();
       
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));

        //tlDados.Settings.ScrollableHeight = alturaPrincipal - 395;
        tlDados.Settings.HorizontalScrollBarMode = DevExpress.Web.ScrollBarMode.Auto;
    }

    private void carregaGrid()
    {

        string eventoLinha_EXC = (checkTarefasExcluidas.Checked == true) ? "'EXC'," : "";
        string eventoLinha_INC = (checkTarefasIncluidas.Checked == true) ? "'INC'," : "";
        string eventoLinha_ALT = (checkTarefasAlteradas.Checked == true) ? "'ALT'," : "";

        string eventoLinha = " WHERE EventoLinha IN(" + eventoLinha_EXC + eventoLinha_INC + eventoLinha_ALT + ")";
        if (eventoLinha_EXC == "" && eventoLinha_INC == "" && eventoLinha_ALT == "")
        {
            eventoLinha = "";
        }
        else
        {
            eventoLinha = eventoLinha.Replace("',)", "')");
        }

        

 
        string comandoSQL = string.Format(@"
        SELECT  VersaoAtual,VersaoAnterior, CodigoCronogramaProjeto, CodigoTarefa, 
                NomeTarefa,SequenciaTarefaCronograma, CodigoTarefaSuperior, Inicio, 
                Termino,Duracao, Trabalho, Custo, 
                InicioLB, TerminoLB,  DuracaoLB,TrabalhoLB, 
                CustoLB, PercentualFisicoConcluido, DataExclusao,EventoLinha,
                FormatoDuracao 
          FROM {0}.{1}.f_CronogramaPendenteAprovacaoLB({2}, {3}, {4})  {5}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, codigoWF, codigoInstancia, eventoLinha);
        DataSet ds = cDados.getDataSet(comandoSQL);
        tlDados.DataSource = ds.Tables[0];
        tlDados.DataBind();
        tlDados.ExpandAll();
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/cronogramaPendenteAprovacao.js""></script>"));
        this.TH(this.TS("cronogramaPendenteAprovacao"));
    }

    protected void tlDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlDataCellEventArgs e)
    {
        string tagHTMLInicio = "", tagHTMLFim = "";

        if (e.GetValue("EventoLinha") != null && e.GetValue("EventoLinha").ToString() != "")
        {
            if (e.GetValue("EventoLinha").ToString() == "EXC")
            {
                /*'EXC' : fonte riscada no meio*/
                tagHTMLInicio = "<strike>";
                tagHTMLFim = "</strike>";

            }
            else if (e.GetValue("EventoLinha").ToString() == "INC")
            {
                /* 'INC' : cor da fonte 'VERDE'*/
                tagHTMLInicio = "<font color='green'>";
                tagHTMLFim = "</font>";
            }
            else if (e.GetValue("EventoLinha").ToString() == "ALT")
            {
                /* 'ALT' : cor da fonte 'AZUL*/
                tagHTMLInicio = "<font color='blue'>";
                tagHTMLFim = "</font>";
            }
        }

        if (e.Column.FieldName == "Duracao")
        {
            e.Cell.Text = tagHTMLInicio + e.GetValue("Duracao").ToString() + " " + e.GetValue("FormatoDuracao").ToString() + tagHTMLFim;
        }
        else if (e.Column.FieldName == "DuracaoLB")
        {
            e.Cell.Text = tagHTMLInicio + e.GetValue("DuracaoLB").ToString() + " " + e.GetValue("FormatoDuracao").ToString() + tagHTMLFim;
        }

        else if (e.Column.FieldName == "Inicio")
        {
            if (e.GetValue("Inicio").ToString().Trim() != "")
            {
                e.Cell.Text = tagHTMLInicio + string.Format("{0:dd/MM/yyyy}", DateTime.Parse(e.GetValue("Inicio").ToString())) + tagHTMLFim;
            }
        }
        else if (e.Column.FieldName == "InicioLB")
        {
            if (e.GetValue("InicioLB").ToString().Trim() != "")
            {
                e.Cell.Text = tagHTMLInicio + string.Format("{0:dd/MM/yyyy}", DateTime.Parse(e.GetValue("InicioLB").ToString())) + tagHTMLFim;
            }

        }
        else if (e.Column.FieldName == "Termino")
        {
            if (e.GetValue("Termino").ToString().Trim() != "")
            {
                e.Cell.Text = tagHTMLInicio + string.Format("{0:dd/MM/yyyy}", DateTime.Parse(e.GetValue("Termino").ToString())) + tagHTMLFim;
            }
        }
        else if (e.Column.FieldName == "TerminoLB")
        {
            if (e.GetValue("TerminoLB").ToString().Trim() != "")
            {
                e.Cell.Text = tagHTMLInicio + string.Format("{0:dd/MM/yyyy}", DateTime.Parse(e.GetValue("TerminoLB").ToString())) + tagHTMLFim;
            }
        }
        else if (e.Column.FieldName == "Custo")
        {
            if (e.GetValue("Custo").ToString().Trim() != "")
            {
                e.Cell.Text = tagHTMLInicio + string.Format("{0:c2}", decimal.Parse(e.GetValue("Custo").ToString())) + tagHTMLFim;
            }
        }
        else if (e.Column.FieldName == "CustoLB")
        {
            if (e.GetValue("CustoLB").ToString().Trim() != "")
            {
                e.Cell.Text = tagHTMLInicio + string.Format("{0:c2}", decimal.Parse(e.GetValue("Custo").ToString())) + tagHTMLFim;
            }
        }
        else
        {
            e.Cell.Text = tagHTMLInicio + e.CellValue + tagHTMLFim;
        }
    }

    #region  EXPORTAÇÃO DE DADOS

    private void populaOpcoesExportacao()
    {
        ddlExporta.Items.Clear();
        ListEditItem liExcel = new ListEditItem("XLS", "XLS");
        liExcel.ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";
        ddlExporta.Items.Add(liExcel);
        ddlExporta.ClientEnabled = false;

        if (exportaOLAPTodosFormatos)
        {
            ddlExporta.ClientEnabled = true;
            //ddlExporta.Items.Add(new ListEditItem("XLS", "XLS"));

            //ListEditItem liPDF = new ListEditItem("PDF", "PDF");
            //liPDF.ImageUrl = "~/imagens/menuExportacao/iconoPDF.png";
            //ddlExporta.Items.Add(liPDF);


            //ListEditItem liHTML = new ListEditItem("HTML", "HTML");
            //liHTML.ImageUrl = "~/imagens/menuExportacao/iconoHtml.png";
            //ddlExporta.Items.Add(liHTML);

            //ListEditItem liRTF = new ListEditItem("RTF", "RTF");
            //liRTF.ImageUrl = "~/imagens/menuExportacao/iconoPortfolio.png";
            //ddlExporta.Items.Add(liRTF);

            //ListEditItem liCSV = new ListEditItem("CSV", "CSV");
            //liCSV.ImageUrl = "~/imagens/menuExportacao/iconoCSV.png";
            //ddlExporta.Items.Add(liCSV);
        }

        ddlExporta.SelectedIndex = 0;
    }

    public void StartProcess(string path)
    {
        Process process = new Process();

        try
        {
            process.StartInfo.FileName = path;
            process.Start();
            process.WaitForInputIdle();
        }
        catch { }
    }

    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string nomeArquivo = "";

        if (e.Parameter == "HTML")
            nomeArquivo = "~/imagens/menuExportacao/iconoHtml.png";

        if (e.Parameter == "PDF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPDF.png";

        if (e.Parameter == "XLS")
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";

        if (e.Parameter == "RTF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPortfolio.png";

        if (e.Parameter == "CSV")
            nomeArquivo = "~/imagens/menuExportacao/iconoCSV.png";

        //imgExportacao.ImageUrl = nomeArquivo;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuarioResponsavel;
            string nomeArquivo = "", app = "", erro = "";

            nomeArquivo = "CronogramaPendenteAprovacao_" + dataHora + ".xls";
            try
            {
                ASPxTreeListExporter1.WriteXls(stream);
            }
            catch (Exception ex)
            {
                erro = "S";
                string erroX = ex.Message;
            }

            app = "application/ms-excel";

            if (erro == "")
            {
                Response.Clear();
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", app);
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + nomeArquivo + "\"");
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

    protected void ASPxTreeListExporter1_RenderBrick(object sender, DevExpress.Web.ASPxTreeList.ASPxTreeListExportRenderBrickEventArgs e)
    {
        if(e.RowKind == TreeListRowKind.Header)
        {
            e.Text = RemoveFormatacaoHtml(e.Text);
        }

        if (e.RowKind == TreeListRowKind.Header)
        {
            if (e.Column.FieldName == "Duracao")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            }
            else if (e.Column.FieldName == "DuracaoLB")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            }
            else if (e.Column.FieldName == "Inicio")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            }
            else if (e.Column.FieldName == "InicioLB")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            }
            else if (e.Column.FieldName == "Termino")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            }
            else if (e.Column.FieldName == "TerminoLB")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            }
        }

        if (e.RowKind == TreeListRowKind.Data)
        {

            /*'EXC' : fonte riscada no meio*/
            if (tlDados.FindNodeByKeyValue(e.NodeKey)["EventoLinha"].ToString() == "EXC")
            {
                e.BrickStyle.Font = new System.Drawing.Font("Verdana", 9.0f, System.Drawing.FontStyle.Strikeout);
                //e.BrickStyle. = System.Drawing.FontStyle.Strikeout;
            }
            /* 'INC' : cor da fonte 'VERDE'*/
            if (tlDados.FindNodeByKeyValue(e.NodeKey)["EventoLinha"].ToString() == "INC")
            {
                //System.Drawing.Font fontex = new System.Drawing.Font("Verdana", 8.0f, System.Drawing.FontStyle.Regular);
                e.BrickStyle.ForeColor = System.Drawing.Color.Green;

            }
            /* 'ALT' : cor da fonte 'AZUL*/
            if (tlDados.FindNodeByKeyValue(e.NodeKey)["EventoLinha"].ToString() == "ALT")
            {
                e.BrickStyle.ForeColor = System.Drawing.Color.Blue;
            }


            if (e.Column.FieldName == "Duracao")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if ((tlDados.FindNodeByKeyValue(e.NodeKey)["Duracao"] != null) &&
                    (tlDados.FindNodeByKeyValue(e.NodeKey)["FormatoDuracao"] != null))
                {
                    e.TextValue = tlDados.FindNodeByKeyValue(e.NodeKey)["Duracao"].ToString() + " " + tlDados.FindNodeByKeyValue(e.NodeKey)["FormatoDuracao"].ToString();
                    // e.TextValue = e.Text;
                }
            }
            else if (e.Column.FieldName == "DuracaoLB")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                if (tlDados.FindNodeByKeyValue(e.NodeKey)["DuracaoLB"] != null && tlDados.FindNodeByKeyValue(e.NodeKey)["FormatoDuracao"] != null)
                {
                    e.TextValue = tlDados.FindNodeByKeyValue(e.NodeKey)["DuracaoLB"].ToString() + " " + tlDados.FindNodeByKeyValue(e.NodeKey)["FormatoDuracao"].ToString();
                    //e.TextValue = e.Text;
                }
            }

            else if (e.Column.FieldName == "Inicio")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                object value = tlDados.FindNodeByKeyValue(e.NodeKey)["Inicio"];

                if (value is DateTime)
                {
                    e.Text = ((DateTime)value).ToString("d");
                    e.TextValueFormatString = "dd/MM/yyyy";
                }
                else
                    e.Text = ((value != null) && (value.ToString().Trim() != "")) ? value.ToString() : e.Text;
            }
            else if (e.Column.FieldName == "InicioLB")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                object value = tlDados.FindNodeByKeyValue(e.NodeKey)["InicioLB"];

                if (value is DateTime)
                {
                    e.Text = ((DateTime)value).ToString("d");
                    e.TextValueFormatString = "dd/MM/yyyy";
                }
                else
                    e.Text = ((value != null) && (value.ToString().Trim() != "")) ? value.ToString() : e.Text;
            }
            else if (e.Column.FieldName == "Termino")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                object value = tlDados.FindNodeByKeyValue(e.NodeKey)["Termino"];

                if (value is DateTime)
                {
                    e.Text = ((DateTime)value).ToString("d");
                    e.TextValueFormatString = "dd/MM/yyyy";
                }

                else
                    e.Text = ((value != null) && (value.ToString().Trim() != "")) ? value.ToString() : e.Text;
            }
            else if (e.Column.FieldName == "TerminoLB")
            {
                e.BrickStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                object value = tlDados.FindNodeByKeyValue(e.NodeKey)["TerminoLB"];

                if (value is DateTime)
                {
                    e.Text = ((DateTime)value).ToString("d");
                    e.TextValueFormatString = "dd/MM/yyyy";
                }

                else
                    e.Text = ((value != null) && (value.ToString().Trim() != "")) ? value.ToString() : e.Text;

            }
        }
    }

    private string RemoveFormatacaoHtml(string input)
    {
        string strValue = Regex.Replace(input ?? string.Empty, @"<[^>]*>", " ");
        return strValue;
    }
    #endregion


}