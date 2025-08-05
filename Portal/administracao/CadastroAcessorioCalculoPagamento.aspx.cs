using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Data;

public partial class administracao_CadastroAcessorioCalculoPagamento : System.Web.UI.Page
{
    private dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private int alturaPrincipal = 0;
    private string resolucaoCliente = "";

    protected bool podeEditar = true;
    protected bool podeIncluir = true;
    protected bool podeExcluir = true;
    protected bool podePermissao = true;
    protected bool podeConsultar = true;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        this.Title = cDados.getNomeSistema();

        sdsAcessorio.ConnectionString = cDados.classeDados.getStringConexao();
        ConfiguraParametros();
    }

    private void ConfiguraParametros()
    {
        DataSet dsParametros = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "labelAcessoriosContrato");
        string labelAcessoriosContrato = dsParametros.Tables[0].Rows[0]["labelAcessoriosContrato"] as string;
        gvDados.Columns["DescricaoAcessorio"].Caption = labelAcessoriosContrato;
        lblTituloTela.Text = labelAcessoriosContrato;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        if (!IsPostBack)
        {
            
            cDados.aplicaEstiloVisual(Page);

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        int largura = 0;
        int altura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = altura;

        gvDados.Settings.VerticalScrollableHeight = altura - 210;

    }

    protected string ObtemBtnIncluir()
    {
        string imagemHabilitada = @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""gvDados.AddNewRow ();"" style=""cursor: pointer;""/>";
        string imagemDesabilitada = @"<img src=""../imagens/botoes/incluirRegDes.png"" title="""" style=""cursor: default;""/>";
        string conteudoHtml = string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>",
            (podeIncluir) ? imagemHabilitada : imagemDesabilitada);
        return conteudoHtml;
    }
    protected void gvDados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        OrderedDictionary values = e.NewValues;
        if (!VerificaValorAcessorioValido(values))
            throw new Exception("Deve ser informado um valor válido para o campo 'Aliquota' ou para o campo 'Valor'.");
    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OrderedDictionary values = e.NewValues;
        if (!VerificaValorAcessorioValido(values))
            throw new Exception("Deve ser informado um valor válido para o campo 'Aliquota' ou para o campo 'Valor'.");
    }

    private static bool VerificaValorAcessorioValido(OrderedDictionary values)
    {
        bool aliquotaValida;
        bool valorValido;
        decimal aliquota;
        decimal valor;
        //Verifica se o campo 'Aliquota' foi informado e se ter um valor válido
        if (values.Contains("Aliquota") && values["Aliquota"] != null)
            aliquotaValida = decimal.TryParse(values["Aliquota"].ToString(), out aliquota);
        else
            aliquotaValida = false;
        //Verifica se o campo 'Valor' foi informado e se ter um valor válido
        if (values.Contains("Valor") && values["Valor"] != null)
            valorValido = decimal.TryParse(values["Valor"].ToString(), out valor);
        else
            valorValido = false;

        return aliquotaValida || valorValido;
    }
}