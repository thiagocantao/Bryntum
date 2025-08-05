using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_StatusAtivacaoPortal : System.Web.UI.Page
{
    dados cDados;
    private int CodigoEntidade;
    private int idUsuarioLogado;

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


        //hfGeral.Set("CodigoEntidadeAtual", CodigoEntidade); // p/ controle de edição dos usuários listados
        //hfGeral.Set("idUsuarioLogado", idUsuarioLogado);
        
        this.Title = cDados.getNomeSistema();



    }

    protected void Page_Load(object sender, EventArgs e)
    {

        string comandoSQL = string.Format(@"select Valor from ParametroConfiguracaoSistema where Parametro = 'QuantidadeLicencasCliente' and codigoEntidade = {0}", CodigoEntidade);
        string comandoSQLContaUsuarios = string.Format(@"SELECT COUNT(DISTINCT u.CodigoUsuario)
                                                           FROM Usuario AS u INNER JOIN
                                                                UsuarioUnidadeNegocio AS uun ON (u.CodigoUsuario = uun.CodigoUsuario
                                                                        AND uun.IndicaUsuarioAtivoUnidadeNegocio = 'S'
																		AND u.DataExclusao IS NULL) INNER JOIN
                                                                           UnidadeNegocio AS un ON (un.CodigoUnidadeNegocio = uun.CodigoUnidadeNegocio 
                                                                           ANd un.IndicaUnidadeNegocioAtiva = 'S')");

        int LicencasAdquiridas = -1;
        int qtdUsuarios = -1;
        DataSet ds = cDados.getDataSet(comandoSQL);
        DataSet dsContaUsuarios = cDados.getDataSet(comandoSQLContaUsuarios);
        if(cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            LicencasAdquiridas = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            lblLicencasAdquiridas.Text = ds.Tables[0].Rows[0][0].ToString();
        }
        if (cDados.DataSetOk(dsContaUsuarios) && cDados.DataTableOk(dsContaUsuarios.Tables[0]))
        {
            qtdUsuarios = int.Parse(dsContaUsuarios.Tables[0].Rows[0][0].ToString());
            lblLicencasUtilizadas.Text = dsContaUsuarios.Tables[0].Rows[0][0].ToString();
        }

        lblLicencasDisponiveis.Text = (LicencasAdquiridas - qtdUsuarios).ToString();

    }
}