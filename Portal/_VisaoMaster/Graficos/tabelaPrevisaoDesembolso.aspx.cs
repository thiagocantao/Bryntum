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
using System.Drawing;
using DevExpress.Web;

public partial class _VisaoMaster_Graficos_tabelaPrevisaoDesembolso : System.Web.UI.Page
{
    dados cDados;
    
    int codigoEntidade;

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

        cDados.aplicaEstiloVisual(this);

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        ajustaCabecalhoGrid();
        carregaGrid();

        if (Request.QueryString["Altura"] != null && Request.QueryString["Altura"].ToString() != "")
        {
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["Altura"].ToString()) - 155;
        }
    }

    private void ajustaCabecalhoGrid()
    {
        int diasTolerancia = cDados.getDiasToleranciaUHE();

        DateTime data = DateTime.Now.Day < diasTolerancia ? DateTime.Now.AddMonths(-2) : DateTime.Now.AddMonths(-1);

        int mesatual = data.Month;
        if (mesatual == 2 || mesatual == 3 || mesatual == 4)
        {
            //Se o mes atual for janeiro fevereiro e março
            gvDados.Columns["PrevistoTri"].Caption = "Prev. 1º Trim (R$ Mil)";
            gvDados.Columns["RealTri"].Caption = "Real. 1º Trim (R$ Mil)";

            gvDados.Columns["PrevistoMes1"].Caption = "Prev. Jan (R$ Mil)";
            gvDados.Columns["RealMes1"].Caption = "Real. Jan (R$ Mil)";
            gvDados.Columns["PrevistoMes2"].Caption = "Prev. Fev (R$ Mil)";
            gvDados.Columns["RealMes2"].Caption = "Real. Fev (R$ Mil)";
            gvDados.Columns["PrevistoMes3"].Caption = "Prev. Mar (R$ Mil)";
            gvDados.Columns["RealMes3"].Caption = "Real. Mar (R$ Mil)";
        }

        else if (mesatual == 5 || mesatual == 6 || mesatual == 7)
        {
            //Se o mes atual for abril maio e junho
            gvDados.Columns["PrevistoTri"].Caption = "Prev. 2º Trim (R$ Mil)";
            gvDados.Columns["RealTri"].Caption = "Real. 2º Trim (R$ Mil)";

            gvDados.Columns["PrevistoMes1"].Caption = "Prev. Abr (R$ Mil)";
            gvDados.Columns["RealMes1"].Caption = "Real. Abr (R$ Mil)";
            gvDados.Columns["PrevistoMes2"].Caption = "Prev. Mai (R$ Mil)";
            gvDados.Columns["RealMes2"].Caption = "Real. Mai (R$ Mil)";
            gvDados.Columns["PrevistoMes3"].Caption = "Prev. Jun (R$ Mil)";
            gvDados.Columns["RealMes3"].Caption = "Real. Jun (R$ Mil)";
        }
        else if (mesatual == 8 || mesatual == 9 || mesatual == 10)
        {
            //Se o mes atual for julho agosto e setembro
            gvDados.Columns["PrevistoTri"].Caption = "Prev. 3º Trim (R$ Mil)";
            gvDados.Columns["RealTri"].Caption = "Real. 3º Trim (R$ Mil)";

            gvDados.Columns["PrevistoMes1"].Caption = "Prev. Jul (R$ Mil)";
            gvDados.Columns["RealMes1"].Caption = "Real. Jul (R$ Mil)";
            gvDados.Columns["PrevistoMes2"].Caption = "Prev. Ago (R$ Mil)";
            gvDados.Columns["RealMes2"].Caption = "Real. Ago (R$ Mil)";
            gvDados.Columns["PrevistoMes3"].Caption = "Prev. Set (R$ Mil)";
            gvDados.Columns["RealMes3"].Caption = "Real. Set (R$ Mil)";
        }
        else if (mesatual == 11 || mesatual == 12 || mesatual == 1)
        {
            //Se o mes atual for outubro novembro e dezembro
            gvDados.Columns["PrevistoTri"].Caption = "Prev. 4º Trim (R$ Mil)";
            gvDados.Columns["RealTri"].Caption = "Prev. 4º Trim (R$ Mil)";

            gvDados.Columns["PrevistoMes1"].Caption = "Prev. Out (R$ Mil)";
            gvDados.Columns["RealMes1"].Caption = "Real. Out (R$ Mil)";
            gvDados.Columns["PrevistoMes2"].Caption = "Prev. Nov (R$ Mil)";
            gvDados.Columns["RealMes2"].Caption = "Real. Nov (R$ Mil)";
            gvDados.Columns["PrevistoMes3"].Caption = "Prev. Dez (R$ Mil)";
            gvDados.Columns["RealMes3"].Caption = "Real. Dez (R$ Mil)";
        }

    }

    private void carregaGrid()
    {
        int diasTolerancia = cDados.getDiasToleranciaUHE();

        DateTime data = DateTime.Now.Day < diasTolerancia ? DateTime.Now.AddMonths(-2) : DateTime.Now.AddMonths(-1);
        string codigoArea = Request.QueryString["CA"] == null ? "0" : Request.QueryString["CA"].ToString();
        int ano = data.Year;
        int trimestre = Convert.ToInt32(Math.Round(Convert.ToDouble(data.Month) / 3 + 0.25));

        string comandoSQL = string.Format(@"
        BEGIN 
        /* PARÂMETROS DA CONSULTA !!! */
        DECLARE @CodigoEntidade Int,
                @CodigoCategoria Int,
                @Ano Int,
                @Trimestre Tinyint
        /* Atribuição dos parâmetros da consulta! */
        SET @CodigoEntidade  = {0}
        SET @CodigoCategoria = {1}
        SET @Ano = {2}
        SET @Trimestre = {3}
        DECLARE @tblRetorno TABLE
        (CodigoConta Int,
         Tipo Varchar(100),
         Grupo Varchar(200),
         Conta Varchar(200),
         PrevistoTri Decimal(25,4),
         RealTri Decimal(25,4),
         PrevistoMes1 Decimal(25,4),
         RealMes1 Decimal(25,4),
         PrevistoMes2 Decimal(25,4),
         RealMes2 Decimal(25,4),
         PrevistoMes3 Decimal(25,4),
         RealMes3 Decimal(25,4))    
         /* Insere as informações principais na tabela de retorno */
         INSERT INTO @tblRetorno
         (CodigoConta,
          Tipo,
          Grupo,
          Conta,
          PrevistoTri,
          RealTri,
          PrevistoMes1,
          RealMes1,
          PrevistoMes2,
          RealMes2,
          PrevistoMes3,
          RealMes3)
         SELECT CodigoConta,
         Tipo,
         Grupo,
         NomeConta,
         SUM(CASE WHEN Ordem = 0 THEN ValorPrevisto  ELSE 0 END),
         SUM(CASE WHEN Ordem = 0 THEN ValorRealizado ELSE 0 END),
         SUM(CASE WHEN Ordem = 1 THEN ValorPrevisto  ELSE 0 END),
         SUM(CASE WHEN Ordem = 1 THEN ValorRealizado ELSE 0 END),
         SUM(CASE WHEN Ordem = 2 THEN ValorPrevisto  ELSE 0 END),
         SUM(CASE WHEN Ordem = 2 THEN ValorRealizado ELSE 0 END),
         SUM(CASE WHEN Ordem = 3 THEN ValorPrevisto  ELSE 0 END),
         SUM(CASE WHEN Ordem = 3 THEN ValorRealizado ELSE 0 END)
   FROM f_uhe_ListaPrevisaoDesembolso(@CodigoEntidade,@CodigoCategoria,@Ano,@Trimestre)
  GROUP BY CodigoConta,
           Tipo,
           Grupo,
           NomeConta           
  SELECT * FROM @tblRetorno ORDER BY Tipo, Grupo, Conta         
END ", codigoEntidade, int.Parse(codigoArea), ano, trimestre);

        DataSet ds = cDados.getDataSet(comandoSQL);// cDados.getListaPrevisaoDesembolso(codigoEntidade, int.Parse(codigoArea), ano, trimestre);

        gvDados.DataSource = ds;
        gvDados.DataBind();
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
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
    }
    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.GridViewRowType.Group)
            e.Row.BackColor = Color.FromName("#EBEBEB");
    }
}
