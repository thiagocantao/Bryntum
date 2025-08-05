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

public partial class _VisaoMaster_visaoCorporativa : System.Web.UI.Page
{
    dados cDados;
    public string alturaTela = "", urlVisao = "";
    int codigoEntidade, codigoUsuarioResponsavel;
    bool acessaGestaoEmpreendimento = false, acessaEAP = false, acessaVisaoGlobal = false, acessaPainelDesempenho = false, acessaVisaoCusto = false;
    bool acessaPimental = false, acessaBeloMonte = false, acessaInfra = false, acessaDerivacao = false, acessaReservatorios = false;
    bool acessaIndicadores = false;
    string urlTelaInicial = "";
    int codigoAnexo = -1, codigoImagemEscavacao = -1;
    int larguraImagem = 0, alturaImagem = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // =========================== Verifica se a sessão existe INICIO ========================
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioResponsavel, codigoEntidade, codigoEntidade, "NULL", "EN", 0, "NULL", "EN_AcsPnlMst");
        }

        getAcessoPaineis();

        if (urlTelaInicial == "")
        {
            try
            {
                Response.Redirect("~/erros/SemAcessoNoMaster.aspx");
            }
            catch
            {
                Response.RedirectLocation = cDados.getPathSistema() + "erros/SemAcessoNoMaster.aspx";
                Response.End();
            }
        }

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);
            cDados.excluiNiveisAbaixo(0);
            cDados.insereNivel(0, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "PNL_AN", "ENT", -1, "Adicionar Painel aos Favoritos");
        }

        pMenu.Items.FindByName("0").Text = lblTituloTela.Text;
        lblTituloTela.JSProperties["cp_TextoMenu"] = lblTituloTela.Text;

        defineLarguraTela();
        this.Title = cDados.getNomeSistema();
        imgAnterior.ClientVisible = false;
        imgProximo.ClientVisible = false;

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var urlProximo = ''; var urlAnterior = ''</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" src=""../scripts/PainelGerenciamento.js"" language=""javascript""></script>"));
        this.TH(this.TS("PainelGerenciamento"));
        if(!IsPostBack)
            carregaComboAreas();

        divAjuda.Controls.Add(cDados.getLiteral(@"<span id='spanAjuda'></span>"));
        if(urlTelaInicial != "")
            urlVisao = cDados.getPathSistema() + "_VisaoMaster/Graficos/" + urlTelaInicial.Split(';')[0] + ".aspx?CA=" + (ddlArea2.Value == null ? "-1" : ddlArea2.Value.ToString()) + "&NA=" + (ddlArea2.Value + "" != "-1" ? ddlArea2.Text : "");

        defineImagemBriefing();
        codigoImagemEscavacao = defineImagemEvolucao("Cnl_Derivacao");

        imgFotosEscavacao.JSProperties["cp_MostraImagemEscavacao"] = codigoImagemEscavacao == -1 ? "N" : "S";
        imgSequenciaEvolutiva.JSProperties["cp_MostraImagemEvolucaoPimental"] = possuiImagensSequenciaEvolucao("St_Pimental") ? "S" : "N";
        imgSequenciaEvolutiva.JSProperties["cp_MostraImagemEvolucaoBeloMonte"] = possuiImagensSequenciaEvolucao("St_BeloMonte") ? "S" : "N";

        int diasTolerancia = cDados.getDiasToleranciaUHE();

        DateTime data = DateTime.Now.Day < diasTolerancia ? DateTime.Now.AddMonths(-2) : DateTime.Now.AddMonths(-1);

        lblDataReferencia.Text = string.Format("(*Dados referentes a {0:MM/yyyy})", data);
    }

    private void defineImagemBriefing()
    {
        DataSet dsAnexo = cDados.getDataSet(string.Format(@"SELECT ISNULL({0}.{1}.f_uhe_getUltimoRelatorioAcompanhamento({2}), -1) AS CodigoAnexo", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade));

        if (cDados.DataSetOk(dsAnexo) && cDados.DataTableOk(dsAnexo.Tables[0]))
            codigoAnexo = int.Parse(dsAnexo.Tables[0].Rows[0]["CodigoAnexo"].ToString());

        if (codigoAnexo == -1)
        {
            btnDownLoad.ClientEnabled = false;
            btnDownLoad.Image.Url = "~/imagens/botoes/btnPDFDes.png";
        }
    }

    private int defineImagemEvolucao(string sitio)
    {
        int codigoImagem = -1;

        DataSet dsAnexo = cDados.getDataSet(string.Format(@"SELECT ISNULL({0}.{1}.f_uhe_getFotoRegistroEvolucaoSitio({2}, '{3}'), -1) AS CodigoAnexo", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade, sitio));

        if (cDados.DataSetOk(dsAnexo) && cDados.DataTableOk(dsAnexo.Tables[0]))
            codigoImagem = int.Parse(dsAnexo.Tables[0].Rows[0]["CodigoAnexo"].ToString());

        return codigoImagem;
    }

    private bool possuiImagensSequenciaEvolucao(string sitio)
    {
        bool possuiImagens = false;

        DataSet dsAnexo = cDados.getDataSet(string.Format(@"SELECT TOP 1 CodigoFoto FROM {0}.{1}.f_uhe_getFotosSequenciaEvolutiva({2}, '{3}')", cDados.getDbName(), cDados.getDbOwner(), codigoEntidade, sitio));

        if (cDados.DataSetOk(dsAnexo) && cDados.DataTableOk(dsAnexo.Tables[0]))
            possuiImagens = true;

        return possuiImagens;
    }

    private void getAcessoPaineis()
    {
        DataSet ds = cDados.getAcessoObjetosPainelGerenciamento(codigoUsuarioResponsavel, codigoEntidade);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            acessaGestaoEmpreendimento = (bool)ds.Tables[0].Rows[0]["GestaoEmpreendimento"];
            acessaEAP = (bool)ds.Tables[0].Rows[0]["EAP"];
            acessaVisaoGlobal = (bool)ds.Tables[0].Rows[0]["VisaoGlobalUHE"];
            acessaPainelDesempenho = (bool)ds.Tables[0].Rows[0]["PainelDesempenho"];
            acessaVisaoCusto = (bool)ds.Tables[0].Rows[0]["VisaoCusto"];

            acessaPimental = (bool)ds.Tables[0].Rows[0]["Pimental"];
            acessaBeloMonte = (bool)ds.Tables[0].Rows[0]["BeloMonte"];
            acessaInfra = (bool)ds.Tables[0].Rows[0]["Infra"];
            acessaDerivacao = (bool)ds.Tables[0].Rows[0]["Derivacao"];
            acessaReservatorios = (bool)ds.Tables[0].Rows[0]["Reservatorios"];
            acessaIndicadores = (bool)ds.Tables[0].Rows[0]["Indicadores"];
        }

        hfPermissoes.Set("GestaoEmpreendimento", acessaGestaoEmpreendimento ? "S" : "N");
        hfPermissoes.Set("EAP", acessaEAP ? "S" : "N");
        hfPermissoes.Set("VisaoGlobal", acessaVisaoGlobal ? "S" : "N");
        hfPermissoes.Set("PainelDesempenho", acessaPainelDesempenho ? "S" : "N");
        hfPermissoes.Set("VisaoCusto", acessaVisaoCusto ? "S" : "N");

        hfPermissoes.Set("Pimental", acessaPimental ? "S" : "N");
        hfPermissoes.Set("BeloMonte", acessaBeloMonte ? "S" : "N");
        hfPermissoes.Set("Infra", acessaInfra ? "S" : "N");
        hfPermissoes.Set("Derivacao", acessaDerivacao ? "S" : "N");
        hfPermissoes.Set("Reservatorios", acessaReservatorios ? "S" : "N");
        hfPermissoes.Set("Indicadores", acessaIndicadores ? "S" : "N");

        string idsTelas = "";

        if (acessaGestaoEmpreendimento)
        {
            urlTelaInicial += "visaoPresidencia;";
            idsTelas += "0;";
        }
        if (acessaEAP)
        {
            urlTelaInicial += "painelGerencial;";
            idsTelas += "1;";
        }
        if (acessaVisaoGlobal)
        {
            urlTelaInicial += "visaoCorporativa_00;";
            idsTelas += "2;";
        }
        if (acessaPainelDesempenho)
        {
            urlTelaInicial += "visaoCorporativa_01;";
            idsTelas += "3;";
        }
        if (acessaVisaoCusto)
        {
            urlTelaInicial += "visaoCorporativa_08;";
            idsTelas += "4;";
        }

        if (acessaPimental)
        {
            urlTelaInicial += "visaoCorporativa_02;";
            idsTelas += "5;";
        }
        if (acessaBeloMonte)
        {
            urlTelaInicial += "visaoCorporativa_03;";
            idsTelas += "6;";
        }
        if (acessaInfra)
        {
            urlTelaInicial += "visaoCorporativa_04;";
            idsTelas += "7;";
        }
        if (acessaDerivacao)
        {
            urlTelaInicial += "visaoCorporativa_05;";
            idsTelas += "8;";
        }
        if (acessaReservatorios)
        {
            urlTelaInicial += "visaoCorporativa_06;";
            idsTelas += "9;";
        }

        hfPermissoes.Set("Urls", idsTelas);

        pMenu.Items.FindByName("0").ClientVisible = acessaGestaoEmpreendimento;
        pMenu.Items.FindByName("1").ClientVisible = acessaEAP;
        pMenu.Items.FindByName("4").ClientVisible = acessaVisaoCusto;

        pMenu.Items.FindByName("5").ClientVisible = acessaPimental;
        pMenu.Items.FindByName("6").ClientVisible = acessaBeloMonte;
        pMenu.Items.FindByName("7").ClientVisible = acessaInfra; 
        pMenu.Items.FindByName("8").ClientVisible = acessaDerivacao;
        pMenu.Items.FindByName("9").ClientVisible = acessaReservatorios;

        imgIndicador.ClientVisible = acessaIndicadores;
    }

    private void defineLarguraTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); 
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x'))); 

        divAjuda.Style.Add("max-height", (altura - 185).ToString() + "px"); 

        alturaTela = (altura - 210).ToString() + "px";
        alturaImagem = altura - 125;
        larguraImagem = largura - 125;
    }

    private void carregaComboAreas()
    {

        string where = "";

        DataSet ds = cDados.getAreasPainelGerenciamento(codigoUsuarioResponsavel, codigoEntidade, where);

        if (cDados.DataSetOk(ds))
        {
            ddlArea.DataSource = ds;
            ddlArea.TextField = "DescricaoCategoria";
            ddlArea.ValueField = "CodigoCategoria";
            ddlArea.DataBind();

            ddlArea2.DataSource = ds;
            ddlArea2.TextField = "DescricaoCategoria";
            ddlArea2.ValueField = "CodigoCategoria";
            ddlArea2.DataBind();
        }

        ListEditItem lei = new ListEditItem(Resources.traducao.todos, "-1");

        ddlArea.Items.Insert(0, lei);

        if (ddlArea.Items.Count > 0)
            ddlArea.SelectedIndex = 0;



        ListEditItem lei2 = new ListEditItem(Resources.traducao.todos, "-1");

        ddlArea2.Items.Insert(0, lei2);


        if (ddlArea2.Items.Count > 0)
            ddlArea2.SelectedIndex = 0;
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

    protected void callbackZoom_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "Escavacao")
        {
            imgZoom.Width = larguraImagem;
            imgZoom.Height = alturaImagem;

            DataSet dsDados = cDados.getFotoEscavacaoPainelGerenciamento(codigoImagemEscavacao, "");

            if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
            {
                foreach (DataRow dr in dsDados.Tables[0].Rows)
                {
                    MemoryStream ms = new MemoryStream((byte[])dr["Foto"]);

                    Size sz = new Size(1024, 768);

                    System.Drawing.Image image = resizeImage(System.Drawing.Image.FromStream(ms), sz);

                    MemoryStream ms2 = new MemoryStream();

                    image.Save(ms2, ImageFormat.Jpeg);

                    imgZoom.ContentBytes = ms2.ToArray();
                    txtDescricaoFoto.Text = dr["DescricaoFoto"].ToString();
                    imgZoom.Cursor = "Pointer";
                    pcZoom.HeaderText = "Evolução da Escavação";
                }
            }
        }
        else
            if (e.Parameter.ToString() != "")
            {
                int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

                DataSet dsDados = cDados.getFotosPainelGerenciamento("St_Pimental", codigoEntidade, 9, "AND CodigoFoto = " + e.Parameter);

                if (cDados.DataSetOk(dsDados) && cDados.DataTableOk(dsDados.Tables[0]))
                {
                    foreach (DataRow dr in dsDados.Tables[0].Rows)
                    {
                        MemoryStream ms = new MemoryStream((byte[])dr["Foto"]);

                        Size sz = new Size(700, 500);

                        System.Drawing.Image image = resizeImage(System.Drawing.Image.FromStream(ms), sz);

                        MemoryStream ms2 = new MemoryStream();

                        image.Save(ms2, ImageFormat.Jpeg);

                        imgZoom.ContentBytes = ms2.ToArray();
                        txtDescricaoFoto.Text = dr["DescricaoFoto"].ToString();
                        imgZoom.Cursor = "Pointer";
                        pcZoom.HeaderText = dr["NomeArquivo"].ToString();
                    }
                }
            }
        defineLarguraTela();
    }

    protected void btnDownLoad_Click(object sender, EventArgs e)
    {
        cDados.download(codigoAnexo, null, Page, Response, Request, true);
    }

    protected void callbackMetrica_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter != "")
        {
            string indicador = "";
            object atualizacao = null;
            string metrica = "";
            string labelObejto = "Indicador:";
            string codigoGrafico = e.Parameter.Split(';')[0];
            string iniciaisSitio = e.Parameter.Split(';')[1];

            string comandoSQL = string.Format(@"SELECT * FROM {0}.{1}.f_uhe_GetMetricaUltimaAtualizacaoGrafico('{2}', '{3}')"
                , cDados.getDbName()
                , cDados.getDbOwner()
                , codigoGrafico
                , iniciaisSitio);

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                indicador = ds.Tables[0].Rows[0]["NomeObjetoAssociado"].ToString();
                atualizacao = ds.Tables[0].Rows[0]["DataUltimaAtualizacao"];
                metrica = ds.Tables[0].Rows[0]["Metrica"].ToString();
               
                if (ds.Tables[0].Rows[0]["TipoObjeto"].ToString() == "PR")
                    labelObejto = "Projeto:";
            }

            callbackMetrica.JSProperties["cp_Indicador"] = indicador;
            callbackMetrica.JSProperties["cp_Atualizacao"] = string.Format("{0:dd/MM/yyyy HH:mm}", atualizacao);
            callbackMetrica.JSProperties["cp_Metrica"] = metrica;
            callbackMetrica.JSProperties["cp_LabelObjeto"] = labelObejto;
        }
    }
}
