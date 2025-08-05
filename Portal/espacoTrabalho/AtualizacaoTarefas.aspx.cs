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
using System.IO;
using DevExpress.XtraPrinting;
using System.Globalization;

public partial class espacoTrabalho_frameEspacoTrabalho_TimeSheet : System.Web.UI.Page
{
    dados cDados;
    private int codigoEntidade;
    int idUsuarioLogado;
    public int linha = 0;

    string definicaoToDoList = "To Do List";

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.Title = cDados.getNomeSistema();
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

        DataSet dsParametros = cDados.getParametrosSistema("labelToDoList", "VersaoMSProject");

        if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]))
        {
            if(dsParametros.Tables[0].Rows[0]["labelToDoList"] + "" != "")
                definicaoToDoList = dsParametros.Tables[0].Rows[0]["labelToDoList"] + "";        
        }


        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/TarefasAtualizacao.js""></script>"));
        this.TH(this.TS("TarefasAtualizacao"));

        defineAlturaTela();

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        cDados.aplicaEstiloVisual(Page);

        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack && !IsCallback)
        {
            gvDados.FilterExpression += " IndicaConcluida = 'Não'";

            if (Request.QueryString["Atrasadas"] != null && Request.QueryString["Atrasadas"].ToString() == "S")
                gvDados.FilterExpression += " AND IndicaAtrasada = 'Sim'";
        }
        gvDados.JSProperties["cp_Msg"] = "";
        carregaGrid();
        carregaComboRecursos();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, lblTituloTela.Text, "ATLTAR", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
    }

    private void carregaGrid()
    {
        DataSet ds = cDados.getTarefasAtualizacao(idUsuarioLogado, codigoEntidade, "");
                
        if (cDados.DataSetOk(ds))
        {
            string expressao = "NomeProjeto Like '%{0}%' OR NomeTarefa Like '%{0}%'" + "";
            gvDados.DataSource = cDados.getDataTableFiltrado(gvDados, ds, expressao);
            gvDados.DataBind();
        }
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        gvDados.Settings.VerticalScrollableHeight = (alturaPrincipal - 330); 
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex >= 0 && e.CellType == GridViewTableCommandCellType.Data)
        {
            string status = gvDados.GetRowValues(e.VisibleIndex, "StatusAprovacao").ToString();
            string possuiAnexo = gvDados.GetRowValues(e.VisibleIndex, "IndicaAnexo").ToString();

            if (e.ButtonID == "btnStatus")
            {
                e.Enabled = false;

                if (status == "PP")
                {
                    e.Image.Url = "~/imagens/botoes/tarefasPP.PNG";
                    e.Text = "Envio Pendente para Aprovação";
                }
                else
                {
                    if (status == "PA")
                    {
                        e.Image.Url = "~/imagens/botoes/tarefasPA.PNG";
                        e.Text = "Pendente de Aprovação";
                    }
                    else
                    {
                        if (status == "AP")
                        {
                            e.Image.Url = "~/imagens/botoes/salvar.gif";
                            e.Text = "Aprovado";
                        }
                        else
                        {
                            if (status == "RP")
                            {
                                e.Image.Url = "~/imagens/botoes/tarefaRecusada.PNG";
                                e.Text = "Reprovado";
                            }
                            else
                            {
                                if (status == "EA" || status == "ER")
                                {
                                    e.Image.Url = "~/imagens/botoes/tarefasPA.PNG";
                                    e.Text = "Em Processo de Aprovação/Reprovação. Não Poderá Ser Editado Durante o Processo!";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (e.ButtonID == "btnDetalhes")
                {
                    if (status == "EA" || status == "ER")
                    {
                        e.Image.Url = "~/imagens/botoes/editarRegDes.PNG";
                        e.Enabled = false;
                        e.Text = "";
                    }
                }
                else if (e.ButtonID == "btnDelegar")
                {
                    string tipoTarefa = gvDados.GetRowValues(e.VisibleIndex, "TipoTarefa").ToString();
                    string percConcluido = gvDados.GetRowValues(e.VisibleIndex, "PercConcluido").ToString() == "" ? "0" : gvDados.GetRowValues(e.VisibleIndex, "PercConcluido").ToString();

                    e.Visible = (tipoTarefa == "P" && double.Parse(percConcluido) == 0) ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
    }

    protected void callBack_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callBack.JSProperties["cp_OK"] = "";
        callBack.JSProperties["cp_Erro"] = "";

        string msgErro = "";
        bool retorno = cDados.publicaTarefasEntidade(idUsuarioLogado, codigoEntidade, ref msgErro);

        if (retorno)
        {
            callBack.JSProperties["cp_OK"] = "Tarefas Enviadas com Sucesso!";
            carregaGrid();
        }
        else
        {
            callBack.JSProperties["cp_Erro"] = "Erro ao Enviar as Tarefas para Aprovação!\n\nEntre em contato com o Administrador e informe a seguinte mensagem:\n\n" + msgErro;
        }
    }

    public string getNomeGrupo()
    {
        string descricaoGrupo = "";
        string icone = "";

        icone = "<td style='width:21px'>" + ((Eval("TipoTarefa").ToString() == "T") ? "<img src='../imagens/toDoList.png' alt='" + definicaoToDoList + "'/>" : "<img src='../imagens/projeto.PNG' alt='Projeto' />") + "</td>";

        descricaoGrupo = string.Format(@"<table><tr>{0}<td>{1}</td></tr></table>"
            , icone
            , Eval("NomeProjeto"));

        return descricaoGrupo;
    }    

    protected void gvExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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

            if (e.VisibleIndex > -1 && e.Column.Name.ToString() == "NomeProjeto")
            {
                string tipo = gvDados.GetRowValues(e.VisibleIndex, "TipoTarefa").ToString();
                string atrasada = gvDados.GetRowValues(e.VisibleIndex, "IndicaAtrasada").ToString();
                string critica = gvDados.GetRowValues(e.VisibleIndex, "IndicaCritica").ToString();

                e.BrickStyle.Font = new Font("Verdana", 8, FontStyle.Bold);

                if (tipo == "T")
                {
                    e.Text = definicaoToDoList + ": " + e.Text;
                }
                else
                {
                    e.Text = "Projeto: " + e.Text;
                }
            }
        }
    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex > -1 && e.RowType == GridViewRowType.Data)
        {
            string trabalhoPrevisto = gvDados.GetRowValues(e.VisibleIndex, "TrabalhoPrevisto").ToString() == "" ? "0" : gvDados.GetRowValues(e.VisibleIndex, "TrabalhoPrevisto").ToString();
            string trabalhoReal = gvDados.GetRowValues(e.VisibleIndex, "TrabalhoRealTotal").ToString() == "" ? "0" : gvDados.GetRowValues(e.VisibleIndex, "TrabalhoRealTotal").ToString();

            if (trabalhoReal != "")
            {
                if (float.Parse(trabalhoPrevisto) < float.Parse(trabalhoReal))
                    e.Row.Font.Underline = true;
            }
        }
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "AtlMinTar");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "abreTarefasEquipe()", true, true, true, "AtlMinTar", lblTituloTela.Text, this);        
    }

    #endregion

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        gvDados.JSProperties["cp_Msg"] = "";
        gvDados.JSProperties["cp_Erro"] = "";

        if (e.Parameters == "DLG")
        {
             string codigoAtribuicao = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoAtribuicao") == null ? "-1" : gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoAtribuicao").ToString();

            string comandoSQL = string.Format(@"
            BEGIN
                DECLARE @CodigoRecurso Int
	
	            SELECT @CodigoRecurso = CodigoRecursoCorporativo
		          FROM RecursoCorporativo
	             WHERE CodigoUsuario = {1}
                   AND CodigoEntidade = {3}

	            EXEC dbo.p_RealizaDelegacaoAtribuicao {0}, @CodigoRecurso, {2}
 												
             END
            ", codigoAtribuicao
             , idUsuarioLogado
             , ddlRecurso.Value
             , codigoEntidade);

            int regAf = 0;
            bool result = cDados.execSQL(comandoSQL, ref regAf);

            if (result == false)
                gvDados.JSProperties["cp_Erro"] = "Erro ao delegar a tarefa!";
            else
            {
                carregaGrid();
                gvDados.JSProperties["cp_Msg"] = "Tarefa delegada com sucesso!";
            }
        }
    }

    private void carregaComboRecursos()
    {
        string codigoAtribuicao = gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoAtribuicao") == null ? "-1" : gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoAtribuicao").ToString();

        string comandoSQL = string.Format(@"        
        BEGIN
	       DECLARE @CodigoRecurso Int
	
	        SELECT @CodigoRecurso = CodigoRecursoCorporativo
		      FROM RecursoCorporativo
	         WHERE CodigoUsuario = {1}
               AND CodigoEntidade = {2}

	        SELECT CodigoRecurso, 
                   NomeRecurso 
              FROM dbo.f_GetRecursosEquipeParaDelegar({0}, @CodigoRecurso)
             ORDER BY NomeRecurso
         END
", codigoAtribuicao
 ,idUsuarioLogado
 , codigoEntidade);

        DataSet ds = cDados.getDataSet(comandoSQL);

        ddlRecurso.DataSource = ds;
        ddlRecurso.TextField = "NomeRecurso";
        ddlRecurso.ValueField = "CodigoRecurso";
        ddlRecurso.DataBind();
    }

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "-1";
    }
}
