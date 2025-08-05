<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupProgramasDoProjeto.aspx.cs" Inherits="_Projetos_Administracao_popupProgramasDoProjeto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .iniciaisMaiusculas{
            text-transform:capitalize !important
        }
    </style>
    <script type="text/javascript">
        function verificarDadosPreenchidos() {
            var retorno = true;

            var numAux = 0;
            var mensagem = "";

            if (txtNomePrograma.GetText() === "") {
                numAux++;
                mensagem += "\n" + numAux + ") O nome do programa deve ser informado.";
            }
            if (ddlGerentePrograma.GetValue() === null) {
                numAux++;
                mensagem += "\n" + numAux + ") O gerente deve ser informado.";
            }
            if (ddlUnidadeNegocio.GetValue() === null) {
                numAux++;
                mensagem += "\n" + numAux + ") A unidade deve ser informada.";
            }
            if (mensagem !== "") {
                retorno = false;
                window.top.mostraMensagem(mensagem, 'atencao', true, false, null);
            }

            return retorno;
        }

        function abreSelecaoProjeto() {
            window.top.showModal2("./ListagemDeProjetos.aspx", 'Selecionar projeto', 840, 400, processaRetornoTela, null);
        }

        function processaRetornoTela(retornoTela) {
            if (retornoTela !== null && retornoTela !== '') {
                //hfGeral.Set("CodigoProjeto", retornoTela[0]);
                txtNomePrograma.SetText(retornoTela[2]);
                ddlGerentePrograma.SetValue(retornoTela[3]);
                //alert(retornoTela[0]);
                gvProjetos.PerformCallback(retornoTela[0]);

                //TipoOperacao = "Editar";
                //hfGeral.Set("TipoOperacao", TipoOperacao);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td>
                            <!-- table -->
                            <table cellspacing="0" cellpadding="2" width="100%" border="0" style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td valign="bottom" style="width: 300px">
                                            <dxcp:ASPxLabel runat="server" Text="Programa:" ClientInstanceName="lblPrograma" ID="lblPrograma"></dxcp:ASPxLabel>
                                        </td>
                                        <td valign="bottom">
                            <dxcp:ASPxLabel runat="server" Text="Unidade:" ClientInstanceName="lblUnidade" ID="lblUnidade"></dxcp:ASPxLabel>


                                        </td>
                                        <td valign="bottom" style="width: 350px">
                                            <dxcp:ASPxLabel runat="server" Text="Gerente:" ClientInstanceName="lblGerente" ID="lblGerente"></dxcp:ASPxLabel>


                                        </td>
                                        <td valign="bottom" style="width: 100%" id="tdProjToProg" runat="server">
                                            <dxcp:ASPxLabel runat="server" Text="Transformar projeto em programa." ClientInstanceName="lbltransformarprojetoEmprograma" ID="lbltransformarprojetoEmprograma"></dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 300px">
                                            <dxcp:ASPxTextBox runat="server" Width="100%" MaxLength="255" ClientInstanceName="txtNomePrograma" ID="txtNomePrograma">
                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </ReadOnlyStyle>
                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                            </dxcp:ASPxTextBox>


                                        </td>
                                        <td>

                            <dxcp:ASPxComboBox runat="server" ValueType="System.Int32" NullValueItemDisplayText="{1}" TextFormatString="{1}" Width="100%" ClientInstanceName="ddlUnidadeNegocio" ID="ddlUnidadeNegocio">
                                <Columns>
                                    <dxcp:ListBoxColumn FieldName="SiglaUnidadeNegocio" Width="140px" Caption="Sigla"></dxcp:ListBoxColumn>
                                    <dxcp:ListBoxColumn FieldName="NomeUnidadeNegocio" Width="300px" Caption="Nome"></dxcp:ListBoxColumn>
                                </Columns>

                                <SettingsLoadingPanel Text="Carregando;"></SettingsLoadingPanel>

                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                </ReadOnlyStyle>

                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                            </dxcp:ASPxComboBox>




                                        </td>
                                        <td style="width: 350px">
                                            <dxcp:ASPxCallbackPanel ID="callbackGerenteProjeto" runat="server" ClientInstanceName="callbackGerenteProjeto" OnCallback="callbackGerenteProjeto_Callback" Width="100%">
                                                <ClientSideEvents EndCallback="function(s, e) {
ddlGerentePrograma.SetValue(s.cpCodigoGerente);
ddlGerentePrograma.SetText(s.cpNomeGerente);
}" />
                                                <PanelCollection>
                                                    <dxcp:PanelContent runat="server">
                                                        <dxtv:ASPxComboBox ID="ddlGerentePrograma" runat="server" ClientInstanceName="ddlGerentePrograma" DropDownStyle="DropDown" EnableCallbackMode="True" NullValueItemDisplayText="{0}" OnItemRequestedByValue="ddlGerentePrograma_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlGerentePrograma_ItemsRequestedByFilterCondition" TextField="NomeUsuario" TextFormatString="{0}" ValueField="CodigoUsuario" ValueType="System.Int32" Width="100%">
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	
}" />
                                                            <Columns>
                                                                <dxtv:ListBoxColumn Caption="Nome" FieldName="NomeUsuario" Width="300px">
                                                                </dxtv:ListBoxColumn>
                                                                <dxtv:ListBoxColumn Caption="Email" FieldName="EMail" Width="200px">
                                                                </dxtv:ListBoxColumn>
                                                            </Columns>
                                                            <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </ReadOnlyStyle>
                                                            <DisabledStyle BackColor="#EBEBEB" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dxtv:ASPxComboBox>
                                                    </dxcp:PanelContent>
                                                </PanelCollection>
                                            </dxcp:ASPxCallbackPanel>
                                        </td>
                                        <td style="width: 310px" id="tdProjToProg1" runat="server">
                                            <dxcp:ASPxComboBox runat="server" DropDownStyle="DropDown" ValueType="System.Int32" NullValueItemDisplayText="{0}" EnableCallbackMode="True" TextField="NomeUsuario" ValueField="CodigoUsuario" TextFormatString="{0}" Width="100%" ClientInstanceName="ddlTransformaProjetoEmPrograma" ID="ddlTransformaProjetoEmPrograma" OnItemRequestedByValue="ddlTransformaProjetoEmPrograma_ItemRequestedByValue" OnItemsRequestedByFilterCondition="ddlTransformaProjetoEmPrograma_ItemsRequestedByFilterCondition">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	txtNomePrograma.SetText(s.GetText());
               callbackGerenteProjeto.PerformCallback(s.GetValue());
}"></ClientSideEvents>

                                                <ReadOnlyStyle BackColor="#EBEBEB" ForeColor="Black">
                                                </ReadOnlyStyle>

                                                <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                            </dxcp:ASPxComboBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxcp:ASPxGridView runat="server" AutoGenerateColumns="False" KeyFieldName="CodigoProjeto" ClientInstanceName="gvProjetos" Width="100%" ID="gvProjetos" OnCustomCallback="gvProjetos_CustomCallback">
                                <ClientSideEvents Init="function(s, e) {
	    var height = Math.max(0, document.documentElement.clientHeight) - 140;
    s.SetHeight(height);
}" />
                                <Templates>
                                    <GroupRowContent>
                                        <%# Container.GroupText == "0" ? Resources.traducao.programasDoProjetos_selecionados : Resources.traducao.programasDoProjetos_dispon_veis %>
                                    </GroupRowContent>
                                </Templates>

                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                                <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto"></Settings>

                                <SettingsDataSecurity AllowInsert="False" AllowEdit="False" AllowDelete="False"></SettingsDataSecurity>

                                <SettingsPopup>
                                    <HeaderFilter MinHeight="140px"></HeaderFilter>
                                </SettingsPopup>
                                <Columns>
                                    <dxcp:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" ShowInCustomizationForm="True" Width="40px" Caption=" " VisibleIndex="0"></dxcp:GridViewCommandColumn>
                                    <dxcp:GridViewDataTextColumn FieldName="NomeProjeto" ShowInCustomizationForm="True" Caption="Projeto" VisibleIndex="2">
                                        <PropertiesTextEdit EncodeHtml="False"></PropertiesTextEdit>

                                        <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>
                                    </dxcp:GridViewDataTextColumn>
                                    <dxcp:GridViewDataTextColumn FieldName="Selecionado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3"></dxcp:GridViewDataTextColumn>
                                    <dxcp:GridViewDataTextColumn FieldName="ColunaAgrupamento" GroupIndex="0" SortIndex="0" SortOrder="Ascending" ShowInCustomizationForm="True" Caption=" " VisibleIndex="1"></dxcp:GridViewDataTextColumn>
                                </Columns>
                            </dxcp:ASPxGridView>


                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 20px">

                                        <img border='0' src='../../imagens/projeto.PNG' title='<%# Resources.traducao.programasDoProjetos_projeto %>' />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" Text="<%$ Resources:traducao, programasDoProjetos_projeto %>" ID="Literal1"></asp:Literal>


                                    </td>

                                    <td style="width: 20px">
                                        <img border='0' src='../../imagens/processo.PNG' style='width: 21px; height: 18px;' title='<%# Resources.traducao.programasDoProjetos_processo %>' />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" Text="<%$ Resources:traducao, programasDoProjetos_processo %>" ID="Literal2"></asp:Literal>


                                    </td>
                                    <td style="width: 20px">
                                        <img border='0' src='../../imagens/agile.PNG' style='width: 21px; height: 18px;' title='<%# Resources.traducao.programasDoProjetos_projeto__gil %>' />
                                    </td>
                                    <td>
                                        <asp:Literal runat="server" Text="<%$ Resources:traducao, programasDoProjetos_projeto__gil %>" ID="Literal3"></asp:Literal>


                                    </td>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="width: 100px">
                                                        <dxcp:ASPxButton runat="server" AutoPostBack="False" ClientInstanceName="btnSalvar" Text="Salvar" Width="90px" Font-Names="Verdana" Font-Size="8pt" ID="btnSalvar" Style="margin: 10px" CssClass="iniciaisMaiusculas">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	if(verificarDadosPreenchidos())
	{
                          callbackTela.PerformCallback();
	}
}"></ClientSideEvents>
                                                        </dxcp:ASPxButton>


                                                    </td>
                                                    <td style="width: 10px"></td>
                                                    <td style="width: 120px">
                                                        <dxcp:ASPxButton runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="90px" Font-Names="Verdana" Font-Size="8pt" ID="btnCancelar" CssClass="iniciaisMaiusculas">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
window.top.fechaModal();
}"></ClientSideEvents>
                                                        </dxcp:ASPxButton>


                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>


                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:SqlDataSource ID="dsResponsavel" runat="server"></asp:SqlDataSource>
            <dxcp:ASPxCallback ID="callbackTela" runat="server" ClientInstanceName="callbackTela" OnCallback="callbackTela_Callback">
                <ClientSideEvents EndCallback="function(s, e) {
    if(s.cpErro != '')
    {
          window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
    }
    else
    {
          window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null,2700);
          window.top.fechaModal();
    }
}" />
            </dxcp:ASPxCallback>
        </div>
    </form>
</body>
</html>
