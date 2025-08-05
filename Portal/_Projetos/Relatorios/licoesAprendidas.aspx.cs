//Revisado
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.IO;
using System.Web.Hosting;
using DevExpress.XtraPrinting;
using DevExpress.Web;

public partial class _Projetos_Relatorios_licoesAprendidas : System.Web.UI.Page
{
    string resolucaoCliente = "";
    dados cDados;
    int idUsuarioLogado = 0;
    int CodigoEntidade = 0;
    public bool exportaOLAPTodosFormatos = false;

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

        //Get dado do usuario logado, e do qual entidad ele pertenece.        
        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());

        if (!IsPostBack)
        {
            cDados.VerificaAcessoTela(this, idUsuarioLogado, CodigoEntidade, CodigoEntidade, "null", "EN", 0, "null", "EN_PrjRelLicApr");
        }

        this.Title = cDados.getNomeSistema();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        if (!IsPostBack)
            cDados.aplicaEstiloVisual(Page);
        if (!IsCallback)
            pnCallback.HideContentOnCallback = false;
        if (!IsPostBack && !IsCallback)
        {
            DataSet dsTemp = cDados.getParametrosSistema("exportaOLAPTodosFormatos");
            if ((cDados.DataSetOk(dsTemp) && cDados.DataTableOk(dsTemp.Tables[0])) && dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() + "" != "")
                exportaOLAPTodosFormatos = (dsTemp.Tables[0].Rows[0]["exportaOLAPTodosFormatos"].ToString() == "S");
            //populaOpcoesExportacao();
            hfGeral.Set("tipoArquivo", "XLS");
        }

        // monta a clausula where que será utilizada nos eventos de Atualização e Exclusão
        // whereUpdateDelete = gvDados.KeyFieldName + " = " + getChavePrimaria();

        resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
        defineAlturaTela(resolucaoCliente);

        populaGrid();

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }

    private string getChavePrimaria()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    private ListDictionary getDadosFormulario()
    {

        ListDictionary oDadosFormulario = new ListDictionary();
        oDadosFormulario.Add("DescricaoRiscoPadrao", dteData.Text);
        oDadosFormulario.Add("CodigoUsuarioInclusao", idUsuarioLogado);
        oDadosFormulario.Add("CodigoEntidade", CodigoEntidade);
        oDadosFormulario.Add("UsuarioInclusao", txtIncluidaPor.Text);
        oDadosFormulario.Add("NomeProjeto", txtProjeto.Text);
        oDadosFormulario.Add("TipoLicao", txtTipo.Text);
        oDadosFormulario.Add("AssuntoLicao", txtAssunto.Text);
        return oDadosFormulario;
    }

    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 135);
        if (altura > 0)
            gvDados.Settings.VerticalScrollableHeight = altura - 90;
    }

    private void HeaderOnTela()
    {

        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));        
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/licoesAprendidas.js""></script>"));
        this.TH(this.TS("barraNavegacao", "licoesAprendidas"));

    }

    private void populaGrid()
    {
        gvDados.DataSource = cDados.getLicoesAprendidas(CodigoEntidade, " and f.DataPublicacao is not null ");
        gvDados.DataBind();
    }
    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        if (e.Parameter == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        else if (e.Parameter == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        else if (e.Parameter == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            pnCallback.JSProperties["cp_OperacaoOk"] = e.Parameter;
        }
        else // alguma coisa deu errado...
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
    }

    private string persisteExclusaoRegistro()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    private string persisteEdicaoRegistro()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    private string persisteInclusaoRegistro()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    protected void pnImage_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string nomeArquivo = "";

        if (e.Parameter == "PDF")
        {
            nomeArquivo = "~/imagens/botoes/imprimir.png";

        }
        if (e.Parameter == "XLS")//excel.PNG
        {
            nomeArquivo = "~/imagens/menuExportacao/iconoExcel.png";
        }
        btnImprimir.ImageUrl = nomeArquivo;
    }

    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            string dataHora = DateTime.Now.ToString().Replace("/", "-").Replace(":", "_") + "_" + idUsuarioLogado;

            string nomeArquivo1 = "", app = "", erro = "";

            try
            {


                if (hfGeral.Get("tipoArquivo").ToString() == "XLS")
                {
                    nomeArquivo1 = "licoesAprendidas_" + dataHora + ".xls";
                    XlsExportOptionsEx x = new XlsExportOptionsEx();

                    ASPxGridViewExporter1.WriteXlsToResponse(new DevExpress.XtraPrinting.XlsExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                    //app = "application/vnd.ms-excel"; TIPO DE REFERENCIA MAIS UTILIZADA
                    app = "application/ms-excel";
                }
            }
            catch
            {
                erro = "S";
            }
            //app = "application/ms-excel";

            if (erro == "")
            {
                if (hfGeral.Get("tipoArquivo").ToString() != "HTML")
                {
                    nomeArquivo1 = "\"" + nomeArquivo1 + "\"";
                    Response.Clear();
                    Response.Buffer = false;
                    Response.AppendHeader("Content-Type", app);
                    Response.AppendHeader("Content-Transfer-Encoding", "binary");
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + nomeArquivo1);
                    Response.BinaryWrite(stream.GetBuffer());
                    Response.End();
                }

            }
            else
            {

                string script = @"<script type='text/Javascript' language='Javascript'>
                                    window.top.mostraMensagem('Erro ao exportar os dados. Verifique se não foi ultrapassado o número máximo de 256 colunas!', 'erro', true, false, null);                                   
                                 </script>";

                ClientScript.RegisterClientScriptBlock(GetType(), "client", script);
            }
        }
    }
    protected void ASPxGridViewExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
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
}
