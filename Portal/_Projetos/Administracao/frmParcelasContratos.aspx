<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmParcelasContratos.aspx.cs"
    Inherits="_Projetos_Administracao_frmParcelasContratos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <link href="../../estilos/cdisEstilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function refreshGridParcelas() {
            gvParcelas.PerformCallback();
        }
        function mostraPopupParcelasContrato(values) {
            //debugger;

            var sHeight = Math.max(0, document.documentElement.clientHeight) + 70;
            var sWidth = Math.max(0, document.documentElement.clientWidth) + 70;

            var NumeroAditivoContrato = (values[0] != null ? values[0] : "");
            var NumeroParcela = (values[1] != null ? values[1] : "");
            var CodigoProjetoParcela = (values[2] != null ? values[2] : "");
            var CodigoContrato = (values[3] != null ? values[3] : "");

            var url = window.top.pcModal.cp_Path + '_Projetos/Administracao/popupParcelaContrato.aspx?';
            url += '&na=' + NumeroAditivoContrato;
            url += '&np=' + NumeroParcela;
            url += '&cpp=' + CodigoProjetoParcela;
            url += '&cc=' + CodigoContrato;
            url += '&IVG=' + gvParcelas.cp_InicioVigencia;
            url += '&RO=' + gvParcelas.cp_SomenteLeitura;
            url += '&TP=' + gvParcelas.cp_TP;
            url += '&ALT=' + (sHeight);

            window.top.showModal2(url, 'Editar detalhes da parcela', null, null, refreshGridParcelas, null);

        }

        function mostraPopupLancamentoFinanceiro(valores) {
            var url = window.top.pcModal.cp_Path + "_projetos/administracao/LancamentosFinanceirosConvenio.aspx";
            url += "?clf=" + valores[0];
            url += "&cc=" + valores[1];
            url += "&tipo=PAR";
            window.top.showModal2(url, "Parcelas", screen.width - 200, 500, atualizaGrid, null);
        }
        function atualizaGrid() {
          gvParcelas.Refresh();
        }

        function onValidation_NumeroAditivo(s, e) {
            var parcela = txtNumeroParcela.GetText();
            var parcelas = hfGeral.Get("ListaDeParcelas");
            var tipoOperacionEmParcela = hfGeral.Get("TipoOperacaoEmParcela");
            var verificar = false;

            if (tipoOperacionEmParcela != "Editar") {
                if (parcelas && parcelas.length > 0) {
                    var listaParcelas = parcelas.split(";");
                    for (var i in listaParcelas) {
                        //if(e.value == listaParcelas[i])
                        if ((e.value + parcela) == listaParcelas[i])
                            verificar = true;
                    }
                }
                if (verificar) {
                    e.isValid = false;
                    e.errorText = 'Parcela já cadastrada!';
                }
                else
                    e.isValid = true;
            }
        }

        /*-------------------------------------------------
        <summary></summary>
        -------------------------------------------------*/
        function onValidation_NumeroParcela(s, e) {
            var aditivo = txtNumeroAditivo.GetText();
            var parcelas = hfGeral.Get("ListaDeParcelas");
            var tipoOperacionEmParcela = hfGeral.Get("TipoOperacaoEmParcela");
            var verificar = false;

            if (tipoOperacionEmParcela != "Editar") {
                if (isNaN(e.value) || parseFloat(e.value) == 0) {
                    e.isValid = false;
                    e.errorText = 'Campo obrigatório!';
                } else {
                    if (parcelas && parcelas.length > 0) {
                        var listaParcelas = parcelas.split(";");
                        for (var i in listaParcelas) {
                            //if(e.value == listaParcelas[i])
                            if ((aditivo + e.value) == listaParcelas[i])
                                verificar = true;
                        }
                    }
                    if (verificar) {
                        e.isValid = false;
                        e.errorText = 'Parcela ou Aditivo já cadastrado!';
                    }
                    else
                        e.isValid = true;
                }
            }
        }


        /*-------------------------------------------------
        <summary></summary>
        -------------------------------------------------*/
        function onValidation_ValorPrevisto(s, e) {
            if (e.value == null) {
                e.isValid = false;
                e.errorText = 'Campo obrigatório!';
            } else {
                e.isValid = true;
            }
        }

        function onValidation_DataVencimento(s, e) {
            var valueData = (s.GetValue() == null || s.GetText() == "");

            if (valueData) {
                e.isValid = false;
                e.errorText = 'Campo Obrigatório!';
            } else {
                e.isValid = true;
            }
        }

        function onValidation_DataPagamentoGvParcela(s, e) {
            var test = txtValorPagoGvParcela.GetValue();
            var valorPago = (test == null || parseFloat(test.replace(',', '.')) == 0);
            var value = (s.GetValue() == null || s.GetText() == "");
            
            if (valorPago && !value) {
                e.isValid = false;
                e.errorText = 'Campo valor pago deve ser preenchido!';
            } else {
                onValidation_PosDataPagamento();
                e.isValid = true;
            }
        }

        function onValidation_ValorPagoGvParcela(s, e) {
            var test = ddlDataPagamento.GetValue();
            var dataPagamento = (test == null || ddlDataPagamento.GetText() == "");
            var value = (e.value == null || parseFloat(e.value.replace(',','.')) == 0);

            if (dataPagamento && !value) {
                e.isValid = false;
                e.errorText = 'Campo data de pagamento deve ser preenchido!';
            } else {
                onValidation_PosValorPago();
                e.isValid = true;
            }
        }

        function onValidation_PosDataPagamento() {
            var value = (txtValorPagoGvParcela.GetValue() == null) || (parseFloat(txtValorPagoGvParcela.GetValue().replace(',', '.')) == 0);
        
            if (!value) {
                txtValorPagoGvParcela.SetIsValid(true);
            }
        }

        function onValidation_PosValorPago() {
            var value = (ddlDataPagamento.GetValue() == null) || (ddlDataPagamento.GetText() == '');

            if (!value) {
                ddlDataPagamento.SetIsValid(true);
            }
        }
    </script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        
        <table class="headerGrid">
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

        <dxwgv:ASPxGridView ID="gvParcelas" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvParcelas"
             KeyFieldName="CodigoContrato;NumeroAditivoContrato;NumeroParcela"
            OnCellEditorInitialize="gvParcelas_CellEditorInitialize" OnCommandButtonInitialize="gvParcelas_CommandButtonInitialize"
            OnHtmlDataCellPrepared="gvParcelas_HtmlDataCellPrepared" OnRowDeleting="gvParcelas_RowDeleting"
            OnRowInserting="gvParcelas_RowInserting" OnRowUpdating="gvParcelas_RowUpdating"
            Width="100%" OnCustomErrorText="gvParcelas_CustomErrorText" OnCustomCallback="gvParcelas_CustomCallback" OnCustomButtonInitialize="gvParcelas_CustomButtonInitialize">
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
	//onClick_CustomButtomGridParcelas(s, e);
    if(e.buttonID == 'btnEditar')
    {
         if(s.cp_utilizaConvenio == undefined || s.cp_utilizaConvenio == &quot;N&quot;)
         {
           s.GetRowValues(s.GetFocusedRowIndex(), 'NumeroAditivoContrato;NumeroParcela;CodigoProjetoParcela;CodigoContrato;', mostraPopupParcelasContrato);     
         }
         else
         {
           s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoLancamentoFinanceiro;CodigoContrato', mostraPopupLancamentoFinanceiro);
         }
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
                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0"
                    ShowDeleteButton="true" Width="80px">
                    <CustomButtons>
                        <dxtv:GridViewCommandColumnCustomButton ID="btnEditar">
                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                            </Image>
                            <Styles>
                                <Style Cursor="pointer">
                                </Style>
                            </Styles>
                        </dxtv:GridViewCommandColumnCustomButton>
                        <dxtv:GridViewCommandColumnCustomButton ID="btnCloud">
                            <Image ToolTip="Parcela originária de integração com sistemas externos" Url="~/imagens/botoes/cloud2.png">
                            </Image>
                            <Styles>
                                <Style Cursor="help">
                                </Style>
                            </Styles>
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
                <dxwgv:GridViewDataTextColumn Caption="N&#186; Aditivo" FieldName="NumeroAditivoContrato"
                    Name="NumeroAditivo" VisibleIndex="1" Width="100px">
                    <PropertiesTextEdit ClientInstanceName="txtNumeroAditivo" MaxLength="3">
                        <ClientSideEvents Validation="function(s, e) {
	//onValidation_NumeroAditivo(s, e);
}" />
                        <MaskSettings Mask="&lt;0..999&gt;" />
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False" />
                    <EditFormSettings Caption="N&#186; Aditivo:" CaptionLocation="Top" VisibleIndex="1" />
                    <EditCellStyle >
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="N&#186; Parcela" FieldName="NumeroParcela"
                    Name="NumeroParcela" VisibleIndex="2" Width="100px">
                    <PropertiesTextEdit ClientInstanceName="txtNumeroParcela" MaxLength="3">
                        <ClientSideEvents Validation="function(s, e) {
	onValidation_NumeroParcela(s, e);
}" />
                        <MaskSettings Mask="&lt;0..999&gt;" />
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False" />
                    <EditFormSettings Caption="N&#186; Parcela:" CaptionLocation="Top" VisibleIndex="2" />
                    <EditCellStyle >
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Valor  Medido" FieldName="ValorPrevisto" Name="ValorPrevisto"
                    VisibleIndex="3" Width="100px">
                    <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:n2}">
                        <ClientSideEvents Validation="function(s, e) {
	onValidation_ValorPrevisto(s, e);
}" />
                        <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" />
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False" />
                    <EditFormSettings Caption="Valor  Medido:" CaptionLocation="Top" VisibleIndex="3" />
                    <EditCellStyle >
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataDateColumn Caption="Data de Vencimento" FieldName="DataVencimento"
                    Name="DataVencimento" VisibleIndex="5" Width="130px">
                    <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy"
                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                        <ValidationSettings>
                            <RequiredField ErrorText="Campo obrigat&#243;rio!" IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesDateEdit>
                    <Settings ShowFilterRowMenu="True" />
                    <EditFormSettings Caption="Data de Vencimento:" CaptionLocation="Top" VisibleIndex="4" />
                    <EditCellStyle >
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn Caption="Data de Pagamento" FieldName="DataPagamento"
                    Name="DataPagamento" VisibleIndex="6" Width="130px">
                    <PropertiesDateEdit ClientInstanceName="ddlDataPagamento" DisplayFormatInEditMode="True"
                        DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                        UseMaskBehavior="True">
                        <ClientSideEvents Validation="function(s, e) {
	onValidation_DataPagamentoGvParcela(s, e);
}" />
                        <ValidationSettings ValidationGroup="VGG">
                        </ValidationSettings>
                    </PropertiesDateEdit>
                    <Settings ShowFilterRowMenu="True" />
                    <EditFormSettings Caption="Data de Pagamento:" CaptionLocation="Top" VisibleIndex="6" />
                    <EditCellStyle >
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataTextColumn Caption="Valor Pago" FieldName="ValorPago" Name="ValorPago"
                    VisibleIndex="7" Width="100px">
                    <PropertiesTextEdit ClientInstanceName="txtValorPagoGvParcela" DisplayFormatInEditMode="True"
                        DisplayFormatString="{0:n2}">
                        <ClientSideEvents Validation="function(s, e) {
	onValidation_ValorPagoGvParcela(s, e);
}" />
                        <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" />
                        <ValidationSettings ValidationGroup="VGG">
                        </ValidationSettings>
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False" />
                    <EditFormSettings Caption="Valor Pago:" CaptionLocation="Top" VisibleIndex="5" />
                    <EditCellStyle >
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataMemoColumn Caption="Hist&#243;rico" FieldName="HistoricoParcela"
                    Name="HistoricoParcela" VisibleIndex="15" Width="400px">
                    <PropertiesMemoEdit Rows="4">
                        <ClientSideEvents KeyPress="function(s, e) {
 var texto = s.GetText();
 if(texto.length &gt; 500)
 {
  s.SetText(texto.substring(0,500));
 }
}" />
                    </PropertiesMemoEdit>
                    <EditFormSettings Caption="Hist&#243;rico:" CaptionLocation="Top" ColumnSpan="3"
                        VisibleIndex="11" />
                    <EditCellStyle >
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                </dxwgv:GridViewDataMemoColumn>
                <dxwgv:GridViewDataTextColumn Caption="CondigoContrato" FieldName="CodigoContrato"
                    Name="CodigoContrato" Visible="False" VisibleIndex="11">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="TipoRegistro" FieldName="TipoRegistro" Name="TipoRegistro"
                    Visible="False" VisibleIndex="12">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Nº Nota Fiscal" FieldName="NumeroNF" Name="NumeroNF"
                    Visible="False" VisibleIndex="8">
                    <PropertiesTextEdit MaxLength="20">
                    </PropertiesTextEdit>
                    <EditFormSettings Caption="Nº Nota Fiscal:" CaptionLocation="Top" Visible="True"
                        VisibleIndex="7" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Valor da Retenção" FieldName="ValorRetencao"
                    Name="ValorRetencao" VisibleIndex="9" Width="100px">
                    <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:n2}">
                        <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" />
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False" />
                    <EditFormSettings Caption="Valor da Retenção:" CaptionLocation="Top" Visible="True"
                        VisibleIndex="8" />
                    <HeaderStyle HorizontalAlign="Center" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Valor de Multas" FieldName="ValorMultas" Name="ValorMultas"
                    VisibleIndex="10" Width="100px">
                    <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:n2}">
                        <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" />
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False" />
                    <EditFormSettings Caption="Valor de Multas:" CaptionLocation="Top" Visible="True"
                        VisibleIndex="9" />
                    <HeaderStyle HorizontalAlign="Center" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="CodigoMedicao" Visible="False" VisibleIndex="16">
                </dxwgv:GridViewDataTextColumn>
                <dxtv:GridViewDataComboBoxColumn Caption="Conta Contábil" FieldName="CodigoConta"
                    VisibleIndex="13" Width="300px">
                    <PropertiesComboBox DataSourceID="dsConta" TextField="DescricaoConta" ValueField="CodigoConta"
                        ValueType="System.Int32">
                    </PropertiesComboBox>
                    <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="9" />
                    <HeaderStyle HorizontalAlign="Center" />
                </dxtv:GridViewDataComboBoxColumn>
                <dxtv:GridViewDataComboBoxColumn Caption="Projeto" FieldName="CodigoProjetoParcela" VisibleIndex="14" Width="200px">
                    <PropertiesComboBox DataSourceID="dsProjetos" TextField="NomeProjeto" ValueField="CodigoProjeto" ValueType="System.Int32">
                    </PropertiesComboBox>
                    <EditFormSettings CaptionLocation="Top" VisibleIndex="10" />
                    <HeaderStyle HorizontalAlign="Center" />
                </dxtv:GridViewDataComboBoxColumn>
                <dxtv:GridViewDataTextColumn Caption="Data de Emissão" FieldName="DataEmissaoNF" Visible="False" VisibleIndex="4">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="CodigoLancamentoFinanceiro" FieldName="CodigoLancamentoFinanceiro" Visible="False" VisibleIndex="17">
                </dxtv:GridViewDataTextColumn>
                <dxtv:GridViewDataTextColumn Caption="IndicaTipoIntegracao" FieldName="IndicaTipoIntegracao" ShowInCustomizationForm="False" Visible="False" VisibleIndex="18">
                </dxtv:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFooter="True" VerticalScrollBarMode="Visible"
                VerticalScrollableHeight="136" HorizontalScrollBarMode="Visible" />
            <StylesEditors>
                <Style ></Style>
            </StylesEditors>
        </dxwgv:ASPxGridView>
        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxhf:ASPxHiddenField>
        <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Default >
                </Default>
                <Header >
                </Header>
                <Cell >
                </Cell>
                <GroupFooter Font-Bold="True">
                </GroupFooter>
                <Title Font-Bold="True"></Title>
            </Styles>
        </dxwgv:ASPxGridViewExporter>
    </div>
    <asp:SqlDataSource ID="dsConta" runat="server" SelectCommand="SELECT pcfc.CodigoConta,
                           pcfc.CodigoReservadoGrupoConta + ' - ' + pcfc.DescricaoConta as DescricaoConta
                      FROM PlanoContasFluxoCaixa AS pcfc
                     WHERE pcfc.CodigoEntidade = @CodigoEntidade
                       AND pcfc.IndicaContaAnalitica = 'S'
                       AND pcfc.EntradaSaida = @EntradaSaida
                       ORDER BY pcfc.DescricaoConta ASC">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="sfCodigoEntidade" />
            <asp:SessionParameter Name="EntradaSaida" SessionField="sfEntradaSaida" />
        </SelectParameters>
    </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsProjetos" runat="server" SelectCommand=" SELECT rs.* 
   FROM
    (
     SELECT p.CodigoProjeto,
            p.NomeProjeto
       FROM Projeto AS p INNER JOIN
            LinkProjeto AS lp ON lp.CodigoProjetoFilho = p.CodigoProjeto
      WHERE lp.CodigoProjetoPai = @CodigoPrograma
        AND lp.TipoLink = 'PP'
        AND p.DataExclusao IS NULL
    UNION
     SELECT NULL, NULL
    ) AS rs
  ORDER BY 2">
            <SelectParameters>
                <asp:SessionParameter Name="CodigoPrograma" SessionField="codProg" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
