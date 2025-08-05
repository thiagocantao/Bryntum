<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CadastroDocumentos.aspx.cs"
    Inherits="administracao_CadastroDocumentos" Title="Documentos" %>
    <%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript" language="javascript" src="../scripts/AnexosCadastroDocumentos.js"></script>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr>
                <td valign="middle" style="height: 26px">
                    &nbsp; &nbsp;
                    <dxe:ASPxLabel ID="lblTitulo" runat="server" Font-Bold="True"
                        Text="Biblioteca de Documentos">
                    </dxe:ASPxLabel>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 10px; height: 21px;">
                            </td>
                            <td style="height: 21px; padding-top: 4px;" valign="middle">
                                <table>
                                    <tr>
                                        <td style="width: 30px">
                                            <dxe:ASPxImage ID="imgPastaRaiz" runat="server" ClientInstanceName="imgPastaRaiz"
                                                Cursor="pointer" ImageUrl="~/imagens/pastaRaiz.PNG" ToolTip="Incluir Pasta na Raiz">
                                                <ClientSideEvents Click="function(s, e) 
{
	abrePopUp('Pasta','IncluirRaiz');
}" />
                                            </dxe:ASPxImage>
                                        </td>
                                        <td style="width: 30px">
                                            <dxe:ASPxImage ID="imgIncluirPastaRaiz" runat="server" ClientInstanceName="imgIncluirPastaRaiz"
                                                Cursor="pointer" ImageUrl="~/imagens/novoAnexo.png" ToolTip="Incluir Pasta">
                                                <ClientSideEvents Click="function(s, e) {
	abrePopUp('Pasta','Incluir');
}" />
                                            </dxe:ASPxImage>
                                        </td>
                                        <td style="width: 30px">
                                            <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgIncluirPastaRaiz"
                                                Cursor="pointer" ImageUrl="~/imagens/anexar.png" ToolTip="Incluir Arquivo">
                                                <ClientSideEvents Click="function(s, e) {
	abrePopUp('Arquivo','Incluir');
}" />
                                            </dxe:ASPxImage>
                                        </td>
                                        <td style="width: 30px">
                                            <dxe:ASPxImage ID="ASPxImage3" runat="server" ClientInstanceName="imgIncluirPastaRaiz"
                                                Cursor="pointer" ImageUrl="~/imagens/botoes/editarReg02.PNG" ToolTip="Editar">
                                                <ClientSideEvents Click="function(s, e) {
	var tipoAnexo = hfGeral.Contains(&quot;IndicaPasta&quot;) ? hfGeral.Get(&quot;IndicaPasta&quot;) : &quot;&quot;;
	if(tipoAnexo != &quot;&quot;)
	{
		if(tipoAnexo == &quot;S&quot;)
		{
			abrePopUp('Pasta','Editar'); 
		} 	
		else
		{
			abrePopUp('Arquivo','Editar'); 
		}
	}
	else
	{
		window.top.mostraMensagem(&quot;Selecione uma pasta ou arquivo para editar.&quot;, 'Atencao', true, false, null);
	}
}

" />
                                            </dxe:ASPxImage>
                                        </td>
                                        <td>
                                            <dxe:ASPxImage ID="ASPxImage4" runat="server" Cursor="pointer" ImageUrl="~/imagens/botoes/excluirReg02.PNG"
                                                ToolTip="Excluir">
                                                <ClientSideEvents Click="function(s, e) 
{
	var tipoAnexo = hfGeral.Contains(&quot;IndicaPasta&quot;) ? hfGeral.Get(&quot;IndicaPasta&quot;) : &quot;&quot;;
    if(tipoAnexo != &quot;&quot;)
	{
		var confirmaExclusao = confirm(&quot;Deseja realmente excluir o registro?&quot;);
		if(true == confirmaExclusao)
			pnCallback.PerformCallback(&quot;excluir&quot;);
		else
		{
			e.processOnServer =	false;
		}
	}
	else
		window.top.mostraMensagem(&quot;Selecione uma pasta ou arquivo para excluir.&quot;, 'Atencao', true, false, null);
}" />
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="height: 21px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                                    HideContentOnCallback="False" OnCallback="pnCallback_Callback" 
                                    >
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server"><dxp:ASPxPanel ID="pnAnexos" runat="server" ClientInstanceName="pnAnexos" style="overflow: scroll"
                                                Width="100%"><panelcollection>
<dxp:PanelContent runat="server"><dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"  ></dxhf:ASPxHiddenField>

 <dxwtl:ASPxTreeList runat="server" KeyFieldName="CodigoAnexo" ParentFieldName="CodigoPastaSuperior" AutoGenerateColumns="False" ClientInstanceName="tlAnexos" Width="97%" Height="95%"  ID="tlAnexos" ><Columns>
<dxwtl:TreeListTextColumn FieldName="Nome" AllowSort="False" Name="Nome" Width="100%" Caption="Anexo" VisibleIndex="0">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<CellStyle HorizontalAlign="Left"></CellStyle>
</dxwtl:TreeListTextColumn>
</Columns>

<Settings ShowColumnHeaders="False"></Settings>

<SettingsBehavior AutoExpandAllNodes="True" AllowFocusedNode="True" FocusNodeOnLoad="False"></SettingsBehavior>

<Styles>
<Indent BackColor="Transparent"></Indent>

<IndentWithButton BackColor="Transparent"></IndentWithButton>

<Node BackColor="Transparent"></Node>

<Cell>
<Paddings PaddingLeft="1px"></Paddings>
</Cell>
</Styles>

<ClientSideEvents FocusedNodeChanged="function(s, e) {
	OnFocusedNodeChanged(s);
}" EndCallback="function(s, e) {
	//onEnd_pnCallback(s);
}"></ClientSideEvents>

<Templates><DataCell>
                                                            <table  cellpadding="0" cellspacing="0">
                                                                <tr >
                                                                    <td >
                                                                        <table >
                                                                            <tr >
                                                                                <td >
                                                                                    <dxe:ASPxImage  ID="ASPxImage1" runat="server" Height="16" ImageUrl='<%# GetIconUrl(Container) %>'
                                                                                          Width="16">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                                <td >
                                                                                    <dxe:ASPxButton  ID="btnDownLoad" runat="server" AutoPostBack="False" Height="16px"
                                                                                        ImageSpacing="0px" OnClick="btnDownLoad_Click" ToolTip="Visualizar o arquivo"
                                                                                        Width="16px" Wrap="False">
                                                                                        <Image  Url="../imagens/anexo/download.png"  />
                                                                                        <FocusRectPaddings  Padding="0px"  />
                                                                                        <FocusRectBorder  BorderColor="Transparent" BorderStyle="None"  />
                                                                                        <ClientSideEvents  Click="function(s, e) {
	e.processOnServer = true;	
}"  />
                                                                                        <Paddings  Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                                                            PaddingTop="0px"  />
                                                                                        <Border  BorderStyle="None" BorderWidth="0px"  />
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td  style="width: 2px">
                                                                    </td>
                                                                    <td  style="padding-bottom: 1px;" title="<%# GetToolTip(Container) %>">
                                                                        <%# Container.Text %>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        
</DataCell>
</Templates>
</dxwtl:ASPxTreeList>

 </dxp:PanelContent>
</panelcollection>
</dxp:ASPxPanel>
 </dxp:PanelContent>
                                    </PanelCollection>
                                    <ClientSideEvents EndCallback="function(s, e) {
	if(hfGeral.Contains(&quot;hfErro&quot;) &amp;&amp; hfGeral.Get(&quot;hfErro&quot;) != &quot;&quot;)
		window.top.mostraMensagem(hfGeral.Get(&quot;hfErro&quot;), 'Atencao', true, false, null);
}" />
                                </dxcp:ASPxCallbackPanel>
                                <!-- PANEL CALLBACK do treeList -->
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
