<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="CadastroFornecedores.aspx.cs" Inherits="administracao_CadastroFornecedores" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <!-- TABLA CONTEUDO -->
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="left">
                <table>
                    <tr>
                        <td align="center" style="width: 1px;>
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px; padding-top:10px;">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Cadastro de Fornecedores">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
            </td>
            <td style="height: 1px" align="right">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                    Width="100%" OnCallback="pnCallback_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoPessoa"
                                AutoGenerateColumns="False" Width="100%" 
                                ID="gvDados" OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnAfterPerformCallback="gvDados_AfterPerformCallback1"
                                OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                                <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) 
{
     gvDados.SetFocusedRowIndex(e.visibleIndex);
     if(e.buttonID == &quot;btnNovo&quot;)
     {
        onClickBarraNavegacao(&quot;Incluir&quot;, gvDados, pcDados);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Incluir&quot;);
		TipoOperacao = &quot;Incluir&quot;;
     }
     if(e.buttonID == &quot;btnEditar&quot;)
     {
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
        hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
		TipoOperacao = &quot;Editar&quot;;
                                    btnSalvarFormulario.SetVisible(true);
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvarFormulario.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
"></ClientSideEvents>
                                
<SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
                                        <CustomButtons>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
                                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
                                                <Image Url="~/imagens/botoes/pFormulario.PNG">
                                                </Image>
                                            </dxwgv:GridViewCommandColumnCustomButton>
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
                                                                    <Border BorderStyle="None" />
                                                                </HoverStyle>
                                                                <Paddings Padding="0px" />
                                                            </ItemStyle>
                                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                <SelectedStyle>
                                                                    <Border BorderStyle="None" />
                                                                </SelectedStyle>
                                                            </SubMenuItemStyle>
                                                            <Border BorderStyle="None" />
                                                        </dxm:ASPxMenu>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NomePessoa" Name="NomePessoa" Caption="Raz&#227;o Social"
                                        VisibleIndex="1" Width="400px">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Nome Fantasia" FieldName="NomeFantasia" ShowInCustomizationForm="True"
                                        VisibleIndex="2">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeContato" Name="NomeContato" Caption="Nome do Contato"
                                        VisibleIndex="3">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TelefonePessoa" Name="TelefonePessoa" VisibleIndex="4"
                                        Caption="Telefone">
                                        <Settings AllowAutoFilter="False" AllowHeaderFilter="False" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TipoPessoa" Name="TipoPessoa" Visible="False"
                                        VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="InformacaoContato" Name="InformacaoContato"
                                        Visible="False" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoMunicipioEnderecoPessoa" Name="CodigoMunicipioEnderecoPessoa"
                                        Visible="False" VisibleIndex="10">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NumeroCNPJCPF" Name="NumeroCNPJCPF" Visible="False"
                                        VisibleIndex="11">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="EnderecoPessoa" Name="EnderecoPessoa" Visible="False"
                                        VisibleIndex="12">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoRamoAtividade" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="13">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Email" ShowInCustomizationForm="True" Visible="False"
                                        VisibleIndex="14" Name="Email">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Comentarios" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="15">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeMunicipio" Name="NomeMunicipio" ShowInCustomizationForm="False"
                                        Visible="False" VisibleIndex="16">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="RamoAtividade" Name="RamoAtividade" ShowInCustomizationForm="False"
                                        Visible="False" VisibleIndex="17">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SiglaUF" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="9">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataCheckColumn Caption="Cliente" FieldName="IndicaCliente" 
                                        ShowInCustomizationForm="True" VisibleIndex="7">
                                        <PropertiesCheckEdit DisplayTextChecked="Sim" DisplayTextUnchecked="Não" 
                                            ValueChecked="S" ValueType="System.Char" ValueUnchecked="N">
                                        </PropertiesCheckEdit>
                                    </dxwgv:GridViewDataCheckColumn>
                                    <dxwgv:GridViewDataCheckColumn Caption="Fornecedor" 
                                        FieldName="IndicaFornecedor" ShowInCustomizationForm="True" VisibleIndex="8">
                                        <PropertiesCheckEdit DisplayTextChecked="Sim" DisplayTextUnchecked="Não" 
                                            ValueChecked="S" ValueType="System.Char" ValueUnchecked="N">
                                        </PropertiesCheckEdit>
                                    </dxwgv:GridViewDataCheckColumn>

                                    <dxtv:GridViewDataCheckColumn Caption="Partícipe" FieldName="IndicaParticipe" ShowInCustomizationForm="False" VisibleIndex="9" Width="80px">
                                        <PropertiesCheckEdit DisplayTextChecked="Sim" DisplayTextUnchecked="Não" ValueChecked="S" ValueType="System.Char" ValueUnchecked="N">
                                        </PropertiesCheckEdit>
                                    </dxtv:GridViewDataCheckColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords" Visible="False">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True"></Settings>
                                <SettingsText></SettingsText>
                            </dxwgv:ASPxGridView>
                            <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None"
                                HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                ShowCloseButton="False" Width="920px"  ID="pcDados" Height="500px">
                                <ContentStyle>
                                    <Paddings Padding="5px"></Paddings>
                                </ContentStyle>
                                <HeaderStyle Font-Bold="True"></HeaderStyle>
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                        Text="Raz&#227;o Social:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtNomePessoa" runat="server" ClientInstanceName="txtNomePessoa"
                                                         MaxLength="150" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="padding-right: 10px" class="auto-style2">
                                                                <dxe:ASPxLabel ID="ASPxLabel11" runat="server" 
                                                                    Text="Nome Fantasia:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblCliente" runat="server" ClientInstanceName="lblCliente"
                                                                    Text=" ">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblFornecedor" runat="server" ClientInstanceName="lblFornecedor"
                                                                     Text=" ">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                            
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-right: 10px" class="auto-style2">
                                                                <dxe:ASPxTextBox ID="txtNomeFantasia" runat="server" ClientInstanceName="txtNomeFantasia"
                                                                     MaxLength="150" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td style="padding-right: 10px">
                                                                <dxe:ASPxCheckBox ID="ckbCliente" runat="server" Checked="True" CheckState="Checked"
                                                                    ClientInstanceName="ckbCliente"  Text="Cliente">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxCheckBox>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxCheckBox ID="ckbFornecedor" runat="server" Checked="True" CheckState="Checked"
                                                                    ClientInstanceName="ckbFornecedor"  Text="Fornecedor">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxCheckBox>
                                                            </td>
                                                            <td>
                                                                <dxtv:ASPxCheckBox ID="ckbParticipe" runat="server" Checked="True" CheckState="Checked" 
                                                                    ClientInstanceName="ckbParticipe" Font-Names="Verdana" Font-Size="8pt" Text="Partícipe">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxtv:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 1px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td class="style3" style="width: 250px">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style2" style="width: 160px">
                                                                <dxe:ASPxLabel ID="lblTipoPessoa1" runat="server" ClientInstanceName="lblTipoPessoa"
                                                                     Text="CNPJ:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 265px">
                                                                <dxe:ASPxLabel ID="lblTipoPessoa" runat="server" 
                                                                    Text="Ramo de Atividade:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style3" style="width: 250px">
                                                                <dxe:ASPxRadioButtonList ID="rbTipoPessoa" runat="server" ClientInstanceName="rbTipoPessoa"
                                                                     ItemSpacing="20px" RepeatDirection="Horizontal"
                                                                    SelectedIndex="1" Width="240px">
                                                                    <Paddings Padding="0px" />
                                                                    <ClientSideEvents Init="function(s, e) {
	alteraTipoPessoa(s.GetValue());
}" SelectedIndexChanged="function(s, e) {
	alteraTipoPessoa(s.GetValue());
}" />
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Pessoa Física" Value="F" />
                                                                        <dxe:ListEditItem Selected="True" Text="Pessoa Jurídica" Value="J" />
                                                                    </Items>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxRadioButtonList>
                                                            </td>
                                                            <td class="style2" style="width: 160px">
                                                                <dxe:ASPxTextBox ID="txtCPF" runat="server" ClientInstanceName="txtCPF" ClientVisible="False"
                                                                     Width="150px">
                                                                    <MaskSettings IncludeLiterals="None" Mask="999,999,999-99" PromptChar=" " />
                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                    </ValidationSettings>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                                <dxe:ASPxTextBox ID="txtCNPJ" runat="server" ClientInstanceName="txtCNPJ"
                                                                    Width="150px">
                                                                    <MaskSettings IncludeLiterals="None" Mask="99,999,999/9999-99" PromptChar=" " />
                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                    </ValidationSettings>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="ddlRamoAtividade" runat="server" ClientInstanceName="ddlRamoAtividade"
                                                                     Width="100%">
                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="Construção" Value="C" />
                                                                        <dxe:ListEditItem Text="Reforma" Value="R" />
                                                                        <dxe:ListEditItem Text="Ampliação" Value="A" />
                                                                    </Items>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 1px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                        Text="Endereço:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxMemo ID="txtEnderecoPessoa" runat="server" ClientInstanceName="txtEnderecoPessoa"
                                                         Rows="1" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                    <dxe:ASPxLabel ID="lblContadorEnderecoPessoa" runat="server" ClientInstanceName="lblContadorEnderecoPessoa"
                                                        Font-Bold="False" Font-Size="11px" ForeColor="#484848">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 1px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="width: 75px">
                                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" 
                                                                    Text="UF:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                                                    Text="Município:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 75px; padding-right: 10px">
                                                                <dxe:ASPxComboBox ID="ddlUF" runat="server" ClientInstanceName="ddlUF"
                                                                    TextField="SiglaUF" ValueField="SiglaUF" Width="100%">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddlMunicipio.PerformCallback();
}" />
                                                                    <DisabledStyle BackColor="#EBEBEB"  ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="ddlMunicipio" runat="server" ClientInstanceName="ddlMunicipio"
                                                                     IncrementalFilteringMode="Contains" OnCallback="ddlMunicipio_Callback"
                                                                    TextFormatString="{1} - {0}" Width="100%">
                                                                    <ClientSideEvents EndCallback="function(s, e) {
	s.SetValue(s.cp_CodigoMunicipio);
}" />
                                                                    <Columns>
                                                                        <dxe:ListBoxColumn Caption="UF" FieldName="SiglaUF" Width="50px" />
                                                                        <dxe:ListBoxColumn Caption="Município" FieldName="NomeMunicipio" Width="810px" />
                                                                    </Columns>
                                                                    <ItemStyle Wrap="True" />
                                                                    <ListBoxStyle Wrap="True">
                                                                    </ListBoxStyle>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="height: 1px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 230px">
                                                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" 
                                                                    Text="Nome do Contato:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td class="linhaEdicaoPeriodo" style="width: 145px">
                                                                <dxe:ASPxLabel ID="ASPxLabel10" runat="server" 
                                                                    Text="Telefone:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="width: 260px">
                                                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server" 
                                                                    Text="Email:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 210px">
                                                                <dxe:ASPxTextBox ID="txtNomeContato" runat="server" ClientInstanceName="txtNomeContato"
                                                                     MaxLength="150" Width="200px">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td class="linhaEdicaoPeriodo" style="width: 125px">
                                                                <dxe:ASPxTextBox ID="txtTelefone" runat="server" ClientInstanceName="txtTelefone"
                                                                     Width="115px">
                                                                    <MaskSettings Mask="(99) 9999-9999" PromptChar=" " />
                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                    </ValidationSettings>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxTextBox ID="txtEmail" runat="server" ClientInstanceName="txtEmail"
                                                                    MaxLength="150" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height:1px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server" 
                                                        Text="Informações do Contato:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxMemo ID="txtInformacoesContato" runat="server" ClientInstanceName="txtInformacoesContato"
                                                         Rows="1" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                    <dxe:ASPxLabel ID="lblContadorInformacoesContato" runat="server" ClientInstanceName="lblContadorInformacoesContato"
                                                        Font-Bold="False" Font-Size="11px" ForeColor="#484848">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 1px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                        Text="Comentários:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxMemo ID="txtComentarios" runat="server" ClientInstanceName="txtComentarios"
                                                         Rows="2" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                    <dxe:ASPxLabel ID="lblContadorInformacoesComentarios" runat="server" ClientInstanceName="lblContadorInformacoesComentarios"
                                                        Font-Bold="False" Font-Size="11px" ForeColor="#484848">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                           <%-- <tr>
                                                <td style="height: 10px">
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td align="right">
                                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td class="formulario-botao">
                                                                   <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarFormulario"
                                                                         Text="Salvar" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}" />
                                                                        <Paddings Padding="0px" />
                                                                    </dxe:ASPxButton>
                                                                </td>                                                               
                                                                <td class="formulario-botao">
                                                                    <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                                         Text="Fechar" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}" />
                                                                        <Paddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px"
                                                                            PaddingTop="0px" />
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
                            <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                            </dxhf:ASPxHiddenField>
                            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                                Landscape="True" LeftMargin="50" OnRenderBrick="ASPxGridViewExporter1_RenderBrick"
                                RightMargin="50">
                                <Styles>
                                    <Default >
                                    </Default>
                                    <Header >
                                    </Header>
                                    <Cell >
                                    </Cell>
                                    <Footer >
                                    </Footer>
                                    <GroupFooter >
                                    </GroupFooter>
                                    <GroupRow >
                                    </GroupRow>
                                    <Title ></Title>
                                </Styles>
                            </dxwgv:ASPxGridViewExporter>
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) 
{

	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.CadastroFornecedores_fornecedor_inclu_do_com_sucesso_, 'sucesso', false, false, null);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.CadastroFornecedores_fornecedor_alterado_com_sucesso_, 'sucesso', false, false, null);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(traducao.CadastroFornecedores_fornecedor_exclu_do_com_sucesso_, 'sucesso', false, false, null);
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style2 {
            width: 630px;
        }
    </style>
</asp:Content>