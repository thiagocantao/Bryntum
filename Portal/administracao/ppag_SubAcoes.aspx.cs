using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_ppag_SubAcoes : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            populaddlAcoes(-1);
            populadllPrograma();
        }
        cDados.aplicaEstiloVisual(this);
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ppag_SubAcoes.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
    }

    public void populadllPrograma()
    {
        string comandoSQL = string.Format(@"
        SELECT [CodigoPrograma]
              ,RIGHT('000' + CONVERT(Varchar,NumeroPrograma),3) + ' - ' + NomePrograma as NomePrograma
          FROM [pbh_Programa] 
         WHERE [CodigoEntidade] = {0} 
           AND [DataExclusao] IS NULL
         ORDER BY RIGHT('000' + CONVERT(Varchar,NumeroPrograma),3) + ' - ' + NomePrograma ASC", CodigoEntidade);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            ddlPrograma.TextField = "NomePrograma";
            ddlPrograma.ValueField = "CodigoPrograma";
            ddlPrograma.DataSource = ds;
            ddlPrograma.DataBind();
        }
    }

    public void carregaGrid()
    {
        string comandoSQL = string.Format(@"
          SELECT pro.CodigoPrograma
                ,pro.NomePrograma
                ,sa.[CodigoSubAcao]
                ,sa.[CodigoEntidade]
                ,sa.[CodigoAcao]
                ,a.NomeAcao
                ,sa.[NumeroSubAcao]
                ,sa.[NomeSubAcao]
                ,sa.[DataExclusao]
            FROM [pbh_SubAcao] sa
      INNER JOIN pbh_Acao a ON (a.CodigoAcao = sa.CodigoAcao 
                            AND a.DataExclusao IS NULL 
                            AND a.CodigoEntidade = {0})
      INNER JOIN pbh_Programa pro ON (pro.CodigoPrograma = a.CodigoPrograma 
                                  AND pro.CodigoEntidade = {0} 
                                  AND pro.DataExclusao IS NULL)
           WHERE sa.DataExclusao IS NULL 
             AND sa.CodigoEntidade = {0} 
             AND pro.CodigoEntidade  = {0} 
             AND pro.DataExclusao is null
        ORDER BY a.NomeAcao, sa.NomeSubAcao
", CodigoEntidade);
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

        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "LsPpagSubAcao");
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
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, " btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados)", true, true, false, "LsPpagSubAcao", lblTituloTela.Text, this);
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

        altura -= 320;

        gvDados.Settings.VerticalScrollableHeight = altura;
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";

        ((ASPxGridView)sender).JSProperties["cp_Erro"] = "";
        ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "";

        if (e.Parameters == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "Dados incluídos com sucesso!";
        }
        if (e.Parameters == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "Dados atualizados com sucesso!";
        }
        if (e.Parameters == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "Dados excluídos com sucesso!";
        }
        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            ((ASPxGridView)sender).JSProperties["cp_Erro"] = "";

        }
        else
        {// alguma coisa deu errado...
            ((ASPxGridView)sender).JSProperties["cp_Erro"] = mensagemErro_Persistencia;
            ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "";
        }
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";
        string codigoSubAcao = getChavePrimaria();
        string comandoSQL = string.Format(@"
        declare @CodigoSubAcao as int
        set @CodigoSubAcao = {0}
        UPDATE [pbh_SubAcao]
           SET [DataExclusao] = GETDATE()
        WHERE [CodigoSubAcao] = @CodigoSubAcao", codigoSubAcao);

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

    private void populaddlAcoes(int CodigoProgramaSelecionado)
    {
        string comandoSQL = string.Format(@"
        declare @CodigoEntidade int
        declare @CodigoPrograma int

        set @CodigoEntidade = {0}
        set @CodigoPrograma = {1}
       IF(@CodigoPrograma <> -1)
       BEGIN
        SELECT RIGHT('000' + CONVERT(Varchar,p.NumeroPrograma),3) + '.' + CONVERT(Varchar,a.NumeroAcao) +  ' - ' + a.Nomeacao as NomeAcao,
               [CodigoAcao]
          FROM [pbh_Acao]  AS a INNER JOIN 
               pbh_programa AS p ON (p.CodigoPrograma = a.CodigoPrograma AND p.DataExclusao IS NULL)
         WHERE a.[CodigoEntidade] = @CodigoEntidade 
           AND a.[DataExclusao] IS NULL
           AND (a.[CodigoPrograma] = @CodigoPrograma)
         ORDER BY NomeAcao ASC
      END
      ELSE
        BEGIN
        SELECT RIGHT('000' + CONVERT(Varchar,p.NumeroPrograma),3) + '.' + CONVERT(Varchar,a.NumeroAcao) +  ' - ' + a.Nomeacao as NomeAcao,
               [CodigoAcao]
          FROM [pbh_Acao] AS a INNER JOIN 
               pbh_programa AS p ON (p.CodigoPrograma = a.CodigoPrograma AND p.DataExclusao IS NULL)
         WHERE a.[CodigoEntidade] = @CodigoEntidade 
           AND a.[DataExclusao] IS NULL
         ORDER BY NomeAcao ASC
        END", CodigoEntidade, CodigoProgramaSelecionado);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            ddlAcao.TextField = "NomeAcao";
            ddlAcao.ValueField = "CodigoAcao";
            ddlAcao.DataSource = ds.Tables[0];
            ddlAcao.DataBind();

            if(ds.Tables[0].Rows.Count > 0)
            {
                ddlAcao.SelectedIndex = 0;
            }
            else
            {
                ddlAcao.SelectedIndex = -1;
            }
        }

    }

    private string persisteEdicaoRegistro()
    {
        string retorno = "";
        string nomeSubAcao = txtSubAcao.Text.Replace("'", "'+char(39)+'");
        string numeroSubAcao = spnNumero.Text;
        string codigoAcao = ddlAcao.Value.ToString();
        string codigoSubAcao = getChavePrimaria();

        string comandoSQL = string.Format(@"
        declare @CodigoAcao as int
        declare @NumeroSubAcao as int
        declare @NomeSubAcao as varchar(500)
        DECLARE @CodigoSubAcao as int

        SET @CodigoAcao = {0} 
        SET @NumeroSubAcao = {1}
        SET @NomeSubAcao = '{2}'
        set @CodigoSubAcao = {3}

        UPDATE [pbh_SubAcao]
           SET [CodigoAcao] = @CodigoAcao
              ,[NumeroSubAcao] = @NumeroSubAcao
              ,[NomeSubAcao] = @NomeSubAcao
         WHERE CodigoSubAcao = @CodigoSubAcao", codigoAcao, numeroSubAcao, nomeSubAcao, codigoSubAcao);

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
        string nomeSubAcao = txtSubAcao.Text.Replace("'", "'+char(39)+'");
        string numeroSubAcao = spnNumero.Text;
        string codigoAcao = ddlAcao.Value.ToString();


        string comandoSQL = string.Format(@"
        declare @CodigoEntidade as int
        declare @CodigoAcao as int
        declare @NumeroSubAcao as int
        declare @NomeSubAcao as varchar(500)

        SET @CodigoEntidade = {0}
        SET @CodigoAcao = {1} 
        SET @NumeroSubAcao = {2}
        SET @NomeSubAcao = '{3}'

        INSERT INTO [pbh_SubAcao]
                   ([CodigoEntidade]
                   ,[CodigoAcao]
                   ,[NumeroSubAcao]
                   ,[NomeSubAcao])
             VALUES
                   (@CodigoEntidade
                   ,@CodigoAcao
                   ,@NumeroSubAcao
                   ,@NomeSubAcao)", CodigoEntidade, codigoAcao, numeroSubAcao, nomeSubAcao);

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

    protected void pncb_acao_Callback(object sender, CallbackEventArgsBase e)
    {
        int codigo = -1;
        if(int.TryParse(e.Parameter, out codigo) == true)
        {
            populaddlAcoes(codigo);
        }        
    }
}