<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CadastroProjeto.aspx.cs" Inherits="_Estrategias_mapa_CadastroProjeto" %>

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
            height: 10px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function funcaoPosSalvarFormulario(codigoProjeto, codigoForm) {
            var url = window.top.pcModal.cp_Path + '_Estrategias/mapa/CadastroProjeto.aspx?CP=' + codigoProjeto + '&CF=' + codigoForm;

            window.top.atualizaURLModal(url);
        }
    </script>
</head>
<body style="margin:5px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
    
        <dxtc:ASPxPageControl ID="pcProjeto" runat="server" ActiveTabIndex="0" 
            ClientInstanceName="pcProjeto"  
            Width="100%">
            <TabPages>
                <dxtc:TabPage Name="tabDados" Text="Dados do Projeto">
                    <ContentCollection>
                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                            
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
                <dxtc:TabPage Name="tabAcoes" Text="Ações do Mapa">
                    <ContentCollection>
                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
            </TabPages>
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
        </dxtc:ASPxPageControl>
    
                </td>
            </tr>
            <tr>
                <td class="style2">
                </td>
            </tr>
            <tr>
                <td align="right">
                    <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" 
                         Text="Fechar" Width="100px">
                        <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                        <Paddings Padding="0px" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
