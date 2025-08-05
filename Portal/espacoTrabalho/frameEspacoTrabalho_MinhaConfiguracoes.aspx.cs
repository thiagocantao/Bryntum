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
using System.Collections.Specialized;

public partial class espacoTrabalho_frameEspacoTrabalho_MinhaConfiguracoes : System.Web.UI.Page
{
    dados cDados;
    private string nomeTabelaDb = "ParametroConfiguracaoSistema";
    private string whereUpdateDelete;

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private string resolucaoCliente = "";

    private int alturaPrincipal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ConfiguracaoSistema.js"" ></script>"));

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        //Alterado por Ericsson em 17/04/2010 para trazer a entidade do usuário logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);

        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        populaGrid();
        defineAlturaTela(resolucaoCliente);

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void populaGrid()
    {
        //Alterado por Ericsson em 17/04/2010. Não estava passando o código do entidade para filtrar os riscos padrões.
        string where = "";
        DataSet ds = cDados.getConfiguracoesEntidade(CodigoEntidade, where);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 80;
    }

    private ListDictionary getDadosFormulario()
    {
        string tipoDado = "";

        if (gvDados.FocusedRowIndex >= 0)
            tipoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "TipoDadoParametro").ToString();
        else
            tipoDado = "";

        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();

        switch (tipoDado)
        {
            case "TXT": oDadosFormulario.Add("Valor", txtValorTXT.Text);
                break;
            case "INT": oDadosFormulario.Add("Valor", txtValorINT.Text);
                break;
            case "MES": oDadosFormulario.Add("Valor", ddlValorMES.Value.ToString());
                break;
            case "BOL": oDadosFormulario.Add("Valor", rbValorBOL.Value.ToString());
                break;
            case "LOG": oDadosFormulario.Add("Valor", rbValorBOL.Value.ToString());
                break;
            case "COR": oDadosFormulario.Add("Valor", ddlCOR.Text);
                break;
        }
        return oDadosFormulario;
    }

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        try
        {
            // Lê as informações disponíveis no formulário
            ListDictionary oDadosFormulario = getDadosFormulario();

            cDados.update(nomeTabelaDb, oDadosFormulario, whereUpdateDelete);
            populaGrid();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    #endregion
}
