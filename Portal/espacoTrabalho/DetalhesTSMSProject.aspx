<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalhesTSMSProject.aspx.cs" Inherits="espacoTrabalho_DetalhesTS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<base target="_self" />
    <title>Detalhes</title>
    <script type="text/javascript" src="../scripts/CDIS.js" language="javascript"></script>
    
  
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <dxtc:aspxpagecontrol id="pcDados" runat="server" activetabindex="0" clientinstancename="pcDados"
                         width="100%"><TabPages>
<dxtc:TabPage Name="TabD" Text="Detalhe"><ContentCollection>
<dxw:ContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td><dxe:ASPxLabel runat="server" Text="Projeto:" ClientInstanceName="lblProjeto"  ID="lblProjeto"></dxe:ASPxLabel>
 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtProjeto" ClientEnabled="False"  ID="txtProjeto">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td></tr><tr><td style="HEIGHT: 5px"></td></tr><tr><td><dxe:ASPxLabel runat="server" Text="Tarefa:" ClientInstanceName="lblTarefa"  ID="lblTarefa"></dxe:ASPxLabel>
 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtTarefa" ClientEnabled="False"  ID="txtTarefa">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td></tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxLabel runat="server" Text="Tarefa Superior:" ClientInstanceName="lblTarefaSuperior"  ID="lblTarefaSuperior">
            </dxe:ASPxLabel>
        </td>
    </tr>
    <tr>
        <td>
            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtTarefaSuperior" ClientEnabled="False"  ID="txtTarefaSuperior">
                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                </DisabledStyle>
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr><td><table cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><dxe:ASPxLabel runat="server" Text="In&#237;cio Previsto:"  ID="ASPxLabel1"></dxe:ASPxLabel>
 </td><td style="WIDTH: 10px"></td><td><dxe:ASPxLabel runat="server" Text="T&#233;rmino Previsto:"  ID="ASPxLabel2"></dxe:ASPxLabel>
 </td><td style="WIDTH: 10px"></td><td><dxe:ASPxLabel runat="server" Text="Trabalho Previsto (h):" ClientInstanceName="lblTrabalhoPrevisto"  ID="lblTrabalhoPrevisto"></dxe:ASPxLabel>
 </td><td style="WIDTH: 10px"></td><td></td><td></td><td></td>
</tr><tr><td><dxe:ASPxTextBox runat="server" Width="110px" DisplayFormatString="{0:dd/MM/yyyy}" ClientInstanceName="txtInicioPrevisto" ClientEnabled="False"  ID="txtInicioPrevisto">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td><td></td><td><dxe:ASPxTextBox runat="server" Width="110px" DisplayFormatString="{0:dd/MM/yyyy}" ClientInstanceName="txtTerminoPrevisto" ClientEnabled="False"  ID="txtTerminoPrevisto">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td><td></td><td><dxe:ASPxTextBox runat="server" Width="130px" DisplayFormatString="{0:n0}" ClientInstanceName="txtTabalhoPrevisto" ClientEnabled="False"  ID="txtTabalhoPrevisto">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td><td></td><td><dxe:ASPxTextBox runat="server" Width="100px" DisplayFormatString="{0:dd/MM/yyyy}" ClientInstanceName="txtInicio" ClientVisible="False" ClientEnabled="False"  ID="txtInicio">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td><td><dxe:ASPxTextBox runat="server" Width="100px" DisplayFormatString="{0:dd/MM/yyyy}" ClientInstanceName="txtTermino" ClientVisible="False" ClientEnabled="False"  ID="txtTermino">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td><td><dxe:ASPxTextBox runat="server" Width="100px" DisplayFormatString="{0:n0}" ClientInstanceName="txtTrabalho" ClientVisible="False" ClientEnabled="False"  ID="txtTrabalho">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td>
</tr></TBODY></table></td></tr><tr><td style="HEIGHT: 5px"></td></tr><tr><td><dxe:ASPxLabel runat="server" Text="Anota&#231;&#245;es do Gerente:" ClientInstanceName="lblAnotacoes1"  ID="ASPxLabel5"></dxe:ASPxLabel>
 </td></tr><tr><td>
     <dxhe:ASPxHtmlEditor ID="mmAnotacoes" runat="server" ActiveView="Preview" BackColor="#EBEBEB"
         ClientInstanceName="mmAnotacoes" Height="145px" Width="100%">
         <Styles>
             <PreviewArea BackColor="#EBEBEB">
             </PreviewArea>
         </Styles>
         <Settings AllowDesignView="False" AllowHtmlView="False" />
     </dxhe:ASPxHtmlEditor>
 </td></tr><tr><td style="HEIGHT: 5px"></td></tr><tr><td id="Td1"><FIELDSET><LEGEND>Execução da Tarefa</LEGEND><table style="MARGIN-TOP: 5px" cellspacing="0" cellpadding="0" border="0"><TBODY><tr><td><dxe:ASPxLabel runat="server" Text="% Concluido:" ClientInstanceName="lblPorcentaje"  ID="lblPorcentaje"></dxe:ASPxLabel>
 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 100px"><dxe:ASPxLabel runat="server" Text="In&#237;cio Real:"  ID="ASPxLabel3"></dxe:ASPxLabel>
 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 100px"><dxe:ASPxLabel runat="server" Text="T&#233;rmino Real:"  ID="ASPxLabel4"></dxe:ASPxLabel>
 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 110px"><dxe:ASPxLabel runat="server" Text="Trabalho Real (h):" ClientInstanceName="lblTrabalhoReal"  ID="lblTrabalhoReal"></dxe:ASPxLabel>
 </td><td style="WIDTH: 10px"></td><td style="WIDTH: 140px"><dxe:ASPxLabel runat="server" Text="Trabalho Restante (h):" ClientInstanceName="lblTrabalhoRestante"  ID="lblTrabalhoRestante"></dxe:ASPxLabel>
 </td></tr><tr><td><dxe:ASPxTextBox runat="server" Width="80px" ClientInstanceName="txtPorcentaje"  ID="txtPorcentaje" ClientEnabled="False">
<ClientSideEvents KeyPress="function(s, e) {
	keypressPorcentual = &quot;1&quot;;
}" GotFocus="function(s, e) {
	keypressPorcentual = &quot;&quot;;
}" LostFocus="function(s, e) {
	e.processOnServer = false;
	if(keypressPorcentual == &quot;1&quot;)
		verificarPorcentual();
}"></ClientSideEvents>

<MaskSettings Mask="&lt;0..100&gt;"></MaskSettings>

<ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td><td></td><td><dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlInicioReal"  ID="ddlInicioReal" ClientEnabled="False">
<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
     <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
     </CalendarProperties>
</dxe:ASPxDateEdit>
 </td><td></td><td><dxe:ASPxDateEdit runat="server" UseMaskBehavior="True" EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False" Width="100%" DisplayFormatString="dd/MM/yyyy" ClientInstanceName="ddlTerminoReal"  ID="ddlTerminoReal" ClientEnabled="False">
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

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
     <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
     </CalendarProperties>
</dxe:ASPxDateEdit>
 </td><td></td><td><dxe:ASPxTextBox runat="server" Width="100%" DisplayFormatString="{0:n}" ClientInstanceName="txtTrabalhoReal"  ID="txtTrabalhoReal" ClientEnabled="False">
<ClientSideEvents KeyPress="function(s, e) {
	keypressTrabalhoReal = &quot;1&quot;;
}" GotFocus="function(s, e) {
	keypressTrabalhoReal = &quot;&quot;;
}" LostFocus="function(s, e) {
	e.processOnServer = false;
	if(keypressTrabalhoReal == &quot;1&quot;)
		verificarTrabalhoReal();
}"></ClientSideEvents>

<MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;"></MaskSettings>

<ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td><td></td><td><dxe:ASPxTextBox runat="server" Width="100%" DisplayFormatString="{0:n}" ClientInstanceName="txtTrabalhoRestante"  ID="txtTrabalhoRestante" ClientEnabled="False">
<ClientSideEvents KeyPress="function(s, e) {
	keypressTrabalhoRestante = &quot;1&quot;;
}" GotFocus="function(s, e) {
	keypressTrabalhoRestante = &quot;&quot;;
}" LostFocus="function(s, e) {
	e.processOnServer = false;
	if(keypressTrabalhoRestante == &quot;1&quot;)
		verificarTrabalhoRestante();
}" Init="function(s, e) {
	trabalhoRestanteReal = s.GetText();
}"></ClientSideEvents>

<MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;"></MaskSettings>

<ValidationSettings ErrorDisplayMode="None"></ValidationSettings>

<DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
</dxe:ASPxTextBox>
 </td></tr></TBODY></table><table style="MARGIN-BOTTOM: 10px" cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="HEIGHT: 5px"></td></tr></TBODY></table></FIELDSET> </td></tr><tr><td><dxpc:ASPxPopupControl runat="server" ClientInstanceName="pcUsuarioIncluido" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" Width="270px"  ID="pcUsuarioIncluido"><ContentCollection>
<dxpc:PopupControlContentControl runat="server"><table cellspacing="0" cellpadding="0" width="100%" border="0"><TBODY><tr><td style="" align="center"></td><td style="WIDTH: 70px" align="center" rowSpan=3><dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>



 </td></tr><tr><td style="HEIGHT: 10px"></td></tr><tr><td align="center"><dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"  ID="lblAcaoGravacao"></dxe:ASPxLabel>



 </td></tr></TBODY></table></dxpc:PopupControlContentControl>
</ContentCollection>
</dxpc:ASPxPopupControl>
 </td></tr><tr><td align=right>&nbsp;&nbsp;</td></tr></TBODY></table></dxw:ContentControl>
</ContentCollection>
</dxtc:TabPage>
</TabPages>

<ClientSideEvents ActiveTabChanged="function(s, e) {
	var tab = pcDados.GetActiveTab();
	if(e.tab.name=='TabD')
		btnSalvar.SetVisible(true);
	else
		btnSalvar.SetVisible(false);
}"></ClientSideEvents>
</dxtc:aspxpagecontrol>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" >
                    <table>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                            <td>
                    <dxe:aspxbutton id="btnFechar" runat="server" autopostback="False" clientinstancename="btnFechar"
                         text="Fechar" width="90px">
<Paddings Padding="0px"></Paddings>

<ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
						window.top.retornoModal = 'N';
                        window.top.fechaModal();
                    }"></ClientSideEvents>
</dxe:aspxbutton>
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
