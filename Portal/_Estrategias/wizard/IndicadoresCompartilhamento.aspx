<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndicadoresCompartilhamento.aspx.cs" Inherits="_Estrategias_wizard_IndicadoresCompartilhamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../estilos/custom.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            height: 10px;
        }

        .style3 {
            height: 15px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var codigoUnidadeEdicao = null;

        function mostraDivSalvoPublicado(acao) {
            if (acao.toUpperCase().indexOf('SUCESSO'))
                window.top.mostraMensagem(acao, 'sucesso', false, false, null);
            else
                window.top.mostraMensagem(acao, 'erro', true, false, null);

        }

        function MontaCamposFormulario(values) {

            var codigoUnidade = (values[0] != null ? values[0] : "");
            var descricao = (values[1] != null ? values[1] : "");
            var codigoResponsavel = values[2];
            var CodigoUsuarioResponsavelResultado = values[3];
            var nomeRespResultado = values[4];

            ddlUnidades.SetText(descricao);
            ddlResponsavel.SetValue(codigoResponsavel);
            ddlResponsavel.SetEnabled(true);
            abrePopupEdicao(codigoUnidade);
            (CodigoUsuarioResponsavelResultado == null ? ddlResponsavelResultado.SetText("") : ddlResponsavelResultado.SetValue(CodigoUsuarioResponsavelResultado));
            (CodigoUsuarioResponsavelResultado == null ? ddlResponsavelResultado.SetText("") : ddlResponsavelResultado.SetText(nomeRespResultado));
            ddlResponsavelResultado.SetEnabled(true);
        }

        function abrePopupEdicao(codigoUnidade) {

            codigoUnidadeEdicao = codigoUnidade;

            ddlUnidades.SetEnabled(codigoUnidade == null);

            pcDados.Show();
        }

        function habilitaDesabilitaResponsavel() {

            if (ddlUnidades.GetSelectedItem() != null && ddlUnidades.GetSelectedItem().imageUrl.indexOf('Entidade') != -1) {
                ddlResponsavel.SetValue(null);
                ddlResponsavel.SetEnabled(false);

                ddlResponsavelResultado.SetValue(null);
                ddlResponsavelResultado.SetEnabled(false);
            }
            else {
                ddlResponsavel.SetEnabled(true);
                ddlResponsavelResultado.SetEnabled(true);
            }
        }

        function verificaCampos() {
            if (ddlUnidades.GetValue() == null)
                window.top.mostraMensagem(traducao.IndicadoresCompartilhamento_o_campo_ + '"' + lblUnidade.GetText() + '"' + traducao.IndicadoresCompartilhamento____de_preenchimento_obrigat_rio_, 'atencao', true, false, null);
            else if (codigoUnidadeEdicao != null && (ddlResponsavel.GetValue() == null || ddlResponsavel.GetValue() + "" == "-1")) {
                window.top.mostraMensagem(traducao.IndicadoresCompartilhamento_o_campo__respons_vel_pelo_indicador____de_preenchimento_obrigat_rio_, 'atencao', true, false, null);
            } else if (codigoUnidadeEdicao != null && ddlResponsavel.GetValue() != null && (ddlResponsavelResultado.GetValue() == null || ddlResponsavelResultado.GetValue() + "" == "-1")) {
                window.top.mostraMensagem(traducao.IndicadoresCompartilhamento_o_campo__respons_vel_pela_atualiza__o_do_resultado____de_preenchimento_obrigat_rio_, 'atencao', true, false, null);
            }
            else {
                if (codigoUnidadeEdicao == null)
                    gvDados.PerformCallback('S');
                else
                    gvDados.PerformCallback('E;' + codigoUnidadeEdicao);
            }
        }
    </script>
</head>
<body style="padding: 0px">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <td>
                        <dxe:ASPxLabel runat="server" Text="Indicador:" ID="ASPxLabel1011"></dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox runat="server" Width="100%" MaxLength="30" ReadOnly="True" ClientInstanceName="txtNomeIndicador" ID="txtNomeIndicador">
                            <ValidationSettings>
                                <ErrorImage Height="14px" Url="~/App_Themes/Aqua/Editors/edtError.png"></ErrorImage>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="Gainsboro" ForeColor="#404040"></ReadOnlyStyle>
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2"></td>
                </tr>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False"
                            ClientInstanceName="gvDados"
                            Width="100%" OnCustomCallback="gvDados_CustomCallback"
                            KeyFieldName="CodigoUnidadeNegocio"
                            OnCustomButtonInitialize="gvDados_CustomButtonInitialize">
                            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_FechaEdicao == 'S')
		pcDados.Hide();

	if(s.cp_Msg != '')
		mostraDivSalvoPublicado(s.cp_Msg);
	if(s.cp_AtualizarCombo == 'S')
	{
        ddlResponsavel.SetValue(null);
        ddlResponsavelResultado.SetValue(null);
		ddlUnidades.SetValue(null);
		//ddlUnidades.PerformCallback('A');
	}
}"
                                CustomButtonClick="function(s, e) {
	if(e.buttonID == 'btnEditar')
     {		
		s.GetRowValues(e.visibleIndex, 'CodigoUnidadeNegocio;NomeUnidadeNegocio;CodigoResponsavel;CodigoResponsavelAtualizacaoIndicador;NomeUsuarioResponsavelResultado;', MontaCamposFormulario);		
     }
     else if(e.buttonID == 'btnExcluir')
     {
        var funcObj = { funcaoClickOK: function(s, e){ gvDados.PerformCallback('X;' + s.GetRowKey(e.visibleIndex)); } }
	    window.top.mostraConfirmacao('Deseja excluir o compartilhamento?', function(){funcObj['funcaoClickOK'](s, e)}, null);
     }
	
}" />
                            <Columns>
                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" Caption=" " VisibleIndex="0"
                                    Width="80px">
                                    <CustomButtons>
                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar"
                                            Text="Editar Responsável">
                                            <Image ToolTip="Editar Responsável" Url="~/imagens/botoes/editarReg02.PNG">
                                            </Image>
                                        </dxwgv:GridViewCommandColumnCustomButton>
                                        <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir"
                                            Text="Excluir Compartilhamento">
                                            <Image ToolTip="Excluir Compartilhamento"
                                                Url="~/imagens/botoes/excluirReg02.PNG">
                                            </Image>
                                        </dxwgv:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <dxm:ASPxMenu ID="menu" runat="server" BackColor="Transparent"
                                                        ClientInstanceName="menu"
                                                        ItemSpacing="5px" OnItemClick="menu_ItemClick"
                                                        OnInit="menu_Init">
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

                                    </HeaderTemplate>
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Caption=" " FieldName="Tipo" VisibleIndex="1"
                                    Width="45px">
                                    <PropertiesTextEdit DisplayFormatString="&lt;img src='../../imagens/{0}.png' /&gt;">
                                    </PropertiesTextEdit>
                                    <Settings AllowAutoFilter="False" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Entidade/Unidade"
                                    FieldName="NomeUnidadeNegocio" VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Responsável pelo Indicador"
                                    FieldName="Responsavel" VisibleIndex="3" Width="220px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="NomeUsuarioResponsavelResultado"
                                    Visible="False" VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="CodigoResponsavelAtualizacaoIndicador"
                                    Visible="False" VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings ShowFooter="True" VerticalScrollBarMode="Visible"
                                VerticalScrollableHeight="330" />
                            <Templates>
                                <FooterRow>
                                    <table class="grid-legendas">
                                        <tr>
                                            <td class="grid-legendas-icone">
                                                <dxe:ASPxImage ID="ASPxImage1" runat="server"
                                                    ImageUrl="~/imagens/entidadeMenor.png" Height="16px">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td class="grid-legendas-label">
                                                <dxe:ASPxLabel ID="ASPxLabel1012" runat="server"
                                                    Text='<%#definicaoEntidadeSingular %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td class="grid-legendas-icone">
                                                <dxe:ASPxImage ID="ASPxImage2" runat="server"
                                                    ImageUrl="~/imagens/UnidadeMenor.png" Height="16px">
                                                </dxe:ASPxImage>
                                            </td>
                                            <td class="grid-legendas-label">
                                                <dxe:ASPxLabel ID="ASPxLabel1013" runat="server"
                                                    Text='<%# definicaoUnidadeSingular %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterRow>
                            </Templates>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td class="style2"></td>
                </tr>
                <tr>
                    <td align="right">
                        <dxe:ASPxButton ID="ASPxButton1" runat="server"
                            Text="Fechar" Width="100px">
                            <ClientSideEvents Click="function(s, e) {
	window.top.fechaModal();
}" />
                            <Paddings Padding="0px" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>

            <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" CloseAction="None"
                ClientInstanceName="pcDados" HeaderText="Detalhes" ShowCloseButton="False"
                Width="620px" ID="pcDados">
                <ClientSideEvents CloseUp="function(s, e) {
	ddlResponsavel.SetValue(null);
    ddlResponsavelResultado.SetValue(null);
	ddlUnidades.SetValue(null);
    habilitaDesabilitaResponsavel();
}" />
                <ContentStyle>
                    <Paddings Padding="5px"></Paddings>
                </ContentStyle>
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <table class="formulario" width="100%">
                            <tr class="formulario-linha">
                                <td class="formulario-label">
                                    <dxe:ASPxLabel runat="server" Text="Unidade:"
                                        ID="lblUnidade" ClientInstanceName="lblUnidade">
                                    </dxe:ASPxLabel>

                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td>
                                    <dxe:ASPxComboBox ID="ddlUnidades" runat="server"
                                        ClientInstanceName="ddlUnidades"
                                        Width="100%" ImageUrlField="Tipo" OnCallback="ddlUnidades_Callback">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	habilitaDesabilitaResponsavel();
}"
                                            EndCallback="function(s, e) {
	s.SetValue(null);
}" />
                                    </dxe:ASPxComboBox>

                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td class="formulario-label">
                                    <dxe:ASPxLabel ID="lblResponsavel" runat="server"
                                        ClientInstanceName="lblResponsavel" Font-Bold="False"
                                        Text="Responsável pelo Indicador:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td>
                                    <dxe:ASPxComboBox ID="ddlResponsavel" runat="server"
                                        ClientInstanceName="ddlResponsavel" DropDownStyle="DropDown"
                                        EnableCallbackMode="True" Font-Bold="False"
                                        IncrementalFilteringMode="Contains"
                                        OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue"
                                        OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition"
                                        TextFormatString="{0}" ValueType="System.Int32" Width="100%">
                                        <Columns>
                                            <dxe:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="300px" />
                                            <dxe:ListBoxColumn Caption="Email" FieldName="EMail" Width="200px" />
                                        </Columns>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td class="formulario-label">
                                    <dxe:ASPxLabel ID="lblResponsavelIndicador0" runat="server"
                                        Text="Responsável pela Atualização do Resultado:">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td>
                                    <dxe:ASPxComboBox ID="ddlResponsavelResultado" runat="server"
                                        ClientInstanceName="ddlResponsavelResultado" EnableCallbackMode="True"
                                        IncrementalFilteringMode="Contains"
                                        OnItemRequestedByValue="ddlResponsavel_ItemRequestedByValue"
                                        OnItemsRequestedByFilterCondition="ddlResponsavel_ItemsRequestedByFilterCondition"
                                        ValueType="System.Int32" Width="100%" DropDownStyle="DropDown"
                                        Font-Bold="False" TextFormatString="{0}">
                                        <Columns>
                                            <dxe:ListBoxColumn Caption="Nome" Width="300px" FieldName="NomeUsuario" />
                                            <dxe:ListBoxColumn Caption="Email" Width="200px" FieldName="EMail" />
                                        </Columns>
                                        <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                        </DisabledStyle>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr class="formulario-linha">
                                <td align="right">
                                    <table class="formulario-botoes" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False"
                                                        ClientInstanceName="btnSalvar" Text="Salvar" Width="90px"
                                                        ID="btnSalvar">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    verificaCampos();
}	"></ClientSideEvents>
                                                        <Paddings Padding="0px"></Paddings>
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td>
                                                    <dxe:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnFechar" Text="Fechar" Width="90px" ID="btnFechar">
                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
    pcDados.Hide();
}"></ClientSideEvents>
                                                        <Paddings Padding="0px" PaddingLeft="0px" PaddingTop="0px" PaddingRight="0px" PaddingBottom="0px"></Paddings>
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

            <dxwgv:ASPxGridViewExporter runat="server" GridViewID="gvDados"
                ID="ASPxGridViewExporter1"
                OnRenderBrick="ASPxGridViewExporter1_RenderBrick">
                <Styles>
                    <Default></Default>
                    <Header></Header>
                    <Cell></Cell>
                    <GroupFooter Font-Bold="True"></GroupFooter>
                    <Title Font-Bold="True"></Title>
                </Styles>
            </dxwgv:ASPxGridViewExporter>
            <dxpc:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUsuarioIncluido"
                HeaderText="Incluir a Entidad Atual" ShowCloseButton="False" ShowHeader="False"
                Width="450px" ID="pcUsuarioIncluido">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
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

        </div>
    </form>
</body>
</html>
