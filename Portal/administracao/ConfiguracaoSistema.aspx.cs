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
using System.Collections.Specialized;
using System.Drawing;
using DevExpress.Web;
using DevExpress.XtraPrinting;

public partial class administracao_ConfiguracaoSistema : System.Web.UI.Page
{
    dados cDados;
    private bool podeAdministrar = false;
    
    private string nomeTabelaDb = "ParametroConfiguracaoSistema";
    private string whereUpdateDelete;
    private string resolucaoCliente = "";

    private int idUsuarioLogado;
    private int CodigoEntidade;
    private int alturaPrincipal = 0;
    public bool temPermissaoDeAdministrarParametros = false;



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
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/ConfiguracaoSistema.js"" ></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/barraNavegacao.js"" ></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link rel=""stylesheet"" media=""screen"" type=""text/css"" href=""../estilos/colorpicker.css"" />"));
        //Header.Controls.Add(cDados.getLiteral(@"<script src=""../scripts/jquery.ColorPicker.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script src=""../scripts/colorpicker.js""></script>"));
        this.TH(this.TS("barraNavegacao", "ConfiguracaoSistema"));


        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        //Alterado por Ericsson em 17/04/2010 para trazer a entidade do usuário logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "NULL", "EN", 0, "NULL", "SI_CnsPmt");

            //gvDados.SettingsPager.
        }
        cDados.aplicaEstiloVisual(Page);
        temPermissaoDeAdministrarParametros = cDados.VerificaAcessoEmAlgumObjeto(idUsuarioLogado, CodigoEntidade, "EN", "EN_AdmPar");
        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;

        //dbo.f_GetPermissoesUsuario(CodigoObjeto, IniciaisObjeto, CodigoObjetoPai, CodigoEntidade, CodigoUsuarioInteressado, IniciaisTipoPermissao, CodigoUsuarioOtorgante, ListaPerfis, HerdaPermissao)
        podeAdministrar = cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "SI_AltPmt");
        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        populaGrid();

        gvDados.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Left;
        ((GridViewDataTextColumn)gvDados.Columns["Parametro"]).Visible = temPermissaoDeAdministrarParametros;
        gvDados.Settings.ShowFooter = temPermissaoDeAdministrarParametros;

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private void populaGrid()
    {
        //Alterado por Ericsson em 17/04/2010. Não estava passando o código do entidade para filtrar os riscos padrões.
        string where = "";

        DataSet ds = cDados.getConfiguracoesEntidade(CodigoEntidade, where, temPermissaoDeAdministrarParametros);
        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    private ListDictionary getDadosFormulario()
    {
        string tipoDado = "";

        if (gvDados.FocusedRowIndex >= 0)
            tipoDado = gvDados.GetRowValues(gvDados.FocusedRowIndex, "TipoDadoParametro").ToString();
        else
            tipoDado = "";

        // Lê as informações disponíveis no formulário
        ListDictionary oDadosFormulario = new ListDictionary();

        switch (tipoDado)
        {
            case "TXT": oDadosFormulario.Add("Valor", txtValorTXT.Text);
                break;
            case "INT": oDadosFormulario.Add("Valor", txtValorINT.Text);
                break;
            case "MES": oDadosFormulario.Add("Valor", ddlValorMES.Value.ToString());
                break;
            case "BOL": oDadosFormulario.Add("Valor", rbValorBOL.Value.ToString());
                break;
            case "LOG": oDadosFormulario.Add("Valor", rbValorBOL.Value.ToString());
                break;
            case "COR": oDadosFormulario.Add("Valor", ddlCOR.Text);
                break;
            case "NUM": oDadosFormulario.Add("Valor", txtValorINT.Text);
                break;
        }
        return oDadosFormulario;
    }

    #region Provavelmente não será preciso alterar nada aqui.

    // retorna a primary key da tabela
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    // Método responsável pela Atualização do registro
    private string persisteEdicaoRegistro()
    {
        try
        {
            // Lê as informações disponíveis no formulário
            ListDictionary oDadosFormulario = getDadosFormulario();
                        
            cDados.update(nomeTabelaDb, oDadosFormulario, whereUpdateDelete);
            populaGrid();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }   
    #endregion


    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditar")
        {
            if (!podeAdministrar)
            {
                e.Enabled = false;
                e.Text = "Edição não disponível";
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
    }

    public string getTratamentoCores(string conteudoCelula)
    {
        string htmlCor = @"";
        if (conteudoCelula.Length == 7 && conteudoCelula.IndexOf("#") == 0)
        {
            htmlCor = string.Format(@"<div style=""width: 35px; height: 15px; background-color: {0};""></div>", conteudoCelula);
        }

        return htmlCor;
    }
    protected void gvDados_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ConfigGerSis");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "ConfigGerSis", "Configurações Gerais", this);
    }

    #endregion
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

    protected void gvDados_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string indicaControladoSistema = e.GetValue("IndicaControladoSistema").ToString();

            if (indicaControladoSistema == "S")
            {
                e.Row.ForeColor = Color.FromName("#619340");
            }
        }
    }
}
