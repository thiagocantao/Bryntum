<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="listaProjetos.aspx.cs" Inherits="_Portfolios_listaProjetos" Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height:26px">
            <td valign="middle" style="height: 26px; padding-left: 10px;">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                    Text="Projetos de Portfólio" 
                    ClientInstanceName="lblTituloTela">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 10px; height: 10px">
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td style="border-bottom: lightgrey 1px solid">
                <dxwtl:aspxtreelist id="tvDados" runat="server" autogeneratecolumns="False" clientinstancename="tvDados"
                     keyfieldname="Codigo" 
                    parentfieldname="CodigoPai" 
                    OnHtmlDataCellPrepared="tvDados_HtmlDataCellPrepared" Width="100%"><Columns>
                        <dxwtl:TreeListTextColumn Caption="Descri&#231;&#227;o" FieldName="Descricao" Name="Descricao"
                            VisibleIndex="0" Width="30%">
                            <DataCellTemplate>
        <%# (Eval("CodigoProjeto").ToString() == "0") ? Eval("Descricao").ToString() : "<a href='../_Projetos/DadosProjeto/indexResumoProjeto.aspx?IDProjeto=" + Eval("CodigoProjeto") + "&NomeProjeto=" + Eval("Descricao") + "'>" + Eval("Descricao").ToString() + "</a>"%>
                            </DataCellTemplate>
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwtl:TreeListTextColumn>
                        <dxwtl:TreeListTextColumn Caption="Pontua&#231;&#227;o" FieldName="ScoreCriterios"
                            Name="Criterios" VisibleIndex="1" Width="10%">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwtl:TreeListTextColumn>
                        <dxwtl:TreeListTextColumn Caption="Riscos" FieldName="ScoreRiscos" Name="Riscos"
                            VisibleIndex="2" Width="10%">
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwtl:TreeListTextColumn>
                        <dxwtl:TreeListTextColumn Caption="Desempenho" FieldName="Desempenho" Name="Desempenho"
                            VisibleIndex="3" Width="12%">
                            <DataCellTemplate>
                                <%# (Eval("CodigoProjeto").ToString() == "0") ? "&nbsp;" : "<img border='0' src='../imagens/" + Eval("Desempenho").ToString().Trim() + ".gif' />"%>
                            </DataCellTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwtl:TreeListTextColumn>
                        <dxwtl:TreeListTextColumn Caption="Despesa (R$)" FieldName="Despesa" Name="Despesa"
                            VisibleIndex="4" Width="12%">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Right" />
                        </dxwtl:TreeListTextColumn>
                        <dxwtl:TreeListTextColumn Caption="Receita (R$)" FieldName="Receita" Name="Receita"
                            VisibleIndex="5" Width="12%">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Right" />
                        </dxwtl:TreeListTextColumn>
                        <dxwtl:TreeListTextColumn Caption="Status" FieldName="Status" Name="Status" VisibleIndex="6"
                            Width="12%">
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwtl:TreeListTextColumn>
</Columns>
                    <Settings VerticalScrollBarMode="Visible" />
</dxwtl:aspxtreelist>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 5px">
            </td>
            <td class="<%=estiloFooter %>" >
                            <table cellpadding="0" cellspacing="0" __designer:mapid="752">
                                <tbody __designer:mapid="753">
                                    <tr __designer:mapid="754">
                                        <td style="width: 20px" __designer:mapid="755">
                                            <img src="../imagens/verdeMenor.gif" __designer:mapid="756" /></td>
                                        <td style="width: 65px" __designer:mapid="757">
                                            <span style="" __designer:mapid="758">
                                                <asp:Label runat="server" Text="Satisfat&#243;rio" Font-Size="7pt" 
                                                ID="lblVerde" EnableViewState="False"></asp:Label>

                                            </span>
                                        </td>
                                        <td style="width: 20px" __designer:mapid="75a">
                                            <img src="../imagens/amareloMenor.gif" __designer:mapid="75b" /></td>
                                        <td style="width: 55px" __designer:mapid="75c">
                                            <span style="" __designer:mapid="75d">
                                                <asp:Label runat="server" Text="Aten&#231;&#227;o" Font-Size="7pt" 
                                                ID="lblAmarelo" EnableViewState="False"></asp:Label>

                                            </span>
                                        </td>
                                        <td style="width: 20px" __designer:mapid="75f">
                                            <img src="../imagens/vermelhoMenor.gif" __designer:mapid="760" /></td>
                                        <td style="width: 45px" __designer:mapid="761">
                                            <span style="" __designer:mapid="762">
                                                <asp:Label runat="server" Text="Cr&#237;tico" Font-Size="7pt" 
                                                ID="lblVermelho" EnableViewState="False"></asp:Label>

                                            </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

