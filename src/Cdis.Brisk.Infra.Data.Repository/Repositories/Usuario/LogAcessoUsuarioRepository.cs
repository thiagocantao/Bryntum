using Cdis.Brisk.Domain.Domains.Entities;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Infra.Data.RepositoryBase;
using System.Data.Entity;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.Usuario
{
    /// <summary>
    /// Classe de repositório LogAcessoUsuarioRepository
    /// </summary>
    public class LogAcessoUsuarioRepository : RepositoryBaseCommand<LogAcessoUsuarioDomain>, IRepository
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório LogAcessoUsuarioRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório LogAcessoUsuarioRepository
        /// </summary>
        private UnitOfWorkRepository<DomainContext> UowRepository
        {
            get
            {
                return _unitOfWorkRepository;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor da classe de repositório LogAcessoUsuarioRepository
        /// </summary>
        public LogAcessoUsuarioRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserir na tabela de log.        
        /// </summary>
        public void InsertLogAcessoUsuario(LogAcessoUsuarioDomain logAcesso)
        {
            string query = string.Format(@"INSERT INTO [{0}].[{1}].[LogAcessoUsuario]
                                   ([CodigoUsuario]
                                   ,[DataAcesso]
                                   ,[CodigoEntidade]
                                   ,[TipoAcesso]
                                   ,[ObjetoAcesso]
                                   ,[SituacaoAcesso]
                                   ,[EmailSemCadastro])
                             VALUES
                                   ({2}
                                   ,GETDATE()
                                   ,{3}
                                   ,{4}
                                   ,{5}
                                   ,{6}
                                   ,{7})",
                                    Core.Data.DatabaseInfo.GetDatabaseNameSqlServer(this.UowRepository.GetStringConnection()),
                                    Core.Data.DatabaseInfo.GetOwnerBb(),
                                    logAcesso.CodigoUsuario.HasValue ? logAcesso.CodigoUsuario.Value.ToString() : "NULL",
                                    logAcesso.CodigoEntidade.HasValue ? logAcesso.CodigoEntidade.Value.ToString() : "NULL",
                                    logAcesso.TipoAcesso != null && logAcesso.TipoAcesso.Length > 0 ? "'" + logAcesso.TipoAcesso + "'" : "NULL",
                                    logAcesso.ObjetoAcesso != null && logAcesso.ObjetoAcesso.Length > 0 ? "'" + logAcesso.ObjetoAcesso + "'" : "NULL",
                                    logAcesso.SituacaoAcesso != null && logAcesso.SituacaoAcesso.Length > 0 ? "'" + logAcesso.SituacaoAcesso + "'" : "NULL",
                                    logAcesso.EmailSemCadastro != null && logAcesso.EmailSemCadastro.Length > 0 ? "'" + logAcesso.EmailSemCadastro + "'" : "NULL"
                                    );

            this.ExecuteSqlCommand(query);
        }
        #endregion
    }
}
