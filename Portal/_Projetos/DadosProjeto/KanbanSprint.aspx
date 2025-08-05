<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="KanbanSprint.aspx.cs" Inherits="Tarefas_Aprovacao" %>

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
                callbackStatus.PerformCallback(tarefaEdicao.Id + ';' + tarefaEdicao.State + ";" + estadoAnterior);  
            }           
        }

        function executaAcaoEdicao(task) {
             
        }

        function executaAcaoConsulta(task) {
            tarefaEdicao = task;
            var readOnly = 'S';
            window.top.showModal('./DetalhesItemSprint.aspx?CI=' + task.Id + '&RO=' + readOnly, 'Detalhes do Item', 850, 300, finalizaEdicao, null);    
        }

        function abreComentarios(task) {
            
        }

        function abreAnexos(task) {
           
        }

        function finalizaEdicao(retorno) {
//            if (retorno == 'S')
//                callbackStatus.PerformCallback(tarefaEdicao.Id + ';');
        }

        function eventoSelect(task) {
            task.deselectAll();
        }

        var refreshinterval = 300;
        var starttime;
        var nowtime;
        var reloadseconds = 0;
        var secondssinceloaded = 0;

        function starttime() {
            starttime = new Date();
            starttime = starttime.getTime();
            countdown();
        }

        function countdown() {
            nowtime = new Date();
            nowtime = nowtime.getTime();
            secondssinceloaded = (nowtime - starttime) / 1000;
            reloadseconds = Math.round(refreshinterval - secondssinceloaded);
            if (refreshinterval >= secondssinceloaded) {
                var timer = setTimeout("countdown()", 1000);

            }
            else {
                clearTimeout(timer);
                window.location.reload(true);
            }
        }
        window.onload = starttime;
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
        tarefaEdicao.IndicaCorBorda = 1;
        tarefaEdicao.Name = s.cp_DetalheItem;
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
