using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;

public partial class _Projetos_Administracao_LicoesAprendidas_popupArvoresConhecimentoEDT : System.Web.UI.Page
{
    dados cDados;
    public int alturaTelaUrl = 0;
    private int codigoUsuarioLogado;
    private int codigoEntidadeContexto;
    private int codigoElementoRaizArvore = -1;
    private int codigoElementoSelecionado = -1;
    public string resolucaoCliente;
    public int larguraTela = 0;
    public int alturaTela = 0;

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
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        cDados.getLarguraAlturaTela(resolucaoCliente, out larguraTela, out alturaTela);


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeContexto = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

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

        if (!IsPostBack)
        {

            cDados.VerificaAcessoTelaSemMaster(this, codigoUsuarioLogado, codigoEntidadeContexto, codigoEntidadeContexto, "null", "EN", 0, "null", "LA_CnsArvCnh");
        }

        if (Request.QueryString["ALT"] != null)
        {
            int.TryParse(Request.QueryString["ALT"] + "", out alturaTelaUrl);
        }

        if (Request.QueryString["CERARV"] != null)
        {
            int.TryParse(Request.QueryString["CERARV"] + "", out codigoElementoRaizArvore);
        }
        if (Request.QueryString["CS"] != null)
        {
            int.TryParse(Request.QueryString["CS"] + "", out codigoElementoSelecionado);
        }
        cDados.aplicaEstiloVisual(Page);
        carregaTlDados();
        carregaComboCriticidade();


    }

    private void carregaComboCriticidade()
    {
        string comandoSQL = string.Format(@"
        SELECT convert(varchar,[NivelCriticidade]) + ' - ' + convert(varchar, [DescricaoNivelCriticidade]) as [DescricaoNivelCriticidade]
              ,[NivelCriticidade]
         FROM [dbo].[LCA_NivelCriticidadeConhecimento]");

        DataSet ds = cDados.getDataSet(comandoSQL);
        comboCriticidade.TextField = "DescricaoNivelCriticidade";
        comboCriticidade.ValueField = "NivelCriticidade";
        comboCriticidade.DataSource = ds;
        comboCriticidade.DataBind();
    }

    private void carregaTlDados()
    {
        DataSet ds = getElementosArvore();

        if (cDados.DataSetOk(ds))
        {
            tlArvore.DataSource = ds;
            tlArvore.DataBind();
        }
    }
    public DataSet getElementosArvore()
    {
        string comandoSQL = string.Format(@"
        DECLARE @CodigoEntidadeContexto as int
              ,@CodigoUsuarioSistema as int
              ,@CodigoElementoRaizArvore as int
              ,@IniciaisTipoAssociacao as char(2)
              ,@CodigoTipoAssociacao as smallint
              ,@IndicaExibicaoLCA as char(1)

            set @CodigoEntidadeContexto = {0}
            set @CodigoUsuarioSistema = {1}
            set @CodigoElementoRaizArvore = {2}
            set @IniciaisTipoAssociacao = 'CX'
            set @CodigoTipoAssociacao = null
            set @IndicaExibicaoLCA = 'S'


            SELECT   [CodigoElementoArvore] 
             , [DescricaoElementoArvore] 
             , [CodigoElementoArvoreSuperior] 
             , [CodigoEstruturaAnalitica] 
             , [IndicaElementoFolha] 
             , [NivelCriticidadeConhecimento] 
           	 ,(SELECT convert(varchar,ncc.[NivelCriticidade]) + ' - ' + convert(varchar, ncc.[DescricaoNivelCriticidade]) 
                    FROM [dbo].[LCA_NivelCriticidadeConhecimento] ncc 
					where ncc.NivelCriticidade = [NivelCriticidadeConhecimento])  as DescricaoNIvelCriticidade
             , [CodigoTipoAssociacao]
             , [DataInclusao] 
             , [CodigoUsuarioInclusao] 
             , [DataExclusao] 
             , [CodigoUsuarioExclusao] 
             , [DataUltimaAlteracao] 
             , [CodigoUsuarioUltimaAlteracao] 
             , [CorBordaElemento]
             , [CorFonteElemento]
             , [CorFundoElemento] 
             , [IndicaPodeExcluir] 
             , [IndicaTipoElemento] 
 
              FROM [dbo].[f_lca_GetArvoreConhecimento] (
               @CodigoEntidadeContexto
              ,@CodigoUsuarioSistema
              ,@CodigoElementoRaizArvore
              ,@IniciaisTipoAssociacao
              ,@CodigoTipoAssociacao
              ,@IndicaExibicaoLCA)
        ", codigoEntidadeContexto, codigoUsuarioLogado, codigoElementoRaizArvore);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    public string getDescricaoObjetosLista()
    {
        StringBuilder retornoHTML = new StringBuilder();

        string codigoElementoArvore = Eval("CodigoElementoArvore").ToString();
        string descricaoElementoArvore = Eval("DescricaoElementoArvore").ToString();
        string indicaElementoFolha = Eval("IndicaElementoFolha").ToString();
        string estiloIndicaEementoFolha = indicaElementoFolha == "S" ? "style='color: #1A0DAB'" : "";
        string indicaPodeExcluir = Eval("IndicaPodeExcluir").ToString();
        string idEditar = "btnEditar_" + codigoElementoArvore;
        string idExcluir = "btnExcluir_" + codigoElementoArvore;
        string indicaTipoElemento = Eval("IndicaTipoElemento").ToString();

        retornoHTML.AppendLine(string.Format(@"<table {0}>", estiloIndicaEementoFolha));
        retornoHTML.AppendLine("<tr>");
        retornoHTML.AppendLine("<td>");
        if (indicaTipoElemento != "LA")
        {
            retornoHTML.AppendLine("<img border='0' src='../../../imagens/botoes/editarReg02.PNG' style='width: 21px; height: 18px;cursor:pointer;margin-right:5px' title='' onclick='editar(" + codigoElementoArvore + ")' ID='" + idEditar + "' />");
        }

        retornoHTML.AppendLine("</td>");
        retornoHTML.AppendLine("<td>");

        if (indicaTipoElemento != "LA")
        {
            if (indicaPodeExcluir == "S")
            {
                retornoHTML.AppendLine("<img border='0' src='../../../imagens/botoes/excluirReg02.PNG' style='width: 21px; height: 18px;cursor:pointer;margin-right:5px' title='' onclick='excluir(" + codigoElementoArvore + ")' ID='" + idExcluir + "' />");
            }
            else
            {
                retornoHTML.AppendLine("<img border='0' src='../../../imagens/botoes/excluirRegDes.PNG' style='width: 21px; height: 18px;cursor:pointer;margin-right:5px' title='' ID='" + idExcluir + "' />");
            }
        }
        retornoHTML.AppendLine("</td>");
        retornoHTML.AppendLine("<td>");
        if (indicaTipoElemento != "LA")
        {
            retornoHTML.AppendLine("<span style='cursor:pointer'>" + descricaoElementoArvore + "</span>");
        }
        else if (indicaTipoElemento == "LA")
        {
            retornoHTML.AppendLine("<img src='../../../imagens/botoes/elementoArvoreConhecimento.png' style='width: 21px; height: 18px;cursor:pointer;margin-right:5px' /><span style='cursor:pointer'>" + "<a href='/_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=" + codigoElementoArvore.Replace("-", "") + "' target='_top'> " + descricaoElementoArvore + " </a>" + "</span>");
        }
        retornoHTML.AppendLine("</td>");

        retornoHTML.AppendLine("</tr>");
        retornoHTML.AppendLine("</table>");

        return retornoHTML.ToString();
    }
    protected void menu_Init(object sender, EventArgs e)
    {
        DevExpress.Web.MenuItem btnIncluir = (sender as ASPxMenu).Items.FindByName("btnIncluir");
        btnIncluir.ToolTip = Resources.traducao.planoContasFluxoCaixa_incluir_uma_nova_conta_abaixo_da_conta_selecionada_;
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "abrePopUp('Conta','Incluir');TipoOperacao = 'Incluir';", false, false, false, "ArConh", "Arvore Conhecimento", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        cDados.exportaTreeList(ASPxTreeListExporter2, "XLS");
    }

    protected void ASPxTreeListExporter1_RenderBrick(object sender, DevExpress.Web.ASPxTreeList.ASPxTreeListExportRenderBrickEventArgs e)
    {
        if (e.Column.FieldName == "IndicaElementoFolha")
        {

            e.BrickStyle.BackColor = Color.Red;
        }
    }



    protected void tlArvore_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK, timeout) 
        ((ASPxTreeList)sender).JSProperties["cp_textoMsg"] = "";
        ((ASPxTreeList)sender).JSProperties["cp_nomeImagem"] = "";
        ((ASPxTreeList)sender).JSProperties["cp_mostraBtnOK"] = "";
        ((ASPxTreeList)sender).JSProperties["cp_mostraBtnCancelar"] = "";
        ((ASPxTreeList)sender).JSProperties["cp_eventoOK"] = "";
        ((ASPxTreeList)sender).JSProperties["cp_timeout"] = "";

        string comandoSQL = string.Format(@"
        DECLARE @in_codigoEntidadeContexto int
        DECLARE @in_codigoUsuarioSistema int
        DECLARE @in_codigoElementoArvore int

        SET @in_codigoEntidadeContexto = {0}
        SET @in_codigoUsuarioSistema = {1}
        SET @in_codigoElementoArvore = {2}

        EXECUTE  [dbo].[p_lca_excluiElementoArvoreConhecimento] 
        @in_codigoEntidadeContexto
        ,@in_codigoUsuarioSistema
        ,@in_codigoElementoArvore", codigoEntidadeContexto, codigoUsuarioLogado, e.Keys[0]);
        try
        {
            int quantidade = 0;
            cDados.execSQL(comandoSQL, ref quantidade);
            tlArvore.CancelEdit();
            carregaTlDados();
            e.Cancel = true;
            ((ASPxTreeList)sender).JSProperties["cp_textoMsg"] = "Item excluído com sucesso!";
            ((ASPxTreeList)sender).JSProperties["cp_nomeImagem"] = "sucesso";
            ((ASPxTreeList)sender).JSProperties["cp_mostraBtnOK"] = "false";
            ((ASPxTreeList)sender).JSProperties["cp_mostraBtnCancelar"] = "false";
            ((ASPxTreeList)sender).JSProperties["cp_timeout"] = "2000";
        }
        catch (Exception ex)
        {
            ((ASPxTreeList)sender).JSProperties["cp_textoMsg"] = ex.Message;
            ((ASPxTreeList)sender).JSProperties["cp_nomeImagem"] = "erro";
            ((ASPxTreeList)sender).JSProperties["cp_mostraBtnOK"] = "true";
            ((ASPxTreeList)sender).JSProperties["cp_mostraBtnCancelar"] = "false";
        }

    }

    protected void tlArvore_NodeUpdating(ASPxCallback cb)
    {
        //mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK, timeout) 
        cb.JSProperties["cp_textoMsg"] = "";
        cb.JSProperties["cp_nomeImagem"] = "";
        cb.JSProperties["cp_mostraBtnOK"] = "";
        cb.JSProperties["cp_mostraBtnCancelar"] = "";
        cb.JSProperties["cp_eventoOK"] = "";
        cb.JSProperties["cp_timeout"] = "";

        string comandoSQL = string.Format(@"UPDATE [dbo].[LCA_ArvoreConhecimento]
                                               SET [DescricaoElementoArvore] = '{0}',
                                                   [NivelCriticidadeConhecimento] = {2},
                                                   [IndicaElementoFolha] = '{3}',
                                                   [DataUltimaAlteracao] = GETDATE(),
                                                   [CodigoUsuarioUltimaAlteracao] = {4}
                                             WHERE CodigoElementoArvore = {1}", txtDescricaoElementoArvore.Text.Replace("'", "''"), tlArvore.FocusedNode.GetValue("CodigoElementoArvore"), comboCriticidade.Value == null ? "NULL" : comboCriticidade.Value, radioIndicaElementoFolha.Value, codigoUsuarioLogado);
        try
        {
            int quantidade = 0;
            var retorno = cDados.execSQL(comandoSQL, ref quantidade);
            cb.JSProperties["cp_textoMsg"] = "Item atualizado com sucesso!";
            cb.JSProperties["cp_nomeImagem"] = "sucesso";
            cb.JSProperties["cp_mostraBtnOK"] = "false";
            cb.JSProperties["cp_mostraBtnCancelar"] = "false";
            cb.JSProperties["cp_timeout"] = "2000";
            carregaTlDados();
        }
        catch (Exception ex)
        {
            cb.JSProperties["cp_textoMsg"] = ex.Message;
            cb.JSProperties["cp_nomeImagem"] = "erro";
            cb.JSProperties["cp_mostraBtnOK"] = "true";
            cb.JSProperties["cp_mostraBtnCancelar"] = "false";
        }
    }

    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "processaInclusao();", true, false, false, "CadRecCorp", "Cadastro de items do Conhecimento", this);
    }

    protected void tlArvore_NodeInserting(ASPxCallback cb)
    {


        cb.JSProperties["cp_textoMsg"] = "";
        cb.JSProperties["cp_nomeImagem"] = "";
        cb.JSProperties["cp_mostraBtnOK"] = "";
        cb.JSProperties["cp_mostraBtnCancelar"] = "";
        cb.JSProperties["cp_eventoOK"] = "";
        cb.JSProperties["cp_timeout"] = "";

        string comandosql = string.Format(@"
           DECLARE @DescricaoElementoArvore  varchar(50),
           @CodigoElementoArvoreSuperior  int,
           @CodigoEstruturaAnalitica varchar(50),
           @IndicaElementoFolha char(1),
           @NivelCriticidadeConhecimento tinyint,
           @CodigoEntidade int,
           @CodigoTipoAssociacao smallint,
           @DataInclusao datetime,
           @CodigoUsuarioInclusao int,
           @DataExclusao datetime,
           @CodigoUsuarioExclusao int,
           @DataUltimaAlteracao datetime,
           @CodigoUsuarioUltimaAlteracao int,
           @CorBordaElemento varchar(7),
           @CorFonteElemento varchar(7),
           @CorFundoElemento varchar(7)

           SET @DescricaoElementoArvore = '{0}'
           SET @CodigoElementoArvoreSuperior = {3}
           SET @CodigoEstruturaAnalitica = 1
           SET @IndicaElementoFolha = '{5}'
           SET @NivelCriticidadeConhecimento = {4}
           SET @CodigoEntidade = {1}
           SET @CodigoTipoAssociacao = dbo.f_GetCodigoTipoAssociacao('CX')
           SET @DataInclusao = GETDATE()
           SET @CodigoUsuarioInclusao = {2}
           SET @DataExclusao = NULL
           SET @CodigoUsuarioExclusao = NULL
           SET @DataUltimaAlteracao = NULL
           SET @CodigoUsuarioUltimaAlteracao = NULL
           SET @CorBordaElemento = NULL
           SET @CorFonteElemento = NULL
           SET @CorFundoElemento = NULL

        EXECUTE  [dbo].[p_lca_insereElementoArvoreConhecimento] 
           @CodigoEntidade
          ,@CodigoUsuarioInclusao
          ,@DescricaoElementoArvore
          ,@CodigoElementoArvoreSuperior
          ,@CodigoEstruturaAnalitica
          ,@IndicaElementoFolha
          ,@NivelCriticidadeConhecimento
          ,@CodigoTipoAssociacao
          ,@CorBordaElemento
          ,@CorFonteElemento
          ,@CorFundoElemento", txtDescricaoElementoArvore.Text.Replace("'", "''"),
           codigoEntidadeContexto,
           codigoUsuarioLogado,
           tlArvore.FocusedNode.GetValue("CodigoElementoArvore"),
           comboCriticidade.Value == null ? "NULL" : comboCriticidade.Value, radioIndicaElementoFolha.Value);


        int registrosAfetados = 0;
        try
        {
            bool retorno = cDados.execSQL(comandosql, ref registrosAfetados);
            if (retorno)
            {
               
                carregaTlDados();
            }


            cb.JSProperties["cp_textoMsg"] = "Item cadastrado com sucesso!";
            cb.JSProperties["cp_nomeImagem"] = "sucesso";
            cb.JSProperties["cp_mostraBtnOK"] = "false";
            cb.JSProperties["cp_mostraBtnCancelar"] = "false";
            cb.JSProperties["cp_timeout"] = "2000";

        }
        catch (Exception ex)
        {
            cb.JSProperties["cp_textoMsg"] = ex.Message;
            cb.JSProperties["cp_nomeImagem"] = "erro";
            cb.JSProperties["cp_mostraBtnOK"] = "true";
            cb.JSProperties["cp_mostraBtnCancelar"] = "false";
            cb.JSProperties["cp_timeout"] = "null";
        }
    }

    protected void callbackTela_Callback(object source, CallbackEventArgs e)
    {
        var modo = e.Parameter;
        if(modo == "E")
        {
            tlArvore_NodeUpdating((ASPxCallback)source);
        }
        else if(modo == "I")
        {
            tlArvore_NodeInserting((ASPxCallback)source);
        }
    }

    protected void tlArvore_FocusedNodeChanged(object sender, EventArgs e)
    {
        string[] x = new string[((ASPxTreeList)sender).FocusedNode.ChildNodes.Count];
        for (int j = 0; j < x.Length; j++)
        {
            x[j] = ((ASPxTreeList)sender).FocusedNode.ChildNodes[j].GetValue("DescricaoElementoArvore").ToString();
        }
        string output = JsonConvert.SerializeObject(x);
        ((ASPxTreeList)sender).JSProperties["cpChildNodes"] = output;        
    }
}