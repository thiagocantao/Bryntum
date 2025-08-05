<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="FrameAtualizacao.aspx.cs" Inherits="Tarefas_Aprovacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        var tarefaEdicao;
        var estadoAnterior = '';
        function executaAcaoMovimento(drop, task, eOpts) {
            tarefaEdicao = task[0].data;
            if (estadoAnterior != tarefaEdicao.State) {
                callbackStatus.PerformCallback(tarefaEdicao.Id + ';' + tarefaEdicao.State);
            }           
        }

        function executaAcaoEdicao(task) {
            tarefaEdicao = task;
            var readOnly = task.State == 'EE' ? 'N' : 'S';
            if(task.IndicaCorDiferente == 1)
                window.top.showModal('./DetalhesTDL.aspx?CTDL=' + task.Id + '&RO=' + readOnly, 'Detalhes', 980, 420, finalizaEdicao, null);    
            else
                window.top.showModal('./DetalhesTS.aspx?CA=' + task.Id + '&RO=' + readOnly, 'Detalhes', 810, 550, finalizaEdicao, null);    
        }

        function executaAcaoConsulta(task) {
            tarefaEdicao = task;
            var readOnly = 'S';
            if (task.IndicaCorDiferente == 1)
                window.top.showModal('./DetalhesTDL.aspx?CTDL=' + task.Id + '&RO=' + readOnly, 'Detalhes', 980, 420, finalizaEdicao, null);
            else
                window.top.showModal('./DetalhesTS.aspx?CA=' + task.Id + '&RO=' + readOnly, 'Detalhes', 810, 550, finalizaEdicao, null);    
        }

        function abreComentarios(task) {
            tarefaEdicao = task;
            window.top.showModal('./ComentariosTarefa.aspx?CA=' + task.Id, 'Editar Comentários', 950, 485, '', null);
        }

        function abreAnexos(task) {
            var urlAnexo = "../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&TA=TC&ID=" + task.Id + "&ALT=392";
            window.top.showModal(urlAnexo, 'Anexos', 860, 435, '', null);
        }

        function finalizaEdicao(retorno) {
            if (retorno == 'S')
                callbackStatus.PerformCallback(tarefaEdicao.Id + ';');
        }

        function eventoSelect(task) {
            task.deselectAll();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxcb:ASPxCallback ID="callbackStatus" runat="server" 
            ClientInstanceName="callbackStatus" oncallback="callbackStatus_Callback">
            <ClientSideEvents EndCallback="function(s, e) {

    if(s.cp_AtualizaTela == 'S')
    {
        tarefaEdicao.IndicaCorBorda = s.cp_IndicaCorBorda;
        tarefaEdicao.StatusAux = s.cp_StatusAux;
        tarefaEdicao.ColunaValorAux = s.cp_ColunaValorAux;
        taskBoard.refresh();
    }
}" />
        </dxcb:ASPxCallback>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        if(window.parent.lpCarregando)
            window.parent.lpCarregando.Hide();
    </script>
</body>
</html>
