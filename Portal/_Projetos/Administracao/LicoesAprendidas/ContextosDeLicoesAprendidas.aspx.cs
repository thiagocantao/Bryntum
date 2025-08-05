using DevExpress.Web;
using System;
using System.Data;
using System.Web.UI;

public partial class _Projetos_Administracao_LicoesAprendidas_ContextosDeLicoesAprendidas : System.Web.UI.Page
{
    dados cDados;
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private bool podeIncluir;
    private bool podeEditar;
    private bool podeExcluir;

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

        codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeLogada = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        this.Title = cDados.getNomeSistema();

        populaGrid();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeLogada, "LA_CadCtx");
        podeEditar = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeLogada, "LA_CadCtx");
        podeExcluir = cDados.VerificaPermissaoUsuario(codigoUsuarioLogado, codigoEntidadeLogada, "LA_CadCtx");

        //Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        if (!Page.IsPostBack)
        {
            cDados.VerificaAcessoTela(this, codigoUsuarioLogado, codigoEntidadeLogada, codigoEntidadeLogada, "null", "EN", 0, "null", "LA_CnsCtx");
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, "Cadastro De Contextos", "CTXLCA", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
        cDados.aplicaEstiloVisual(this.Page);
    }
    private void populaGrid()
    {
        string comandosql = string.Format(@"
        SELECT 
			    [CodigoContexto]		= c.[CodigoContexto]
		    , [DescricaoContexto]	    = c.[DescricaoContexto]
				, [IndicaPodeExcluir]	= CAST( CASE WHEN EXISTS
					( SELECT TOP 1 1 
							FROM
								[dbo].[LinkObjeto]					AS [lo]

								INNER JOIN [dbo].[Projeto]			AS [p]		ON 
									(			p.[codigoProjeto] = lo.[CodigoObjetoLink]  
										AND p.[DataExclusao]	IS NULL )
							WHERE
										lo.[CodigoObjeto]		= c.[CodigoContexto]
								AND lo.[CodigoTipoObjeto]		= dbo.f_GetCodigoTipoAssociacao('CX') 
								AND lo.[CodigoObjetoPai]		= 0
								AND lo.[CodigoTipoObjetoLink]   = dbo.f_GetCodigoTipoAssociacao('PR')
								AND lo.[CodigoObjetoPaiLink]	= 0
								AND lo.[CodigoTipoLink]			= dbo.f_GetCodigoTipoLinkObjeto('AS')
					) THEN 'N' ELSE 'S' END AS char(1) )
	    FROM 
		    [dbo].[ContextoLicoesAprendidas] AS [c] 
	    WHERE
                c.[CodigoEntidade] = {0}
            AND	c.[DataExclusao] IS NULL;", codigoEntidadeLogada);
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
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "gvDados.AddNewRow();", podeIncluir, false, false, "LA_CadCtx", "Cadastro de Contextos de Lições Aprendidas", this);
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
            DECLARE @DescricaoContexto  varchar(255),
                @CodigoEntidade int,
                @DataInclusao datetime

            SET @DescricaoContexto = '{0}'
            SET @CodigoEntidade = {1}
            SET @DataInclusao = GETDATE()

            INSERT INTO [dbo].[ContextoLicoesAprendidas]
                ([DescricaoContexto], [CodigoEntidade], [DataInclusao])
            VALUES
                (@DescricaoContexto, @CodigoEntidade, @DataInclusao)", e.NewValues["DescricaoContexto"].ToString().Replace("'", "''"), codigoEntidadeLogada);

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


            ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = "Contexto cadastrado com sucesso!";
            ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "sucesso";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_timeout"] = "3000";

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
                    if (podeExcluir == true)
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
            if ("btnEditar" == e.ButtonID)
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
           DELETE FROM [dbo].[ContextoLicoesAprendidas] WHERE [CodigoContexto] = {0}", e.Keys[0]);

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

            ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = "Contexto excluído!";
            ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "sucesso";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_timeout"] = "3000";

        }
        catch (Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = ex.Message;
            ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "erro";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "true";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_timeout"] = "3000";
        }
    }

    protected void gvDados_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //mostraMensagem(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK, timeout) 
        ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = "";
        ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "";
        ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "";
        ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "";
        ((ASPxGridView)sender).JSProperties["cp_timeout"] = "";

        string comandosql = string.Format(@"
           DECLARE @DescricaoContexto  varchar(255), @CodigoContexto int;

           SET @CodigoContexto = {0}
           SET @DescricaoContexto = '{1}'

           UPDATE [dbo].[ContextoLicoesAprendidas] SET [DescricaoContexto] = @DescricaoContexto WHERE [CodigoContexto] = @CodigoContexto;
            ", e.Keys[0].ToString(), e.NewValues["DescricaoContexto"].ToString().Replace("'", "''"));

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

            ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = "Dados gravados!";
            ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "sucesso";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_timeout"] = "3000";

        }
        catch (Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cp_textoMsg"] = ex.Message;
            ((ASPxGridView)sender).JSProperties["cp_nomeImagem"] = "erro";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnOK"] = "true";
            ((ASPxGridView)sender).JSProperties["cp_mostraBtnCancelar"] = "false";
            ((ASPxGridView)sender).JSProperties["cp_timeout"] = "3000";
        }
    }
}