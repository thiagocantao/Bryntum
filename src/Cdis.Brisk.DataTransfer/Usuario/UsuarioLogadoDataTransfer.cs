namespace Cdis.Brisk.DataTransfer.Usuario
{
    public enum UserBrowser
    {
        InternetExplorer = 1,
        Chrome = 2,
        Firefox = 3,
        Others = 4
    }
    /// <summary>
    /// Objeto do usuário logado
    /// </summary>
    public class UserDataTransfer
    {
        public int Id { get; set; }
        public int CodigoEntidade { get; set; }
        public int CodigoPortfolio { get; set; }
        public int CodigoCarteira { get; set; }
        public string Nome { get; set; }
        public UserBrowser Browser { get; set; }
    }
}
