using Cdis.Brisk.Infra.Core.Extensions;
using CDIS;
using System;
using System.Data;
using System.Web.Services;

/// <summary>
/// Summary description for wsPortal
/// </summary>
[WebService(Namespace = "http://www.cdis.com.br/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class menusAlerta : System.Web.Services.WebService
{

    private string _codigoEntidade;
    /// <summary>
    /// Estilo do Xml da tarea.
    /// </summary>
    public string CodigoEntidade
    {
        get { return _codigoEntidade; }
        set { _codigoEntidade = value; }
    }

    string _key = "#COMANDO#CDIS!";
    private string PathDB = System.Configuration.ConfigurationManager.AppSettings["pathDB"].ToString();
    private string IDProduto = System.Configuration.ConfigurationManager.AppSettings["IDProduto"].ToString();
    private string tipoBancoDados = System.Configuration.ConfigurationManager.AppSettings["tipoBancoDados"].ToString();
    private string Ownerdb = System.Configuration.ConfigurationManager.AppSettings["dbOwner"].ToString();
    ClasseDados classeDados;
    private string bancodb = string.Empty;
    private string ownerdb = string.Empty;

    #region Variáveis Alerta

    public dados cDados;

    public bool existemMensagens = false;
    public bool existemNotificacoes = false;
    public bool existemTarefas = false;
    public string mensagens = "0";
    public string notificacoes = "0";
    public string tarefas = "0";
    private int codigoUsuarioLogado;
    private int codigoEntidadeLogada;
    private string mensagensMenu = "";
    #endregion

    /// <summary>
    /// Buscar os alertas utilizando as chamadas através de consultas SOA
    /// </summary>    
    [WebMethod(EnableSession = true, MessageName = "alertas")]
    public string Alertas()
    {
        AtualizarSessao();
        return (Session["Brisk:Alertas:Json"] == null ? "" : Session["Brisk:Alertas:Json"]).ToString();
    }

    /// <summary>
    /// Atualizar as sessões utilizadas para controlar a busca dos alertas
    /// </summary>
    private void AtualizarSessao()
    {
        string jsonAlertas = GetSituacaoAlerta();
        Session["Brisk:Alertas:Json"] = jsonAlertas;
        Session["Brisk:Alertas:DateTime"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }

    /// <summary>
    /// Montar as string correspondente ao json dos alertas
    /// </summary>    
    private string GetSituacaoAlerta()
    {
        cDados = CdadosUtil.GetCdados(null);
        var codSessionCodigoEntidade = cDados.getInfoSistema("CodigoEntidade");

        if (codSessionCodigoEntidade != null)
        {
            mensagens = "";
            notificacoes = "";
            tarefas = "";
            try
            {
                codigoEntidadeLogada = int.Parse(codSessionCodigoEntidade.ToString());
                codigoUsuarioLogado = int.Parse(cDados.getInfoSistema("IDUsuarioLogado").ToString());
                string texto;
                DataSet ds;
                DataTable dt;
                float total, atrasados;
                ds = cDados.getAtualizacoesPainelAnalista(codigoEntidadeLogada, codigoUsuarioLogado, "", 'L', "AND TerminoRealInformado IS NULL");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    dt = ds.Tables[0];
                    float tarefasTotal, tarefasAtrasadas, indicadoresTotal, indicadoresAtrasados, contratosTotal, contratosAtrasados, toDoListTotal, toDoListAtrasadas;
                    tarefasTotal = float.Parse(dt.Rows[0]["Total"].ToString());
                    tarefasAtrasadas = float.Parse(dt.Rows[0]["Atrasados"].ToString());
                    indicadoresTotal = float.Parse(dt.Rows[1]["Total"].ToString());
                    indicadoresAtrasados = float.Parse(dt.Rows[1]["Atrasados"].ToString());
                    contratosTotal = float.Parse(dt.Rows[2]["Total"].ToString());
                    contratosAtrasados = float.Parse(dt.Rows[2]["Atrasados"].ToString());
                    toDoListTotal = float.Parse(dt.Rows[3]["Total"].ToString());
                    toDoListAtrasadas = float.Parse(dt.Rows[3]["Atrasados"].ToString());

                    texto = "";
                    total = tarefasTotal;
                    atrasados = tarefasAtrasadas;
                    ds = cDados.getParametrosSistema(codigoEntidadeLogada, "ExibeTarefas_an003");
                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                    {
                        //if (ds.Tables[0].Rows[0]["ExibeTarefas_an003"].ToString() == "S")
                        //{
                        DateTime data = DateTime.Now.AddDays(10);
                        total += toDoListTotal;
                        atrasados += toDoListAtrasadas;
                        if (total == 0)
                        {
                            texto = Resources.traducao.menu_tarefas_nao_existem_tarefas;
                        }
                        else
                        {
                            existemTarefas = true;
                            if ((total == 1) && (atrasados == 0))
                            {
                                texto = Resources.traducao.menu_tarefas_voce_tem_n_tarefa;
                            }
                            else if ((total == 1) && (atrasados == 1))
                            {
                                texto = Resources.traducao.menu_tarefas_voce_tem_n_tarefa_sendo_n_atrasado;
                            }
                            else if ((total == 1) && (atrasados > 1))
                            {
                                texto = Resources.traducao.menu_tarefas_voce_tem_n_tarefa_sendo_n_atrasados;
                            }
                            else if ((total > 1) && (atrasados == 0))
                            {
                                texto = Resources.traducao.menu_tarefas_voce_tem_n_tarefas;
                            }
                            else if ((total > 1) && (atrasados == 1))
                            {
                                texto = Resources.traducao.menu_tarefas_voce_tem_n_tarefas_sendo_n_atrasado;
                            }
                            else if ((total > 1) && (atrasados > 1))
                            {
                                texto = Resources.traducao.menu_tarefas_voce_tem_n_tarefas_sendo_n_atrasados;
                            }
                        }
                        //}
                        //else
                        //{
                        //    texto = Resources.traducao.menu_tarefas_acesso_negado;
                        //}
                        if (texto != "")
                        {
                            tarefas += string.Format(@"<li>{0}</li>", string.Format(texto,
                                string.Format(total.ToString(), "n0"),
                                string.Format(atrasados.ToString(), "n0"))
                            );
                        }
                    }
                    texto = "";
                    total = indicadoresTotal;
                    atrasados = indicadoresAtrasados;
                    if ((int)total == 0)
                    {
                        existemNotificacoes = false;
                        texto = Resources.traducao.menu_notificacoes_nao_existem_indicadores;
                    }
                    else
                    {
                        existemNotificacoes = true;
                        if ((total == 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_indicador;
                        }
                        else if ((total == 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_indicador_sendo_n_atrasado;
                        }
                        else if ((total == 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_indicador_sendo_n_atrasados;
                        }
                        else if ((total > 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_indicadores;
                        }
                        else if ((total > 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_indicadores_sendo_n_atrasado;
                        }
                        else if ((total > 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_indicadores_sendo_n_atrasados;
                        }
                    }
                    if (texto != "")
                    {
                        notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                            string.Format(total.ToString(), "n0"),
                            string.Format(atrasados.ToString(), "n0"))
                        );
                    }
                    texto = "";
                    total = contratosTotal;
                    atrasados = contratosAtrasados;
                    double diasParcelas = 0;
                    ds = cDados.getParametrosSistema(codigoEntidadeLogada, "diasParcelasVencendo");
                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["diasParcelasVencendo"].ToString() != "")
                    {
                        diasParcelas = double.Parse(ds.Tables[0].Rows[0]["diasParcelasVencendo"].ToString());
                    }
                    DateTime dataVencimento = DateTime.Now.AddDays(diasParcelas);
                    if ((int)total == 0)
                    {
                        texto = Resources.traducao.menu_notificacoes_nao_existem_parcelas_contratos;
                    }
                    else
                    {
                        existemNotificacoes = true;
                        if ((total == 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_parcela_contrato;
                        }
                        else if ((total == 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_parcela_contrato_sendo_n_atrasado;
                        }
                        else if ((total == 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_parcela_contrato_sendo_n_atrasados;
                        }
                        else if ((total > 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_parcelas_contratos;
                        }
                        else if ((total > 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_parcelas_contratos_sendo_n_atrasado;
                        }
                        else if ((total > 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_parcelas_contratos_sendo_n_atrasados;
                        }
                    }
                    if (texto != "")
                    {
                        notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                            string.Format(dataVencimento.ToString(), Resources.traducao.geral_formato_data_csharp),
                            string.Format(total.ToString(), "n0"),
                            string.Format(atrasados.ToString(), "n0"))
                        );
                    }
                }
                // an_004.aspx.cs
                string formatoTelaPendenciaWorkflows = "";
                ds = cDados.getParametrosSistema(codigoEntidadeLogada, "formatoTelaPendenciaWorkflows");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["formatoTelaPendenciaWorkflows"].ToString() == "SGDA")
                {
                    formatoTelaPendenciaWorkflows = "SGDA";
                }
                ds = cDados.getAprovacoesPainelAnalista(codigoEntidadeLogada, codigoUsuarioLogado, "");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    dt = ds.Tables[0];
                    float tarefasRecursosTotal, workflowTotal, workflowAtrasados;
                    tarefasRecursosTotal = float.Parse(dt.Rows[0]["Total"].ToString());
                    workflowTotal = float.Parse(dt.Rows[1]["Total"].ToString());
                    workflowAtrasados = float.Parse(dt.Rows[1]["Atrasados"].ToString());
                    texto = "";
                    total = tarefasRecursosTotal;
                    if ((int)total == 0)
                    {
                        texto = Resources.traducao.menu_tarefas_nao_existem_tarefas_recursos;
                    }
                    else
                    {
                        existemTarefas = true;
                        if (total == 1)
                        {
                            texto = Resources.traducao.menu_tarefas_voce_tem_n_tarefa_recurso;
                        }
                        else if (total > 1)
                        {
                            texto = Resources.traducao.menu_tarefas_voce_tem_n_tarefas_recursos;
                        }
                    }
                    if (texto != "")
                    {
                        tarefas += string.Format(@"<li>{0}</li>", string.Format(texto,
                            string.Format(total.ToString(), "n0"))
                        );
                    }
                    texto = "";
                    total = workflowTotal;
                    atrasados = workflowAtrasados;
                    if ((int)total == 0)
                    {
                        texto = Resources.traducao.menu_notificacoes_nao_existem_pendencias_workflow;
                    }
                    else
                    {
                        existemNotificacoes = true;
                        if ((total == 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_pendencia_workflow;
                        }
                        else if ((total == 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_pendencia_workflow_sendo_n_atrasado;
                        }
                        else if ((total == 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_pendencia_workflow_sendo_n_atrasados;
                        }
                        else if ((total > 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_pendencias_workflow;
                        }
                        else if ((total > 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_pendencias_workflow_sendo_n_atrasado;
                        }
                        else if ((total > 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_pendencias_workflow_sendo_n_atrasados;
                        }
                    }
                    if (texto != "")
                    {
                        notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                            string.Format(total.ToString(), "n0"),
                            string.Format(atrasados.ToString(), "n0"),
                            formatoTelaPendenciaWorkflows)
                        );
                    }
                }
                // an_005.aspx.cs

                int quantidadeDiasAlertaContratos = 60;

                ds = cDados.getParametrosSistema("QuantidadeDiasAlertaContratosVencimento");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["QuantidadeDiasAlertaContratosVencimento"] + "" != "")
                {
                    quantidadeDiasAlertaContratos = int.Parse(ds.Tables[0].Rows[0]["QuantidadeDiasAlertaContratosVencimento"] + "");
                }
                ds = cDados.getGestaoPainelAnalista(codigoEntidadeLogada, codigoUsuarioLogado, quantidadeDiasAlertaContratos, "");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    dt = ds.Tables[0];
                    float riscosTotal, riscosAtrasados, issuesTotal, issuesAtrasadas, contratosTotal, contratosVencidos;
                    riscosTotal = float.Parse(dt.Rows[0]["Total"].ToString());
                    riscosAtrasados = float.Parse(dt.Rows[0]["Atrasados"].ToString());
                    issuesTotal = float.Parse(dt.Rows[1]["Total"].ToString());
                    issuesAtrasadas = float.Parse(dt.Rows[1]["Atrasados"].ToString());
                    contratosTotal = float.Parse(dt.Rows[2]["Total"].ToString());
                    contratosVencidos = float.Parse(dt.Rows[2]["Atrasados"].ToString());

                    texto = "";
                    total = riscosTotal;
                    atrasados = riscosAtrasados;
                    if ((int)total == 0)
                    {
                        texto = Resources.traducao.menu_notificacoes_nao_existem_riscos;
                    }
                    else
                    {
                        existemNotificacoes = true;
                        if ((total == 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_risco;
                        }
                        else if ((total == 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_risco_sendo_n_atrasado;
                        }
                        else if ((total == 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_risco_sendo_n_atrasados;
                        }
                        else if ((total > 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_riscos;
                        }
                        else if ((total > 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_riscos_sendo_n_atrasado;
                        }
                        else if ((total > 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_riscos_sendo_n_atrasados;
                        }
                    }
                    if (texto != "")
                    {
                        notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                            string.Format(total.ToString(), "n0"),
                            string.Format(atrasados.ToString(), "n0"))
                        );
                    }

                    texto = "";
                    total = issuesTotal;
                    atrasados = issuesAtrasadas;
                    string definicaoQuestao = "Questão", definicaoQuestaoPlural = "questões ativas", definicaoQuestaoSingular = "questão ativa";
                    string labelCriticaSingular = "crítica", labelCriticaPlural = "críticas";
                    DataSet dsParametros = cDados.getParametrosSistema("labelQuestao", "labelQuestoes", "lblGeneroLabelQuestao");
                    if (cDados.DataSetOk(dsParametros) && cDados.DataTableOk(dsParametros.Tables[0]) && dsParametros.Tables[0].Rows[0]["labelQuestao"] + "" != "")
                    {
                        string genero = dsParametros.Tables[0].Rows[0]["lblGeneroLabelQuestao"] + "";
                        definicaoQuestao = dsParametros.Tables[0].Rows[0]["labelQuestao"] + "";
                        definicaoQuestaoPlural = dsParametros.Tables[0].Rows[0]["labelQuestoes"] + "";
                        definicaoQuestaoSingular = string.Format(@"{0} ativ{1}", definicaoQuestao, genero == "M" ? "o" : "a");
                        labelCriticaSingular = string.Format(@"crític{0}", genero == "M" ? "o" : "a");
                        labelCriticaPlural = string.Format(@"crític{0}", genero == "M" ? "os" : "as");
                    }
                    if ((int)total == 0)
                    {
                        texto = Resources.traducao.menu_notificacoes_nao_existem_questoes;
                    }
                    else
                    {
                        existemNotificacoes = true;
                        if ((total == 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_questao;
                        }
                        else if ((total == 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_questao_sendo_n_atrasado;
                        }
                        else if ((total == 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_questao_sendo_n_atrasados;
                        }
                        else if ((total > 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_questoes;
                        }
                        else if ((total > 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_questoes_sendo_n_atrasado;
                        }
                        else if ((total > 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_questoes_sendo_n_atrasados;
                        }
                    }
                    if (texto != "")
                    {
                        notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                            string.Format(total.ToString(), "n0"),
                            string.Format(atrasados.ToString(), "n0"))
                        );
                    }
                    texto = "";
                    total = contratosTotal;
                    atrasados = contratosVencidos;
                    string varLigacao = "e";
                    string varUsaContratoEstendido = "N";
                    ds = cDados.getParametrosSistema(codigoEntidadeLogada, "UtilizaContratosExtendidos");
                    if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]) && ds.Tables[0].Rows[0]["UtilizaContratosExtendidos"].ToString() == "S")
                    {
                        varUsaContratoEstendido = "S";
                    }
                    if ((int)total == 0)
                    {
                        texto = Resources.traducao.menu_notificacoes_nao_existem_contratos;
                        //texto = "<li><a href=\"" + Session["baseUrl"] + "taskboard/TaskboardWrap.aspx?TITULO=Kanban\" \">" + Resources.traducao.menu_notificacoes_nao_existem_contratos + "</a><li>";
                    }
                    else
                    {
                        existemNotificacoes = true;
                        if ((total == 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_contrato;
                        }
                        else if ((total == 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_contrato_sendo_n_atrasado;
                        }
                        else if ((total == 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_contrato_sendo_n_atrasados;
                        }
                        else if ((total > 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_contratos;
                        }
                        else if ((total > 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_contratos_sendo_n_atrasado;
                        }
                        else if ((total > 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_contratos_sendo_n_atrasados;
                        }
                    }
                    if (texto != "")
                    {
                        notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                            quantidadeDiasAlertaContratos,
                            string.Format(total.ToString(), "n0"),
                            string.Format(atrasados.ToString(), "n0"),
                            varUsaContratoEstendido)
                        );
                    }
                }

                ds = cDados.getComunicacaoPainelAnalista(codigoEntidadeLogada, codigoUsuarioLogado, "");
                if (cDados.DataSetOk(ds) && cDados.DataTableOk(ds.Tables[0]))
                {
                    dt = ds.Tables[0];
                    float compromissosTotal, compromissosProximosDias, mensagensTotal, mensagensAtrasadas;
                    compromissosTotal = float.Parse(dt.Rows[0]["Total"].ToString());
                    compromissosProximosDias = float.Parse(dt.Rows[0]["Atrasados"].ToString());
                    mensagensTotal = float.Parse(dt.Rows[1]["Total"].ToString());
                    mensagensAtrasadas = float.Parse(dt.Rows[1]["Atrasados"].ToString());

                    texto = "";
                    total = compromissosTotal;
                    atrasados = compromissosProximosDias;
                    if ((int)total == 0)
                    {
                        texto = Resources.traducao.menu_notificacoes_nao_existem_compromissos;
                    }
                    else
                    {
                        existemNotificacoes = true;
                        if ((total == 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_compromisso;
                        }
                        else if ((total == 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_compromisso_sendo_n_atrasado;
                        }
                        else if ((total == 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_compromisso_sendo_n_atrasados;
                        }
                        else if ((total > 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_compromissos;
                        }
                        else if ((total > 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_compromissos_sendo_n_atrasado;
                        }
                        else if ((total > 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_notificacoes_voce_tem_n_compromissos_sendo_n_atrasados;
                        }
                    }
                    if (texto != "")
                    {
                        notificacoes += string.Format(@"<li>{0}</li>", string.Format(texto,
                            string.Format(total.ToString(), "n0"),
                            string.Format(atrasados.ToString(), "n0"),
                            10)
                        );
                    }
                    texto = "";
                    total = mensagensTotal;
                    atrasados = mensagensAtrasadas;
                    if ((int)total == 0)
                    {
                        //texto = Resources.traducao.menu_mensagens_nao_existem_mensagens;

                        texto = "<li><a href=\"" + Session["baseUrl"] + "/Mensagens/PainelMensagens.aspx?TITULO=Messages\" \">" + Resources.traducao.menu_mensagens_nao_existem_mensagens + "</a><li>";
                    }
                    else
                    {
                        existemMensagens = true;
                        if ((total == 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_mensagens_voce_tem_n_mensagem;
                        }
                        else if ((total == 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_mensagens_voce_tem_n_mensagem_sendo_n_atrasado;
                        }
                        else if ((total == 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_mensagens_voce_tem_n_mensagem_sendo_n_atrasados;
                        }
                        else if ((total > 1) && (atrasados == 0))
                        {
                            texto = Resources.traducao.menu_mensagens_voce_tem_n_mensagens;
                        }
                        else if ((total > 1) && (atrasados == 1))
                        {
                            texto = Resources.traducao.menu_mensagens_voce_tem_n_mensagens_sendo_n_atrasado;
                        }
                        else if ((total > 1) && (atrasados > 1))
                        {
                            texto = Resources.traducao.menu_mensagens_voce_tem_n_mensagens_sendo_n_atrasados;
                        }
                    }
                    if (texto != "")
                    {
                        mensagensMenu += string.Format(@"<li>{0}</li>", string.Format(texto,
                            string.Format(total.ToString(), "n0"))
                        );
                    }
                }

                if (existemNotificacoes == false)
                {
                    notificacoes = Resources.traducao.novaCdis_visualize_aqui_a_lista_de_notifica__es;
                }
                if (existemTarefas == false)
                {
                    tarefas = "<li><a href=\"" + Session["baseUrl"] + "/taskboard/TaskboardWrap.aspx?TITULO=Kanban\" \">" + Resources.traducao.menu_tarefas_nao_existem_tarefas + "</a><li>";
                }
                if (existemMensagens == false)
                {
                    mensagensMenu = "<li><a href=\"" + Session["baseUrl"] + "/Mensagens/PainelMensagens.aspx?TITULO=Messages\" \">" + Resources.traducao.menu_mensagens_nao_existem_mensagens + "</a><li>";
                }

            }
            catch (Exception ex)
            {

            }
            return (new { existemMensagens, existemNotificacoes, existemTarefas, mensagensMenu, notificacoes, tarefas }).ToJson();
        }
        else
        {
            return string.Empty;
        }
    }

}