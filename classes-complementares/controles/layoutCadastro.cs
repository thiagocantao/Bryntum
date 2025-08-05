using DevExpress.Web;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDIS.Web.Controles
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:layoutCadastro runat=server></{0}:layoutCadastro>")]
    public class layoutCadastro : WebControl
    {
        private Utils utils = new Utils();
        private BarraNavegacao barNavegacao = new BarraNavegacao();
        private ASPxGridView gvDados = new ASPxGridView();
        private ASPxPopupControl pcDados = new ASPxPopupControl();

        [Category("Controls")]
        [Description("ASPxGridView que representa a grid de dados")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        //[DesignerSerializationVisibility(DesignerSerializat ionVisibility.Content)]
        [NotifyParentProperty(true)]
        public ASPxGridView _ASPxGridView
        {
            get
            {
                return gvDados;
            }
        }

        [Category("Controls")]
        [Description("ASPxPopupControl que representa o formulário de dados")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        //[DesignerSerializationVisibility(DesignerSerializat ionVisibility.Content)]
        [NotifyParentProperty(true)]
        public ASPxPopupControl _ASPxPopupControl
        {
            get
            {
                return pcDados;
            }
        }

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
                barNavegacao.PathImagesUrl = value;
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
                barNavegacao.PathScripts = value;
            }
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();
            gvDados.ID = "gvDados";
            gvDados.ClientInstanceName = "gvDados";
            pcDados.ID = "pcDados";
            pcDados.ClientInstanceName = "pcDados";

            Controls.Add(utils.getLiteral(string.Format(
                @"<table cellpadding=""0"" cellspacing=""0"">
                    <tr>")));

            Controls.Add(utils.getLiteral(@"<td>"));
            Controls.Add(barNavegacao);
            Controls.Add(utils.getLiteral("</td>"));

            // linha em branco
            Controls.Add(utils.getLiteral("<tr><td>&nbsp;</td></tr>"));

            // ASPxGridView gvDados
            Controls.Add(utils.getLiteral(@"<td>"));
            Controls.Add(gvDados);
            Controls.Add(utils.getLiteral("</td>"));

            // linha em branco
            Controls.Add(utils.getLiteral("<tr><td>&nbsp;</td></tr>"));

            // ASPxPopupControl pcDados
            Controls.Add(utils.getLiteral(@"<td>"));
            Controls.Add(pcDados);
            Controls.Add(utils.getLiteral("</td>"));


            Controls.Add(utils.getLiteral("</tr></table>"));
        }

        protected override void Render(HtmlTextWriter writer)
        {
            EnsureChildControls();
            base.Render(writer);
        }
    }
}
