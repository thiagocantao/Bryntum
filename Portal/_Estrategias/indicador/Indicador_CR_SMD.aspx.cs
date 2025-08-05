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
using System.Collections.Specialized;

public partial class _Estrategias_indicador_Indicador_CR_SMD : System.Web.UI.Page
{
    dados cDados;
    private string whereUpdateDelete;
    private int idUsuarioLogado;
    private int CodigoEntidade;
    private int alturaPrincipal = 0;
    public bool podeIncluir = false;

   
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

        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "EN_SmdIndCR");
        }

        if (cDados.VerificaPermissaoUsuario(idUsuarioLogado, CodigoEntidade, "EN_SmdIndCR"))
        {
            podeIncluir = true;
        }
        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (!IsPostBack)
        {
            gridCR.JSProperties["cp_msg"] = "";
            gridCR.JSProperties["cp_status"] = "";
            cDados.aplicaEstiloVisual(Page);
            defineAlturaTela();            
            gridCR.Columns[0].Visible = false;
        }

        populaGrid();

       
       
            
        
        populagridCR(txtIndicador.Text != "" ? int.Parse(getChavePrimaria()) : -1);

        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;       

        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        //gridCR.Settings.ShowFilterRow = false;
        gridCR.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;

        
    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/Indicador_CR_SMD.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<title>Relacionamento de CR's com Indicador</title>"));
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        int altura = 0;
        int largura = 0;
        bool retorno = cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);

        alturaPrincipal = altura;
        gvDados.Settings.VerticalScrollableHeight = alturaPrincipal - 250;
        gvDados.Width = new Unit((largura - 10) + "px");
    }

   
    #endregion

    private void populaGrid()
    {
        string where = ""; // "AND iu.IndicaUnidadeCriadoraIndicador = 'S'";        


        DataSet ds = cDados.getIndicadores(CodigoEntidade, idUsuarioLogado, "N", where);

        if (cDados.DataSetOk(ds))
        {
            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }

    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {

        if (e.ButtonID == "btnEditar")
        {
            if (podeIncluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeIncluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
    }
       
    #region BANCO DE DADOS.

    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private string getChavePrimariaCR()
    {
        if ( gridCR.FocusedRowIndex >= 0)
            return gridCR.GetRowValues(gridCR.FocusedRowIndex, gridCR.KeyFieldName).ToString();
        else
            return "";
    }

    #endregion

    #region gridCR

    private void mostraBotaoInsercaoGridDetail()
    {
        string CaptionGrid =
            @"  <table style='width:100%'>
                    <tr><td style='width:30px'>
                        <img src='../../imagens/botoes/novoReg.png' alt='Novo CR' onclick='gridCR.AddNewRow();' style='cursor: pointer; border: 0px solid #CDCDCD'/>
                    </td>
                    <td>Descrições</td>
                    </tr>
                </table>";

        gridCR.SettingsText.Title = CaptionGrid;
    }

    public void populagridCR(int CodigoIndicador)
    {
       string comandoSQL = string.Format(
     @"SELECT * 
                FROM {0}.{1}.smd_indicadorcr
               WHERE CodigoIndicador = {2} 
            order by CR", cDados.getDbName(), cDados.getDbOwner(), CodigoIndicador);


       DataSet ds = cDados.getDataSet(comandoSQL);
        gridCR.DataSource = ds.Tables[0];
        gridCR.DataBind();
    }

    protected void gridCR_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int CodigoIndicador = int.Parse(getChavePrimaria());
        int ID = int.Parse(getChavePrimariaCR());
        string msg = "";

        int regAfetados = 0;
        try
        {
            string comandoSQL = string.Format(@"
                    DELETE FROM {0}.{1}.smd_indicadorcr
                    WHERE ID = {2}
                    ", cDados.getDbName(), cDados.getDbOwner(), ID);
            cDados.execSQL(comandoSQL, ref regAfetados);
            msg = "Registro excluído com sucesso";
            ((ASPxGridView)sender).JSProperties["cp_status"] = "ok";
        }
        catch (Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cp_status"] = "erro";
            msg = ex.Message;
        }

 
        e.Cancel = true;
        gridCR.CancelEdit();
        populagridCR(CodigoIndicador);
        ((ASPxGridView)sender).JSProperties["cp_msg"] = msg;
        return;
    }

    protected void gridCR_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string msg = "";

        int CodigoIndicador = int.Parse(getChavePrimaria());
        string CR = e.NewValues[0].ToString();

        int regAfetados = 0;
        try
        {
            string comandoSQL = string.Format(@"
                    INSERT INTO {0}.{1}.smd_indicadorcr(CodigoIndicador,CR)
                    VALUES({2}, '{3}')
                    ", cDados.getDbName(), cDados.getDbOwner(), CodigoIndicador, CR);
            cDados.execSQL(comandoSQL, ref regAfetados);
            msg = "Registro incluído com sucesso";
            ((ASPxGridView)sender).JSProperties["cp_status"] = "ok";
        }
        catch (Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cp_status"] = "erro";
            if (ex.Message.Contains("Não é possível inserir uma linha de chave duplicada no objeto"))
                msg = "CR já incluído para este indicador";
            else
                msg = ex.Message;
        }


        e.Cancel = true;
        gridCR.CancelEdit();
        populagridCR(CodigoIndicador);
        ((ASPxGridView)sender).JSProperties["cp_msg"] = msg;
        return;
    }

    protected void gridCR_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string msg = "";

        int CodigoIndicador = int.Parse(getChavePrimaria());
        int ID = int.Parse(getChavePrimariaCR());
        string CR = e.NewValues[0].ToString();


        int regAfetados = 0;
        try
        {
            string comandoSQL = string.Format(@"
                    UPDATE {0}.{1}.smd_indicadorcr
                    SET CR = '{3}'
                    WHERE ID = {2} 
                    ", cDados.getDbName(), cDados.getDbOwner(), ID, CR);
            cDados.execSQL(comandoSQL, ref regAfetados);
            msg = "Registro alterado com sucesso";
            ((ASPxGridView)sender).JSProperties["cp_status"] = "ok";

        }
        catch (Exception ex)
        {
            ((ASPxGridView)sender).JSProperties["cp_status"] = "erro";
            if (ex.Message.Contains("Não é possível inserir uma linha de chave duplicada no objeto"))
                msg = "CR já incluído para este indicador";
            else
                msg = ex.Message;
        }

 

        e.Cancel = true;
        gridCR.CancelEdit();
        populagridCR(CodigoIndicador);
        ((ASPxGridView)sender).JSProperties["cp_msg"] = msg;
        return;
        
    }

   
    protected void gridCR_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.IndexOf("Incluir") >= 0)
        {
            // no modo de inclusão da grid Master, a grid detail não pode ter nenhuma ação habilitada
            gridCR.Columns[0].Visible = true;
            populagridCR(-1);
        }
        else if (e.Parameters.IndexOf("Editar") >= 0)
        {
            //mostraBotaoInsercaoGridDetail();
            gridCR.Columns[0].Visible = true;
            populagridCR(-1);
        }
        else
        {
            // retira o botão de inserçao da grid detail
            gridCR.SettingsText.Title = "&nbsp;";
            // esconde a coluna de ações da grid detail
            gridCR.Columns[0].Visible = false;
        }

        if (e.Parameters.IndexOf("Limpar") >= 0)
        {
            // no modo de inclusão da grid Master, a grid detail não pode ter nenhuma ação habilitada
            gridCR.Columns[0].Visible = true;
            populagridCR(-1);
        }
        else
        {        
            // se o codigo da grid Master veio no parametro, vamos popular a grid detail a partir dela
            int posDelimitadorCodigo = e.Parameters.IndexOf("_");
            if (posDelimitadorCodigo >= 0)
            {
                int CodigoIndicador = int.Parse(e.Parameters.Substring(posDelimitadorCodigo + 1));
                populagridCR(CodigoIndicador);
            }

        }

    }

    #endregion

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "IndiCRSMD");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, false, "", lblTituloTela.Text, this);
    }

    protected void menu_ItemClick1(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "IndiCRSMD1");
    }

    protected void menu_Init1(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "gridCR.AddNewRow();", true, false, false, "IndiCRSMD1", lblTituloTela.Text, this);
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




}
