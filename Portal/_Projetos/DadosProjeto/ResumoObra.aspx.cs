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
using System.Globalization;
using System.Drawing;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_ResumoProjeto : System.Web.UI.Page
{
    dados cDados;

    public int codigoProjeto = 0;
    public int larguraGrafico = 400;
    public int alturaGraficos = 166;
    public string telaEspaco1 = "grafico_018", telaEspaco2 = "grafico_020";
    int codigoEntidade;

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

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {       
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/FusionCharts.js?v=1""></script>"));
        this.TH(this.TS("FusionCharts"));
        if (Request.QueryString["IDProjeto"] != null && Request.QueryString["IDProjeto"].ToString() != "")
        {
            codigoProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            

            cDados.VerificaAcessoTela(this, codigoUsuario, codigoEntidade, codigoProjeto, "null", "PR", 0, "null", "PR_CnsStt");
        }
        
        defineTamanhoObjetos();
        preencheCampos();
        carregaFotosObra();

        telaEspaco1 = string.Format(@"./graficos/{0}.aspx?FRM=frm01&CP={1}&IDProjeto={1}&TipoTela=P", telaEspaco1, codigoProjeto);
        telaEspaco2 = string.Format(@"./graficos/{0}.aspx?FRM=frm02&CP={1}&IDProjeto={1}&TipoTela=P", telaEspaco2, codigoProjeto);

        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidade, "labelPrevistoParcelaContrato");
        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if (dsParametros.Tables[0].Rows[0]["labelPrevistoParcelaContrato"].ToString() != "")
            {
                lblPrevistoParcela.Text = dsParametros.Tables[0].Rows[0]["labelPrevistoParcelaContrato"].ToString() + ":";
            }
        }

        string estiloFooter = "dxgvControl dxgvGroupPanel";

        string cssPostfix = "", cssPath = "";

        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);

        if (cssPostfix != "")
            estiloFooter = "dxgvControl_" + cssPostfix + " dxgvGroupPanel_" + cssPostfix;

        tbBotoes.Attributes.Add("class", estiloFooter);

        tbBotoes.Style.Add("padding", "3px");

        tbBotoes.Style.Add("border-collapse", "collapse");

        tbBotoes.Style.Add("border-bottom", "none");
    }

    //Preenche os numeros do projeto
    private void preencheCampos()
    {        
        DataSet dsDados = cDados.getDadosGeraisObra(codigoProjeto, codigoEntidade, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            DataRow dr = dsDados.Tables[0].Rows[0];

            txtProjeto.Text = dr["NomeProjeto"].ToString();

            txtInicioPrevisto.Text = string.Format("{0:dd/MM/yyyy}", dr["InicioPrevisto"]);
            txtTerminoPrevisto.Text = string.Format("{0:dd/MM/yyyy}", dr["TerminoPrevisto"]); 
            txtInicioReal.Text = string.Format("{0:dd/MM/yyyy}", dr["InicioReal"]);
            txtTerminoReprogramado.Text = string.Format("{0:dd/MM/yyyy}", dr["TerminoPrevisto"]) != string.Format("{0:dd/MM/yyyy}", dr["TerminoReprogramado"]) ? string.Format("{0:dd/MM/yyyy}", dr["TerminoReprogramado"]) : "";
            txtMunicipio.Text = dr["NomeMunicipio"].ToString(); 
            txtContratada.Text = dr["Contratada"].ToString();
            txtNumeroContrato.Text = dr["NumeroContrato"].ToString();
            txtInicioVigencia.Text = string.Format("{0:dd/MM/yyyy}", dr["DataInicio"]);
            txtTerminoVigencia.Text = string.Format("{0:dd/MM/yyyy}", dr["DataTermino"]);
            txtValorContratado.Value = dr["ValorContrato"];
            txtValorMedido.Value = dr["ValorMedido"];
            txtSaldoContratual.Value = dr["SaldoContratual"];

            if (dr["UltimaAtualizacao"] != null && dr["UltimaAtualizacao"].ToString() != "")
            {
                txtAtualizacao.Text = string.Format("{0:dd/MM/yyyy}", dr["UltimaAtualizacao"]);

                DataSet dsParametro = cDados.getParametrosSistema("diasAtualizacaoProjeto");
                double diasAtualizacao = 0;

                if (cDados.DataSetOk(dsParametro) && cDados.DataTableOk(dsParametro.Tables[0]))
                {
                    if (dsParametro.Tables[0].Rows[0]["diasAtualizacaoProjeto"] + "" != "")
                    {
                        diasAtualizacao = double.Parse(dsParametro.Tables[0].Rows[0]["diasAtualizacaoProjeto"] + "");
                    }
                }

                if (((DateTime)dr["UltimaAtualizacao"]).AddDays(diasAtualizacao) < DateTime.Now)
                    txtAtualizacao.DisabledStyle.ForeColor = Color.Red;

                formataLinkDataAtualizacao();
            }
        }

        DataSet dsComentarios = cDados.getUltimoComentarioFiscalizacaoObra(codigoProjeto, "");

        if (cDados.DataSetOk(dsComentarios) && cDados.DataTableOk(dsComentarios.Tables[0]))
        {
            DataRow dr = dsComentarios.Tables[0].Rows[0];
            htmlUltimoComentario.Html = dr["Analise"].ToString();

            lblUltimoComentario.Text = string.Format("Comentário feito em {0:dd/MM/yyyy} por {1}", dr["DataInclusao"], dr["NomeUsuario"].ToString());
        }
    }

    private void formataLinkDataAtualizacao()
    {
        DataSet dsDados = cDados.getItensProjetoAtualizacaoMonitorada(codigoProjeto, "");
        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {
            if (dsDados.Tables[0].Rows.Count > 1)
            {
                txtAtualizacao.Style.Add("cursor", "pointer");
                txtAtualizacao.Font.Underline = true;
                txtAtualizacao.Attributes.Add("onclick", "pcEventosAtlPrj.Show();");
                gvEventosAtlPrj.Settings.ShowFilterRow = false;
                gvEventosAtlPrj.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;
                gvEventosAtlPrj.SettingsLoadingPanel.Mode = DevExpress.Web.GridViewLoadingPanelMode.Disabled;
                gvEventosAtlPrj.DataSource = dsDados;
                gvEventosAtlPrj.DataBind();
            }
        }
    }

    private void carregaFotosObra()
    {
        DataSet dsDados = cDados.getFotosObra(codigoProjeto, "");

        if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
        {                   
            int index = 0;
            foreach(DataRow dr in dsDados.Tables[0].Rows)
            {
                ASPxBinaryImage img = ((ASPxBinaryImage)this.FindControl("img00" + (index + 1)));
                img.BinaryStorageMode = DevExpress.Web.BinaryStorageMode.Session;
                img.Value = cDados.GetImageThumbnail(dr["Anexo"], 180, 120);
                img.ToolTip = dr["DescricaoAnexo"].ToString();
                img.Cursor = "Pointer";

                string corpoFuncao = string.Format(@"
                                                     window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/GaleriaFotos.aspx?NumeroFotos=999&CP={1}&CF={0}', ""Fotos"", 565, 490, """", null);
                                                     ", dr["CodigoAnexo"].ToString()
                                                      , codigoProjeto);

                img.ClientSideEvents.Click = "function(s, e) {" + corpoFuncao + "}";
                index++;
            }            
        }
    }

    private void defineTamanhoObjetos()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); 
        
        int larguraPaineis = ((largura) / 2 - 105);
        int alturaPaineis = altura - 217;
        
        larguraGrafico = larguraPaineis;

        alturaGraficos = (altura - 212) / 2;
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "FormAtlObr");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "FormAtlObr", "Atualização Obra", this);
    }

    #endregion    
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
    }
}

    
