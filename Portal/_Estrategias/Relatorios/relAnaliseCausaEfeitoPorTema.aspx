<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="relAnaliseCausaEfeitoPorTema.aspx.cs" Inherits="_Estrategias_Relatorios_relAnaliseCausaEfeitoPorTema" Title="Análise de Causa e Efeito por Tema" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table>
        <tr style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); width: 100%;
            height: 26px">
            <td align="left" valign="middle">
                &nbsp;
                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                     Font-Overline="False" Font-Strikeout="False"
                    Text="Análise de Causa e Efeito por Tema"></asp:Label>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
            </td>
            <td >
                <dxrp:aspxroundpanel id="rpFiltro" runat="server" clientinstancename="rpFiltro" 
                    headertext="Filtro" width="800px">
<HeaderStyle Font-Bold="True"></HeaderStyle>
<PanelCollection>
<dxp:PanelContent runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td><dxe:ASPxLabel runat="server" Text="Mapa:" ClientInstanceName="lblMapa"  ID="lblMapa"></dxe:ASPxLabel>
</td></tr><tr><td><dxe:ASPxComboBox runat="server" EnableSynchronization="False" ValueType="System.String" Width="100%" ClientInstanceName="ddlMapa"  ID="ddlMapa">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlTemaEstrategico.PerformCallback(s.GetValue());
}"></ClientSideEvents>
</dxe:ASPxComboBox>
</td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td><dxe:ASPxLabel runat="server" Text="Tema Estrat&#233;gico:" ClientInstanceName="lblTemaEstrategico"  ID="lblTemaEstrategico" ></dxe:ASPxLabel>
</td></tr><tr><td><dxe:ASPxComboBox runat="server" EnableSynchronization="False" ValueType="System.String" Width="100%" ClientInstanceName="ddlTemaEstrategico"  ID="ddlTemaEstrategico"  OnCallback="ddlTemaEstrategico_Callback">
<ClientSideEvents EndCallback="function(s, e) {
	tlRelatorioCausaEfeito.PerformCallback();
}" SelectedIndexChanged="function(s, e) {
	//s.PerformCallback(s.GetValue());
	//callBack.PerformCallback('ddl'+s.GetValue());
	tlRelatorioCausaEfeito.PerformCallback(s.GetValue());
}"></ClientSideEvents>
</dxe:ASPxComboBox>
</td></tr></tbody></table></dxp:PanelContent>
</PanelCollection>
</dxrp:aspxroundpanel>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
            <td style="display: none;" id="tdExport">
                <table>
                    <tr>
                        <td>
                        </td>
                        <td align="center" valign="top">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px">
                            <dxe:aspxcombobox id="ddlExporta" runat="server" clientinstancename="ddlExporta"
                                 valuetype="System.String" width="100%">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set('tipoArquivo',s.GetValue());
}"></ClientSideEvents>
</dxe:aspxcombobox>
                        </td>
                        <td align="center" style="width: 40px" valign="top">
                            <dxcp:aspxcallbackpanel id="pnImage" runat="server" clientinstancename="pnImage"
                                height="22px" hidecontentoncallback="False" oncallback="pnImage_Callback" 
                                 width="23px"><PanelCollection>
<dxp:PanelContent runat="server"><dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png" Width="20px" Height="20px" ClientInstanceName="imgExportacao" ID="imgExportacao" ></dxe:ASPxImage>
 </dxp:PanelContent>
</PanelCollection>
</dxcp:aspxcallbackpanel>
                        </td>
                        <td style="width: 100px">
                            <dxe:aspxbutton id="btnExportar" runat="server" 
                                onclick="btnExportar_Click" text="Exportar" width="100%" clientinstancename="btnExportar">
<Paddings Padding="0px"></Paddings>
</dxe:aspxbutton>
                        </td>
                        <td style="width: 100px">
                            <dxhf:aspxhiddenfield id="hfGeral" runat="server" clientinstancename="hfGeral">
                            </dxhf:aspxhiddenfield>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="center" valign="top">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="padding-top: 5px; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid;">
                <dxwtle:aspxtreelistexporter id="pvgCausaEfeito" runat="server" treelistid="tlRelatorioCausaEfeito" onrenderbrick="pvgCausaEfeito_RenderBrick">
<Settings AutoWidth="True" ExpandAllNodes="True" ExportAllPages="True">
<PageSettings Landscape="True">
<Margins Left="20" Right="20" Top="50" Bottom="50"></Margins>
</PageSettings>
</Settings>

<Styles>
<Cell Wrap="False"></Cell>
</Styles>
</dxwtle:aspxtreelistexporter>
                
                <dxwtl:aspxtreelist id="tlRelatorioCausaEfeito" runat="server" autogeneratecolumns="False"
                clientinstancename="tlRelatorioCausaEfeito" clientvisible="False" 
                keyfieldname="Codigo" parentfieldname="CodigoSuperior" width="100%" oncustomcallback="tlRelatorioCausaEfeito_CustomCallback">
                    <Settings VerticalScrollBarMode="Visible" />
<SettingsBehavior AutoExpandAllNodes="True"></SettingsBehavior>

<ClientSideEvents EndCallback="function(s, e) {
				End_CallbackTree(s, e);
                }"></ClientSideEvents>
<Columns>
<dxwtl:TreeListTextColumn FieldName="Descricao" Name="Descricao" Width="95%" Caption="Descri&#231;&#227;o" VisibleIndex="0">
<DataCellTemplate>
 <%# (("OBJ" == Eval("Tipo").ToString()) ?
         "<table><tr><td><img src='../../imagens/mapaEstrategico/Objetivo.png' /></td><td>" + Eval("Descricao").ToString() + "</td></table>"
         :
              ("IND" == Eval("Tipo").ToString()) ?
                           "<table><tr><td><img src='../../imagens/mapaEstrategico/indicador.png' /></td><td>" + Eval("Descricao").ToString() + "</td></table>"
            :
                   ("PRJ" == Eval("Tipo").ToString()) ?
                                     "<table><tr><td><img src='../../imagens/mapaEstrategico/projeto.png' /></td><td>" + Eval("Descricao").ToString() + "</td></table>"
                 :
                          Eval("Descricao").ToString()) %>
</DataCellTemplate>
</dxwtl:TreeListTextColumn>

<dxwtl:TreeListTextColumn FieldName="Status" Name="Status" Width="5%" Caption="Status" VisibleIndex="1"><DataCellTemplate>
                <%# Eval("Status").ToString() != "" ? "<img src='../../imagens/" + Eval("Status").ToString().Trim() + ".gif' />" : ""%>
                
</DataCellTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center" ></CellStyle>
</dxwtl:TreeListTextColumn>
<dxwtl:TreeListTextColumn FieldName="Tipo" Name="Tipo" Caption="Tipo" Visible="False" VisibleIndex="2"></dxwtl:TreeListTextColumn>
</Columns>
</dxwtl:aspxtreelist>
                
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
           <td class="<%=estiloFooter %>" >
                <table cellspacing="0" 
        cellpadding="0" __designer:mapid="428"><TBODY __designer:mapid="4c4"><TR __designer:mapid="4c5">
                        <td __designer:mapid="4c6"><IMG alt="" src="../../imagens/verde.gif" 
                                __designer:mapid="4c7" /></td><td __designer:mapid="4c8"><asp:Label runat="server" Text="Satisfat&#243;rio" Font-Size="7pt"  ID="Label1" EnableViewState="False"></asp:Label>

 </td><td style="WIDTH: 10px" __designer:mapid="4ca"></td><td __designer:mapid="4cb"><IMG alt="" 
                            src="../../imagens/amarelo.gif" __designer:mapid="4cc" /></td>
                        <td __designer:mapid="4cd"><asp:Label runat="server" Text="Aten&#231;&#227;o" Font-Size="7pt"  ID="Label2" EnableViewState="False"></asp:Label>

 </td><td style="WIDTH: 10px" __designer:mapid="4cf"></td><td __designer:mapid="4d0">
                        <IMG style="MARGIN-TOP: 0px" alt="" src="../../imagens/vermelho.gif" 
                            __designer:mapid="4d1" /></td><td __designer:mapid="4d2"><asp:Label runat="server" Text="Cr&#237;tico" Font-Size="7pt"  ID="Label3" EnableViewState="False"></asp:Label>

 </td><td style="WIDTH: 10px" __designer:mapid="4d4"></td><td __designer:mapid="4d5">
                        <IMG style="MARGIN-TOP: 0px" alt="" src="../../imagens/Branco.gif" 
                            __designer:mapid="4d6" /></td><td __designer:mapid="4d7"><asp:Label runat="server" Text="Sem informa&#231;&#227;o" Font-Size="7pt"  ID="Label7" EnableViewState="False"></asp:Label>

 </td><td style="WIDTH: 10px" __designer:mapid="4d9"></td><td __designer:mapid="4da"><asp:Image runat="server" ImageUrl="~/imagens/mapaEstrategico/Objetivo.png" ID="Image2" EnableViewState="False"></asp:Image>

 </td><td __designer:mapid="4dc"><asp:Label runat="server" Text="Objetivos" Font-Size="7pt"  ID="Label4" EnableViewState="False"></asp:Label>

 </td><td style="WIDTH: 10px" __designer:mapid="4de"></td><td __designer:mapid="4df"><asp:Image runat="server" ImageUrl="~/imagens/mapaEstrategico/indicador.png" ID="Image3" EnableViewState="False"></asp:Image>

 </td><td __designer:mapid="4e1"><asp:Label runat="server" Text="Indicador" Font-Size="7pt" ID="Label5" 
                                EnableViewState="False" ></asp:Label>

 </td><td style="WIDTH: 10px" __designer:mapid="4e3"></td><td __designer:mapid="4e4"><asp:Image runat="server" ImageUrl="~/imagens/mapaEstrategico/projeto.PNG" ID="Image1" EnableViewState="False"></asp:Image>

 </td><td __designer:mapid="4e6"><asp:Label runat="server" Text="Projeto" Font-Size="7pt" ID="Label6" 
                                EnableViewState="False" ></asp:Label>

 </td></tr></tbody></table>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>