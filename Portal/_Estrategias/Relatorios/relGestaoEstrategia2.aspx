<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="relGestaoEstrategia2.aspx.cs" Inherits="_Estrategias_Relatorios_relGestaoEstrategia2" Title="Untitled Page" %>
<%@ Register assembly="DevExpress.XtraReports.v19.1.Web.WebForms, Version=19.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<%@ MasterType VirtualPath="~/novaCdis.master"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" Runat="Server">
  <div>
        <table border="0" cellpadding="0" cellspacing="0"
            width="100%">
            <tr style="height:26px">
                <td valign="top" align="right">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo.gif);
                        width: 100%">
                        <tr>
                            <td align="right" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif); height: 26px">
                                <table>
                                    <tr>
                                        <td align="left" valign="middle" style="padding-left: 10px">
                                           <dxe:ASPxLabel ID="lblTituloTela" runat="server" 
                                                ClientInstanceName="lblTituloTela" Font-Bold="True"
                         Text="Relatório de Gestão da Estratégia">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height:26px">
                <td valign="middle" align="left" style="padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <dxcp:aspxcallbackpanel id="pnRelatorio" runat="server" clientinstancename="pnRelatorio"
                        oncallback="pnRelatorio_Callback" width="100%"><PanelCollection>
<dxp:PanelContent runat="server"><table style="WIDTH: 100%" cellspacing="0" cellpadding="0" border="0"><tbody>
    <tr>
        <td align="left">
            <table>
                <tr>
                    <td align="left" valign="middle">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblTitulo" Font-Bold="False"
                         Text="Mapa:">
                                            </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                                        <td align="left" valign="middle">
                                            <dxe:ASPxComboBox ID="ddlMapa" runat="server" ClientInstanceName="ddlMapa"
                                                Width="377px" ValueType="System.String" AutoPostBack="True">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	e.processOnServer = false;
	//hfGeral.Set(&quot;CodigoMapa&quot;,s.GetSelectedItem().value);
	var option = s.GetSelectedItem().value;
	var nome = s.GetSelectedItem().text;

	var parametro = option + &quot;|&quot; + nome;
	
	hfGeral.Set('CodigoMapaSelecionado', option );
    hfGeral.Set('NomeMapaSelecionado', nome );
	
	pnRelatorio.PerformCallback(parametro);
}"></ClientSideEvents>
</dxe:ASPxComboBox>
                                        </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height: 7px">
        </td>
    </tr>
    <tr><td><table style="WIDTH: 100%" cellspacing="0" cellpadding="0" border="0"><tbody><tr><td style="WIDTH: 354px"><dx:ReportToolbar runat="server" ShowDefaultButtons="False" ReportViewer="<%# ReportViewer1 %>" Width="62%" ID="ReportToolbar1" ><Items>
<dx:ReportToolbarButton ItemKind="Search"></dx:ReportToolbarButton>
<dx:ReportToolbarSeparator></dx:ReportToolbarSeparator>
<dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage"></dx:ReportToolbarButton>
<dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage"></dx:ReportToolbarButton>
<dx:ReportToolbarLabel ItemKind="PageLabel" Text="P&#225;gina"></dx:ReportToolbarLabel>
<dx:ReportToolbarComboBox Width="65px" ItemKind="PageNumber"></dx:ReportToolbarComboBox>
<dx:ReportToolbarLabel ItemKind="OfLabel" Text="de"></dx:ReportToolbarLabel>
<dx:ReportToolbarTextBox ItemKind="PageCount"></dx:ReportToolbarTextBox>
<dx:ReportToolbarButton ItemKind="NextPage"></dx:ReportToolbarButton>
<dx:ReportToolbarButton ItemKind="LastPage"></dx:ReportToolbarButton>
</Items>

<Styles>
<LabelStyle>
<Margins MarginLeft="3px" MarginRight="3px"></Margins>
</LabelStyle>
</Styles>
</dx:ReportToolbar>
 </td><td>
     <dxe:ASPxButton ID="ASPxButton1" runat="server" 
         Text="Gerar PDF">
         <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    var nomeMapa = hfGeral.Contains(&quot;NomeMapaSelecionado&quot;) ? hfGeral.Get(&quot;NomeMapaSelecionado&quot;).toString() : &quot;&quot;;
    var codMapa = hfGeral.Contains(&quot;CodigoMapaSelecionado&quot;) ? hfGeral.Get(&quot;CodigoMapaSelecionado&quot;).toString() : -1;
	window.top.showModal(&quot;popupRelGestaoEstrategia2.aspx?NM=&quot; + nomeMapa + &quot;&amp;CM=&quot; + codMapa, 'Gestão Estratégica', screen.width - 60, screen.height - 260, '', null);
}" />
         <Image Url="~/imagens/menuExportacao/iconoPDF.png">
         </Image>
         <Paddings Padding="0px" />
     </dxe:ASPxButton>
 </td></tr></tbody></table></td></tr><tr><td><DIV style="OVERFLOW: auto; WIDTH: 100%; height:<%=alturaDivGrid %>" id="divRelatorio"><dx:ReportViewer runat="server" ClientInstanceName="ReportViewer1" AutoSize="False" Width="95%" Height="93%" ID="ReportViewer1">
<Paddings PaddingLeft="5px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dx:ReportViewer>
 </DIV></td></tr></tbody></table></dxp:PanelContent>
</PanelCollection>
</dxcp:aspxcallbackpanel>

                </td>
            </tr>
        </table>
        </div>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
</asp:Content>

