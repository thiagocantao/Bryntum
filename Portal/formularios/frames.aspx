<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="frames.aspx.cs"
    Inherits="formularios_frames" Title="Portal da Estratégia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table>
        <tr>
            <td style="width: 5px" valign="top">
            </td>
            <td style="width: 150px" valign="top">
                <iframe name="menuLateral" src="frame_MenuEsquerdo.aspx" width="100%" scrolling="no"
                    frameborder="0" style="height: <%=alturaTabela %>"></iframe>
            </td>
            <td style="width: 5px">
            </td>
            <td>
                <iframe name="framePrincipal" src="RenderizaFormularios.aspx" width="100%" scrolling="auto" frameborder="0"
                    style="height: <%=alturaTabela %>"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>
