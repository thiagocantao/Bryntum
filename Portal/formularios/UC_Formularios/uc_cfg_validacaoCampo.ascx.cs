using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formularios_UC_Formularios_uc_cfg_validacaoCampo : System.Web.UI.UserControl
{
    dados cDados;
    DataTable dtCamposDisponiveis;

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    private void criaCDados()
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
    }

    protected void ppDvEditorExpressaoValidacao_WindowCallback(object source, DevExpress.Web.PopupWindowCallbackArgs e)
    {
        criaCDados();
        int codigoCampo = int.Parse(e.Parameter);
        populaGridCamposDisponiveis(codigoCampo);

            ppDvEditorExpressaoValidacao.JSProperties["cpCodigoNomeCampoSelecionado"] = codigoCampo;

    }

    private void populaGridCamposDisponiveis(int codigoCampo)
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoCampo, NomeCampo, DescricaoCampo,  CodigoTipoCampo, CampoObrigatorio, ExpressaoValidacao, MensagemValidacao
                        FROM CampoModeloFormulario
                       WHERE CodigoModeloFormulario = (Select CodigoModeloFormulario from CampoModeloFormulario where CodigoCampo = {0})
                         --AND CodigoTipoCampo in ('NUM')
                         AND DataExclusao is null
                         AND IndicaCampoAtivo = 'S'
                       ORDER BY NomeCampo", codigoCampo);

        DataSet ds = cDados.getDataSet(comandoSQL);
        dtCamposDisponiveis = new DataTable("CamposNumericos");
        dtCamposDisponiveis.Columns.Add("Sequencia", typeof(int));
        dtCamposDisponiveis.Columns.Add("CodigoCampo", typeof(int));
        dtCamposDisponiveis.Columns.Add("NomeCampo", typeof(string));
        dtCamposDisponiveis.Columns.Add("CodigoTipoCampo", typeof(string));

        // os campos disponíveis serão enviado para o cliente em um array
        object[] arrayCamposDisponiveis = new object[ds.Tables[0].Rows.Count];

        // popula o datatale
        int sequencia = 0;
        string ExpressaoValidacao = "";
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            // se este é o campo selecionado para receber a expressao. Monta um resposta para ser enviada ao cliente via JSProperties
            if (int.Parse(row["CodigoCampo"] + "") == codigoCampo)
            {
                ppDvEditorExpressaoValidacao.JSProperties["cpCodigoNomeCampoSelecionado"] = string.Format("{0};[{1}!{2}]", codigoCampo, sequencia + 1, row["NomeCampo"]);
                txtCampoSelecionado.Text = string.Format("[{0}!{1}]", sequencia + 1, row["NomeCampo"]);
                txtMensagemValidacao.Text = row["MensagemValidacao"] + "";
                ExpressaoValidacao = row["ExpressaoValidacao"] + "";
            }

            dtCamposDisponiveis.Rows.Add(sequencia + 1, row["CodigoCampo"], row["NomeCampo"], row["CodigoTipoCampo"]);
            object[] campoDisponivel = new object[4];
            campoDisponivel[0] = sequencia + 1;
            campoDisponivel[1] = int.Parse(row["CodigoCampo"] + "");
            campoDisponivel[2] = row["NomeCampo"] + "";
            campoDisponivel[3] = row["CodigoTipoCampo"] + "";
            arrayCamposDisponiveis[sequencia] = campoDisponivel;
            sequencia++;
        }

        // Reescreve a expressão de validação, trocando os códigos pela sequencia e nome de campos
        foreach(DataRow row in dtCamposDisponiveis.Rows)
            ExpressaoValidacao = ExpressaoValidacao.Replace("[" + row["CodigoCampo"] + "]", string.Format("[{0}!{1}]", row["Sequencia"], row["NomeCampo"]));
        txtEdidorExpressao.Text = ExpressaoValidacao;

        gvCamposDisponiveis.DataSource = dtCamposDisponiveis;
        gvCamposDisponiveis.DataBind();

        hfCamposDisponiveis.Set("CodigoCampo", codigoCampo);
        hfCamposDisponiveis.Set("arrayCamposDisponiveis", arrayCamposDisponiveis);
    }

    protected void hfCamposDisponiveis_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string codigoCampo = hfCamposDisponiveis.Get("CodigoCampo") + "";
        string expressaoAvaliada = hfCamposDisponiveis.Get("expressaoAvaliada") + "";
        string expressaoExtenso = hfCamposDisponiveis.Get("expressaoExtenso") + "";
        string mensagem = hfCamposDisponiveis.Get("mensagem") + "";

        string comandoSQL = string.Format(
            @"UPDATE CampoModeloFormulario
                 SET ExpressaoValidacao = '{1}',
                     MensagemValidacao = '{2}'
               WHERE CodigoCampo = {0}", codigoCampo, expressaoAvaliada, mensagem.Replace("'", "''"));

        criaCDados();
        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);
    }
}