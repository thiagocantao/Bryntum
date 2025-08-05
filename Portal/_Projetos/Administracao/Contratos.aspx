<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" EnableViewState="false"  AutoEventWireup="true" CodeFile="Contratos.aspx.cs" Inherits="_Projetos_DadosProjeto_Contratos" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">                
                <tr>
                    <td><iframe frameborder="0" height="<%=alturaTela %>" scrolling="no" src="<%=urlContratos %>"
                                    width="100%"></iframe>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Content>
