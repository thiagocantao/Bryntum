<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDO.aspx.cs" Inherits="_Projetos_DadosProjeto_RDO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 10px;
        }
        .style3
        {
            width: 10px;
            height: 10px;
        }
        .style4
        {
            height: 10px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var houveMudanca = 'N';

        function atualizaFrameLancamentos() {
            document.getElementById('frm002').src = document.getElementById('frm002').src;
        }
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td class="style3">
                </td>
                <td class="style4">
                </td>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    <dxtc:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                         Width="100%" 
                        ClientInstanceName="ASPxPageControl1">
                        <TabPages>
                            <dxtc:TabPage Name="tbLancamento" Text="Lançamento">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <iframe id="frm002" frameborder="0" height="<%=alturaFrame %>" name="frm002" src="./LancamentoRDO.aspx?CP=<%=codigoProjeto %>" scrolling="no" 
                                            width="100%"></iframe>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="tbComentario" Text="Comentários">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                      <iframe ID="frm003" frameborder="0" height="<%=alturaFrame %>" name="frm003" src="./ComentarioRDO.aspx?CP=<%=codigoProjeto %>&Altura=<%=alturaFrame %>" scrolling="no" 
                                            width="100%"></iframe>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="tbCadastro" Text="Cadastro">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <iframe ID="frm001" frameborder="0" height="<%=alturaFrame %>" name="frm001" src="" scrolling="no" 
                                            width="100%"></iframe>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                        </TabPages>
                        <ClientSideEvents ActiveTabChanged="function(s, e) {
	if(e.tab.index == 0 &amp;&amp; houveMudanca == 'S')
	{
		houveMudanca = 'N';
		atualizaFrameLancamentos();
	}
    if(e.tab.index == 1)
    {
        document.getElementById('frm003').contentWindow.location.reload(true);        
    }
    if(e.tab.index == 2)
    {
        document.getElementById('frm001').contentWindow.location.reload(true);
    }
}" />
                    </dxtc:ASPxPageControl>
                </td>
                <td class="style2">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
