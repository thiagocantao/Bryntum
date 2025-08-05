<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanoAcaoIndicador.aspx.cs" Inherits="_VisaoMaster_PlanoAcaoIndicador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function gvPeriodo_FocusedRowChanged(s, e) {
            var rowIndex = s.GetFocusedRowIndex();
            if (-1 < rowIndex)
                s.GetRowValues(rowIndex, ';', preencheDetalhePeriodo);
        }

        function preencheDetalhePeriodo(valores) {
            if (null != valores) {
                var CodigoProjeto = valores[0];
                var CodigoIndicador = valores[1];
                var Ano = valores[2];
                var Mes = valores[3];
                var periodo = valores[4];
                var MetaMes = valores[5];
                var ResultadoMes = valores[6];
                var Desempenho = valores[7];

                hfGeral.Set('Ano', Ano);
                hfGeral.Set('Mes', Mes);
            }
        }
    </script>
    <style type="text/css">
        .style2
        {
            height: 10px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table cellspacing="0" 
        cellpadding="0" width="100%" border="0"><TBODY><tr><td align=left>
                <dxp:ASPxPanel ID="pnPlanoAcao" runat="server" Width="100%">
                </dxp:ASPxPanel>



 </td></tr><tr><td align=left class="style2">



 </td></tr><tr><td align=right>
                    <dxe:ASPxButton runat="server" CommandArgument="btnCancelar" Text="Fechar" 
                        Width="100px"  ID="btnCancelar">
<ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}"></ClientSideEvents>
</dxe:ASPxButton>



 </td></tr></tbody></table>
    
    </div>
        <dxhf:ASPxHiddenField ID="hfDadosSessao" runat="server" ClientInstanceName="hfDadosSessao">
        </dxhf:ASPxHiddenField>
    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
        <ClientSideEvents Init="function(s, e) {
	hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
    hfGeralToDoList.Set('codigoObjetoAssociado', hfGeral.Get(&quot;codigoObjetoAssociado&quot;) );
           
}" />
    </dxhf:ASPxHiddenField>



    </form>
</body>
</html>
