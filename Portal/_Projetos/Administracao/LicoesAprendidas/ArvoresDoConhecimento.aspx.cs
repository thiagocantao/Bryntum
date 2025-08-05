using DevExpress.Web;
using System;
using System.Data;
using System.Web.UI;

public partial class _Projetos_Administracao_LicoesAprendidas_ArvoresDoConhecimento : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private bool podeIncluir;
    private bool podeEditar;
    private bool podeExcluir;

    //26536 - Construir tela PRINCIPAL de cadastro de árvores de conhecimento
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

        this.TH(this.TS("RecursosCorporativos", "barraNavegacao"));

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();

        populaGrid();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ArvoresDoConhecimento.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ArvoresDoConhecimento"));

        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeLogada, "LA_EdtArvCnh");
        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeLogada, "LA_EdtArvCnh");
        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeLogada, "LA_EdtArvCnh");

        if (!Page.IsPostBack)
        {
            cDados.VerificaAcessoTela(this.Page, codigoUsuarioLogado, codigoEntidadeLogada, "EN", "LA_CnsArvCnh");
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, "Cadastro De Arvores do Conhecimento", "CADACO", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);           
        }
        cDados.aplicaEstiloVisual(this.Page);
    }
    private void populaGrid()
    {
        string comandosql = string.Format(@"
        SELECT 
		 [CodigoElementoArvore]	   = ac.[CodigoElementoArvore]							
		,[DescricaoElementoArvore] = ac.[DescricaoElementoArvore]					
		,[IndicaPodeExcluir] = CAST( CASE WHEN EXISTS (SELECT 1 
                                                         FROM [dbo].[LCA_ArvoreConhecimento] AS [ac2] 
                                                        WHERE ac2.[CodigoElementoArvoreSuperior] = ac.[CodigoElementoArvore] 
                                                          AND ac2.[DataExclusao] IS NULL )
					                      THEN 'N'
					                      ELSE 'S'
			                         END AS char(1))
         FROM [dbo].[LCA_ArvoreConhecimento] AS [ac]
         WHERE ac.[DataExclusao] IS NULL
	       AND ac.[CodigoEntidade]	= {0} -- @CodigoEntidadeContexto
	       AND ac.[CodigoTipoAssociacao] = dbo.f_GetCodigoTipoAssociacao('CX')
	       AND ac.[CodigoElementoArvoreSuperior] IS NULL;", codigoEntidadeLogada);
        DataSet ds = cDados.getDataSet(comandosql);
        gvDados.DataSource = ds;
        gvDados.DataBind();
    }



    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "gvDados.AddNewRow();", podeIncluir, false, false, "CadRecCorp", "Cadastro de Árvores do Conhecimento", this);

    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {

    }

    protected void gvDados_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK, timeout) 
        ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = "";
        ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "";
        ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "";
        ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "";
        ((ASPxGridView)sender).JSProperties["cp_eventoOK"] = "";
        ((ASPxGridView)sender).JSProperties["cp_timeout"] = "";

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
           SET @CodigoElementoArvoreSuperior = NULL
           SET @CodigoEstruturaAnalitica = 1
           SET @IndicaElementoFolha = 'N'
           SET @NivelCriticidadeConhecimento = NULL
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

           INSERT INTO [dbo].[LCA_ArvoreConhecimento]
           ([DescricaoElementoArvore]
           ,[CodigoElementoArvoreSuperior]
           ,[CodigoEstruturaAnalitica]
           ,[IndicaElementoFolha]
           ,[NivelCriticidadeConhecimento]
           ,[CodigoEntidade]
           ,[CodigoTipoAssociacao]
           ,[DataInclusao]
           ,[CodigoUsuarioInclusao]
           ,[DataExclusao]
           ,[CodigoUsuarioExclusao]
           ,[DataUltimaAlteracao]
           ,[CodigoUsuarioUltimaAlteracao]
           ,[CorBordaElemento]
           ,[CorFonteElemento]
           ,[CorFundoElemento])
     VALUES
           (@DescricaoElementoArvore
           ,@CodigoElementoArvoreSuperior
           ,@CodigoEstruturaAnalitica
           ,@IndicaElementoFolha
           ,@NivelCriticidadeConhecimento
           ,@CodigoEntidade
           ,@CodigoTipoAssociacao
           ,@DataInclusao
           ,@CodigoUsuarioInclusao
           ,@DataExclusao
           ,@CodigoUsuarioExclusao
           ,@DataUltimaAlteracao
           ,@CodigoUsuarioUltimaAlteracao
           ,@CorBordaElemento
           ,@CorFonteElemento
           ,@CorFundoElemento)", e.NewValues["DescricaoElementoArvore"].ToString().Replace("'", "''"), codigoEntidadeLogada, codigoUsuarioLogado);


        int registrosAfetados = 0;
        try
        {
            bool retorno = cDados.execSQL(comandosql, ref registrosAfetados);
            e.Cancel = retorno;
            if (retorno)
            {
                populaGrid();
                gvDados.CancelEdit();
            }
                

            ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = "Árvore de conhecimento cadastrada com sucesso!";
            ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "sucesso";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_timeout"] = "2000";

        }
        catch (Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = ex.Message;
            ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "erro";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "true";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_timeout"] = "null";
        }
        
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            string indicaPodeExcluir = (gvDados.GetRowValues(e.VisibleIndex, "IndicaPodeExcluir") != null) ? gvDados.GetRowValues(e.VisibleIndex, "IndicaPodeExcluir").ToString() : "";

            if ("btnExcluir" == e.ButtonID)
            {
                //Si Perfil se encuentra en la tabla [AcessoEtapasWf] y [RegrasnotificacionesRecursosWf]
                // entonces se puede eliminar el perfil.
                if (indicaPodeExcluir.Trim().ToUpper() == "S")
                {
                    //e.IsVisible = DevExpress.Utils.DefaultBoolean.True;
                    
                    if(podeExcluir == true)
                    {
                        e.Enabled = true;
                        e.Image.Url = "~/imagens/botoes/excluirReg02.PNG";
                    }
                    else
                    {
                        e.Enabled = false;
                        e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                        e.Text = "";
                    }                    
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                    e.Text = "";
                }
            }
            if("btnEditar" == e.ButtonID)
            {
                if (podeEditar == true)
                {
                    e.Enabled = true;
                    e.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                    e.Text = "";
                }
            }
        }
    }

    protected void gvDados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK, timeout) 
        ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = "";
        ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "";
        ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "";
        ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "";
        ((ASPxGridView)sender).JSProperties["cp_timeout"] = "";

        string comandosql = string.Format(@"
           DELETE FROM [dbo].[LCA_ArvoreConhecimento]
      WHERE CodigoElementoArvore = {0}", e.Keys[0]);


        int registrosAfetados = 0;
        try
        {
            bool retorno = cDados.execSQL(comandosql, ref registrosAfetados);
            e.Cancel = retorno;
            if (retorno)
            {
                populaGrid();
                gvDados.CancelEdit();
            }

            ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = "Árvore de conhecimento excluída com sucesso!";
            ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "sucesso";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_timeout"] = "2000";

        }
        catch (Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = ex.Message;
            ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "erro";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "true";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_timeout"] = "2000";
        }
    }

}