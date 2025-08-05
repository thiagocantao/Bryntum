<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmIndiceReajusteContrato.aspx.cs" Inherits="_Projetos_Administracao_frmIndiceReajusteContrato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../../scripts/_Strings.js"></script>
    <script type="text/javascript" language="javascript">
        function Trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }
        
        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";

            if (Trim(txtDescricao.GetText()) == "") {
                numAux++;
                mensagem += "\n" + numAux + ") O campo Descrição do reajuste deve ser informado.";
            }
            if (spPercentualReajuste.GetValue() == "") {
                numAux++;
                mensagem += "\n" + numAux + ") O percentual do reajuste deve ser informado.";
            }
            if (dtDataAplicacaoReajuste.GetValue() == null) {
                numAux++;
                mensagem += "\n" + numAux + ") A data de aplicação do reajuste deve ser informada.";
            }

            if (mensagem != "") {
                mensagemErro_ValidaCamposFormulario = "Alguns dados são de preenchimento obrigatório:\n\n" + mensagem;
            }
            return mensagemErro_ValidaCamposFormulario;
        }

        function SalvarCamposFormulario() {   // esta função chama o método no servidor responsável por persistir as informações no banco
            // o método será chamado por meio do objeto pnCallBack
            hfGeral.Set("StatusSalvar", "0");
            pnCallback.PerformCallback(TipoOperacao);
            return false;
        }

        function ExcluirRegistroSelecionado() {   // esta função chama o método no servidor responsável por excluir o registro selecionado
            // o método será chamado por meio do objeto pnCallBack
            hfGeral.Set("StatusSalvar", "0");
            pnCallback.PerformCallback(TipoOperacao);
            return false;
        }

        // **************************************************************************************
        // - Altere as funções abaixo conforme a necessidade da tela que está sendo implementada
        // **************************************************************************************

        function LimpaCamposFormulario() {

            txtDescricao.SetText("");
            spPercentualReajuste.SetValue(0);
            dtDataAplicacaoReajuste.SetValue(null);
            //dtDataAplicacaoReajuste.SetText("");

            
            desabilitaHabilitaComponentes();
        }

        // Função responsável por preparar os campos do formulário para apresentar as informações do registro corrente
        function OnGridFocusedRowChanged(grid, forcarMontaCampos) {
            if (forcarMontaCampos || (window.pcDados && pcDados.IsVisible())) {
                grid.GetRowValues(grid.GetFocusedRowIndex(), 'CodigoContrato;NomeIndiceReajusteContrato;IndicaReajusteAplicado;DataInclusao;CodigoUsuarioInclusao;DataAplicacaoReajuste;PercentualReajuste;CodigoIndiceReajusteContrato;', MontaCamposFormulario);
            }
        }

        function MontaCamposFormulario(values) {
            //             0;                         1;                     2;           3;                    4;                    5;                 6;                           7;
            //CodigoContrato;NomeIndiceReajusteContrato;IndicaReajusteAplicado;DataInclusao;CodigoUsuarioInclusao;DataAplicacaoReajuste;PercentualReajuste;CodigoIndiceReajusteContrato;

            var codigoContrato = (values[0] != null ? values[0] : "");
            var nomeIndiceReajusteContrato = (values[1] != null ? values[1] : "");
            var indicaReajusteAplicado = (values[2] != null ? values[2] : "");
            var dataInclusao = (values[3] != null ? values[3] : "");
            var codigoUsuarioInclusao = (values[4] != null ? values[4] : "");
            var dataAplicacaoReajuste = (values[5] != null ? values[5] : null);
            var percentualReajuste = (values[6] != null ? values[6] : "");
            var codigoIndiceReajusteContrato = (values[7] != null ? values[7] : "");



            txtDescricao.SetText(nomeIndiceReajusteContrato);
            spPercentualReajuste.SetValue(percentualReajuste);
            dtDataAplicacaoReajuste.SetValue(dataAplicacaoReajuste);
 
            desabilitaHabilitaComponentes();
        }

        // ---------------------------------------------------------------------------------
        // Caso precise fazer alguma ação após a tela ter sido salva, altere a função abaixo
        // ---------------------------------------------------------------------------------

        function desabilitaHabilitaComponentes() {
            txtDescricao.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            spPercentualReajuste.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            dtDataAplicacaoReajuste.SetEnabled(window.TipoOperacao && TipoOperacao != "Consultar");
            
        }

        //----------- Mensagem modificação con sucesso..!!!
        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

            fechaTelaEdicao();
        }

        function fechaTelaEdicao() {
            
            onClick_btnCancelar();
        }   
       
    </script>
    <style type="text/css">

.dxpcControl
{
	font: 12px Tahoma;
	color: black;
	background-color: white;
	border: 1px solid #8B8B8B;
	width: 200px;
}
.dxpcHeader
{
	color: #404040;
	background-color: #DCDCDC;
	border-bottom: 1px solid #C9C9C9;
}
.dxpcHeader td.dxpc
{
	color: #404040;
	white-space: nowrap;
}
.dxpcContent
{
	color: #010000;
	white-space: normal;
	vertical-align: top;
}
.dxpcContentPaddings 
{
	padding: 9px 12px;
}


        .style5
        {
            width: 5px;
        }
        .style1
        {
            height: 10px;
        }
        .headerGrid
	{
		width:100%;
	}
	
	    .style8
        {
            width: 100%;
        }
        .style9
        {
            width: 413px;
        }
        .style10
        {
            width: 413px;
            height: 13px;
        }
    </style>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" Width="100%" 
            ClientInstanceName="pnCallback" oncallback="pnCallback_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	if (window.onEnd_pnCallback)
		onEnd_pnCallback();		
	if(&quot;Incluir&quot; == s.cp_OperacaoOk)
    {
		mostraDivSalvoPublicado(&quot;Reajuste incluído com sucesso!&quot;);
        try
	    {		
		    window.parent.gvDados.PerformCallback();
		}catch(e)
	    {}
    }
    else if(&quot;Editar&quot; == s.cp_OperacaoOk)
    {
		mostraDivSalvoPublicado(&quot;Reajuste alterado com sucesso!&quot;);
        try
	    {		
		    window.parent.gvDados.PerformCallback();
		}catch(e)
	    {}
    }
    else if(&quot;Excluir&quot; == s.cp_OperacaoOk)
    {
		mostraDivSalvoPublicado(&quot;Reajuste excluído com sucesso!&quot;);	
        try
	    {		
		    window.parent.gvDados.PerformCallback();
		}catch(e)
	    {}
    }
}" />
            <PanelCollection>
                <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral"></dxhf:ASPxHiddenField>
                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" AutoGenerateColumns="False" 
                    Width="100%"  ID="gvDados" 
                    OnCustomButtonInitialize="gvDados_CustomButtonInitialize" 
                    OnAfterPerformCallback="gvDados_AfterPerformCallback1" 
                    OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" 
                        KeyFieldName="CodigoIndiceReajusteContrato">
                    <ClientSideEvents 
                    FocusedRowChanged=
                    "function(s, e) {
	                    OnGridFocusedRowChanged(s);
                        }" 
                        CustomButtonClick=
                        "function(s, e){
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
                        }"></ClientSideEvents>
            <Columns>
                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" 
                        Width="100px" VisibleIndex="0"><CustomButtons>
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
                        <%# string.Format(@"<table style=""width:100%""><tr><td align=""center"">{0}</td></tr></table>", (podeIncluir) ? @"<img src=""../../imagens/botoes/incluirReg02.png"" title=""Incluir"" onclick=""onClickBarraNavegacao('Incluir', gvDados, pcDados);"" style=""cursor: pointer;""/>" : @"<img src=""../../imagens/botoes/incluirRegDes.png"" title=""Incluir"" style=""cursor: default;""/>")%>
                </HeaderTemplate>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn FieldName="CodigoContrato" Caption="CodigoContrato" 
                        VisibleIndex="1" Visible="False">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NomeIndiceReajusteContrato" 
                        Caption="Descrição" VisibleIndex="2">
                    <PropertiesTextEdit>
                        <Style >
                        </Style>
                    </PropertiesTextEdit>
                    <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                        AllowHeaderFilter="False" ShowFilterRowMenu="False" 
                        ShowInFilterControl="False" />
                </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Percentual Aplicado(%)" 
                        FieldName="PercentualReajuste" VisibleIndex="7" Width="150px">
                        <Settings AllowAutoFilter="False" AllowDragDrop="False" 
                            AllowHeaderFilter="False" ShowFilterRowMenu="False" 
                            ShowInFilterControl="False" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Data do Reajuste" 
                    FieldName="DataAplicacaoReajuste" ShowInCustomizationForm="True" 
                    VisibleIndex="6" Width="120px">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                        </PropertiesDateEdit>
                        <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                            AllowDragDrop="False" AllowHeaderFilter="False" ShowFilterRowMenu="False" 
                            ShowInFilterControl="False" />
                </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Aplicado?" 
                        FieldName="IndicaReajusteAplicado" VisibleIndex="3" Visible="False">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Incluído Em" 
                    FieldName="DataInclusao" ShowInCustomizationForm="True" VisibleIndex="4" 
                    Width="110px">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                        </PropertiesDateEdit>
                        <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                            AllowHeaderFilter="False" AllowSort="False" ShowFilterRowMenu="False" 
                            ShowInFilterControl="False" />
                </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Incluído Por" 
                        FieldName="CodigoUsuarioInclusao" Visible="False" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CodigoIndiceReajusteContrato" FieldName="CodigoIndiceReajusteContrato" 
                        VisibleIndex="8" Visible="False">
                    </dxwgv:GridViewDataTextColumn>
            </Columns>
<SettingsBehavior AllowSort="False" AllowFocusedRow="True"></SettingsBehavior>
<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>
<Settings ShowHeaderFilterBlankItems="False" 
         VerticalScrollBarMode="Visible"></Settings>
<SettingsText ></SettingsText>
</dxwgv:ASPxGridView>
 <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" CloseAction="None" 
            ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False" 
            Width="362px"  ID="pcDados">
<ContentStyle>
<Paddings Padding="5px"></Paddings>
</ContentStyle>
<HeaderStyle Font-Bold="True"></HeaderStyle>
<ContentCollection>
<dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td class="style5">
                    &nbsp;</td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="style8">
                                        <tr>
                                            <td class="style9">
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                    Text="Descrição">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxTextBox ID="txtDescricao" runat="server" MaxLength="64" 
                                                    Text="123456789 123456789 123456789 123456789 123456789 123456789 " 
                                                    Width="100%" ClientInstanceName="txtDescricao" 
                                                   >
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
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                                Text="Percentual Reajuste (%):">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                            <td>
                                                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                                Text="Data de Aplicação:">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-right: 3px">
                                                            <dxe:ASPxSpinEdit ID="spPercentualReajuste" runat="server" Height="21px" 
                                                                Number="0" Width="100%" ClientInstanceName="spPercentualReajuste" 
                                                                >
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                            <td>
                                                            <dxe:ASPxDateEdit ID="dtDataAplicacaoReajuste" runat="server" 
                                                                ClientInstanceName="dtDataAplicacaoReajuste" 
                                                                Width="100%" DisplayFormatString="dd/MM/yyyy">
                                                                <CalendarProperties>
                                                                    <DayHeaderStyle  />
                                                                    <WeekNumberStyle >
                                                                    </WeekNumberStyle>
                                                                    <DayStyle  />
                                                                    <DaySelectedStyle >
                                                                    </DaySelectedStyle>
                                                                    <DayOtherMonthStyle >
                                                                    </DayOtherMonthStyle>
                                                                    <DayWeekendStyle >
                                                                    </DayWeekendStyle>
                                                                    <DayOutOfRangeStyle >
                                                                    </DayOutOfRangeStyle>
                                                                    <TodayStyle >
                                                                    </TodayStyle>
                                                                    <ButtonStyle >
                                                                    </ButtonStyle>
                                                                    <HeaderStyle  />
                                                                    <FooterStyle  />
                                                                    <Style >
                                                                    </Style>
                                                                </CalendarProperties>
                                                                <HelpTextStyle >
                                                                </HelpTextStyle>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">
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
	e.processOnServer = false;
    if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>
<Paddings Padding="0px"></Paddings>
</dxe:ASPxButton>
                                                </td>
                                                <td style="WIDTH: 10px">
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
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" Modal="True" CloseAction="None" 
            ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" 
            ShowCloseButton="False" ShowHeader="False" Width="270px" 
            ID="pcUsuarioIncluido">
<ContentCollection>
<dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tbody>
        <tr>
            <td style="" align="center"></td>
            <td style="WIDTH: 70px" align="center" rowspan="3">
                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
            </td>
        </tr>
        <tr>
            <td style="HEIGHT: 10px"></td>
        </tr>
        <tr>
            <td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>
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
    </div>
    </form>
</body>
</html>
