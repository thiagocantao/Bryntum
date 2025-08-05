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

public partial class _Projetos_VisaoCorporativa_vc_004 : System.Web.UI.Page
{
    dados cDados;

    protected void Page_Init(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();
    }

    protected void Page_Load(object sender, EventArgs e)
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
        
        // a pagina não pode ser armazenada no cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        defineLarguraTela();

        //Função que gera o gráfico
        geraGrid();
    }

    private void defineLarguraTela()
    {
        int largura, altura;


        string ResolucaoCliente = cDados.getInfoSistema("ResolucaoCliente").ToString().ToString();
        largura = int.Parse(ResolucaoCliente.Substring(0, ResolucaoCliente.IndexOf('x')));
        altura = int.Parse(ResolucaoCliente.Substring(ResolucaoCliente.IndexOf('x') + 1));

        gvDados.Settings.VerticalScrollableHeight = (altura - 200) / 2 - 10;
        gvDados.Width = ((largura - 44) / 3);
    }

    //Função para geração do gráfico (Quantidade de Projetos por Desempenho) - PIZZA
    private void geraGrid()
    {
        gvDados.DataSource = getDataTableGrid();
        gvDados.DataBind();

    }

    private DataTable getDataTableGrid()
    {
        DataTable dtRetorno = new DataTable();

        dtRetorno.Columns.Add("Descricao");
        dtRetorno.Columns.Add("Coluna01");
        dtRetorno.Columns.Add("Coluna02");
        dtRetorno.Columns.Add("Coluna03");

        if (cDados.getInfoSistema("DataEstagio") != null && cDados.getInfoSistema("DataEstagio").ToString() != "")
        {
            string dataEstagio = cDados.getInfoSistema("DataEstagio").ToString();
            int diasMes = DateTime.DaysInMonth(int.Parse(dataEstagio.Substring(3)), int.Parse(dataEstagio.Substring(0, 2)));

            string data = diasMes + "/" + dataEstagio;

            int idUsuario = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
            int codigoEntidade = int.Parse(cDados.getInfoSistema("CodigoEntidade").ToString());
            
            DataSet ds = cDados.getDadosEstagios(codigoEntidade.ToString(), idUsuario, data);

            if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
            {
                DataRow dr = dtRetorno.NewRow();

                dr["Descricao"] = String.Format("{0:MMM/yyyy}", DateTime.Parse(ds.Tables[0].Rows[0]["DataReferencia"].ToString())).ToUpper();
                dr["Coluna01"] = String.Format("{0:n0}", double.Parse(ds.Tables[0].Rows[0]["QuantidadeTCE"].ToString()));
                dr["Coluna02"] = String.Format("{0:n0}", double.Parse(ds.Tables[0].Rows[0]["QuantidadeContratos"].ToString()));
                dr["Coluna03"] = String.Format("{0:n0}", double.Parse(ds.Tables[0].Rows[0]["QuantidadeConvenios"].ToString()));

                dtRetorno.Rows.Add(dr);

                if (ds.Tables[0].Rows.Count > 1)
                {
                    dr = dtRetorno.NewRow();
                    dr["Descricao"] = String.Format("{0:MMM/yyyy}", DateTime.Parse(ds.Tables[0].Rows[1]["DataReferencia"].ToString())).ToUpper();
                    dr["Coluna01"] = String.Format("{0:n0}", double.Parse(ds.Tables[0].Rows[1]["QuantidadeTCE"].ToString()));
                    dr["Coluna02"] = String.Format("{0:n0}", double.Parse(ds.Tables[0].Rows[1]["QuantidadeContratos"].ToString()));
                    dr["Coluna03"] = String.Format("{0:n0}", double.Parse(ds.Tables[0].Rows[1]["QuantidadeConvenios"].ToString()));

                    dtRetorno.Rows.Add(dr);

                    dr = dtRetorno.NewRow();

                    double variacaoColuna1, variacaoColuna2, variacaoColuna3;
                    double valorMesAnterior, valorMesAtual;

                    valorMesAnterior = double.Parse(ds.Tables[0].Rows[0]["QuantidadeTCE"].ToString());
                    valorMesAtual = double.Parse(ds.Tables[0].Rows[1]["QuantidadeTCE"].ToString());

                    variacaoColuna1 = valorMesAnterior == 0 ? 0 : (valorMesAtual - valorMesAnterior) / valorMesAnterior;

                    valorMesAnterior = double.Parse(ds.Tables[0].Rows[0]["QuantidadeContratos"].ToString());
                    valorMesAtual = double.Parse(ds.Tables[0].Rows[1]["QuantidadeContratos"].ToString());

                    variacaoColuna2 = valorMesAnterior == 0 ? 0 : (valorMesAtual - valorMesAnterior) / valorMesAnterior;

                    valorMesAnterior = double.Parse(ds.Tables[0].Rows[0]["QuantidadeConvenios"].ToString());
                    valorMesAtual = double.Parse(ds.Tables[0].Rows[1]["QuantidadeConvenios"].ToString());

                    variacaoColuna3 = valorMesAnterior == 0 ? 0 : (valorMesAtual - valorMesAnterior) / valorMesAnterior;

                    dr["Descricao"] = "Variação";
                    dr["Coluna01"] = String.Format("{0:n1}%", variacaoColuna1 * 100);
                    dr["Coluna02"] = String.Format("{0:n1}%", variacaoColuna2 * 100);
                    dr["Coluna03"] = String.Format("{0:n1}%", variacaoColuna3 * 100);

                    dtRetorno.Rows.Add(dr);
                }

                DateTime dataAnterior = DateTime.Parse(dataEstagio.Substring(0, 2) + "/" + dataEstagio).AddMonths(-1);

                string dataInicioAnterior = "01/" + dataAnterior.Month + "/" + dataAnterior.Year;
                string dataTerminoAnterior = DateTime.DaysInMonth(dataAnterior.Year,dataAnterior.Month) + "/"  + dataAnterior.Month + "/" + dataAnterior.Year;
                string dataInicioAtual = "01/" + dataEstagio;
                string dataTerminoAtual = diasMes + "/" + dataEstagio;

                DataSet dsVariacoes = cDados.getVariacoesEstagios("NULL", idUsuario, dataInicioAnterior, dataTerminoAnterior, dataInicioAtual, dataTerminoAtual);

                if (cDados.DataSetOk(dsVariacoes) && cDados.DataTableOk(dsVariacoes.Tables[0]))
                {
                    dr = dtRetorno.NewRow();

                    dr["Descricao"] = "Maior Alta";
                    dr["Coluna01"] = dsVariacoes.Tables[0].Rows[0]["MaxTCE"].ToString().Replace('.', ',');
                    dr["Coluna02"] = dsVariacoes.Tables[0].Rows[0]["MaxContratos"].ToString().Replace('.', ',');
                    dr["Coluna03"] = dsVariacoes.Tables[0].Rows[0]["MaxConvenios"].ToString().Replace('.', ',');

                    dtRetorno.Rows.Add(dr);

                    dr = dtRetorno.NewRow();

                    dr["Descricao"] = "Menor Alta";
                    dr["Coluna01"] = dsVariacoes.Tables[0].Rows[0]["MinTCE"].ToString().Replace('.', ',');
                    dr["Coluna02"] = dsVariacoes.Tables[0].Rows[0]["MinContratos"].ToString().Replace('.', ',');
                    dr["Coluna03"] = dsVariacoes.Tables[0].Rows[0]["MinConvenios"].ToString().Replace('.', ',');

                    dtRetorno.Rows.Add(dr);
                }
            }
        }

        return dtRetorno;
    }

    protected void gvDados_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.VisibleIndex > 2 && e.DataColumn.Index > 0)
        {
            if (e.CellValue.ToString().Contains("-"))
                e.Cell.Style.Add("Color", "Red");
            else
                e.Cell.Style.Add("Color", "Green");

            e.Cell.Style.Add("text-align", "center");
        }
    }
}
