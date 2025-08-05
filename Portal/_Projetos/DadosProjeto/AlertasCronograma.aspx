<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertasCronograma.aspx.cs" Inherits="_Projetos_DadosProjeto_TarefasToDoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
    <title>Alertas</title>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>



    <script language="javascript" type="text/javascript">
// <!CDATA[

// ]]>
    </script>
    <style type="text/css">
        .style1 {
            height: 10px;
        }

        .style2 {
            width: 100%;
        }

        .style7 {
            height: 5px;
        }

        .style8 {
            height: 21px;
        }

        .style9 {
            height: 11px;
        }
    </style>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
<dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%" ClientInstanceName="pnCallback" OnCallback="pnCallback_Callback">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <!-- ASPxGRIDVIEW: gvDados -->
                                    <div id="divGrid" style="visibility: hidden">
                                        <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados"
                                            KeyFieldName="CodigoAlerta" AutoGenerateColumns="False" Width="100%"
                                            ID="gvDados"
                                            OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                                            <ClientSideEvents FocusedRowChanged="function(s, e) {
	OnGridFocusedRowChanged(s);
}"
                                                CustomButtonClick="function(s, e) {
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
	 else if(e.buttonID == &quot;btnTarefas&quot;)
     {
		clickAssociacaoTarefas();
     }
	
}"
                                                Init="function(s, e) {
    AdjustSize();
    document.getElementById('divGrid').style.visibility = '';
}"></ClientSideEvents>
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="125px"
                                                    Caption=" " VisibleIndex="0">
                                                    <CustomButtons>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditarCustom" Text="Editar">
                                                            <Image Url="~/imagens/botoes/editarReg02.PNG"></Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluirCustom" Text="Excluir">
                                                            <Image Url="~/imagens/botoes/excluirReg02.PNG"></Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnFormularioCustom" Text="Detalhe">
                                                            <Image Url="~/imagens/botoes/pFormulario.png"></Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnTarefas"
                                                            Text="Associar Tarefas e Destinatários">
                                                            <Image Url="~/imagens/botoes/tarefas_BTN.png">
                                                            </Image>
                                                        </dxwgv:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>

                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                                    <HeaderTemplate>
                                                        <%# string.Format(@"<table style=""width:100%""><tr><td align=""left"">{0}</td></tr></table>", 
    ((podeIncluir) ? 
                    string.Format(@"<img src=""../../imagens/botoes/incluirReg02.png"" title=""{0}"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>", Resources.traducao.incluir) : 
                    string.Format(@"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""{0}"" style=""cursor: default;""/>", Resources.traducao.incluir)
                    ))%>
                                                    </HeaderTemplate>
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Alerta" FieldName="DescricaoAlerta"
                                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Incluída por"
                                                    FieldName="UsuarioInclusao" ShowInCustomizationForm="True" VisibleIndex="2"
                                                    Width="220px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="DiasAntecedenciaInicio1"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="DiasIntervaloRecorrenciaInicio2"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="DiasAntecedenciaInicio2"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="DiasIntervaloRecorrenciaInicio3"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="DiasIntervaloRecorrenciaTermino"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="DiasIntervaloRecorrenciaAtraso"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="MensagemAlertaInicio1"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="MensagemAlertaInicio2"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="MensagemAlertaInicio3"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="MensagemAlertaTermino"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="MensagemAlertaAtraso"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="IndicaAlertaInicio1"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="IndicaAlertaInicio2"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="IndicaAlertaInicio3"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="IndicaAlertaTermino"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="IndicaAlertaAtraso"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>

                                            <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                            <Settings VerticalScrollBarMode="Visible" />

                                            <SettingsText GroupPanel="Arraste aqui as colunas que deseja agrupar"></SettingsText>
                                            <Templates>
                                                <FooterRow>
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Tarefa Concluída" ClientInstanceName="lblDescricaoConcluido" ID="lblDescricaoConcluido"></dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 10px"></td>
                                                                <td style="width: 10px; background-color: green"></td>
                                                                <td style="width: 10px" align="center">|</td>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Tarefa Atrasada" ClientInstanceName="lblDescricaoAtrasada" ID="lblDescricaoAtrasada"></dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 10px"></td>
                                                                <td style="width: 10px; background-color: red"></td>
                                                                <td style="width: 10px" align="center">|</td>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Tem Anotações" ClientInstanceName="lblDescricaoAnotacoes" ID="lblDescricaoAnotacoes"></dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 10px"></td>
                                                                <td>
                                                                    <img style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; border-right-width: 0px" src="../../imagens/anotacao.gif" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </FooterRow>
                                            </Templates>
                                        </dxwgv:ASPxGridView>
                                    </div>
                                    <!-- PANEL CONTROL : pcDados -->

                                </dxp:PanelContent>
                            </PanelCollection>

                            <ClientSideEvents EndCallback="function(s, e) {
     if(s.cp_MSG != '')
    {
            window.top.mostraMensagem(s.cp_MSG, 'sucesso', false, false, null);
     onClick_btnCancelar();
    }
    else if(s.cp_Erro != '')
   {
            window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
   }	
}"></ClientSideEvents>
                        </dxcp:ASPxCallbackPanel>
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="774px" PopupVerticalOffset="-20" Height="145px" ID="pcDados">
            <ClientSideEvents Shown="function(s, e) {
	desabilitaHabilitaComponentes();
}"
                CloseUp="function(s, e) {
	pcAlertas.SetActiveTabIndex(0);
}"></ClientSideEvents>
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Nome do Alerta:"
                                                        ClientInstanceName="lblDescricaoTarefa"
                                                        ID="lblDescricaoRegra">
                                                    </dxe:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="100%"
                                                        ClientInstanceName="txtNomeRegra"
                                                        ID="txtNomeRegra">
                                                        <DisabledStyle BackColor="#E0E0E0" ForeColor="Black">
                                                            <border bordercolor="Silver"></border>
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxtc:ASPxPageControl ID="pcAlertas" runat="server" ActiveTabIndex="0"
                                        Width="100%"
                                        ClientInstanceName="pcAlertas">
                                        <TabPages>
                                            <dxtc:TabPage Text="Alertas de Início">
                                                <ContentCollection>
                                                    <dxw:ContentControl runat="server">
                                                        <div style="overflow-y: auto; height:230px;">
                                                            <table cellspacing="0" class="style2" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <table cellspacing="0" class="style2" cellpadding="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellspacing="1">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel11" runat="server"
                                                                                                    Text="1)">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxCheckBox ID="ck001" runat="server" ClientInstanceName="ck001"
                                                                                                    Font-Bold="False" Text="Avisar com">
                                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	habilitaAlerta001();
	limpaCampos001();
	limpaCampos002();
	limpaCampos003();
}" />
                                                                                                    <DisabledStyle ForeColor="#646464">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxCheckBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxSpinEdit ID="txt001" runat="server" ClientInstanceName="txt001"
                                                                                                    MaxValue="999" MinValue="1"
                                                                                                    NumberType="Integer" Width="35px">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                                                    Text="dias antes que a tarefa inicie.">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxMemo ID="txtDescricao001" runat="server"
                                                                                        Rows="4" Width="100%" ClientInstanceName="txtDescricao001">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td id="tb002">
                                                                        <table cellspacing="0" class="style2" cellpadding="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellspacing="1">
                                                                                        <tr>
                                                                                            <td style="padding-left: 25px">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel12" runat="server"
                                                                                                    Text="2)">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxCheckBox ID="ck002" runat="server" ClientInstanceName="ck002"
                                                                                                    Font-Bold="False" Text="A cada">
                                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	habilitaAlerta002(true);
	limpaCampos002();
}" />
                                                                                                    <DisabledStyle ForeColor="#646464">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxCheckBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxSpinEdit ID="txt002" runat="server"
                                                                                                    NumberType="Integer" Width="35px"
                                                                                                    ClientInstanceName="txt002" MaxValue="999" MinValue="1">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server"
                                                                                                    Text="dias até">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxSpinEdit ID="txt003" runat="server"
                                                                                                    NumberType="Integer" Width="35px"
                                                                                                    ClientInstanceName="txt003" MaxValue="999" MinValue="1">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                                                                    Text="dias antes que a tarefa inicie.">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-left: 25px">
                                                                                    <dxe:ASPxMemo ID="txtDescricao002" runat="server"
                                                                                        Rows="4" Width="100%" ClientInstanceName="txtDescricao002">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style7" id="tdSeparador"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td id="tb003">
                                                                        <table cellspacing="0" class="style2" cellpadding="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellspacing="1">
                                                                                        <tr>
                                                                                            <td style="padding-left: 25px">
                                                                                                <dxe:ASPxLabel ID="ASPxLabel13" runat="server"
                                                                                                    Text="3)">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxCheckBox ID="ck003" runat="server" ClientInstanceName="ck003"
                                                                                                    Font-Bold="False" Text="E depois a cada">
                                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	habilitaAlerta003(true);
	limpaCampos003();
}" />
                                                                                                    <DisabledStyle ForeColor="#646464">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxCheckBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxSpinEdit ID="txt004" runat="server"
                                                                                                    NumberType="Integer" Width="35px"
                                                                                                    ClientInstanceName="txt004" MaxValue="999" MinValue="1">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                                                    Text="dias antes que a tarefa inicie.">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-left: 25px">
                                                                                    <dxe:ASPxMemo ID="txtDescricao003" runat="server"
                                                                                        Rows="4" Width="100%" ClientInstanceName="txtDescricao003">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style9" style="padding-left: 25px"></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </dxw:ContentControl>
                                                </ContentCollection>
                                            </dxtc:TabPage>
                                            <dxtc:TabPage Text="Alertas de Término e Atraso">
                                                <ContentCollection>
                                                    <dxw:ContentControl runat="server">
                                                        <div style="overflow-y: auto; height: 230px">
                                                            <table cellspacing="0" class="style2" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <table cellspacing="0" class="style2" cellpadding="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellspacing="1">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel14" runat="server"
                                                                                                    Text="4)">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxCheckBox ID="ck004" runat="server" ClientInstanceName="ck004"
                                                                                                    Font-Bold="False" Text="Avisar a cada">
                                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	habilitaAlerta004();
	limpaCampos004();
}" />
                                                                                                </dxe:ASPxCheckBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxSpinEdit ID="txt005" runat="server"
                                                                                                    NumberType="Integer" Width="35px"
                                                                                                    ClientInstanceName="txt005" MaxValue="999" MinValue="1">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server"
                                                                                                    Text="dias antes que a tarefa termine.">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxMemo ID="txtDescricao004" runat="server"
                                                                                        Rows="7" Width="100%" ClientInstanceName="txtDescricao004">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style8"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellspacing="0" class="style2" cellpadding="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellspacing="1">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel15" runat="server"
                                                                                                    Text="5)">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxCheckBox ID="ck005" runat="server" ClientInstanceName="ck005"
                                                                                                    Font-Bold="False" Text="Avisar a cada">
                                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	habilitaAlerta005();
	limpaCampos005();
}" />
                                                                                                </dxe:ASPxCheckBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxSpinEdit ID="txt006" runat="server"
                                                                                                    NumberType="Integer" Width="35px"
                                                                                                    ClientInstanceName="txt006" MaxValue="999" MinValue="1">
                                                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                                                    </SpinButtons>
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxe:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                                                    Text="dias em caso de atraso até que a tarefa esteja 100% concluída.">
                                                                                                </dxe:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxMemo ID="txtDescricao005" runat="server"
                                                                                        Rows="7" Width="100%" ClientInstanceName="txtDescricao005">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxMemo>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </dxw:ContentControl>
                                                </ContentCollection>
                                            </dxtc:TabPage>
                                        </TabPages>
                                    </dxtc:ASPxPageControl>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <table  cellspacing="0" cellpadding="0" border="0" style="width:220px">
                                        <tbody>
                                            <tr>
                                                <td style="width:100px;padding-right:5px">
                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar" ValidationGroup="MKE"  Width="100%" ID="btnSalvar">
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
                                                        <Paddings PaddingRight="5px"/>
                                                    </dxe:ASPxButton>

                                                </td>
                                                <td style="width:100px">
                                                    <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar" Width="100%" ID="btnFechar">
                                                        <ClientSideEvents Click="function(s, e) {	
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
                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>

                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>

    </form>
</body>
</html>
