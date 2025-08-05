using Cdis.Brisk.Domain.Domains.DataBaseObject.StoredProcedure.Param;
using Cdis.Brisk.Infra.Core.Interface;
using System.Data.Entity;

namespace Cdis.Brisk.Infra.Data.Repository.Repositories.DataBaseObject.StoredProcedure
{
    public class PEnviaEmailPortalRepository : RepositoryBase.RepositoryBase, IRepository
    {   
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de repositório ConexaoContratoRepository
        /// </summary>
        private readonly UnitOfWorkRepository<DomainContext> _unitOfWorkRepository;

        /// <summary>
        /// Propriedade pública da unit of work da classe de repositório ConexaoContratoRepository
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
        /// Construtor da classe de repositório CategoriaRepository
        /// </summary>
        public PEnviaEmailPortalRepository(DbContext contexto, UnitOfWorkRepository<DomainContext> unitOfWork)
            : base(contexto)
        {
            _unitOfWorkRepository = unitOfWork;
        }
        #endregion


        #region Methods

        /// <summary>
        /// Executar a stored procedure p_EnviaEmailPortal
        /// </summary>        
        public object ExecStoredProcedure(ParamEnviaEmail param)
        {

            string query = string.Format("exec p_EnviaEmailPortal '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                param.CodigoEntidadeMaster,
                param.EmailDestinatario,
                param.EmailCopia,
                param.AssuntoEmail,
                param.Body,
                param.Convite,
                param.Anexo);

            return this.ExecSqlQuery(query);
        }

        #endregion


    }
}
