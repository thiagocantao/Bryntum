<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MensagensEnviadasExecutivo.aspx.cs" Inherits="_VisaoExecutiva_MensagensEnviadasExecutivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self" />
    <title>Mensagens</title>
    <script type="text/javascript" language="javascript">
        function OnGridFocusedRowChanged(grid) 
        {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'Mensagem;Resposta;Assunto', MontaCampos);
        }
        
        function MontaCampos(values)
        {
            txtMensagem.SetText(values[0]);
            txtResposta.SetText(values[1]);
            txtAssunto.SetText(values[2]);
        }
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                </td>
                <td style="width: 10px; height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxwgv:ASPxGridView ID="gvMensagens" runat="server" AutoGenerateColumns="False"
                        ClientInstanceName="gvMensagens"  KeyFieldName="CodigoMensagem" OnHtmlDataCellPrepared="gvMensagens_HtmlDataCellPrepared" Width="100%">
                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                        </SettingsPager>
                        <SettingsText ConfirmDelete="Deseja Realmente Excluir a Associa&#231;&#227;o?"  />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Mensagem" FieldName="Mensagem" Name="colMensagem"
                                VisibleIndex="0" Width="30%">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Autor Resposta" FieldName="UsuarioResposta"
                                VisibleIndex="1" Width="25%">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Data Resposta" FieldName="DataResposta" VisibleIndex="2"
                                Width="12%">
                                <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Limite Resposta" FieldName="DataLimiteResposta"
                                Visible="False" VisibleIndex="3" Width="90px">
                                <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                </PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Resposta" FieldName="Resposta" Name="colResposta"
                                VisibleIndex="5" Width="30%">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Assunto" Visible="False" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Settings VerticalScrollBarMode="Visible" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" />
                    </dxwgv:ASPxGridView>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                </td>
                <td style="width: 10px; height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                        Text="Assunto:">
                    </dxe:ASPxLabel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txtAssunto" runat="server" ClientEnabled="False" ClientInstanceName="txtAssunto"
                         Width="915px">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                </td>
                <td style="width: 10px; height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
            <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                        Text="Mensagem:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxMemo ID="txtMensagem" runat="server" ClientEnabled="False" Rows="6" Width="100%" ClientInstanceName="txtMensagem" >
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                        Text="Resposta:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxMemo ID="txtResposta" runat="server" 
                        Rows="6" Width="100%" ClientInstanceName="txtResposta" ClientEnabled="False">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxe:ASPxMemo>
                </td>
            </tr>
        </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                </td>
                <td style="width: 10px; height: 5px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="right">
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" ClientInstanceName="btnFecharGeral"
                                         Text="Fechar" Width="90px">
                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	window.top.fechaModal();
}" />
                    </dxe:ASPxButton>
                </td>
                <td>
                </td>
            </tr>
            </table>
    
    </div>
                                    &nbsp;
    </form>
</body>
</html>
