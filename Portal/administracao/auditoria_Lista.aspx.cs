using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;
using DevExpress.Web;

public partial class administracao_auditoria : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string resolucaoCliente = "";

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

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            carregaFiltroTabela();
        }

        carregaGvAuditoria();

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);            
        
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/auditoria_Lista.js""></script>"));

        cDados.aplicaEstiloVisual(Page);
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

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        gvDados.Settings.VerticalScrollableHeight = altura - 370;
    }

    private void carregaGvAuditoria()
    {
        string where1 = (ddlTabela.SelectedIndex == -1) ? "" : string.Format(@" and ta.TABELA = '{0}'", ddlTabela.SelectedItem.Text);

        if (txtFiltroAlteracao.Text.Trim() != "")
        {
            if (ddlOperacao.Value.ToString() == "U")
            {
                where1 += string.Format(@" 
             AND (OLD_DATA.exist('/DADO_ANTIGO[contains(upper-case(.), upper-case(sql:variable(""@Role"")))]') = 1
	           OR NEW_DATA.exist('/DADO_ANTIGO[contains(upper-case(.), upper-case(sql:variable(""@Role"")))]') = 1)
            ", txtFiltroAlteracao.Text.Replace("'", "'+char(39)+'"));
            }
            else if (ddlOperacao.Value.ToString() == "I")
            {
                where1 += string.Format(@" 
             AND NEW_DATA.exist('/INSERIDO[contains(upper-case(.), upper-case(sql:variable(""@Role"")))]') = 1
            ", txtFiltroAlteracao.Text.Replace("'", "'+char(39)+'"));
            }
            else if (ddlOperacao.Value.ToString() == "D")
            {
                where1 += string.Format(@" 
             AND OLD_DATA.exist('/EXCLUIDO[contains(upper-case(.), upper-case(sql:variable(""@Role"")))]') = 1
            ", txtFiltroAlteracao.Text.Replace("'", "'+char(39)+'"));
            }
        }
        where1 += string.Format(@" and ta.Operacao = '{0}' ", ddlOperacao.Value); 

        DataSet ds = cDados.getListaAuditoria(where1, txtFiltroAlteracao.Text.Replace("'", "'+char(39)+'").ToUpper());

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

        retorno = string.Format(@"<img alt='Visualizar Detalhes' style='cursor:pointer' src='../imagens/botoes/pFormulario.png' onclick=""mostraPopupAuditoria({0}, '{1}');"" />"
            , codigo 
            , Eval("OPERACAO").ToString());

        return retorno;
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LstAud");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "LstAud", "Auditoria", this);
    }

    #endregion
}