<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalhesTS.aspx.cs" Inherits="espacoTrabalho_DetalhesTS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Detalhes</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 579px;
        }
        .style2
        {
            height: 4px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <dxtc:ASPxPageControl ID="pcDados" runat="server" ActiveTabIndex="0" ClientInstanceName="pcDados"
                     Width="100%">
                    <TabPages>
                        <dxtc:TabPage Name="TabD" Text="Detalhe">
                            <ContentCollection>
                                <dxw:ContentControl runat="server">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Projeto:" ClientInstanceName="lblProjeto"
                                                        ID="lblProjeto">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtProjeto" ClientEnabled="False"
                                                         ID="txtProjeto">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Tarefa:" ClientInstanceName="lblTarefa"
                                                        ID="lblTarefa">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtTarefa" ClientEnabled="False"
                                                         ID="txtTarefa">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Tarefa Superior:" ClientInstanceName="lblTarefaSuperior"
                                                         ID="lblTarefaSuperior">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtTarefaSuperior"
                                                        ClientEnabled="False"  ID="txtTarefaSuperior">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="In&#237;cio Previsto:" 
                                                                        ID="ASPxLabel1">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="T&#233;rmino Previsto:"
                                                                        ID="ASPxLabel2">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Trabalho Previsto (h):" ClientInstanceName="lblTrabalhoPrevisto"
                                                                         ID="lblTrabalhoPrevisto">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server" ClientInstanceName="lblTarefaSuperior"
                                                                         Text="Aprovador:">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="txtInicioPrevisto" runat="server" ClientEnabled="False" ClientInstanceName="txtInicioPrevisto"
                                                                        DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                        EncodeHtml="False"  UseMaskBehavior="True"
                                                                        Width="110px">
                                                                        <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
 

                                                                            <DayHeaderStyle ></DayHeaderStyle>
                                                                            <WeekNumberStyle >
                                                                            </WeekNumberStyle>
                                                                            <DayStyle ></DayStyle>
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
                                                                            <HeaderStyle ></HeaderStyle>
                                                                            <FooterStyle ></FooterStyle>
                                                                            <FastNavMonthAreaStyle >
                                                                            </FastNavMonthAreaStyle>
                                                                            <InvalidStyle >
                                                                            </InvalidStyle>
                                                                            <Style ></Style>
                                                                        </CalendarProperties>
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="txtTerminoPrevisto" runat="server" ClientEnabled="False" ClientInstanceName="txtTerminoPrevisto"
                                                                        DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                        EncodeHtml="False"  UseMaskBehavior="True"
                                                                        Width="110px">
                                                                        <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
 

                                                                            <DayHeaderStyle ></DayHeaderStyle>
                                                                            <WeekNumberStyle >
                                                                            </WeekNumberStyle>
                                                                            <DayStyle ></DayStyle>
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
                                                                            <HeaderStyle ></HeaderStyle>
                                                                            <FooterStyle ></FooterStyle>
                                                                            <FastNavMonthAreaStyle >
                                                                            </FastNavMonthAreaStyle>
                                                                            <InvalidStyle >
                                                                            </InvalidStyle>
                                                                            <Style ></Style>
                                                                        </CalendarProperties>
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="txtTabalhoPrevisto" runat="server" ClientEnabled="False" ClientInstanceName="txtTabalhoPrevisto"
                                                                        DecimalPlaces="2" DisplayFormatString="{0:n2}" 
                                                                        Number="0" Width="130px">
                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                        </SpinButtons>
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtIndicaMarco" runat="server" ClientEnabled="False" ClientInstanceName="txtIndicaMarco"
                                                                        ClientVisible="False"  Width="100px">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="txtInicio" runat="server" ClientEnabled="False" ClientInstanceName="txtInicio"
                                                                        ClientVisible="False" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                        EncodeHtml="False"  UseMaskBehavior="True"
                                                                        Width="110px">
                                                                        <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
 

                                                                            <DayHeaderStyle ></DayHeaderStyle>
                                                                            <WeekNumberStyle >
                                                                            </WeekNumberStyle>
                                                                            <DayStyle ></DayStyle>
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
                                                                            <HeaderStyle ></HeaderStyle>
                                                                            <FooterStyle ></FooterStyle>
                                                                            <FastNavMonthAreaStyle >
                                                                            </FastNavMonthAreaStyle>
                                                                            <InvalidStyle >
                                                                            </InvalidStyle>
                                                                            <Style ></Style>
                                                                        </CalendarProperties>
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxDateEdit ID="txtTermino" runat="server" ClientEnabled="False" ClientInstanceName="txtTermino"
                                                                        ClientVisible="False" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                        EncodeHtml="False"  UseMaskBehavior="True"
                                                                        Width="110px">
                                                                        <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
 

                                                                            <DayHeaderStyle ></DayHeaderStyle>
                                                                            <WeekNumberStyle >
                                                                            </WeekNumberStyle>
                                                                            <DayStyle ></DayStyle>
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
                                                                            <HeaderStyle ></HeaderStyle>
                                                                            <FooterStyle ></FooterStyle>
                                                                            <FastNavMonthAreaStyle >
                                                                            </FastNavMonthAreaStyle>
                                                                            <InvalidStyle >
                                                                            </InvalidStyle>
                                                                            <Style ></Style>
                                                                        </CalendarProperties>
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxDateEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="txtTrabalho" runat="server" ClientEnabled="False" ClientInstanceName="txtTrabalho"
                                                                        ClientVisible="False" DecimalPlaces="2" DisplayFormatString="{0:n2}"
                                                                        Number="0" Width="130px">
                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                        </SpinButtons>
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                        </DisabledStyle>
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="txtAprovador" runat="server" ClientEnabled="False" ClientInstanceName="txtAprovador"
                                                                         Width="300px">
                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="Anota&#231;&#245;es do Gerente:" ClientInstanceName="lblAnotacoes1"
                                                         ID="ASPxLabel5">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxMemo runat="server" Width="100%" ClientInstanceName="mmAnotacoes" ClientEnabled="False"
                                                        ID="mmAnotacoes"  Rows="8">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td1">
                                                    <fieldset>
                                                        <legend>Execução da Tarefa</legend>
                                                        <table style="margin-top: 5px" cellspacing="0" cellpadding="0" border="0" class="style1">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 80px">
                                                                        <dxe:ASPxLabel runat="server" Text="% Concluido:" ClientInstanceName="lblPorcentaje"
                                                                             ID="lblPorcentaje">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <dxe:ASPxLabel runat="server" Text="In&#237;cio Real:" 
                                                                            ID="ASPxLabel3">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <dxe:ASPxLabel runat="server" Text="T&#233;rmino Real:" 
                                                                            ID="ASPxLabel4">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 110px">
                                                                        <dxe:ASPxLabel runat="server" Text="Trabalho Real (h):" ClientInstanceName="lblTrabalhoReal"
                                                                             ID="lblTrabalhoReal">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td style="width: 140px">
                                                                        <dxe:ASPxLabel runat="server" Text="Trabalho Restante (h):" ClientInstanceName="lblTrabalhoRestante"
                                                                             ID="lblTrabalhoRestante">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit ID="txtPorcentaje" runat="server" ClientInstanceName="txtPorcentaje"
                                                                            DisplayFormatString="{0:n0}"  Number="0"
                                                                            Width="100%" AllowMouseWheel="False" MaxValue="100">
                                                                            <SpinButtons ShowIncrementButtons="False">
                                                                            </SpinButtons>
                                                                            <ClientSideEvents GotFocus="function(s, e) {
	keypressPorcentual = &quot;&quot;;
}" KeyPress="function(s, e) {
	keypressPorcentual = &quot;1&quot;;
}" LostFocus="function(s, e) {
	e.processOnServer = false;
	if(keypressPorcentual == &quot;1&quot;)
		verificarPorcentual();
}" />
                                                                            <ClientSideEvents KeyPress="function(s, e) {
	keypressPorcentual = &quot;1&quot;;
}" GotFocus="function(s, e) {
	keypressPorcentual = &quot;&quot;;
}" LostFocus="function(s, e) {
	e.processOnServer = false;
	if(keypressPorcentual == &quot;1&quot;)
		verificarPorcentual();
}"></ClientSideEvents>
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                            EncodeHtml="False" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlInicioReal"
                                                                             ID="ddlInicioReal">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
 

                                                                                <DayHeaderStyle ></DayHeaderStyle>
                                                                                <WeekNumberStyle >
                                                                                </WeekNumberStyle>
                                                                                <DayStyle ></DayStyle>
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
                                                                                <HeaderStyle ></HeaderStyle>
                                                                                <FooterStyle ></FooterStyle>
                                                                                <FastNavMonthAreaStyle >
                                                                                </FastNavMonthAreaStyle>
                                                                                <InvalidStyle >
                                                                                </InvalidStyle>
                                                                                <Style ></Style>
                                                                            </CalendarProperties>
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                                                            EncodeHtml="False" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlTerminoReal"
                                                                             ID="ddlTerminoReal">
                                                                            <ClientSideEvents KeyPress="function(s, e) {
	keypressTerminoReal = &quot;1&quot;;
}" GotFocus="function(s, e) {
	keypressTerminoReal = &quot;&quot;;
}" LostFocus="function(s, e) {
	e.processOnServer = false;
	if(keypressTerminoReal == &quot;1&quot;)
		verificarTerminoReal();
}" ValueChanged="function(s, e) {
	keypressTerminoReal = &quot;1&quot;;
}"></ClientSideEvents>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black" >
                                                                            </DisabledStyle>
                                                                            <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
 

                                                                                <DayHeaderStyle ></DayHeaderStyle>
                                                                                <WeekNumberStyle >
                                                                                </WeekNumberStyle>
                                                                                <DayStyle ></DayStyle>
                                                                                <DayOtherMonthStyle >
                                                                                </DayOtherMonthStyle>
                                                                                <DayWeekendStyle >
                                                                                </DayWeekendStyle>
                                                                                <TodayStyle >
                                                                                </TodayStyle>
                                                                                <ButtonStyle >
                                                                                </ButtonStyle>
                                                                                <HeaderStyle ></HeaderStyle>
                                                                                <FooterStyle ></FooterStyle>
                                                                                <Style ></Style>
                                                                            </CalendarProperties>
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit ID="txtTrabalhoReal" runat="server" ClientInstanceName="txtTrabalhoReal"
                                                                            DecimalPlaces="2" DisplayFormatString="{0:n2}" 
                                                                            Number="0" Width="100%" AllowMouseWheel="False" MaxValue="99999999">
                                                                            <SpinButtons ShowIncrementButtons="False">
                                                                            </SpinButtons>
                                                                            <ClientSideEvents GotFocus="function(s, e) {
	keypressTrabalhoReal = &quot;&quot;;
}" KeyPress="function(s, e) {
	keypressTrabalhoReal = &quot;1&quot;;
}" LostFocus="function(s, e) {
	e.processOnServer = false;
	if(keypressTrabalhoReal == &quot;1&quot;)
		verificarTrabalhoReal();
}" />
                                                                            <ClientSideEvents KeyPress="function(s, e) {
	keypressTrabalhoReal = &quot;1&quot;;
}" GotFocus="function(s, e) {
	keypressTrabalhoReal = &quot;&quot;;
	valorTrabalhoReal = s.GetValue();
}" LostFocus="function(s, e) {
	e.processOnServer = false;
	if(keypressTrabalhoReal == &quot;1&quot;)
		verificarTrabalhoReal();
}"></ClientSideEvents>
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit ID="txtTrabalhoRestante" runat="server" ClientInstanceName="txtTrabalhoRestante"
                                                                            DecimalPlaces="2" DisplayFormatString="{0:n2}" 
                                                                            Number="0" Width="100%" AllowMouseWheel="False" MaxValue="99999999">
                                                                            <SpinButtons ShowIncrementButtons="False">
                                                                            </SpinButtons>
                                                                            <ClientSideEvents GotFocus="function(s, e) {
	keypressTrabalhoRestante = &quot;&quot;;
}" Init="function(s, e) {
	trabalhoRestanteReal = s.GetValue();
}" KeyPress="function(s, e) {
	keypressTrabalhoRestante = &quot;1&quot;;
}" LostFocus="function(s, e) {
	e.processOnServer = false;
	if(keypressTrabalhoRestante == &quot;1&quot;)
		verificarTrabalhoRestante();
}" />
                                                                            <ClientSideEvents KeyPress="function(s, e) {
	keypressTrabalhoRestante = &quot;1&quot;;
}" GotFocus="function(s, e) {
	keypressTrabalhoRestante = &quot;&quot;;
}" LostFocus="function(s, e) {
	e.processOnServer = false;
	if(keypressTrabalhoRestante == &quot;1&quot;)
		verificarTrabalhoRestante();
}" Init="function(s, e) {
	trabalhoRestanteReal = s.GetValue();
}" ValueChanged="function(s, e) {
	mudouRestante = s.GetValue() != trabalhoRestanteReal;
}"></ClientSideEvents>
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table style="margin-bottom: 10px" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="Coment&#225;rios Recurso:" ClientInstanceName="lblComentarioRecurso"
                                                                             ID="lblAnotacoes">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxMemo runat="server" Rows="7" Width="100%" ClientInstanceName="mmComentariosRecurso"
                                                                             ID="mmComentariosRecurso">
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="TabA" Text="Coment&#225;rios">
                            <ContentCollection>
                                <dxw:ContentControl runat="server">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackAnotacoes" Width="100%"
                                                        ID="pnCallbackAnotacoes3">
                                                        <PanelCollection>
                                                            <dxp:PanelContent runat="server">
                                                                <iframe id="frmComentarios" frameborder="0" name="frmComentarios" scrolling="no"
                                                                    style="width: 100%; height: 489px"></iframe>
                                                            </dxp:PanelContent>
                                                        </PanelCollection>
                                                    </dxcp:ASPxCallbackPanel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="tabAnexos" Text="Anexos">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <iframe id="frameAnexos" frameborder="0" scrolling="no" style="width: 100%; height: 489px">
                                    </iframe>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="function(s, e) {
	var tab = pcDados.GetActiveTab();
	if(e.tab.name=='TabD')
		btnSalvar.SetVisible(true);
     else if(e.tab.name == &quot;tabAnexos&quot; &amp;&amp; document.getElementById('frameAnexos').src == &quot;&quot;)
     {
		document.getElementById('frameAnexos').src = s.cp_URLAnexos;
        btnSalvar.SetVisible(false);
     }  
     else if(e.tab.name == &quot;TabA&quot; &amp;&amp; document.getElementById('frmComentarios').src == &quot;&quot;)
     {
		document.getElementById('frmComentarios').src = s.cp_URLComentarios;
        btnSalvar.SetVisible(false);
     }  
     else
		btnSalvar.SetVisible(false);
}"></ClientSideEvents>
                </dxtc:ASPxPageControl>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="right" style="padding-right: 5px; padding-top: 5px;">
                <table>
                    <tr>
                        <td>
                            <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                 Text="Salvar" Width="90px">
                                <ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
	                    if(verificarDadosPreenchidos())
						{
							callBack.PerformCallback();
                        	//window.top.fechaModal();
						}
                    }"></ClientSideEvents>
                                <Paddings Padding="0px"></Paddings>
                            </dxe:ASPxButton>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                 Text="Fechar" Width="90px">
                                <Paddings Padding="0px"></Paddings>
                                <ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
						window.top.retornoModal = 'N';
                        window.top.fechaModal();
                    }"></ClientSideEvents>
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <dxcb:ASPxCallback ID="callBack" runat="server" ClientInstanceName="callBack" OnCallback="callBack_Callback">
        <ClientSideEvents EndCallback="function(s, e) {
        if(s.cp_OK != '')
        {
                    window.top.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
         window.top.retornoModal = 'S';
         window.top.fechaModal();
        }
        else
        {
                   if(s.cp_Erro != '')
                  {
                             window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
                  }
       }	
}"></ClientSideEvents>
    </dxcb:ASPxCallback>
    </form>
</body>
</html>
