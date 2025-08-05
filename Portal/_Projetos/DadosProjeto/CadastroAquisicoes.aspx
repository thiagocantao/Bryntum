<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CadastroAquisicoes.aspx.cs"
    Inherits="CadastroAquisicoes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 100px;
        }
        .style9
        {
            width: 178px;
        }
        .style10
        {
            width: 211px;
        }
    </style>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td align="right">
                    <table cellspacing="0" cellpadding="0" border="0" >
                        <tbody>
                            <tr>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ClientInstanceName="ddlExporta"
                                        ID="ddlExporta">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set('tipoArquivo', s.GetValue());
}"></ClientSideEvents>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td style="padding-right: 3px; padding-left: 3px">
                                    <dxcp:ASPxCallbackPanel runat="server"  
                                        ClientInstanceName="pnImage" Width="23px" Height="22px" ID="pnImage" OnCallback="pnImage_Callback">
                                        <PanelCollection>
                                            <dxp:PanelContent ID="PanelContent1" runat="server">
                                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/menuExportacao/iconoExcel.png"
                                                    Width="20px" Height="20px" ClientInstanceName="imgExportacao" ID="imgExportacao">
                                                </dxe:ASPxImage>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxcp:ASPxCallbackPanel>
                                </td>
                                <td style="padding-right: 3px; padding-left: 3px; width: 75px">
                                    <dxe:ASPxButton runat="server" Text="Exportar" 
                                        ID="Aspxbutton1" OnClick="btnExcel_Click">
                                        <ClientSideEvents Click="function(s, e) 
{
	if(gvDados.pageRowCount == 0)
	{
		window.top.mostraMensagem(&quot;N&#227;o h&#225; Nenhuma informa&#231;&#227;o para exportar.&quot;, 'erro', true, false, null);
		e.processOnServer = false;	
	}
}"></ClientSideEvents>
                                        <Paddings Padding="0px"></Paddings>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 5px; padding-right: 10px; padding-left: 10px;">
                    <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                        Width="100%" OnCallback="pnCallback_Callback">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoAquisicao"
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
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
		OnGridFocusedRowChanged(gvDados, true);
		btnSalvar.SetVisible(false);
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Consultar&quot;);
		TipoOperacao = &quot;Consultar&quot;;
		pcDados.Show();
     }	
}
"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="90px" VisibleIndex="0" FixedStyle="Left">
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
                                                <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Novo"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
                                            </HeaderTemplate>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn Name="Aquisicao" Caption="Item" VisibleIndex="2" FieldName="Item">
                                            <Settings AutoFilterCondition="Contains" AllowAutoFilter="True" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataDateColumn Caption="Prazo Máximo Contratação" ShowInCustomizationForm="True"
                                            VisibleIndex="3" Width="110px" FieldName="DataPrevista">
                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
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
                                                        <HoverStyle >
                                                        </HoverStyle>
                                                    </ButtonStyle>
                                                    <FastNavMonthAreaStyle >
                                                    </FastNavMonthAreaStyle>
                                                    <FastNavMonthStyle >
                                                        <SelectedStyle >
                                                        </SelectedStyle>
                                                    </FastNavMonthStyle>
                                                    <FastNavFooterStyle >
                                                    </FastNavFooterStyle>
                                                    <Style >
                                                        
                                                    </Style>
                                                </CalendarProperties>
                                            </PropertiesDateEdit>
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Equals" />
                                            <HeaderStyle Wrap="True" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Valor Previsto (R$)" ShowInCustomizationForm="True"
                                            VisibleIndex="4" Width="125px" FieldName="ValorPrevisto">
                                            <PropertiesTextEdit DisplayFormatString="n2">
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="False" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="% Contratado" ShowInCustomizationForm="True"
                                            VisibleIndex="5" Width="98px" FieldName="PercentualContratado">
                                            <PropertiesTextEdit DisplayFormatString="{0:p2}">
                                                <Style >
                                                    
                                                </Style>
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="False" />
                                            <HeaderStyle HorizontalAlign="Right" Wrap="True" />
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="CodigoResponsavel" ShowInCustomizationForm="True"
                                            VisibleIndex="6" Visible="False">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Responsável" ShowInCustomizationForm="True"
                                            VisibleIndex="7" FieldName="NomeUsuario" Width="190px">
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Codigo" ShowInCustomizationForm="True" VisibleIndex="1"
                                            Width="60px" FieldName="CodigoAquisicao" Visible="False">
                                            <Settings AllowAutoFilter="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Tipo de Item" FieldName="GrupoAquisicao" Name="GrupoAquisicao"
                                            ShowInCustomizationForm="True" VisibleIndex="8" Width="190px">
                                            <PropertiesTextEdit>
                                                <Style >
                                                    
                                                </Style>
                                            </PropertiesTextEdit>
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" ShowInCustomizationForm="True"
                                            VisibleIndex="9" ExportWidth="75" Width="60px">
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" FilterMode="DisplayText" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Código Conta" FieldName="CodigoConta" ShowInCustomizationForm="True"
                                            Visible="False" VisibleIndex="11">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Conta Associada" FieldName="DescricaoConta"
                                            ShowInCustomizationForm="True" VisibleIndex="10" Width="190px">
                                            <Settings AllowAutoFilter="False" />
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowSort="False" AllowFocusedRow="True" AutoExpandAllGroups="True">
                                    </SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                    </SettingsPager>
                                    <Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" ShowGroupPanel="True"
                                        HorizontalScrollBarMode="Visible"></Settings>
                                    <SettingsText></SettingsText>
                                </dxwgv:ASPxGridView>
                                
                                
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) 
{
    if (window.onEnd_pnCallback)
		onEnd_pnCallback();
		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Aquisição/Contratação incluída com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Aquisição/Contratação alterada com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Aquisição/Contratação excluída com sucesso!&quot;);
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
    </div>

                                <dxpc:ASPxPopupControl runat="server" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        CloseAction="None" AllowDragging="True" ClientInstanceName="pcDados" 
        HeaderText="Detalhes" ShowCloseButton="False" Width="780px" 
         ID="pcDados">
<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>

<LinkStyle>
<Font></Font>
</LinkStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Item:"  
                                                                        ID="ASPxLabel1"></dxe:ASPxLabel>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="250" 
                                                                        ClientInstanceName="txtAquisicao"  
                                                                        ID="txtAquisicao">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="Conta:" 
                                                            ID="ASPxLabel14"></dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                                                            Width="100%" ClientInstanceName="ddlConta"  
                                                            ID="ddlConta">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px">
                                                        <dxe:ASPxLabel runat="server" Text="Tipo de Item:" 
                                                            ID="ASPxLabel11"></dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px">
                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="250" 
                                                            ClientInstanceName="txtGrupoAquisicao"  
                                                            ID="txtGrupoAquisicao">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                            <tr>
                                                                <td class="style2">
                                                                    <dxe:ASPxLabel runat="server" Text="Contratado?:" 
                                                                        ID="ASPxLabel13"></dxe:ASPxLabel>

                                                                </td>
                                                                <td class="style9">
                                                                    <dxe:ASPxLabel runat="server" 
                                                                        Text="Prazo M&#225;ximo de Contrata&#231;&#227;o:" 
                                                                        ID="ASPxLabel2"></dxe:ASPxLabel>

                                                                </td>
                                                                <td class="style10">
                                                                    <dxe:ASPxLabel runat="server" Text="Valor Previsto:" 
                                                                        ID="ASPxLabel3"></dxe:ASPxLabel>

                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Percentual Contratado(%):" 
                                                                         ID="ASPxLabel5"></dxe:ASPxLabel>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style2" >
                                                                    <dxe:ASPxComboBox runat="server" SelectedIndex="0" ValueType="System.Int32" 
                                                                        Width="100%" ClientInstanceName="ddlContratado" 
                                                                        ID="ddlContratado">
<ClientSideEvents SelectedIndexChanged="function(s, e) {
	var selecionado = s.GetValue();
    if(selecionado == 1)//sim
    {
		spnValorRealizado.SetEnabled(true);
    }
    else if(selecionado == 2)//N&#227;o
    {
		spnValorRealizado.SetEnabled(false);
        spnValorRealizado.SetText(&#39;&#39;);
    }
    else if(selecionado == &#39;3&#39;)//Parcialmente
    {    
        spnValorRealizado.SetEnabled(true);
    }
}"></ClientSideEvents>
<Items>
<dxe:ListEditItem Selected="True" Text="Sim" Value="1"></dxe:ListEditItem>
<dxe:ListEditItem Text="N&#227;o" Value="2"></dxe:ListEditItem>
<dxe:ListEditItem Text="Parcial" Value="3"></dxe:ListEditItem>
</Items>

<ButtonStyle ></ButtonStyle>

<DisabledStyle BackColor="#EBEBEB"  ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>

                                                                </td>
                                                                <td class="style9" >
                                                                    <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" 
                                                                        EditFormatString="dd/MM/yyyy" EncodeHtml="False" Width="100%" 
                                                                        DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlDataPrevista" 
                                                                         ID="ddlDataPrevista">
<CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">

<DayHeaderStyle ></DayHeaderStyle>

<WeekNumberStyle ></WeekNumberStyle>

<DayStyle ></DayStyle>

<DaySelectedStyle ></DaySelectedStyle>

<DayOtherMonthStyle ></DayOtherMonthStyle>

<DayWeekendStyle ></DayWeekendStyle>

<DayOutOfRangeStyle ></DayOutOfRangeStyle>

<TodayStyle ></TodayStyle>

<ButtonStyle ></ButtonStyle>

<HeaderStyle ></HeaderStyle>

<FooterStyle ></FooterStyle>

<FastNavStyle ></FastNavStyle>

<FastNavMonthAreaStyle ></FastNavMonthAreaStyle>

<FastNavYearAreaStyle ></FastNavYearAreaStyle>

<FastNavMonthStyle ></FastNavMonthStyle>

<FastNavYearStyle ></FastNavYearStyle>

<FastNavFooterStyle ></FastNavFooterStyle>

<FocusedStyle ></FocusedStyle>

<InvalidStyle ></InvalidStyle>

<Style ></Style>
</CalendarProperties>

<ClientSideEvents ValueChanged="function(s, e) {
	
}"></ClientSideEvents>

<ButtonStyle ></ButtonStyle>

<NullTextStyle ></NullTextStyle>

<ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxDateEdit>

                                                                </td>
                                                                <td class="style10" >
                                                                    <dxe:ASPxSpinEdit runat="server" Number="0" Width="100%" Height="21px" 
                                                                        DisplayFormatString="{0:c2}" ClientInstanceName="spnValorPrevisto" 
                                                                         ID="spnValorPrevisto">
<SpinButtons ShowIncrementButtons="False"></SpinButtons>

<ValidationSettings ErrorDisplayMode="None" ValidateOnLeave="False" ErrorTextPosition="Top"></ValidationSettings>

<ReadOnlyStyle BackColor="#EBEBEB"  ForeColor="Black"></ReadOnlyStyle>

<DisabledStyle BackColor="#EBEBEB"  ForeColor="Black"></DisabledStyle>
</dxe:ASPxSpinEdit>

                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit runat="server" MaxValue="100" DecimalPlaces="2" Number="0" 
                                                                        AllowMouseWheel="False" Width="100%" Height="21px" DisplayFormatString="{0:n2}" 
                                                                        ClientInstanceName="spnValorRealizado"  
                                                                        ID="spnValorRealizado">
<SpinButtons ShowIncrementButtons="False"></SpinButtons>

<DisabledStyle BackColor="#EBEBEB"  ForeColor="Black"></DisabledStyle>
</dxe:ASPxSpinEdit>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px">
                                                        <dxe:ASPxLabel runat="server" Text="Respons&#225;vel:" 
                                                            ID="ASPxLabel6"></dxe:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px">
                                                        <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                                                            Width="100%" ClientInstanceName="ddlResponsavel" 
                                                            ID="ddlResponsavel"><Columns>
<dxe:ListBoxColumn Width="60%" Caption="Nome"></dxe:ListBoxColumn>
<dxe:ListBoxColumn Width="40%" Caption="Email"></dxe:ListBoxColumn>
</Columns>

<ButtonStyle ></ButtonStyle>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" 
                                                                            ClientInstanceName="btnSalvar" Text="Salvar" Width="100px" 
                                                                            ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	//debugger
    e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton runat="server" AutoPostBack="False" 
                                                                            ClientInstanceName="btnFechar" Text="Fechar" Width="90px" 
                                                                            ID="btnFechar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
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
<dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual"
                                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
                                    ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido">
                                    <ContentCollection>
                                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="" align="center">
                                                        </td>
                                                        <td style="width: 70px" align="center" rowspan="3">
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
                                <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                </dxhf:ASPxHiddenField>
                                <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" LeftMargin="50" RightMargin="50"
                                    Landscape="True" ID="ASPxGridViewExporter1" OnRenderBrick="ASPxGridViewExporter1_RenderBrick"
                                    ExportEmptyDetailGrid="True" PreserveGroupRowStates="False" PaperKind="A4">
                                    <Styles>
                                        <Default >
                                        </Default>
                                        <Header  Wrap="True">
                                        </Header>
                                        <Cell  Wrap="True">
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
    </form>
</body>
</html>
