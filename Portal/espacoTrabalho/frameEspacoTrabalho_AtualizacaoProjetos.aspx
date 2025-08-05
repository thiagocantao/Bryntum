<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="frameEspacoTrabalho_AtualizacaoProjetos.aspx.cs" Inherits="espacoTrabalho_frameEspacoTrabalho_AtualizacaoProjetos"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height: 26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                    Text="Registro de Timesheet" ClientInstanceName="lblTituloTela">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" 
        style="width: 100%; padding-right: 10px;">
        <tr>
            <td style="width: 10px; height: 5px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 100%; padding-right: 5px;">
                    <tr>
                        <td style="width: 450px" valign="top">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                            Text="Ação:">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxComboBox ID="ddlProjeto" runat="server" 
                                            Width="445px" IncrementalFilteringMode="Contains" TextField="NomeProjeto" TextFormatString="{0}"
                                            ValueField="CodigoProjeto" ValueType="System.String">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback();
}"></ClientSideEvents>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" valign="top" style="width: 300px; padding-right: 10px; padding-left: 10px;">
                            <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" 
                                HeaderText="Período de Apontamento" View="GroupBox" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table cellspacing="0" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="De:"  ID="ASPxLabel4">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxtv:ASPxDateEdit ID="txtDe" runat="server" 
                                                            ClientInstanceName="txtDe" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" 
                                                            EditFormatString="dd/MM/yyyy"  
                                                            Width="105px">
                                                            <CalendarProperties HighlightToday="False" ShowClearButton="False" 
                                                                ShowTodayButton="False">
                                                            </CalendarProperties>
                                                            <ClientSideEvents DateChanged="function(s, e) {                        
                        var dataFimAtual = new Date(txtFim.GetValue());
                        var dataDe = new Date(s.GetValue());
                        var dataAte = new Date(dataDe);
                       dataAte.setDate(dataDe.getDate() + 6);

                       if(dataFimAtual &gt;dataAte){
                               txtFim.SetValue(dataAte );
                       }
                      else if (dataFimAtual&lt;dataDe){
                              txtFim.SetValue(dataDe );
                      }

                      txtFim.SetMinDate(dataDe);
                      txtFim.SetMaxDate(dataAte );                  
}" Init="function(s, e) {
               var data = new Date();
               data.setDate(data.getDate() - 6);

	txtDe.SetValue(data );
                txtDe.SetMaxDate(new Date());               
}" />
                                                        </dxtv:ASPxDateEdit>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="At&#233;:" 
                                                            ID="ASPxLabel5">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxtv:ASPxDateEdit ID="txtFim" runat="server" ClientInstanceName="txtFim" 
                                                            DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" 
                                                            EditFormatString="dd/MM/yyyy"  
                                                            Width="105px">
                                                            <CalendarProperties HighlightToday="False" ShowClearButton="False" 
                                                                ShowTodayButton="False">
                                                            </CalendarProperties>
                                                            <ClientSideEvents Init="function(s, e) {
var dataAtual = new Date();
var dataMinima = new Date();

dataMinima.setDate(dataMinima .getDate() - 6);

                txtFim.SetValue( dataAtual);
                txtFim.SetMaxDate( dataAtual );
                txtFim.SetMinDate( dataMinima);
}" />
                                                        </dxtv:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxrp:ASPxRoundPanel>
                        </td>
                        <td align="right" valign="bottom">
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
        </tr>
        <tr>
            <td style="width: 10px; height: 7px;">
            </td>
            <td style="height: 7px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxwgv:ASPxGridView ID="gvDados" runat="server" ClientInstanceName="gvDados"
                    Width="100%" AutoGenerateColumns="False" KeyFieldName="EstruturaHierarquica"
                    OnCommandButtonInitialize="gvDados_CommandButtonInitialize" OnCellEditorInitialize="gvDados_CellEditorInitialize"
                    OnRowUpdating="gvDados_RowUpdating" OnCustomCallback="gvDados_CustomCallback"
                    PreviewFieldName="EstruturaHierarquica" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared"
                    OnSummaryDisplayText="gvDados_SummaryDisplayText">
                    <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True">
                    </SettingsBehavior>
                    <Styles>
                        <FocusedRow BackColor="Transparent" ForeColor="Black">
                        </FocusedRow>
                    </Styles>
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n1}"></dxwgv:ASPxSummaryItem>
                        <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n1}"></dxwgv:ASPxSummaryItem>
                        <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n1}"></dxwgv:ASPxSummaryItem>
                        <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n1}"></dxwgv:ASPxSummaryItem>
                        <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n1}"></dxwgv:ASPxSummaryItem>
                        <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n1}"></dxwgv:ASPxSummaryItem>
                        <dxwgv:ASPxSummaryItem SummaryType="Sum" DisplayFormat="{0:n1}"></dxwgv:ASPxSummaryItem>
                    </TotalSummary>
                    <SettingsEditing Mode="Inline">
                    </SettingsEditing>
                    <SettingsText></SettingsText>
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" FixedStyle="Left" Width="80px" Caption=" "
                            VisibleIndex="0" ShowEditButton="true">
                            <FooterTemplate>
                                <strong>Total</strong>
                            </FooterTemplate>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Descricao" ReadOnly="True" FixedStyle="Left"
                            Width="370px" Caption="A&#231;&#227;o/Tipo de Atividade" VisibleIndex="1">
                            <DataItemTemplate>
                                <%# (Eval("Nivel").ToString() == "1" ?  "<b>" + Eval("Descricao") + "</b>" : "&nbsp;&nbsp;&nbsp;&nbsp;- "  + Eval("Descricao"))%>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Name="dia1" Width="105px" Caption="1" VisibleIndex="2">
                            <PropertiesTextEdit DisplayFormatString="{0:n5}">
                                <MaskSettings Mask="HH:mm" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Name="dia2" Width="105px" Caption="2" VisibleIndex="3">
                            <PropertiesTextEdit DisplayFormatString="{0:n5}">
                                <MaskSettings Mask="HH:mm" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Name="dia3" Width="105px" Caption="3" VisibleIndex="4">
                            <PropertiesTextEdit DisplayFormatString="{0:n5}">
                                <MaskSettings Mask="HH:mm" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Name="dia4" Width="105px" Caption="4" VisibleIndex="5">
                            <PropertiesTextEdit DisplayFormatString="{0:n5}">
                                <MaskSettings Mask="HH:mm" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Name="dia5" Width="105px" Caption="5" VisibleIndex="6">
                            <PropertiesTextEdit DisplayFormatString="{0:n5}">
                                <MaskSettings Mask="HH:mm" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Name="dia6" Width="105px" Caption="6" VisibleIndex="7">
                            <PropertiesTextEdit DisplayFormatString="{0:n5}">
                                <MaskSettings Mask="HH:mm" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Name="dia7" Width="105px" Caption="7" VisibleIndex="8">
                            <PropertiesTextEdit DisplayFormatString="{0:n5}">
                                <MaskSettings Mask="HH:mm" IncludeLiterals="DecimalSymbol"></MaskSettings>
                                <ValidationSettings ErrorDisplayMode="None">
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Nivel" Name="Nivel" Visible="False" VisibleIndex="14">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoTarefaTimeSheet" Visible="False"
                            VisibleIndex="9">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoTipoProjeto" Visible="False" VisibleIndex="9">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFooter="True" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible"
                        ShowStatusBar="Visible"></Settings>
                    <StylesEditors>
                        <ReadOnly BackColor="Transparent">
                            <Border BorderStyle="None"></Border>
                        </ReadOnly>
                        <ReadOnly BackColor="Transparent">
                            <Border BorderStyle="None"></Border>
                        </ReadOnly>
                        <TextBox BackColor="Transparent" >
                        </TextBox>
                    </StylesEditors>
                    <Templates>
                        <StatusBar>
                            <table cellpadding="0" cellspacing="0" style="vertical-align: middle;">
                                <tbody>
                                    <tr>
                                        <td>
                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/botoes/tarefasPPLenda.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td style="padding-right: 10px; padding-left: 5px">
                                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Font-Bold="False"
                                                Font-Size="7pt" Text="Envio Pendente">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxImage ID="ASPxImage2" runat="server" ImageUrl="~/imagens/botoes/tarefasPALenda.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td style="padding-right: 10px; padding-left: 5px">
                                            <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Font-Bold="False"
                                                Font-Size="7pt" Text="Pendente Aprovação">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="~/imagens/botoes/salvarLenda.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td style="padding-right: 10px; padding-left: 5px">
                                            <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Font-Bold="False"
                                                Font-Size="7pt" Text="Aprovado">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxImage ID="ASPxImage4" runat="server" ImageUrl="~/imagens/botoes/tarefaRecusadaLenda.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td style="padding-right: 10px; padding-left: 5px">
                                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Font-Bold="False"
                                                Font-Size="7pt" Text="Reprovado">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="border-right: gold 2px solid; border-top: gold 2px solid; border-left: gold 2px solid;
                                            width: 10px; border-bottom: gold 2px solid; background-color: #ffff66">
                                            &nbsp;
                                        </td>
                                        <td style="padding-left: 5px">
                                            <dxe:ASPxLabel ID="lblDescricaoNaoAtiva" runat="server" ClientInstanceName="lblDescricaoNaoAtiva"
                                                Font-Bold="False"  Text="Possui Comentários do Aprovador">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </StatusBar>
                    </Templates>
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td style="width: 10px; height: 6px">
            </td>
            <td style="height: 6px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table>
                    <tr>
                        <td style="width: 610px">
                            <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False"
                                Text="Publicar" Width="100px">
                                <Paddings Padding="0px"></Paddings>
                                <ClientSideEvents Click="function(s, e) {
window.top.mostraMensagem('Deseja Enviar as Tarefas para Aprovação?','confirmacao', true, true, enviaParaAprovacao);	
}"></ClientSideEvents>
                            </dxe:ASPxButton>
                            <dxcb:ASPxCallback ID="callBack1" runat="server" ClientInstanceName="callBack1" OnCallback="callBack1_CallBack">
                                <ClientSideEvents EndCallback="function(s, e) 
{
    if(s.cp_Erro != null &amp;&amp; s.cp_Erro != undefined)
          {
                   window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
          }
          else
          {
                    window.top.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
                        pcAprovacao.Hide();
                       gvDados.PerformCallback();
           }

}" />
                            </dxcb:ASPxCallback>
                        </td>
                        <td style="padding-left: 5px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <dxcb:ASPxCallback ID="callBack" runat="server" ClientInstanceName="callBack" OnCallback="callBack_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
          if(s.cp_Erro != null &amp;&amp; s.cp_Erro != undefined &amp;&amp; s.cp_Erro != '')
          {
                   window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
          }
          else
          {
                    window.top.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
                    gvDados.PerformCallback();
           }
}" />
    </dxcb:ASPxCallback>
    <dxpc:ASPxPopupControl ID="pcAprovacao" runat="server" ClientInstanceName="pcAprovacao"
         HeaderText="Comentários" Height="77px" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
        Width="729px" AllowDragging="True">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table>
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel11" runat="server" 
                                Text="Comentários do Recurso:">
                            </dxe:ASPxLabel>
                            &nbsp;&nbsp;&nbsp;
                            <dxe:ASPxLabel ID="lblCantCarater" runat="server" ClientInstanceName="lblCantCarater"
                                 ForeColor="Silver" Text="0">
                            </dxe:ASPxLabel>
                            &nbsp;<dxe:ASPxLabel ID="lblDe250" runat="server" ClientInstanceName="lblDe250"
                                ForeColor="Silver" Text=" de 1000">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxMemo ID="txtComentarioRecurso" runat="server" ClientInstanceName="txtComentarioRecurso"
                                 Rows="7" Width="692px">
                                <ClientSideEvents Init="function(s, e) 
{ 
	try
    {
       return setMaxLength(s.GetInputElement(), 1000);
    }
    catch(e)
    {
	}
}
" />
                                <DisabledStyle ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" 
                                Text="Comentários do Aprovador:">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxe:ASPxMemo ID="txtComentarioAprovador" runat="server" ClientEnabled="False" ClientInstanceName="txtComentarioAprovador"
                                 Rows="7" Width="692px">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxe:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                            <dxhf:ASPxHiddenField ID="hfData" runat="server" ClientInstanceName="hfData">
                            </dxhf:ASPxHiddenField>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table style="text-align:right" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                             Text="Salvar" Width="81px">
                                            <ClientSideEvents Click="function(s, e) {
callBack1.PerformCallback('S');

}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
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
                                    <td style="width:15px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
</asp:Content>
