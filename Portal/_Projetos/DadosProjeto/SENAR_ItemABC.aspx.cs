using DevExpress.Web;
using System;
using System.Data;
using System.Drawing;
using System.Web;


public partial class _Projetos_DadosProjeto_SENAR_ItemABC : System.Web.UI.Page
{
    dados cDados;

    private string resolucaoCliente = "";
    private int idProjeto;
    private int codigoUsuario;
    private int codigoEntidade;
    bool podeIncluir;

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

        codigoUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        idProjeto = int.Parse(Request.QueryString["idProjeto"].ToString());

        string iniciaisTela = "PR_ManterItmABC";
        podeIncluir = cDados.VerificaPermissaoUsuario(codigoUsuario, codigoEntidade, iniciaisTela);
        cDados.VerificaAcessoTelaSemMaster(this, codigoUsuario, codigoEntidade, idProjeto, "null", "PR", 0, "null", iniciaisTela);

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (!IsPostBack)
        {
            defineAlturaTela();
        }

        populaGrid();
        cDados.aplicaEstiloVisual(Page);

        ((GridViewDataSpinEditColumn)gvDados.Columns["ValorUnitarioItem"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
        ((GridViewDataSpinEditColumn)gvDados.Columns["ValorUnitarioItem"]).Settings.ShowFilterRowMenu = DevExpress.Utils.DefaultBoolean.True;
    }

    private void populaGrid()
    {
        string comandoSQL = string.Format(@"
        SELECT CodigoItem, 
               DescricaoItem, 
               ValorUnitarioItem,
               TipoItem, 
               case TipoItem when 'A' then 'Ação' else 'Outros' end as TipoItemDescritivo, 
               IndicaItemAtivo, 
               IniciaisItem
          FROM SENAR_ItemABC order by DescricaoItem asc");
        DataSet ds = cDados.getDataSet(comandoSQL);

        gvDados.DataSource = ds.Tables[0];
        gvDados.DataBind();
    }

    private void defineAlturaTela()
    {
        int largura = 0;
        int altura = 0;
        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        gvDados.Settings.VerticalScrollableHeight = (altura - 295);
    }

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/SENAR_ItemABC.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "SENAR_ItemABC", "_Strings"));
    }



    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "PR_ManterItmABC");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "PR_ManterItmABC", "Itens ABC", this);
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
        if (e.RowType == GridViewRowType.Data)
        {
            string controlado = gvDados.GetRowValues(e.VisibleIndex, "IniciaisItem").ToString();
            if (!string.IsNullOrEmpty(controlado) && !string.IsNullOrWhiteSpace(controlado))
            {
                e.BrickStyle.BackColor = Color.FromArgb(221, 255, 204);
            }
        }
    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        bool isEnabled = false;
        string controlado = "";
        if (gvDados.GetRowValues(e.VisibleIndex, "IniciaisItem") != null && gvDados.GetRowValues(e.VisibleIndex, "IniciaisItem").ToString() != "")
        {
            controlado = gvDados.GetRowValues(e.VisibleIndex, "IniciaisItem").ToString();
        }
        if (string.IsNullOrEmpty(controlado) || string.IsNullOrWhiteSpace(controlado))
        {
            isEnabled = true;
        }

        if (e.ButtonID.Equals("btnExcluirCustom"))
        {
            
            if (isEnabled)
            {
                e.Enabled = true;
                e.Text = "Excluir";
                e.Image.Url = "~/imagens/botoes/ExcluirReg02.png";
            }
            else
            {
                e.Enabled = false;
                e.Text = "Item controlado pelo sistema";
                e.Image.Url = "~/imagens/botoes/ExcluirRegDes.png";
            }
        }
        if (e.ButtonID == "btnEditarCustom")
        {

            if (isEnabled)
            {
                e.Enabled = true;
                e.Text = "Editar";
                e.Image.Url = "~/imagens/botoes/editarReg02.png";
            }
            else
            {
                e.Enabled = false;
                e.Text = "Item controlado pelo sistema";
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
    }

    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";
        string cpErroAux = "";
        ((ASPxGridView)sender).JSProperties["cp_Erro"] = "";
        ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "";
        ((ASPxGridView)sender).JSProperties["cp_Alerta"] = "";
        
        if (e.Parameters == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "item incluído com sucesso.";
            if(mensagemErro_Persistencia != string.Empty)
            {
                cpErroAux = "Erro ao incluir o registro:";
            }
        }
        if (e.Parameters == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "item atualizado com sucesso.";
            if (mensagemErro_Persistencia != string.Empty)
            {
                cpErroAux = "Erro ao alterar o registro:";
            }
        }
        if (e.Parameters == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
            ((ASPxGridView)sender).JSProperties["cp_Sucesso"] = "item excluído com sucesso.";
            if (mensagemErro_Persistencia != string.Empty)
            {
                cpErroAux = "Erro ao exluir o registro:";
            }
        }
        if (mensagemErro_Persistencia != string.Empty) // não deu erro durante o processo de persistência
        {
            ((ASPxGridView)sender).JSProperties["cp_Erro"] = cpErroAux + Environment.NewLine + mensagemErro_Persistencia;
        }
    }

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";
        string chave = getChavePrimaria();
        string comandoSQL = string.Format(@"
         DELETE FROM [SENAR_ItemABC]
          WHERE CodigoItem = {0}", chave);

        DataSet ds = cDados.getDataSet(
            cDados.geraBlocoBeginTran()
            + Environment.NewLine
            + comandoSQL
            + Environment.NewLine
            + cDados.geraBlocoEndTran());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString().Trim();
            if (retorno.ToLower() == "ok")
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
        string declareSets = string.Format(@"
           declare @DescricaoItem as varchar(150)
           declare @ValorUnitarioItem as decimal(25,4)
           declare @TipoItem as char(1)
           declare @IndicaItemAtivo as char(1)
           declare @IniciaisItem as varchar(25)

           set @DescricaoItem = '{0}'
           set @ValorUnitarioItem = {1}
           set @TipoItem = '{2}'
           set @IndicaItemAtivo =  '{3}'
           set @IniciaisItem = null", txtDescricao.Text.Replace("'", "'+char(39)+'"),
           spnValor.Value.ToString().Replace(".","").Replace(",","."),
           rbTipo.Value.ToString(),
           ckbAtivo.Value.ToString());
        
        string comandoSQL = string.Format(@"
        {0}        
        UPDATE [SENAR_ItemABC]
           SET [DescricaoItem] = @DescricaoItem,
               [ValorUnitarioItem] = @ValorUnitarioItem,
               [TipoItem] = @TipoItem,
               [IndicaItemAtivo] = @IndicaItemAtivo
          WHERE CodigoItem = {1}", declareSets, chave);

        DataSet ds = cDados.getDataSet(
            cDados.geraBlocoBeginTran()
            + Environment.NewLine
            + comandoSQL
            + Environment.NewLine
            + cDados.geraBlocoEndTran());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString().Trim();
            if (retorno.ToLower() == "ok")
            {
                retorno = string.Empty;
            }
        }
        return retorno;
    }

    private string persisteInclusaoRegistro()
    {
        string retorno = "";
        string declareSets = string.Format(@"
           declare @DescricaoItem as varchar(150)
           declare @ValorUnitarioItem as decimal(25,4)
           declare @TipoItem as char(1)
           declare @IndicaItemAtivo as char(1)
           declare @IniciaisItem as varchar(25)

           set @DescricaoItem = '{0}'
           set @ValorUnitarioItem = '{1}'
           set @TipoItem = '{2}'
           set @IndicaItemAtivo =  '{3}'
           set @IniciaisItem = null", txtDescricao.Text.Replace("'", "'+char(39)+'"),
           spnValor.Value.ToString().Replace(".", "").Replace(",", "."),
           rbTipo.Value.ToString(),
           ckbAtivo.Value.ToString());


        string comandoSQL = string.Format(@"
        {0}        
        INSERT INTO [SENAR_ItemABC]
           ([DescricaoItem]
           ,[ValorUnitarioItem]
           ,[TipoItem]
           ,[IndicaItemAtivo]
           ,[IniciaisItem])
        VALUES
           (@DescricaoItem,
            @ValorUnitarioItem,
            @TipoItem,
            @IndicaItemAtivo,
            @IniciaisItem)", declareSets);

        DataSet ds = cDados.getDataSet(
            cDados.geraBlocoBeginTran()
            + Environment.NewLine
            + comandoSQL
            + Environment.NewLine
            + cDados.geraBlocoEndTran());

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            retorno = ds.Tables[0].Rows[0][0].ToString().Trim();
            if (retorno.ToLower() == "ok")
            {
                retorno = string.Empty;
            }
        }
        return retorno;
    }

    protected void gvDados_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        populaGrid();
    }

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string controlado = e.GetValue("IniciaisItem").ToString();

            if (!string.IsNullOrEmpty(controlado) && !string.IsNullOrWhiteSpace(controlado))
            {
                e.Row.BackColor = Color.FromName("#DDFFCC");
                e.Row.ForeColor = Color.Black;
            }
        }
    }
}