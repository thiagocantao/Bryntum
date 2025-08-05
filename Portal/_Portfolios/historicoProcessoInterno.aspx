<%@ Page Language="C#" AutoEventWireup="true" CodeFile="historicoProcessoInterno.aspx.cs" Inherits="_Portfolios_historicoProcessoInterno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../estilos/custom.css" rel="stylesheet" />
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <link href="../estilos/custom.css" rel="stylesheet" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">

        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr style="height: 26px">
                <td valign="middle">&nbsp; &nbsp;<dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloSelecao"
                    Font-Bold="True">
                </dxe:ASPxLabel>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td id="ConteudoPrincipal">
                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="lblProcesso" runat="server" ClientInstanceName="lblProcesso" Text="Processo:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="width: 200px">
                                            <dxe:ASPxLabel ID="lblDataInicio" runat="server" ClientInstanceName="lblDataInicio" Text="Data de Início:">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtProcesso" runat="server" ClientInstanceName="txtProcesso"
                                                ReadOnly="True" Width="100%">
                                                <ReadOnlyStyle BackColor="WhiteSmoke" ForeColor="Black"></ReadOnlyStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td style="width: 200px">
                                            <dxe:ASPxTextBox ID="txtDataInicio" runat="server" ClientInstanceName="txtDataInicio"
                                                ReadOnly="True" Width="100%">
                                                <ReadOnlyStyle BackColor="WhiteSmoke" ForeColor="Black"></ReadOnlyStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                        <tr>

                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="formulario-colunas">
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="lblEtapaAtual" runat="server" ClientInstanceName="lblEtapaAtual" Text="Etapa Atual:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="width: 200px">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                Text="Status:">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtEtapaAtual" runat="server" ClientInstanceName="txtEtapaAtual"
                                                ReadOnly="True" Width="100%">
                                                <ReadOnlyStyle BackColor="WhiteSmoke" ForeColor="Black"></ReadOnlyStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtStatus" runat="server" ClientInstanceName="txtStatus"
                                                ReadOnly="True" Width="100%">
                                                <ReadOnlyStyle BackColor="WhiteSmoke"></ReadOnlyStyle>
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                        <tr>
                            <td style="padding-top: 5px">
                                 <div id="divGrid" style="visibility:hidden">
                                <dxwgv:ASPxGridView ID="gvHistorico" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvHistorico"
                                    Width="100%" OnBeforeColumnSortingGrouping="gvHistorico_BeforeColumnSortingGrouping"
                                    OnHtmlRowCreated="gvHistorico_HtmlRowCreated" KeyFieldName="SequenciaOcorrenciaEtapaWf">
                                    <ClientSideEvents Init="function(s, e) {
    var height = Math.max(0, document.documentElement.clientHeight) - 150;
    s.SetHeight(height);
    document.getElementById('divGrid').style.visibility = '';
}" />
                                    <SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>
                                    <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn FieldName="SequenciaOcorrenciaEtapaWf" Width="50px" Caption="Seq." ToolTip="Seq&#252;&#234;ncia" VisibleIndex="0"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NomeEtapaWf" Caption="Etapa" VisibleIndex="1" Width="300px"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DataInicioEtapa" Width="200px" Caption="Inicio" VisibleIndex="2">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="DataTerminoEtapa" Width="200px" Caption="Fim" VisibleIndex="3">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="TempoDecorrido" Width="150px" Caption="Tempo Decorrido" VisibleIndex="4">
                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="TerminoPrevisto" Width="6%" Caption="T&#233;rmino Previsto" Visible="False" VisibleIndex="5">
                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Atraso" Width="200px" Caption="Atraso" VisibleIndex="6"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioFinalizador" Width="250px" Caption="Respons&#225;vel" VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="TextoAcao" Width="250px" Caption="A&#231;&#227;o Finalizadora" VisibleIndex="8">
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Left"></CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="ComAtraso" Width="6%" Visible="False" VisibleIndex="9"></dxwgv:GridViewDataTextColumn>
                                    </Columns>

                                    <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"></Settings>
                                </dxwgv:ASPxGridView>
                                     </div>
                            </td>

                        </tr>
                    </table>
                </td>
            </tr>
        </table>



    </form>
</body>
</html>
