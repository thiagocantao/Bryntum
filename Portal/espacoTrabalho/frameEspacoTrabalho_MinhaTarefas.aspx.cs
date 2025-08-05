//Revisado
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
using System.Drawing;

public partial class espacoTrabalho_FrameEspacoTrabalho_MinhaTarefas : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int idProjeto;
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

        // datos do usuario logado e da entidad logada.
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();

        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            defineAlturaTela(resolucaoCliente);
            cDados.aplicaEstiloVisual(Page);
            populaCombo();
        }

        carregaGvDados();

        if (!IsPostBack && !IsCallback)
        {
            if (Request.QueryString["Estagio"] != null)
            {
                string descricaoStatusEmExecucao = cDados.getDescricaoStatusTarefaToDoList(1);

                if (Request.QueryString["Estagio"].ToString() != "")
                    gvDados.FilterExpression += "Estagio = '" + Request.QueryString["Estagio"].ToString() + "' AND DescricaoStatusTarefa = '" + descricaoStatusEmExecucao + "'";
                else
                    gvDados.FilterExpression += "DescricaoStatusTarefa = '" + descricaoStatusEmExecucao + "'";
            }
        }

        if (!IsPostBack)
        {
            DataSet dsParametros = cDados.getParametrosSistema("labelToDoList");
            string tituloTela = "Minhas Tarefas";
            if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
                tituloTela += (dsParametros.Tables[0].Rows[0]["labelToDoList"].ToString().ToLower().Trim() == "to do list" ? " de " : " ") + dsParametros.Tables[0].Rows[0]["labelToDoList"].ToString();

            lblTituloTela.Text = tituloTela;

            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "MTAREF", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
    }

    #region COMBOBOX

    private void populaCombo()
    {
        DataSet ds = cDados.getStatusTarefas("");

        if ((cDados.DataSetOk(ds)) && (cDados.DataTableOk(ds.Tables[0])))
        {
            ddlStatusTarefa.TextField = "DescricaoStatusTarefa";
            ddlStatusTarefa.ValueField = "CodigoStatusTarefa";
            ddlStatusTarefa.DataSource = ds.Tables[0];
            ddlStatusTarefa.DataBind();
        }
    }

    #endregion

    #region GRID

    private void carregaGvDados()
    {
        //neste caso, vo enviar como parâmetro do codigo de projeto '-1', pra trazer tudas as
        //tarefas do usuario logado, independiente ao projeto.
        int codigoProjeto = -1;
        DataSet ds = cDados.getMinhasTarefasLogado(codigoProjeto, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    protected void gvToDoList_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        try
        {
            DataSet dsCustom = cDados.getGerenteProjeto(idProjeto);
            if ((cDados.DataSetOk(dsCustom)) && (cDados.DataTableOk(dsCustom.Tables[0])))
            {
                if (codigoUsuarioResponsavel != int.Parse(dsCustom.Tables[0].Rows[0]["CodigoGerenteProjeto"].ToString()))
                {
                    //if (e.ButtonID == "btnExcluirCustom")
                    //{
                    //    e.IsVisible = DevExpress.Utils.DefaultBoolean.False;
                    //}

                    if ((gvDados.GetRowValues(e.VisibleIndex, "CodigoUsuarioResponsavel") != null))
                    {
                        string usuarioResponsavel = gvDados.GetRowValues(e.VisibleIndex, "CodigoUsuarioResponsavel").ToString();
                        if (codigoUsuarioResponsavel.ToString() != usuarioResponsavel)
                        {
                            if (e.ButtonID == "btnEditarCustom")
                                e.Visible = DevExpress.Utils.DefaultBoolean.False;
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void gvDados_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        /*
                if (e.RowType == GridViewRowType.Data)
                {
                    string anotacoes = e.GetValue("Anotacoes").ToString();
                    string tarefa = e.GetValue("DescricaoTarefa").ToString();

                    if (anotacoes != "" && anotacoes != null)
                    {
                        e.Row.Cells[1].Text = @"<img src=""../../imagens/TemMensage.png"" style=""border-width:0px;"" /><span> " + tarefa + "</span>";
                        //e.Row.Cells[1].Attributes.Add("onmouseover", "return overlib('" + anotacoes + "');");
                        //e.Row.Cells[1].Attributes.Add("onmouseout", "return nd();");
                    }
                }
          */
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        //if (true == e.DataColumn.Name.Equals("Tarefa"))
        //{
        //    ASPxGridView grid = (ASPxGridView)sender;

        //    string anotacoes = grid.GetRowValues(e.VisibleIndex, "Anotacoes").ToString();
        //    //string tarefa = string.Format(@"<span onclick=""showDetalhe();""  style=""cursor: pointer""> {0}</span>", e.GetValue("DescricaoTarefa").ToString());
        //    string tarefa = e.GetValue("DescricaoTarefa").ToString();

        //    if (anotacoes != "" && anotacoes != null)
        //        e.Cell.Text = @"<img src=""../../imagens/anotacao.png"" style=""border-width:0px;"" /> " + tarefa;
        //    else
        //        e.Cell.Text = @"<span> " + tarefa + "</span>";
        //}
        if (true == e.DataColumn.Name.Equals("M"))
        {
            ASPxGridView grid = (ASPxGridView)sender;

            string anotacoes = grid.GetRowValues(e.VisibleIndex, "Anotacoes").ToString();

            if (anotacoes != "" && anotacoes != null)
                e.Cell.Text = @"<img src=""../imagens/anotacao.png"" style=""border-width:0px;"" />";
        }

    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        Color color = new Color();

        if (e.RowType == GridViewRowType.Data)
        {
            string terminoReal = e.GetValue("TerminoReal").ToString();
            string terminoPrevisto = e.GetValue("TerminoPrevisto").ToString();
            string Descricaostatus = e.GetValue("DescricaoStatusTarefa").ToString();

            if ((terminoReal == "") && (DateTime.Parse(terminoPrevisto) < DateTime.Now) && (Descricaostatus == "Em Execução"))
            {
                int ri = Int32.Parse("FF", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("EE", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("EE", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.ForeColor = Color.Red;

            }

            if (Descricaostatus == "Concluída")
            {
                int ri = Int32.Parse("E4", System.Globalization.NumberStyles.HexNumber);
                int gi = Int32.Parse("FF", System.Globalization.NumberStyles.HexNumber);
                int bi = Int32.Parse("E1", System.Globalization.NumberStyles.HexNumber);

                color = Color.FromArgb(ri, gi, bi);
                e.Row.ForeColor = Color.Green;
            }
        }
    }

    #endregion

    #region VARIOS

    private void headerOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/frameEspacoTrabalho_MinhaTarefas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        Header.Controls.Add(cDados.getLiteral(@"<title>TO DO List</title>"));
    }

    private void defineAlturaTela(string resolucaoCliente)
    {   // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 190);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 105;
    }

    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoTarefa").ToString();
        return codigoDado;
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";

        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.

        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgErro = "";

        string esforcoReal  = (txtEsforcoReal.Text == "" ? "0" : txtEsforcoReal.Text);
        string statusTarefa = (ddlStatusTarefa.Value != null) ? ddlStatusTarefa.Value.ToString() : "NULL";
        string inicioReal   = ddlInicioReal.Text  == "" ? "NULL" : ("'" + ddlInicioReal.Text + "'");
        string terminoReal  = ddlTerminoReal.Text == "" ? "NULL" : ("'" + ddlTerminoReal.Text + "'");

        cDados.atualizaTarefaToDoList(chave, inicioReal, terminoReal, esforcoReal,
                                      mmAnotacoesBanco.Text.Replace("'", "''"),
                                      statusTarefa, codigoUsuarioResponsavel, ref msgErro);
        carregaGvDados();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
        gvDados.ClientVisible = false;
        return "";
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgErro = "";

        cDados.excluiTarefaToDoList(chave, codigoUsuarioResponsavel, ref msgErro);
        carregaGvDados();
        return "";
    }

    #endregion
}
