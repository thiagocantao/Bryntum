using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Drawing;
using System.IO;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using System.Diagnostics;

public partial class _Projetos_Relatorios_relAnalisePropriedades : System.Web.UI.Page
{
    dados cDados;
    private string dbName, dbOwner;
    private bool exportaOLAPTodosFormatos = false;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoUsuario;
    public string larguraTela = "", alturaTela = "";
    public string larguraGrafico = "", alturaGrafico = "";
    public string alturaTabela = "";
    public string larguraTabela = "";

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
        // =========================== Verifica se a sessão existe FIM ========================

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        if (!IsPostBack)
        {
            cDados.aplicaEstiloVisual(Page);

            if (!hfGeral.Contains("tipoArquivo"))
            {
                hfGeral.Set("tipoArquivo", "XLS");
            }
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            populaOpcoesExportacao();
        }

        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuario, codigoEntidadeUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_AcsRelProp");
        }
        CDIS_PivotGridLocalizer.Activate();  // ativa tradução dos textos da grid Pivot

        buscaDadosGridPropriedades();
        buscaDadosGridOcupantes();
        defineLarguraTela();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            //Master.geraRastroSite();
        }
    }
    private void populaOpcoesExportacao()
    {
        ddlExporta.Items.Clear();

        ListEditItem liExcel = new ListEditItem("XLS", "XLS");
        liExcel.ImageUrl = "~/imagens/menuExportacao/iconoExcel.png";


        ddlExporta.Items.Add(liExcel);
        ddlExporta.ClientEnabled = false;
        if (exportaOLAPTodosFormatos)
        {
            ddlExporta.ClientEnabled = true;
            ddlExporta.Items.Add(new ListEditItem("XLS", "XLS"));

            ListEditItem liPDF = new ListEditItem("PDF", "PDF");
            liPDF.ImageUrl = "~/imagens/menuExportacao/iconoPDF.png";
            ddlExporta.Items.Add(liPDF);


            ListEditItem liHTML = new ListEditItem("HTML", "HTML");
            liHTML.ImageUrl = "~/imagens/menuExportacao/iconoHtml.png";
            ddlExporta.Items.Add(liHTML);

            ListEditItem liRTF = new ListEditItem("RTF", "RTF");
            liRTF.ImageUrl = "~/imagens/menuExportacao/iconoPortfolio.png";
            ddlExporta.Items.Add(liRTF);

            ListEditItem liCSV = new ListEditItem("CSV", "CSV");
            liCSV.ImageUrl = "~/imagens/menuExportacao/iconoCSV.png";
            ddlExporta.Items.Add(liCSV);

        }
        ddlExporta.SelectedIndex = 0;
    }

    private void defineLarguraTela()
    {
        /*
         alturaTabela maior que
pgControlPropriedades maior que

gvPropriedades mesmo tamanho
gvOcupantes mesmo tamanho
         */

        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        int largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        larguraTela = (largura - 33).ToString() + "px";

        int altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1)); ;
        /*height:<%=alturaTabela %>;width:<%=larguraTabela %>*/
        alturaTela = (altura - 100).ToString() + "px";
        
        alturaTabela = (altura - 200) + "px";//a div vai ficar com essa altura
        larguraTabela = (largura) + "px";

        //calculo baseado na hierarquia de tamanho da variavel: alturaTabela
        pgControlPropriedades.Height = new Unit(((altura - 200) - 100) + "px");
        gvPropriedades.Settings.VerticalScrollableHeight = ((altura - 235) - 100) - 20;
        gvOcupantes.Settings.VerticalScrollableHeight = ((altura - 235) - 100) - 20;

        Div1.Style.Add("height", ((altura - 200) + 0).ToString() + "px");
        Div1.Style.Add("width", (largura) + "px");
        Div1.Style.Add("overflow", "auto");

    }

    private void buscaDadosGridPropriedades()
    {


        string comandoSQL = string.Format(@" 
 /*
  Objetivos..: Consulta para apresentar uma listagem dos imóveis cadastrados para gestão de liberação 
               de áreas (UHETP).
 */ 
  BEGIN
   DECLARE @CodigoEntidade Int
   
   SET @CodigoEntidade = {2}
   
		         SELECT p.NomeProjeto,
						prop.SequencialImovel,
						prop.Localizacao,
						m.NomeMunicipio,
						prop.Distrito,
						prop.Comarca,
						prop.NomeRegiao,
						prop.IdentificacaoFundiaria,
						prop.Coordenadas,
						prop.AreaTotal,
						prop.AreaAtingida,
						prop.IndicaEspolio,
						prop.NomeInventariante,
						prop.JuizoEspolio,
						prop.CartorioEspolio,
						prop.NomeAdvogadoEspolio,
						prop.EnderecoAdvogadoEspolio,
						prop.TelefoneAdvogadoEspolio,
						prop.DadosUltimaDeclaracaoITR,
						Sum(IsNull(aa.Area,0)) AS AreaAverbada,
						Sum(IsNull(dri.Area,0)) AS AreaRegistrada,
						SUM(CASE WHEN pesImov.IndicaOcupante = 'S' THEN 1 ELSE 0 END) AS QuantidadeOcupantes,
						SUM(CASE WHEN pesImov.IndicaProprietario = 'S' THEN 1 ELSE 0 END) AS QuantidadeProprietarios
			       FROM {0}.{1}.Projeto AS p INNER JOIN
						{0}.{1}.Prop_Imovel AS prop ON (prop.CodigoProjeto = p.CodigoProjeto) INNER JOIN
						{0}.{1}.Municipio AS m ON (m.CodigoMunicipio = prop.CodigoMunicipio) LEFT JOIN
						{0}.{1}.Prop_AreaAverbada AS aa ON (aa.CodigoImovel = prop.CodigoImovel) LEFT JOIN
						{0}.{1}.Prop_DocumentoRegistradoImovel AS dri ON (dri.CodigoImovel = prop.CodigoImovel) LEFT JOIN
						{0}.{1}.Prop_PessoaImovel AS pesImov ON (pesImov.CodigoProjeto = p.CodigoProjeto)
			WHERE p.DataExclusao IS NULL
				AND p.CodigoEntidade = @CodigoEntidade
		 GROUP BY p.NomeProjeto,
							prop.SequencialImovel,
							prop.Localizacao,
							m.NomeMunicipio,
							prop.Distrito,
							prop.Comarca,
							prop.NomeRegiao,
							prop.IdentificacaoFundiaria,
							prop.Coordenadas,
							prop.AreaTotal,
							prop.AreaAtingida,
							prop.IndicaEspolio,
							prop.NomeInventariante,
							prop.JuizoEspolio,
							prop.CartorioEspolio,
							prop.NomeAdvogadoEspolio,
							prop.EnderecoAdvogadoEspolio,
							prop.TelefoneAdvogadoEspolio,
							prop.DadosUltimaDeclaracaoITR	      
				
END", dbName, dbOwner, codigoEntidadeUsuarioResponsavel);

        gvPropriedades.DataSource = cDados.getDataSet(comandoSQL);

        gvPropriedades.DataBind();
    }
    private void buscaDadosGridOcupantes()
    {
        string comandoSQL = string.Format(@" 
        BEGIN
   DECLARE @CodigoEntidade Int
   
   SET @CodigoEntidade = {2}
   
		 SELECT p.NomeProjeto,
						prop.SequencialImovel,
						prop.Localizacao,
						m.NomeMunicipio,
					  prop.IdentificacaoFundiaria,
						prop.Coordenadas,
						prop.AreaTotal,
                        prop.Distrito,
                        prop.Comarca,
                        prop.NomeRegiao,
						prop.AreaAtingida,
						prop.IndicaEspolio,
						prop.NomeInventariante,
						prop.JuizoEspolio,
						prop.CartorioEspolio,
						prop.NomeAdvogadoEspolio,
						prop.EnderecoAdvogadoEspolio,
						prop.TelefoneAdvogadoEspolio,
						prop.DadosUltimaDeclaracaoITR,
						pesImov.NomePessoa, 
						pesImov.DataNascimentoPessoa, 						
						CASE WHEN pesImov.IndicaNacionalidadePessoa = 'B' THEN 'Brasileira' ELSE 'Estrangeira' END AS NacionalidadePessoa,
						pesImov.NomePaisPessoa,    
						munNatPessoa.NomeMunicipio, 
						pesImov.ProfissaoPessoa,    
						pesImov.NumeroCPFCNPJPessoa,
						CASE WHEN pesImov.IndicaPessoaSabeAssinar = 'S' THEN 'Sim' ELSE 'Não' End AS PessoaSabeAssinar,
						pesImov.TipoDocumentoPessoa,   
						pesImov.NumeroDocumentoPessoa,    
						pesImov.OrgaoExpedidorDocumentoPessoa,
						pesImov.NomePaiPessoa,    
						pesImov.NomeMaePessoa,    						
						CASE pesImov.IndicaEstadoCivilPessoa WHEN 'C' THEN 'Casado'
																								 WHEN 'S' THEN 'Solteiro'
																							   WHEN 'D' THEN 'Divorciado'
																							   WHEN 'SJ' THEN 'Separado Judicialmente'
																								 WHEN 'UE' THEN 'União Estável'
																								 WHEN 'VI' THEN 'Viúvo'
																								 ELSE 'Não Informado' END AS EstadoCivilPessoa,
						
                        CASE pesImov.RegimeSeparacaoBensPessoa WHEN 'CP' THEN 'Comunhão Parcial de Bens'
																								 WHEN 'CU' THEN 'Comunhão Universal de Bens'
																							   WHEN 'SEB' THEN 'Separação de Bens'
																							   WHEN 'PFA' THEN 'Participação Final nos Aquestros'
																							ELSE 'Não Informado' END AS RegimeSeparacaoBensPessoa,  
						pesImov.CertidaoEstadoCivilPessoa,  
						pesImov.LivroCertidaoEstadoCivilPessoa ,
						pesImov.FolhaCertidaoEstadoCivilPessoa , 
						pesImov.EmissaoCertidaoEstadoCivilPessoa,
						pesImov.NomeCartorioCertidaoEstadoCivilPessoa ,    
						pesImov.AutosSeparacaoPessoa ,    
						pesImov.DataSeparacaoPessoa ,    
						pesImov.JuizoSeparacaoPessoa ,   
						pesImov.DataUniaoEstavel ,    
						pesImov.CertidaoViuvoPessoa,  
						pesImov.FolhaCertidaoViuvoPessoa ,    
						pesImov.LivroCertidaoViuvoPessoa ,    
						CASE WHEN pesImov.IndicaEscrituraRegistroPactoAnteNupcialPessoa = 'E' then 'Escritura' else 'Registro' end as IndicaEscrituraRegistroPactoAnteNupcialPessoa,
						pesImov.NumeroPactoAnteNupcialPessoa,    
						pesImov.FolhaRegistroPactoAnteNupcialPessoa,    
						pesImov.LivroRegistroPactoAnteNupcialPessoa,    
						pesImov.NomeCartorioRegistroPactoAnteNupcialPessoa,    
						pesImov.EnderecoResidencialPessoa,    
						pesImov.NumeroEnderecoResidencialPessoa,    
						munResPessoa.NomeMunicipio as NomeMunicipioResPessoa, 
						pesImov.TelefonePessoa,    
						pesImov.TempoOcupacaoPessoa,    
						pesImov.NomeConjuge,    
						pesImov.DataNascimentoConjuge,    
						CASE WHEN pesImov.IndicaNacionalidadeConjuge = 'B' THEN 'Brasileira' ELSE 'Estrangeira' END AS NacionalidadeConjuge,
						pesImov.NomePaisConjuge ,    
						munNatConjuge.NomeMunicipio, 
						pesImov.ProfissaoConjuge,    
						pesImov.NumeroCPFCNPJConjuge ,    
						CASE WHEN pesImov.IndicaConjugeSabeAssinar = 'S' THEN 'Sim' ELSE 'Não' END AS ConjugeSabeAssinar,    
						pesImov.TipoDocumentoConjuge,    
						pesImov.NumeroDocumentoConjuge,    
						pesImov.OrgaoExpedidorDocumentoConjuge,    
						pesImov.NomePaiConjuge,    
						pesImov.NomeMaeConjuge,    
						pesImov.IndicaEstadoCivilConjuge,    
						pesImov.CertidaoEstadoCivilConjuge,    
						pesImov.LivroCertidaoEstadoCivilConjuge,
						pesImov.FolhaCertidaoEstadoCivilConjuge,    
						pesImov.EmissaoCertidaoEstadoCivilConjuge,   
						pesImov.NomeCartorioCertidaoEstadoCivilConjuge,    
						CASE WHEN pesImov.IndicaProprietario = 'S' THEN 'Sim' ELSE 'Não' END AS Proprietario,    
						CASE WHEN pesImov.IndicaOcupante = 'S' THEN 'Sim' ELSE 'Não' END AS Ocupante 						
			 FROM {0}.{1}.Projeto AS p INNER JOIN
				  {0}.{1}.Prop_Imovel AS prop ON (prop.CodigoProjeto = p.CodigoProjeto) INNER JOIN
				  {0}.{1}.Municipio AS m ON (m.CodigoMunicipio = prop.CodigoMunicipio) INNER JOIN						
				  {0}.{1}.Prop_PessoaImovel AS pesImov ON (pesImov.CodigoProjeto = p.CodigoProjeto) LEFT JOIN
				  {0}.{1}.Municipio AS munNatPessoa ON (munNatPessoa.CodigoMunicipio = pesImov.CodigoMunicipioNaturalidadePessoa) LEFT JOIN
				  {0}.{1}.Municipio AS munNatConjuge ON (munNatConjuge.CodigoMunicipio = pesImov.CodigoMunicipioNaturalidadePessoa) LEFT JOIN
				  {0}.{1}.Municipio AS munResPessoa ON (munResPessoa.CodigoMunicipio = pesImov.CodigoMunicipioResidenciaPessoa)
			WHERE p.DataExclusao IS NULL
				AND p.CodigoEntidade = @CodigoEntidade
              END", dbName, dbOwner, codigoEntidadeUsuarioResponsavel);

        gvOcupantes.DataSource = cDados.getDataSet(comandoSQL);
        gvOcupantes.DataBind();

    }

    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string nomeArquivo = "";


        if (e.Parameter == "HTML")
            nomeArquivo = "~/imagens/menuExportacao/iconoHtml.png";

        if (e.Parameter == "PDF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPDF.png";

        if (e.Parameter == "XLS")
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";

        if (e.Parameter == "RTF")
            nomeArquivo = "~/imagens/menuExportacao/iconoPortfolio.png";

        if (e.Parameter == "CSV")
            nomeArquivo = "~/imagens/menuExportacao/iconoCSV.png";

        imgExportacao.ImageUrl = nomeArquivo;
    }
    public void StartProcess(string path)
    {
        Process process = new Process();
        try
        {
            process.StartInfo.FileName = path;
            process.Start();
            process.WaitForInputIdle();
        }
        catch { }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
     
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + codigoUsuario;

            string nomeArquivo = "", app = "", erro = "";
            if (pgControlPropriedades.ActiveTabIndex == 0)
            {
                GridViewExporter1.GridViewID = "gvPropriedades";
            }
            else
            {
                GridViewExporter1.GridViewID = "gvOcupantes";
            }
            try
            {
                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo = (pgControlPropriedades.ActiveTabIndex == 0) ? "OLAPAnalisePropriedades_" + dataHora + ".xls" : "OLAPAnaliseOcupantes_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();
                    GridViewExporter1.WriteXlsToResponse(nomeArquivo, new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                    //ASPxPivotGridExporter1.ExportToXls(stream);
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }

            }
            catch 
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {
                if (hfGeral.Get("tipoArquivo").ToString() != "HTML")
                {
                    nomeArquivo = "\"" + nomeArquivo + "\"";
                    Response.Clear();
                    Response.Buffer = false;
                    Response.AppendHeader("Content-Type", app);
                    Response.AppendHeader("Content-Transfer-Encoding", "binary");
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
                    Response.BinaryWrite(stream.GetBuffer());
                    Response.End();
                }

            }
            else
            {

                string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. Verifique se não foi ultrapassado o número máximo de 256 colunas!', 'erro', true, false, null);                                   
                                 </script>";

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }
    protected void GridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
