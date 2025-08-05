<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanilhaCustos.aspx.cs" Inherits="_Projetos_DadosProjeto_PlanilhaCustos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script language="javascript" type="text/javascript">
        function defineStatusLinhaBase(texto) {
            if (texto.indexOf('REPROVADA') != -1) {
                lblStatusLinhaBase.mainElement.style.color = 'red'
            }
            else if (texto.indexOf('APROVADA') != -1) {
                lblStatusLinhaBase.mainElement.style.color = 'green'
            }
            else {
                lblStatusLinhaBase.mainElement.style.color = 'black'
            }
            lblStatusLinhaBase.SetText(texto);
        }

    </script>
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
            height: 10px;
        }
        .style8
        {
            width: 10px;
            height: 5px;
        }
        .style9
        {
            height: 5px;
        }
        .style10
        {
            width: 450px;
        }
        .style28
        {
            width: 20px;
        }
        .style30
        {
            width: 460px;
        }
        .style31
        {
            width: 115px;
        }
        .style32
        {
            width: 90px;
        }
        .style34
        {
            width: 83px;
        }
        .style35
        {
            width: 100px;
        }
        .style36
        {
            width: 125px;
        }
        </style>
    </head>
<body class="body">
    <form id="form1" runat="server" style="height : 100%;">
        <div>
    <table style="width: 100%">
        <tr>
            <td class="style9">
            </td>
            <td class="style9">
            </td>
            <td class="style8">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td align="left">
                            <dxe:ASPxLabel runat="server" 
                                Font-Size="7pt" ID="lblStatusLinhaBase" 
                                ClientInstanceName="lblStatusLinhaBase" Font-Bold="True" Font-Italic="True"></dxe:ASPxLabel>


                        </td>
                        <td align="right">
                            <dxe:ASPxLabel runat="server" Text="Linha de Base:" 
                                ID="ASPxLabel6"></dxe:ASPxLabel>


                        </td>
                        <td class="style30">
                            <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains" 
                                Width="100%" ClientInstanceName="ddlLinhaBase" 
                                ID="ddlLinhaBase">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	setTimeout('defineStatusLinhaBase(ddlLinhaBase.cp_StatusLB' + s.GetSelectedIndex() + ')', 1);
	pnCallback.PerformCallback();
}" Init="function(s, e) {
	if(s.GetValue() != null)
		setTimeout('defineStatusLinhaBase(ddlLinhaBase.cp_StatusLB' + s.GetSelectedIndex() + ')', 1);
	
		
}" />
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxComboBox>


                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td class="style8">
            </td>
            <td class="style9">
            </td>
            <td class="style8">
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
        KeyFieldName="CodigoItemOrcamento" AutoGenerateColumns="False" Width="100%" 
         ID="gvDados" 
        OnCustomButtonInitialize="gvDados_CustomButtonInitialize" 
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
                 hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Editar&quot;);
                TipoOperacao = &quot;Editar&quot;;
               s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoItemRecurso;QuantidadeOrcada;ValorUnitario;ValorTotal;ValorRequeridoAnoCorrente;ValorRequeridoAnoSeguinte;CodigoFonteRecursosFinanceiros;DotacaoOrcamentaria;IndicaContratarItem;UnidadeMedida;DetalheItemRecurso;ValorRequeridoPosAnoSeguinte;DescricaoItemOrcamentoProjeto;CodigoItemOrcamento', callbackPopupPlanilhaCustos);
     }
     else if(e.buttonID == &quot;btnExcluir&quot;)
     {
		//onClickBarraNavegacao(&quot;Excluir&quot;, gvDados, pcDados);
        window.top.mostraMensagem(traducao.barraNavegacao_deseja_realmente_excluir_o_registro_, 'confirmacao', true, true, ExcluirRegistroSelecionado);
     }
     else if(e.buttonID == &quot;btnDetalhesCustom&quot;)
     {	
                TipoOperacao = &quot;Consultar&quot;;
               s.GetRowValues(s.GetFocusedRowIndex(), 'CodigoItemRecurso;QuantidadeOrcada;ValorUnitario;ValorTotal;ValorRequeridoAnoCorrente;ValorRequeridoAnoSeguinte;CodigoFonteRecursosFinanceiros;DotacaoOrcamentaria;IndicaContratarItem;UnidadeMedida;DetalheItemRecurso;ValorRequeridoPosAnoSeguinte;DescricaoItemOrcamentoProjeto;CodigoItemOrcamento', callbackPopupPlanilhaCustos);
     }	
	 if(e.buttonID == &quot;btnAprovar&quot;)
     {
		hfGeral.Set(&quot;TipoOperacao&quot;, &quot;Aprovar&quot;);
		TipoOperacao = &quot;Aprovar&quot;;
		pcAprovacao.Show();
		OnGridFocusedRowChanged(gvDados, true);		
     }
}
"></ClientSideEvents>
     <GroupSummary>
         <dxwgv:ASPxSummaryItem DisplayFormat="N2" FieldName="ValorTotal" 
             SummaryType="Sum" ShowInGroupFooterColumn="ValorTotal" />
         <dxwgv:ASPxSummaryItem DisplayFormat="N2" FieldName="ValorRequeridoAnoCorrente" ShowInGroupFooterColumn="ValorRequeridoAnoCorrente" 
             SummaryType="Sum" />
         <dxwgv:ASPxSummaryItem DisplayFormat="N2" FieldName="ValorRequeridoAnoSeguinte" ShowInGroupFooterColumn="ValorRequeridoAnoSeguinte" 
             SummaryType="Sum" />
         <dxwgv:ASPxSummaryItem DisplayFormat="N2" 
             FieldName="ValorRequeridoPosAnoSeguinte" 
             ShowInGroupFooterColumn="ValorRequeridoPosAnoSeguinte" SummaryType="Sum" />
         <dxwgv:ASPxSummaryItem DisplayFormat="Total:" FieldName="NomeGrupoNivel2" 
             ShowInGroupFooterColumn="NomeGrupoNivel2" />
     </GroupSummary>
     <TotalSummary>
         <dxwgv:ASPxSummaryItem DisplayFormat="N2" FieldName="ValorTotal" 
             SummaryType="Sum" ShowInColumn="ValorTotal" />
         <dxwgv:ASPxSummaryItem DisplayFormat="N2" FieldName="ValorRequeridoAnoCorrente" ShowInColumn="ValorRequeridoAnoCorrente" 
             SummaryType="Sum" />
         <dxwgv:ASPxSummaryItem DisplayFormat="N2" FieldName="ValorRequeridoAnoSeguinte" ShowInColumn="ValorRequeridoAnoSeguinte" 
             SummaryType="Sum" />
         <dxwgv:ASPxSummaryItem DisplayFormat="N2" 
             FieldName="ValorRequeridoPosAnoSeguinte" 
             ShowInColumn="ValorRequeridoPosAnoSeguinte" SummaryType="Sum" />
         <dxwgv:ASPxSummaryItem DisplayFormat="Total:" FieldName="NomeItemRecurso" 
             ShowInColumn="NomeItemRecurso" />
     </TotalSummary>
<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="100px" VisibleIndex="0" 
        FixedStyle="Left">
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
    <table style="width:100%"><tr>
        <td style="padding-right:5px" align="left"><%# string.Format((podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir"" onclick=""novoRegistro();"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Novo"" style=""cursor: default;""/>")%>
        </td>
        <td><asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="~/imagens/botoes/btnExcel.png" onclick="ImageButton1_Click" 
            ToolTip="Exportar para excel"  /></td>
        <td>
            <dxe:ASPxImage ID="ASPxImage1" runat="server" 
                ImageUrl="~/imagens/botoes/btnPDF.png" ToolTip="Exportar para PDF" 
               >
                <ClientSideEvents Click="function(s, e) {
	callback.PerformCallback(&quot;Download&quot;);
}" />
            </dxe:ASPxImage>
        </td>
        </tr></table>
</HeaderTemplate>
</dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn Caption="Item" FieldName="NomeGrupoNivel2" 
        ShowInCustomizationForm="True" VisibleIndex="1" FixedStyle="Left" Width="300px">
        <FooterTemplate>
            Total:
        </FooterTemplate>
    </dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Caption="Especificação" FieldName="NomeItemRecurso" 
        ShowInCustomizationForm="True" VisibleIndex="2" FixedStyle="Left" 
        Width="300px">
        <Settings AutoFilterCondition="Contains" />       
    </dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn 
        Caption="Quantidade" FieldName="QuantidadeOrcada" VisibleIndex="3" 
        Width="85px">
    <PropertiesTextEdit DisplayFormatString="N2">
    </PropertiesTextEdit>
    <Settings AllowAutoFilter="False" />
    <HeaderStyle HorizontalAlign="Right" />
    <CellStyle HorizontalAlign="Right">
    </CellStyle>
</dxwgv:GridViewDataTextColumn>
    
    <dxwgv:GridViewDataTextColumn Caption="Unitário (R$)" 
        ShowInCustomizationForm="True" VisibleIndex="4" Width="120px" 
        FieldName="ValorUnitario">
        <PropertiesTextEdit DisplayFormatString="N2">
        </PropertiesTextEdit>
        <Settings AllowAutoFilter="False" />
        <HeaderStyle HorizontalAlign="Right" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxwgv:GridViewDataTextColumn>
     <dxwgv:GridViewDataTextColumn Caption="Total (R$)" 
        ShowInCustomizationForm="True" VisibleIndex="5" Width="140px" 
        FieldName="ValorTotal">
        <PropertiesTextEdit DisplayFormatString="N2">
        </PropertiesTextEdit>
        <Settings AllowAutoFilter="False" />
        <HeaderStyle HorizontalAlign="Right" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Requerido 1 (R$)" 
        ShowInCustomizationForm="True" VisibleIndex="6" Width="140px" 
        FieldName="ValorRequeridoAnoCorrente">
        <PropertiesTextEdit DisplayFormatString="N2">
        </PropertiesTextEdit>
        <Settings AllowAutoFilter="False" />
        <HeaderStyle HorizontalAlign="Right" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Requerido 2 (R$)" 
        ShowInCustomizationForm="True" VisibleIndex="7" Width="140px" 
        FieldName="ValorRequeridoAnoSeguinte">
        <PropertiesTextEdit DisplayFormatString="N2">
        </PropertiesTextEdit>
        <Settings AllowAutoFilter="False" />
        <HeaderStyle HorizontalAlign="Right" />
        <CellStyle HorizontalAlign="Right">
        </CellStyle>
    </dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="ValorRequeridoPosAnoSeguinte" 
        ShowInCustomizationForm="True" Width="140px" Caption="Requerido 3 (R$)" 
        VisibleIndex="8" Name="ValorRequeridoPosAnoSeguinte">
    <PropertiesTextEdit DisplayFormatString="N2">
    </PropertiesTextEdit>
<Settings AllowAutoFilter="False"></Settings>
    <HeaderStyle HorizontalAlign="Right" />
    <CellStyle HorizontalAlign="Right">
    </CellStyle>
</dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Fonte de Recursos" FieldName="NomeFonteRecursosFinanceiros" 
        ShowInCustomizationForm="True" VisibleIndex="9" Width="180px">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Dotação Orçamentária" FieldName="DotacaoOrcamentaria" 
        ShowInCustomizationForm="True" VisibleIndex="10" Width="270px">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Contratar" FieldName="IndicaContratarItem" 
        ShowInCustomizationForm="True" VisibleIndex="11" Width="100px">
        <Settings AutoFilterCondition="Contains" />
        <Settings AllowAutoFilter="False" AutoFilterCondition="Contains" />
        <DataItemTemplate>
            <%# Eval("IndicaContratarItem").ToString() == "S" ? "Sim" : "Não" %>
        </DataItemTemplate>
        <HeaderStyle HorizontalAlign="Center" />
        <CellStyle HorizontalAlign="Center">
        </CellStyle>
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption=" Tipo" FieldName="NomeGrupoNivel1" 
        GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" 
        SortOrder="Ascending" VisibleIndex="12">
    </dxwgv:GridViewDataTextColumn>    
    <dxwgv:GridViewDataTextColumn FieldName="CodigoFonteRecursosFinanceiros" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="UnidadeMedida" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="DetalheItemRecurso" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn FieldName="DescricaoItemOrcamentoProjeto" 
        ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
    </dxwgv:GridViewDataTextColumn>
    <dxtv:GridViewDataTextColumn Caption="CodigoItemOrcamento" FieldName="CodigoItemOrcamento" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
    </dxtv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

<Settings VerticalScrollBarMode="Visible" ShowFilterRow="True" 
         HorizontalScrollBarMode="Visible" 
         ShowGroupFooter="VisibleIfExpanded" ShowFooter="True"></Settings>

</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido"><ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><tbody><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>

























 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>

























 </td></tr></tbody></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 </dxp:PanelContent>
</PanelCollection>

<ClientSideEvents EndCallback="function(s, e) 
{
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Item incluído com sucesso!&quot;);
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Item alterado com sucesso!&quot;);
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
		mostraDivSalvoPublicado(&quot;Item excluído com sucesso!&quot;);
	
}"></ClientSideEvents>
</dxcp:aspxcallbackpanel>
            </td>
            <td>
            </td>
        </tr>

    </table>
    </div>
 <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="gvExporter" 
                        OnRenderBrick="gvExporter_RenderBrick">
     <Styles>
         <Cell >
         </Cell>
     </Styles>
        </dxwgv:ASPxGridViewExporter>

        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" 
            oncallback="callback_Callback">
            <ClientSideEvents EndCallback="function(s, e) {           
	if(s.cp_CustoUnitario != null &amp;&amp; s.cp_CustoUnitario != '')
	{
		txtValorUnitario.SetValue(parseFloat(s.cp_CustoUnitario));
	}
	else
		txtValorUnitario.SetValue(null);

	if(s.cp_DescricaoItem != null &amp;&amp; s.cp_DescricaoItem != '')
	{		
		document.getElementById('tdAjuda').style.display = '';
		lblDescricaoItem.SetText(s.cp_DescricaoItem);
	}
	else
	{
		lblDescricaoItem.SetText('');
		document.getElementById('tdAjuda').style.display = 'none';
	}
	
	calculaTotal();
}" CallbackComplete="function(s, e) {	
	if(e.parameter.indexOf(&quot;Download&quot;) &gt; -1)
	{
		window.location = window.top.pcModal.cp_Path + &quot;_Projetos/DadosProjeto/ReportOutput.aspx?exportType=pdf&amp;bInline=False&quot;;
	}
}" />
        </dxcb:ASPxCallback>

 <dxpc:ASPxPopupControl runat="server" PopupElementID="tdAjuda" CloseAction="MouseOut" 
            AllowDragging="True" ClientInstanceName="pcAjuda" 
            HeaderText="Descrição do Item" Width="380px" 
            ID="pcAjuda" PopupAction="MouseOver">
<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <dxe:ASPxLabel ID="lblDescricaoItem" runat="server" 
                    ClientInstanceName="lblDescricaoItem"  
                    Wrap="True">
                </dxe:ASPxLabel>
    </dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>

    </form>
    </body>
</html>
