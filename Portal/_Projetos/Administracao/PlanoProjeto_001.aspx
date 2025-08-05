<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanoProjeto_001.aspx.cs" Inherits="_Projetos_Administracao_PlanoProjeto_001" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .secaoFormulario {
            padding-top: 10px;
            padding-bottom: 10px;
        }
        .descricaoSecao {
            
            font-style: italic;
        }

        .contadorCaracteres {
            font-size: 10pt;
            color: gray;
        }

        .tituloColunaGrid {
            font-weight: bold;
        }

        .tabelaFormulario {
            width: 100%;
            border: 1px solid;
            border-collapse: collapse;
        }

            .tabelaFormulario td, .tabelaFormulario th {
                border: 1px solid;
                text-align: left;
                height: 20px;
            }

        .auto-style1 {
            height: 18px;
        }
        .auto-style2 {
            height: 20px;
        }
                
        #pageControl_C0 > div > div.tabContent { height: auto !important; overflow: auto !important }
    </style>
    <script type="text/javascript">
        function RealizaContagemCaracteres(memo, label, tamanho) {
            label.SetText(memo.GetText().length + ' de ' + tamanho);
        }

        function DefineAltura() {
            var height = Math.max(0, document.documentElement.clientHeight - 100);
            var x = document.getElementsByClassName("tabContent");
            var i;
            for (i = 0; i < x.length; i++) {
                x[i].style.height = height + "px";
            }
        }

        function showWindowByName(name) {
            var window = popup.GetWindowByName(name);
            popup.ShowWindow(window);
        }

        function hideWindowByName(name) {
            var window = popup.GetWindowByName(name);
            popup.HideWindow(window);
        }

        function SalvarAlteracaoNomesColunas() {
            var valida = ASPxClientEdit.ValidateEditorsInContainer(null);
            if (valida) {
                hideWindowByName('winAlterarNomesColunas');
                gvMatrizResponsabilidade.PerformCallback('alterar_nomes_colunas');
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxtv:ASPxPageControl ID="pageControl" runat="server" ActiveTabIndex="0" Height="500px" Width="100%" ClientInstanceName="pageControl" EnableCallBacks="True">
                <TabPages>
                    <dxtv:TabPage Name="tabCaracterizacao" Text="Caracterização">
                        <ContentCollection>
                            <dxtv:ContentControl runat="server">
                                <div class="tabContent">
                                    <div id="divHistorico" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel1" runat="server" CssClass="tituloSecao" Text="Histórico de Revisões no documento">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxGridView ID="gvHistorico" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvHistorico" Width="100%" DataSourceID="dsHistoricoRevisoes" KeyFieldName="CodigoRevisaoPPJ">
                                            <StylesEditors>
                                                <ReadOnly BackColor="LightGray">
                                                </ReadOnly>
                                            </StylesEditors>
                                            <Columns>
                                                <dxtv:GridViewDataTextColumn Caption="Versão" FieldName="NumeroVersaoRevisaoProjeto" ShowInCustomizationForm="True" VisibleIndex="0" Width="5%">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Data da Revisão" FieldName="DataRevisao" ShowInCustomizationForm="True" VisibleIndex="1" Width="15%">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Descrição" FieldName="DescricaoRevisao" ShowInCustomizationForm="True" VisibleIndex="2">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Autor" FieldName="IdentificacaoAutorRevisao" ShowInCustomizationForm="True" VisibleIndex="3" Width="25%">
                                                </dxtv:GridViewDataTextColumn>
                                            </Columns>
                                        </dxtv:ASPxGridView>
                                        <asp:SqlDataSource ID="dsHistoricoRevisoes" runat="server" DeleteCommand="DELETE FROM [ppj_HistoricoRevisoes] WHERE [CodigoRevisaoPPJ] = @CodigoRevisaoPPJ AND [CodigoProjeto] = @CodigoProjeto" InsertCommand="INSERT INTO [ppj_HistoricoRevisoes] ([CodigoProjeto], [NumeroVersaoRevisaoProjeto], [DataRevisao], [IdentificacaoAutorRevisao], [DataRegistroRevisao], [DataUltimaAlteracao], [CodigoUsuarioUltimaAlteracao], [IndicaRevisaoPublicada], [DescricaoRevisao]) VALUES (@CodigoProjeto, @NumeroVersaoRevisaoProjeto, @DataRevisao, @IdentificacaoAutorRevisao, @DataRegistroRevisao, @DataUltimaAlteracao, @CodigoUsuarioUltimaAlteracao, @IndicaRevisaoPublicada, @DescricaoRevisao)" SelectCommand="SELECT * FROM dbo.f_ppj_getHistoricoVersoesPlano(@CodigoProjeto, NULL) ORDER BY CAST(NumeroVersaoRevisaoProjeto AS Decimal(10,2)) DESC" UpdateCommand="UPDATE [ppj_HistoricoRevisoes] SET [NumeroVersaoRevisaoProjeto] = @NumeroVersaoRevisaoProjeto, [DataRevisao] = @DataRevisao, [IdentificacaoAutorRevisao] = @IdentificacaoAutorRevisao, [DataRegistroRevisao] = @DataRegistroRevisao, [DataUltimaAlteracao] = @DataUltimaAlteracao, [CodigoUsuarioUltimaAlteracao] = @CodigoUsuarioUltimaAlteracao, [IndicaRevisaoPublicada] = @IndicaRevisaoPublicada, [DescricaoRevisao] = @DescricaoRevisao WHERE [CodigoRevisaoPPJ] = @CodigoRevisaoPPJ AND [CodigoProjeto] = @CodigoProjeto">
                                            <DeleteParameters>
                                                <asp:Parameter Name="CodigoRevisaoPPJ" Type="Int64" />
                                                <asp:Parameter Name="CodigoProjeto" Type="Int32" />
                                            </DeleteParameters>
                                            <InsertParameters>
                                                <asp:Parameter Name="CodigoProjeto" Type="Int32" />
                                                <asp:Parameter Name="NumeroVersaoRevisaoProjeto" Type="Decimal" />
                                                <asp:Parameter Name="DataRevisao" Type="DateTime" />
                                                <asp:Parameter Name="IdentificacaoAutorRevisao" Type="String" />
                                                <asp:Parameter Name="DataRegistroRevisao" Type="DateTime" />
                                                <asp:Parameter Name="DataUltimaAlteracao" Type="DateTime" />
                                                <asp:Parameter Name="CodigoUsuarioUltimaAlteracao" Type="Int32" />
                                                <asp:Parameter Name="IndicaRevisaoPublicada" Type="String" />
                                                <asp:Parameter Name="DescricaoRevisao" Type="String" />
                                            </InsertParameters>
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" Type="Int32" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="NumeroVersaoRevisaoProjeto" Type="Decimal" />
                                                <asp:Parameter Name="DataRevisao" Type="DateTime" />
                                                <asp:Parameter Name="IdentificacaoAutorRevisao" Type="String" />
                                                <asp:Parameter Name="DataRegistroRevisao" Type="DateTime" />
                                                <asp:Parameter Name="DataUltimaAlteracao" Type="DateTime" />
                                                <asp:Parameter Name="CodigoUsuarioUltimaAlteracao" Type="Int32" />
                                                <asp:Parameter Name="IndicaRevisaoPublicada" Type="String" />
                                                <asp:Parameter Name="DescricaoRevisao" Type="String" />
                                                <asp:Parameter Name="CodigoRevisaoPPJ" Type="Int64" />
                                                <asp:Parameter Name="CodigoProjeto" Type="Int32" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div id="divInformacoes" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel2" runat="server" CssClass="tituloSecao" Text="Informações iniciais">
                                        </dxtv:ASPxLabel>
                                        <table class="tabelaFormulario">
                                            <tr>
                                                <th style="width: 70%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel29" runat="server" CssClass="tituloColunaGrid" Text="Categoria">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                                <th style="width: 15%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel30" runat="server" CssClass="tituloColunaGrid"  Text="Data Início Planejada">
                                                    </dxtv:ASPxLabel>
                                                </th> 
                                                <th style="width: 15%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel31" runat="server" CssClass="tituloColunaGrid"  Text="Data Fim Planejada">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblCategoria" runat="server" ClientInstanceName="lblNome">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="text-align:center">
                                                    <dxtv:ASPxLabel ID="lblDataInicio" runat="server" ClientInstanceName="lblDataInicio">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="text-align:center">
                                                    <dxtv:ASPxLabel ID="lblDataFim" runat="server" ClientInstanceName="lblDataFim">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divResponsavel" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel3" runat="server" CssClass="tituloSecao" Text="Responsável">
                                        </dxtv:ASPxLabel>
                                        <table class="tabelaFormulario">
                                            <tr>
                                                <th class="auto-style2" style="width: 70%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel23" runat="server" CssClass="tituloColunaGrid"  Text="Nome Completo">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                                <th class="auto-style2" style="width: 15%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel24" runat="server" CssClass="tituloColunaGrid"  Text="Sigla da Unidade">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                                <th class="auto-style2" style="width: 15%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel25" runat="server" CssClass="tituloColunaGrid"  Text="Matrícula">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblNomeResponsavel" runat="server" ClientInstanceName="lblNomeResponsavel">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblSiglaUnidadeResponsavel" runat="server" ClientInstanceName="lblSiglaUnidadeResponsavel">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblMatriculaResponsavel" runat="server" ClientInstanceName="lblMatriculaResponsavel">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divPatrocinador" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel4" runat="server" CssClass="tituloSecao" Text="Patrocinador">
                                        </dxtv:ASPxLabel>
                                        <table class="tabelaFormulario">
                                            <tr>
                                                <th style="width: 70%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel26" runat="server" CssClass="tituloColunaGrid"  Text="Nome Completo">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                                <th style="width: 15%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel27" runat="server" CssClass="tituloColunaGrid"  Text="Sigla da Unidade">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                                <th style="width: 15%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel28" runat="server" CssClass="tituloColunaGrid" Text="Matrícula">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblNomePatrocinador" runat="server" ClientInstanceName="lblNomePatrocinador">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblSiglaUnidadePatrocinador" runat="server" ClientInstanceName="lblSiglaUnidadePatrocinador">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblMatriculaPatrocinador" runat="server" ClientInstanceName="lblMatriculaPatrocinador">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divObjetivoGeral" class="secaoFormulario">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="ASPxLabel5" runat="server" CssClass="tituloSecao" Text="Objetivo Geral">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel32" runat="server" ClientInstanceName="labelCount_5" CssClass="contadorCaracteres" Text="0 de 0">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxtv:ASPxLabel ID="ASPxLabel48" runat="server" CssClass="descricaoSecao" Text="[Resultados mais abrangentes para os quais o projeto pretende atingir. Observe-se que a formulação do objetivo se faz mediante o emprego de verbos no infinitivo]">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxMemo ID="txtObjetivoGeral" runat="server" ClientInstanceName="txtObjetivoGeral" MaxLength="2000" Rows="5" Width="100%">
                                            <ClientSideEvents Init="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_5, 2000);
}"
                                                KeyUp="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_5, 2000);
}"
                                                ValueChanged="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_5, 2000);
}" />
                                        </dxtv:ASPxMemo>
                                    </div>
                                    <div id="divObjetivoEspecifico" class="secaoFormulario">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="ASPxLabel6" runat="server" CssClass="tituloSecao" Text="Objetivos Específicos">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel36" runat="server" ClientInstanceName="labelCount_5_1" CssClass="contadorCaracteres" Text="0 de 0">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxtv:ASPxLabel ID="ASPxLabel49" runat="server" CssClass="descricaoSecao" Text="[Resultados detalhados que, somadas, conduzirão ao desfecho do objetivo geral. Observe-se que a formulação dos objetivos se faz mediante o emprego de verbos no infinitivo]">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxMemo ID="txtObjetivosEspecificos" runat="server" ClientInstanceName="txtObjetivosEspecificos" MaxLength="8000" Rows="5" Width="100%">
                                            <ClientSideEvents Init="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_5_1, 8000);
}"
                                                KeyUp="function(s, e) {                     
	RealizaContagemCaracteres(s, labelCount_5_1, 8000);
}"
                                                ValueChanged="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_5_1, 8000);
}" />
                                        </dxtv:ASPxMemo>
                                    </div>
                                    <div id="divJustificativa" class="secaoFormulario">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="ASPxLabel7" runat="server" CssClass="tituloSecao" Text="Justificativa">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel37" runat="server" ClientInstanceName="labelCount_6" CssClass="contadorCaracteres" Text="0 de 0">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxtv:ASPxLabel ID="ASPxLabel50" runat="server" CssClass="descricaoSecao" Text="[Descrição das necessidades negociais que deram origem ao projeto (demanda de mercado, solicitação de cliente, avanço tecnológico, requisito legal, necessidade social, entre outros)]">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxMemo ID="txtJustificativa" runat="server" ClientInstanceName="txtJustificativa" MaxLength="8000" Rows="5" Width="100%">
                                            <ClientSideEvents Init="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_6, 8000);
}"
                                                KeyUp="function(s, e) {                     
	RealizaContagemCaracteres(s, labelCount_6, 8000);
}"
                                                ValueChanged="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_6, 8000);
}" />
                                        </dxtv:ASPxMemo>
                                    </div>
                                    <div id="divAprovadores" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel8" runat="server" CssClass="tituloSecao" Text="Aprovadores">
                                        </dxtv:ASPxLabel>
                                        <table class="tabelaFormulario">
                                            <tr>
                                                <th style="width: 20%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel57" runat="server" Text="Patrocinador do projeto">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                                <th style="width: 20%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel58" runat="server" Text="Gerente Nacional do Projeto">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                                <th style="width: 20%">
                                                    <dxtv:ASPxLabel ID="ASPxLabel59" runat="server" Text="Escritório de Projetos / GEDTI">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                                <th>
                                                    <dxtv:ASPxLabel ID="ASPxLabel60" runat="server" Text="Comprovação de aprovação do documento">
                                                    </dxtv:ASPxLabel>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblPatrocinadorProjeto" runat="server" ClientInstanceName="lblPatrocinadorProjeto">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblGerenteNacionalProjeto" runat="server" ClientInstanceName="lblGerenteNacionalProjeto">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxLabel ID="lblEscritorioProjetos" runat="server" ClientInstanceName="lblEscritorioProjetos">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxMemo ID="txtComprovacaoAprovacaoDocumento" runat="server" ClientInstanceName="txtComprovacaoAprovacaoDocumento" MaxLength="500" Rows="5" Width="100%">
                                                        <Border BorderStyle="None" />
                                                    </dxtv:ASPxMemo>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </dxtv:ContentControl>
                        </ContentCollection>
                    </dxtv:TabPage>
                    <dxtv:TabPage Name="tabEscopo" Text="Escopo">
                        <ContentCollection>
                            <dxtv:ContentControl runat="server">
                                <div class="tabContent">
                                    <div id="divEscopo" class="secaoFormulario">
                                        <table>
                                            <tr>
                                                <td style="white-space: nowrap;">
                                                    <dxtv:ASPxLabel ID="ASPxLabel9" runat="server" CssClass="tituloSecao" Text="Escopo">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="padding-left: 10px; width: 100%;">
                                                    <dxtv:ASPxLabel ID="ASPxLabel38" runat="server" ClientInstanceName="labelCount_8" CssClass="contadorCaracteres" Text="0 de 0">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxtv:ASPxLabel ID="ASPxLabel46" runat="server" CssClass="descricaoSecao" Text="[Descrever sucintamente o produto, serviço ou resultado que será gerado pelo projeto e os principais sub-produtos]">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxMemo ID="txtEscopo" runat="server" ClientInstanceName="txtEscopo" MaxLength="8000" Rows="5" Width="100%">
                                            <ClientSideEvents Init="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_8, 8000);
}"
                                                KeyUp="function(s, e) {                     
	RealizaContagemCaracteres(s, labelCount_8, 8000);
}"
                                                ValueChanged="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_8, 8000);
}" />
                                        </dxtv:ASPxMemo>
                                    </div>
                                    <div id="divNaoEscopo" class="secaoFormulario">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="ASPxLabel10" runat="server" CssClass="tituloSecao" Text="Não Escopo">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel39" runat="server" ClientInstanceName="labelCount_9" CssClass="contadorCaracteres" Text="0 de 0">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxtv:ASPxLabel ID="ASPxLabel51" runat="server" CssClass="descricaoSecao" Text="[Descrever de forma clara o que NÃO fará parte do produto, serviço ou resultado que será gerado pelo projeto]">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxMemo ID="txtNaoEscopo" runat="server" ClientInstanceName="txtNaoEscopo" MaxLength="8000" Rows="5" Width="100%">
                                            <ClientSideEvents Init="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_9, 8000);
}"
                                                KeyUp="function(s, e) {                     
	RealizaContagemCaracteres(s, labelCount_9, 8000);
}"
                                                ValueChanged="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_9, 8000);
}" />
                                        </dxtv:ASPxMemo>
                                    </div>
                                    <div id="divStakeholders" class="secaoFormulario">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="ASPxLabel11" runat="server" CssClass="tituloSecao" Text="Stakeholders">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel40" runat="server" ClientInstanceName="labelCount_7" CssClass="contadorCaracteres" Text="0 de 0">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxtv:ASPxLabel ID="ASPxLabel52" runat="server" CssClass="descricaoSecao" Text="[Descreva os influenciadores, beneficiados, fornecedores e interessados pelo projeto]">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxMemo ID="txtStakeholders" runat="server" ClientInstanceName="txtStakeholders" MaxLength="8000" Rows="5" Width="100%">
                                            <ClientSideEvents Init="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_7, 8000);
}"
                                                KeyUp="function(s, e) {                     
	RealizaContagemCaracteres(s, labelCount_7, 8000);
}"
                                                ValueChanged="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_7, 8000);
}" />
                                        </dxtv:ASPxMemo>
                                    </div>
                                    <div id="divPremissas" class="secaoFormulario">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="ASPxLabel12" runat="server" CssClass="tituloSecao" Text="Premissas">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel41" runat="server" ClientInstanceName="labelCount_10" CssClass="contadorCaracteres" Text="0 de 0">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxtv:ASPxLabel ID="ASPxLabel53" runat="server" CssClass="descricaoSecao" Text=" [Fatores que para fins de planejamento são considerados verdadeiros, reais ou certos. Toda premissa gera um risco]">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxMemo ID="txtPremissas" runat="server" ClientInstanceName="txtPremissas" MaxLength="8000" Rows="5" Width="100%">
                                            <ClientSideEvents Init="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_10, 8000);
}"
                                                KeyUp="function(s, e) {                     
	RealizaContagemCaracteres(s, labelCount_10, 8000);
}"
                                                ValueChanged="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_10, 8000);
}" />
                                        </dxtv:ASPxMemo>
                                    </div>
                                    <div id="divRestricoes" class="secaoFormulario">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="ASPxLabel13" runat="server" CssClass="tituloSecao" Text="Restrições">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel42" runat="server" ClientInstanceName="labelCount_11" CssClass="contadorCaracteres" Text="0 de 0">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxtv:ASPxLabel ID="ASPxLabel54" runat="server" CssClass="descricaoSecao" Text=" [Fatores que vão limitar a execução do projeto. Podem ser recursos tecnológicos, humanos, orçamentos, prazos, etc. Em grande parte das vezes é uma resposta à um risco]">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxMemo ID="txtRestricoes" runat="server" ClientInstanceName="txtRestricoes" MaxLength="8000" Rows="5" Width="100%">
                                            <ClientSideEvents Init="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_11, 8000);
}"
                                                KeyUp="function(s, e) {                     
	RealizaContagemCaracteres(s, labelCount_11, 8000);
}"
                                                ValueChanged="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_11, 8000);
}" />
                                        </dxtv:ASPxMemo>
                                    </div>
                                    <div id="divBeneficios" class="secaoFormulario">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="ASPxLabel14" runat="server" CssClass="tituloSecao" Text="Benefícios Esperados">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel43" runat="server" ClientInstanceName="labelCount_12" CssClass="contadorCaracteres" Text="0 de 0">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxtv:ASPxLabel ID="ASPxLabel55" runat="server" CssClass="descricaoSecao" Text=" [Informar os benefícios para a organização nos âmbitos estratégicos, negociais e tecnológicos de forma mensurável]">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxMemo ID="txtBeneficiosEsperados" runat="server" ClientInstanceName="txtBeneficiosEsperados" MaxLength="8000" Rows="5" Width="100%">
                                            <ClientSideEvents Init="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_12, 8000);
}"
                                                KeyUp="function(s, e) {                     
	RealizaContagemCaracteres(s, labelCount_12, 8000);
}"
                                                ValueChanged="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_12, 8000);
}" />
                                        </dxtv:ASPxMemo>
                                    </div>
                                    <div id="divOrcamento" class="secaoFormulario">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxLabel ID="ASPxLabel15" runat="server" CssClass="tituloSecao" Text="Orçamento Previsto">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <dxtv:ASPxLabel ID="ASPxLabel44" runat="server" ClientInstanceName="labelCount_13" CssClass="contadorCaracteres" Text="0 de 0">
                                                    </dxtv:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxtv:ASPxLabel ID="ASPxLabel56" runat="server" CssClass="descricaoSecao" Text=" [Informar o valor previsto de gastos (ferramentas, equipes, destacamentos), para o projeto por ano]">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxMemo ID="txtOrcamentoPrevisto" runat="server" ClientInstanceName="txtOrcamentoPrevisto" MaxLength="8000" Rows="5" Width="100%">
                                            <ClientSideEvents Init="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_13, 8000);
}"
                                                KeyUp="function(s, e) {                     
	RealizaContagemCaracteres(s, labelCount_13, 8000);
}"
                                                ValueChanged="function(s, e) {
	RealizaContagemCaracteres(s, labelCount_13, 8000);
}" />
                                        </dxtv:ASPxMemo>
                                    </div>
                                    <div id="divAlinhamento" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel16" runat="server" CssClass="tituloSecao" Text="Alinhamento Estratégico">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxGridView ID="gvAlinhamentoEstrategico" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvAlinhamentoEstrategico" Width="100%" DataSourceID="dsAlinhamentoEstrategico" KeyFieldName="SequenciaRegistro">
                                            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm">
                                            </SettingsEditing>
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsPopup>
                                                <EditForm Modal="True" Width="600px" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                            </SettingsPopup>
                                            <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                            <StylesEditors>
                                                <ReadOnly BackColor="LightGray">
                                                </ReadOnly>
                                            </StylesEditors>
                                            <Columns>
                                                <dxtv:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="60px">
                                                </dxtv:GridViewCommandColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Alinhamento Estratégico CAIXA" FieldName="DescricaoObjetoMapaInstituicao" ShowInCustomizationForm="True" VisibleIndex="1">
                                                    <PropertiesMemoEdit MaxLength="8000" Rows="5" Width="100%">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Alinhamento Estratégico TI" FieldName="DescricaoObjetivoTI" ShowInCustomizationForm="True" VisibleIndex="2">
                                                    <PropertiesMemoEdit MaxLength="8000" Rows="5" Width="100%">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                            </Columns>
                                            <Styles>
                                                <Header Wrap="True">
                                                </Header>
                                            </Styles>
                                        </dxtv:ASPxGridView>
                                        <asp:SqlDataSource ID="dsAlinhamentoEstrategico" runat="server" DeleteCommand="EXEC	[dbo].[p_ppj_excluiRegistroAlinhamentoEstrategico]
		@in_codigoRevisaoPPJ = @CodigoRevisaoPPJ,
		@in_codigoProjeto = @CodigoProjeto,
		@in_sequenciaRegistro = @SequenciaRegistro,
		@in_codigoUsuarioExclusao = @CodigoUsuarioExclusao"
                                            InsertCommand="EXEC p_ppj_incluiRegistroAlinhamentoEstrategico
@in_codigoRevisaoPPJ = @CodigoRevisaoPPJ,
@in_codigoProjeto = @CodigoProjeto,
@in_sequenciaRegistro = 0,
@in_descricaoObjetoMapaInstituicao = @DescricaoObjetoMapaInstituicao,
@in_descricaoObjetivoTI = @DescricaoObjetivoTI,
@in_codigoUsuarioAtualizacao = @CodigoUsuarioAtualizacao"
                                            SelectCommand="SELECT * FROM dbo.f_ppj_getDadosAlinhamentoEstrategico(@CodigoRevisaoPPJ, @CodigoProjeto) ORDER BY [SequenciaRegistro]" UpdateCommand="EXEC p_ppj_AtualizaRegistroAlinhamentoEstrategico
@in_CodigoRevisaoPPJ = @CodigoRevisaoPPJ,
@in_CodigoProjeto = @CodigoProjeto,
@in_SequenciaRegistro = @SequenciaRegistro,
@in_DescricaoObjetoMapaInstituicao = @DescricaoObjetoMapaInstituicao,
@in_DescricaoObjetivoTI = @DescricaoObjetivoTI,
@in_CodigoUsuarioAtualizacao = @CodigoUsuarioAtualizacao">
                                            <DeleteParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" Type="Int64" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" Type="Int32" />
                                                <asp:SessionParameter Name="CodigoUsuarioExclusao" SessionField="CodigoUsuario" Type="Int32" />
                                                <asp:Parameter Name="SequenciaRegistro" />
                                                <asp:Parameter Name="in_codigoRevisaoPPJ" />
                                                <asp:Parameter Name="in_codigoProjeto" />
                                                <asp:Parameter Name="in_sequenciaRegistro" />
                                                <asp:Parameter Name="in_codigoUsuarioExclusao" />
                                            </DeleteParameters>
                                            <InsertParameters>
                                                <asp:Parameter Name="in_codigoRevisaoPPJ" Type="Int64" />
                                                <asp:Parameter Name="in_codigoProjeto" Type="Int32" />
                                                <asp:Parameter Name="in_sequenciaRegistro" Type="Int16" />
                                                <asp:Parameter Name="in_descricaoObjetoMapaInstituicao" Type="String" />
                                                <asp:Parameter Name="in_descricaoObjetivoTI" Type="String" />
                                                <asp:Parameter Name="in_codigoUsuarioAtualizacao" Type="Int32" />
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                                <asp:Parameter Name="DescricaoObjetoMapaInstituicao" />
                                                <asp:Parameter Name="DescricaoObjetivoTI" />
                                                <asp:SessionParameter Name="CodigoUsuarioAtualizacao" SessionField="CodigoUsuario" />
                                            </InsertParameters>
                                            <SelectParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" Type="Int64" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" Type="Int32" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="in_CodigoRevisaoPPJ" Type="Int64" />
                                                <asp:Parameter Name="in_CodigoProjeto" Type="Int32" />
                                                <asp:Parameter Name="in_SequenciaRegistro" Type="Int16" />
                                                <asp:Parameter Name="in_DescricaoObjetoMapaInstituicao" Type="String" />
                                                <asp:Parameter Name="in_DescricaoObjetivoTI" Type="String" />
                                                <asp:Parameter Name="in_CodigoUsuarioAtualizacao" Type="Int32" />
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                                <asp:Parameter Name="SequenciaRegistro" />
                                                <asp:Parameter Name="DescricaoObjetoMapaInstituicao" />
                                                <asp:Parameter Name="DescricaoObjetivoTI" />
                                                <asp:SessionParameter Name="CodigoUsuarioAtualizacao" SessionField="CodigoUsuario" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div id="divEap" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel17" runat="server" CssClass="tituloSecao" Text="EAP e Dicionário EAP">
                                        </dxtv:ASPxLabel>
                                        <br />
                                        <dxtv:ASPxGridView ID="gvEntregas" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvEntregas" DataSourceID="dsEntregas" Width="100%" KeyFieldName="CodigoCronogramaProjeto;CodigoTarefaCronograma">
                                            <SettingsPager PageSize="15">
                                            </SettingsPager>
                                            <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm">
                                            </SettingsEditing>
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
                                            </SettingsPopup>
                                            <EditFormLayoutProperties ColCount="3">
                                                <Items>
                                                    <dxtv:GridViewColumnLayoutItem ColSpan="3" ColumnName="IdentificacaoEntrega">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColSpan="3" ColumnName="DescricaoUnidadesRelacionadas">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColumnName="PercentualRepresentacao">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColumnName="DataInicio">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColumnName="DataTermino">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColSpan="3" ColumnName="DescricaoEntrega">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColSpan="3" ColumnName="DescricaoCriterioAceitacao">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:EditModeCommandLayoutItem ColSpan="3" HorizontalAlign="Right">
                                                    </dxtv:EditModeCommandLayoutItem>
                                                </Items>
                                            </EditFormLayoutProperties>
                                            <StylesEditors>
                                                <ReadOnly BackColor="LightGray">
                                                </ReadOnly>
                                            </StylesEditors>
                                            <Columns>
                                                <dxtv:GridViewCommandColumn ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="40px">
                                                </dxtv:GridViewCommandColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Nº" FieldName="SequenciaEntrega" ShowInCustomizationForm="True" VisibleIndex="1" Width="25px">
                                                    <EditFormSettings Visible="False" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Nome da Entrega" FieldName="IdentificacaoEntrega" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="True">
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="3" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Unidades relacionadas" FieldName="DescricaoUnidadesRelacionadas" ShowInCustomizationForm="True" VisibleIndex="3">
                                                    <PropertiesMemoEdit MaxLength="500" Rows="5">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="3" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Descrição da Entrega" FieldName="DescricaoEntrega" ShowInCustomizationForm="True" VisibleIndex="7">
                                                    <PropertiesMemoEdit MaxLength="2000" Rows="5">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="3" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Critério de Aceitação" FieldName="DescricaoCriterioAceitacao" ShowInCustomizationForm="True" VisibleIndex="8">
                                                    <PropertiesMemoEdit MaxLength="500" Rows="5">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="3" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataDateColumn Caption="Data fim planejada" FieldName="DataTermino" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="6" Width="75px">
                                                    <PropertiesDateEdit DisplayFormatString="{0:d}">
                                                    </PropertiesDateEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataDateColumn>
                                                <dxtv:GridViewDataDateColumn Caption="Data início planejada" FieldName="DataInicio" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" Width="75px">
                                                    <PropertiesDateEdit DisplayFormatString="{0:d}">
                                                    </PropertiesDateEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataDateColumn>
                                                <dxtv:GridViewDataSpinEditColumn Caption="% de representatividade p/ o projeto" FieldName="PercentualRepresentacao" ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                    <PropertiesSpinEdit DisplayFormatString="g" MaxValue="100" NumberType="Integer">
                                                    </PropertiesSpinEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataSpinEditColumn>
                                            </Columns>
                                            <Styles>
                                                <Header Wrap="True">
                                                </Header>
                                            </Styles>
                                        </dxtv:ASPxGridView>
                                        <asp:SqlDataSource ID="dsEntregas" runat="server" DeleteCommand="DELETE FROM [ppj_Entregas] WHERE [CodigoRevisaoPPJ] = @CodigoRevisaoPPJ AND [CodigoProjeto] = @CodigoProjeto AND [CodigoTarefaCronograma] = @CodigoTarefaCronograma" InsertCommand="INSERT INTO [ppj_Entregas] ([CodigoRevisaoPPJ], [CodigoProjeto], [CodigoTarefaCronograma], [SequenciaTarefaCronograma], [SequenciaEntrega], [IdentificacaoEntrega], [DescricaoUnidadesRelacionadas], [PercentualRepresentacao], [DataInicio], [DataTermino], [DescricaoEntrega], [DescricaoCriterioAceitacao], [DataUltimaAlteracao], [CodigoUsuarioUltimaAlteracao]) VALUES (@CodigoRevisaoPPJ, @CodigoProjeto, @CodigoTarefaCronograma, @SequenciaTarefaCronograma, @SequenciaEntrega, @IdentificacaoEntrega, @DescricaoUnidadesRelacionadas, @PercentualRepresentacao, @DataInicio, @DataTermino, @DescricaoEntrega, @DescricaoCriterioAceitacao, @DataUltimaAlteracao, @CodigoUsuarioUltimaAlteracao)" SelectCommand="SELECT * FROM f_ppj_getDadosEntregas (@CodigoRevisaoPPJ, @CodigoProjeto) ORDER BY [SequenciaEntrega]" UpdateCommand="EXECUTE p_ppj_atualizaDadosEntrega
   @codigoRevisaoPPJ
  ,@codigoProjeto
  ,@CodigoCronogramaProjeto
  ,@CodigoTarefaCronograma
  ,@DescricaoUnidadesRelacionadas
  ,@PercentualRepresentacao
  ,@DescricaoEntrega
  ,@DescricaoCriterioAceitacao
  ,@CodigoUsuarioAtualizacao">
                                            <DeleteParameters>
                                                <asp:Parameter Name="CodigoRevisaoPPJ" Type="Int64" />
                                                <asp:Parameter Name="CodigoProjeto" Type="Int32" />
                                                <asp:Parameter Name="CodigoTarefaCronograma" Type="Int32" />
                                            </DeleteParameters>
                                            <InsertParameters>
                                                <asp:Parameter Name="CodigoRevisaoPPJ" Type="Int64" />
                                                <asp:Parameter Name="CodigoProjeto" Type="Int32" />
                                                <asp:Parameter Name="CodigoTarefaCronograma" Type="Int32" />
                                                <asp:Parameter Name="SequenciaTarefaCronograma" Type="Int32" />
                                                <asp:Parameter Name="SequenciaEntrega" Type="Int32" />
                                                <asp:Parameter Name="IdentificacaoEntrega" Type="String" />
                                                <asp:Parameter Name="DescricaoUnidadesRelacionadas" Type="String" />
                                                <asp:Parameter Name="PercentualRepresentacao" Type="Int32" />
                                                <asp:Parameter Name="DataInicio" Type="DateTime" />
                                                <asp:Parameter Name="DataTermino" Type="DateTime" />
                                                <asp:Parameter Name="DescricaoEntrega" Type="String" />
                                                <asp:Parameter Name="DescricaoCriterioAceitacao" Type="String" />
                                                <asp:Parameter Name="DataUltimaAlteracao" Type="DateTime" />
                                                <asp:Parameter Name="CodigoUsuarioUltimaAlteracao" Type="Int32" />
                                            </InsertParameters>
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" Type="Int32" />
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" Type="Int64" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:SessionParameter Name="codigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter DefaultValue="" Name="codigoProjeto" QueryStringField="cp" />
                                                <asp:Parameter Name="CodigoCronogramaProjeto" />
                                                <asp:Parameter Name="CodigoTarefaCronograma" />
                                                <asp:Parameter Name="DescricaoUnidadesRelacionadas" />
                                                <asp:Parameter Name="PercentualRepresentacao" />
                                                <asp:Parameter Name="DescricaoEntrega" />
                                                <asp:Parameter Name="DescricaoCriterioAceitacao" />
                                                <asp:SessionParameter Name="CodigoUsuarioAtualizacao" SessionField="CodigoUsuario" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </dxtv:ContentControl>
                        </ContentCollection>
                    </dxtv:TabPage>
                    <dxtv:TabPage Name="tabPlanos" Text="Planos">
                        <ContentCollection>
                            <dxtv:ContentControl runat="server">
                                <div class="tabContent">
                                    <div id="divMatrizResponsabilidade" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel18" runat="server" CssClass="tituloSecao" Text="Matriz de Responsabilidades">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxGridView ID="gvMatrizResponsabilidade" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvMatrizResponsabilidade" DataSourceID="dsMatrizResponsabilidade" Width="100%" KeyFieldName="SequenciaRegistro" OnCellEditorInitialize="gvMatrizResponsabilidade_CellEditorInitialize" OnLoad="gvMatrizResponsabilidade_Load" OnCustomCallback="gvMatrizResponsabilidade_CustomCallback" OnAfterPerformCallback="gvMatrizResponsabilidade_AfterPerformCallback" OnCustomJSProperties="gvMatrizResponsabilidade_CustomJSProperties">
                                            <ClientSideEvents ContextMenu="function(s, e) {
	if(e.objectType == &quot;header&quot;)
            pmColumnMenu.ShowAtPos(ASPxClientUtils.GetEventX(e.htmlEvent), ASPxClientUtils.GetEventY(e.htmlEvent)); 
}" />
                                            <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm">
                                            </SettingsEditing>
                                            <Settings HorizontalScrollBarMode="Auto" />
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="750px" />
                                            </SettingsPopup>
                                            <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                            <StylesEditors>
                                                <ReadOnly BackColor="LightGray">
                                                </ReadOnly>
                                            </StylesEditors>
                                            <Columns>
                                                <dxtv:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="60px" FixedStyle="Left">
                                                </dxtv:GridViewCommandColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Entregável / Grupo de Trabalho" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="IdentificacaoEntregavel" FixedStyle="Left" Width="200px">
                                                    <PropertiesTextEdit MaxLength="250">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="3" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Grupo de Trab X" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="DescricaoIntegrantesGrupo1">
                                                    <PropertiesMemoEdit MaxLength="4000" Rows="5" Width="100%">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Grupo de Trab X" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="DescricaoIntegrantesGrupo2">
                                                    <PropertiesMemoEdit MaxLength="4000" Rows="5" Width="100%">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Grupo de Trab X" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="DescricaoIntegrantesGrupo3">
                                                    <PropertiesMemoEdit MaxLength="4000" Rows="5" Width="100%">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Grupo de Trab X" ShowInCustomizationForm="True" VisibleIndex="5" FieldName="DescricaoIntegrantesGrupo4">
                                                    <PropertiesMemoEdit MaxLength="4000" Rows="5" Width="100%">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Grupo de Trab X" ShowInCustomizationForm="True" VisibleIndex="6" FieldName="DescricaoIntegrantesGrupo5">
                                                    <PropertiesMemoEdit MaxLength="4000" Rows="5" Width="100%">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Grupo de Trab X" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="DescricaoIntegrantesGrupo6">
                                                    <PropertiesMemoEdit MaxLength="4000" Rows="5" Width="100%">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Grupo de Trab X" ShowInCustomizationForm="True" VisibleIndex="8" FieldName="DescricaoIntegrantesGrupo7">
                                                    <PropertiesMemoEdit MaxLength="4000" Rows="5" Width="100%">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Grupo de Trab X" ShowInCustomizationForm="True" VisibleIndex="9" FieldName="DescricaoIntegrantesGrupo8">
                                                    <PropertiesMemoEdit MaxLength="4000" Rows="5" Width="100%">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Grupo de Trab X" ShowInCustomizationForm="True" VisibleIndex="10" FieldName="DescricaoIntegrantesGrupo9">
                                                    <PropertiesMemoEdit MaxLength="4000" Rows="5" Width="100%">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataTextColumn FieldName="IndicaNomeTarefaEditavel" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                </dxtv:GridViewDataTextColumn>
                                            </Columns>
                                            <Styles>
                                                <Header Wrap="True">
                                                </Header>
                                            </Styles>
                                        </dxtv:ASPxGridView>
                                        <asp:SqlDataSource ID="dsMatrizResponsabilidade" runat="server" SelectCommand="SELECT * FROM [dbo].[f_ppj_getDadosMtzRspIntegrantes] (@CodigoRevisaoPPJ,@CodigoProjeto)" DeleteCommand="EXECUTE [dbo].[p_ppj_excluiRegistroMtzRspIntegrantes] 
   @CodigoRevisaoPPJ
  ,@CodigoProjeto
  ,@SequenciaRegistro
  ,@CodigoUsuarioExclusao"
                                            InsertCommand="EXECUTE [dbo].[p_ppj_incluiRegistroMtzRspIntegrantes] 
   @CodigoRevisaoPPJ
  ,@CodigoProjeto
  ,@IdentificacaoEntregavel
  ,@DescricaoIntegrantesGrupo1
  ,@DescricaoIntegrantesGrupo2
  ,@DescricaoIntegrantesGrupo3
  ,@DescricaoIntegrantesGrupo4
  ,@DescricaoIntegrantesGrupo5
  ,@DescricaoIntegrantesGrupo6
  ,@DescricaoIntegrantesGrupo7
  ,@DescricaoIntegrantesGrupo8
  ,@DescricaoIntegrantesGrupo9
  ,@CodigoUsuarioAtualizacao"
                                            UpdateCommand="EXECUTE [p_ppj_AtualizaRegistroMtzRspIntegrantes] 
   @CodigoRevisaoPPJ
  ,@CodigoProjeto
  ,@SequenciaRegistro
  ,@IdentificacaoEntregavel
  ,@DescricaoIntegrantesGrupo1
  ,@DescricaoIntegrantesGrupo2
  ,@DescricaoIntegrantesGrupo3
  ,@DescricaoIntegrantesGrupo4
  ,@DescricaoIntegrantesGrupo5
  ,@DescricaoIntegrantesGrupo6
  ,@DescricaoIntegrantesGrupo7
  ,@DescricaoIntegrantesGrupo8
  ,@DescricaoIntegrantesGrupo9
  ,@CodigoUsuarioAtualizacao">
                                            <DeleteParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                                <asp:Parameter Name="SequenciaRegistro" />
                                                <asp:SessionParameter Name="CodigoUsuarioExclusao" SessionField="CodigoUsuario" />
                                            </DeleteParameters>
                                            <InsertParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                                <asp:Parameter Name="IdentificacaoEntregavel" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo1" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo2" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo3" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo4" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo5" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo6" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo7" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo8" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo9" />
                                                <asp:SessionParameter Name="CodigoUsuarioAtualizacao" SessionField="CodigoUsuario" />
                                            </InsertParameters>
                                            <SelectParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                                <asp:Parameter Name="SequenciaRegistro" />
                                                <asp:SessionParameter Name="CodigoUsuarioAtualizacao" SessionField="CodigoUsuario" />
                                                <asp:Parameter Name="IdentificacaoEntregavel" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo1" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo2" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo3" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo4" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo5" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo6" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo7" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo8" />
                                                <asp:Parameter Name="DescricaoIntegrantesGrupo9" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
                                        <dxtv:ASPxPopupMenu ID="pmColumnMenu" runat="server" ClientInstanceName="pmColumnMenu">
                                            <ClientSideEvents ItemClick="function(s, e) {
	if(e.item.name == 'cmdAlterarNomesColunas'){
showWindowByName('winAlterarNomesColunas');
}
}" />
                                            <Items>
                                                <dxtv:MenuItem Name="cmdAlterarNomesColunas" Text="Alterar nomes coluna">
                                                </dxtv:MenuItem>
                                            </Items>
                                        </dxtv:ASPxPopupMenu>
                                    </div>
                                    <div id="divPlanoRecursosHumanos" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel19" runat="server" CssClass="tituloSecao" Text="Plano de Recursos Humanos">
                                        </dxtv:ASPxLabel>
                                        <br />
                                        <dxtv:ASPxLabel ID="ASPxLabel35" runat="server" CssClass="tituloSecao" Text="Equipe do Projeto">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxGridView ID="gvEquipeProjeto" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvEquipeProjeto" Width="100%" DataSourceID="dsEquipeProjeto">
                                            <StylesEditors>
                                                <ReadOnly BackColor="LightGray">
                                                </ReadOnly>
                                            </StylesEditors>
                                            <Columns>
                                                <dxtv:GridViewDataTextColumn Caption="Nome" ShowInCustomizationForm="True" VisibleIndex="0" FieldName="NomeIntegrante">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Função" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="IdentificacaoFuncaoIntegrante">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="E-mail" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="EmailIntegrante">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Telefone" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="NumeroTelefoneIntegrante">
                                                </dxtv:GridViewDataTextColumn>
                                            </Columns>
                                        </dxtv:ASPxGridView>
                                        <asp:SqlDataSource ID="dsEquipeProjeto" runat="server" SelectCommand="SELECT * FROM [dbo].[f_ppj_getEquipeProjeto] (@CodigoRevisaoPPJ,@CodigoProjeto)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div id="divPlanoComunicacao" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel20" runat="server" CssClass="tituloSecao" Text="Plano de Comunicação">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxGridView ID="gvPlanoComunicacao" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvPlanoComunicacao" Width="100%" DataSourceID="dsPlanoComunicacao" KeyFieldName="SequenciaRegistro">
                                            <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm">
                                            </SettingsEditing>
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="600px" />
                                            </SettingsPopup>
                                            <EditFormLayoutProperties ColCount="3">
                                                <Items>
                                                    <dxtv:GridViewColumnLayoutItem ColSpan="3" ColumnName="IdentificacaoObjetoComunicacao">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColSpan="3" ColumnName="IdentificacaoAssunto">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColumnName="IdentificacaoMeioComunicacao">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColumnName="IdentificacaoPeriodicidade">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColumnName="IdentificacaoPrazoValidacao">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColSpan="3" ColumnName="IdentificacaoDestinatarios">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:GridViewColumnLayoutItem ColSpan="3" ColumnName="IdentificacaoResponsavel">
                                                        <CaptionSettings Location="Top" />
                                                    </dxtv:GridViewColumnLayoutItem>
                                                    <dxtv:EditModeCommandLayoutItem ColSpan="3" HorizontalAlign="Right">
                                                    </dxtv:EditModeCommandLayoutItem>
                                                </Items>
                                            </EditFormLayoutProperties>
                                            <StylesEditors>
                                                <ReadOnly BackColor="LightGray">
                                                </ReadOnly>
                                            </StylesEditors>
                                            <Columns>
                                                <dxtv:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="60px">
                                                </dxtv:GridViewCommandColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Objeto de comunicação" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="IdentificacaoObjetoComunicacao">
                                                    <PropertiesTextEdit MaxLength="250">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="3" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Assuntos abordados" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="IdentificacaoAssunto">
                                                    <PropertiesTextEdit MaxLength="150">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="3" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Meios Utilizados" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="IdentificacaoMeioComunicacao">
                                                    <PropertiesTextEdit MaxLength="150">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Periodicidade" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="IdentificacaoPeriodicidade">
                                                    <PropertiesTextEdit MaxLength="50">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Prazos de Validação" ShowInCustomizationForm="True" VisibleIndex="5" FieldName="IdentificacaoPrazoValidacao">
                                                    <PropertiesTextEdit MaxLength="50">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Responsável" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="IdentificacaoResponsavel">
                                                    <PropertiesTextEdit MaxLength="150">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="3" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Destinatários" FieldName="IdentificacaoDestinatarios" ShowInCustomizationForm="True" VisibleIndex="6">
                                                    <PropertiesMemoEdit MaxLength="500" Rows="5">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="3" />
                                                </dxtv:GridViewDataMemoColumn>
                                            </Columns>
                                            <Styles>
                                                <Header Wrap="True">
                                                </Header>
                                            </Styles>
                                        </dxtv:ASPxGridView>
                                        <asp:SqlDataSource ID="dsPlanoComunicacao" runat="server" DeleteCommand="EXECUTE [dbo].[p_ppj_excluiRegistroPlanoComunicacao] 
   @CodigoRevisaoPPJ
  ,@CodigoProjeto
  ,@SequenciaRegistro
  ,@CodigoUsuarioExclusao"
                                            InsertCommand="EXECUTE [dbo].[p_ppj_incluiRegistroPlanoComunicacao] 
   @CodigoRevisaoPPJ
  ,@CodigoProjeto
  ,@SequenciaRegistro
  ,@IdentificacaoObjetoComunicacao
  ,@IdentificacaoAssunto
  ,@IdentificacaoMeioComunicacao
  ,@IdentificacaoPeriodicidade
  ,@IdentificacaoPrazoValidacao
  ,@IdentificacaoDestinatarios
  ,@IdentificacaoResponsavel
  ,@CodigoUsuarioAtualizacao"
                                            SelectCommand="SELECT * FROM [dbo].[f_ppj_getDadosPlanoComunicacao] (@CodigoRevisaoPPJ,@CodigoProjeto)" UpdateCommand="EXECUTE p_ppj_AtualizaRegistroPlanoComunicacao
   @CodigoRevisaoPPJ
  ,@CodigoProjeto
  ,@SequenciaRegistro
  ,@IdentificacaoObjetoComunicacao
  ,@IdentificacaoAssunto
  ,@IdentificacaoMeioComunicacao
  ,@IdentificacaoPeriodicidade
  ,@IdentificacaoPrazoValidacao
  ,@IdentificacaoDestinatarios
  ,@IdentificacaoResponsavel
  ,@CodigoUsuarioAtualizacao">
                                            <DeleteParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                                <asp:Parameter Name="SequenciaRegistro" />
                                                <asp:SessionParameter Name="CodigoUsuarioExclusao" SessionField="CodigoUsuario" />
                                            </DeleteParameters>
                                            <InsertParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                                <asp:Parameter Name="SequenciaRegistro" />
                                                <asp:Parameter Name="IdentificacaoObjetoComunicacao" />
                                                <asp:Parameter Name="IdentificacaoAssunto" />
                                                <asp:Parameter Name="IdentificacaoMeioComunicacao" />
                                                <asp:Parameter Name="IdentificacaoPeriodicidade" />
                                                <asp:Parameter Name="IdentificacaoPrazoValidacao" />
                                                <asp:Parameter Name="IdentificacaoDestinatarios" />
                                                <asp:Parameter Name="IdentificacaoResponsavel" />
                                                <asp:SessionParameter Name="CodigoUsuarioAtualizacao" SessionField="CodigoUsuario" />
                                            </InsertParameters>
                                            <SelectParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                                <asp:Parameter Name="SequenciaRegistro" />
                                                <asp:Parameter Name="IdentificacaoObjetoComunicacao" />
                                                <asp:Parameter Name="IdentificacaoAssunto" />
                                                <asp:Parameter Name="IdentificacaoMeioComunicacao" />
                                                <asp:Parameter Name="IdentificacaoPeriodicidade" />
                                                <asp:Parameter Name="IdentificacaoPrazoValidacao" />
                                                <asp:Parameter Name="IdentificacaoDestinatarios" />
                                                <asp:Parameter Name="IdentificacaoResponsavel" />
                                                <asp:SessionParameter Name="CodigoUsuarioAtualizacao" SessionField="CodigoUsuario" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </dxtv:ContentControl>
                        </ContentCollection>
                    </dxtv:TabPage>
                    <dxtv:TabPage Name="tabRiscos" Text="Riscos e Interdependências">
                        <ContentCollection>
                            <dxtv:ContentControl runat="server">
                                <div class="tabContent">
                                    <div id="divAnaliseRiscos" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel21" runat="server" CssClass="tituloSecao" Text="Análise de Riscos">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxGridView ID="gvAnaliseRiscos" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvAnaliseRicos" Width="100%" DataSourceID="dsAnaliseRiscos" KeyFieldName="CodigoRisco">
                                            <StylesEditors>
                                                <ReadOnly BackColor="LightGray">
                                                </ReadOnly>
                                            </StylesEditors>
                                            <Columns>
                                                <dxtv:GridViewDataTextColumn Caption="Risco Identificado" FieldName="IdentificacaoRisco" ShowInCustomizationForm="True" VisibleIndex="1">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Probabilidade" FieldName="GrauProbabilidadeRisco" ShowInCustomizationForm="True" VisibleIndex="2" Width="90px">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Impacto" FieldName="GrauImpactoRisco" ShowInCustomizationForm="True" VisibleIndex="3" Width="75px">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Criticidade" FieldName="GrauCriticidadeRisco" ShowInCustomizationForm="True" VisibleIndex="4" Width="75px">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Classificação" FieldName="IdentificacaoCategoriaRisco" ShowInCustomizationForm="True" VisibleIndex="5">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Estratégia" FieldName="IdentificacaoTipoResposta" ShowInCustomizationForm="True" VisibleIndex="6" Width="75px">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Ação Proposta" FieldName="DescricaoAcaoProposta" ShowInCustomizationForm="True" VisibleIndex="7">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Responsável" FieldName="NomeUsuarioResponsavel" ShowInCustomizationForm="True" VisibleIndex="8">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Data de conclusão" FieldName="DataLimiteTratamento" ShowInCustomizationForm="True" VisibleIndex="9" Width="75px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:d}">
                                                    </PropertiesTextEdit>
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Status do Risco" FieldName="IdentificacaoStatusRisco" ShowInCustomizationForm="True" VisibleIndex="10" Width="100px">
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Número" FieldName="SequenciaRisco" ShowInCustomizationForm="True" VisibleIndex="0" Width="50px">
                                                </dxtv:GridViewDataTextColumn>
                                            </Columns>
                                            <Styles>
                                                <Header Wrap="True">
                                                </Header>
                                            </Styles>
                                        </dxtv:ASPxGridView>
                                        <asp:SqlDataSource ID="dsAnaliseRiscos" runat="server" SelectCommand="SELECT * FROM f_ppj_getDadosRiscos(@CodigoRevisaoPPJ, @CodigoProjeto)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                    <div id="divRelacionamento" class="secaoFormulario">
                                        <dxtv:ASPxLabel ID="ASPxLabel22" runat="server" CssClass="tituloSecao" Text="Relacionamento com outros projetos">
                                        </dxtv:ASPxLabel>
                                        <dxtv:ASPxGridView ID="gvRelacionamento" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvRelacionamento" Width="100%" DataSourceID="dsRelacionamento" KeyFieldName="CodigoProjetoRelacionado">
                                            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm">
                                            </SettingsEditing>
                                            <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="600px" />
                                            </SettingsPopup>
                                            <StylesEditors>
                                                <ReadOnly BackColor="LightGray">
                                                </ReadOnly>
                                            </StylesEditors>
                                            <Columns>
                                                <dxtv:GridViewCommandColumn ShowEditButton="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="30px">
                                                </dxtv:GridViewCommandColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Descrição do relacionamento" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="IdentificacaoRelacionamento" ReadOnly="True">
                                                    <PropertiesTextEdit MaxLength="30">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataMemoColumn Caption="Plano de Ação" FieldName="DescricaoPlanoAcao" ShowInCustomizationForm="True" VisibleIndex="4">
                                                    <PropertiesMemoEdit MaxLength="8000" Rows="5">
                                                    </PropertiesMemoEdit>
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataMemoColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Projeto Relacionado" FieldName="CodigoProjetoRelacionado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                    <PropertiesTextEdit>
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" Visible="False" />
                                                </dxtv:GridViewDataTextColumn>
                                                <dxtv:GridViewDataTextColumn Caption="Projeto Relacionado" FieldName="IdentificacaoProjetoRelacionado" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2">
                                                    <EditFormSettings CaptionLocation="Top" />
                                                </dxtv:GridViewDataTextColumn>
                                            </Columns>
                                            <Styles>
                                                <Header Wrap="True">
                                                </Header>
                                            </Styles>
                                        </dxtv:ASPxGridView>
                                        <asp:SqlDataSource ID="dsRelacionamento" runat="server" SelectCommand="select * from f_ppj_getDadosRelacionamentoProjeto(@CodigoRevisaoPPJ, @CodigoProjeto)" UpdateCommand="EXEC p_ppj_AtualizaRegistroRelacionamentoProjeto 
@in_codigoRevisaoPPJ = @CodigoRevisaoPPJ, 
@in_codigoProjeto = @CodigoProjeto,
@in_codigoProjetoRelacionado = @CodigoProjetoRelacionado,
@in_descricaoPlanoAcao = @DescricaoPlanoAcao,
@in_codigoUsuarioAtualizacao = @CodigoUsuarioAtualizacao">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="in_codigoRevisaoPPJ" Type="Int64" />
                                                <asp:Parameter Name="in_codigoProjeto" Type="Int32" />
                                                <asp:Parameter Name="in_codigoProjetoRelacionado" Type="Int32" />
                                                <asp:Parameter Name="in_descricaoPlanoAcao" Type="String" />
                                                <asp:Parameter Name="in_codigoUsuarioAtualizacao" Type="Int32" />
                                                <asp:SessionParameter Name="CodigoRevisaoPPJ" SessionField="CodigoRevisaoPPJ" />
                                                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="cp" />
                                                <asp:Parameter Name="CodigoProjetoRelacionado" />
                                                <asp:Parameter Name="DescricaoPlanoAcao" />
                                                <asp:SessionParameter Name="CodigoUsuarioAtualizacao" SessionField="CodigoUsuario" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
                                        <asp:SqlDataSource ID="dsProjetos" runat="server"></asp:SqlDataSource>
                                    </div>
                                </div>
                            </dxtv:ContentControl>
                        </ContentCollection>
                    </dxtv:TabPage>
                </TabPages>
                <ClientSideEvents Init="function(s, e) {
    DefineAltura();
}" />
                <ContentStyle>
                    <Paddings Padding="5px" />
                </ContentStyle>
            </dxtv:ASPxPageControl>
            <div style="padding-top: 10px">
                <table style="margin-left: auto">
                    <tr>
                        <td>
                            <dxcp:ASPxButton ID="btnSalvar" runat="server" Text="Salvar" AutoPostBack="false"  Width="100px">
                                <ClientSideEvents Click="function(s, e) {
	callback.PerformCallback('');
}" />
                            </dxcp:ASPxButton>
                            <dxtv:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                                <ClientSideEvents CallbackComplete="function(s, e) {
	if(s.cp_status == 'ok')
            window.top.mostraMensagem(e.result, 'sucesso', false, false, null);
        else
            window.top.mostraMensagem(e.result, 'erro', true, false, null);
}" />
                            </dxtv:ASPxCallback>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <dxcp:ASPxPopupControl ID="popup" runat="server" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popup">
            <Windows>
                <dxtv:PopupWindow CloseAction="CloseButton" HeaderText="Alterar nomes colunas" Modal="True" Name="winAlterarNomesColunas" Width="400px">
                    <ContentCollection>
                        <dxtv:PopupControlContentControl runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel61" runat="server" Text="Grupo 1">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtGrupo1" runat="server" ClientInstanceName="txtGrupo1" MaxLength="50" Width="100%">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel62" runat="server" Text="Grupo 2">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtGrupo2" runat="server" ClientInstanceName="txtGrupo2" MaxLength="50" Width="100%">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel63" runat="server" Text="Grupo 3">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtGrupo3" runat="server" ClientInstanceName="txtGrupo3" MaxLength="50" Width="100%">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel64" runat="server" Text="Grupo 4">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtGrupo4" runat="server" ClientInstanceName="txtGrupo4" MaxLength="50" Width="100%">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">
                                        <dxtv:ASPxLabel ID="ASPxLabel65" runat="server" Text="Grupo 5">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtGrupo5" runat="server" ClientInstanceName="txtGrupo5" MaxLength="50" Width="100%">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel66" runat="server" Text="Grupo 6">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtGrupo6" runat="server" ClientInstanceName="txtGrupo6" MaxLength="50" Width="100%">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel67" runat="server" Text="Grupo 7">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtGrupo7" runat="server" ClientInstanceName="txtGrupo7" MaxLength="50" Width="100%">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel68" runat="server" Text="Grupo 8">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtGrupo8" runat="server" ClientInstanceName="txtGrupo8" MaxLength="50" Width="100%">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxLabel ID="ASPxLabel69" runat="server" Text="Grupo 9">
                                        </dxtv:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxtv:ASPxTextBox ID="txtGrupo9" runat="server" ClientInstanceName="txtGrupo9" MaxLength="50" Width="100%">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                            </ValidationSettings>
                                        </dxtv:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="margin-left: auto">
                                            <tr>
                                                <td>
                                                    <dxtv:ASPxButton ID="btnSalvarNomesColunas" runat="server" AutoPostBack="False" ClientInstanceName="btnSalvarNomesColunas" Text="Salvar">
                                                        <ClientSideEvents Click="function(s, e) {
	SalvarAlteracaoNomesColunas();
}" />
                                                    </dxtv:ASPxButton>
                                                </td>
                                                <td>
                                                    <dxtv:ASPxButton ID="btnCancelar" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cancelar">
                                                        <ClientSideEvents Click="function(s, e) {
            hideWindowByName('winAlterarNomesColunas');
}" />
                                                    </dxtv:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </dxtv:PopupControlContentControl>
                    </ContentCollection>
                </dxtv:PopupWindow>
            </Windows>
            <ClientSideEvents PopUp="function(s, e) {
    ASPxClientEdit.ClearEditorsInContainer(null);
	txtGrupo1.SetText(gvMatrizResponsabilidade.cpColGrupo1);
	txtGrupo2.SetText(gvMatrizResponsabilidade.cpColGrupo2);
	txtGrupo3.SetText(gvMatrizResponsabilidade.cpColGrupo3);
	txtGrupo4.SetText(gvMatrizResponsabilidade.cpColGrupo4);
	txtGrupo5.SetText(gvMatrizResponsabilidade.cpColGrupo5);
	txtGrupo6.SetText(gvMatrizResponsabilidade.cpColGrupo6);
	txtGrupo7.SetText(gvMatrizResponsabilidade.cpColGrupo7);
	txtGrupo8.SetText(gvMatrizResponsabilidade.cpColGrupo8);
	txtGrupo9.SetText(gvMatrizResponsabilidade.cpColGrupo9);
}" />
            <ContentCollection>
                <dxcp:PopupControlContentControl runat="server"></dxcp:PopupControlContentControl>
            </ContentCollection>
        </dxcp:ASPxPopupControl>
    </form>
</body>
</html>
