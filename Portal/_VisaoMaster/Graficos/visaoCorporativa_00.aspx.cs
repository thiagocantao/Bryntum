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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public partial class _VisaoMaster_Graficos_visaoCorporativa_00 : System.Web.UI.Page
{
    dados cDados;

    public string larguraTela = "", alturaTela = "", larguraTabela = "", larguraGrafico = "", alturaGrafico1 = "", alturaGrafico2 = "", alturaNumeros, alturaFrame1 = "";

    public string grafico1 = "", grafico2 = "";

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

        defineLarguraTela();

        string codigoArea = "-1", nomeArea = "";

        if (Request.QueryString["CA"] != null && Request.QueryString["CA"].ToString() != "" && Request.QueryString["CA"].ToString().ToUpper().Trim() != "NULL")
            codigoArea = Request.QueryString["CA"].ToString();

        if (Request.QueryString["NA"] != null && Request.QueryString["NA"].ToString() != "" && Request.QueryString["NA"].ToString().ToUpper().Trim() != "NULL")
            nomeArea = Request.QueryString["NA"].ToString();

        grafico1 = "vm_001.aspx?CR=UHE_Principal&Altura=" + alturaGrafico1 + "&Largura=" + larguraGrafico + "&CA=" + codigoArea + "&NA=" + nomeArea;
        
        carregaFotosObra();

        defineAjuda();
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        alturaTela = (altura - 159).ToString() + "px";
        alturaGrafico1 = ((altura - 290)).ToString();
        alturaFrame1 = ((altura - 290)).ToString();
        //alturaGrafico2 = ((altura - 285) / 2 + 20).ToString();
        larguraTela = (largura - 380).ToString();
        larguraGrafico = (largura - 390).ToString();
        alturaNumeros = (altura - 160).ToString() + "px";
    }

    private void carregaFotosObra()
    {
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        DataSet dsDados = cDados.getFotosPainelGerenciamento("St_Pimental", codigoEntidade, 1, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            int index = 0;
            foreach (DataRow dr in dsDados.Tables[0].Rows)
            {
                MemoryStream ms = new MemoryStream((byte[])dr["Foto"]);

                Size sz = new Size(200, 140);

                System.Drawing.Image image = resizeImage(System.Drawing.Image.FromStream(ms), sz);

                MemoryStream ms2 = new MemoryStream();

                image.Save(ms2, ImageFormat.Jpeg);

                img001.ContentBytes = ms2.ToArray(); 
                img001.ToolTip = dr["DescricaoFoto"].ToString();
                img001.Cursor = "Pointer";
                img001.ClientSideEvents.Click = "function(s, e) {abreFotosSitio('St_Pimental', 'Sítio Pimental');}";
                index++;
            }
        }

        dsDados = cDados.getFotosPainelGerenciamento("St_BeloMonte", codigoEntidade, 1, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            int index = 0;
            foreach (DataRow dr in dsDados.Tables[0].Rows)
            {
                MemoryStream ms = new MemoryStream((byte[])dr["Foto"]);

                Size sz = new Size(200, 140);

                System.Drawing.Image image = resizeImage(System.Drawing.Image.FromStream(ms), sz);

                MemoryStream ms2 = new MemoryStream();

                image.Save(ms2, ImageFormat.Jpeg);

                img002.ContentBytes = ms2.ToArray(); 
                img002.ToolTip = dr["DescricaoFoto"].ToString();
                img002.Cursor = "Pointer";
                img002.ClientSideEvents.Click = "function(s, e) {abreFotosSitio('St_BeloMonte', 'Sítio Belo Monte');}";
                index++;
            }
        }

        dsDados = cDados.getFotosPainelGerenciamento("Infraestrutura", codigoEntidade, 1, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            int index = 0;
            foreach (DataRow dr in dsDados.Tables[0].Rows)
            {
                MemoryStream ms = new MemoryStream((byte[])dr["Foto"]);

                Size sz = new Size(200, 140);

                System.Drawing.Image image = resizeImage(System.Drawing.Image.FromStream(ms), sz);

                MemoryStream ms2 = new MemoryStream();

                image.Save(ms2, ImageFormat.Jpeg);

                img003.ContentBytes = ms2.ToArray(); 
                img003.ToolTip = dr["DescricaoFoto"].ToString();
                img003.Cursor = "Pointer";
                img003.ClientSideEvents.Click = "function(s, e) {abreFotosSitio('Infraestrutura', 'Sítio Infraestrutura');}";
                index++;
            }
        }

        dsDados = cDados.getFotosPainelGerenciamento("Cnl_Derivacao", codigoEntidade, 1, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            int index = 0;
            foreach (DataRow dr in dsDados.Tables[0].Rows)
            {
                MemoryStream ms = new MemoryStream((byte[])dr["Foto"]);

                Size sz = new Size(200, 140);

                System.Drawing.Image image = resizeImage(System.Drawing.Image.FromStream(ms), sz);

                MemoryStream ms2 = new MemoryStream();

                image.Save(ms2, ImageFormat.Jpeg);

                img004.ContentBytes = ms2.ToArray();
                img004.ToolTip = dr["DescricaoFoto"].ToString();
                img004.Cursor = "Pointer";
                img004.ClientSideEvents.Click = "function(s, e) {abreFotosSitio('Cnl_Derivacao', 'Sítio Canais de Derivação, Transposição e Enchimento');}";
                index++;
            }
        }

        dsDados = cDados.getFotosPainelGerenciamento("Diques", codigoEntidade, 1, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            int index = 0;
            foreach (DataRow dr in dsDados.Tables[0].Rows)
            {
                MemoryStream ms = new MemoryStream((byte[])dr["Foto"]);

                Size sz = new Size(200, 140);

                System.Drawing.Image image = resizeImage(System.Drawing.Image.FromStream(ms), sz);

                MemoryStream ms2 = new MemoryStream();

                image.Save(ms2, ImageFormat.Jpeg);

                img005.ContentBytes = ms2.ToArray(); 
                img005.ToolTip = dr["DescricaoFoto"].ToString();
                img005.Cursor = "Pointer";
                img005.ClientSideEvents.Click = "function(s, e) {abreFotosSitio('Diques', 'Sítio Diques');}";
                index++;
            }
        }        
    }

    private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
    {
        Bitmap b = new Bitmap(size.Width, size.Height);
        Graphics g = Graphics.FromImage((System.Drawing.Image)b);
        //g.InterpolationMode = InterpolationMode.High;
        g.CompositingQuality = CompositingQuality.HighSpeed;
        g.SmoothingMode = SmoothingMode.HighSpeed;
        g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
        g.Dispose();

        return (System.Drawing.Image)b;
    }

    private void defineAjuda()
    {
        string textoAjuda = @"<p class=""ecxmsolistparagraph"" 
            style=""margin: 0cm; margin-bottom: .0001pt; text-indent: 0pt; background: white"">
            <u>
            <span style=""font-size:11.5pt;
font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;"">Curva S Física</span></u><span 
                style=""font-size:11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""><o:p></o:p></span></p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Objetivo/descrição</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Apresentar a Curva S 
            Física da UHE Belo Monte, isto é, consolidada de todos os Sítios. <o:p></o:p>
            </span>
        </p>
        <p>
            <b style=""mso-bidi-font-weight:normal"">
            <span style=""font-size:11.5pt;font-family:
&quot;Calibri&quot;,&quot;sans-serif&quot;"">Fonte das informações</span></b><span style=""font-size:
11.5pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;""> - Curva S Física da UHE Belo 
            Monte.<o:p></o:p></span></p>
        <b style=""mso-bidi-font-weight:normal"">
        <span style=""font-size:11.5pt;
line-height:115%;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-bidi-font-family:&quot;Times New Roman&quot;;
mso-bidi-theme-font:minor-bidi;mso-ansi-language:PT-BR;mso-fareast-language:
EN-US;mso-bidi-language:AR-SA"">Área responsável</span></b><span style=""font-size:11.5pt;line-height:115%;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:minor-latin;mso-bidi-font-family:
&quot;Times New Roman&quot;;mso-bidi-theme-font:minor-bidi;mso-ansi-language:PT-BR;
mso-fareast-language:EN-US;mso-bidi-language:AR-SA""> - Superintendência de Planejamento 
        do Empreendimento / DC. </span>";

        hfAjuda.Set("TextoAjuda", textoAjuda);
    }
}

