<%@ Page Language="C#" AutoEventWireup="true" CodeFile="url_auditoriaTarefaCronograma_Lista.aspx.cs"
    Inherits="_Processos_Visualizacao_RelatoriosURL_Administracao_url_auditoriaTarefaCronograma_Lista"
    Title="Portal da Estratégia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 10px;
        }
        .style3
        {
            width: 10px;
            height: 10px;
        }
        .style4
        {
            height: 10px;
        }
    </style>
    </head>
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../../../imagens/titulo/back_Titulo.gif);
        width: 100%">
        <tr>
            <td align="left" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                height: 26px">
                <table>

                </table>
            </td>
        </tr>
    </table>
    <table cellspacing="1" class="style1">
        <tr>
            <td class="style3">
            </td>
            <td style="height: 10px">
                <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="gvExporter" 
                    onrenderbrick="gvExporter_RenderBrick">
                </dxwgv:ASPxGridViewExporter>
            </td>
            <td class="style3">
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
            </td>
            <td>
                <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                    <ClientSideEvents CustomButtonClick="function(s, e) {
	gvDetalhes.PerformCallback(e.visibleIndex);
}" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="Usuário" FieldName="UsuarioOperacao" VisibleIndex="2"
                            Width="300px">
                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" AllowHeaderFilter="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Operação" FieldName="Operacao" VisibleIndex="3">
                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="True" AllowHeaderFilter="True" AutoFilterCondition="Equals" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Quando" FieldName="DataOperacao" VisibleIndex="4"
                            Width="135px">
                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                <CalendarProperties>
                                    <Style  />
                                    <DayHeaderStyle  />
                                    <WeekNumberStyle >
                                    </WeekNumberStyle>
                                    <DayStyle  />
                                    <DaySelectedStyle >
                                    </DaySelectedStyle>
                                    <DayOtherMonthStyle >
                                    </DayOtherMonthStyle>
                                    <DayWeekendStyle >
                                    </DayWeekendStyle>
                                    <DayOutOfRangeStyle >
                                    </DayOutOfRangeStyle>
                                    <TodayStyle >
                                    </TodayStyle>
                                    <ButtonStyle >
                                        <PressedStyle >
                                        </PressedStyle>
                                        <HoverStyle >
                                        </HoverStyle>
                                    </ButtonStyle>
                                    <HeaderStyle  />
                                    <FooterStyle  />
                                    <FastNavStyle >
                                    </FastNavStyle>
                                    <FastNavMonthAreaStyle >
                                    </FastNavMonthAreaStyle>
                                    <FastNavYearAreaStyle >
                                    </FastNavYearAreaStyle>
                                    <FastNavMonthStyle >
                                        <SelectedStyle >
                                        </SelectedStyle>
                                        <HoverStyle >
                                        </HoverStyle>
                                    </FastNavMonthStyle>
                                    <FastNavYearStyle >
                                        <SelectedStyle >
                                        </SelectedStyle>
                                        <HoverStyle >
                                        </HoverStyle>
                                    </FastNavYearStyle>
                                    <FastNavFooterStyle >
                                    </FastNavFooterStyle>
                                    <FocusedStyle >
                                    </FocusedStyle>
                                    <InvalidStyle >
                                    </InvalidStyle>
                                    <Style >
                                        
                                    </Style>
                                </CalendarProperties>
                                <NullTextStyle >
                                </NullTextStyle>
                                <FocusedStyle >
                                </FocusedStyle>
                                <Style >
                                    
                                </Style>
                            </PropertiesDateEdit>
                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Equals" AllowDragDrop="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="NomeProjeto" VisibleIndex="5" Caption="Projeto"
                            Width="350px">
                            <Settings AllowHeaderFilter="True" />
                            <Settings AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="NomeTarefa" VisibleIndex="6" Caption="Nome da Tarefa"
                            Width="350px">
                            <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Duração" FieldName="Duracao" VisibleIndex="7"
                            Width="95px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Início" FieldName="Inicio" VisibleIndex="8"
                            Width="135px">
                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                <CalendarProperties>
                                    <Style  />
                                    <DayHeaderStyle  />
                                    <DayStyle  />
                                    <DaySelectedStyle >
                                    </DaySelectedStyle>
                                    <DayOtherMonthStyle >
                                    </DayOtherMonthStyle>
                                    <DayWeekendStyle >
                                    </DayWeekendStyle>
                                    <DayOutOfRangeStyle >
                                    </DayOutOfRangeStyle>
                                    <TodayStyle >
                                    </TodayStyle>
                                    <ButtonStyle >
                                        <HoverStyle >
                                        </HoverStyle>
                                    </ButtonStyle>
                                    <HeaderStyle  />
                                    <FooterStyle  />
                                    <FastNavStyle >
                                    </FastNavStyle>
                                    <FastNavMonthAreaStyle >
                                    </FastNavMonthAreaStyle>
                                    <FastNavYearAreaStyle >
                                    </FastNavYearAreaStyle>
                                    <FastNavMonthStyle >
                                    </FastNavMonthStyle>
                                    <FastNavYearStyle >
                                        <SelectedStyle >
                                        </SelectedStyle>
                                        <HoverStyle >
                                        </HoverStyle>
                                    </FastNavYearStyle>
                                    <FastNavFooterStyle >
                                    </FastNavFooterStyle>
                                    <ReadOnlyStyle >
                                    </ReadOnlyStyle>
                                    <FocusedStyle >
                                    </FocusedStyle>
                                    <InvalidStyle >
                                    </InvalidStyle>
                                    <Style >
                                        
                                    </Style>
                                </CalendarProperties>
                                <MaskHintStyle >
                                </MaskHintStyle>
                                <NullTextStyle >
                                </NullTextStyle>
                                <ReadOnlyStyle >
                                </ReadOnlyStyle>
                                <FocusedStyle >
                                </FocusedStyle>
                                <InvalidStyle >
                                </InvalidStyle>
                                <Style >
                                    
                                </Style>
                            </PropertiesDateEdit>
                            <Settings AllowAutoFilter="True" AllowDragDrop="False" AutoFilterCondition="GreaterOrEqual" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Término" FieldName="Termino" VisibleIndex="9"
                            Width="135px">
                            <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                <CalendarProperties>
                                    <Style  />
                                    <DayHeaderStyle  />
                                    <DayStyle  />
                                    <DaySelectedStyle >
                                    </DaySelectedStyle>
                                    <DayOtherMonthStyle >
                                    </DayOtherMonthStyle>
                                    <DayWeekendStyle >
                                    </DayWeekendStyle>
                                    <DayOutOfRangeStyle >
                                    </DayOutOfRangeStyle>
                                    <ButtonStyle >
                                        <HoverStyle >
                                        </HoverStyle>
                                    </ButtonStyle>
                                    <HeaderStyle  />
                                    <FooterStyle  />
                                    <FastNavStyle >
                                    </FastNavStyle>
                                    <FastNavMonthAreaStyle >
                                    </FastNavMonthAreaStyle>
                                    <FastNavYearAreaStyle >
                                    </FastNavYearAreaStyle>
                                    <FastNavMonthStyle >
                                        <SelectedStyle >
                                        </SelectedStyle>
                                        <HoverStyle >
                                        </HoverStyle>
                                    </FastNavMonthStyle>
                                    <FastNavYearStyle >
                                        <SelectedStyle >
                                        </SelectedStyle>
                                        <HoverStyle >
                                        </HoverStyle>
                                    </FastNavYearStyle>
                                    <FastNavFooterStyle >
                                    </FastNavFooterStyle>
                                    <ReadOnlyStyle >
                                    </ReadOnlyStyle>
                                    <FocusedStyle >
                                    </FocusedStyle>
                                    <Style >
                                        
                                    </Style>
                                </CalendarProperties>
                                <MaskHintStyle >
                                </MaskHintStyle>
                                <ButtonStyle >
                                </ButtonStyle>
                                <NullTextStyle >
                                </NullTextStyle>
                                <ReadOnlyStyle >
                                </ReadOnlyStyle>
                                <FocusedStyle >
                                </FocusedStyle>
                                <InvalidStyle >
                                </InvalidStyle>
                                <Style >
                                    
                                </Style>
                            </PropertiesDateEdit>
                            <Settings AllowAutoFilter="True" AllowDragDrop="False" AutoFilterCondition="LessOrEqual" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Predecessoras" FieldName="Predecessoras" VisibleIndex="10"
                            Width="100px">
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="% Concluído" FieldName="PercentualFisicoConcluido"
                            VisibleIndex="11">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}%">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Duração Real" FieldName="DuracaoReal" VisibleIndex="12">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Início Real" FieldName="InicioReal" VisibleIndex="13"
                            Width="135px">
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Término Real" FieldName="TerminoReal" VisibleIndex="14"
                            Width="135px">
                            <PropertiesTextEdit>
                                <Style >
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Trabalho Real (hrs)" FieldName="TrabalhoReal"
                            VisibleIndex="15" Width="150px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                <Style >
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Despesa Real (R$)" FieldName="CustoReal" VisibleIndex="16"
                            Width="150px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                <Style >
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Início LB" FieldName="InicioLB" VisibleIndex="17"
                            Width="135px">
                            <PropertiesTextEdit>
                                <Style >
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Término LB" FieldName="TerminoLB" VisibleIndex="19"
                            Width="135px">
                            <PropertiesTextEdit>
                                <Style >
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="False" AllowDragDrop="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0" Width="45px">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton Text="Detalhes">
                                    <Image ToolTip="Detalhes" Url="~/imagens/botoes/pFormulario.png">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/botoes/btnExcel.png"
                                                OnClick="ImageButton1_Click" ToolTip="Exportar para excel"  />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" />
                    <SettingsPager PageSize="200">
                    </SettingsPager>
                    <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" HorizontalScrollBarMode="Visible"
                        ShowGroupPanel="True" ShowHeaderFilterBlankItems="False" />
                </dxwgv:ASPxGridView>
                <dxpc:ASPxPopupControl ID="pcDetalhes" runat="server" ClientInstanceName="pcDetalhes"
                     HeaderText="Detalhamento da Operação" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="850px">
                    <HeaderStyle Font-Bold="True" Font-Underline="True" />
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <table class="headerGrid">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td width="25">
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="True"
                                                        RightToLeft="False" Text="Usuário:       ">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="lblUsuario" runat="server" ClientInstanceName="lblUsuario"
                                                       >
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td align="right">
                                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="True"
                                                            RightToLeft="False" Text="Data:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel ID="lblData" runat="server" ClientInstanceName="lblData"
                                                           >
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td align="right">
                                                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True"
                                                            RightToLeft="False" Text="Operação:">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel ID="lblOperacao" runat="server" ClientInstanceName="lblOperacao"
                                                           >
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxwgv:ASPxGridView ID="gvDetalhes" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDetalhes"
                                             OnCustomCallback="gvDetalhes_CustomCallback"
                                            OnHtmlRowPrepared="gvDetalhes_HtmlRowPrepared" Width="100%">
                                            <ClientSideEvents EndCallback="function(s, e) {
	lblUsuario.SetText(s.cp_lblUsuario);
    lblData.SetText(s.cp_lblData);
    lblOperacao.SetText(s.cp_lblOperacao);
	pcDetalhes.Show();
}" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn Caption="Campo" FieldName="Descricao" ShowInCustomizationForm="True"
                                                    VisibleIndex="0">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Valor Antes" FieldName="ValorOLD" ShowInCustomizationForm="True"
                                                    VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Valor Depois" FieldName="Valor" ShowInCustomizationForm="True"
                                                    VisibleIndex="2">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="IndicaAlterado" ShowInCustomizationForm="True"
                                                    Visible="False" VisibleIndex="3">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" />
                                            <SettingsPager Mode="ShowAllRecords">
                                            </SettingsPager>
                                            <Settings VerticalScrollBarMode="Visible" />
                                        </dxwgv:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                             Text="Fechar">
                                            <ClientSideEvents Click="function(s, e) {
	pcDetalhes.Hide();
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
            </td>
            <td class="style2">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
