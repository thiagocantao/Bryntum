using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_sprint_antigo : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private string codigoCronogramaProjeto = "";
    private string resolucaoCliente = "";
    public bool podeIncluir = true;

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
        sdsProjectOwner.ConnectionString = cDados.classeDados.getStringConexao();

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        cDados.setaTamanhoMaximoMemo(txtObjetivos, 4000, lblContadorMemoDescricao);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();

        cDados.setaTamanhoMaximoMemo(txtObjetivos, 4000, lblContadorMemoDescricao);

        if (Request.QueryString["IDProjeto"] != null)
        {
            idProjeto = int.Parse(Request.QueryString["IDProjeto"].ToString());
        }
        gvDados.JSProperties["cp_CodigoProjeto"] = idProjeto.ToString();

        if (!IsPostBack)
        {
            int codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", "PR_Sprint");
        }

        var percentualConcluido = (int?)(null);
        var data = (DateTime?)(null);
        DataSet ds = cDados.getCronogramaGantt(idProjeto, "-1", 1, true, false, false, percentualConcluido, data);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            codigoCronogramaProjeto = ds.Tables[0].Rows[0]["CodigoCronogramaProjeto"].ToString();
        }

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        sdsProjectOwner.SelectCommand = cDados.getSelect_Usuario(codigoEntidadeUsuarioResponsavel, "");
        sdsProjectOwner.DataBind();

        populaTrabalhoAssociado();

        carregaGvDados();
        verificaVisibilidadeObjetos();
        cDados.aplicaEstiloVisual(Page);
    }

    private void populaTrabalhoAssociado()
    {
        //ddlPacoteTrabalho.Items.Clear();
        string selectCommand = string.Format(
         @"BEGIN
          SELECT -1 AS CodigoTarefa, -1 AS SequenciaTarefaCronograma, 'NENHUM PACOTE' AS NomeTarefa     
         union
         SELECT  tc.[CodigoTarefa]
				, tc.[SequenciaTarefaCronograma]
				, CONVERT(Varchar,tc.SequenciaTarefaCronograma) + ' - ' + tc.[NomeTarefa]	as NomeTarefa	
			FROM 
					[TarefaCronogramaProjeto]					AS [tc]
					
						INNER JOIN [CronogramaProjeto]	AS [cp]		ON 
							(cp.[CodigoCronogramaProjeto]	= tc.[CodigoCronogramaProjeto] and  tc.[CodigoCronogramaProjeto] = '{1}')
            INNER JOIN [TipoTarefaCronograma]	AS [ttc] ON (ttc.CodigoTipoTarefaCronograma = tc.CodigoTipoTarefaCronograma
																											 AND ttc.IniciaisTipoControladoSistema = 'ITERACAO')						
			WHERE
						(cp.[CodigoProjeto]				= {0} OR cp.CodigoProjeto IS NULL)
				AND cp.[DataExclusao]					IS NULL
				AND tc.[DataExclusao]					IS NULL				
        AND tc.[IndicaTarefaResumo] = 'N' 
        AND tc.CodigoTarefa  not in(SELECT i.CodigoTarefaItemEAP
                                      FROM [Agil_Iteracao]   i                                                        
                                     WHERE CodigoTarefaItemEAP is not null  
                                     AND i.CodigoCronogramaProjetoItemEAP = '{1}')                
     ORDER BY 2
END", idProjeto, codigoCronogramaProjeto);

        DataSet ds = cDados.getDataSet(selectCommand);
        ddlPacoteTrabalho.DataSource = ds;
        ddlPacoteTrabalho.TextField = "NomeTarefa";
        ddlPacoteTrabalho.ValueField = "CodigoTarefa";
        ddlPacoteTrabalho.DataBind();
    }

    private void carregaGvDados()
    {
        DataSet ds = cDados.getSprints(idProjeto, "");

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds;
            gvDados.DataBind();
        }
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int largura = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x'))) - 200;
        int altura = (alturaPrincipal - 190);

        gvDados.Settings.VerticalScrollableHeight = altura - 200;
        gvDados.Width = new Unit("100%");
    }
    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string codigoDado = "";
        if (gvDados.FocusedRowIndex != -1)
            codigoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        return codigoDado;
    }
    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/sprint_antigo.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<title>sprint</title>"));
        this.TH(this.TS("sprint_antigo", "barraNavegacao"));
    }
    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";
        ((ASPxGridView)sender).JSProperties["cp_MSG"] = "";

        if (e.Parameters == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                mensagemErro_Persistencia = "Item incluído com sucesso!";
            }

        }

        else if (e.Parameters == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                mensagemErro_Persistencia = "Item alterado com sucesso!";
            }

        }
        else if (e.Parameters == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();

            if (mensagemErro_Persistencia == "")
            {
                hfGeral.Set("StatusSalvar", "1");
                mensagemErro_Persistencia = "Item excluído com sucesso!";
            }
        }
        ((ASPxGridView)sender).JSProperties["cp_MSG"] = mensagemErro_Persistencia;
    }

    private void verificaVisibilidadeObjetos()
    {
        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "controlaClientesGestaoAgil");

        ddlPacoteTrabalho.JSProperties["cp_Visivel"] = "S";

        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["controlaClientesGestaoAgil"] + "" == "N")
        {
            ddlPacoteTrabalho.JSProperties["cp_Visivel"] = "N";
            tdPacote.Style.Add(HtmlTextWriterStyle.Display, "none");
            gvDados.Columns["NomePacoteTrabalho"].Visible = false;
        }
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";

        string chave = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoProjetoIteracao").ToString();
        DataSet ds = cDados.excluiSprint(chave, getChavePrimaria(), idProjeto.ToString());
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
        }
        return retorno;
    }

    private string persisteEdicaoRegistro()
    {
        string retorno = "";
        string tituloItem = txtTitulo.Text.Replace("'", "'+char(39)+'");
        string inicio = dtInicio.Date.ToString("dd/MM/yyyy"); // pegando a data no formato 103 do SQL Server, já que a instrução de insert assume essa forma
        string termino = dtTermino.Date.ToString("dd/MM/yyyy"); // pegando a data no formato 103 do SQL Server, já que a instrução de insert assume essa forma
        int codigoProjectOwner = int.Parse(ddlProjectOwner.Value.ToString());
        int codigoPacoteTrabalho = ddlPacoteTrabalho.Value == null ? -1 : int.Parse(ddlPacoteTrabalho.Value.ToString());
        string objetivos = txtObjetivos.Text.Replace("'", "'+char(39)+'");
        decimal fatorProdutividade = (decimal)(spnFatorProdutividade.Value) / 100;
        string chave = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoProjetoIteracao").ToString();

        DataSet ds = cDados.atualizaSprint(int.Parse(chave), tituloItem, inicio, termino, objetivos, codigoProjectOwner.ToString(), codigoPacoteTrabalho.ToString(), fatorProdutividade);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
        }
        return retorno;
    }

    private string persisteInclusaoRegistro()
    {
        string retorno = "";

        string tituloItem = txtTitulo.Text.Replace("'", "'+char(39)+'");
        string inicio = dtInicio.Date.ToString("dd/MM/yyyy"); // pegando a data no formato 103 do SQL Server, já que a instrução de insert assume essa forma
        string termino = dtTermino.Date.ToString("dd/MM/yyyy"); // pegando a data no formato 103 do SQL Server, já que a instrução de insert assume essa forma
        int codigoProjectOwner = int.Parse(ddlProjectOwner.Value.ToString());
        int codigoPacoteTrabalho = ddlPacoteTrabalho.Value == null ? -1 : int.Parse(ddlPacoteTrabalho.Value.ToString());
        string objetivos = txtObjetivos.Text.Replace("'", "'+char(39)+'");
        int codigoIteracao = 0;
        decimal fatorProdutividade = (decimal)(spnFatorProdutividade.Value) / 100;
        DataSet ds = cDados.incluiSprint(codigoEntidadeUsuarioResponsavel, tituloItem, inicio, termino, codigoProjectOwner, codigoPacoteTrabalho, objetivos, idProjeto, codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel, idProjeto, fatorProdutividade, codigoCronogramaProjeto);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
            codigoIteracao = (int)ds.Tables[0].Rows[0][1];
        }
        carregaGvDados();
        gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(codigoIteracao);
        return retorno;
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "PR_Sprint", "Sprint", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PR_Sprint");
    }

    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        if (e.ErrorTextKind == GridErrorTextKind.General)
        {
            e.ErrorText = e.Exception.Message;
        }
        else if (e.ErrorTextKind == GridErrorTextKind.RowValidate)
        {
            e.ErrorText = "Erro de validação: " + e.ErrorText;
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        string status = (gvDados.GetRowValues(e.VisibleIndex, "Status") == null) ? "" : gvDados.GetRowValues(e.VisibleIndex, "Status").ToString();

        if (e.ButtonID == "btnExcluirCustom")
        {
            if (status == "Não Iniciado")
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Text = Resources.traducao.sprint_antigo_n_o___permitido_excluir_uma_sprint_em_andamento_;
                e.Image.ToolTip  = Resources.traducao.sprint_antigo_n_o___permitido_excluir_uma_sprint_em_andamento_;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
    }

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }

    public string getDescricaoObjetosLista()
    {
        string retornoHTML = "";

        string codigoProjetoAtual = Eval("CodigoProjetoIteracao").ToString();
        string nomeProjetoAtual = Eval("Titulo").ToString();

        retornoHTML = "<table><tr><td>";
        string styleCor = "";

        //retornoHTML += "<img border='0' src='../imagens/projeto.PNG'/>";
        //Retirada a verificação de permissão (PBH). A linha abaixo foi colocada e as outras foram comentadas.
        retornoHTML += "</td><td>&nbsp;<a  " + styleCor + " class='LinkGrid' href='./indexResumoProjeto.aspx?IDProjeto=" + codigoProjetoAtual + "&NomeProjeto=" + nomeProjetoAtual + "' target='_parent'>" + nomeProjetoAtual + "</a>";

        retornoHTML += "</td></tr></table>";

        return retornoHTML;
    }

    protected void pnPacoteTrabalho_Callback(object sender, CallbackEventArgsBase e)
    {
        populaTrabalhoAssociado();
    }
}