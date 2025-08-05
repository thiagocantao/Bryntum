<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CadastroRDO.aspx.cs" Inherits="_Projetos_DadosProjeto_CadastroRDO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 255px;
        }
        .style3
        {
            height: 10px;
        }
        .style4
        {
            width: 630px;
        }
        .style5
        {
            width: 70px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function Trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        function validaCamposFormulario() {
            // Esta função tem que retornar uma string.
            // "" se todas as validações estiverem OK
            // "<erro>" indicando o que deve ser corrigido
            mensagemErro_ValidaCamposFormulario = "";
            var numAux = 0;
            var mensagem = "";

            if (Trim(txtItem.GetText()) == "") {
                numAux++;
                
                if (rbGrupo.GetValue() == 'MOB')
                    mensagem += "\n" + numAux + ") O cargo/função deve ser informado.";
                else
                    mensagem += "\n" + numAux + ") A descrição deve ser informada.";

                
            }

            if (ddlTipo.GetValue() == null) {
                numAux++;

                mensagem += "\n" + numAux + ") O tipo deve ser informado.";
            }

            if (mensagem != "") {
                window.top.mostraMensagem(mensagem, 'atencao', true, false, null);
                txtItem.SetFocus();
                return false;
            }

            return true;
        }
    </script>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td valign="bottom">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Tipo:">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2" >
                                <dxe:ASPxRadioButtonList ID="rbGrupo" runat="server" 
                                    ClientInstanceName="rbGrupo"  Height="22px" 
                                    RepeatDirection="Horizontal" SelectedIndex="0" Width="100%">
                                    <Paddings Padding="0px" />
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(s.GetValue() == 'MOB')
		lblDescricao.SetText('Cargo/Função:');
	else
		lblDescricao.SetText('Descrição:');

	ddlTipo.PerformCallback('A');
}" />
                                    <Items>
                                        <dxe:ListEditItem Selected="True" Text="Equipamento" Value="EQP" />
                                        <dxe:ListEditItem Text="Mão de Obra" Value="MOB" />
                                    </Items>
                                </dxe:ASPxRadioButtonList>
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="ddlTipo" runat="server" 
                                    Height="22px" IncrementalFilteringMode="Contains" 
                                    oncallback="ddlTipo_Callback" Width="550px">
                                    <ClientSideEvents EndCallback="function(s, e) {
	gvDados.PerformCallback();
}" SelectedIndexChanged="function(s, e) {
	gvDados.PerformCallback();
}" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td class="style4" valign="bottom">
                                <dxe:ASPxLabel ID="lblDescricao" runat="server" 
                                    ClientInstanceName="lblDescricao"  
                                    Text="Descrição:">
                                </dxe:ASPxLabel>
                            </td>
                            <td class="style5" valign="bottom">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                    Text="Ordem:">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style4" >
                                <dxe:ASPxTextBox ID="txtItem" runat="server" ClientInstanceName="txtItem" 
                                     Height="22px" MaxLength="132" Width="100%">
                                </dxe:ASPxTextBox>
                            </td>
                            <td class="style5" >
                                <dxcp:ASPxCallbackPanel ID="pnOrdem" runat="server" 
                                    ClientInstanceName="pnOrdem"  
                                     Width="100%" oncallback="pnOrdem_Callback">
                                    <Paddings Padding="0px" />
                                    <PanelCollection>
<dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dxe:ASPxTextBox ID="txtOrdem" runat="server" ClientInstanceName="txtOrdem" 
         Height="22px" Width="100%" 
        HorizontalAlign="Right">
        <MaskSettings Mask="&lt;0..100&gt;" />
        <ValidationSettings ErrorDisplayMode="None">
        </ValidationSettings>
    </dxe:ASPxTextBox>
                                        </dxp:PanelContent>
</PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnSalvar" runat="server" 
                                    Height="22px" Text="Salvar" Width="100px" 
                                    AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
	if(validaCamposFormulario())
		gvDados.PerformCallback('I');
}" />
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td>

 <dxwgv:ASPxGridView runat="server" ClientInstanceName="gvDados" KeyFieldName="CodigoItem" 
                        AutoGenerateColumns="False" Width="100%"  
                        ID="gvDados" 
                        OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize" 
                        oncelleditorinitialize="gvDados_CellEditorInitialize" 
                        oncustomcallback="gvDados_CustomCallback" 
                        onrowdeleting="gvDados_RowDeleting" onrowupdating="gvDados_RowUpdating" 
                        oncommandbuttoninitialize="gvDados_CommandButtonInitialize">

     <ClientSideEvents EndCallback="function(s, e) {
	txtItem.SetFocus();

	if(s.cp_Msg != '')
		window.top.mostraMensagem(s.cp_Msg, 'erro', true, false, null);

	if(s.cp_LimpaCampos == 'S')
	{
		txtItem.SetText('');
	}
	
	if(window.parent.houveMudanca)
		window.parent.houveMudanca = 'S';

	pnOrdem.PerformCallback('A');
}" />

<Columns>
<dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0"
                    Width="80px" ShowEditButton="true" ShowDeleteButton="true">
                </dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataTextColumn FieldName="NumeroOrdem" Caption="#" VisibleIndex="1" 
        Width="60px">
    <PropertiesTextEdit Width="100%">
        <ValidationSettings ErrorDisplayMode="None">
        </ValidationSettings>
    </PropertiesTextEdit>
    <Settings AllowAutoFilter="False" />
    <EditFormSettings Caption="Ordem:" VisibleIndex="1" />
    <EditCellStyle HorizontalAlign="Right">
    </EditCellStyle>
    <HeaderStyle HorizontalAlign="Center" />
    <CellStyle HorizontalAlign="Right">
    </CellStyle>
</dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="DescricaoItem" 
        Name="DescricaoItem" VisibleIndex="2">
        <PropertiesTextEdit Width="100%">
            <ValidationSettings ErrorDisplayMode="None">
                <RequiredField IsRequired="True" />
            </ValidationSettings>
        </PropertiesTextEdit>
        <Settings AutoFilterCondition="Contains" />
        <EditFormSettings Caption="Descrição:" VisibleIndex="0" />
    </dxwgv:GridViewDataTextColumn>
</Columns>

<SettingsBehavior AllowSort="False" AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>

<SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

     <SettingsEditing Mode="Inline" />

<Settings ShowFilterRow="True" VerticalScrollBarMode="Visible"></Settings>
     <SettingsText ConfirmDelete="Deseja excluir o item selecionado?" />
</dxwgv:ASPxGridView>

                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
