using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_ppag_Programas : System.Web.UI.Page
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
        cDados.aplicaEstiloVisual(this.Page);
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ppag_Programas.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/_Strings.js""></script>"));
    }

    public void carregaGrid()
    {
        string comandoSQL = string.Format(@"
        SELECT [CodigoPrograma]
              ,[CodigoEntidade]
              ,[NumeroPrograma]
              ,[NomePrograma]
              ,[DataExclusao]
          FROM [pbh_Programa] 
         WHERE [CodigoEntidade] = {0} 
           AND [DataExclusao] IS NULL
         ORDER BY NomePrograma ASC", CodigoEntidade);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
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

        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ListaPpagProgramas");
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
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "btnSalvar1.SetVisible(true);onClickBarraNavegacao('Incluir', gvDados, pcDados);", true, true, false, "ListaPpagProgramas", lblTituloTela.Text, this);
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

        altura -= 325;

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
        string codigoPrograma = getChavePrimaria();
        string comandoSQL = string.Format(@"
        declare @NomePrograma as varchar(500)
        declare @CodigoPrograma as int
        declare @NumeroPrograma as int

        set @CodigoPrograma = {0}

        UPDATE [pbh_Programa]
           SET [DataExclusao] = GETDATE()
        WHERE [CodigoPrograma] = @CodigoPrograma", codigoPrograma);

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

    private string persisteEdicaoRegistro()
    {
        string retorno = "";
        string nomePrograma = txtPrograma.Text.Replace("'", "'+char(39)+'");
        string numeroPrograma = spnNumero.Text;
        string codigoPrograma = getChavePrimaria();

        string comandoSQL = string.Format(@"
        declare @NomePrograma as varchar(500)
        declare @CodigoPrograma as int
        declare @NumeroPrograma as int

        set @NumeroPrograma = {0}
        set @NomePrograma = '{1}'        
        set @CodigoPrograma = {2}

        UPDATE [pbh_Programa]
           SET [NomePrograma] = @NomePrograma,
               [NumeroPrograma] = @NumeroPrograma
        WHERE [CodigoPrograma] = @CodigoPrograma", numeroPrograma, nomePrograma, codigoPrograma);

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
        string nomePrograma = txtPrograma.Text.Replace("'", "'+char(39)+'");
        string numeroPrograma = spnNumero.Text;

        string comandoSQL = string.Format(@"
        DECLARE @CodigoEntidade as int
        DECLARE @NumeroPrograma as int
        DECLARE @NomePrograma as varchar(500)

        SET @CodigoEntidade = {0}
        SET @NumeroPrograma = {1}
        SET @NomePrograma = '{2}'

        INSERT INTO [pbh_Programa]
           ([CodigoEntidade]
           ,[NumeroPrograma]
           ,[NomePrograma])
        VALUES
           (@CodigoEntidade
           ,@NumeroPrograma
           ,@NomePrograma)", CodigoEntidade, numeroPrograma, nomePrograma);

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
        object codigoPrograma = null;
        if (e.ButtonID == "btnExcluir")
        {
            if (e.CellType == GridViewTableCommandCellType.Data)
            {
                codigoPrograma = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "CodigoPrograma");
                string comandoSQl = string.Format(@"SELECT TOP 1 CodigoPrograma 
                                                      FROM  pbh_Acao 
                                                     WHERE codigoprograma = {0} 
                                                       AND DataExclusao is null", codigoPrograma);
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