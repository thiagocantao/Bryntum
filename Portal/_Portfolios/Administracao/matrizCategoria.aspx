<%@ Page Language="C#" AutoEventWireup="true" CodeFile="matrizCategoria.aspx.cs" Inherits="_Portfolios_Administracao_matrizCategoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../../scripts/ASPxListbox.js" language="javascript"></script>
    <script type="text/javascript" language="javascript" src="../../scripts/CDIS.js"></script>
    <script type="text/javascript" language="javascript">
        var codigoGrupo;
        var codigoFator;
        var retornoModal = null;

        function abreEdicaoPesos(codigo, iniciais) {

            //todas as telas vao se abrir com a mesma altura e largura
            var alturaTela = (560 - 110);
            var larguraTela = (900 - 100);

            if (iniciais == "FT")
                showSubModal('CalculoPesoObjetosCategoria.aspx?CF=' + codigo + "&CC=" + tlDados.cp_Categoria + '&larg=' + larguraTela + '&alt=' + alturaTela, "", null, null, atualizaPosModal);
            else if (iniciais == "CT")
                showSubModal('CalculoPesoFatoresCategoria.aspx?CC=' + tlDados.cp_Categoria + '&larg=' + larguraTela + '&alt=' + alturaTela, "", null, null, atualizaPosModal);
            else if (iniciais == "GP")
                showSubModal('CalculoPesoCriteriosCategoria.aspx?CG=' + codigo + '&CC=' + tlDados.cp_Categoria + '&larg=' + larguraTela + '&alt=' + alturaTela, traducao.matrizCategoria_edi__o_de_peso_dos_crit_rios, null, null, atualizaPosModal);

        }

        function validaCamposFormulario() {
            var retorno = "";
            var countMsg = 1;

            if (txtGrupo.GetText() == null || txtGrupo.GetText() == "") {
                retorno += countMsg++ + ")" + " " +  traducao.matrizCategoria_descri__o_do_grupo_deve_ser_informada_ + "\n";
            }
            return retorno;
        }

        function showSubModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal) {

            if (sWidth == null) {
                sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
            }
            if (sHeight == null) {
                sHeight = Math.max(0, document.documentElement.clientHeight) - 190;
            }




            posExecutar = sFuncaoPosModal != "" ? sFuncaoPosModal : null;

            pcModalComFooter.SetWidth(sWidth);
            pcModalComFooter.SetWidth(sWidth);
            pcModalComFooter.SetHeight(sHeight + 50);

            urlModal = sUrl;
            //setTimeout ('alteraUrlModal();', 0);            
            pcModalComFooter.SetHeaderText(sHeaderTitulo);
            pcModalComFooter.SetContentUrl(urlModal);
            pcModalComFooter.Show();

        }

        function fechaModal() {
            pcModalComFooter.Hide();
        }

        function resetaModal() {
            posExecutar = null;
            pcModalComFooter.SetHeaderText("");
            pcModalComFooter.SetContentUrl("../../branco.htm");
            retornoModal = null;
        }

        function atualizaPosModal() {
            tlDados.PerformCallback();
        }

        function abreNovoGrupo(codigo) {
            codigoGrupo = -1;
            txtGrupo.SetText("");
            codigoFator = codigo;
            pcGrupo.Show();
        }

        function editaGrupo(nomeGrupo, codigo) {
            codigoGrupo = codigo;
            txtGrupo.SetText(nomeGrupo);
            pcGrupo.Show();
        }

        function excluiGrupo(codigo) {
            tlDados.PerformCallback("X" + codigo);
        }

        function mostraPopupMensagemGravacao(acao) {
            lblAcaoGravacao.SetText(acao);
            pcMensagemGravacao.Show();
            setTimeout('fechaTelaEdicao();', 1500);
        }

        function fechaTelaEdicao() {
            pcMensagemGravacao.Hide();
            tlDados.cp_msg = "";

            if (tlDados.cp_status != "-1")
                pcGrupo.Hide();
        }

        var __mcat_delimitadorValores = "|";
        var __mcat_delimitadorElementoLista = "¢";

        function preencheListBoxesTela() {
            var parametro = "POPLBX_" + codigoGrupo;

            // busca os status da base de dados
            lbDisponiveisCriterios.PerformCallback(parametro);
            lbSelecionadosCriterios.PerformCallback(parametro);
        }

        function setListBoxItemsInMemory(listBox, inicial) {
            if ((null != listBox) && (null != inicial)) {
                var strConteudo = "";
                var idLista = inicial;
                var nQtdItems = listBox.GetItemCount();
                var item;

                for (var i = 0; i < nQtdItems; i++) {
                    item = listBox.GetItem(i);
                    strConteudo = strConteudo + item.text + __mcat_delimitadorValores + item.value + __mcat_delimitadorElementoLista;
                }

                if (0 < strConteudo.length)
                    strConteudo = strConteudo.substr(0, strConteudo.length - 1);

                // grava a string no hiddenField
                hfCriterios.Set(idLista, strConteudo);
            }

        }
        function habilitaBotoesListBoxes() {
            btnAddAll.SetEnabled(lbDisponiveisCriterios.GetItemCount() > 0);
            btnAddSel.SetEnabled(lbDisponiveisCriterios.GetSelectedItem() != null);

            btnRemoveAll.SetEnabled(lbSelecionadosCriterios.GetItemCount() > 0);
            btnRemoveSel.SetEnabled(lbSelecionadosCriterios.GetSelectedItem() != null);
        }
        function validaBotoesSalvarFechar() {

            var botaoSalvar = window.top.document.getElementById('pcModalComFooter_TPCFm1_btnSalvarPcModal_CD');
            botaoSalvar.style.display = 'none';
            
        }

        function funcaoCallbackFechar() {
            window.top.fechaModalComFooter();

        }

        document.addEventListener("DOMContentLoaded", function (event) {
            validaBotoesSalvarFechar();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" width="100%">
        <div>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <dxwtl:ASPxTreeList ID="tlDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlDados"
                            Width="100%"
                            KeyFieldName="IDObjetoCriterio" ParentFieldName="IDObjetoCriterioSuperior"
                            OnCustomCallback="tlDados_CustomCallback">
                            <Columns>
                                <dxwtl:TreeListTextColumn Caption="Matriz de Prioriza&#231;&#227;o de Projetos" FieldName="NomeObjetoCriterio" VisibleIndex="2"
                                    Width="100%">
                                    <DataCellTemplate>
                                        <%# montaLinksMatriz(Eval("CodigoObjetoCriterio").ToString(), Eval("NomeObjetoCriterio").ToString(), Eval("IniciaisTipoObjetoCriterio").ToString(), Eval("PesoObjetoMatriz").ToString())%>
                                    </DataCellTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dxwtl:TreeListTextColumn>
                                <dxwtl:TreeListTextColumn FieldName="CodigoObjetoCriterio" Visible="False" VisibleIndex="1"
                                    Width="77px">
                                </dxwtl:TreeListTextColumn>
                                <dxwtl:TreeListTextColumn FieldName="IniciaisTipoObjetoCriterio" Visible="False"
                                    VisibleIndex="3">
                                </dxwtl:TreeListTextColumn>
                                <dxwtl:TreeListTextColumn FieldName="PesoObjetoMatriz" Visible="False" VisibleIndex="4">
                                </dxwtl:TreeListTextColumn>
                            </Columns>
                            <SettingsCustomizationWindow PopupHorizontalAlign="RightSides" PopupVerticalAlign="BottomSides"></SettingsCustomizationWindow>
                            <SettingsPopupEditForm VerticalOffset="-1"></SettingsPopupEditForm>
                            <SettingsPopup>
                                <EditForm VerticalOffset="-1"></EditForm>
<HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>
                            <Styles>
                                <Cell>
                                    <Paddings PaddingBottom="5px" PaddingTop="5px" />
                                </Cell>
                                <Header>
                                </Header>
                            </Styles>
                            <Settings ScrollableHeight="430" VerticalScrollBarMode="Visible" ShowColumnHeaders="False" />
                            <SettingsBehavior AutoExpandAllNodes="True" AllowDragDrop="False" AllowSort="False" />
                            <ClientSideEvents EndCallback="function(s, e) {
       if(s.cp_status == &quot;0&quot;)
       {
               if(s.cp_msg != null &amp;&amp; s.cp_msg != &quot;&quot;)
              {
                       window.top.mostraMensagem(s.cp_msg, 'sucesso', false, false, null);
              }
        }
        else
        {
                if(s.cp_status == &quot;-1&quot;)
                {
                       if(s.cp_msg != null &amp;&amp; s.cp_msg != &quot;&quot;)
                       {
                                 window.top.mostraMensagem(s.cp_msg, 'erro', true, false, null);
                       }                      
               }	
         }
        tlDados.cp_msg = &quot;&quot;;
   if(tlDados.cp_status != &quot;-1&quot;)
   {
       pcGrupo.Hide();
   }
       
}" Init="function(s, e) {
		var sHeight = Math.max(0, document.documentElement.clientHeight) - 50;
       s.SetHeight(sHeight);
}" />
                        </dxwtl:ASPxTreeList>
                    </td>
                </tr>
            </table>

        </div>
        <dxpc:ASPxPopupControl ID="pcGrupo" runat="server" Width="730px"
            ClientInstanceName="pcGrupo" HeaderText="Grupo" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowFooter="True">
            <ClientSideEvents CloseUp="function(s, e) {
    // limpa os itens dos combobox que a pr&#243;xima vez que mostrar, 
    // evitar o efeito de ver os dados desaparecendo e reaparecendo na tela
    lbDisponiveisCriterios.ClearItems();
    lbSelecionadosCriterios.ClearItems();
}"
                PopUp="function(s, e) {
	// limpa o hidden field de grupos
	hfCriterios.Clear();
	preencheListBoxesTela();
}"></ClientSideEvents>
            <FooterTemplate>
                <table style="width:100%">
                    <tr>
                        <td align="right">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <dxtv:ASPxButton ID="btnSalvarPcGrupo" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarPcGrupo" Text="Salvar" Theme="MaterialCompact" Width="90px">
                                            <ClientSideEvents Click="function(s, e) {
           var valido = validaCamposFormulario();
           if(valido == &quot;&quot;)
           {
                    if(codigoGrupo == -1)
	    {
		tlDados.PerformCallback(&quot;I&quot; + codigoFator);
	    }
	    else
	    {
		tlDados.PerformCallback(&quot;E&quot; + codigoGrupo);
	    }
           }
          else
          {
                     window.top.mostraMensagem(valido, 'atencao',true, false, null);
          }
}" />
                                            <Paddings Padding="0px" />
                                        </dxtv:ASPxButton>
                                    </td>
                                    <td align="right" style="padding-left: 10px;">
                                        <dxtv:ASPxButton ID="btnFecharPcGrupo" runat="server" AutoPostBack="False" ClientInstanceName="btnFecharPcGrupo" Text="Fechar" Theme="MaterialCompact" Width="90px">
                                            <ClientSideEvents Click="function(s, e) {
pcGrupo.Hide();
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
                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel runat="server" Text="Grupo:" ID="ASPxLabel1"></dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom: 10px">
                                    <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="50" ClientInstanceName="txtGrupo" ID="txtGrupo"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">
                                    <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 310px">
                                                                    <dxe:ASPxLabel runat="server" Text="Crit&#233;rios Dispon&#237;veis:" ID="ASPxLabel6"></dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 70px" align="center"></td>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="Crit&#233;rios Selecionados:" ID="ASPxLabel7"></dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="3" ClientInstanceName="lbDisponiveisCriterios" EnableClientSideAPI="True" Width="100%" Height="220px" ID="lbDisponiveisCriterios" OnCallback="lbDisponiveisCriterios_Callback">
                                                                        <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Disp_');
}"
                                                                            SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}"
                                                                            Init="function(s, e) {
	habilitaBotoesListBoxes();
}"></ClientSideEvents>

                                                                        <ValidationSettings>
                                                                            <ErrorImage Height="14px" Width="14px"></ErrorImage>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxListBox>
                                                                </td>
                                                                <td style="width: 70px" valign="middle" align="center">
                                                                    <table cellspacing="0" cellpadding="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="height: 28px">
                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                                        ClientInstanceName="btnAddAll" ClientEnabled="False" Text="&gt;&gt;"
                                                                                        EncodeHtml="False" Width="60px" Height="25px" Font-Bold="True"
                                                                                        ToolTip="Selecionar todos os status"
                                                                                        ID="btnAddAll">
                                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbDisponiveisCriterios,lbSelecionadosCriterios);
	setListBoxItemsInMemory(lbDisponiveisCriterios,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosCriterios,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>

                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 28px">
                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                                        ClientInstanceName="btnAddSel" ClientEnabled="False" Text="&gt;"
                                                                                        EncodeHtml="False" Width="60px" Height="25px" Font-Bold="True"
                                                                                        ToolTip="Selecionar os status marcados"
                                                                                        ID="btnAddSel">
                                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbDisponiveisCriterios, lbSelecionadosCriterios);
	setListBoxItemsInMemory(lbDisponiveisCriterios,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosCriterios,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>

                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 28px">
                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                                        ClientInstanceName="btnRemoveSel" ClientEnabled="False" Text="&lt;"
                                                                                        EncodeHtml="False" Width="60px" Height="25px" Font-Bold="True"
                                                                                        ToolTip="Retirar da sele&#231;&#227;o os status marcados" ID="btnRemoveSel">
                                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbSelecionadosCriterios, lbDisponiveisCriterios);
	setListBoxItemsInMemory(lbDisponiveisCriterios,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosCriterios,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>

                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 28px">
                                                                                    <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                                                        ClientInstanceName="btnRemoveAll" ClientEnabled="False" Text="&lt;&lt;"
                                                                                        EncodeHtml="False" Width="60px" Height="25px" Font-Bold="True"
                                                                                        ToolTip="Retirar da sele&#231;&#227;o todos os status" ID="btnRemoveAll">
                                                                                        <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbSelecionadosCriterios, lbDisponiveisCriterios);
	setListBoxItemsInMemory(lbDisponiveisCriterios,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosCriterios,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>

                                                                                        <Paddings Padding="0px"></Paddings>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <td valign="top">
                                                                    <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="4" ClientInstanceName="lbSelecionadosCriterios" EnableClientSideAPI="True" Width="100%" Height="220px" ID="lbSelecionadosCriterios" OnCallback="lbSelecionadosCriterios_Callback">
                                                                        <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Sel_');
	setListBoxItemsInMemory(s,'InDB_');
	habilitaBotoesListBoxes();
}"
                                                                            SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}"></ClientSideEvents>

                                                                        <ValidationSettings>
                                                                            <ErrorImage Height="14px" Width="14px"></ErrorImage>
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxListBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfCriterios" ID="hfCriterios"></dxhf:ASPxHiddenField>
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

            <HeaderStyle></HeaderStyle>
        </dxpc:ASPxPopupControl>
        
        <dxpc:ASPxPopupControl ID="pcModalComFooter" runat="server" ClientInstanceName="pcModalComFooter" HeaderText="" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" AllowDragging="True" AllowResize="True" CloseAction="CloseButton" PopupVerticalOffset="20" ShowFooter="True">
            <FooterTemplate>
                <table style="width:100%">
                    <tr>
                        <td align="right">
                            <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right">
                            <dxtv:ASPxButton ID="btnSalvarPcModal" runat="server" AutoPostBack="False" Text="Salvar" Width="90px" ClientInstanceName="btnSalvarPcModal" Theme="MaterialCompact">
                                <ClientSideEvents Click="function(s, e) {
                                    pcModalComFooter.GetContentIFrameWindow().funcaoCallbackSalvar();
}" />
                                <Paddings Padding="0px" />
                            </dxtv:ASPxButton>
                        </td>
                        <td align="right" style="padding-left: 10px;">
                            <dxtv:ASPxButton ID="btnFecharPcModal" runat="server" AutoPostBack="False" Text="Fechar" Width="90px" ClientInstanceName="btnFecharPcModal" Theme="MaterialCompact">
                                <ClientSideEvents Click="function(s, e) {
	                                pcModalComFooter.GetContentIFrameWindow().funcaoCallbackFechar();

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
            <HeaderTemplate>
               <div style="display:flex;flex-direction:row;width:100%">
                  <div style="width:50%"  id="divTextoCabecalhoComFooter"></div> 
                  <div style="width:50%">                      
                      <div  id="divBotaoFechar0" style="float:right;"><i class="fas fa-times-circle" style="color:gainsboro;cursor:pointer" onclick="pcModalComFooter.GetContentIFrameWindow().funcaoCallbackFechar();"></i> </div>
                  </div>                   
                </div>
            </HeaderTemplate>
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl13" runat="server">
                    <%--<iframe id="frmModal" frameborder="0" name="frmModal" style="overflow: hidden; padding: 0px; margin: 0px;"></iframe>--%>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents CloseUp="function(s, e) {
            var retorno = '';
            
            if(retornoModal != null)
            {   
                retorno = retornoModal;
            }
            
            if(posExecutar != null)
               posExecutar(retorno);
                
            resetaModal();
}" />
            <ContentStyle>
                <Paddings Padding="5px" />
            </ContentStyle>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
