using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_Administracao_frmItensContrato : System.Web.UI.Page
{
    dados cDados;
    int codigoContrato = -1;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
        try
        {
            bool ret = int.TryParse((Request.QueryString["CC"] + ""), out codigoContrato);
            codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        }
        catch
        {
            Response.RedirectLocation = cDados.getPathSistema() + "erros/erroInatividade.aspx";
            Response.End();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this.Page);
        DataSet ds1 = cDados.getContratosAquisicoes(codigoEntidadeUsuarioResponsavel, codigoUsuarioResponsavel, " and cont.CodigoContrato = " + codigoContrato);
        if (cDados.DataSetOk(ds1) && cDados.DataTableOk(ds1.Tables[0]))
        {
            txtNumeroContrato.Text = ds1.Tables[0].Rows[0]["NumeroContrato"].ToString();
            txtTipoContrato.Text = ds1.Tables[0].Rows[0]["DescricaoTipoContrato"].ToString();
            txtStatusContrato.Text = (ds1.Tables[0].Rows[0]["StatusContrato"].ToString().ToUpper().Trim() == "A") ? "Ativo" : "Inativo";

            txtInicioVigencia.Text = string.IsNullOrWhiteSpace(ds1.Tables[0].Rows[0]["DataInicio"].ToString()) ? "" : ((DateTime)ds1.Tables[0].Rows[0]["DataInicio"]).ToString("d");
            txtTerminoVigencia.Text = string.IsNullOrWhiteSpace(ds1.Tables[0].Rows[0]["DataTermino"].ToString()) ? "" : ((DateTime)ds1.Tables[0].Rows[0]["DataTermino"]).ToString("d");

        }

        carregaGrid();

        gvItens.Settings.ShowFilterRow = false;
    }

    private void carregaGrid()
    {
        string comandosql = string.Format(@"
        SELECT [CodigoItemMedicaoContrato]
              ,[DescricaoItem]
              ,[UnidadeMedida]
	          ,[QuantidadePrevistaTotal]
              ,[ValorUnitarioItem]
              ,[ValorTotalPrevisto]
              ,[DataExclusaoItem]
          FROM [dbo].[ItemMedicaoContrato]
         WHERE CodigoContrato = {0} and DataExclusaoItem is null", codigoContrato);

        DataSet ds2 = cDados.getDataSet(comandosql);
        if (cDados.DataSetOk(ds2))
        {
            gvItens.DataSource = ds2.Tables[0];
            gvItens.DataBind();
        }
    }

    private string getChavePrimaria()
    {
        if (gvItens.FocusedRowIndex >= 0)
            return gvItens.GetRowValues(gvItens.FocusedRowIndex, gvItens.KeyFieldName).ToString();
        else
            return "-1";
    }
    protected void callbackTela_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        ((ASPxCallback)source).JSProperties["cpSucesso"] = "";
        ((ASPxCallback)source).JSProperties["cpErro"] = "";

        string declare = string.Format(@"
            DECLARE @CodigoItemMedicaoContrato as int
            DECLARE @CodigoContrato as int
            DECLARE @DescricaoItem as varchar(255)
            DECLARE @QuantidadePrevistaTotal as decimal(25,6)
            DECLARE @UnidadeMedida as varchar(50)
            DECLARE @ValorUnitarioItem as decimal(25,2)
            DECLARE @ValorTotalPrevisto as decimal(25,2)
            DECLARE @DataExclusaoItem as datetime
            DECLARE @TipoOperacao as varchar(7)");

        StringBuilder sets = new StringBuilder();
        //Editar   Excluir  Editar
        sets.AppendLine("SET @CodigoItemMedicaoContrato = " + getChavePrimaria());
        sets.AppendLine("SET @CodigoContrato = " + codigoContrato);
        sets.AppendLine(string.Format("SET @DescricaoItem = '{0}'", txtDescricao.Text));

        sets.AppendLine("SET @QuantidadePrevistaTotal = " + Decimal.Parse(spnQuantidade.Value.ToString()).ToString().Replace(",", "."));
        sets.AppendLine(string.Format("SET @UnidadeMedida = '{0}'", txtUnidadeMedida.Text));
        sets.AppendLine("SET @ValorUnitarioItem = " + Decimal.Parse(spnValorUnitario.Value.ToString()).ToString().Replace(",", "."));
        sets.AppendLine("SET @DataExclusaoItem = GETDATE()");
        sets.AppendLine("SET @ValorTotalPrevisto = " + Decimal.Parse(spnValorTotal.Value.ToString()).ToString().Replace(",", "."));
        sets.AppendLine(string.Format("SET @TipoOperacao = '{0}'", hfGeral.Get("TipoOperacao").ToString()));

        string comandoSQL = string.Format(@"
       {0}

       {1}

         IF (@TipoOperacao = 'Incluir')
         BEGIN
             IF NOT EXISTS(SELECT 1 FROM ItemMedicaoContrato WHERE CodigoContrato = @CodigoContrato AND  DescricaoItem = @DescricaoItem)
             BEGIN
                   INSERT INTO [dbo].[ItemMedicaoContrato]([CodigoContrato] ,[DescricaoItem] ,[QuantidadePrevistaTotal] ,[UnidadeMedida],[ValorUnitarioItem],[ValorTotalPrevisto])
                                                    VALUES(@CodigoContrato  ,@DescricaoItem  ,@QuantidadePrevistaTotal  ,@UnidadeMedida ,@ValorUnitarioItem ,@ValorTotalPrevisto)
               END
         END
        IF (@TipoOperacao = 'Editar')
        BEGIN
              UPDATE [dbo].[ItemMedicaoContrato]
              SET [DescricaoItem]           = @DescricaoItem
                 ,[QuantidadePrevistaTotal] = @QuantidadePrevistaTotal
                 ,[UnidadeMedida]           = @UnidadeMedida
                 ,[ValorUnitarioItem]       = @ValorUnitarioItem
                 ,[ValorTotalPrevisto]      = @ValorTotalPrevisto
              WHERE CodigoItemMedicaoContrato = @CodigoItemMedicaoContrato
        END
        
        IF(@TipoOperacao = 'Excluir')
        BEGIN
              UPDATE [dbo].[ItemMedicaoContrato]
              SET DataExclusaoItem = @DataExclusaoItem
              WHERE CodigoItemMedicaoContrato = @CodigoItemMedicaoContrato
        END ", declare, sets);

        try
        {
            int registrosAfetados = 0;
            bool ret = cDados.execSQL(comandoSQL, ref registrosAfetados);
            if(ret == true)
            {
               if(registrosAfetados == 0)
                {
                    ((ASPxCallback)source).JSProperties["cpErro"] = "Nenhum registro foi alterado";
                }
                else
                {
                    ((ASPxCallback)source).JSProperties["cpSucesso"] = "Operação executada com sucesso!";
                }
            }
            else
            {
                ((ASPxCallback)source).JSProperties["cpErro"] = "Erro!";
            }
        }
        catch(Exception ex)
        {
            ((ASPxCallback)source).JSProperties["cpErro"] = ex.Message;
        }
  
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        bool readOnly1 = ((Request.QueryString["RO"] + "") == "S");

        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), (readOnly1 == false), "limpaCamposPopup()", (readOnly1 == false), true, false, "ItemContra", "Itens Contrato", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ItensContrato");
    }

    protected void gvItens_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        carregaGrid();
    }

    protected void gvItens_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if ("btnEditar" == e.ButtonID)
            {
                if(Request.QueryString["RO"] + "" == "S")
                {
                    e.Image.Url = "~/imagens/botoes/pFormulario.png";
                }
                else
                {
                    e.Image.Url = "~/imagens/botoes/editarReg02.PNG";
                }
            }
            if("btnExcluir" == e.ButtonID)
            {
                if (Request.QueryString["RO"] + "" == "S")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
                else
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                }
            }
        }
    }
}