<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CadastroRaias.aspx.cs" Inherits="_Projetos_Agil_CadastroRaias" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var comando;
        function switchRowsPosition(moveUpRowKey, moveDownRowKey) {
            grid.PerformCallback(moveUpRowKey + '|' + moveDownRowKey);
        }
    </script>
    <style>
    .container {
        position: relative;
    }
   
    .button-container {
        position: absolute;
        top: 0;
        right: 0;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxcp:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid" DataSourceID="dataSource" KeyFieldName="CodigoRaia" Width="100%" OnCommandButtonInitialize="grid_CommandButtonInitialize" OnCellEditorInitialize="grid_CellEditorInitialize" OnCustomButtonInitialize="grid_CustomButtonInitialize" OnCustomCallback="grid_CustomCallback" OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated" OnRowDeleted="grid_RowDeleted">
                <ClientSideEvents Init="function(s, e) {
                var height = Math.max(0, document.documentElement.clientHeight);
                height = height - 45;
                s.SetHeight(height);
}" CustomButtonClick="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
	switch(e.buttonID){
		case 'btnPraCima':
            var prevRowKey = s.GetRowKey(e.visibleIndex - 1);
            switchRowsPosition(key, prevRowKey);
			break;
		case 'btnPraBaixo':
            var nextRowKey = s.GetRowKey(e.visibleIndex + 1);
            switchRowsPosition(nextRowKey, key);
			break;
		default:
			break;
	}
}" BeginCallback="function(s, e) {
	comando = e.command;
}" EndCallback="function(s, e) {
       //alert(comando);
       if(comando ==  &quot;UPDATEEDIT&quot; || comando ==  &quot;DELETEROW&quot;)
       {
                if(s.cpErro != '')
               {
                            window.top.mostraMensagem(s.cpErro, 'erro', true, false, null);
               }
               else
               {
                          if(s.cpSucesso != '')
                          {
                                       window.top.mostraMensagem(s.cpSucesso, 'sucesso', false, false, null);
                           }
                }
       }
}" />
                <SettingsEditing Mode="PopupEditForm" NewItemRowPosition="Bottom">
                </SettingsEditing>
                <Settings VerticalScrollBarMode="Visible" />
                <SettingsBehavior ConfirmDelete="True" />
                <SettingsCommandButton RenderMode="Image">
                    <NewButton Text="Novo">
                        <Image Url="~/imagens/botoes/incluirReg02.png">
                        </Image>
                    </NewButton>
                    <EditButton Text="Editar">
                        <Image Url="~/imagens/botoes/editarReg02.PNG">
                        </Image>
                    </EditButton>
                    <DeleteButton Text="Excluir">
                        <Image Url="~/imagens/botoes/excluirReg02.PNG">
                        </Image>
                    </DeleteButton>
                </SettingsCommandButton>
                <SettingsPopup>
                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter">
                    </EditForm>

<HeaderFilter MinHeight="140px"></HeaderFilter>
                </SettingsPopup>
                <SettingsText ConfirmDelete="Deseja excluir a raia?" PopupEditFormCaption="Edição da Raia" />
                <Columns>
                    <dxtv:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="125px">
                        <CustomButtons>
                            <dxtv:GridViewCommandColumnCustomButton ID="btnPraCima">
                                <Image ToolTip="Mover uma posição acima" Url="~/imagens/botoes/pra-cima.png">
                                </Image>
                            </dxtv:GridViewCommandColumnCustomButton>
                            <dxtv:GridViewCommandColumnCustomButton ID="btnPraBaixo">
                                <Image ToolTip="Mover uma posição abaixo" Url="~/imagens/botoes/pra-baixo.png">
                                </Image>
                            </dxtv:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dxtv:GridViewCommandColumn>
                    <dxtv:GridViewDataTextColumn FieldName="CodigoRaia" Visible="False" VisibleIndex="1">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn Caption="Coluna" FieldName="NomeRaia" VisibleIndex="2">
                        <PropertiesTextEdit MaxLength="50">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataTextColumn FieldName="SequenciaApresentacaoRaia" Visible="False" VisibleIndex="4">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataSpinEditColumn Caption="% Concluído" FieldName="PercentualConcluido" VisibleIndex="3">
                        <PropertiesSpinEdit DisplayFormatString="g" MaxValue="100" NumberType="Integer">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesSpinEdit>
                        <Settings AllowAutoFilter="False" />
                        <EditFormSettings CaptionLocation="Top" />
                    </dxtv:GridViewDataSpinEditColumn>
                    <dxtv:GridViewDataColorEditColumn Caption="Cor do Cabeçalho" FieldName="CorCabecalho" VisibleIndex="5">
                        <PropertiesColorEdit AutomaticColor="Gray" AutomaticColorItemCaption="Cor Automática" CancelButtonText="Cancelar" CustomColorButtonText="Customizar Cor..." EnableCustomColors="True">
                        </PropertiesColorEdit>
                        <Settings AllowAutoFilter="False" />
                        <EditFormSettings CaptionLocation="Top" />
                    </dxtv:GridViewDataColorEditColumn>
                    <dxtv:GridViewDataTextColumn FieldName="PossuiItensAssociados" Visible="False" VisibleIndex="6">
                    </dxtv:GridViewDataTextColumn>
                    <dxtv:GridViewDataSpinEditColumn Caption="Apresentar destaque quando a quantidade de itens na raia for maior que:" FieldName="QuantidadeMaximaItensRaia" Visible="False" VisibleIndex="7">
                        <PropertiesSpinEdit DisplayFormatString="{0:n0}" NumberType="Integer" MaxValue="255" MaxLength="3">
                        </PropertiesSpinEdit>
                        <EditFormSettings Caption="Apresentar destaque quando a quantidade de itens na raia for maior que:" CaptionLocation="Top" Visible="True"/>
                    </dxtv:GridViewDataSpinEditColumn>
                    <dxtv:GridViewDataCheckColumn Caption="Indica Notificação de Recursos" FieldName="IndicaNotificacaoRecursos" Visible="False" VisibleIndex="8">
                        <EditFormSettings Caption="Notificar recursos quando um item for movido para esta raia" CaptionLocation="Top" Visible="True" />
                        <PropertiesCheckEdit ValueChecked="S" ValueUnchecked="N" ValueType="System.String"></PropertiesCheckEdit>
                    </dxtv:GridViewDataCheckColumn>
                </Columns>
                <FormatConditions>
                    <dxtv:GridViewFormatConditionHighlight ApplyToRow="True" Expression="SequenciaApresentacaoRaia == 0  Or SequenciaApresentacaoRaia == 255" FieldName="SequenciaApresentacaoRaia" Format="GreenFillWithDarkGreenText">
                    </dxtv:GridViewFormatConditionHighlight>
                </FormatConditions>
            </dxcp:ASPxGridView>
        </div>
        <div class="container">
            <div class="button-container">
                <dxcp:ASPxButton ID="btnFechar" runat="server" ClientInstanceName="btnFechar"
                    Text="Fechar" Width="110px">
                    <ClientSideEvents Click="function(s, e) {
	                                            window.top.fechaModal();
                                            }" />
                </dxcp:ASPxButton>
            </div>
        </div>
        <asp:SqlDataSource ID="dataSource" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="SELECT @CodigoIteracao = CodigoIteracao FROM [dbo].[Agil_Iteracao] WHERE CodigoProjetoIteracao = @CodigoProjetoIteracao

 SELECT ri.*, 
        CONVERT(bit, CASE WHEN EXISTS (SELECT 1 FROM Agil_ItemBacklog AS ib WHERE ib.CodigoRaia = ri.CodigoRaia) THEN 1 ELSE 0 END) AS PossuiItensAssociados
   FROM [dbo].[f_Agil_GetRaiasIteracao](@CodigoIteracao) AS ri 
  ORDER BY 
        SequenciaApresentacaoRaia, 
        PercentualConcluido"
            DeleteCommand="SELECT @CodigoIteracao = CodigoIteracao FROM [dbo].[Agil_Iteracao] WHERE CodigoProjetoIteracao = @CodigoProjetoIteracao

 DELETE FROM [Agil_RaiasIteracao] WHERE [CodigoRaia] = @original_CodigoRaia

 UPDATE [Agil_RaiasIteracao]
    SET [SequenciaApresentacaoRaia] = [SequenciaApresentacaoRaia] - 1
  WHERE CodigoIteracao = @CodigoIteracao
    AND SequenciaApresentacaoRaia  &lt; 255
    AND SequenciaApresentacaoRaia &gt; @original_SequenciaApresentacaoRaia" InsertCommand=" SELECT @CodigoIteracao = CodigoIteracao FROM [dbo].[Agil_Iteracao] WHERE CodigoProjetoIteracao = @CodigoProjetoIteracao

 SELECT @SequenciaApresentacaoRaia = COUNT(1) FROM Agil_RaiasIteracao AS ri WHERE ri.CodigoIteracao = @CodigoIteracao AND ri.PercentualConcluido &lt;= @PercentualConcluido AND ri.SequenciaApresentacaoRaia &lt; 255

 INSERT INTO [Agil_RaiasIteracao]
            ([CodigoIteracao]
            ,[NomeRaia]
            ,[PercentualConcluido]
            ,[SequenciaApresentacaoRaia]
            ,[CorCabecalho]
            ,[QuantidadeMaximaItensRaia]
            ,[IndicaNotificacaoRecursos])
      VALUES
            (@CodigoIteracao
            ,@NomeRaia
            ,@PercentualConcluido
            ,@SequenciaApresentacaoRaia
            ,@CorCabecalho
            ,@QuantidadeMaximaItensRaia
            ,@IndicaNotificacaoRecursos)

    SET @CodigoRaia = SCOPE_IDENTITY()

 UPDATE [Agil_RaiasIteracao]
    SET [SequenciaApresentacaoRaia] = [SequenciaApresentacaoRaia] + 1
  WHERE [CodigoIteracao] = @CodigoIteracao
    AND [CodigoRaia] &lt;&gt; @CodigoRaia
    AND [SequenciaApresentacaoRaia] &gt;= @SequenciaApresentacaoRaia
    AND [SequenciaApresentacaoRaia] &lt; 255" UpdateCommand="SELECT @CodigoIteracao = CodigoIteracao FROM [dbo].[Agil_Iteracao] WHERE CodigoProjetoIteracao = @CodigoProjetoIteracao

IF(@original_PercentualConcluido &lt;&gt; @PercentualConcluido)
BEGIN
  IF(@original_PercentualConcluido &lt; @PercentualConcluido)
  BEGIN
     UPDATE Agil_RaiasIteracao
        SET SequenciaApresentacaoRaia = SequenciaApresentacaoRaia - 1
      WHERE CodigoIteracao = @CodigoIteracao
        AND PercentualConcluido &lt;= @PercentualConcluido
        AND SequenciaApresentacaoRaia &gt; @SequenciaApresentacaoRaia
        AND SequenciaApresentacaoRaia &lt; 255
  END
  ELSE IF(@original_PercentualConcluido &gt; @PercentualConcluido)
  BEGIN
     UPDATE Agil_RaiasIteracao
        SET SequenciaApresentacaoRaia = SequenciaApresentacaoRaia + 1
      WHERE CodigoIteracao = @CodigoIteracao
        AND PercentualConcluido &gt; @PercentualConcluido
        AND SequenciaApresentacaoRaia &lt; @SequenciaApresentacaoRaia
  END
  UPDATE Agil_RaiasIteracao
     SET SequenciaApresentacaoRaia = (SELECT COUNT(1) FROM Agil_RaiasIteracao AS ri WHERE ri.CodigoRaia &lt;&gt; @CodigoRaia AND ri.CodigoIteracao = @CodigoIteracao AND ri.PercentualConcluido &lt;= @PercentualConcluido AND ri.SequenciaApresentacaoRaia &lt; 255)
   WHERE CodigoRaia = @CodigoRaia
END

 UPDATE [Agil_RaiasIteracao] 
    SET [NomeRaia] = @NomeRaia, 
        [PercentualConcluido] = @PercentualConcluido, 
        [CorCabecalho] = @CorCabecalho,
        [QuantidadeMaximaItensRaia] = @QuantidadeMaximaItensRaia,
        [IndicaNotificacaoRecursos] = @IndicaNotificacaoRecursos
  WHERE [CodigoRaia] = @CodigoRaia


" ConflictDetection="CompareAllValues" OldValuesParameterFormatString="original_{0}" OnInserting="dataSource_Inserting" OnUpdating="dataSource_Updating">
            <DeleteParameters>
                <asp:Parameter Name="CodigoRaia" Type="Int32" />
                <asp:Parameter Name="CodigoIteracao" Type="Int32" />
                <asp:Parameter Name="SequenciaApresentacaoRaia" Type="Byte" />
                <asp:QueryStringParameter Name="CodigoProjetoIteracao" QueryStringField="cp" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="CodigoIteracao" Type="Int32" />
                <asp:Parameter Name="NomeRaia" Type="String" />
                <asp:Parameter Name="PercentualConcluido" Type="Byte" />
                <asp:Parameter Name="SequenciaApresentacaoRaia" Type="Byte" />
                <asp:Parameter Name="CorCabecalho" Type="String" />
                <asp:QueryStringParameter Name="CodigoProjetoIteracao" QueryStringField="cp" />
                <asp:Parameter Name="CodigoRaia" />
                <asp:Parameter Name="QuantidadeMaximaItensRaia" Type="Int32"/>
                <asp:Parameter Name="IndicaNotificacaoRecursos" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="CodigoIteracao" Type="Int32" />
                <asp:QueryStringParameter Name="CodigoProjetoIteracao" QueryStringField="cp" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="CodigoIteracao" />
                <asp:QueryStringParameter Name="CodigoProjetoIteracao" QueryStringField="cp" />
                <asp:Parameter Name="original_PercentualConcluido" />
                <asp:Parameter Name="PercentualConcluido" />
                <asp:Parameter Name="SequenciaApresentacaoRaia" />
                <asp:Parameter Name="CodigoRaia" />
                <asp:Parameter Name="NomeRaia" />
                <asp:Parameter Name="CorCabecalho" />
                <asp:Parameter Name="QuantidadeMaximaItensRaia" Type="Int32"/>
                <asp:Parameter Name="IndicaNotificacaoRecursos" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
