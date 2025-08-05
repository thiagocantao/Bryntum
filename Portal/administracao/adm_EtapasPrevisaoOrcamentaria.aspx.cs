using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Service.Services.DataBaseObject.Function;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_adm_EtapasPrevisaoOrcamentaria : BasePageBrisk
{
    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);

        this.TH(this.TS("barraNavegacao"));

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        populaGrid();
        CDados.setaTamanhoMaximoMemo(memoObservacoes, 500, lblContadorMemo);
        gvDados.Settings.VerticalScrollableHeight = TelaAltura - 350;

        
        this.TH(this.TS("adm_EtapasPrevisaoOrcamentaria"));

        if (!IsPostBack)
        {
            ddlMesInicioBloqueio.Items.Insert(0,(new ListEditItem("Nenhum", "0")));
            ddlMesTerminoBloqueio.Items.Insert(0, (new ListEditItem("Nenhum", "0")));

            CDados.excluiNiveisAbaixo(1);
            CDados.insereNivel(1, this);
            Master.geraRastroSite();
            Master.verificaAcaoFavoritos(true, "Etapas Previsão Orçamentária", "EtpPrev", "ENT", -1, Resources.traducao.adicionar_aos_favoritos);
        }
    }


    private void populaGrid()
    {
        gvDados.DataSource = UowApplication.UowService.GetUowService<FOrcGetEtapasPrevisaoOrcamentariaService>().GetListEtapasPrevisaoOrcamentaria(UsuarioLogado.CodigoEntidade).ToDataSet();
        gvDados.DataBind();
    }

    protected void menu_Init(object sender, EventArgs e)
    {        
        CDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "TipoOperacao='Incluir';abreNovoRegistro();", true, true, false, "CadRecCorp", "Etapas Previsão Orçamentária", this);
    }
    
    protected void callbackSalvar_Callback(object source, CallbackEventArgs e)
    {
        string parametro = e.Parameter;
        string mensagemErro = "";
        ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "";
        ((ASPxCallback)source).JSProperties["cp_Erro"] = "";
        
        if (parametro == "Editar")
        {
            ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "Registro atualizado com sucesso!";
            mensagemErro = persisteEdicaoRegistro();
        }
        else if (parametro == "Excluir")
        {
            ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "Registro excluído com sucesso!";
            mensagemErro = persisteExclusaoRegistro();
        }
        else if (parametro == "Igualar")
        {
            ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "Orçamento igualado com sucesso!";
            mensagemErro = persisteIgualarRegistro();
        }
        else if (parametro == "Incluir")
        {
            ((ASPxCallback)source).JSProperties["cp_Sucesso"] = "Orçamento incluído com sucesso!";
            mensagemErro = persisteInclusaoRegistro();
        }
        if (string.IsNullOrEmpty(mensagemErro))
        {
            ((ASPxCallback)source).JSProperties["cp_Erro"] = "";
        }
        else
        {
            ((ASPxCallback)source).JSProperties["cp_Erro"] = mensagemErro;
        }

    }

    private string persisteInclusaoRegistro()
    {
        string retorno = "";

        string descricaoPrevisao = txtPrevisao.Text;
        int anoOrcamento = -1;
        bool ret = int.TryParse(spnAno.Value.ToString(), out anoOrcamento);
        string observacoes = memoObservacoes.Text;
        int mesInicioBloqueio = int.Parse(ddlMesInicioBloqueio.Value == null ? "0" : ddlMesInicioBloqueio.Value.ToString());
        int mesTerminoBloqueio = int.Parse(ddlMesTerminoBloqueio.Value == null ? "0" : ddlMesTerminoBloqueio.Value.ToString());

        var dataInicioElaboracao = ddlDataInicioElaboracao.Value;
        var dataTerminoElaboracao = ddlDataTerminoElaboracao.Value;


        int codigoStatus = int.Parse(ddlStatus.Value.ToString());

        string comandosql = string.Format(@"
        DECLARE @RC int
        DECLARE @in_CodigoPrevisao smallint
        DECLARE @in_DescricaoPrevisao varchar(100)
        DECLARE @in_Observacao varchar(500)
        DECLARE @in_AnoOrcamento smallint
        DECLARE @in_MesInicioBloqueio tinyint
        DECLARE @in_MesTerminoBloqueio tinyint
        DECLARE @in_InicioPeriodoElaboracao datetime
        DECLARE @in_TerminoPeriodoElaboracao datetime
        DECLARE @in_CodigoStatusPrevisaoFluxoCaixa smallint
        DECLARE @in_CodigoEntidade int
        DECLARE @in_CodigoUsuarioLogado int

        SET @in_CodigoPrevisao  = null
        SET @in_DescricaoPrevisao  = '{2}'
        SET @in_Observacao = '{3}'
        SET @in_AnoOrcamento = {4} 
        SET @in_MesInicioBloqueio =  {5}
        SET @in_MesTerminoBloqueio = {6}
        SET @in_InicioPeriodoElaboracao = CONVERT(datetime,'{7}', 103)
        SET @in_TerminoPeriodoElaboracao = CONVERT(datetime,'{8}', 103)
        SET @in_CodigoStatusPrevisaoFluxoCaixa = {9}
        SET @in_CodigoEntidade = {10}
        SET @in_CodigoUsuarioLogado = {11}

        EXECUTE @RC = {0}.{1}.[p_orc_SalvaPrevisaoFluxoCaixa] 
           @in_CodigoPrevisao
          ,@in_DescricaoPrevisao
          ,@in_Observacao
          ,@in_AnoOrcamento
          ,@in_MesInicioBloqueio
          ,@in_MesTerminoBloqueio
          ,@in_InicioPeriodoElaboracao
          ,@in_TerminoPeriodoElaboracao
          ,@in_CodigoStatusPrevisaoFluxoCaixa
          ,@in_CodigoEntidade
          ,@in_CodigoUsuarioLogado ", /*{0}*/CDados.getDbName(), /*{1}*/CDados.getDbOwner(),
/*{2}*/descricaoPrevisao,
/*{3}*/observacoes,
/*{4}*/anoOrcamento,
/*{5}*/mesInicioBloqueio,
/*{6}*/mesTerminoBloqueio,
/*{7}*/dataInicioElaboracao,
/*{8}*/dataTerminoElaboracao,
/*{9}*/codigoStatus,
/*{10}*/UsuarioLogado.CodigoEntidade,
/*{11}*/UsuarioLogado.Id);

        try
        {
            int regAfetados = 0;
            CDados.execSQL(comandosql, ref regAfetados);
        }
        catch(Exception ex)
        {
            retorno = ex.Message;
        }
        return retorno;

    }

    private string persisteIgualarRegistro()
    {
        string retorno = "";

        string comandosql = string.Format(@"
        DECLARE @RC int
        DECLARE @in_CodigoPrevisao smallint
        SET @in_CodigoPrevisao = {0}
       EXECUTE @RC = [dbo].[p_orc_IgualaPrevistoRealizadoOrcamento] @in_CodigoPrevisao ", /*{0}*/getChavePrimaria());
        try
        {
            int regAfetados = 0;
            CDados.execSQL(comandosql, ref regAfetados);
        }
        catch (Exception ex)
        {
            retorno = ex.Message;
        }
        return retorno;
    }

    private string persisteExclusaoRegistro()
    {
        string retorno = "";

        string comandosql = string.Format(@"
        DECLARE @RC int
        DECLARE @in_CodigoPrevisao smallint
        SET @in_CodigoPrevisao = {0}
       EXECUTE @RC = [dbo].[p_orc_ExcluiPrevisaoFluxoCaixa] @in_CodigoPrevisao ", /*{0}*/getChavePrimaria());
        try
        {
            int regAfetados = 0;
            CDados.execSQL(comandosql, ref regAfetados);
        }
        catch (Exception ex)
        {
            retorno = ex.Message;
        }
        return retorno;
    }

    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (gvDados.FocusedRowIndex >= 0)
            return gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    private string persisteEdicaoRegistro()
    {
        string retorno = "";

        string descricaoPrevisao = txtPrevisao.Text;
        int anoOrcamento = -1;
        bool ret = int.TryParse(spnAno.Value.ToString(), out anoOrcamento);
        string observacoes = memoObservacoes.Text;
        int mesInicioBloqueio = int.Parse(ddlMesInicioBloqueio.Value == null ? "0" : ddlMesInicioBloqueio.Value.ToString());
        int mesTerminoBloqueio = int.Parse(ddlMesTerminoBloqueio.Value == null ? "0": ddlMesTerminoBloqueio.Value.ToString());

        var dataInicioElaboracao = ddlDataInicioElaboracao.Value;
        var dataTerminoElaboracao = ddlDataTerminoElaboracao.Value;

        int codigoStatus = int.Parse(ddlStatus.Value.ToString());

        string comandosql = string.Format(@"
        DECLARE @RC int
        DECLARE @in_CodigoPrevisao smallint
        DECLARE @in_DescricaoPrevisao varchar(100)
        DECLARE @in_Observacao varchar(500)
        DECLARE @in_AnoOrcamento smallint
        DECLARE @in_MesInicioBloqueio tinyint
        DECLARE @in_MesTerminoBloqueio tinyint
        DECLARE @in_InicioPeriodoElaboracao datetime
        DECLARE @in_TerminoPeriodoElaboracao datetime
        DECLARE @in_CodigoStatusPrevisaoFluxoCaixa smallint
        DECLARE @in_CodigoEntidade int
        DECLARE @in_CodigoUsuarioLogado int

        SET @in_CodigoPrevisao  = {12}
        SET @in_DescricaoPrevisao  = '{2}'
        SET @in_Observacao = '{3}'
        SET @in_AnoOrcamento = {4} 
        SET @in_MesInicioBloqueio =  {5}
        SET @in_MesTerminoBloqueio = {6}
        SET @in_InicioPeriodoElaboracao = CONVERT(datetime,'{7}', 103)
        SET @in_TerminoPeriodoElaboracao = CONVERT(datetime,'{8}', 103)
        SET @in_CodigoStatusPrevisaoFluxoCaixa = {9}
        SET @in_CodigoEntidade = {10}
        SET @in_CodigoUsuarioLogado = {11}

        EXECUTE @RC = {0}.{1}.[p_orc_SalvaPrevisaoFluxoCaixa] 
           @in_CodigoPrevisao
          ,@in_DescricaoPrevisao
          ,@in_Observacao
          ,@in_AnoOrcamento
          ,@in_MesInicioBloqueio
          ,@in_MesTerminoBloqueio
          ,@in_InicioPeriodoElaboracao
          ,@in_TerminoPeriodoElaboracao
          ,@in_CodigoStatusPrevisaoFluxoCaixa
          ,@in_CodigoEntidade
          ,@in_CodigoUsuarioLogado ", /*{0}*/CDados.getDbName(), /*{1}*/CDados.getDbOwner(),
/*{2}*/descricaoPrevisao,
/*{3}*/observacoes,
/*{4}*/anoOrcamento,
/*{5}*/mesInicioBloqueio == 0? "NULL" : mesInicioBloqueio.ToString(),
/*{6}*/mesTerminoBloqueio == 0? "NULL" : mesTerminoBloqueio.ToString(),
/*{7}*/dataInicioElaboracao,
/*{8}*/dataTerminoElaboracao,
/*{9}*/codigoStatus,
/*{10}*/UsuarioLogado.CodigoEntidade,
/*{11}*/UsuarioLogado.Id,
/*{12}*/getChavePrimaria());

        try
        {
            int regAfetados = 0;
            CDados.execSQL(comandosql, ref regAfetados);
        }
        catch (Exception ex)
        {
            retorno = ex.Message;
        }
        return retorno;

    }

    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {

        if (e.VisibleIndex >= 0)
        {
            string podeExcluir = (gvDados.GetRowValues(e.VisibleIndex, "PodeExcluir") + "").Trim().ToUpper(); 
            string podeIgualarPrevistoRealizado = (gvDados.GetRowValues(e.VisibleIndex, "PodeIgualarPrevistoRealizado") + "").Trim().ToUpper();
            

            if (e.ButtonID.Equals("btnIgualar"))
            {
                if (podeIgualarPrevistoRealizado == "S")
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/igualarDes.png";
                }
            }
            else if (e.ButtonID.Equals("btnExcluir"))
            {
                if (podeExcluir == "S")
                    e.Enabled = true;
                else
                {
                    e.Enabled = false;
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                }
            }

        }
    }

    protected void pnCallbackStatus_Callback(object sender, CallbackEventArgsBase e)
    {               
        if(e.Parameter.Split('|')[0] != null)
        {
            int codStatusAtual = Convert.ToInt32(e.Parameter.Split('|')[0]);

            ddlStatus.DataSource = UowApplication.UowService.GetUowService<FGetStatusPrevisaoFluxoCaixaService>().GetListPrevisaoFluxoCaixa(codStatusAtual).ToDataSet();
            ddlStatus.TextField = "DescricaoStatusPrevisaoFluxoCaixa";
            ddlStatus.ValueField = "CodigoStatusPrevisaoFluxoCaixa";
            ddlStatus.DataBind();
            ddlStatus.ClientEnabled = !(e.Parameter.Split('|')[1] == "Consultar");
        }        
    }

    protected void menu_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        if(e.Item.Image.Url != "~/imagens/botoes/btnDownload.png" && e.Item.Image.Url != "~/imagens/botoes/layout.png")
        {
            CDados.eventoClickMenu((source as ASPxMenu), parameter, gridviewExporter, "PR_PrvOrc");
        }
    }
}