<%@ Page Language="C#" AutoEventWireup="true" CodeFile="erroConexaoBancoDeDados.aspx.cs" Inherits="erros_erroInatividade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><asp:Literal runat="server" Text="<%$ Resources:traducao, erroInatividade_inatividade_no_sistema_brisk %>" /></title>
    <script type="text/javascript" src="./scripts/CDIS.js" language="javascript"></script>
    <meta name="viewport" content="width=device-width, user-scalable=no">

    <style>
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
<body>
    <form id="form1" runat="server">
        <div class="box-erro">
            <span><asp:Literal runat="server" Text="<%$ Resources:traducao, erroConexaoBancoDeDados%>"></asp:Literal></span>
        </div>
    </form>
</body>
</html>
