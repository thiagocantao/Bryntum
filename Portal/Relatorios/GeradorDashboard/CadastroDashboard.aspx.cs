using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Relatorios_GeradorDashboard_CadastroDashboard :BasePageBrisk
{
    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);

        VerificarAuth();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        carregaGrid();
        CDados.aplicaEstiloVisual(this.Page);
        gvDados.Settings.VerticalScrollableHeight = (TelaAltura - 325);
    }


    private void carregaGrid()
    {
        string comandoSQL = string.Format(@"
        SELECT d.IDDashboard, 
               d.TituloDashboard, 
               d.Descricao, 
               d.IniciaisControle, 
               d.TipoAssociacao
          FROM Dashboard d
    ORDER BY 2 ASC");

        DataSet ds = CDados.getDataSet(comandoSQL);
        gvDados.DataSource = ds;
        gvDados.DataBind();

    }

   

    protected void menu_Init(object sender, EventArgs e)
    {
        CDados.setaDefinicoesBotoesInserirExportar((sender as DevExpress.Web.ASPxMenu), true, "TipoOperacao = 'Incluir';limpaCamposFormulario();", true, true, false, "GerDash", "Geração de Dashboard WEB", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {

    }

    private string getChavePrimaria() // retorna a primary key da tabela
    {
        string chave = "";

        try
        {
            chave = gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        }
        catch (Exception ex)
        {
            chave = "-1";
        }
        return chave;
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        //CUSTOMCALLBACK
        ((DevExpress.Web.ASPxGridView)sender).JSProperties["cpErro"] = "";
        ((DevExpress.Web.ASPxGridView)sender).JSProperties["cpSucesso"] = "";
        ((DevExpress.Web.ASPxGridView)sender).JSProperties["cpGuidRetorno"] = "";
        bool indicaInclusao = false;

        string comandoSql = "";
        string mensagemErro = "";

        string[] arrayParametros = e.Parameters.Split('|');
        if (arrayParametros[0] == "Salvar" && arrayParametros[1] == "Incluir")
        {
            indicaInclusao = true;
            comandoSql = string.Format(@"
           DECLARE @IDDashboard as uniqueidentifier

           DECLARE @TituloDashboard as varchar(150)
           DECLARE @Descricao as varchar(2000)
           DECLARE @IniciaisControle as varchar(25)
           DECLARE @TipoAssociacao as varchar(2)

           SET @IDDashboard = '{0}'
           SET @TituloDashboard = '{1}'
           SET @Descricao = '{2}'
           SET @IniciaisControle = '{3}'
           SET @TipoAssociacao = '{4}'


INSERT INTO [dbo].[Dashboard]
           ([IDDashboard]
           ,[TituloDashboard]
           ,[Descricao]
           ,[IniciaisControle]
           ,[TipoAssociacao])
     VALUES
           (@IDDashboard
           ,@TituloDashboard
           ,@Descricao
           ,@IniciaisControle
           ,@TipoAssociacao)

     SELECT @IDDashboard ",
           /*{0}*/Guid.NewGuid(),
           /*{1}*/txtTituloDashboard.Text,
           /*{2}*/memoDescricao.Text,
           /*{3}*/txtIniciaisControle.Text,
           /*{4}*/comboTipoAssociacao.Value);
            ((DevExpress.Web.ASPxGridView)sender).JSProperties["cpSucesso"] = "Dashboard incluído com sucesso!";
        }
        else if (arrayParametros[0] == "Salvar" && arrayParametros[1] == "Editar")
        {
            comandoSql = string.Format(@"
           DECLARE @IDDashboard as uniqueidentifier

           DECLARE @TituloDashboard as varchar(150)
           DECLARE @Descricao as varchar(2000)
           DECLARE @IniciaisControle as varchar(25)
           DECLARE @TipoAssociacao as varchar(2)

           SET @IDDashboard = '{0}'
           SET @TituloDashboard = '{1}'
           SET @Descricao = '{2}'
           SET @IniciaisControle = '{3}'
           SET @TipoAssociacao = '{4}'


            UPDATE [dbo].[Dashboard]
            SET [TituloDashboard] = @TituloDashboard
            ,[Descricao] = @Descricao
            ,[IniciaisControle] = @IniciaisControle
            ,[TipoAssociacao] = @TipoAssociacao
            WHERE [IDDashboard] = @IDDashboard 
",
           /*{0}*/getChavePrimaria(),
           /*{1}*/txtTituloDashboard.Text,
           /*{2}*/memoDescricao.Text,
           /*{3}*/txtIniciaisControle.Text,
           /*{4}*/comboTipoAssociacao.Value);
            ((DevExpress.Web.ASPxGridView)sender).JSProperties["cpSucesso"] = "Dashboard alterado com sucesso!";
        }
        else if (arrayParametros[0] == "Excluir")
        {
            comandoSql = string.Format(@"
            DECLARE @IDDashboard as uniqueidentifier
            SET @IDDashboard = '{0}'
            DELETE FROM [Dashboard] WHERE [IDDashboard] = @IDDashboard ", arrayParametros[1]);
            ((DevExpress.Web.ASPxGridView)sender).JSProperties["cpSucesso"] = "Dashboard excluído com sucesso!";
        }
        int regAfetados = 0;
        try
        {
            if (indicaInclusao == true)
            {
                DataSet ds = CDados.getDataSet(comandoSql);
                if (CDados.DataSetOk(ds) && CDados.DataTableOk(ds.Tables[0]))
                {
                    ((DevExpress.Web.ASPxGridView)sender).JSProperties["cpGuidRetorno"] = ds.Tables[0].Rows[0][0];
                }

            }
            else
            {
                CDados.execSQL(comandoSql, ref regAfetados);
            }
        }
        catch (Exception ex)
        {
            mensagemErro = ex.Message;
            ((DevExpress.Web.ASPxGridView)sender).JSProperties["cpErro"] = "ERRO: " + mensagemErro;
        }

    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {   
        string tipoAssociacao = ((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "TipoAssociacao").ToString();
        if (e.DataColumn.FieldName == "TipoAssociacao")
        {
            if (tipoAssociacao == "PR")
            {
                e.Cell.Text = "Detalhes de Projeto";
            }
            else if (tipoAssociacao == "IN")
            {
                e.Cell.Text = "Detalhes de Indicador";
            }
            else if (tipoAssociacao == "RD")
            {
                e.Cell.Text = "Menu de Relatórios Dinâmicos";
            }
            else if (tipoAssociacao == "RE")
            {
                e.Cell.Text = "Reuniões";
            }
            else if (tipoAssociacao == "OB")
            {
                e.Cell.Text = "Detalhes de Objetivo Estratégico";
            }
        }
    }
}