<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListagemDeProjetos.aspx.cs" Inherits="_Projetos_Administracao_ListagemDeProjetos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<base target="_self" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>

<title>Listagem de Projetos</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

<table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody>
    <tr>
        <td style="width: 10px; height: 5px">
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>

<td>
<dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackProjetos" Width="100%" id="pnCallbackProjetos"><PanelCollection>
<dxp:PanelContent runat="server"><!-- ASPxGRidVIEW: gvProjetos --><dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoProjeto" AutoGenerateColumns="False" Width="100%"  ID="gvDados">
<ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged();
}"></ClientSideEvents>
<Columns>
<dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" Name="NomeProjeto" Caption="Nome do Projeto" VisibleIndex="0"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="SiglaUnidadeNegocio" Name="SiglaUnidadeNegocio" Width="100px" Caption="Unidade" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeGerente" Name="NomeGerente" Width="200px" Caption="Gerente" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" Name="CodigoProjeto" Visible="False" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoMSProject" Name="CodigoMSProject" Visible="False" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoGerenteProjeto" Name="CodigoGerenteProjeto" Visible="False" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoUnidadeNegocio" Name="CodigoUnidadeNegocio" Visible="False" VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings VerticalScrollBarMode="Visible"></Settings>
</dxwgv:ASPxGridView>
 </dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>

 </td>
        <td>
        </td>
    </tr>
 <tr>
     <td align="right">
     </td>
 
<td align=right><table cellspacing="0" cellpadding="0" border="0"><tbody><tr><td>
<dxe:ASPxButton runat="server" ClientInstanceName="btnSalvarProjeto" Text="Selecionar" Width="90px" 
                 ID="btnSalvarProjeto" autopostback="False">
<ClientSideEvents Click="function(s, e) {
    selecionaProjeto();
    window.top.fechaModal2();
}"></ClientSideEvents>
</dxe:ASPxButton>
</td><td style="WIDTH: 10px;"></td><td>
 <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelarProjeto" Text="Cancelar" Width="90px" 
                 ID="btnCancelarProjeto">
<ClientSideEvents Click="function(s, e) {
                    window.top.fechaModal2();
                }"></ClientSideEvents>
</dxe:ASPxButton>

 </td>
    <td>
    </td>
    <td style="height: 38px">
    </td>
</tr></tbody></table></td>
     <td align="right">
     </td>
 </tr></tbody></table>
    
    </div>
    </form>
</body>
</html>
