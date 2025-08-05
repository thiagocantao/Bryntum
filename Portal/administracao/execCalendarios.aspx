<%@ Page Language="C#" AutoEventWireup="true" CodeFile="execCalendarios.aspx.cs" Inherits="administracao_execCalendarios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>

    <title>Cadastro de Exceções</title>
    <style>
        .btn{
            text-transform:capitalize !important;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" 
                            Text="Exceção:">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txtExcecao" runat="server" 
                            Width="100%" ClientInstanceName="txtExcecao">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                                <RequiredField IsRequired="True" ErrorText="Campo obrigatório !"></RequiredField>
                            </ValidationSettings>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 110px">
                                    <dxe:ASPxLabel ID="ASPxLabel13" runat="server" 
                                        Text="De:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 110px">
                                    <dxe:ASPxLabel ID="ASPxLabel14" runat="server" 
                                        Text="Até:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 110px">
                                    <dxe:ASPxDateEdit ID="txtDe" runat="server" 
                                        Width="120px" DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" ClientInstanceName="txtDe" EditFormat="Custom" EditFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                                        <ClientSideEvents DateChanged="function(s, e) {
         txtAte.SetDate(s.GetValue());
}" Init="function(s, e) {
	txtAte.SetMinDate(s.GetValue());
}"></ClientSideEvents>

                                        <CalendarProperties TodayButtonText="Hoje" ShowClearButton="False"></CalendarProperties>
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td style="width: 110px">
                                    <dxe:ASPxDateEdit ID="txtAte" runat="server" 
                                        Width="120px" DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>" ClientInstanceName="txtAte" AllowUserInput="True" EditFormat="Custom" EditFormatString="dd/MM/yyyy" UseMaskBehavior="True">
                                        <CalendarProperties ShowClearButton="False" TodayButtonText="Hoje">
                                        </CalendarProperties>
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False"
                                        Text="Selecionar" ValidationGroup="MKE" CssClass="btn">
                                        <Paddings Padding="0px" />
                                        <ClientSideEvents Click="function(s, e) {
	pnCallback.PerformCallback();
}" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <dxcp:ASPxCallbackPanel ID="pnCallback" runat="server" ClientInstanceName="pnCallback"
                            Width="100%">
                            <ClientSideEvents EndCallback="function(s, e) {
	ASPxClientEdit.ValidateGroup('MKE', true);
}" />
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <!-- TABLE CALENDARIO -->
                                    <table style="margin-bottom:5px;"
                                        id="tbCalendario" cellspacing="0" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="width: 75px; height: 18px" align="center"></td>
                                                <td style="width: 150px; height: 18px; background-color: #ffffff" align="center">
                                                    <dxe:ASPxLabel runat="server" Text="Turno 1"  ID="ASPxLabel1"></dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 150px; height: 18px" align="center">
                                                    <dxe:ASPxLabel runat="server" Text="Turno 2"  ID="ASPxLabel2"></dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 150px; height: 18px; background-color: #ffffff" align="center">
                                                    <dxe:ASPxLabel runat="server" Text="Turno 3"  ID="ASPxLabel3"></dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 150px; height: 18px" align="center">
                                                    <dxe:ASPxLabel runat="server" Text="Turno 4"  ID="ASPxLabel4"></dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr id="tr1">
                                                <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="Segunda"  ID="ASPxLabel5"></dxe:ASPxLabel>
                                                    &nbsp;&nbsp;</td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Ini_1" ID="txt_Seg_Ini_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == '__:__' || s.GetText() == ':' || s.GetText() == '')
		e.isValid = true;
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Seg_Ini_1, txt_Seg_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ErrorText="Hora Inv&#225;lida" ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Term_1" ID="txt_Seg_Term_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Seg_Ini_1, txt_Seg_Term_1);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Seg_Ini_1, txt_Seg_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Ini_2" ID="txt_Seg_Ini_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Seg_Term_1, txt_Seg_Ini_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Seg_Ini_2, txt_Seg_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Term_2" ID="txt_Seg_Term_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Seg_Ini_2, txt_Seg_Term_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Seg_Ini_2, txt_Seg_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Ini_3" ID="txt_Seg_Ini_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Seg_Term_2, txt_Seg_Ini_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Seg_Ini_3, txt_Seg_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Term_3" ID="txt_Seg_Term_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Seg_Ini_3, txt_Seg_Term_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Seg_Ini_3, txt_Seg_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Ini_4" ID="txt_Seg_Ini_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Seg_Term_3, txt_Seg_Ini_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Seg_Ini_4, txt_Seg_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Term_4" ID="txt_Seg_Term_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Seg_Ini_4, txt_Seg_Term_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Seg_Ini_4, txt_Seg_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="tr2">
                                                <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="Ter&#231;a"  ID="ASPxLabel6"></dxe:ASPxLabel>
                                                    &nbsp; </td>
                                               <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Ini_1" ID="txt_Ter_Ini_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == '__:__' || s.GetText() == ':' || s.GetText() == ';')
		e.isValid = true;
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Ter_Ini_1, txt_Ter_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Term_1" ID="txt_Ter_Term_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Ter_Ini_1, txt_Ter_Term_1);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Ter_Ini_1, txt_Ter_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Ini_2" ID="txt_Ter_Ini_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Ter_Term_1, txt_Ter_Ini_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Ter_Ini_2, txt_Ter_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Term_2" ID="txt_Ter_Term_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Ter_Ini_2, txt_Ter_Term_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Ter_Ini_2, txt_Ter_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                               <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Ini_3" ID="txt_Ter_Ini_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Ter_Term_2, txt_Ter_Ini_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Ter_Ini_3, txt_Ter_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Term_3" ID="txt_Ter_Term_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Ter_Ini_3, txt_Ter_Term_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Ter_Ini_3, txt_Ter_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Ini_4" ID="txt_Ter_Ini_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Ter_Term_3, txt_Ter_Ini_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Ter_Ini_4, txt_Ter_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Term_4" ID="txt_Ter_Term_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Ter_Ini_4, txt_Ter_Term_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Ter_Ini_4, txt_Ter_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="tr3">
                                                <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="Quarta"  ID="ASPxLabel7"></dxe:ASPxLabel>
                                                    &nbsp; </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Ini_1" ID="txt_Qua_Ini_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == '__:__' || s.GetText() == ':' || s.GetText() == '')
		e.isValid = true;
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qua_Ini_1, txt_Qua_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Term_1" ID="txt_Qua_Term_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Qua_Ini_1, txt_Qua_Term_1);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qua_Ini_1, txt_Qua_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Ini_2" ID="txt_Qua_Ini_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Qua_Term_1, txt_Qua_Ini_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qua_Ini_2, txt_Qua_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Term_2" ID="txt_Qua_Term_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Qua_Ini_2, txt_Qua_Term_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qua_Ini_2, txt_Qua_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                               <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Ini_3" ID="txt_Qua_Ini_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Qua_Term_2, txt_Qua_Ini_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qua_Ini_3, txt_Qua_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Term_3" ID="txt_Qua_Term_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Qua_Ini_3, txt_Qua_Term_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qua_Ini_3, txt_Qua_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                               <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Ini_4" ID="txt_Qua_Ini_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Qua_Term_3, txt_Qua_Ini_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qua_Ini_4, txt_Qua_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Term_4" ID="txt_Qua_Term_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Qua_Ini_4, txt_Qua_Term_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qua_Ini_4, txt_Qua_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="tr4">
                                                <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="Quinta"  ID="ASPxLabel8"></dxe:ASPxLabel>
                                                    &nbsp; </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Ini_1" ID="txt_Qui_Ini_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == '__:__' || s.GetText() == ':' || s.GetText() == '')
		e.isValid = true;
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qui_Ini_1, txt_Qui_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Term_1" ID="txt_Qui_Term_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Qui_Ini_4, txt_Qui_Term_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qui_Ini_1, txt_Qui_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" ClientInstanceName="txt_Qui_Ini_2" ID="txt_Qui_Ini_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Qui_Term_1, txt_Qui_Ini_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qui_Ini_2, txt_Qui_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Term_2" ID="txt_Qui_Term_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Qui_Ini_2, txt_Qui_Term_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qui_Ini_2, txt_Qui_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Ini_3" ID="txt_Qui_Ini_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Qui_Term_2, txt_Qui_Ini_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qui_Ini_3, txt_Qui_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Term_3" ID="txt_Qui_Term_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Qui_Ini_3, txt_Qui_Term_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qui_Ini_3, txt_Qui_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Ini_4" ID="txt_Qui_Ini_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Qui_Term_3, txt_Qui_Ini_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qui_Ini_4, txt_Qui_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Term_4" ID="txt_Qui_Term_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Qui_Ini_4, txt_Qui_Term_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Qui_Ini_4, txt_Qui_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="tr5">
                                                <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="Sexta"  ID="ASPxLabel9"></dxe:ASPxLabel>
                                                    &nbsp; </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Ini_1" ID="txt_Sex_Ini_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == '__:__' || s.GetText() == ':' || s.GetText() == '')
		e.isValid = true;
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sex_Ini_1, txt_Sex_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Term_1" ID="txt_Sex_Term_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Sex_Ini_1, txt_Sex_Term_1);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sex_Ini_1, txt_Sex_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Ini_2" ID="txt_Sex_Ini_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Sex_Term_1, txt_Sex_Ini_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sex_Ini_2, txt_Sex_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Term_2" ID="txt_Sex_Term_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Sex_Ini_2, txt_Sex_Term_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sex_Ini_2, txt_Sex_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Ini_3" ID="txt_Sex_Ini_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Sex_Term_2, txt_Sex_Ini_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sex_Ini_3, txt_Sex_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Term_3" ID="txt_Sex_Term_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Sex_Ini_3, txt_Sex_Term_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sex_Ini_3, txt_Sex_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Ini_4" ID="txt_Sex_Ini_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Sex_Term_3, txt_Sex_Ini_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sex_Ini_4, txt_Sex_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Term_4" ID="txt_Sex_Term_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Sex_Ini_4, txt_Sex_Term_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sex_Ini_4, txt_Sex_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="tr6">
                                                <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="S&#225;bado"  ID="ASPxLabel10"></dxe:ASPxLabel>
                                                    &nbsp; </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Ini_1" ID="txt_Sab_Ini_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == '__:__' || s.GetText() == ':' || s.GetText() == '')
		e.isValid = true;
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sab_Ini_1, txt_Sab_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Term_1" ID="txt_Sab_Term_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Sab_Ini_1, txt_Sab_Term_1);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sab_Ini_1, txt_Sab_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Ini_2" ID="txt_Sab_Ini_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Sab_Term_1, txt_Sab_Ini_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sab_Ini_2, txt_Sab_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Term_2" ID="txt_Sab_Term_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Sab_Ini_2, txt_Sab_Term_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sab_Ini_2, txt_Sab_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                               <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Ini_3" ID="txt_Sab_Ini_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Sab_Term_2, txt_Sab_Ini_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sab_Ini_3, txt_Sab_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Term_3" ID="txt_Sab_Term_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Sab_Ini_3, txt_Sab_Term_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sab_Ini_3, txt_Sab_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Ini_4" ID="txt_Sab_Ini_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Sab_Term_3, txt_Sab_Ini_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sab_Ini_4, txt_Sab_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Term_4" ID="txt_Sab_Term_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Sab_Ini_4, txt_Sab_Term_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Sab_Ini_4, txt_Sab_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="tr0">
                                                <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="Domingo"  ID="ASPxLabel11"></dxe:ASPxLabel>
                                                    &nbsp; </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Ini_1" ID="txt_Dom_Ini_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == '__:__' || s.GetText() == ':' || s.GetText() == '')
		e.isValid = true;
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Dom_Ini_1, txt_Dom_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Term_1" ID="txt_Dom_Term_1">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Dom_Ini_1, txt_Dom_Term_1);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Dom_Ini_1, txt_Dom_Term_1);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Ini_2" ID="txt_Dom_Ini_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Dom_Term_1, txt_Dom_Ini_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Dom_Ini_2, txt_Dom_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Term_2" ID="txt_Dom_Term_2">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Dom_Ini_2, txt_Dom_Term_2);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Dom_Ini_2, txt_Dom_Term_2);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Ini_3" ID="txt_Dom_Ini_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Dom_Term_2, txt_Dom_Ini_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Dom_Ini_3, txt_Dom_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Term_3" ID="txt_Dom_Term_3">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Dom_Ini_3, txt_Dom_Term_3);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Dom_Ini_3, txt_Dom_Term_3);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td class="excecoesCalendar" align="center">
                                                    <table cellspacing="2" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Ini_4" ID="txt_Dom_Ini_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarTurno(txt_Dom_Term_3, txt_Dom_Ini_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Dom_Ini_4, txt_Dom_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 60px" align="center">
                                                                    <dxe:ASPxTextBox runat="server" Width="55px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Term_4" ID="txt_Dom_Term_4">
                                                                        <ClientSideEvents Validation="function(s, e) {
	e.isValid = validarHorario(txt_Dom_Ini_4, txt_Dom_Term_4);
}"
                                                                            ValueChanged="function(s, e) {
	verificarTurnoPequenhos(txt_Dom_Ini_4, txt_Dom_Term_4);
}"></ClientSideEvents>

                                                                        <MaskSettings Mask="99:99"></MaskSettings>

                                                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                                                            <RegularExpression ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <!-- FIM TABLE CALENDARIO -->
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxCallbackPanel>
                    </td>
                </tr>
                <tr >
                    <td>
                        <dxrp:ASPxRoundPanel ID="pLegenda" runat="server" 
                            HeaderText="Legenda" HorizontalAlign="Left" Width="100%"
                            ShowHeader="False">
                            <ContentPaddings Padding="1px"></ContentPaddings>

                            <HeaderStyle HorizontalAlign="Left" Font-Bold="True">
                                <Paddings Padding="1px" PaddingLeft="3px"></Paddings>
                            </HeaderStyle>
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td style="width: 16px;  background-color: #ff9933; border-radius:18px; height:16px;"></td>
                                                <td style="padding-right: 10px; padding-left: 3px; font-size:12px;">
                                                    <asp:Label runat="server" Text="<%$ Resources:traducao, frm_Calendarios_turno_com_mesma_hora %>"  ID="Label1"></asp:Label>
                                                </td>
                                                <td style="width: 16px; background-color: red; border-radius:18px; height:16px;"></td>
                                                <td style="padding-right: 10px; padding-left: 3px; font-size:12px;">
                                                    <asp:Label runat="server" Text="Erro."  ID="Label2"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>
                <tr >
                    <td>
                        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                            <ClientSideEvents EndCallback="function(s, e) {
       if(s.cp_MSG != '')
       {
                   window.top.mostraMensagem(s.cp_MSG, 'sucesso', false, false, null);
                   window.parent.finalizaExcecao();
       }
        else if(s.cp_Erro != '')
       {
                 window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
       }
}" />
                        </dxcb:ASPxCallback>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
