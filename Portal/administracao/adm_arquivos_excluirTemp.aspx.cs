using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administracao_adm_arquivos_excluirTemp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        dados cDados;
        cDados = CdadosUtil.GetCdados(null);
        cDados.apagaArquivosTemporarios();
    }
}