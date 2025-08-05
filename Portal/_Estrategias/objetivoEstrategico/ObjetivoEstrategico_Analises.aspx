<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjetivoEstrategico_Analises.aspx.cs"
    Inherits="_Estrategias_objetivoEstrategico_ObjetivoEstrategico_Analises" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            height: 10px;
        }

        .Resize textarea {
            resize: both;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">

                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%;">
                                    <asp:Label ID="lblObjetivoEstrategico" CssClass="campo-label" runat="server" Text="<%$ Resources:traducao, objetivo_estrat_gico_ %>"></asp:Label>
                                </td>

                                <td style="width: 50%">
                                    <asp:Label ID="Label3" runat="server" CssClass="campo-label" Text="<%$ Resources:traducao, respons_vel_ %>"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 50%">
                                    <dxe:ASPxTextBox ID="txtObjetivoEstrategico" runat="server"
                                        Width="100%" ReadOnly="True" ClientInstanceName="txtObjetivoEstrategico">
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td style="width: 50%">
                                    <dxe:ASPxTextBox ID="txtResponsavel" runat="server"
                                        Width="100%" ReadOnly="True" ClientInstanceName="txtResponsavel">
                                        <ReadOnlyStyle BackColor="#E0E0E0">
                                        </ReadOnlyStyle>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="grid" KeyFieldName="CodigoAnalisePerformance"
                                        AutoGenerateColumns="False" Width="100%"
                                        ID="grid" OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared" OnBeforeColumnSortingGrouping="grid_BeforeColumnSortingGrouping"
                                        OnRowDeleting="grid_RowDeleting"
                                        OnCommandButtonInitialize="grid_CommandButtonInitialize" OnCustomCallback="grid_CustomCallback">
                                        <ClientSideEvents
                                            CustomButtonClick="function(s, e) {
	e.processOnServer = false;
     btnSalvar.SetVisible(true);
	if(e.buttonID == 'btnDetalhesCustom')
	{
	              btnSalvar.SetVisible(false);
                                            TipoOperacao = 'Visualizar';	 
                               s.GetRowValues(s.GetFocusedRowIndex(),'Analise;Recomendacoes;DataInclusao;NomeUsuarioInclusao;DataUltimaAlteracao;NomeUsuarioUltimaAlteracao', MontaCamposFormulario);        
		pcDetalhesAnalise.Show();
	}
                else if(e.buttonID == 'btnEditarCustom')
                {
                              TipoOperacao = 'Editar';	 
                               s.GetRowValues(s.GetFocusedRowIndex(),'Analise;Recomendacoes;DataInclusao;NomeUsuarioInclusao;DataUltimaAlteracao;NomeUsuarioUltimaAlteracao', MontaCamposFormulario);        
	             pcDetalhesAnalise.Show();
               }
}"
                                            EndCallback="function(s, e) {
    var command = hfGeral.Get('command');
     if(command === 'DELETEROW')
     {
              window.top.mostraMensagem(traducao.ObjetivoEstrategico_Analises_MensagemRegistroExcluidoComSucesso, 'sucesso', false, false, null);
      }
      if(s.cpErro != '')
      {
                window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
                            s.cpErro = '';
                            s.cpSucesso = '';     
      }
      else
      {
                 if(s.cpSucesso != '')
                 {
                            window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null);
                            s.cpErro = '';
                            s.cpSucesso = '';                            
                           grid.Refresh();
                            pcDetalhesAnalise.Hide();
                 }
      }        

}"
                                            BeginCallback="function(s, e) {
	hfGeral.Set('command',e.command);
}"></ClientSideEvents>
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" Caption=" " VisibleIndex="0" ShowDeleteButton="true" ShowCancelButton="true" ShowUpdateButton="true">
                                                <CustomButtons>
                                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Visualizar Detalhes">
                                                        <Image Url="~/imagens/botoes/pFormulario.png">
                                                        </Image>
                                                    </dxwgv:GridViewCommandColumnCustomButton>
                                                    <dxtv:GridViewCommandColumnCustomButton ID="btnEditarCustom">
                                                        <Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                                        </Image>
                                                    </dxtv:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <td align="center">
                                                                <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                                    ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
                                                                    <Paddings Padding="0px" />
                                                                    <Items>
                                                                        <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                            <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                            <Items>
                                                                                <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                    <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                                <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                    <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                                <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                    <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                                <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
                                                                                    <Image Url="~/imagens/menuExportacao/html.png">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                                <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                    <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                            </Items>
                                                                            <Image Url="~/imagens/botoes/btnDownload.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                            <Items>
                                                                                <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                    <Image IconID="save_save_16x16">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                                <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                    <Image IconID="actions_reset_16x16">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                            </Items>
                                                                            <Image Url="~/imagens/botoes/layout.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                    </Items>
                                                                    <ItemStyle Cursor="pointer">
                                                                        <HoverStyle>
                                                                            <border borderstyle="None" />
                                                                        </HoverStyle>
                                                                        <Paddings Padding="0px" />
                                                                    </ItemStyle>
                                                                    <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                        <SelectedStyle>
                                                                            <border borderstyle="None" />
                                                                        </SelectedStyle>
                                                                    </SubMenuItemStyle>
                                                                    <Border BorderStyle="None" />
                                                                </dxm:ASPxMenu>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                            </dxwgv:GridViewCommandColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="DataAnalisePerformance" Width="115px" Caption="Data"
                                                VisibleIndex="1">
                                                <PropertiesDateEdit Width="100px">
                                                </PropertiesDateEdit>
                                                <Settings ShowFilterRowMenu="True" AllowAutoFilter="False" />
                                                <SettingsHeaderFilter>
                                                    <ListBoxSearchUISettings Visibility="Visible" />
                                                </SettingsHeaderFilter>
                                                <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataMemoColumn FieldName="Analise" Name="txtAnaliseForm" Caption="An&#225;lise"
                                                VisibleIndex="2">
                                                <PropertiesMemoEdit Rows="4" Height="60px">
                                                    <ClientSideEvents KeyUp="function(s, e) {
	limitaASPxMemo(s, 2000);
}"></ClientSideEvents>
                                                    <ValidationSettings ErrorDisplayMode="None">
                                                        <RequiredField IsRequired="True" ErrorText="Campo Obrigat&#243;rio!"></RequiredField>
                                                    </ValidationSettings>
                                                    <Style CssClass="Resize">
                                                </Style>
                                                </PropertiesMemoEdit>
                                                <Settings AllowAutoFilter="False" />
                                                <EditFormSettings ColumnSpan="2" RowSpan="3" Visible="True" VisibleIndex="0" CaptionLocation="Top"
                                                    Caption="An&#225;lise:"></EditFormSettings>
                                            </dxwgv:GridViewDataMemoColumn>
                                            <dxwgv:GridViewDataMemoColumn FieldName="Recomendacoes" Name="txtRecomendacoesForm"
                                                Caption="<%$ Resources:traducao, recomenda__es %>" VisibleIndex="3">
                                                <PropertiesMemoEdit Rows="4" Height="60px">
                                                    <ClientSideEvents KeyUp="function(s, e) {
	limitaASPxMemo(s, 2000);
}"></ClientSideEvents>
                                                    <ValidationSettings CausesValidation="True" ErrorText="*">
                                                    </ValidationSettings>
                                                    <Style CssClass="Resize">
                                                </Style>
                                                </PropertiesMemoEdit>
                                                <Settings AllowAutoFilter="False" />
                                                <EditFormSettings ColumnSpan="2" RowSpan="2" Visible="True" VisibleIndex="1" CaptionLocation="Top"
                                                    Caption="<%$ Resources:traducao, recomenda__es_ %>"></EditFormSettings>
                                            </dxwgv:GridViewDataMemoColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoAnalisePerformance" Visible="False"
                                                VisibleIndex="9">
                                                <EditFormSettings VisibleIndex="2"></EditFormSettings>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioInclusao" Visible="False" VisibleIndex="4">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="DataInclusao" ReadOnly="True" Visible="False"
                                                VisibleIndex="5" Caption="Data Inclusão">
                                                <PropertiesDateEdit DisplayFormatString=""
                                                    EditFormat="DateTime" DisplayFormatInEditMode="True">
                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </ReadOnlyStyle>
                                                </PropertiesDateEdit>
                                                <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Inclu&#237;do Em:"></EditFormSettings>
                                                <CellStyle>
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioInclusao" UnboundType="String"
                                                ReadOnly="True" Caption="Inclu&#237;do Por" Visible="False" VisibleIndex="6">
                                                <PropertiesTextEdit EncodeHtml="False">
                                                    <ReadOnlyStyle BackColor="#EBEBEB">
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Inclu&#237;do Por:"></EditFormSettings>
                                                <CellStyle>
                                                    <border borderstyle="None" borderwidth="0px"></border>
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataDateColumn FieldName="DataUltimaAlteracao" ReadOnly="True" Caption="<%$ Resources:traducao,_ltima_altera__o_ %>"
                                                Visible="False" VisibleIndex="7">
                                                <PropertiesDateEdit DisplayFormatString=""
                                                    EditFormat="DateTime" DisplayFormatInEditMode="True">
                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </ReadOnlyStyle>
                                                </PropertiesDateEdit>
                                                <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Última Alteração:"></EditFormSettings>
                                                <CellStyle>
                                                </CellStyle>
                                            </dxwgv:GridViewDataDateColumn>
                                            <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioUltimaAlteracao" UnboundType="String"
                                                ReadOnly="True" Caption="Nome Usuário Última Alteração" Visible="False"
                                                VisibleIndex="8">
                                                <PropertiesTextEdit EncodeHtml="False">
                                                    <ReadOnlyStyle BackColor="#EBEBEB">
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Alterado Por:"></EditFormSettings>
                                                <CellStyle>
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                        </SettingsPager>
                                        <SettingsEditing Mode="PopupEditForm">
                                        </SettingsEditing>
                                        <SettingsPopup>
                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                AllowResize="True" Width="600px" />
                                        </SettingsPopup>
                                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="180"></Settings>
                                        <SettingsText ConfirmDelete="Deseja realmente excluir o registro?" PopupEditFormCaption="An&#225;lises"></SettingsText>
                                    </dxwgv:ASPxGridView>
                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                    </dxhf:ASPxHiddenField>

                                </dxp:PanelContent>
                            </PanelCollection>
                            <ClientSideEvents EndCallback="function(s, e) {
	/*if (window.onEnd_pnCallback)
		onEnd_pnCallback();	

	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Estrat&#233;gia inclu&#237;da com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Dados gravados com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Estrat&#233;gia exclu&#237;da com sucesso!&quot;);*/
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                </tr>
            </table>
        </div>
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Default>
                </Default>
                <Header>
                </Header>
                <Cell>
                </Cell>
                <GroupFooter Font-Bold="True">
                </GroupFooter>
                <Title Font-Bold="True"></Title>
            </Styles>
        </dxwgv:ASPxGridViewExporter>
        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
            ShowHeader="False" Width="270px" ID="pcUsuarioIncluido">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td style="" align="center"></td>
                                <td style="width: 70px" align="center" rowspan="3">
                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                        ClientInstanceName="imgSalvar" ID="imgSalvar">
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                        ID="lblAcaoGravacao">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDetalhesAnalise" CloseAction="CloseButton"
            HeaderText="An&#225;lises" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            Height="117px" ID="pcDetalhesAnalise" AllowDragging="True" AllowResize="True">
            <ClientSideEvents PopUp="function(s, e) {
s.SetHeight(Math.max(0, document.documentElement.clientHeight) - 155);
s.SetWidth(Math.max(0, document.documentElement.clientWidth) - 100);
s.UpdatePosition();
}" />
            <ContentStyle>
                <Paddings Padding="4px" PaddingRight="6px"></Paddings>
            </ContentStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" class="style1">
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                    Text="Análise:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxMemo ID="txtAnalise" runat="server" ClientInstanceName="txtAnalise"
                                    Height="71px" Width="100%" CssClass="Resize">
                                    <ReadOnlyStyle ForeColor="Black">
                                    </ReadOnlyStyle>
                                </dxe:ASPxMemo>
                                <dxe:ASPxLabel ID="lblContadorCaracterAnalise" ClientInstanceName="lblContadorCaracterAnalise" runat="server" Font-Bold="True" ForeColor="#999999"
                                    Text="">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                    Text="Recomendações:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxMemo ID="txtRecomendacoes" runat="server" ClientInstanceName="txtRecomendacoes"
                                    Height="71px" Width="100%" CssClass="Resize">
                                    <ReadOnlyStyle ForeColor="Black">
                                    </ReadOnlyStyle>
                                </dxe:ASPxMemo>
                               <dxe:ASPxLabel ID="lblContadorCaracterRecomendacao" ClientInstanceName="lblContadorCaracterRecomendacao" runat="server" Font-Bold="True" ForeColor="#999999"
                                    Text="">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 287px">
                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                    Text="Incluído Em:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                    Text="Incluído Por:">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 287px">
                                                <dxe:ASPxDateEdit ID="dteDataInclusao" runat="server" ClientInstanceName="dteDataInclusao" EditFormat="DateTime"
                                                    ReadOnly="True" Width="100%">
                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </ReadOnlyStyle>
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxDateEdit>
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtIncluidoPor" runat="server" ClientInstanceName="txtIncluidoPor"
                                                    ReadOnly="True" Width="100%">
                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </ReadOnlyStyle>
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 287px">
                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                    Text="Última Alteração:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td>
                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                                    Text="Alterado Por:">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 287px">
                                                <dxe:ASPxDateEdit ID="dteUltimaAlteracao" runat="server" ClientInstanceName="dteUltimaAlteracao" EditFormat="DateTime"
                                                    ReadOnly="True" Width="100%">
                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </ReadOnlyStyle>
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxDateEdit>
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtAlteradoPor" runat="server" ClientInstanceName="txtAlteradoPor"
                                                    ReadOnly="True" Width="100%">
                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </ReadOnlyStyle>
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table class="formulario-botoes" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <td>


                                                <dxe:ASPxButton ID="btnSalvar" ClientInstanceName="btnSalvar" runat="server" AutoPostBack="False"
                                                    Text="Salvar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
	var msgValidaCampos = validaCamposFormulario();
                if(validaCamposFormulario() != '')
                {
                                window.top.mostraMensagem(msgValidaCampos, 'erro', true, false, null);
                }
                else
                {
                                  grid.PerformCallback(TipoOperacao);                                           
                                  pcDetalhesAnalise.Hide();                     
                }
}"></ClientSideEvents>
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btnFechar" ClientInstanceName="btnFechar" runat="server" AutoPostBack="False"
                                                    Text="Fechar" Width="90px">
                                                    <ClientSideEvents Click="function(s, e) {
                                                                                    pcDetalhesAnalise.Hide();
                                                                                    grid.Refresh();
}"></ClientSideEvents>
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
