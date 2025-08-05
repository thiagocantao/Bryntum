<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="FrameAprovacao.aspx.cs" Inherits="Tarefas_Aprovacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function executaAcaoMovimento(drop, task, eOpts) {
            var codigosAtrib = '';

            for (i = 0; i < task.length; i++) {
                codigosAtrib += task[i].data.Id + ",";
            }

            callbackStatus.PerformCallback(codigosAtrib + ';' + task[0].data.State);   
        }

        function executaAcaoEdicao(task) {

        }

        function executaAcaoConsulta(task) {
            window.top.showModal('../espacoTrabalho/ConsultaTarefa.aspx?CA=' + task.Id, 'Detalhes da Tarefa', 940, 490, '', null);
        }

        function abreComentarios(task) {
            window.top.showModal('./ComentariosTarefa.aspx?CA=' + task.Id, 'Editar Comentários', 950, 485, '', null);
        }

        function abreAnexos(task) {
            var urlAnexo = "../espacoTrabalho/frameEspacoTrabalho_BibliotecaInterno.aspx?Popup=S&TA=TC&ID=" + task.Id + "&ALT=392";
            window.top.showModal(urlAnexo, 'Anexos', 860, 435, '', null);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxcb:ASPxCallback ID="callbackStatus" runat="server" 
            ClientInstanceName="callbackStatus" oncallback="callbackStatus_Callback">
        </dxcb:ASPxCallback>
    </div>
    </form>
</body>
</html>
