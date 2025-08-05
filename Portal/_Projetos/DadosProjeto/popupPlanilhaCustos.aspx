<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupPlanilhaCustos.aspx.cs" Inherits="_Projetos_DadosProjeto_popupPlanilhaCustos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
       </style>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; height: 100%">
        <div style="height: 100%; width: 100%">
            <table style="height: 100%; width: 100%">
                <tr id="trProjeto">
                    <td>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>
                                    <dxcp:ASPxLabel runat="server" Text="Item:" ID="ASPxLabel5"></dxcp:ASPxLabel>


                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                <dxcp:ASPxComboBox runat="server" TextFormatString="{1}" Width="100%" ClientInstanceName="ddlItem" ID="ddlItem" NullValueItemDisplayText="{1}">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
               pnCallbackValorUnitario.PerformCallback(s.GetValue());
}" />
                                                    <Columns>
                                                        <dxcp:ListBoxColumn FieldName="NomeGrupo" Width="400px" Caption="Grupo"></dxcp:ListBoxColumn>
                                                        <dxcp:ListBoxColumn FieldName="NomeItem" Width="400px" Caption="Item"></dxcp:ListBoxColumn>
                                                    </Columns>

                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </ReadOnlyStyle>

                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                </dxcp:ASPxComboBox>


                                            </td>
                                            <td id="tdAjuda" align="center" style="width: 10px">
                                                <dxcp:ASPxImage runat="server" ImageUrl="~/imagens/ajuda.png" ClientInstanceName="imgAjuda" Cursor="Pointer" ID="imgAjuda">
                                                    <ClientSideEvents Click="function(s, e) {
	//pcAjuda.Show();
}"></ClientSideEvents>
                                                </dxcp:ASPxImage>


                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 14.28%">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 14.28%">
                                    <dxcp:ASPxLabel runat="server" Text="Quantidade:" ID="ASPxLabel7"></dxcp:ASPxLabel>


                                </td>
                                <td style="width: 14.28%">
                                    <dxcp:ASPxLabel runat="server" Text="Valor Unit&#225;rio (R$):" ID="ASPxLabel8"></dxcp:ASPxLabel>


                                </td>
                                <td style="width: 14.28%">
                                    <dxcp:ASPxLabel runat="server" Text="Valor Total (R$):" ID="ASPxLabel9"></dxcp:ASPxLabel>


                                </td>
                                <td style="width: 14.28%">
                                    <dxcp:ASPxLabel runat="server" Text="Requerido 1:" ID="lblRequeridoAnoCorrente"></dxcp:ASPxLabel>


                                </td>
                                <td style="width: 14.28%">
                                    <dxcp:ASPxLabel runat="server" Text="Requerido 2:" ID="lblRequeridoAnoSeguinte"></dxcp:ASPxLabel>


                                </td>
                                <td style="width: 14.28%">
                                    <dxcp:ASPxLabel runat="server" Text="Requerido 3:" ID="lblRequeridoAnoSeguinte2"></dxcp:ASPxLabel>


                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="padding-right: 10px">
                                    <dxcp:ASPxSpinEdit runat="server" Increment="0" MaxValue="9999999999999" NullText=" " DecimalPlaces="2" HorizontalAlign="Right" Width="100%" DisplayFormatString="{0:n2}" ClientInstanceName="txtQuantidade" ID="txtQuantidade">
                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>

                                        <ClientSideEvents ValueChanged="function(s, e) {
	calculaTotal();
}"></ClientSideEvents>

                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>

                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                    </dxcp:ASPxSpinEdit>


                                </td>
                                <td style="padding-right: 10px">
                                    <dxcp:ASPxCallbackPanel ID="pnCallbackValorUnitario" runat="server" Width="100%" ClientInstanceName="pnCallbackValorUnitario" OnCallback="pnCallbackValorUnitario_Callback">
                                        <PanelCollection>
                                            <dxcp:PanelContent runat="server">
                                                <dxcp:ASPxSpinEdit runat="server" Increment="0" MaxValue="9999999999999" NullText=" " DecimalPlaces="2" Number="0" HorizontalAlign="Right" Width="100%" DisplayFormatString="{0:n2}" ClientInstanceName="txtValorUnitario" ID="txtValorUnitario">
                                                    <SpinButtons ShowIncrementButtons="False"></SpinButtons>

                                                    <ClientSideEvents ValueChanged="function(s, e) {
	calculaTotal();
}"></ClientSideEvents>

                                                    <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                    </ReadOnlyStyle>

                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                </dxcp:ASPxSpinEdit>
                                            </dxcp:PanelContent>
                                        </PanelCollection>
                                    </dxcp:ASPxCallbackPanel>



                                </td>
                                <td style="padding-right: 10px">
                                    <dxcp:ASPxSpinEdit runat="server" Increment="0" MaxValue="9999999999999" NullText=" " DecimalPlaces="2" Number="0" HorizontalAlign="Right" Width="100%" DisplayFormatString="{0:n2}" ClientInstanceName="txtValorTotal" ClientEnabled="False" ID="txtValorTotal">
                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>

                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                    </dxcp:ASPxSpinEdit>


                                </td>
                                <td style="padding-right: 10px">
                                    <dxcp:ASPxSpinEdit runat="server" Increment="0" MaxValue="9999999999999" NullText=" " DecimalPlaces="2" Number="0" HorizontalAlign="Right" Width="100%" DisplayFormatString="{0:n2}" ClientInstanceName="txtValorRequeridoAnoCorrente" ID="txtValorRequeridoAnoCorrente">
                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>

                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>

                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                    </dxcp:ASPxSpinEdit>


                                </td>
                                <td style="padding-right: 10px">
                                    <dxcp:ASPxSpinEdit runat="server" Increment="0" MaxValue="9999999999999" NullText=" " DecimalPlaces="2" Number="0" HorizontalAlign="Right" Width="100%" DisplayFormatString="{0:n2}" ClientInstanceName="txtValorRequeridoAnoSeguinte" ID="txtValorRequeridoAnoSeguinte">
                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>

                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>

                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                    </dxcp:ASPxSpinEdit>


                                </td>
                                <td style="padding-right: 10px">
                                    <dxcp:ASPxSpinEdit runat="server" Increment="0" MaxValue="9999999999999" NullText=" " DecimalPlaces="2" Number="0" HorizontalAlign="Right" Width="100%" DisplayFormatString="{0:n2}" ClientInstanceName="txtValorRequeridoAnoSeguinte2" ID="txtValorRequeridoAnoSeguinte2">
                                        <SpinButtons ShowIncrementButtons="False"></SpinButtons>

                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>

                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                    </dxcp:ASPxSpinEdit>


                                </td>
                                <td>
                                    <dxcp:ASPxCheckBox runat="server" CheckState="Unchecked" Text="Contratar" ClientInstanceName="ckContratar" ID="ckContratar">
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                    </dxcp:ASPxCheckBox>


                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>
                                    <dxcp:ASPxLabel runat="server" Text="Fonte de Recurso:" ID="ASPxLabel10"></dxcp:ASPxLabel>


                                </td>
                                <td>
                                    <dxcp:ASPxLabel runat="server" Text="Dota&#231;&#227;o Or&#231;ament&#225;ria:" ID="ASPxLabel11"></dxcp:ASPxLabel>


                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 10px">
                                    <dxcp:ASPxComboBox runat="server" TextField="NomeProjeto" ValueField="CodigoProjeto" Width="100%" ClientInstanceName="ddlFonte" ID="ddlFonte">
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                    </dxcp:ASPxComboBox>


                                </td>
                                <td>
                                    <dxcp:ASPxComboBox runat="server" EnableCallbackMode="True" Width="100%" ClientInstanceName="txtDotacao" ID="txtDotacao" OnItemRequestedByValue="txtDotacao_ItemRequestedByValue" OnItemsRequestedByFilterCondition="txtDotacao_ItemsRequestedByFilterCondition">
                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </ReadOnlyStyle>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                    </dxcp:ASPxComboBox>


                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxcp:ASPxLabel runat="server" Text="Descri&#231;&#227;o:" ID="ASPxLabel12"></dxcp:ASPxLabel>


                        <dxcp:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCaraterOb" ForeColor="Silver" ID="lblCantCaraterOb"></dxcp:ASPxLabel>


                        <dxcp:ASPxLabel runat="server" Text="&amp;nbsp;de 2000" EncodeHtml="False" ClientInstanceName="lblDe250Ob" ForeColor="Silver" ID="lblDe250Ob"></dxcp:ASPxLabel>


                    </td>
                </tr>
                <tr>
                    <td>
                        <dxcp:ASPxMemo runat="server" Width="100%" ClientInstanceName="txtComentario" ID="txtComentario">
                            <ClientSideEvents Init="function(s, e) {
	onInit_txtComentario(s, e);
}"></ClientSideEvents>

                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                            </ReadOnlyStyle>

                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                        </dxcp:ASPxMemo>


                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tbody>
                                <tr>
                                    <td align="left">&nbsp;</td>
                                    <td width="100px">
                                        <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="100%" ID="btnSalvar">
                                            <ClientSideEvents Click="function(s, e) {

	e.processOnServer = false;
    //hfGeral.Set(&quot;Critica&quot;,&quot;S&quot;);
	//if(window.parent.parent.hfGeralWorkflow)
	//	hfGeral.Set('CodigoInstanciaWf', window.parent.parent.hfGeralWorkflow.Get('CodigoInstanciaWf'));
    //if (window.onClick_btnSalvar)
	 //   onClick_btnSalvar();

callbackSalvar.PerformCallback();
}"></ClientSideEvents>

                                            <Paddings Padding="0px"></Paddings>
                                        </dxcp:ASPxButton>



                                    </td>
                                    <td style="width: 10px"></td>
                                    <td width="100px">

                                        <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="100%" ID="btnFechar">
                                            <ClientSideEvents Click="function(s, e) {
e.processOnServer = false;
window.top.fechaModal();
}"></ClientSideEvents>

                                            <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                        </dxcp:ASPxButton>



                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <dxcp:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
        if(s.cpErro == '')
        {
                   if(s.cpSucesso != '')
                   {
                                 window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null, 3000);
                                s.cpSucesso = '';
                                 window.top.fechaModal();
                    }
        }
        else
        {
                window.top.mostraMensagem(s.cpErro, 'erro', true, false, null, 3000);
               s.cpErro = false;
        }
        
}" />
        </dxcp:ASPxCallback>

    </form>
</body>
</html>
