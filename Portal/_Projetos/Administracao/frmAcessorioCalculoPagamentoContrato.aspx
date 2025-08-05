<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAcessorioCalculoPagamentoContrato.aspx.cs"
    Inherits="_Projetos_Administracao_frmAcessorioCalculoPagamentoContrato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script language="javascript" type="text/javascript">
        function InitEditForm(s, e) {
            var operacao = "InitEditForm";
            var codigoAcessorio = s.GetValue();
            if (codigoAcessorio != null)
                callback.PerformCallback(operacao + ";" + codigoAcessorio);
        }

        function ObtemValorAcessorio(s, e) {
            var operacao = "ObtemValorAcessorio";
            var codigoAcessorio = s.GetValue();
            if (codigoAcessorio != null)
                callback.PerformCallback(operacao + ";" + codigoAcessorio);
        }

        function DefineDadosAcessorio(s, e) {
            if (e.parameter.indexOf("ObtemValorAcessorio") > -1) {
                var valor = e.result.substring(2);
                var tipo = e.result.substring(0, 1);
                if (tipo == "A") {
                    seAliquota.SetEnabled(true);
                    seAliquota.SetValue(valor);
                    seAliquota.Focus();
                    seValor.SetEnabled(false);
                    seValor.SetValue(null);
                }
                else {
                    seValor.SetEnabled(true);
                    seValor.SetValue(valor);
                    seValor.Focus();
                    seAliquota.SetEnabled(false);
                    seAliquota.SetValue(null);
                }
            }
            else if (e.parameter.indexOf("InitEditForm") > -1) {
                var tipo = e.result.substring(0, 1);
                if (tipo == "A") {
                    seAliquota.SetEnabled(true);
                    seAliquota.Focus();
                    seValor.SetEnabled(false);
                }
                else {
                    seValor.SetEnabled(true);
                    seValor.Focus();
                    seAliquota.SetEnabled(false);
                }
            }
        }

        function EncerraVigenciaAcessorio(s, e) {
            var rowIndex = s.GetFocusedRowIndex();
            var codigoAcessorioContrato = s.GetRowKey(rowIndex);
            var operacao = "EncerraVigenciaAcessorio";
            callback.PerformCallback(operacao + ";" + codigoAcessorioContrato);
            s.Refresh();
        }
    </script>
    <title></title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                </td>
                <td style="height: 10px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <dxwgv:ASPxGridView ID="gvDados" runat="server" ClientInstanceName="gvDados"
                        Width="950px" AutoGenerateColumns="False" DataSourceID="sdsAcessoriosContrato"
                        KeyFieldName="CodigoAcessorioContrato" OnCommandButtonInitialize="gvDados_CommandButtonInitialize"
                        OnCustomButtonInitialize="gvDados_CustomButtonInitialize" OnRowInserting="gvDados_RowInserting"
                        OnRowUpdating="gvDados_RowUpdating" OnRowValidating="gvDados_RowValidating" OnAutoFilterCellEditorInitialize="gvDados_AutoFilterCellEditorInitialize">
                        <ClientSideEvents CustomButtonClick="function(s, e) {
	if(e.buttonID == &quot;btnTerminoVigencia&quot;)
	{
		if(confirm(&quot;Deseja encerrar a vigência do acessório?&quot;))
		{
			EncerraVigenciaAcessorio(s, e);
		}
	}
}" />
                        <Columns>
                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="100px" ShowEditButton="true"
                                ShowDeleteButton="true">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton ID="btnTerminoVigencia" Text="Definir término vigência"
                                        Visibility="Invisible">
                                        <Image AlternateText="Definir término vigência" Url="~/imagens/botoes/contratoEncerrado.png">
                                        </Image>
                                    </dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                                <HeaderTemplate>
                                    <%# ObtemBtnIncluir() %>
                                </HeaderTemplate>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataComboBoxColumn Caption="Acessório" FieldName="CodigoAcessorio"
                                VisibleIndex="3" Visible="False">
                                <PropertiesComboBox DataSourceID="sdsOpcoesAcessorios" TextField="DescricaoAcessorio"
                                    ValueField="CodigoAcessorio" ClientInstanceName="cmbAcessorio">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ObtemValorAcessorio(s, e);
}" Init="function(s, e) {
	InitEditForm(s, e);
}" />
                                    <ItemStyle  />
                                    <ValidationSettings Display="Dynamic">
                                        <RequiredField ErrorText="Campo obrigatório" IsRequired="True" />
                                    </ValidationSettings>
                                    <Style >
                                        
                                    </Style>
                                </PropertiesComboBox>
                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" Visible="True" />
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CodigoContrato" Visible="False" VisibleIndex="1">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataSpinEditColumn Caption="Alíquota" FieldName="Aliquota" VisibleIndex="5"
                                Width="100px">
                                <PropertiesSpinEdit ClientInstanceName="seAliquota" DisplayFormatString="{0:n3}"
                                    NumberFormat="Custom">
                                    <Style >
                                        
                                    </Style>
                                </PropertiesSpinEdit>
                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataSpinEditColumn Caption="Valor" FieldName="Valor" VisibleIndex="6"
                                Width="100px">
                                <PropertiesSpinEdit ClientInstanceName="seValor" DisplayFormatString="{0:n2}" NumberFormat="Custom">
                                    <Style >
                                        
                                    </Style>
                                </PropertiesSpinEdit>
                                <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                            </dxwgv:GridViewDataSpinEditColumn>
                            <dxwgv:GridViewDataDateColumn Caption="Início Vigência" FieldName="DataInicioVigencia"
                                VisibleIndex="7" Width="110px">
                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
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
                                        <HeaderStyle />
                                        <FooterStyle  />
                                        <InvalidStyle >
                                        </InvalidStyle>
                                        <Style >
                                            
                                        </Style>
                                    </CalendarProperties>
                                    <ValidationSettings Display="Dynamic">
                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                    </ValidationSettings>
                                    <Style >
                                        
                                    </Style>
                                </PropertiesDateEdit>
                                <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="1" />
                            </dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewDataDateColumn Caption="Término Vigência" FieldName="DataTerminoVigencia"
                                VisibleIndex="8" Width="110px">
                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}">
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
                                        <Style >
                                            
                                        </Style>
                                    </CalendarProperties>
                                    <Style >
                                        
                                    </Style>
                                </PropertiesDateEdit>
                                <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="1" />
                            </dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="2" FieldName="CodigoAcessorioContrato">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Acessório" FieldName="DescricaoAcessorio"
                                VisibleIndex="4">
                                <EditFormSettings Visible="False" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="IndicaAcessorioBloqueado" Visible="False"
                                VisibleIndex="10">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                        </SettingsPager>
                        <SettingsEditing Mode="PopupEditForm" />
                        <SettingsPopup>
                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                AllowResize="True" Width="400px" />
                        </SettingsPopup>
                        <Settings VerticalScrollBarMode="Visible" />
                        <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                    </dxwgv:ASPxGridView>
                    <asp:SqlDataSource ID="sdsAcessoriosContrato" runat="server" DeleteCommand="DELETE FROM [AcessorioCalculoPagamentoContrato] WHERE [CodigoAcessorioContrato] = @CodigoAcessorioContrato"
                        InsertCommand="INSERT INTO AcessorioCalculoPagamentoContrato (CodigoAcessorio, CodigoContrato, Aliquota, Valor, DataInicioVigencia, DataTerminoVigencia, Tipo, DescricaoAcessorio) VALUES (@CodigoAcessorio,@CodigoContrato,@Aliquota,@Valor, @DataInicioVigencia, @DataTerminoVigencia, (SELECT Tipo FROM AcessorioCalculoPagamento WHERE CodigoAcessorio = @CodigoAcessorio), (SELECT DescricaoAcessorio FROM AcessorioCalculoPagamento WHERE CodigoAcessorio = @CodigoAcessorio))"
                        SelectCommand=" SELECT 
               CodigoAcessorioContrato, 
               CodigoAcessorio, 
               DescricaoAcessorio,
               CodigoContrato, 
               Aliquota, 
               Valor, 
               DataInicioVigencia, 
               DataTerminoVigencia,
               IndicaAcessorioBloqueado
   FROM AcessorioCalculoPagamentoContrato AS acpc
 WHERE (CodigoContrato = @CodigoContrato)
  ORDER BY
        DescricaoAcessorio" UpdateCommand="UPDATE AcessorioCalculoPagamentoContrato SET CodigoAcessorio = @CodigoAcessorio, Aliquota = @Aliquota, Valor = @Valor, Tipo = (SELECT Tipo FROM AcessorioCalculoPagamento AS acp_1 WHERE (CodigoAcessorio = @CodigoAcessorio)), DescricaoAcessorio = (SELECT DescricaoAcessorio FROM AcessorioCalculoPagamento AS acp_2 WHERE (CodigoAcessorio = @CodigoAcessorio)), DataInicioVigencia = @DataInicioVigencia, DataTerminoVigencia = @DataTerminoVigencia WHERE (CodigoAcessorioContrato = @CodigoAcessorioContrato)">
                        <DeleteParameters>
                            <asp:Parameter Name="CodigoAcessorioContrato" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" Type="Int32" />
                            <asp:Parameter Name="CodigoAcessorio" />
                            <asp:Parameter Name="Aliquota" />
                            <asp:Parameter Name="Valor" />
                            <asp:Parameter Name="DataInicioVigencia" />
                            <asp:Parameter Name="DataTerminoVigencia" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="CodigoAcessorio" />
                            <asp:Parameter Name="CodigoAcessorioContrato" />
                            <asp:Parameter Name="Aliquota" />
                            <asp:Parameter Name="Valor" />
                            <asp:Parameter Name="DataInicioVigencia" />
                            <asp:Parameter Name="DataTerminoVigencia" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="sdsOpcoesAcessorios" runat="server" SelectCommand=" SELECT CodigoAcessorio, 
        DescricaoAcessorio 
   FROM AcessorioCalculoPagamento 
  WHERE (CodigoAcessorio NOT IN (SELECT CodigoAcessorio 
                                   FROM AcessorioCalculoPagamentoContrato 
                                  WHERE (DataTerminoVigencia IS NULL)
                                       AND (CodigoContrato = @CodigoContrato)))" OnSelecting="sdsOpcoesAcessorios_Selecting">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
                        <ClientSideEvents CallbackComplete="function(s, e) {
	DefineDadosAcessorio(s, e);
}" />
                    </dxcb:ASPxCallback>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
