using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class _Projetos_DadosProjeto_agrupamentoContratoMaster_NE_OS : System.Web.UI.Page
{
    dados cDados;
    private int idUsuarioLogado;
    private int CodigoEntidade;
    bool podeEditar;
    bool podeExcluir;
    string dbName = "";
    string dbOwner = "";

    protected void Page_Init(object sender, EventArgs e)
    {
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

        idUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString()); //usuario logado.
        CodigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());   //entidad logada.

        this.Title = cDados.getNomeSistema();
        podeEditar = true;
        podeExcluir = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HeaderOnTela();

        dbName = cDados.getDbName();
        dbOwner = cDados.getDbOwner();

        defineAlturaTela(cDados.getInfoSistema("ResolucaoCliente").ToString());

        carregaGrid();

        cDados.aplicaEstiloVisual(Page);
       
        if (!IsPostBack)
        {
            cDados.excluiNiveisAbaixo(1);
            cDados.insereNivel(1, this);
            Master.geraRastroSite();
        }
    }


    private void defineAlturaTela(string resolucaoCliente)
    {
        int largura = 0;
        int altura = 0;
        cDados.getLarguraAlturaTela(resolucaoCliente, out largura, out altura);
        gvDados.Settings.VerticalScrollableHeight = altura - 340;
    }

    private void HeaderOnTela()
    {
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/agrupamentoContratoMaster_NE_OS.js""></script>"));
        Header.Controls.Add(cDados.getLiteral(@"<script type=""text/javascript"" language=""javascript"" src=""../../scripts/barraNavegacao.js""></script>"));
        this.TH(this.TS("barraNavegacao", "agrupamentoContratoMaster_NE_OS"));
    }
    // retorna a primary key da tabela.
    private string getChavePrimaria()
    {
        string retorno = "";
        if (gvDados.FocusedRowIndex >= 0)
        {
            retorno = gvDados.GetRowValues(gvDados.FocusedRowIndex, gvDados.KeyFieldName).ToString();
            gvDados.FocusedRowIndex = gvDados.FindVisibleIndexByKeyValue(retorno);
        }
        else
        {
            retorno = "";
        }
        return retorno;
    }

    public void carregaGrid()
    {
        string comandoSQL = string.Format(@"
        SELECT [CodigoContratoEspecial]
              ,[DescricaoContrato]
              ,[ValorContratado]
              ,[ValorContratadoReaj]
              ,[ValorRealizado]
              ,[ValorRealizadoReaj]
  FROM {0}.{1}.[ContratosEspeciais]", cDados.getDbName(), cDados.getDbOwner());

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {

            gvDados.DataSource = ds.Tables[0];
            gvDados.DataBind();
        }
    }

    protected void menu_Init(object sender, EventArgs e)
    {
        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.setaDefinicoesBotoesInserirExportar((sender as ASPxMenu), true, "onClickBarraNavegacao('Incluir', gvDados, pcDados); TipoOperacao = 'Incluir';", true, true, false, "ContratMaster", lblTituloTela.Text, this);
    }
    protected void menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string parameter = e.Item.Text == "" ? "XLS" : e.Item.Text;

        System.Collections.Specialized.OrderedDictionary listaParametrosDados = new System.Collections.Specialized.OrderedDictionary();

        listaParametrosDados["RemoteIPUsuario"] = Session["RemoteIPUsuario"] + "";
        listaParametrosDados["NomeUsuario"] = Session["NomeUsuario"] + "";

        cDados = CdadosUtil.GetCdados(listaParametrosDados);
        cDados.eventoClickMenu((source as ASPxMenu), parameter, ASPxGridViewExporter1, "ContratMaster");
    }
    protected void gvDados_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
    {
        e.ErrorText = e.Exception.Message;
    }

    private string persisteExclusaoRegistro()
    {
        string mensagemErro = "";
        string beginTranSQL = cDados.geraBlocoBeginTran();
        string endTranSQL = cDados.geraBlocoEndTran();

        string codigoContratoEspecial = getChavePrimaria();

        string comandosql = string.Format(@"
       DELETE FROM {0}.{1}.[ContratosEspeciais]
      WHERE CodigoContratoEspecial = {2}", cDados.getDbName(), cDados.getDbOwner(), codigoContratoEspecial);

        DataSet ds = cDados.getDataSet(beginTranSQL + " " + comandosql + " " + endTranSQL);

        mensagemErro = ds.Tables[0].Rows[0][0].ToString();
        return mensagemErro;
    }

    private string persisteEdicaoRegistro()
    {
        string beginTranSQL = cDados.geraBlocoBeginTran();
        string endTranSQL = cDados.geraBlocoEndTran();
        string chavePrimaria = getChavePrimaria();

        string DescricaoContrato = txtDescricaoContrato.Text;
        decimal ValorContratado = (txtValorContratado.Value == null) ? (decimal)0.0 : (decimal)txtValorContratado.Value;
        decimal ValorContratadoReaj = (txtValorContratadoReaj.Value == null) ? (decimal)0.0 : (decimal)txtValorContratadoReaj.Value;
        decimal ValorRealizado = (txtValorRealizado.Value == null) ? (decimal)0.0 : (decimal)txtValorRealizado.Value;
        decimal ValorRealizadoReaj = (txtValorRealizadoReaj.Value == null) ? (decimal)0.0 : (decimal)txtValorRealizadoReaj.Value;


        string comandosql = string.Format(@"
        UPDATE {0}.{1}.[ContratosEspeciais]
                   SET [DescricaoContrato] = '{2}'
                      ,[ValorContratado] = {3}
                      ,[ValorContratadoReaj] = {4}
                      ,[ValorRealizado] = {5}
                      ,[ValorRealizadoReaj] = {6}
        WHERE CodigoContratoEspecial = {7}", cDados.getDbName(), cDados.getDbOwner(), DescricaoContrato, ValorContratado.ToString().Replace(',', '.'), ValorContratadoReaj.ToString().Replace(',', '.'), ValorRealizado.ToString().Replace(',', '.'), ValorRealizadoReaj.ToString().Replace(',', '.'), chavePrimaria);
        DataSet ds = cDados.getDataSet(beginTranSQL + " " + comandosql + " " + endTranSQL);

        string mensagemErro = ds.Tables[0].Rows[0][0].ToString();
        return mensagemErro;
    }

    private string persisteInclusaoRegistro()
    {
        string beginTranSQL = cDados.geraBlocoBeginTran();
        string endTranSQL = cDados.geraBlocoEndTran();
        string chavePrimaria = getChavePrimaria();

        string DescricaoContrato = txtDescricaoContrato.Text;
        decimal ValorContratado = (txtValorContratado.Value == null) ? (decimal)0.0 : (decimal)txtValorContratado.Value;
        decimal ValorContratadoReaj = (txtValorContratadoReaj.Value == null) ? (decimal)0.0 : (decimal)txtValorContratadoReaj.Value;
        decimal ValorRealizado = (txtValorRealizado.Value == null) ? (decimal)0.0 : (decimal)txtValorRealizado.Value;
        decimal ValorRealizadoReaj = (txtValorRealizadoReaj.Value == null) ? (decimal)0.0 : (decimal)txtValorRealizadoReaj.Value;


        string comandosql = string.Format(@"
        INSERT INTO {0}.{1}.[ContratosEspeciais]([DescricaoContrato]
           ,[ValorContratado],[ValorContratadoReaj]
           ,[ValorRealizado],[ValorRealizadoReaj])
        VALUES('{2}'        
               ,{3}         ,{4}
               ,{5}         ,{6})", cDados.getDbName(), cDados.getDbOwner(), DescricaoContrato, ValorContratado.ToString().Replace(',', '.'), ValorContratadoReaj.ToString().Replace(',', '.'), ValorRealizado.ToString().Replace(',', '.'), ValorRealizadoReaj.ToString().Replace(',', '.'), chavePrimaria);
        DataSet ds = cDados.getDataSet(beginTranSQL + " " + comandosql + " " + endTranSQL);

        string mensagemErro = ds.Tables[0].Rows[0][0].ToString();
        return mensagemErro;
    }


    protected void gvDados_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string mensagemErro_Persistencia = "";


        ((ASPxGridView)sender).JSProperties["cp_OperacaoOk"] = "";

        if (e.Parameters == "Incluir")
        {
            mensagemErro_Persistencia = persisteInclusaoRegistro();
        }
        if (e.Parameters == "Editar")
        {
            mensagemErro_Persistencia = persisteEdicaoRegistro();
        }
        if (e.Parameters == "Excluir")
        {
            mensagemErro_Persistencia = persisteExclusaoRegistro();
        }

        if (mensagemErro_Persistencia == "OK") // não deu erro durante o processo de persistência
        {
            hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            ((ASPxGridView)sender).JSProperties["cp_OperacaoOk"] = e.Parameters;
        }
        else
        {// alguma coisa deu errado...
            // se for erro de Chave Estrangeira (FK)
            if (mensagemErro_Persistencia.Contains("REFERENCE"))
            {
                mensagemErro_Persistencia = "O registro não pode ser excluído pois está sendo utilizado por outras tabelas.";
            }
            if (mensagemErro_Persistencia.Contains("UNIQUE KEY"))
            {
                mensagemErro_Persistencia = "Esta combinação de registros já existe no banco de dados.\nEscreva uma descrição diferente para o campo: \"Contrato\"";
            }
            hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
            ((ASPxGridView)sender).JSProperties["cp_ErroSalvar"] = mensagemErro_Persistencia;
        }

        carregaGrid();
    }
    protected void gvDados_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
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

    protected void pncb_txtValorContratado_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.Replace('.',',');
        string[] vetorParametros = parametro.Split('|');

        txtValorContratado.Text = vetorParametros[0];
        txtValorContratado.ClientEnabled = vetorParametros[1] == "S";

    }
    protected void pncb_txtValorContratadoReaj_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.Replace('.', ',');
        string[] vetorParametros = parametro.Split('|');

        txtValorContratadoReaj.Text = vetorParametros[0];
        txtValorContratadoReaj.ClientEnabled = (vetorParametros[1] == "S");

    }
    protected void pncb_txtValorRealizado_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.Replace('.', ',');
        string[] vetorParametros = parametro.Split('|');

        txtValorRealizado.Text = vetorParametros[0];
        txtValorRealizado.ClientEnabled = (vetorParametros[1] == "S");

    }
    protected void pncb_txtValorRealizadoReaj_Callback(object sender, CallbackEventArgsBase e)
    {
        string parametro = e.Parameter.Replace('.', ',');
        string[] vetorParametros = parametro.Split('|');

        txtValorRealizadoReaj.Text = vetorParametros[0];
        txtValorRealizadoReaj.ClientEnabled = (vetorParametros[1] == "S");

    }
}