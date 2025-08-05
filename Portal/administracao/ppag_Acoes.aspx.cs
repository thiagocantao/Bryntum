using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_ppag_Acoes : System.Web.UI.Page
{
    dados cDados;
    private int idUsuarioLogado;
    private int CodigoEntidade;
    private bool podeIncluir = true;

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


        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); //usuario logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());   //entidad logada.

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        gvDados.JSProperties["cp_Erro"] = "";
        gvDados.JSProperties["cp_Sucesso"] = "";
        defineAlturatela();
        HeaderOnTela();
        carregaGrid();
        populaddlProgramas();
        cDados.aplicaEstiloVisual(this);
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ppag_Acoes.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
    }

    public void carregaGrid()
    {
        string comandoSQL = string.Format(@"
          SELECT  ac.[CodigoAcao]
                 ,ac.[CodigoEntidade]
                 ,ac.[CodigoPrograma] 
                 ,pr.[NomePrograma]
                 ,ac.[NumeroAcao]
                 ,ac.[NomeAcao]
                 ,ac.[DataExclusao]
            FROM [pbh_Acao] ac
      INNER JOIN [pbh_Programa] pr ON (ac.CodigoPrograma = pr.CodigoPrograma)
         WHERE ac.[CodigoEntidade] = {0}
           AND ac.[DataExclusao] IS NULL
         ORDER BY pr.NomePrograma, ac.NomeAcao ASC", CodigoEntidade);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ListaPpagAcoes");
    }

    protected void menu_Init(object sender, EventArgs e)
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
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados)", true, true, false, "ListaPpagAcoes", lblTituloTela.Text, this);
    }


    private string getChavePrimaria()
    {
        if (gvDados.GetSelectedFieldValues(gvDados.KeyFieldName).Count > 0)
            return gvDados.GetSelectedFieldValues(gvDados.KeyFieldName)[0].ToString();
        else
            return "-1";
    }

    private void defineAlturatela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int largura = 0;
        int altura = 0;

        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        altura -= 330;

        gvDados.Settings.VerticalScrollableHeight = altura;
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";

        gvDados.JSProperties["cp_Erro"] = "";
        gvDados.JSProperties["cp_Sucesso"] = "";

        if (e.Parameters == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
            gvDados.JSProperties["cp_Sucesso"] = "Dados incluídos com sucesso!";
        }
        if (e.Parameters == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
            gvDados.JSProperties["cp_Sucesso"] = "Dados atualizados com sucesso!";
        }
        if (e.Parameters == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
            gvDados.JSProperties["cp_Sucesso"] = "Dados excluídos com sucesso!";
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            gvDados.JSProperties["cp_Erro"] = "";

        }
        else
        {// alguma coisa deu errado...
            gvDados.JSProperties["cp_Erro"] = mensagemErro_Persistencia;
            gvDados.JSProperties["cp_Sucesso"] = "";
        }
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";
        string codigoAcao = getChavePrimaria();
        string comandoSQL = string.Format(@"
        declare @NomePrograma as varchar(500)
        declare @CodigoAcao as int
        declare @NumeroPrograma as int

        set @CodigoAcao = {0}

        UPDATE [pbh_Acao]
           SET [DataExclusao] = GETDATE()
        WHERE [CodigoAcao] = @CodigoAcao", codigoAcao);

        string cmd = cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoSQL +
            Environment.NewLine +
            cDados.geraBlocoEndTran();

        DataSet ds = cDados.getDataSet(cmd);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
        }
        if (retorno.ToUpper().Trim() == "OK")
        {
            retorno = "";
        }
        return retorno;
    }

    private void populaddlProgramas()
    {
        string comandoSQL = string.Format(@"
        declare @CodigoEntidade int
        declare @CodigoAcaoSelecionada int

        set @CodigoEntidade = {0}


        SELECT [CodigoPrograma]
              ,RIGHT('000' + CONVERT(Varchar,NumeroPrograma),3) + ' - ' + NomePrograma as NomePrograma
          FROM [pbh_Programa] 
         WHERE [CodigoEntidade] = @CodigoEntidade 
           AND [DataExclusao] IS NULL          
         ORDER BY RIGHT('000' + CONVERT(Varchar,NumeroPrograma),3) + ' - ' + NomePrograma ASC", CodigoEntidade);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            ddlPrograma.TextField = "NomePrograma";
            ddlPrograma.ValueField = "CodigoPrograma";
            ddlPrograma.DataSource = ds.Tables[0];
            ddlPrograma.DataBind();
        }

    }

    private string persisteEdicaoRegistro()
    {
        string retorno = "";
        string nomeAcao = txtAcao.Text.Replace("'", "'+char(39)+'");
        string numeroAcao = spnNumero.Text;
        string codigoPrograma = ddlPrograma.Value.ToString();
        string codigoAcao = getChavePrimaria();

        string comandoSQL = string.Format(@"
        declare @CodigoPrograma as int
        declare @NumeroAcao as int
        declare @NomeAcao as varchar(500)
        declare @CodigoAcao as int

        set @CodigoPrograma = {0}
        set @NumeroAcao = {1}
        set @NomeAcao  = '{2}'
        set @CodigoAcao = {3}

        UPDATE [pbh_Acao]
           SET [CodigoPrograma] = @CodigoPrograma
              ,[NumeroAcao] = @NumeroAcao
              ,[NomeAcao] = @NomeAcao
         WHERE CodigoAcao = @CodigoAcao", codigoPrograma, numeroAcao, nomeAcao, codigoAcao);

        string cmd = cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoSQL +
            Environment.NewLine +
            cDados.geraBlocoEndTran();

        DataSet ds = cDados.getDataSet(cmd);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
        }
        if (retorno.ToUpper().Trim() == "OK")
        {
            retorno = "";
        }
        return retorno;
    }

    private string persisteInclusaoRegistro()
    {
        string retorno = "";
        string nomeAcao = txtAcao.Text.Replace("'", "'+char(39)+'");
        string numeroAcao = spnNumero.Text;
        string codigoPrograma = ddlPrograma.Value.ToString();
        string codigoAcao = getChavePrimaria();

        string comandoSQL = string.Format(@"
        declare @CodigoEntidade as int
        declare @CodigoPrograma as int
        declare @NumeroAcao as int
        declare @NomeAcao as varchar(500)

        set @CodigoEntidade  = {0}
        set @CodigoPrograma = {1}
        set @NumeroAcao = {2}
        set @NomeAcao = '{3}'

        INSERT INTO [pbh_Acao]
           ([CodigoEntidade]
           ,[CodigoPrograma]
           ,[NumeroAcao]
           ,[NomeAcao])
        VALUES
           (@CodigoEntidade
           ,@CodigoPrograma
           ,@NumeroAcao
           ,@NomeAcao)
", CodigoEntidade, codigoPrograma, numeroAcao, nomeAcao);

        string cmd = cDados.geraBlocoBeginTran() +
            Environment.NewLine +
            comandoSQL +
            Environment.NewLine +
            cDados.geraBlocoEndTran();

        DataSet ds = cDados.getDataSet(cmd);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString();
        }
        if (retorno.ToUpper().Trim() == "OK")
        {
            retorno = "";
        }
        return retorno;
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
        carregaGrid();
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        object codigoAcao = null;
        if (e.ButtonID == "btnExcluir")
        {
            if (e.CellType == GridViewTableCommandCellType.Data)
            {
                codigoAcao = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "CodigoAcao");
                string comandoSQl = string.Format(@"SELECT TOP 1 CodigoAcao
                                                      FROM  pbh_SubAcao 
                                                     WHERE CodigoAcao = {0} 
                                                       AND DataExclusao is null", codigoAcao);
                DataSet dsTemp = cDados.getDataSet(comandoSQl);
                if (cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0]))
                {

                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";

                }
                else
                {
                    e.Enabled = true;
                    e.Image.Url = "~/imagens/botoes/excluirReg02.PNG";
                }
            }
        }
    }
}