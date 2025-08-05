<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Caracterizacao.aspx.cs" Inherits="_Projetos_DadosProjeto_Caracterizacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 10px; height: 5px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxLabel ID="lblProjeto" runat="server" 
                        Text="Projeto:">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txtNomeProjeto" runat="server" ClientInstanceName="txtNomeProjeto"
                        Width="98%" >
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="#404040">
                        </DisabledStyle>
                    </dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 10px">
                </td>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td style="width: 315px">
                                <dxe:ASPxLabel ID="lblUnidade" runat="server" ClientInstanceName="lblUnidade"
                                    Text="Unidade:">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                            </td>
                            <td valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Responsável:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 315px; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;
                                border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid;">
                                <div style="height: <%= alturaDivArvore %>px; width: 420px; overflow: auto;">
                                    <dxwtl:ASPxTreeList ID="tlUnidades" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridUnidades"
                                        Height="150px" KeyFieldName="CodigoUnidadeNegocio" ParentFieldName="CodigoUnidadeNegocioSuperior"
                                        Width="402px" >
                                        <Styles>
                                            <SelectedNode BackColor="Transparent">
                                            </SelectedNode>
                                        </Styles>
                                        <SettingsBehavior AutoExpandAllNodes="True" AllowSort="False" AllowFocusedNode="True"
                                            FocusNodeOnExpandButtonClick="False"></SettingsBehavior>
                                        <Columns>
                                            <dxwtl:TreeListTextColumn FieldName="SiglaUnidadeNegocio" AllowSort="False" Caption="..."
                                                VisibleIndex="0">
                                            </dxwtl:TreeListTextColumn>
                                        </Columns>
                                    </dxwtl:ASPxTreeList>
                                </div>
                            </td>
                            <td>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxComboBox ID="ddlGerente" runat="server" 
                                                Width="370px" IncrementalFilteringMode="Contains" TextField="NomeUsuario" TextFormatString="{0}"
                                                ValueField="CodigoUsuario" ValueType="System.String">
                                                <Columns>
                                                    <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="300px" />
                                                    <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="200px" />
                                                </Columns>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="lblCategoria" runat="server" 
                                                Text="Categoria:">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxComboBox ID="ddlCategoria" runat="server" 
                                                Width="370px">
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 10px;">
                </td>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td style="width: 110px">
                                <dxe:ASPxButton ID="btnSalvar" runat="server" 
                                    OnClick="btnSalvar_Click" Text="Salvar" Width="90px">
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnCancelar" runat="server" 
                                    OnClick="btnCancelar_Click" Text="Cancelar" Width="90px">
                                    <Paddings Padding="0px" />
                                    <ClientSideEvents Click="function(s, e) 
{
	if(confirm('Todos os Dados Ser&#227;o Perdidos! Deseja Cancelar a Caracteriza&#231;&#227;o?') == false)
		e.processOnServer = false;
		
}" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
