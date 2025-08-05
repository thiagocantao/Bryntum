using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using DevExpress.Utils;
using DevExpress.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class _Projetos_Agil_agil_Links : System.Web.UI.Page
{
    const string PrefixoCodigoReservadoItem = "TFS-";

    private dados cDados;

    private int _codigoEntidade;
    private int _codigoItem;
    private string _tituloItemBacklog;
    private string _codigoReservado;
    protected void Page_Init(object sender, EventArgs e)
    {
        var listaParametrosDados = new OrderedDictionary();

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

        _codigoItem = int.Parse(Request.QueryString["CI"]);
        _codigoEntidade = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cDados.aplicaEstiloVisual(Page);

        CarregarConfiguracoesItem();
        PopularProjetosTfs();
        PopularGridLinks();
    }

    private void PopularProjetosTfs()
    {
        cmbProjetosTfs.DataSource = ObterProjetosTfs();
        cmbProjetosTfs.DataBind();
    }

    private void PopularGridLinks()
    {
        if (!callback.IsCallback)
        {
            gvLinks.DataSource = ObterLinksWorkItems().ToList();
            gvLinks.DataBind();
        }
    }

    private List<ProjectTFS> ObterProjetosTfs()
    {
        var projects = new List<ProjectTFS>();
        var projectsJson = GetStringBodyRequest("project");
        if (!string.IsNullOrWhiteSpace(projectsJson))
        {
            projects = JArray.Parse(projectsJson)
                .Select(o => new ProjectTFS(o.Value<string>("id"), o.Value<string>("name")))
                .ToList();
        }

        return projects;
    }

    private IEnumerable<LinkWorkItem> ObterLinksWorkItems()
    {
        if (!string.IsNullOrWhiteSpace(_codigoReservado) && _codigoReservado.StartsWith(PrefixoCodigoReservadoItem))
        {
            var workItemId = _codigoReservado.Replace(PrefixoCodigoReservadoItem, string.Empty);

            string workItemJson;
            HttpResponseMessage response = GetResponse("workitem", workItemId);
            if (response.IsSuccessStatusCode)
                workItemJson = response.Content.ReadAsStringAsync().Result;
            else
                yield break;

            if (!string.IsNullOrWhiteSpace(workItemJson))
            {
                var workItem = JObject.Parse(workItemJson);
                var workItemUrl = ((JValue)workItem.SelectToken("links.links.html.href")).Value as string;
                var workItemTitle = ((JValue)workItem["fields"]["System.Title"]).Value as string;
                yield return new LinkWorkItem(workItemId, workItemTitle, "Work Item", workItemUrl);

                var links = workItem["links"]["links"].Children().OfType<JProperty>()
                    .Where(l => l.Name.StartsWithInvariantCultureIgnoreCase(PrefixoCodigoReservadoItem));
                foreach (var link in links)
                {
                    char[] splitCharacters = new[] { (char)001 };
                    var nameParts = link.Name.Split(splitCharacters, StringSplitOptions.RemoveEmptyEntries);
                    var id = nameParts[1];
                    var tipo = nameParts[2];
                    var titulo = nameParts[3];
                    var url = (string)((JValue)link.Value["href"]).Value;
                    yield return new LinkWorkItem(id, titulo, tipo, url);
                }
            }
        }
    }

    private string GetStringBodyRequest(string path, string id = null)
    {
        string urlBriskIntegracao = ObtemValorParametroUrlBriskIntegracao();

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("codigo-entidade", _codigoEntidade.ToString());
            var requestUri = string.Format("{0}/{1}/{2}", urlBriskIntegracao, path, id);
            string respose = httpClient.GetStringAsync(requestUri).Result;

            return respose;
        }
    }

    private HttpResponseMessage GetResponse(string path, string id = null)
    {
        string urlBriskIntegracao = ObtemValorParametroUrlBriskIntegracao();
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("codigo-entidade", _codigoEntidade.ToString());
            var requestUri = string.Format("{0}/{1}/{2}", urlBriskIntegracao, path, id);
            return httpClient.GetAsync(requestUri).Result;
        }
    }

    private string PostRequestWithFormData(string path, Dictionary<string, string> formData)
    {
        var formContent = new MultipartFormDataContent();
        foreach (var data in formData)
            formContent.Add(new StringContent(data.Value), data.Key);
        return PostRequet(path, formContent);
    }

    private string PostRequet(string path, HttpContent content)
    {
        string urlBriskIntegracao = ObtemValorParametroUrlBriskIntegracao();

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("codigo-entidade", _codigoEntidade.ToString());

            var requestUri = string.Format("{0}/{1}", urlBriskIntegracao, path);
            var response = httpClient.PostAsync(requestUri, content).Result;
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content.ReadAsStringAsync().Result;

            return string.Empty;
        }
    }

    private string ObtemValorParametroUrlBriskIntegracao()
    {
        const string nomeParametroURLBRISKIntegracao = "URLBRISKIntegracao";
        return ObterValorParametro(nomeParametroURLBRISKIntegracao);
    }

    private string ObtemValorParametroURLClienteIntegracao()
    {
        const string nomeParametroURLBRISKIntegracao = "TFS_URLClienteIntegracao";
        return ObterValorParametro(nomeParametroURLBRISKIntegracao);
    }

    private string ObterValorParametro(string nomeParametroURLBRISKIntegracao)
    {
        var dataSet = cDados.getParametrosSistema(nomeParametroURLBRISKIntegracao);
        var dataRow = dataSet.Tables[0].AsEnumerable().SingleOrDefault();
        var valorParametroURLBRISKIntegracao = dataRow.Field<string>(nomeParametroURLBRISKIntegracao);
        return valorParametroURLBRISKIntegracao;
    }

    private void CarregarConfiguracoesItem()
    {
        var comandoSQL = string.Format("SELECT [TituloItem], [CodigoReservado] FROM [dbo].[Agil_ItemBacklog] AS ib WHERE ib.CodigoItem = {0}", _codigoItem);
        var dataSet = cDados.getDataSet(comandoSQL);
        var dataRow = dataSet.Tables[0].AsEnumerable().SingleOrDefault();
        if (dataRow != null)
        {
            txtTituloItem.Text = _tituloItemBacklog = dataRow.Field<string>("TituloItem");
            _codigoReservado = dataRow.Field<string>("CodigoReservado");
        }
    }

    private void RemoverVinculoWorkItem()
    {
        var comandoSql = string.Format(@"
 UPDATE [Agil_ItemBacklog]
	SET [CodigoReservado] = NULL,
        [IndicaItemIntegracaoExterna] = NULL
  WHERE [CodigoItem] = {0}", _codigoItem);

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    private void VincularWorkItem(string workItemId)
    {
        var codigoReservado = string.Format(@"{0}{1}", PrefixoCodigoReservadoItem, workItemId);

        var comandoSql = string.Format(@"
 UPDATE [Agil_ItemBacklog]
	SET [CodigoReservado] = '{1}',
        [IndicaItemIntegracaoExterna] = 'S'
  WHERE [CodigoItem] = {0}", _codigoItem, codigoReservado);

        int registrosAfetados = 0;
        cDados.execSQL(comandoSql, ref registrosAfetados);
    }

    protected void callback_Callback(object source, CallbackEventArgs e)
    {
        bool sucesso = true;
        string mensagem = string.Empty;

        var parametros = e.Parameter.Split(';');
        var acao = parametros.FirstOrDefault();
        var id = parametros.LastOrDefault();
        try
        {
            switch (acao)
            {
                case "vincular":
                    string mensagemErro;
                    if (ValidarVinculoWorkItem(id, out mensagemErro))
                        VincularWorkItem(id);
                    else
                    {
                        sucesso = false;
                        mensagem = mensagemErro;
                    }
                    break;
                case "criar-e-vincular":
                    CriarWorkItemEAssociar();
                    break;
                case "remover":
                    RemoverVinculoWorkItem();
                    break;
            }
        }
        catch (Exception ex)
        {
            sucesso = false;
            mensagem = ex.Message;
        }
        e.Result = JsonConvert.SerializeObject(new { Sucesso = sucesso, Mensagem = mensagem });
    }

    private bool ValidarVinculoWorkItem(string workItemId, out string mensagemErro)
    {
        if (!VerificarWorkItemNaoAssociadoOutroItemDeTrabalho(workItemId))
        {
            mensagemErro = "Work Item já associado a outro item de trabalho";
            return false;
        }
        if (!VerificaSePossivelObterWorkItem(workItemId))
        {
            mensagemErro = "Não foi possível obter um Work Item com o Id informado. Certifique que o Id está correto.";
            return false;
        }
        mensagemErro = string.Empty;
        return true;
    }

    private bool VerificaSePossivelObterWorkItem(string workItemId)
    {
        HttpResponseMessage response = GetResponse("workitem", workItemId);
        return response.StatusCode == HttpStatusCode.OK;
    }

    private bool VerificarWorkItemNaoAssociadoOutroItemDeTrabalho(string workItemId)
    {
        var comandoSql = string.Format(@"
 SELECT ib.CodigoReservado
   FROM Agil_ItemBacklog AS ib 
  WHERE ib.CodigoReservado LIKE 'TFS-' + '{0}' + '%'
	AND ib.CodigoItem <> {1}", workItemId, _codigoItem);
        var ds = cDados.getDataSet(comandoSql);

        return ds.Tables[0].Rows.Count == 0;
    }

    private void CriarWorkItemEAssociar()
    {
        string responseBody = PostRequestWithFormData("workitem", new Dictionary<string, string>()
        {
            { "projeto", cmbProjetosTfs.Value as string },
            { "workItemTitle", _tituloItemBacklog }
        });
        var workItem = JObject.Parse(responseBody);
        var workItemId = workItem["id"].Value<string>();
        VincularWorkItem(workItemId);
    }

    protected void gvLinks_Load(object sender, EventArgs e)
    {
        var toolbar = gvLinks.Toolbars.SingleOrDefault() as GridViewToolbar;
        toolbar.Items.FindByName("remover-item").ClientEnabled = !string.IsNullOrWhiteSpace(_codigoReservado);
    }

    private class LinkWorkItem
    {
        public LinkWorkItem(string id, string titulo, string tipo, string url)
        {
            Id = id;
            Titulo = titulo;
            Tipo = tipo;
            Url = url;
        }

        public string Id { get; private set; }
        public string Titulo { get; private set; }
        public string Tipo { get; private set; }
        public string Url { get; private set; }
    }

    private class ProjectTFS
    {
        public ProjectTFS(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; private set; }
        public string Name { get; private set; }
    }
}