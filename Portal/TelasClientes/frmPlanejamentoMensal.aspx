<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="frmPlanejamentoMensal.aspx.cs" Inherits="administracao_CadastroRamosAtividades" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
.dxgvControl,
.dxgvDisabled
{
	border: 1px Solid #9F9F9F;
	font: 12px Tahoma, Geneva, sans-serif;
	background-color: #F2F2F2;
	color: Black;
	cursor: default;
}
.dxeHLC, .dxeHC, .dxeHFC
{
    display: none;
}
.dxgvTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxgvTable
{
	background-color: White;
	border-width: 0;
	border-collapse: separate!important;
	overflow: hidden;
}
.dxgvHeader
{
	cursor: pointer;
	white-space: nowrap;
	padding: 4px 6px;
	border: 1px Solid #9F9F9F;
	background-color: #DCDCDC;
	overflow: hidden;
	font-weight: normal;
	text-align: left;
}
.dxgvFilterRow
{
	background-color: #E7E7E7;
}
.dxgvCommandColumn
{
	padding: 2px;
}
.dxgvFocusedRow
{
	background-color: #8D8D8D;
	color: White;
}
.dxgvFooter
{
	background-color: #D7D7D7;
	white-space: nowrap;
}
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 130px;
        }
        .auto-style3 {
            width: 216px;
        }
    </style>
</head>
<body style="margin:0">
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>
            </td>
            <td style="height: 10px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dxcp:aspxcallbackpanel id="pnCallback" runat="server" clientinstancename="pnCallback"
                    width="100%" oncallback="pnCallback_Callback"><PanelCollection>
<dxp:PanelContent runat="server"><dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" 
        KeyFieldName="CodigoAcao" AutoGenerateColumns="False" Width="100%" 
         ID="gvDados" 
        OnCustomButtonInitialize="gvDados_CustomButtonInitialize" 
        OnAfterPerformCallback="gvDados_AfterPerformCallback1" 
        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" OnCustomJSProperties="gvDados_CustomJSProperties">
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
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0">
<CustomButtons>
<dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
<Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
<Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
<dxwgv:GridViewCommandColumnCustomButton ID="btnDetalhesCustom" Text="Detalhes">
<Image Url="~/imagens/botoes/pFormulario.PNG"></Image>
</dxwgv:GridViewCommandColumnCustomButton>
</CustomButtons>
<HeaderTemplate>
        <%# getBotaoIncluir()%>
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxcp:GridViewDataTextColumn FieldName="Acao" ShowInCustomizationForm="True" Caption="Ação" VisibleIndex="1">
</dxcp:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="NomeInstrutor" 
        Caption="Nome do Instrutor/Responsável" VisibleIndex="2" Width="200px">
</dxwgv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Município" FieldName="Municipio" ShowInCustomizationForm="True" VisibleIndex="5">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Valor" FieldName="Valor" ShowInCustomizationForm="True" VisibleIndex="6" Width="125px">
        <PropertiesTextEdit DisplayFormatString="c2">
        </PropertiesTextEdit>
        <Settings AllowAutoFilter="False" />
        <HeaderStyle HorizontalAlign="Right" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataDateColumn Caption="Data Início" FieldName="Inicio" ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
        </PropertiesDateEdit>
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
    </dxtv:GridViewDataDateColumn>
    <dxtv:GridViewDataDateColumn Caption="Data Término" FieldName="Termino" ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
        </PropertiesDateEdit>
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
    </dxtv:GridViewDataDateColumn>
    <dxtv:GridViewDataTextColumn FieldName="CodigoMunicipio" ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="CodigoConsultor" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="CodigoItemAcao" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataDateColumn Caption="Término Real" FieldName="DataTerminoReal" ShowInCustomizationForm="True" VisibleIndex="9" Width="100px">
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
    </dxtv:GridViewDataDateColumn>
    <dxtv:GridViewDataDateColumn Caption="Início Real" FieldName="DataInicioReal" ShowInCustomizationForm="True" VisibleIndex="8" Width="100px">
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
    </dxtv:GridViewDataDateColumn>
    <dxtv:GridViewDataCheckColumn Caption="Ação executada?" FieldName="AcaoExecutada" ShowInCustomizationForm="True" VisibleIndex="7" Width="110px" Visible="False">
        <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
        </PropertiesCheckEdit>
        <HeaderStyle HorizontalAlign="Center" />
    </dxtv:GridViewDataCheckColumn>
    <dxtv:GridViewDataTextColumn FieldName="AcaoPlanejada" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
    </dxtv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" ShowFooter="True"></Settings>

<SettingsText ></SettingsText>
     <TotalSummary>
         <dxtv:ASPxSummaryItem DisplayFormat="{0:c2}" FieldName="Valor" ShowInColumn="Valor" SummaryType="Sum" ValueDisplayFormat="{0:c2}" />
     </TotalSummary>
</dxwgv:ASPxGridView>
    <dxtv:ASPxPopupMenu ID="popupMenu" runat="server" ClientInstanceName="popupMenu" PopupAction="None" PopupElementID="imgIncluirPrestacaoContas" PopupVerticalAlign="Below">
        <ClientSideEvents ItemClick="popupMenu_ItemClick" />
        <Items>
            <dxtv:MenuItem Name="itemNovaAcaoNaoPlanejada" Text="Nova ação não planejada">
            </dxtv:MenuItem>
            <dxtv:MenuItem Name="itemRegistrarAcoesPlanejadas" Text="Registrar ações planejadas">
            </dxtv:MenuItem>
        </Items>
    </dxtv:ASPxPopupMenu>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="650px"  ID="pcDados">
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
                    Text="Ação:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxComboBox ID="ddlAcao" runat="server" ClientInstanceName="ddlAcao" DropDownRows="5" EnableCallbackMode="True"  OnCustomJSProperties="ddlAcao_CustomJSProperties" TextFormatString="{0}" Width="100%">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
var valor = ddlAcao.cpHiddenColumnValues[ddlAcao.GetSelectedIndex()];
txtValor.SetValue(valor);
}" />
                    <Columns>
                        <dxtv:ListBoxColumn Caption="Ação" FieldName="Descricao" Width="500px" />
                        <dxtv:ListBoxColumn FieldName="ValorUnitario" Visible="False" />
                    </Columns>
                    <ReadOnlyStyle BackColor="#EBEBEB">
                    </ReadOnlyStyle>
                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                    </DisabledStyle>
                </dxtv:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" class="auto-style1">
                    <tr>
                        <td class="auto-style3">
                            <dxtv:ASPxLabel ID="ASPxLabel2" runat="server"  Text="Nome do Instrutor/Responsável:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td>
                            <dxtv:ASPxLabel ID="ASPxLabel3" runat="server"  Text="Município:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3" style="padding-right: 10px">
                            <dxtv:ASPxComboBox ID="ddlInstrutor" runat="server" ClientInstanceName="ddlInstrutor" DropDownRows="5" EnableCallbackMode="True"  Width="100%">
                                <ReadOnlyStyle BackColor="#EBEBEB">
                                </ReadOnlyStyle>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxComboBox>
                        </td>
                        <td>
                            <dxtv:ASPxComboBox ID="ddlMunicipio" runat="server" ClientInstanceName="ddlMunicipio" DropDownRows="5" EnableCallbackMode="True"  Width="100%">
                                <ReadOnlyStyle BackColor="#EBEBEB">
                                </ReadOnlyStyle>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 15px">&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 15px">
                <table cellspacing="0" class="auto-style1">
                    <tr>
                        <td class="auto-style2">
                            <dxtv:ASPxLabel ID="ASPxLabel4" runat="server"  Text="Data de Início:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td class="auto-style2">
                            <dxtv:ASPxLabel ID="ASPxLabel5" runat="server"  Text="Data de Término:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td>
                            <dxtv:ASPxLabel ID="ASPxLabel6" runat="server"  Text="Valor:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2" style="padding-right: 10px">
                            <dxtv:ASPxDateEdit ID="ddlInicio" runat="server" ClientInstanceName="ddlInicio" DisplayFormatString="{0:dd/MM/yyyy}"  Width="100%" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="OutsideRight">
                                <ClientSideEvents ValueChanged="function(s, e) {
	if(gvDados.cpExibirColunas){
		ddlInicioReal.SetValue(s.GetValue());
	}
}" />
                                <ReadOnlyStyle BackColor="#EBEBEB">
                                </ReadOnlyStyle>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxDateEdit>
                        </td>
                        <td class="auto-style2" style="padding-right: 10px">
                            <dxtv:ASPxDateEdit ID="ddlTermino" runat="server" ClientInstanceName="ddlTermino" DisplayFormatString="{0:dd/MM/yyyy}"  Width="100%" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="OutsideRight">
                                <ClientSideEvents ValueChanged="function(s, e) {
	if(gvDados.cpExibirColunas){
		ddlTerminoReal.SetValue(s.GetValue());
	}
}" />
                                <ReadOnlyStyle BackColor="#EBEBEB">
                                </ReadOnlyStyle>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxDateEdit>
                        </td>
                        <td>
                            <dxtv:ASPxSpinEdit ID="txtValor" runat="server" ClientInstanceName="txtValor" DecimalPlaces="2" DisplayFormatString="{0:c2}"  Number="0" Width="100%">
                                <SpinButtons ClientVisible="False">
                                </SpinButtons>
                                <ReadOnlyStyle BackColor="#EBEBEB">
                                </ReadOnlyStyle>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxSpinEdit>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 15px">&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 15px">
                <table cellspacing="0" class="auto-style1" id="tblDatasRealizacao" runat="server">
                    <tr>
                        <td class="auto-style2">
                            <dxtv:ASPxCheckBox ID="cbAcaoExecutada" runat="server" CheckState="Checked" ClientInstanceName="cbAcaoExecutada" Text="Ação executada?" Checked="True" ClientVisible="False">
                                <ClientSideEvents CheckedChanged="function(s, e) {
	var checked = s.GetChecked ();	
ddlInicioReal.SetEnabled (checked);
ddlTerminoReal.SetEnabled (checked);
	if(!checked){
		ddlInicioReal.SetValue(null);
		ddlTerminoReal.SetValue(null);
}
}" />
                            </dxtv:ASPxCheckBox>
                        </td>
                        <td class="auto-style2">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <dxtv:ASPxLabel ID="ASPxLabel7" runat="server"  Text="Início Real:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td class="auto-style2">
                            <dxtv:ASPxLabel ID="ASPxLabel8" runat="server"  Text="Término Real:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style2" style="padding-right: 10px">
                            <dxtv:ASPxDateEdit ID="ddlInicioReal" runat="server" ClientInstanceName="ddlInicioReal" DisplayFormatString="{0:dd/MM/yyyy}"  PopupVerticalAlign="WindowCenter" Width="100%" PopupHorizontalAlign="OutsideRight">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxDateEdit>
                        </td>
                        <td class="auto-style2" style="padding-right: 10px">
                            <dxtv:ASPxDateEdit ID="ddlTerminoReal" runat="server" ClientInstanceName="ddlTerminoReal" DisplayFormatString="{0:dd/MM/yyyy}"  PopupVerticalAlign="WindowCenter" Width="100%" PopupHorizontalAlign="OutsideRight">
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxDateEdit>
                        </td>
                        <td style="padding-right: 10px">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100px"  ID="btnSalvar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>

 </td><td style="WIDTH: 10px"></td><td><dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px"  ID="btnFechar">
<ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
}"></ClientSideEvents>

<Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
</dxe:ASPxButton>

 </td></tr></tbody></table>
            </td>
        </tr>
    </table>
</dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Registro incluído com sucesso!&quot;, 'sucesso', false, false, null);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Registro alterado com sucesso!&quot;, 'sucesso', false, false, null);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		window.top.mostraMensagem(&quot;Registro excluído com sucesso!&quot;, 'sucesso', false, false, null);

    window.parent.atualizaValor();
}"></ClientSideEvents>
</dxcp:aspxcallbackpanel>
            </td>
            <td>
            </td>
        </tr>

    </table>
</div>
    </form>
</body>
</html>
