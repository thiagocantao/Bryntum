<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Calendarios.aspx.cs" Inherits="administracao_CalendarioRecurso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Cadastro de Calendários</title>

    <style type="text/css">
        html .dxeCalendarSelected_MaterialCompact, .dxeCalendarToday_MaterialCompact {
            background-repeat: no-repeat;
            background-position: center !important;
            background-size: auto !important;
        }

        html .dxtcLite_MaterialCompact > .dxtc-content {
            padding: 0px !important;
        }

        .btn {
            text-transform: capitalize !important;
        }

        .style6 {
            width: 75px;
            /* height: 29px;*/
        }

        .style7 {
            width: 150px;
            /*  height: 30px; */
        }

        .style8 {
            width: 75px;
            /*  height: 24px;*/
        }

        .style11 {
            width: 150px;
            /*  height: 24px;*/
        }

        .style12 {
            width: 75px;
            /*   height: 23px;*/
        }

        .style13 {
            width: 150px;
            /*  height: 23px;*/
        }

        .style14 {
            width: 75px;
            /*   height: 26px;*/
        }
    </style>
    <script type="text/javascript" language="javascript">

        function calculaDisponibilidade() {
            callbackDisp.PerformCallback();
        }

        function abreEdicao(codigoExecao, codigoCalendario) {
            //document.getElementById('frm_Excecao').src = 'execCalendarios.aspx?CC=' + codigoCalendario + '&EX=' + codigoExecao;
            pcDados.SetContentUrl('execCalendarios.aspx?CC=' + codigoCalendario + '&EX=' + codigoExecao);
            pcDados.Show();
        }

        function abreInsercao(codigoCalendarioBase) {
            //document.getElementById('frm_Excecao').src = 'execCalendarios.aspx?CC=' + codigoCalendarioBase;
            pcDados.SetContentUrl('execCalendarios.aspx?CC=' + codigoCalendarioBase);
            pcDados.Show();
        }

        function fechaEdicao() {
            pcDados.SetContentUrl('../branco.htm');
            pcDados.Hide();
        }

        function finalizaExcecao() {
            pcDados.SetContentUrl('../branco.htm');
            pcDados.Hide();
            gvExcecao.PerformCallback();
        }

        function validarHorario(hsInicio, hsFinal) {
            var vacioHs1 = false;
            var vacioHs2 = false;

            //testar qeu tenha prenchido os dados.
            if (hsInicio.GetText() == "__:__" || hsInicio.GetText() == ":" || hsInicio.GetText() == "")
                vacioHs1 = true;
            if (hsFinal.GetText() == "__:__" || hsFinal.GetText() == ":" || hsFinal.GetText() == "")
                vacioHs2 = true

            if ((vacioHs1 != vacioHs2) && (vacioHs1 == true || vacioHs2 == true)) {

                return false; //um de los horarios esta vacio.
            }

            //testar qeu horario inicio seja menor ao horario final.
            var hora1 = hsInicio.GetText().split(':');
            var hora2 = hsFinal.GetText().split(':');

            // Obtener horas y minutos (hora 1)
            var hh1 = parseInt(hora1[0], 10);
            var mm1 = parseInt(hora1[1], 10);

            // Obtener horas y minutos (hora 2)
            var hh2 = parseInt(hora2[0], 10);
            var mm2 = parseInt(hora2[1], 10);

            if (hh1 > hh2) {

                return false; // hora inicio maior a hora final.
            }
            else if (hh1 == hh2) {
                if (mm1 > mm2) {

                    return false; // horas iguis, mais minutos do inicio maior ao minutos final.
                }
            }
            return true
        }

        function validarTurno(hsTerminoAnteriorT, hsInicioProximoT) {
            //isNaN(123) devolverá False
            //isNaN("prueba") devolverá True
            //Agora vejo que o inicio do siguente turno possiu o horario igual o maior.
            var hora1 = hsTerminoAnteriorT.GetText().split(':');
            var hora2 = hsInicioProximoT.GetText().split(':');

            // Obtener horas y minutos (hora 1)
            var hh1 = parseInt(hora1[0], 10);
            var mm1 = parseInt(hora1[1], 10);

            // Obtener horas y minutos (hora 2)
            var hh2 = parseInt(hora2[0], 10);
            var mm2 = parseInt(hora2[1], 10);

            //si termino de turno anterio vacio, e inicio proximo turno prenchido, ERRO.
            if (!isNaN(hh2)) {
                if (isNaN(hh1)) {

                    return false;
                }
            }

            if (hh1 > hh2) {

                return false; // hora termino de turno anterior maior a hora inicio do proximo turno.
            }
            else if (hh1 == hh2) {
                if (mm1 > mm2) {

                    return false; // horas iguais, mais minutos do termino de turno anterior maior ao minutos inicio proximo turno.
                }
            }

            return true;
        }

        function verificarTurnoPequenhos(horaInicio, horaFim) {
            var hora1 = horaInicio.GetText().split(':');
            var hora2 = horaFim.GetText().split(':');

            //Obtener horas y minutos (hora 1)
            var hh1 = parseInt(hora1[0], 10);
            var mm1 = parseInt(hora1[1], 10);

            //Obtener horas y minutos (hora 2)
            var hh2 = parseInt(hora2[0], 10);
            var mm2 = parseInt(hora2[1], 10);

            if (!isNaN(hh1)) {
                if (hh1 == hh2) {
                    trocarFondo(horaInicio.name, true);
                    trocarFondo(horaFim.name, true);
                }
                else {
                    trocarFondo(horaInicio.name, false);
                    trocarFondo(horaFim.name, false);
                }
            }
        }


        function trocarFondo(idObjeto, value) {
            if (value)
                document.getElementById(idObjeto).style.backgroundColor = "#FF9933";
            else
                document.getElementById(idObjeto).style.backgroundColor = "#FFFFFF";
        }

        function funcaoCallbackSalvar() {
            console.log('funcaoCallbackSalvar');
            if (ASPxClientEdit.ValidateGroup("MKE")) {
                callback.PerformCallback();
            }
            else {
                window.top.mostraMensagem(traducao.frm_Calendarios_existem_horas_no_formato_incorreto__n_n__hor_rio_dentro_de_um_turno_deve_ser_preenchido__in_cio___fim_ + '\n- ' + traducao.frm_Calendarios_a_hora_de_in_cio_n_o_pode_ser_maior_que_a_hora_de_t_rmino_do_turno_ + '\n- ' + traducao.frm_Calendarios_hora_de_in_cio_de_um_turno_implica_preencher_o_hor_rio_de_t_rmino_do_turno_anterior_ + '\n- ' + traducao.frm_Calendarios_hor_rio_de_t_rmino_de_um_turno_n_o_pode_ser_maior_que_o_hor_rio_de_in_cio_do_pr_ximo_turno_, 'Atencao', true, false, null);
            }
        }

        function funcaoCallbackFechar() {
            console.log('funcaoCallbackfechar');
            callbackProjCalend.PerformCallback();
        }
    </script>

</head>
<body style="margin: 0px; text-align: center;">
    <form id="form1" runat="server">
        <div style="display: none" runat="server" id="dividioma"><%=idioma%></div>
        <div>
            <table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="5" style="width: 100%">
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel12" runat="server"
                                        Text="Descrição:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 20px; text-align: center;">
                                    <dxe:ASPxLabel ID="ASPxLabel15" runat="server"
                                        Text="Horas/Dia:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 20px; text-align: center;">
                                    <dxe:ASPxLabel ID="ASPxLabel16" runat="server"
                                        Text="Horas/Semana:">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="width: 20px; text-align: center;">
                                    <dxe:ASPxLabel ID="ASPxLabel17" runat="server"
                                        Text="Dias/Mês:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxTextBox ID="txtDescricao" runat="server" Width="100%"
                                        ClientInstanceName="txtDescricao">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="#404040"></DisabledStyle>

                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                            <RequiredField IsRequired="True" ErrorText="Campo obrigat&#243;rio !"></RequiredField>
                                        </ValidationSettings>

                                        <MaskSettings IncludeLiterals="None"></MaskSettings>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td style="width: 20px; text-align: center;">
                                    <dxe:ASPxTextBox ID="txtHorasDia" runat="server" Width="100%"
                                        ClientInstanceName="txtHorasDia" HorizontalAlign="Center">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="#404040"></DisabledStyle>

                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                            <RequiredField IsRequired="True" ErrorText="Campo obrigat&#243;rio !"></RequiredField>
                                        </ValidationSettings>

                                        <MaskSettings IncludeLiterals="None" Mask="&lt;0..24&gt;"></MaskSettings>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td style="width: 20px; text-align: center;">
                                    <dxe:ASPxTextBox ID="txtHorasSemana" runat="server" Width="100%"
                                        ClientInstanceName="txtHorasSemana" HorizontalAlign="Center">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="#404040"></DisabledStyle>

                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                            <RequiredField IsRequired="True" ErrorText="Campo obrigat&#243;rio !"></RequiredField>
                                        </ValidationSettings>

                                        <MaskSettings IncludeLiterals="None" Mask="&lt;0..168&gt;"></MaskSettings>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td style="width: 20px; text-align: center;">
                                    <dxe:ASPxTextBox ID="txtDiasMes" runat="server" Width="100%"
                                        ClientInstanceName="txtDiasMes" HorizontalAlign="Center">
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="#404040"></DisabledStyle>

                                        <ValidationSettings ErrorDisplayMode="None" ValidationGroup="MKE">
                                            <RequiredField IsRequired="True" ErrorText="Campo obrigat&#243;rio !"></RequiredField>
                                        </ValidationSettings>

                                        <MaskSettings IncludeLiterals="None" Mask="&lt;0..30&gt;"></MaskSettings>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td valign="top" style="width: 50%">
                                    <dxcp:ASPxCallbackPanel ID="pnCalendario" runat="server" ClientInstanceName="pnCalendario" OnCallback="pnCalendario_Callback" Width="100%">
                                        <SettingsLoadingPanel Enabled="False"></SettingsLoadingPanel>
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <dxe:ASPxCalendar ID="calendario" runat="server" ClearButtonText="Limpar Sele&#231;&#227;o"
                                                    Font-Size="10pt" ShowClearButton="False" ShowTodayButton="False"
                                                    TodayButtonText="Hoje" Width="100%" HighlightToday="False"
                                                    OnDayRender="ASPxCalendar1_DayRender" ShowWeekNumbers="False"
                                                    OnDayCellPrepared="calendario_DayCellPrepared" Height="210px">
                                                    <ClientSideEvents SelectionChanged="function(s, e) {
	var dataSelecionada = new Date(s.GetSelectedDate());
	var dia = dataSelecionada.getDate();
	var mes = dataSelecionada.getMonth() + 1;
	var ano = dataSelecionada.getFullYear();
	
	if(dia &lt; 10)
		dia = '0' + dia;

	if(mes &lt; 10)
		mes = '0' + mes;
	
    var idioma = document.querySelector('#dividioma').innerHTML;

    if (idioma.toString().substr(0, 2) == 'en')
    {
	    dataSelecionada = mes + '/' + dia + '/' + ano;
    }
    else
    {
	    dataSelecionada = dia + '/' + mes + '/' + ano;
    }
	lblDataSelecionada.SetText(traducao.frm_Calendarios_data__ + dataSelecionada);
	gvHorariosTrabalho.PerformCallback();
}"
                                                        VisibleMonthChanged="function(s, e) {
	pnCalendario.PerformCallback();
}" />
                                                    <DayOtherMonthStyle ForeColor="Gray">
                                                    </DayOtherMonthStyle>
                                                    <DayWeekendStyle ForeColor="Red">
                                                    </DayWeekendStyle>
                                                    <ValidationSettings ValidationGroup="MKD">
                                                    </ValidationSettings>
                                                    <SettingsLoadingPanel Enabled="False" />
                                                    <DayStyle>
                                                        <Paddings PaddingBottom="2px" PaddingTop="2px" />
                                                    </DayStyle>
                                                </dxe:ASPxCalendar>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxcp:ASPxCallbackPanel>
                                </td>
                                <td valign="top" style="width: 50%">
                                    <dxrp:ASPxRoundPanel runat="server" HeaderText="Horários de Trabalho e Legenda"
                                        Width="100%" ClientInstanceName="rdTipoAtualizacao"
                                        ID="rdTipoAtualizacao">
                                        <HeaderStyle Font-Bold="False"></HeaderStyle>
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left">
                                                            <dxe:ASPxLabel ID="lblDataSelecionada" runat="server"
                                                                ClientInstanceName="lblDataSelecionada">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 88px" valign="top">
                                                            <dxwgv:ASPxGridView ID="gvHorariosTrabalho" runat="server"
                                                                AutoGenerateColumns="False" ClientInstanceName="gvHorariosTrabalho"
                                                                OnCustomCallback="gvHorariosTrabalho_CustomCallback" Width="100%">
                                                                <Columns>
                                                                    <dxwgv:GridViewDataTextColumn Caption="Inicio" FieldName="HoraInicio"
                                                                        ShowInCustomizationForm="True" VisibleIndex="0" Width="140px">
                                                                        <PropertiesTextEdit DisplayFormatString="HH:mm">
                                                                        </PropertiesTextEdit>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                    <dxwgv:GridViewDataTextColumn Caption="Termino" FieldName="HoraTermino"
                                                                        ShowInCustomizationForm="True" VisibleIndex="1" Width="140px">
                                                                        <PropertiesTextEdit DisplayFormatString="HH:mm">
                                                                        </PropertiesTextEdit>
                                                                    </dxwgv:GridViewDataTextColumn>
                                                                </Columns>
                                                                <SettingsPager Mode="ShowAllRecords">
                                                                </SettingsPager>
                                                                <Settings ShowColumnHeaders="False" />

                                                                <SettingsPopup>
                                                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                </SettingsPopup>

                                                                <SettingsText EmptyDataRow="Nenhuma informação" />
                                                            </dxwgv:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="center"
                                                                        style="border-right: darkgray 1px solid; border-top: darkgray 1px solid; border-left: darkgray 1px solid; width: 40px; border-bottom: darkgray 1px solid; height: 25px; background-color: #ccd9c1">
                                                                        <span>1</span></td>
                                                                    <td align="left" style="width: 80px; height: 25px">&nbsp;<dxe:ASPxLabel ID="ASPxLabel14" runat="server"
                                                                        Text="Exceção">
                                                                    </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="height: 25px">&nbsp;</td>
                                                                    <td align="center"
                                                                        style="border-right: darkgray 1px solid; border-top: darkgray 1px solid; border-left: darkgray 1px solid; width: 40px; color: red; border-bottom: darkgray 1px solid; height: 25px">
                                                                        <span>1</span></td>
                                                                    <td align="left" style="width: 80px; height: 25px">&nbsp;<dxe:ASPxLabel ID="ASPxLabel13" runat="server"
                                                                        Text="Dia Não Útil">
                                                                    </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>


                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxrp:ASPxRoundPanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxtc:ASPxPageControl ID="pageControl" ClientInstanceName="pageControl" runat="server" ActiveTabIndex="0" Width="100%">
                            <TabPages>
                                <dxtc:TabPage Name="tabHP" Text="Horário Padrão">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server">
                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" style="width: 100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="center" style="width: 20%;"></td>
                                                                        <td align="center" style="width: 20%">
                                                                            <dxe:ASPxLabel runat="server" Text="Turno 1" ID="ASPxLabel1"></dxe:ASPxLabel>
                                                                        </td>
                                                                        <td align="center" style="width: 20%">
                                                                            <dxe:ASPxLabel runat="server" Text="Turno 2" ID="ASPxLabel2"></dxe:ASPxLabel>
                                                                        </td>
                                                                        <td align="center" style="width: 20%">
                                                                            <dxe:ASPxLabel runat="server" Text="Turno 3" ID="ASPxLabel3"></dxe:ASPxLabel>
                                                                        </td>
                                                                        <td align="center" style="width: 20%">
                                                                            <dxe:ASPxLabel runat="server" Text="Turno 4" ID="ASPxLabel4"></dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; font-size: 12pt; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right" class="style8">
                                                                            <dxe:ASPxLabel runat="server" Text="Segunda" ID="ASPxLabel5"></dxe:ASPxLabel>
                                                                        </td>
                                                                        <td align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Ini_1" ID="txt_Seg_Ini_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Term_1" ID="txt_Seg_Term_1">
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
                                                                        <td align="center" class="style11">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Ini_2" ID="txt_Seg_Ini_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Term_2" ID="txt_Seg_Term_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td align="center" class="style11">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Ini_3" ID="txt_Seg_Ini_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Term_3" ID="txt_Seg_Term_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td align="center" class="style11">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Ini_4" ID="txt_Seg_Ini_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Seg_Term_4" ID="txt_Seg_Term_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                    <tr class="style11">
                                                                        <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; font-size: 12pt; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right" class="style12">
                                                                            <dxe:ASPxLabel runat="server" Text="Ter&#231;a" ID="ASPxLabel6"></dxe:ASPxLabel>
                                                                            &nbsp; </td>
                                                                        <td align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Ini_1" ID="txt_Ter_Ini_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Term_1" ID="txt_Ter_Term_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td align="center" class="style13">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Ini_2" ID="txt_Ter_Ini_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Term_2" ID="txt_Ter_Term_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td align="center" class="style13">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Ini_3" ID="txt_Ter_Ini_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Term_3" ID="txt_Ter_Term_3">
                                                                                                <ClientSideEvents GotFocus="function(s, e) {
	verificarTurnoPequenhos(txt_Ter_Ini_3, txt_Ter_Term_3);
}"
                                                                                                    Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td align="center" class="style11">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Ini_4" ID="txt_Ter_Ini_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Ter_Term_4" ID="txt_Ter_Term_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                    <tr class="style11">
                                                                        <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; font-size: 12pt; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="Quarta" ID="ASPxLabel7"></dxe:ASPxLabel>
                                                                            &nbsp; </td>
                                                                        <td align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Ini_1" ID="txt_Qua_Ini_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Term_1" ID="txt_Qua_Term_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="width: 150px;" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Ini_2" ID="txt_Qua_Ini_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Term_2" ID="txt_Qua_Term_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="width: 150px;" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Ini_3" ID="txt_Qua_Ini_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Term_3" ID="txt_Qua_Term_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="width: 150px;" align="center" class="style11">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Ini_4" ID="txt_Qua_Ini_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qua_Term_4" ID="txt_Qua_Term_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                    <tr class="style11">

                                                                        <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; font-size: 12pt; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="Quinta" ID="ASPxLabel8"></dxe:ASPxLabel>
                                                                            &nbsp; </td>

                                                                        <td align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Ini_1" ID="txt_Qui_Ini_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Term_1" ID="txt_Qui_Term_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
	e.isValid = validarHorario(txt_Qui_Ini_1, txt_Qui_Term_1);
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
                                                                        <td style="width: 150px;" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Ini_2" ID="txt_Qui_Ini_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Term_2" ID="txt_Qui_Term_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="solid; width: 150px; background-color: #ffffff" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Ini_3" ID="txt_Qui_Ini_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Term_3" ID="txt_Qui_Term_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="font-size: 12pt; width: 150px;" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Ini_4" ID="txt_Qui_Ini_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Qui_Term_4" ID="txt_Qui_Term_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                    <tr style="font-size: 12pt; font-family: Times New Roman" class="style11">
                                                                        <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; font-size: 12pt; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="Sexta" ID="ASPxLabel9"></dxe:ASPxLabel>
                                                                            &nbsp; </td>
                                                                        <td style="font-size: 12pt; background-color: #ffffff" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Ini_1" ID="txt_Sex_Ini_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Term_1" ID="txt_Sex_Term_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="border-right: #ffffff 1px solid; border-top: #ffffff 1px solid; font-size: 12pt; border-left: #ffffff 1px solid; width: 150px; border-bottom: #ffffff 1px solid" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Ini_2" ID="txt_Sex_Ini_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Term_2" ID="txt_Sex_Term_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="font-size: 12pt; border-left: #ffffff 1px solid; width: 150px; border-bottom: #ffffff 1px solid; background-color: #ffffff" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Ini_3" ID="txt_Sex_Ini_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Term_3" ID="txt_Sex_Term_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="border-right: #ffffff 1px solid; border-top: #ffffff 1px solid; font-size: 12pt; border-left: #ffffff 1px solid; width: 150px; border-bottom: #ffffff 1px solid" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Ini_4" ID="txt_Sex_Ini_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sex_Term_4" ID="txt_Sex_Term_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                    <tr style="font-size: 12pt; font-family: Times New Roman" class="style11">
                                                                        <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; font-size: 12pt; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="S&#225;bado" ID="ASPxLabel10"></dxe:ASPxLabel>
                                                                            &nbsp; </td>
                                                                        <td style="border-right: #ffffff 1px solid; border-top: #ffffff 1px solid; font-size: 12pt; border-left: #ffffff 1px solid; border-bottom: #ffffff 1px solid; background-color: #ffffff" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Ini_1" ID="txt_Sab_Ini_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Term_1" ID="txt_Sab_Term_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="border-right: #ffffff 1px solid; border-top: #ffffff 1px solid; font-size: 12pt; border-left: #ffffff 1px solid; width: 150px; border-bottom: #ffffff 1px solid" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Ini_2" ID="txt_Sab_Ini_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Term_2" ID="txt_Sab_Term_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="border-right: #ffffff 1px solid; border-top: #ffffff 1px solid; font-size: 12pt; border-left: #ffffff 1px solid; width: 150px; border-bottom: #ffffff 1px solid; background-color: #ffffff" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Ini_3" ID="txt_Sab_Ini_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Term_3" ID="txt_Sab_Term_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="border-right: #ffffff 1px solid; border-top: #ffffff 1px solid; font-size: 12pt; border-left: #ffffff 1px solid; width: 150px; border-bottom: #ffffff 1px solid" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Ini_4" ID="txt_Sab_Ini_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Sab_Term_4" ID="txt_Sab_Term_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                                                        <td style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; font-size: 12pt; border-left: gainsboro 1px solid; width: 75px; border-bottom: gainsboro 1px solid" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="Domingo" ID="ASPxLabel11"></dxe:ASPxLabel>
                                                                            &nbsp; </td>
                                                                        <td style="border-right: #ffffff 1px solid; border-top: #ffffff 1px solid; font-size: 12pt; border-left: #ffffff 1px solid; border-bottom: #ffffff 1px solid; background-color: #ffffff" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Ini_1" ID="txt_Dom_Ini_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Term_1" ID="txt_Dom_Term_1">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="border-right: #ffffff 1px solid; border-top: #ffffff 1px solid; font-size: 12pt; border-left: #ffffff 1px solid; width: 150px; border-bottom: #ffffff 1px solid" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Ini_2" ID="txt_Dom_Ini_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Term_2" ID="txt_Dom_Term_2">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="border-right: #ffffff 1px solid; border-top: #ffffff 1px solid; font-size: 12pt; border-left: #ffffff 1px solid; width: 150px; border-bottom: #ffffff 1px solid; background-color: #ffffff" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Ini_3" ID="txt_Dom_Ini_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Term_3" ID="txt_Dom_Term_3">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                        <td style="width: 150px;" align="center">
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 60px" align="center">
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Ini_4" ID="txt_Dom_Ini_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                                                            <dxe:ASPxTextBox runat="server" Width="51px" HorizontalAlign="Center" ClientInstanceName="txt_Dom_Term_4" ID="txt_Dom_Term_4">
                                                                                                <ClientSideEvents Validation="function(s, e) {
	//if(s.GetText() == &quot;__:__&quot; || s.GetText() == &quot;:&quot; || s.GetText() == &quot;&quot;)
	//	e.isValid = true;
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
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <table>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="border-radius: 18px; width: 16px; height: 16px; background: #ff9933;"></td>
                                                                                        <td style="padding-right: 5px; padding-left: 5px; font-size: 12px;">
                                                                                            <asp:Label ID="Label1" runat="server"
                                                                                                Text="<%$ Resources:traducao, frm_Calendarios_turno_com_mesma_hora %>"></asp:Label>
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
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabE" Text="Exce&#231;&#245;es">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server">
                                            <!-- GRIDVIEW gvExceçao -->
                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvExcecao"
                                                KeyFieldName="CodigoExcecao" AutoGenerateColumns="False" Width="100%"
                                                ID="gvExcecao"
                                                OnCustomCallback="gvExcecao_CustomCallback">
                                                <ClientSideEvents EndCallback="function(s, e) {
	        if(s.cp_MSG != &quot;&quot;)
	        {
		        window.top.mostraMensagem(s.cp_MSG, 'Atencao', true, false, null);
	        }
	        pnCalendario.PerformCallback();
        }"></ClientSideEvents>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoExcecao" Width="80px" Caption=" "
                                                        VisibleIndex="0">
                                                        <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                            AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                            AllowSort="False" />
                                                        <DataItemTemplate>
                                                            <%# getBotoes(Eval("CodigoExcecao").ToString())  %>
                                                        </DataItemTemplate>

                                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                                        <HeaderTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td align="center">
                                                                        <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                                            ClientInstanceName="menu"
                                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                                            OnInit="menu_Init">
                                                                            <Paddings Padding="0px" />
                                                                            <Items>
                                                                                <dxm:MenuItem Name="btnIncluir" Text="" ToolTip="Incluir">
                                                                                    <Image Url="~/imagens/botoes/incluirReg02.png">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                                <dxm:MenuItem Name="btnExportar" Text="" ToolTip="Exportar">
                                                                                    <Items>
                                                                                        <dxm:MenuItem Name="btnXLS" Text="XLS" ToolTip="Exportar para XLS">
                                                                                            <Image Url="~/imagens/menuExportacao/xls.png">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                        <dxm:MenuItem Name="btnPDF" Text="PDF" ToolTip="Exportar para PDF">
                                                                                            <Image Url="~/imagens/menuExportacao/pdf.png">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                        <dxm:MenuItem Name="btnRTF" Text="RTF" ToolTip="Exportar para RTF">
                                                                                            <Image Url="~/imagens/menuExportacao/rtf.png">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML"
                                                                                            ClientVisible="False">
                                                                                            <Image Url="~/imagens/menuExportacao/html.png">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                        <dxm:MenuItem Text="CSV" ToolTip="Exportar para CSV">
                                                                                            <Image Url="~/imagens/menuExportacao/iconoCSV.png">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                    </Items>
                                                                                    <Image Url="~/imagens/botoes/btnDownload.png">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                                <dxm:MenuItem Name="btnLayout" Text="" ClientVisible="false" ToolTip="Layout">
                                                                                    <Items>
                                                                                        <dxm:MenuItem Text="Salvar" ToolTip="Salvar Layout">
                                                                                            <Image IconID="save_save_16x16">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                            <Image IconID="actions_reset_16x16">
                                                                                            </Image>
                                                                                        </dxm:MenuItem>
                                                                                    </Items>
                                                                                    <Image Url="~/imagens/botoes/layout.png">
                                                                                    </Image>
                                                                                </dxm:MenuItem>
                                                                            </Items>
                                                                            <ItemStyle Cursor="pointer">
                                                                                <HoverStyle>
                                                                                    <border borderstyle="None" />
                                                                                </HoverStyle>
                                                                                <Paddings Padding="0px" />
                                                                            </ItemStyle>
                                                                            <SubMenuItemStyle BackColor="White" Cursor="pointer">
                                                                                <SelectedStyle>
                                                                                    <border borderstyle="None" />
                                                                                </SelectedStyle>
                                                                            </SubMenuItemStyle>
                                                                            <Border BorderStyle="None" />
                                                                        </dxm:ASPxMenu>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </HeaderTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="Excecao" Caption="Exce&#231;&#227;o"
                                                        VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataDateColumn Caption="Início" FieldName="Inicio"
                                                        ShowInCustomizationForm="True" VisibleIndex="2" Width="200px"
                                                        Name="Inicio">
                                                        <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>">
                                                        </PropertiesDateEdit>
                                                        <Settings ShowFilterRowMenu="True" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataDateColumn>
                                                    <dxwgv:GridViewDataDateColumn Caption="Término" FieldName="Termino"
                                                        ShowInCustomizationForm="True" VisibleIndex="3" Width="200px"
                                                        Name="Termino">
                                                        <PropertiesDateEdit DisplayFormatString="<%$ Resources:traducao, geral_formato_data_csharp %>">
                                                        </PropertiesDateEdit>
                                                        <Settings ShowFilterRowMenu="True" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataDateColumn>
                                                </Columns>

                                                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="160"></Settings>

                                                <SettingsPopup>
                                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                </SettingsPopup>

                                                <SettingsText EmptyDataRow="Nenhuma Exce&#231;&#227;o Cadastrada"></SettingsText>
                                            </dxwgv:ASPxGridView>

                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                        </dxtc:ASPxPageControl>
                    </td>
                </tr>
                </table>

        </div>
        <dxpc:ASPxPopupControl ID="pcDados" runat="server" AllowDragging="True" ClientInstanceName="pcDados"
            CloseAction="None" HeaderText="Exceção"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Modal="True" ShowFooter="True">
            <FooterTemplate>
                <table style="width:100%">
                    <tr>
                        <td align="right">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <dxtv:ASPxButton ID="btnSalvarPcModal" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarPcModal" Text="Salvar" Theme="MaterialCompact" Width="90px" CssClass="btn">
                                            <ClientSideEvents Click="function(s, e) {
                                    pcDados.GetContentIFrameWindow().funcaoCallbackSalvar();
}" />
                                            <Paddings Padding="0px" />
                                        </dxtv:ASPxButton>
                                    </td>
                                    <td align="right" style="padding-left: 10px;">
                                        <dxtv:ASPxButton ID="btnFecharPcModal" runat="server" AutoPostBack="False" ClientInstanceName="btnFecharPcModal" Text="Fechar" Theme="MaterialCompact" Width="90px" CssClass="btn">
                                            <ClientSideEvents Click="function(s, e) {
	                                pcDados.GetContentIFrameWindow().funcaoCallbackFechar();

}" />
                                            <Paddings Padding="0px" />
                                        </dxtv:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    
                </dxpc:PopupControlContentControl>
            </ContentCollection>

            <ClientSideEvents PopUp="function(s, e) {
var sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
sWidth = sWidth * (2/3);
var sHeight = Math.max(0, document.documentElement.clientHeight) - 50;
 s.SetHeight(sHeight);
s.SetWidth(sWidth);
s.UpdatePosition();           
}" />

            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>

            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        </dxpc:ASPxPopupControl>
        <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
          if(s.cp_MSG != '')
          {
                       window.top.mostraMensagem(s.cp_MSG, 'Atencao', true, false, null);
          }
          else if(s.cp_Erro != '')
          {
                       window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
          }
           else if(s.cp_Sucesso != ' ')	
          {
                     window.top.mostraMensagem(s.cp_Sucesso, 'sucesso', false, false, null);
                     pnCalendario.PerformCallback();
          }
}" />
        </dxcb:ASPxCallback>
        <dxcb:ASPxCallback ID="callbackProjCalend" runat="server"
            ClientInstanceName="callbackProjCalend"
            OnCallback="callbackProjCalend_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
    window.top.fechaModalComFooter();
    window.top.fechaModal();

}" />
        </dxcb:ASPxCallback>
        <dxhf:ASPxHiddenField ID="hfDadosSessao" runat="server" ClientInstanceName="hfDadosSessao">
        </dxhf:ASPxHiddenField>
        <dxcb:ASPxCallback ID="callbackDisp" runat="server" ClientInstanceName="callbackDisp"
            OnCallback="callbackDisp_Callback">
            <ClientSideEvents EndCallback="function(s, e) 
{
	if(callbackDisp.cp_Erro != &quot;&quot;)
	{
		window.top.mostraMensagem(traducao.frm_Calendarios_erro_ + callbackDisp.cp_Erro, 'erro', true, false, null);
	}
	else
	{
		window.top.mostraMensagem(traducao.frm_Calendarios_disponibilidade_calculada_com_sucesso_, 'sucesso', false, false, null);
	}
}" />
        </dxcb:ASPxCallback>

        <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvExcecao" ID="ASPxGridViewExporter1"
            OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
            <Styles>
                <Default></Default>

                <Header></Header>

                <Cell></Cell>

                <GroupFooter Font-Bold="True"></GroupFooter>

                <Title Font-Bold="True"></Title>
            </Styles>
        </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
