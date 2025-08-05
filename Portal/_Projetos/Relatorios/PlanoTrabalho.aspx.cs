using Cdis.Brisk.Application.Applications.Relatorio;
using Cdis.Brisk.Application.Applications.UnidadeNegocio;
using Cdis.Brisk.Domain.Generic;
using Cdis.Brisk.Infra.Core.Extensions;
using DevExpress.Web;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Web;

public partial class _Projetos_Relatorios_PlanoTrabalho : BasePageBrisk
{
   public static string mensagemErro = "";
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
        if(!IsPostBack)
        {
            LoadCombobox();
            CDados.aplicaEstiloVisual(this.Page);
        }        
    }

    private void LoadCombobox()
    {
        string comandoSQL = string.Format(@"
        SELECT CodigoUnidadeNegocio, NomeUnidadeNegocio FROM UnidadeNegocio
         WHERE CodigoEntidade = {0} 
           AND IndicaUnidadeNegocioAtiva = 'S'
           AND DataExclusao IS NULL", UsuarioLogado.CodigoEntidade);

        DataSet ds = CDados.getDataSet(comandoSQL);

        ddlUnidade.DataSource = ds;//UowApplication.GetUowApplication<UnidadeNegocioApplication>().GetListUnidadeNegocioItemTodosDataTransfer(UsuarioLogado.Id, UsuarioLogado.CodigoEntidade).ToDataSet();
        ddlUnidade.TextField = "NomeUnidadeNegocio";
        ddlUnidade.ValueField = "CodigoUnidadeNegocio";
        ddlUnidade.DataBind();
        ddlUnidade.Items.Insert(0, new ListEditItem("Todas", 0));

        ddlCarteira.DataSource = UowApplication.GetUowApplication<CarteiraApplication>().GetListCarteiraDataTransfer(UsuarioLogado.CodigoEntidade).ToDataSet();
        ddlCarteira.TextField = "NomeCarteira";
        ddlCarteira.ValueField = "CodigoCarteira";
        ddlCarteira.DataBind();

        if (!IsPostBack)
        {
            ddlUnidade.SelectedIndex = 0;
            ddlCarteira.SelectedIndex = 0;
        }
    }
    
    protected void painelCallbackLoading_Callback(object sender, CallbackEventArgsBase e)
    {
        Session["exportStream"] = null;
        ((ASPxCallback)sender).JSProperties["cp_erro"] = "";
        try
        {

            int? codigoUnidade = Convert.ToInt32(ddlUnidade.SelectedItem.Value) == 0 ? (int?) null: Convert.ToInt32(ddlUnidade.SelectedItem.Value);

            int? codigoCarteira = Convert.ToInt32(ddlCarteira.SelectedItem.Value) == 0 ? (int?)null : Convert.ToInt32(ddlCarteira.SelectedItem.Value);

            string nomeUnidade = ddlUnidade.SelectedItem.Text.Trim();
            string nomeCarteira = ddlCarteira.SelectedItem.Text.Trim();
          

            string comandoSQL = string.Format(@"SELECT NomeUnidadeNegocio FROM UnidadeNegocio WHERE CodigoEntidade = {0} AND CodigoUnidadeNegocio = {1}", UsuarioLogado.CodigoEntidade, UsuarioLogado.CodigoEntidade);
            DataSet ds = CDados.getDataSet(comandoSQL);
            string nomeEntidade = ds.Tables[0].Rows[0]["NomeUnidadeNegocio"].ToString();

            byte[] byteArray = UowApplication.GetUowApplication<RelatorioPlanoTrabalhoApplication>().GetByteArrayPdfStreamRelatorioPlanoTrabalhoNew(UsuarioLogado.CodigoEntidade, UsuarioLogado.Id, codigoUnidade, codigoCarteira, nomeUnidade, nomeCarteira, nomeEntidade);
            MemoryStream ms = new MemoryStream(byteArray);
            Stream s = ms;
            if (byteArray == null)
            {
                throw new Exception("Não ha dados para serem exibidos no relatório com os filtros informados.");
            }
            Session["exportStream"] = s;
        }
        catch (Exception exc)
        {
            ResultRequestDomain result = new ResultRequestDomain(exc);
            ((ASPxCallback)sender).JSProperties["cp_erro"] = result.Message;

        }
    }
}