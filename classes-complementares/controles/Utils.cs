using System.Web.UI.WebControls;

namespace CDIS.Web.Controles
{
    class Utils
    {
        public Literal getLiteral(string texto)
        {
            Literal myLiteral = new Literal();
            myLiteral.Text = texto;
            return myLiteral;
        }
    }
}
