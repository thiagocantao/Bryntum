using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;

public partial class administracao_auditoria_Lista_New : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string resolucaoCliente = "";
    public int qtdBotaoWhere = 0;


    protected void Page_Init(object sender, EventArgs e)
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

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
    }

    private void HeaderOnTela()
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache); // Ok

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/auditoria_Lista_New.js""></script>"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            carregaFiltroTabela();
        }
        carregaFiltroColunas();



        if (gvDados.IsCallback)
            carregaGvAuditoria();

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
        }

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
        this.Title = cDados.getNomeSistema();
        MemoWhere.JSProperties["cp_qtdWhere"] = qtdBotaoWhere.ToString();

        if (!hfGeral.Contains("cp_qtdWhere"))
        {
            hfGeral.Set("cp_qtdWhere", qtdBotaoWhere);
        }
        
    }

    private void carregaFiltroTabela()
    {
        string comandoSQL = "select distinct TABELA from TB_AUDIT";
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            ddlTabela.DataSource = ds.Tables[0];
            ddlTabela.TextField = "TABELA";
            ddlTabela.ValueField = "TABELA";
            ddlTabela.DataBind();

            if (!IsPostBack && cDados.DataTableOk(ds.Tables[0]))
                ddlTabela.SelectedIndex = 0;
        }

    }

    private void carregaFiltroColunas()
    {
        string comandoSQL = string.Format(@" select * from f_GetListaColunasTabela('{0}')", ddlTabela.Text);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds))
        {
            ddlCampos.DataSource = ds.Tables[0];
            ddlCampos.TextField = "NomeColuna";
            ddlCampos.ValueField = "NomeColuna";
            ddlCampos.DataBind();

            if (!IsPostBack && cDados.DataTableOk(ds.Tables[0]))
                ddlTabela.SelectedIndex = 0;
        }

    }
    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        gvDados.Settings.VerticalScrollableHeight = altura - 300;
    }

    private void carregaGvAuditoria()
    {
        string where1 = (ddlTabela.SelectedIndex == -1) ? "" : string.Format(@" and ta.TABELA = '{0}'", ddlTabela.SelectedItem.Text);

        if (txtFiltroAlteracao.Text.Trim() != "")
        {
            if (ddlOperacao.Value.ToString() == "U")
            {
                where1 += MemoWhere.Text;
            }
            else if (ddlOperacao.Value.ToString() == "I")
            {
                where1 += string.Format(@" 
             AND NEW_DATA.exist('/INSERIDO[contains(upper-case(.), upper-case(sql:variable(""@Role"")))]') = 1
            ", txtFiltroAlteracao.Text);
            }
            else if (ddlOperacao.Value.ToString() == "D")
            {
                where1 += string.Format(@" 
             AND OLD_DATA.exist('/EXCLUIDO[contains(upper-case(.), upper-case(sql:variable(""@Role"")))]') = 1
            ", txtFiltroAlteracao.Text);
            }
        }
        where1 += string.Format(@" and ta.Operacao = '{0}' ", ddlOperacao.Value);

        DataSet ds = cDados.getListaAuditoria(where1, txtFiltroAlteracao.Text.ToUpper());

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    public string getBotaoVisualizar()
    {
        int codigo = int.Parse(Eval("ID").ToString());
        string retorno = "";

        retorno = string.Format(@"<img alt='Visualizar' style='cursor:pointer' src='../imagens/botoes/pFormulario.png' onclick=""window.top.showModal('{0}?ID={1}', 'Auditoria', 890, 360, '', null);"" />"
            , Eval("OPERACAO").ToString() == "U" ? "auditoria_Atualizacao.aspx" : "auditoria_Insercao.aspx"
            , codigo);

        return retorno;
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    protected void ASPxButton3_Click(object sender, EventArgs e)
    {
        this.carregaGvAuditoria();
    }

}
 