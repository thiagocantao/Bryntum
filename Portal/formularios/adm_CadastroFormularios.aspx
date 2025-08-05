<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="adm_CadastroFormularios.aspx.cs" Inherits="Formularios_adm_CadastroFormularios"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<%@ Register Src="~/formularios/UC_Formularios/uc_cfg_validacaoCampo.ascx" TagPrefix="UC" TagName="uc_cfg_validacaoCampo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;display:none">
        <tr>
            <td align="left" style="height: 26px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="width: 1px; height: 19px">
                            <span id="Span2" runat="server"></span>
                        </td>
                        <td align="left" valign="middle" style="padding-left: 10px">
                            <dxe:ASPxLabel ID="lblTituloTela" runat="server" Font-Bold="True"
                                Text="Cadastro de Formulários" ClientInstanceName="lblTituloTela">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td id="ConteudoPrincipal">
                <dxwgv:ASPxGridView ID="gvFormularios" runat="server" AutoGenerateColumns="False"
                    KeyFieldName="CodigoModeloFormulario" OnRowInserting="gvFormularios_RowInserting"
                    OnRowUpdating="gvFormularios_RowUpdating" OnDetailRowExpandedChanged="gvFormularios_DetailRowExpandedChanged"
                    OnStartRowEditing="gvFormularios_StartRowEditing" OnRowValidating="gvFormularios_RowValidating"
                    OnCommandButtonInitialize="gvFormularios_CommandButtonInitialize" OnCustomButtonCallback="gvFormularios_CustomButtonCallback"
                    OnCellEditorInitialize="gvFormularios_CellEditorInitialize" OnRowDeleting="gvFormularios_RowDeleting"
                    ClientInstanceName="gvFormularios" Width="100%"
                    OnDetailRowGetButtonVisibility="gvFormularios_DetailRowGetButtonVisibility" OnHtmlEditFormCreated="gvFormularios_HtmlEditFormCreated"
                    KeyboardSupport="True" OnAfterPerformCallback="gvFormularios_AfterPerformCallback"
                    OnCustomErrorText="gvFormularios_CustomErrorText" OnCustomButtonInitialize="gvFormularios_CustomButtonInitialize">
                    <Styles>
                        <AlternatingRow Enabled="True">
                        </AlternatingRow>
                    </Styles>
                    <Templates>
                        <DetailRow>
                            <dxe:ASPxButton ID="btnNovoCampo" OnClick="btnNovoCampo_Click" runat="server"
                                Text="Novo Campo" Visible="False">
                            </dxe:ASPxButton>
                            <dxwgv:ASPxGridView ID="gvCampos" runat="server" Width="100%" ClientInstanceName="gvCampos"
                                OnRowDeleting="gvCampos_RowDeleting" OnCellEditorInitialize="gvCampos_CellEditorInitialize"
                                OnCommandButtonInitialize="gvCampos_CommandButtonInitialize" OnRowValidating="gvCampos_RowValidating"
                                OnDetailRowExpandedChanged="gvCampos_DetailRowExpandedChanged" OnRowUpdating="gvCampos_RowUpdating"
                                OnRowInserting="gvCampos_RowInserting" KeyFieldName="CodigoCampo" AutoGenerateColumns="False"
                                OnHtmlEditFormCreated="gvCampos_HtmlEditFormCreated" OnBeforePerformDataSelect="gvCampos_BeforePerformDataSelect"
                                OnAfterPerformCallback="gvCampos_AfterPerformCallback" OnInit="gvCampos_Init"
                                OnLoad="gvCampos_Load">
                                <Templates>
                                    <DetailRow>
                                        <dxhf:ASPxHiddenField ID="hfControle" runat="server" ClientInstanceName="hfControle">
                                        </dxhf:ASPxHiddenField>
                                        <dxp:ASPxPanel ID="dvVAR" runat="server" Width="100%" Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 214px; height: 20px;" align="left">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_quantidade_m_xima_de_caracteres_ %>"
                                                                        ID="ASPxLabel1">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="left" style="width: 10px; height: 20px;"></td>
                                                                <td align="left" style="width: 291px; height: 20px;" valign="middle">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel118" runat="server"
                                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_m_scara_ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxImage ID="imgAjudaMascara" runat="server" ClientInstanceName="imgAjudaMascara"
                                                                                    ImageUrl="~/imagens/ajuda.png" ToolTip="" Width="16px">
                                                                                    <ClientSideEvents Init="function(s, e) {
	s.mainElement.title = gvFormularios.cp_Ajuda; 
}" />
                                                                                </dxe:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td align="left" style="height: 20px;" valign="middle">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxtv:ASPxLabel ID="ASPxLabel122" runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_valor_inicial_ %>">
                                                                                </dxtv:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxtv:ASPxImage ID="imgAjudaValorInicial" runat="server" ClientInstanceName="imgAjudaValorInicial" ImageUrl="~/imagens/ajuda.png" ToolTip="<%$ Resources:traducao, adm_CadastroFormularios_informe_aqui_o_valor_inicial__default__a_ser_atribu_do_ao_campo_no_momento_da_cria__o_do_formul_rio %>" Width="16px">
                                                                                </dxtv:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 214px" align="left">
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txt_Var_tamanho"
                                                                        ID="txt_Var_tamanho">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="left">&nbsp;
                                                                </td>
                                                                <td align="left" style="width: 291px; padding-right: 10px;">
                                                                    <dxe:ASPxTextBox ID="txt_Var_mascara" runat="server" ClientInstanceName="txt_Var_mascara"
                                                                        Width="100%">
                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                            <RegularExpression ErrorText="<%$ Resources:traducao, adm_CadastroFormularios_m_scara_inv_lida %>" ValidationExpression="([H]|[h]|[@]|[09\(\)\-\.\,/ ]*)" />
                                                                        </ValidationSettings>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <dxtv:ASPxTextBox ID="txt_Var_padrao" runat="server" ClientInstanceName="txt_Var_padrao" Width="100%">
                                                                        <ClientSideEvents Validation="function(s, e) {
	if(txt_Var_tamanho.GetValue() != null &amp;&amp; parseInt(txt_Var_tamanho.GetValue()) &lt; s.GetText().length)
	{
		e.isValid = false;
		e.errorText = traducao.adm_CadastroFormularios_a_quantidade_de_caracteres_do_campo__quot_padr_o_quot__n_o_pode_ser_maior_que_o_valor_determinado_no_campo__quot_tamanho_quot_;
	}
}" />
                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                                                                            <RegularExpression ErrorText="" />
                                                                        </ValidationSettings>
                                                                    </dxtv:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 214px; height: 10px" align="left"></td>
                                                                <td align="left" style="width: 10px; height: 10px">&nbsp;
                                                                </td>
                                                                <td align="left" style="width: 291px; height: 10px">&nbsp;
                                                                </td>
                                                                <td align="left" style="height: 10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 214px" align="right">&nbsp;
                                                                </td>
                                                                <td align="right">&nbsp;
                                                                </td>
                                                                <td align="right" style="width: 291px;">&nbsp;</td>
                                                                <td align="right">
                                                                    <dxtv:ASPxButton ID="btnSalvar_VAR" runat="server" OnClick="btnSalvar_VAR_Click" Text="<%$ Resources:traducao, adm_CadastroFormularios_salvar %>" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('VAR');
	//gvCampos.PerformCallback();
}" />
                                                                    </dxtv:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel ID="dvTXT" runat="server" Width="100%" Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tr>
                                                            <td style="width: 226px" valign="top">
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 215px">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_formata__o_a_ser_utilizada_ %>">
                                                                                </dxtv:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="height: 10px"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <dxtv:ASPxRadioButton ID="rb_TXT_FormatacaoSimples" runat="server" ClientInstanceName="rb_TXT_FormatacaoSimples" GroupName="Formato" Text="<%$ Resources:traducao, adm_CadastroFormularios_formata__o_do_texto %>" Width="100%">
                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	txt_TXT_Linhas.SetEnabled(!s.GetChecked());
    txt_TXT_Tamanho.SetEnabled(!s.GetChecked());
    if(txt_TXT_Tamanho.GetEnabled() == false)
	{
		txt_TXT_Tamanho.SetText(&quot;&quot;);
        txt_TXT_Linhas.SetText(&quot;&quot;);
	}
}"
                                                                                        Init="function(s, e) {
	txt_TXT_Linhas.SetEnabled(!s.GetChecked());
    txt_TXT_Tamanho.SetEnabled(!s.GetChecked());
    if(txt_TXT_Tamanho.GetEnabled() == false)
	{
		txt_TXT_Tamanho.SetText(&quot;&quot;);
        txt_TXT_Linhas.SetText(&quot;&quot;);
	}
}" />
                                                                                </dxtv:ASPxRadioButton>
                                                                                <dxtv:ASPxRadioButton ID="rb_TXT_FormatacaoAvancada" runat="server" ClientInstanceName="rb_TXT_FormatacaoAvancada" GroupName="Formato" Text="<%$ Resources:traducao, adm_CadastroFormularios_avan_ada__inclus_o_de_imagens_ %>" Visible="False" Width="100%">
                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	txt_TXT_Linhas.SetEnabled(!s.GetChecked());
    txt_TXT_Tamanho.SetEnabled(!s.GetChecked());
    if(txt_TXT_Tamanho.GetEnabled() == false)
	{
		txt_TXT_Tamanho.SetText(&quot;&quot;);
        txt_TXT_Linhas.SetText(&quot;&quot;);
	}
}"
                                                                                        Init="function(s, e) {
	txt_TXT_Linhas.SetEnabled(!s.GetChecked());
    txt_TXT_Tamanho.SetEnabled(!s.GetChecked());
    if(txt_TXT_Tamanho.GetEnabled() == false)
	{
		txt_TXT_Tamanho.SetText(&quot;&quot;);
        txt_TXT_Linhas.SetText(&quot;&quot;);
	}
}" />
                                                                                </dxtv:ASPxRadioButton>
                                                                                <dxtv:ASPxRadioButton ID="rb_TXT_SemFormatacao" runat="server" ClientInstanceName="rb_TXT_SemFormatacao" GroupName="Formato" Text="<%$ Resources:traducao, adm_CadastroFormularios_n_o_utilizar_formata__o %>" Width="100%">
                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
    txt_TXT_Linhas.SetEnabled(s.GetChecked());
    txt_TXT_Tamanho.SetEnabled(s.GetChecked());
    if(txt_TXT_Tamanho.GetEnabled() == false)
	{
		txt_TXT_Tamanho.SetText(&quot;&quot;);
        txt_TXT_Linhas.SetText(&quot;&quot;);
	}
}"
                                                                                        Init="function(s, e) {
	txt_TXT_Linhas.SetEnabled(s.GetChecked());
    txt_TXT_Tamanho.SetEnabled(s.GetChecked());
    if(txt_TXT_Tamanho.GetEnabled() == false)
	{
		txt_TXT_Tamanho.SetText(&quot;&quot;);
        txt_TXT_Linhas.SetText(&quot;&quot;);
	}
}" />
                                                                                </dxtv:ASPxRadioButton>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <table id="tabFormataTexto" border="0" cellpadding="0" cellspacing="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_quantidade_de_linhas__at__10__ %>">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxTextBox ID="txt_TXT_Linhas" runat="server" ClientInstanceName="txt_TXT_Linhas" Width="100%">
                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxTextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 10px"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_quantidade_de_caracteres_at__4000__ %>">
                                                                                                </dxtv:ASPxLabel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dxtv:ASPxTextBox ID="txt_TXT_tamanho" runat="server" ClientInstanceName="txt_TXT_Tamanho" Width="100%">

                                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                                    </DisabledStyle>
                                                                                                </dxtv:ASPxTextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <table cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <dxtv:ASPxLabel ID="ASPxLabel123" runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_valor_inicial_ %>">
                                                                                        </dxtv:ASPxLabel>
                                                                                    </td>
                                                                                    <td>
                                                                                        <dxtv:ASPxImage ID="imgAjudaValorInicial0" runat="server" ImageUrl="~/imagens/ajuda.png" ToolTip="<%$ Resources:traducao, adm_CadastroFormularios_informe_aqui_o_valor_inicial__default__a_ser_atribu_do_ao_campo_no_momento_da_cria__o_do_formul_rio %>" Width="16px">
                                                                                            <ClientSideEvents Init="function(s, e) {
	s.mainElement.title = gvFormularios.cp_Ajuda;
}" />
                                                                                        </dxtv:ASPxImage>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxtv:ASPxMemo ID="txt_TXT_padrao" runat="server" ClientInstanceName="txt_TXT_padrao" Height="152px" Width="100%">
                                                                                <ClientSideEvents Validation="function(s, e) {
	if(txt_TXT_Tamanho.GetValue() != null &amp;&amp; parseInt(txt_TXT_Tamanho.GetValue()) &lt; s.GetText().length)
	{
		e.isValid = false;
		e.errorText = traducao.adm_CadastroFormularios_a_quantidade_de_caracteres_do_campo__quot_padr_o_quot__n_o_pode_ser_maior_que_o_valor_determinado_no_campo__quot_tamanho_quot_;
	}

}" />
                                                                            </dxtv:ASPxMemo>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 226px">&nbsp;</td>
                                                            <td align="right">
                                                                <dxtv:ASPxButton ID="btnSalvar_TXT" runat="server" OnClick="btnSalvar_VAR_Click" Text="<%$ Resources:traducao, adm_CadastroFormularios_salvar %>" Width="90px">
                                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = validaCamposTexto();
   obtemValoresClientes('TXT');
}" />
                                                                </dxtv:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel ID="dvNUM" runat="server" Width="100%" Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="padding-right: 10px;" width="110px">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_valor_m_nimo_ %>"
                                                                        ID="ASPxLabel4">
                                                                    </dxe:ASPxLabel>
                                                                    <dxcp:ASPxSpinEdit runat="server" Width="100%" ClientInstanceName="txt_NUM_Minimo"
                                                                        ID="txt_NUM_Minimo">
                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                        </SpinButtons>
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        </ValidationSettings>
                                                                    </dxcp:ASPxSpinEdit>
                                                                </td>
                                                                <td style="padding-right: 10px; width: 109px;">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_valor_m_ximo_ %>"
                                                                        ID="ASPxLabel5">
                                                                    </dxe:ASPxLabel>
                                                                    <dxcp:ASPxSpinEdit runat="server" Width="100%" ClientInstanceName="txt_NUM_Maximo"
                                                                        ID="txt_NUM_Maximo">
                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                        </SpinButtons>
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        </ValidationSettings>
                                                                    </dxcp:ASPxSpinEdit>
                                                                </td>
                                                                <td width="120px">
                                                                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server"
                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_precis_o_decimal_ %>">
                                                                    </dxe:ASPxLabel>
                                                                    <dxe:ASPxComboBox ID="ddl_NUM_Precisao" runat="server" ClientInstanceName="ddl_NUM_Precisao"
                                                                        SelectedIndex="0" Width="100%">
                                                                        <Items>
                                                                            <dxe:ListEditItem Selected="True" Text="0" Value="0" />
                                                                            <dxe:ListEditItem Text="1" Value="1" />
                                                                            <dxe:ListEditItem Text="2" Value="2" />
                                                                            <dxe:ListEditItem Text="3" Value="3" />
                                                                            <dxe:ListEditItem Text="4" Value="4" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" width="110px"></td>
                                                                <td style="width: 109px"></td>
                                                                <td width="120px">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="110px">
                                                                    <dxe:ASPxLabel ID="ASPxLabel108" runat="server"
                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_formato_ %>">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 109px">
                                                                    <dxe:ASPxLabel ID="ASPxLabel109" runat="server"
                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_agrega__o_ %>">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td width="120px">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dxtv:ASPxLabel ID="ASPxLabel124" runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_valor_inicial_ %>">
                                                                                </dxtv:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxtv:ASPxImage ID="imgAjudaValorInicial1" runat="server" ImageUrl="~/imagens/ajuda.png" ToolTip="<%$ Resources:traducao, adm_CadastroFormularios_informe_aqui_o_valor_inicial__default__a_ser_atribu_do_ao_campo_no_momento_da_cria__o_do_formul_rio %>" Width="16px">
                                                                                </dxtv:ASPxImage>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-right: 10px;" width="110px">
                                                                    <dxe:ASPxComboBox ID="ddl_NUM_Formato" runat="server" ClientInstanceName="ddl_NUM_Formato"
                                                                        SelectedIndex="0" Width="100%">
                                                                        <Items>
                                                                            <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, adm_CadastroFormularios_n_mero %>" Value="N" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_moeda %>" Value="M" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_percentual %>" Value="P" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td style="padding-right: 10px; width: 109px;">
                                                                    <dxe:ASPxComboBox ID="ddl_NUM_Agregacao" runat="server" ClientInstanceName="ddl_NUM_Agregacao"
                                                                        SelectedIndex="0" Width="100%">
                                                                        <Items>
                                                                            <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, adm_CadastroFormularios_nenhuma %>" Value="" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_soma %>" Value="SOM" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_m_dia %>" Value="MED" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_m_ximo %>" Value="MAX" />
                                                                            <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_m_nimo %>" Value="MIN" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td width="120px">
                                                                    <dxcp:ASPxSpinEdit ID="txt_NUM_padrao" runat="server" ClientInstanceName="txt_NUM_padrao" Width="100%">
                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                        </SpinButtons>
                                                                        <ClientSideEvents Validation="function(s, e) {
	if(s.GetValue() != null &amp;&amp; s.GetText() != '')
	{
	        if(txt_NUM_Minimo.GetValue() != null &amp;&amp; s.GetValue() &lt; txt_NUM_Minimo.GetValue())
	        {
		e.isValid = false;
		e.errorText = traducao.adm_CadastroFormularios_o_valor_inicial_n_o_pode_ser_menor_que_o_valor_m_nimo_permitido_;
	         }else 
	        { 
                             if(txt_NUM_Maximo.GetValue() != null &amp;&amp; s.GetValue() &gt; txt_NUM_Maximo.GetValue())
                             {                                                                                                                                              
		    e.isValid = false;
		    e.errorText = traducao.adm_CadastroFormularios_o_valor_inicial_n_o_pode_ser_maior_que_o_valor_m_ximo_permitido_;
                              }
	        }
	}
}" />
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        </ValidationSettings>
                                                                    </dxcp:ASPxSpinEdit>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" width="110px"></td>
                                                                <td style="width: 109px"></td>
                                                                <td width="120px">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="110px"></td>
                                                                <td align="right" style="width: 109px">&nbsp;</td>
                                                                <td align="right" width="120px">
                                                                    <dxtv:ASPxButton ID="btnSalvar_NUM" runat="server" OnClick="btnSalvar_VAR_Click" Text="<%$ Resources:traducao, adm_CadastroFormularios_salvar %>" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('NUM');
}" />
                                                                    </dxtv:ASPxButton>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    &nbsp; &nbsp; &nbsp;
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel ID="dvDAT" runat="server" Width="100%" Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table style="width: 406px" cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_incluir_hora_ %>"
                                                                        ID="ASPxLabel9">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 50px">
                                                                                    <dxe:ASPxRadioButton runat="server" GroupName="Hora" Text="<%$ Resources:traducao, adm_CadastroFormularios_sim %>" ClientInstanceName="rb_DAT_Sim"
                                                                                        ID="rb_DAT_Sim">
                                                                                    </dxe:ASPxRadioButton>
                                                                                </td>
                                                                                <td style="width: 50px">
                                                                                    <dxe:ASPxRadioButton runat="server" GroupName="Hora" Text="<%$ Resources:traducao, adm_CadastroFormularios_n_o %>" ClientInstanceName="rb_DAT_Nao"
                                                                                        ID="rb_DAT_Nao">
                                                                                    </dxe:ASPxRadioButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="right"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_valor_inicial_ %>"
                                                                        ID="ASPxLabel24">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="width: 190px">
                                                                                    <dxe:ASPxRadioButton runat="server" GroupName="Inicial" Text="<%$ Resources:traducao, adm_CadastroFormularios_em_branco %>" ClientInstanceName="rb_DAT_Branco"
                                                                                        ID="rb_DAT_Branco">
                                                                                    </dxe:ASPxRadioButton>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxRadioButton runat="server" GroupName="Inicial" Text="<%$ Resources:traducao, adm_CadastroFormularios_data_hora_atual %>" ClientInstanceName="rb_DAT_Atual"
                                                                                        ID="rb_DAT_Atual">
                                                                                        <ClientSideEvents Validation="function(s, e) {
	    e.isValid = true;
    }" />
                                                                                    </dxe:ASPxRadioButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="right"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 19px" align="right">
                                                                    <dxe:ASPxButton runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_salvar %>" Width="90px" ID="btnSalvar_DAT"
                                                                        OnClick="btnSalvar_VAR_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('DAT');
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel ID="dvLST" runat="server" Width="100%" Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table style="width: 406px" cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_lista_de_opera__es_ %>"
                                                                        ID="ASPxLabel11">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxMemo runat="server" Height="71px" Width="100%" ClientInstanceName="txt_LST_Opcoes"
                                                                        ID="txt_LST_Opcoes">
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="left"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                        <tr>
                                                                            <td style="width: 190px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel12" runat="server"
                                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_apresentar_utilizando_ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="lbl_LST_tamanhoListaSuspensa" runat="server" ClientInstanceName="lbl_LST_tamanhoListaSuspensa"
                                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_quantidade_m_xima_de_caracteres_ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 190px">
                                                                                <dxe:ASPxRadioButton ID="rb_LST_Combo" runat="server" ClientInstanceName="rb_LST_Combo"
                                                                                    GroupName="tipoLista" Text="<%$ Resources:traducao, adm_CadastroFormularios_lista_suspensa %>">
                                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	lbl_LST_tamanhoListaSuspensa.SetVisible(s.GetChecked());
	txt_LST_tamanho.SetVisible(s.GetChecked());
}"
                                                                                        Init="function(s, e) {
	lbl_LST_tamanhoListaSuspensa.SetVisible(s.GetChecked());
	txt_LST_tamanho.SetVisible(s.GetChecked());
}" />
                                                                                </dxe:ASPxRadioButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_LST_tamanho" runat="server" ClientInstanceName="txt_LST_tamanho"
                                                                                    Width="100%">
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 190px">
                                                                                <dxe:ASPxRadioButton ID="rb_LST_Radio" runat="server" ClientInstanceName="rb_LST_Radio"
                                                                                    GroupName="tipoLista" Text="<%$ Resources:traducao, adm_CadastroFormularios_op__es_sele__o_simples %>">
                                                                                </dxe:ASPxRadioButton>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxtv:ASPxLabel ID="ASPxLabel125" runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_valor_inicial_ %>">
                                                                                            </dxtv:ASPxLabel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxtv:ASPxImage ID="imgAjudaValorInicial2" runat="server" ImageUrl="~/imagens/ajuda.png" ToolTip="<%$ Resources:traducao, adm_CadastroFormularios_informe_aqui_o_valor_inicial__default__a_ser_atribu_do_ao_campo_no_momento_da_cria__o_do_formul_rio %>" Width="16px">
                                                                                            </dxtv:ASPxImage>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 190px">
                                                                                <dxe:ASPxRadioButton ID="rb_LST_Check" runat="server" ClientInstanceName="rb_LST_Check"
                                                                                    GroupName="tipoLista" Text="<%$ Resources:traducao, adm_CadastroFormularios_op__es_sele__o_m_ltipla %>">
                                                                                </dxe:ASPxRadioButton>
                                                                            </td>
                                                                            <td>
                                                                                <dxtv:ASPxTextBox ID="txt_LST_padrao" runat="server" ClientInstanceName="txt_LST_padrao" Width="100%">
                                                                                    <ClientSideEvents Validation="function(s, e) {
	
	if(s.GetValue() != null &amp;&amp; txt_LST_Opcoes.GetValue().split('\n').indexOf(s.GetValue()) == -1)
	{
		e.isValid = false;
		e.errorText = traducao.adm_CadastroFormularios_o_valor_inicial_deve_conter_na_lista_;
	}
}" />
                                                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                                        <RegularExpression ErrorText="" />
                                                                                    </ValidationSettings>
                                                                                </dxtv:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">*
                                                                                <asp:Literal runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_em_subformul_rios__as_op__es_sele__o_simples_e_sele__o_m_ltipla_ser_o_tratadas_como_lista_suspensa_ %>" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="left"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 19px" align="right">
                                                                    <dxe:ASPxButton runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_salvar %>" Width="90px" ID="btnSalvar_LST"
                                                                        OnClick="btnSalvar_VAR_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('LST');
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel ID="dvBOL" runat="server" Width="100%" Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table style="width: 320px" cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_situa__o_verdadeiro %>"
                                                                        ID="ASPxLabel14">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150px">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_texto %>" ID="ASPxLabel13">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td></td>
                                                                <td style="width: 150px">&nbsp;<dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_valor %>"
                                                                    ID="ASPxLabel18">
                                                                </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150px">
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txt_BOL_TextoVerdadeiro"
                                                                        ID="txt_BOL_TextoVerdadeiro">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td></td>
                                                                <td style="width: 150px">
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txt_BOL_ValorVerdadeiro"
                                                                        ID="txt_BOL_ValorVerdadeiro">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="width: 320px" cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_situa__o_falso %>"
                                                                        ID="ASPxLabel15">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150px">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_texto %>" ID="ASPxLabel16">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td></td>
                                                                <td style="width: 150px">&nbsp;<dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_valor %>"
                                                                    ID="ASPxLabel17">
                                                                </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150px">
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txt_BOL_TextoFalso"
                                                                        ID="txt_BOL_TextoFalso">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td></td>
                                                                <td style="width: 150px">
                                                                    <dxe:ASPxTextBox runat="server" Width="100%" ClientInstanceName="txt_BOL_ValorFalso"
                                                                        ID="txt_BOL_ValorFalso">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150px; height: 10px"></td>
                                                                <td></td>
                                                                <td style="width: 150px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150px"></td>
                                                                <td></td>
                                                                <td style="width: 150px" align="right">
                                                                    <dxe:ASPxButton runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_salvar %>" Width="90px" ID="btnSalvar_BOL"
                                                                        OnClick="btnSalvar_VAR_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('BOL');
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel ID="dvSUB" runat="server" Width="100%" Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_nome_do_subformul_rio_ %>"
                                                                        Font-Overline="False" ID="ASPxLabel19">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    
                                                                   <!-- <dxe:ASPxComboBox
                                                                                        DropDownStyle="DropDown" EnableCallbackMode="True"  IncrementalFilteringMode="Contains"
                                                                                        OnItemRequestedByValue="ddlGerenteProjeto_ItemRequestedByValue"
                                                                                        OnItemsRequestedByFilterCondition="ddlGerenteProjeto_ItemsRequestedByFilterCondition"
                                                                                        TextField="NomeUsuario" ValueField="CodigoUsuario" CallbackPageSize="80" DropDownRows="10" DropDownHeight="150px">
                                                                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                        </DisabledStyle>
                                                                                    </dxe:ASPxComboBox> -->
                                                                    
                                                                    <dxe:ASPxComboBox runat="server" ValueType="System.Int32" TextField="nomeFormulario" 
                                                                        ValueField="codigoModeloFormulario" Width="320px" ClientInstanceName="ddl_SUB_Formulario"
                                                                        DropDownStyle="DropDown" EnableCallbackMode="True"  IncrementalFilteringMode="Contains"
                                                                        DropDownRows="10" DropDownHeight="150px"
                                                                        Font-Overline="False" ID="ddl_SUB_Formulario" OnItemsRequestedByFilterCondition="ddl_SUB_Formulario_ItemsRequestedByFilterCondition" OnItemRequestedByValue="ddl_SUB_Formulario_ItemRequestedByValue">
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="right"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 19px" align="right">
                                                                    <dxe:ASPxButton runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_salvar %>" Width="90px" ID="btnSalvar_SUB"
                                                                        OnClick="btnSalvar_VAR_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('SUB');
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel ID="dvCPD" runat="server" Width="100%" Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="width: 415px">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_nome_do_campo_pr__definido_ %>"
                                                                        Font-Overline="False" ID="ASPxLabel25">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <dxe:ASPxComboBox runat="server" ValueType="System.Int32" TextField="Descricao" ValueField="Codigo"
                                                                        Width="100%" ClientInstanceName="ddl_CPD_CampoPre" Font-Overline="False"
                                                                        ID="ddl_CPD_CampoPre">
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="left">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="left">
                                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                        <tr>
                                                                            <td style="width: 181px">
                                                                                <dxe:ASPxLabel ID="ASPxLabel113" runat="server"
                                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_quantidade_de_linhas__at__10__ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="lbl_CPD_tamanho" runat="server" ClientInstanceName="lbl_CPD_tamanho"
                                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_quantidade_m_xima_de_caracteres_ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 181px; padding-right: 10px">
                                                                                <dxe:ASPxTextBox ID="txt_CPD_Linhas" runat="server" ClientInstanceName="txt_CPD_Linhas"
                                                                                    Text="1" Width="100%">
                                                                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                                                    </DisabledStyle>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_CPD_tamanho" runat="server" ClientInstanceName="txt_CPD_tamanho"
                                                                                    Width="100%">
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="height: 10px">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="height: 10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="height: 19px">
                                                                    <dxe:ASPxButton ID="btnSalvar_CPD" runat="server" OnClick="btnSalvar_VAR_Click"
                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_salvar %>" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('CPD');
}" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel ID="dvLOO" runat="server" Width="100%" Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_nome_da_lista_pr__definida_ %>"
                                                                        Font-Overline="False" ID="ASPxLabel26">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel ID="lbl_LOO_tamanho" runat="server" ClientInstanceName="lbl_LOO_tamanho"
                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_quantidade_m_xima_de_caracteres_ %>">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel ID="lbl_LOO_ApresentarComo" runat="server" ClientInstanceName="lbl_LOO_ApresentarComo"
                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_apresentar_como_ %>">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="padding-right: 10px">
                                                                 <dxe:ASPxComboBox runat="server" ValueType="System.Int32" TextField="Descricao" 
                                                                        ValueField="Codigo" Width="320px" ClientInstanceName="ddl_LOO_ListaPre"
                                                                        DropDownStyle="DropDown" EnableCallbackMode="True"  IncrementalFilteringMode="Contains"
                                                                        DropDownRows="10" DropDownHeight="150px"
                                                                        Font-Overline="False" ID="ddl_LOO_ListaPre" OnItemsRequestedByFilterCondition="ddl_LOO_ListaPre_ItemsRequestedByFilterCondition" OnItemRequestedByValue="ddl_LOO_ListaPre_ItemRequestedByValue">
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td align="left">
                                                                    <dxe:ASPxTextBox ID="txt_LOO_tamanho" runat="server" ClientInstanceName="txt_LOO_tamanho"
                                                                        Width="210px">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                                <td>&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <dxe:ASPxRadioButton ID="rb_LOO_Combo" runat="server" ClientInstanceName="rb_LOO_Combo"
                                                                        GroupName="tipoListaLOO" Text="<%$ Resources:traducao, adm_CadastroFormularios_lista_suspensa__combo_box_ %>">
                                                                    </dxe:ASPxRadioButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="left"></td>
                                                                <td align="left" style="height: 10px">&nbsp;
                                                                </td>
                                                                <td>&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left" style="height: 10px">
                                                                    <dxe:ASPxRadioButton ID="rb_LOO_Lov" runat="server" ClientInstanceName="rb_LOO_Lov"
                                                                        GroupName="tipoListaLOO" Text="<%$ Resources:traducao, adm_CadastroFormularios_sele__o_de_valores__l_o_v__ %>">
                                                                    </dxe:ASPxRadioButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 19px" align="right">&nbsp;
                                                                </td>
                                                                <td align="right" style="height: 19px">
                                                                    <dxe:ASPxButton ID="btnSalvar_LOO" runat="server" OnClick="btnSalvar_VAR_Click"
                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_salvar %>" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('LOO');
}" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td align="right" style="height: 19px">&nbsp;
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel ID="dvREF" runat="server" Width="100%" Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel runat="server" Text="<%$ Resources:traducao, adm_CadastroFormularios_nome_do_formul_rio_de_refer_ncia_ %>"
                                                                        Font-Overline="False" ID="ASPxLabel22">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxComboBox runat="server" ValueType="System.Int32" TextField="nomeFormulario"
                                                                        ValueField="codigoModeloFormulario" Width="450px" ClientInstanceName="ddl_REF_ModeloFormulario"
                                                                        Font-Overline="False" ID="ddl_REF_ModeloFormulario">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ddl_REF_CampoFormulario.PerformCallback(s.GetValue());
}" />
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="left">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 10px">
                                                                    <dxe:ASPxLabel ID="ASPxLabel114" runat="server" Font-Overline="False"
                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_nome_do_campo_de_refer_ncia_ %>">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 10px">
                                                                    <dxe:ASPxComboBox ID="ddl_REF_CampoFormulario" runat="server" ClientInstanceName="ddl_REF_CampoFormulario"
                                                                        Font-Overline="False" TextField="nomeCampo"
                                                                        ValueField="codigoCampo" ValueType="System.Int32" Width="450px" OnCallback="ddl_REF_CampoFormulario_Callback">
                                                                        <ClientSideEvents EndCallback="function(s, e) {
	//s.SetValue(s.cp_Valor);
}" />
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 10px">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 10px">
                                                                    <table cellpadding="0" cellspacing="0" class="headerGrid">
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel115" runat="server"
                                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_somente_leitura_ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel117" runat="server"
                                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_t_tulo_externo_ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxLabel ID="ASPxLabel116" runat="server"
                                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_titulo_interno_ %>">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="ddl_REF_SomenteLeitura" runat="server" ClientInstanceName="ddl_REF_SomenteLeitura"
                                                                                    SelectedIndex="1" Width="100px">
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_sim %>" Value="S" />
                                                                                        <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, adm_CadastroFormularios_n_o %>" Value="N" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="ddl_REF_TituloExterno" runat="server" ClientInstanceName="ddl_REF_TituloExterno"
                                                                                    SelectedIndex="1" Width="100px">
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_sim %>" Value="S" />
                                                                                        <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, adm_CadastroFormularios_n_o %>" Value="N" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxComboBox ID="ddl_REF_TituloInterno" runat="server" ClientInstanceName="ddl_REF_TituloInterno"
                                                                                    SelectedIndex="1" Width="100px">
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_sim %>" Value="S" />
                                                                                        <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, adm_CadastroFormularios_n_o %>" Value="N" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 10px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 19px" align="right">
                                                                    <dxe:ASPxButton runat="server" Text="Salvar" Width="90px" ID="btnSalvar_REF"
                                                                        OnClick="btnSalvar_VAR_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('REF');
}"></ClientSideEvents>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel ID="dvLNP" runat="server" Visible="False" Width="600px">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 25px">
                                                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/imagens/alertAmarelo.png">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel119" runat="server" Font-Size="10pt"
                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_esta_op__o_estar__dispon_vel_somente_para_formul_rios_de_projetos_ %>">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel ID="dvCAL" runat="server" Width="100%" ClientInstanceName="dvCAL"
                                            Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table style="width: 95%" cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 400px" valign="top">
                                                                    <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvd_CAL_CamposCalculaveis"
                                                                        KeyFieldName="CodigoCampo" AutoGenerateColumns="False"
                                                                        ID="gvd_CAL_CamposCalculaveis" OnHtmlRowCreated="gvd_CAL_CamposCalculaveis_HtmlRowCreated">
                                                                        <Columns>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoCampo" Name="colCodigo" Caption="<%$ Resources:traducao, adm_CadastroFormularios_c_digo %>"
                                                                                Visible="False" VisibleIndex="0">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn Name="colNumLinha" Width="30px" Caption="<%$ Resources:traducao, adm_CadastroFormularios_linha %>" VisibleIndex="0">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="NomeCampo" Name="colCampo" Width="250px"
                                                                                Caption="<%$ Resources:traducao, adm_CadastroFormularios__a____campos %>" VisibleIndex="1">
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                            <dxwgv:GridViewDataTextColumn FieldName="ValorCampo" Name="colValorCampo" Width="80px"
                                                                                Caption="<%$ Resources:traducao, adm_CadastroFormularios__b____valor %>" VisibleIndex="2">
                                                                                <DataItemTemplate>
                                                                                    <dxe:ASPxTextBox ID="txtValorCampo" runat="server" ClientInstanceName="txtValorCampo"
                                                                                        Width="60px" HorizontalAlign="Right">
                                                                                    </dxe:ASPxTextBox>
                                                                                </DataItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                            </dxwgv:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                                        </SettingsPager>
                                                                    </dxwgv:ASPxGridView>
                                                                </td>
                                                                <td style="width: 25px"></td>
                                                                <td valign="top" align="left">
                                                                    <table style="width: 120px; height: 140px;">
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <dxe:ASPxLabel ID="ASPxLabel110" runat="server"
                                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_precis_o_decimal_ %>">
                                                                                </dxe:ASPxLabel>
                                                                                <dxe:ASPxComboBox ID="ddl_CAL_Precisao" runat="server" ClientInstanceName="ddl_CAL_Precisao"
                                                                                    SelectedIndex="0" Width="100px">
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Selected="True" Text="0" Value="0" />
                                                                                        <dxe:ListEditItem Text="1" Value="1" />
                                                                                        <dxe:ListEditItem Text="2" Value="2" />
                                                                                        <dxe:ListEditItem Text="3" Value="3" />
                                                                                        <dxe:ListEditItem Text="4" Value="4" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <dxe:ASPxLabel ID="ASPxLabel111" runat="server"
                                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_formato_ %>">
                                                                                </dxe:ASPxLabel>
                                                                                <dxe:ASPxComboBox ID="ddl_CAL_Formato" runat="server" ClientInstanceName="ddl_CAL_Formato"
                                                                                    SelectedIndex="0" Width="100px">
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, adm_CadastroFormularios_n_mero %>" Value="N" />
                                                                                        <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_moeda %>" Value="M" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <dxe:ASPxLabel ID="ASPxLabel112" runat="server"
                                                                                    Text="<%$ Resources:traducao, adm_CadastroFormularios_agrega__o_ %>">
                                                                                </dxe:ASPxLabel>
                                                                                <dxe:ASPxComboBox ID="ddl_CAL_Agregacao" runat="server" ClientInstanceName="ddl_CAL_Agregacao"
                                                                                    SelectedIndex="0" Width="100px">
                                                                                    <Items>
                                                                                        <dxe:ListEditItem Selected="True" Text="<%$ Resources:traducao, adm_CadastroFormularios_nenhuma %>" Value="" />
                                                                                        <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_soma %>" Value="SOM" />
                                                                                        <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_m_dia %>" Value="MED" />
                                                                                        <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_m_ximo %>" Value="MAX" />
                                                                                        <dxe:ListEditItem Text="<%$ Resources:traducao, adm_CadastroFormularios_m_nimo %>" Value="MIN" />
                                                                                    </Items>
                                                                                </dxe:ASPxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 251px">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel20" runat="server"
                                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_f_rmula_ %>">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txt_CAL_Formula" runat="server" ClientInstanceName="txt_CAL_Formula"
                                                                                        MaxLength="150" Width="100%" ToolTip="<%$ Resources:traducao, adm_CadastroFormularios_referenciar_os_campos_no_formato_bn_onde__n__corresponde_ao_n_mero_da_linha_do_campo_na_tabela_de_campos_ao_lado__exemplo___b1_b2__100 %>">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="btnAvaliarFormula" runat="server" AutoPostBack="False" ClientInstanceName="btnAvaliarFormula"
                                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_avaliar_f_rmula %>" Width="135px">
                                                                                        <ClientSideEvents Click="function(s, e) {
	validaFormula();
}" />
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 10px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <dxe:ASPxLabel ID="ASPxLabel21" runat="server"
                                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_resultado_ %>">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxTextBox ID="txt_CAL_ResultadoFormula" runat="server" ClientInstanceName="txt_CAL_ResultadoFormula"
                                                                                        ReadOnly="True" Width="100%">
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="height: 10px"></td>
                                                                            </tr>
                                                                        </tbody>
                                                                        
                                                                    </table>
                                                                    <dxe:ASPxButton ID="btnSalvar_CAL" runat="server" OnClick="btnSalvar_VAR_Click"
                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_salvar %>" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                            if(trim(txt_CAL_Formula.GetText()) == '')
                                                                            {
                                                                                    window.top.mostraMensagem('Campo fórmula deve ser preenchido','erro' ,true,  false , null); 
                                                                                    e.processOnServer = false;
                                                                                     
                                                                            }
                                                                            else
                                                                            {
                                                                                   obtemValoresClientes('CAL');
                                                                            }	
}" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td valign="top"></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <br />
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="height: 19px" align="right">&nbsp;
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                         <dxp:ASPxPanel ID="dvVao" runat="server" Width="100%" ClientInstanceName="dvVAO"
                                            Visible="False">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <table cellspacing="0" cellpadding="0" border="0" style="width: 60%">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left">
                                                                    <dxe:ASPxLabel runat="server" Text="Nome do campo de projeto:"
                                                                        Font-Overline="False" ID="ASPxLabel23">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="padding-right: 10px">
                                                                    <dxe:ASPxComboBox runat="server" ValueType="System.Int32" TextField="Descricao"
                                                                        ValueField="Codigo" Width="100%" ClientInstanceName="ddl_VAO"
                                                                        DropDownStyle="DropDown" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                        DropDownRows="10" DropDownHeight="150px"
                                                                        Font-Overline="False" ID="ddl_VAO"  OnItemsRequestedByFilterCondition="ddl_VAO_ItemsRequestedByFilterCondition" OnItemRequestedByValue="ddl_VAO_ItemRequestedByValue">
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="padding-top: 5px;">
                                                                    <dxe:ASPxButton ID="ASPxButton1" runat="server" OnClick="btnSalvar_VAR_Click"
                                                                        Text="<%$ Resources:traducao, adm_CadastroFormularios_salvar %>" Width="90px">
                                                                        <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('VAO');
}" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </DetailRow>
                                </Templates>
                                <SettingsBehavior AllowSort="False" AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowAllRecords">
                                </SettingsPager>
                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="4">
                                </SettingsEditing>
                                <SettingsPopup>
                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                        AllowResize="True" />
                                    <HeaderFilter MinHeight="140px">
                                    </HeaderFilter>
                                </SettingsPopup>
                                <SettingsText ConfirmDelete="Deseja realmente excluir o campo selecionado?" PopupEditFormCaption="Informações do Campo"></SettingsText>
                                <ClientSideEvents EndCallback="function(s, e) {
         //alert(comando_gvCampos);
         if(comando_gvCampos == 'UPDATEEDIT' || 
             comando_gvCampos ==  'DELETEROW')
         {
                    gvFormularios.Refresh();
         }
         if(s.cp_Msg != '')
                   mostraPopupMensagemGravacao(s.cp_Msg);
}"
                                    CustomButtonClick="function(s, e) {
    var CodigoCampoSelecionado = s.GetRowKey(e.visibleIndex);
    if (e.buttonID == 'btnEditarCampo'){
        ppDvCampoCalculado.PerformCallback(CodigoCampoSelecionado);
    }
    else if (e.buttonID == 'btnExpressaoCampo')
        ppDvEditorExpressaoValidacao.PerformCallback(CodigoCampoSelecionado);
}" BeginCallback="function(s, e) {
	comando_gvCampos = e.command;
}" />
                                <Columns>
                                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="190px" Caption="Ações"
                                        ShowEditButton="true" ShowDeleteButton="true" VisibleIndex="0">
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                        <CustomButtons>
                                            <dxtv:GridViewCommandColumnCustomButton ID="btnEditarCampo" Visibility="Invisible" Text="Editar caracterização do campo">
                                                <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                </Image>
                                            </dxtv:GridViewCommandColumnCustomButton>
                                            <dxtv:GridViewCommandColumnCustomButton ID="btnExpressaoCampo" Text="Expressão de validação" Visibility="Invisible">
                                                <Image Url="~/imagens/botoes/btnExpValidacao.png">
                                                </Image>
                                            </dxtv:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <dxm:ASPxMenu ID="menu1" runat="server" BackColor="Transparent" ClientInstanceName="menu1"
                                                            ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init1">
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
                                                                        <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                                                            <Image Url="~/imagens/layout/saveLayout.png">
                                                                            </Image>
                                                                        </dxm:MenuItem>
                                                                        <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                            <Image Url="~/imagens/layout/restoreLayout.png">
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
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoCampo" Caption="ID" Visible="false" VisibleIndex="7">
                                        <PropertiesTextEdit>
                                            <Style>
                                                
                                            </Style>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NomeCampo" Caption="Nome do Campo" VisibleIndex="1">
                                        <PropertiesTextEdit MaxLength="500" Width="600px">
                                            <Style>
                                                
                                            </Style>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Nome do campo:" ColumnSpan="2" RowSpan="1" VisibleIndex="1"></EditFormSettings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DescricaoCampo" Caption="Descrição do Campo"
                                        Visible="False" VisibleIndex="8">
                                        <PropertiesTextEdit MaxLength="250" Width="600px">
                                            <Style>
                                                
                                            </Style>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Descrição do campo:" ColumnSpan="2" RowSpan="1" VisibleIndex="2"></EditFormSettings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn FieldName="CampoObrigatorio" Width="100px" Caption="Obrigatório"
                                        VisibleIndex="2">
                                        <PropertiesComboBox ValueType="System.String">
                                            <Items>
                                                <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="Não" Value="N"></dxe:ListEditItem>
                                            </Items>
                                            <Style>
                                                
                                            </Style>
                                        </PropertiesComboBox>
                                        <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Obrigatório:" ColumnSpan="2" RowSpan="1" VisibleIndex="2"></EditFormSettings>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoTipoCampo" Caption="Tipo do Campo" Visible="false"
                                        VisibleIndex="9" Width="150px">
                                        <PropertiesComboBox DropDownRows="8">
                                         </PropertiesComboBox>
                                        <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Tipo do campo:" ColumnSpan="2" RowSpan="1" VisibleIndex="3"></EditFormSettings>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="DefinicaoCampo" Caption="DefinicaoCampo"
                                        Visible="False" VisibleIndex="10">
                                        <PropertiesTextEdit>
                                            <Style>
                                                
                                            </Style>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="CodigoLookup" Visible="False" VisibleIndex="11">
                                        <PropertiesTextEdit>
                                            <Style>
                                                
                                            </Style>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn FieldName="Aba" Name="colAba" Caption="Aba" VisibleIndex="4"
                                        Width="150px">
                                        <PropertiesComboBox ValueType="System.String">
                                            <Items>
                                                <dxe:ListEditItem Text="Primeira Aba" Value="1"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="Segunda Aba" Value="2"></dxe:ListEditItem>
                                            </Items>
                                            <Style>
                                                
                                            </Style>
                                        </PropertiesComboBox>
                                        <EditFormSettings CaptionLocation="Top" Caption="Aba:" ColumnSpan="2" RowSpan="1" VisibleIndex="4"></EditFormSettings>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataComboBoxColumn FieldName="IndicaCampoVisivelGrid" Caption="Indica Campo Visível Grid"
                                        Visible="False" VisibleIndex="13">
                                        <PropertiesComboBox ValueType="System.String">
                                            <Items>
                                                <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="Não" Value="N"></dxe:ListEditItem>
                                            </Items>
                                            <Style>
                                                
                                            </Style>
                                        </PropertiesComboBox>
                                        <EditFormSettings Visible="True" VisibleIndex="5" CaptionLocation="Top" Caption="<%$ Resources:traducao, adm_CadastroFormularios_o_campo___vis_vel_na_lista_ %>" ColumnSpan="2" RowSpan="1"></EditFormSettings>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IniciaisCampoControladoSistema" Visible="False"
                                        VisibleIndex="14" Caption="Tag de controle">
                                        <EditFormSettings Caption="Tag de controle" CaptionLocation="Top" ColumnSpan="2" Visible="True" />
                                        <PropertiesTextEdit MaxLength="24"></PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn FieldName="IndicaCampoAtivo" Width="65px" Caption="Ativo"
                                        VisibleIndex="6">
                                        <PropertiesComboBox ValueType="System.String">
                                            <Items>
                                                <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="Nao" Value="N"></dxe:ListEditItem>
                                            </Items>
                                            <Style>
                                                
                                            </Style>
                                        </PropertiesComboBox>
                                        <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Ativo:" ColumnSpan="2" RowSpan="1" VisibleIndex="6"></EditFormSettings>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxtv:GridViewDataSpinEditColumn Caption="Ordem" FieldName="OrdemCampoFormulario" Name="OrdemCampoFormulario" Visible="true" VisibleIndex="5" Width="120px">
                                        <PropertiesSpinEdit DisplayFormatString="g" MaxValue="99" MaxLength="2" Width="670px">
                                        </PropertiesSpinEdit>
                                        <EditFormSettings Caption="Ordem:" CaptionLocation="Top" Visible="True" ColumnSpan="2" VisibleIndex="5" />
                                    </dxtv:GridViewDataSpinEditColumn>
                                    <dxtv:GridViewDataCheckColumn Caption="Campo Bloqueado?" FieldName="IndicaControladoSistema" Visible="False" VisibleIndex="12">
                                        <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                                        </PropertiesCheckEdit>
                                        <EditFormSettings Caption="Campo Bloqueado?" CaptionLocation="Top" ColumnSpan="2" Visible="True" />
                                    </dxtv:GridViewDataCheckColumn>
                                    <dxtv:GridViewDataTextColumn Caption="CodigoModeloFormulario" FieldName="CodigoModeloFormulario" Visible="False" VisibleIndex="16">
                                    </dxtv:GridViewDataTextColumn>
                                    <dxtv:GridViewDataTextColumn Caption="Tipo do Campo" FieldName="DescricaoCampoUsuario" ShowInCustomizationForm="False" VisibleIndex="3">
                                        <EditFormSettings Visible="False" />
                                    </dxtv:GridViewDataTextColumn>
                                </Columns>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="300"></Settings>
                                <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                            </dxwgv:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" EnableRowHotTrack="True"></SettingsBehavior>
                    <SettingsPager PageSize="50">
                    </SettingsPager>
                    <SettingsEditing Mode="PopupEditForm">
                    </SettingsEditing>
                    <SettingsPopup>
                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                            AllowResize="True" Height="385px" VerticalOffset="20" />

<HeaderFilter MinHeight="140px"></HeaderFilter>

                    </SettingsPopup>
                    <SettingsText Title="Formulários" GroupPanel="Arraste o cabeçalho da coluna para agrupar"
                        ConfirmDelete="Deseja excluir o formulário selecionado?" PopupEditFormCaption="Informações Sobre o Formulário"
                        EmptyDataRow="Nenhum formulário para exibir." CommandNew="Novo" CommandCancel="Cancelar"
                        CommandUpdate="Salvar"></SettingsText>
                    <ClientSideEvents CustomButtonClick="function(s, e) {
	if ( e.buttonID == 'btnTipoProjeto' )
	{
		e.processOnServer = false;
		pcDados.Show();
	}
    else if ( e.buttonID == 'btnCopiaFormulario' )
	{
		e.processOnServer = false;
		pcCopiaFormulario.Show();
	}
              else if ( e.buttonID == 'btnExcluir' )
	{
       var funcObj = { funcaoClickOK: function(linha){ s.DeleteRow(linha); } }
	window.top.mostraConfirmacao(traducao.adm_CadastroFormularios_deseja_realmente_excluir_o_registro_, function(){funcObj['funcaoClickOK'](e.visibleIndex)}, null);		

	}
	else
		e.processOnServer = true;
}"
                        EndCallback="function(s, e) {
              //debugger	
               if(s.cp_Msg != '')
               {
                          //mostraPopupMensagemGravacao(s.cp_Msg);
                         window.top.mostraMensagem(s.cp_Msg, 'sucesso', false, false, null);
                         pcMensagemGravacao.Hide();
                         onClick_btnCancelar();
               }	   
               else if(s.cp_Erro != '' && s.cp_Erro != undefined)
               {                          
                        window.top.mostraMensagem(s.cp_Erro, 'erro', true, false, null);
               }
               s.cp_Msg = '';
               s.cp_Erro ='';
}"></ClientSideEvents>
                    <Columns>
                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="180" Caption="Ações"
                            ShowEditButton="true" VisibleIndex="0">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnPublicar" Text="Publicar">
                                    <Image AlternateText="Publicar" Url="~/imagens/botoes/PublicarReg02.png">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnTipoProjeto" Text="Administrar Tipos de Projetos Relacionados ao Modelo">
                                    <Image AlternateText="Administrar Tipos de Projetos Relacionados ao Modelo" Url="~/imagens/botoes/permissoes.png">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnCopiaFormulario" Text="Criar um Novo Modelo de Formulário com Base Neste">
                                    <Image AlternateText="Criar um Novo Modelo de Formulário com Base Neste" Url="~/imagens/botoes/btnDuplicar.png">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir Formulário" Visibility="BrowsableRow">
                                    <Image AlternateText="Excluir Formulário" Url="~/imagens/botoes/excluirReg02.PNG">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td align="center">
                                            <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent" ClientInstanceName="menu"
                                                ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init">
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
                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                                                <Image Url="~/imagens/layout/saveLayout.png">
                                                                </Image>
                                                            </dxm:MenuItem>
                                                            <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                <Image Url="~/imagens/layout/restoreLayout.png">
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
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Publicado" FieldName="IndicaModeloPublicadoStr"
                            VisibleIndex="1" Width="90px">
                            <Settings AllowHeaderFilter="False" />
                            <EditFormSettings Visible="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="NomeFormulario" Name="colNomeFormulario"
                            Caption="Nome do Formulário" VisibleIndex="2">
                            <PropertiesTextEdit MaxLength="250" Width="670px">
                                <ValidationSettings ErrorText="">
                                    <RequiredField ErrorText=""></RequiredField>
                                </ValidationSettings>
                                <Style>
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <Settings AllowAutoFilter="True" AllowHeaderFilter="False" AutoFilterCondition="Contains"></Settings>
                            <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Nome do formulário:" ColumnSpan="2"></EditFormSettings>
                            <FilterCellStyle>
                                <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                            </FilterCellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="DescricaoFormulario" Name="colDescricaoFormulario"
                            Caption="Descri&#231;&#227;o" Visible="False" VisibleIndex="3">
                            <PropertiesTextEdit MaxLength="2500" Width="670px">
                                <Style>
                                    
                                </Style>
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Descrição:" ColumnSpan="2"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Tipo do Formulário" FieldName="DescricaoTipoFormulario"
                            VisibleIndex="4" Width="180px">
                            <Settings AllowAutoFilter="True" AllowHeaderFilter="False" />
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoTipoFormulario" Name="colCodigoTipoFormulario"
                            Width="180px" Caption="Tipo do Formulário" VisibleIndex="5" Visible="False">
                            <PropertiesComboBox DataSourceID="dsTipoFormulario" TextField="descricaoTipoFormulario"
                                ValueField="codigoTipoFormulario" ValueType="System.Int32">
                                <ValidationSettings ErrorText="">
                                    <RequiredField ErrorText=""></RequiredField>
                                </ValidationSettings>
                                <Style>
                                    
                                </Style>
                            </PropertiesComboBox>
                            <Settings AllowHeaderFilter="False"></Settings>
                            <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Tipo do formulário:" ColumnSpan="2"></EditFormSettings>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataComboBoxColumn FieldName="IndicaToDoListAssociado" Caption="Indica To Do List Associado"
                            Visible="False" VisibleIndex="6">
                            <PropertiesComboBox ValueType="System.String">
                                <Items>
                                    <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Não" Value="N"></dxe:ListEditItem>
                                </Items>
                                <Style>
                                    
                                </Style>
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" VisibleIndex="3" CaptionLocation="Top" Caption="Disponibilizar To Do List para o formulário?" ColumnSpan="2"></EditFormSettings>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataComboBoxColumn FieldName="IndicaAnexoAssociado" Caption="Indica Anexo Associado"
                            Visible="False" VisibleIndex="9">
                            <PropertiesComboBox ValueType="System.String">
                                <Items>
                                    <dxe:ListEditItem Text="Sim" Value="S"></dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Não" Value="N"></dxe:ListEditItem>
                                </Items>
                                <Style>
                                    
                                </Style>
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" VisibleIndex="4" CaptionLocation="Top" Caption="Permitir anexação de documentos ao formulário?" ColumnSpan="2"></EditFormSettings>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataMemoColumn FieldName="Abas" Name="colAbas" Caption="Abas" Visible="False"
                            VisibleIndex="7">
                            <PropertiesMemoEdit Rows="4">
                                <Style>
                                    
                                </Style>
                            </PropertiesMemoEdit>
                            <EditFormSettings Visible="True" CaptionLocation="Top" Caption="Abas:" ColumnSpan="2"></EditFormSettings>
                        </dxwgv:GridViewDataMemoColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="IniciaisFormularioControladoSistema" Caption="Tag de controle"
                            Visible="False" VisibleIndex="10">
                            <EditFormSettings Caption="Tag de controle" CaptionLocation="Top" ColumnSpan="2" Visible="True"  />
                            <PropertiesTextEdit MaxLength="24"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxtv:GridViewDataCheckColumn Caption="Modelo de Formulário Bloqueado?" FieldName="IndicaControladoSistema" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                            <PropertiesCheckEdit ValueChecked="S" ValueType="System.String" ValueUnchecked="N">
                            </PropertiesCheckEdit>
                            <EditFormSettings Caption="Modelo de Formulário Bloqueado?" CaptionLocation="Top" ColumnSpan="2" Visible="True" />
                        </dxtv:GridViewDataCheckColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Visible"
                        VerticalScrollableHeight="300"></Settings>
                    <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                    <Paddings PaddingTop="10px" />
                </dxwgv:ASPxGridView>
            </td>
        </tr>
    </table>
    <dxpc:ASPxPopupControl ID="pcDados" runat="server" ClientInstanceName="pcDados" HeaderText="Tipos de Projetos Relacionados ao Formulário"
        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowCloseButton="False" Width="721px" PopupVerticalOffset="25" ShowHeader="False">
        <ClientSideEvents PopUp="function(s, e) {
	e.processOnServer = false;
	pcDados_OnPopup(s,e);
}" Init="function(s, e) {
 var sHeight = Math.max(0, document.documentElement.clientHeight) - 100;
s.SetHeight(sHeight);
}"></ClientSideEvents>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                    <tbody>
                        <tr>
                            <td>
                                <dxe:ASPxLabel runat="server" Text="Formulário:"
                                    ID="ASPxLabel1011">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="30" ReadOnly="True" ClientInstanceName="txtNomeFormulario"
                                    ID="txtNomeFormulario">
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="Tipos de Projetos Relacionados ao Formulário:"
                                                    Width="100%" ID="ASPxLabel105">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvTiposProjetos" KeyFieldName="CodigoTipoProjeto"
                                                    AutoGenerateColumns="False" Width="100%" ID="gvTiposProjetos"
                                                    OnRowDeleting="gvTiposProjetos_RowDeleting" OnCellEditorInitialize="gvTiposProjetos_CellEditorInitialize"
                                                    OnRowUpdating="gvTiposProjetos_RowUpdating" OnRowInserting="gvTiposProjetos_RowInserting"
                                                    OnCustomCallback="gvTiposProjetos_CustomCallback">
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) {	
	e.processOnServer = false;
	gvTiposProjetos_FocusedRowChanged(s,e);
}"
                                                        EndCallback="function(s, e) {	
	gvTipoProjetos_onEndCallback(s,e);
	
}"></ClientSideEvents>
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Width="80px" VisibleIndex="0" ShowEditButton="true"
                                                            ShowDeleteButton="true">
                                                            <HeaderTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <dxm:ASPxMenu ID="menu2" runat="server" BackColor="Transparent" ClientInstanceName="menu2"
                                                                                ItemSpacing="5px" OnItemClick="menu_ItemClick" OnInit="menu_Init2">
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
                                                                                            <dxm:MenuItem Name="btnHTML" Text="HTML" ToolTip="Exportar para HTML" ClientVisible="False">
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
                                                                                                <Image Url="~/imagens/layout/saveLayout.png">
                                                                                                </Image>
                                                                                            </dxm:MenuItem>
                                                                                            <dxm:MenuItem Text="Restaurar" ToolTip="Restaurar Layout">
                                                                                                <Image Url="~/imagens/layout/restoreLayout.png">
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
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoTipoProjeto" Caption="Tipo de projeto"
                                                            Visible="False" VisibleIndex="2">
                                                            <PropertiesComboBox ValueType="System.Int32" Width="120px">
                                                                <DropDownButton ToolTip="Selecione o tipo de projeto a relacionar com o Formulário em quest&#227;o.">
                                                                </DropDownButton>
                                                                <ValidationSettings SetFocusOnError="True" Display="Dynamic">
                                                                    <RequiredField IsRequired="True" ErrorText="Escolher o tipo de projeto a relacionar com o formulário."></RequiredField>
                                                                </ValidationSettings>
                                                                <Style>
                                                                    
                                                                </Style>
                                                            </PropertiesComboBox>
                                                            <EditFormSettings Visible="True" VisibleIndex="0" CaptionLocation="Top" Caption="Associar a:"></EditFormSettings>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="TipoProjeto" Width="150px" Caption="Tipo"
                                                            VisibleIndex="1">
                                                            <PropertiesTextEdit Width="180px">
                                                                <ValidationSettings Display="Dynamic">
                                                                </ValidationSettings>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings Visible="False" VisibleIndex="0" CaptionLocation="Top"></EditFormSettings>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="TextoOpcaoFormulario" Width="200px" Caption="Nome no Menu"
                                                            VisibleIndex="3">
                                                            <PropertiesTextEdit MaxLength="40" Width="150px">
                                                                <ValidationSettings SetFocusOnError="True" Display="Dynamic">
                                                                    <RequiredField IsRequired="True" ErrorText="Iinformar o nome a ser mostrado como op&#231;&#227;o de acesso ao formulário para o tipo de projeto em quest&#227;o."></RequiredField>
                                                                </ValidationSettings>
                                                                <Style>
                                                                    
                                                                </Style>
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings Visible="True" VisibleIndex="1" CaptionLocation="Top" Caption="Nome no Menu"></EditFormSettings>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="TipoOcorrencia" Caption="Ocorr&#234;ncia"
                                                            VisibleIndex="4">
                                                            <PropertiesComboBox ValueType="System.String" Width="80px">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="1 vez" Value="U"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="N Vezes" Value="N"></dxe:ListEditItem>
                                                                </Items>
                                                                <DropDownButton ToolTip="Selecione o tipo de ocorr&#234;ncia do formulário para o tipo de projeto em quest&#227;o">
                                                                </DropDownButton>
                                                                <ValidationSettings>
                                                                    <RequiredField IsRequired="True" ErrorText="Escolher o tipo de ocorr&#234;ncia do formulário para o tipo de projeto em quest&#227;o."></RequiredField>
                                                                </ValidationSettings>
                                                                <Style>
                                                                    
                                                                </Style>
                                                            </PropertiesComboBox>
                                                            <EditFormSettings ColumnSpan="2" Visible="True" VisibleIndex="2" CaptionLocation="Top"></EditFormSettings>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="SomenteLeitura" Caption="Acesso" VisibleIndex="6">
                                                            <PropertiesComboBox ValueType="System.String">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Leitura" Value="S"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Text="Escrita" Value="N"></dxe:ListEditItem>
                                                                </Items>
                                                                <Style>
                                                                    
                                                                </Style>
                                                            </PropertiesComboBox>
                                                            <EditFormSettings Visible="True" VisibleIndex="4" CaptionLocation="Top"></EditFormSettings>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="RegistroNovo" Caption="RegistroNovo" Visible="False"
                                                            VisibleIndex="5">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowGroup="False" AllowFocusedRow="True"
                                                        ConfirmDelete="True"></SettingsBehavior>
                                                    <SettingsPager PageSize="4" Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="4">
                                                    </SettingsEditing>
                                                    <SettingsPopup>
                                                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                            Width="350px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
                                                    </SettingsPopup>
                                                    <Settings VerticalScrollableHeight="135" VerticalScrollBarMode="Auto" />
                                                    <SettingsText ConfirmDelete="Retirar o formulário para este tipo de projeto?"
                                                        PopupEditFormCaption="Formulário no Tipo de Projeto" EmptyDataRow="Nenhum tipo de projeto está relacionado a este formulário"
                                                        CommandNew="Novo" CommandCancel="Cancelar" CommandUpdate="Salvar"></SettingsText>
                                                    <Styles>
                                                        <Cell>
                                                        </Cell>
                                                        <CommandColumn>
                                                        </CommandColumn>
                                                        <CommandColumnItem>
                                                        </CommandColumnItem>
                                                    </Styles>
                                                </dxwgv:ASPxGridView>
                                                <dxhf:ASPxHiddenField runat="server" ClientInstanceName="hfStatus" ID="hfStatus"
                                                    OnCustomCallback="hfStatus_CustomCallback">
                                                    <ClientSideEvents EndCallback="function(s, e) {
	hfStatus_onEndCallback(s,e);
}"></ClientSideEvents>
                                                </dxhf:ASPxHiddenField>
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
                            <td align="left">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel runat="server" Text="Sele&#231;&#227;o de Status para o Tipo:  " Width="180px"
                                                    Height="15px" ID="ASPxLabel8">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 5px; height: 16px"></td>
                                            <td style="height: 16px">
                                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblSelecaoStatus" Height="15px"
                                                    Font-Bold="True" Font-Italic="True" ID="lblSelecaoStatus">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 47%">
                                                <dxe:ASPxLabel runat="server" Text="Status Dispon&#237;veis:"
                                                    ID="ASPxLabel106">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="width: 6%"></td>
                                            <td style="width: 47%">
                                                <dxe:ASPxLabel runat="server" Text="Status Selecionados:"
                                                    ID="ASPxLabel107">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="3" ClientInstanceName="lbDisponiveisStatus"
                                                    EnableClientSideAPI="True" Width="100%" Height="150px"
                                                    ID="lbDisponiveisStatus" OnCallback="lbDisponiveisStatus_Callback" EnableSynchronization="False">
                                                    <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Disp_');
}"
                                                        SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                    <ValidationSettings>
                                                        <ErrorImage Width="14px">
                                                        </ErrorImage>
                                                    </ValidationSettings>
                                                </dxe:ASPxListBox>
                                            </td>
                                            <td align="center">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 41px; height: 10px" valign="middle"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 5px;">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddAll"
                                                                    ClientEnabled="False" Text="&gt;&gt;" EncodeHtml="False" Width="55px"
                                                                    ToolTip="Selecionar todos os usuarios" ID="btnAddAll">
                                                                    <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbDisponiveisStatus,lbSelecionadosStatus);
	setListBoxItemsInMemory(lbDisponiveisStatus,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosStatus,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 5px;">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnAddSel"
                                                                    ClientEnabled="False" Text="&gt;" EncodeHtml="False" Width="55px"
                                                                    ToolTip="Selecionar os usu&#225;rios marcados" ID="btnAddSel">
                                                                    <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbDisponiveisStatus, lbSelecionadosStatus);
	setListBoxItemsInMemory(lbDisponiveisStatus,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosStatus,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-bottom: 5px;">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveSel"
                                                                    ClientEnabled="False" Text="&lt;" EncodeHtml="False" Width="55px"
                                                                    ToolTip="Retirar da sele&#231;&#227;o os usu&#225;rios marcados" ID="btnRemoveSel">
                                                                    <ClientSideEvents Click="function(s, e) {
	lb_moveItem(lbSelecionadosStatus, lbDisponiveisStatus);
	setListBoxItemsInMemory(lbDisponiveisStatus,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosStatus,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 28px">
                                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnRemoveAll"
                                                                    ClientEnabled="False" Text="&lt;&lt;" EncodeHtml="False" Width="55px"
                                                                    ToolTip="Retirar da sele&#231;&#227;o todos os usu&#225;rios" ID="btnRemoveAll">
                                                                    <ClientSideEvents Click="function(s, e) {
	lb_moveTodosItens(lbSelecionadosStatus, lbDisponiveisStatus);
	setListBoxItemsInMemory(lbDisponiveisStatus,'Disp_');
	setListBoxItemsInMemory(lbSelecionadosStatus,'Sel_');
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td>
                                                <dxe:ASPxListBox runat="server" EncodeHtml="False" Rows="4" ClientInstanceName="lbSelecionadosStatus"
                                                    EnableClientSideAPI="True" Width="100%" Height="150px"
                                                    ID="lbSelecionadosStatus" OnCallback="lbSelecionadosStatus_Callback" EnableSynchronization="False">
                                                    <ClientSideEvents EndCallback="function(s, e) {
	setListBoxItemsInMemory(s,'Sel_');
	habilitaBotoesListBoxes();
}"
                                                        SelectedIndexChanged="function(s, e) {
	habilitaBotoesListBoxes();
}"></ClientSideEvents>
                                                    <ValidationSettings>
                                                        <ErrorImage Width="14px">
                                                        </ErrorImage>
                                                    </ValidationSettings>
                                                </dxe:ASPxListBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table class="formulario-botoes" id="Table2" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvar"
                                                    CausesValidation="False" Text="Salvar" Width="90px" ID="btnSalvar">
                                                    <ClientSideEvents Click="function(s, e) {
    e.processOnServer = false;
    if (window.onClick_btnSalvar)
		onClick_btnSalvar();
}"></ClientSideEvents>
                                                </dxe:ASPxButton>
                                            </td>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar"
                                                    Text="Fechar" Width="90px" ID="btnFechar">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelar)
       onClick_btnCancelar();
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
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="pcMensagemGravacao" runat="server" ClientInstanceName="pcMensagemGravacao"
        HeaderText="" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
        ShowShadow="False" Width="270px">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tbody>
                        <tr>
                            <td style="" align="center"></td>
                            <td style="width: 70px" align="center" rowspan="3">
                                <dxe:ASPxImage runat="server" ImageUrl="~/imagens/Workflow/salvarBanco.png" ImageAlign="TextTop"
                                    ClientInstanceName="imgSalvar" ID="imgSalvar">
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblAcaoGravacao"
                                    ID="lblAcaoGravacao">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="pcCopiaFormulario" runat="server" ClientInstanceName="pcCopiaFormulario"
        HeaderText="Copia Formulário" Modal="True" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" ShowCloseButton="True" Width="721px"
        AllowDragging="True" AllowResize="True" CssClass="popup">
        <ClientSideEvents PopUp="function(s, e) {
	e.processOnServer = false;
	pcCopiaFormulario_OnPopup(s,e);
}"></ClientSideEvents>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <table class="formulario" cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tbody>
                        <tr>
                            <td class="formulario-label">
                                <dxe:ASPxLabel runat="server" Text="Informe o nome do novo formulário:"
                                    ID="ASPxLabel1012">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="250" ClientInstanceName="txtNomeFormularioCopia"
                                    ID="txtNomeFormularioCopia">
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <dxrp:ASPxRoundPanel ID="rpEntidade" runat="server"
                                    HeaderText="Selecione a Entidade para a qual se deseja copiar o formulário:"
                                    View="GroupBox" Width="100%" ClientVisible="False">
                                    <PanelCollection>
                                        <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                            <dxe:ASPxComboBox ID="ddlEntidade" runat="server" ClientInstanceName="ddlEntidade"
                                                IncrementalFilteringMode="Contains" TextFormatString="{1}"
                                                ValueType="System.Int32" Width="100%">
                                                <Columns>
                                                    <dxe:ListBoxColumn Caption="Sigla" FieldName="SiglaUnidadeNegocio" Width="100px" />
                                                    <dxe:ListBoxColumn Caption="Entidade" FieldName="NomeUnidadeNegocio" Width="300px" />
                                                </Columns>
                                            </dxe:ASPxComboBox>
                                            <br />
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxrp:ASPxRoundPanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table class="formulario-botoes" id="Table3" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSalvarCopiaFormulario"
                                                    CausesValidation="False" Text="Salvar" Width="90px" ID="btnSalvarCopiaFormulario">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnSalvarCopiaFormulario)
		onClick_btnSalvarCopiaFormulario();
}"></ClientSideEvents>
                                                </dxe:ASPxButton>
                                            </td>
                                            <td class="formulario-botao">
                                                <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFecharCopiaFormulario"
                                                    Text="Fechar" Width="90px" ID="btnFecharCopiaFormulario">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    if (window.onClick_btnCancelarCopiaFormulario)
       onClick_btnCancelarCopiaFormulario();
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
                <dxhf:ASPxHiddenField ID="hfStatusCopiaFormulario" runat="server" ClientInstanceName="hfStatusCopiaFormulario"
                    OnCustomCallback="hfStatusCopiaFormulario_CustomCallback">
                    <ClientSideEvents EndCallback="function(s, e) {
	hfStatusCopiaFormulario_onEndCallback(s,e);
}" />
                </dxhf:ASPxHiddenField>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <asp:SqlDataSource ID="dsTipoCampo" runat="server" SelectCommand="SELECT * FROM [TipoCampo] order by DescricaoCampoUsuario"></asp:SqlDataSource>
    <asp:SqlDataSource ID="dsTipoFormulario" runat="server" SelectCommand="SELECT * FROM [TipoFormulario] order by DescricaoTipoFormulario"></asp:SqlDataSource>
    <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados" ID="ASPxGridViewExporter1"
        OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
        <Styles>
            <Default>
            </Default>
            <Header>
            </Header>
            <Cell>
            </Cell>
            <GroupFooter Font-Bold="True">
            </GroupFooter>
            <Title Font-Bold="True"></Title>
        </Styles>
    </dxwgv:ASPxGridViewExporter>
    <dxcp:ASPxHiddenField runat="server" ClientInstanceName="hfGeral" ID="hfGeral">
    </dxcp:ASPxHiddenField>

    <UC:uc_cfg_validacaoCampo runat="server" ID="uc_cfg_validacaoCampo" />

    <script src="../../Bootstrap/vendor/jquery/v3.3.1/jquery-3.3.1.min.js"></script>
    <script src="../Bootstrap/vendor/jquery/v3.3.1/jquery-3.3.1.min.js"></script>
</asp:Content>

