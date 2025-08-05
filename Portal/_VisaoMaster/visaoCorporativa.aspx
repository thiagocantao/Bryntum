<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="visaoCorporativa.aspx.cs" Inherits="_VisaoMaster_visaoCorporativa"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript" language="JavaScript">
        //    var refreshinterval=600; 
        //    var starttime; 
        //    var nowtime; 
        //    var reloadseconds=0;
        //    var secondssinceloaded = 0;
        //    
        //    function starttime() 
        //    { 
        //        starttime=new Date(); 
        //        starttime=starttime.getTime(); 
        //        countdown(); 
        //    } 
        // 
        //    function countdown() 
        //    { 
        //        nowtime= new Date(); 
        //        nowtime=nowtime.getTime(); 
        //        secondssinceloaded=(nowtime-starttime)/1000; 
        //        reloadseconds=Math.round(refreshinterval-secondssinceloaded); 
        //        if (refreshinterval>=secondssinceloaded) 
        //        { 
        //            var timer=setTimeout("countdown()",25000);         
        //            
        //        } 
        //        else 
        //        { 
        //            clearTimeout(timer); 
        //            window.location.reload(true); 
        //        } 
        //    }
        //    window.onload = starttime;

        function abreMapaIndicadores() {
            var filtro = '';
            if (idPaginaAtual == 0)
                filtro = '?Marcador=S';
            popup = window.open("./ListaIndicadores.aspx" + filtro, "frameIndicadores", "menubar=1, scrollbars=1, statusbar=1, resizable=1, fullscreen=1");
            if (popup != null)
                popup.focus();
        }

        function abreGantt() {
            altura = screen.height - 240;
            largura = screen.width - 60;
            window.top.showModal(window.top.pcModal.cp_Path + '_VisaoMaster/Graficos/gantt.aspx?Altura=' + (altura - 100) + '&Largura=' + (largura - 20) + '&NS=UHE&NUG=-1', 'Gantt', largura, altura, '', null);


        }
    </script>
    <table style="width:100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);
                    width: 100%">
                    <tr style="height: 26px">
                        <td valign="middle">
                            <table cellpadding="0" cellspacing="0" class="headerGrid">
                                <tr>
                                    <td style="padding-left: 10px">
                                        <table cellpadding="0" cellspacing="0" style="height: 24px">
                                            <tr>
                                                <td style="padding-right: 10px">
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 2px">
                                                                <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTitulo" Font-Bold="True"
                                                                     Text="Gestão do Empreendimento">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="lblDataReferencia" runat="server" Font-Italic="True"
                                                                    Font-Size="7pt">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgGrafico" runat="server" ClientInstanceName="imgGrafico" ClientVisible="False"
                                                        Cursor="pointer" ImageUrl="~/imagens/botoes/btnFinanceiro.png" ToolTip="Desempenho Econômico">
                                                        <ClientSideEvents Click="function(s, e) {
	abreCurvaSSitio();
}" />
                                                        <BorderRight BorderColor="White" BorderStyle="Solid" BorderWidth="3px" />
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgIndicador" runat="server" ClientInstanceName="imgIndicador"
                                                        ClientVisible="False" Cursor="pointer" ImageUrl="~/imagens/botoes/btnIndicador.png"
                                                        ToolTip="Indicadores Estratégicos">
                                                        <ClientSideEvents Click="function(s, e) {
	abreMapaIndicadores();
}" />
                                                        <BorderRight BorderColor="White" BorderStyle="Solid" BorderWidth="3px" />
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td>
                                                    <dxe:ASPxButton ID="btnDownLoad" OnClick="btnDownLoad_Click" runat="server" ToolTip="Relatório de Acompanhamento Mensal"
                                                        Wrap="False" ImageSpacing="0px" AutoPostBack="False" ClientInstanceName="btnDownLoad"
                                                        Cursor="pointer" ClientVisible="False">
                                                        <Image Url="~/imagens/botoes/btnPDF.png" />
                                                        <Paddings Padding="0px" />
                                                        <PressedStyle>
                                                            <Border BorderStyle="None" />
                                                        </PressedStyle>
                                                        <HoverStyle BackColor="Transparent">
                                                            <Border BorderStyle="None" BorderWidth="0px" />
                                                        </HoverStyle>
                                                        <FocusRectPaddings Padding="0px" />
                                                        <FocusRectBorder BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" />
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = true;	
}" />
                                                        <Border BorderWidth="0px" BorderStyle="None" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td>
                                                    <dxe:ASPxButton ID="btnEAP" runat="server" ToolTip="EAP de Marcos" Wrap="False" ImageSpacing="0px"
                                                        AutoPostBack="False" ClientInstanceName="btnEAP" Cursor="pointer" ClientVisible="False">
                                                        <Image Url="~/imagens/botoes/botaoGantt.png" />
                                                        <Paddings Padding="0px" />
                                                        <PressedStyle>
                                                            <Border BorderStyle="None" />
                                                        </PressedStyle>
                                                        <HoverStyle BackColor="Transparent">
                                                            <Border BorderStyle="None" BorderWidth="0px" />
                                                        </HoverStyle>
                                                        <FocusRectPaddings Padding="0px" />
                                                        <FocusRectBorder BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" />
                                                        <ClientSideEvents Click="function(s, e) {
	abreGantt();
}" />
                                                        <Border BorderWidth="0px" BorderStyle="None" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td id="tdArea">
                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                        <tr>
                                                            <td style="padding-right: 2px">
                                                                <dxe:ASPxLabel runat="server" Text="Imagens:" Font-Bold="False"
                                                                    ID="lblImagens" ClientInstanceName="lblImagens" ClientVisible="False">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="padding-right: 10px">
                                                                <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxImage ID="imgFotos" runat="server" ClientInstanceName="imgFotos" ClientVisible="False"
                                                                                Cursor="pointer" ImageUrl="~/imagens/botoes/btnFotos.png" ToolTip="Visualizar últimas fotos">
                                                                                <ClientSideEvents Click="function(s, e) {
	abreFotosSitio();
}" />
                                                                                <BorderRight BorderColor="White" BorderStyle="Solid" BorderWidth="3px" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxImage ID="imgSequenciaEvolutiva" runat="server" ClientInstanceName="imgSequenciaEvolutiva"
                                                                                ClientVisible="False" Cursor="pointer" ImageUrl="~/imagens/botoes/btnFilme.png"
                                                                                ToolTip="Sequência Evolutiva">
                                                                                <ClientSideEvents Click="function(s, e) {
	abreSequenciaEvolutiva();
}" />
                                                                                <BorderRight BorderColor="White" BorderStyle="Solid" BorderWidth="3px" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxImage ID="imgFotosEscavacao" runat="server" ClientInstanceName="imgFotosEscavacao"
                                                                                ClientVisible="False" Cursor="pointer" ImageUrl="~/imagens/botoes/btnEscavacao.png"
                                                                                ToolTip="Evolução da Escavação">
                                                                                <ClientSideEvents Click="function(s, e) {
	callbackZoom.PerformCallback('Escavacao');
}" />
                                                                                <BorderRight BorderColor="White" BorderStyle="Solid" BorderWidth="3px" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel runat="server" Text="Área:" Font-Bold="False"
                                                                    ID="lblDesempenhoUHE3">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="padding-right: 10px">
                                                                <dxe:ASPxComboBox ID="ddlArea" runat="server" ClientInstanceName="ddlArea"
                                                                    IncrementalFilteringMode="Contains" Width="300px" ClientVisible="False">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	//var nomeArea = s.GetValue() == &quot;-1&quot; ? &quot;Obras Civis e Fornecimento e Montagem&quot; : s.GetText();
	//window.top.gotoURL('./_VisaoMaster/Graficos/visaoCorporativa_0' + idPaginaAtual + '.aspx?CA=' + s.GetValue() + '&amp;NA=' + nomeArea, 'frmVC'); 
    selecionaMenu(idPaginaAtual);
}" />
                                                                </dxe:ASPxComboBox>
                                                                <dxe:ASPxComboBox ID="ddlArea2" runat="server" ClientInstanceName="ddlArea2"
                                                                    IncrementalFilteringMode="Contains" Width="300px">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
    //var nomeArea = s.GetValue() == &quot;-1&quot; ? &quot;&quot; : s.GetText();	
	//window.top.gotoURL('./_VisaoMaster/Graficos/visaoPresidencia.aspx?CA=' + s.GetValue() + '&NA=' + nomeArea, 'frmVC'); 
    selecionaMenu(idPaginaAtual);
}" />
                                                                </dxe:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgAjuda" runat="server" ClientInstanceName="imgAjuda" ClientVisible="False"
                                                        Cursor="pointer" ImageUrl="~/imagens/ajuda.png" ToolTip="Ajuda">
                                                        <ClientSideEvents Click="function(s, e) {
	if(frmVC.getAjuda)
	{
		document.getElementById('spanAjuda').innerHTML = frmVC.getAjuda();
		pcAjuda.Show();
	}
}" />
                                                        <BorderLeft BorderColor="White" BorderStyle="Solid" BorderWidth="5px" />
                                                        <BorderRight BorderColor="White" BorderStyle="Solid" BorderWidth="3px" />
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgAnterior" runat="server" ClientInstanceName="imgAnterior" ImageUrl="~/imagens/anterior.png"
                                                        Cursor="pointer">
                                                        <ClientSideEvents Click="function(s, e) {
	//if(urlAnterior != &quot;&quot;)
	//	{
	//		setTimeout('abreVisao' + urlAnterior + '();', 1);    
		//window.top.gotoURL('./_VisaoMaster/Graficos/visaoCorporativa_0' + urlAnterior + '.aspx?CA=' + ddlArea.GetValue(), 'frmVC'); 
        //window.parent.window.top.mudaLogoLD('imagens/ne/UHE_0' + urlAnterior + '.png');
    //}
	getPaginaPorId(-1);
}" />
                                                        <BorderLeft BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                                        <BorderRight BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgProximo" runat="server" ClientInstanceName="imgProximo" ImageUrl="~/imagens/proximo.png"
                                                        Cursor="pointer">
                                                        <ClientSideEvents Click="function(s, e) {
	//if(urlProximo != &quot;&quot;)
    //{
	//	setTimeout('abreVisao' + urlProximo + '();', 1);      

		//window.top.gotoURL('./_VisaoMaster/Graficos/visaoCorporativa_0' + urlProximo + '.aspx?CA=' + ddlArea.GetValue(), 'frmVC'); 
        //window.parent.window.top.mudaLogoLD('imagens/ne/UHE_0' + urlProximo + '.png');
    //}
	getPaginaPorId(+1);
}" />
                                                        <BorderLeft BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                                        <BorderRight BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td id="tdMenu">
                                                    <dxe:ASPxImage ID="imgMenu" runat="server" ClientInstanceName="imgMenu" ImageUrl="~/imagens/ne/menuPaineis.png"
                                                        Cursor="pointer" ToolTip="Navegar entre os painéis">
                                                        <ClientSideEvents Click="function(s, e) {
	pMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent));
}" />
                                                        <BorderLeft BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                                        <BorderRight BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 2px;">
                <iframe frameborder="0" name="frmVC" scrolling="yes" src="<%=urlVisao %>" width="100%"
                    style="height: <%=alturaTela %>" id="frmVC"></iframe>
                <dxm:ASPxPopupMenu ID="pMenu" runat="server" ClientInstanceName="pMenu" CloseAction="MouseOut"
                     PopupElementID="tdMenu" EncodeHtml="False">
                    <ClientSideEvents ItemClick="function(s, e) {
	selecionaMenu(e.item.name)
}" />
                    <Items>
                        <dxm:MenuItem Text="Gestão do Empreendimento" Name="0">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="1" Text="UHE Belo Monte">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="4" Text="&amp;nbsp;&amp;nbsp;-&amp;nbsp;Visão Global de Orçamento UHE Belo Monte">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="&amp;nbsp;&amp;nbsp;-&amp;nbsp;Sítio Pimental" Name="5">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="&amp;nbsp;&amp;nbsp;-&amp;nbsp;Sítio Belo Monte" Name="6">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="&amp;nbsp;&amp;nbsp;-&amp;nbsp;Sítio Infraestrutura" Name="7">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="&amp;nbsp;&amp;nbsp;-&amp;nbsp;Sítio Canais de Derivação, Transposição e Enchimento"
                            Name="8">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="&amp;nbsp;&amp;nbsp;-&amp;nbsp;Sítio Diques" Name="9">
                        </dxm:MenuItem>
                    </Items>
                    <ItemStyle Cursor="pointer" />
                </dxm:ASPxPopupMenu>
                <dxcp:ASPxCallbackPanel ID="callbackZoom" runat="server" ClientInstanceName="callbackZoom"
                    OnCallback="callbackZoom_Callback">
                    <ClientSideEvents EndCallback="function(s, e) {
	pcZoom.Show();
}" />
                    <Styles>
                    <LoadingPanel HorizontalAlign="Center" VerticalAlign="Middle"></LoadingPanel>
                    </Styles>
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxpc:ASPxPopupControl ID="pcZoom" runat="server" ClientInstanceName="pcZoom"
                                HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter"
                                PopupVerticalAlign="WindowCenter" Height="400px" Width="600px">
                                <ContentCollection>
                                    <dxpc:PopupControlContentControl runat="server">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <dxe:ASPxBinaryImage ID="imgZoom" runat="server" Width="100%" ImageSizeMode="FitProportional">
                                                        <EmptyImage Url="~/imagens/Sem_Imagem.png">
                                                        </EmptyImage>
                                                        <Border BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" />
                                                    </dxe:ASPxBinaryImage>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label28" runat="server"  Text="Comentários:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxMemo ID="txtDescricaoFoto" runat="server" ClientEnabled="False"
                                                        Rows="3" Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxMemo>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxpc:PopupControlContentControl>
                                </ContentCollection>
                            </dxpc:ASPxPopupControl>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    Modal="True" CloseAction="CloseButton" ClientInstanceName="pcAjuda" HeaderText="Ajuda"
                     ID="pcAjuda" Width="800px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table cellpadding="0" cellspacing="0" class="headerGrid">
                                <tr>
                                    <td>
                                        <div id="divAjuda" style="overflow: auto;" runat="server">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <dxe:ASPxButton ID="btnFechar" runat="server" AutoPostBack="False"
                                            Text="Fechar" Width="90px">
                                            <ClientSideEvents Click="function(s, e) {
	pcAjuda.Hide();
	document.getElementById('spanAjuda').innerHTML = '';
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxhf:ASPxHiddenField ID="hfPermissoes" runat="server" ClientInstanceName="hfPermissoes">
                </dxhf:ASPxHiddenField>
                <dxcb:ASPxCallback ID="callbackMetrica" runat="server" ClientInstanceName="callbackMetrica"
                    OnCallback="callbackMetrica_Callback">
                    <ClientSideEvents EndCallback="function(s, e) {
	txtIndicador.SetText(s.cp_Indicador);
	txtAtualizacao.SetText(s.cp_Atualizacao);
	txtMetrica.SetText(s.cp_Metrica);
	lblObjeto.SetText(s.cp_LabelObjeto);
	pcMetrica.Show();
}" />
                </dxcb:ASPxCallback>
                <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    Modal="True" CloseAction="CloseButton" ClientInstanceName="pcMetrica" HeaderText="Métrica e Última Atualização"
                     ID="pcMetrica" Width="950px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel ID="lblObjeto" runat="server" ClientInstanceName="lblObjeto"
                                                        Text="Indicador:">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td style="width: 120px">
                                                    <asp:Label ID="Label30" runat="server"  Text="Última Atualização:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 10px">
                                                    <dxe:ASPxTextBox ID="txtIndicador" runat="server" ClientEnabled="False" ClientInstanceName="txtIndicador"
                                                         Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
                                                </td>
                                                <td style="width: 120px">
                                                    <dxe:ASPxTextBox ID="txtAtualizacao" runat="server" ClientEnabled="False" ClientInstanceName="txtAtualizacao"
                                                        DisplayFormatString="dd/MM/yyyy"  Width="100%">
                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                        </DisabledStyle>
                                                    </dxe:ASPxTextBox>
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
                                    <td>
                                        <asp:Label ID="Label31" runat="server"  Text="Métrica:"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                        <dxe:ASPxMemo ID="txtMetrica" runat="server" ClientEnabled="False" ClientInstanceName="txtMetrica"
                                             Rows="20" Width="100%" AutoResizeWithContainer="True">
                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                            </DisabledStyle>
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <dxe:ASPxButton ID="btnFechar0" runat="server" AutoPostBack="False"
                                            Text="Fechar" Width="90px">
                                            <ClientSideEvents Click="function(s, e) {
	pcMetrica.Hide();
}" />
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
            </td>
        </tr>
    </table>
</asp:Content>
