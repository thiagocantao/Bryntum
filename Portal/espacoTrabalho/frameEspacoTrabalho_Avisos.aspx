<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="frameEspacoTrabalho_Avisos.aspx.cs" 
         Inherits="espacoTrabalho_frameEspacoTrabalho_Avisos" %>
<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <script type="text/javascript"> 
        <!--
        var curTailElement = null;
        var loadingDivText = '<div class="LoadingDiv">Carregando&hellip;</div>';
        function OnTailClick(newsID, htmlElement) {                
             if (!NewsCallback.InCallback() && !IsCurrentNews(htmlElement)) {
                curTailElement = htmlElement;            
                ShowPopup(htmlElement, loadingDivText);
                NewsCallback.PerformCallback(newsID);        
            }
        }
        function OnCallbackComplete(result) { 
             if (GetPopupControl().IsVisible()) {
                 ShowPopup(curTailElement, result);
                 //txtAviso.SetValue(result);
             }
        }
        function OnNewsControlBeginCallback() {
             GetPopupControl().Hide();
        }
        function IsCurrentNews(htmlElement) {
             return (curTailElement == htmlElement) && GetPopupControl().IsVisible();
        }
        function GetPopupControl() {
             return pcDetalhes;
        }        
        function ShowPopup(element, contentText) {            
            GetPopupControl().Hide();
            GetPopupControl().SetContentHTML(contentText);
            GetPopupControl().ShowAtElement(element);
            //txtAviso.SetText(contentText);
        }
        //-->
    </script>

    <div>
    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);width: 100%">
        <tr>
            <td  style="height: 26px; padding-left: 10px;" valign="middle">
                <dxe:ASPxLabel ID="lblTitulo" runat="server" ClientInstanceName="lblTituloSelecao"
                    Font-Bold="True"  Text="Notícias">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr style="height:26px">
            <td valign="middle">
            </td>
        </tr>
    </table>
    <dxcb:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="NewsCallback"
        OnCallback="ASPxCallback1_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { OnCallbackComplete(e.result); }" />
    </dxcb:ASPxCallback>
    <div style="height:<%=alturaTela%>; overflow-y:auto;">
   <dxnc:ASPxNewsControl ID="ncAvisos" runat="server" DataSourceID="dsAvisos" DateField="DataInicio"
        HeaderTextField="Assunto" TextField="Aviso" ClientInstanceName="ncAvisos" NavigateUrlFormatString="javascript:void('{0}');"
        Width="82%" NameField="CodigoAviso" Font-Size="Small" ForeColor="Black" ItemSpacing="15px"
         PagerPanelSpacing="0px" RowPerPage="4" Font-Bold="False"
        Font-Italic="False" PagerAlign="Center" BackToTopText="Voltar para o início"
        ImageUrlField="ImagemAvisoLido" EmptyDataText="Nenhum aviso para exibir no momento."
        AlwaysShowPager="True">
        <ItemDateStyle Font-Bold="False" Font-Size="X-Small" BackColor="White">
            <Paddings PaddingTop="3px" PaddingLeft="3px" />
        </ItemDateStyle>
        <ItemSettings MaxLength="120" TailText="Detalhe" ShowImageAsLink="True" />
        <ItemStyle VerticalAlign="Top" Height="90px" >
            <Paddings Padding="4px" />
            <Border BorderColor="#E0E0E0" BorderWidth="1px" />
        </ItemStyle>
        <PagerSettings CurrentPageNumberFormat="{0}" Position="Top">
            <Summary AllPagesText="P&#225;gina:" Text="P&#225;gina:" />
        </PagerSettings>
        <ClientSideEvents TailClick="function(s, e) {
	OnTailClick(e.name, e.htmlElement);
    }" BeginCallback="function(s, e) {
        OnNewsControlBeginCallback();
    }" />
        <Paddings PaddingLeft="5px" PaddingTop="15px" />
        <ItemHeaderStyle LineHeight="10px" Font-Bold="True" Font-Overline="False" Font-Size="Small"
            Font-Strikeout="False" Font-Underline="True">
            <Paddings PaddingLeft="0px" PaddingTop="0px" />
        </ItemHeaderStyle>
        <ItemContentStyle Font-Italic="True" Font-Size="12px">
            <Paddings PaddingBottom="2px" PaddingLeft="10px" PaddingTop="0px" />
        </ItemContentStyle>
        <Items>
            <dxnc:NewsItem Date="2009-12-01" HeaderText="Primeiro aviso" Text="Aviso ...">
            </dxnc:NewsItem>
            <dxnc:NewsItem Date="2009-12-15" HeaderText="Segundo Aviso" Text="Aviso para testar a apresenta&#231;&#227;o do primeir aviso. Aviso para testar a apresenta&#231;&#227;o do primeir aviso. Aviso para testar a apresenta&#231;&#227;o do primeir aviso. Aviso para testar a apresenta&#231;&#227;o do primeir aviso. FIM">
            </dxnc:NewsItem>
            <dxnc:NewsItem Date="2009-12-14" HeaderText="Jornal ingl&#234;s diz que Schumacher aceitou proposta de R$ 57 milh&#245;es da Mercedes"
                Text="O jornal ingl&#234;s &quot;Daily Mirror&quot; afirma, em sua edi&#231;&#227;o desta segunda-feira, que Michael Schumacher aceitou retornar &#224; F&#243;rmula 1 pela Mercedes, que comprou a vitoriosa Brawn GP e  assumiu seu lugar, em 2010. O heptacampe&#227;o receberia R$ 57 milh&#245;es (&#163; 20 milh&#245;es) por uma temporada, R$ 40 milh&#245;es (&#163; 14 milh&#245;es) a mais do que as primeiras especula&#231;&#245;es sobre a volta do alem&#227;o &#224; categoria. O alem&#227;o de 40 anos estreou na F&#243;rmula 1 em 1991 com o apoio da Mercedes, por quem corria no Mundial de Prot&#243;tipos. A montadora pagou R$ 360 mil (&#163; 125 mil) na &#233;poca para proporcionar sua primeira corrida na categoria, pela Jordan, em Spa-Francorchamps. Na corrida seguinte, na It&#225;lia, ele foi para a vaga de Roberto Moreno na Benetton. Schumacher assumiria a vaga deixada por Jenson Button, que foi para a McLaren. Ele seria companheiro do jovem alem&#227;o Nico Rosberg, ex-Williams. A Mercedes, que se recusou a pagar o aumento pedido pelo ingl&#234;s campe&#227;o de 2009, pagar&#225; quase o dobro ao heptacampe&#227;o para ter um substituto de peso. Ele, inclusive, j&#225; teria acertado a rescis&#227;o do contrato de consultor da Ferrari, que tinha validade de tr&#234;s anos.">
            </dxnc:NewsItem>
            <dxnc:NewsItem Date="2009-12-01" HeaderText="Diretoria aguarda resposta de Cuca e diz que n&#227;o tem outro nome para o cargo"
                Text="A diretoria do Fluminense conta os minutos para, enfim, poder anunciar que o t&#233;cnico Cuca, respons&#225;vel pela salva&#231;&#227;o da equipe no Brasileir&#227;o, fica para a pr&#243;xima temporada. De acordo com o gestor de futebol do clube, M&#225;rio Bittencourt, a proposta j&#225; foi feita e a resposta do treinador deve acontecer em breve. O dirigente disse que o clube ainda n&#227;o estudou um outro nome para o caso de Cuca decidir n&#227;o seguir nas Laranjeiras no ano que vem. - J&#225; fizemos nossa proposta, o Cuca ficou de responder. Deve responder ainda hoje, no mais tardar amanh&#227;. Para ser sincero, n&#243;s n&#227;o temos nenhum outro nome que n&#227;o seja o Cuca. &#201; claro que o profissional para o Fluminense tem que ser de ponta, de qualidade. Caso ele n&#227;o renove, teremos que procurar um profissional do n&#237;vel do cuca e do fluminense. Mas acreditamos que ele esteja fechando conosco - disse &#224; R&#225;dio Brasil. Sobre a situa&#231;&#227;o do coordenador Branco, Bittencourt disse que tamb&#233;m n&#227;o existe uma defini&#231;&#227;o. O que atrapalha a perman&#234;ncia do tetracampe&#227;o &#233; que o clube tem uma d&#237;vida com ele, e, de acordo com as normas do Flu, n&#227;o poderia receber esses valores enquanto estiver sob contrato. Assim como no caso de Cuca, os dirigentes dizem que n&#227;o h&#225; outro nome para o cargo. - Realmente o clube tem uma d&#237;vida com ele, que tem o direito de receber esses valores. Mas H&#225; uma quest&#227;o: quem tem d&#237;vida mas &#233; contratado, esses valores a receber est&#227;o suspensos. Temos exemplos de dois jogadores neste caso, como o Roni e o Paulo C&#233;sar. Infelizmente n&#227;o podemos ultrapassar as diretrizes do conselho diretor do clube. A quest&#227;o &#233; f&#225;cil de entender. Ele (Branco) tem op&#231;&#227;o. Isso hoje &#233; uma decis&#227;o dele em querer ficar ou n&#227;o - explicou. M&#225;rio Bittencourt afirmou que j&#225; existe um planejamento para 2010, mas a diretoria est&#225; esperando o acerto com Cuca para dar come&#231;ar a contratar refor&#231;os. Segundo o gestor, os torcedores podem confiar que o time ser&#225; forte em 2010. - Planejamento de pr&#233;-temporada, n&#250;mero de jogadores, j&#225; temos definido. ">
            </dxnc:NewsItem>
        </Items>
        <ItemLeftPanelStyle>
            <Paddings PaddingLeft="0px" />
        </ItemLeftPanelStyle>
        <ItemImage Height="25px" Width="25px" />
       <PagerStyle  />
    </dxnc:ASPxNewsControl>
    <asp:SqlDataSource ID="dsAvisos" runat="server" ConnectionString="Data Source=srv03;Initial Catalog=Portfolio;Persist Security Info=True;User ID=sa;Password=rsenha"
        SelectCommand="SELECT CodigoAviso, Assunto, Aviso, DataInicio, DataTermino, DataInclusao FROM Aviso">
    </asp:SqlDataSource>
    <dxpc:ASPxPopupControl PopupAction="None" ClientInstanceName="pcDetalhes" PopupHorizontalAlign="OutsideRight"
        EnableViewState="False" ID="pcDetalhes" runat="server" PopupHorizontalOffset="5"
        AllowDragging="True" PopupAnimationType="None" PopupVerticalAlign="TopSides" CloseAction="CloseButton"
        HeaderText="Detalhe" Font-Size="Small" Font-Underline="False" Width="800px" Font-Bold="False"
        Font-Italic="False" ShowPageScrollbarWhenModal="True" Height="50px">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                &nbsp;</dxpc:PopupControlContentControl>
        </ContentCollection>
        <HeaderStyle  />
        <ClientSideEvents Closing="function(s, e) {
	ncAvisos.PerformCallback();
}" />
    </dxpc:ASPxPopupControl>
    </div>
    </div>
    
</asp:Content>
