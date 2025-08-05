using Cdis.Brisk.DataTransfer.Cronograma;
using Cdis.Brisk.DataTransfer.Usuario;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Service.Services.Cronograma;
using Cdis.Brisk.Service.Services.DataBaseObject.Function;
using Cdis.Brisk.Service.Services.DataBaseObject.StoredProcedure;
using Cdis.Brisk.Service.Services.Parametro;
using Cdis.Brisk.Service.Services.Projeto;
using Cdis.Brisk.Service.Services.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Application.Applications.Cronograma
{
    /// <summary>
    /// Classe de aplicação CronogramaProjetoApplication
    /// </summary>
    public class CronogramaProjetoApplication : IApplication
    {
        #region Properties

        /// <summary>
        /// Propriedade unit of work da classe de aplicação CronogramaProjetoApplication
        /// </summary>
        private readonly UnitOfWorkApplication _unitOfWorkApplication;

        /// <summary>
        /// Propriedade pública da unit of work da classe de aplicação CronogramaProjetoApplication
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
        /// Construtor da classe de aplicação CronogramaProjetoApplication
        /// </summary>
        public CronogramaProjetoApplication(UnitOfWorkApplication unitOfWorkApplication)
        {
            _unitOfWorkApplication = unitOfWorkApplication;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Buscar as informações sobre a EAP
        /// </summary>        
        public InfoEapDataTransfer GetInfoEapDataTransfer(int codProjeto, int codEntidade, int idUsuarioLogado)
        {
            var listParametro = UowApplication.UowService.GetUowService<ParametroConfiguracaoSistemaService>().GetListParametroEntidade(codEntidade, "utilizaCronoInstalado", "utilizaNovaEAP");
            var infoEapDataTransfer = new InfoEapDataTransfer
            {
                IdUsuarioLogado = idUsuarioLogado,
                CodEntidade = codEntidade,
                CodProjeto = codProjeto
            };

            if (listParametro.Any(p => p.Parametro == "utilizaNovaEAP"))
            {
                var eapInfoDomain = UowApplication.UowService.GetUowService<PEapBuscaCodigoEapService>().GetEapBuscaCodigoEap(codProjeto);

                if (eapInfoDomain == null)
                {
                    infoEapDataTransfer.IsEdicaoEAP = true;
                }
                else
                {
                    infoEapDataTransfer.CronogramaReplanejamento = eapInfoDomain.CronogramaReplanejamento;
                    infoEapDataTransfer.CronogramaOficial = eapInfoDomain.CronogramaOficial;
                    infoEapDataTransfer.EAPBloqueadaEm = eapInfoDomain.EAPBloqueadaEm;
                    infoEapDataTransfer.EAPBloqueadaPor = eapInfoDomain.EAPBloqueadaPor;
                    infoEapDataTransfer.IndicaEAPEditavel = eapInfoDomain.IndicaEAPEditavel;
                    //todo -> descobrir o que significa e onde é usado esse -1;
                    if (string.IsNullOrEmpty(eapInfoDomain.CronogramaReplanejamento) && string.IsNullOrEmpty(eapInfoDomain.CronogramaOficial))
                    {
                        eapInfoDomain.CronogramaReplanejamento = "-1";
                    }
                }
            }
            else
            {
                var listDataUltimaGravacao = UowApplication.UowService.GetUowService<CronogramaProjetoService>().GetListDataUltimaGravacaoDesktopProjeto(codEntidade);
                string indicaEap = listDataUltimaGravacao.Any() ? "N" : "S";
                infoEapDataTransfer.IndicaEAPEditavel = indicaEap;

                //todo -> descobrir o que significa e onde é usado esse -1;
                infoEapDataTransfer.CronogramaReplanejamento = "-1";
            }

            infoEapDataTransfer.BaseUrl = GetBaseUrlEAP(infoEapDataTransfer);

            return infoEapDataTransfer;
        }

        /// <summary>
        /// Buscar Url base para abrir o popUp da EAP.
        /// </summary>        
        public string GetBaseUrlEAP(InfoEapDataTransfer info)
        {
            string codigoCrono = info.IsEdicaoEAP ? info.CronogramaReplanejamento : info.CronogramaOficial;

            string pageUrl = info.IsUtilizaNovaEAP
                ? "../../../../_Projetos/DadosProjeto/EdicaoEAP.aspx"
                : "../../../../GoJs/EAP/graficoEAP.aspx";

            string nomeProjeto = UowApplication.UowService.GetUowService<ProjetoService>().GetNomeProjeto(info.CodProjeto);
            string baseUrl = pageUrl + "?IDProjeto=" + info.CodProjeto + "&CCR=" + codigoCrono + "&CU=" + info.IdUsuarioLogado + "&CE=" + info.CodEntidade + "&NP=" + nomeProjeto;

            return baseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        public InfoCronogramaDataTransfer GetInfoCronogramaDataTransfer(UserDataTransfer usuario, int codProjeto, Type typeResourceTraducao, int controleLocal)
        {
            var listResourceTraducao = Cdis.Brisk.Infra.Core.Util.ResourceUtil.GetListResourceItem(typeResourceTraducao, new List<string>
                {
                    "Cronograma_gantt_o_cronograma_est__bloqueado_com_o_usu_rio_",
                    "Cronograma_gantt_ao_fazer_o_desbloqueio_as_atualiza__es_pendentes_ser_o_perdidas__deseja_realmente_desbloquear_o_projeto_",
                    "DescricaoDownloadClickOnce"
                }
            );

            var cronograma = UowApplication.UowService.GetUowService<CronogramaProjetoService>().GetCronogramaProjetoBloqueadoParaEdicao(usuario.CodigoEntidade, codProjeto);
            string nomeUsuarioBloqueio = cronograma == null ? "" : UowApplication.UowService.GetUowService<UsuarioService>().GetUsuario(cronograma.CodigoUsuarioCheckoutCronograma.Value).NomeUsuario;

            string msgBloqueio = cronograma == null ? "" : string.Format("{0} {1} {2}",
                    listResourceTraducao.FirstOrDefault(i => i.Key == "Cronograma_gantt_o_cronograma_est__bloqueado_com_o_usu_rio_").Text,
                    nomeUsuarioBloqueio,
                    listResourceTraducao.LastOrDefault(i => i.Key == "Cronograma_gantt_ao_fazer_o_desbloqueio_as_atualiza__es_pendentes_ser_o_perdidas__deseja_realmente_desbloquear_o_projeto_").Text);

            string strLinkDownloadClickOnce = "";
            switch (usuario.Browser)
            {
                case UserBrowser.InternetExplorer:
                    strLinkDownloadClickOnce = "";
                    break;
                case UserBrowser.Chrome:
                    strLinkDownloadClickOnce = "https://chrome.google.com/webstore/detail/clickonce-for-google-chro/kekahkplibinaibelipdcikofmedafmb";
                    break;
                case UserBrowser.Firefox:
                    strLinkDownloadClickOnce = "https://addons.mozilla.org/pt-BR/firefox/addon/meta4clickoncelauncher/?src=search";
                    break;
                case UserBrowser.Others:
                    strLinkDownloadClickOnce = "";
                    break;
                default:
                    strLinkDownloadClickOnce = "";
                    break;
            }

            bool isBloqueadoParaUsuarioAtual = (cronograma != null && cronograma.CodigoUsuarioCheckoutCronograma != usuario.Id);

            InfoCronogramaDataTransfer info = new InfoCronogramaDataTransfer
            {
                MensagemBloqueio = msgBloqueio,
                IsCronogramaBloqueado = isBloqueadoParaUsuarioAtual,
                LinkTasques = "../../../../" + string.Format("tasques/tasques.application?En={0}&Us={1}&Pr={2}&Ct={3}&Sl={4}", usuario.CodigoEntidade, usuario.Id, codProjeto, controleLocal, isBloqueadoParaUsuarioAtual ? "S" : "N"),//; "", // todo-> migração cdados
                DescDownloadClickOnce = listResourceTraducao.FirstOrDefault(i => i.Key == "DescricaoDownloadClickOnce").Text,
                LinkDownloadClickOnce = strLinkDownloadClickOnce
            };

            return info;
        }

        /// <summary>
        /// Listar os números das linhas de base do projeto.
        /// </summary>        
        public List<LinhaBaseDataTransfer> GetListNumLinhaBase(int codProjeto)
        {
            return UowApplication.UowService
                     .GetUowService<FCronoGetVersoesLbProjetoService>()
                     .GetListFCronoGetVersoesLbProjeto(codProjeto)
                     .Select(l => new LinhaBaseDataTransfer
                     {
                         NumVersao = Convert.ToInt16(l.NumeroVersao),
                         NumLinhaBase = Convert.ToInt16(l.VersaoLinhaBase),
                         Situacao = l.Situacao,
                         Anotacao = l.Anotacoes.Replace("\n", "\\n"),
                         NomeAprovador = l.NomeAprovador,
                         DataAprovacao = l.DataStatusAprovacao.HasValue ? l.DataStatusAprovacao.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
                         DataSolicitacao = l.DataSolicitacao.HasValue ? l.DataSolicitacao.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
                         NomeSolicitante = l.NomeSolicitante
                     })
                     .OrderByDescending(l => l.NumLinhaBase)
                     .ToList();
        }
        #endregion
    }
}
