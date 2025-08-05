/*OBSERVAÇÕES
 * 
 * MODIFICAÇÕES
 * 
 * 01/03/2011 :: Alejandro : -Alteração no método 'carregaComboFontePagadora()', aonde agora indicara o código da
 *                          entidade logada para filtrar as fontes pagadoras correspondientes.
 *                          -Alteração do desenho da grid para obter o padron.
 * 
 * 17/03/2011 :: Alejandro : adiciono el botão de Permissãos para os contratos.
 * 21/03/2011 :: Alejandro : adiciono el control de acesso para o botão de permissões [CT_AdmPrs].
 */
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
using DevExpress.XtraPrinting;
using System.IO;
using System.Drawing;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using System.Collections.Generic;

public partial class frmCadastroPessoa : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    public string ocultarLinhas = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // a pagina não pode ser armazenada no cache.
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HeaderOnTela();

        if (cDados.getInfoSistema("ResolucaoCliente") == null)
            Response.Redirect("~/index.aspx");

        cDados.aplicaEstiloVisual(Page);

        carregaComboUF();
        carregaComboMunicipio();
        carregaRamoAtividade();

        if (Request.QueryString["CP"] != null && Request.QueryString["CP"].ToString() != "")
            montaCampos(int.Parse(Request.QueryString["CP"].ToString()));

        if (Request.QueryString["RO"] != null && Request.QueryString["RO"].ToString() == "S")
            desabilitaCampos();

        if (Request.QueryString["PP"] != null && Request.QueryString["PP"].ToString() == "N")
            btnCancelar.ClientVisible = false;


    }

    #region VARIOS

    private void HeaderOnTela()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"">var pastaImagens = ""../../imagens"";</script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link href=""../../estilos/cdisEstilos.css"" rel=""stylesheet"" type=""text/css"" />"));
        this.TH(this.TS("frmCadastroPessoa"));
    }

    #endregion

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        int codigoPessoa = -1;

        string nomePessoa = txtNomePessoa.Text;
        string nomeFantasia = txtNomeFantasia.Text;
        string tipoPessoa = rbTipoPessoa.Value.ToString();
        string numeroIdentificacao = tipoPessoa == "F" ? txtCPF.Text : txtCNPJ.Text;
        string codigoRamoAtividade = ddlRamoAtividade.SelectedIndex == -1 ? "NULL" : ddlRamoAtividade.Value.ToString();
        string email = txtEmail.Text == "" ? "NULL" : "'" + txtEmail.Text.Replace("'", "''") + "'";
        string comentarios = txtComentarios.Text;

        bool result = cDados.incluiFornecedor(codigoEntidadeUsuarioResponsavel, nomePessoa, nomeFantasia, tipoPessoa, numeroIdentificacao, codigoRamoAtividade, txtEnderecoPessoa.Text, txtTelefone.Text
            , txtNomeContato.Text, email, txtInformacoesContato.Text, comentarios, int.Parse(ddlMunicipio.Value.ToString()), "N", "S", "N",ref codigoPessoa);

        if (result)
            callbackSalvar.JSProperties["cp_CodigoPessoa"] = codigoPessoa;
        else
            callbackSalvar.JSProperties["cp_MsgErro"] = "Erro ao cadastrar a razão social!";
    }

    private void carregaComboUF()
    {
        DataSet ds = cDados.getUF("");
        ddlUF.DataSource = ds;
        ddlUF.DataBind();

        if (!IsPostBack && ddlUF.Items.Count > 0)
            ddlUF.SelectedIndex = 0;
    }

    private void carregaComboMunicipio()
    {
        DataSet ds = cDados.getMunicipios("AND SiglaUF = '" + ddlUF.Value + "'");

        ddlMunicipio.TextField = "NomeMunicipio";
        ddlMunicipio.ValueField = "CodigoMunicipio";
        ddlMunicipio.DataSource = ds;
        ddlMunicipio.DataBind();
    }

    private void carregaRamoAtividade()
    {
        DataSet ds = cDados.getTipoRamoAtividade("");

        ddlRamoAtividade.TextField = "RamoAtividade";
        ddlRamoAtividade.ValueField = "CodigoRamoAtividade";
        ddlRamoAtividade.DataSource = ds;
        ddlRamoAtividade.DataBind();
    }

    private void montaCampos(int codigoPessoa)
    {
        DataSet ds = cDados.getFornecedores(codigoEntidadeUsuarioResponsavel, "AND p.codigoPessoa = " + codigoPessoa);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {

            DataRow dr = ds.Tables[0].Rows[0];
            txtNomePessoa.Text = dr["NomePessoa"].ToString();
            txtNomeFantasia.Text = dr["NomeFantasia"].ToString();
            rbTipoPessoa.Value = dr["TipoPessoa"] != null && dr["TipoPessoa"].ToString() != "" ? dr["TipoPessoa"].ToString() : null;
            txtCPF.Text = dr["NumeroCNPJCPF"].ToString();
            txtCNPJ.Text = dr["NumeroCNPJCPF"].ToString();
            ddlRamoAtividade.Value = dr["CodigoRamoAtividade"] != null && dr["CodigoRamoAtividade"].ToString() != "" ? dr["CodigoRamoAtividade"].ToString() : null;
            txtEmail.Text = dr["Email"].ToString();
            txtComentarios.Text = dr["Comentarios"].ToString();
            txtEnderecoPessoa.Text = dr["EnderecoPessoa"].ToString();
            txtTelefone.Text = dr["TelefonePessoa"].ToString();
            txtNomeContato.Text = dr["NomeContato"].ToString();
            txtInformacoesContato.Text = dr["InformacaoContato"].ToString();
            ddlUF.Value = dr["SiglaUF"] != null && dr["SiglaUF"].ToString() != "" ? dr["SiglaUF"].ToString() : null;
            carregaComboMunicipio();
            ddlMunicipio.Value = dr["CodigoMunicipioEnderecoPessoa"] != null && dr["CodigoMunicipioEnderecoPessoa"].ToString() != "" ? dr["CodigoMunicipioEnderecoPessoa"].ToString() : null;
        }

    }

    private void desabilitaCampos()
    {
        txtNomePessoa.ClientEnabled = false;
        txtNomeFantasia.ClientEnabled = false;
        rbTipoPessoa.ClientEnabled = false;
        txtCPF.ClientEnabled = false;
        txtCNPJ.ClientEnabled = false;
        ddlRamoAtividade.ClientEnabled = false;
        txtEmail.ClientEnabled = false;
        txtComentarios.ClientEnabled = false;
        txtEnderecoPessoa.ClientEnabled = false;
        txtTelefone.ClientEnabled = false;
        txtNomeContato.ClientEnabled = false;
        txtInformacoesContato.ClientEnabled = false;
        ddlMunicipio.ClientEnabled = false;
        ddlUF.ClientEnabled = false;
        btnSalvar.ClientVisible = false;

    }





}
