using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_Agil_configSprint : System.Web.UI.Page
{
    dados cDados;

    private int idProjeto;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    public bool podeIncluir = true;

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
        

        codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        idProjeto = int.Parse((Request.QueryString["CP"] != null) ? Request.QueryString["CP"].ToString() + "" : "-1");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        headerOnTela();
        if (!IsPostBack)
        {
            carregaDadosTela();
        }
        
    }

    private void carregaDadosTela()
    {

        string comandosql = string.Format(@"
        SELECT [CodigoIteracao]
              ,[IndicaItensNaoPlanejados]
              ,[LocalReuniaoDiaria]
              ,[TipoGraficoTaskBoard]
              ,[HorarioInicioReuniaoDiaria]
              ,[HorarioTerminoReuniaoDiaria]
         FROM {0}.{1}.[Agil_Iteracao] WHERE [CodigoProjetoIteracao] = {2}", cDados.getDbName(), cDados.getDbOwner(), idProjeto);
        DataSet ds = cDados.getDataSet(comandosql);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            txtLocal.Text = ds.Tables[0].Rows[0]["LocalReuniaoDiaria"].ToString();//--> Local das Reuniões Diárias
            txtHoraInicioReal.Text = ds.Tables[0].Rows[0]["HorarioInicioReuniaoDiaria"].ToString();//Hora de Início das Reuniões Diárias
            txtHoraTerminoReal.Text = ds.Tables[0].Rows[0]["HorarioTerminoReuniaoDiaria"].ToString();//--> Hora de Término das Reuniões Diárias
            rblTipoGrafico.Value = ds.Tables[0].Rows[0]["TipoGraficoTaskBoard"].ToString();//--> Tipo de Gráfico do Taskboard (BurnDown ou BurnUp)
            ckbItensNaoPlanejados.Value = ds.Tables[0].Rows[0]["IndicaItensNaoPlanejados"].ToString();//--> Itens não planejados influenciam no desempenho? (Sim/Não)
        }
        else
        {
            txtLocal.Text = "";
            txtHoraInicioReal.Text = "";
            txtHoraTerminoReal.Text = "";
            rblTipoGrafico.Value = "";
            ckbItensNaoPlanejados.Value = "";
        }
    }

    private void headerOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/configSprint.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/_Strings.js""></script>"));
        this.TH(this.TS("barraNavegacao", "configSprint", "_Strings"));
    }

    protected void cbSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string mensagemRetorno = "";
        string comandoSQL = string.Format(@"
        BEGIN 
            DECLARE @CodigoProjetoIteracao AS INT
              DECLARE @IndicaItensNaoPlanejados AS CHAR(1)
              DECLARE @LocalReuniaoDiaria AS VARCHAR(50)
              DECLARE @TipoGraficoTaskBoard AS CHAR(1)
              DECLARE @HorarioInicioReuniaoDiaria AS VARCHAR(5)
              DECLARE @HorarioTerminoReuniaoDiaria AS VARCHAR(5)
           
              SET @CodigoProjetoIteracao = {2}
              SET @LocalReuniaoDiaria = '{3}'
              SET @HorarioInicioReuniaoDiaria = '{4}'
              SET @HorarioTerminoReuniaoDiaria = '{5}'
              SET @TipoGraficoTaskBoard = '{6}'             
              SET @IndicaItensNaoPlanejados = '{7}'
        
              UPDATE {0}.{1}.[Agil_Iteracao]
              SET 
               [IndicaItensNaoPlanejados] = @IndicaItensNaoPlanejados
              ,[LocalReuniaoDiaria] = @LocalReuniaoDiaria 
              ,[TipoGraficoTaskBoard] = @TipoGraficoTaskBoard
              ,[HorarioInicioReuniaoDiaria] = @HorarioInicioReuniaoDiaria
              ,[HorarioTerminoReuniaoDiaria] = @HorarioTerminoReuniaoDiaria
              WHERE [CodigoProjetoIteracao] = @CodigoProjetoIteracao
            END", cDados.getDbName(), cDados.getDbOwner(), 
            idProjeto, 
            txtLocal.Text,
            txtHoraInicioReal.Text, 
            txtHoraTerminoReal.Text,
            rblTipoGrafico.Value.ToString(),
            ckbItensNaoPlanejados.Value);

        DataSet dsResultado = cDados.getDataSet(cDados.geraBlocoBeginTran() + comandoSQL + cDados.geraBlocoEndTran());
        if (cDados.DataSetOk(dsResultado) && cDados.DataTableOk(dsResultado.Tables[0]))
        {
            mensagemRetorno = dsResultado.Tables[0].Rows[0][0].ToString();
        }


        ((ASPxCallback)source).JSProperties["cp_MensagemErro"] = (mensagemRetorno.Trim().ToLower() != "ok") ? mensagemRetorno : "";
        ((ASPxCallback)source).JSProperties["cp_MensagemSucesso"] = "registro salvo com sucesso!!";
    }
}