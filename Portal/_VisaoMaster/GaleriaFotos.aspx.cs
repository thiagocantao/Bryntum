using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

public partial class _VisaoMaster_GaleriaFotos : System.Web.UI.Page
{
    dados cDados;
    DataSet dsDados;

    int codigoProjeto = -1;

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
        cDados.aplicaEstiloVisual(this);
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

        int qtdFotos = int.Parse(Request.QueryString["NumeroFotos"].ToString());

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

            dsDados = cDados.getCodigosFotosPainelProjeto(codigoProjeto, "");
        }
        else
        {

            string codigoReservado = Request.QueryString["CR"] == null ? "" : Request.QueryString["CR"].ToString();
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            if (codigoReservado == "Diques")
                dsDados = cDados.getFotosCanaisDiques(codigoEntidade, "");
            else if (codigoReservado == "Cnl_Transp" || codigoReservado == "Cnl_Derivacao")
                dsDados = cDados.getFotosCanalDerivacao(codigoEntidade, "");
            else
                dsDados = cDados.getCodigosFotosPainelGerenciamento(codigoReservado, codigoEntidade, qtdFotos, "");

            pnFotos.JSProperties["cp_NumeroImagem"] = 0;
        }

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataRow dr;

            dr = dsDados.Tables[0].Rows[0];

            if (Request.QueryString["CF"] != null && Request.QueryString["CF"].ToString() != "")
            {
                DataRow[] drs = dsDados.Tables[0].Select("CodigoFoto=" + Request.QueryString["CF"].ToString());

                dr = drs[0];

                int i = 0;

                for (i = 0; i < dsDados.Tables[0].Rows.Count; i++)
                {
                    if (dsDados.Tables[0].Rows[i]["CodigoFoto"].ToString() == Request.QueryString["CF"].ToString())
                    {
                        pnFotos.JSProperties["cp_NumeroImagem"] = i;
                        break;
                    }
                }

                //if (drs.Length > 0)
                //{
                //    DataRow newRow = dsDados.Tables[0].NewRow();
                //    newRow.ItemArray = drs[0].ItemArray; // copy data
                //    dsDados.Tables[0].Rows.Remove(drs[0]);
                //    dsDados.Tables[0].Rows.InsertAt(newRow, 0);
                //}
            }

            if (!IsPostBack)
            {
                string nomeArquivo = "";
                byte[] conteudoanexoEmBytes = cDados.getConteudoAnexo(int.Parse(dr["CodigoFoto"].ToString()), null, ref nomeArquivo, "BD");
                if(conteudoanexoEmBytes == null)
                {
                    return;
                }
                MemoryStream ms = new MemoryStream(conteudoanexoEmBytes);

                Size sz = new Size(500, 360);

                System.Drawing.Image image = resizeImage(System.Drawing.Image.FromStream(ms), sz);

                MemoryStream ms2 = new MemoryStream();

                image.Save(ms2, ImageFormat.Jpeg);

                imgZoom.ContentBytes = ms2.ToArray();

                txtDescricaoFoto.Text = dr["DescricaoFoto"].ToString();

            }
        }

        pnFotos.JSProperties["cp_NumeroFotos"] = dsDados.Tables[0].Rows.Count - 1;
        
    }

    protected void pnFoto_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter != "")
        {
            int numeroFoto = int.Parse(e.Parameter.ToString());

            if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
            {

                DataRow dr = dsDados.Tables[0].Rows[numeroFoto];

                string nomeArquivo = "";

                MemoryStream ms = new MemoryStream(cDados.getConteudoAnexo(int.Parse(dr["CodigoFoto"].ToString()), null, ref nomeArquivo, "BD"));

                Size sz = new Size(500, 360);

                System.Drawing.Image image = resizeImage(System.Drawing.Image.FromStream(ms), sz);

                MemoryStream ms2 = new MemoryStream();
                ms.Flush();
                ms.Dispose();

                image.Save(ms2, ImageFormat.Jpeg);

                imgZoom.ContentBytes = ms2.ToArray();
                pnFotos.JSProperties["cp_DescricaoFotos"] = dr["DescricaoFoto"].ToString();

                ms2.Flush();
                ms2.Dispose();

            }

            pnFotos.JSProperties["cp_NumeroFotos"] = dsDados.Tables[0].Rows.Count - 1;
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
}
