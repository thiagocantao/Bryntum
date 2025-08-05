using DevExpress.Web;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Projetos_Administracao_frmLinksContrato1 : System.Web.UI.Page
{
    dados cDados;

    int codigoContrato = -1;
    int numeroAditivoContrato = -1;
    int numeroParcelaContrato = -1;
    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;

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
        
        bool ret = int.TryParse(Request.QueryString["CC"] + "", out codigoContrato);
        ret = int.TryParse(Request.QueryString["NAC"] + "", out numeroAditivoContrato);
        ret = int.TryParse(Request.QueryString["NPC"] + "", out numeroParcelaContrato);
        hfProcessoSenar.Set("possuiProcessoSenar", "N");
        hfProcessoSenar.Set("possuiOutroProcessoSenar", "N");

        GridViewDataHyperLinkColumn nomeLink = new GridViewDataHyperLinkColumn();
        nomeLink.Caption = "Documentos vinculados a parcela";
        nomeLink.Settings.AllowSort = DevExpress.Utils.DefaultBoolean.False;
        nomeLink.FieldName = "NumeroAditivoContratoLink";
        nomeLink.PropertiesHyperLinkEdit.TextFormatString = "{0}";
        nomeLink.PropertiesHyperLinkEdit.NavigateUrlFormatString = "{0}";
        nomeLink.PropertiesHyperLinkEdit.TextField = "NumeroAditivoContratoLink";
        nomeLink.PropertiesHyperLinkEdit.EncodeHtml = false;
        nomeLink.PropertiesHyperLinkEdit.Target = "_blank";
        nomeLink.Width = new Unit(90, UnitType.Percentage);

        gvDocumentos.Columns.Add(nomeLink);

        GridViewDataTextColumn data = new GridViewDataTextColumn();
        data.Caption = "Data";
        data.Width = new Unit(10, UnitType.Percentage);
        data.Settings.AllowSort = DevExpress.Utils.DefaultBoolean.False;
        data.FieldName = "DataDocumentoProcesso";
        gvDocumentos.Columns.Add(data);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(this);
        
        if (codigoContrato > 0)
        {
            if (!IsPostBack)
            {
                DataSet dsProcesso = cDados.buscarProcessoVinculadoSenarDocs(codigoContrato);
                if (dsProcesso != null && dsProcesso.Tables.Count > 0 && dsProcesso.Tables[0].Rows.Count > 0)
                {
                    for (int y = 0; y < dsProcesso.Tables[0].Rows.Count; y++)
                    {
                        var NumeroProcessoSenarDocs = dsProcesso.Tables[0].Rows[y]["NumeroProcessoSenarDocs"];
                        var Assunto = dsProcesso.Tables[0].Rows[y]["Assunto"];
                        var Fornecedor = dsProcesso.Tables[0].Rows[y]["Fornecedor"];
                        var UltimaSincronizacao = dsProcesso.Tables[0].Rows[y]["UltimaSincronizacao"];
                        //var DescricaoTipoFolder = dsProcesso.Tables[0].Rows[y]["DescricaoTipoFolder"];
                        var CodigoTipoFolder = dsProcesso.Tables[0].Rows[y]["CodigoTipoFolder"].ToString();           
                    }
                }
                else
                {
                }

                DataSet dsAnexos = buscarAnexosProcessoSenarDocs(codigoContrato, numeroAditivoContrato, numeroParcelaContrato);
                  
                if (dsAnexos != null && dsAnexos.Tables.Count > 0 && dsAnexos.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    DataRow dr = null;
                    dt.Columns.Add("NumeroAditivoContratoLink", System.Type.GetType("System.String"));
                    dt.Columns.Add("DataDocumentoProcesso", System.Type.GetType("System.String"));
                    List<DocumentoProcesso> listDocumento = new List<DocumentoProcesso>();
                    for (int y = 0; y < dsAnexos.Tables[0].Rows.Count; y++)
                    {
                        var nomeDocumento = dsAnexos.Tables[0].Rows[y]["NomeDocumento"];
                        var dataDocumento = dsAnexos.Tables[0].Rows[y]["DataDocumento"];
                        var urlDocumento = dsAnexos.Tables[0].Rows[y]["LinkDocumento"];

                        listDocumento.Add(new DocumentoProcesso { nomeDocumento = nomeDocumento.ToString(), urlDocumento = urlDocumento.ToString(), dataDocumento = DateTime.Parse(dataDocumento.ToString())});
                    }
                    if (listDocumento.Count > 0)
                    {
                        List<DocumentoProcesso> listaOrdenada = listDocumento.OrderBy(o => o.nomeDocumento).ToList();

                        foreach (var doc in listaOrdenada)
                        {
                            dr = dt.NewRow();
                            dr["NumeroAditivoContratoLink"] = "<a href='" + doc.urlDocumento + "'>" + doc.nomeDocumento + "</a>";
                            dr["DataDocumentoProcesso"] = doc.dataDocumento.ToString("dd/MM/yyyy");
                            dt.Rows.Add(dr);
                        }
                    }
                    gvDocumentos.DataSource = dt;
                    gvDocumentos.DataBind();
                }
            }
            gvDocumentos.Settings.ShowFilterRow = false;
            gvDocumentos.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        }

        int largura = 0, altura = 0;
        cDados.getLarguraAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString(), out largura, out altura);
        gvDocumentos.Settings.VerticalScrollableHeight = altura - 275;
        gvDocumentos.DataBind();
    }
    public DataSet buscarAnexosProcessoSenarDocs(int CodigoContrato, int numeroAditivoContrato, int numeroParcela)
    {
        string comandoSQLNovo = string.Format(@"
        DECLARE @RC int
        DECLARE @in_CodigoContrato int
        DECLARE @in_NumeroAditivoContrato smallint
        DECLARE @in_NumeroParcela smallint

        SET @in_CodigoContrato = {0}
        SET @in_NumeroAditivoContrato = {1}
        SET @in_NumeroParcela = {2}

        EXECUTE @RC = [dbo].[p_SENAR_getListaDocumentosParcela] 
           @in_CodigoContrato
          ,@in_NumeroAditivoContrato
          ,@in_NumeroParcela ", CodigoContrato, numeroAditivoContrato, numeroParcela);

        DataSet ds = cDados.getDataSet(comandoSQLNovo);
        return ds;
    }
    protected void hfCarregaDocumentos_CustomCallback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (codigoContrato > 0)
        {
            string parametro = e.Parameter;
            DataTable dt = new DataTable();

            if (parametro.Equals("DESVINCULAR"))
            {
                gvDocumentos.DataSource = dt;
                gvDocumentos.DataBind();
                DataSet dsProcesso = cDados.buscarProcessoVinculadoSenarDocs(codigoContrato);
                if (dsProcesso != null && dsProcesso.Tables.Count > 0 && dsProcesso.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        cDados.desvincularProcesso(codigoContrato, codigoUsuarioResponsavel);
                    }catch(Exception ee)
                    {
                        return;
                    }

                }
                return;
            }
            DataSet ds = cDados.verificaVinculacaoProcesso(codigoContrato, "0", "0");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && bool.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString()) == false)
            {
                hfProcessoSenar.Set("possuiOutroProcessoSenar", "S");
                return;
            }

            dynamic obj = apiSenarDocs();

            DataRow dr = null;
            dt.Columns.Add("NumeroAditivoContratoLink", System.Type.GetType("System.String"));
            dt.Columns.Add("DataDocumentoProcesso", System.Type.GetType("System.String"));
            //dt.AcceptChanges();
            var assunto = "";
            var fornecedor = "";
            foreach (var item in obj)
            {
                var newObject = (IDictionary<string, object>)item;
                if (!newObject.ContainsKey("Documentos"))
                    continue;
                assunto = item.Indice02;
                fornecedor = item.Indice05;
                var documentos = (List<object>)newObject["Documentos"];
                if (documentos.Count > 0)
                {
                    DataSet dsVinculaProcesso = cDados.vincularProcesso(codigoContrato, "0", "0", assunto, fornecedor, codigoUsuarioResponsavel);
                    List<DocumentoProcesso> listDocumento = new List<DocumentoProcesso>();
                    foreach (var doc in documentos)
                    {
                        var newDoc = (IDictionary<string, object>)doc;
                        var nomeDocumento = newDoc["ClassificacaoDocumento"].ToString().Trim() + newDoc["Extensao"].ToString().Trim();
                        var dataCadastro = DateTime.Parse(newDoc["DataCadastroDocumento"].ToString());
                        var urlDocumento = newDoc["URLDocumento"].ToString();

                        listDocumento.Add(new DocumentoProcesso { nomeDocumento = nomeDocumento ,urlDocumento = urlDocumento, dataDocumento = dataCadastro });                      
                    }
                    if(listDocumento.Count > 0)
                    {
                        List<DocumentoProcesso> listaOrdenada = listDocumento.OrderBy(o => o.nomeDocumento).ToList();

                        foreach (var doc in listaOrdenada)
                        {
                            dr = dt.NewRow();
                            dr["NumeroAditivoContratoLink"] = "<a href='" + doc.urlDocumento + "'>" + doc.nomeDocumento + "</a>";
                            dr["DataDocumentoProcesso"] = doc.dataDocumento.ToString("dd/MM/yyyy");
                            dt.Rows.Add(dr);

                            DataSet dsAnexos = cDados.incluirAnexoProcesso(codigoContrato, doc.nomeDocumento, doc.urlDocumento, doc.dataDocumento);
                        }
                    }
                }
                hfProcessoSenar.Set("possuiProcessoSenar", "S");
            }
            gvDocumentos.DataSource = dt;
            gvDocumentos.DataBind();
            hfProcessoSenar.DataBind();
        }
    }

    protected void hfProcessoSenar_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        hfProcessoSenar.Set("possuiProcessoSenar", "N");
        hfProcessoSenar.Set("possuiOutroProcessoSenar", "N");
        hfProcessoSenar.Set("processoDesvinculado", "N");

        dynamic obj = apiSenarDocs();
        foreach (var item in obj)
        {
            var newObject = (IDictionary<string, object>)item;
            if (newObject.ContainsKey("Documentos"))
            {
                hfProcessoSenar.Set("possuiProcessoSenar", "S");
                break;
            }
        }
        DataSet ds = cDados.verificaVinculacaoProcesso(codigoContrato, "0", "0");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && bool.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString()) == false)
        {
            hfProcessoSenar.Set("possuiOutroProcessoSenar", "S");
        }
        string parametro = e.Parameter;
        if (parametro.Equals("DESVINVULADO"))
            hfProcessoSenar.Set("processoDesvinculado", "S");

        hfProcessoSenar.DataBind();
    }

    private dynamic apiSenarDocs()
    {
        var numeroProcesso = "0";
        var tipoProcesso = "0";

        var principalJSON = @"{
    'Acao': 'CONSULTARFOLDER',
    'Cliente': '6d677178415164674c55673d',
    'TipoFolder': "+tipoProcesso+@",
    'Origem': '',
    'Protocolo':'" + numeroProcesso + @"',
    'Indice01': '',
    'Indice02': '',
    'Indice03': '',
    'Indice04': '',
    'Indice05': '',
    'Indice06': '',
    'Indice07': '',
    'Indice08': '',
    'Indice09': '',
    'Indice10': ''
}
";

        DataTable dtParametros = cDados.getParametrosSistema("senardocs_urlapi").Tables[0];
        string senardocs_urlapi= "https://senardocs.senar.org.br/Handles";
        if (dtParametros.Rows.Count > 0)
            senardocs_urlapi = dtParametros.Rows[0].ItemArray[0].ToString();

        var client = new RestClient(senardocs_urlapi+"/CloudDocsAPISenar.ashx?Protocolo=" + numeroProcesso);
        var request = new RestRequest(Method.POST);
        request.RequestFormat = DataFormat.Json;
        request.AddHeader("cache-control", "no-cache");
        request.AddParameter("application/json", principalJSON, RestSharp.ParameterType.RequestBody);

        IRestResponse response = client.Execute(request);
        var streamDados = response.Content;
        var retorno = response.Content;
        retorno = retorno.Replace("\"", "'");
        //retorno = retorno.Substring(1, retorno.Length - 2);
        dynamic obj = JsonConvert.DeserializeObject<List<ExpandoObject>>(retorno);
        return obj;
    }
 
    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "abreDetalhesLinksContrato(-1);", true, true, false, "CtrPrj", "Links do Contrato", this);
    }

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, gvExporter, "CtrPrj");
    }

    protected void gvExporter_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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

   public class DocumentoProcesso
    {
        public string nomeDocumento { get; set; }
        public string urlDocumento { get; set; }
        public DateTime dataDocumento { get; set; }
    }
}