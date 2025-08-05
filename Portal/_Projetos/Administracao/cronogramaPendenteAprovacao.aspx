<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cronogramaPendenteAprovacao.aspx.cs"
    Inherits="_Projetos_Administracao_cronogramaPendenteAprovacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td align="left">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxComboBox runat="server" Height="22px" ClientInstanceName="ddlExporta"
                                                            ID="ddlExporta" ClientVisible="False" Visible="False">
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                pnImage.PerformCallback(s.GetValue());
	                                                                hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
                                                            }"></ClientSideEvents>
                                                            <Paddings Padding="0px"></Paddings>
                                                        </dxe:ASPxComboBox>
                                                    </td>
                                                    <td style="padding-left: 2px">&nbsp;
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td style="padding-right: 5px;">
                                        <dxcp:ASPxCallbackPanel runat="server"
                                            ClientInstanceName="pnImage" Width="23px" Height="22px" ID="pnImage0" OnCallback="pnImage_Callback">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/botoes/btnExcel.png"
                                                        ToolTip="Exportar para arquivo Excel" OnClick="ImageButton1_Click" />
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxcp:ASPxCallbackPanel>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton runat="server" Text="Exportar" Height="22px"
                                            ID="Aspxbutton2" ClientVisible="False">
                                            <Paddings Padding="0px"></Paddings>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td style="padding-right:5px">
                                        <dxcp:ASPxLabel ID="lblLegendaFiltrar" runat="server" Text="Filtrar somente"></dxcp:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxp:ASPxPanel ID="paLegenda" runat="server" ClientInstanceName="paLegenda" Width="100%">
                                            <Border BorderColor="Gray"></Border>
                                            <Paddings PaddingLeft="0px" PaddingTop="5px"></Paddings>
                                            <PanelCollection>
                                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                                    <table cellspacing="0" cellpadding="0" bgcolor="#F3F3F3">
                                                        <tbody>
                                                            <tr>

                                                                <td style="width: 150px">
                                                                    <dxtv:ASPxCheckBox ID="checkTarefasIncluidas" Text="Tarefas Incluídas" ForeColor="Green" runat="server" CheckState="Checked" ClientInstanceName="checkTarefasIncluidas" Checked="True">
                                                                    </dxtv:ASPxCheckBox>
                                                                </td>
                                                                <td style="width: 150px">
                                                                    <dxtv:ASPxCheckBox ID="checkTarefasAlteradas" Text="Tarefas Alteradas" ForeColor="#0033CC" runat="server" CheckState="Checked" ClientInstanceName="checkTarefasAlteradas" Checked="True">
                                                                    </dxtv:ASPxCheckBox>
                                                                </td>
                                                                <td style="width: 150px">
                                                                    <dxtv:ASPxCheckBox ID="checkTarefasExcluidas" Text="Tarefas Excluídas" Font-Strikeout="True" runat="server" CheckState="Checked" ClientInstanceName="checkTarefasExcluidas" Checked="True">
                                                                    </dxtv:ASPxCheckBox>
                                                                </td>
                                                                <td>
                                                                    <dxtv:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar" ClientInstanceName="btnSelecionar" Width="100px">
                                                                    </dxtv:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>

            <dxwtl:ASPxTreeList ID="tlDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlDados"
                KeyFieldName="CodigoTarefa" ParentFieldName="CodigoTarefaSuperior"
                Width="100%" OnHtmlDataCellPrepared="tlDados_HtmlDataCellPrepared"
                Height="100%">
                <Columns>
                    <dxwtl:TreeListTextColumn FieldName="VersaoAtual" VisibleIndex="0" Visible="False">
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="VersaoAnterior" VisibleIndex="1" Visible="False">
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="CodigoCronogramaProjeto" VisibleIndex="2" Visible="False">
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="CodigoTarefa" Visible="False" VisibleIndex="3">
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="NomeTarefa" VisibleIndex="4" Width="30%" Caption="Nome Tarefa">
                        <HeaderStyle Wrap="True" />
                        <CellStyle Wrap="True">
                        </CellStyle>
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="SequenciaTarefaCronograma" Visible="False" VisibleIndex="5">
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="CodigoTarefaSuperior" Visible="False" VisibleIndex="6">
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="Inicio" VisibleIndex="7" Width="100px" Caption="Início &lt;br&gt; (A aprovar)">
                        <PropertiesTextEdit EncodeHtml="False">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                        <CellStyle HorizontalAlign="Center" Wrap="True">
                        </CellStyle>
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="Termino" VisibleIndex="8" Width="100px" Caption="Término &lt;br&gt; (A aprovar)">
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                        <CellStyle HorizontalAlign="Center" Wrap="True">
                        </CellStyle>
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="Duracao" VisibleIndex="9" Caption="Duração <br> (A aprovar)"
                        Width="100px">
                        <HeaderStyle HorizontalAlign="Right" Wrap="True" />
                        <CellStyle HorizontalAlign="Right" Wrap="True">
                        </CellStyle>
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="Trabalho" VisibleIndex="10" Caption="Trabalho &lt;br&gt; (A aprovar)"
                        Width="100px">
                        <HeaderStyle HorizontalAlign="Right" Wrap="True" />
                        <CellStyle HorizontalAlign="Right" Wrap="True">
                        </CellStyle>
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="Custo" VisibleIndex="11" Caption="Custo &lt;br&gt; (A aprovar)"
                        Width="100px">
                        <HeaderStyle HorizontalAlign="Right" Wrap="True" />
                        <CellStyle HorizontalAlign="Right" Wrap="True">
                        </CellStyle>
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="InicioLB" VisibleIndex="12" Caption="Início &lt;br&gt;(Atual)"
                        Width="100px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                        <CellStyle HorizontalAlign="Center" Wrap="True">
                        </CellStyle>
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="TerminoLB" VisibleIndex="13" Caption="Término &lt;br&gt; (Atual)"
                        Width="100px">
                        <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                        <CellStyle HorizontalAlign="Center" Wrap="True">
                        </CellStyle>
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="DuracaoLB" VisibleIndex="14" Caption="Duração &lt;br&gt; (Atual)"
                        Width="100px">
                        <HeaderStyle HorizontalAlign="Right" Wrap="True" />
                        <CellStyle HorizontalAlign="Right" Wrap="True">
                        </CellStyle>
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="TrabalhoLB" VisibleIndex="15" Caption="Trabalho &lt;br&gt;(Atual)"
                        Width="100px">
                        <HeaderStyle HorizontalAlign="Right" Wrap="True" />
                        <CellStyle HorizontalAlign="Right" Wrap="True">
                        </CellStyle>
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="CustoLB" VisibleIndex="16" Caption="Custo &lt;br&gt; (Atual)"
                        Width="100px">
                        <HeaderStyle HorizontalAlign="Right" Wrap="True" />
                        <CellStyle HorizontalAlign="Right" Wrap="True">
                        </CellStyle>
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="PercentualFisicoConcluido" Visible="False" VisibleIndex="17">
                        <HeaderStyle HorizontalAlign="Right" />
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="DataExclusao" Visible="False" VisibleIndex="18">
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn FieldName="EventoLinha" VisibleIndex="19" Visible="False">
                    </dxwtl:TreeListTextColumn>
                    <dxwtl:TreeListTextColumn Caption="Formato Duração" FieldName="FormatoDuracao" Visible="False"
                        VisibleIndex="20">
                    </dxwtl:TreeListTextColumn>
                </Columns>
                <Settings SuppressOuterGridLines="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" />
                <SettingsBehavior AllowFocusedNode="True" />

                <SettingsCustomizationWindow PopupHorizontalAlign="RightSides" PopupVerticalAlign="BottomSides"></SettingsCustomizationWindow>

                <SettingsPopupEditForm VerticalOffset="-1"></SettingsPopupEditForm>

                <SettingsPopup>
                    <EditForm VerticalOffset="-1"></EditForm>
                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                </SettingsPopup>

                <Styles>
                    <AlternatingNode BackColor="#EBEBEB" Enabled="True">
                    </AlternatingNode>
                </Styles>
                <ClientSideEvents Init="function(s, e) {
		var height = Math.max(0, document.documentElement.clientHeight) - 55;
	s.SetHeight(height);
}" />
            </dxwtl:ASPxTreeList>
        </div>
        <dxwtle:ASPxTreeListExporter ID="ASPxTreeListExporter1" runat="server"
            TreeListID="tlDados" OnRenderBrick="ASPxTreeListExporter1_RenderBrick">
            <Styles>
                <Header Font-Names="roboto regular">
                </Header>
                <Cell Font-Names="roboto regular">
                </Cell>
            </Styles>
        </dxwtle:ASPxTreeListExporter>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
    </form>
</body>
</html>
