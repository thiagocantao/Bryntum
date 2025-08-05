<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frame_graficoEAP.aspx.cs" Inherits="GoJs_EAP_frame_graficoEAP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript"  src="../../scripts/jquery.ultima.js"></script>
    <script type="text/javascript"  src="../../scripts/jquery.ui.ultima.js"></script>
    <script type="text/javascript">
        var existeConteudoCampoAlterado = false;

        $(document).ready(function () {
            var height = Math.max(0, document.documentElement.clientHeight);
            $('#frameConteudo').height(height - 20);
        });

        function verificaAvancoWorkflow(codigoWorkflow, codigoEtapa, codigoAcao) {
            if (existeConteudoCampoAlterado) {
                window.top.mostraMensagem('As alterações não foram gravadas. É necessário gravar os dados antes de prosseguir com o fluxo.', 'erro', true, false, null);
                return false;
            }
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <iframe id="frameConteudo" runat="server" style="width: 98%; height: 900px; border: none;"></iframe>
    </div>
    </form>

</body>
</html>
