<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmItensContrato.aspx.cs" Inherits="_Projetos_Administracao_frmItensContrato" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 14px;
        }
        .iniciais-maiusculas{
            text-transform:capitalize !important
        }
    </style>
    <script type="text/javascript">
        function mostraPopup(valores) {
            hfGeral.Set("TipoOperacao", "Editar");
            var DescricaoItem = (valores[0] == null) ? "" : valores[0].toString();
            var UnidadeMedida = (valores[1] == null) ? "" : valores[1].toString();
            var QuantidadePrevistaTotal = (valores[2] == null) ? "" : valores[2].toString();
            var ValorUnitarioItem = (valores[3] == null) ? "" : valores[3].toString();
            var ValorTotalPrevisto = (valores[4] == null) ? "" : valores[4].toString();
            //var DataExclusaoItem = (valores[5] == null) ? "" : valores[5].toString();

            txtDescricao.SetText(DescricaoItem);
            txtUnidadeMedida.SetText(UnidadeMedida);
            spnQuantidade.SetValue(QuantidadePrevistaTotal);
            spnValorUnitario.SetValue(ValorUnitarioItem);
            spnValorTotal.SetValue(ValorTotalPrevisto);


            const urlParams = new URLSearchParams(window.location.search);
            const somenteLeitura = (urlParams.get('RO') === 'S');

            txtDescricao.SetReadOnly(somenteLeitura);
            txtUnidadeMedida.SetReadOnly(somenteLeitura);
            spnQuantidade.SetReadOnly(somenteLeitura);
            spnValorUnitario.SetReadOnly(somenteLeitura);
            spnValorTotal.SetReadOnly(somenteLeitura);
            btnSalvar.SetEnabled(!somenteLeitura);
            pcDados.Show();
        }

        function limpaCamposPopup() {
            hfGeral.Set("TipoOperacao", "Incluir");
            txtDescricao.SetText('');
            txtUnidadeMedida.SetText('');
            spnQuantidade.SetValue(null);
            spnValorUnitario.SetValue(null);
            spnValorTotal.SetValue(null);
            pcDados.Show();
        }

        function validaCamposFormulario() {

            var mensagemErro = "";
            var countMsg = 0;

            if (txtDescricao.GetText() == "") {
                mensagemErro += countMsg++ + ") A descrição do item deve ser informada!\n";
            }
            if (txtUnidadeMedida.GetText() == "") {
                mensagemErro += countMsg++ + ") A unidade de medida do item deve ser informada!\n";
            }
            if (spnQuantidade.GetText() == "") {
                mensagemErro += countMsg++ + ") A quantidade deve ser informada!\n";
            }
            if (spnValorUnitario.GetText() == "") {
                mensagemErro += countMsg++ + ") O Valor unitário deve ser informado!\n";
            }
            return mensagemErro;
        }

        function excluirItem(codigoItem) {
            hfGeral.Set("TipoOperacao", "Excluir");
            var funcObj = { funcaoClickOK: function () { callbackTela.PerformCallback(); } }
            window.top.mostraConfirmacao('Deseja realmente excluir?', function () { funcObj['funcaoClickOK']() }, null);
            e.processOnServer = false;            
        }

        function preencheCampoValorTotal() {
            if (spnQuantidade.GetValue() > 0 && spnValorUnitario.GetValue() > 0) {
                var valorResultado = (spnQuantidade.GetValue() * spnValorUnitario.GetValue());
                spnValorTotal.SetValue(valorResultado);
            }

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%">
                        <tr>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel25" runat="server" Text="Nº Contrato:">
                                </dxtv:ASPxLabel>
                            </td>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel29" runat="server" Text="Tipo de Contrato:">
                                </dxtv:ASPxLabel>
                            </td>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel28" runat="server" Text="Status:">
                                </dxtv:ASPxLabel>
                            </td>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel27" runat="server" Text="Início da Vigência:">
                                </dxtv:ASPxLabel>
                            </td>
                            <td>
                                <dxtv:ASPxLabel ID="ASPxLabel26" runat="server" Text="Término da Vigência:">
                                </dxtv:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxtv:ASPxTextBox ID="txtNumeroContrato" runat="server" ClientInstanceName="txtNumeroContrato" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </td>
                            <td>
                                <dxtv:ASPxTextBox ID="txtTipoContrato" runat="server" ClientInstanceName="txtTipoContrato" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </td>
                            <td>
                                <dxtv:ASPxTextBox ID="txtStatusContrato" runat="server" ClientInstanceName="txtStatusContrato" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </td>
                            <td>
                                <dxtv:ASPxTextBox ID="txtInicioVigencia" runat="server" ClientInstanceName="txtInicioVigencia" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </td>
                            <td>
                                <dxtv:ASPxTextBox ID="txtTerminoVigencia" runat="server" ClientInstanceName="txtStatusContrato" MaxLength="50" ReadOnly="True" TabIndex="1" Width="100%">
                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </ReadOnlyStyle>
                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                    </DisabledStyle>
                                </dxtv:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
        </div>

        <dxwgv:ASPxGridView ID="gvItens" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvItens"
             KeyFieldName="CodigoItemMedicaoContrato"
            Width="100%" OnCustomCallback="gvItens_CustomCallback" OnCustomButtonInitialize="gvItens_CustomButtonInitialize">
            <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False"
                AllowSort="False" ConfirmDelete="True" />
            <StylesPopup>
                <EditForm>
                    <Header Font-Bold="True">
                    </Header>
                    <MainArea Font-Bold="False" ></MainArea>
                </EditForm>
            </StylesPopup>
            <Styles>
                <Header Font-Bold="False" Wrap="True">
                </Header>
                <HeaderPanel Font-Bold="False">
                </HeaderPanel>
                <TitlePanel Font-Bold="True">
                </TitlePanel>
                <Cell >
                </Cell>
                <EditForm >
                </EditForm>
                <EditFormCell >
                </EditFormCell>
            </Styles>
            <SettingsPager Mode="ShowAllRecords" Visible="False">
            </SettingsPager>
            <TotalSummary>
                <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorPrevisto" ShowInColumn="ValorPrevisto"
                    ShowInGroupFooterColumn="ValorPrevisto" SummaryType="Sum" Tag="Tot.:" />
                <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorPago" ShowInColumn="ValorPago"
                    ShowInGroupFooterColumn="ValorPago" SummaryType="Sum" Tag="Tat.:" />
                <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorRetencao" ShowInColumn="ValorRetencao"
                    ShowInGroupFooterColumn="ValorRetencao" SummaryType="Sum" />
                <dxwgv:ASPxSummaryItem DisplayFormat="{0:n2}" FieldName="ValorMultas" ShowInColumn="ValorMultas"
                    ShowInGroupFooterColumn="ValorMultas" SummaryType="Sum" />
            </TotalSummary>
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="3" />

<SettingsCommandButton>
<ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>

            <SettingsPopup>
                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                    VerticalOffset="-40" Width="600px" AllowResize="True" />
                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter"/>

<HeaderFilter MinHeight="140px"></HeaderFilter>
            </SettingsPopup>
            <SettingsText ConfirmDelete="Retirar a Parcela para este contrato?" PopupEditFormCaption="Parcela do Contrato"
                Title="Parcelas associadas" />
            <ClientSideEvents CustomButtonClick="function(s, e) {
    if(e.buttonID == 'btnEditar')
    {
                s.GetRowValues(s.GetFocusedRowIndex(), 'DescricaoItem;UnidadeMedida;QuantidadePrevistaTotal;ValorUnitarioItem;ValorTotalPrevisto;DataExclusaoItem', mostraPopup);     
    }
      if(e.buttonID == 'btnExcluir')
    {
                s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoItemMedicaoContrato', excluirItem);     
    }
}" EndCallback="function(s, e) {
    try{
    pnPagoAcumulado.PerformCallback(s.cpCodigoContrato);    
    pnSaldo.PerformCallback(s.cpCodigoContrato);
    pnPrevistoAcumulado.PerformCallback(s.cpCodigoContrato);
   }
    catch(e){
                
   }
}" Init="function(s, e) {
		 var sHeight = Math.max(0, document.documentElement.clientHeight) - 75;
                  var sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
                 s.SetHeight(sHeight);
}" />
            <Columns>
                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="80px">
                    <CustomButtons>
                        <dxtv:GridViewCommandColumnCustomButton ID="btnEditar">
                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                            </Image>
                        </dxtv:GridViewCommandColumnCustomButton>
                        <dxtv:GridViewCommandColumnCustomButton ID="btnExcluir">
                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
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
                                                </Items>
                                                <Image Url="~/imagens/botoes/btnDownload.png">
                                                </Image>
                                            </dxm:MenuItem>
                                            <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                <Items>
                                                    <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout" Name="btnSalvarLayout">
                                                        <Image IconID="save_save_16x16">
                                                        </Image>
                                                    </dxm:MenuItem>
                                                    <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout" Name="btnRestaurarLayout">
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
                    <FooterTemplate>
                        <dxe:ASPxLabel ID="lblTotales" runat="server" ClientInstanceName="lblTotales"
                            Text="TOTAL:">
                        </dxe:ASPxLabel>
                    </FooterTemplate>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Descrição" VisibleIndex="1" FieldName="DescricaoItem">
                    <EditCellStyle >
                    </EditCellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Unidade Medida" VisibleIndex="2" Width="100px" FieldName="UnidadeMedida">
                    <EditCellStyle >
                    </EditCellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="CodigoItemMedicaoContrato"
                    VisibleIndex="6" FieldName="CodigoItemMedicaoContrato" Visible="False">
                    <EditCellStyle >
                    </EditCellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxtv:GridViewDataSpinEditColumn Caption="Quantidade" FieldName="QuantidadePrevistaTotal" VisibleIndex="3" Width="170px">
                    <PropertiesSpinEdit DecimalPlaces="2" DisplayFormatString="n2" NumberFormat="Custom">
                    </PropertiesSpinEdit>
                </dxtv:GridViewDataSpinEditColumn>
                <dxtv:GridViewDataSpinEditColumn Caption="Valor Unitário" FieldName="ValorUnitarioItem" VisibleIndex="4" Width="120px">
                    <PropertiesSpinEdit DisplayFormatString="c2" NumberFormat="Custom">
                    </PropertiesSpinEdit>
                </dxtv:GridViewDataSpinEditColumn>
                <dxtv:GridViewDataSpinEditColumn Caption="Valor Total" FieldName="ValorTotalPrevisto" VisibleIndex="5" Width="200px">
                    <PropertiesSpinEdit DisplayFormatString="c2" NumberFormat="Custom">
                    </PropertiesSpinEdit>
                </dxtv:GridViewDataSpinEditColumn>
            </Columns>
            <Settings VerticalScrollBarMode="Visible"
                VerticalScrollableHeight="136" />
            <StylesEditors>
                <Style ></Style>
            </StylesEditors>
        </dxwgv:ASPxGridView>
        <dxcp:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" Height="150px" Width="850px" AllowDragging="True" AllowResize="True" CloseAction="None" HeaderText="Item" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False">
            <ContentCollection>
<dxcp:PopupControlContentControl runat="server">
    <table cellpadding="0" cellspacing="0" class="auto-style1">
        <tr>
            <td>
                <dxtv:ASPxLabel ID="ASPxLabel30" runat="server" Text="Descrição:">
                </dxtv:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxTextBox ID="txtDescricao" runat="server" ClientInstanceName="txtDescricao" Width="100%">
                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                    </ReadOnlyStyle>
                </dxtv:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 10px">
                <table cellpadding="0" cellspacing="0" class="auto-style1">
                    <tr>
                        <td class="auto-style2">
                            <dxtv:ASPxLabel ID="ASPxLabel31" runat="server" Text="Unidade Medida:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td class="auto-style2">
                            <dxtv:ASPxLabel ID="ASPxLabel32" runat="server" Text="Quantidade:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td class="auto-style2">
                            <dxtv:ASPxLabel ID="ASPxLabel33" runat="server" Text="Valor Unitário:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td class="auto-style2">
                            <dxtv:ASPxLabel ID="ASPxLabel34" runat="server" Text="Valor Total:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right: 5px">
                            <dxtv:ASPxTextBox ID="txtUnidadeMedida" runat="server" ClientInstanceName="txtUnidadeMedida" Width="100%">
                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                </ReadOnlyStyle>
                            </dxtv:ASPxTextBox>
                        </td>
                        <td style="padding-right: 5px">
                            <dxtv:ASPxSpinEdit ID="spnQuantidade" runat="server" ClientInstanceName="spnQuantidade" Number="0" Width="100%" DisplayFormatString="n2">
                                <SpinButtons ClientVisible="False">
                                </SpinButtons>
                                <ClientSideEvents GotFocus="function(s, e) {
	preencheCampoValorTotal();
}" KeyPress="function(s, e) {
	preencheCampoValorTotal();
}" LostFocus="function(s, e) {
	preencheCampoValorTotal();
}" NumberChanged="function(s, e) {
	preencheCampoValorTotal();
}" />
                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                </ReadOnlyStyle>
                            </dxtv:ASPxSpinEdit>
                        </td>
                        <td style="padding-right: 5px">
                            <dxtv:ASPxSpinEdit ID="spnValorUnitario" runat="server" ClientInstanceName="spnValorUnitario" Number="0" Width="100%" DisplayFormatString="c2">
                                <SpinButtons ClientVisible="False">
                                </SpinButtons>
                                <ClientSideEvents GotFocus="function(s, e) {
	preencheCampoValorTotal();
}" KeyPress="function(s, e) {
	preencheCampoValorTotal();
}" LostFocus="function(s, e) {
	preencheCampoValorTotal();
}" NumberChanged="function(s, e) {
		preencheCampoValorTotal();
}" ValueChanged="function(s, e) {
	preencheCampoValorTotal();
}" />
                            </dxtv:ASPxSpinEdit>
                        </td>
                        <td>
                            <dxtv:ASPxSpinEdit ID="spnValorTotal" runat="server" ClientInstanceName="spnValorTotal" Number="0" Width="100%" ReadOnly="True" DisplayFormatString="c2">
                                <SpinButtons ClientVisible="False">
                                </SpinButtons>
                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                </ReadOnlyStyle>
                            </dxtv:ASPxSpinEdit>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" style="padding-top: 10PX">
                <table cellpadding="0" cellspacing="0" style="width: 200px">
                    <tr>
                        <td style="width: 50%; padding-right: 5px">
                            <dxtv:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" Text="Salvar" Width="100%" AutoPostBack="False" CssClass="iniciais-maiusculas">
                                <ClientSideEvents Click="function(s, e) {
         var mensagemErroValidaFormulario = validaCamposFormulario();
         if(mensagemErroValidaFormulario == '')
         {
                  callbackTela.PerformCallback();
         } 
         else
        {
                  window.top.mostraMensagem(mensagemErroValidaFormulario, 'erro', true, false, null, null) ;
        }
}" />
                            </dxtv:ASPxButton>
                        </td>
                        <td style="width: 50%">
                            <dxtv:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100%" CssClass="iniciais-maiusculas">
                                <ClientSideEvents Click="function(s, e) {
	pcDados.Hide();
}" />
                            </dxtv:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
                </dxcp:PopupControlContentControl>
</ContentCollection>
        </dxcp:ASPxPopupControl>
        <dxcp:ASPxCallback ID="callbackTela" runat="server" ClientInstanceName="callbackTela" OnCallback="callbackTela_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
         if(s.cpErro !== '')
         {
                   window.top.mostraMensagem(s.cpErro, 'erro', true, false, null, null);
         }
         else
        {
                  if(s.cpSucesso !== '')
                  {
                           window.top.mostraMensagem(s.cpSucesso , 'sucesso', false, false, null, 3000);                           
                           pcDados.Hide(); 
                           gvItens.PerformCallback();                          
                   }
     }
}" />
        </dxcp:ASPxCallback>
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
        <dxcp:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
        </dxcp:ASPxGridViewExporter>
    </form>
</body>
</html>
