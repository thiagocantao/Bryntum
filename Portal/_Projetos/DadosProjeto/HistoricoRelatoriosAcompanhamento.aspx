<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoricoRelatoriosAcompanhamento.aspx.cs"
    Inherits="_Projetos_DadosProjeto_HistoricoRelatoriosAcompanhamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script language="javascript" type="text/javascript">
        var linhaSelecionada = -1;
        function excluiRelatorio() {
            gvDados.DeleteRow(linhaSelecionada);
        }
        function ShowEditForm(codigoRelatorio, novo, readOnly) {
            var width = screen.width - 60;
            var height = screen.height - 260;
            var strNovo = novo ? "S" : "N";
            var strReadOnly = readOnly ? "S" : "N";
            var url = "DadosRelatorioAcompanhamento.aspx?CR=" + codigoRelatorio + "&altura=" + height + "&novo=" + strNovo + "&RO=" + strReadOnly;
            window.top.showModal(url, '', width, height, OnPopuClosed, null);
        }

        function OnCustomButtonClick(s, e) {
            switch (e.buttonID) {
                case 'btnPublicar':
                    if (confirm("Deseja publicar o relatório?")) {
                        e.processOnServer = true;
                    }
                    break;
                case 'btnEditar':
                    var codigoRelatorio = s.GetRowKey(s.GetFocusedRowIndex());
                    ShowEditForm(codigoRelatorio, false, false);
                    e.processOnServer = false;
                    break;
                case 'btnExcluir':
                    linhaSelecionada = e.visibleIndex
                    e.processOnServer = false;
                    window.top.mostraMensagem("Deseja excluir o relatório?", 'confirmacao', true, true, excluiRelatorio);

                    break;
                case 'btnDownload':
                    e.processOnServer = false;
                    callback.PerformCallback("Download;" + s.GetRowKey(s.GetFocusedRowIndex()));
                    break;
                case 'btnVisualizar':
                    var codigoRelatorio = s.GetRowKey(s.GetFocusedRowIndex());
                    ShowEditForm(codigoRelatorio, false, true);
                    e.processOnServer = false;
                    break;
            }
        }

        function AdicionarNovo() {
            callback.PerformCallback("Novo");
        }

        function OnPopuClosed(result) {
            if (result != null)
            {
                var index = result.indexOf('pendente');
                if (index > -1)
                {
                   var codigoRelatorio = result.substring(result.indexOf(';') + 1);
                   callback.PerformCallback("Limpar;" + codigoRelatorio);
                }
                else
                {
                    gvDados.Refresh();
                }
            }
            
        }

    </script>
    <title></title>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <dxwgv:ASPxGridView ID="gvDados" runat="server" AutoGenerateColumns="False" ClientInstanceName="gvDados"
                DataSourceID="sdsRelatorioAcompanhamento"
                KeyFieldName="CodigoRelatorio" Width="100%" OnCustomButtonInitialize="gvDados_CustomButtonInitialize"
                OnCustomButtonCallback="gvDados_CustomButtonCallback">
                <ClientSideEvents CustomButtonClick="function(s, e) {
	OnCustomButtonClick(s, e);
}" />
                <Columns>
                    <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" VisibleIndex="0" Width="150px">
                        <CustomButtons>
                            <dxwgv:GridViewCommandColumnCustomButton ID="btnPublicar" Text="Publicar">
                                <Image AlternateText="Publicar" Url="~/imagens/botoes/PublicarReg02.png">
                                </Image>
                            </dxwgv:GridViewCommandColumnCustomButton>
                            <dxwgv:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                <Image AlternateText="Editar" Url="~/imagens/botoes/editarReg02.PNG">
                                </Image>
                            </dxwgv:GridViewCommandColumnCustomButton>
                            <dxwgv:GridViewCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                <Image AlternateText="Excluir" Url="~/imagens/botoes/excluirReg02.PNG">
                                </Image>
                            </dxwgv:GridViewCommandColumnCustomButton>
                            <dxwgv:GridViewCommandColumnCustomButton ID="btnVisualizar"
                                Text="Mostrar Detalhes">
                                <Image AlternateText="Mostrar Detalhes" Url="~/imagens/botoes/pFormulario.png">
                                </Image>
                            </dxwgv:GridViewCommandColumnCustomButton>
                            <dxwgv:GridViewCommandColumnCustomButton ID="btnDownload" Text="Visualizar o arquivo">
                                <Image AlternateText="Visualizar o arquivo" Url="~/imagens/botoes/btnPDF.png">
                                </Image>
                            </dxwgv:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                        <HeaderTemplate>
                            <%# ObtemHtmlBtnAdicionar() %>
                        </HeaderTemplate>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Data Elaboração" FieldName="DataElaboracao"
                        VisibleIndex="1" Width="120px">
                        <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Data Publicação" FieldName="DataPublicacao"
                        VisibleIndex="2" Width="120px">
                        <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Elaborado Por" FieldName="NomeElaborador"
                        VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Publicado Por" FieldName="NomePublicador"
                        VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True" />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Settings VerticalScrollBarMode="Visible" />
                <SettingsText />
            </dxwgv:ASPxGridView>
        </div>
        <dxcb:ASPxCallback ID="callback" runat="server" OnCallback="callback_Callback" ClientInstanceName="callback">
            <ClientSideEvents CallbackComplete="function(s, e) {
	if(e.parameter == &quot;Novo&quot; &amp;&amp; e.result != &quot;-1&quot;)
	{
		var codigoRelatorio = e.result;
        ShowEditForm(codigoRelatorio, true, false);
	}
	else if(e.parameter.indexOf(&quot;Download&quot;) &gt; -1)
	{
		window.location = &quot;./ReportOutput.aspx?exportType=pdf&amp;bInline=False&quot;;
	}
}" />
        </dxcb:ASPxCallback>
        <asp:SqlDataSource ID="sdsRelatorioAcompanhamento" runat="server" SelectCommand=" SELECT CodigoRelatorio,
        DataElaboracao,
        DataPublicacao,
        e.NomeUsuario AS NomeElaborador,
        p.NomeUsuario AS NomePublicador
   FROM pbh_RelatorioAcompanhamento r LEFT JOIN
        Usuario e ON e.CodigoUsuario = r.CodigoUsuarioInclusao LEFT JOIN
        Usuario p ON p.CodigoUsuario = r.CodigoUsuarioPublicacao
  WHERE r.CodigoProjeto = @CodigoProjeto
    AND r.DataExclusao IS NULL"
            DeleteCommand="UPDATE [pbh_RelatorioAcompanhamento] 
SET [DataExclusao] = GETDATE(), [CodigoUsuarioExclusao] = @CodigoUsuario
WHERE [CodigoRelatorio] = @CodigoRelatorio"
            InsertCommand="[p_pbh_CriaRelatorioAcompanhamento]" UpdateCommand="UPDATE [pbh_RelatorioAcompanhamento] 
SET [DataPublicacao] = GETDATE(), [CodigoUsuarioPublicacao] = @CodigoUsuario 
WHERE [CodigoRelatorio] = @CodigoRelatorio"
            InsertCommandType="StoredProcedure">
            <DeleteParameters>
                <asp:Parameter Name="CodigoRelatorio" Type="Int32" />
                <asp:SessionParameter Name="CodigoUsuario" SessionField="CodigoUsuario" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="RETURN_VALUE" Type="Int32" Direction="ReturnValue" />
                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="CP" Type="Int32" />
                <asp:SessionParameter Name="CodigoUsuario" SessionField="CodigoUsuario" Type="Int32" />
            </InsertParameters>
            <SelectParameters>
                <asp:QueryStringParameter Name="CodigoProjeto" QueryStringField="CP" />
            </SelectParameters>
            <UpdateParameters>
                <asp:SessionParameter Name="CodigoUsuario" SessionField="CodigoUsuario" />
                <asp:Parameter Name="CodigoRelatorio" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
