<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="ListaMedicoes.aspx.cs" Inherits="administracao_ListaMedicoes" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 140px;
        }
        
        
        .style7
        {
            width: 200px;
            border-style:solid;
            border-width:1px;
        }
        
                
        .btNovasMedicoesStyle
        {
            width: 22px;
            height: 22px;
            cursor:pointer;
        }
        
        .style14
        {
            width: 100%;
        }
        .style15
        {
            height: 10px;
        }
        .style16
        {
            width: 145px;
        }
        
    </style>

    <div>
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%; height: 26px;">
        <tr>
            <td align="left">
                <table>
                    <tr style="height:26px">
                        <td align="center" style="width: 1px; height: 26px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="top" class="style16" style="padding-top: 2px">
                           <table><tr>
                           <td style="padding-left: 10px; padding-right: 10px">
                            <dxe:aspxlabel id="lblTitulo" runat="server" Font-Bold="True"
                                Text="Medições"></dxe:aspxlabel> 
                           </td>
                           <td valign="middle">
                               <dxe:aspximage ID="btNovasMedicoes" runat="server" class="btNovasMedicoesStyle" 
                                   ClientInstanceName="btNovasMedicoes" ImageAlign="Middle" 
                                   ImageUrl="~/imagens/botoes/incluirReg02.png" 
                                   ToolTip="Incluir novas medições">
                                   <ClientSideEvents Click="function(s, e) {
    gvContratos.PerformCallback();
	pcNovasMedicoes.Show();
}" />
                               </dxe:aspximage>
                            </td>
                            </tr></table>
                        </td>

                        <td align="right" valign="middle">
                            <dxe:aspxcheckbox ID="cbMedicoesPendentes" runat="server" Checked="True" 
                                CheckState="Checked" Text="somente minhas pendências &nbsp;&nbsp;" 
                                >
                                <ClientSideEvents CheckedChanged="function(s, e) {
	gvMedicao.PerformCallback();
}" />
                            </dxe:aspxcheckbox>
                            &nbsp;</td>

                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width:100%">
    <tr>
    <td>
        <dxwgv:aspxgridview ID="gvMedicao" runat="server" AutoGenerateColumns="False" 
            ClientInstanceName="gvMedicao"  
            KeyFieldName="CodigoMedicao" Width="100%" 
            oncustombuttoninitialize="gvMedicao_CustomButtonInitialize" 
            oncustomcallback="gvMedicao_CustomCallback" 
            
            
            onautofiltercelleditorinitialize="gvMedicao_AutoFilterCellEditorInitialize" 
            KeyboardSupport="True">
            <ClientSideEvents FocusedRowChanged="function(s, e) {
	tlItensMedicao.PerformCallback();
}" CustomButtonClick="function(s, e) {
	gvMedicao.SetFocusedRowIndex(e.visibleIndex);
    //gvMedicao.PerformCallback(); 	
	e.processOnServer = false;
    if (e.buttonID == &quot;btnDetalhes&quot;)
    {
       gvMedicao.GetRowValues(gvMedicao.GetFocusedRowIndex(), 'CodigoMedicao;Contrato;Mes_Ano;Fornecedor;ObjetoContrato;ValorContrato;DataInicio;DataTermino;DataBaseReajuste;CodigoContrato;ValorTotalMedicao;ValorMedidoAteMes;', MontaCampos );
       gvHistorico.PerformCallback(e.visibleIndex);   
	   gvImpostos.PerformCallback(e.visibleIndex);      
    }
    else
    if (e.buttonID == &quot;btnEditar&quot;)
    {
       //gvMedicao.GetRowValues(gvMedicao.GetFocusedRowIndex(), 'CodigoMedicao;Contrato;Mes_Ano;Fornecedor;ObjetoContrato;ValorContrato;DataInicio;DataTermino;DataBaseReajuste;CodigoContrato;ValorTotalMedicao;ValorMedidoAteMes;', ShowPcMudaStatus );
       pnGeral.PerformCallback(e.visibleIndex);
       
    }
    else
    if (e.buttonID == &quot;btnMedicao&quot;)
    {
    	var codigoMedicao = s.GetRowKey(e.visibleIndex);  
		if(codigoMedicao != null)
			abreMedicao(codigoMedicao);     
    }
    else
    if (e.buttonID == &quot;btnExcluir&quot;)
    {
       if (confirm(&quot;Confirma a exclusão desta medição ?&quot;))
       {
           //gvMedicao.GetRowValues(gvMedicao.GetFocusedRowIndex(), 'CodigoMedicao;Contrato;Mes_Ano;Fornecedor;ObjetoContrato;ValorContrato;DataInicio;DataTermino;DataBaseReajuste;CodigoContrato;ValorTotalMedicao;ValorMedidoAteMes;', ShowPcMudaStatus );
           gvMedicao.PerformCallback(e.visibleIndex);
       }
    }
}" EndCallback="function(s, e) {
      if(s.cp_Msg != '')
      {
                 window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null);
                 tlItensMedicao.PerformCallback();
      }
      else if(s.cp_Erro != '')
     {
                 window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
     }
}" />
            <Columns>
                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="120px" 
                    Caption=" ">
                    <CustomButtons>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Aprovar/Reprovar">
                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                            </Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                            </Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhes" Text="Detalhes">
                            <Image Url="~/imagens/botoes/pFormulario.png">
                            </Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnMedicao" 
                            Text="Realizar Medição">
                            <Image ToolTip="Realizar Medição" Url="~/imagens/botoes/capacete.png">
                            </Image>
                        </dxwgv:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Mês/Ano" FieldName="Mes_Ano" 
                    VisibleIndex="3" Width="85px">
                    <Settings AllowAutoFilter="False" AllowHeaderFilter="True" AllowGroup="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Nº Contrato" FieldName="Contrato" 
                    VisibleIndex="4" Width="115px">
                    <Settings AutoFilterCondition="Contains" AllowGroup="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" 
                    VisibleIndex="5" Width="150px">
                    <Settings AllowAutoFilter="False" AllowHeaderFilter="True" AllowGroup="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Projeto / Obra" FieldName="Projeto" 
                    VisibleIndex="9">
                    <Settings AutoFilterCondition="Contains" AllowGroup="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="CodigoMedicao" FieldName="CodigoMedicao" 
                    VisibleIndex="2" Width="50px" Visible="False">
                    <Settings AllowGroup="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Objeto do Contrato" 
                    FieldName="ObjetoContrato" VisibleIndex="7" Visible="False">
                    <Settings AllowGroup="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Fornecedor" FieldName="Fornecedor" 
                    VisibleIndex="6" Width="200px">
                    <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" 
                        AllowGroup="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Valor Total Medição" 
                    FieldName="ValorTotalMedicao" VisibleIndex="11" Width="125px">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False" AllowGroup="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DataInicioContrato" 
                    FieldName="DataInicio" Visible="False" VisibleIndex="10">
                    <Settings AllowGroup="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DataTerminoContrato" 
                    FieldName="DataTermino" Visible="False" VisibleIndex="15">
                    <Settings AllowGroup="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ValorContrato" FieldName="ValorContrato" 
                    Visible="False" VisibleIndex="8">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                    <Settings AllowGroup="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DataBaseReajuste" 
                    FieldName="DataBaseReajuste" Visible="False" VisibleIndex="17">
                    <Settings AllowGroup="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewCommandColumn Caption="Marcar" ShowSelectCheckbox="True" 
                    VisibleIndex="1" Width="32px" Visible="False">
                    <CellStyle VerticalAlign="Middle">
                    </CellStyle>
                    <HeaderTemplate>
                        <input onclick="gvMedicao.SelectAllRowsOnPage(this.checked);" title="Marcar/Desmarcar Todos"
                                                    type="checkbox" />
                    </HeaderTemplate>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="CodigoContrato" 
                    FieldName="CodigoContrato" Visible="False" VisibleIndex="16">
                    <Settings AllowGroup="False" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="CodigoProjeto" FieldName="CodigoProjeto" 
                    Visible="False" VisibleIndex="14">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ValorMedidoAteMes" 
                    FieldName="ValorMedidoAteMes" Visible="False" VisibleIndex="13">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="IniciaisStatus" Visible="False" 
                    VisibleIndex="12">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <SettingsPager PageSize="50">
            </SettingsPager>
            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="197" 
                ShowFilterRow="True" ShowHeaderFilterBlankItems="False" 
                ShowGroupPanel="True" />
            <Styles>
                <SelectedRow BackColor="White" ForeColor="#990000">
                </SelectedRow>
                <FocusedRow BackColor="White" ForeColor="#0033CC" Font-Bold="True">
                </FocusedRow>
            </Styles>
        </dxwgv:aspxgridview>

    </td>
    </tr>
    <tr style="height:6px;padding-bottom:0px;padding-top:0px">
    <td style="height:3px;">
        <span>&nbsp;</span>
    </td>
    </tr>

    <tr style="height:8px;padding-bottom:0px;padding-top:0px">
    <td>
        <span>Itens da Medição:</span>
        <div id="detalhes" style="overflow:auto; height:<%=alturaDiv%>">
        <dxwtl:aspxtreelist ID="tlItensMedicao" runat="server" 
            AutoGenerateColumns="False" Width="100%" 
            ClientInstanceName="tlItensMedicao" KeyboardSupport="True" 
            KeyFieldName="CodigoItemMedicaoContrato" ParentFieldName="CodigoTarefaPai" 
            >
            <Columns>
                <dxwtl:TreeListTextColumn Caption="CodigoMedicao" FieldName="CodigoMedicao" 
                    Visible="False" VisibleIndex="0">
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn Caption="CodigoItemMedicao" 
                    FieldName="CodigoItemMedicao" Visible="False" VisibleIndex="1">
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn Caption="CodigoItemMedicaoContrato" 
                    FieldName="CodigoItemMedicaoContrato" Visible="False" VisibleIndex="2">
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn Caption="CodigoTarefaPai" FieldName="CodigoTarefaPai" 
                    Visible="False" VisibleIndex="3">
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn Caption="Descrição Item" FieldName="DescricaoItem" 
                    VisibleIndex="4">
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn Caption="Quant. Prevista Total" 
                    FieldName="QuantidadePrevistaTotal" VisibleIndex="5" Width="130px">
                     <DataCellTemplate>
                       <%# getValorFormatado(Eval("QuantidadePrevistaTotal") + "")%>
                    </DataCellTemplate>
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn Caption="Unidade" 
                    FieldName="UnidadeMedidaItem" VisibleIndex="6" Width="70px">
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn Caption="Valor Unitário " 
                    FieldName="ValorUnitarioItem" VisibleIndex="7" Width="120px">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dxwtl:TreeListTextColumn>
<dxwtl:TreeListTextColumn FieldName="ValorPrevistoTotal" 
                    Width="120px" Caption="Valor Previsto Total" VisibleIndex="8">
<PropertiesTextEdit DisplayFormatString="{0:n2}"></PropertiesTextEdit>

<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>

<CellStyle HorizontalAlign="Right"></CellStyle>
</dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn Caption="Valor Medido Mês" 
                    FieldName="ValorMedidoMes" VisibleIndex="9" Width="120px">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                    </PropertiesTextEdit>
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListTextColumn Caption="Quant. Medida Mês" 
                    FieldName="QuantidadeMedidaMes" VisibleIndex="10" Width="120px">
                    <DataCellTemplate>
                       <%# getValorFormatado(Eval("QuantidadeMedidaMes") + "")%>
                    </DataCellTemplate>
                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                    <CellStyle BackColor="#CCFFCC" HorizontalAlign="Right" Font-Bold="True">
                    </CellStyle>
                </dxwtl:TreeListTextColumn>
            </Columns>
            <Settings GridLines="Horizontal" VerticalScrollBarMode="Auto" 
                ScrollableHeight="150" />
            <SettingsBehavior AllowDragDrop="False" />
            <Styles>
                <Header  Wrap="True">
                </Header>
            </Styles>
        </dxwtl:aspxtreelist>
        </div>

    </td>
    </tr>
    </table>
    </div>
    <asp:SqlDataSource ID="dsMedicao" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsItensMedicao" runat="server"></asp:SqlDataSource>
        <dxpc:ASPxPopupControl ID="pcUsuarioIncluido" runat="server" ClientInstanceName="pcUsuarioIncluido"
             HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
            Width="270px">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td align="center" style="">
                                </td>
                                <td align="center" rowspan="3" style="width: 70px">
                                    <dxe:ASPxImage ID="imgSalvar" runat="server" ClientInstanceName="imgSalvar" ImageAlign="TextTop"
                                        ImageUrl="~/imagens/Workflow/salvarBanco.png">
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <dxe:ASPxLabel ID="lblAcaoGravacao" runat="server" ClientInstanceName="lblAcaoGravacao"
                                        EncodeHtml="False" >
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    <dxpc:aspxpopupcontrol ID="pcDetalhesMedicao" runat="server" 
        ClientInstanceName="pcDetalhesMedicao" Height="331px" Modal="True" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        Width="968px" AllowDragging="True" CloseAction="CloseButton" 
        HeaderText="Detalhes" >
        <ClientSideEvents AfterResizing="function(s, e) {
}" Shown="function(s, e) {

}" CloseUp="function(s, e) {
	pcToolTip.SetPopupElementID('');
}" />
  <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dxtc:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
        Height="262px" Width="100%" >
        <TabPages>
            <dxtc:TabPage Text="Histórico da Medição">
                <ContentCollection>
                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">

                        <dxpc:ASPxPopupControl ID="pcToolTip" runat="server" BackColor="#FFFFCC" 
                            ClientInstanceName="pcToolTip"  
                            HeaderText="Incluir a Entidade Atual" PopupHorizontalAlign="LeftSides" 
                            PopupVerticalAlign="Below" ShowCloseButton="False" ShowHeader="False" 
                            Width="420px">
                            <ContentStyle>
                                <Paddings Padding="3px" />
                            </ContentStyle>
                            <ContentCollection>
                                <dxpc:PopupControlContentControl runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dxe:ASPxLabel ID="lblToolTip" runat="server" ClientInstanceName="lblToolTip" 
                                        EncodeHtml="False" >
                                    </dxe:ASPxLabel>
                                </dxpc:PopupControlContentControl>
                            </ContentCollection>
                            <Border BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                        </dxpc:ASPxPopupControl><dxwgv:ASPxGridView ID="gvHistorico" runat="server" 
                            AutoGenerateColumns="False" Width="100%" 
                            ClientInstanceName="gvHistorico" 
                            OnHtmlDataCellPrepared="gvHistorico_HtmlDataCellPrepared" 
                            OnCustomCallback="gvHistorico_CustomCallback"><ClientSideEvents EndCallback="function(s, e) {
	pcDetalhesMedicao.Show();
}" /><Columns>
                                <dxwgv:GridViewDataTextColumn Caption="Status (para)" FieldName="DescricaoStatusMedicao" 
                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="250px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Data" FieldName="DataHistorico" 
                                    ShowInCustomizationForm="True" VisibleIndex="0" Width="95px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Usuário" FieldName="NomeUsuario" 
                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="150px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Comentário" 
                                    FieldName="ComentarioMudancaStatus" ShowInCustomizationForm="True" 
                                    VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Status (de)" 
                                    FieldName="DescricaoStatusAnterior" ShowInCustomizationForm="True" 
                                    VisibleIndex="1" Width="250px">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager PageSize="7">
                            </SettingsPager>
                        </dxwgv:ASPxGridView>

                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
            <dxtc:TabPage Text="Impostos e Retenções">
                <ContentCollection>
                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dxwgv:ASPxGridView ID="gvImpostos" runat="server" AutoGenerateColumns="False" 
                            ClientInstanceName="gvImpostos"  
                            OnCustomCallback="gvImpostos_CustomCallback" 
                            OnHtmlDataCellPrepared="gvHistorico_HtmlDataCellPrepared" Width="50%">
                            <ClientSideEvents EndCallback="function(s, e) {
	pcDetalhesMedicao.Show();
}" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="Descricao" 
                                    ShowInCustomizationForm="True" VisibleIndex="0" Width="95px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Valor (R$)" FieldName="Valor" 
                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="150px">
                                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Alíquota (%)" FieldName="Aliquota" 
                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="50px">
                                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager PageSize="9">
                            </SettingsPager>
                        </dxwgv:ASPxGridView>
                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
            <dxtc:TabPage Text="Contrato">
                <ContentCollection>
                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Fornecedor:" 
                                        >
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxTextBox ID="txtFornecedor" runat="server" Width="100%" 
                                            ClientEnabled="False" ClientInstanceName="txtFornecedor">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Objeto:" 
                                            >
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxTextBox ID="txtObjeto" runat="server" ClientInstanceName="txtObjeto" 
                                            Width="100%" ClientEnabled="False">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tr>
                                                <td style="width: 50%">
                                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Data início vigência:" 
                                                        >
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Data término vigência:" 
                                                        >
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 5px; width: 50%;">
                                                    <dxe:ASPxTextBox ID="txtDataInicio" runat="server" 
                                                        ClientInstanceName="txtDataInicio" Width="100%" ClientEnabled="False">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtDataTermino" runat="server" 
                                                        ClientInstanceName="txtDataTermino" Width="100%" ClientEnabled="False">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 25%">
                                                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Valor contrato: R$" 
                                                        >
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 25%">
                                                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Valor medido até o mês: R$" 
                                                        >
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 25%">
                                                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="Valor medido no mês: R$" 
                                                        >
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 25%">
                                                    <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="Saldo: R$" 
                                                        >
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtValor" runat="server" ClientInstanceName="txtValor" 
                                                        Width="100%" ClientEnabled="False">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtValorMedidoAteMes" runat="server" 
                                                        ClientInstanceName="txtValorMedidoAteMes" Width="100%" 
                                                        ClientEnabled="False">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtValorMedidoNoMes" runat="server" 
                                                        ClientInstanceName="txtValorMedidoNoMes" Width="100%" 
                                                        ClientEnabled="False">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dxe:ASPxTextBox ID="txtSaldo" runat="server" ClientInstanceName="txtSaldo" 
                                                        Width="100%" ClientEnabled="False">
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                        </table>


                    </dxw:ContentControl>
                </ContentCollection>
            </dxtc:TabPage>
        </TabPages>
    </dxtc:ASPxPageControl>
    <table border="0" cellpadding="3" cellspacing="3" style="width:100%">
    <tbody><tr>
    <td style="width:45%">&nbsp;</td><td style="width:45%">&nbsp;</td><td><dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100px" 
                                             
            ID="btnFechar" AutoPostBack="False">
             <ClientSideEvents Click="function(s, e) {	
 pcDetalhesMedicao.Hide(); }"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton></td>
    </tr>
    </table>
 </dxpc:PopupControlContentControl>
</ContentCollection>
    </dxpc:aspxpopupcontrol>
    <dxpc:aspxpopupcontrol ID="pcMudaStatus" runat="server" 
        ClientInstanceName="pcMudaStatus" Height="449px" Modal="True" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        Width="769px" AllowDragging="True" CloseAction="CloseButton" 
        HeaderText="" >
        <ClientSideEvents AfterResizing="function(s, e) {
}" Shown="function(s, e) {

}" />
        <ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">

<dxcp:ASPxCallbackPanel ID="pnGeral"  ClientInstanceName="pnGeral" runat="server" 
        Width="100%" OnCallback="pnGeral_Callback">
    <ClientSideEvents EndCallback="function(s, e) {
	pcMudaStatus.SetHeaderText(s.cp_header);
	pcMudaStatus.Show();
}" />
<PanelCollection>
                 <dxp:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                 

    <table>
    <tr><td>
        <dxe:ASPxLabel ID="lbComentarioAnterior" runat="server" 
            ClientInstanceName="lbComentarioAnterior" 
           >
        </dxe:ASPxLabel>
        </td></tr>
    <tr><td>

    <dxe:ASPxMemo ID="mmComentarioAnterior" runat="server" Height="148px" Width="748px" 
            ClientInstanceName="mmComentarioAnterior" ReadOnly="True" 
            HorizontalAlign="Justify" >
        <ReadOnlyStyle ForeColor="#666666">
        </ReadOnlyStyle>
        </dxe:ASPxMemo>

    </td></tr>
    </table>

    <table>
    <tr><td>
        <dxe:ASPxLabel ID="lbComentarioAtual" runat="server" 
            ClientInstanceName="lbComentarioAtual" 
           >
        </dxe:ASPxLabel> 
        &nbsp;&nbsp; <a href="#" onclick="abreAnexos()" title="Clique para anexar documentos ou consultar documentos anexados a esta medição.">Anexar/consultar documentos</a>
        </td></tr>
    <tr><td>
        <dxe:ASPxMemo ID="mmComentarioAtual" runat="server" 
            ClientInstanceName="mmComentarioAtual" Height="148px" Width="748px" 
            HorizontalAlign="Justify" >
            <ReadOnlyStyle ForeColor="#666666">
            </ReadOnlyStyle>
        </dxe:ASPxMemo>
        
    </td></tr>
    </table>

    <table style="width:100%">
    <tr>
     <td>

            <dxcp:ASPxCallbackPanel ID="pnBotoes"  ClientInstanceName="pnBotoes" runat="server" 
                  Width="100%">
             <PanelCollection>
              <dxp:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">

              </dxp:PanelContent>
             </PanelCollection>
            </dxcp:ASPxCallbackPanel>


     </td>
     <td style="width:120px;" align="right">
         <dxe:ASPxButton ID="btCancelar" runat="server" Text="Cancelar" 
             ClientInstanceName="btCancelar" HorizontalAlign="Justify" 
             AutoPostBack="False" Width="100px" >
             <ClientSideEvents Click="function(s, e) {
	pcMudaStatus.Hide();
}" />
             <Image Url="~/imagens/botoes/close2.png">
             </Image>
             <Paddings Padding="0px" />
         </dxe:ASPxButton>
         

             
         
         
     </td>
    </tr>
    </table>

    </dxp:PanelContent>
</PanelCollection>
</dxcp:ASPxCallbackPanel>

            </dxpc:PopupControlContentControl>
</ContentCollection>
    </dxpc:aspxpopupcontrol>


    <dxcb:aspxcallback ID="callBackSalvar" runat="server" 
        ClientInstanceName="callBackSalvar" 
    oncallback="callBackSalvar_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
	mostraDivSalvoPublicado(s.cp_Msg);
}" />
    </dxcb:aspxcallback>

     <dxpc:aspxpopupcontrol ID="pcNovasMedicoes" runat="server" Height="512px" 
        Width="800px" AllowDragging="True" ClientInstanceName="pcNovasMedicoes" 
         HeaderText="Incluir novas medições" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
     <ContentCollection>
     <dxpc:PopupControlContentControl>

      <table cellpadding="0" cellspacing="0" class="style14" >
      <tr>
         <td>
              <table>
                  <tr>
                      <td>
                          <dxe:ASPxLabel ID="lbMes" runat="server" ClientInstanceName="lbMes" 
                               Text="Mês:">
                          </dxe:ASPxLabel>
                      </td>
                      <td>
                          <dxe:ASPxLabel ID="lbMes0" runat="server" ClientInstanceName="lbMes" 
                               Text="Ano:">
                          </dxe:ASPxLabel>
                      </td>
                  </tr>
                  <tr>
                      <td style="padding-right: 10px">
                          <dxe:ASPxSpinEdit ID="tbMes" runat="server" ClientInstanceName="tbMes" 
                              DisplayFormatString="{0:D2}"  
                              NumberType="Integer" Width="60px">
                              <SpinButtons ShowIncrementButtons="False">
                              </SpinButtons>
                          </dxe:ASPxSpinEdit>
                      </td>
                      <td>
                          <dxe:ASPxSpinEdit ID="tbAno" runat="server" ClientInstanceName="tbAno" 
                              DisplayFormatString="{0:D4}"  
                              MaxValue="9999" NumberType="Integer" Width="70px">
                              <SpinButtons ShowIncrementButtons="False">
                              </SpinButtons>
                          </dxe:ASPxSpinEdit>
                      </td>
                  </tr>
              </table>
         </td>
      </tr>
          <tr>
              <td class="style15">
              </td>
          </tr>
          <tr>
              <td>
                  <dxwgv:ASPxGridView ID="gvContratos" runat="server" AutoGenerateColumns="False" 
                      ClientInstanceName="gvContratos"  
                      KeyFieldName="CodigoContrato;CodigoProjeto" OnCustomCallback="gvContratos_CustomCallback" 
                      Width="100%">
                      <ClientSideEvents EndCallback="function(s, e) {
       if(s.cp_Status == &quot;1&quot;)
       {
                 if(s.cp_Msg != '')
                 {
                         window.top.mostraMensagem(s.cp_Msg, 'Atencao', true, false, null);
                }
        }
       else if(s.cp_Status == &quot;0&quot;)
       {
                 if(s.cp_Msg != '')
                 { 
                          window.top.mostraMensagem(s.cp_Msg, 'erro', true, false, null);
                 }
        }
}" SelectionChanged="function(s, e) {
	e.processOnServer = false;
    gvContratos.PerformCallback('Verificar;'+e.visibleIndex);
}" />
                      <Columns>
                          <dxwgv:GridViewDataTextColumn Caption="CodigoContrato" 
                              FieldName="CodigoContrato" ShowInCustomizationForm="True" Visible="False" 
                              VisibleIndex="2">
                          </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn Caption="CodigoProjeto" FieldName="CodigoProjeto" 
                              ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                          </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn Caption="Nº Contrato" FieldName="NumeroContrato" 
                              ShowInCustomizationForm="True" VisibleIndex="4" Width="115px">
                          </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn Caption="Projeto / Obra" FieldName="NomeProjeto" 
                              ShowInCustomizationForm="True" VisibleIndex="6">
                          </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn Caption="Fornecedor" FieldName="Fornecedor" 
                              ShowInCustomizationForm="True" VisibleIndex="5" Width="200px">
                          </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewCommandColumn ShowInCustomizationForm="True" 
                              ShowSelectCheckbox="True" VisibleIndex="0" Width="30px">
                          </dxwgv:GridViewCommandColumn>
                      </Columns>
                      <SettingsPager Mode="ShowAllRecords">
                      </SettingsPager>
                      <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="350" />
                  </dxwgv:ASPxGridView>
              </td>
          </tr>
          <tr>
              <td class="style15">
              </td>
          </tr>
          <tr>
              <td align="right">
                  <table>
                      <tbody>
                          <tr>
                              <td>
                                  <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" 
                                      ClientInstanceName="btnSalvar"  
                                      Text="Salvar" Width="100px">
                                      <clientsideevents click="function(s, e) {
	e.processOnServer = false;
    gvContratos.PerformCallback('S');
}" />
                                      <paddings padding="0px" />
                                  </dxe:ASPxButton>
                              </td>
                              <td style="WIDTH: 10px">
                              </td>
                              <td>
                                  <dxe:ASPxButton ID="btnFechar0" runat="server" AutoPostBack="False" 
                                      ClientInstanceName="btnFechar"  
                                      Text="Fechar" Width="100px">
                                      <clientsideevents click="function(s, e) {
	e.processOnServer = false;
    pcNovasMedicoes.Hide();
}" />
                                      <paddings padding="0px" paddingbottom="0px" paddingleft="0px" 
                                          paddingright="0px" paddingtop="0px" />
                                  </dxe:ASPxButton>
                              </td>
                          </tr>
                      </tbody>
                  </table>
              </td>
          </tr>
      </table>
      <br />

    </dxpc:PopupControlContentControl>
    </ContentCollection>
    </dxpc:aspxpopupcontrol>


     </asp:Content>
