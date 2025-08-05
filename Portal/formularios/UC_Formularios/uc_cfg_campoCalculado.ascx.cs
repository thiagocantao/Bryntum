using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formularios_UC_Formularios_uc_cfg_campoCalculado : System.Web.UI.UserControl
{
    dados cDados;
    DataTable dtCamposNumericos;

    ASPxGridView gvCampos_;
    object objCodigo;
    private char DelimitadorPropriedadeCampo = '¥';
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;

    private string dbName;
    private string dbOwner;

    private char delimitadorValores = '$';
    private char delimitadorElementoLista = '¢';
    public string ajudaMascara = "";
    public string entidadeDestino = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        /*System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);*/
    }
    
    private void criaCDados()
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

       listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
       listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

       cDados = CdadosUtil.GetCdados(listaParametrosDados);
    }

    protected void gvd_CAL_CamposCalculaveis_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex >= 0)
            e.Row.Cells[0].Text = (e.VisibleIndex + 1).ToString();
    }

    /*
    protected void btnSalvar_VAR_Click(object sender, EventArgs e)
    {
        try
        {
            string comandoSQL = "";

            // obtem o objeto "Grid" que está sendo editado.
            GridViewDetailRowTemplateContainer container = (sender as ASPxButton).NamingContainer.NamingContainer as GridViewDetailRowTemplateContainer;
            gvCampos_ = container.Grid;

            ASPxHiddenField hfControle = gvCampos_.FindDetailRowTemplateControl(gvCampos_.FocusedRowIndex, "hfControle") as ASPxHiddenField;

            string codigoCampo = hfControle.Get("codigoCampo").ToString();
            string codigoTipoCampo = hfControle.Get("CodigoTipoCampo").ToString();
            string codigoFormulario = hfControle.Get("cogigoFormulario").ToString();
            if (codigoTipoCampo == "VAR")
            {
                string tamanho = hfControle.Get("VAR_tamanho").ToString();
                string mascara = hfControle.Get("VAR_mascara").ToString();
                string padrao = hfControle.Get("VAR_padrao").ToString();

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Tam: {2}{1}Msk: {3}{1}Pdr:{4}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, tamanho, mascara, padrao);
                //dvVAR.Visible = true;
            }
            else if (codigoTipoCampo == "TXT")
            {
                string tamanho = hfControle.Get("TXT_tamanho").ToString();
                string linhas = hfControle.Get("TXT_linhas").ToString();
                string formatacao = hfControle.Get("TXT_formatacao").ToString();
                string padrao = hfControle.Get("TXT_padrao").ToString();

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Tam: {2}{1}Lin: {3}{1}For:{4}{1}Pdr:{5}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, tamanho, linhas, formatacao, padrao);
                //dvTXT.Visible = true;
            }

            else if (codigoTipoCampo == "NUM")
            {
                string NUM_Minimo = hfControle.Get("NUM_Minimo").ToString();
                string NUM_Maximo = hfControle.Get("NUM_Maximo").ToString();
                string NUM_Precisao = hfControle.Get("NUM_Precisao").ToString();
                string NUM_Formato = hfControle.Get("NUM_Formato").ToString();
                string NUM_Agregacao = hfControle.Get("NUM_Agregacao").ToString();
                string NUM_padrao = hfControle.Get("NUM_padrao").ToString();

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Min: {2}{1}Max: {3}{1}Pre: {4}{1}For: {5}{1}Agr: {6}{1}Pdr:{7}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, NUM_Minimo, NUM_Maximo, NUM_Precisao, NUM_Formato, NUM_Agregacao, NUM_padrao);
                //dvNUM.Visible = true;
            }
            else if (codigoTipoCampo == "LST")
            {
                string LST_Opcoes = hfControle.Get("LST_Opcoes").ToString();
                string LST_Formatacao = hfControle.Get("LST_Formatacao").ToString();
                string LST_Tamanho = hfControle.Get("LST_Tamanho").ToString();
                string LST_padrao = hfControle.Get("LST_padrao").ToString();
                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Opc: {2}{1}For: {3}{1}Tam: {4}{1}Pdr:{5}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, LST_Opcoes, LST_Formatacao, LST_Tamanho, LST_padrao);
                //dvLST.Visible = true;
            }
            else if (codigoTipoCampo == "DAT")
            {
                string DAT_IncluirHora = hfControle.Get("DAT_IncluirHora").ToString();
                string DAT_ValorInicial = hfControle.Get("DAT_ValorInicial").ToString();
                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Hor: {2}{1}Ini: {3}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, DAT_IncluirHora, DAT_ValorInicial);
                //dvDAT.Visible = true;
            }
            else if (codigoTipoCampo == "BOL")
            {
                string BOL_TextoVerdadeiro = hfControle.Get("BOL_TextoVerdadeiro").ToString();
                string BOL_ValorVerdadeiro = hfControle.Get("BOL_ValorVerdadeiro").ToString();
                string BOL_TextoFalso = hfControle.Get("BOL_TextoFalso").ToString();
                string BOL_ValorFalso = hfControle.Get("BOL_ValorFalso").ToString();

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'TVe: {2}{1}VVe: {3}{1}TFa: {4}{1}VFa: {5}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, BOL_TextoVerdadeiro, BOL_ValorVerdadeiro, BOL_TextoFalso, BOL_ValorFalso);
                //dvBOL.Visible = true;
            }
            else if (codigoTipoCampo == "SUB")
            {
                string CodigoFormulario = hfControle.Get("SUB_CodigoFormulario").ToString();
                comandoSQL = string.Format(
                    @"BEGIN
                        UPDATE CampoModeloFormulario
                           SET DefinicaoCampo = 'Sub: {2}{1}'
                         WHERE codigoCampo = {0} 
                          
                      -- subFormularios não podem aparecer no menu de Formularios do projeto
                      DELETE FROM ModeloFormularioTipoProjeto 
                       WHERE CodigoModeloFormulario = {2}
                  END", codigoCampo, DelimitadorPropriedadeCampo, CodigoFormulario);
                //dvSUB.Visible = true;
            }
            else if (codigoTipoCampo == "CPD")
            {
                string CodigoCampoPre = hfControle.Get("CPD_CampoPre").ToString();
                string linhas = hfControle.Get("CPD_Linhas").ToString();
                string tamanho = hfControle.Get("CPD_Tamanho").ToString();
                int linha = 1;
                if (int.TryParse(linhas, out linha))
                {
                    if (linha > 10)
                        linha = 10;
                }

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'CPD: {2}{1}Lin: {3}{1}Tam: {4}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, CodigoCampoPre, linha, tamanho);
                //dvCPD.Visible = true;
            }
            else if (codigoTipoCampo == "LOO")
            {
                string CodigoListaPre = hfControle.Get("LOO_ListaPre").ToString();
                string tamanho = hfControle.Get("LOO_Tamanho").ToString();
                string LOO_ApresentacaoLOV = hfControle.Get("LOO_ApresentacaoLOV").ToString();
                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'LOO: {2}{1}Tam: {3}{1}LOV: {4}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, CodigoListaPre, tamanho, LOO_ApresentacaoLOV);
                //dvLOO.Visible = true;
            }
            else if (codigoTipoCampo == "REF")
            {
                string CodigoModeloFormulario = hfControle.Get("REF_ModeloFormulario").ToString();
                string CodigoCampoFormulario = hfControle.Get("REF_CampoFormulario").ToString();
                string SomenteLeitura = hfControle.Get("REF_SomenteLeitura").ToString();
                string TituloExterno = hfControle.Get("REF_TituloExterno").ToString();
                string TituloInterno = hfControle.Get("REF_TituloInterno").ToString();

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'CMF: {2}{1}CC: {3}{1}RO: {4}{1}TIE: {5}{1}TII: {6}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, CodigoModeloFormulario, CodigoCampoFormulario, SomenteLeitura, TituloExterno, TituloInterno);
                //dvREF.Visible = true;
            }
            else if (codigoTipoCampo == "CAL")
            {
                string CAL_Precisao = hfControle.Get("CAL_Precisao").ToString();
                string CAL_Formato = hfControle.Get("CAL_Formato").ToString();
                string CAL_Agregacao = hfControle.Get("CAL_Agregacao").ToString();
                string CAL_Formula = hfControle.Get("CAL_Formula").ToString();

                // Ao salvar um campo calculado, temos que substituir a variável (b1, b2, b3...) pelo código do campo
                string formulaComCodigo = getFormulaCampoCalculado_comCodigo(CAL_Formula, int.Parse(codigoFormulario));

                comandoSQL = string.Format(
                    @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Pre: {2}{1}For: {3}{1}Agr: {4}{1}CAL: {5}{1}CL2:{6}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, CAL_Precisao, CAL_Formato, CAL_Agregacao, CAL_Formula, formulaComCodigo);
                //dvCAL.Visible = true;
            }

            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);

            if (gvCampos_ != null)
            {
                populaGridCamposFormulario(int.Parse(objCodigo.ToString()));
                gvCampos_.DetailRows.CollapseAllRows();
                gvCampos_.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private string getFormulaCampoCalculado_comCodigo(string FormulaCampoCalculado_comVariavel, int codigoModeloFormulario)
    {
        FormulaCampoCalculado_comVariavel = FormulaCampoCalculado_comVariavel.ToUpper();

        DataTable dt = getCamposNumericosFormulario(codigoModeloFormulario);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string codigo = "[" + dt.Rows[i]["CodigoCampo"].ToString() + "]";
            FormulaCampoCalculado_comVariavel = FormulaCampoCalculado_comVariavel.Replace("B" + (i + 1), codigo);
        }
        return FormulaCampoCalculado_comVariavel;
    }



    private DataTable getCamposNumericosFormulario(int codigoModeloFormulario)
    {
        string comandoSQL = string.Format(
           @"SELECT CodigoCampo, NomeCampo, null as ValorCampo
                FROM {0}.{1}.CampoModeloFormulario
               WHERE CodigoModeloFormulario = {2}
                 --AND CodigoTipoCampo in ('LOO', 'DAT', 'NUM')
                 AND CodigoTipoCampo in ('NUM')
                 AND DataExclusao is null
               ORDER BY NomeCampo", cDados.getDbName(), cDados.getDbOwner(), codigoModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds.Tables[0];
    }

    private void populaGridCamposFormulario(int codigoModeloFormulario)
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoCampo, NomeCampo, DescricaoCampo, CampoObrigatorio, CodigoTipoCampo, DefinicaoCampo, 
                     OrdemCampoFormulario, CodigoLookup, Aba, IndicaControladoSistema, IndicaCampoVisivelGrid, 
                     IniciaisCampoControladoSistema, IndicaCampoAtivo
                FROM {0}.{1}.CampoModeloFormulario 
               WHERE codigoModeloFormulario = {2}
                 AND dataExclusao is null
               ORDER BY Aba, OrdemCampoFormulario", cDados.getDbName(), cDados.getDbOwner(), codigoModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvCampos_.DataSource = ds;
        gvCampos_.DataBind();
    }*/

    protected void ppDvCampoCalculado_WindowCallback(object source, PopupWindowCallbackArgs e)
    {
        criaCDados();
        int codigoCampo = int.Parse(e.Parameter);
        populaGridCamposCalculaveis(codigoCampo);

        // busca a definição do campo
        preencheInformacoesCampo(codigoCampo);

    }

    private void populaGridCamposCalculaveis(int codigoCampo)
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoCampo, NomeCampo, null as ValorCampo
                        FROM CampoModeloFormulario
                       WHERE CodigoModeloFormulario = (Select CodigoModeloFormulario from CampoModeloFormulario where CodigoCampo = {0})
                         AND CodigoTipoCampo in ('NUM')
                         AND DataExclusao is null
                       ORDER BY NomeCampo", codigoCampo);
        DataSet ds = cDados.getDataSet(comandoSQL);
        dtCamposNumericos = new DataTable("CamposNumericos");
        dtCamposNumericos.Columns.Add("Sequencia", typeof(int));
        dtCamposNumericos.Columns.Add("CodigoCampo", typeof(int));
        dtCamposNumericos.Columns.Add("NomeCampo", typeof(string));
        dtCamposNumericos.Columns.Add("ValorCampo", typeof(decimal));

        // popula o datatale
        int sequencia = 1;
        foreach (DataRow row in ds.Tables[0].Rows)
            dtCamposNumericos.Rows.Add(sequencia++, row["CodigoCampo"], row["NomeCampo"], row["ValorCampo"]);

        gvd_CAL_CamposCalculaveis.DataSource = dtCamposNumericos;
        gvd_CAL_CamposCalculaveis.DataBind();
    }

    private string preencheInformacoesCampo(int codigoCampo)
    {
        string comandoSQL = string.Format(
           @"SELECT NomeCampo, DescricaoCampo, CampoObrigatorio, CodigoTipoCampo, DefinicaoCampo, 
                     OrdemCampoFormulario, CodigoLookup, Aba, IndicaControladoSistema, IndicaCampoVisivelGrid, 
                     IniciaisCampoControladoSistema, IndicaCampoAtivo
                FROM CampoModeloFormulario 
               WHERE CodigoCampo = {0}
                 AND dataExclusao is null
               ORDER BY Aba, OrdemCampoFormulario", codigoCampo);
        DataSet ds = cDados.getDataSet(comandoSQL);
        DataRow rowCampo = ds.Tables[0].Rows[0];

        string DefinicaoCampo = rowCampo["DefinicaoCampo"] + "";

        if (DefinicaoCampo != "")
        {
            string[] aDefinicaoCampo = DefinicaoCampo.Split(DelimitadorPropriedadeCampo);

            string precisao = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            string formato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
            string agregacao = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
            string formulaVariavel = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();
            string formulaCodigo = aDefinicaoCampo[4].Substring(aDefinicaoCampo[4].IndexOf(":") + 1).Trim();

            formulaVariavel = getFormulaCampoCalculado_comVariavel(formulaCodigo);

            ddl_CAL_Precisao.Value = precisao;
            ddl_CAL_Formato.Value = formato;
            ddl_CAL_Agregacao.Value = agregacao;
            ddl_CAL_Agregacao.Value = agregacao;
            txt_CAL_Formula.Value = formulaVariavel;
        }

        return "";
    }

    private string getFormulaCampoCalculado_comVariavel(string FormulaCampoCalculado_comCodigo)
    {
        for (int i = 0; i < dtCamposNumericos.Rows.Count; i++)
        {
            string codigo = "[" + dtCamposNumericos.Rows[i]["CodigoCampo"].ToString() + "]";
            FormulaCampoCalculado_comCodigo = FormulaCampoCalculado_comCodigo.Replace(codigo, "B" + (i + 1));
        }
        return FormulaCampoCalculado_comCodigo;
    }

}