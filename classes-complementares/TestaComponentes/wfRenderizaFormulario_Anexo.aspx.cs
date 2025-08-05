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
using CDIS;
using System.IO;

public partial class wfRenderizaFormulario_Anexo : System.Web.UI.Page
{
    dados cDados = new dados();
    private Int64 tamanhoMaximoArquivoUpload;
    int codigoUsuarioResponsavel;
    int codigoEntidadeUsuarioResponsavel;
    int codigoTipoAssociacao;
    int IDObjetoAssociado;

    protected void Page_Load(object sender, EventArgs e)
    {
        codigoUsuarioResponsavel = int.Parse(Request.QueryString["US"]);
        codigoEntidadeUsuarioResponsavel = int.Parse(Request.QueryString["CE"]);
        codigoTipoAssociacao = cDados.getCodigoTipoAssociacao("FO");
        IDObjetoAssociado = int.Parse(Request.QueryString["CF"]);

        tamanhoMaximoArquivoUpload = 2; // inicialmente é 2 megas
        DataSet ds = cDados.getParametrosSistema(codigoEntidadeUsuarioResponsavel, "tamanhoMaximoArquivoAnexoEmMegaBytes");
        if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["tamanhoMaximoArquivoAnexoEmMegaBytes"].ToString() != "")
            tamanhoMaximoArquivoUpload = int.Parse(ds.Tables[0].Rows[0]["tamanhoMaximoArquivoAnexoEmMegaBytes"].ToString());

    }

    protected void btnSalvarNovoAnexo_Click(object sender, EventArgs e)
    {
        if (incluiNovoAnexo() == "")
            hfGeral.Value = "OK";
    }

    private string incluiNovoAnexo()
    {
        try
        {
            // VERIFICA SE O USUÁRIO SELECIONOU ALGUM ARQUIVO
            if (fluArquivo.HasFile)
            {
                string nomeNovoAnexo = Path.GetFileName(fluArquivo.FileName);
                string descricaoNovoAnexo = txtDescricaoNovoAnexo.Text.Replace("'", "");
                int tamanhoImagem = fluArquivo.PostedFile.ContentLength;

                // VERIFICA A EXTENSÃO DO ARQUIVO.
                string extensao = Path.GetExtension(nomeNovoAnexo).ToLower();
                if (extensao == ".exe" || extensao == ".com" || extensao == ".dll")
                    return "O tipo do arquivo não pode ser anexado.";

                // BLOQUEIA A TRANSFERÊNCIA DE ARQUIVOS MAIOR QUE O LIMITE PERMITIDO - VER PARAMETRO
                if (tamanhoImagem > tamanhoMaximoArquivoUpload * (1024 * 1024))
                    return string.Format("Limite máximo para arquivo é de {0} MB", tamanhoMaximoArquivoUpload);

                //RECEBE O ARQUIVO COLOCANDO-O NA MEMÓRIA
                Stream imagem = fluArquivo.PostedFile.InputStream;
                byte[] imagemBinario = new byte[tamanhoImagem];
                int n = imagem.Read(imagemBinario, 0, tamanhoImagem);

                // insere o arquivo no banco de dados
                string mensagem = cDados.incluirAnexo(descricaoNovoAnexo, codigoUsuarioResponsavel.ToString(), nomeNovoAnexo, codigoEntidadeUsuarioResponsavel.ToString(), null, 'N', 'N', codigoTipoAssociacao, IDObjetoAssociado.ToString(), imagemBinario);
                if (mensagem != "")
                    return mensagem;
            }
            else
                return "O arquivo não foi informado";

            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

}
