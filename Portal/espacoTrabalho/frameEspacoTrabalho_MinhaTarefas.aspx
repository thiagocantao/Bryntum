<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="frameEspacoTrabalho_MinhaTarefas.aspx.cs"
    Inherits="espacoTrabalho_FrameEspacoTrabalho_MinhaTarefas" Title="Minhas Tarefas" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
        width: 100%">
        <tr style="height:26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:aspxlabel id="lblTituloTela" runat="server" font-bold="True"
                    text="Minhas Tarefas"></dxe:aspxlabel>
            </td>
        </tr>
    </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 10px; height: 10px">
                </td>
                <td>
                    </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                <!-- PANELCALLBACK: pnCallback -->
                    <dxcp:aspxcallbackpanel id="pnCallback" runat="server" width="100%" clientinstancename="pnCallback" oncallback="pnCallback_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><!-- ASPxGRIDVIEW: gvDados --><dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoTarefa" AutoGenerateColumns="False" Width="100%"  ID="gvDados" OnHtmlRowPrepared="gvDados_HtmlRowPrepared" OnCustomButtonInitialize="gvToDoList_CustomButtonInitialize" OnHtmlRowCreated="gvDados_HtmlRowCreated" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared">
<ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}" CustomButtonClick="function(s, e) {
    //gvDados.SetFocusedRowIndex(e.visibleIndex);

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
	
}"></ClientSideEvents>
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="75px" Caption="A&#231;&#227;o" VisibleIndex="0"><CustomButtons>
<dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Visibility="Invisible" Text="Excluir">
<Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
<Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
<Image Url="~/imagens/botoes/pFormulario.png"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
</CustomButtons>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn Name="M" Width="30px" VisibleIndex="1">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoTarefa" Name="Tarefa" Caption="Tarefa" VisibleIndex="2">
<Settings AllowAutoFilter="True"></Settings>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoStatusTarefa" Name="Status" Width="95px" Caption="Status" VisibleIndex="3">
<Settings AllowAutoFilter="True"></Settings>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Estagio" Name="Estagio" Width="65px" Caption="Est&#225;gio" VisibleIndex="4">
<Settings AllowAutoFilter="True"></Settings>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataDateColumn FieldName="InicioPrevisto" Name="InicioPrevisto" Width="75px" Caption="In&#237;cio Previsto" VisibleIndex="5">
<PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" UseMaskBehavior="True"></PropertiesDateEdit>

<Settings AllowAutoFilter="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxwgv:GridViewDataDateColumn>
<dxwgv:GridViewDataDateColumn FieldName="TerminoPrevisto" Name="TerminoPrevisto" Width="75px" Caption="T&#233;rmino Previsto" VisibleIndex="6">
<PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" UseMaskBehavior="True"></PropertiesDateEdit>

<Settings AllowAutoFilter="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxwgv:GridViewDataDateColumn>
<dxwgv:GridViewDataDateColumn FieldName="InicioReal" Name="InicioReal" Width="75px" Caption="In&#237;cio Real" VisibleIndex="7">
<PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" UseMaskBehavior="True"></PropertiesDateEdit>

<Settings AllowAutoFilter="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxwgv:GridViewDataDateColumn>
<dxwgv:GridViewDataDateColumn FieldName="TerminoReal" Name="TerminoReal" Width="75px" Caption="T&#233;rmino Real" VisibleIndex="8">
<PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" UseMaskBehavior="True"></PropertiesDateEdit>

<Settings AllowAutoFilter="False"></Settings>

<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
</dxwgv:GridViewDataDateColumn>
<dxwgv:GridViewDataTextColumn FieldName="DescricaoOrigem" Name="Origem" Caption="Origem" VisibleIndex="9">
<Settings AllowAutoFilter="True"></Settings>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavel" Name="Responsavel" Caption="Respons&#225;vel" Visible="False" VisibleIndex="10">
<Settings AllowAutoFilter="False"></Settings>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoUsuarioResponsavel" Name="CodigoUsuarioResponsavel" Caption="CodigoUsuarioResponsavel" Visible="False" VisibleIndex="9"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="PercentualConcluido" Name="PercentualConcluido" Caption="PercentualConcluido" Visible="False" VisibleIndex="9"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Anotacoes" Name="Anotacoes" Caption="Anotacoes" Visible="False" VisibleIndex="9"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CodigoStatusTarefa" 
        Name="CodigoStatusTarefa" Caption="CodigoStatusTarefa" Visible="False" 
        VisibleIndex="9"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="EsforcoPrevisto" Name="EsforcoPrevisto" Caption="EsforcoPrevisto" Visible="False" VisibleIndex="9"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="EsforcoReal" Name="EsforcoReal" Caption="EsforcoReal" Visible="False" VisibleIndex="10"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CustoPrevisto" Name="CustoPrevisto" Caption="CustoPrevisto" Visible="False" VisibleIndex="9"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CustoReal" Name="CustoReal" Caption="CustoReal" Visible="False" VisibleIndex="9"></dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords"></SettingsPager>

<Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible" ShowFooter="True"></Settings>

<SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar" ></SettingsText>
    <Templates>
        <FooterRow>
                                            <table>
                                                <tr>
                                                    <td 
                                                        style="border: 1px solid #808080; background-color: #008000; width:10px">
                                                    </td>
                                                    <td  style="padding-left: 3px; padding-right: 10px">
                                                        <dxe:ASPxLabel ID="lblDescricaoConcluido" runat="server" 
                                                            ClientInstanceName="lblDescricaoConcluido"  
                                                            Text="Tarefa concluída">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td 
                                                        style="border: 1px solid #808080; background-color: #FF0000; width:10px">
                                                    </td>
                                                    <td  style="padding-left: 3px; padding-right: 10px">
                                                        <dxe:ASPxLabel ID="lblDescricaoAtrasada" runat="server" 
                                                            ClientInstanceName="lblDescricaoAtrasada"  
                                                            Text="Tarefa atrasada">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td class="style9">
                                                        <img style="border: 0px" src="../imagens/anotacao.png" />
                                                    </td>
                                                    <td class="style10" style="padding-left: 3px; padding-right: 10px">
                                                        <dxe:ASPxLabel ID="lblDescricaoAnotacoes" runat="server" 
                                                            ClientInstanceName="lblDescricaoAnotacoes"  
                                                            Text="Possui anotações">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterRow>
    </Templates>
</dxwgv:ASPxGridView>
 <!-- PANEL CONTROL : pcDados --><dxpc:ASPxPopupControl runat="server" 
        AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" 
        HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" PopupVerticalOffset="50" 
        ShowCloseButton="False" Width="830px" Height="145px" 
        ID="pcDados">
<ClientSideEvents Shown="function(s, e) {
	desabilitaHabilitaComponentes();
}"></ClientSideEvents>

<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>

<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="WIDTH: 450px"><dxe:ASPxLabel runat="server" Text="Descri&#231;&#227;o Tarefa:" ClientInstanceName="lblDescricaoTarefa"  ID="lblDescricaoTarefa"></dxe:ASPxLabel>

 </td><td style="WIDTH: 10px"></td><td><dxe:ASPxLabel runat="server" Text="Usu&#225;rio Respons&#225;vel:" ClientInstanceName="lblCodigoUsuarioResponsavel"  ID="lblCodigoUsuarioResponsavel"></dxe:ASPxLabel>

 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtDescricaoTarefaBanco" ClientEnabled="False"  ID="txtDescricaoTarefaBanco">
<DisabledStyle BackColor="#EBEBEB"  
            ForeColor="Black">
<Border BorderColor="Silver"></Border>
</DisabledStyle>
</dxe:ASPxTextBox>

 </td><td></td><td><dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtCodigoUsuarioResponsavelBanco" ClientEnabled="False"  ID="txtCodigoUsuarioResponsavelBanco">
<DisabledStyle BackColor="#EBEBEB"  
            ForeColor="Black">
<Border BorderColor="Silver"></Border>
</DisabledStyle>
</dxe:ASPxTextBox>

 </td></tr></TBODY></table></td></tr></TBODY></table></td></tr><tr><td style="HEIGHT: 5px"></td></tr><tr><td><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="WIDTH: 90px"><dxe:ASPxLabel runat="server" Text="In&#237;cio Prev.:" ClientInstanceName="lblInicioPrevisto"  ID="lblInicioPrevisto"></dxe:ASPxLabel>

 </td><td style="WIDTH: 10px; HEIGHT: 17px"></td><td style="WIDTH: 90px"><dxe:ASPxLabel runat="server" Text="T&#233;rmino Prev.:" ClientInstanceName="lblTerminoPrevisto"  ID="lblTerminoPrevisto"></dxe:ASPxLabel>

 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 120px"><dxe:ASPxLabel runat="server" Text="Despesa Prev.(R$):" ClientInstanceName="lblCustoPrevisto"  ID="lblCustoPrevisto"></dxe:ASPxLabel>

 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 100px"><dxe:ASPxLabel runat="server" Text="Esfor&#231;o Prev.(h):" ClientInstanceName="lblEsforcoPrevisto"  ID="lblEsforcoPrevisto"></dxe:ASPxLabel>

 </td><td style="WIDTH: 10px"></td><td><dxe:ASPxLabel runat="server" Text="Est&#225;gio:" ClientInstanceName="lblEstagio"  ID="lblEstagio"></dxe:ASPxLabel>

 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 115px"><dxe:ASPxLabel runat="server" Text="Despesa Real (R$):" ClientInstanceName="lblCustoReal"  ID="lblCustoReal"></dxe:ASPxLabel>

 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtInicioPrevistoBanco" ClientEnabled="False"  ID="txtInicioPrevistoBanco">
<DisabledStyle BackColor="#EBEBEB"  
            ForeColor="Black">
<Border BorderColor="Silver"></Border>
</DisabledStyle>
</dxe:ASPxTextBox>

 </td><td></td><td><dxe:ASPxTextBox runat="server" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="txtTerminoPrevistoBanco" ClientEnabled="False"  ID="txtTerminoPrevistoBanco">
<DisabledStyle BackColor="#EBEBEB"  
            ForeColor="Black">
<Border BorderColor="Silver"></Border>
</DisabledStyle>
</dxe:ASPxTextBox>

 </td><td style="WIDTH: 10px"></td><td style="width: 120px"><dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right" ClientInstanceName="txtCustoPrevistoBanco" ClientEnabled="False"  ID="txtCustoPrevistoBanco">
<DisabledStyle BackColor="#EBEBEB"  
            ForeColor="Black">
<Border BorderColor="Silver"></Border>
</DisabledStyle>
</dxe:ASPxTextBox>

 </td><td></td><td><dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right" ClientInstanceName="txtEsforcoPrevistoBanco" ClientEnabled="False"  ID="txtEsforcoPrevistoBanco">
<DisabledStyle BackColor="#EBEBEB"  
            ForeColor="Black">
<Border BorderColor="Silver"></Border>
</DisabledStyle>
</dxe:ASPxTextBox>

 </td><td></td><td><dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtEstagioBanco" ClientEnabled="False"  ID="txtEstagioBanco">
<DisabledStyle BackColor="#EBEBEB"  
            ForeColor="Black">
<Border BorderColor="Silver"></Border>
</DisabledStyle>
</dxe:ASPxTextBox>

 </td><td></td><td style="width: 115px"><dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right" ClientInstanceName="txtCustoRealBanco" ClientEnabled="False"  ID="txtCustoRealBanco">
<DisabledStyle BackColor="#EBEBEB"  
            ForeColor="Black">
<Border BorderColor="Silver"></Border>
</DisabledStyle>
</dxe:ASPxTextBox>

 </td></tr></TBODY></table></td></tr><tr><td style="HEIGHT: 5px"></td></tr><tr><td><dxe:ASPxLabel runat="server" Text="Origem Tarefa:" ClientInstanceName="lblOrigemTarefa"  ID="lblOrigemTarefa"></dxe:ASPxLabel>

 </td></tr><tr><td><dxe:ASPxMemo runat="server" Height="32px" Width="100%" ClientInstanceName="txtOrigemTarefaBanco" ClientEnabled="False"  ID="txtOrigemTarefaBanco">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
<Border BorderColor="Silver"></Border>
</DisabledStyle>
</dxe:ASPxMemo>

</td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td><dxp:ASPxPanel runat="server" ClientInstanceName="paEditar" Width="100%" ID="paEditar"><PanelCollection>
<dxp:PanelContent runat="server"><table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td><table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td style="WIDTH: 120px"><dxe:ASPxLabel runat="server" Text="In&#237;cio Real:" ClientInstanceName="lblInicioReal"  ID="lblInicioReal"></dxe:ASPxLabel>


 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 120px"><dxe:ASPxLabel runat="server" Text="T&#233;rmino Real:" ClientInstanceName="lblTerminoReal"  ID="lblTerminoReal"></dxe:ASPxLabel>


 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 100px"><dxe:ASPxLabel runat="server" Text="Esfor&#231;o Real (h):" ClientInstanceName="lblEsforcoReal"  ID="lblEsforcoReal"></dxe:ASPxLabel>


 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 140px"><dxe:ASPxLabel runat="server" Text="Status:" ClientInstanceName="lblStatusTarefa"  ID="lblStatusTarefa"></dxe:ASPxLabel>


 </td></tr><tr><td><dxe:ASPxDateEdit runat="server" Width="100%" ClientInstanceName="ddlInicioReal"  ID="ddlInicioReal">
<CalendarProperties>

<DayHeaderStyle ></DayHeaderStyle>

<WeekNumberStyle ></WeekNumberStyle>

<DayStyle ></DayStyle>

<DaySelectedStyle ></DaySelectedStyle>

<DayOtherMonthStyle ></DayOtherMonthStyle>

<DayWeekendStyle ></DayWeekendStyle>

<DayOutOfRangeStyle ></DayOutOfRangeStyle>

<TodayStyle ></TodayStyle>

<ButtonStyle ></ButtonStyle>

<Style ></Style>
</CalendarProperties>

<ValidationSettings ValidationGroup="MKE"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxDateEdit>


 </td><td></td><td id="Td1"><dxe:ASPxDateEdit runat="server" Width="100%" ClientInstanceName="ddlTerminoReal"  ID="ddlTerminoReal">
<CalendarProperties>

<DayHeaderStyle ></DayHeaderStyle>

<WeekNumberStyle ></WeekNumberStyle>

<DayStyle ></DayStyle>

<DaySelectedStyle ></DaySelectedStyle>

<DayOtherMonthStyle ></DayOtherMonthStyle>

<DayWeekendStyle ></DayWeekendStyle>

<DayOutOfRangeStyle ></DayOutOfRangeStyle>

<TodayStyle ></TodayStyle>

<ButtonStyle ></ButtonStyle>

<Style ></Style>
</CalendarProperties>

<ValidationSettings ValidationGroup="MKE"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxDateEdit>


 </td><td></td><td><dxe:ASPxTextBox runat="server" Width="100%" HorizontalAlign="Right" ClientInstanceName="txtEsforcoReal"  ID="txtEsforcoReal">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>


 </td><td></td><td><dxe:ASPxComboBox runat="server" ValueType="System.Int32" Width="100%" ClientInstanceName="ddlStatusTarefa"  ID="ddlStatusTarefa">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>


 </td></tr></TBODY></table></td></tr><tr><td style="HEIGHT: 5px"></td></tr><tr><td><table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="HEIGHT: 16px"><dxe:ASPxLabel runat="server" Text="Anota&#231;&#245;es:" ClientInstanceName="lblAnotacoes"  ID="lblAnotacoes"></dxe:ASPxLabel>


 </td></tr><tr><td><dxe:ASPxMemo runat="server" Rows="10" Width="100%" ClientInstanceName="mmAnotacoesBanco"  ID="mmAnotacoesBanco">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxMemo>


 </td></tr></TBODY></table></td></tr></TBODY></table></dxp:PanelContent>
</PanelCollection>
</dxp:ASPxPanel>

 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align=right><dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>

 <table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE" Width="90px"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	
	if(verificarDadosPreenchidos())
	{
		if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
	}
	else
		return false;
    
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

 </td><td style="WIDTH: 10px"></td><td><dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

 </td></tr></TBODY></table></td></tr></TBODY></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) {
	onEnd_pnCallback();
}"></ClientSideEvents>
</dxcp:aspxcallbackpanel>
                </td>
                <td>
                </td>
            </tr>
        </table>

</asp:Content>