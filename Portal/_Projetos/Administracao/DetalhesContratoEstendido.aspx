<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalhesContratoEstendido.aspx.cs" Inherits="_Projetos_Administracao_DetalhesContratoEstendido" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        var atualizarURLParcelas = '';
        var atualizarURLAnexos = '';
        var atualizarURLAditivos = '';
        var atualizarURLPrevisao = '';
        var atualizarURLAcessorios = '';
        var atualizarURLComentarios = '';
        var atualizarURLFornecedor = '';
        var atualizarURLFornecedor2 = '';
        var atualizarURLReajuste = '';

        function Valida(retorno) {
            if (window.top.cancelaFechamentoPopUp == 'S') {
                if (confirm("Existem dados de importação de cronograma que ainda não foram salvos. \nDeseja sair e perder as alterações?"))
                    window.top.cancelaFechamentoPopUp = 'N';
            }
        }

        function atualizaAlturaDosFrames() {
            var sHeight = Math.max(0, document.documentElement.clientHeight) - 100;
            document.getElementById("frmAditivos").style.height = sHeight + "px";
            document.getElementById("frmFornecedor3").style.height = sHeight + "px";
            document.getElementById("frmPrevisao").style.height = sHeight + "px";
            document.getElementById("frmParcelas").style.height = sHeight + "px";
            document.getElementById("frmAnexos").style.height = sHeight + "px";
            document.getElementById("frmComentarios").style.height = sHeight + "px";
            try { document.getElementById("frmAcessorios").style.height = sHeight + "px"; } catch (e) { }
            try { document.getElementById("frmReajuste").style.height = sHeight + "px"; } catch (e) { }
            document.getElementById("frmFornecedor").style.height = (sHeight - 180) + "px";
        }

    </script>
    <style type="text/css">
        .style15 {
            width: 115px;
        }

        .style14 {
            width: 140px;
        }

        .style13 {
            width: 100px;
        }

        .style21 {
            width: 167px;
        }

        .style22 {
            width: 214px;
        }

        .style31 {
            width: 170px;
        }

        .style36 {
            width: 120px;
        }

        .style37 {
            width: 196px;
        }

        .style38 {
            width: 199px;
        }

        .style39 {
            width: 208px;
        }

        .style40 {
            width: 206px;
        }

        .style43 {
            width: 550px;
        }
    </style>
</head>
<body style="padding: 0px">
    <form id="form1" runat="server" style="width: 100%">
        <div style="width: 100%">
            <dxcp:ASPxCallbackPanel ID="callbackSalvar" runat="server"
                ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback1"
                Style="text-align: left" Width="100%">
                <ClientSideEvents EndCallback="function(s, e) {
	atualizarURLParcelas = '';
    atualizarURLAnexos = '';
    atualizarURLAditivos = '';
    atualizarURLPrevisao = '';
    atualizarURLComentarios = '';
    atualizarURLFornecedor = '';
    atualizarURLFornecedor2 = '';
	atualizaAbas();
	mostraDivSalvoPublicado(s.cp_MSG);	
    if(s.cp_CodigoContrato != null &amp;&amp; s.cp_CodigoContrato != '')
		hfGeral.Set('CodigoContrato', s.cp_CodigoContrato);
}" />
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                        <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0"
                            ClientInstanceName="tabControl" Width="100%"
                            ID="tabControl" TabSpacing="10px">
                            <TabPages>
                                <dxtc:TabPage Name="tabP" Text="Principal">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <div runat="server" id="divRolagem">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="lblTipoContrato" runat="server"
                                                                                    ClientInstanceName="lblTipoContrato"
                                                                                    Text="Instrumento:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="lblNumeroContrato" runat="server"
                                                                                    ClientInstanceName="lblNumeroContrato"
                                                                                    Text="Número do Documento:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel25" runat="server"
                                                                                    Text="Número SAP:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server"
                                                                                    Text="Situação:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px;">
                                                                                <dxe:ASPxComboBox ID="ddlTipoContrato" runat="server"
                                                                                    ClientInstanceName="ddlTipoContrato"
                                                                                    Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txtNumeroContrato" runat="server"
                                                                                    ClientInstanceName="txtNumeroContrato"
                                                                                    MaxLength="50" Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px;">
                                                                                <dxe:ASPxTextBox ID="txtNumeroContratoSAP" runat="server"
                                                                                    ClientInstanceName="txtNumeroContratoSAP"
                                                                                    MaxLength="15" Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="ddlSituacao" runat="server"
                                                                                    ClientInstanceName="ddlSituacao"
                                                                                    Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td id="td7">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxe:ASPxLabel ID="ASPxLabel38" runat="server"
                                                                                            Text="Razão Social:">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td>(<a href="#" onclick="novaRazaoSocial()" tabindex="4">Incluir Novo</a>)</td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td id="td8" style="padding-right: 10px;">
                                                                            <dxe:ASPxLabel ID="ASPxLabel39" runat="server"
                                                                                Text="Permitir SubContratação:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td id="td9">
                                                                            <dxe:ASPxLabel ID="ASPxLabel40" runat="server"
                                                                                Text="Classificação:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td id="td10">
                                                                            <dxe:ASPxComboBox ID="ddlRazaoSocial" runat="server"
                                                                                ClientInstanceName="ddlRazaoSocial"
                                                                                Height="21px" IncrementalFilteringMode="Contains" TextFormatString="{0}"
                                                                                Width="100%">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	callbackFornecedor.PerformCallback();
}" />
                                                                                <Columns>
                                                                                    <dxe:ListBoxColumn Caption="Razão Social" FieldName="NomePessoa"
                                                                                        Name="NomePessoa" Width="300px" />
                                                                                    <dxe:ListBoxColumn Caption="Nome Fantasia" FieldName="NomeFantasia"
                                                                                        Name="NomeFantasia" Width="260px" />
                                                                                </Columns>
                                                                                <ItemStyle Wrap="True" />
                                                                                <ListBoxStyle Wrap="True">
                                                                                </ListBoxStyle>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td id="td11" style="padding-right: 10px">
                                                                            <dxe:ASPxComboBox ID="ddlPermitirSub" runat="server"
                                                                                ClientInstanceName="ddlPermitirSub"
                                                                                Width="100%">
                                                                                <ClientSideEvents Init="function(s, e) {
	tabControl.GetTabByName('tabSubContrato').SetVisible(s.GetValue() == 'S');

}"
                                                                                    SelectedIndexChanged="function(s, e) {
  tabControl.GetTabByName('tabSubContrato').SetVisible(s.GetValue() == 'S');
  if (s.GetValue() == 'N'){
     mostraMsgSub(); 
  }
}
" />
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Sim" Value="S" />
                                                                                    <dxe:ListEditItem Text="Não" Value="N" />
                                                                                </Items>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td id="td12">
                                                                            <dxe:ASPxCheckBoxList ID="cbNacionalidade" runat="server"
                                                                                ClientInstanceName="cbNacionalidade"
                                                                                Height="16px" ItemSpacing="10px" RepeatDirection="Horizontal" Width="100%">
                                                                                <Paddings Padding="0px" />
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Nacional" Value="N" />
                                                                                    <dxe:ListEditItem Text="Internacional" Value="I" />
                                                                                </Items>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxCheckBoxList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel37" runat="server"
                                                                    ClientInstanceName="lblNumeroContrato"
                                                                    Text="Objeto:">
                                                                </dxe:ASPxLabel>
                                                                <dxe:ASPxLabel ID="lblCantCarater0" runat="server"
                                                                    ClientInstanceName="lblCantCarater"
                                                                    ForeColor="Silver" Text="0">
                                                                </dxe:ASPxLabel>
                                                                <dxe:ASPxLabel ID="lblDe251" runat="server" ClientInstanceName="lblDe250"
                                                                    EncodeHtml="False" ForeColor="Silver"
                                                                    Text="&amp;nbsp;de 4000">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxMemo ID="mmObjeto" runat="server" ClientInstanceName="mmObjeto"
                                                                    Rows="6" Width="100%">
                                                                    <ClientSideEvents Init="function(s, e) {
	onInit_mmObjeto(s, e);
}" />
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="1" style="width: 100%; margin-right: 0px;">
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="ASPxLabel19" runat="server"
                                                                                ClientInstanceName="lblNumeroContrato"
                                                                                Text="Projeto:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td id="tdLinkProjeto" align="right"
                                                                            style="<%=mostraLinkProjeto %>">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxComboBox ID="ddlProjetos" runat="server"
                                                                                ClientInstanceName="ddlProjetos"
                                                                                IncrementalFilteringMode="Contains" TextField="NomeProjeto"
                                                                                ValueField="CodigoProjeto" Width="100%">
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td id="tdLinkProjeto0" align="right"
                                                                            style="<%=mostraLinkProjeto %>">
                                                                            <dxe:ASPxHyperLink ID="lkProjeto" runat="server" ClientInstanceName="lkProjeto"
                                                                                NavigateUrl="#" Target="_top"
                                                                                Text="Ir para os detalhes do projeto" Width="185px">
                                                                            </dxe:ASPxHyperLink>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel24" runat="server"
                                                                    ClientInstanceName="lblNumeroContrato"
                                                                    Text="Unidade Gestora:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="ddlUnidadeGestora" runat="server"
                                                                    ClientInstanceName="ddlUnidadeGestora"
                                                                    IncrementalFilteringMode="Contains" TextField="NomeUnidadeNegocio"
                                                                    ValueField="CodigoUnidadeNegocio" Width="100%">
                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server"
                                                                    Text="Município:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxComboBox ID="ddlMunicipio" runat="server"
                                                                    ClientInstanceName="ddlMunicipio"
                                                                    IncrementalFilteringMode="Contains" TextFormatString="{1} - {0}" Width="100%">
                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                    <Columns>
                                                                        <dxe:ListBoxColumn Caption="UF" FieldName="SiglaUF" Width="50px" />
                                                                        <dxe:ListBoxColumn Caption="Município" FieldName="NomeMunicipio"
                                                                            Width="830px" />
                                                                    </Columns>
                                                                    <ItemStyle Wrap="True" />
                                                                    <ListBoxStyle Wrap="True">
                                                                    </ListBoxStyle>
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel12" runat="server"
                                                                                    ClientInstanceName="lblNumeroContrato"
                                                                                    Text="Segmento:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                                                    ClientInstanceName="lblNumeroContrato"
                                                                                    Text="Data Assinatura:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel21" runat="server"
                                                                                    ClientInstanceName="lblNumeroContrato"
                                                                                    Text="Data OS Externa:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server"
                                                                                    ClientInstanceName="lblNumeroContrato"
                                                                                    Text="Data Término:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel23" runat="server"
                                                                                    Text="Término c/ Aditivo/TEC:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="ddlsegmento" runat="server"
                                                                                    ClientInstanceName="ddlsegmento"
                                                                                    Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px;">
                                                                                <dxe:ASPxDateEdit ID="ddlAssinatura" runat="server"
                                                                                    ClientInstanceName="ddlAssinatura" DisplayFormatString="dd/MM/yyyy"
                                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False"
                                                                                    UseMaskBehavior="True" Width="100%">
                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                        <DayHeaderStyle />
                                                                                        <DayOtherMonthStyle>
                                                                                        </DayOtherMonthStyle>
                                                                                        <ButtonStyle>
                                                                                            <PressedStyle>
                                                                                            </PressedStyle>
                                                                                            <HoverStyle>
                                                                                            </HoverStyle>
                                                                                        </ButtonStyle>
                                                                                        <Style>
                                                            </Style>
                                                                                    </CalendarProperties>
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                    </ValidationSettings>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="padding-right: 10px;">
                                                                                <dxe:ASPxDateEdit ID="ddlInicioDeVigencia" runat="server"
                                                                                    ClientInstanceName="ddlInicioDeVigencia" DisplayFormatString="dd/MM/yyyy"
                                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False"
                                                                                    UseMaskBehavior="True" Width="100%">
                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                    </CalendarProperties>
                                                                                    <ClientSideEvents DateChanged="function(s, e) {
	//ddlTerminoDeVigencia.SetDate(s.GetValue());
	//calendar = ddlTerminoDeVigencia.GetCalendar();
  	//if ( calendar )
    //	calendar.minDate = new Date(s.GetValue());
}"
                                                                                        ValueChanged="function(s, e) {
	
}" />
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                    </ValidationSettings>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td style="padding-right: 10px;">
                                                                                <dxe:ASPxDateEdit ID="ddlTerminoDeVigencia" runat="server"
                                                                                    ClientInstanceName="ddlTerminoDeVigencia" DisplayFormatString="dd/MM/yyyy"
                                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False"
                                                                                    UseMaskBehavior="True" Width="100%">
                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                    </CalendarProperties>
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                    </ValidationSettings>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxDateEdit ID="ddlTerminoComAditivo" runat="server"
                                                                                    ClientEnabled="False" ClientInstanceName="ddlTerminoComAditivo"
                                                                                    DisplayFormatString="dd/MM/yyyy" EditFormat="Custom"
                                                                                    EditFormatString="dd/MM/yyyy" EncodeHtml="False"
                                                                                    Width="100%">
                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                    </CalendarProperties>
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                    </ValidationSettings>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="lblValorDoContrato" runat="server"
                                                                                    ClientInstanceName="lblValorDoContrato"
                                                                                    Text="Valor do Contrato:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel22" runat="server"
                                                                                    Text="Valor c/ Aditivo/TEC:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server"
                                                                                    Text="Data-Base:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                                                    ClientInstanceName="lblNumeroContrato"
                                                                                    Text="Critério de Reajuste:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="lblPagamentos" runat="server"
                                                                                    ClientInstanceName="Pagamentos"
                                                                                    Text="Pagamentos:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="padding-right: 10px;">
                                                                                <dxe:ASPxTextBox ID="txtValorDoContrato" runat="server"
                                                                                    ClientInstanceName="txtValorDoContrato" DisplayFormatString="{0:n2}"
                                                                                    HorizontalAlign="Right" Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" />
                                                                                    <ValidationSettings ErrorDisplayMode="None" ErrorText=""
                                                                                        ErrorTextPosition="Top" ValidateOnLeave="False">
                                                                                        <ErrorFrameStyle>
                                                                                            <Paddings Padding="0px" />
                                                                                        </ErrorFrameStyle>
                                                                                        <RequiredField ErrorText="" />
                                                                                    </ValidationSettings>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td align="left" style="padding-right: 10px;">
                                                                                <dxe:ASPxTextBox ID="txtValorComAditivo" runat="server" ClientEnabled="False"
                                                                                    ClientInstanceName="txtValorComAditivo" DisplayFormatString="{0:n2}"
                                                                                    HorizontalAlign="Right" Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                    </ValidationSettings>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px;">
                                                                                <dxe:ASPxDateEdit ID="ddlDataBase" runat="server"
                                                                                    ClientInstanceName="ddlDataBase" DisplayFormatString="dd/MM/yyyy"
                                                                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False"
                                                                                    UseMaskBehavior="True" Width="100%">
                                                                                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                    </CalendarProperties>
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                    </ValidationSettings>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxDateEdit>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="ddlCriterioReajuste" runat="server"
                                                                                    ClientInstanceName="ddlCriterioReajuste"
                                                                                    Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="ddlTipoPagamento" runat="server"
                                                                                    ClientInstanceName="ddlTipoPagamento"
                                                                                    Width="100%">
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server"
                                                                                Text="Centro de Custo:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="ASPxLabel33" runat="server"
                                                                                Text="Tipo de Custo:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="ASPxLabel34" runat="server"
                                                                                Text="Conta Contábil:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td id="tdLabelNumeroInterno2" style="<%=mostraNumeroInterno2 %>">
                                                                            <dxe:ASPxLabel ID="ASPxLabelNumeroInterno2" runat="server"
                                                                                Text="DD:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td id="tdLabelNumeroInterno3" style="<%=mostraNumeroInterno3 %>">
                                                                            <dxe:ASPxLabel ID="ASPxLabelNumeroInterno3" runat="server"
                                                                                Text="RD:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxTextBox ID="txtCentroCusto" runat="server"
                                                                                ClientInstanceName="txtCentroCusto"
                                                                                MaxLength="20" Width="100%">
                                                                                <ValidationSettings>
                                                                                    <RequiredField ErrorText="" />
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td style="padding-right: 10px;">
                                                                            <dxe:ASPxComboBox ID="ddlTipoCusto" runat="server"
                                                                                ClientInstanceName="ddlTipoCusto"
                                                                                Width="100%">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Capex" Value="Capex" />
                                                                                    <dxe:ListEditItem Text="Opex" Value="Opex" />
                                                                                </Items>
                                                                                <ValidationSettings>
                                                                                    <RequiredField ErrorText="" />
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td style="padding-right: 10px;">
                                                                            <dxe:ASPxTextBox ID="txtContaContabil" runat="server"
                                                                                ClientInstanceName="txtContaContabil"
                                                                                MaxLength="20" Width="100%">
                                                                                <ValidationSettings>
                                                                                    <RequiredField ErrorText="" />
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td id="tdNumeroInterno2" style="padding-right: 10px;">
                                                                            <dxe:ASPxTextBox ID="txtnumeroInterno2" runat="server"
                                                                                ClientInstanceName="txtnumeroInterno2"
                                                                                MaxLength="25" Width="100%">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td id="tdNumeroInterno3">
                                                                            <dxe:ASPxTextBox ID="txtnumeroInterno3" runat="server"
                                                                                ClientInstanceName="txtnumeroInterno3"
                                                                                MaxLength="25" Width="100%">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 200px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server"
                                                                                    Text="Tipo de Contratação:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 200px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server"
                                                                                    Text="Origem:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel27" runat="server"
                                                                                    Text="Fonte:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel11" runat="server"
                                                                                    Text="Critério de Medição:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px;">
                                                                                <dxe:ASPxComboBox ID="ddlTipoContratacao" runat="server"
                                                                                    ClientInstanceName="ddlTipoContratacao"
                                                                                    Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px;">
                                                                                <dxe:ASPxComboBox ID="ddlOrigem" runat="server" ClientInstanceName="ddlOrigem"
                                                                                    Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td style="padding-right: 10px;">
                                                                                <dxe:ASPxComboBox ID="ddlFonte" runat="server" ClientInstanceName="ddlFonte"
                                                                                    Width="100%">
                                                                                    <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="ddlCriterioMedicao" runat="server"
                                                                                    ClientInstanceName="ddlCriterioMedicao"
                                                                                    Width="100%">
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	var value = s.GetValue();
	callbackCriteriosMedicao.PerformCallback(value);
}" />
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td align="right" style="padding-right: 10px; width: 100px;">
                                                                                <dxe:ASPxHyperLink ID="lkItensMedicao" runat="server"
                                                                                    ClientInstanceName="lkItensMedicao" Cursor="pointer"
                                                                                    Target="_top" Text="Itens de Medição" Width="100px">
                                                                                    <ClientSideEvents Click="function(s, e) {
	var codigoContrato = hfGeral.Get(&quot;CodigoContrato&quot;);
	window.top.showModal2(window.top.pcModal.cp_Path + &quot;_Projetos/administracao/frmItensMedicao.aspx?CC=&quot; + codigoContrato, txtNumeroContrato.GetValue(), null, null, Valida, null);
}" />
                                                                                    <DisabledStyle ForeColor="Gray">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxHyperLink>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="txtRetencaoGarantia" runat="server"
                                                                                ClientInstanceName="txtRetencaoGarantia"
                                                                                Text="Retenção para Garantia: ">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td id="tdlblValorGarantia">
                                                                            <dxe:ASPxLabel ID="lblValorGarantia" runat="server"
                                                                                ClientInstanceName="lblValorGarantia"
                                                                                Text="Valor Garantia:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td id="tdlblPercGarantia" style="display: none">
                                                                            <dxe:ASPxLabel ID="lblPercGarantia" runat="server"
                                                                                ClientInstanceName="lblPercGarantia"
                                                                                Text="% Garantia:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="ASPxLabel35" runat="server"
                                                                                Text="Início Garantia:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxLabel ID="ASPxLabel36" runat="server"
                                                                                Text="Término Garantia:">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 10px;" align="left">
                                                                            <dxe:ASPxComboBox ID="ddlRetencaoGarantia" runat="server"
                                                                                ClientInstanceName="ddlRetencaoGarantia"
                                                                                SelectedIndex="0" Width="100%"
                                                                                PopupHorizontalAlign="NotSet">
                                                                                <ClientSideEvents BeginCallback="function(s, e) {

}"
                                                                                    SelectedIndexChanged="function(s, e) {
	 mostraCampoGarantia(s,e);
}" />
                                                                                <Items>
                                                                                    <dxe:ListEditItem Selected="True" Text="Não informado" Value="NI" />
                                                                                    <dxe:ListEditItem Text="Carta Fiança" Value="CF" />
                                                                                    <dxe:ListEditItem Text="Garantia" Value="GR" />
                                                                                </Items>
                                                                                <ValidationSettings>
                                                                                    <RequiredField ErrorText="" />
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                        <td id="tdValorGarantia" style="padding-right: 10px;">
                                                                            <dxe:ASPxTextBox ID="txtValorGarantia" runat="server"
                                                                                ClientInstanceName="txtValorGarantia" DisplayFormatString="{0:n2}"
                                                                                HorizontalAlign="Right" Width="100%">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;00..99&gt;" />
                                                                                <ValidationSettings ErrorDisplayMode="None">
                                                                                    <RequiredField ErrorText="" />
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td id="tdPercGarantia" style="padding-right: 10px; display: none;">
                                                                            <dxe:ASPxTextBox ID="txtPercGarantia" runat="server"
                                                                                ClientInstanceName="txtPercGarantia" DisplayFormatString="{0:n4}"
                                                                                HorizontalAlign="Right" Width="100%">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <MaskSettings Mask="&lt;0..9999999999999&gt;.&lt;0000..9999&gt;" />
                                                                                <ValidationSettings ErrorDisplayMode="None">
                                                                                    <RequiredField ErrorText="" />
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                        <td style="padding-right: 10px;">
                                                                            <dxe:ASPxDateEdit ID="ddlDataInicioGarantia" runat="server"
                                                                                ClientInstanceName="ddlDataInicioGarantia" DisplayFormatString="dd/MM/yyyy"
                                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False"
                                                                                UseMaskBehavior="True" Width="100%">
                                                                                <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                </CalendarProperties>
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
}" />
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxDateEdit>

                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxDateEdit ID="ddlDataTerminoGarantia" runat="server"
                                                                                ClientInstanceName="ddlDataTerminoGarantia" DisplayFormatString="dd/MM/yyyy"
                                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" EncodeHtml="False"
                                                                                UseMaskBehavior="True" Width="100%">
                                                                                <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                                                                                </CalendarProperties>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                </ValidationSettings>
                                                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                </DisabledStyle>
                                                                            </dxe:ASPxDateEdit>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 380px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel15" runat="server"
                                                                                    Text="Gestor:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel16" runat="server"
                                                                                    Text="Gestor da Contratada:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td id="tdNumeroTrabalhadores01"
                                                                                style="padding-left: 10px; width: 190px; <%=mostraNumeroTrabalhadores %>">
                                                                                <dxe:ASPxLabel ID="ASPxLabel18" runat="server"
                                                                                    Text="Nº de Trabalhadores Diretos:">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-right: 10px; width: 380px;">
                                                                                <dxe:ASPxComboBox ID="ddlGestorContrato" runat="server"
                                                                                    ClientInstanceName="ddlGestorContrato" DropDownStyle="DropDown"
                                                                                    EnableCallbackMode="True"
                                                                                    IncrementalFilteringMode="Contains"
                                                                                    OnItemRequestedByValue="ddlGestorContrato_ItemRequestedByValue"
                                                                                    OnItemsRequestedByFilterCondition="ddlGestorContrato_ItemsRequestedByFilterCondition"
                                                                                    Width="100%">
                                                                                    <ClientSideEvents Init="function(s, e) {
	s.SetText(s.cp_NomeGestor);
}" />
                                                                                    <Columns>
                                                                                        <dxe:ListBoxColumn Caption="Nome" Width="300px" />
                                                                                        <dxe:ListBoxColumn Caption="Email" Width="200px" />
                                                                                    </Columns>
                                                                                    <ValidationSettings ErrorDisplayMode="None" ErrorText="*">
                                                                                    </ValidationSettings>
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txtGestorContratada" runat="server" BackColor="#EBEBEB"
                                                                                    ClientInstanceName="txtGestorContratada"
                                                                                    ReadOnly="True" Width="100%">
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td id="tdNumeroTrabalhadores02"
                                                                                style="padding-left: 10px; width: 190px; <%=mostraNumeroTrabalhadores %>">
                                                                                <dxe:ASPxTextBox ID="txtNumeroTrabalhadores" runat="server"
                                                                                    ClientInstanceName="txtNumeroTrabalhadores" DisplayFormatString="{0:n0}"
                                                                                    HorizontalAlign="Right" Width="100%">
                                                                                    <MaskSettings Mask="&lt;0..999999999&gt;" />
                                                                                    <ValidationSettings ErrorDisplayMode="None">
                                                                                    </ValidationSettings>
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
                                                                <dxe:ASPxLabel ID="ASPxLabel17" runat="server"
                                                                    Text="Origem do Fornecedor:">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxTextBox ID="txtOrigemContratada" runat="server" BackColor="#EBEBEB"
                                                                    ClientInstanceName="txtOrigemContratada"
                                                                    ReadOnly="True" Width="100%">
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblObservacoes" runat="server"
                                                                    ClientInstanceName="lblObservacoes"
                                                                    Text="Observações:">
                                                                </dxe:ASPxLabel>
                                                                <dxe:ASPxLabel ID="lblCantCaraterOb" runat="server"
                                                                    ClientInstanceName="lblCantCaraterOb"
                                                                    ForeColor="Silver" Text="0">
                                                                </dxe:ASPxLabel>
                                                                <dxe:ASPxLabel ID="lblDe250Ob" runat="server" ClientInstanceName="lblDe250Ob"
                                                                    ForeColor="Silver" Text=" de 1000">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxMemo ID="mmObservacoes" runat="server"
                                                                    ClientInstanceName="mmObservacoes"
                                                                    Rows="6" Width="100%">
                                                                    <ClientSideEvents Init="function(s, e) {
	onInit_mmObservacoes(s, e);
}" />
                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                    </DisabledStyle>
                                                                </dxe:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                                                </dxhf:ASPxHiddenField>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabFornecedor" Text="Fornecedor">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabSubContrato" Text="Sub Contratação"
                                    ClientVisible="False">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabPr" Text="Previsão Financeira">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabF" Text="Financeiro">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabA" Text="Anexos">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabAD" Text="Documentos Relacionados">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tabCM" Text="Comentários">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="tbAC" Text="Acréscimos/Retenções" Visible="False">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="TabReajuste" Text="Reajustes"
                                    Visible="False">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                            <ClientSideEvents ActiveTabChanged="function(s, e) {
	atualizaAbas();
}"
                                Init="function(s, e) {
	mostraCampoGarantia(ddlRetencaoGarantia ,e);
                  var sHeight = Math.max(0, document.documentElement.clientHeight) - 100;
        document.getElementById(&quot;callbackSalvar_tabControl_divRolagem&quot;).style.height = sHeight + &quot;px&quot;;
       document.getElementById(&quot;callbackSalvar_tabControl_divRolagem&quot;).style.overflow = &quot;auto&quot;;

}" />
                            <TabStyle>
                                <Paddings PaddingLeft="3px" PaddingRight="3px" />
                            </TabStyle>

                            <ContentStyle>
                                <Paddings Padding="3px"></Paddings>
                            </ContentStyle>
                        </dxtc:ASPxPageControl>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td align="right">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td id="tdBtnImprimir0" align="right">
                                        <dxe:ASPxButton runat="server" AutoPostBack="False"
                                            ClientInstanceName="btnSalvar_MSR" Text="Imprimir" Width="100px"
                                            ID="btnSalvar_MSR">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
}"></ClientSideEvents>

                                            <Paddings Padding="0px"></Paddings>
                                        </dxe:ASPxButton>

                                    </td>
                                    <td id="tdBtnSalvar" align="right"
                                        style="padding-right: 5px; padding-left: 5px;">
                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnSalvar" Text="Salvar"
                                            Width="100px" ID="btnSalvar">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if (window.onClick_btnSalvar)
	    onClick_btnSalvar();
}"></ClientSideEvents>

                                            <Paddings Padding="0px"></Paddings>
                                        </dxe:ASPxButton>

                                    </td>
                                    <td align="right" style="width: 100px;">
                                        <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar"
                                            CommandArgument="btnCancelar" Text="Fechar" Width="100px"
                                            ID="btnCancelar">
                                            <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}"></ClientSideEvents>

                                            <Paddings Padding="0px"></Paddings>
                                        </dxe:ASPxButton>

                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUsuarioIncluido"
            HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False"
            Width="270px" ID="pcUsuarioIncluido">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <table cellspacing="0" cellpadding="0"
                        width="100%" border="0">
                        <tbody>
                            <tr>
                                <td style="" align="center"></td>
                                <td style="width: 70px" align="center" rowspan="3">
                                    <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop" ClientInstanceName="imgSalvar" ID="imgSalvar"></dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao" ID="lblAcaoGravacao"></dxe:ASPxLabel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>

        <dxcb:ASPxCallback runat="server" ClientInstanceName="callbackFornecedor"
            ID="callbackFornecedor" OnCallback="callbackFornecedor_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	carregaDadosFornecedor(s.cp_municipioFornecedor, s.cp_contatoFornecedor);
}"></ClientSideEvents>
        </dxcb:ASPxCallback>
        <dxcb:ASPxCallback ID="callbackCriteriosMedicao" runat="server"
            ClientInstanceName="callbackCriteriosMedicao"
            OnCallback="callbackCriteriosMedicao_Callback">
            <ClientSideEvents CallbackComplete="function(s, e) {
	var habilitado = e.result == &quot;PG&quot; || e.result == &quot;PU&quot;;
	lkItensMedicao.SetEnabled(habilitado);
}" />
        </dxcb:ASPxCallback>

        <asp:SqlDataSource runat="server" ID="dsResponsavel"></asp:SqlDataSource>

    </form>
    <script type="text/javascript">
        atualizaAlturaDosFrames();
    </script>
</body>
</html>
