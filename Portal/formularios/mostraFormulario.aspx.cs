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
using System.Drawing;
using System.Collections.Specialized;
using DevExpress.Web;
using DevExpress.Web.ASPxHtmlEditor;

public partial class formularios_mostraFormulario : System.Web.UI.Page
{
    private char DelimitadorPropriedadeCampo = '¥';

    dados cDados;
    string cssFilePath = "~/App_Themes/Aqua/{0}/styles.css";
    string cssPostFix = "Aqua";
    int codigoModeloFormulario;
    int codigoFormularioMaster;
    DataSet dsCampos;

    protected void Page_Load(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);

        if (Request.QueryString["CM"] != null && Request.QueryString["CM"] != "" && int.Parse(Request.QueryString["CM"].ToString()) > 0)
        {
            codigoModeloFormulario = int.Parse(Request.QueryString["CM"].ToString());

            if (!IsPostBack)
            {
                if (Request.QueryString["CF"] != null && Request.QueryString["CF"] != "" && int.Parse(Request.QueryString["CF"].ToString()) > 0)
                    hfCodigoFormularioMaster.Value = Request.QueryString["CF"].ToString();
                else
                    hfCodigoFormularioMaster.Value = "-1";
            }
            codigoFormularioMaster = int.Parse(hfCodigoFormularioMaster.Value.ToString());

            inicializaFormulario();
            renderizaFormulario();
            if (codigoFormularioMaster > 0)
            {
                ASPxPageControl pcFormulario = new ASPxPageControl();
                pcFormulario = (ASPxPageControl)pnCampos.FindControl("pcFormulario");
                getConteudoFormularioMaster(pcFormulario);
            }
        }
    }

    private void inicializaFormulario()
    {
        ASPxPageControl pcFormulario = new ASPxPageControl();
        pcFormulario.ID = "pcFormulario";
        pcFormulario.Width = new Unit("100%");
        pcFormulario.CssFilePath = cssFilePath;
        pcFormulario.CssPostfix = cssPostFix;
        pcFormulario.TabPages.Clear();
        DataSet ds = cDados.getDataSet("Select * from modeloFormulario where codigoModeloFormulario = " + codigoModeloFormulario);
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            lblNomeFormulario.Text = dt.Rows[0]["NomeFormulario"].ToString();
            lblDescricaoFormulario.Text = dt.Rows[0]["DescricaoFormulario"].ToString();
            string[] aAbas = dt.Rows[0]["Abas"].ToString().Split('\r');
            foreach (string aba in aAbas)
            {
                if (aba != "")
                    pcFormulario.TabPages.Add(aba.Replace("\n", ""));
            }
        }
        pnCampos.Controls.Add(pcFormulario);
    }

    private Literal getLiteral(string texto)
    {
        Literal myLiteral = new Literal();
        myLiteral.Text = texto;
        return myLiteral;
    }

    private DataSet getCampoModeloFormulario(int codigoModeloFormulario, int aba)
    {
        string filtroAba = "";
        if (aba >= 0)
            filtroAba = " AND Aba = " + aba;

        string comandoSQL = string.Format(
            @"SELECT CodigoCampo, NomeCampo, DescricaoCampo, CampoObrigatorio, CodigoTipoCampo, DefinicaoCampo, OrdemCampoFormulario, Aba
                FROM campomodeloFormulario
               WHERE codigoModeloFormulario = {0}
                 AND dataExclusao is null
                 {1}
               ORDER BY Aba, OrdemCampoFormulario", codigoModeloFormulario, filtroAba);

        DataSet ds = cDados.getDataSet(comandoSQL);
        return ds;
    }

    private void renderizaFormulario()
    {
        ASPxPageControl pcFormulario = new ASPxPageControl();
        pcFormulario = (ASPxPageControl)pnCampos.FindControl("pcFormulario");

        int margemFormulario = 2;
        for (int aba = 0; aba <= pcFormulario.TabPages.Count - 1; aba++)
        {
            dsCampos = getCampoModeloFormulario(codigoModeloFormulario, aba);
            DataTable dtCampos = dsCampos.Tables[0];
            pcFormulario.TabPages[aba].Controls.Add(getLiteral(
                string.Format(
                    @"<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-color:Green;width:100%"">
                    <tr>
                        <td style=""width:{0}px;""></td>
                        <td></td>
                    </tr>", margemFormulario)));

            // adiciona os campos
            foreach (DataRow dr in dtCampos.Rows)
            {
                pcFormulario.TabPages[aba].Controls.Add(getLiteral(
                    string.Format(@"<tr><td style=""width:{0}px;""></td><td>", margemFormulario)));
                pcFormulario.TabPages[aba].Controls.Add(renderizaCampo(dr));
                pcFormulario.TabPages[aba].Controls.Add(getLiteral("<p>"));
                pcFormulario.TabPages[aba].Controls.Add(getLiteral("</td></tr>"));
            }
            pcFormulario.TabPages[aba].Controls.Add(getLiteral("</table>"));
        }
    }

    private Control renderizaCampo(DataRow drCampo)
    {
        string larguraCampo = "99%";
        string idCampo = "id_" + drCampo["CodigoCampo"].ToString();
        idCampo = drCampo["CodigoTipoCampo"].ToString() + drCampo["CodigoCampo"].ToString();
        string nomeCampo = drCampo["NomeCampo"].ToString();
        string descricaoCampo = drCampo["DescricaoCampo"].ToString();
        string definicaoCampo = drCampo["DefinicaoCampo"].ToString();
        string codigoTipoCampo = drCampo["CodigoTipoCampo"].ToString();
        string[] aDefinicaoCampo = definicaoCampo.Split(DelimitadorPropriedadeCampo);

        Panel pnExterno = new Panel();
        pnExterno.ID = "pnExterno_" + idCampo;

        int margemCampo = 5;
        pnExterno.Controls.Add(getLiteral(
            string.Format(
                @"<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-color:Red;width:100%"">
                    <tr>
                        <td style=""width:{0}px;""></td>
                        <td></td>
                    </tr>", margemCampo)));

        // adiciona o nome do campo
        pnExterno.Controls.Add(getLiteral(
            string.Format(@"<tr><td style=""width:{0}px;""></td><td valign=""top"">", margemCampo)));
        ASPxLabel lblTitulo = new ASPxLabel();
        lblTitulo.Text = nomeCampo;
        lblTitulo.CssFilePath = cssFilePath;
        lblTitulo.Font.Bold = true;
        pnExterno.Controls.Add(lblTitulo);
        pnExterno.Controls.Add(getLiteral(" "));
        System.Web.UI.WebControls.Image imgAjudaTitulo = new System.Web.UI.WebControls.Image();
        imgAjudaTitulo.ImageUrl = "~/imagens/ajuda.png";
        imgAjudaTitulo.ToolTip = descricaoCampo;
        imgAjudaTitulo.Style.Add("vertical-align","top");
        imgAjudaTitulo.Style.Add("cursor", "pointer");
        pnExterno.Controls.Add(imgAjudaTitulo);
        pnExterno.Controls.Add(getLiteral("</td></tr>"));

        pnExterno.Controls.Add(getLiteral(
            string.Format(@"<tr><td style=""width:{0}px;""></td><td valign=""top"">", margemCampo)));

        Control controle = null;
        if (codigoTipoCampo == "VAR")
        {
            controle = renderizaCampoVar(aDefinicaoCampo, larguraCampo);
        }
        else if (codigoTipoCampo == "TXT")
        {
            controle = renderizaCampoTXT(aDefinicaoCampo, larguraCampo); 
        }
        else if (codigoTipoCampo == "NUM")
        {
            controle = renderizaCampoNUM(aDefinicaoCampo, larguraCampo);
        }
        else if (codigoTipoCampo == "LST")
        {
            controle = renderizaCampoLST(aDefinicaoCampo, larguraCampo);
        }
        else if (codigoTipoCampo == "DAT")
        {
            controle = renderizaCampoDAT(aDefinicaoCampo, larguraCampo);
        }
        else if (codigoTipoCampo == "SUB")
        {
            controle = renderizaCampoSUB(aDefinicaoCampo, larguraCampo);
        }
        controle.ID = idCampo;
        pnExterno.Controls.Add(controle);
        pnExterno.Controls.Add(getLiteral("</td></tr>"));
        pnExterno.Controls.Add(getLiteral("</table>"));
        return pnExterno;
    }

    private Control renderizaCampoVar(string[] aDefinicaoCampo, string larguraCampo)
    {
        string tamanho = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();

        ASPxTextBox controle = new ASPxTextBox();
        controle.CssFilePath = cssFilePath;
        controle.CssPostfix = cssPostFix;
        controle.Width = new Unit(larguraCampo);
        controle.MaxLength = int.Parse(tamanho);
        return controle;
    }

    private Control renderizaCampoTXT(string[] aDefinicaoCampo, string larguraCampo)
    {
        string tamanho = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
        string linhas = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
        string formato = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();

        Control controle = null;
        if (formato == "0")
        {
            controle = new ASPxMemo();
            ((ASPxMemo)controle).CssFilePath = cssFilePath;
            ((ASPxMemo)controle).CssPostfix = cssPostFix;
            ((ASPxMemo)controle).Width = new Unit(larguraCampo);
            ((ASPxMemo)controle).Rows = int.Parse(linhas);
        }

        else if (formato == "1" || formato == "2")
        {
            int alturaMinima = 350;
            controle = new ASPxHtmlEditor();
            ((ASPxHtmlEditor)controle).Width = new Unit("99%");
            ((ASPxHtmlEditor)controle).Toolbars.CreateDefaultToolbars();
            ((ASPxHtmlEditor)controle).Toolbars[0].Visible = (formato == "2");
            ((ASPxHtmlEditor)controle).Settings.AllowHtmlView = false;
            ((ASPxHtmlEditor)controle).Height = alturaMinima;
            ((ASPxHtmlEditor)controle).CssFilePath = cssFilePath;
            ((ASPxHtmlEditor)controle).CssPostfix = cssPostFix;
        }
        return controle;
    }

    private Control renderizaCampoNUM(string[] aDefinicaoCampo, string larguraCampo)
    {
        string minimo = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
        string maximo = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
        string precisao = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
        string formato = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();

        if (minimo == "")
            minimo = "-999999999999";
        if (maximo == "")
            maximo = "999999999999";
        if (precisao == "1")
            precisao = ".<0..9>";
        else if (precisao == "2")
            precisao = ".<00..99>";
        else if (precisao == "3")
            precisao = ".<000..999>";
        else if (precisao == "4")
            precisao = ".<0000..9999>";
        else if (precisao == "5")
            precisao = ".<00000..99999>";
        else
            precisao = "";

        ASPxTextBox controle = new ASPxTextBox();
        controle.CssFilePath = cssFilePath;
        controle.CssPostfix = cssPostFix;
        controle.Width = new Unit("170px");
        controle.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.None;
        controle.MaskSettings.Mask = string.Format("$<{0}..{1}g>{2}", minimo, maximo, precisao);

        return controle;
    }

    private Control renderizaCampoLST(string[] aDefinicaoCampo, string larguraCampo)
    {
        string opcoes = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
        opcoes = opcoes.Replace("\r\n", "\r");
        string[] aOpcoes = opcoes.Split('\r');
        string formato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();

        Control controle = null;
        if (formato == "0")// comboBox
        {
            controle = new ASPxComboBox();
            ((ASPxComboBox)controle).CssFilePath = cssFilePath;
            ((ASPxComboBox)controle).CssPostfix = cssPostFix;
            foreach (string item in aOpcoes)
            {
                ((ASPxComboBox)controle).Items.Add(item);
            }
        }
        else if (formato == "1")// radio button
        {
            controle = new ASPxRadioButtonList();
            ((ASPxRadioButtonList)controle).CssFilePath = cssFilePath;
            ((ASPxRadioButtonList)controle).CssPostfix = cssPostFix;
            ((ASPxRadioButtonList)controle).Border.BorderStyle = BorderStyle.None;
            foreach (string item in aOpcoes)
            {
                ((ASPxRadioButtonList)controle).Items.Add(item);
            }
        }
        else if (formato == "2")// check box
        {
            controle = new CheckBoxList();
            foreach (string item in aOpcoes)
            {
                ((CheckBoxList)controle).Items.Add(item);
            }
        }
        else
        {
            controle = new TextBox();
            ((TextBox)controle).ReadOnly = true;
        }

        return controle;
    }

    private Control renderizaCampoDAT(string[] aDefinicaoCampo, string larguraCampo)
    {
        string incluirHora = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
        string valorInicial = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
        ASPxDateEdit controle = new ASPxDateEdit();
        controle.CssFilePath = cssFilePath;
        controle.CssPostfix = cssPostFix;
        controle.DisplayFormatString = "dd/MM/yyyy hh:mm:ss";
        if (incluirHora == "S")
            controle.EditFormat = EditFormat.DateTime;
        if (valorInicial == "A")
            controle.Date = DateTime.Today;

        return controle;
    }

    private Control renderizaCampoSUB(string[] aDefinicaoCampo, string larguraCampo)
    {
        string codigoFormulario = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
        ASPxGridView controle = new ASPxGridView();
        controle.ID = "gv_form_" + codigoModeloFormulario;
        controle.CssFilePath = cssFilePath;
        controle.CssPostfix = cssPostFix;
        controle.SettingsEditing.EditFormColumnCount = 3;
        controle.KeyFieldName = "CodigoFormulario";
        controle.Width = new Unit("99%");
        controle.CellEditorInitialize += ASPxGridView1_CellEditorInitialize;
        controle.RowInserting += ASPxGridView1_RowInserting;

        GridViewCommandColumn column = new GridViewCommandColumn();
        column.ShowNewButton = true;
        column.ShowEditButton = true;
        controle.Columns.Add(column);


        GridViewDataTextColumn columnKey = new GridViewDataTextColumn();
        columnKey.Visible = false;
        columnKey.FieldName = "CodigoFormulario";
        controle.Columns.Add(columnKey);

        DataSet dsCamposSub = getCampoModeloFormulario(int.Parse(codigoFormulario), -1);
        DataTable dtSub = new DataTable();
        dtSub.Columns.Add("CodigoFormulario", Type.GetType("System.Int32"));
        
        //adiciona as colunas
        foreach (DataRow dr in dsCamposSub.Tables[0].Rows)
        {
            string definicaoCampoSUB = dr["definicaoCampo"].ToString();
            string[] aDefinicaoCampoSUB = definicaoCampoSUB.Split(DelimitadorPropriedadeCampo);
            string fieldName = dr["codigoTipoCampo"].ToString() + dr["CodigoCampo"].ToString();
            Type fieldType = Type.GetType("System.String");

            GridViewColumn newColumn = null;
            if (dr["codigoTipoCampo"].ToString() == "DA" || dr["codigoTipoCampo"].ToString() == "TXT" || dr["codigoTipoCampo"].ToString() == "VAR" || dr["codigoTipoCampo"].ToString() == "NUM")
            {
                newColumn = new GridViewDataTextColumn(); // "campo_" + 
                
                ((GridViewDataTextColumn)newColumn).FieldName = fieldName;
                
            }
            else if (dr["codigoTipoCampo"].ToString() == "LST")
            {
                string opcoes = aDefinicaoCampoSUB[0].Substring(aDefinicaoCampoSUB[0].IndexOf(":") + 1).Trim();
                string formato = aDefinicaoCampoSUB[1].Substring(aDefinicaoCampoSUB[1].IndexOf(":") + 1).Trim();
                if (formato == "0" || formato == "1")
                {
                    opcoes = opcoes.Replace("\r\n", "\r");
                    string[] aOpcoes = opcoes.Split('\r');
                    newColumn = new GridViewDataComboBoxColumn();
                    ((GridViewDataComboBoxColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.DataSource = getTableOpcoesLista(opcoes);
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValueField = "codigo";
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.TextField = "descricao";
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValueType = Type.GetType("System.String");
                }
                else
                {
                    newColumn = new GridViewDataTextColumn();
                    ((GridViewDataTextColumn)newColumn).FieldName = fieldName;
                }
            }
            else if (dr["codigoTipoCampo"].ToString() == "DAT")
            {
                fieldType = Type.GetType("System.DateTime");
                newColumn = new GridViewDataDateColumn();
                ((GridViewDataDateColumn)newColumn).FieldName = fieldName;
            }

            if (newColumn != null)
            {
                dtSub.Columns.Add(fieldName, fieldType);
                newColumn.Caption = dr["NomeCampo"].ToString();
                newColumn.Name = dr["NomeCampo"].ToString();
                controle.Columns.Add(newColumn);
            }
        }

        // busca os valores para os campos do subFormularios
        dtSub = getConteudoSubFormulario(dtSub);
        controle.DataSource = dtSub;
        controle.DataBind();

        return controle;
    }

    private void getConteudoFormularioMaster(Control Container)
    {
        string comandoSQL = string.Format(
            @"SELECT CF.CodigoFormulario, CF.CodigoCampo, CMF.CodigoTipoCampo, CF.valorNum, 
                     CF.ValorDat, CF.ValorVar, CF.ValorTxt, CF.ValorBol, CMF.DefinicaoCampo
                FROM campoFormulario CF inner join
                     campomodeloFormulario CMF on CF.codigoCampo = CMF.codigoCampo 
               WHERE CF.codigoFormulario = {0}
                 AND dataExclusao is null
               ORDER BY Aba, OrdemCampoFormulario", codigoFormularioMaster);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                string nomeCampo = dr["codigoTipoCampo"].ToString() + dr["CodigoCampo"].ToString();
                Control controle = Container.FindControl(nomeCampo);
                if (controle != null)
                {
                    string colunaCampo = "Valor" + dr["codigoTipoCampo"].ToString();
                    string definicaoCampo = dr["DefinicaoCampo"].ToString();
                    string[] aDefinicaoCampo = definicaoCampo.Split(DelimitadorPropriedadeCampo);

                    if (dr["codigoTipoCampo"].ToString() == "VAR" || dr["codigoTipoCampo"].ToString() == "NUM")
                    {
                        ((ASPxTextBox)controle).Text = dr[colunaCampo].ToString();
                    }
                    else if (dr["codigoTipoCampo"].ToString() == "TXT")
                    {
                        string formato = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                        if (formato == "0")
                            ((ASPxMemo)controle).Text = dr[colunaCampo].ToString();
                        else
                            ((ASPxHtmlEditor)controle).Html = dr[colunaCampo].ToString();
                    }
                    else if (dr["codigoTipoCampo"].ToString() == "DAT")
                    {
                        ((ASPxDateEdit)controle).Value = dr[colunaCampo];
                    }
                    else if (dr["codigoTipoCampo"].ToString() == "LST")
                    {
                        colunaCampo = "ValorVAR";
                        string formato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        if (formato == "0")
                            ((ASPxComboBox)controle).Text = dr[colunaCampo].ToString();
                        else if(formato == "1")
                            ((ASPxRadioButtonList)controle).Value = dr[colunaCampo].ToString();
                        else if (formato == "2")
                            ((CheckBoxList)controle).Text = dr[colunaCampo].ToString();
                    }

                }
            }
        }
    }

    private DataTable getConteudoSubFormulario(DataTable dataTableReferencia)
    {
        string comandoSQL = string.Format(
            @"SELECT CF.CodigoFormulario, CF.CodigoCampo, CMF.CodigoTipoCampo, CF.valorNum, 
                     CF.ValorDat, CF.ValorVar, CF.ValorTxt, CF.ValorBol
                FROM linkFormulario LF  inner join
                     campoFormulario CF on CF.codigoFormulario = LF.CodigoSubFormulario inner join
                     campomodeloFormulario CMF on CF.codigoCampo = CMF.codigoCampo 
               WHERE LF.codigoFormulario = {0}
                 AND dataExclusao is null
               ORDER BY LF.CodigoSubFormulario, Aba, OrdemCampoFormulario", codigoFormularioMaster);
        DataSet ds = cDados.getDataSet(comandoSQL);
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            dataTableReferencia.Rows.Clear();
            int codigoFormulario = 0;
            int linha = -1;
            foreach (DataRow dr in dt.Rows)
            {
                if (int.Parse(dr["CodigoFormulario"].ToString()) != codigoFormulario)
                {
                    codigoFormulario = int.Parse(dr["CodigoFormulario"].ToString());
                    dataTableReferencia.Rows.Add();
                    dataTableReferencia.Rows[++linha]["CodigoFormulario"] = codigoFormulario;
                }
                string nomeCampo = dr["codigoTipoCampo"].ToString() + dr["CodigoCampo"].ToString();
                string colunaCampo = "Valor" + dr["codigoTipoCampo"].ToString();
                if (dr["codigoTipoCampo"].ToString() == "LST")
                {
                    colunaCampo = "ValorVAR";
                }

                dataTableReferencia.Rows[linha][nomeCampo] = dr[colunaCampo];
            }

            return dataTableReferencia;
        }
        else
            return null;
    }

    private DataTable getTableOpcoesLista(string opcoes)
    {
        opcoes = opcoes.Replace("\r\n", "\r");
        string[] aOpcoes = opcoes.Split('\r');

        DataTable dt = new DataTable();
        dt.Columns.Add("codigo");
        dt.Columns.Add("descricao");
        foreach (string item in aOpcoes)
        {
            dt.Rows.Add(item, item);
        }
        return dt;
    }

    private object getValorCampo(Control controle)
    {
        string tipoControle = controle.ToString().Replace("DevExpress.Web.ASPxEditors.", "");
        tipoControle = tipoControle.Replace("DevExpress.Web.ASPxHtmlEditor.", "");

        if (tipoControle == "ASPxTextBox")
            return ((ASPxTextBox)controle).Text;
        else if (tipoControle == "ASPxComboBox")
            return ((ASPxComboBox)controle).Text;
        else if (tipoControle == "ASPxMemo")
            return ((ASPxMemo)controle).Text;
        else if (tipoControle == "ASPxRadioButtonList")
        {
            if (((ASPxRadioButtonList)controle).Value != null)
                return ((ASPxRadioButtonList)controle).Value.ToString();
            else
                return "";
        }
        else if (tipoControle == "ASPxDateEdit")
        {
            return AjustaValorCampoData(((ASPxDateEdit)controle).Date);
        }
        else if (tipoControle == "ASPxHtmlEditor")
            return ((ASPxHtmlEditor)controle).Html;

        return "";

    }

    private string AjustaValorCampoData(DateTime data)
    {
        string dia = data.Day.ToString();
        string mes = data.Month.ToString();
        string ano = data.Year.ToString();
        string hora = data.TimeOfDay.ToString();
        return string.Format("{0:n2}/{1:n2}/{2} {3}", dia, mes, ano, hora);
    }

    private string getNomeValorCampo(string tipoCampo)
    {
        if (tipoCampo == "VAR")
            return "ValorVarchar";
        else if (tipoCampo == "NUM")
            return "ValorNumero";
        else if (tipoCampo == "DAT")
            return "ValorData";
        else if (tipoCampo == "TXT")
            return "ValorText";
        else
            return "";
    }

    protected void btnSalvar1_Click(object sender, EventArgs e)
    {
        ASPxPageControl pcFormulario = new ASPxPageControl();
        pcFormulario = (ASPxPageControl)pnCampos.FindControl("pcFormulario");
        System.Collections.Specialized.OrderedDictionary valores = new OrderedDictionary();
        // lê no formulário os valores associados aos campos
        dsCampos = getCampoModeloFormulario(codigoModeloFormulario, -1);
        foreach (DataRow dr in dsCampos.Tables[0].Rows)
        {
            string idCampo = dr["CodigoTipoCampo"].ToString() + dr["CodigoCampo"].ToString();
            if (dr["CodigoTipoCampo"].ToString() != "SUB")
            {
                Control controle = pcFormulario.TabPages[int.Parse(dr["Aba"].ToString())].FindControl(idCampo);
                if (controle != null)
                {
                    string key = idCampo;// dr["CodigoTipoCampo"].ToString() + dr["CodigoCampo"].ToString();
                    string value = getValorCampo(controle).ToString();
                    valores.Add(key, value);
                }
            }
        }

        if (codigoFormularioMaster == -1)
        {
            int tempFormularioMaster = -1;
            codigoFormularioMaster = insereFormulario(false, false, codigoModeloFormulario, lblDescricaoFormulario.Text, 1, valores, ref tempFormularioMaster);
            hfCodigoFormularioMaster.Value = codigoFormularioMaster.ToString();
        }
        else
        {
            atualizaFormulario(lblDescricaoFormulario.Text, 1, valores);
        }
    }

    protected void btnCancelar1_Click(object sender, EventArgs e)
    {

    }

    private int insereFormulario(bool isSubForm, bool inserirFormularioMaster, int codigoModeloFormulario, string descricaoFormulario, int codigoUsuarioInclusao, OrderedDictionary valoresCampos, ref int codigoFormularioMaster)
    {
        string comandoSQL;

        string comandoSQLMaster = "";
        if (inserirFormularioMaster)
        {
            if (codigoFormularioMaster <= 0)
            {
                comandoSQLMaster = string.Format(
                    @"
                      INSERT INTO Formulario (CodigoModeloFormulario, DescricaoFormulario, DataInclusao, IncluidoPor, DataExclusao)
                           VALUES ({0}, '{1}', getdate(), {2}, getdate())

                      SELECT @CodigoFormularioMaster = scope_identity()

                 ", codigoModeloFormulario, descricaoFormulario, codigoUsuarioInclusao);
            }
            else
            {
                comandoSQLMaster = string.Format(
                    @"
                        SELECT @CodigoFormularioMaster = {0}

                     ", codigoFormularioMaster);
            }
            comandoSQLMaster +=
                    @"
                        INSERT INTO linkFormulario
                            VALUES (@CodigoFormularioMaster, @CodigoFormulario )

                     ";

        }
        comandoSQL = string.Format(
            @"BEGIN
                DECLARE @CodigoFormularioMaster bigint
                DECLARE @CodigoFormulario bigint    
                  
                SET @CodigoFormularioMaster = -1;

                INSERT INTO Formulario (CodigoModeloFormulario, DescricaoFormulario, DataInclusao, IncluidoPor)
                    VALUES ({1}, '{2}', getdate(), {3}) 

                SET @CodigoFormulario = scope_identity()

                {0} 

              ", comandoSQLMaster, codigoModeloFormulario, descricaoFormulario, codigoUsuarioInclusao);

        foreach (DictionaryEntry valor in valoresCampos)
        {
            string tipoCampo = valor.Key.ToString().Substring(0, 3);
            string codigoCampo = valor.Key.ToString().Substring(3);
            string valorCampo = valor.Value.ToString().Trim();

            // os campos "LST" são salvos como se fossem "VAR"
            if (tipoCampo == "LST")
                tipoCampo = "VAR";

            // se não for campo texto, tem que colocar as aspas
            if (tipoCampo == "VAR" || tipoCampo == "TXT")
                valorCampo = "'" + valorCampo + "'";

            // se for campo data, tem que fazer o convert
            if (tipoCampo == "DAT")
            {
                DateTime Data;
                Data = DateTime.Parse(valorCampo);
                valorCampo = AjustaValorCampoData(Data);
                valorCampo = " convert(datetime, '" + valorCampo + "', 103)";
            }

            comandoSQL += string.Format(
                   @"INSERT INTO CampoFormulario (CodigoFormulario, codigoCampo, Valor{0} )
                            VALUES (@CodigoFormulario, {1}, {2} )

                        ", tipoCampo, codigoCampo, valorCampo);
        }
        comandoSQL +=
            @"      SELECT @CodigoFormulario as CodigoFormulario, 
                           @CodigoFormularioMaster as CodigoFormularioMaster

                END";

        DataTable dtRet = cDados.getDataSet(comandoSQL).Tables[0];
        int codigoFormulario = int.Parse(dtRet.Rows[0]["CodigoFormulario"].ToString());
        codigoFormularioMaster = int.Parse(dtRet.Rows[0]["CodigoFormularioMaster"].ToString());
        

        return codigoFormulario;
    }

    private int atualizaFormulario(string descricaoFormulario, int codigoUsuarioAtualizacao, OrderedDictionary valoresCampos)
    {
        string comandoSQL;
        comandoSQL = string.Format(
            @"BEGIN
                DECLARE @CodigoFormularioMaster bigint
                DECLARE @CodigoFormulario bigint    
                  
                SET @CodigoFormularioMaster = -1;

                UPDATE Formulario 
                   SET DescricaoFormulario = '{2}'
                     , DataUltimaAlteracao = getdate()
                     , AlteradoPor = {2}
                 WHERE codigoFormulario = {0}
                
              ", codigoFormularioMaster, descricaoFormulario, codigoUsuarioAtualizacao);

        foreach (DictionaryEntry valor in valoresCampos)
        {
            string tipoCampo = valor.Key.ToString().Substring(0, 3);
            string codigoCampo = valor.Key.ToString().Substring(3);
            string valorCampo = valor.Value.ToString().Trim();

            // os campos "LST" são salvos como se fossem "VAR"
            if (tipoCampo == "LST")
                tipoCampo = "VAR";

            // se não for campo texto, tem que colocar as aspas
            if (tipoCampo == "VAR" || tipoCampo == "TXT")
                valorCampo = "'" + valorCampo + "'";

            // se for campo data, tem que fazer o convert
            if (tipoCampo == "DAT")
            {
                DateTime Data;
                Data = DateTime.Parse(valorCampo);

                valorCampo = AjustaValorCampoData(Data);
                valorCampo = " convert(datetime, '" + valorCampo + "', 103)";
            }

            comandoSQL += string.Format(
                   @"UPDATE CampoFormulario 
                        SET Valor{2} = {3}
                      WHERE codigoFormulario = {0}
                        AND codigoCampo = {1}

                        ", codigoFormularioMaster, codigoCampo, tipoCampo, valorCampo);
        }
        comandoSQL +=
            @"    
                END";

        int afetados = 0;
        cDados.execSQL(comandoSQL, ref afetados);

        return afetados;
    }
    
    protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxComboBox combo = e.Editor as ASPxComboBox;
        if (combo != null)
            combo.DataBind();
    }

    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        insereFormulario(true, true, codigoModeloFormulario, lblDescricaoFormulario.Text, 1, e.NewValues, ref codigoFormularioMaster);
        hfCodigoFormularioMaster.Value = codigoFormularioMaster.ToString();
        
        DataTable dtSub = (sender as ASPxGridView).DataSource as DataTable;
        dtSub = getConteudoSubFormulario(dtSub);
        (sender as ASPxGridView).DataBind();

        e.Cancel = true;
        (sender as ASPxGridView).CancelEdit();
    }


}
