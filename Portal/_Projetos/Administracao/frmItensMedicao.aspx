<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmItensMedicao.aspx.cs"
    Inherits="_Projetos_Administracao_frmItensMedicao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script language="javascript" type="text/javascript">
        function ImportaItensMedicaoCronograma() {
            var operacao = "ImportaItensMedicaoCronograma";
            callback.PerformCallback(operacao + ";");
        }

        function SalvaItensMedicaoCronograma() {
            var operacao = "SalvaItensMedicaoCronograma";
            callback.PerformCallback(operacao + ";");
        }

        function CancelaImportacaoItensMedicaoCronograma() {
            var operacao = "CancelaImportacaoItensMedicaoCronograma";
            callback.PerformCallback(operacao + ";");
        }

        function OnBeginCallback(s, e) {
            switch (e.command.toUpperCase()) {
                case "UPDATEEDIT":
                case "DELETEROW":
                case "DELETENODE":
                    hfGeral.Set("AtualizarValorTotalItens", true);
                    break;
                default:
                    hfGeral.Set("AtualizarValorTotalItens", false);
                    break;
            }
        }

        function OnEndCallback(s, e) {
            var atualizar = hfGeral.Get("AtualizarValorTotalItens");
            if (atualizar) {
                callbackPanel.PerformCallback();
                hfGeral.Set("AtualizarValorTotalItens", false);
            }
        }

        function InitFormEdicaoItemPai(codigoItemPai) {
            var operacao = "InitFormEdicaoItemPai";
            callback.PerformCallback(operacao + ";" + codigoItemPai);
            hfGeral.Set("CodigoItemPai", codigoItemPai);
        }

        function NovoItem(codigoItemPai) {
            var operacao = "NovoItem";
            callback.PerformCallback(operacao + ";" + codigoItemPai);
        }

        function NovoItemPai() {
            limpaPopupEdicaoItemPai();
            InitFormEdicaoItemPai(-1);
        }

        function limpaPopupEdicaoItemPai() {
            txtDescricaoItemPai.SetValue(null);
            seOrdem.SetValue(null);
        }

        function fechaPopupEdicaoItemPai() {
            pcEditItemPai.Hide();
        }

        function salvaPopupEdicaoItemPai() {
            if (ValidaFormulario()) {
                var codigoItemPai = hfGeral.Get("CodigoItemPai");
                var operacao = codigoItemPai == -1 ? "NovoItemPai" : "EditaItemPai";
                callback.PerformCallback(operacao + ";" + codigoItemPai);
                fechaPopupEdicaoItemPai();
            }
        }

        function ValidaFormulario() {
            var valida = true;
            var msg = "";
            var descricaoItemPai = txtDescricaoItemPai.GetValue();
            var ordemItemPai = seOrdem.GetValue();

            if ((descricaoItemPai == null) || (Trim(descricaoItemPai) == "")) {
                msg += "\n - O campo 'Descrição' deve ser informado.";
                valida = false;
            }
            if (ordemItemPai == null) {
                msg += "\n - O campo 'Ordem' deve ser informado.";
                valida = false;
            }
            if (!valida)
                window.top.mostraMensagem(msg, 'atencao', true, false, null);
            return valida;
        }

        function Trim(str) {
            //Retira os espaços vazios do vazio do início e do fim da string
            return str.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
        }

        function callbackComplete(s, e) {
            if (e.parameter.indexOf("NovoItemPai") > -1) {
                var codigoNovoItem = e.result;
                gvDados.StartEditRowByKey(codigoNovoItem);
                hfGeral.Set("CodigoNovoItem", codigoNovoItem);
            }
            else if (e.parameter.indexOf("NovoItem") > -1) {
                var codigoNovoItem = e.result;
                gvDados.StartEditRowByKey(codigoNovoItem);
            }
            else if (e.parameter.indexOf("InitFormEdicaoItemPai") > -1) {
                var resultado = e.result.split(";");
                var descricaoItem = resultado[0];
                var numeroOrdem = parseInt(resultado[1]);
                var numeroOrdemMaximo = parseInt(resultado[2]);
                txtDescricaoItemPai.SetValue(descricaoItem);
                seOrdem.SetMinValue(1);
                seOrdem.SetMaxValue(numeroOrdemMaximo);
                seOrdem.SetValue(numeroOrdem);
                pcEditItemPai.Show();
            }
            else if (e.parameter.indexOf("EditaItemPai") > -1) {
                gvDados.Refresh();
            }
            else if (e.parameter.indexOf("ImportaItensMedicaoCronograma") > -1) {
                var qtdeRegistrosAfetados = parseInt(e.result);
                if (qtdeRegistrosAfetados > 0) {
                    gvDadosImportacaoCronograma.Refresh();
                    DefineSalvamentoPendente(true);
                }
                else
                    window.top.mostraMensagem('Não existem dados a serem importados.', 'atencao', true, false, null);
            }
            else if (e.parameter.indexOf("SalvaItensMedicaoCronograma") > -1) {
                var qtdeRegistrosAfetados = parseInt(e.result);
                DefineSalvamentoPendente(false);
                if (qtdeRegistrosAfetados > 0)
                    gvDadosImportacaoCronograma.Refresh();
            }
            else if (e.parameter.indexOf("CancelaImportacaoItensMedicaoCronograma") > -1) {
                gvDadosImportacaoCronograma.Refresh();
                DefineSalvamentoPendente(false);
            }
        }

        function DefineSalvamentoPendente(pendente) {
            if (pendente) {
                hfGeral.Set("cancelaFechamentoPopUp", "S");
                window.top.cancelaFechamentoPopUp = "S";
            }
            else {
                hfGeral.Set("cancelaFechamentoPopUp", "N");
                window.top.cancelaFechamentoPopUp = "N";
            }
        }

        function ObtemArrayValores(str) {
            var dados = new Array();
            var linhas = str.split(";");
            for (i = 0; i < linhas.length; i++) {
                var colunas = linhas[i].split("=");
                dados[colunas[0]] = colunas[1];
            }
            return dados;
        }
    </script>
    <title></title>
</head>
<body onload="DefineSalvamentoPendente(hfGeral.Get('cancelaFechamentoPopUp')=='S')">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%" frame="border">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr id="tdInformacoesPrograma" runat="server">
                            <td>
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 55px">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Programa:"
                                               >
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-left: 10px">
                                            <dxe:ASPxLabel ID="lblNomePrograma" runat="server" Text="Nome do programa"
                                               >
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Projeto:"
                                               >
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-left: 10px">
                                            <dxe:ASPxComboBox ID="cmbProjeto" runat="server" TextField="NomeProjeto" ValueField="CodigoProjeto"
                                                ValueType="System.Int32" Width="100%" DataSourceID="sdsProjetosDoPrograma" IncrementalFilteringMode="Contains"
                                                OnCallback="cmbProjeto_Callback" AutoPostBack="True" >
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="tdInformacoesProjeto" runat="server">
                            <td>
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 55px">
                                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Projeto:"
                                               >
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-left: 10px">
                                            <dxe:ASPxLabel ID="lblNomeProjeto" runat="server" Text="Nome do projeto"
                                                Font-Strikeout="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="tdInformacoesEntidade" runat="server">
                            <td>
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 55px">
                                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Projeto:"
                                               >
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="padding-left: 10px">
                                            <dxe:ASPxComboBox ID="cmbProjetosEntidade" runat="server" TextField="NomeProjeto"
                                                ValueField="CodigoProjeto" ValueType="System.Int32" Width="100%" DataSourceID="sdsProjetosEntidade"
                                                IncrementalFilteringMode="Contains" OnCallback="cmbProjeto_Callback" AutoPostBack="True"
                                                >
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <dxtc:ASPxPageControl ID="tabControl" runat="server" ActiveTabIndex="0" Width="100%"
                         ClientInstanceName="tabControl">
                        <TabPages>
                            <dxtc:TabPage Text="Preço Unitário" Name="tabPrecoUnitario">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <dxwtl:ASPxTreeList ID="treeListItensMedicao" runat="server" Width="100%" ClientInstanceName="treeListItensMedicao"
                                            DataSourceID="sdsItemMedicaoContrato"  KeyFieldName="CodigoItemMedicaoContrato"
                                            OnCellEditorInitialize="treeListItensMedicao_CellEditorInitialize" OnCommandColumnButtonInitialize="treeListItensMedicao_CommandColumnButtonInitialize"
                                            ParentFieldName="CodigoItemPai">
                                            <Columns>
                                                <dxwtl:TreeListCommandColumn ButtonType="Image" VisibleIndex="0" Width="100px">
                                                    <UpdateButton>
                                                        <Image ToolTip="Salvar" Url="~/imagens/botoes/salvar.PNG">
                                                        </Image>
                                                    </UpdateButton>
                                                    <CancelButton>
                                                        <Image ToolTip="Cancelar" Url="~/imagens/botoes/cancelar.PNG">
                                                        </Image>
                                                    </CancelButton>
                                                    <CustomButtons>
                                                        <dxwtl:TreeListCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                            <Image Url="~/imagens/botoes/editarReg02.PNG">
                                                            </Image>
                                                        </dxwtl:TreeListCommandColumnCustomButton>
                                                        <dxwtl:TreeListCommandColumnCustomButton ID="btnIncluir" Text="Incluir">
                                                            <Image Url="~/imagens/botoes/incluirReg02.PNG">
                                                            </Image>
                                                        </dxwtl:TreeListCommandColumnCustomButton>
                                                        <dxwtl:TreeListCommandColumnCustomButton ID="btnExcluir" Text="Excluir">
                                                            <Image Url="~/imagens/botoes/excluirReg02.PNG">
                                                            </Image>
                                                        </dxwtl:TreeListCommandColumnCustomButton>
                                                    </CustomButtons>
                                                    <HeaderCaptionTemplate>
                                                        <dxe:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" ImageUrl="~/imagens/botoes/incluirReg02.png">
                                                            <ClientSideEvents Click="function(s, e) {
	treeListItensMedicao.SetFocusedNodeKey(null);
	treeListItensMedicao.StartEditNewNode();
}" />
                                                        </dxe:ASPxImage>
                                                    </HeaderCaptionTemplate>
                                                </dxwtl:TreeListCommandColumn>
                                                <dxwtl:TreeListTextColumn FieldName="CodigoItemMedicaoContrato" Visible="False" VisibleIndex="1">
                                                    <EditFormSettings Visible="False" />
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListSpinEditColumn Caption="Ordem" FieldName="NumeroOrdem" ShowInCustomizationForm="True"
                                                    VisibleIndex="2" Width="40px" Visible="False">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:D3}" NumberFormat="Custom" NumberType="Integer">
                                                    </PropertiesSpinEdit>
                                                    <EditFormSettings CaptionLocation="Top" Visible="True" VisibleIndex="0" />
                                                </dxwtl:TreeListSpinEditColumn>
                                                <dxwtl:TreeListTextColumn Caption="Descrição" FieldName="DescricaoItem" VisibleIndex="3" Width="350px">
                                                    <PropertiesTextEdit MaxLength="255">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings ColumnSpan="5" VisibleIndex="1" CaptionLocation="Top" />
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListSpinEditColumn Caption="Quantidade" FieldName="QuantidadePrevistaTotal"
                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="155px">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:n8}" NumberFormat="Custom" DecimalPlaces="8" ClientInstanceName="spnQuantidade">
                                                        <SpinButtons ShowIncrementButtons="False">
                                                        </SpinButtons>
                                                    </PropertiesSpinEdit>
                                                    <EditFormSettings CaptionLocation="Top" VisibleIndex="2" />
                                                </dxwtl:TreeListSpinEditColumn>
                                                <dxwtl:TreeListTextColumn Caption="Unidade de Medida" FieldName="UnidadeMedida" VisibleIndex="5"
                                                    Width="175px">
                                                    <PropertiesTextEdit MaxLength="50">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" VisibleIndex="3" />
                                                    <HeaderStyle Wrap="True" />
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListSpinEditColumn Caption="Valor Unitário" FieldName="ValorUnitarioItem"
                                                    ShowInCustomizationForm="True" VisibleIndex="6" Width="120px">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:n2}" NumberFormat="Custom" DecimalPlaces="2" ClientInstanceName="spnValorUnitario">
                                                        <SpinButtons ShowIncrementButtons="False">
                                                        </SpinButtons>
                                                        <ClientSideEvents Validation="function(s, e) {
var valorContrato = tabControl.cp_ValorContrato;
var valorTotalItens =parseFloat(lblValorTotalItens.GetValue().toString().replace(&quot;R$&quot;,&quot;&quot;).replace(&quot;.&quot;,&quot;&quot;));
valorTotalItens +=  (spnQuantidade.GetValue() * e.value);
if(valorContrato &lt; valorTotalItens)
 {
e.isValid = false;
e.errorText = 'O valor dos itens não pode superar o valor do contrato';
 }
else
{
e.isValid = true;
}
}" />
                                                    </PropertiesSpinEdit>
                                                    <EditFormSettings CaptionLocation="Top" VisibleIndex="4" />
                                                </dxwtl:TreeListSpinEditColumn>
                                                <dxwtl:TreeListTextColumn Caption="Valor Total" FieldName="ValorTotalPrevisto" VisibleIndex="7"
                                                    ReadOnly="True" Width="175px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                                        <ReadOnlyStyle BackColor="LightGray">
                                                        </ReadOnlyStyle>
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" Visible="False" VisibleIndex="5" />
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListDateTimeColumn FieldName="DataPrevistaConclusao" Visible="False" VisibleIndex="8">
                                                </dxwtl:TreeListDateTimeColumn>
                                                <dxwtl:TreeListDateTimeColumn Caption="Conclusão Real" FieldName="DataConclusaoReal"
                                                    VisibleIndex="9" Visible="False">
                                                    <EditFormSettings Visible="False" />
                                                </dxwtl:TreeListDateTimeColumn>
                                                <dxwtl:TreeListTextColumn FieldName="CodigoItemPai" VisibleIndex="10" Visible="False">
                                                </dxwtl:TreeListTextColumn>
                                                <dxwtl:TreeListTextColumn FieldName="NumeroOrdemMaximo" Visible="False" VisibleIndex="11">
                                                    <EditFormSettings Visible="False" />
                                                </dxwtl:TreeListTextColumn>
                                            </Columns>
                                            <Settings VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" />
                                            <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" AllowDragDrop="False"
                                                AllowSort="False" />
                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="6" />
                                            <SettingsPopupEditForm AllowResize="True" HorizontalAlign="WindowCenter" Modal="True"
                                                Width="600px" Caption="Item" VerticalAlign="WindowCenter" />
                                            <SettingsText ConfirmDelete="Deseja excluir o registro?" />

<SettingsPopup>
<EditForm Caption="Item" Width="600px" Modal="True" AllowResize="True" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter"></EditForm>

<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                                            <Styles>
                                                <Header Wrap="True">
                                                </Header>
                                            </Styles>
                                            <ClientSideEvents CustomButtonClick="function(s, e) {
	var key = e.nodeKey;
	s.SetFocusedNodeKey(key);
	switch(e.buttonID){
		case &quot;btnEditar&quot;:
			s.StartEdit(key);
		break;
		case &quot;btnIncluir&quot;:
			s.StartEditNewNode(key);
		break;
		case &quot;btnExcluir&quot;:
			var r = confirm(&quot;Deseja realmente excluir o registro&quot;);
			if (r == true)
				s.DeleteNode(key);
		break;
	}
}" BeginCallback="function(s, e) {
	OnBeginCallback(s, e);
}" EndCallback="function(s, e) {
	OnEndCallback(s, e);
}" Init="function(s, e) {
	 var sHeight = Math.max(0, document.documentElement.clientHeight) - 155;
        s.SetHeight(sHeight);
}" />
                                        </dxwtl:ASPxTreeList>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Text="Preço Global" Name="tabPrecoGlobal">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                        <dxwgv:ASPxGridView ID="gvDadosPrecoGlobal" runat="server" AutoGenerateColumns="False"
                                            ClientInstanceName="gvDadosPrecoGlobal" DataSourceID="sdsItemMedicaoContratoPrecoGlobal"
                                             KeyFieldName="CodigoItemMedicaoContrato"
                                            OnCellEditorInitialize="gvDados_CellEditorInitialize" Width="100%">
                                            <ClientSideEvents BeginCallback="function(s, e) {
	OnBeginCallback(s, e);
}" EndCallback="function(s, e) {
	OnEndCallback(s, e);
}" Init="function(s, e) {
         var sHeight = Math.max(0, document.documentElement.clientHeight) - 155;
    s.SetHeight(sHeight);
}" />
                                            <GroupSummary>
                                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="colValorTotal" ShowInGroupFooterColumn="colValorTotal"
                                                    SummaryType="Sum" />
                                            </GroupSummary>
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                    Width="60px" ShowEditButton="true" ShowDeleteButton="true">
                                                    
                                                    <HeaderTemplate>
                                                        <%# ObtemBtnIncluirItemGlobal()%>
                                                    </HeaderTemplate>
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoItemMedicaoContrato" ReadOnly="True"
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                    <PropertiesTextEdit DisplayFormatString="{0:n2}" MaxLength="50">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings Visible="False" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataSpinEditColumn Caption=" " FieldName="NumeroOrdem" ShowInCustomizationForm="True"
                                                    VisibleIndex="2" Width="40px">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:D3}" NumberFormat="Custom">
                                                    </PropertiesSpinEdit>
                                                </dxwgv:GridViewDataSpinEditColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="DescricaoItem" ShowInCustomizationForm="True"
                                                    VisibleIndex="3" Width="350px">
                                                    <PropertiesTextEdit MaxLength="255">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="4" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Unidade de Medida" FieldName="UnidadeMedida"
                                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="175px">
                                                    <PropertiesTextEdit ClientInstanceName="txtValorTotal" MaxLength="50">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="1" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataSpinEditColumn Caption="Quantidade" FieldName="QuantidadePrevistaTotal"
                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="155px">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:n2}" NumberFormat="Custom" DisplayFormatInEditMode="True">
                                                        <SpinButtons ShowIncrementButtons="False">
                                                        </SpinButtons>
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesSpinEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="1" />
                                                </dxwgv:GridViewDataSpinEditColumn>
                                                <dxwgv:GridViewDataSpinEditColumn Caption="Valor Unitário" FieldName="ValorUnitarioItem"
                                                    ShowInCustomizationForm="True" VisibleIndex="6" Width="155px">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:n2}" NumberFormat="Custom" DisplayFormatInEditMode="True">
                                                        <SpinButtons ShowIncrementButtons="False">
                                                        </SpinButtons>
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesSpinEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="1" />
                                                </dxwgv:GridViewDataSpinEditColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Valor Total" FieldName="colValorTotal" Name="colValorTotal"
                                                    ReadOnly="True" ShowInCustomizationForm="True" UnboundExpression="[ValorUnitarioItem] * [QuantidadePrevistaTotal]"
                                                    UnboundType="Decimal" VisibleIndex="7" Width="175px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:n2}" MaxLength="50" DisplayFormatInEditMode="True">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings Visible="False" CaptionLocation="Top" ColumnSpan="1" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataDateColumn Caption="Conclusão Prevista" FieldName="DataPrevistaConclusao"
                                                    ShowInCustomizationForm="True" VisibleIndex="8" Width="150px">
                                                    <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" EditFormat="Custom" EditFormatString="dd/MM/yy">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesDateEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataDateColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Conclusão Real" FieldName="DataConclusaoReal"
                                                    ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="9" Width="150px">
                                                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings CaptionLocation="Top" ColumnSpan="2" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="NumeroOrdemMaximo" ShowInCustomizationForm="False"
                                                    Visible="False" VisibleIndex="12">
                                                    <EditFormSettings Visible="False" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False"
                                                AllowSort="False" AutoExpandAllGroups="True" ConfirmDelete="True" />
                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                            </SettingsPager>
                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="4" />
                                            <Settings GroupFormat="{1} {2}" ShowGroupFooter="VisibleAlways" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" />
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                    AllowResize="True" Width="700px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
                                            </SettingsPopup>
                                            <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                            <Styles>
                                                <Header Wrap="True">
                                                </Header>
                                            </Styles>
                                        </dxwgv:ASPxGridView>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Text="Importação de Cronograma" Name="tabImportacaoCronograma">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <dxwgv:ASPxGridView ID="gvDadosImportacaoCronograma" runat="server" AutoGenerateColumns="False"
                                                        ClientInstanceName="gvDadosImportacaoCronograma" DataSourceID="sdsImportacaoCronograma"
                                                         KeyFieldName="CodigoItemMedicaoContrato"
                                                        OnCellEditorInitialize="gvDados_CellEditorInitialize" Width="100%" OnCommandButtonInitialize="gvDadosImportacaoCronograma_CommandButtonInitialize"
                                                        OnHtmlRowPrepared="gvDadosImportacaoCronograma_HtmlRowPrepared">
                                                        <ClientSideEvents BeginCallback="function(s, e) {
	OnBeginCallback(s, e);
}" EndCallback="function(s, e) {
	OnEndCallback(s, e);
}" Init="function(s, e) {
        var sHeight = Math.max(0, document.documentElement.clientHeight) - 190;
   s.SetHeight(sHeight);
}" />
                                                        <GroupSummary>
                                                            <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="ValorTotalPrevisto" ShowInGroupFooterColumn="colValorTotal"
                                                                SummaryType="Sum" />
                                                        </GroupSummary>
                                                        <Columns>
                                                            <dxwgv:GridViewCommandColumn ButtonRenderMode="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                Width="40px" ShowDeleteButton="true">
                                                            </dxwgv:GridViewCommandColumn>
                                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoItemMedicaoContrato" ReadOnly="True"
                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                <PropertiesTextEdit DisplayFormatString="{0:n2}" MaxLength="50">
                                                                </PropertiesTextEdit>
                                                                <EditFormSettings Visible="False" />
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption=" " FieldName="NumeroOrdem" ShowInCustomizationForm="True"
                                                                VisibleIndex="2" Width="40px">
                                                                <PropertiesSpinEdit DisplayFormatString="{0:D3}" NumberFormat="Custom">
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Descrição" FieldName="DescricaoItem" ShowInCustomizationForm="True"
                                                                VisibleIndex="3" Width="350px">
                                                                <PropertiesTextEdit MaxLength="255">
                                                                    <ValidationSettings Display="Dynamic">
                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </PropertiesTextEdit>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Unidade de Medida" FieldName="UnidadeMedida"
                                                                ShowInCustomizationForm="True" VisibleIndex="5" Width="175px">
                                                                <PropertiesTextEdit ClientInstanceName="txtValorTotal" MaxLength="50">
                                                                    <ValidationSettings Display="Dynamic">
                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </PropertiesTextEdit>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Quantidade" FieldName="QuantidadePrevistaTotal"
                                                                ShowInCustomizationForm="True" VisibleIndex="4" Width="155px">
                                                                <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:n2}" NumberFormat="Custom">
                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                    </SpinButtons>
                                                                    <ValidationSettings Display="Dynamic">
                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataSpinEditColumn Caption="Valor Unitário" FieldName="ValorUnitarioItem"
                                                                ShowInCustomizationForm="True" VisibleIndex="6" Width="155px">
                                                                <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:n2}" NumberFormat="Custom">
                                                                    <SpinButtons ShowIncrementButtons="False">
                                                                    </SpinButtons>
                                                                    <ValidationSettings Display="Dynamic">
                                                                        <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </PropertiesSpinEdit>
                                                            </dxwgv:GridViewDataSpinEditColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Valor Total" FieldName="ValorTotalPrevisto"
                                                                Name="colValorTotal" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="7"
                                                                Width="175px">
                                                                <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:n2}" MaxLength="50">
                                                                </PropertiesTextEdit>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataDateColumn Caption="Conclusão Prevista" FieldName="DataPrevistaConclusao"
                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="8" Width="80px">
                                                                <PropertiesDateEdit DisplayFormatString="{0:dd/MM/yyyy}" EditFormat="Custom" EditFormatString="dd/MM/yy">
                                                                </PropertiesDateEdit>
                                                                <EditFormSettings Visible="False" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataDateColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Conclusão Real" FieldName="DataConclusaoReal"
                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="9" Width="175px">
                                                                <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                                </PropertiesTextEdit>
                                                                <EditFormSettings Visible="False" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn FieldName="CodigoItemPai" ShowInCustomizationForm="True"
                                                                Visible="False" VisibleIndex="10">
                                                                <EditFormSettings Visible="False" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="Grupo" FieldName="DescricaoItemPai" GroupIndex="0"
                                                                ReadOnly="True" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending"
                                                                VisibleIndex="11" Width="270px">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn FieldName="NumeroOrdemMaximo" ShowInCustomizationForm="False"
                                                                Visible="False" VisibleIndex="13">
                                                                <EditFormSettings Visible="False" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn FieldName="IndicaImportacaoConfirmada" ShowInCustomizationForm="True"
                                                                Visible="False" VisibleIndex="12">
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False"
                                                            AllowSort="False" AutoExpandAllGroups="True" />
                                                        <SettingsPager Mode="ShowAllRecords" Visible="False">
                                                        </SettingsPager>
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsPopup>
                                                            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                                AllowResize="True" Width="400px" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
                                                        </SettingsPopup>
                                                        <Settings GroupFormat="{1} {2}" ShowGroupFooter="VisibleAlways" VerticalScrollBarMode="Visible" HorizontalScrollBarMode="Visible" />
                                                        <SettingsText />
                                                        <Styles>
                                                            <Header Wrap="True">
                                                            </Header>
                                                        </Styles>
                                                    </dxwgv:ASPxGridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="padding-top: 0px;">
                                                    <table>
                                                        <tr>
                                                            <td style="padding-right: 10px">
                                                                <dxe:ASPxButton ID="btnImportar" runat="server" AutoPostBack="False" Text="Importar"
                                                                    Width="75px">
                                                                    <ClientSideEvents Click="function(s, e) {
	ImportaItensMedicaoCronograma();
}" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td style="padding-right: 10px">
                                                                <dxe:ASPxButton ID="btnSalvar" runat="server" AutoPostBack="False" Text="Salvar"
                                                                    Width="75px">
                                                                    <ClientSideEvents Click="function(s, e) {
	SalvaItensMedicaoCronograma();
}" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxButton ID="btnCancelar" runat="server" AutoPostBack="False" Text="Cancelar"
                                                                    Width="75px">
                                                                    <ClientSideEvents Click="function(s, e) {
	CancelaImportacaoItensMedicaoCronograma();
}" />
                                                                </dxe:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                            <dxtc:TabPage Text="Responsáveis pela Medição">
                                <ContentCollection>
                                    <dxw:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dxwgv:ASPxGridView ID="gvResponsaveisMedicao" runat="server" AutoGenerateColumns="False"
                                            ClientInstanceName="gvResponsaveisMedicao" DataSourceID="sdsResponsaveisMedicao"
                                             OnCellEditorInitialize="gvResponsaveisMedicao_CellEditorInitialize"
                                            Width="100%" KeyFieldName="CodigoUsuario" OnRowInserting="gvResponsaveisMedicao_RowInserting"
                                            OnRowUpdating="gvResponsaveisMedicao_RowUpdating" OnRowDeleted="gvResponsaveisMedicao_RowDeleted">
                                            <GroupSummary>
                                                <dxwgv:ASPxSummaryItem DisplayFormat="n2" FieldName="colValorTotal" ShowInGroupFooterColumn="colValorTotal"
                                                    SummaryType="Sum" />
                                            </GroupSummary>
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonRenderMode="Image" Width="60px" ShowEditButton="true"
                                                    ShowDeleteButton="true">
                                                    <HeaderTemplate>
                                                        <img src="../../imagens/botoes/incluirReg02.png" title="Responsáveis pela Medição"
                                                            onclick="gvResponsaveisMedicao.AddNewRow();" style="cursor: pointer;" alt="" />
                                                    </HeaderTemplate>
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CodigoContrato" ShowInCustomizationForm="True"
                                                    VisibleIndex="1" Visible="False">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Responsável" FieldName="NomeUsuario" ShowInCustomizationForm="True"
                                                    VisibleIndex="2">
                                                    <PropertiesTextEdit>
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings Visible="False" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataComboBoxColumn FieldName="CodigoUsuario" ShowInCustomizationForm="True"
                                                    VisibleIndex="3" Width="50%" Visible="False">
                                                    <PropertiesComboBox DataSourceID="sdsUsuarios" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                        TextField="NomeUsuario" ValueField="CodigoUsuario" ValueType="System.Int32">
                                                        <Columns>
                                                            <dxe:ListBoxColumn Caption="Usuário" FieldName="NomeUsuario" Width="100%" />
                                                        </Columns>
                                                    </PropertiesComboBox>
                                                    <EditFormSettings Caption="Responsável" CaptionLocation="Top" ColumnSpan="4" Visible="True"
                                                        VisibleIndex="1" />
                                                </dxwgv:GridViewDataComboBoxColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="DescricaoCargo" ShowInCustomizationForm="True"
                                                    VisibleIndex="4" Caption="Cargo">
                                                    <PropertiesTextEdit MaxLength="32">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings Caption="Cargo" CaptionLocation="Top" ColumnSpan="5" Visible="True"
                                                        VisibleIndex="2" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataSpinEditColumn FieldName="NumeroOrdemAssinatura" ShowInCustomizationForm="True"
                                                    Visible="False" VisibleIndex="5">
                                                    <PropertiesSpinEdit DisplayFormatString="n0" NumberFormat="Custom" NumberType="Integer">
                                                        <ValidationSettings Display="Dynamic">
                                                            <RequiredField ErrorText="Campo Obrigatório" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesSpinEdit>
                                                    <EditFormSettings Caption="Ordem" CaptionLocation="Top" ColumnSpan="1" Visible="True"
                                                        VisibleIndex="0" />
                                                </dxwgv:GridViewDataSpinEditColumn>
                                            </Columns>
                                            <SettingsBehavior AllowDragDrop="False" AllowFocusedRow="True" AllowGroup="False"
                                                AllowSort="False" AutoExpandAllGroups="True" ConfirmDelete="True" />
                                            <ClientSideEvents Init="function(s, e) {
       var sHeight = Math.max(0, document.documentElement.clientHeight) - 155;
   s.SetHeight(sHeight);
}" />
                                            <SettingsPager Mode="ShowAllRecords" Visible="False">
                                            </SettingsPager>
                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="5" />
                                            <Settings GroupFormat="{1} {2}" ShowGroupFooter="VisibleAlways" VerticalScrollBarMode="Visible" />
                                            <SettingsText ConfirmDelete="Deseja excluir o registro?" />
                                            <SettingsPopup>
                                                <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter"
                                                    Width="400px" AllowResize="true" />

<HeaderFilter MinHeight="140px"></HeaderFilter>
                                            </SettingsPopup>
                                            <Styles>
                                                <Header Wrap="True">
                                                </Header>
                                            </Styles>
                                        </dxwgv:ASPxGridView>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                        </TabPages>
                        <Paddings Padding="0px" />
                    </dxtc:ASPxPageControl>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 0px; width: 100%;">
                        <tr>
                                                        <td style="width: 145px">
                                <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Valor total do contrato:" Width="100%"
                                   >
                                </dxe:ASPxLabel>
                            </td>
                            <td style="padding-left: 10px; padding-right: 5px;">
                                 <dxe:ASPxLabel ID="lblValorTotalContrato" runat="server" 
                                                Text="0" ClientInstanceName="lblValorTotalContrato">
                                 </dxe:ASPxLabel>

                            </td>
                            <td style="width: 145px">
                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Valor total dos itens:" Width="100%"
                                   >
                                </dxe:ASPxLabel>
                            </td>
                            <td style="padding-left: 10px">
                                <dxcp:ASPxCallbackPanel ID="callbackPanel" runat="server" ClientInstanceName="callbackPanel"
                                    Width="200px">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                            <dxtv:ASPxLabel ID="lblValorTotalItens" runat="server" ClientInstanceName="lblValorTotalItens" Text="0">
                                            </dxtv:ASPxLabel>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxCallbackPanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:SqlDataSource ID="sdsItemMedicaoContrato" runat="server" SelectCommand=" SELECT imc.CodigoItemMedicaoContrato, 
        ISNULL(imc.[CodigoImportacaoPlanilha],'') + ' ' + imc.[DescricaoItem] AS DescricaoItem, 
        imc.NumeroOrdem,
        ( SELECT ISNULL(COUNT(NumeroOrdem), 0)
            FROM ItemMedicaoContrato
          WHERE (CodigoItemPai = imc.CodigoItemPai OR (CodigoItemPai IS NULL AND imc.CodigoItemPai IS NULL))
			   AND (imc.CodigoContrato = @CodigoContrato) 
			   AND (CodigoPrograma = @CodigoPrograma OR (CodigoPrograma IS NULL AND @CodigoPrograma = -1))
			   AND (CodigoProjeto = @CodigoProjeto OR (CodigoProjeto IS NULL AND @CodigoProjeto = -1))
			   AND (ISNULL(IndicaItemPrecoGlobal,'') &lt;&gt; 'S')
			   AND (CodigoAtribuicao IS NULL)
			   AND (DataExclusaoItem IS NULL)) AS NumeroOrdemMaximo,
        imc.UnidadeMedida, 
        imc.QuantidadePrevistaTotal, 
        imc.ValorUnitarioItem, 
        (imc.QuantidadePrevistaTotal * imc.ValorUnitarioItem) AS ValorTotalPrevisto,
        imc.DataPrevistaConclusao, 
        imc.DataConclusaoReal, 
        imc.CodigoItemPai,
        (right('000'+CONVERT(VARCHAR(3),imc_pai.NumeroOrdem), 3)+ '. ' + imc_pai.DescricaoItem) AS DescricaoItemPai
   FROM ItemMedicaoContrato AS imc LEFT JOIN
        ItemMedicaoContrato imc_pai ON imc.CodigoItemPai = imc_pai.CodigoItemMedicaoContrato
  WHERE (imc.CodigoContrato = @CodigoContrato) 
    AND (imc.CodigoPrograma = @CodigoPrograma OR (imc.CodigoPrograma IS NULL AND @CodigoPrograma = -1))
    AND (imc.CodigoProjeto = @CodigoProjeto OR (imc.CodigoProjeto IS NULL AND @CodigoProjeto = -1))
    AND (ISNULL(imc.IndicaItemPrecoGlobal,'') &lt;&gt; 'S')
    AND (imc.CodigoAtribuicao IS NULL)
    AND (imc.DataExclusaoItem IS NULL)
  ORDER BY
        imc_pai.NumeroOrdem,
        imc.NumeroOrdem,
        imc.DescricaoItem" DeleteCommand="DECLARE @CodigoContrato INT,
		@CodigoPrograma INT,
		@CodigoProjeto INT,
		@CodigoItemPai INT,
        @NumeroOrdem INT
        
 SELECT @CodigoItemPai = CodigoItemPai,
        @NumeroOrdem = NumeroOrdem,
        @CodigoContrato = CodigoContrato,
        @CodigoPrograma = CodigoPrograma,
        @CodigoProjeto = CodigoProjeto
   FROM [ItemMedicaoContrato] 
  WHERE [CodigoItemMedicaoContrato] = @Original_CodigoItemMedicaoContrato

 UPDATE [ItemMedicaoContrato]
    SET [DataExclusaoItem] = GETDATE()
  WHERE [CodigoItemMedicaoContrato] = @Original_CodigoItemMedicaoContrato

IF (@CodigoItemPai IS NULL OR @CodigoItemPai = 0)
BEGIN      
 UPDATE [ItemMedicaoContrato]
    SET [NumeroOrdem] = [NumeroOrdem] - 1
  WHERE ([CodigoItemPai] IS NULL OR [CodigoItemPai] = 0)  
	AND ([CodigoContrato] = @CodigoContrato)  
    AND ([CodigoPrograma] = @CodigoPrograma OR ([CodigoPrograma] IS NULL AND @CodigoPrograma  IS NULL))  
	AND ([CodigoProjeto] = @CodigoProjeto OR ([CodigoProjeto] IS NULL AND @CodigoProjeto  IS NULL))
    AND ([NumeroOrdem] &gt; @NumeroOrdem)
    AND ([DataExclusaoItem] IS NULL)
END
ELSE
BEGIN
 UPDATE [ItemMedicaoContrato]
    SET [NumeroOrdem] = [NumeroOrdem] - 1
  WHERE ([CodigoItemPai] = @CodigoItemPai)
    AND ([NumeroOrdem] &gt; @NumeroOrdem)
    AND ([DataExclusaoItem] IS NULL)
END" UpdateCommand="DECLARE @CodigoContrato INT,
		@CodigoPrograma INT,
		@CodigoProjeto INT
		
 SELECT @CodigoContrato = CodigoContrato,
		@CodigoPrograma = CodigoPrograma,
		@CodigoProjeto = CodigoProjeto
   FROM [ItemMedicaoContrato]
  WHERE [CodigoItemMedicaoContrato] = @CodigoItemMedicaoContrato

UPDATE [ItemMedicaoContrato]
   SET [DescricaoItem] = @DescricaoItem
      ,[QuantidadePrevistaTotal] = @QuantidadePrevistaTotal
      ,[UnidadeMedida] = @UnidadeMedida
      ,[ValorUnitarioItem] = @ValorUnitarioItem
      ,[DataPrevistaConclusao] = @DataPrevistaConclusao
      ,[DataConclusaoReal] = @DataConclusaoReal
      ,[NumeroOrdem] = @NumeroOrdem
 WHERE
       [CodigoItemMedicaoContrato] = @CodigoItemMedicaoContrato

IF(@Original_NumeroOrdem = -1)
BEGIN
UPDATE [ItemMedicaoContrato]
   SET [NumeroOrdem] = [NumeroOrdem] + 1
 WHERE [CodigoItemMedicaoContrato] &lt;&gt; @CodigoItemMedicaoContrato
   AND [CodigoItemPai] = @CodigoItemPai
   AND [NumeroOrdem] &gt;= @NumeroOrdem
   AND [DataExclusaoItem] IS NULL
END
ELSE IF(@Original_NumeroOrdem &gt; @NumeroOrdem)
BEGIN
	IF (@CodigoItemPai IS NULL OR @CodigoItemPai = 0)
	BEGIN
	UPDATE [ItemMedicaoContrato]
	   SET [NumeroOrdem] = [NumeroOrdem] + 1
	 WHERE [CodigoItemMedicaoContrato] &lt;&gt; @CodigoItemMedicaoContrato
	   AND (CodigoItemPai IS NULL OR CodigoItemPai = 0)
	   AND ([CodigoContrato] = @CodigoContrato)  
       AND ([CodigoPrograma] = @CodigoPrograma OR ([CodigoPrograma] IS NULL AND @CodigoPrograma IS NULL))  
	   AND ([CodigoProjeto] = @CodigoProjeto OR ([CodigoProjeto] IS NULL AND @CodigoProjeto IS NULL))
	   AND [NumeroOrdem] &gt;= @NumeroOrdem
	   AND [NumeroOrdem] &lt; @Original_NumeroOrdem
	   AND [DataExclusaoItem] IS NULL
	END
	ELSE
	BEGIN
	UPDATE [ItemMedicaoContrato]
	   SET [NumeroOrdem] = [NumeroOrdem] + 1
	 WHERE [CodigoItemMedicaoContrato] &lt;&gt; @CodigoItemMedicaoContrato
	   AND [CodigoItemPai] = @CodigoItemPai
	   AND [NumeroOrdem] &gt;= @NumeroOrdem
	   AND [NumeroOrdem] &lt; @Original_NumeroOrdem
	   AND [DataExclusaoItem] IS NULL
	END
END
ELSE IF(@Original_NumeroOrdem &lt; @NumeroOrdem)
BEGIN
	IF (@CodigoItemPai IS NULL OR @CodigoItemPai = 0)
	BEGIN
	UPDATE [ItemMedicaoContrato]
	   SET [NumeroOrdem] = [NumeroOrdem] - 1
	 WHERE [CodigoItemMedicaoContrato] &lt;&gt; @CodigoItemMedicaoContrato
	   AND (CodigoItemPai IS NULL OR CodigoItemPai = 0)
	   AND ([CodigoContrato] = @CodigoContrato)  
       AND ([CodigoPrograma] = @CodigoPrograma OR ([CodigoPrograma] IS NULL AND @CodigoPrograma  IS NULL))  
	   AND ([CodigoProjeto] = @CodigoProjeto OR ([CodigoProjeto] IS NULL AND @CodigoProjeto  IS NULL))  
	   AND [NumeroOrdem] &lt;= @NumeroOrdem
	   AND [NumeroOrdem] &gt; @Original_NumeroOrdem
	   AND [DataExclusaoItem] IS NULL
	END
	ELSE
	BEGIN
	UPDATE [ItemMedicaoContrato]
	   SET [NumeroOrdem] = [NumeroOrdem] - 1
	 WHERE [CodigoItemMedicaoContrato] &lt;&gt; @CodigoItemMedicaoContrato
	   AND [CodigoItemPai] = @CodigoItemPai 
	   AND [NumeroOrdem] &lt;= @NumeroOrdem
	   AND [NumeroOrdem] &gt; @Original_NumeroOrdem
	   AND [DataExclusaoItem] IS NULL
	END
END" ConflictDetection="CompareAllValues" OldValuesParameterFormatString="Original_{0}"
        InsertCommand="  INSERT INTO [ItemMedicaoContrato] 
            ( [CodigoContrato]
            , [CodigoPrograma]
            , [CodigoProjeto]
            , [CodigoItemPai]
            , [DescricaoItem]
            , [QuantidadePrevistaTotal]
            , [UnidadeMedida]
            , [ValorUnitarioItem]
            , [IndicaItemPrecoGlobal]
            , [NumeroOrdem]) 
     VALUES ( @CodigoContrato
            , @CodigoPrograma
            , @CodigoProjeto
            , @CodigoItemPai
            , @DescricaoItem
            , @QuantidadePrevistaTotal
            , @UnidadeMedida
            , @ValorUnitarioItem
            , 'N'
            , @NumeroOrdem)

SET @CodigoItemMedicaoContrato = @@IDENTITY
            
 UPDATE [ItemMedicaoContrato]
    SET [NumeroOrdem] = [NumeroOrdem] + 1
  WHERE [CodigoItemMedicaoContrato] &lt;&gt; @CodigoItemMedicaoContrato
    AND ISNULL([CodigoItemPai],-1) = ISNULL(@CodigoItemPai, -1)
    AND [IndicaItemPrecoGlobal] = 'N'
    AND [CodigoContrato] = @CodigoContrato
    AND ([CodigoPrograma] = @CodigoPrograma OR ([CodigoPrograma] IS NULL AND @CodigoPrograma IS NULL))
    AND ([CodigoProjeto] = @CodigoProjeto OR ([CodigoProjeto] IS NULL AND @CodigoProjeto IS NULL))
    AND [NumeroOrdem] &gt;= @NumeroOrdem
    AND [DataExclusaoItem] IS NULL
    
 UPDATE [ItemMedicaoContrato] 
	SET [QuantidadePrevistaTotal] = NULL, 
		[UnidadeMedida] = NULL,
		[ValorUnitarioItem] = NULL
  WHERE [CodigoItemMedicaoContrato] = @CodigoItemPai">
        <DeleteParameters>
            <asp:Parameter Name="Original_CodigoItemMedicaoContrato" />
        </DeleteParameters>
        <InsertParameters>
            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
            <asp:SessionParameter Name="CodigoPrograma" SessionField="CodigoPrograma" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="CodigoProjeto" />
            <asp:Parameter Name="DescricaoItem" />
            <asp:Parameter Name="QuantidadePrevistaTotal" />
            <asp:Parameter Name="UnidadeMedida" />
            <asp:Parameter Name="ValorUnitarioItem" />
            <asp:Parameter Name="DataPrevistaConclusao" />
            <asp:Parameter Name="NumeroOrdem" />
            <asp:Parameter Name="CodigoItemMedicaoContrato" />
            <asp:Parameter Name="CodigoItemPai" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
            <asp:SessionParameter DefaultValue="-1" Name="CodigoPrograma" SessionField="CodigoPrograma" />
            <asp:SessionParameter DefaultValue="-1" Name="CodigoProjeto" SessionField="CodigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CodigoItemMedicaoContrato" />
            <asp:Parameter Name="DescricaoItem" />
            <asp:Parameter Name="QuantidadePrevistaTotal" />
            <asp:Parameter Name="UnidadeMedida" />
            <asp:Parameter Name="ValorUnitarioItem" />
            <asp:Parameter Name="DataPrevistaConclusao" />
            <asp:Parameter Name="DataConclusaoReal" />
            <asp:Parameter Name="NumeroOrdem" />
            <asp:Parameter Name="CodigoItemPai" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsItemMedicaoContratoPrecoGlobal" runat="server" DeleteCommand=" UPDATE [ItemMedicaoContrato] 
    SET [DataExclusaoItem] = GETDATE() 
  WHERE [CodigoItemMedicaoContrato] = @Original_CodigoItemMedicaoContrato

 UPDATE [ItemMedicaoContrato]
    SET [NumeroOrdem] = [NumeroOrdem] - 1
  WHERE [CodigoItemMedicaoContrato] &lt;&gt; @Original_CodigoItemMedicaoContrato
    AND [IndicaItemPrecoGlobal] = 'S'
    AND [CodigoContrato] = @CodigoContrato
    AND ([CodigoPrograma] = @CodigoPrograma OR ([CodigoPrograma] IS NULL AND @CodigoPrograma IS NULL))
    AND ([CodigoProjeto] = @CodigoProjeto OR ([CodigoProjeto] IS NULL AND @CodigoProjeto IS NULL))
    AND [NumeroOrdem] &gt;= @Original_NumeroOrdem
    AND [DataExclusaoItem] IS NULL" InsertCommand="  INSERT INTO [ItemMedicaoContrato] 
            ( [CodigoContrato]
            , [CodigoPrograma]
            , [CodigoProjeto]
            , [DescricaoItem]
            , [QuantidadePrevistaTotal]
            , [UnidadeMedida]
            , [ValorUnitarioItem]
            , [DataPrevistaConclusao]
            , [IndicaItemPrecoGlobal]
            , [NumeroOrdem]) 
     VALUES ( @CodigoContrato
            , @CodigoPrograma
            , @CodigoProjeto
            , @DescricaoItem
            , @QuantidadePrevistaTotal
            , @UnidadeMedida
            , @ValorUnitarioItem
            , @DataPrevistaConclusao
            , 'S'
            , @NumeroOrdem)

SET @CodigoItemMedicaoContrato = @@IDENTITY
            
 UPDATE [ItemMedicaoContrato]
    SET [NumeroOrdem] = [NumeroOrdem] + 1
  WHERE [CodigoItemMedicaoContrato] &lt;&gt; @CodigoItemMedicaoContrato
    AND [IndicaItemPrecoGlobal] = 'S'
    AND [CodigoContrato] = @CodigoContrato
    AND ([CodigoPrograma] = @CodigoPrograma OR ([CodigoPrograma] IS NULL AND @CodigoPrograma IS NULL))
    AND ([CodigoProjeto] = @CodigoProjeto OR ([CodigoProjeto] IS NULL AND @CodigoProjeto IS NULL))
    AND [NumeroOrdem] &gt;= @NumeroOrdem
    AND [DataExclusaoItem] IS NULL" SelectCommand=" SELECT imc.CodigoItemMedicaoContrato, 
        imc.DescricaoItem, 
        imc.NumeroOrdem,
        (SELECT ISNULL(COUNT(NumeroOrdem), 0)
           FROM ItemMedicaoContrato
          WHERE IndicaItemPrecoGlobal = 'S'
            AND CodigoContrato = imc.CodigoContrato
            AND (CodigoPrograma = imc.CodigoPrograma OR (CodigoPrograma IS NULL AND imc.CodigoPrograma IS NULL))
            AND (CodigoProjeto = imc.CodigoProjeto OR (CodigoProjeto IS NULL AND imc.CodigoProjeto IS NULL))
            AND DataExclusaoItem IS NULL) AS NumeroOrdemMaximo,
        imc.UnidadeMedida, 
        imc.QuantidadePrevistaTotal, 
        imc.ValorUnitarioItem, 
        imc.DataPrevistaConclusao, 
        imc.DataConclusaoReal
   FROM ItemMedicaoContrato AS imc
  WHERE (imc.CodigoContrato = @CodigoContrato) 
    AND (imc.CodigoPrograma = @CodigoPrograma OR (imc.CodigoPrograma IS NULL AND @CodigoPrograma = -1))
    AND (imc.CodigoProjeto = @CodigoProjeto OR (imc.CodigoProjeto IS NULL AND @CodigoProjeto = -1))
    AND (ISNULL(imc.IndicaItemPrecoGlobal,'') = 'S')
    AND (imc.DataExclusaoItem IS NULL)
  ORDER BY
        imc.NumeroOrdem,
        imc.DescricaoItem" UpdateCommand=" UPDATE [ItemMedicaoContrato] 
    SET [DescricaoItem] = @DescricaoItem, 
        [QuantidadePrevistaTotal] = @QuantidadePrevistaTotal, 
        [UnidadeMedida] = @UnidadeMedida, 
        [ValorUnitarioItem] = @ValorUnitarioItem, 
        [DataPrevistaConclusao] = @DataPrevistaConclusao, 
        [NumeroOrdem] = @NumeroOrdem 
  WHERE [CodigoItemMedicaoContrato] = @CodigoItemMedicaoContrato

IF(@Original_NumeroOrdem &gt; @NumeroOrdem)
BEGIN
UPDATE [ItemMedicaoContrato]
   SET [NumeroOrdem] = [NumeroOrdem] + 1
 WHERE [CodigoItemMedicaoContrato] &lt;&gt; @CodigoItemMedicaoContrato
   AND [IndicaItemPrecoGlobal] = 'S'
   AND [CodigoContrato] = @CodigoContrato
   AND ([CodigoPrograma] = @CodigoPrograma OR ([CodigoPrograma] IS NULL AND @CodigoPrograma = -1))
   AND ([CodigoProjeto] = @CodigoProjeto OR ([CodigoProjeto] IS NULL AND @CodigoProjeto = -1))
   AND [NumeroOrdem] &gt;= @NumeroOrdem
   AND [NumeroOrdem] &lt; @Original_NumeroOrdem
   AND [DataExclusaoItem] IS NULL
END
ELSE IF(@Original_NumeroOrdem &lt; @NumeroOrdem)
BEGIN
UPDATE [ItemMedicaoContrato]
   SET [NumeroOrdem] = [NumeroOrdem] - 1
 WHERE [CodigoItemMedicaoContrato] &lt;&gt; @CodigoItemMedicaoContrato
   AND [IndicaItemPrecoGlobal] = 'S'
   AND [CodigoContrato] = @CodigoContrato
   AND ([CodigoPrograma] = @CodigoPrograma OR ([CodigoPrograma] IS NULL AND @CodigoPrograma IS NULL))
   AND ([CodigoProjeto] = @CodigoProjeto OR ([CodigoProjeto] IS NULL AND @CodigoProjeto IS NULL))
   AND [NumeroOrdem] &lt;= @NumeroOrdem
   AND [NumeroOrdem] &gt; @Original_NumeroOrdem
   AND [DataExclusaoItem] IS NULL
END" ConflictDetection="CompareAllValues" OldValuesParameterFormatString="Original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="Original_CodigoItemMedicaoContrato" />
            <asp:Parameter Name="Original_NumeroOrdem" />
            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
            <asp:SessionParameter Name="CodigoPrograma" SessionField="CodigoPrograma" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="CodigoProjeto" />
        </DeleteParameters>
        <InsertParameters>
            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
            <asp:SessionParameter Name="CodigoPrograma" SessionField="CodigoPrograma" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="CodigoProjeto" />
            <asp:Parameter Name="DescricaoItem" />
            <asp:Parameter Name="QuantidadePrevistaTotal" />
            <asp:Parameter Name="UnidadeMedida" />
            <asp:Parameter Name="ValorUnitarioItem" />
            <asp:Parameter Name="DataPrevistaConclusao" />
            <asp:Parameter Name="NumeroOrdem" />
            <asp:Parameter Name="CodigoItemMedicaoContrato" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
            <asp:SessionParameter DefaultValue="-1" Name="CodigoPrograma" SessionField="CodigoPrograma" />
            <asp:SessionParameter DefaultValue="-1" Name="CodigoProjeto" SessionField="CodigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CodigoItemMedicaoContrato" />
            <asp:Parameter Name="DescricaoItem" />
            <asp:Parameter Name="QuantidadePrevistaTotal" />
            <asp:Parameter Name="UnidadeMedida" />
            <asp:Parameter Name="ValorUnitarioItem" />
            <asp:Parameter Name="DataPrevistaConclusao" />
            <asp:Parameter Name="NumeroOrdem" />
            <asp:Parameter Name="Original_NumeroOrdem" />
            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
            <asp:SessionParameter Name="CodigoPrograma" SessionField="CodigoPrograma" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="CodigoProjeto" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsImportacaoCronograma" runat="server" DeleteCommand=" UPDATE ItemMedicaoContrato
    SET IndicaImportacaoConfirmada = CASE IndicaImportacaoConfirmada
                                        WHEN 'S' THEN 'XS'
                                        WHEN 'P' THEN 'XP'
                                        WHEN 'XS' THEN 'S'
                                        WHEN 'XP' THEN 'P' 
                                     END
  WHERE CodigoItemMedicaoContrato = @CodigoItemMedicaoContrato" InsertCommand="[p_ImportaRecursosCronogramaMedicao]"
        SelectCommand=" SELECT imc.CodigoItemMedicaoContrato, 
        imc.DescricaoItem, 
        imc.NumeroOrdem,
        ( SELECT ISNULL(COUNT(NumeroOrdem), 0)
            FROM ItemMedicaoContrato
          WHERE CodigoItemPai = imc.CodigoItemPai
               AND DataExclusaoItem IS NULL) AS NumeroOrdemMaximo,
        imc.UnidadeMedida, 
        imc.QuantidadePrevistaTotal, 
        imc.ValorUnitarioItem, 
        imc.DataPrevistaConclusao, 
        imc.DataConclusaoReal, 
        imc.CodigoItemPai,
        (right('000'+CONVERT(VARCHAR(3),imc_pai.NumeroOrdem), 3)+ '. ' + imc_pai.DescricaoItem) AS DescricaoItemPai,
        imc.ValorTotalPrevisto,
        imc.IndicaImportacaoConfirmada
   FROM ItemMedicaoContrato AS imc INNER JOIN
        ItemMedicaoContrato imc_pai ON imc.CodigoItemPai = imc_pai.CodigoItemMedicaoContrato
  WHERE (imc.CodigoContrato = @CodigoContrato) 
    AND (imc.CodigoPrograma = @CodigoPrograma OR (imc.CodigoPrograma IS NULL AND @CodigoPrograma = -1))
    AND (imc.CodigoProjeto = @CodigoProjeto OR (imc.CodigoProjeto IS NULL AND @CodigoProjeto = -1))
    AND (imc.CodigoItemPai &lt;&gt; 0)
    AND (imc.CodigoAtribuicao IS NOT NULL)
    AND (LEFT(UPPER(imc.IndicaImportacaoConfirmada), 1) IN ('S', 'P', 'X'))
    AND (imc.DataExclusaoItem IS NULL)
  ORDER BY
        imc_pai.NumeroOrdem,
        imc.NumeroOrdem,
        imc.DescricaoItem" InsertCommandType="StoredProcedure" UpdateCommand=" UPDATE ItemMedicaoContrato
    SET IndicaImportacaoConfirmada = CASE IndicaImportacaoConfirmada
                                        WHEN 'S' THEN 'S'
                                        WHEN 'P' THEN 'S'
                                        ELSE 'N'
                                     END
  WHERE CodigoAtribuicao IS NOT NULL
    AND DataExclusaoItem IS NULL
    AND CodigoContrato = @CodigoContrato
    AND ([CodigoPrograma] = @CodigoPrograma OR ([CodigoPrograma] IS NULL AND @CodigoPrograma = -1))
    AND ([CodigoProjeto] = @CodigoProjeto OR ([CodigoProjeto] IS NULL AND @CodigoProjeto = -1))">
        <DeleteParameters>
            <asp:Parameter Name="CodigoItemMedicaoContrato" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
            <asp:QueryStringParameter Name="in_CodigoContrato" QueryStringField="CC" Type="Int32" />
            <asp:SessionParameter Name="in_CodigoPrograma" SessionField="CodigoPrograma" Type="Int32" />
            <asp:SessionParameter Name="in_CodigoProjeto" SessionField="CodigoProjeto" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
            <asp:SessionParameter Name="CodigoPrograma" SessionField="CodigoPrograma" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="CodigoProjeto" />
        </SelectParameters>
        <UpdateParameters>
            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
            <asp:SessionParameter Name="CodigoPrograma" SessionField="CodigoPrograma" />
            <asp:SessionParameter Name="CodigoProjeto" SessionField="CodigoProjeto" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsProjetosDoPrograma" runat="server" SelectCommand=" SELECT NULL AS CodigoProjeto, NULL AS NomeProjeto
UNION
 SELECT p.CodigoProjeto, p.NomeProjeto
   FROM LinkProjeto lp INNER JOIN 
        Projeto p ON p.CodigoProjeto = lp.CodigoProjetoFilho
  WHERE lp.CodigoProjetoPai = @CodigoPrograma
    AND TipoLink = 'PP'
  ORDER BY 2">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoPrograma" SessionField="CodigoPrograma" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsProjetosEntidade" runat="server" SelectCommand=" SELECT NULL AS CodigoProjeto, NULL AS NomeProjeto
UNION
 SELECT p.CodigoProjeto, p.NomeProjeto
   FROM Projeto p
  WHERE p.CodigoUnidadeNegocio = @CodigoEntidade">
        <SelectParameters>
            <asp:SessionParameter Name="CodigoEntidade" SessionField="CodigoEntidade" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource runat="server" SelectCommand=" SELECT AMC.CodigoContrato, 
		AMC.CodigoUsuario, 
		AMC.DescricaoCargo, 
		AMC.NumeroOrdemAssinatura, 
		(SELECT ISNULL(COUNT(NumeroOrdemAssinatura), 0) AS NumeroOrdemAssinatura 
		   FROM AssinaturasMedicaoContrato 
		  WHERE (CodigoContrato = @CodigoContrato)) AS NumeroOrdemMaximo,
		U.NomeUsuario
   FROM AssinaturasMedicaoContrato AS AMC INNER JOIN
		Usuario U ON U.CodigoUsuario = AMC.CodigoUsuario
  WHERE (CodigoContrato = @CodigoContrato) 
  ORDER BY
		NumeroOrdemAssinatura" ID="sdsResponsaveisMedicao" DeleteCommand="DELETE FROM AssinaturasMedicaoContrato WHERE (CodigoContrato = @CodigoContrato) AND (CodigoUsuario = @CodigoUsuario)"
        InsertCommand="INSERT INTO AssinaturasMedicaoContrato(CodigoContrato, CodigoUsuario, DescricaoCargo, NumeroOrdemAssinatura) VALUES (@CodigoContrato, @CodigoUsuario, @DescricaoCargo, @NumeroOrdemAssinatura)"
        UpdateCommand="UPDATE AssinaturasMedicaoContrato SET DescricaoCargo = @DescricaoCargo, NumeroOrdemAssinatura = @NumeroOrdemAssinatura WHERE (CodigoContrato = @CodigoContrato) AND (CodigoUsuario = @CodigoUsuario)"
        OnDeleting="sdsResponsaveisMedicao_Deleting">
        <DeleteParameters>
            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
            <asp:Parameter Name="CodigoUsuario" />
        </DeleteParameters>
        <InsertParameters>
            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
            <asp:Parameter Name="CodigoUsuario" />
            <asp:Parameter Name="DescricaoCargo" />
            <asp:Parameter Name="NumeroOrdemAssinatura" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter QueryStringField="CC" Name="CodigoContrato" Type="Int32">
            </asp:QueryStringParameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:QueryStringParameter Name="CodigoContrato" QueryStringField="CC" />
            <asp:Parameter Name="CodigoUsuario" />
            <asp:Parameter Name="DescricaoCargo" />
            <asp:Parameter Name="NumeroOrdemAssinatura" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsUsuarios" runat="server"></asp:SqlDataSource>
    <dxcb:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) {
	callbackComplete(s, e);
}" />
    </dxcb:ASPxCallback>
    <dxhf:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
    </dxhf:ASPxHiddenField>
    <dxpc:ASPxPopupControl ID="pcEditItemPai" runat="server" HeaderText="Atividade" Width="600px"
        ClientInstanceName="pcEditItemPai" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" AllowDragging="True">
        <ClientSideEvents Shown="function(s, e) {
	txtDescricaoItemPai.Focus();
}" />
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table width="100%">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Descrição">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 50px">
                                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Ordem">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-right: 5px">
                                        <dxe:ASPxTextBox ID="txtDescricaoItemPai" runat="server" ClientInstanceName="txtDescricaoItemPai"
                                            Width="100%" MaxLength="255">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="seOrdem" runat="server" Height="21px" Number="0" Width="100%"
                                            ClientInstanceName="seOrdem" NumberType="Integer">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-top: 10px">
                            <table>
                                <tr>
                                    <td style="padding-right: 10px">
                                        <dxe:ASPxButton ID="btnSalvarEdicaoItemPai" runat="server" Text="Salvar" AutoPostBack="False"
                                            ClientInstanceName="btnSalvarEdicaoItemPai" Width="75px">
                                            <ClientSideEvents Click="function(s, e) {
	salvaPopupEdicaoItemPai();
}" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btnCancelarEdicaoItemPai" runat="server" Text="Cancelar" AutoPostBack="False"
                                            ClientInstanceName="btnCancelarEdicaoItemPai" Width="75px">
                                            <ClientSideEvents Click="function(s, e) {
	fechaPopupEdicaoItemPai();
}" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
