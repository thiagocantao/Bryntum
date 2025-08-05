using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

public partial class administracao_adm_arquivos_visaocronogramaxml : System.Web.UI.Page
{
    dados cDados;
    string pastaCronogramas = System.Configuration.ConfigurationManager.AppSettings["diretorioCronogramas"].ToString();
    string codigoCronogramaProjeto;
    tasques dsProjetoTasques;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        this.Title = cDados.getNomeSistema();
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

        if (Request.QueryString["arquivo"] == null)
            Response.Redirect("adm_arquivos.aspx");

        codigoCronogramaProjeto = Request.QueryString["arquivo"].ToString();
        if (codigoCronogramaProjeto.Length >= 36)
            codigoCronogramaProjeto = codigoCronogramaProjeto.Substring(0, 36);

        buscaInformacoesProjeto();

        defineAlturaTela();

        populaComboVersao();

        if (cbListaCronogramas.Value != null)
        {
            LeArquivoXML();

            populaTreelist();

            populaOutrasInformacoes();
        }
    }

    private void buscaInformacoesProjeto()
    {
        string comandoSQL =string.Format(
            @"select un.NomeUnidadeNegocio, p.codigoprojeto, p.NomeProjeto, cp.DataUltimaAlteracao, 
                     u.NomeUsuario
                from CronogramaProjeto cp  inner join
                     Projeto p on p.CodigoProjeto = cp.CodigoProjeto inner join 
                     usuario u on u.CodigoUsuario = cp.CodigoUsuarioUltimaAlteracao inner join
                     UnidadeNegocio un on un.CodigoUnidadeNegocio = p.CodigoEntidade 
               where CodigoCronogramaProjeto = '{0}'", codigoCronogramaProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            DataRow row = ds.Tables[0].Rows[0];
            lblNomeProjeto.Text = string.Format("{0}; Projeto: {1} - {2}\nAlterado em: {3} por {4}", row["NomeUnidadeNegocio"].ToString(), row["codigoprojeto"].ToString(), row["NomeProjeto"].ToString(), ((DateTime)row["DataUltimaAlteracao"]).ToString("dd/MM/yyyy HH:mm"), row["NomeUsuario"].ToString());
        }
        else
            lblNomeProjeto.Text = "PROJETO NÃO FOI ENCONTRADO NO BANCO DE DADOS";
    }

    private void defineAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        pcCronograma.Height = alturaPrincipal - 160;
       // gv_arquivos.Settings.VerticalScrollableHeight = alturaPrincipal - 160;
    }

    private void populaComboVersao()
    {
        // seleciona todas as versões do cronograma
        DirectoryInfo pasta = new DirectoryInfo(pastaCronogramas);
        FileInfo[] arquivos = pasta.GetFiles(codigoCronogramaProjeto + "*.*");

        DataTable dt = new DataTable();

        dt.Columns.Add("Nome");
        dt.Columns.Add("Modificado");

        foreach (FileInfo file in arquivos)
        {
            DataRow dr = dt.NewRow();
            dr["Nome"] = file.Name;
            dr["Modificado"] = file.LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");

            dt.Rows.Add(dr);
        }
        dt.DefaultView.Sort = "Modificado desc";
        cbListaCronogramas.DataSource = dt;
        cbListaCronogramas.ValueField = "Nome";
        cbListaCronogramas.TextField = "Modificado";
        cbListaCronogramas.DataBind();
    }

    private void LeArquivoXML()
    {
        dsProjetoTasques = new tasques();
        string arquivo = pastaCronogramas + @"\" + cbListaCronogramas.Value.ToString();
        dsProjetoTasques.ReadXml(arquivo);
    }

    private void populaTreelist()
    {
        tlTarefas.DataSource = dsProjetoTasques.Tarefas;
        tlTarefas.KeyFieldName = "codigoTarefa";
        tlTarefas.ParentFieldName = "codigoTarefaSuperior";
        tlTarefas.DataBind();
        tlTarefas.ExpandAll();

    }

    private void populaOutrasInformacoes()
    {
        // outras informações
        List<string> lTemp = new List<string>();
        foreach (DataTable dt in dsProjetoTasques.Tables)
            lTemp.Add(dt.TableName);
        lTemp.Sort();
        cbInformacoes.Items.AddRange(lTemp);

        gvInformacoes.DataSource = null;
        gvInformacoes.Columns.Clear();

        if (cbInformacoes.Text != "")
        {
            gvInformacoes.DataSource = dsProjetoTasques.Tables[cbInformacoes.Text];
            gvInformacoes.AutoGenerateColumns = true;
            gvInformacoes.DataBind();
        }
    }

    protected void btnLerCronograma_Click(object sender, EventArgs e)
    {

    }

    protected void cbInformacoes_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}