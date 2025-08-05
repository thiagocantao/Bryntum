<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrcamentoTP.aspx.cs" Inherits="OrcamentoTP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <script type="text/javascript">
        var existeConteudoCampoAlterado = false;

        function conteudoCampoAlterado() 
        {
            existeConteudoCampoAlterado = true;
        }

        function novaPrevisao() {

            window.top.showModal('../../Administracao/CadastroPrevisoesOrcamentarias.aspx?CP=' + ddlPrevisao.cp_CodigoProjeto, 'Nova Previsão', 900, 435, funcaoPosModal, null);
        }

        function funcaoPosModal(valor) {
            ddlPrevisao.PerformCallback('');
        }
    </script> 

    <style type="text/css">
        .style3
        {
            width: 10px;
            height: 10px;
        }
        .style4
        {
            height: 10px;
        }
        .style5
        {
            width: 100px;
        }
    </style>

</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 10px; height: 10px;">
                </td>
                <td style="height: 10px">
                </td>
                <td style="width: 10px; height: 10px;">
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <table>
                        <tr>
                            <td>
                <dxe:ASPxLabel runat="server" Text="Previsão Orçamentária:" 
                        ID="ASPxLabel1"></dxe:ASPxLabel>


                            </td>
                            <td>
                                (<a href="#" onclick="novaPrevisao()" tabindex="4">Editar Previsões</a>)</td>
                        </tr>
                    </table>


                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <dxe:ASPxComboBox ID="ddlPrevisao" runat="server" 
                        ClientInstanceName="ddlPrevisao"  
                        Width="500px">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	grid.PerformCallback();
}" />
                    </dxe:ASPxComboBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                </td>
                <td class="style4">
                </td>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid"
                        KeyFieldName="Codigo" Width="100%" 
                        OnRowUpdating="grid_RowUpdating" OnHtmlRowCreated="grid_HtmlRowCreated" 
                        oncelleditorinitialize="grid_CellEditorInitialize" 
                        onsummarydisplaytext="grid_SummaryDisplayText" EnableRowsCache="False" 
                        EnableViewState="False" onhtmldatacellprepared="grid_HtmlDataCellPrepared">
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsEditing Mode="Inline" />
                        <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" 
                            ShowFooter="True" ShowGroupButtons="False" ShowGroupPanel="True" 
                            ShowGroupFooter="VisibleAlways" />
                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" />
                        <Styles>
                            <GroupRow BackColor="#EBEBEB" Font-Bold="True">
                            </GroupRow>
                            <Footer Font-Bold="True">
                            </Footer>
                        </Styles>
                    </dxwgv:ASPxGridView>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
