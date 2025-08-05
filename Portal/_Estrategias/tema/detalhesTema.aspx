<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detalhesTema.aspx.cs" Inherits="_Estrategias_detalhesObjetivoEstrategico" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript">
        function getCodigoProjeto(indexLinha)
        {
             gridProjetos.GetRowValues(indexLinha, 'CodigoProjeto;CodigoObjetivoEstrategico;NomeProjeto;Prioridade;CodigoIndicador;NomeIndicador', abreEdicaoProjeto);
        }
        
        function abreGanttProjeto(valores)
        {
            var codigoProjeto = valores[0];
            var codigoObjetivo = valores[1];
        }
        
        function abreEdicaoProjeto(valores)
        {
            var codigoProjeto = valores[0];
            var nomeProjeto = valores[2];
            var prioridade = valores[3];
            var codigoIndicador = valores[4] !=null ? valores[4] : "-1";
            var nomeIndicador =  valores[5];
            
            txtProjeto.SetText(nomeProjeto);
            mostraTabelas("E");
            pcAssociarProjeto.Show();
            ddlMeta.SetValue(codigoIndicador);
            gvAcoes.PerformCallback(codigoProjeto);
            
            if(prioridade != null && prioridade != "")
                ddlPrioridade.SetValue(prioridade.toString());
        }
        
        function mostraTabelas(tipo)
        {
            if(tipo == "I")
            {
                document.getElementById('tbInsercao').style.display = "block";
                document.getElementById('tbEdicao').style.display = "none";
                ddlMeta.SetValue("-1");
            }
            else
            {
                document.getElementById('tbInsercao').style.display = "none";
                document.getElementById('tbEdicao').style.display = "block";
            }
        }
        function abreGanttOE(codigoOE)
        {
            window.top.showModal('gantt.aspx?COE='+ codigoOE, "Gantt dos Projetos Associados ao Objetivo Estratégico Selecionado", 1020, screen.height - 240, "", null);
        }
    </script>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="padding-right: 10px; padding-left: 10px; padding-top: 5px">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                <asp:Label ID="Label1" runat="server"  Text="Mapa Estratégico:"></asp:Label></td>
                        <td>
                        </td>
                        <td style="width: 209px">
                            <asp:Label ID="lblPerspectiva" runat="server" 
                                Text="Perspectiva:"></asp:Label></td>
                        <td>
                        </td>
                        <td style="width: 220px">
                            <asp:Label ID="Label6" runat="server"  Text="Tema:"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientInstanceName="txtMapa"
                     ReadOnly="True" Width="100%">
                    <ReadOnlyStyle BackColor="#E0E0E0">
                    </ReadOnlyStyle>
                </dxe:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 220px">
                            <dxe:ASPxTextBox ID="txtPerspectiva" runat="server" ClientInstanceName="txtPerspectiva"
                     ReadOnly="True" Width="100%">
                            <ReadOnlyStyle BackColor="#E0E0E0">
                            </ReadOnlyStyle>
                        </dxe:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 220px">
                            <dxe:ASPxTextBox ID="txtTema" runat="server" ClientInstanceName="txtTema"
                     ReadOnly="True" Width="100%">
                                <ReadOnlyStyle BackColor="#E0E0E0">
                                </ReadOnlyStyle>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td >
                <dxwgv:aspxgridview id="gridOE" runat="server" autogeneratecolumns="False"
                    clientinstancename="gridOE"  keyfieldname="Codigo"
                    width="100%">
<SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>

<Styles>
<FocusedRow BackColor="Transparent" ForeColor="Black"></FocusedRow>
</Styles>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<SettingsText ConfirmDelete="Deseja Realmente Excluir a Associa&#231;&#227;o?" ></SettingsText>

<Columns>
<dxwgv:GridViewDataTextColumn FieldName="Descricao" Caption="Objetivo Estratégico" 
        VisibleIndex="0" ShowInCustomizationForm="True">
    <DataItemTemplate>
        <%# getDescricaoOE() %>
    </DataItemTemplate>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Cor" Width="60px" Caption=" " 
        VisibleIndex="1" ShowInCustomizationForm="True">
    <DataItemTemplate>
        <%# "<img src='../../imagens/" + Eval("Cor").ToString().Trim() + ".gif' />" %>
    </DataItemTemplate>
    <CellStyle HorizontalAlign="Center">
    </CellStyle>
    </dxwgv:GridViewDataTextColumn>
</Columns>

<Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="140"></Settings>
</dxwgv:aspxgridview>
            </td>
        </tr>
    </table>
                </td>
            </tr>
        </table>
        &nbsp;&nbsp;
    </form>
    </body>
    </html>