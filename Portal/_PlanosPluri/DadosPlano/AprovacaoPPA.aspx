<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AprovacaoPPA.aspx.cs" Inherits="_PlanosPluri_DadosPlano_AprovacaoPPA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 135px;
        }
        .auto-style3 {
            width: 136px;
        }
    </style>
     <script type="text/javascript" language="javascript">

        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

            fechaTelaEdicao();
        }

        function fechaTelaEdicao() {
            if (callbackAprovar.cp_Status == "OK")
                btnAprovar.SetEnabled(false);
        }
    </script>
</head>
<body style="margin:0px">    
    <form id="form1" runat="server">
    <div>
    <table cellspacing="0" class="auto-style1">
        <tr>
            <td style="padding: 5px">
                <dxcp:ASPxGridView ID="gvPlano" runat="server" AutoGenerateColumns="False"  Width="100%" ClientInstanceName="gvPlano" KeyFieldName="CodigoLimite">
                    <Settings ShowTitlePanel="True" VerticalScrollBarMode="Auto" />
                    <SettingsText Title="Limites do Plano" />
                    <Columns>
                        <dxtv:GridViewDataTextColumn Caption="Nome do Limite" FieldName="IdentificacaoLimite" VisibleIndex="0">
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Valor Mínimo" FieldName="ValorMinimo" VisibleIndex="1" Width="100px">
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Valor Máximo" FieldName="ValorMaximo" VisibleIndex="2" Width="100px">
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Status" VisibleIndex="3" Width="120px" FieldName="Status">
                        </dxtv:GridViewDataTextColumn>
                    </Columns>
                </dxcp:ASPxGridView>
                <script language="javascript" type="text/javascript">
                            gvPlano.SetHeight((window.innerHeight - 80) / 2);
                </script>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px">
                <dxcp:ASPxGridView ID="gvOrcamento" runat="server" AutoGenerateColumns="False"  Width="100%" ClientInstanceName="gvOrcamento" KeyFieldName="CodigoLimite">
                    <Settings ShowTitlePanel="True" VerticalScrollBarMode="Auto" />
                    <SettingsText Title="Limites de Orçamento" />
                    <Columns>
                        <dxtv:GridViewDataTextColumn Caption="Nome do Limite" FieldName="IdentificacaoLimite" VisibleIndex="0">
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Valor Mínimo" FieldName="ValorMinimo" VisibleIndex="1" Width="100px">
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Valor Máximo" FieldName="ValorMaximo" VisibleIndex="2" Width="100px">
                            <PropertiesTextEdit DisplayFormatString="n2">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" />
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxtv:GridViewDataTextColumn>
                        <dxtv:GridViewDataTextColumn Caption="Status" VisibleIndex="3" Width="120px" FieldName="Status">
                        </dxtv:GridViewDataTextColumn>
                    </Columns>
                </dxcp:ASPxGridView>
                <script language="javascript" type="text/javascript">
                            gvOrcamento.SetHeight((window.innerHeight - 80) / 2);
                </script>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" class="auto-style1">
                    <tr>
                        <td class="auto-style2">
                            <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Total de Receitas:" >
                            </dxcp:ASPxLabel>
                        </td>
                        <td class="auto-style3">
                            <dxcp:ASPxLabel ID="ASPxLabel2" runat="server" Text="Total de Despesas:" >
                            </dxcp:ASPxLabel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style2" style="padding-right: 10px">
                            <dxcp:ASPxTextBox ID="txtReceita" runat="server" ClientEnabled="False"  Width="100%" DisplayFormatString="n2">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxcp:ASPxTextBox>
                        </td>
                        <td class="auto-style3">
                            <dxcp:ASPxTextBox ID="txtDespesas" runat="server" ClientEnabled="False"  Width="100%" DisplayFormatString="n2">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxcp:ASPxTextBox>
                        </td>
                        <td align="right">
                <dxcp:ASPxButton ID="btnAprovar" runat="server"  Text="Aprovar Plano" Width="120px" AutoPostBack="False" ClientInstanceName="btnAprovar">
                    <ClientSideEvents Click="function(s, e) {
	if(confirm('Deseja aprovar o plano?'))
		callbackAprovar.PerformCallback();
}" />
                    <Paddings Padding="0px" />
                </dxcp:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
        <dxcp:ASPxCallback ID="callbackAprovar" runat="server" ClientInstanceName="callbackAprovar" OnCallback="callbackAprovar_Callback">
            <clientsideevents endcallback="function(s, e) {
	mostraDivSalvoPublicado(s.cp_Msg);
}" />
        </dxcp:ASPxCallback>

 <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido"><ContentCollection>
<dxcp:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxcp:ASPxImage>


























 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxcp:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxcp:ASPxLabel>


























 </td></tr></tbody></table></dxcp:PopupControlContentControl>
</ContentCollection>
</dxcp:ASPxPopupControl>

    </form>
</body>
</html>
