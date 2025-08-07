using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Data;
using System.Web.Script.Serialization;
using System.Web;
using Cdis.Brisk.Application.Applications.Cronograma;
using Cdis.Brisk.DataTransfer.Cronograma;
using Cdis.Brisk.Infra.Core.Extensions;
using Cdis.Brisk.Presentation.Portal.Gantt;
using Cdis.Brisk.Service.Services.Usuario;
using CDIS;

public partial class Gantt_Default : BriskGanttPageBase
{
    protected int idProjeto;
    protected string langCode;
    protected object jsonCol;
    protected string baseUrlEAP;
    protected string jsonInfoCronograma;
    public string jsonTraducao;
    public string jsonRecursosCorporativos;
    public string jsonComboLinhaBase;
    public string jsonLinhaBase;
    public string numLinhaBase;


    protected void Page_Load(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);
        
        VerificarAuth();
        idProjeto = Convert.ToInt32(Request.QueryString["IDProjeto"] == null ? "0" : Request.QueryString["IDProjeto"].ToString());             
        langCode = GetLangPage();
        baseUrlEAP = UowApplication.GetUowApplication<CronogramaProjetoApplication>().GetInfoEapDataTransfer(idProjeto, UsuarioLogado.CodigoEntidade, UsuarioLogado.Id).BaseUrl;

        //todo -> Essa variável é utilizada dentro do Tasques para validação interna. 
        int controleLocal = Math.Abs(CDados.ObtemCodigoHash(((UsuarioLogado.Id * idProjeto * UsuarioLogado.CodigoEntidade) + (UsuarioLogado.CodigoEntidade - UsuarioLogado.Id)) + "CDIS"));

        InfoCronogramaDataTransfer infoCronograma = UowApplication.GetUowApplication<CronogramaProjetoApplication>().GetInfoCronogramaDataTransfer(UsuarioLogado, idProjeto, typeof(Resources.traducao), controleLocal);        
        //todo -> migração cdados
        //infoCronograma.LinkTasques = CDados.getLinkPortalDesktop(Request.Url, UsuarioLogado.CodigoEntidade, UsuarioLogado.Id, idProjeto, "../../../../");
        jsonInfoCronograma = infoCronograma.ToJson();                       
        lblInformacao.Text = infoCronograma.MensagemBloqueio;
        btnAbrirCronoBloqueado.Text = Resources.traducao.sim;
        
        List<string> listTraducaoItem = new List<string>()
        {
            "RecursosHumanos_expandir_todos",
            "RecursosHumanos_contrair_todos",
            "aumentar_zoom",
            "diminuir_zoom",
            "tela_cheia",
            "mapaEstrategico_exportar_para_pdf",
            "desbloquear_cronograma",
            "editar_cronograma",
            "editar_eap",
            "visualizar_eap",
            "linha_de_base",
            "selecionar",
            "caminho_critico",
            "sim",
            "nao",
            "Cronograma_gantt_detalhes_da_tarefa",
            "Primeiro_selecione_a_tarefa_que_deseja_visualizar",
            "versao",
            "descri__o",
            "data_solicita__o_",
            "solicitante",
            "data_aprov__reprov__",
            "aprov__reprov__por_",
            "informa__es_da_linha_de_base_selecionada",
            "Visualizar_infor_da_linha_de_base",
            "carregando___"
        };
        
        numLinhaBase = "-1";
        var listLinhaBase = UowApplication.GetUowApplication<CronogramaProjetoApplication>().GetListNumLinhaBase(idProjeto);
        jsonLinhaBase = listLinhaBase.ToJson().Replace('\\'.ToString() + '"'.ToString(), '\\'.ToString() + '\\'.ToString() + '"');

        jsonComboLinhaBase =
                listLinhaBase.Select(p=> 
                    new {
                        value = p.NumVersao.ToString(),
                        text = p.NumVersao == -1 ? Resources.traducao.vers_o_atual : Resources.traducao.linha_de_base + " " + p.NumLinhaBase.ToString() + " - " + p.Situacao
                    }
                ).ToJson();

        DataSet dsRecursos = CDados.getRecursosCorporativosProjeto(idProjeto.ToString(), UsuarioLogado.CodigoEntidade);
        jsonRecursosCorporativos = "[]";
        if (dsRecursos != null && dsRecursos.Tables.Count > 0)
        {
            var dtRecursos = dsRecursos.Tables[0];
            var rows = dtRecursos.Rows.Cast<DataRow>()
                .Select(r => dtRecursos.Columns.Cast<DataColumn>()
                    .ToDictionary(c => c.ColumnName, c => r[c]));

            jsonRecursosCorporativos = new JavaScriptSerializer()
                .Serialize(rows)
                .Replace("\\\"", "\\\\\"");
        }
        jsonTraducao = Cdis.Brisk.Infra.Core.Util.ResourceUtil.GetListResourceItem(typeof(Resources.traducao), listTraducaoItem).ToJson();
        

        //todo -> migração cdados
        CDados.aplicaEstiloVisual(this.Page, false);
    }


    protected void callbackAtualizaTela_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        var x = e.Parameter;
        //fazer as mudanças do desbloqueio do cronograma.
        //pasando como parametro o codigoEAP
        string comandoSQL = "";
        comandoSQL = string.Format(@"
            --Desbloquear Cronograma.
            EXEC dbo.[p_crono_UndoCheckoutEdicaoEAP] @in_IdEdicaoEAP = '{0}'",  e.Parameter);
        //System.Diagnostics.Debug.WriteLine(comandoSQL);
        int regAfetados = 0;
        CDados.execSQL(comandoSQL, ref regAfetados);
    }
}
