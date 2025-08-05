<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupArvoresConhecimentoEDT.aspx.cs" Inherits="_Projetos_Administracao_LicoesAprendidas_popupArvoresConhecimentoEDT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .iniciaisMaiusculas {
            text-transform: capitalize !important
        }
    </style>
    <script type="text/javascript" language="javascript">
        var comando;
        var modo = '';
        function editar(chave) {
            tlArvore.GetNodeValues(chave, "CodigoElementoArvore;IndicaElementoFolha;DescricaoElementoArvore;NivelCriticidadeConhecimento;DescricaoNIvelCriticidade;IndicaPodeExcluir;CodigoEstruturaAnalitica", function (valores) {
                hfGeral.Set('modo', 'E');
                var CodigoElementoArvore = (valores[0] == null) ? "" : valores[0].toString();
                var IndicaElementoFolha = (valores[1] == null) ? "" : valores[1].toString();
                var DescricaoElementoArvore = (valores[2] == null) ? "" : valores[2].toString();
                var NivelCriticidadeConhecimento = (valores[3] == null) ? "" : valores[3].toString();
                var DescricaoNIvelCriticidade = (valores[4] == null) ? "" : valores[4].toString();
                var IndicaPodeExcluir = (valores[5] == null) ? "" : valores[5].toString();
                var CodigoEstruturaAnalitica = (valores[6] == null) ? "" : valores[6].toString();

                txtDescricaoElementoArvore.SetText(DescricaoElementoArvore);
                radioIndicaElementoFolha.SetValue(IndicaElementoFolha);
                comboCriticidade.SetValue(NivelCriticidadeConhecimento);
                comboCriticidade.SetText(DescricaoNIvelCriticidade);

                //Se a coluna IndicaPodeExcluir = "N"  OU a coluna CodigoEstruturaAnalitica = 1 então impedir alteração.
                if ((IndicaPodeExcluir == 'N') || (CodigoEstruturaAnalitica.indexOf('.') < 0)) {
                    radioIndicaElementoFolha.SetEnabled(false);
                }
                else {
                    radioIndicaElementoFolha.SetEnabled(true);
                }

                //Criticidade do conhecimento (este campo só deverá aparecer se radiobutton estiver "Sim")
                if (IndicaElementoFolha == 'S') {
                    divLabelCriticidade.style.display = 'block';
                    divComboCriticidade.style.display = 'block';
                }
                else {
                    divLabelCriticidade.style.display = 'none';
                    divComboCriticidade.style.display = 'none';
                }
                pcDados.Show();
            });
        }

        function processaInclusao() {
            hfGeral.Set('modo', 'I');
            txtDescricaoElementoArvore.SetText('');
            radioIndicaElementoFolha.SetSelectedIndex(-1);

            comboCriticidade.SetValue(null);

            txtDescricaoElementoArvore.SetEnabled(true);
            radioIndicaElementoFolha.SetEnabled(true);
            comboCriticidade.SetEnabled(true);
            btnSalvar.SetEnabled(true);

            divLabelCriticidade.style.display = 'none';
            divComboCriticidade.style.display = 'none';


            pcDados.Show();
        }

        function Trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function busca(strChildNodes) {
            var retorno = false;
            var jsonChildNodes = JSON.parse(strChildNodes);

            var valorBuscado = jsonChildNodes.filter(function (jsonChildNodes) {
                return jsonChildNodes == txtDescricaoElementoArvore.GetText();
            });
            return valorBuscado.length > 0;
        }

        function validaTela() {
            var mensagemError = "";
            var numError = 0;
            if (busca(tlArvore.cpChildNodes) == true) {
                mensagemError += ++numError + ") Já existe um item com este mesmo nome neste nó\n";
            }
            if (Trim(txtDescricaoElementoArvore.GetText()) == "") {
                mensagemError += ++numError + ") Elemento deve ser informado\n";
            }
            if (radioIndicaElementoFolha.GetValue() == null) {
                mensagemError += ++numError + ") Por favor, informe se o elemento em questão é o útimo nível\n";
            }
            if (radioIndicaElementoFolha.GetValue() == 'S') {
                if (comboCriticidade.GetValue() == null) {
                    mensagemError += ++numError + ") Criticidade deve ser informada\n";
                }
            }
            return mensagemError;
        }

        function excluir(chave) {
            var funcObj = { funcaoClickOK: function () { tlArvore.DeleteNode(chave); } }
            window.top.mostraConfirmacao('Deseja realmente excluir o registro?', function () { funcObj['funcaoClickOK']() }, null);

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: flex; flex-direction: column">
            <div style="visibility: visible; padding-top: 5px; padding-left: 10px; padding-right: 5px">
                <table id="tabMenuInclusao">
                    <tr>
                        <td align="center">
                            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                ClientInstanceName="menu"
                                ItemSpacing="5px" OnInit="menu_Init1">
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
            </div>
            <div id="divTreeList" style="visibility: visible; padding-top: 5px; padding-left: 10px; padding-right: 5px">
                <dxwtl:ASPxTreeListExporter ID="ASPxTreeListExporter2" runat="server" OnRenderBrick="ASPxTreeListExporter1_RenderBrick" TreeListID="tlArvore">
                </dxwtl:ASPxTreeListExporter>
                <dxwtl:ASPxTreeList ID="tlArvore" runat="server" AutoGenerateColumns="False" KeyFieldName="CodigoElementoArvore" ParentFieldName="CodigoElementoArvoreSuperior" ClientInstanceName="tlArvore" Width="100%" OnNodeDeleting="tlArvore_NodeDeleting" OnFocusedNodeChanged="tlArvore_FocusedNodeChanged">
                    <SettingsSelection Enabled="False" />
                    <Columns>
                        <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="CodigoElemento" FieldName="CodigoElementoArvore" Name="CodigoElemento" ShowInFilterControl="Default" Visible="False" VisibleIndex="1">
                        </dxwtle:TreeListTextColumn>
                        <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="É último nível?" FieldName="IndicaElementoFolha" Name="IndicaElementoFolha" ShowInFilterControl="Default" Visible="False" VisibleIndex="2">
                            <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="False" />
                        </dxwtle:TreeListTextColumn>
                        <dxwtle:TreeListTextColumn AutoFilterCondition="Contains" FieldName="DescricaoElementoArvore" Name="TituloElemento" ShowInFilterControl="Default" VisibleIndex="3" Caption=" Descrição">
                            <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="False" />
                            <DataCellTemplate>
                                <%# getDescricaoObjetosLista()%>
                            </DataCellTemplate>
                        </dxwtle:TreeListTextColumn>
                        <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="Criticidade do conhecimento" FieldName="DescricaoNIvelCriticidade" Name="DescricaoNIvelCriticidade" ShowInFilterControl="Default" VisibleIndex="4" AllowAutoFilter="False">
                            <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="False" />
                        </dxwtle:TreeListTextColumn>
                        <dxwtle:TreeListTextColumn AutoFilterCondition="Default" Caption="CodigoElementoSuperior" FieldName="CodigoElementoArvoreSuperior" Name="CodigoElementoSuperior" ShowInFilterControl="Default" Visible="False" VisibleIndex="5">
                        </dxwtle:TreeListTextColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" VerticalScrollBarMode="Visible" />
                    <SettingsBehavior AllowSort="False" AutoExpandAllNodes="True" ExpandNodesOnFiltering="True" AllowFocusedNode="True" />
                    <SettingsEditing Mode="PopupEditForm">
                    </SettingsEditing>
                    <SettingsPopupEditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Width="500px" ShowPinButton="true">
                    </SettingsPopupEditForm>
                    <SettingsPopup>
                        <EditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Width="500px" Modal="true" MinHeight="500px">
                        </EditForm>
                        <HeaderFilter MinHeight="250px"></HeaderFilter>
                    </SettingsPopup>

                    <ClientSideEvents
                        Init="function(s, e) {	
     var sHeight = Math.max(0, document.documentElement.clientHeight) - 120;
     s.SetHeight(sHeight);
}"
                        BeginCallback="function(s, e) {
	comando = e.command;
}"
                        EndCallback="function(s, e) {
         //alert(comando);	 
          if(s.cp_textoMsg != undefined &amp;&amp; s.cp_textoMsg != '')
         {          
              var temp = s.cp_textoMsg;
               s.cp_textoMsg = '';
               window.top.mostraMensagem(temp, s.cp_nomeImagem, (s.cp_mostraBtnOK == 'true'), (s.cp_mostraBtnCancelar == 'true'), null, parseInt(s.cp_timeout));
         }
        if(comando == 'CustomCallback')
       {
             s.ExpandNode(s.GetFocusedNodeKey());
       }
}"
                        FocusedNodeChanged="function(s, e) {
    try
    {
               s.GetNodeValues(s.GetFocusedNodeKey(), 'IndicaElementoFolha', function(indicafolha){
                 if(indicafolha == 'S')
                 {
                          document.getElementById('tabMenuInclusao').style.visibility = 'hidden';
                }
                else
               {
                          document.getElementById('tabMenuInclusao').style.visibility = 'visible';
               }
       });
    }
    catch(e){}

}" />
                </dxwtl:ASPxTreeList>

            </div>
            <div>
                <div id="divDosBotoes" style="display: flex; flex-direction: row-reverse; align-content: flex-end; visibility: visible">
                    <div style="margin: 5px">
                        <dxcp:ASPxButton ID="btnFechar" ClientInstanceName="btnFechar" runat="server" Text="Fechar" CssClass="iniciaisMaiusculas" Width="100px">
                            <ClientSideEvents Click="function(s, e) {
    //window.top.retornoModal = popup_aco_CodigoArvoreConhecimento.toString();
    window.top.fechaModal();
}" />
                        </dxcp:ASPxButton>
                    </div>
                </div>
            </div>
        </div>
        <dxpc:ASPxPopupControl runat="server" AllowDragging="True" ClientInstanceName="pcDados" Modal="true"
            CloseAction="None" HeaderText="Detalhes" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" ShowCloseButton="False" Width="730px"
            ID="pcDados">
            <ContentStyle>
                <Paddings Padding="5px"></Paddings>
            </ContentStyle>
            <HeaderStyle Font-Bold="True"></HeaderStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <div style="display: flex; flex-direction: column">
                        <div style="margin: 5px">
                            <dxtv:ASPxLabel ID="ASPxLabel1" runat="server" Text="Descrição" Width="100%">
                            </dxtv:ASPxLabel>
                        </div>
                        <div style="margin: 5px">
                            <dxtv:ASPxTextBox ID="txtDescricaoElementoArvore" ClientInstanceName="txtDescricaoElementoArvore" runat="server" Width="100%" MaxLength="50">
                            </dxtv:ASPxTextBox>
                        </div>
                        <div style="margin: 5px">
                            <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" Text="É último nível?" Width="100%">
                            </dxtv:ASPxLabel>
                        </div>
                        <div style="margin: 5px">
                            <dxcp:ASPxRadioButtonList ID="radioIndicaElementoFolha" ClientInstanceName="radioIndicaElementoFolha" runat="server"
                                ValueType="System.String" Width="100%" RepeatDirection="Horizontal" SelectedIndex="-1">
                                <ClientSideEvents Init="function(s, e) {
      if(s.GetValue() == 'S')
      {
              divLabelCriticidade.style.display = 'block';
              divComboCriticidade.style.display = 'block';   
      }
      else 
     {
               comboCriticidade.SetValue(null);
               divLabelCriticidade.style.display = 'none';
               divComboCriticidade.style.display = 'none';
     }
}"
                                    SelectedIndexChanged="function(s, e) {
      if(s.GetValue() == 'S')
      {
              divLabelCriticidade.style.display = 'block';
              divComboCriticidade.style.display = 'block';   
      }
      else 
     {
               comboCriticidade.SetValue(null);
               divLabelCriticidade.style.display = 'none';
               divComboCriticidade.style.display = 'none';
     }
}" />
                                <Items>
                                    <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Não" Value="N"></dxe:ListEditItem>
                                </Items>
                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                </DisabledStyle>
                            </dxcp:ASPxRadioButtonList>
                        </div>
                        <div style="margin: 5px" id="divLabelCriticidade">
                            <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="Criticidade" Width="100%">
                            </dxtv:ASPxLabel>
                        </div>
                        <div style="margin: 5px" id="divComboCriticidade">
                            <dxcp:ASPxComboBox ID="comboCriticidade" ClientInstanceName="comboCriticidade" runat="server" ValueType="System.String" Width="100%">
                            </dxcp:ASPxComboBox>
                        </div>
                        <div style="margin: 5px">
                            <div style="display: flex; flex-direction: row-reverse">
                                <div style="margin-left: 5px">
                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                        Text="Fechar" Width="90px"
                                        ID="ASPxButton1" CssClass="iniciaisMaiusculas">
                                        <ClientSideEvents Click="function(s, e) {
	                    e.processOnServer = false;
                        pcDados.Hide();
                    }"></ClientSideEvents>
                                        <Paddings Padding="0px"></Paddings>
                                    </dxe:ASPxButton>
                                </div>
                                <div>
                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar"
                                        Text="Salvar" Width="90px"
                                        ID="btnSalvar" CssClass="iniciaisMaiusculas">
                                        <ClientSideEvents Click="function(s, e) {
                                            var msgErro = validaTela();
                                            if(msgErro == '')
                                            {
                                                callbackTela.PerformCallback(hfGeral.Get('modo'));
                                            }
                                            else
                                            {
                                                window.top.mostraMensagem(msgErro, 'erro', true,false, null, null);
                                            }
	
}" />
                                        <Paddings Padding="0px"></Paddings>
                                    </dxe:ASPxButton>
                                </div>
                            </div>
                        </div>
                    </div>

                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        <script type="text/javascript">
            var popup_aco_CodigoArvoreConhecimento;
        </script>
        <dxcp:ASPxCallback ID="callbackTela" runat="server" ClientInstanceName="callbackTela" OnCallback="callbackTela_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
         if(s.cp_textoMsg != undefined &amp;&amp; s.cp_textoMsg != '')
         {          
              var temp = s.cp_textoMsg;
               s.cp_textoMsg = '';
                tlArvore.PerformCallback();
                pcDados.Hide();
               window.top.mostraMensagem(temp, s.cp_nomeImagem, (s.cp_mostraBtnOK == 'true'), (s.cp_mostraBtnCancelar == 'true'), null, parseInt(s.cp_timeout));
         }
	
}" />
        </dxcp:ASPxCallback>
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
    </form>
</body>
</html>
