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

public partial class _Estrategias_wizard_desenhaMapaEstrategico : System.Web.UI.Page
{
    dados cDados;
    int codigoUsuarioResponsavel;
    int idObjetoJS = 0; // será usado no hfGeral
    int codigoMapaEstrategico = -1;
    int codigoVersaoMapaEstrategico = 1;
    Panel pnUltimaPerspectivaInserida = new Panel(); //null;

    enum TipoObjetoEstrategia
    {
        Mapa, Missao, Visao, Perspectiva, Perspectiva1, Perspectiva2, Perspectiva3, Perspectiva4, Objetivo, Tema, CausaEfeito
    };

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        cDados.aplicaEstiloVisual(Page);
        //codigoUsuarioResponsavel = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());

        if (Request.QueryString["CUR"] != null)
            codigoUsuarioResponsavel = int.Parse(Request.QueryString["CUR"].ToString());

        if (Request.QueryString["CME"] != null)
            codigoMapaEstrategico = int.Parse(Request.QueryString["CME"].ToString());

        if (!IsPostBack && !IsCallback)
        {
            Page.Title = "Desenho do Mapa Estrátégico";
            lblTituloTela.Text = "Desenho do Mapa Estratégigo";

            dvDesktop.Height = new Unit("700px");

            insereObjetoMapaEstrategico(codigoMapaEstrategico);
        }
    }

    private void insereObjetoMapaEstrategico(int codigoMapaEstrategico)
    {
        string comandoSQL = string.Format(
            @"SELECT TOE.IniciaisTipoObjeto, OE.CodigoObjetoEstrategia, OE.CodigoMapaEstrategico, OE.CodigoVersaoMapaEstrategico, OE.TituloObjetoEstrategia, 
                     OE.DescricaoObjetoEstrategia, OE.AlturaObjetoEstrategia, OE.LarguraObjetoEstrategia, OE.TopoObjetoEstrategia, OE.EsquerdaObjetoEstrategia, 
                     OE.CorFundoObjetoEstrategia, OE.CorBordaObjetoEstrategia, OE.CorFonteObjetoEstrategia, OE.CodigoTipoObjetoEstrategia, OE.OrdemObjeto, 
                     OE.CodigoObjetoEstrategiaSuperior, OE.GlossarioObjeto, OE.DataInclusao, OE.CodigoUsuarioInclusao, OE.DataUltimaAlteracao, OE.CodigoUsuarioUltimaAlteracao, 
                     OE.DataExclusao, OE.CodigoUsuarioExclusao, 
                     CASE WHEN OE.CodigoObjetoEstrategiaSuperior IS NULL THEN RIGHT('000000' + CONVERT(Varchar, OE.CodigoObjetoEstrategia), 6) 
                         WHEN TOE.NivelObjetoEstruturaHierarquica = 2 THEN RIGHT('000000' + CONVERT(Varchar, OE.CodigoObjetoEstrategiaSuperior), 6) + '.' + RIGHT('000000' + CONVERT(Varchar, oe.CodigoObjetoEstrategia), 6) 
                         ELSE RIGHT('000000' + CONVERT(Varchar, oe.CodigoObjetoEstrategia), 6) 
                     END AS Sequencia
                FROM ObjetoEstrategia AS OE INNER JOIN
                     TipoObjetoEstrategia AS TOE ON OE.CodigoTipoObjetoEstrategia = TOE.CodigoTipoObjetoEstrategia
               WHERE OE.CodigoMapaEstrategico = {0}
                 AND OE.CodigoVersaoMapaEstrategico = 1
                 AND OE.DataExclusao is null
               ORDER BY Sequencia, OE.OrdemObjeto", codigoMapaEstrategico);

        DataSet ds = cDados.getDataSet(comandoSQL);
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
        {
            byte ordemPerspectiva = 1;
            int posSuperiorDefault = 5;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int codigoObjetoEstrategia = int.Parse(dr["codigoObjetoEstrategia"].ToString());
                int posEsquerda = (dr["EsquerdaObjetoEstrategia"].ToString() != "") ? int.Parse(dr["EsquerdaObjetoEstrategia"].ToString()) : 5;
                int posSuperior = (dr["TopoObjetoEstrategia"].ToString() != "") ? int.Parse(dr["TopoObjetoEstrategia"].ToString()) : posSuperiorDefault;
                int largura = (dr["LarguraObjetoEstrategia"].ToString() != "") ? int.Parse(dr["LarguraObjetoEstrategia"].ToString()) : 780;
                int altura = (dr["AlturaObjetoEstrategia"].ToString() != "") ? int.Parse(dr["AlturaObjetoEstrategia"].ToString()) : 35;
                string textoInterno = (dr["DescricaoObjetoEstrategia"].ToString() != "") ? dr["DescricaoObjetoEstrategia"].ToString() : "";
                string textoIdentificadoObjeto = (dr["TituloObjetoEstrategia"].ToString() != "") ? dr["TituloObjetoEstrategia"].ToString() : "";
                string corFundo = (dr["CorFundoObjetoEstrategia"].ToString() != "") ? dr["CorFundoObjetoEstrategia"].ToString() : "#ccffcc"; // cor clara, parece um ciano
                string corBorda = (dr["CorBordaObjetoEstrategia"].ToString() != "") ? dr["CorBordaObjetoEstrategia"].ToString() : "#000080"; // azul escuro
                string corFonte = (dr["CorFonteObjetoEstrategia"].ToString() != "") ? dr["CorFonteObjetoEstrategia"].ToString() : "#000000"; // preto
                if (dr["IniciaisTipoObjeto"].ToString() == "MAP")
                {
                    textoIdentificadoObjeto = "";
                    insereObjetoEstrategia(TipoObjetoEstrategia.Mapa, codigoObjetoEstrategia, posEsquerda, posSuperior, largura, altura, textoInterno, textoIdentificadoObjeto, corFundo, corBorda, corFonte, false);
                    posSuperiorDefault += 80;
                }

                else if (dr["IniciaisTipoObjeto"].ToString() == "VIS")
                {
                    insereObjetoEstrategia(TipoObjetoEstrategia.Visao, codigoObjetoEstrategia, posEsquerda, posSuperior, largura, altura, textoInterno, textoIdentificadoObjeto, corFundo, corBorda, corFonte, false);
                    posSuperiorDefault += 80;
                }
                else if (dr["IniciaisTipoObjeto"].ToString() == "MIS")
                {
                    insereObjetoEstrategia(TipoObjetoEstrategia.Missao, codigoObjetoEstrategia, posEsquerda, posSuperior, largura, altura, textoInterno, textoIdentificadoObjeto, corFundo, corBorda, corFonte, false);
                    posSuperiorDefault += 80;
                }
                else if (dr["IniciaisTipoObjeto"].ToString() == "PSP")
                {
                  /*  TipoObjetoEstrategia toeTemp = new TipoObjetoEstrategia();
                    if (ordemPerspectiva == 1)
                        toeTemp = TipoObjetoEstrategia.Perspectiva1;
                    else if (ordemPerspectiva == 2)
                        toeTemp = TipoObjetoEstrategia.Perspectiva2;
                    else if (ordemPerspectiva == 3)
                        toeTemp = TipoObjetoEstrategia.Perspectiva3;
                    else if (ordemPerspectiva == 4)
                        toeTemp = TipoObjetoEstrategia.Perspectiva4;
                    */
                    // perspectiva não tem o texto interno
                    textoInterno = "";

                    pnUltimaPerspectivaInserida = insereObjetoEstrategia(TipoObjetoEstrategia.Perspectiva, codigoObjetoEstrategia, posEsquerda, posSuperior, largura, altura, textoInterno, textoIdentificadoObjeto, corFundo, corBorda, corFonte, false);
                    //hfGeral.Add(toeTemp.ToString(), codigoObjetoEstrategia);
                    hfGeral.Add("Psp" + codigoObjetoEstrategia, codigoObjetoEstrategia);
                    ordemPerspectiva++;
                    posSuperiorDefault += 80;
                }
                else if (dr["IniciaisTipoObjeto"].ToString() == "OBJ")
                {
                    largura = (dr["LarguraObjetoEstrategia"].ToString() != "") ? int.Parse(dr["LarguraObjetoEstrategia"].ToString()) : 150;
                    posSuperior = (dr["TopoObjetoEstrategia"].ToString() != "") ? int.Parse(dr["TopoObjetoEstrategia"].ToString()) : (pnUltimaPerspectivaInserida.Controls.Count * 5 + 5);
                    Panel pnObjetivo = insereObjetoEstrategia(TipoObjetoEstrategia.Objetivo, codigoObjetoEstrategia, posEsquerda, posSuperior, largura, altura, textoInterno, textoIdentificadoObjeto, corFundo, corBorda, corFonte, false);
                    pnUltimaPerspectivaInserida.Controls.Add(pnObjetivo);
                }
                else if (dr["IniciaisTipoObjeto"].ToString() == "TEM")
                {
                    largura = (dr["LarguraObjetoEstrategia"].ToString() != "") ? int.Parse(dr["LarguraObjetoEstrategia"].ToString()) : 200;
                    altura = (dr["AlturaObjetoEstrategia"].ToString() != "") ? int.Parse(dr["AlturaObjetoEstrategia"].ToString()) : 20;
                    posSuperior = (dr["TopoObjetoEstrategia"].ToString() != "") ? int.Parse(dr["TopoObjetoEstrategia"].ToString()) : (pnUltimaPerspectivaInserida.Controls.Count * 5 + 5);
                    Panel pnTema = insereObjetoEstrategia(TipoObjetoEstrategia.Tema, codigoObjetoEstrategia, posEsquerda, posSuperior, largura, altura, textoInterno, textoIdentificadoObjeto, corFundo, corBorda, corFonte, false);
                    pnUltimaPerspectivaInserida.Controls.Add(pnTema);
                }
                else if (dr["IniciaisTipoObjeto"].ToString() == "CEF")
                {
                    System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                    img.ID = textoIdentificadoObjeto + "_" + idObjetoJS;
                    img.Attributes.Add("style", string.Format("left: {0}px; top: {1}px; width: {2}px; height: {3}px;z-index: 1;", posEsquerda, posSuperior, largura, altura));
                    img.Attributes.Add("class", "moveable bordaBox");
                    img.Attributes.Add("onclick", "selecionaObjeto(this)");

                    if (textoIdentificadoObjeto == "ImgSC")
                        img.ImageUrl = "~/imagens/wizardMapaEstrategico/SetaCima.png";
                    else if (textoIdentificadoObjeto == "ImgSB")
                        img.ImageUrl = "~/imagens/wizardMapaEstrategico/SetaBaixo.png";
                    else if (textoIdentificadoObjeto == "ImgSD")
                        img.ImageUrl = "~/imagens/wizardMapaEstrategico/SetaDir.png";
                    else if (textoIdentificadoObjeto == "ImgSE")
                        img.ImageUrl = "~/imagens/wizardMapaEstrategico/SetaEsq.png";
                    else if (textoIdentificadoObjeto == "ImgSFC")
                        img.ImageUrl = "~/imagens/wizardMapaEstrategico/SetaFinaCima.png";
                    else if (textoIdentificadoObjeto == "ImgSFB")
                        img.ImageUrl = "~/imagens/wizardMapaEstrategico/SetaFinaBaixo.png";
                    else if (textoIdentificadoObjeto == "ImgSFD")
                        img.ImageUrl = "~/imagens/wizardMapaEstrategico/SetaFinaDir.png";
                    else if (textoIdentificadoObjeto == "ImgSFE")
                        img.ImageUrl = "~/imagens/wizardMapaEstrategico/SetaFinaEsq.png";
                    
                    dvDesktop.Controls.Add(img);

                    // registra no hfGeral a existência do objeto
                    hfGeral.Add("o" + idObjetoJS.ToString() + "_c", codigoObjetoEstrategia);
                    hfGeral.Add("o" + idObjetoJS.ToString() + "_n", img.ID);
                    idObjetoJS++;

                }

            }
        }
        hfGeral.Add("maxID", idObjetoJS);
    }

    private Panel insereObjetoEstrategia(TipoObjetoEstrategia tipoObjeto, int codigoObjetoEstrategia, int posEsquerda, int posSuperior, int largura, int altura, string textoInterno, string textoIdentificadorObjeto, string corFundo, string corBorda, string corFonte, bool mostrarIdentificadorExternamente)
    {
        Panel pnTituloMapa = new Panel();
        // Para os objetivos, a identificação sera o prefixo "Obj" + nomePerspectiva + sequencial
        if (tipoObjeto == TipoObjetoEstrategia.Perspectiva)
            pnTituloMapa.ID = "Psp" + codigoObjetoEstrategia;
        else if (tipoObjeto == TipoObjetoEstrategia.Objetivo)
            pnTituloMapa.ID = "Obj" + pnUltimaPerspectivaInserida.ID + "_" + idObjetoJS;
        else if (tipoObjeto == TipoObjetoEstrategia.Tema)
            pnTituloMapa.ID = "Tem" + pnUltimaPerspectivaInserida.ID + "_" + idObjetoJS;
        else
            pnTituloMapa.ID = tipoObjeto.ToString();

        pnTituloMapa.Attributes.Add("class", "moveable bordaBox");
        pnTituloMapa.Attributes.Add("style", string.Format("left: {0}px; top: {1}px; width: {2}px; z-index: 1;", posEsquerda, posSuperior, largura));
        pnTituloMapa.Attributes.Add("onclick", "selecionaObjeto(this)");
        pnTituloMapa.Attributes.Add("onkeydown", "removeElemento(this)");

        // Se tem texto para identificar o objeto, ajusta sua posição
        string htmlIdentificadorObjeto = "";
        if (textoIdentificadorObjeto != "")
        {
            if (mostrarIdentificadorExternamente)
                pnTituloMapa.Controls.Add(cDados.getLiteral(string.Format(
                    @"<span id='Titulo{0}'>{1}</span>", pnTituloMapa.ID, textoIdentificadorObjeto)));
            else
                htmlIdentificadorObjeto = string.Format(
                    @"<tr><td id='Titulo{0}' valign='top'>{1}</td></tr>", pnTituloMapa.ID, textoIdentificadorObjeto);
        }

        // Se for "TEMA", não tem bordas Arredondadas
        if (tipoObjeto != TipoObjetoEstrategia.Tema)
        {
            // bordas superiores
            pnTituloMapa.Controls.Add(cDados.getLiteral(string.Format(
                @"<b id='b1s{0}' class='b1' style='background:{2}'></b>
              <b id='b2s{0}' class='b2' style='background:{1}; border-color:{2}'></b>
              <b id='b3s{0}' class='b3' style='background:{1}; border-color:{2}'></b>
              <b id='b4s{0}' class='b4' style='background:{1}; border-color:{2}'></b>"
                , pnTituloMapa.ID, corFundo, corBorda)));
        }

        string htmlTabelaInterna = string.Format(
            @"<table id='tb{0}' class='{7}' style='width: 100%; height: {1}px;background:{3}; border-color:{4}; color:{5}'>
                    {6}
                    <tr>
                        <td id='Conteudo{0}' align='center'>
                            <span id='Texto{0}'>{2}</span></td>
                    </tr>
                </table>", pnTituloMapa.ID, altura, textoInterno, corFundo, corBorda, corFonte, htmlIdentificadorObjeto, (tipoObjeto != TipoObjetoEstrategia.Tema) ? "conteudo" : "conteudoBordaQuad");
        pnTituloMapa.Controls.Add(cDados.getLiteral(htmlTabelaInterna));

        // Se for "TEMA", não tem bordas Arredondadas
        if (tipoObjeto != TipoObjetoEstrategia.Tema)
        {
            // bordas inferiores
            pnTituloMapa.Controls.Add(cDados.getLiteral(string.Format(
                @"<b id='b4i{0}'class='b4' style='background:{1}; border-color:{2}'></b>
              <b id='b3i{0}'class='b3' style='background:{1}; border-color:{2}'></b>
              <b id='b2i{0}'class='b2' style='background:{1}; border-color:{2}'></b>
              <b id='b1i{0}'class='b1' style='background:{2}'></b>"
                , pnTituloMapa.ID, corFundo, corBorda)));
        }

        // o objetivo não sera inseridos dentro da área de trabalho e sim dentro da última perspectiva
        if (tipoObjeto != TipoObjetoEstrategia.Objetivo)
            dvDesktop.Controls.Add(pnTituloMapa);

        // registra no hfGeral a existência do objeto
        hfGeral.Add("o" + idObjetoJS.ToString() + "_c", codigoObjetoEstrategia);
        hfGeral.Add("o" + idObjetoJS.ToString() + "_n", pnTituloMapa.ID);
        idObjetoJS++;

        return pnTituloMapa;
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        processaAlteracoesDesenhoMapa();
        hfGeral.Clear();
        insereObjetoMapaEstrategico(codigoMapaEstrategico);
    }

    // Este evento foi substituido pelo clique no botão Salvar
    protected void hfGeral_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        processaAlteracoesDesenhoMapa();
    } 
    

    protected void processaAlteracoesDesenhoMapa()
    {
        string mensagemErro_Persistencia = "";
        string comandoSQLGeral = "";

        // Lê as informações dos objetos
        int maxID = int.Parse(hfGeral.Get("maxID").ToString());
        for (int id = 0; id < maxID; id++)
        {
            int codigoObjetoEstrategia = -1;
            string tipoObjeto = "";
            string modoOperacao = "";
            if (hfGeral.Contains("o" + id + "_c"))
            {
                modoOperacao = "Incluir";

                // primeiro testa se o objeto esta marcado para exclusão e se ele ja possui código
                if (hfGeral.Contains("o" + id + "_x") && hfGeral.Get("o" + id + "_x").ToString() == "S")
                {
                    // se o objeto não possui código, deve ser ignorado.
                    if (hfGeral.Get("o" + id + "_c").ToString() == "")
                        continue;

                    codigoObjetoEstrategia = int.Parse(hfGeral.Get("o" + id + "_c").ToString());
                    modoOperacao = "Excluir";

                }
                // se não é para excluir, testa se o objeto pode ser editado. Se tem codigo é pq já existe.
                else if (hfGeral.Get("o" + id + "_c").ToString() != "")
                {
                    codigoObjetoEstrategia = int.Parse(hfGeral.Get("o" + id + "_c").ToString());
                    modoOperacao = "Editar";
                }

                tipoObjeto = hfGeral.Get("o" + id + "_n").ToString();

                string informacoes = hfGeral.Get("o" + id + "_i").ToString();

                string[] aInfo = informacoes.Split('¥');

                if (modoOperacao == "Incluir")
                {
                    int CodigoObjetoEstrategiaSuperior = -1;
                    if (hfGeral.Contains("o" + id + "_p"))
                        CodigoObjetoEstrategiaSuperior = int.Parse(hfGeral.Get("o" + id + "_p").ToString());

                    comandoSQLGeral += getComandoSQL_incluirObjetoEstrategia(tipoObjeto, CodigoObjetoEstrategiaSuperior, aInfo);
                }
                else if (modoOperacao == "Editar")
                    comandoSQLGeral += getComandoSQL_editarObjetoEstrategia(codigoObjetoEstrategia, tipoObjeto, aInfo);
                else
                    comandoSQLGeral += getComandoSQL_excluirObjetoEstrategia(codigoObjetoEstrategia);

            }
        }

        if (comandoSQLGeral != "")
        {
            // insere o Controle de transação
            // ------------------------------
            comandoSQLGeral =
                @"BEGIN
                    DECLARE	@QtdErros int
                        SET @QtdErros = 0

                    BEGIN TRAN t1

                 " + comandoSQLGeral +
                @"
                    if @QtdErros = 0 
	                    Commit tran t1
                    else
	                    rollback tran t1

                  END ";

            //mensagemErro_Persistencia = "";
            mensagemErro_Persistencia = persisteObjetosEstrategia(comandoSQLGeral);
            hfGeral.Set("StatusSalvar", "0");

            if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
                hfGeral.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
            else // alguma coisa deu errado...
                hfGeral.Set("ErroSalvar", mensagemErro_Persistencia);
        }
    }

    private string getComandoSQL_incluirObjetoEstrategia(string tipoObjeto, int CodigoObjetoEstrategiaSuperior, string[] aInfo)
    {
        string IniciaisTipoObjeto = "";
        if (tipoObjeto.Substring(0, 3).ToUpper() == "PSP")
            IniciaisTipoObjeto = "PSP";
        else if (tipoObjeto.Substring(0, 3).ToUpper() == "OBJ")
            IniciaisTipoObjeto = "OBJ";
        else if (tipoObjeto.Substring(0, 3).ToUpper() == "TEM")
            IniciaisTipoObjeto = "TEM";
        else if (tipoObjeto.Substring(0, 3).ToUpper() == "IMG")
            IniciaisTipoObjeto = "CEF";


        if (IniciaisTipoObjeto != "")
        {
            int codigoTipoObjetoEstrategico;
            DataSet ds = cDados.getTipoObjetoEstrategia(IniciaisTipoObjeto);
            if (ds == null)
                return "";

            codigoTipoObjetoEstrategico = int.Parse(ds.Tables[0].Rows[0]["CodigoTipoObjetoEstrategia"].ToString());

            string comandoSQLLocal = string.Format(
            @" -- Inclui um novo objeto ------------------------------
             IF @QtdErros = 0 
             BEGIN
                  INSERT INTO ObjetoEstrategia 
                        ( TituloObjetoEstrategia, DescricaoObjetoEstrategia,  
                          EsquerdaObjetoEstrategia, TopoObjetoEstrategia, LarguraObjetoEstrategia, AlturaObjetoEstrategia, 
                          CorFundoObjetoEstrategia, CorBordaObjetoEstrategia, CorFonteObjetoEstrategia, CodigoObjetoEstrategiaSuperior,
                          CodigoMapaEstrategico, CodigoVersaoMapaEstrategico, CodigoTipoObjetoEstrategia, DataInclusao, CodigoUsuarioInclusao
                        )
                   VALUES 
                        (  {0}, {1},
                           {2}, {3}, {4}, {5},
                           {6}, {7}, {8}, {9},
                           {10}, {11}, {12}, getdate(), {13}
                        )

                    SET @QtdErros = @QtdErros + @@Error
             END

            ", aInfo[0] == "" ? "null" : "'" + aInfo[0] + "'",
               aInfo[1] == "" ? "null" : "'" + aInfo[1] + "'",
               aInfo[2] == "" ? "null" : aInfo[2],
               aInfo[3] == "" ? "null" : aInfo[3],
               aInfo[4] == "" ? "null" : aInfo[4],
               aInfo[5] == "" ? "null" : aInfo[5],
               aInfo[6] == "" ? "null" : "'" + aInfo[6] + "'",
               aInfo[7] == "" ? "null" : "'" + aInfo[7] + "'",
               aInfo[8] == "" ? "null" : "'" + aInfo[8] + "'",
               CodigoObjetoEstrategiaSuperior <= 0 ? "null" : CodigoObjetoEstrategiaSuperior.ToString(),
               codigoMapaEstrategico, codigoVersaoMapaEstrategico, codigoTipoObjetoEstrategico, codigoUsuarioResponsavel);

            return comandoSQLLocal;
        }
        else
            return "";

    }

    private string getComandoSQL_editarObjetoEstrategia(int codigoObjetoEstrategia, string tipoObjeto, string[] aInfo)
    {
        string colunaTituloObjeto = string.Format(
            ", TituloObjetoEstrategia = {0}", aInfo[0] == "" ? "null" : "'" + aInfo[0] + "'");

        // o objeto Mapa não pode ter o título Alterado
        if (tipoObjeto == TipoObjetoEstrategia.Mapa.ToString())
            colunaTituloObjeto = "";

        string comandoSQLLocal = string.Format(
            @" -- Atualiza objeto ------------------------------
             IF @QtdErros = 0 
             BEGIN
                  UPDATE ObjetoEstrategia 
                     SET DescricaoObjetoEstrategia = {1}
                         {2} -- Coluna TituloObjeto
                       , EsquerdaObjetoEstrategia  = {3}
                       , TopoObjetoEstrategia      = {4}
                       , LarguraObjetoEstrategia   = {5}
                       , AlturaObjetoEstrategia    = {6}
                       , CorFundoObjetoEstrategia  = {7}
                       , CorBordaObjetoEstrategia  = {8}
                       , CorFonteObjetoEstrategia  = {9}
                       , DataUltimaAlteracao       = Getdate()
                       , CodigoUsuarioUltimaAlteracao = {10}
                   WHERE codigoObjetoEstrategia = {0}

                   SET @QtdErros = @QtdErros + @@Error
             END

             ", codigoObjetoEstrategia,
                aInfo[1] == "" ? "null" : "'" + aInfo[1] + "'",
                colunaTituloObjeto, 
                aInfo[2] == "" ? "null" : aInfo[2],
                aInfo[3] == "" ? "null" : aInfo[3],
                aInfo[4] == "" ? "null" : aInfo[4],
                aInfo[5] == "" ? "null" : aInfo[5],
                aInfo[6] == "" ? "null" : "'" + aInfo[6] + "'",
                aInfo[7] == "" ? "null" : "'" + aInfo[7] + "'",
                aInfo[8] == "" ? "null" : "'" + aInfo[8] + "'",
                codigoUsuarioResponsavel );

        return comandoSQLLocal;
    }

    private string getComandoSQL_excluirObjetoEstrategia(int codigoObjetoEstrategia)
    {
        string comandoSQLLocal = string.Format(
            @" -- Excluir objeto ------------------------------
             IF @QtdErros = 0 
             BEGIN
                UPDATE ObjetoEstrategia 
                   SET DataExclusao = GetDate()
                     , CodigoUsuarioExclusao = {1}
                 WHERE codigoObjetoEstrategia = {0}

                   SET @QtdErros = @QtdErros + @@Error
             END

             ", codigoObjetoEstrategia, codigoUsuarioResponsavel);

        return comandoSQLLocal;
    }

    private string persisteObjetosEstrategia(string comandoSQL)
    {
        try
        {
            int afetados = 0;
            cDados.execSQL(comandoSQL, ref afetados);
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

   
}
