<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frameProposta_Criterios.aspx.cs"
    Inherits="espacoTrabalho_frameProposta_Criterios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <script type="text/javascript" language="javascript" src="../scripts/CDIS.js"></script>
    <title>Untitled Page</title>
    <script type="text/javascript">

        var existeConteudoCampoAlterado = false;

        function conteudoCampoAlterado() {
            if ((btnSalvar.GetVisible == undefined) || (btnSalvar.GetVisible() == false))
                existeConteudoCampoAlterado = true;
        }

        function processaOpcaoEscolhida(s, e, nomeGrid, valores, indexLinha, valoresDecimais) {
            conteudoCampoAlterado();
        }

        function calculaTotal(nomeGrid, valorAntigo, valorNovo) {
        }

        function validaGrids(grid) {
            if (ddlCategoria.GetValue() == null) {
                window.top.mostraMensagem("A escolha da categoria é obrigatória!", 'atencao', true, false, null);
                return false;
            }

            // valida Grid de Critérios Positivos
            var nomeGrid = gvCritPos.name + "_cell";
            var qtdeLinhas = gvCritPos.GetVisibleRowsOnPage();
            var opcoes = "";
            var valorCelula;
            var codigoOpcao;

            for (i = 0; i < qtdeLinhas; i++) {
                if (gvCritPos.IsDataRow(i)) {
                    //valorCelula = document.getElementById(nomeGrid + (i)+"_4_valor_" + i + "_I").value;
                    codigoOpcao = document.getElementById(nomeGrid + (i) + "_2_cbOpcao_" + (i) + "_VI").value;
                    if (codigoOpcao == null || codigoOpcao == "") {
                        window.top.mostraMensagem("É necessário escolher uma opção para cada critério.", 'atencao', true, false, null);
                        return false;
                    }
                    opcoes += codigoOpcao + ";";
                }
            }

            nomeGrid = gvCritNeg.name + "_cell";
            qtdeLinhas = gvCritNeg.GetVisibleRowsOnPage();
            for (i = 0; i < qtdeLinhas; i++) {
                if (gvCritNeg.IsDataRow(i)) {
                    //valorCelula = document.getElementById(nomeGrid + (i)+"_4_valor_" + i + "_I").value;
                    codigoOpcao = document.getElementById(nomeGrid + (i) + "_2_cbOpcao_" + (i) + "_VI").value;
                    if (codigoOpcao == null || codigoOpcao == "") {
                        window.top.mostraMensagem("É necessário escolher uma opção para cada critério.", 'atencao', true, false, null);
                        return false;
                    }
                    opcoes += codigoOpcao + ";";
                }
            }
            parametro = "SALVAR" + opcoes;
            callbackSalvar.PerformCallback(parametro);

            return true;
        }

        function verificaAvancoWorkflow() {
            if (callbackSalvar.cp_PodePassarFluxo == "N") {
                window.parent.parent.frmCriteriosPendente = "Sim";
                return false;
            }
            else {
                window.parent.parent.frmCriteriosPendente = "";
                return true;
            }
        }

        function funcaoCallbackSalvar() {
            validaGrids();
        }

        function funcaoCallbackFechar() {
            window.top.fechaModalComFooter();

        }

        function validaBotoesSalvarFechar(ehPopup) {
            if (ehPopup == 'True') {
                btnFechar.SetVisible(false);
                btnSalvar2.SetVisible(false);
            }
            else {
                btnFechar.SetVisible(false);
                btnSalvar2.SetVisible(true);
            }
        }

        document.addEventListener("DOMContentLoaded", function (event) {
            validaBotoesSalvarFechar(btnFechar.cpEhPopup);
        });
    </script>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .btn-Upp {
            text-transform: capitalize !important;
        }
    </style>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="97%">
            <tr>
                <td></td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Categoria:" Font-Bold="False"></dxe:ASPxLabel>
                </td>
                <td style="width: 15px"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxComboBox runat="server" IncrementalFilteringMode="Contains"
                                    TextFormatString="{1}" Width="100%"
                                    ClientInstanceName="ddlCategoria"
                                    ID="ddlCategoria">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {	
		callbackCategoria.PerformCallback();
}"></ClientSideEvents>
                                    <Columns>
                                        <dxe:ListBoxColumn FieldName="SiglaCategoria" Width="120px" Caption="Sigla"></dxe:ListBoxColumn>
                                        <dxe:ListBoxColumn FieldName="DescricaoCategoria" Width="250px"
                                            Caption="Descri&#231;&#227;o">
                                        </dxe:ListBoxColumn>
                                    </Columns>

                                    <DisabledStyle BackColor="#EBEBEB" ForeColor="Black"></DisabledStyle>
                                </dxe:ASPxComboBox>

                            </td>
                            <td></td>
                            <td style="width: 100px;">
                                <dxe:ASPxButton ID="btnSalvar" runat="server" Text="Salvar"
                                    Width="100%" CssClass="btn-Upp">
                                    <Paddings Padding="0px"></Paddings>
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	validaGrids();
}"></ClientSideEvents>
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 15px"></td>
            </tr>
            <tr style="height: 10px">
                <td></td>
                <td style="height: 10px"></td>
                <td style="width: 15px"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <dxwgv:ASPxGridView ID="gvCritPos" runat="server" AutoGenerateColumns="False"
                        DataSourceID="dsDados" OnCustomCallback="GridsCriterios_CustomCallback"
                        Width="960px" KeyFieldName="CodigoCriterioSelecao"
                        OnHtmlRowCreated="GridsCriterios_HtmlRowCreated"
                        ClientInstanceName="gvCritPos">
                        <SettingsLoadingPanel Mode="Disabled" />
                        <Templates>
                            <FooterRow>
                                <table style="width: 100%">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="display: none">Total:</td>
                                                            <td style="display: none">
                                                                <dxe:ASPxTextBox ID="txtTotal" runat="server" Width="100px" ReadOnly="True" HorizontalAlign="Right">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FooterRow>
                        </Templates>

                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" AutoExpandAllGroups="True"></SettingsBehavior>

                        <SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

                        <SettingsEditing Mode="Inline"></SettingsEditing>

                        <SettingsText></SettingsText>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoCriterioSelecao" ReadOnly="True" Caption="Crit&#233;rio" VisibleIndex="0" Width="160px">
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Peso" ReadOnly="True" Name="Peso"
                                Width="50px" Caption="Peso" VisibleIndex="1" Visible="False">
                                <PropertiesTextEdit DisplayFormatString="{0:p2}"></PropertiesTextEdit>
                                <Settings AllowSort="False" />
                                <EditFormSettings Visible="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="codigoOpcao" Width="230px" Caption="Op&#231;&#227;o Escolhida" VisibleIndex="2">
                                <PropertiesComboBox DataSourceID="dsOpcao" TextField="descricaoOpcao" ValueField="codigoOpcao" ValueType="System.String"></PropertiesComboBox>
                                <DataItemTemplate>
                                    <table cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cbOpcao" runat="server" Width="170px" ValueType="System.String" ClientInstanceName="cbOpcao">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	processaOpcaoEscolhida(s, e, 'gvCritPos');
}"></ClientSideEvents>

                                                        <DisabledStyle ForeColor="#404040"></DisabledStyle>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td style="width: 25px" align="center">
                                                    <dxe:ASPxImage ID="imgLegenda" runat="server" ImageUrl="~/imagens/ajuda.png">
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td align="center" style="width: 25px">
                                                    <dxe:ASPxImage ID="imgVotacao" runat="server"
                                                        ImageUrl="~/imagens/opCriterios.png">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxpc:ASPxPopupControl ID="pcLegenda" runat="server" Width="580px" ClientInstanceName="pcLegenda" PopupElementID="imgLegenda" HeaderText="Ajuda">
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <div id="divLegenda" style="width: 100%; height: 150px; overflow: auto"></div>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                        <HeaderStyle />
                                    </dxpc:ASPxPopupControl>
                                    <dxpc:ASPxPopupControl ID="pcVotacao" runat="server"
                                        ClientInstanceName="pcVotacao"
                                        HeaderText="Indicações do Critério" PopupElementID="imgVotacao" Width="580px">
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <div id="divVotacao" style="width: 100%; height: 150px; overflow: auto">
                                                </div>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                        <HeaderStyle />
                                    </dxpc:ASPxPopupControl>
                                </DataItemTemplate>
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoOpcaoCriterioSelecao" ReadOnly="True" Name="DescricaoOpcaoCriterioSelecao" Caption="Detalhes" Visible="False" VisibleIndex="6">
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox ID="txtDetalhe" runat="server" Width="170px" ReadOnly="True" ClientInstanceName="txtDetalhe">
                                        <Border BorderStyle="None" />
                                    </dxe:ASPxTextBox>
                                </DataItemTemplate>
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Valor" ReadOnly="True" Name="Valor"
                                Width="140px" Caption="Valor" VisibleIndex="3" Visible="False">
                                <PropertiesTextEdit DisplayFormatString="{0:p2}"></PropertiesTextEdit>
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox ID="txtValor" runat="server" Width="60px" ReadOnly="True" ClientInstanceName="txtValor" DisplayFormatString="{0:p2}">
                                        <Border BorderStyle="None"></Border>
                                    </dxe:ASPxTextBox>
                                </DataItemTemplate>

                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoCategoria" Visible="False" VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoCriterioSelecao" Name="CodigoCriterioSelecao" Visible="False" VisibleIndex="8"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoOpcaoCriterioSelecao" Visible="False" VisibleIndex="9">
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="ValorOpcaoCriterioSelecao" Visible="False" VisibleIndex="10">
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="NomeFatorPortfolio" ReadOnly="True" Caption="Fator" VisibleIndex="4" GroupIndex="0" SortIndex="0" SortOrder="Ascending">
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="NomeGrupo" ReadOnly="True" Caption="Grupo" VisibleIndex="5">
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>

                        <Settings ShowGroupPanel="True" ShowFooter="True"></Settings>
                    </dxwgv:ASPxGridView>
                </td>
                <td style="width: 15px"></td>
            </tr>
            <tr>
                <td></td>
                <td style="height: 10px"></td>
                <td style="width: 15px"></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <dxwgv:ASPxGridView ID="gvCritNeg" runat="server" AutoGenerateColumns="False"
                        DataSourceID="dsRiscos" KeyFieldName="CodigoCriterioSelecao"
                        OnCustomCallback="GridsCriterios_CustomCallback" OnHtmlRowCreated="GridsCriterios_HtmlRowCreated"
                        Width="960px" ClientInstanceName="gvCritNeg">
                        <SettingsLoadingPanel Mode="Disabled" />
                        <Templates>
                            <FooterRow>
                                <table style="width: 100%">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="display: none">Total:</td>
                                                            <td style="display: none">
                                                                <dxe:ASPxTextBox ID="txtTotal" runat="server" Width="100px" ReadOnly="True" HorizontalAlign="Right">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </FooterRow>
                        </Templates>

                        <SettingsBehavior AllowDragDrop="False" AllowSort="False" AutoExpandAllGroups="True"></SettingsBehavior>

                        <SettingsPager Mode="ShowAllRecords" Visible="False"></SettingsPager>

                        <SettingsEditing Mode="Inline"></SettingsEditing>

                        <SettingsText></SettingsText>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoCriterioSelecao" ReadOnly="True" Caption="Crit&#233;rio" VisibleIndex="0" Width="160px">
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Peso" ReadOnly="True" Name="Peso"
                                Width="50px" Caption="Peso" VisibleIndex="1" Visible="False">
                                <PropertiesTextEdit DisplayFormatString="{0:p2}"></PropertiesTextEdit>
                                <Settings AllowSort="False" />
                                <EditFormSettings Visible="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="codigoOpcao" Width="230px" Caption="Op&#231;&#227;o Escolhida" VisibleIndex="2">
                                <PropertiesComboBox DataSourceID="dsOpcao" TextField="descricaoOpcao" ValueField="codigoOpcao" ValueType="System.String"></PropertiesComboBox>
                                <DataItemTemplate>
                                    <table cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxComboBox ID="cbOpcao" runat="server" Width="170px" ValueType="System.String" ClientInstanceName="cbOpcao">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	processaOpcaoEscolhida(s, e, 'gvCritNeg');
}"></ClientSideEvents>

                                                        <DisabledStyle ForeColor="#404040"></DisabledStyle>
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td style="width: 25px" align="center">
                                                    <dxe:ASPxImage ID="imgLegenda" runat="server" ImageUrl="~/imagens/ajuda.png">
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td align="center" style="width: 25px">
                                                    <dxe:ASPxImage ID="imgVotacao" runat="server"
                                                        ImageUrl="~/imagens/opCriterios.png">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxpc:ASPxPopupControl ID="pcLegenda" runat="server" Width="580px" ClientInstanceName="pcLegenda" PopupElementID="imgLegenda" HeaderText="Ajuda">
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <div id="divLegenda" style="width: 100%; height: 150px; overflow: auto"></div>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                        <HeaderStyle />
                                    </dxpc:ASPxPopupControl>
                                    <dxpc:ASPxPopupControl ID="pcVotacao" runat="server"
                                        ClientInstanceName="pcVotacao"
                                        HeaderText="Indicações do Critério" PopupElementID="imgVotacao" Width="580px">
                                        <ContentCollection>
                                            <dxpc:PopupControlContentControl runat="server">
                                                <div id="divVotacao0" style="width: 100%; height: 150px; overflow: auto">
                                                </div>
                                            </dxpc:PopupControlContentControl>
                                        </ContentCollection>
                                        <HeaderStyle />
                                    </dxpc:ASPxPopupControl>
                                </DataItemTemplate>
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="DescricaoOpcaoCriterioSelecao" ReadOnly="True" Name="DescricaoOpcaoCriterioSelecao" Caption="Detalhes" Visible="False" VisibleIndex="6">
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox ID="txtDetalhe" runat="server" Width="170px" ReadOnly="True" ClientInstanceName="txtDetalhe">
                                        <Border BorderStyle="None" />
                                    </dxe:ASPxTextBox>

                                </DataItemTemplate>
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="Valor" ReadOnly="True" Name="Valor"
                                Width="140px" Caption="Valor" VisibleIndex="3" Visible="False">
                                <PropertiesTextEdit DisplayFormatString="{0:p2}"></PropertiesTextEdit>
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox ID="txtValor" runat="server" Width="60px" ReadOnly="True" ClientInstanceName="txtValor" DisplayFormatString="{0:p2}">
                                        <Border BorderStyle="None"></Border>
                                    </dxe:ASPxTextBox>
                                </DataItemTemplate>

                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoCategoria" Visible="False" VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoCriterioSelecao" Name="CodigoCriterioSelecao" Visible="False" VisibleIndex="8"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoOpcaoCriterioSelecao" Visible="False" VisibleIndex="9"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="ValorOpcaoCriterioSelecao" Visible="False" VisibleIndex="10">
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="NomeFatorPortfolio" ReadOnly="True" Caption="Fator" VisibleIndex="4" GroupIndex="0" SortIndex="0" SortOrder="Ascending" Width="100px">
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="NomeGrupo" ReadOnly="True" Caption="Grupo" VisibleIndex="5">
                                <Settings AllowSort="False" />
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>

                        <Settings ShowGroupPanel="True" ShowFooter="True"></Settings>
                    </dxwgv:ASPxGridView>
                </td>
                <td style="width: 15px"></td>
            </tr>
            <tr style="height: 16px">
                <td></td>
                <td style="height: 10px"></td>
                <td style="width: 15px"></td>
            </tr>
            <tr>
                <td></td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btnSalvar2" ClientInstanceName="btnSalvar2" runat="server" Text="Salvar" Width="100px" AutoPostBack="False" CssClass="btn-Upp">
                                    <Paddings Padding="0px"></Paddings>

                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
	validaGrids();
}"></ClientSideEvents>
                                </dxe:ASPxButton>
                            </td>
                            <td style="padding-left: 5px">
                                <dxe:ASPxButton runat="server" ClientInstanceName="btnFechar" Text="Fechar"
                                    Width="90px" ID="btnFechar" CssClass="btn-Upp">
                                    <ClientSideEvents Click="function(s, e) {	
	e.processOnServer = false;
    window.top.fechaModal();
}"></ClientSideEvents>

                                    <Paddings Padding="0px"></Paddings>
                                </dxe:ASPxButton>





                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 15px"></td>
            </tr>
        </table>
        <asp:SqlDataSource ID="dsDados" runat="server"
            SelectCommand="SELECT c.[CodigoCategoria],c.[DescricaoCategoria],fp.[NomeFatorPortfolio]
			,gcs.[NomeGrupo],cs.[CodigoCriterioSelecao],cs.[DescricaoCriterioSelecao]
			,pcs.[CodigoOpcaoCriterioSelecao],ocs.[DescricaoOpcaoCriterioSelecao]
			,m1.[PesoObjetoMatriz] AS [Peso],m2.[PesoObjetoMatriz] AS [Valor] 
	FROM [dbo].[Projeto] AS [p] INNER JOIN 
			 [dbo].[Categoria] AS [c] ON (c.[CodigoCategoria]=p.[CodigoCategoria] 
															  AND c.[DataExclusao] IS NULL) INNER JOIN 
			 [dbo].[MatrizObjetoCriterio] AS [m1] ON (m1.CodigoCategoria=p.[CodigoCategoria] 
																						AND m1.[IniciaisTipoObjetoCriterioPai]='GP') INNER JOIN 
			 [dbo].[GrupoCriterioSelecao] AS [gcs] ON (gcs.CodigoGrupoCriterio=m1.[CodigoObjetoCriterioPai]) INNER JOIN 
			 [dbo].[FatorPortfolio] AS [fp] ON (fp.[CodigoFatorPortfolio]=gcs.[CodigoFatorPortfolio] 
																		  AND fp.[ValorSinalFator]=1) INNER JOIN 
			 [dbo].[CriterioSelecao] AS [cs] ON (cs.[CodigoCriterioSelecao]=m1.[CodigoObjetoCriterio] 
																		  AND cs.[DataExclusao] IS NULL) INNER JOIN 
			 [dbo].[ProjetoCriterioSelecao] AS [pcs] ON (pcs.[CodigoProjeto]=p.[CodigoProjeto]
																							 AND pcs.[CodigoCriterioSelecao]=cs.[CodigoCriterioSelecao]) INNER JOIN 
			 [dbo].[OpcaoCriterioSelecao] AS [ocs]ON (ocs.[CodigoCriterioSelecao]=pcs.[CodigoCriterioSelecao] 
																						AND ocs.[CodigoOpcaoCriterioSelecao]=pcs.[CodigoOpcaoCriterioSelecao]) INNER JOIN 
			 [dbo].[MatrizObjetoCriterio] AS [m2] ON (m2.[CodigoCategoria]=p.[CodigoCategoria] 
																						AND m2.IniciaisTipoObjetoCriterioPai='CR' 
																						AND m2.[CodigoObjetoCriterioPai]=pcs.[CodigoCriterioSelecao] 
																						AND m2.CodigoObjetoCriterio=pcs.[CodigoCriterioSelecao]*1000+ASCII(pcs.[CodigoOpcaoCriterioSelecao]) ) 
 WHERE (p.[CodigoProjeto]=@CodigoProjeto AND pcs.[EtapaPreenchimento] = @EtapaPreenchimento) 
UNION 
	SELECT c.[CodigoCategoria],c.[DescricaoCategoria],fp.[NomeFatorPortfolio]
				,gcs.[NomeGrupo],cs.[CodigoCriterioSelecao],cs.[DescricaoCriterioSelecao]
				,NULL,NULL,NULL,NULL 
	  FROM [dbo].[Projeto] AS [p] INNER JOIN 
	       [dbo].[Categoria] AS [c]ON(c.[CodigoCategoria]=p.[CodigoCategoria] 
															  AND c.[DataExclusao]IS NULL) INNER JOIN 
				 [dbo].[MatrizObjetoCriterio] AS [m1]ON (m1.CodigoCategoria=p.[CodigoCategoria] 
																AND m1.[IniciaisTipoObjetoCriterioPai]='GP') INNER JOIN 
				 [dbo].[GrupoCriterioSelecao] AS [gcs] ON (gcs.CodigoGrupoCriterio=m1.[CodigoObjetoCriterioPai]) INNER JOIN 
				 [dbo].[FatorPortfolio] AS [fp] ON (fp.[CodigoFatorPortfolio]=gcs.[CodigoFatorPortfolio] AND fp.[ValorSinalFator]=1) INNER JOIN 
				 [dbo].[CriterioSelecao] AS [cs]ON (cs.[CodigoCriterioSelecao]=m1.[CodigoObjetoCriterio] AND cs.[DataExclusao]IS NULL) 
	WHERE (p.[CodigoProjeto]=@CodigoProjeto) 
	  AND (NOT EXISTS(SELECT 1 
											FROM [dbo].[ProjetoCriterioSelecao] AS [pcs] 
										 WHERE pcs.[CodigoProjeto]=p.[CodigoProjeto] 
										   AND pcs.[CodigoCriterioSelecao]=cs.[CodigoCriterioSelecao]
										   AND pcs.[EtapaPreenchimento] = @EtapaPreenchimento))">
            <SelectParameters>
                <asp:Parameter Name="CodigoProjeto" />
                <asp:Parameter Name="EtapaPreenchimento" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsRiscos" runat="server"
            SelectCommand="SELECT c.[CodigoCategoria],c.[DescricaoCategoria],fp.[NomeFatorPortfolio],gcs.[NomeGrupo],cs.[CodigoCriterioSelecao],cs.[DescricaoCriterioSelecao],pcs.[CodigoOpcaoCriterioSelecao],ocs.[DescricaoOpcaoCriterioSelecao],m1.[PesoObjetoMatriz] AS [Peso],m2.[PesoObjetoMatriz] AS [Valor] FROM [dbo].[Projeto] AS [p] INNER JOIN [dbo].[Categoria] AS [c] ON (c.[CodigoCategoria]=p.[CodigoCategoria] AND c.[DataExclusao] IS NULL) INNER JOIN [dbo].[MatrizObjetoCriterio] AS [m1] ON (m1.CodigoCategoria=p.[CodigoCategoria] AND m1.[IniciaisTipoObjetoCriterioPai]='GP') INNER JOIN [dbo].[GrupoCriterioSelecao] AS [gcs] ON (gcs.CodigoGrupoCriterio=m1.[CodigoObjetoCriterioPai]) INNER JOIN [dbo].[FatorPortfolio] AS [fp] ON (fp.[CodigoFatorPortfolio]=gcs.[CodigoFatorPortfolio] AND fp.[ValorSinalFator]=-1) INNER JOIN [dbo].[CriterioSelecao] AS [cs] ON (cs.[CodigoCriterioSelecao]=m1.[CodigoObjetoCriterio] AND cs.[DataExclusao] IS NULL) INNER JOIN [dbo].[ProjetoCriterioSelecao] AS [pcs] ON (pcs.[CodigoProjeto]=p.[CodigoProjeto] AND pcs.[CodigoCriterioSelecao]=cs.[CodigoCriterioSelecao]) INNER JOIN [dbo].[OpcaoCriterioSelecao] AS [ocs]ON (ocs.[CodigoCriterioSelecao]=pcs.[CodigoCriterioSelecao] AND ocs.[CodigoOpcaoCriterioSelecao]=pcs.[CodigoOpcaoCriterioSelecao]) INNER JOIN [dbo].[MatrizObjetoCriterio] AS [m2] ON (m2.[CodigoCategoria]=p.[CodigoCategoria] AND m2.IniciaisTipoObjetoCriterioPai='CR' AND m2.[CodigoObjetoCriterioPai]=pcs.[CodigoCriterioSelecao] AND m2.CodigoObjetoCriterio=pcs.[CodigoCriterioSelecao]*1000+ASCII(pcs.[CodigoOpcaoCriterioSelecao]) ) WHERE (p.[CodigoProjeto]=@CodigoProjeto AND pcs.[EtapaPreenchimento] = @EtapaPreenchimento) UNION SELECT c.[CodigoCategoria],c.[DescricaoCategoria],fp.[NomeFatorPortfolio],gcs.[NomeGrupo],cs.[CodigoCriterioSelecao],cs.[DescricaoCriterioSelecao],NULL,NULL,NULL,NULL FROM [dbo].[Projeto] AS [p] INNER JOIN [dbo].[Categoria] AS [c]ON(c.[CodigoCategoria]=p.[CodigoCategoria] AND c.[DataExclusao]IS NULL) INNER JOIN [dbo].[MatrizObjetoCriterio] AS [m1]ON (m1.CodigoCategoria=p.[CodigoCategoria] AND m1.[IniciaisTipoObjetoCriterioPai]='GP') INNER JOIN [dbo].[GrupoCriterioSelecao] AS [gcs] ON (gcs.CodigoGrupoCriterio=m1.[CodigoObjetoCriterioPai]) INNER JOIN [dbo].[FatorPortfolio] AS [fp] ON (fp.[CodigoFatorPortfolio]=gcs.[CodigoFatorPortfolio] AND fp.[ValorSinalFator]=-1) INNER JOIN [dbo].[CriterioSelecao] AS [cs]ON (cs.[CodigoCriterioSelecao]=m1.[CodigoObjetoCriterio] AND cs.[DataExclusao]IS NULL) WHERE (p.[CodigoProjeto]=@CodigoProjeto) AND (NOT EXISTS(SELECT 1 FROM [dbo].[ProjetoCriterioSelecao] AS [pcs] WHERE pcs.[CodigoProjeto]=p.[CodigoProjeto] AND pcs.[CodigoCriterioSelecao]=cs.[CodigoCriterioSelecao] AND pcs.[EtapaPreenchimento] = @EtapaPreenchimento))">
            <SelectParameters>
                <asp:Parameter Name="CodigoProjeto" />
                <asp:Parameter Name="EtapaPreenchimento" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsOpcao" runat="server"
            SelectCommand="SELECT CONVERT (varchar, CodigoCriterioSelecao) + CodigoOpcaoCriterioSelecao AS codigoOpcao, CodigoOpcaoCriterioSelecao + ' - ' + DescricaoOpcaoCriterioSelecao AS descricaoOpcao FROM OpcaoCriterioSelecao WHERE (CodigoCriterioSelecao = @CodigoCriterioSelecao) ORDER BY descricaoOpcao">
            <SelectParameters>
                <asp:Parameter Name="CodigoCriterioSelecao" />
            </SelectParameters>
        </asp:SqlDataSource>
        <dxcb:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar"
            OnCallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {	
	if(s.cp_msg != null &amp;&amp; s.cp_msg != &quot;&quot;)
	{
		window.top.mostraMensagem(s.cp_msg,  'sucesso', false, false, null);
                                window.top.fechaModal();
		gvCritPos.PerformCallback('ATUALIZAR');
		gvCritNeg.PerformCallback('ATUALIZAR');

            //Se salvou os critérios, limpa a variável de obrigatoriedade de preenchimento dos critérios
            window.parent.parent.frmCriteriosPendente = &quot;&quot;;
	}
                else
                {
                           if(s.cp_erro != null &amp;&amp; s.cp_erro != &quot;&quot;)
                           {
                                     window.top.mostraMensagem(s.cp_erro,  'erro', true, false, null);
                           }
                }
}" />
        </dxcb:ASPxCallback>
        <dxcb:ASPxCallback ID="callbackCategoria" runat="server" ClientInstanceName="callbackCategoria"
            OnCallback="callbackCategoria_Callback">
            <ClientSideEvents EndCallback="function(s, e) {		
		gvCritPos.PerformCallback('ATUALIZAR');
		gvCritNeg.PerformCallback('ATUALIZAR');
}" />
        </dxcb:ASPxCallback>
    </form>
</body>
</html>
