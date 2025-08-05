using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using DevExpress.Web;

public partial class _Projetos_Administracao_GestaoPlanosInvestimentoTIC : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel = 0;
    int codigoEntidadeUsuarioResponsavel = 0;

    bool podeEditar = true;
    public bool podeExcluir = true;
    bool podeReativar = true;
    int codigoStatus = -1;

    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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
        this.Title = cDados.getNomeSistema();

        cDados.aplicaEstiloVisual(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/GestaoPlanosInvestimentoTIC.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<link type=""text/css"" rel=""Stylesheet"" href=""../../estilos/cdisEstilos.css"" />"));

        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }

        carregaComboPlanosInvestimento();
        carregaGrid();

        gvDados.JSProperties["cp_Status"] = "";
        gvDados.JSProperties["cp_Msg"] = "";
        gvDados.JSProperties["cp_AtualizaPlanoInvestimento"] = "";
    }

    private void defineAlturaTela()
    {
        string resolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString();

        // Calcula a altura da tela
        int alturaPrincipal = int.Parse(resolucaoCliente.Substring(resolucaoCliente.IndexOf('x') + 1));
        int larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));        
        //problemas quando o botão publicar é falso a altura deve ser um pouco menor
        gvDados.Settings.VerticalScrollableHeight = btnPublicar.ClientVisible ? alturaPrincipal - 400 : alturaPrincipal - 330;

    }

    private void carregaComboPlanosInvestimento()
    {        
        DataSet ds = cDados.getPlanosInvestimentoTIC("");

        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            ddlPlanoInvestimento.DataSource = ds;
            ddlPlanoInvestimento.TextField = "DescricaoPlanoInvestimento";
            ddlPlanoInvestimento.ValueField = "CodigoPlanoInvestimento";
            ddlPlanoInvestimento.DataBind();

            if (!IsPostBack && !IsCallback)
            {
                ddlPlanoInvestimento.SelectedIndex = 0;
            }

            if (ddlPlanoInvestimento.SelectedIndex != -1)
            {
                DataRow[] drs = ds.Tables[0].Select("CodigoPlanoInvestimento = " + ddlPlanoInvestimento.Value);

                DataRow dr = drs[0];

                codigoStatus = int.Parse(dr["CodigoStatusPlanoInvestimento"].ToString());

                gvDados.JSProperties["cp_StatusPlanoInvestimento"] = codigoStatus;

                if (codigoStatus == 5)
                {
                    //podeEditar = false;
                    podeExcluir = false;
                    podeReativar = false;
                    btnPublicar.ClientVisible = false;

                    lblValor.Text = "Reprogramação(R$):";                    
                }
                else
                {
                    btnPublicar.ClientVisible = true;

                    if (codigoStatus == 1)
                        lblValor.Text = "Ajustado CGTIC(R$):";
                    else if (codigoStatus == 2)
                        lblValor.Text = "Ajustado LOA(R$):";
                        
                }

                gvDados.JSProperties["cp_DescricaoLabel"] = lblValor.Text;
                gvDados.JSProperties["cp_TextoBotao"] = dr["DescricaoAcaoProximoStatus"].ToString() != "" ? dr["DescricaoAcaoProximoStatus"].ToString() : "Publicar";
            }
        }
        else
        {
            podeEditar = false;
            podeExcluir = false;
            podeReativar = false;
            btnPublicar.ClientVisible = false;
        }
    }

    private void carregaGrid()
    {
        if (ddlPlanoInvestimento.Items.Count > 0 && ddlPlanoInvestimento.SelectedIndex != -1)
        {
            int codigoPlanoInvestimento = int.Parse(ddlPlanoInvestimento.Value.ToString());

            DataSet ds = cDados.getProjetosPlanoInvestimentoTIC(codigoPlanoInvestimento, "");

            if (cDados.DataSetOk(ds))
            {
                gvDados.DataSource = ds;
                gvDados.DataBind();
            }
        }

        defineAlturaTela();
    }

    public string getBotoes()
    {
        string tabelaBotoes = "";
        string botaoEditar = "";
        string botaoDetalhes = "";
        string botaoReativar = "";
        object valor = "";

        switch (codigoStatus)
        {
            case 1: valor = Eval("AjustadoCGTIC");
                break;
            case 2: valor = Eval("LOA");
                break;
            case 5: valor = Eval("ValorRemanejado");
                break;
            default: valor = "null";
                break;
        }

        bool projetoEditavel = Eval("CodigoStatus").ToString() != "3" || codigoStatus == 5;

        botaoEditar = podeEditar && projetoEditavel ? string.Format(@"<img src='../../imagens/botoes/editarReg02.png' style='cursor:pointer' alt='Alterar Valor' onclick=""txtValor.SetValue({1}); abreEdicao('{0}')"" />"
            , Eval("NomeProjeto").ToString().Replace("'", "\'"), valor) :
           @"<img src='../../imagens/botoes/editarRegDes.png' />";

        botaoReativar = podeReativar && !projetoEditavel ? string.Format(@"<img src='../../imagens/botoes/retornar.png' style='cursor:pointer' alt='Incluir Registro' onclick=""abreReativacao()"" />") :
            @"<img src='../../imagens/botoes/retornarDes.png' />";

        botaoDetalhes = string.Format(@"<img src='../../imagens/botoes/pFormulario.png' style='cursor:pointer' alt='Histórico' onclick=""abreDetalhes({0}, {1})"" />"
            , Eval("CodigoProjeto")
            , ddlPlanoInvestimento.Value);


        tabelaBotoes = string.Format(@"<table cellSpacing=0 cellPadding=0 width=""100%"" border=0>
                                        <tr>
                                            <td title='Alterar Valor'>{0}</td>
                                            <td title='Incluir Registro'>{1}</td>
                                            <td title='Histórico'>{2}</td>
                                        </tr>
                                     </table>", botaoEditar, botaoReativar, botaoDetalhes);

        return tabelaBotoes;
    }

    protected void gvDados_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {

    }
    
    protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
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
        if (e.Column.Name == "CodigoProjeto")
            e.Text = " ";
            
        Font fonte = new Font("Verdana", 9);
        e.BrickStyle.Font = fonte;
    }

    protected void gvDados_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        string codigoStatusLinha = gvDados.GetRowValues(e.VisibleIndex, "CodigoStatus") + "";

        if (codigoStatusLinha == "3" || codigoStatus == 5)
            e.Enabled = false;
    }

    protected void gvDados_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string comandoSQL = "";
        string comentarios = mmObjeto.Text.Replace("'", "''");

        if (e.Parameters == "X")
        {

            for (int i = 0; i < gvDados.GetSelectedFieldValues("CodigoProjeto").Count; i++)
            {
                int codigoProjeto = int.Parse(gvDados.GetSelectedFieldValues("CodigoProjeto")[i].ToString());

                comandoSQL += string.Format(@"
                                               INSERT INTO {0}.{1}.[pbh_MovimentoPlanoInvestimento]
                                                           ([DataMovimento]
                                                           ,[CodigoUsuarioMovimento]
                                                           ,[ComentarioMovimento]
                                                           ,[CodigoPlanoInvestimento]
                                                           ,[CodigoProjeto]
                                                           ,[CodigoStatusDe]
                                                           ,[CodigoStatusPara]
                                                           ,[TipoMovimento])
                                                     VALUES
                                                           (GetDate()
                                                           ,{2}
                                                           ,'{3}'
                                                           ,{4}
                                                           ,{5}
                                                           ,{6}
                                                           ,{7}
                                                           ,'{8}')
                                            UPDATE {0}.{1}.pbh_PlanoInvestimentoProjeto SET CodigoStatusProjetoPlanoInvestimento = 3 WHERE CodigoPlanoInvestimento = {4} AND CodigoProjeto = {5}
                                            "
                    , cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel, comentarios, ddlPlanoInvestimento.Value, codigoProjeto, codigoStatus, 3, "EX");
            }

            if (comandoSQL != "")
            {
                int regAf = 0;

                try
                {
                    cDados.execSQL("BEGIN" + comandoSQL + "END", ref regAf);

                    gvDados.JSProperties["cp_Status"] = "1";
                    gvDados.JSProperties["cp_Msg"] = "Projetos marcados como não selecionados para LOA!";
                }
                catch (Exception ex)
                {
                    gvDados.JSProperties["cp_Status"] = "0";
                    gvDados.JSProperties["cp_Msg"] = "Erro ao marcar os projetos como não selecionados para LOA!" + Environment.NewLine + ex.Message;
                }
            }
        }
        else if (e.Parameters == "R")
        {
            int codigoProjeto = int.Parse(gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoProjeto").ToString());

            comandoSQL += string.Format(@"BEGIN
                                        INSERT INTO {0}.{1}.[pbh_MovimentoPlanoInvestimento]
                                                           ([DataMovimento]
                                                           ,[CodigoUsuarioMovimento]
                                                           ,[ComentarioMovimento]
                                                           ,[CodigoPlanoInvestimento]
                                                           ,[CodigoProjeto]
                                                           ,[CodigoStatusDe]
                                                           ,[CodigoStatusPara]
                                                           ,[TipoMovimento])
                                                     VALUES
                                                           (GetDate()
                                                           ,{2}
                                                           ,'{3}'
                                                           ,{4}
                                                           ,{5}
                                                           ,{6}
                                                           ,{7}
                                                           ,'{8}')
                                            UPDATE {0}.{1}.pbh_PlanoInvestimentoProjeto SET CodigoStatusProjetoPlanoInvestimento = {7} WHERE CodigoPlanoInvestimento = {4} AND CodigoProjeto = {5}
                                           END
                                            "
                , cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel, comentarios, ddlPlanoInvestimento.Value, codigoProjeto, 3, codigoStatus, "AS");

            int regAf = 0;

            try
            {
                cDados.execSQL(comandoSQL, ref regAf);

                gvDados.JSProperties["cp_Status"] = "1";
                gvDados.JSProperties["cp_Msg"] = "Registro incluído!";
            }
            catch (Exception ex)
            {
                gvDados.JSProperties["cp_Status"] = "0";
                gvDados.JSProperties["cp_Msg"] = "Erro ao incluir o registro!" + Environment.NewLine + ex.Message;
            }

        }
        else if (e.Parameters == "E")
        {
            int codigoProjeto = int.Parse(gvDados.GetRowValues(gvDados.FocusedRowIndex, "CodigoProjeto").ToString());

            string nomeValor = codigoStatus == 1 ? "ValorAjustado" : "ValorAprovadoLOA";
            string nomeCampoGrid = codigoStatus == 1 ? "AjustadoCGTIC" : "LOA";
            string tipoMovimento = "AV";

            if (codigoStatus == 5)
            {
                nomeValor = "ValorRemanejadoLOA";
                nomeCampoGrid = "ValorRemanejado";
                tipoMovimento = "RM";
            }

            string novoValor = txtValor.Value == null ? "NULL" : txtValor.Value.ToString().Replace(',', '.');
            string valorAnterior = gvDados.GetRowValues(gvDados.FocusedRowIndex, nomeCampoGrid).ToString() == "" ? "NULL" : gvDados.GetRowValues(gvDados.FocusedRowIndex, nomeCampoGrid).ToString().Replace(',', '.');

            comandoSQL += string.Format(@"BEGIN
                                        INSERT INTO {0}.{1}.[pbh_MovimentoPlanoInvestimento]
                                                           ([DataMovimento]
                                                           ,[CodigoUsuarioMovimento]
                                                           ,[ComentarioMovimento]
                                                           ,[CodigoPlanoInvestimento]
                                                           ,[CodigoProjeto]
                                                           ,[ValorAnterior]
                                                           ,[NovoValor]
                                                           ,[CodigoStatusDe]
                                                           ,[TipoMovimento])
                                                     VALUES
                                                           (GetDate()
                                                           , {2}
                                                           ,'{3}'
                                                           , {4}
                                                           , {5}
                                                           , {6}
                                                           , {7}
                                                           , {10}
                                                           ,'{8}')
                                            UPDATE {0}.{1}.pbh_PlanoInvestimentoProjeto SET {9} = {7} WHERE CodigoPlanoInvestimento = {4} AND CodigoProjeto = {5}
                                            END"
                , cDados.getDbName(), cDados.getDbOwner(), codigoUsuarioResponsavel, comentarios, ddlPlanoInvestimento.Value, codigoProjeto, valorAnterior, novoValor, tipoMovimento, nomeValor, codigoStatus);

            int regAf = 0;

            try
            {
                cDados.execSQL(comandoSQL, ref regAf);

                gvDados.JSProperties["cp_Status"] = "1";
                gvDados.JSProperties["cp_Msg"] = "Valor alterado com sucesso!";
            }
            catch (Exception ex)
            {
                gvDados.JSProperties["cp_Status"] = "0";
                gvDados.JSProperties["cp_Msg"] = "Erro ao alterar o valor!" + Environment.NewLine + ex.Message;
            }

        }
        else if (e.Parameters == "P")
        {
            comandoSQL += string.Format(@"EXEC {0}.{1}.p_pbh_publicaPlanoInvestimento {2}, {3}"
                , cDados.getDbName(), cDados.getDbOwner(), ddlPlanoInvestimento.Value, codigoUsuarioResponsavel);

            int regAf = 0;

            try
            {
                cDados.execSQL(comandoSQL, ref regAf);

                gvDados.JSProperties["cp_Status"] = "1";
                gvDados.JSProperties["cp_Msg"] = "Alteração de status realizada com sucesso!";
                carregaComboPlanosInvestimento();
                gvDados.JSProperties["cp_AtualizaPlanoInvestimento"] = "S";
            }
            catch (Exception ex)
            {
                gvDados.JSProperties["cp_Status"] = "0";
                gvDados.JSProperties["cp_Msg"] = "Erro ao alterar o status!" + Environment.NewLine + ex.Message;
            }

        }
        else if (e.Parameters != string.Empty)
        {
            (sender as ASPxGridView).Columns[e.Parameters].Visible = false;
            (sender as ASPxGridView).Columns[e.Parameters].ShowInCustomizationForm = true;
        }

        gvDados.Selection.UnselectAll();
        carregaGrid();
    }

    #region Eventos Menu Botões Inserção e Exportação

    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "GesPlanInv");
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), false, "", false, true, true, "GesPlanInv", lblTituloTela.Text, this);
    }

    #endregion
}
