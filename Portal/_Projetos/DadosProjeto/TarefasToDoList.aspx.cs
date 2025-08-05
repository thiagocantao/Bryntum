using System;
using System.Data;
using DevExpress.Web;
using System.Drawing;

public partial class _Projetos_DadosProjeto_TarefasToDoList : System.Web.UI.Page
{
    dados cDados;

    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    private string resolucaoCliente = "";
    public bool podeIncluir = true;
    bool podeEditar = true;
    bool podeExcluir = true;

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
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();

        if (Request.QueryString["idProjeto"] != null)
            idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());

        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_IncTdl");
        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_AltTdl");
        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, "null", "PR", 0, "null", "PR_ExcTdl");

        cDados.verificaPermissaoProjetoInativo(idProjeto, ref podeIncluir, ref podeEditar, ref podeExcluir);

        cDados.aplicaEstiloVisual(this);
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        dsResponsavel.ConnectionString = cDados.classeDados.getStringConexao();
        headerOnTela();        

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_CnsTdl");
        }

        if (!IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            populaCombo();
        }

        carregaComboResponsaveis();

        carregaGvDados();

        if (!IsPostBack && !IsCallback)
        {
            if (Request.QueryString["Estagio"] != null && Request.QueryString["Estagio"].ToString() != "")
            {
                string descricaoStatusEmExecucao = cDados.getDescricaoStatusTarefaToDoList(1);
                gvDados.FilterExpression = "Estagio = '" + Request.QueryString["Estagio"].ToString() + "'";
            }
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
        int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        DataSet ds = cDados.getProjetosToDoList(codigoEntidade, idProjeto);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }
    //Verifica Código estatus da tarefa para validar permissões de botões Crud a mesma.
    protected void gvToDoList_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        try
        {
            if(e.VisibleIndex < 0)
            {
                return;
            }

            string codigoStatus = gvDados.GetRowValues(e.VisibleIndex, "CodigoStatusTarefa").ToString();

            if (e.ButtonID == "btnExcluirCustom")
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

            if (e.ButtonID == "btnEditarCustom")
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
        }
        catch (Exception)
        {
        }
    }
    protected void gvDados_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        string anotacoes = grid.GetRowValues(e.VisibleIndex, "Anotacoes").ToString();
        if (e.DataColumn.Name.Equals("M"))
        {
            if (anotacoes != "" && anotacoes != null)
            {
                e.Cell.Text += @"<img src=""../../imagens/anotacao.png"" style=""border-width:0px;"" />";
                e.Cell.ToolTip = "Anotações";
            }

            //Insere a Marcação de Estatus na coluna M
            switch (grid.GetRowValues(e.VisibleIndex, "Estagio").ToString())
            {
                case "Concluida":
                    e.Cell.Text = e.Cell.Text + @"<img src=""../../imagens/verde.gif"" alt=""Concluída"" style=""border-width:0px;"" />";
                    e.Cell.ForeColor = Color.FromName("#619340");
                    break;
                case "Future":
                    e.Cell.Text = e.Cell.Text + @"<img src=""../../imagens/verde.gif"" alt=""Concluída"" style=""border-width:0px;"" />";
                    e.Cell.ForeColor = Color.FromName("#619340");
                    break;
                case "Completed":
                    e.Cell.Text = e.Cell.Text + @"<img src=""../../imagens/verde.gif"" alt=""Concluída"" style=""border-width:0px;"" />";
                    e.Cell.ForeColor = Color.FromName("#619340");
                    break;
                case "Late":
                    e.Cell.Text = e.Cell.Text + @"<img src=""../../imagens/vermelho.gif"" alt=""Atrasada"" style=""border-width:0px;"" />";
                    break;
                case "Atrasada":
                    e.Cell.Text = e.Cell.Text + @"<img src=""../../imagens/vermelho.gif"" alt=""Atrasada"" style=""border-width:0px;"" />";
                    break;
            }
            e.Cell.ToolTip = grid.GetRowValues(e.VisibleIndex, "DescricaoStatusTarefa").ToString();
        }
    }
    // retorna a primary key da tabela.
    private string setaStatusGridView()
    {
        return "-1";
    }
    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string terminoReal      = e.GetValue("TerminoReal").ToString();
            string terminoPrevisto  = e.GetValue("TerminoPrevisto").ToString();
            string Descricaostatus  = e.GetValue("DescricaoStatusTarefa").ToString();
            string Estagio = e.GetValue("Estagio").ToString();

            if ((terminoReal == "") && (DateTime.Parse(terminoPrevisto) < DateTime.Parse(DateTime.Now.ToShortDateString())) && (Descricaostatus == "Em Execução"))
            {
                e.Row.ForeColor = Color.FromName("#fc6e51");
            }

            //Validação Português
            if (Descricaostatus == "Concluída")
            {                
                e.Row.ForeColor = Color.FromName("#619340");
            }


            //Validação Inglês
            if (Descricaostatus == "Completed")
            {
                e.Row.ForeColor = Color.FromName("#619340");
            }


            //Atrasada Português
            if (Estagio == "Atrasada")
            {
                e.Row.ForeColor = Color.FromName("#fc6e51");
            }
            //Atrasada Inglês
            if (Estagio == "Late")
            {
                e.Row.ForeColor = Color.FromName("#fc6e51");
            }
        }
    }

    #endregion

    #region VARIOS

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/TarefasToDoList.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "TarefasToDoList"));
        Header.Controls.Add(cDados.getLiteral(@"<title>TO DO List</title>"));
    }
    #endregion

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados.
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_Sucesso"] = "";
        pnCallback.JSProperties["cp_Erro"] = "";

        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                mensagemErro_Persistencia = Resources.traducao.TarefasToDoList_tarefa_inclu_da_com_sucesso_;
                pnCallback.JSProperties["cp_Sucesso"] = mensagemErro_Persistencia;
            }
            else
            {
                pnCallback.JSProperties["cp_Erro"] = mensagemErro_Persistencia;
            }

        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                mensagemErro_Persistencia = Resources.traducao.TarefasToDoList_tarefa_alterada_com_sucesso_;
                pnCallback.JSProperties["cp_Sucesso"] = mensagemErro_Persistencia;
            }
            else
            {
                pnCallback.JSProperties["cp_Erro"] = mensagemErro_Persistencia;
            }

        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                mensagemErro_Persistencia = Resources.traducao.TarefasToDoList_tarefa_exclu_da_com_sucesso_;
                pnCallback.JSProperties["cp_Sucesso"] = mensagemErro_Persistencia;
            }
            else
            {
                pnCallback.JSProperties["cp_Erro"] = mensagemErro_Persistencia;
            }
        }
    }
    
    private string persisteInclusaoRegistro() // Método responsável pela Inclusao do registro
    {
        string msgErro = "";

        string nomeTarefa = txtDescricaoTarefaBanco.Text.Replace("'", "' + char(39)+ '");
        int codigoResponsavel = int.Parse(ddlResponsavel.Value.ToString());
        string prioridade = ddlPrioridade.Value.ToString();
        string statusTarefa = (ddlStatusTarefa.Value != null) ? ddlStatusTarefa.Value.ToString() : "NULL";
        string inicioPrevisto = ddlInicioPrevisto.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlInicioPrevisto.Date.ToString("dd/MM/yyyy") + "', 103)");
        string terminoPrevisto = ddlTerminoPrevisto.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlTerminoPrevisto.Date.ToString("dd/MM/yyyy") + "', 103)");
        string esforcoPrevisto = (txtEsforcoPrevistoBanco.Text == "" ? "0" : txtEsforcoPrevistoBanco.Text);
        string esforcoReal = (txtEsforcoReal.Text == "" ? "0" : txtEsforcoReal.Text);
        string custoPrevisto = (txtCustoPrevistoBanco.Text == "" ? "0" : txtCustoPrevistoBanco.Text);
        string custoReal = (txtCustoRealBanco.Text == "" ? "0" : txtCustoRealBanco.Text);
        string inicioReal = ddlInicioReal.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlInicioReal.Date.ToString("dd/MM/yyyy") + "', 103)");
        string terminoReal = ddlTerminoReal.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlTerminoReal.Date.ToString("dd/MM/yyyy") + "', 103)");
        string anotacoes = mmAnotacoesBanco.Text.Replace("'", "' + char(39)+ '");

        cDados.IncluiTarefaToDoListProjeto(codigoEntidadeUsuarioResponsavel, idProjeto, codigoUsuarioResponsavel, nomeTarefa, codigoResponsavel, prioridade, statusTarefa
                            , inicioPrevisto, terminoPrevisto, esforcoPrevisto, esforcoReal, custoPrevisto, custoReal, inicioReal, terminoReal, anotacoes, ref msgErro);
        carregaGvDados();
        return msgErro;
    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgErro = "";

        string nomeTarefa = txtDescricaoTarefaBanco.Text.Replace("'", "' + char(39)+ '");
        int codigoResponsavel = int.Parse(ddlResponsavel.Value.ToString());
        string prioridade = ddlPrioridade.Value.ToString();
        string statusTarefa = (ddlStatusTarefa.Value != null) ? ddlStatusTarefa.Value.ToString() : "NULL";
        string inicioPrevisto = ddlInicioPrevisto.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlInicioPrevisto.Date.ToString("dd/MM/yyyy") + "', 103)");
        string terminoPrevisto = ddlTerminoPrevisto.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlTerminoPrevisto.Date.ToString("dd/MM/yyyy") + "', 103)");
        string esforcoPrevisto = (txtEsforcoPrevistoBanco.Text == "" ? "0" : txtEsforcoPrevistoBanco.Text);
        string esforcoReal = (txtEsforcoReal.Text == "" ? "0" : txtEsforcoReal.Text);
        string custoPrevisto = (txtCustoPrevistoBanco.Text == "" ? "0" : txtCustoPrevistoBanco.Text);
        string custoReal = (txtCustoRealBanco.Text == "" ? "0" : txtCustoRealBanco.Text);
        string inicioReal = ddlInicioReal.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlInicioReal.Date.ToString("dd/MM/yyyy") + "', 103)");
        string terminoReal = ddlTerminoReal.Text == "" ? "NULL" : ("CONVERT(DateTime, '" + ddlTerminoReal.Date.ToString("dd/MM/yyyy") + "', 103)");
        string anotacoes = mmAnotacoesBanco.Text.Replace("'", "' + char(39)+ '");

        cDados.atualizaTarefaToDoListProjeto(chave, codigoUsuarioResponsavel, nomeTarefa, codigoResponsavel, prioridade, statusTarefa
                                    , inicioPrevisto, terminoPrevisto, esforcoPrevisto, esforcoReal, custoPrevisto, custoReal
                                    , inicioReal, terminoReal, anotacoes, ref msgErro);
        carregaGvDados();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(chave);
        gvDados.ClientVisible = false;
        return msgErro;
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {   // busca a chave primaria
        string chave = getChavePrimaria();
        string msgErro = "";

        cDados.excluiTarefaToDoList(chave, codigoUsuarioResponsavel, ref msgErro);
        carregaGvDados();
        return msgErro;
    }

    #endregion

    private void carregaComboResponsaveis()
    {
        ddlResponsavel.Columns[0].FieldName = "NomeUsuario";
        ddlResponsavel.Columns[1].FieldName = "EMail";
        ddlResponsavel.TextField = "NomeUsuario";
        ddlResponsavel.ValueField = "CodigoUsuario";
        ddlResponsavel.TextFormatString = "{0}";
 
    }
    
    protected void ddlResponsavel_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
    {
        if (e.Value != null)
        {
            long value = 0;
            if (!Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsResponsavel.SelectCommand = cDados.getSQLComboUsuariosPorID(codigoEntidadeUsuarioResponsavel);

            dsResponsavel.SelectParameters.Clear();
            dsResponsavel.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsResponsavel;
            comboBox.DataBind();
        }
    }

    protected void ddlResponsavel_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;

        string comandoSQL = cDados.getSQLComboUsuarios(codigoEntidadeUsuarioResponsavel, e.Filter, "");

        cDados.populaComboVirtual(dsResponsavel, comandoSQL, comboBox, e.BeginIndex, e.EndIndex);

    }
    
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
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
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ToDoPrj");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados);", true, true, false, "ToDoPrj", "Tarefas Projetos", this);
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


