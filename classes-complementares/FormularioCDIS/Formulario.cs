using CDIS.Properties;
using DevExpress.Web;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
//using CDIS;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDIS
{
    public enum FormulariosPreDefinidos
    {
        Nenhum,
        NovaProposta,
        LicoesAprendidas
    }

    public class Formulario : System.Web.UI.Page
    {
        #region --- [Variáveis da Classe]

        private ClasseDados dados;
        private int codigoUsuarioResponsavel;
        private int codigoModeloFormulario;
        private Hashtable parametrosEntrada;
        private Unit width;
        private Unit height;
        private bool somenteLeitura;
        private Page page;
        private bool isPopUp;
        private bool mostrarTitulos;
        private bool isPostBack;
        private string cssFilePath;
        private string cssPostFix;
        private List<object[]> camposControladoSistema;
        private List<object[]> camposFormulario; // todos os campos serão adicionados aqui
        private Color corReadOnly = Color.LightYellow;

        private int codigoFormularioMaster;
        //private int codigoSubFormulario;
        private int codigoProjeto;
        private int codigoEntidade;
        private string nomeBDEmpresa;
        private string nomeOwnerEmpresa;
        private string inCodigosFormularios;
        private string iniciaisModeloFormulario;
        private bool indicaFormAssinado = false;

        private char DelimitadorPropriedadeCampo = '¥';
        private Hashtable retorno = new Hashtable();
        //private char operacaoInclusaoEdicao;


        // informações do modelo de formulario
        private int modeloCodigoTipoFormulario;
        private string modeloNomeFormulario;
        private string modeloDescricaoFormulario;
        private string[] modeloAbas;
        private char modeloIndicaToDoListAssociado;
        private string URLOrigemServidor;
        private string URLOrigemCliente;

        // informações do formulario selecionado
        string descricaoFormularioSelecionado;
        bool formularioPossuiCampoCalculado;

        //componentes compartilhados entre métodos
        private ASPxGridViewExporter gridExporterExcel = new ASPxGridViewExporter();
        private ASPxCallbackPanel pnFormulario;
        private ASPxHiddenField hfGeralFormulario = new ASPxHiddenField();
        private TabPage tabPageToDoList;
        private TabPage tabPageAnexos;
        private ASPxPageControl pcFormulario;
        private ASPxGridView gvToDoList;
        private ASPxGridView gvDocAnexo;
        private ASPxPanel pnAnexo;
        private double widthLinhaAtual = 0;
        bool mudaLinha = false;
        private ASPxHiddenField hfSessao = new ASPxHiddenField();
        private int nCallbackPageSize = 200;
        private string textoCheckOut = "";
        public readonly FormulariosPreDefinidos TipoFormularioPreDefinido;
        private static DataTable dtConteudoMaster_global = null;
        #endregion


        /// <summary>
        /// Método responsável por preparar o objeto que retornará um formulário.
        /// </summary>
        /// <param name="dados"></param>
        /// <param name="codigoModeloFormulario"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="somenteLeitura"></param>
        /// <param name="page"></param>
        /// <param name="parametrosEntrada">Possíveis valores: [formularioEdicao] :string - Url da tela onde será mostrado o formulário de edição (apenas para formulários do tipo lista. [isPopUp] :bool - indica se o formulário esta sendo aberto por uma tela popup. [mostrarTitulos] :bool - Indica se os títulos do formulário devem ser mostrados</param>
        public Formulario(ClasseDados dados, int codigoUsuarioResponsavel, int codigoEntidade, int codigoModeloFormulario, Unit width, Unit height, bool somenteLeitura, Page page, Hashtable parametrosEntrada, ref ASPxHiddenField hfSessaoParam, bool indicaFormularioAssinado)
        {
            this.dados = dados;
            this.codigoUsuarioResponsavel = codigoUsuarioResponsavel;
            this.codigoEntidade = codigoEntidade;
            this.codigoModeloFormulario = codigoModeloFormulario;
            this.width = width;
            this.height = height;
            this.somenteLeitura = somenteLeitura;
            this.page = page;
            this.iniciaisModeloFormulario = "";
            this.hfSessao = hfSessaoParam;
            this.parametrosEntrada = parametrosEntrada;
            indicaFormAssinado = indicaFormularioAssinado;

            // busca as informações do modelo de formulario selecionado - codigoModeloFormulario
            DataSet ds = dados.getDataSet("Select * from modeloFormulario where codigoModeloFormulario = " + codigoModeloFormulario);

            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                modeloCodigoTipoFormulario = int.Parse(dt.Rows[0]["CodigoTipoFormulario"].ToString());
                modeloNomeFormulario = dt.Rows[0]["NomeFormulario"].ToString();
                modeloDescricaoFormulario = dt.Rows[0]["DescricaoFormulario"].ToString();
                modeloAbas = dt.Rows[0]["Abas"].ToString().Split('\r');
                modeloIndicaToDoListAssociado = dt.Rows[0]["IndicaToDoListAssociado"].ToString()[0];
                URLOrigemServidor = dt.Rows[0]["URLOrigemServidor"].ToString();
                URLOrigemCliente = dt.Rows[0]["URLOrigemCliente"].ToString();
                iniciaisModeloFormulario = dt.Rows[0]["IniciaisFormularioControladoSistema"].ToString().Trim();

                if (parametrosEntrada != null)
                {
                    // se estiver relacionado a workflow, não aceita o tipo 2 (lista). => passar para 1
                    if ((parametrosEntrada.Contains("CodigoWorkflow")) && (modeloCodigoTipoFormulario == 2))
                        modeloCodigoTipoFormulario = 1;

                    parametrosEntrada.Add("modeloCodigoTipoFormulario", modeloCodigoTipoFormulario);
                }
            }

            // define o campo TipoFormularioPreDefinido;
            if (iniciaisModeloFormulario.Equals("PROP"))
                TipoFormularioPreDefinido = FormulariosPreDefinidos.NovaProposta;
            else if (iniciaisModeloFormulario.Equals("LICOESAPR"))
                TipoFormularioPreDefinido = FormulariosPreDefinidos.LicoesAprendidas;
            else
                TipoFormularioPreDefinido = FormulariosPreDefinidos.Nenhum;

            // cria o objeto hidden field
            hfGeralFormulario.ID = "hfGeralFormulario";
            hfGeralFormulario.ClientInstanceName = hfGeralFormulario.ID;
        }




        public Control constroiInterfaceFormulario(bool isPopUp, bool isPostBack, int? codigoFormulario, int? codigoProjeto, string cssFilePath, string cssPostFix)
        {
            this.isPopUp = isPopUp;
            this.isPostBack = isPostBack;
            this.cssFilePath = cssFilePath;
            this.cssPostFix = cssPostFix;

            if (!isPostBack)
            {
                // se o usuário da classe não passou o codigo do formulario... é operação de Inclusão
                if (!codigoFormulario.HasValue || codigoFormulario.Value <= 0)
                {
                    hfSessao.Set("_TipoOperacao_", "I");
                }
                // o usuário da classe passou o código do Formulário
                else
                {
                    hfSessao.Set("_TipoOperacao_", somenteLeitura ? "C" : "E");
                    hfSessao.Set("_CodigoFormularioMaster_", codigoFormulario.Value);
                    codigoFormularioMaster = codigoFormulario.Value;
                }
            }
            else // é postback
            {
                if (hfSessao.Contains("_CodigoFormularioMaster_"))
                {
                    codigoFormularioMaster = int.Parse(hfSessao.Get("_CodigoFormularioMaster_").ToString());
                }
            }

            // se passou o parametro codigoProjeto é pq o formulário deverá ser vinculado a ele - tabela "FormularioProjeto"
            if (codigoProjeto.HasValue)
            {
                this.codigoProjeto = codigoProjeto.Value;
                hfGeralFormulario.Set("CodigoProjetoParam", codigoProjeto);
            }

            if (parametrosEntrada != null)
            {
                if (parametrosEntrada.Contains("isPopUp"))
                    isPopUp = Boolean.Parse(parametrosEntrada["isPopUp"].ToString());
                if (parametrosEntrada.Contains("mostrarTitulos"))
                    mostrarTitulos = Boolean.Parse(parametrosEntrada["mostrarTitulos"].ToString());
            }

            // Se já tem o código do formulário, verifica se este está associado a algum projeto
            int? tempCodigoProjetoAssociado = getProjetoAssociado();
            if (tempCodigoProjetoAssociado.HasValue)
            {
                this.codigoProjeto = tempCodigoProjetoAssociado.Value;
                hfGeralFormulario.Set("CodigoProjetoParam", codigoProjeto);
            }

            // Se o formulário é do tipo "Formulário" ou é uma janela popUp
            // -----------------------------------------------------------------------------------------------------------------------
            if (modeloCodigoTipoFormulario == 1 || isPopUp)
            {
                // se não tem o codigo do formulário, verifica se já existe formulário preenchido para o projeto selecionado
                if (codigoFormularioMaster <= 0)
                {
                    // a verificação só pode ser feita se a chamada NÃO está relacionada com workflow
                    if (parametrosEntrada == null || !parametrosEntrada.Contains("CodigoWorkflow"))
                    {
                        int codigoFormularioTemp = getCodigoFormularioModeloUnico();
                        if (codigoFormularioTemp > 0)
                        {
                            hfSessao.Set("_TipoOperacao_", somenteLeitura ? "C" : "E");
                            hfSessao.Set("_CodigoFormularioMaster_", codigoFormularioTemp);
                            codigoFormularioMaster = codigoFormularioTemp;
                        }
                    }
                }

                verificaBloqueioFormulario(codigoFormularioMaster);

                Control controlesFormulario = inicializaFormulario();
                (controlesFormulario as ASPxPanel).Width = new Unit("100%");

                if (pcFormulario == null)
                    return controlesFormulario;

                if (codigoFormularioMaster > 0)
                {
                    hfSessao.Set("_CodigoFormularioMaster_", codigoFormularioMaster);
                    hfGeralFormulario.Set("_CodigoFormularioMaster_", codigoFormularioMaster);
                    hfGeralFormulario.Set("CodigoProjetoParam", codigoProjeto);
                    populaFormularioMaster(pcFormulario);
                }

                if (existeSubFormularios)
                {
                    // insere o script responsável por tratar o endCallback das grids de subformularios.
                    page.ClientScript.RegisterClientScriptBlock(GetType(), "trataSubFormularios",
                        @"<script type='text/javascript' language='javascript'>
                        function trataEndCallbackSubFormulario(s,e) 
                        {
                            if (s.cpAcaoSubFormulario == null)
                                return;

                            var aAcaoSubFormulario = s.cpAcaoSubFormulario.split('_');
                            var codigoModeloFormulario = aAcaoSubFormulario[2].substring(3);
                            var aba = aAcaoSubFormulario[3].substring(3);
                            if (aAcaoSubFormulario[0]=='T')
                            {
                                hfGeralFormulario.Set('FEDIT' + codigoModeloFormulario, aba);
                            }
                            else
                            {
                                if (hfGeralFormulario.Contains('FEDIT' + codigoModeloFormulario))
                                {
                                    hfGeralFormulario.Remove('FEDIT' + codigoModeloFormulario);
                                }
                                if (s.cpbotaoAcaoSubFormulario == 'Salvar')
                                {
                                    hfGeralFormulario.Set('_CodigoFormularioMaster_', s.cpCodigoFormularioMaster);
                                }
                            }
                        } </script>");
                }
                // Insere script que será executado sempre que qualquer campo do formulário for alterado
                page.ClientScript.RegisterClientScriptBlock(GetType(), "conteudoCampoAlterado",
                        @"<script type='text/javascript' language='javascript'>
                        function conteudoCampoAlterado() 
                        {
                            if (window.existeConteudoCampoAlterado == undefined) 
                               window.top.mostraMensagem('" + Resources.AVariávelExisteConteudoCampoAlteradoNãoFoi + @"', 'erro', true, false, null);
                            else 
                               existeConteudoCampoAlterado = true;
                        } </script>");

                // Insere script para validar se todos os campos de preenchimento obrigatório estão preenchidos.
                page.ClientScript.RegisterClientScriptBlock(GetType(), "validaCamposObrigatorios",
                        @"<script type='text/javascript' language='javascript'>
                        function validaCamposObrigatorios() 
                        {
                            var tabPageCount = pcFormulario.GetTabCount();
                            var tabAtual = pcFormulario.GetActiveTab();

                            for(var i = 0; i < tabPageCount; i++) 
                            {
                                var tabPageCount = pcFormulario.GetTabCount();
                                for(var i = 0; i < tabPageCount; i++) 
                                {
                                    // As tabs de anexos e todolist não devem ser validadas
                                    var nomeTab = pcFormulario.GetTab(i).name;
                                    if (nomeTab == 'tabPageToDoList' || nomeTab == 'tabPageAnexos')
                                        continue;

                                    pcFormulario.SetActiveTab(pcFormulario.GetTab(i));
                                    if (!ASPxClientEdit.ValidateGroup('FormularioCDIS') || !ValidaSubformulariosAba(i)) 
                                        return false;
                                    else
                                        pcFormulario.SetActiveTab(tabAtual);
                                }

                                pcFormulario.SetActiveTab(tabAtual);
                                if(tabAtual.name == 'tabPageAnexos')
                                    document.getElementById('frmAnexo').src = s.cp_urlAnexos;
                                
                                return true;
                            }

                            function ValidaSubformulariosAba(indexAba){
                                var key = 'sub-obrigatorio_' + indexAba;
                                if(hfGeralFormulario.Contains(key)){
                                    var nomesSubFormularios = hfGeralFormulario.Get(key).split(';');
                                    for(var j = 0; j < nomesSubFormularios.length; j++){
                                        var nomeSubFormulario = nomesSubFormularios[j];
                                        var gridViewSubFormulario = eval(nomeSubFormulario);
                                        var idIconRequiredFieldSub = 'ico-required-field_sub' + nomeSubFormulario.substring(nomeSubFormulario.lastIndexOf('_') + 1);
                                        if(gridViewSubFormulario.cpVisibleRowCount == 0){
                                            document.getElementById(idIconRequiredFieldSub).style.display = 'inline-block';
                                            return false;
                                        }
                                        document.getElementById(idIconRequiredFieldSub).style.display = 'none';
                                    }
                                }
                                return true;
                            }

                            function validaCamposComExpressao(s, e) 
                            {
                                var collection = ASPxClientControl.GetControlCollection();
                                for (var key in collection.elements) 
                                {
                                    var control = collection.elements[key];
                                    if (ASPxClientUtils.IsExists(control) && control.cpExpressaoValidacao && control.cpExpressaoValidacao != '')
                                        if (!validaExpressaoCampo(control))
                                            return false;
                                }
 
                                return true;
                            }  

                            function validaExpressaoCampo(controleComExpressao) 
                            {
                                var expressaoValidacao = controleComExpressao.cpExpressaoValidacao;
                                var expressaoTemp = expressaoValidacao;
                                while (expressaoTemp.indexOf('[') >= 0) 
                                {
                                    inicio = expressaoTemp.indexOf('[')+1;
                                    termino = expressaoTemp.indexOf(']');
                                    elemento = expressaoTemp.substr(inicio, termino - inicio);
                                    var objElemento = eval('id_' + elemento);
                                    
                                    var valor = objElemento.GetValue();
                                    if (valor == null)
                                    {
                                        pcFormulario.SetActiveTabIndex(objElemento.cpIndexAba);
                                        objElemento.Focus();
                                        alert('" + Resources.OCampo + @" [' + objElemento.cpNomeCampo + '] " + Resources.precisaSerInformado + @".');
                                        return false;
                                    }
                                    valor = valor.toString();
                                    if (valor.indexOf(' ')>0)
                                        valor = valor.substr(valor.indexOf(' ')+1);

                                    valor = valor.replace('.', '');
                                    valor = valor.replace(',', '.');

                                    expressaoValidacao = replaceAll(expressaoValidacao, '[' + elemento + ']', valor);
                                    expressaoTemp = replaceAll(expressaoTemp, '[' + elemento + ']', '');
                                }
                                var resultado = eval(expressaoValidacao);
                                if (resultado)
                                    return true;
 
                                pcFormulario.SetActiveTabIndex(controleComExpressao.cpIndexAba);
                                controleComExpressao.Focus();
                                alert(controleComExpressao.cpMensagemValidacao);
                                return false;
                            }

                            pcFormulario.SetActiveTab(tabAtual);

                            return true;
                        } </script>");

                // Insere script para verificar 
                page.ClientScript.RegisterClientScriptBlock(GetType(), "verificaAvancoWorkflow",
                        @"<script type='text/javascript' language='javascript'>
                        function verificaAvancoWorkflow() 
                        {
                            if(existeConteudoCampoAlterado)
                            {
                                window.top.mostraMensagem('" + Resources.AsAlteraçõesNãoForamGravadasÉNecessárioGra + @"', 'erro', true, false, null);
                                return false;
                            }
                            return true;
                        } </script>");

                // Se existe campo calculado, insere script para correspondente
                if (formularioPossuiCampoCalculado)
                {
                    bool ehPortugues = (System.Globalization.CultureInfo.CurrentCulture.Name == "pt-BR");

                    page.ClientScript.RegisterClientScriptBlock(GetType(), "calculaFormula",
                    @"<script type='text/javascript' language='javascript'>
                         var ehPortugues = " + ehPortugues.ToString().ToLower() + @"; 
                        //Bug 3721: [SESCOOP][GERAL][Formulários]Sistema não apresenta corretamente o valor do resultado quando está configurado para ter 0 casas decimais
                        //https://stackoverflow.com/questions/10808671/javascript-how-to-prevent-tofixed-from-rounding-off-decimal-numbers/44184500#44184500
                        function truncateToDecimals(num, dec = 2) 
                        {
                           const calcDec = Math.pow(10, dec);
                           return Math.trunc(num * calcDec) / calcDec;
                        }
                        function avaliarFormula(a,formula, precisaoDecimal, ehCampoSubFormulario = false) 
                        {
                            var objCampo = eval(a);
                            var resultado = calcula(formula, ehCampoSubFormulario);
                            if(resultado == 'indefinido')
                            {
                                 return;
                            }
                             if(precisaoDecimal == 0)
                            {
                                 resultado = truncateToDecimals(resultado, 0); 
                            }
                            else
                            {
                                 resultado = parseFloat(resultado.toFixed(precisaoDecimal));
                            }
                            if(ehPortugues && !ehCampoSubFormulario)
                            {                                 
                                 resultado = replaceAll(resultado.toString(), '.', ',');
                            }
                            objCampo.SetValue(resultado);
                        } 

                        function calcula(formula, ehCampoSubFormulario = false) 
                        {
                            var formulaComValores = formula;
                            var existeCampoNulo = false;
                            while (formula.indexOf('[') >= 0) 
                            {
                                inicio = formula.indexOf('[')+1;
                                termino = formula.indexOf(']');
                                elemento = formula.substr(inicio, termino - inicio);
                                var objElemento = eval('id_' + elemento);
                                var valor = objElemento.GetValue();
                                if (valor == null)
                                {
                                    existeCampoNulo = true;
                                    break;
                                }
                                valor = valor.toString();
                                if (valor.indexOf(' ')>0)
                                    valor = valor.substr(valor.indexOf(' ')+1);
                                formulaComValores = replaceAll(formulaComValores, '[' + elemento + ']', valor);
                                formula = formula.substr(termino + 1);
                            }



                            if(ehPortugues && !ehCampoSubFormulario)
                            {
                               formulaComValores = replaceAll(formulaComValores, ',', '.');
                            }
                            if (existeCampoNulo)
                                result = 'indefinido';
                            else
                                result = calculate(formulaComValores);
                           
                            return result;//calculate(formulaComValores);
                        }                       

                        function calculate(equation)
                        {
                            var answer = 'erro';
                                try
                                {
                                    answer = equation != '' ? eval(equation) : '0';
                                }
                                catch (e)
                                {
                                }
                            return answer;
                        }

                       function replaceAll(origem, antigo, novo) 
                       {
                            return origem.split(antigo).join(novo);
                       }

                       function trim(str) 
                       {
                            return str.replace(/^\s+|\s+$/g,'');
                       }

                        </script>");
                }

                return controlesFormulario;
            }
            // Se o formulário é do tipo "Lista"
            // -----------------------------------------------------------------------------------------------------------------------
            if (modeloCodigoTipoFormulario == 2)
            {
                if (parametrosEntrada != null && parametrosEntrada.Contains("formularioEdicao"))
                {
                    string formularioEdicao = parametrosEntrada["formularioEdicao"].ToString();
                    // insere o script responsável por abrir o pop-up de edição da grid.
                    string parametros = string.Format(formularioEdicao + "?CE={0}&CMF={1}&CP={2}&US={3}", codigoEntidade, codigoModeloFormulario, codigoProjeto, codigoUsuarioResponsavel);

                    //if (parametros.Contains("AT=") == false)
                    //    parametros += "&AT=' + alturaTela + '";

                    string formularioImpressao = parametrosEntrada["formularioImpressao"].ToString();
                    string parametrosImpressao = string.Format(formularioImpressao + "?CE={0}&CMF={1}&CP={2}&US={3}", codigoEntidade, codigoModeloFormulario, codigoProjeto, codigoUsuarioResponsavel);

                    string scripts =
                        @"<script type='text/javascript' language='javascript'>
                        var alturaTela = screen.height - 240;
                        function popUpFormularioLeitura(values) 
                        {                             
                            window.top.showModal('" + parametros + @"&RO=S&CF='+values + '&AT=' + (screen.height - 240) + '&Largura=' + (screen.width - 100), '" + Resources.Formulário + @"', null, null, null, null);
                        }

                        function popUpFormularioImpressao(values) 
                        {                             
                            window.showModal('" + parametrosImpressao + @"&CF=' + values, '" + Resources.Impressão + @"', 1000, (screen.height - 260), '', null);
                        }

                        function atualizaTela(lParam)
                        {
                            lpLoading.Show();
                            window.location.reload();
                        
                        }
                        ";

                    if (!somenteLeitura)
                        scripts +=
                              @"function popUpFormularioEdicao(values) 
                                { 
                                      window.top.showModalFormulario('" + parametros + @"&CF='+values + '&AT=' + (screen.height - 350) + '&Largura=' + (screen.width - 100), '" + Resources.Formulário + @"', (screen.width - 30), (screen.height - 285), atualizaTela, null);
                                }
                                ";

                    scripts += "</script>";
                    page.ClientScript.RegisterClientScriptBlock(GetType(), "popUpEditaGrid", scripts);

                    verificaBloqueioFormulario(codigoFormularioMaster);

                    return renderizaGridPrincipal(isPostBack);
                    //expGridPrincipal = new ASPxGridViewExporter();
                    //expGridPrincipal.GridViewID = "gvGridPrincipal";

                    /*Panel pnExporta = new Panel();
                    ASPxButton btnExportaGridPrincipalXLS = new ASPxButton();
                    btnExportaGridPrincipalXLS.ID = "btnExportaGridPrincipalXLS";
                    btnExportaGridPrincipalXLS.Text = "Exportar para XLS";
                    btnExportaGridPrincipalXLS.Click += new EventHandler(btnExportar_Click);
                    pnExporta.Controls.Add(btnExportaGridPrincipalXLS);
                    pnExporta.Controls.Add(gvGridPrincipal);
                    return pnExporta;*/
                }
            }
            // Se o formulário é do tipo "Tela"
            // -----------------------------------------------------------------------------------------------------------------------
            else if (modeloCodigoTipoFormulario == 3)
            {
                // se na urlCliente tem referencia a projeto, subtituir o X pelo codigo do projeto
                if (URLOrigemCliente.IndexOf("CP=X") > 0)
                {
                    URLOrigemCliente = URLOrigemCliente.Replace("CP=X", "CP=" + (codigoProjeto == null ? "0" : codigoProjeto.ToString()));
                }

                verificaBloqueioFormulario(codigoFormularioMaster);

                if (somenteLeitura)
                    URLOrigemCliente += "&RO=S";

                if (parametrosEntrada != null)
                {
                    if (parametrosEntrada.Contains("CodigoWorkflow"))
                        URLOrigemCliente += "&CWF=" + parametrosEntrada["CodigoWorkflow"] + "";
                    if (parametrosEntrada.Contains("CodigoInstanciaWorkflow"))
                        URLOrigemCliente += "&CIWF=" + parametrosEntrada["CodigoInstanciaWorkflow"] + "";
                    if (parametrosEntrada.Contains("CodigoOcorrenciaAtual"))
                        URLOrigemCliente += "&CSOWF=" + parametrosEntrada["CodigoOcorrenciaAtual"] + "";
                    if (parametrosEntrada.Contains("CodigoEtapaAtual"))
                        URLOrigemCliente += "&CEWF=" + parametrosEntrada["CodigoEtapaAtual"] + "";
                }

                URLOrigemCliente += "&ALT=" + (height.Value + 10);
                URLOrigemCliente += "&Altura=" + (height.Value + 10);

                string largura = (width.Value + 20) + "px";
                Control iFrame = getLiteral(string.Format(
                    @"<iframe name='wfTela' src='{0}' width='100%' scrolling='auto' height='{2}' frameborder='0'></iframe>", URLOrigemCliente, largura, (height.Value + 95).ToString()));
                return iFrame;
            }


            return null;
        }

        private void verificaBloqueioFormulario(int? codigoFormulario)
        {
            DataSet ds = dados.getDataSet("SELECT Valor FROM ParametroConfiguracaoSistema WHERE Parametro = 'ControlaBloqueioEdicaoFormulario' AND CodigoEntidade = " + codigoEntidade);

            if (this.somenteLeitura == false && codigoFormulario.HasValue && codigoFormulario > 0 && ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Valor"].ToString() == "S")
            {
                DataSet dsFormBloqueio = dados.getDataSet(@"
                SELECT DataCheckOut, NomeUsuario 
                  FROM Formulario f INNER JOIN
                       Usuario u ON u.CodigoUsuario = f.CodigoUsuarioCheckOut
                 WHERE ISNULL(CodigoUsuarioCheckOut, " + codigoUsuarioResponsavel + ") <> " + codigoUsuarioResponsavel + " AND CodigoFormulario = " + codigoFormulario);

                if (dsFormBloqueio.Tables[0] != null && dsFormBloqueio.Tables[0].Rows.Count > 0)
                {
                    this.somenteLeitura = true;
                    textoCheckOut = string.Format("{2} {0} {3} {1:dd/MM/yyyy HH:mm}.", dsFormBloqueio.Tables[0].Rows[0]["NomeUsuario"]
                        , dsFormBloqueio.Tables[0].Rows[0]["DataCheckOut"], Resources.FormulárioBloqueadoParaOUsuário, Properties.Resources.desde);
                }
                else
                {
                    int regAf = 0;
                    dados.execSQL("UPDATE Formulario SET CodigoUsuarioCheckOut = " + codigoUsuarioResponsavel + ", DataCheckOut = GetDate() WHERE CodigoFormulario = " + codigoFormulario, ref regAf);
                }
            }
        }

        private void populaCampoFormularioMaster(DataRow dr, Control controle)
        {
            //string nomeCampo = dr["codigoTipoCampo"].ToString() + dr["CodigoCampo"].ToString();
            int codigoFormulario = int.Parse(dr["codigoFormulario"].ToString());
            string colunaCampo = "Valor" + dr["codigoTipoCampo"].ToString();
            string definicaoCampo = dr["DefinicaoCampo"].ToString();
            string[] aDefinicaoCampo = definicaoCampo.Split(DelimitadorPropriedadeCampo);
            string tipoCampo = dr["codigoTipoCampo"].ToString();
            bool indicaAssinado = indicaFormAssinado;

            // Os campos REF precisam ser do tipo do campo para o qual eles apontam
            if (tipoCampo == "REF")
            {
                int codigoCampo = int.Parse(dr["CodigoCampo"].ToString());
                DataSet dsRef = getInformacoesCampo(codigoCampo);
                DataRow drCampo = dsRef.Tables[0].Rows[0];
                string definicaoCampoRef = drCampo["DefinicaoCampo"].ToString();
                string[] aDefinicaoCampoRef = definicaoCampoRef.Split(DelimitadorPropriedadeCampo);
                string codigoCampoRef = aDefinicaoCampoRef[1].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();

                // busca o tipo do campo apontado
                DataSet ds = getInformacoesCampo(int.Parse(codigoCampoRef));
                drCampo = ds.Tables[0].Rows[0];
                tipoCampo = drCampo["codigoTipoCampo"].ToString();
                definicaoCampo = drCampo["DefinicaoCampo"].ToString();
                aDefinicaoCampo = definicaoCampo.Split(DelimitadorPropriedadeCampo);
                colunaCampo = "Valor" + drCampo["codigoTipoCampo"].ToString();

                // se a coluna correspondente estiver nula, temos que ler o valor no formulário de referência.
                if (dr[colunaCampo] == DBNull.Value)
                    dr[colunaCampo] = getValorCampoREF_formularioOrigem(controle, codigoCampo, int.Parse(codigoCampoRef), 0) + "";
            }

            if (tipoCampo == "VAR")
            {
                ((ASPxTextBox)controle).Text = dr[colunaCampo].ToString();
            }
            else if (tipoCampo == "NUM")
            {
                if (dr[colunaCampo].ToString() != "")
                {
                    decimal numero = decimal.Parse(dr[colunaCampo].ToString());
                    Int64 parteInteira = (Int64)Math.Truncate(numero);
                    // se não tem separador de decimal no número
                    if (parteInteira - numero == 0)
                        ((ASPxSpinEdit)controle).Value = parteInteira.ToString();
                    else
                        ((ASPxSpinEdit)controle).Value = dr[colunaCampo].ToString();
                }
            }
            else if (tipoCampo == "TXT")
            {
                string formato = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                if (formato == "0")
                    ((ASPxMemo)controle).Text = dr[colunaCampo].ToString();
                else
                    ((ASPxHtmlEditor)controle).Html = dr[colunaCampo].ToString();
            }
            else if (tipoCampo == "DAT")
            {
                ((ASPxDateEdit)controle).Value = dr[colunaCampo];
            }
            else if (tipoCampo == "LST" || tipoCampo == "LOO")
            {
                colunaCampo = "ValorVAR";
                string formato = "0"; // os campos LOO SEMPRE terão formato igual a zero
                if (tipoCampo == "LST")
                    formato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();

                if (formato == "0")
                {
                    if (indicaAssinado)
                        ((ASPxComboBox)controle).Text = dr["ValorCampoSomenteLeitura"].ToString();
                    else
                    {
                        ((ASPxComboBox)controle).Value = dr[colunaCampo].ToString();

                        // se é campo LOO e tem valor a ser preenchido
                        if (tipoCampo == "LOO" && dr[colunaCampo].ToString() != "")
                        {
                            bool mostrarComoLov = false;
                            if (aDefinicaoCampo.Length > 2 && aDefinicaoCampo[2] != "")
                                mostrarComoLov = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim().ToUpper() == "S";

                            // se o campo está configurado para "LOV"
                            if (mostrarComoLov)
                                populaConteudoCampoLOO_Lov((ASPxComboBox)controle, aDefinicaoCampo, dr[colunaCampo].ToString());
                        }
                    }
                }
                else if (formato == "1")
                {
                    if (indicaAssinado)
                        ((ASPxTextBox)controle).Text = dr["ValorCampoSomenteLeitura"].ToString();
                    else
                        ((ASPxRadioButtonList)controle).Value = dr[colunaCampo].ToString().Trim();
                }
                else if (formato == "2")
                {
                    if (indicaAssinado)
                        ((ASPxTextBox)controle).Text = dr["ValorCampoSomenteLeitura"].ToString();
                    else
                    {
                        for (int index = 0; index < ((CheckBoxList)controle).Items.Count; index++)
                        {
                            string temp = DelimitadorPropriedadeCampo + index.ToString() + DelimitadorPropriedadeCampo;
                            if (dr[colunaCampo].ToString().IndexOf(temp) >= 0)
                                ((CheckBoxList)controle).Items[index].Selected = true;
                            else
                                ((CheckBoxList)controle).Items[index].Selected = false;
                        }
                    }
                }
            }
            else if (tipoCampo == "CPD")
            {
                char IndicaCampoDinamico = 'N';
                string temp = getConteudoCampoPreDefinido(int.Parse(dr["codigoCampo"].ToString()), codigoFormulario, ref IndicaCampoDinamico, indicaAssinado, false);

                if (indicaAssinado)
                    temp = dr["ValorCampoSomenteLeitura"].ToString();

                string linhas = "1";
                if (aDefinicaoCampo.Length > 0 && aDefinicaoCampo[1] != "")
                    linhas = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();

                if (IndicaCampoDinamico == 'S' || indicaAssinado)
                {
                    if (linhas != "1")
                        ((ASPxMemo)controle).Text = temp;
                    else
                        ((ASPxTextBox)controle).Text = temp;
                }
            }
            else if (tipoCampo == "CAL")
            {
                colunaCampo = "ValorNum";
                string valor = dr[colunaCampo].ToString().Replace('.', ',');

                if (indicaAssinado)
                    valor = dr["ValorCampoSomenteLeitura"].ToString().Replace('.', ',');

                ((ASPxTextBox)controle).Text = valor;
            }
        }

        // ACG - 15/02 - Os campos LOO do tipo LOV só serão preenchidos apenas com o registro correspondente a escolha do usuário.
        private bool populaConteudoCampoLOO_Lov(ASPxComboBox controle, string[] aDefinicaoCampo, string valor)
        {
            controle.Items.Clear();
            int codigoLookup = int.Parse(aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim());
            string comandoSQL = getComandoTabelaLookup(codigoLookup, "").ToUpper();

            // retira o order by
            if (comandoSQL.IndexOf("ORDER BY") > 0)
                comandoSQL = comandoSQL.Substring(0, comandoSQL.IndexOf("ORDER BY"));

            // encapsula comando genérico para selecionar apenas uma linha
            comandoSQL = "SELECT Codigo, descricao FROM (" + comandoSQL + ") as CMD WHERE Codigo = '" + valor + "' ";

            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null)
            {
                controle.Items.Add(ds.Tables[0].Rows[0]["descricao"].ToString(), ds.Tables[0].Rows[0]["codigo"]);
                return true;
            }
            return false;
        }

        private void populaFormularioMaster(Control Container)
        {
            DataSet ds = getConteudoFormulario(0, 0);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    // Lê a descrição do formulário
                    if (dt.Rows[0]["DescricaoFormulario"] != null && dt.Rows[0]["DescricaoFormulario"].ToString() != "")
                    {
                        descricaoFormularioSelecionado = dt.Rows[0]["DescricaoFormulario"].ToString();
                        // Atribui a descrição do formulário ao componente de tela
                        ASPxTextBox controleDescricaoFormulario = Container.FindControl("DescricaoFormulario") as ASPxTextBox;
                        if (controleDescricaoFormulario != null)
                            controleDescricaoFormulario.Text = descricaoFormularioSelecionado;
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        string nomeCampo = dr["codigoTipoCampo"].ToString() + dr["CodigoCampo"].ToString();
                        Control controle = Container.FindControl(nomeCampo);
                        if (controle != null)
                            populaCampoFormularioMaster(dr, controle);
                    }
                }
            }
        }

        #region Modelo formulário do tipo "Formulário" - Tipo 1

        // prepara o controle "pnExterno" para avisar ao usuário que o formulário não é válido.
        private void avisoFormularioInvalido(ASPxPanel pnExterno, string mensagem)
        {
            pnExterno.Controls.Add(getLiteral("<p>"));
            pnExterno.Controls.Add(getLiteral("<p>"));
            pnExterno.Controls.Add(getLiteral("<p>"));
            pnExterno.Controls.Add(getLiteral("<p>"));
            pnExterno.Controls.Add(getLiteral("<p>"));
            pnExterno.Controls.Add(getLiteral("<hr>"));

            ASPxLabel lblAvisoTitulo = new ASPxLabel();
            lblAvisoTitulo.ID = "lblAvisoTitulo";
            lblAvisoTitulo.CssFilePath = cssFilePath;
            lblAvisoTitulo.CssPostfix = cssPostFix;
            //lblAvisoTitulo.Font.Bold = true;
            lblAvisoTitulo.Text = Resources.AtençãoNãoFoiPossívelApresentarOFormulário;
            pnExterno.Controls.Add(lblAvisoTitulo);
            pnExterno.Controls.Add(getLiteral("<br>"));

            ASPxLabel lblAviso = new ASPxLabel();
            lblAviso.ID = "lblAviso";
            lblAviso.CssFilePath = cssFilePath;
            lblAviso.CssPostfix = cssPostFix;
            lblAviso.Text = mensagem;
            pnExterno.Controls.Add(lblAviso);
            pnExterno.Controls.Add(getLiteral("<hr>"));
        }

        private void incluiBotoesSalvarCancelar(ASPxPanel pnExterno, int idBotao, bool linhaBrancaPosBotoes)
        {
            // Verifica se a tela terá botões
            if (!somenteLeitura || isPopUp)
            {
                pnExterno.Controls.Add(
                    getLiteral(string.Format(@"<tr>
                                    <td colspan=""2"" align=""right"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                                           <tr>
                                              <td style=""width: 80px"">")));

                // se tem o botão "Salvar"...
                //---------------------------------------------------------------------------
                if (!somenteLeitura)
                {
                    ASPxButton btnSalvar = getBotaoSalvar("btnSalvar", Resources.Salvar);
                    btnSalvar.ID = "btnSalvar_" + idBotao;
                    btnSalvar.ClientInstanceName = btnSalvar.ID;
                    pnExterno.Controls.Add(btnSalvar);
                }
                else
                    pnExterno.Controls.Add(getLiteral("&nbsp;"));

                pnExterno.Controls.Add(getLiteral(
                        @"</td>
                          <td style=""width: 10px""></td>
                          <td>"));

                // se tem o botão "Cancelar"...
                //---------------------------------------------------------------------------
                if (isPopUp) // o botão cancelar só deve ser mostrado em popup´s
                {
                    ASPxButton btnCancelar = getBotaoCancelar("btnCancelar", Resources.Fechar);
                    btnCancelar.ID = "btnCancelar_" + idBotao;
                    btnCancelar.ClientInstanceName = btnCancelar.ID;
                    pnExterno.Controls.Add(btnCancelar);
                }
                else
                    pnExterno.Controls.Add(getLiteral("&nbsp;"));

                pnExterno.Controls.Add(getLiteral(
                       @"</td>
                          <td style=""width: 10px""></td>
                          <td>"));

                // se tem o botão "Imprimir"...
                //---------------------------------------------------------------------------
                if (parametrosEntrada != null && parametrosEntrada.Contains("formularioImpressao"))
                {
                    ASPxButton btnImpressao = getBotaoImprimir("btnImpressao", Resources.Imprimir);
                    btnImpressao.ID = "btnImpressao_" + idBotao;
                    btnImpressao.ClientInstanceName = btnImpressao.ID;
                    //btnImpressao.ClientEnabled = codigoFormularioMaster > 0;
                    pnExterno.Controls.Add(btnImpressao);
                }
                else
                    pnExterno.Controls.Add(getLiteral("&nbsp;"));

                pnExterno.Controls.Add(
                    getLiteral(
                        @"</td>
                          <td style=""width: 3px""></td>
                        </tr>
                      </table>
                    </td>
                  </tr>"));

                /* apenas para testes
                   ASPxButton btnCancelarx = getBotaoTemp("btnTemp", "Temp");
                   btnCancelarx.ID = "btnCancelarx_" + idBotao;
                   btnCancelarx.ClientInstanceName = btnCancelarx.ID;
                   pnExterno.Controls.Add(btnCancelarx);

                   ASPxButton btnCancelarx2 = getBotaoTemp2("btnTemp", "Temp 2");
                   btnCancelarx2.ID = "btnCancelarx2_" + idBotao;
                   btnCancelarx2.ClientInstanceName = btnCancelarx2.ID;
                   pnExterno.Controls.Add(btnCancelarx2);
                   */
                if (linhaBrancaPosBotoes)
                    pnExterno.Controls.Add(getLiteral(
                        @"<tr style=""height: 5px"">
                             <td style=""width: 2px"">
                             </td>
                             <td align=""right"">
                             </td>
                          </tr>"));
            }
            else
            {
                pnExterno.Controls.Add(
                   getLiteral(string.Format(@"<tr>
                                    <td style=""width: 2px""></td>
                                    <td align=""right"">
                                      <table style=""width:100%"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                        <tr><td>{0}</td><td align=""right"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                                           <tr>
                                              <td style=""width: 80px"">", textoCheckOut)));

                pnExterno.Controls.Add(getLiteral("&nbsp;"));
                pnExterno.Controls.Add(getLiteral("&nbsp;"));
                pnExterno.Controls.Add(getLiteral(
                        @"</td>
                          <td></td>
                          <td>"));

                // se tem o botão "Imprimir"...
                //---------------------------------------------------------------------------
                if (parametrosEntrada != null && parametrosEntrada.Contains("formularioImpressao"))
                {
                    ASPxButton btnImpressao = getBotaoImprimir("btnImpressao", Resources.Imprimir);
                    btnImpressao.ID = "btnImpressao_" + idBotao;
                    btnImpressao.ClientInstanceName = btnImpressao.ID;
                    //btnImpressao.ClientEnabled = codigoFormularioMaster > 0;
                    pnExterno.Controls.Add(btnImpressao);
                }
                else
                    pnExterno.Controls.Add(getLiteral("&nbsp;"));

                pnExterno.Controls.Add(
                    getLiteral(
                        @"</td>
                          <td style=""width: 3px""></td>
                        </tr>
                      </table>
                    </td>
                    </tr>
                    </table>
                    </td>
                  </tr>"));

                if (linhaBrancaPosBotoes)
                    pnExterno.Controls.Add(getLiteral(
                        @"<tr style=""height: 5px"">
                             <td style=""width: 2px"">
                             </td>
                             <td align=""right"">
                             </td>
                          </tr>"));


            }
        }

        private void incluiPanelCallback(ASPxPanel pnExterno)
        {
            pnExterno.Controls.Add(
                getLiteral(
                    @"<tr>
                            <td style=""width: 2px""></td>
                            <td valign=""top"">"));
            // o formulário será renderizzado dentro de um painel callback.
            pnFormulario = new ASPxCallbackPanel();
            pnFormulario.ID = "pnFormulario";
            pnFormulario.ClientInstanceName = "pnFormulario";
            pnFormulario.Callback += pnFormulario_Callback;
            //pnFormulario.Height = (int)height.Value - 10;
            pnFormulario.ScrollBars = ScrollBars.Vertical;
            pnFormulario.Width = new Unit("100%");
            pnFormulario.Attributes.Add("Style", "text-align: left");
            pnFormulario.ClientSideEvents.Init = @"function(s, e) {
window.top.lpAguardeMasterPage.Hide(); 
            var height = Math.max(0, document.documentElement.clientHeight - 75);
            s.SetHeight(height);
}";
            pnFormulario.SettingsLoadingPanel.Enabled = false;
            pnFormulario.ClientSideEvents.EndCallback =
                @"function(s, e) {
                    
                    if(s.cp_CF != null && s.cp_CF != '' && s.cp_CF != -1 && s.cp_CF != 0)
                    {
                        hfSessao.Set('_TipoOperacao_', s.cp_TO);
                        hfSessao.Set('_CodigoFormularioMaster_', s.cp_CF);
                        window.top.lpAguardeMasterPage.Hide();
                        pcVersoes.PerformCallback();
                    }
                    if (hfGeralFormulario.Get('StatusSalvar')=='1')
                    { 
                        window.top.retornoModal2 = 'S';
	                    window.returnValue = 'OK';
                        var codigoFormulario = hfGeralFormulario.Get('_CodigoFormularioMaster_'); 

                        // se tem hfGeralWorkflow, é poque é de engenharia de Workflow e tem também tcLinksFormuarios
                        if (window.parent.hfGeralWorkflow)
                        {
                            var aba = window.parent.tcLinksFormuarios.GetActiveTab().index;
                            window.parent.hfGeralWorkflow.Set('FormID" + codigoModeloFormulario + @"_' + aba, codigoFormulario);
                            if (hfGeralFormulario.Contains('CodigoInstanciaWf'))
                                window.parent.hfGeralWorkflow.Set('CodigoInstanciaWf', hfGeralFormulario.Get('CodigoInstanciaWf'));
                            if (hfGeralFormulario.Contains('CodigoEtapaWf'))
                                window.parent.hfGeralWorkflow.Set('CodigoEtapaWf', hfGeralFormulario.Get('CodigoEtapaWf'));
                            if (hfGeralFormulario.Contains('SequenciaOcorrenciaEtapaWf'))
                                window.parent.hfGeralWorkflow.Set('SequenciaOcorrenciaEtapaWf', hfGeralFormulario.Get('SequenciaOcorrenciaEtapaWf'));

                        }
                        existeConteudoCampoAlterado = false;
                        window.top.mostraMensagem('" + Resources.AsInformaçõesDoFormulárioForamSalvas + @"', 'sucesso', false, false, null);

                        if(window.fechaTelaPosSalvar)
                            fechaTelaPosSalvar(); 

                        if(window.executaFuncaoTelaPai)
                            executaFuncaoTelaPai(hfGeralFormulario.Get('CodigoProjetoParam'), hfGeralFormulario.Get('_CodigoFormularioMaster_'));
                        
                        if (hfGeralFormulario.Contains('CodigoInstanciaWf') && hfGeralFormulario.Contains('CodigoEtapaWf') && hfGeralFormulario.Contains('SequenciaOcorrenciaEtapaWf'))
                        {
                                var paramsRenderiza = '&CI=' + hfGeralFormulario.Get('CodigoInstanciaWf') + '&CE=' + hfGeralFormulario.Get('CodigoEtapaWf')  + '&CS=' + hfGeralFormulario.Get('SequenciaOcorrenciaEtapaWf') + '&CP=' + hfGeralFormulario.Get('CodigoProjetoParam');
                                window.top.lpAguardeMasterPage.Show();
                                callbackReload.PerformCallback(paramsRenderiza);

                         }
                         else
                         {
                                window.top.lpAguardeMasterPage.Hide();
                                pcVersoes.PerformCallback();
                         }
                        " + (isPopUp ? "//window.close();" : "") + @"
                    }  
                    else
                    {
                        window.top.lpAguardeMasterPage.Hide();
                        window.top.mostraMensagem(hfGeralFormulario.Get('ErroSalvar'), 'erro', true, false, null);
                    }
                  }";

            // inclui o ASPxHiddenField dentro do painel callback
            pnFormulario.Controls.Add(hfGeralFormulario);

            // inclui o PageControl para renderizar as "Abas" do formulário
            incluiPageControl();

            // aqui será criado os campos do formulário
            renderizaFormulario(pcFormulario, codigoModeloFormulario);

            // se existir ToDoList associado, cria a grid para ediçao
            if (tabPageToDoList != null)
            {
                int codigoTipoAssociacao = getCodigoTipoAssociacao();
                PlanoDeAcao myPlanoDeAcao = new PlanoDeAcao(dados, codigoEntidade, codigoUsuarioResponsavel, codigoProjeto, codigoTipoAssociacao, codigoFormularioMaster, new Unit("100%"), (int)(height.Value - 225), somenteLeitura, null, true, "");
                tabPageToDoList.Controls.Add(myPlanoDeAcao.constroiInterfaceFormulario());
                gvToDoList = myPlanoDeAcao.gvToDoList;

                if (!page.IsCallback)
                    gvToDoList.DataBind();
            }//renderizaToDoList();

            // se existir Documentos Associados, cria a grid para ediçao
            if (tabPageAnexos != null)
                renderizaAnexos(false);


            pnExterno.Controls.Add(pnFormulario);

            // fecha a linha <tr> e insere um em branco
            pnExterno.Controls.Add(
                getLiteral(
                      @"</td>
                    </tr>
                    <tr style=""height: 5px"">
                        <td style=""width: 2px""></td>
                        <td align=""right""></td>
                    </tr>"));
        }

        // Método responsável por escolher o tipo de persistência a ser executada no banco de dados
        protected void pnFormulario_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string mensagemErro_Persistencia = "";
            if (getExisteFormularioEmEdicao())
            {
                mensagemErro_Persistencia = Resources.ExistemSubformuláriosQueEstãoSendoEditados;
            }

            char tipoOperacao = hfSessao.Get("_TipoOperacao_").ToString()[0];

            if (tipoOperacao != 'I')
            {
                //if (hfGeralFormulario.Get("_codigoProjeto_") != null)
                //   codigoProjeto = int.Parse(hfGeralFormulario.Get("_codigoProjeto_").ToString());
                // if (hfGeralFormulario.Get("_CodigoFormularioMaster_") != null)
                //    codigoFormularioMaster = int.Parse(hfGeralFormulario.Get("_CodigoFormularioMaster_").ToString());
                if (hfGeralFormulario.Contains("CodigoInstanciaWf") && hfGeralFormulario.Get("CodigoInstanciaWf") != null)
                    if (!retorno.Contains("CodigoInstanciaWf"))
                        retorno.Add("CodigoInstanciaWf", hfGeralFormulario.Get("CodigoInstanciaWf").ToString());
            }

            EventFormsWF eFormsWf = new EventFormsWF(tipoOperacao, ref codigoProjeto, ref codigoFormularioMaster, ref retorno, parametrosEntrada, camposControladoSistema, TipoFormularioPreDefinido);

            // se existe um evento a ser executado antes de salvar o formulário
            // -----------------------------------------------------------------
            if (mensagemErro_Persistencia == "" && AntesSalvar != null)
            {
                try
                {
                    string mensagemErroEvento = "";
                    AntesSalvar(sender, eFormsWf, ref mensagemErroEvento);
                    if (mensagemErroEvento != "")
                        throw new Exception(mensagemErroEvento);
                    codigoProjeto = eFormsWf.codigoProjeto;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("PRIMARY KEY"))
                    {
                        mensagemErro_Persistencia = Resources.ViolaçãoDeChavePrimáriaEntreEmContatoComOA +
                                                    Resources.NãoÉPossívelProsseguirComAOperação;

                    }
                    else if (ex.Message.Contains("UNIQUE KEY"))
                    {
                        mensagemErro_Persistencia = Resources.JáExisteUmProjetoComONomeInformado +
                                                    Resources.NãoÉPossívelProsseguirComAOperação;

                    }
                    else
                    {
                        mensagemErro_Persistencia = Resources.ErroAoExecutarOEventoAntesSalvar + ex.Message;
                    }
                }
            }

            if (mensagemErro_Persistencia == "")
            {
                System.Collections.Specialized.OrderedDictionary valores = new OrderedDictionary();
                System.Collections.Specialized.OrderedDictionary valoresTexto = new OrderedDictionary();
                getInformacoesFormulario(ref valores, ref valoresTexto);

                mensagemErro_Persistencia = executaAcaoSalvarInformacoes(valores, valoresTexto, tipoOperacao);
            }

            // se existe um evento a ser executado APÓS salvar o formulário
            // ------------------------------------------------------------
            if (mensagemErro_Persistencia == "" && AposSalvar != null)
            {
                try
                {
                    // se é um formulário que esta dentro de um fluxo...
                    if (hfGeralFormulario.Contains("CodigoInstanciaWf") && hfGeralFormulario.Get("CodigoInstanciaWf") != null)
                    {
                        // atualiza o "CodigoInstanciaWorkflow" com o conteúdo do HField "CodigoInstanciaWf"
                        eFormsWf.parametrosEntrada["CodigoInstanciaWorkflow"] = hfGeralFormulario.Get("CodigoInstanciaWf");
                    }

                    eFormsWf.codigoFormulario = codigoFormularioMaster;
                    string mensagemErroEvento = "";
                    AposSalvar(sender, eFormsWf, ref mensagemErroEvento);
                    if (mensagemErroEvento != "")
                        throw new Exception(mensagemErroEvento);

                    if (eFormsWf.parametros.Count > 0)
                    {
                        foreach (DictionaryEntry de in eFormsWf.parametros)
                        {
                            hfGeralFormulario.Set(de.Key.ToString(), de.Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    mensagemErro_Persistencia = Resources.ErroAoExecutarOEventoAposSalvar + ex.Message;
                }
            }

            if (mensagemErro_Persistencia == "") // não deu erro durante o processo de persistência
            {
                hfGeralFormulario.Set("StatusSalvar", "1"); // 1 indica que foi salvo com sucesso.
                hfGeralFormulario.Set("_CodigoFormularioMaster_", codigoFormularioMaster); // é necessário pois será utilizada no click da tab do workflow
                hfSessao.Set("_CodigoFormularioMaster_", codigoFormularioMaster);
                pnFormulario.JSProperties["cp_CF"] = codigoFormularioMaster;
                hfGeralFormulario.Set("CodigoProjetoParam", codigoProjeto);

                //// se existir o botão de impressão, ele será habilitado
                //if (parametrosEntrada != null && parametrosEntrada.Contains("formularioImpressao"))
                //{
                //    Control btnImpressao = ((ASPxCallbackPanel)sender).Parent.FindControl("btnImpressao_1");
                //    if (btnImpressao != null)
                //        ((ASPxButton)btnImpressao).ClientEnabled = true;
                //}

                // se existir Documentos Associados, cria a grid para ediçao
                if (tabPageAnexos != null)
                    renderizaAnexos(true);
            }
            else
            {
                // alguma coisa deu errado...
                hfGeralFormulario.Set("StatusSalvar", "0"); // 0 indica que NÃO foi salvo.
                hfGeralFormulario.Set("ErroSalvar", mensagemErro_Persistencia);
            }
        }

        private void getInformacoesFormulario(ref System.Collections.Specialized.OrderedDictionary valores, ref System.Collections.Specialized.OrderedDictionary textoValores)
        {
            //ASPxPageControl pcFormulario = new ASPxPageControl();
            pcFormulario = (ASPxPageControl)pnFormulario.FindControl("pcFormulario");
            //pcFormulario.Font.Name = "Verdana";
            //pcFormulario.Font.Size = new FontUnit("8pt");
            // Lê a descrição do formulário
            Control controle = pcFormulario.TabPages[0].FindControl("DescricaoFormulario");

            if (controle != null)
            {
                descricaoFormularioSelecionado = ((ASPxTextBox)controle).Text;
            }

            // lê no formulário os valores associados aos campos
            DataSet dsCampos = getCamposModeloFormulario(codigoModeloFormulario, -1);
            foreach (DataRow dr in dsCampos.Tables[0].Rows)
            {
                string idCampo = dr["CodigoTipoCampo"].ToString() + dr["CodigoCampo"].ToString();
                if (dr["CodigoTipoCampo"].ToString() != "SUB")
                {
                    controle = pcFormulario.TabPages[int.Parse(dr["Aba"].ToString())].FindControl(idCampo);
                    if (controle != null)
                    {
                        object valorControle = getValorCampo(controle);
                        string key = idCampo;
                        string value = (valorControle != null) ? valorControle.ToString() : "";
                        valores.Add(key, value);

                        object valorTextoControle = getValorTextoCampo(controle);
                        string valueText = (valorTextoControle != null) ? valorTextoControle.ToString() : "";
                        textoValores.Add(key, valueText);
                    }
                }
            }
        }

        private string executaAcaoSalvarInformacoes(OrderedDictionary valores, OrderedDictionary valoresTexto, char tipoOperacao)
        {
            try
            {
                if (tipoOperacao == 'I' && codigoFormularioMaster <= 0)
                {
                    insereFormulario(false, codigoModeloFormulario, descricaoFormularioSelecionado, codigoUsuarioResponsavel, valores, null, valoresTexto);
                    hfSessao.Set("_TipoOperacao_", "E");
                    tipoOperacao = 'E';
                    pnFormulario.JSProperties["cp_TO"] = tipoOperacao;
                }
                else
                {
                    atualizaFormulario(codigoFormularioMaster, descricaoFormularioSelecionado, codigoUsuarioResponsavel, valores, valoresTexto);
                    hfSessao.Set("_TipoOperacao_", "E");
                    tipoOperacao = 'E';
                    pnFormulario.JSProperties["cp_TO"] = tipoOperacao;
                }
                Session["_ClosePopUp_"] = "S";
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        private void incluiPageControl()
        {
            pcFormulario = new ASPxPageControl();
            pcFormulario.CssFilePath = cssFilePath;
            pcFormulario.CssPostfix = cssPostFix;
            //pcFormulario.Font.Name = "Verdana";
            //pcFormulario.Font.Size = new FontUnit("18pt");
            pcFormulario.ID = "pcFormulario";
            pcFormulario.ClientInstanceName = pcFormulario.ID;
            pcFormulario.Height = (int)height.Value - 20;
            pcFormulario.Width = new Unit("100%");
            pcFormulario.ContentStyle.Paddings.Padding = 3;
            pcFormulario.TabPages.Clear();

            // ####### Esta linha foi uma tentativa de ganhar espaço no formulário dinâmico fazendo com que o menu horizontal
            // passasse a ser exibido no modo vertical, ganhando espaço (altura) na tela.
            // Essa iniciativa não deu certo em dispositivos móveis (PBH).
            // Estou desabilitando (Guilherme Cruz - 08/11/2019)
            //pcFormulario.TabPosition = TabPosition.Left;

            DataSet ds = dados.getDataSet("Select * from modeloFormulario where codigoModeloFormulario = " + codigoModeloFormulario);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];

                // Se é para mostrar os títulos do formulário...
                if (mostrarTitulos)
                {
                    ASPxLabel lblNomeFormulario = new ASPxLabel();
                    lblNomeFormulario.ID = "lblNomeFormulario";
                    lblNomeFormulario.CssFilePath = cssFilePath;
                    lblNomeFormulario.CssPostfix = cssPostFix;
                    //lblNomeFormulario.Font.Bold = true;

                    ASPxLabel lblDescricaoFormulario = new ASPxLabel();
                    lblDescricaoFormulario.ID = "lblDescricaoFormulario";
                    lblDescricaoFormulario.CssFilePath = cssFilePath;
                    lblDescricaoFormulario.CssPostfix = cssPostFix;

                    lblNomeFormulario.Text = modeloNomeFormulario;
                    lblDescricaoFormulario.Text = modeloDescricaoFormulario;

                    pnFormulario.Controls.Add(lblNomeFormulario);
                    pnFormulario.Controls.Add(getLiteral("<br>"));
                    pnFormulario.Controls.Add(lblDescricaoFormulario);
                }

                string[] aAbas = dt.Rows[0]["Abas"].ToString().Split('\r');
                foreach (string aba in aAbas)
                {
                    if (aba != "")
                    {
                        TabPage tb = pcFormulario.TabPages.Add(aba.Replace("\n", ""));
                        //tb.TabStyle.Font.Name = "Verdana";
                        //tb.TabStyle.Font.Size = new FontUnit("8pt");
                    }
                }

                // se o formulário possui ToDoList associado, vamos inserir um nova aba
                if (dt.Rows[0]["IndicaToDoListAssociado"].ToString() == "S")
                {
                    tabPageToDoList = pcFormulario.TabPages.Add(Resources.PlanoDeAção, "tabPageToDoList");
                    //tabPageToDoList.TabStyle.Font.Name = "Verdana";
                    //tabPageToDoList.TabStyle.Font.Size = new FontUnit("8pt");
                }
                // se o formulário possui ArquivosAnexos associado, vamos inserir um nova aba
                if (dt.Rows[0]["IndicaAnexoAssociado"].ToString() == "S")
                {
                    tabPageAnexos = pcFormulario.TabPages.Add(Resources.Documentos, "tabPageAnexos");
                    //tabPageAnexos.TabStyle.Font.Name = "Verdana";
                    //tabPageAnexos.TabStyle.Font.Size = new FontUnit("8pt");
                }

                pcFormulario.ClientSideEvents.ActiveTabChanging =
                    @"
                    function(s, e) 
                    {
                        if (e.tab.name=='tabPageToDoList')
                        {
                            if (!hfGeralFormulario.Contains('_CodigoFormularioMaster_') || hfGeralFormulario.Get('_CodigoFormularioMaster_') == '' )
                            {
                                var abaPrincipal = pcFormulario.GetTab(0).GetText();
                                window.top.mostraMensagem('" + Resources.OPlanoDeAçãoSóPodeSerDefinidoApósAsInforma + @" [' + abaPrincipal + '] " + Resources.teremSidoSalvas + @".', 'erro', true, false, null);
                                e.cancel = true;
                            }
                            if (window.hfGeralToDoList)
                            {
                                if (hfGeralToDoList.Get('codigoObjetoAssociado') != hfGeralFormulario.Get('_CodigoFormularioMaster_'))
                                {
                                    hfGeralToDoList.Set('codigoObjetoAssociado', hfGeralFormulario.Get('_CodigoFormularioMaster_'));
                                    gvToDoList.PerformCallback('Popular');
                                }
                            }
                            else
                                window.top.mostraMensagem('" + Resources.NãoFoiPossívelEncontrarOComponenteToDoList + @"', 'erro', true, false, null);
                        }
                        else if (e.tab.name=='tabPageAnexos')
                        {                               
                            if (!hfGeralFormulario.Contains('_CodigoFormularioMaster_') || hfGeralFormulario.Get('_CodigoFormularioMaster_') == '' )
                            {
                                var abaPrincipal = pcFormulario.GetTab(0).GetText();
                                window.top.mostraMensagem('" + Resources.SóÉPossívelAnexarDocumentosApósAsInformaçõ + @" [' + abaPrincipal + '] " + Resources.teremSidoSalvas + @".', 'erro', true, false, null);
                                e.cancel = true;
                            }else 
                            {
                                document.getElementById('frmAnexo').src = s.cp_urlAnexos;
                            }   
                        }
                    }";
            }

            // se o formulário só possui uma aba, esta não será apresentada
            if (pcFormulario.TabPages.Count == 1)
                pcFormulario.ShowTabs = false;

            for (int aba = 0; aba < pcFormulario.TabPages.Count; aba++)
            {
                pcFormulario.TabPages[aba].TabStyle.HorizontalAlign = HorizontalAlign.Left;
            }

            pnFormulario.Controls.Add(pcFormulario);
        }

        // metodo responsável por criar os campos dentro da estrutura do formulário.
        private void renderizaFormulario(ASPxPageControl pcFormulario, int codigoModeloFormulario)
        {
            int margemFormulario = 2;
            camposControladoSistema = new List<object[]>();
            camposFormulario = new List<object[]>();
            for (int aba = 0; aba <= pcFormulario.TabPages.Count - 1; aba++)
            {
                DataSet dsCampos = getCamposModeloFormulario(codigoModeloFormulario, aba);
                DataTable dtCampos = dsCampos.Tables[0];

                string ocultaTabelaEmAnexos = "";
                if (pcFormulario.TabPages[aba].Name == "tabPageAnexos")
                {
                    ocultaTabelaEmAnexos = "display:none;";
                }
                pcFormulario.TabPages[aba].Controls.Add(getLiteral(
                    string.Format(
                        @"<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-color:Green;width:{1};{2}"">
                    <tr>
                        <td style=""width:{0}px;""></td>
                        <td style=""display: flex; flex-wrap: wrap;""></td>
                    </tr>", margemFormulario, "100%", ocultaTabelaEmAnexos)));

                Control controleLinha = new Control();
                controleLinha.Controls.Add(getLiteral(string.Format(@"<table style=""width:100%""><tr>")));

                int index = 0;
                // adiciona os campos definidos pelo administrador
                foreach (DataRow dr in dtCampos.Rows)
                {
                    // veja se a getCamposModeloFormulario já trouxe da base um formulário master
                    // esse situação ocorre quando o formulário atual, mesmo que não criado ainda, deve apresentar 
                    // uma lista padrão do projeto
                    if ((index == 0) && (codigoFormularioMaster <= 0) && (codigoProjeto > 0))
                    {
                        if (string.IsNullOrEmpty(dr["CodigoFormularioMaster"].ToString()) == false)
                        {
                            int.TryParse(dr["CodigoFormularioMaster"].ToString(), out codigoFormularioMaster);
                        }
                    }
                    Control controle = null;
                    index++;
                    Control novoControle = dr["CodigoTipoCampo"].ToString() == "LNP" ? renderizaCampoLinkCrono(dr) : renderizaCampo(dr, aba, true, "", null, out controle);

                    if (mudaLinha && index != 0)
                    {
                        controleLinha.Controls.Add(getLiteral(string.Format(@"</tr></table>")));
                        controleLinha.Controls.Add(getLiteral(string.Format(@"<table style=""width: 100%;""><tr>")));
                    }

                    controleLinha.Controls.Add(getLiteral(string.Format(@"<td>")));
                    controleLinha.Controls.Add(novoControle);
                    controleLinha.Controls.Add(getLiteral(string.Format(@"</td>")));

                    if (mudaLinha && index == 0)
                    {
                        controleLinha.Controls.Add(getLiteral(string.Format(@"</tr></table>")));
                    }
                }

                controleLinha.Controls.Add(getLiteral(string.Format(@"</tr></table>")));

                pcFormulario.TabPages[aba].Controls.Add(getLiteral(
                       string.Format(@"<tr><td style=""width:{0}px;""></td><td style=""display: flex; flex-wrap: wrap;"">", margemFormulario)));
                pcFormulario.TabPages[aba].Controls.Add(controleLinha);
                pcFormulario.TabPages[aba].Controls.Add(getLiteral("<p>"));
                pcFormulario.TabPages[aba].Controls.Add(getLiteral("</td></tr>"));

                pcFormulario.TabPages[aba].Controls.Add(getLiteral("</table>"));
            }

            // Associa o evento que deve ser executado sempre que o conteúdo do campo for alterado
            associaEventoAlteracao();

        }

        public ASPxPanel inicializaFormulario()
        {
            ASPxPanel pnExterno = new ASPxPanel();
            pnExterno.Width = new Unit("100%");
            pnExterno.ID = "pnExterno";
            pnExterno.ClientInstanceName = "pnExterno";

            // se o modelo não estiver válido
            string msgValidacaoFormulario = validarModeloFormulario();
            if (msgValidacaoFormulario != "")
            {
                avisoFormularioInvalido(pnExterno, msgValidacaoFormulario);
                return pnExterno;
            }

            // inclui a tag <table> que delimita a área utilizada pelos controles 
            pnExterno.Controls.Add(getLiteral(string.Format(
                @"<table border=""0"" bordercolor=red cellpadding=""0"" cellspacing=""0"" style=""width: 100%"">", width.ToString())));

            // botões superiores
            //incluiBotoesSalvarCancelar(pnExterno, 1, true);

            // o formulário será renderizzado dentro de um painel callback.
            incluiPanelCallback(pnExterno);

            // botões inferiores
            incluiBotoesSalvarCancelar(pnExterno, 2, false);

            // inclui a tag </table> que delimita a área utilizada pelos controles 
            pnExterno.Controls.Add(getLiteral(
                @"</table>"));

            pnExterno.Controls.Add(gridExporterExcel);
            return pnExterno;
        }

        private ASPxButton getBotaoSalvar(string ID, string Text)
        {
            ASPxButton btnSalvar = new ASPxButton();
            //btnSalvar.Font.Name = "Verdana";
            //btnSalvar.Font.Size = new FontUnit("8pt");
            btnSalvar.ID = ID;
            btnSalvar.Text = Text;
            btnSalvar.CssFilePath = cssFilePath;
            btnSalvar.CssPostfix = cssPostFix;
            btnSalvar.Enabled = !somenteLeitura;
            btnSalvar.Width = new Unit("95px");
            //btnSalvar.Click += btnSalvar_Click;
            // if (isPopUp)
            // {
            btnSalvar.ClientSideEvents.Click =
                @"function(s, e) 
                      {
                        var cancela = false;
                        if (window.onClickBotaoSalvar)
                        {
                            cancela = onClickBotaoSalvar();
                        } 
                        if (cancela == false)
                        {
                           var SolicitarPublicacao = 'N';
                           if (window.hfMostraDivPublicar)
                              SolicitarPublicacao = document.getElementById('hfMostraDivPublicar').value;
                            if (SolicitarPublicacao=='S')
                            {
                                retorno = mostrarPublicacao();
                                document.getElementById('hfPublicarFormulario').value = retorno;
                            }
                            e.processOnServer = false; 
                            hfGeralFormulario.Set('StatusSalvar','0');

                            // 01/11/2011 - mudanças ACG para tratar vários formulários dentro de um fluxo 
                            // se estiver dentro de um fluxo...
                            if (window.parent.hfGeralWorkflow)
                            {
                                // copia o 'CodigoInstanciaWf' da frame Fluxo para a frame do formulário atual
                                var CodigoInstanciaWf = window.parent.hfGeralWorkflow.Get('CodigoInstanciaWf');
                                hfGeralFormulario.Set('CodigoInstanciaWf', CodigoInstanciaWf);
                            }

                            if (!validaCamposObrigatorios())//Alterado em 10/09/2018 16:10
                               window.top.mostraMensagem('" + Resources.ExistemCamposObrigatóriosQueNãoForamPreenc + @"', 'erro', true, false, null);
                            else
                                {
                                    window.top.lpAguardeMasterPage.Show();
                                    pnFormulario.PerformCallback();
                                }
                        }
                        else
                            e.processOnServer = false;
                      }
                    ";
            // }
            return btnSalvar;
        }

        private ASPxButton getBotaoCancelar(string ID, string Text)
        {
            ASPxButton btnCancelar = new ASPxButton();
            //btnCancelar.Font.Name = "Verdana";
            //btnCancelar.Font.Size = new FontUnit("8pt");
            btnCancelar.ID = ID;
            btnCancelar.Text = Text;
            btnCancelar.CssFilePath = cssFilePath;
            btnCancelar.CssPostfix = cssPostFix;
            btnCancelar.Click += btnCancelar_Click;
            btnCancelar.Width = new Unit("95px");
            if (isPopUp)
            {
                btnCancelar.ClientSideEvents.Click =
                    @"function(s, e) 
                      {
                        e.processOnServer = false;
                        window.returnValue = 'Cancel';
                        if (hfGeralFormulario.Contains('StatusSalvar') && hfGeralFormulario.Get('StatusSalvar')=='1')
                        {
                              window.returnValue = 'OK';
	                          if(window.top.pcFormularioDinamico.GetVisible() == true)
                              {
                                  window.top.fechaModalFormularioDinamico(window.returnValue, hfGeralFormulario.Get('StatusSalvar'));
                              }
                              else
                              {
                                  window.top.fechaModal(window.returnValue);
                              }
                        }
                        else
                        {
	                          if(window.top.pcFormularioDinamico.GetVisible() == true)
                              {                              
                                  window.top.fechaModalFormularioDinamico(window.returnValue, hfGeralFormulario.Get('StatusSalvar'));
                              }
                              else
                              {
                                  window.top.fechaModal(window.returnValue);
                              }
                        }
                      }";
            }
            return btnCancelar;
        }

        private ASPxButton getBotaoImprimir(string ID, string Text)
        {
            string formularioImpressao = parametrosEntrada["formularioImpressao"].ToString();
            // insere o script responsável por abrir o pop-up de edição da grid.
            // 21/07/2015 - assinatura digital: Inclusão do parametro "AD" para indicar a função showModal se é uma impressão normal ou a impressão de um form assinado.
            string parametros = string.Format(formularioImpressao + "?CE={0}&CMF={1}&CP={2}&US={3}&AD={4}", codigoEntidade, codigoModeloFormulario, codigoProjeto, codigoUsuarioResponsavel, indicaFormAssinado ? 1 : 0);

            ASPxButton btnImprimir = new ASPxButton();
            //btnImprimir.Font.Name = "Verdana";
            //btnImprimir.Font.Size = new FontUnit("8pt");
            btnImprimir.ID = ID;
            btnImprimir.ClientInstanceName = ID;
            btnImprimir.Text = Text;
            btnImprimir.CssFilePath = cssFilePath;
            btnImprimir.CssPostfix = cssPostFix;
            btnImprimir.Width = new Unit("95px");
            //btnImprimir.Click += btnImprimir_Click;
            string indicaModal = isPopUp ? "window.showModal" : "window.top.showModal";
            // 21/07/2015 - assinatura digital: Inclusão da variável "formularioAssinado" e do IF para tratá-la.
            // a função javascript "ImprimeFormAssinado(param)", foi incluída no arquivo "wfRenderizaFormulario.aspx"
            btnImprimir.ClientSideEvents.Click =
                @"function(s, e) 
                  {
                      var sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
                      var sHeight = Math.max(0, document.documentElement.clientHeight) - 155;
            
                     e.processOnServer = false;
                     var formularioAssinado = " + (indicaFormAssinado ? 1 : 0) + @"
                     var codigoFormularioMaster = hfGeralFormulario.Get('_CodigoFormularioMaster_');

                     if (codigoFormularioMaster == null ||  codigoFormularioMaster == '' || codigoFormularioMaster <= 0)
                        window.top.mostraMensagem('" + Resources.OFormulárioPrecisaSerSalvoAntesDeSerImpres + @"', 'erro', true, false, null);
                     else
                        if (formularioAssinado == 1)
                            ImprimeFormAssinado('" + parametros + @"&CF='+codigoFormularioMaster );
                        else
                            " + indicaModal + "('" + parametros + @"&CF='+codigoFormularioMaster, 'Impressão', sWidth, sHeight, '', null);
                  }";
            return btnImprimir;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (isPopUp)
            {

            }
        }

        //protected void btnImprimir_Click(object sender, EventArgs e)
        //{
        //    string mensagemErroEvento = "";
        //    EventFormsWF eFormsWf = new EventFormsWF('P', ref codigoProjeto, ref codigoFormularioMaster, ref retorno, parametrosEntrada, camposControladoSistema, TipoFormularioPreDefinido);
        //    Imprimir(sender, eFormsWf, ref mensagemErroEvento);
        //    if (mensagemErroEvento != "")
        //        throw new Exception(mensagemErroEvento);
        //}

        /*protected void btnTemp_Click(object sender, EventArgs e)
        {
            if (hfGeralFormulario.Contains("_CodigoFormularioMaster_"))
                (sender as ASPxButton).Text = hfGeralFormulario.Get("_CodigoFormularioMaster_").ToString();
        }*/

        private Control renderizaCampoLinkCrono(DataRow drCampo)
        {
            string texto = drCampo["NomeCampo"].ToString();
            mudaLinha = true;
            Panel pnExterno = new Panel();
            pnExterno.ID = "pnExterno_" + drCampo["CodigoTipoCampo"].ToString() + drCampo["CodigoCampo"].ToString();

            if (!somenteLeitura && codigoProjeto > 0)
            {
                pnExterno.Controls.Add(getLiteral(
                    string.Format(
                        @"<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width:100%"">
                        <tr><td valign=""top"">
                            <a href='#' onclick='abreCrono();'>{0}</a>
                        </td></tr></table>", texto)));
            }

            return pnExterno;
        }

        private Control renderizaCampo(DataRow drCampo, int indexAba, bool incluirRotuloCampo, string idCampoREF, bool? somenteLeituraREF, out Control controle)
        {
            string larguraCampo = (width.Value - 50) + "px";
            int codigoCampo = int.Parse(drCampo["CodigoCampo"].ToString());
            //string idCampo = "id_" + drCampo["CodigoCampo"].ToString();
            string idCampo;
            // se a chamada veio a partir de um campo REF, o idCampo deve ser o do campo que fez a chamada.
            if (idCampoREF != "")
                idCampo = idCampoREF;
            else
                idCampo = drCampo["CodigoTipoCampo"].ToString() + drCampo["CodigoCampo"].ToString();

            string nomeCampo = drCampo["NomeCampo"].ToString();
            string descricaoCampo = drCampo["DescricaoCampo"].ToString();
            string definicaoCampo = drCampo["DefinicaoCampo"].ToString();
            string codigoTipoCampo = drCampo["CodigoTipoCampo"].ToString();
            bool obrigatorio = drCampo["CampoObrigatorio"].ToString() == "S";
            string[] aDefinicaoCampo = definicaoCampo.Split(DelimitadorPropriedadeCampo);

            Panel pnExterno = new Panel();
            pnExterno.ID = "pnExterno_" + idCampo;
            if (somenteLeituraREF.HasValue && somenteLeituraREF.Value)
                pnExterno.Enabled = false;

            // se a chamada veio a partir de um campo REF, o idCampo deve ser o do campo que fez a chamada.
            if (idCampoREF != "")
                pnExterno.ID += "_ref";

            // Os campos do tipo "REF" podem não ter o cabeçalho
            if (codigoTipoCampo == "REF")
            {
                incluirRotuloCampo = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim() == "S";
            }
            int margemCampo = 0;
            pnExterno.Controls.Add(getLiteral(
                string.Format(
                    @"<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width:100%"">
                   ")));

            if (incluirRotuloCampo)
            {
                // adiciona o nome do campo
                pnExterno.Controls.Add(getLiteral(
                    string.Format(@"<tr><td><table style=""width: 100%""><tr><td valign='bottom'>", margemCampo)));
                ASPxLabel lblTitulo = new ASPxLabel();
                //lblTitulo.Font.Name = "Verdana";
                //lblTitulo.Font.Size = new FontUnit("8pt");
                lblTitulo.Text = nomeCampo.Contains(":") ? nomeCampo : (nomeCampo + ":");
                lblTitulo.Wrap = DevExpress.Utils.DefaultBoolean.True;
                lblTitulo.ToolTip = descricaoCampo;

                pnExterno.Controls.Add(lblTitulo);
                if (codigoTipoCampo == "SUB")
                {
                    if (descricaoCampo.Trim() != "")
                    {
                        pnExterno.Controls.Add(getLiteral("</td><td>"));
                        System.Web.UI.WebControls.Image imgAjudaTitulo = new System.Web.UI.WebControls.Image();
                        imgAjudaTitulo.ImageUrl = "~/imagens/ajuda.png";
                        imgAjudaTitulo.ToolTip = descricaoCampo;
                        imgAjudaTitulo.Style.Add("vertical-align", "top");
                        imgAjudaTitulo.Style.Add("cursor", "hand");
                        pnExterno.Controls.Add(imgAjudaTitulo);
                    }
                    // mostrar botão para exportar para excel
                    // --------------------------------------
                    pnExterno.Controls.Add(getLiteral("</td><td>"));
                    ASPxButton btnExportarExcel = new ASPxButton();
                    btnExportarExcel.ImageUrl = @"~\imagens\botoes\btnExcel.png";
                    btnExportarExcel.RenderMode = ButtonRenderMode.Link;
                    btnExportarExcel.ID = "btnExpSubExcel_" + codigoCampo + "_" + aDefinicaoCampo[0].Replace("Sub: ", "") + "_" + indexAba;
                    btnExportarExcel.Click += BtnExportarExcel_Click;
                    pnExterno.Controls.Add(btnExportarExcel);
                }
                pnExterno.Controls.Add(getLiteral("</td></tr></table>"));
                pnExterno.Controls.Add(getLiteral("</td></tr>"));
            }

            pnExterno.Controls.Add(getLiteral(
                string.Format(@"<tr><td valign=""top"">", margemCampo)));

            controle = renderizaCampoGeral(codigoTipoCampo, codigoCampo, nomeCampo, aDefinicaoCampo, idCampo, obrigatorio, larguraCampo, indexAba, descricaoCampo);

            controle.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            if (controle.ID == null || controle.ID == "")
                controle.ID = idCampo;



            if (controle is ASPxEdit)
                ((ASPxEdit)controle).ClientInstanceName = "id_" + codigoCampo;

            if (controle is ASPxEditBase)
            {
                ((ASPxEditBase)controle).JSProperties.Add("cpExpressaoValidacao", drCampo["ExpressaoValidacao"] + "");
                ((ASPxEditBase)controle).JSProperties.Add("cpMensagemValidacao", drCampo["MensagemValidacao"] + "");
                ((ASPxEditBase)controle).JSProperties.Add("cpNomeCampo", drCampo["NomeCampo"] + "");
                ((ASPxEditBase)controle).JSProperties.Add("cpIndexAba", indexAba + "");
            }

            if (obrigatorio && codigoTipoCampo != "CAL")
            {
                if ((codigoTipoCampo == "TXT") || (codigoTipoCampo == "SUB"))
                {
                    pnExterno.Controls.Add(getLiteral(@"<table style=""width: 100%;""><tr><td>"));
                }
                else
                {
                    pnExterno.Controls.Add(getLiteral(@"<table style=""width: 100%;""><tr><td>"));
                }
                pnExterno.Controls.Add(controle);
                if (codigoTipoCampo == "SUB")
                    pnExterno.Controls.Add(getLiteral(string.Format("</td><td valign='top'>* <img style='display:none' id='ico-required-field_sub{0}' title='*' class='dxEditors_edtError_MaterialCompact' src='/DXR.axd?r=1_123-gVMpj' alt='*'></td></tr></table>", codigoCampo)));
                else
                    pnExterno.Controls.Add(getLiteral("</td><td valign='top'>*</td></tr></table>"));
                if (codigoTipoCampo == "TXT")
                {
                    if (controle is ASPxMemo)
                    {
                        string tamanhoMaxMemo = aDefinicaoCampo[0].Split(':')[1].Trim();
                        string traducao_de = "de";//Resources.traducao.de;
                        string funcaoChange = string.Format(@"{1}.SetText(s.GetInputElement().value.length + ' {3} ' + {0}); ", tamanhoMaxMemo, "labelContador_" + ((ASPxMemo)controle).ClientInstanceName, ((ASPxMemo)controle).ClientInstanceName, traducao_de);

                        string funcao = "function(){" + funcaoChange + "}";

                        string funcaoInit = string.Format(@"{2}.GetInputElement().maxLength = {0}; 
                                            {1}.SetText({2}.GetInputElement().value.length + ' {4} ' + {0}); 
                                            ASPxClientUtils.AttachEventToElement({2}.GetInputElement(), ""input"", {3});
                                            ASPxClientUtils.AttachEventToElement({2}.GetInputElement(), ""change"", {3});
                                            ASPxClientUtils.AttachEventToElement({2}.GetInputElement(), ""keyup"", {3});", tamanhoMaxMemo, "labelContador_" + ((ASPxMemo)controle).ClientInstanceName, ((ASPxMemo)controle).ClientInstanceName, funcao, traducao_de);

                        ((ASPxMemo)controle).ClientSideEvents.Init = "function(s,e){" + funcaoInit + " }";
                        ((ASPxMemo)controle).ClientSideEvents.Validation = "function(s,e){" + funcaoChange + " }";

                        //pnExterno.Controls.Add(getLiteral(@"<table style=""width: 100%;"">"));
                        //pnExterno.Controls.Add(getLiteral(@"<tr>"));
                        //pnExterno.Controls.Add(getLiteral(@"<td>"));
                        ASPxLabel labelContador = new ASPxLabel();
                        labelContador.Font.Bold = true;
                        labelContador.Font.Size = new FontUnit("7pt");
                        labelContador.ForeColor = Color.FromName("#999999");
                        labelContador.ID = "labelContador_" + ((ASPxMemo)controle).ClientInstanceName;
                        labelContador.ClientInstanceName = "labelContador_" + ((ASPxMemo)controle).ClientInstanceName;
                        labelContador.Style.Add("padding-left", "5px");
                        //labelContador.Text = "Contador de caracteres";
                        pnExterno.Controls.Add(labelContador);
                        //pnExterno.Controls.Add(getLiteral(@"</tr>"));
                        //pnExterno.Controls.Add(getLiteral(@"</td>"));
                        //pnExterno.Controls.Add(getLiteral(@"</table>"));
                    }
                }
            }
            else
            {
                pnExterno.Controls.Add(controle);
                pnExterno.Controls.Add(getLiteral("&nbsp;"));
            }

            if (codigoTipoCampo != "SUB" && descricaoCampo.Trim() != "")
            {
                pnExterno.Controls.Add(getLiteral("</td><td align='left' style='width:16px'>"));
                System.Web.UI.WebControls.Image imgAjudaTitulo = new System.Web.UI.WebControls.Image();
                imgAjudaTitulo.ImageUrl = "~/imagens/ajuda.png";
                imgAjudaTitulo.ToolTip = descricaoCampo;
                //imgAjudaTitulo.Style.Add("vertical-align", "top");
                imgAjudaTitulo.Style.Add("cursor", "hand");
                pnExterno.Controls.Add(imgAjudaTitulo);
            }

            pnExterno.Controls.Add(getLiteral("</td></tr>"));
            pnExterno.Controls.Add(getLiteral("</table>"));

            // todos os campos serão adicionados a lista "camposFormulario"
            object[] infoCampoAtual = new object[4];
            infoCampoAtual[0] = codigoCampo;
            infoCampoAtual[1] = codigoTipoCampo;
            infoCampoAtual[2] = aDefinicaoCampo;
            infoCampoAtual[3] = controle;
            camposFormulario.Add(infoCampoAtual);


            // Os campos controlados pelo sistema devem ser inseridos em uma lista
            if (drCampo["IndicaControladoSistema"].ToString() == "S")
            {
                object[] infoCampos = new object[3];
                infoCampos[0] = drCampo["IniciaisCampoControladoSistema"].ToString();
                infoCampos[1] = drCampo["CodigoCampo"].ToString();
                infoCampos[2] = controle;
                camposControladoSistema.Add(infoCampos);

                if (drCampo["IniciaisCampoControladoSistema"].ToString() == "PUBL")
                {
                    (controle as ASPxDateEdit).ReadOnly = true;
                    (controle as ASPxDateEdit).ClientEnabled = false;
                    (controle as ASPxDateEdit).BackColor = corReadOnly;
                }

            }
            return pnExterno;
        }

        private void BtnExportarExcel_Click(object sender, EventArgs e)
        {
            string IdButtonExpExcelClicado = ((ASPxButton)sender).ID;
            string[] aParametros = IdButtonExpExcelClicado.Split('_');
            string idGrid = string.Format("SUB{0}_CMF{1}_ABA{2}_{0}", aParametros[1], aParametros[2], aParametros[3]);
            gridExporterExcel.GridViewID = idGrid;
            gridExporterExcel.DataBind();
            gridExporterExcel.WriteXlsxToResponse(new DevExpress.XtraPrinting.XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
        }

        private Control renderizaCampoGeral(string codigoTipoCampo, int codigoCampo, string nomeCampo, string[] aDefinicaoCampo, string idCampo, bool obrigatorio, string larguraCampo, int indexAba, string descricaoCampo)
        {
            Control controle = null;
            if (codigoTipoCampo == "VAR")
            {
                controle = renderizaCampoVAR(obrigatorio, aDefinicaoCampo, larguraCampo, nomeCampo, descricaoCampo);
            }
            else if (codigoTipoCampo == "TXT")
            {
                mudaLinha = true;
                //Unit larguraTxt = new Unit(larguraCampo);
                Unit larguraTxt = new Unit("100%");
                controle = renderizaCampoTXT(obrigatorio, aDefinicaoCampo, larguraCampo, descricaoCampo);
            }
            else if (codigoTipoCampo == "NUM")
            {
                controle = renderizaCampoNUM(obrigatorio, aDefinicaoCampo, larguraCampo, nomeCampo, descricaoCampo);
            }
            else if (codigoTipoCampo == "LST")
            {
                controle = renderizaCampoLST(obrigatorio, nomeCampo, aDefinicaoCampo, larguraCampo, nomeCampo, descricaoCampo);
            }
            else if (codigoTipoCampo == "DAT")
            {
                controle = renderizaCampoDAT(obrigatorio, aDefinicaoCampo, larguraCampo, nomeCampo, descricaoCampo);
            }
            else if (codigoTipoCampo == "SUB")
            {
                mudaLinha = true;
                Unit larguraTxt = new Unit(larguraCampo);
                widthLinhaAtual = larguraTxt.Value + 15;
                // nos sub-formulários, o ID também terá o codigoModeloDoFormulário
                controle = renderizaCampoSUB(codigoCampo, aDefinicaoCampo, larguraCampo, indexAba);
                controle.ID = idCampo + "_" + controle.ID;

                //Adiciona o 'ClientInstanceName' dos subformulários ao 'hfGeralFormulario' 
                //caso sejam obrigatórios para possibilitar a devida validação no lado cliente
                //Eduardo Rocha 01/04/2020
                if (obrigatorio)
                {
                    var key = string.Format("sub-obrigatorio_{0}", indexAba);
                    var clientInstanceName = ((ASPxGridBase)controle).ClientInstanceName;
                    if (!hfGeralFormulario.Contains(key))
                        hfGeralFormulario.Add(key, clientInstanceName);
                    else
                        hfGeralFormulario.Set(key, string.Format("{0};{1}", hfGeralFormulario.Get(key), clientInstanceName));
                }
            }
            else if (codigoTipoCampo == "CPD")
            {
                controle = renderizaCampoCPD(codigoCampo, aDefinicaoCampo, larguraCampo, nomeCampo, descricaoCampo);
            }
            else if (codigoTipoCampo == "LOO")
            {
                controle = renderizaCampoLOO(obrigatorio, nomeCampo, aDefinicaoCampo, larguraCampo, nomeCampo, descricaoCampo);
            }
            else if (codigoTipoCampo == "CAL")
            {
                formularioPossuiCampoCalculado = true;
                controle = renderizaCampoCAL(codigoCampo, nomeCampo, aDefinicaoCampo, larguraCampo, nomeCampo, descricaoCampo);
            }
            else if (codigoTipoCampo == "REF")
            {
                mudaLinha = true;
                Unit larguraTxt = new Unit(larguraCampo);
                widthLinhaAtual = larguraTxt.Value + 15;
                controle = renderizaCampoREF(codigoCampo, obrigatorio, aDefinicaoCampo, larguraCampo, indexAba, idCampo);
            }

            return controle;
        }

        private Control renderizaCampoVAR(bool obrigatorio, string[] aDefinicaoCampo, string larguraCampo, string nomeCampo, string descricaoCampo)
        {
            string tamanho = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            string mascara = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
            string padrao = aDefinicaoCampo.Length > 2 ? aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim() : "";

            Unit larguraTxt = new Unit(larguraCampo);

            if (tamanho != "" && int.Parse(tamanho) > 0)
            {
                larguraTxt = (int.Parse(tamanho) * 7 + 20) > larguraTxt.Value ? larguraTxt : new Unit((int.Parse(tamanho) * 7 + 30).ToString());
            }

            double tamanhoParam = nomeCampo.Length;

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {
                SizeF size = graphics.MeasureString(nomeCampo, new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                tamanhoParam = size.Width + 23;
            }

            larguraTxt = (tamanhoParam) < larguraTxt.Value ? larguraTxt : new Unit((tamanhoParam).ToString());

            if (larguraTxt.Value + widthLinhaAtual > (width.Value - 30))
            {
                mudaLinha = true;
                widthLinhaAtual = larguraTxt.Value + 15;
            }
            else
            {
                mudaLinha = false;
                widthLinhaAtual += larguraTxt.Value + 15;
            }


            ASPxTextBox controle = new ASPxTextBox();
            //controle.Font.Name = "Verdana";
            //controle.Font.Size = new FontUnit("8pt");
            controle.CssFilePath = cssFilePath;
            controle.CssPostfix = cssPostFix;
            controle.Width = larguraTxt;// new Unit(larguraCampo);
            controle.Width = new Unit("100%");// new Unit(larguraCampo);
            if (mascara == "" || mascara == "@")
                controle.MaxLength = int.Parse(tamanho);
            controle.ToolTip = descricaoCampo;
            controle.ValidationSettings.Display = Display.Dynamic;

            if (mascara == "@")
            {
                controle.ValidationSettings.RegularExpression.ValidationExpression = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                controle.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                controle.ValidationSettings.RegularExpression.ErrorText = Resources.EmailInválido;
            }
            else if (mascara.ToLower() == "h")
            {
                controle.MaskSettings.Mask = "HH:mm";
                controle.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                controle.ValidationSettings.ErrorText = Resources.HoraInválida;
                controle.ValidationSettings.RegularExpression.ErrorText = Resources.HoraInválida;
            }
            else
            {
                char[] arrayDeMascara = mascara.ToCharArray();
                for (int i = 0; i < arrayDeMascara.Length; i++)
                {
                    if (arrayDeMascara[i] == '.')
                    {
                        arrayDeMascara[i] = ',';
                    }
                    else
                    {
                        if (arrayDeMascara[i] == ',')
                        {
                            arrayDeMascara[i] = '.';
                        }
                    }
                }
                controle.MaskSettings.Mask = new string(arrayDeMascara);

                controle.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.None;
                controle.ValidationSettings.ErrorText = Resources.FormatoInválido;
                controle.ValidationSettings.RegularExpression.ErrorText = Resources.FormatoInválido;
            }

            if (obrigatorio)
            {
                controle.ValidationSettings.RequiredField.IsRequired = true;
                controle.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                controle.ValidationSettings.ErrorFrameStyle.Paddings.Padding = new Unit("0px");
            }
            else
            {
                controle.ClientSideEvents.Validation = @"function(s,e)
                { 
                    if(s.GetValue() == null) 
                        e.isValid = true; 
                    else if(e.isValid == false)
                        e.errorText = '" + Resources.FormatoInválido + @"';
                }";
            }

            controle.ReadOnly = somenteLeitura;

            controle.ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
            controle.ReadOnlyStyle.ForeColor = Color.Black;
            controle.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
            controle.DisabledStyle.ForeColor = Color.Black;


            if (padrao != "")
                controle.Text = padrao;

            controle.ValidationSettings.ValidationGroup = "FormularioCDIS";
            return controle;
        }

        private Control renderizaCampoTXT(bool obrigatorio, string[] aDefinicaoCampo, string larguraCampo, string descricaoCampo)
        {
            string tamanho = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            string linhas = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
            string formato = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
            string padrao = aDefinicaoCampo.Length > 3 ? aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim() : "";

            Control controle = null;
            if (formato == "0")
            {
                controle = new ASPxMemo();
                //((ASPxMemo)controle).Font.Name = "Verdana";
                //((ASPxMemo)controle).Font.Size = new FontUnit("8pt");
                ((ASPxMemo)controle).CssFilePath = cssFilePath;
                ((ASPxMemo)controle).CssPostfix = cssPostFix;
                //((ASPxMemo)controle).Width = new Unit(larguraCampo);
                ((ASPxMemo)controle).Width = new Unit("100%");
                ((ASPxMemo)controle).Rows = int.Parse(linhas);
                ((ASPxMemo)controle).ToolTip = descricaoCampo;
                if (obrigatorio)
                {
                    ((ASPxMemo)controle).ValidationSettings.RequiredField.IsRequired = true;
                    ((ASPxMemo)controle).ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                    ((ASPxMemo)controle).ValidationSettings.ErrorFrameStyle.Paddings.Padding = new Unit("0px");
                }
                ((ASPxMemo)controle).ValidationSettings.ValidationGroup = "FormularioCDIS";
                ((ASPxMemo)controle).ReadOnly = somenteLeitura;

                ((ASPxMemo)controle).ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
                ((ASPxMemo)controle).ReadOnlyStyle.ForeColor = Color.Black;
                ((ASPxMemo)controle).DisabledStyle.BackColor = Color.FromName("#EBEBEB");
                ((ASPxMemo)controle).DisabledStyle.ForeColor = Color.Black;
                ((ASPxMemo)controle).Text = padrao;
            }

            else if (formato == "1" || formato == "2")
            {
                int alturaMinima = 350;
                controle = new ASPxHtmlEditor();
                //((ASPxHtmlEditor)controle).Font.Name = "Verdana";
                //((ASPxHtmlEditor)controle).Font.Size = new FontUnit("8pt");
                ((ASPxHtmlEditor)controle).ClientEnabled = !somenteLeitura;

                int larguraTela = int.Parse(parametrosEntrada["LarguraTela"].ToString()) - 100;

                ((ASPxHtmlEditor)controle).Width = new Unit(larguraTela.ToString() + "px");
                ((ASPxHtmlEditor)controle).Toolbars.CreateDefaultToolbars();
                ((ASPxHtmlEditor)controle).Toolbars[0].Items.RemoveAt(15);
                ((ASPxHtmlEditor)controle).Settings.AllowHtmlView = false;
                ((ASPxHtmlEditor)controle).Settings.AllowPreview = false;
                ((ASPxHtmlEditor)controle).Height = alturaMinima;
                ((ASPxHtmlEditor)controle).CssFilePath = cssFilePath;
                ((ASPxHtmlEditor)controle).CssPostfix = cssPostFix;
                ((ASPxHtmlEditor)controle).Html = padrao;
                ((ASPxHtmlEditor)controle).ToolTip = descricaoCampo;
                ((ASPxHtmlEditor)controle).ClientSideEvents.Init = @"
                function(s, e) 
                {
                     var str = s.GetHtml();
                     str = str.replace('–', '&ndash;').replace('—', '&mdash;');
                     s.SetHtml(str);
                }";
                ((ASPxHtmlEditor)controle).ClientSideEvents.BeforePaste = @"function(s, e) {
      //debugger
      if(e.commandName == ""pastehtml"" || e.commandName == ""pastehtmlsourceformatting"")
      {
                    var html = e.html;
                    if (html.toString().search(""<img "") >= 0)
                    {
                        if (html.toString().search(""px;"") >= 0)
                        {
                            var tratamento = html.toString().split(';');
                            var larguraTratada;
                            var alturaTratada;
                            for (i = 0; i < tratamento.length; i++)
                            {
                                if (tratamento[i].toString().search(""width"") > 0)
                                {
                                    larguraTratada = tratamento[i].toString().split(':')[1];
                                }
                                if (tratamento[i].toString().search(""height"") > 0)
                                {
                                    alturaTratada = tratamento[i].toString().split(':')[1];
                                }
                            }
                            if (parseInt(larguraTratada.replace('px', '')) > 600)
                            {
                                html = html.toString().replace(larguraTratada, '600px');
                                html = html.toString().replace(alturaTratada, '375px');
                            }
                            e.html = html;
                        }
                        else if (html.toString().search('px;') == -1)
                        {
                            var tratamento = html.toString().split('>')[0];
                            var stringAInserir = '  style=""width: 600px; height: 375px"" ';
                            e.html = tratamento + stringAInserir + ""/>"";
                        }
                    }
                }
            }";


                if (somenteLeitura)
                    ((ASPxHtmlEditor)controle).ClientSideEvents.Init = "function(s,e){s.SetEnabled(false);}";
            }
            return controle;
        }

        private Control renderizaCampoLNP(string textoLink)
        {
            Control controle = null;

            controle = new ASPxButton();
            ((ASPxButton)controle).Text = textoLink;

            ((ASPxButton)controle).ClientSideEvents.Click = "function(s,e){callbackEditarCronograma.PerformCallback();}";

            return controle;
        }

        private Control renderizaCampoNUM(bool obrigatorio, string[] aDefinicaoCampo, string larguraCampo, string nomeCampo, string descricaoCampo)
        {
            string minimo = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            string maximo = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
            string precisao = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
            string formato = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();
            string padrao = aDefinicaoCampo.Length > 5 ? aDefinicaoCampo[5].Substring(aDefinicaoCampo[5].IndexOf(":") + 1).Trim() : "";

            if (minimo == "")
                minimo = "-99999999999";
            if (maximo == "")
                maximo = "99999999999";

            if (precisao == "")
                precisao = "0";

            if (formato == "M")
                formato = "C" + precisao;
            else if (formato == "P")
                formato = "";
            else
                formato = "N" + precisao;

            Unit larguraTxt = new Unit("170px");
            double tamanhoParam = nomeCampo.Length;

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {
                SizeF size = graphics.MeasureString(nomeCampo, new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                tamanhoParam = size.Width + 23;
            }

            larguraTxt = (tamanhoParam) < larguraTxt.Value ? larguraTxt : new Unit((tamanhoParam).ToString());

            if (larguraTxt.Value + widthLinhaAtual > (width.Value - 30))
            {
                mudaLinha = true;
                widthLinhaAtual = larguraTxt.Value + 15;
            }
            else
            {
                mudaLinha = false;
                widthLinhaAtual += larguraTxt.Value + 15;
            }

            ASPxSpinEdit controle = new ASPxSpinEdit();
            //controle.Font.Name = "Verdana";
            //controle.Font.Size = new FontUnit("8pt");
            controle.CssFilePath = cssFilePath;
            controle.CssPostfix = cssPostFix;
            controle.Width = larguraTxt;
            controle.Width = new Unit("100%");
            controle.AllowMouseWheel = false;
            controle.MinValue = Int64.Parse(minimo);
            controle.MaxValue = Int64.Parse(maximo);
            controle.DecimalPlaces = int.Parse(precisao);
            controle.DisplayFormatString = formato;
            controle.SpinButtons.ShowIncrementButtons = false;
            controle.ToolTip = descricaoCampo;

            if (precisao == "0")
                controle.NumberType = SpinEditNumberType.Integer;
            else
                controle.NumberType = SpinEditNumberType.Float;
            controle.Increment = 0;

            controle.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;

            if (obrigatorio)
            {
                ((ASPxSpinEdit)controle).ValidationSettings.RequiredField.IsRequired = true;
                ((ASPxSpinEdit)controle).ValidationSettings.ErrorFrameStyle.Paddings.Padding = new Unit("0px");
            }
            controle.ReadOnly = somenteLeitura;

            controle.ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
            controle.ReadOnlyStyle.ForeColor = Color.Black;
            controle.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
            controle.DisabledStyle.ForeColor = Color.Black;

            ((ASPxSpinEdit)controle).ValidationSettings.ValidationGroup = "FormularioCDIS";
            ((ASPxSpinEdit)controle).ValidationSettings.Display = Display.Dynamic;
            ((ASPxSpinEdit)controle).Value = padrao;

            return controle;
        }

        private Control renderizaCampoLST(bool obrigatorio, string NomeCampoLST, string[] aDefinicaoCampo, string larguraCampo, string nomeCampo, string descricaoCampo)
        {
            if (aDefinicaoCampo[0].Trim() == "")
            {
                throw new Exception(string.Format("{1} ({0}) {2}.", NomeCampoLST, Resources.OFormulárioPossuiCampoDeListaDeOpções, Resources.queNãoTeveOConteúdoDefinidoDuranteSuaCriaç));
            }
            string opcoes = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            opcoes = opcoes.Replace("\r\n", "\r");
            string[] aOpcoes = opcoes.Split('\r');
            string formato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
            string padrao = aDefinicaoCampo.Length > 3 ? aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim() : "";

            Control controle = null;
            if (formato == "0")// comboBox
            {
                string tamanho = "";

                if (aDefinicaoCampo.Length > 2 && aDefinicaoCampo[2] != "")
                    tamanho = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();

                Unit larguraTxt = new Unit(larguraCampo);

                if (tamanho != "" && int.Parse(tamanho) > 0)
                {
                    larguraTxt = (int.Parse(tamanho) * 7 + 20) > larguraTxt.Value ? larguraTxt : new Unit((int.Parse(tamanho) * 7 + 30).ToString());
                }

                double tamanhoParam = nomeCampo.Length;

                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                {
                    SizeF size = graphics.MeasureString(nomeCampo, new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                    tamanhoParam = size.Width + 23;
                }

                larguraTxt = (tamanhoParam) < larguraTxt.Value ? larguraTxt : new Unit((tamanhoParam).ToString());

                if (larguraTxt.Value + widthLinhaAtual > (width.Value - 30))
                {
                    mudaLinha = true;
                    widthLinhaAtual = larguraTxt.Value + 15;
                }
                else
                {
                    mudaLinha = false;
                    widthLinhaAtual += larguraTxt.Value + 15;
                }

                controle = new ASPxComboBox();
                //((ASPxComboBox)controle).Font.Name = "Verdana";
                //((ASPxComboBox)controle).Font.Size = new FontUnit("8pt");
                ((ASPxComboBox)controle).CssFilePath = cssFilePath;
                ((ASPxComboBox)controle).CssPostfix = cssPostFix;
                ((ASPxComboBox)controle).Width = larguraTxt;
                ((ASPxComboBox)controle).Width = new Unit("100%");
                ((ASPxComboBox)controle).ToolTip = descricaoCampo;
                //((ASPxComboBox)controle).EnableCallbackMode = true;
                //((ASPxComboBox)controle).CallbackPageSize = nCallbackPageSize;
                //((ASPxComboBox)controle).DropDownStyle = DropDownStyle.DropDown;

                if (obrigatorio)
                {
                    ((ASPxComboBox)controle).ValidationSettings.RequiredField.IsRequired = true;
                    ((ASPxComboBox)controle).ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                    ((ASPxComboBox)controle).ValidationSettings.ErrorFrameStyle.Paddings.Padding = new Unit("0px");
                }
                else
                {
                    ((ASPxComboBox)controle).Items.Add(Resources.NãoSeAplica, "");
                }

                foreach (string item in aOpcoes)
                {
                    ((ASPxComboBox)controle).Items.Add(item, item);
                }
                ((ASPxComboBox)controle).ValidationSettings.ValidationGroup = "FormularioCDIS";
                ((ASPxComboBox)controle).ReadOnly = somenteLeitura;

                ((ASPxComboBox)controle).ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
                ((ASPxComboBox)controle).ReadOnlyStyle.ForeColor = Color.Black;
                ((ASPxComboBox)controle).DisabledStyle.BackColor = Color.FromName("#EBEBEB");
                ((ASPxComboBox)controle).DisabledStyle.ForeColor = Color.Black;
                ((ASPxComboBox)controle).Value = padrao;

                ((ASPxComboBox)controle).ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            }
            else if (formato == "1")// radio button
            {
                mudaLinha = true;
                Unit larguraTxt = new Unit(larguraCampo);

                double tamanhoParam = nomeCampo.Length;

                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                {
                    SizeF size = graphics.MeasureString(nomeCampo, new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                    tamanhoParam = size.Width + 23;
                }

                larguraTxt = (tamanhoParam) < larguraTxt.Value ? larguraTxt : new Unit((tamanhoParam).ToString());

                widthLinhaAtual = larguraTxt.Value + 15;

                if (indicaFormAssinado)
                {
                    controle = new ASPxTextBox();
                    //((ASPxTextBox)controle).Font.Name = "Verdana";
                    //((ASPxTextBox)controle).Font.Size = new FontUnit("8pt");
                    ((ASPxTextBox)controle).CssFilePath = cssFilePath;
                    ((ASPxTextBox)controle).CssPostfix = cssPostFix;
                    ((ASPxTextBox)controle).Width = larguraTxt;
                    ((ASPxTextBox)controle).Width = new Unit("100%");

                    ((ASPxTextBox)controle).ToolTip = descricaoCampo;
                    ((ASPxTextBox)controle).ReadOnly = true;

                    ((ASPxTextBox)controle).ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
                    ((ASPxTextBox)controle).ReadOnlyStyle.ForeColor = Color.Black;
                    ((ASPxTextBox)controle).DisabledStyle.BackColor = Color.FromName("#EBEBEB");
                    ((ASPxTextBox)controle).DisabledStyle.ForeColor = Color.Black;
                }
                else
                {
                    double larguraDoRadioButtonItem = 0;
                    controle = new ASPxRadioButtonList();
                    ((ASPxRadioButtonList)controle).RepeatDirection = RepeatDirection.Horizontal;
                    //((ASPxRadioButtonList)controle).Font.Name = "Verdana";
                    //((ASPxRadioButtonList)controle).Font.Size = new FontUnit("8pt");
                    ((ASPxRadioButtonList)controle).CssFilePath = cssFilePath;
                    ((ASPxRadioButtonList)controle).CssPostfix = cssPostFix;
                    ((ASPxRadioButtonList)controle).Border.BorderStyle = BorderStyle.None;
                    ((ASPxRadioButtonList)controle).ToolTip = descricaoCampo;
                    if (obrigatorio)
                    {
                        ((ASPxRadioButtonList)controle).ValidationSettings.RequiredField.IsRequired = true;
                        ((ASPxRadioButtonList)controle).ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                        ((ASPxRadioButtonList)controle).ValidationSettings.ErrorFrameStyle.Paddings.Padding = new Unit("0px");
                    }
                    else
                    {

                        ((ASPxRadioButtonList)controle).Items.Add(Resources.NãoSeAplica, "");
                    }

                    foreach (string item in aOpcoes)
                    {
                        using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                        {

                            SizeF size = graphics.MeasureString(item.Trim(), new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                            larguraDoRadioButtonItem += size.Width + 23;

                        }

                        ListEditItem li = new ListEditItem(item.Trim(), item.Trim());
                        ((ASPxRadioButtonList)controle).Items.Add(li);
                    }

                    ((ASPxRadioButtonList)controle).TextWrap = false;
                    ((ASPxRadioButtonList)controle).ValidationSettings.ValidationGroup = "FormularioCDIS";
                    ((ASPxRadioButtonList)controle).ReadOnly = somenteLeitura;
                    ((ASPxRadioButtonList)controle).Value = padrao;
                    ((ASPxRadioButtonList)controle).Width = new Unit(((int)larguraDoRadioButtonItem).ToString() + "px");
                }
            }
            else if (formato == "2")// check box
            {
                mudaLinha = true;
                Unit larguraTxt = new Unit(larguraCampo);

                double tamanhoParam = nomeCampo.Length;

                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                {
                    SizeF size = graphics.MeasureString(nomeCampo, new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                    tamanhoParam = size.Width + 23;
                }

                larguraTxt = (tamanhoParam) < larguraTxt.Value ? larguraTxt : new Unit((tamanhoParam).ToString());

                widthLinhaAtual = larguraTxt.Value + 15;

                if (indicaFormAssinado)
                {
                    controle = new ASPxTextBox();
                    //((ASPxTextBox)controle).Font.Name = "Verdana";
                    //((ASPxTextBox)controle).Font.Size = new FontUnit("8pt");
                    ((ASPxTextBox)controle).CssFilePath = cssFilePath;
                    ((ASPxTextBox)controle).CssPostfix = cssPostFix;
                    ((ASPxTextBox)controle).Width = larguraTxt;
                    ((ASPxTextBox)controle).Width = new Unit("100%");
                    ((ASPxTextBox)controle).ToolTip = descricaoCampo;

                    ((ASPxTextBox)controle).ReadOnly = true;

                    ((ASPxTextBox)controle).ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
                    ((ASPxTextBox)controle).ReadOnlyStyle.ForeColor = Color.Black;
                    ((ASPxTextBox)controle).DisabledStyle.BackColor = Color.FromName("#EBEBEB");
                    ((ASPxTextBox)controle).DisabledStyle.ForeColor = Color.Black;
                }
                else
                {
                    controle = new CheckBoxList();
                    ((CheckBoxList)controle).RepeatDirection = RepeatDirection.Horizontal;
                    //((CheckBoxList)controle).Font.Name = "Verdana";
                    //((CheckBoxList)controle).Font.Size = new FontUnit("8pt");
                    ((CheckBoxList)controle).ToolTip = descricaoCampo;
                    int count = 0;
                    foreach (string item in aOpcoes)
                    {
                        ((CheckBoxList)controle).Items.Add(item);
                        ((CheckBoxList)controle).Items[count].Enabled = !somenteLeitura;
                        if (item == padrao)
                            ((CheckBoxList)controle).Items[count].Selected = true;
                        count++;
                    }
                }
            }
            else
            {
                mudaLinha = true;
                Unit larguraTxt = new Unit(larguraCampo);

                double tamanhoParam = nomeCampo.Length;

                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                {
                    SizeF size = graphics.MeasureString(nomeCampo, new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                    tamanhoParam = size.Width + 23;
                }

                larguraTxt = (tamanhoParam) < larguraTxt.Value ? larguraTxt : new Unit((tamanhoParam).ToString());

                widthLinhaAtual = larguraTxt.Value + 15;
                controle = new ASPxTextBox();
                //((ASPxTextBox)controle).Font.Name = "Verdana";
                //((ASPxTextBox)controle).Font.Size = new FontUnit("8pt");
                (controle as ASPxTextBox).ReadOnly = true;
                //(controle as ASPxTextBox).Enabled = false;
                (controle as ASPxTextBox).ClientEnabled = false;
                (controle as ASPxTextBox).BackColor = corReadOnly;
                (controle as ASPxTextBox).Width = new Unit(larguraCampo);
                (controle as ASPxTextBox).Width = new Unit("100%");
                ((ASPxTextBox)controle).ToolTip = descricaoCampo;
                if (obrigatorio)
                {
                    ((ASPxTextBox)controle).ValidationSettings.RequiredField.IsRequired = true;
                    ((ASPxTextBox)controle).ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                    ((ASPxTextBox)controle).ValidationSettings.ErrorFrameStyle.Paddings.Padding = new Unit("0px");
                }
                ((ASPxTextBox)controle).ValidationSettings.ValidationGroup = "FormularioCDIS";
                ((ASPxTextBox)controle).ReadOnly = somenteLeitura;

                ((ASPxTextBox)controle).ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
                ((ASPxTextBox)controle).ReadOnlyStyle.ForeColor = Color.Black;
                ((ASPxTextBox)controle).DisabledStyle.BackColor = Color.FromName("#EBEBEB");
                ((ASPxTextBox)controle).DisabledStyle.ForeColor = Color.Black;
            }

            return controle;
        }

        private Control renderizaCampoDAT(bool obrigatorio, string[] aDefinicaoCampo, string larguraCampo, string nomeCampo, string descricaoCampo)
        {
            string incluirHora = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            string valorInicial = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
            string padrao = aDefinicaoCampo.SingleOrDefault(dc => dc.StartsWith("Pdr:", StringComparison.InvariantCultureIgnoreCase));
            padrao = padrao.Substring(padrao.IndexOf(':') + 1).Trim();

            Unit larguraTxt = new Unit("170px");

            double tamanhoParam = nomeCampo.Length;

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {
                SizeF size = graphics.MeasureString(nomeCampo, new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                tamanhoParam = size.Width + 23;
            }

            larguraTxt = (tamanhoParam) < larguraTxt.Value ? larguraTxt : new Unit((tamanhoParam).ToString());

            if (larguraTxt.Value + widthLinhaAtual > (width.Value - 30))
            {
                mudaLinha = true;
                widthLinhaAtual = larguraTxt.Value + 15;
            }
            else
            {
                mudaLinha = false;
                widthLinhaAtual += larguraTxt.Value + 15;
            }

            ASPxDateEdit controle = new ASPxDateEdit();
            //controle.Font.Name = "Verdana";
            //controle.Font.Size = new FontUnit("8pt");
            controle.CssFilePath = cssFilePath;
            controle.CssPostfix = cssPostFix;
            controle.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
            controle.ToolTip = descricaoCampo;

            controle.Width = larguraTxt;
            controle.Width = new Unit("100%");
            if (obrigatorio)
            {
                controle.ValidationSettings.RequiredField.IsRequired = true;
                controle.ValidationSettings.ErrorFrameStyle.Paddings.Padding = new Unit("0px");
            }

            if (incluirHora == "S")
            {
                //todo: 2. Não está permitindo informar os segundos;
                controle.EditFormat = EditFormat.Custom;
                controle.EditFormatString = "dd/MM/yyyy HH:mm:ss";
                controle.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";
            }
            else
            {
                controle.EditFormat = EditFormat.Date;
                controle.DisplayFormatString = "dd/MM/yyyy";
            }
            //todo: 1. Não está sugerindo a data/hora atual como valor inicial;
            DateTime dataPadrao;
            if(!string.IsNullOrWhiteSpace(padrao) && DateTime.TryParse(padrao, out dataPadrao))
                controle.Date = dataPadrao;
            else if (valorInicial == "A")
                controle.Date = DateTime.Now;
            controle.ValidationSettings.ValidationGroup = "FormularioCDIS";
            controle.ReadOnly = somenteLeitura;

            controle.ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
            controle.ReadOnlyStyle.ForeColor = Color.Black;
            controle.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
            controle.DisabledStyle.ForeColor = Color.Black;

            return controle;
        }

        private Control renderizaCampoCPD(int codigoCampoModeloFormulario, string[] aDefinicaoCampo, string larguraCampo, string nomeCampo, string descricaoCampo)
        {
            string linhas = "1";
            if (aDefinicaoCampo.Length > 1 && aDefinicaoCampo[1] != "")
                linhas = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();

            Control controle = null;
            char IndicaCampoDinamico = 'N';

            if (linhas != "1")
            {
                mudaLinha = true;
                Unit larguraTxt = new Unit(larguraCampo);

                double tamanhoParam = nomeCampo.Length;

                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                {
                    SizeF size = graphics.MeasureString(nomeCampo, new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                    tamanhoParam = size.Width + 23;
                }

                larguraTxt = (tamanhoParam) < larguraTxt.Value ? larguraTxt : new Unit((tamanhoParam).ToString());

                widthLinhaAtual = larguraTxt.Value + 15;
                controle = new ASPxMemo();
                //((ASPxMemo)controle).Font.Name = "Verdana";
                //((ASPxMemo)controle).Font.Size = new FontUnit("8pt");
                ((ASPxMemo)controle).CssFilePath = cssFilePath;
                ((ASPxMemo)controle).CssPostfix = cssPostFix;
                ((ASPxMemo)controle).Width = new Unit(larguraCampo);
                ((ASPxMemo)controle).Width = new Unit("100%");
                ((ASPxMemo)controle).Rows = int.Parse(linhas);
                ((ASPxMemo)controle).ReadOnly = true;
                ((ASPxMemo)controle).ClientEnabled = false;
                ((ASPxMemo)controle).Text = getConteudoCampoPreDefinido(codigoCampoModeloFormulario, -1, ref IndicaCampoDinamico, indicaFormAssinado, false);
                ((ASPxMemo)controle).ToolTip = descricaoCampo;

                ((ASPxMemo)controle).ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
                ((ASPxMemo)controle).ReadOnlyStyle.ForeColor = Color.Black;
                ((ASPxMemo)controle).DisabledStyle.BackColor = Color.FromName("#EBEBEB");
                ((ASPxMemo)controle).DisabledStyle.ForeColor = Color.Black;
            }
            else
            {
                string tamanho = "";

                if (aDefinicaoCampo.Length > 2 && aDefinicaoCampo[2] != "")
                    tamanho = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();

                Unit larguraTxt = new Unit(larguraCampo);

                if (tamanho != "" && int.Parse(tamanho) > 0)
                {
                    larguraTxt = (int.Parse(tamanho) * 7 + 20) > larguraTxt.Value ? larguraTxt : new Unit((int.Parse(tamanho) * 7 + 30).ToString());
                }

                double tamanhoParam = nomeCampo.Length;

                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                {
                    SizeF size = graphics.MeasureString(nomeCampo, new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                    tamanhoParam = size.Width + 23;
                }

                larguraTxt = (tamanhoParam) < larguraTxt.Value ? larguraTxt : new Unit((tamanhoParam).ToString());

                if (larguraTxt.Value + widthLinhaAtual > (width.Value - 30))
                {
                    mudaLinha = true;
                    widthLinhaAtual = larguraTxt.Value + 15;
                }
                else
                {
                    mudaLinha = false;
                    widthLinhaAtual += larguraTxt.Value + 15;
                }

                controle = new ASPxTextBox();
                //((ASPxTextBox)controle).Font.Name = "Verdana";
                //((ASPxTextBox)controle).Font.Size = new FontUnit("8pt");
                ((ASPxTextBox)controle).CssFilePath = cssFilePath;
                ((ASPxTextBox)controle).CssPostfix = cssPostFix;
                ((ASPxTextBox)controle).Width = larguraTxt;
                ((ASPxTextBox)controle).Width = new Unit("100%");
                ((ASPxTextBox)controle).ReadOnly = true;
                ((ASPxTextBox)controle).ClientEnabled = false;
                ((ASPxTextBox)controle).Text = getConteudoCampoPreDefinido(codigoCampoModeloFormulario, -1, ref IndicaCampoDinamico, indicaFormAssinado, false);
                ((ASPxTextBox)controle).ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                ((ASPxTextBox)controle).ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
                ((ASPxTextBox)controle).ReadOnlyStyle.ForeColor = Color.Black;
                ((ASPxTextBox)controle).DisabledStyle.BackColor = Color.FromName("#EBEBEB");
                ((ASPxTextBox)controle).DisabledStyle.ForeColor = Color.Black;
                ((ASPxTextBox)controle).ToolTip = descricaoCampo;
            }

            //controle.Text = getConteudoCampoPreDefinido(codigoCampoModeloFormulario, -1, ref IndicaCampoDinamico);
            //controle.ReadOnly = somenteLeitura; 
            return controle;
        }

        private Control renderizaCampoLOO(bool obrigatorio, string NomeCampoLoo, string[] aDefinicaoCampo, string larguraCampo, string nomeCampo, string descricaoCampo)
        {
            string codigoListaPre = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            if (codigoListaPre == "")
            {
                throw new Exception(string.Format("{1} ({0}) {2}.", NomeCampoLoo, Resources.OFormulárioPossuiCampoLookup, Resources.queNãoFoiEspecificadoDuranteSuaCriação));
            }

            string tamanho = "";

            if (aDefinicaoCampo.Length > 1 && aDefinicaoCampo[1] != "")
                tamanho = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();

            Unit larguraTxt = new Unit(larguraCampo);

            if (tamanho != "" && int.Parse(tamanho) > 0)
            {
                larguraTxt = (int.Parse(tamanho) * 7 + 20) > larguraTxt.Value ? larguraTxt : new Unit((int.Parse(tamanho) * 7 + 30).ToString());
            }

            double tamanhoParam = nomeCampo.Length;

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {
                SizeF size = graphics.MeasureString(nomeCampo, new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                tamanhoParam = size.Width + 23;
            }

            larguraTxt = (tamanhoParam) < larguraTxt.Value ? larguraTxt : new Unit((tamanhoParam).ToString());

            if (larguraTxt.Value + widthLinhaAtual > (width.Value - 30))
            {
                mudaLinha = true;
                widthLinhaAtual = larguraTxt.Value + 15;
            }
            else
            {
                mudaLinha = false;
                widthLinhaAtual += larguraTxt.Value + 15;
            }

            // LOV - ACG em 15/02 - Opção para selecionar a partir de uma lista de valores em uma nova janela
            bool mostrarComoLov = false;
            if (aDefinicaoCampo.Length > 2 && aDefinicaoCampo[2] != "")
                mostrarComoLov = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim().ToUpper() == "S";

            var padrao = aDefinicaoCampo.SingleOrDefault(dc => (dc ?? string.Empty).Trim().StartsWith("Pdr", StringComparison.InvariantCultureIgnoreCase));

            ASPxComboBox controle = new ASPxComboBox();
            //controle.Font.Name = "Verdana";
            //controle.Font.Size = new FontUnit("8pt");
            controle.CssFilePath = cssFilePath;
            controle.CssPostfix = cssPostFix;
            //controle.EnableCallbackMode = true;
            controle.CallbackPageSize = nCallbackPageSize;
            //controle.DropDownStyle = DropDownStyle.DropDownList;
            controle.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            controle.ToolTip = descricaoCampo;

            controle.Width = larguraTxt;
            controle.Width = new Unit("100%");
            if (obrigatorio)
            {
                controle.ValidationSettings.RequiredField.IsRequired = true;
                controle.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                controle.ValidationSettings.ErrorFrameStyle.Paddings.Padding = new Unit("0px");
            }
            else
            {
                (controle).Items.Add(Resources.NãoSeAplica, "");
            }
            controle.ReadOnly = somenteLeitura;

            controle.ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
            controle.ReadOnlyStyle.ForeColor = Color.Black;
            controle.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
            controle.DisabledStyle.ForeColor = Color.Black;

            // ACG - 15/02/2014 - se é para mostrar como LOV
            if (mostrarComoLov)
            {
                controle.IncrementalFilteringMode = IncrementalFilteringMode.None;
                controle.DropDownButton.Visible = false;
                controle.Buttons.Add("...");
                controle.ClientSideEvents.ButtonClick =
                @"function(s, e) 
                  {
                     mostrarLov(s, e, " + codigoListaPre + @");
                  }";
            }
            // se É para mostrar como combo normal
            else
            {
                DataTable dt = getTableOpcoesListaPre(int.Parse(codigoListaPre), "", -1);
                foreach (DataRow dr in dt.Rows)
                {
                    controle.Items.Add(dr["descricao"].ToString(), dr["codigo"]);
                }
            }
            if (!string.IsNullOrWhiteSpace(padrao))
            {
                long value;
                if (long.TryParse(padrao.Substring(padrao.IndexOf(':') + 1), out value))
                {
                    controle.Value = value.ToString();
                }
            }
            controle.ValidationSettings.ValidationGroup = "FormularioCDIS";

            return controle;
        }

        private Control renderizaCampoCAL(int codigoCampoCalculado, string NomeCampoCal, string[] aDefinicaoCampo, string larguraCampo, string nomeCampo, string descricaoCampo)
        {
            string precisao = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            string formato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();

            Unit larguraTxt = new Unit("170px");

            double tamanhoParam = nomeCampo.Length;

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {
                SizeF size = graphics.MeasureString(nomeCampo, new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Point));
                tamanhoParam = size.Width + 23;
            }

            larguraTxt = (tamanhoParam) < larguraTxt.Value ? larguraTxt : new Unit((tamanhoParam).ToString());

            if (larguraTxt.Value + widthLinhaAtual > (width.Value - 30))
            {
                mudaLinha = true;
                widthLinhaAtual = larguraTxt.Value + 15;
            }
            else
            {
                mudaLinha = false;
                widthLinhaAtual += larguraTxt.Value + 15;
            }

            string minimo = "-99999999999";
            string maximo = "99999999999";
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

            if (formato == "M")
                formato = "$ ";
            else if (formato == "P")
                formato = "% ";
            else
                formato = "";
            string formula = aDefinicaoCampo[4].Substring(aDefinicaoCampo[4].IndexOf(":") + 1).Trim();
            if (formula == "")
            {
                throw new Exception(string.Format("{1} ({0}) {2}.", NomeCampoCal, Resources.OFormulárioPossuiCampoCalculado, Resources.queNãoFoiEspecificadoDuranteSuaCriação));
            }
            ASPxTextBox controle = new ASPxTextBox();
            //controle.Font.Name = "Verdana";
            //controle.Font.Size = new FontUnit("8pt");
            controle.CssFilePath = cssFilePath;
            controle.CssPostfix = cssPostFix;
            //controle.Width = new Unit(larguraCampo);
            controle.Width = larguraTxt;
            controle.Width = new Unit("100%");
            controle.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.All;
            controle.MaskSettings.Mask = string.Format("{0}<{1}..{2}g>{3}", formato, minimo, maximo, precisao);
            controle.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
            controle.ValidationSettings.Display = Display.Dynamic;
            // campo calculado é sempre somente leitura
            //controle.ClientEnabled = false;
            controle.ReadOnly = true;
            controle.ToolTip = descricaoCampo;

            controle.ReadOnlyStyle.BackColor = Color.FromName("#EBEBEB");
            controle.ReadOnlyStyle.ForeColor = Color.Black;
            controle.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
            controle.DisabledStyle.ForeColor = Color.Black;

            //string resultado = "";
            // se existe um formulário, busca o resultado do campo
            //            if (codigoFormularioMaster > 0)
            //                resultado = getResultadoCampoCalculado(codigoFormularioMaster, codigoCampoCalculado);

            return controle;
        }

        private Control renderizaCampoREF(int codigoCampo, bool obrigatorio, string[] aDefinicaoCampo, string larguraCampo, int indexAba, string idCampoREF)
        {
            Control controle = null;
            string codigoModeloFormularioRef = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            string codigoCampoRef = aDefinicaoCampo[1].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            bool somenteLeituraRef = aDefinicaoCampo[2].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim() == "S";
            bool incluirRotuloCampoExterno = aDefinicaoCampo[3].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim() == "S";
            bool incluirRotuloCampoInterno = aDefinicaoCampo[4].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim() == "S";

            DataSet ds = getInformacoesCampo(int.Parse(codigoCampoRef));
            if (ds != null && ds.Tables[0] != null)
            {
                Control controleInternoRef = null;
                DataRow drCampo = ds.Tables[0].Rows[0];

                // se o campo referenciado é um subformulario, teremos que inserir os formularios originais no formulario master padrão
                if (drCampo["codigoTipoCampo"].ToString() == "SUB")
                {
                    string DefinicaoCampoRef = drCampo["definicaoCampo"].ToString();
                    string[] aDefinicaoCampoRef = DefinicaoCampoRef.Split(DelimitadorPropriedadeCampo);
                    codigoModeloFormularioRef = aDefinicaoCampoRef[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();

                    // busca os registros do subformulario referenciado
                    getValorCampoREF_formularioOrigem(null, codigoCampo, int.Parse(codigoCampoRef), int.Parse(codigoModeloFormularioRef));
                }
                else
                {

                    controle = drCampo["CodigoTipoCampo"].ToString() == "LNP" ? renderizaCampoLinkCrono(drCampo) : renderizaCampo(drCampo, indexAba, incluirRotuloCampoInterno, idCampoREF, somenteLeituraRef, out controleInternoRef);

                    // busca o conteúdo do campo no formulário padrão
                    ds = getConteudoFormulario(codigoCampo, 0);

                    bool buscaValorFormularioReferencia = true;
                    // se o registro existir, vamos ver se tem valor
                    if (ds != null)
                    {
                        buscaValorFormularioReferencia = true;
                    }

                    // se é para buscar os valores no formulário de referência
                    if (buscaValorFormularioReferencia)
                    {
                        getValorCampoREF_formularioOrigem(controleInternoRef, codigoCampo, int.Parse(codigoCampoRef), int.Parse(codigoModeloFormularioRef));
                    }
                }


                // populaCampoFormularioMaster(ds.Tables[0].Rows[0], controleInternoRef);
            }

            return controle;
        }

        private object getValorCampoREF_formularioOrigem(Control controleInternoRef, int codigoCampo, int codigoCampoRef, int codigoModeloFormularioRef)
        {
            // se existe o projeto, busca a última ocorrencia do campo
            if (codigoProjeto > 0)
            {
                // busca o conteúdo do campo no formulário padrão
                DataSet ds = getConteudoFormulario(controleInternoRef != null ? codigoCampoRef : 0, codigoModeloFormularioRef);

                // se for subformulario, duplica os registro referenciados
                if (controleInternoRef == null)
                {

                }
                if (ds != null)
                    if (ds.Tables[0].Rows[0]["codigoTipoCampo"].ToString() != "SUB")
                        populaCampoFormularioMaster(ds.Tables[0].Rows[0], controleInternoRef);
                    else
                    {

                    }

            }
            return Resources.TemQueFazerUmaChamadaNaProcedureParaLerOCa;
        }

        #endregion

        #region Campos Calculados

        // evento JAVASCRIPT que será executado sempre que algum campo que faz parte de alguma fórmula for alterado
        /*private void procuraCamposCalculados()
        {
            for (int i = 0; i < camposFormulario.Count; i++)
            {
                object[] Controles = camposFormulario[i];
                if (Controles[1].ToString() == "CAL")
                {
                    associaEventoAlteracao(Controles[2].ToString());
                }
            }
        }*/

        // evento JAVASCRIPT que será executado sempre que algum campo que faz parte de alguma fórmula for alterado
        private void associaEventoAlteracao()
        {
            // ACG 30/03/2017: BUG 652 
            // O script na variável "scriptInicializaCampoCalculado" deverá ser executado assim que o formulário estiver renderizado. 
            // O objetivo é atualizar os campos calculados com os valores padrões dos campos que os compôem.
            string scriptInicializaCampoCalculado = "";

            for (int i = 0; i < camposFormulario.Count; i++)
            {
                object[] Controles = camposFormulario[i];
                string[] aDefinicaoCampo = (string[])Controles[2];

                string funcoes = "conteudoCampoAlterado();  ";

                if (formularioPossuiCampoCalculado)
                {
                    // Por enquanto, apenas campos numéricos podem participar de Fórmulas
                    if (Controles[1].ToString() == "NUM")
                    {
                        // procura os campos calculados na lista do formulário
                        foreach (object[] campo in camposFormulario)
                        {
                            // só interessa campos calculados... queremos a fórmula
                            if (campo[1].ToString() == "CAL")
                            {
                                string[] aDefinicao = (string[])campo[2];
                                string formulaCalculo = aDefinicao[4].Substring(aDefinicao[4].IndexOf(":") + 1).Trim();
                                string nomeControle = ((ASPxTextBox)campo[3]).ClientInstanceName;// ((ASPxEdit)Controles[3]).ClientInstanceName;
                                string precisao = aDefinicao[0].Split(':')[1].Trim();

                                string comando = string.Format("avaliarFormula('{0}','{1}','{2}');  ", nomeControle, formulaCalculo, precisao);

                                funcoes += comando;

                                // se a fórmula ainda não está no script de inicialização...
                                if (scriptInicializaCampoCalculado.IndexOf(comando) < 0)
                                    scriptInicializaCampoCalculado += comando;
                            }
                        }
                    }
                }
                string comandoEvento = "function(s, e) { " + funcoes + "}";

                if (Controles[1].ToString() == "LOO")
                {
                    (Controles[3] as ASPxComboBox).ClientSideEvents.SelectedIndexChanged = comandoEvento;
                }
                if (Controles[1].ToString() == "NUM")
                {
                    //(Controles[3] as ASPxTextBox).ClientSideEvents.TextChanged = comandoEvento;
                    (Controles[3] as ASPxSpinEdit).ClientSideEvents.NumberChanged = comandoEvento;
                }
                if (Controles[1].ToString() == "VAR")
                {
                    (Controles[3] as ASPxTextBox).ClientSideEvents.TextChanged = comandoEvento;
                }
                if (Controles[1].ToString() == "TXT")
                {
                    string formato = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                    if (formato == "0")
                        (Controles[3] as ASPxMemo).ClientSideEvents.TextChanged = comandoEvento;
                    else
                        (Controles[3] as ASPxHtmlEditor).ClientSideEvents.HtmlChanged = comandoEvento;
                }
            }

            if (scriptInicializaCampoCalculado != "")
            {
                scriptInicializaCampoCalculado = @"
<script type='text/javascript' language='javascript'>
   function InicializaCampoCalculado() 
   { 
         " + scriptInicializaCampoCalculado + @"
   }
   window.addEventListener('load', InicializaCampoCalculado );
</script>

";

                page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializaCamposCalculados", scriptInicializaCampoCalculado);
            }
        }

        #endregion

        #region Modelo formulário do tipo lista/Grid - Tipo 2

        private ASPxGridView renderizaGridPrincipal(bool isPostBack)
        {
            ASPxGridView gvGridPrincipal = new ASPxGridView();
            //gvGridPrincipal.Font.Name = "Verdana";
            //gvGridPrincipal.Font.Size = new FontUnit("8pt");
            gvGridPrincipal.Width = new Unit("99%");
            //gvGridPrincipal.Settings.ShowTitlePanel = true;
            gvGridPrincipal.ClientInstanceName = "gvGridPrincipal";
            gvGridPrincipal.ID = "gvGridPrincipal";
            gvGridPrincipal.CssFilePath = cssFilePath;
            gvGridPrincipal.CssPostfix = cssPostFix;
            gvGridPrincipal.AutoGenerateColumns = false;

            gvGridPrincipal.Settings.ShowGroupPanel = true;
            gvGridPrincipal.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;
            gvGridPrincipal.Settings.VerticalScrollBarMode = ScrollBarMode.Auto;
            //gvGridPrincipal.Settings.VerticalScrollableHeight = (int)(height.Value - 210);
            gvGridPrincipal.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

            //if (somenteLeitura)
            //    gvGridPrincipal.SettingsText.EmptyDataRow = "O formulário ainda não possui registro.";
            //else
            //gvGridPrincipal.SettingsText.EmptyDataRow = "O formulário ainda não possui registro. Clique <a href=\"#\" onclick=\"popUpFormularioEdicao(-1);\">aqui</a> para abrir a tela de cadastro. ";
            gvGridPrincipal.SettingsText.Title = modeloNomeFormulario;
            gvGridPrincipal.SettingsBehavior.AllowFocusedRow = true;

            //gvGridPrincipal.RowDeleting += gvGridPrincipal_RowDeleting;

            gvGridPrincipal.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gvGridPrincipal_CustomCallback);

            gvGridPrincipal.HtmlEditFormCreated += gvPrincipal_HtmlEditFormCreated;

            gvGridPrincipal.CustomButtonInitialize += gvPrincipal_CustomButtonInitialize;

            gvGridPrincipal.HtmlDataCellPrepared += GvGridPrincipal_HtmlDataCellPrepared;

            //Cria um dataTable com as colunas que devem ser inseridas na grid.
            DataTable dtDadosLista = getDTCamposFormulario();

            // Insere as colunas na grid
            ajustaColunasGridPrincipal(gvGridPrincipal, dtDadosLista);

            //gvGridPrincipal.DataSource = dtDadosLista;
            //gvGridPrincipal.DataBind();
            //gvGridPrincipal.Columns["CodigoFormulario"].Visible = false;
            gvGridPrincipal.ClientSideEvents.Init = @"function(s,e){
            var height = Math.max(0, document.documentElement.clientHeight) - 70;
            s.SetHeight(height);
}";
            gvGridPrincipal.ClientSideEvents.CustomButtonClick =
               @"function(s, e) 
                    {
                        e.processOnServer = false;
                        if  (e.buttonID == 'Incluir')
                           popUpFormularioEdicao(-1);
                        else if (e.buttonID == 'Excluir')
                        {
                           
                                var funcObj = { exclui: function(s, e){ var acao = 'Excluir;' + s.GetRowKey(s.GetFocusedRowIndex()); gvGridPrincipal.PerformCallback(acao); } }

                                window.top.mostraMensagem('" + Resources.ConfirmaAExclusãoDoRegistroSelecionado + @"', 'confirmacao', true, true, function(){funcObj['exclui'](s, e)});

                            
                        }
                        else if (e.buttonID == 'Visualizar')
                        {
                           gvGridPrincipal.GetRowValues(e.visibleIndex, 'CodigoFormulario', popUpFormularioLeitura);
                        }
                        else if (e.buttonID == 'Imprimir')
                        {
                           gvGridPrincipal.GetRowValues(e.visibleIndex, 'CodigoFormulario', popUpFormularioImpressao);
                        }
                        else 
                           gvGridPrincipal.GetRowValues(e.visibleIndex, 'CodigoFormulario', popUpFormularioEdicao);
                    }";


            // popula a grid
            populaGridPrincipal(gvGridPrincipal, dtDadosLista, isPostBack);
            return gvGridPrincipal;
        }

        private void GvGridPrincipal_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            bool ehCampoDataNumOuCal = e.DataColumn.FieldName.Contains("DAT") || e.DataColumn.FieldName.Contains("NUM") || e.DataColumn.FieldName.Contains("CAL");
            if (!ehCampoDataNumOuCal)
                e.Cell.Text = e.GetValue(e.DataColumn.FieldName).ToString();
        }

        protected void gvPrincipal_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.CellType == GridViewTableCommandCellType.Data)
            {
                bool indicaAssinado = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "IndicaFormularioAssinado").ToString() == "S";

                if (indicaAssinado)
                {
                    if (e.ButtonID == "Alterar")
                    {
                        e.Enabled = false;
                        e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                    }
                    else if (e.ButtonID == "Excluir")
                    {
                        e.Enabled = false;
                        e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                    }
                }
            }
        }

        protected void gvPrincipal_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            GridViewEditFormPopup popUp = (e.EditForm.NamingContainer as GridViewEditFormPopup);
            popUp.Style.Add("max-height", (((int)height.Value) - 90) + "px");
        }

        void gvGridPrincipal_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.IndexOf("Excluir;") >= 0)
            {
                int codigoFormulario = int.Parse(e.Parameters.Substring(8));
                excluiFormulario(codigoFormulario, codigoUsuarioResponsavel);
                populaGridPrincipal((sender as ASPxGridView), getDTCamposFormulario());
                (sender as ASPxGridView).DataBind();
            }
        }

        private DataTable getDTCamposFormulario(string somenteCamposGrid = "N")
        {
            DataTable dtResult = new DataTable();
            DataColumn NewColumn = null;

            // adiciona a coluna "CodigoFormulario"  que será a primary key da grid
            NewColumn = new DataColumn("CodigoFormulario", Type.GetType("System.Int32"));
            NewColumn.Caption = "CodigoFormulario";
            NewColumn.ReadOnly = true;
            dtResult.Columns.Add(NewColumn);

            NewColumn = new DataColumn("CodigoModeloFormulario", Type.GetType("System.Int32"));
            NewColumn.Caption = "CodigoModeloFormulario";
            NewColumn.ReadOnly = true;
            dtResult.Columns.Add(NewColumn);

            // adiciona a coluna "DescricaoFormulario" 
            NewColumn = new DataColumn("DescricaoFormulario", Type.GetType("System.String"));
            NewColumn.Caption = modeloNomeFormulario;
            dtResult.Columns.Add(NewColumn);

            // busca os campos do formulario
            DataTable dtCampos = getCamposModeloFormulario(this.codigoModeloFormulario, -1, somenteCamposGrid).Tables[0]; // -1= Todas as abas
            foreach (DataRow drCampo in dtCampos.Rows)
            {
                // Tipo do campo a ser representado da grid
                // =================================================================================
                Type FieldType = Type.GetType("System.String");

                string codigoTipoCampo = drCampo["CodigoTipoCampo"].ToString();
                string definicaoCampo = drCampo["DefinicaoCampo"].ToString();
                int precisaoDecimal = 0;
                string agregacao = "";
                string mostraHorario = "";
                string formatacaoCampo = "";
                string[] aDefinicaoCampo = definicaoCampo.Split(DelimitadorPropriedadeCampo);
                if (codigoTipoCampo == "VAR" || codigoTipoCampo == "TXT" ||
                    codigoTipoCampo == "CPD" || codigoTipoCampo == "LOO" ||
                    codigoTipoCampo == "LST" || codigoTipoCampo == "SUB" || codigoTipoCampo == "LNP")
                {
                    FieldType = Type.GetType("System.String");
                }
                else if (codigoTipoCampo == "NUM" || codigoTipoCampo == "CAL")
                {
                    string minimo = "";
                    string maximo = "";
                    string precisao = "0";
                    string formato = "N";

                    if (codigoTipoCampo == "NUM" && aDefinicaoCampo[0] != "")
                    {
                        minimo = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        maximo = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        precisao = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                        formatacaoCampo = formato = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();
                        if (aDefinicaoCampo[4] != "")
                            agregacao = aDefinicaoCampo[4].Substring(aDefinicaoCampo[4].IndexOf(":") + 1).Trim(); //Agr: MAX
                    }

                    if (codigoTipoCampo == "CAL" && aDefinicaoCampo[0] != "")
                    {
                        precisao = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                        formatacaoCampo = formato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        agregacao = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim(); //Agr: MAX
                    }
                    if (precisao == "0")
                        FieldType = Type.GetType("System.Int32");
                    else
                    {
                        FieldType = Type.GetType("System.Double");
                        precisaoDecimal = precisao != "" ? int.Parse(precisao) : 0;
                    }
                }
                else if (codigoTipoCampo == "DAT")
                {
                    mostraHorario = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                    FieldType = Type.GetType("System.DateTime");
                }

                // Nome do campo
                // =================================================================================
                string FieldName = drCampo["CodigoTipoCampo"].ToString() + drCampo["CodigoCampo"].ToString();

                // Título do campo
                // =================================================================================
                string FieldCaption = drCampo["NomeCampo"].ToString();

                // Adiciona a coluna no dataTable
                NewColumn = new DataColumn(FieldName, FieldType);
                NewColumn.ExtendedProperties.Add("IndicaCampoVisivelGrid", drCampo["IndicaCampoVisivelGrid"].ToString());
                NewColumn.ExtendedProperties.Add("precisaoDecimal", precisaoDecimal);
                NewColumn.ExtendedProperties.Add("agregacao", agregacao);
                NewColumn.ExtendedProperties.Add("mostraHorario", mostraHorario);
                NewColumn.ExtendedProperties.Add("formatacaoCampo", formatacaoCampo); 
                NewColumn.Caption = FieldCaption;
                NewColumn.ReadOnly = (drCampo["CodigoTipoCampo"].ToString() == "CPD");
                dtResult.Columns.Add(NewColumn);
            }

            // adiciona a coluna "DescricaoFormulario" 
            NewColumn = new DataColumn("IndicaFormularioAssinado", Type.GetType("System.String"));
            NewColumn.Caption = "";
            dtResult.Columns.Add(NewColumn);

            return dtResult;
        }

        class InsertTitleTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                GridViewHeaderTemplateContainer gridContainer = (GridViewHeaderTemplateContainer)container;

                Literal myLiteral = new Literal();
                myLiteral.Text = "<table style='width:100%'><tr><td title='" + Resources.Incluir + "' align='center'><img style='cursor:pointer' alt='" + Resources.Incluir + "' src='" + VirtualPathUtility.ToAbsolute("~/") + "imagens/botoes/incluirReg02.png' onclick='popUpFormularioEdicao(-1);'/></td></tr></table>";

                container.Controls.Add(myLiteral);
            }
        }

        private void ajustaColunasGridPrincipal(ASPxGridView gvGridPrincipal, DataTable dtDados)
        {
            gvGridPrincipal.KeyFieldName = "CodigoFormulario";
            GridViewCommandColumn column = new GridViewCommandColumn();
            column.ButtonRenderMode = GridCommandButtonRenderMode.Image;
            //column.DeleteButton.Visible = !somenteLeitura;// true;
            //column.DeleteButton.Image.Url = "~/imagens/botoes/excluirReg02.png";

            if (!somenteLeitura)
            {
                //popUpFormularioEdicao(-1);
                column.HeaderTemplate = new InsertTitleTemplate();
                column.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                //GridViewCommandColumnCustomButton btnIncluir = new GridViewCommandColumnCustomButton();
                //btnIncluir.Text = "Incluir";
                //btnIncluir.Image.Url = "~/imagens/botoes/incluirReg02.png";
                //column.CustomButtons.Add(btnIncluir);

                GridViewCommandColumnCustomButton btnEditar = new GridViewCommandColumnCustomButton();
                btnEditar.ID = "Alterar";
                btnEditar.Text = Resources.Editar;
                btnEditar.Image.Url = "~/imagens/botoes/editarReg02.png";
                column.CustomButtons.Add(btnEditar);

                GridViewCommandColumnCustomButton btnExcluir = new GridViewCommandColumnCustomButton();
                btnExcluir.ID = "Excluir";
                btnExcluir.Text = Resources.Excluir;
                btnExcluir.Image.Url = "~/imagens/botoes/excluirReg02.png";
                column.CustomButtons.Add(btnExcluir);
            }

            GridViewCommandColumnCustomButton btnVisualizar = new GridViewCommandColumnCustomButton();
            btnVisualizar.ID = "Visualizar";
            btnVisualizar.Text = Resources.Visualizar;
            btnVisualizar.Image.Url = "~/imagens/botoes/pFormulario.png";
            column.CustomButtons.Add(btnVisualizar);

            //GridViewCommandColumnCustomButton btnImprimir = new GridViewCommandColumnCustomButton();
            //btnImprimir.Text = "Imprimir";
            //btnImprimir.ID = "Imprimir";
            //btnImprimir.Image.Url = "~/imagens/botoes/imprimir.png";
            //column.CustomButtons.Add(btnImprimir);

            column.Caption = " ";
            column.Width = somenteLeitura ? new Unit("50px") : new Unit("100px");
            column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gvGridPrincipal.Columns.Add(column);

            foreach (DataColumn coluna in dtDados.Columns)
            {
                bool IndicaCampoVisivelGrid = false;
                int precisaoDecimal = 0;
                string agregacao = "";
                string formatacaoCampo = "";
                if (coluna.ExtendedProperties.Contains("IndicaCampoVisivelGrid"))
                    IndicaCampoVisivelGrid = coluna.ExtendedProperties["IndicaCampoVisivelGrid"].ToString() == "S";

                if (coluna.ExtendedProperties.Contains("precisaoDecimal"))
                {
                    precisaoDecimal = (int)coluna.ExtendedProperties["precisaoDecimal"];
                    if (coluna.ExtendedProperties.Contains("agregacao"))
                        agregacao = coluna.ExtendedProperties["agregacao"].ToString();
                }

                if (coluna.ExtendedProperties.Contains("formatacaoCampo"))
                {
                    formatacaoCampo = coluna.ExtendedProperties["formatacaoCampo"].ToString();
                }

                GridViewDataTextColumn columnTemp = new GridViewDataTextColumn();
                columnTemp.FieldName = coluna.ColumnName;
                columnTemp.Caption = coluna.Caption;
                columnTemp.Visible = IndicaCampoVisivelGrid;

                if (coluna.ColumnName == "CodigoFormulario" || coluna.ColumnName == "CodigoModeloFormulario" || coluna.ColumnName == "DescricaoFormulario" || coluna.ColumnName == "IndicaFormularioAssinado")
                    columnTemp.Visible = false;

                if (coluna.DataType == Type.GetType("System.String"))
                    columnTemp.CellStyle.HorizontalAlign = HorizontalAlign.Left;

                if (coluna.DataType == Type.GetType("System.Int32") || coluna.DataType == Type.GetType("System.Double"))
                {
                    columnTemp.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                    
                    string displayFormat = "{0:N" + precisaoDecimal + "}";
                    if(formatacaoCampo == "M")
                    {
                        displayFormat = "{0:C" + precisaoDecimal + "}";
                    }
                    if (formatacaoCampo == "P")
                    {
                        displayFormat = "{0:P" + precisaoDecimal + "}";
                    }
                    columnTemp.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                    columnTemp.PropertiesTextEdit.DisplayFormatString = displayFormat;
                    if (coluna.DataType == Type.GetType("System.Double"))
                        columnTemp.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;

                    if (agregacao != "")
                    {
                        DevExpress.Data.SummaryItemType tipoAgregacao = DevExpress.Data.SummaryItemType.None;
                        if (agregacao == "SOM")
                            tipoAgregacao = DevExpress.Data.SummaryItemType.Sum;
                        else if (agregacao == "MED")
                            tipoAgregacao = DevExpress.Data.SummaryItemType.Average;
                        else if (agregacao == "MAX")
                            tipoAgregacao = DevExpress.Data.SummaryItemType.Max;
                        else if (agregacao == "MIN")
                            tipoAgregacao = DevExpress.Data.SummaryItemType.Min;
                        ASPxSummaryItem agregacaoItem = new ASPxSummaryItem(coluna.ColumnName, tipoAgregacao);
                        agregacaoItem.DisplayFormat = displayFormat;
                        agregacaoItem.ShowInColumn = coluna.ColumnName;
                        agregacaoItem.ShowInGroupFooterColumn = coluna.ColumnName;
                        gvGridPrincipal.TotalSummary.Add(agregacaoItem);
                        gvGridPrincipal.Settings.ShowFooter = true;
                    }
                }

                if (coluna.DataType == Type.GetType("System.DateTime"))
                {
                    columnTemp.PropertiesTextEdit.DisplayFormatString = coluna.ExtendedProperties["mostraHorario"].ToString() == "S" ? "dd/MM/yyyy HH:mm:ss" : "dd/MM/yyyy";
                }
                columnTemp.Settings.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
                columnTemp.Settings.AllowDragDrop = DevExpress.Utils.DefaultBoolean.True;
                columnTemp.Width = calculaLarguraColuna(coluna.ColumnName);
                gvGridPrincipal.Columns.Add(columnTemp);
            }
            gvGridPrincipal.KeyFieldName = "CodigoFormulario";
            // se tem a linha de totalização, tem de diminuir a altura da grid
            if (gvGridPrincipal.Settings.ShowFooter)
                gvGridPrincipal.Settings.VerticalScrollableHeight = gvGridPrincipal.Settings.VerticalScrollableHeight - 35;
        }

        private Unit calculaLarguraColuna(string nomeDaColuna)
        {
            bool retorno = false;
            int codigoCampoModeloFormulario = 0;
            int largura = 0;
            if (nomeDaColuna.Contains("VAR"))
            {
                retorno = int.TryParse(nomeDaColuna.Replace("VAR", ""), out codigoCampoModeloFormulario);
            }
            if (!retorno)//retorna um valor de coluna por default
            {
                return new Unit("190px");
            }
            else
            {
                string comandoSQL = string.Format(@"select DefinicaoCampo FROM CampoModeloFormulario where CodigoCampo = {0}", codigoCampoModeloFormulario);
                DataSet ds = dados.getDataSet(comandoSQL);
                string definicaoCampo = ds.Tables[0].Rows[0]["DefinicaoCampo"].ToString();
                string[] tamanhos = definicaoCampo.Split('¥');
                string tamanho = tamanhos[0].Replace("Tam:", "").Trim();
                int tamanhoInt = int.Parse(tamanho);
                largura = tamanhoInt * 4;
                return new Unit(largura.ToString() + "px");
            }
        }

        private void populaGridPrincipal(ASPxGridView gvGridPrincipal, DataTable dtDados, bool isPostback = false)
        {
            // popula o datatable
            if (!isPostback)
            {
                DataTable dtConteudoMaster = getConteudoFormulario(0, 0, "S").Tables[0];
                int codigoFormularioTemp = 0;
                DataRow drTempResult = null;

                dtConteudoMaster_global = dtDados.Clone();
                foreach (DataRow drConteudo in dtConteudoMaster.Rows)
                {
                    bool indicaAssinado = drConteudo["IndicaFormularioAssinado"].ToString() == "S";
                    // se o formulário da lista ainda não estiver publicado, passa para o próximo;
                    if (drConteudo["DataPublicacao"].ToString().Length == 0)
                        continue;

                    // ACG 30/03/2013 - campo do tipo "SUB" não pode aparecer na grid
                    if (drConteudo["codigoTipoCampo"].ToString() == "SUB")
                        continue;

                    // se o campo não está visível na grid, pega o próximo
                    if (drConteudo["indicaCampoVisivelGrid"].ToString() == "N")
                        continue;


                    if (codigoFormularioTemp != int.Parse(drConteudo["codigoFormulario"].ToString()))
                    {
                        codigoFormularioTemp = int.Parse(drConteudo["codigoFormulario"].ToString());
                        if (drTempResult != null)
                            dtConteudoMaster_global.Rows.Add(drTempResult);
                        drTempResult = dtConteudoMaster_global.NewRow();
                        drTempResult["CodigoFormulario"] = int.Parse(drConteudo["CodigoFormulario"].ToString());
                        drTempResult["CodigoModeloFormulario"] = codigoModeloFormulario;
                        drTempResult["DescricaoFormulario"] = drConteudo["DescricaoFormulario"].ToString();
                        drTempResult["IndicaFormularioAssinado"] = drConteudo["IndicaFormularioAssinado"].ToString();
                    }
                    string codigoTipoCampo = drConteudo["codigoTipoCampo"].ToString();
                    string nomeCampo = drConteudo["codigoTipoCampo"].ToString() + drConteudo["CodigoCampo"].ToString();

                    // se o campo não tem a definição preenchida, pega o próximo
                    if (drConteudo["DefinicaoCampo"].ToString() == "")
                    {
                        drTempResult[nomeCampo] = DBNull.Value;
                        continue;
                    }

                    string[] aDefinicaoCampo = drConteudo["DefinicaoCampo"].ToString().Split(DelimitadorPropriedadeCampo);

                    string prefixoTipoCampo = "";
                    if (codigoTipoCampo == "VAR" ||
                        codigoTipoCampo == "CPD" || codigoTipoCampo == "LOO" ||
                        codigoTipoCampo == "LST" || codigoTipoCampo == "SUB" || codigoTipoCampo == "LNP")
                    {
                        prefixoTipoCampo = "VAR";
                    }
                    else if (codigoTipoCampo == "CAL")
                        prefixoTipoCampo = "NUM";
                    else
                        prefixoTipoCampo = drConteudo["codigoTipoCampo"].ToString();
                    string colunaCampo = "Valor" + prefixoTipoCampo;
                    string conteudoCampo = drConteudo[colunaCampo].ToString();
                    if (codigoTipoCampo == "CPD")
                    {
                        char IndicaCampoDinamico = 'N';
                        string conteudoCampoTemp = getConteudoCampoPreDefinido(int.Parse(drConteudo["CodigoCampo"].ToString()), codigoFormularioTemp, ref IndicaCampoDinamico, indicaAssinado, false);

                        if (indicaAssinado)
                            conteudoCampoTemp = drConteudo["ValorCampoSomenteLeitura"].ToString();

                        if (IndicaCampoDinamico == 'S' || indicaAssinado)
                            conteudoCampo = conteudoCampoTemp;
                    }
                    if (codigoTipoCampo == "LOO")
                    {
                        if (conteudoCampo != "")
                            conteudoCampo = getConteudoCampoLookup(int.Parse(drConteudo["CodigoCampo"].ToString()), int.Parse(conteudoCampo), drConteudo["NomeCampo"].ToString(), drConteudo["DefinicaoCampo"].ToString());

                        if (indicaAssinado)
                            conteudoCampo = drConteudo["ValorCampoSomenteLeitura"].ToString();
                    }
                    if (codigoTipoCampo == "DAT")
                    {
                        if (conteudoCampo == "")
                            drTempResult[nomeCampo] = DBNull.Value;
                        else
                            drTempResult[nomeCampo] = (DateTime)drConteudo[colunaCampo];
                    }
                    else if (codigoTipoCampo == "NUM")
                    {

                        string minimo = "";
                        string maximo = "";
                        string precisao = "0";
                        string formato = "N";
                        if (aDefinicaoCampo[0] != "")
                        {
                            minimo = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                            maximo = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                            precisao = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                            formato = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();
                        }

                        if (drConteudo[colunaCampo] != null && drConteudo[colunaCampo] != DBNull.Value)
                        {
                            decimal tempConteudoTipoNum = drConteudo[colunaCampo].ToString() != "" ? (decimal)drConteudo[colunaCampo] : 0;

                            if (precisao == "0" && (conteudoCampo.IndexOf(',') >= 0 || conteudoCampo.IndexOf('.') >= 0))
                                drTempResult[nomeCampo] = Convert.ToInt64(tempConteudoTipoNum);
                            else
                                drTempResult[nomeCampo] = tempConteudoTipoNum;
                        }
                    }
                    else if (codigoTipoCampo == "CAL")
                    {
                        string precisao = "0";
                        string formato = "N";
                        if (aDefinicaoCampo[0] != "")
                        {
                            precisao = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                            formato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                        }

                        decimal tempConteudoTipoNum = drConteudo[colunaCampo].ToString() != "" ? (decimal)drConteudo[colunaCampo] : 0;

                        if (indicaAssinado)
                            tempConteudoTipoNum = drConteudo["ValorCampoSomenteLeitura"].ToString() != "" ? (decimal)drConteudo["ValorCampoSomenteLeitura"] : 0;

                        if (precisao == "0" && (conteudoCampo.IndexOf(',') >= 0 || conteudoCampo.IndexOf('.') >= 0))
                            drTempResult[nomeCampo] = Convert.ToInt64(tempConteudoTipoNum);
                        else
                            drTempResult[nomeCampo] = tempConteudoTipoNum;
                    }
                    // ACG 31/03/2017: BUG 664
                    // Até a data, não havia tratamento especifico para mostrar na grid campos "LST"
                    // em 15/05/2017: 772 
                    else if (codigoTipoCampo == "LST")
                    {
                        string conteudoCampoTemp = conteudoCampo;

                        if (indicaAssinado)
                        {
                            conteudoCampoTemp = drConteudo["ValorCampoSomenteLeitura"] + "";
                            conteudoCampoTemp = conteudoCampoTemp.Replace("¥¥", ";").Replace("¥", "");
                        }
                        else
                        {
                            string[] aOpcSelecionadas = conteudoCampoTemp.Replace("¥¥", ";").Replace("¥", "").Split(';');
                            string[] aDefCampo = aDefinicaoCampo[0].Replace("Opc: ", "").Replace("\r\n", ";").Split(';');
                            string OpcoesSelecionadas = "";
                            foreach (string op in aOpcSelecionadas)
                                if (op != "")
                                {
                                    int posicaoSelecao = 0;
                                    if (int.TryParse(op, out posicaoSelecao))
                                        OpcoesSelecionadas += aDefCampo[posicaoSelecao] + "; ";
                                    else
                                        OpcoesSelecionadas += op + "; ";
                                }
                            if (OpcoesSelecionadas != "")
                                OpcoesSelecionadas = OpcoesSelecionadas.Substring(0, OpcoesSelecionadas.Length - 2);
                            conteudoCampoTemp = OpcoesSelecionadas;
                        }

                        drTempResult[nomeCampo] = conteudoCampoTemp;
                    }
                    else
                    {
                        if (indicaAssinado)
                            drTempResult[nomeCampo] = drConteudo["ValorCampoSomenteLeitura"] + "";
                        else
                            drTempResult[nomeCampo] = conteudoCampo;
                    }
                }
                // inclui a última linha
                if (drTempResult != null)
                    dtConteudoMaster_global.Rows.Add(drTempResult);
            }
            gvGridPrincipal.DataSource = dtConteudoMaster_global;
            gvGridPrincipal.DataBind();

        }

        #endregion

        #region ToDoList

        private string AjustaValorCampoData(DateTime data)
        {
            string dia = data.Day.ToString();
            string mes = data.Month.ToString();
            string ano = data.Year.ToString();
            string horax = data.TimeOfDay.ToString();
            string hora = data.Hour.ToString();
            string minuto = data.Minute.ToString();
            string segundo = data.Second.ToString();
            return string.Format("{0:n2}/{1:n2}/{2} {3:n2}:{4:n2}:{5:n2}", dia, mes, ano, hora, minuto, segundo);
        }

        #endregion

        #region Anexos - Documentos Associados
        private void renderizaAnexos(bool chamadaVeioDoEvento_pnFormulario_Callback)
        {
            string urlAnexo = VirtualPathUtility.ToAbsolute("~/") + "espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?TA=FO&ID=" + codigoFormularioMaster + "&ALT=" + (int)(height.Value - 40) + (somenteLeitura ? "&RO=S" : "");
            string frmAnexo = string.Format(@"<iframe frameborder=""0""  scrolling=""no"" width=""100%"" height=""{0}px"" id=""frmAnexo"" name=""frmAnexo"" src=""""></iframe>", (int)(height.Value + 350), urlAnexo);

            pcFormulario.JSProperties["cp_urlAnexos"] = urlAnexo;

            // Se o formulário está sendo incluído/atualizado, a url da tela de anexo tem de ser substituida, pois agora já temos o código do formulário
            if (chamadaVeioDoEvento_pnFormulario_Callback)
            {
                tabPageAnexos.Controls.Clear();
                tabPageAnexos.Controls.Add(getLiteral(frmAnexo));
            }
            else
            {
                tabPageAnexos.Controls.Add(getLiteral(frmAnexo));
            }
        }

        void gvDocAnexo_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "Atualizar")
            {
                DataSet dsAnexo = getAnexos();
                DataTable dtAnexo = dsAnexo.Tables[0];
                gvDocAnexo.DataSource = dtAnexo;
                gvDocAnexo.DataBind();
            }
        }

        void gvDocAnexo_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            //btnDownload_
            string nome = ((ASPxButton)e.CommandSource).ID.Substring(12);
            int codigoAnexo = int.Parse(nome);
            download(codigoAnexo);
        }

        protected void gvDocAnexo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int codigoAnexo = int.Parse(e.Keys["CodigoAnexo"].ToString());
            string DescricaoAnexo = e.NewValues["DescricaoAnexo"] != null ? e.NewValues["DescricaoAnexo"].ToString() : "";

            alteraDocumentoAnexo(codigoAnexo, DescricaoAnexo);

            (sender as ASPxGridView).DataSource = getAnexos();
            (sender as ASPxGridView).DataBind();

            e.Cancel = true;
            (sender as ASPxGridView).CancelEdit();

            setExisteFormularioEmEdicao((sender as ASPxGridView).ID, false);
        }

        protected void gvDocAnexo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int codigoAnexo = int.Parse(e.Keys["CodigoAnexo"].ToString());
            excluiDocumentoAnexo(codigoAnexo);
            (sender as ASPxGridView).DataSource = getAnexos();
            (sender as ASPxGridView).DataBind();
            e.Cancel = true;
        }

        private void download(int CodigoAnexoProjeto)
        {
            string NomeArquivo = "";
            // aqui ele pega o nome do arquivo no 'ref NomeArquivo'
            byte[] imagem = getConteudoAnexo(CodigoAnexoProjeto, ref NomeArquivo);

            string arquivo = page.Request.ServerVariables["APPL_PHYSICAL_PATH"] + "ArquivosTemporarios\\" + NomeArquivo;
            //string arquivo2 = "../../ArquivosTemporarios/" + NomeArquivo;
            string arquivo2 = "~/ArquivosTemporarios/" + NomeArquivo;
            FileStream fs = new FileStream(arquivo, FileMode.Create, FileAccess.Write);
            fs.Write(imagem, 0, imagem.Length);
            fs.Close();

            ForceDownloadFile(arquivo2, true);
        }

        private void ForceDownloadFile(string fname, bool forceDownload)
        {
            string path = page.MapPath(fname);
            string name = Path.GetFileName(path);
            string ext = Path.GetExtension(path);
            string type = "application/octet-stream";
            if (forceDownload)
            {
                page.Response.AppendHeader("content-disposition",
                    "attachment; filename=" + name);
            }
            page.Response.ContentType = type;
            //page.Response.WriteFile(path);
            page.Response.TransmitFile(path);
            page.Response.Flush();
            page.Response.End();
        }

        #endregion

        #region Subformulários

        private string acaoSubFormulario;
        private string botaoAcaoSubFormulario;
        private bool existeSubFormularios = false;

        protected void gv_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            GridViewEditFormPopup popUp = (e.EditForm.NamingContainer as GridViewEditFormPopup);
            popUp.Style.Add("max-height", (((int)height.Value) - 90) + "px");
            popUp.Style.Add("overflow", "auto");
            Control wc = ((ASPxGridView)sender).FindEditFormTemplateControl("CustomBtnSalvarForm");
            if (wc != null)
            {
                ASPxButton templateEdit = wc as ASPxButton;
                templateEdit.ClientVisible = !somenteLeitura;
            }
        }

        private Control renderizaCampoSUB(int codigoCampo, string[] aDefinicaoCampo, string larguraCampo, int indexAba)
        {
            existeSubFormularios = true;
            string codigoModeloSubFormulario = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            ASPxGridView controle = new ASPxGridView();
            //controle.Font.Name = "Verdana";
            //controle.Font.Size = new FontUnit("8pt");
            //controle.Width = new Unit((width.Value - 40) + "px");
            controle.Width = new Unit("100%");
            controle.ID = "CMF" + codigoModeloSubFormulario + "_ABA" + indexAba + "_" + codigoCampo;
            controle.ClientInstanceName = controle.ID;
            controle.CssFilePath = cssFilePath;
            controle.CssPostfix = cssPostFix;
            controle.SettingsEditing.EditFormColumnCount = 1;
            controle.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm;
            controle.SettingsPopup.EditForm.Modal = true;
            controle.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
            //controle.StylesEditors.Style.Font.Name = "Verdana";
            //controle.StylesEditors.Style.Font.Size = new FontUnit("8pt");
            controle.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm;
            controle.SettingsPopup.EditForm.Height = ((int)height.Value) - 10;
            //controle.SettingsPopup.EditForm.Width = new Unit((width.Value - 40) + "px");

            string resolucaoCliente = null;
            OrderedDictionary infoSistema = null;
            try
            {
                if (Session["infoSistema"] != null)
                    infoSistema = (OrderedDictionary)Session["infoSistema"];
                else
                    infoSistema = null;
            }
            catch
            {
                infoSistema = null;
            }
            if (infoSistema == null || (!infoSistema.Contains("ResolucaoCliente")))
                resolucaoCliente = null;
            else
                resolucaoCliente = infoSistema["ResolucaoCliente"].ToString();

            int larguraPrincipal = 600;
            if (resolucaoCliente != null)
            {
                larguraPrincipal = int.Parse(resolucaoCliente.Substring(0, resolucaoCliente.IndexOf('x')));
            }
            controle.SettingsPopup.EditForm.Width = new Unit((larguraPrincipal - 370) + "px");

            controle.SettingsPopup.EditForm.VerticalAlign = PopupVerticalAlign.WindowCenter;
            controle.SettingsPopup.EditForm.HorizontalAlign = PopupHorizontalAlign.WindowCenter;
            controle.StylesPopup.EditForm.MainArea.Paddings.PaddingRight = new Unit("20px");

            controle.SettingsText.CommandNew = Resources.Incluir;
            controle.SettingsText.CommandEdit = Resources.Alterar;
            controle.SettingsText.CommandDelete = Resources.Excluir;
            controle.SettingsText.CommandUpdate = Resources.Incluir;
            controle.SettingsText.CommandCancel = Resources.Cancelar;
            controle.SettingsText.CommandSelect = Resources.Visualizar;
            //controle.SettingsText.EmptyDataRow = "O formulário ainda não possui registro. Clique no botão \"Incluir\" para abrir a tela de cadastro.";

            controle.KeyFieldName = "CodigoFormulario";
            controle.CellEditorInitialize += gvSubFormulario_CellEditorInitialize;
            controle.RowInserting += gvSubFormulario_RowInserting;
            controle.RowUpdating += gvSubFormulario_RowUpdating;
            controle.RowDeleting += gvSubFormulario_RowDeleting;
            controle.InitNewRow += gvSubFormulario_InitNewRow;
            controle.StartRowEditing += gvSubFormulario_StartRowEditing;
            controle.CancelRowEditing += gvSubFormulario_CancelRowEditing;
            controle.CustomJSProperties += gvSubFormulario_CustomJSProperties;
            controle.HtmlEditFormCreated += gv_HtmlEditFormCreated;
            controle.AfterPerformCallback += gvSubFormulario__AfterPerformCallback; // new ASPxGridViewAfterPerformCallbackEventHandler(gvSubFormulario__AfterPerformCallback);
            controle.CommandButtonInitialize += gvSubFormulario_CommandButtonInitialize;
            controle.CustomButtonCallback += gvSubFormulario_CustomButtonCallback; // new ASPxGridViewCustomButtonCallbackEventHandler(gvSubFormulario_CustomButtonCallback);


            controle.ClientSideEvents.EndCallback = string.Format(
                @"function(s, e) {{
                    if(s.cp_CF != null && s.cp_CF != '' && s.cp_CF != -1 && s.cp_CF != 0)
                    {{
                        hfSessao.Set('_TipoOperacao_', s.cp_TO);
                        hfSessao.Set('_CodigoFormularioMaster_', s.cp_CF);
                    }}
                    document.getElementById('ico-required-field_sub{0}').style.display = 'none'
                    trataEndCallbackSubFormulario(s, e);
                  }}", codigoCampo);

            controle.ClientSideEvents.CustomButtonClick =
                @"function(s, e) {
                    if (e.buttonID == 'btnLupa')
                    {
                        e.processOnServer = true;
                         //" + controle.ClientInstanceName + @".StartEditRow(e.visibleIndex);
                     }
                  }";

            controle.SettingsCommandButton.NewButton.Image.Url = "~/imagens/botoes/incluirReg02.png";
            controle.SettingsCommandButton.EditButton.Image.Url = "~/imagens/botoes/editarReg02.png";
            controle.SettingsCommandButton.DeleteButton.Image.Url = "~/imagens/botoes/excluirReg02.png";
            controle.SettingsCommandButton.UpdateButton.Image.Url = "~/imagens/botoes/salvar.gif";
            controle.SettingsCommandButton.CancelButton.Image.Url = "~/imagens/botoes/cancelar.gif";

            // bug 791: Limitar o tamanho do campo a largura da coluna
            controle.SettingsBehavior.AllowEllipsisInText = true;
            controle.SettingsResizing.ColumnResizeMode = ColumnResizeMode.NextColumn;

            GridViewCommandColumn column = new GridViewCommandColumn();
            column.ButtonRenderMode = GridCommandButtonRenderMode.Image;
            if (somenteLeitura)
                column.HeaderTemplate = new BotaoInsertReadOnlyTitleTemplate();
            else
                column.HeaderTemplate = new BotaoInsertTitleTemplate();

            column.ShowEditButton = true && !somenteLeitura;
            column.ShowDeleteButton = true && !somenteLeitura;

            GridViewCommandColumnCustomButton btnLupa = new GridViewCommandColumnCustomButton() { ID = "btnLupa", Text = "Visualizar" };
            btnLupa.Image.Url = "~/imagens/botoes/pFormulario.png";
            column.CustomButtons.Add(btnLupa);

            column.Width = 120;
            controle.Columns.Add(column);

            GridViewDataTextColumn columnKey = new GridViewDataTextColumn();
            columnKey.Visible = false;
            columnKey.FieldName = "CodigoFormulario";
            controle.Columns.Add(columnKey);

            columnKey = new GridViewDataTextColumn();
            columnKey.Visible = false;
            columnKey.FieldName = "CodigoCampoModeloFormulario";
            columnKey.ToolTip = codigoCampo.ToString();
            controle.Columns.Add(columnKey);

            DataSet dsCamposSub = getCamposModeloFormulario(int.Parse(codigoModeloSubFormulario), -1);
            DataTable dtSub = new DataTable();
            dtSub.Columns.Add("CodigoFormulario", Type.GetType("System.Int32"));
            dtSub.Columns.Add("codigoModeloSubFormulario_" + codigoModeloSubFormulario, Type.GetType("System.Int32"));
            dtSub.Columns.Add("CodigoCampoModeloFormulario", Type.GetType("System.Int32"));
            dtSub.Columns.Add("ValorCampoSomenteLeitura", Type.GetType("System.String"));

            // Procura por campos calculados
            DataRow[] rCamposCalculado = dsCamposSub.Tables[0].Select("codigoTipoCampo = 'CAL'");

            if (!formularioPossuiCampoCalculado)
                formularioPossuiCampoCalculado = rCamposCalculado.Length > 0;

            //adiciona as colunas
            foreach (DataRow dr in dsCamposSub.Tables[0].Rows)
            {
                //columnTemp.Visible = IndicaCampoVisivelGrid;
                string agregacao = "";

                string definicaoCampoSUB = dr["definicaoCampo"].ToString();
                string[] aDefinicaoCampoSUB = definicaoCampoSUB.Split(DelimitadorPropriedadeCampo);
                string fieldName = dr["codigoTipoCampo"].ToString() + dr["CodigoCampo"].ToString();
                bool obrigatorio = dr["CampoObrigatorio"].ToString() == "S";
                Type fieldType = Type.GetType("System.String");

                GridViewColumn newColumn = null;

                if (dr["codigoTipoCampo"].ToString() == "VAR")
                {
                    string tamanho = aDefinicaoCampoSUB[0].Substring(aDefinicaoCampoSUB[0].IndexOf(":") + 1).Trim();

                    newColumn = new GridViewDataTextColumn();
                    ((GridViewDataTextColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataTextColumn)newColumn).PropertiesTextEdit.MaxLength = int.Parse(tamanho);
                    if (obrigatorio)
                        ((GridViewDataTextColumn)newColumn).PropertiesTextEdit.ValidationSettings.RequiredField.IsRequired = true;
                    ((GridViewDataTextColumn)newColumn).PropertiesTextEdit.ValidationSettings.ValidationGroup = "FormularioCDIS";

                    ((GridViewDataTextColumn)newColumn).EditFormSettings.Caption = dr["NomeCampo"].ToString() + ":";
                    ((GridViewDataTextColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataTextColumn)newColumn).EditFormSettings.ColumnSpan = 3;
                    ((GridViewDataTextColumn)newColumn).PropertiesTextEdit.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                    ((GridViewDataTextColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                }
                else if (dr["codigoTipoCampo"].ToString() == "TXT")
                {
                    newColumn = new GridViewDataMemoColumn();
                    ((GridViewDataMemoColumn)newColumn).FieldName = fieldName;
                    if (obrigatorio)
                        ((GridViewDataMemoColumn)newColumn).PropertiesMemoEdit.ValidationSettings.RequiredField.IsRequired = true;

                    ((GridViewDataMemoColumn)newColumn).PropertiesMemoEdit.ValidationSettings.ValidationGroup = "FormularioCDIS";
                    ((GridViewDataMemoColumn)newColumn).PropertiesMemoEdit.Rows = 5;

                    ((GridViewDataMemoColumn)newColumn).EditFormSettings.Caption = dr["NomeCampo"].ToString() + ":";
                    ((GridViewDataMemoColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataMemoColumn)newColumn).EditFormSettings.ColumnSpan = 3;
                    ((GridViewDataMemoColumn)newColumn).PropertiesMemoEdit.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                    ((GridViewDataMemoColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                }
                else if (dr["codigoTipoCampo"].ToString() == "NUM" || dr["codigoTipoCampo"].ToString() == "CAL")
                {
                    newColumn = new GridViewDataSpinEditColumn();
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DisplayFormatInEditMode = true;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.SpinButtons.ClientVisible = false;
                    ((GridViewDataSpinEditColumn)newColumn).FieldName = fieldName;

                    ((GridViewDataSpinEditColumn)newColumn).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;

                    string minimo = "";
                    string maximo = "";
                    string precisao = "";
                    string formato = "";
                    if (dr["codigoTipoCampo"].ToString() == "NUM")
                    {
                        minimo = aDefinicaoCampoSUB[0].Substring(aDefinicaoCampoSUB[0].IndexOf(":") + 1).Trim();
                        maximo = aDefinicaoCampoSUB[1].Substring(aDefinicaoCampoSUB[1].IndexOf(":") + 1).Trim();
                        precisao = aDefinicaoCampoSUB[2].Substring(aDefinicaoCampoSUB[2].IndexOf(":") + 1).Trim();
                        formato = aDefinicaoCampoSUB[3].Substring(aDefinicaoCampoSUB[3].IndexOf(":") + 1).Trim();
                        if (aDefinicaoCampoSUB[4] != "")
                            agregacao = aDefinicaoCampoSUB[4].Substring(aDefinicaoCampoSUB[4].IndexOf(":") + 1).Trim();

                    }
                    else
                    {
                        precisao = aDefinicaoCampoSUB[0].Substring(aDefinicaoCampoSUB[0].IndexOf(":") + 1).Trim();
                        formato = aDefinicaoCampoSUB[1].Substring(aDefinicaoCampoSUB[1].IndexOf(":") + 1).Trim();
                        agregacao = aDefinicaoCampoSUB[2].Substring(aDefinicaoCampoSUB[2].IndexOf(":") + 1).Trim();                        
                        ((GridViewDataSpinEditColumn)newColumn).ReadOnly = true;// campo calculado é sempre "Somente leitura"

                    }

                    string precisaoDecimal = precisao;
                    if (precisao == "0")
                    {
                        fieldType = Type.GetType("System.Int32");
                        ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NumberType = SpinEditNumberType.Integer;
                    }
                    else
                    {
                        fieldType = Type.GetType("System.Double");
                        ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NumberType = SpinEditNumberType.Float;
                    }
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DecimalPlaces = int.Parse(precisaoDecimal);
                    
                    string displayFormat = "{0:N" + precisaoDecimal + "}";
                    if (formato == "M")
                        displayFormat = "{0:C" + precisaoDecimal + "}";
                    else if (formato == "P")
                        displayFormat = "{0:P" + precisaoDecimal + "}";
                    else
                        formato = "";

                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DisplayFormatString = displayFormat;// "N" + precisaoDecimal;
                   
                    if (obrigatorio)
                        ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.ValidationSettings.RequiredField.IsRequired = true;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.ValidationSettings.ValidationGroup = "FormularioCDIS";

                    string nomeControle = "id_" + dr["CodigoCampo"].ToString();
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.ClientInstanceName = nomeControle;

                    // campo calculado é sempre "Somente leitura"
                    if (dr["codigoTipoCampo"].ToString() == "CAL")
                        ((GridViewDataSpinEditColumn)newColumn).ReadOnly = true;
                    // se existe campo calculado, verifica se este faz parte da fórmula
                    if (rCamposCalculado.Length > 0 && dr["codigoTipoCampo"].ToString() == "NUM")
                    {
                        string eventoTextChanged = "";
                        foreach (DataRow r in rCamposCalculado)
                        {
                            string[] aDefinicaoCampoCalculado = r[5].ToString().Split(DelimitadorPropriedadeCampo);
                            string formulaCalculo = aDefinicaoCampoCalculado[4].Substring(aDefinicaoCampoCalculado[4].IndexOf(":") + 1).Trim();
                            string nomeCampoCalculado = "id_" + r["CodigoCampo"].ToString();
                            string precisaoDoCampoCalculado = aDefinicaoCampoCalculado[0].Substring(aDefinicaoCampoCalculado[0].IndexOf(":") + 1).Trim();
                            // se o campo faz parta da fórmula
                            if (formulaCalculo.IndexOf("[" + dr["CodigoCampo"].ToString() + "]") >= 0)
                            {
                                eventoTextChanged += string.Format("avaliarFormula('{0}','{1}', {2}, true);", nomeCampoCalculado, formulaCalculo, precisaoDoCampoCalculado);
                            }

                        }

                        if (eventoTextChanged != "")
                        {
                            eventoTextChanged = "function(s, e) {" + eventoTextChanged + "}";
                            ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.ClientSideEvents.ValueChanged = eventoTextChanged;
                        }
                    }


                    if (agregacao != "")
                    {
                        DevExpress.Data.SummaryItemType tipoAgregacao = DevExpress.Data.SummaryItemType.None;
                        if (agregacao == "SOM")
                            tipoAgregacao = DevExpress.Data.SummaryItemType.Sum;
                        else if (agregacao == "MED")
                            tipoAgregacao = DevExpress.Data.SummaryItemType.Average;
                        else if (agregacao == "MAX")
                            tipoAgregacao = DevExpress.Data.SummaryItemType.Max;
                        else if (agregacao == "MIN")
                            tipoAgregacao = DevExpress.Data.SummaryItemType.Min;
                        ASPxSummaryItem agregacaoItem = new ASPxSummaryItem(fieldName, tipoAgregacao);
                        agregacaoItem.DisplayFormat = displayFormat;
                        agregacaoItem.ShowInColumn = fieldName;
                        agregacaoItem.ShowInGroupFooterColumn = fieldName;
                        controle.TotalSummary.Add(agregacaoItem);
                        controle.Settings.ShowFooter = true;
                    }

                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.Caption = dr["NomeCampo"].ToString() + ":";
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.ColumnSpan = 1;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;

                }
                else if (dr["codigoTipoCampo"].ToString() == "LST")
                {
                    if (indicaFormAssinado)
                    {
                        newColumn = new GridViewDataTextColumn();
                        ((GridViewDataTextColumn)newColumn).FieldName = "ValorCampoSomenteLeitura";
                        ((GridViewDataTextColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }
                    else
                    {
                        string opcoes = aDefinicaoCampoSUB[0].Substring(aDefinicaoCampoSUB[0].IndexOf(":") + 1).Trim();
                        string formato = aDefinicaoCampoSUB[1].Substring(aDefinicaoCampoSUB[1].IndexOf(":") + 1).Trim();
                        if (formato == "0" || formato == "1" || formato == "2")
                        {
                            opcoes = opcoes.Replace("\r\n", "\r");
                            string[] aOpcoes = opcoes.Split('\r');

                            newColumn = new GridViewDataComboBoxColumn();
                            ((GridViewDataComboBoxColumn)newColumn).FieldName = fieldName;
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.DataSource = getTableOpcoesLista(opcoes);
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValueField = "codigo";
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.TextField = "descricao";
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValueType = Type.GetType("System.String");
                            if (obrigatorio)
                                ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = true;
                            else
                            {
                                ListEditItem lei = new ListEditItem(Resources.NãoSeAplica, "");
                                ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.Items.Insert(0, lei);
                            }

                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValidationSettings.ValidationGroup = "FormularioCDIS";

                            ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.Caption = "" + dr["NomeCampo"].ToString() + ":";
                            ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                            ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.ColumnSpan = 1;
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                            ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;

                        }
                        else
                        {
                            newColumn = new GridViewDataTextColumn();
                            ((GridViewDataTextColumn)newColumn).FieldName = fieldName;
                            if (obrigatorio)
                                ((GridViewDataTextColumn)newColumn).PropertiesTextEdit.ValidationSettings.RequiredField.IsRequired = true;

                            ((GridViewDataTextColumn)newColumn).PropertiesTextEdit.ValidationSettings.ValidationGroup = "FormularioCDIS";

                            ((GridViewDataTextColumn)newColumn).EditFormSettings.Caption = dr["NomeCampo"].ToString() + ":";
                            ((GridViewDataTextColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                            ((GridViewDataTextColumn)newColumn).EditFormSettings.ColumnSpan = 3;
                            ((GridViewDataTextColumn)newColumn).PropertiesTextEdit.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                            ((GridViewDataTextColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                        }
                    }
                }
                else if (dr["codigoTipoCampo"].ToString() == "DAT")
                {
                    string incluirHora = aDefinicaoCampoSUB[0].Substring(aDefinicaoCampoSUB[0].IndexOf(":") + 1).Trim();
                    string valorInicial = aDefinicaoCampoSUB[1].Substring(aDefinicaoCampoSUB[1].IndexOf(":") + 1).Trim();

                    fieldType = Type.GetType("System.DateTime");
                    newColumn = new GridViewDataDateColumn();
                    ((GridViewDataDateColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataDateColumn)newColumn).Settings.ShowFilterRowMenu = DevExpress.Utils.DefaultBoolean.True;

                    if (obrigatorio)
                        ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ValidationSettings.RequiredField.IsRequired = true;

                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ValidationSettings.ValidationGroup = "FormularioCDIS";

                    ((GridViewDataDateColumn)newColumn).EditFormSettings.Caption = dr["NomeCampo"].ToString() + ":";
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.ColumnSpan = 1;
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;

                    if (incluirHora == "S")
                    {
                        ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.EditFormat = EditFormat.DateTime;
                        ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";
                    }
                    else
                    {
                        ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.EditFormat = EditFormat.Date;
                        ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.DisplayFormatString = "dd/MM/yyyy";
                    }
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;

                }
                else if (dr["codigoTipoCampo"].ToString() == "CPD")
                {
                    if (indicaFormAssinado)
                    {
                        newColumn = new GridViewDataTextColumn();
                        ((GridViewDataTextColumn)newColumn).FieldName = "ValorCampoSomenteLeitura";
                        ((GridViewDataTextColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }
                    else
                    {
                        string codigoCampoPre = aDefinicaoCampoSUB[0].Substring(aDefinicaoCampoSUB[0].IndexOf(":") + 1).Trim();
                        string linhas = "1";
                        if (aDefinicaoCampoSUB.Length > 0 && aDefinicaoCampoSUB[1] != "")
                            linhas = aDefinicaoCampoSUB[1].Substring(aDefinicaoCampoSUB[1].IndexOf(":") + 1).Trim();

                        if (linhas != "1")
                        {
                            newColumn = new GridViewDataMemoColumn();
                            ((GridViewDataMemoColumn)newColumn).FieldName = fieldName;
                            ((GridViewDataMemoColumn)newColumn).PropertiesMemoEdit.Rows = int.Parse(linhas);
                            ((GridViewDataMemoColumn)newColumn).ReadOnly = true;

                            ((GridViewDataMemoColumn)newColumn).EditFormSettings.Caption = dr["NomeCampo"].ToString() + ":";
                            ((GridViewDataMemoColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                            ((GridViewDataMemoColumn)newColumn).EditFormSettings.ColumnSpan = 3;
                            ((GridViewDataMemoColumn)newColumn).PropertiesMemoEdit.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                            ((GridViewDataMemoColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                        }
                        else
                        {
                            newColumn = new GridViewDataTextColumn();
                            ((GridViewDataTextColumn)newColumn).FieldName = fieldName;
                            ((GridViewDataTextColumn)newColumn).ReadOnly = true;

                            ((GridViewDataTextColumn)newColumn).EditFormSettings.Caption = dr["NomeCampo"].ToString() + ":";
                            ((GridViewDataTextColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                            ((GridViewDataTextColumn)newColumn).EditFormSettings.ColumnSpan = 3;
                            ((GridViewDataTextColumn)newColumn).PropertiesTextEdit.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                            ((GridViewDataTextColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                        }
                    }
                }
                else if (dr["codigoTipoCampo"].ToString() == "LOO") // ACG LOV
                {
                    if (indicaFormAssinado)
                    {
                        newColumn = new GridViewDataTextColumn();
                        ((GridViewDataTextColumn)newColumn).FieldName = "ValorCampoSomenteLeitura";
                        ((GridViewDataTextColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }
                    else
                    {
                        string codigoListaPre = aDefinicaoCampoSUB[0].Substring(aDefinicaoCampoSUB[0].IndexOf(":") + 1).Trim();
                        bool mostrarComoLov = false;
                        if (aDefinicaoCampoSUB.Length > 2 && aDefinicaoCampoSUB[2] != "")
                            mostrarComoLov = aDefinicaoCampoSUB[2].Substring(aDefinicaoCampoSUB[2].IndexOf(":") + 1).Trim().ToUpper() == "S";

                        newColumn = new GridViewDataComboBoxColumn();
                        ((GridViewDataComboBoxColumn)newColumn).FieldName = fieldName;
                        ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.DataSource = getTableOpcoesListaPre(int.Parse(codigoListaPre), "", (mostrarComoLov ? int.Parse(dr["codigoCampo"].ToString()) : -1));
                        ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValueField = "codigo";
                        ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.TextField = "descricao";
                        //((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.EnableCallbackMode = true;
                        //((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.CallbackPageSize = nCallbackPageSize;
                        ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                        ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValueType = Type.GetType("System.String");
                        if (obrigatorio)
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = true;
                        else
                        {
                            ListEditItem lei = new ListEditItem(Resources.NãoSeAplica, "");
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.Items.Insert(0, lei);
                        }

                        ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValidationSettings.ValidationGroup = "FormularioCDIS";

                        ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.Caption = dr["NomeCampo"].ToString() + ":";
                        ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                        ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.ColumnSpan = 3;
                        ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;

                        // ACG - 15/02/2014 - se é para mostrar como LOV
                        if (mostrarComoLov)
                        {
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.NullDisplayText = Resources.CliqueNoBotãoÀDireitaParaSelecionarUmNovoR;
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ClientInstanceName = "LOV_" + fieldName;
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.None;
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.DropDownButton.Visible = false;
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.Buttons.Add("...");
                            ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ClientSideEvents.ButtonClick =
                            @"function(s, e) 
                              {
                                 mostrarLov(s, e, " + codigoListaPre + @");
                              }";
                        }
                        else
                        {

                        }
                        ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    }
                }

                if (newColumn != null)
                {
                    dtSub.Columns.Add(fieldName, fieldType);
                    newColumn.Caption = dr["NomeCampo"].ToString();
                    newColumn.Name = fieldName;// dr["NomeCampo"].ToString();

                    // se o campo não deve aparece na lista...
                    if (dr["IndicaCampoVisivelGrid"].ToString() == "N")
                    {
                        //newColumn.Visible = false;
                    }

                    controle.Columns.Add(newColumn);
                }
            }

            // busca os valores para os campos do subFormularios
            dtSub = getConteudoSubFormulario(codigoCampo, dtSub, int.Parse(codigoModeloSubFormulario));
            controle.DataSource = dtSub;
            controle.DataBind();

            return controle;
        }

        class BotaoInsertTitleTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                GridViewHeaderTemplateContainer gridContainer = (GridViewHeaderTemplateContainer)container;

                Literal myLiteral = new Literal();

                myLiteral.Text = "<table style='width:100%'><tr><td title='" + Resources.Incluir + "' align='center'><img style='cursor:pointer' alt='" + Resources.Incluir + "' src='" + VirtualPathUtility.ToAbsolute("~/") + "imagens/botoes/incluirReg02.png' onclick='" + gridContainer.Grid.ClientInstanceName + ".AddNewRow()'/></td></tr></table>";

                container.Controls.Add(myLiteral);
            }
        }

        class BotaoInsertReadOnlyTitleTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                GridViewHeaderTemplateContainer gridContainer = (GridViewHeaderTemplateContainer)container;

                System.Web.UI.HtmlControls.HtmlTable table = new System.Web.UI.HtmlControls.HtmlTable();
                table.Attributes.Add("style", "width:100%");

                System.Web.UI.HtmlControls.HtmlTableRow row = new System.Web.UI.HtmlControls.HtmlTableRow();
                table.Attributes.Add("align", "center");

                //System.Web.UI.HtmlControls.HtmlTableCell cellExcel = new System.Web.UI.HtmlControls.HtmlTableCell();
                //ASPxButton btnExportarExcel = new ASPxButton();
                //btnExportarExcel.ImageUrl = @"~\imagens\botoes\btnExcel.png";
                //btnExportarExcel.RenderMode = ButtonRenderMode.Link;

                //cellExcel.Controls.Add(btnExportarExcel);
                //row.Cells.Add(cellExcel);

                System.Web.UI.HtmlControls.HtmlTableCell cellIncluir = new System.Web.UI.HtmlControls.HtmlTableCell();
                cellIncluir.InnerHtml = "<img alt='" + Resources.Incluir + "' src='" + VirtualPathUtility.ToAbsolute("~/") + "imagens/botoes/incluirRegDes.png' />";
                row.Cells.Add(cellIncluir);

                table.Rows.Add(row);
                container.Controls.Add(table);

                //Literal myLiteral = new Literal();

                // myLiteral.Text = "<table style='width:100%'><tr><td title='Incluir' align='center'><img alt='Incluir' src='" + VirtualPathUtility.ToAbsolute("~/") + "imagens/botoes/incluirRegDes.png' /></td></tr></table>";

                // container.Controls.Add(myLiteral);
            }
        }

        protected void gvSubFormulario_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            ASPxGridView controle = (ASPxGridView)sender;
            if (e.ButtonID == "btnLupa")
            {
                somenteLeitura = true;
                controle.StartEdit(e.VisibleIndex);
            }
        }

        private void gvSubFormulario_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (somenteLeitura)
            {
                if (e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }
        }

        protected void gvSubFormulario__AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            // caso exista algum campo "lookup" na grid, estes devem ser atualizados
            atualizaDataSourceCampoLookup((sender as ASPxGridView));
        }

        protected void gvSubFormulario_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView gridView = ((ASPxGridView)sender);
            Dictionary<string, object> gridViewJSProperties = gridView.JSProperties;
            if (codigoFormularioMaster > 0)
                gridViewJSProperties["cpCodigoFormularioMaster"] = codigoFormularioMaster.ToString();

            gridViewJSProperties["cpAcaoSubFormulario"] = acaoSubFormulario;
            gridViewJSProperties["cpbotaoAcaoSubFormulario"] = botaoAcaoSubFormulario;
            gridViewJSProperties["cpVisibleRowCount"] = gridView.VisibleRowCount;
        }

        protected void gvSubFormulario_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxComboBox combo = e.Editor as ASPxComboBox;
            if (combo != null)
            {
                // Apenas para combo
                if (combo.ClientInstanceName != "" && combo.ClientInstanceName.Contains("LOV"))
                {
                    string valor = combo.Value != null ? combo.Value.ToString() : "";
                    string texto = combo.Text;
                    combo.DataSource = null;
                    combo.Items.Clear();
                    //combo.DataBind();
                    if (valor != "")
                        combo.Items.Add(texto, valor);

                }
                else
                {
                    combo.DataBind();
                    if (combo.ValidationSettings.RequiredField.IsRequired == false)
                    {
                        ListEditItem lei = new ListEditItem(Resources.NãoSeAplica, "");
                        combo.Items.Insert(0, lei);
                    }
                }
                combo.ItemStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
            }

            if (e.Column.ReadOnly || somenteLeitura)
            {
                e.Editor.ReadOnly = true;
                //e.Editor.Enabled = false;
                e.Editor.ClientEnabled = false;
                e.Editor.BackColor = corReadOnly;
            }
        }

        protected void gvSubFormulario_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            DataTable dtSub = (sender as ASPxGridView).DataSource as DataTable;
            // lê o codigoModeloSubFormulario
            string sCodigoModeloSubFormulario = dtSub.Columns[1].ColumnName;
            sCodigoModeloSubFormulario = sCodigoModeloSubFormulario.Substring(sCodigoModeloSubFormulario.IndexOf('_') + 1);
            /* em 30/03/2013 - ACG inclui a linha abaixo para buscar o código do campo no formulário correspondente ao subformulário. 
             * foi criada uma coluna especificamente para guardar esta informação - CodigoCampoModeloFormulario. 
             * A informação esta armazenada na propriedade ToolTip "Miau"*/
            int codigoCampoModeloFormulario = int.Parse((sender as ASPxGridView).Columns["CodigoCampoModeloFormulario"].ToolTip);

            System.Collections.Specialized.OrderedDictionary valoresTexto = new OrderedDictionary();

            foreach (DictionaryEntry valor in e.NewValues)
            {
                string nomeColuna = valor.Key.ToString();
                if ((sender as ASPxGridView).Columns[nomeColuna] is GridViewDataComboBoxColumn)
                {
                    GridViewDataComboBoxColumn column = (sender as ASPxGridView).Columns[nomeColuna] as GridViewDataComboBoxColumn;

                    string text = column.PropertiesComboBox.Items.FindByValue(e.NewValues[nomeColuna]) == null ? (e.NewValues[nomeColuna] + "") : column.PropertiesComboBox.Items.FindByValue(e.NewValues[nomeColuna]).Text;

                    valoresTexto.Add(valor.Key, text);
                }
                else
                {
                    valoresTexto.Add(valor.Key, valor.Value);
                }
            }



            insereFormulario(true, int.Parse(sCodigoModeloSubFormulario), modeloDescricaoFormulario, codigoUsuarioResponsavel, e.NewValues, codigoCampoModeloFormulario, valoresTexto);

            dtSub = getConteudoSubFormulario(codigoCampoModeloFormulario, dtSub, int.Parse(sCodigoModeloSubFormulario));
            (sender as ASPxGridView).DataBind();

            // caso exista algum campo "lookup" na grid, estes devem ser atualizados
            // atualizaDataSourceCampoLookup((sender as ASPxGridView));

            e.Cancel = true;
            (sender as ASPxGridView).CancelEdit();

            // a atribuição para a variavel botaoAcaoSubFormulario deve ficar depois da chamada ao CancelEdit()
            botaoAcaoSubFormulario = "Salvar";
            setExisteFormularioEmEdicao((sender as ASPxGridView).ID, false);

            ((ASPxGridView)sender).JSProperties["cp_CF"] = codigoFormularioMaster;
            ((ASPxGridView)sender).JSProperties["cp_TO"] = "E";
        }

        protected void gvSubFormulario_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int codigoFormulario = int.Parse(e.Keys["CodigoFormulario"].ToString());

            /* em 30/03/2013 - ACG inclui a linha abaixo para buscar o código do campo no formulário correspondente ao subformulário. 
             * foi criada uma coluna especificamente para guardar esta informação - CodigoCampoModeloFormulario. 
             * A informação esta armazenada na propriedade ToolTip "Miau"*/
            int codigoCampoModeloFormulario = int.Parse((sender as ASPxGridView).Columns["CodigoCampoModeloFormulario"].ToolTip);

            System.Collections.Specialized.OrderedDictionary valoresTexto = new OrderedDictionary();

            foreach (DictionaryEntry valor in e.NewValues)
            {
                string nomeColuna = valor.Key.ToString();
                if ((sender as ASPxGridView).Columns[nomeColuna] is GridViewDataComboBoxColumn)
                {
                    GridViewDataComboBoxColumn column = (sender as ASPxGridView).Columns[nomeColuna] as GridViewDataComboBoxColumn;

                    string text = column.PropertiesComboBox.Items.FindByValue(e.NewValues[nomeColuna]) == null ? (e.NewValues[nomeColuna] + "") : column.PropertiesComboBox.Items.FindByValue(e.NewValues[nomeColuna]).Text;

                    valoresTexto.Add(valor.Key, text);
                }
                else
                {
                    valoresTexto.Add(valor.Key, valor.Value);
                }
            }


            atualizaFormulario(codigoFormulario, modeloDescricaoFormulario, codigoUsuarioResponsavel, e.NewValues, valoresTexto);

            // Atualiza o datasource por "Referência"
            DataTable dtSub = (sender as ASPxGridView).DataSource as DataTable;
            string sCodigoModeloSubFormulario = dtSub.Columns[1].ColumnName;
            sCodigoModeloSubFormulario = sCodigoModeloSubFormulario.Substring(sCodigoModeloSubFormulario.IndexOf('_') + 1);
            dtSub = getConteudoSubFormulario(codigoCampoModeloFormulario, dtSub, int.Parse(sCodigoModeloSubFormulario));
            (sender as ASPxGridView).DataBind();
            // ------------------ FIM - Atualiza o datasource por "Referência"

            // caso exista algum campo "lookup" na grid, estes devem ser atualizados
            // atualizaDataSourceCampoLookup((sender as ASPxGridView));

            e.Cancel = true;
            (sender as ASPxGridView).CancelEdit();

            // a atribuição para a variavel botaoAcaoSubFormulario deve ficar depois da chamada ao CancelEdit()
            botaoAcaoSubFormulario = "Salvar";
            setExisteFormularioEmEdicao((sender as ASPxGridView).ID, false);
        }

        private void atualizaDataSourceCampoLookup(ASPxGridView grid)
        {
            DataTable dtSub = grid.DataSource as DataTable;

            foreach (DataColumn coluna in dtSub.Columns)
            {

                if (coluna.ColumnName.Substring(0, 3) != "LOO")
                    continue;

                if (((GridViewDataComboBoxColumn)grid.Columns[coluna.ColumnName]).PropertiesComboBox.ClientInstanceName.Contains("LOV"))
                {
                    string codigoCampo = coluna.ColumnName.Substring(3);
                    // busca o código da lista
                    string comandoSQL = "SELECT DefinicaoCampo FROM campoModeloFormulario WHERE CodigoCampo = " + codigoCampo;
                    DataSet ds = dados.getDataSet(comandoSQL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string DefinicaoCampo = ds.Tables[0].Rows[0]["DefinicaoCampo"].ToString();
                        DefinicaoCampo = DefinicaoCampo.Substring(0, DefinicaoCampo.IndexOf(DelimitadorPropriedadeCampo));
                        DefinicaoCampo = DefinicaoCampo.Substring(DefinicaoCampo.IndexOf(":") + 1).Trim();
                        int codigoLista = int.Parse(DefinicaoCampo);
                        ((GridViewDataComboBoxColumn)grid.Columns[coluna.ColumnName]).PropertiesComboBox.DataSource = getTableOpcoesListaPre(codigoLista, "", int.Parse(codigoCampo));
                    }
                }
            }
            grid.DataBind();
        }

        protected void gvSubFormulario_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int codigoFormulario = int.Parse(e.Keys["CodigoFormulario"].ToString());

            /* em 30/03/2013 - ACG inclui a linha abaixo para buscar o código do campo no formulário correspondente ao subformulário. 
             * foi criada uma coluna especificamente para guardar esta informação - CodigoCampoModeloFormulario. 
             * A informação esta armazenada na propriedade ToolTip "Miau"*/
            int codigoCampoModeloFormulario = int.Parse((sender as ASPxGridView).Columns["CodigoCampoModeloFormulario"].ToolTip);

            DataTable dtSub = (sender as ASPxGridView).DataSource as DataTable;

            excluiFormulario(codigoFormulario, codigoUsuarioResponsavel);

            string sCodigoModeloSubFormulario = dtSub.Columns[1].ColumnName;
            sCodigoModeloSubFormulario = sCodigoModeloSubFormulario.Substring(sCodigoModeloSubFormulario.IndexOf('_') + 1);

            dtSub = getConteudoSubFormulario(codigoCampoModeloFormulario, dtSub, int.Parse(sCodigoModeloSubFormulario));
            (sender as ASPxGridView).DataBind();

            e.Cancel = true;
            //(sender as ASPxGridView).Cancel;
        }

        protected void gvSubFormulario_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            botaoAcaoSubFormulario = "Editar";
            setExisteFormularioEmEdicao((sender as ASPxGridView).ID, true);
        }

        protected void gvSubFormulario_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            botaoAcaoSubFormulario = "Incluir";
            ASPxGridView grid = sender as ASPxGridView;
            setExisteFormularioEmEdicao(grid.ID, true);
            foreach (GridViewColumn coluna in grid.Columns)
            {
                string nomeColuna = coluna.Name;
                if (nomeColuna.Length >= 3 && nomeColuna.Substring(0, 3) == "CPD")
                {
                    int codigoCampoModeloFormulario = int.Parse(nomeColuna.Substring(3));
                    char IndicaCampoDinamico = 'N';
                    e.NewValues[nomeColuna] = getConteudoCampoPreDefinido(codigoCampoModeloFormulario, -1, ref IndicaCampoDinamico, indicaFormAssinado, true);//, 0);
                }
            }
        }

        protected void gvSubFormulario_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            botaoAcaoSubFormulario = "Cancelar";
            setExisteFormularioEmEdicao((sender as ASPxGridView).ID, false);
        }

        #endregion

        #region Funções que interagem com o banco de dados

        private int getCodigoTipoAssociacao()
        {
            string comandoSQL =
                @"SELECT CodigoTipoAssociacao
                    FROM TipoAssociacao
                   WHERE IniciaisTipoAssociacao = 'FO'";

            DataSet ds = dados.getDataSet(comandoSQL);
            return int.Parse(ds.Tables[0].Rows[0]["CodigoTipoAssociacao"].ToString());
        }

        private int? getProjetoAssociado()
        {
            string comandoSQL = string.Format(
                @"SELECT CodigoProject
                    FROM FormularioProjeto
                   WHERE CodigoFormulario = {0}", codigoFormularioMaster);

            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0]["CodigoProject"].ToString());
            }
            return null;
        }

        private int getCodigoFormularioModeloUnico()
        {
            string comandoSQL = string.Format(
                @"select ISNULL(Max(F.codigoFormulario), -1) AS codigoFormulario
                    from FormularioProjeto FP inner join 
                         Formulario F on (F.CodigoFormulario = FP.CodigoFormulario) inner join
                         ModeloFormulario MF on (MF.CodigoModeloFormulario = F.CodigoModeloFormulario)
                   where FP.CodigoProject = {0}
                    and MF.codigoModeloFormulario = {1}
                    and MF.CodigoTipoFormulario = 1
                    and MF.DataExclusao is null
                    and F.DataExclusao is null", codigoProjeto, codigoModeloFormulario);
            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
                return -1;
        }

        private int insereFormulario(bool inserirFormularioMaster, int codigoModeloFormulario, string descricaoFormulario, int codigoUsuarioInclusao, OrderedDictionary valoresCampos, int? codigoCampoModeloFormulario, OrderedDictionary valoresTextoCampos)
        {
            /*O parametro "codigoCampoModeloFormulario" foi inserido em 30/03/2013 por ACG para idendificar qual o campo do formulário master corresponde ao subformulário
              Isto foi necessário para permitir que o subformulario possa ser utilizado mais de uma vez no mesmo formulário*/
            try
            {
                string comandoSQL;

                string comandoSQLMaster = "";
                string comandoLinkFormularios = "";
                string conteudoCampoDataPublicacao = "";

                if (parametrosEntrada == null || !parametrosEntrada.Contains("CodigoWorkflow"))
                    conteudoCampoDataPublicacao = "GETDATE()";
                else
                    conteudoCampoDataPublicacao = "NULL"; // null no campo datapublicação caso o formulário esteja sendo
                // dentro de um workflow

                if (inserirFormularioMaster)
                {
                    // se é para inserir o formulário master, inserir com o modelo do máster => "this.codigoModeloFormulario"
                    if (codigoFormularioMaster <= 0)
                    {
                        comandoSQLMaster = string.Format(
                            @"
                      INSERT INTO Formulario (CodigoModeloFormulario, DescricaoFormulario, DataInclusao, IncluidoPor, DataExclusao, DataPublicacao)
                           VALUES ({0}, '{1}', getdate(), {2}, getdate(), {3})

                      SELECT @CodigoFormularioMaster = scope_identity()

                 ", this.codigoModeloFormulario, descricaoFormulario, codigoUsuarioInclusao, conteudoCampoDataPublicacao);

                        // se tem código de projeto, vamos inserir um registro na tabela formularioProjeto
                        if (codigoProjeto > 0)
                        {
                            comandoSQLMaster += string.Format(
                                @"
                      INSERT INTO FormularioProjeto (CodigoFormulario, CodigoProject)
                           VALUES (@CodigoFormularioMaster, {0})

                         ", codigoProjeto);
                        }
                    }
                    else
                    {
                        comandoSQLMaster = string.Format(
                            @"
                        SELECT @CodigoFormularioMaster = {0}

                     ", codigoFormularioMaster);
                    }

                    comandoLinkFormularios = string.Format(
                        @"  INSERT INTO linkFormulario (CodigoFormulario, CodigoSubFormulario, CodigoCampoModeloFormulario)
                            VALUES (@CodigoFormularioMaster, @CodigoFormulario, {0} )

                     ", codigoCampoModeloFormulario.HasValue ? codigoCampoModeloFormulario.Value.ToString() : "NULL");
                }
                comandoSQL = string.Format(
                    @"BEGIN
                DECLARE @CodigoFormularioMaster bigint
                DECLARE @CodigoFormulario bigint    
                  
                SET @CodigoFormularioMaster = -1;

                {0} 

                INSERT INTO Formulario (CodigoModeloFormulario, DescricaoFormulario, DataInclusao, IncluidoPor, DataPublicacao)
                    VALUES ({2}, '{3}', getdate(), {4}, {5}) 

                SET @CodigoFormulario = scope_identity()

                {1}

DECLARE @RC int
DECLARE @in_codigoEntidadeContexto int
DECLARE @in_codigoUsuarioSistema int
DECLARE @in_codigoObjetoAssociado bigint
DECLARE @in_iniciaisTipoAssociacao char(2)
DECLARE @in_codigoObjetoPai bigint
DECLARE @in_codigoFormulario bigint
DECLARE @in_codigoCampo int
DECLARE @in_tipoValorCampo char(3)
DECLARE @in_valorCampo varchar(max)
DECLARE @in_valorCampoSomenteLeitura varchar(max)

    SET @in_codigoEntidadeContexto = {6}
    SET @in_codigoUsuarioSistema = {7}
    SET @in_codigoObjetoAssociado = {8}
    SET @in_iniciaisTipoAssociacao = 'PR'
    SET @in_codigoObjetoPai = 0
    SET @in_codigoFormulario = @CodigoFormulario

              ", comandoSQLMaster, comandoLinkFormularios, codigoModeloFormulario, descricaoFormulario
               , codigoUsuarioInclusao, conteudoCampoDataPublicacao, codigoEntidade, codigoUsuarioResponsavel, codigoProjeto);

                // se tem código de projeto, vamos inserir um registro na tabela formularioProjeto
                if (codigoProjeto > 0)
                {
                    comandoSQL += string.Format(
                        @"
                      INSERT INTO FormularioProjeto (CodigoFormulario, CodigoProject)
                           VALUES (@CodigoFormulario, {0})

                         ", codigoProjeto);
                }

                foreach (DictionaryEntry valor in valoresCampos)
                {
                    string tipoCampo = valor.Key.ToString().Substring(0, 3);
                    string codigoCampo = valor.Key.ToString().Substring(3);
                    string valorTextoCampo = valoresTextoCampos[valor.Key] == null ? "null" : valoresTextoCampos[valor.Key].ToString();
                    string valorCampo = "null";
                    string tipoCampoOriginal = tipoCampo;

                    // se o valor for nulo e for um campo pré-definido
                    if (valor.Value == null)
                    {

                    }
                    else
                        valorCampo = valor.Value.ToString().Trim();



                    // Os campos REF precisam ser do tipo do campo para o qual eles apontam
                    if (tipoCampo == "REF")
                    {
                        DataSet ds = getInformacoesCampo(int.Parse(codigoCampo));
                        DataRow drCampo = ds.Tables[0].Rows[0];
                        string definicaoCampo = drCampo["DefinicaoCampo"].ToString();
                        string[] aDefinicaoCampo = definicaoCampo.Split(DelimitadorPropriedadeCampo);
                        string codigoCampoRef = aDefinicaoCampo[1].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();

                        // busca o tipo do campo apontado
                        ds = getInformacoesCampo(int.Parse(codigoCampoRef));
                        drCampo = ds.Tables[0].Rows[0];
                        tipoCampo = drCampo["codigoTipoCampo"].ToString();
                    }
                    // os campos "LST" são salvos como se fossem "VAR"
                    if (tipoCampo == "LST" || tipoCampo == "CPD" || tipoCampo == "LOO")
                        tipoCampo = "VAR";

                    // se não for campo texto, tem que colocar as aspas
                    if (tipoCampo == "VAR" || tipoCampo == "TXT")
                        if (valorCampo != "null")
                        {
                            valorCampo = "'" + valorCampo.Replace("'", "''") + "'";
                            valorTextoCampo = "'" + valorTextoCampo.Replace("'", "''") + "'";
                        }

                    // se for campo data, tem que fazer o convert
                    if (tipoCampo == "DAT")
                    {
                        string tempValorCampo = valorCampo;
                        if (valorCampo.IndexOf(' ') > 0)
                            tempValorCampo = valorCampo.Substring(0, valorCampo.IndexOf(' '));

                        if (tempValorCampo == "1/1/1" || tempValorCampo == "1/1/2001" || tempValorCampo == "1/1/1 00:00:00" || tempValorCampo == "1/1/2001 0:0:0" || tempValorCampo == "null" || tempValorCampo.Trim() == "")
                        {
                            valorCampo = "null";
                            valorTextoCampo = "null";
                        }
                        else
                        {
                            DateTime Data;
                            Data = DateTime.Parse(valorCampo);
                            valorCampo = AjustaValorCampoData(Data);
                            valorCampo = " convert(datetime, '" + valorCampo + "', 103)";
                            valorTextoCampo = string.Format("'{0:dd/MM/yyyy HH:mm:ss}'", DateTime.Parse(valorTextoCampo));
                        }
                    }

                    // Para campos numéricos, temos que retirar a "," 
                    if (tipoCampo == "NUM")
                    {
                        if (System.Globalization.CultureInfo.CurrentCulture.Name == "pt-BR")
                        {
                            valorCampo = valorCampo.ToString().Replace(".", "").Trim();
                            valorCampo = valorCampo.ToString().Replace(',', '.');
                        }
                        if (valorCampo.IndexOf(' ') > 0)
                            valorCampo = valorCampo.Substring(valorCampo.IndexOf(' '));
                        else if (valorCampo == "")
                            valorCampo = "null";

                        valorTextoCampo = valorCampo;
                    }

                    if (tipoCampo == "CAL")
                    {
                        tipoCampo = "NUM";
                        if (System.Globalization.CultureInfo.CurrentCulture.Name == "pt-BR")
                        {
                            valorCampo = valorCampo.ToString().Replace(".", "").Trim();
                            valorCampo = valorCampo.ToString().Replace(',', '.');
                        }
                        if (valorCampo.IndexOf(' ') > 0)
                            valorCampo = valorCampo.Substring(valorCampo.IndexOf(' '));
                        else if (valorCampo == "")
                            valorCampo = "null";

                        valorTextoCampo = valorCampo;
                    }

                    //, ValorCampoSomenteLeitura
                    comandoSQL += string.Format(
                           @"

    SET @in_codigoCampo = {1}
    SET @in_tipoValorCampo = '{0}'
    SET @in_valorCampo = {2}
    SET @in_valorCampoSomenteLeitura = {3}

EXECUTE @RC = [dbo].[p_frm_GravaCampoFormulario] 
   @in_codigoEntidadeContexto
  ,@in_codigoUsuarioSistema
  ,@in_codigoObjetoAssociado
  ,@in_iniciaisTipoAssociacao
  ,@in_codigoObjetoPai
  ,@in_codigoFormulario
  ,@in_codigoCampo
  ,@in_tipoValorCampo
  ,@in_valorCampo
  ,@in_valorCampoSomenteLeitura
                        ", tipoCampo, codigoCampo, valorCampo.Replace("—", "-"), valorTextoCampo.Replace("—", "-"));
                }
                comandoSQL +=
                    @"      SELECT @CodigoFormulario as CodigoFormulario, 
                           @CodigoFormularioMaster as CodigoFormularioMaster

                END";

                DataTable dtRet = dados.getDataSet(comandoSQL).Tables[0];
                int codigoFormulario = int.Parse(dtRet.Rows[0]["CodigoFormulario"].ToString());


                if (codigoFormularioMaster <= 0)
                {
                    hfSessao.Set("_CodigoFormularioMaster_", dtRet.Rows[0]["CodigoFormularioMaster"].ToString());
                }

                codigoFormularioMaster = int.Parse(dtRet.Rows[0]["CodigoFormularioMaster"].ToString());

                // se não era para inserir formulário master, o codigoFormularioMaster passa a ser o codigoFormulário inserido.
                if (!inserirFormularioMaster)
                    codigoFormularioMaster = codigoFormulario;



                // executa evento after save interno
                InternalAfterSave(codigoFormulario, 'I');

                return codigoFormulario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //hfGeralFormulario.Set("CodigoProjetoParam", codigoProjeto);
        }

        private int atualizaFormulario(int codigoFormulario, string descricaoFormulario, int codigoUsuarioAtualizacao, OrderedDictionary valoresCampos, OrderedDictionary valoresTextoCampos)
        {
            try
            {
                // Se a varíavel codigoProjeto tiver valor maior que zero, tem que verificar se o formulário já está associado com este projeto.
                string verificaLinkFormularioMasterComProjeto = "";
                if (codigoProjeto > 0)
                    verificaLinkFormularioMasterComProjeto = string.Format(
                        @"IF (not exists (SELECT 1 FROM FormularioProjeto WHERE CodigoFormulario = {0} AND CodigoProject = {1} ) )
                             INSERT INTO FormularioProjeto (CodigoFormulario, CodigoProject) VALUES ( {0}, {1} )", codigoFormulario, codigoProjeto);

                string comandoSQL;
                comandoSQL = string.Format(
                    @"BEGIN
                DECLARE @CodigoFormularioMaster bigint
                DECLARE @CodigoFormulario bigint    
                  
                SET @CodigoFormularioMaster = -1;

                UPDATE Formulario 
                   SET DescricaoFormulario = '{1}'
                     , DataUltimaAlteracao = getdate()
                     , AlteradoPor = {2}
                     , DataExclusao = null
                 WHERE codigoFormulario = {0}

                 -- Verifica se o formulário está vinculado com o projeto
                 {3}
                
DECLARE @RC int
DECLARE @in_codigoEntidadeContexto int
DECLARE @in_codigoUsuarioSistema int
DECLARE @in_codigoObjetoAssociado bigint
DECLARE @in_iniciaisTipoAssociacao char(2)
DECLARE @in_codigoObjetoPai bigint
DECLARE @in_codigoFormulario bigint
DECLARE @in_codigoCampo int
DECLARE @in_tipoValorCampo char(3)
DECLARE @in_valorCampo varchar(max)
DECLARE @in_valorCampoSomenteLeitura varchar(max)

SET @in_codigoEntidadeContexto = {4}
SET @in_codigoUsuarioSistema = {5}
SET @in_codigoObjetoAssociado = {6}
SET @in_iniciaisTipoAssociacao = 'PR'
SET @in_codigoObjetoPai = 0
SET @in_codigoFormulario = {0}

              ", codigoFormulario, descricaoFormulario, codigoUsuarioAtualizacao, verificaLinkFormularioMasterComProjeto, codigoEntidade, codigoUsuarioResponsavel, codigoProjeto);

                foreach (DictionaryEntry valor in valoresCampos)
                {
                    string tipoCampo = valor.Key.ToString().Substring(0, 3);
                    string codigoCampo = valor.Key.ToString().Substring(3);
                    string valorCampo = valor.Value == null ? "null" : valor.Value.ToString().Trim();
                    string valorTextoCampo = valoresTextoCampos[valor.Key] == null ? "null" : valoresTextoCampos[valor.Key].ToString();

                    // Os campos REF precisam ser do tipo do campo para o qual eles apontam
                    if (tipoCampo == "REF")
                    {
                        DataSet ds = getInformacoesCampo(int.Parse(codigoCampo));
                        DataRow drCampo = ds.Tables[0].Rows[0];
                        string definicaoCampo = drCampo["DefinicaoCampo"].ToString();
                        string[] aDefinicaoCampo = definicaoCampo.Split(DelimitadorPropriedadeCampo);
                        string codigoCampoRef = aDefinicaoCampo[1].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();

                        // busca o tipo do campo apontado
                        ds = getInformacoesCampo(int.Parse(codigoCampoRef));
                        drCampo = ds.Tables[0].Rows[0];
                        tipoCampo = drCampo["codigoTipoCampo"].ToString();
                    }

                    // os campos "LST" são salvos como se fossem "VAR"
                    if (tipoCampo == "LST" || tipoCampo == "CPD" || tipoCampo == "LOO")
                        tipoCampo = "VAR";

                    // Para campos numéricos, temos que retirar a "," 
                    if (tipoCampo == "NUM")
                    {
                        valorCampo = valorCampo.ToString().Replace(".", "").Trim();
                        valorCampo = valorCampo.ToString().Replace(',', '.');
                        if (valorCampo.IndexOf(' ') > 0)
                            valorCampo = valorCampo.Substring(valorCampo.IndexOf(' '));
                        else if (valorCampo == "")
                            valorCampo = "null";

                        valorTextoCampo = valorCampo;
                    }

                    // se não for campo texto, tem que colocar as aspas
                    if (tipoCampo == "VAR" || tipoCampo == "TXT")
                        if (valorCampo != "null")
                        {
                            valorCampo = "'" + valorCampo.Replace("'", "''") + "'";
                            valorTextoCampo = "'" + valorTextoCampo.Replace("'", "''") + "'";

                        }//

                    // se for campo data, tem que fazer o convert
                    if (tipoCampo == "DAT")
                    {
                        string tempValorCampo = valorCampo;
                        if (valorCampo.IndexOf(' ') > 0)
                            tempValorCampo = valorCampo.Substring(0, valorCampo.IndexOf(' '));

                        if (tempValorCampo == "1/1/1" || tempValorCampo == "1/1/2001" || tempValorCampo == "1/1/1 00:00:00" || tempValorCampo == "1/1/2001 0:0:0" || tempValorCampo == "null" || tempValorCampo.Trim() == "")
                        {
                            valorCampo = "null";
                            valorTextoCampo = "null";
                        }
                        else
                        {
                            DateTime Data;
                            Data = DateTime.Parse(valorCampo);

                            valorCampo = AjustaValorCampoData(Data);
                            valorCampo = " convert(datetime, '" + valorCampo + "', 103)";

                            valorTextoCampo = string.Format("'{0:dd/MM/yyyy HH:mm:ss}'", DateTime.Parse(valorTextoCampo));
                        }
                    }

                    if (tipoCampo == "CAL")
                    {
                        tipoCampo = "NUM";
                        valorCampo = valorCampo.ToString().Replace(".", "").Trim();
                        valorCampo = valorCampo.ToString().Replace(',', '.');
                        if (valorCampo.IndexOf(' ') > 0)
                            valorCampo = valorCampo.Substring(valorCampo.IndexOf(' '));
                        else if (valorCampo == "")
                            valorCampo = "null";

                        valorTextoCampo = valorCampo;
                        // por enquanto não fazer nada com campos calculados... ainda será desenvolvido
                        //continue;
                    }
                    //, ValorCampoSomenteLeitura
                    comandoSQL += string.Format(
                           @"

    SET @in_codigoCampo = {0}
    SET @in_tipoValorCampo = '{1}'
    SET @in_valorCampo = {2}
    SET @in_valorCampoSomenteLeitura = {3}

EXECUTE @RC = [dbo].[p_frm_GravaCampoFormulario] 
   @in_codigoEntidadeContexto
  ,@in_codigoUsuarioSistema
  ,@in_codigoObjetoAssociado
  ,@in_iniciaisTipoAssociacao
  ,@in_codigoObjetoPai
  ,@in_codigoFormulario
  ,@in_codigoCampo
  ,@in_tipoValorCampo
  ,@in_valorCampo
  ,@in_valorCampoSomenteLeitura
                        ", codigoCampo, tipoCampo, valorCampo.Replace("—", "-"), valorTextoCampo.Replace("—", "-"));
                }
                comandoSQL +=
                    @"    
                END";

                int afetados = 0;
                dados.execSQL(comandoSQL, ref afetados);

                // executa evento after save interno
                InternalAfterSave(codigoFormulario, 'E');

                return afetados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private int excluiFormulario(int codigoFormulario, int codigoUsuarioAtualizacao)
        {
            string comandoSQL;
            comandoSQL = string.Format(
                @"BEGIN
                    UPDATE Formulario 
                       SET ExcluidoPor = {1}
                         , DataExclusao = getdate()
                     WHERE codigoFormulario = {0}
                  END", codigoFormulario, codigoUsuarioAtualizacao);

            int afetados = 0;
            dados.execSQL(comandoSQL, ref afetados);

            // executa evento after delete interno
            InternalAfterDelete(codigoFormulario);

            return afetados;
        }

        /// <summary>
        /// Função para chamar proc no banco que irá tratar situações de inclusão e alteração de 
        /// formulários constrolados pelo sistema
        /// </summary>
        /// <param name="codigoFormulario"></param>
        /// <param name="operacaoInclusaoEdicao"></param>
        private void InternalAfterSave(int codigoFormulario, char operacaoInclusaoEdicao)
        {
            string comandoSQL = "";
            if (operacaoInclusaoEdicao == 'I')
                comandoSQL = string.Format(@"EXEC [dbo].[p_TrataInclusaoFormulario] {0}, '{1}' ", codigoFormulario, "PR");
            else if (operacaoInclusaoEdicao == 'E')
                comandoSQL = string.Format(@"EXEC [dbo].[p_TrataAlteracaoFormulario] {0} ", codigoFormulario);

            int afetados = 0;
            dados.execSQL(comandoSQL, ref afetados);
        }

        /// <summary>
        /// Função para chamar proc no banco que irá tratar exclusão de 
        /// formulários constrolados pelo sistema
        /// </summary>
        /// <param name="codigoFormulario"></param>
        private void InternalAfterDelete(int codigoFormulario)
        {
            string comandoSQL = string.Format(@"EXEC [dbo].[p_TrataExclusaoFormulario] {0} ", codigoFormulario);
            int afetados = 0;
            dados.execSQL(comandoSQL, ref afetados);
        }

        private DataSet getCamposModeloFormulario(int codigoModeloFormulario, int aba, string somenteCamposGrid = "N")
        {
            string comandoSQL = string.Format(@"
                DECLARE @RC int
                DECLARE @CodigoEntidadeContexto int
                DECLARE @CodigoUsuariosistema int
                DECLARE @CodigoModeloFormulario int
                DECLARE @CodigoAbaFormulario smallint
                DECLARE @CodigoProjetoAssociado int
                DECLARE @CodigoFormularioMaster bigint
                DECLARE @SomenteCamposGrid char(1)

                SET @CodigoEntidadeContexto     = {0}
                SET @CodigoUsuariosistema       = {1}
                SET @CodigoModeloFormulario     = {2}
                SET @CodigoAbaFormulario        = {3}
                SET @CodigoProjetoAssociado     = {4}
                SET @CodigoFormularioMaster     = {5}
                SET @SomenteCamposGrid          = '{6}'

                EXECUTE @RC = [dbo].[p_GetDefinicoesCMF] 
                   @CodigoEntidadeContexto
                  ,@CodigoUsuariosistema
                  ,@CodigoModeloFormulario
                  ,@CodigoAbaFormulario
                  ,@CodigoProjetoAssociado
                  ,@CodigoFormularioMaster
                  ,@SomenteCamposGrid
                ", codigoEntidade, codigoUsuarioResponsavel, codigoModeloFormulario, aba, codigoProjeto, codigoFormularioMaster, somenteCamposGrid);

            DataSet ds = dados.getDataSet(comandoSQL);
            return ds;
        }

        private DataSet getInformacoesCampo(int codigoCampoModeloFormulario)
        {
            string comandoSQL = string.Format(
                @"SELECT CodigoCampo, NomeCampo, DescricaoCampo, CampoObrigatorio, 
                         CodigoTipoCampo, DefinicaoCampo, OrdemCampoFormulario, Aba, 
                         IndicaControladoSistema, IniciaisCampoControladoSistema, 
                         IndicaCampoVisivelGrid
                    FROM {0}.{1}.CampoModeloFormulario
                   WHERE CodigoCampo = {2}", dados.databaseNameCdis, dados.OwnerdbCdis, codigoCampoModeloFormulario);
            DataSet ds = dados.getDataSet(comandoSQL);

            return ds;
        }

        private string getConteudoCampoPreDefinido(int codigoCampoModeloFormulario, int codigoFormulario, ref char IndicaCampoDinamico, bool indicaFormularioAssinado, bool indicaInclusao)//, int codigoCampoPre)
        {
            if (nomeBDEmpresa == null || nomeBDEmpresa == "")
                ajustaNomeBDEmpresa();

            int codigoCampoPre = 0;
            string ConteudoCampoPre = Resources.ValorNãoEncontrado;
            // Le a definição do campo para obter o código do campo pré-definido
            DataSet ds = getInformacoesCampo(codigoCampoModeloFormulario);
            if (ds != null && ds.Tables[0] != null)
            {
                string tempDefinicao = ds.Tables[0].Rows[0]["definicaoCampo"].ToString();
                // Se não tem definição do campo gera uma Exception e interrompe a renderização do formulário
                if (tempDefinicao == "")
                {
                    string nomeCampoPre = ds.Tables[0].Rows[0]["nomeCampo"].ToString();
                    throw new Exception(string.Format("{1} ({0}) {2}.", nomeCampoPre, Resources.OFormulárioPossuiCampoPréDefinido, Resources.queNãoFoiEspecificadoDuranteSuaCriação));
                }

                string[] atempDefinicaoCampo = tempDefinicao.Split(DelimitadorPropriedadeCampo);
                codigoCampoPre = int.Parse(atempDefinicaoCampo[0].Substring(atempDefinicaoCampo[0].IndexOf(":") + 1).Trim());

                // busca o comando sql para recuperar o valor do campo pré-definido
                string comandoSQL = string.Format(
                    @"SELECT ComandoRetornoCampo, IndicaCampoDinamico, IndicaFiltroProjeto
                    FROM {0}.{1}.CampoPreDefinido
                   WHERE CodigoCampoPreDefinido = {2}", dados.databaseNameCdis, dados.OwnerdbCdis, codigoCampoPre);
                ds = dados.getDataSet(comandoSQL);
                ConteudoCampoPre = "";
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    IndicaCampoDinamico = ds.Tables[0].Rows[0]["IndicaCampoDinamico"].ToString()[0];
                    char tipoOperacao = hfSessao.Get("_TipoOperacao_").ToString()[0];
                    // se o campo é dinamico OU é um formulário novo, busca os valores atuais
                    if (IndicaCampoDinamico == 'S' || codigoFormularioMaster == 0 || tipoOperacao == 'I' || indicaInclusao)
                    {
                        string ComandoRetornoCampo = ds.Tables[0].Rows[0]["ComandoRetornoCampo"].ToString();
                        ComandoRetornoCampo = ajustaComandoSQLCamposLookup_E_CampoPreDefinido(ComandoRetornoCampo);
                        // executa o comando pré-definido
                        DataSet ds2 = dados.getDataSet(ComandoRetornoCampo);
                        if (ds2 != null && ds2.Tables[0] != null && ds2.Tables[0].Rows.Count > 0)
                            ConteudoCampoPre = ds2.Tables[0].Rows[0][0].ToString();

                        if (!indicaFormularioAssinado)
                        {
                            if (codigoFormulario == -1)
                                codigoFormulario = codigoFormularioMaster;

                            string comandoUpdate = string.Format(@"
                            UPDATE CampoFormulario SET ValorCampoSomenteLeitura = '{0}'
                             WHERE CodigoFormulario = {1}
                               AND codigoCampo = {2}", ConteudoCampoPre.Replace("'", "''"), codigoFormulario, codigoCampoModeloFormulario);

                            int regAf = 0;

                            dados.execSQL(comandoUpdate, ref regAf);
                        }
                    }
                    else // lê o valor que foi salvo no momento da inclusão
                    {
                        if (codigoFormulario == -1)
                            codigoFormulario = codigoFormularioMaster;

                        comandoSQL = string.Format(
                            @"SELECT ValorVar
                                FROM {0}.{1}.CampoFormulario
                               WHERE CodigoFormulario = {2} AND CodigoCampo = {3}", dados.databaseNameCdis, dados.OwnerdbCdis, codigoFormulario, codigoCampoModeloFormulario);
                        ds = dados.getDataSet(comandoSQL);
                        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                            ConteudoCampoPre = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            return ConteudoCampoPre;
        }

        private string getConteudoCampoLookup(int codigoCampoModeloFormulario, int codigoLookupSelecionado, string NomeCampoLoo, object definicaoCampo)
        {
            if (definicaoCampo == null)
            {
                // busca a definição do campo na tabela "CampoModeloFormulario"
            }
            string[] aDefinicaoCampo = definicaoCampo.ToString().Split(DelimitadorPropriedadeCampo);
            string codigoListaPre = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
            if (codigoListaPre == "")
            {
                throw new Exception(string.Format("{1} ({0}) {2}.", NomeCampoLoo, Resources.OFormulárioPossuiCampoLookup, Resources.queNãoFoiEspecificadoDuranteSuaCriação));
            }

            DataTable dt = getTableOpcoesListaPre(int.Parse(codigoListaPre), "", -1);
            DataRow[] rowsTemp = dt.Select("codigo = '" + codigoLookupSelecionado + "'");
            if (rowsTemp.Length > 0)
            {
                return rowsTemp[0]["Descricao"].ToString();
            }
            else
                return "";
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

        private DataSet getConteudoFormulario(int codigoCampo, int codigoModeloFormularioRef, string somenteCamposGrid = "N")
        {
            var cmf = codigoModeloFormularioRef > 0 ? codigoModeloFormularioRef : codigoModeloFormulario;
            var filtroProjeto = ((codigoProjeto > 0) && ((parametrosEntrada == null) || !parametrosEntrada.Contains("CodigoWorkflow"))) ? "S" : "N";

            var sql = string.Format(@"
DECLARE @CodigoEntidadeContexto INT,
        @CodigoUsuariosistema INT,
        @CodigoFormulario BIGINT,
        @ListaCodigosFormularios VARCHAR(MAX),
        @CodigoCampo INT,
        @SomenteCamposGrid char(1),
        @CodigoModeloFormulario INT,
        @CodigoProjeto INT,
        @IndicaFiltroProjeto CHAR(1)

    SET @CodigoEntidadeContexto = {0}
    SET @CodigoUsuariosistema = {1}
    SET @CodigoFormulario = {2}
    SET @ListaCodigosFormularios = '{3}'
    SET @CodigoCampo = {4}
    SET @SomenteCamposGrid = '{8}'
    SET @CodigoModeloFormulario = {5}
    SET @CodigoProjeto = {6}
    SET @IndicaFiltroProjeto = '{7}'

SELECT * FROM [dbo].[f_GetConteudosCMF] (
   @CodigoEntidadeContexto
  ,@CodigoUsuariosistema
  ,@CodigoFormulario
  ,@ListaCodigosFormularios
  ,@CodigoCampo
  ,@SomenteCamposGrid
  ,@CodigoModeloFormulario
  ,@CodigoProjeto
  ,@IndicaFiltroProjeto) order by CodigoFormulario desc",
  codigoEntidade,
  codigoUsuarioResponsavel,
  codigoFormularioMaster,
  inCodigosFormularios,
  codigoCampo,
  cmf,
  codigoProjeto,
  filtroProjeto,
  somenteCamposGrid);

            DataSet ds = dados.getDataSet(sql);
            return ds;
        }

        private DataTable getConteudoSubFormulario(int codigoCampo, DataTable dataTableReferencia, int codigoModeloSubFormulario)
        {
            if (hfGeralFormulario.Contains("_CodigoFormularioMaster_"))
                codigoFormularioMaster = int.Parse(hfGeralFormulario.Get("_CodigoFormularioMaster_").ToString());

            string comandoSQL = string.Format(
                @"SELECT CF.CodigoFormulario, CF.CodigoCampo, CMF.CodigoTipoCampo, CF.valorNum, 
                     CF.ValorDat, CF.ValorVar, CF.ValorTxt, CF.ValorBoo, CMF.DefinicaoCampo,
                     ValorCampoSomenteLeitura
                FROM Formulario F inner join
                     linkFormulario LF  on (LF.codigoSubFormulario = F.codigoFormulario) inner join
                     campoFormulario CF on CF.codigoFormulario = LF.CodigoSubFormulario inner join
                     campomodeloFormulario CMF on CF.codigoCampo = CMF.codigoCampo 
               WHERE LF.codigoFormulario = {0}
                 AND F.dataExclusao is null
                 AND CMF.dataExclusao is null
                 AND CMF.IndicaCampoAtivo = 'S'
                 AND CMF.CodigoModeloFormulario = {1}
                 AND (LF.CodigoCampoModeloFormulario = {2} or LF.CodigoCampoModeloFormulario is null)
               ORDER BY LF.CodigoSubFormulario, Aba, OrdemCampoFormulario", codigoFormularioMaster, codigoModeloSubFormulario, codigoCampo);
            DataSet ds = dados.getDataSet(comandoSQL);
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
                        dataTableReferencia.Rows[linha]["CodigoModeloSubFormulario_" + codigoModeloSubFormulario] = codigoModeloSubFormulario;
                        dataTableReferencia.Rows[linha]["CodigoCampoModeloFormulario"] = codigoCampo;
                    }

                    string nomeCampo = dr["codigoTipoCampo"].ToString() + dr["CodigoCampo"].ToString();
                    string colunaCampo = "Valor" + dr["codigoTipoCampo"].ToString();
                    if (dr["codigoTipoCampo"].ToString() == "LST" || dr["codigoTipoCampo"].ToString() == "LOO" || dr["codigoTipoCampo"].ToString() == "CPD")
                    {
                        colunaCampo = "ValorVAR";
                    }
                    else if (dr["codigoTipoCampo"].ToString() == "CAL")
                    {
                        colunaCampo = "ValorNUM";
                    }

                    if (indicaFormAssinado)
                        dataTableReferencia.Rows[linha]["ValorCampoSomenteLeitura"] = dr["ValorCampoSomenteLeitura"];
                    else
                        dataTableReferencia.Rows[linha][nomeCampo] = dr[colunaCampo];


                    // para os campos Pré-definidos, os que são "Dinâmicos" devem ter o seu valor sempre lidos do banco original.
                    if (dr["codigoTipoCampo"].ToString() == "CPD")
                    {


                        char IndicaCampoDinamico = 'N';
                        string temp = getConteudoCampoPreDefinido(int.Parse(dr["codigoCampo"].ToString()), codigoFormulario, ref IndicaCampoDinamico, indicaFormAssinado, false);
                        if (IndicaCampoDinamico == 'S')
                            dataTableReferencia.Rows[linha][nomeCampo] = temp;
                        /*
                                                // busca o comando sql para recuperar o valor do campo pré-definido
                                                comandoSQL = string.Format(
                                                    @"SELECT IndicaCampoDinamico 
                                                        FROM {0}.{1}.CampoPreDefinido
                                                       WHERE CodigoCampoPreDefinido = {2}", dados.databaseNameCdis, dados.OwnerdbCdis, codigoCampoPre);
                                                DataSet ds2 = dados.getDataSet(comandoSQL);
                                                if (ds2 != null && ds2.Tables[0] != null)
                                                {
                                                    string IndicaCampoDinamico = ds2.Tables[0].Rows[0]["IndicaCampoDinamico"].ToString();
                                                    if (IndicaCampoDinamico == "S")
                                                    {
                                                        char IndicaCampoDinamico = 'N';
                                                        dataTableReferencia.Rows[linha][nomeCampo] = getConteudoCampoPre(int.Parse(dr["codigoCampo"].ToString()), ref IndicaCampoDinamico);
                                                    }
                                                }
                         */
                    }
                }

                return dataTableReferencia;
            }
            else
                return null;
        }

        private string getComandoTabelaLookup(int codigoListaPre, string iniciaisLookup)
        {
            // monta o WHERE de pesquisa na tabela Lookup. Se o código for menor ou igual a zero, utilizaremos as iniciais
            string where = " WHERE CodigoLookup = " + codigoListaPre;
            if (codigoListaPre <= 0 && iniciaisLookup.Trim() != "")
                where = "WHERE IniciaisLookup = '" + iniciaisLookup + "' ";

            // busca o comando sql para recuperar o valor do campo pré-definido
            string comandoSQL = string.Format(
                @"SELECT ComandoRetornoLookup
                    FROM {0}.{1}.Lookup
                     {2}", dados.databaseNameCdis, dados.OwnerdbCdis, where);

            DataSet ds = dados.getDataSet(comandoSQL);
            string ComandoRetornoCampo = "";
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                ComandoRetornoCampo = ds.Tables[0].Rows[0]["ComandoRetornoLookup"].ToString();
                ComandoRetornoCampo = ajustaComandoSQLCamposLookup_E_CampoPreDefinido(ComandoRetornoCampo);
            }

            return ComandoRetornoCampo;
        }

        private DataTable getTableOpcoesListaPre(int codigoListaPre, string iniciaisLookup, int codigocampo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("codigo");
            dt.Columns.Add("descricao");

            if (nomeBDEmpresa == null || nomeBDEmpresa == "")
                ajustaNomeBDEmpresa();

            string comandoSQL = getComandoTabelaLookup(codigoListaPre, iniciaisLookup);

            // se passou o código do campo, filtra apenas os valores que já foram utilizados por este campo
            if (codigocampo > 0)
            {
                string orderBy = "";
                // retira o order by
                if (comandoSQL.IndexOf("ORDER BY") > 0)
                {
                    orderBy = comandoSQL.Substring(comandoSQL.IndexOf("ORDER BY"));
                    comandoSQL = comandoSQL.Substring(0, comandoSQL.IndexOf("ORDER BY"));
                }

                // encapsula comando genérico para selecionar apenas uma linha
                comandoSQL = string.Format(
                    @"SELECT Codigo, descricao FROM ({0}) as CMD 
                       WHERE Codigo in (SELECT valorvar
                                                  FROM linkformulario lf inner join
                                                       campoFormulario CF on CF.codigoFormulario = LF.CodigoSubFormulario inner join 
                                                       Formulario f on f.CodigoFormulario = lf.CodigoSubFormulario
                                                 WHERE LF.codigoFormulario = {1}
                                                   AND codigoCampo = {2}
                                                   AND f.DataExclusao is null
                                        ) 
                       {3}", comandoSQL, codigoFormularioMaster, codigocampo, orderBy);
            }

            try
            {
                DataSet ds2 = dados.getDataSet(comandoSQL);
                if (ds2 != null && ds2.Tables[0] != null)
                {
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        dt.Rows.Add(dr["Codigo"], dr["Descricao"]);
                    }
                }
            }
            catch { }

            return dt;


            //            // monta o WHERE de pesquisa na tabela Lookup. Se o código for menor ou igual a zero, utilizaremos as iniciais
            //            string where = " WHERE CodigoLookup = " + codigoListaPre;
            //            if (codigoListaPre <= 0 && iniciaisLookup.Trim() != "")
            //                where = "WHERE IniciaisLookup = '" + iniciaisLookup + "' ";

            //            // busca o comando sql para recuperar o valor do campo pré-definido
            //            string comandoSQL = string.Format(
            //                @"SELECT ComandoRetornoLookup
            //                    FROM {0}.{1}.Lookup
            //                     {2}", dados.databaseNameCdis, dados.OwnerdbCdis, where);

            //DataSet ds = dados.getDataSet(comandoSQL);
            //if (ds != null && ds.Tables[0] != null)
            //{
            //    string ComandoRetornoCampo = ds.Tables[0].Rows[0]["ComandoRetornoLookup"].ToString();
            //    ComandoRetornoCampo = ajustaComandoSQLCamposLookup_E_CampoPreDefinido(ComandoRetornoCampo);
            //    // executa o comando pré-definido
            //    try
            //    {
            //        DataSet ds2 = dados.getDataSet(ComandoRetornoCampo);
            //        if (ds2 != null && ds2.Tables[0] != null)
            //        {
            //            foreach (DataRow dr in ds2.Tables[0].Rows)
            //            {
            //                dt.Rows.Add(dr["Codigo"], dr["Descricao"]);
            //            }
            //        }
            //    }
            //    catch { }
            //}
            //return dt;
        }

        private ListEditItemCollection getItemsUsuarios(string where)
        {
            ListEditItemCollection items = new ListEditItemCollection();

            DataTable dtUsuarios = getTableOpcoesListaPre(-1, "INTP", -1);
            foreach (DataRow dr in dtUsuarios.Rows)
            {
                items.Add(dr["Descricao"].ToString(), int.Parse(dr["Codigo"].ToString()));
            }
            return items;
        }

        private ListEditItemCollection getItemsStatusTarefa()
        {
            ListEditItemCollection items = new ListEditItemCollection();

            // Monta o comando sql para recuperar a lista de usuários
            string comandoSQL = string.Format(
                @"SELECT CodigoStatusTarefa as Codigo, DescricaoStatusTarefa as Descricao
                    FROM {0}.{1}.StatusTarefa order by DescricaoStatusTarefa", dados.databaseNameCdis, dados.OwnerdbCdis);
            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    items.Add(dr["Descricao"].ToString(), int.Parse(dr["Codigo"].ToString()));
                }
            }
            return items;
        }

        private void ajustaNomeBDEmpresa()
        {
            //busca o nome do banco de dados da empresa 
            string comandoSQL = string.Format(
                 @"SELECT NomeBDProjeto, NomeOwnerProjeto 
                FROM {0}.{1}.Empresa 
               WHERE CodigoEmpresa = {2}", dados.databaseNameCdis, dados.OwnerdbCdis, codigoEntidade);
            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                nomeBDEmpresa = ds.Tables[0].Rows[0]["NomeBDProjeto"].ToString();
                nomeOwnerEmpresa = ds.Tables[0].Rows[0]["NomeOwnerProjeto"].ToString();
            }
            else
                throw new Exception(Resources.AsInformaçõesDaEmpresaInformadaNãoForamEnc);
        }

        private string ajustaComandoSQLCamposLookup_E_CampoPreDefinido(string comandoSQLOriginal)
        {
            string comandoSQL = string.Format(
                @"SELECT De, Para
                    FROM {0}.{1}.DeParaObjetosDB
                   ORDER By De", dados.databaseNameCdis, dados.OwnerdbCdis);
            DataSet ds = dados.getDataSet(comandoSQL);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                int De = int.Parse(dr["De"].ToString());
                string Para = dr["Para"].ToString().ToUpper();
                // se for campo reservado - #CodigoProjeto#, #Entidade#, #Usuario#
                if (Para == "#CODIGOPROJETO#")
                {
                    comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", codigoProjeto.ToString());
                }
                else if (Para == "#ENTIDADE#")
                {
                    comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", codigoEntidade.ToString());
                }
                else if (Para == "#USUARIO#")
                {
                    comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", codigoUsuarioResponsavel.ToString());
                }
                else if (Para == "#CODIGOWORKFLOW#")
                {
                    if ((parametrosEntrada != null) && (parametrosEntrada.Contains("CodigoWorkflow")))
                        comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", parametrosEntrada["CodigoWorkflow"].ToString());
                    else
                        comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", "-1"); // assume codigoWorkflow = -1 quando o formulário estiver sendo visualizado FORA de um fluxo
                }
                else if (Para == "#CODIGOINSTANCIAWF#")
                {
                    if ((parametrosEntrada != null) && (parametrosEntrada.Contains("CodigoInstanciaWorkflow")))
                        comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", parametrosEntrada["CodigoInstanciaWorkflow"].ToString());
                    else
                        comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", "-1"); // assume CodigoInstanciaWf = -1 quando o formulário estiver sendo visualizado FORA de um fluxo
                }
                else
                    comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", Para);
            }

            return comandoSQLOriginal;
        }

        //Validar o formulário
        public string validarModeloFormulario()
        {
            string mensagemErro = "";
            string comandoSQL = string.Format(
            @"SELECT CodigoCampo, NomeCampo, DescricaoCampo, CampoObrigatorio, CodigoTipoCampo, 
                     DefinicaoCampo, OrdemCampoFormulario, CodigoLookup, Aba, IndicaControladoSistema
                FROM {0}.{1}.CampoModeloFormulario 
               WHERE codigoModeloFormulario = {2}
                 AND dataExclusao is null
                 AND IndicaCampoAtivo = 'S'
               ORDER BY OrdemCampoFormulario", dados.databaseNameCdis, dados.OwnerdbCdis, codigoModeloFormulario);
            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                    mensagemErro = Resources.NenhumCampoFoiDefinido;
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string NomeCampo = dr["NomeCampo"].ToString();
                        string CodigoTipoCampo = dr["CodigoTipoCampo"].ToString();
                        string DefinicaoCampo = dr["DefinicaoCampo"].ToString().Trim();
                        try
                        {
                            if (DefinicaoCampo == "")
                                mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OCampo, Resources.nãoEstáCaracterizado) + '\n';
                            else
                            {
                                string[] aDefinicaoCampo = DefinicaoCampo.Split(DelimitadorPropriedadeCampo);
                                if (CodigoTipoCampo == "VAR")
                                {
                                    int tamanho = 0;
                                    if (!int.TryParse(aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim(), out tamanho) || tamanho <= 0)
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OTamanhoDoCampo, Resources.nãoFoiInformadoCorretamente) + '\n';
                                    }
                                    else
                                    {
                                        if (tamanho < aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Length)
                                        {
                                            mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OTamanhoDoCampo, Resources.éMenorQueAMáscaraInformada) + '\n';
                                        }
                                    }

                                }
                                else if (CodigoTipoCampo == "TXT")
                                {
                                    string sTamanho = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                                    string sLinhas = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                                    string sFormato = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();

                                    int formato = -1;
                                    if (!int.TryParse(sFormato, out formato) || formato < 0 || formato > 2)
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OFormatoDoCampo, Resources.nãoFoiInformadoCorretamente) + '\n';
                                    }

                                    if (formato == 0) // Multilinha não formatado, não html
                                    {
                                        int tamanho = 0;
                                        if (!int.TryParse(sTamanho, out tamanho) || tamanho <= 0)
                                        {
                                            mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OTamanhoDoCampo, Resources.nãoFoiInformadoCorretamente) + '\n';
                                        }
                                        int linhas = 0;
                                        if (!int.TryParse(sLinhas, out linhas) || linhas <= 0)
                                        {
                                            mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.AQuantidadeDeLinhasDoCampo, Resources.nãoFoiInformadaCorretamente) + '\n';
                                        }
                                    }
                                }
                                else if (CodigoTipoCampo == "NUM")
                                {
                                    string sMinimo = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                                    string sMaximo = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                                    string sPrecisao = aDefinicaoCampo[2].Substring(aDefinicaoCampo[2].IndexOf(":") + 1).Trim();
                                    string sFormato = aDefinicaoCampo[3].Substring(aDefinicaoCampo[3].IndexOf(":") + 1).Trim();

                                    if (sMinimo != "")
                                    {
                                        int temp = 0;
                                        if (!int.TryParse(sMinimo, out temp))
                                        {
                                            mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OValorMínimoDoCampo, Resources.nãoFoiInformadoCorretamente) + '\n';
                                        }
                                    }
                                    if (sMaximo != "")
                                    {
                                        int temp = 0;
                                        if (!int.TryParse(sMaximo, out temp))
                                        {
                                            mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OValorMáximoDoCampo, Resources.nãoFoiInformadoCorretamente) + '\n';
                                        }
                                    }
                                    int precisao = 0;
                                    if (!int.TryParse(sPrecisao, out precisao) || precisao < 0 || precisao > 5)
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.APrecisãoDoCampo, Resources.nãoFoiInformadoCorretamente) + '\n';
                                    }

                                    if (sFormato != "M" && sFormato != "N" && sFormato != "P")
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OFormatoDoCampo, Resources.nãoFoiInformadoCorretamente) + '\n';
                                    }

                                }
                                else if (CodigoTipoCampo == "LST")
                                {
                                    string sOpcoes = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                                    string sFormato = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();

                                    if (sOpcoes == "")
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.AsOpçõesDoCampo, Resources.nãoForamInformadasCorretamente) + '\n';
                                    }

                                    if (sFormato != "0" && sFormato != "1" && sFormato != "2")
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OFormatoDoCampo, Resources.nãoFoiInformadoCorretamente) + '\n';
                                    }

                                }
                                else if (CodigoTipoCampo == "DAT")
                                {
                                    string sIncluirHora = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                                    string sValorInicial = aDefinicaoCampo[1].Substring(aDefinicaoCampo[1].IndexOf(":") + 1).Trim();
                                    if (sIncluirHora == "" || sValorInicial == "")
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.AsInformaçõesDoCampo, Resources.nãoForamInformadasCorretamente) + '\n';
                                    }

                                }
                                else if (CodigoTipoCampo == "SUB")
                                {
                                    string sCodigoSubFormulario = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                                    if (sCodigoSubFormulario == "")
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.AsInformaçõesDoCampo, Resources.nãoForamInformadasCorretamente) + '\n';
                                    }
                                    int CodigoSubFormulario = 0;
                                    if (!int.TryParse(sCodigoSubFormulario, out CodigoSubFormulario) || CodigoSubFormulario < 0 || CodigoSubFormulario == codigoModeloFormulario)
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OSubformulárioDoCampo, Resources.nãoFoiInformadoCorretamente) + '\n';
                                    }

                                    // verificar se o subformulario ainda está disponível
                                    comandoSQL = string.Format(
                                        @"SELECT * 
                                            FROM modeloFormulario 
                                           WHERE codigoModeloFormulario = {0}
                                             AND IndicaModeloPublicado = 'S'
                                             AND IndicaToDoListAssociado = 'N'
                                             AND CodigoTipoFormulario = 2 -- APENAS LISTAS
                                             AND DataExclusao is null", sCodigoSubFormulario);
                                    DataSet dsTemp = dados.getDataSet(comandoSQL);
                                    if (dsTemp == null || dsTemp.Tables[0] == null || dsTemp.Tables[0].Rows.Count == 0)
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OSubformulárioDoCampo, Resources.nãoFoiInformadoCorretamenteOuNãoEstáMaisDi) + '\n';
                                    }
                                }
                                else if (CodigoTipoCampo == "CPD")
                                {
                                    string sCodigoCampoPre = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                                    if (sCodigoCampoPre == "")
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.AsInformaçõesDoCampo, Resources.nãoForamInformadasCorretamente) + '\n';
                                    }
                                }
                                else if (CodigoTipoCampo == "LOO")
                                {
                                    string sCodigoLista = aDefinicaoCampo[0].Substring(aDefinicaoCampo[0].IndexOf(":") + 1).Trim();
                                    if (sCodigoLista == "")
                                    {
                                        mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.AsInformaçõesDoCampo, Resources.nãoForamInformadasCorretamente) + '\n';
                                    }
                                }
                            }
                        }
                        catch
                        {
                            mensagemErro += string.Format(
@"{1} ""{0}"" {2}.", NomeCampo, Resources.OCampo, Resources.nãoEstáCaracterizadoCorretamente) + '\n';
                        }
                    }

                    if (mensagemErro == "") // tudo certo, vamos publicar o campo
                    {

                    }
                }
            }
            return mensagemErro;
        }

        public bool publicarModeloFormulario()
        {
            try
            {
                string comandoSQL = string.Format(
                    @"UPDATE ModeloFormulario
                         SET IndicaModeloPublicado = 'S'
                       WHERE CodigoModeloFormulario = {0}", codigoModeloFormulario);
                int afetados = 0;
                dados.execSQL(comandoSQL, ref afetados);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Documentos anexos
        private DataSet getAnexos()
        {
            string comandoSQL = string.Format(
                @"SELECT A.CodigoAnexo, A.Nome, A.DescricaoAnexo
                    FROM Anexo AS A INNER JOIN
                         AnexoAssociacao AS AA ON A.CodigoAnexo = AA.CodigoAnexo INNER JOIN
                         TipoAssociacao AS TA ON AA.CodigoTipoAssociacao = TA.CodigoTipoAssociacao
                   WHERE TA.IniciaisTipoAssociacao = 'FO'
                     AND AA.CodigoObjetoAssociado > 0 
                     AND AA.CodigoObjetoAssociado = {0}
               ORDER BY Nome", codigoFormularioMaster);

            DataSet ds = dados.getDataSet(comandoSQL);
            return ds;
        }

        private int excluiDocumentoAnexo(int codigoAnexo)
        {
            string comandoSQL;
            comandoSQL = string.Format(
                @"BEGIN
                    DELETE FROM AnexoAssociacao 
                     WHERE codigoAnexo = {0}

                    DELETE FROM ConteudoAnexo 
                     WHERE codigoAnexo = {0}

                    DELETE FROM Anexo 
                     WHERE codigoAnexo = {0}

                  END", codigoAnexo);

            int afetados = 0;
            dados.execSQL(comandoSQL, ref afetados);
            return afetados;
        }

        private bool alteraDocumentoAnexo(int codigoAnexo, string descricao)
        {
            string comandoSQL = string.Format(
                @" UPDATE Anexo 
                      SET DescricaoAnexo = '{1}'
                    WHERE codigoAnexo = {0}", codigoAnexo, descricao);

            int afetatos = 0;
            dados.execSQL(comandoSQL, ref afetatos);
            return true;
        }

        public byte[] getConteudoAnexo(int CodigoAnexo, ref string NomeArquivo)
        {
            byte[] ImagemArmazenada = null;
            string ComandoSQL = string.Format(
                 @"SELECT A.Nome, CA.Anexo 
                 FROM Anexo AS A INNER JOIN
                      ConteudoAnexo AS CA ON A.CodigoAnexo = CA.CodigoAnexo
                WHERE     (CA.CodigoAnexo = {0})", CodigoAnexo);
            DataSet ds = dados.getDataSet(ComandoSQL);
            if (ds != null && ds.Tables[0] != null)
            {
                NomeArquivo = ds.Tables[0].Rows[0][0].ToString();
                ImagemArmazenada = (byte[])ds.Tables[0].Rows[0][1];
            }
            return ImagemArmazenada;
        }

        #endregion

        private void setExisteFormularioEmEdicao(string idFormulario, bool emEdicao)
        {
            acaoSubFormulario = idFormulario;
            if (emEdicao)
            {
                acaoSubFormulario = "T_" + acaoSubFormulario;
            }
            else
            {
                acaoSubFormulario = "F_" + acaoSubFormulario;
            }
            /*
            string codigoModeloFormulario = idFormulario.Substring(idFormulario.IndexOf("CMF")+3);
            if (codigoModeloFormulario.IndexOf("_") >= 0)
                codigoModeloFormulario = codigoModeloFormulario.Substring(0, codigoModeloFormulario.IndexOf("_"));

            string aba = idFormulario.Substring(idFormulario.IndexOf("ABA")+3);

            if (emEdicao)
            {
                hfGeralFormulario.Set("FEDIT" + codigoModeloFormulario, aba);
            }
            else
            {
                if (hfGeralFormulario.Contains("FEDIT" + codigoModeloFormulario))
                    hfGeralFormulario.Remove("FEDIT" + codigoModeloFormulario);
            }*/
        }

        private bool getExisteFormularioEmEdicao()
        {
            return false;
            string temp = Session["_ControleInterno_"].ToString();
            return temp[0] == '1';
        }

        private object getValorCampo(Control controle)
        {
            string tipoControle = controle.ToString().Replace("DevExpress.Web.ASPxEditors.", "");
            tipoControle = tipoControle.Replace("DevExpress.Web.ASPxHtmlEditor.", "");
            tipoControle = tipoControle.Replace("DevExpress.Web.", "");
            tipoControle = tipoControle.Replace("System.Web.UI.WebControls.", "");

            if (tipoControle == "ASPxTextBox")
                return ((ASPxTextBox)controle).Text;
            else if (tipoControle == "ASPxSpinEdit")
                return ((ASPxSpinEdit)controle).Text;
            else if (tipoControle == "ASPxComboBox")
                return ((ASPxComboBox)controle).Value;
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
            else if (tipoControle == "CheckBoxList")
            {
                string indexSelected = "";
                for (int indexItem = 0; indexItem < (controle as CheckBoxList).Items.Count; indexItem++)
                {
                    ListItem item = (controle as CheckBoxList).Items[indexItem];
                    if (item.Selected)
                        indexSelected += DelimitadorPropriedadeCampo + indexItem.ToString() + DelimitadorPropriedadeCampo;
                }
                return indexSelected;
            }
            return "";
        }

        private object getValorTextoCampo(Control controle)
        {
            string tipoControle = controle.ToString().Replace("DevExpress.Web.ASPxEditors.", "");
            tipoControle = tipoControle.Replace("DevExpress.Web.ASPxHtmlEditor.", "");
            tipoControle = tipoControle.Replace("DevExpress.Web.", "");
            tipoControle = tipoControle.Replace("System.Web.UI.WebControls.", "");

            if (tipoControle == "ASPxTextBox")
                return ((ASPxTextBox)controle).Text;
            else if (tipoControle == "ASPxSpinEdit")
                return ((ASPxSpinEdit)controle).Text;
            else if (tipoControle == "ASPxComboBox")
                return ((ASPxComboBox)controle).Text;
            else if (tipoControle == "ASPxMemo")
                return ((ASPxMemo)controle).Text;
            else if (tipoControle == "ASPxRadioButtonList")
            {
                if (((ASPxRadioButtonList)controle).Value != null)
                    return ((ASPxRadioButtonList)controle).SelectedItem.Text;
                else
                    return "";
            }
            else if (tipoControle == "ASPxDateEdit")
            {
                return AjustaValorCampoData(((ASPxDateEdit)controle).Date);
            }
            else if (tipoControle == "ASPxHtmlEditor")
                return ((ASPxHtmlEditor)controle).Html;
            else if (tipoControle == "CheckBoxList")
            {
                string indexSelected = "";
                for (int indexItem = 0; indexItem < (controle as CheckBoxList).Items.Count; indexItem++)
                {
                    ListItem item = (controle as CheckBoxList).Items[indexItem];
                    if (item.Selected)
                        indexSelected += DelimitadorPropriedadeCampo + item.Text + DelimitadorPropriedadeCampo;
                }
                return indexSelected;
            }
            return "";
        }

        private Literal getLiteral(string texto)
        {
            Literal myLiteral = new Literal();
            myLiteral.Text = texto;
            return myLiteral;
        }

        #region Eventos Externos


        #region AntesSalvar

        public delegate void AntesSalvarEventHandler(object sender, EventFormsWF e, ref string mensagemErroEvento);

        public event AntesSalvarEventHandler AntesSalvar;

        /*   public virtual void RaiseAntesSalvar()
           {
               if (operacaoInclusaoEdicao != 'I')
               {
                   if (hfGeralFormulario.Get("_codigoProjeto_") != null)
                       codigoProjeto = int.Parse(hfGeralFormulario.Get("_codigoProjeto_").ToString());
               }
               EventFormsWF eFormsWf = new EventFormsWF(operacaoInclusaoEdicao, ref codigoProjeto, ref codigoFormularioMaster, ref retorno, parametrosEntrada, camposControladoSistema);

               AntesSalvar(this, eFormsWf);
           }**/

        #endregion

        #region AposSalvar

        public delegate void AposSalvarEventHandler(object sender, EventFormsWF e, ref string mensagemErroEvento);

        public event AposSalvarEventHandler AposSalvar;

        /*public virtual void RaiseAposSalvar()
        {
            if (operacaoInclusaoEdicao != 'I')
            {
                if (hfGeralFormulario.Get("_codigoProjeto_") != null)
                {
                    codigoProjeto = int.Parse(hfGeralFormulario.Get("_codigoProjeto_").ToString());
                    codigoFormularioMaster = int.Parse(hfGeralFormulario.Get("_codigoFormularioMaster_").ToString());
                }
            }
            EventFormsWF eFormsWf = new EventFormsWF(operacaoInclusaoEdicao, ref codigoProjeto, ref codigoFormularioMaster, ref retorno, parametrosEntrada, camposControladoSistema);

            AposSalvar(this, eFormsWf);
        }*/

        #endregion

        #region aposCriarFormulario

        public delegate void AposCriarFormularioEventHandler(object sender, EventFormsWF e);

        public event AposCriarFormularioEventHandler AposCriarFormulario;

        /*public virtual void RaiseAposCriarFormulario()
        {
            EventFormsWF eFormsWf = new EventFormsWF(operacaoInclusaoEdicao, ref codigoProjeto, ref codigoFormularioMaster, ref retorno, parametrosEntrada, camposControladoSistema);
            AposCriarFormulario(this, eFormsWf);
        }*/

        #endregion

        #region Imprimir

        public delegate void ImprimirEventHandler(object sender, EventFormsWF e, ref string mensagemErroEvento);

        public event ImprimirEventHandler Imprimir;

        #endregion

        #endregion
    }

    public class EventFormsWF : EventArgs
    {
        public readonly char operacaoInclusaoEdicao;
        public int codigoProjeto;
        public int codigoFormulario;
        public Hashtable parametros;
        public Hashtable parametrosEntrada;
        public List<object[]> camposControladoSistema;
        public readonly FormulariosPreDefinidos TipoFormularioPreDefinido;

        public EventFormsWF(char operacaoInclusaoEdicao, ref int codigoProjeto, ref int codigoFormulario, ref Hashtable parametros, Hashtable parametrosEntrada, List<object[]> camposControladoSistema, FormulariosPreDefinidos tipoFormularioPreDefinido)
        {
            this.operacaoInclusaoEdicao = operacaoInclusaoEdicao;
            this.codigoProjeto = codigoProjeto;
            this.codigoFormulario = codigoFormulario;
            this.parametros = parametros;
            this.parametrosEntrada = parametrosEntrada;
            this.camposControladoSistema = camposControladoSistema;
            this.TipoFormularioPreDefinido = tipoFormularioPreDefinido;
        }
    }

    class MyHyperlinkTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;
            ASPxButton btnDownload = new ASPxButton();
            btnDownload.ID = "btnDownload_" + gridContainer.KeyValue;
            btnDownload.Image.Url = "~/imagens/anexo/download.png";
            btnDownload.ToolTip = Resources.VisualizarODocumento;
            btnDownload.Width = new Unit("20px");
            btnDownload.Border.BorderStyle = BorderStyle.None;
            btnDownload.ClientSideEvents.Click =
                @"function(s, e) 
                    {
                        e.processOnServer = false;
                        var destino = 'wfDownloadArquivoAnexo.aspx?ID=" + gridContainer.KeyValue + @"';
                        window.open(destino, '_self', 'width=10,height=10,scrollbars=NO');
                    }";
            container.Controls.Add(btnDownload);
        }
    }

}
