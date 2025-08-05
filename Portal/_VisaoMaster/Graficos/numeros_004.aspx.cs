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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using DevExpress.Web;

public partial class _VisaoMaster_Graficos_numeros_004 : System.Web.UI.Page
{
    dados cDados;
    int codigoEntidade, codigoCategoria;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoCategoria = int.Parse(Request.QueryString["CA"].ToString());
                
        defineLarguraTela();
        getInfo_Tabela1();
        getInfo_Tabela2();
        carregaFotosObra();
        cDados.aplicaEstiloVisual(this.Page);
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;

        //ASPxRoundPanel1.Width = 250;
        //ASPxRoundPanel1.ContentHeight = (altura - 192);
    }

    private void getInfo_Tabela1()
    {
        

        DataSet dsTurbina = cDados.getDataGeracaoTurbina(1, codigoEntidade);

        try
        {
            DateTime dataTurbina = (DateTime)dsTurbina.Tables[0].Rows[0][0];
            TimeSpan numeroDias = dataTurbina.Subtract(DateTime.Now);

            gauge.Value = numeroDias.Days.ToString();
        }
        catch {
            gauge.Value = null;
        }

        DataSet dsLO = cDados.getDataObtencaoLO(codigoEntidade);

        try
        {
            DateTime dataLO = (DateTime)dsLO.Tables[0].Rows[0][0];
            TimeSpan numeroDiasLO = dataLO.Subtract(DateTime.Now);

            gauge2.Value = numeroDiasLO.Days.ToString();
        }
        catch
        {
            gauge2.Value = null;
        }
    }

    private void getInfo_Tabela2()
    {
        DataSet dsParametro = cDados.getInformacoesPainelGerenciamento(codigoEntidade, codigoCategoria, "");

         if (cDados.DataSetOk(dsParametro))
         {
             if (cDados.DataTableOk(dsParametro.Tables[0]))
             {
                 DataRow dr = dsParametro.Tables[0].Rows[0];

                 imgFisicoUHE.ImageUrl = string.Format(@"~/imagens/Fisico{0}.png", dr["CorFisicoUHE"]);
                 imgFinanceiroUHE.ImageUrl = string.Format(@"~/imagens/Financeiro{0}.png", dr["CorFinanceiroUHE"]);
                 lblDesempenhoUHE.Text = string.Format("{0:n2}%", dr["DesempenhoUHE"]);
             }
         }
    }

    private void carregaFotosObra()
    {
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        DataSet dsDados = cDados.getFotosPainelGerenciamento("UHE_Principal", codigoEntidade, 9, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            int index = 1;
            foreach (DataRow dr in dsDados.Tables[0].Rows)
            {
                ASPxBinaryImage img = Page.FindControl("img00" + index) as ASPxBinaryImage;

                img.Value = cDados.GetImageThumbnail(dr["Foto"], 70, 40);
                img.ToolTip = dr["DescricaoFoto"].ToString();
                img.Cursor = "Pointer";

                string corpoFuncao = string.Format(@"
                                                     window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/GaleriaFotos.aspx?NumeroFotos=999&CR=UHE_Principal&CF={0}', ""Últimas Fotos UHE BeloMonte"", 565, 490, """", null);
                                                     ", dr["CodigoFoto"].ToString());

                img.ClientSideEvents.Click = "function(s, e) {" + corpoFuncao + "}";
                index++;
            }
        }
    }   
}
