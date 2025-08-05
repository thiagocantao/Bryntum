<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="frmOutrasDespesas.aspx.cs" Inherits="administracao_CadastroRamosAtividades" %>
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
.dxgvTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxeHLC, .dxeHC, .dxeHFC
{
    display: none;
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
<dxcp:GridViewDataTextColumn FieldName="Despesa" ShowInCustomizationForm="True" Caption="Despesa" VisibleIndex="1">
</dxcp:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="Quantidade" 
        Caption="Quantidade" VisibleIndex="3" Width="120px">
    <PropertiesTextEdit DisplayFormatString="{0:n0}">
    </PropertiesTextEdit>
    <Settings AllowAutoFilter="False" />
    <HeaderStyle HorizontalAlign="Right" />
    <CellStyle HorizontalAlign="Right">
    </CellStyle>
</dxwgv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="Valor" FieldName="Valor" ShowInCustomizationForm="True" VisibleIndex="4" Width="130px">
        <PropertiesTextEdit DisplayFormatString="c2">
        </PropertiesTextEdit>
        <Settings AllowAutoFilter="False" />
        <HeaderStyle HorizontalAlign="Right" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="CodigoItemOutraDespesa" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="QuantidadeReal" ShowInCustomizationForm="True" VisibleIndex="7" Width="120px">
        <PropertiesTextEdit DisplayFormatString="{0:n0}">
        </PropertiesTextEdit>
        <Settings AllowAutoFilter="False" />
    </dxtv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn FieldName="AcaoPlanejada" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
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
            <dxtv:MenuItem Name="itemOutraDespesaNaoPlanejada" Text="Outra despesa não planejada">
            </dxtv:MenuItem>
            <dxtv:MenuItem Name="itemRegistrarOutrasDespesasPlanejadas" Text="Registrar outras despesas planejadas">
            </dxtv:MenuItem>
        </Items>
    </dxtv:ASPxPopupMenu>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="550px"  ID="pcDados">
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
                    Text="Despesa:">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dxtv:ASPxComboBox ID="ddlAcao" runat="server" ClientInstanceName="ddlAcao" EnableCallbackMode="True"  Width="100%" OnCustomJSProperties="ddlAcao_CustomJSProperties" TextFormatString="{0}" DropDownRows="5">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
var valor = ddlAcao.cpHiddenColumnValues[ddlAcao.GetSelectedIndex()];
if(txtQuantidade.GetValue() != null)
	txtValor.SetValue(txtQuantidade.GetValue() * valor);
	}" />
                    <Columns>
                        <dxtv:ListBoxColumn Caption="Despesa" FieldName="Descricao" Width="500px" />
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
            <td style="height: 15px">
                <table cellspacing="0" class="auto-style1">
                    <tr>
                        <td class="auto-style2">
                            <dxtv:ASPxLabel ID="ASPxLabel5" runat="server"  Text="Quantidade:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td>
                            <dxtv:ASPxLabel ID="ASPxLabel6" runat="server"  Text="Valor:">
                            </dxtv:ASPxLabel>
                        </td>
                        <td class="auto-style2">
                            <dxtv:ASPxLabel ID="lblQuantidadeReal" runat="server"  Text="Quantidade Real:">
                            </dxtv:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2" style="padding-right: 10px">
                            <dxtv:ASPxSpinEdit ID="txtQuantidade" runat="server" ClientInstanceName="txtQuantidade" DisplayFormatString="{0:n0}"  Number="0" NumberType="Integer" Width="100%">
                                <SpinButtons ClientVisible="False">
                                </SpinButtons>
                                <ClientSideEvents ValueChanged="function(s, e) {
var valor = ddlAcao.cpHiddenColumnValues[ddlAcao.GetSelectedIndex()];
if(txtQuantidade.GetValue() != null)
	txtValor.SetValue(txtQuantidade.GetValue() * valor);
if(gvDados.cpExibirColunas){
		txtQuantidadeReal.SetValue(s.GetValue());
	}
	}
" />
                                <ReadOnlyStyle BackColor="#EBEBEB">
                                </ReadOnlyStyle>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxSpinEdit>
                        </td>
                        <td style="padding-right: 10px">
                            <dxtv:ASPxSpinEdit ID="txtValor" runat="server" ClientInstanceName="txtValor" DecimalPlaces="2" DisplayFormatString="{0:c2}"  Number="0" Width="100%">
                                <SpinButtons ClientVisible="False">
                                </SpinButtons>
                                <ReadOnlyStyle BackColor="#EBEBEB">
                                </ReadOnlyStyle>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxtv:ASPxSpinEdit>
                        </td>
                        <td class="auto-style2" style="padding-right: 10px">
                            <dxtv:ASPxSpinEdit ID="txtQuantidadeReal" runat="server" ClientInstanceName="txtQuantidadeReal" DisplayFormatString="{0:n0}"  Number="0" NumberType="Integer" Width="100%">
                                <SpinButtons ClientVisible="False">
                                </SpinButtons>
                                <ClientSideEvents ValueChanged="function(s, e) {
var valor = ddlAcao.cpHiddenColumnValues[ddlAcao.GetSelectedIndex()];
if(txtQuantidade.GetValue() != null)
	txtValor.SetValue(txtQuantidade.GetValue() * valor);
	}
" />
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
