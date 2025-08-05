<%@ Page Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true"
    CodeFile="OLAP_TarefasAtribuicoes.aspx.cs" Inherits="_Projetos_Relatorios_OLAP_ContratosEstendida"
    Title="Portal da Estratégia" %>

<%@ MasterType VirtualPath="~/novaCdis.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
            width: 100%">
            <tr style="height:26px">
                <td valign="middle">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(../../imagens/titulo/back_Titulo_Desktop.gif);
                        width: 99%">
                        <tr style="height:26px">
                            <td valign="middle" style="height: 26px">
                                <asp:Label ID="lblTituloTela" runat="server" EnableViewState="False" Font-Bold="True"
                                    Font-Overline="False" Font-Strikeout="False"
                                    Text="Análise de Tarefas"></asp:Label>
                            </td>
                            <td valign="middle" style="padding-right: 5px; height: 26px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" 
                                    Text="Tarefas com término entre:">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 105px; padding-right: 5px; height: 26px;" align="right" valign="middle">
                                <dxe:ASPxDateEdit ID="dteInicio" runat="server" Font-Overline="False"
                                    Width="105px" AllowNull="False" ClientInstanceName="dteInicio">
                                    <CalendarProperties>
                                        <DayHeaderStyle  />
                                        <DayStyle  />
                                        <DaySelectedStyle >
                                        </DaySelectedStyle>
                                        <DayOtherMonthStyle >
                                        </DayOtherMonthStyle>
                                        <DayWeekendStyle >
                                        </DayWeekendStyle>
                                        <DayOutOfRangeStyle >
                                        </DayOutOfRangeStyle>
                                        <ButtonStyle >
                                        </ButtonStyle>
                                        <FastNavStyle >
                                        </FastNavStyle>
                                        <FastNavMonthAreaStyle >
                                        </FastNavMonthAreaStyle>
                                        <FastNavMonthStyle >
                                        </FastNavMonthStyle>
                                        <FastNavFooterStyle >
                                        </FastNavFooterStyle>
                                        <Style >
                                        </Style>
                                    </CalendarProperties>
                                </dxe:ASPxDateEdit>
                            </td>
                            <td style="width: 10px; padding-right: 5px; height: 26px;" align="right">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" 
                                    Text="e" Width="10px">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 105px; padding-right: 5px; height: 26px;" align="right" valign="middle">
                                <dxe:ASPxDateEdit ID="dteFim" runat="server" 
                                    Font-Strikeout="False" Width="105px" AllowNull="False" ClientInstanceName="dteFim">
                                    <CalendarProperties>
                                        <DayHeaderStyle  />
                                        <WeekNumberStyle >
                                        </WeekNumberStyle>
                                        <DayStyle  />
                                        <DaySelectedStyle >
                                        </DaySelectedStyle>
                                        <DayOtherMonthStyle >
                                        </DayOtherMonthStyle>
                                        <DayWeekendStyle >
                                        </DayWeekendStyle>
                                        <DayOutOfRangeStyle >
                                        </DayOutOfRangeStyle>
                                        <TodayStyle >
                                        </TodayStyle>
                                        <ButtonStyle >
                                        </ButtonStyle>
                                        <HeaderStyle  />
                                        <FooterStyle  />
                                        <FastNavStyle >
                                        </FastNavStyle>
                                        <FastNavMonthAreaStyle >
                                        </FastNavMonthAreaStyle>
                                        <FastNavYearAreaStyle >
                                        </FastNavYearAreaStyle>
                                        <FastNavMonthStyle >
                                        </FastNavMonthStyle>
                                        <FastNavYearStyle >
                                        </FastNavYearStyle>
                                        <FastNavFooterStyle >
                                        </FastNavFooterStyle>
                                        <ReadOnlyStyle >
                                        </ReadOnlyStyle>
                                        <InvalidStyle >
                                        </InvalidStyle>
                                        <Style >
                                        </Style>
                                    </CalendarProperties>
                                    <ClientSideEvents ButtonClick="function(s, e) {
btnSelecionar.SetEnabled(true);
}" ValueChanged="function(s, e) {
	btnSelecionar.SetEnabled(true);
}" />
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="left" style="width: 85px; height: 26px;" valign="middle">
                                <dxe:ASPxButton ID="btnSelecionar" runat="server" Text="Selecionar"
                                    AutoPostBack="False" Width="85px" ClientInstanceName="btnSelecionar">
                                    <ClientSideEvents Click="function(s, e) {
	var mensagemErro_ValidaCamposFormulario = '';
    if(dteInicio.GetValue()!= null &amp;&amp; dteFim.GetValue()  != null)
    {
        var dataAtual 	 = new Date();
	    var meuDataAtual = (dataAtual.getMonth() +1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
	    var dataHoje 	 = Date.parse(meuDataAtual);
        
        var dataInicio 	  = new Date(dteInicio.GetValue());
		var meuDataInicio = (dataInicio.getMonth() +1) + '/' + dataInicio.getDate() + '/' + dataInicio.getFullYear();
		dataInicio  	  = Date.parse(meuDataInicio);
		
		var dataTermino 	= new Date(dteFim.GetValue());
		var meuDataTermino 	= (dataTermino.getMonth() +1) + '/' + dataTermino.getDate() + '/' + dataTermino.getFullYear();
		dataTermino		    = Date.parse(meuDataTermino);
		
		if(dataInicio &gt; dataTermino)
        {
            mensagemErro_ValidaCamposFormulario =  &quot;A Data de Início não pode ser maior que a Data de Término!\n&quot;;
        }    
	}
   if(mensagemErro_ValidaCamposFormulario == '')
   {
        grid.PerformCallback(&quot;PopularGrid&quot;);
   }
   else
   {
      window.top.mostraMensagem(mensagemErro_ValidaCamposFormulario, 'atencao', true, false, null);
      return false;
   }

}"></ClientSideEvents>
                                    <Paddings Padding="0px" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 5px" valign="top">
                        </td>
                        <td valign="top">
                            <dxpgwx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="grid"
                                >
                            </dxpgwx:ASPxPivotGridExporter>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td valign="top">
                            <table>
                                <tr>
                                    <td style="width: 205px">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxComboBox ID="ddlExporta" runat="server" ClientInstanceName="ddlExporta"
                                                         ValueType="System.String">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnImage.PerformCallback(s.GetValue());
	hfGeral.Set(&quot;tipoArquivo&quot;,s.GetValue());
}" />
                                                    </dxe:ASPxComboBox>
                                                </td>
                                                <td style="padding-left: 2px">
                                                    <dxcp:ASPxCallbackPanel ID="pnImage" runat="server" ClientInstanceName="pnImage"
                                                        Height="22px" HideContentOnCallback="False" OnCallback="pnImage_Callback" 
                                                         Width="23px">
                                                        <PanelCollection>
                                                            <dxp:PanelContent ID="PanelContent1" runat="server">
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
                                        <dxe:ASPxButton ID="Aspxbutton1" runat="server" 
                                            OnClick="btnExcel_Click" Text="Exportar">
                                            <Paddings Padding="0px" />
                                        </dxe:ASPxButton>
                                        <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
                                        </dxhf:ASPxHiddenField>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px; height: 3px" valign="top">
                        </td>
                        <td style="height: 3px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td colspan="1" valign="top">
                            <div id="Div2" style="overflow: auto; height: <%=alturaTabela %>; width: <%=larguraTabela %>">
                                <dxwpg:ASPxPivotGrid ID="grid" runat="server" ClientInstanceName="grid"
                                    Width="98%" ClientIDMode="AutoID" OnCustomFieldSort="grid_CustomFieldSort" OnCustomFilterPopupItems="grid_CustomFilterPopupItems" OnFieldFilterChanging="grid_FieldFilterChanging">
                                    <Fields>
                                        <dxwpg:PivotGridField FieldName="NomeUnidadeNegocio" ID="field1" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" AreaIndex="1" Caption="Unidade" Visible="False">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                            <ValueTotalStyle >
                                            </ValueTotalStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="NomePrograma" ID="field2" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" AreaIndex="2" Caption="Programa" Visible="False">
                                            <CellStyle >
                                            </CellStyle>
                                            <HeaderStyle ></HeaderStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="NomeProjeto" ID="field3" AllowedAreas="RowArea, FilterArea"
                                            Caption="Projeto" Options-ShowInCustomizationForm="False" AreaIndex="1" Area="RowArea">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="NomeTarefaSuperior" ID="field4" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" AreaIndex="2" Caption="Nome Tarefa Superior" Visible="False">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="NomeTarefa" ID="fldSNomeTarefa" AllowedAreas="RowArea"
                                            AreaIndex="2" Caption="Nome Tarefa" Area="RowArea" SortMode="Custom">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="Inicio" ID="fieldInicio" AllowedAreas="RowArea"
                                            Area="RowArea" AreaIndex="3" Caption="Início" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="InicioReal" ID="PivotGridField1" AllowedAreas="RowArea"
                                            Area="RowArea" AreaIndex="6" Visible="False" Caption="Início Real" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="InicioLB" ID="PivotGridField2" AllowedAreas="RowArea"
                                            Area="RowArea" AreaIndex="6" Visible="False" Caption="Início Linha de Base" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="Termino" ID="PivotGridField3" AllowedAreas="RowArea"
                                            Area="RowArea" AreaIndex="4" Caption="Término" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="TerminoReal" ID="PivotGridField4" AllowedAreas="RowArea"
                                            Area="RowArea" AreaIndex="6" Visible="False" Caption="Término Real" CellFormat-FormatString="dd/MM/yyyy"
                                            CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy" TotalCellFormat-FormatType="DateTime"
                                            GrandTotalCellFormat-FormatString="dd/MM/yyyy" GrandTotalCellFormat-FormatType="DateTime"
                                            ValueFormat-FormatString="dd/MM/yyyy" ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField FieldName="TerminoLB" ID="PivotGridField5" AllowedAreas="RowArea"
                                            Area="RowArea" AreaIndex="6" Visible="False" Caption="Término Linha de Base"
                                            CellFormat-FormatString="dd/MM/yyyy" CellFormat-FormatType="DateTime" TotalCellFormat-FormatString="dd/MM/yyyy"
                                            TotalCellFormat-FormatType="DateTime" GrandTotalCellFormat-FormatString="dd/MM/yyyy"
                                            GrandTotalCellFormat-FormatType="DateTime" ValueFormat-FormatString="dd/MM/yyyy"
                                            ValueFormat-FormatType="DateTime" TotalValueFormat-FormatString="dd/MM/yyyy"
                                            TotalValueFormat-FormatType="DateTime">
                                            <CellStyle >
                                            </CellStyle>
                                            <ValueStyle >
                                            </ValueStyle>
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldPercentualRealTarefa" AllowedAreas="RowArea" Area="RowArea"
                                            AreaIndex="6" Caption="% Concluído" CellFormat-FormatString="{0:n2}%" CellFormat-FormatType="Numeric"
                                            FieldName="PercentualRealTarefa" GrandTotalCellFormat-FormatString="{0:n2}%"
                                            GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n2}%"
                                            TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:n2}%"
                                            TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n2}%" ValueFormat-FormatType="Numeric">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldNomeRecurso" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" AreaIndex="7" Caption="Nome Recurso" 
                                            FieldName="NomeRecurso">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTrabalho" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="0" Caption="Trabalho" CellFormat-FormatString="{0:n2}" CellFormat-FormatType="Numeric"
                                            FieldName="Trabalho" GrandTotalCellFormat-FormatString="{0:n2}" GrandTotalCellFormat-FormatType="Numeric"
                                            TotalCellFormat-FormatString="{0:n2}" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:n2}"
                                            TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n2}" ValueFormat-FormatType="Numeric">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="PivotGridField6" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="0" Caption="Trabalho Real" CellFormat-FormatString="{0:n2}" CellFormat-FormatType="Numeric"
                                            FieldName="TrabalhoReal" GrandTotalCellFormat-FormatString="{0:n2}" GrandTotalCellFormat-FormatType="Numeric"
                                            TotalCellFormat-FormatString="{0:n2}" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:n2}"
                                            TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n2}" ValueFormat-FormatType="Numeric"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="PivotGridField7" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="3" Caption="Trabalho Linha de Base" CellFormat-FormatString="{0:n2}"
                                            CellFormat-FormatType="Numeric" FieldName="TrabalhoLB" GrandTotalCellFormat-FormatString="{0:n2}"
                                            GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n2}"
                                            TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:n2}" TotalValueFormat-FormatType="Numeric"
                                            ValueFormat-FormatString="{0:n2}" ValueFormat-FormatType="Numeric" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="PivotGridField8" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="0" Caption="Trabalho Restante" CellFormat-FormatString="{0:n2}" CellFormat-FormatType="Numeric"
                                            FieldName="TrabalhoRestante" GrandTotalCellFormat-FormatString="{0:n2}" GrandTotalCellFormat-FormatType="Numeric"
                                            TotalCellFormat-FormatString="{0:n2}" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:n2}"
                                            TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n2}" ValueFormat-FormatType="Numeric"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="PivotGridField9" AllowedAreas="DataArea" Area="DataArea"
                                            AreaIndex="0" Caption="Variação Trabalho" CellFormat-FormatString="{0:n2}" CellFormat-FormatType="Numeric"
                                            FieldName="VariacaoTrabalho" GrandTotalCellFormat-FormatString="{0:n2}" GrandTotalCellFormat-FormatType="Numeric"
                                            TotalCellFormat-FormatString="{0:n2}" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:n2}"
                                            TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n2}" ValueFormat-FormatType="Numeric"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldAnotacoes" AllowedAreas="RowArea" Area="RowArea"
                                            AreaIndex="7" Caption="Anotações" FieldName="Anotacoes" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldIndicaAtraso" AllowedAreas="RowArea, FilterArea" Area="RowArea"
                                            AreaIndex="5" Caption="Atrasada?" FieldName="IndicaAtraso">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldNomeGerente" AllowedAreas="RowArea, FilterArea" Area="RowArea"
                                            AreaIndex="5" Caption="Nome do Gerente do Projeto" FieldName="NomeGerente" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldIndicaMarco" AllowedAreas="RowArea, FilterArea" Area="RowArea"
                                            AreaIndex="5" Caption="Marco?" FieldName="IndicaMarco" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldIndicaTarefaCritica" AllowedAreas="RowArea, FilterArea"
                                            Area="RowArea" AreaIndex="5" Caption="Crítica?" FieldName="IndicaTarefaCritica"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldNomeStatus" AllowedAreas="RowArea, FilterArea" Area="RowArea"
                                            AreaIndex="0" Caption="Status do Projeto" FieldName="NomeStatus">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTipoProjeto" AllowedAreas="RowArea, FilterArea" Area="RowArea"
                                            AreaIndex="5" Caption="Tipo de Projeto" FieldName="TipoProjeto" Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldPercentualPrevistoTarefa" AllowedAreas="RowArea" Area="RowArea"
                                            AreaIndex="7" Caption="% Previsto" CellFormat-FormatString="{0:n2}%" CellFormat-FormatType="Numeric"
                                            FieldName="PercentualPrevistoTarefa" GrandTotalCellFormat-FormatString="{0:n2}%"
                                            GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatString="{0:n2}%"
                                            TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatString="{0:n2}%"
                                            TotalValueFormat-FormatType="Numeric" ValueFormat-FormatString="{0:n2}%" ValueFormat-FormatType="Numeric"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxwpg:PivotGridField ID="fieldTipoTarefa" AllowedAreas="RowArea, ColumnArea, FilterArea"
                                            Area="RowArea" AreaIndex="7" Caption="Tipo de Tarefa" FieldName="TipoTarefa"
                                            Visible="False">
                                        </dxwpg:PivotGridField>
                                        <dxpgwx:PivotGridField ID="fieldIDTarefa" FieldName="IDTarefa" Options-ShowInCustomizationForm="False" Options-ShowInPrefilter="False" Visible="False">
                                        </dxpgwx:PivotGridField>
                                    </Fields>
                                    <Styles>
                                        <HeaderStyle ></HeaderStyle>
                                        <AreaStyle >
                                        </AreaStyle>
                                        <DataAreaStyle >
                                        </DataAreaStyle>
                                        <ColumnAreaStyle >
                                        </ColumnAreaStyle>
                                        <RowAreaStyle >
                                        </RowAreaStyle>
                                        <FieldValueStyle >
                                        </FieldValueStyle>
                                        <CellStyle >
                                        </CellStyle>
                                        <TotalCellStyle >
                                        </TotalCellStyle>
                                        <GrandTotalCellStyle >
                                        </GrandTotalCellStyle>
                                        <CustomTotalCellStyle >
                                        </CustomTotalCellStyle>
                                        <FilterWindowStyle >
                                        </FilterWindowStyle>
                                        <MenuStyle ></MenuStyle>
                                        <CustomizationFieldsStyle >
                                        </CustomizationFieldsStyle>
                                        <CustomizationFieldsContentStyle >
                                        </CustomizationFieldsContentStyle>
                                        <CustomizationFieldsHeaderStyle >
                                        </CustomizationFieldsHeaderStyle>
                                    </Styles>
                                    <OptionsPager Visible="False">
                                    </OptionsPager>
                                    <StylesEditors>
                                        <DropDownWindow >
                                        </DropDownWindow>
                                    </StylesEditors>
                                </dxwpg:ASPxPivotGrid>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                        </td>
                        <td valign="middle">
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" EnableViewState="False" Font-Bold="True"
                                Font-Overline="False" Font-Strikeout="False" Text="S/I = Sem Informação"></asp:Label>
                        </td>
                        <td valign="top">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
