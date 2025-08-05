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
using System.Text;


public partial class espacoTrabalho_propostas : System.Web.UI.Page
{
    dados cDados;
    private int idUsuarioLogado;
    private string resolucaoCliente = "";

    private int alturaPrincipal = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        // inclui o arquivo de scripts desta tela
        Header.Controls.Add(cDados.getLiteral(@"<link href=""estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/Cadastro.js""></script>"));
        this.TH(this.TS("Cadastro"));
        //Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../scripts/riscosPadroes.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<title>Cadastro de Propostas</title>"));

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
        if (!Page.IsPostBack)
        {
            resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();
            tabControl.ActiveTabIndex = 0;
            carregaGrid();
            defineAlturaTela(resolucaoCliente);
            cDados.habilitaComponentes(false, painelCallback);

            gridObjetivos.SettingsText.Title = "<table style='width:100%'><tr><td>Objetivos Estratégicos</td></tr></table>";
        }
    }

    private void carregaGrid()
    {
        //DataSet ds = cDados.getPropostas("");
        
        //if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        //{
        //    grid.DataSource = ds.Tables[0];
        //    grid.DataBind();
        //    grid.FocusedRowIndex = 0;
            
        //}
    }

    private void salvaRegistro()
    {
        string msg = "";
        bool retorno = false;
        if (hfGeral.Get("hfModoEdicao").ToString() == "I")
        {
            //retorno = cDados.incluiRiscosPadroes(txtTitulo.Text, idUsuarioLogado, 1, ref msg);
            if (!retorno)
            {
                cDados.alerta(this,msg);
            }
        }
        else if (hfGeral.Get("hfModoEdicao").ToString() == "E")
        {
            //retorno = cDados.atualizaRiscosPadroes(txtTitulo.Text, int.Parse(hfGeral.Get("hfCodigoRisco").ToString()), ref msg1);
            if (!retorno)
            {
                cDados.alerta(this,msg);
            }
        }
        hfGeral.Set("hfModoEdicao", "");
        carregaGrid();
    }

    protected void painelCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "Desabilitar")
        {
            //Desabilita todos os componentes de edição
            cDados.habilitaComponentes(false, painelCallback);
            gridObjetivos.SettingsText.Title = "<table style='width:100%'><tr></td><td>Objetivos Estratégicos</td></tr></table>";
        
        }
        else
        {
            if (e.Parameter == "Inserir")
            {
                cDados.limpaCampos(painelCallback);
                cDados.habilitaComponentes(true, painelCallback);
                gridObjetivos.SettingsText.Title = "<table style='width:100%'><tr><td style='width:30px'><img src='../imagens/botoes/novoReg.png' alt='Novo Objetivo' onclick='gridObjetivos.AddNewRow();' style='cursor: pointer; border: 1px solid #CDCDCD'/></td></td><td>Objetivos Estratégicos</td></tr></table>";
            }

            else if (e.Parameter == "Editar")
            {
                cDados.habilitaComponentes(true, painelCallback);
                gridObjetivos.SettingsText.Title = "<table style='width:100%'><tr><td style='width:30px'><img src='../imagens/botoes/novoReg.png' alt='Novo Objetivo' onclick='gridObjetivos.AddNewRow();' style='cursor: pointer; border: 1px solid #CDCDCD'/></td></td><td>Objetivos Estratégicos</td></tr></table>";
            }
        }
    }
    
    protected void painelCallBackGrid_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter == "Excluir")
        {
            executaExlusao();
        }
        else
        {
            if (e.Parameter == "Salvar")
            {
                salvaRegistro();
            }
        }
    }
    
    private void defineAlturaTela(string resolucaoCliente)
    {
        // Calcula a altura da tela
        alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int altura = (alturaPrincipal - 205);
        if (altura > 0)
            tabControl.Height = (altura);

        gridObjetivos.Settings.VerticalScrollableHeight = altura - 250;
    }

    private void executaExlusao()
    {
        string msg = "";
        
        bool retorno = false;// = cDados.excluiRiscosPadroes(int.Parse(hfGeral.Get("hfCodigoRisco").ToString()), idUsuarioLogado, ref msg);
        
        if (!retorno)
        {
            cDados.alerta(this,msg);
        }
        else
        {
            carregaGrid();
        }
    }

    protected void gridObjetivos_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {

    }

    protected void gridObjetivos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }

    protected void gridObjetivos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

    }

    protected void gridObjetivos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

    }
}
