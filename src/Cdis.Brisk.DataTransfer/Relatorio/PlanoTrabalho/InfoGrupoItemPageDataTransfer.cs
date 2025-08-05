using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.DataTransfer.Relatorio.PlanoTrabalho
{
    public class InfoGrupoItemPageDataTransfer
    {
        public InfoItemPageDataTranfer this[string name] { get { return ListItemPage.Any() ? ListItemPage.FirstOrDefault(i => i.NomeItem == name) : null; } }
        public List<InfoItemPageDataTranfer> ListItemPage { get; private set; }

        public InfoGrupoItemPageDataTransfer(List<InfoItemPageDataTranfer> listItemPage)
        {
            ListItemPage = listItemPage;
        }
    }
}
