<%@ Page Language="C#" AutoEventWireup="true" CodeFile="novaMensagemDemanda.aspx.cs" Inherits="_Projetos_DadosProjeto_novaMensagem" UICulture="pt-BR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <title>Mensagens</title>
    <base target="_self" />
</head>
<body class="body">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="background-color: whitesmoke">
                    <dxe:ASPxLabel ID="lblNomeProjeto" runat="server" ClientInstanceName="lblNomeProjeto"
                        Font-Bold="True" >
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 457px" valign="top">
                    <dxe:ASPxLabel ID="lblMsg" runat="server" ClientInstanceName="lblMsg"
                        Text="Mensagem">
                    </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxe:ASPxMemo ID="txtMensagem" runat="server" ClientInstanceName="txtMensagem" Height="70px"
                        Width="665px" >
                        <ClientSideEvents Init="function(s, e) {
	return setMaxLength(s.GetInputElement(), 2000);
}" />
                    </dxe:ASPxMemo>
                    <dxe:ASPxLabel ID="lblCantCarater" runat="server" ClientInstanceName="lblCantCarater"
                         ForeColor="Silver" Text="0">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblDe250" runat="server" ClientInstanceName="lblDe250"
                        ForeColor="Silver" Text=" de 2000">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                                <dxe:ASPxCheckBox ID="ckbRespondeMsg" runat="server" ClientInstanceName="ckbRespondeMsg"
                                    Text="A mensagem deve ser respondida?" >
                                    <ClientSideEvents CheckedChanged="function(s, e) {
	dtePrazo.SetEnabled(s.GetChecked());
//	pnData.PerformCallback(s.GetChecked());
}" />
                                </dxe:ASPxCheckBox>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
                <td>
                                <dxe:ASPxLabel ID="lblPrazoResposta" runat="server" 
                                    Text="Prazo para resposta">
                                </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                                <dxe:ASPxDateEdit ID="dtePrazo" runat="server" ClientInstanceName="dtePrazo" EditFormatString="dd/MM/yyyy"  Width="193px" ClientEnabled="False" EditFormat="Custom" EnableClientSideAPI="True">
                                    <CalendarProperties>
                                        <DayHeaderStyle  />
                                        <WeekNumberStyle >
                                        </WeekNumberStyle>
                                        <DayStyle  />
                                        <DayOtherMonthStyle >
                                        </DayOtherMonthStyle>
                                        <DayWeekendStyle >
                                        </DayWeekendStyle>
                                        <DayOutOfRangeStyle >
                                        </DayOutOfRangeStyle>
                                        <HeaderStyle  />
                                        <Style ></Style>
                                    </CalendarProperties>
                                </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
                <td>
                    <dxrp:ASPxRoundPanel ID="rdpDestinatarios" runat="server" ClientInstanceName="rdpDestinatarios"
                        ContentHeight="155px" EnableClientSideAPI="True" 
                        HeaderText="Destinatários" Width="665px">
                        <HeaderStyle Height="7px" />
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 643px">
                        <tr>
                            <td style="width: 297px; height: 16px" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" 
                                    Text="Dispon&#237;veis">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="center" style="width: 50px; height: 16px">
                            </td>
                            <td style="height: 16px; width: 296px;" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" 
                                    Text="Selecionados">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 297px" valign="top">
                                <dxe:ASPxListBox ID="lbDisponiveis" runat="server" ClientInstanceName="lbDisponiveis"
                                   
                                    ImageFolder="~/App_Themes/Glass/{0}/" Rows="6" Width="290px" SelectionMode="Multiple">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}" Init="function(s, e) {
	UpdateButtons();
}" />
                                    <ItemStyle  Wrap="True" />
                                </dxe:ASPxListBox>
                            </td>
                            <td align="center" style="width: 50px">
                                <table border="0" cellpadding="0" cellspacing="0" style="height: 110px">
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="btnADDTodos" runat="server" AutoPostBack="False" ClientInstanceName="btnADDTodos"
                                                 Text="&gt;&gt;" Width="40px">
                                                <ClientSideEvents Click="function(s, e){
	lb_moveTodosItens(lbDisponiveis,lbSelecionados);
	UpdateButtons();
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="btnADD" runat="server" AutoPostBack="False" ClientInstanceName="btnADD"
                                                 Text="&gt;" Width="40px">
                                                <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbDisponiveis, lbSelecionados);
	UpdateButtons();
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="btnRMV" runat="server" AutoPostBack="False" ClientInstanceName="btnRMV"
                                                 Text="&lt;" Width="40px">
                                                <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbSelecionados, lbDisponiveis);
	UpdateButtons();
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="btnRMVTodos" runat="server" AutoPostBack="False" ClientInstanceName="btnRMVTodos"
                                                 Text="&lt;&lt;" Width="40px">
                                                <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbSelecionados,lbDisponiveis);
	UpdateButtons();
}" />
                                                <Paddings Padding="0px" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" style="width: 296px">
                                <dxe:ASPxListBox ID="lbSelecionados" runat="server" ClientInstanceName="lbSelecionados"
                                   
                                    ImageFolder="~/App_Themes/Glass/{0}/" Rows="6" Width="290px" SelectionMode="Multiple">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	UpdateButtons();
}" />
                                    <ValidationSettings>
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png" Width="14px" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ItemStyle  Wrap="True" />
                                </dxe:ASPxListBox>
                                <dxhf:ASPxHiddenField ID="hfCount" runat="server" ClientInstanceName="hfCount">
                                </dxhf:ASPxHiddenField>
                            </td>
                        </tr>
                    </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td align="right">
                                <dxe:ASPxButton ID="btnSalvar" runat="server" ClientInstanceName="btnSalvar" Height="5px"
                                    Text="Salvar" Width="90px" OnClick="btnSalvar_Click" >
                                    <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                                    <ClientSideEvents Click="function(s, e) 
{
    var valida = validaMensagem(); 	
	if(valida == &quot;&quot;)
	{
        e.processOnServer = true;
		
	}
	else
	{
		window.top.mostraMensagem(valida, 'atencao', true, false, null);
		e.processOnServer = false;
	}
}" />
                                </dxe:ASPxButton>
                            </td>
                            <td align="right">
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnCancelar" runat="server" ClientInstanceName="btnCancelar"
                                    Height="1px" Text="Fechar" Width="90px" >
                                    <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                                    <ClientSideEvents Click="function(s, e) 
{
	e.processOnServer = false;
	window.parent.fechaModal();
	try
	{
	     this.parent.src = 'editaMensagens.aspx';
	     this.parent.framePrincipal.carregaGrid(); 
	}
	catch(e)
	{
	    
	}
}" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
                    <dxhf:ASPxHiddenField ID="hiddenField" runat="server" ClientInstanceName="hiddenField">
                    </dxhf:ASPxHiddenField>
        <dxhf:aspxhiddenfield id="hfGeral" runat="server" clientinstancename="hfGeral"></dxhf:aspxhiddenfield>
    
    </div>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">
    try {
        var oMyObject = window.parent.myObject;

        var nomeProjeto = oMyObject.nomeProjeto;
        lblNomeProjeto.SetText(nomeProjeto);
    } catch (e) {
        lblNomeProjeto.SetText("");
    }
</script>
