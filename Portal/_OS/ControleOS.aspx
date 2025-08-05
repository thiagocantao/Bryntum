<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ControleOS.aspx.cs" Inherits="_OS_ControleOS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">

        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

            fechaTelaEdicao();
        }

        function fechaTelaEdicao() {
            
            if (callbackSalvar.cp_AtualizarTela == 'S') {
                txtNumeroOS.SetText(callbackSalvar.cpNumeroOS);
                txtTituloOS.SetText(callbackSalvar.cpTituloOS);
                txtStatus.SetText(callbackSalvar.cpStatus);
                txtDescricao.SetText(callbackSalvar.cpDescricao);
            }
        }

        function gravaInstanciaWf() {
            try {
                window.parent.executaCallbackWF();
            } catch (e) { }
        }

        function eventoPosSalvar(codigoInstancia) {
            try {
                window.parent.parent.hfGeralWorkflow.Set('CodigoInstanciaWf', codigoInstancia);
            } catch (e) {
            }
            hfWF.Set("CodigoCI", codigoInstancia);
            callbackSalvar.PerformCallback();
        }

        function novoItem()
        {
            if (callbackSalvar.cp_OS == '-1')
                window.top.mostraMensagem('A OS precisa ser salva para adicionar novos itens!', 'atencao', true, false, null);
            else
                gvDados.AddNewRow();
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 201px;
        }
        .auto-style3 {
            width: 211px;
        }

.dxpcLite .dxpc-header,
.dxdpLite .dxpc-header 
{
	color: #404040;
	background-color: #DCDCDC;
	border-bottom: 1px solid #C9C9C9;
	padding: 2px 2px 2px 12px;
}

    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellspacing="0" class="auto-style1">
            <tr>
                <td style="padding-bottom: 10px">
                    <table cellspacing="0" class="auto-style1">
                        <tr>
                            <td class="auto-style2">
                                <dxcp:ASPxLabel ID="ASPxLabel1" runat="server"  Text="Número da OS:">
                                </dxcp:ASPxLabel>
                            </td>
                            <td>
                                <dxcp:ASPxLabel ID="ASPxLabel2" runat="server"  Text="Título:">
                                </dxcp:ASPxLabel>
                            </td>
                            <td class="auto-style3">
                                <dxcp:ASPxLabel ID="ASPxLabel3" runat="server"  Text="Status:">
                                </dxcp:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2" style="padding-right: 10px">
                                <dxcp:ASPxTextBox ID="txtNumeroOS" runat="server" ClientEnabled="False" ClientInstanceName="txtNumeroOS"  Width="100%">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="#484848">
                                    </DisabledStyle>
                                </dxcp:ASPxTextBox>
                            </td>
                            <td style="padding-right: 10px">
                                <dxcp:ASPxTextBox ID="txtTituloOS" runat="server" ClientInstanceName="txtTituloOS"  MaxLength="255" Width="100%">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="#484848">
                                    </DisabledStyle>
                                </dxcp:ASPxTextBox>
                            </td>
                            <td class="auto-style3">
                                <dxcp:ASPxTextBox ID="txtStatus" runat="server" ClientEnabled="False" ClientInstanceName="txtStatus"  Width="100%">
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="#484848">
                                    </DisabledStyle>
                                </dxcp:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <dxcp:ASPxLabel ID="ASPxLabel4" runat="server"  Text="Descrição:">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 10px">
                    <dxcp:ASPxMemo runat="server" Rows="6" Width="100%" ClientInstanceName="txtDescricao"  TabIndex="6" ID="txtDescricao">
                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                        </DisabledStyle>
                    </dxcp:ASPxMemo>
                    <dxcp:ASPxLabel runat="server" ClientInstanceName="lbl_descricao" Font-Bold="True"  ForeColor="#999999" ID="lbl_descricao">
                    </dxcp:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 10px">

 <dxcp:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoItemOS" AutoGenerateColumns="False" Width="100%"  ID="gvDados" OnCommandButtonInitialize="gvDados_CommandButtonInitialize" OnCellEditorInitialize="gvDados_CellEditorInitialize" OnRowDeleting="gvDados_RowDeleting" OnRowInserting="gvDados_RowInserting" OnRowUpdating="gvDados_RowUpdating">

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

     <SettingsEditing Mode="PopupEditForm">
     </SettingsEditing>

<Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Auto" ShowFooter="True"></Settings>

<SettingsBehavior AllowFocusedRow="True" AllowSort="False" ConfirmDelete="True"></SettingsBehavior>
     <SettingsPopup>
         <EditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Width="800px" />
     </SettingsPopup>
     <SettingsText ConfirmDelete="Confirma o cancelamento do item?" />
<Columns>
<dxcp:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" Width="100px" VisibleIndex="0" ShowDeleteButton="True" ShowEditButton="True">
<HeaderTemplate>
            <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""novoItem()"" style=""cursor: pointer;""/>" : @"<img src=""../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
        
</HeaderTemplate>
</dxcp:GridViewCommandColumn>
<dxcp:GridViewDataTextColumn FieldName="SequenciaItemOS" Caption="Item" VisibleIndex="1" Width="50px">
<Settings AllowAutoFilter="False"></Settings>
    <EditFormSettings Visible="False" />
    <FooterTemplate>
        Total
    </FooterTemplate>
</dxcp:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Custo Plan." FieldName="ValorPlanejadoCusto" VisibleIndex="7" Width="100px">
        <PropertiesTextEdit DisplayFormatString="n2">
        </PropertiesTextEdit>
        <Settings AllowAutoFilter="False" />
        <EditFormSettings Visible="False" />
        <HeaderStyle HorizontalAlign="Right" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Custo Aprov." FieldName="ValorAprovadoCusto" VisibleIndex="8" Width="100px">
        <PropertiesTextEdit DisplayFormatString="n2">
        </PropertiesTextEdit>
        <Settings AllowAutoFilter="False" />
        <EditFormSettings Visible="False" />
        <HeaderStyle HorizontalAlign="Right" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Status" FieldName="DescricaoStatusItem" VisibleIndex="9" Width="200px">
        <EditFormSettings Visible="False" />
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Custo Atend." FieldName="ValorAtendidoCusto" VisibleIndex="10" Width="100px">
        <PropertiesTextEdit DisplayFormatString="n2">
        </PropertiesTextEdit>
        <Settings AllowAutoFilter="False" />
        <EditFormSettings Visible="False" />
        <HeaderStyle HorizontalAlign="Right" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="IndicaPodeAlterar" VisibleIndex="11" Visible="False">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="IndicaPodeExcluir" VisibleIndex="12" Visible="False">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataComboBoxColumn Caption="Item Catálogo" FieldName="IdentificacaoItemCatalogo" VisibleIndex="2" Width="300px">
        <PropertiesComboBox DisplayFormatString="{0} - {1}" TextFormatString="{0} - {1}">
            <Columns>
                <dxtv:ListBoxColumn Caption="Item Catálogo" FieldName="IdentificacaoItemCatalogo" />
                <dxtv:ListBoxColumn Caption="Produto" FieldName="DescricaoProduto" />
            </Columns>
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True" />
            </ValidationSettings>
        </PropertiesComboBox>
        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
        <EditFormSettings Caption="Item Catálogo:" CaptionLocation="Top" ColumnSpan="2" />
    </dxtv:GridViewDataComboBoxColumn>
    <dxtv:GridViewDataComboBoxColumn Caption="Projeto" FieldName="NomeProjetoAtendido" VisibleIndex="3" Width="300px">
        <PropertiesComboBox DisplayFormatString="{0} - {1}" TextFormatString="{0} - {1}">
            <Columns>
                <dxtv:ListBoxColumn Caption="Projeto" FieldName="NomeProjeto" />
                <dxtv:ListBoxColumn Caption="Áreas Envolvidas" FieldName="Numero1" />
            </Columns>
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True" />
            </ValidationSettings>
        </PropertiesComboBox>
        <Settings AutoFilterCondition="Contains" />
        <EditFormSettings Caption="Projeto:" CaptionLocation="Top" ColumnSpan="2" />
    </dxtv:GridViewDataComboBoxColumn>
    <dxtv:GridViewDataComboBoxColumn Caption="Criticidade" FieldName="IndicaCriticidadeItem" VisibleIndex="4" Width="100px">
        <PropertiesComboBox>
            <Items>
                <dxtv:ListEditItem Selected="True" Text="Normal" Value="N" />
                <dxtv:ListEditItem Text="Crítico" Value="C" />
            </Items>
            <ValidationSettings Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="True" />
            </ValidationSettings>
        </PropertiesComboBox>
        <EditFormSettings Caption="Criticidade:" CaptionLocation="Top" />
    </dxtv:GridViewDataComboBoxColumn>
    <dxtv:GridViewDataComboBoxColumn Caption="Prioridade" FieldName="IndicaPrioridadeItem" VisibleIndex="5" Width="100px">
        <PropertiesComboBox>
            <Items>
                <dxtv:ListEditItem Text="Prioritário" Value="P" />
                <dxtv:ListEditItem Selected="True" Text="Não Prioritário" Value="N" />
            </Items>
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True" />
            </ValidationSettings>
        </PropertiesComboBox>
        <EditFormSettings Caption="Prioridade:" CaptionLocation="Top" />
    </dxtv:GridViewDataComboBoxColumn>
    <dxtv:GridViewDataMemoColumn Caption="Descrição" Visible="False" VisibleIndex="6" FieldName="DescricaoItemOS">
        <PropertiesMemoEdit Rows="7">
            <ValidationSettings Display="Dynamic">
            </ValidationSettings>
        </PropertiesMemoEdit>
        <EditFormSettings Caption="Descrição:" CaptionLocation="Top" ColumnSpan="2" Visible="True" />
    </dxtv:GridViewDataMemoColumn>
    <dxtv:GridViewDataTextColumn FieldName="CodigoProjetoAtendido" Visible="False" VisibleIndex="13">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="CodigoItemCatalogo" Visible="False" VisibleIndex="14">
    </dxtv:GridViewDataTextColumn>
</Columns>
     <TotalSummary>
         <dxtv:ASPxSummaryItem DisplayFormat="n2" FieldName="ValorPlanejadoCusto" ShowInColumn="ValorPlanejadoCusto" ShowInGroupFooterColumn="ValorPlanejadoCusto" SummaryType="Sum" />
         <dxtv:ASPxSummaryItem DisplayFormat="n2" FieldName="ValorAprovadoCusto" ShowInColumn="ValorAprovadoCusto" ShowInGroupFooterColumn="ValorAprovadoCusto" SummaryType="Sum" />
         <dxtv:ASPxSummaryItem DisplayFormat="n2" FieldName="ValorAtendidoCusto" ShowInColumn="ValorAtendidoCusto" ShowInGroupFooterColumn="ValorAtendidoCusto" SummaryType="Sum" />
     </TotalSummary>
</dxcp:ASPxGridView>

                    <dxcp:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
	mostraDivSalvoPublicado(s.cp_Msg);
}" />
                    </dxcp:ASPxCallback>

    <dxhf:ASPxHiddenField ID="hfWF" runat="server" ClientInstanceName="hfWF">
    </dxhf:ASPxHiddenField>

 <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido"><ContentCollection>
<dxcp:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxcp:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxcp:ASPxImage>


























 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxcp:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxcp:ASPxLabel>


























 </td></tr></tbody></table></dxcp:PopupControlContentControl>
</ContentCollection>
</dxcp:ASPxPopupControl>

                </td>
            </tr>
            <tr>
                <td align="right" style="padding-bottom: 10px">
                    <dxcp:ASPxButton ID="btnSalvar" runat="server"  Text="Salvar" Width="100px" AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) {
	
}" />
                        <Paddings Padding="0px" />
                    </dxcp:ASPxButton>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
