using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DevExpress.Web;

public partial class administracao_adm_arquivos : System.Web.UI.Page
{
    dados cDados;
    string pastaCronogramas = System.Configuration.ConfigurationManager.AppSettings["diretorioCronogramas"].ToString();

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

        defineAlturaTela();
        lblQtdeArquivos.Text = "";
        btnExcluir.Visible = false;
        // se é requisição para baixar o arquivo
        if (Request.QueryString["arq"] != null)
            download();
        else
        {

            // se a pasta de cronogramas não existe, avisa o usuário e oculta os comandos
            if (rgCronograma.Checked && !Directory.Exists(pastaCronogramas))
            {
                pnControles.Visible = false;
                lblAviso.Visible = true;
                lblAviso.Text = "O diretório de cronogramas não existe. O camanho definido no web.config é: " + pastaCronogramas;
            }
            else
            {
                pnControles.Visible = true;
                lblAviso.Visible = false;
            }

            if (IsCallback)
            {
                btnProcurar_Click(btnProcurar, null);
            }
        }
    }

    private void defineAlturaTela()
    {
        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));
        gv_arquivos.Settings.VerticalScrollableHeight = alturaPrincipal - 160;
    }

    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        string arquivos = txtCodigoCronograma.Text.Trim();
        string pasta = pastaCronogramas;
        if (rgTemporarios.Checked)
            pasta = Server.MapPath("~/ArquivosTemporarios");

        loadFolder(pasta, arquivos, false);
    }

    public void loadFolder(String caminho, string arquivoCronograma, bool excluirArquivos)
    {
        DirectoryInfo pasta = new DirectoryInfo(caminho);
        FileInfo[] arquivos = pasta.GetFiles(arquivoCronograma + "*.*");
        lblQtdeArquivos.Text = "Arquivos encontrados: " + arquivos.Length;
        // se esta na pasta de arquivos temporários e encontrou algum, mostra o botão de exclusão
        if (rgTemporarios.Checked && arquivos.Length > 0)
        {
            btnExcluir.Visible = true;

            // se é para exlcuir os arquivos localizados... só pode excluir arquivos da pasta "Arquivos temporarios"
            if (excluirArquivos)
            {
                foreach (FileInfo file in arquivos)
                {
                    file.Delete();
                }
                lblAviso.Visible = true;
                lblAviso.Text = "Os arquivos foram excluídos";
                lblQtdeArquivos.Text = "Arquivos excluídos";
                btnExcluir.Visible = false;
                gv_arquivos.DataSource = null;
                gv_arquivos.DataBind();
                return;
            }
        }
        DataTable dt = new DataTable();

        dt.Columns.Add("Nome");
        dt.Columns.Add("Tamanho");
        dt.Columns.Add("Tipo");
        dt.Columns.Add("Modificado");
        dt.Columns.Add("acao");

        foreach (FileInfo file in arquivos)
        {
            DataRow dr = dt.NewRow();
            dr["Nome"] = file.Name;
            dr["Tamanho"] = Convert.ToString(file.Length / 1024) + " kb";
            dr["Tipo"] = file.Extension;
            dr["Modificado"] = file.LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");
            dr["acao"] = "";

            dt.Rows.Add(dr);
        }

        dt.DefaultView.Sort = "Nome desc";
        gv_arquivos.DataSource = dt;
        gv_arquivos.DataBind();
    }

    private void download()
    {
        string arquivo = Request.QueryString["arq"].ToString();
        arquivo = pastaCronogramas + "\\" + arquivo;
        string nomeCliente = Path.GetFileName(arquivo);

        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=" + nomeCliente);
        // Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(arquivo);
        Response.End();
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        if (!rgTemporarios.Checked)
            return;

        string arquivos = txtCodigoCronograma.Text.Trim();
        string pasta = Server.MapPath("~/ArquivosTemporarios");

        loadFolder(pasta, arquivos, true);
    }

    protected void gv_arquivos_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID == "btnDetalhaProjeto")
        {
            string nomeArquivoXML = gv_arquivos.GetDataRow(e.VisibleIndex)["Nome"].ToString();
            ASPxWebControl.RedirectOnCallback("adm_arquivos_visaocronogramaxml.aspx?arquivo=" + nomeArquivoXML);
        }
    }
}