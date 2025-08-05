using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Drawing;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Utils;
using System.Collections.Specialized;

public partial class planoContasFluxoCaixa : BasePageBrisk
{
  

    private int codigoUsuarioResponsavel;
    private int codigoEntidadeUsuarioResponsavel;
    private int alturaPrincipal = 0;

    private string resolucaoCliente = "";

    public bool podeEditar = true;
    public bool podeIncluir = true;
    public bool podeExcluir = true;
    public string alturaTabela = "";
    public bool passouNoHtmlRowPrepared = false;
    public string estiloFooter = "dxtlFooter";


    protected void Page_Init(object sender, EventArgs e)
    {
        OrderedDictionary listaParametrosDados = new OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";
        CDados = CdadosUtil.GetCdados(listaParametrosDados);


        codigoUsuarioResponsavel = int.Parse(CDados.getInfoSistema("IDUsuarioLogado").ToString());
        codigoEntidadeUsuarioResponsavel = int.Parse(CDados.getInfoSistema("CodigoEntidade").ToString());

        podeEditar = CDados.VerificaPermissaoUsuario(codigoUsuarioResponsavel, codigoEntidadeUsuarioResponsavel,
            codigoEntidadeUsuarioResponsavel, "null", "EN", 0, "null", "EN_CadPlnCta1");
        podeIncluir = podeEditar;
        podeExcluir = podeEditar;

        this.Title = CDados.getNomeSistema();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.TH(this.TS("planoContasFluxoCaixa", "_Strings"));

        tlCentroCustos.Settings.ScrollableHeight = TelaAltura - 250;
        carregaGvDados();

        if (!IsPostBack)
        {
            configTipoConta("S");
            CDados.excluiNiveisAbaixo(1);
            CDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }
 
    private void carregaGvDados()
    {
        string where = string.Format(@" AND pc.DescricaoConta like '%{0}%'", txtFiltro.Text);
        where += string.Format(@" AND pc.EntradaSaida = '{0}'", rbDespesaReceita.Value.ToString());
        DataSet ds = CDados.getPlanoContasFluxoCaixa(codigoEntidadeUsuarioResponsavel, where);

        if ((CDados.DataSetOk(ds)))
        {
            tlCentroCustos.DataSource = ds;
            tlCentroCustos.DataBind();
        }
    }
  

    protected void gvDados_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID == "btnEditar")
        {
            if (podeEditar)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/editarRegDes.png";
            }
        }
        if (e.ButtonID == "btnExcluir")
        {
            if (podeExcluir)
            {
                e.Enabled = true;
            }
            else
            {
                e.Enabled = false;
                e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
            }
        }
    }

    protected void pnCallback_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string mensagemErro_Persistencia = "";
        pnCallback.JSProperties["cp_Sucesso"] = "";
        pnCallback.JSProperties["cp_Erro"] = "";

        if (e.Parameter == "Incluir")
        {
            pnCallback.JSProperties["cp_Sucesso"] = Resources.traducao.planoContasFluxoCaixa_conta_inclu_da_com_sucesso_;
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameter == "Editar")
        {
            pnCallback.JSProperties["cp_Sucesso"] = Resources.traducao.planoContasFluxoCaixa_conta_alterada_com_sucesso_;
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameter == "Excluir")
        {
            pnCallback.JSProperties["cp_Sucesso"] = Resources.traducao.planoContasFluxoCaixa_conta_exclu_da_com_sucesso_;
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
        {
            pnCallback.JSProperties["cp_Erro"] = "";
        }
        else
        {// alguma coisa deu errado...
            if (mensagemErro_Persistencia.Contains("REFERENCE"))
            {
                mensagemErro_Persistencia = Resources.traducao.planoContasFluxoCaixa_a_conta_n_o_pode_ser_exclu_da_pois_est__sendo_utilizada_por_outros_registros_;
            }

            pnCallback.JSProperties["cp_Erro"] = mensagemErro_Persistencia;
        }
    }


    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        if (tlCentroCustos.FocusedNode != null)
            return tlCentroCustos.FocusedNode.Key;//.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
        else
            return "";
    }

    // Método responsável pela Inclusão do registro
    private string persisteInclusaoRegistro()
    {
        string codContaSuperior = hfGeral.Contains("codigoContaSelecionada") == true ? hfGeral.Get("codigoContaSelecionada").ToString() : "null";
        string indicaAnalitica = checkIndicaContaAnalitica.Value.ToString();
        string tipoConta = cbTipoConta.Value.ToString();
        string mensagemErro = "";
        string EntradaSaida = rbDespesaReceita.Value.ToString();
        string where = " and pc.DescricaoConta = '" + txtDescricaoConta.Text + "'";

        bool result = CDados.incluiPlanoDeContasFluxoCaixa(txtDescricaoConta.Text, codContaSuperior, codigoEntidadeUsuarioResponsavel, txtCodigoReservado.Text, tipoConta, EntradaSaida, indicaAnalitica, ref mensagemErro);

        if (result == false)
            return Resources.traducao.planoContasFluxoCaixa_erro + ":" + mensagemErro;
        else
        {
            carregaGvDados();
            return mensagemErro;
        }

    }

    private string persisteEdicaoRegistro() // Método responsável pela Atualização do registro
    {
        int codigoConta = int.Parse(getChavePrimaria());
        string mensagemErro = "";
        string codContaSuperior = "null";
        string EntradaSaida = rbDespesaReceita.Value.ToString();
        string tipoConta = cbTipoConta.Value.ToString();
        string indicaAnalitica = checkIndicaContaAnalitica.Value.ToString();
        bool result = CDados.atualizaPlanoDeContasFluxoCaixa(txtDescricaoConta.Text, codContaSuperior, codigoConta, txtCodigoReservado.Text, EntradaSaida, tipoConta, indicaAnalitica, ref mensagemErro);

        if (result == false)
            return mensagemErro;
        else
        {
            carregaGvDados();
            return "";
        }
    }

    private string persisteExclusaoRegistro() // Método responsável pela Exclusão do registro
    {
        int codigoPlanoContas = int.Parse(getChavePrimaria());
        string mensagemErro = "";
        if (CDados.verificaExclusaoPlanoDeContasFluxoCaixa(codigoPlanoContas, ref mensagemErro) == false)
        {
            return Resources.traducao.planoContasFluxoCaixa_a_conta_n_o_pode_ser_exclu_da__pois_existem_contas_subordinadas_a_esta_;
        }
        else
        {
            bool result = CDados.excluiPlanoDeContasFluxoCaixa(codigoPlanoContas, ref mensagemErro);

            if (result == false)
                return mensagemErro;
            else
            {
                carregaGvDados();
                return "";
            }
        }
    }

    protected void tlCentroCustos_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
    {
        if (e.RowKind == TreeListRowKind.Data)
        {
            if (e.GetValue("DescricaoConta") != null)
            {
                TreeListNode tln = tlCentroCustos.FindNodeByKeyValue(e.GetValue("CodigoConta").ToString());
                Control controle1 = e.Row.FindControl("btnExcluir");
                if (tln != null && tln.HasChildren)
                {
                    e.Row.BackColor = Color.FromName("#EAEAEA");//http://www.color-hex.com/color/eaeaea
                    e.Row.ForeColor = Color.Black;
                }
            }
        }
    }

    protected void tlCentroCustos_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
    {
        carregaGvDados();
    }

    protected void tlCentroCustos_ProcessDragNode(object sender, TreeListNodeDragEventArgs e)
    {
        bool retornoBanco = false;
        ((ASPxTreeList)sender).JSProperties["cpMensagemErro"] = "";
        string CodigoConta_NoArrastado = e.Node.GetValue("CodigoConta").ToString();//item que ta sendo arrastado
        string CodigoConta_NoDestino = (e.NewParentNode.GetValue("CodigoConta") == null) ? "" : e.NewParentNode.GetValue("CodigoConta").ToString();//item em que aponta na hora de soltar

        if (string.IsNullOrEmpty(CodigoConta_NoDestino))
        {
            ((ASPxTreeList)sender).JSProperties["cpMensagemErro"] = Resources.traducao.planoContasFluxoCaixa_o_n__de_destino_n_o_pode_ser_nulo__neste_caso__exclua_este_n__e_cadastre_o_novamente_para_que_o_mesmo_possa_ser_um_n____raiz____a_opera__o_de_arrastar_e_soltar_n_o_foi_executada_;
            e.Cancel = true;
            e.Handled = false;
            return;
        }

        int conta = 0;
        string updateNos = string.Format(@"
        BEGIN

         declare @CodigoConta_NoDestino as int
         declare @CodigoConta_NoArrastado as int         
         
         set @CodigoConta_NoArrastado = {0}
         set @CodigoConta_NoDestino = {1}

           --Atualiza os nós
           UPDATE [PlanoContasFluxoCaixa]
              SET [CodigoContaSuperior] = @CodigoConta_NoDestino
            WHERE CodigoConta = @CodigoConta_NoArrastado

        END", CodigoConta_NoArrastado, CodigoConta_NoDestino);

        try
        {
            retornoBanco = CDados.execSQL(updateNos, ref conta);
            e.Handled = true;
        }
        catch (Exception ex)
        {
            ((ASPxTreeList)sender).JSProperties["cpMensagemErro"] = ex.Message;
            e.Cancel = true;
            e.Handled = false;
        }
        carregaGvDados();
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        DevExpress.Web.MenuItem btnIncluir = (sender as ASPxMenu).Items.FindByName("btnIncluir");
        btnIncluir.ToolTip = Resources.traducao.planoContasFluxoCaixa_incluir_uma_nova_conta_abaixo_da_conta_selecionada_;

        CDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), podeIncluir, "abrePopUp('Conta','Incluir');TipoOperacao = 'Incluir';", true, true, false, "PlaCon", "Centro de Custos", this);
    }

    protected void menu_ItemClick(object source, MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;
        CDados.exportaTreeList(ASPxTreeListExporter1, "XLS");
    }


    protected void pnTipoConta_Callback(object sender, CallbackEventArgsBase e)
    {
        string entradaSaida = e.Parameter;
        configTipoConta(entradaSaida);

    }

    protected void configTipoConta(string entradaSaida)
    {
        ListEditItem liIV = new ListEditItem(Resources.traducao.planoContasFluxoCaixa_investimento, "IV");
        ListEditItem liCF = new ListEditItem(Resources.traducao.planoContasFluxoCaixa_custo_fixo, "CF");
        ListEditItem liCV = new ListEditItem(Resources.traducao.planoContasFluxoCaixa_custo_vari_vel, "CV");
        ListEditItem liRC = new ListEditItem(Resources.traducao.planoContasFluxoCaixa_receita, "RC");
        cbTipoConta.Items.Clear();
        cbTipoConta.Text = "";
        if (entradaSaida == "E")
        {
            cbTipoConta.Items.Add(liRC);
        }
        if (entradaSaida == "S")
        {
            cbTipoConta.Items.Add(liIV);
            cbTipoConta.Items.Add(liCF);
            cbTipoConta.Items.Add(liCV);
        }
    }

    protected void tlCentroCustos_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
    {
        if (e.Column.Name == "TipoConta")
        {
            string textoInicial = e.CellValue.ToString();
            if (textoInicial == "IV")
            {
                e.Cell.Text = Resources.traducao.planoContasFluxoCaixa_investimento;
            }
            else if (textoInicial == "CF")
            {
                e.Cell.Text = Resources.traducao.planoContasFluxoCaixa_custo_fixo;
            }
            else if (textoInicial == "CV")
            {
                e.Cell.Text = Resources.traducao.planoContasFluxoCaixa_custo_vari_vel;
            }
            else if (textoInicial == "RC")
            {
                e.Cell.Text = Resources.traducao.planoContasFluxoCaixa_receita;
            }
        }
        if (e.Column.Name == "IndicaContaAnalitica")
        {
            string textoInicial = e.CellValue.ToString();
            if (textoInicial == "S")
            {
                e.Cell.Text = Resources.traducao.sim;
            }
            else if (textoInicial == "N")
            {
                e.Cell.Text = Resources.traducao.nao;
            }            
        }
    }

    protected void ASPxTreeListExporter1_RenderBrick(object sender, ASPxTreeListExportRenderBrickEventArgs e)
    {
        if (e.Column.FieldName == "TipoConta")
        {
            string textoInicial = e.Text;
            if (textoInicial == "IV")
            {
                e.TextValue = e.Text = Resources.traducao.planoContasFluxoCaixa_investimento;
            }
            else if (textoInicial == "CF")
            {
                e.TextValue = e.Text = Resources.traducao.planoContasFluxoCaixa_custo_fixo;
            }
            else if (textoInicial == "CV")
            {
                e.TextValue = e.Text = Resources.traducao.planoContasFluxoCaixa_custo_vari_vel;
            }
            else if (textoInicial == "RC")
            {
                e.TextValue = e.Text = Resources.traducao.planoContasFluxoCaixa_receita;
            }
        }
        if (e.Column.Name == "IndicaContaAnalitica")
        {
            string textoInicial = e.Text;
            if (textoInicial == "S")
            {
                e.TextValue = Resources.traducao.sim;
            }
            else if (textoInicial == "N")
            {
                e.TextValue = Resources.traducao.nao;
            }
        }

    }
}