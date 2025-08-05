/*
 ALTERAÇÕES:
 * 
 * 30/11/2010 : BY Alejandro
    Alterar opção onde registra-se a análise de indicadores de projetos para contemplar 
    o novo modelo que irá gravar as análises em uma tabela denominada AnalisePerformance
    ao invés de AnalisePerformanceProjeto.
 
 * 23/03/2011 :: Alejandro : Alteração de acesso a tela, e dos codigos de permissão.
 * * */
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

public partial class _Estrategias_objetivoEstrategico_ObjetivoEstrategico_Analises : System.Web.UI.Page
{
    //variaveis de acceso ao banco de dados.
    dados cDados;

    //variaveis de logeo.
    private int codigoUnidadeLogada = -1;
    private int idUsuarioLogado = 0;

    //variaveis gerais.
    private int codigoIndicador = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoUnidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        codigoIndicador = int.Parse(Request.QueryString["CI"].ToString());
        grid.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["ALT"].ToString());
        headerOnTela();
        cDados.aplicaEstiloVisual(this);
        carregaComboStatus();
        carregaGrid();
    }

    #region VARIOS

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/AnalisesIndicador.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        this.TH(this.TS("barraNavegacao", "AnalisesIndicador", "_Strings"));
    }

    #endregion

    #region GRIDVIEW

    private void carregaGrid()
    {
        DataTable dt = cDados.getAnalisesIndicador(codigoIndicador.ToString(), "").Tables[0];

        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void grid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.CellValue.ToString().Length > 120)
        {
            e.Cell.Text = e.CellValue.ToString().Substring(0, 120) + "  ...";
        }
    }
    #endregion

    private void carregaComboStatus()
    {
        DataSet ds = cDados.getCoresApresentacao();

        ddlStatus.DataSource = ds;
        ddlStatus.TextField = "LegendaCorEstrategia";
        ddlStatus.ValueField = "CodigoCorApresentacao";
        ddlStatus.ImageUrlField = "urlCor";
        ddlStatus.DataBind();
    }

    #region CALLBACK's

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";
        hfGeral.Set("ErroSalvar", "");

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;            
        }
        else
        {// alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                grid.ClientVisible = false;
        }
    }

    #endregion

    #region BANCO DE DADOS

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (grid.FocusedRowIndex >= 0)
            return grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string tendencias = txtAnalise.Text;
        string agenda = txtRecomendacoes.Text;
        int codigoStatusCor = ddlStatus.Value == null ? -1 : int.Parse(ddlStatus.Value.ToString());

        bool result = cDados.incluiAnalisePerformanceIndicador(codigoIndicador, idUsuarioLogado, tendencias, agenda, codigoStatusCor);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGrid();
            return "";
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoAnalise = int.Parse(getChavePrimaria());

        string tendencias = txtAnalise.Text;
        string agenda = txtRecomendacoes.Text;
        int codigoStatusCor = ddlStatus.Value == null ? -1 : int.Parse(ddlStatus.Value.ToString());

        bool result = cDados.atualizaAnalisePerformanceIndicador(codigoAnalise, idUsuarioLogado, tendencias, agenda, codigoStatusCor);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGrid();
            return "";
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoAnalise = int.Parse(getChavePrimaria());

        bool result = cDados.excluiAnalisePerformanceIndicador(codigoAnalise, idUsuarioLogado);

        if (result == false)
            return "Erro ao salvar o registro!";
        else
        {
            carregaGrid();
            return "";
        }
    }

    #endregion
}
