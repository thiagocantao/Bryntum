using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_DadosProjeto_UODemanda : System.Web.UI.Page
{
    dados cDados;

    private int alturaPrincipal = 0;
    private int idUsuarioLogado;
    private int CodigoEntidade;
    private int codigoProjeto;

    private string resolucaoCliente;

    public bool podeIncluir = false;
    public bool podeEditar = false;
    public bool podeExcluir = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);


        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        podeIncluir = cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "PR_UOVinculado");
        podeEditar = podeIncluir;
        podeExcluir = podeIncluir;
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        HeaderOnTela();
        defineAlturaTela(resolucaoCliente);
        bool retorno = int.TryParse(Request.QueryString["IDProjeto"].ToString() + "", out codigoProjeto);
        carregaGvDados();
        cDados.aplicaEstiloVisual(this);

    }

    private void carregaGvDados()
    {
        string comandoSQL = string.Format(@"
        SELECT [CodigoUO]
              ,[NumeroProtocolo]
              ,[DataInclusao]
              ,[CodigoUsuarioInclusao]
              ,[DataUltimaAlteracao]
              ,[CodigoUsuarioUltimaAlteracao]
              ,[DataExclusao]
              ,[CodigoUsuarioExclusao]
         FROM [pbh_DemandaUO] where [DataExclusao] is null 
          AND CodigoControleCCG = (SELECT TOP 1 d.NumeroDemanda AS CodigoControleCCG
                                     FROM Demanda d
                                    WHERE d.CodigoProjetoDemanda = {0})", codigoProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 280;
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/UODemanda.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "UODemanda"));

    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "UODemanda", "Demandas de U.O.", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "UODemanda");
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";
        ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "";
        ((ASPxGridView)sender).JSProperties["cp_erro"] = "";


        if (e.Parameters == "Incluir")
        {
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "Registro de U.O. incluído com sucesso!";
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameters == "Editar")
        {
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "Registro de U.O. alterado com sucesso!";
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameters == "Excluir")
        {
            ((ASPxGridView)sender).JSProperties["cp_sucesso"] = "Registro de U.O. excluído com sucesso!";
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }
        if (mensagemErro_Persistencia != "")
        {// alguma coisa deu errado...
            ((ASPxGridView)sender).JSProperties["cp_erro"] = mensagemErro_Persistencia;
            //if (e.Parameters != "Excluir")
            //    gvDados.ClientVisible = false;
        }
    }

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";
        string chave = getChavePrimaria();

        string comandoSQL = string.Format(@"
        DECLARE @CodigoUO char(4)
        DECLARE @DataExclusao datetime
        DECLARE @CodigoUsuarioExclusao int

        SET @DataExclusao = GETDATE()
        SET @CodigoUsuarioExclusao = {1}

        UPDATE [dbo].[pbh_DemandaUO]
           SET [CodigoUsuarioExclusao] = @CodigoUsuarioExclusao
              ,[DataExclusao] = @DataExclusao
             WHERE CodigoUO = {0} 
               AND CodigoControleCCG = (SELECT TOP 1 d.NumeroDemanda AS CodigoControleCCG
                                          FROM Demanda d
                                         WHERE d.CodigoProjetoDemanda = {2}) ", chave, idUsuarioLogado, codigoProjeto);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoSQL + Environment.NewLine +
            cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
            if (retorno.Trim().ToLower() == "ok")
            {
                retorno = string.Empty;
            }
        }
        return retorno;
    }

    private string persisteEdicaoRegistro()
    {
        string retorno = "";

        string chave = getChavePrimaria();

        string comandoSQL = string.Format(@"
        DECLARE @CodigoUO char(4)
        DECLARE @CodigoUOEntrada char(4)
        DECLARE @DataUltimaAlteracao datetime
        DECLARE @CodigoUsuarioUltimaAlteracao int
        DECLARE @CodigoProjeto as int 

        SET @CodigoUO = '{0}'
        SET @DataUltimaAlteracao = GETDATE()
        SET @CodigoUsuarioUltimaAlteracao = {1}
        SET @CodigoUOEntrada = '{2}'
        SET @CodigoProjeto = {3}

        UPDATE [dbo].[pbh_DemandaUO]
           SET [CodigoUsuarioUltimaAlteracao] = @CodigoUsuarioUltimaAlteracao
              ,[DataUltimaAlteracao] = @DataUltimaAlteracao
              ,[CodigoUO] = @CodigoUOEntrada
         WHERE [CodigoUO] = @CodigoUO
           AND CodigoControleCCG = (SELECT TOP 1 d.NumeroDemanda AS CodigoControleCCG
                                          FROM Demanda d
                                         WHERE d.CodigoProjetoDemanda = @CodigoProjeto)", chave, idUsuarioLogado, spnUO.Text, codigoProjeto);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoSQL + Environment.NewLine +
            cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
            if (retorno.Trim().ToLower() == "ok")
            {
                retorno = string.Empty;
            }
        }
        return retorno;
    }

    private string persisteInclusaoRegistro()
    {
        string retorno = "";
        string comandoSQL = string.Format(@"
        DECLARE @CodigoUO char(4)
        DECLARE @DataInclusao datetime
        DECLARE @CodigoUsuarioInclusao int
        DECLARE @CodigoProjeto as int

        SET @CodigoUO = '{0}'
        SET @DataInclusao = GETDATE()
        SET @CodigoUsuarioInclusao = {1}
        SET @CodigoProjeto = {2}

        IF NOT EXISTS (SELECT 1 
                         FROM [pbh_DemandaUO] 
                        WHERE [CodigoUO] = '{0}')
        BEGIN
           INSERT INTO [pbh_DemandaUO]
           ([CodigoUO]
           ,[CodigoControleCCG]
           ,[DataInclusao]
           ,[CodigoUsuarioInclusao])
            VALUES
            (@CodigoUO
            ,(SELECT TOP 1 d.NumeroDemanda AS CodigoControleCCG FROM Demanda d WHERE d.CodigoProjetoDemanda = @CodigoProjeto)
           ,@DataInclusao
           ,@CodigoUsuarioInclusao)
        END
        ELSE
        BEGIN 
             UPDATE [dbo].[pbh_DemandaUO]
               SET 
                  [CodigoUsuarioInclusao] = @CodigoUsuarioInclusao
                  ,[DataInclusao] = GETDATE()
                  ,[DataExclusao] = NULL
                  ,[CodigoUsuarioExclusao] = NULL
                   ,[CodigoControleCCG] = (SELECT TOP 1 d.NumeroDemanda AS CodigoControleCCG 
                                             FROM Demanda d 
                                            WHERE d.CodigoProjetoDemanda = @CodigoProjeto)
             WHERE [CodigoUO] = '{0}'
        END ", spnUO.Text, idUsuarioLogado, codigoProjeto);

        DataSet ds = cDados.getDataSet(cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoSQL + Environment.NewLine +
            cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(ds))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
            if (retorno.Trim().ToLower() == "ok")
            {
                retorno = string.Empty;
            }
        }
        return retorno;

    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        bool podeEditarExcluir = true;
        if (gvDados.GetRowValues(e.VisibleIndex, "NumeroProtocolo") != null)
        {
            podeEditarExcluir = string.IsNullOrEmpty(gvDados.GetRowValues(e.VisibleIndex, "NumeroProtocolo").ToString());
            if (e.ButtonID == "btnEditar")
            {
                if (podeEditarExcluir)
                {
                    e.Enabled = true;
                    e.Image.Url = "~/imagens/botoes/editarReg02.png";
                }
                else
                {
                    e.Enabled = false;
                    e.Image.ToolTip = "Registros feitos pelo processo de inclusão de demanda não podem ser editados ou excluídos";
                    e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                }
            }

            if (e.ButtonID == "btnExcluir")
            {
                if (podeEditarExcluir)
                {
                    e.Enabled = true;
                    e.Image.Url = "~/imagens/botoes/excluirReg02.png";
                }
                else
                {
                    e.Enabled = false;
                    e.Image.ToolTip = "Registros feitos pelo processo de inclusão de demanda não podem ser editados ou excluídos";
                    e.Image.Url = "~/imagens/botoes/ExcluirRegDes.png";
                }
            }
        }
    }

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

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        carregaGvDados();
    }
}