<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarioReuniaoDiariaSprint.aspx.cs" Inherits="_Projetos_Agil_CalendarioReuniaoDiariaSprint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function abreReuniaoDiaria(codigoProjeto,codigoIteracao, dia, mes, ano) {
            //debugger
            window.top.fechaModal();

            var altura = (screen.availHeight - 190);
            var largura = (screen.availWidth - 150);

            var url = window.top.pcModal.cp_Path + '_Projetos/Agil/ReuniaoDiariaSprint.aspx?CP=' + codigoProjeto;
            url += '&CI=' + codigoIteracao;
            url += '&ano=' + ano;
            url += '&mes=' + mes;
            url += '&dia=' + dia;
            url += '&alt=' + altura;
            url += '&larg=' + largura;

            window.top.showModal(url, 'Reunião', largura, altura, '', null);
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dxcp:ASPxCalendar runat="server" ID="calendario" 
            ondaycellprepared="ASPxCalendar1_DayCellPrepared" 
            ClientInstanceName="calendario"  
            ShowClearButton="False" ShowTodayButton="False" ShowWeekNumbers="False">
        </dxcp:ASPxCalendar>
    
    </div>
    </form>
</body>
</html>
