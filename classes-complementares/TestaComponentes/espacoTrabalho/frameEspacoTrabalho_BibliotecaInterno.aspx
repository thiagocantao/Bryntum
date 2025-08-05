<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="frameEspacoTrabalho_BibliotecaInterno.aspx.cs"
    Inherits="espacoTrabalho_frameEspacoTrabalho_Biblioteca" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <style type="text/css">
        .style1
        {
            height: 10px;
        }
    </style>

    <script type="text/javascript">

        function habilitaBotoesListBoxes() {
           
        }
    </script>
 </head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url('../imagens/titulo/back_Titulo_Desktop.gif');
            width: 100%;<%=mostraBarraTitulo %>">
            <tr height="26">
                <td valign="middle">
                    &nbsp; &nbsp;<dx:ASPxLabel ID="lblTitulo" runat="server" ClientInstanceName="lblTituloSelecao"
                        Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" 
                        Text="Anexos">
                    </dx:ASPxLabel>
                </td>
            </tr>
        </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 5px; ">
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>
                                    <dx:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%" ClientInstanceName="pnCallback"
                                        OnCallback="pnCallback_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                <dx:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                                </dx:ASPxHiddenField>
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr style="display:">
                                                        <td class="style1">
                                                        </td>
                                                    </tr>
                                                    <tr style="display: ">
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 30px">
                                                                        <dx:ASPxImage ID="imgPastaRaiz" runat="server" 
                                                                            ClientInstanceName="imgPastaRaiz" Cursor="pointer" 
                                                                            ImageUrl="~/imagens/pastaRaiz.PNG" ToolTip="Incluir Pasta na Raiz">
                                                                            <ClientSideEvents Click="function(s, e) 
{
	abrePopUp('Pasta','IncluirRaiz');
}" />
                                                                        </dx:ASPxImage>
                                                                    </td>
                                                                    <td style="width: 30px">
                                                                        <dx:ASPxImage ID="imgIncluirPastaRaiz" runat="server" 
                                                                            ClientInstanceName="imgIncluirPastaRaiz" Cursor="pointer" 
                                                                            ImageUrl="~/imagens/novoAnexo.png" ToolTip="Incluir Pasta">
                                                                            <ClientSideEvents Click="function(s, e) {
	abrePopUp('Pasta','Incluir');
}" />
                                                                        </dx:ASPxImage>
                                                                    </td>
                                                                    <td style="width: 30px">
                                                                        <dx:ASPxImage ID="imgAnexarArquivoPasta" runat="server" 
                                                                            ClientInstanceName="imgAnexarArquivoPasta" Cursor="pointer" 
                                                                            ImageUrl="~/imagens/anexar.png" ToolTip="Incluir Arquivo">
                                                                            <ClientSideEvents Click="function(s, e) {
	abrePopUp('Arquivo','Incluir');
}" />
                                                                        </dx:ASPxImage>
                                                                    </td>
                                                                    <td style="width: 30px">
                                                                        <dx:ASPxImage ID="imgEditarArquivoPasta" runat="server" 
                                                                            ClientInstanceName="imgEditarArquivoPasta" Cursor="pointer" 
                                                                            ImageUrl="~/imagens/botoes/editarReg02.PNG" ToolTip="Editar">
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
		alert(&quot;Selecione uma pasta ou arquivo para editar.&quot;);
	}
}" />
                                                                        </dx:ASPxImage>
                                                                    </td>
                                                                    <td style="width: 30px">
                                                                        <dx:ASPxImage ID="imgExcluirArquivoPasta" runat="server" 
                                                                            ClientInstanceName="imgExcluirArquivoPasta" ClientVisible="False" 
                                                                            Cursor="pointer" ImageUrl="~/imagens/botoes/excluirReg02.PNG" ToolTip="Excluir">
                                                                            <ClientSideEvents Click="function(s, e) 
{
	var tipoAnexo = hfGeral.Contains(&quot;IndicaPasta&quot;) ? hfGeral.Get(&quot;IndicaPasta&quot;) : &quot;&quot;;
    if(tipoAnexo != &quot;&quot;)
	{
		var confirmaExclusao = confirm(&quot;Deseja realmente excluir o registro?&quot;);
		if(true == confirmaExclusao)
			pnCallback.PerformCallback(&quot;excluir&quot;);
		else
			e.processOnServer =	false;
	}
	else
		alert(&quot;Selecione uma pasta ou arquivo para excluir.&quot;);
}" />
                                                                        </dx:ASPxImage>
                                                                    </td>
                                                                    <td style="width: 30px" valign="middle">
                                                                        <dx:ASPxButton ID="btnCompartilhar" runat="server" AutoPostBack="False" 
                                                                            ClientInstanceName="btnCompartilhar" ClientVisible="False" Height="15px" 
                                                                            ImageSpacing="0px" 
                                                                            ToolTip="Compartilhar arquivo ou pasta com outras entidades." Width="16px" 
                                                                            Wrap="False">
                                                                            <ClientSideEvents Click="function(s, e) {
e.processOnServer = false;	
pcDados.Show();	
}" />
                                                                            <Image Url="~/imagens/compartilhar.PNG">
                                                                            </Image>
                                                                            <FocusRectPaddings Padding="0px" />
                                                                            <FocusRectBorder BorderColor="Transparent" BorderStyle="None" />
                                                                            <Border BorderWidth="0px" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 5px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                <dx:ASPxPanel ID="pnAnexos" runat="server" ClientInstanceName="pnAnexos" style="overflow: scroll"
                                                    Width="100%">
                                                    <panelcollection>
<dx:PanelContent runat="server"><dx:ASPxTreeList runat="server" 
        KeyFieldName="CodigoAnexo" ParentFieldName="CodigoPastaSuperior" 
        AutoGenerateColumns="False" ClientInstanceName="tlAnexos" Width="97%" 
        Font-Names="Verdana" Font-Size="8pt" ID="tlAnexos" __designer:wfdid="w532"><Columns>
<dx:TreeListTextColumn FieldName="Nome" AllowSort="False" Name="Nome" Width="100%" Caption="Anexo" VisibleIndex="0">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<CellStyle HorizontalAlign="Left"></CellStyle>
</dx:TreeListTextColumn>
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
<Paddings Padding="0px"></Paddings>

<ClientSideEvents FocusedNodeChanged="function(s, e) {
	OnFocusedNodeChanged(s);
}"></ClientSideEvents>

<Templates><DataCell>
<TABLE cellSpacing=0 cellPadding=0><TBODY><TR><TD><TABLE><TBODY><TR ><TD><dx:ASPxImage id="imgIndicaCompartilhou" runat="server" ClientInstanceName="imgIndicaCompartilhou" Width="7px" ImageUrl="~/imagens/anexo/indicaCompartilhado.png" ClientVisible="False" Height="7px" __designer:wfdid="w453"></dx:ASPxImage></TD><TD><dx:ASPxImage id="ASPxImage1" runat="server" Width="16" ImageUrl="<%# GetIconUrl(Container) %>" Height="16" __designer:wfdid="w454">
                                                                    </dx:ASPxImage></TD><TD><dx:ASPxButton id="btnDownLoad" onclick="btnDownLoad_Click" runat="server" Width="16px" ToolTip="Visualizar o arquivo" Height="16px" Wrap="False" ImageSpacing="0px" AutoPostBack="False" __designer:wfdid="w455">
                                                                        <Image Url="~/imagens/anexo/download.png" />
                                                                        <FocusRectPaddings Padding="0px" />
                                                                        <FocusRectBorder BorderColor="Transparent" BorderStyle="None" />
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = true;	
}" />
                                                                        <Border BorderWidth="0px" />
                                                                    </dx:ASPxButton></TD><td valign="bottom"><%#GetVersaoIcone(Container)%></td><TD style="WIDTH: 2px"></TD><TD style="PADDING-BOTTOM: 1px" title="<%# GetToolTip(Container) %>"><%#GetVersaoTexto(Container)%><%# Container.Text %></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
</DataCell>
</Templates>
</dx:ASPxTreeList>

 </dx:PanelContent>
</panelcollection>
                                                </dx:ASPxPanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dx:ASPxPopupControl ID="pcMensagemGravacao" runat="server" ClientInstanceName="pcMensagemGravacao"
                                                    Font-Names="Verdana" Font-Size="8pt" HeaderText="Incluir a Entidad Atual" Modal="True"
                                                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                                    ShowHeader="False" Width="270px">
                                                    <ContentCollection>
                                                        <dx:PopupControlContentControl runat="server">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="center" style="font-size: 8pt; font-family: Verdana">
                                                                        </td>
                                                                        <td align="center" rowspan="3" style="width: 70px">
                                                                            <dx:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar" ImageAlign="TextTop"
                                                                                ImageUrl="~/imagens/Workflow/salvarBanco.png">
                                                                            </dx:ASPxImage>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <dx:ASPxLabel ID="lblAcaoGravacao" runat="server" ClientInstanceName="lblAcaoGravacao"
                                                                                Font-Names="Verdana" Font-Size="8pt">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </dx:PopupControlContentControl>
                                                    </ContentCollection>
                                                </dx:ASPxPopupControl>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                        <ClientSideEvents EndCallback="function(s, e) 
{
		//debugger
        if(&quot;Editar&quot; == s.cp_LastOperation)
        {
		
			if(hfGeral.Contains(&quot;ErroSalvar&quot;) &amp;&amp; hfGeral.Get(&quot;ErroSalvar&quot;).toString() != &quot;&quot;)
			{
				alert(hfGeral.Get(&quot;ErroSalvar&quot;).toString());
            }
			else
			{
                 mostraPopupMensagemGravacao(&quot;Compartilhamento alterado com sucesso!&quot;);
			}
            pnCallback.PerformCallback(&quot;setaControles&quot;);			
        }
		else if(hfGeral.Contains(&quot;ErroSalvar&quot;) &amp;&amp; hfGeral.Get(&quot;ErroSalvar&quot;).toString() != &quot;&quot; &amp;&amp; s.cp_LastOperation == &quot;excluir&quot;)
		{
			alert(trataMensagemErro(&quot;Excluir&quot;,hfGeral.Get(&quot;ErroSalvar&quot;).toString()));
            pnCallback.PerformCallback(&quot;setaControles&quot;);
		}
	    else if(&quot;excluir&quot; == s.cp_LastOperation)
			mostraPopupMensagemGravacao(&quot;Registro exclu&#237;do com sucesso!&quot;);
		 
        hfGeral.Set(&quot;ErroSalvar&quot;,&quot;&quot;);
		s.cp_LastOperation = &quot;&quot;;
}" />
                                    </dx:ASPxCallbackPanel>
                                    <table cellpadding="0" cellspacing="0" class="<%=estiloFooter %>" width="100%">
                                        <tr>
                                            <td style="height: 25px" valign="middle">
                                                <span style="font-size: 8pt">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 12px">
                                                                                    <img src="../imagens/anexo/pasta.gif" /></td>
                                                                                <td style="width: 25px">
                                                                                    &nbsp;<dx:ASPxLabel ID="ASPxLabel4" runat="server" ClientInstanceName="lblDescricaoPendiente"
                                                                                        Font-Names="Verdana" Font-Size="7pt" Text="Pasta">
                                                                                    </dx:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                    <img src="../imagens/anexo/pastaCompartilhada.PNG" /></td>
                                                                                <td style="width: 112px">
                                                                                    &nbsp;<dx:ASPxLabel ID="ASPxLabel7" runat="server" ClientInstanceName="lblDescricaoPendiente"
                                                                                        Font-Names="Verdana" Font-Size="7pt" Text="Pasta Compartilhada">
                                                                                    </dx:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                    <img src="../imagens/anexo/arquivo.GIF" /></td>
                                                                                <td style="width: 20px">
                                                                                    &nbsp;<dx:ASPxLabel ID="ASPxLabel8" runat="server" ClientInstanceName="lblDescricaoPendiente"
                                                                                        Font-Names="Verdana" Font-Size="7pt" Text="Arquivo">
                                                                                    </dx:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                    <dx:ASPxImage ID="ASPxImage2" runat="server" Height="16px" ImageUrl="~/imagens/anexo/arquivoCompartilhado.png"
                                                                                        Width="16px">
                                                                                    </dx:ASPxImage>
                                                                                </td>
                                                                                <td style="width: 32px">
                                                                                    &nbsp;<dx:ASPxLabel ID="ASPxLabel9" runat="server" ClientInstanceName="lblDescricaoPendiente"
                                                                                        Font-Names="Verdana" Font-Size="7pt" Text="Arquivo Compartilhado" Width="125px">
                                                                                    </dx:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 11px">
                                                                                    <img src="../imagens/anexo/download.png" /></td>
                                                                                <td style="width: 50px">
                                                                                    &nbsp;<dx:ASPxLabel ID="ASPxLabel10" runat="server" ClientInstanceName="lblDescricaoPendiente"
                                                                                        Font-Names="Verdana" Font-Size="7pt" Text="Download">
                                                                                    </dx:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 10px">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 11px">
                                                                                    <img src="../imagens/anexo/version.png" /></td>
                                                                                <td style="width: 50px">
                                                                                    &nbsp;<dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblDescricaoPendiente"
                                                                                        Font-Names="Verdana" Font-Size="7pt" Text="Versões">
                                                                                    </dx:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <!-- PANEL CALLBACK do treeList -->
                        <dx:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" CloseAction="None"
                            Font-Names="Verdana" Font-Size="8pt" 
                            HeaderText="Compartilhamento de Documentos" 
                            ImageFolder="~/App_Themes/Aqua/{0}/" Modal="True" PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="721px">
                            <ContentStyle VerticalAlign="Top">
                                <Paddings Padding="2px" PaddingLeft="10px" PaddingRight="10px" />
                            </ContentStyle>
                            <ClientSideEvents PopUp="function(s, e) {
	e.processOnServer = false;
	pcDados_OnPopup(s,e);
}" />
                            <ContentCollection>
                                <dx:PopupControlContentControl runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblIndicaDocumentoPasta" runat="server" ClientInstanceName="lblIndicaDocumentoPasta"
                                                        Font-Names="Verdana" Font-Size="8pt" Text="Documento:">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox ID="txtNome" runat="server" ClientInstanceName="txtNome" Font-Names="Verdana"
                                                        Font-Size="8pt" MaxLength="30" ReadOnly="True" Width="100%">
                                                        <ValidationSettings>
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                                        </ReadOnlyStyle>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tbody>
                                                            <tr>
                                                                <td align="center">
                                                                    <dx:ASPxHiddenField ID="hfUnidades" runat="server" ClientInstanceName="hfUnidades">
                                                                    </dx:ASPxHiddenField>
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left" style="width: 325px">
                                                                                    <dx:ASPxLabel ID="lblEntidadesDisponiveis" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                                                        Text="Entidades Dispon&#237;veis:" ClientInstanceName="lblEntidadesDisponiveis">
                                                                                    </dx:ASPxLabel>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td style="width: 5px">
                                                                                </td>
                                                                                <td align="left" style="width: 325px">
                                                                                    <dx:ASPxLabel ID="lblEntidadesSelecionadas" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                                                        Text="Entidades Selecionadas:" ClientInstanceName="lblEntidadesSelecionadas">
                                                                                    </dx:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxListBox ID="lbItensDisponiveis" runat="server" ClientInstanceName="lbItensDisponiveis"
                                                                                        EnableClientSideAPI="True" EncodeHtml="False" Font-Names="Verdana" Font-Size="8pt"
                                                                                        Height="110px" OnCallback="lbItensDisponiveis_Callback" Rows="3"
                                                                                        SelectionMode="Multiple" Width="100%">
                                                                                        <ItemStyle BackColor="White">
                                                                                            <SelectedStyle BackColor="#FFE4AC">
                                                                                            </SelectedStyle>
                                                                                        </ItemStyle>
                                                                                        <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Disp_');
}" Init="function(s, e) {
	habilitaBotoesListBoxes();
}" SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}" />
                                                                                        <ValidationSettings>
                                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" Width="14px">
                                                                                            </ErrorImage>
                                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                                                            </ErrorFrameStyle>
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dx:ASPxListBox>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="height: 28px">
                                                                                                    <dx:ASPxButton ID="btnAddAll" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                                        ClientInstanceName="btnAddAll" EncodeHtml="False" Font-Bold="True" Font-Names="Verdana"
                                                                                                        Font-Size="8pt" Height="25px" Text="&gt;&gt;" ToolTip="Selecionar todas as unidades"
                                                                                                        Width="40px">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensDisponiveis,lbItensSelecionados);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </dx:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="height: 28px">
                                                                                                    <dx:ASPxButton ID="btnAddSel" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                                        ClientInstanceName="btnAddSel" EncodeHtml="False" Font-Bold="True" Font-Names="Verdana"
                                                                                                        Font-Size="8pt" Height="25px" Text="&gt;" ToolTip="Selecionar as unidades marcadas"
                                                                                                        Width="40px">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensDisponiveis, lbItensSelecionados);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </dx:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="height: 28px">
                                                                                                    <dx:ASPxButton ID="btnRemoveSel" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                                        ClientInstanceName="btnRemoveSel" EncodeHtml="False" Font-Bold="True" Font-Names="Verdana"
                                                                                                        Font-Size="8pt" Height="25px" Text="&lt;" ToolTip="Retirar da sele&#231;&#227;o as unidades marcadas"
                                                                                                        Width="40px">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbItensSelecionados, lbItensDisponiveis);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </dx:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="height: 28px">
                                                                                                    <dx:ASPxButton ID="btnRemoveAll" runat="server" AutoPostBack="False" ClientEnabled="False"
                                                                                                        ClientInstanceName="btnRemoveAll" EncodeHtml="False" Font-Bold="True" Font-Names="Verdana"
                                                                                                        Font-Size="8pt" Height="25px" Text="&lt;&lt;" ToolTip="Retirar da sele&#231;&#227;o todas as unidades"
                                                                                                        Width="40px">
                                                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbItensSelecionados, lbItensDisponiveis);
	setListBoxItemsInMemory(lbItensDisponiveis,'Disp_');
	setListBoxItemsInMemory(lbItensSelecionados,'Sel_');
	habilitaBotoesListBoxes();
}" />
                                                                                                        <Paddings Padding="0px" />
                                                                                                    </dx:ASPxButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxListBox ID="lbItensSelecionados" runat="server" ClientInstanceName="lbItensSelecionados"
                                                                                        EnableClientSideAPI="True" EncodeHtml="False" Font-Names="Verdana" Font-Size="8pt"
                                                                                        Height="110px" ImageFolder="~/App_Themes/Aqua/{0}/" OnCallback="lbItensSelecionados_Callback"
                                                                                        Rows="4" SelectionMode="Multiple" Width="100%">
                                                                                        <ItemStyle BackColor="White">
                                                                                            <SelectedStyle BackColor="#FFE4AC">
                                                                                            </SelectedStyle>
                                                                                        </ItemStyle>
                                                                                        <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Sel_');
	setListBoxItemsInMemory(s,'InDB_');
	habilitaBotoesListBoxes();
}" SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}" />
                                                                                        <ValidationSettings>
                                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png" Width="14px">
                                                                                            </ErrorImage>
                                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                                                            </ErrorFrameStyle>
                                                                                        </ValidationSettings>
                                                                                        <DisabledStyle ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dx:ASPxListBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <table id="tblSalvarFechar" border="0" cellpadding="0" cellspacing="0">
                                                        <tbody>
                                                            <tr style="height: 35px">
                                                                <td align="right">
                                                                    <dx:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                        ClientInstanceName="btnSalvar" Font-Names="Verdana" Font-Size="8pt" Text="Salvar"
                                                                        UseSubmitBehavior="False" Width="100px">
                                                                        <ClientSideEvents Click="function(s, e) {
e.processOnServer = false;  
if (window.onClick_btnSalvar)
		onClick_btnSalvar();
}" />
                                                                        <Paddings Padding="0px" />
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td align="right" style="width: 10px">
                                                                </td>
                                                                <td align="right">
                                                                    <dx:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                        Font-Names="Verdana" Font-Size="8pt" Text="Fechar" Width="100px">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    //if (window.onClick_btnCancelar)
    //   onClick_btnCancelar();
pcDados.Hide();
}" />
                                                                        <Paddings Padding="0px" />
                                                                    </dx:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                            <HeaderStyle Font-Bold="True" >
                            <Paddings Padding="6px" />
                            </HeaderStyle>
                        </dx:ASPxPopupControl>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
</form>
</body>
</html>
    
