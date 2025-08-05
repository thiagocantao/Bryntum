using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Drawing;
using DevExpress.Web.ASPxTreeList;

public partial class planoContasFluxoCaixa1 : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public string alturaTabela = "";
    public bool passouNoHtmlRowPrepared = false;
    List<string> nodeKeys = new List<string>();
    public string estiloFooter = "dxtlFooter";


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

        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "AcessPlanoDeContas");
        podeIncluir = podeEditar;
        podeExcluir = podeEditar;

        this.Title = cDados.getNomeSistema();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DevExpress.Web.ASPxWebControl.RegisterBaseScript(Page);

        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        string cssPostfix = "", cssPath = "";

        cDados.aplicaEstiloVisual(Page);
        cDados.getVisual(cDados.getInfoSistema("IDEstiloVisual").ToString(), ref cssPath, ref cssPostfix);
        if (cssPostfix != "")
            estiloFooter += "_" + cssPostfix;

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);
        carregaGvDados();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            //Master.geraRastroSite();
        }
    }
    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/planoContasFluxoCaixa1.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
        this.TH(this.TS("planoContasFluxoCaixa1", "_Strings"));
    }

    private void carregaGvDados()
    {
        string where = string.Format(@" and pc.DescricaoConta like '%{0}%'", txtFiltro.Text);
        DataSet ds = cDados.getPlanoContasFluxoCaixa(codigoEntidadeUsuarioResponsavel, where);

        if ((cDados.DataSetOk(ds)))
        {
            tlCentroCusto.DataSource = ds;
            tlCentroCusto.DataBind();
        }
    }
    private void defineAlturaTela(string resolucaoCliente)
    {
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);


        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        alturaPrincipal = (altura - 235);

        tlCentroCusto.Settings.ScrollableHeight = altura - 245;

    }

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeExcluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
    }
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        //e.Editor.Font.Name = "Verdana";
        //e.Editor.Font.Size = new FontUnit("8pt");
    }
    protected void gvDados_AfterPerformCallback1(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_OperacaoOk"] = "";

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
            hfGeral.Set("StatusSalvar", "0");
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);

            if (e.Parameter != "Excluir")
                tlCentroCusto.ClientVisible = false;// gvDados.ClientVisible = false;

        }
    }


    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (tlCentroCusto.FocusedNode != null)
            return tlCentroCusto.FocusedNode.Key;//.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string codContaSuperior = ((ddlContaSuperior.Value != null) && (ddlContaSuperior.Value.ToString() != "")) ? ddlContaSuperior.Value.ToString() : "null";
        string indicaAnalitica = codContaSuperior != "null" ? "S" : "N";
        string tipoConta = (rblTipoConta.SelectedItem.Value.ToString() != "CT") ? rblTipoConta.SelectedItem.Value.ToString() : "";
        string mensagemErro = "";

        string where = " and pc.DescricaoConta ='" + txtDescricaoConta.Text + "'";

        DataSet ds = cDados.getPlanoContasFluxoCaixa(codigoEntidadeUsuarioResponsavel, where);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            mensagemErro = "Já existe plano de contas com esse nome, tente outro nome.";
            return mensagemErro;
        }

        bool result = cDados.incluiPlanoDeContasFluxoCaixa(txtDescricaoConta.Text, codContaSuperior, codigoEntidadeUsuarioResponsavel, txtCodigoReservado.Text, tipoConta, "S", "S", ref mensagemErro);

        if (result == false)
            return "Erro:" + mensagemErro;
        else
        {
            carregaGvDados();
            return mensagemErro;
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoConta = int.Parse(getChavePrimaria());
        string mensagemErro = "";
        string codContaSuperior = (ddlContaSuperior.Value != null) ? ddlContaSuperior.Value.ToString() : "null";

        bool result = cDados.atualizaPlanoDeContasFluxoCaixa(txtDescricaoConta.Text, codContaSuperior, codigoConta, txtCodigoReservado.Text, "S", "RC", "S", ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoPlanoContas = int.Parse(getChavePrimaria());
        string mensagemErro = "";
        if (cDados.verificaExclusaoPlanoDeContasFluxoCaixa(codigoPlanoContas, ref mensagemErro) == false)
        {
            return "O plano de contas não pode ser excluído. Pois existem planos de conta subordinados a este.";
        }
        else
        {
            bool result = cDados.excluiPlanoDeContasFluxoCaixa(codigoPlanoContas, ref mensagemErro);

            if (result == false)
                return mensagemErro;
            else
            {
                carregaGvDados();
                return "";
            }
        }
    }
    protected void ddlContaSuperior_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        ddlContaSuperior.Items.Clear();
        
        TreeListNodeIterator iterator = new TreeListNodeIterator(tlCentroCusto.RootNode);
        int nivel = rblTipoConta.SelectedIndex + 1;

        while (iterator.Current != null)
        {
            if (iterator.Current.Level == nivel - 1)
            {
                if (iterator.Current.GetValue("DescricaoConta") != null)
                {
                    ListEditItem li = new ListEditItem(iterator.Current.GetValue("DescricaoConta").ToString(), iterator.Current.GetValue("codigoConta"));
                    ddlContaSuperior.Items.Insert(0, li);
                }
            }
            iterator.GetNext();
            
        }
        ddlContaSuperior.Items.Insert(0, new ListEditItem(Resources.traducao.nenhum, ""));

        //ddlContaSuperior.Items.IndexOfText
        ListEditItem itemauxiliar = (tlCentroCusto.FocusedNode.ParentNode.GetValue("DescricaoConta") != null) ? ddlContaSuperior.Items.FindByText(tlCentroCusto.FocusedNode.ParentNode.GetValue("DescricaoConta").ToString()) : null;
        ddlContaSuperior.SelectedIndex = (itemauxiliar != null) ? itemauxiliar.Index : -1;
        ddlContaSuperior.JSProperties["cpIndiceSelecionado"] = ddlContaSuperior.SelectedIndex;

    }

    private void GetParentNodeKey(TreeListNode node)
    {
        //se o no atual nao é o no raiz e se ele tiver filhos, entao adiciona na lista de nos que tem filhos
        if (node != tlCentroCusto.RootNode && node.HasChildren)
            nodeKeys.Add(node.Key);
    }
    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string contaSintetica = e.GetValue("IndicaContaAnalitica").ToString();

            if (contaSintetica == "N")
            {
                //e.Row.BackColor = Color.FromName("#DDFFCC");
                //e.Row.ForeColor = Color.Black;
            }
        }
    }
    protected void tlCentroCustos_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
    {
        if (e.RowKind == TreeListRowKind.Data)
        {
            if (e.GetValue("DescricaoConta") != null)
            {
                TreeListNode tln = tlCentroCusto.FindNodeByKeyValue(e.GetValue("CodigoConta").ToString());
                if (tln != null && tln.HasChildren)
                {
                    e.Row.BackColor = Color.FromName("#DDFFCC");
                    e.Row.ForeColor = Color.Black;
                }
            }
        }
    }


    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        cDados = CdadosUtil.GetCdados(null);
        cDados.exportaTreeList(ASPxTreeListExporter1, parameter);
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        cDados = CdadosUtil.GetCdados(null);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        podeIncluir = cDados.VerificaAcessoEmAlgumObjeto(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "UN", "UN_IncPrj");
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "abrePopUp('Conta','Incluir');", true, true, false, "LstProjEn", "Plano de Contas", this);
    }

    #endregion


    protected void tlCentroCustos_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
    {
        carregaGvDados();
    }


    public string getDescricaoObjetosLista()
    {
        string retornoHTML = "";


        string indicaProjeto = Eval("CodigoConta").ToString();
        string DescricaoConta = Eval("DescricaoConta").ToString();
        string Departamento = Eval("Departamento").ToString();
        string Diretoria = Eval("Diretoria").ToString();
        string TipoConta = Eval("TipoConta").ToString();
        string IndicaContaAnalitica = Eval("IndicaContaAnalitica").ToString();
        string CodigoReservadoGrupoConta = Eval("CodigoReservadoGrupoConta").ToString();
        string CodigoContaSuperior = Eval("CodigoContaSuperior").ToString();


        retornoHTML = "<table><tr><td>";


        string strHREF = string.Format("href=javascript:abrePopUp('{0}','{1}');","Conta", "Editar");

        retornoHTML += string.Format("</td><td>&nbsp;<a class='LinkGrid' " + strHREF + "  >{0}</a>", DescricaoConta);

        retornoHTML += "</td></tr></table>";

        return retornoHTML;
    }
}