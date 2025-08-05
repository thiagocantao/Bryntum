<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="frameEspacoTrabalho_AprovacoesHorasProjetos.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_AprovacoesTarefas"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height: 26px">
            <td valign="middle">
                &nbsp; &nbsp; &nbsp;
                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True"
                    Text="Aprovação de Timesheet">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 10px; height: 5px;">
            </td>
            <td align="right">
            </td>
            <td style="width: 10px; height: 5px;">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                    Text="Recurso:">
                </dxe:ASPxLabel>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table>
                    <tr>
                        <td style="width: 265px">
                            <dxe:ASPxComboBox ID="ddlRecurso" runat="server" ClientInstanceName="ddlRecurso"
                                 IncrementalFilteringMode="Contains" TextField="NomeUsuario"
                                TextFormatString="{0}" ValueField="CodigoUsuario" ValueType="System.String" Width="257px">
                                <Columns>
                                    <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="300px" />
                                    <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="200px" />
                                </Columns>
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback('');
}" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxImage ID="imgAtualiza" runat="server" ClientInstanceName="imgAtualiza" Cursor="pointer"
                                ImageUrl="~/imagens/atualizar.PNG" ToolTip="Atualizar">
                                <ClientSideEvents Click="function(s, e) {
	gvDados.PerformCallback();
}"></ClientSideEvents>
                            </dxe:ASPxImage>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 10px; height: 5px;">
            </td>
            <td>
            </td>
            <td style="width: 10px; height: 5px;">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxwgv:ASPxGridView ID="gvDados" runat="server" ClientInstanceName="gvDados"
                    Width="100%" KeyFieldName="EstruturaHierarquica" PreviewFieldName="EstruturaHierarquica"
                    OnCustomCallback="gvDados_CustomCallback" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared"
                    OnSummaryDisplayText="gvDados_SummaryDisplayText">
                    <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" ShowFooter="True" />
                    <StylesEditors>
                        <TextBox  BackColor="Transparent">
                        </TextBox>
                        <ReadOnly BackColor="Transparent">
                            <Border BorderStyle="None" />
                        </ReadOnly>
                    </StylesEditors>
                    <Styles>
                        <FocusedRow BackColor="Transparent" ForeColor="Black">
                        </FocusedRow>
                    </Styles>
                    <SettingsText EmptyHeaders="  " />
                    <ClientSideEvents CustomButtonClick="function(s, e) {
	if(e.buttonID == &quot;btnComentarios&quot;)
	    {
			abreComentarios();	
     	}
     
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
            <td style="height: 5px; width: 10px;">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 425px">
                            <dxrp:ASPxRoundPanel ID="pLegenda" runat="server" 
                                HeaderText="Legenda" Width="415px" Font-Bold="True">
                                <ContentPaddings Padding="1px"></ContentPaddings>
                                <HeaderStyle>
                                    <Paddings Padding="1px" PaddingLeft="3px"></Paddings>
                                </HeaderStyle>
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table cellspacing="0" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 26px">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/tarefasPA.png" ID="ASPxImage2">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td style="width: 130px">
                                                        <dxe:ASPxLabel runat="server" Text="Pendente Aprova&#231;&#227;o" Font-Bold="False"
                                                             ID="ASPxLabel8">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 25px">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/tarefasEA.PNG" ID="ASPxImage3">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td style="width: 95px">
                                                        <dxe:ASPxLabel runat="server" Text="Em Aprova&#231;&#227;o" Font-Bold="False"
                                                            Font-Size="7pt" ID="ASPxLabel9">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 25px">
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/tarefasER.PNG" ID="ASPxImage4">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Em Reprova&#231;&#227;o" Font-Bold="False"
                                                            Font-Size="7pt" ID="ASPxLabel10">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxrp:ASPxRoundPanel>
                        </td>
                        <td style="width: 175px">
                            <dxe:ASPxComboBox ID="ddlAcao" runat="server" 
                                SelectedIndex="0" ValueType="System.String" Width="100%" ClientInstanceName="ddlAcao">
                                <Items>
                                    <dxe:ListEditItem Text="Aprovar Sele&#231;&#227;o" Value="EA" Selected="True"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Reprovar Sele&#231;&#227;o" Value="ER"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Deixar Sele&#231;&#227;o Pendente" Value="PA"></dxe:ListEditItem>
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px">
                            <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False"
                                Text="Aplicar " Width="100px">
                                <Paddings Padding="0px"></Paddings>
                                <ClientSideEvents Click="function(s, e) {
	var msgStatus = &quot;&quot;;

	if(ddlAcao.GetValue() == &quot;EA&quot;)
		msgStatus = &quot;Deseja Marcar as Linhas Selecionadas para Aprova&#231;&#227;o?&quot;;
	else
	{
		if(ddlAcao.GetValue() == &quot;ER&quot;)
			msgStatus = &quot;Deseja Marcar as Linhas Selecionadas para Reprova&#231;&#227;o?&quot;;
		else
			msgStatus = &quot;Deseja Marcar as Linhas Selecionadas para Pendentes?&quot;;
	}

	if(confirm(msgStatus))
		callBack.PerformCallback('A');
}"></ClientSideEvents>
                            </dxe:ASPxButton>
                        </td>
                        <td style="width: 10px;">
                        </td>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False"
                                Text="Publicar" Width="100px">
                                <Paddings Padding="0px"></Paddings>
                                <ClientSideEvents Click="function(s, e) {
	if(confirm(&quot;Deseja Publicar?&quot;))
		callBack.PerformCallback('P');
}"></ClientSideEvents>
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
                <dxpc:ASPxPopupControl ID="pcAprovacao" runat="server" ClientInstanceName="pcAprovacao"
                    Height="77px" Width="729px"  HeaderText="Alterar Status"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowCloseButton="False">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                            Text="Status:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxComboBox ID="ddlStatusAssunto" runat="server" ClientInstanceName="ddlStatusAssunto"
                                             SelectedIndex="0" ValueType="System.String">
                                            <Items>
                                                <dxe:ListEditItem ImageUrl="~/imagens/botoes/tarefasPA.png" Selected="True" Text="Pendente de Aprova&#231;&#227;o"
                                                    Value="PA" />
                                                <dxe:ListEditItem ImageUrl="~/imagens/botoes/salvar.gif" Text="Aprovado" Value="EA" />
                                                <dxe:ListEditItem ImageUrl="~/imagens/botoes/tarefaRecusada.PNG" Text="Reprovado"
                                                    Value="ER" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" 
                                            Text="Comentários do Recurso:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxMemo ID="txtComentariosRecurso" runat="server" BackColor="#EBEBEB" ClientInstanceName="txtComentariosRecurso"
                                             ForeColor="Black" Rows="7" Width="692px"
                                            ReadOnly="True">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                            Text="Comentários do Aprovador:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxMemo ID="txtComentarios" runat="server" ClientInstanceName="txtComentarios"
                                             Rows="7" Width="692px">
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 100px">
                                                    <dxe:ASPxButton ID="btnSalvarStatus" runat="server" AutoPostBack="False"
                                                        Text="Salvar" Width="81px">
                                                        <ClientSideEvents Click="function(s, e) {
	mudaStatus();
}" />
                                                        <Paddings Padding="0px" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td>
                                                    <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                                        Text="Fechar" Width="81px">
                                                        <ClientSideEvents Click="function(s, e) {
	pcAprovacao.Hide();
}" />
                                                        <Paddings Padding="0px" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <dxcb:ASPxCallback ID="callBack" runat="server" ClientInstanceName="callBack" OnCallback="callBack_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
         if(s.cp_OK != '')
          {
                    window.top.mostraMensagem(s.cp_OK,'sucesso', false, false, null);
                     ddlRecurso.PerformCallback();
	     gvDados.PerformCallback();
           }
         else if(s.cp_Erro != '')
          {
                    window.top.mostraMensagem(s.cp_Erro,'erro', true, false, null);
         }
         else if(s.cp_MSG != '')
        {
               window.top.mostraMensagem(s.cp_MSG,'Atencao', true, false, null);
        }
}" />
    </dxcb:ASPxCallback>
    <dxcb:ASPxCallback ID="callbackStatus" runat="server" ClientInstanceName="callbackStatus"
        OnCallback="callbackStatus_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
       if(s.cp_OK != '')
       {
                  window.top.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
                  pcAprovacao.Hide();
	 gvDados.PerformCallback();
       }
       else if(s.cp_Erro != '')
       {
                window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
        }
}" />
    </dxcb:ASPxCallback>
</asp:Content>
