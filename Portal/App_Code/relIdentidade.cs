using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System.Drawing;

/// <summary>
/// Summary description for relIdentidade
/// </summary>
public class relIdentidade : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;

    private DevExpress.XtraReports.Parameters.Parameter parametroNomeSindicato = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pnegocio = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pmissao = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pvisao = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pcrencasvalores = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter poportunidades = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pameacas = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pforcas = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pfraquezas = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter ppessoas = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pprocessos = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pmercadoImagem = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pfinanceira = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter psiglaSindicato = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pimagemLogo = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter ppathLogo = new DevExpress.XtraReports.Parameters.Parameter();
    private DevExpress.XtraReports.Parameters.Parameter pVetBytes = new DevExpress.XtraReports.Parameters.Parameter();




    public XRPictureBox xrPictureBox1;
    private XRLabel xrLabel1;
    private XRLabel lblNomeSindicato;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    PaddingInfo recuoZero = new PaddingInfo(0, 0, 0, 0);
    public relIdentidade()
    {
        InitializeComponent();
        parametroNomeSindicato.Name = "parametroNomeSindicato";
        pnegocio.Name = "negocio";
        pmissao.Name = "missao";
        pvisao.Name = "visao";
        pcrencasvalores.Name = "crencasvalores";
        poportunidades.Name = "oportunidades";
        pameacas.Name = "ameacas";
        pforcas.Name = "forcas";
        pfraquezas.Name = "fraquezas";
        ppessoas.Name = "pessoas";
        pprocessos.Name = "processos";
        pmercadoImagem.Name = "mercadoImagem";
        pfinanceira.Name = "financeira";
        psiglaSindicato.Name = "siglaSindicato";
        pimagemLogo.Name = "imagemLogo";
        ppathLogo.Name = "pathLogo";
        pVetBytes.Name = "vetBytes";
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[]
        {
            this.parametroNomeSindicato,
            this.pnegocio ,
            this.pmissao,
            this.pvisao,
            this.pcrencasvalores,
            this.poportunidades,
            this.pameacas,
            this.pforcas,
            this.pfraquezas,
            this.ppessoas,
            this.pprocessos,
            this.pmercadoImagem,
            this.pfinanceira,
            this.psiglaSindicato,
            this.pimagemLogo,
            this.ppathLogo,
        this.pVetBytes});
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        //string resourceFileName = "relIdentidade.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.lblNomeSindicato = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrLabel1,
            this.lblNomeSindicato});
        this.PageHeader.Height = 55;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
        // 
        // PageFooter
        // 
        this.PageFooter.Height = 30;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.ImageUrl = "D:\\Meus documentos\\Minhas imagens\\logotesteDevExpress.JPG";
        this.xrPictureBox1.Location = new System.Drawing.Point(0, 0);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.Size = new System.Drawing.Size(170, 55);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.Location = new System.Drawing.Point(200, 0);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.Size = new System.Drawing.Size(542, 17);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "Identidade Organizacional";
        // 
        // lblNomeSindicato
        // 
        this.lblNomeSindicato.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblNomeSindicato.Location = new System.Drawing.Point(208, 25);
        this.lblNomeSindicato.Name = "lblNomeSindicato";
        this.lblNomeSindicato.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.lblNomeSindicato.Size = new System.Drawing.Size(542, 17);
        this.lblNomeSindicato.StylePriority.UseFont = false;
        this.lblNomeSindicato.Text = "lblNomeSindicato";
        // 
        // relIdentidade
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter});
        this.Landscape = true;
        this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
        this.PageHeight = 827;
        this.PageWidth = 1169;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Version = "9.2";
        this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.relIdentidade_BeforePrint);
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        string txtCabecalho = psiglaSindicato.Value.ToString() + " - " + parametroNomeSindicato.Value.ToString();
        txtCabecalho = txtCabecalho.TrimStart(' ');

        xrPictureBox1.ImageUrl = ppathLogo.Value.ToString();
        xrPictureBox1.ResumeLayout();

        lblNomeSindicato.Text = parametroNomeSindicato.Value.ToString() != "" ? txtCabecalho : "";
    }

    private byte[] strToByteArray(string vetorDeBytes)
    {
        string[] vetStrAux = vetorDeBytes.Split(';');
        byte[] byteRetorno = new byte[vetStrAux.Length];
        for (int i = 0; i < vetStrAux.Length; i++)
        {
            byteRetorno[i] = byte.Parse(vetStrAux[i]);
        }
        return byteRetorno;
    }
    private XRTableRow getRow(int alturaLinha, PaddingInfo recuo, char tipo, params object[] conteudoColunas)
    {
        if (conteudoColunas.Length <= 0)
            return null;
        FontStyle fonteBold = FontStyle.Bold;
        FontStyle fonteNormal = FontStyle.Regular;

        Font fnegrito = new Font(this.Font.FontFamily.Name, this.Font.SizeInPoints + 1, fonteBold);

        Font fcomum = new Font(this.Font.FontFamily.Name, this.Font.SizeInPoints + 1, fonteNormal);


        XRTableRow row = new XRTableRow();
        row.Height = alturaLinha;
        foreach (object conteudoColuna in conteudoColunas)
        {
            XRTableCell celula = new XRTableCell();
            if (tipo == 'T')
                celula.Font = fnegrito;
            else
                celula.Font = fcomum;
            celula.Text = conteudoColuna.ToString();
            celula.Padding = recuo;
            row.Cells.Add(celula);
        }
        return row;
    }

    private void trataQuebraDeLinha(string texto, PaddingInfo recuoDezEsq, ref XRTable tabela)
    {
        string[] retorno = texto.Split('\r');
        for (int i = 0; i < retorno.Length; i++)
        {
            tabela.Rows.Add(getRow(10, recuoDezEsq, 'C', retorno[i]));
        }
    }

    private void pulaLinha(ref XRTable tabela)
    {
        tabela.Rows.Add(getRow(10, recuoZero, 'C', " "));//pula uma linha
    }

    private void relIdentidade_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        this.PageHeader_BeforePrint(new object(), new System.Drawing.Printing.PrintEventArgs());
        PaddingInfo recuoDezEsq = new PaddingInfo(10, 0, 0, 0);
        PaddingInfo recuoVinteEsq = new PaddingInfo(20, 0, 0, 0);
        PaddingInfo recuoTrintaEsq = new PaddingInfo(30, 0, 0, 0);

        XRTable tabela = new XRTable();
        tabela.Rows.Add(getRow(10, recuoZero, 'T', "1 Negócio"));
        trataQuebraDeLinha(pnegocio.Value.ToString(), recuoDezEsq, ref tabela);

        pulaLinha(ref tabela);

        tabela.Rows.Add(getRow(10, recuoZero, 'T', "2 Missão"));
        trataQuebraDeLinha(pmissao.Value.ToString(), recuoDezEsq, ref tabela);

        pulaLinha(ref tabela);

        tabela.Rows.Add(getRow(10, recuoZero, 'T', "3 Visão"));
        trataQuebraDeLinha(pvisao.Value.ToString(), recuoDezEsq, ref tabela);

        pulaLinha(ref tabela);

        //tabela.Rows.Add(getRow(10, recuoZero, 'T', "4 Crenças e Valores"));
        //trataQuebraDeLinha(pcrencasvalores.Value.ToString(), recuoDezEsq, ref tabela);

        //pulaLinha(ref tabela);

        tabela.Rows.Add(getRow(10, recuoZero, 'T', "4 Análise FOFA"));
        tabela.Rows.Add(getRow(10, recuoDezEsq, 'T', "4.1 Oportunidades"));
        trataQuebraDeLinha(poportunidades.Value.ToString(), recuoVinteEsq, ref tabela);

        pulaLinha(ref tabela);

        tabela.Rows.Add(getRow(10, recuoDezEsq, 'T', "4.2 Ameaças"));
        trataQuebraDeLinha(pameacas.Value.ToString(), recuoVinteEsq, ref tabela);

        pulaLinha(ref tabela);

        tabela.Rows.Add(getRow(10, recuoDezEsq, 'T', "4.3 Forças"));
        trataQuebraDeLinha(pforcas.Value.ToString(), recuoVinteEsq, ref tabela);

        pulaLinha(ref tabela);

        tabela.Rows.Add(getRow(10, recuoDezEsq, 'T', "4.4 Fraquezas"));
        trataQuebraDeLinha(pfraquezas.Value.ToString(), recuoVinteEsq, ref tabela);

        pulaLinha(ref tabela);

        tabela.Rows.Add(getRow(10, recuoZero, 'T', "5 Objetivos Estratégicos"));
        tabela.Rows.Add(getRow(10, recuoDezEsq, 'T', "5.1 Pessoas"));
        trataQuebraDeLinha(ppessoas.Value.ToString(), recuoVinteEsq, ref tabela);

        pulaLinha(ref tabela);

        tabela.Rows.Add(getRow(10, recuoDezEsq, 'T', "5.2 Processos"));
        trataQuebraDeLinha(pprocessos.Value.ToString(), recuoVinteEsq, ref tabela);

        pulaLinha(ref tabela);

        tabela.Rows.Add(getRow(10, recuoDezEsq, 'T', "5.3 Imagem e mercado"));
        trataQuebraDeLinha(pmercadoImagem.Value.ToString(), recuoVinteEsq, ref tabela);

        pulaLinha(ref tabela);

        tabela.Rows.Add(getRow(10, recuoDezEsq, 'T', "5.4 Financeira"));
        trataQuebraDeLinha(pfinanceira.Value.ToString(), recuoVinteEsq, ref tabela);

        pulaLinha(ref tabela);

        tabela.Width = 700;
        Detail.Controls.Add(tabela);

        XRLine linhaRodape = new XRLine();
        linhaRodape.Left = 0;
        linhaRodape.Size = new Size(700, 1);
        PageFooter.Controls.Add(linhaRodape);

    }
}
