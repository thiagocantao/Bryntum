<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="manterFormularios.aspx.cs"
    Inherits="formularios_manterFormularios" Title="Portal da Estratégia" %>
<%@ MasterType VirtualPath="~/novaCdis.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">

    <script type="text/javascript"> 
function obtemValoresClientes(tipo)
{
    if (tipo=="VAR")
    {
        hfControle.Set("VAR_tamanho", txt_Var_tamanho.GetText());
        //hfControle.Set("VAR_mascara", txt_Var_mascara.GetText());
    }
    else if (tipo == "TXT")
    {
        hfControle.Set("TXT_tamanho", txt_TXT_Tamanho.GetText());
        hfControle.Set("TXT_linhas", txt_TXT_Linhas.GetText());
        var TXT_Formatacao = -1
        if (rb_TXT_SemFormatacao.GetChecked())
            TXT_Formatacao = 0;
        else if (rb_TXT_FormatacaoSimples.GetChecked())
            TXT_Formatacao = 1;
        else if (rb_TXT_FormatacaoAvancada.GetChecked())
            TXT_Formatacao = 2;
        hfControle.Set("TXT_formatacao", TXT_Formatacao);
    }
    else if (tipo == "NUM")
    {
        hfControle.Set("NUM_Minimo", txt_NUM_Minimo.GetText());
        hfControle.Set("NUM_Maximo", txt_NUM_Maximo.GetText());
        hfControle.Set("NUM_Precisao", ddl_NUM_Precisao.GetSelectedItem().value);
        hfControle.Set("NUM_Formato", ddl_NUM_Formato.GetSelectedItem().value);
    }
    else if (tipo == "LST")
    {
        hfControle.Set("LST_Opcoes", txt_LST_Opcoes.GetText());
        var LST_Formatacao = -1
        if (rb_LST_Combo.GetChecked())
            LST_Formatacao = 0;
        else if (rb_LST_Radio.GetChecked())
            LST_Formatacao = 1;
        else if (rb_LST_Check.GetChecked())
            LST_Formatacao = 2;
        hfControle.Set("LST_Formatacao", LST_Formatacao);
    }
    else if (tipo == "DAT")
    {
        var DAT_IncluirHora = "N"
        if (rb_DAT_Sim.GetChecked())
            DAT_IncluirHora = "S";

        var DAT_ValorInicial = "B"
        if (rb_DAT_Atual.GetChecked())
            DAT_ValorInicial = "A";
    
        hfControle.Set("DAT_IncluirHora", DAT_IncluirHora);
        hfControle.Set("DAT_ValorInicial", DAT_ValorInicial);
    }
    else if (tipo == "BOL")
    {
        hfControle.Set("BOL_TextoVerdadeiro", txt_BOL_TextoVerdadeiro.GetText());
        hfControle.Set("BOL_ValorVerdadeiro", txt_BOL_ValorVerdadeiro.GetText());
        hfControle.Set("BOL_TextoFalso", txt_BOL_TextoFalso.GetText());
        hfControle.Set("BOL_ValorFalso", txt_BOL_ValorFalso.GetText());
    }
    else if (tipo == "SUB")
    {
        hfControle.Set("SUB_CodigoFormulario", ddl_SUB_Formulario.GetSelectedItem().value);
    }
    else if (tipo == "CPD")
    {
        hfControle.Set("CPD_CampoPre", ddl_CPD_CampoPre.GetSelectedItem().value);
    }
    else if (tipo == "LOO")
    {
        hfControle.Set("LOO_ListaPre", ddl_LOO_ListaPre.GetSelectedItem().value);
    }
    
}
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    &nbsp;
    <dxe:ASPxButton ID="btnNovoFormulario" runat="server" 
        CssPostfix="Aqua" OnClick="btnNovoFormulario_Click" Text="Novo Formulário">
    </dxe:ASPxButton>
    <br />
    <dxwgv:ASPxGridView ID="gvFormularios" runat="server" 
        CssPostfix="Aqua" AutoGenerateColumns="False" KeyFieldName="CodigoModeloFormulario"
        OnRowInserting="gvFormularios_RowInserting" OnRowUpdating="gvFormularios_RowUpdating"
        OnDetailRowExpandedChanged="gvFormularios_DetailRowExpandedChanged" OnInitNewRow="gvFormularios_InitNewRow"  OnStartRowEditing="gvFormularios_StartRowEditing" OnRowValidating="gvFormularios_RowValidating" >
        <Styles >
            <FocusedRow BackColor="#FFFFC0">
            </FocusedRow>
        </Styles>
        <SettingsLoadingPanel Text="" />
        <SettingsPager>
            <AllButton>
                <Image Height="19px" Width="27px" />
            </AllButton>
            <FirstPageButton>
                <Image Height="19px" Width="23px" />
            </FirstPageButton>
            <LastPageButton>
                <Image Height="19px" Width="23px" />
            </LastPageButton>
            <NextPageButton>
                <Image Height="19px" Width="19px" />
            </NextPageButton>
            <PrevPageButton>
                <Image Height="19px" Width="19px" />
            </PrevPageButton>
        </SettingsPager>
        <Images>
            <CollapsedButton Height="15px" 
                Width="15px" />
            <ExpandedButton Height="15px" 
                Width="15px" />
            <DetailCollapsedButton Height="15px" 
                Width="15px" />
            <DetailExpandedButton Height="15px" 
                Width="15px" />
            <HeaderFilter Height="19px"  Width="19px" />
            <HeaderActiveFilter Height="19px" 
                Width="19px" />
            <HeaderSortDown Height="5px" 
                Width="7px" />
            <HeaderSortUp Height="5px"  Width="7px" />
            <FilterRowButton Height="13px" Width="13px" />
            <WindowResizer Height="13px" Width="13px" />
        </Images>
        <StylesEditors>
            <ProgressBar Height="25px">
            </ProgressBar>
        </StylesEditors>
        <ImagesEditors>
            <CalendarFastNavPrevYear Height="19px" 
                Width="19px" />
            <CalendarFastNavNextYear Height="19px" 
                Width="19px" />
            <DropDownEditDropDown Height="7px" 
                
                Width="9px" />
            <SpinEditIncrement Height="6px" 
                Width="7px" />
            <SpinEditDecrement Height="7px" 
                Width="7px" />
            <SpinEditLargeIncrement Height="9px" 
                Width="7px" />
            <SpinEditLargeDecrement Height="9px" 
                Width="7px" />
        </ImagesEditors>
        <SettingsText CommandCancel="Cancelar" CommandNew="Novo" CommandUpdate="Salvar" Title="Formul&#225;rios"
            PopupEditFormCaption="Informa&#231;&#245;es sobre o formul&#225;rio" />
        <Columns>
            <dxwgv:GridViewCommandColumn VisibleIndex="0" ShowEditButton="true">
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn Caption="Nome do formul&#225;rio" FieldName="NomeFormulario"
                Name="colNomeFormulario" VisibleIndex="1" Width="800px">
                <PropertiesTextEdit MaxLength="250" Width="670px">
                    <ValidationSettings ErrorText="">
                        <RequiredField ErrorText="" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings CaptionLocation="Top" Visible="True" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Descri&#231;&#227;o" FieldName="DescricaoFormulario"
                Name="colDescricaoFormulario" Visible="False" VisibleIndex="2">
                <PropertiesTextEdit MaxLength="2500" Width="670px">
                </PropertiesTextEdit>
                <EditFormSettings CaptionLocation="Top" Visible="True" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="Tipo do Formulario" FieldName="CodigoTipoFormulario"
                Name="colCodigoTipoFormulario" VisibleIndex="2">
                <PropertiesComboBox DataSourceID="dsTipoFormulario" TextField="descricaoTipoFormulario"
                    ValueField="codigoTipoFormulario" ValueType="System.Int32">
                    <ValidationSettings ErrorText="">
                        <RequiredField ErrorText="" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <EditFormSettings CaptionLocation="Top" Visible="True" />
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataMemoColumn Caption="Abas" FieldName="Abas" Name="colAbas" Visible="False"
                VisibleIndex="3">
                <PropertiesMemoEdit Rows="4">
                </PropertiesMemoEdit>
                <EditFormSettings CaptionLocation="Top" Visible="True" />
            </dxwgv:GridViewDataMemoColumn>
        </Columns>
        <SettingsEditing EditFormColumnCount="1"/>
        <Templates>
            <DetailRow>
                <dxe:ASPxButton ID="btnNovoCampo" runat="server" 
                    CssPostfix="Aqua" OnClick="btnNovoCampo_Click" Text="Novo Campo">
                </dxe:ASPxButton>
                <br />
                <dxwgv:ASPxGridView ID="gvCampos" runat="server" AutoGenerateColumns="False" 
                    CssPostfix="Aqua" KeyFieldName="CodigoCampo" OnBeforePerformDataSelect="gvCampos_BeforePerformDataSelect"
                    OnDetailRowExpandedChanged="gvCampos_DetailRowExpandedChanged" OnRowInserting="gvCampos_RowInserting"
                    OnRowUpdating="gvCampos_RowUpdating" Width="100%" OnCellEditorInitialize="gvCampos_CellEditorInitialize" OnRowValidating="gvCampos_RowValidating">
                    <Templates>
                        <DetailRow>
                            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                <ContentTemplate>
                                    <!--Inicio -->
                                    <dxhf:ASPxHiddenField ID="hfControle" runat="server" ClientInstanceName="hfControle">
                                    </dxhf:ASPxHiddenField>
                                    <dxp:ASPxPanel ID="dvVAR" runat="server" Visible="False" Width="600px">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Quantidade m&#225;xima de caracteres:">
                                                </dxe:ASPxLabel>
                                                <dxe:ASPxTextBox ID="txt_Var_tamanho" runat="server" Width="170px" ClientInstanceName="txt_Var_tamanho">
                                                </dxe:ASPxTextBox>
                                                &nbsp;
                                                <table>
                                                    <tr>
                                                        <td align="right" style="height: 19px">
                                                            <dxe:ASPxButton ID="btnSalvar_VAR" runat="server" 
                                                                CssPostfix="Aqua" Text="Salvar" OnClick="btnSalvar_VAR_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('VAR');
}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                    <dxp:ASPxPanel ID="dvTXT" runat="server" Visible="False" Width="100%">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Quantidade m&#225;xima de caracteres:">
                                                </dxe:ASPxLabel>
                                                <dxe:ASPxTextBox ID="txt_TXT_Tamanho" runat="server" Width="170px" ClientInstanceName="txt_TXT_Tamanho">
                                                </dxe:ASPxTextBox>
                                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Quantidade de linhas:">
                                                </dxe:ASPxLabel>
                                                <dxe:ASPxTextBox ID="txt_TXT_Linhas" runat="server" Width="170px" ClientInstanceName="txt_TXT_Linhas">
                                                </dxe:ASPxTextBox>
                                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Formata&#231;&#227;o a ser utilizada:">
                                                </dxe:ASPxLabel>
                                                <br />
                                                <dxe:ASPxRadioButton ID="rb_TXT_SemFormatacao" runat="server" GroupName="Formato"
                                                    Text="N&#227;o utilizar formata&#231;&#227;o" ClientInstanceName="rb_TXT_SemFormatacao">
                                                </dxe:ASPxRadioButton>
                                                <dxe:ASPxRadioButton ID="rb_TXT_FormatacaoSimples" runat="server" GroupName="Formato"
                                                    Text="Simples (formata&#231;&#227;o do texto)" ClientInstanceName="rb_TXT_FormatacaoSimples">
                                                </dxe:ASPxRadioButton>
                                                <dxe:ASPxRadioButton ID="rb_TXT_FormatacaoAvancada" runat="server" GroupName="Formato"
                                                    Text="Avan&#231;ada (Inclus&#227;o de imagens)" ClientInstanceName="rb_TXT_FormatacaoAvancada">
                                                </dxe:ASPxRadioButton>
                                                <table>
                                                    <tr>
                                                        <td align="right" style="height: 19px">
                                                            <dxe:ASPxButton ID="btnSalvar_TXT" runat="server" 
                                                                CssPostfix="Aqua" Text="Salvar" OnClick="btnSalvar_VAR_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('TXT');
}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                    <dxp:ASPxPanel ID="dvNUM" runat="server" Visible="False" Width="100%">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <table>
                                                    <tr>
                                                        <td style="width: 100px">
                                                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Valor M&#237;nimo:">
                                                            </dxe:ASPxLabel>
                                                            <dxe:ASPxTextBox ID="txt_NUM_Minimo" runat="server" Width="80px" ClientInstanceName="txt_NUM_Minimo">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 100px">
                                                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Valor M&#225;ximo:">
                                                            </dxe:ASPxLabel>
                                                            <dxe:ASPxTextBox ID="txt_NUM_Maximo" runat="server" Width="80px" ClientInstanceName="txt_NUM_Maximo">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Precis&#227;o decimal:">
                                                </dxe:ASPxLabel>
                                                &nbsp;<dxe:ASPxComboBox ID="ddl_NUM_Precisao" runat="server" SelectedIndex="0" ValueType="System.String"
                                                    Width="90px" ClientInstanceName="ddl_NUM_Precisao">
                                                    <Items>
                                                        <dxe:ListEditItem Text="Autom&#225;tico" Value="-1" Selected="True" />
                                                        <dxe:ListEditItem Text="0" Value="0" />
                                                        <dxe:ListEditItem Text="1" Value="1" />
                                                        <dxe:ListEditItem Text="2" Value="2" />
                                                        <dxe:ListEditItem Text="3" Value="3" />
                                                        <dxe:ListEditItem Text="4" Value="4" />
                                                    </Items>
                                                </dxe:ASPxComboBox>
                                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Formato:">
                                                </dxe:ASPxLabel>
                                                <dxe:ASPxComboBox ID="ddl_NUM_Formato" runat="server" ValueType="System.String" SelectedIndex="0"
                                                    ClientInstanceName="ddl_NUM_Formato">
                                                    <Items>
                                                        <dxe:ListEditItem Text="N&#250;mero" Value="N" Selected="True" />
                                                        <dxe:ListEditItem Text="Moeda" Value="M" />
                                                        <dxe:ListEditItem Text="Percentual" Value="P" />
                                                    </Items>
                                                </dxe:ASPxComboBox>
                                                <table>
                                                    <tr>
                                                        <td align="right" style="height: 19px">
                                                            <dxe:ASPxButton ID="btnSalvar_NUM" runat="server" 
                                                                CssPostfix="Aqua" Text="Salvar" OnClick="btnSalvar_VAR_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('NUM');
}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                    <dxp:ASPxPanel ID="dvDAT" runat="server" Visible="False" Width="100%">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="Incluir Hora:">
                                                </dxe:ASPxLabel>
                                                <br />
                                                <table>
                                                    <tr>
                                                        <td style="width: 50px">
                                                            <dxe:ASPxRadioButton ID="rb_DAT_Sim" runat="server" GroupName="Hora" Text="Sim" ClientInstanceName="rb_DAT_Sim">
                                                            </dxe:ASPxRadioButton>
                                                        </td>
                                                        <td style="width: 50px">
                                                            <dxe:ASPxRadioButton ID="rb_DAT_Nao" runat="server" GroupName="Hora" Text="N&#227;o"
                                                                ClientInstanceName="rb_DAT_Nao">
                                                            </dxe:ASPxRadioButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="Valor inicial:">
                                                </dxe:ASPxLabel>
                                                <br />
                                                <dxe:ASPxRadioButton ID="rb_DAT_Branco" runat="server" GroupName="Inicial" Text="Em branco"
                                                    ClientInstanceName="rb_DAT_Branco">
                                                </dxe:ASPxRadioButton>
                                                <dxe:ASPxRadioButton ID="rb_DAT_Atual" runat="server" GroupName="Inicial" Text="Data/Hora Atual"
                                                    ClientInstanceName="rb_DAT_Atual">
                                                </dxe:ASPxRadioButton>
                                                <table>
                                                    <tr>
                                                        <td align="right" style="height: 19px">
                                                            <dxe:ASPxButton ID="btnSalvar_DAT" runat="server" 
                                                                CssPostfix="Aqua" Text="Salvar" OnClick="btnSalvar_VAR_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('DAT');
}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                    <dxp:ASPxPanel ID="dvLST" runat="server" Visible="False" Width="100%">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="Lista de op&#231;&#245;es:">
                                                </dxe:ASPxLabel>
                                                <dxe:ASPxMemo ID="txt_LST_Opcoes" runat="server" Height="71px" Width="350px" ClientInstanceName="txt_LST_Opcoes">
                                                </dxe:ASPxMemo>
                                                <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Text="Apresentar utilizando:">
                                                </dxe:ASPxLabel>
                                                <br />
                                                <dxe:ASPxRadioButton ID="rb_LST_Combo" runat="server" Text="Lista suspensa" ClientInstanceName="rb_LST_Combo" GroupName="tipoLista">
                                                </dxe:ASPxRadioButton>
                                                <dxe:ASPxRadioButton ID="rb_LST_Radio" runat="server" Text="Op&#231;&#245;es sele&#231;&#227;o simples"
                                                    ClientInstanceName="rb_LST_Radio" GroupName="tipoLista">
                                                </dxe:ASPxRadioButton>
                                                <dxe:ASPxRadioButton ID="rb_LST_Check" runat="server" Text="Op&#231;&#245;es sele&#231;&#227;o m&#250;ltipla"
                                                    ClientInstanceName="rb_LST_Check" GroupName="tipoLista">
                                                </dxe:ASPxRadioButton>
                                                <table>
                                                    <tr>
                                                        <td align="right" style="height: 19px">
                                                            <dxe:ASPxButton ID="btnSalvar_LST" runat="server" 
                                                                CssPostfix="Aqua" Text="Salvar" OnClick="btnSalvar_VAR_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('LST');
}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                    <dxp:ASPxPanel ID="dvBOL" runat="server" Width="100%" Visible="False">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 320px">
                                                    <tr>
                                                        <td align="center" colspan="3">
                                                            <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Text="Situa&#231;&#227;o Verdadeiro">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px">
                                                            <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="Texto">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 150px">
                                                            &nbsp;<dxe:ASPxLabel ID="ASPxLabel18" runat="server" Text="Valor">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px">
                                                            <dxe:ASPxTextBox ID="txt_BOL_TextoVerdadeiro" runat="server" Width="100px" ClientInstanceName="txt_BOL_TextoVerdadeiro">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 150px">
                                                            <dxe:ASPxTextBox ID="txt_BOL_ValorVerdadeiro" runat="server" Width="100px" ClientInstanceName="txt_BOL_ValorVerdadeiro">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 320px">
                                                    <tr>
                                                        <td align="center" colspan="3">
                                                            <dxe:ASPxLabel ID="ASPxLabel15" runat="server" Text="Situa&#231;&#227;o Falso">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px">
                                                            <dxe:ASPxLabel ID="ASPxLabel16" runat="server" Text="Texto">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 150px">
                                                            &nbsp;<dxe:ASPxLabel ID="ASPxLabel17" runat="server" Text="Valor">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px">
                                                            <dxe:ASPxTextBox ID="txt_BOL_TextoFalso" runat="server" Width="100px" ClientInstanceName="txt_BOL_TextoFalso">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 150px">
                                                            <dxe:ASPxTextBox ID="txt_BOL_ValorFalso" runat="server" Width="100px" ClientInstanceName="txt_BOL_ValorFalso">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td align="right" style="height: 19px">
                                                            <dxe:ASPxButton ID="btnSalvar_BOL" runat="server" 
                                                                CssPostfix="Aqua" Text="Salvar" OnClick="btnSalvar_VAR_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('BOL');
}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                    <dxp:ASPxPanel ID="dvSUB" runat="server" Width="100%" Visible="False">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Text="Nome do subformul&#225;rio:">
                                                </dxe:ASPxLabel>
                                                <br />
                                                <dxe:ASPxComboBox ID="ddl_SUB_Formulario" runat="server" TextField="nomeFormulario" ClientInstanceName="ddl_SUB_Formulario"
                                                    ValueField="codigoModeloFormulario" ValueType="System.Int32" Width="320px">
                                                </dxe:ASPxComboBox>
                                                <table>
                                                    <tr>
                                                        <td align="right" style="height: 19px">
                                                            <dxe:ASPxButton ID="btnSalvar_SUB" runat="server" 
                                                                CssPostfix="Aqua" Text="Salvar" OnClick="btnSalvar_VAR_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('SUB');
}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                    <dxp:ASPxPanel ID="dvCPD" runat="server" Visible="False" Width="100%">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <dxe:ASPxLabel ID="ASPxLabel25" runat="server" Text="Nome do campo pr&#233;-definido:">
                                                </dxe:ASPxLabel>
                                                <dxe:ASPxComboBox ID="ddl_CPD_CampoPre" runat="server" TextField="Descricao" ValueField="Codigo" ClientInstanceName="ddl_CPD_CampoPre"
                                                    ValueType="System.Int32" Width="320px">
                                                </dxe:ASPxComboBox>
                                                <table>
                                                    <tr>
                                                        <td align="right" style="height: 19px">
                                                            <dxe:ASPxButton ID="btnSalvar_CPD" runat="server" 
                                                                CssPostfix="Aqua" Text="Salvar" OnClick="btnSalvar_VAR_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('CPD');
}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                    <!--FIM-->
                                    <dxp:ASPxPanel ID="dvLOO" runat="server" Visible="False" Width="100%">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <dxe:ASPxLabel ID="ASPxLabel26" runat="server" Text="Nome da lista pr&#233;-definida:">
                                                </dxe:ASPxLabel>
                                                <br />
                                                <dxe:ASPxComboBox ID="ddl_LOO_ListaPre" runat="server" TextField="Descricao" ValueField="Codigo" ClientInstanceName="ddl_LOO_ListaPre"
                                                    ValueType="System.Int32" Width="320px">
                                                </dxe:ASPxComboBox>
                                                <table>
                                                    <tr>
                                                        <td align="right" style="height: 19px">
                                                            <dxe:ASPxButton ID="btnSalvar_LOO" runat="server" 
                                                                CssPostfix="Aqua" Text="Salvar" OnClick="btnSalvar_VAR_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	obtemValoresClientes('LOO');
}" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </DetailRow>
                    </Templates>
                    <Styles>
                        <FocusedRow BackColor="#FFFFC0">
                        </FocusedRow>
                    </Styles>
                    <SettingsLoadingPanel Text="" />
                    <SettingsPager Mode="ShowAllRecords">
                        <AllButton>
                            <Image Height="19px" Width="27px" />
                        </AllButton>
                        <FirstPageButton>
                            <Image Height="19px" Width="23px" />
                        </FirstPageButton>
                        <LastPageButton>
                            <Image Height="19px" Width="23px" />
                        </LastPageButton>
                        <NextPageButton>
                            <Image Height="19px" Width="19px" />
                        </NextPageButton>
                        <PrevPageButton>
                            <Image Height="19px" Width="19px" />
                        </PrevPageButton>
                    </SettingsPager>
                    <Images>
                        <CollapsedButton Height="15px" 
                            Width="15px" />
                        <ExpandedButton Height="15px" 
                            Width="15px" />
                        <DetailCollapsedButton Height="15px" 
                            Width="15px" />
                        <DetailExpandedButton Height="15px"
                            Width="15px" />
                        <HeaderFilter Height="19px"  Width="19px" />
                        <HeaderActiveFilter Height="19px" 
                            Width="19px" />
                        <HeaderSortDown Height="5px" 
                            Width="7px" />
                        <HeaderSortUp Height="5px" Width="7px" />
                        <FilterRowButton Height="13px" Width="13px" />
                        <WindowResizer Height="13px" Width="13px" />
                    </Images>
                    <Columns>
                        <dxwgv:GridViewCommandColumn VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true">
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="codigoCampo" Visible="False" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Nome do Campo" FieldName="NomeCampo" VisibleIndex="1">
                            <PropertiesTextEdit MaxLength="60" Width="600px">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" Caption="Nome:" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Descri&#231;&#227;o do campo" FieldName="DescricaoCampo"
                            Visible="False" VisibleIndex="1">
                            <PropertiesTextEdit MaxLength="250" Width="600px">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="True" Caption="Descri&#231;&#227;o:" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="Obrigat&#243;rio" FieldName="CampoObrigatorio"
                            VisibleIndex="2">
                            <PropertiesComboBox ValueType="System.String">
                                <Items>
                                    <dxe:ListEditItem Text="Sim" Value="S">
                                    </dxe:ListEditItem>
                                    <dxe:ListEditItem Text="N&#227;o" Value="N">
                                    </dxe:ListEditItem>
                                </Items>
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" />
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="OrdemCampoFormulario" Visible="False" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoTipoCampo" VisibleIndex="3" Caption="Tipo do Campo">
                            <PropertiesComboBox DataSourceID="dsTipoCampo" TextField="DescricaoCampoUsuario"
                                ValueField="CodigoTipoCampo" ValueType="System.String">
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" Caption="Tipo:" />
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn Caption="DefinicaoCampo" FieldName="DefinicaoCampo"
                            Visible="False" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CodigoLookup" Visible="False" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="Aba" FieldName="Aba" Name="colAba" VisibleIndex="4">
                            <PropertiesComboBox ValueType="System.String">
                                <Items>
                                    <dxe:ListEditItem Text="Primeira Aba" Value="1">
                                    </dxe:ListEditItem>
                                    <dxe:ListEditItem Text="Segunda Aba" Value="2">
                                    </dxe:ListEditItem>
                                </Items>
                            </PropertiesComboBox>
                        </dxwgv:GridViewDataComboBoxColumn>
                    </Columns>
                    <StylesEditors>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                    <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />
                    <ImagesEditors>
                        <CalendarFastNavPrevYear Height="19px" 
                            Width="19px" />
                        <CalendarFastNavNextYear Height="19px" 
                            Width="19px" />
                        <DropDownEditDropDown Height="7px" 
                             
                            Width="9px" />
                        <SpinEditIncrement Height="6px" 
                            Width="7px" />
                        <SpinEditDecrement Height="7px" 
                            Width="7px" />
                        <SpinEditLargeIncrement Height="9px" 
                            Width="7px" />
                        <SpinEditLargeDecrement Height="9px"
                            Width="7px" />
                    </ImagesEditors>
                    <SettingsEditing EditFormColumnCount="1" />
                    <SettingsPopup>
                    <EditForm  Modal="true"/>
                    </SettingsPopup>
                    <Settings VerticalScrollBarMode="Visible" />
                    <SettingsBehavior AllowFocusedRow="True" />
                </dxwgv:ASPxGridView>
            </DetailRow>
         
        </Templates>
        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />
        <SettingsBehavior AllowFocusedRow="True" />
    </dxwgv:ASPxGridView>
    <asp:HiddenField ID="hfIndexFormulario" runat="server" />
    <asp:SqlDataSource ID="dsTipoCampo" runat="server" SelectCommand="SELECT * FROM [TipoCampo] order by DescricaoCampoUsuario">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsTipoFormulario" runat="server" SelectCommand="SELECT * FROM [TipoFormulario] order by DescricaoTipoFormulario">
    </asp:SqlDataSource>
    <br />
</asp:Content>

