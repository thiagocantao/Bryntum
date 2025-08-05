namespace Cdis.Brisk.DataTransfer.Cronograma
{
    public class InfoCronogramaDataTransfer
    {
        //1 - Verificar se o cronograma está bloquado
        //2 - Definir mensagem para o cronograma bloqueado
        //3 - Bloquear ou ocultar botão desbloquear caso o cronograma não esteja bloqueado (Tela, usar IsCronogramaBloqueado)
        //4 - Montar parametros para a tela do clickOnce (Usar LinkTasques)
        //5 - Ao clicar em editar, abrir a tela do clickOnce (Usar LinkTasques)
        public bool IsCronogramaBloqueado { get; set; }
        public string MensagemBloqueio { get; set; }
        public string DescDownloadClickOnce { get; set; }
        public string LinkDownloadClickOnce { get; set; }
        public bool HasDescDownloadOnce { get { return !string.IsNullOrEmpty(DescDownloadClickOnce); } }
        public string LinkTasques { get; set; }
        public bool HasLinkDownloadClickOnce { get { return !string.IsNullOrEmpty(LinkDownloadClickOnce); } }
    }
}
