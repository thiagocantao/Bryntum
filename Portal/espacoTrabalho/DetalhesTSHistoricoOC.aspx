<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalhesTSHistoricoOC.aspx.cs" Inherits="espacoTrabalho_DetalhesTSHistoricoOC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Detalhes</title>
    <style type="text/css">
        .afastamento {
          padding-right:5px;
          padding-left:10px;
          display: inline-flex !important; 
          align-items: center;
          justify-content: center; 
        }
        .style1 {
            width: 579px;
        }

        .btn {
            text-transform: capitalize !important;
        }

        .max-height-40 {
            max-height: 40px;
        }

        .border {
            border-bottom-color: black !important;
            border-left-color: black !important;
            border-top-color: black !important;
            border-right-color: black !important;
            border-width: 2px;
        }

        .dxgvControl_MaterialCompact .dxgvTable_MaterialCompact .dxgvFocusedRow_MaterialCompact, .dxgvControl_MaterialCompact .dxgvTable_MaterialCompact .dxgvFocusedRow_MaterialCompact.dxgvDataRowHover_MaterialCompact {
            background-color: white;
            color: black;
        }

        .dxgvEditFormDisplayRow_MaterialCompact td.dxgv, .dxgvDetailCell_MaterialCompact td.dxgv, .dxgvDataRow_MaterialCompact td.dxgv, .dxgvDetailRow_MaterialCompact.dxgvADR td.dxgvAIC {
            overflow: hidden;
            border-bottom: 1px solid #000000 !important;
            border-right: 1px solid #000000;
            border-top-width: 1px;
            border-left-width: 1px;
            padding: 10px 5px 9px 3px;
        }
    </style>


</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <dxtc:ASPxPageControl ID="pcDados" runat="server" ActiveTabIndex="5" ClientInstanceName="pcDados"
                        Width="100%">
                        <TabPages>
                            <dxtc:TabPage Name="TabD" Text="TRABALHO">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server">
                                        <div id="divDetalhe">

                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel runat="server" Text="Projeto:" ClientInstanceName="lblProjeto" ID="lblProjeto"></dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtProjeto" ClientEnabled="False" ID="txtProjeto">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <dxe:ASPxLabel runat="server" Text="Tarefa:" ClientInstanceName="lblTarefa" ID="lblTarefa"></dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtTarefa" ClientEnabled="False" ID="txtTarefa">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <dxe:ASPxLabel runat="server" Text="Tarefa Superior:" ClientInstanceName="lblTarefaSuperior" ID="lblTarefaSuperior">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtTarefaSuperior" ClientEnabled="False" ID="txtTarefaSuperior">
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" border="0" style="width: 100%; padding-top: 5px;">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="In&#237;cio Previsto:" ID="lblInicioPrevisto" ClientInstanceName="lblInicioPrevisto"></dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="Término Previsto" ID="lblTerminoPrevisto" ClientInstanceName="lblTerminoPrevisto"></dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="Trabalho Previsto (h):" ClientInstanceName="lblTrabalhoPrevisto" ID="lblTrabalhoPrevisto"></dxe:ASPxLabel>
                                                                        </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                                                ClientInstanceName="lblTarefaSuperior"
                                                                                Text="Aprovador:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 5px">
                                                                            <dxe:ASPxTextBox runat="server" Width="100%" DisplayFormatString="{0:dd/MM/yyyy}" ClientInstanceName="txtInicioPrevisto" ClientEnabled="False" ID="txtInicioPrevisto">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td style="padding-right: 5px">
                                                                            <dxe:ASPxTextBox runat="server" Width="100%" DisplayFormatString="{0:dd/MM/yyyy}" ClientInstanceName="txtTerminoPrevisto" ClientEnabled="False" ID="txtTerminoPrevisto">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td style="padding-right: 5px">
                                                                            <dxe:ASPxTextBox runat="server" Width="100%"
                                                                                DisplayFormatString="{0:n2}" ClientInstanceName="txtTabalhoPrevisto"
                                                                                ClientEnabled="False"
                                                                                ID="txtTabalhoPrevisto">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txtIndicaMarco" runat="server" ClientEnabled="False"
                                                                                ClientInstanceName="txtIndicaMarco" ClientVisible="False">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox runat="server" DisplayFormatString="{0:dd/MM/yyyy}"
                                                                                ClientInstanceName="txtInicio" ClientVisible="False" ClientEnabled="False"
                                                                                ID="txtInicio">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox runat="server" DisplayFormatString="{0:dd/MM/yyyy}"
                                                                                ClientInstanceName="txtTermino" ClientVisible="False" ClientEnabled="False"
                                                                                ID="txtTermino">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxTextBox runat="server" ClientInstanceName="txtTrabalho"
                                                                                ClientEnabled="False" ID="txtTrabalho"
                                                                                ClientVisible="False" DisplayFormatString="{0:n0}">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td style="width: 25%">
                                                                            <dxe:ASPxTextBox ID="txtAprovador" runat="server" ClientEnabled="False"
                                                                                ClientInstanceName="txtAprovador"
                                                                                Width="100%">
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
                                                        <td style="padding-top: 5px">
                                                            <dxe:ASPxLabel runat="server" Text="Anota&#231;&#245;es do Gerente:" ClientInstanceName="lblAnotacoes1" ID="ASPxLabel5"></dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxMemo runat="server" Width="100%" Height="40" ClientInstanceName="mmAnotacoes" ClientEnabled="False" ID="mmAnotacoes" Rows="2">
                                                                <DisabledStyle BackColor="#EBEBEB" CssClass="max-height-40" ForeColor="Black"></DisabledStyle>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="Td1" style="padding-top: 5px">
                                                            <fieldset>
                                                                <legend>
                                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, DetalhesTS_execu__o_da_tarefa  %>"></asp:Literal></legend>
                                                                <table style="margin-top: 5px; width: 100%;" cellspacing="0" cellpadding="0" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 80px">
                                                                                <dxe:ASPxLabel runat="server" Text="% Concluido:" ClientInstanceName="lblPorcentaje" ID="lblPorcentaje"></dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 100px">
                                                                                <dxe:ASPxLabel runat="server" Text="In&#237;cio Real:" ID="ASPxLabel3"></dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 100px">
                                                                                <dxe:ASPxLabel runat="server" Text="T&#233;rmino Real:" ID="ASPxLabel4"></dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 110px">
                                                                                <dxe:ASPxLabel runat="server" Text="Trabalho Real (h):" ClientInstanceName="lblTrabalhoReal" ID="lblTrabalhoReal"></dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 140px">
                                                                                <dxe:ASPxLabel runat="server" Text="Trabalho Restante (h):" ClientInstanceName="lblTrabalhoRestante" ID="lblTrabalhoRestante"></dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 5px">
                                                                                <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtPorcentaje" ID="txtPorcentaje">
                                                                                    <ClientSideEvents KeyPress="function(s, e) {
	keypressPorcentual = &quot;1&quot;;
}"
                                                                                        GotFocus="function(s, e) {
	keypressPorcentual = &quot;&quot;;
}"
                                                                                        TextChanged="function(s, e) {
	e.processOnServer = false;
	if(keypressPorcentual == &quot;1&quot;)
		verificarPorcentual();
}
"></ClientSideEvents>

                                                                                    <MaskSettings Mask="&lt;0..100&gt;"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 5px">
                                                                                <dxe:ASPxDateEdit runat="server" PopupHorizontalAlign="OutsideRight" PopupVerticalAlign="Middle" UseMaskBehavior="True" EncodeHtml="False" Width="100%" ClientInstanceName="ddlInicioReal" ID="ddlInicioReal" DisplayFormatString="<%# Resources.traducao.geral_formato_data_csharp %>" EditFormatString="<%# Resources.traducao.geral_formato_data_csharp %>">
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                                                                                    </ValidationSettings>
                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                    </CalendarProperties>
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="padding-right: 5px">
                                                                                <dxe:ASPxDateEdit runat="server" PopupHorizontalAlign="OutsideRight" PopupVerticalAlign="Middle" UseMaskBehavior="True" EncodeHtml="False" Width="100%" ClientInstanceName="ddlTerminoReal" ID="ddlTerminoReal" DisplayFormatString="<%# Resources.traducao.geral_formato_data_csharp %>" EditFormatString="<%# Resources.traducao.geral_formato_data_csharp %>">
                                                                                    <ClientSideEvents KeyPress="function(s, e) {
	keypressTerminoReal = &quot;1&quot;;
}"
                                                                                        GotFocus="function(s, e) {
	keypressTerminoReal = &quot;&quot;;
}"
                                                                                        LostFocus="function(s, e) {
	e.processOnServer = false;
	if(keypressTerminoReal == &quot;1&quot;)
		verificarTerminoReal();
}
"
                                                                                        ValueChanged="function(s, e) {
	keypressTerminoReal = &quot;1&quot;;
}
"></ClientSideEvents>

                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="MKE">
                                                                                    </ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                    </CalendarProperties>
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="padding-right: 5px">
                                                                                <dxe:ASPxTextBox runat="server" Width="100%" DisplayFormatString="{0:n2}"
                                                                                    ClientInstanceName="txtTrabalhoReal"
                                                                                    ID="txtTrabalhoReal">
                                                                                    <ClientSideEvents KeyPress="function(s, e) {
	keypressTrabalhoReal = &quot;1&quot;;
}"
                                                                                        GotFocus="function(s, e) {
	keypressTrabalhoReal = &quot;&quot;;
}"
                                                                                        ValueChanged="function(s, e) {
	e.processOnServer = false;
	if(keypressTrabalhoReal == &quot;1&quot;)
		verificarTrabalhoReal();
}
"></ClientSideEvents>

                                                                                    <MaskSettings Mask="&lt;0..999999&gt;.&lt;00..99&gt;"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox runat="server" Width="100%" DisplayFormatString="{0:n}" ClientInstanceName="txtTrabalhoRestante" ID="txtTrabalhoRestante">
                                                                                    <ClientSideEvents KeyPress="function(s, e) {
	keypressTrabalhoRestante = &quot;1&quot;;
}"
                                                                                        GotFocus="function(s, e) {
	keypressTrabalhoRestante = &quot;&quot;;
}"
                                                                                        Init="function(s, e) {
	trabalhoRestanteReal = s.GetText();
}"
                                                                                        ValueChanged="function(s, e) {
	e.processOnServer = false;
	if(keypressTrabalhoRestante == &quot;1&quot;)
		verificarTrabalhoRestante();
}
"></ClientSideEvents>

                                                                                    <MaskSettings Mask="&lt;0..999999&gt;.&lt;00..99&gt;"></MaskSettings>

                                                                                    <ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <table style="margin-bottom: 10px" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="padding-top: 5px">
                                                                                <dxe:ASPxLabel runat="server" Text="Coment&#225;rios Recurso:" ClientInstanceName="lblComentarioRecurso" ID="lblAnotacoes"></dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxMemo runat="server" Rows="5" Height="80" Width="100%" MaxLength="2000" ClientInstanceName="mmComentariosRecurso" ID="mmComentariosRecurso">
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
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
                                        </div>


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
                                                        <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="pnCallbackAnotacoes" Width="100%" ID="pnCallbackAnotacoes3">
                                                            <PanelCollection>
                                                                <dxp:PanelContent runat="server">
                                                                    <div id="divComentario">
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxLabel ID="lblProjeto0" runat="server" ClientInstanceName="lblProjeto" Text="Projeto:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxTextBox ID="txtProjetoComentarios" runat="server" ClientEnabled="False" ClientInstanceName="txtProjetoComentarios" Width="100%">
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding-top: 5px">
                                                                                                        <dxtv:ASPxLabel ID="lblTarefa0" runat="server" ClientInstanceName="lblTarefa" Text="Tarefa:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxTextBox ID="txtTarefaComentarios" runat="server" ClientEnabled="False" ClientInstanceName="txtTarefaComentarios" Width="100%">
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding-top: 5px">
                                                                                                        <dxtv:ASPxLabel ID="lblTarefaSuperior0" runat="server" ClientInstanceName="lblTarefaSuperior" Text="Tarefa Superior:">
                                                                                                        </dxtv:ASPxLabel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dxtv:ASPxTextBox ID="txtTarefaSuperiorComentarios" runat="server" ClientEnabled="False" ClientInstanceName="txtTarefaSuperiorComentarios" Width="100%">
                                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                            </DisabledStyle>
                                                                                                        </dxtv:ASPxTextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                        <table style="width: 100%; padding-top: 5px;">
                                                                                            <tr>
                                                                                                <td style="width: 130px">
                                                                                                    <dxe:ASPxLabel runat="server" Text="Data:" ClientInstanceName="lblStatusAprovacao" ID="lblStatusAprovacao">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxLabel runat="server" Text="Respons&#225;vel:" ClientInstanceName="lblAprovadorTarefa" ID="lblAprovadorTarefa">
                                                                                                    </dxe:ASPxLabel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 130px">
                                                                                                    <dxe:ASPxTextBox ID="txtDataStatus" runat="server" ClientEnabled="False" ClientInstanceName="txtDataStatus"
                                                                                                        Width="120px">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <dxe:ASPxTextBox ID="txtAprovadorTarefa" runat="server" ClientEnabled="False" ClientInstanceName="txtAprovadorTarefa"
                                                                                                        Width="100%">
                                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                        </DisabledStyle>
                                                                                                    </dxe:ASPxTextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-top: 5px">
                                                                                        <dxe:ASPxLabel runat="server" Text="Coment&#225;rios do Respons&#225;vel:" ClientInstanceName="lblComentariosAprovador" ID="lblComentariosAprovador2"></dxe:ASPxLabel>

                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxMemo runat="server" Width="100%" ReadOnly="True" ClientInstanceName="mmComentariosAprovador" ClientEnabled="False" ID="mmComentariosAprovador" Rows="19">
                                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                                                                        </dxe:ASPxMemo>

                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
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
                            <dxtc:TabPage Name="TabP" Text="Predecessoras" ClientEnabled="False" ClientVisible="False">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server">
                                        <div id="divPredecessoras" runat="server">
                                            <dxwgv:ASPxGridView ID="gvPredecessoras" runat="server" AutoGenerateColumns="False"
                                                ClientInstanceName="gvPredecessoras" Width="100%">

                                                <SettingsCommandButton>
                                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                                </SettingsCommandButton>

                                                <SettingsPopup>
                                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                </SettingsPopup>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="Tarefa" FieldName="NomeTarefa" VisibleIndex="0">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="280" />
                                            </dxwgv:ASPxGridView>
                                        </div>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="TabS" Text="Sucessoras" ClientEnabled="False" ClientVisible="False">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server">
                                        <div id="divSucessoras" runat="server">
                                            <dxwgv:ASPxGridView ID="gvSucessoras" runat="server" AutoGenerateColumns="False"
                                                ClientInstanceName="gvSucessoras" Width="100%">

                                                <SettingsCommandButton>
                                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>
                                                </SettingsCommandButton>

                                                <SettingsPopup>
                                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                </SettingsPopup>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="Tarefa" FieldName="NomeTarefa" VisibleIndex="0">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsPager Mode="ShowAllRecords">
                                                </SettingsPager>
                                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="280" />
                                            </dxwgv:ASPxGridView>
                                        </div>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Name="tabAnexos" Text="Anexos">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <div id="divAnexos">
                                            <iframe id="frameAnexos" frameborder="0" scrolling="no"
                                                style="width: 100%; height: 422px"></iframe>
                                        </div>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtv:TabPage Name="tabOutrosCustos" Text="Outros Custos">
                                <ContentCollection>
                                    <dxtv:ContentControl runat="server">
                                        
                                        <div id="divContainerOutrosCustos" runat="server" style="display: flex; flex-direction: column;">
                                            <div id="divSaldoProjeto"  runat="server" style="display: flex; align-self: flex-end;">

                                                <dxtv:ASPxCallbackPanel ID="pnCallbackSaldoProjeto" runat="server" ClientInstanceName="pnCallbackSaldoProjeto" OnCallback="pnCallbackSaldoProjeto_Callback">
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            
                                                            <dxtv:ASPxLabel ID="lblTextoSaldoDoProjeto" runat="server" Width="110px" Text="Saldo do projeto:" ClientInstanceName="lblTextoSaldoDoProjeto">
                                                            </dxtv:ASPxLabel>
                                                            
                                                            <dxtv:ASPxLabel ID="lblSaldoDoProjeto" runat="server" Height="22px"  BackColor="#E1E1E1" CssClass="afastamento" Text="R$ 1.000,00" ClientInstanceName="lblSaldoDoProjeto">
                                                            </dxtv:ASPxLabel>
                                                          
                                                            <dxtv:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                                            </dxtv:ASPxHiddenField>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxCallbackPanel>
                                            </div>
                                            <div id="divOutrosCustos" runat="server">
                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoAtribuicao" AutoGenerateColumns="False" Width="100%" ID="gvDados" OnDataBinding="gvDados_DataBinding" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt" OnHtmlDataCellPrepared="gvDados_HtmlDataCellPrepared" OnHtmlCommandCellPrepared="gvDados_HtmlCommandCellPrepared">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) 
{
	gvDados.SetFocusedRowIndex(e.visibleIndex);
                if(e.buttonID == 'btnEditar')
               {                s.GetRowValues(e.visibleIndex,'CodigoAtribuicao;CodigoRecursoProjeto;NomeRecurso;UnidadeMedidaRecurso;UnidadeAtribuicaoLB;CustoUnitarioLB;CustoLB;UnidadeAtribuicaoReal;CustoUnitarioReal;CustoReal;CustoRestante;CodigoTipoRecurso;UnidadeAtribuicaoRealInformado;CustoUnitarioRealInformado;CustoRealInformado;CustoRestanteCalculado', processaAbrePopup);
                }
                if(e.buttonID == 'btnHistorico')
               {
s.GetRowValues(e.visibleIndex,'CodigoAtribuicao;NomeRecurso;UnidadeMedidaRecurso;CodigoTipoRecurso', processaAbreHistorico);
               }
}"
                                                        Init="function(s, e) {
    var height =window.top.getClientHeight(-280);
    s.SetHeight(height);
}"></ClientSideEvents>
                                                    <Columns>
                                                        <dxtv:GridViewDataTextColumn Caption="CodigoTipoRecurso" VisibleIndex="3" FieldName="CodigoTipoRecurso" Visible="False" ShowInCustomizationForm="False">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn Caption="CodigoRecursoProjeto" FieldName="CodigoRecursoProjeto" ShowInCustomizationForm="False" Visible="False" VisibleIndex="2">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn Caption="CodigoAtribuicao" FieldName="CodigoAtribuicao" ShowInCustomizationForm="False" Visible="False" VisibleIndex="1">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewBandColumn Caption="Planejado" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <HeaderStyle HorizontalAlign="Center" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Bold="False" Font-Names="Verdana" Font-Size="10pt" />
                                                            <Columns>
                                                                <dxtv:GridViewDataSpinEditColumn Caption="Quantidade" FieldName="UnidadeAtribuicaoLB" ShowInCustomizationForm="False" VisibleIndex="0">
                                                                    <HeaderStyle BackColor="#E1E1E1" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" />
                                                                    <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                                                    </PropertiesSpinEdit>
                                                                    <HeaderStyle BackColor="#E1E1E1" HorizontalAlign="Center" />
                                                                </dxtv:GridViewDataSpinEditColumn>
                                                                <dxtv:GridViewDataSpinEditColumn Caption="Valor Unitário" FieldName="CustoUnitarioLB" ShowInCustomizationForm="False" VisibleIndex="1">
                                                                    <PropertiesSpinEdit DisplayFormatString="c2" NumberFormat="Custom">
                                                                    </PropertiesSpinEdit>
                                                                    <HeaderStyle BackColor="#E1E1E1" HorizontalAlign="Center" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" />
                                                                </dxtv:GridViewDataSpinEditColumn>
                                                                <dxtv:GridViewDataSpinEditColumn Caption="Valor Total" FieldName="CustoLB" ShowInCustomizationForm="False" VisibleIndex="2">
                                                                    <PropertiesSpinEdit DisplayFormatString="c2" NumberFormat="Custom">
                                                                    </PropertiesSpinEdit>
                                                                    <HeaderStyle BackColor="#E1E1E1" HorizontalAlign="Center" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" />
                                                                </dxtv:GridViewDataSpinEditColumn>
                                                            </Columns>
                                                        </dxtv:GridViewBandColumn>
                                                        <dxtv:GridViewBandColumn Caption="Realizado" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <HeaderStyle HorizontalAlign="Center" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" />
                                                            <Columns>
                                                                <dxtv:GridViewDataSpinEditColumn Caption="Quantidade" FieldName="UnidadeAtribuicaoReal" ShowInCustomizationForm="False" VisibleIndex="0">
                                                                    <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom">
                                                                    </PropertiesSpinEdit>
                                                                    <HeaderStyle BackColor="#E1E1E1" HorizontalAlign="Center" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" />
                                                                </dxtv:GridViewDataSpinEditColumn>
                                                                <dxtv:GridViewDataSpinEditColumn Caption="Valor Unitário" FieldName="CustoUnitarioReal" Name="colunaNomeRecurso" ShowInCustomizationForm="False" VisibleIndex="1">
                                                                    <PropertiesSpinEdit DisplayFormatString="c2" NumberFormat="Custom">
                                                                    </PropertiesSpinEdit>
                                                                    <HeaderStyle BackColor="#E1E1E1" HorizontalAlign="Center" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" />
                                                                </dxtv:GridViewDataSpinEditColumn>
                                                                <dxtv:GridViewDataSpinEditColumn Caption="Valor Total" FieldName="CustoReal" ShowInCustomizationForm="False" VisibleIndex="2">
                                                                    <PropertiesSpinEdit DisplayFormatString="c2" NumberFormat="Custom">
                                                                    </PropertiesSpinEdit>
                                                                    <HeaderStyle BackColor="#E1E1E1" HorizontalAlign="Center" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" />
                                                                </dxtv:GridViewDataSpinEditColumn>
                                                            </Columns>
                                                        </dxtv:GridViewBandColumn>
                                                        <dxtv:GridViewBandColumn Caption=" " ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <HeaderStyle Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" Font-Bold="False" />
                                                            <Columns>
                                                                <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ButtonType="Image" Caption=" " ShowInCustomizationForm="False" VisibleIndex="0" Width="5%">
                                                                    <CustomButtons>
                                                                        <dxtv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                                            </Image>
                                                                        </dxtv:GridViewCommandColumnCustomButton>
                                                                        <dxtv:GridViewCommandColumnCustomButton ID="btnHistorico" Text="Histórico">
                                                                            <Image Url="~/imagens/colunas.png">
                                                                            </Image>
                                                                        </dxtv:GridViewCommandColumnCustomButton>
                                                                    </CustomButtons>
                                                                    <HeaderStyle BackColor="#E1E1E1" HorizontalAlign="Center" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" />
                                                                </dxtv:GridViewCommandColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Recurso" FieldName="NomeRecurso" ShowInCustomizationForm="False" VisibleIndex="1" Width="28%">
                                                                    <HeaderStyle BackColor="#E1E1E1" HorizontalAlign="Center" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" />
                                                                </dxtv:GridViewDataTextColumn>
                                                                <dxtv:GridViewDataTextColumn Caption="Und." FieldName="UnidadeMedidaRecurso" ShowInCustomizationForm="False" VisibleIndex="2" Width="4%">
                                                                    <HeaderStyle BackColor="#E1E1E1" HorizontalAlign="Center" Border-BorderColor="Black" Border-BorderStyle="Solid" Border-BorderWidth="1px" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                </dxtv:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dxtv:GridViewBandColumn>
                                                        <dxtv:GridViewDataTextColumn FieldName="UnidadeAtribuicaoRealInformado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn FieldName="CustoUnitarioRealInformado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn FieldName="CustoRealInformado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn FieldName="UnidadeAtribuicaoRestanteInformado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn FieldName="CustoUnitarioRestanteInformado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn FieldName="CustoRestanteInformado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn FieldName="CustoLB" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn FieldName="CustoUnitarioLB" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn FieldName="UnidadeAtribuicaoLB" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataSpinEditColumn Caption="Valor Restante" FieldName="CustoRestante" ShowInCustomizationForm="True" VisibleIndex="15">
                                                            <PropertiesSpinEdit DisplayFormatString="c2" NumberFormat="Custom">
                                                                <ClearButton DisplayMode="Never">
                                                                </ClearButton>
                                                            </PropertiesSpinEdit>
                                                        </dxtv:GridViewDataSpinEditColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowSelectSingleRowOnly="True" SelectionStoringMode="PerformanceOptimized" AllowHeaderFilter="False" AllowSort="False" AllowAutoFilter="False" AllowGroup="False"></SettingsBehavior>
                                                    <SettingsPager PageSize="100" Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Settings VerticalScrollBarMode="Visible" ShowHeaderFilterBlankItems="False" ShowHeaderFilterListBoxSearchUI="False"></Settings>

                                                    <SettingsPopup>
                                                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                    </SettingsPopup>

                                                    <Styles>
                                                        <AlternatingRow Enabled="False">
                                                        </AlternatingRow>
                                                        <FocusedRow ForeColor="Black">
                                                        </FocusedRow>
                                                        <Cell>
                                                            <BorderBottom BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                        </Cell>
                                                    </Styles>
                                                    <BorderLeft BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                    <BorderTop BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                    <BorderBottom BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />

                                                </dxwgv:ASPxGridView>
                                            </div>
                                        </div>

                                    </dxtv:ContentControl>
                                </ContentCollection>
                            </dxtv:TabPage>
                        </TabPages>

                        <ClientSideEvents ActiveTabChanged="function(s, e) {
                if(e.tab.name == &quot;tabAnexos&quot; &amp;&amp; document.getElementById('frameAnexos').src == &quot;&quot;)
               {
	         document.getElementById('frameAnexos').src = s.cp_URLAnexos;
               }   
}"></ClientSideEvents>
                        <Paddings Padding="0px" />
                    </dxtc:ASPxPageControl>
                </td>
                <td></td>
            </tr>
        </table>
        <div runat="server" id="divSessao" style="display: none"></div>
        <dxcb:ASPxCallback ID="callBack" runat="server" ClientInstanceName="callBack" OnCallback="callBack_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
     lpLoading.Hide(); 
     if(s.cp_OK != '')
     {
               window.top.mostraMensagem(s.cp_OK, 'sucesso', false, false, null);
               window.top.retornoModal = 'S';
      window.top.fechaModalComFooter();
     }
      else
     {
              if(s.cp_Erro != '')               
                    window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
      }
}"></ClientSideEvents>
        </dxcb:ASPxCallback>
        <dxcb:ASPxCallback ID="callbackAplicar" runat="server" ClientInstanceName="callbackAplicar" OnCallback="callbackAplicar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
  if(s.cpOK != '')
     {
               hfGeral.Set('indicaApontamentosSessao', true); 
               var totalRealizado;
                if(s.cpTipoRecurso == 'Equipamento')
                {
                totalRealizado = spnValorTotalRealizado.GetValue();
                }
                else
                {
                totalRealizado = spnRealizadoFinanceiro.GetValue();
                }
              
              var novoSaldo = SaldoProjetoInicialPopup - totalRealizado;
              const saldoFormatadoBRL =  (novoSaldo).toLocaleString('pt-BR', {style: 'currency', currency: 'BRL'});                
                
              lblSaldoDoProjeto.SetText('Saldo do projeto: ' + saldoFormatadoBRL);
              hfGeral.Set('saldoProjeto', novoSaldo);
              pcTipoFinanceiro.Hide();
              pcTipoEquipamento.Hide();
              gvDados.Refresh();
               
     }
      else
     {
              if(s.cpErro != '')               
                    window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
      }
}"></ClientSideEvents>
        </dxcb:ASPxCallback>
        <dxcb:ASPxCallback ID="callbackMontaCamposFormulario" runat="server" ClientInstanceName="callbackMontaCamposFormulario" OnCallback="callbackMontaCamposFormulario_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
 	hfValoresInformados.Set(s.cpCodigoAtribuicao.toString() + '.CodigoTipoRecurso', s.cpCodigoTipoRecurso);
 	hfValoresInformados.Set(s.cpCodigoAtribuicao.toString() + '.NomeRecurso', s.cpCodigoTipoRecurso);
 hfValoresInformados.Set(s.cpCodigoAtribuicao.toString() + '.UnidadeMedidaRecurso', s.cpCodigoTipoRecurso);
 hfValoresInformados.Set(s.cpCodigoAtribuicao.toString() + '.UnidadeAtribuicao', s.cpCodigoTipoRecurso);
hfValoresInformados.Set(s.cpCodigoAtribuicao.toString() + '.CustoUnitario', s.cpCodigoTipoRecurso);
hfValoresInformados.Set(s.cpCodigoAtribuicao.toString() + '.Custo', s.cpCodigoTipoRecurso);
hfValoresInformados.Set(s.cpCodigoAtribuicao.toString() + '.UnidadeAtribuicaoReal', s.cpCodigoTipoRecurso);
hfValoresInformados.Set(s.cpCodigoAtribuicao.toString() + '.CustoUnitarioReal', s.cpCodigoTipoRecurso);
hfValoresInformados.Set(s.cpCodigoAtribuicao.toString() + '.CustoReal', s.cpCodigoTipoRecurso);

if( s.cpCodigoTipoRecurso == 2 )
{
pcTipoEquipamento.Show();
}
else
{
pcTipoFinanceiro.Show();
                }

}" />
        </dxcb:ASPxCallback>
        <dxcp:ASPxHiddenField ID="hfValoresInformados" runat="server" ClientInstanceName="hfValoresInformados">
        </dxcp:ASPxHiddenField>
        <dxcp:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading" Text="Carregando&amp;hellip;">
        </dxcp:ASPxLoadingPanel>
        <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" AllowDragging="True" ClientInstanceName="pcTipoEquipamento" HeaderText="Detalhes" ShowCloseButton="False" Width="730px" ID="pcTipoEquipamento" ShowFooter="True" Modal="True" AllowResize="True">
            <ClientSideEvents PopUp="function(s, e) {
	var largura = Math.max(0, document.documentElement.clientWidth) - 100;
    var altura = Math.max(0, document.documentElement.clientHeight) - 255;

    s.SetWidth(largura);
    s.SetHeight(altura);	   
s.UpdatePosition();

}" />
            <ContentStyle>
                <Paddings Padding="5px"></Paddings>
            </ContentStyle>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <FooterTemplate>
                <div style="float: right;">
                    <table id="Table1" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td style="padding-right: 5px">
                                    <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarTipoMaterial" Text="Aplicar" Width="90px" ID="btnSalvarTipoMaterial" CssClass="btn" Theme="MaterialCompact">
                                        <ClientSideEvents Click="function(s, e) {
                                            executaAcaoBotaoAplicar('Equipamento', spnValorTotalRealizado, spnValorRestante);
}"></ClientSideEvents>

                                        <Paddings Padding="0px"></Paddings>
                                    </dxcp:ASPxButton>

                                </td>
                                <td>
                                    <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFecharTipoMaterial" Text="Fechar" Width="90px" ID="btnFecharTipoMaterial" CssClass="btn" Theme="MaterialCompact">
                                        <ClientSideEvents Click="function(s, e) {
pcTipoEquipamento.Hide();
                    }"></ClientSideEvents>

                                        <Paddings Padding="0px"></Paddings>
                                    </dxcp:ASPxButton>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </FooterTemplate>
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td style="padding-bottom: 5px">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td style="width: 66.66%">
                                                    <dxcp:ASPxLabel runat="server" Text="Recurso:" ID="ASPxLabel1"></dxcp:ASPxLabel>



                                                </td>


                                                <td style="width: 40px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel13" runat="server" Text="Unidade:">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 5px">
                                                    <dxtv:ASPxTextBox ID="txtRecurso" runat="server" ClientInstanceName="txtRecurso" MaxLength="100" Width="100%" ReadOnly="True">
                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </ReadOnlyStyle>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxtv:ASPxTextBox>

                                                </td>


                                                <td align="right">
                                                    <dxtv:ASPxTextBox ID="txtUnidadeMedida" runat="server" ClientInstanceName="txtUnidadeMedida" MaxLength="100" Width="100%" ReadOnly="True">
                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </ReadOnlyStyle>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxtv:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 33.33%; padding-bottom: 5px; padding-right: 5px;">
                                                <dxtv:ASPxRoundPanel ID="ASPxRoundPanel4" runat="server" HeaderText="Último Planejamento" Width="100%">
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 33.34%; text-align: right; padding-right: 5px;">
                                                                            <dxtv:ASPxLabel ID="labelQuantidadeUltimoPlanejamento" runat="server" ClientInstanceName="labelQuantidadeUltimoPlanejamento" Text="Quantidade:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                        <td style="width: 33.34%; text-align: right; padding-right: 5px;">
                                                                            <dxtv:ASPxLabel ID="ASPxLabel18" runat="server" Text="Valor Unitário:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                        <td style="width: 33.34%; text-align: right; padding-right: 5px;">
                                                                            <dxtv:ASPxLabel ID="ASPxLabel19" runat="server" Text="Valor Total:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 33.34%; padding-right: 5px;">
                                                                            <dxtv:ASPxTextBox ID="txtQuantidadeUltimoPlanejamento" runat="server" ClientInstanceName="txtQuantidadeUltimoPlanejamento" DisplayFormatString="n2" HorizontalAlign="Right" MaxLength="20" ReadOnly="True" Width="100%">
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxTextBox>
                                                                        </td>
                                                                        <td style="width: 33.34%; padding-right: 5px;">
                                                                            <dxtv:ASPxSpinEdit ID="spnValorUnitarioUltimoPlanejamento" runat="server" ClientInstanceName="spnValorUnitarioUltimoPlanejamento" DecimalPlaces="2" DisplayFormatString="c2" HorizontalAlign="Right" ReadOnly="True" Width="100%">
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxSpinEdit>
                                                                        </td>
                                                                        <td style="width: 33.34%">
                                                                            <dxtv:ASPxSpinEdit ID="spnValorTotalUltimoPlanejamento" runat="server" ClientInstanceName="spnValorTotalUltimoPlanejamento" DecimalPlaces="2" DisplayFormatString="c2" HorizontalAlign="Right" ReadOnly="True" Width="100%">
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxRoundPanel>
                                            </td>
                                            <td style="width: 33.33%; padding-bottom: 5px; padding-right: 5px;">
                                                <dxtv:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Realizado" Width="100%">
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 33.34%; padding-right: 5px; text-align: right;">
                                                                            <dxtv:ASPxLabel ID="labelQuantidadeRealizado" runat="server" ClientInstanceName="labelQuantidadeRealizado" Text="Quantidade:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                        <td style="width: 33.34%; text-align: right; padding-right: 5px;">
                                                                            <dxtv:ASPxLabel ID="ASPxLabel21" runat="server" Text="Valor Unitário:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                        <td style="width: 100%; text-align: right; padding-right: 5px;">
                                                                            <dxtv:ASPxLabel ID="ASPxLabel22" runat="server" Text="Valor Total:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 33.34%; padding-right: 5px;">
                                                                            <dxtv:ASPxSpinEdit ID="txtQuantidadeRealizado" runat="server" ClientInstanceName="txtQuantidadeRealizado" DisplayFormatString="n2" HorizontalAlign="Right" MaxLength="20" Width="100%">
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                                <ClientSideEvents LostFocus="function(s, e) {
	calculaValorTotalRealizado(s.GetValue(), spnValorUnitarioRealizado.GetValue());

}"
                                                                                    ValueChanged="function(s, e) {
calculaValorTotalRealizado(s.GetValue(), spnValorUnitarioRealizado.GetValue());
}" />
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxSpinEdit>
                                                                        </td>
                                                                        <td style="width: 33.34%; padding-right: 5px;">
                                                                            <dxtv:ASPxSpinEdit ID="spnValorUnitarioRealizado" runat="server" ClientInstanceName="spnValorUnitarioRealizado" DecimalPlaces="2" DisplayFormatString="c2" HorizontalAlign="Right" Width="100%">
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                                <ClientSideEvents LostFocus="function(s, e) {
	calculaValorTotalRealizado(s.GetValue(), txtQuantidadeRealizado.GetValue());

}"
                                                                                    ValueChanged="function(s, e) {
calculaValorTotalRealizado(s.GetValue(), txtQuantidadeRealizado.GetValue());
}" />
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxSpinEdit>
                                                                        </td>
                                                                        <td style="width: 120px">
                                                                            <dxtv:ASPxSpinEdit ID="spnValorTotalRealizado" runat="server" ClientInstanceName="spnValorTotalRealizado" DecimalPlaces="2" DisplayFormatString="c2" HorizontalAlign="Right" ReadOnly="True" Width="100%">
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </ReadOnlyStyle>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxRoundPanel>
                                            </td>
                                            <td style="width: 33.33%; padding-bottom: 5px">
                                                <dxtv:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="Restante" Width="100%">
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 33.34%; padding-right: 5px;">
                                                                            <dxtv:ASPxLabel ID="ASPxLabel25" runat="server" Text="Valor Total:">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 33.34%; padding-right: 5px;">
                                                                            <dxtv:ASPxSpinEdit ID="spnValorRestante" runat="server" ClientInstanceName="spnValorRestante" DecimalPlaces="2" DisplayFormatString="c2" HorizontalAlign="Right" Width="100%" ReadOnly="True">
                                                                                <SpinButtons ShowIncrementButtons="False">
                                                                                </SpinButtons>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <ReadOnlyStyle BackColor="#EBEBEB">
                                                                                </ReadOnlyStyle>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxtv:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxRoundPanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>

        <dxcp:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="None" AllowDragging="True" ClientInstanceName="pcTipoFinanceiro" HeaderText="Detalhes" ShowCloseButton="False" Width="731px" ID="pcTipoFinanceiro" ShowFooter="True" Modal="True" Height="152px" AllowResize="True">
            <ClientSideEvents PopUp="function(s, e) {
	var largura = Math.max(0, document.documentElement.clientWidth) - 100;
    var altura = Math.max(0, document.documentElement.clientHeight) - 255;

    s.SetWidth(largura);
    s.SetHeight(altura);	   
s.UpdatePosition();

}" />
            <ContentStyle>
                <Paddings Padding="5px"></Paddings>
            </ContentStyle>

            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <FooterTemplate>
                <div style="float: right;">
                    <table id="Table2" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td style="padding-right: 5px">
                                    <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarTipoFinanceiro" Text="Aplicar" Width="90px" ID="btnSalvarTipoFinanceiro" CssClass="btn" Theme="MaterialCompact">
                                        <ClientSideEvents Click="function(s, e) {
            
                                            executaAcaoBotaoAplicar('Financeiro', spnRealizadoFinanceiro, spnRestanteFinanceiro);                                            

}"></ClientSideEvents>

                                        <Paddings Padding="0px"></Paddings>
                                    </dxcp:ASPxButton>

                                </td>
                                <td>
                                    <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFecharTipoFinanceiro" Text="Fechar" Width="90px" ID="btnFecharTipoFinanceiro" CssClass="btn" Theme="MaterialCompact">
                                        <ClientSideEvents Click="function(s, e) {
pcTipoFinanceiro.Hide();
}"></ClientSideEvents>

                                        <Paddings Padding="0px"></Paddings>
                                    </dxcp:ASPxButton>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </FooterTemplate>
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td style="padding-bottom: 5px">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tbody>
                                            <tr>
                                                <td style="width: 66.66%">
                                                    <dxcp:ASPxLabel runat="server" Text="Recurso:" ID="ASPxLabel26"></dxcp:ASPxLabel>



                                                </td>


                                                <td style="width: 40px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel27" runat="server" Text="Unidade:">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 5px">
                                                    <dxtv:ASPxTextBox ID="txtRecursoFinanceiro" runat="server" ClientInstanceName="txtRecursoFinanceiro" MaxLength="100" Width="100%" ReadOnly="True">
                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </ReadOnlyStyle>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxtv:ASPxTextBox>

                                                </td>


                                                <td align="right">
                                                    <dxtv:ASPxTextBox ID="txtUnidadeMedidaFinanceiro" runat="server" ClientInstanceName="txtUnidadeMedidaFinanceiro" MaxLength="100" Width="100%" ReadOnly="True">
                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </ReadOnlyStyle>
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxtv:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom: 10px;">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 33.33%; padding-right: 5px;">
                                                <dxtv:ASPxRoundPanel ID="ASPxRoundPanel5" runat="server" HeaderText="Linha de Base" Width="100%">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            <dxtv:ASPxSpinEdit ID="spnLinhaBaseFinanceiro" runat="server" ClientInstanceName="spnLinhaBaseFinanceiro" DecimalPlaces="2" DisplayFormatString="c2" Width="100%" ReadOnly="True" HorizontalAlign="Right">
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </ReadOnlyStyle>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxtv:ASPxSpinEdit>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxRoundPanel>
                                            </td>
                                            <td style="width: 33.33%; padding-right: 5px">
                                                <dxtv:ASPxRoundPanel ID="ASPxRoundPanel9" runat="server" HeaderText="Realizado" Width="100%">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            <dxtv:ASPxSpinEdit ID="spnRealizadoFinanceiro" runat="server" ClientInstanceName="spnRealizadoFinanceiro" DecimalPlaces="2" DisplayFormatString="c2" Width="100%" HorizontalAlign="Right">
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
calculaValorTotalFinanceiroRealizado(s.GetValue());
}" />
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                </ValidationSettings>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxtv:ASPxSpinEdit>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxRoundPanel>
                                            </td>
                                            <td>
                                                <dxtv:ASPxRoundPanel ID="ASPxRoundPanel10" runat="server" HeaderText="Restante" Width="100%">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <PanelCollection>
                                                        <dxtv:PanelContent runat="server">
                                                            <dxtv:ASPxSpinEdit ID="spnRestanteFinanceiro" runat="server" ClientInstanceName="spnRestanteFinanceiro" DecimalPlaces="2" DisplayFormatString="n2" Width="100%" HorizontalAlign="Right" ReadOnly="True">
                                                                <SpinButtons ShowIncrementButtons="False">
                                                                </SpinButtons>
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#EBEBEB">
                                                                </ReadOnlyStyle>
                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dxtv:ASPxSpinEdit>
                                                        </dxtv:PanelContent>
                                                    </PanelCollection>
                                                </dxtv:ASPxRoundPanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>
    </form>
    <script type="text/javascript">

        document.getElementById("divDetalhe").style.height = window.top.getClientHeight(-260) + 'px';
        document.getElementById("divDetalhe").style.overflow = 'auto';

        document.getElementById("divComentario").style.height = window.top.getClientHeight(-260) + 'px';
        document.getElementById("divComentario").style.overflow = 'auto';

        document.getElementById("divAnexos").style.height = window.top.getClientHeight(-260) + 'px';
        document.getElementById("divAnexos").style.overflow = 'auto';

        document.getElementById("pcDados_divPredecessoras").style.height = window.top.getClientHeight(-65) + 'px';
        document.getElementById("pcDados_divSucessoras").style.height = window.top.getClientHeight(-65) + 'px';

        document.getElementById("pcDados_divOutrosCustos").style.height = window.top.getClientHeight(-260) + 'px';


    </script>
</body>
</html>
