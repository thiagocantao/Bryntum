using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using DevExpress.Web;
using System.Collections.Specialized;

public partial class _Projetos_Administracao_CronogramaOrcamentarioNovo : System.Web.UI.Page
{
    dados cDados;

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;

    int codigoProjeto = -1;
    public string somenteLeitura = "";
    int indexTotalSup = 0, indexTotalTrans = 0, indexTotalProp = 0, indexTotalRef = 0;
    string faseTAI = "";

    DataTable dtb;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();
        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        //TODO: Descomentar esse código

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
        gvDados.JSProperties["cp_AtualizaComboContas"] = "N";

        codigoEntidadeUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("CodigoEntidade") ?? -1);
        codigoUsuarioResponsavel = Convert.ToInt32(cDados.getInfoSistema("IDUsuarioLogado") ?? -1);

        faseTAI = Request.QueryString["FaseTAI"] != null ? Request.QueryString["FaseTAI"].ToString() : "";
        
        if (Request.QueryString["CP"] != null)
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

        if (Request.QueryString["RO"] != null)
            somenteLeitura = Request.QueryString["RO"].ToString();

        if (Request.QueryString["ALT"] != null && Request.QueryString["ALT"].ToString() != "")
        {
            gvDados.Settings.VerticalScrollableHeight = int.Parse(Request.QueryString["ALT"].ToString());
        }
        
        dtb = new DataTable();
        dtb = CriaDataTable();
        
        this.gvDados.DataSource = dtb;
        this.gvDados.DataBind();
        gvDados.ExpandAll();

        cDados.aplicaEstiloVisual(this);

        if (somenteLeitura == "S")
        {
            txtComentario.ClientEnabled = false;
            btnSalvarMemoria.ClientVisible = false;
        }

        ajustaColunas();
        gvDados.JSProperties["cp_IndexNavegacao"] = faseTAI == "R" ? 2 : 3;
        gvDados.ExpandAll();
        gvDados.JSProperties["cp_AtualizaTela"] = "N";
    }   

    private DataTable CriaDataTable()
    {
        string comandoSQL = string.Format(@"
                     BEGIN
                      DECLARE @CodigoProjeto INT
  
                      SET @CodigoProjeto = {2}
  
                     SELECT ai.CodigoProjeto, ai.CodigoAcao, co.SeqPlanoContas AS CodigoConta, co.Quantidade, co.ValorUnitario
                            ,co.ValorProposto, co.MemoriaCalculo ,co.[ValorSuplemento],co.[ValorTransposto]
                            ,co.[ValorRealizado],co.[DisponibilidadeAtual],co.[DisponibilidadeReformulada]
                            , opc.CONTA_DES, opc.CONTA_COD, 'a' AS Controle, ai.NomeAcao, co.EtapaAcaoAtividade, ai.NumeroAcao
                            ,co.ValorSuplementacao_Old , ValorTransposicao_Old 
                       FROM {0}.{1}.tai02_AcoesIniciativa ai   INNER JOIN
                            {0}.{1}.CronogramaOrcamentarioAcao co ON ai.CodigoAcao = co.CodigoAcao AND ai.CodigoProjeto = co.CodigoProjeto INNER JOIN
                            {0}.{1}.orc_planoContas opc ON opc.SeqPlanoContas = co.SeqPlanoContas
                      WHERE ai.CodigoProjeto = @CodigoProjeto
                        AND FonteRecurso <> 'SR'
                        AND ai.CodigoAcao = ai.CodigoAcaoSuperior
                      UNION
                      SELECT ai.CodigoProjeto, ai.CodigoAcao, NULL, NULL, NULL
                            ,ISNULL(Sum(co.ValorProposto), 0), NULL, ISNULL(Sum(co.[ValorSuplemento]), 0),ISNULL(Sum(co.[ValorTransposto]), 0)
                            ,ISNULL(Sum(co.[ValorRealizado]), 0),ISNULL(Sum(co.[DisponibilidadeAtual]), 0),ISNULL(Sum(co.[DisponibilidadeReformulada]), 0)
                            , 'TOTAL', NULL, 'z' AS Controle, ai.NomeAcao, NULL, ai.NumeroAcao
                            ,ISNULL(Sum(co.[ValorSuplementacao_Old]), 0),ISNULL(Sum(co.[ValorTransposicao_Old]), 0)   
                      FROM {0}.{1}.tai02_AcoesIniciativa ai   LEFT JOIN
                           {0}.{1}.CronogramaOrcamentarioAcao co ON ai.CodigoAcao = co.CodigoAcao AND ai.CodigoProjeto = co.CodigoProjeto LEFT JOIN
                           {0}.{1}.orc_planoContas opc ON opc.SeqPlanoContas = co.SeqPlanoContas
                      WHERE ai.CodigoProjeto = @CodigoProjeto
                        AND FonteRecurso <> 'SR'
                        AND ai.CodigoAcao = ai.CodigoAcaoSuperior
                      GROUP BY ai.CodigoProjeto, ai.CodigoAcao, ai.NomeAcao, ai.NumeroAcao
                      ORDER BY ai.NumeroAcao, Controle, opc.CONTA_DES DESC
                    END  ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto);

        DataSet ds = cDados.getDataSet(comandoSQL);

        return ds.Tables[0];
    }

    private void ajustaColunas()
    {
        gvDados.Columns["Quantidade"].Visible = faseTAI != "R";
        gvDados.Columns["ValorUnitario"].Visible = faseTAI != "R";
        gvDados.Columns["ValorProposto"].Visible = faseTAI != "R";

        gvDados.Columns["ValorRealizado"].Visible = faseTAI == "R";
        gvDados.Columns["DisponibilidadeAtual"].Visible = faseTAI == "R";
        gvDados.Columns["ValorSuplemento"].Visible = faseTAI == "R";
        gvDados.Columns["ValorTransposto"].Visible = faseTAI == "R";
        gvDados.Columns["DisponibilidadeReformulada"].Visible = faseTAI == "R";
        gvDados.Columns["ValorSuplementacao_Old"].Visible = faseTAI == "R";
        gvDados.Columns["ValorTransposicao_Old"].Visible = faseTAI == "R";
    }

    protected void gvDados_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex != -1)
        {
            DataRowView x = (DataRowView)gvDados.GetRow(e.VisibleIndex);
            if (x.Row["Controle"].ToString() == "z")
                e.Row.BackColor = System.Drawing.Color.Bisque;
        }
    }

    protected void callbackSalvar_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        //Grava as informações no momento em que o usuário muda de campo na grid
        if (e.Parameter != "")
        {
            if (e.Parameter.Split(';')[0] == "SUP" || e.Parameter.Split(';')[0] == "TRANS")
            {
                string nomeColunaAtualizacao = e.Parameter.Split(';')[0] == "SUP" ? "ValorSuplemento" : "ValorTransposto";
                string codigoAcao = e.Parameter.Split(';')[1]; //Codigo da Ação
                string codigoConta = e.Parameter.Split(';')[2]; //Codigo da Conta
                string valor = e.Parameter.Split(';')[3] != "" && e.Parameter.Split(';')[3] != "null" ? e.Parameter.Split(';')[3].Replace(",", ".") : "0";

                string comandoSQL = string.Format(@"                                             
                                    UPDATE {0}.{1}.CronogramaOrcamentarioAcao SET {2} = {3}
                                     WHERE CodigoProjeto = {4} AND CodigoAcao = {5} AND SeqPlanoContas = {6}

                                     UPDATE {0}.{1}.CronogramaOrcamentarioAcao 
                                            set DisponibilidadeAtual =       (isnull(ValorProposto,0) - isnull(valorRealizado,0)) + (isnull(ValorSuplementacao_Old,0)+isnull(ValorTransposicao_Old,0)) ,
                                                DisponibilidadeReformulada = (isnull(ValorProposto,0) - isnull(valorRealizado,0)) + (isnull(ValorSuplementacao_Old,0)+isnull(ValorTransposicao_Old,0 + isnull(valorSuplemento,0)+isnull(ValorTransposto,0))) 
                                      WHERE CodigoProjeto = {4} AND CodigoAcao = {5} AND SeqPlanoContas = {6}   
 
   ", cDados.getDbName(), cDados.getDbOwner(), nomeColunaAtualizacao, valor, codigoProjeto, codigoAcao, codigoConta);

                int regAf = 0;

                cDados.execSQL(comandoSQL, ref regAf);
            }
            else if (e.Parameter.Split(';')[0] == "QTDE")
            {
                string codigoAcao = e.Parameter.Split(';')[1]; //Codigo da Ação
                string codigoConta = e.Parameter.Split(';')[2]; //Codigo da Conta
                string valor = e.Parameter.Split(';')[3] != "" && e.Parameter.Split(';')[3] != "null" ? e.Parameter.Split(';')[3].Replace(",", ".") : "0";

                string comandoSQL = string.Format(@"                                             
                                    UPDATE {0}.{1}.CronogramaOrcamentarioAcao SET Quantidade = {2}
                                                                                 ,ValorProposto = ValorUnitario * {2}
                                     WHERE CodigoProjeto = {3} AND CodigoAcao = {4} AND SeqPlanoContas = {5}

                                     UPDATE {0}.{1}.CronogramaOrcamentarioAcao 
                                            set DisponibilidadeAtual =       (isnull(ValorProposto,0) - isnull(valorRealizado,0)) + (isnull(ValorSuplementacao_Old,0)+isnull(ValorTransposicao_Old,0)) ,
                                                DisponibilidadeReformulada = (isnull(ValorProposto,0) - isnull(valorRealizado,0)) + (isnull(ValorSuplementacao_Old,0)+isnull(ValorTransposicao_Old,0 + isnull(valorSuplemento,0)+isnull(ValorTransposto,0))) 
                                      WHERE CodigoProjeto = {3} AND CodigoAcao = {4} AND SeqPlanoContas = {5}

", cDados.getDbName(), cDados.getDbOwner(), valor, codigoProjeto, codigoAcao, codigoConta);

                int regAf = 0;

                cDados.execSQL(comandoSQL, ref regAf);
            }
            else if (e.Parameter.Split(';')[0] == "UNI")
            {
                string codigoAcao = e.Parameter.Split(';')[1]; //Codigo da Ação
                string codigoConta = e.Parameter.Split(';')[2]; //Codigo da Conta
                string valor = e.Parameter.Split(';')[3] != "" && e.Parameter.Split(';')[3] != "null" ? e.Parameter.Split(';')[3].Replace(",", ".") : "0";

                string comandoSQL = string.Format(@"                                             
                                    UPDATE {0}.{1}.CronogramaOrcamentarioAcao SET ValorUnitario = {2}
                                                                                 ,ValorProposto = Quantidade * {2}
                                     WHERE CodigoProjeto = {3} AND CodigoAcao = {4} AND SeqPlanoContas = {5}

                                     UPDATE {0}.{1}.CronogramaOrcamentarioAcao 
                                           set DisponibilidadeAtual =       (isnull(ValorProposto,0) - isnull(valorRealizado,0)) + (isnull(ValorSuplementacao_Old,0)+isnull(ValorTransposicao_Old,0)) ,
                                               DisponibilidadeReformulada = (isnull(ValorProposto,0) - isnull(valorRealizado,0)) + (isnull(ValorSuplementacao_Old,0)+isnull(ValorTransposicao_Old,0 + isnull(valorSuplemento,0)+isnull(ValorTransposto,0))) 
                                      WHERE CodigoProjeto = {3} AND CodigoAcao = {4} AND SeqPlanoContas = {5}", cDados.getDbName(), cDados.getDbOwner(), valor, codigoProjeto, codigoAcao, codigoConta);

                int regAf = 0;

                cDados.execSQL(comandoSQL, ref regAf);
            }
        }
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.VisibleIndex != -1)
        {
            //Se a coluna for Suplementado ou Transposto, insere os comandos JS que irão fazer o calculo somatório e a navegação entre os campos
            if (e.DataColumn.FieldName == "ValorSuplemento" || e.DataColumn.FieldName == "ValorTransposto")
            {
                DataRowView x = (DataRowView)gvDados.GetRow(e.VisibleIndex);

                ASPxSpinEdit spin = gvDados.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txt" + e.DataColumn.Name) as ASPxSpinEdit;
                                
                if (x.Row["Controle"].ToString() == "z") //Linhas Totalizadoras
                {
                    //Define propriedades das linhas totalizadoras
                    e.Cell.BackColor = System.Drawing.Color.Bisque;
                    spin.BackColor = System.Drawing.Color.Bisque;
                    spin.DisabledStyle.BackColor = System.Drawing.Color.Bisque;
                    spin.DisabledStyle.ForeColor = System.Drawing.Color.Black;
                    spin.MaxValue = 0;
                    spin.MinValue = 0;
                    //spin.ClientEnabled = false;
                    spin.ReadOnly = true;
                    if (e.DataColumn.Name == "SUP")
                    {
                        spin.ClientInstanceName = "spinTotalSUP" + indexTotalSup;
                        //spin.ID = "spinTotal" + indexTotalSup;
                        indexTotalSup++;
                    }
                    else
                    {
                        spin.ClientInstanceName = "spinTotalTRANS" + indexTotalTrans;
                        //spin.ID = "spinTotal" + indexTotalTrans;
                        indexTotalTrans++;
                    }
                }
                else //Linhas Editáveis
                {
                    spin.TabIndex = 1;
                    if (somenteLeitura == "S")
                    {
                        spin.ReadOnly = true;
                        e.Cell.BackColor = System.Drawing.Color.FromName("#EBEBEB");
                        spin.BackColor = System.Drawing.Color.FromName("#EBEBEB");
                        spin.DisabledStyle.BackColor = System.Drawing.Color.FromName("#EBEBEB");
                        spin.DisabledStyle.ForeColor = System.Drawing.Color.Black;

                        return;
                    }

                    e.Cell.BackColor = Color.FromName("#E1EAFF");

                    spin.ClientInstanceName = "txt_" + e.VisibleIndex + "_" + e.DataColumn.Name;

                    string valorDisponibilidadeAtual = (gvDados.GetRowValues(e.VisibleIndex, "DisponibilidadeAtual") + "" == "") ? "0" : gvDados.GetRowValues(e.VisibleIndex, "DisponibilidadeAtual").ToString().Replace(".", "").Replace(",", ".");

                    string comandoJS = string.Format(" executaCalculoCampo(s.GetValue(), txt_{1}_{2}, spinTotal{2}{0}, txtSomaTotal{2}, txt_{1}_REF, spinTotalREF{3}, txtSomaTotalREF, {4}); "
                        , e.DataColumn.Name == "SUP" ? indexTotalSup : indexTotalTrans
                        , e.VisibleIndex
                        , e.DataColumn.Name
                        , indexTotalRef
                        , valorDisponibilidadeAtual);

                    spin.ClientSideEvents.Validation = @"function(s, e) { " + comandoJS + " }";

                    spin.ClientSideEvents.GotFocus = @"function(s, e) { valorAtual = s.GetValue(); }";
                    
                    string codigoAcao = gvDados.GetRowValues(e.VisibleIndex, "CodigoAcao") + "";
                    string codigoConta = gvDados.GetRowValues(e.VisibleIndex, "CodigoConta") + "";

                    spin.ClientSideEvents.ValueChanged = @"function(s, e) { callbackSalvar.PerformCallback('" + e.DataColumn.Name + ";" + codigoAcao + ";" + codigoConta + ";' + s.GetValue());}";
                }

                string comandoFocus = string.Format(@"if(e.htmlEvent.keyCode == 38)
                                                      navegaSetas('C');", (e.VisibleIndex - 1));

                comandoFocus += string.Format(@"if(e.htmlEvent.keyCode == 40)
                                                        navegaSetas('B');", (e.VisibleIndex - 1)
                                                                                            , e.VisibleIndex + 1);

                spin.ClientSideEvents.KeyDown = @"function(s, e) { " + comandoFocus + " }";
                spin.ClientSideEvents.KeyPress = @"function(s, e) { if(e.htmlEvent.keyCode == 13)
                                                                    navegaSetas('B'); }";
            }
            else if (e.DataColumn.FieldName == "MemoriaCalculo")
            {
                gvDados.JSProperties["cp_Memoria_" + e.VisibleIndex] = e.CellValue.ToString();
                string codigoAcao = gvDados.GetRowValues(e.VisibleIndex, "CodigoAcao") + "";
                string codigoConta = gvDados.GetRowValues(e.VisibleIndex, "CodigoConta") + "";

                if (e.CellValue != null && e.CellValue.ToString().Length > 40)
                    e.Cell.Text = e.CellValue.ToString().Substring(0, 39) + "...";

                e.Cell.Attributes.Add("onclick", string.Format(@"abreMemoria({0}, {1})", codigoAcao, codigoConta, e.VisibleIndex));
                e.Cell.Attributes.Add("title", "Clique aqui para visualizar/editar a memória de cálculo");
                e.Cell.Style.Add("cursor", "pointer");
            }
            else if (e.DataColumn.FieldName == "DisponibilidadeReformulada")
            {
                DataRowView x = (DataRowView)gvDados.GetRow(e.VisibleIndex);

                ASPxSpinEdit spin = gvDados.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txtREF") as ASPxSpinEdit;
                                
                if (x.Row["Controle"].ToString() == "z") //Linhas Totalizadoras
                {
                    //Define propriedades das linhas totalizadoras
                    e.Cell.BackColor = System.Drawing.Color.Bisque;
                    spin.BackColor = System.Drawing.Color.Bisque;
                    spin.DisabledStyle.BackColor = System.Drawing.Color.Bisque;
                    spin.DisabledStyle.ForeColor = System.Drawing.Color.Black;
                    spin.MaxValue = 0;
                    spin.MinValue = 0;
                    spin.ReadOnly = true;

                    spin.ClientInstanceName = "spinTotalREF" + indexTotalRef;
                    indexTotalRef++;

                }
                else //Linhas Editáveis
                {
                    e.Cell.BackColor = Color.FromName("#EBEBEB");
                    spin.BackColor = Color.FromName("#EBEBEB");
                    spin.ClientInstanceName = "txt_" + e.VisibleIndex + "_REF";
                }

            }
            else if (e.DataColumn.FieldName == "ValorProposto")
            {
                DataRowView x = (DataRowView)gvDados.GetRow(e.VisibleIndex);

                ASPxSpinEdit spin = gvDados.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txtPROP") as ASPxSpinEdit;

                
                if (x.Row["Controle"].ToString() == "z") //Linhas Totalizadoras
                {
                    //Define propriedades das linhas totalizadoras
                    e.Cell.BackColor = System.Drawing.Color.Bisque;
                    spin.BackColor = System.Drawing.Color.Bisque;
                    spin.DisabledStyle.BackColor = System.Drawing.Color.Bisque;
                    spin.DisabledStyle.ForeColor = System.Drawing.Color.Black;
                    spin.MaxValue = 0;
                    spin.MinValue = 0;
                    spin.ReadOnly = true;
                    spin.ClientEnabled = false;
                    spin.ClientInstanceName = "spinTotalPROP" + indexTotalProp;
                    //spin.ID = "spinTotal" + indexTotalSup;
                    indexTotalProp++;

                }
                else //Linhas Editáveis
                {
                    e.Cell.BackColor = Color.FromName("#EBEBEB");
                    spin.BackColor = Color.FromName("#EBEBEB");
                    spin.ClientInstanceName = "txt_" + e.VisibleIndex + "_PROP";                    
                }               

            }
            else if (e.DataColumn.FieldName == "Quantidade" || e.DataColumn.FieldName == "ValorUnitario")
            {
                DataRowView x = (DataRowView)gvDados.GetRow(e.VisibleIndex);

                ASPxSpinEdit spin = gvDados.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txt" + e.DataColumn.Name) as ASPxSpinEdit;
                                

                if (x.Row["Controle"].ToString() == "z") //Linhas Totalizadoras
                {
                    //Define propriedades das linhas totalizadoras
                    e.Cell.BackColor = System.Drawing.Color.Bisque;
                    spin.BackColor = System.Drawing.Color.Bisque;
                    spin.DisabledStyle.BackColor = System.Drawing.Color.Bisque;
                    spin.DisabledStyle.ForeColor = System.Drawing.Color.Black;
                    spin.MaxValue = 0;
                    spin.MinValue = 0;
                    spin.Value = null;
                    spin.NullText = "";
                    spin.ReadOnly = true;
                }
                else //Linhas Editáveis
                {
                    spin.TabIndex = 1;

                    if (somenteLeitura == "S")
                    {
                        spin.ReadOnly = true;
                        e.Cell.BackColor = System.Drawing.Color.FromName("#EBEBEB");
                        spin.BackColor = System.Drawing.Color.FromName("#EBEBEB");
                        spin.DisabledStyle.BackColor = System.Drawing.Color.FromName("#EBEBEB");
                        spin.DisabledStyle.ForeColor = System.Drawing.Color.Black;

                        return;
                    }

                    e.Cell.BackColor = Color.FromName("#E1EAFF");

                    spin.ClientInstanceName = "txt_" + e.VisibleIndex + "_" + e.DataColumn.Name;

                    string comandoJS = string.Format(" executaCalculoCampoFormulacao(txt_{1}_UNI, txt_{1}_QTDE, txt_{1}_PROP, spinTotalPROP{0}, txtSomaTotalPROP); "
                        , indexTotalProp
                        , e.VisibleIndex
                        , e.DataColumn.Name);

                    spin.ClientSideEvents.Validation = @"function(s, e) { " + comandoJS + " }";

                    spin.ClientSideEvents.GotFocus = @"function(s, e) { valorAtual = s.GetValue(); }";

                    //spin.ClientSideEvents.LostFocus = @"function(s, e) { valorAtual = 0; }";

                    string codigoAcao = gvDados.GetRowValues(e.VisibleIndex, "CodigoAcao") + "";
                    string codigoConta = gvDados.GetRowValues(e.VisibleIndex, "CodigoConta") + "";

                    spin.ClientSideEvents.ValueChanged = @"function(s, e) { callbackSalvar.PerformCallback('" + e.DataColumn.Name + ";" + codigoAcao + ";" + codigoConta + ";' + s.GetValue());}";
                }

                string comandoFocus = string.Format(@"if(e.htmlEvent.keyCode == 38)
                                                      navegaSetas('C');", (e.VisibleIndex - 1));

                comandoFocus += string.Format(@"if(e.htmlEvent.keyCode == 40)
                                                        navegaSetas('B');", (e.VisibleIndex - 1)
                                                                                            , e.VisibleIndex + 1);

                spin.ClientSideEvents.KeyDown = @"function(s, e) { " + comandoFocus + " }";
                spin.ClientSideEvents.KeyPress = @"function(s, e) { if(e.htmlEvent.keyCode == 13)
                                                                    navegaSetas('B'); }";
            }
        }
    }

    public double getSomaColuna(string nomeColuna)
    {
        if (dtb != null && dtb.Rows.Count > 0)
        {
            object sumObject = dtb.Compute("Sum(" + nomeColuna + ")", "Controle <> 'z'");

            double valorRetorno = sumObject == null || (sumObject + "") == "" ? 0 : double.Parse(sumObject.ToString());

            return valorRetorno;
        }
        else
        {
            return 0;
        }
    }

    protected void callbackSalvarMemoria_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter != "")
        {
            string codigoAcao = e.Parameter.Split(';')[0]; //Codigo da Ação
            string codigoConta = e.Parameter.Split(';')[1]; //Codigo da Conta

            string comandoSQL = string.Format(@"                                             
                                    UPDATE {0}.{1}.CronogramaOrcamentarioAcao SET MemoriaCalculo = '{5}'
                                     WHERE CodigoProjeto = {2} AND CodigoAcao = {3} AND SeqPlanoContas = {4}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, codigoAcao, codigoConta, txtComentario.Text.Replace("'", "''"));

            int regAf = 0;

            cDados.execSQL(comandoSQL, ref regAf);
        }
    }

    protected void callbackGetMemoria_Callback(object source, DevExpress.Web.CallbackEventArgs e)
    {
        if (e.Parameter != "")
        {
            string codigoAcao = e.Parameter.Split(';')[0]; //Codigo da Ação
            string codigoConta = e.Parameter.Split(';')[1]; //Codigo da Conta

            string comandoSQL = string.Format(@"                                             
                                    SELECT MemoriaCalculo FROM {0}.{1}.CronogramaOrcamentarioAcao
                                     WHERE CodigoProjeto = {2} AND CodigoAcao = {3} AND SeqPlanoContas = {4}", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, codigoAcao, codigoConta);

            DataSet ds = cDados.getDataSet(comandoSQL);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                callbackGetMemoria.JSProperties["cp_Memoria"] = ds.Tables[0].Rows[0]["MemoriaCalculo"].ToString();
            }
            else
            {
                callbackGetMemoria.JSProperties["cp_Memoria"] = "";
            }
        }
    }

    protected void gvDados_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        int registrosAfetados = 0;

        string comandoSQL = string.Format(@"DELETE {0}.{1}.CronogramaOrcamentarioAcao WHERE CodigoAcao = {2} AND SeqPlanoContas = {3}"
           ,cDados.getDbName()
           ,cDados.getDbOwner()
           ,e.Keys[0]
           ,e.Keys[1]);

        bool retorno = cDados.execSQL(comandoSQL, ref registrosAfetados);

        if (retorno)
        {
            dtb = new DataTable();
            dtb = CriaDataTable();
            this.gvDados.DataSource = dtb;
            this.gvDados.DataBind();
            e.Cancel = true;
            gvDados.JSProperties["cp_AtualizaTela"] = "S";
            gvDados.CancelEdit();
        }
    }

    protected void gvDados_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        DataRowView x = (DataRowView)gvDados.GetRow(e.VisibleIndex);

        if (x != null)
        {
            if (x.Row["Controle"].ToString() == "z") //Linhas Totalizadoras
            {
                e.Visible = false;
            }
            else
            {
                if (somenteLeitura == "S" || faseTAI != gvDados.GetRowValues(e.VisibleIndex, "EtapaAcaoAtividade").ToString().Trim())
                {
                    e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                    e.Enabled = false;
                }
            }
        }
    }

    private void carregaComboContas(int codigoAcao)
    {
        if (Request.QueryString["CP"] != null)
            codigoProjeto = int.Parse(Request.QueryString["CP"].ToString());

        cDados = CdadosUtil.GetCdados(null);

        string comandoSQL = string.Format(@"SELECT * 
                                              FROM {0}.{1}.orc_planoContas pc 
                                             WHERE pc.SeqPlanoContas NOT IN(SELECT co.SeqPlanoContas 
																			 FROM {0}.{1}.CronogramaOrcamentarioAcao co
																		    WHERE co.CodigoAcao = {2} )
                                               AND pc.Ano = (SELECT ano 
                                                               FROM {0}.{1}.orc_movimentoOrcamento mo INNER JOIN
                                                                    {0}.{1}.TermoAbertura02 ta ON ta.CodigoMovimentoOrcamento = mo.CodigoMovimentoOrcamento
                                                              WHERE ta.CodigoProjeto = {3})", cDados.getDbName(), cDados.getDbOwner(), codigoAcao, codigoProjeto);
        DataSet ds = cDados.getDataSet(comandoSQL);

        if (cDados.DataSetOk(ds))
        {
            ddlConta.DataSource = ds;
            ddlConta.TextField = "CONTA_DES";
            ddlConta.ValueField = "SeqPlanoContas";
            ddlConta.DataBind();
        }
    }

    protected void ddlConta_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (e.Parameter != "")
        {
            carregaComboContas(int.Parse(e.Parameter));
        }
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        gvDados.JSProperties["cp_AtualizaComboContas"] = "N";

        if (e.Parameters != "")
        {
            string codigoAcao = e.Parameters.Split(';')[0];
            string codigoConta = e.Parameters.Split(';')[1];

            string comandoSQL = string.Format(@"                                             
                                    INSERT INTO {0}.{1}.CronogramaOrcamentarioAcao(CodigoProjeto, CodigoAcao, SeqPlanoContas, MemoriaCalculo, Quantidade, ValorUnitario
                                                                                ,ValorProposto
                                                                                ,ValorSuplemento
                                                                                ,ValorTransposto
                                                                                ,ValorRealizado
                                                                                ,DisponibilidadeAtual
                                                                                ,DisponibilidadeReformulada
                                                                                ,EtapaAcaoAtividade) 
                                                                            VALUES({2}, {3}, {4}, '', 0, 0, 0, 0, 0, 0, 0, 0, '{5}')
                                    ", cDados.getDbName(), cDados.getDbOwner(), codigoProjeto, codigoAcao, codigoConta, faseTAI);

            int regAf = 0;

            if (cDados.execSQL(comandoSQL, ref regAf))
            {
                gvDados.JSProperties["cp_AtualizaComboContas"] = "S";
                dtb = new DataTable();
                dtb = CriaDataTable();
                this.gvDados.DataSource = dtb;
                this.gvDados.DataBind();
            }
        }
    }
}