using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using System.Collections.Specialized;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.Hosting;

public partial class espacoTrabalho_galeriaFotos : System.Web.UI.Page
{
    private dados cDados;
    public String tempoTransicao = "2000";
    private string codigoSitio;
    private int codigoEntidade;
    private string Ownerdb;
    private string bancodb;
    public string todasImagens = "";
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
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoSitio = Request.QueryString["CR"];
        Ownerdb = cDados.getDbOwner();
        bancodb = cDados.getDbName();

        DataSet dsParam = cDados.getParametrosSistema(codigoEntidade, "IntervaloTransicaoFilmeImagens");
        tempoTransicao = dsParam.Tables[0].Rows[0]["IntervaloTransicaoFilmeImagens"].ToString();

        string comando = string.Format(@"
        select c.Anexo from f_uhe_getFotosSequenciaEvolutiva({2}, '{3}') as f 
        inner join {0}.{1}.AnexoVersao as a on f.CodigoFoto = a.codigoAnexo 
        inner join {0}.{1}.ConteudoAnexo as c on a.codigoSequencialAnexo = c.codigoSequencialAnexo where c.Anexo is not null"
        , bancodb, Ownerdb, codigoEntidade, codigoSitio);

        DataTable dtCodigos = cDados.getDataSet(comando).Tables[0];
        string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + ".jpg"; 
        int numeroImagem = 1;
        if (dtCodigos.Rows.Count > 0)
        {
            foreach (DataRow row in dtCodigos.Rows)
            {
                MemoryStream ms = new MemoryStream((Byte[])row["Anexo"]);
                //Size sz = new Size(500, 400);
                string nomeImagem = numeroImagem + "_" + dataHora;
                string enderecoSalvar = HostingEnvironment.ApplicationPhysicalPath + "ArquivosTemporarios\\" + nomeImagem;
                numeroImagem ++;
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);//resizeImage(System.Drawing.Image.FromStream(ms), sz);                
                image.Save(enderecoSalvar);
                todasImagens += string.Format(@"<img src='../ArquivosTemporarios/{0}' />", nomeImagem);
                ms.Flush();
                ms.Dispose();
            }
        }
    }

    //private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
    //{
    //    Bitmap b = new Bitmap(size.Width, size.Height);
    //    Graphics g = Graphics.FromImage((System.Drawing.Image)b);
    //    //g.InterpolationMode = InterpolationMode.High;
    //    g.CompositingQuality = CompositingQuality.HighSpeed;
    //    g.SmoothingMode = SmoothingMode.HighSpeed;
    //    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
    //    g.Dispose();

    //    return (System.Drawing.Image)b;
    //}
}
