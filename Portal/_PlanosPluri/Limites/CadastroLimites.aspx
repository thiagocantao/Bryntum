<%@ Page Title="" Language="C#" MasterPageFile="~/novaCdis.master" AutoEventWireup="true" CodeFile="CadastroLimites.aspx.cs" Inherits="_PlanosPluri_Limites_CadastroLimites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../../scripts/jquery.ultima.js"></script>
    <script type="text/javascript">
        var endPosition = 0;
        var startPosition = 0;
        function onRowDblClick(s, e) {
            var siglaCampo = "[" + s.GetRowKey(e.visibleIndex) + "]";
            var formula = txtFormula.GetText();
            var formula = formula.substring(0, startPosition) + siglaCampo + formula.substring(endPosition);
            txtFormula.SetText(formula);
            txtFormula.SetCaretPosition(endPosition + siglaCampo.length);
        }

        function onLostFocus(s, e) {
            endPosition = $(txtFormula.GetMainElement()).find("textarea").prop("selectionEnd");
            startPosition = $(txtFormula.GetMainElement()).find("textarea").prop("selectionStart");
        }

        function onClick(s, e) {
            if (VerificaFormulaValida()) {
                txtFormula_editor.SetText(txtFormula.GetText());
                fecharEditorFormulas();
            }
        }

        function VerificaFormulaValida() {
            var initRange = 1;
            var endRange = 100;
            var val = rand(initRange, endRange);
            var keys = gvCamposLimite.cpKeys;
            var obj = {};
            var funcReplace = function (match, capture) {
                if (keys.indexOf(capture) == -1)
                    throw new Error("Um campo não cadastrado foi utilizado na fórmula.");

                if (obj[capture] == undefined) {
                    obj[capture] = val;
                    val = rand(val + initRange, val + endRange);
                }
                return obj[capture];
            };
            try {
                var formula = txtFormula.GetText();
                formula = formula.replace(/(\])/g, "] ");
                formula = formula.replace(/\[(.+?)\]/g, funcReplace);
                var result = eval(formula);

                if (isNaN(result)) {
                    window.top.mostraMensagem('Não é uma expressão numérica válida', 'atencao', true, false, null);
                    return false;
                }

                if (Math.abs(result) == Infinity) {
                    window.top.mostraMensagem('A fórmula resulta em uma divisão por 0', 'atencao', true, false, null);
                    return false;
                }
            } catch (ex) {
                window.top.mostraMensagem('Não foi possível avaliar a expressão. \n\n' + ex.message, 'atencao', true, false, null);
                return false;
            }

            return true;
        }

        function rand(min, max) {
            return Math.floor((Math.random() * (max - min + 1)) + min);
        }

        function abrirEditorFormulas() {
            var window = popup.GetWindowByName('winEditor');
            popup.ShowWindow(window);
        }

        function fecharEditorFormulas() {
            var window = popup.GetWindowByName('winEditor');
            popup.HideWindow(window);
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AreaTrabalho" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-image: url(../imagens/titulo/back_Titulo_Desktop.gif);">
        <tr style="height: 26px">
            <td valign="middle" style="padding-left: 10px">
                <asp:Label ID="lblTituloTela" runat="server" Font-Bold="True"
                    Font-Overline="False" Font-Strikeout="False" Text="Limites de Plano Plurianual"
                    EnableViewState="False"></asp:Label>
            </td>
            <td align="left" valign="middle"></td>
        </tr>
    </table>
    <table cellspacing="0" class="auto-style1">
        <tr>
            <td style="padding: 5px; padding-bottom: 0px">
    <dxcp:ASPxGridView ID="gvLimite" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvLimite" DataSourceID="dataSource" KeyFieldName="CodigoLimite" Width="100%" OnCellEditorInitialize="gvLimite_CellEditorInitialize">
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <SettingsEditing EditFormColumnCount="3" Mode="PopupEditForm">
        </SettingsEditing>
        <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="900px" />
        </SettingsPopup>
        <EditFormLayoutProperties ColCount="3">
            <Items>
                <dxtv:GridViewColumnLayoutItem ColumnName="NomeLimite" ColSpan="2">
                    <CaptionSettings Location="Top" />
                </dxtv:GridViewColumnLayoutItem>
                <dxtv:GridViewColumnLayoutItem ColumnName="TipoLimite">
                    <CaptionSettings Location="Top" />
                </dxtv:GridViewColumnLayoutItem>
                <dxtv:GridViewColumnLayoutItem ColumnName="Objetivo Estratégico">
                    <CaptionSettings Location="Top" />
                </dxtv:GridViewColumnLayoutItem>
<dxcp:GridViewColumnLayoutItem ColumnName="Conta de Receita" Caption="Conta de Receita">
<CaptionSettings Location="Top"></CaptionSettings>
</dxcp:GridViewColumnLayoutItem>
<dxcp:GridViewColumnLayoutItem ColumnName="Conta de Despesa">
<CaptionSettings Location="Top"></CaptionSettings>
</dxcp:GridViewColumnLayoutItem>
<dxcp:GridViewColumnLayoutItem ColumnName="Público Alvo" ColSpan="3">
<CaptionSettings Location="Top"></CaptionSettings>
</dxcp:GridViewColumnLayoutItem>
                <dxtv:GridViewColumnLayoutItem ColumnName="DescricaoLimite" ColSpan="3">
                    <CaptionSettings Location="Top" />
                </dxtv:GridViewColumnLayoutItem>
                <dxtv:GridViewColumnLayoutItem ColumnName="InicioValidade">
                    <CaptionSettings Location="Top" />
                </dxtv:GridViewColumnLayoutItem>
                <dxtv:GridViewColumnLayoutItem ColumnName="TerminoValidade">
                    <CaptionSettings Location="Top" />
                </dxtv:GridViewColumnLayoutItem>
                <dxtv:GridViewColumnLayoutItem ColumnName="CodigoUnidadeMedida">
                    <CaptionSettings Location="Top" />
                </dxtv:GridViewColumnLayoutItem>
                <dxtv:GridViewColumnLayoutItem ColSpan="3" ColumnName="FormulaLimite">
                    <CaptionSettings Location="Top" />
                </dxtv:GridViewColumnLayoutItem>
                <dxtv:EditModeCommandLayoutItem HorizontalAlign="Right" ColSpan="3">
                </dxtv:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
        <Columns>
            <dxtv:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="70px">
            </dxtv:GridViewCommandColumn>
            <dxtv:GridViewDataTextColumn FieldName="CodigoLimite" ReadOnly="True" Visible="False" VisibleIndex="1">
                <EditFormSettings Visible="True" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn Caption="Limite" FieldName="NomeLimite" VisibleIndex="2" Width="350px">
                <PropertiesTextEdit MaxLength="250">
                    <ValidationSettings Display="Dynamic">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataDateColumn Caption="Início de Validade" FieldName="InicioValidade" VisibleIndex="4" Width="100px">
                <EditFormSettings CaptionLocation="Top" />
            </dxtv:GridViewDataDateColumn>
            <dxtv:GridViewDataDateColumn Caption="Término de Validade" FieldName="TerminoValidade" VisibleIndex="5" Width="100px">
                <EditFormSettings CaptionLocation="Top" />
            </dxtv:GridViewDataDateColumn>
            <dxtv:GridViewDataComboBoxColumn Caption="Tipo de Limite" FieldName="TipoLimite" VisibleIndex="6" Width="100px">
                <PropertiesComboBox>
                    <Items>
                        <dxtv:ListEditItem Text="Planejamento" Value="PLA" />
                        <dxtv:ListEditItem Text="Orçamentário" Value="ORC" />
                    </Items>
                    <ValidationSettings Display="Dynamic">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <EditFormSettings CaptionLocation="Top" />
            </dxtv:GridViewDataComboBoxColumn>
            <dxtv:GridViewDataComboBoxColumn Caption="Objetivo Estratégico" VisibleIndex="7" Width="220px" FieldName="DescricaoObjetoEstrategia">
                <EditFormSettings CaptionLocation="Top" Caption="Objetivo Estratégico:" Visible="True" />
            </dxtv:GridViewDataComboBoxColumn>
             <dxtv:GridViewDataComboBoxColumn Caption="Conta de Receita" FieldName="DescricaoContaReceita" VisibleIndex="8" Width="220px">
                <EditFormSettings Caption="Conta de Receita:" Visible="True" />
            </dxtv:GridViewDataComboBoxColumn>
            <dxtv:GridViewDataComboBoxColumn VisibleIndex="9" Caption="Conta de Despesa" FieldName="DescricaoContaDespesa" Width="220px">
                <EditFormSettings Caption="Conta de Despesa:" Visible="True" />
            </dxtv:GridViewDataComboBoxColumn>           
            <dxtv:GridViewDataTextColumn Caption="Público Alvo" FieldName="PublicoAlvo" VisibleIndex="10" Width="200px">
                <PropertiesTextEdit MaxLength="200">
                </PropertiesTextEdit>
                <EditFormSettings Caption="Público Alvo:" ColumnSpan="3" Visible="True" />
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataMemoColumn Caption="Fórmula" FieldName="FormulaLimite" Visible="False" VisibleIndex="11" ReadOnly="true">
                <PropertiesMemoEdit MaxLength="8000" Rows="5" ClientInstanceName="txtFormula_editor" HelpText="Para editar a fórmula clique no campo acima">
                    <ClientSideEvents GotFocus="function(s, e) {
	abrirEditorFormulas();
txtFormula.SetText(s.GetText());
}" />
                    <ValidationSettings Display="Dynamic">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesMemoEdit>
                <EditFormSettings CaptionLocation="Top" Visible="True" />
            </dxtv:GridViewDataMemoColumn>
            <dxtv:GridViewDataComboBoxColumn Caption="Unidade de Medida" FieldName="CodigoUnidadeMedida" Visible="False" VisibleIndex="12">
                <PropertiesComboBox DataSourceID="dsUnidadeMedida" TextField="SiglaUnidadeMedida" ValueField="CodigoUnidadeMedida" ValueType="System.Byte">
                    <ValidationSettings Display="Dynamic">
                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <EditFormSettings CaptionLocation="Top" Visible="True" />
            </dxtv:GridViewDataComboBoxColumn>
            <dxtv:GridViewDataMemoColumn Caption="Descrição" FieldName="DescricaoLimite" Visible="False" VisibleIndex="3">
                <PropertiesMemoEdit MaxLength="2000" Rows="5">
                </PropertiesMemoEdit>
                <EditFormSettings CaptionLocation="Top" Visible="True" />
            </dxtv:GridViewDataMemoColumn>
            
            <dxtv:GridViewDataTextColumn FieldName="CodigoObjetivoEstrategico" Visible="False" VisibleIndex="13">
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn FieldName="CodigoContaReceita" Visible="False" VisibleIndex="14">
            </dxtv:GridViewDataTextColumn>
            <dxtv:GridViewDataTextColumn FieldName="CodigoContaDespesa" Visible="False" VisibleIndex="15">
            </dxtv:GridViewDataTextColumn>
        </Columns>
        <Styles>
            <Header Wrap="True">
            </Header>
        </Styles>
    </dxcp:ASPxGridView>
                <script language="javascript" type="text/javascript">
                    gvLimite.SetHeight(window.innerHeight - 160);
                </script>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="dataSource" runat="server" 
        DeleteCommand="DELETE FROM [Limite] WHERE [CodigoLimite] = @CodigoLimite" 
        InsertCommand="INSERT INTO [Limite] ([NomeLimite], [DescricaoLimite], [InicioValidade], [TerminoValidade], [TipoLimite]
                    , [CodigoObjetivoEstrategico], [CodigoContaReceita], [CodigoContaDespesa], [PublicoAlvo], [FormulaLimite], [CodigoUnidadeMedida]) 
        VALUES (@NomeLimite, @DescricaoLimite, @InicioValidade, @TerminoValidade, @TipoLimite, @DescricaoObjetoEstrategia, @DescricaoContaReceita, @DescricaoContaDespesa
        ,@PublicoAlvo, @FormulaLimite, @CodigoUnidadeMedida)" 
        SelectCommand="
SELECT l.CodigoLimite, l.NomeLimite, l.DescricaoLimite, l.InicioValidade, l.TerminoValidade, l.TipoLimite
     , l.TipoFoco, l.FormulaLimite, l.CodigoUnidadeMedida, oe.DescricaoObjetoEstrategia, pcRec.DescricaoConta AS DescricaoContaReceita,
     pcDes.DescricaoConta AS DescricaoContaDespesa, l.PublicoAlvo, l.CodigoObjetivoEstrategico, l.CodigoContaReceita, l.CodigoContaDespesa
  FROM Limite AS l LEFT JOIN
       ObjetoEstrategia AS oe ON (oe.CodigoObjetoEstrategia = l.CodigoObjetivoEstrategico) LEFT JOIN
       PlanoContasFluxoCaixa AS pcRec ON (pcRec.CodigoConta = l.CodigoContaReceita) LEFT JOIN
       PlanoContasFluxoCaixa AS pcDes ON (pcDes.CodigoConta = l.CodigoContaDespesa)
 ORDER BY NomeLimite" 
        UpdateCommand="UPDATE [Limite] SET [NomeLimite] = @NomeLimite, 
            [DescricaoLimite] = @DescricaoLimite, 
            [InicioValidade] = @InicioValidade, 
            [TerminoValidade] = @TerminoValidade, 
            [TipoLimite] = @TipoLimite, 
            [CodigoObjetivoEstrategico] = @DescricaoObjetoEstrategia, 
            [CodigoContaReceita] = @DescricaoContaReceita, 
            [CodigoContaDespesa] = @DescricaoContaDespesa, 
            [PublicoAlvo] = @PublicoAlvo, 
            [FormulaLimite] = @FormulaLimite, 
            [CodigoUnidadeMedida] = @CodigoUnidadeMedida 
        WHERE [CodigoLimite] = @CodigoLimite">
        <DeleteParameters>
            <asp:Parameter Name="CodigoLimite" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="NomeLimite" Type="String" />
            <asp:Parameter Name="DescricaoLimite" Type="String" />
            <asp:Parameter Name="InicioValidade" Type="DateTime" />
            <asp:Parameter Name="TerminoValidade" Type="DateTime" />
            <asp:Parameter Name="TipoLimite" Type="String" />
            <asp:Parameter Name="DescricaoObjetoEstrategia" Type="Int32" />
            <asp:Parameter Name="DescricaoContaReceita" Type="Int32" />
            <asp:Parameter Name="DescricaoContaDespesa" Type="Int32" />
            <asp:Parameter Name="PublicoAlvo" Type="String" />
            <asp:Parameter Name="FormulaLimite" Type="String" />
            <asp:Parameter Name="CodigoUnidadeMedida" Type="Byte" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="NomeLimite" Type="String" />
            <asp:Parameter Name="DescricaoLimite" Type="String" />
            <asp:Parameter Name="InicioValidade" Type="DateTime" />
            <asp:Parameter Name="TerminoValidade" Type="DateTime" />
            <asp:Parameter Name="TipoLimite" Type="String" />
           <asp:Parameter Name="DescricaoObjetoEstrategia" Type="Int32" />
            <asp:Parameter Name="DescricaoContaReceita" Type="Int32" />
            <asp:Parameter Name="DescricaoContaDespesa" Type="Int32" />
            <asp:Parameter Name="PublicoAlvo" Type="String" />
            <asp:Parameter Name="FormulaLimite" Type="String" />
            <asp:Parameter Name="CodigoUnidadeMedida" Type="Byte" />
            <asp:Parameter Name="CodigoLimite" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsUnidadeMedida" runat="server" SelectCommand="SELECT [CodigoUnidadeMedida], [SiglaUnidadeMedida] FROM [TipoUnidadeMedida] ORDER BY [SiglaUnidadeMedida]"></asp:SqlDataSource>
    <dxcp:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="800px"  Height="320px">
        <Windows>
            <dxtv:PopupWindow HeaderText="Editor de fórmula" Modal="True" Name="winEditor" Width="800px">
                <ContentCollection>
                    <dxtv:PopupControlContentControl runat="server">

                            <dxcp:ASPxSplitter ID="split" runat="server" ClientInstanceName="split" Height="300px">
                                <Panes>
                                    <dxtv:SplitterPane Size="60%">
                                        <ContentCollection>
                                            <dxcp:SplitterContentControl runat="server">
                                                <dxtv:ASPxGridView ID="gvCamposLimite" runat="server" AutoGenerateColumns="False" DataSourceID="dsCampo" KeyFieldName="SiglaCampo" Width="100%" ClientInstanceName="gvCamposLimite" OnCustomJSProperties="gvCamposLimite_CustomJSProperties" >
                                                    <ClientSideEvents RowDblClick="onRowDblClick" />
                                                    <SettingsPager Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <Settings VerticalScrollableHeight="250" VerticalScrollBarMode="Visible" />
                                                    <Columns>
                                                        <dxtv:GridViewDataTextColumn FieldName="CodigoCampo" ReadOnly="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                            <EditFormSettings Visible="False" />
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn Caption="Campo" FieldName="NomeCampo" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dxtv:GridViewDataTextColumn>
                                                        <dxtv:GridViewDataTextColumn Caption="Sigla" FieldName="SiglaCampo" ShowInCustomizationForm="True" VisibleIndex="2" Width="120px">
                                                        </dxtv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxtv:ASPxGridView>
                                            </dxcp:SplitterContentControl>
                                        </ContentCollection>
                                    </dxtv:SplitterPane>
                                    <dxtv:SplitterPane>
                                        <ContentCollection>
                                            <dxcp:SplitterContentControl runat="server">
                                                <dxtv:ASPxMemo ID="txtFormula" runat="server" ClientInstanceName="txtFormula" Height="270px" Width="100%" >
                                                    <ClientSideEvents LostFocus="onLostFocus" />
                                                </dxtv:ASPxMemo>
                                            </dxcp:SplitterContentControl>
                                        </ContentCollection>
                                    </dxtv:SplitterPane>
                                </Panes>
                            </dxcp:ASPxSplitter>
                            <asp:SqlDataSource ID="dsCampo" runat="server" SelectCommand="SELECT [CodigoCampo], [NomeCampo], [SiglaCampo] FROM [CampoLimite] ORDER BY [NomeCampo]"></asp:SqlDataSource>
                        <div>
                            <table style="margin-left:auto">
                                <tr>
                                    <td>
                                        <dxcp:ASPxButton ID="btnConfirmar" runat="server" Text="Confirmar" AutoPostBack="False"  Width="100px">
                                            <ClientSideEvents Click="onClick" />
                                        </dxcp:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </dxtv:PopupControlContentControl>
                </ContentCollection>
            </dxtv:PopupWindow>
        </Windows>
        <ContentCollection>
            <dxcp:PopupControlContentControl runat="server"></dxcp:PopupControlContentControl>
        </ContentCollection>
    </dxcp:ASPxPopupControl>
</asp:Content>

