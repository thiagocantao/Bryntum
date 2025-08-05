<%@ Page Language="C#" AutoEventWireup="true" CodeFile="listaEntregasProjetos.aspx.cs"
    Inherits="_Projetos_DadosProjeto_listaEntregasProjetos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
</head>
<body style="margin: 0">
    <div>
        <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo.gif);
            width: 100%">
            <tr>
                <td align="left" valign="middle">
                    <table>
                        <tr>
                            <td align="center" style="width: 1px; height: 19px">
                                <span id="Span2" runat="server"></span>
                            </td>
                            <td align="left" style="height: 19px" valign="middle">
                                &nbsp; &nbsp;
                            </td>
                            <td align="right" style="height: 19px; width: 30px; padding-right: 5px;" valign="middle">
                                <asp:ImageButton cursor="hand" ID="imgExcel" runat="server" ImageUrl="~/imagens/botoes/btnExcel.png"
                                    OnClick="imgExcel_Click" 
                                    ToolTip="Relatório em EXCEL das Entregas dos Projetos" />
                            </td>
                            <td align="left" style="height: 19px; width: 30px; padding-right: 5px;" valign="middle">
                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/btnPDF.png" ClientInstanceName="btnImprimir"
                                    Cursor="pointer" ID="btnImprimir" ToolTip="Relatório em PDF das Entregas dos Projetos">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    var codigoProjeto = hfGeral.Get(&quot;CodigoProjeto&quot;);
    var where1 = gvDados.cp_Where;
    relListaEntregasProjetos(codigoProjeto,where1);
}"></ClientSideEvents>
                                </dxe:ASPxImage>
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
                <td style="height: 10px">
                    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvDados"
                        MaxColumnWidth="1000" PaperKind="A4" PaperName="RelatorioDeEntregasDoProjeto"
                        ReportHeader="{\rtf1\ansi\ansicpg1252\deff0\deflang1046{\fonttbl{\f0\fnil\fcharset0 Verdana;}}
\viewkind4\uc1\pard\qc\f0\fs32 Lista de Entregas do Projeto\fs20\par
}
" onrenderbrick="ASPxGridViewExporter1_RenderBrick">
                        <Styles>
                            <Default >
                            </Default>
                            <Header Font-Size="10pt">
                            </Header>
                            <Cell >
                            </Cell>
                        </Styles>
                        <PageHeader VerticalAlignment="None">
                            <Font></Font>
                        </PageHeader>
                    </dxwgv:ASPxGridViewExporter>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        OnCallback="pnCallback_Callback" Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent1" runat="server">
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoProjeto;NomeTarefa"
                                    AutoGenerateColumns="False" Width="100%" 
                                    ID="gvDados" OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize"
                                    OnAfterPerformCallback="gvDados_AfterPerformCallback">
                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
		OnGridFocusedRowChanged(s,true);
}" CustomButtonClick="function(s, e) 
{
	if(e.buttonID == &quot;btnFormulario&quot;)
	{			
		onClickBarraNavegacao(&quot;Editar&quot;, gvDados, pcDados);
	}
}"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoProjeto" VisibleIndex="1" Visible="False">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="NomeTarefa" VisibleIndex="2" Caption="Entrega"
                                            ExportWidth="400">
                                            <PropertiesTextEdit Width="200px">
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                            <CellStyle >
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataDateColumn Caption="Data Pactuada" FieldName="TerminoLB" ShowInCustomizationForm="True"
                                            VisibleIndex="3" Width="120px" ExportWidth="130">
                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" Width="100px" EditFormat="Custom"
                                                EditFormatString="dd/MM/yyyy">
                                                <CalendarProperties>
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
                                                        <PressedStyle >
                                                        </PressedStyle>
                                                        <HoverStyle >
                                                        </HoverStyle>
                                                    </ButtonStyle>
                                                    <Style >
                                                        
                                                    </Style>
                                                </CalendarProperties>
                                            </PropertiesDateEdit>
                                            <CellStyle  HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataDateColumn Caption="Data Real" FieldName="TerminoReal" ShowInCustomizationForm="True"
                                            VisibleIndex="4" Width="120px" ExportWidth="130">
                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" Width="100px" EditFormat="Custom"
                                                EditFormatString="dd/MM/yyyy">
                                                <CalendarProperties>
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
                                                    <HeaderStyle  />
                                                    <Style >
                                                        
                                                    </Style>
                                                </CalendarProperties>
                                            </PropertiesDateEdit>
                                            <CellStyle  HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Situacao" Caption="Situação" VisibleIndex="5"
                                            ExportWidth="170" Width="140px">
                                            <PropertiesTextEdit Width="200px">
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                            <CellStyle >
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <SettingsEditing Mode="PopupEditForm" />
                                    <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible"></Settings>
                                    <Styles>
                                        <Header >
                                        </Header>
                                        <Cell >
                                        </Cell>
                                        <TitlePanel Font-Size="16pt">
                                        </TitlePanel>
                                    </Styles>
                                </dxwgv:ASPxGridView>
                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <dxe:ASPxFilterControl ID="ASPxFilterControl1" runat="server" ClientVisible="False">
                                </dxe:ASPxFilterControl>
                                <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados"
                                    CloseAction="None" HeaderText="Detalhes" PopupAction="None" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" PopupVerticalOffset="40" ShowCloseButton="False"
                                    Width="690px" Height="251px"  ID="pcDados">
                                    <HeaderStyle Font-Bold="True"></HeaderStyle>
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                            <table cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 189px">
                                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/btnPDF.png" ClientInstanceName="btnRelatorioRisco"
                                                                                EnableClientSideAPI="True" Cursor="pointer" ToolTip="Gerar Relat&#243;rio em PDF"
                                                                                ID="btnRelatorioRisco">
                                                                                <ClientSideEvents Click="function(s, e) 
{
mostrarRelatorioLicoesAprendidas();
e.processOnServer = false;
}"></ClientSideEvents>
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 189px; height: 10px">
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 189px">
                                                                            <dxe:ASPxLabel runat="server" Text="Data:"  ID="ASPxLabel4">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="Inclu&#237;da Por:" 
                                                                                ID="ASPxLabel5">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 189px">
                                                                            <dxe:ASPxDateEdit runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                                DisplayFormatString="dd/MM/yyyy" ReadOnly="True" ClientInstanceName="dteData"
                                                                                 ID="dteData">
                                                                                <CalendarProperties ClearButtonText="Limpar">
                                                                                </CalendarProperties>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtIncluidaPor"
                                                                                ClientEnabled="False"  ID="txtIncluidaPor">
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
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 190px">
                                                                            <dxe:ASPxLabel runat="server" Text="Tipo:"  ID="ASPxLabel6">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td style="width: 475px">
                                                                            <dxe:ASPxLabel runat="server" Text="Assunto:" 
                                                                                ID="ASPxLabel7">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 190px">
                                                                            <dxe:ASPxTextBox runat="server" Width="170px" ClientInstanceName="txtTipo" ClientEnabled="False"
                                                                                 ID="txtTipo">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td style="width: 475px">
                                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtAssunto" ClientEnabled="False"
                                                                                 ID="txtAssunto">
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
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel runat="server" Text="Projeto:" 
                                                                ID="ASPxLabel8">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtProjeto" ClientEnabled="False"
                                                                 ID="txtProjeto">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel runat="server" Text="Li&#231;&#227;o:" 
                                                                ID="ASPxLabel9">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxMemo runat="server" Rows="9" Width="100%" ClientInstanceName="txtLicao"
                                                                ClientEnabled="False"  ID="txtLicao">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 100px">
                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" ClientVisible="False"
                                                                                ClientEnabled="False" Text="Salvar" Width="90px" 
                                                                                ID="btnSalvar">
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td style="width: 100px">
                                                                            <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="90px"
                                                                                 ID="btnCancelar">
                                                                                <ClientSideEvents Click="function(s, e) 
{
		e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>
                                                                            </dxe:ASPxButton>
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
                                <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcMensagemGravacao" HeaderText="Incluir a Entidad Atual"
                                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                    ShowHeader="False" Width="270px"  ID="pcMensagemGravacao">
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td align="center" style="">
                                                        </td>
                                                        <td align="center" rowspan="3" style="width: 70px">
                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                                                ClientInstanceName="imgSalvar" ID="imgSalvar">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
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
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
