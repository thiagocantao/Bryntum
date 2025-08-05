using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class espacoTrabalho_AnexosCompartilhamento : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int codigoTipoAssociacao;

    string IniciaisTipoAssociacao;
    int IDObjetoAssociado;
    char Origem;


    private int? CodigoPastaDestino;


    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
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

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        IniciaisTipoAssociacao = (Request.QueryString["TA"] != null && Request.QueryString["TA"].ToString() != "") ? Request.QueryString["TA"].ToString() : "";
        IDObjetoAssociado = (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "") ? int.Parse(Request.QueryString["ID"].ToString()) : -1;
        Origem = Request.QueryString["O"].ToString()[0];
        int CodigoPastaSuperior = (Request.QueryString["CPS"] != null && Request.QueryString["CPS"].ToString() != "" ? int.Parse(Request.QueryString["CPS"].ToString()) : -1);
        int CodigoPastaAtual = (Request.QueryString["CPA"] != null && Request.QueryString["CPA"].ToString() != "" ? int.Parse(Request.QueryString["CPA"].ToString()) : -1);
        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
        codigoTipoAssociacao = cDados.getCodigoTipoAssociacao(IniciaisTipoAssociacao);

        if (CodigoPastaAtual != -1)
            CodigoPastaDestino = CodigoPastaAtual;
        // Se CodigoPastaSuperior != -1, o novo arquivo/pasta será colocado abaixo dela (pasta atual) 
        else if (CodigoPastaSuperior != -1)
            CodigoPastaDestino = CodigoPastaSuperior;
        // Se não tem pasta atual e nem pasta superior, o novo arquivo/psata, será colocado abaixo da pasta raiz
        else
            CodigoPastaDestino = null;

        carregaGrid();

        cDados.aplicaEstiloVisual(this);

        gvAnexos.Settings.ShowFilterRow = true;
        gvAnexos.SettingsPager.Mode = DevExpress.Web.GridViewPagerMode.ShowAllRecords;


        ((GridViewDataTextColumn)gvAnexos.Columns["Nome"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
        ((GridViewDataTextColumn)gvAnexos.Columns["NomeUsuario"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;
        ((GridViewDataTextColumn)gvAnexos.Columns["DescricaoAnexo"]).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True;



        defineAlturaTela();
    }

    private void carregaGrid()
    {
        DataSet ds = cDados.getAnexosOpcaoLink(codigoEntidadeUsuarioResponsavel, IDObjetoAssociado, codigoTipoAssociacao);

        gvAnexos.DataSource = ds;
        gvAnexos.DataBind();
    }

    private void defineAlturaTela()
    {
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString(), out largura, out altura);
        gvAnexos.Settings.VerticalScrollableHeight = altura - 330;
    }

    protected void callback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        callback.JSProperties["cp_Msg"] = "";
        callback.JSProperties["cp_Erro"] = "";

        string comandoSQL = "", msg = "", mensagemRetornoBanco = "";

        foreach (object anexo in gvAnexos.GetSelectedFieldValues("CodigoAnexo"))
        {
            comandoSQL += string.Format(@"
            INSERT into {0}.{1}.AnexoAssociacao (CodigoAnexo, CodigoObjetoAssociado, CodigoTipoAssociacao, IndicaLinkCompartilhado, CodigoPastaLink)
                                         VALUES ({2}, {3}, {4}, 'S', {5})", cDados.getDbName(), cDados.getDbOwner(), anexo.ToString(), IDObjetoAssociado, codigoTipoAssociacao, CodigoPastaDestino.HasValue ? CodigoPastaDestino.ToString() : "NULL");
        }
        if (comandoSQL != "")
        {

            comandoSQL = cDados.geraBlocoBeginTran() +
                Environment.NewLine + 
                comandoSQL + 
                Environment.NewLine + 
                cDados.geraBlocoEndTran();

            try
            {
                DataSet dsTemp = cDados.getDataSet(comandoSQL);
                if(cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0]))
                {
                    mensagemRetornoBanco = dsTemp.Tables[0].Rows[0][0].ToString();
                    if(mensagemRetornoBanco == "OK")
                    {
                        callback.JSProperties["cp_Msg"] = "Dados salvos com sucesso!";
                    }
                    else
                    {
                        callback.JSProperties["cp_Erro"] = "Erro ao salvar dados!" + Environment.NewLine + mensagemRetornoBanco;
                    }
                }
            }
            catch(Exception ex)
            {
                mensagemRetornoBanco = ex.Message;
                callback.JSProperties["cp_Erro"] = "Erro ao salvar dados!" + Environment.NewLine + mensagemRetornoBanco;
            }
        }
    }
}