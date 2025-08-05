using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_UC_Workflow_uc_crud_caminhoCondicional : System.Web.UI.UserControl
{
    dados cDados;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // inclui a referencia para o arquivo de scripts correspondente
            LiteralControl jsResource = new LiteralControl();
            jsResource.Text = "<script type=\"text/javascript\" src=\"../scripts/uc_crud_caminhoCondicional.js?v=2\"></script>";
            Page.Header.Controls.Add(jsResource);
        }

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

        gv_Condicoes.SettingsText.Title = @"<table cellpadding=""0"" cellspacing=""0"" style=""width: 100%""><tr><td style=""width: 50px"">
                                            <img style=""cursor: pointer"" onclick=""preparaTelaParaNovaCondicao();"" src=""../imagens/botoes/incluirReg02.png"" alt=""Adicionar Nova Condição""/>                                               
                                            </td><td align=""center"">Condições</td><td style=""width: 50px""></td></tr></table>";
    }

    protected void lstCampos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        int CodigoModeloFormulario = int.Parse(e.Parameter);
        DataTable dtCampos = getCamposModeloFormulario(CodigoModeloFormulario);
        lstCampos.DataSource = dtCampos;
        lstCampos.ValueField = "CodigoCampo";
        lstCampos.TextField = "NomeCampo";
        lstCampos.DataBind();
    }

    private DataTable getCamposModeloFormulario(int CodigoModeloFormulario)
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoCampo, NomeCampo
                FROM CampoModeloFormulario
               WHERE CodigoModeloFormulario = {0}
                 AND DataExclusao IS NULL
                 AND IndicaCampoAtivo = 'S'
               ORDER BY NomeCampo", CodigoModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds.Tables[0];
    }

    public bool getFormularioValido(int CodigoModeloFormulario)
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoTipoFormulario
                FROM ModeloFormulario
               WHERE CodigoModeloFormulario = {0}", CodigoModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds.Tables[0].Rows[0]["CodigoTipoFormulario"] + "" != "3";
    }


    protected void lstFormularios_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        // retira da lista de formulários, aqueles que não são válidos para o editor de condições
        if (e.Parameter.ToUpper() == "FILTRA")
        {
            //percorre a lista de itens
            List<DevExpress.Web.ListEditItem> lstItensInvalidos = new List<DevExpress.Web.ListEditItem>();
            foreach (DevExpress.Web.ListEditItem item in lstFormularios.Items)
            {
                // se não é válido, copia para a lista de inválidos
                if (!getFormularioValido(int.Parse(item.Value + "")))
                    lstItensInvalidos.Add(item);
            }

            // remove os itens inválidos
            foreach (DevExpress.Web.ListEditItem item in lstItensInvalidos)
                lstFormularios.Items.Remove(item);

            // constroi um array De-Para com o código e nome de todos os campos disponíveis para uso na expressão
            string simulaArrayRetorno = "";
            foreach (DevExpress.Web.ListEditItem item in lstFormularios.Items)
            {
                // busca os campos para o modelo de formulário selecionado 
                DataTable dtCampos = getCamposModeloFormulario(int.Parse(item.Value + ""));
                // inclui todos os campos na string que simula o array "De-Para" que será tratado no cliente. Método "constroiDeParaCodigoCampo()" do arquivo "uc_crud_caminhoCondicional.js"
                foreach (DataRow row in dtCampos.Rows)
                    simulaArrayRetorno += string.Format("{0};[{1}.{2}]", row["CodigoCampo"], item.Text, row["NomeCampo"]) + "|";
            }

            // se existe depara, retira o último delimitador "|"
            if (simulaArrayRetorno != "")
                simulaArrayRetorno = simulaArrayRetorno.Substring(0, simulaArrayRetorno.Length - 1);

            lstFormularios.JSProperties["cpDeParaCodigoCampo"] = simulaArrayRetorno;
        }
    }

    protected void gv_Condicoes_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        // obtem os registros a partir do HF
        object[] arrayRegistrosGridExpressoes = (object[])hf.Get("arrayRegistrosGridExpressoes");

        // cria o datatable para receber as expressoes
        DataTable dt = new DataTable("Expressoes");
        dt.Columns.Add("CodigoExpressao", typeof(int));
        dt.Columns.Add("ExpressaoExtenso", typeof(string));
        dt.Columns.Add("ExpressaoAvaliada", typeof(string));
        dt.Columns.Add("CodigoEtapaDestino", typeof(int));
        dt.Columns.Add("NomeEtapaDestino", typeof(string));

        // popula o datatale
        foreach (object[] registro in arrayRegistrosGridExpressoes)
            dt.Rows.Add(registro[0], registro[1], registro[2], registro[3], registro[4]);

        gv_Condicoes.DataSource = dt;
        gv_Condicoes.DataBind();
    }

    protected void callbackForms_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        string valoresInvalidos = "";
        if (e.Parameter != "")
        {
            foreach(string valor in e.Parameter.Split(','))
            {
                if(valor != "")
                {
                    if (!getFormularioValido(int.Parse(valor)))
                    {
                        valoresInvalidos += (valor + ";");
                    }

                }
            }
        }

        callbackForms.JSProperties["cp_Valores"] = valoresInvalidos;

    }
}