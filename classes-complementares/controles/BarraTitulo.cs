using DevExpress.Web;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDIS.Web.Controles
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BarraTitulo runat=server></{0}:BarraTitulo>")]
    public class BarraTitulo : WebControl, INamingContainer
    {
        private Utils utils = new Utils();
        private ASPxLabel lblTitulo = new ASPxLabel();

        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }


        [Category("Controls")]
        [Description("ASPxLabel que representa o texto a ser apresentado como título")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        //[DesignerSerializationVisibility(DesignerSerializat ionVisibility.Content)]
        [NotifyParentProperty(true)]
        public ASPxLabel Titulo
        {
            get
            {
                return lblTitulo;
            }
        }

        /* [Bindable(true)]
         [Category("Appearance")]
         [DefaultValue("")]
         [Localizable(true)]
         public string Text
         {
             get
             {
                 String s = (String)ViewState["Text"];
                 return ((s == null) ? String.Empty : s);
             }

             set
             {
                 ViewState["Text"] = value;
             }
         }
         */
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

        protected override void CreateChildControls()
        {
            //EnsureChildControls();
            Controls.Clear();

            //lblTitulo.Text = this.Text;
            string stiloBordas = string.Format(@"style=""width:100%; background-image: url({0}/barraTitulo_Fundo.png); background-repeat: repeat-x"" ", PathImagesUrl);

            Controls.Add(utils.getLiteral(string.Format(
                @"<table cellpadding=""0"" cellspacing=""0"" {0}>
                    <tr style=""height:26px"">", stiloBordas)));

            Controls.Add(utils.getLiteral(@"<td style=""padding-left:5px"">"));
            Controls.Add(lblTitulo);
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
