using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.Web;

public partial class _VisaoMaster_PlanilhaOrcamentoAprovadoCA : System.Web.UI.Page
{
    dados cDados;

    private int idUsuarioLogado;
    private int codigoEntidade;
    private string resolucaoCliente = "";
    private int alturaPrincipal = 0;

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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        carregaGvDados();
        definicoesIniciais();
        cDados.aplicaEstiloVisual(this.Page);
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int largura1 = 0;
        int altura1 = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura1, out altura1);

        if (Request.QueryString["Popup"] + "" == "S")
        {
            gvDados.Settings.VerticalScrollableHeight = altura1 - 370;
        }
        else
        {
            alturaPrincipal = altura1;
            int altura = (alturaPrincipal - 135);
            if (altura1 > 0)
                gvDados.Settings.VerticalScrollableHeight = altura1 - 330;
        }
    }

    private void definicoesIniciais()
    {
        ASPxImage imgImportar = gvDados.FindTitleTemplateControl("imgImportar") as ASPxImage;
        ASPxImage imgAtualizar = gvDados.FindTitleTemplateControl("imgAtualizar") as ASPxImage;

        if (Request.QueryString["Popup"] + "" == "S")
        {
            imgImportar.ClientVisible = false;
            imgAtualizar.ClientVisible = false;
            btnFechar.ClientVisible = true;
            //gvDados.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        }
        else
        {
            imgImportar.ClientVisible = true;
            imgAtualizar.ClientVisible = true;
            btnFechar.ClientVisible = false;

            string linkOpcao = string.Format("./../portalDesktop/NE/LerExcel.application?Us={0}&UrlApp={1}", idUsuarioLogado, Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf("_VisaoMaster")) + "wsPortal.asmx");

            string eventoBotaoEditarCrono = "window.open('" + linkOpcao + "', '_self');";

            imgImportar.ClientSideEvents.Click = "function(s, e) {" + eventoBotaoEditarCrono + "}";
        }        
    }

    private void carregaGvDados()
    {
        DataSet ds = cDados.getPlanilhaOrcamentoAprovadoCA();

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();

            if (cDados.DataTableOk(ds.Tables[0]) && !IsPostBack)
            {
                defineAnoNomeColunas(ds.Tables[0].Rows[0]["Ano"].ToString());
            }
        }
    }

    private void defineAnoNomeColunas(string ano)
    {
        gvDados.Columns["descricao"].Caption = "ORÇAMENTO " + ano + " - ACOMPANHAMENTO";
        //gvDados.Columns["DescricaoConta"].Caption = "TOTAL NORTE ENERGIA" + Environment.NewLine + "(Valores em R$ mil)";

        for (int i = 1; i <= 12; i++)
        {
            gvDados.Columns["mes" + i].Caption = gvDados.Columns["mes" + i].Caption + " " + ano;
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.VisibleIndex != -1)
        {
            int nivel = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Nivel").ToString());

            char conta = gvDados.GetRowValues(e.VisibleIndex, "CodigoConta").ToString()[0];

            if (nivel != 0)
            {
                if(e.DataColumn.FieldName == "DescricaoConta")
                    e.Cell.Style.Add("padding-left", (20 * nivel) + "px");

                if (nivel == 1)
                {
                    if (conta == '1')
                    {
                        e.Cell.Style.Add("Color", "#000000");
                        e.Cell.Style.Add("background-color", "#CCE6FF");
                    }
                    else
                    {
                        e.Cell.Style.Add("Color", "#000000");
                        e.Cell.Style.Add("background-color", "#FFEAEA");
                    }
                }
            }
            else
            {
                if (e.VisibleIndex == 0)
                {
                    e.Cell.Style.Add("Color", "#000000");
                    e.Cell.Style.Add("font-weight", "bold");
                    e.Cell.Style.Add("background-color", "#FFFF00");
                }
                else
                {
                    if (conta == '1')
                    {
                        e.Cell.Style.Add("Color", "#FFFFFF");
                        e.Cell.Style.Add("font-weight", "bold");
                        e.Cell.Style.Add("background-color", "#0E6BC9");
                    }
                    else
                    {
                        e.Cell.Style.Add("Color", "#000000");
                        e.Cell.Style.Add("font-weight", "bold");
                        e.Cell.Style.Add("background-color", "#FFC4C4");
                    }
                }
            }
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        XlsExportOptionsEx opcoes = new XlsExportOptionsEx();

        opcoes.TextExportMode = TextExportMode.Text;

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
        if (e.RowType == DevExpress.Web.GridViewRowType.Header)
        {
            if (e.Column is GridViewBandColumn)
            {
                if (e.Column.Name.Contains("mes"))
                {
                    e.BrickStyle.ForeColor = Color.Black;
                    e.BrickStyle.BackColor = Color.White;
                    e.BrickStyle.Font = new Font("Arial", 18, FontStyle.Bold);
                    e.BrickStyle.Padding = new PaddingInfo(80, 80, 80, 80);
                    e.BrickStyle.Sides = BorderSide.Left;
                    e.BrickStyle.BorderColor = Color.Black;
                    e.BrickStyle.BorderWidth = 2;
                }
                else
                {
                    e.BrickStyle.ForeColor = Color.FromArgb(31, 73, 125);
                    e.BrickStyle.BackColor = Color.FromArgb(220, 230, 241);
                    e.BrickStyle.Font = new Font("Arial", 16, FontStyle.Bold);
                    e.BrickStyle.Padding = new PaddingInfo(10, 10, 10, 10);
                    e.BrickStyle.TextAlignment = TextAlignment.BottomRight;
                }
            }
            else
            {
                e.BrickStyle.BackColor = Color.FromArgb(197, 217, 241);

                if (e.Column.Name == "DescricaoConta")
                {
                    e.BrickStyle.Font = new Font("Arial", 14, FontStyle.Bold);
                    e.BrickStyle.TextAlignment = TextAlignment.MiddleRight;
                    e.BrickStyle.ForeColor = Color.FromArgb(192, 80, 77);
                    e.Text = "TOTAL NORTE ENERGIA" + Environment.NewLine + "(Valores em R$ mil)";
                }
                else if (e.Column.Name.Contains("ValorOrcado"))
                {
                    e.BrickStyle.ForeColor = Color.FromArgb(192, 80, 77);
                    e.BrickStyle.Font = new Font("Arial", 11, FontStyle.Bold);
                    e.BrickStyle.Sides = BorderSide.Left;
                    e.BrickStyle.BorderColor = Color.Black;
                    e.BrickStyle.BorderWidth = 2;
                }
                else
                {
                    e.BrickStyle.ForeColor = Color.Black;
                    e.BrickStyle.Font = new Font("Arial", 11, FontStyle.Bold);
                }
            }
        }else
        {
            if (e.Column.Name != "DescricaoConta")
            {
                e.BrickStyle.TextAlignment = TextAlignment.MiddleRight;
            }

            e.BrickStyle.Font = new Font("Arial", 11);

            int nivel = int.Parse(gvDados.GetRowValues(e.VisibleIndex, "Nivel").ToString());

            char conta = gvDados.GetRowValues(e.VisibleIndex, "CodigoConta").ToString()[0];

            if (nivel != 0)
            {
                if (e.Column.Name == "DescricaoConta")
                {
                    string identacao = "";

                    for (int i = 0; i < nivel; i++)
                        identacao += "      ";

                    e.TextValue = identacao + e.Text;
                }
                               
                e.BrickStyle.ForeColor = Color.Black;
                
                if (nivel == 1)
                {
                    if (conta == '1')
                    {
                        e.BrickStyle.BackColor = Color.FromArgb(197, 217, 241);
                    }
                    else
                    {
                        e.BrickStyle.BackColor = Color.FromArgb(242, 220, 219);
                    }
                }
            }
            else
            {
                e.BrickStyle.Font = new Font("Arial", 11, FontStyle.Bold);                               
                
                if (e.VisibleIndex == 0)
                {
                    e.BrickStyle.ForeColor = Color.Black;
                    e.BrickStyle.BackColor = Color.Yellow;
                }
                else
                {
                    if (conta == '1')
                    {
                        e.BrickStyle.ForeColor = Color.White;
                        e.BrickStyle.BackColor = Color.FromArgb(79, 129, 189);
                    }
                    else
                    {
                        e.BrickStyle.ForeColor = Color.Black;
                        e.BrickStyle.BackColor = Color.FromArgb(230, 184, 183);
                    }
                }
            }
        }
    }
}