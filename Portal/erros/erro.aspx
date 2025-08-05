<%@ Page Language="C#" AutoEventWireup="true" CodeFile="erro.aspx.cs" Inherits="erros_erro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>Project Explorer</title>
    <script type="text/javascript"  language="javascript">
        document.addEventListener("DOMContentLoaded", function(event) { 
        window.top.SetBotaoSalvarVisivel(false);   
});

function funcaoCallbackFechar(){
   window.top.fechaModalComFooter();
}
    </script>

    <style type="text/css">
        .box-erro{
            display:flex;
            justify-content:center;
            margin:3rem 0rem;
            padding: 1rem;
            color: #004085;
            background-color: #cce5ff;
           border: 1px solid #a4cffb;
            border-radius: 6px;
            font-family: Verdana, sans-serif;
            font-size:16px;
            line-height: 180%;
        }

        .box-erro a{
           color: #ffffff;
            background: #7bbafb;
            padding: 8px;
            border-radius: 6px;
            text-decoration: none;
            font-size: 14px;
        }

    </style>

</head>
<body style="text-align: center; margin: 0;">
    <form id="form1" runat="server">
        <div class="box-erro">
            <span>
                <dxcp:ASPxLabel ID="lblComunicacaoErro" runat="server" Text="Ocorreu eu erro no sistema. Favor abrir um chamado para o suporte do sistema informando o código <GUID>"></dxcp:ASPxLabel>
                <%--<a href="#" onclick="history.back();return false;" title="Clique para voltar">Voltar</a>--%>
                <dxcp:ASPxHyperLink ID="linkVoltar" runat="server" Text="Voltar" ToolTip="Clique para voltar" Cursor="pointer">
                    <ClientSideEvents Click="function(s,e){history.back();}" />
                </dxcp:ASPxHyperLink>
            </span>
        </div>
    </form>
</body>
</html>
