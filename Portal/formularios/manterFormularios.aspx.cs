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
using DevExpress.Web;
using System.Collections.Generic;
using System.Drawing;

public partial class formularios_manterFormularios : System.Web.UI.Page
{
    dados cDados;
    ASPxGridView gvCampos_;
    object objCodigo;
    private char DelimitadorPropriedadeCampo = '¥';

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        dsTipoCampo.ConnectionString = cDados.classeDados.getStringConexao();
        dsTipoFormulario.ConnectionString = cDados.classeDados.getStringConexao();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        populaGridFormularios();
        gvFormularios.DataBind();

/*        ASPxPanel pnAplicacaoFormulario = gvFormularios.FindEditFormTemplateControl("pnAplicacaoFormulario") as ASPxPanel;
        // insere os tipos de aplicação disponível
        if (pnAplicacaoFormulario != null)
            tipoProjeto(ref pnAplicacaoFormulario);
  */      
    }

    private void populaGridFormularios()
    {
        string comandoSQL = string.Format(
            @"select * 
                from {0}.{1}.modeloFormulario 
               where dataExclusao is null", cDados.getDbName(), cDados.getDbOwner());
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvFormularios.DataSource = ds;
    }

  /*  private void populaComboTipoFormulario()
    {
        string comandoSQL = "Select * from tipoFormulario order by descricaoTipoFormulario";
        DataSet ds = dados.getDataSet(comandoSQL);
        // associa o dataset (ds) com a coluna codigoTipoFormlario
        ((GridViewDataComboBoxColumn)gvFormularios.Columns["CodigoTipoFormulario"]).PropertiesComboBox.DataSource = ds;
    }*/

    private void populaGridCamposFormulario(int codigoModeloFormulario)
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoCampo, NomeCampo, DescricaoCampo, CampoObrigatorio, CodigoTipoCampo, 
                     DefinicaoCampo, OrdemCampoFormulario, CodigoLookup, Aba
                FROM {0}.{1}.CampoModeloFormulario 
               WHERE codigoModeloFormulario = {2}
                 AND dataExclusao is null
               ORDER BY OrdemCampoFormulario", cDados.getDbName(), cDados.getDbOwner(), codigoModeloFormulario);
        DataSet ds = cDados.getDataSet(comandoSQL);
        gvCampos_.DataSource = ds;
    }

    private void populaComboSubFormularios(ASPxComboBox combo, int codigoFormularioPai)
    {
        string comandoSQL = string.Format(
            @"SELECT codigoModeloFormulario, nomeFormulario 
                FROM {0}.{1}.ModeloFormulario
               WHERE codigoModeloFormulario <> {2}
               ORDER BY nomeFormulario", cDados.getDbName(), cDados.getDbOwner(), codigoFormularioPai);
        DataSet ds = cDados.getDataSet(comandoSQL);
        combo.DataSource = ds.Tables[0];
        //combo.ValueField = "codigoModeloFormulario";
        //combo.TextField = "nomeFormulario";
        combo.DataBind();
    }

    private void populaComboCampoPredefinido(ASPxComboBox combo)
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoCampoPreDefinido as Codigo, NomeCampoPreDefinido as Descricao
                FROM {0}.{1}.CampoPreDefinido
               ORDER BY NomeCampoPreDefinido ", cDados.getDbName(), cDados.getDbOwner());
        DataSet ds = cDados.getDataSet(comandoSQL);
        combo.DataSource = ds.Tables[0];
        combo.DataBind();
    }

    private void populaComboListaLookup(ASPxComboBox combo)
    {
        string comandoSQL = string.Format(
            @"SELECT CodigoLookup as Codigo, DescricaoLookup as Descricao
                FROM {0}.{1}.Lookup
               ORDER BY DescricaoLookup ", cDados.getDbName(), cDados.getDbOwner());
        DataSet ds = cDados.getDataSet(comandoSQL);
        combo.DataSource = ds.Tables[0];
        combo.DataBind();
    }

    protected void gvFormularios_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string Abas = "";
        if (e.NewValues["Abas"] != null)
            Abas = e.NewValues["Abas"].ToString().Trim();
        else // se o usuário não informou nenhuma aba, vamos inserir uma com o nome Principal
            Abas = "Principal";

        string comandoSQL = string.Format(
                @"INSERT INTO ModeloFormulario (NomeFormulario, DescricaoFormulario, IndicaControladoSistema, CodigoTipoFormulario,
                    DataInclusao, IncluidoPor, CodigoEntidade, Abas)
                  VALUES ('{0}', '{1}', 'N', {2}, getDate(), 1, 1, '{3}')",
                e.NewValues["NomeFormulario"] != null ? e.NewValues["NomeFormulario"].ToString() : "",
                e.NewValues["DescricaoFormulario"] != null ? e.NewValues["DescricaoFormulario"].ToString() : "",
                e.NewValues["CodigoTipoFormulario"] != null ? e.NewValues["CodigoTipoFormulario"].ToString() : "",
                Abas);
        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);
        e.Cancel = true;
        gvFormularios.CancelEdit();
        populaGridFormularios();
    }

    protected void gvFormularios_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string Abas="";
        if (e.NewValues["Abas"] != null)
            Abas = e.NewValues["Abas"].ToString().Trim();
        else // se o usuário não informou nenhuma aba, vamos inserir uma com o nome Principal
            Abas = "Principal";

        // vamos ver a quantidade de abas do formulário
        string[] aAbas = Abas.Split('\r');
        int qtdeMaximaAbas = 0;
        foreach (string aba in aAbas)
        {
            if (aba.Trim() != "")
                qtdeMaximaAbas++;
        }

        string comandoSQL = string.Format(
                @"BEGIN
                    UPDATE ModeloFormulario 
                       SET NomeFormulario = '{1}'
                         , DescricaoFormulario = '{2}' 
                         , CodigoTipoFormulario = {3}
                         , DataUltimaAlteracao = getdate()
                         , AlteradoPor = 1
                         , Abas = '{4}'
                   WHERE CodigoModeloFormulario = {0}

                   UPDATE CampoModeloFormulario
                      SET Aba = 0 
                    WHERE codigoModeloFormulario = {0}
                      AND (Aba is null or Aba > {5} )

                END",
                e.Keys[0].ToString(),
                e.NewValues["NomeFormulario"].ToString(),
                e.NewValues["DescricaoFormulario"]!=null?e.NewValues["DescricaoFormulario"].ToString():"",
                e.NewValues["CodigoTipoFormulario"].ToString(),
                Abas,
                qtdeMaximaAbas-1);
        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);
        e.Cancel = true;
        gvFormularios.CancelEdit();
        populaGridFormularios();
    }

    protected void gvCampos_BeforePerformDataSelect(object sender, EventArgs e)
    {
        // Este evento ocorre antes da grid gvCampos receber os dados do select que a popula
        // como é um master-detail, antes de popularmos o detail, temos que o obter o código (keyFieldName) da grid master
        objCodigo = (sender as ASPxGridView).GetMasterRowKeyValue();

        // obter a relação de abas do formulário selecionado
        string Abas = (sender as ASPxGridView).GetMasterRowFieldValues("Abas").ToString().Replace("\r\n", "\r");
        string[] aAbas = Abas.Split('\r');
        int seqAba = 0;

        ((GridViewDataComboBoxColumn)(sender as ASPxGridView).Columns["Aba"]).PropertiesComboBox.Items.Clear();
        foreach (string aba in aAbas)
        {
            if (aba.Trim() != "")
                ((GridViewDataComboBoxColumn)(sender as ASPxGridView).Columns["Aba"]).PropertiesComboBox.Items.Add(aba.Trim(), seqAba++);
        }
    }

    protected void gvFormularios_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        if (e.Expanded)
        {
            hfIndexFormulario.Value = e.VisibleIndex.ToString();
            // procura pela grid "Campos" dentro do detailRow da grid Formularios
            gvCampos_ = gvFormularios.FindDetailRowTemplateControl(e.VisibleIndex, "gvCampos") as ASPxGridView;
            if (gvCampos_ != null)
            {
                // a variavel "objCodigo" é lida no evento "gvCampos_BeforePerformDataSelect"
                if (objCodigo != null)
                {
                    populaGridCamposFormulario(int.Parse(objCodigo.ToString()));
                    gvCampos_.DataBind();

                    // busca a lista de abas do formulário selecionado
                    DataRowView drv = (DataRowView)gvFormularios.GetRow(e.VisibleIndex);
                    string Abas = drv["Abas"].ToString();
                    ((GridViewDataComboBoxColumn)gvCampos_.Columns["Aba"]).PropertiesComboBox.Items.Add("Aba 1_", 3);
                    ((GridViewDataComboBoxColumn)gvCampos_.Columns["Aba"]).PropertiesComboBox.Items.Add("Aba 1_", 4);
                    // busca a lista de campos para o formulário selecionado.

                    // guarda a linha selecionada na grid de formularios
                    //ASPxHiddenField hfControle_ = gvCampos_.FindControl("hfControle") as ASPxHiddenField;
                    //gvCampos_.ToolTip = Abas;
                    hfIndexFormulario.Value = e.VisibleIndex.ToString();
                }
            }
        }
    }

    protected void gvCampos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //object objCodigoMaster = (sender as ASPxGridView).GetMasterRowKeyValue();

        string comandoSQL = string.Format(
                @"INSERT INTO CampoModeloFormulario (CodigoModeloFormulario, NomeCampo, DescricaoCampo, CampoObrigatorio, 
                         OrdemCampoFormulario, CodigoTipoCampo, DefinicaoCampo, DataInclusao, IncluidoPor, Aba)
                  VALUES ({0}, '{1}', '{2}', '{3}',
                         {4}, '{5}', '', getDate(), 1, '{6}')",
                int.Parse(objCodigo.ToString()),
                e.NewValues["NomeCampo"].ToString(),
                e.NewValues["DescricaoCampo"] != null ? e.NewValues["DescricaoCampo"].ToString() : "",
                e.NewValues["CampoObrigatorio"].ToString(),
                1,
                e.NewValues["CodigoTipoCampo"].ToString(),
                e.NewValues["Aba"] != null ? e.NewValues["Aba"].ToString() : "0");
        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);
        e.Cancel = true;
        gvCampos_ = (sender as ASPxGridView);
        gvCampos_.CancelEdit();
        populaGridCamposFormulario(int.Parse(objCodigo.ToString()));

        gvCampos_.AddNewRow();
    }

    protected void gvCampos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string comandoSQL = string.Format(
        @"UPDATE CampoModeloFormulario 
                     SET NomeCampo = '{1}'
                       , DescricaoCampo = '{2}' 
                       , CampoObrigatorio = '{3}'
                       , CodigoTipoCampo = '{4}'
                       , DataUltimaAlteracao = getdate()
                       , AlteradoPor = 1
                       , Aba = '{5}'
                  WHERE CodigoCampo = {0}",
        e.Keys[0].ToString(),
        e.NewValues["NomeCampo"].ToString(),
        e.NewValues["DescricaoCampo"] != null ? e.NewValues["DescricaoCampo"].ToString() : "",
        e.NewValues["CampoObrigatorio"].ToString(),
        e.NewValues["CodigoTipoCampo"].ToString(),
        e.NewValues["Aba"].ToString());
        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);
        e.Cancel = true;
        gvCampos_ = (sender as ASPxGridView);
        gvCampos_.CancelEdit();
        populaGridCamposFormulario(int.Parse(objCodigo.ToString()));
    }

    protected void gvCampos_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        if (e.Expanded)
        {
            ASPxGridView gridCampos = (sender as ASPxGridView);
            string codigoTipoCampo = gridCampos.GetRowValues(e.VisibleIndex, "CodigoTipoCampo").ToString();
            string codigoCampo = gridCampos.GetRowValues(e.VisibleIndex, "CodigoCampo").ToString();
            ASPxHiddenField hfControle_ = gridCampos.FindDetailRowTemplateControl(e.VisibleIndex, "hfControle") as ASPxHiddenField;
            hfControle_.Set("codigoCampo", codigoCampo);
            hfControle_.Set("CodigoTipoCampo", codigoTipoCampo);
            hfControle_.Set("indexLinhaCampos", e.VisibleIndex);
            hfControle_.Set("cogigoFormulario", objCodigo.ToString());

            ASPxPanel divTipoCampoSelecionado = gridCampos.FindDetailRowTemplateControl(e.VisibleIndex, "dv" + codigoTipoCampo) as ASPxPanel;
            if (divTipoCampoSelecionado != null)
            {
                divTipoCampoSelecionado.Visible = true;
                string definicaoCampo = gridCampos.GetRowValues(e.VisibleIndex, "DefinicaoCampo").ToString().Trim();

                if (definicaoCampo != "" || codigoTipoCampo == "SUB" || codigoTipoCampo == "CPD" || codigoTipoCampo == "LOO")
                {
                    string[] aDefinicaoCampo = definicaoCampo.Split(DelimitadorPropriedadeCampo);

                    if (codigoTipoCampo == "VAR")
                    {
                        string tamanho = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        (divTipoCampoSelecionado.FindControl("txt_VAR_tamanho") as ASPxTextBox).Value = tamanho;
                    }
                    else if (codigoTipoCampo == "TXT")
                    {
                        string tamanho = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string linhas = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        string formato = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                        (divTipoCampoSelecionado.FindControl("txt_TXT_tamanho") as ASPxTextBox).Value = tamanho;
                        (divTipoCampoSelecionado.FindControl("txt_TXT_linhas") as ASPxTextBox).Value = linhas;
                        if (formato == "0")
                            (divTipoCampoSelecionado.FindControl("rb_TXT_SemFormatacao") as ASPxRadioButton).Checked = true;
                        else if (formato == "1")
                            (divTipoCampoSelecionado.FindControl("rb_TXT_FormatacaoSimples") as ASPxRadioButton).Checked = true;
                        else if (formato == "2")
                            (divTipoCampoSelecionado.FindControl("rb_TXT_FormatacaoAvancada") as ASPxRadioButton).Checked = true;
                    }
                    else if (codigoTipoCampo == "NUM")
                    {
                        string minimo = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string maximo = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        string precisao = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                        string formato = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();
                        (divTipoCampoSelecionado.FindControl("txt_NUM_Minimo") as ASPxTextBox).Value = minimo;
                        (divTipoCampoSelecionado.FindControl("txt_NUM_Maximo") as ASPxTextBox).Value = maximo;
                        (divTipoCampoSelecionado.FindControl("ddl_NUM_Precisao") as ASPxComboBox).Value = precisao;
                        (divTipoCampoSelecionado.FindControl("ddl_NUM_Formato") as ASPxComboBox).Value = formato;
                    }
                    else if (codigoTipoCampo == "LST")
                    {
                        string opcoes = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string formato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        (divTipoCampoSelecionado.FindControl("txt_LST_Opcoes") as ASPxMemo).Value = opcoes;
                        if (formato == "0")
                            (divTipoCampoSelecionado.FindControl("rb_LST_Combo") as ASPxRadioButton).Checked = true;
                        else if (formato == "1")
                            (divTipoCampoSelecionado.FindControl("rb_LST_Radio") as ASPxRadioButton).Checked = true;
                        else if (formato == "2")
                            (divTipoCampoSelecionado.FindControl("rb_LST_Check") as ASPxRadioButton).Checked = true;
                    }
                    else if (codigoTipoCampo == "DAT")
                    {
                        string incluirHora = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string valorInicial = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        if (incluirHora == "S")
                            (divTipoCampoSelecionado.FindControl("rb_DAT_Sim") as ASPxRadioButton).Checked = true;
                        else if (incluirHora == "N")
                            (divTipoCampoSelecionado.FindControl("rb_DAT_Nao") as ASPxRadioButton).Checked = true;

                        if (valorInicial == "B")
                            (divTipoCampoSelecionado.FindControl("rb_DAT_Branco") as ASPxRadioButton).Checked = true;
                        if (valorInicial == "A")
                            (divTipoCampoSelecionado.FindControl("rb_DAT_Atual") as ASPxRadioButton).Checked = true;
                    }
                    else if (codigoTipoCampo == "BOL")
                    {
                        string textoVerdadeiro = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        string valorVerdadeiro = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        string textoFalso = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                        string valorFalso = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();
                        (divTipoCampoSelecionado.FindControl("txt_BOL_TextoVerdadeiro") as ASPxTextBox).Value = textoVerdadeiro;
                        (divTipoCampoSelecionado.FindControl("txt_BOL_ValorVerdadeiro") as ASPxTextBox).Value = valorVerdadeiro;
                        (divTipoCampoSelecionado.FindControl("txt_BOL_TextoFalso") as ASPxTextBox).Value = textoFalso;
                        (divTipoCampoSelecionado.FindControl("txt_BOL_ValorFalso") as ASPxTextBox).Value = valorFalso;
                    }
                    else if (codigoTipoCampo == "SUB")
                    {
                        string codigoSubFormulario = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        // popula o combo de subformularios
                        ASPxComboBox combo = divTipoCampoSelecionado.FindControl("ddl_SUB_Formulario") as ASPxComboBox;
                        if (combo != null)
                        {
                            populaComboSubFormularios(combo, int.Parse(objCodigo.ToString()));
                            if (codigoSubFormulario != "")
                                combo.Value = int.Parse(codigoSubFormulario);
                        }
                    }
                    else if (codigoTipoCampo == "CPD")
                    {
                        string codigoCampoPre = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        // popula o combo de campos pré-definidos
                        ASPxComboBox combo = divTipoCampoSelecionado.FindControl("ddl_CPD_campoPre") as ASPxComboBox;
                        if (combo != null)
                        {
                            populaComboCampoPredefinido(combo);
                            if (codigoCampoPre != "")
                                combo.Value = int.Parse(codigoCampoPre);
                        }
                    }
                    else if (codigoTipoCampo == "LOO")
                    {
                        string codigoLista = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        // popula o combo de subformularios
                        ASPxComboBox combo = divTipoCampoSelecionado.FindControl("ddl_LOO_ListaPre") as ASPxComboBox;
                        if (combo != null)
                        {
                            populaComboListaLookup(combo);
                            if (codigoLista != "")
                                combo.Value = int.Parse(codigoLista);
                        }
                    }

                }
            }
        }
    }

    protected void btnSalvar_VAR_Click(object sender, EventArgs e)
    {
        string comandoSQL = "";
        string codigoCampo = hfControle.Get("codigoCampo").ToString();
        string codigoTipoCampo = hfControle.Get("CodigoTipoCampo").ToString();
        string codigoFormulario = hfControle.Get("cogigoFormulario").ToString();
        if (codigoTipoCampo == "VAR")
        {
            string tamanho = hfControle.Get("VAR_tamanho").ToString();
            comandoSQL = string.Format(
                @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Tam: {2}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, tamanho);
            dvVAR.Visible = true;
        }
        else if (codigoTipoCampo == "TXT")
        {
            string tamanho = hfControle.Get("TXT_tamanho").ToString();
            string linhas = hfControle.Get("TXT_linhas").ToString();
            string formatacao = hfControle.Get("TXT_formatacao").ToString();
            comandoSQL = string.Format(
                @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Tam: {2}{1}Lin: {3}{1}For:{4}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, tamanho, linhas, formatacao);
            dvTXT.Visible = true;
        }

        else if (codigoTipoCampo == "NUM")
        {
            string NUM_Minimo = hfControle.Get("NUM_Minimo").ToString();
            string NUM_Maximo = hfControle.Get("NUM_Maximo").ToString();
            string NUM_Precisao = hfControle.Get("NUM_Precisao").ToString();
            string NUM_Formato = hfControle.Get("NUM_Formato").ToString();
            comandoSQL = string.Format(
                @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Min: {2}{1}Max: {3}{1}Pre: {4}{1}For: {5}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, NUM_Minimo, NUM_Maximo, NUM_Precisao, NUM_Formato);
            dvNUM.Visible = true;
        }
        else if (codigoTipoCampo == "LST")
        {
            string LST_Opcoes = hfControle.Get("LST_Opcoes").ToString();
            string LST_Formatacao = hfControle.Get("LST_Formatacao").ToString();
            comandoSQL = string.Format(
                @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Opc: {2}{1}For: {3}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, LST_Opcoes, LST_Formatacao);
            dvLST.Visible = true;
        }
        else if (codigoTipoCampo == "DAT")
        {
            string DAT_IncluirHora = hfControle.Get("DAT_IncluirHora").ToString();
            string DAT_ValorInicial = hfControle.Get("DAT_ValorInicial").ToString();
            comandoSQL = string.Format(
                @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Hor: {2}{1}Ini: {3}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, DAT_IncluirHora, DAT_ValorInicial);
            dvDAT.Visible = true;
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
            dvBOL.Visible = true;
        }
        else if (codigoTipoCampo == "SUB")
        {
            string CodigoFormulario = hfControle.Get("SUB_CodigoFormulario").ToString();
            comandoSQL = string.Format(
                @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'Sub: {2}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, CodigoFormulario);
            dvSUB.Visible = true;
        }
        else if (codigoTipoCampo == "CPD")
        {
            string CodigoCampoPre = hfControle.Get("CPD_CampoPre").ToString();
            comandoSQL = string.Format(
                @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'CPD: {2}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, CodigoCampoPre);
            dvSUB.Visible = true;
        }
        else if (codigoTipoCampo == "LOO")
        {
            string CodigoListaPre = hfControle.Get("LOO_ListaPre").ToString();
            comandoSQL = string.Format(
                @"UPDATE CampoModeloFormulario
                     SET DefinicaoCampo = 'LOO: {2}{1}'
                   WHERE codigoCampo = {0} ", codigoCampo, DelimitadorPropriedadeCampo, CodigoListaPre);
            dvSUB.Visible = true;
        }
        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);

        // obtem o objeto "Grid" que está sendo editado.
        GridViewDetailRowTemplateContainer container = (sender as ASPxButton).NamingContainer.NamingContainer as GridViewDetailRowTemplateContainer;
        gvCampos_ = container.Grid;

        if (gvCampos_ != null)
        {
            populaGridCamposFormulario(int.Parse(objCodigo.ToString()));
            gvCampos_.DataBind();
            //dvVAR.Visible = true;
            gvCampos_.DetailRows.CollapseAllRows();
        }
    }

  /*  private int getLinhaAberta(ASPxGridView grid)
    {
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            if (grid.DetailRows.IsVisible(i))
                return i;
        }
        return -1;
    }*/
    protected void gvCampos_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {/*
        ASPxComboBox combo = e.Editor as ASPxComboBox;
        //int xx = ((GridViewDataComboBoxColumn)gvCampos_.Columns["Aba"]).PropertiesComboBox.Items.Count;
        if (combo != null)
            combo.DataBind();
        */
    }

    protected void gvFormularios_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {/*
        (gvFormularios.FindEditFormTemplateControl("btnSalvarFormulario") as ASPxButton).ToolTip = "Incluir o novo formulário";
        (gvFormularios.FindEditFormTemplateControl("txtDescricaoFormulario") as ASPxTextBox).Text = "Salvar as alterações";
      */ 
/*        ASPxPanel pnAplicacaoFormulario = gvFormularios.FindEditFormTemplateControl("pnAplicacaoFormulario") as ASPxPanel;
        // insere os tipos de aplicação disponível
        tipoProjeto(ref pnAplicacaoFormulario);*/
    }

    protected void gvFormularios_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        
      /*  (gvFormularios.FindEditFormTemplateControl("btnSalvarFormulario") as ASPxButton).ToolTip = "Salvar as alterações";
        (gvFormularios.FindEditFormTemplateControl("txtDescricaoFormulario") as ASPxTextBox).Text = "Salvar as alterações";
        ASPxPanel pnAplicacaoFormulario = gvFormularios.FindEditFormTemplateControl("pnAplicacaoFormulario") as ASPxPanel;
        // insere os tipos de aplicação disponível
        tipoProjeto(ref pnAplicacaoFormulario);

        ASPxButton xx = new ASPxButton();
        xx.Text = "sdfds";
        pnAplicacaoFormulario.Controls.Add(xx);
        pnAplicacaoFormulario.Border.BorderWidth = new Unit("2px");
        pnAplicacaoFormulario.Border.BorderColor = Color.Red;
        */
        
    }

    private void tipoProjeto(ref ASPxPanel pnAplicacaoFormulario)
    {
        CheckBoxList cblAplicacao = pnAplicacaoFormulario.FindControl("cblAplicacao") as CheckBoxList;
        //tem que pegar o codigo da empresa correto.
        DataSet ds = cDados.getTipoProjeto(1);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ASPxCheckBox check = new ASPxCheckBox();
                check.Text = dr["TipoProjeto"].ToString();
                check.Value = dr["CodigoTipoProjeto"].ToString();
                pnAplicacaoFormulario.Controls.Add(check);
                //cblAplicacao.Items.Add(dr["TipoProjeto"].ToString());
            }
        }
    }

    protected void gvFormularios_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.NewValues["NomeFormulario"] == null || e.NewValues["NomeFormulario"].ToString() == "")
        {
            AddError(e.Errors, gvFormularios.Columns["NomeFormulario"], "O nome do formulário é de preenchimento obrigatório.");
        }
        if (e.NewValues["CodigoTipoFormulario"] == null || e.NewValues["CodigoTipoFormulario"].ToString() == "")
        {
            AddError(e.Errors, gvFormularios.Columns["CodigoTipoFormulario"], "O tipo do formulário é de preenchimento obrigatório.");
        }

        if (e.Errors.Count > 0)
            e.RowError = "Preencha todos os campos obrigatórios.";
    }

    void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
    {
        if (errors.ContainsKey(column)) return;
        errors[column] = errorText;
    }

    protected void gvCampos_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (e.NewValues["NomeCampo"] == null || e.NewValues["NomeCampo"].ToString() == "")
        {
            AddError(e.Errors, grid.Columns["NomeCampo"], "O nome do campo é de preenchimento obrigatório.");
        }
        if (e.NewValues["CampoObrigatorio"] == null || e.NewValues["CampoObrigatorio"].ToString() == "")
        {
            AddError(e.Errors, grid.Columns["CampoObrigatorio"], "O preenchimento do campo é de preenchimento obrigatório.");
        }
        if (e.NewValues["CodigoTipoCampo"] == null || e.NewValues["CodigoTipoCampo"].ToString() == "")
        {
            AddError(e.Errors, grid.Columns["CodigoTipoCampo"], "O tipo do campo é de preenchimento obrigatório.");
        }

        if (e.Errors.Count > 0)
            e.RowError = "Preencha todos os campos obrigatórios.";
    }

    protected void btnNovoFormulario_Click(object sender, EventArgs e)
    {
        gvFormularios.AddNewRow();
    }

    protected void btnNovoCampo_Click(object sender, EventArgs e)
    {
        // obtem o objeto "Grid" que está sendo editado.
        GridViewDetailRowTemplateContainer container = (sender as ASPxButton).NamingContainer as GridViewDetailRowTemplateContainer;
        foreach (Control grid in container.Controls)
        {
            if ((grid as ASPxGridView) != null)
            {
                (grid as ASPxGridView).AddNewRow();
            }
        }
    }

    protected void btnSalvarFormulario_Click(object sender, EventArgs e)
    {
        ASPxPanel pnAplicacaoFormulario = gvFormularios.FindEditFormTemplateControl("pnAplicacaoFormulario") as ASPxPanel;
    }
}


/*
<!--   <EditForm> 
                <table border="0" cellpadding="0" cellspacing="0" style="width: 800px" id="tbEditFormACG">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="width: 400px">
                                        <dxe:ASPxLabel ID="ASPxLabel20" runat="server" Text="Nome do Formulário">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Text="Tipo do Formulário">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 400px">
                                        <dxe:ASPxTextBox ID="txtNomeFormulario" runat="server" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                            CssPostfix="Aqua" Width="380px" Text = '<%# Eval("NomeFormulario")%>'>
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmbTipoFormulario" runat="server" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                            CssPostfix="Aqua" ImageFolder="~/App_Themes/Aqua/{0}/"   ShowShadow="False"
                                            ValueType="System.String" Width="380px">
                                            <ButtonEditEllipsisImage Height="3px" Url="~/App_Themes/Aqua/Editors/edtEllipsis.png"
                                                UrlDisabled="~/App_Themes/Aqua/Editors/edtEllipsisDisabled.png" UrlHottracked="~/App_Themes/Aqua/Editors/edtEllipsisHottracked.png"
                                                UrlPressed="~/App_Themes/Aqua/Editors/edtEllipsisPressed.png" />
                                            <DropDownButton>
                                                <Image Height="7px" Url="~/App_Themes/Aqua/Editors/edtDropDown.png" UrlDisabled="~/App_Themes/Aqua/Editors/edtDropDownDisabled.png"
                                                    UrlHottracked="~/App_Themes/Aqua/Editors/edtDropDownHottracked.png" UrlPressed="~/App_Themes/Aqua/Editors/edtDropDownPressed.png" />
                                            </DropDownButton>
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel22" runat="server" Text="Descrição">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxTextBox ID="txtDescricaoFormulario" runat="server" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                            CssPostfix="Aqua" Width="780px">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="width: 400px">
                                        <dxe:ASPxLabel ID="ASPxLabel23" runat="server" Text="Abas">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel24" runat="server" Text="Aplicação">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="left" style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 400px">
                                        <dxe:ASPxMemo ID="txtAbas" runat="server" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                            CssPostfix="Aqua" Rows="6" Width="380px">
                                            <ValidationSettings>
                                                <ErrorImage Url="~/App_Themes/Aqua/Editors/edtError.png" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dxe:ASPxMemo>
                                    </td>
                                    <td>
                                        &nbsp;<dxp:ASPxPanel ID="pnAplicacaoFormulario" runat="server" Width="200px">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <asp:CheckBoxList ID="cblAplicacao" runat="server">
                                        </asp:CheckBoxList></td>
                                    <td align="left" style="width: 100px">
                                        <dxe:ASPxButton ID="btnSalvarFormulario" runat="server" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                            CssPostfix="Aqua" Text="Salvar" Width="80px" OnClick="btnSalvarFormulario_Click">
                                        </dxe:ASPxButton>
                                        <br />
                                        <dxe:ASPxButton ID="btnCancelarFormulario" runat="server" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                            CssPostfix="Aqua" Text="Cancelar" Width="80px">
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </EditForm> -->
*/
