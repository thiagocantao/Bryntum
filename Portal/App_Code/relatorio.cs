using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Diagnostics;
using System.Drawing;

/// <summary>
/// Summary description for relatorio
/// </summary>

public class relatorio : DevExpress.XtraReports.UI.XtraReport
{
    public relatorio()
    {
        InitializeComponent();

        ds = getConteudoFormulario();
    }

    dados cDados = CdadosUtil.GetCdados(null);
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;


    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private PaddingInfo recuoEsquerda = new PaddingInfo(5, 0, 0, 0);
    public DataSet ds;
    // master = 223 - modelo = 16 - risco 
    // master = 224 - Modelo = 14 - issues
    int codigoFormularioMaster = 223;
    int codigoModeloFormulario = 16;

    int codigoProjeto = 1;
    private XRLabel lblCabecalho;
    string inCodigosFormularios = "";


    #region conteúdo do formulário

    private DataSet getConteudoFormulario()
    {
        string linkProjeto = "";
        string whereCodigoFormulario = "";
        // se tem projeto associado
        if (codigoProjeto > 0)
            linkProjeto = string.Format(
                @"inner join
                       FormularioProjeto FP on (FP.codigoFormulario = CF.codigoFormulario AND codigoProject = {0})", codigoProjeto);

        if (codigoFormularioMaster > 0)
        {
            /*whereCodigoFormulario = string.Format(
                @" AND F.codigoFormulario in (select codigoSubFormulario 
                                                from linkFormulario
                                               where codigoFormulario = {0}
                                               union
                                               Select {0} )", codigoFormularioMaster);*/

            whereCodigoFormulario = string.Format(
                @" AND F.codigoFormulario = {0}", codigoFormularioMaster);
        }
        // se tem uma lista de filtros de formularios
        string whereFiltraFormularios = "";
        if (inCodigosFormularios != "")
        {
            whereFiltraFormularios = string.Format(" AND F.CodigoFormulario in ({0}) ", inCodigosFormularios);
        }
        string comandoSQL = string.Format(
            @"SELECT F.CodigoFormulario, Aba, CMF.OrdemCampoFormulario, CMF.nomeCampo,
                         F.DescricaoFormulario, CMF.CodigoTipoCampo, CF.CodigoCampo, CF.valorNum, 
                         CF.ValorDat, CF.ValorVar, CF.ValorTxt, CF.ValorBoo, CMF.DefinicaoCampo
                    FROM campoFormulario CF inner join
                         campomodeloFormulario CMF on (CF.codigoCampo = CMF.codigoCampo) inner join
                         Formulario F on (F.codigoFormulario = CF.CodigoFormulario) {0}
                   WHERE CMF.dataExclusao is null
                     AND F.dataExclusao is null
                     {1} {2}
                     AND CMF.CodigoModeloFormulario = {3}
                   ORDER BY CF.CodigoFormulario, Aba, OrdemCampoFormulario", linkProjeto, whereCodigoFormulario, whereFiltraFormularios, codigoModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    #endregion

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    /// 

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        //string resourceFileName = "relatorio.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.lblCabecalho = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Height = 10;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblCabecalho});
        this.PageHeader.Height = 25;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // lblCabecalho
        // 
        this.lblCabecalho.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.lblCabecalho.Font = new System.Drawing.Font("Verdana", 8F);
        this.lblCabecalho.Location = new System.Drawing.Point(0, 0);
        this.lblCabecalho.Name = "lblCabecalho";
        this.lblCabecalho.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblCabecalho.Size = new System.Drawing.Size(792, 25);
        this.lblCabecalho.StylePriority.UseBorders = false;
        this.lblCabecalho.StylePriority.UseFont = false;
        // 
        // PageFooter
        // 
        this.PageFooter.Height = 42;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // relatorio
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter});
        this.Margins = new System.Drawing.Printing.Margins(20, 10, 10, 10);
        this.PageHeight = 1169;
        this.PageWidth = 827;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "9.2";
        this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.relatorio_BeforePrint);
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private string getValorCampo(string codigoTipoCampo, DataRow campo)
    {
        string descricaoCampo = "";
        if (codigoTipoCampo == "TXT")
        {
            descricaoCampo = campo["ValorTxt"].ToString();
        }
        if (codigoTipoCampo == "NUM")
        {
            descricaoCampo = campo["valorNum"].ToString();
        }
        if (codigoTipoCampo == "DAT")
        {
            if (campo["ValorDat"].ToString() == "")
                descricaoCampo = "--";
            else
                descricaoCampo = campo["ValorDat"].ToString();
        }
        if (codigoTipoCampo == "VAR" || codigoTipoCampo == "LST" || codigoTipoCampo == "LOO")
        {
            if (campo["ValorVar"].ToString() == "")
                descricaoCampo = "--";
            else
                descricaoCampo = campo["ValorVar"].ToString();
        }
        return descricaoCampo;
    }

    private XRTableRow getRow(int alturaLinha, PaddingInfo recuo, ref XRLabel lbl, params object[] conteudoColunas)
    {
        if (conteudoColunas.Length <= 0)
            return null;

        XRTableRow row = new XRTableRow();

        row.Height = alturaLinha;
        foreach (object conteudoColuna in conteudoColunas)
        {
            XRTableCell celula = new XRTableCell();
            celula.Text = conteudoColuna.ToString();
            celula.HtmlItemCreated += new HtmlEventHandler(celula1_HtmlItemCreated);
            celula.Padding = recuo;
            row.Cells.Add(celula);
        }
        return row;
    }

    private void celula1_HtmlItemCreated(object sender, HtmlEventArgs e)
    {
        // e.ContentCell.InnerHtml = "<ol><li>" + e.Brick.Text + "</li></ol>";
        e.ContentCell.InnerHtml = string.Format(@" <p>teste</p><p>linha <strong>2 em </strong>negrito.
</p><ol><li>ffsdfasdfasdfds</li><li>fadsfasdfasdfasdf</li><li>asdfasdfasdfasdfasf</li></ol><p><span style=""BACKGROUND-COLOR:
#ff0000; COLOR: #ffff00"">FIM</span></p>");
    }

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        DataRow[] dr = ds.Tables[0].Select("");
        string descricaoForm = dr[0]["DescricaoFormulario"].ToString();
        lblCabecalho.Text = descricaoForm;
    }

    private void relatorio_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

        XRTable tabela = new XRTable();
        XRLabel lbl = new XRLabel();

        tabela.BeginInit();
        foreach (DataRow campo in ds.Tables[0].Rows)
        {
            if (campo["CodigoTipoCampo"].ToString() == "SUB")
                continue;
            XRTableRow linha1 = getRow(10, recuoEsquerda, ref lbl, campo["nomeCampo"].ToString());
            XRTableRow linha2 = getRow(10, recuoEsquerda, ref lbl, getValorCampo(campo["CodigoTipoCampo"].ToString(), campo));
            tabela.Rows.Add(linha1);

            tabela.Rows.Add(linha2);
            linha2.Borders = BorderSide.Bottom;
            string nomeCampo = campo["nomeCampo"].ToString();
            string valorCampo = getValorCampo(campo["CodigoTipoCampo"].ToString(), campo);
            tabela.Height += 20;
        }

        tabela.Size = new Size(lblCabecalho.Size.Width, tabela.Height);
        tabela.EndInit();
        Detail.Controls.Add(tabela);

        /* string caminho = "C:\\pastax\\teste.html";
         string caminhopdf = "C:\\pastax\\teste.pdf";
         try
         {
             this.ExportToRtf(caminho);

         }
         catch (Exception ex)
         {
         }
         try
         {
             XRRichText richText = new XRRichText();
             richText.BeginInit();
             richText.Width = 900;
             richText.Height = tabela.Height;
             richText.LoadFile(caminho);
             richText.EndInit();
             Detail.Controls.Clear();
             Detail.Controls.Add(richText);
             this.ExportToPdf(caminhopdf);
         }
         catch(Exception ex) 
         {}*/
    }

    public void StartProcess(string path)
    {
        //precisa dessa função para chamar as funções loadfile e exporttopdf
        Process process = new Process();
        try
        {
            process.StartInfo.FileName = path;
            process.Start();
            process.WaitForInputIdle();

        }
        catch { }
    }
}
