<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="detalhesObjetivoEstrategico.aspx.cs" Inherits="_Estrategias_detalhesObjetivoEstrategico" Title="Portal da Estratégia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <a href="indicador/principalIndicador.aspx?COIN=2"></a><table border="0" cellpadding="0"
        cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height:26px">
            <td valign="middle">
                &nbsp; &nbsp;<dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTituloSelecao"
                    Font-Bold="True"  Text="Detalhes do objetivo estratégico">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 5px; height: 3px">
            </td>
            <td style="height: 3px">
                </td>
            <td style="width: 5px; height: 3px">
            </td>
        </tr>
        <tr>
            <td style="width: 5px; height: 6px">
            </td>
            <td>
                <asp:Label ID="Label1" runat="server"  Text="Mapa Estratégico:"></asp:Label></td>
            <td style="width: 5px; height: 6px">
            </td>
        </tr>
        <tr>
            <td style="width: 5px; height: 6px">
            </td>
            <td>
                <dxe:ASPxTextBox ID="txtMapa" runat="server" ClientInstanceName="txtMapa"
                     ReadOnly="True" Width="100%">
                    <ReadOnlyStyle BackColor="#E0E0E0">
                    </ReadOnlyStyle>
                </dxe:ASPxTextBox>
            </td>
            <td style="width: 5px; height: 6px">
            </td>
        </tr>
        <tr>
            <td style="width: 5px; height: 6px">
            </td>
            <td style="height: 6px">
            </td>
            <td style="width: 5px; height: 6px">
            </td>
        </tr>
        <tr>
            <td style="width: 5px; height: 6px">
            </td>
            <td style="height: 6px">
                <table>
                    <tr>
                        <td style="width: 480px">
                            <asp:Label ID="lblPerspectiva" runat="server" 
                                Text="Perspectiva:"></asp:Label></td>
                        <td>
                        </td>
                        <td valign="top">
                                        <asp:Label ID="lblObjetivoEstrategico" runat="server" 
                                            Text="Objetivo Estratégico:"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 49%"><dxe:ASPxTextBox ID="txtPerspectiva" runat="server" ClientInstanceName="txtPerspectiva"
                     ReadOnly="True" Width="100%">
                            <ReadOnlyStyle BackColor="#E0E0E0">
                            </ReadOnlyStyle>
                        </dxe:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td valign="top">
                <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server" ClientInstanceName="txtObjetivoEstrategico"
                     ReadOnly="True" Width="100%">
                    <ReadOnlyStyle BackColor="#E0E0E0">
                    </ReadOnlyStyle>
                </dxe:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 5px; height: 6px">
            </td>
        </tr>
        <tr>
            <td style="width: 5px; height: 10px">
            </td>
            <td style="height: 10px">
            </td>
            <td style="width: 5px; height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 5px; height: 5px">
            </td>
            <td>
                <dxwgv:ASPxGridView ID="gridIndicadores" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="gridIndicadores"  OnBeforeColumnSortingGrouping="gridIndicadores_BeforeColumnSortingGrouping"
                    Width="100%"><SettingsPager Mode="ShowAllRecords" Visible="False">
                    </SettingsPager>
                    <SettingsText  />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="Indicador" FieldName="CodigoIndicador" VisibleIndex="0"
                            Width="90%">
                            <PropertiesTextEdit DisplayFormatString="{0}">
                            </PropertiesTextEdit>
                            <DataItemTemplate>
                                <a href='./indicador/index.aspx?COIN=<%# Eval("CodigoIndicador") %>&<%# Request.QueryString.ToString() %>'
                                    style="cursor: pointer">
                                    <%# Eval("NomeIndicador") %>
                                </a>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusDesempenho" VisibleIndex="1"
                            Width="10%">
                            <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/{0}.gif' style='border-width:0px;' /&gt;">
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="150" />
                </dxwgv:ASPxGridView>
            </td>
            <td style="width: 5px; height: 5px">
            </td>
        </tr>
        <tr>
            <td style="width: 5px; height: 10px">
            </td>
            <td style="height: 10px">
                </td>
            <td style="width: 5px; height: 10px">
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td><dxwgv:ASPxGridView ID="gridProjetos" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="gridProjetos"  OnBeforeColumnSortingGrouping="gridIndicadores_BeforeColumnSortingGrouping"
                    Width="100%">
                <SettingsPager Mode="ShowAllRecords" Visible="False">
                </SettingsPager>
                <SettingsText  />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Projeto" FieldName="NomeProjeto" VisibleIndex="0"
                            Width="55%">
                        <PropertiesTextEdit DisplayFormatString="{0}">
                        </PropertiesTextEdit>
                        <DataItemTemplate>
                            <a href='../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=<%# Eval("CodigoProjeto")%>&NomeProjeto=<%# Eval("NomeProjeto")%>'
                                    style="cursor: pointer">
                                <%# Eval("NomeProjeto") %>
                            </a>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Respons&#225;vel" VisibleIndex="1" Width="25%" FieldName="Responsavel">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Conclu&#237;do" VisibleIndex="2" Width="10%" FieldName="Concluido">
                        <HeaderStyle HorizontalAlign="Right" />
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesTextEdit DisplayFormatString="{0:p0}" EncodeHtml="False">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" VisibleIndex="3"
                            Width="10%">
                        <PropertiesTextEdit DisplayFormatString="&lt;img src='../imagens/{0}.gif' style='border-width:0px;' /&gt;">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="150" />
            </dxwgv:ASPxGridView>
            </td>
            <td style="width: 5px">
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td>
            </td>
            <td style="width: 5px">
            </td>
        </tr>
    </table>
</asp:Content>

