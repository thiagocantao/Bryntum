<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoricoPlanoInvestimento.aspx.cs"
    Inherits="HistoricoPlanoInvestimento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
       
    </script>
    <style type="text/css">
         .style6
        {
            width: 50%;
            height: 5px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td align="left">
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                            Text="Projeto:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtProjeto" ClientEnabled="False"
                             ID="txtProjeto">
                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                            </DisabledStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style6">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvPeriodo" KeyFieldName="CodigoMovimentoPlanoInvestimento"
                            AutoGenerateColumns="False" Width="100%" 
                            ID="gvPeriodo">
                            <ClientSideEvents FocusedRowChanged="function(s, e) {
	e.processOnServer = false;
	pnCallbackDetalhe.PerformCallback();
}"></ClientSideEvents>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="Data" FieldName="DataMovimento" VisibleIndex="0"
                                    Width="120px">
                                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm}">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="NomeUsuario" VisibleIndex="1"
                                    Width="250px">
                                    <Settings AutoFilterCondition="Contains" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Movimento" FieldName="DescricaoMovimento"
                                    VisibleIndex="2">
                                    <Settings AutoFilterCondition="Contains" />
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True">
                            </SettingsBehavior>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings ShowFooter="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="180">
                            </Settings>
                            <SettingsText Title="Histórico de Alterações"></SettingsText>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style6">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackDetalhe" Width="100%"
                            ID="pnCallbackDetalhe">
                            <PanelCollection>
                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblDe" runat="server"  Text="De:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblPara" runat="server"  Text="Para:"
                                                                    ClientInstanceName="lblPara">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-right: 10px">
                                                                <dxe:ASPxTextBox ID="txtDe" runat="server" ClientEnabled="False" ClientInstanceName="txtDe"
                                                                    DisplayFormatString="n2"  Width="140px">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxTextBox ID="txtPara" runat="server" ClientEnabled="False" ClientInstanceName="txtPara"
                                                                    DisplayFormatString="n2"  Width="140px">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                        Text="Comentários:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxMemo runat="server" Width="100%" ClientInstanceName="mmComentarios"
                                                        ID="mmComentarios" Rows="10" ClientEnabled="False">
                                                        <ClientSideEvents KeyUp="function(s, e) {
	//limitaASPxMemo(s, 2000);
}"></ClientSideEvents>
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="MKE">
                                                            <RequiredField ErrorText="Campo Obrigat&#243;rio!"></RequiredField>
                                                        </ValidationSettings>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                            <Styles>
                                <LoadingPanel HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                                </LoadingPanel>
                            </Styles>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style6">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <dxe:ASPxButton runat="server" CommandArgument="btnCancelar" Text="Fechar" Width="100px"
                             ID="btnCancelar">
                            <ClientSideEvents Click="function(s, e) {
	window.parent.fechaModal();
}"></ClientSideEvents>
                            <Paddings Padding="1px" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
