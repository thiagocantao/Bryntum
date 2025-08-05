<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Public_Gantt_paginas_projetometa_Default" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <title>Cronograma Balanceamento Bryntum </title>
    <link rel="stylesheet" href="../../build/gantt.stockholm.css" id="bryntum-theme">
    <link rel="stylesheet" href="../_shared/shared.css">
    <link rel="stylesheet" href="resources/app.css">    
</head>

<body>    
    <form runat="server">              
    </form>     
     <div id="container" class="no-tools"></div>
    
    <script src="../../../../scripts/custom/util/linqtoo.min.js"></script>    
    <script>                
        var idEntidade = '<%=idEntidade%>';
        var idUsuario = '<%=idUsuario%>';
        var idCarteira = '<%=idCarteira%>';
        var langCode = '<%=langCode%>';        
        var baseUrl = '<%=Session["baseUrl"]%>'        
        var jsonTraducao = JSON.parse('<%=jsonTraducao%>');  
        console.log('langCode:', langCode);
        
        var getTraducao = function (nameKey) {
            if (jsonTraducao == null) {
                return "No translation";
            } else {
                var itemTraducao = jsonTraducao.Where(function (item) { return item.Key == nameKey; }).First().Text;
                return itemTraducao;
            }
        }

        function recarregar() {
            location.reload();
        }    
   
    </script>
    <!-- Using Ecma Modules bundle -->
    <script src="../../../../scripts/jquery-3.1.1.min.js"></script>
    <script src="../../build/locales/gantt.locale.Nl.js"></script>
    <script src="../../build/locales/gantt.locale.examples.Nl.js"></script>
    <script src="../../build/locales/gantt.locale.Ru.js"></script>
    <script src="../../build/locales/gantt.locale.examples.Ru.js"></script>
    <script src="../../build/locales/gantt.locale.SvSE.js"></script>
    <script src="../../build/locales/gantt.locale.examples.SvSE.js"></script>
    <script src="../../build/locales/gantt.locale.Pt_BR.js"></script>
    <script src="../../../../scripts/custom/util/browser.js"></script>
    <script src="columns/col<%=langCode%>.js"></script>
    <script data-default-locale="<%=langCode%>" src="../../build/gantt.umd.min.js"></script>

    <script src="../_shared/shared.umd.js"></script>
    <script src="app.umd.js"></script>    

</body>

</html>
