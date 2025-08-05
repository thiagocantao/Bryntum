<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/novaCdis.master"
    AutoEventWireup="true" CodeFile="adm_edicaoWorkflows.aspx.cs" Inherits="administracao_adm_edicaoWorkflows"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<%@ Register Src="~/administracao/UC_Workflow/uc_crud_caminhoCondicional.pt-BR.ascx" TagPrefix="va" TagName="uc_crud_caminhoCondicionalptBR" %>
<%@ Register Src="~/administracao/UC_Workflow/uc_crud_caminhoCondicional.en-US.ascx" TagPrefix="va" TagName="uc_crud_caminhoCondicionalenUS" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .alinhamentoImage {
            vertical-align: bottom
        }

        p.small {
            line-height: 0.8;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr style="height: 26px">
            <td valign="middle" style="padding-left: 10px">
                <dxe:ASPxLabel ID="lblTituloTela" runat="server" ClientInstanceName="lblTitulo" Font-Bold="True"
                    Text="<%$ Resources:traducao, adm_edicaoWorkflows_edi__o_de_modelo_de_fluxo %>">
                </dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lblDescricaoDaVersaoWf" runat="server" ClientInstanceName="lblDescricaoDaVersaoWf" ClientVisible="false"
                    Font-Bold="True">
                </dxe:ASPxLabel>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tbody>
            <tr>
                <td id="ConteudoPrincipal">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td valign="top"></td>
                            <td valign="top">
                                <dxm:ASPxMenu ID="mnTelaEdicaoWorkflow" runat="server" ClientInstanceName="mnTelaEdicaoWorkflow"
                                    EnableClientSideAPI="True">
                                    <Paddings PaddingTop="0px" PaddingBottom="0px" />
                                    <ItemStyle>
                                        <Paddings PaddingTop="5px" PaddingBottom="5px" />
                                    </ItemStyle>
                                    <ClientSideEvents ItemClick="function(s, e) {
    var opcao = e.item.name; //.substring(2,e.item.name.length);
    var subOpcao = e.item.name.substring(3, e.item.name.length);
    var textoOpcao = e.item.GetText();
    if (&quot;salvar&quot; == opcao) {
        if (true == onBtnSalvarClick(s, e)) {
            mostraDivLoading();
            pnlCbkEdicaoElementos.PerformCallback(opcao);
        }
    }
    else if (&quot;publicar&quot; == opcao) {
        if (true == onBtnPublicarXml_Click(s, e)) {
            mostraDivLoading();
            pnlCbkEdicaoElementos.PerformCallback(opcao);
        }
    }
    else if (&quot;resumo&quot; == opcao) {
        //        if (true == onBtnPublicarXml_Click(s, e)) {
        //mostraDivLoading();
        //           pnlCbkEdicaoElementos.PerformCallback(opcao);
        tlResumoWf.PerformCallback();
        //        }
    }

    else if (&quot;btnVoltarTela&quot; == opcao) {

        window.top.gotoURL('administracao/adm_CadastroWorkflows.aspx', '_self');
    }
    else if (opcao.substring(0, 3) == &quot;mne&quot;) {
        if (&quot;Propriedades&quot; == subOpcao) {
            inicializarPropiedadesWF();
            pcConfiguracaoWf.Show();
        }
        else {
            lblTituloElemento.SetText(textoOpcao);
            lpLoading.Show();
            pnlCbkEdicaoElementos.PerformCallback(subOpcao);           
        }
    }
    e.processOnServer = false;
}"></ClientSideEvents>
                                    <Items>
                                        <dxm:MenuItem Name="editar" Text="<%$ Resources:traducao, adm_edicaoWorkflows_editar %>" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_editar_elementos_do_fluxo_ %>">
                                            <Items>
                                                <dxm:MenuItem GroupName="grp1" Name="mneEtapas" Text="<%$ Resources:traducao, adm_edicaoWorkflows_etapas %>">
                                                    <SubMenuStyle GutterWidth="0px"></SubMenuStyle>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem GroupName="grp1" Name="mneConectores" Text="<%$ Resources:traducao, adm_edicaoWorkflows_conectores %>">
                                                    <SubMenuStyle GutterWidth="0px"></SubMenuStyle>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem GroupName="grp1" Name="mneCaminhoCondicional" Text="<%$ Resources:traducao, adm_edicaoWorkflows_caminhos_condicionais %>">
                                                    <SubMenuStyle GutterWidth="0px"></SubMenuStyle>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem GroupName="grp1" Name="mneSubprocessos" Text="<%$ Resources:traducao, adm_edicaoWorkflows_subprocesos %>">
                                                    <SubMenuStyle GutterWidth="0px"></SubMenuStyle>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem GroupName="grp1" Name="mneTimers" Text="Timers">
                                                    <SubMenuStyle GutterWidth="0px"></SubMenuStyle>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem GroupName="grp1" Name="mneDisjuncoes" Text="<%$ Resources:traducao, adm_edicaoWorkflows_disjun__es %>">
                                                    <SubMenuStyle GutterWidth="0px"></SubMenuStyle>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem GroupName="grp1" Name="mneJuncoes" Text="<%$ Resources:traducao, adm_edicaoWorkflows_jun__es %>">
                                                    <SubMenuStyle GutterWidth="0px"></SubMenuStyle>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem GroupName="grp1" Name="mneFins" Text="<%$ Resources:traducao, adm_edicaoWorkflows_fins %>">
                                                    <SubMenuStyle GutterWidth="0px"></SubMenuStyle>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem ClientVisible="False" GroupName="grp1" Name="mnePropriedades" Text="<%$ Resources:traducao, adm_edicaoWorkflows_propriedades %>"
                                                    Visible="False">
                                                    <Image Url="~/imagens/Configuracao_14x14.png">
                                                    </Image>
                                                </dxm:MenuItem>
                                                <dxm:MenuItem GroupName="grp1" Name="mneAcaoWf" Text="<%$ Resources:traducao, adm_edicaoWorkflows_a__es_do_fluxo %>">
                                                    <Image Url="~/imagens/Workflow/acaoAutomaticaWf.png">
                                                    </Image>
                                                </dxm:MenuItem>
                                            </Items>
                                            <Image Url="~/imagens/Workflow/editarW.png">
                                            </Image>
                                            <SubMenuStyle GutterWidth="0px"></SubMenuStyle>
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="salvar" Text="<%$ Resources:traducao, adm_edicaoWorkflows_salvar %>" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_gravar_o_modelo_na_base_de_dados_ %>">
                                            <Image Url="~/imagens/Workflow/salvarW.png">
                                            </Image>
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="publicar" Text="<%$ Resources:traducao, adm_edicaoWorkflows_publicar %>" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_publicar_o_modelo_para_uso_no_sistema_ %>">
                                            <Image Url="~/imagens/Workflow/publicarW.png">
                                            </Image>
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="resumo" Text="<%$ Resources:traducao, adm_edicaoWorkflows_relat_rio_resumo %>" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_exibe_o_relat_rio_resumo_do_modelo_de_fluxo %>">
                                            <Image Url="~/imagens/Workflow/relatorioResumo.jpg">
                                            </Image>
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Name="btnVoltarTela" Text="<%$ Resources:traducao, adm_edicaoWorkflows_voltar %>" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_volta_pra_lista_de_modelos_de_fluxos %>">
                                            <Image Url="~/imagens/left-arrow.png" Width="17px" Height="21px">
                                            </Image>
                                        </dxm:MenuItem>
                                    </Items>
                                </dxm:ASPxMenu>

                            </td>
                        </tr>
                        <tr>
                            <td style="width: 59px; padding-bottom: 0px; padding-top: 0px" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 62px;">
                                    <tr>
                                        <td align="center">
                                            <p class="small">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../imagens/workflow/etapa001.jpg"
                                                    OnClientClick="return trataClickNewElementButton(__wf_cTipoElementoEtapa);" Width="20px"
                                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_incluir_etapas %>" Height="20px" CssClass="alinhamentoImage" BorderWidth="0px" />
                                                <br />
                                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nova_etapa %>" Font-Size="7pt"></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <p class="small">
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../imagens/workflow/conector001.jpg"
                                                    Height="15px" Width="20px" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_conectar_etapas %>" BorderWidth="0px" CssClass="alinhamentoImage" OnClientClick="return trataClickNewElementButton(__wf_cTipoElementoConector);" />
                                                <br />
                                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_conector_de_etapas %>" Font-Size="7pt" BorderWidth="0px"></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                    <!--- ACG: 23/11/2015 (Início)-->
                                    <tr id="tr_MenuEsquerdoCaminhoCondicional" runat="server">
                                        <td align="center">
                                            <p class="small">
                                                <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="../imagens/workflow/condicional.png"
                                                    Width="20px" Height="20px" CssClass="alinhamentoImage" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_definir_caminhos_condicionais %>" OnClientClick='return trataClickNewElementButton(__wf_cTipoElementoCondicao);' />
                                                <br />
                                                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_caminho_condicional %>" Font-Size="7pt" BorderWidth="0px"></asp:Label>
                                            </p>

                                        </td>
                                    </tr>
                                    <!--- ACG: 23/11/2015 (Fim)-->
                                    <tr id="tr_MenuEsquerdoSubProcesso" runat="server">
                                        <td align="center">
                                            <p class="small">
                                                <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="../imagens/workflow/subprocesso.png"
                                                    Width="15px" Height="15px" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_novo_subprocesso %>" CssClass="alinhamentoImage" OnClientClick='return trataClickNewElementButton(__wf_cTipoElementoSubprocesso);' />
                                                <br />
                                                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_subprocesso %>" Font-Size="7pt" BorderWidth="0px"></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <p class="small">
                                                <asp:ImageButton ID="ImageButton3" runat="server" Height="20px" ImageUrl="~/imagens/Workflow/clock0062.png"
                                                    OnClientClick="return trataClickNewElementButton(__wf_cTipoElementoTimer);" Width="20px"
                                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_definir_tempo_de_espera %>" CssClass="alinhamentoImage" />
                                                <br />
                                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_tempo_de_espera %>" Font-Size="7pt" BorderWidth="0px"></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <p class="small">
                                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/imagens/Workflow/disjunction002.png" CssClass="alinhamentoImage"
                                                    Width="25px" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_iniciar_execu__o_paralela__disjun__o_ %>" Height="20px" OnClientClick='return trataClickNewElementButton(__wf_cTipoElementoDisjuncao);' />
                                                <br />
                                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_in_cio_de_execu__o_paralela %>" Font-Size="7pt"></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <p class="small">
                                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="../imagens/workflow/junction023.png"
                                                    Width="25px" Height="20px" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_finalizar_execu__o_paralela__jun__o_ %>" OnClientClick='return trataClickNewElementButton(__wf_cTipoElementoJuncao);' />
                                                <br />
                                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_fim_de_execu__o_paralela %>" Font-Size="7pt" BorderWidth="0px"></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <p class="small">
                                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="../imagens/workflow/end001.JPG"
                                                    Width="15px" Height="15px" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_finalizar_fluxo %>" OnClientClick='return trataClickNewElementButton(__wf_cTipoElementoFim);' />
                                                <br />
                                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_fim_de_fluxo %>" Font-Size="7pt" BorderWidth="0px"></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <dxp:ASPxPanel ID="pnFlash" runat="server" ClientInstanceName="pnFlash" Width="100%">
                                    <ClientSideEvents Init="function(s, e) {
                                        var sHeight = Math.max(0, document.documentElement.clientHeight) - 180;
                                        s.SetHeight(sHeight);
                                        }" />
                                    <PanelCollection>
                                        <dxp:PanelContent ID="PanelContent1" runat="server">
                                            <div id="divFlash">
                                            </div>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxp:ASPxPanel>
                                <table>
                                    <tr>
                                        <td valign="top" style="display: flex; flex-direction: row;">
                                            <dxe:ASPxLabel ID="Aspxlabel7" runat="server" Font-Size="7pt"
                                                Text="<%$ Resources:traducao, adm_edicaoWorkflows_conectores_com_a__es_autom_ticas__ %>">
                                            </dxe:ASPxLabel>
                                            <dxe:ASPxLabel ID="lblDescricaoAcoes" runat="server" ClientInstanceName="lblDescricaoAcoes"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="LightSkyBlue"
                                                Text="- - -">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <dxhf:ASPxHiddenField ID="hfValoresTela" runat="server" ClientInstanceName="hfValoresTela">
    </dxhf:ASPxHiddenField>
    <dxcp:ASPxLoadingPanel ID="lpLoading" runat="server" ClientInstanceName="lpLoading">
    </dxcp:ASPxLoadingPanel>
    <%-- ---------[DIV EDIÇÃO DO ELEMENTOS --%>
    <div id="DivDisenhando" style="left: 49%; top: 331px; width: 200px; position: absolute; height: 42px; display: none;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" valign="middle">
                    <img src="../imagens/Workflow/loading2.gif" alt="" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="__wf_divFundo">
    </div>
    <script type="text/javascript">
        document.getElementById("DivDisenhando").style.display = "";
    </script>
    <%# getDescricaoObjeto() %>
    <dxpc:ASPxPopupControl ID="divConector" runat="server" ClientInstanceName="divConector"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="False"
        Width="950px" AllowDragging="True" DragElement="Window" PopupVerticalOffset="15">
        <ClientSideEvents PopUp="function(s, e) 
{
	 var sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
                 var sHeight = Math.max(0, document.documentElement.clientHeight) - 100;        
       s.SetWidth(sWidth);
       s.SetHeight(sHeight); 

        sHeight = Math.max(0, document.documentElement.clientHeight) - 290;
         gv_GruposNotificados_cnt.SetHeight(sHeight);
       pcTipoMensagemAcao.SetHeight(sHeight - 40);
       mmTexto1_cnt.style.height = (sHeight - 115) + 'px';
       mmTexto2_cnt.style.height = (sHeight - 115) + 'px';
       s.UpdatePosition();
}" />
        <ContentStyle>
            <Paddings Padding="0px" PaddingBottom="5px" PaddingLeft="5px" PaddingRight="5px" />
        </ContentStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td id="handleConector">
                            <table border="0" cellpadding="0" cellspacing="0" style="height: 25px; background-color: #DCDCDC"
                                width="100%">
                                <tr>
                                    <td align="center" valign="middle">
                                        <asp:HiddenField runat="server" ID="posicaoTextArea" />
                                        <dxe:ASPxLabel ID="lblCaptionConector" runat="server" Font-Bold="True"
                                            Text="<%$ Resources:traducao, adm_edicaoWorkflows_informa__es_do_conector__ %>" ClientInstanceName="lblCaptionConector">
                                        </dxe:ASPxLabel>
                                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Font-Bold="True" Text="<%$ Resources:traducao, adm_edicaoWorkflows_modelo_de_fluxo_ %>">
                                        </dxe:ASPxLabel>
                                        <dxe:ASPxLabel ID="lbWorkflow_cnt" runat="server" ClientInstanceName="lbWorkflow_cnt"
                                            EnableClientSideAPI="True" Font-Bold="True" ForeColor="#C04000">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="center" style="width: 30px; height: 25px" valign="middle">
                                        <dxe:ASPxImage ID="ASPxImage3" runat="server" Cursor="pointer" ImageUrl="~/imagens/botoes/close2.png"
                                            ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_fechar %>">
                                            <ClientSideEvents Click="function(s, e) {
        divConector.Hide();
        e.processOnServer = false;			
        }"></ClientSideEvents>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <dxcp:ASPxCallbackPanel ID="painelCallbackConectores" runat="server" ClientInstanceName="painelCallbackConectores"
                    OnCallback="painelCallbackConectores_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent3" runat="server">
                            <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td id="tdEtapas">
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td valign="top">
                                                            <table cellspacing="0" cellpadding="0" style="width: 100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_etapa_origem_ %>"
                                                                                ID="ASPxLabel15">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 5px">
                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="cmbEtapaOrigem_cnt"
                                                                                TabIndex="1" ID="cmbEtapaOrigem_cnt" DropDownHeight="150px" DropDownStyle="DropDown" EnableSynchronization="True" EnableClientSideAPI="true"
                                                                                IncrementalFilteringMode="Contains">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	OnCmbEtapaOrigem_cntSelectedIndexChanged(s,e);
}"></ClientSideEvents>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td id="tdOpcoes" valign="top">
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_op__o_ %>" ClientInstanceName="lblOpcao_cnt"
                                                                                EnableClientSideAPI="True" ID="lblOpcao_cnt">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 5px">
                                                                            <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="edtAcao_cnt"
                                                                                TabIndex="2" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_informe_a_op__o_que_levar____etapa_de_destino %>"
                                                                                ID="edtAcao_cnt" MaxLength="50">
                                                                                <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}"></ClientSideEvents>
                                                                            </dxe:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_etapa_destino_ %>"
                                                                                ID="ASPxLabel16">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 5px">
                                                                            <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="cmbEtapaDestino_cnt"
                                                                                EnableClientSideAPI="True" TabIndex="3" ID="cmbEtapaDestino_cnt"
                                                                                DropDownHeight="150px" DropDownStyle="DropDown" IncrementalFilteringMode="Contains"
                                                                                EnableSynchronization="True">
                                                                                <DropDownButton>
                                                                                    <Image Width="9px">
                                                                                    </Image>
                                                                                </DropDownButton>
                                                                                <ButtonEditEllipsisImage Width="12px">
                                                                                </ButtonEditEllipsisImage>
                                                                                <ValidationSettings>
                                                                                    <ErrorImage Width="14px">
                                                                                    </ErrorImage>
                                                                                </ValidationSettings>
                                                                            </dxe:ASPxComboBox>
                                                                        </td>

                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td valign="top" style="width: 115px">
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <dxtv:ASPxLabel ID="ASPxLabel8" runat="server" ClientInstanceName="lblCor_cnt" EnableClientSideAPI="True" Text="<%$ Resources:traducao, adm_edicaoWorkflows_cor_do_bot_o_ %>">
                                                                            </dxtv:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-right: 5px">
                                                                            <dxcp:ASPxColorEdit ID="ceCorBotao_cnt" ClientInstanceName="ceCorBotao_cnt" runat="server" Width="100%"></dxcp:ASPxColorEdit>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td style="width: 115px" valign="top">
                                                            <table cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <dxtv:ASPxLabel ID="ASPxLabel29" runat="server" EnableClientSideAPI="True" Text="<%$ Resources:traducao, adm_edicaoWorkflows__cone_do_bot_o_ %>">
                                                                        </dxtv:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxtv:ASPxComboBox ID="cmbIconeBotao_cnt" runat="server" ClientInstanceName="cmbIconeBotao_cnt" EncodeHtml="False" Width="100%">
                                                                        </dxtv:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxp:ASPxPanel runat="server" ClientInstanceName="paDadosAuxConector" Width="100%"
                                                ID="paDadosAuxConector">
                                                <PanelCollection>
                                                    <dxp:PanelContent ID="PanelContent40" runat="server">
                                                        <table id="tdDadosAuxConector" cellspacing="0" cellpadding="0"
                                                            border="0" style="width: 100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td id="tdSolicAssinaturaDigital">
                                                                        <dxtv:ASPxCheckBox ID="cbSolicitarAssinaturaDigital" runat="server" CheckState="Unchecked"
                                                                            ClientInstanceName="cbSolicitarAssinaturaDigital"
                                                                            TabIndex="3" Text="Exige a assinatura digital" TextSpacing="2px" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_marcar_se_for_para_exigir_assinatura_digital_quando_o_fluxo_percorrer_essa_conex_o %>"
                                                                            Width="200px">
                                                                        </dxtv:ASPxCheckBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxtv:ASPxPageControl ID="tcDivConector" runat="server" ActiveTabIndex="0" ClientInstanceName="tcDivConector" EnableClientSideAPI="True" TabIndex="4" TabSpacing="3px" Width="100%">
                                                                            <TabPages>
                                                                                <dxtv:TabPage Text="<%$ Resources:traducao, adm_edicaoWorkflows_perfis_notificados %>" Name="tab_gv_GruposNotificados_cnt">
                                                                                    <TabStyle Wrap="False">
                                                                                    </TabStyle>
                                                                                    <ContentCollection>
                                                                                        <dxtv:ContentControl runat="server">
                                                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                <tr>
                                                                                                    <td style="width: 50%">
                                                                                                        <dxtv:ASPxGridView ID="gv_GruposNotificados_cnt" runat="server" AutoGenerateColumns="False" ClientInstanceName="gv_GruposNotificados_cnt" KeyFieldName="CodigoPerfilWf" OnAfterPerformCallback="gv_GruposNotificados_cnt_AfterPerformCallback" OnCellEditorInitialize="gv_GruposNotificados_cnt_CellEditorInitialize" OnRowDeleting="gv_GruposNotificados_cnt_RowDeleting" OnRowInserting="gv_GruposNotificados_cnt_RowInserting" OnRowUpdating="gv_GruposNotificados_cnt_RowUpdating" Width="100%">
                                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                                            </SettingsPager>
                                                                                                            <SettingsEditing Mode="PopupEditForm">
                                                                                                            </SettingsEditing>
                                                                                                            <Settings ShowTitlePanel="True" VerticalScrollableHeight="140" VerticalScrollBarMode="Visible" />
                                                                                                            <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False" AllowSort="False" />
                                                                                                            <SettingsCommandButton>
                                                                                                                <UpdateButton>
                                                                                                                    <Image Url="~/imagens/botoes/salvar.png">
                                                                                                                    </Image>
                                                                                                                </UpdateButton>
                                                                                                                <CancelButton>
                                                                                                                    <Image Url="~/imagens/botoes/cancelar.png">
                                                                                                                    </Image>
                                                                                                                </CancelButton>
                                                                                                                <EditButton>
                                                                                                                    <Image Url="~/imagens/botoes/editarReg02.png">
                                                                                                                    </Image>
                                                                                                                </EditButton>
                                                                                                                <DeleteButton>
                                                                                                                    <Image Url="~/imagens/botoes/excluirReg02.png">
                                                                                                                    </Image>
                                                                                                                </DeleteButton>
                                                                                                            </SettingsCommandButton>
                                                                                                            <SettingsPopup>
                                                                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="650px">
                                                                                                                </EditForm>
                                                                                                                <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                                <HeaderFilter MinHeight="140px">
                                                                                                                </HeaderFilter>
                                                                                                            </SettingsPopup>
                                                                                                            <SettingsText EmptyDataRow="<%$ Resources:traducao, adm_edicaoWorkflows_nenhum_perfil_notificado___ %>" PopupEditFormCaption="<%$ Resources:traducao, adm_edicaoWorkflows_perfis_notificados %>" />
                                                                                                            <Columns>
                                                                                                                <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ButtonType="Image" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>" ShowCancelButton="True" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowUpdateButton="True" VisibleIndex="0" Width="80px">
                                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                                </dxtv:GridViewCommandColumn>
                                                                                                                <dxtv:GridViewDataComboBoxColumn Caption="<%$ Resources:traducao, adm_edicaoWorkflows_perfil %>" FieldName="CodigoPerfilWf" Name="CodigoPerfilWf" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1" Width="30px">
                                                                                                                    <EditFormSettings Caption="<%$ Resources:traducao, adm_edicaoWorkflows_perfil_ %>" CaptionLocation="Top" Visible="True" />
                                                                                                                </dxtv:GridViewDataComboBoxColumn>
                                                                                                                <dxtv:GridViewDataTextColumn Caption="<%$ Resources:traducao, adm_edicaoWorkflows_perfil %>" FieldName="NomePerfilWf" Name="NomePerfilWf" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                                                    <PropertiesTextEdit>
                                                                                                                        <ValidationSettings CausesValidation="True">
                                                                                                                            <RequiredField IsRequired="True" />
                                                                                                                        </ValidationSettings>
                                                                                                                    </PropertiesTextEdit>
                                                                                                                    <EditFormSettings CaptionLocation="Top" Visible="False" />
                                                                                                                </dxtv:GridViewDataTextColumn>
                                                                                                                <dxtv:GridViewDataComboBoxColumn Caption="<%$ Resources:traducao, adm_edicaoWorkflows_tipo_de_mensagem %>" FieldName="Mensagem" Name="Mensagem" ShowInCustomizationForm="True" VisibleIndex="3" Width="130px">
                                                                                                                    <PropertiesComboBox>
                                                                                                                        <Items>
                                                                                                                            <dxtv:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>" Value="E" />
                                                                                                                            <dxtv:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_acompanhamento %>" Value="S" />
                                                                                                                        </Items>
                                                                                                                        <ValidationSettings CausesValidation="True">
                                                                                                                            <RequiredField IsRequired="True" />
                                                                                                                        </ValidationSettings>
                                                                                                                    </PropertiesComboBox>
                                                                                                                    <EditFormSettings Caption="<%$ Resources:traducao, adm_edicaoWorkflows_tipo_de_mensagem_ %>" CaptionLocation="Top" Visible="True" />
                                                                                                                </dxtv:GridViewDataComboBoxColumn>
                                                                                                            </Columns>
                                                                                                        </dxtv:ASPxGridView>
                                                                                                    </td>
                                                                                                    <td style="width: 50%">

                                                                                                        <dxtv:ASPxPageControl ID="pcTipoMensagemAcao" runat="server" ActiveTabIndex="0" ClientInstanceName="pcTipoMensagemAcao" Width="100%" TabAlign="Center">
                                                                                                            <TabPages>
                                                                                                                <dxtv:TabPage Name="tcMsgAcao_Cnt" Text="<%$ Resources:traducao, adm_edicaoWorkflows_mensagem_a__o %>">
                                                                                                                    <ContentCollection>
                                                                                                                        <dxtv:ContentControl ID="ContentControl1" runat="server">
                                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                                <tbody>
                                                                                                                                    <tr>
                                                                                                                                        <td valign="top">
                                                                                                                                            <dxtv:ASPxLabel ID="lblAssunto1_cnt" runat="server" ClientInstanceName="lblAssunto1_cnt"
                                                                                                                                                Text="<%$ Resources:traducao, adm_edicaoWorkflows_assunto_ %>">
                                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                                                <tbody>
                                                                                                                                                    <tr>
                                                                                                                                                        <td valign="top">
                                                                                                                                                            <asp:TextBox ID="txtAssunto1_cnt" runat="server" onfocus="elEditor = __ta_initialize(this);"
                                                                                                                                                                TabIndex="5" Width="98%"></asp:TextBox>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </tbody>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td></td>
                                                                                                                                        <td align="right" style="height: 3px"></td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 60px" valign="top">
                                                                                                                                            <dxtv:ASPxLabel ID="lblTexto1_cnt" runat="server" ClientInstanceName="lblTexto1_cnt"
                                                                                                                                                Text="<%$ Resources:traducao, adm_edicaoWorkflows_texto_ %>">
                                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                                        </td>
                                                                                                                                        <td align="left" valign="top">
                                                                                                                                            <asp:TextBox ID="mmTexto1_cnt" runat="server" Height="65px" onfocus="elEditor = __ta_initialize(this)" TabIndex="6" TextMode="MultiLine" Width="98%"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 60px" valign="top">&nbsp;</td>
                                                                                                                                        <td align="left" valign="top">
                                                                                                                                            <table>
                                                                                                                                                <tbody>
                                                                                                                                                    <tr>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="imgNomeDoProcessoConector" runat="server" ClientInstanceName="imgNomeDoProcessoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/NomeProcessoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_fluxo %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
           __ta_insertText(elEditor, &quot; [nomeDoFluxo] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="imgDataInicioProcessoConector" runat="server" ClientInstanceName="imgDataInicioProcessoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/DataDoInicioProcessoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto___data_inicio_fluxo %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
        __ta_insertText(elEditor, &quot; [dataInicioFluxo]  &quot;);
  }" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="imgResponsavelProcessoConector" runat="server" ClientInstanceName="imgResponsavelProcessoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/ResponsavelPeloProcessoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__responsavel_fluxo %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
            __ta_insertText(elEditor, &quot; [responsavelFluxo] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="imgPrazoFinalRespostaConector" runat="server" ClientInstanceName="imgPrazoFinalRespostaConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/PrazoFinalParaRespostaSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__prazo_final_resposta %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
              __ta_insertText(elEditor, &quot; [prazoFinalResposta] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="imgResponsavelAcaoConector" runat="server" ClientInstanceName="imgResponsavelAcaoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/ResponsavelPeloAcaoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__responsavel_a__o %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
            __ta_insertText(elEditor, &quot; [ResponsavelAcao] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="imgDataUltimaAcaoConector" runat="server" ClientInstanceName="imgDataUltimaAcaoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/dataUltimaAcao2.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__data_ultima_a__o_ %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
            __ta_insertText(elEditor, &quot;  [dataUltimaAcao] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="imgNomeProjetoConector" runat="server" ClientInstanceName="imgNomeProjetoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/iconoProjeto.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_projeto_ %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
              __ta_insertText(elEditor, &quot; [nomeProjeto] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage10" runat="server" ClientInstanceName="imgNomeProjetoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/nomeSistema.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_sistema %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
            __ta_insertText(elEditor, &quot; [nomeSistema] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage11" runat="server" ClientInstanceName="imgNomeProjetoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/linkSistema.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__link_do_sistema %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {                                                                                                                                               
              elEditor.blur();
             __ta_insertText(elEditor, &quot; [linkSistema] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px"></td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="imgAjudaAutoTextoCnr" runat="server" ClientInstanceName="imgAjudaAutoTextoCnr" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/Help.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_clique_para_ajuda_com_autotextos %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
	popupAjudaAutoTexto.Show();
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </tbody>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </tbody>
                                                                                                                            </table>
                                                                                                                        </dxtv:ContentControl>
                                                                                                                    </ContentCollection>
                                                                                                                </dxtv:TabPage>
                                                                                                                <dxtv:TabPage Name="tcAcompanhamento_tc" Text="<%$ Resources:traducao, adm_edicaoWorkflows_mensagem_acompanhamento %>">
                                                                                                                    <ContentCollection>
                                                                                                                        <dxtv:ContentControl ID="ContentControl2" runat="server">
                                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                                <tbody>
                                                                                                                                    <tr>
                                                                                                                                        <td valign="top">
                                                                                                                                            <dxtv:ASPxLabel ID="lblAssunto2_cnt" runat="server" ClientInstanceName="lblAssunto2_cnt"
                                                                                                                                                Text="<%$ Resources:traducao, adm_edicaoWorkflows_assunto_ %>">
                                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                                                <tbody>
                                                                                                                                                    <tr>
                                                                                                                                                        <td valign="top">
                                                                                                                                                            <asp:TextBox ID="txtAssunto2_cnt" runat="server"
                                                                                                                                                                onfocus="elEditor = __ta_initialize(this);"
                                                                                                                                                                TabIndex="5" Width="98%"></asp:TextBox>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </tbody>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td></td>
                                                                                                                                        <td align="right" style="height: 3px"></td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 60px" valign="top">
                                                                                                                                            <dxtv:ASPxLabel ID="lblTexto2_cnt" runat="server" ClientInstanceName="lblTexto2_cnt"
                                                                                                                                                Text="<%$ Resources:traducao, adm_edicaoWorkflows_texto_ %>">
                                                                                                                                            </dxtv:ASPxLabel>
                                                                                                                                        </td>
                                                                                                                                        <td align="left" valign="top">
                                                                                                                                            <asp:TextBox ID="mmTexto2_cnt" runat="server" Height="65px" onfocus="elEditor = __ta_initialize(this)" TabIndex="6" TextMode="MultiLine" Width="98%"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 60px" valign="top">&nbsp;</td>
                                                                                                                                        <td align="left" valign="top">
                                                                                                                                            <table>
                                                                                                                                                <tbody>
                                                                                                                                                    <tr>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage5" runat="server" ClientInstanceName="imgNomeDoProcessoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/NomeProcessoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_fluxo %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
       __ta_insertText(elEditor, &quot; [nomeDoFluxo] ] &quot;);     
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage7" runat="server" ClientInstanceName="imgDataInicioProcessoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/DataDoInicioProcessoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto___data_inicio_fluxo %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
       __ta_insertText(elEditor, &quot; [dataInicioFluxo] &quot;);        
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage8" runat="server" ClientInstanceName="imgResponsavelProcessoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/ResponsavelPeloProcessoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__responsavel_fluxo %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
           __ta_insertText(elEditor, &quot; [responsavelFluxo] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage9" runat="server" ClientInstanceName="imgPrazoFinalRespostaConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/PrazoFinalParaRespostaSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__prazo_final_resposta %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
         __ta_insertText(elEditor, &quot; [prazoFinalResposta] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage15" runat="server" ClientInstanceName="imgResponsavelAcaoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/ResponsavelPeloAcaoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__responsavel_a__o %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
             __ta_insertText(elEditor, &quot; [ResponsavelAcao] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage25" runat="server" ClientInstanceName="imgDataUltimaAcaoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/dataUltimaAcao2.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__data_ultima_a__o_ %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
       __ta_insertText(elEditor, &quot; [dataUltimaAcao] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage26" runat="server" ClientInstanceName="imgNomeProjetoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/iconoProjeto.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_projeto_ %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
       __ta_insertText(elEditor, &quot; [nomeProjeto] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage27" runat="server" ClientInstanceName="imgNomeProjetoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/nomeSistema.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_sistema %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
              elEditor.blur();
       __ta_insertText(elEditor, &quot; [nomeSistema] &quot;);
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage28" runat="server" ClientInstanceName="imgNomeProjetoConector" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/linkSistema.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__link_do_sistema %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
                elEditor.blur();
               __ta_insertText(elEditor, &quot; [linkSistema] &quot;);         
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                        <td align="center" style="width: 35px"></td>
                                                                                                                                                        <td align="center" style="width: 35px">
                                                                                                                                                            <dxtv:ASPxImage ID="ASPxImage29" runat="server" ClientInstanceName="imgAjudaAutoTextoCnr" Cursor="pointer" Height="20px" ImageUrl="~/imagens/Workflow/Help.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_clique_para_ajuda_com_autotextos %>">
                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
	popupAjudaAutoTexto.Show();
}" />
                                                                                                                                                            </dxtv:ASPxImage>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </tbody>
                                                                                                                                            </table>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </tbody>
                                                                                                                            </table>
                                                                                                                        </dxtv:ContentControl>
                                                                                                                    </ContentCollection>
                                                                                                                </dxtv:TabPage>
                                                                                                            </TabPages>
                                                                                                            <Paddings Padding="0px" />
                                                                                                            <Border BorderStyle="None" />
                                                                                                        </dxtv:ASPxPageControl>

                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>

                                                                                        </dxtv:ContentControl>
                                                                                    </ContentCollection>
                                                                                </dxtv:TabPage>
                                                                                <dxtv:TabPage Text="<%$ Resources:traducao, adm_edicaoWorkflows_a__es_autom_ticas %>" Name="tab_gv_Acoes_cnt">
                                                                                    <TabStyle Wrap="False">
                                                                                    </TabStyle>
                                                                                    <ContentCollection>
                                                                                        <dxtv:ContentControl runat="server">
                                                                                            <dxtv:ASPxGridView ID="gv_Acoes_cnt" runat="server" AutoGenerateColumns="False" ClientInstanceName="gv_Acoes_cnt" KeyFieldName="CodigoAcaoAutomaticaWf" OnAfterPerformCallback="gv_Acoes_cnt_AfterPerformCallback" OnCellEditorInitialize="gv_Acoes_cnt_CellEditorInitialize" OnRowDeleting="gv_Acoes_cnt_RowDeleting" OnRowInserting="gv_Acoes_cnt_RowInserting" OnRowUpdating="gv_Acoes_cnt_RowUpdating" Width="100%">
                                                                                                <SettingsPager Mode="ShowAllRecords">
                                                                                                </SettingsPager>
                                                                                                <SettingsEditing Mode="PopupEditForm">
                                                                                                </SettingsEditing>
                                                                                                <Settings ShowGroupButtons="False" ShowStatusBar="Hidden" ShowTitlePanel="True" VerticalScrollableHeight="140" VerticalScrollBarMode="Visible" />
                                                                                                <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False" AllowSort="False" />
                                                                                                <SettingsCommandButton>
                                                                                                    <UpdateButton>
                                                                                                        <Image Url="~/imagens/botoes/salvar.png">
                                                                                                        </Image>
                                                                                                    </UpdateButton>
                                                                                                    <CancelButton>
                                                                                                        <Image Url="~/imagens/botoes/cancelar.png">
                                                                                                        </Image>
                                                                                                    </CancelButton>
                                                                                                    <EditButton>
                                                                                                        <Image Url="~/imagens/botoes/editarReg02.png">
                                                                                                        </Image>
                                                                                                    </EditButton>
                                                                                                    <DeleteButton>
                                                                                                        <Image Url="~/imagens/botoes/excluirReg02.png">
                                                                                                        </Image>
                                                                                                    </DeleteButton>
                                                                                                </SettingsCommandButton>
                                                                                                <SettingsPopup>
                                                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="310px">
                                                                                                    </EditForm>
                                                                                                    <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                    <HeaderFilter MinHeight="140px">
                                                                                                    </HeaderFilter>
                                                                                                </SettingsPopup>
                                                                                                <SettingsText EmptyDataRow="<%$ Resources:traducao, adm_edicaoWorkflows_nenhuma_a__o_autom_tica___ %>" PopupEditFormCaption="<%$ Resources:traducao, adm_edicaoWorkflows_a__es %>" />
                                                                                                <Columns>
                                                                                                    <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ButtonType="Image" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>" ShowCancelButton="True" ShowDeleteButton="True" ShowInCustomizationForm="True" ShowUpdateButton="True" VisibleIndex="0" Width="80px">
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                    </dxtv:GridViewCommandColumn>
                                                                                                    <dxtv:GridViewDataComboBoxColumn Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o_autom_tica %>" FieldName="CodigoAcaoAutomaticaWf" Name="colActionCode_cnt" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                                        <PropertiesComboBox ClientInstanceName="colActionCode_cnt" EnableClientSideAPI="True" Width="300px">
                                                                                                            <ValidationSettings CausesValidation="True">
                                                                                                                <RequiredField IsRequired="True" />
                                                                                                            </ValidationSettings>
                                                                                                        </PropertiesComboBox>
                                                                                                        <EditFormSettings Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o_autom_tica__ %>" CaptionLocation="Top" Visible="True" VisibleIndex="0" />
                                                                                                    </dxtv:GridViewDataComboBoxColumn>
                                                                                                    <dxtv:GridViewDataTextColumn Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o_autom_tica %>" FieldName="Nome" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                                        <EditFormSettings CaptionLocation="Top" Visible="False" />
                                                                                                    </dxtv:GridViewDataTextColumn>
                                                                                                </Columns>
                                                                                            </dxtv:ASPxGridView>
                                                                                        </dxtv:ContentControl>
                                                                                    </ContentCollection>
                                                                                </dxtv:TabPage>

                                                                                <dxtv:TabPage Name="tab_AcionamentosAPI" Text="Acionamentos API">
                                                                                    <ContentCollection>
                                                                                        <dxtv:ContentControl runat="server">
                                                                                            <div id="div_tabAcionamentosAPI" style="height: 150px; overflow: auto">
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxtv:ASPxGridView ID="gv_Acionamentos" runat="server" AutoGenerateColumns="False" ClientInstanceName="gv_Acionamentos" KeyFieldName="Id" Width="100%" OnAfterPerformCallback="gv_Acionamentos_AfterPerformCallback" OnCellEditorInitialize="gv_Acionamentos_CellEditorInitialize" OnRowDeleting="gv_Acionamentos_RowDeleting" OnRowInserting="gv_Acionamentos_RowInserting" OnRowUpdating="gv_Acionamentos_RowUpdating">
                                                                                                                <ClientSideEvents RowClick="function(s, e) {
        //if(s.GetFocusedRowIndex()  &gt; -1)
        //{
        //          lpLoading.Show();  
        //          pnCallbackParametrosAcionamentos.PerformCallback(s.GetRowKey(s.GetFocusedRowIndex()) );
        //} 
}"
                                                                                                                    FocusedRowChanged="function(s, e) {
        //if(s.GetFocusedRowIndex()  &gt; -1)
        //{
                 //lpLoading.Show();  
                 //pnCallbackParametrosAcionamentos.PerformCallback(s.GetRowKey(s.GetFocusedRowIndex()) );
       //}      
}"
                                                                                                                    CustomButtonClick="function(s, e) {
        switch(e.buttonID) 
        {
&nbsp;&nbsp;               case&nbsp;'btnEditar1':
&nbsp;&nbsp;&nbsp;&nbsp;                   s.StartEditRow(e.visibleIndex);
&nbsp;&nbsp;&nbsp;&nbsp;                   break;
&nbsp;&nbsp;               case&nbsp;'btnExcluir1':
&nbsp;&nbsp;&nbsp;&nbsp;                   var index = s.GetFocusedRowIndex(); 
                       var numLinhas = gv_ParametrosAcionamentos.GetVisibleRowsOnPage();
                       if(numLinhas &gt; 0)
                       {
                                window.top.mostraMensagem('Registro com dependências!', 'atencao', true, false, null);
                       }
                       else
                       {
                                 s.DeleteRow(index);
                       }         
&nbsp;&nbsp;&nbsp;&nbsp;                   break;
&nbsp;&nbsp;               default:
&nbsp;&nbsp; &nbsp;                   //&nbsp;code block
          }
}"
                                                                                                                    BeginCallback="function(s, e) {
	comandoGridAcionamentos = e.command;
}"
                                                                                                                    EndCallback="function(s, e) {
	if(comandoGridAcionamentos  == 'UPDATEEDIT' || comandoGridAcionamentos  == 'DELETEROW' )
                {
                           if(s.GetVisibleRowsOnPage() &gt; 0)
                           {
                                     tabelaAcionamentosAPI.style.display = 'none';
                                     if (comandoGridAcionamentos  == 'UPDATEEDIT' )
                                     {
                                        s.SetFocusedRowIndex(0);
                                     }
                            }
                             else
                            {
                                      tabelaAcionamentosAPI.style.display = 'block';
                             }
                }
}"
                                                                                                                    Init="function(s, e){
var sHeight = (Math.max(0, document.documentElement.clientHeight) - 305)/2;
s.SetHeight(sHeight);
                                                                                                                    }" />
                                                                                                                <SettingsPager Mode="ShowAllRecords">
                                                                                                                </SettingsPager>
                                                                                                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="8">
                                                                                                                </SettingsEditing>
                                                                                                                <Settings ShowGroupButtons="False" ShowStatusBar="Hidden" ShowTitlePanel="True" VerticalScrollableHeight="140" VerticalScrollBarMode="Visible" />
                                                                                                                <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False" AllowSort="False" />
                                                                                                                <SettingsCommandButton>
                                                                                                                    <UpdateButton>
                                                                                                                        <Image Url="~/imagens/botoes/salvar.png">
                                                                                                                        </Image>
                                                                                                                    </UpdateButton>
                                                                                                                    <CancelButton>
                                                                                                                        <Image Url="~/imagens/botoes/cancelar.png">
                                                                                                                        </Image>
                                                                                                                    </CancelButton>
                                                                                                                    <EditButton>
                                                                                                                        <Image Url="~/imagens/botoes/editarReg02.png">
                                                                                                                        </Image>
                                                                                                                    </EditButton>
                                                                                                                    <DeleteButton>
                                                                                                                        <Image Url="~/imagens/botoes/excluirReg02.png">
                                                                                                                        </Image>
                                                                                                                    </DeleteButton>
                                                                                                                </SettingsCommandButton>
                                                                                                                <SettingsPopup>
                                                                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="750px">
                                                                                                                    </EditForm>
                                                                                                                    <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                                    <HeaderFilter MinHeight="140px">
                                                                                                                    </HeaderFilter>
                                                                                                                </SettingsPopup>
                                                                                                                <SettingsText EmptyDataRow="Nenhum acionamento de API" PopupEditFormCaption="<%$ Resources:traducao, adm_edicaoWorkflows_a__es %>" />
                                                                                                                <Columns>
                                                                                                                    <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ButtonType="Image" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>" ShowCancelButton="True" ShowInCustomizationForm="True" ShowUpdateButton="True" VisibleIndex="0" Width="80px">
                                                                                                                        <CustomButtons>
                                                                                                                            <dxtv:GridViewCommandColumnCustomButton ID="btnEditar1">
                                                                                                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                                                                                                </Image>
                                                                                                                            </dxtv:GridViewCommandColumnCustomButton>
                                                                                                                            <dxtv:GridViewCommandColumnCustomButton ID="btnExcluir1">
                                                                                                                                <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                                                                                                </Image>
                                                                                                                            </dxtv:GridViewCommandColumnCustomButton>
                                                                                                                        </CustomButtons>
                                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                                    </dxtv:GridViewCommandColumn>
                                                                                                                    <dxtv:GridViewDataTextColumn Caption="URL" FieldName="webServiceURL" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                                                        <PropertiesTextEdit MaxLength="2048">
                                                                                                                            <ValidationSettings>
                                                                                                                                <RequiredField IsRequired="true" ErrorText="*" />
                                                                                                                            </ValidationSettings>
                                                                                                                        </PropertiesTextEdit>
                                                                                                                        <EditFormSettings CaptionLocation="Top" Visible="True" Caption="URL a acionar:" ColumnSpan="8" VisibleIndex="0" />
                                                                                                                    </dxtv:GridViewDataTextColumn>
                                                                                                                    <dxtv:GridViewDataCheckColumn Caption="Ativo?" FieldName="enabled" ShowInCustomizationForm="True" VisibleIndex="3" Width="75px">
                                                                                                                        <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                                                                                                        </PropertiesCheckEdit>
                                                                                                                        <EditFormSettings CaptionLocation="Top" Visible="True" Caption="Acionamento Ativo?" ColumnSpan="2" VisibleIndex="2" />
                                                                                                                    </dxtv:GridViewDataCheckColumn>
                                                                                                                    <dxtv:GridViewDataSpinEditColumn Caption="#" FieldName="activationSequence" ShowInCustomizationForm="True" VisibleIndex="1" Width="70px">
                                                                                                                        <PropertiesSpinEdit DisplayFormatString="g" MaxValue="255" NumberType="Integer" AllowNull="false">
                                                                                                                            <ValidationSettings>
                                                                                                                                <RequiredField IsRequired="true" ErrorText="*" />
                                                                                                                            </ValidationSettings>
                                                                                                                        </PropertiesSpinEdit>
                                                                                                                        <EditFormSettings Caption="Ordem:" CaptionLocation="Top" ColumnSpan="2" Visible="True" VisibleIndex="1" />
                                                                                                                    </dxtv:GridViewDataSpinEditColumn>
                                                                                                                </Columns>
                                                                                                            </dxtv:ASPxGridView>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dxtv:ASPxCallbackPanel ID="pnCallbackParametrosAcionamentos" runat="server" Width="100%" ClientInstanceName="pnCallbackParametrosAcionamentos" OnCallback="pnCallbackParametrosAcionamentos_Callback">
                                                                                                                <ClientSideEvents EndCallback="function(s, e) { lpLoading.Hide(); }" />
                                                                                                                <PanelCollection>
                                                                                                                    <dxtv:PanelContent runat="server">
                                                                                                                        <dxtv:ASPxGridView ID="gv_ParametrosAcionamentos" runat="server" KeyFieldName="IdKey" AutoGenerateColumns="False" ClientInstanceName="gv_ParametrosAcionamentos" Width="100%" OnAfterPerformCallback="gv_ParametrosAcionamentos_AfterPerformCallback" OnCellEditorInitialize="gv_ParametrosAcionamentos_CellEditorInitialize" OnRowDeleting="gv_ParametrosAcionamentos_RowDeleting" OnRowInserting="gv_ParametrosAcionamentos_RowInserting" OnRowUpdating="gv_ParametrosAcionamentos_RowUpdating">
                                                                                                                            <ClientSideEvents Init="function(s, e){
var sHeight = (Math.max(0, document.documentElement.clientHeight) - 305)/2;
s.SetHeight(sHeight);
}" />
                                                                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                                                                            </SettingsPager>
                                                                                                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="8">
                                                                                                                            </SettingsEditing>
                                                                                                                            <Settings ShowGroupButtons="False" ShowStatusBar="Hidden" ShowTitlePanel="True" VerticalScrollableHeight="140" VerticalScrollBarMode="Visible" />
                                                                                                                            <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False" AllowSort="False" />
                                                                                                                            <SettingsCommandButton>
                                                                                                                                <UpdateButton>
                                                                                                                                    <Image Url="~/imagens/botoes/salvar.png">
                                                                                                                                    </Image>
                                                                                                                                </UpdateButton>
                                                                                                                                <CancelButton>
                                                                                                                                    <Image Url="~/imagens/botoes/cancelar.png">
                                                                                                                                    </Image>
                                                                                                                                </CancelButton>
                                                                                                                                <EditButton>
                                                                                                                                    <Image Url="~/imagens/botoes/editarReg02.png">
                                                                                                                                    </Image>
                                                                                                                                </EditButton>
                                                                                                                                <DeleteButton>
                                                                                                                                    <Image Url="~/imagens/botoes/excluirReg02.png">
                                                                                                                                    </Image>
                                                                                                                                </DeleteButton>
                                                                                                                            </SettingsCommandButton>
                                                                                                                            <SettingsPopup>
                                                                                                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="760px">
                                                                                                                                </EditForm>
                                                                                                                                <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                                                <HeaderFilter MinHeight="140px">
                                                                                                                                </HeaderFilter>
                                                                                                                            </SettingsPopup>
                                                                                                                            <SettingsText EmptyDataRow="Nenhum parâmetro" PopupEditFormCaption="<%$ Resources:traducao, adm_edicaoWorkflows_a__es %>" />
                                                                                                                            <Columns>
                                                                                                                                <dxtv:GridViewCommandColumn ButtonRenderMode="Image" ButtonType="Image" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>" ShowCancelButton="True" ShowDeleteButton="True" ShowInCustomizationForm="True" ShowUpdateButton="true" ShowEditButton="true" VisibleIndex="0" Width="80px">
                                                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                                                </dxtv:GridViewCommandColumn>
                                                                                                                                <dxtv:GridViewDataTextColumn Caption="Id" Visible="false" FieldName="Id" ShowInCustomizationForm="False" VisibleIndex="1">
                                                                                                                                    <EditFormSettings Visible="False" />
                                                                                                                                </dxtv:GridViewDataTextColumn>
                                                                                                                                <dxtv:GridViewDataTextColumn Caption="IdKey" Visible="false" FieldName="IdKey" ShowInCustomizationForm="False" VisibleIndex="2">
                                                                                                                                    <EditFormSettings Visible="False" />
                                                                                                                                </dxtv:GridViewDataTextColumn>
                                                                                                                                <dxtv:GridViewDataTextColumn Caption="Parâmetro" FieldName="parameter" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                                                                    <EditFormSettings CaptionLocation="Top" Visible="True" Caption="Parâmetro:" ColumnSpan="2" VisibleIndex="0" />
                                                                                                                                </dxtv:GridViewDataTextColumn>
                                                                                                                                <dxtv:GridViewDataComboBoxColumn Caption="Tipo" FieldName="dataType" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                                                                    <PropertiesComboBox>
                                                                                                                                        <Items>
                                                                                                                                            <dxtv:ListEditItem Text="string" Value="string" />
                                                                                                                                            <dxtv:ListEditItem Text="number" Value="number" />
                                                                                                                                            <dxtv:ListEditItem Text="boolean" Value="boolean" />
                                                                                                                                            <dxtv:ListEditItem Text="object" Value="object" />
                                                                                                                                            <dxtv:ListEditItem Text="array" Value="array" />
                                                                                                                                        </Items>
                                                                                                                                    </PropertiesComboBox>
                                                                                                                                    <EditFormSettings CaptionLocation="Top" Visible="True" Caption="Tipo de dados:" ColumnSpan="2" VisibleIndex="1" />
                                                                                                                                </dxtv:GridViewDataComboBoxColumn>
                                                                                                                                <dxtv:GridViewDataComboBoxColumn Caption="Seção" FieldName="httpPart" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                                                                    <PropertiesComboBox>
                                                                                                                                        <Items>
                                                                                                                                            <dxtv:ListEditItem Text="body" Value="body" />
                                                                                                                                            <dxtv:ListEditItem Text="header" Value="header" />
                                                                                                                                        </Items>
                                                                                                                                    </PropertiesComboBox>
                                                                                                                                    <EditFormSettings CaptionLocation="Top" Visible="True" Caption="Seção:" ColumnSpan="2" VisibleIndex="2" />
                                                                                                                                </dxtv:GridViewDataComboBoxColumn>
                                                                                                                                <dxtv:GridViewDataCheckColumn Caption="Valor nulo?" FieldName="sendNull" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                                                                    <PropertiesCheckEdit ClientInstanceName="checkValorNulo">
                                                                                                                                        <ClientSideEvents CheckedChanged="function(s, e) {
	memoValor.SetReadOnly(s.GetChecked());
                if(s.GetChecked() == true)
                {
                          memoValor.SetText('');
                }
}" />
                                                                                                                                    </PropertiesCheckEdit>
                                                                                                                                    <EditFormSettings CaptionLocation="Near" Visible="True" Caption="Nulo:" ColumnSpan="2" VisibleIndex="3" />
                                                                                                                                </dxtv:GridViewDataCheckColumn>
                                                                                                                                <dxtv:GridViewDataMemoColumn Caption="Valor" FieldName="value" ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                                                                    <PropertiesMemoEdit MaxLength="4000" ClientInstanceName="memoValor">
                                                                                                                                        <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                                                        </ReadOnlyStyle>
                                                                                                                                    </PropertiesMemoEdit>
                                                                                                                                    <EditFormSettings Caption="Valor:" CaptionLocation="Top" ColumnSpan="8" Visible="True" VisibleIndex="4" />
                                                                                                                                </dxtv:GridViewDataMemoColumn>
                                                                                                                            </Columns>
                                                                                                                        </dxtv:ASPxGridView>
                                                                                                                    </dxtv:PanelContent>
                                                                                                                </PanelCollection>
                                                                                                            </dxtv:ASPxCallbackPanel>

                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </div>
                                                                                        </dxtv:ContentControl>
                                                                                    </ContentCollection>
                                                                                </dxtv:TabPage>
                                                                            </TabPages>
                                                                            <ClientSideEvents ActiveTabChanging="function(s, e) {
var sHeight = Math.max(0, document.documentElement.clientHeight) - 290;
var sWidth = Math.max(0, document.documentElement.clientWidth) - 100;
if(e.tab.name == 'tab_AcionamentosAPI')
{
        document.getElementById('div_tabAcionamentosAPI').style.height = sHeight + 'px';
}
else if(e.tab.name == 'tab_gv_GruposNotificados_cnt')
{
        gv_GruposNotificados_cnt.SetHeight(sHeight);
       pcTipoMensagemAcao.SetHeight(sHeight - 40);
       mmTexto1_cnt.style.height = (sHeight - 115) + 'px';
       mmTexto2_cnt.style.height = (sHeight - 115) + 'px';
}
else if(e.tab.name == 'tab_gv_Acoes_cnt')
{
         gv_Acoes_cnt.SetHeight(sHeight);
}
}" />
                                                                            <Paddings Padding="0px" />
                                                                            <ContentStyle Border-BorderColor="#4986A2">
                                                                            </ContentStyle>
                                                                        </dxtv:ASPxPageControl>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxp:ASPxPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td align="left" valign="top">&nbsp;</td>
                                                        <td align="right">
                                                            <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="formulario-botao">
                                                                            <dxe:ASPxButton ID="ASPxButton2" runat="server"
                                                                                TabIndex="6" Text="Ok" Width="90px">
                                                                                <ClientSideEvents Click="function(s, e)
                {
	                onOkDivConectorClick(s, e);
                }"></ClientSideEvents>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td class="formulario-botao">
                                                                            <dxe:ASPxButton runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_cancelar %>" Width="90px"
                                                                                TabIndex="7" ID="ASPxButton1">
                                                                                <ClientSideEvents Click="function(s, e) 
            {	
	            divConector.Hide();
	            e.processOnServer = false;	
            }"></ClientSideEvents>
                                                                            </dxe:ASPxButton>
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
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {
	// faz os ajustes de exibição necessários à div Conector
ocultaDivEdicaoWorkflow(&quot;divGridEdicaoElementos&quot;);
	preparaExibicaoDivConector(&quot;E&quot;);
    lpLoading.Hide();
    divConector.Show();
} "></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <%# getDescricaoObjeto() %>
    <dxpc:ASPxPopupControl ID="divTimer" runat="server" ClientInstanceName="divTimer"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="False"
        Width="800px" AllowDragging="True">
        <ContentStyle>
            <Paddings Padding="0px" PaddingBottom="5px" PaddingLeft="5px" PaddingRight="5px" />
        </ContentStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl6" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td id="handleTimers">
                            <table border="0" cellpadding="0" cellspacing="0" style="height: 25px; background-color: #DCDCDC"
                                width="100%">
                                <tr>
                                    <td align="center" valign="middle">
                                        <dxe:ASPxLabel ID="lblInfoTimer" runat="server" ClientInstanceName="lblInfoTimer"
                                            EnableClientSideAPI="True" Font-Bold="True"
                                            Text="<%$ Resources:traducao, adm_edicaoWorkflows_informa__es_do_timer__ %>">
                                        </dxe:ASPxLabel>
                                        <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Font-Bold="True"
                                            Text="Modelo de Fluxo:">
                                        </dxe:ASPxLabel>
                                        <dxe:ASPxLabel ID="lbWorkflow_tmr" runat="server" ClientInstanceName="lbWorkflow_tmr"
                                            EnableClientSideAPI="True" Font-Bold="True"
                                            ForeColor="#C04000">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="center" style="width: 30px; height: 23px" valign="middle">&nbsp;<dxe:ASPxImage ID="ASPxImage2" runat="server" Cursor="pointer" ImageUrl="~/imagens/botoes/close2.png"
                                        ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_fechar %>">
                                        <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;			
    divTimer.Hide();
}"></ClientSideEvents>
                                    </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxcp:ASPxCallbackPanel ID="painelCallbackTimers" runat="server" ClientInstanceName="painelCallbackTimers"
                                OnCallback="painelCallbackTimers_Callback">
                                <PanelCollection>
                                    <dxp:PanelContent ID="PanelContent5" runat="server">
                                        <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 270px">
                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_etapa_timer_ %>"
                                                                            ID="ASPxLabel23">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 5px"></td>
                                                                    <td style="width: 150px">
                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_tempo_limite_ %>" Width="100px"
                                                                            ID="ASPxLabel25">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td style="width: 5px"></td>
                                                                    <td>
                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nova_etapa_ %>"
                                                                            ID="ASPxLabel24">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="cmbEtapaOrigem_tmr"
                                                                            EnableClientSideAPI="True" TabIndex="1" ID="cmbEtapaOrigem_tmr" EnableSynchronization="True">
                                                                            <DropDownButton>
                                                                                <Image Width="9px">
                                                                                </Image>
                                                                            </DropDownButton>
                                                                            <ValidationSettings>
                                                                                <ErrorImage Width="14px">
                                                                                </ErrorImage>
                                                                            </ValidationSettings>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width: 40px" align="left">
                                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="4" ClientInstanceName="edtQtdTempo_tmr"
                                                                                            TabIndex="2" ID="edtQtdTempo_tmr" onkeypress="return SomenteNumero(event)">
                                                                                            <ValidationSettings>
                                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                                </ErrorFrameStyle>
                                                                                            </ValidationSettings>
                                                                                        </dxe:ASPxTextBox>
                                                                                    </td>
                                                                                    <td style="width: 5px"></td>
                                                                                    <td style="width: 96px" align="left">
                                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="cmbUnidadeTempo_tmr"
                                                                                            EnableClientSideAPI="True" TabIndex="3" ID="cmbUnidadeTempo_tmr">
                                                                                            <Items>
                                                                                                <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_minutos %>" Value="minutos"></dxe:ListEditItem>
                                                                                                <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_horas %>" Value="horas"></dxe:ListEditItem> 
                                                                                                <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_dias %>" Value="dias"></dxe:ListEditItem>
                                                                                                <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_semanas %>" Value="semanas"></dxe:ListEditItem>
                                                                                                <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_meses %>" Value="meses"></dxe:ListEditItem>
                                                                                            </Items>
                                                                                            <DropDownButton>
                                                                                                <Image Width="9px">
                                                                                                </Image>
                                                                                            </DropDownButton>
                                                                                            <ButtonEditEllipsisImage Width="12px">
                                                                                            </ButtonEditEllipsisImage>
                                                                                            <ValidationSettings>
                                                                                                <ErrorImage Width="14px">
                                                                                                </ErrorImage>
                                                                                            </ValidationSettings>
                                                                                        </dxe:ASPxComboBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                    <td></td>
                                                                    <td valign="top">
                                                                        <dxe:ASPxComboBox runat="server" ValueType="System.String" Width="100%" ClientInstanceName="cmbEtapaDestino_tmr"
                                                                            EnableClientSideAPI="True" TabIndex="4" ID="cmbEtapaDestino_tmr" EnableSynchronization="True">
                                                                            <DropDownButton>
                                                                                <Image Width="9px">
                                                                                </Image>
                                                                            </DropDownButton>
                                                                            <ValidationSettings>
                                                                                <ErrorImage Width="14px">
                                                                                </ErrorImage>
                                                                            </ValidationSettings>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tcDivTimer"
                                                            EnableClientSideAPI="True" TabSpacing="0px" Width="100%"
                                                            TabIndex="5" ID="tcDivTimer">
                                                            <TabPages>
                                                                <dxtc:TabPage Text="<%$ Resources:traducao, adm_edicaoWorkflows_perfis_notificados %>">
                                                                    <TabStyle Wrap="False">
                                                                    </TabStyle>
                                                                    <ContentCollection>
                                                                        <dxw:ContentControl ID="ContentControl7" runat="server">
                                                                            <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gv_GruposNotificados_tmr"
                                                                                                KeyFieldName="CodigoPerfilWf" AutoGenerateColumns="False" Width="100%"
                                                                                                ID="gv_GruposNotificados_tmr" OnAfterPerformCallback="gv_GruposNotificados_tmr_AfterPerformCallback"
                                                                                                OnRowUpdating="gv_GruposNotificados_tmr_RowUpdating" OnRowDeleting="gv_GruposNotificados_tmr_RowDeleting"
                                                                                                OnRowInserting="gv_GruposNotificados_tmr_RowInserting" OnCellEditorInitialize="gv_GruposNotificados_tmr_CellEditorInitialize">
                                                                                                <Columns>
                                                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="60px" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>"
                                                                                                        VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" ShowUpdateButton="true"
                                                                                                        ShowCancelButton="true">
                                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoPerfilWf" Name="CodigoPerfilWf"
                                                                                                        Width="30px" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_perfil_ %>" Visible="False" VisibleIndex="1">
                                                                                                        <PropertiesComboBox ValueType="System.String">
                                                                                                        </PropertiesComboBox>
                                                                                                        <EditFormSettings Visible="True" CaptionLocation="Top"></EditFormSettings>
                                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="NomePerfilWf" Name="NomePerfilWf" Width="250px"
                                                                                                        Caption="Perfil" ToolTip="Indique os perfis de pessoas que terão acesso a esta etapa"
                                                                                                        VisibleIndex="1">
                                                                                                        <PropertiesTextEdit>
                                                                                                            <ValidationSettings CausesValidation="True">
                                                                                                                <RequiredField IsRequired="True"></RequiredField>
                                                                                                            </ValidationSettings>
                                                                                                        </PropertiesTextEdit>
                                                                                                        <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="Mensagem" Name="Mensagem" Width="130px"
                                                                                                        Caption="<%$ Resources:traducao, adm_edicaoWorkflows_tipo_de_mensagem %>" VisibleIndex="2">
                                                                                                        <PropertiesComboBox ValueType="System.String">
                                                                                                            <Items>
                                                                                                                <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>" Value="E"></dxe:ListEditItem>
                                                                                                                <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_acompanhamento %>" Value="S"></dxe:ListEditItem>
                                                                                                            </Items>
                                                                                                            <ValidationSettings CausesValidation="True">
                                                                                                                <RequiredField IsRequired="True"></RequiredField>
                                                                                                            </ValidationSettings>
                                                                                                        </PropertiesComboBox>
                                                                                                        <EditFormSettings Visible="True" CaptionLocation="Top"></EditFormSettings>
                                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                                </Columns>
                                                                                                <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>
                                                                                                <SettingsPager Mode="ShowAllRecords">
                                                                                                </SettingsPager>
                                                                                                <SettingsEditing Mode="PopupEditForm">
                                                                                                </SettingsEditing>
                                                                                                <SettingsPopup>
                                                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                                        Width="650px" />
                                                                                                    <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                                                </SettingsPopup>
                                                                                                <Settings ShowTitlePanel="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="140"></Settings>
                                                                                                <SettingsText PopupEditFormCaption="<%$ Resources:traducao, adm_edicaoWorkflows_perfis_notificados %>" EmptyDataRow="<%$ Resources:traducao, adm_edicaoWorkflows_nenhum_perfil_notificado___ %>"></SettingsText>
                                                                                                <SettingsCommandButton>
                                                                                                    <DeleteButton Image-Url="~/imagens/botoes/excluirReg02.png">
                                                                                                        <Image Url="~/imagens/botoes/excluirReg02.png"></Image>
                                                                                                    </DeleteButton>
                                                                                                    <EditButton Image-Url="~/imagens/botoes/editarReg02.png">
                                                                                                        <Image Url="~/imagens/botoes/editarReg02.png"></Image>
                                                                                                    </EditButton>
                                                                                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                                                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>

                                                                                                    <UpdateButton Image-Url="~/imagens/botoes/salvar.png">
                                                                                                        <Image Url="~/imagens/botoes/salvar.png"></Image>
                                                                                                    </UpdateButton>
                                                                                                    <CancelButton Image-Url="~/imagens/botoes/cancelar.png">
                                                                                                        <Image Url="~/imagens/botoes/cancelar.png"></Image>
                                                                                                    </CancelButton>
                                                                                                </SettingsCommandButton>
                                                                                            </dxwgv:ASPxGridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </dxw:ContentControl>
                                                                    </ContentCollection>
                                                                </dxtc:TabPage>
                                                                <dxtc:TabPage Text="<%$ Resources:traducao, adm_edicaoWorkflows_a__es_autom_ticas %>">
                                                                    <TabStyle Wrap="False">
                                                                    </TabStyle>
                                                                    <ContentCollection>
                                                                        <dxw:ContentControl ID="ContentControl8" runat="server">
                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gv_Acoes_tmr" KeyFieldName="CodigoAcaoAutomaticaWf"
                                                                                                AutoGenerateColumns="False" AccessibilityCompliant="True" Width="100%"
                                                                                                ID="gv_Acoes_tmr" OnAfterPerformCallback="gv_Acoes_tmr_AfterPerformCallback"
                                                                                                OnRowUpdating="gv_Acoes_tmr_RowUpdating" OnRowDeleting="gv_Acoes_tmr_RowDeleting"
                                                                                                OnRowInserting="gv_Acoes_tmr_RowInserting" OnCellEditorInitialize="gv_Acoes_tmr_CellEditorInitialize">
                                                                                                <Columns>
                                                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="70px" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>"
                                                                                                        VisibleIndex="0" ShowDeleteButton="true" ShowCancelButton="true" ShowUpdateButton="true"
                                                                                                        ShowEditButton="true">
                                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoAcaoAutomaticaWf" Name="colActionCode_tmr"
                                                                                                        Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o_autom_tica %>" Visible="False" VisibleIndex="1">
                                                                                                        <PropertiesComboBox ValueType="System.String" Width="300px">
                                                                                                            <ValidationSettings CausesValidation="True">
                                                                                                                <RequiredField IsRequired="True"></RequiredField>
                                                                                                            </ValidationSettings>
                                                                                                        </PropertiesComboBox>
                                                                                                        <EditFormSettings Visible="True" VisibleIndex="0" CaptionLocation="Top" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o_autom_tica_ %>"></EditFormSettings>
                                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="Nome" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o_autom_tica %>"
                                                                                                        VisibleIndex="1">
                                                                                                        <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                </Columns>
                                                                                                <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>
                                                                                                <SettingsPager Mode="ShowAllRecords">
                                                                                                </SettingsPager>
                                                                                                <SettingsEditing Mode="PopupEditForm">
                                                                                                </SettingsEditing>
                                                                                                <SettingsPopup>
                                                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
                                                                                                    <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                                                </SettingsPopup>
                                                                                                <Settings ShowTitlePanel="True" ShowGroupButtons="False" VerticalScrollBarMode="Visible"
                                                                                                    VerticalScrollableHeight="140"></Settings>
                                                                                                <SettingsText PopupEditFormCaption="<%$ Resources:traducao, adm_edicaoWorkflows_a__es %>" EmptyDataRow="<%$ Resources:traducao, adm_edicaoWorkflows_nenhuma_a__o_autom_tica___ %>"></SettingsText>
                                                                                                <SettingsCommandButton>
                                                                                                    <DeleteButton Image-Url="~/imagens/botoes/excluirReg02.png">
                                                                                                        <Image Url="~/imagens/botoes/excluirReg02.png"></Image>
                                                                                                    </DeleteButton>
                                                                                                    <ShowAdaptiveDetailButton RenderMode="Image"></ShowAdaptiveDetailButton>

                                                                                                    <HideAdaptiveDetailButton RenderMode="Image"></HideAdaptiveDetailButton>

                                                                                                    <UpdateButton Image-Url="~/imagens/botoes/salvar.png">
                                                                                                        <Image Url="~/imagens/botoes/salvar.png"></Image>
                                                                                                    </UpdateButton>
                                                                                                    <CancelButton Image-Url="~/imagens/botoes/cancelar.png">
                                                                                                        <Image Url="~/imagens/botoes/cancelar.png"></Image>
                                                                                                    </CancelButton>
                                                                                                </SettingsCommandButton>
                                                                                            </dxwgv:ASPxGridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </dxw:ContentControl>
                                                                    </ContentCollection>
                                                                </dxtc:TabPage>
                                                            </TabPages>
                                                            <ContentStyle>
                                                                <border bordercolor="#4986A2"></border>
                                                            </ContentStyle>
                                                        </dxtc:ASPxPageControl>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px">
                                                        <dxtc:ASPxPageControl ID="pcNotificacoes_tmr" runat="server" ActiveTabIndex="0" ClientInstanceName="pcNotificacoes_tmr"
                                                            Width="100%">
                                                            <TabPages>
                                                                <dxtc:TabPage Name="tcMsgAcao_Cnt" Text="<%$ Resources:traducao, adm_edicaoWorkflows_mensagem_a__o %>">
                                                                    <ContentCollection>
                                                                        <dxw:ContentControl ID="ContentControl9" runat="server">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <dxe:ASPxLabel ID="lblAssunto1_cnt0" runat="server" ClientInstanceName="lblAssunto1_cnt"
                                                                                                Text="<%$ Resources:traducao, adm_edicaoWorkflows_assunto_ %>">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td valign="top">
                                                                                                            <asp:TextBox ID="txtAssunto1_tmr" runat="server" onfocus="elEditor = __ta_initialize(this);"
                                                                                                                TabIndex="5" Width="98%"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td></td>
                                                                                        <td align="right" style="height: 3px"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 60px" valign="top">
                                                                                            <dxe:ASPxLabel ID="lblTexto1_cnt0" runat="server" ClientInstanceName="lblTexto1_cnt"
                                                                                                Text="<%$ Resources:traducao, adm_edicaoWorkflows_texto_ %>">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td align="left" valign="top">
                                                                                            <asp:TextBox ID="mmTexto1_tmr" runat="server"
                                                                                                Height="65px" onfocus="elEditor = __ta_initialize(this)"
                                                                                                TabIndex="6" TextMode="MultiLine" Width="98%"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </dxw:ContentControl>
                                                                    </ContentCollection>
                                                                </dxtc:TabPage>
                                                                <dxtc:TabPage Name="tcAcompanhamento_tc" Text="<%$ Resources:traducao, adm_edicaoWorkflows_mensagem_acompanhamento %>">
                                                                    <ContentCollection>
                                                                        <dxw:ContentControl ID="ContentControl10" runat="server">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <dxe:ASPxLabel ID="lblAssunto2_cnt0" runat="server" ClientInstanceName="lblAssunto2_cnt"
                                                                                                Text="<%$ Resources:traducao, adm_edicaoWorkflows_assunto_ %>">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td valign="top">
                                                                                                            <asp:TextBox ID="txtAssunto2_tmr" runat="server"
                                                                                                                onfocus="elEditor = __ta_initialize(this);"
                                                                                                                TabIndex="5" Width="98%"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td></td>
                                                                                        <td align="right" style="height: 3px"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 60px" valign="top">
                                                                                            <dxe:ASPxLabel ID="lblTexto2_cnt0" runat="server" ClientInstanceName="lblTexto2_cnt"
                                                                                                Text="<%$ Resources:traducao, adm_edicaoWorkflows_texto_ %>">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td align="left" valign="top">
                                                                                            <asp:TextBox ID="mmTexto2_tmr" runat="server"
                                                                                                Height="65px" onfocus="elEditor = __ta_initialize(this)" TabIndex="6"
                                                                                                TextMode="MultiLine" Width="98%"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </dxw:ContentControl>
                                                                    </ContentCollection>
                                                                </dxtc:TabPage>
                                                            </TabPages>
                                                        </dxtc:ASPxPageControl>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 60px"></td>
                                                                    <td align="left" valign="top">
                                                                        <table>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td align="center" style="width: 35px">
                                                                                        <dxe:ASPxImage ID="imgNomeDoProcessoConector0" runat="server" ClientInstanceName="imgNomeDoProcessoConector"
                                                                                            Cursor="pointer" ImageUrl="~/imagens/Workflow/NomeProcessoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_fluxo %>">
                                                                                            <ClientSideEvents Click="function(s, e) {
	                __ta_insertText(elEditor, &quot; [nomeDoFluxo] &quot;);
                }"></ClientSideEvents>
                                                                                        </dxe:ASPxImage>
                                                                                    </td>
                                                                                    <td align="center" style="width: 35px">
                                                                                        <dxe:ASPxImage ID="imgDataInicioProcessoConector0" runat="server" ClientInstanceName="imgDataInicioProcessoConector"
                                                                                            Cursor="pointer" ImageUrl="~/imagens/Workflow/DataDoInicioProcessoSmall.png"
                                                                                            ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto___data_inicio_fluxo %>">
                                                                                            <ClientSideEvents Click="function(s, e) {
	                __ta_insertText(elEditor, &quot; [dataInicioFluxo] &quot;);
                }"></ClientSideEvents>
                                                                                        </dxe:ASPxImage>
                                                                                    </td>
                                                                                    <td align="center" style="width: 35px">
                                                                                        <dxe:ASPxImage ID="imgResponsavelProcessoConector0" runat="server" ClientInstanceName="imgResponsavelProcessoConector"
                                                                                            Cursor="pointer" ImageUrl="~/imagens/Workflow/ResponsavelPeloProcessoSmall.png"
                                                                                            ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__responsavel_fluxo %>">
                                                                                            <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [responsavelFluxo] &quot;);
}"></ClientSideEvents>
                                                                                        </dxe:ASPxImage>
                                                                                    </td>
                                                                                    <td align="center" style="width: 35px">&nbsp;<dxe:ASPxImage ID="imgPrazoFinalRespostaConector0" runat="server" ClientInstanceName="imgPrazoFinalRespostaConector"
                                                                                        Cursor="pointer" ImageUrl="~/imagens/Workflow/PrazoFinalParaRespostaSmall.png"
                                                                                        ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__prazo_final_resposta %>">
                                                                                        <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [prazoFinalResposta] &quot;);
}"></ClientSideEvents>
                                                                                    </dxe:ASPxImage>
                                                                                    </td>
                                                                                    <td align="center" style="width: 35px">&nbsp;<dxe:ASPxImage ID="imgResponsavelAcaoConector0" runat="server" ClientInstanceName="imgResponsavelAcaoConector"
                                                                                        Cursor="pointer" ImageUrl="~/imagens/Workflow/ResponsavelPeloAcaoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__responsavel_a__o %>">
                                                                                        <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [ResponsavelAcao] &quot;);
}"></ClientSideEvents>
                                                                                    </dxe:ASPxImage>
                                                                                    </td>
                                                                                    <td align="center" style="width: 35px">&nbsp;<dxe:ASPxImage ID="imgDataUltimaAcaoConector0" runat="server" ClientInstanceName="imgDataUltimaAcaoConector"
                                                                                        Cursor="pointer" ImageUrl="~/imagens/Workflow/dataUltimaAcao2.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__data_ultima_a__o_ %>">
                                                                                        <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [dataUltimaAcao] &quot;);
}"></ClientSideEvents>
                                                                                    </dxe:ASPxImage>
                                                                                    </td>
                                                                                    <td align="center" style="width: 35px">
                                                                                        <dxe:ASPxImage ID="imgNomeProjetoConector0" runat="server" ClientInstanceName="imgNomeProjetoConector"
                                                                                            Cursor="pointer" ImageUrl="~/imagens/Workflow/iconoProjeto.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_projeto_ %>">
                                                                                            <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [nomeProjeto] &quot;);
}"></ClientSideEvents>
                                                                                        </dxe:ASPxImage>
                                                                                    </td>
                                                                                    <td align="center" style="width: 35px">
                                                                                        <dxe:ASPxImage ID="ASPxImage1010" runat="server" ClientInstanceName="imgNomeProjetoConector"
                                                                                            Cursor="pointer" ImageUrl="~/imagens/Workflow/nomeSistema.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_sistema %>">
                                                                                            <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [nomeSistema] &quot;);
}"></ClientSideEvents>
                                                                                        </dxe:ASPxImage>
                                                                                    </td>
                                                                                    <td align="center" style="width: 35px">
                                                                                        <dxe:ASPxImage ID="ASPxImage1011" runat="server" ClientInstanceName="imgNomeProjetoConector"
                                                                                            Cursor="pointer" ImageUrl="~/imagens/Workflow/linkSistema.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__link_do_sistema %>">
                                                                                            <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [linkSistema] &quot;);
}"></ClientSideEvents>
                                                                                        </dxe:ASPxImage>
                                                                                    </td>
                                                                                    <td align="center" style="width: 35px"></td>
                                                                                    <td align="center" style="width: 35px">
                                                                                        <dxe:ASPxImage ID="imgAjudaAutoTextoCnr0" runat="server" ClientInstanceName="imgAjudaAutoTextoCnr0"
                                                                                            Cursor="pointer" ImageUrl="~/imagens/Workflow/Help.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_clique_para_ajuda_com_autotextos %>">
                                                                                            <ClientSideEvents Click="function(s, e) {
popupAjudaAutoTexto.Show();	
}" />
                                                                                        </dxe:ASPxImage>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                    <td align="right">
                                                                        <table cellspacing="0" cellpadding="0" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class="formulario-botao">
                                                                                        <dxe:ASPxButton ID="ASPxButton7" runat="server"
                                                                                            TabIndex="6" Text="Ok" Width="90px">
                                                                                            <ClientSideEvents Click="function(s, e){
    e.processOnServer = false;
	onOkDivTimerClick(s, e);
}" />
                                                                                        </dxe:ASPxButton>
                                                                                    </td>
                                                                                    <td class="formulario-botao">
                                                                                        <dxe:ASPxButton ID="ASPxButton8" runat="server"
                                                                                            TabIndex="7" Text="<%$ Resources:traducao, adm_edicaoWorkflows_cancelar %>" Width="90px">
                                                                                            <ClientSideEvents Click="function(s, e) 
{	
    e.processOnServer = false;
	divTimer.Hide();
}" />
                                                                                        </dxe:ASPxButton>
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
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <ClientSideEvents EndCallback="function(s, e) {
	// não há nada para preparar na exibição da div timer, por isso a linha abaixo comentada
	// preparaExibicaoDivTimer('E');
    divTimer.Show();
}"></ClientSideEvents>
                            </dxcp:ASPxCallbackPanel>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <%# getDescricaoObjeto() %>
     <div id="divGridEdicaoElementos" style="position: absolute; width: 800px; top: 50%; left: 50%; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; margin-top: 100px" class="fundoDados">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td id="handle">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: #DCDCDC">
                        <tr>
                            <td style="height: 25px; background-color: #DCDCDC;" align="center" valign="middle">
                                <dxe:ASPxLabel ID="lblTituloElemento" runat="server" ClientInstanceName="lblTituloElemento"
                                    Text="DivEdição" Font-Bold="True">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 30px;" align="center" valign="middle">
                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/botoes/close2.png"
                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_fechar %>" Cursor="pointer">
                                    <ClientSideEvents Click="function(s, e) 
            {
	            ocultaDivEdicaoWorkflow(&quot;divGridEdicaoElementos&quot;);
	            e.processOnServer = false;		
            }" />
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                    </table>
                    <dxcp:ASPxCallbackPanel ID="pnlCbkEdicaoElementos" runat="server" ClientInstanceName="pnlCbkEdicaoElementos"
                        OnCallback="pnlCbkEdicaoElementos_Callback" Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent2" runat="server">
                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvEdicaoElementos" KeyFieldName="idorigemUnbound"
                                    AutoGenerateColumns="False" Width="100%" ID="gvEdicaoElementos"
                                    OnCustomButtonInitialize="gvEdicaoElementos_CustomButtonInitialize">
                                    <ClientSideEvents RowDblClick="function(s, e) {
         // define propriedades usadas no endCallBack da grid;
    s.cpLastIndexRow = e.visibleIndex;
    s.cpButtonID = 'btnEditar';  // simula edição do item no double click da linha

    // se não conseguir manipular o XML, não faz nada.
    if (false == xmlWorkflowOk()) {
        e.processOnServer = false;
        return;
    }
    s.PerformCallback(&quot;&quot;);
}"
                                        CustomButtonClick="function(s, e) {
    // define propriedades usadas no endCallBack da grid;
    s.cpLastIndexRow = e.visibleIndex;
    s.cpButtonID = e.buttonID;
    // se não conseguir manipular o XML, não faz nada.
    if (false == xmlWorkflowOk()) {
        e.processOnServer = false;
        return;
    }

    if (&quot;btnExcluir&quot; == e.buttonID) {
        e.processOnServer = false; // na exclusão, tudo é processado no cliente;
        if (&quot;Etapas&quot; == s.cpItemEmEdicao) {
            var funcObj = {
                funcaoClickOK: function (s, e) {
                    ocultaDivEdicaoWorkflow(&quot;divGridEdicaoElementos&quot;);
                    gvEdicaoElementos.GetRowValues(e.visibleIndex, &quot;idEtapa&quot;, recebeCamposExclusaoElementos);
                }
            }
            window.top.mostraConfirmacao(traducao.WorkflowCharts_deseja_excluir_a_etapa_selecionada_, function () { funcObj['funcaoClickOK'](s, e) }, null);
        }
        else if (&quot;Conectores&quot; == s.cpItemEmEdicao) {
            var funcObj = {
                funcaoClickOK: function (s, e) {
                    ocultaDivEdicaoWorkflow(&quot;divGridEdicaoElementos&quot;);
                    gvEdicaoElementos.GetRowValues(e.visibleIndex, &quot;descricao;idorigemUnbound;iddestino&quot;, function(valores)
                                                                                                             {    
                                                                                                                 var acao = valores[0];
                                                                                                                 var from = valores[1];
                                                                                                                 var to = valores[2];
                                                                                                                 var connector = new xConnector();
                                                                                                                 if ((null != acao) &amp; (null != from) &amp; (null != to)) 
                                                                                                                 {
                                                                                                                    if (false == xmlWorkflowOk())
                                                                                                                        return;

                                                                                                                    connector.from = from;
                                                                                                                    connector.to = to;
                                                                                                                    connector.acao = acao;
                                                                                                                    removeConnectorFromXml(connector);
                                                                                                                 }
                                                                                                                 return;
                                                                                                             });
                }
            }
            window.top.mostraConfirmacao(traducao.WorkflowCharts_deseja_excluir_o_conector_selecionado_, function () { funcObj['funcaoClickOK'](s, e) }, null);

        }
        if (&quot;Subprocessos&quot; == s.cpItemEmEdicao) {
            var funcObj = {
                funcaoClickOK: function (s, e) {
                    ocultaDivEdicaoWorkflow(&quot;divGridEdicaoElementos&quot;);
                    gvEdicaoElementos.GetRowValues(e.visibleIndex, &quot;idEtapa&quot;, recebeCamposExclusaoElementos);
                }
            }
            window.top.mostraConfirmacao(traducao.WorkflowCharts_deseja_excluir_o_subprocesso_selecionado_, function () { funcObj['funcaoClickOK'](s, e) }, null);

        }
        else if (&quot;Timers&quot; == s.cpItemEmEdicao) {
            var funcObj = {
                funcaoClickOK: function (s, e) {
                    ocultaDivEdicaoWorkflow(&quot;divGridEdicaoElementos&quot;);
                    s.GetRowValues(e.visibleIndex, &quot;idEtapa;idorigemUnbound;iddestino;&quot;, function (valores) {
                                  var timerID = valores[0];
                                  var from = valores[1];
                                  var to = valores[2];

                                  if ((null != timerID) &amp; (null != from) &amp; (null != to)) 
                                  {
                                              if (false == xmlWorkflowOk())
                                                     return;

                                              __wf_timerObj.elementId = timerID;
                                              __wf_timerObj.from = from;
                                              __wf_timerObj.to = to;
                                              removeTimerFromXml(__wf_timerObj);
                                  }
                     });
                }
            }
            window.top.mostraConfirmacao(traducao.WorkflowCharts_deseja_excluir_o_timer_selecionado_, function () { funcObj['funcaoClickOK'](s, e) }, null);

        }
        else if (&quot;Fins&quot; == s.cpItemEmEdicao) {
            var funcObj = {
                funcaoClickOK: function (s, e) {
                    ocultaDivEdicaoWorkflow(&quot;divGridEdicaoElementos&quot;);
                    gvEdicaoElementos.GetRowValues(e.visibleIndex, &quot;idEtapa&quot;, recebeCamposExclusaoElementos);
                }
            }
            window.top.mostraConfirmacao(traducao.WorkflowCharts_deseja_excluir_o_fim_selecionado_, function () { funcObj['funcaoClickOK'](s, e) }, null);

        }
        else if (&quot;Juncoes&quot; == s.cpItemEmEdicao) {
            var funcObj = {
                funcaoClickOK: function (s, e) {
                    ocultaDivEdicaoWorkflow(&quot;divGridEdicaoElementos&quot;);
                    gvEdicaoElementos.GetRowValues(e.visibleIndex, &quot;idEtapa&quot;, recebeCamposExclusaoElementos);
                }
            }
            window.top.mostraConfirmacao(traducao.WorkflowCharts_deseja_excluir_a_jun__o_selecionada_, function () { funcObj['funcaoClickOK'](s, e) }, null);

        }
        else if (&quot;Disjuncoes&quot; == s.cpItemEmEdicao) {
            var funcObj = {
                funcaoClickOK: function (s, e) {
                    ocultaDivEdicaoWorkflow(&quot;divGridEdicaoElementos&quot;);
                    gvEdicaoElementos.GetRowValues(e.visibleIndex, &quot;idEtapa&quot;, recebeCamposExclusaoElementos);
                }
            }
            window.top.mostraConfirmacao(traducao.WorkflowCharts_deseja_excluir_a_disjun__o_selecionada_, function () { funcObj['funcaoClickOK'](s, e) }, null);

        }
        else if (&quot;CaminhoCondicional&quot; == s.cpItemEmEdicao) {
            var funcObj = {
                funcaoClickOK: function (s, e) {
                    ocultaDivEdicaoWorkflow(&quot;divGridEdicaoElementos&quot;);
                    gvEdicaoElementos.GetRowValues(e.visibleIndex, &quot;idEtapa&quot;, recebeCamposExclusaoElementos);
                }
            }
            window.top.mostraConfirmacao(traducao.WorkflowCharts_deseja_excluir_o_caminho_condicional_selecionado_, function () { funcObj['funcaoClickOK'](s, e) }, null);

        }

    }
    else {
        if (&quot;CaminhoCondicional&quot; == s.cpItemEmEdicao) {
            gvEdicaoElementos.GetRowValues(e.visibleIndex, &quot;idEtapa&quot;, recebeCamposEditaCaminhoCondicional);
            return;
        }
        e.processOnServer = true;
    }
}
"
                                        EndCallback="function(s, e) {
	// as propriedades customizadas da grid (s) são definidas na onCustomButtonClick_gvEdicaoElementos()
               if ( (null != s.cpButtonID) &amp;&amp; (&quot;btnEditar&quot; == s.cpButtonID) )
	{
		// s.cpLastIndexRow foi criada no CustomButtonClick
		
		if(s.cpItemEmEdicao == 'Etapas')
                                {
			                                   lpLoading.Show();
                                               painelCallbackEtapas.PerformCallback(s.cpLastIndexRow);
                                }
		else if(s.cpItemEmEdicao == 'Conectores')
		{
                                                lpLoading.Show();
                                                limpaCamposDivConector('E');
			painelCallbackConectores.PerformCallback(s.cpLastIndexRow);

			if(s.cpItemEmEdicao == 'AcaoWf')
            	                                              lblCaptionConector.SetText('Ação Inicia / Cancela Fluxo -');
			else
            	                                              lblCaptionConector.SetText(traducao.adm_edicaoWorkflows_informa__es_do_conector + ' - ');
		}
		else if(s.cpItemEmEdicao == 'Subprocessos')
		{
			ocultaDivEdicaoWorkflow('divGridEdicaoElementos');
                                                painelCallbackSubprocesso.PerformCallback(s.cpLastIndexRow);
		}
		else if( s.cpItemEmEdicao == 'AcaoWf')
		{
			ocultaDivEdicaoWorkflow('divGridEdicaoElementos');
                                                limpaCamposDivAcaoWf();
			painelCallbackAcoesWf.PerformCallback(s.cpLastIndexRow);
		}
		else if(s.cpItemEmEdicao == 'Timers')
		{
			ocultaDivEdicaoWorkflow('divGridEdicaoElementos');
                                                limpaCamposDivTimer();
			painelCallbackTimers.PerformCallback(s.cpLastIndexRow);	
		}
		else if(s.cpItemEmEdicao == 'Disjuncoes')
		{
			ocultaDivEdicaoWorkflow('divGridEdicaoElementos');
                                                // cpElementType será usada no endCallBack do pnlCnkDisJunFim
			pnlCnkDisJunFim.cpElementType = __wf_cTipoElementoDisjuncao
			pnlCnkDisJunFim.PerformCallback(s.cpLastIndexRow);
		}
		else if(s.cpItemEmEdicao == 'Juncoes')
		{
 		               ocultaDivEdicaoWorkflow('divGridEdicaoElementos');	
                                               // cpElementType será usada no endCallBack do pnlCnkDisJunFim
			pnlCnkDisJunFim.cpElementType = __wf_cTipoElementoJuncao
			pnlCnkDisJunFim.PerformCallback(s.cpLastIndexRow);	
		}
		else if(s.cpItemEmEdicao == &quot;Fins&quot;)
		{
			ocultaDivEdicaoWorkflow('divGridEdicaoElementos');
                                                // cpElementType será usada no endCallBack do pnlCnkDisJunFim
			pnlCnkDisJunFim.cpElementType = __wf_cTipoElementoFim;
			pnlCnkDisJunFim.PerformCallback(s.cpLastIndexRow);
		}                              
                                else if(s.cpItemEmEdicao == 'CaminhoCondicional')
		{
		            ocultaDivEdicaoWorkflow('divGridEdicaoElementos');
                                            // ACG: 23/11/2015 - O IF abaixo é apenas para informar o local do tratamento	
                                            // o tratamento está no método recebeCamposEditaCaminhoCondicional() do arquivo 'uc_crud_caminhoCondicional.js'
		}
	}
}"></ClientSideEvents>
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="80px" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>"
                                            VisibleIndex="0">
                                            <CustomButtons>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="<%$ Resources:traducao, adm_edicaoWorkflows_excluir %>">
                                                    <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                                <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="<%$ Resources:traducao, adm_edicaoWorkflows_editar %>">
                                                    <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                    </Image>
                                                </dxwgv:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="idEtapa" Visible="False" VisibleIndex="1">
                                            <EditFormSettings CaptionLocation="Top"></EditFormSettings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="idorigemUnbound" UnboundExpression="idorigem" UnboundType="String" Name="idOrigem" Visible="False"
                                            VisibleIndex="3">
                                            <PropertiesTextEdit ClientInstanceName="idOrigem">
                                            </PropertiesTextEdit>
                                            <EditFormSettings CaptionLocation="Top"></EditFormSettings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="origem" Name="origem" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_origem %>" VisibleIndex="2">
                                            <PropertiesTextEdit ClientInstanceName="origem">
                                            </PropertiesTextEdit>
                                            <EditFormSettings CaptionLocation="Top"></EditFormSettings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="descricao" Name="descricao" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_descri__o %>"
                                            VisibleIndex="4">
                                            <PropertiesTextEdit ClientInstanceName="descricao">
                                            </PropertiesTextEdit>
                                            <EditFormSettings CaptionLocation="Top"></EditFormSettings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="iddestino" Name="idDestino" Visible="False"
                                            VisibleIndex="5">
                                            <PropertiesTextEdit ClientInstanceName="idDestino">
                                            </PropertiesTextEdit>
                                            <EditFormSettings CaptionLocation="Top"></EditFormSettings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="destino" Name="destino" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_destino %>"
                                            VisibleIndex="6">
                                            <PropertiesTextEdit ClientInstanceName="destino">
                                            </PropertiesTextEdit>
                                            <EditFormSettings CaptionLocation="Top"></EditFormSettings>
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowFocusedRow="True"
                                        EnableRowHotTrack="True" AllowSort="False"></SettingsBehavior>
                                    <SettingsPager Mode="ShowAllRecords" Visible="False">
                                    </SettingsPager>
                                    <Settings ShowGroupButtons="False" VerticalScrollBarMode="Visible" VerticalScrollableHeight="240"></Settings>

                                    <SettingsPopup>
                                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                                    </SettingsPopup>

                                    <SettingsText EmptyDataRow="<%$ Resources:traducao, adm_edicaoWorkflows_nenhuma_informa__o_a_apresentar %>"></SettingsText>
                                </dxwgv:ASPxGridView>
                                <asp:HiddenField runat="server" Value="8" ID="hfCodigoWorkflow"></asp:HiddenField>
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) {
    if (&quot;salvar&quot; == s.cpAcaoEscolhida) {
        ocultaDivLoading();
        if (&quot;1&quot; == s.cpAcaoExecutadaComSucesso)
            mostraDivSalvoPublicado(&quot;...&quot;, traducao.WorkflowCharts_vers_o_gravada_com_sucesso_);
    }
    else if (&quot;publicar&quot; == s.cpAcaoEscolhida) {
        ocultaDivLoading();

        if (&quot;1&quot; == s.cpAcaoExecutadaComSucesso)
            mostraDivSalvoPublicado(&quot;...&quot;, traducao.WorkflowCharts_vers_o_publicada_com_sucesso_);
    }
    else
   {
           if(s.cpOpcaoClicada == &quot;Etapas&quot; 
                   || s.cpOpcaoClicada == &quot;Subprocessos&quot;  
                   || s.cpOpcaoClicada == &quot;Conectores&quot; 
                   || s.cpOpcaoClicada == &quot;Timers&quot; 
                   || s.cpOpcaoClicada == &quot;Disjuncoes&quot; 
                   || s.cpOpcaoClicada == &quot;Juncoes&quot; 
                   || s.cpOpcaoClicada == &quot;Fins&quot;
                   || s.cpOpcaoClicada == &quot;AcaoWf&quot;
                   || s.cpOpcaoClicada == &quot;CaminhoCondicional&quot;)
           {
                  lpLoading.Hide();     
                  mostraDivEdicaoWorkflow(&quot;divGridEdicaoElementos&quot;);
           }
    }
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript" language="javascript">
        //fazo uso da libreria DOM Drag & Drop script (http://www.dynamicdrive.com/dynamicindex11/domdrag/)
        //en ela se ten qeu definir qual seria o objeto a movimentarse. ('id' da div).
        //e tamben o componente aonde recivira o evento do mouse (ex. 'id' do um componente dentro da div, nesse exemplo a celda 'handler'.
        var theHandle = document.getElementById("handle");
        var theRoot = document.getElementById("divGridEdicaoElementos");
        Drag.init(theHandle, theRoot);
    </script>

    <dxpc:ASPxPopupControl ID="pcResumoWf" runat="server" AllowDragging="True" AllowResize="True"
        ClientInstanceName="pcResumoWf" HeaderText="<%$ Resources:traducao, adm_edicaoWorkflows_resumo_do_modelo_de_fluxo %>"
        Height="580px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ScrollBars="Auto" Width="1022px" Top="100" CloseAction="CloseButton">
        <ClientSideEvents PopUp="function(s, e) {
		 var sWidth = Math.max(0, document.documentElement.clientWidth) - 150;
                 var sHeight = Math.max(0, document.documentElement.clientHeight) - 150;        
       s.SetWidth(sWidth);
       s.SetHeight(sHeight); 
      tlResumoWf.SetHeight(sHeight - 135);
       s.UpdatePosition();

}" />
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <table cellpadding="0" cellspacing="0" class="headerGrid">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 205px">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxComboBox ID="ddlExporta" runat="server" ClientInstanceName="ddlExporta"
                                                        ValueType="System.String">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfTipoArquivo.Set(&quot;tipoArquivo&quot;,s.GetValue());
}" />
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td style="padding-left: 2px">
                                                    <dxcp:ASPxCallbackPanel ID="pnImage" runat="server" ClientInstanceName="pnImage"
                                                        Height="22px" HideContentOnCallback="False" OnCallback="pnImage_Callback" Width="23px">
                                                        <SettingsLoadingPanel Enabled="False" ShowImage="False"></SettingsLoadingPanel>
                                                        <PanelCollection>
                                                            <dxp:PanelContent ID="PanelContent30" runat="server">
                                                                <dxe:ASPxImage ID="imgExportacao" runat="server" ClientInstanceName="imgExportacao"
                                                                    Height="20px" ImageUrl="~/imagens/menuExportacao/iconoExcel.png" Width="20px">
                                                                </dxe:ASPxImage>
                                                            </dxp:PanelContent>
                                                        </PanelCollection>
                                                    </dxcp:ASPxCallbackPanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="Aspxbutton107" runat="server"
                                            OnClick="btnExcel_Click" Text="<%$ Resources:traducao, adm_edicaoWorkflows_exportar %>" ClientInstanceName="Aspxbutton1">
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxwtl:ASPxTreeList ID="tlResumoWf" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlResumoWf"
                                KeyboardSupport="True" KeyFieldName="Codigo"
                                ParentFieldName="CodigoPai" Width="100%">
                                <Columns>
                                    <dxwtl:TreeListHyperLinkColumn Caption="<%$ Resources:traducao, adm_edicaoWorkflows_objeto %>" FieldName="Nome" ShowInCustomizationForm="True"
                                        VisibleIndex="0" Width="50%">
                                        <DataCellTemplate>
                                            <%# getDescricaoObjeto() %>
                                        </DataCellTemplate>
                                    </dxwtl:TreeListHyperLinkColumn>
                                    <dxwtl:TreeListTextColumn Caption="<%$ Resources:traducao, adm_edicaoWorkflows_informa__es_complementares %>" FieldName="Descricao"
                                        ShowInCustomizationForm="True" VisibleIndex="1" Width="50%">
                                        <CellStyle Wrap="True">
                                        </CellStyle>
                                    </dxwtl:TreeListTextColumn>
                                    <dxwtl:TreeListTextColumn Caption="Hint" FieldName="Hint" ShowInCustomizationForm="True"
                                        Visible="False" VisibleIndex="2">
                                    </dxwtl:TreeListTextColumn>
                                </Columns>
                                <Settings VerticalScrollBarMode="Visible" />
                                <SettingsBehavior AllowFocusedNode="True" />

                                <SettingsPopup>
                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                </SettingsPopup>

                                <Styles>
                                    <SelectedNode BackColor="#CCFFCC">
                                    </SelectedNode>
                                </Styles>
                                <ClientSideEvents EndCallback="function(s, e) {
	pcResumoWf.Show();
}" />
                            </dxwtl:ASPxTreeList>
                        </td>
                    </tr>
                </table>
                <dxhf:ASPxHiddenField ID="hfTipoArquivo" runat="server" ClientInstanceName="hfTipoArquivo">
                </dxhf:ASPxHiddenField>
                <dxwtle:ASPxTreeListExporter ID="ASPxTreeListExporter1" runat="server" TreeListID="tlResumoWf">
                    <Settings ExpandAllNodes="True" ExportAllPages="True" ShowTreeButtons="True">
                    </Settings>
                    <Styles>
                        <Cell Wrap="False">
                        </Cell>
                    </Styles>
                </dxwtle:ASPxTreeListExporter>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>

    <%# getDescricaoObjeto() %>
    <dxpc:ASPxPopupControl ID="divEtapa" runat="server" ClientInstanceName="divEtapa"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="800px"
        AllowDragging="True" CloseAction="CloseButton" HeaderText="" CloseAnimationType="Fade"
        CloseOnEscape="True">
        <ContentStyle>
            <Paddings Padding="0px" PaddingBottom="5px" PaddingLeft="5px" PaddingRight="5px" />
        </ContentStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                <table>
                    <tr>
                        <td id="handleEtapa">
                            <table border="0" cellpadding="0" cellspacing="0" style="height: 25px; background-color: #EEDD82"
                                width="100%">
                                <tr>
                                    <td align="center" style="width: 773px" valign="middle">
                                        <dxe:ASPxLabel ID="ASPxLabel28" runat="server" Font-Bold="True"
                                            Text="<%$ Resources:traducao, adm_edicaoWorkflows_informa__es_da_etapa__ %>">
                                        </dxe:ASPxLabel>
                                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True"
                                            Text="<%$ Resources:traducao, adm_edicaoWorkflows_modelo_de_fluxo_ %>">
                                        </dxe:ASPxLabel>
                                        <dxe:ASPxLabel ID="lbWorkflow_etp" runat="server" ClientInstanceName="lbWorkflow_etp"
                                            EnableClientSideAPI="True" Font-Bold="True"
                                            ForeColor="#C04000">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="center" valign="middle">
                                        <dxe:ASPxImage ID="ASPxImage4" runat="server" Cursor="pointer" ImageUrl="~/imagens/botoes/close2.png"
                                            ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_fechar %>">
                                            <ClientSideEvents Click="function(s, e) {
	       divEtapa.Hide();
		    gvFormularios_etp.PerformCallback();
			gv_PessoasAcessos_etp.PerformCallback();
	        e.processOnServer = false;			
}"></ClientSideEvents>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxcp:ASPxCallbackPanel ID="painelCallbackEtapas" runat="server" ClientInstanceName="painelCallbackEtapas"
                                OnCallback="painelCallbackEtapas_Callback">
                                <PanelCollection>
                                    <dxp:PanelContent ID="PanelContent4" runat="server">
                                        <table cellspacing="0" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 36px;">
                                                                        <dxe:ASPxLabel runat="server" Text="ID:" ID="ASPxLabel2">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td></td>
                                                                    <td style="width: 300px;">
                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nome_abreviado_ %>"
                                                                            ID="ASPxLabel3">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td></td>
                                                                    <td style="width: 298px">
                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_descri__o_resumida_ %>"
                                                                            ID="ASPxLabel4">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" ClientInstanceName="edtIdEtapa_etp"
                                                                            ID="edtIdEtapa_etp">
                                                                            <ValidationSettings>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td style="padding: 5px;">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="40" ClientInstanceName="edtNomeAbreviado_etp"
                                                                            TabIndex="1" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_informe_o_nome_abreviado_da_etapa %>"
                                                                            ID="edtNomeAbreviado_etp">
                                                                            <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}"></ClientSideEvents>
                                                                            <ValidationSettings>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="edtDescricaoResumida_etp"
                                                                            TabIndex="2" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_esta_informa__o_ser__mostrada_como__tooltip__ao_posicionar_o_mouse_sobre_o_item_no_gr_fico %>"
                                                                            ID="edtDescricaoResumida_etp" MaxLength="60">
                                                                            <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}"></ClientSideEvents>
                                                                            <ValidationSettings>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <dxe:ASPxCheckBox runat="server" TextSpacing="2px" Text="<%$ Resources:traducao, adm_edicaoWorkflows_etapa_inicial %>" ClientInstanceName="cbEtapaInicial_etp"
                                                                            Width="98px" TabIndex="3" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_marcar_se_esta_for_a_etapa_inicial_do_fluxo %>"
                                                                            ID="cbEtapaInicial_etp">
                                                                        </dxe:ASPxCheckBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server"
                                                            Text="<%$ Resources:traducao, adm_edicaoWorkflows_descri__o_detalhada_ %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo ID="mmDescricao_etp" runat="server" ClientInstanceName="mmDescricao_etp"
                                                            EnableClientSideAPI="True" Height="50px"
                                                            HorizontalAlign="Left" TabIndex="4" Width="100%">
                                                            <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}" />
                                                            <ValidationSettings>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 100px">
                                                                    <dxe:ASPxLabel ID="lblCaptionPrazoEtapa" runat="server" ClientInstanceName="lblCaptionPrazoEtapa"
                                                                        Text="<%$ Resources:traducao, adm_edicaoWorkflows_prazo_previsto_ %>" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_prazo_previsto_para_conclus_o_da_etapa %>">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 65px">
                                                                    <dxe:ASPxTextBox ID="txtQtdTempo" runat="server" ClientInstanceName="txtQtdTempo"
                                                                        Width="100%" HorizontalAlign="Right" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_informe_a_quantidade_de_tempo_previsto_para_a_conclus_o_da_etapa %>">
                                                                        <MaskSettings IncludeLiterals="None" Mask="&lt;0..9999g&gt;"></MaskSettings>
                                                                        <ValidationSettings ErrorDisplayMode="None">
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 125px; padding-left: 10px;">
                                                                    <dxe:ASPxComboBox ID="ddlUnidadeTempo" runat="server" ClientInstanceName="ddlUnidadeTempo"
                                                                        ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_unidade_de_tempo_do_prazo_previsto_para_a_etapa %>" Width="100%">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_minutos %>" Value="minutos" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_horas %>" Value="horas" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_dias %>" Value="dias" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_dias__teis %>" Value="diasuteis" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_semanas %>" Value="semanas" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_meses %>" Value="meses" />
                                                                        </Items>
                                                                        <ValidationSettings ErrorDisplayMode="None">
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 290px; padding-left: 10px;">
                                                                    <dxe:ASPxComboBox ID="ddlReferenciaTempo" runat="server" ClientInstanceName="ddlReferenciaTempo"
                                                                        ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_refer_ncia_para_o_prazo_previsto_da_etapa %>" Width="100%">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_a_partir_do_in_cio_da_etapa %>" Value="IETP" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_a_partir_do_in_cio_do_fluxo %>" Value="IFLX" />
                                                                        </Items>
                                                                        <ValidationSettings ErrorDisplayMode="None">
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tcDivEtapa"
                                                            EnableClientSideAPI="True" TabSpacing="0px" Width="100%"
                                                            ID="tcDivEtapa">
                                                            <TabPages>
                                                                <dxtc:TabPage Text="<%$ Resources:traducao, adm_edicaoWorkflows_formul_rios %>">
                                                                    <ContentCollection>
                                                                        <dxw:ContentControl ID="ContentControl3" runat="server">
                                                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvFormularios_etp" KeyFieldName="HashCode"
                                                                                AutoGenerateColumns="False" Width="100%" ID="gvFormularios_etp"
                                                                                OnAfterPerformCallback="gvFormularios_etp_AfterPerformCallback" OnRowUpdating="gvFormularios_etp_RowUpdating"
                                                                                OnRowDeleting="gvFormularios_etp_RowDeleting" OnRowInserting="gvFormularios_etp_RowInserting"
                                                                                OnCellEditorInitialize="gvFormularios_etp_CellEditorInitialize" OnCustomCallback="gvFormularios_etp_CustomCallback">
                                                                                <Columns>
                                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="80px" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>"
                                                                                        VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" ShowUpdateButton="true"
                                                                                        ShowCancelButton="true">
                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoModeloFormulario" Name="colFormCode_etp"
                                                                                        Caption="<%$ Resources:traducao, adm_edicaoWorkflows_formul_rio %>" Visible="False" VisibleIndex="1">
                                                                                        <PropertiesComboBox TextField="nomeFormulario" ValueField="codigoModeloFormulario"
                                                                                            ValueType="System.Int32" DisplayFormatString="&lt;table&gt;&lt;tr&gt;&lt;td&gt;{0}&lt;/td&gt;&lt;/tr&gt;&lt;/table&gt;"
                                                                                            ClientInstanceName="colFormCode_etp" EnableClientSideAPI="True">
                                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                        onColFormCode_etpSelectedIndexChanged();
	                        e.processOnServer = false;
                        }"></ClientSideEvents>
                                                                                            <ValidationSettings CausesValidation="True">
                                                                                                <RequiredField IsRequired="True"></RequiredField>
                                                                                            </ValidationSettings>
                                                                                        </PropertiesComboBox>
                                                                                        <EditFormSettings ColumnSpan="2" Visible="True" VisibleIndex="0" CaptionLocation="Top"></EditFormSettings>
                                                                                        <HeaderStyle Font-Size="7pt"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                    <dxwgv:GridViewDataTextColumn FieldName="NomeFormulario" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_formul_rio %>"
                                                                                        VisibleIndex="2">
                                                                                        <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                                        <HeaderStyle Font-Size="7pt"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataTextColumn FieldName="TituloFormulario" Name="colFormTitle_etp"
                                                                                        Caption="<%$ Resources:traducao, adm_edicaoWorkflows_t_tulo %>" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_t_tulos_dos_formul_rios__quando_da_apresenta__o_dos_formul_rios__estes_ser_o_os_t_tulos_que_cada_um_ter__ %>"
                                                                                        VisibleIndex="3">
                                                                                        <PropertiesTextEdit MaxLength="50" ClientInstanceName="colFormTitle_etp" EnableClientSideAPI="True">
                                                                                            <ClientSideEvents TextChanged="function(s, e) {
	onColFormTitle_etpTextChanged(s, e);
	e.processOnServer= false;
}"></ClientSideEvents>
                                                                                            <ValidationSettings CausesValidation="True">
                                                                                                <RequiredField IsRequired="True"></RequiredField>
                                                                                            </ValidationSettings>
                                                                                        </PropertiesTextEdit>
                                                                                        <EditFormSettings ColumnSpan="4" Visible="True" VisibleIndex="1" CaptionLocation="Top"></EditFormSettings>
                                                                                        <HeaderStyle Font-Size="7pt"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataCheckColumn FieldName="TipoAcessoFormulario" Width="70px" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_somente_leitura_ %>"
                                                                                        ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_nas_linhas_em_que_esta_coluna_estiver_marcada__indica_que_formul_rio_a_ser_mostrado_ser__somente_para_leitura_ %>"
                                                                                        VisibleIndex="4">
                                                                                        <PropertiesCheckEdit DisplayTextGrayed="False" ClientInstanceName="cbReadOnly_frmEtp"
                                                                                            EnableClientSideAPI="True">
                                                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	onCbReadOnly_frmEtp__Checked(s,e);
	e.processOnServer = false;
}"></ClientSideEvents>
                                                                                        </PropertiesCheckEdit>
                                                                                        <EditFormSettings VisibleIndex="2" CaptionLocation="Top"></EditFormSettings>
                                                                                        <HeaderStyle HorizontalAlign="Center" Wrap="True" Font-Size="7pt"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataCheckColumn>
                                                                                    <dxwgv:GridViewDataCheckColumn FieldName="PreenchimentoObrigatorio" Width="70px"
                                                                                        Caption="<%$ Resources:traducao, adm_edicaoWorkflows_obrigat_rio_ %>" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_nas_linhas_em_que_esta_coluna_estiver_marcada__indica_que_formul_rio_a_ser_mostrado_ser__de_preenchimento_obrigat_rio_ %>"
                                                                                        VisibleIndex="6">
                                                                                        <PropertiesCheckEdit DisplayTextGrayed="False" ClientInstanceName="cbRequired_frmEtp"
                                                                                            EnableClientSideAPI="True">
                                                                                        </PropertiesCheckEdit>
                                                                                        <EditFormSettings VisibleIndex="3" CaptionLocation="Top"></EditFormSettings>
                                                                                    </dxwgv:GridViewDataCheckColumn>
                                                                                    <dxwgv:GridViewDataCheckColumn FieldName="NovoCadaOcorrencia" Width="80px" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_novo_em_cada_ocorr_ncia_ %>"
                                                                                        ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_nas_linhas_em_que_esta_coluna_estiver_marcada__indica_que_se_o_fluxo_passar_mais_de_uma_vez_por_esta_etapa____para_criar_um_formul_rio_novo_a_cada_vez_ %>"
                                                                                        VisibleIndex="7">
                                                                                        <PropertiesCheckEdit DisplayTextGrayed="False" ClientInstanceName="cbNovoCadaOcorrencia_frmEtp">
                                                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	onCbNovoCadaOcorrencia_frmEtp__Checked(s,e);
	e.processOnServer = false;
	
}"></ClientSideEvents>
                                                                                        </PropertiesCheckEdit>
                                                                                        <EditFormSettings VisibleIndex="3" CaptionLocation="Top"></EditFormSettings>
                                                                                        <HeaderStyle HorizontalAlign="Center" Wrap="True" Font-Size="7pt"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataCheckColumn>
                                                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoEtapaWfOrigem" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_etapa_origem %>"
                                                                                        ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_se_o_formul_rio_a_ser_mostrado_nesta_etapa__venha_de_uma_etapa_anterior__indicar_aqui_qual___a_etapa_ %>"
                                                                                        VisibleIndex="9" Visible="False">
                                                                                        <PropertiesComboBox ValueType="System.Int32" ClientInstanceName="ddlEtapaOrigem_etp"
                                                                                            EnableClientSideAPI="True">
                                                                                            <ClientSideEvents Init="function(s, e) {
	ddlEtapaOrigem_etpInit(s,e);
	e.ProcessOnServer = false;
}"></ClientSideEvents>
                                                                                            <DropDownButton ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_se_o_formul_rio_a_ser_mostrado_nesta_etapa__vem_de_uma_etapa_anterior__selecione_a_etapa_de_origem_ %>">
                                                                                            </DropDownButton>
                                                                                        </PropertiesComboBox>
                                                                                        <EditFormSettings ColumnSpan="4" Visible="True" VisibleIndex="5" CaptionLocation="Top"></EditFormSettings>
                                                                                        <HeaderStyle Font-Size="7pt"></HeaderStyle>
                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                    <dxwgv:GridViewDataComboBoxColumn FieldName="idEtapa" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_etapa_origem %>" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_se_o_formul_rio_a_ser_mostrado_nesta_etapa__venha_de_uma_etapa_anterior__indicar_aqui_qual___a_etapa_ %>"
                                                                                        VisibleIndex="10">
                                                                                        <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                    <dxwgv:GridViewDataTextColumn FieldName="HashCode" ShowInCustomizationForm="True"
                                                                                        Visible="False" VisibleIndex="11">
                                                                                        <EditFormSettings Visible="False"></EditFormSettings>
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxtv:GridViewDataCheckColumn Caption="Requer Assinatura Digital ?" FieldName="RequerAssinaturaDigital"
                                                                                        ShowInCustomizationForm="True" VisibleIndex="8" Width="80px">
                                                                                        <PropertiesCheckEdit ClientInstanceName="cbRequerAssinaturaDigital_frmEtp" DisplayTextGrayed="False">
                                                                                        </PropertiesCheckEdit>
                                                                                        <EditFormSettings CaptionLocation="Top" VisibleIndex="4" />
                                                                                        <EditFormSettings VisibleIndex="4" CaptionLocation="Top"></EditFormSettings>
                                                                                        <HeaderStyle Font-Size="7pt" HorizontalAlign="Center" Wrap="True" />
                                                                                    </dxtv:GridViewDataCheckColumn>
                                                                                    <dxtv:GridViewDataTextColumn Caption="<%$ Resources:traducao, adm_edicaoWorkflows_ordem %>" FieldName="OrdemFormularioEtapa" Name="OrdemFormulario"
                                                                                        ShowInCustomizationForm="True" VisibleIndex="5" SortIndex="0" SortOrder="Ascending"
                                                                                        Visible="False">
                                                                                        <Settings SortMode="Value" />
                                                                                        <EditFormSettings Caption="<%$ Resources:traducao, adm_edicaoWorkflows_ordem %>" CaptionLocation="Top" Visible="True" VisibleIndex="5" />
                                                                                        <Settings SortMode="Value"></Settings>
                                                                                        <EditFormSettings Visible="True" VisibleIndex="5" CaptionLocation="Top" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_ordem %>"></EditFormSettings>
                                                                                    </dxtv:GridViewDataTextColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"
                                                                                    SortMode="Value"></SettingsBehavior>
                                                                                <SettingsPager Mode="ShowAllRecords">
                                                                                </SettingsPager>
                                                                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="4">
                                                                                </SettingsEditing>
                                                                                <SettingsPopup>
                                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                        Width="650px" />
                                                                                    <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" VerticalOffset="-30" />

                                                                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                                </SettingsPopup>
                                                                                <Settings ShowTitlePanel="True" ShowGroupButtons="False" VerticalScrollBarMode="Visible"
                                                                                    VerticalScrollableHeight="132" ShowStatusBar="Hidden"></Settings>
                                                                                <SettingsText PopupEditFormCaption="<%$ Resources:traducao, adm_edicaoWorkflows_formul_rios_associados___etapa %>" EmptyDataRow="<%$ Resources:traducao, adm_edicaoWorkflows_nenhum_formul_rio_associado_ %>"></SettingsText>
                                                                                <Styles>
                                                                                    <Header Font-Size="7pt">
                                                                                    </Header>
                                                                                </Styles>
                                                                                <SettingsCommandButton>
                                                                                    <DeleteButton Image-Url="~/imagens/botoes/excluirReg02.png">
                                                                                        <Image Url="~/imagens/botoes/excluirReg02.png"></Image>
                                                                                    </DeleteButton>
                                                                                    <EditButton Image-Url="~/imagens/botoes/editarReg02.png">
                                                                                        <Image Url="~/imagens/botoes/editarReg02.png"></Image>
                                                                                    </EditButton>
                                                                                    <UpdateButton Image-Url="~/imagens/botoes/salvar.png">
                                                                                        <Image Url="~/imagens/botoes/salvar.png"></Image>
                                                                                    </UpdateButton>
                                                                                    <CancelButton Image-Url="~/imagens/botoes/cancelar.png">
                                                                                        <Image Url="~/imagens/botoes/cancelar.png"></Image>
                                                                                    </CancelButton>
                                                                                </SettingsCommandButton>
                                                                            </dxwgv:ASPxGridView>
                                                                        </dxw:ContentControl>
                                                                    </ContentCollection>
                                                                </dxtc:TabPage>
                                                                <dxtc:TabPage Text="<%$ Resources:traducao, adm_edicaoWorkflows_acessos %>">
                                                                    <ContentCollection>
                                                                        <dxw:ContentControl ID="ContentControl4" runat="server">
                                                                            <dxwgv:ASPxGridView runat="server" ClientInstanceName="gv_PessoasAcessos_etp" KeyFieldName="CodigoPerfilWf"
                                                                                AutoGenerateColumns="False" Width="100%" ID="gv_PessoasAcessos_etp"
                                                                                OnAfterPerformCallback="gv_PessoasAcessos_etp_AfterPerformCallback" OnRowUpdating="gv_PessoasAcessos_etp_RowUpdating"
                                                                                OnRowDeleting="gv_PessoasAcessos_etp_RowDeleting" OnRowInserting="gv_PessoasAcessos_etp_RowInserting"
                                                                                OnCellEditorInitialize="gv_PessoasAcessos_etp_CellEditorInitialize">
                                                                                <Columns>
                                                                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>" VisibleIndex="0"
                                                                                        Width="80px" ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true"
                                                                                        ShowUpdateButton="true">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                    </dxwgv:GridViewCommandColumn>
                                                                                    <dxwgv:GridViewDataComboBoxColumn Caption="<%$ Resources:traducao, adm_edicaoWorkflows_perfil %>" FieldName="CodigoPerfilWf" Name="CodigoPerfilWf"
                                                                                        Visible="False" VisibleIndex="1" Width="30px">
                                                                                        <PropertiesComboBox ValueType="System.String" Width="150px">
                                                                                            <ValidationSettings>
                                                                                                <RequiredField IsRequired="True"></RequiredField>
                                                                                            </ValidationSettings>
                                                                                        </PropertiesComboBox>
                                                                                        <EditFormSettings Visible="True" CaptionLocation="Top" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_perfil_ %>"></EditFormSettings>
                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                    <dxwgv:GridViewDataTextColumn Caption="<%$ Resources:traducao, adm_edicaoWorkflows_perfil %>" FieldName="NomePerfilWf" Name="NomePerfilWf"
                                                                                        ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_indique_os_perfis_de_pessoas_que_ter_o_acesso_a_esta_etapa %>" VisibleIndex="1">
                                                                                        <PropertiesTextEdit Width="230px">
                                                                                            <ValidationSettings CausesValidation="True">
                                                                                                <RequiredField IsRequired="True" />
                                                                                            </ValidationSettings>
                                                                                        </PropertiesTextEdit>
                                                                                        <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                                        <EditCellStyle HorizontalAlign="Left">
                                                                                        </EditCellStyle>
                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                    <dxwgv:GridViewDataComboBoxColumn Caption="<%$ Resources:traducao, adm_edicaoWorkflows_acesso %>" FieldName="TipoAcesso" Name="TipoAcesso"
                                                                                        ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_indique_o_tipo_de_acesso_de_cada_perfil %>" VisibleIndex="2" Width="90px">
                                                                                        <PropertiesComboBox ValueType="System.String" Width="80px">
                                                                                            <Items>
                                                                                                <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_consulta %>" Value="C"></dxe:ListEditItem>
                                                                                                <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>" Value="A"></dxe:ListEditItem>
                                                                                            </Items>
                                                                                            <ClientSideEvents Init="function(s, e) {
	s.SetSelectedIndex(1);
	
}" />
                                                                                            <ValidationSettings CausesValidation="True">
                                                                                                <RequiredField IsRequired="True" />
                                                                                            </ValidationSettings>
                                                                                        </PropertiesComboBox>
                                                                                        <EditFormSettings Visible="True" CaptionLocation="Top" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_acesso_ %>"></EditFormSettings>
                                                                                        <EditCellStyle HorizontalAlign="Left">
                                                                                        </EditCellStyle>
                                                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                                                </Columns>
                                                                                <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>
                                                                                <SettingsPager Mode="ShowAllRecords">
                                                                                </SettingsPager>
                                                                                <SettingsEditing Mode="PopupEditForm">
                                                                                </SettingsEditing>
                                                                                <SettingsPopup>
                                                                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                        Width="350px" />
                                                                                    <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                                </SettingsPopup>
                                                                                <Settings ShowTitlePanel="True" ShowGroupButtons="False" VerticalScrollBarMode="Visible"
                                                                                    VerticalScrollableHeight="144" ShowStatusBar="Hidden"></Settings>
                                                                                <SettingsText PopupEditFormCaption="<%$ Resources:traducao, adm_edicaoWorkflows_pessoas_com_acesso___etapa %>" EmptyDataRow="<%$ Resources:traducao, adm_edicaoWorkflows_nenhum_perfil_cadastrado %>"></SettingsText>
                                                                                <Styles>
                                                                                    <Header Font-Size="7pt">
                                                                                    </Header>
                                                                                </Styles>
                                                                                <SettingsCommandButton>
                                                                                    <DeleteButton Image-Url="~/imagens/botoes/excluirReg02.png">
                                                                                        <Image Url="~/imagens/botoes/excluirReg02.png"></Image>
                                                                                    </DeleteButton>
                                                                                    <EditButton Image-Url="~/imagens/botoes/editarReg02.png">
                                                                                        <Image Url="~/imagens/botoes/editarReg02.png"></Image>
                                                                                    </EditButton>
                                                                                    <UpdateButton Image-Url="~/imagens/botoes/salvar.png">
                                                                                        <Image Url="~/imagens/botoes/salvar.png"></Image>
                                                                                    </UpdateButton>
                                                                                    <CancelButton Image-Url="~/imagens/botoes/cancelar.png">
                                                                                        <Image Url="~/imagens/botoes/cancelar.png"></Image>
                                                                                    </CancelButton>
                                                                                </SettingsCommandButton>
                                                                            </dxwgv:ASPxGridView>
                                                                        </dxw:ContentControl>
                                                                    </ContentCollection>
                                                                </dxtc:TabPage>
                                                            </TabPages>
                                                            <ContentStyle>
                                                                <border bordercolor="#4986A2"></border>
                                                            </ContentStyle>
                                                        </dxtc:ASPxPageControl>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 10px" align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table cellspacing="0" cellpadding="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="left" style="width: 100%">
                                                                        <dxtv:ASPxCheckBox ID="cbOcultaBotoes" runat="server" CheckState="Unchecked" ClientInstanceName="cbOcultaBotoes"
                                                                            TabIndex="3" Text="<%$ Resources:traducao, adm_edicaoWorkflows_ocultar_os_bot_es_de_a__es_nessa_etapa %>"
                                                                            TextSpacing="2px" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_marcar_se_essa_for_uma_etapa_com_movimenta__o_autom_tica_ %>"
                                                                            Width="300px">
                                                                        </dxtv:ASPxCheckBox>
                                                                    </td>
                                                                    <td style="padding: 5px;">
                                                                        <dxtv:ASPxButton ID="btnOk_etp" runat="server" ClientInstanceName="btnOk_etp"
                                                                            Height="10px" TabIndex="7" Text="Ok" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
                if(onOkDivEtapaClick(s, e))
	                {
		                gvFormularios_etp.PerformCallback();
		                gv_PessoasAcessos_etp.PerformCallback();

	                }	
                }" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                    <td align="right"></td>
                                                                    <td>
                                                                        <dxtv:ASPxButton ID="btnCancelar_etp" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar_etp"
                                                                            Height="10px" TabIndex="8" Text="<%$ Resources:traducao, adm_edicaoWorkflows_cancelar %>" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	                divEtapa.Hide();
	                gvFormularios_etp.PerformCallback();
	                gv_PessoasAcessos_etp.PerformCallback();
	                e.processOnServer = false;
                }" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <ClientSideEvents EndCallback="function(s, e) {
	// faz os ajustes de exibição necessários à div Etapa
	preparaExibicaoDivEtapa('E');
    ocultaDivEdicaoWorkflow('divGridEdicaoElementos');
    lpLoading.Hide();
    divEtapa.Show();
}"></ClientSideEvents>
                            </dxcp:ASPxCallbackPanel>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <%--        --------------[DIV ETAPAS  --%>
    <dxpc:ASPxPopupControl ID="divSubprocesso" runat="server" ClientInstanceName="divSubprocesso"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="800px"
        AllowDragging="True" CloseAction="CloseButton" HeaderText="" CloseAnimationType="Fade"
        CloseOnEscape="True">
        <ContentStyle>
            <Paddings Padding="0px" PaddingBottom="5px" PaddingLeft="5px" PaddingRight="5px" />
        </ContentStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl9" runat="server">
                <table>
                    <tr>
                        <td id="handleSubfluxo">
                            <table border="0" cellpadding="0" cellspacing="0" style="height: 25px; background-color: #EEDD82"
                                width="100%">
                                <tr>
                                    <td align="center" style="width: 773px" valign="middle">
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True"
                                            Text="<%$ Resources:traducao, adm_edicaoWorkflows_informa__es_do_subprocesso__ %>">
                                        </dxe:ASPxLabel>
                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Font-Bold="True"
                                            Text="<%$ Resources:traducao, adm_edicaoWorkflows_modelo_de_fluxo_ %>">
                                        </dxe:ASPxLabel>
                                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" ClientInstanceName="lbWorkflow_sub"
                                            EnableClientSideAPI="True" Font-Bold="True"
                                            ForeColor="#C04000">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="center" valign="middle">
                                        <dxe:ASPxImage ID="ASPxImage12" runat="server" Cursor="pointer" ImageUrl="~/imagens/botoes/close2.png"
                                            ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_fechar %>">
                                            <ClientSideEvents Click="function(s, e) {
	       divSubprocesso.Hide();
	        e.processOnServer = false;			
}"></ClientSideEvents>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxcp:ASPxCallbackPanel ID="painelCallbackSubprocesso" runat="server" ClientInstanceName="painelCallbackSubprocesso"
                                OnCallback="painelCallbackSubprocesso_Callback">
                                <PanelCollection>
                                    <dxp:PanelContent ID="PanelContent7" runat="server">
                                        <table cellspacing="0" cellpadding="0" style="width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 46px">
                                                                        <dxe:ASPxLabel runat="server" Text="ID:" ID="ASPxLabel13">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td></td>
                                                                    <td style="width: 310px">
                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nome_abreviado_ %>"
                                                                            ID="ASPxLabel14">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td></td>
                                                                    <td style="width: 298px">
                                                                        <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_descri__o_resumida_ %>"
                                                                            ID="ASPxLabel17">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" ClientInstanceName="edtIdEtapa_sub"
                                                                            ID="edtIdEtapa_sub">
                                                                            <ValidationSettings>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="40" ClientInstanceName="edtNomeAbreviado_sub"
                                                                            TabIndex="1" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_informe_o_nome_abreviado_do_subprocesso %>"
                                                                            ID="edtNomeAbreviado_sub">
                                                                            <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}"></ClientSideEvents>
                                                                            <ValidationSettings>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                            </ValidationSettings>
                                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                            </DisabledStyle>
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="edtDescricaoResumida_sub"
                                                                            TabIndex="2" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_esta_informa__o_ser__mostrada_como__tooltip__ao_posicionar_o_mouse_sobre_o_item_no_gr_fico %>"
                                                                            ID="edtDescricaoResumida_sub" MaxLength="60">
                                                                            <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}"></ClientSideEvents>
                                                                            <ValidationSettings>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
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
                                                        <dxe:ASPxLabel ID="ASPxLabel18" runat="server"
                                                            Text="<%$ Resources:traducao, adm_edicaoWorkflows_descri__o_detalhada_ %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxMemo ID="mmDescricao_sub" runat="server" ClientInstanceName="mmDescricao_sub"
                                                            EnableClientSideAPI="True" Height="50px"
                                                            HorizontalAlign="Left" TabIndex="4" Width="100%">
                                                            <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}" />
                                                            <ValidationSettings>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel ID="ASPxLabel20" runat="server"
                                                            Text="<%$ Resources:traducao, adm_edicaoWorkflows_fluxo_a_executar_ %>">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxtv:ASPxComboBox ID="cmbFluxos_sub" runat="server" ClientInstanceName="cmbFluxos_sub" EncodeHtml="False" Width="100%" ValueType="System.Int32">
                                                        </dxtv:ASPxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="formulario-botao">
                                                                        <dxtv:ASPxButton ID="btnOk_sub" runat="server" ClientInstanceName="btnOk_sub"
                                                                            Height="10px" TabIndex="7" Text="Ok" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
                onOkDivSubprocessoClick(s, e);
                }" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                    <td class="formulario-botao">
                                                                        <dxtv:ASPxButton ID="btnCancelar_sub" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar_sub"
                                                                            Height="10px" TabIndex="8" Text="<%$ Resources:traducao, adm_edicaoWorkflows_cancelar %>" Width="90px">
                                                                            <ClientSideEvents Click="function(s, e) {
	                divSubprocesso.Hide();
	                e.processOnServer = false;
                }" />
                                                                        </dxtv:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <ClientSideEvents EndCallback="function(s, e) {
	// faz os ajustes de exibição necessários à div Subprocesso
	preparaExibicaoDivSubprocesso('E');
    divSubprocesso.Show();
}"></ClientSideEvents>
                            </dxcp:ASPxCallbackPanel>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <%--        --------------[DIV SUBPROCESSO  --%>
    <div id="divDescricao" style="padding-right: 10px; display: none; padding-left: 10px; left: 35px; padding-bottom: 10px; width: 400px; padding-top: 10px; top: 4045px;"
        class="fundoDados">
        <table>
            <tr>
                <td id="HandleDescricao">
                    <table>
                        <tr>
                            <td align="center" style="height: 25px;" valign="middle">
                                <dxe:ASPxLabel ID="lblCAptionDescricao" runat="server" ClientInstanceName="lblCAptionDescricao"
                                    Font-Bold="True" Text="<%$ Resources:traducao, adm_edicaoWorkflows_descri__o___ %>">
                                </dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="lblCaptionTipoElemento" runat="server" ClientInstanceName="lblCaptionTipoElemento"
                                    Font-Bold="True" ForeColor="#C04000" Text="ASPxLabel">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="center" style="width: 30px;" valign="middle">
                                <dxe:ASPxImage ID="imgCerrarDescricao" runat="server" ClientInstanceName="imgCerrarDescricao"
                                    ImageUrl="~/imagens/botoes/close2.png" Cursor="pointer">
                                    <ClientSideEvents Click="function(s, e) {
	ocultaDivEdicaoWorkflow(&quot;divDescricao&quot;);
	e.processOnServer = false;
}"></ClientSideEvents>
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="height: 10px">
                    <dxcp:ASPxCallbackPanel ID="pnlCnkDisJunFim" runat="server" ClientInstanceName="pnlCnkDisJunFim"
                        Width="100%" OnCallback="pnlCnkDisJunFim_Callback">
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent50" runat="server">
                                <table cellspacing='0' cellpadding='0' width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_descri__o_ %>" ClientInstanceName="lblDescricaoDescricao"
                                                    ID="lblDescricaoDescricao">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txtDescricaoDivDescricao"
                                                    TabIndex="1" ID="txtDescricaoDivDescricao">
                                                    <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}"></ClientSideEvents>
                                                    <ValidationSettings>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" align='right'></td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 395px">
                                                <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="formulario-botao">
                                                                <dxe:ASPxButton runat="server" ClientInstanceName="btnOkDivDescricao" Text="Ok" Width="90px"
                                                                    TabIndex="2" ID="btnOkDivDescricao">
                                                                    <ClientSideEvents Click="function(s, e) {

	if (true == onOkDisJunEndClick())
		ocultaDivEdicaoWorkflow('divDescricao');

	e.processOnServer = false;

}"></ClientSideEvents>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td class="formulario-botao">
                                                                <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelarDivDescricao" Text="<%$ Resources:traducao, adm_edicaoWorkflows_cancelar %>"
                                                                    Width="90px" TabIndex="3" ID="btnCancelarDivDescricao">
                                                                    <ClientSideEvents Click="function(s, e) {
	                            ocultaDivEdicaoWorkflow('divDescricao');
                                e.processOnServer = false;
                            }"></ClientSideEvents>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) {
	preparaExibicaoDivDisJunEnd(pnlCnkDisJunFim.cpElementType);
	mostraDivEdicaoWorkflow(&quot;divDescricao&quot;); 
}"></ClientSideEvents>
                    </dxcp:ASPxCallbackPanel>
                </td>
            </tr>
        </table>
    </div>
    <%--    ---------[div para ser mostrada a descricao do componente fim, disjunção e junção --%>
    <dxpc:ASPxPopupControl ID="divAcaoWf" runat="server" ClientInstanceName="divAcaoWf"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="False"
        Width="800px">
        <ContentStyle>
            <Paddings Padding="0px" PaddingBottom="5px" PaddingLeft="5px" PaddingRight="5px" />
        </ContentStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
                <dxcp:ASPxCallbackPanel ID="painelCallbackAcoesWf" runat="server" ClientInstanceName="painelCallbackAcoesWf"
                    OnCallback="painelCallbackAcoesWf_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent6" runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td id="handleAcaoWf">
                                            <table style="height: 25px; background-color: #DCDCDC" cellspacing="0" cellpadding="0"
                                                width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td valign="middle" align="center">&nbsp;<dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_modelo_de_fluxo_ %>" ClientInstanceName="lblCaptionDivAcaoWf"
                                                            Font-Bold="True" ID="lblCaptionDivAcaoWf">
                                                        </dxe:ASPxLabel>
                                                            &nbsp;
                                                            <dxe:ASPxLabel runat="server" ClientInstanceName="lbWorkflow_acaoWf" EnableClientSideAPI="True"
                                                                Font-Bold="True" ForeColor="#C04000" ID="lbWorkflow_acaoWf">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td style="width: 30px; height: 25px" valign="middle" align="center">
                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/botoes/close2.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_fechar %>"
                                                                Cursor="pointer" ID="Aspximage16">
                                                                <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    divAcaoWf.Hide();
}"></ClientSideEvents>
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td align="center">
                                            <table style="height: 40px" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td valign="middle">
                                                            <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/atencao2.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_fluxo %>"
                                                                ClientInstanceName="imgNomeDoProcessoConector" Cursor="pointer" ID="ASPxImage17">
                                                                <ClientSideEvents Click="function(s, e) {
	                __ta_insertText(elEditor, &quot; [nomeDoFluxo] &quot;);
                }"></ClientSideEvents>
                                                            </dxe:ASPxImage>
                                                        </td>
                                                        <td></td>
                                                        <td valign="middle">
                                                            <dxe:ASPxLabel runat="server" Text="Atenção !!! As notificações e ações automáticas informadas nesta tela ocorrerão quando e se o Fluxo for "
                                                                ClientInstanceName="lblMensagemWf" ID="lblMensagemWf">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td style="width: 5px" valign="middle"></td>
                                                        <td valign="middle">
                                                            <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoWf" Font-Bold="False"
                                                                ID="lblAcaoWf">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxtc:ASPxPageControl runat="server" ActiveTabIndex="0" ClientInstanceName="tcDivAcaoWf"
                                                EnableClientSideAPI="True" TabSpacing="0px" Width="100%"
                                                TabIndex="4" ID="tcDivAcaoWf">
                                                <TabPages>
                                                    <dxtc:TabPage Text="<%$ Resources:traducao, adm_edicaoWorkflows_perfis_notificados %>">
                                                        <TabStyle Wrap="False">
                                                        </TabStyle>
                                                        <ContentCollection>
                                                            <dxw:ContentControl ID="ContentControl51" runat="server">
                                                                <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gv_GruposNotificados_wf" KeyFieldName="CodigoPerfilWf"
                                                                                    AutoGenerateColumns="False" Width="100%" ID="gv_GruposNotificados_wf"
                                                                                    OnCellEditorInitialize="gv_GruposNotificados_wf_CellEditorInitialize" OnRowInserting="gv_GruposNotificados_wf_RowInserting"
                                                                                    OnRowDeleting="gv_GruposNotificados_wf_RowDeleting" OnRowUpdating="gv_GruposNotificados_wf_RowUpdating"
                                                                                    OnAfterPerformCallback="gv_GruposNotificados_wf_AfterPerformCallback">
                                                                                    <Columns>
                                                                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="80px" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>"
                                                                                            VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true"
                                                                                            ShowUpdateButton="true">
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                        </dxwgv:GridViewCommandColumn>
                                                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoPerfilWf" Name="CodigoPerfilWf"
                                                                                            Width="30px" Caption="Perfil" Visible="False" VisibleIndex="1">
                                                                                            <EditFormSettings Visible="True" CaptionLocation="Top" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_perfil_ %>"></EditFormSettings>
                                                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="NomePerfilWf" Name="NomePerfilWf" Caption="Perfil"
                                                                                            VisibleIndex="1">
                                                                                            <PropertiesTextEdit>
                                                                                                <ValidationSettings CausesValidation="True">
                                                                                                    <RequiredField IsRequired="True"></RequiredField>
                                                                                                </ValidationSettings>
                                                                                            </PropertiesTextEdit>
                                                                                            <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="Mensagem" Name="Mensagem" Width="130px"
                                                                                            Caption="<%$ Resources:traducao, adm_edicaoWorkflows_tipo_de_mensagem %>" VisibleIndex="2">
                                                                                            <PropertiesComboBox ValueType="System.String">
                                                                                                <Items>
                                                                                                    <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>" Value="E"></dxe:ListEditItem>
                                                                                                    <dxe:ListEditItem Text="<%$ Resources:traducao, adm_edicaoWorkflows_acompanhamento %>" Value="S" Selected="True"></dxe:ListEditItem>
                                                                                                </Items>
                                                                                                <ValidationSettings CausesValidation="True">
                                                                                                    <RequiredField IsRequired="True"></RequiredField>
                                                                                                </ValidationSettings>
                                                                                            </PropertiesComboBox>
                                                                                            <EditFormSettings Visible="True" CaptionLocation="Top" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_tipo_de_mensagem_ %>"></EditFormSettings>
                                                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                                                    </Columns>
                                                                                    <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>
                                                                                    <SettingsPager Mode="ShowAllRecords">
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <SettingsPopup>
                                                                                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                                            Width="650px" />
                                                                                        <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                                    </SettingsPopup>
                                                                                    <Settings ShowTitlePanel="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="140"></Settings>
                                                                                    <SettingsText PopupEditFormCaption="<%$ Resources:traducao, adm_edicaoWorkflows_perfis_notificados %>" EmptyDataRow="<%$ Resources:traducao, adm_edicaoWorkflows_nenhum_perfil_notificado___ %>"></SettingsText>
                                                                                    <SettingsCommandButton>
                                                                                        <DeleteButton Image-Url="~/imagens/botoes/excluirReg02.png">
                                                                                            <Image Url="~/imagens/botoes/excluirReg02.png"></Image>
                                                                                        </DeleteButton>
                                                                                        <UpdateButton Image-Url="~/imagens/botoes/salvar.png">
                                                                                            <Image Url="~/imagens/botoes/salvar.png"></Image>
                                                                                        </UpdateButton>
                                                                                        <CancelButton Image-Url="~/imagens/botoes/cancelar.png">
                                                                                            <Image Url="~/imagens/botoes/cancelar.png"></Image>
                                                                                        </CancelButton>
                                                                                    </SettingsCommandButton>
                                                                                </dxwgv:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                    <dxtc:TabPage Text="<%$ Resources:traducao, adm_edicaoWorkflows_a__es_autom_ticas %>">
                                                        <TabStyle Wrap="False">
                                                        </TabStyle>
                                                        <ContentCollection>
                                                            <dxw:ContentControl ID="ContentControl61" runat="server">
                                                                <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gv_Acoes_wf" KeyFieldName="CodigoAcaoAutomaticaWf"
                                                                                    AutoGenerateColumns="False" Width="100%" ID="gv_Acoes_wf" OnCellEditorInitialize="gv_Acoes_wf_CellEditorInitialize"
                                                                                    OnRowInserting="gv_Acoes_wf_RowInserting" OnRowDeleting="gv_Acoes_wf_RowDeleting"
                                                                                    OnRowUpdating="gv_Acoes_wf_RowUpdating" OnAfterPerformCallback="gv_Acoes_wf_AfterPerformCallback">
                                                                                    <Columns>
                                                                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="80px" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o %>"
                                                                                            VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true"
                                                                                            ShowUpdateButton="true">
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                        </dxwgv:GridViewCommandColumn>
                                                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoAcaoAutomaticaWf" Name="colActionCode_cnt"
                                                                                            Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o_autom_tica %>" Visible="False" VisibleIndex="1">
                                                                                            <PropertiesComboBox ValueType="System.String" Width="300px" ClientInstanceName="colActionCode_cnt"
                                                                                                EnableClientSideAPI="True">
                                                                                                <ValidationSettings CausesValidation="True">
                                                                                                    <RequiredField IsRequired="True"></RequiredField>
                                                                                                </ValidationSettings>
                                                                                            </PropertiesComboBox>
                                                                                            <EditFormSettings Visible="True" VisibleIndex="0" CaptionLocation="Top" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o_autom_tica__ %>"></EditFormSettings>
                                                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="Nome" Caption="<%$ Resources:traducao, adm_edicaoWorkflows_a__o_autom_tica %>"
                                                                                            VisibleIndex="1">
                                                                                            <EditFormSettings Visible="False" CaptionLocation="Top"></EditFormSettings>
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                    </Columns>
                                                                                    <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"></SettingsBehavior>
                                                                                    <SettingsPager Mode="ShowAllRecords">
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <SettingsPopup>
                                                                                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
                                                                                        <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                                        <HeaderFilter MinHeight="140px"></HeaderFilter>
                                                                                    </SettingsPopup>
                                                                                    <Settings ShowTitlePanel="True" ShowGroupButtons="False" VerticalScrollBarMode="Visible"
                                                                                        VerticalScrollableHeight="140" ShowStatusBar="Hidden"></Settings>
                                                                                    <SettingsText PopupEditFormCaption="<%$ Resources:traducao, adm_edicaoWorkflows_a__es %>" EmptyDataRow="<%$ Resources:traducao, adm_edicaoWorkflows_nenhuma_a__o_autom_tica___ %>"></SettingsText>
                                                                                    <SettingsCommandButton>
                                                                                        <DeleteButton Image-Url="~/imagens/botoes/excluirReg02.png">
                                                                                            <Image Url="~/imagens/botoes/excluirReg02.png"></Image>
                                                                                        </DeleteButton>
                                                                                        <UpdateButton Image-Url="~/imagens/botoes/salvar.png">
                                                                                            <Image Url="~/imagens/botoes/salvar.png"></Image>
                                                                                        </UpdateButton>
                                                                                        <CancelButton Image-Url="~/imagens/botoes/cancelar.png">
                                                                                            <Image Url="~/imagens/botoes/cancelar.png"></Image>
                                                                                        </CancelButton>
                                                                                    </SettingsCommandButton>
                                                                                </dxwgv:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                </TabPages>
                                                <ContentStyle>
                                                    <border bordercolor="#4986A2"></border>
                                                </ContentStyle>
                                            </dxtc:ASPxPageControl>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                            <dxtc:ASPxPageControl ID="pcNotificacoes_wf" runat="server" ActiveTabIndex="0" ClientInstanceName="pcNotificacoes_wf"
                                                Width="100%">
                                                <TabPages>
                                                    <dxtc:TabPage Name="tcMsgAcao_Cnt" Text="<%$ Resources:traducao, adm_edicaoWorkflows_mensagem_a__o %>">
                                                        <ContentCollection>
                                                            <dxw:ContentControl ID="ContentControl71" runat="server">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <dxe:ASPxLabel ID="lblAssunto1_cnt1" runat="server" ClientInstanceName="lblAssunto1_cnt"
                                                                                    Text="<%$ Resources:traducao, adm_edicaoWorkflows_assunto_ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                <asp:TextBox ID="txtAssunto1_wf" runat="server" onfocus="elEditor = __ta_initialize(this);"
                                                                                                    TabIndex="5" Width="98%"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 60px" valign="top">
                                                                                <dxe:ASPxLabel ID="lblTexto1_cnt1" runat="server" ClientInstanceName="lblTexto1_cnt"
                                                                                    Text="<%$ Resources:traducao, adm_edicaoWorkflows_texto_ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="left" valign="top">
                                                                                <asp:TextBox ID="mmTexto1_wf" runat="server"
                                                                                    Height="65px" onfocus="elEditor = __ta_initialize(this)"
                                                                                    TabIndex="6" TextMode="MultiLine" Width="98%"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                    <dxtc:TabPage Name="tcAcompanhamento_tc" Text="<%$ Resources:traducao, adm_edicaoWorkflows_mensagem_acompanhamento %>">
                                                        <ContentCollection>
                                                            <dxw:ContentControl ID="ContentControl81" runat="server">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <dxe:ASPxLabel ID="lblAssunto2_wf" runat="server" ClientInstanceName="lblAssunto2_wf"
                                                                                    Text="<%$ Resources:traducao, adm_edicaoWorkflows_assunto_ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                <asp:TextBox ID="txtAssunto2_wf" runat="server"
                                                                                                    onfocus="elEditor = __ta_initialize(this);"
                                                                                                    TabIndex="5" Width="98%"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td></td>
                                                                            <td align="right" style="height: 3px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 60px" valign="top">
                                                                                <dxe:ASPxLabel ID="lblTexto2_wf" runat="server" ClientInstanceName="lblTexto2_wf"
                                                                                    Text="<%$ Resources:traducao, adm_edicaoWorkflows_texto_ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td align="left" valign="top">
                                                                                <asp:TextBox ID="mmTexto2_wf" runat="server"
                                                                                    Height="65px" onfocus="elEditor = __ta_initialize(this)" TabIndex="6"
                                                                                    TextMode="MultiLine" Width="98%"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </dxw:ContentControl>
                                                        </ContentCollection>
                                                    </dxtc:TabPage>
                                                </TabPages>
                                            </dxtc:ASPxPageControl>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 60px"></td>
                                                        <td align="left" valign="top">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxe:ASPxImage ID="imgNomeDoProcessoConector1" runat="server" ClientInstanceName="imgNomeDoProcessoConector"
                                                                                Cursor="pointer" ImageUrl="~/imagens/Workflow/NomeProcessoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_fluxo %>">
                                                                                <ClientSideEvents Click="function(s, e) {
	                __ta_insertText(elEditor, &quot; [nomeDoFluxo] &quot;);
                }" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxe:ASPxImage ID="imgDataInicioProcessoConector1" runat="server" ClientInstanceName="imgDataInicioProcessoConector"
                                                                                Cursor="pointer" ImageUrl="~/imagens/Workflow/DataDoInicioProcessoSmall.png"
                                                                                ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto___data_inicio_fluxo %>">
                                                                                <ClientSideEvents Click="function(s, e) {
	                __ta_insertText(elEditor, &quot; [dataInicioFluxo] &quot;);
                }" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxe:ASPxImage ID="imgResponsavelProcessoConector1" runat="server" ClientInstanceName="imgResponsavelProcessoConector"
                                                                                Cursor="pointer" ImageUrl="~/imagens/Workflow/ResponsavelPeloProcessoSmall.png"
                                                                                ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__responsavel_fluxo %>">
                                                                                <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [responsavelFluxo] &quot;);
}" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td align="center" style="width: 35px">&nbsp;<dxe:ASPxImage ID="imgPrazoFinalRespostaConector1" runat="server" ClientInstanceName="imgPrazoFinalRespostaConector"
                                                                            Cursor="pointer" ImageUrl="~/imagens/Workflow/PrazoFinalParaRespostaSmall.png"
                                                                            ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__prazo_final_resposta %>">
                                                                            <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [prazoFinalResposta] &quot;);
}" />
                                                                        </dxe:ASPxImage>
                                                                        </td>
                                                                        <td align="center" style="width: 35px">&nbsp;<dxe:ASPxImage ID="imgResponsavelAcaoConector1" runat="server" ClientInstanceName="imgResponsavelAcaoConector"
                                                                            Cursor="pointer" ImageUrl="~/imagens/Workflow/ResponsavelPeloAcaoSmall.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__responsavel_a__o %>">
                                                                            <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [ResponsavelAcao] &quot;);
}" />
                                                                        </dxe:ASPxImage>
                                                                        </td>
                                                                        <td align="center" style="width: 35px">&nbsp;<dxe:ASPxImage ID="imgDataUltimaAcaoConector1" runat="server" ClientInstanceName="imgDataUltimaAcaoConector"
                                                                            Cursor="pointer" ImageUrl="~/imagens/Workflow/dataUltimaAcao2.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__data_ultima_a__o_ %>">
                                                                            <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [dataUltimaAcao] &quot;);
}" />
                                                                        </dxe:ASPxImage>
                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxe:ASPxImage ID="imgNomeProjetoConector1" runat="server" ClientInstanceName="imgNomeProjetoConector"
                                                                                Cursor="pointer" ImageUrl="~/imagens/Workflow/iconoProjeto.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_projeto_ %>">
                                                                                <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [nomeProjeto] &quot;);
}" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxe:ASPxImage ID="ASPxImage1021" runat="server" ClientInstanceName="imgNomeProjetoConector"
                                                                                Cursor="pointer" ImageUrl="~/imagens/Workflow/nomeSistema.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__nome_do_sistema %>">
                                                                                <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [nomeSistema] &quot;);
}" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxe:ASPxImage ID="ASPxImage1022" runat="server" ClientInstanceName="imgNomeProjetoConector"
                                                                                Cursor="pointer" ImageUrl="~/imagens/Workflow/linkSistema.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto__link_do_sistema %>">
                                                                                <ClientSideEvents Click="function(s, e) {
	__ta_insertText(elEditor, &quot; [linkSistema] &quot;);
}" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                        <td align="center" style="width: 35px"></td>
                                                                        <td align="center" style="width: 35px">
                                                                            <dxe:ASPxImage ID="imgAjudaAutoTextoCnr1" runat="server" ClientInstanceName="imgAjudaAutoTextoCnr1"
                                                                                Cursor="pointer" ImageUrl="~/imagens/Workflow/Help.png" ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_clique_para_ajuda_com_autotextos %>">
                                                                                <ClientSideEvents Click="function(s, e) {
	popupAjudaAutoTexto.Show();
}" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td align="right">
                                                            <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="formulario-botao">
                                                                            <dxe:ASPxButton ID="ASPxButton9" runat="server"
                                                                                TabIndex="6" Text="Ok" Width="90px">
                                                                                <ClientSideEvents Click="function(s, e){
    e.processOnServer = false;
	onOkDivAcaoWfClick(s, e);
}" />
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td class="formulario-botao">
                                                                            <dxe:ASPxButton ID="ASPxButton10" runat="server"
                                                                                TabIndex="7" Text="<%$ Resources:traducao, adm_edicaoWorkflows_cancelar %>" Width="90px">
                                                                                <ClientSideEvents Click="function(s, e) 
{	
    e.processOnServer = false;
	divAcaoWf.Hide();
}" />
                                                                            </dxe:ASPxButton>
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
                        </dxp:PanelContent>
                    </PanelCollection>
                    <ClientSideEvents EndCallback="function(s, e) {
   divAcaoWf.Show();
}"></ClientSideEvents>
                </dxcp:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <div id="divSalvoPublicado" style="left: 39%; top: 55%; position: absolute; padding: 15px; width: 280px; background-color: #EBEBEB; border-style: solid; border-width: 1px; display: none">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" valign="middle">
                    <dxe:ASPxLabel ID="lblDivSalvoPublicado_Titulo" runat="server" ClientInstanceName="lblDivSalvoPublicado_Titulo"
                        Text="..." Font-Bold="True" EnableClientSideAPI="True">
                    </dxe:ASPxLabel>
                    <br />
                    <dxe:ASPxLabel ID="lblDivSalvoPublicado_Acao" runat="server" ClientInstanceName="lblDivSalvoPublicado_Acao"
                        EnableClientSideAPI="True">
                    </dxe:ASPxLabel>
                </td>
                <td align="center" valign="middle">
                    <dxe:ASPxImage ID="ASPxImage6" runat="server" ImageAlign="TextTop" ImageUrl="~/imagens/Workflow/salvarBanco.png">
                    </dxe:ASPxImage>
                </td>
            </tr>
        </table>
    </div>
    <div id="divLoading" style="left: 49%; top: 331px; width: 42px; position: absolute; height: 42px; display: none;">
        <img src="../imagens/Workflow/loading2.gif" alt="" />
    </div>
    <%-- ------[DIV ACÃO --%>
    <dxpc:ASPxPopupControl ID="pcConfiguracaoWf" runat="server" ClientInstanceName="pcConfiguracaoWf"
        ShowOnPageLoad="true" HeaderText="<%$ Resources:traducao, adm_edicaoWorkflows_configura__o %>"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False"
        Width="450px">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_descri__o_da_vers_o_ %>" ClientInstanceName="lblDescricaoVersaoWf"
                                    ID="lblDescricaoVersaoWf">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="60" ClientInstanceName="txtDescricaoVersaoWf"
                                    ID="txtDescricaoVersaoWf">
                                    <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}"></ClientSideEvents>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_observa__es_ %>" ClientInstanceName="lblObservacaoWf"
                                    ID="lblObservacaoWf">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxMemo runat="server" Rows="4" Width="100%" ClientInstanceName="memObservacaoWf"
                                    ID="memObservacaoWf">
                                    <ClientSideEvents KeyPress="function(s, e) {
	tratarSimbolos(s, e);
}"
                                        Init="function(s, e) {
                    try{
                        return setMaxLength(s.GetInputElement(), 500);
                    }
                    catch(e){}
                    }"></ClientSideEvents>
                                </dxe:ASPxMemo>
                                <dxe:ASPxLabel runat="server" Text="0" ClientInstanceName="lblCantCarater"
                                    ForeColor="Silver" ID="lblCantCarater">
                                </dxe:ASPxLabel>
                                <dxe:ASPxLabel runat="server" Text=" de 500" ClientInstanceName="lblDe250"
                                    ForeColor="Silver" ID="lblDe250">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle">
                                <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_dimens_es__rea_desenho %>" Font-Bold="True"
                                    ID="ASPxLabel9">
                                </dxe:ASPxLabel>
                                <!--<%# Resources.traducao.detalhesObjetivoEstrategico_indicador_resultante %> -->
                                <img style="cursor: pointer" title="<%# Resources.traducao.adm_edicaoWorkflows_para_alterar_as_dimens_es_da__rea_de_desenho__informe_novos_valores_nos_campos_abaixo_ %>"
                                    src="../imagens/ajuda.png" alt="<%# Resources.traducao.adm_edicaoWorkflows_dimens_es %>" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_altura_ %>" ClientInstanceName="lblLarguraWf"
                                                    ID="lblLarguraWf">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="70px" ClientInstanceName="txtAlturaWf"
                                                    ID="txtAlturaWf">
                                                    <MaskSettings Mask="&lt;300..10000&gt;" PromptChar=" " IncludeLiterals="None"></MaskSettings>
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td></td>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_largura_ %>" ClientInstanceName="lblAlturaWf"
                                                    ID="lblAlturaWf">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="70px" ClientInstanceName="txtLarguraWf"
                                                    ID="txtLarguraWf">
                                                    <MaskSettings Mask="&lt;300..10000&gt;" PromptChar=" " IncludeLiterals="None"></MaskSettings>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton runat="server" ClientInstanceName="btnAplicarPcConfiguracao" ClientVisible="False"
                                                    Text="Aplicar" Width="90px" TabIndex="2"
                                                    ID="btnAplicarPcConfiguracao" Visible="False">
                                                    <ClientSideEvents Click="function(s, e) {
                    e.processOnServer = false;
                    aplicarConfiguracaoWf();
                    //rdm('divFlash',txtLarguraWf.GetText(), txtAlturaWf.GetText());
                    }"></ClientSideEvents>
                                                    <Paddings Padding="0px"></Paddings>
                                                </dxe:ASPxButton>
                                            </td>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton runat="server" ClientInstanceName="btnOkPcConfiguracao" Text="Ok"
                                                    Width="90px" TabIndex="2" ID="btnOkPcConfiguracao">
                                                    <ClientSideEvents Click="function(s, e) {
                    e.processOnServer = false;
                    aplicarConfiguracaoWf();
                    //rdm('divFlash',txtLarguraWf.GetText(), txtAlturaWf.GetText());
                    pcConfiguracaoWf.Hide();
                    }"></ClientSideEvents>
                                                    <Paddings Padding="0px"></Paddings>
                                                </dxe:ASPxButton>
                                            </td>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelarPcConfiguracao" Text="<%$ Resources:traducao, adm_edicaoWorkflows_cancelar %>"
                                                    Width="90px" TabIndex="3" ID="btnCancelarPcConfiguracao">
                                                    <ClientSideEvents Click="function(s, e) {
                        e.processOnServer = false;
                        pcConfiguracaoWf.Hide();
                    }"></ClientSideEvents>
                                                    <Paddings Padding="0px"></Paddings>
                                                </dxe:ASPxButton>
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
        <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="ASPxPopupControl3" runat="server"
        HeaderText="<%$ Resources:traducao, adm_edicaoWorkflows_autotexto %>" ClientInstanceName="popupAjudaAutoTexto"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="600px">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; text-align: justify; font-family: Verdana; font-size: 8pt">
                                <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_o_recurso_de_autotexto_permite_incluir_informa__es_din_micas_numa_notifica__o__os_autotextos_s_o_substitu_dos_por_informa__es_reais_do_fluxo_e_da_a__o_causadores_da_notifica__o__para_us__lo__posicione_o_cursor_no_campo__assunto__ou__texto____no_local_que_deseja_inserir_a_informa__o_e_clique_sobre_o__cone_do_autotexto_correspondente__segue_breve_explica__o_de_cada_autotexto_ %>" /><br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                <dxe:ASPxImage ID="ASPxImage13" runat="server" Cursor="pointer" ImageUrl="~/imagens/Workflow/NomeProcessoSmall.png"
                                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_auto_texto___nomedofluxo_ %>">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td>
                                                <strong style="text-align: justify">
                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nome_do_fluxo_ %>" /></strong>
                                                <span style="color: #339900; text-align: justify">[nomeDoFluxo]</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: justify; font-family: Verdana; font-size: 8pt">
                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nome_que_o_usu_rio_deu_ao_fluxo_no_momento_de_sua_cria__o__este_autotexto___uma_importante_informa__o_para_quem_vai_receber_a_notifica__o_ %>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                <dxe:ASPxImage ID="ASPxImage14" runat="server" Cursor="pointer" ImageUrl="~/imagens/Workflow/ResponsavelPeloProcessoSmall.png"
                                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_auto_texto____datainiciofluxo__ %>">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td>
                                                <strong style="text-align: justify">
                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_repons_vel_pelo_fluxo_ %>" /></strong> <span style="color: #339900; text-align: justify">[responsavelFluxo]</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: justify; font-family: Verdana; font-size: 8pt">
                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nome_do_usu_rio_criador_do_fluxo_ %>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                <dxe:ASPxImage ID="ASPxImage18" runat="server" Cursor="pointer" ImageUrl="~/imagens/Workflow/DataDoInicioProcessoSmall.png"
                                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_auto_texto___responsavelfluxo__ %>">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td>
                                                <strong style="text-align: justify">
                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_data_de_in_cio_do_fluxo %>" />: </strong><span style="color: #339900; text-align: justify">[dataInicioFluxo]</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: justify; font-family: Verdana; font-size: 8pt">
                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_data_de_cria__o_do_fluxo_ %>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                <dxe:ASPxImage ID="ASPxImage19" runat="server" Cursor="pointer" ImageUrl="~/imagens/Workflow/PrazoFinalParaRespostaSmall.png"
                                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_auto_texto___prazofinalresposta__ %>">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td style="text-align: justify">
                                                <strong>
                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_prazo_final_para_resposta_ %>" /></strong><span style="color: #339900"> [prazoFinalResposta]</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: justify; font-family: Verdana; font-size: 8pt">
                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_data_limite_sugerida_para_resposta___a__o_ %>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                <dxe:ASPxImage ID="ASPxImage20" runat="server" Cursor="pointer" ImageUrl="~/imagens/Workflow/ResponsavelPeloAcaoSmall.png"
                                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_auto_texto___responsavelacao_ %>">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td>
                                                <strong style="text-align: justify">
                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_respons_vel_pela_a__o_ %>" /></strong> <span style="color: #339900; text-align: justify">[responsavelAcao]</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: justify; font-family: Verdana; font-size: 8pt">
                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nome_da_pessoa_que_gerou_a_a__o_ %>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                <dxe:ASPxImage ID="ASPxImage21" runat="server" Cursor="pointer" ImageUrl="~/imagens/Workflow/dataUltimaAcao2.png"
                                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_auto_texto___dataultimaacao__ %>">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td>
                                                <strong style="text-align: justify">
                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_data_da__ltima_a__o_ %>" /></strong> <span style="color: #339900; text-align: justify">[dataUltimaAcao]</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: justify; font-family: Verdana; font-size: 8pt">
                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_data_hora_de_ocorr_ncia_da_a__o_ %>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                <dxe:ASPxImage ID="ASPxImage22" runat="server" Cursor="pointer" ImageUrl="~/imagens/Workflow/iconoProjeto.png"
                                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_auto_texto___nomeprojeto__ %>">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td style="text-align: justify">
                                                <strong>
                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nome_do_projeto_ %>" /></strong><span style="color: #339900">[nomeProjeto]</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: justify; font-family: Verdana; font-size: 8pt">
                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nome_do_projeto_relacionado_ao_fluxo_ %>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                <dxe:ASPxImage ID="ASPxImage23" runat="server" Cursor="pointer" ImageUrl="~/imagens/Workflow/nomeSistema.png"
                                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_auto_texto___nomesistema__ %>">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td style="text-align: justify; font-family: Verdana; font-size: 8pt">
                                                <strong>
                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nome_do_sistema_ %>" /></strong><span style="color: #339900">[nomeSistema]</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: justify">
                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_nome_do_sistema_ %>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" rowspan="2" style="width: 31px" valign="middle">
                                                <dxe:ASPxImage ID="ASPxImage24" runat="server" Cursor="pointer" ImageUrl="~/imagens/Workflow/linkSistema.png"
                                                    ToolTip="<%$ Resources:traducao, adm_edicaoWorkflows_auto_texto___linksistema__ %>">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td style="text-align: justify">
                                                <strong>
                                                    <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_link_do_sistema_ %>" /></strong><span style="color: #339900">[linkSistema]</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: justify; font-family: Verdana; font-size: 8pt">
                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_edicaoWorkflows_endere_o_do_sistema_na_internet_ %>" />
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
    </dxpc:ASPxPopupControl>
    <script type="text/javascript">
        renderizaFlash();
        // __wf_chartObj -> variável definida no script WorkflowCharts.js
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 195;
        __wf_chartObj = new FusionCharts("../flashs/DragNode.swf", "nodeChart", "<%=larguraGrafico %>", sHeight, "0", "1");
        __wf_chartObj.setTransparent(true);
        setXmlComponente(); // atualiza o xml objeto chart
        __wf_chartObj.render("divFlash");

        inicializarPropiedadesWF();

        //fazo uso da libreria DOM Drag & Drop script (http://www.dynamicdrive.com/dynamicindex11/domdrag/)
        //en ela se ten qeu definir qual seria o objeto a movimentarse. ('id' da div).
        //e tamben o componente aonde recivira o evento do mouse (ex. 'id' do um componente dentro da div, nesse exemplo a celda 'handler'.
        //	            var HandleTimer = document.getElementById("HandleDescricao");
        //	            var RootTimer   = document.getElementById("divDescricao");
        //	            Drag.init(HandleTimer, RootTimer);

        //pcConfiguracaoWf.Show();
    </script>
    <va:uc_crud_caminhoCondicionalptBR runat="server" ID="uc_crud_caminhoCondicionalptBR" />
    <va:uc_crud_caminhoCondicionalenUS runat="server" ID="uc_crud_caminhoCondicionalenUS" />
</asp:Content>
