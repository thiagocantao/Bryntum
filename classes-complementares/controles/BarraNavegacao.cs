using DevExpress.Web;
using System;
using System.ComponentModel;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CDIS.Web.Controles
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BarraNavegacao runat=server></{0}:BarraNavegacao>")]
    public class BarraNavegacao : WebControl
    {
        private Utils utils = new Utils();
        private Button btnTeste = new Button();
        private ASPxImage imgBtnFormulario = new ASPxImage();
        private ASPxImage imgBordaEsquerda = new ASPxImage();
        private ASPxImage imgBordaDireita = new ASPxImage();
        private ASPxImage imgBtnPrimeiro = new ASPxImage();
        private ASPxImage imgBtnAnterior = new ASPxImage();
        private ASPxImage imgBtnProximo = new ASPxImage();
        private ASPxImage imgBtnUltimo = new ASPxImage();

        private ASPxImage imgBtnIncluir = new ASPxImage();
        private ASPxImage imgBtnEditar = new ASPxImage();
        private ASPxImage imgBtnExcluir = new ASPxImage();

        //private ASPxImage imgBtnSalvar = new ASPxImage();
        //private ASPxImage imgBtnCancelar = new ASPxImage();

        [Description("Ajusta o valor da propriedade cellpadding"), DefaultValue(0)]
        public int CellPadding
        {
            get
            {
                String b = (String)ViewState["CellPadding"];
                return (b == null) ? 0 : int.Parse(b);
            }
            set
            {
                ViewState["CellPadding"] = value.ToString();
            }
        }

        [Description("Ajusta o valor da propriedade cellspacing"), DefaultValue(0)]
        public int CellSpacing
        {
            get
            {
                String b = (String)ViewState["CellSpacing"];
                return (b == null) ? 0 : int.Parse(b);
            }
            set
            {
                ViewState["CellSpacing"] = value.ToString();
            }
        }

        [Description("Ajusta o valor da propriedade border"), DefaultValue(0)]
        public int LarguraBorda
        {
            get
            {
                String b = (String)ViewState["LarguraBorda"];
                return (b == null) ? 0 : int.Parse(b);
            }
            set
            {
                ViewState["LarguraBorda"] = value.ToString();
            }
        }

        [Description("Ajusta o valor da largura das colunas"), DefaultValue(21)]
        public int LarguraColunaAcao
        {
            get
            {
                String b = (String)ViewState["LarguraColunaAcao"];
                return (b == null) ? 21 : int.Parse(b);
            }
            set
            {
                ViewState["LarguraColunaAcao"] = value.ToString();
            }
        }

        #region Ações 

        [Bindable(true)]
        [Category("Ações")]
        [DefaultValue("")]
        [Localizable(true)]
        public string NomeASPxGridView
        {
            get
            {
                String s = (String)ViewState["NomeASPxGridView"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["NomeASPxGridView"] = value;
            }
        }

        [Bindable(true)]
        [Category("Ações")]
        [DefaultValue("")]
        [Localizable(true)]
        public string NomeASPxPopupControl
        {
            get
            {
                String s = (String)ViewState["NomeASPxPopupControl"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["NomeASPxPopupControl"] = value;
            }
        }

        [Category("Ações"), Description("Valor indicando se os botões de navegação estarão visíveis."), DefaultValue(true), Localizable(false), Bindable(true)]
        public bool MostrarNavegacao
        {
            get
            {
                String b = (String)ViewState["MostrarNavegacao"];
                return (b == null) ? true : b == "True";
            }
            set
            {
                ViewState["MostrarNavegacao"] = value.ToString();
            }
        }

        [Category("Ações"), Description("Valor indicando se o botão de inclusão estará visível."), DefaultValue(true), Localizable(false), Bindable(true)]
        public bool MostrarInclusao
        {
            get
            {
                String b = (String)ViewState["MostrarInclusao"];
                return (b == null) ? true : b == "True";
            }
            set
            {
                ViewState["MostrarInclusao"] = value.ToString();
            }
        }

        [Category("Ações"), Description("Valor indicando se o botão de edição estará visível."), DefaultValue(true), Localizable(false), Bindable(true)]
        public bool MostrarEdicao
        {
            get
            {
                String b = (String)ViewState["MostrarEdicao"];
                return (b == null) ? true : b == "True";
            }
            set
            {
                ViewState["MostrarEdicao"] = value.ToString();
            }
        }

        [Category("Ações"), Description("Valor indicando se o botão de exclusão estará visível."), DefaultValue(true), Localizable(false), Bindable(true)]
        public bool MostrarExclusao
        {
            get
            {
                String b = (String)ViewState["MostrarExclusao"];
                return (b == null) ? true : b == "True";
            }
            set
            {
                ViewState["MostrarExclusao"] = value.ToString();
            }
        }

        [Category("Ações"), Description("Valor indicando se as bordas estarão visíveis."), DefaultValue(false), Localizable(false), Bindable(true)]
        public bool MostrarBordas
        {
            get
            {
                String b = (String)ViewState["MostrarBordas"];
                return (b == null) ? false : b == "True";
            }
            set
            {
                ViewState["MostrarBordas"] = value.ToString();
            }
        }

        #endregion

        [Localizable(false), Description("Ajusta a pasta base onde estão as imagens a ser mostradas na barra de navegação."), DefaultValue("../imagens/Temas/Aqua"), UrlProperty, Bindable(true)]
        public string PathImagesUrl
        {
            get
            {
                String s = (String)ViewState["PathImagesUrl"];
                return ((s == null) ? "../imagens/Temas/Aqua" : s);
            }
            set
            {
                ViewState["PathImagesUrl"] = value;
            }
        }

        [Localizable(false), Description("Ajusta a pasta base onde estão os scripts"), DefaultValue("../scripts"), UrlProperty, Bindable(true)]
        public string PathScripts
        {
            get
            {
                String s = (String)ViewState["PathScripts"];
                return ((s == null) ? "../scripts" : s);
            }
            set
            {
                ViewState["PathScripts"] = value;
            }
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();
            imgBtnFormulario.ID = "Formulario";
            imgBtnPrimeiro.ID = "Primeiro";
            imgBtnAnterior.ID = "Anterior";
            imgBtnProximo.ID = "Proximo";
            imgBtnUltimo.ID = "Ultimo";
            imgBtnIncluir.ID = "Incluir";
            imgBtnEditar.ID = "Editar";
            imgBtnExcluir.ID = "Excluir";
            // imgBtnSalvar.ID = "Salvar";
            //imgBtnCancelar.ID = "Cancelar";

            imgBtnFormulario.ClientInstanceName = "imgBtnFormulario";
            imgBtnPrimeiro.ClientInstanceName = "imgBtnPrimeiro";
            imgBtnAnterior.ClientInstanceName = "imgBtnAnterior";
            imgBtnProximo.ClientInstanceName = "imgBtnProximo";
            imgBtnUltimo.ClientInstanceName = "imgBtnUltimo";
            imgBtnIncluir.ClientInstanceName = "imgBtnIncluir";
            imgBtnEditar.ClientInstanceName = "imgBtnEditar";
            imgBtnExcluir.ClientInstanceName = "imgBtnExcluir";
            // imgBtnSalvar.ClientInstanceName = "imgBtnSalvar";
            // imgBtnCancelar.ClientInstanceName = "imgBtnCancelar";

            imgBtnFormulario.JSProperties["cp_acao"] = imgBtnFormulario.ID;
            imgBtnPrimeiro.JSProperties["cp_acao"] = imgBtnPrimeiro.ID;
            imgBtnAnterior.JSProperties["cp_acao"] = imgBtnAnterior.ID;
            imgBtnProximo.JSProperties["cp_acao"] = imgBtnProximo.ID;
            imgBtnUltimo.JSProperties["cp_acao"] = imgBtnUltimo.ID;
            imgBtnIncluir.JSProperties["cp_acao"] = imgBtnIncluir.ID;
            imgBtnEditar.JSProperties["cp_acao"] = imgBtnEditar.ID;
            imgBtnExcluir.JSProperties["cp_acao"] = imgBtnExcluir.ID;
            //imgBtnSalvar.JSProperties["cp_acao"] = imgBtnSalvar.ID;
            // imgBtnCancelar.JSProperties["cp_acao"] = imgBtnCancelar.ID;

            imgBtnFormulario.ToolTip = "Mostrar formulário";
            imgBtnPrimeiro.ToolTip = "Primeiro registro";
            imgBtnAnterior.ToolTip = "Registro anterior";
            imgBtnProximo.ToolTip = "Próximo registro";
            imgBtnUltimo.ToolTip = "Último registro";
            imgBtnIncluir.ToolTip = "Novo registro";
            imgBtnEditar.ToolTip = "Editar registro";
            imgBtnExcluir.ToolTip = "Excluir registro";
            //imgBtnSalvar.ToolTip = "Salvar";
            //imgBtnCancelar.ToolTip = "Cancelar";

            imgBtnFormulario.ImageUrl = PathImagesUrl + "/pFormulario.png";
            imgBordaEsquerda.ImageUrl = PathImagesUrl + "/BordaEsquerda.png";
            imgBtnPrimeiro.ImageUrl = PathImagesUrl + "/pFirst.png";
            imgBtnAnterior.ImageUrl = PathImagesUrl + "/pPrev.png";
            imgBtnProximo.ImageUrl = PathImagesUrl + "/pNext.png";
            imgBtnUltimo.ImageUrl = PathImagesUrl + "/pLast.png";
            imgBtnIncluir.ImageUrl = PathImagesUrl + "/pIncluir.png";
            imgBtnEditar.ImageUrl = PathImagesUrl + "/pEditar.png";
            imgBtnExcluir.ImageUrl = PathImagesUrl + "/pExcluir.png";
            //imgBtnSalvar.ImageUrl = PathImagesUrl + "/pSalvarDisabled.png";
            //imgBtnCancelar.ImageUrl = PathImagesUrl + "/pCancelarDisabled.png";
            imgBordaDireita.ImageUrl = PathImagesUrl + "/BordaDireita.png";

            imgBtnFormulario.Style.Add("cursor", "hand");
            imgBtnPrimeiro.Style.Add("cursor", "hand");
            imgBtnAnterior.Style.Add("cursor", "hand");
            imgBtnProximo.Style.Add("cursor", "hand");
            imgBtnUltimo.Style.Add("cursor", "hand");
            imgBtnIncluir.Style.Add("cursor", "hand");
            imgBtnEditar.Style.Add("cursor", "hand");
            imgBtnExcluir.Style.Add("cursor", "hand");
            //imgBtnSalvar.Style.Add("cursor", "hand");
            //imgBtnCancelar.Style.Add("cursor", "hand");

            imgBtnFormulario.ClientSideEvents.Click = onClickSideClient('F');
            imgBtnPrimeiro.ClientSideEvents.Click = onClickSideClient('A');
            imgBtnAnterior.ClientSideEvents.Click = onClickSideClient('A');
            imgBtnProximo.ClientSideEvents.Click = onClickSideClient('A');
            imgBtnUltimo.ClientSideEvents.Click = onClickSideClient('A');
            imgBtnIncluir.ClientSideEvents.Click = onClickSideClient('A');
            imgBtnEditar.ClientSideEvents.Click = onClickSideClient('A');
            imgBtnExcluir.ClientSideEvents.Click = onClickSideClient('A');
            //imgBtnSalvar.ClientSideEvents.Click = onClickSideClient('A');
            // imgBtnCancelar.ClientSideEvents.Click = onClickSideClient('A');

            Controls.Add(utils.getLiteral(string.Format(
                @"<table border=""{0}"" bordercolor=""red"" cellpadding=""{1}"" cellspacing=""{2}"">
                    <tr style=""height:35px"">", LarguraBorda, CellPadding, CellSpacing)));

            string stiloBordas = string.Format(@"style=""width:{0}px;text-align: center;"" ", LarguraColunaAcao);
            if (MostrarBordas)
            {
                stiloBordas = string.Format(@"style=""background-image: url({0}/bordasSuperiorInferior.png); width:{1}px; text-align: center;background-repeat: repeat-x"" ", PathImagesUrl, LarguraColunaAcao);
                Controls.Add(utils.getLiteral("<td>"));
                Controls.Add(imgBordaEsquerda);
                Controls.Add(utils.getLiteral("</td>"));
            }

            Controls.Add(utils.getLiteral(string.Format("<td {0}>", stiloBordas)));
            Controls.Add(imgBtnFormulario);
            Controls.Add(utils.getLiteral("</td>"));


            if (MostrarNavegacao)
            {
                Controls.Add(utils.getLiteral(string.Format("<td {0}>", stiloBordas)));
                Controls.Add(imgBtnPrimeiro);
                Controls.Add(utils.getLiteral(string.Format("</td><td {0}>", stiloBordas)));
                Controls.Add(imgBtnAnterior);
                Controls.Add(utils.getLiteral(string.Format("</td><td {0}>", stiloBordas)));
                Controls.Add(imgBtnProximo);
                Controls.Add(utils.getLiteral(string.Format("</td><td {0}>", stiloBordas)));
                Controls.Add(imgBtnUltimo);
                Controls.Add(utils.getLiteral("</td>"));
            }
            if (MostrarInclusao)
            {
                Controls.Add(utils.getLiteral(string.Format("<td {0}>", stiloBordas)));
                Controls.Add(imgBtnIncluir);
                Controls.Add(utils.getLiteral("</td>"));
            }
            if (MostrarEdicao)
            {
                Controls.Add(utils.getLiteral(string.Format("<td {0}>", stiloBordas)));
                Controls.Add(imgBtnEditar);
                Controls.Add(utils.getLiteral("</td>"));
            }
            if (MostrarExclusao)
            {
                Controls.Add(utils.getLiteral(string.Format("<td {0}>", stiloBordas)));
                Controls.Add(imgBtnExcluir);
                Controls.Add(utils.getLiteral("</td>"));
            }
            /* if (MostrarInclusao || MostrarEdicao)
             {
                 Controls.Add(utils.getLiteral(string.Format("<td {0}>", stiloBordas)));
                 Controls.Add(imgBtnSalvar);
                 Controls.Add(utils.getLiteral("</td>"));
                 Controls.Add(utils.getLiteral(string.Format("<td {0}>", stiloBordas)));
                 Controls.Add(imgBtnCancelar);
                 Controls.Add(utils.getLiteral("</td>"));
             }*/
            if (MostrarBordas)
            {
                Controls.Add(utils.getLiteral("<td>"));
                Controls.Add(imgBordaDireita);
                Controls.Add(utils.getLiteral("</td>"));
            }

            Controls.Add(utils.getLiteral("</tr></table>"));

            //base.CreateChildControls();
        }

        private string onClickSideClient(char tipoBotao)
        {
            if (tipoBotao == 'A') // botões de ações 
            {
                if (NomeASPxGridView != "")
                {
                    // O uso do popupControl é opcional
                    if (NomeASPxPopupControl == "")
                        NomeASPxPopupControl = "null";

                    return "function(s, e) { if (window." + NomeASPxGridView + ") onClickBarraNavegacao(s.cp_acao , " + NomeASPxGridView + ", " + NomeASPxPopupControl + "); else alert('A grid informada na propriedade NomeASPxGridView não foi encontrada.');}";
                }
                else
                    return "function(s, e) { alert('A barra de navegação não foi associada a uma grid de dados. Utilize a propriedade \"NomeASPxGridView\".');}";
            }
            else if (tipoBotao == 'F') // Botão Modo    Formulário
            {
                if (NomeASPxPopupControl != "")
                {
                    return @"function(s, e) 
                             {
                                 if (window." + NomeASPxPopupControl + @") 
                                 {
                                    if (!" + NomeASPxPopupControl + @".IsVisible())
                                    {
                                        if (!window.LimpaCamposFormulario) 
                                           alert('O método ""LimpaCamposFormulario"" não foi implementado!'); 
                                        else if (!window.MontaCamposFormulario) 
                                           alert('O método ""MontaCamposFormulario"" não foi implementado!'); 
                                        else 
                                        { 
                                           if ( " + NomeASPxGridView + @".GetVisibleRowsOnPage() <=0) 
                                           {
                                              habilitarBotoesAcessoModoFormulario(false);
                                              habilitarBotoesNavegacao(false);
                                              alert('Não existe registro para ser mostrado no modo formulário');
                                           }
                                           else
                                           {
                                               if (window.btnSalvar) 
                                                  btnSalvar.SetVisible(false);

                                               OnGridFocusedRowChanged(" + NomeASPxGridView + @", true)
                                               " + NomeASPxPopupControl + @".Show();
                                           }
                                        }
                                    }
                                    else
                                       " + NomeASPxPopupControl + @".Hide();
                                 }
                                 else 
                                    alert('O popupControl informado na propriedade NomeASPxPopupControl não foi encontrado.');  
                                 }";
                }
                else
                    return "function(s, e) { alert('A barra de navegação não foi associada a um popupControl. Utilize a propriedade \"NomeASPxPopupControl\".');}";

            }
            else
            {
                return "";
            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("barraNavegacao"))
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "variaveis", string.Format(@"<script type=""text/javascript"">var PathImagesUrl = ""{0}""; var TipoOperacao = """";</script>", PathImagesUrl));
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "barraNavegacao", string.Format(@"<script src=""{0}/barraNavegacao.js"" type=""text/javascript""></script>", PathScripts));

                string NomeArquivoJS = Page.Request.PhysicalPath.Replace("aspx", "js");
                string pathArquivoJS = Page.Request.PhysicalApplicationPath + @"\Scripts\" + Path.GetFileName(NomeArquivoJS);
                if (!File.Exists(pathArquivoJS))
                {
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "aviso", string.Format(@"<script type=""text/javascript"">alert(""O arquivo {0} não foi encontrado."");</script>", pathArquivoJS)); //Path.GetFileName(NomeArquivoJS)
                }
                else
                {
                    //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptPagina", string.Format(@"<script src=""{0}"" type=""text/javascript""></script>", Page.Request.(pathArquivoJS)));
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptPagina", string.Format(@"<script src=""{0}/{1}"" type=""text/javascript""></script>", PathScripts, Path.GetFileName(NomeArquivoJS)));
                }
            }

            //ASPxGridView grid = Page.FindControl(NomeASPxGridView) as ASPxGridView;
            //if (grid != null)
            //    grid.ClientSideEvents.FocusedRowChanged = "function(s, e) {alert('mudou linha');}";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            EnsureChildControls();
            base.Render(writer);
        }
    }
}
