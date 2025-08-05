<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sprint_antigo.aspx.cs" Inherits="_Projetos_DadosProjeto_sprint_antigo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .GridSprint {
            min-width: 600px !important;
        }

        .GridColum01Sprint {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoIteracao"
            AutoGenerateColumns="False" Width="100%" CssClass="GridSprint"
            ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnCustomErrorText="gvDados_CustomErrorText"
            OnCustomCallback="gvDados_CustomCallback" OnAfterPerformCallback="gvDados_AfterPerformCallback">
            <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                CustomButtonClick="function(s, e) {
//debugger    
gvDados.SetFocusedRowIndex(e.visibleIndex);

     if(e.buttonID == &quot;btnEditarCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
		hfGeral.Get(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		desabilitaHabilitaComponentes();
     }
     else if(e.buttonID == &quot;btnExcluirCustom&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnFormularioCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		pcDados.Show();
     }	
}"
                BeginCallback="function(s, e) {
	comando = e.command;
}"
                EndCallback="function(s, e) {
          if(comando == &quot;CUSTOMCALLBACK&quot;)
          {
                    var msg = s.cp_MSG;
                    if(msg != &quot;OK&quot; &amp;&amp; msg  != &quot;&quot;)
                    {
                              window.top.mostraMensagem(msg, 'erro', true, false, null);
                    }
                    else
                    {
                              e.processOnServer = false;
                              if (window.onClick_btnCancelar)
                                      onClick_btnCancelar(); 
                               window.top.mostraMensagem(&quot;Dados atualizados com sucesso!&quot;, 'sucesso', false, false, null);
                    }
                    pnPacoteTrabalho.PerformCallback();
          }
}"></ClientSideEvents>
            <Columns>
                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="15%" Caption=" " VisibleIndex="0">
                    <CustomButtons>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                            </Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                            </Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
                            <Image Url="~/imagens/botoes/pFormulario.png">
                            </Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                    <FooterCellStyle CssClass="GridColum01Sprint">
                    </FooterCellStyle>
                    <HeaderTemplate>
                        <table align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <dxtv:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                        ItemSpacing="5px" OnInit="menu_Init" OnItemClick="menu_ItemClick">
                                        <Paddings Padding="0px" />
                                        <Items>
                                            <dxtv:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                <Image Url="~/imagens/botoes/incluirReg02.png">
                                                </Image>
                                            </dxtv:MenuItem>
                                            <dxtv:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                <Items>
                                                    <dxtv:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                        <Image Url="~/imagens/menuExportacao/xls.png">
                                                        </Image>
                                                    </dxtv:MenuItem>
                                                    <dxtv:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                        <Image Url="~/imagens/menuExportacao/pdf.png">
                                                        </Image>
                                                    </dxtv:MenuItem>
                                                    <dxtv:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                        <Image Url="~/imagens/menuExportacao/rtf.png">
                                                        </Image>
                                                    </dxtv:MenuItem>
                                                    <dxtv:MenuItem ClientVisible="False" Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML">
                                                        <Image Url="~/imagens/menuExportacao/html.png">
                                                        </Image>
                                                    </dxtv:MenuItem>
                                                </Items>
                                                <Image Url="~/imagens/botoes/btnDownload.png">
                                                </Image>
                                            </dxtv:MenuItem>
                                            <dxtv:MenuItem ClientVisible="false" Name="btnLayout" Text="" ToolTip="Layout">
                                                <Items>
                                                    <dxtv:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                        <Image IconID="save_save_16x16">
                                                        </Image>
                                                    </dxtv:MenuItem>
                                                    <dxtv:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                        <Image IconID="actions_reset_16x16">
                                                        </Image>
                                                    </dxtv:MenuItem>
                                                </Items>
                                                <Image Url="~/imagens/botoes/layout.png">
                                                </Image>
                                            </dxtv:MenuItem>
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
                                    </dxtv:ASPxMenu>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                </dxwgv:GridViewCommandColumn>
                <dxtv:GridViewDataTextColumn VisibleIndex="3" Caption="Título" FieldName="Titulo"
                    Width="30%">
                    <DataItemTemplate>
                        <%# getDescricaoObjetosLista()%>
                    </DataItemTemplate>
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataDateColumn Caption="Início" VisibleIndex="4" FieldName="Inicio"
                    Width="15%">
                    <PropertiesDateEdit DisplayFormatString="d">
                    </PropertiesDateEdit>
                </dxtv:GridViewDataDateColumn>
                <dxtv:GridViewDataDateColumn Caption="Término" VisibleIndex="5" FieldName="Termino"
                    Width="15%">
                    <PropertiesDateEdit DisplayFormatString="d">
                    </PropertiesDateEdit>
                </dxtv:GridViewDataDateColumn>
                <dxwgv:GridViewDataTextColumn Caption="Status" VisibleIndex="6" FieldName="Status"
                    Visible="False">
                </dxwgv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn VisibleIndex="7" Caption="Responsável" Width="30%"
                    FieldName="NomeProjectOwner">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="CodigoProjectOwner" FieldName="CodigoProjectOwner"
                    Visible="False" VisibleIndex="8">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Pacote de Trabalho" FieldName="NomePacoteTrabalho"
                    VisibleIndex="9" Width="250px">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="CodigoPacoteTrabalho" FieldName="CodigoPacoteTrabalho"
                    Visible="False" VisibleIndex="10">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Objetivos" FieldName="Objetivos" Visible="False"
                    VisibleIndex="11">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="CodigoIteracao" FieldName="CodigoIteracao"
                    Visible="False" VisibleIndex="1">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="CodigoProjetoIteracao" FieldName="CodigoProjetoIteracao"
                    Visible="False" VisibleIndex="2">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="Disponibilidade"
                    FieldName="FatorProdutividade" Visible="False" VisibleIndex="12">
                </dxtv:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" HorizontalScrollBarMode="Visible"
                ShowGroupPanel="True" />
            <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
            <Templates>
                <FooterRow>
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Tarefa Concluída" ClientInstanceName="lblDescricaoConcluido"
                                        ID="lblDescricaoConcluido">
                                    </dxe:ASPxLabel>
                                </td>
                                <td></td>
                                <td style="width: 10px; background-color: green"></td>
                                <td align="center">|
                                </td>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Tarefa Atrasada" ClientInstanceName="lblDescricaoAtrasada"
                                        ID="lblDescricaoAtrasada">
                                    </dxe:ASPxLabel>
                                </td>
                                <td></td>
                                <td style="width: 10px; background-color: red"></td>
                                <td align="center">|
                                </td>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Tem Anotações" ClientInstanceName="lblDescricaoAnotacoes"
                                        ID="lblDescricaoAnotacoes">
                                    </dxe:ASPxLabel>
                                </td>
                                <td></td>
                                <td>
                                    <img style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; border-right-width: 0px"
                                        src="../../imagens/anotacao.gif" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </FooterRow>
            </Templates>
        </dxwgv:ASPxGridView>
        <!-- PANEL CONTROL : pcDados -->
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
            CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalOffset="-20"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="770px" Height="145px"
            ID="pcDados">
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <table class="formulario" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr class="formulario-linha">
                                <td>
                                    <table class="formulario-colunas" border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="formulario-label">
                                                    <dxtv:ASPxLabel ID="lblTitulo6" runat="server" ClientInstanceName="lblTitulo"
                                                        Text="Título:">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxTextBox ID="txtTitulo" runat="server" ClientInstanceName="txtTitulo"
                                                        MaxLength="150" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxtv:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td>
                                    <table class="formulario-colunas" cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td class="formulario-label">
                                                <dxtv:ASPxLabel ID="lblTitulo16" runat="server" ClientInstanceName="lblTitulo"
                                                    Text="Início:" Width="120">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td class="formulario-label">
                                                <dxtv:ASPxLabel ID="lblTitulo17" runat="server" ClientInstanceName="lblTitulo"
                                                    Text="Término:" Width="120">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td class="formulario-label">
                                                <dxtv:ASPxLabel ID="lblTitulo20" runat="server" ClientInstanceName="lblTitulo"
                                                    Text="Produtividade da Equipe:" Width="147px">
                                                </dxtv:ASPxLabel>
                                            </td>
                                            <td class="formulario-label">
                                                <dxtv:ASPxLabel ID="lblTitulo11" runat="server" ClientInstanceName="lblTitulo"
                                                    Text="Status:">
                                                </dxtv:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxtv:ASPxDateEdit ID="dtInicio" runat="server" ClientInstanceName="dtInicio" Width="100%">
                                                    <ClientSideEvents ValueChanged="function(s, e) {
	if(ddlPacoteTrabalho.cp_Visivel == 'N')
	{
		
		dtTermino.SetDate(null);
		if(s.GetDate() != null)
        {      
            var data = s.GetDate();
            var dataMax = new Date();   
            dataMax.setDate(data.getDate() + 28);                                        
            dtTermino.SetMinDate(data);
			dtTermino.SetMaxDate(dataMax);

        }
	}
}" />
                                                </dxtv:ASPxDateEdit>
                                            </td>
                                            <td>
                                                <dxtv:ASPxDateEdit ID="dtTermino" runat="server" ClientInstanceName="dtTermino" Width="100%">
                                                </dxtv:ASPxDateEdit>
                                            </td>
                                            <td>
                                                <dxtv:ASPxSpinEdit ID="spnFatorProdutividade" runat="server"
                                                    ClientInstanceName="spnFatorProdutividade"
                                                    MaxLength="3" MaxValue="100" Number="0" NumberType="Integer" Width="100%"
                                                    DisplayFormatString="{0}%">
                                                    <SpinButtons ClientVisible="False" ShowIncrementButtons="False">
                                                    </SpinButtons>
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxtv:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxtv:ASPxTextBox ID="txtStatus" runat="server" ClientInstanceName="txtStatus"
                                                    MaxLength="150" Width="100%" ClientEnabled="False">
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxtv:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td class="formulario-label">
                                    <dxtv:ASPxLabel ID="lblTitulo18" runat="server" ClientInstanceName="lblTitulo"
                                        Text="Responsável:">
                                    </dxtv:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td>
                                    <dxtv:ASPxComboBox ID="ddlProjectOwner" runat="server" ClientInstanceName="ddlProjectOwner"
                                        Width="100%" DataSourceID="sdsProjectOwner"
                                        TextField="NomeUsuario" ValueField="CodigoUsuario">
                                        <ItemStyle Wrap="True" />
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxtv:ASPxComboBox>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td class="formulario-label" id="tdPacote" runat="server">
                                    <dxtv:ASPxLabel ID="lblTitulo19" runat="server" ClientInstanceName="lblTitulo"
                                        Text="Pacote de trabalho associado:">
                                    </dxtv:ASPxLabel>
                                    <dxtv:ASPxCallbackPanel ID="pnPacoteTrabalho" runat="server" ClientInstanceName="pnPacoteTrabalho" OnCallback="pnPacoteTrabalho_Callback" Width="100%">
                                        <SettingsLoadingPanel Delay="0" Enabled="False" ShowImage="False" Text="" />
                                        <SettingsCollapsing AnimationType="None">
                                        </SettingsCollapsing>
                                        <Paddings Padding="0px" />
                                        <PanelCollection>
                                            <dxtv:PanelContent runat="server">
                                                <dxtv:ASPxComboBox ID="ddlPacoteTrabalho" runat="server" ClientInstanceName="ddlPacoteTrabalho" TextField="NomeTarefa" ValueField="CodigoTarefa" ValueType="System.Int32" Width="100%">
                                                    <ItemStyle Wrap="True" />
                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dxtv:ASPxComboBox>
                                            </dxtv:PanelContent>
                                        </PanelCollection>
                                    </dxtv:ASPxCallbackPanel>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td>
                                    <dxtv:ASPxLabel ID="lblTitulo7" runat="server" ClientInstanceName="lblTitulo"
                                        Text="Objetivos:">
                                    </dxtv:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td>
                                    <dxtv:ASPxMemo ID="txtObjetivos" runat="server" ClientInstanceName="txtObjetivos"
                                        Rows="2" Width="100%">
                                        <ValidationSettings ErrorDisplayMode="None">
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxtv:ASPxMemo>
                                    <dxtv:ASPxLabel ID="lblContadorMemoDescricao" runat="server" ClientInstanceName="lblContadorMemoDescricao"
                                        Font-Bold="True" ForeColor="#999999">
                                    </dxtv:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar"
                                                        Text="Salvar" ValidationGroup="MKE" Width="100px">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(verificarDadosPreenchidos())
	{
		if (window.onClick_btnSalvar)
	                        onClick_btnSalvar();
	}
	else
                {
                         return false;
                }  
}" />
                                                    </dxtv:ASPxButton>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <dxtv:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar"
                                                        Text="Fechar" Width="100px">
                                                        <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                    </dxtv:ASPxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>

        <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxtv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados">
        </dxtv:ASPxGridViewExporter>
        <asp:SqlDataSource ID="sdsProjectOwner" runat="server" SelectCommand=""></asp:SqlDataSource>
    </form>
</body>
</html>
