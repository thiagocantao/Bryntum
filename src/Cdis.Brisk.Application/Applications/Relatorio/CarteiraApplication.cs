using Cdis.Brisk.DataTransfer.Relatorio;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Service.Services.DataBaseObject.Function;
using System.Collections.Generic;

namespace Cdis.Brisk.Application.Applications.Relatorio
{
    public  class CarteiraApplication : IApplication
    {
        #region Properties
            /// <summary>
            /// Propriedade unit of work da classe de aplicação RelatorioPlanoTrabalhoApplication
            /// </summary>
            private readonly UnitOfWorkApplication _unitOfWorkApplication;

            /// <summary>
            /// Propriedade pública da unit of work da classe de aplicação RelatorioPlanoTrabalhoApplication
            /// </summary>
            public UnitOfWorkApplication UowApplication
            {
                get
                {
                    return _unitOfWorkApplication;
                }
            }
        #endregion

        #region Constructor
            /// <summary>
            /// Construtor da classe de aplicação RelatorioPlanoTrabalhoApplication
            /// </summary>
            public CarteiraApplication(UnitOfWorkApplication unitOfWorkApplication)
            {
                _unitOfWorkApplication = unitOfWorkApplication;
            }
        #endregion

        #region Methods
        /// <summary>
        /// GetListUnidadeNegocioDataTransfer
        /// </summary>        
        public List<CarteiraDataTransfer> GetListCarteiraDataTransfer(int codEntidade)
        {
            var listCarteira = UowApplication.UowService.GetUowService<FGetCarteirasEntidadeService>().GetListFGetCarteirasEntidade(codEntidade);

            List<CarteiraDataTransfer> listCarteiraDataTransfer = new List<CarteiraDataTransfer>();

            listCarteiraDataTransfer.Add(new CarteiraDataTransfer
            {
                CodigoCarteira = 0,
                NomeCarteira = "Todas"
            });

            foreach (var carteira in listCarteira)
            {
                listCarteiraDataTransfer.Add(new CarteiraDataTransfer { CodigoCarteira = carteira.CodigoCarteira, NomeCarteira = carteira.NomeCarteira }); 
            }
           
            return listCarteiraDataTransfer;
        }
        #endregion
    }
}
